using System;
using System.Collections.Generic;
using System.Text;

namespace OYMLCN.WeChat
{
    public partial class Api
    {
        public class Config
        {
            public Config(string accountName, string appId, string appSecret)
            {
                AppId = appId;
                AppSecret = appSecret;
                AccountName = accountName;
            }

            public string AccountName { get; private set; }
            public string AppId { get; private set; }
            public string AppSecret { get; private set; }
        }
    }
}
