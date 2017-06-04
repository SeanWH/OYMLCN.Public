using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Tesseract;

namespace OYMLCN
{
    /// <summary>
    /// TesseractHelper
    /// </summary>
    public static class TesseractHelper
    {
        /// <summary>
        /// 获取测试训练结果文件夹
        /// </summary>
        public static string TestDataPath => Path.Combine(Directory.GetCurrentDirectory() , "tessdata");

        /// <summary>
        /// 从互联网获取图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Bitmap GetPicByUrl(string url)
        {
            WebClient wc = new WebClient();
            var bytes = wc.DownloadData(url);
            return (Bitmap)Image.FromStream(new MemoryStream(bytes) as Stream);
        }

        /// <summary>
        /// 获取数字结果
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static string GetNumber(Bitmap bitmap)
        {
            string defaultList = "0123456789";
            TesseractEngine test = new TesseractEngine(TestDataPath, "eng");
            test.SetVariable("tessedit_char_whitelist", defaultList);
            BitmapHandler uc = new BitmapHandler(bitmap);
            //uc.GrayByPixels();
            //uc.ClearPicBorder(1);
            uc.ResizeImage(bitmap.Width * 2, bitmap.Height * 2);
            Page tmpPage = test.Process(uc.Result, pageSegMode: test.DefaultPageSegMode);
            return tmpPage?.GetText().Trim();
        }
        /// <summary>
        /// 获取文字（中文）结果
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static string GetText(Bitmap bitmap)
        {
            TesseractEngine test = new TesseractEngine(TestDataPath, "chi_sim");
            BitmapHandler uc = new BitmapHandler(bitmap);
            uc.GrayByLine().ResizeImage(bitmap.Width * 2, bitmap.Height * 2);
            Page tmpPage = test.Process(uc.Result, pageSegMode: test.DefaultPageSegMode);
            return tmpPage?.GetText().Replace("\r", "").Replace("\n", "").RemoveSpace().Trim();
        }
    }
}
