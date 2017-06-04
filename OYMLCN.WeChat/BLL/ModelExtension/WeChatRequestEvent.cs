using OYMLCN.WeChat.Model;
using OYMLCN.WeChat.Enum;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// Extension
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 将事件类型字符串转换为对应的事件类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static RequestEventType ToRequestEventType(this string str)
        {
            switch (str.ToLower())
            {
                #region 基础事件
                case "subscribe":
                    return RequestEventType.关注;
                case "unsubscribe":
                    return RequestEventType.取消关注;
                case "scan":
                    return RequestEventType.扫描带参数二维码;
                case "location":
                    return RequestEventType.上报地理位置;
                case "click":
                    return RequestEventType.点击自定义菜单;
                case "view":
                    return RequestEventType.点击菜单跳转链接;
                #endregion

                case "templatesendjobfinish":
                    return RequestEventType.Push模板消息发送结果;
                case "masssendjobfinish":
                    return RequestEventType.Push群发结果;

                #region 自定义菜单推送
                case "scancode_push":
                    return RequestEventType.Push扫码推事件;
                case "scancode_waitmsg":
                    return RequestEventType.Push扫码推等待事件;
                case "pic_sysphoto":
                    return RequestEventType.Push系统拍照发图;
                case "pic_photo_or_album":
                    return RequestEventType.Push拍照或者相册发图;
                case "pic_weixin":
                    return RequestEventType.Push微信相册发图;
                case "location_select":
                    return RequestEventType.Push位置选择;
                #endregion

                case "kf_create_session":
                    return RequestEventType.Push客服接入会话;
                case "kf_close_session":
                    return RequestEventType.Push客服关闭会话;
                case "kf_switch_session":
                    return RequestEventType.Push客服接入会话;

                default:
                    return RequestEventType.未知;
            }
        }

        /// <summary>
        /// 反序列化Xml数据为微信事件消息
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatEventMessageBase ToEventMessage(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            return new WeChatEventMessageBase()
            {
                Event = dom.SelectValue("Event").ToRequestEventType()
            }.FillByDom(xdoc);
        }
        /// <summary>
        /// 反序列化Xml数据为扫描带参数二维码事件消息
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatEventMessage扫描带参数二维码 ToEventMessage扫描带参数二维码(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            return new WeChatEventMessage扫描带参数二维码()
            {
                EventKey = dom.SelectValue("EventKey"),
                Ticket = dom.SelectValue("Ticket")
            }.FillByDom(xdoc);
        }
        /// <summary>
        /// 反序列化Xml数据为上报地理位置事件消息
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatEventMessage上报地理位置 ToEventMessage上报地理位置(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            return new WeChatEventMessage上报地理位置()
            {
                Latitude = dom.SelectValue("Latitude").ConvertToDouble(),
                Longitude = dom.SelectValue("Longitude").ConvertToDouble(),
                Precision = dom.SelectValue("Precision").ConvertToDouble()
            }.FillByDom(xdoc);
        }
        /// <summary>
        /// 反序列化Xml数据为自定义菜单事件消息
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatEventMessage点击自定义菜单 ToEventMessage点击自定义菜单(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            return new WeChatEventMessage点击自定义菜单()
            {
                EventKey = dom.SelectValue("EventKey")
            }.FillByDom(xdoc);
        }
        /// <summary>
        /// 反序列化Xml数据为点击菜单跳转链接事件消息
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatEventMessage点击菜单跳转链接 ToEventMessage点击菜单跳转链接(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            return new WeChatEventMessage点击菜单跳转链接()
            {
                EventKey = dom.SelectValue("EventKey"),
                MenuId = dom.SelectValue("MenuId").ConvertToInt()
            }.FillByDom(xdoc);
        }
    }
}
