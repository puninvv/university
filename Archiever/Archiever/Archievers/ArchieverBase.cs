using Archiever.Helpers;
using Archiever.IO.Readers;
using Archiever.IO.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archiever.Archievers
{
    internal abstract class ArchieverBase : ThreadWrapper
    {
        private BlocksReaderBase m_reader;
        private BlocksWriterBase m_writer;

        public ArchieverBase(BlocksReaderBase _reader, BlocksWriterBase _writer)
        {
            m_reader = _reader;
            m_writer = _writer;
        }

        protected override void MainJob(CancellationToken _cancellationToken)
        {
            int total = 0;

            while (!_cancellationToken.IsCancellationRequested && (!m_reader.IsFinished || !m_writer.IsFinished))
            {
                var block = m_reader.Get();

                if (block == null)
                    continue;

                m_writer.Write(Update(block));

                total++;
                Logger.Instance.Log(string.Format("ARchiever: total = {0}", total));
            }
        }

        protected abstract IndexedBlock Update(IndexedBlock _block);
    }
}
