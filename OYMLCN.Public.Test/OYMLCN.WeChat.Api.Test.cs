using Microsoft.VisualStudio.TestTools.UnitTesting;
using OYMLCN.WeChat.Enums;
using System;
using System.Collections.Generic;

namespace OYMLCN.WeChat.Test
{
    [TestClass]
    public class ApiUnitTest : Api
    {
        //Api.Config Config = new Api.Config("maiqunw", "wxb5b0dff3abebb00c", "4a1d6d04a0c04e2e6b016f629fe4b32c");

        ////[TestMethod]
        //public void WeChatApiTest()
        //{
        //    //var list = Api.GetCallbackIP(Api.GetAccessToken(Config));
        //}

        [TestClass]
        public class MenuTest : Api.Menu
        {
            [TestMethod]
            public void WeChatApiMenuTest()
            {
                //Config = new Api.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a");
                Assert.AreEqual(JsonCreate.Create(new List<Model.MenuBase>() {
                    new Model.MenuClick("今日歌曲","V1001_TODAY_MUSIC"),
                    new Model.MenuTop("菜单",new List<Model.MenuBase>(){
                        new Model.MenuView("搜索","http://www.soso.com/"),
                        new Model.MenuMiniProgram("wxa","wx286b93c14bbf93aa","pages/lunar/index.html","http://mp.weixin.qq.com"),
                        new Model.MenuClick("赞一下我们","V1001_GOOD")
                    })
                }), "{\"button\":[{\"type\":\"click\",\"name\":\"今日歌曲\",\"key\":\"V1001_TODAY_MUSIC\"},{\"name\":\"菜单\",\"sub_button\":[{\"type\":\"view\",\"name\":\"搜索\",\"url\":\"http://www.soso.com/\"},{\"type\":\"miniprogram\",\"name\":\"wxa\",\"url\":\"http://mp.weixin.qq.com\",\"appid\":\"wx286b93c14bbf93aa\",\"pagepath\":\"pages/lunar/index.html\"},{\"type\":\"click\",\"name\":\"赞一下我们\",\"key\":\"V1001_GOOD\"}]}]}");
                Assert.AreEqual(JsonCreate.Create(new List<Model.MenuBase>() {
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

                Assert.AreEqual(JsonCreate.AddCondition(new Model.MenuMatchRule()
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
                Assert.AreEqual(JsonCreate.TryMatch("weixin"), "{\"user_id\":\"weixin\"}");
                Assert.AreEqual(JsonCreate.DelCondition("208379533"), "{\"menuid\":\"208379533\"}");

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
        }

        [TestClass]
        public class MediaTest : Api.Material
        {
            [TestMethod]
            public void WeChatApiMediaTest()
            {
                //Api.Media.Upload(Api.GetAccessToken(Config), MediaType.Image, @"C:\Users\Vic\Desktop\topbar_logo.png");

                Assert.AreEqual(Api.Media.GetUrl("test", MediaType.Image, "demoMediaId"), "https://api.weixin.qq.com/cgi-bin/media/get?access_token=test&media_id=demoMediaId");
                Assert.AreEqual(Api.Media.SpeexDownloadUrl("test", "demoMediaId"), "https://api.weixin.qq.com/cgi-bin/media/get/jssdk?access_token=test&media_id=demoMediaId");

                Assert.AreEqual(Api.Material.MiniProgramContentXml("wx123123123", "pages/index/index", "小程序示例", "http://mmbizqbic.cn/demo.jpg"), "<mp-miniprogram data-miniprogram-appid=\"wx123123123\" data-miniprogram-path=\"pages/index/index\" data-miniprogram-title=\"小程序示例\" data-progarm-imageurl=\"http://mmbizqbic.cn/demo.jpg\"></mp-miniprogram>");

                Assert.AreEqual(JsonCreate.AddNews(new List<Model.Article>() {
                    new Model.Article()
                    {
                        title = "TITLE",
                        thumb_media_id = "THUMB_MEDIA_ID",
                        author = "AUTHOR",
                        digest = "DIGEST",
                        show_cover_pic = false,
                        content = "CONTENT",
                        content_source_url = "CONTENT_SOURCE_URL"
                    }
                }), "{\"articles\":[{\"title\":\"TITLE\",\"thumb_media_id\":\"THUMB_MEDIA_ID\",\"author\":\"AUTHOR\",\"digest\":\"DIGEST\",\"show_cover_pic\":0,\"content\":\"CONTENT\",\"content_source_url\":\"CONTENT_SOURCE_URL\"}]}");
                Assert.AreEqual(JsonCreate.Add("VIDEO_TITLE", "INTRODUCTION"), "{\"title\":\"VIDEO_TITLE\",\"introduction\":\"INTRODUCTION\"}");

                var getRequest = "{\"media_id\":\"MEDIA_ID\"}";
                Assert.AreEqual(JsonCreate.GetVideoInfo("MEDIA_ID"), getRequest);
                Assert.AreEqual(JsonCreate.GetNews("MEDIA_ID"), getRequest);
                Assert.AreEqual(JsonCreate.Delete("MEDIA_ID"), getRequest);

                Assert.AreEqual(JsonCreate.UpdateNews("MEDIA_ID", 1, new Model.Article()
                {
                    title = "TITLE",
                    thumb_media_id = "THUMB_MEDIA_ID",
                    author = "AUTHOR",
                    digest = "DIGEST",
                    show_cover_pic = true,
                    content = "CONTENT",
                    content_source_url = "CONTENT_SOURCE_URL"
                }), "{\"media_id\":\"MEDIA_ID\",\"index\":1,\"articles\":{\"title\":\"TITLE\",\"thumb_media_id\":\"THUMB_MEDIA_ID\",\"author\":\"AUTHOR\",\"digest\":\"DIGEST\",\"show_cover_pic\":1,\"content\":\"CONTENT\",\"content_source_url\":\"CONTENT_SOURCE_URL\"}}");

                Assert.AreEqual(JsonCreate.BatchGet("image", 0, 20), "{\"type\":\"image\",\"offset\":0,\"count\":20}");
                Assert.AreEqual(JsonCreate.BatchGetNews(0, 20), "{\"type\":\"news\",\"offset\":0,\"count\":20}");

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
        }

        [TestClass]
        public class CustomerServiceAccountManageTest : Api.CustomerService.Account
        {
            [TestMethod]
            public void WeChatApiCustomerServiceAccountManageTest()
            {
                Assert.AreEqual(JsonCreate.Add("test1@test", "客服1"), "{\"kf_account\":\"test1@test\",\"nickname\":\"客服1\"}");
                Assert.AreEqual(JsonCreate.Invite("test1@test", "test_kfwx"), "{\"kf_account\":\"test1@test\",\"invite_wx\":\"test_kfwx\"}");
                Assert.AreEqual(JsonCreate.Update("test1@test", "客服1"), "{\"kf_account\":\"test1@test\",\"nickname\":\"客服1\"}");

                //var token = Api.GetAccessToken(Config);
                //var account = Api.CustomerService.GetList(token);
                //var online = Api.CustomerService.GetOnlineList(token);
                //var add = Api.CustomerService.Account.Add(token, "测试01", "test1", Config);
                //var invite = Api.CustomerService.Account.Invite(token, "ouyangminlan", "test1", Config);
                //var update = Api.CustomerService.Account.Update(token, "测试号", "test1@maiqunw");
                //var upload = Api.CustomerService.Account.UploadHeadImg(token, @"C:\Users\Vic\Desktop\www.100qun.com.jpg", "test1", Config);
                //var del = Api.CustomerService.Account.Delete(token, "test1", Config);

                //var record = Api.CustomerService.Record.GetMsgList(token, DateTime.Now.AddHours(-1), DateTime.Now);
            }
        }
        [TestClass]
        public class CustomerServiceSessionTest : Api.CustomerService.Session
        {
            [TestMethod]
            public void WeChatApiCustomerServiceSessionTest()
            {
                string sessionRequest = "{\"kf_account\":\"test1@test\",\"openid\":\"OPENID\"}";
                Assert.AreEqual(JsonCreate.Create("test1@test", "OPENID"), sessionRequest);
                Assert.AreEqual(JsonCreate.Close("test1@test", "OPENID"), sessionRequest);
                //var token = Api.GetAccessToken(Config);
                //var openid = "okOeUwaD9TRRqFh_hyE4xlMwBg8Y";
                //var create = Api.CustomerService.Session.Create(token, openid, "test1", Config);
                //var get = Api.CustomerService.Session.Get(token, openid);
                //var getlist = Api.CustomerService.Session.GetList(token, "test1", Config);
                //var getwait = Api.CustomerService.Session.GetWaitCase(token);
                //var close = Api.CustomerService.Session.Close(token, openid, "test1", Config);
            }
        }
        [TestClass]
        public class CustomerServiceMessageSendTest : Api.CustomerService.MessageSend
        {
            [TestMethod]
            public void WeChatApiCustomerServiceMessageSendTest()
            {
                //Config = new Api.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a");

                Assert.AreEqual(JsonCreate.Text("OPENID", "Hello World"), "{\"touser\":\"OPENID\",\"msgtype\":\"text\",\"text\":{\"content\":\"Hello World\"}}");
                Assert.AreEqual(JsonCreate.Media("OPENID", "image", "MEDIA_ID"), "{\"touser\":\"OPENID\",\"msgtype\":\"image\",\"image\":{\"media_id\":\"MEDIA_ID\"}}");
                Assert.AreEqual(JsonCreate.Media("OPENID", "voice", "MEDIA_ID"), "{\"touser\":\"OPENID\",\"msgtype\":\"voice\",\"voice\":{\"media_id\":\"MEDIA_ID\"}}");
                Assert.AreEqual(JsonCreate.Video("OPENID", "MEDIA_ID", "MEDIA_ID", "TITLE", "DESCRIPTION"), "{\"touser\":\"OPENID\",\"msgtype\":\"video\",\"video\":{\"media_id\":\"MEDIA_ID\",\"thumb_media_id\":\"MEDIA_ID\",\"title\":\"TITLE\",\"description\":\"DESCRIPTION\"}}");
                Assert.AreEqual(JsonCreate.Music("OPENID", "THUMB_MEDIA_ID", "MUSIC_URL", "HQ_MUSIC_URL", "MUSIC_TITLE", "MUSIC_DESCRIPTION"), "{\"touser\":\"OPENID\",\"msgtype\":\"music\",\"music\":{\"title\":\"MUSIC_TITLE\",\"description\":\"MUSIC_DESCRIPTION\",\"musicurl\":\"MUSIC_URL\",\"hqmusicurl\":\"HQ_MUSIC_URL\",\"thumb_media_id\":\"THUMB_MEDIA_ID\"}}");
                Assert.AreEqual(JsonCreate.News("OPENID", new List<Article>() {
                    new Article("HappyDay","IsReallyAHappyDay","PIC_URL","URL"),
                    new Article("HappyDay","IsReallyAHappyDay","PIC_URL","URL")
                }), "{\"touser\":\"OPENID\",\"msgtype\":\"news\",\"news\":{\"articles\":[{\"title\":\"HappyDay\",\"description\":\"IsReallyAHappyDay\",\"url\":\"URL\",\"picurl\":\"PIC_URL\"},{\"title\":\"HappyDay\",\"description\":\"IsReallyAHappyDay\",\"url\":\"URL\",\"picurl\":\"PIC_URL\"}]}}");
                Assert.AreEqual(JsonCreate.Media("OPENID", "mpnews", "MEDIA_ID"), "{\"touser\":\"OPENID\",\"msgtype\":\"mpnews\",\"mpnews\":{\"media_id\":\"MEDIA_ID\"}}");
                Assert.AreEqual(JsonCreate.Card("OPENID", "123dsdajkasd231jhksad"), "{\"touser\":\"OPENID\",\"msgtype\":\"wxcard\",\"wxcard\":{\"card_id\":\"123dsdajkasd231jhksad\"}}");

                Assert.AreEqual(JsonCreate.Text("OPENID", "HelloWorld", "test1@kftest"), "{\"touser\":\"OPENID\",\"msgtype\":\"text\",\"text\":{\"content\":\"HelloWorld\"},\"customservice\":{\"kf_account\":\"test1@kftest\"}}");

                //var token = Api.GetAccessToken(Config);

                //Api.CustomerService.MessageSend.Text(token, "oOk2XjhrbcHP3tGgzDGAVHppo3Bs", "ApiTest");
                //Api.CustomerService.MessageSend.News(token, "oOk2XjhrbcHP3tGgzDGAVHppo3Bs",new List<Api.CustomerService.MessageSend.Article>() {
                //    new Api.CustomerService.MessageSend.Article("HappyDay","IsReallyAHappyDay","","http://www.qq.com"),
                //    new Api.CustomerService.MessageSend.Article("HappyDay","IsReallyAHappyDay","","")
                //});

            }
        }

        [TestClass]
        public class CustomerServiceRecordTest : Api.CustomerService.Record
        {
            [TestMethod]
            public void WeChatApiCustomerServiceRecordTest()
            {
                Assert.AreEqual(JsonCreate.GetMsgList(
                    ((long)987654321).TimestampToDateTime(),
                    ((long)987654321).TimestampToDateTime(),
                    1, 10000),
                    "{\"starttime\":987654321,\"endtime\":987654321,\"msgid\":1,\"number\":10000}");
                //Api.CustomerService.Record.GetMsgList("", DateTime.Now.AddDays(-1), DateTime.Now);
            }
        }

        [TestClass]
        public class UserTest : Api.User
        {
            [TestMethod]
            public void WeChatApiUserTest()
            {
                Assert.AreEqual(JsonCreate.UpdateRemark("oDF3iY9ffA-hqb2vVvbr7qxf6A0Q", "pangzi"), "{\"openid\":\"oDF3iY9ffA-hqb2vVvbr7qxf6A0Q\",\"remark\":\"pangzi\"}");

                //var token = Api.GetAccessToken(Config);
                //var list = Api.User.Get(token);
                //Model.UserInfo info = null;
                //foreach (var item in list.data.openid)
                //{
                //    info = Api.User.Info(token, item);
                //    if (info.nickname.Contains("VicBilibily"))
                //        throw new Exception(info.openid);//"okOeUwaD9TRRqFh_hyE4xlMwBg8Y"
                //}
                //var remark = Api.User.UpdateRemark(token, list[0], "");
            }
        }

        [TestClass]
        public class TagsTest : Api.Tags
        {
            [TestMethod]
            public void WeChatApiTagsTest()
            {
                Assert.AreEqual(JsonCreate.Create("广东"), "{\"tag\":{\"name\":\"广东\"}}");
                Assert.AreEqual(JsonCreate.Update(134, "广东人"), "{\"tag\":{\"id\":134,\"name\":\"广东人\"}}");
                Assert.AreEqual(JsonCreate.Delete(134), "{\"tag\":{\"id\":134}}");
                Assert.AreEqual(JsonCreate.GetUsers(134), "{\"tagid\":134,\"next_openid\":\"\"}");
                Assert.AreEqual(JsonCreate.GetUsers(134, "test"), "{\"tagid\":134,\"next_openid\":\"test\"}");
                Assert.AreEqual(JsonCreate.GetIdList("test"), "{\"openid\":\"test\"}");

                //Config = new Api.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a");
                //var token = Api.GetAccessToken(Config);
                //var create = Api.Tags.Create(token, "测试");
                //var tags = Api.Tags.Get(token);
                //var update = Api.Tags.Update(token, create.id, "test");
                //tags = Api.Tags.Get(token);
                //var del = Api.Tags.Delete(token, create.id);
                //tags = Api.Tags.Get(token);
                //var users = Api.Tags.GetUsers(token, tags[0].id);
                //var ids = Api.Tags.GetIdList(token, "oOk2XjhrbcHP3tGgzDGAVHppo3Bs");
            }
        }

        [TestClass]
        public class TagsMembersTest : Api.Tags.Members
        {
            [TestMethod]
            public void WeChatApiTagsMembersTest()
            {
                var taggingRequest = "{\"openid_list\":[\"ocYxcuAEy30bX0NXmGn4ypqx3tI0\",\"ocYxcuBt0mRugKZ7tGAHPnUaOW7Y\"],\"tagid\":134}";
                Assert.AreEqual(JsonCreate.BatchTagging(134, new List<string>() {
                    "ocYxcuAEy30bX0NXmGn4ypqx3tI0",
                    "ocYxcuBt0mRugKZ7tGAHPnUaOW7Y"
                }), taggingRequest);
                Assert.AreEqual(JsonCreate.BatchUntagging(134, new List<string>() {
                    "ocYxcuAEy30bX0NXmGn4ypqx3tI0",
                    "ocYxcuBt0mRugKZ7tGAHPnUaOW7Y"
                }), taggingRequest);

                Assert.AreEqual(JsonCreate.GetBlackList("OPENID1"), "{\"begin_openid\":\"OPENID1\"}");

                var blackRequest = "{\"openid_list\":[\"OPENID1\",\"OPENID2\"]}";
                Assert.AreEqual(JsonCreate.BatchBlackList(new List<string>() { "OPENID1", "OPENID2" }), blackRequest);
                Assert.AreEqual(JsonCreate.BatchUnblackList(new List<string>() { "OPENID1", "OPENID2" }), blackRequest);

                //Config = new Api.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a");
                //var openid = "oOk2XjhrbcHP3tGgzDGAVHppo3Bs";
                ////openid = "okOeUwaD9TRRqFh_hyE4xlMwBg8Y";
                //var token = Api.GetAccessToken(Config);
                //var tagging = Api.Tags.Members.BatchTagging(token, 2, openid);
                //var users = Api.Tags.GetUsers(token, 2);
                //var ids = Api.Tags.GetIdList(token, openid);
                //var untagging = Api.Tags.Members.BatchUntagging(token, 2, openid);
                //var blacklist = Api.Tags.Members.GetBlackList(token);
                //var black = Api.Tags.Members.BatchBlackList(token, openid);
                //blacklist = Api.Tags.Members.GetBlackList(token);
                //var unblack = Api.Tags.Members.BatchUnblackList(token, openid);
                //blacklist = Api.Tags.Members.GetBlackList(token);
            }
        }

        [TestClass]
        public class QRCodeTest : Api.QRCode
        {
            [TestMethod]
            public void WeChatApiQRCodeTest()
            {
                Assert.AreEqual(JsonCreate.CreateScene(123, 604800), "{\"expire_seconds\":604800,\"action_name\":\"QR_SCENE\",\"action_info\":{\"scene\":{\"scene_id\":123}}}");
                Assert.AreEqual(JsonCreate.CreateLimitScene(123), "{\"action_name\":\"QR_LIMIT_SCENE\",\"action_info\":{\"scene\":{\"scene_id\":123}}}");
                Assert.AreEqual(JsonCreate.CreateLimitScene("123"), "{\"action_name\":\"QR_LIMIT_STR_SCENE\",\"action_info\":{\"scene\":{\"scene_str\":\"123\"}}}");

                //Config = new Api.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a");
                //var token = Api.GetAccessToken(Config);
                //var code = Api.QRCode.CreateScene(token, 1);
                //var url = Api.QRCode.ShowUrl(code);
                //code = Api.QRCode.CreateLimitScene(token, 2);
                //url = Api.QRCode.ShowUrl(code);
                //code = Api.QRCode.CreateLimitScene(token, "2_0");
                //url = Api.QRCode.ShowUrl(code);
            }
        }

        [TestMethod]
        public void WeChatApiShortUrlTest()
        {
            Assert.AreEqual(JsonCreate.ShortUrl("http://wap.koudaitong.com/v2/showcase/goods?alias=128wi9shh&spm=h56083&redirect_count=1"), "{\"action\":\"long2short\",\"long_url\":\"http://wap.koudaitong.com/v2/showcase/goods?alias=128wi9shh&spm=h56083&redirect_count=1\"}");

            //var url = Api.ShortUrl(Api.GetAccessToken(Config), "http://www.qq.com/");
        }

        [TestClass]
        public class TemplateTest : Api.Template
        {
            [TestMethod]
            public void WeChatApiTemplateTest()
            {
                var data = new List<Model.TemplateParameter>()
                {
                    new Model.TemplateParameter("first","恭喜你购买成功！"),
                    new Model.TemplateParameter("keynote1","巧克力"),
                    new Model.TemplateParameter("keynote2","39.8元"),
                    new Model.TemplateParameter("keynote3","2014年9月22日"),
                    new Model.TemplateParameter("remark","欢迎再次购买！")
                };
                Assert.AreEqual(JsonCreate.SendMessage("OPENID", "ngqIpbwh8bUfcSsECmogfXcV14J0tQlEpBO27izEYtY", "http://weixin.qq.com/download", data),
                    "{\"touser\":\"OPENID\",\"template_id\":\"ngqIpbwh8bUfcSsECmogfXcV14J0tQlEpBO27izEYtY\",\"url\":\"http://weixin.qq.com/download\",\"data\":{\"first\":{\"value\":\"恭喜你购买成功！\",\"color\":\"#173177\"},\"keynote1\":{\"value\":\"巧克力\",\"color\":\"#173177\"},\"keynote2\":{\"value\":\"39.8元\",\"color\":\"#173177\"},\"keynote3\":{\"value\":\"2014年9月22日\",\"color\":\"#173177\"},\"remark\":{\"value\":\"欢迎再次购买！\",\"color\":\"#173177\"}}}");
                Assert.AreEqual(JsonCreate.SendMessage("OPENID", "ngqIpbwh8bUfcSsECmogfXcV14J0tQlEpBO27izEYtY", "http://weixin.qq.com/download", data, "xiaochengxuappid12345", "index?foo=bar"),
                    "{\"touser\":\"OPENID\",\"template_id\":\"ngqIpbwh8bUfcSsECmogfXcV14J0tQlEpBO27izEYtY\",\"url\":\"http://weixin.qq.com/download\",\"miniprogram\":{\"appid\":\"xiaochengxuappid12345\",\"pagepath\":\"index?foo=bar\"},\"data\":{\"first\":{\"value\":\"恭喜你购买成功！\",\"color\":\"#173177\"},\"keynote1\":{\"value\":\"巧克力\",\"color\":\"#173177\"},\"keynote2\":{\"value\":\"39.8元\",\"color\":\"#173177\"},\"keynote3\":{\"value\":\"2014年9月22日\",\"color\":\"#173177\"},\"remark\":{\"value\":\"欢迎再次购买！\",\"color\":\"#173177\"}}}");

                Assert.AreEqual(JsonCreate.SetIndustry(IndustryCode.IT科技_互联网_电子商务, IndustryCode.IT科技_电子技术), "{\"industry_id1\":\"1\",\"industry_id2\":\"4\"}");
                Assert.AreEqual(JsonCreate.Add("TM00015"), "{\"template_id_short\":\"TM00015\"}");
                Assert.AreEqual(JsonCreate.Delete("Dyvp3-Ff0cnail_CDSzk1fIc6-9lOkxsQE7exTJbwUE"), "{\"template_id\":\"Dyvp3-Ff0cnail_CDSzk1fIc6-9lOkxsQE7exTJbwUE\"}");

                //Config = new Api.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a");
                //var openid = "okOeUwaD9TRRqFh_hyE4xlMwBg8Y";
                //var token = Api.GetAccessToken(Config);
                //var send = Api.Template.SendMessage(token, openid, "avmLDdQFqlD9wGJyO5uFR7Y5IKylPYf2A6ZEt7frlxg", "", "",
                //    new Dictionary<string, string>()
                //    {
                //        {"test","测试"},
                //        {"name","名字" }
                //    });
                //var set = Api.Template.SetIndustry(token, IndustryCode.IT科技_电子技术, IndustryCode.IT科技_互联网_电子商务);
                //var industry = Api.Template.GetIndustry(token);
                //var add = Api.Template.Add(token, "TM00001");
                //var get = Api.Template.Get(token);
                //var del = Api.Template.Delete(token, get[0].template_id);
            }
        }

        [TestClass]
        public class MassTest : Api.Mass
        {
            [TestMethod]
            public void WeChatApiMassTest()
            {
                Assert.Fail("未编写单元测试");

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
        }

        [TestMethod]
        public void WeChatApiTicketTest()
        {
            //Config = new Api.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a");
            //var token = Api.GetAccessToken(Config);
            //var ticket = Api.Ticket.GetJsTicket(token);
            //var pack = Api.Ticket.CreateJsPackage(Config, ticket, "http://www.qq.com/");
        }

    }
}
