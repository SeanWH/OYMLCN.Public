using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OYMLCN.WeChat;
using System.Text;

namespace OYMLCN.Open.WebTest.NetCore.Controllers
{
    [Produces("application/json")]
    [Route("WeChat")]
    public class WeChatController : Controller
    {
        OYMLCN.WeChat.Config Config = new WeChat.Config("gh_69438e79ea75", "wx95dfa97f9fca2358", "4e053f53f278347179416898e3d7883a", "wxapi");

        public ContentResult Post()
        {
            ////Config = new Config("wx9960b2eab9e87d17", "46e57cb6421846723a2dbebc387ac290", "wxapi", "ibuHr5GSvEvHmGJbQF2S9OY4miQnqT7Mvf0SUrD3vyp", "oymlcn");
            ////return Content(this.ConfigVerify(Config));
            //StringBuilder str = new StringBuilder();
            //var token = Config.GetAccessToken();
            ////str.AppendLine(token.ToJsonString() + "<br/>");
            ////str.AppendLine(token.GetIpAdress().ToJsonString() + "<br/>");

            //if (!this.IsValidRequest(Config))
            //    return Content("");
            //var xdoc = this.GetWeChatRequsetXmlDocument(Config);
            //if (xdoc.IsRepeat())
            //    return Content("");
            ////return Content(xdoc.ToRequestMessageText().ResponseText("WeChatTest").Result);
            //switch (xdoc.GetMsgType())
            //{
            //    case RequestMsgType.Text:
            //        var text = xdoc.ToRequestMessageText();
            //        if (text.Content == "news")
            //            token.CustomerServiceSendNews(text.FromUserName, "nihao", new WeChat.Model.WeChatResponseNewItem("test", "dd", null, "http://www.qq.com"));
            //        token.CustomerServiceSendText(text.FromUserName, text.Content);
            //        break;
            //    case RequestMsgType.Image:
            //        var image = xdoc.ToRequestMessageImage();
            //        token.CustomerServiceSendImage(image.FromUserName, image.MediaId);
            //        break;
            //    case RequestMsgType.Voice:
            //        var voice = xdoc.ToRequestMessageVoice();
            //        token.CustomerServiceSendVoice(voice.FromUserName, voice.MediaId);
            //        break;
            //}


            //OYMLCN.WeChat.Config config = new OYMLCN.WeChat.Config("微信账号名", "AppId", "AppSecret", "Token", "AESKey");
            //OYMLCN.WeChat.PostModel postModel = this.Request.GetQuery().IsValidRequest(config);
            //string body = this.Request.GetBody().ReadToEnd();
            //OYMLCN.WeChat.WeChatRequest request = OYMLCN.WeChat.WeChatRequest.Build(config, postModel, body);
            //OYMLCN.WeChat.WeChatResponse response = null;
            //if (request.MessageType == OYMLCN.WeChat.WeChatRequestMessageType.Text)
            //    response = OYMLCN.WeChat.WeChatResponse.ResponseText(request, "我是测试");
            //else if (request.MessageType == OYMLCN.WeChat.WeChatRequestMessageType.Event)
            //    if (request.EventType == OYMLCN.WeChat.WeChatRequestEventType.Event关注)
            //        response = OYMLCN.WeChat.WeChatResponse.ResponseText(request, "你来晚了");
            //if (response != null)
            //    return Content(response.Result);
            //return Content("");


            var postModel = this.Request.GetQuery().IsValidRequest(Config);
            if (postModel == null)
                return Content("");
            return Content(new HandlerDemo(WeChatRequest.Build(Config, postModel, this.Request.GetBody().ReadToEnd())).Result);
        }
        public class DemoHandler : OYMLCN.WeChat.MessageHandler
        {
            public DemoHandler(OYMLCN.WeChat.WeChatRequest request) : base(request) { }

            public override OYMLCN.WeChat.WeChatResponse DefaultResponseMessage(OYMLCN.WeChat.WeChatRequest request)
            {
                return OYMLCN.WeChat.WeChatResponse.ResponseText(request, "Success");
            }
        }

        public class HandlerDemo : MessageHandler
        {
            public HandlerDemo(WeChatRequest request) : base(request)
            {
                AddTextKeyWordHandler(new HandlerRule(HandlerContrast.Equal, "123"), (req, msg) =>
                {
                    return WeChatResponse.ResponseText(req, "123" + msg.ToJsonString());
                });
                AddTextKeyWordHandler(new HandlerRule(HandlerContrast.Contain, "456"), (req, msg) =>
                {
                    return WeChatResponse.ResponseText(req, "123456" + msg.ToJsonString());
                });
                AddTextKeyWordHandler(new HandlerRule(HandlerContrast.Superficial, "abc"), (req, msg) =>
                {
                    return WeChatResponse.ResponseText(req, "ABc" + msg.ToJsonString());
                });
                AddTextKeyWordHandler(new HandlerRule(HandlerContrast.ContainSuperficial, "def"), (req, msg) =>
                {
                    return WeChatResponse.ResponseText(req, "ABcdEf" + msg.ToJsonString());
                });

                AddEventMenuClickHandler("test_menu", (req, msg) =>
                {
                    return WeChatResponse.ResponseText(req, "Event点击自定义菜单Test" + msg.ToJsonString());
                });
                AddEventScanIdHandler("scanId", (req, msg) =>
                {
                    return WeChatResponse.ResponseText(req, "Event扫描带参数二维码scanId" + msg.ToJsonString());
                });
            }

