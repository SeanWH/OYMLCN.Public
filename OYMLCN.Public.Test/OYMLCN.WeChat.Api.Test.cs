using Microsoft.VisualStudio.TestTools.UnitTesting;
using OYMLCN.WeChat.Enums;
using System;
using System.Collections.Generic;

namespace OYMLCN.WeChat.Test
{
    [TestClass]
    public class ApiUnitTest
    {
        Api.Config Config = new Api.Config("maiqunw", "wxb5b0dff3abebb00c", "4a1d6d04a0c04e2e6b016f629fe4b32c");

        //[TestMethod]
        public void WeChatApiTest()
        {
            var list = Api.GetCallbackIP(Api.GetAccessToken(Config));
        }

        [TestMethod]
        public void WeChatApiMenuTest()
        {
            Config = new Api.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a");
            Assert.AreEqual(Api.Menu.CreateJson(new List<Model.MenuBase>() {
                new Model.MenuClick("今日歌曲","V1001_TODAY_MUSIC"),
                new Model.MenuTop("菜单",new List<Model.MenuBase>(){
                    new Model.MenuView("搜索","http://www.soso.com/"),
                    new Model.MenuMiniProgram("wxa","wx286b93c14bbf93aa","pages/lunar/index.html","http://mp.weixin.qq.com"),
                    new Model.MenuClick("赞一下我们","V1001_GOOD")
                })
            }), "{\"button\":[{\"type\":\"click\",\"name\":\"今日歌曲\",\"key\":\"V1001_TODAY_MUSIC\"},{\"name\":\"菜单\",\"sub_button\":[{\"type\":\"view\",\"name\":\"搜索\",\"url\":\"http://www.soso.com/\"},{\"type\":\"miniprogram\",\"name\":\"wxa\",\"url\":\"http://mp.weixin.qq.com\",\"appid\":\"wx286b93c14bbf93aa\",\"pagepath\":\"pages/lunar/index.html\"},{\"type\":\"click\",\"name\":\"赞一下我们\",\"key\":\"V1001_GOOD\"}]}]}");
            Assert.AreEqual(Api.Menu.CreateJson(new List<Model.MenuBase>() {
                new Model.MenuTop("扫码",new List<Model.MenuBase>(){
                    new Model.MenuScanWait("扫码带提示","rselfmenu_0_0"),
                    new Model.MenuScanPush("扫码推事件","rselfmenu_0_1")
                }),
                new Model.MenuTop("发图",new List<Model.MenuBase>(){
                    new Model.MenuSysPhoto("系统拍照发图","rselfmenu_1_0"),
                    new Model.MenuPhotoOrAlbum("拍照或者相册发图","rselfmenu_1_1"),
                    new Model.MenuPicWeixin("微信相册发图","rselfmenu_1_2")
                }),
                new Model.MenuLocationSelect("发送位置","rselfmenu_2_0"),
                new Model.MenuMedia("图片","MEDIA_ID1"),
                new Model.MenuViewLimited("图文消息","MEDIA_ID2")
            }), "{\"button\":[{\"name\":\"扫码\",\"sub_button\":[{\"type\":\"scancode_waitmsg\",\"name\":\"扫码带提示\",\"key\":\"rselfmenu_0_0\"},{\"type\":\"scancode_push\",\"name\":\"扫码推事件\",\"key\":\"rselfmenu_0_1\"}]},{\"name\":\"发图\",\"sub_button\":[{\"type\":\"pic_sysphoto\",\"name\":\"系统拍照发图\",\"key\":\"rselfmenu_1_0\"},{\"type\":\"pic_photo_or_album\",\"name\":\"拍照或者相册发图\",\"key\":\"rselfmenu_1_1\"},{\"type\":\"pic_weixin\",\"name\":\"微信相册发图\",\"key\":\"rselfmenu_1_2\"}]},{\"type\":\"location_select\",\"name\":\"发送位置\",\"key\":\"rselfmenu_2_0\"},{\"type\":\"media_id\",\"name\":\"图片\",\"media_id\":\"MEDIA_ID1\"},{\"type\":\"view_limited\",\"name\":\"图文消息\",\"media_id\":\"MEDIA_ID2\"}]}");

            Assert.AreEqual(Api.Menu.CreateConditionJson(new Model.MenuMatchRule()
            {
                TagId = 2,
                Sex = MenuMatchSex.男,
                Country = "中国",
                Province = "广东",
                City = "广州",
                ClientPlatformType = Enums.MenuMatchPlatform.Android,
                Language = MenuMatchLanguage.简体中文
            }, new List<Model.MenuBase>() {
                new Model.MenuClick("今日歌曲","V1001_TODAY_MUSIC"),
                new Model.MenuTop("菜单",new List<Model.MenuBase>(){
                    new Model.MenuView("搜索","http://www.soso.com/"),
                    new Model.MenuMiniProgram("wxa","wx286b93c14bbf93aa","pages/lunar/index.html","http://mp.weixin.qq.com"),
                    new Model.MenuClick("赞一下我们","V1001_GOOD")
                })
            }), "{\"button\":[{\"type\":\"click\",\"name\":\"今日歌曲\",\"key\":\"V1001_TODAY_MUSIC\"},{\"name\":\"菜单\",\"sub_button\":[{\"type\":\"view\",\"name\":\"搜索\",\"url\":\"http://www.soso.com/\"},{\"type\":\"miniprogram\",\"name\":\"wxa\",\"url\":\"http://mp.weixin.qq.com\",\"appid\":\"wx286b93c14bbf93aa\",\"pagepath\":\"pages/lunar/index.html\"},{\"type\":\"click\",\"name\":\"赞一下我们\",\"key\":\"V1001_GOOD\"}]}],\"matchrule\":{\"tag_id\":\"2\",\"sex\":\"1\",\"country\":\"中国\",\"province\":\"广东\",\"city\":\"广州\",\"client_platform_type\":\"2\",\"language\":\"zh_CN\"}}");

            //Api.Menu.Create(Api.GetAccessToken(Config), new List<Model.MenuBase>() {
            //    new Model.MenuTop("菜单",new List<Model.MenuBase>(){
            //        new Model.MenuView("搜索","http://www.soso.com/"),
            //        new Model.MenuClick("赞一下我们","V1001_GOOD"),
            //        new Model.MenuLocationSelect("发送位置","rselfmenu_2_0"),
            //    }),
            //    new Model.MenuTop("扫码",new List<Model.MenuBase>(){
            //        new Model.MenuScanWait("扫码带提示","rselfmenu_0_0"),
            //        new Model.MenuScanPush("扫码推事件","rselfmenu_0_1")
            //    }),
            //    new Model.MenuTop("发图",new List<Model.MenuBase>(){
            //        new Model.MenuSysPhoto("系统拍照发图","rselfmenu_1_0"),
            //        new Model.MenuPhotoOrAlbum("拍照或者相册发图","rselfmenu_1_1"),
            //        new Model.MenuPicWeixin("微信相册发图","rselfmenu_1_2")
            //    }),
            //});

            //var data = Api.Menu.Get(Api.GetAccessToken(Config));
            //data = Api.Menu.GetCurrentSelfMenuInfo(Api.GetAccessToken(Config));

            //var condition = Api.Menu.AddCondition(Api.GetAccessToken(Config), new Model.MenuMatchRule()
            //{
            //    TagId = 2,
            //    Sex = MenuMatchSex.男,
            //    Country = "中国",
            //    Province = "广东",
            //    City = "广州",
            //    ClientPlatformType = MenuMatchPlatform.Android,
            //    Language = MenuMatchLanguage.简体中文
            //}, new List<Model.MenuBase>() {
            //    new Model.MenuClick("今日歌曲","V1001_TODAY_MUSIC"),
            //    new Model.MenuTop("菜单",new List<Model.MenuBase>(){
            //        new Model.MenuView("搜索","http://www.soso.com/"),
            //        new Model.MenuClick("赞一下我们","V1001_GOOD")
            //    })
            //});
            //Api.Menu.DelCondition(Api.GetAccessToken(Config), condition);
            //var match = Api.Menu.TryMatch(Api.GetAccessToken(Config), "oOk2XjhrbcHP3tGgzDGAVHppo3Bs");
            //Api.Menu.Delete(Api.GetAccessToken(Config));
        }

