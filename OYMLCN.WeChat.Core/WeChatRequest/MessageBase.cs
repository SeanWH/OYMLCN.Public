using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

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
        }
    }
}
