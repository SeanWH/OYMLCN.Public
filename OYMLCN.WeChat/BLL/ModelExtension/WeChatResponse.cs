using System.Collections.Generic;
using System.Text;
using OYMLCN.WeChat.Model;
using System.Linq;
using System.Collections;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// Extension
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 通过请求信息初始化回复实体
        /// </summary>
        /// <param name="rsp"></param>
        /// <param name="request">微信消息</param>
        public static T FiilByRequest<T>(this T rsp, WeChatMessageBase request) where T : WeChatResponseBase
        {
            rsp.CreateTime = request.CreateTime;
            rsp.FromUserName = request.ToUserName;
            rsp.ToUserName = request.FromUserName;

            rsp.IsEncrypt = request.IsEncrypt;
            rsp.Config = request.Config;
            rsp.PostModel = request.PostModel;
            return rsp;
        }

        private class DictionarySort : IComparer
        {
            public int Compare(object oLeft, object oRight)
            {
                string sLeft = oLeft as string;
                string sRight = oRight as string;
                int iLeftLength = sLeft.Length;
                int iRightLength = sRight.Length;
                int index = 0;
                while (index < iLeftLength && index < iRightLength)
                {
                    if (sLeft[index] < sRight[index])
                        return -1;
                    else if (sLeft[index] > sRight[index])
                        return 1;
                    else
                        index++;
                }
                return iLeftLength - iRightLength;

            }
        }

        private static WeChatResponseXmlDocument FillXmlDocument(this WeChatResponseXmlDocument xdoc, WeChatResponseBase rsp)
        {
            string source = rsp.ToString(),
                   encrypt = string.Empty;
            xdoc.Source = source;

            if (rsp.IsEncrypt)
            {
                StringBuilder str = new StringBuilder();
                encrypt = Cryptography.AES_encrypt(source, rsp.Config.EncodingAESKey, rsp.Config.AppId);
                string signature = Extension.CreateSignature(rsp.PostModel.Timestamp, rsp.PostModel.Nonce, rsp.Config.Token, encrypt);
                str.AppendFormat("<xml><Encrypt><![CDATA[{0}]]></Encrypt>", encrypt);
                str.AppendFormat("<MsgSignature><![CDATA[{0}]]></MsgSignature>", signature);
                str.AppendFormat("<TimeStamp>{0}</TimeStamp>", rsp.PostModel.Timestamp);
                str.AppendFormat("<Nonce><![CDATA[{0}]]></Nonce></xml>", rsp.PostModel.Nonce);
                xdoc.Result = str.ToString();
            }
            else
                xdoc.Result = source;
            return xdoc;
        }

        /// <summary>
        /// 回复文本消息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="content">回复正文</param>
        /// <param name="param">拼接参数</param>
        /// <returns></returns>
        public static WeChatResponseXmlDocument ResponseText(this WeChatMessageBase req, string content, params string[] param) =>
            new WeChatResponseXmlDocument().FillXmlDocument(new WeChatResponseText(string.Format(content, param)).FiilByRequest(req));
        /// <summary>
        /// 回复图片消息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="mediaId">图片消息媒体id</param>
        /// <returns></returns>
        public static WeChatResponseXmlDocument ResponseImage(this WeChatMessageBase req, string mediaId) =>
             new WeChatResponseXmlDocument().FillXmlDocument(new WeChatResponseImage(mediaId).FiilByRequest(req));
        /// <summary>
        /// 回复语音消息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="mediaId">语音消息媒体id</param>
        /// <returns></returns>
        public static WeChatResponseXmlDocument ResponseVoice(this WeChatMessageBase req, string mediaId) =>
             new WeChatResponseXmlDocument().FillXmlDocument(new WeChatResponseVoice(mediaId).FiilByRequest(req));
        /// <summary>
        /// 回复视频消息（视频素材需要通过审核方可回复，否则微信不会有响应）
        /// </summary>
        /// <param name="req"></param>
        /// <param name="mediaId">视频消息媒体id</param>
        /// <param name="title">视频消息的标题（可选）</param>
        /// <param name="description">视频消息的描述（可选）</param>
        /// <returns></returns>
        public static WeChatResponseXmlDocument ResponseVideo(this WeChatMessageBase req, string mediaId, string title = null, string description = null) =>
             new WeChatResponseXmlDocument().FillXmlDocument(new WeChatResponseVideo(mediaId, title, description).FiilByRequest(req));
        /// <summary>
        /// 回复音乐消息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="thumbMediaId">缩略图的媒体id</param>
        /// <param name="musicUrl">音乐链接（可选）</param>
        /// <param name="title">音乐标题（可选）</param>
        /// <param name="description">音乐描述（可选）</param>
        /// <param name="hqMusicUrl">高质量音乐链接，WIFI环境优先使用该链接播放音乐（可选）</param>
        /// <returns></returns>
        public static WeChatResponseXmlDocument ResponseMusic(this WeChatMessageBase req, string thumbMediaId, string musicUrl = null, string title = null, string description = null, string hqMusicUrl = null) =>
             new WeChatResponseXmlDocument().FillXmlDocument(new WeChatResponseMusic(thumbMediaId, musicUrl, title, description, hqMusicUrl).FiilByRequest(req));
        /// <summary>
        /// 回复图文消息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="news">图文条目</param>
        /// <returns></returns>
        public static WeChatResponseXmlDocument ResponseNews(this WeChatMessageBase req, params WeChatResponseNewItem[] news) => ResponseNews(req, news.ToList());
        /// <summary>
        /// 回复图文消息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="news">图文条目</param>
        /// <returns></returns>
        public static WeChatResponseXmlDocument ResponseNews(this WeChatMessageBase req, List<WeChatResponseNewItem> news) =>
            new WeChatResponseXmlDocument().FillXmlDocument(new WeChatResponseNews(news).FiilByRequest(req));
        /// <summary>
        /// 转移消息到客服平台
        /// </summary>
        /// <param name="req"></param>
        /// <param name="kfName">指定的客服名称或完整客服账号（为空则不指定客服直接转接到客服平台）</param>
        /// <returns></returns>
        public static WeChatResponseXmlDocument TransferToCustomerService(this WeChatEventMessageBase req, string kfName = null)
        {
            if (!kfName.IsNullOrEmpty() && !kfName.Contains("@"))
                kfName = string.Format("{0}@{1}", kfName, req.Config.Name);
            return new WeChatResponseXmlDocument().FillXmlDocument(new WeChatResponseTransferToCustomerService(kfName).FiilByRequest(req));
        }
    }
}
