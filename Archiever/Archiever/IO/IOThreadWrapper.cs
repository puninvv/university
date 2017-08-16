using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Archiever.IO
{
    internal abstract class IOThreadWrapper : ThreadWrapper, IOperation
    {
        protected string m_fileFullPath;

        public bool IsFinished
        {
            get;
            protected set;
        }

        public IOThreadWrapper(string _fileFullPath) 
        {
            m_fileFullPath = _fileFullPath;
            IsFinished = false;
        }

        protected override Thread CreateThreadFromMainJob()
        {
            return new Thread(() =>
               {
                   IsFinished = false;
                   MainJob(m_cancellationToken);
                   IsFinished = true;
               });
        }
    }
}
