using Archiever.Archievers;
using Archiever.IO;
using Archiever.IO.Readers;
using Archiever.IO.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static Archiever.Helpers.ArgsParser;

namespace Archiever.Helpers
{
    internal class Setup
    {
        public BlocksReaderBase Reader
        {
            get;
            private set;
        }

        public BlocksWriterBase Writer
        {
            get;
            private set;
        }

        public List<ArchieverBase> Archievers
        {
            get;
            private set;
        }

        public Setup(BlocksReaderBase _reader, BlocksWriterBase _writer, List<ArchieverBase> _archievers)
        {
            Reader = _reader;
            Writer = _writer;
            Archievers = _archievers;
        }

        public void Start(CancellationToken _token)
        {
            Reader.Start(_token);

            foreach (var archiever in Archievers)
                archiever.Start(_token);

            Writer.Start(_token);
        }

        public void Join()
        {
            while (!Reader.IsFinished || !Writer.IsFinished)
                Thread.Sleep(10);
        }

        public void Stop()
        {
            Reader.Stop();

            Writer.Stop();

            foreach (var archiever in Archievers)
                archiever.Stop();
        }

        public static Setup CreateFrom(ArgsParserResult _vars, int _archieversCount)
        {
            var reader = _vars.Compress ? (BlocksReaderBase)new UncompressedBlocksReader(_vars.InputFileName) : new CompressedBlocksReader(_vars.InputFileName);
            var writer = _vars.Compress ? (BlocksWriterBase)new CompressedBlocksWriter(_vars.OutputFileName) : new UncompressedBlocksWriter(_vars.OutputFileName);

            var archievers = new List<ArchieverBase>();

            for (int i = 0; i < _archieversCount; i++)
                archievers.Add(_vars.Compress ? (ArchieverBase)new Compressor(reader, writer) : new Decompressor(reader, writer));

            return new Setup(reader, writer, archievers);
        }
    }
}
