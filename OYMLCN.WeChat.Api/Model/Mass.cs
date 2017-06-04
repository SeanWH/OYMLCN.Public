namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 群发消息返回结果
    /// </summary>
    public class MassResult : JsonResult
    {
        /// <summary>
        /// 消息的数据ID，，该字段只有在群发图文消息时，才会出现。可以用于在图文分析数据接口中，获取到对应的图文消息的数据，是图文分析数据接口中的msgid字段中的前半部分，详见图文分析数据接口中的msgid字段的介绍。
        /// </summary>
        public string msg_data_id { get; set; }
    }

    /// <summary>
    /// 群发消息发送状态
    /// </summary>
    public class MassState : JsonResult
    {
        /// <summary>
        /// 消息发送后的状态，SEND_SUCCESS表示发送成功
        /// </summary>
        public string msg_status { get; set; }
        /// <summary>
        /// 群发状态是否为SEND_SUCCESS
        /// </summary>
        public new bool Success => msg_status?.ToLower().Contains("success") ?? false;
    }
}
