using Newtonsoft.Json.Linq;
using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public partial class CustomerService
        {
            public partial class Session
            {
                public static JsonResult Create(AccessToken token, string openid, string kfName, Config cfg = null) =>
                    ApiPost<JsonResult>("{\"kf_account\":\"" + CompleteKefuName(kfName, cfg) + "\",\"openid\":\"" + openid + "\"}",
                        "/customservice/kfsession/create?access_token={0}", token.access_token);
                public static JsonResult Close(AccessToken token, string openid, string kfName, Config cfg = null) =>
                    ApiPost<JsonResult>("{\"kf_account\":\"" + CompleteKefuName(kfName, cfg) + "\",\"openid\":\"" + openid + "\"}",
                        "/customservice/kfsession/close?access_token={0}", token.access_token);

                public static KefuSessionList.Session Get(AccessToken token, string openid) =>
                    ApiGet<KefuSessionList.Session>("/customservice/kfsession/getsession?access_token={0}&openid={1}", token.access_token, openid);
                public static KefuSessionList.Session[] GetList(AccessToken token, string kfName, Config cfg = null) =>
                    ApiGet<KefuSessionList>("/customservice/kfsession/getsessionlist?access_token={0}&kf_account={1}", token.access_token, CompleteKefuName(kfName, cfg).EncodeAsUrlData()).sessionlist;
                public static KefuSessionList.Session[] GetWaitCase(AccessToken token) =>
                    ApiGet<KefuSessionList>("/customservice/kfsession/getwaitcase?access_token={0}", token.access_token).waitcaselist;

            }
        }
    }
}
