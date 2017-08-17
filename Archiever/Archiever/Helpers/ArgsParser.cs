using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Archiever.Helpers
{
    internal class ArgsParser
    {
        public class ArgsParserResult
        {
            public string InputFileName
            {
                get;
                private set;
            }

            public string OutputFileName
            {
                get;
                private set;
            }

            public bool Compress
            {
                get;
                private set;
            }

            public ArgsParserResult(string _inputFileFullPath, string _outputFileFullPath, bool _compress)
            {
                InputFileName = _inputFileFullPath;
                OutputFileName = _outputFileFullPath;
                Compress = _compress;
            }
        }

        public static ArgsParserResult Parse(string[] _args)
        {
            if (_args == null || _args.Length != 3)
                throw new ArgumentOutOfRangeException("3 args awaited");

            var compress = IsCompressor(_args[0]);
            var inputPath = ParseInputFileFullPath(_args[1]);
            var outputPath = ParseOutputFilePath(_args[2]);

            return new ArgsParserResult(inputPath, outputPath, compress);
        }

        private static bool IsCompressor(string _mode)
        {
            if (string.IsNullOrEmpty(_mode))
                throw new ArgumentOutOfRangeException("Incorrect mode");

            if (_mode.Equals("compress", StringComparison.InvariantCultureIgnoreCase))
                return true;
            if (_mode.Equals("decompress", StringComparison.InvariantCultureIgnoreCase))
                return false;

            throw new ArgumentOutOfRangeException("Incorrect mode");
        }

        private static string ParseInputFileFullPath(string _inputFileFullPath)
        {
            if (string.IsNullOrEmpty(_inputFileFullPath) || !File.Exists(_inputFileFullPath))
                throw new ArgumentOutOfRangeException("Input file not exists or file path is empty");

            return _inputFileFullPath;
        }

        private static string ParseOutputFilePath(string _outputFilePath)
        {
            if (string.IsNullOrEmpty(_outputFilePath) || !Directory.Exists(Path.GetDirectoryName(_outputFilePath)))
                throw new ArgumentOutOfRangeException("Directory of output file is not exists or empty");

            return _outputFilePath;
        }
    }

}
