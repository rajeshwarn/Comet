namespace Comet.Controls
{
    #region Namespace

    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    #endregion

    internal class Notification
    {
        #region Events

        /// <summary> Displays notification icon.</summary>
        /// <param name="icon">The icon.</param>
        /// <param name="title">The title.</param>
        /// <param name="text">The text.</param>
        /// <param name="toolTipIcon">The tool tip icon.</param>
        /// <param name="timeout">The timeout.</param>
        public static void DisplayNotification(Icon icon, string title, string text, ToolTipIcon toolTipIcon, int timeout = 5000)
        {
            new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    NotifyIcon _notifyIcon = new NotifyIcon
                        {
                            Visible = true,
                            Icon = icon,
                            BalloonTipIcon = toolTipIcon,
                            BalloonTipTitle = title,
                            BalloonTipText = text
                        };
                    _notifyIcon.ShowBalloonTip(timeout);

                    // This will let the balloon close after it's 5 second timeout
                    // for demonstration purposes. Comment this out to see what happens
                    // when dispose is called while a balloon is still visible.
                    Thread.Sleep(10000);

                    // The notification should be disposed when you don't need it anymore,
                    // but doing so will immediately close the balloon if it's visible.
                    _notifyIcon.Dispose();
                }).Start();
        }

        #endregion
    }
}