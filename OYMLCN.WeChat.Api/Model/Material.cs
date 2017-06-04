using System;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.WeChat.Model
{

    /// <summary>
    /// 永久视频信息
    /// </summary>
    public class MaterialVideoInfo : JsonResult
    {
        /// <summary>
        /// 视频标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 视频描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        public string down_url { get; set; }
    }



    /// <summary>
    /// 素材统计信息
    /// </summary>
    public class MaterialCount : JsonResult
    {
        /// <summary>
        /// 语音总数量
        /// </summary>
        public int voice_count { get; set; }
        /// <summary>
        /// 视频总数量
        /// </summary>
        public int video_count { get; set; }
        /// <summary>
        /// 图片总数量
        /// </summary>
        public int image_count { get; set; }
        /// <summary>
        /// 图文总数量
        /// </summary>
        public int news_count { get; set; }
    }

    /// <summary>
    /// 媒体素材列表基类
    /// </summary>
    public class MaterialItem
    {
        /// <summary>
        /// 素材Id
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 这篇图文消息素材的最后更新时间
        /// </summary>
        public string update_time { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 图文页的URL，或者，当获取的列表是图片素材列表时，该字段是图片的URL
        /// </summary>
        public string url { get; set; }
    }
    /// <summary>
    /// 图文素材列表
    /// </summary>
    public class MaterialNews : MaterialItem
    {
        /// <summary>
        /// 图文项目
        /// </summary>
        public MaterialNewsContent content { get; set; }
        /// <summary>
        /// 图文项目
        /// </summary>
        public class MaterialNewsContent
        {
            /// <summary>
            /// 图文项目
            /// </summary>
            public Article[] news_item { get; set; }
        }
    }


    /// <summary>
    /// 媒体素材列表
    /// </summary>
    public class MaterialList<T> : JsonResult where T : MaterialItem
    {
        /// <summary>
        /// 该类型的素材的总数
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 本次调用获取的素材的数量
        /// </summary>
        public int item_count { get; set; }
        /// <summary>
        /// 媒体素材项目
        /// </summary>
        public T[] item { get; set; }

    }

    /// <summary>
    /// 图文消息素材
    /// </summary>
    public class Article
    {
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 图文消息的封面图片素材id（必须是永久mediaID）
        /// </summary>
        public string thumb_media_id { get; set; }
        /// <summary>
        /// 是否显示封面
        /// </summary>
        public bool show_cover_pic { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 图文消息的摘要，仅有单图文消息才有摘要，多图文此处为空
        /// </summary>
        public string digest { get; set; }
        /// <summary>
        /// 图文消息的具体内容，支持HTML标签，必须少于2万字符，小于1M，且此处会去除JS
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 图文页的URL
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 图文消息的原文地址，即点击“阅读原文”后的URL
        /// </summary>
        public string content_source_url { get; set; }
    }
}
