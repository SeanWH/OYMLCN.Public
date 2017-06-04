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
        public partial class Tags
        {
            public static class Members
            {
                public static JsonResult BatchTagging(AccessToken token, int tagId, params string[] openid) => BatchTagging(token, tagId, openid.ToList());
                public static JsonResult BatchTagging(AccessToken token, int tagId, List<string> openid)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("{\"openid_list\":[");
                    foreach (var item in openid.Take(50))
                        str.AppendFormat("\"{0}\",", item);
                    if (str.ToString().EndsWith(","))
                        str.Remove(str.Length - 1, 1);
                    str.Append("],\"tagid\":" + tagId + "}");
                    return ApiPost<JsonResult>(str.ToString(), "/cgi-bin/tags/members/batchtagging?access_token={0}", token.access_token);
                }
                public static JsonResult BatchUntagging(AccessToken token, int tagId, params string[] openid) => BatchUntagging(token, tagId, openid.ToList());
                public static JsonResult BatchUntagging(AccessToken token, int tagId, List<string> openid)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("{\"openid_list\":[");
                    foreach (var item in openid.Take(50))
                        str.AppendFormat("\"{0}\",", item);
                    if (str.ToString().EndsWith(","))
                        str.Remove(str.Length - 1, 1);
                    str.Append("],\"tagid\":" + tagId + "}");
                    return ApiPost<JsonResult>(str.ToString(), "/cgi-bin/tags/members/batchuntagging?access_token={0}", token.access_token);
                }


                public static UsersQuery GetBlackList(AccessToken token, string begin_openid = null) =>
                    ApiPost<UsersQuery>("{\"begin_openid\":\"" + begin_openid + "\"}",
                        "/cgi-bin/tags/members/getblacklist?access_token={0}", token.access_token);

                public static JsonResult BatchBlackList(AccessToken token, params string[] openid) => BatchBlackList(token, openid.ToList());
                public static JsonResult BatchBlackList(AccessToken token, List<string> openid)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("{\"openid_list\":[");
                    foreach (var item in openid.Take(20))
                        str.AppendFormat("\"{0}\",", item);
                    if (str.ToString().EndsWith(","))
                        str.Remove(str.Length - 1, 1);
                    str.Append("]}");
                    return ApiPost<JsonResult>(str.ToString(), "/cgi-bin/tags/members/batchblacklist?access_token={0}", token.access_token);
                }
                public static JsonResult BatchUnblackList(AccessToken token, params string[] openid) => BatchUnblackList(token, openid.ToList());
                public static JsonResult BatchUnblackList(AccessToken token, List<string> openid)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("{\"openid_list\":[");
                    foreach (var item in openid.Take(20))
                        str.AppendFormat("\"{0}\",", item);
                    if (str.ToString().EndsWith(","))
                        str.Remove(str.Length - 1, 1);
                    str.Append("]}");
                    return ApiPost<JsonResult>(str.ToString(), "/cgi-bin/tags/members/batchunblacklist?access_token={0}", token.access_token);
                }

            }
        }
    }
}
