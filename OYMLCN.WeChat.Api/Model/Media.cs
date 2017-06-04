using System;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 素材创建返回结果
    /// </summary>
    public class MediaUpload : JsonResult
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
}
