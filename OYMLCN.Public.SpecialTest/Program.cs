using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OYMLCN.Open.SpecialTest
{
    class Program
    {
        static void Main(string[] args)
        {
#if NETCOREAPP1_0
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
            var start = DateTime.Now;
            //Console.WriteLine(OYMLCN.HttpClient.HttpGetString("http://localhost:5000/"));
            //Console.WriteLine(DateTime.Now - start);
            //start = DateTime.Now;

            //CookieContainer cookie = new CookieContainer();
            //Console.WriteLine(OYMLCN.HttpClient.HttpGetString("http://localhost:5000/", null, 300, cookie));
            //Console.WriteLine(DateTime.Now - start);

            //start = DateTime.Now;
            //Console.WriteLine(OYMLCN.HttpClient.HttpGetString("http://localhost:5000/", null, 30, cookie));
            //Console.WriteLine(DateTime.Now - start);

            //start = DateTime.Now;
            //Console.WriteLine(OYMLCN.HttpClient.HttpPostData("http://localhost:5000/", "testForPost").ReadToEnd());
            //Console.WriteLine(DateTime.Now - start);

            start = DateTime.Now;
            var dic = new Dictionary<string, string>();
            dic.Add("test1", "ddd");
            dic.Add("ddd", "dddas");
            dic.Add("file", @"C:\Users\Vic\Desktop\index.html");
            Console.WriteLine(OYMLCN.HttpClient.PostData("http://localhost:4322/Home/JsonTest", "{'json':'00'}", mediaType: "application/json", queryDir: dic).ReadToEnd());
            Console.WriteLine(DateTime.Now - start);

            Console.WriteLine(OYMLCN.HttpClient.PostJsonString("http://localhost:4322/Home/JsonTest", "{'json':'00'}"));



            Console.ReadLine();
        }
    }
}