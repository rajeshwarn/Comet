namespace Comet.Commands
{
    #region Namespace

    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Xml.Linq;

    using Comet.Localize;
    using Comet.Managers;
    using Comet.Structure;

    #endregion

    /// <summary>The default commands.</summary>
    public static class DefaultCommands
    {
        #region Events

        /// <summary>Check for an update.</summary>
        /// <param name="executablePath">The path to the executable.</param>
        /// <param name="packagePath">The path to the package source.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Check(string executablePath = "", string packagePath = "")
        {
            if (string.IsNullOrEmpty(packagePath) || !NetworkManager.SourceExists(packagePath) || string.IsNullOrEmpty(executablePath))
            {
                ConsoleManager.WriteOutput(Descriptions.CommandDescriptions[0]);
                ConsoleManager.WriteOutput("Usage: Check [executablePath] [packagePath]");
                Console.WriteLine();
            }
            else
            {
                try
                {
                    PackageManager.WorkingPath = packagePath;
                    PackageManager.WorkingPackage = new Package(packagePath);
                    Version _currentVersion = ApplicationManager.GetFileVersion(executablePath);

                    ConsoleManager.WriteOutput("Current version: " + _currentVersion);
                    ConsoleManager.WriteOutput("Latest version: " + PackageManager.WorkingPackage.Version);

                    bool _updateRequired = ApplicationManager.CompareVersion(_currentVersion, PackageManager.WorkingPackage.Version);

                    string _updateMessage;
                    if (_updateRequired)
                    {
                        _updateMessage = "You don't have the latest version.";
                    }
                    else
                    {
                        _updateMessage = "You have the latest version.";
                    }

                    ConsoleManager.WriteOutput(_updateMessage);
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    ExceptionManager.WriteException(e.Message);
                }

                return string.Empty;
            }

            return string.Empty;
        }

        /// <summary>Clears the console.</summary>
        public static void Clear()
        {
            Console.Clear();
        }

        /// <summary>Compress the directory.</summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="output">The output file.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Compress(string directoryPath = "", string output = "")
        {
            if (string.IsNullOrEmpty(directoryPath) && string.IsNullOrEmpty(output))
            {
                ConsoleManager.WriteOutput(Descriptions.CommandDescriptions[16]);
                ConsoleManager.WriteOutput("Usage: Compress [directoryPath] [output]");
                Console.WriteLine();
            }
            else
            {
                if (string.IsNullOrEmpty(directoryPath))
                {
                    ExceptionManager.ShowNullOrEmpty("The directory path is null or empty.");
                }

                if (!Directory.Exists(directoryPath))
                {
                    ExceptionManager.WriteException("The directory was not found.");
                }

                if (string.IsNullOrEmpty(output))
                {
                    ExceptionManager.ShowNullOrEmpty("The output is null or empty");
                }

                try
                {
                    CompressionManager.CompressDirectory(directoryPath, output, CompressionLevel.Optimal);
                    ConsoleManager.WriteOutput("The directory has been compressed.");
                    ConsoleManager.WriteOutput("Output: " + output);
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    ExceptionManager.WriteException(e.Message);
                }
            }

            return string.Empty;
        }

        /// <summary>Connects to a host.</summary>
        /// <param name="hostname">The hostname.</param>
        /// <param name="port">The port.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Connect(string hostname = "", int port = 80)
        {
            if (string.IsNullOrEmpty(hostname))
            {
                ConsoleManager.WriteOutput("Connects to a host.");
                ConsoleManager.WriteOutput("Usage: Connect [hostname] [port=80]");
                Console.Write(Environment.NewLine);
                return string.Empty;
            }
            else
            {
                bool _dnsConnection = NetworkManager.GetConnectionState(hostname);
                bool _tcpConnection = NetworkManager.GetConnectionState(hostname, port);

                StringBuilder _stringBuilder = new StringBuilder();
                _stringBuilder.AppendLine("Host: " + hostname + ":" + port);
                _stringBuilder.AppendLine("DNS: " + _dnsConnection);
                _stringBuilder.AppendLine("TCP: " + _tcpConnection);

                return _stringBuilder.ToString();
            }
        }

        /// <summary>Download a file.</summary>
        /// <param name="url">The url.</param>
        /// <param name="fileName">The file Name.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Download(string url = "", string fileName = "")
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(fileName))
            {
                ConsoleManager.WriteOutput("Downloads a file.");
                ConsoleManager.WriteOutput("Usage: Download [url] [filename]");
                Console.Write(Environment.NewLine);
                return string.Empty;
            }
            else
            {
                if (NetworkManager.IsURLFormatted(url))
                {
                    Thread _downloadThread = new Thread(() =>
                        {
                            ConsoleManager.WriteOutput("Download link: " + url);
                            ConsoleManager.WriteOutput("Download output: " + fileName);
                            Console.WriteLine();
                            NetworkManager.Download(new Uri(url), fileName);
                        });

                    _downloadThread.Start();
                }
                else
                {
                    ExceptionManager.WriteException("The url is not well formatted.");
                    Console.WriteLine();
                }
            }

            return string.Empty;
        }

        /// <summary> Edit the package.</summary>
        /// <param name="packageData">Package data to edit.</param>
        /// <param name="data">The data.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Edit(int packageData = 0, string data = "")
        {
            if ((packageData == 0) && string.IsNullOrEmpty(data))
            {
                ConsoleManager.WriteOutput(Descriptions.CommandDescriptions[3]);
                ConsoleManager.WriteOutput("Usage: Edit [#] [data]");
                ConsoleManager.WriteOutput("0 = ChangeLog");
                ConsoleManager.WriteOutput("1 = Download");
                ConsoleManager.WriteOutput("2 = Filename");
                ConsoleManager.WriteOutput("3 = Name");
                ConsoleManager.WriteOutput("4 = Release");
                ConsoleManager.WriteOutput("5 = Version");
                Console.Write(Environment.NewLine);
                return string.Empty;
            }
            else
            {
                try
                {
                    switch (packageData)
                    {
                        case (int)Package.PackageData.ChangeLog:
                            {
                                PackageManager.WorkingPackage.ChangeLog = data;
                                break;
                            }

                        case (int)Package.PackageData.Download:
                            {
                                PackageManager.WorkingPackage.Download = data;
                                break;
                            }

                        case (int)Package.PackageData.Filename:
                            {
                                PackageManager.WorkingPackage.Filename = data;
                                break;
                            }

                        case (int)Package.PackageData.Name:
                            {
                                PackageManager.WorkingPackage.Name = data;
                                break;
                            }

                        case (int)Package.PackageData.Release:
                            {
                                PackageManager.WorkingPackage.Release = data;
                                break;
                            }

                        case (int)Package.PackageData.Version:
                            {
                                PackageManager.WorkingPackage.Version = new Version(data);
                                break;
                            }

                        default:
                            {
                                throw new ArgumentOutOfRangeException(nameof(packageData), packageData, null);
                            }
                    }

                    ConsoleManager.WriteOutput("Updated: " + (Package.PackageData)packageData);
                    ConsoleManager.WriteOutput("Data: " + data);

                    Console.Write(Environment.NewLine);
                    return string.Empty;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return string.Empty;
                }
            }
        }

        /// <summary>Exits the application.</summary>
        public static void Exit()
        {
            Environment.Exit(0);
        }

        /// <summary>Extract the archive to directory.</summary>
        /// <param name="archivePath">The archive path.</param>
        /// <param name="output">The output file.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Extract(string archivePath = "", string output = "")
        {
            if (string.IsNullOrEmpty(archivePath) && string.IsNullOrEmpty(output))
            {
                ConsoleManager.WriteOutput(Descriptions.CommandDescriptions[15]);
                ConsoleManager.WriteOutput("Usage: Extract [archivePath] [output]");
                Console.WriteLine();
            }
            else
            {
                if (string.IsNullOrEmpty(archivePath))
                {
                    ExceptionManager.ShowNullOrEmpty("The archive path is null or empty.");
                }

                if (!File.Exists(archivePath))
                {
                    ExceptionManager.WriteException("The archive was not found.");
                }

                if (string.IsNullOrEmpty(output))
                {
                    ExceptionManager.ShowNullOrEmpty("The output is null or empty");
                }

                try
                {
                    CompressionManager.ExtractToDirectory(archivePath, output);
                    ConsoleManager.WriteOutput("The directory has been extracted.");
                    ConsoleManager.WriteOutput("Output: " + output);
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    ExceptionManager.WriteException(e.Message);
                }
            }

            return string.Empty;
        }

        /// <summary>Get the status code from the url.</summary>
        /// <param name="url">The url.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Get(string url = "")
        {
            if (string.IsNullOrEmpty(url))
            {
                ConsoleManager.WriteOutput("Get a status code from the url.");
                ConsoleManager.WriteOutput("Usage: Get [url]");
                ConsoleManager.WriteOutput("Example: Get http://www.example.com/");
                Console.Write(Environment.NewLine);
                return string.Empty;
            }
            else
            {
                HttpStatusCode _statusCode = NetworkManager.GetStatusCode(url);
                var _statusCodeValue = (int)NetworkManager.GetStatusCode(url);

                ConsoleManager.WriteOutput("HTTP status code: " + _statusCode + " (" + _statusCodeValue + ")");
                ConsoleManager.WriteOutput("Source exists: " + NetworkManager.SourceExists(url));
                Console.Write(Environment.NewLine);
                return string.Empty;
            }
        }

        /// <summary>Displays help commands.</summary>
        public static void Help()
        {
            CommandManager _commandManager = new CommandManager();

            for (var i = 0; i < _commandManager.Count; i++)
            {
                string _command = _commandManager.GetDataByIndex(i, CommandManager.CommandData.Command).PadRight(10);
                string _description = "-".PadRight(5) + _commandManager.GetDataByIndex(i, CommandManager.CommandData.Description);
                ConsoleManager.WriteOutput(_command + _description);
            }

            Console.Write(Environment.NewLine);
        }

        /// <summary>Displays details about the working package file.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public static string Info()
        {
            if ((PackageManager.WorkingPackage == null) || string.IsNullOrWhiteSpace(PackageManager.WorkingPath))
            {
                ConsoleManager.WriteOutput(Descriptions.CommandDescriptions[7]);
                ConsoleManager.WriteOutput("Usage: Info");
                Console.WriteLine();
                ConsoleManager.WriteOutput("No package has been loaded.");
                Console.WriteLine();
            }
            else
            {
                try
                {
                    string _file = Path.GetFileName(PackageManager.WorkingPath);
                    string _path = Path.GetFullPath(PackageManager.WorkingPath);
                    string _directory = Path.GetDirectoryName(_path);

                    ConsoleManager.WriteOutput("Package Information:");
                    ConsoleManager.WriteOutput("File: " + _file);
                    ConsoleManager.WriteOutput("Path: " + _directory);
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    ExceptionManager.WriteException(e.Message);
                }
            }

            return string.Empty;
        }

        /// <summary>Displays network information.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public static string Net()
        {
            ConsoleManager.WriteOutput("Network available: " + NetworkManager.NetworkAvailable);
            ConsoleManager.WriteOutput("Internet available: " + NetworkManager.InternetAvailable);
            Console.Write(Environment.NewLine);
            return string.Empty;
        }

        /// <summary>Creates a new package file.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public static string New()
        {
            PackageManager.WorkingPath = string.Empty;
            PackageManager.WorkingPackage = new Package();
            ConsoleManager.WriteOutput("Created a new package.");
            Console.Write(Environment.NewLine);
            return string.Empty;
        }

        /// <summary>Open a package file.</summary>
        /// <param name="path">The file path.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Open(string path = "")
        {
            if (string.IsNullOrEmpty(path))
            {
                ConsoleManager.WriteOutput(Descriptions.CommandDescriptions[11]);
                ConsoleManager.WriteOutput("Usage: Open [path]");
                Console.WriteLine();
                return string.Empty;
            }
            else
            {
                try
                {
                    PackageManager.WorkingPath = path;
                    PackageManager.WorkingPackage = new Package(path);

                    if (!PackageManager.WorkingPackage.IsEmpty)
                    {
                        StringManager.DrawPackageTable(PackageManager.WorkingPackage);
                    }

                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    ExceptionManager.WriteException(e.Message);
                }

                return string.Empty;
            }
        }

        /// <summary>Reads the working package file.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public static string Read()
        {
            if (PackageManager.WorkingPackage == null)
            {
                ConsoleManager.WriteOutput(Descriptions.CommandDescriptions[12]);
                ConsoleManager.WriteOutput("Usage: Read");
                Console.WriteLine();
            }
            else
            {
                try
                {
                    StringManager.DrawPackageTable(PackageManager.WorkingPackage);
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    ExceptionManager.WriteException(e.Message);
                }
            }

            return string.Empty;
        }

        /// <summary>Saves the working package to file.</summary>
        /// <param name="path">The file path.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Save(string path = "")
        {
            if (string.IsNullOrEmpty(path) || (PackageManager.WorkingPackage == null))
            {
                ConsoleManager.WriteOutput(Descriptions.CommandDescriptions[13]);
                ConsoleManager.WriteOutput("Usage: Save [path]");
                Console.WriteLine();
            }
            else
            {
                try
                {
                    PackageManager.WorkingPath = path;
                    PackageManager.WorkingPackage.Save(path, SaveOptions.None);

                    ConsoleManager.WriteOutput("The package was saved to the file.");
                    ConsoleManager.WriteOutput("Path: " + path);
                    Console.WriteLine();
                }
                catch (NullReferenceException e)
                {
                    ExceptionManager.WriteException(e.Message);
                }
                catch (Exception e)
                {
                    ExceptionManager.WriteException(e.Message);
                }
            }

            return string.Empty;
        }

        /// <summary>Unload the working package.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public static string Unload()
        {
            if (PackageManager.WorkingPackage == null)
            {
                ConsoleManager.WriteOutput(Descriptions.CommandDescriptions[14]);
                ConsoleManager.WriteOutput("Usage: Unload");
                Console.WriteLine();
            }
            else
            {
                try
                {
                    PackageManager.WorkingPath = string.Empty;
                    PackageManager.WorkingPackage = new Package();
                    ConsoleManager.WriteOutput("Unloaded package.");
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    ExceptionManager.WriteException(e.Message);
                }
            }

            return string.Empty;
        }

        #endregion
    }
}