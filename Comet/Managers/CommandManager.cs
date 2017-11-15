namespace Comet.Managers
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using Comet.Localize;

    #endregion

    /// <summary>The <see cref="CommandManager" /> class.</summary>
    public class CommandManager
    {
        #region Variables

        private Dictionary<string, string> _commandDictionary;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="CommandManager" /> class.</summary>
        public CommandManager()
        {
            _commandDictionary = new Dictionary<string, string>
                {
                    { "Check", Descriptions.CommandDescriptions[0] },
                    { "Clear", Descriptions.CommandDescriptions[1] },
                    { "Connect", Descriptions.CommandDescriptions[2] },
                    { "Download", Descriptions.CommandDescriptions[3] },
                    { "Edit", Descriptions.CommandDescriptions[4] },
                    { "Exit", Descriptions.CommandDescriptions[5] },
                    { "Get", Descriptions.CommandDescriptions[6] },
                    { "Help", Descriptions.CommandDescriptions[7] },
                    { "Info", Descriptions.CommandDescriptions[8] },
                    { "Net", Descriptions.CommandDescriptions[9] },
                    { "New", Descriptions.CommandDescriptions[10] },
                    { "Open", Descriptions.CommandDescriptions[11] },
                    { "Read", Descriptions.CommandDescriptions[12] },
                    { "Save", Descriptions.CommandDescriptions[13] },
                    { "Unload", Descriptions.CommandDescriptions[14] }
                };
        }

        /// <summary>Initializes a new instance of the <see cref="CommandManager" /> class.</summary>
        /// <param name="commandDictionary">The command dictionary.</param>
        public CommandManager(Dictionary<string, string> commandDictionary)
        {
            _commandDictionary = commandDictionary;
        }

        /// <summary>The <see cref="CommandData"></see>.</summary>
        public enum CommandData
        {
            /// <summary>The command.</summary>
            Command,

            /// <summary>The description.</summary>
            Description
        }

        #endregion

        #region Properties

        /// <summary>The <see cref="CommandsDictionary" />.</summary>
        public Dictionary<string, string> CommandsDictionary
        {
            get
            {
                return _commandDictionary;
            }
        }

        /// <summary>The <see cref="int"></see> determines the <see cref="CommandManager"></see> count.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int Count
        {
            get
            {
                return _commandDictionary.Count;
            }
        }

        /// <summary>The <see cref="bool"></see> determines whether the <see cref="CommandManager"></see> is empty.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsEmpty
        {
            get
            {
                return _commandDictionary.Count == 0;
            }
        }

        #endregion

        #region Events

        /// <summary>Get data by index.</summary>
        /// <param name="index">The index.</param>
        /// <param name="type">The data type.</param>
        /// <returns>The <see cref="string" />.</returns>
        public string GetDataByIndex(int index, CommandData type)
        {
            switch (type)
            {
                case CommandData.Command:
                    {
                        return _commandDictionary.Keys.ElementAt(index);
                    }

                case CommandData.Description:
                    {
                        return _commandDictionary.Values.ElementAt(index);
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                    }
            }
        }

        #endregion
    }
}