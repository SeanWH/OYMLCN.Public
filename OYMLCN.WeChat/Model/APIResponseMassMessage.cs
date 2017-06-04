using System;

// 本文件放置调用微信API所返回的数据

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 上传消息素材返回信息
    /// </summary>
    public class MassMessageUpload : JsonResult
    {
        /// <summary>
        /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb），图文消息（news）
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 媒体文件/图文消息上传后获取的唯一标识
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 媒体文件上传时间
        /// </summary>
        public long created_at { get; set; }
    }

    /// <summary>
    /// 群发消息返回结果
    /// </summary>
    public class MassMessageSend : JsonResult
    {
        /// <summary>
        /// 消息发送任务的ID
        /// </summary>
        public Int64 msg_id { get; set; }
        /// <summary>
        /// 消息的数据ID，，该字段只有在群发图文消息时，才会出现。可以用于在图文分析数据接口中，获取到对应的图文消息的数据，是图文分析数据接口中的msgid字段中的前半部分，详见图文分析数据接口中的msgid字段的介绍。
        /// </summary>
        public string msg_data_id { get; set; }
    }

    /// <summary>
    /// 群发消息发送状态
    /// </summary>
    public class MassMessageState : JsonResult
    {
        /// <summary>
        /// 群发消息后返回的消息id
        /// </summary>
        public Int64 msg_id { get; set; }
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