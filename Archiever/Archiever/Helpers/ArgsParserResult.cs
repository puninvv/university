using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archiever.Helpers
{
    internal class ArgsParserResult
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
}
