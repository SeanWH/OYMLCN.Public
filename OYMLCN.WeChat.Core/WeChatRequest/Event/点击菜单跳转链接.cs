namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        /// <summary>
        /// 点击菜单跳转链接事件
        /// </summary>
        public WeChatEvent点击菜单跳转链接 Event点击菜单跳转链接 => new WeChatEvent点击菜单跳转链接(this);

        /// <summary>
        /// 点击菜单跳转链接事件
        /// </summary>
        public class WeChatEvent点击菜单跳转链接 : WeChatMessageBase
        {
            /// <summary>
            /// 点击菜单跳转链接事件
            /// </summary>
            /// <param name="request"></param>
            public WeChatEvent点击菜单跳转链接(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 事件KEY值，设置的跳转URL
            /// </summary>
            public string Url => Request.Document.SelectValue("EventKey");

            /// <summary>
            /// 指菜单ID，如果是个性化菜单，则可以通过这个字段，知道是哪个规则的菜单被点击了。
            /// </summary>
            public int MenuId => Request.Document.SelectValue("MenuId").ConvertToInt();
        }
    }
}
