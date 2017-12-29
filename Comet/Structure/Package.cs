namespace Comet.Structure
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml.Linq;

    using Comet.Controls;
    using Comet.Managers;

    #endregion

    /// <summary>The package structure.</summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [Description("The Package structure.")]
    [DesignerCategory("code")]
    public class Package
    {
        #region Variables

        private string _changeLog;
        private List<Uri> _downloads;
        private string _filename;
        private string _name;
        private DateTime _release;
        private Version _version;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="Package" /> class.</summary>
        /// <param name="changeLog">The change Log.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="name">The name.</param>
        /// <param name="release">The release.</param>
        /// <param name="version">The version.</param>
        /// <param name="downloads">The download.</param>
        public Package(string changeLog, string filename, string name, string release, Version version, List<Uri> downloads) : this()
        {
            Update(changeLog, filename, name, release, version, downloads);
        }

        /// <summary>Initializes a new instance of the <see cref="Package" /> class.</summary>
        /// <param name="package">The package.</param>
        public Package(Package package)
        {
            Clone(package);
        }

        /// <summary>Initializes a new instance of the <see cref="Package" /> class.</summary>
        public Package()
        {
            Update(string.Empty, string.Empty, string.Empty, DateTime.Today.ToString(CultureInfo.CurrentCulture), new Version(0, 0, 0, 0), new List<Uri>());
        }

        /// <summary>Initializes a new instance of the <see cref="Package" /> class.</summary>
        /// <param name="uri">The uri.</param>
        public Package(Uri uri)
        {
            Load(uri);
        }

        /// <summary>Initializes a new instance of the <see cref="Package" /> class.</summary>
        /// <param name="filePath">The file path.</param>
        public Package(string filePath)
        {
            Load(filePath);
        }

        /// <summary>Initializes a new instance of the <see cref="Package" /> class.</summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="encoding">The encoding.</param>
        public Package(string filePath, Encoding encoding)
        {
            Load(filePath, encoding);
        }

        /// <summary>The package data.</summary>
        public enum PackageData
        {
            /// <summary>The change log.</summary>
            ChangeLog = 0,

            /// <summary>The downloads.</summary>
            Downloads = 1,

            /// <summary>The filename.</summary>
            Filename = 2,

            /// <summary>The name.</summary>
            Name = 3,

            /// <summary>The release.</summary>
            Release = 4,

            /// <summary>The version.</summary>
            Version = 5
        }

        #endregion

        #region Properties

        /// <summary>The <see cref="ChangeLog"></see> information.</summary>
        public string ChangeLog
        {
            get
            {
                return _changeLog;
            }

            set
            {
                _changeLog = value;
            }
        }

        /// <summary>
        ///     The <see cref="Count"></see> determines the amount of <see cref="PackageData"></see> that is in the
        ///     <see cref="Package"></see>.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public int Count
        {
            get
            {
                return Enum.GetNames(typeof(PackageData)).Length;
            }
        }

        /// <summary>The downloads list</summary>
        public List<Uri> Downloads
        {
            get
            {
                return _downloads;
            }

            set
            {
                _downloads = value;
            }
        }

        /// <summary>The <see cref="Filename"></see> information.</summary>
        public string Filename
        {
            get
            {
                return _filename;
            }

            set
            {
                _filename = value;
            }
        }

        /// <summary>The <see cref="bool"></see> determines whether the <see cref="Package"></see> is empty.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(_changeLog) && (_downloads.Count <= 0) && string.IsNullOrEmpty(_filename) && string.IsNullOrEmpty(_name);
            }
        }

        /// <summary>The <see cref="Name"></see> information.</summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        /// <summary>The <see cref="Release"></see> information.</summary>
        public DateTime Release
        {
            get
            {
                return _release;
            }

            set
            {
                _release = value;
            }
        }

        /// <summary>The <see cref="Version"></see> information.</summary>
        public Version Version
        {
            get
            {
                return _version;
            }

            set
            {
                _version = value;
            }
        }

        #endregion

        #region Events

        /// <summary>Clone the package information from another package to the current one.</summary>
        /// <param name="package">The package to clone.</param>
        public void Clone(Package package)
        {
            Update(package.ChangeLog, package.Filename, package.Name, package.Release.ToString(CultureInfo.CurrentCulture), package.Version, package.Downloads);
        }

        /// <summary>Get the index by the item.</summary>
        /// <param name="item">The item string.</param>
        /// <returns>The <see cref="int" />.</returns>
        public int GetIndexByItem(string item)
        {
            return (int)Enum.Parse(typeof(PackageData), item);
        }

        /// <summary>Load a package from a uri.</summary>
        /// <param name="uri">The uri.</param>
        public void Load(Uri uri)
        {
            try
            {
                if (string.IsNullOrEmpty(uri.OriginalString))
                {
                    throw new NoNullAllowedException(StringManager.IsNullOrEmpty(uri.OriginalString));
                }

                if (!NetworkManager.IsURLFormatted(uri.OriginalString))
                {
                    throw new UriFormatException(StringManager.UrlNotWellFormatted(uri.OriginalString));
                }

                if (NetworkManager.InternetAvailable)
                {
                    XDocument _xmlPackageDocument = XDocument.Load(uri.OriginalString);
                    Deserialize(_xmlPackageDocument);

                    // Bug: Gets thrown on slow connection.
                    // if (!NetworkManager.SourceExists(url))
                    // {
                    // throw new FileNotFoundException(StringManager.PackageNotFound(url));
                    // }
                    // else
                    // {
                    // // Load from url
                    // XDocument _xPackage = XDocument.Load(url);
                    // Deserialize(_xPackage);
                    // }
                }
            }
            catch (WebException)
            {
                VisualExceptionDialog.Show(new FileNotFoundException(StringManager.RemoteFileNotFound(uri.OriginalString)));
            }
            catch (Exception e)
            {
                VisualExceptionDialog.Show(e);
            }
        }

        /// <summary>Load a package from a file path.</summary>
        /// <param name="filePath">The file path.</param>
        public void Load(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new NoNullAllowedException(StringManager.IsNullOrEmpty(filePath));
                }

                if (File.Exists(filePath))
                {
                    XDocument _xmlPackageDocument = XDocument.Load(filePath);
                    Deserialize(_xmlPackageDocument);
                }
                else
                {
                    VisualExceptionDialog.Show(new FileNotFoundException(StringManager.FileNotFound(filePath)));
                }
            }
            catch (WebException)
            {
                VisualExceptionDialog.Show(new FileNotFoundException(StringManager.RemoteFileNotFound(filePath)));
            }
            catch (Exception e)
            {
                VisualExceptionDialog.Show(e);
            }
        }

        /// <summary>Load a package from filename.</summary>
        /// <param name="path">The file path</param>
        /// <param name="encoding">The encoding.</param>
        public void Load(string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new NoNullAllowedException(StringManager.IsNullOrEmpty(path));
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(StringManager.FileNotFound(path));
            }

            try
            {
                XDocument _packageFile = XDocument.Parse(File.ReadAllText(path, encoding));
                Deserialize(_packageFile);
            }
            catch (Exception e)
            {
                VisualExceptionDialog.Show(e);
            }
        }

        /// <summary>Saves the package to file.</summary>
        /// <param name="path">The file path.</param>
        /// <param name="saveOptions">The save options.</param>
        public void Save(string path, SaveOptions saveOptions = SaveOptions.DisableFormatting)
        {
            try
            {
                XElement _downloadsElement = new XElement(Enum.GetName(typeof(PackageData), 1));

                foreach (Uri _url in _downloads)
                {
                    XElement _urlElement = new XElement("Link", _url.OriginalString);
                    _downloadsElement.Add(_urlElement);
                }

                XDocument _packageFile = new XDocument(new XElement(
                    @"Comet",
                    new XElement(Enum.GetName(typeof(PackageData), 0), _changeLog),
                    new XElement(Enum.GetName(typeof(PackageData), 2), _filename),
                    new XElement(Enum.GetName(typeof(PackageData), 3), _name),
                    new XElement(Enum.GetName(typeof(PackageData), 4), _release.Date.ToShortDateString()),
                    new XElement(Enum.GetName(typeof(PackageData), 5), _version),
                    _downloadsElement));

                _packageFile.Save(path, saveOptions);
            }
            catch (Exception e)
            {
                VisualExceptionDialog.Show(e);
            }
        }

        /// <summary>Update the package information.</summary>
        /// <param name="changeLog">The change Log.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="name">The name.</param>
        /// <param name="release">The release.</param>
        /// <param name="version">The version.</param>
        /// <param name="downloads">The download.</param>
        public void Update(string changeLog, string filename, string name, string release, Version version, List<Uri> downloads)
        {
            _changeLog = changeLog;
            _filename = filename;
            _name = name;
            _release = Convert.ToDateTime(release);
            _version = version;
            _downloads = downloads;
        }

        /// <summary>Deserialize the package.</summary>
        /// <param name="package">The package.</param>
        private void Deserialize(XContainer package)
        {
            var changeLogElement = package.Descendants("ChangeLog");

            var _downloadList = new List<Uri>();

            foreach (XElement _innerElements in package.Descendants("Downloads").Elements())
            {
                _downloadList.Add(new Uri(_innerElements.Value));
            }

            var filenameElement = package.Descendants("Filename");
            var nameElement = package.Descendants("Name");
            var releaseDateElement = package.Descendants("Release");
            var versionElement = package.Descendants("Version");

            Update(
                string.Concat(changeLogElement.Nodes()),
                string.Concat(filenameElement.Nodes()),
                string.Concat(nameElement.Nodes()),
                string.Concat(releaseDateElement.Nodes()),
                new Version(string.Concat(versionElement.Nodes())),
                _downloadList);
        }

        #endregion
    }
}