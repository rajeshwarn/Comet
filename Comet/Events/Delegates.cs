namespace Comet.Events
{
    public class Delegates
    {
        #region Constructors

        public delegate void DownloaderProgressChangedEventHandler(DownloaderEventArgs e);

        public delegate void UpdaterStateChangedEventHandler(UpdaterStateEventArgs e);

        #endregion
    }
}