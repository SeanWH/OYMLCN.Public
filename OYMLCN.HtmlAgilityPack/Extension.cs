using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OYMLCN
{
    /// <summary>
    /// HtmlAgilityPackExtension
    /// </summary>
    public static class HtmlAgilityPackExtension
    {

        /// <summary>
        /// 将字符串转换为Html便捷操作模式
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlNode AsAgilityHtml(this string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode;
        }

        /// <summary>
        /// 获取指定路径内元素的Html
        /// </summary>
        /// <param name="hn"></param>
        /// <param name="domPath"></param>
        /// <returns></returns>
        public static string GetInnerHtml(this HtmlNode hn, string domPath) => hn?.SelectSingleNode(domPath)?.InnerHtml?.Trim();
        /// <summary>
        /// 获取指定路径内元素的文本
        /// </summary>
        /// <param name="hn"></param>
        /// <param name="domPath"></param>
        /// <returns></returns>
        public static string GetInnerText(this HtmlNode hn, string domPath) => hn?.SelectSingleNode(domPath)?.InnerText?.Trim();

        /// <summary>
        /// 获取指定路径元素的属性值
        /// </summary>
        /// <param name="gn"></param>
        /// <param name="domPath"></param>
        /// <param name="attr">属性名称</param>
        /// <param name="def">默认返回值</param>
        /// <returns></returns>
        public static string GetAttributeValue(this HtmlNode gn, string domPath, string attr, string def) =>
            gn?.SelectSingleNode(domPath)?.GetAttributeValue(attr, def);



        /// <summary>
        /// 获取具有指定属性名的元素
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static HtmlNode GetAttributeNode(this HtmlNode hnc, string attrName) =>
            hnc.GetAttributeNodes(attrName)?.FirstOrDefault();
        /// <summary>
        /// 获取具有指定属性名以及包含指定值的元素
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="attrName"></param>
        /// <param name="attrValue"></param>
        /// <returns></returns>
        public static HtmlNode GetAttributeNode(this HtmlNode hnc, string attrName, string attrValue) =>
            hnc.GetAttributeNodes(attrName, attrValue)?.FirstOrDefault();

        /// <summary>
        /// 获取具有指定属性名的元素集合
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static IEnumerable<HtmlNode> GetAttributeNodes(this HtmlNode hnc, string attrName) =>
            hnc.ChildNodes.GetAttributeNodes(attrName);
        /// <summary>
        /// 获取具有指定属性名以及包含指定值的元素集合
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="attrName"></param>
        /// <param name="attrValue"></param>
        /// <returns></returns>
        public static IEnumerable<HtmlNode> GetAttributeNodes(this HtmlNode hnc, string attrName, string attrValue) =>
            hnc.ChildNodes.GetAttributeNodes(attrName, attrValue);

        /// <summary>
        /// 获取具有指定属性名的元素
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static HtmlNode GetAttributeNode(this HtmlNodeCollection hnc, string attrName) =>
            hnc.GetAttributeNodes(attrName)?.FirstOrDefault();
        /// <summary>
        /// 获取具有指定属性名以及包含指定值的元素
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="attrName"></param>
        /// <param name="attrValue"></param>
        /// <returns></returns>
        public static HtmlNode GetAttributeNode(this HtmlNodeCollection hnc, string attrName, string attrValue) =>
            hnc.GetAttributeNodes(attrName, attrValue)?.FirstOrDefault();

        /// <summary>
        /// 获取具有指定属性名的元素集合
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static IEnumerable<HtmlNode> GetAttributeNodes(this HtmlNodeCollection hnc, string attrName) =>
            hnc.Where(d => d.Attributes.Where(i => i.Name == attrName).Any());
        /// <summary>
        /// 获取具有指定属性名以及包含指定值的元素集合
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="attrName"></param>
        /// <param name="attrValue"></param>
        /// <returns></returns>
        public static IEnumerable<HtmlNode> GetAttributeNodes(this HtmlNodeCollection hnc, string attrName, string attrValue) =>
            hnc.Where(d => d.Attributes.Where(i => i.Name == attrName && i.Value.Contains(attrValue)).Any());



        /// <summary>
        /// 获取具有指定Id名称的元素
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="idName"></param>
        /// <returns></returns>
        public static HtmlNode GetIdNode(this HtmlNode hnc, string idName) =>
            hnc.ChildNodes.Where(d => d.Id == idName).FirstOrDefault();
        /// <summary>
        /// 获取具有指定Id名称的元素
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="idName"></param>
        /// <returns></returns>
        public static HtmlNode GetIdNode(this HtmlNodeCollection hnc, string idName) =>
            hnc.Where(d => d.Id == idName).FirstOrDefault();

        /// <summary>
        /// 获取包含指定样式名称的元素
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static HtmlNode GetClassNameNode(this HtmlNode hnc, string className) =>
            hnc.GetAttributeNode("class", className);
        /// <summary>
        /// 获取包含指定样式名称的元素集合
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static IEnumerable<HtmlNode> GetClassNameNodes(this HtmlNode hnc, string className) =>
            hnc.GetAttributeNodes("class", className);
        /// <summary>
        /// 获取包含指定样式名称的元素
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static HtmlNode GetClassNameNode(this HtmlNodeCollection hnc, string className) =>
            hnc.GetAttributeNode("class", className);
        /// <summary>
        /// 获取包含指定样式名称的元素集合
        /// </summary>
        /// <param name="hnc"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static IEnumerable<HtmlNode> GetClassNameNodes(this HtmlNodeCollection hnc, string className) =>
            hnc.GetAttributeNodes("class", className);

    }
}
