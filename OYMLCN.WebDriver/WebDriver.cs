using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;

namespace OYMLCN
{
    /// <summary>
    /// WebDriver
    /// </summary>
    public class WebDriver
    {
        /// <summary>
        /// 浏览器类型
        /// </summary>
        public enum Browsers
        {
            /// <summary>
            ///  Internet Explorer
            /// </summary>
            IE,
            /// <summary>
            /// Google Chrome
            /// </summary>
            Chrome,
            /// <summary>
            /// Mozilla Firefox
            /// </summary>
            Firefox,
            /// <summary>
            /// PhantomJS
            /// </summary>
            PhantomJS
        }

        private IWebDriver wd = null;
        private Browsers browser = Browsers.IE;
        /// <summary>
        /// WebDriver
        /// </summary>
        /// <param name="Browser">浏览器类型</param>
        public WebDriver(Browsers Browser=Browsers.Chrome)
        {
            browser = Browser;
            wd = InitWebDriver();
        }

        private IWebDriver InitWebDriver()
        {
            IWebDriver theDriver = null;
            switch (browser)
            {
                case Browsers.Chrome:
                    theDriver = new ChromeDriver();
                    break;
                case Browsers.Firefox:
                    theDriver = new FirefoxDriver();
                    break;
                case Browsers.PhantomJS:
                    theDriver = new PhantomJSDriver();
                    break;
                case Browsers.IE:
                default:
                    theDriver = new InternetExplorerDriver(new InternetExplorerOptions()
                    {
                        IntroduceInstabilityByIgnoringProtectedModeSettings = false
                    });
                    break;
            }
            return theDriver;
        }

        /// <summary>
        /// 设置最长等待时间
        /// 只需要设置一次（如果需要的话）
        /// </summary>
        /// <param name="seconds"></param>
        public void ImplicitlyWait(double seconds) =>
            wd.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);

        /// <summary>
        /// 等待页面标题 如果匹配则立即返回
        /// </summary>
        /// <param name="title"></param>
        public void WaitForPage(string title) =>
            new WebDriverWait(wd, TimeSpan.FromSeconds(10)).Until((d) =>
            {
                return d.Title.ToLower().StartsWith(title.ToLower());
            });

        /// <summary>
        /// 等待指定Id的页面元素 如果存在则立即返回
        /// </summary>
        /// <param name="id"></param>
        public void WaitForIdElement(string id) =>
            new WebDriverWait(wd, TimeSpan.FromSeconds(10)).Until((d) =>
            {
                return ExpectedConditions.ElementExists(By.Id(id));
            });
        /// <summary>
        /// 等待指定样式名称的页面元素 如果存在则立即返回
        /// </summary>
        /// <param name="className"></param>
        public void WaitForClassElement(string className) =>
            new WebDriverWait(wd, TimeSpan.FromSeconds(10)).Until((d) =>
            {
                return ExpectedConditions.ElementExists(By.ClassName(className));
            });
        /// <summary>
        /// 等待指定路径的页面元素 如果存在则立即返回
        /// </summary>
        /// <param name="xpath"></param>
        public void WaitForXPathElement(string xpath) =>
            new WebDriverWait(wd, TimeSpan.FromSeconds(10)).Until((d) =>
            {
                return ExpectedConditions.ElementExists(By.XPath(xpath));
            });


        /// <summary>
        /// 加载一个新页面
        /// </summary>
        /// <param name="url"></param>
        public void GoToUrl(string url) => wd.Navigate().GoToUrl(url);

        /// <summary>
        /// 刷新页面
        /// </summary>
        public void Refresh() => wd.Navigate().Refresh();

        /// <summary>
        /// 返回上一页
        /// </summary>
        public void Back() => wd.Navigate().Back();

        /// <summary>
        /// 导航到下一页
        /// </summary>
        public void Forward() => wd.Navigate().Forward();

        /// <summary>
        /// 当前已导航到的Url
        /// </summary>
        /// <returns></returns>
        public string Url => wd.Url;

        /// <summary>
        /// 当前页面标题
        /// </summary>
        /// <returns></returns>
        public string Title => wd.Title;

        /// <summary>
        /// 页面源代码
        /// </summary>
        public string PageSource => wd.PageSource;

        /// <summary>
        /// 获取Cookies
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAllCookies()
        {
            Dictionary<string, string> cookies = new Dictionary<string, string>();
            switch (browser)
            {
                case Browsers.Chrome:
                    foreach (Cookie cookie in ((ChromeDriver)wd).Manage().Cookies.AllCookies)
                        cookies[cookie.Name] = cookie.Value;
                    break;
                case Browsers.Firefox:
                    foreach (Cookie cookie in ((FirefoxDriver)wd).Manage().Cookies.AllCookies)
                        cookies[cookie.Name] = cookie.Value;
                    break;
                case Browsers.PhantomJS:
                    foreach (Cookie cookie in ((PhantomJSDriver)wd).Manage().Cookies.AllCookies)
                        cookies[cookie.Name] = cookie.Value;
                    break;
                case Browsers.IE:
                default:
                    foreach (Cookie cookie in ((InternetExplorerDriver)wd).Manage().Cookies.AllCookies)
                        cookies[cookie.Name] = cookie.Value;
                    break;
            }

            return cookies;
        }

