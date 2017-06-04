using OYMLCN.WeChat.Enum;
using System;
using System.Collections.Generic;

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 群发消息事件推送
    /// </summary>
    public class WeChatPush群发消息 : WeChatEventMessageBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public override RequestEventType Event => RequestEventType.Push群发结果;
        /// <summary>
        /// 群发的消息ID
        /// </summary>
        public Int64 MsgID { get; set; }
        /// <summary>
        /// 群发的结构，为“send success”或“send fail”或“err(num)”。
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// tag_id下粉丝数；或者openid_list中的粉丝数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 过滤（过滤是指特定地区、性别的过滤、用户设置拒收的过滤，用户接收已超4条的过滤）后，准备发送的粉丝数，原则上，FilterCount = SentCount + ErrorCount
        /// </summary>
        public int FilterCount { get; set; }
        /// <summary>
        /// 发送成功的粉丝数
        /// </summary>
        public int SentCount { get; set; }
        /// <summary>
        /// 发送失败的粉丝数
        /// </summary>
        public int ErrorCount { get; set; }

        /// <summary>
        /// 各个单图文校验结果
        /// </summary>
        public List<WeChatPushMassCheckResult> ResultList { get; set; }

        /// <summary>
        /// 整体校验结果
        /// </summary>
        public MassCheckState CheckState { get; set; }


        /// <summary>
        /// 是否群发成功
        /// </summary>
        public bool Success => Status?.ToLower().Contains("success") ?? false;
        /// <summary>
        /// 审核失败的具体原因
        /// </summary>
        public MassStatus ErrorReason => (MassStatus)Status.ConvertToInt();
    }

    /// <summary>
    /// 原创校验结果
    /// </summary>
    public class WeChatPushMassCheckResult
    {
        /// <summary>
        /// 群发文章的序号，从1开始
        /// </summary>
        public short ArticleIdx { get; set; }
        /// <summary>
        /// 用户声明文章的状态
        /// </summary>
        public bool UserDeclareState { get; set; }
        /// <summary>
        /// 系统校验的状态
        /// </summary>
        public MassCheckState AuditState { get; set; }
        /// <summary>
        /// 相似原创文的url
        /// </summary>
        public string OriginalArticleUrl { get; set; }
        /// <summary>
        /// 相似原创文的类型
        /// </summary>
        public string OriginalArticleType { get; set; }
        /// <summary>
        /// 是否能转载
        /// </summary>
        public bool CanReprint { get; set; }
        /// <summary>
        /// 是否需要替换成原创文内容
        /// </summary>
        public bool NeedReplaceContent { get; set; }
        /// <summary>
        /// 是否需要注明转载来源
        /// </summary>
        public bool NeedShowReprintSource { get; set; }
    }
}
