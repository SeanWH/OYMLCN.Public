#if NET461
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OYMLCN
{
    /// <summary>
    /// SystemExtension
    /// </summary>
    public static class SystemExtension
    {
        [DllImport(@"wininet", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "InternetSetOption", CallingConvention = CallingConvention.StdCall)]
        static extern bool InternetSetOption(int hInternet, int dmOption, IntPtr lpBuffer, int dwBufferLength);

        static void SetIEProxy(bool enable = false, bool global = false, string host = "127.0.0.1", int port = 1080, string autoConfigPath = "")
        {
            using (var setting = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", true))
                if (enable)
                {
                    setting.SetValue("ProxyEnable", 1);
                    if (!global)
                    {
                        setting.DeleteValue("ProxyOverride", false);
                        setting.DeleteValue("ProxyServer", false);
                        setting.SetValue("AutoConfigURL", $"http://{host}:{port.ToString()}/{autoConfigPath}?t={DateTime.Now.ToString("yyyyMMddHHmmssfff")}");
                    }
                    else
                    {
                        setting.DeleteValue("AutoConfigURL", false);
                        setting.SetValue("ProxyOverride", "<local>;localhost;127.*;10.*;172.16.*;172.17.*;172.18.*;172.19.*;172.20.*;172.21.*;172.22.*;172.23.*;172.24.*;172.25.*;172.26.*;172.27.*;172.28.*;172.29.*;172.30.*;172.31.*;172.32.*;192.168.*");
                        setting.SetValue("ProxyServer", $"{host}:{port.ToString()}");
                    }
                }
                else
                    setting.SetValue("ProxyEnable", 0);

            //激活代理设置
            InternetSetOption(0, 39, IntPtr.Zero, 0);
            InternetSetOption(0, 37, IntPtr.Zero, 0);
        }

        /// <summary>
        /// 设置自动智能代理
        /// </summary>
        /// <param name="host">域名</param>
        /// <param name="port">端口</param>
        /// <param name="autoConfigPath">配置文件路径</param>
        public static void SetIEProxyWithAutoConfigUrl(string host = "127.0.0.1", int port = 1080, string autoConfigPath = "pac") =>
            SetIEProxy(true, false, host, port, autoConfigPath);
        /// <summary>
        /// 设置全局代理
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public static void SetIEProxyWithGlobal(string host = "127.0.0.1", int port = 1080) =>
            SetIEProxy(true, true, host, port);
        /// <summary>
        /// 设置禁用代理
        /// </summary>
        public static void SetIEProxyInDisable() => SetIEProxy();


    }
}
#endif