namespace Comet.PInvoke
{
    #region Namespace

    using System.ComponentModel;
    using System.Runtime.InteropServices;

    #endregion

    internal static class Wininet
    {
        #region Events

        [Description("Retrieves the connected state of the local system.")]
        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetGetConnectedState(out int lpdwFlags, int dwReserved);

        #endregion
    }
}