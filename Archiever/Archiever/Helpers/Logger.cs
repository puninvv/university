using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Archiever.Helpers
{
    public class Logger
    {
        private static Logger m_instance;
        private static object m_lock = new object();
        private static object m_writerLock = new object();

        public static Logger Instance
        {
            get
            {
                if (m_instance == null)
                    lock (m_lock)
                    {
                        if (m_instance == null)
                            m_instance = new Logger();
                    }

                return m_instance;
            }
        }

        private StringBuilder m_sb;

        private Logger()
        {
            m_sb = new StringBuilder();
        }

        public void Log(string _str)
        {
            lock (m_writerLock)
                m_sb.Append(DateTime.Now).Append("\t").AppendLine(_str);
        }

        public void DropToFile(string _filePath)
        {
            File.WriteAllText(_filePath, m_sb.ToString());
        }
    }
}
