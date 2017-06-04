namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        /// <summary>
        /// 模板消息事件推送
        /// </summary>
        public WeChatPush模板消息发送结果 Push模板消息发送结果 => new WeChatPush模板消息发送结果(this);

        /// <summary>
        /// 模板消息事件推送
        /// </summary>
        public class WeChatPush模板消息发送结果 : WeChatMessageBase
        {
            /// <summary>
            /// 模板消息事件推送
            /// </summary>
            /// <param name="request"></param>
            public WeChatPush模板消息发送结果(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 状态报文
            /// </summary>
            public string Status => Request.Document.SelectValue("Status");
            /// <summary>
            /// 是否成功
            /// </summary>
            public bool Success => Status?.ToLower().StartsWith("success") ?? false;
        }
    }
}
