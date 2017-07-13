using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace OYMLCN.WeChat.Test
{
    [TestClass]
    public class CoreUnitTest
    {
        Config Config = new Config("test", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a", "wxapi");
        PostModel PostModel = PostModel.Build(new Dictionary<string, string>()
        {
            {"nonce","1362870167" },
            {"openid","oOk2XjhrbcHP3tGgzDGAVHppo3Bs" },
            {"signature","7940891098b505c22f99b0e3708627ec715aa832" },
            {"timestamp","1496218735" }
        });

        [TestMethod]
        public void WeChatRequestTest()
        {
            string textMsg = @"<xml>
 <ToUserName><![CDATA[toUser]]></ToUserName>
 <FromUserName><![CDATA[fromUser]]></FromUserName>
 <CreateTime>1348831860</CreateTime>
 <MsgType><![CDATA[text]]></MsgType>
 <Content><![CDATA[this is a test]]></Content>
 <MsgId>1234567890123456</MsgId>
 </xml>";

            var request = WeChatRequest.Build(Config, PostModel, textMsg);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Text);
            Assert.AreEqual(request.ToUserName, "toUser");
            Assert.AreEqual(request.FromUserName, "fromUser");
            Assert.AreEqual(request.CreateTime, 1348831860);
            Assert.AreEqual(request.MsgId, 1234567890123456);
            Assert.AreEqual(request.MessageText.Content, "this is a test");

            string imgMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1348831860</CreateTime>
<MsgType><![CDATA[image]]></MsgType>
<PicUrl><![CDATA[this is a url]]></PicUrl>
<MediaId><![CDATA[media_id]]></MediaId>
<MsgId>1234567890123456</MsgId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, imgMsg);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Image);
            Assert.AreEqual(request.ToUserName, "toUser");
            Assert.AreEqual(request.FromUserName, "fromUser");
            Assert.AreEqual(request.CreateTime, 1348831860);
            Assert.AreEqual(request.MessageImage.PicUrl, "this is a url");
            Assert.AreEqual(request.MessageImage.MediaId, "media_id");
            Assert.AreEqual(request.MsgId, 1234567890123456);

            string voiceMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1357290913</CreateTime>
<MsgType><![CDATA[voice]]></MsgType>
<MediaId><![CDATA[media_id]]></MediaId>
<Format><![CDATA[Format]]></Format>
<MsgId>1234567890123456</MsgId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, voiceMsg);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Voice);
            Assert.AreEqual(request.ToUserName, "toUser");
            Assert.AreEqual(request.FromUserName, "fromUser");
            Assert.AreEqual(request.CreateTime, 1357290913);
            Assert.AreEqual(request.MessageVoice.MediaId, "media_id");
            Assert.AreEqual(request.MessageVoice.Format, "Format");
            Assert.AreEqual(request.MsgId, 1234567890123456);

            voiceMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1357290913</CreateTime>
<MsgType><![CDATA[voice]]></MsgType>
<MediaId><![CDATA[media_id]]></MediaId>
<Format><![CDATA[Format]]></Format>
<Recognition><![CDATA[腾讯微信团队]]></Recognition>
<MsgId>1234567890123456</MsgId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, voiceMsg);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Voice);
            Assert.AreEqual(request.ToUserName, "toUser");
            Assert.AreEqual(request.FromUserName, "fromUser");
            Assert.AreEqual(request.CreateTime, 1357290913);
            Assert.AreEqual(request.MessageVoice.MediaId, "media_id");
            Assert.AreEqual(request.MessageVoice.Format, "Format");
            Assert.AreEqual(request.MessageVoice.Recognition, "腾讯微信团队");
            Assert.AreEqual(request.MsgId, 1234567890123456);

            var videoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1357290913</CreateTime>
<MsgType><![CDATA[video]]></MsgType>
<MediaId><![CDATA[media_id]]></MediaId>
<ThumbMediaId><![CDATA[thumb_media_id]]></ThumbMediaId>
<MsgId>1234567890123456</MsgId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, videoMsg);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Video);
            Assert.AreEqual(request.ToUserName, "toUser");
            Assert.AreEqual(request.FromUserName, "fromUser");
            Assert.AreEqual(request.CreateTime, 1357290913);
            Assert.AreEqual(request.MessageVideo.MediaId, "media_id");
            Assert.AreEqual(request.MessageVideo.ThumbMediaId, "thumb_media_id");
            Assert.AreEqual(request.MsgId, 1234567890123456);

            videoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1357290913</CreateTime>
<MsgType><![CDATA[shortvideo]]></MsgType>
<MediaId><![CDATA[media_id]]></MediaId>
<ThumbMediaId><![CDATA[thumb_media_id]]></ThumbMediaId>
<MsgId>1234567890123456</MsgId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, videoMsg);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.ShortVideo);
            Assert.AreEqual(request.ToUserName, "toUser");
            Assert.AreEqual(request.FromUserName, "fromUser");
            Assert.AreEqual(request.CreateTime, 1357290913);
            Assert.AreEqual(request.MessageVideo.MediaId, "media_id");
            Assert.AreEqual(request.MessageVideo.ThumbMediaId, "thumb_media_id");
            Assert.AreEqual(request.MsgId, 1234567890123456);

            var locationMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1351776360</CreateTime>
