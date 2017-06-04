using OYMLCN.WeChat.Enum;

// 本文件放置微信推送的通知消息类型

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 客服接入会话事件推送
    /// </summary>
    public class WeChatPushCustomerService接入会话 : WeChatEventMessageBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.Push客服接入会话;
        /// <summary>
        /// 客服账号
        /// </summary>
        public string KFAccount { get; set; }
    }

    /// <summary>
    /// 客服关闭会话事件推送
    /// </summary>
    public class WeChatPushCustomerService关闭会话 : WeChatPushCustomerService接入会话
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.Push客服关闭会话;
    }

    /// <summary>
    /// 客服转接会话事件推送
    /// </summary>
    public class WeChatPushCustomerService转接会话 : WeChatEventMessageBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.Push客服转接会话;
        /// <summary>
        /// 发起账号
        /// </summary>
        public string FromKfAccount { get; set; }
        /// <summary>
        /// 目标账号
        /// </summary>
        public string ToKfAccount { get; set; }
    }
}
