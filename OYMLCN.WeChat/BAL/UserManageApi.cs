using OYMLCN.WeChat.Enum;
using OYMLCN.WeChat.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 用户管理接口通讯辅助
    /// </summary>
    public static class UserManageApi
    {
        /// <summary>
        /// 创建标签（返回结果字段为tag）
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name">标签名称</param>
        /// <returns></returns>
        public static TagResult TagCreate(this AccessToken token, string name) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/tags/create?access_token={0}", token.access_token), 
                "{\"tag\":{\"name\":\"" + name + "\"}}").DeserializeJsonString<TagResult>();
        /// <summary>
        /// 获取已创建的标签（返回结果字段为tags）
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static TagResult TagQuery(this AccessToken token) =>
            HttpClient.GetString(CoreApi.ApiUrl("/cgi-bin/tags/get?access_token={0}", token.access_token)).DeserializeJsonString<TagResult>();
        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <param name="token"></param>
        /// <param name="id">已有标签Id</param>
        /// <param name="name">新的标签名称</param>
        /// <returns></returns>
        public static JsonResult TagUpdate(this AccessToken token, int id, string name) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/tags/update?access_token={0}", token.access_token),
                "{\"tag\":{\"id\":" + id.ToString() + ",\"name\":\"" + name + "\"}}").DeserializeJsonString<JsonResult>();
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="token"></param>
        /// <param name="id">标签Id</param>
        /// <returns></returns>
        public static JsonResult TagDelete(this AccessToken token, int id) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/tags/delete?access_token={0}", token.access_token), 
                "{\"tag\":{\"id\":" + id.ToString() + "}}").DeserializeJsonString<JsonResult>();

        /// <summary>
        /// 获取标签下粉丝列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="id">标签Id</param>
        /// <param name="nextOpenId">第一个拉取的OPENID，不填默认从头开始拉取</param>
        /// <returns></returns>
        public static TagUsers TagUsersQuery(this AccessToken token, int id, string nextOpenId = "") =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/user/tag/get?access_token={0}", token.access_token), 
                "{\"tagid\":" + id + ",\"next_openid\":\"" + nextOpenId + "\"}").DeserializeJsonString<TagUsers>();


        /// <summary>
        /// 批量为用户打标签
        /// 粉丝身上的标签数不能超过20个的限制
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tagId"></param>
        /// <param name="openid">每次传入的openid列表个数不能超过50个</param>
        /// <returns></returns>
        public static JsonResult TagApply(this AccessToken token, int tagId, params string[] openid) => token.TagApply(tagId, openid.ToList());
        /// <summary>
        /// 批量为用户打标签
        /// 粉丝身上的标签数不能超过20个的限制
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tagId"></param>
        /// <param name="openid">每次传入的openid列表个数不能超过50个</param>
        public static JsonResult TagApply(this AccessToken token, int tagId, List<string> openid)
        {
            StringBuilder str = new StringBuilder();
            str.Append("{\"openid_list\":[");
            foreach (var item in openid.Take(50))
                str.AppendFormat("\"{0}\",", item);
            if (str.ToString().EndsWith(","))
                str.Remove(str.Length - 1, 1);
            str.Append("],\"tagid\":" + tagId + "}");
            return HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/tags/members/batchtagging?access_token={0}", token.access_token), 
                str.ToString()).DeserializeJsonString<JsonResult>();
        }
        /// <summary>
        /// 批量为用户取消标签
        /// 粉丝身上的标签数不能超过20个的限制
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tagId"></param>
        /// <param name="openid">每次传入的openid列表个数不能超过50个</param>
        /// <returns></returns>
        public static JsonResult TagCancel(this AccessToken token, int tagId, params string[] openid) => token.TagCancel(tagId, openid.ToList());
        /// <summary>
        /// 批量为用户取消标签
        /// 粉丝身上的标签数不能超过20个的限制
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tagId"></param>
        /// <param name="openid">每次传入的openid列表个数不能超过50个</param>
        public static JsonResult TagCancel(this AccessToken token, int tagId, List<string> openid)
        {
            StringBuilder str = new StringBuilder();
            str.Append("{\"openid_list\":[");
            foreach (var item in openid.Take(50))
                str.AppendFormat("\"{0}\",", item);
            if (str.ToString().EndsWith(","))
                str.Remove(str.Length - 1, 1);
            str.Append("],\"tagid\":" + tagId + "}");
            return HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/tags/members/batchuntagging?access_token={0}", token.access_token), 
                str.ToString()).DeserializeJsonString<JsonResult>();
        }

        /// <summary>
        /// 获取用户身上的标签列表（返回结果字段为tagid_list）
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static UserInfo TagUserQuery(this AccessToken token, string openid) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/tags/getidlist?access_token={0}", token.access_token),
                "{\"openid\":\"" + openid + "\"}").DeserializeJsonString<UserInfo>();


        /// <summary>
        /// 设置用户备注名
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid">用户标识</param>
        /// <param name="remark">新的备注名，长度必须小于30字符 </param>
        /// <returns></returns>
        public static JsonResult UserRemark(this AccessToken token, string openid, string remark) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/user/info/updateremark?access_token={0}", token.access_token), 
                "{\"openid\":\"" + openid + "\",\"remark\":\"" + remark + "\"}"
                ).DeserializeJsonString<JsonResult>();


        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid">普通用户的标识，对当前公众号唯一</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <returns></returns>
        public static UserInfo UserInfo(this AccessToken token, string openid, Language lang = Language.zh_CN) =>
             HttpClient.GetString(CoreApi.ApiUrl("/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}", token.access_token, openid, lang.ToString())).DeserializeJsonString<UserInfo>();
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="next_openid">第一个拉取的OPENID，不填默认从头开始拉取</param>
        /// <returns></returns>
        public static UsersQuery UsersQuery(this AccessToken token, string next_openid = null) =>
            HttpClient.GetString(CoreApi.ApiUrl("/cgi-bin/user/get?access_token={0}&next_openid={1}", token.access_token, next_openid)).DeserializeJsonString<UsersQuery>();
        /// <summary>
        /// 获取公众号的黑名单列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="begin_openid"></param>
        /// <returns></returns>
        public static UsersQuery UserDefriendQuery(this AccessToken token, string begin_openid = null) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/tags/members/getblacklist?access_token={0}", token.access_token),
                "{\"begin_openid\":\"" + begin_openid + "\"}").DeserializeJsonString<UsersQuery>();


        /// <summary>
        /// 拉黑用户
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid">需要拉入黑名单的用户的openid，一次拉黑最多允许20个</param>
        /// <returns></returns>
        public static JsonResult UserDefriendApply(this AccessToken token, params string[] openid) => token.UserDefriendApply(openid.ToList());
        /// <summary>
        /// 拉黑用户
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid">需要拉入黑名单的用户的openid，一次拉黑最多允许20个</param>
        /// <returns></returns>
        public static JsonResult UserDefriendApply(this AccessToken token, List<string> openid)
        {
            StringBuilder str = new StringBuilder();
            str.Append("{\"opened_list\":[");
            foreach (var item in openid.Take(20))
                str.AppendFormat("{0},", item);
            if (str.ToString().EndsWith(","))
                str.Remove(str.Length - 1, 1);
            str.Append("]}");
            return HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/tags/members/batchblacklist?access_token={0}", token.access_token),
                str.ToString()).DeserializeJsonString<JsonResult>();
        }
        /// <summary>
        /// 取消拉黑用户
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid">需要拉出黑名单的用户的openid，一次拉出最多允许20个</param>
        /// <returns></returns>
        public static JsonResult UserDefriendCancel(this AccessToken token, params string[] openid) => token.UserDefriendCancel(openid.ToList());
        /// <summary>
        /// 取消拉黑用户
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid">需要拉出黑名单的用户的openid，一次拉出最多允许20个</param>
        /// <returns></returns>
        public static JsonResult UserDefriendCancel(this AccessToken token, List<string> openid)
        {
            StringBuilder str = new StringBuilder();
            str.Append("{\"opened_list\":[");
            foreach (var item in openid.Take(20))
                str.AppendFormat("{0},", item);
            if (str.ToString().EndsWith(","))
                str.Remove(str.Length - 1, 1);
            str.Append("]}");
            return HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/tags/members/batchunblacklist?access_token={0}", token.access_token),
                str.ToString()).DeserializeJsonString<JsonResult>();
        }
    }
}
