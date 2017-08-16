using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archiever.IO.Readers
{
    internal interface IBlocksReader : IOperation
    { 
        IndexedBlock Get();
    }
}
