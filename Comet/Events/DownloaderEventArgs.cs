namespace Comet.Events
{
    #region Namespace

    using System;

    using Comet.Managers;

    #endregion

    public class DownloaderEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="DownloaderEventArgs" /> class.</summary>
        /// <param name="percentDone">The percent Done.</param>
        /// <param name="unweightedPercent">The unweighted Percent.</param>
        /// <param name="extraStatus">The extra Status.</param>
        /// <param name="status">The status.</param>
        /// <param name="payload">The payload.</param>
        public DownloaderEventArgs(int percentDone, int unweightedPercent, string extraStatus, OldDownloader.ProgressStatus status, object payload)
        {
            PercentDone = percentDone;
            UnWeightedPercent = unweightedPercent;
            ExtraStatus = extraStatus;
            Status = status;
            Payload = payload;
        }

        #endregion

        #region Properties

        public string ExtraStatus { get; }

        public object Payload { get; }

        public int PercentDone { get; }

        public OldDownloader.ProgressStatus Status { get; }

        public int UnWeightedPercent { get; }

        #endregion
    }
}