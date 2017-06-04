using OYMLCN.WeChat.Model;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using OYMLCN.WeChat.Enum;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 模板消息通讯逻辑辅助
    /// </summary>
    public static class TemplateMessageApi
    {
        /// <summary>
        /// 发送模板通知消息
        /// </summary>
        /// <param name="token">公众号全局唯一票据</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="templateId">模板Id</param>
        /// <param name="url">Url</param>
        /// <param name="topColor">顶部颜色</param>
        /// <param name="data">参数</param>
        /// <returns></returns>
        public static JsonResult TemplateMessageSend(this AccessToken token, string openid, string templateId, string url, string topColor = "#FF0000", params TemplateMessageData[] data) =>
            TemplateMessageSend(token, openid, templateId, url, topColor, data.ToList());
        /// <summary>
        /// 发送模板通知消息
        /// </summary>
        /// <param name="token">公众号全局唯一票据</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="templateId">模板Id</param>
        /// <param name="url">Url</param>
        /// <param name="topColor">顶部颜色</param>
        /// <param name="data">参数</param>
        /// <returns></returns>
        public static JsonResult TemplateMessageSend(this AccessToken token, string openid, string templateId, string url, string topColor = "#FF0000", List<TemplateMessageData> data = null)
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in data)
                str.Append("\"" + item.Key + "\":{\"value\":\"" + item.Value + "\",\"color\":\"" + item.Color + "\"},");
            return HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/message/template/send?access_token={0}", token.access_token),
                "{\"touser\":\"" + openid + "\",\"template_id\":\"" + templateId + "\",\"url\":\"" + url + "\",\"topcolor\":\"" + topColor
                + "\",\"data\":{" + str.ToString().Remove(str.Length - 1) + "}}"
                ).DeserializeJsonString<JsonResult>();
        }
        /// <summary>
        /// 发送模板通知消息
        /// </summary>
        /// <param name="token">公众号全局唯一票据</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="templateId">模板Id</param>
        /// <param name="url">Url</param>
        /// <param name="topColor">顶部颜色</param>
        /// <param name="data">Key/Value字典</param>
        /// <returns></returns>
        public static JsonResult TemplateMessageSend(this AccessToken token, string openid, string templateId, string url, string topColor = "#FF0000", Dictionary<string, string> data = null)
        {
            var list = new List<TemplateMessageData>();
            foreach (var item in data)
                list.Add(new TemplateMessageData(item.Key, item.Value));
            return token.TemplateMessageSend(openid, templateId, url, topColor, list);
        }


        /// <summary>
        /// 设置所属行业
        /// </summary>
        /// <param name="token"></param>
        /// <param name="industry">公众号模板消息所属行业</param>
        /// <param name="secondIndustry">公众号模板消息所属行业</param>
        /// <returns></returns>
        public static JsonResult TemplateIndustrySet(this AccessToken token, IndustryCode industry, IndustryCode secondIndustry) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/template/api_set_industry?access_token={0}", token.access_token),
                "{\"industry_id1\":\"" + ((int)industry).ToString() + "\",\"industry_id2\":\"" + ((int)secondIndustry).ToString() + "\"}"
                ).DeserializeJsonString<JsonResult>();
        /// <summary>
        /// 设置所属行业
        /// </summary>
        /// <param name="token"></param>
        /// <param name="industry">公众号模板消息所属行业</param>
        /// <returns></returns>
        public static JsonResult TemplateIndustrySet(this AccessToken token, TemplateIndustry industry) => token.TemplateIndustrySet(industry.primary_industry, industry.secondary_industry);
        /// <summary>
        /// 获取设置的行业信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static TemplateIndustry TemplateIndustryQuery(this AccessToken token) =>
            HttpClient.GetString(CoreApi.ApiUrl("/cgi-bin/template/get_industry?access_token={0}", token.access_token)).DeserializeJsonString<TemplateIndustry>();


        /// <summary>
        /// 获得模板ID
        /// 有效字段：template_id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="templateIdShort">模板库中模板的编号，有“TM**”和“OPENTMTM**”等形式</param>
        /// <returns></returns>
        public static TemplateItem TemplateAdd(this AccessToken token, string templateIdShort) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/template/api_add_template?access_token={0}", token.access_token), 
                "{\"template_id_short\":\"" + templateIdShort + "\"}"
                ).DeserializeJsonString<TemplateItem>();
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static TemplateList TemplateQuery(this AccessToken token) =>
            HttpClient.GetString(CoreApi.ApiUrl("/cgi-bin/template/get_all_private_template?access_token={0}", token.access_token)).DeserializeJsonString<TemplateList>();
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="token"></param>
        /// <param name="templateId">公众帐号下模板消息ID</param>
        /// <returns></returns>
        public static JsonResult TemplateDelete(this AccessToken token, string templateId) =>
            HttpClient.PostJsonString(CoreApi.ApiUrl("/cgi-bin/template/del_private_template?access_token={0}", token.access_token),
                "{\"template_id\":\"" + templateId + "\"}"
                ).DeserializeJsonString<JsonResult>();
    }
}