        [TestMethod]
        public void WeChatApiMediaTest()
        {
            Assert.AreEqual(Api.Media.GetUrl(new Model.AccessToken() { access_token = "test" }, MediaType.Image, "demoMediaId"), "https://api.weixin.qq.com/cgi-bin/media/get?access_token=test&media_id=demoMediaId");
            Assert.AreEqual(Api.Media.SpeexDownloadUrl(new Model.AccessToken() { access_token = "test" }, "demoMediaId"), "https://api.weixin.qq.com/cgi-bin/media/get/jssdk?access_token=test&media_id=demoMediaId");
            Assert.AreEqual(Api.Material.MiniProgramContentXml("wx123123123", "pages/index/index", "小程序示例", "http://mmbizqbic.cn/demo.jpg"), "<mp-miniprogram data-miniprogram-appid=\"wx123123123\" data-miniprogram-path=\"pages/index/index\" data-miniprogram-title=\"小程序示例\" data-progarm-imageurl=\"http://mmbizqbic.cn/demo.jpg\"></mp-miniprogram>");


            //Api.Media.Upload(Api.GetAccessToken(Config), MediaType.Image, @"C:\Users\Vic\Desktop\topbar_logo.png");

            //var data = Api.Material.Add(Api.GetAccessToken(Config), MediaType.Image, @"C:\Users\Vic\Desktop\topbar_logo.png");
            //var url = Api.Material.UploadImage(Api.GetAccessToken(Config), @"C:\Users\Vic\Desktop\topbar_logo.png");
            //var newsMediaId = Api.Material.AddNews(Api.GetAccessToken(Config),
            //     new Model.MaterialNew()
            //     {
            //         title = "接口测试",
            //         thumb_media_id = data.media_id,
            //         author = "测试",
            //         digest = "测试信息",
            //         show_cover_pic = true,
            //         content = "测试呀",
            //         content_source_url = "http://www.qq.com/"
            //     }, new Model.MaterialNew()
            //     {
            //         title = "接口测试",
            //         thumb_media_id = data.media_id,
            //         author = "测试",
            //         digest = "测试信息",
            //         show_cover_pic = true,
            //         content = "测试呀",
            //         content_source_url = "http://www.qq.com/"
            //     }
            //     );
            //Api.Material.Get(Api.GetAccessToken(Config), MediaType.Image, data.media_id);
            //var newsData = Api.Material.GetNews(Api.GetAccessToken(Config), newsMediaId);
            //Api.Material.UpdateNews(Api.GetAccessToken(Config), newsMediaId, 1, new Model.MaterialNew()
            //{
            //    title = "接口测试" + DateTime.Now.ToTimeString(),
            //    thumb_media_id = data.media_id,
            //    author = "测试",
            //    digest = "测试信息",
            //    show_cover_pic = true,
            //    content = "测试呀",
            //    content_source_url = "http://www.qq.com/"
            //});

            //Api.Material.Delete(Api.GetAccessToken(Config), data.media_id);
            //Api.Material.Delete(Api.GetAccessToken(Config), newsMediaId);

            //var count = Api.Material.Count(Api.GetAccessToken(Config));
            //var batch = Api.Material.BatchGet(Api.GetAccessToken(Config), MediaType.Image, 0, 10);
            //var news = Api.Material.BatchGetNews(Api.GetAccessToken(Config), 0, 10);

        }

