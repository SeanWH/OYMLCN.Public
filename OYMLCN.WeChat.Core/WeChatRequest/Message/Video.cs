using System;
using System.Collections.Generic;
using System.Text;


namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        /// <summary>
        /// 视频消息
        /// </summary>
        public WeChatMessageVideo MessageVideo => new WeChatMessageVideo(this);
        /// <summary>
        /// 短视频消息
        /// </summary>
        public WeChatMessageVideo MessageShortVideo => MessageVideo;

        /// <summary>
        /// 视频消息
        /// </summary>
        public class WeChatMessageVideo : WeChatMessageBase
        {
            /// <summary>
            /// 视频消息
            /// </summary>
            /// <param name="request"></param>
            public WeChatMessageVideo(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 视频消息媒体id
            /// </summary>
            public string MediaId => Request.Document.SelectValue("MediaId");
            /// <summary>
            /// 视频消息缩略图的媒体id
            /// </summary>
            public string ThumbMediaId => Request.Document.SelectValue("ThumbMediaId");
        }
    }
}
