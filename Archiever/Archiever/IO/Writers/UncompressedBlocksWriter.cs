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
    internal class UncompressedBlocksWriter : BlocksWriterBase
    {
        public UncompressedBlocksWriter(string _fileFullPath) 
            : base(_fileFullPath)
        { }

        protected override void WriteIndexedBlock(Stream _stream, IndexedBlock _block)
        {
            _stream.Write(_block.Bytes, 0, _block.Bytes.Length);
        }
    }
}
