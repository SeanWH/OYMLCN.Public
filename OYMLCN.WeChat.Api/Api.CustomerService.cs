using Newtonsoft.Json.Linq;
using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public partial class CustomerService
        {

            public static KefuList.Info[] GetList(AccessToken token) =>
                ApiGet<KefuList>("/cgi-bin/customservice/getkflist?access_token={0}", token.access_token).kf_list;

            public static KefuList.Info[] GetOnlineList(AccessToken token) =>
                ApiGet<KefuList>("/cgi-bin/customservice/getonlinekflist?access_token={0}", token.access_token).kf_online_list;

            public static class Account
            {

                public static JsonResult Add(AccessToken token, string nickName, string kfName, Config cfg = null) =>
                    ApiPost<JsonResult>("{\"kf_account\":\"" + CompleteKefuName(kfName, cfg) + "\",\"nickname\":\"" + nickName + "\"}",
                        "/customservice/kfaccount/add?access_token={0}", token.access_token);

                public static JsonResult Invite(AccessToken token, string inviteWX, string kfName, Config cfg = null) =>
                   ApiPost<JsonResult>("{\"kf_account\":\"" + CompleteKefuName(kfName, cfg) + "\",\"invite_wx\":\"" + inviteWX + "\"}",
                       "/customservice/kfaccount/inviteworker?access_token={0}", token.access_token);

                public static JsonResult Update(AccessToken token, string nickName, string kfName, Config cfg = null) =>
                    ApiPost<JsonResult>("{\"kf_account\":\"" + CompleteKefuName(kfName, cfg) + "\",\"nickname\":\"" + nickName + "\"}",
                        "/customservice/kfaccount/update?access_token={0}", token.access_token);

                public static JsonResult UploadHeadImg(AccessToken token, string filePath, string kfName, Config cfg = null)
                {
                    if (!filePath.GetFileInfo().Extension.Equals(".jpg"))
                        throw new FormatException("头像图片文件必须是jpg格式");
                    return ApiPostFile<JsonResult>(new Dictionary<string, string>() {
                        { "media", filePath }
                    }, "/customservice/kfaccount/uploadheadimg?access_token={0}&kf_account={1}", token.access_token, CompleteKefuName(kfName, cfg).EncodeAsUrlData());
                }
                public static JsonResult Delete(AccessToken token, string kfName, Config cfg = null) =>
                    ApiGet<JsonResult>("/customservice/kfaccount/del?access_token={0}&kf_account={1}", token.access_token, CompleteKefuName(kfName, cfg).EncodeAsUrlData());

            }
        }
    }
}
