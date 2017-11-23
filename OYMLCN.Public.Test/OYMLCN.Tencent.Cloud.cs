using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OYMLCN.Tencent.Cloud.Test
{
    [TestClass]
    public class UnitTest
    {
        const int AppId = 1251041168;
        const string SecretId = "AKIDOisutwqKqQp5OdtghbL9pW5SficL5usm";
        const string SecretKey = "28FCA5pNkw40mpibO1ShleLqBPRHvQly";
        const string CosBucketName = "img";
        [TestMethod]
        public void CosTest()
        {
            var cos = new CosCloud(AppId, SecretId, SecretKey);
            //var rs1 = cos.UploadFile(CosBucketName, "/test/ha.png", @"C:\Users\Vic\Desktop\logo.png");
            //var rs2 = cos.UploadStream(CosBucketName, "/test/ha.png", "ha.png", @"C:\Users\Vic\Desktop\logo.png".GetFileInfo().ReadToStream());
        }
    }
}
