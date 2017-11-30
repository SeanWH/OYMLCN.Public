using System;

namespace OYMLCN
{
    /// <summary>
    /// DateTimeExtension
    /// </summary>
    public static partial class DateTimeExtension
    {
        /// <summary>
        /// 将Datetime转换成时间戳（1970-1-1 00:00:00至target的总秒数）
        /// </summary>
        /// <param name="target">DateTime</param>
        /// <returns>时间戳（1970-1-1 00:00:00至target的总秒数）</returns>
        public static long ToTimestamp(this DateTime target) => (target.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        /// <summary>
        /// 将时间戳（1970-1-1 00:00:00至target的总秒数）转换成Datetime
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime TimestampToDateTime(this long timestamp) => new DateTime(1970, 1, 1).AddTicks((timestamp + 8 * 60 * 60) * 10000000);
        /// <summary>
        /// 将时间戳（1970-1-1 00:00:00至target的总秒数）转换成Datetime
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime TimestampToDateTime(this int timestamp) => TimestampToDateTime((long)timestamp);

        /// <summary>
        /// 获取年 开始时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetYearStart(this DateTime dt) => new DateTime(dt.Year, 1, 1, 0, 0, 0);
        /// <summary>
        /// 获取年 结束时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetYearEnd(this DateTime dt) => dt.GetYearStart().AddYears(1).AddMilliseconds(-1);

        /// <summary>
        /// 获取月 开始时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetMonthStart(this DateTime dt) => new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
        /// <summary>
        /// 获取月 结束时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetMonthEnd(this DateTime dt) => dt.GetMonthStart().AddMonths(1).AddMilliseconds(-1);

        /// <summary>
        /// 获取天 开始时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetDayStart(this DateTime dt) => new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        /// <summary>
        /// 获取天 结束时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetDayEnd(this DateTime dt) => dt.GetDayStart().AddDays(1).AddMilliseconds(-1);

        /// <summary>
        /// 判断是否是当天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsToday(this DateTime dt) => dt.Date == DateTime.Now.Date;

    }
}
