using Archiever.Archievers;
using Archiever.Helpers;
using Archiever.IO.Readers;
using Archiever.IO.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Archiever
{
    class Program
    {
        static void Main(string[] args)
        {
            var vars = ArgsParser.Parse(args);

            var setup = Setup.CreateFrom(vars, 3);

            var token = new CancellationToken();

            setup.Start(token);
            setup.Join();
            setup.Stop();
           
            Logger.Instance.DropToFile(@"log.txt");
        }
    }
}
