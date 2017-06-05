namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        /// <summary>
        /// 地理位置消息
        /// </summary>
        public WeChatMessageLocation MessageLocation => new WeChatMessageLocation(this);

        /// <summary>
        /// 地理位置消息
        /// </summary>
        public class WeChatMessageLocation : WeChatMessageBase
        {
            /// <summary>
            /// 地理位置消息
            /// </summary>
            /// <param name="request"></param>
            public WeChatMessageLocation(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 地理位置维度
            /// </summary>
            public double Location_X => Request.Document.SelectValue("Location_X").ConvertToDouble();
            /// <summary>
            /// 地理位置经度
            /// </summary>
            public double Location_Y => Request.Document.SelectValue("Location_Y").ConvertToDouble();
            /// <summary>
            /// 地图缩放大小 
            /// </summary>
            public byte Scale => Request.Document.SelectValue("Scale").ConvertToByte();
            /// <summary>
            /// 地理位置信息
            /// </summary>
            public string Label => Request.Document.SelectValue("Label");
        }
    }
}
