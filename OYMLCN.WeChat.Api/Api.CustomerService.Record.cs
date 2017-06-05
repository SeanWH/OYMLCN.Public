using OYMLCN.WeChat.Model;
using System;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public partial class CustomerService
        {
            public class Record
            {
                protected class JsonCreate
                {
                    public static string GetMsgList(DateTime starttime, DateTime endtime, long msgid = 1, int number = 10000) =>
                        "{\"starttime\":" + starttime.ToTimestamp().ToString() + ",\"endtime\":" + endtime.ToTimestamp().ToString() +
                        ",\"msgid\":" + msgid.ToString() + ",\"number\":" + number.ToString() + "}";
                }
                public static KefuRecordList GetMsgList(string access_token, DateTime starttime, DateTime endtime, long msgid = 1, int number = 10000) =>
                     ApiPost<KefuRecordList>(JsonCreate.GetMsgList(starttime, endtime, msgid, number), "/customservice/msgrecord/getmsglist?access_token={0}", access_token);

            }
        }
    }
}
