using OYMLCN.WeChat.Enum;
using System;
using System.Xml.Linq;

// 本文件放置微信消息请求的模型

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 微信请求消息Xml处理结果
    /// </summary>
    public class WeChatRequsetXmlDocument
    {
        /// <summary>
        /// 是否加密的消息
        /// </summary>
        public bool IsEncrypt { get; set; }
        /// <summary>
        /// 解密后或原始消息
        /// </summary>
        public XDocument Document { get; set; }
        /// <summary>
        /// 原始消息
        /// </summary>
        public XDocument Source { get; set; }

        /// <summary>
        /// 平台基础接口配置（联合调用所需）
        /// </summary>
        public Config Config { get; set; }
        /// <summary>
        /// 请求本体（联合调用所需）
        /// </summary>
        public PostModel PostModel { get; set; }
    }

    /// <summary>
    /// 微信消息基类
    /// </summary>
    public abstract class WeChatMessageBase
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方帐号（一个OpenID） 
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间 （整型） 
        /// </summary>
        public int CreateTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public abstract RequestMsgType MsgType { get; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId { get; set; }

        /// <summary>
        /// 消息排重标记
        /// </summary>
        public string RepeatSign
        {
            get
            {
                if (MsgId != 0)
                    return MsgId.ToString();
                else
                    return FromUserName + CreateTime.ToString();
            }
        }

        /// <summary>
        /// 请求体加密标记（标识微信请求是否已加密）
        /// </summary>
        public bool IsEncrypt { get; set; }
        /// <summary>
        /// 平台基础接口配置（联合调用所需）
        /// </summary>
        public Config Config { get; set; }
        /// <summary>
        /// 请求本体（联合调用所需）
        /// </summary>
        public PostModel PostModel { get; set; }
    }

    /// <summary>
    /// 文本消息
    /// </summary>
    public class WeChatMessageText : WeChatMessageBase
    {
        /// <summary>
        /// text 文本消息
        /// </summary>
        public override RequestMsgType MsgType => RequestMsgType.Text;
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 图片消息
    /// </summary>
    public class WeChatMessageImage : WeChatMessageBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public override RequestMsgType MsgType => RequestMsgType.Image;
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 图片消息媒体id
        /// </summary>
        public string MediaId { get; set; }
    }

    /// <summary>
    /// 语音消息
    /// </summary>
    public class WeChatMessageVoice : WeChatMessageBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public override RequestMsgType MsgType => RequestMsgType.Voice;
        /// <summary>
        /// 语音消息媒体id
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 语音识别结果
        /// 开通语音识别功能，用户每次发送语音给公众号时，微信会在推送的语音消息XML数据包中，增加一个Recongnition字段。 
        /// </summary>
        public string Recognition { get; set; }
    }

    /// <summary>
    /// 视频消息
    /// </summary>
    public class WeChatMessageVideo : WeChatMessageBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public override RequestMsgType MsgType => RequestMsgType.Video;
        /// <summary>
        /// 视频消息媒体id
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 视频消息缩略图的媒体id
        /// </summary>
        public string ThumbMediaId { get; set; }
    }

    /// <summary>
    /// 小视频消息
    /// </summary>
    public class WeChatMessageShortVideo : WeChatMessageVideo
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public override RequestMsgType MsgType => RequestMsgType.ShortVideo;
    }

    /// <summary>
    /// 地理位置消息
    /// </summary>
    public class WeChatMessageLocation : WeChatMessageBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public override RequestMsgType MsgType => RequestMsgType.Location;

        /// <summary>
        /// 地理位置维度
        /// </summary>
        public double Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小 
        /// </summary>
        public byte Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }

    }

    /// <summary>
    /// 链接消息
    /// </summary>
    public class WeChatMessageLink : WeChatMessageBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public override RequestMsgType MsgType => RequestMsgType.Link;

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }
    }
}