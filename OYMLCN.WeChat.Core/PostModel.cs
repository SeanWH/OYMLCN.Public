using System.Collections.Generic;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信消息有效性验证参数
    /// </summary>
    public class PostModel
    {
        private PostModel() { }
        /// <summary>
        /// 微信消息有效性验证参数
        /// </summary>
        /// <param name="queryDic"></param>
        /// <returns></returns>
        public static PostModel Build(Dictionary<string, string> queryDic)
        {
            var model = new PostModel()
            {
                Nonce = queryDic.SelectValueOrDefault("nonce"),
                Signature = queryDic.SelectValueOrDefault("signature"),
                Timestamp = queryDic.SelectValueOrDefault("timestamp"),
                OpenId = queryDic.SelectValueOrDefault("openid"),
                MsgSignature = queryDic.SelectValueOrDefault("msg_signature")
            };
            return model;
        }

        /// <summary>
        /// 微信加密签名
        /// </summary>
        public string Signature { get; private set; }
        /// <summary>
        /// 加密消息体签名
        /// </summary>
        public string MsgSignature { get; private set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; private set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string Nonce { get; private set; }
        /// <summary>
        /// 用户对应公众号的唯一Id
        /// </summary>
        public string OpenId { get; private set; }
    }
}
