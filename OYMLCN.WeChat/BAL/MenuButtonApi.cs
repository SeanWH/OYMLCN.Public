using System.Collections.Generic;
using System.Text;
using OYMLCN;
using OYMLCN.WeChat.Model;
using System.Linq;

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
        public static JsonResult MenuCreate(this AccessToken token, MenuButtonObject menu1, MenuButtonObject menu2 = null, MenuButtonObject menu3 = null)
        {
            var list = new List<MenuButtonObject>();
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
        public static JsonResult MenuCreate(this AccessToken token, List<MenuButtonObject> buttons)
        {
            StringBuilder str = new StringBuilder();
            str.Append("{\"button\":[");
            foreach (var item in buttons.Take(3))
                if (item != null)
                    str.Append(item.ToString());
            if (str.ToString().EndsWith(","))
                str.Remove(str.Length - 1, 1);
            str.Append("]}");
            return HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/menu/create?access_token={0}", token.access_token), str.ToString()).DeserializeJsonString<JsonResult>();
        }


        /// <summary>
        /// 自定义菜单删除
        /// 注意：调用此接口会删除默认菜单及全部个性化菜单。
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static JsonResult MenuDelete(this AccessToken token) => HttpClient.GetString(CoreApi.ApiUrl("/cgi-bin/menu/delete?access_token={0}", token.access_token)).DeserializeJsonString<JsonResult>();


        /// <summary>
        /// 创建个性化菜单
        /// </summary>
        /// <param name="token"></param>
        /// <param name="rule">匹配规则共六个字段，均可为空，但不能全部为空，至少要有一个匹配信息是不为空的。</param>
        /// <param name="menu1"></param>
        /// <param name="menu2"></param>
        /// <param name="menu3"></param>
        /// <returns></returns>
        public static JsonResult MenuCreatCondition(this AccessToken token, MenuButtonMatchRule rule, MenuButtonObject menu1, MenuButtonObject menu2 = null, MenuButtonObject menu3 = null)
        {
            var list = new List<MenuButtonObject>();
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
        /// <returns></returns>
        public static JsonResult MenuCreatCondition(this AccessToken token, MenuButtonMatchRule rule, List<MenuButtonObject> buttons)
        {
            StringBuilder str = new StringBuilder();
            str.Append("{\"button\":[");
            foreach (var item in buttons.Take(3))
                if (item != null)
                    str.Append(item.ToString());
            if (str.ToString().EndsWith(","))
                str.Remove(str.Length - 1, 1);
            str.Append("],").Append(rule.ToString()).Append("}");
            return HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/menu/addconditional?access_token={0}", token.access_token), str.ToString()).DeserializeJsonString<JsonResult>();
        }
        /// <summary>
        /// 删除指定的个性化菜单
        /// </summary>
        /// <param name="token"></param>
        /// <param name="menuid">menuid为菜单id，可以通过自定义菜单查询接口获取。</param>
        /// <returns></returns>
        public static JsonResult MenuDeleteCondition(this AccessToken token, int menuid) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/menu/delconditional?access_token={0}", token.access_token), 
                "{\"menuid\":\"" + menuid.ToString() + "\"}").DeserializeJsonString<JsonResult>();
        /// <summary>
        /// 测试个性化菜单匹配结果
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId">可以是粉丝的OpenID，也可以是粉丝的微信号。</param>
        public static string MenuTestCondition(this AccessToken token, string userId) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/menu/trymatch?access_token={0}", token.access_token),
                "{\"user_id\":\"" + userId + "\"}");


        /// <summary>
        /// 自定义菜单查询
        /// </summary>
        /// <param name="token"></param>
        public static MenuButtonInfo MenuQuery(this AccessToken token) =>
            HttpClient.GetString(CoreApi.ApiUrl("/cgi-bin/menu/get?access_token={0}", token.access_token))
                .ParseToJToken().ParseToMenuButton();
        /// <summary>
        /// 获取自定义菜单配置
        /// （仅能查询默认菜单及状态，个性化菜单信息请使用MenuQuery查询）
        /// </summary>
        /// <param name="token"></param>
        public static MenuButtonConfig MenuConfigQuery(this AccessToken token)
        {
            var config = new MenuButtonConfig();
            var data = HttpClient.GetString(CoreApi.ApiUrl("/cgi-bin/get_current_selfmenu_info?access_token={0}", token.access_token)).ParseToJToken();
            config.IsMenuOpen = data["is_menu_open"].ToString() == "1" ? true : false;
            config.Info = data["selfmenu_info"].ParseToMenuButton();
            return config;
        }
    }
}
