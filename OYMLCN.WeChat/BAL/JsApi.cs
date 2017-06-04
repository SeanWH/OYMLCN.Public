using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;
using System.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// JS-SDK通讯逻辑辅助
    /// </summary>
    public static class JsApi
    {
        private static Dictionary<AccessToken, JsApiTicket> ticketDic = new Dictionary<AccessToken, JsApiTicket>();
        /// <summary>
        /// 获取微信JS接口的临时票据
        /// </summary>
        /// <param name="token"></param>
        /// <param name="oldTicket">旧的凭据（为空则会尝试用已经获取到的未过期票据）</param>
        /// <returns></returns>
        public static JsApiTicket GetJsApiTicket(this AccessToken token, JsApiTicket oldTicket = null)
        {
            if (oldTicket == null)
                oldTicket = ticketDic.Where(d => d.Key == token).Select(d => d.Value).FirstOrDefault();
            if (oldTicket != null && oldTicket.ExpiresTime > DateTime.Now)
                return oldTicket;
            if (Monitor.TryEnter(ticketDic, TimeSpan.FromMilliseconds(100)))
            {
                var data = HttpClient.GetString(CoreApi.ApiUrl("/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", token.access_token)).DeserializeJsonString<JsApiTicket>();
                if (data != null && data.Success)
                {
                    data.Config = token.Config;
                    ticketDic[token] = data;

                    var removeList = ticketDic.Where(d => d.Value.ExpiresTime < DateTime.Now).Select(d => d.Key).ToList();
                    foreach (var item in removeList)
                        ticketDic.Remove(item);
                }
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
        /// <param name="url">调用页面的Url</param>
        /// <returns></returns>
        public static string CreatePackage(this JsApiTicket ticket, string url)
        {
            var timestamp = DateTime.Now.ToTimestamp();
            string nonce = "".RandCode(16);

            var parameters = new Hashtable();
            parameters.Add("jsapi_ticket", ticket.ticket);
            parameters.Add("noncestr", nonce);
            parameters.Add("timestamp", timestamp.ToString());
            parameters.Add("url", url.SplitThenGetFirst("#"));

            var sb = new StringBuilder();
            var akeys = new ArrayList(parameters.Keys);
            akeys.Sort();
            foreach (var k in akeys)
                if (parameters[k] != null)
                {
                    var v = (string)parameters[k];
                    if (sb.Length == 0)
                        sb.Append(k + "=" + v);
                    else
                        sb.Append("&" + k + "=" + v);
                }
            string signature = sb.ToString().EncodeToSHA1();
            return new
            {
                appId = ticket.Config.AppId,
                timestamp = timestamp,
                nonceStr = nonce,
                signature = signature
            }.ToJsonString();
        }
    }
}
