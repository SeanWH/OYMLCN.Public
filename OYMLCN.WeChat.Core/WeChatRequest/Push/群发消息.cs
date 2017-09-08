using System.Collections.Generic;
using System.Xml.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        /// <summary>
        /// 模板消息事件推送
        /// </summary>
        public WeChatPush群发结果 Push群发结果 => new WeChatPush群发结果(this);

        /// <summary>
        /// 模板消息事件推送
        /// </summary>
        public class WeChatPush群发结果 : WeChatMessageBase
        {
            /// <summary>
            /// 模板消息事件推送
            /// </summary>
            /// <param name="request"></param>
            public WeChatPush群发结果(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 群发的消息ID
            /// </summary>
            public long MsgID => Request.Document.SelectValue("MsgID").ConvertToLong();
            /// <summary>
            /// 群发的结构，为“send success”或“send fail”或“err(num)”。
            /// </summary>
            public string Status => Request.Document.SelectValue("Status");
            /// <summary>
            /// 是否群发成功
            /// </summary>
            public bool Success => Status?.ToLower().Contains("success") ?? false;

            Dictionary<string, string> errReson = new Dictionary<string, string>() {
                { "", "正常"},
                { "10001", "涉嫌广告"},
                { "20001", "涉嫌政治"},
                { "20004", "涉嫌社会"},
                { "20002", "涉嫌色情"},
                { "20006", "涉嫌违法犯罪"},
                { "20008", "涉嫌欺诈"},
                { "20013", "涉嫌版权"},
                { "22000", "涉嫌互推(互相宣传)"},
                { "21000", "涉嫌其他" },
                { "30001", "原创校验出现系统错误且用户选择了被判为转载就不群发" },
                { "30002", "原创校验被判定为不能群发" },
                { "30003", "原创校验被判定为转载文且用户选择了被判为转载就不群发" },
            };
            /// <summary>
            /// 审核失败的具体原因
            /// </summary>
            public string ErrorReason => errReson.SelectValueOrDefault(Status.ToNumeric());


            /// <summary>
            /// tag_id下粉丝数；或者openid_list中的粉丝数
            /// </summary>
            public int TotalCount => Request.Document.SelectValue("TotalCount").ConvertToInt();
            /// <summary>
            /// 过滤（过滤是指特定地区、性别的过滤、用户设置拒收的过滤，用户接收已超4条的过滤）后，准备发送的粉丝数，原则上，FilterCount = SentCount + ErrorCount
            /// </summary>
            public int FilterCount => Request.Document.SelectValue("FilterCount").ConvertToInt();
            /// <summary>
            /// 发送成功的粉丝数
            /// </summary>
            public int SentCount => Request.Document.SelectValue("SentCount").ConvertToInt();
            /// <summary>
            /// 发送失败的粉丝数
            /// </summary>
            public int ErrorCount => Request.Document.SelectValue("ErrorCount").ConvertToInt();

            /// <summary>
            /// 各个单图文校验结果
            /// </summary>
            public List<CheckResult> CopyrightCheckResult
            {
                get
                {
                    var result = new List<CheckResult>();
                    var results = Request.Document.Elements().Elements("CopyrightCheckResult").Elements("ResultList").Elements("item");
                    foreach (var item in results)
                        result.Add(new CheckResult(item));
                    return result;
                }
            }

            /// <summary>
            /// 整体校验结果
            /// </summary>
            public byte CheckState => Request.Document.Elements().Elements("CopyrightCheckResult").SelectValue("CheckState").ConvertToByte();




            /// <summary>
            /// 原创校验结果
            /// </summary>
            public class CheckResult
            {
                XElement Element;
                private CheckResult() { }
                /// <summary>
                /// 原创校验结果
                /// </summary>
                /// <param name="element"></param>
                public CheckResult(XElement element) => Element = element;
                /// <summary>
                /// 群发文章的序号，从1开始
                /// </summary>
                public short ArticleIdx => Element.SelectValue("ArticleIdx").ConvertToByte();
                /// <summary>
                /// 用户声明文章的状态
                /// </summary>
                public byte UserDeclareState => Element.SelectValue("UserDeclareState").ConvertToByte();
                /// <summary>
                /// 系统校验的状态
                /// </summary>
                public byte AuditState => Element.SelectValue("AuditState").ConvertToByte();
                /// <summary>
                /// 相似原创文的url
                /// </summary>
                public string OriginalArticleUrl => Element.SelectValue("OriginalArticleUrl");
                /// <summary>
                /// 相似原创文的类型
                /// </summary>
                public byte OriginalArticleType => Element.SelectValue("OriginalArticleType").ConvertToByte();
                /// <summary>
                /// 是否能转载
                /// </summary>
                public bool CanReprint => Element.SelectValue("CanReprint").ConvertToBoolean();
                /// <summary>
                /// 是否需要替换成原创文内容
                /// </summary>
                public bool NeedReplaceContent => Element.SelectValue("NeedReplaceContent").ConvertToBoolean();
                /// <summary>
                /// 是否需要注明转载来源
                /// </summary>
                public bool NeedShowReprintSource => Element.SelectValue("NeedShowReprintSource").ConvertToBoolean();
            }

        }
    }
}
