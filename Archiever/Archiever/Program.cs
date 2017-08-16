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
            args = new string[] { "compress", @"D:\LUGA_2017.mp4", @"D:\LUGA_2017_copy.mp4" };

            var vars = new ArgsParser().Parse(args);

            var reader = new BlocksReader(vars.InputFileName, 1024 * 1024 * 10);
            var writer = new BlocksWriter(vars.OutputFileName);
            var archiever = new Compressor(reader, writer);

            var token = new CancellationToken();
            reader.Start(token);
            archiever.Start(token);
            writer.Start(token);

            while (!reader.IsFinished || !writer.IsFinished)
                Thread.Sleep(10);

            archiever.Stop();
            reader.Stop();
            writer.Stop();

            Console.ReadKey();

            Logger.Instance.DropToFile(@"d:\log.txt");
        }
    }
}
