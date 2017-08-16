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
    internal class Decompressor : ArchieverBase
    {
        public Decompressor(BlocksReaderBase _reader, BlocksWriterBase _writer) 
            : base(_reader, _writer)
        { }

        protected override IndexedBlock Update(IndexedBlock _block)
        {
            using (var memoryStream = new MemoryStream(_block.Bytes))
            {
                var resultBuffer = new byte[_block.Bytes.Length];

                using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    int readed = gzipStream.Read(resultBuffer, 0, resultBuffer.Length);
                    var tmpBuffer = new byte[readed];
                    Array.Copy(resultBuffer, tmpBuffer, readed);

                    resultBuffer = tmpBuffer;
                }

                return new IndexedBlock(_block.Index, resultBuffer, _block.IsLastBlock);
            }
        }
    }
}
