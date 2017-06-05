using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public partial class CustomerService
        {

            public static KefuList.Info[] GetList(string access_token) =>
                ApiGet<KefuList>("/cgi-bin/customservice/getkflist?access_token={0}", access_token).kf_list;

            public static KefuList.Info[] GetOnlineList(string access_token) =>
                ApiGet<KefuList>("/cgi-bin/customservice/getonlinekflist?access_token={0}", access_token).kf_online_list;

            public class Account
            {
                protected class JsonCreate
                {
                    public static string Add(string kf_account, string nickname) =>
                        "{\"kf_account\":\"" + kf_account + "\",\"nickname\":\"" + nickname + "\"}";
                    public static string Invite(string kf_account, string invite_wx) =>
                        "{\"kf_account\":\"" + kf_account + "\",\"invite_wx\":\"" + invite_wx + "\"}";
                    public static string Update(string kf_account, string nickname) => Add(kf_account, nickname);
                }

                public static JsonResult Add(string access_token, string kf_account, string nickname) =>
                    ApiPost<JsonResult>(JsonCreate.Add(kf_account, nickname), "/customservice/kfaccount/add?access_token={0}", access_token);
                public static JsonResult Invite(string access_token, string kf_account, string invite_wx) =>
                   ApiPost<JsonResult>(JsonCreate.Invite(kf_account, invite_wx), "/customservice/kfaccount/inviteworker?access_token={0}", access_token);
                public static JsonResult Update(string access_token, string kf_account, string nickname) =>
                    ApiPost<JsonResult>(JsonCreate.Update(kf_account, nickname), "/customservice/kfaccount/update?access_token={0}", access_token);

                public static JsonResult UploadHeadImg(string access_token, string kf_account, string filePath)
                {
                    if (!filePath.GetFileInfo().Extension.Equals(".jpg"))
                        throw new FormatException("头像图片文件必须是jpg格式");
                    return ApiPostFile<JsonResult>(new Dictionary<string, string>() {
                        { "media", filePath }
                    }, "/customservice/kfaccount/uploadheadimg?access_token={0}&kf_account={1}", access_token, kf_account.EncodeAsUrlData());
                }
                public static JsonResult Delete(string access_token, string kf_account) =>
                    ApiGet<JsonResult>("/customservice/kfaccount/del?access_token={0}&kf_account={1}", access_token, kf_account.EncodeAsUrlData());

            }
        }
    }
}
