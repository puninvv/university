using Archiever.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Archiever.IO.Readers
{
    internal abstract class BlocksReaderBase : IOThreadWrapper
    {
        private const int m_bufferLength = 1024 * 1024 * 10;
        private object m_lock;
        private Queue<IndexedBlock> m_queue;
        private int m_maxQueueLength;

        public BlocksReaderBase(string _fileFullPath, int _maxQueueLength = 20) 
            : base(_fileFullPath)
        {
            m_lock = new object();
            m_queue = new Queue<IndexedBlock>();
            m_maxQueueLength = _maxQueueLength;
        }

        public IndexedBlock Get()
        {
            lock (m_lock)
            {
                return m_queue.Count != 0 ? m_queue.Dequeue() : null;
            }
        }

        protected override void MainJob(CancellationToken _cancellationToken)
        {
            IsFinished = false;

            using (var stream = File.Open(m_fileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var index = 0;

                while (!_cancellationToken.IsCancellationRequested && stream.Position < stream.Length)
                {
                    if (m_queue.Count >= m_maxQueueLength)
                        continue;

                    var indexedBlock = ReadFromStream(stream, index);

                    lock (m_lock)
                    {
                        m_queue.Enqueue(indexedBlock);
                        index++;
                    }

                    Logger.Instance.Log(string.Format("Reader: in queue = {0}, total = {1}", m_queue.Count, index));

                    if (indexedBlock.IsLastBlock)
                        break;
                }
            }

            IsFinished = true;
        }

        protected abstract IndexedBlock ReadFromStream(Stream _stream, int _index);
    }
}
