using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
//using OYMLCN.Word.Segmentation;
using System.Diagnostics;
using System.Linq;
//using OYMLCN.Word.Keywords;
//using OYMLCN.Word.Segmentation.Pos;

namespace OYMLCN.Open.SpecialTest
{
    class Program
    {
        static void Main(string[] args)
        {
#if NETCOREAPP1_0
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
            //string str = "我来到北京清华大学";
            //Console.WriteLine("【全模式】：{0}", string.Join("/ ", str.CutAllApart()));
            //Console.WriteLine("【精确模式】：{0}", string.Join("/ ", str.CutApart()));
            //str = "他来到了网易杭研大厦";
            //Console.WriteLine("【新词识别】：{0}", string.Join("/ ", str.CutApart()));
            //str = "小明硕士毕业于中国科学院计算所，后在日本京都大学深造";
            //Console.WriteLine("【搜索引擎模式】：{0}", string.Join("/ ", str.CutApartForSearch()));
            //str = "结过婚的和尚未结过婚的";
            //Console.WriteLine("【歧义消除】：{0}", string.Join("/ ", str.CutApart()));
            //str = "北京大学生喝进口红酒";
            //Console.WriteLine("【歧义消除】：{0}", string.Join("/ ", str.CutApart()));
            //str = "在北京大学生活区喝进口红酒";
            //Console.WriteLine("【歧义消除】：{0}", string.Join("/ ", str.CutApart()));
            //str = "腾讯视频致力于打造中国最大的在线视频媒体平台,以丰富的内容、极致的观看体验";
            //Console.WriteLine("【精确模式】：{0}", string.Join("/ ", str.CutApart()));
            //var segmenter = new Segmenter();
            //segmenter.DeleteWord("湖南");
            //segmenter.AddWord("湖南");
            //segmenter.AddWord("长沙市");
            //str = "湖南长沙市天心区";
            //Console.WriteLine("【精确模式】：{0}", string.Join("/ ", str.CutApart(segmenter)));

            //str = "永和服装饰品有限公司";
            //var tokens = str.GetWordTokenize();
            //foreach (var token in tokens)
            //    Console.WriteLine("word {0,-12} start: {1,-3} end: {2,-3}", token.Word, token.StartIndex, token.EndIndex);
            //tokens = str.GetWordTokenizeForSearch();
            //foreach (var token in tokens)
            //    Console.WriteLine("word {0,-12} start: {1,-3} end: {2,-3}", token.Word, token.StartIndex, token.EndIndex);

            //str = "一团硕大无朋的高能离子云，在遥远而神秘的太空中迅疾地飘移";
            //var pairs = str.CutApartAndGetFlag();
            //Console.WriteLine(string.Join(" ", pairs.Select(token => string.Format("{0}/{1}", token.Word, token.Flag))));

            //str = "程序员(英文Programmer)是从事程序开发、维护的专业人员。一般将程序员分为程序设计人员和程序编码人员，但两者的界限并不非常清楚，特别是在中国。软件从业人员分为初级程序员、高级程序员、系统分析员和项目经理四大类。";
            //Console.WriteLine(string.Join(" ", str.GetKeyWords()));

            //str = @"在数学和计算机科学/算学之中，算法/算则法（Algorithm）为一个计算的具体步骤，常用于计算、数据处理和自动推理。精确而言，算法是一个表示为有限长列表的有效方法。算法应包含清晰定义的指令用于计算函数。
            //        算法中的指令描述的是一个计算，当其运行时能从一个初始状态和初始输入（可能为空）开始，经过一系列有限而清晰定义的状态最终产生输出并停止于一个终态。一个状态到另一个状态的转移不一定是确定的。随机化算法在内的一些算法，包含了一些随机输入。
            //        形式化算法的概念部分源自尝试解决希尔伯特提出的判定问题，并在其后尝试定义有效计算性或者有效方法中成形。这些尝试包括库尔特·哥德尔、雅克·埃尔布朗和斯蒂芬·科尔·克莱尼分别于1930年、1934年和1935年提出的递归函数，阿隆佐·邱奇于1936年提出的λ演算，1936年Emil Leon Post的Formulation 1和艾伦·图灵1937年提出的图灵机。即使在当前，依然常有直觉想法难以定义为形式化算法的情况。";
            //Console.WriteLine(string.Join("\r\n", str.GetKeyWordsWegiht(int.MaxValue).Select(d => "{0}/{1}".StringFormat(d.Word, d.Weight.ToString()))));
                    
            Console.WriteLine("哈哈".Pinyin().TotalPinYin);

            Console.ReadLine();
        }
    }
}