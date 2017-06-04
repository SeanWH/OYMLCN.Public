

namespace OYMLCN.WeChat.Enum
{
    /// <summary>
    /// 微信客户端版本
    /// </summary>
    public enum WeChatBrowserType
    {
        /// <summary>
        /// 非微信客户端
        /// </summary>
        None,
        /// <summary>
        /// iPhone
        /// </summary>
        iPhone,
        /// <summary>
        /// iPad
        /// </summary>
        iPad,
        /// <summary>
        /// Android
        /// </summary>
        Android,
        /// <summary>
        /// Windows
        /// </summary>
        Windows
    }


    /// <summary>
    /// 语言
    /// </summary>
    public enum Language
    {
        /// <summary>
        /// 中文简体
        /// </summary>
        zh_CN,
        /// <summary>
        /// 中文繁体
        /// </summary>
        zh_TW,
        /// <summary>
        /// 英文
        /// </summary>
        en
    }

    /// <summary>
    /// 接收消息类型
    /// </summary>
    public enum RequestMsgType
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text,
        /// <summary>
        /// 地理位置
        /// </summary>
        Location,
        /// <summary>
        /// 图片
        /// </summary>
        Image,
        /// <summary>
        /// 语音
        /// </summary>
        Voice,
        /// <summary>
        /// 视频
        /// </summary>
        Video,
        /// <summary>
        /// 连接信息
        /// </summary>
        Link,
        /// <summary>
        /// 小视频
        /// </summary>
        ShortVideo,
        /// <summary>
        /// 事件推送
        /// </summary>
        Event,
        /// <summary>
        /// 未被支持的事件类型
        /// </summary>
        Unknow = -1
    }

    /// <summary>
    /// 当RequestMsgType类型为Event时，Event属性的类型
    /// 由于事件类型较多 所以以中文标识
    /// </summary>
    public enum RequestEventType
    {
        /// <summary>
        /// 未被支持的事件消息类型
        /// </summary>
        未知 = 0,
        /// <summary>
        /// 关注 事件
        /// </summary>
        关注,
        /// <summary>
        /// 取消关注 事件
        /// </summary>
        取消关注,
        /// <summary>
        /// 扫描带参数二维码 事件
        /// </summary>
        扫描带参数二维码,
        /// <summary>
        /// 上报地理位置 事件
        /// </summary>
        上报地理位置,
        /// <summary>
        /// 点击自定义菜单 事件
        /// </summary>
        点击自定义菜单,
        /// <summary>
        /// 点击菜单跳转链接 事件
        /// </summary>
        点击菜单跳转链接,

        /// <summary>
        /// 模板消息发送结果 事件推送
        /// </summary>
        Push模板消息发送结果,
        /// <summary>
        /// 群发结果 事件推送
        /// </summary>
        Push群发结果,

        /// <summary>
        /// 扫码推事件 事件推送
        /// </summary>
        Push扫码推事件,
        /// <summary>
        /// 扫码推等待事件 事件推送
        /// </summary>
        Push扫码推等待事件,
        /// <summary>
        /// 系统拍照发图 事件推送
        /// </summary>
        Push系统拍照发图,
        /// <summary>
        /// 拍照或者相册发图 事件推送
        /// </summary>
        Push拍照或者相册发图,
        /// <summary>
        /// 微信相册发图 事件推送
        /// </summary>
        Push微信相册发图,
        /// <summary>
        /// 位置选择 事件推送
        /// </summary>
        Push位置选择,

        /// <summary>
        /// 客服接入会话 事件推送
        /// </summary>
        Push客服接入会话,
        /// <summary>
        /// 客服关闭会话 事件推送
        /// </summary>
        Push客服关闭会话,
        /// <summary>
        /// 客服转接会话 事件推送
        /// </summary>
        Push客服转接会话


    }


    /// <summary>
    /// 发送消息类型
    /// </summary>
    public enum ResponseMsgType
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text = 0,
        /// <summary>
        /// 单图文
        /// </summary>
        News = 1,
        /// <summary>
        /// 音乐
        /// </summary>
        Music = 2,
        /// <summary>
        /// 图片
        /// </summary>
        Image = 3,
        /// <summary>
        /// 语音
        /// </summary>
        Voice = 4,
        /// <summary>
        /// 视频
        /// </summary>
        Video = 5,
        /// <summary>
        /// 转接消息到客服平台
        /// </summary>
        TransferToCustomerService = 6
    }

    /// <summary>
    /// 关键字对比方式
    /// </summary>
    public enum HandlerContrast
    {
        /// <summary>
        /// 完全相等
        /// </summary>
        Equal,
        /// <summary>
        /// 包含关键字
        /// </summary>
        Contain,
        /// <summary>
        /// 不区分大小写
        /// </summary>
        Superficial,
        /// <summary>
        /// 包含关键字不区分大小写
        /// </summary>
        ContainSuperficial
    }
}
