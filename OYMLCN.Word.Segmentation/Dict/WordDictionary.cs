using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OYMLCN.Word.Segmentation
{
    partial class Word
    {
        static List<Word> _dict;
#if DEBUG
        internal static List<Word> Dict
        {
            get
            {
                if (_dict != null)
                    return _dict;

                _dict = new List<Word>();
                var dic = "Resources/";
                var str = new StringBuilder();
                str.AppendLine((dic + "dict.txt").GetFileInfo().ReadAllText());
                str.AppendLine((dic + "dict.big").GetFileInfo().ReadAllText());
                str.AppendLine((dic + "dict.small").GetFileInfo().ReadAllText());
                var lines = str.ToString().SplitByLine().Where(d => !d.Trim().IsNullOrWhiteSpace()).Distinct();
                var dict = new Dictionary<string, string>();
                foreach (var item in lines)
                {
                    var line = item.SplitBySign(" ");
                    if (line.Length == 3)
                    {
                        string key = line.FirstOrDefault();
                        string value = item.SubString(key.Length + 1);
                        dict[key] = value;
                    }
                }
                foreach (var item in dict)
                {
                    var line = item.Value.SplitBySign(" ");
                    _dict.Add(new Word()
                    {
                        Key = item.Key,
                        Frequency = line.First().ConvertToInt(),
                        Tag = line.Last().Trim()
                    });
                }
                "dict.gzip".GetFileInfo().WriteAllText(_dict.Distinct().ToJsonString().GZipCompressString());
                return _dict;
            }
        }
#endif
        public string Key { get; set; }
        public int Frequency { get; set; }
        public string Tag { get; set; }
    }
}
