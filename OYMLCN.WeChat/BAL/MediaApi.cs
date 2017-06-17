using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using OYMLCN.WeChat.Enums;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 素材接口通讯及逻辑辅助
    /// </summary>
    public static class MediaApi
    {
        /// <summary>
        /// 上传临时素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type">素材类型</param>
        /// <param name="filePath">素材文件路径</param>
        /// <returns></returns>
        public static MediaUpload MediaUpload(this AccessToken token, MediaType type, string filePath) =>
            Api.Media.Upload(token.access_token, type, filePath);
        /// <summary>
        /// 获取临时素材下载地址
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type">素材类型</param>
        /// <param name="media_id">素材的media_id</param>
        /// <exception cref="NotSupportedException">图文信息类型不被支持</exception>
        public static string MediaDownloadUrl(this AccessToken token, MediaType type, string media_id) =>
            Api.Media.GetUrl(token.access_token, type, media_id);
        /// <summary>
        /// 高清语音素材获取接口
        /// 获取从JSSDK的uploadVoice接口上传的临时语音素材下载地址
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">素材的media_id，即uploadVoice接口返回的serverID</param>
        public static void MediaSpeexDownloadUrl(this AccessToken token, string media_id) =>
            Api.Media.SpeexDownloadUrl(token.access_token, media_id);


        /// <summary>
        /// 新增永久图文素材（仅返回media_id）
        /// </summary>
        /// <param name="token"></param>
        /// <param name="items">图文列表</param>
        /// <returns>media_id</returns>
        public static string MaterialNewsAdd(this AccessToken token, params Article[] items) => token.MaterialNewsAdd(items.ToList());
        /// <summary>
        /// 新增永久图文素材（仅返回media_id）
        /// </summary>
        /// <param name="token"></param>
        /// <param name="items">图文列表</param>
        /// <returns>media_id</returns>
        public static string MaterialNewsAdd(this AccessToken token, List<Article> items) =>
            Api.Material.AddNews(token.access_token, items);

        /// <summary>
        /// 上传图文消息内的图片获取URL（仅返回url）
        /// 本接口所上传的图片不占用公众号的素材库中图片数量的5000个的限制。
        /// 图片仅支持jpg/png格式，大小必须在1MB以下。
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filePath">素材文件路径</param>
        /// <returns>url</returns>
        public static string MaterialUploadImage(this AccessToken token, string filePath) =>
            Api.Material.UploadImage(token.access_token, filePath);
        /// <summary>
        /// 新增其他类型永久素材（返回media_id、url）
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type">素材类型</param>
        /// <param name="filePath">素材文件路径</param>
        /// <param name="title">视频素材的标题（仅视频类型需要）</param>
        /// <param name="introduction">视频素材的描述（仅视频类型需要）</param>
        /// <returns></returns>
        public static MediaUpload MaterialUpload(this AccessToken token, MediaType type, string filePath, string title = null, string introduction = null) =>
            Api.Material.Add(token.access_token, type, filePath, title, introduction);
        /// <summary>
        /// 获取永久素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type">素材类型</param>
        /// <param name="media_id">素材的media_id</param>
        public static Stream MaterialDownload(this AccessToken token, MediaType type, string media_id)
        {
            if (type == MediaType.News)
                throw new NotSupportedException();
            else if (type == MediaType.Video)
                throw new NotSupportedException("下载视频请使用MaterialVideoDownloadUrl获取下载地址");
            else
                return Api.Material.Get(token.access_token, type, media_id);
        }
        /// <summary>
        /// 获取永久视频素材信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">素材的media_id</param>
        /// <returns></returns>
        public static MaterialVideoInfo MaterialVideoDownloadUrl(this AccessToken token, string media_id) =>
                Api.Material.GetVideoInfo(token.access_token, media_id);

        /// <summary>
        /// 获取永久图文素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">素材的media_id</param>
        /// <returns></returns>
        public static List<Article> MaterialNewsQuery(this AccessToken token, string media_id) =>
            Api.Material.GetNews(token.access_token, media_id);
        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">素材的media_id</param>
        /// <returns></returns>
        public static JsonResult MaterialDelete(this AccessToken token, string media_id) =>
            Api.Material.Delete(token.access_token, media_id);
        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id">要修改的图文消息的id</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <param name="item">图文项</param>
        /// <returns></returns>
        public static JsonResult MaterialNewUpdate(this AccessToken token, string media_id, int index, Article item) =>
            Api.Material.UpdateNews(token.access_token, media_id, index, item);


        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static MaterialCount MaterialCount(this AccessToken token) =>
            Api.Material.Count(token.access_token);
        /// <summary>
        /// 获取素材列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type">素材的类型，图片（image）、视频（video）、语音 （voice））</param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <returns></returns>
        public static MaterialList<MaterialItem> MaterialMediaListQuery(this AccessToken token, MediaType type, int offset, int count) =>
             Api.Material.BatchGet(token.access_token, type, offset, count);
        /// <summary>
        /// 获取图文素材列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <returns></returns>
        public static MaterialList<MaterialNews> MaterialNewsListQuery(this AccessToken token, int offset, int count) =>
             Api.Material.BatchGetNews(token.access_token, offset, count);
    }
}
