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
            //args = new string[] { "compress", @"D:\compress\LUGA_2017.mp4", @"D:\compress\LUGA_2017_copy.pvv" };
            args = new string[] { "decompress", @"D:\decompress\LUGA_2017_copy.pvv", @"D:\decompress\LUGA_2017.mp4" };

            var vars = new ArgsParser().Parse(args);

            if (vars.Compress)
                Compress(vars);
            else
                Decompress(vars);
        }

        private static void Compress(ArgsParserResult _vars)
        {
            var reader = new UncompressedBlocksReader(_vars.InputFileName);
            var writer = new CompressedBlocksWriter(_vars.OutputFileName);
            var archiever = new Compressor(reader, writer);
            var archiever1 = new Compressor(reader, writer);
            var archiever2 = new Compressor(reader, writer);

            var token = new CancellationToken();
            reader.Start(token);
            archiever.Start(token);
            archiever1.Start(token);
            archiever2.Start(token);
            writer.Start(token);

            while (!reader.IsFinished || !writer.IsFinished)
                Thread.Sleep(10);

            archiever.Stop();
            archiever1.Stop();
            archiever2.Stop();
            reader.Stop();
            writer.Stop();

            Console.ReadKey();

            Logger.Instance.DropToFile(@"d:\log_compress.txt");
        }


        private static void Decompress(ArgsParserResult _vars)
        {
            var reader = new CompressedBlocksReader(_vars.InputFileName);
            var writer = new UncompressedBlocksWriter(_vars.OutputFileName);
            var archiever = new Decompressor(reader, writer);
            var archiever1 = new Decompressor(reader, writer);
            var archiever2 = new Decompressor(reader, writer);

            var token = new CancellationToken();
            reader.Start(token);
            archiever.Start(token);
            archiever1.Start(token);
            archiever2.Start(token);
            writer.Start(token);

            while (!reader.IsFinished || !writer.IsFinished)
                Thread.Sleep(10);

            archiever.Stop();
            archiever1.Stop();
            archiever2.Stop();
            reader.Stop();
            writer.Stop();

            Console.ReadKey();

            Logger.Instance.DropToFile(@"d:\log)decompress.txt");
        }
    }
}
