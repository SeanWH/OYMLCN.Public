using Newtonsoft.Json.Linq;
using OYMLCN.WeChat.Enum;
using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// Extension
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 将自定义菜单类型字符串转换为实体类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static MenuButtonType ToMenuButtonType(this string str)
        {
            switch (str.ToLower())
            {
                case "click":
                    return MenuButtonType.Click;
                case "view":
                    return MenuButtonType.View;
                case "scancode_push":
                    return MenuButtonType.ScanPush;
                case "scancode_waitmsg":
                    return MenuButtonType.ScanWait;
                case "pic_sysphoto":
                    return MenuButtonType.SysPhoto;
                case "pic_photo_or_album":
                    return MenuButtonType.PhotoOrAlbum;
                case "pic_weixin":
                    return MenuButtonType.PicWeixin;
                case "location_select":
                    return MenuButtonType.LocationSelect;
                case "media_id":
                    return MenuButtonType.Media;
                case "view_limited":
                    return MenuButtonType.ViewLimited;
                default:
                    throw new NotSupportedException();
            }
        }

        private static MenuButtonMatchLanguage ToMenuButtonMatchLanguage(this string str)
        {
            switch (str.ToLower())
            {
                case "zh_cn":
                    return MenuButtonMatchLanguage.简体中文;
                case "zh_tw":
                    return MenuButtonMatchLanguage.繁体中文TW;
                case "zh_hk":
                    return MenuButtonMatchLanguage.繁体中文HK;
                case "en":
                    return MenuButtonMatchLanguage.英文;
                case "id":
                    return MenuButtonMatchLanguage.印尼;
                case "ms":
                    return MenuButtonMatchLanguage.马来;
                case "es":
                    return MenuButtonMatchLanguage.西班牙;
                case "ko":
                    return MenuButtonMatchLanguage.韩国;
                case "it":
                    return MenuButtonMatchLanguage.意大利;
                case "ja":
                    return MenuButtonMatchLanguage.日本;
                case "pl":
                    return MenuButtonMatchLanguage.波兰;
                case "pt":
                    return MenuButtonMatchLanguage.葡萄牙;
                case "ru":
                    return MenuButtonMatchLanguage.俄国;
                case "th":
                    return MenuButtonMatchLanguage.泰文;
                case "vi":
                    return MenuButtonMatchLanguage.越南;
                case "ar":
                    return MenuButtonMatchLanguage.阿拉伯语;
                case "hi":
                    return MenuButtonMatchLanguage.北印度;
                case "he":
                    return MenuButtonMatchLanguage.希伯来;
                case "tr":
                    return MenuButtonMatchLanguage.土耳其;
                case "de":
                    return MenuButtonMatchLanguage.德语;
                case "fr":
                    return MenuButtonMatchLanguage.法语;
                default:
                    throw new NotSupportedException();
            }
        }

        private static MenuButtonObject FillMenuItem(this JToken data)
        {
            MenuButtonObject result = null;
            var sub = data["sub_button"];
            try
            {
                if (sub["list"] != null)
                    sub = sub["list"];
            }
            catch { }
            var name = data["name"].ToString();
            var key = data["key"]?.ToString();
            if (sub == null || sub.Count() == 0)
            {
                var type = data["type"].ToString().ToMenuButtonType();
                switch (type)
                {
                    case MenuButtonType.Click:
                        return new MenuButtonClick(name, key);
                    case MenuButtonType.ScanWait:
                        return new MenuButtonScanWait(name, key);
                    case MenuButtonType.ScanPush:
                        return new MenuButtonScanPush(name, key);
                    case MenuButtonType.SysPhoto:
                        return new MenuButtonSysPhoto(name, key);
                    case MenuButtonType.PhotoOrAlbum:
                        return new MenuButtonPhotoOrAlbum(name, key);
                    case MenuButtonType.PicWeixin:
                        return new MenuButtonPicWeixin(name, key);
                    case MenuButtonType.LocationSelect:
                        return new MenuButtonLocationSelect(name, key);

                    case MenuButtonType.View:
                        return new MenuButtonView(name, data["url"].ToString());
                    case MenuButtonType.Media:
                        return new MenuButtonMedia(name, data["media_id"].ToString());
                    case MenuButtonType.ViewLimited:
                        return new MenuButtonViewLimited(name, data["media_id"].ToString());
                }
            }
            else
            {
                var top = new MenuButtonTop(name, new List<MenuButtonObject>());
                foreach (var item in sub)
                    top.SubButton.Add(FillMenuItem(item));
                result = top;
            }
            return result;
        }
        private static MenuButtonMatchRule FillMenuRule(this JToken data)
        {
            var rule = new MenuButtonMatchRule()
            {
                TagId = data["tag_id"]?.ToString().ConvertToInt() ?? 0
            };
            switch (data["sex"]?.ToString())
            {
                case "1":
                    rule.Sex = MenuButtonMatchSex.男;
                    break;
                case "2":
                    rule.Sex = MenuButtonMatchSex.女;
                    break;
            }
            rule.Country = data["country"]?.ToString();
            rule.Province = data["province"]?.ToString();
            rule.City = data["city"]?.ToString();
            switch (data["client_platform_type"]?.ToString())
            {
                case "1":
                    rule.ClientPlatformType = MenuButtonMatchPlatform.IOS;
                    break;
                case "2":
                    rule.ClientPlatformType = MenuButtonMatchPlatform.Android;
                    break;
                case "3":
                    rule.ClientPlatformType = MenuButtonMatchPlatform.Others;
                    break;
            }
            rule.Language = data["language"]?.ToString().ToMenuButtonMatchLanguage() ?? MenuButtonMatchLanguage.不匹配;
            return rule;
        }

        /// <summary>
        /// 根据返回的Json数据反序列化为实体菜单信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static MenuButtonInfo ParseToMenuButton(this JToken data)
        {
            var info = new MenuButtonInfo();
            var menu = data["menu"];
            if (menu == null)
                menu = data["button"];
            else
                menu = menu["button"];
            info.Button = new MenuButtonDefinition();
            foreach (var item in menu)
                info.Button.Button.Add(item.FillMenuItem());
            info.Button.MenuId = data["menuid"]?.ToString().ConvertToInt() ?? 0;

            var condition = data["conditionalmenu"];
            if (condition != null)
                foreach (var item in condition)
                {
                    var sub = new MenuButtonDefinition();
                    foreach (var subBtn in item["button"])
                        sub.Button.Add(subBtn.FillMenuItem());
                    sub.Rule = item["matchrule"].FillMenuRule();
                    sub.MenuId = item["menuid"].ToString().ConvertToInt();
                    info.ConditionalMenu.Add(sub);
                }
            return info;
        }
    }
}
