using System;
using System.Collections.Generic;

namespace OYMLCN
{
    /// <summary>
    /// EnumExtension
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 将枚举值转换为字符串值（替换 _ 标头）
        /// </summary>
        /// <param name="enumClass"></param>
        /// <returns></returns>
        public static string EnumToString(this Enum enumClass) => enumClass.ToString().TrimStart('_');

        /// <summary>
        /// 将枚举类型转换为Key/Value数组
        /// 必须为enum枚举的任意值，其他类型将返回Null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumClass"></param>
        /// <returns></returns>
        public static Dictionary<string, T> EnumToKeyValues<T>(this T enumClass)
        {
            try
            {
                var reuslt = new Dictionary<string, T>();
                foreach (T value in Enum.GetValues(enumClass.GetType()))
                    reuslt.Add(value.ToString(), value);
                return reuslt;
            }
            catch
            {
                return null;
            }
        }

    }
}
