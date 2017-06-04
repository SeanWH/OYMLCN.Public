using OYMLCN.WeChat.Enum;

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 关键字处理规则
    /// </summary>
    public class HandlerRule
    {
        /// <summary>
        /// 关键字处理规则
        /// </summary>
        public HandlerRule() { }
        /// <summary>
        /// 关键字处理规则
        /// </summary>
        /// <param name="method"></param>
        /// <param name="keyWord"></param>
        public HandlerRule(HandlerContrast method, string keyWord)
        {
            this.Method = method;
            this.KeyWord = keyWord;
        }
        /// <summary>
        /// 对比方式
        /// </summary>
        public HandlerContrast Method { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
    }
}
