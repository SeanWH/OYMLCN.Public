微信公众平台接口
（已通过调用测试）表明通过调用接口的方式与微信公众平台接口进行有效验证。
（已通过调试测试）表明通过调试的方式与微信公众平台Api联调进行有效验证。
（已通过单元测试）表明通过单元测试的方法与微信公众平台说明文档示例进行验证。

 V1.0.0
 项目不再依赖所有MVC及WebApi，允许有选择地使用相应的框架扩展。
 不再支持NetCore1.0(NetStandard1.6)，已升级NetCore2.0(NetStandard2.0)支持。
    MVC对应扩展包：OYMLCN.WeChat.Mvc
 WebApi对应扩展包：OYMLCN.WeChat.AspNetWebApi

 V0.3.X
 对项目整体进行调整，部分命名变更，方法扩展集中到Extension，枚举移到Enum命名空间。

 微信消息统一请求处理入口MessageHandler
 微信推送信息转由委托事件处理（为优化正式项目代码所设置）
 MessageHandler.AddTextKeyWordHandler 添加文本消息关键词处理
 MessageHandler.AddEventMenuClickHandler 添加菜单点击事件值处理
 MessageHandler.AddEventScanIdHandler 添加二维码扫描/关注ScanId处理

开始开发
    为了更方便的开发，可以引入如下命名空间：
	  using OYMLCN; // 可选，包含常用的方法扩展
	  using OYMLCN.WeChat;
	  using OYMLCN.WeChat.Enum;
	  using OYMLCN.WeChat.Model;
  接入指南  （已通过调试测试）
    第一步：填写服务器配置
            new OYMLCN.WeChat.Config() 接口配置信息 后文cfg均为该实例的参数引用。
    第二步：验证消息的确来自微信服务器
      方法1：OYMLCN.WeChat.Extension.CreateSignature(timestamp,nonce,token) 创建签名后对比signature，一致后返回echostr。
      方法2：调用ConfigVerify(cfg)扩展方法，将方法返回的值（echostr）返回。
        调用方式：（分别扩展了HttpRequest、Controller、HttpRequestBase、ApiController、HttpRequestMessage类，后三仅.NET461）
          1、Controller及ApiController可调用 this.ConfigVerify(cfg)或this.Request.ConfigVerify(cfg)。
          2、其他地方调用则获取当前请求的Request后调用扩展方法。 
      方法3：调用IsValidRequest(cfg)扩展方法，对签名进行验证，若正确则返回true，再然后返回echostr。
  获取AccessToken  （已通过调试测试）
    方法1、调用Config接口配置信息类扩展方法 cfg.GetAccessToken(oldToken = null)
           oldToken = null 表示该参数可不传入，在单例服务时自管理，
      默认会使用最近一次获取的未到达过期提前条件的AccessToken（如果有），自管理过期时间会根据微信返回过期时间值提前一段时间，如果之前的已过期或不存在则会自动获取新的AccessToken凭据。
	  分布式请传入旧凭据或直接使用旧凭据调用相关接口，若用于分布式服务，通过该方法扩展调用接口，在传入或使用AccessToken凭据的时候需要确保过期时间未到达过期提前条件
      过期提前条件 3600 -> 600 | 1800 -> 300 | 300 -> 30
    方法2、调用原始请求方法 OYMLCN.WeChat.Extension.GetAccessToken(cfg)
      此方法只做HTTPS请求并解析信息为AccessToken类，不会做过期判断。
    说明：后文中的token均为AccessToken实例的参数引用。
  获取微信服务器IP地址  （已通过调试测试）
    方法1、调用AccessToken凭据的扩展方法 token.GetIpAdress()，返回属性ip_list。
    方法2、调用原始方法 OYMLCN.WeChat.Extension.GetIpAdress(token)
自定义菜单
  接口调用入口：AccessToken
  自定义菜单创建接口  MenuCreate
  自定义菜单查询接口  MenuQuery
  自定义菜单删除接口  MenuDelete MenuDeleteCondition
  自定义菜单事件推送  RequestMessage.ToEventMessageXXX
  个性化菜单接口  MenuCreatCondition
  获取自定义菜单配置接口  MenuConfigQuery
