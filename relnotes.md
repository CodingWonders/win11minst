## Windows 11 Manual Installer Release Notes

### Updating the program
---

To install updates when the program is open:

1. Go to the About screen
2. Click "Check for updates"

If updates are found:

3. Click "`Updates are available. Click here to learn more.`"
4. Click "`Install now`"
5. Wait a little bit for the program to update

Congratulations, you have updated the program. If no updates were found, you don't have anything to do.

To install updates while the program is loading:

1. Wait until the update screen is shown

If updates are found:

2. Click "`Install now`"
3. Wait a little bit for the program to update

Congratulations, you have updated the program. If no updates are found, the program will continue startup.

### 2.0.0100_220731
---

This is the `stable` release of the Windows 11 Manual Installer.

#### Features

If you haven't checked the releases from the `hummingbird` branch, here is a feature recap:

- Redesigned user interface, which makes it look and feel like a modern Windows application

	- The automatic color scheme has been added, which checks Windows color modes and applies them to the program
	- The program layout has been redesigned, like a modern WinUI application
	- The program lets you set two navigation bar positions: left, or top


- A new installer creation method has been added, `REGTWEAK`, which makes changes to the Windows registry on the Windows 11 installer

	- This method can be customized to also bypass forced Microsoft Account sign-in and forced Internet connection
		
		> This option can be applied on Windows 11 installation mediums, builds 22557 and later

- The installer label can be picked from the Windows 11 installer
- An installer scanner has been added, letting you scan a directory for them, even recursively
- An installer history has been added, letting you see your recently created installers, as well as export them to many file formats (XML, HTML and CSV)
- The program now saves and loads custom preferences, meaning that you don't need to set them each time the program loads
- An installer downloader has been added, letting you download (or build, with admin. privileges, thanks to UUP Dump) Windows installation mediums from Microsoft's download pages
- The program now detects the space of the target installer path, and warns you about low disk spaces
- The program base has now been updated to .NET 4.8, for better security practices
- The instruction and help pages have been updated, making them more useful
- A new language has been added, French
- The program can now run from the system tray
- The program no longer freezes when creating an installer
- Fixed a bug that showed `´╗┐` when running external commands (1.0 builds)
- The program now detects missing components and warns you about them
- The program now remembers the installers that were going to be used previously, and gives you a chance of continuing with them

If you come from `hummingbird` releases, here are the bugfixes and features:

#### **Bugfixes**

- Fixed a bug related to the installer issues
- Fixed a bug that would show external panels on the top-left corner if the program was previously hidden to the system tray
- Fixed incomplete translations

#### **New features**

- The disclaimer can now be dismissed and automatically agreed to
- Fade animations can now be used when loading external panels
- The REGTWEAK script now sets the title
- Added the ability to cancel installer creation when closing the program
- The program can now append a colon (:) if not specified on the target installer parh's drive letter

#### **Removed features**

- The ability to cancel the installer creation without closing the program has been removed due to a lack of reliable functionality

**NOTE**: to view the release notes from the `hummingbird` branch, please check the [Hummingbird branch release notes][hrels]

[hrels]: https://github.com/CodingWonders/win11minst/blob/hummingbird/relnotes.md
