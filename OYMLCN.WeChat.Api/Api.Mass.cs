using OYMLCN.WeChat.Enums;
using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        /// <summary>
        /// 未测试
        /// </summary>
        public class Mass
        {
            private static string CreateJson(string type, string data, int tagId = 0, bool sendIgnoreReprint = false, List<string> openid = null, string previewOpenId = null)
            {
                StringBuilder str = new StringBuilder();
                //str.Append("{");
                //if (openid == null)
                //{
                //    str.Append("\"filter\":{");
                //    str.Append("\"is_to_all\":" + (tagId == 0 ? "true" : "false") + "");
                //    if (tagId != 0)
                //        str.Append(",\"tag_id\":" + tagId.ToString());
                //    str.Append("},");
                //}
                //else if (!string.IsNullOrWhiteSpace(previewOpenId))
                //    str.Append("\"towxname\":\"" + previewOpenId + "\",");
                //else
                //{
                //    str.Append("\"touser\":[");
                //    foreach (var item in openid)
                //        str.Append("\"" + item + "\",");
                //    str.Append("],");
                //}

                //str.Append("\"" + type + "\":{");
                //if (type == "text")
                //    str.Append("\"content\":\"" + data + "\"");
                //else if (type == "wxcard")
                //    str.Append("\"card_id\":\"" + data + "\"");
                //else
                //    str.Append("\"media_id\":\"" + data + "\"");

                //if (type == "mpnews" && string.IsNullOrWhiteSpace(previewOpenId))
                //    str.Append("},\"msgtype\":\"mpnews\",\"send_ignore_reprint\":" + (sendIgnoreReprint ? "1" : "0"));
                //else
                //    str.Append("},\"msgtype\":\"" + type + "\"");

                //str.Append("}");
                return str.ToString();
            }
            private static MassResult SendAll(AccessToken token, string json) =>
                ApiPost<MassResult>(json, "/cgi-bin/message/mass/sendall?access_token={0}", token.access_token);
            private static MassResult SendOpenId(AccessToken token, string json) =>
                ApiPost<MassResult>(json, "/cgi-bin/message/mass/send?access_token={0}", token.access_token);
            private static MassResult SendPreview(AccessToken token, string json) =>
                ApiPost<MassResult>(json, "/cgi-bin/message/mass/preview?access_token={0}", token.access_token);


            [Obsolete("建议调用Api.Material.UploadImage")]
            public static string UploadImage(AccessToken token, string filePath) => Api.Material.UploadImage(token, filePath);

            public static MediaUpload UploadNews(AccessToken token, params Article[] items) => UploadNews(token, items.ToList());
            public static MediaUpload UploadNews(AccessToken token, List<Article> items)
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
                return ApiPost<MediaUpload>(str.ToString(), "/cgi-bin/media/uploadnews?access_token={0}", token.access_token);
            }


            public static MassResult SendMpNews(AccessToken token, string mediaId, int tagId = 0, bool sendIgnoreReprint = false) =>
                SendAll(token, CreateJson("mpnews", mediaId, tagId, sendIgnoreReprint));
            public static MassResult SendMpNews(AccessToken token, string mediaId, bool sendIgnoreReprint = false, params string[] openid) =>
                SendMpNews(token, mediaId, sendIgnoreReprint, openid.ToList());
            public static MassResult SendMpNews(AccessToken token, string mediaId, bool sendIgnoreReprint = false, List<string> openid = null) =>
                SendOpenId(token, CreateJson("mpnews", mediaId, 0, sendIgnoreReprint, openid));
            public static MassResult PreviewMpNews(AccessToken token, string mediaId, string openid) =>
                SendPreview(token, CreateJson("mpnews", mediaId, 0, false, null, openid));


            public static MassResult SendText(AccessToken token, string text, int tagId = 0) =>
                SendAll(token, CreateJson("text", text, tagId));
            public static MassResult SendText(AccessToken token, string text, params string[] openid) => SendText(token, text, openid.ToList());
            public static MassResult SendText(AccessToken token, string text, List<string> openid) =>
                SendOpenId(token, CreateJson("text", text, 0, false, openid));
            public static MassResult PreviewText(AccessToken token, string text, string openid) =>
                SendPreview(token, CreateJson("text", text, 0, false, null, openid));


            public static MediaUpload SendVideoPreGetMediaId(AccessToken token, string mediaId, string title, string description) =>
                ApiPost<MediaUpload>(("{\"media_id\":\"" + mediaId + "\",\"title\":\"" + title + "\",\"description\":\"" + description + "\"}"), "/cgi-bin/media/uploadvideo?access_token={0}", token.access_token);

            public static MassResult SendMedia(AccessToken token, MediaType type, string mediaId, int tagId = 0)
            {
                if (type == MediaType.Image || type == MediaType.Video || type == MediaType.Voice)
                    return SendAll(token, CreateJson(type.ToString().ToLower(), mediaId, tagId));
                throw new NotSupportedException("非媒体内容请使用对应的方法发送");
            }
            public static MassResult SendMedia(AccessToken token, MediaType type, string mediaId, params string[] openid) => SendMedia(token, type, mediaId, openid.ToList());
            public static MassResult SendMedia(AccessToken token, MediaType type, string mediaId, List<string> openid)
            {
                if (type == MediaType.Image || type == MediaType.Video || type == MediaType.Voice)
                    ApiPost<MassResult>(CreateJson("voice", mediaId, 0, false, openid), "/cgi-bin/message/mass/send?access_token={0}", token.access_token);
                throw new NotSupportedException("非媒体内容请使用对应的方法发送");
            }
            public static MassResult PreviewMedia(AccessToken token, MediaType type, string mediaId, string openid)
            {
                if (type == MediaType.Image || type == MediaType.Video || type == MediaType.Voice)
                    ApiPost<MassResult>(CreateJson("voice", mediaId, 0, false, null, openid), "/cgi-bin/message/mass/preview?access_token={0}", token.access_token);
                throw new NotSupportedException("非媒体内容请使用对应的方法发送");
            }


            public static MassResult SendCard(AccessToken token, string cardId, int tagId = 0) =>
                SendAll(token, CreateJson("wxcard", cardId, tagId));
            public static MassResult SendCard(AccessToken token, string cardId, params string[] openid) => SendCard(token, cardId, openid.ToList());
            public static MassResult SendCard(AccessToken token, string cardId, List<string> openid) =>
                SendOpenId(token, CreateJson("wxcard", cardId, 0, false, openid));
            public static MassResult PreviewCard(AccessToken token, string cardId, string openid) =>
                SendPreview(token, CreateJson("wxcard", cardId, 0, false, null, openid));


            public static JsonResult SentDelete(AccessToken token, Int64 msgId) =>
                ApiPost<MassResult>("{\"msg_id\":" + msgId.ToString() + "}", "/cgi-bin/message/mass/delete?access_token={0}", token.access_token);
            public static MassState MassMessageSentStateQuery(AccessToken token, Int64 msgId) =>
                ApiPost<MassState>("{\"msg_id\":\"" + msgId.ToString() + "\"}", "/cgi-bin/message/mass/get?access_token={0}", token.access_token);
        }
    }
}
