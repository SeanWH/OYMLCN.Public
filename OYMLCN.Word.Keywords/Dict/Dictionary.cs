using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYMLCN.Word.Keywords
{
    internal static partial class Dict
    {
#if DEBUG
        public static ISet<string> StopWords
        {
            get
            {
                var stopWords = new HashSet<string>();
                var str = new StringBuilder();
                var dir = "Resources/";
                str.AppendLine((dir + "stopwords.txt").GetFileInfo().ReadAllText());
                str.AppendLine((dir + "stopwords_en_nltk.txt").GetFileInfo().ReadAllText());
                str.AppendLine((dir + "stopwords_zh_hit.txt").GetFileInfo().ReadAllText());
                var words = str.ToString().SplitByLine().Distinct().Where(d => !d.IsNullOrWhiteSpace()).OrderBy(d => d);
                foreach (var line in words)
                    stopWords.Add(line.Trim());
                "stopwords.gzip".GetFileInfo().WriteAllText(stopWords.ToJsonString().GZipCompressString());
                return stopWords;
            }
        }

        public static IDictionary<string, double> Idf
        {
            get
            {
                var idf = new Dictionary<string, double>();
                var str = new StringBuilder();
                var dir = "Resources/";
                str.AppendLine((dir + "idf.txt").GetFileInfo().ReadAllText());
                str.AppendLine((dir + "idf.txt.big").GetFileInfo().ReadAllText());
                var words = str.ToString().SplitByLine().Distinct().Where(d => !d.IsNullOrWhiteSpace()).OrderBy(d => d);
                foreach (var line in words)
                {
                    var word = line.SplitBySign(" ");
                    var key = word.First();
                    var freq = word.Skip(1).First().ConvertToDouble();
                    idf[key] = freq;
                }
                "idf.gzip".GetFileInfo().WriteAllText(idf.ToJsonString().GZipCompressString());
                return idf;
            }
        }

#endif
    }
}
