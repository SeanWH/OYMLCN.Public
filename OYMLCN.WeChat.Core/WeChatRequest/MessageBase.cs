using System;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        /// <summary>
        /// 微信请求
        /// </summary>
        public class WeChatMessageBase
        {
            /// <summary>
            /// 微信请求体
            /// </summary>
            protected WeChatRequest Request { get; private set; }
            /// <summary>
            /// 微信请求
            /// </summary>
            /// <param name="request"></param>
            public WeChatMessageBase(WeChatRequest request) => Request = request;


            /// <summary>
            /// 开发者微信号
            /// </summary>
            public string ToUserName => Request.Document.SelectValue("ToUserName");
            /// <summary>
            /// 发送方帐号（一个OpenID） 
            /// </summary>
            public string FromUserName => Request.Document.SelectValue("FromUserName");
            /// <summary>
            /// 消息创建时间 （整型） 
            /// </summary>
            public int CreateTime => Request.Document.SelectValue("CreateTime").ConvertToInt();
            /// <summary>
            /// 消息id，64位整型
            /// </summary>
            public Int64 MsgId => Request.Document.SelectValue("MsgId").ConvertToLong();
        }
    }
}
