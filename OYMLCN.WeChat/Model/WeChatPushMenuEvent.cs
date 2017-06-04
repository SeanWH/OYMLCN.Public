using OYMLCN.WeChat.Enum;

// 本文件放置自定义菜单事件推送的请求模型

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 微信自定义菜单事件基类
    /// </summary>
    public abstract class WeChatPushMenuEventBase : WeChatEventMessageBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override abstract RequestEventType Event { get; }
        /// <summary>
        /// 事件KEY值，由开发者在创建菜单时设定
        /// </summary>
        public string EventKey { get; set; }
    }

    /// <summary>
    /// 扫码推事件的事件推送
    /// </summary>
    public class WeChatPushMenu扫码推事件 : WeChatPushMenuEventBase
    {
        /// <summary>
        /// 扫描类型，一般是qrcode
        /// </summary>
        public string ScanType { get; set; }
        /// <summary>
        /// 扫描结果，即二维码对应的字符串信息
        /// </summary>
        public string ScanResult { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.Push扫码推事件;
    }

    /// <summary>
    /// 扫码推事件且弹出“消息接收中”提示框的事件推送
    /// </summary>
    public class WeChatPushMenu扫码推等待事件 : WeChatPushMenu扫码推事件
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.Push扫码推等待事件;
    }

    /// <summary>
    /// 弹出系统拍照发图的事件推送
    /// </summary>
    public class WeChatPushMenu系统拍照发图 : WeChatPushMenuEventBase
    {
        /// <summary>
        /// 发送的图片数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 图片的MD5值，开发者若需要，可用于验证接收到图片
        /// </summary>
        public string[] PicMd5Sum { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.Push系统拍照发图;
    }

    /// <summary>
    /// 弹出拍照或者相册发图的事件推送
    /// </summary>
    public class WeChatPushMenu拍照或者相册发图 : WeChatPushMenu系统拍照发图
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.Push拍照或者相册发图;
    }

    /// <summary>
    /// 弹出微信相册发图器的事件推送
    /// </summary>
    public class WeChatPushMenu微信相册发图 : WeChatPushMenu系统拍照发图
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.Push微信相册发图;
    }

    /// <summary>
    /// 弹出地理位置选择器的事件推送
    /// </summary>
    public class WeChatPushMenu位置选择 : WeChatPushMenuEventBase
    {
        /// <summary>
        /// X坐标信息
        /// </summary>
        public string Location_X { get; set; }
        /// <summary>
        /// Y坐标信息
        /// </summary>
        public string Location_Y { get; set; }
        /// <summary>
        /// 精度，可理解为精度或者比例尺、越精细的话 scale越高
        /// </summary>
        public string Scale { get; set; }
        /// <summary>
        /// 地理位置的字符串信息
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 朋友圈POI的名字，可能为空
        /// </summary>
        public string Poiname { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.Push位置选择;
    }
}
