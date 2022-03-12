:: Windows 11 Manual Installer, version 2.0.0100 (hummingbird)                                    |
:: -----------------------------------------------------------------------------------------------|
::  File name: regtweak.bat						File version: 1.2.1                               |
::                                                                                                |
::                                                                                                |
::  Description: changes the Windows 11 installer's registry to bypass the system requirements	  |
::               and a few other settings that might bug users out                                |
::                                                                                                |
::  History: 10-31-2021 - Created                                                                 |
::           12-10-2021 - Bundled in Version 2.0.0100                                             |
::           12-26-2021 - Added support to mount the 2nd index of 'boot.wim', because             |
::                        it seems to mount this index on boot                                    |
::           03-06-2022 - Added support to bypass forced Internet connection setup and Microsoft  |
::                        account sign-in by using 'bypassnro'                                    |
::           03-13-2022 - Added support for hiding the 'System requirements not met'              |
::                        watermark                                                               |
:: -----------------------------------------------------------------------------------------------|
:: This process MUST be run by the Windows 11 Manual Installer AND as an Administrator            |
:: ------------------------------------------------------------------------------------------------
::  Make love, not war. Love to Ukraine citizens (and everyone affected by this situation) from Spain

@echo off
setlocal EnableDelayedExpansion
if "%1%"=="/bypassnro" (set bypassnro=1)
if "%1%"=="/sv2" (set sv2=1)
if "%2%"=="/bypassnro" (set bypassnro=1)
if "%2%"=="/sv2" (set sv2=1)
:: Let's do the fun stuff!
echo [    ] Mounting "boot.wim"...
dism /English /mount-wim /wimfile=".\boot.wim" /index=1 /mountdir=".\wimmount"
echo [ OK ] Mounted "boot.wim"
echo [    ] Loading 'boot.wim's SYSTEM registry hive onto computer's registry...
reg load HKLM\WIN11SYS ".\wimmount\Windows\system32\config\SYSTEM" > NUL
echo [ OK ] Loaded 'boot.wim's SYSTEM registry hive onto computer's registry
echo [    ] Adding 'LabConfig'...
reg add HKLM\WIN11SYS\Setup\LabConfig /f > NUL
echo [ OK ] Added 'LabConfig'
echo [    ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassCPUCheck /t REG_DWORD /d 1 /f > NUL
echo [-   ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassRAMCheck /t REG_DWORD /d 1 /f > NUL
echo [--  ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassStorageCheck /t REG_DWORD /d 1 /f > NUL
echo [--- ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassTPMCheck /t REG_DWORD /d 1 /f > NUL
echo [----] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassSecureBootCheck /t REG_DWORD /d 1 /f > NUL
echo [ OK ] Added registry keys
echo [    ] Unloading 'boot.wim's SYSTEM registry hive from computer's registry...
reg unload HKLM\WIN11SYS > NUL
echo [ OK ] Unloaded 'boot.wim's SYSTEM registry hive from computer's registry...
echo [    ] Unmounting "boot.wim"...
dism /English /unmount-wim /mountdir=".\wimmount" /commit
echo [ OK ] Unmounted "boot.wim"
echo [    ] Mounting index 2 of "boot.wim"...
dism /English /mount-wim /wimfile=".\boot.wim" /index=2 /mountdir=".\wimmount"
echo [ OK ] Mounted index 2
echo [    ] Loading 'boot.wim's SYSTEM registry hive onto computer's registry...
reg load HKLM\WIN11SYS ".\wimmount\Windows\system32\config\SYSTEM" > NUL
echo [ OK ] Loaded 'boot.wim's SYSTEM registry hive onto computer's registry
echo [    ] Adding 'LabConfig'...
reg add HKLM\WIN11SYS\Setup\LabConfig /f > NUL
echo [ OK ] Added 'LabConfig'
echo [    ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassCPUCheck /t REG_DWORD /d 1 /f > NUL
echo [-   ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassRAMCheck /t REG_DWORD /d 1 /f > NUL
echo [--  ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassStorageCheck /t REG_DWORD /d 1 /f > NUL
echo [--- ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassTPMCheck /t REG_DWORD /d 1 /f > NUL
echo [----] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassSecureBootCheck /t REG_DWORD /d 1 /f > NUL
echo [ OK ] Added registry keys
echo [    ] Unloading 'boot.wim's SYSTEM registry hive from computer's registry...
reg unload HKLM\WIN11SYS > NUL
echo [ OK ] Unloaded 'boot.wim's SYSTEM registry hive from computer's registry...
echo [    ] Unmounting "boot.wim"...
dism /English /unmount-wim /mountdir=".\wimmount" /commit
if "%bypassnro%"=="1" (echo [ OK ] Unmounted index 2 of "boot.wim". && goto bypassnro)
if "%sv2%"=="1" (echo [ OK ] Unmounted index 2 of "boot.wim". && goto sv2)
echo [ OK ] Unmounted index 2 of "boot.wim". Leaving...
goto end

:: Inspiration from Window$(TM) 11 build 22557+
:bypassnro
dism /English /Get-WimInfo /wimfile=".\temp\sources\install.wim"
set /p wimindex=Which index corresponds to Windows 11 Pro? Index: 
echo [    ] Mounting index %wimindex% of "install.wim"...
dism /English /mount-wim /wimfile=".\temp\sources\install.wim" /index=%wimindex% /mountdir=".\wimmount"
echo [ OK ] Mounted index %wimindex% of "install.wim"
echo [    ] Loading 'install.wim's SOFTWARE registry hive onto computer's registry...
reg load HKLM\W11NROSOFT ".\wimmount\Windows\system32\config\SOFTWARE" > NUL
echo [ OK ] Loaded 'install.wim's SOFTWARE registry hive onto computer's registry
echo [    ] Adding BypassNRO...
:: This was directly taken from bypassnro.cmd. Microsoft couldn't think of another strategy to do this method
:: other than a batch file.
reg add HKLM\W11NROSOFT\Microsoft\Windows\CurrentVersion\OOBE /v BypassNRO /t REG_DWORD /d 1 /f > NUL
echo [ OK ] Added BypassNRO
echo [    ] Unloading 'install.wim's SOFTWARE registry hive from computer's registry...
reg unload HKLM\W11NROSOFT > NUL
echo [ OK ] Unloaded 'install.wim's SOFTWARE registry hive from computer's registry
echo [    ] Unmounting index %wimindex% of "install.wim"...
dism /English /unmount-wim /mountdir=".\wimmount" /commit
if "%sv2%"=="1" (
	echo [ OK ] Unmounted index %wimindex% of "install.wim".
	goto sv2
) else (
	echo [ OK ] Unmounted index %wimindex% of "install.wim". Leaving...
	goto end
)

:: Thanks to BetaWiki for this registry tweak!
:: Side note: you can also use ViVeTool
:sv2
dism /English /Get-WimInfo /wimfile=".\temp\sources\install.wim"
set /p wimindex=Which index do you want to mount? Index: 
if "%wimindex%"=="all" (goto wimcount)
echo [    ] Mounting index %wimindex% of "install.wim"...
dism /English /mount-wim /wimfile=".\temp\sources\install.wim" /index=%wimindex% /mountdir=".\wimmount"
echo [ OK ] Mounted index %wimindex% of "install.wim"
echo [    ] Loading 'install.wim's SOFTWARE registry hive onto computer's registry...
reg load HKLM\ActiveSetupSoftware ".\wimmount\Windows\system32\config\SOFTWARE" > NUL
echo [ OK ] Loaded 'install.wim's SOFTWARE registry hive onto computer's registry
echo [    ] Adding SV2...
:: This requires creating a reg file and adding it to a new StubPath in Active Setup (because it must be ran each time a user account is created)
:: Thanks to my Windows 7 VM for being a guinea pig and experimenting with this.
echo Windows Registry Editor Version 5.00 > ".\wimmount\disablesv2.reg"
echo. >> ".\wimmount\disablesv2.reg"
echo [HKEY_CURRENT_USER\Control Panel\UnsupportedHardwareNotificationCache] >> ".\wimmount\disablesv2.reg"
echo "SV2"=dword:00000000 >> ".\wimmount\disablesv2.reg"
attrib ".\wimmount\disablesv2.reg" +h
reg add "HKLM\ActiveSetupSoftware\Microsoft\Active Setup\Installed Components\DisableSV2" /v Version /t REG_SZ /d 1 > NUL
reg add "HKLM\ActiveSetupSoftware\Microsoft\Active Setup\Installed Components\DisableSV2" /v StubPath /t REG_SZ /d "regedit /s \disablesv2.reg" > NUL
echo [ OK ] Added SV2
echo [    ] Unloading 'install.wim's SOFTWARE registry hive from computer's registry...
reg unload HKLM\ActiveSetupSoftware > NUL
echo [ OK ] Unloaded 'install.wim's SOFTWARE registry hive from computer's registry
echo [    ] Unmounting index %wimindex% of "install.wim"...
dism /English /unmount-wim /mountdir=".\wimmount" /commit
echo [ OK ] Unmounted index %wimindex% of "install.wim". Leaving...
goto end

:wimcount
set wimindex=0
set /p wimindexcount=How many indexes does this file have? Index count: 
echo Mounting %wimindexcount% index(es) of "install.wim". This might take a LONG time...
goto instwimmount

:instwimmount
:: If an "install.wim" file contains more than 1 index, this must be ran in a loop until all conditions
:: were worked on.
set /a wimindex=%wimindex% + 1
echo [    ] Mounting index %wimindex% of "install.wim"...
dism /English /mount-wim /wimfile=".\temp\sources\install.wim" /index=%wimindex% /mountdir=".\wimmount"
echo [ OK ] Mounted index %wimindex% of "install.wim"
echo [    ] Loading 'install.wim's SOFTWARE registry hive onto computer's registry...
reg load HKLM\ActiveSetupSoftware ".\wimmount\Windows\system32\config\SOFTWARE" > NUL
echo [ OK ] Loaded 'install.wim's SOFTWARE registry hive onto computer's registry
echo [    ] Adding SV2...
echo Windows Registry Editor Version 5.00 > ".\wimmount\disablesv2.reg"
echo. >> ".\wimmount\disablesv2.reg"
echo [HKEY_CURRENT_USER\Control Panel\UnsupportedHardwareNotificationCache] >> ".\wimmount\disablesv2.reg"
echo "SV2"=dword:00000000 >> ".\wimmount\disablesv2.reg"
attrib ".\wimmount\disablesv2.reg" +h
reg add "HKLM\ActiveSetupSoftware\Microsoft\Active Setup\Installed Components\DisableSV2" /v Version /t REG_SZ /d 1 /f > NUL
reg add "HKLM\ActiveSetupSoftware\Microsoft\Active Setup\Installed Components\DisableSV2" /v StubPath /t REG_SZ /d "regedit /s \disablesv2.reg" /f > NUL
echo [ OK ] Added SV2
echo [    ] Unloading 'install.wim's SOFTWARE registry hive from computer's registry...
reg unload HKLM\ActiveSetupSoftware > NUL
echo [ OK ] Unloaded 'install.wim's SOFTWARE registry hive from computer's registry
echo [    ] Unmounting index %wimindex% of "install.wim"...
dism /English /unmount-wim /mountdir=".\wimmount" /commit
if !wimindex! == !wimindexcount! (
	echo [ OK ] Unmounted index %wimindex% of "install.wim". Leaving...
	goto end
) else (
	echo [ OK ] Unmounted index %wimindex% of "install.wim".
	goto instwimmount
)


:end
ping -n 5 127.0.0.1 > NUL
if "%CMDCMDLINE%"=="C:\Windows\system32\cmd.exe" (
	exit /b
) else (
	exit
)
endlocal