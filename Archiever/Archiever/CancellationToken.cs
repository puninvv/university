using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archiever
{
    internal class CancellationToken
    {
        private volatile bool m_flag = false;

        public bool IsCancellationRequested
        {
            get
            {
                return m_flag;
            }
        }

        public void RequestCancellation()
        {
            m_flag = true;
        }
    }
}