<MsgType><![CDATA[location]]></MsgType>
<Location_X>23.134521</Location_X>
<Location_Y>113.358803</Location_Y>
<Scale>20</Scale>
<Label><![CDATA[位置信息]]></Label>
<MsgId>1234567890123456</MsgId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, locationMsg);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Location);
            Assert.AreEqual(request.ToUserName, "toUser");
            Assert.AreEqual(request.FromUserName, "fromUser");
            Assert.AreEqual(request.CreateTime, 1351776360);
            Assert.AreEqual(request.MessageLocation.Location_X, 23.134521);
            Assert.AreEqual(request.MessageLocation.Location_Y, 113.358803);
            Assert.AreEqual(request.MessageLocation.Scale, 20);
            Assert.AreEqual(request.MessageLocation.Label, "位置信息");
            Assert.AreEqual(request.MsgId, 1234567890123456);

            var linkMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1351776360</CreateTime>
<MsgType><![CDATA[link]]></MsgType>
<Title><![CDATA[公众平台官网链接]]></Title>
<Description><![CDATA[公众平台官网链接]]></Description>
<Url><![CDATA[url]]></Url>
<MsgId>1234567890123456</MsgId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, linkMsg);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Link);
            Assert.AreEqual(request.ToUserName, "toUser");
            Assert.AreEqual(request.FromUserName, "fromUser");
            Assert.AreEqual(request.CreateTime, 1351776360);
            Assert.AreEqual(request.MessageLink.Title, "公众平台官网链接");
            Assert.AreEqual(request.MessageLink.Description, "公众平台官网链接");
            Assert.AreEqual(request.MessageLink.Url, "url");
            Assert.AreEqual(request.MsgId, 1234567890123456);

        }

        [TestMethod]
        public void WeChatRequestEventTest()
        {
            var subscribeEvent = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[subscribe]]></Event>
</xml>";
            var request = WeChatRequest.Build(Config, PostModel, subscribeEvent);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.Event关注);
            Assert.AreEqual(request.ToUserName, "toUser");
            Assert.AreEqual(request.FromUserName, "FromUser");
            Assert.AreEqual(request.CreateTime, 123456789);
            Assert.IsTrue(request.Event关注);

            subscribeEvent = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[unsubscribe]]></Event>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, subscribeEvent);
            Assert.IsTrue(request.Event取消关注);

            subscribeEvent = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[subscribe]]></Event>
<EventKey><![CDATA[qrscene_123123]]></EventKey>
<Ticket><![CDATA[TICKET]]></Ticket>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, subscribeEvent);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.Event关注);
            Assert.AreEqual(request.ToUserName, "toUser");
            Assert.AreEqual(request.FromUserName, "FromUser");
            Assert.AreEqual(request.CreateTime, 123456789);
            Assert.AreEqual(request.Event扫描带参数二维码.EventKey, "qrscene_123123");
            Assert.AreEqual(request.Event扫描带参数二维码.SceneId, "123123");
            Assert.AreEqual(request.Event扫描带参数二维码.Ticket, "TICKET");

            subscribeEvent = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[SCAN]]></Event>
<EventKey><![CDATA[SCENE_VALUE]]></EventKey>
<Ticket><![CDATA[TICKET]]></Ticket>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, subscribeEvent);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.Event扫描带参数二维码);
            Assert.AreEqual(request.ToUserName, "toUser");
            Assert.AreEqual(request.FromUserName, "FromUser");
            Assert.AreEqual(request.CreateTime, 123456789);
            Assert.AreEqual(request.Event扫描带参数二维码.EventKey, "SCENE_VALUE");
            Assert.AreEqual(request.Event扫描带参数二维码.SceneId, "SCENE_VALUE");
            Assert.AreEqual(request.Event扫描带参数二维码.Ticket, "TICKET");


            var locationEvent = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[LOCATION]]></Event>
<Latitude>23.137466</Latitude>
<Longitude>113.352425</Longitude>
<Precision>119.385040</Precision>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, locationEvent);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.Event上报地理位置);
            Assert.AreEqual(request.ToUserName, "toUser");
            Assert.AreEqual(request.FromUserName, "fromUser");
            Assert.AreEqual(request.CreateTime, 123456789);
            Assert.AreEqual(request.Event上报地理位置.Latitude, 23.137466);
            Assert.AreEqual(request.Event上报地理位置.Longitude, 113.352425);
            Assert.AreEqual(request.Event上报地理位置.Precision, 119.385040);

            var clickEvent = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[CLICK]]></Event>
<EventKey><![CDATA[EVENTKEY]]></EventKey>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, clickEvent);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.Event点击自定义菜单);
            Assert.AreEqual(request.ToUserName, "toUser");
            Assert.AreEqual(request.FromUserName, "FromUser");
            Assert.AreEqual(request.CreateTime, 123456789);
            Assert.AreEqual(request.Event点击自定义菜单.EventKey, "EVENTKEY");

            var linkEvent = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[VIEW]]></Event>
