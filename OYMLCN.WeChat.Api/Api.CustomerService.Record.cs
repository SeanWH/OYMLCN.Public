using Newtonsoft.Json.Linq;
using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public partial class CustomerService
        {
            public static class Record
            {
                public static KefuRecordList GetMsgList(AccessToken token, DateTime start, DateTime end, long msgid = 1, int number = 10000) =>
                     ApiPost<KefuRecordList>("{\"starttime\":" + start.ToTimestamp().ToString() + ",\"endtime\":" + end.ToTimestamp().ToString() +
                        ",\"msgid\":" + msgid.ToString() + ",\"number\":" + number.ToString() + "}",
                         "/customservice/msgrecord/getmsglist?access_token={0}", token.access_token);

            }
        }
    }
}
