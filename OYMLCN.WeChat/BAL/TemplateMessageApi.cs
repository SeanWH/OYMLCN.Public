using OYMLCN.WeChat.Model;
using System.Collections.Generic;
using System.Linq;
using OYMLCN.WeChat.Enums;
using Newtonsoft.Json.Linq;

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
        /// <param name="template_id">模板Id</param>
        /// <param name="url">Url</param>
        /// <param name="data">参数</param>
        /// <returns></returns>
        public static JsonResult TemplateMessageSend(this AccessToken token, string openid, string template_id, string url, params TemplateParameter[] data) =>
            TemplateMessageSend(token, openid, template_id, url, data.ToList());
        /// <summary>
        /// 发送模板通知消息
        /// </summary>
        /// <param name="token">公众号全局唯一票据</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="template_id">模板Id</param>
        /// <param name="url">Url</param>
        /// <param name="data">参数</param>
        /// <returns></returns>
        public static JsonResult TemplateMessageSend(this AccessToken token, string openid, string template_id, string url, List<TemplateParameter> data = null) =>
            Api.Template.SendMessage(token.access_token, openid, template_id, url, data);
        /// <summary>
        /// 发送模板通知消息
        /// </summary>
        /// <param name="token">公众号全局唯一票据</param>
        /// <param name="openid">普通用户openid</param>
        /// <param name="template_id">模板Id</param>
        /// <param name="url">Url</param>
        /// <param name="data">Key/Value字典</param>
        /// <returns></returns>
        public static JsonResult TemplateMessageSend(this AccessToken token, string openid, string template_id, string url, Dictionary<string, string> data = null)
        {
            var list = new List<TemplateParameter>();
            foreach (var item in data)
                list.Add(new TemplateParameter(item.Key, item.Value));
            return token.TemplateMessageSend(openid, template_id, url, list);
        }


        /// <summary>
        /// 设置所属行业
        /// </summary>
        /// <param name="token"></param>
        /// <param name="industry">公众号模板消息所属行业</param>
        /// <param name="secondIndustry">公众号模板消息所属行业</param>
        /// <returns></returns>
        public static JsonResult TemplateIndustrySet(this AccessToken token, IndustryCode industry, IndustryCode secondIndustry) =>
            Api.Template.SetIndustry(token.access_token, industry, secondIndustry);
        /// <summary>
        /// 获取设置的行业信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static JToken TemplateIndustryQuery(this AccessToken token) =>
            Api.Template.GetIndustry(token.access_token);

        /// <summary>
        /// 获得模板ID
        /// 有效字段：template_id
        /// </summary>
        /// <param name="token"></param>
        /// <param name="template_id_short">模板库中模板的编号，有“TM**”和“OPENTMTM**”等形式</param>
        /// <returns></returns>
        public static Template TemplateAdd(this AccessToken token, string template_id_short) =>
            Api.Template.Add(token.access_token, template_id_short);
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Template[] TemplateQuery(this AccessToken token) =>
            Api.Template.Get(token.access_token);
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="token"></param>
        /// <param name="template_id">公众帐号下模板消息ID</param>
        /// <returns></returns>
        public static JsonResult TemplateDelete(this AccessToken token, string template_id) =>
            Api.Template.Delete(token.access_token, template_id);
    }
}
