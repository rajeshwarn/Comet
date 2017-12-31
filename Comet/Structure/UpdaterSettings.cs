namespace Comet.Structure
{
    #region Namespace

    using System;

    #endregion

    public struct UpdaterSettings
    {
        public bool AutoUpdate { get; set; }

        public bool NotifyUpdateAvailable { get; set; }

        public bool NotifyUpdateReadyToInstall { get; set; }

        public bool DisplayWelcomePage { get; set; }

        [Obsolete]
        public bool RestartApplicationAfterInstall { get; set; }
    }
}