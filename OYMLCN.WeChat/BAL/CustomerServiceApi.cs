using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 客服接口通讯辅助
    /// </summary>
    public static class CustomerServiceApi
    {
        #region 客服消息接口
        /// <summary>
        /// 客服接口调用地址
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static string ApiUrl(this AccessToken token) => CoreApi.ApiUrl("/cgi-bin/message/custom/send?access_token={0}", token.access_token);
        private static JsonResult Send(this AccessToken token, string json)
        {
            var result = HttpClient.PostJsonString(ApiUrl(token), json).DeserializeJsonString<JsonResult>();
            if (result.Success)
                return result;
            throw result.Error;
        }

        /// <summary>
        /// 以指定客服身份发送客服消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="json"></param>
        /// <param name="kfName"></param>
        /// <returns></returns>
        public static string SendAs(AccessToken token, string json, string kfName)
        {
            if (kfName.IsNullOrEmpty())
                return json;
            else
                return json.Substring(0, json.Length - 1) + ",\"customservice\":{\"kf_account\":\"" + kfName.CompleteKefuName(token) + "\"}}";
        }

        /// <summary>
        /// 创建客服消息Json字符串
        /// </summary>
        /// <param name="openid">普通用户openid</param>
        /// <param name="text">文本消息内容</param>
        /// <returns></returns>
        public static string TextJson(string openid, string text) =>
            "{\"touser\":\"" + openid + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + text + "\"}}";
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="text">文本消息内容</param>
        /// <param name="kfName">指定消息客服名称(前缀)或完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendText(this AccessToken token, string openid, string text, string kfName = null) =>
            token.Send(SendAs(token, TextJson(openid, text), kfName));

        /// <summary>
        /// 创建客服消息Json字符串
        /// </summary>
        /// <param name="openid">普通用户openid</param>
        /// <param name="mediaId">发送的图片的媒体ID</param>
        /// <returns></returns>
        public static string ImageJson(string openid, string mediaId) =>
            "{\"touser\":\"" + openid + "\",\"msgtype\":\"image\",\"image\":{\"media_id\":\"" + mediaId + "\"}}";
        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="mediaId">发送的图片的媒体ID</param>
        /// <param name="kfName">指定消息客服名称(前缀)或完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendImage(this AccessToken token, string openid, string mediaId, string kfName = null) =>
            token.Send(SendAs(token, ImageJson(openid, mediaId), kfName));

        /// <summary>
        /// 创建客服消息Json字符串
        /// </summary>
        /// <param name="openid">普通用户openid</param>
        /// <param name="mediaId">发送的语音的媒体ID</param>
        /// <returns></returns>
        public static string VoiceJson(string openid, string mediaId) =>
            "{\"touser\":\"" + openid + "\",\"msgtype\":\"voice\",\"voice\":{\"media_id\":\"" + mediaId + "\"}}";
        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="mediaId">发送的语音的媒体ID </param>
        /// <param name="kfName">指定消息客服名称(前缀)或完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendVoice(this AccessToken token, string openid, string mediaId, string kfName = null) =>
            token.Send(SendAs(token, VoiceJson(openid, mediaId), kfName));

        /// <summary>
        /// 创建客服消息Json字符串
        /// </summary>
        /// <param name="openid">普通用户openid</param>
        /// <param name="mediaId">发送的视频的媒体ID</param>
        /// <param name="thumbMediaId">缩略图的媒体ID</param>
        /// <param name="title">视频消息的标题（可选）</param>
        /// <param name="description">视频消息的描述（可选）</param>
        /// <returns></returns>
        public static string VideoJson(string openid, string mediaId, string thumbMediaId, string title = null, string description = null) =>
            "{\"touser\":\"" + openid + "\",\"msgtype\":\"video\",\"video\":{\"media_id\":\"" + mediaId + "\"," +
                "\"thumb_media_id\":\"" + thumbMediaId + "\",\"title\":\"" + title + "\",\"description\":\"" + description + "\"}}";
        /// <summary>
        /// 发送视频消息（视频素材需要通过审核方可发送，否则会发送失败）
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="mediaId">发送的视频的媒体ID</param>
        /// <param name="thumbMediaId">缩略图的媒体ID</param>
        /// <param name="title">视频消息的标题（可选）</param>
        /// <param name="description">视频消息的描述（可选）</param>
        /// <param name="kfName">指定消息客服名称(前缀)或完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendVideo(this AccessToken token, string openid, string mediaId, string thumbMediaId, string title = null, string description = null, string kfName = null) =>
            token.Send(SendAs(token, VideoJson(openid, mediaId, thumbMediaId, title, description), kfName));

        /// <summary>
        /// 创建客服消息Json字符串
        /// </summary>
        /// <param name="openid">普通用户openid</param>
        /// <param name="thumbMediaId">缩略图的媒体ID</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="title">音乐标题（可选）</param>
        /// <param name="description">音乐描述（可选）</param>
        /// <returns></returns>
        public static string MusicJson(string openid, string thumbMediaId, string musicUrl, string hqMusicUrl, string title = null, string description = null) =>
             "{\"touser\":\"" + openid + "\",\"msgtype\":\"music\",\"music\":{\"title\":\"" + title + "\",\"description\":\"" + description +
                "\",\"musicurl\":\"" + musicUrl + "\",\"hqmusicurl\":\"" + hqMusicUrl + "\",\"thumb_media_id\":\"" + thumbMediaId + "\"}}";
        /// <summary>
        /// 发送音乐消息
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="thumbMediaId">缩略图的媒体ID</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="title">音乐标题（可选）</param>
        /// <param name="description">音乐描述（可选）</param>
        /// <param name="kfName">指定消息客服名称(前缀)或完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendMusic(this AccessToken token, string openid, string thumbMediaId, string musicUrl, string hqMusicUrl, string title = null, string description = null, string kfName = null) =>
            token.Send(SendAs(token, MusicJson(openid, thumbMediaId, musicUrl, hqMusicUrl, title, description), kfName));

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="kfName">指定消息客服名称(前缀)或完整客服帐号（若不指定则传入null或空字符串）</param>
        /// <param name="news">图文项目，图文消息条数限制在10条以内，超过则只发送10条</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendNews(this AccessToken token, string openid, string kfName = null, params WeChatResponseNewItem[] news) =>
            CustomerServiceSendNews(token, openid, news.ToList(), kfName);
        /// <summary>
        /// 创建客服消息Json字符串
        /// </summary>
        /// <param name="openid">普通用户openid</param>
        /// <param name="news">图文项目，图文消息条数限制在10条以内，超过则只发送10条</param>        /// <returns></returns>
        public static string NewsJson(string openid, List<WeChatResponseNewItem> news)
        {
            StringBuilder str = new StringBuilder();
            var list = news.Take(10);
            foreach (var item in list)
                str.Append("{\"title\":\"" + item.Title + "\",\"description\":\"" + item.Description +
                    "\",\"url\":\"" + item.Url + "\",\"picurl\":\"" + item.PicUrl + "\"},");
            return "{\"touser\":\"" + openid + "\",\"msgtype\":\"news\",\"news\":{\"articles\":[" + str.ToString().Remove(str.Length - 1) + "]}}";
        }
        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="news">图文项目，图文消息条数限制在10条以内，超过则只发送10条</param>
        /// <param name="kfName">指定消息客服名称(前缀)或完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendNews(this AccessToken token, string openid, List<WeChatResponseNewItem> news, string kfName = null) =>
            token.Send(SendAs(token, NewsJson(openid, news), kfName));

        /// <summary>
        /// 创建客服消息Json字符串
        /// </summary>
        /// <param name="openid">普通用户openid</param>
        /// <param name="mediaId">图文消息（点击跳转到图文消息页）的媒体ID</param>        /// <returns></returns>
        public static string MpNewsJson(string openid, string mediaId) =>
            "{\"touser\":\"" + openid + "\",\"msgtype\":\"mpnews\",\"mpnews\":{\"media_id\":\"" + mediaId + "\"}}";
        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="mediaId">图文消息（点击跳转到图文消息页）的媒体ID</param>        /// <returns></returns>
        /// <param name="kfName">指定消息客服名称(前缀)或完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendMpNews(this AccessToken token, string openid, string mediaId, string kfName = null) =>
            token.Send(SendAs(token, MpNewsJson(openid, mediaId), kfName));

        /// <summary>
        /// 创建客服消息Json字符串
        /// </summary>
        /// <param name="openid">普通用户openid</param>
        /// <param name="cardId">卡券Id</param>
        /// <returns></returns>
        public static string CardJson(string openid, string cardId) =>
            "{\"touser\":\"" + openid + "\",\"msgtype\":\"wxcard\",\"wxcard\":{\"card_id\":\"" + cardId + "\"}}";
        /// <summary>
        /// 发送卡券
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="cardId">卡券Id</param>
        /// <param name="kfName">指定消息客服名称(前缀)或完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendCard(this AccessToken token, string openid, string cardId, string kfName = null) =>
            token.Send(SendAs(token, CardJson(openid, cardId), kfName));
        #endregion



        #region 客服帐号操作
        /// <summary>
        /// 获取所有客服账号基本信息
        /// 有效列表：kf_list
        /// 有效信息字段：
        /// kf_account、kf_nick、kf_id、kf_headimgurl、kf_wx、
        /// invite_wx、invite_expire_time、invite_status
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static KefuList CustomerServiceAccountQuery(this AccessToken token) =>
            HttpClient.GetString(CoreApi.ApiUrl("/cgi-bin/customservice/getkflist?access_token={0}", token.access_token)).DeserializeJsonString<KefuList>();
        /// <summary>
        /// 获取在线客服信息
        /// 有效列表：kf_online_list
        /// 有效信息字段：
        /// kf_account、status、kf_id、accepted_case
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static KefuList CustomerServiceAccountOnlineQuery(this AccessToken token) =>
            HttpClient.GetString(CoreApi.ApiUrl("/cgi-bin/customservice/getonlinekflist?access_token={0}", token.access_token)).DeserializeJsonString<KefuList>();


        private static string CompleteKefuName(this string name, AccessToken token) =>
            name.Contains("@") ? name : string.Format("{0}@{1}", name, token.Config.Name);

        /// <summary>
        /// 添加客服帐号
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kfName">客服名称(前缀)或完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <param name="nickName">客服昵称，最长16个字</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceAccountAdd(this AccessToken token, string kfName, string nickName) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/customservice/kfaccount/add?access_token={0}", token.access_token),
                "{\"kf_account\":\"" + kfName.CompleteKefuName(token) + "\",\"nickname\":\"" + nickName + "\"}"
                ).DeserializeJsonString<JsonResult>();
        /// <summary>
        /// 邀请绑定客服帐号
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kfName">客服名称(前缀)或完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <param name="inviteWX">接收绑定邀请的客服微信号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceAccountInvite(this AccessToken token, string kfName, string inviteWX) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/customservice/kfaccount/inviteworker?access_token={0}", token.access_token),
                "{\"kf_account\":\"" + kfName.CompleteKefuName(token) + "\",\"invite_wx\":\"" + inviteWX + "\"}"
                ).DeserializeJsonString<JsonResult>();
        /// <summary>
        /// 设置客服信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kfName">客服名称(前缀)或完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <param name="nickName">客服昵称，最长16个字</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceAccountUpdate(this AccessToken token, string kfName, string nickName) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/customservice/kfaccount/update?access_token={0}", token.access_token),
                "{\"kf_account\":\"" + kfName.CompleteKefuName(token) + "\",\"nickname\":\"" + nickName + "\"}"
                ).DeserializeJsonString<JsonResult>();
        /// <summary>
        /// 上传客服头像
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kfName">客服名称(前缀)或完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <param name="filePath">头像图片文件必须是jpg格式，推荐使用640*640大小的图片以达到最佳效果</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceAccountUploadHeadImg(this AccessToken token, string kfName, string filePath)
        {
            if (!filePath.EndsWith(".jpg"))
                throw new FormatException("头像图片文件必须是jpg格式");
            var file = new Dictionary<string, string>();
            file["media"] = filePath;
            return HttpClient.CurlPost(CoreApi.ApiUrl("/customservice/kfaccount/uploadheadimg?access_token={0}&kf_account={1}", token.access_token, kfName.CompleteKefuName(token)), queryDir: file).ReadToEnd().DeserializeJsonString<JsonResult>();
        }
        /// <summary>
        /// 删除客服帐号
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kfName">客服名称(前缀)或完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceAccountDelete(this AccessToken token, string kfName) =>
            HttpClient.GetString(CoreApi.ApiUrl("/customservice/kfaccount/del?access_token={0}&kf_account={1}", token.access_token, kfName.CompleteKefuName(token).EncodeAsUrlData())).DeserializeJsonString<JsonResult>();

        #endregion



        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kfName">客服名称(前缀)或完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <param name="openid">粉丝的openid</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSessionCreate(this AccessToken token, string kfName, string openid) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/customservice/kfsession/create?access_token={0}", token.access_token), "{\"kf_account\":\"" + kfName.CompleteKefuName(token) + "\",\"openid\":\"" + openid + "\"}").DeserializeJsonString<JsonResult>();
        /// <summary>
        /// 关闭会话
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kfName">客服名称(前缀)或完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <param name="openid">粉丝的openid</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSessionClose(this AccessToken token, string kfName, string openid) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/customservice/kfsession/close?access_token={0}", token.access_token), "{\"kf_account\":\"" + kfName.CompleteKefuName(token) + "\",\"openid\":\"" + openid + "\"}").DeserializeJsonString<JsonResult>();
        /// <summary>
        /// 获取客户会话状态
        /// 有效字段：kf_account、createtime
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid">粉丝的openid</param>
        /// <returns></returns>
        public static KefuSession CustomerServiceSessionQuery(this AccessToken token, string openid) =>
            HttpClient.GetString(CoreApi.ApiUrl("/customservice/kfsession/getsession?access_token={0}&openid={1}", token.access_token, openid)).DeserializeJsonString<KefuSession>();
        /// <summary>
        /// 获取客服会话列表
        /// 有效列表：sessionlist
        /// 有效字段：createtime、openid
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kfName">客服名称(前缀)或完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <returns></returns>
        public static KefuSessionList CustomerServiceSessionListQuery(this AccessToken token, string kfName) =>
            HttpClient.GetString(CoreApi.ApiUrl("/customservice/kfsession/getsessionlist?access_token={0}&kf_account={1}", token.access_token, kfName.CompleteKefuName(token).EncodeAsUrlData())).DeserializeJsonString<KefuSessionList>();
        /// <summary>
        /// 获取未接入会话列表
        /// 有效列表：waitcaselist
        /// 未接入会话数量：count
        /// 有效字段：latest_time、openid
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static KefuSessionList CustomerServiceWaitingListQuery(this AccessToken token) =>
            HttpClient.GetString(CoreApi.ApiUrl("/customservice/kfsession/getwaitcase?access_token={0}", token.access_token)).DeserializeJsonString<KefuSessionList>();
        /// <summary>
        /// 获取聊天记录
        /// 请求的number(10000)和返回的number(10000)一样，
        /// 该时间段可能还有聊天记录未获取，将msgid填进下次请求中
        /// </summary>
        /// <param name="token"></param>
        /// <param name="start">起始时间</param>
        /// <param name="end">结束时间，每次查询时段不能超过24小时</param>
        /// <param name="msgid">消息id顺序从小到大，从1开始</param>
        /// <param name="number">每次获取条数，最多10000条</param>
        /// <returns></returns>
        public static KefuRecordList CustomerServiceRecordQuery(this AccessToken token, DateTime start, DateTime end, Int64 msgid = 1, int number = 10000) =>
             HttpClient.PostJsonString(CoreApi.ApiUrl("/customservice/msgrecord/getmsglist?access_token={0}", token.access_token),
                "{\"starttime\":" + start.ToTimestamp().ToString() + ",\"endtime\":" + end.ToTimestamp().ToString() +
                ",\"msgid\":" + msgid.ToString() + ",\"number\":" + number.ToString() + "}"
                ).DeserializeJsonString<KefuRecordList>();
    }
}
