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
    internal class Compressor : ArchieverBase
    {
        public Compressor(BlocksReaderBase _reader, BlocksWriterBase _writer) 
            : base(_reader, _writer)
        { }

        protected override IndexedBlock Update(IndexedBlock _block)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
                {
                    gzipStream.Write(_block.Bytes, 0, _block.Bytes.Length);
                }

                return new IndexedBlock(_block.Index, memoryStream.ToArray(), _block.IsLastBlock);
            }
        }
    }
}
