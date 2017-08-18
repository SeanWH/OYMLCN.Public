using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OYMLCN
{
    /// <summary>
    /// FormatExtension
    /// </summary>
    public static class FormatExtension
    {
        struct CapacityInfo
        {
            /// <summary>
            /// 值
            /// </summary>
            public float value;
            /// <summary>
            /// 单位
            /// </summary>
            public string unitName;
            /// <summary>
            /// 原始计数
            /// </summary>
            public long unit;

            /// <summary>
            /// 容量信息
            /// </summary>
            /// <param name="value"></param>
            /// <param name="unitName"></param>
            /// <param name="unit"></param>
            public CapacityInfo(float value, string unitName, long unit)
            {
                this.value = value;
                this.unitName = unitName;
                this.unit = unit;
            }
        }
        private static CapacityInfo GetCapacity(this ulong n)
        {
            long scale = 1;
            float f = n;
            string unit = "B";
            if (f > 1024)
            {
                f = f / 1024;
                scale <<= 10;
                unit = "KiB";
            }
            if (f > 1024)
            {
                f = f / 1024;
                scale <<= 10;
                unit = "MiB";
            }
            if (f > 1024)
            {
                f = f / 1024;
                scale <<= 10;
                unit = "GiB";
            }
            if (f > 1024)
            {
                f = f / 1024;
                scale <<= 10;
                unit = "TiB";
            }
            return new CapacityInfo(f, unit, scale);
        }

        /// <summary>
        /// 格式化字节数到更大单位
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string FormatCapacity(this ulong n)
        {
            var result = GetCapacity(n);
            return $"{result.value:0.##}{result.unitName}";
        }
        /// <summary>
        /// 格式化字节数到更大单位
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatCapacityBytes(this ulong bytes)
        {
            const long K = 1024L;
            const long M = K * 1024L;
            const long G = M * 1024L;
            const long T = G * 1024L;
            const long P = T * 1024L;
            const long E = P * 1024L;

            if (bytes >= P * 990)
                return (bytes / (double)E).ToString("F5") + "EiB";
            if (bytes >= T * 990)
                return (bytes / (double)P).ToString("F5") + "PiB";
            if (bytes >= G * 990)
                return (bytes / (double)T).ToString("F5") + "TiB";
            if (bytes >= M * 990)
                return (bytes / (double)G).ToString("F4") + "GiB";
            if (bytes >= M * 100)
                return (bytes / (double)M).ToString("F1") + "MiB";
            if (bytes >= M * 10)
                return (bytes / (double)M).ToString("F2") + "MiB";
            if (bytes >= K * 990)
                return (bytes / (double)M).ToString("F3") + "MiB";
            if (bytes > K * 2)
                return (bytes / (double)K).ToString("F1") + "KiB";
            return bytes.ToString() + "B";
        }


        /// <summary>
        /// 字节总量转换为MB标识
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static decimal BytesLengthToMB(this ulong length) => Math.Round(length / Convert.ToDecimal(1024 * 1024), 2, MidpointRounding.AwayFromZero);
        /// <summary>
        /// 字节总量转换为GB标识
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static decimal BytesLengthToGB(this ulong length) => Math.Round(length / Convert.ToDecimal(1024 * 1024 * 1024), 2, MidpointRounding.AwayFromZero);

    }
}
