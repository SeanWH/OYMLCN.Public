using OYMLCN.WeChat.Model;
using OYMLCN.WeChat.Enum;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求集中处理入口
    /// </summary>
    public abstract partial class MessageHandler
    {
        /// <summary>
        /// 返回事件推送处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private WeChatResponseXmlDocument OnPushCustomerService(WeChatRequsetXmlDocument msg)
        {
            switch (msg.GetEventType())
            {
                case RequestEventType.Push客服接入会话:
                    return OnPushCustomerService接入会话(msg.ToPushCustomerService接入会话());
                case RequestEventType.Push客服关闭会话:
                    return OnPushCustomerService关闭会话(msg.ToPushCustomerService关闭会话());
                case RequestEventType.Push客服转接会话:
                    return OnPushCustomerService转接会话(msg.ToPushCustomerService转接会话());
            }
            return null;
        }

        /// <summary>
        /// 返回客服接入会话推送处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnPushCustomerService接入会话(WeChatPushCustomerService接入会话 msg) => DefaultResponseMessage(msg);
        /// <summary>
        /// 返回客服关闭会话推送处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnPushCustomerService关闭会话(WeChatPushCustomerService关闭会话 msg) => DefaultResponseMessage(msg);
        /// <summary>
        /// 返回客服转接会话推送处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnPushCustomerService转接会话(WeChatPushCustomerService转接会话 msg) => DefaultResponseMessage(msg);
    }
}
