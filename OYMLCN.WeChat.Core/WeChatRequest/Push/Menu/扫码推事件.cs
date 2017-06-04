using System.Collections.Generic;
using System.Xml.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        /// <summary>
        /// 扫码推事件
        /// </summary>
        public WeChatMenuPush扫码事件 MenuPush扫码推事件 => new WeChatMenuPush扫码事件(this);
        /// <summary>
        /// Push扫码推等待事件
        /// </summary>
        public WeChatMenuPush扫码事件 MenuPush扫码推等待事件 => new WeChatMenuPush扫码事件(this);

        /// <summary>
        /// 扫码推事件
        /// </summary>
        public class WeChatMenuPush扫码事件 : WeChatMessageBase
        {
            /// <summary>
            /// 扫码推事件
            /// </summary>
            /// <param name="request"></param>
            public WeChatMenuPush扫码事件(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 事件KEY值，由开发者在创建菜单时设定
            /// </summary>
            public string EventKey => Request.Document.SelectValue("EventKey");
            /// <summary>
            /// 扫描类型，一般是qrcode
            /// </summary>
            public string ScanType => Request.Document.Elements().Elements("ScanCodeInfo").SelectValue("ScanType");
            /// <summary>
            /// 扫描结果，即二维码对应的字符串信息
            /// </summary>
            public string ScanResult => Request.Document.Elements().Elements("ScanCodeInfo").SelectValue("ScanResult");

        }
    }
}
