using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archiever.IO.Writers
{
    internal interface IBlocksWriter : IOperation
    {
        void Write(IndexedBlock _block);
    }
}
