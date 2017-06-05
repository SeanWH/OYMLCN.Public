namespace OYMLCN.WeChat.Model
{

    /// <summary>
    /// 客服账号信息列表
    /// </summary>
    public class KefuList : JsonResult
    {
        /// <summary>
        /// 客服账号信息
        /// </summary>
        public class Info
        {
            /// <summary>
            /// 完整客服帐号，格式为：帐号前缀@公众号微信号
            /// </summary>
            public string kf_account { get; set; }
            /// <summary>
            /// 客服昵称
            /// </summary>
            public string kf_nick { get; set; }
            /// <summary>
            /// 客服编号
            /// </summary>
            public string kf_id { get; set; }
            /// <summary>
            /// 客服头像
            /// </summary>
            public string kf_headimgurl { get; set; }
            /// <summary>
            /// 如果客服帐号已绑定了客服人员微信号，
            /// 则此处显示微信号
            /// </summary>
            public string kf_wx { get; set; }
            /// <summary>
            /// 如果客服帐号尚未绑定微信号，但是已经发起了一个绑定邀请，
            /// 则此处显示绑定邀请的微信号
            /// </summary>
            public string invite_wx { get; set; }
            /// <summary>
            /// 如果客服帐号尚未绑定微信号，但是已经发起过一个绑定邀请，
            /// 邀请的过期时间，为unix 时间戳
            /// </summary>
            public int invite_expire_time { get; set; }
            /// <summary>
            /// 邀请的状态，有等待确认“waiting”，被拒绝“rejected”，过期“expired”
            /// </summary>
            public string invite_status { get; set; }

            /// <summary>
            /// 客服在线状态，目前为：1、web 在线
            /// </summary>
            public bool status { get; set; }
            /// <summary>
            /// 客服当前正在接待的会话数
            /// </summary>
            public int accepted_case { get; set; }
        }

        /// <summary>
        /// 账号列表
        /// </summary>
        public Info[] kf_list { get; set; }
        /// <summary>
        /// 在线账号列表
        /// </summary>
        public Info[] kf_online_list { get; set; }
    }



    /// <summary>
    /// 客户会话列表
    /// </summary>
    public class KefuSessionList : JsonResult
    {
        /// <summary>
        /// 客户会话状态
        /// </summary>
        public class Session : JsonResult
        {
            /// <summary>
            /// 正在接待的客服，为空表示没有人在接待
            /// </summary>
            public string kf_account { get; set; }
            /// <summary>
            /// 会话接入的时间
            /// </summary>
            public int createtime { get; set; }
            /// <summary>
            /// 粉丝的最后一条消息的时间
            /// </summary>
            public int latest_time { get; set; }
            /// <summary>
            /// 完整客服帐号，格式为：帐号前缀@公众号微信号
            /// </summary>
            public string openid { get; set; }
        }

        /// <summary>
        /// 客服会话列表
        /// </summary>
        public Session[] sessionlist { get; set; }
        /// <summary>
        /// 未接入会话列表，最多返回100条数据，按照来访顺序
        /// </summary>
        public Session[] waitcaselist { get; set; }
        /// <summary>
        /// 未接入会话数量
        /// </summary>
        public int count { get; set; }
    }



    /// <summary>
    /// 聊天记录列表
    /// </summary>
    public class KefuRecordList : JsonResult
    {
        /// <summary>
        /// 聊天记录
        /// </summary>
        public class Record
        {
            /// <summary>
            /// 用户标识
            /// </summary>
            public string openid { get; set; }
            /// <summary>
            /// 操作码，2002（客服发送信息），2003（客服接收消息）
            /// </summary>
            public int opercode { get; set; }
            /// <summary>
            /// 聊天记录【对于图片、语音、视频，分别展示成文本格式的[image]、[voice]、[video]】
            /// </summary>
            public string text { get; set; }
            /// <summary>
            /// 操作时间，unix时间戳
            /// </summary>
            public int time { get; set; }
            /// <summary>
            /// 完整客服帐号，格式为：帐号前缀@公众号微信号
            /// </summary>
            public string worker { get; set; }
        }

        /// <summary>
        /// 聊天记录
        /// </summary>
        public Record[] recordlist { get; set; }
        /// <summary>
        /// 消息条数
        /// </summary>
        public int number { get; set; }
    }
}
