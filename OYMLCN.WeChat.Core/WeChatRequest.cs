using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        private WeChatRequest() { }
        /// <summary>
        /// 微信请求体 构建方法
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="postModel"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static WeChatRequest Build(Config cfg, PostModel postModel, string xml)
        {
            if (Signature.Create(postModel.Timestamp, postModel.Nonce, cfg.Token) != postModel.Signature)
                throw new NotImplementedException("签名验证失败");

            var xdoc = xml.ToXDocument();
            string encrypt = xdoc.SelectValue("Encrypt");

            if (!encrypt.IsNullOrEmpty() && Signature.Create(postModel.Timestamp, postModel.Nonce, cfg.Token, encrypt) != postModel.MsgSignature)
                throw new NotImplementedException("消息体加密签名验证失败");

            var result = new WeChatRequest()
            {
                Source = xdoc,
                PostModel = postModel,
                Config = cfg
            };
            if (encrypt.IsNullOrEmpty())
                result.Document = xdoc;
            else
            {
                result.IsEncrypt = true;
                if (cfg.EncodingAESKey.IsNullOrEmpty())
                    throw new ArgumentNullException("Config.EncodingAESKey未配置有效值");
                result.Document = xdoc = XDocument.Parse(Cryptography.AES_decrypt(encrypt, cfg.EncodingAESKey));
            }
            return result;
        }

        /// <summary>
        /// 是否加密的消息
        /// </summary>
        public bool IsEncrypt { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Config Config { get; private set; }
        /// <summary>
        /// 消息有效性验证参数
        /// </summary>
        public PostModel PostModel { get; private set; }
        /// <summary>
        /// 解密后或原始消息
        /// </summary>
        public XDocument Document { get; private set; }
        /// <summary>
        /// 原始消息
        /// </summary>
        public XDocument Source { get; private set; }



        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName => Document.SelectValue("ToUserName");
        /// <summary>
        /// 发送方帐号（一个OpenID） 
        /// </summary>
        public string FromUserName => Document.SelectValue("FromUserName");
        /// <summary>
        /// 消息创建时间 （整型） 
        /// </summary>
        public int CreateTime => Document.SelectValue("CreateTime").ConvertToInt();
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId => Document.SelectValue("MsgId").ConvertToLong();
        /// <summary>
        /// 消息排重标记
        /// </summary>
        public string RepeatSign => MsgId != 0 ? MsgId.ToString() : (FromUserName + CreateTime.ToString());
        private static Dictionary<string, DateTime> msgSigns = new Dictionary<string, DateTime>();
        /// <summary>
        /// 是否重返消息
        /// </summary>
        public bool IsRepeat
        {
            get
            {
                var repeat = msgSigns.Where(d => d.Key == RepeatSign).Any();
                msgSigns[RepeatSign] = DateTime.Now;
                ThreadPool.QueueUserWorkItem(e =>
                {
                    if (msgSigns.Count > 2500)
                        lock (msgSigns)
                            foreach (var key in msgSigns
                            .Where(d => d.Value < DateTime.Now.AddSeconds(-30))
                            .Select(d => d.Key)
                            .ToList())
                                msgSigns.Remove(key);
                });
                return repeat;
            }
        }


        /// <summary>
        /// 消息类型
        /// </summary>
        public WeChatRequestMessageType MessageType
        {
            get
            {
                switch (Document.SelectValue("MsgType"))
                {
                    case "text":
                        return WeChatRequestMessageType.Text;
                    case "image":
                        return WeChatRequestMessageType.Image;
                    case "voice":
                        return WeChatRequestMessageType.Voice;
                    case "video":
                        return WeChatRequestMessageType.Video;
                    case "shortvideo":
                        return WeChatRequestMessageType.ShortVideo;
                    case "location":
                        return WeChatRequestMessageType.Location;
                    case "link":
                        return WeChatRequestMessageType.Link;
                    case "event":
                        return WeChatRequestMessageType.Event;
                    default:
                        return WeChatRequestMessageType.Unknow;
                }
            }
        }
        /// <summary>
        /// 事件类型
        /// </summary>
        public WeChatRequestEventType EventType
        {
            get
            {
                if (MessageType != WeChatRequestMessageType.Event)
                    return WeChatRequestEventType.Event未知;
                switch (Document.SelectValue("Event").ToLower())
                {
                    #region 基础事件
                    case "subscribe":
                        return WeChatRequestEventType.Event关注;
                    case "unsubscribe":
                        return WeChatRequestEventType.Event取消关注;
                    case "scan":
                        return WeChatRequestEventType.Event扫描带参数二维码;
                    case "location":
                        return WeChatRequestEventType.Event上报地理位置;
                    case "click":
                        return WeChatRequestEventType.Event点击自定义菜单;
                    case "view":
                        return WeChatRequestEventType.Event点击菜单跳转链接;
                    #endregion

                    case "templatesendjobfinish":
                        return WeChatRequestEventType.Push模板消息发送结果;
                    case "masssendjobfinish":
                        return WeChatRequestEventType.Push群发结果;

                    #region 自定义菜单推送
                    case "scancode_push":
                        return WeChatRequestEventType.MenuPush扫码推事件;
                    case "scancode_waitmsg":
                        return WeChatRequestEventType.MenuPush扫码推等待事件;
                    case "pic_sysphoto":
                        return WeChatRequestEventType.MenuPush系统拍照发图;
                    case "pic_photo_or_album":
                        return WeChatRequestEventType.MenuPush拍照或者相册发图;
                    case "pic_weixin":
                        return WeChatRequestEventType.MenuPush微信相册发图;
                    case "location_select":
                        return WeChatRequestEventType.MenuPush位置选择;
                    #endregion

                    default:
                        return WeChatRequestEventType.Event未知;
                }
            }
        }

    }
}
