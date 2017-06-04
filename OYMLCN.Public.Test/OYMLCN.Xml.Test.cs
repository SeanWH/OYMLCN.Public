using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace OYMLCN.Xml.Test
{
    [TestClass]
    public class UnitTest
    {

        public class TestData
        {
            public string st { get; set; }
            public List<TestData> td { get; set; }
        }
        readonly TestData obj = new TestData()
        {
            st = "hi",
            td = new List<TestData>()
            {
               new TestData(){ st="qq" },
               new TestData(){ st="pp" },
            }
        };

        [TestMethod]
        public void XmlTest()
        {
            var demo = obj.ToXmlStream().DeserializeXmlString<TestData>();
            Assert.AreEqual(demo.st, "hi");
            demo = obj.ToXmlString().DeserializeXmlString<TestData>();
            Assert.AreEqual(demo.st, "hi");
            var doc = obj.ToXmlStream().ToXDocument();
            Assert.AreEqual(doc.Element("TestData").Element("td").Elements("TestData").SelectValue("st"), "qq");

            var xml = "<xml><st>hi</st><td><st>qq</st></td><td><st>pp</st></td></xml>";
            doc = xml.ToXDocument();
            Assert.AreEqual(doc.SelectValue("st"), "hi");
            Assert.AreEqual(doc.Root.SelectValue("st"), "hi");
            Assert.AreEqual(doc.Root.Elements("td").SelectValue("st"), "qq");
            CollectionAssert.AreEqual(doc.Root.Elements("td").SelectValues("st"), new string[] { "qq", "pp" });
        }
    }
}
