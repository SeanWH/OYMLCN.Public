using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace OYMLCN
{
    /// <summary>
    /// HTMLExtension
    /// </summary>
    public static class HTMLExtension
    {
        /// <summary>
        /// 使用正则表达式删除Html标签
        /// </summary>
        /// <param name="html"></param>
        /// <param name="length">截取长度（默认0则返回完整结果）</param>
        /// <returns></returns>
        public static string RemoveHtml(this string html, int length = 0)
        {
            if (html == null)
                return string.Empty;
            string strText = Regex.Replace(html, "<[^>]+>", "");
            strText = Regex.Replace(strText, "&[^;]+;", "");

            if (length > 0 && strText.Length > length)
                return strText.Substring(0, length);

            return strText;
        }
        /// <summary>
        /// 使用正则表达式删除Script标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveScript(this string html)
        {
            if (html == null)
                return string.Empty;
            return Regex.Replace(html, @"(\<script(.+?)\</script\>)|(\<style(.+?)\</style\>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }
        /// <summary>
        /// 替换换行Br为换行符
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ReplaceBr(this string html)
        {
            if (html == null)
                return string.Empty;
            html = html.Replace("\r", "{{__rbrn__}}").Replace("\n", "{{__rbrn__}}").Replace("{{__rbrn__}}", "\r\n").Replace("\t", "\r\n").Replace("<br>", "\r\n").Replace("<br/>", "\r\n").Replace("<br />", "\r\n");
            do
                html = html.Replace("\r\n\r\n", "\r\n");
            while (html.Contains("\r\n\r\n\r\n"));
            return html;
        }
        /// <summary>
        /// 删除多余的空格其换行符
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveHtmlBrAndSpace(this string html)
        {
            if (html == null)
                return string.Empty;

            var lines = html.SplitByLine();
            for (var i = 0; i < lines.Length; i++)
                lines[i] = lines[i].Trim();
            html = string.Join("\r\n", lines);

            html = html.Replace("\r", "{{__rbrn__}}").Replace("\n", "{{__rbrn__}}").Replace("{{__rbrn__}}", "\r\n").Replace("\t", "\r\n").Replace("<br>", "\r\n").Replace("<br/>", "\r\n").Replace("<br />", "\r\n");
            do
                html = html.Replace("\r\n\r\n", "\r\n");
            while (html.Contains("\r\n\r\n\r\n"));
            return html;
        }
        /// <summary>
        /// 替换所有换行符
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveBr(this string html) => html.ReplaceBr().Replace("\r", "").Replace("\n", "").Replace("\t", "");
        /// <summary>
        /// Html的多个连续空格只保留一个
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveSpace(this string html)
        {
            if (html == null)
                return string.Empty;
            var str = string.Join(" ", html.Replace("&nbsp;", " ").Replace("　", " ").SplitBySign(" ", StringSplitOptions.RemoveEmptyEntries).ToArray());
            do str = str.Replace("  ", " ");
            while (str.Contains("  "));
            return str;
        }
        /// <summary>
        /// 去除所有HTML标签和换行获取第一行内容
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlGetFirstLine(this string html) =>
            html.RemoveSpace().ReplaceBr().RemoveScript().RemoveHtml().SplitBySign("\n").FirstOrDefault()?.SplitBySign("\r").FirstOrDefault()?.Trim();

        /// <summary>
        /// HTML转义为数据库合法模式
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string html) => WebUtility.HtmlEncode(html);
        /// <summary>
        /// 被转义HTML的字符串还原
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlDecode(this string html) => WebUtility.HtmlDecode(html);
        /// <summary>
        /// 将URL转义为合法参数地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlEncode(this string url) => WebUtility.UrlEncode(url);
        /// <summary>
        /// 被转义的URL字符串还原
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlDecode(this string url) => WebUtility.UrlDecode(url);
        /// <summary>
        /// 将 URL 中的参数名称/值编码为合法的格式。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncodeAsUrlData(this string str) => Uri.EscapeDataString(str);


        /// <summary>
        /// 组装QueryString
        /// 参数之间用&amp;连接，首位没有符号，如：a=1&amp;b=2&amp;c=3
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        public static string ToQueryString(this Dictionary<string, string> formData)
        {
            if (formData == null || formData.Count == 0)
                return "";

            StringBuilder sb = new StringBuilder();
            var i = 0;
            foreach (var kv in formData)
            {
                i++;
                sb.AppendFormat("{0}={1}", kv.Key.EncodeAsUrlData(), kv.Value.EncodeAsUrlData());
                if (i < formData.Count)
                    sb.Append("&");
            }
            return sb.ToString();
        }


    }
}