        /// <summary>
        /// 清空Cookies
        /// </summary>
        public void DeleteAllCookies()
        {
            switch (browser)
            {
                case Browsers.Chrome:
                    ((ChromeDriver)wd).Manage().Cookies.DeleteAllCookies();
                    break;
                case Browsers.Firefox:
                    ((FirefoxDriver)wd).Manage().Cookies.DeleteAllCookies();
                    break;
                case Browsers.PhantomJS:
                    ((PhantomJSDriver)wd).Manage().Cookies.DeleteAllCookies();
                    break;
                default:
                    ((InternetExplorerDriver)wd).Manage().Cookies.DeleteAllCookies();
                    break;
            }
        }

        /// <summary>
        /// 激活指定的窗口
        /// </summary>
        /// <param name="title"></param>
        /// <param name="exactMatch"></param>
        public void GoToWindow(string title, bool exactMatch)
        {
            string theCurrent = wd.CurrentWindowHandle;
            IList<string> windows = wd.WindowHandles;
            if (exactMatch)
            {
                foreach (var window in windows)
                {
                    wd.SwitchTo().Window(window);
                    if (wd.Title.ToLower() == title.ToLower())
                        return;
                }
            }
            else
            {
                foreach (var window in windows)
                {
                    wd.SwitchTo().Window(window);
                    if (wd.Title.ToLower().Contains(title.ToLower()))
                        return;
                }
            }

            wd.SwitchTo().Window(theCurrent);
        }

        /// <summary>
        /// 切换到指定的框架
        /// </summary>
        /// <param name="name"></param>
        public void GoToFrame(string name)
        {
            IWebElement theFrame = null;
            var frames = wd.FindElements(By.TagName("iframe"));
            foreach (var frame in frames)
            {
                if (frame.GetAttribute("name").ToLower() == name.ToLower())
                {
                    theFrame = (IWebElement)frame;
                    break;
                }
            }
            if (theFrame != null)
                wd.SwitchTo().Frame(theFrame);
        }

        /// <summary>
        /// 切换到指定的框架
        /// </summary>
        /// <param name="frame"></param>
        public void GoToFrame(IWebElement frame) => wd.SwitchTo().Frame(frame);

        /// <summary>
        /// 切换到默认的窗体框架
        /// </summary>
        public void GoToDefault() => wd.SwitchTo().DefaultContent();

        /// <summary>
        /// 获取弹窗信息
        /// </summary>
        /// <returns></returns>
        public string AlertString
        {
            get
            {
                string theString = string.Empty;
                IAlert alert = null;
                alert = wd.SwitchTo().Alert();
                if (alert != null)
                    theString = alert.Text;
                return theString;
            }
        }

        /// <summary>
        /// 接受提示
        /// </summary>
        public void AlertAccept()
        {
            IAlert alert = null;
            alert = wd.SwitchTo().Alert();
            if (alert != null)
                alert.Accept();
        }

        /// <summary>
        /// 取消提示
        /// </summary>
        public void AlertDismiss()
        {
            IAlert alert = null;
            alert = wd.SwitchTo().Alert();
            if (alert != null)
                alert.Dismiss();
        }

        /// <summary>
        /// 执行JS代码
        /// </summary>
        /// <param name="script"></param>
        /// <param name="args"></param>
        public void ExecuteScript(string script, params object[] args)
        {
            switch (browser)
            {
                case Browsers.Chrome:
                    ((ChromeDriver)wd).ExecuteScript(script, args);
                    break;
                case Browsers.Firefox:
                    ((FirefoxDriver)wd).ExecuteScript(script, args);
                    break;
                case Browsers.PhantomJS:
                    ((PhantomJSDriver)wd).ExecuteScript(script, args);
                    break;
                case Browsers.IE:
                default:
                    ((InternetExplorerDriver)wd).ExecuteScript(script, args);
                    break;
            }
        }

        /// <summary>
        /// 移动到页面底部
        /// </summary>
        public void PageScrollToBottom() => ExecuteScript("document.documentElement.scrollTop=10000");

        /// <summary>
        /// 移动到页面最右
        /// </summary>
        public void PageScrollToRight() => ExecuteScript("document.documentElement.scrollLeft=10000");


        /// <summary>
        /// 指定的元素区域移动到底部
        /// </summary>
        /// <param name="element"></param>
        public void ElementScrollToBottom(IWebElement element)
        {
            string id = element.GetAttribute("id");
            string name = element.GetAttribute("name");
            var js = "";
            if (!string.IsNullOrWhiteSpace(id))
                js = "document.getElementById('" + id + "').scrollTop=10000";
            else if (!string.IsNullOrWhiteSpace(name))
                js = "document.getElementsByName('" + name + "')[0].scrollTop=10000";
            ExecuteScript(js);
        }

