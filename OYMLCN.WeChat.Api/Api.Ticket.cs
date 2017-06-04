using OYMLCN.WeChat.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        private static Dictionary<AccessToken, JsTicket> ticketDic = new Dictionary<AccessToken, JsTicket>();

        public class Ticket
        {
            public static JsTicket GetJsTicket(AccessToken token) =>
                ApiGet<JsTicket>("/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", token.access_token);

            public static JsTicket GetJsTicket(AccessToken token, JsTicket oldTicket = null)
            {
                if (oldTicket == null)
                    oldTicket = ticketDic.Where(d => d.Key == token).Select(d => d.Value).FirstOrDefault();
                if (oldTicket != null && oldTicket.ExpiresTime > DateTime.Now)
                    return oldTicket;
                if (Monitor.TryEnter(ticketDic, TimeSpan.FromMilliseconds(100)))
                {
                    var data = GetJsTicket(token);
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
            public static string CreateJsPackage(Config cfg, JsTicket ticket, string url)
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
                    appId = cfg.AppId,
                    timestamp = timestamp,
                    nonceStr = nonce,
                    signature = signature
                }.ToJsonString();
            }
        }

       
    }
}
