using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Archiever.IO.Readers
{
    internal class CompressedBlocksReader : UncompressedBlocksReader
    {
        public CompressedBlocksReader(string _fileFullPath) 
            : base(_fileFullPath)
        {
        }

        protected override IndexedBlock ReadFromStream(Stream _stream, int _index)
        {
            var lengthBytes = new byte[4];
            _stream.Read(lengthBytes, 0, 4);

            int length = BitConverter.ToInt32(lengthBytes, 0);

            var buffer = new byte[length];
            int readed = _stream.Read(buffer, 0, length);

            return new IndexedBlock(_index, buffer, _stream.Position == _stream.Length);
        }
    }
}
