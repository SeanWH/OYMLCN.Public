using Newtonsoft.Json.Linq;
using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public partial class CustomerService
        {

            static string CompleteKefuName(string name, Config cfg) =>
                name.Contains("@") ? name : string.Format("{0}@{1}", name, cfg.AccountName);

            public partial class MessageSend
            {
                private static JsonResult Send(AccessToken token, string json) =>
                    ApiPost<JsonResult>(json, "/cgi-bin/message/custom/send?access_token={0}", token.access_token);

                public static string SendAs(AccessToken token, string json, string kfName, Config cfg) =>
                    kfName.IsNullOrEmpty() ?
                        json :
                        (json.Substring(0, json.Length - 1) + ",\"customservice\":{\"kf_account\":\"" + CompleteKefuName(kfName, cfg) + "\"}}");

                public static string TextJson(string openid, string text) =>
                    "{\"touser\":\"" + openid + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + text + "\"}}";
                public static JsonResult Text(AccessToken token, string openid, string text, string kfName = null, Config cfg = null) =>
                    Send(token, SendAs(token, TextJson(openid, text), kfName, cfg));

                public static string ImageJson(string openid, string mediaId) =>
                    "{\"touser\":\"" + openid + "\",\"msgtype\":\"image\",\"image\":{\"media_id\":\"" + mediaId + "\"}}";
                public static JsonResult Image(AccessToken token, string openid, string mediaId, string kfName = null, Config cfg = null) =>
                    Send(token, SendAs(token, ImageJson(openid, mediaId), kfName, cfg));


                public static string VoiceJson(string openid, string mediaId) =>
                    "{\"touser\":\"" + openid + "\",\"msgtype\":\"voice\",\"voice\":{\"media_id\":\"" + mediaId + "\"}}";
                public static JsonResult Voice(AccessToken token, string openid, string mediaId, string kfName = null, Config cfg = null) =>
                   Send(token, SendAs(token, VoiceJson(openid, mediaId), kfName, cfg));


                public static string VideoJson(string openid, string mediaId, string thumbMediaId, string title = null, string description = null) =>
                    "{\"touser\":\"" + openid + "\",\"msgtype\":\"video\",\"video\":{\"media_id\":\"" + mediaId + "\"," +
                        "\"thumb_media_id\":\"" + thumbMediaId + "\",\"title\":\"" + title + "\",\"description\":\"" + description + "\"}}";
                public static JsonResult Video(AccessToken token, string openid, string mediaId, string thumbMediaId, string title = null, string description = null, string kfName = null, Config cfg = null) =>
                    Send(token, SendAs(token, VideoJson(openid, mediaId, thumbMediaId, title, description), kfName, cfg));


                public static string MusicJson(string openid, string thumbMediaId, string musicUrl, string hqMusicUrl, string title = null, string description = null) =>
                     "{\"touser\":\"" + openid + "\",\"msgtype\":\"music\",\"music\":{\"title\":\"" + title + "\",\"description\":\"" + description +
                        "\",\"musicurl\":\"" + musicUrl + "\",\"hqmusicurl\":\"" + hqMusicUrl + "\",\"thumb_media_id\":\"" + thumbMediaId + "\"}}";
                public static JsonResult Music(AccessToken token, string openid, string thumbMediaId, string musicUrl, string hqMusicUrl, string title = null, string description = null, string kfName = null, Config cfg = null) =>
                    Send(token, SendAs(token, MusicJson(openid, thumbMediaId, musicUrl, hqMusicUrl, title, description), kfName, cfg));



                public class Article
                {
                    public Article(string title, string description, string picUrl, string url)
                    {
                        Title = title;
                        Description = description;
                        PicUrl = picUrl;
                        Url = url;
                    }
                    public string Title { get; set; }
                    public string Description { get; set; }
                    public string PicUrl { get; set; }
                    public string Url { get; set; }
                }

                public static JsonResult News(AccessToken token, string openid, string kfName = null, Config cfg = null, params Article[] news) =>
                    News(token, openid, news.ToList(), kfName, cfg);
                public static string NewsJson(string openid, List<Article> news)
                {
                    StringBuilder str = new StringBuilder();
                    var list = news.Take(10);
                    foreach (var item in list)
                        str.Append("{\"title\":\"" + item.Title + "\",\"description\":\"" + item.Description +
                            "\",\"url\":\"" + item.Url + "\",\"picurl\":\"" + item.PicUrl + "\"},");
                    return "{\"touser\":\"" + openid + "\",\"msgtype\":\"news\",\"news\":{\"articles\":[" + str.ToString().Remove(str.Length - 1) + "]}}";
                }
                public static JsonResult News(AccessToken token, string openid, List<Article> news, string kfName = null, Config cfg = null) =>
                    Send(token, SendAs(token, NewsJson(openid, news), kfName, cfg));


                public static string MpNewsJson(string openid, string mediaId) =>
                    "{\"touser\":\"" + openid + "\",\"msgtype\":\"mpnews\",\"mpnews\":{\"media_id\":\"" + mediaId + "\"}}";
                public static JsonResult MpNews(AccessToken token, string openid, string mediaId, string kfName = null, Config cfg = null) =>
                    Send(token, SendAs(token, MpNewsJson(openid, mediaId), kfName, cfg));


                public static string CardJson(string openid, string cardId) =>
                    "{\"touser\":\"" + openid + "\",\"msgtype\":\"wxcard\",\"wxcard\":{\"card_id\":\"" + cardId + "\"}}";
                public static JsonResult Card(AccessToken token, string openid, string cardId, string kfName = null, Config cfg = null) =>
                    Send(token, SendAs(token, CardJson(openid, cardId), kfName, cfg));


            }

        }
    }
}
