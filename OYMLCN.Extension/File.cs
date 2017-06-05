using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OYMLCN
{
    /// <summary>
    /// FileExtension
    /// </summary>
    public static class FileExtension
    {
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FileInfo GetFileInfo(this string filePath) => new FileInfo(filePath);

        /// <summary>
        /// 将文本信息保存到文件
        /// <para>如果文件已经存在则直接覆盖</para>
        /// </summary>
        /// <param name="file"></param>
        /// <param name="content">文本内容</param>
        public static void WriteAllText(this FileInfo file, string content) => File.WriteAllText(file.FullName, content);
        /// <summary>
        /// 向文本文件追加文本
        /// </summary>
        /// <param name="file"></param>
        /// <param name="content">文本内容</param>
        /// <param name="appendOnEnd">在文件结尾添加，false时在文件开头添加</param>
        public static void AppendText(this FileInfo file, string content, bool appendOnEnd = true)
        {
            StringBuilder str = new StringBuilder();
            var temp = file.ReadAllText();
            if (appendOnEnd)
            {
                str.AppendLine(temp);
                str.AppendLine(content);
            }
            else
            {
                str.AppendLine(content);
                str.AppendLine(temp);
            }
            file.WriteAllText(str.ToString());
        }
        /// <summary>
        /// 读取文本文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string ReadAllText(this FileInfo file)
        {
            if (!file.Exists)
                return string.Empty;
            return File.ReadAllText(file.FullName);
        }


        /// <summary>
        /// 获取文件夹信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DirectoryInfo GetDirectoryInfo(this string path) => new DirectoryInfo(path);
        /// <summary>
        /// 获取文件夹大小(字节)
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static long GetLength(this DirectoryInfo directory)
        {
            if (!directory.Exists)
                return 0;
            long len = 0;
            foreach (FileInfo fi in directory.GetFiles())
                len += fi.Length;
            DirectoryInfo[] dis = directory.GetDirectories();
            if (dis.Length > 0)
                for (int i = 0; i < dis.Length; i++)
                    len += GetLength(dis[i]);
            return len;
        }
        /// <summary>
        /// 将指定文件夹下面的所有内容copy到目标文件夹下面
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="target">目标路径路径</param>
        public static void FolderCopy(this DirectoryInfo directory, string target)
        {
            if (!directory.Exists)
                return;
            if (target[target.Length - 1] != Path.DirectorySeparatorChar)
                target += Path.DirectorySeparatorChar;
            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);
            string[] fileList = Directory.GetFileSystemEntries(directory.FullName);
            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                    file.GetDirectoryInfo().FolderCopy(target + Path.GetFileName(file));
                else
                    File.Copy(file, target + Path.GetFileName(file), true);
            }
        }


        /// <summary>
        /// 获取路径文件的MD5码
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetMD5Hash(this FileInfo file)
        {
            MD5 md5 = MD5.Create();
            byte[] retVal;
            using (FileStream temp = new FileStream(file.FullName, FileMode.Open))
                retVal = md5.ComputeHash(temp);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
                sb.Append(retVal[i].ToString("x2"));
            return sb.ToString();
        }

    }
}
