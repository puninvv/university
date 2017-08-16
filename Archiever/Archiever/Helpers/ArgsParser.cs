﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archiever.Helpers
{
    internal class ArgsParser
    {
        public ArgsParserResult Parse(string[] _args)
        {
            if (_args == null || _args.Length != 3)
                throw new ArgumentOutOfRangeException();

            var compress = _args[0].Equals("compress", StringComparison.InvariantCultureIgnoreCase) ? true : false;
            var inputPath = _args[1];
            var outputPath = _args[2];

            return new ArgsParserResult(inputPath, outputPath, compress);
        }
    }
}
