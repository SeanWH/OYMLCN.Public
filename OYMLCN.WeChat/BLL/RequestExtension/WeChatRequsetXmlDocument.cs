using System;
using OYMLCN.WeChat.Model;
#if !NETCOREAPP1_0
using System.Web;
using System.Net.Http;
#else
using Microsoft.AspNetCore.Http;
#endif
using System.Xml.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// Extension
    /// </summary>
    public static partial class Extension
    {
        private static WeChatRequsetXmlDocument GetWeChatRequsetXmlDocument(this XDocument xdoc, Config config, PostModel model)
        {
            if (CreateSignature(model.Timestamp, model.Nonce, config.Token) != model.Signature)
                throw new NotImplementedException("签名验证失败");

            var result = new WeChatRequsetXmlDocument();
            result.Config = config;
            result.PostModel = model;
            string encrypt = xdoc.Root.Element("Encrypt")?.Value;

            if (!encrypt.IsNullOrEmpty() && CreateSignature(model.Timestamp, model.Nonce, config.Token, encrypt) != model.MsgSignature)
                throw new NotImplementedException("消息体加密签名验证失败");

            result.Source = xdoc;
            if (encrypt.IsNullOrEmpty())
                result.Document = xdoc;
            else
            {
                result.IsEncrypt = true;
                result.Document = XDocument.Parse(Cryptography.AES_decrypt(encrypt, config.EncodingAESKey));
            }
            return result;
        }

#if !NETCOREAPP1_0
        /// <summary>
        /// 获取微信请求正文XmlDocument
        /// </summary>
        /// <param name="request"></param>
        /// <param name="config">如果开启加密模式或兼容模式则需要提供配置信息以用于解密消息体</param>
        /// <returns></returns>
        public static WeChatRequsetXmlDocument GetWeChatRequsetXmlDocument(this HttpRequestMessage request, Config config = null) => request.GetBody().ReadToEnd().ToXDocument().GetWeChatRequsetXmlDocument(config, request.GetPostModel());
        /// <summary>
        /// 获取微信请求正文XmlDocument
        /// </summary>
        /// <param name="request"></param>
        /// <param name="config">如果开启加密模式或兼容模式则需要提供配置信息以用于解密消息体</param>
        /// <returns></returns>
        public static WeChatRequsetXmlDocument GetWeChatRequsetXmlDocument(this HttpRequestBase request, Config config = null) => request.GetBody().ReadToEnd().ToXDocument().GetWeChatRequsetXmlDocument(config, request.GetPostModel());
#endif
        /// <summary>
        /// 获取微信请求正文XmlDocument
        /// </summary>
        /// <param name="request"></param>
        /// <param name="config">如果开启加密模式或兼容模式则需要提供配置信息以用于解密消息体</param>
        /// <returns></returns>
        public static WeChatRequsetXmlDocument GetWeChatRequsetXmlDocument(this HttpRequest request, Config config = null) => request.GetBody().ReadToEnd().ToXDocument().GetWeChatRequsetXmlDocument(config, request.GetPostModel());

    }
}
