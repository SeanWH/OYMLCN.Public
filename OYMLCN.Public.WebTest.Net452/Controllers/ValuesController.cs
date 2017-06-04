using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web;

namespace OYMLCN.Open.WebTest.Net452.Controllers
{
    public class ValuesController : ApiController
    {

        // GET api/values/5
        public void Get()
        {
            var str = new StringBuilder();

            var request = HttpContext.Current.Request;
            str.AppendLine($"UrlPath：{Request.GetUrlPath()}");
            str.AppendLine($"UrlPath2：{ request.GetUrlPath()}");
            str.AppendLine($"UserAgent：{Request.GetUserAgent()}");
            str.AppendLine($"UserAgent2：{request.GetUserAgent()}");
            str.AppendLine($"QueryStr：{Request.GetQuery("qq")}");
            str.AppendLine($"QueryStr2：{request.GetQuery("qq")}");
            str.AppendLine($"Ip：{Request.GetUserIpAddress()}");
            str.AppendLine($"Ip2：{request.GetUserIpAddress()}");

            HttpContext.Current.Response.Write(str.ToString());
            HttpContext.Current.Response.End();

        }

        // POST api/values
        public void Post()
        {
            var str = new StringBuilder();

            var request = HttpContext.Current.Request;
            str.AppendLine($"Query：{string.Join(",", Request.GetQuery())}");
            str.AppendLine($"Query2：{string.Join(",", request.GetQuery())}");
            str.AppendLine($"Body：{Request.GetBody().ReadToEnd()}");
            str.AppendLine($"Body2：{request.GetBody().ReadToEnd()}");
            

            HttpContext.Current.Response.Write(str.ToString());
            HttpContext.Current.Response.End();

        }

    }
}
