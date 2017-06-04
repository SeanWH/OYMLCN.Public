using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OYMLCN
{
    /// <summary>
    /// JsonExtension
    /// </summary>
    public static partial class JsonExtension
    {
        /// <summary>
        /// 转换JSON字符串为可供查询的Array数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static JToken ParseToJToken(this string str) => JToken.Parse(str);


        /// <summary>
        /// 获取指定值
        /// </summary>
        /// <param name="jt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(this JToken jt, string key) => jt[key]?.Value<string>();
        /// <summary>
        /// 获取指定值
        /// </summary>
        /// <param name="jt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int? GetInt32(this JToken jt, string key) => jt[key]?.Value<int>();
        /// <summary>
        /// 获取指定值
        /// </summary>
        /// <param name="jt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool? GetBoolean(this JToken jt, string key) => jt[key]?.Value<bool>();
        /// <summary>
        /// 获取指定值
        /// </summary>
        /// <param name="jt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime? GetDateTime(this JToken jt, string key) => jt[key]?.Value<DateTime>();


        /// <summary>
        /// 转换为整型数组
        /// </summary>
        /// <param name="jt"></param>
        /// <returns></returns>
        public static int[] ToIntArray(this JToken jt)
        {
            if (jt == null || jt.Count() == 0)
                return new int[0];
            int length = jt.Count();
            int[] array = new int[length];
            for (int i = 0; i < length; i++)
                array[i] = jt[i].Value<int>();
            return array;
        }
        /// <summary>
        /// 获取整型数组
        /// </summary>
        /// <param name="jt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int[] GetIntArray(this JToken jt, string key) => jt[key].ToIntArray();

        /// <summary>
        /// 转换为字符串数组
        /// </summary>
        /// <param name="jt"></param>
        /// <returns></returns>
        public static string[] ToStringArray(this JToken jt)
        {
            if (jt == null || jt.Count() == 0)
                return new string[0];
            int length = jt.Count();
            string[] array = new string[length];
            for (int i = 0; i < length; i++)
                array[i] = jt[i].Value<string>();
            return array;
        }
        /// <summary>
        /// 获取字符串数组
        /// </summary>
        /// <param name="jt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string[] GetStringArray(this JToken jt, string key) => jt[key].ToStringArray();

        /// <summary>
        /// 转换为对象数组
        /// </summary>
        /// <param name="jt"></param>
        /// <returns></returns>
        public static object[] ToObjectArray(this JToken jt)
        {
            if (jt == null || jt.Count() == 0)
                return new object[0];
            int length = jt.Count();
            object[] array = new object[length];
            for (int i = 0; i < length; i++)
                array[i] = jt[i].Value<JValue>().Value;
            return array;
        }
        /// <summary>
        /// 获取对象数组
        /// </summary>
        /// <param name="jt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object[] GetObjectArray(this JToken jt,string key) => jt[key].ToObjectArray();

    }
}
