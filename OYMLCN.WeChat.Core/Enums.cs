#pragma warning disable
namespace OYMLCN.WeChat
{
    /// <summary>
    /// 接收消息类型
    /// </summary>
    public enum WeChatRequestMessageType
    {
        Text,
        Location,
        Image,
        Voice,
        Video,
        Link,
        ShortVideo,
        Event,
        Unknow = -1
    }
    /// <summary>
    /// 当RequestMsgType类型为Event时，Event属性的类型
    /// </summary>
    public enum WeChatRequestEventType
    {
        Event未知 = 0,
        Event关注,
        Event取消关注,
        Event扫描带参数二维码,
        Event上报地理位置,
        Event点击自定义菜单,
        Event点击菜单跳转链接,

        Push模板消息发送结果,
        Push群发结果,

        MenuPush扫码推事件,
        MenuPush扫码推等待事件,
        MenuPush系统拍照发图,
        MenuPush拍照或者相册发图,
        MenuPush微信相册发图,
        MenuPush位置选择
    }

}
