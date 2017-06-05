﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        /// <summary>
        /// API调用域名
        /// </summary>
        public const string ApiHost = "api.weixin.qq.com";
        /// <summary>
        /// 公众平台域名
        /// </summary>
        public const string MpHost = "mp.weixin.qq.com";
        /// <summary>
        /// 开放平台域名
        /// </summary>
        public const string OpenHost = "open.weixin.qq.com";



        private static string ApiUrl(string apiStr, params string[] param) => string.Format("https://{0}{1}", ApiHost, string.Format(apiStr, param));
        private static string MpUrl(string apiStr, params string[] param) => string.Format("https://{0}{1}", MpHost, string.Format(apiStr, param));


        private static T ApiGet<T>(string apiStr, params string[] param) where T : Model.JsonResult
        {
            var result = HttpClient.GetString(ApiUrl(apiStr, param)).DeserializeJsonString<T>();
            if (result != null && result.Success)
                return result;
            throw result?.Error ?? new InvalidCastException("未知返回异常");
        }
        private static T ApiPost<T>(string data, string apiStr, params string[] param) where T : Model.JsonResult
        {
            var result = HttpClient.PostJsonString(ApiUrl(apiStr, param), data).DeserializeJsonString<T>();
            if (result != null && result.Success)
                return result;
            throw result?.Error ?? new InvalidCastException("未知返回异常");
        }
        private static JToken ApiJTokenGet(string apiStr, params string[] param)
        {
            string result = HttpClient.GetString(ApiUrl(apiStr, param));
            var jr = result.DeserializeJsonString<Model.JsonResult>();
            if (jr != null && jr.Success)
                return result.ParseToJToken();
            throw jr?.Error ?? new InvalidCastException("未知返回异常");
        }
        private static JToken ApiJTokenPost(string data, string apiStr, params string[] param)
        {
            string result = HttpClient.PostJsonString(ApiUrl(apiStr, param), data);
            var jr = result.DeserializeJsonString<Model.JsonResult>();
            if (jr != null && jr.Success)
                return result.ParseToJToken();
            throw jr?.Error ?? new InvalidCastException("未知返回异常");
        }
        private static T ApiPostFile<T>(Dictionary<string,string> data, string apiStr, params string[] param) where T : Model.JsonResult
        {
            var result = HttpClient.CurlPost(ApiUrl(apiStr, param), data).ReadToEnd().DeserializeJsonString<T>();
            if (result != null && result.Success)
                return result;
            throw result?.Error ?? new InvalidCastException("未知返回异常");
        }

    }
}
