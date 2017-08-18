﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace OYMLCN
{
    /// <summary>
    /// Extension
    /// </summary>
    public static class Extension
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process,
            UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        /// <param name="removePages"></param>
        public static void ReleaseMemory(bool removePages)
        {
            // release any unused pages
            // making the numbers look good in task manager
            // this is totally nonsense in programming
            // but good for those users who care
            // making them happier with their everyday life
            // which is part of user experience
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
            if (removePages)
            {
                // as some users have pointed out
                // removing pages from working set will cause some IO
                // which lowered user experience for another group of users
                //
                // so we do 2 more things here to satisfy them:
                // 1. only remove pages once when configuration is changed
                // 2. add more comments here to tell users that calling
                //    this function will not be more frequent than
                //    IM apps writing chat logs, or web browsers writing cache files
                //    if they're so concerned about their disk, they should
                //    uninstall all IM apps and web browsers
                //
                // please open an issue if you're worried about anything else in your computer
                // no matter it's GPU performance, monitor contrast, audio fidelity
                // or anything else in the task manager
                // we'll do as much as we can to help you
                //
                // just kidding
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);
            }
        }



        /// <summary>
        /// 验证居民身份证号码
        /// 验证支持：GB11643-1989、GB11643-1999
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsChineseIDCard(this string str)
        {
            string address = "11,12,13,14,15,21,22,23,31,32,33,34,35,36,37,41,42,43,44,45,46,51,52,53,54,50,61,62,63,64,65,71,81,82";
            long n = 0;

            if (str.Length == 15)
            {
                if (long.TryParse(str, out n) == false || n < Math.Pow(10, 14))
                    return false;//数字验证
                if (address.IndexOf(str.Remove(2)) == -1)
                    return false;//省份验证
                if (str.Substring(6, 6).Insert(4, "-").Insert(2, "-").ConvertToNullableDatetime().IsNull())
                    return false;//生日验证  
                return true;//符合GB11643-1989标准
            }
            else if (str.Length == 18)
            {
                if (long.TryParse(str.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(str.Replace('x', '0').Replace('X', '0'), out n) == false)
                    return false;//数字验证  
                if (address.IndexOf(str.Remove(2)) == -1)
                    return false;//省份验证  
                if (str.Substring(6, 8).Insert(6, "-").Insert(4, "-").ConvertToNullableDatetime().IsNull())
                    return false;//生日验证  
                string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
                char[] Ai = str.Remove(17).ToCharArray();
                int sum = 0;
                for (int i = 0; i < 17; i++)
                    sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
                if (arrVarifyCode[sum % 11] != str.Substring(17, 1).ToLower())
                    return false;//校验码验证
                return true;//符合GB11643-1999标准
            }
            throw new FormatException("身份证长度不正确或暂未支持该身份证验证");
        }

        /// <summary>
        /// 判断字典键值类型是否未赋值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="keyValuePair"></param>
        /// <returns></returns>
        public static bool IsDefault<TKey, TValue>(this KeyValuePair<TKey, TValue> keyValuePair) =>
            default(KeyValuePair<TKey, TValue>).Equals(keyValuePair);

        /// <summary>
        /// 判断是否为Null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj) => obj == null;
        /// <summary>
        /// 判断是否为非Null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj) => obj != null;
        /// <summary>
        /// 判断集合是否为空或Null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable) =>
            enumerable.IsNull() || !enumerable.Any();
        /// <summary>
        /// 判断集合是否不为空或Null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable) =>
            enumerable.IsNotNull() && enumerable.Any();
        /// <summary>
        /// 获取字典值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) =>
            GetValueOrDefault(dict, key, default(TValue));

        /// <summary>
        /// 获取字典值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue) =>
             dict.ContainsKey(key) ? dict[key] : defaultValue;
        /// <summary>
        /// 从另一个字典增添或更新值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="other"></param>
        public static void Update<TKey, TValue>(this IDictionary<TKey, TValue> dict, IDictionary<TKey, TValue> other)
        {
            foreach (var key in other.Keys)
                dict[key] = other[key];
        }

        /// <summary>
        /// CharToInt32
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static int ToInt32(this char ch) => ch;
        /// <summary>
        /// Int32ToChar
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static char ToChar(this int i) => (char)i;
    }
}
