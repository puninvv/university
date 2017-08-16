using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Archiever.IO.Writers
{
    class CompressedBlocksWriter : UncompressedBlocksWriter
    {
        public CompressedBlocksWriter(string _fileFullPath)
            : base(_fileFullPath)
        {
        }

        protected override void WriteIndexedBlock(Stream _stream, IndexedBlock _block)
        {
            _stream.Write(BitConverter.GetBytes(_block.Bytes.Length), 0, 4);
            base.WriteIndexedBlock(_stream, _block);
        }
    }
}
