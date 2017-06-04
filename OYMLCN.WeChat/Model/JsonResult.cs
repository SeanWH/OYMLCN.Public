using System;

namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 微信返回结果
    /// </summary>
    public class JsonResult
    {
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool Success => errcode == 0;
        /// <summary>
        /// 操作结果类型
        /// </summary>
        public Exception Error => new Exception(errcode.GetErrorCodeDescription() ?? "未知错误", new Exception(errmsg ?? "未返回有效错误信息", new Exception($"错误码：{errcode}")));

        /// <summary>
        /// 错误代码
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 仅部分请求会返回
        /// </summary>
        public Int64 msgid { get; set; }
    }
}