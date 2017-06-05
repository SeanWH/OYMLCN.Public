using OYMLCN.WeChat.Enums;
using OYMLCN.WeChat.Model;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public class User
        {
            protected class JsonCreate
            {
                public static string UpdateRemark(string openid, string remark) =>
                    "{\"openid\":\"" + openid + "\",\"remark\":\"" + remark + "\"}";
            }

            public static JsonResult UpdateRemark(string access_token, string openid, string remark) =>
                ApiPost<JsonResult>(JsonCreate.UpdateRemark(openid, remark), "/cgi-bin/user/info/updateremark?access_token={0}", access_token);
            public static UserInfo Info(string access_token, string openid, Language lang = Language.zh_CN) =>
                 ApiGet<UserInfo>("/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}", access_token, openid, lang.ToString());
            public static UsersQuery Get(string access_token, string next_openid = null) =>
                ApiGet<UsersQuery>("/cgi-bin/user/get?access_token={0}&next_openid={1}", access_token, next_openid);

        }
    }
}
