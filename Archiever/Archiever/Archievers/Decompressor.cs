using Archiever.Helpers;
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
    internal class Decompressor : ThreadWrapper
    {
        private IBlocksReader m_reader;
        private IBlocksWriter m_writer;

        public Decompressor(IBlocksReader _reader, IBlocksWriter _writer)
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

                using (var memoryStream = new MemoryStream(block.Bytes))
                {
                    var resultBuffer = new byte[block.Bytes.Length];

                    using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                    {
                        int readed = gzipStream.Read(resultBuffer, 0, resultBuffer.Length);
                        var tmpBuffer = new byte[readed];
                        Array.Copy(resultBuffer, tmpBuffer, readed);

                        resultBuffer = tmpBuffer;
                    }

                    var result = new IndexedBlock(block.Index, resultBuffer, block.IsLastBlock);
                    m_writer.Write(result);
                }

                total++;
                Logger.Instance.Log(string.Format("Decompresor: total = {0}", total));
            }
        }
    }
}
