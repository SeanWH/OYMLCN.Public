using OYMLCN.WeChat.Model;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public partial class CustomerService
        {
            public partial class Session
            {
                protected class JsonCreate
                {
                    public static string Create(string kf_account, string openid) =>
                        "{\"kf_account\":\"" + kf_account + "\",\"openid\":\"" + openid + "\"}";
                    public static string Close(string kf_account, string openid) => Create(kf_account, openid);
                }

                public static JsonResult Create(string access_token, string kf_account, string openid) =>
                    ApiPost<JsonResult>(JsonCreate.Create(kf_account, openid), "/customservice/kfsession/create?access_token={0}", access_token);
                public static JsonResult Close(string access_token, string kf_account, string openid) =>
                    ApiPost<JsonResult>(JsonCreate.Create(kf_account, openid), "/customservice/kfsession/close?access_token={0}", access_token);

                public static KefuSessionList.Session Get(string access_token, string openid) =>
                    ApiGet<KefuSessionList.Session>("/customservice/kfsession/getsession?access_token={0}&openid={1}", access_token, openid);
                public static KefuSessionList.Session[] GetList(string access_token, string kf_account) =>
                    ApiGet<KefuSessionList>("/customservice/kfsession/getsessionlist?access_token={0}&kf_account={1}", access_token, kf_account.EncodeAsUrlData()).sessionlist;
                public static KefuSessionList.Session[] GetWaitCase(string access_token) =>
                    ApiGet<KefuSessionList>("/customservice/kfsession/getwaitcase?access_token={0}", access_token).waitcaselist;

            }
        }
    }
}
