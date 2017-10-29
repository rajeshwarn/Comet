namespace Comet.Managers
{
    #region Namespace

    using Comet.Structure;

    #endregion

    /// <summary>The Package Manager.</summary>
    public class PackageManager
    {
        #region Properties

        /// <summary>The working package.</summary>
        public static Package WorkingPackage { get; set; }

        /// <summary>The working path. </summary>
        public static string WorkingPath { get; set; }

        #endregion
    }
}