namespace OYMLCN.WeChat.Model
{
    /// <summary>
    /// 标签管理
    /// </summary>
    public class TagResult : JsonResult
    {
        /// <summary>
        /// 标签
        /// </summary>
        public class Tag
        {
            /// <summary>
            /// 标签id，由微信分配
            /// </summary>
            public int id;
            /// <summary>
            /// 标签名
            /// </summary>
            public string name;
            /// <summary>
            /// 用户数量
            /// </summary>
            public int count;
        }
        /// <summary>
        /// 标签信息
        /// </summary>
        public Tag tag { get; set; }
        /// <summary>
        /// 标签列表
        /// </summary>
        public Tag[] tags { get; set; }
    }

    /// <summary>
    /// 粉丝列表
    /// </summary>
    public class TagUsers : JsonResult
    {
        /// <summary>
        /// 粉丝列表
        /// </summary>
        public class User
        {
            /// <summary>
            /// OpenId列表
            /// </summary>
            public string[] openid { get; set; }
        }
        /// <summary>
        /// 本次获取的粉丝数量
        /// </summary>
        public int count;
        /// <summary>
        /// 粉丝列表
        /// </summary>
        public User data;
        /// <summary>
        /// 拉取列表最后一个用户的openid
        /// </summary>
        public string next_openid;
    }
}
