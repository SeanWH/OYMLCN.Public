namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        /// <summary>
        /// 语音消息
        /// </summary>
        public WeChatMessageVoice MessageVoice => new WeChatMessageVoice(this);

        /// <summary>
        /// 语音消息
        /// </summary>
        public class WeChatMessageVoice : WeChatMessageBase
        {
            /// <summary>
            /// 语音消息
            /// </summary>
            /// <param name="request"></param>
            public WeChatMessageVoice(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 语音消息媒体id
            /// </summary>
            public string MediaId => Request.Document.SelectValue("MediaId");
            /// <summary>
            /// 语音格式，如amr，speex等
            /// </summary>
            public string Format => Request.Document.SelectValue("Format");
            /// <summary>
            /// 语音识别结果
            /// 开通语音识别功能，用户每次发送语音给公众号时，微信会在推送的语音消息XML数据包中，增加一个Recongnition字段。 
            /// </summary>
            public string Recognition => Request.Document.SelectValue("Recognition");
        }
    }
}
