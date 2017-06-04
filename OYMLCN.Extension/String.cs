using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace OYMLCN
{
    /// <summary>
    /// StringExtension
    /// </summary>
    public static partial class StringExtension
    {
        /// <summary>
        /// 生成随机字符
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <param name="blur">是否包含特殊符号</param>
        /// <returns></returns>
        public static string RandCode(int length = 6, bool blur = false)
        {
            if (length <= 0)
                return "";
            if (blur)
            {
                const string letters = "ABCDEFGHIJKLMNOPQRSTWXYZ";
                const string numbers = "0123456789";
                const string symbols = "~!@#$%^&*()_-+=[{]}|><,.?/";
                var hash = string.Empty;
                var rand = new Random();

                for (var cont = 0; cont < length; cont++)
                    switch (rand.Next(0, 3))
                    {
                        case 1:
                            hash = hash + numbers[rand.Next(0, 10)];
                            break;
                        case 2:
                            hash = hash + symbols[rand.Next(0, 26)];
                            break;
                        default:
                            hash = hash + ((cont % 3 == 0)
                                ? letters[rand.Next(0, 24)].ToString()
                                : (letters[rand.Next(0, 24)]).ToString().ToLower());
                            break;
                    }
                return hash;
            }
            else
            {
                string sCode = "";
                string codeSerial = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
                string[] arr = codeSerial.Split(',');
                int randValue = -1;
                Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
                for (int i = 0; i < length; i++)
                {
                    randValue = rand.Next(0, arr.Length - 1);
                    sCode += arr[randValue];
                }
                return sCode;
            }
        }
        /// <summary>
        /// 生成随机字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">字符串长度</param>
        /// <returns></returns>
        public static string RandCode(this string str, int length = 6) => RandCode(length);
        /// <summary>
        /// 生成带特殊符号的随机字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">字符串长度</param>
        /// <returns></returns>
        public static string RandBlurCode(this string str, int length) => RandCode(length, true);


        /// <summary>
        /// 判断字符串是否为空/空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str?.Trim());
        /// <summary>
        /// 判断字符串是否是邮箱地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(this string str) =>
            str.IsNullOrEmpty() ? false : new Regex(@"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?").IsMatch(str.Trim());

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubString(this string str, int start, int length = int.MaxValue) =>
            new string(str.Skip(start).Take(length).ToArray());


        /// <summary>
        /// 将Boolean转换为Yes是或No否
        /// </summary>
        /// <param name="boolean"></param>
        /// <param name="cnString">是否返回中文是/否</param>
        /// <returns></returns>
        public static string ToYesNo(this bool boolean, bool cnString = true) => cnString ? boolean ? "是" : "否" : boolean ? "Yes" : "No";


        /// <summary>
        /// 根据标志分割字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sign">分割标识</param>
        /// <param name="option">分割结果去重方式</param>
        /// <returns></returns>
        public static string[] SplitBySign(this string str, string sign, StringSplitOptions option = StringSplitOptions.None) => str?.Split(new string[] { sign }, option);
        /// <summary>
        /// 根据标志分割字符串后获得第一个字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sign"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static string SplitThenGetFirst(this string str, string sign, StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries) => str?.SplitBySign(sign, option).FirstOrDefault();
        /// <summary>
        /// 根据标志分割字符串后获得最后一个字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sign">分割标志</param>
        /// <param name="option">分割结果去重方式</param>
        /// <returns></returns>
        public static string SplitThenGetLast(this string str, string sign, StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries) => str?.SplitBySign(sign, option).LastOrDefault();
        /// <summary>
        /// 根据标志分割字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="signs">分割标识，多重标识</param>
        /// <returns></returns>
        public static string[] SplitByMultiSign(this string str, params string[] signs) => str?.Split(signs, StringSplitOptions.RemoveEmptyEntries);


        /// <summary>
        /// 根据 | \ / 、 ， , 空格 中文空格 制表符空格换行 分割字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="option">分割结果去重方式</param>
        /// <returns></returns>
        public static string[] AutoSplit(this string str, StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries) =>
            str?.SplitByMultiSign("|", "\\", "/", "、", ":", "：", "，", ",", "　", " ", "\t");

        /// <summary>
        /// 根据换行符拆分字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SplitByLine(this string str) => str?.SplitByMultiSign("\t", "\r", "\n");



        /// <summary>
        /// 转化为半角字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToDBC(this string str)
        {
            var c = str.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                // 全角空格为12288，半角空格为32
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                // 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        /// <summary>
        /// 转化为全角字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToSBC(this string str)
        {
            var c = str.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] > 32 && c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }




        /// <summary>
        /// 转换网页地址为Uri
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Uri ToUri(this string str) => new Uri(str);
        /// <summary>
        /// 转换网页地址为Uri(失败返回null)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Uri ToNullableUri(this string str)

        {
            try
            {
                return new Uri(str);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取url字符串的的协议域名地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHostUrl(this string url) => url.ToUri().GetHostUrl();
        /// <summary>
        /// 获取Uri的协议域名地址
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetHostUrl(this Uri uri) => uri == null ? null : $"{uri.Scheme}://{uri.Host}/";


        /// <summary>
        /// 将单个Cookie字符串转换为Cookie类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Cookie ToCookie(this string str)
        {
            if (str.IsNullOrEmpty())
                return null;
            var index = str.IndexOf('=');
            return new Cookie(str.SubString(0, index).Trim(), str.SubString(++index, int.MaxValue));
        }
        /// <summary>
        /// 将Cookies字符串转换为CookieCollection集合
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static CookieCollection ToCookieCollection(this string str)
        {
            var result = new CookieCollection();
            foreach (var cookie in str?.SplitBySign(";") ?? new string[0])
                result.Add(cookie.ToCookie());
            return result;
        }

        /// <summary>
        /// 将字符串填充到Steam中
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoder">默认使用UTF-8进行编码</param>
        /// <returns></returns>
        public static Stream ToStream(this string str, Encoding encoder = null) => new MemoryStream(str.ToBytes(encoder));

        /// <summary>
        /// 将字符串填充到byte[]字节流中
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoder">默认使用UTF-8进行编码</param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str, Encoding encoder = null) => encoder?.GetBytes(str) ?? Encoding.UTF8.GetBytes(str);
    }
}
