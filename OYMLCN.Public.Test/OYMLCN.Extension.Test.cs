using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
#if NET452 && NoBuild
using System.Drawing;
using System.Drawing.Imaging;
#endif
using System.IO;
using System.Linq;
using System.Net;

namespace OYMLCN.Extension.Test
{
    [TestClass]
    public class UnitTest
    {
#if NET452 && NoBuild
        // 示例方法 不进行单元测试
        public void BitmapTest()
        {
            var image = Image.FromStream(new MemoryStream());
            var bytes = image.ToBytes(ImageFormat.Bmp);
            image = bytes.ToImage();
            var bitmap = bytes.ToBitmap();
            bytes = bitmap.ToBytes();
        }
        // 示例方法 不进行单元测试
        public void BitmapHandlerTest()
        {
            var bitmap = System.Drawing.Bitmap.FromStream(new MemoryStream())
                .ToBytes(System.Drawing.Imaging.ImageFormat.Bmp).ToBitmap();
            var bitmapHanddler = new BitmapHandler(bitmap);
            bitmapHanddler.ResizeImage(100, 100);
            bitmap = bitmapHanddler.Cut(0, 0, 100, 100);
            bitmapHanddler = bitmapHanddler
                .GrayByPixels()
                .ClearPicBorder(1)
                .GrayByLine()
                .GetPicValidByValue(0)
                .GetPicValidByValue(0, 1);
            bitmap = bitmapHanddler.GetPicValidByValue(bitmap, 0);
            var bitmaps = bitmapHanddler.GetSplitPics(2, 2);
            var str = bitmapHanddler.GetSingleBmpCode(bitmap, 0);
        }
        // 示例方法 不进行单元测试
        public void HardwareTest()
        {
            string demo = string.Empty;
            demo = OYMLCN.Hardware.CPUID;
            demo = OYMLCN.Hardware.MacAddress;
            demo = OYMLCN.Hardware.DiskId;
            demo = OYMLCN.Hardware.IpAddress;
            demo = OYMLCN.Hardware.UserName;
            demo = OYMLCN.Hardware.ComputerName;
            demo = OYMLCN.Hardware.SystemType;
            demo = OYMLCN.Hardware.TotalPhysicalMemory;
            bool result = OYMLCN.Hardware.CheckConnection("http://www.github.com");
        }
#endif


        [TestMethod]
        public void SystemInfoTest()
        {
            uint length = 1024 * 1024 * 1024;
            Assert.AreEqual(length.BytesLengthToMB(), 1024);
            Assert.AreEqual(length.BytesLengthToGB(), 1);

#if NET452 && NoBuild
            var memory = OYMLCN.SystemInfo.Memory.Initialize();
            uint demo = 0;
            demo = memory.UsedPercent;
            demo = memory.Total;
            demo = memory.Availale;
            demo = memory.TotalSwap;
            demo = memory.AvailableSwap;
            demo = memory.TotalVirtual;
            demo = memory.AvailableVirtual;

            var systemVersion = OYMLCN.SystemInfo.Version;
            bool result = false;
            result = OYMLCN.SystemInfo.IsWindows7;
            result = OYMLCN.SystemInfo.IsWindows8;
            result = OYMLCN.SystemInfo.IsWindows8_1;
            result = OYMLCN.SystemInfo.IsWindows2008;
            result = OYMLCN.SystemInfo.IsWindows10;
#endif
        }

