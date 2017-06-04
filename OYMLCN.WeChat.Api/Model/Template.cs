using OYMLCN.WeChat.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 模板消息模板信息
    /// </summary>
    public class Template : JsonResult
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
    /// 模板消息参数数据
    /// </summary>
    public class TemplateParameter
    {
        /// <summary>
        /// 模板消息参数数据
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="color">默认为蓝色</param>
        public TemplateParameter(string key, string value, string color = "#173177")
        {
            Key = key;
            Value = value;
            Color = color;
        }

        /// <summary>
        /// 参数名
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 显示颜色
        /// </summary>
        public string Color { get; set; }
    }
}
