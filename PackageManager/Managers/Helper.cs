namespace PackageManager.Managers
{
    #region Namespace

    using System.Windows.Forms;

    #endregion

    internal class Helper
    {
        #region Events

        /// <summary>Determine whether the item is in the collection.</summary>
        /// <param name="listViewItem">The item.</param>
        /// <param name="listView">The collection.</param>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        public static bool IsItemInCollection(ListViewItem listViewItem, ListView listView)
        {
            foreach (ListViewItem item in listView.Items)
            {
                var _subItemFlag = true;
                for (var i = 0; i < item.SubItems.Count; i++)
                {
                    string sub1 = item.SubItems[i].Text;
                    string sub2 = listViewItem.SubItems[i].Text;

                    if (sub1 != sub2)
                    {
                        _subItemFlag = false;
                    }
                }

                if (_subItemFlag)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}