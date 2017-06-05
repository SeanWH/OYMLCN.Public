namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        public WeChatMessageText MessageText => new WeChatMessageText(this);

        /// <summary>
        /// 文本消息
        /// </summary>
        public class WeChatMessageText : WeChatMessageBase
        {
            /// <summary>
            /// 文本消息
            /// </summary>
            /// <param name="request"></param>
            public WeChatMessageText(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 文本消息内容
            /// </summary>
            public string Content => Request.Document.SelectValue("Content");
        }
    }
}
