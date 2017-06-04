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
        /// 日期转换为中文 年月
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToCnMonthString(this DateTime dt) => dt.ToString("yyyy年MM月");
        /// <summary>
        /// 日期转换为中文 年月日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToCnDateString(this DateTime dt) => dt.ToString("yyyy年MM月dd日");
        /// <summary>
        /// 时间转换为中文 年月日时分|秒
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="second">是否包含秒</param>
        /// <returns></returns>
        public static string ToCnDatetimeString(this DateTime dt, bool second = false)
        {
            if (second)
                return dt.ToString("yyyy年MM月dd日 HH:mm:ss");
            return dt.ToString("yyyy年MM月dd日 HH:mm");
        }


        /// <summary>
        /// 时间转换 时分|秒
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="second">是否包含秒</param>
        /// <returns></returns>
        public static string ToTimeString(this DateTime dt, bool second = false)
        {
            if (second)
                return dt.ToString("HH:mm:ss");
            return dt.ToString("HH:mm");
        }
        /// <summary>
        /// 时间转换 日时分|秒
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="second">是否包含秒</param>
        /// <returns></returns>
        public static string ToDayTimeString(this DateTime dt, bool second = false)
        {
            if (second)
                return dt.ToString("dd HH:mm:ss");
            return dt.ToString("dd HH:mm");
        }

        /// <summary>
        /// 与现在时间的间隔（中文） --前/后
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToCnIntervalString(this DateTime dt)
        {
            var interval = dt - DateTime.Now;
            var endStr = interval > TimeSpan.Parse("0") ? "后" : "前";
            var day = interval.Days;
            if (day != 0)
            {
                if (day == -1)
                    return "昨天";
                if (day == 1)
                    return "明天";
                day = day < 0 ? day * -1 : day;
                if (day >= 365)
                    return $"{day / 365}年{endStr}";
                else if (day >= 30)
                    return $"{day / 30}个月{endStr}";
                else if (day >= 7)
                    return $"{day / 7}周{endStr}";
                else
                    return $"{day}天{endStr}";
            }
            var hour = interval.Hours;
            if (hour != 0)
            {
                if (dt.Date == DateTime.Now.Date && hour < -6)
                    return "今天";
                if (hour > 0 && DateTime.Now.AddDays(1).Date == dt.Date)
                    return "明天";
                hour = hour < 0 ? hour * -1 : hour;
                return $"{hour}小时{endStr}";
            }
            var minute = interval.Minutes;
            if (minute != 0)
            {
                if (minute < 0 && minute > -3)
                    return "刚刚";
                minute = minute < 0 ? minute * -1 : minute;
                return $"{minute}分钟{endStr}";
            }
            var second = interval.Seconds;
            {
                if (second > 0)
                    return $"{second}秒后";
                return "刚刚";
            }

        }


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
