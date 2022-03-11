:: Windows 11 Manual Installer, version 2.0.0100 (dbg)                                            |
:: -----------------------------------------------------------------------------------------------|
::  File name: regtweak.bat						File version: 1.2                                 |
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
:: -----------------------------------------------------------------------------------------------|
:: This process MUST be run by the Windows 11 Manual Installer AND as an Administrator            |
:: ------------------------------------------------------------------------------------------------

@echo off
if "%1%"=="/bypassnro" (
	set bypassnro=1
)
:: Let's do the fun stuff!
echo [    ] Mounting 'boot.wim'...
dism /English /mount-wim /wimfile=.\boot.wim /index=1 /mountdir=.\wimmount
echo [ OK ] Mounted 'boot.wim'
echo [    ] Loading 'boot.wim's SYSTEM registry hive onto computer's registry...
reg load HKLM\WIN11SYS .\wimmount\Windows\system32\config\SYSTEM > NUL
echo [ OK ] Loaded 'boot.wim's SYSTEM registry hive onto computer's registry
echo [    ] Adding 'LabConfig'...
reg add HKLM\WIN11SYS\Setup\LabConfig > NUL
echo [ OK ] Added 'LabConfig'
echo [    ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassCPUCheck /t REG_DWORD /d 1  > NUL
echo [-   ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassRAMCheck /t REG_DWORD /d 1 > NUL
echo [--  ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassStorageCheck /t REG_DWORD /d 1 > NUL
echo [--- ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassTPMCheck /t REG_DWORD /d 1 > NUL
echo [----] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassSecureBootCheck /t REG_DWORD /d 1 > NUL
echo [ OK ] Added registry keys
echo [    ] Unloading 'boot.wim's SYSTEM registry hive from computer's registry...
reg unload HKLM\WIN11SYS > NUL
echo [ OK ] Unloaded 'boot.wim's SYSTEM registry hive from computer's registry...
echo [    ] Unmounting 'boot.wim'...
dism /English /unmount-wim /mountdir=.\wimmount /commit
echo [ OK ] Unmounted 'boot.wim'
echo [    ] Mounting index 2 of 'boot.wim'...
dism /English /mount-wim /wimfile=.\boot.wim /index=2 /mountdir=.\wimmount
echo [ OK ] Mounted index 2
echo [    ] Loading 'boot.wim's SYSTEM registry hive onto computer's registry...
reg load HKLM\WIN11SYS .\wimmount\Windows\system32\config\SYSTEM > NUL
echo [ OK ] Loaded 'boot.wim's SYSTEM registry hive onto computer's registry
echo [    ] Adding 'LabConfig'...
reg add HKLM\WIN11SYS\Setup\LabConfig > NUL
echo [ OK ] Added 'LabConfig'
echo [    ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassCPUCheck /t REG_DWORD /d 1  > NUL
echo [-   ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassRAMCheck /t REG_DWORD /d 1 > NUL
echo [--  ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassStorageCheck /t REG_DWORD /d 1 > NUL
echo [--- ] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassTPMCheck /t REG_DWORD /d 1 > NUL
echo [----] Adding registry keys...
reg add HKLM\WIN11SYS\Setup\LabConfig /v BypassSecureBootCheck /t REG_DWORD /d 1 > NUL
echo [ OK ] Added registry keys
echo [    ] Unloading 'boot.wim's SYSTEM registry hive from computer's registry...
reg unload HKLM\WIN11SYS > NUL
echo [ OK ] Unloaded 'boot.wim's SYSTEM registry hive from computer's registry...
echo [    ] Unmounting 'boot.wim'...
dism /English /unmount-wim /mountdir=.\wimmount /commit
if %bypassnro%==1 (
	echo [ OK ] Unmounted index 2 of 'boot.wim'.
	goto bypassnro
) else (
	echo [ OK ] Unmounted index 2 of 'boot.wim'. Leaving...
	goto end
)

:: Inspiration from Window$(TM) 11 build 22557+
:bypassnro
dism /English /Get-WimInfo /wimfile=.\temp\sources\install.wim
set /p wimindex=Which index corresponds to Windows 11 Pro? Index: 
echo [    ] Mounting index %wimindex% of "install.wim"...
dism /English /mount-wim /wimfile=.\temp\sources\install.wim /index=%wimindex% /mountdir=.\wimmount
echo [ OK ] Mounted index %wimindex% of "install.wim"
echo [    ] Loading 'install.wim's SOFTWARE registry hive onto computer's registry...
reg load HKLM\W11NROSOFT .\wimmount\Windows\system32\config\SOFTWARE > NUL
echo [ OK ] Loaded 'install.wim's SOFTWARE registry hive onto computer's registry
echo [    ] Adding BypassNRO...
reg add HKLM\W11NROSOFT\Microsoft\Windows\CurrentVersion\OOBE /v BypassNRO /t REG_DWORD /d 1 /f > NUL
echo [ OK ] Added BypassNRO...
echo [    ] Unloading 'install.wim's SOFTWARE registry hive from computer's registry...
reg unload HKLM\W11NROSOFT > NUL
echo [    ] Unmounting index %wimindex% of 'install.wim'...
dism /English /unmount-wim /mountdir=.\wimmount /commit
echo [ OK ] Unmounted index %wimindex% of 'install.wim'. Leaving...
goto end 


:end

ping -n 5 127.0.0.1 > NUL
if %CMDCMDLINE%=="C:\Windows\system32\cmd.exe" (
	exit /b
) else (
	exit
)