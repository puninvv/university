using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Archiever.IO.Writers
{
    class CompressedBlocksWriter : BlocksWriterBase
    {
        public CompressedBlocksWriter(string _fileFullPath)
            : base(_fileFullPath)
        { }

        protected override void WriteIndexedBlock(Stream _stream, IndexedBlock _block)
        {
            _stream.Write(BitConverter.GetBytes(_block.Bytes.Length), 0, 4);
            _stream.Write(_block.Bytes, 0, _block.Bytes.Length);
        }
    }
}
