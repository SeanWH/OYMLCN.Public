using Newtonsoft.Json.Linq;
using OYMLCN.WeChat.Enums;
using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public static class Media
        {
            public static MediaUpload Upload(AccessToken token, MediaType type, string filePath) =>
                ApiPostFile<MediaUpload>(new Dictionary<string, string>() {
                    { "media", filePath}
                }, "/cgi-bin/media/upload?access_token={0}&type={1}", token.access_token, type.ToString().ToLower());

            public static string GetUrl(AccessToken token, MediaType type, string mediaId)
            {
                string url = ApiUrl("/cgi-bin/media/get?access_token={0}&media_id={1}", token.access_token, mediaId);
                if (type == MediaType.News)
                    throw new NotSupportedException("不能下载图文信息");
                else if (type == MediaType.Video)
                    url = HttpClient.GetString(url).ParseToJToken()["video_url"].ToString();
                return url;
            }
            public static string SpeexDownloadUrl(AccessToken token, string mediaId) =>
                ApiUrl("/cgi-bin/media/get/jssdk?access_token={0}&media_id={1}", token.access_token, mediaId);
        }
    }
}
