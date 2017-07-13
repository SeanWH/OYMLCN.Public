using System;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.Word.Segmentation.Dictionary
{
    public static partial class Viterbi
    {
#if DEBUG

        const string dic = "Resources/";

        public static IDictionary<char, IDictionary<char, double>> TransProbs
        {
            get
            {
                var d = (dic+ "prob_trans.json").GetFileInfo().ReadAllText().DeserializeJsonString<IDictionary<char, IDictionary<char, double>>>();
                "prob_trans.gzip".GetFileInfo().WriteAllText(d.ToJsonString().GZipCompressString());
                return d;
            }
        }

        public static IDictionary<char, IDictionary<char, double>> EmitProbs
        {
            get
            {
                var d = (dic + "prob_emit.json").GetFileInfo().ReadAllText().DeserializeJsonString<IDictionary<char, IDictionary<char, double>>>();
                "prob_emit.gzip".GetFileInfo().WriteAllText(d.ToJsonString().GZipCompressString());
                return d;
            }
        }


        public static IDictionary<string, double> PosStartProbs
        {
            get
            {
                var d = (dic + "pos_prob_start.json").GetFileInfo().ReadAllText().DeserializeJsonString<IDictionary<string, double>>();
                "pos_prob_start.gzip".GetFileInfo().WriteAllText(d.ToJsonString().GZipCompressString());
                return d;
            }
        }
        public static IDictionary<string, IDictionary<string, double>> PosTransProbs
        {
            get
            {
                var d = (dic + "pos_prob_trans.json").GetFileInfo().ReadAllText().DeserializeJsonString<IDictionary<string, IDictionary<string, double>>>();
                "pos_prob_trans.gzip".GetFileInfo().WriteAllText(d.ToJsonString().GZipCompressString());
                return d;
            }
        }
        public static IDictionary<string, IDictionary<char, double>> PosEmitProbs
        {
            get
            {
                var d = (dic + "pos_prob_emit.json").GetFileInfo().ReadAllText().DeserializeJsonString<IDictionary<string, IDictionary<char, double>>>();
                "pos_prob_emit.gzip".GetFileInfo().WriteAllText(d.ToJsonString().GZipCompressString());
                return d;
            }
        }
        public static IDictionary<char, List<string>> StateTab
        {
            get
            {
                var d = (dic + "char_state_tab.json").GetFileInfo().ReadAllText().DeserializeJsonString<IDictionary<char, List<string>>>();
                "char_state_tab.gzip".GetFileInfo().WriteAllText(d.ToJsonString().GZipCompressString());
                return d;
            }
        }
#endif
    }
}
