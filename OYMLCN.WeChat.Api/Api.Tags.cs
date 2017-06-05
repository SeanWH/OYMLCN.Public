using OYMLCN.WeChat.Model;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public partial class Tags
        {
            protected class JsonCreate
            {
                public static string Create(string name) =>
                    "{\"tag\":{\"name\":\"" + name + "\"}}";
                public static string Update(int id, string name) =>
                    "{\"tag\":{\"id\":" + id.ToString() + ",\"name\":\"" + name + "\"}}";
                public static string Delete(int id) =>
                    "{\"tag\":{\"id\":" + id.ToString() + "}}";
                public static string GetUsers(int id, string next_openid = "") =>
                    "{\"tagid\":" + id + ",\"next_openid\":\"" + next_openid + "\"}";
                public static string GetIdList(string openid) =>
                    "{\"openid\":\"" + openid + "\"}";
            }

            public static TagResult.Tag Create(string access_token, string name) =>
                ApiPost<TagResult>(JsonCreate.Create(name), "/cgi-bin/tags/create?access_token={0}", access_token).tag;
            public static TagResult.Tag[] Get(string access_token) =>
                ApiGet<TagResult>("/cgi-bin/tags/get?access_token={0}", access_token).tags;
            public static JsonResult Update(string access_token, int id, string name) =>
                ApiPost<JsonResult>(JsonCreate.Update(id, name), "/cgi-bin/tags/update?access_token={0}", access_token);
            public static JsonResult Delete(string access_token, int id) =>
                ApiPost<JsonResult>(JsonCreate.Delete(id), "/cgi-bin/tags/delete?access_token={0}", access_token);

            public static TagUsers GetUsers(string access_token, int id, string next_openid = "") =>
                ApiPost<TagUsers>(JsonCreate.GetUsers(id, next_openid), "/cgi-bin/user/tag/get?access_token={0}", access_token);
            public static int[] GetIdList(string access_token, string openid) =>
                ApiPost<UserInfo>(JsonCreate.GetIdList(openid), "/cgi-bin/tags/getidlist?access_token={0}", access_token).tagid_list;

        }
    }
}
