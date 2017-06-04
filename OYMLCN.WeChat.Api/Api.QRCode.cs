using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public class QRCode
        {
            public static QRScene CreateScene(AccessToken token, int sceneId, int expire = 604800) =>
                ApiPost<QRScene>("{\"expire_seconds\":" + (expire > 604800 ? 604800 : expire).ToString() +
                    ",\"action_name\":\"QR_SCENE\",\"action_info\":{\"scene\":{\"scene_id\":" + sceneId.ToString() + "}}}",
                    "/cgi-bin/qrcode/create?access_token={0}", token.access_token);
            public static QRScene CreateLimitScene(AccessToken token, int sceneId) =>
                ApiPost<QRScene>("{\"action_name\":\"QR_LIMIT_SCENE\",\"action_info\":{\"scene\":{\"scene_id\":" + sceneId.ToString() + "}}}",
                    "/cgi-bin/qrcode/create?access_token={0}", token.access_token);
            public static QRScene CreateLimitScene(AccessToken token, string sceneStr) =>
                ApiPost<QRScene>("{\"action_name\":\"QR_LIMIT_STR_SCENE\",\"action_info\":{\"scene\":{\"scene_str\":\"" + sceneStr.SubString(0, 64) + "\"}}}",
                    "/cgi-bin/qrcode/create?access_token={0}", token.access_token);

            public static string ShowUrl(QRScene qr) => MpUrl("/cgi-bin/showqrcode?ticket={0}", qr.ticket.UrlEncode());

        }
    }
}
