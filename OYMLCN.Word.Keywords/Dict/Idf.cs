﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.Word.Keywords
{
    internal static partial class Dict
    {
#if !DEBUG
        static IDictionary<string, double> _idf;

        public static IDictionary<string, double> Idf =>
                .GZipDecompressString().DeserializeJsonString<IDictionary<string, double>>());
#endif
    }
}