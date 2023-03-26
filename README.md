# Windows 11 Manual Installer
The tool that helps you install Windows 11 on unsupported systems

## Features

Version 2.0 has new features and tricks up its sleeve.

- Redesigned user interface, which makes the program look like a modern Windows application
- New method: REGTWEAK

And more features you can look at in the release notes: [View](https://github.com/CodingWonders/win11minst/blob/stable/relnotes.md)

## Building the software
To make a build of this software, you need to do the following:
Prerequisites:
- [.NET Framework 4.8 Developer Pack][netfxdp]

Required step: clone the repository - `git clone https://github.com/CodingWonders/win11minst`

1. Open the *.sln* file on Visual Studio 2012 or newer

> If you don't have Visual Studio installed, please go to *visualstudio.microsoft.com* to download Visual Studio Community 2019 or 2022.
>> Make sure your computer meets the system requirements for Visual Studio.

> If you open the solution without installing the .NET Framework 4.8 Developer Pack, you will have an error loading the solution.

2. Change the build target from *Debug* to ***Release***

3. Click Build > Build solution

> If you have one of the latest versions of Visual Studio, *Build solution* will be renamed to *Build Windows 11 Manual Installer.vbproj*, otherwise, click Build solution (or press Ctrl + Shift + B)

4. After building the solution, right-click the project name, *Windows 11 Manual Installer 2.0* and click *Open folder in File Explorer*
5. Go to *Bin > Release*

> If you haven't performed Step 2, go to *Bin > Debug*

You now have a working copy of the Windows 11 Manual Installer.

[netfxdp]: https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net48-developer-pack-offline-installer ".NET Framework 4.8 Developer Pack"
