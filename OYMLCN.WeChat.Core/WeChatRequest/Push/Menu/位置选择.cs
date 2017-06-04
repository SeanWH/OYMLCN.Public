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
        /// 位置选择
        /// </summary>
        public WeChatMenuPush位置选择 MenuPush位置选择 => new WeChatMenuPush位置选择(this);
        /// <summary>
        /// 位置选择
        /// </summary>
        public class WeChatMenuPush位置选择 : WeChatMessageBase
        {
            /// <summary>
            /// 扫码推事件
            /// </summary>
            /// <param name="request"></param>
            public WeChatMenuPush位置选择(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 事件KEY值，由开发者在创建菜单时设定
            /// </summary>
            public string EventKey => Request.Document.SelectValue("EventKey");
            /// <summary>
            /// X坐标信息
            /// </summary>
            public double Location_X => Request.Document.Elements().Elements("SendLocationInfo").SelectValue("Location_X").ConvertToDouble();
            /// <summary>
            /// Y坐标信息
            /// </summary>
            public double Location_Y => Request.Document.Elements().Elements("SendLocationInfo").SelectValue("Location_Y").ConvertToDouble();
            /// <summary>
            /// 精度，可理解为精度或者比例尺、越精细的话 scale越高
            /// </summary>
            public double Scale => Request.Document.Elements().Elements("SendLocationInfo").SelectValue("Scale").ConvertToDouble();
            /// <summary>
            /// 地理位置的字符串信息
            /// </summary>
            public string Label => Request.Document.Elements().Elements("SendLocationInfo").SelectValue("Label");
            /// <summary>
            /// 朋友圈POI的名字，可能为空
            /// </summary>
            public string Poiname => Request.Document.Elements().Elements("SendLocationInfo").SelectValue("Poiname");

        }
    }
}
