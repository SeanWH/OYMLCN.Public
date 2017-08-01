#if NET452
using Microsoft.Win32;
using System;
using System.Linq;

namespace OYMLCN
{
    /// <summary>
    /// 注册表操作
    /// </summary>
    public static class RegistryExtension
    {

        /// <summary>
        /// Url协议操作
        /// </summary>
        public static class URLProcotol
        {
            /// <summary>
            /// 注册启动项到注册表
            /// </summary>
            /// <param name="procotol"></param>
            /// <param name="exeFullPath">System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName</param>
            public static void Reg(string procotol, string exeFullPath)
            {
                //注册的协议头，即在地址栏中的路径 如QQ的：tencent://xxxxx/xxx
                var surekamKey = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(procotol);
                //以下这些参数都是固定的，不需要更改，直接复制过去 
                var shellKey = surekamKey.CreateSubKey("shell");
                var openKey = shellKey.CreateSubKey("open");
                var commandKey = openKey.CreateSubKey("command");
                surekamKey.SetValue("URL Protocol", "");
                //注册可执行文件取当前程序全路径
                commandKey.SetValue("", "\"" + exeFullPath + "\"" + " \"%1\"");
            }
            /// <summary>
            /// 取消注册
            /// </summary>
            /// <param name="procotol"></param>
            public static void UnReg(string procotol) =>
                //直接删除节点
                Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(procotol);
        }
    }
}
#endif