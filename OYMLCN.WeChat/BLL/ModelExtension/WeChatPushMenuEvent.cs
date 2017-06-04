using OYMLCN.WeChat.Model;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OYMLCN.WeChat
{
    /// <summary>
    /// Extension
    /// </summary>
    public static partial class Extension
    {
        private static T FillPushMenuScan<T>(this T item, WeChatRequsetXmlDocument xdoc) where T : WeChatPushMenu扫码推事件
        {
            var dom = xdoc.Document.Elements();
            var result = new WeChatPushMenu扫码推事件();
            item.EventKey = dom.SelectValue("EventKey");
            var info = dom.Elements("ScanCodeInfo");
            item.ScanType = info.SelectValue("ScanType");
            item.ScanResult = info.SelectValue("ScanResult");
            return item.FillByDom(xdoc);
        }

        /// <summary>
        /// 反序列化Xml数据为微信扫码推事件推送结果
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatPushMenu扫码推事件 ToPushMenu扫码推事件(this WeChatRequsetXmlDocument xdoc) => new WeChatPushMenu扫码推事件().FillPushMenuScan(xdoc);
        /// <summary>
        /// 反序列化Xml数据为微信扫码推等待事件推送结果
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatPushMenu扫码推等待事件 ToPushMenu扫码推等待事件(this WeChatRequsetXmlDocument xdoc) => new WeChatPushMenu扫码推等待事件().FillPushMenuScan(xdoc);

        private static T FillPushMenuPic<T>(this T item, WeChatRequsetXmlDocument xdoc) where T : WeChatPushMenu系统拍照发图
        {
            var dom = xdoc.Document.Elements();
            item.EventKey = dom.SelectValue("EventKey");
            var info = dom.Elements("SendPicsInfo");
            item.Count = info.SelectValue("Count").ConvertToInt();
            var plitem = info.Elements("PicList").Elements();
            var list = new List<string>();
            foreach (var md5 in plitem)
                list.Add(md5.Value);
            item.PicMd5Sum = list.ToArray();
            return item.FillByDom(xdoc);
        }
        /// <summary>
        /// 反序列化Xml数据为系统拍照发图事件推送结果
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatPushMenu系统拍照发图 ToPushMenu系统拍照发图(this WeChatRequsetXmlDocument xdoc) => new WeChatPushMenu系统拍照发图().FillPushMenuPic(xdoc);
        /// <summary>
        /// 反序列化Xml数据为拍照或者相册发图事件推送结果
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatPushMenu拍照或者相册发图 ToPushMenu拍照或者相册发图(this WeChatRequsetXmlDocument xdoc) => new WeChatPushMenu拍照或者相册发图().FillPushMenuPic(xdoc);
        /// <summary>
        /// 反序列化Xml数据为微信相册发图事件推送结果
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatPushMenu微信相册发图 ToPushMenu微信相册发图(this WeChatRequsetXmlDocument xdoc) => new WeChatPushMenu微信相册发图().FillPushMenuPic(xdoc);
        /// <summary>
        /// 反序列化Xml数据为微信位置选择事件推送结果
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static WeChatPushMenu位置选择 ToPushMenu位置选择(this WeChatRequsetXmlDocument xdoc)
        {
            var dom = xdoc.Document.Elements();
            var result = new WeChatPushMenu位置选择();
            result.EventKey = dom.SelectValue("EventKey");
            var info = dom.Elements("SendLocationInfo");
            result.Location_X = info.SelectValue("Location_X");
            result.Location_Y = info.SelectValue("Location_Y");
            result.Scale = info.SelectValue("Scale");
            result.Label = info.SelectValue("Label");
            result.Poiname = info.SelectValue("Poiname");
            return result.FillByDom(xdoc);
        }
    }
}
