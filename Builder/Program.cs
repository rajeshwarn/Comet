namespace Builder
{
    #region Namespace

    using System;
    using System.Windows.Forms;

    using Builder.Forms;

    #endregion

    internal static class Program
    {
        #region Events

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        #endregion
    }
}