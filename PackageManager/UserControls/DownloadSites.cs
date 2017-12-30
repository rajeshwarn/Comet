namespace PackageManager.UserControls
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;

    using Comet.Controls;
    using Comet.Managers;

    using PackageManager.Managers;

    #endregion

    /// <summary>The download sites UserControl page.</summary>
    [ToolboxItem(false)]
    public partial class DownloadSites : UserControl
    {
        #region Variables

        private List<Uri> _urlList;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DownloadSites" /> class.
        /// </summary>
        public DownloadSites()
        {
            InitializeComponent();
            ControlPanel.WriteLog($"Initializing Download Sites Panel");

            _urlList = new List<Uri>();
            CbUrlScheme.SelectedIndex = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Get the downloads list.
        /// </summary>
        public List<Uri> DownloadsList
        {
            get
            {
                return _urlList;
            }
        }

        #endregion

        #region Events

        /// <summary>
        ///     Import the package downloads to the list view.
        /// </summary>
        /// <param name="downloads">The downloads.</param>
        internal void ImportPackageDownloads(List<Uri> downloads)
        {
            try
            {
                foreach (Uri _download in downloads)
                {
                    ListViewItem _item = new ListViewItem(_download.OriginalString);
                    _item.SubItems.Add(NetworkManager.SourceExists(_download.OriginalString).ToString());

                    if (Helper.IsItemInCollection(_item, ListViewUrlList))
                    {
                        return;
                    }

                    ListViewUrlList.Items.Add(_item);
                }

                UpdateDownloadsList();
            }
            catch (Exception e)
            {
                VisualExceptionDialog.Show(e);
            }
        }

        /// <summary>
        ///     Button add Url.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxUrl.Text) || !NetworkManager.IsURLFormatted(WebsiteUrl()))
            {
                return;
            }

            ListViewItem _item = new ListViewItem(WebsiteUrl());
            _item.SubItems.Add(NetworkManager.SourceExists(WebsiteUrl()).ToString());

            if (Helper.IsItemInCollection(_item, ListViewUrlList))
            {
                return;
            }

            ListViewUrlList.Items.Add(_item);
            UpdateDownloadsList();
            TextBoxUrl.Clear();
        }

        /// <summary>
        ///     Tool strip menu item refresh.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem _item in ListViewUrlList.SelectedItems)
            {
                bool _sourceExists = NetworkManager.SourceExists(_item.Text);
                _item.SubItems[1].Text = _sourceExists.ToString();
            }
        }

        /// <summary>
        ///     Tool strip menu item remove.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ListViewUrlList.SelectedItems.Count <= 0)
            {
                return;
            }

            foreach (ListViewItem _item in ListViewUrlList.SelectedItems)
            {
                ListViewUrlList.Items.Remove(_item);
            }

            UpdateDownloadsList();
        }

        /// <summary>
        ///     Tool strip menu item refresh.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void SelectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewUrlList.Items)
            {
                item.Selected = true;
            }
        }

        /// <summary>
        ///     Update the url downloads list.
        /// </summary>
        private void UpdateDownloadsList()
        {
            _urlList.Clear();

            foreach (ListViewItem _item in ListViewUrlList.Items)
            {
                _urlList.Add(new Uri(_item.Text));
            }
        }

        /// <summary>
        ///     Retrieves the website url.
        /// </summary>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        private string WebsiteUrl()
        {
            return CbUrlScheme.Text + TextBoxUrl.Text;
        }

        #endregion
    }
}