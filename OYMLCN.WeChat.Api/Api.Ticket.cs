using OYMLCN.WeChat.Model;
using System;
using System.Collections;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public class Ticket
        {
            public static JsTicket GetJsTicket(string access_token) =>
                ApiGet<JsTicket>("/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", access_token);
            public static JsTicket GetJsTicket(string access_token, JsTicket oldTicket = null) =>
                oldTicket?.ExpiresTime < DateTime.Now ? oldTicket : GetJsTicket(access_token);

            public static string CreateJsPackage(string appid, string ticket, string url)
            {
                var timestamp = DateTime.Now.ToTimestamp();
                string nonce = StringExtension.RandCode(16, onlyNumber: true);

                var parameters = new Hashtable
                {
                    { "jsapi_ticket", ticket },
                    { "noncestr", nonce },
                    { "timestamp", timestamp.ToString() },
                    { "url", url.SplitThenGetFirst("#") }
                };
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
                    appId = appid,
                    timestamp = timestamp,
                    nonceStr = nonce,
                    signature = signature
                }.ToJsonString();
            }
        }
    }
}