        //[TestMethod]
        public void WeChatApiCustomerServiceAccountManageTest()
        {
            var token = Api.GetAccessToken(Config);
            //var account = Api.CustomerService.GetList(token);
            //var online = Api.CustomerService.GetOnlineList(token);
            //var add = Api.CustomerService.Account.Add(token, "测试01", "test1", Config);
            //var invite = Api.CustomerService.Account.Invite(token, "ouyangminlan", "test1", Config);
            //var update = Api.CustomerService.Account.Update(token, "测试号", "test1@maiqunw");
            //var upload = Api.CustomerService.Account.UploadHeadImg(token, @"C:\Users\Vic\Desktop\www.100qun.com.jpg", "test1", Config);
            //var del = Api.CustomerService.Account.Delete(token, "test1", Config);

            var record = Api.CustomerService.Record.GetMsgList(token, DateTime.Now.AddHours(-1), DateTime.Now);
        }

        //[TestMethod]
        public void WeChatApiCustomerServiceSessionTest()
        {
            var token = Api.GetAccessToken(Config);
            var openid = "okOeUwaD9TRRqFh_hyE4xlMwBg8Y";
            var create = Api.CustomerService.Session.Create(token, openid, "test1", Config);
            var get = Api.CustomerService.Session.Get(token, openid);
            var getlist = Api.CustomerService.Session.GetList(token, "test1", Config);
            var getwait = Api.CustomerService.Session.GetWaitCase(token);
            var close = Api.CustomerService.Session.Close(token, openid, "test1", Config);
        }

