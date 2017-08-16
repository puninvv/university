using Archiever.Helpers;
using Archiever.IO.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Archiever.IO.Writers
{
    internal class UncompressedBlocksWriter : IOThreadWrapper, IBlocksWriter
    {
        private SortedList<int, IndexedBlock> m_blocks;
        private object m_lock = new object();

        public UncompressedBlocksWriter(string _fileFullPath) 
            : base(_fileFullPath)
        {
            m_blocks = new SortedList<int, IndexedBlock>();
            m_lock = new object();
        }

        public void Write(IndexedBlock _block)
        {
            lock (m_lock)
            {
                m_blocks.Add(_block.Index, _block);
            }
        }

        protected override void MainJob(CancellationToken _cancellationToken)
        {
            IsFinished = false;

            using (var stream = File.Open(m_fileFullPath, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                bool isLastBlockWritten = false;
                int currentPositionInQueue = 0;

                while (!_cancellationToken.IsCancellationRequested && !isLastBlockWritten)
                {
                    IndexedBlock block;

                    lock (m_lock)
                    {
                        if (m_blocks.Count == 0 || !m_blocks.ContainsKey(currentPositionInQueue))
                            continue;

                        block = m_blocks[currentPositionInQueue];
                        m_blocks.Remove(currentPositionInQueue);
                        currentPositionInQueue++;
                    }
                    
                    WriteIndexedBlock(stream, block);

                    if (block.IsLastBlock)
                        isLastBlockWritten = true;

                    Logger.Instance.Log(string.Format("Writer: in queue = {0}, total = {1}", m_blocks.Count, block.Index + 1));
                }
            }

            IsFinished = true;
        }

        protected virtual void WriteIndexedBlock(Stream _stream, IndexedBlock _block)
        {
            _stream.Write(_block.Bytes, 0, _block.Bytes.Length);
        }
    }
}
