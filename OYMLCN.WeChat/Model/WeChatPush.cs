using OYMLCN.WeChat.Enum;

// 本文件放置微信推送的通知消息类型

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 模板消息事件推送
    /// </summary>
    public class WeChatPush模板消息 : WeChatEventMessageBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public override RequestMsgType MsgType => RequestMsgType.Event;
        /// <summary>
        /// 事件类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.Push模板消息发送结果;
        /// <summary>
        /// 状态报文
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success => Status?.ToLower().StartsWith("success") ?? false;
    }
}
