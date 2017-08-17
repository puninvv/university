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
            ArgsParser.ArgsParserResult vars;

            try
            {
                vars = ArgsParser.Parse(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.Instance.Log(ex);
                Logger.Instance.DropToFile(Properties.Settings.Default.LogFileName);
                return;
            }

            var setup = Setup.CreateFrom(vars, Properties.Settings.Default.ArchieversCount);

            var token = new CancellationToken();

            setup.Start(token);
            setup.Join();
            setup.Stop();

            Logger.Instance.DropToFile(Properties.Settings.Default.LogFileName);
        }
    }
}
