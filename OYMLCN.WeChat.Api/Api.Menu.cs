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
            protected class JsonCreate
            {
                public static string Create(List<MenuBase> buttons)
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
                public static string AddCondition(MenuMatchRule rule, List<MenuBase> buttons)
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
                public static string TryMatch(string user_id) =>
                    "{\"user_id\":\"" + user_id + "\"}";
                public static string DelCondition(string menuid) =>
                    "{\"menuid\":\"" + menuid + "\"}";
            }

            public static JsonResult Create(string access_token, List<MenuBase> buttons) =>
                ApiPost<JsonResult>(JsonCreate.Create(buttons), "/cgi-bin/menu/create?access_token={0}", access_token);
            public static JsonResult Delete(string access_token) =>
                ApiGet<JsonResult>("/cgi-bin/menu/delete?access_token={0}", access_token);
            public static string AddCondition(string access_token, MenuMatchRule rule, List<MenuBase> buttons) =>
                ApiJTokenPost(JsonCreate.AddCondition(rule, buttons), "/cgi-bin/menu/addconditional?access_token={0}", access_token)
                    ?.GetString("menuid") ?? throw new Exception("未找到个性化菜单Id信息");
            public static JToken TryMatch(string access_token, string user_id) =>
                ApiJTokenPost(JsonCreate.TryMatch(user_id), "/cgi-bin/menu/trymatch?access_token={0}", access_token);
            public static JsonResult DelCondition(string access_token, string menuid) =>
                ApiPost<JsonResult>(JsonCreate.DelCondition(menuid), "/cgi-bin/menu/delconditional?access_token={0}", access_token);
            public static JToken Get(string access_token) =>
                ApiJTokenGet("/cgi-bin/menu/get?access_token={0}", access_token);
            public static JToken GetCurrentSelfMenuInfo(string access_token) =>
                ApiJTokenGet("/cgi-bin/get_current_selfmenu_info?access_token={0}", access_token);
        }
    }
}
