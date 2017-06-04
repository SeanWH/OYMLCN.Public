namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        /// <summary>
        /// 点击自定义菜单事件
        /// </summary>
        public WeChatEvent点击自定义菜单 Event点击自定义菜单 => new WeChatEvent点击自定义菜单(this);

        /// <summary>
        /// 点击自定义菜单事件
        /// </summary>
        public class WeChatEvent点击自定义菜单 : WeChatMessageBase
        {
            /// <summary>
            /// 点击自定义菜单事件
            /// </summary>
            /// <param name="request"></param>
            public WeChatEvent点击自定义菜单(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 事件KEY值，与自定义菜单接口中KEY值对应
            /// </summary>
            public string EventKey => Request.Document.SelectValue("EventKey");
        }
    }
}
