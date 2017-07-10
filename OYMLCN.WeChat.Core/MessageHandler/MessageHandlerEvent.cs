namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求集中处理入口
    /// </summary>
    public abstract partial class MessageHandler
    {
        /// <summary>
        /// 返回事件消息处理结果
        /// </summary>
        /// <returns></returns>
        private WeChatResponse OnEventMessage()
        {
            switch (Request.EventType)
            {
                case WeChatRequestEventType.Event关注:
                    var subscribe = Request.Event扫描带参数二维码;
                    if (subscribe.Ticket.IsNullOrEmpty())
                        return OnEvent关注(Request);
                    else
                        return OnEvent关注(Request, subscribe);
                case WeChatRequestEventType.Event取消关注:
                    OnEvent取消关注(Request);
                    break;
                case WeChatRequestEventType.Event扫描带参数二维码:
                    return OnEvent扫描带参数二维码(Request, Request.Event扫描带参数二维码);
                case WeChatRequestEventType.Event上报地理位置:
                    return OnEvent上报地理位置(Request, Request.Event上报地理位置);
                case WeChatRequestEventType.Event点击自定义菜单:
                    return OnEvent点击自定义菜单(Request, Request.Event点击自定义菜单);
                case WeChatRequestEventType.Event点击菜单跳转链接:
                    OnEvent点击菜单跳转链接(Request, Request.Event点击菜单跳转链接);
                    break;
                default:
                    return OnPushMessage();
            }
            return null;
        }

        /// <summary>
        /// 返回关注事件处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <returns></returns>
        public virtual WeChatResponse OnEvent关注(WeChatRequest request) => WeChatResponse.ResponseText(request, $"欢迎关注：{request.Config.AccountName}");
        /// <summary>
        /// 返回关注事件处理结果
        /// 扫描带参数二维码首次关注时会推送此事件
        /// 若有带参数二维码请手动转换事件类型为扫描带参数二维码事件
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnEvent关注(WeChatRequest request, WeChatRequest.WeChatEvent扫描带参数二维码 msg) => OnEvent关注(request);
        /// <summary>
        /// 返回取消关注事件处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <returns></returns>
        public virtual void OnEvent取消关注(WeChatRequest request) { }
        /// <summary>
        /// 返回扫描带参数二维码事件处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnEvent扫描带参数二维码(WeChatRequest request, WeChatRequest.WeChatEvent扫描带参数二维码 msg) => DefaultResponseMessage(request);
        /// <summary>
        /// 返回上报地理位置事件处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnEvent上报地理位置(WeChatRequest request, WeChatRequest.WeChatEvent上报地理位置 msg) => DefaultResponseMessage(request);
        /// <summary>
        /// 返回点击自定义菜单事件处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnEvent点击自定义菜单(WeChatRequest request, WeChatRequest.WeChatEvent点击自定义菜单 msg) => DefaultResponseMessage(request);
        /// <summary>
        /// 处理点击菜单跳转链接事件
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="msg"></param>
        public virtual void OnEvent点击菜单跳转链接(WeChatRequest request, WeChatRequest.WeChatEvent点击菜单跳转链接 msg) { }
    }
}
