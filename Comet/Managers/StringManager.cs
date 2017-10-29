namespace Comet.Managers
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Text;

    using Comet.Structure;

    #endregion

    internal class StringManager
    {
        #region Events

        /// <summary>Draws the text centered on the console.</summary>
        /// <param name="text">The text to draw.</param>
        public static void DrawCenterText(string text)
        {
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text);
        }

        /// <summary>Draws the table.</summary>
        /// <param name="formatting">The formatting.</param>
        /// <param name="commandsDictionary">The commands Dictionary.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string DrawDictionaryTable(string formatting, Dictionary<string, string> commandsDictionary)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var valuePair in commandsDictionary)
            {
                stringBuilder.AppendFormat(formatting, valuePair.Key, valuePair.Value);
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        ///// <summary>Draws the table.</summary>
        ///// <param name="list">The list.</param>
        ///// <returns>The <see cref="string" />.</returns>
        // public static string DrawListTable(List<string> list)
        // {
        // StringBuilder stringBuilder = new StringBuilder();
        // foreach (string listItem in list)
        // {
        // stringBuilder.AppendFormat(listItem);
        // stringBuilder.AppendLine();
        // }

        // return stringBuilder.ToString();
        // }

        /// <summary>Draws the package data table.</summary>
        /// <param name="package">The package.</param>
        public static void DrawPackageTable(Package package)
        {
            if (package.IsEmpty)
            {
                package = new Package();
            }

            for (var i = 0; i < package.Count; i++)
            {
                Package.PackageData _packageData = (Package.PackageData)i;
                string _package = _packageData.ToString().PadRight(10);
                string _data = "-".PadRight(2) + package.ToList[i];

                ConsoleManager.WriteOutput(_package + _data);
            }
        }

        /// <summary>Uppercase the first letter of the input.</summary>
        /// <param name="input">The input string.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string UppercaseFirst(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            return char.ToUpper(input[0]) + input.Substring(1);
        }

        #endregion
    }
}