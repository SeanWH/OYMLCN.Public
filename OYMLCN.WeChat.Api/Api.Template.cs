using Newtonsoft.Json.Linq;
using OYMLCN.WeChat.Enums;
using OYMLCN.WeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public class Template
        {
            public static JsonResult SendMessage(AccessToken token, string openid, string templateId, string url, string topColor = "#FF0000", params TemplateParameter[] data) =>
                SendMessage(token, openid, templateId, url, topColor, data.ToList());

            public static JsonResult SendMessage(AccessToken token, string openid, string templateId, string url, string topColor = "#FF0000", List<TemplateParameter> data = null)
            {
                StringBuilder str = new StringBuilder();
                foreach (var item in data)
                    str.Append("\"" + item.Key + "\":{\"value\":\"" + item.Value + "\",\"color\":\"" + item.Color + "\"},");
                return ApiPost<JsonResult>("{\"touser\":\"" + openid + "\",\"template_id\":\"" + templateId + "\",\"url\":\"" + url + "\",\"topcolor\":\"" + topColor
                    + "\",\"data\":{" + str.ToString().Remove(str.Length - 1) + "}}",
                    "/cgi-bin/message/template/send?access_token={0}", token.access_token);
            }

            public static JsonResult SendMessage(AccessToken token, string openid, string templateId, string url, string topColor = "#FF0000", Dictionary<string, string> data = null)
            {
                var list = new List<TemplateParameter>();
                foreach (var item in data)
                    list.Add(new TemplateParameter(item.Key, item.Value));
                return SendMessage(token, openid, templateId, url, topColor, list);
            }


            public static JsonResult SetIndustry(AccessToken token, IndustryCode industry, IndustryCode secondIndustry) =>
                ApiPost<JsonResult>("{\"industry_id1\":\"" + ((int)industry).ToString() + "\",\"industry_id2\":\"" + ((int)secondIndustry).ToString() + "\"}",
                    "/cgi-bin/template/api_set_industry?access_token={0}", token.access_token);

            public static JToken GetIndustry(AccessToken token)=> ApiJTokenGet("/cgi-bin/template/get_industry?access_token={0}", token.access_token);


            public static Model.Template Add(AccessToken token, string templateIdShort) =>
                ApiPost<Model.Template>("{\"template_id_short\":\"" + templateIdShort + "\"}",
                    "/cgi-bin/template/api_add_template?access_token={0}", token.access_token);


            class TemplateList : JsonResult
            {
                public Model.Template[] template_list { get; set; }
            }

            public static Model.Template[] Get(AccessToken token) =>
                ApiGet<TemplateList>("/cgi-bin/template/get_all_private_template?access_token={0}", token.access_token).template_list;

            public static JsonResult Delete(AccessToken token, string templateId) =>
                ApiPost<JsonResult>("{\"template_id\":\"" + templateId + "\"}",
                    "/cgi-bin/template/del_private_template?access_token={0}", token.access_token);
        }
    }
}