        [TestMethod]
        public void WeChatApiCustomerServiceMessageSendTest()
        {
            Config = new Api.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a");

            Assert.AreEqual(Api.CustomerService.MessageSend.TextJson("OPENID", "Hello World"), "{\"touser\":\"OPENID\",\"msgtype\":\"text\",\"text\":{\"content\":\"Hello World\"}}");
            Assert.AreEqual(Api.CustomerService.MessageSend.ImageJson("OPENID", "MEDIA_ID"), "{\"touser\":\"OPENID\",\"msgtype\":\"image\",\"image\":{\"media_id\":\"MEDIA_ID\"}}");
            Assert.AreEqual(Api.CustomerService.MessageSend.VoiceJson("OPENID", "MEDIA_ID"), "{\"touser\":\"OPENID\",\"msgtype\":\"voice\",\"voice\":{\"media_id\":\"MEDIA_ID\"}}");
            Assert.AreEqual(Api.CustomerService.MessageSend.VideoJson("OPENID", "MEDIA_ID", "MEDIA_ID", "TITLE", "DESCRIPTION"), "{\"touser\":\"OPENID\",\"msgtype\":\"video\",\"video\":{\"media_id\":\"MEDIA_ID\",\"thumb_media_id\":\"MEDIA_ID\",\"title\":\"TITLE\",\"description\":\"DESCRIPTION\"}}");
            Assert.AreEqual(Api.CustomerService.MessageSend.MusicJson("OPENID", "THUMB_MEDIA_ID", "MUSIC_URL", "HQ_MUSIC_URL", "MUSIC_TITLE", "MUSIC_DESCRIPTION"), "{\"touser\":\"OPENID\",\"msgtype\":\"music\",\"music\":{\"title\":\"MUSIC_TITLE\",\"description\":\"MUSIC_DESCRIPTION\",\"musicurl\":\"MUSIC_URL\",\"hqmusicurl\":\"HQ_MUSIC_URL\",\"thumb_media_id\":\"THUMB_MEDIA_ID\"}}");
            Assert.AreEqual(Api.CustomerService.MessageSend.NewsJson("OPENID", new List<Api.CustomerService.MessageSend.Article>() {
                new Api.CustomerService.MessageSend.Article("HappyDay","IsReallyAHappyDay","PIC_URL","URL"),
                new Api.CustomerService.MessageSend.Article("HappyDay","IsReallyAHappyDay","PIC_URL","URL")
            }), "{\"touser\":\"OPENID\",\"msgtype\":\"news\",\"news\":{\"articles\":[{\"title\":\"HappyDay\",\"description\":\"IsReallyAHappyDay\",\"url\":\"URL\",\"picurl\":\"PIC_URL\"},{\"title\":\"HappyDay\",\"description\":\"IsReallyAHappyDay\",\"url\":\"URL\",\"picurl\":\"PIC_URL\"}]}}");
            Assert.AreEqual(Api.CustomerService.MessageSend.MpNewsJson("OPENID", "MEDIA_ID"), "{\"touser\":\"OPENID\",\"msgtype\":\"mpnews\",\"mpnews\":{\"media_id\":\"MEDIA_ID\"}}");
            Assert.AreEqual(Api.CustomerService.MessageSend.CardJson("OPENID", "123dsdajkasd231jhksad"), "{\"touser\":\"OPENID\",\"msgtype\":\"wxcard\",\"wxcard\":{\"card_id\":\"123dsdajkasd231jhksad\"}}");
            Assert.AreEqual(Api.CustomerService.MessageSend.SendAs(null, Api.CustomerService.MessageSend.TextJson("OPENID", "HelloWorld"), "test1@kftest", null), "{\"touser\":\"OPENID\",\"msgtype\":\"text\",\"text\":{\"content\":\"HelloWorld\"},\"customservice\":{\"kf_account\":\"test1@kftest\"}}");
            Assert.AreEqual(Api.CustomerService.MessageSend.SendAs(null, Api.CustomerService.MessageSend.TextJson("OPENID", "HelloWorld"), "test1", new Api.Config("kftest", "", "")), "{\"touser\":\"OPENID\",\"msgtype\":\"text\",\"text\":{\"content\":\"HelloWorld\"},\"customservice\":{\"kf_account\":\"test1@kftest\"}}");

            //var token = Api.GetAccessToken(Config);

            //Api.CustomerService.MessageSend.Text(token, "oOk2XjhrbcHP3tGgzDGAVHppo3Bs", "ApiTest");
            //Api.CustomerService.MessageSend.News(token, "oOk2XjhrbcHP3tGgzDGAVHppo3Bs",new List<Api.CustomerService.MessageSend.Article>() {
            //    new Api.CustomerService.MessageSend.Article("HappyDay","IsReallyAHappyDay","","http://www.qq.com"),
            //    new Api.CustomerService.MessageSend.Article("HappyDay","IsReallyAHappyDay","","")
            //});

        }


