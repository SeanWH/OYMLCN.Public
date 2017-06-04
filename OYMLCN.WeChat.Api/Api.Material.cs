using Newtonsoft.Json.Linq;
using OYMLCN.WeChat.Enums;
using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public static class Material
        {

            public static string AddNews(AccessToken token, params Article[] items) => AddNews(token, items.ToList());

            public static string MaterialNewsJson(List<Article> items)
            {
                StringBuilder str = new StringBuilder();
                str.Append("{\"articles\":[");
                foreach (var item in items)
                {
                    str.Append("{");
                    str.AppendFormat("\"title\":\"{0}\",", item.title);
                    str.AppendFormat("\"thumb_media_id\":\"{0}\",", item.thumb_media_id);
                    str.AppendFormat("\"author\":\"{0}\",", item.author);
                    str.AppendFormat("\"digest\":\"{0}\",", item.digest);
                    str.AppendFormat("\"show_cover_pic\":{0},", item.show_cover_pic ? "1" : "0");
                    str.AppendFormat("\"content\":\"{0}\",", item.content);
                    str.AppendFormat("\"content_source_url\":\"{0}\"", item.content_source_url);
                    str.Append("},");
                }
                if (str.ToString().EndsWith(","))
                    str.Remove(str.Length - 1, 1);
                str.Append("]}");
                return str.ToString();
            }
            public static string AddNews(AccessToken token, List<Article> items) =>
                ApiPost<MediaUpload>(MaterialNewsJson(items), "/cgi-bin/material/add_news?access_token={0}", token.access_token).media_id;


            public static string MiniProgramContentXml(string appId, string path, string title, string imageUrl) =>
                "<mp-miniprogram data-miniprogram-appid=\"" + appId + "\" data-miniprogram-path=\"" + path + "\" data-miniprogram-title=\"" + title + "\" data-progarm-imageurl=\"" + imageUrl + "\"></mp-miniprogram>";


            public static JsonResult UpdateNews(AccessToken token, string mediaId, int index, Article item)
            {
                StringBuilder str = new StringBuilder();
                str.Append("{");
                str.AppendFormat("\"media_id\":\"{0}\",", mediaId);
                str.AppendFormat("\"index\":{0},", index.ToString());
                str.Append("\"articles\":{");
                str.AppendFormat("\"title\":\"{0}\",", item.title);
                str.AppendFormat("\"thumb_media_id\":\"{0}\",", item.thumb_media_id);
                str.AppendFormat("\"author\":\"{0}\",", item.author);
                str.AppendFormat("\"digest\":\"{0}\",", item.digest);
                str.AppendFormat("\"show_cover_pic\":{0},", item.show_cover_pic ? "1" : "0");
                str.AppendFormat("\"content\":\"{0}\",", item.content);
                str.AppendFormat("\"content_source_url\":\"{0}\"", item.content_source_url);
                str.Append("}}");
                return ApiPost<JsonResult>(str.ToString(), "/cgi-bin/material/update_news?access_token={0}", token.access_token);
            }

            public static string UploadImage(AccessToken token, string filePath) =>
                ApiPostFile<MediaUpload>(new Dictionary<string, string>() {
                    { "media", filePath}
                }, "/cgi-bin/media/uploadimg?access_token={0}", token.access_token).url;


            public static MediaUpload Add(AccessToken token, MediaType type, string filePath, string title = null, string introduction = null)
            {
                if (type == MediaType.News)
                    throw new NotSupportedException();
                var file = new Dictionary<string, string>();
                file["media"] = filePath;
                if (type == MediaType.Video)
                    file["description"] = string.Format("{{\"title\":\"{0}\",\"introduction\":\"{1}\"}}", title, introduction);
                return ApiPostFile<MediaUpload>(file, "/cgi-bin/material/add_material?access_token={0}", token.access_token);
            }

            public static Stream Get(AccessToken token, MediaType type, string mediaId)
            {
                if (type == MediaType.News)
                    throw new NotSupportedException();
                else if (type == MediaType.Video)
                    throw new NotSupportedException("下载视频请使用MaterialVideoDownloadUrl获取下载地址");
                else
                    return HttpClient.PostData(ApiUrl("/cgi-bin/material/get_material?access_token={0}", token.access_token), "{\"media_id\":\"" + mediaId + "\"}", mediaType: "application/json");
            }
            public static MaterialVideoInfo GetVideoInfo(AccessToken token, string mediaId) =>
                 ApiPost<MaterialVideoInfo>("{\"media_id\":\"" + mediaId + "\"}", "/cgi-bin/material/get_material?access_token={0}", token.access_token);
            public static List<Article> GetNews(AccessToken token, string mediaId)
            {
                var result = new List<Article>();
                var data = ApiJTokenPost("{\"media_id\":\"" + mediaId + "\"}", "/cgi-bin/material/get_material?access_token={0}", token.access_token);
                try
                {
                    foreach (var item in data["news_item"])
                        result.Add(new Article()
                        {
                            title = item["title"]?.ToString(),
                            thumb_media_id = item["thumb_media_id"]?.ToString(),
                            show_cover_pic = item["show_cover_pic"]?.ToString() == "0" ? false : true,
                            author = item["author"]?.ToString(),
                            digest = item["digest"]?.ToString(),
                            content = item["content"]?.ToString(),
                            url = item["url"]?.ToString(),
                            content_source_url = item["content_source_url"]?.ToString()
                        });
                    return result;
                }
                catch
                {
                    throw new Exception(data.ToString().DeserializeJsonString<JsonResult>().ToJsonString());
                }
            }

            public static JsonResult Delete(AccessToken token, string mediaId) =>
                ApiPost<JsonResult>("{\"media_id\":\"" + mediaId + "\"}", "/cgi-bin/material/del_material?access_token={0}", token.access_token);


            public static MaterialCount Count(AccessToken token) =>
                ApiGet<MaterialCount>("/cgi-bin/material/get_materialcount?access_token={0}", token.access_token);
            public static MaterialList<MaterialItem> BatchGet(AccessToken token, MediaType type, int offset, int count) =>
                 ApiPost<MaterialList<MaterialItem>>("{\"type\":\"" + type.ToString().ToLower() + "\",\"offset\":" + offset.ToString() + ",\"count\":" + count.ToString() + "}",
                     "/cgi-bin/material/batchget_material?access_token={0}", token.access_token);
            public static MaterialList<MaterialNews> BatchGetNews(AccessToken token, int offset, int count) =>
                 ApiPost<MaterialList<MaterialNews>>("{\"type\":\"news\",\"offset\":" + offset.ToString() + ",\"count\":" + count.ToString() + "}",
                     "/cgi-bin/material/batchget_material?access_token={0}", token.access_token);

        }
    }
}
