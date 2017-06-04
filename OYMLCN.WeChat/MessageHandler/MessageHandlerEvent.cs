using OYMLCN.WeChat.Enum;
using OYMLCN.WeChat.Model;

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
        /// <param name="msg"></param>
        /// <returns></returns>
        private WeChatResponseXmlDocument OnEventMessage(WeChatRequsetXmlDocument msg)
        {
            switch (msg.GetEventType())
            {
                case RequestEventType.关注:
                    var subscribe = msg.ToEventMessage扫描带参数二维码();
                    if (subscribe.Ticket.IsNullOrEmpty())
                        return OnEvent关注(msg.ToEventMessage());
                    else
                        return OnEvent关注(subscribe);
                case RequestEventType.取消关注:
                    OnEvent取消关注(msg.ToEventMessage());
                    break;
                case RequestEventType.扫描带参数二维码:
                    return OnEvent扫描带参数二维码(msg.ToEventMessage扫描带参数二维码());
                case RequestEventType.上报地理位置:
                    return OnEvent上报地理位置(msg.ToEventMessage上报地理位置());
                case RequestEventType.点击自定义菜单:
                    return OnEvent点击自定义菜单(msg.ToEventMessage点击自定义菜单());
                case RequestEventType.点击菜单跳转链接:
                    OnEvent点击菜单跳转链接(msg.ToEventMessage点击菜单跳转链接());
                    break;
                default:
                    return OnPushMessage(msg);
            }
            return null;
        }

        /// <summary>
        /// 返回关注事件处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnEvent关注(WeChatEventMessageBase msg) => DefaultResponseMessage(msg);
        /// <summary>
        /// 返回关注事件处理结果
        /// 扫描带参数二维码首次关注时会推送此事件
        /// 若有带参数二维码请手动转换事件类型为扫描带参数二维码事件
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnEvent关注(WeChatEventMessage扫描带参数二维码 msg) => DefaultResponseMessage(msg);
        /// <summary>
        /// 返回取消关注事件处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual void OnEvent取消关注(WeChatEventMessageBase msg) { }
        /// <summary>
        /// 返回扫描带参数二维码事件处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnEvent扫描带参数二维码(WeChatEventMessage扫描带参数二维码 msg) => DefaultResponseMessage(msg);
        /// <summary>
        /// 返回上报地理位置事件处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnEvent上报地理位置(WeChatEventMessage上报地理位置 msg) => DefaultResponseMessage(msg);
        /// <summary>
        /// 返回点击自定义菜单事件处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnEvent点击自定义菜单(WeChatEventMessage点击自定义菜单 msg) => DefaultResponseMessage(msg);
        /// <summary>
        /// 处理点击菜单跳转链接事件
        /// </summary>
        /// <param name="msg"></param>
        public virtual void OnEvent点击菜单跳转链接(WeChatEventMessage点击菜单跳转链接 msg) { }
    }
}
