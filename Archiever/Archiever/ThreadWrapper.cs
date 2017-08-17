using Archiever.Helpers;
using Archiever.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Archiever
{
    internal abstract class ThreadWrapper
    {
        private Thread m_thread;
        protected CancellationToken m_cancellationToken;

        public virtual void Start(CancellationToken _cancellationToken)
        {
            if (m_thread != null)
                Stop();

            m_cancellationToken = _cancellationToken;
            m_thread = CreateThreadFromMainJob();
            m_thread.Start();
        }

        protected virtual Thread CreateThreadFromMainJob()
        {
            return new Thread(() =>
            {
                try
                {
                    MainJob(m_cancellationToken);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Log(ex);
                    m_cancellationToken.RequestCancellation();
                }
            });
        }

        protected abstract void MainJob(CancellationToken _cancellationToken);

        public virtual void Stop()
        {
            if (m_thread == null)
                return;

            m_cancellationToken.RequestCancellation();
            m_thread.Join();
            m_thread = null;
        }
    }
}
