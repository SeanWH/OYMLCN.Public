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
        public partial class Menu
        {
            public static JsonResult Create(AccessToken token, MenuBase menu1, MenuBase menu2 = null, MenuBase menu3 = null)
            {
                var list = new List<MenuBase>();
                list.Add(menu1);
                list.Add(menu2);
                list.Add(menu3);
                return Create(token, list.Where(d => d != null).ToList());
            }
            public static string CreateJson(List<MenuBase> buttons)
            {
                StringBuilder str = new StringBuilder();
                str.Append("{\"button\":[");
                foreach (var item in buttons)
                    if (item != null)
                        str.Append(item.ToString());
                if (str.ToString().EndsWith(","))
                    str.Remove(str.Length - 1, 1);
                str.Append("]}");
                return str.ToString();
            }
            public static JsonResult Create(AccessToken token, List<MenuBase> buttons) =>
                ApiPost<JsonResult>(CreateJson(buttons), "/cgi-bin/menu/create?access_token={0}", token.access_token);
            public static JsonResult Delete(AccessToken token) =>
                ApiGet<JsonResult>("/cgi-bin/menu/delete?access_token={0}", token.access_token);


            public static string AddCondition(AccessToken token, MenuMatchRule rule, MenuBase menu1, MenuBase menu2 = null, MenuBase menu3 = null)
            {
                var list = new List<MenuBase>();
                list.Add(menu1);
                list.Add(menu2);
                list.Add(menu3);
                return AddCondition(token, rule, list.ToList());
            }

            public static string CreateConditionJson(MenuMatchRule rule, List<MenuBase> buttons)
            {
                StringBuilder str = new StringBuilder();
                str.Append("{\"button\":[");
                foreach (var item in buttons.Take(3))
                    if (item != null)
                        str.Append(item.ToString());
                if (str.ToString().EndsWith(","))
                    str.Remove(str.Length - 1, 1);
                str.Append("],").Append(rule.ToString()).Append("}");
                return str.ToString();
            }
            public static string AddCondition(AccessToken token, MenuMatchRule rule, List<MenuBase> buttons) =>
                ApiJTokenPost(CreateConditionJson(rule, buttons), "/cgi-bin/menu/addconditional?access_token={0}", token.access_token)
                    ?.GetString("menuid") ?? throw new Exception("未找到个性化菜单Id信息");

            public static JToken TryMatch(AccessToken token, string userId) =>
                ApiJTokenPost("{\"user_id\":\"" + userId + "\"}", "/cgi-bin/menu/trymatch?access_token={0}", token.access_token);


            public static JsonResult DelCondition(AccessToken token, string menuid) =>
                ApiPost<JsonResult>("{\"menuid\":\"" + menuid.ToString() + "\"}", "/cgi-bin/menu/delconditional?access_token={0}", token.access_token);

            public static JToken Get(AccessToken token) =>
                ApiJTokenGet("/cgi-bin/menu/get?access_token={0}", token.access_token);

            public static JToken GetCurrentSelfMenuInfo(AccessToken token) =>
                ApiJTokenGet("/cgi-bin/get_current_selfmenu_info?access_token={0}", token.access_token);
        }
    }
}
