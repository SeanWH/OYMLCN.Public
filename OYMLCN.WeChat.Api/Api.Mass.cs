using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
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
            protected class JsonCreate
            {
                public static string UploadNews(List<Article> items)
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
                    return str.ToString();
                }
                private static string TypeConent(string type, string typeKey, string content) =>
                    "\"" + type + "\":{\"" + typeKey + "\":\"" + content + "\"},\"msgtype\":\"" + type + "\"";
                private static string Filter(int tag_id = 0) =>
                    "\"filter\":{\"is_to_all\":" + (tag_id == 0 ? "true" : "false") + ",\"tag_id\":" + tag_id.ToString() + "},";
                private static string ToUser(List<string> openid)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("\"touser\":[");
                    foreach (var item in openid)
                        str.Append("\"" + item + "\",");
                    if (str.ToString().EndsWith(","))
                        str.Remove(str.Length - 1, 1);
                    str.Append("],");
                    return str.ToString();
                }
                private static string ToUser(string openid) => $"\"touser\":\"{openid}\",";
                private static string ToWxName(string openid) => $"\"towxname\":\"{openid}\",";

                private static string SendAll(string type, string typeKey, string content, int tag_id = 0) =>
                    "{" + Filter(tag_id) + TypeConent(type, typeKey, content) + "}";
                private static string SendOpenId(string type, string typeKey, string content, List<string> openid) =>
                    "{" + ToUser(openid) + TypeConent(type, typeKey, content) + "}";
                private static string Preview(string type, string typeKey, string content, string openid = null, string wxname = null) =>
                    "{" + (openid.IsNullOrEmpty() ? ToWxName(wxname) : ToUser(openid)) + TypeConent(type, typeKey, content) + "}";



                public static string SendMpNews(string media_id, int tag_id = 0, bool send_ignore_reprint = false) =>
                    "{" + Filter(tag_id) + TypeConent("mpnews", "media_id", media_id) + ",\"send_ignore_reprint\":" + (send_ignore_reprint ? "1" : "0") + "}";
                public static string SendMpNews(string media_id, List<string> openid, bool send_ignore_reprint = false) =>
                    "{" + ToUser(openid) + TypeConent("mpnews", "media_id", media_id) + ",\"send_ignore_reprint\":" + (send_ignore_reprint ? "1" : "0") + "}";
                public static string PreviewMpNews(string media_id, string openid = null, string wxname = null) =>
                    Preview("mpnews", "media_id", media_id, openid, wxname);

                public static string SendText(string content, int tag_id = 0) =>
                    SendAll("text", "content", content, tag_id);
                public static string SendText(string content, List<string> openid) =>
                    SendOpenId("text", "content", content, openid);
                public static string PreviewText(string content, string openid = null, string wxname = null) =>
                    Preview("text", "content", content, openid, wxname);

                public static string SendVoice(string media_id, int tag_id = 0) =>
                    SendAll("voice", "media_id", media_id, tag_id);
                public static string SendVoice(string media_id, List<string> openid) =>
                    SendOpenId("voice", "media_id", media_id, openid);
                public static string PreviewVoice(string media_id, string openid = null, string wxname = null) =>
                    Preview("voice", "media_id", media_id, openid, wxname);

                public static string SendImage(string media_id, int tag_id = 0) =>
                    SendAll("image", "media_id", media_id, tag_id);
                public static string SendImage(string media_id, List<string> openid) =>
                    SendOpenId("image", "media_id", media_id, openid);
                public static string PreviewImage(string media_id, string openid = null, string wxname = null) =>
                    Preview("image", "media_id", media_id, openid, wxname);

                public static string SendVideoPreGetMediaId(string media_id, string title, string description) =>
                    "{\"media_id\":\"" + media_id + "\",\"title\":\"" + title + "\",\"description\":\"" + description + "\"}";
                public static string SendMpVideo(string media_id, int tag_id = 0) =>
                    SendAll("mpvideo", "media_id", media_id, tag_id);
                public static string SendMpVideo(string media_id, string title, string description, List<string> openid) =>
                    "{" + ToUser(openid) + "\"mpvideo\":{\"media_id\":\"" + media_id + "\",\"title\":\"" + title + "\",\"description\":\"" + description + "\"},\"msgtype\":\"mpvideo\"" + "}";
                public static string PreviewMpVideo(string media_id, string openid = null, string wxname = null) =>
                    Preview("mpvideo", "media_id", media_id, openid, wxname);

                public static string SendCard(string card_id, int tag_id = 0) =>
                    SendAll("wxcard", "card_id", card_id, tag_id);
                public static string SendCard(string card_id, List<string> openid) =>
                    SendOpenId("wxcard", "card_id", card_id, openid);
                public static string PreviewCard(string card_id, string code, string timestamp, string signature, string openid, string wxname = null) =>
                    "{" + (openid.IsNullOrEmpty() ? ToWxName(wxname) : ToUser(openid)) +
                    "\"wxcard\":{\"card_id\":\"" + card_id + "\",\"card_ext\":\"" +
                    "{\\\"code\\\":\\\"" + code + "\\\",\\\"openid\\\":\\\"" + openid + "\\\",\\\"timestamp\\\":\\\"" + timestamp + "\\\",\\\"signature\\\":\\\"" + signature + "\\\"}\"" +
                    "},\"msgtype\":\"wxcard\"" + "}";

                public static string SentDelete(long msg_id, byte? article_idx = null)
                {
                    string json = "{\"msg_id\":" + msg_id.ToString();
                    if (article_idx != null)
                        json += ",\"article_idx\":2";
                    json += "}";
                    return json;
                }
                public static string MassMessageSentStateQuery(long msg_id) => "{\"msg_id\":\"" + msg_id.ToString() + "\"}";
            }

            //public static string CreateJson(string type, string data, int tag_id = 0, bool sendIgnoreReprint = false, List<string> openid = null, string previewOpenId = null, string previewWeixinName = null)
            //{
            //    StringBuilder str = new StringBuilder();
            //    str.Append("{");
            //    if (!previewOpenId.IsNullOrEmpty())
            //        str.Append("\"touser\":\"" + previewOpenId + "\",");
            //    else if (!previewWeixinName.IsNullOrEmpty())
            //        str.Append("\"towxname\":\"" + previewWeixinName + "\",");
            //    else if (openid == null)
            //    {
            //        str.Append("\"filter\":{");
            //        str.Append("\"is_to_all\":" + (tag_id == 0 ? "true" : "false") + "");
            //        if (tag_id != 0)
            //            str.Append(",\"tag_id\":" + tag_id.ToString());
            //        str.Append("},");
            //    }
            //    else
            //    {
            //        str.Append("\"touser\":[");
            //        foreach (var item in openid)
            //            str.Append("\"" + item + "\",");
            //        if (str.ToString().EndsWith(","))
            //            str.Remove(str.Length - 1, 1);
            //        str.Append("],");
            //    }


            //    type = type == "video" ? "mpvideo" : type;

            //    str.Append("\"" + type + "\":{");
            //    if (type == "text")
            //        str.Append("\"content\":\"" + data + "\"");
            //    else if (type == "wxcard")
            //        str.Append("\"card_id\":\"" + data + "\"");
            //    else
            //        str.Append("\"media_id\":\"" + data + "\"");

            //    if (type == "mpnews" && string.IsNullOrWhiteSpace(previewOpenId))
            //        str.Append("},\"msgtype\":\"mpnews\",\"send_ignore_reprint\":" + (sendIgnoreReprint ? "1" : "0"));
            //    else
            //        str.Append("},\"msgtype\":\"" + type + "\"");

            //    str.Append("}");
            //    return str.ToString();
            //}
            private static MassResult SendAll(string access_token, string json) =>
            ApiPost<MassResult>(json, "/cgi-bin/message/mass/sendall?access_token={0}", access_token);
            private static MassResult SendOpenId(string access_token, string json) =>
                ApiPost<MassResult>(json, "/cgi-bin/message/mass/send?access_token={0}", access_token);
            private static MassResult SendPreview(string access_token, string json) =>
                ApiPost<MassResult>(json, "/cgi-bin/message/mass/preview?access_token={0}", access_token);


            [Obsolete("建议调用Api.Material.UploadImage")]
            public static string UploadImage(string access_token, string filePath) => Api.Material.UploadImage(access_token, filePath);

            public static MediaUpload UploadNews(string access_token, List<Article> items) =>
                ApiPost<MediaUpload>(JsonCreate.UploadNews(items), "/cgi-bin/media/uploadnews?access_token={0}", access_token);

            public static MassResult SendMpNews(string access_token, string media_id, int tag_id = 0, bool send_ignore_reprint = false) =>
                SendAll(access_token, JsonCreate.SendMpNews(media_id, tag_id, send_ignore_reprint));
            public static MassResult SendMpNews(string access_token, string media_id, List<string> openid, bool send_ignore_reprint = false) =>
                SendOpenId(access_token, JsonCreate.SendMpNews(media_id, openid, send_ignore_reprint));
            public static MassResult PreviewMpNews(string access_token, string media_id, string openid = null, string wxname = null) =>
                SendPreview(access_token, JsonCreate.PreviewMpNews(media_id, openid, wxname));

            public static MassResult SendText(string access_token, string text, int tag_id = 0) =>
                SendAll(access_token, JsonCreate.SendText(text, tag_id));
            public static MassResult SendText(string access_token, string text, List<string> openid) =>
                SendOpenId(access_token, JsonCreate.SendText(text, openid));
            public static MassResult PreviewText(string access_token, string text, string openid = null, string wxname = null) =>
                SendPreview(access_token, JsonCreate.PreviewText(text, openid, wxname));

            public static MassResult SendVoice(string access_token, string media_id, int tag_id = 0) =>
                SendAll(access_token, JsonCreate.SendVoice(media_id, tag_id));
            public static MassResult SendVoice(string access_token, string media_id, List<string> openid) =>
                SendOpenId(access_token, JsonCreate.SendVoice(media_id, openid));
            public static MassResult PreviewVoice(string access_token, string media_id, string openid = null, string wxname = null) =>
                SendPreview(access_token, JsonCreate.PreviewVoice(media_id, openid, wxname));

            public static MassResult SendImage(string access_token, string media_id, int tag_id = 0) =>
                SendAll(access_token, JsonCreate.SendImage(media_id, tag_id));
            public static MassResult SendImage(string access_token, string media_id, List<string> openid) =>
                SendOpenId(access_token, JsonCreate.SendImage(media_id, openid));
            public static MassResult PreviewImage(string access_token, string media_id, string openid = null, string wxname = null) =>
                SendPreview(access_token, JsonCreate.PreviewImage(media_id, openid, wxname));

            public static MediaUpload SendVideoPreGetMediaId(string access_token, string mediaId, string title, string description) =>
                ApiPost<MediaUpload>(JsonCreate.SendVideoPreGetMediaId(mediaId, title, description), "/cgi-bin/media/uploadvideo?access_token={0}", access_token);
            public static MassResult SendVideo(string access_token, string media_id, int tag_id = 0) =>
                SendAll(access_token, JsonCreate.SendMpVideo(media_id, tag_id));
            public static MassResult SendVideo(string access_token, string media_id, string title, string description, List<string> openid) =>
                SendOpenId(access_token, JsonCreate.SendMpVideo(media_id, title, description, openid));
            public static MassResult PreviewVideo(string access_token, string media_id, string openid = null, string wxname = null) =>
                SendPreview(access_token, JsonCreate.PreviewMpVideo(media_id, openid, wxname));

            public static MassResult SendCard(string access_token, string card_id, int tag_id = 0) =>
                SendAll(access_token, JsonCreate.SendCard(card_id, tag_id));
            public static MassResult SendCard(string access_token, string card_id, List<string> openid) =>
                SendOpenId(access_token, JsonCreate.SendCard(card_id, openid));
            public static MassResult PreviewCard(string access_token, string card_id, string code, string timestamp, string signature, string openid, string wxname = null) =>
                SendPreview(access_token, JsonCreate.PreviewCard(card_id, code, timestamp, signature, openid, wxname));


            public static JsonResult SentDelete(string access_token, long msg_id, byte? article_idx = null) =>
                ApiPost<MassResult>(JsonCreate.SentDelete(msg_id, article_idx), "/cgi-bin/message/mass/delete?access_token={0}", access_token);
            public static MassState MassMessageSentStateQuery(string access_token, long msg_id) =>
                ApiPost<MassState>(JsonCreate.MassMessageSentStateQuery(msg_id), "/cgi-bin/message/mass/get?access_token={0}", access_token);
        }
    }
}
