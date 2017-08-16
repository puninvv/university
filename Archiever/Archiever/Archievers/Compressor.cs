using Archiever.Helpers;
using Archiever.IO.Readers;
using Archiever.IO.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archiever.Archievers
{
    internal class Compressor : ThreadWrapper
    {
        private IBlocksReader m_reader;
        private IBlocksWriter m_writer;

        public Compressor(IBlocksReader _reader, IBlocksWriter _writer)
        {
            m_reader = _reader;
            m_writer = _writer;
        }

        protected override void MainJob(CancellationToken _cancellationToken)
        {
            int total = 0;

            while (!m_reader.IsFinished || !m_writer.IsFinished || !_cancellationToken.IsCancellationRequested)
            {
                var block = m_reader.Get();
                if (block == null)
                    continue;
                m_writer.Write(block);

                total++;

                Logger.Instance.Log(string.Format("Compressor: total = {0}", total));
            }
        }
    }
}
