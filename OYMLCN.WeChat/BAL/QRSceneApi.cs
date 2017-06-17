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
        /// <param name="scene_id">场景值ID，临时二维码时为32位非0整型</param>
        /// <param name="expire">该二维码有效时间，以秒为单位。 最大不超过604800（即7天）。 </param>
        /// <returns></returns>
        public static QRScene CreateQRScene(this AccessToken token, int scene_id, int expire = 604800) =>
            Api.QRCode.CreateScene(token.access_token, scene_id, expire);
        /// <summary>
        /// 创建永久二维码
        /// </summary>
        /// <param name="token">公众号全局唯一票据</param>
        /// <param name="scene_id">场景值ID，永久二维码时最大值为100000（目前参数只支持1--100000） </param>
        /// <returns></returns>
        public static QRScene CreateQRLimitScene(this AccessToken token, int scene_id) =>
            Api.QRCode.CreateLimitScene(token.access_token, scene_id);
        /// <summary>
        /// 创建永久二维码
        /// </summary>
        /// <param name="token">公众号全局唯一票据</param>
        /// <param name="scene_str">场景值ID（字符串形式的ID），字符串类型，长度限制为1到64</param>
        /// <returns></returns>
        public static QRScene CreateQRLimitScene(this AccessToken token, string scene_str) =>
            Api.QRCode.CreateLimitScene(token.access_token, scene_str);
        /// <summary>
        /// 换取二维码地址
        /// </summary>
        /// <param name="qr"></param>
        /// <returns></returns>
        public static string GetUrl(this QRScene qr) => Api.QRCode.ShowUrl(qr);
    }
}