        [TestMethod]
        public void CryptographyTest()
        {
            string demo = "hello";

            Assert.AreEqual(demo.EncodeToSHA1(), "aaf4c61ddcc5e8a2dabede0f3b482cd9aea9434d");
            Assert.AreEqual(demo.EncodeToSHA256(), "2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824");
            Assert.AreEqual(demo.EncodeToSHA384(), "59e1748777448c69de6b800d7a33bbfb9ff1b463e44354c3553bcdb9c666fa90125a3c79f90397bdf5f6a13de828684f");
            Assert.AreEqual(demo.EncodeToSHA512(), "9b71d224bd62f3785d96d46ad3ea3d73319bfbc2890caadae2dff72519673ca72323c3d99ba5c11d7c7acc6e14b8c5da0c4663475c2e5c3adef46f73bcdec043");
            Assert.AreEqual(demo.EncodeToMD5(), "5d41402abc4b2a76b9719d911017c592");

            Assert.AreEqual(demo.EncodeToBase64().DecodeFromBase64(), demo);
            Assert.AreEqual(demo.StringToBitString().BitStringToString(), demo);

            var demoKey = StringExtension.RandCode(32);
            Assert.AreEqual(demo.AESEncrypt(demoKey).AESDecrypt(demoKey), demo);
            Assert.AreEqual(demo.DESEncrypt(demoKey).DESDecrypt(demoKey), demo);

        }
        [TestMethod]
        public void DateTimeTest()
        {
            var datetime = new DateTime(2017, 1, 1, 0, 0, 0);
            long timestamp;
            Assert.AreEqual(timestamp = datetime.ToTimestamp(), 1483200000);
            Assert.AreEqual(timestamp.TimestampToDateTime(), datetime);

            Assert.AreEqual(datetime.ToCnMonthString(), "2017年01月");
            Assert.AreEqual(datetime.ToCnDateString(), "2017年01月01日");
            Assert.AreEqual(datetime.ToCnDatetimeString(), "2017年01月01日 00:00");
            Assert.AreEqual(datetime.ToCnDatetimeString(true), "2017年01月01日 00:00:00");

            Assert.AreEqual(datetime.ToTimeString(), "00:00");
            Assert.AreEqual(datetime.ToTimeString(true), "00:00:00");
            Assert.AreEqual(datetime.ToDayTimeString(), "01 00:00");
            Assert.AreEqual(datetime.ToDayTimeString(true), "01 00:00:00");

            var now = DateTime.Now;
            Assert.AreEqual(now.ToCnIntervalString(), "刚刚");
            Assert.AreEqual(now.AddSeconds(-3 * 60 + 1).ToCnIntervalString(), "刚刚");
            Assert.AreEqual(now.AddMinutes(-3).ToCnIntervalString(), "3分钟前");
            Assert.AreEqual(now.AddHours(-3).ToCnIntervalString(), "3小时前");
            Assert.AreEqual(now.AddHours(-6).ToCnIntervalString(), "6小时前");
            Assert.AreEqual(now.AddHours(-7).ToCnIntervalString(), "今天");
            Assert.AreEqual(now.AddDays(-1).ToCnIntervalString(), "昨天");
            Assert.AreEqual(now.AddDays(-2).ToCnIntervalString(), "2天前");
            Assert.AreEqual(now.AddDays(-6).ToCnIntervalString(), "6天前");
            Assert.AreEqual(now.AddDays(-7).ToCnIntervalString(), "1周前");
            Assert.AreEqual(now.AddDays(-13).ToCnIntervalString(), "1周前");
            Assert.AreEqual(now.AddDays(-14).ToCnIntervalString(), "2周前");
            Assert.AreEqual(now.AddDays(-30).ToCnIntervalString(), "1个月前");
            Assert.AreEqual(now.AddDays(-365).ToCnIntervalString(), "1年前");
            now = DateTime.Now;
            Assert.AreEqual(now.AddSeconds(59).ToCnIntervalString(), "59秒后");
            Assert.AreEqual(now.AddMinutes(3).ToCnIntervalString(), "3分钟后");
            //Assert.AreEqual(now.AddHours(3).ToCnIntervalString(), "3小时后");
            //Assert.AreEqual(now.AddHours(6).ToCnIntervalString(), "6小时后");
            //Assert.AreEqual(now.AddHours(7).ToCnIntervalString(), "7小时后");
            //Assert.AreEqual(now.AddHours(12).ToCnIntervalString(), "明天");
            Assert.AreEqual(now.AddDays(1).ToCnIntervalString(), "明天");
            Assert.AreEqual(now.AddDays(2).ToCnIntervalString(), "2天后");
            Assert.AreEqual(now.AddDays(6).ToCnIntervalString(), "6天后");
            Assert.AreEqual(now.AddDays(7).ToCnIntervalString(), "1周后");
            Assert.AreEqual(now.AddDays(13).ToCnIntervalString(), "1周后");
            Assert.AreEqual(now.AddDays(14).ToCnIntervalString(), "2周后");
            Assert.AreEqual(now.AddDays(30).ToCnIntervalString(), "1个月后");
            Assert.AreEqual(now.AddDays(365).ToCnIntervalString(), "1年后");

            Assert.AreEqual(datetime.GetYearStart(), new DateTime(2017, 1, 1, 0, 0, 0, 0));
            Assert.AreEqual(datetime.GetYearEnd(), new DateTime(2017, 12, 31, 23, 59, 59, 999));
            Assert.AreEqual(datetime.GetMonthStart(), new DateTime(2017, 1, 1, 0, 0, 0, 0));
            Assert.AreEqual(datetime.GetMonthEnd(), new DateTime(2017, 1, 31, 23, 59, 59, 999));
            Assert.AreEqual(datetime.GetDayStart(), new DateTime(2017, 1, 1, 0, 0, 0, 0));
            Assert.AreEqual(datetime.GetDayEnd(), new DateTime(2017, 1, 1, 23, 59, 59, 999));

            Assert.IsTrue(now.IsToday());
        }


