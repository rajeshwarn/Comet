# Comet
[![Issues](https://img.shields.io/github/issues/DarkByte7/Comet.svg?style=flat)](https://github.com/DarkByte7/Comet/issues)
[![Gitter](https://img.shields.io/gitter/room/nwjs/nw.js.svg)](https://gitter.im/Comet7/Lobby)

The Comet library provides integration into your .NET Windows Form and WPF applications for automatic updating functionality. You will be able to create packages and place them on local or remote servers that represent updates.

| Service | Stable | Beta |
| :---- | :---- | :------ |
AppVeyor | [ ![Stable build status][1]][2] | [![Beta build status][3]][4] |
Travic Cl | [ ![Stable build status][5]][6] | [![Beta build status][7]][8] |

[1]: https://img.shields.io/appveyor/ci/DarkByte7/Comet/master.svg?style=plastic
[2]: https://github.com/DarkByte7/Comet/releases
[3]: https://img.shields.io/appveyor/ci/DarkByte7/Comet/beta.svg?style=plastic
[4]: https://ci.appveyor.com/project/DarkByte7/Comet
[5]: https://img.shields.io/travis/DarkByte7/Comet/master.svg?style=plastic
[6]: https://github.com/DarkByte7/Comet/releases
[7]: https://img.shields.io/travis/DarkByte7/Comet/beta.svg?style=plastic
[8]: https://travis-ci.org/DarkByte7/Comet

To get the latest release, you can download a [`Fresh Build`](https://ci.appveyor.com/project/DarkByte7/Comet/build/artifacts) here.

The [`Comet`](https://github.com/DarkByte7/Comet) repository is where we do development and there are many ways you can participate in the project, for example:
- [Submit bugs and feature requests](https://github.com/DarkByte7/Comet/issues) and help us verify as they are checked in
- Review [source code changes](https://github.com/DarkByte7/Comet/pulls)
- Review the [documentation](https://github.com/DarkByte7/Comet/wiki) and make pull requests for anything from typos to new content

## Build requirements
- [.NET Framework 4.6.2](https://www.microsoft.com/en-us/download/details.aspx?id=53345)
- [Visual Studio 2017](https://www.visualstudio.com/downloads/)

## Compiling
1. Open the project in Visual Studio and click build. To generate an ```Updater.dll``` file in ```Comet\Updater\bin\Debug```.
2. Add the ```Updater.dll``` as a reference to your project. [How to: Add or Remove References in Visual Studio](https://msdn.microsoft.com/en-us/library/wkze6zky(v=vs.100).aspx).
3. Rebuild your project and it will now be available in the toolbox.

## Creating a Package
1. Compress all your files into a `.zip` archieve and upload the archive to your server.
2. Run the `new` command in the console.
3. Use the `edit` command to change the package data.
```C#
edit 0 "Initial release"
edit 1 "https://www.example.com/link"
edit 2 "FileName.exe"
edit 3 "ProductName"
edit 4 "01/01/2017"
edit 5 "1.0.0.0"
```
4. Use the `save` command to save the package. And upload it to your remote server.

## Using the UpdateManager
Create a new UpdateManager object like this:
```C#
Uri _packagePath = new Uri("https://raw.githubusercontent.com/DarkByte7/Comet/stable/Comet/Update.package");
string _downloadPath = FileManager.CreateTempPath("Download");
string _executablePath = @"G:\Comet\Comet\bin\Debug\Comet.exe";

// Create update manager object
UpdateManager _updateManager = new UpdateManager(_packagePath, _downloadPath, _executablePath, false);
```
Check for updates:
```C#
try
{
    // Check for updates.
     _updateManager.CheckForUpdates();
}
     catch (Exception e)
{
     Console.WriteLine(e);
     throw;
}
```

## Features
* Windows Toolbox Control
* Automatic Updating

## Contributing
If you are interested in fixing issues and contributing directly to the code base, please see the document [How to Contribute](https://github.com/DarkByte7/Comet/wiki/How-to-Contribute), which covers the following:
- [How to build and run from source](https://github.com/DarkByte7/Comet/wiki/How-to-Contribute#build-and-run-from-source)
- [Coding Guidelines](https://github.com/DarkByte7/Comet/wiki/Coding-Guidelines)
- [Submitting pull requests](https://github.com/DarkByte7/Comet/compare)

Enjoy using `Comet` or just want to say thanks?
Hit the ⭐️ Star ⭐️ button.

BitCoin donations are also welcome: `1KKghRonJu6orcu7rf4r1wSnsnAPbnC8B7`

## Feedback
- Ask a question on [Gitter](https://gitter.im/Comet7/Lobby).
- Request a new feature on [GitHub](https://github.com/DarkByte7/Comet/blob/beta/CONTRIBUTE.md).
- Vote for [popular feature requests](https://github.com/DarkByte7/Comet/issues?q=is:open+is:issue+label:feature-request+sort:reactions-B1-desc).
- File a bug in [GitHub Issues](https://github.com/DarkByte7/Comet/issues?q=is:open+is:issue).

## Screenshots
![Imgur](http://i.imgur.com/lvwo5D5.jpg)

![Imgur](http://i.imgur.com/0X0QULc.jpg)

## License
This repository is licensed with the [GPLv3](LICENSE) license.
