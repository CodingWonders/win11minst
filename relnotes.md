## Windows 11 Manual Installer Release Notes

Here you can see the feature list for each released version.

### 2.0.0100_220515

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
