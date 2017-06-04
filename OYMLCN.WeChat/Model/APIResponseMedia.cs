namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 媒体素材类型
    /// </summary>
    public enum MediaType
    {
        /// <summary>
        /// 图片
        /// </summary>
        Image,
        /// <summary>
        /// 语音
        /// </summary>
        Voice,
        /// <summary>
        /// 视频
        /// </summary>
        Video,
        /// <summary>
        /// 缩略图
        /// </summary>
        Thumb,
        /// <summary>
        /// 图文
        /// </summary>
        News
    }

    /// <summary>
    /// 素材创建返回结果
    /// </summary>
    public class Media : JsonResult
    {
        /// <summary>
        /// 媒体文件类型，
        /// 分别有图片（image）、语音（voice）、视频（video）和
        /// 缩略图（thumb，主要用于视频与音乐格式的缩略图）
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 媒体文件上传后，获取标识
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 媒体文件上传时间戳
        /// </summary>
        public string created_at { get; set; }
        /// <summary>
        /// 新增的图片素材的图片URL（仅新增图片素材时会返回该字段）
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 素材统计信息
    /// </summary>
    public class MediaCount : JsonResult
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
    public abstract class MediaListBase
    {
        /// <summary>
        /// 素材Id
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 这篇图文消息素材的最后更新时间
        /// </summary>
        public string update_time { get; set; }
    }

    /// <summary>
    /// 媒体素材列表
    /// </summary>
    public class MediaList<T> : JsonResult where T : MediaListBase
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
    public class MediaNewItem
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

    /// <summary>
    /// 图文素材列表
    /// </summary>
    public class MediaListNews : MediaListBase
    {
        /// <summary>
        /// 图文项目
        /// </summary>
        public MediaListNewsContent content { get; set; }
    }
    /// <summary>
    /// 图文项目
    /// </summary>
    public class MediaListNewsContent
    {
        /// <summary>
        /// 图文项目
        /// </summary>
        public MediaNewItem[] news_item { get; set; }
    }

    /// <summary>
    /// 素材项目
    /// </summary>
    public class MediaItem : MediaListBase
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 当获取的是图片素材时，该字段是图片的URL
        /// </summary>
        public string url { get; set; }
    }
}
