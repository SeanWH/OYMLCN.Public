using OYMLCN.WeChat.Model;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// Extension
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        ///  反序列化Xml数据为客服接入会话推送结果事件
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatPushCustomerService接入会话 ToPushCustomerService接入会话(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            var result = new WeChatPushCustomerService接入会话();
            result.KFAccount = dom.SelectValue("KfAccount");
            return result.FillByDom(xdoc);
        }
        /// <summary>
        ///  反序列化Xml数据为客服关闭会话推送结果事件
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatPushCustomerService关闭会话 ToPushCustomerService关闭会话(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            var result = new WeChatPushCustomerService关闭会话();
            result.KFAccount = dom.SelectValue("KfAccount");
            return result.FillByDom(xdoc);
        }
        /// <summary>
        ///  反序列化Xml数据为客服转接会话推送结果事件
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatPushCustomerService转接会话 ToPushCustomerService转接会话(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            var result = new WeChatPushCustomerService转接会话();
            result.FromKfAccount = dom.SelectValue("FromKfAccount");
            result.ToKfAccount = dom.SelectValue("ToKfAccount");
            return result.FillByDom(xdoc);
        }
    }
}
