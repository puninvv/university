using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code2PngBot
{
    public class Logger
    {
        private static Logger m_instance = new Logger();

        public static Logger Instance
        {
            get
            {
                return m_instance;
            }
        }

        private Logger()
        {
        }

        public void Write(string _message)
        {
            using (var writer = File.AppendText("log.txt"))
            {
                writer.WriteLine(DateTime.Now + "\t"+ _message);
            }
        }
    }

}
