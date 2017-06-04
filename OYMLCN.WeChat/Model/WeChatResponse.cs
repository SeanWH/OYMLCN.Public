using OYMLCN.WeChat.Enum;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 本文件放置微信消息被动回复的模型

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 微信被动回复消息体
    /// </summary>
    public class WeChatResponseXmlDocument
    {
        /// <summary>
        /// 是否加密的消息
        /// </summary>
        public bool IsEncrypt { get; set; }
        /// <summary>
        /// 加密后或原始消息
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 原始消息
        /// </summary>
        public string Source { get; set; }
    }

    /// <summary>
    /// 微信回复基类
    /// </summary>
    public abstract class WeChatResponseBase
    {
        /// <summary>
        /// 接收方帐号（收到的OpenID） 
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public int CreateTime { get; set; }

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

        /// <summary>
        /// 消息类型
        /// </summary>
        public abstract ResponseMsgType MsgType { get; }
        /// <summary>
        /// 必须重写的方法 转换成微信字符串
        /// </summary>
        /// <returns></returns>
        public abstract string ToXmlString();
        /// <summary>
        /// 重写ToString()为调用ToXmlString()
        /// </summary>
        /// <returns></returns>
        public override string ToString() => ToXmlString().Replace("\r\n", "\n");
    }

    /// <summary>
    /// 回复文本消息
    /// </summary>
    public class WeChatResponseText : WeChatResponseBase
    {
        /// <summary>
        /// 回复文本消息
        /// </summary>
        /// <param name="content">回复正文</param>
        public WeChatResponseText(string content) => Content = content;

        /// <summary>
        /// 回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示） 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public override ResponseMsgType MsgType => ResponseMsgType.Text;
        /// <summary>
        /// 回复Xml
        /// </summary>
        /// <returns></returns>
        public override string ToXmlString() => new StringBuilder()
                .AppendFormat("<xml><ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName)
                .AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName)
                .AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime.ToString())
                .AppendFormat("<MsgType><![CDATA[text]]></MsgType>")
                .AppendFormat("<Content><![CDATA[{0}]]></Content></xml>", Content)
                .ToString();
    }

    /// <summary>
    /// 回复图片消息
    /// </summary>
    public class WeChatResponseImage : WeChatResponseBase
    {
        /// <summary>
        /// 回复图片消息
        /// </summary>
        /// <param name="mediaId">图片消息媒体id</param>
        public WeChatResponseImage(string mediaId) => MediaId = mediaId;

        /// <summary>
        /// 图片消息媒体id
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public override ResponseMsgType MsgType => ResponseMsgType.Image;
        /// <summary>
        /// 回复Xml
        /// </summary>
        /// <returns></returns>
        public override string ToXmlString() => new StringBuilder()
                .AppendFormat("<xml><ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName)
                .AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName)
                .AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime.ToString())
                .AppendFormat("<MsgType><![CDATA[image]]></MsgType>")
                .AppendFormat("<Image><MediaId><![CDATA[{0}]]></MediaId></Image></xml>", MediaId)
                .ToString();
    }

    /// <summary>
    /// 回复语音消息
    /// </summary>
    public class WeChatResponseVoice : WeChatResponseBase
    {
        /// <summary>
        /// 回复语音消息
        /// </summary>
        /// <param name="mediaId">语音消息媒体id</param>
        public WeChatResponseVoice(string mediaId) => MediaId = mediaId;

        /// <summary>
        /// 语音消息媒体id
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public override ResponseMsgType MsgType => ResponseMsgType.Voice;
        /// <summary>
        /// 回复Xml
        /// </summary>
        /// <returns></returns>
        public override string ToXmlString() => new StringBuilder()
                .AppendFormat("<xml><ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName)
                .AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName)
                .AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime.ToString())
                .AppendFormat("<MsgType><![CDATA[voice]]></MsgType>")
                .AppendFormat("<Voice><MediaId><![CDATA[{0}]]></MediaId></Voice></xml>", MediaId)
                .ToString();
    }

    /// <summary>
    /// 回复视频消息（视频素材需要通过审核方可回复，否则微信不会有响应）
    /// </summary>
    public class WeChatResponseVideo : WeChatResponseBase
    {
        /// <summary>
        /// 回复视频消息
        /// </summary>
        /// <param name="mediaId">视频消息媒体id</param>
        /// <param name="title">视频消息的标题（可选）</param>
        /// <param name="description">视频消息的描述（可选）</param>
        public WeChatResponseVideo(string mediaId, string title = null, string description = null)
        {
            MediaId = mediaId;
            Title = title;
            Description = description;
        }

        /// <summary>
        /// 视频消息媒体id
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 视频消息的标题（可选）
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 视频消息的描述（可选）
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public override ResponseMsgType MsgType => ResponseMsgType.Video;
        /// <summary>
        /// 回复Xml
        /// </summary>
        /// <returns></returns>
        public override string ToXmlString() => new StringBuilder()
                .AppendFormat("<xml><ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName)
                .AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName)
                .AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime.ToString())
                .AppendFormat("<MsgType><![CDATA[video]]></MsgType>")
                .AppendFormat("<Video><MediaId><![CDATA[{0}]]></MediaId>", MediaId)
                .AppendFormat("<Title><![CDATA[{0}]]></Title>", Title)
                .AppendFormat("<Description><![CDATA[{0}]]></Description></Video></xml>", Description)
                .ToString();
    }

    /// <summary>
    /// 回复音乐消息
    /// </summary>
    public class WeChatResponseMusic : WeChatResponseBase
    {
        /// <summary>
        /// 回复音乐消息
        /// </summary>
        /// <param name="thumbMediaId">缩略图的媒体id</param>
        /// <param name="musicUrl">音乐链接（可选）</param>
        /// <param name="title">音乐标题（可选）</param>
        /// <param name="description">音乐描述（可选）</param>
        /// <param name="hqMusicUrl">高质量音乐链接，WIFI环境优先使用该链接播放音乐（可选）</param>
        public WeChatResponseMusic(string thumbMediaId, string musicUrl = null, string title = null, string description = null, string hqMusicUrl = null)
        {
            ThumbMediaId = thumbMediaId;
            Title = title;
            Description = description;
            MusicURL = musicUrl;
            HQMusicUrl = hqMusicUrl;
        }
        /// <summary>
        /// 缩略图的媒体id
        /// </summary>
        public string ThumbMediaId { get; set; }
        /// <summary>
        /// 音乐标题（可选）
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 音乐描述（可选）
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 音乐链接（可选）
        /// </summary>
        public string MusicURL { get; set; }
        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐（可选）
        /// </summary>
        public string HQMusicUrl { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public override ResponseMsgType MsgType => ResponseMsgType.Music;
        /// <summary>
        /// 回复Xml
        /// </summary>
        /// <returns></returns>
        public override string ToXmlString() => new StringBuilder()
                .AppendFormat("<xml><ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName)
                .AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName)
                .AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime.ToString())
                .AppendFormat("<MsgType><![CDATA[music]]></MsgType>")
                .AppendFormat("<Music><Title><![CDATA[{0}]]></Title>", Title)
                .AppendFormat("<Description><![CDATA[{0}]]></Description>", Description)
                .AppendFormat("<MusicUrl><![CDATA[{0}]]></MusicUrl>", MusicURL)
                .AppendFormat("<HQMusicUrl><![CDATA[{0}]]></HQMusicUrl>", HQMusicUrl)
                .AppendFormat("<ThumbMediaId><![CDATA[{0}]]></ThumbMediaId></Music></xml>", ThumbMediaId)
                .ToString();
    }


    /// <summary>
    /// 单条图文
    /// </summary>
    public class WeChatResponseNewItem
    {
        /// <summary>
        /// 单条图文
        /// </summary>
        /// <param name="title">图文消息标题（可选）</param>
        /// <param name="description">图文消息描述（可选）</param>
        /// <param name="picUrl">图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200（可选）</param>
        /// <param name="url">点击图文消息跳转链接（可选）</param>
        public WeChatResponseNewItem(string title = null, string description = null, string picUrl = null, string url = null)
        {
            Title = title;
            Description = description;
            PicUrl = picUrl;
            Url = url;
        }
        /// <summary>
        /// 图文消息标题（可选）
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图文消息描述（可选）
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200（可选）
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接（可选）
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// 回复图文消息
    /// </summary>
    public class WeChatResponseNews : WeChatResponseBase, ICollection<WeChatResponseNewItem>
    {
        /// <summary>
        /// 回复图文消息
        /// </summary>
        /// <param name="news"></param>
        public WeChatResponseNews(List<WeChatResponseNewItem> news) => News = news;

        /// <summary>
        /// 图文项目
        /// 多条图文消息信息，默认第一个item为大图,注意，如果图文数超过10，则将会无响应
        /// 序列化时已经限制最大图文数量为10
        /// </summary>
        public List<WeChatResponseNewItem> News { get; private set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public override ResponseMsgType MsgType => ResponseMsgType.News;
        /// <summary>
        /// 回复Xml
        /// </summary>
        /// <returns></returns>
        public override string ToXmlString()
        {
            var list = News.Take(10);
            var str = new StringBuilder()
                .AppendFormat("<xml><ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName)
                .AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName)
                .AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime.ToString())
                .AppendFormat("<MsgType><![CDATA[news]]></MsgType>")
                .AppendFormat("<ArticleCount>{0}</ArticleCount><Articles>", list.Count());
            foreach (var item in list)
                str.AppendFormat("<item><Title><![CDATA[{0}]]></Title>", item.Title)
                    .AppendFormat("<Description><![CDATA[{0}]]></Description>", item.Description)
                    .AppendFormat("<PicUrl><![CDATA[{0}]]></PicUrl>", item.PicUrl)
                    .AppendFormat("<Url><![CDATA[{0}]]></Url></item>", item.Url);
            return str.Append("</Articles></xml>").ToString();
        }

        #region ICollection接口实现
        /// <summary>
        /// 获取图文信息数量
        /// </summary>
        public int Count => ((ICollection<WeChatResponseNewItem>)News).Count;
        /// <summary>
        /// 指示集合是否只读
        /// </summary>
        public bool IsReadOnly => ((ICollection<WeChatResponseNewItem>)News).IsReadOnly;

        /// <summary>
        /// 添加图文信息
        /// </summary>
        /// <param name="item"></param>
        public void Add(WeChatResponseNewItem item) => ((ICollection<WeChatResponseNewItem>)News).Add(item);
        /// <summary>
        /// 清空图文信息
        /// </summary>
        public void Clear() => ((ICollection<WeChatResponseNewItem>)News).Clear();
        /// <summary>
        /// 是否存在指定图文信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(WeChatResponseNewItem item) => ((ICollection<WeChatResponseNewItem>)News).Contains(item);
        /// <summary>
        /// 覆盖指定位置的图文信息
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(WeChatResponseNewItem[] array, int arrayIndex) => ((ICollection<WeChatResponseNewItem>)News).CopyTo(array, arrayIndex);
        /// <summary>
        /// 移除指定的图文信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(WeChatResponseNewItem item) => ((ICollection<WeChatResponseNewItem>)News).Remove(item);
        /// <summary>
        /// 返回循环集合的枚举
        /// </summary>
        /// <returns></returns>
        public IEnumerator<WeChatResponseNewItem> GetEnumerator() => ((ICollection<WeChatResponseNewItem>)News).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((ICollection<WeChatResponseNewItem>)News).GetEnumerator();
        #endregion
    }

    /// <summary>
    /// 转接消息到客服平台
    /// </summary>
    public class WeChatResponseTransferToCustomerService : WeChatResponseBase
    {
        /// <summary>
        /// 转接消息到客服平台
        /// </summary>
        /// <param name="kfAccount">指定会话接入的客服账号</param>
        public WeChatResponseTransferToCustomerService(string kfAccount = null) => KFAccount = kfAccount;
        /// <summary>
        /// 指定会话接入的客服账号
        /// </summary>
        public string KFAccount { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public override ResponseMsgType MsgType => ResponseMsgType.TransferToCustomerService;

        /// <summary>
        /// 回复Xml
        /// </summary>
        /// <returns></returns>
        public override string ToXmlString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("<xml><ToUserName><![CDATA[{0}]]></ToUserName>", ToUserName);
            str.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", FromUserName);
            str.AppendFormat("<CreateTime>{0}</CreateTime>", CreateTime);
            str.AppendFormat("<MsgType><![CDATA[transfer_customer_service]]></MsgType>");
            if (!KFAccount.IsNullOrEmpty())
                str.AppendFormat("<TransInfo><KfAccount><![CDATA[{0}]]></KfAccount></TransInfo>", KFAccount);
            str.Append("</xml>");
            return str.ToString();
        }
    }
}