<EventKey><![CDATA[www.qq.com]]></EventKey>
<MenuId>123</MenuId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, linkEvent);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.Event点击菜单跳转链接);
            Assert.AreEqual(request.ToUserName, "toUser");
            Assert.AreEqual(request.FromUserName, "FromUser");
            Assert.AreEqual(request.CreateTime, 123456789);
            Assert.AreEqual(request.Event点击菜单跳转链接.Url, "www.qq.com");
            Assert.AreEqual(request.Event点击菜单跳转链接.MenuId, 123);

        }

        [TestMethod]
        public void WeChatRequestPushEventTest()
        {
            #region 模板消息发送结果
            var tempplatePush = @"<xml>
<ToUserName><![CDATA[gh_7f083739789a]]></ToUserName>
<FromUserName><![CDATA[oia2TjuEGTNoeX76QEjQNrcURxG8]]></FromUserName>
<CreateTime>1395658920</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[TEMPLATESENDJOBFINISH]]></Event>
<MsgID>200163836</MsgID>
<Status><![CDATA[success]]></Status>
</xml>";
            var request = WeChatRequest.Build(Config, PostModel, tempplatePush);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.Push模板消息发送结果);
            Assert.AreEqual(request.ToUserName, "gh_7f083739789a");
            Assert.AreEqual(request.FromUserName, "oia2TjuEGTNoeX76QEjQNrcURxG8");
            Assert.AreEqual(request.CreateTime, 1395658920);
            Assert.AreEqual(request.Push模板消息发送结果.Status, "success");
            Assert.IsTrue(request.Push模板消息发送结果.Success);

            tempplatePush = @"<xml>
<ToUserName><![CDATA[gh_7f083739789a]]></ToUserName>
<FromUserName><![CDATA[oia2TjuEGTNoeX76QEjQNrcURxG8]]></FromUserName>
<CreateTime>1395658984</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[TEMPLATESENDJOBFINISH]]></Event>
<MsgID>200163840</MsgID>
<Status><![CDATA[failed:userblock]]></Status>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, tempplatePush);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.Push模板消息发送结果);
            Assert.AreEqual(request.ToUserName, "gh_7f083739789a");
            Assert.AreEqual(request.FromUserName, "oia2TjuEGTNoeX76QEjQNrcURxG8");
            Assert.AreEqual(request.CreateTime, 1395658984);
            Assert.AreEqual(request.Push模板消息发送结果.Status, "failed:userblock");
            Assert.IsFalse(request.Push模板消息发送结果.Success);
            tempplatePush = @"<xml>
<ToUserName><![CDATA[gh_7f083739789a]]></ToUserName>
<FromUserName><![CDATA[oia2TjuEGTNoeX76QEjQNrcURxG8]]></FromUserName>
<CreateTime>1395658984</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[TEMPLATESENDJOBFINISH]]></Event>
<MsgID>200163840</MsgID>
<Status><![CDATA[failed:system failed]]></Status>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, tempplatePush);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.Push模板消息发送结果);
            Assert.AreEqual(request.ToUserName, "gh_7f083739789a");
            Assert.AreEqual(request.FromUserName, "oia2TjuEGTNoeX76QEjQNrcURxG8");
            Assert.AreEqual(request.CreateTime, 1395658984);
            Assert.AreEqual(request.Push模板消息发送结果.Status, "failed:system failed");
            Assert.IsFalse(request.Push模板消息发送结果.Success);
            #endregion

            #region 群发结果
            var massResultPush = @"<xml>
<ToUserName><![CDATA[gh_4d00ed8d6399]]></ToUserName>
<FromUserName><![CDATA[oV5CrjpxgaGXNHIQigzNlgLTnwic]]></FromUserName>
<CreateTime>1481013459</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[MASSSENDJOBFINISH]]></Event>
<MsgID>1000001625</MsgID>
<Status><![CDATA[err(30003)]]></Status>
<TotalCount>0</TotalCount>
<FilterCount>0</FilterCount>
<SentCount>0</SentCount>
<ErrorCount>0</ErrorCount>
<CopyrightCheckResult>
<Count>2</Count>
<ResultList>
<item>
<ArticleIdx>1</ArticleIdx>
<UserDeclareState>0</UserDeclareState>
<AuditState>2</AuditState>
<OriginalArticleUrl><![CDATA[Url_1]]></OriginalArticleUrl>
<OriginalArticleType>1</OriginalArticleType>
<CanReprint>1</CanReprint>
<NeedReplaceContent>1</NeedReplaceContent>
<NeedShowReprintSource>1</NeedShowReprintSource>
</item>
<item>
<ArticleIdx>2</ArticleIdx>
<UserDeclareState>0</UserDeclareState>
<AuditState>2</AuditState>
<OriginalArticleUrl><![CDATA[Url_2]]></OriginalArticleUrl>
<OriginalArticleType>1</OriginalArticleType>
<CanReprint>1</CanReprint>
<NeedReplaceContent>1</NeedReplaceContent>
<NeedShowReprintSource>1</NeedShowReprintSource>
</item>
</ResultList>
<CheckState>2</CheckState>
</CopyrightCheckResult>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, massResultPush);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.Push群发结果);
            Assert.AreEqual(request.ToUserName, "gh_4d00ed8d6399");
            Assert.AreEqual(request.FromUserName, "oV5CrjpxgaGXNHIQigzNlgLTnwic");
            Assert.AreEqual(request.CreateTime, 1481013459);
            Assert.AreEqual(request.Push群发结果.MsgID, 1000001625);
            Assert.AreEqual(request.Push群发结果.Status, "err(30003)");
            Assert.IsFalse(request.Push群发结果.Success);
            Assert.AreEqual(request.Push群发结果.ErrorReason, "原创校验被判定为转载文且用户选择了被判为转载就不群发");
            Assert.AreEqual(request.Push群发结果.TotalCount, 0);
            Assert.AreEqual(request.Push群发结果.FilterCount, 0);
            Assert.AreEqual(request.Push群发结果.SentCount, 0);
            Assert.AreEqual(request.Push群发结果.ErrorCount, 0);
            var checkResult = request.Push群发结果.CopyrightCheckResult;
            var item = checkResult.First();
            Assert.AreEqual(item.ArticleIdx, 1);
            Assert.AreEqual(item.UserDeclareState, 0);
            Assert.AreEqual(item.AuditState, 2);
            Assert.AreEqual(item.OriginalArticleUrl, "Url_1");
            Assert.AreEqual(item.OriginalArticleType, 1);
            Assert.AreEqual(item.CanReprint, true);
            Assert.AreEqual(item.NeedReplaceContent, true);
            Assert.AreEqual(item.NeedShowReprintSource, true);
            item = checkResult.Last();
            Assert.AreEqual(item.ArticleIdx, 2);
            Assert.AreEqual(item.UserDeclareState, 0);
            Assert.AreEqual(item.AuditState, 2);
            Assert.AreEqual(item.OriginalArticleUrl, "Url_2");
            Assert.AreEqual(item.OriginalArticleType, 1);
            Assert.AreEqual(item.CanReprint, true);
            Assert.AreEqual(item.NeedReplaceContent, true);
            Assert.AreEqual(item.NeedShowReprintSource, true);
            Assert.AreEqual(request.Push群发结果.CheckState, 2);
            #endregion

            #region 菜单相关推送
            var scancodePush = @"<xml>
<ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090502</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[scancode_push]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<ScanCodeInfo><ScanType><![CDATA[qrcode]]></ScanType>
<ScanResult><![CDATA[1]]></ScanResult>
</ScanCodeInfo>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, scancodePush);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.MenuPush扫码推事件);
            Assert.AreEqual(request.ToUserName, "gh_e136c6e50636");
            Assert.AreEqual(request.FromUserName, "oMgHVjngRipVsoxg6TuX3vz6glDg");
            Assert.AreEqual(request.CreateTime, 1408090502);
            Assert.AreEqual(request.MenuPush扫码推事件.EventKey, "6");
            Assert.AreEqual(request.MenuPush扫码推事件.ScanType, "qrcode");
            Assert.AreEqual(request.MenuPush扫码推事件.ScanResult, "1");
            scancodePush = @"<xml>
<ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090606</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[scancode_waitmsg]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<ScanCodeInfo><ScanType><![CDATA[qrcode]]></ScanType>
<ScanResult><![CDATA[2]]></ScanResult>
</ScanCodeInfo>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, scancodePush);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.MenuPush扫码推等待事件);
            Assert.AreEqual(request.ToUserName, "gh_e136c6e50636");
            Assert.AreEqual(request.FromUserName, "oMgHVjngRipVsoxg6TuX3vz6glDg");
            Assert.AreEqual(request.CreateTime, 1408090606);
            Assert.AreEqual(request.MenuPush扫码推等待事件.EventKey, "6");
            Assert.AreEqual(request.MenuPush扫码推等待事件.ScanType, "qrcode");
            Assert.AreEqual(request.MenuPush扫码推等待事件.ScanResult, "2");

            var imagePush = @"<xml>
<ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090651</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_sysphoto]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[1b5f7c23b5bf75682a53e7b6d163e185]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, imagePush);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.MenuPush系统拍照发图);
            Assert.AreEqual(request.ToUserName, "gh_e136c6e50636");
            Assert.AreEqual(request.FromUserName, "oMgHVjngRipVsoxg6TuX3vz6glDg");
            Assert.AreEqual(request.CreateTime, 1408090651);
            Assert.AreEqual(request.MenuPush系统拍照发图.EventKey, "6");
            Assert.AreEqual(request.MenuPush系统拍照发图.Count, 1);
            CollectionAssert.AreEqual(request.MenuPush系统拍照发图.PicMd5Sum, new string[] { "1b5f7c23b5bf75682a53e7b6d163e185" });
            imagePush = @"<xml>
<ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090816</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_photo_or_album]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[5a75aaca956d97be686719218f275c6b]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, imagePush);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.MenuPush拍照或者相册发图);
            Assert.AreEqual(request.ToUserName, "gh_e136c6e50636");
            Assert.AreEqual(request.FromUserName, "oMgHVjngRipVsoxg6TuX3vz6glDg");
            Assert.AreEqual(request.CreateTime, 1408090816);
            Assert.AreEqual(request.MenuPush系统拍照发图.EventKey, "6");
            Assert.AreEqual(request.MenuPush系统拍照发图.Count, 1);
            CollectionAssert.AreEqual(request.MenuPush系统拍照发图.PicMd5Sum, new string[] { "5a75aaca956d97be686719218f275c6b" });
            imagePush = @"<xml>
<ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090816</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_weixin]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[5a75aaca956d97be686719218f275c6b]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, imagePush);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.MenuPush微信相册发图);
            Assert.AreEqual(request.ToUserName, "gh_e136c6e50636");
            Assert.AreEqual(request.FromUserName, "oMgHVjngRipVsoxg6TuX3vz6glDg");
            Assert.AreEqual(request.CreateTime, 1408090816);
            Assert.AreEqual(request.MenuPush系统拍照发图.EventKey, "6");
            Assert.AreEqual(request.MenuPush系统拍照发图.Count, 1);
            CollectionAssert.AreEqual(request.MenuPush系统拍照发图.PicMd5Sum, new string[] { "5a75aaca956d97be686719218f275c6b" });

            var locationPush = @"<xml>
<ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408091189</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[location_select]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendLocationInfo><Location_X><![CDATA[23]]></Location_X>
<Location_Y><![CDATA[113]]></Location_Y>
<Scale><![CDATA[15]]></Scale>
<Label><![CDATA[广州市海珠区客村艺苑路 106号]]></Label>
<Poiname><![CDATA[]]></Poiname>
</SendLocationInfo>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, locationPush);
            Assert.AreEqual(request.MessageType, WeChatRequestMessageType.Event);
            Assert.AreEqual(request.EventType, WeChatRequestEventType.MenuPush位置选择);
            Assert.AreEqual(request.ToUserName, "gh_e136c6e50636");
            Assert.AreEqual(request.FromUserName, "oMgHVjngRipVsoxg6TuX3vz6glDg");
            Assert.AreEqual(request.CreateTime, 1408091189);
            Assert.AreEqual(request.MenuPush位置选择.EventKey, "6");
            Assert.AreEqual(request.MenuPush位置选择.Location_X, 23);
            Assert.AreEqual(request.MenuPush位置选择.Location_Y, 113);
            Assert.AreEqual(request.MenuPush位置选择.Scale, 15);
            Assert.AreEqual(request.MenuPush位置选择.Label, "广州市海珠区客村艺苑路 106号");
            Assert.AreEqual(request.MenuPush位置选择.Poiname, "");
            #endregion
        }

        [TestMethod]
        public void WeChatResponseTest()
        {
            var demoMsg = @"<xml>
<ToUserName><![CDATA[AppId]]></ToUserName>
<FromUserName><![CDATA[OpenId]]></FromUserName>
<CreateTime>12345678</CreateTime>
</xml>";
            var request = WeChatRequest.Build(Config, PostModel, demoMsg);
            Assert.AreEqual(WeChatResponse.ResponseText(request, "你好").Source, @"<xml>
<ToUserName><![CDATA[OpenId]]></ToUserName>
<FromUserName><![CDATA[AppId]]></FromUserName>
<CreateTime>12345678</CreateTime>
<MsgType><![CDATA[text]]></MsgType>
<Content><![CDATA[你好]]></Content>
</xml>".RemoveWrap());
            Assert.AreEqual(WeChatResponse.ResponseImage(request, "media_id").Source, @"<xml>
<ToUserName><![CDATA[OpenId]]></ToUserName>
<FromUserName><![CDATA[AppId]]></FromUserName>
<CreateTime>12345678</CreateTime>
<MsgType><![CDATA[image]]></MsgType>
<Image>
<MediaId><![CDATA[media_id]]></MediaId>
</Image>
</xml>".RemoveWrap());
            Assert.AreEqual(WeChatResponse.ResponseVoice(request, "media_id").Source, @"<xml>
<ToUserName><![CDATA[OpenId]]></ToUserName>
<FromUserName><![CDATA[AppId]]></FromUserName>
<CreateTime>12345678</CreateTime>
<MsgType><![CDATA[voice]]></MsgType>
<Voice>
<MediaId><![CDATA[media_id]]></MediaId>
</Voice>
</xml>".RemoveWrap());
            Assert.AreEqual(WeChatResponse.ResponseVideo(request, "media_id", "title", "description").Source, @"<xml>
<ToUserName><![CDATA[OpenId]]></ToUserName>
<FromUserName><![CDATA[AppId]]></FromUserName>
<CreateTime>12345678</CreateTime>
<MsgType><![CDATA[video]]></MsgType>
<Video>
<MediaId><![CDATA[media_id]]></MediaId>
<Title><![CDATA[title]]></Title>
<Description><![CDATA[description]]></Description>
</Video>
</xml>".RemoveWrap());
            Assert.AreEqual(WeChatResponse.ResponseMusic(request, "media_id", "MUSIC_Url", "TITLE", "DESCRIPTION", "HQ_MUSIC_Url").Source, @"<xml>
<ToUserName><![CDATA[OpenId]]></ToUserName>
<FromUserName><![CDATA[AppId]]></FromUserName>
<CreateTime>12345678</CreateTime>
<MsgType><![CDATA[music]]></MsgType>
<Music>
<Title><![CDATA[TITLE]]></Title>
<Description><![CDATA[DESCRIPTION]]></Description>
<MusicUrl><![CDATA[MUSIC_Url]]></MusicUrl>
<HQMusicUrl><![CDATA[HQ_MUSIC_Url]]></HQMusicUrl>
<ThumbMediaId><![CDATA[media_id]]></ThumbMediaId>
</Music>
</xml>".RemoveWrap());

            Assert.AreEqual(WeChatResponse.ResponseNews(request,
                new WeChatResponse.Article("title1", "description1", "picurl", "url"),
                new WeChatResponse.Article("title", "description", "picurl", "url")
                ).Source, @"<xml>
<ToUserName><![CDATA[OpenId]]></ToUserName>
<FromUserName><![CDATA[AppId]]></FromUserName>
<CreateTime>12345678</CreateTime>
<MsgType><![CDATA[news]]></MsgType>
<ArticleCount>2</ArticleCount>
<Articles>
<item>
<Title><![CDATA[title1]]></Title>
<Description><![CDATA[description1]]></Description>
<PicUrl><![CDATA[picurl]]></PicUrl>
<Url><![CDATA[url]]></Url>
</item>
<item>
<Title><![CDATA[title]]></Title>
<Description><![CDATA[description]]></Description>
<PicUrl><![CDATA[picurl]]></PicUrl>
<Url><![CDATA[url]]></Url>
</item>
</Articles>
</xml>".RemoveWrap());
            Assert.AreEqual(WeChatResponse.TransferToCustomerService(request).Source, @"<xml>
<ToUserName><![CDATA[OpenId]]></ToUserName>
<FromUserName><![CDATA[AppId]]></FromUserName>
<CreateTime>12345678</CreateTime>
<MsgType><![CDATA[transfer_customer_service]]></MsgType>
</xml>".RemoveWrap());
            Assert.AreEqual(WeChatResponse.TransferToCustomerService(request, "test1").Source, @"<xml>
<ToUserName><![CDATA[OpenId]]></ToUserName>
<FromUserName><![CDATA[AppId]]></FromUserName>
<CreateTime>12345678</CreateTime>
<MsgType><![CDATA[transfer_customer_service]]></MsgType>
<TransInfo><KfAccount><![CDATA[test1@test]]></KfAccount></TransInfo>
</xml>".RemoveWrap());
        }

        [TestMethod]
        public void WeChatExtensionTest()
        {
            var demoQuery = new Dictionary<string, string>()
            {
                {"nonce","1362870167"},
                {"openid","oOk2XjhrbcHP3tGgzDGAVHppo3Bs"},
                {"signature","7940891098b505c22f99b0e3708627ec715aa832"},
                {"timestamp","1496218735"},
                {"echostr","demoEchostr"}
            };
            Assert.IsNotNull(demoQuery.IsValidRequest(Config));
            Assert.AreEqual(demoQuery.ConfigVerify(Config), "demoEchostr");
            demoQuery = new Dictionary<string, string>();
            Assert.IsNull(demoQuery.IsValidRequest(Config));
            Assert.AreEqual(demoQuery.ConfigVerify(Config), "");
        }

        [TestMethod]
        public void WeChatHanddlerTest()
        {
            string demoMsg = @"<xml>
 <ToUserName><![CDATA[toUser]]></ToUserName>
 <FromUserName><![CDATA[fromUser]]></FromUserName>
 <CreateTime>1348831860</CreateTime>
 <MsgType><![CDATA[test]]></MsgType>
 <Content><![CDATA[this is a test]]></Content>
 <MsgId>1234567890123456</MsgId>
 </xml>";
            bool IsOkReponse(HandlerDemo demo, string text) => demo.Result.ToXDocument().SelectValue("Content").Equals(text);

            var request = WeChatRequest.Build(Config, PostModel, demoMsg);
            var demoHandler = new HandlerDemo(request);
            Assert.AreEqual(demoHandler.Result, "");//未支持消息，直接回复空字符串

            #region 微信消息
            demoMsg = @"<xml>
 <ToUserName><![CDATA[toUser]]></ToUserName>
 <FromUserName><![CDATA[fromUser]]></FromUserName>
 <CreateTime>1348831860</CreateTime>
 <MsgType><![CDATA[text]]></MsgType>
 <Content><![CDATA[this is a test]]></Content>
 <MsgId>1234567890123456</MsgId>
 </xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnMessageText"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1348831860</CreateTime>
<MsgType><![CDATA[image]]></MsgType>
<PicUrl><![CDATA[this is a url]]></PicUrl>
<MediaId><![CDATA[media_id]]></MediaId>
<MsgId>1234567890123456</MsgId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnMessageImage"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1357290913</CreateTime>
<MsgType><![CDATA[voice]]></MsgType>
<MediaId><![CDATA[media_id]]></MediaId>
<Format><![CDATA[Format]]></Format>
<Recognition><![CDATA[腾讯微信团队]]></Recognition>
<MsgId>1234567890123456</MsgId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnMessageVoice"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1357290913</CreateTime>
<MsgType><![CDATA[video]]></MsgType>
<MediaId><![CDATA[media_id]]></MediaId>
<ThumbMediaId><![CDATA[thumb_media_id]]></ThumbMediaId>
<MsgId>1234567890123456</MsgId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnMessageVideo"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1357290913</CreateTime>
<MsgType><![CDATA[shortvideo]]></MsgType>
<MediaId><![CDATA[media_id]]></MediaId>
<ThumbMediaId><![CDATA[thumb_media_id]]></ThumbMediaId>
<MsgId>1234567890123456</MsgId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnMessageShortVideo"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1351776360</CreateTime>
<MsgType><![CDATA[location]]></MsgType>
<Location_X>23.134521</Location_X>
<Location_Y>113.358803</Location_Y>
<Scale>20</Scale>
<Label><![CDATA[位置信息]]></Label>
<MsgId>1234567890123456</MsgId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnMessageLocaltion"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1351776360</CreateTime>
<MsgType><![CDATA[link]]></MsgType>
<Title><![CDATA[公众平台官网链接]]></Title>
<Description><![CDATA[公众平台官网链接]]></Description>
<Url><![CDATA[url]]></Url>
<MsgId>1234567890123456</MsgId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnMessageLink"));
            #endregion

            #region 事件消息
            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[subscribe]]></Event>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnEvent关注"));


            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[unsubscribe]]></Event>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.ThrowsException<NotImplementedException>(() => { string result = demoHandler.Result; }, "OnEvent取消关注");

            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[subscribe]]></Event>
<EventKey><![CDATA[qrscene_123123]]></EventKey>
<Ticket><![CDATA[TICKET]]></Ticket>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnEvent关注-扫描带参数二维码"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[SCAN]]></Event>
<EventKey><![CDATA[SCENE_VALUE]]></EventKey>
<Ticket><![CDATA[TICKET]]></Ticket>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnEvent扫描带参数二维码"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[LOCATION]]></Event>
<Latitude>23.137466</Latitude>
<Longitude>113.352425</Longitude>
<Precision>119.385040</Precision>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnEvent上报地理位置"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[CLICK]]></Event>
<EventKey><![CDATA[EVENTKEY]]></EventKey>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnEvent点击自定义菜单"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[VIEW]]></Event>
<EventKey><![CDATA[www.qq.com]]></EventKey>
<MenuId>123</MenuId>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.ThrowsException<NotImplementedException>(() => { string result = demoHandler.Result; }, "OnEvent点击菜单跳转链接");
            #endregion

            #region 微信服务相关
            demoMsg = @"<xml>
<ToUserName><![CDATA[gh_7f083739789a]]></ToUserName>
<FromUserName><![CDATA[oia2TjuEGTNoeX76QEjQNrcURxG8]]></FromUserName>
<CreateTime>1395658920</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[TEMPLATESENDJOBFINISH]]></Event>
<MsgID>200163836</MsgID>
<Status><![CDATA[success]]></Status>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.ThrowsException<NotImplementedException>(() => { string result = demoHandler.Result; }, "OnPush模板消息发送结果");

            demoMsg = @"<xml>
<ToUserName><![CDATA[gh_4d00ed8d6399]]></ToUserName>
<FromUserName><![CDATA[oV5CrjpxgaGXNHIQigzNlgLTnwic]]></FromUserName>
<CreateTime>1481013459</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[MASSSENDJOBFINISH]]></Event>
<MsgID>1000001625</MsgID>
<Status><![CDATA[err(30003)]]></Status>
<TotalCount>0</TotalCount>
<FilterCount>0</FilterCount>
<SentCount>0</SentCount>
<ErrorCount>0</ErrorCount>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.ThrowsException<NotImplementedException>(() => { string result = demoHandler.Result; }, "OnPush群发结果");
            #endregion

            #region 菜单相关推送
            demoMsg = @"<xml>
<ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090502</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[scancode_push]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<ScanCodeInfo><ScanType><![CDATA[qrcode]]></ScanType>
<ScanResult><![CDATA[1]]></ScanResult>
</ScanCodeInfo>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnPushMenu扫码推事件"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090606</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[scancode_waitmsg]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<ScanCodeInfo><ScanType><![CDATA[qrcode]]></ScanType>
<ScanResult><![CDATA[2]]></ScanResult>
</ScanCodeInfo>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnPushMenu扫码推等待事件"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090651</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_sysphoto]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[1b5f7c23b5bf75682a53e7b6d163e185]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnPushMenu系统拍照发图"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090816</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_photo_or_album]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[5a75aaca956d97be686719218f275c6b]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnPushMenu拍照或者相册发图"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090816</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_weixin]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[5a75aaca956d97be686719218f275c6b]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnPushMenu微信相册发图"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[gh_e136c6e50636]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408091189</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[location_select]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendLocationInfo><Location_X><![CDATA[23]]></Location_X>
<Location_Y><![CDATA[113]]></Location_Y>
<Scale><![CDATA[15]]></Scale>
<Label><![CDATA[广州市海珠区客村艺苑路 106号]]></Label>
<Poiname><![CDATA[]]></Poiname>
</SendLocationInfo>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "OnPushMenu位置选择"));
            #endregion


            string FillDemoContent(string text) => $@"<xml>
 <ToUserName><![CDATA[toUser]]></ToUserName>
 <FromUserName><![CDATA[fromUser]]></FromUserName>
 <CreateTime>1348831860</CreateTime>
 <MsgType><![CDATA[text]]></MsgType>
 <Content><![CDATA[{text}]]></Content>
 <MsgId>1234567890123456</MsgId>
 </xml>";
            demoMsg = FillDemoContent("123");
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "123"));

            demoMsg = FillDemoContent("34567");
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "123456"));

            demoMsg = FillDemoContent("aBc");
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "ABc"));

            demoMsg = FillDemoContent("BcDeFgh");
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "ABcdEf"));


            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[CLICK]]></Event>
