## Windows 11 Manual Installer Release Notes

Here you can see the feature list for each released version since `2.0.0100_220515`. If you want to take a look at the release notes for an older release, please look at **that specific release** (you can do this more easily by clicking the links at the bottom of the file)

### Updating the program
---
Since version `2.0.0100_220529`, the program has had a built-in updater, which installs up-to-date versions (with new features and bugfixes), and installs the necessary references to make the program work.

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

### 2.0.0100_220619
---
This version may not have many new features. As the date for the stable version of 2.0 is getting nearer, the following releases will focus more on bugfixes. Still, there are some new features you might find interesting

#### **Bugfixes**

- Fixed a bug that put external panels after clicking their context menu option (like the installer history or the functionality advanced options) to the top-left corner if the main window was minimized
- Fixed a bug that caused a program exception if the user clicked "Cancel" on the file specification dialogs

#### **New features**

- Component version information is now displayed in the About screen

![win11minst_UOnQNw3Lw8](https://user-images.githubusercontent.com/101426328/174486722-2edf9865-97f4-4fa8-b07e-10ee628d4557.png)

- Functionality settings can now be accessed from the installer creation screen

![XOZ2RL8IUJ](https://user-images.githubusercontent.com/101426328/174486754-c9cdd582-a9cb-4d52-8e5c-7c2412544458.gif)


- When hovering over the side panel images, a description is shown
- Added French translations

    > Since releases are weekly, not a lot much was done for full French translations, so it will only apply on the main window (for now)

- Began work on a revamped Instructions panel (although it can't be loaded yet)
- Began improving program speed and fluidity
- If the program is run on a 32-bit (x86) system, a 'processor' icon will appear next to the window buttons

#### **Removed/stripped down features**

There are no removed features from this release


### 2.0.0100_220612
---

#### **Bugfixes**

- Fixed a bug where the program would throw an exception if a solid color was set as the desktop background

    > If you apply a solid background on your system, the program will default to its background color


#### **New features**

- You can now drag ISO files and drop them in the program

    > If you run the program as an Administrator, the drag & drop feature will not work (and it is not an isolated case, every program that supports drag & drop will not perform the operation). There is a workaround, which requires you to change a Group Policy setting related to User Account Control (UAC); although it's not recommended, as this change may pose a security risk to your system. Still, if you are interested, here is the workaround link: [https://weblogs.asp.net/jeffwids/windows-7-user-account-control-does-not-allow-drag-and-drop][dndadmin]

[dndadmin]: https://weblogs.asp.net/jeffwids/windows-7-user-account-control-does-not-allow-drag-and-drop

- Installer history items can now be exported to HTML files

    > This option, and the "`Export to XML file...`" option, are now shown by clicking the "`Export options`" button

- Audible events are now produced by dialogs during installer creation
- On-the-fly color mode and language changes for external panels

    > It may take some time to perform these on-the-fly changes on some panels, like the update screen

- New references (from later versions) can now be downloaded and applied by the updater, replacing existing ones
- Settings are now saved when installing updates
- Updated user agent
- Advanced options can now be accessed by selecting "`Installer creation method > Advanced options`"

#### **Removed/stripped down features**

There are no removed features in this release


### 2.0.0100_220605
---

#### **Bugfixes**

- Fixed a bug where the navigation bar icons would not change images or change pages from certain ones
- Fixed a bug that allowed moving the window by dragging from the `Personalization` screen header
- Fixed a bug where `View log file` would not be moved after resizing the window
- Fixed a bug where clicking the `View installer history` context menu option would restore the window to a normal state when it was maximized


#### **New features**

- You can now view release notes from the update window
- You can now export installer history items to an XML file. Because of this, the `System.Data` dependency is needed

    > Support for other file types is planned in the future, such as exporting to a HTML file, or to an Excel spreadsheet

- When updates are detected, the message is less vague
- Refactored some code (it still works, though)

#### **Removed/stripped down features**

There are no removed features in this release

### 2.0.0100_220529
---

#### **Bugfixes**

- Fixed a bug where restoring the window to its normal state would put it to the top left corner, `(0,0)`
- Fixed a bug where the progress label would not change after switching languages to `Automatic`
- Fixed a bug where the panel titles would not be localized
- Fixed a bug where the installer creation method option in the context menu is still disabled after creating an installer
- Fixed a bug where the navigation bar icons don't change correctly when changing color modes


#### **New features**

- The image download panel now detects administrative privileges
    - If they are present, the build mode option is visible
    - If they aren't present, the build mode option is hidden
- The build process is now automated and will not require you to type `0` after script completion
- The update system is functional

    > From this release aftwerwards, you can trigger an update by clicking `Check for updates` on the About screen or on program startup. Also, in new copies (those that didn't check for updates before), the program will suggest the user to do so

- Updated the About screen
- The log can now be viewed by clicking `View log file` after installer creation
- When specifying additional REGTWEAK options, the script flags will be shown in the installer creation log (`MainForm.LogBox`)

#### **Removed/stripped down features**

- Removed dependency: `Microsoft.VisualBasic.PowerPacks.Vs`. You don't need it anymore to launch the program, starting from this release

### 2.0.0100_220515
---
#### **Bugfixes**

- Fixed item positioning and scale when switching languages

#### **New features**

- Added missing icons for panels

|  Icon  |  Description  |
| ------------ | ------------ |
|  ![advanced_options](https://user-images.githubusercontent.com/101426328/168483895-08b1562a-ccf9-4b4d-9918-30b387c019ae.png)  |  Advanced options (REGTWEAK only)  |
|  ![cancel](https://user-images.githubusercontent.com/101426328/168483923-4624dbde-d4a7-4acf-a497-22b3102f7fc6.png)  |  Cancel installer creation  |
|  ![copy](https://user-images.githubusercontent.com/101426328/168483930-55d6dcf2-1ac1-4170-ac33-d5b3c3609bc5.png)  |  Installer copy to local disk  |
|  ![debug_mode](https://user-images.githubusercontent.com/101426328/168483945-a0fdf05f-75a3-4394-a555-eeaa8ca39d7c.png)  |  Debug mode (for testing)  |
|  ![disclaimer](https://user-images.githubusercontent.com/101426328/168483958-2b43facb-4ed2-4504-a60f-2ebe0d0c8b93.png)  |  Disclaimer notice  |
|  ![download](https://user-images.githubusercontent.com/101426328/168483961-51c02961-93e9-4878-a65d-85d23a3c1dc1.png)  |  Download/Build Windows images  |
|  ![help](https://user-images.githubusercontent.com/101426328/168483970-e56ff600-5fdc-4238-8961-09e0448d3287.png)   |  Method help  |
|  ![history](https://user-images.githubusercontent.com/101426328/168484014-9cd8e4f5-5b7a-4e2a-b51e-041de20080fa.png)  |  Installer history  |
|  ![reset](https://user-images.githubusercontent.com/101426328/168484021-18045cab-71aa-436b-aedf-8ce99f456316.png)  |  Reset preferences  |
|  ![search](https://user-images.githubusercontent.com/101426328/168484025-222901bd-6f31-43e5-8398-b9340b5d15e0.png)  |  Scan for ISO images  |
|  ![update](https://user-images.githubusercontent.com/101426328/168484037-a857226a-7dfa-41c9-8ba7-365a3dc3ae45.png)  |  Program updates  |

Example:

![220515_1](https://user-images.githubusercontent.com/101426328/168484197-563a618a-89ac-4605-bfa4-ff7e979f99ea.png)

> Installer history and new icon shown in `Alt-Tab` menu

- When the installer creation process finishes, the program will display the warnings and errors, and will append them to the log file

![win11minst_z5R1tXMdYl](https://user-images.githubusercontent.com/101426328/168484351-197f8136-5257-442d-a5fc-505740af5fb3.png)

Example log file:

```
Finished creating the installer. Details:
Began installer creation at: 14/05/2022 20:04:35
Ended installer creation at: 14/05/2022 20:10:29
Message(s): 946. Warning(s): 2. Error(s): 0.
Warnings:
14/05/2022 20:05:33 - The program attempted to run the REGTWEAK script with advanced options, but one of the source images contains ESD files. These cannot be mounted by DISM
14/05/2022 20:10:28 - An exception ocurred while deleting the temporary directory. The program has attempted an alternative folder deletion method

Errors:

```

- Added links for [WhyNotWin11][wnw11]

![win11minst_nrL1bOXQEw](https://user-images.githubusercontent.com/101426328/168484743-798f3748-c7eb-4695-a3c1-bad1d9f04d19.png)


- When finishing build mode, the program will tell the user that the process has finished

![win11minst_G9j4hs1ZOZ](https://user-images.githubusercontent.com/101426328/168486977-78e4c1be-04e3-4492-b7ed-022d354e53a5.png)


#### **Removed/stripped down features**
- Removed copyright information
- The program no longer detects Windows Server operating systems to suggest creating custom Server installers (Microsoft still doesn't check hardware on Windows Server, but they might in the future)

> The code to detect Windows Server isn't gone, but it is commented out

[wnw11]: http://github.com/rcmaehl/WhyNotWin11 "WhyNotWin11"

### Other releases
If you want to look at older versions's release notes, click on one of the links below:

- [2.0.0100_220501][beta_220501]
- [2.0.0100_220424][beta_220424]
- [2.0.0100_220417][beta_220417]
- [2.0.0100_220410][beta_220410]
- [2.0.0100_220313][beta_220313]

[beta_220501]: https://github.com/CodingWonders/win11minst/releases/tag/beta_220501 "2.0.0100_220501"
[beta_220424]: https://github.com/CodingWonders/win11minst/releases/tag/beta_220424 "2.0.0100_220424"
[beta_220417]: https://github.com/CodingWonders/win11minst/releases/tag/beta_220417 "2.0.0100_220417"
[beta_220410]: https://github.com/CodingWonders/win11minst/releases/tag/beta_220410 "2.0.0100_220410"
[beta_220313]: https://github.com/CodingWonders/win11minst/releases/tag/beta_220313 "2.0.0100_220313"
