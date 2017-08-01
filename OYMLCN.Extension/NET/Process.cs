using System.Threading;

namespace OYMLCN
{
    /// <summary>
    /// 进程或线程相关操作
    /// </summary>
    public static partial class ProcessExtension
    {
        /// <summary>
        /// 挂起线程（Thread.Sleep一年）
        /// </summary>
        public static void Hold()
        {
            while (true)
                Thread.Sleep(1000 * 60 * 60 * 365);
        }
        /// <summary>
        /// 杀掉指定名称的所有程序
        /// </summary>
        /// <param name="processName">程序名称</param>
        public static void Kill(string processName)
        {            
            var ps = System.Diagnostics.Process.GetProcesses();
            foreach (var p in ps)
                if (p.ProcessName == processName)
                    p.Kill();
        }        
    }
  
}
