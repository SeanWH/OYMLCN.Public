using System;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 被动回复微信消息请求
    /// </summary>
    public partial class WeChatResponse
    {

        private WeChatResponse() { }
        /// <summary>
        /// 被动回复微信消息请求体 构建
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        WeChatResponse FillBaseResponse(WeChatRequest request, string response)
        {
            IsEncrypt = request.IsEncrypt;
            
            Source =
                "<xml>" +
                    $"<ToUserName><![CDATA[{request.Document.SelectValue("FromUserName")}]]></ToUserName>" +
                    $"<FromUserName><![CDATA[{request.Document.SelectValue("ToUserName")}]]></FromUserName>" +
                    $"<CreateTime>{request.Document.SelectValue("CreateTime")}</CreateTime>" +
                    response +
                "</xml>";

            if (IsEncrypt)
            {
                string encrypt = Cryptography.AES_encrypt(Source, request.Config.EncodingAESKey, request.Config.AppId);
                string signature = Signature.Create(request.PostModel.Timestamp, request.PostModel.Nonce, request.Config.Token, encrypt);
                Result =
                    "<xml>" +
                        $"<Encrypt><![CDATA[{encrypt}]]></Encrypt>" +
                        $"<MsgSignature><![CDATA[{signature}]]></MsgSignature>" +
                        $"<TimeStamp>{ request.PostModel.Timestamp}</TimeStamp>" +
                        $"<Nonce><![CDATA[{ request.PostModel.Nonce}]]></Nonce>" +
                    "</xml>";
            }
            else
                Result = Source;
            return this;
        }


        /// <summary>
        /// 是否加密的消息
        /// </summary>
        public bool IsEncrypt { get; private set; }
        /// <summary>
        /// 加密后或原始消息
        /// </summary>
        public string Result { get; private set; }
        /// <summary>
        /// 原始消息
        /// </summary>
        public string Source { get; private set; }

        /// <summary>
        /// 回复消息主体内容
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Result;


        /// <summary>
        /// 回复文本消息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content">回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）</param>
        /// <returns></returns>
        public static WeChatResponse ResponseText(WeChatRequest request, string content) =>
            new WeChatResponse().FillBaseResponse(request,
                    $"<MsgType><![CDATA[text]]></MsgType>" +
                    $"<Content><![CDATA[{content}]]></Content>"
                );
        /// <summary>
        /// 回复图片消息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="mediaId">通过素材管理中的接口上传多媒体文件，得到的id。</param>
        /// <returns></returns>
        public static WeChatResponse ResponseImage(WeChatRequest request, string mediaId) =>
            new WeChatResponse().FillBaseResponse(request,
                    $"<MsgType><![CDATA[image]]></MsgType>" +
                    $"<Image><MediaId><![CDATA[{mediaId}]]></MediaId></Image>"
                );
        /// <summary>
        /// 回复语音消息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="mediaId">通过素材管理中的接口上传多媒体文件，得到的id</param>
        /// <returns></returns>
        public static WeChatResponse ResponseVoice(WeChatRequest request, string mediaId) =>
            new WeChatResponse().FillBaseResponse(request,
                    $"<MsgType><![CDATA[voice]]></MsgType>" +
                    $"<Voice><MediaId><![CDATA[{mediaId}]]></MediaId></Voice>"
                );
        /// <summary>
        /// 回复视频消息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="mediaId">通过素材管理中的接口上传多媒体文件，得到的id</param>
        /// <param name="title">视频消息的标题</param>
        /// <param name="description">视频消息的描述</param>
        /// <returns></returns>
        public static WeChatResponse ResponseVideo(WeChatRequest request, string mediaId, string title = null, string description = null) =>
            new WeChatResponse().FillBaseResponse(request,
                    $"<MsgType><![CDATA[video]]></MsgType>" +
                    $"<Video>" +
                        $"<MediaId><![CDATA[{mediaId}]]></MediaId>" +
                        $"<Title><![CDATA[{title}]]></Title>" +
                        $"<Description><![CDATA[{description}]]></Description>" +
                    $"</Video>"
                );
        /// <summary>
        /// 回复音乐消息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="thumbMediaId">缩略图的媒体id，通过素材管理中的接口上传多媒体文件，得到的id</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="title">音乐标题</param>
        /// <param name="description">音乐描述</param>
        /// <param name="hqMusicUrl">高质量音乐链接，WIFI环境优先使用该链接播放音乐</param>
        /// <returns></returns>
        public static WeChatResponse ResponseMusic(WeChatRequest request, string thumbMediaId, string musicUrl = null, string title = null, string description = null, string hqMusicUrl = null) =>
            new WeChatResponse().FillBaseResponse(request,
                    $"<MsgType><![CDATA[music]]></MsgType>" +
                    $"<Music>" +
                        $"<Title><![CDATA[{title}]]></Title>" +
                        $"<Description><![CDATA[{description}]]></Description>" +
                        $"<MusicUrl><![CDATA[{musicUrl}]]></MusicUrl>" +
                        $"<HQMusicUrl><![CDATA[{hqMusicUrl}]]></HQMusicUrl>" +
                        $"<ThumbMediaId><![CDATA[{thumbMediaId}]]></ThumbMediaId>" +
                    $"</Music>"
                );

        /// <summary>
        /// 图文信息项
        /// </summary>
        public class Article
        {
            /// <summary>
            /// 单条图文
            /// </summary>
            /// <param name="title">图文消息标题</param>
            /// <param name="description">图文消息描述</param>
            /// <param name="picUrl">图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200</param>
            /// <param name="url">点击图文消息跳转链接</param>
            public Article(string title, string description, string picUrl, string url)
            {
                Title = title;
                Description = description;
                PicUrl = picUrl;
                Url = url;
            }
            /// <summary>
            /// 图文消息标题（可选）
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 图文消息描述（可选）
            /// </summary>
            public string Description { get; set; }
            /// <summary>
            /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200（可选）
            /// </summary>
            public string PicUrl { get; set; }
            /// <summary>
            /// 点击图文消息跳转链接（可选）
            /// </summary>
            public string Url { get; set; }
        }
        /// <summary>
        /// 回复图文消息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="param">多条图文消息信息，默认第一个item为大图,注意，如果图文数超过8，则将会无响应</param>
        /// <returns></returns>
        public static WeChatResponse ResponseNews(WeChatRequest request, params Article[] param)
        {
            if (param.Length > 8)
                throw new OverflowException("图文数量超过8条");
            var str = new StringBuilder()
                .Append("<MsgType><![CDATA[news]]></MsgType>")
                .Append($"<ArticleCount>{param.Length}</ArticleCount><Articles>");
            foreach (var item in param)
                str.Append(
                    $"<item>" +
                        $"<Title><![CDATA[{item.Title}]]></Title>" +
                        $"<Description><![CDATA[{item.Description}]]></Description>" +
                        $"<PicUrl><![CDATA[{item.PicUrl}]]></PicUrl>" +
                        $"<Url><![CDATA[{item.Url}]]></Url>" +
                    $"</item>");
            str.Append("</Articles>");
            return new WeChatResponse().FillBaseResponse(request, str.ToString());
        }

        /// <summary>
        /// 消息转移到客服平台
        /// </summary>
        /// <param name="request"></param>
        /// <param name="kfAccount">指定会话接入的客服账号</param>
        /// <returns></returns>

        public static WeChatResponse TransferToCustomerService(WeChatRequest request, string kfAccount = null)
        {
            kfAccount = kfAccount.IsNullOrEmpty() ? string.Empty :
                $"<TransInfo>" +
                    $"<KfAccount><![CDATA[{(kfAccount.Contains("@") ? kfAccount : $"{kfAccount}@{request.Config.AccountName}")}]]></KfAccount>" +
                $"</TransInfo>";
            return new WeChatResponse().FillBaseResponse(request, $"<MsgType><![CDATA[transfer_customer_service]]></MsgType>{kfAccount}");
        }

    }
}
