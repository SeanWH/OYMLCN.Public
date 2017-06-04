using OYMLCN.WeChat.Model;
#if !NETCOREAPP1_0
using System.Web.Mvc;
using System.Web.Http;
#else
using Microsoft.AspNetCore.Mvc;
#endif
using System.Xml.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// Extension
    /// </summary>
    public static partial class Extension
    {
#if !NETCOREAPP1_0
        /// <summary>
        /// 获取微信请求正文XmlDocument
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="config">如果开启加密模式或兼容模式则需要提供配置信息以用于解密消息体</param>
        /// <returns></returns>
        public static WeChatRequsetXmlDocument GetWeChatRequsetXmlDocument(this ApiController controller, Config config = null) => controller.Request.GetWeChatRequsetXmlDocument(config);
#endif
        /// <summary>
        /// 获取微信请求正文XmlDocument
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="config">如果开启加密模式或兼容模式则需要提供配置信息以用于解密消息体</param>
        /// <returns></returns>
        public static WeChatRequsetXmlDocument GetWeChatRequsetXmlDocument(this Controller controller, Config config = null) => controller.Request.GetWeChatRequsetXmlDocument(config);
    }
}
