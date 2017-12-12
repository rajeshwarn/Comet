namespace Comet.Enums
{
    #region Namespace

    using System;

    #endregion

    [Serializable]
    public enum UpdaterState
    {
        /// <summary>The not checked.</summary>
        NotChecked,

        /// <summary>The checking.</summary>
        Checking,

        /// <summary>The updated.</summary>
        Updated,

        /// <summary>The outdated.</summary>
        Outdated,

        /// <summary>The downloading.</summary>
        Downloading,

        /// <summary>The unable to connect state.</summary>
        NoConnection,

        /// <summary>The package does not exist.</summary>
        PackageNotFound,

        /// <summary>The package data does not exist.</summary>
        PackageDataNotFound,

        /// <summary>The update ready state.</summary>
        UpdateReady
    }
}