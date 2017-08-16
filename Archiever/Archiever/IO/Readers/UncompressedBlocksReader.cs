using Archiever.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Archiever.IO.Readers
{
    internal class UncompressedBlocksReader : BlocksReaderBase
    {
        private const int m_bufferLength = 1024 * 1024 * 10;

        public UncompressedBlocksReader(string _fileFullPath) 
            : base(_fileFullPath)
        { }

        protected override IndexedBlock ReadFromStream(Stream _stream, int _index)
        {
            var buffer = new byte[m_bufferLength];

            int readed = _stream.Read(buffer, 0, m_bufferLength);

            if (readed == m_bufferLength)
                return new IndexedBlock(_index, buffer);

            var cuttedBuffer = new byte[readed];
            Array.Copy(buffer, cuttedBuffer, readed);
            return new IndexedBlock(_index, cuttedBuffer, _isLastBlock: true);
        }
    }
}
