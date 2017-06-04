using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using OYMLCN.WeChat;

namespace OYMLCN.Open.WebTest.NetCore.Controllers
{
    public class HomeController : Controller
    {
        //OYMLCN.WeChat.Config Config = new WeChat.Config("wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a", "wxapi", null, "gh_69438e79ea75");
        OYMLCN.WeChat.Config Config = new WeChat.Config("wxb5b0dff3abebb00c", "4a1d6d04a0c04e2e6b016f629fe4b32c", "525F6F8660944B97921BEFD1B20E8756", "oO5kZMstD7jQrJXiQBuC7ZSs6E7J91NBAEzHf4dnHRs", "maiqunw");
        //OYMLCN.WeChat.Config OYMLCNConfig = new Config("wx9960b2eab9e87d17", "46e57cb6421846723a2dbebc387ac290", "wxapi", "ibuHr5GSvEvHmGJbQF2S9OY4miQnqT7Mvf0SUrD3vyp", "oymlcn");


        public IActionResult Index()
        {
            var str = new StringBuilder();

            str.AppendLine($"UrlPath：{Request.GetUrlPath()}");
            str.AppendLine($"UserAgent：{Request.GetUserAgent()}");
            str.AppendLine($"Query：{string.Join(",", Request.GetQuery())}");
            str.AppendLine($"QueryStr：{Request.GetQuery("qq")}");
            str.AppendLine($"Body：{Request.GetBody().ReadToEnd()}");
            str.AppendLine($"Ip：{Request.GetUserIpAddress()}");

            str.AppendLine(Request.GetUrlPath());
            if (Request.Cookies.Count > 0)
                str.AppendLine(string.Join(",", Request.Cookies));
            else
                Response.Cookies.Append("test", "123");
            var body = Request.GetBody().ReadToEnd();
            str.AppendLine(body);
            var query = string.Join(",", Request.GetQuery());
            str.AppendLine(query);

            return Content(str.ToString());
        }

        //public IActionResult CustomerServiceAccountQuery()
        //{
        //    var str = new StringBuilder();
        //    var token = Config.GetAccessToken();
        //    str.AppendLine($"CustomerServiceAccountQuery：{token.CustomerServiceAccountQuery().ToJsonString()}");
        //    return Content(str.ToString());
        //}
        //public IActionResult CustomerServiceAccountOnlineQuery()
        //{
        //    var str = new StringBuilder();
        //    var token = Config.GetAccessToken();
        //    str.AppendLine($"CustomerServiceAccountOnlineQuery：{token.CustomerServiceAccountOnlineQuery().ToJsonString()}");
        //    return Content(str.ToString());
        //}
        //public IActionResult CustomerServiceAccountAdd()
        //{
        //    var str = new StringBuilder();
        //    var token = Config.GetAccessToken();
        //    str.AppendLine($"CustomerServiceAccountAdd：{token.CustomerServiceAccountAdd("test", "未命名").ToJsonString()}");
        //    return Content(str.ToString());
        //}
        //public IActionResult CustomerServiceAccountInvite()
        //{
        //    var str = new StringBuilder();
        //    var token = Config.GetAccessToken();
        //    str.AppendLine($"CustomerServiceAccountInvite：{token.CustomerServiceAccountInvite("test", "ouyangminlan").ToJsonString()}");
        //    return Content(str.ToString());
        //}
        //public IActionResult CustomerServiceAccountUpdate()
        //{
        //    var str = new StringBuilder();
        //    var token = Config.GetAccessToken();
        //    str.AppendLine($"CustomerServiceAccountUpdate：{token.CustomerServiceAccountUpdate("test", "测试").ToJsonString()}");
        //    return Content(str.ToString());
        //}
        //public IActionResult CustomerServiceAccountUploadHeadImg()
        //{
        //    var str = new StringBuilder();
        //    var token = Config.GetAccessToken();
        //    str.AppendLine($"CustomerServiceAccountUploadHeadImg：{token.CustomerServiceAccountUploadHeadImg("test", @"C:\Users\Vic\Desktop\www.100qun.com.jpg").ToJsonString()}");
        //    return Content(str.ToString());
        //}
        //public IActionResult CustomerServiceAccountDelete()
        //{
        //    var str = new StringBuilder();
        //    var token = Config.GetAccessToken();
        //    str.AppendLine($"CustomerServiceAccountDelete：{token.CustomerServiceAccountDelete("test").ToJsonString()}");
        //    return Content(str.ToString());
        //}

