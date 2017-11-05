namespace Comet.Structure
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml.Linq;

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
        private string _download;
        private string _filename;
        private string _name;
        private List<string> _packageList;
        private string _release;
        private Version _version;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="Package" /> class.</summary>
        /// <param name="changeLog">The change Log.</param>
        /// <param name="download">The download.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="name">The name.</param>
        /// <param name="release">The release.</param>
        /// <param name="version">The version.</param>
        public Package(string changeLog, string download, string filename, string name, string release, Version version) : this()
        {
            Update(changeLog, download, filename, name, release, version);
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
            Update(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, new Version(0, 0, 0, 0));
        }

        /// <summary>Initializes a new instance of the <see cref="Package" /> class.</summary>
        /// <param name="url">The url.</param>
        public Package(string url)
        {
            Load(url);
        }

        /// <summary>Initializes a new instance of the <see cref="Package" /> class.</summary>
        /// <param name="path">The file path.</param>
        /// <param name="encoding">The encoding.</param>
        public Package(string path, Encoding encoding)
        {
            Load(path, encoding);
        }

        /// <summary>The package data.</summary>
        public enum PackageData
        {
            /// <summary>The change log.</summary>
            ChangeLog = 0,

            /// <summary>The download.</summary>
            Download = 1,

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

        /// <summary>The <see cref="Download"></see> information.</summary>
        public string Download
        {
            get
            {
                return _download;
            }

            set
            {
                _download = value;
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
                return string.IsNullOrEmpty(_changeLog) && string.IsNullOrEmpty(_download) && string.IsNullOrEmpty(_filename) && string.IsNullOrEmpty(_name) && string.IsNullOrEmpty(_release);
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
        public string Release
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

        /// <summary>The <see cref="List{T}"></see> is from a <see cref="Package"></see>.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public List<string> ToList
        {
            get
            {
                var _list = new List<string>
                    {
                        _changeLog,
                        _download,
                        _filename,
                        _name,
                        _release,
                        _version.ToString()
                    };
                return _list;
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
            Update(package.ChangeLog, package.Download, package.Filename, package.Name, package.Release, package.Version);
        }

        /// <summary>Get the index by the item.</summary>
        /// <param name="item">The item string.</param>
        /// <returns>The <see cref="int" />.</returns>
        public int GetIndexByItem(string item)
        {
            return (int)Enum.Parse(typeof(PackageData), item);
        }

        /// <summary>Get the item by the index.</summary>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="string" />.</returns>
        public string GetItemByIndex(int index)
        {
            return _packageList[index];
        }

        /// <summary>Load a package from a url.</summary>
        /// <param name="url">The url.</param>
        public void Load(string url)
        {
            try
            {
                XDocument _xPackage = XDocument.Load(url);
                Deserialize(_xPackage);
            }
            catch (WebException)
            {
                ExceptionManager.ShowSourceNotFoundException(url);
            }
            catch (Exception e)
            {
                ExceptionManager.WriteException(e.Message);
            }
        }

        /// <summary>Load a package from filename.</summary>
        /// <param name="path">The file path</param>
        /// <param name="encoding">The encoding.</param>
        public void Load(string path, Encoding encoding)
        {
            try
            {
                XDocument _packageFile = XDocument.Parse(File.ReadAllText(path, encoding));
                Deserialize(_packageFile);
            }
            catch (FileNotFoundException)
            {
                ExceptionManager.ShowFileNotFoundException(path);
            }
            catch (Exception e)
            {
                ExceptionManager.WriteException(e.Message);
            }
        }

        /// <summary>Saves the package to file.</summary>
        /// <param name="path">The file path.</param>
        /// <param name="saveOptions">The save options.</param>
        public void Save(string path, SaveOptions saveOptions)
        {
            try
            {
                XDocument _packageFile = new XDocument(new XElement(
                    Application.ProductName,
                    new XElement(Enum.GetName(typeof(PackageData), 0), _changeLog),
                    new XElement(Enum.GetName(typeof(PackageData), 1), _download),
                    new XElement(Enum.GetName(typeof(PackageData), 2), _filename),
                    new XElement(Enum.GetName(typeof(PackageData), 3), _name),
                    new XElement(Enum.GetName(typeof(PackageData), 4), _release),
                    new XElement(Enum.GetName(typeof(PackageData), 5), _version)));

                _packageFile.Save(path, saveOptions);
            }
            catch (Exception e)
            {
                ExceptionManager.WriteException(e.Message);
            }
        }

        /// <summary>Update the package information.</summary>
        /// <param name="changeLog">The change Log.</param>
        /// <param name="download">The download.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="name">The name.</param>
        /// <param name="release">The release.</param>
        /// <param name="version">The version.</param>
        public void Update(string changeLog, string download, string filename, string name, string release, Version version)
        {
            _changeLog = changeLog;
            _download = download;
            _filename = filename;
            _name = name;
            _release = release;
            _version = version;

            _packageList = new List<string> { _changeLog, _download, _filename, _name, _release, _version.ToString() };
        }

        /// <summary>Deserialize the package.</summary>
        /// <param name="package">The package.</param>
        private void Deserialize(XContainer package)
        {
            var changeLogElement = package.Descendants("ChangeLog");
            var downloadElement = package.Descendants("Download");
            var filenameElement = package.Descendants("Filename");
            var nameElement = package.Descendants("Name");
            var releaseDateElement = package.Descendants("Release");
            var versionElement = package.Descendants("Version");

            Update(
                string.Concat(changeLogElement.Nodes()),
                string.Concat(downloadElement.Nodes()),
                string.Concat(filenameElement.Nodes()),
                string.Concat(nameElement.Nodes()),
                string.Concat(releaseDateElement.Nodes()),
                new Version(string.Concat(versionElement.Nodes())));
        }

        #endregion
    }
}