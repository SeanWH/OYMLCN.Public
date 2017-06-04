namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {

        /// <summary>
        /// 用户关注事件
        /// </summary>
        public bool Event关注 => EventType == WeChatRequestEventType.Event关注;
        /// <summary>
        /// 用户取消关注事件
        /// </summary>
        public bool Event取消关注 => EventType == WeChatRequestEventType.Event取消关注;

        /// <summary>
        /// 扫描带参数二维码事件
        /// </summary>
        public WeChatEvent扫描带参数二维码 Event扫描带参数二维码 => new WeChatEvent扫描带参数二维码(this);

        /// <summary>
        /// 扫描带参数二维码事件
        /// </summary>
        public class WeChatEvent扫描带参数二维码 : WeChatMessageBase
        {
            /// <summary>
            /// 扫描带参数二维码事件
            /// </summary>
            /// <param name="request"></param>
            public WeChatEvent扫描带参数二维码(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id 
            /// </summary>
            public string EventKey => Request.Document.SelectValue("EventKey");
            /// <summary>
            /// 去除qrscene_的事件Key值
            /// </summary>
            public string SceneId => EventKey?.Replace("qrscene_", "");
            /// <summary>
            /// 二维码的ticket，可用来换取二维码图片 
            /// </summary>
            public string Ticket => Request.Document.SelectValue("Ticket");
        }
    }
}
