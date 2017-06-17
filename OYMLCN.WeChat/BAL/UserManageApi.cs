using OYMLCN.WeChat.Enums;
using OYMLCN.WeChat.Model;
using System.Collections.Generic;
using System.Linq;

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
        public static TagResult.Tag TagCreate(this AccessToken token, string name) =>
            Api.Tags.Create(token.access_token, name);
        /// <summary>
        /// 获取已创建的标签（返回结果字段为tags）
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static TagResult.Tag[] TagQuery(this AccessToken token) =>
            Api.Tags.Get(token.access_token);
        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <param name="token"></param>
        /// <param name="id">已有标签Id</param>
        /// <param name="name">新的标签名称</param>
        /// <returns></returns>
        public static JsonResult TagUpdate(this AccessToken token, int id, string name) =>
            Api.Tags.Update(token.access_token, id, name);
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="token"></param>
        /// <param name="id">标签Id</param>
        /// <returns></returns>
        public static JsonResult TagDelete(this AccessToken token, int id) =>
            Api.Tags.Delete(token.access_token, id);

        /// <summary>
        /// 获取标签下粉丝列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="id">标签Id</param>
        /// <param name="nextOpenId">第一个拉取的OPENID，不填默认从头开始拉取</param>
        /// <returns></returns>
        public static TagUsers TagUsersQuery(this AccessToken token, int id, string nextOpenId = "") =>
            Api.Tags.GetUsers(token.access_token, id, nextOpenId);


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
        public static JsonResult TagApply(this AccessToken token, int tagId, List<string> openid) =>
            Api.Tags.Members.BatchTagging(token.access_token, tagId, openid);
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
        public static JsonResult TagCancel(this AccessToken token, int tagId, List<string> openid) =>
            Api.Tags.Members.BatchUntagging(token.access_token, tagId, openid);

        /// <summary>
        /// 获取用户身上的标签列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static int[] TagUserQuery(this AccessToken token, string openid) =>
            Api.Tags.GetIdList(token.access_token, openid);


        /// <summary>
        /// 设置用户备注名
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid">用户标识</param>
        /// <param name="remark">新的备注名，长度必须小于30字符 </param>
        /// <returns></returns>
        public static JsonResult UserRemark(this AccessToken token, string openid, string remark) =>
            Api.User.UpdateRemark(token.access_token, openid, remark);


        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid">普通用户的标识，对当前公众号唯一</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <returns></returns>
        public static UserInfo UserInfo(this AccessToken token, string openid, Language lang = Language.zh_CN) =>
             Api.User.Info(token.access_token, openid, lang);
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="next_openid">第一个拉取的OPENID，不填默认从头开始拉取</param>
        /// <returns></returns>
        public static UsersQuery UsersQuery(this AccessToken token, string next_openid = null) =>
            Api.User.Get(token.access_token, next_openid);
        /// <summary>
        /// 获取公众号的黑名单列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="begin_openid"></param>
        /// <returns></returns>
        public static UsersQuery UserDefriendQuery(this AccessToken token, string begin_openid = null) =>
            Api.Tags.Members.GetBlackList(token.access_token, begin_openid);


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
        public static JsonResult UserDefriendApply(this AccessToken token, List<string> openid) =>
            Api.Tags.Members.BatchBlackList(token.access_token, openid);
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
        public static JsonResult UserDefriendCancel(this AccessToken token, List<string> openid) =>
            Api.Tags.Members.BatchUnblackList(token.access_token, openid);
    }
}
