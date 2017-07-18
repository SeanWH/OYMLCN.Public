﻿using System;
using System.Collections.Generic;
using System.Text;
using OYMLCN;

namespace OYMLCN.Word.Segmentation
{
    partial class Word
    {
#if !DEBUG
        internal static List<Word> Dict =>
            .GZipDecompressString().DeserializeJsonString<List<Word>>());
#endif
    }
}