using OYMLCN.WeChat.Enums;
using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public class Media
        {
            public static MediaUpload Upload(string access_token, MediaType type, string filePath) =>
                ApiPostFile<MediaUpload>(new Dictionary<string, string>() {
                    { "media", filePath}
                }, "/cgi-bin/media/upload?access_token={0}&type={1}", access_token, type.ToString().ToLower());

            public static string GetUrl(string access_token, MediaType type, string media_id)
            {
                string url = ApiUrl("/cgi-bin/media/get?access_token={0}&media_id={1}", access_token, media_id);
                if (type == MediaType.News)
                    throw new NotSupportedException("不能下载图文信息");
                else if (type == MediaType.Video)
                    url = HttpClientExtensions.GetString(url).ParseToJToken()["video_url"].ToString();
                return url;
            }
            public static string SpeexDownloadUrl(string access_token, string media_id) =>
                ApiUrl("/cgi-bin/media/get/jssdk?access_token={0}&media_id={1}", access_token, media_id);
        }
    }
}
