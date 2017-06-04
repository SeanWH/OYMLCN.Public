namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 微信提交的明文数据
    /// </summary>
    public class PostModel
    {
        /// <summary>
        /// 微信加密签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 加密消息体签名
        /// </summary>
        public string MsgSignature { get; set; }
        //public string Msg_Signature { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// 随机数
        /// </summary>
        public string Nonce { get; set; }
        /// <summary>
        /// 用户对应公众号的唯一Id
        /// </summary>
        public string OpenId { get; set; }
    }
}
