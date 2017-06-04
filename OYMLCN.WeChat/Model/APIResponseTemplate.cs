using OYMLCN.WeChat.Enum;

// 本文件放置调用微信API所返回的数据

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 所属行业信息
    /// </summary>
    public class TemplateIndustry : JsonResult
    {
        /// <summary>
        /// 帐号设置的主营行业
        /// </summary>
        public IndustryCode primary_industry { get; set; }
        /// <summary>
        /// 帐号设置的副营行业
        /// </summary>
        public IndustryCode secondary_industry { get; set; }
    }

    /// <summary>
    /// 模板消息模板信息
    /// </summary>
    public class TemplateItem : JsonResult
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 模板标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 模板所属行业的一级行业
        /// </summary>
        public string primary_industry { get; set; }
        /// <summary>
        /// 模板所属行业的二级行业
        /// </summary>
        public string deputy_industry { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 模板示例
        /// </summary>
        public string example { get; set; }
    }

    /// <summary>
    /// 模板消息模板列表
    /// </summary>
    public class TemplateList : JsonResult
    {
        /// <summary>
        /// 模板消息模板列表
        /// </summary>
        public TemplateItem[] template_list { get; set; }
    }
}