消息管理  （已通过调试测试）
    消息调用入口：分别扩展了HttpRequest、Controller、HttpRequestBase、ApiController、HttpRequestMessage类，后三仅.NET461。
  验证消息真实性  （已通过调试测试）
    通过消息调用入口IsValidRequest(cfg)扩展方法，对签名进行验证，若正确则返回true。
  接收普通消息  （已通过单元测试）
    注意*：由于 NETCore1.0 不支持Stream指针位置的调整，故Body的内容正常情况下只能读取一次，为了不必要的麻烦勿在调用扩展方法GetWeChatRequsetXmlDocument前操作Request.Body。
    步骤1：通过消息调用入口GetWeChatRequsetXmlDocument(cfg=null)扩展方法获取Xml处理体。
           本方法会对消息真实性进行验证，若验证失败会抛出NotImplementedException("签名验证失败")异常。
		   若开启兼容模式或加密模式，会同时验证消息体加密签名，若验证失败会抛出NotImplementedException("消息体加密签名验证失败")异常。
           cfg参数可选，当开启兼容模式或加密模式时，将启用加密模式，需要提供基本配置信息以加解密信息。
    小节1：可通过处理体WeChatResponseXmlDocument的扩展方法IsRepeat()判断是否是重复消息以确定是否需要处理。
    步骤2：通过处理体WeChatResponseXmlDocument的扩展方法GetMsgType()获取消息类型。
    步骤3：若消息类型为Event事件类型，则可通过WeChatResponseXmlDocument扩展方法GetEventType()获取事件类型。
    步骤4：根据类型调用WeChatResponseXmlDocument的扩展方法ToXXX()转换为对应的消息体。
	小节2：消息体转换扩展方法分别有ToRequestMessageText()、ToRequestMessageImage()、ToRequestMessageVoice()、ToRequestMessageVideo()、ToRequestMessageShortVideo()、ToRequestMessageLocation()、ToRequestMessageLink()。
    步骤5：根据业务编写处理逻辑。
    步骤6：从消息体的扩展方法ResponseXXX(***)生成对应的回复信息处理体。
	小节3：消息体回复扩展方法分别有ResponseText(content)、ResponseImage(mediaId)、ResponseVoice(mediaId)、ResponseVideo(mediaId,title,description)、ResponseMusic(mediaId,musicUrl,title,description,hqMusicUrl)、ResponseNews(WeChatResponseNewItem)。
	小节4：图文消息传入参数为 OYMLCN.WeChat.Model.WeChatResponseNewItem(title,description,picurl,url) 实例。
    步骤7：直接返回回复信息处理体的Result属性中Xml内容以应答微信请求。
	小节5：回复信息处理体中的Result为最终返回给微信请求的内容，Source为返回的原始内容，当开启加密后Result将会是加密后的消息内容，其中IsEncrypt标识当前返回信息是否已经加密。
    链式调用示例（Controller或ApiController）：
        this.GetWeChatRequsetXmlDocument(Config).ToRequestMessageText().ResponseText("WeChatTest").Result;
  接收事件推送  （已通过单元测试）
      使用方法同 接收普通消息 ，到达 步骤3 判断消息类型后使用对应的扩展方法将消息转换为对应的事件消息体。
    消息体转换事件扩展方法分别有ToEventMessage()、ToEventMessage扫描带参数二维码()、ToEventMessage上报地理位置()、ToEventMessage点击自定义菜单()、ToEventMessage点击菜单跳转链接()。
  被动回复消息  （已通过单元测试）
      参考 接收普通消息 的 步骤6 。
  消息加密  （已通过调试测试）
      在调用GetWeChatRequsetXmlDocument时会自动根据消息体处理信息的解密
	  在调用扩展方法ResponseXXX(***)的时候会根据处理体是否经过解密来决定是否加密返回的Result
  客服消息
    客服帐号管理  （已通过调用测试）
	    接口调用入口：AccessToken（token）
		接口所在模块：OYMLCN.WeChat.CustomerServiceApi
	  添加客服帐号  token.CustomerServiceAccountAdd(kfName,nickName)
	  修改客服帐号  token.CustomerServiceAccountUpdate(kfName,nickName)
	  删除客服帐号  token.CustomerServiceAccountDelete(kfName)
      设置客服帐号的头像  token.CustomerServiceAccountUploadHeadImg(kfName,filePath)
	  获取所有客服账号  token.CustomerServiceAccountQuery()
    客服接口-发消息  （已通过单元测试）（已通过调用测试）
		接口调用入口：AccessToken（token）
		接口所在模块：OYMLCN.WeChat.CustomerServiceApi
	  发送文本消息  token.CustomerServiceSendText(openid,text,kfName=null)
	  发送图片消息  token.CustomerServiceSendImage(openid,mediaId,kfName=null)
	  发送语音消息  token.CustomerServiceSendVoice(openid,mediaId,kfName=null)
	  发送视频消息  token.CustomerServiceSendVoice(openid,mediaId,thumbMediaId,title=null,description=null,kfName=null)
	  发送音乐消息  token.CustomerServiceSendMusic(openid,mediaId,thumbMediaId,musicUrl,hqMusicUrl,title=null,description=null,kfName=null)
	  发送图文消息
	      token.CustomerServiceSendNews(openid,kfName=null,WeChatResponseNewItem[])
	      token.CustomerServiceSendNews(openid,List<WeChatResponseNewItem>,kfName=null)
	      token.CustomerServiceSendMpNews(openid,mediaId)
	  发送卡券 （暂无权限进行调用测试） token.CustomerServiceSendCard(openid,cardId,kfName=null)
  群发接口和原创校验  接口入口AccessToken  MassXXX 未进行测试验证
    上传图文消息内的图片获取URL  MassMessageImageUpload
    上传图文消息素材  MassMessageNewsUpload
    根据标签进行群发  MassMessageSendXXX
    根据OpenID列表群发  MassMessageSendXXXByOpenId
    删除群发  MassMessageSentDelete
    预览接口  MassMessageSendXXXPreview
    查询群发消息发送状态  MassMessageSentStateQuery
    事件推送群发结果  RequestMessage.ToPush群发消息
  模板消息  接口入口AccessToken 未完整测试
    设置所属行业  TemplateIndustrySet
    获取设置的行业信息  TemplateIndustryQuery
    获得模板ID  TemplateAdd
    获取模板列表  TemplateQuery
    删除模板  TemplateDelete
    发送模板消息  TemplateMessageSend
    事件推送  RequestMessage.ToPush模板消息
  获取公众号的自动回复规则 ×

