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
        public static class User
        {
            public static JsonResult UpdateRemark(AccessToken token, string openid, string remark) =>
                ApiPost<JsonResult>("{\"openid\":\"" + openid + "\",\"remark\":\"" + remark + "\"}",
                    "/cgi-bin/user/info/updateremark?access_token={0}", token.access_token);

            public static UserInfo Info(AccessToken token, string openid, Language lang = Language.zh_CN) =>
                 ApiGet<UserInfo>("/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}", token.access_token, openid, lang.ToString());

            public static UsersQuery Get(AccessToken token, string next_openid = null) =>
                ApiGet<UsersQuery>("/cgi-bin/user/get?access_token={0}&next_openid={1}", token.access_token, next_openid);
          
        }
    }
}
