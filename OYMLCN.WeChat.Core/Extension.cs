using System.Collections.Generic;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// Extension
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 验证请求是否来自微信，如果是则返回PostModel
        /// </summary>
        /// <param name="query">请求参数</param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
        public static PostModel IsValidRequest(this Dictionary<string, string> query, Config cfg)
        {
            var model = PostModel.Build(query);
            if (Signature.Create(model.Timestamp, model.Nonce, cfg.Token) == model.Signature)
                return model;
            return null;
        }
        /// <summary>
        /// 验证消息的确来自微信服务器
        /// </summary>
        /// <param name="query">请求参数</param>
        /// <param name="cfg">基础接口配置</param>
        /// <returns></returns>
        public static string ConfigVerify(this Dictionary<string, string> query, Config cfg) =>
            query.IsValidRequest(cfg) == null ? string.Empty : query.GetValueOrDefault("echostr");
    }
}