<EventKey><![CDATA[test_menu]]></EventKey>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "Event点击自定义菜单Test"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[subscribe]]></Event>
<EventKey><![CDATA[qrscene_scanId]]></EventKey>
<Ticket><![CDATA[TICKET]]></Ticket>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "Event扫描带参数二维码scanId"));

            demoMsg = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[SCAN]]></Event>
<EventKey><![CDATA[scanId]]></EventKey>
<Ticket><![CDATA[TICKET]]></Ticket>
</xml>";
            request = WeChatRequest.Build(Config, PostModel, demoMsg);
            demoHandler = new HandlerDemo(request);
            Assert.IsTrue(IsOkReponse(demoHandler, "Event扫描带参数二维码scanId"));

        }
        public class HandlerDemo : MessageHandler
        {
            public HandlerDemo(WeChatRequest request) : base(request)
            {
                AddTextKeyWordHandler(new HandlerRule(HandlerContrast.Equal, "123"), (req, msg) =>
                 {
                     return WeChatResponse.ResponseText(req, "123");
                 });
                AddTextKeyWordHandler(new HandlerRule(HandlerContrast.Contain, "456"), (req, msg) =>
                {
                    return WeChatResponse.ResponseText(req, "123456");
                });
                AddTextKeyWordHandler(new HandlerRule(HandlerContrast.Superficial, "abc"), (req, msg) =>
                {
                    return WeChatResponse.ResponseText(req, "ABc");
                });
                AddTextKeyWordHandler(new HandlerRule(HandlerContrast.ContainSuperficial, "def"), (req, msg) =>
                {
                    return WeChatResponse.ResponseText(req, "ABcdEf");
                });

                AddEventMenuClickHandler("test_menu", (req, msg) =>
                {
                    return WeChatResponse.ResponseText(req, "Event点击自定义菜单Test");
                });
                AddEventScanIdHandler("scanId", (req, msg) =>
                {
                    return WeChatResponse.ResponseText(req, "Event扫描带参数二维码scanId");
                });
            }

            public override WeChatResponse DefaultResponseMessage(WeChatRequest request)
            {
                return null; //未定义处理的默认方法，不处理则直接返回Null以回复空字符串给微信服务器
            }
            public override WeChatResponse OnMessageText(WeChatRequest request, WeChatRequest.WeChatMessageText text)
            {
                return WeChatResponse.ResponseText(request, "OnMessageText");
            }
            public override WeChatResponse OnMessageImage(WeChatRequest request, WeChatRequest.WeChatMessageImage image)
            {
                return WeChatResponse.ResponseText(request, "OnMessageImage");
            }
            public override WeChatResponse OnMessageVoice(WeChatRequest request, WeChatRequest.WeChatMessageVoice voice)
            {
                return WeChatResponse.ResponseText(request, "OnMessageVoice");
            }
            public override WeChatResponse OnMessageLocaltion(WeChatRequest request, WeChatRequest.WeChatMessageLocation location)
            {
                return WeChatResponse.ResponseText(request, "OnMessageLocaltion");
            }
            public override WeChatResponse OnMessageVideo(WeChatRequest request, WeChatRequest.WeChatMessageVideo video)
            {
                return WeChatResponse.ResponseText(request, "OnMessageVideo");
            }
            public override WeChatResponse OnMessageShortVideo(WeChatRequest request, WeChatRequest.WeChatMessageVideo shortVideo)
            {
                return WeChatResponse.ResponseText(request, "OnMessageShortVideo");
            }
            public override WeChatResponse OnMessageLink(WeChatRequest request, WeChatRequest.WeChatMessageLink link)
            {
                return WeChatResponse.ResponseText(request, "OnMessageLink");
            }

            public override WeChatResponse OnEvent关注(WeChatRequest request)
            {
                return WeChatResponse.ResponseText(request, "OnEvent关注");
            }
            public override WeChatResponse OnEvent关注(WeChatRequest request, WeChatRequest.WeChatEvent扫描带参数二维码 msg)
            {
                return WeChatResponse.ResponseText(request, "OnEvent关注-扫描带参数二维码");
            }
            public override void OnEvent取消关注(WeChatRequest request)
            {
                throw new NotImplementedException("OnEvent取消关注");
            }
            public override WeChatResponse OnEvent扫描带参数二维码(WeChatRequest request, WeChatRequest.WeChatEvent扫描带参数二维码 msg)
            {
                return WeChatResponse.ResponseText(request, "OnEvent扫描带参数二维码");
            }
            public override WeChatResponse OnEvent上报地理位置(WeChatRequest request, WeChatRequest.WeChatEvent上报地理位置 msg)
            {
                return WeChatResponse.ResponseText(request, "OnEvent上报地理位置");
            }
            public override WeChatResponse OnEvent点击自定义菜单(WeChatRequest request, WeChatRequest.WeChatEvent点击自定义菜单 msg)
            {
                return WeChatResponse.ResponseText(request, "OnEvent点击自定义菜单");
            }
            public override void OnEvent点击菜单跳转链接(WeChatRequest request, WeChatRequest.WeChatEvent点击菜单跳转链接 msg)
            {
                throw new NotImplementedException("OnEvent点击菜单跳转链接");
            }

            public override WeChatResponse OnPushMenu位置选择(WeChatRequest request, WeChatRequest.WeChatMenuPush位置选择 msg)
            {
                return WeChatResponse.ResponseText(request, "OnPushMenu位置选择");
            }
            public override WeChatResponse OnPushMenu扫码推事件(WeChatRequest request, WeChatRequest.WeChatMenuPush扫码事件 msg)
            {
                return WeChatResponse.ResponseText(request, "OnPushMenu扫码推事件");
            }
            public override WeChatResponse OnPushMenu扫码推等待事件(WeChatRequest request, WeChatRequest.WeChatMenuPush扫码事件 msg)
            {
                return WeChatResponse.ResponseText(request, "OnPushMenu扫码推等待事件");
            }
            public override WeChatResponse OnPushMenu系统拍照发图(WeChatRequest request, WeChatRequest.WeChatMenuPush拍照发图 msg)
            {
                return WeChatResponse.ResponseText(request, "OnPushMenu系统拍照发图");
            }
            public override WeChatResponse OnPushMenu微信相册发图(WeChatRequest request, WeChatRequest.WeChatMenuPush拍照发图 msg)
            {
                return WeChatResponse.ResponseText(request, "OnPushMenu微信相册发图");
            }
            public override WeChatResponse OnPushMenu拍照或者相册发图(WeChatRequest request, WeChatRequest.WeChatMenuPush拍照发图 msg)
            {
                return WeChatResponse.ResponseText(request, "OnPushMenu拍照或者相册发图");
            }

            public override void OnPush模板消息发送结果(WeChatRequest request, WeChatRequest.WeChatPush模板消息发送结果 msg)
            {
                throw new NotImplementedException("OnPush模板消息发送结果");
            }
            public override void OnPush群发结果(WeChatRequest request, WeChatRequest.WeChatPush群发结果 msg)
            {
                throw new NotImplementedException("OnPush群发结果");
            }
        }

    }
}
