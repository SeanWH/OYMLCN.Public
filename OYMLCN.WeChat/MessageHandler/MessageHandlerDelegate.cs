using OYMLCN.WeChat.Enum;
using OYMLCN.WeChat.Model;
using System.Collections.Generic;
using System.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 消息处理器委托
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="msg"></param>
    /// <returns></returns>
    public delegate WeChatResponseXmlDocument Handler<T>(T msg) where T : WeChatMessageBase;

    /// <summary>
    /// 微信请求集中处理入口
    /// </summary>
    public abstract partial class MessageHandler
    {
        private WeChatResponseXmlDocument DelegateHandler(WeChatRequsetXmlDocument msg)
        {
            if (textHandler.Count == 0 ||
                eventMenuClickHandler.Count == 0 ||
                EventScanIdHandler.Count == 0
                )
                return null;
            switch (msg.GetMsgType())
            {
                case RequestMsgType.Text:
                    if (textHandler.Count > 0)
                    {
                        var text = Request.ToRequestMessageText();
                        var item = textHandler.Where(d => d.Key.KeyWord.ToLower().Contains(text.Content.ToLower())).FirstOrDefault();
                        if (item.Value != null)
                        {
                            switch (item.Key.Method)
                            {
                                case HandlerContrast.Contain:
                                    if (text.Content.Contains(item.Key.KeyWord))
                                        return item.Value.Invoke(text);
                                    break;
                                case HandlerContrast.Equal:
                                    if (text.Content.Equals(item.Key.KeyWord))
                                        return item.Value.Invoke(text);
                                    break;
                                case HandlerContrast.Superficial:
                                    if (text.Content.ToLower().Equals(item.Key.KeyWord.ToLower()))
                                        return item.Value.Invoke(text);
                                    break;
                                case HandlerContrast.ContainSuperficial:
                                    if (text.Content.ToLower().Contains(item.Key.KeyWord.ToLower()))
                                        return item.Value.Invoke(text);
                                    break;
                            }
                        }
                    }
                    break;
                case RequestMsgType.Event:
                    switch (msg.GetEventType())
                    {
                        case RequestEventType.点击自定义菜单:
                            if (eventMenuClickHandler.Count > 0)
                            {
                                var clickEvent = msg.ToEventMessage点击自定义菜单();
                                return eventMenuClickHandler.Where(d => d.Key == clickEvent.EventKey).FirstOrDefault().Value?.Invoke(clickEvent);
                            }
                            break;
                        case RequestEventType.扫描带参数二维码:
                        case RequestEventType.关注:
                            if (EventScanIdHandler.Count > 0)
                            {
                                var scanTicket = msg.ToEventMessage扫描带参数二维码();
                                return EventScanIdHandler.Where(d => d.Key == scanTicket.SceneId).FirstOrDefault().Value?.Invoke(scanTicket);
                            }
                            break;
                    }
                    break;
            }
            return null;
        }

        private Dictionary<HandlerRule, Handler<WeChatMessageText>> textHandler = new Dictionary<HandlerRule, Handler<WeChatMessageText>>();
        /// <summary>
        /// 关键字处理方法集合
        /// </summary>
        protected Dictionary<HandlerRule, Handler<WeChatMessageText>> TextKeyWordHandler => textHandler;
        /// <summary>
        /// 添加关键字处理方法
        /// </summary>
        /// <param name="rule">关键字对比方式</param>
        /// <param name="method">处理方法</param>
        public void AddTextKeyWordHandler(HandlerRule rule, Handler<WeChatMessageText> method) =>
            TextKeyWordHandler.Add(rule, method);
        /// <summary>
        /// 添加关键字处理方法
        /// </summary>
        /// <param name="contrast">关键字对比方式</param>
        /// <param name="keyWord">关键词</param>
        /// <param name="method">处理方法</param>
        public void AddTextKeyWordHandler(HandlerContrast contrast, string keyWord, Handler<WeChatMessageText> method) =>
            TextKeyWordHandler.Add(new HandlerRule() { KeyWord = keyWord, Method = contrast }, method);


        private Dictionary<string, Handler<WeChatEventMessage点击自定义菜单>> eventMenuClickHandler = new Dictionary<string, Handler<WeChatEventMessage点击自定义菜单>>();
        /// <summary>
        /// 自定义菜单事件处理方法集合
        /// </summary>
        protected Dictionary<string, Handler<WeChatEventMessage点击自定义菜单>> EventMenuClickHandler => eventMenuClickHandler;
        /// <summary>
        /// 添加自定义菜单事件处理方法
        /// </summary>
        /// <param name="eventKey">事件值（区分大小写）</param>
        /// <param name="method">处理方法</param>
        public void AddEventMenuClickHandler(string eventKey, Handler<WeChatEventMessage点击自定义菜单> method) =>
            EventMenuClickHandler.Add(eventKey, method);


        private Dictionary<string, Handler<WeChatEventMessage扫描带参数二维码>> eventScanIdHandler = new Dictionary<string, Handler<WeChatEventMessage扫描带参数二维码>>();
        /// <summary>
        /// 二维码扫描ScanId处理方法集合
        /// </summary>
        protected Dictionary<string, Handler<WeChatEventMessage扫描带参数二维码>> EventScanIdHandler => eventScanIdHandler;
        /// <summary>
        /// 添加二维码扫描ScanId处理方法
        /// </summary>
        /// <param name="scanId"></param>
        /// <param name="method"></param>
        public void AddEventScanIdHandler(string scanId, Handler<WeChatEventMessage扫描带参数二维码> method) =>
            EventScanIdHandler.Add(scanId, method);
    }
}
