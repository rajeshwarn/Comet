namespace Comet
{
    #region Namespace

    using System;
    using System.Collections.Generic;

    using Comet.Managers;

    #endregion

    internal class Program
    {
        #region Events

        /// <summary>The main program.</summary>
        /// <param name="arguments">The program arguments.</param>
        private static void Main(string[] arguments)
        {
            ConsoleManager.Initialize();
            Settings.Initialize();
            ProcessContent(arguments);
        }

        /// <summary>Process content.</summary>
        /// <param name="arguments">The program arguments.</param>
        private static void ProcessContent(IReadOnlyCollection<string> arguments)
        {
            if ((arguments == null) || (arguments.Count == 0))
            {
                while (true)
                {
                    string consoleInput = ConsoleManager.ReadInput();

                    if (string.IsNullOrWhiteSpace(consoleInput))
                    {
                        continue;
                    }
                    else
                    {
                        // Checks for uppercase character.
                        if (!char.IsUpper(consoleInput, 0))
                        {
                            string uppercaseFirst = StringManager.UppercaseFirst(consoleInput);
                            consoleInput = uppercaseFirst;
                        }
                    }

                    ConsoleManager.CreateCommandInstance(consoleInput);
                }
            }
            else
            {
                ConsoleManager.WriteOutput("Total command argument/s: " + arguments.Count);
                foreach (string argument in arguments)
                {
                    ConsoleManager.WriteOutput("Processing command argument: " + argument);
                    ConsoleManager.CreateCommandInstance(argument);
                }
            }
        }


       #endregion
    }
}