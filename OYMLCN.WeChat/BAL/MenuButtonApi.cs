using System.Collections.Generic;
using OYMLCN.WeChat.Model;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信自定义菜单通讯辅助逻辑
    /// </summary>
    public static class MenuButtonApi
    {
        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <param name="token"></param>
        /// <param name="menu1"></param>
        /// <param name="menu2"></param>
        /// <param name="menu3"></param>
        /// <returns></returns>
        public static JsonResult MenuCreate(this AccessToken token, MenuBase menu1, MenuBase menu2 = null, MenuBase menu3 = null)
        {
            var list = new List<MenuBase>();
            list.Add(menu1);
            list.Add(menu2);
            list.Add(menu3);
            return token.MenuCreate(list.ToList());
        }
        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <param name="token"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public static JsonResult MenuCreate(this AccessToken token, List<MenuBase> buttons) =>
            Api.Menu.Create(token.access_token, buttons);

        /// <summary>
        /// 自定义菜单删除
        /// 注意：调用此接口会删除默认菜单及全部个性化菜单。
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static JsonResult MenuDelete(this AccessToken token) => Api.Menu.Delete(token.access_token);


        /// <summary>
        /// 创建个性化菜单
        /// </summary>
        /// <param name="token"></param>
        /// <param name="rule">匹配规则共六个字段，均可为空，但不能全部为空，至少要有一个匹配信息是不为空的。</param>
        /// <param name="menu1"></param>
        /// <param name="menu2"></param>
        /// <param name="menu3"></param>
        /// <returns>menuid</returns>
        public static string MenuCreatCondition(this AccessToken token, MenuMatchRule rule, MenuBase menu1, MenuBase menu2 = null, MenuBase menu3 = null)
        {
            var list = new List<MenuBase>();
            list.Add(menu1);
            list.Add(menu2);
            list.Add(menu3);
            return token.MenuCreatCondition(rule, list.ToList());
        }
        /// <summary>
        /// 创建个性化菜单
        /// </summary>
        /// <param name="token"></param>
        /// <param name="rule">匹配规则共六个字段，均可为空，但不能全部为空，至少要有一个匹配信息是不为空的。</param>
        /// <param name="buttons"></param>
        /// <returns>menuid</returns>
        public static string MenuCreatCondition(this AccessToken token, MenuMatchRule rule, List<MenuBase> buttons) =>
            Api.Menu.AddCondition(token.access_token, rule, buttons);
        /// <summary>
        /// 删除指定的个性化菜单
        /// </summary>
        /// <param name="token"></param>
        /// <param name="menuid">menuid为菜单id，可以通过自定义菜单查询接口获取。</param>
        /// <returns></returns>
        public static JsonResult MenuDeleteCondition(this AccessToken token, string menuid) =>
            Api.Menu.DelCondition(token.access_token, menuid);
        /// <summary>
        /// 测试个性化菜单匹配结果
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId">可以是粉丝的OpenID，也可以是粉丝的微信号。</param>
        public static JToken MenuTestCondition(this AccessToken token, string userId) =>
            Api.Menu.TryMatch(token.access_token, userId);


        /// <summary>
        /// 自定义菜单查询
        /// </summary>
        /// <param name="token"></param>
        public static JToken MenuQuery(this AccessToken token) =>
            Api.Menu.Get(token.access_token);
        /// <summary>
        /// 获取自定义菜单配置
        /// （仅能查询默认菜单及状态，个性化菜单信息请使用MenuQuery查询）
        /// </summary>
        /// <param name="token"></param>
        public static JToken MenuConfigQuery(this AccessToken token) =>
            Api.Menu.GetCurrentSelfMenuInfo(token.access_token);
    }
}
