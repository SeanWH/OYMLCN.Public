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
        /// <returns></returns>
        private WeChatResponse OnPushMessage()
        {
            switch (Request.EventType)
            {
                case WeChatRequestEventType.Push模板消息发送结果:
                    OnPush模板消息发送结果(Request, Request.Push模板消息发送结果);
                    break;
                case WeChatRequestEventType.Push群发结果:
                    OnPush群发结果(Request, Request.Push群发结果);
                    break;
                default:
                    var result = OnPushMessageMenu();
                    if (result != null)
                        return result;
                    return result;
            }
            return null;
        }

        /// <summary>
        /// 返回模板消息事件推送处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual void OnPush模板消息发送结果(WeChatRequest request, WeChatRequest.WeChatPush模板消息发送结果 msg) { }
        /// <summary>
        /// 返回群发结果事件推送处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual void OnPush群发结果(WeChatRequest request, WeChatRequest.WeChatPush群发结果 msg) { }
    }
}
