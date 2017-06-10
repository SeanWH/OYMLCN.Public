using OYMLCN.WeChat.Enums;
using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public class Material
        {
            protected class JsonCreate
            {
                public static string AddNews(List<Article> items)
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
                public static string UpdateNews(string media_id, int index, Article item)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("{");
                    str.AppendFormat("\"media_id\":\"{0}\",", media_id);
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
                    return str.ToString();
                }
                public static string Add(string title, string introduction) =>
                    "{\"title\":\"" + title + "\",\"introduction\":\"" + introduction + "\"}";
                public static string GetVideoInfo(string media_id) =>
                    "{\"media_id\":\"" + media_id + "\"}";
                public static string GetNews(string media_id) => GetVideoInfo(media_id);
                public static string Delete(string media_id) => GetVideoInfo(media_id);

                public static string BatchGet(string type, int offset, int count) =>
                    "{\"type\":\"" + type + "\",\"offset\":" + offset.ToString() + ",\"count\":" + count.ToString() + "}";
                public static string BatchGetNews(int offset, int count) => BatchGet("news", offset, count);
            }

            public static string AddNews(string access_token, List<Article> items) =>
                ApiPost<MediaUpload>(JsonCreate.AddNews(items), "/cgi-bin/material/add_news?access_token={0}", access_token).media_id;

            public static string MiniProgramContentXml(string appId, string path, string title, string imageUrl) =>
                "<mp-miniprogram data-miniprogram-appid=\"" + appId + "\" data-miniprogram-path=\"" + path + "\" data-miniprogram-title=\"" + title + "\" data-progarm-imageurl=\"" + imageUrl + "\"></mp-miniprogram>";

            public static JsonResult UpdateNews(string access_token, string media_id, int index, Article item) =>
                ApiPost<JsonResult>(JsonCreate.UpdateNews(media_id, index, item), "/cgi-bin/material/update_news?access_token={0}", access_token);
            public static string UploadImage(string access_token, string filePath) =>
    ApiPostFile<MediaUpload>(new Dictionary<string, string>() {
                    { "media", filePath}
    }, "/cgi-bin/media/uploadimg?access_token={0}", access_token).url;
            public static MediaUpload Add(string access_token, MediaType type, string filePath, string title = null, string introduction = null)
            {
                if (type == MediaType.News)
                    throw new NotSupportedException();
                var file = new Dictionary<string, string>
                {
                    ["media"] = filePath
                };
                if (type == MediaType.Video)
                    file["description"] = JsonCreate.Add(title, introduction);
                return ApiPostFile<MediaUpload>(file, "/cgi-bin/material/add_material?access_token={0}", access_token);
            }

            public static Stream Get(string access_token, MediaType type, string mediaId)
            {
                if (type == MediaType.News)
                    throw new NotSupportedException("图文信息请使用GetNews获取");
                else if (type == MediaType.Video)
                    throw new NotSupportedException("下载视频请使用MaterialVideoDownloadUrl获取下载地址");
                else
                    return HttpClient.PostData(ApiUrl("/cgi-bin/material/get_material?access_token={0}", access_token), "{\"media_id\":\"" + mediaId + "\"}", mediaType: "application/json");
            }
            public static MaterialVideoInfo GetVideoInfo(string access_token, string media_id) =>
                 ApiPost<MaterialVideoInfo>(JsonCreate.GetVideoInfo(media_id), "/cgi-bin/material/get_material?access_token={0}", access_token);
            public static List<Article> GetNews(string access_token, string media_id)
            {
                var result = new List<Article>();
                var data = ApiJTokenPost(JsonCreate.GetNews(media_id), "/cgi-bin/material/get_material?access_token={0}", access_token);
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

            public static JsonResult Delete(string access_token, string media_id) =>
                ApiPost<JsonResult>(JsonCreate.Delete(media_id), "/cgi-bin/material/del_material?access_token={0}", access_token);

            public static MaterialCount Count(string access_token) =>
                ApiGet<MaterialCount>("/cgi-bin/material/get_materialcount?access_token={0}", access_token);
            public static MaterialList<MaterialItem> BatchGet(string access_token, MediaType type, int offset, int count) =>
                 ApiPost<MaterialList<MaterialItem>>(JsonCreate.BatchGet(type.ToString().ToLower(), offset, count), "/cgi-bin/material/batchget_material?access_token={0}", access_token);
            public static MaterialList<MaterialNews> BatchGetNews(string access_token, int offset, int count) =>
                 ApiPost<MaterialList<MaterialNews>>(JsonCreate.BatchGetNews(offset, count), "/cgi-bin/material/batchget_material?access_token={0}", access_token);

        }
    }
}
