namespace OYMLCN.WeChat
{
    public partial class Api
    {
        protected class JsonCreate
        {
            public static string ShortUrl(string url) =>
                "{\"action\":\"long2short\",\"long_url\":\"" + url + "\"}";
        }

        public static string ShortUrl(string access_token, string url) =>
            ApiJTokenPost(JsonCreate.ShortUrl(url), "/cgi-bin/shorturl?access_token={0}", access_token).GetString("short_url");
    }
}
