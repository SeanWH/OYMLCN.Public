namespace OYMLCN.WeChat
{
    /// <summary>
    /// 微信请求
    /// </summary>
    public partial class WeChatRequest
    {
        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        public WeChatEvent上报地理位置 Event上报地理位置 => new WeChatEvent上报地理位置(this);

        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        public class WeChatEvent上报地理位置 : WeChatMessageBase
        {
            /// <summary>
            /// 上报地理位置事件
            /// </summary>
            /// <param name="request"></param>
            public WeChatEvent上报地理位置(WeChatRequest request) : base(request) { }

            /// <summary>
            /// 地理位置纬度 
            /// </summary>
            public double Latitude=>Request.Document.SelectValue("Latitude").ConvertToDouble();
            /// <summary>
            /// 地理位置经度 
            /// </summary>
            public double Longitude => Request.Document.SelectValue("Longitude").ConvertToDouble();
            /// <summary>
            /// 地理位置精度 
            /// </summary>
            public double Precision => Request.Document.SelectValue("Precision").ConvertToDouble();
        }
    }
}
