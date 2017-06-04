using OYMLCN.WeChat.Model;
using OYMLCN.WeChat.Enum;

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
        /// <param name="msg"></param>
        /// <returns></returns>
        private WeChatResponseXmlDocument OnPushMessageMenu(WeChatRequsetXmlDocument msg)
        {
            switch (msg.GetEventType())
            {
                case RequestEventType.Push扫码推事件:
                    return OnPushMenu扫码推事件(msg.ToPushMenu扫码推事件());
                case RequestEventType.Push扫码推等待事件:
                    return OnPushMenu扫码推等待事件(msg.ToPushMenu扫码推等待事件());
                case RequestEventType.Push系统拍照发图:
                    return OnPushMenu系统拍照发图(msg.ToPushMenu系统拍照发图());
                case RequestEventType.Push拍照或者相册发图:
                    return OnPushMenu拍照或者相册发图(msg.ToPushMenu拍照或者相册发图());
                case RequestEventType.Push微信相册发图:
                    return OnPushMenu微信相册发图(msg.ToPushMenu微信相册发图());
                case RequestEventType.Push位置选择:
                    return OnPushMenu位置选择(msg.ToPushMenu位置选择());
            }
            return null;
        }

        /// <summary>
        /// 返回菜单扫码推事件推送处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnPushMenu扫码推事件(WeChatPushMenu扫码推事件 msg) => DefaultResponseMessage(msg);
        /// <summary>
        /// 返回菜单扫码推等待事件推送处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnPushMenu扫码推等待事件(WeChatPushMenu扫码推等待事件 msg) => DefaultResponseMessage(msg);
        /// <summary>
        /// 返回菜单系统拍照发图事件推送处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnPushMenu系统拍照发图(WeChatPushMenu系统拍照发图 msg) => DefaultResponseMessage(msg);
        /// <summary>
        /// 返回菜单拍照或者相册发图事件推送处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnPushMenu拍照或者相册发图(WeChatPushMenu拍照或者相册发图 msg) => DefaultResponseMessage(msg);
        /// <summary>
        /// 返回菜单微信相册发图事件推送处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnPushMenu微信相册发图(WeChatPushMenu微信相册发图 msg) => DefaultResponseMessage(msg);
        /// <summary>
        /// 返回菜单位置选事件推送处理结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual WeChatResponseXmlDocument OnPushMenu位置选择(WeChatPushMenu位置选择 msg) => DefaultResponseMessage(msg);
    }
}