        /// <summary>
        /// 保存页面截图
        /// </summary>
        /// <param name="savePath"></param>
        public void TakeScreenshot(string savePath)
        {
            Screenshot theScreenshot = null;
            switch (browser)
            {
                case Browsers.Chrome:
                    theScreenshot = ((ChromeDriver)wd).GetScreenshot();
                    break;
                case Browsers.Firefox:
                    theScreenshot = ((FirefoxDriver)wd).GetScreenshot();
                    break;
                case Browsers.PhantomJS:
                    theScreenshot = ((PhantomJSDriver)wd).GetScreenshot();
                    break;
                case Browsers.IE:
                default:
                    theScreenshot = ((InternetExplorerDriver)wd).GetScreenshot();
                    break;
            }
            if (theScreenshot != null)
                theScreenshot.SaveAsFile(savePath, ScreenshotImageFormat.Jpeg);
        }

        /// <summary>
        /// 查找指定Id的元素
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IWebElement FindElementById(string id) => wd.FindElement(By.Id(id));

        /// <summary>
        /// 查找指定名称的元素
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IWebElement FindElementByName(string name) => wd.FindElement(By.Name(name));

        /// <summary>
        /// 查找指定路径的元素
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public IWebElement FindElementByXPath(string xpath) => wd.FindElement(By.XPath(xpath));

        /// <summary>
        /// 查找指定文字的链接
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IWebElement FindElementByLinkText(string text) => wd.FindElement(By.LinkText(text));

        /// <summary>
        /// 查找指定文字的链接
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IList<IWebElement> FindElementsByLinkText(string text) => wd.FindElements(By.LinkText(text));

        /// <summary>
        /// 查找包含指定文字的链接
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IList<IWebElement> FindElementsByPartialLinkText(string text) => wd.FindElements(By.PartialLinkText(text));

        /// <summary>
        /// 查找指定样式名称的元素
        /// </summary>
        /// <param name="clsName"></param>
        /// <returns></returns>
        public IList<IWebElement> FindElementsByClassName(string clsName) => wd.FindElements(By.ClassName(clsName));

        /// <summary>
        /// 查找指定标签名称的元素
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public IList<IWebElement> FindElementsByTagName(string tagName) => wd.FindElements(By.TagName(tagName));

        /// <summary>
        /// 查找指定CSS内联样式的元素
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        public IList<IWebElement> FindElementsByCssSelector(string css) => wd.FindElements(By.CssSelector(css));

        /// <summary>
        /// 查找指定路径的元素
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public IList<IWebElement> FindElementsByXPathName(string xpath) => wd.FindElements(By.XPath(xpath));

        /// <summary>
        /// Executes javascript
        /// </summary>
        /// <param name="js"></param>
        public void ExecuteJS(string js)
        {
            switch (browser)
            {
                case Browsers.Chrome:
                    ((ChromeDriver)wd).ExecuteScript(js, null);
                    break;
                case Browsers.Firefox:
                    ((FirefoxDriver)wd).ExecuteScript(js, null);
                    break;
                case Browsers.PhantomJS:
                    ((PhantomJSDriver)wd).ExecuteScript(js, null);
                    break;
                case Browsers.IE:
                default:
                    ((InternetExplorerDriver)wd).ExecuteScript(js, null);
                    break;
            }
        }

        /// <summary>
        /// 单击指定元素
        /// </summary>
        /// <param name="element"></param>
        public void ClickElement(IWebElement element) => (new Actions(wd)).Click(element).Perform();

        /// <summary>
        /// 双击指定元素
        /// </summary>
        /// <param name="element"></param>
        public void DoubleClickElement(IWebElement element) => (new Actions(wd)).DoubleClick(element).Perform();

        /// <summary>
        /// 按住指定元素
        /// </summary>
        /// <param name="element"></param>
        public void ClickAndHoldOnElement(IWebElement element) => (new Actions(wd)).ClickAndHold(element).Perform();

        /// <summary>
        /// 右键单击指定元素
        /// </summary>
        /// <param name="element"></param>
        public void ContextClickOnElement(IWebElement element) => (new Actions(wd)).ContextClick(element).Perform();
        /// <summary>
        /// 右键单击指定元素
        /// </summary>
        /// <param name="element"></param>
        public void RightClickElement(IWebElement element) => ContextClickOnElement(element);

        /// <summary>
        /// 拖动元素到指定目标
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void DragAndDropElement(IWebElement source, IWebElement target) => (new Actions(wd)).DragAndDrop(source, target).Perform();

        /// <summary>
        /// 向指定元素输入文本
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        public void SendKeysToElement(IWebElement element, string text) => (new Actions(wd)).SendKeys(element, text).Perform();


        /// <summary>
        /// 关闭测试代理服务，并尝试退出所有已打开的窗口
        /// </summary>
        public void Cleanup() => wd.Quit();
    }

}
