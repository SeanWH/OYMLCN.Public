using Newtonsoft.Json.Linq;
using OYMLCN.WeChat.Enums;
using OYMLCN.WeChat.Model;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public class Template
        {
            protected class JsonCreate
            {
                public static string SendMessage(string touser, string template_id, string url, string color = "#FF0000", List<TemplateParameter> data = null)
                {
                    StringBuilder str = new StringBuilder();
                    foreach (var item in data)
                        str.Append("\"" + item.Key + "\":{\"value\":\"" + item.Value + "\",\"color\":\"" + item.Color + "\"},");
                    return "{\"touser\":\"" + touser + "\",\"template_id\":\"" + template_id + "\",\"url\":\"" + url + "\",\"topcolor\":\"" + color +
                        "\",\"data\":{" + str.ToString().Remove(str.Length - 1) + "}}";
                }
                public static string SetIndustry(IndustryCode industry_id1, IndustryCode industry_id2) =>
                    "{\"industry_id1\":\"" + ((int)industry_id1).ToString() + "\",\"industry_id2\":\"" + ((int)industry_id2).ToString() + "\"}";
                public static string Add(string template_id_short) =>
                    "{\"template_id_short\":\"" + template_id_short + "\"}";
                public static string Delete(string template_id) =>
                    "{\"template_id\":\"" + template_id + "\"}";
            }

            public static JsonResult SendMessage(string access_token, string touser, string template_id, string url, string color = "#FF0000", List<TemplateParameter> data = null) =>
                ApiPost<JsonResult>(JsonCreate.SendMessage(touser, template_id, url, color, data), "/cgi-bin/message/template/send?access_token={0}", access_token);
            public static JsonResult SendMessage(string access_token, string touser, string template_id, string url, string color = "#FF0000", Dictionary<string, string> data = null)
            {
                var list = new List<TemplateParameter>();
                foreach (var item in data)
                    list.Add(new TemplateParameter(item.Key, item.Value));
                return SendMessage(access_token, touser, template_id, url, color, list);
            }

            public static JsonResult SetIndustry(string access_token, IndustryCode industry_id1, IndustryCode industry_id2) =>
                ApiPost<JsonResult>(JsonCreate.SetIndustry(industry_id1, industry_id2), "/cgi-bin/template/api_set_industry?access_token={0}", access_token);
            public static JToken GetIndustry(string access_token) => ApiJTokenGet("/cgi-bin/template/get_industry?access_token={0}", access_token);

            public static Model.Template Add(string access_token, string template_id_short) =>
                ApiPost<Model.Template>(JsonCreate.Add(template_id_short), "/cgi-bin/template/api_add_template?access_token={0}", access_token);

            class TemplateList : JsonResult
            {
                public Model.Template[] template_list { get; set; }
            }
            public static Model.Template[] Get(string access_token) =>
                ApiGet<TemplateList>("/cgi-bin/template/get_all_private_template?access_token={0}", access_token).template_list;

            public static JsonResult Delete(string access_token, string template_id) =>
                ApiPost<JsonResult>(JsonCreate.Delete(template_id),"/cgi-bin/template/del_private_template?access_token={0}", access_token);
        }
    }
}
