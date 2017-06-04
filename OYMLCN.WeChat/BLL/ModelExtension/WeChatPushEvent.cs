using OYMLCN.WeChat.Enum;
using OYMLCN.WeChat.Model;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// Extension
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 反序列化Xml数据为微信模板消息推送结果事件
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatPush模板消息 ToPush模板消息(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            var result = new WeChatPush模板消息()
            {
                Status = dom.SelectValue("Status")
            };
            return result.FillByDom(xdoc);
        }

        /// <summary>
        /// 反序列化Xml数据为微信群发消息推送结果事件
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatPush群发消息 ToPush群发消息(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            var result = new WeChatPush群发消息()
            {
                MsgId = dom.SelectValue("MsgID").ConvertToLong(),
                Status = dom.SelectValue("Status"),
                TotalCount = dom.SelectValue("TotalCount").ConvertToInt(),
                FilterCount = dom.SelectValue("FilterCount").ConvertToInt(),
                SentCount = dom.SelectValue("SentCount").ConvertToInt(),
                ErrorCount = dom.SelectValue("ErrorCount").ConvertToInt()
            };
            var items = dom.Elements("CopyrightCheckResult").Elements("ResultList").Elements("item");
            result.ResultList = new List<WeChatPushMassCheckResult>();
            foreach (var item in items)
            {
                var checkResult = new WeChatPushMassCheckResult()
                {
                    ArticleIdx = item.SelectValue("ArticleIdx").ConvertToByte(),
                    UserDeclareState = item.SelectValue("UserDeclareState") == "0" ? false : true,
                    AuditState = (MassCheckState)item.SelectValue("AuditState").ConvertToInt(),
                    OriginalArticleUrl = item.SelectValue("OriginalArticleUrl"),
                    OriginalArticleType = item.SelectValue("OriginalArticleType"),
                    CanReprint = item.SelectValue("CanReprint") == "0" ? false : true,
                    NeedReplaceContent = item.SelectValue("NeedReplaceContent") == "0" ? false : true,
                    NeedShowReprintSource = item.SelectValue("NeedShowReprintSource") == "0" ? false : true
                };
                result.ResultList.Add(checkResult);
            }
            return result.FillByDom(xdoc);
        }
    }
}
