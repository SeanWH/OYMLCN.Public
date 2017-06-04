using OYMLCN.WeChat.Enum;

// 本文件放置微信消息请求的模型

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 微信事件消息基类
    /// </summary>
    public class WeChatEventMessageBase : WeChatMessageBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public override RequestMsgType MsgType => RequestMsgType.Event;

        /// <summary>
        /// 事件类型
        /// </summary>
        public virtual RequestEventType Event { get; set; }
    }


    /// <summary>
    /// 扫描带参数二维码事件
    /// </summary>
    public class WeChatEventMessage扫描带参数二维码 : WeChatEventMessageBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.扫描带参数二维码;

        /// <summary>
        /// 事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id 
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 去除qrscene_的事件Key值
        /// </summary>
        public string SceneId => EventKey.ToLower().Replace("qrscene_", "");
        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片 
        /// </summary>
        public string Ticket { get; set; }
    }

    /// <summary>
    /// 上报地理位置事件
    /// </summary>
    public class WeChatEventMessage上报地理位置 : WeChatEventMessageBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.上报地理位置;
        /// <summary>
        /// 地理位置纬度 
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 地理位置经度 
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 地理位置精度 
        /// </summary>
        public double Precision { get; set; }
    }

    /// <summary>
    /// 自定义菜单事件
    /// </summary>
    public class WeChatEventMessage点击自定义菜单 : WeChatEventMessageBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.点击自定义菜单;
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
    }

    /// <summary>
    /// 点击菜单跳转链接事件
    /// </summary>
    public class WeChatEventMessage点击菜单跳转链接 : WeChatEventMessageBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.点击菜单跳转链接;
        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 指菜单ID，如果是个性化菜单，则可以通过这个字段，知道是哪个规则的菜单被点击了。
        /// </summary>
        public int MenuId { get; set; }
    }
}