        enum DemoEnum { _test, pig, dog, cat }
        [TestMethod]
        public void EnumTest()
        {
            Assert.AreEqual(DemoEnum._test.EnumToString(), "test");
            Assert.AreEqual(DemoEnum.pig.EnumToString(), "pig");
            var data = new Dictionary<string, DemoEnum>
            {
                { "_test", DemoEnum._test },
                { "pig", DemoEnum.pig },
                { "dog", DemoEnum.dog },
                { "cat", DemoEnum.cat }
            };
            CollectionAssert.AreEqual(DemoEnum.pig.EnumToKeyValues(), data);
        }

        [TestMethod]
        public void ExtensionTest()
        {
            string host = "http://www.qq.com/";
            string url = host + "index.html";
            Uri uri = new Uri(url);
            Assert.AreEqual(url.UrlGetHost(), host);
            Assert.AreEqual(uri.GetHost(), host);
            Assert.IsTrue("130503810215001".IsChineseIDCard());//GB11643-1989
            Assert.IsTrue("110100198906020014".IsChineseIDCard());//GB11643-1999
            Assert.IsFalse("110100198906020015".IsChineseIDCard());//GB11643-1989

            Dictionary<string, string> TestDic = new Dictionary<string, string>();
            TestDic.Add("1", "test");
            Assert.IsFalse(TestDic.FirstOrDefault().IsDefault());
            Assert.IsTrue(TestDic.FirstOrDefault(d => d.Key == "0").IsDefault());

        }

        // 涉及多项文件操作，偶尔会测试失败
        [TestMethod]
        public void FileTest()
        {
            Directory.CreateDirectory("temp");
            string demoPath = "temp/FileExtension.Test." + Guid.NewGuid().ToString() + ".data";
            var fileInfo = demoPath.GetFileInfo();
            fileInfo.WriteAllText("hello");
            fileInfo.AppendText("world", true);
            Assert.AreEqual(fileInfo.ReadAllText(), "hello\r\nworld\r\n");
            Assert.AreEqual(fileInfo.GetMD5Hash(), "7ac062d8a84466e70d9b899c0821a51c");
            fileInfo.Delete();

            demoPath = "temp/demo/";
            var dirInfo = demoPath.GetDirectoryInfo();
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
                Directory.CreateDirectory(demoPath + "demo/");
                System.IO.File.Create(demoPath + "demo.txt");
                (demoPath + "demo/demo.txt").GetFileInfo().WriteAllText("1");
                dirInfo = demoPath.GetDirectoryInfo();
            }
            Assert.AreEqual(dirInfo.GetLength(), 1);

            demoPath = "temp/target";
            dirInfo.FolderCopy(demoPath);
            Assert.AreEqual(demoPath.GetDirectoryInfo().GetLength(), 1);
            try { Directory.Delete("temp", true); } catch { }
        }

