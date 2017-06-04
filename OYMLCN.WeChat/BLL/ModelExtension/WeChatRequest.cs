using System;
using System.Collections.Generic;
using OYMLCN.WeChat.Model;
using System.Linq;
using System.Threading;
using OYMLCN.WeChat.Enum;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// Extension
    /// </summary>
    public static partial class Extension
    {

        /// <summary>
        /// 获取消息类型
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static RequestMsgType GetMsgType(this WeChatRequsetXmlDocument xdoc) => xdoc.Document.Elements().SelectValue("MsgType").ToRequestMsgType();
        /// <summary>
        /// 获取事件类型
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static RequestEventType GetEventType(this WeChatRequsetXmlDocument xdoc) => xdoc.Document.Elements().SelectValue("Event").ToRequestEventType();

        private static Dictionary<string, DateTime> msgSigns = new Dictionary<string, DateTime>();

        /// <summary>
        /// 获取查重标识字符串
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static string GetRepeatSign(this WeChatRequsetXmlDocument xdoc) => xdoc.ToRequestMessageText().RepeatSign;
        /// <summary>
        /// 检查是否重返消息
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static bool IsRepeat(this WeChatRequsetXmlDocument xdoc)
        {
            // 反序列化为信息量最小的类型
            var sign = xdoc.GetRepeatSign();
            var repeat = msgSigns.Where(d => d.Key == sign).Any();
            msgSigns[sign] = DateTime.Now;
            ThreadPool.QueueUserWorkItem(e =>
            {
                foreach (var key in msgSigns.Where(d => d.Value < DateTime.Now.AddSeconds(-30)).Select(d => d.Key).ToList())
                    msgSigns.Remove(key);
            });
            return repeat;
        }


        /// <summary>
        /// 将消息类型字符串转换为对应的消息类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static RequestMsgType ToRequestMsgType(this string type)
        {
            switch (type)
            {
                case "text":
                    return RequestMsgType.Text;
                case "image":
                    return RequestMsgType.Image;
                case "voice":
                    return RequestMsgType.Voice;
                case "video":
                    return RequestMsgType.Video;
                case "shortvideo":
                    return RequestMsgType.ShortVideo;
                case "location":
                    return RequestMsgType.Location;
                case "link":
                    return RequestMsgType.Link;
                case "event":
                    return RequestMsgType.Event;
                default:
                    return RequestMsgType.Unknow;
            }
        }
        /// <summary>
        /// 根据Xml填充消息实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static T FillByDom<T>(this T msg, WeChatRequsetXmlDocument xdoc) where T : WeChatMessageBase
        {
            msg.Config = xdoc.Config;
            msg.IsEncrypt = xdoc.IsEncrypt;
            msg.PostModel = xdoc.PostModel;

            var dom = xdoc.Document.Elements();
            msg.CreateTime = dom.SelectValue("CreateTime").ConvertToInt();
            msg.FromUserName = dom.SelectValue("FromUserName");
            msg.MsgId = dom.SelectValue("MsgId").ConvertToLong();
            //msg.MsgType = dom.SelectValue("MsgType").ParseToRequestMsgType();
            msg.ToUserName = dom.SelectValue("ToUserName");
            return msg;
        }


        /// <summary>
        /// 反序列化Xml数据为微信文本消息
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatMessageText ToRequestMessageText(this WeChatRequsetXmlDocument xdoc)
        {
            var result = new WeChatMessageText()
            {
                Content = xdoc.Document.Elements().SelectValue("Content")
            };
            return result.FillByDom(xdoc);
        }
        /// <summary>
        /// 反序列化Xml数据为微信图片消息
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatMessageImage ToRequestMessageImage(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            var result = new WeChatMessageImage()
            {
                PicUrl = dom.SelectValue("PicUrl"),
                MediaId = dom.SelectValue("MediaId")
            };
            return result.FillByDom(xdoc);
        }
        /// <summary>
        /// 反序列化Xml数据为微信语音消息
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatMessageVoice ToRequestMessageVoice(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            var result = new WeChatMessageVoice()
            {
                Format = dom.SelectValue("Format"),
                MediaId = dom.SelectValue("MediaId"),
                Recognition = dom.SelectValue("Recognition")
            };
            return result.FillByDom(xdoc);
        }
        /// <summary>
        /// 反序列化Xml数据为微信视频消息
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatMessageVideo ToRequestMessageVideo(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            var result = new WeChatMessageVideo()
            {
                ThumbMediaId = dom.SelectValue("ThumbMediaId"),
                MediaId = dom.SelectValue("MediaId")
            };
            return result.FillByDom(xdoc);
        }
        /// <summary>
        /// 反序列化Xml数据为微信小视频消息
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatMessageShortVideo ToRequestMessageShortVideo(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            var result = new WeChatMessageShortVideo()
            {
                ThumbMediaId = dom.SelectValue("ThumbMediaId"),
                MediaId = dom.SelectValue("MediaId")
            };
            return result.FillByDom(xdoc);
        }
        /// <summary>
        /// 反序列化Xml数据为微信地理位置消息
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatMessageLocation ToRequestMessageLocation(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            var result = new WeChatMessageLocation()
            {
                Location_X = dom.SelectValue("Location_X").ConvertToDouble(),
                Location_Y = dom.SelectValue("Location_Y").ConvertToDouble(),
                Scale = dom.SelectValue("Scale").ConvertToByte(),
                Label = dom.SelectValue("Label")
            };
            return result.FillByDom(xdoc);
        }
        /// <summary>
        /// 反序列化Xml数据为微信链接消息
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatMessageLink ToRequestMessageLink(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            var result = new WeChatMessageLink()
            {
                Title = dom.SelectValue("Title"),
                Description = dom.SelectValue("Description"),
                Url = dom.SelectValue("Url")
            };
            return result.FillByDom(xdoc);
        }
    }
}
