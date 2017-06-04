using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public static string ShortUrl(AccessToken token, string url) =>
            ApiJTokenPost("{\"action\":\"long2short\",\"long_url\":\"" + url + "\"}",
                "/cgi-bin/shorturl?access_token={0}", token.access_token)
            .GetString("short_url");
    }
}