        [TestMethod]
        public void HTMLTest()
        {
            string html = "<html><head><link href='/css/footer.css' rel='stylesheet'/><style type='text/css'>.demo{}</style><script src=''>demo()</script></head><body>你好<h3>世界</h3></body></html>";
            Assert.AreEqual(html.HtmlRemove(), ".demo{}demo()你好世界");
            Assert.AreEqual(html.HtmlRemoveScript(), "<html><head><link href='/css/footer.css' rel='stylesheet'/></head><body>你好<h3>世界</h3></body></html>");
            Assert.AreEqual(html.HtmlRemoveScript().HtmlRemove(), "你好世界");
            Assert.AreEqual("\r\n<br/>\r\n<br />\t\t\r\n<br>\r\n\t\t\r\n".HtmlReplaceBr().Trim(), string.Empty);
            Assert.AreEqual("123\r\n<br/>\r\n<br />\r\n<br>321\r\n\r\n".HtmlReplaceBr().RemoveWrap(1), "123\r\n321");
            Assert.AreEqual("\r\n\t<br/>\n\r\t".RemoveWrap(), "\t<br/>\t");
            Assert.AreEqual("1&nbsp; 2 　  3  　 ".RemoveSpace(), "1 2 3");
            var url = "http://www.qq.com/index.html?qq=10000&code=$%43<>><%";
            var urlEncoded = "http%3A%2F%2Fwww.qq.com%2Findex.html%3Fqq%3D10000%26code%3D%24%2543%3C%3E%3E%3C%25";
            Assert.AreEqual(url.UrlEncode(), urlEncoded);
            Assert.AreEqual(urlEncoded.UrlDecode(), url);
            Assert.AreEqual("$%43<>><%".EncodeAsUrlData(), "%24%2543%3C%3E%3E%3C%25");

            var demoDic = new Dictionary<string, string>
            {
                { "qq", "10000" },
                { "code", "%43<>><%" }
            };
            var demoQuery = "qq=10000&code=%2543%3C%3E%3E%3C%25";
            Assert.AreEqual(demoDic.ToQueryString(), demoQuery);
            CollectionAssert.AreEqual(demoQuery.QueryStringToDictionary(), demoDic);
            CollectionAssert.AreEqual(demoDic.ToQueryString().QueryStringToDictionary(), demoDic);

            Assert.AreEqual("hi\r\n你\r好\n".AllInOneLine(), "hi你好");
        }


        [TestMethod]
        public void PinYinTest()
        {
            string demo = "你是好人";
            var pinyin = demo.Pinyin(false);
            Assert.AreEqual(string.Join(",", pinyin.FirstPinYin), "nshr");
            Assert.AreEqual(string.Join(",", pinyin.TotalPinYin), "nishihaoren");
            demo = "长";
            pinyin = demo.Pinyin(true);
            Assert.AreEqual(string.Join(",", pinyin.FirstPinYin), "c,z");
            Assert.AreEqual(string.Join(",", pinyin.TotalPinYin), "chang,zhang");
        }

        [TestMethod]
        public void StreamTest()
        {
            var stream = "0".StringToStream();
            Assert.AreEqual(stream.ReadToEnd(), "0");

            var demoDic = new Dictionary<string, string>
            {
                { "qq", "10000" },
                { "code", "%43<>><%" }
            };
            Assert.AreEqual(new MemoryStream().FillFormDataStream(demoDic).ReadToEnd(), "qq=10000&code=%2543%3C%3E%3E%3C%25");

            var bytes = "0".StringToBytes();
            Assert.AreEqual(bytes.ConvertToString(), "0");
            bytes = "0".StringToStream().ToBytes();
            Assert.AreEqual(bytes.ConvertToString(), "0");
            Assert.AreEqual(bytes.ToStream().ReadToEnd(), "0");

            // 涉及文件操作，偶尔会因文件占用测试失败
            var demoName = "demo.txt";
            stream.WriteToFile(demoName);
            Assert.AreEqual(demoName.GetFileInfo().ReadToStream().ReadToEnd(), "0");
            try { System.IO.File.Delete(demoName); } catch { }
        }

