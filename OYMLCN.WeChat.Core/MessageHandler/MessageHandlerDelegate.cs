using System;
using System.Collections.Generic;
using System.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 消息处理器委托
    /// </summary>
    /// <param name="request">微信请求</param>
    /// <typeparam name="T"></typeparam>
    /// <param name="msg"></param>
    /// <returns></returns>
    public delegate WeChatResponse Handler<T>(WeChatRequest request, T msg) where T : WeChatRequest.WeChatMessageBase;

    /// <summary>
    /// 微信请求集中处理入口
    /// </summary>
    public abstract partial class MessageHandler
    {
        /// <summary>
        /// 关键字对比方式
        /// </summary>
        public enum HandlerContrast
        {
            /// <summary>
            /// 完全相等
            /// </summary>
            Equal,
            /// <summary>
            /// 包含关键字
            /// </summary>
            Contain,
            /// <summary>
            /// 不区分大小写
            /// </summary>
            Superficial,
            /// <summary>
            /// 包含关键字不区分大小写
            /// </summary>
            ContainSuperficial
        }
        /// <summary>
        /// 关键字处理规则
        /// </summary>
        public class HandlerRule
        {
            /// <summary>
            /// 关键字处理规则
            /// </summary>
            public HandlerRule() { }
            /// <summary>
            /// 关键字处理规则
            /// </summary>
            /// <param name="method"></param>
            /// <param name="keyWord"></param>
            public HandlerRule(HandlerContrast method, string keyWord)
            {
                Method = method;
                KeyWord = keyWord;
            }
            /// <summary>
            /// 对比方式
            /// </summary>
            public HandlerContrast Method { get; set; }
            /// <summary>
            /// 关键字
            /// </summary>
            public string KeyWord { get; set; }
        }


        private WeChatResponse DelegateHandler()
        {
            switch (Request.MessageType)
            {
                case WeChatRequestMessageType.Text:
                    if (textHandler.Count > 0)
                    {
                        var text = this.Request.MessageText;
                        var item = textHandler.Where(d => text.Content.ToLower().Contains(d.Key.KeyWord.ToLower())).FirstOrDefault();
                        if (item.Value != null)
                        {
                            switch (item.Key.Method)
                            {
                                case HandlerContrast.Contain:
                                    if (text.Content.Contains(item.Key.KeyWord))
                                        return item.Value.Invoke(Request, text);
                                    break;
                                case HandlerContrast.Equal:
                                    if (text.Content.Equals(item.Key.KeyWord))
                                        return item.Value.Invoke(Request,text);
                                    break;
                                case HandlerContrast.Superficial:
                                    if (text.Content.Equals(item.Key.KeyWord.ToLower(), StringComparison.OrdinalIgnoreCase))
                                        return item.Value.Invoke(Request, text);
                                    break;
                                case HandlerContrast.ContainSuperficial:
                                    if (text.Content.ToLower().Contains(item.Key.KeyWord.ToLower()))
                                        return item.Value.Invoke(Request, text);
                                    break;
                            }
                        }
                    }
                    break;
                case WeChatRequestMessageType.Event:
                    switch (Request.EventType)
                    {
                        case WeChatRequestEventType.Event点击自定义菜单:
                            if (eventMenuClickHandler.Count > 0)
                            {
                                var clickEvent = Request.Event点击自定义菜单;
                                return eventMenuClickHandler.Where(d => d.Key == clickEvent.EventKey).FirstOrDefault().Value?.Invoke(Request, clickEvent);
                            }
                            break;
                        case WeChatRequestEventType.Event扫描带参数二维码:
                        case WeChatRequestEventType.Event关注:
                            if (EventScanIdHandler.Count > 0)
                            {
                                var scanTicket = Request.Event扫描带参数二维码;
                                return EventScanIdHandler.Where(d => d.Key == scanTicket.SceneId).FirstOrDefault().Value?.Invoke(Request, scanTicket);
                            }
                            break;
                    }
                    break;
            }
            return null;
        }

        private Dictionary<HandlerRule, Handler<WeChatRequest.WeChatMessageText>> textHandler = new Dictionary<HandlerRule, Handler<WeChatRequest.WeChatMessageText>>();
        /// <summary>
        /// 关键字处理方法集合
        /// </summary>
        private Dictionary<HandlerRule, Handler<WeChatRequest.WeChatMessageText>> TextKeyWordHandler => textHandler;
        /// <summary>
        /// 添加关键字处理方法
        /// </summary>
        /// <param name="rule">关键字对比方式</param>
        /// <param name="method">处理方法</param>
        public void AddTextKeyWordHandler(HandlerRule rule, Handler<WeChatRequest.WeChatMessageText> method) => TextKeyWordHandler.Add(rule, method);
        /// <summary>
        /// 添加关键字处理方法
        /// </summary>
        /// <param name="keyWord">关键词</param>
        /// <param name="method">处理方法</param>
        /// <param name="contrast">关键字对比方式（默认完全一致）</param>
        public void AddTextKeyWordHandler(string keyWord, Handler<WeChatRequest.WeChatMessageText> method, HandlerContrast contrast = HandlerContrast.Equal) =>
            TextKeyWordHandler.Add(new HandlerRule()
            {
                KeyWord = keyWord,
                Method = contrast
            }, method);


        private Dictionary<string, Handler<WeChatRequest.WeChatEvent点击自定义菜单>> eventMenuClickHandler = new Dictionary<string, Handler<WeChatRequest.WeChatEvent点击自定义菜单>>();
        /// <summary>
        /// 自定义菜单事件处理方法集合
        /// </summary>
        protected Dictionary<string, Handler<WeChatRequest.WeChatEvent点击自定义菜单>> EventMenuClickHandler => eventMenuClickHandler;
        /// <summary>
        /// 添加自定义菜单事件处理方法
        /// </summary>
        /// <param name="eventKey">事件值（区分大小写）</param>
        /// <param name="method">处理方法</param>
        public void AddEventMenuClickHandler(string eventKey, Handler<WeChatRequest.WeChatEvent点击自定义菜单> method) => EventMenuClickHandler.Add(eventKey, method);


        private Dictionary<string, Handler<WeChatRequest.WeChatEvent扫描带参数二维码>> eventScanIdHandler = new Dictionary<string, Handler<WeChatRequest.WeChatEvent扫描带参数二维码>>();
        /// <summary>
        /// 二维码扫描ScanId处理方法集合
        /// </summary>
        protected Dictionary<string, Handler<WeChatRequest.WeChatEvent扫描带参数二维码>> EventScanIdHandler => eventScanIdHandler;
        /// <summary>
        /// 添加二维码扫描ScanId处理方法
        /// </summary>
        /// <param name="scanId"></param>
        /// <param name="method"></param>
        public void AddEventScanIdHandler(string scanId, Handler<WeChatRequest.WeChatEvent扫描带参数二维码> method) => EventScanIdHandler.Add(scanId, method);
    }
}
