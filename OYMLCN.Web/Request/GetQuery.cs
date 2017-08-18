#if NET452
using System.Web;
using System.Collections.Specialized;
#else
using Microsoft.AspNetCore.Http;
#endif
using System.Linq;
using System.Collections.Generic;

namespace OYMLCN
{
    /// <summary>
    /// RequestExtension
    /// </summary>
    public static partial class RequestExtension
    {

#if NET452
        private static Dictionary<string, string> ToDictionary(this NameValueCollection query)
        {
            var dic = new Dictionary<string, string>();
            if (query != null)
                foreach (var item in query.AllKeys)
                    dic[item] = query[item];
            return dic;
        }
#else
        private static Dictionary<string, string> ToDictionary(this IQueryCollection query)
        {
            var dic = new Dictionary<string, string>();
            if (query != null)
                foreach (var item in query)
                    dic[item.Key] = item.Value;
            return dic;
        }
#endif


#if NET452
        /// <summary>
        /// 获取请求参数集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetQuery(this HttpRequestBase request) => request.Params.ToDictionary();
#endif
        /// <summary>
        /// 获取请求参数集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetQuery(this HttpRequest request)
        {
#if NET452
            return request.Params.ToDictionary();
#else
            if (request.Method != "POST")
                return request.Query.ToDictionary();
            else
            {
                var dic = request.Query.ToDictionary();
                if(dic==null)
                    dic=new Dictionary<string, string>();
                if (request.ContentType.ToLower() == "application/x-www-form-urlencoded")
                    foreach (var item in request.GetBody().ReadToEnd().SplitBySign("&"))
                    {
                        var query = item.SplitBySign("=");
                        dic.Add(query.FirstOrDefault(), query.Skip(1).FirstOrDefault());
                    }
                return dic;
            }
#endif
        }
#if NET452
        /// <summary>
        /// 获取指定键的请求参数值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetQuery(this HttpRequestBase request, string key) => request.GetQuery()?.Where(d => d.Key == key).Select(d => d.Value).FirstOrDefault();
#endif
        /// <summary>
        /// 获取指定键的请求参数值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetQuery(this HttpRequest request, string key) => request.GetQuery()?.Where(d => d.Key == key).Select(d => d.Value).FirstOrDefault();
    }
}