        //[TestMethod]
        public void WeChatApiUserTest()
        {
            var token = Api.GetAccessToken(Config);
            var list = Api.User.Get(token);
            Model.UserInfo info = null;
            foreach (var item in list.data.openid)
            {
                info = Api.User.Info(token, item);
                if (info.nickname.Contains("VicBilibily"))
                    throw new Exception(info.openid);//"okOeUwaD9TRRqFh_hyE4xlMwBg8Y"
            }
            //var remark = Api.User.UpdateRemark(token, list[0], "");
        }

        [TestMethod]
        public void WeChatApiTagsTest()
        {
            Config = new Api.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a");
            var token = Api.GetAccessToken(Config);
            var create = Api.Tags.Create(token, "测试");
            var tags = Api.Tags.Get(token);
            var update = Api.Tags.Update(token, create.id, "test");
            tags = Api.Tags.Get(token);
            var del = Api.Tags.Delete(token, create.id);
            tags = Api.Tags.Get(token);
            var users = Api.Tags.GetUsers(token, tags[0].id);
            var ids = Api.Tags.GetIdList(token, "oOk2XjhrbcHP3tGgzDGAVHppo3Bs");
        }

        //[TestMethod]
        public void WeChatApiTagsMembersTest()
        {
            Config = new Api.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a");
            var openid = "oOk2XjhrbcHP3tGgzDGAVHppo3Bs";
            //openid = "okOeUwaD9TRRqFh_hyE4xlMwBg8Y";
            var token = Api.GetAccessToken(Config);
            var tagging = Api.Tags.Members.BatchTagging(token, 2, openid);
            var users = Api.Tags.GetUsers(token, 2);
            var ids = Api.Tags.GetIdList(token, openid);
            var untagging = Api.Tags.Members.BatchUntagging(token, 2, openid);
            var blacklist = Api.Tags.Members.GetBlackList(token);
            var black = Api.Tags.Members.BatchBlackList(token, openid);
            blacklist = Api.Tags.Members.GetBlackList(token);
            var unblack = Api.Tags.Members.BatchUnblackList(token, openid);
            blacklist = Api.Tags.Members.GetBlackList(token);

        }

