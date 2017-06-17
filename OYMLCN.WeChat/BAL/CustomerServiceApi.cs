using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 客服接口通讯辅助
    /// </summary>
    public static class CustomerServiceApi
    {
        #region 客服消息接口
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="text">文本消息内容</param>
        /// <param name="kf_account">指定消息完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendText(this AccessToken token, string openid, string text, string kf_account = null) =>
            Api.CustomerService.MessageSend.Text(token.access_token, openid, text, kf_account);

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="media_id">发送的图片的媒体ID</param>
        /// <param name="kf_account">指定消息完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendImage(this AccessToken token, string openid, string media_id, string kf_account = null) =>
             Api.CustomerService.MessageSend.Image(token.access_token, openid, media_id, kf_account);

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="media_id">发送的语音的媒体ID </param>
        /// <param name="kf_account">指定消息完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendVoice(this AccessToken token, string openid, string media_id, string kf_account = null) =>
             Api.CustomerService.MessageSend.Voice(token.access_token, openid, media_id, kf_account);

        /// <summary>
        /// 发送视频消息（视频素材需要通过审核方可发送，否则会发送失败）
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="media_id">发送的视频的媒体ID</param>
        /// <param name="thumbMediaId">缩略图的媒体ID</param>
        /// <param name="title">视频消息的标题（可选）</param>
        /// <param name="description">视频消息的描述（可选）</param>
        /// <param name="kf_account">指定消息完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendVideo(this AccessToken token, string openid, string media_id, string thumbMediaId, string title = null, string description = null, string kf_account = null) =>
             Api.CustomerService.MessageSend.Video(token.access_token, openid, media_id, thumbMediaId, title, description, kf_account);

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
        /// <param name="kf_account">指定消息完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendMusic(this AccessToken token, string openid, string thumbMediaId, string musicUrl, string hqMusicUrl, string title = null, string description = null, string kf_account = null) =>
             Api.CustomerService.MessageSend.Music(token.access_token, openid, thumbMediaId, musicUrl, hqMusicUrl, title, description, kf_account);

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="kf_account">指定消息完整客服帐号（若不指定则传入null或空字符串）</param>
        /// <param name="news">图文项目，图文消息条数限制在10条以内，超过则只发送10条</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendNews(this AccessToken token, string openid, string kf_account = null, params Api.CustomerService.MessageSend.Article[] news) =>
            CustomerServiceSendNews(token, openid, news.ToList(), kf_account);
        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="news">图文项目，图文消息条数限制在10条以内，超过则只发送10条</param>
        /// <param name="kf_account">指定消息完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendNews(this AccessToken token, string openid, List<Api.CustomerService.MessageSend.Article> news, string kf_account = null) =>
             Api.CustomerService.MessageSend.News(token.access_token, openid, news, kf_account);
        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="media_id">图文消息（点击跳转到图文消息页）的媒体ID</param>
        /// <param name="kf_account">指定消息完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendMpNews(this AccessToken token, string openid, string media_id, string kf_account = null) =>
             Api.CustomerService.MessageSend.MpNews(token.access_token, openid, media_id, kf_account);


        /// <summary>
        /// 发送卡券
        /// </summary>
        /// <param name="token">调用接口凭证</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="card_id">卡券Id</param>
        /// <param name="kf_account">指定消息完整客服帐号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSendCard(this AccessToken token, string openid, string card_id, string kf_account = null) =>
             Api.CustomerService.MessageSend.Card(token.access_token, openid, card_id, kf_account);
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
        public static KefuList.Info[] CustomerServiceAccountQuery(this AccessToken token) =>
            Api.CustomerService.GetList(token.access_token);
        /// <summary>
        /// 获取在线客服信息
        /// 有效列表：kf_online_list
        /// 有效信息字段：
        /// kf_account、status、kf_id、accepted_case
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static KefuList.Info[] CustomerServiceAccountOnlineQuery(this AccessToken token) =>
            Api.CustomerService.GetOnlineList(token.access_token);


        /// <summary>
        /// 添加客服帐号
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kf_account">完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <param name="nickName">客服昵称，最长16个字</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceAccountAdd(this AccessToken token, string kf_account, string nickName) =>
            Api.CustomerService.Account.Add(token.access_token, kf_account, nickName);
        /// <summary>
        /// 邀请绑定客服帐号
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kf_account">完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <param name="inviteWX">接收绑定邀请的客服微信号</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceAccountInvite(this AccessToken token, string kf_account, string inviteWX) =>
            Api.CustomerService.Account.Invite(token.access_token, kf_account, inviteWX);
        /// <summary>
        /// 设置客服信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kf_account">完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <param name="nickName">客服昵称，最长16个字</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceAccountUpdate(this AccessToken token, string kf_account, string nickName) =>
            Api.CustomerService.Account.Update(token.access_token, kf_account, nickName);
        /// <summary>
        /// 上传客服头像
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kf_account">完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <param name="filePath">头像图片文件必须是jpg格式，推荐使用640*640大小的图片以达到最佳效果</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceAccountUploadHeadImg(this AccessToken token, string kf_account, string filePath) =>
            Api.CustomerService.Account.UploadHeadImg(token.access_token, kf_account, filePath);

        /// <summary>
        /// 删除客服帐号
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kf_account">完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceAccountDelete(this AccessToken token, string kf_account) =>
            Api.CustomerService.Account.Delete(token.access_token, kf_account);

        #endregion



        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kf_account">完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <param name="openid">粉丝的openid</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSessionCreate(this AccessToken token, string kf_account, string openid) =>
            Api.CustomerService.Session.Create(token.access_token, kf_account, openid);
        /// <summary>
        /// 关闭会话
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kf_account">完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <param name="openid">粉丝的openid</param>
        /// <returns></returns>
        public static JsonResult CustomerServiceSessionClose(this AccessToken token, string kf_account, string openid) =>
            Api.CustomerService.Session.Close(token.access_token, kf_account, openid);
        /// <summary>
        /// 获取客户会话状态
        /// 有效字段：kf_account、createtime
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid">粉丝的openid</param>
        /// <returns></returns>
        public static KefuSessionList.Session CustomerServiceSessionQuery(this AccessToken token, string openid) =>
            Api.CustomerService.Session.Get(token.access_token, openid);
        /// <summary>
        /// 获取客服会话列表
        /// 有效列表：sessionlist
        /// 有效字段：createtime、openid
        /// </summary>
        /// <param name="token"></param>
        /// <param name="kf_account">完整客服帐号(格式为：帐号前缀@公众号微信号，帐号前缀最多10个字符，必须是英文、数字字符或者下划线，后缀为公众号微信号，长度不超过30个字符)</param>
        /// <returns></returns>
        public static KefuSessionList.Session[] CustomerServiceSessionListQuery(this AccessToken token, string kf_account) =>
            Api.CustomerService.Session.GetList(token.access_token, kf_account);
        /// <summary>
        /// 获取未接入会话列表
        /// 有效列表：waitcaselist
        /// 未接入会话数量：count
        /// 有效字段：latest_time、openid
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static KefuSessionList.Session[] CustomerServiceWaitingListQuery(this AccessToken token) =>
            Api.CustomerService.Session.GetWaitCase(token.access_token);
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
        public static KefuRecordList CustomerServiceRecordQuery(this AccessToken token, DateTime start, DateTime end, long msgid = 1, int number = 10000) =>
             Api.CustomerService.Record.GetMsgList(token.access_token, start, end, msgid, number);
    }
}
