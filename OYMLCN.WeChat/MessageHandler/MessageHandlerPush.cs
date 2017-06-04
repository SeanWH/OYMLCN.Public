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
        private WeChatResponseXmlDocument OnPushMessage(WeChatRequsetXmlDocument msg)
        {
            switch (msg.GetEventType())
            {
                case RequestEventType.Push模板消息发送结果:
                    OnPush模板消息发送结果(msg.ToPush模板消息());
                    break;
                case RequestEventType.Push群发结果:
                    OnPush群发结果(msg.ToPush群发消息());
                    break;
                default:
                    var result = OnPushMessageMenu(msg);
                    if (result != null)
                        return result;
                    result = OnPushCustomerService(msg);
                    return result;
            }
            return null;
        }

        /// <summary>
        /// 返回模板消息事件推送处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual void OnPush模板消息发送结果(WeChatPush模板消息 msg) { }
        /// <summary>
        /// 返回群发结果事件推送处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual void OnPush群发结果(WeChatPush群发消息 msg) { }
    }
}
