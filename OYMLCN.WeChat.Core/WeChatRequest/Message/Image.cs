namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        /// <summary>
        /// 图片消息
        /// </summary>
        public WeChatMessageImage MessageImage => new WeChatMessageImage(this);

        /// <summary>
        /// 图片消息
        /// </summary>
        public class WeChatMessageImage : WeChatMessageBase
        {
            /// <summary>
            /// 图片消息
            /// </summary>
            /// <param name="request"></param>
            public WeChatMessageImage(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 图片链接
            /// </summary>
            public string PicUrl => Request.Document.SelectValue("PicUrl");
            /// <summary>
            /// 图片消息媒体id
            /// </summary>
            public string MediaId => Request.Document.SelectValue("MediaId");
        }
    }
}
