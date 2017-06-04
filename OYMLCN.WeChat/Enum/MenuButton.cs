namespace OYMLCN.WeChat.Enum
{
    /// <summary>
    /// 菜单按钮类型
    /// </summary>
    public enum MenuButtonType
    {
        /// <summary>
        /// 点击
        /// </summary>
        Click,
        /// <summary>
        /// Url
        /// </summary>
        View,
        /// <summary>
        /// 扫码推事件
        /// </summary>
        ScanPush,
        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框
        /// </summary>
        ScanWait,
        /// <summary>
        /// 弹出系统拍照发图
        /// </summary>
        SysPhoto,
        /// <summary>
        /// 弹出拍照或者相册发图
        /// </summary>
        PhotoOrAlbum,
        /// <summary>
        /// 弹出微信相册发图器
        /// </summary>
        PicWeixin,
        /// <summary>
        /// 弹出地理位置选择器
        /// </summary>
        LocationSelect,
        /// <summary>
        /// 下发媒体消息
        /// </summary>
        Media,
        /// <summary>
        /// 下发图文消息
        /// </summary>
        ViewLimited,
        /// <summary>
        /// 顶层菜单
        /// </summary>
        Top
    }


    /// <summary>
    /// 性别匹配规则
    /// </summary>
    public enum MenuButtonMatchSex
    {
        /// <summary>
        /// 不匹配
        /// </summary>
        不匹配 = 0,
        /// <summary>
        /// 男
        /// </summary>
        男 = 1,
        /// <summary>
        /// 女
        /// </summary>
        女 = 2
    }
    /// <summary>
    /// 平台匹配规则
    /// </summary>
    public enum MenuButtonMatchPlatform
    {
        /// <summary>
        /// 不匹配
        /// </summary>
        不匹配 = 0,
        /// <summary>
        /// IOS
        /// </summary>
        IOS = 1,
        /// <summary>
        /// Android
        /// </summary>
        Android = 2,
        /// <summary>
        /// Others
        /// </summary>
        Others = 3
    }
    /// <summary>
    /// 语言匹配规则
    /// </summary>
    public enum MenuButtonMatchLanguage
    {
        /// <summary>
        /// 不匹配
        /// </summary>
        不匹配 = 0,
        /// <summary>
        /// 简体中文 "zh_CN" 
        /// </summary>
        简体中文 = 1,
        /// <summary>
        /// 繁体中文TW "zh_TW" 
        /// </summary>
        繁体中文TW = 2,
        /// <summary>
        /// 繁体中文HK "zh_HK"
        /// </summary>
        繁体中文HK = 3,
        /// <summary>
        /// 英文 "en"
        /// </summary>
        英文 = 4,
        /// <summary>
        /// 印尼 "id" 
        /// </summary>
        印尼 = 5,
        /// <summary>
        /// 马来 "ms" 
        /// </summary>
        马来 = 6,
        /// <summary>
        /// 西班牙 "es" 
        /// </summary>
        西班牙 = 7,
        /// <summary>
        /// 韩国 "ko" 
        /// </summary>
        韩国 = 8,
        /// <summary>
        /// 意大利 "it" 
        /// </summary>
        意大利 = 9,
        /// <summary>
        /// 日本 "ja" 
        /// </summary>
        日本 = 10,
        /// <summary>
        /// 波兰 "pl" 
        /// </summary>
        波兰 = 11,
        /// <summary>
        /// 葡萄牙 "pt"
        /// </summary>
        葡萄牙 = 12,
        /// <summary>
        /// 俄国 "ru" 
        /// </summary>
        俄国 = 13,
        /// <summary>
        /// 泰文 "th" 
        /// </summary>
        泰文 = 14,
        /// <summary>
        /// 越南 "vi" 
        /// </summary>
        越南 = 15,
        /// <summary>
        /// 阿拉伯语 "ar" 
        /// </summary>
        阿拉伯语 = 16,
        /// <summary>
        /// 北印度 "hi" 
        /// </summary>
        北印度 = 17,
        /// <summary>
        /// 希伯来 "he" 
        /// </summary>
        希伯来 = 18,
        /// <summary>
        /// 土耳其 "tr" 
        /// </summary>
        土耳其 = 19,
        /// <summary>
        /// 德语 "de" 
        /// </summary>
        德语 = 20,
        /// <summary>
        /// 法语 "fr"
        /// </summary>
        法语 = 21
    }
}
