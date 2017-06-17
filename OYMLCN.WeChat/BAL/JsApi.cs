using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// JS-SDK通讯逻辑辅助
    /// </summary>
    public static class JsApi
    {
        private static Dictionary<AccessToken, JsTicket> ticketDic = new Dictionary<AccessToken, JsTicket>();
        /// <summary>
        /// 获取微信JS接口的临时票据
        /// </summary>
        /// <param name="token"></param>
        /// <param name="oldTicket">旧的凭据（为空则会尝试用已经获取到的未过期票据）</param>
        /// <returns></returns>
        public static JsTicket GetJsTicket(this AccessToken token, JsTicket oldTicket = null)
        {
            if (oldTicket == null)
                oldTicket = ticketDic.Where(d => d.Key == token).Select(d => d.Value).FirstOrDefault();
            if (oldTicket != null && oldTicket.ExpiresTime > DateTime.Now)
                return oldTicket;
            if (Monitor.TryEnter(ticketDic, TimeSpan.FromMilliseconds(100)))
            {
                var data = Api.Ticket.GetJsTicket(token.access_token);
                ticketDic[token] = data;
                var removeList = ticketDic.Where(d => d.Value.ExpiresTime < DateTime.Now).Select(d => d.Key).ToList();
                foreach (var item in removeList)
                    ticketDic.Remove(item);
                Monitor.Exit(ticketDic);
                return data;
            }
            else
                return oldTicket;
        }
        /// <summary>
        /// 获取前端网页初始化接口的必要参数
        /// 返回Json字符串{appid,timestamp,nonce,signature}
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="cfg">基础接口配置</param>
        /// <param name="url">调用页面的Url</param>
        /// <returns></returns>
        public static string CreatePackage(this JsTicket ticket, Config cfg, string url) =>
            Api.Ticket.CreateJsPackage(cfg.AppId, ticket.ticket, url);
    }
}
