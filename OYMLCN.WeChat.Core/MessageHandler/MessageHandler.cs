namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求集中处理入口
    /// </summary>
    public abstract partial class MessageHandler
    {
        /// <summary>
        /// 当前请求
        /// </summary>
        public WeChatRequest Request { get; private set; }
        /// <summary>
        /// 微信请求集中处理入口
        /// </summary>
        /// <param name="request"></param>
        public MessageHandler(WeChatRequest request) => Request = request;
        /// <summary>
        /// 执行请求处理并返回消息体
        /// </summary>
        /// <returns></returns>
        private WeChatResponse Execute()
        {
            // 首先处理委托事件，如果委托事件有返回值则直接返回
            var result = DelegateHandler();
            if (result != null)
                return result;
            switch (Request.MessageType)
            {
                case WeChatRequestMessageType.Text:
                    return OnMessageText(Request, Request.MessageText);
                case WeChatRequestMessageType.Location:
                    return OnMessageLocaltion(Request, Request.MessageLocation);
                case WeChatRequestMessageType.Image:
                    return OnMessageImage(Request, Request.MessageImage);
                case WeChatRequestMessageType.Voice:
                    return OnMessageVoice(Request, Request.MessageVoice);
                case WeChatRequestMessageType.Link:
                    return OnMessageLink(Request, Request.MessageLink);
                case WeChatRequestMessageType.Video:
                    return OnMessageVideo(Request, Request.MessageVideo);
                case WeChatRequestMessageType.ShortVideo:
                    return OnMessageShortVideo(Request, Request.MessageShortVideo);
                case WeChatRequestMessageType.Event:
                    return OnEventMessage();
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
        /// <param name="request">微信请求</param>
        /// <returns></returns>
        public abstract WeChatResponse DefaultResponseMessage(WeChatRequest request);
        /// <summary>
        /// 返回文本消息处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="text"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnMessageText(WeChatRequest request, WeChatRequest.WeChatMessageText text) => DefaultResponseMessage(request);
        /// <summary>
        /// 返回图片消息处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="image"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnMessageImage(WeChatRequest request, WeChatRequest.WeChatMessageImage image) => DefaultResponseMessage(request);
        /// <summary>
        /// 返回语言消息处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="voice"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnMessageVoice(WeChatRequest request, WeChatRequest.WeChatMessageVoice voice) => DefaultResponseMessage(request);
        /// <summary>
        /// 返回视频消息处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="video"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnMessageVideo(WeChatRequest request, WeChatRequest.WeChatMessageVideo video) => DefaultResponseMessage(request);
        /// <summary>
        /// 短视频
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="shortVideo"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnMessageShortVideo(WeChatRequest request, WeChatRequest.WeChatMessageVideo shortVideo) => DefaultResponseMessage(request);
        /// <summary>
        /// 返回地理位置消息处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="location"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnMessageLocaltion(WeChatRequest request, WeChatRequest.WeChatMessageLocation location) => DefaultResponseMessage(request);
        /// <summary>
        /// 返回链接消息处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="link"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnMessageLink(WeChatRequest request, WeChatRequest.WeChatMessageLink link) => DefaultResponseMessage(request);
    }
}