        [TestMethod]
        public void StringTest()
        {
            Assert.AreEqual(StringExtension.RandCode().Length, 6);
            Assert.AreEqual("".RandCode(60).Length, 60);
            Assert.AreEqual(StringExtension.RandCode(60).Length, 60);
            Assert.AreEqual("".RandBlurCode(60).Length, 60);
            Assert.AreEqual(StringExtension.RandCode(60, true).Length, 60);
            Assert.IsTrue("".IsNullOrEmpty());
            string demo = null;
            Assert.IsTrue(demo.IsNullOrEmpty());
            Assert.IsTrue(" \t \r\n \t".IsNullOrWhiteSpace());
            Assert.IsFalse("0".IsNullOrEmpty());
            Assert.IsTrue("hi@qq.com".IsEmail());
            Assert.IsFalse("hi@qq".IsEmail());
            demo = "0123456789";
            Assert.AreEqual(demo.SubString(5).Length, 5);
            Assert.AreEqual(demo.SubString(2, 3).Length, 3);
            Assert.AreEqual(true.ToYesNo(false), "Yes");
            Assert.AreEqual(false.ToYesNo(false), "No");
            Assert.AreEqual(true.ToYesNo(true), "是");
            Assert.AreEqual(false.ToYesNo(true), "否");
            demo = "hi,ni,hao,ya";
            CollectionAssert.AreEqual(demo.SplitBySign(","), new string[] { "hi", "ni", "hao", "ya" });
            Assert.AreEqual(demo.SplitThenGetFirst(","), "hi");
            Assert.AreEqual(demo.SplitThenGetLast(","), "ya");
            CollectionAssert.AreEqual("hi,ni,hao,,,ya".SplitBySign(",", System.StringSplitOptions.RemoveEmptyEntries), new string[] { "hi", "ni", "hao", "ya" });
            demo = null;
            Assert.IsNull(demo.SplitBySign(","));
            demo = "hi,ni/hao*ya";
            CollectionAssert.AreEqual(demo.SplitByMultiSign(",", "/", "*"), new string[] { "hi", "ni", "hao", "ya" });
            CollectionAssert.AreEqual(demo.SplitAuto(), new string[] { "hi", "ni", "hao*ya" });
            CollectionAssert.AreEqual("\nhi\rni\r\nhao\rya".SplitByLine(), new string[] { "hi", "ni", "hao", "ya" });

            string dbc = "你好呀 programer", sbc = "你好呀　ｐｒｏｇｒａｍｅｒ";
            Assert.AreEqual(sbc.ToDBC(), dbc);
            Assert.AreEqual(dbc.ToSBC(), sbc);


            Assert.AreEqual("{0}/{1}".StringFormat("1", "2"), "1/2");
            CollectionAssert.AreEqual("你好".StringToArray(), new string[] { "你", "好" });
            Assert.IsFalse("hi".IsChineseRegString());
            Assert.IsTrue("你好".IsChineseRegString());
        }

