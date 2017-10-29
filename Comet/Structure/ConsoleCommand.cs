namespace Comet.Structure
{
    #region Namespace

    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    #endregion

    /// <summary>The <see cref="ConsoleCommand" />.</summary>
    public class ConsoleCommand
    {
        #region Variables

        private List<string> _arguments;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="ConsoleCommand" /> class.</summary>
        /// <param name="input">The command input.</param>
        public ConsoleCommand(string input)
        {
            // Regex to split string on spaces, but preserve quoted text intact:
            var stringArray = Regex.Split(input, "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            _arguments = new List<string>();
            for (var i = 0; i < stringArray.Length; i++)
            {
                // The first element is always the command:
                if (i == 0)
                {
                    Name = stringArray[i];

                    // Set the default:
                    LibraryClassName = "DefaultCommands";
                    var s = stringArray[0].Split('.');
                    if (s.Length != 2)
                    {
                        continue;
                    }

                    LibraryClassName = s[0];
                    Name = s[1];
                }
                else
                {
                    string inputArgument = stringArray[i];

                    // Assume that most of the time, the input argument is NOT quoted text:
                    string argument = inputArgument;

                    // Is the argument a quoted text string?
                    Regex regex = new Regex("\"(.*?)\"", RegexOptions.Singleline);
                    Match match = regex.Match(inputArgument);

                    // If it IS quoted, there will be at least one capture:
                    if (match.Captures.Count > 0)
                    {
                        // Get the unquoted text from within the quotes:
                        Regex captureQuotedText = new Regex("[^\"]*[^\"]");
                        Match quoted = captureQuotedText.Match(match.Captures[0].Value);

                        // The argument should include all text from between the quotes
                        // as a single string:
                        argument = quoted.Captures[0].Value;
                    }

                    _arguments.Add(argument);
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>The <see cref="Arguments" />.</summary>
        public IEnumerable<string> Arguments
        {
            get
            {
                return _arguments;
            }
        }

        /// <summary>The <see cref="LibraryClassName" />.</summary>
        public string LibraryClassName { get; set; }

        /// <summary>The <see cref="Name" />.</summary>
        public string Name { get; set; }

        #endregion
    }
}