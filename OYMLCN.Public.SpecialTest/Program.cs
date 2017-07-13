using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using OYMLCN.Word.Segmentation;
using System.Diagnostics;

namespace OYMLCN.Open.SpecialTest
{
    class Program
    {
        static void Main(string[] args)
        {
#if NETCOREAPP1_0
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
            var segmenter = new Segmenter();

            //var segments = segmenter.Cut("我来到北京清华大学", cutAll: true);
            //Console.WriteLine("【全模式】：{0}", string.Join("/ ", segments));

            //segments = segmenter.Cut("我来到北京清华大学");  // 默认为精确模式
            //Console.WriteLine("【精确模式】：{0}", string.Join("/ ", segments));

            //segments = segmenter.Cut("他来到了网易杭研大厦");  // 默认为精确模式，同时也使用HMM模型
            //Console.WriteLine("【新词识别】：{0}", string.Join("/ ", segments));

            //segments = segmenter.CutForSearch("小明硕士毕业于中国科学院计算所，后在日本京都大学深造"); // 搜索引擎模式
            //Console.WriteLine("【搜索引擎模式】：{0}", string.Join("/ ", segments));

            //segments = segmenter.Cut("结过婚的和尚未结过婚的");
            //Console.WriteLine("【歧义消除】：{0}", string.Join("/ ", segments));


            //segments = segmenter.Cut("北京大学生喝进口红酒");
            //Console.WriteLine("【歧义消除】：{0}", string.Join("/ ", segments));

            //segments = segmenter.Cut("在北京大学生活区喝进口红酒");
            //Console.WriteLine("【歧义消除】：{0}", string.Join("/ ", segments));

            //segments = segmenter.Cut("腾讯视频致力于打造中国最大的在线视频媒体平台,以丰富的内容、极致的观看体验");
            //Console.WriteLine("【精确模式】：{0}", string.Join("/ ", segments));

            //segmenter.DeleteWord("湖南");
            //segmenter.AddWord("湖南");
            //segmenter.AddWord("长沙市");
            //segments = segmenter.Cut("湖南长沙市天心区");
            //Console.WriteLine("【精确模式】：{0}", string.Join("/ ", segments));

            var tab = OYMLCN.Word.Segmentation.Dictionary.Viterbi.StateTab;
            var e = OYMLCN.Word.Segmentation.Dictionary.Viterbi.PosEmitProbs;
            var s = OYMLCN.Word.Segmentation.Dictionary.Viterbi.PosStartProbs;
            var t = OYMLCN.Word.Segmentation.Dictionary.Viterbi.PosTransProbs;

            Console.ReadKey();

            Console.ReadLine();
        }
    }
}