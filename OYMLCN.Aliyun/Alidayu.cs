using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OYMLCN.Aliyun
{
    /// <summary>
    /// 阿里大鱼短信发送
    /// </summary>
    public class Alidayu
    {
        #region SmsResponse
        class SmsResultAli
        {
            public SmsResponseALi Alibaba_Aliqin_Fc_Sms_Num_Send_Response { get; set; }
        }
        class SmsResponseALi
        {
            public string Request_Id { get; set; }
            public SmsResponseResultAli Result { get; set; }
        }
        class SmsResponseResultAli
        {
            public string Err_Code { get; set; }
            public string Model { get; set; }
            public bool Success { get; set; }
            public string Msg { get; set; }
        }
        #endregion

        static string GetAlidayuSign(IDictionary<string, string> parameters, string secret)
        {
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);
            StringBuilder query = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in sortedParams)
                if (!string.IsNullOrEmpty(kv.Key) && !string.IsNullOrEmpty(kv.Value))
                    query.Append(kv.Key).Append(kv.Value);
            return query.ToString().EncodeToHMACMD5(secret).ToUpper();
        }


        readonly IMemoryCache MemoryCache;
        /// <summary>
        /// 阿里大鱼短信发送
        /// 使用该自动注入方法需要配置如下参数
        /// string Alidayu:AppKey
        /// string Alidayu:AppSecret
        /// string Alidayu:SmsFreeSignName
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="memoryCache"></param>
        public Alidayu(IConfiguration configuration, IMemoryCache memoryCache)
        {
            var config = configuration.GetSection("Alidayu");
            this.AppKey = config.GetValue<string>("AppKey");
            this.AppSecret = config.GetValue<string>("AppSecret");
            this.SmsFreeSignName = config.GetValue<string>("SmsFreeSignName");
            this.MemoryCache = memoryCache;
        }
        /// <summary>
        /// 阿里大鱼短信发送
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="smsFreeSignName"></param>
        public Alidayu(string appKey, string appSecret, string smsFreeSignName)
        {
            this.AppKey = appKey;
            this.AppSecret = appSecret;
            this.SmsFreeSignName = smsFreeSignName;
        }

        /// <summary>
        /// AppKey
        /// </summary>
        protected string AppKey { get; set; }
        /// <summary>
        /// AppSecret
        /// </summary>
        protected string AppSecret { get; set; }
        /// <summary>
        /// SmsFreeSignName
        /// </summary>
        protected string SmsFreeSignName { get; set; }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="template">分配的模板ID</param>
        /// <param name="receiveMobilePhoneNo">接收方电话号码</param>
        /// <param name="params">参数字典</param>
        /// <returns></returns>
        public bool SendSms(string template, string receiveMobilePhoneNo, Dictionary<string, string> @params)
        {
            var txtParams = new Dictionary<string, string>
            {
                { "method", "alibaba.aliqin.fc.sms.num.send" },
                { "v", "2.0" },
                { "sign_method", "hmac" },
                { "app_key", AppKey },
                { "format", "json" },
                { "timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },

                { "sms_type", "normal" },
                { "sms_free_sign_name", SmsFreeSignName },
                { "sms_param", @params.ToJsonString() },
                { "rec_num", receiveMobilePhoneNo },
                { "sms_template_code", template }
            };

            txtParams.Add("sign", GetAlidayuSign(txtParams, AppSecret));

            using (var webclient = new WebClient())
            {
                var response = webclient.PostData(
                       "https://eco.taobao.com/router/rest",
                       txtParams.ToQueryString()
                       );
                var result = response.DeserializeJsonString<SmsResultAli>();

                if (result.Alibaba_Aliqin_Fc_Sms_Num_Send_Response == null)
                    throw new Exception(response);
                else
                {
                    var success = result.Alibaba_Aliqin_Fc_Sms_Num_Send_Response.Result.Success;
                    if (!success)
                        throw new Exception(result.Alibaba_Aliqin_Fc_Sms_Num_Send_Response.Result.Msg);
                    return true;
                }
            }
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="iPAddress">记录IP，为null则不进行任何记录以用于测试</param>
        /// <param name="template">分配的模板ID</param>
        /// <param name="receiveMobilePhoneNo">接收方电话号码</param>
        /// <param name="params">参数字典</param>
        /// <param name="ipLimitMinutes">IP发送限制时长（默认1分钟）</param>
        /// <param name="phoneLimitMinutes">手机号码发送限制时长（默认3分钟）</param>
        /// <returns></returns>
        public bool SendSms(IPAddress iPAddress, string template, string receiveMobilePhoneNo, Dictionary<string, string> @params, byte ipLimitMinutes = 1, byte phoneLimitMinutes = 3)
        {
            if (iPAddress.IsNotNull())
            {
                string ipKey = $"IP_{iPAddress}", phoneKey = $"Phone_{receiveMobilePhoneNo}";
                if (MemoryCache.Get(ipKey).IsNull() && MemoryCache.Get(phoneKey).IsNull())
                {
                    MemoryCache.Set(ipKey, string.Empty, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(ipLimitMinutes)));
                    MemoryCache.Set(phoneKey, string.Empty, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(phoneLimitMinutes)));
                }
                else
                    throw new Exception("验证码发送过于频繁，进稍后再试！");
            }
            return SendSms(template, receiveMobilePhoneNo, @params);
        }


    }
}
