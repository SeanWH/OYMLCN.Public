namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 模板消息参数数据
    /// </summary>
    public class TemplateMessageData
    {
        /// <summary>
        /// 模板消息参数数据
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="color">默认为蓝色</param>
        public TemplateMessageData(string key, string value, string color = "#173177")
        {
            this.Key = key;
            this.Value = value;
            this.Color = color;
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