            public override WeChatResponse DefaultResponseMessage(WeChatRequest request)
            {
                return null; //未定义处理的默认方法，不处理则直接返回Null以回复空字符串给微信服务器
            }
            public override WeChatResponse OnMessageText(WeChatRequest request, WeChatRequest.WeChatMessageText text)
            {
                return WeChatResponse.ResponseText(request, "OnMessageText" + text.ToJsonString());
            }
            public override WeChatResponse OnMessageImage(WeChatRequest request, WeChatRequest.WeChatMessageImage image)
            {
                return WeChatResponse.ResponseText(request, "OnMessageImage" + image.ToJsonString());
            }
            public override WeChatResponse OnMessageVoice(WeChatRequest request, WeChatRequest.WeChatMessageVoice voice)
            {
                return WeChatResponse.ResponseText(request, "OnMessageVoice" + voice.ToJsonString());
            }
            public override WeChatResponse OnMessageLocaltion(WeChatRequest request, WeChatRequest.WeChatMessageLocation location)
            {
                return WeChatResponse.ResponseText(request, "OnMessageLocaltion" + location.ToJsonString());
            }
            public override WeChatResponse OnMessageVideo(WeChatRequest request, WeChatRequest.WeChatMessageVideo video)
            {
                return WeChatResponse.ResponseText(request, "OnMessageVideo" + video.ToJsonString());
            }
            public override WeChatResponse OnMessageShortVideo(WeChatRequest request, WeChatRequest.WeChatMessageVideo shortVideo)
            {
                return WeChatResponse.ResponseText(request, "OnMessageShortVideo" + shortVideo.ToJsonString());
            }
            public override WeChatResponse OnMessageLink(WeChatRequest request, WeChatRequest.WeChatMessageLink link)
            {
                return WeChatResponse.ResponseText(request, "OnMessageLink" + link.ToJsonString());
            }

            public override WeChatResponse OnEvent关注(WeChatRequest request)
            {
                return WeChatResponse.ResponseText(request, "OnEvent关注");
            }
            public override WeChatResponse OnEvent关注(WeChatRequest request, WeChatRequest.WeChatEvent扫描带参数二维码 msg)
            {
                return WeChatResponse.ResponseText(request, "OnEvent关注-扫描带参数二维码" + msg.ToJsonString());
            }
            public override void OnEvent取消关注(WeChatRequest request)
            {
                throw new NotImplementedException("OnEvent取消关注");
            }
            public override WeChatResponse OnEvent扫描带参数二维码(WeChatRequest request, WeChatRequest.WeChatEvent扫描带参数二维码 msg)
            {
                return WeChatResponse.ResponseText(request, "OnEvent扫描带参数二维码" + msg.ToJsonString());
            }
            public override WeChatResponse OnEvent上报地理位置(WeChatRequest request, WeChatRequest.WeChatEvent上报地理位置 msg)
            {
                return WeChatResponse.ResponseText(request, "OnEvent上报地理位置" + msg.ToJsonString());
            }
            public override WeChatResponse OnEvent点击自定义菜单(WeChatRequest request, WeChatRequest.WeChatEvent点击自定义菜单 msg)
            {
                return WeChatResponse.ResponseText(request, "OnEvent点击自定义菜单" + msg.ToJsonString());
            }
            public override void OnEvent点击菜单跳转链接(WeChatRequest request, WeChatRequest.WeChatEvent点击菜单跳转链接 msg)
            {
                throw new NotImplementedException("OnEvent点击菜单跳转链接" + msg.ToJsonString());
            }

            public override WeChatResponse OnPushMenu位置选择(WeChatRequest request, WeChatRequest.WeChatMenuPush位置选择 msg)
            {
                return WeChatResponse.ResponseText(request, "OnPushMenu位置选择" + msg.ToJsonString());
            }
            public override WeChatResponse OnPushMenu扫码推事件(WeChatRequest request, WeChatRequest.WeChatMenuPush扫码事件 msg)
            {
                return WeChatResponse.ResponseText(request, "OnPushMenu扫码推事件" + msg.ToJsonString());
            }
            public override WeChatResponse OnPushMenu扫码推等待事件(WeChatRequest request, WeChatRequest.WeChatMenuPush扫码事件 msg)
            {
                return WeChatResponse.ResponseText(request, "OnPushMenu扫码推等待事件" + msg.ToJsonString());
            }
            public override WeChatResponse OnPushMenu系统拍照发图(WeChatRequest request, WeChatRequest.WeChatMenuPush拍照发图 msg)
            {
                return WeChatResponse.ResponseText(request, "OnPushMenu系统拍照发图" + msg.ToJsonString());
            }
            public override WeChatResponse OnPushMenu微信相册发图(WeChatRequest request, WeChatRequest.WeChatMenuPush拍照发图 msg)
            {
                return WeChatResponse.ResponseText(request, "OnPushMenu微信相册发图" + msg.ToJsonString());
            }
            public override WeChatResponse OnPushMenu拍照或者相册发图(WeChatRequest request, WeChatRequest.WeChatMenuPush拍照发图 msg)
            {
                return WeChatResponse.ResponseText(request, "OnPushMenu拍照或者相册发图" + msg.ToJsonString());
            }

            public override void OnPush模板消息发送结果(WeChatRequest request, WeChatRequest.WeChatPush模板消息发送结果 msg)
            {
                throw new NotImplementedException("OnPush模板消息发送结果" + msg.ToJsonString());
            }
            public override void OnPush群发结果(WeChatRequest request, WeChatRequest.WeChatPush群发结果 msg)
            {
                throw new NotImplementedException("OnPush群发结果" + msg.ToJsonString());
            }
        }

    }
}