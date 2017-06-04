using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OYMLCN
{
    /// <summary>
    /// ShowMessage
    /// </summary>
    public static class ShowMessage
    {
        /// <summary>
        /// 显示确定对话条
        /// </summary>
        /// <param name="win"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="ok"></param>
        /// <returns></returns>
        public static async Task<MessageDialogResult> ShowOkAsync(this MetroWindow win, string title, string message, string ok = "确定") =>
            await win.ShowMessageAsync(title, message, MessageDialogStyle.Affirmative, new MetroDialogSettings()
            {
                AffirmativeButtonText = ok
            });
        /// <summary>
        /// 显示确定/取消对话条
        /// </summary>
        /// <param name="win"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="ok"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public static async Task<MessageDialogResult> ShowOkAndCancelAsync(this MetroWindow win, string title, string message, string ok = "确定", string cancel = "取消") =>
            await win.ShowMessageAsync(title, message, MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings()
            {
                AffirmativeButtonText = ok,
                NegativeButtonText = cancel
            });
        /// <summary>
        /// 显示是/否对话条
        /// </summary>
        /// <param name="win"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="yes"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        public static async Task<MessageDialogResult> ShowYesAndNoAsync(this MetroWindow win, string title, string message, string yes = "是", string no = "否") =>
            await win.ShowMessageAsync(title, message, MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings()
            {
                AffirmativeButtonText = yes,
                NegativeButtonText = no
            });
        /// <summary>
        /// 显示是/否/取消对话条
        /// </summary>
        /// <param name="win"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="yes"></param>
        /// <param name="no"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public static async Task<MessageDialogResult> ShowYesNoAndCancelAsync(this MetroWindow win, string title, string message, string yes = "是", string no = "否", string cancel = "取消") =>
            await win.ShowMessageAsync(title, message, MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, new MetroDialogSettings()
            {
                AffirmativeButtonText = yes,
                NegativeButtonText = no,
                FirstAuxiliaryButtonText = cancel
            });
        /// <summary>
        /// 显示是/否/取消/取消对话条
        /// </summary>
        /// <param name="win"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="yes"></param>
        /// <param name="no"></param>
        /// <param name="cancel"></param>
        /// <param name="cancel2"></param>
        /// <returns></returns>
        public static async Task<MessageDialogResult> ShowYesNoCancelAndCancelAsync(this MetroWindow win, string title, string message, string yes = "是", string no = "否", string cancel = "取消", string cancel2 = "取消") =>
            await win.ShowMessageAsync(title, message, MessageDialogStyle.AffirmativeAndNegativeAndDoubleAuxiliary, new MetroDialogSettings()
            {
                AffirmativeButtonText = yes,
                NegativeButtonText = no,
                FirstAuxiliaryButtonText = cancel,
                SecondAuxiliaryButtonText = cancel2
            });
    }
}
