using OYMLCN.WeChat.Model;
using System.Collections.Generic;
using System.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 群发接口辅助通讯逻辑
    /// </summary>
    public static class MassMessageApi
    {
        /// <summary>
        /// 上传图文消息内的图片获取URL
        /// 有效字段：url
        /// 请注意，本接口所上传的图片不占用公众号的素材库中图片数量的5000个的限制。图片仅支持jpg/png格式，大小必须在1MB以下。
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string MassMessageImageUpload(this AccessToken token, string filePath) =>
            Api.Material.UploadImage(token.access_token, filePath);
        /// <summary>
        /// 上传图文消息素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="items">图文列表</param>
        /// <returns></returns>
        public static MediaUpload MassMessageNewsUpload(this AccessToken token, params Article[] items) =>
            token.MassMessageNewsUpload(items.ToList());
        /// <summary>
        /// 上传图文消息素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="items">图文列表</param>
        /// <returns></returns>
        public static MediaUpload MassMessageNewsUpload(this AccessToken token, List<Article> items) =>
            Api.Mass.UploadNews(token.access_token, items);


        /// <summary>
        /// 群发图文消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">图文Id</param>
        /// <param name="tag_id">群发到的标签的tag_id，参加用户管理中用户分组接口，可不填写tag_id以发送到全部用户</param>
        /// <param name="sendIgnoreReprint">图文消息被判定为转载时，是否继续群发。true为继续群发（转载），false为停止群发。该参数默认为false。</param>
        /// <returns></returns>
        public static MassResult MassNews(this AccessToken token, string media_id, int tag_id = 0, bool sendIgnoreReprint = false) =>
             Api.Mass.SendMpNews(token.access_token, media_id, tag_id, sendIgnoreReprint);
        /// <summary>
        /// 群发图文消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">图文Id</param>
        /// <param name="sendIgnoreReprint">图文消息被判定为转载时，是否继续群发。true为继续群发（转载），false为停止群发。该参数默认为false。</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassResult MassNewsByOpenId(this AccessToken token, string media_id, bool sendIgnoreReprint = false, params string[] openid) =>
            token.MassNewsByOpenId(media_id, sendIgnoreReprint, openid.ToList());
        /// <summary>
        /// 群发图文消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">图文Id</param>
        /// <param name="sendIgnoreReprint">图文消息被判定为转载时，是否继续群发。true为继续群发（转载），false为停止群发。该参数默认为false。</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassResult MassNewsByOpenId(this AccessToken token, string media_id, bool sendIgnoreReprint = false, List<string> openid = null) =>
             Api.Mass.SendMpNews(token.access_token, media_id, openid, sendIgnoreReprint);
        /// <summary>
        /// 图文消息预览
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">图文Id</param>
        /// <param name="openid">预览账号的OpenId</param>
        /// <param name="wxname">预览微信账号</param>
        /// <returns></returns>
        public static MassResult MassNewsPreview(this AccessToken token, string media_id, string openid = null, string wxname = null) =>
             Api.Mass.PreviewMpNews(token.access_token, media_id, openid, wxname);



        /// <summary>
        /// 群发文本消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="text">文本信息</param>
        /// <param name="tag_id">群发到的标签的tag_id，参加用户管理中用户分组接口，可不填写tag_id以发送到全部用户</param>
        /// <returns></returns>
        public static MassResult MassText(this AccessToken token, string text, int tag_id = 0) =>
            Api.Mass.SendText(token.access_token, text, tag_id);
        /// <summary>
        /// 群发文本消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="text">文本信息</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassResult MassTextByOpenId(this AccessToken token, string text, params string[] openid) => token.MassTextByOpenId(text, openid.ToList());
        /// <summary>
        /// 群发文本消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="text">文本信息</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassResult MassTextByOpenId(this AccessToken token, string text, List<string> openid) =>
            Api.Mass.SendText(token.access_token, text, openid);
        /// <summary>
        /// 文本消息预览
        /// </summary>
        /// <param name="token"></param>
        /// <param name="text">文本信息</param>
        /// <param name="openid">预览账号的OpenId</param>
        /// <param name="wxname">预览微信账号</param>
        /// <returns></returns>
        public static MassResult MassTextPreview(this AccessToken token, string text, string openid = null, string wxname = null) =>
            Api.Mass.PreviewText(token.access_token, text, openid, wxname);



        /// <summary>
        /// 群发语音消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="tag_id">群发到的标签的tag_id，参加用户管理中用户分组接口，可不填写tag_id以发送到全部用户</param>
        /// <returns></returns>
        public static MassResult MassVoice(this AccessToken token, string media_id, int tag_id = 0) =>
            Api.Mass.SendVoice(token.access_token, media_id, tag_id);
        /// <summary>
        /// 群发语音消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassResult MassVoiceByOpenId(this AccessToken token, string media_id, params string[] openid) => token.MassTextByOpenId(media_id, openid.ToList());
        /// <summary>
        /// 群发语音消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassResult MassVoiceByOpenId(this AccessToken token, string media_id, List<string> openid) =>
            Api.Mass.SendVoice(token.access_token, media_id, openid);
        /// <summary>
        /// 语音消息预览
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="openid">预览账号的OpenId</param>
        /// <param name="wxname">预览微信账号</param>
        /// <returns></returns>
        public static MassResult MassVoicePreview(this AccessToken token, string media_id, string openid = null, string wxname = null) =>
            Api.Mass.PreviewVoice(token.access_token, media_id, openid, wxname);



        /// <summary>
        /// 群发图片消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="tag_id">群发到的标签的tag_id，参加用户管理中用户分组接口，可不填写tag_id以发送到全部用户</param>
        /// <returns></returns>
        public static MassResult MassImage(this AccessToken token, string media_id, int tag_id = 0) =>
            Api.Mass.SendImage(token.access_token, media_id, tag_id);
        /// <summary>
        /// 群发图片消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassResult MassImageByOpenId(this AccessToken token, string media_id, params string[] openid) => token.MassTextByOpenId(media_id, openid.ToList());
        /// <summary>
        /// 群发图片消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassResult MassImageByOpenId(this AccessToken token, string media_id, List<string> openid) =>
            Api.Mass.SendImage(token.access_token, media_id, openid);
        /// <summary>
        /// 图片消息预览
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">此处media_id需通过基础支持中的上传下载多媒体文件来得到</param>
        /// <param name="openid">预览账号的OpenId</param>
        /// <param name="wxname">预览微信账号</param>
        /// <returns></returns>
        public static MassResult MassImagePreview(this AccessToken token, string media_id, string openid = null, string wxname = null) =>
            Api.Mass.PreviewImage(token.access_token, media_id, openid, wxname);


        /// <summary>
        /// 获取群发的视频MediaId
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">原视频的MediaId</param>
        /// <param name="title">群发视频标题</param>
        /// <param name="description">群发视频描述</param>
        /// <returns></returns>
        public static MediaUpload MassMessageGetVideoMediaId(this AccessToken token, string media_id, string title, string description) =>
            Api.Mass.SendVideoPreGetMediaId(token.access_token, media_id, title, description);
        /// <summary>
        /// 群发视频消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">从方法AccessToken.MassMessageGetVideoMediaId获取到的MediaId</param>
        /// <param name="tag_id">群发到的标签的tag_id，参加用户管理中用户分组接口，可不填写tag_id以发送到全部用户</param>
        /// <returns></returns>
        public static MassResult MassVideo(this AccessToken token, string media_id, int tag_id = 0) =>
            Api.Mass.SendVideo(token.access_token, media_id, tag_id);
        /// <summary>
        /// 群发视频消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">原视频的MediaId</param>
        /// <param name="title">群发视频标题</param>
        /// <param name="description">群发视频描述</param>
        /// <param name="tag_id">群发到的标签的tag_id，参加用户管理中用户分组接口，可不填写tag_id以发送到全部用户</param>
        /// <returns></returns>
        public static MassResult MassVideo(this AccessToken token, string media_id, string title, string description, int tag_id = 0) =>
            Api.Mass.SendVideo(token.access_token, token.MassMessageGetVideoMediaId(media_id, title, description).media_id, tag_id);
        /// <summary>
        /// 群发视频消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">原视频的MediaId</param>
        /// <param name="title">群发视频标题</param>
        /// <param name="description">群发视频描述</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassResult MassVideoByOpenId(this AccessToken token, string media_id, string title, string description, params string[] openid) => token.MassTextByOpenId(media_id, openid.ToList());
        /// <summary>
        /// 群发视频消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">原视频的MediaId</param>
        /// <param name="title">群发视频标题</param>
        /// <param name="description">群发视频描述</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassResult MassVideoByOpenId(this AccessToken token, string media_id, string title, string description, List<string> openid) =>
            Api.Mass.SendVideo(token.access_token, media_id, title, description, openid);
        /// <summary>
        /// 视频消息预览
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">从方法AccessToken.MassMessageGetVideoMediaId获取到的MediaId</param>
        /// <param name="openid">预览账号的OpenId</param>
        /// <param name="wxname">预览微信账号</param>
        /// <returns></returns>
        public static MassResult MassVideoPreview(this AccessToken token, string media_id, string openid = null, string wxname = null) =>
            Api.Mass.PreviewVideo(token.access_token, media_id, openid, wxname);
        /// <summary>
        /// 视频消息预览
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">原视频的MediaId</param>
        /// <param name="title">群发视频标题</param>
        /// <param name="description">群发视频描述</param>
        /// <param name="openid">预览账号的OpenId</param>
        /// <param name="wxname">预览微信账号</param>
        /// <returns></returns>
        public static MassResult MassVideoPreview(this AccessToken token, string media_id, string title, string description, string openid = null, string wxname = null) =>
            Api.Mass.PreviewVideo(token.access_token, media_id, openid, wxname);



        /// <summary>
        /// 群发卡券消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="card_id">需要通过卡券接口获得</param>
        /// <param name="tag_id">群发到的标签的tag_id，参加用户管理中用户分组接口，可不填写tag_id以发送到全部用户</param>
        /// <returns></returns>
        public static MassResult MassCard(this AccessToken token, string card_id, int tag_id = 0) =>
            Api.Mass.SendCard(token.access_token, card_id, tag_id);
        /// <summary>
        /// 群发卡券消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="card_id">需要通过卡券接口获得</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassResult MassCardByOpenId(this AccessToken token, string card_id, params string[] openid) => token.MassTextByOpenId(card_id, openid.ToList());
        /// <summary>
        /// 群发卡券消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="card_id">需要通过卡券接口获得</param>
        /// <param name="openid">填写图文消息的接收者，一串OpenID列表，OpenID最少2个，最多10000个</param>
        /// <returns></returns>
        public static MassResult MassCardByOpenId(this AccessToken token, string card_id, List<string> openid) =>
            Api.Mass.SendCard(token.access_token, card_id, openid);
        /// <summary>
        /// 卡券消息预览
        /// </summary>
        /// <param name="token"></param>
        /// <param name="card_id">需要通过卡券接口获得</param>
        /// <param name="code"></param>
        /// <param name="timestamp"></param>
        /// <param name="signature"></param>
        /// <param name="openid">预览账号的OpenId</param>
        /// <param name="wxname">预览微信账号</param>
        /// <returns></returns>
        public static MassResult MassCardPreview(this AccessToken token, string card_id, string code, string timestamp, string signature, string openid = null, string wxname = null) =>
            Api.Mass.PreviewCard(token.access_token, card_id, code, timestamp, signature, openid, wxname);



        /// <summary>
        /// 删除群发
        /// </summary>
        /// <param name="token"></param>
        /// <param name="msgId">发送出去的消息ID</param>
        /// <param name="article_idx">图文序号</param>
        /// <returns></returns>
        public static JsonResult MassMessageSentDelete(this AccessToken token, long msgId, byte? article_idx = null) =>
            Api.Mass.SentDelete(token.access_token, msgId, article_idx);


        /// <summary>
        /// 查询群发消息发送状态
        /// </summary>
        /// <param name="token"></param>
        /// <param name="msgId">群发消息后返回的消息id</param>
        /// <returns></returns>
        public static MassState MassMessageSentStateQuery(this AccessToken token, long msgId) =>
            Api.Mass.MassMessageSentStateQuery(token.access_token, msgId);
    }
}
