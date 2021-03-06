﻿namespace PackageManager.Managers
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using System.Xml.Linq;

    using Comet.Controls;
    using Comet.Managers;

    using PackageManager.Structure;

    #endregion

    public class HistoryManager
    {
        #region Variables

        public EventHandler ToolStripMenuItemClicked;

        #endregion

        #region Variables

        private List<HistoryLogEntry> _historyLogs;
        private string _logFile;
        private int _maximumHistory;
        private ToolStripMenuItem _toolStripMenuItem;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="HistoryManager" /> class.</summary>
        /// <param name="toolStripMenuItem">The tool strip menu item.</param>
        /// <param name="logFile">The file path.</param>
        /// <param name="maximum">The maximum history count.</param>
        public HistoryManager(ToolStripMenuItem toolStripMenuItem, string logFile, int maximum) : this()
        {
            _toolStripMenuItem = toolStripMenuItem;
            _logFile = logFile;
            _maximumHistory = maximum;
            Load();
            UpdateMenu();
        }

        /// <summary>Initializes a new instance of the <see cref="HistoryManager" /> class.</summary>
        public HistoryManager()
        {
            _historyLogs = new List<HistoryLogEntry>();
            _maximumHistory = 7;
            _logFile = null;
            _toolStripMenuItem = null;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the history log entries count.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public int Count
        {
            get
            {
                return _historyLogs.Count;
            }
        }

        /// <summary>
        ///     Gets or sets the history log entries.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<HistoryLogEntry> HistoryLog
        {
            get
            {
                return _historyLogs;
            }

            set
            {
                _historyLogs = value;
            }
        }

        /// <summary>
        ///     Gets or sets the hooked tool strip menu item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ToolStripMenuItem HookedToolStripMenuItem
        {
            get
            {
                return _toolStripMenuItem;
            }

            set
            {
                _toolStripMenuItem = value;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the history manager is empty.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsEmpty
        {
            get
            {
                return _historyLogs.Count <= 0;
            }
        }

        /// <summary>
        ///     Gets or sets the file stream for the history log file.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string LogFile
        {
            get
            {
                return _logFile;
            }

            set
            {
                _logFile = value;
            }
        }

        /// <summary>
        ///     Gets or sets the maximum history log entries.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public int MaximumHistory
        {
            get
            {
                return _maximumHistory;
            }

            set
            {
                _maximumHistory = value;
            }
        }

        #endregion

        #region Events

        /// <summary>
        ///     Add the file to the history.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public void Add(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            if (_historyLogs.Count < _maximumHistory)
            {
                HistoryLogEntry _logEntry = new HistoryLogEntry(filePath);

                bool _duplicateExists = Exists(_logEntry);

                if (!_duplicateExists)
                {
                    _historyLogs.Add(_logEntry);
                }
            }
            else
            {
                if (Exists(new HistoryLogEntry(filePath)))
                {
                    return;
                }

                var _list = _historyLogs.Select(_historyLog => _historyLog.DateModified).ToList();
                DateTime _minimumDateTimeModified = _list.Min();

                foreach (HistoryLogEntry _historyLogEntry in _historyLogs)
                {
                    if (_historyLogEntry.DateModified == _minimumDateTimeModified)
                    {
                        Remove(_historyLogEntry);
                    }
                }
            }
        }

        /// <summary>
        ///     Clears all history.
        /// </summary>
        public void ClearHistory()
        {
            _historyLogs = new List<HistoryLogEntry>();
            UpdateMenu();
        }

        /// <summary>
        ///     Gets a value determined whether the history contains the log entry.
        /// </summary>
        /// <param name="historyLogEntry">The history log entry.</param>
        /// <returns>
        ///     <see cref="HistoryLogEntry" />
        /// </returns>
        public bool Exists(HistoryLogEntry historyLogEntry)
        {
            var _duplicateExists = false;
            foreach (HistoryLogEntry _logs in _historyLogs)
            {
                if (_logs.Equals(historyLogEntry))
                {
                    _duplicateExists = true;
                }
            }

            return _duplicateExists;
        }

        /// <summary>
        ///     Loads the history log file.
        /// </summary>
        public void Load()
        {
            if (string.IsNullOrEmpty(_logFile))
            {
                throw new NoNullAllowedException(StringManager.IsNullOrEmpty(_logFile));
            }

            try
            {
                if (File.Exists(_logFile))
                {
                    XDocument _historyLogFile = XDocument.Load(_logFile);
                    Deserialize(_historyLogFile);
                }
                else
                {
                    Save();
                }
            }
            catch (Exception e)
            {
                VisualExceptionDialog.Show(e);
            }
        }

        /// <summary>
        ///     Removes the history entry log from the list.
        /// </summary>
        /// <param name="historyLogEntry">The history log entry.</param>
        public void Remove(HistoryLogEntry historyLogEntry)
        {
            _historyLogs.Remove(historyLogEntry);
        }

        /// <summary>
        ///     Removes the element of the specified index of the list./>
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveAt(int index)
        {
            _historyLogs.RemoveAt(index);
        }

        /// <summary>
        ///     Saves the history to log file.
        /// </summary>
        public void Save()
        {
            try
            {
                XDocument _historyLogFile = new XDocument();

                XElement _headerElement = new XElement("HistoryLogs");
                _historyLogFile.Add(_headerElement);

                foreach (HistoryLogEntry _historyLog in _historyLogs)
                {
                    XElement _historyLogElement = new XElement("LogEntry");
                    _historyLogElement.Add(new XElement("FilePath", _historyLog.FilePath));
                    _historyLogElement.Add(new XElement("DateModified", _historyLog.DateModified.ToLongDateString()));

                    _headerElement.Add(_historyLogElement);
                }

                _historyLogFile.Save(_logFile, SaveOptions.None);
            }
            catch (Exception e)
            {
                VisualExceptionDialog.Show(e);
            }
        }

        /// <summary>
        ///     Update the tool strip menu item.
        /// </summary>
        public void UpdateMenu()
        {
            _toolStripMenuItem.DropDownItems.Clear();

            _toolStripMenuItem.DropDownItems.Add(CreateClearHistoryLogItem());
            _toolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            foreach (HistoryLogEntry _historyLogEntry in _historyLogs)
            {
                ToolStripItem _item = CreateLogItem(@"toolStripMenuItem" + 0, _historyLogEntry.FilePath, _historyLogEntry.FileName);
                _toolStripMenuItem.DropDownItems.Add(_item);
            }
        }

        /// <summary>
        ///     Clear history tool strip menu item clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ClearHistory_Click(object sender, EventArgs e)
        {
            ClearHistory();
            UpdateMenu();
            Save();
        }

        /// <summary>
        ///     Create clear history tool strip menu item.
        /// </summary>
        /// <returns>
        ///     <see cref="ToolStripMenuItem" />
        /// </returns>
        private ToolStripItem CreateClearHistoryLogItem()
        {
            string _text = $@"Clear all history entries ({_historyLogs.Count})";

            ToolStripItem _toolStripMenuItemClearHistory = new ToolStripMenuItem(_text)
                {
                    Name = @"ClearHistoryToolStripItem",
                    Tag = null
                };

            _toolStripMenuItemClearHistory.Click += ClearHistory_Click;
            return _toolStripMenuItemClearHistory;
        }

        /// <summary>
        ///     Creates a tool strip menu item.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <see cref="ToolStripMenuItem" />
        /// </returns>
        private ToolStripItem CreateLogItem(string name, object tag, string text)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(StringManager.IsNullOrEmpty(name));
            }

            if (tag == null)
            {
                throw new ArgumentNullException(StringManager.IsNullOrEmpty(name));
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(StringManager.IsNullOrEmpty(name));
            }

            ToolStripItem _toolStripItem = new ToolStripMenuItem(text)
                {
                    Tag = tag,
                    Name = name
                };

            _toolStripItem.Click += LogEntryToolStripMenuItem_Click;

            return _toolStripItem;
        }

        /// <summary>
        ///     Deserialize the history log file.
        /// </summary>
        /// <param name="logFile">The file path.</param>
        private void Deserialize(XContainer logFile)
        {
            try
            {
                _historyLogs = new List<HistoryLogEntry>();

                foreach (XElement _historyLogEntries in logFile.Descendants("LogEntry"))
                {
                    var _entryFilePath = _historyLogEntries.Descendants("FilePath").Nodes();
                    string _file = string.Concat(_entryFilePath);

                    // var _dateModified = _historyLogEntries.Descendants("DateModified").Nodes();
                    // string _dateTime = string.Concat(_dateModified);
                    if (File.Exists(_file))
                    {
                        HistoryLogEntry _historyLogEntry = new HistoryLogEntry(_file);

                        if (!Exists(_historyLogEntry))
                        {
                            _historyLogs.Add(_historyLogEntry);
                        }
                    }
                }

                UpdateMenu();
            }
            catch (Exception e)
            {
                VisualExceptionDialog.Show(e);
            }
        }

        /// <summary>
        ///     Occurs when the tool strip menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void LogEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _toolStripMenuItemClicked = (ToolStripMenuItem)sender;

            string _clickedFilePath = _toolStripMenuItemClicked.Tag.ToString();

            if (File.Exists(_clickedFilePath))
            {
                ToolStripMenuItemClicked?.Invoke(sender, e);
            }
            else
            {
                Remove(new HistoryLogEntry(_clickedFilePath));
            }
        }

        #endregion
    }
}