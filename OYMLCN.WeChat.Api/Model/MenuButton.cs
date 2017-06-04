using OYMLCN.WeChat.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 本文件放置自定义菜单的定义

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 自定义菜单基类
    /// </summary>
    public abstract class MenuBase
    {
        /// <summary>
        /// 按钮类型
        /// </summary>
        public virtual MenuType Type { get; }

        /// <summary>
        /// 菜单标题，不超过16个字节，子菜单不超过60个字节
        /// </summary>
        protected string Name { get; set; }

        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        protected string Key { get; set; }

        /// <summary>
        /// 必须重写 转换为Json格式
        /// </summary>
        /// <returns></returns>
        public abstract string ToJson();
        /// <summary>
        /// 覆盖默认方法为ToJson()
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format("{0},", ToJson());
    }

    /// <summary>
    /// 顶层菜单（带有子菜单）
    /// </summary>
    public class MenuTop : MenuBase
    {
        /// <summary>
        /// 顶层菜单（带有子菜单）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="btn1"></param>
        /// <param name="btn2"></param>
        /// <param name="btn3"></param>
        /// <param name="btn4"></param>
        /// <param name="btn5"></param>
        public MenuTop(string name, MenuBase btn1, MenuBase btn2 = null, MenuBase btn3 = null, MenuBase btn4 = null, MenuBase btn5 = null)
        {
            Name = name;
            SubButton = new List<MenuBase>
            {
                btn1,
                btn2,
                btn3,
                btn4,
                btn5
            };
        }
        /// <summary>
        /// 顶层菜单（带有子菜单）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="buttons"></param>
        public MenuTop(string name, List<MenuBase> buttons)
        {
            Name = name;
            SubButton = buttons;
        }

        /// <summary>
        /// 二级菜单数组，个数应为1~5个
        /// </summary>
        List<MenuBase> SubButton { get; set; }
        /// <summary>
        /// 按钮类型
        /// </summary>
        public override MenuType Type => MenuType.Top;

        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns></returns>
        public override string ToJson()
        {
            StringBuilder str = new StringBuilder();
            str.Append("{").AppendFormat("\"name\":\"{0}\",\"sub_button\":[", Name);
            StringBuilder sub = new StringBuilder();
            foreach (var item in SubButton.Take(5))
                if (item != null)
                    sub.Append(item.ToString());
            str.Append(sub.ToString().Remove(sub.Length - 1)).Append("]}");
            return str.ToString();
        }
    }

    /// <summary>
    /// 点击推事件用户点击click类型按钮后，
    /// 微信服务器会通过消息接口推送消息类型为event的结构给开发者
    /// （参考消息接口指南），并且带上按钮中开发者填写的key值，
    /// 开发者可以通过自定义的key值与用户进行交互
    /// </summary>
    public class MenuClick : MenuBase
    {
        /// <summary>
        /// 点击推送事件按钮
        /// </summary>
        /// <param name="name">菜单标题，不超过16个字节，子菜单不超过60个字节</param>
        /// <param name="key">菜单KEY值，用于消息接口推送，不超过128字节</param>
        public MenuClick(string name, string key)
        {
            Name = name;
            Key = key;
        }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public override MenuType Type => MenuType.Click;
        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns></returns>
        public override string ToJson() => "{\"type\":\"click\",\"name\":\"" + Name + "\",\"key\":\"" + Key + "\"}";
    }

    /// <summary>
    /// 跳转URL用户点击view类型按钮后，
    /// 微信客户端将会打开开发者在按钮中填写的网页URL，
    /// 可与网页授权获取用户基本信息接口结合，获得用户基本信息。
    /// </summary>
    public class MenuView : MenuBase
    {
        /// <summary>
        /// 点击打开网页按钮
        /// </summary>
        /// <param name="name">菜单标题，不超过16个字节，子菜单不超过60个字节</param>
        /// <param name="url">网页链接，用户点击菜单可打开链接，不超过1024字节</param>
        public MenuView(string name, string url)
        {
            Name = name;
            Url = url;
        }

        /// <summary>
        /// 网页链接，用户点击菜单可打开链接，不超过1024字节
        /// </summary>
        string Url { get; set; }
        /// <summary>
        /// 按钮类型
        /// </summary>
        public override MenuType Type => MenuType.View;
        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns></returns>
        public override string ToJson() => "{\"type\":\"view\",\"name\":\"" + Name + "\",\"url\":\"" + Url + Key + "\"}";
    }

    /// <summary>
    /// 扫码推事件且弹出“消息接收中”提示框用户点击按钮后，
    /// 微信客户端将调起扫一扫工具，完成扫码操作后，将扫码的结果传给开发者，
    /// 同时收起扫一扫工具，然后弹出“消息接收中”提示框，
    /// 随后可能会收到开发者下发的消息。
    /// </summary>
    public class MenuScanWait : MenuBase
    {
        /// <summary>
        /// 扫码并提示处理中按钮
        /// </summary>
        /// <param name="name">菜单标题，不超过16个字节，子菜单不超过60个字节</param>
        /// <param name="key">菜单KEY值，用于消息接口推送，不超过128字节</param>
        public MenuScanWait(string name, string key)
        {
            Name = name;
            Key = key;
        }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public override MenuType Type => MenuType.ScanWait;
        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns></returns>
        public override string ToJson() => "{\"type\":\"scancode_waitmsg\",\"name\":\"" + Name + "\",\"key\":\"" + Key + "\"}";
    }

    /// <summary>
    /// 扫码推事件用户点击按钮后，微信客户端将调起扫一扫工具，
    /// 完成扫码操作后显示扫描结果（如果是URL，将进入URL），
    /// 且会将扫码的结果传给开发者，开发者可以下发消息。
    /// </summary>
    public class MenuScanPush : MenuBase
    {
        /// <summary>
        /// 扫码后回传结果
        /// </summary>
        /// <param name="name">菜单标题，不超过16个字节，子菜单不超过60个字节</param>
        /// <param name="key">菜单KEY值，用于消息接口推送，不超过128字节</param>
        public MenuScanPush(string name, string key)
        {
            Name = name;
            Key = key;
        }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public override MenuType Type => MenuType.ScanPush;
        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns></returns>
        public override string ToJson() => "{\"type\":\"scancode_push\",\"name\":\"" + Name + "\",\"key\":\"" + Key + "\"}";
    }

    /// <summary>
    /// 弹出系统拍照发图用户点击按钮后，微信客户端将调起系统相机，
    /// 完成拍照操作后，会将拍摄的相片发送给开发者，并推送事件给开发者，
    /// 同时收起系统相机，随后可能会收到开发者下发的消息。
    /// </summary>
    public class MenuSysPhoto : MenuBase
    {
        /// <summary>
        /// 系统拍照发图
        /// </summary>
        /// <param name="name">菜单标题，不超过16个字节，子菜单不超过60个字节</param>
        /// <param name="key">菜单KEY值，用于消息接口推送，不超过128字节</param>
        public MenuSysPhoto(string name, string key)
        {
            Name = name;
            Key = key;
        }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public override MenuType Type => MenuType.SysPhoto;
        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns></returns>
        public override string ToJson() => "{\"type\":\"pic_sysphoto\",\"name\":\"" + Name + "\",\"key\":\"" + Key + "\"}";
    }

    /// <summary>
    /// 弹出拍照或者相册发图用户点击按钮后，
    /// 微信客户端将弹出选择器供用户选择“拍照”或者“从手机相册选择”。
    /// 用户选择后即走其他两种流程。
    /// </summary>
    public class MenuPhotoOrAlbum : MenuBase
    {
        /// <summary>
        /// 拍照或者相册发图
        /// </summary>
        /// <param name="name">菜单标题，不超过16个字节，子菜单不超过60个字节</param>
        /// <param name="key">菜单KEY值，用于消息接口推送，不超过128字节</param>
        public MenuPhotoOrAlbum(string name, string key)
        {
            Name = name;
            Key = key;
        }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public override MenuType Type => MenuType.PhotoOrAlbum;
        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns></returns>
        public override string ToJson() => "{\"type\":\"pic_photo_or_album\",\"name\":\"" + Name + "\",\"key\":\"" + Key + "\"}";
    }

    /// <summary>
    /// 弹出微信相册发图器用户点击按钮后，微信客户端将调起微信相册，
    /// 完成选择操作后，将选择的相片发送给开发者的服务器，并推送事件给开发者，
    /// 同时收起相册，随后可能会收到开发者下发的消息。
    /// </summary>
    public class MenuPicWeixin : MenuBase
    {
        /// <summary>
        /// 微信相册发图
        /// </summary>
        /// <param name="name">菜单标题，不超过16个字节，子菜单不超过60个字节</param>
        /// <param name="key">菜单KEY值，用于消息接口推送，不超过128字节</param>
        public MenuPicWeixin(string name, string key)
        {
            Name = name;
            Key = key;
        }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public override MenuType Type => MenuType.PicWeixin;
        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns></returns>
        public override string ToJson() => "{\"type\":\"pic_weixin\",\"name\":\"" + Name + "\",\"key\":\"" + Key + "\"}";
    }

    /// <summary>
    /// 弹出地理位置选择器用户点击按钮后，微信客户端将调起地理位置选择工具，
    /// 完成选择操作后，将选择的地理位置发送给开发者的服务器，
    /// 同时收起位置选择工具，随后可能会收到开发者下发的消息。
    /// </summary>
    public class MenuLocationSelect : MenuBase
    {
        /// <summary>
        /// 发送位置
        /// </summary>
        /// <param name="name">菜单标题，不超过16个字节，子菜单不超过60个字节</param>
        /// <param name="key">菜单KEY值，用于消息接口推送，不超过128字节</param>
        public MenuLocationSelect(string name, string key)
        {
            Name = name;
            Key = key;
        }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public override MenuType Type => MenuType.LocationSelect;
        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns></returns>
        public override string ToJson() => "{\"type\":\"location_select\",\"name\":\"" + Name + "\",\"key\":\"" + Key + "\"}";
    }

    /// <summary>
    /// 下发消息（除文本消息）用户点击media_id类型按钮后，
    /// 微信服务器会将开发者填写的永久素材id对应的素材下发给用户，
    /// 永久素材类型可以是图片、音频、视频、图文消息。
    /// 请注意：永久素材id必须是在“素材管理/新增永久素材”接口上传后获得的合法id。
    /// </summary>
    public class MenuMedia : MenuBase
    {
        /// <summary>
        /// 发送媒体（功能受限，无推送）
        /// </summary>
        /// <param name="name">菜单标题，不超过16个字节，子菜单不超过60个字节</param>
        /// <param name="mediaId">调用新增永久素材接口返回的合法media_id</param>
        public MenuMedia(string name, string mediaId)
        {
            Name = name;
            MediaId = mediaId;
        }
        /// <summary>
        /// 调用新增永久素材接口返回的合法media_id
        /// </summary>
        public string MediaId { get; private set; }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public override MenuType Type => MenuType.Media;
        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns></returns>
        public override string ToJson() => "{\"type\":\"media_id\",\"name\":\"" + Name + "\",\"media_id\":\"" + MediaId + Key + "\"}";
    }

    /// <summary>
    /// 跳转图文消息URL用户点击view_limited类型按钮后，
    /// 微信客户端将打开开发者在按钮中填写的永久素材id对应的图文消息URL，
    /// 永久素材类型只支持图文消息。
    /// 请注意：永久素材id必须是在“素材管理/新增永久素材”接口上传后获得的合法id。
    /// </summary>
    public class MenuViewLimited : MenuBase
    {
        /// <summary>
        /// 发送图文消息（功能受限，无推送）
        /// </summary>
        /// <param name="name">菜单标题，不超过16个字节，子菜单不超过60个字节</param>
        /// <param name="mediaId">调用新增永久素材接口返回的合法media_id</param>
        public MenuViewLimited(string name, string mediaId)
        {
            Name = name;
            MediaId = mediaId;
        }
        /// <summary>
        /// 调用新增永久素材接口返回的合法media_id
        /// </summary>
        string MediaId { get; set; }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public override MenuType Type => MenuType.ViewLimited;
        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns></returns>
        public override string ToJson() => "{\"type\":\"view_limited\",\"name\":\"" + Name + "\",\"media_id\":\"" + MediaId + Key + "\"}";
    }
    /// <summary>
    /// 小程序
    /// </summary>
    public class MenuMiniProgram : MenuBase
    {
        /// <summary>
        /// 小程序
        /// </summary>
        public override MenuType Type => MenuType.MiniProgram;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="appId">小程序的appid</param>
        /// <param name="pagePath">小程序的页面路径</param>
        /// <param name="url">不支持小程序的老版本客户端将打开本url</param>
        public MenuMiniProgram(string name, string appId, string pagePath, string url = "http://mp.weixin.qq.com")
        {
            Name = name;
            AppId = appId;
            PagePath = pagePath;
            Url = url;
        }
        /// <summary>
        /// 不支持小程序的老版本客户端将打开本url
        /// </summary>
        string Url { get; set; }
        /// <summary>
        /// 小程序的appid（仅认证公众号可配置）
        /// </summary>
        string AppId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string PagePath { get; set; }
        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns></returns>
        public override string ToJson() => "{\"type\":\"miniprogram\",\"name\":\"" + Name + "\",\"url\":\"" + Url + "\",\"appid\":\"" + AppId + "\",\"pagepath\":\"" + PagePath + "\"}";
    }
    /// <summary>
    /// 个性化菜单匹配规则
    /// 共六个字段，均可为空，但不能全部为空，至少要有一个匹配信息是不为空的。
    /// </summary>
    public class MenuMatchRule
    {
        /// <summary>
        /// 用户标签的id，可通过用户标签管理接口获取
        /// </summary>
        public int TagId { get; set; }
        /// <summary>
        /// 性别：男（1）女（2），不填则不做匹配
        /// </summary>
        public MenuMatchSex Sex { get; set; }
        /// <summary>
        /// 客户端版本，当前只具体到系统型号：IOS(1), Android(2),Others(3)，不填则不做匹配
        /// </summary>
        public MenuMatchPlatform ClientPlatformType { get; set; }
        /// <summary>
        /// 国家信息，是用户在微信中设置的地区，具体请参考地区信息表
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 省份信息，是用户在微信中设置的地区，具体请参考地区信息表
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市信息，是用户在微信中设置的地区，具体请参考地区信息表
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 语言信息，是用户在微信中设置的语言
        /// </summary>
        public MenuMatchLanguage Language { get; set; }
        /// <summary>
        /// 重写ToString以输出Json
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string language = string.Empty;
            switch (Language)
            {
                case MenuMatchLanguage.简体中文:
                    language = "zh_CN";
                    break;
                case MenuMatchLanguage.繁体中文TW:
                    language = "zh_TW";
                    break;
                case MenuMatchLanguage.繁体中文HK:
                    language = "zh_HK";
                    break;
                case MenuMatchLanguage.英文:
                    language = "en";
                    break;
                case MenuMatchLanguage.印尼:
                    language = "id";
                    break;
                case MenuMatchLanguage.马来:
                    language = "ms";
                    break;
                case MenuMatchLanguage.西班牙:
                    language = "es";
                    break;
                case MenuMatchLanguage.韩国:
                    language = "ko";
                    break;
                case MenuMatchLanguage.意大利:
                    language = "it";
                    break;
                case MenuMatchLanguage.日本:
                    language = "ja";
                    break;
                case MenuMatchLanguage.波兰:
                    language = "pl";
                    break;
                case MenuMatchLanguage.葡萄牙:
                    language = "pt";
                    break;
                case MenuMatchLanguage.俄国:
                    language = "ru";
                    break;
                case MenuMatchLanguage.泰文:
                    language = "th";
                    break;
                case MenuMatchLanguage.越南:
                    language = "vi";
                    break;
                case MenuMatchLanguage.阿拉伯语:
                    language = "ar";
                    break;
                case MenuMatchLanguage.北印度:
                    language = "hi";
                    break;
                case MenuMatchLanguage.希伯来:
                    language = "he";
                    break;
                case MenuMatchLanguage.土耳其:
                    language = "tr";
                    break;
                case MenuMatchLanguage.德语:
                    language = "de";
                    break;
                case MenuMatchLanguage.法语:
                    language = "fr";
                    break;
                case MenuMatchLanguage.不匹配:
                default:
                    language = "";
                    break;
            }
            return "\"matchrule\":{\"tag_id\":\"" + (TagId == 0 ? "" : TagId.ToString()) + "\",\"sex\":\"" +
                (Sex == MenuMatchSex.不匹配 ? "" : ((int)Sex).ToString()) +
                "\",\"country\":\"" + Country + "\",\"province\":\"" + Province +
                "\",\"city\":\"" + City + "\",\"client_platform_type\":\"" +
                (ClientPlatformType == MenuMatchPlatform.不匹配 ? "" : ((int)ClientPlatformType).ToString()) +
                "\",\"language\":\"" + language + "\"}";
        }
    }
}
