namespace Comet.Localize
{
    #region Namespace

    using System.Collections.Generic;
    using System.Windows.Forms;

    #endregion

    internal class Descriptions
    {
        #region Properties

        public static List<string> CommandDescriptions
        {
            get
            {
                var _listDescriptions = new List<string>
                    {
                        "Clears the console output.",
                        "Connect to a host.",
                        "Download a package.",
                        "Edit the working package data.",
                        "Exits the application.",
                        "Gets the url status information.",
                        "Provides Help information for " + Application.ProductName + " commands.",
                        "Displays package information.",
                        "Displays network information.",
                        "Creates a new package.",
                        "Open a local or remote package file.",
                        "Update the application.",
                        "Reads the  loaded package from memory.",
                        "Saves the working package to a file."
                    };

                return _listDescriptions;
            }
        }

        #endregion
    }
}