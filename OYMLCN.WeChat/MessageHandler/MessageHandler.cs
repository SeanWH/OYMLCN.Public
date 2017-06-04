using OYMLCN.WeChat.Enum;
using OYMLCN.WeChat.Model;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求集中处理入口
    /// </summary>
    public abstract partial class MessageHandler
    {
        private WeChatRequsetXmlDocument Request;
        /// <summary>
        /// 微信请求集中处理入口
        /// </summary>
        /// <param name="XmlDocument"></param>
        public MessageHandler(WeChatRequsetXmlDocument XmlDocument) => Request = XmlDocument;
        /// <summary>
        /// 执行请求处理并返回消息体
        /// </summary>
        /// <returns></returns>
        private WeChatResponseXmlDocument Execute()
        {
            // 首先处理委托事件，如果委托事件有返回值则直接返回
            var result = DelegateHandler(Request);
            if (result != null)
                return result;
            switch (Request.GetMsgType())
            {
                case RequestMsgType.Text:
                    return OnMessageText(Request.ToRequestMessageText());
                case RequestMsgType.Location:
                    return OnMessageLocaltion(Request.ToRequestMessageLocation());
                case RequestMsgType.Image:
                    return OnMessageImage(Request.ToRequestMessageImage());
                case RequestMsgType.Voice:
                    return OnMessageVoice(Request.ToRequestMessageVoice());
                case RequestMsgType.Link:
                    return OnMessageLink(Request.ToRequestMessageLink());
                case RequestMsgType.Video:
                    return OnMessageVideo(Request.ToRequestMessageVideo());
                case RequestMsgType.ShortVideo:
                    return OnMessageShortVideo(Request.ToRequestMessageShortVideo());
                case RequestMsgType.Event:
                    return OnEventMessage(Request);
            }
            return null;
            //throw new NotSupportedException("未知的MsgType请求类型");
        }
        /// <summary>
        /// 获取消息处理结果
        /// </summary>
        public string Result => Execute()?.Result ?? "";



        /// <summary>
        /// 默认返回消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public abstract WeChatResponseXmlDocument DefaultResponseMessage(WeChatMessageBase msg);
        /// <summary>
        /// 返回文本消息处理结果
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnMessageText(WeChatMessageText text) => DefaultResponseMessage(text);
        /// <summary>
        /// 返回图片消息处理结果
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnMessageImage(WeChatMessageImage image) => DefaultResponseMessage(image);
        /// <summary>
        /// 返回语言消息处理结果
        /// </summary>
        /// <param name="voice"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnMessageVoice(WeChatMessageVoice voice) => DefaultResponseMessage(voice);
        /// <summary>
        /// 返回视频消息处理结果
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnMessageVideo(WeChatMessageVideo video) => DefaultResponseMessage(video);
        /// <summary>
        /// 短视频
        /// </summary>
        /// <param name="shortVideo"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnMessageShortVideo(WeChatMessageShortVideo shortVideo) => DefaultResponseMessage(shortVideo);
        /// <summary>
        /// 返回地理位置消息处理结果
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnMessageLocaltion(WeChatMessageLocation location) => DefaultResponseMessage(location);
        /// <summary>
        /// 返回链接消息处理结果
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnMessageLink(WeChatMessageLink link) => DefaultResponseMessage(link);
    }
}
