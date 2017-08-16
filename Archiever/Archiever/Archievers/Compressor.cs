﻿using Archiever.Helpers;
using Archiever.IO.Readers;
using Archiever.IO.Writers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

            while (!_cancellationToken.IsCancellationRequested && (!m_reader.IsFinished || !m_writer.IsFinished))
            {
                var block = m_reader.Get();

                if (block == null)
                    continue;

                using (var memoryStream = new MemoryStream())
                {
                    using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
                    {
                        gzipStream.Write(block.Bytes, 0, block.Bytes.Length);
                    }

                    var result = new IndexedBlock(block.Index, memoryStream.ToArray(), block.IsLastBlock);
                    m_writer.Write(result);
                }

                total++;
                Logger.Instance.Log(string.Format("Compressor: total = {0}", total));
            }
        }
    }
}