微信网页开发
  微信网页授权  
    授权地址生成  Config.WebUrlScopeBase Config.WebUrlScopeUserInfo
    获取授权Code  Request.GetWebOauthCode
    获取用户Token  Config.WebAccessTokenXX
    获取用户信息  WebAccessToken.WebUserInfo
    检查Token是否有效  WebAccessToken.Check
    刷新用户的Token  Config.WebAccessTokenRefresh WebAccessToke.Refresh
  微信JS-SDK
    获取JsApiTicket  AccessToken.GetJsApiTicket
    生成JS-SDK权限签名包  JsApiTicket.CreatePackage
    获取卡券ApiTicket ○
    生成卡券签名包 ○
素材管理  （已通过调用测试）
  	接口调用入口：AccessToken（token）
    接口所在模块：OYMLCN.WeChat.MediaApi
  新增临时素材  token.MediaUpload(type,filePath)
  获取临时素材
      token.MediaDownloadUrl(type,mediaId)
	  token.MediaSpeexDownloadUrl(mediaId)
  新增永久素材
      token.MaterialNewsAdd(MediaNewItem[])
      token.MaterialNewsAdd(List<MediaNewItem>)
      token.MaterialUploadImage(filePath)
	  token.MaterialUpload(type,filePath)
  获取永久素材  MaterialDownload MaterialNewQuery
      token.MaterialDownload(type,mediaId) 
		   返回Stream流 可调用OYMLCN中的Stream扩展方法WriteToFile(fileName)保存
	  token.MaterialVideoDownloadUrl(mediaId)
	  token.MaterialNewsQuery(mediaId)
  删除永久素材  token.MaterialDelete(mediaId)
  修改永久图文素材  token.MaterialNewUpdate(mediaId,index,MediaNewItem)
  获取素材总数  token.MaterialCount()
  获取素材列表  token.MaterialMediaListQuery(type,offset,count)
用户管理  接口入口AccessToken
  用户标签管理  TagCreate TagQuery TagUpdate TagDelete TagUsersQuery
  用户分组管理  TagApply TagCancel TagUserQuery
  设置用户备注名  UserRemark
  获取用户基本信息  UserInfo
  获取用户列表  UsersQuery
  获取用户地理位置  RequestMessage.ToEventMessage上报地理位置
  黑名单管理  UserDefriendApply UserDefriendApply UserDefriendCancel
账号管理  接口入口AccessToken
  生成带参数二维码  CreateQRScene CreateQRLimitScene
  长链接转短链接接口  LongUrlToShort
  微信认证事件推送 ×
数据统计 ○
微信卡券 ○
微信门店 ○
微信小店 ○
微信设备 ×
微信客服  接口未能完整测试
  消息转发到客服  RequestMessage.TransferToCustomerService
  客服管理  AccessToken.CustomerServiceAccountXXX
  会话控制  AccessToken.CustomerServiceSessionXXX
  获取聊天记录  AccessToken.CustomerServiceRecordQuery
  会话状态通知事件  RequestMessage.ToPushCustomerServiceXXX
微信摇一摇周边 ×
微信连Wi-Fi ×
微信扫一扫 ×
微信小程序 ○
微信开放平台 ○