using OYMLCN.WeChat.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public partial class Tags
        {
            public class Members
            {
                protected class JsonCreate
                {
                    public static string BatchTagging(int tagid, List<string> openid_list)
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("{\"openid_list\":[");
                        foreach (var item in openid_list.Take(50))
                            str.AppendFormat("\"{0}\",", item);
                        if (str.ToString().EndsWith(","))
                            str.Remove(str.Length - 1, 1);
                        str.Append("],\"tagid\":" + tagid + "}");
                        return str.ToString();
                    }
                    public static string BatchUntagging(int tagid, List<string> openid_list) => BatchTagging(tagid, openid_list);

                    public static string GetBlackList(string begin_openid = null) =>
                        "{\"begin_openid\":\"" + begin_openid + "\"}";
                    public static string BatchBlackList(List<string> openid_list)
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("{\"openid_list\":[");
                        foreach (var item in openid_list.Take(20))
                            str.AppendFormat("\"{0}\",", item);
                        if (str.ToString().EndsWith(","))
                            str.Remove(str.Length - 1, 1);
                        str.Append("]}");
                        return str.ToString();
                    }
                    public static string BatchUnblackList(List<string> openid_list) => BatchBlackList(openid_list);
                }

                public static JsonResult BatchTagging(string access_token, int tagid, List<string> openid_list) =>
                    ApiPost<JsonResult>(JsonCreate.BatchTagging(tagid, openid_list), "/cgi-bin/tags/members/batchtagging?access_token={0}", access_token);
                public static JsonResult BatchUntagging(string access_token, int tagid, List<string> openid_list) =>
                    ApiPost<JsonResult>(JsonCreate.BatchUntagging(tagid, openid_list), "/cgi-bin/tags/members/batchuntagging?access_token={0}", access_token);

                public static UsersQuery GetBlackList(string access_token, string begin_openid = null) =>
                    ApiPost<UsersQuery>(JsonCreate.GetBlackList(begin_openid), "/cgi-bin/tags/members/getblacklist?access_token={0}", access_token);
                public static JsonResult BatchBlackList(string access_token, List<string> openid_list)=>
                    ApiPost<JsonResult>(JsonCreate.BatchBlackList(openid_list), "/cgi-bin/tags/members/batchblacklist?access_token={0}", access_token);
                public static JsonResult BatchUnblackList(string access_token, List<string> openid_list)=>
                    ApiPost<JsonResult>(JsonCreate.BatchUnblackList(openid_list), "/cgi-bin/tags/members/batchunblacklist?access_token={0}", access_token);
            }
        }
    }
}
