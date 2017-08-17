using Newtonsoft.Json;
using System.Globalization;
using System.Text.RegularExpressions;

namespace OYMLCN
{
    /// <summary>
    /// JsonExtension
    /// </summary>
    public static partial class JsonExtension
    {
        private static string DecodeUnicode(Match match)
        {
            if (!match.Success)
                return null;
            char outStr = (char)int.Parse(match.Value.Remove(0, 2), NumberStyles.HexNumber);
            return new string(outStr, 1);
        }

        private static JsonSerializerSettings DefaultSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        /// <summary>
        /// 将对象转为JSON字符串
        /// </summary>
        /// <param name="data">任意对象</param>
        /// <param name="settings">序列化配置</param>
        /// <returns>JSON字符串</returns>
        public static string ToJsonString(this object data, JsonSerializerSettings settings = null)
        {
            var jsonString = JsonConvert.SerializeObject(data, settings ?? DefaultSettings);
            MatchEvaluator evaluator = new MatchEvaluator(DecodeUnicode);
            return Regex.Replace(jsonString, @"\\u[0123456789abcdef]{4}", evaluator);//或：[\\u007f-\\uffff]，\对应为\u000a，但一般情况下会保持\
        }

        /// <summary>
        /// JSON字符串转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonstr"></param>
        /// <returns></returns>
        public static T DeserializeJsonString<T>(this string jsonstr) => JsonConvert.DeserializeObject<T>(jsonstr);

    }
}
