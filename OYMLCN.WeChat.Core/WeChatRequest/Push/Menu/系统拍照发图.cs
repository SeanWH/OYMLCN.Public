using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        /// <summary>
        /// 系统拍照发图
        /// </summary>
        public WeChatMenuPush拍照发图 MenuPush系统拍照发图 => new WeChatMenuPush拍照发图(this);
        /// <summary>
        /// 拍照或者相册发图
        /// </summary>
        public WeChatMenuPush拍照发图 MenuPush拍照或者相册发图 => new WeChatMenuPush拍照发图(this);
        /// <summary>
        /// 微信相册发图
        /// </summary>
        public WeChatMenuPush拍照发图 MenuPush微信相册发图 => new WeChatMenuPush拍照发图(this);


        /// <summary>
        /// 系统拍照发图
        /// </summary>
        public class WeChatMenuPush拍照发图 : WeChatMessageBase
        {
            /// <summary>
            /// 系统拍照发图
            /// </summary>
            /// <param name="request"></param>
            public WeChatMenuPush拍照发图(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 事件KEY值，由开发者在创建菜单时设定
            /// </summary>
            public string EventKey => Request.Document.SelectValue("EventKey");

            /// <summary>
            /// 发送的图片数量
            /// </summary>
            public byte Count => Request.Document.Elements().Elements("SendPicsInfo").SelectValue("Count").ConvertToByte();
            /// <summary>
            /// 图片的MD5值，开发者若需要，可用于验证接收到图片
            /// </summary>
            public string[] PicMd5Sum => Request.Document.Elements().Elements("SendPicsInfo").Elements("PicList").Elements().Select(d=>d.Value).ToArray();
        }
    }
}
