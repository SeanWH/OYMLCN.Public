using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Text;
using OYMLCN;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 素材接口通讯及逻辑辅助
    /// </summary>
    public static class MediaApi
    {
        /// <summary>
        /// 获取素材类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static MediaType GetMediaType(this string str)
        {
            switch (str.ToLower())
            {
                case "image":
                    return MediaType.Image;
                case "news":
                    return MediaType.News;
                case "voice":
                    return MediaType.Voice;
                case "video":
                    return MediaType.Video;
                case "thumb":
                    return MediaType.Thumb;
                default:
                    throw new NotSupportedException();
            }
        }

        private static MediaNewItem FillNewItem(this JToken item)
        {
            var result = new MediaNewItem()
            {
                title = item["title"]?.ToString(),
                thumb_media_id = item["thumb_media_id"]?.ToString(),
                show_cover_pic = item["show_cover_pic"]?.ToString() == "0" ? false : true,
                author = item["author"]?.ToString(),
                digest = item["digest"]?.ToString(),
                content = item["content"]?.ToString(),
                url = item["url"]?.ToString(),
                content_source_url = item["content_source_url"]?.ToString()
            };
            return result;
        }

        /// <summary>
        /// 上传临时素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type">素材类型</param>
        /// <param name="filePath">素材文件路径</param>
        /// <returns></returns>
        public static Media MediaUpload(this AccessToken token, MediaType type, string filePath) =>
            HttpClient.CurlPost(CoreApi.ApiUrl("/cgi-bin/media/upload?access_token={0}&type={1}", token.access_token, type.ToString().ToLower()), queryDir: new Dictionary<string, string>() {
                { "media", filePath}
            }).ReadToEnd().DeserializeJsonString<Media>();
        /// <summary>
        /// 获取临时素材下载地址
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type">素材类型</param>
        /// <param name="mediaId">素材的media_id</param>
        /// <exception cref="NotSupportedException">图文信息类型不被支持</exception>
        public static string MediaDownloadUrl(this AccessToken token, MediaType type, string mediaId)
        {
            string url = CoreApi.ApiUrl("/cgi-bin/media/get?access_token={0}&media_id={1}", token.access_token, mediaId);
            if (type == MediaType.News)
                throw new NotSupportedException("不能下载图文信息");
            else if (type == MediaType.Video)
                url = HttpClient.GetString(url).ParseToJToken()["video_url"].ToString();
            return url;
        }
        /// <summary>
        /// 高清语音素材获取接口
        /// 获取从JSSDK的uploadVoice接口上传的临时语音素材下载地址
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">素材的media_id，即uploadVoice接口返回的serverID</param>
        public static void MediaSpeexDownloadUrl(this AccessToken token, string mediaId) =>
            CoreApi.ApiUrl("/cgi-bin/media/get/jssdk?access_token={0}&media_id={1}", token.access_token, mediaId);


        /// <summary>
        /// 新增永久图文素材（仅返回media_id）
        /// </summary>
        /// <param name="token"></param>
        /// <param name="items">图文列表</param>
        /// <returns></returns>
        public static Media MaterialNewsAdd(this AccessToken token, params MediaNewItem[] items) => token.MaterialNewsAdd(items.ToList());
        /// <summary>
        /// 创建图文信息Json
        /// </summary>
        /// <param name="items">图文列表</param>
        /// <returns></returns>
        public static string MaterialNewsJson(List<MediaNewItem> items)
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
        /// <summary>
        /// 新增永久图文素材（仅返回media_id）
        /// </summary>
        /// <param name="token"></param>
        /// <param name="items">图文列表</param>
        /// <returns></returns>
        public static Media MaterialNewsAdd(this AccessToken token, List<MediaNewItem> items) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/material/add_news?access_token={0}", token.access_token), MaterialNewsJson(items)).DeserializeJsonString<Media>();


        /// <summary>
        /// 上传图文消息内的图片获取URL（仅返回url）
        /// 本接口所上传的图片不占用公众号的素材库中图片数量的5000个的限制。
        /// 图片仅支持jpg/png格式，大小必须在1MB以下。
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filePath">素材文件路径</param>
        /// <returns></returns>
        public static Media MaterialUploadImage(this AccessToken token, string filePath) =>
            HttpClient.CurlPost(CoreApi.ApiUrl("/cgi-bin/media/uploadimg?access_token={0}", token.access_token), queryDir: new Dictionary<string, string>() {
                { "media", filePath}
            }).ReadToEnd().DeserializeJsonString<Media>();
        /// <summary>
        /// 新增其他类型永久素材（返回media_id、url）
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type">素材类型</param>
        /// <param name="filePath">素材文件路径</param>
        /// <param name="title">视频素材的标题（仅视频类型需要）</param>
        /// <param name="introduction">视频素材的描述（仅视频类型需要）</param>
        /// <returns></returns>
        public static Media MaterialUpload(this AccessToken token, MediaType type, string filePath, string title = null, string introduction = null)
        {
            if (type == MediaType.News)
                throw new NotSupportedException();
            var file = new Dictionary<string, string>();
            file["media"] = filePath;
            if (type == MediaType.Video)
                file["description"] = string.Format("{{\"title\":\"{0}\",\"introduction\":\"{1}\"}}", title, introduction);
            return HttpClient.CurlPost(CoreApi.ApiUrl("/cgi-bin/material/add_material?access_token={0}", token.access_token, type.ToString().ToLower()), queryDir: file).ReadToEnd().DeserializeJsonString<Media>();
        }

        /// <summary>
        /// 获取永久素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type">素材类型</param>
        /// <param name="mediaId">素材的media_id</param>
        public static Stream MaterialDownload(this AccessToken token, MediaType type, string mediaId)
        {
            if (type == MediaType.News)
                throw new NotSupportedException();
            else if (type == MediaType.Video)
                throw new NotSupportedException("下载视频请使用MaterialVideoDownloadUrl获取下载地址");
            else
                return HttpClient.PostData(CoreApi.ApiUrl("/cgi-bin/material/get_material?access_token={0}", token.access_token), "{\"media_id\":\"" + mediaId + "\"}", mediaType: "application/json");
        }
        /// <summary>
        /// 获取永久视频素材下载地址
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">素材的media_id</param>
        /// <returns></returns>
        public static string MaterialVideoDownloadUrl(this AccessToken token, string mediaId) =>
             HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/material/get_material?access_token={0}", token.access_token), "{\"media_id\":\"" + mediaId + "\"}").ParseToJToken()["down_url"].ToString();

        /// <summary>
        /// 获取永久图文素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">素材的media_id</param>
        /// <returns></returns>
        public static List<MediaNewItem> MaterialNewsQuery(this AccessToken token, string mediaId)
        {
            var result = new List<MediaNewItem>();
            var data = HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/material/get_material?access_token={0}", token.access_token), "{\"media_id\":\"" + mediaId + "\"}").ParseToJToken();
            try
            {
                foreach (var item in data["news_item"])
                    result.Add(item.FillNewItem());
                return result;
            }
            catch
            {
                throw new Exception(data.ToString().DeserializeJsonString<JsonResult>().ToJsonString());
            }
        }
        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">素材的media_id</param>
        /// <returns></returns>
        public static JsonResult MaterialDelete(this AccessToken token, string mediaId) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/material/del_material?access_token={0}", token.access_token), "{\"media_id\":\"" + mediaId + "\"}").DeserializeJsonString<JsonResult>();
        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">要修改的图文消息的id</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <param name="item">图文项</param>
        /// <returns></returns>
        public static JsonResult MaterialNewUpdate(this AccessToken token, string mediaId, int index, MediaNewItem item)
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
            return HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/material/update_news?access_token={0}", token.access_token), str.ToString()).DeserializeJsonString<JsonResult>();
        }


        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static MediaCount MaterialCount(this AccessToken token) =>
            HttpClient.GetString(CoreApi.ApiUrl("/cgi-bin/material/get_materialcount?access_token={0}", token.access_token)).DeserializeJsonString<MediaCount>();
        
        /// <summary>
        /// 获取素材列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type">素材的类型，图片（image）、视频（video）、语音 （voice））、图文（news）</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <returns></returns>
        public static MediaList<MediaItem> MaterialMediaListQuery(this AccessToken token, MediaType type, int offset, int count) =>
             HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/material/batchget_material?access_token={0}", token.access_token),
                  "{\"type\":\"" + type.ToString().ToLower() + "\",\"offset\":" + offset.ToString() + ",\"count\":" + count.ToString() + "}"
                  ).DeserializeJsonString<MediaList<MediaItem>>();
    }
}