        [TestMethod]
        public void StringConvertTest()
        {
            string numeric = "-100.00",
                notNumeric = "-100.00l",
                   integer = "-100",
                   unsignN = "100.00",
                   nullStr = null;
            Assert.IsTrue(numeric.IsNumeric());
            Assert.IsFalse(notNumeric.IsNumeric());
            Assert.IsTrue(integer.IsInteger());
            Assert.IsFalse(numeric.IsInteger());
            Assert.IsTrue(unsignN.IsUnsignNumeric());
            Assert.IsFalse(numeric.IsUnsignNumeric());

            Assert.AreEqual("hima(-1000.250)nani".ToNumeric(), "-1000.250");
            Assert.AreEqual(notNumeric.ToNumeric(), "-100.00");
            Assert.IsNull(nullStr.ToNumeric());
            Assert.AreEqual(notNumeric.ToIntegerNumeric(), "-100");
            Assert.IsNull(nullStr.ToIntegerNumeric());

            Assert.AreEqual(numeric.ConvertToNullableSByte(), (sbyte)-100);
            Assert.AreEqual(unsignN.ConvertToNullableByte(), (byte)100);
            Assert.AreEqual(numeric.ConvertToNullableShort(), (short)-100);
            Assert.AreEqual(numeric.ConvertToNullableInt(), -100);
            Assert.AreEqual(numeric.ConvertToNullableLong(), -100);
            Assert.AreEqual(numeric.ConvertToNullableBigInteger(), -100);
            Assert.AreEqual(numeric.ConvertToNullableFloat(), -100.00f);
            Assert.AreEqual(numeric.ConvertToNullableDouble(), -100.00d);
            Assert.AreEqual(numeric.ConvertToNullableDecimal(), -100.00m);
            Assert.AreEqual(nullStr.ConvertToSByte(), 0);
            Assert.AreEqual(nullStr.ConvertToByte(), 0);
            Assert.AreEqual(nullStr.ConvertToShort(), 0);
            Assert.AreEqual(nullStr.ConvertToInt(), 0);
            Assert.AreEqual(nullStr.ConvertToLong(), 0);
            Assert.AreEqual(nullStr.ConvertToBigInteger(), 0);
            Assert.AreEqual(nullStr.ConvertToFloat(), 0);
            Assert.AreEqual(nullStr.ConvertToDouble(), 0);
            Assert.AreEqual(nullStr.ConvertToDecimal(), 0);

            var bint = new System.Numerics.BigInteger(50000000000000000);
            Assert.AreEqual("2500000000000000000000000000000000".ConvertToBigInteger(), bint * bint);

            Assert.AreEqual("2017-01-01 01:30".ConvertToDatetime(), new DateTime(2017, 01, 01, 01, 30, 00));
            Assert.ThrowsException<System.FormatException>(() => "2017-13-33".ConvertToDatetime());
            Assert.AreEqual("2017-01-01 01:30".ConvertToNullableDatetime(), new DateTime(2017, 01, 01, 01, 30, 00));
            Assert.IsNull("2017-13-33".ConvertToNullableDatetime());

            Assert.IsTrue("true".ConvertToBoolean());
            Assert.IsTrue("1".ConvertToBoolean());
            Assert.IsTrue("yes".ConvertToBoolean());
            Assert.IsTrue("checked".ConvertToBoolean());
            Assert.IsTrue("是".ConvertToBoolean());
            Assert.IsTrue("对".ConvertToBoolean());
            Assert.IsFalse("".ConvertToBoolean());
            Assert.IsFalse("asdasd".ConvertToBoolean());
            Assert.AreEqual("1".ConvertToNullableBoolean(), true);
            Assert.IsNull(nullStr.ConvertToNullableBoolean());

            Assert.AreEqual("http://www.qq.com/".ToUri(), new System.Uri("http://www.qq.com/"));
            Assert.IsNull("http//www.qq.com/".ToNullableUri());

            Assert.AreEqual("qq=10000".ToCookie(), new System.Net.Cookie("qq", "10000"));
            CollectionAssert.AreEqual("qq=10000;wechat=weixin".ToCookieCollection(), new System.Net.CookieCollection() {
                new Cookie("qq", "10000"),
                new Cookie("wechat", "weixin")
            });

            Assert.AreEqual("qq".StringToStream(System.Text.Encoding.ASCII).ReadToEnd(System.Text.Encoding.ASCII), "qq");
            Assert.AreEqual("qq".StringToBytes(System.Text.Encoding.ASCII).ConvertToString(System.Text.Encoding.ASCII), "qq");
        }


        [TestMethod]
        public void ZipTest()
        {
            Assert.AreEqual("qq".GZipCompressString().GZipDecompressString(), "qq");

#if NoBuild
            // 示例方法 不进行单元测试
            "dirPath".GetDirectoryInfo().CreateZipFile("filename.zip");
            "filename.zip".GetFileInfo().ExtractZipFile("dirPath");
#endif
        }


#if NoBuild
        // 示例方法 不进行单元测试
        public void HttpClientTest()
        {
            OYMLCN.HttpClient.HttpGetString("http://localhost:5000/");
            var cookie = new CookieContainer();
            OYMLCN.HttpClient.HttpGetString("http://localhost:5000/", null, 300, cookie);
            OYMLCN.HttpClient.HttpGetString("http://localhost:5000/", null, 30, cookie);
            OYMLCN.HttpClient.HttpPostData("http://localhost:5000/", "testForPost").ReadToEnd();
            var dic = new Dictionary<string, string>();
            dic.Add("test1", "ddd");
            dic.Add("ddd", "dddas");
            dic.Add("file", @"C:\test.txt");
            OYMLCN.HttpClient.HttpPostData("http://localhost:5000/Test", "{'json':'00'}", mediaType: "application/json", queryDir: dic).ReadToEnd();
            OYMLCN.HttpClient.HttpPostJsonString("http://localhost:5000/Test/Json", "{'json':'00'}");
            OYMLCN.HttpClient.HttpPostJsonString("http://localhost:5000/Test/Xml", "<xml><data>test</data></xml>");
        }
#endif
    }
}
