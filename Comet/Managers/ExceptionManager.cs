﻿namespace Comet.Managers
{
    #region Namespace

    using System;

    using Comet.Structure;

    #endregion

    internal class ExceptionManager
    {
        #region Events

        /// <summary>Creates an exception command not recognized message.</summary>
        /// <param name="command">The command.</param>
        public static void CommandNotRecognized(ConsoleCommand command)
        {
            WriteException($"\'{command.Name}\' is not recognized as a command. ");
            Console.WriteLine();
        }

        /// <summary>Creates an file not found exception.</summary>
        /// <param name="file">The path to the file.</param>
        public static void ShowFileNotFoundException(string file)
        {
            WriteException("The system cannot find the specified file.");
            WriteException("Path: '" + file + "'");
        }

        /// <summary>Shows an exception null or empty exception message.</summary>
        /// <param name="field">The field.</param>
        public static void ShowNullOrEmpty(string field)
        {
            Console.ForegroundColor = Settings.ErrorColor;
            Console.Write(Settings.ErrorCharacter + " ");
            Console.ForegroundColor = Settings.ErrorTextColor;
            Console.Write("The value is null or empty. The field '" + field + "' must contain a value.");
        }

        /// <summary>Creates an exception message.</summary>
        /// <param name="message">The message.</param>
        public static void WriteException(string message)
        {
            Console.ForegroundColor = Settings.ErrorColor;
            Console.Write(Settings.ErrorCharacter + " ");
            Console.ForegroundColor = Settings.ErrorTextColor;
            Console.WriteLine(message);
        }

        #endregion
    }
}