namespace Comet
{
    #region Namespace

    using System;

    #endregion

    public class ConsoleManager
    {
        #region Events

        /// <summary>Draws a line across the console.</summary>
        /// <param name="symbol">The symbol to draw.</param>
        public static void DrawLine(string symbol = "-")
        {
            for (var i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write(symbol);
            }
        }

        #endregion
    }
}