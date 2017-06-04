using OYMLCN.WeChat.Model;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信二维码生成通讯辅助逻辑
    /// </summary>
    public static class QRSceneApi
    {
        /// <summary>
        /// 创建临时二维码
        /// </summary>
        /// <param name="token">公众号全局唯一票据</param>
        /// <param name="sceneId">场景值ID，临时二维码时为32位非0整型</param>
        /// <param name="expire">该二维码有效时间，以秒为单位。 最大不超过604800（即7天）。 </param>
        /// <returns></returns>
        public static QRScene CreateQRScene(this AccessToken token, int sceneId, int expire = 604800) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/qrcode/create?access_token={0}", token.access_token),
                "{\"expire_seconds\":" + (expire > 604800 ? 604800 : expire).ToString() +
                ",\"action_name\":\"QR_SCENE\",\"action_info\":{\"scene\":{\"scene_id\":" + sceneId.ToString() + "}}}"
                ).DeserializeJsonString<QRScene>();
        /// <summary>
        /// 创建永久二维码
        /// </summary>
        /// <param name="token">公众号全局唯一票据</param>
        /// <param name="sceneId">场景值ID，永久二维码时最大值为100000（目前参数只支持1--100000） </param>
        /// <returns></returns>
        public static QRScene CreateQRLimitScene(this AccessToken token, int sceneId) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/qrcode/create?access_token={0}", token.access_token), 
                "{\"action_name\":\"QR_LIMIT_SCENE\",\"action_info\":{\"scene\":{\"scene_id\":" + sceneId.ToString() + "}}}"
                ).DeserializeJsonString<QRScene>();
        /// <summary>
        /// 创建永久二维码
        /// </summary>
        /// <param name="token">公众号全局唯一票据</param>
        /// <param name="sceneStr">场景值ID（字符串形式的ID），字符串类型，长度限制为1到64</param>
        /// <returns></returns>
        public static QRScene CreateQRLimitScene(this AccessToken token, string sceneStr)=>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/qrcode/create?access_token={0}", token.access_token), 
                "{\"action_name\":\"QR_LIMIT_STR_SCENE\",\"action_info\":{\"scene\":{\"scene_str\":\"" + sceneStr.SubString(0, 64) + "\"}}}"
                ).DeserializeJsonString<QRScene>();
        /// <summary>
        /// 换取二维码地址
        /// </summary>
        /// <param name="qr"></param>
        /// <returns></returns>
        public static string GetUrl(this QRScene qr) => CoreApi.MpUrl("/cgi-bin/showqrcode?ticket={0}", qr.ticket.UrlEncode());
    }
}
