using OYMLCN.WeChat.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public partial class CustomerService
        {
            public partial class MessageSend
            {
                protected class JsonCreate
                {
                    private static string SendAs(string json, string kf_account) =>
                        kf_account.IsNullOrEmpty() ?
                            json :
                        (json.Substring(0, json.Length - 1) + ",\"customservice\":{\"kf_account\":\"" + kf_account + "\"}}");

                    public static string Text(string openid, string text, string kf_account = null) =>
                        SendAs("{\"touser\":\"" + openid + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + text + "\"}}", kf_account);
                    public static string Media(string openid, string type, string media_id, string kf_account = null) =>
                        SendAs("{\"touser\":\"" + openid + "\",\"msgtype\":\"" + type + "\",\"" + type + "\":{\"media_id\":\"" + media_id + "\"}}", kf_account);
                    public static string Video(string openid, string media_id, string thumbMediaId, string title = null, string description = null, string kf_account = null) =>
                        SendAs("{\"touser\":\"" + openid + "\",\"msgtype\":\"video\",\"video\":{\"media_id\":\"" + media_id + "\"," +
                               "\"thumb_media_id\":\"" + thumbMediaId + "\",\"title\":\"" + title + "\",\"description\":\"" + description + "\"}}"
                            , kf_account);
                    public static string Music(string openid, string thumbMediaId, string musicUrl, string hqMusicUrl, string title = null, string description = null, string kf_account = null) =>
                        SendAs("{\"touser\":\"" + openid + "\",\"msgtype\":\"music\",\"music\":{\"title\":\"" + title + "\",\"description\":\"" + description + "\"," +
                                "\"musicurl\":\"" + musicUrl + "\",\"hqmusicurl\":\"" + hqMusicUrl + "\",\"thumb_media_id\":\"" + thumbMediaId + "\"}}"
                            , kf_account);
                    public static string News(string openid, List<Article> news, string kf_account = null)
                    {
                        StringBuilder str = new StringBuilder();
                        var list = news.Take(10);
                        foreach (var item in list)
                            str.Append("{\"title\":\"" + item.Title + "\",\"description\":\"" + item.Description +
                                "\",\"url\":\"" + item.Url + "\",\"picurl\":\"" + item.PicUrl + "\"},");
                        return SendAs("{\"touser\":\"" + openid + "\",\"msgtype\":\"news\",\"news\":{\"articles\":[" + str.ToString().Remove(str.Length - 1) + "]}}", kf_account);
                    }
                    public static string Card(string openid, string card_id, string kf_account = null) =>
                        SendAs("{\"touser\":\"" + openid + "\",\"msgtype\":\"wxcard\",\"wxcard\":{\"card_id\":\"" + card_id + "\"}}", kf_account);

                }

                private static JsonResult Send(string access_token, string json) =>
                    ApiPost<JsonResult>(json, "/cgi-bin/message/custom/send?access_token={0}", access_token);


                public static JsonResult Text(string access_token, string openid, string text, string kf_account = null) =>
                    Send(access_token, JsonCreate.Text(openid, text, kf_account));
                public static JsonResult Image(string access_token, string openid, string media_id, string kf_account = null) =>
                    Send(access_token, JsonCreate.Media(openid, "image", media_id, kf_account));
                public static JsonResult Voice(string access_token, string openid, string media_id, string kf_account = null) =>
                   Send(access_token, JsonCreate.Media(openid, "voice", media_id, kf_account));
                public static JsonResult Video(string access_token, string openid, string media_id, string thumbMediaId, string title = null, string description = null, string kf_account = null) =>
                    Send(access_token, JsonCreate.Video(openid, media_id, thumbMediaId, title, description, kf_account));
                public static JsonResult Music(string access_token, string openid, string thumbMediaId, string musicUrl, string hqMusicUrl, string title = null, string description = null, string kf_account = null) =>
                    Send(access_token, JsonCreate.Music(openid, thumbMediaId, musicUrl, hqMusicUrl, title, description, kf_account));
                public static JsonResult News(string access_token, string openid, List<Article> news, string kf_account = null) =>
                    Send(access_token, JsonCreate.News(openid, news, kf_account));
                public static JsonResult MpNews(string access_token, string openid, string media_id, string kf_account = null) =>
                    Send(access_token, JsonCreate.Media(openid, "mpnews", media_id, kf_account));
                public static JsonResult Card(string access_token, string openid, string card_id, string kf_account = null) =>
                    Send(access_token, JsonCreate.Card(openid, card_id, kf_account));


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
            }

        }
    }
}