        //[TestMethod]
        public void WeChatApiQRCodeTest()
        {
            Config = new Api.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a");
            var token = Api.GetAccessToken(Config);
            var code = Api.QRCode.CreateScene(token, 1);
            var url = Api.QRCode.ShowUrl(code);
            code = Api.QRCode.CreateLimitScene(token, 2);
            url = Api.QRCode.ShowUrl(code);
            code = Api.QRCode.CreateLimitScene(token, "2_0");
            url = Api.QRCode.ShowUrl(code);
        }

        //[TestMethod]
        public void WeChatApiShortUrlTest()
        {
            var url = Api.ShortUrl(Api.GetAccessToken(Config), "http://www.qq.com/");
        }

        //[TestMethod]
        public void WeChatApiTemplateTest()
        {
            Config = new Api.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a");
            var openid = "okOeUwaD9TRRqFh_hyE4xlMwBg8Y";
            var token = Api.GetAccessToken(Config);
            var send = Api.Template.SendMessage(token, openid, "avmLDdQFqlD9wGJyO5uFR7Y5IKylPYf2A6ZEt7frlxg", "", "",
                new Dictionary<string, string>()
                {
                    {"test","测试"},
                    {"name","名字" }
                });
            //var set = Api.Template.SetIndustry(token, IndustryCode.IT科技_电子技术, IndustryCode.IT科技_互联网_电子商务);
            //var industry = Api.Template.GetIndustry(token);
            //var add = Api.Template.Add(token, "TM00001");
            //var get = Api.Template.Get(token);
            //var del = Api.Template.Delete(token, get[0].template_id);
        }

        [TestMethod]
        public void WeChatApiMassTest()
        {
            //var token = Api.GetAccessToken(Config);
            //var openid = "okOeUwaD9TRRqFh_hyE4xlMwBg8Y";
            //var data = Api.Media.Upload(Api.GetAccessToken(Config), MediaType.Image, @"C:\Users\Vic\Desktop\topbar_logo.png");
            //var news = Api.Mass.UploadNews(token, new Model.Article()
            //{
            //    title = "接口测试",
            //    thumb_media_id = data.media_id,
            //    author = "测试",
            //    digest = "测试信息",
            //    show_cover_pic = true,
            //    content = "测试呀",
            //    content_source_url = "http://www.qq.com/"
            //});
            //var preview = Api.Mass.PreviewMpNews(token, news.media_id, openid);
            //var preview = Api.Mass.PreviewMedia(token,MediaType.Image, data.media_id, null, "ouyangminlan");
            //var preview2 = Api.Mass.PreviewText(token, "dd", null, "VicWeChatTest");

        }

        //[TestMethod]
        public void WeChatApiTicketTest()
        {
            Config = new Api.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a");
            var token = Api.GetAccessToken(Config);
            var ticket = Api.Ticket.GetJsTicket(token);
            var pack = Api.Ticket.CreateJsPackage(Config, ticket, "http://www.qq.com/");
        }

    }
}
