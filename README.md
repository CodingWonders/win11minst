# NOTE: THIS REPOSITORY HAS BEEN ARCHIVED FOR HISTORIC PURPOSES. NO DEVELOPMENT WILL BE DONE. Feel free to do whatever you want with it - other than contribute to it!

# Windows 11 Manual Installer (Hummingbird release)
This is the beta version for the future of the Windows 11 Manual Installer.

## Versioning system
Version 2.0.0100_220410 has introduced a new versioning system, which follows this scheme:
		
        Prod version:   2.0.0100.2241
        File version:   2.0.0100.2241
        
*Information obtained from SigCheck. This is what you would see when viewing the properties of win11minst.exe*

Let's break it down

| Major  | Minor  | Revision  | Release from month  |
| ------------ | ------------ | ------------ | ------------ |
|  2 | 0  | 0100  | 2241  |

- *Major* is the major release of the program. For example, this corresponds to version 2.0.
- *Minor* is the minor release of the program. For example, an update to the program might have its minor number set to 1.
- *Revision* is the program revision. This gets incremented by an update (e.g., a bugfix)
- *Release from month* is the unique part of the versioning system. In this case, *2241* is the 1st release from April 2022. This is done to prevent the 16-bit integer overflow (0-65535). For example:

| Release from month  | Details  |
| ------------ | ------------ |
| 2241  | 1st release of April 2022  |
| 22123  | 3rd release of December 2022  |
| 2312  | 2nd release of January 2023  |
| 2343  | 3rd release of April 2023  |
> NOTE: the release from month usually gets updated every Sunday, however, this is not possible on most cases

## Building the software
To make a build of this software, you need to do the following:
Prerequisites:
- [.NET Framework 4.8 Developer Pack][netfxdp]

1. Open the *.sln* file on Visual Studio 2012 or newer

> If you don't have Visual Studio installed, please go to *visualstudio.microsoft.com* to download Visual Studio Community 2019 or 2022.
>> Make sure your computer meets the system requirements for Visual Studio.

> If you open the solution without installing the .NET Framework 4.6.2 Developer Pack, you will have an error loading the solution.

2. Change the build target from *Debug* to ***Release***

3. Click Build > Build solution

> If you have one of the latest versions of Visual Studio, *Build solution* will be renamed to *Build Windows 11 Manual Installer 2.0.vbproj*, otherwise, click Build solution (or press Ctrl + Shift + B)

4. After building the solution, right-click the project name, *Windows 11 Manual Installer 2.0* and click *Open folder in File Explorer*
5. Go to *Bin > Release*

> If you haven't performed Step 2, go to *Bin > Debug*

You now have a working copy of the Windows 11 Manual Installer.

[netfxdp]: https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net48-developer-pack-offline-installer ".NET Framework 4.8 Developer Pack"
