using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archiever
{
    internal class IndexedBlock
    {
        public int Index
        {
            get;
            private set;
        }

        public byte[] Bytes
        {
            get;
            private set;
        }
        
        public bool IsLastBlock
        {
            get;
            private set;
        }

        public IndexedBlock(int _index, byte[] _bytes, bool _isLastBlock = false)
        {
            Index = _index;
            Bytes = _bytes;
            IsLastBlock = _isLastBlock;
        }
    }
}
