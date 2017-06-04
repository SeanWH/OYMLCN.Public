using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 群发接口辅助通讯逻辑
    /// </summary>
    public static class MassMessageApi
    {
        private static string CreateJson(string type, string data, int tagId = 0, bool sendIgnoreReprint = false, List<string> openid = null, string previewOpenId = null)
        {
            StringBuilder str = new StringBuilder();
            str.Append("{");
            if (openid == null)
            {
                str.Append("\"filter\":{");
                str.Append("\"is_to_all\":" + (tagId == 0 ? "true" : "false") + "");
                if (tagId != 0)
                    str.Append(",\"tag_id\":" + tagId.ToString());
                str.Append("},");
            }
            else if (!string.IsNullOrWhiteSpace(previewOpenId))
                str.Append("\"towxname\":\"" + previewOpenId + "\",");
            else
            {
                str.Append("\"touser\":[");
                foreach (var item in openid)
                    str.Append("\"" + item + "\",");
                str.Append("],");
            }

            str.Append("\"" + type + "\":{");
            if (type == "text")
                str.Append("\"content\":\"" + data + "\"");
            else if (type == "wxcard")
                str.Append("\"card_id\":\"" + data + "\"");
            else
                str.Append("\"media_id\":\"" + data + "\"");

            if (type == "mpnews" && string.IsNullOrWhiteSpace(previewOpenId))
                str.Append("},\"msgtype\":\"mpnews\",\"send_ignore_reprint\":" + (sendIgnoreReprint ? "1" : "0") + "}");
            else
                str.Append("},\"msgtype\":\"" + type + "\"}");

            return str.ToString();
        }

        /// <summary>
        /// 上传图文消息内的图片获取URL
        /// 有效字段：url
        /// 请注意，本接口所上传的图片不占用公众号的素材库中图片数量的5000个的限制。图片仅支持jpg/png格式，大小必须在1MB以下。
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static MediaItem MassMessageImageUpload(this AccessToken token, string filePath)
        {
            var file = new Dictionary<string, string>();
            file["media"] = filePath;
            return HttpClient.PostData(CoreApi.ApiUrl("/cgi-bin/media/uploadimg?access_token={0}", token.access_token), queryDir: file).ReadToEnd().DeserializeJsonString<MediaItem>();
        }
        /// <summary>
        /// 上传图文消息素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="items">图文列表</param>
        /// <returns></returns>
        public static MassMessageUpload MassMessageNewsUpload(this AccessToken token, params MediaNewItem[] items) => token.MassMessageNewsUpload(items.ToList());
        /// <summary>
        /// 上传图文消息素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="items">图文列表</param>
        /// <returns></returns>
        public static MassMessageUpload MassMessageNewsUpload(this AccessToken token, List<MediaNewItem> items)
        {
            StringBuilder str = new StringBuilder();
            str.Append("{\"articles\":[");
            foreach (var item in items)
            {
                str.Append("{\"thumb_media_id\":\"" + item.thumb_media_id + "\",");
                str.Append("\"author\":\"" + item.author + "\",");
                str.Append("\"title\":\"" + item.title + "\",");
                str.Append("\"content_source_url\":\"" + item.content_source_url + "\",");
                str.Append("\"content\":\"" + item.content + "\",");
                str.Append("\"digest\":\"" + item.digest + "\",");
                str.Append("\"show_cover_pic\":" + (item.show_cover_pic ? "1" : "0") + "},");
            }
            if (str.ToString().EndsWith(","))
                str.Remove(str.Length - 1, 1);
            str.Append("]}");
            return HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/media/uploadnews?access_token={0}", token.access_token), str.ToString()).DeserializeJsonString<MassMessageUpload>();
        }


        /// <summary>
        /// 群发图文消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">图文Id</param>
        /// <param name="tagId">群发到的标签的tag_id，参加用户管理中用户分组接口，可不填写tagId以发送到全部用户</param>
        /// <param name="sendIgnoreReprint">图文消息被判定为转载时，是否继续群发。true为继续群发（转载），false为停止群发。该参数默认为false。</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendNews(this AccessToken token, string mediaId, int tagId = 0, bool sendIgnoreReprint = false) =>
             HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/sendall?access_token={0}", token.access_token),
                CreateJson("mpnews", mediaId, tagId, sendIgnoreReprint)
                ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 群发图文消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">图文Id</param>
        /// <param name="sendIgnoreReprint">图文消息被判定为转载时，是否继续群发。true为继续群发（转载），false为停止群发。该参数默认为false。</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendNewsByOpenId(this AccessToken token, string mediaId, bool sendIgnoreReprint = false, params string[] openid) =>
            token.MassMessageSendNewsByOpenId(mediaId, sendIgnoreReprint, openid.ToList());
        /// <summary>
        /// 群发图文消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">图文Id</param>
        /// <param name="sendIgnoreReprint">图文消息被判定为转载时，是否继续群发。true为继续群发（转载），false为停止群发。该参数默认为false。</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendNewsByOpenId(this AccessToken token, string mediaId, bool sendIgnoreReprint = false, List<string> openid = null) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/send?access_token={0}", token.access_token),
                CreateJson("mpnews", mediaId, 0, sendIgnoreReprint, openid)
                ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 图文消息预览
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">图文Id</param>
        /// <param name="openid">预览账号的OpenId</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendNewsPreview(this AccessToken token, string mediaId, string openid) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/preview?access_token={0}", token.access_token),
                CreateJson("mpnews", mediaId, 0, false, null, openid)
                ).DeserializeJsonString<MassMessageSend>();


        /// <summary>
        /// 群发文本消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="text">文本信息</param>
        /// <param name="tagId">群发到的标签的tag_id，参加用户管理中用户分组接口，可不填写tagId以发送到全部用户</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendText(this AccessToken token, string text, int tagId = 0) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/sendall?access_token={0}", token.access_token),
                CreateJson("text", text, tagId)
                ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 群发文本消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="text">文本信息</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendTextByOpenId(this AccessToken token, string text, params string[] openid) => token.MassMessageSendTextByOpenId(text, openid.ToList());
        /// <summary>
        /// 群发文本消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="text">文本信息</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendTextByOpenId(this AccessToken token, string text, List<string> openid) =>
             HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/send?access_token={0}", token.access_token),
                CreateJson("text", text, 0, false, openid)
                ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 文本消息预览
        /// </summary>
        /// <param name="token"></param>
        /// <param name="text">文本信息</param>
        /// <param name="openid">预览账号的OpenId</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendTextPreview(this AccessToken token, string text, string openid) =>
             HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/preview?access_token={0}", token.access_token),
                CreateJson("text", text, 0, false, null, openid)
                ).DeserializeJsonString<MassMessageSend>();


        /// <summary>
        /// 群发语音消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="tagId">群发到的标签的tag_id，参加用户管理中用户分组接口，可不填写tagId以发送到全部用户</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendVoice(this AccessToken token, string mediaId, int tagId = 0) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/sendall?access_token={0}", token.access_token),
                CreateJson("voice", mediaId, tagId)
                ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 群发语音消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendVoiceByOpenId(this AccessToken token, string mediaId, params string[] openid) => token.MassMessageSendTextByOpenId(mediaId, openid.ToList());
        /// <summary>
        /// 群发语音消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendVoiceByOpenId(this AccessToken token, string mediaId, List<string> openid) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/send?access_token={0}", token.access_token),
                CreateJson("voice", mediaId, 0, false, openid)
                ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 语音消息预览
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="openid">预览账号的OpenId</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendVoicePreview(this AccessToken token, string mediaId, string openid) =>
             HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/preview?access_token={0}", token.access_token),
                CreateJson("voice", mediaId, 0, false, null, openid)
                ).DeserializeJsonString<MassMessageSend>();


        /// <summary>
        /// 群发图片消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="tagId">群发到的标签的tag_id，参加用户管理中用户分组接口，可不填写tagId以发送到全部用户</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendImage(this AccessToken token, string mediaId, int tagId = 0) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/sendall?access_token={0}", token.access_token),
                CreateJson("image", mediaId, tagId)
                ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 群发图片消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendImageByOpenId(this AccessToken token, string mediaId, params string[] openid) => token.MassMessageSendTextByOpenId(mediaId, openid.ToList());
        /// <summary>
        /// 群发图片消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendImageByOpenId(this AccessToken token, string mediaId, List<string> openid) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/send?access_token={0}", token.access_token),
                CreateJson("image", mediaId, 0, false, openid)
                ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 图片消息预览
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="openid">预览账号的OpenId</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendImagePreview(this AccessToken token, string mediaId, string openid) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/preview?access_token={0}", token.access_token),
                CreateJson("image", mediaId, 0, false, null, openid)
                ).DeserializeJsonString<MassMessageSend>();


        /// <summary>
        /// 获取群发的视频MediaId
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">原视频的MediaId</param>
        /// <param name="title">群发视频标题</param>
        /// <param name="description">群发视频描述</param>
        /// <returns></returns>
        public static MassMessageUpload MassMessageGetVideoMediaId(this AccessToken token, string mediaId, string title, string description) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/media/uploadvideo?access_token={0}", token.access_token),
                ("{\"media_id\":\"" + mediaId + "\",\"title\":\"" + title + "\",\"description\":\"" + description + "\"}"))
                .DeserializeJsonString<MassMessageUpload>();
        /// <summary>
        /// 群发视频消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">从方法AccessToken.MassMessageGetVideoMediaId获取到的MediaId</param>
        /// <param name="tagId">群发到的标签的tag_id，参加用户管理中用户分组接口，可不填写tagId以发送到全部用户</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendVideo(this AccessToken token, string mediaId, int tagId = 0) =>
             HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/sendall?access_token={0}", token.access_token),
               CreateJson("mpvideo", mediaId, tagId)
               ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 群发视频消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">原视频的MediaId</param>
        /// <param name="title">群发视频标题</param>
        /// <param name="description">群发视频描述</param>
        /// <param name="tagId">群发到的标签的tag_id，参加用户管理中用户分组接口，可不填写tagId以发送到全部用户</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendVideo(this AccessToken token, string mediaId, string title, string description, int tagId = 0) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/sendall?access_token={0}", token.access_token),
               CreateJson("mpvideo", token.MassMessageGetVideoMediaId(mediaId, title, description).media_id, tagId)
               ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 群发视频消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">从方法AccessToken.MassMessageGetVideoMediaId获取到的MediaId</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendVideoByOpenId(this AccessToken token, string mediaId, params string[] openid) => token.MassMessageSendTextByOpenId(mediaId, openid.ToList());
        /// <summary>
        /// 群发视频消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">从方法AccessToken.MassMessageGetVideoMediaId获取到的MediaId</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendVideoByOpenId(this AccessToken token, string mediaId, List<string> openid) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/send?access_token={0}", token.access_token),
                CreateJson("mpvideo", mediaId, 0, false, openid)
                ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 群发视频消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">原视频的MediaId</param>
        /// <param name="title">群发视频标题</param>
        /// <param name="description">群发视频描述</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendVideoByOpenId(this AccessToken token, string mediaId, string title, string description, params string[] openid) => token.MassMessageSendTextByOpenId(mediaId, openid.ToList());
        /// <summary>
        /// 群发视频消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">原视频的MediaId</param>
        /// <param name="title">群发视频标题</param>
        /// <param name="description">群发视频描述</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendVideoByOpenId(this AccessToken token, string mediaId, string title, string description, List<string> openid) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/send?access_token={0}", token.access_token),
                CreateJson("mpvideo", token.MassMessageGetVideoMediaId(mediaId, title, description).media_id, 0, false, openid)
                ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 视频消息预览
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">从方法AccessToken.MassMessageGetVideoMediaId获取到的MediaId</param>
        /// <param name="openid">预览账号的OpenId</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendVideoPreview(this AccessToken token, string mediaId, string openid) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/preview?access_token={0}", token.access_token),
                CreateJson("mpvideo", mediaId, 0, false, null, openid)
                ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 视频消息预览
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">原视频的MediaId</param>
        /// <param name="title">群发视频标题</param>
        /// <param name="description">群发视频描述</param>
        /// <param name="openid">预览账号的OpenId</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendVideoPreview(this AccessToken token, string mediaId, string title, string description, string openid) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/preview?access_token={0}", token.access_token),
                CreateJson("mpvideo", token.MassMessageGetVideoMediaId(mediaId, title, description).media_id, 0, false, null, openid)
                ).DeserializeJsonString<MassMessageSend>();


        /// <summary>
        /// 群发卡券消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cardId">需要通过卡券接口获得</param>
        /// <param name="tagId">群发到的标签的tag_id，参加用户管理中用户分组接口，可不填写tagId以发送到全部用户</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendCard(this AccessToken token, string cardId, int tagId = 0) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/sendall?access_token={0}", token.access_token),
               CreateJson("wxcard", cardId, tagId)
               ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 群发卡券消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cardId">需要通过卡券接口获得</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendCardByOpenId(this AccessToken token, string cardId, params string[] openid) => token.MassMessageSendTextByOpenId(cardId, openid.ToList());
        /// <summary>
        /// 群发卡券消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cardId">需要通过卡券接口获得</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendCardByOpenId(this AccessToken token, string cardId, List<string> openid) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/send?access_token={0}", token.access_token),
                CreateJson("wxcard", cardId, 0, false, openid)
                ).DeserializeJsonString<MassMessageSend>();
        /// <summary>
        /// 卡券消息预览
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cardId">需要通过卡券接口获得</param>
        /// <param name="openid">预览账号的OpenId</param>
        /// <returns></returns>
        public static MassMessageSend MassMessageSendCardPreview(this AccessToken token, string cardId, string openid) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/preview?access_token={0}", token.access_token),
                CreateJson("wxcard", cardId, 0, false, null, openid)
                ).DeserializeJsonString<MassMessageSend>();


        /// <summary>
        /// 删除群发
        /// </summary>
        /// <param name="token"></param>
        /// <param name="msgId">发送出去的消息ID</param>
        /// <returns></returns>
        public static JsonResult MassMessageSentDelete(this AccessToken token, Int64 msgId) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/mass/delete?access_token={0}", token.access_token),
               ("{\"msg_id\":" + msgId.ToString() + "}")
                ).DeserializeJsonString<MassMessageSend>();


        /// <summary>
        /// 查询群发消息发送状态
        /// </summary>
        /// <param name="token"></param>
        /// <param name="msgId">群发消息后返回的消息id</param>
        /// <returns></returns>
        public static MassMessageState MassMessageSentStateQuery(this AccessToken token, Int64 msgId) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("", token.access_token),
                ("{\"msg_id\":\"" + msgId.ToString() + "\"}")
                ).DeserializeJsonString<MassMessageState>();
    }
}
