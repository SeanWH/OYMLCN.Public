using OYMLCN.WeChat.Model;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public class QRCode
        {
            protected class JsonCreate
            {
                public static string CreateScene(int scene_id, int expire_seconds = 604800) =>
                    "{\"expire_seconds\":" + (expire_seconds > 604800 ? 604800 : expire_seconds).ToString() +
                    ",\"action_name\":\"QR_SCENE\",\"action_info\":{\"scene\":{\"scene_id\":" + scene_id.ToString() + "}}}";
                public static string CreateLimitScene(int scene_id) =>
                    "{\"action_name\":\"QR_LIMIT_SCENE\",\"action_info\":{\"scene\":{\"scene_id\":" + scene_id.ToString() + "}}}";
                public static string CreateLimitScene(string scene_str) =>
                    "{\"action_name\":\"QR_LIMIT_STR_SCENE\",\"action_info\":{\"scene\":{\"scene_str\":\"" + scene_str.SubString(0, 64) + "\"}}}";
            }

            public static QRScene CreateScene(string access_token, int scene_id, int expire_seconds = 604800) =>
                ApiPost<QRScene>(JsonCreate.CreateScene(scene_id, expire_seconds), "/cgi-bin/qrcode/create?access_token={0}", access_token);
            public static QRScene CreateLimitScene(string access_token, int scene_id) =>
                ApiPost<QRScene>(JsonCreate.CreateLimitScene(scene_id), "/cgi-bin/qrcode/create?access_token={0}", access_token);
            public static QRScene CreateLimitScene(string access_token, string scene_str) =>
                ApiPost<QRScene>(JsonCreate.CreateLimitScene(scene_str), "/cgi-bin/qrcode/create?access_token={0}", access_token);
            public static string ShowUrl(QRScene qr) => MpUrl("/cgi-bin/showqrcode?ticket={0}", qr.ticket.UrlEncode());
        }
    }
}