        //public IActionResult UploadTest()
        //{
        //    return Content(MediaApi.MaterialUploadImage(Config.GetAccessToken(), @"C:\Users\Vic\Desktop\topbar_logo.png").ToJsonString());
        //}

        //public IActionResult UploadTest2()
        //{
        //    return Content(MediaApi.MediaUpload(Config.GetAccessToken(), WeChat.Model.MediaType.Image, @"C:\Users\Vic\Desktop\topbar_logo.png").ToJsonString());
        //}
        //public IActionResult GetUrl(string id)
        //{
        //    return Content(MediaApi.MediaDownloadUrl(Config.GetAccessToken(), WeChat.Model.MediaType.Image, id).ToJsonString());
        //}

        //public IActionResult ArticleTest()
        //{
        //    return Content(MediaApi.MaterialNewsAdd(Config.GetAccessToken(), new WeChat.Model.MediaNewItem()
        //    {
        //        title = "接口测试",
        //        thumb_media_id = "Cx88SJdPgS8GNEl36MFFlqu79TSB6KojaupWejTWzQ4",
        //        author = "测试",
        //        digest = "测试信息",
        //        show_cover_pic = true,
        //        content = "测试呀",
        //        content_source_url = "http://www.qq.com/"
        //    }, new WeChat.Model.MediaNewItem()
        //    {
        //        title = "接口测试",
        //        thumb_media_id = "Cx88SJdPgS8GNEl36MFFlqu79TSB6KojaupWejTWzQ4",
        //        author = "测试",
        //        digest = "测试信息",
        //        show_cover_pic = true,
        //        content = "测试呀",
        //        content_source_url = "http://www.qq.com/"
        //    }).ToJsonString());
        //}
        //public IActionResult ImageUploadTest()
        //{
        //    return Content(MediaApi.MaterialUpload(Config.GetAccessToken(), WeChat.Model.MediaType.Image, @"C:\Users\Vic\Desktop\topbar_logo.png").ToJsonString());
        //}
        //public IActionResult DownloadImage()
        //{
        //    return File(MediaApi.MaterialDownload(Config.GetAccessToken(), WeChat.Model.MediaType.Image, "Cx88SJdPgS8GNEl36MFFlqu79TSB6KojaupWejTWzQ4").ToBytes(), "image/png");
        //}
        //public IActionResult DownloadNews(string id)
        //{
        //    return Content(MediaApi.MaterialNewsQuery(Config.GetAccessToken(), id).ToJsonString());
        //}
        //public IActionResult NewsList()
        //{
        //    return Content(MediaApi.MaterialMediaListQuery(Config.GetAccessToken(), WeChat.Model.MediaType.News, 0, 2).ToJsonString());
        //}
        //public IActionResult MediaList()
        //{
        //    return Content(MediaApi.MaterialMediaListQuery(Config.GetAccessToken(), WeChat.Model.MediaType.Image, 0, 2).ToJsonString());
        //}
        //public IActionResult Count()
        //{
        //    return Content(MediaApi.MaterialCount(Config.GetAccessToken()).ToJsonString());
        //}
        //public IActionResult UpdateNew()
        //{
        //    return Content(MediaApi.MaterialNewUpdate(Config.GetAccessToken(), "Cx88SJdPgS8GNEl36MFFltgVvPEToLNNwzE9awqubrE", 1, new WeChat.Model.MediaNewItem()
        //    {
        //        title = "接口测试" + DateTime.Now.ToTimeString(),
        //        thumb_media_id = "Cx88SJdPgS8GNEl36MFFlqu79TSB6KojaupWejTWzQ4",
        //        author = "测试",
        //        digest = "测试信息",
        //        show_cover_pic = true,
        //        content = "测试呀",
        //        content_source_url = "http://www.qq.com/"
        //    }).ToJsonString());
        //}
        //public IActionResult DeleteNew()
        //{
        //    return Content(MediaApi.MaterialDelete(Config.GetAccessToken(), "Cx88SJdPgS8GNEl36MFFltgVvPEToLNNwzE9awqubrE").ToJsonString());
        //}
        //public IActionResult DeleteImage()
        //{
        //    return Content(MediaApi.MaterialDelete(Config.GetAccessToken(), "Cx88SJdPgS8GNEl36MFFlqu79TSB6KojaupWejTWzQ4").ToJsonString());
        //}
    }
}
