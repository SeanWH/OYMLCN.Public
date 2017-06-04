namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求集中处理入口
    /// </summary>
    public abstract partial class MessageHandler
    {
        /// <summary>
        /// 返回菜单事件推送处理结果
        /// </summary>
        /// <returns></returns>
        private WeChatResponse OnPushMessageMenu()
        {
            switch (Request.EventType)
            {
                case WeChatRequestEventType.MenuPush扫码推事件:
                    return OnPushMenu扫码推事件(Request, Request.MenuPush扫码推事件);
                case WeChatRequestEventType.MenuPush扫码推等待事件:
                    return OnPushMenu扫码推等待事件(Request, Request.MenuPush扫码推等待事件);
                case WeChatRequestEventType.MenuPush系统拍照发图:
                    return OnPushMenu系统拍照发图(Request, Request.MenuPush系统拍照发图);
                case WeChatRequestEventType.MenuPush拍照或者相册发图:
                    return OnPushMenu拍照或者相册发图(Request, Request.MenuPush拍照或者相册发图);
                case WeChatRequestEventType.MenuPush微信相册发图:
                    return OnPushMenu微信相册发图(Request, Request.MenuPush微信相册发图);
                case WeChatRequestEventType.MenuPush位置选择:
                    return OnPushMenu位置选择(Request, Request.MenuPush位置选择);
            }
            return null;
        }

        /// <summary>
        /// 返回菜单扫码推事件推送处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnPushMenu扫码推事件(WeChatRequest request, WeChatRequest.WeChatMenuPush扫码事件 msg) => DefaultResponseMessage(request);
        /// <summary>
        /// 返回菜单扫码推等待事件推送处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnPushMenu扫码推等待事件(WeChatRequest request, WeChatRequest.WeChatMenuPush扫码事件 msg) => DefaultResponseMessage(request);
        /// <summary>
        /// 返回菜单系统拍照发图事件推送处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnPushMenu系统拍照发图(WeChatRequest request, WeChatRequest.WeChatMenuPush拍照发图 msg) => DefaultResponseMessage(request);
        /// <summary>
        /// 返回菜单拍照或者相册发图事件推送处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnPushMenu拍照或者相册发图(WeChatRequest request, WeChatRequest.WeChatMenuPush拍照发图 msg) => DefaultResponseMessage(request);
        /// <summary>
        /// 返回菜单微信相册发图事件推送处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnPushMenu微信相册发图(WeChatRequest request, WeChatRequest.WeChatMenuPush拍照发图 msg) => DefaultResponseMessage(request);
        /// <summary>
        /// 返回菜单位置选事件推送处理结果
        /// </summary>
        /// <param name="request">微信请求</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponse OnPushMenu位置选择(WeChatRequest request, WeChatRequest.WeChatMenuPush位置选择 msg) => DefaultResponseMessage(request);
    }
}
