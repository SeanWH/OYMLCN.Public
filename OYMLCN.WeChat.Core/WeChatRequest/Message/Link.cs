namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        /// <summary>
        /// 链接消息
        /// </summary>
        public WeChatMessageLink MessageLink => new WeChatMessageLink(this);

        /// <summary>
        /// 链接消息
        /// </summary>
        public class WeChatMessageLink : WeChatMessageBase
        {
            /// <summary>
            /// 链接消息
            /// </summary>
            /// <param name="request"></param>
            public WeChatMessageLink(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 消息标题
            /// </summary>
            public string Title => Request.Document.SelectValue("Title");
            /// <summary>
            /// 消息描述
            /// </summary>
            public string Description => Request.Document.SelectValue("Description");
            /// <summary>
            /// 消息链接
            /// </summary>
            public string Url => Request.Document.SelectValue("Url");
        }
    }
}
