using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace OYMLCN.WPF
{
    public partial class WebBrowserHelper
    {
        private class WebBrowserEvent : StandardOleMarshalObject, IDWebBrowserEvents2
        {
            private WebBrowserHelper _helperInstance = null;

            public WebBrowserEvent(WebBrowserHelper helperInstance) => _helperInstance = helperInstance;

            #region DWebBrowserEvents2 成员
            public void StatusTextChange(string text) => _helperInstance.OnStatusTextChange(text);

            public void ProgressChange(int progress, int progressMax) => _helperInstance.OnProgressChange(progress, progressMax);

            public void CommandStateChange(long command, bool enable) => _helperInstance.OnCommandStateChange(command, enable);

            public void DownloadBegin() { }

            public void DownloadComplete() { }

            public void TitleChange(string text) => _helperInstance.OnTitleChange(text);

            public void PropertyChange(string szProperty) { }

            public void BeforeNavigate2(object pDisp, ref object URL, ref object flags, ref object targetFrameName, ref object postData, ref object headers, ref bool cancel) => _helperInstance.OnBeforeNavigate(URL as string, ref cancel);

            public void NewWindow2(ref object pDisp, ref bool cancel) => _helperInstance.OnNewWindow(ref cancel);

            public void NavigateComplete2(object pDisp, ref object URL) => _helperInstance.OnNavigateComplete(URL as string);

            public void DocumentComplete(object pDisp, ref object URL) => _helperInstance.OnLoad(ref URL);

            public void OnQuit() { }

            public void OnVisible(bool visible) { }

            public void OnToolBar(bool toolBar) { }

            public void OnMenuBar(bool menuBar) { }

            public void OnStatusBar(bool statusBar) { }

            public void OnFullScreen(bool fullScreen) { }

            public void OnTheaterMode(bool theaterMode) { }

            public void WindowSetResizable(bool resizable) { }

            public void WindowSetLeft(int left) { }

            public void WindowSetTop(int top) { }

            public void WindowSetWidth(int width) { }

            public void WindowSetHeight(int height) { }

            public void WindowClosing(bool isChildWindow, ref bool cancel) { }

            public void ClientToHostWindow(ref long cx, ref long cy) { }

            public void SetSecureLockIcon(int secureLockIcon) { }

            public void FileDownload(ref bool cancel) { }

            public void NavigateError(object pDisp, ref object URL, ref object frame, ref object statusCode, ref bool cancel) => _helperInstance.OnNavigateError(URL as string, ref cancel);

            public void PrintTemplateInstantiation(object pDisp) { }

            public void PrintTemplateTeardown(object pDisp) { }

            public void UpdatePageStatus(object pDisp, ref object nPage, ref object fDone) { }

            public void PrivacyImpactedStateChange(bool bImpacted) { }

            #endregion
        }
    }
}
