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
            public static TagResult.Tag Create(AccessToken token, string name) =>
                ApiPost<TagResult>("{\"tag\":{\"name\":\"" + name + "\"}}", "/cgi-bin/tags/create?access_token={0}", token.access_token).tag;
            public static TagResult.Tag[] Get(AccessToken token) =>
                ApiGet<TagResult>("/cgi-bin/tags/get?access_token={0}", token.access_token).tags;
            public static JsonResult Update(AccessToken token, int id, string name) =>
                ApiPost<JsonResult>("{\"tag\":{\"id\":" + id.ToString() + ",\"name\":\"" + name + "\"}}", "/cgi-bin/tags/update?access_token={0}", token.access_token);
            public static JsonResult Delete(AccessToken token, int id) =>
                ApiPost<JsonResult>("{\"tag\":{\"id\":" + id.ToString() + "}}", "/cgi-bin/tags/delete?access_token={0}", token.access_token);

            public static TagUsers GetUsers(AccessToken token, int id, string nextOpenId = "") =>
                ApiPost<TagUsers>("{\"tagid\":" + id + ",\"next_openid\":\"" + nextOpenId + "\"}", "/cgi-bin/user/tag/get?access_token={0}", token.access_token);
            public static int[] GetIdList(AccessToken token, string openid) =>
                ApiPost<UserInfo>("{\"openid\":\"" + openid + "\"}", "/cgi-bin/tags/getidlist?access_token={0}", token.access_token).tagid_list;

        }
    }
}
