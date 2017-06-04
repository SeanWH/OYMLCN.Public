using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OYMLCN.Open.WebTest.Net452.Controllers
{
    public class HomeController : Controller
    {
        public class demo
        {
            public string ddd { get; set; }
            public string test1 { get; set; }
        }
        public ActionResult Index(demo demo, HttpPostedFileBase file)
        {
            var str = new StringBuilder();

            str.AppendLine($"UrlPath：{Request.GetUrlPath()}");
            str.AppendLine($"UserAgent：{Request.GetUserAgent()}");
            str.AppendLine($"Query：{string.Join(",", Request.GetQuery())}");
            str.AppendLine($"QueryStr：{Request.GetQuery("qq")}");
            str.AppendLine($"Body：{Request.GetBody().ReadToEnd()}");
            str.AppendLine($"Ip：{Request.GetUserIpAddress()}");



            return Content(str.ToString());
        }

        public ActionResult JsonTest(string json)
        {
            return Content(json + Request.GetQuery().ToQueryString());
        }

    }
}
