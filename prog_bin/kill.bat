@echo off
title Windows 11 Manual Installer
setlocal EnableDelayedExpansion
echo Cancelling installer creation...
if "%1%"=="/wimr" (goto wimr_cancel)
if "%1%"=="/dllr" (goto dllr_cancel)
if "%1%"=="/regtweak" (goto regtweak_cancel)

:wimr_cancel
if exist ".\temp" (
	rd .\temp /s /q
)
if exist ".\install.wim" (
	del ".\install.wim"
)
if exist ".\install.esd" (
	del ".\install.esd"
)
goto finish

:dllr_cancel
if exist ".\temp" (
	rd .\temp /s /q
)
if exist ".\appraiser.dll" (
	del .\*.dll
)
goto finish

:regtweak_cancel
if exist ".\wimmount" (
	reg query HKLM\WIN11SYS\Setup 2>NUL
	if %ERRORLEVEL% equ 0 (
		reg unload HKLM\WIN11SYS
	)
	reg query HKLM\W11NROSOFT 2>NUL
	if %ERRORLEVEL% equ 0 (
		reg unload HKLM\W11NROSOFT
	)
	:: Discard every change done in "boot.wim" or "install.wim"
	dism /English /unmount-wim /mountdir=.\wimmount /discard
	if %ERRORLEVEL% equ 0 (
		rd .\wimmount /s /q
	)
)
if exist ".\boot.wim" (
	move /y ".\boot.wim" ".\temp\sources\boot.wim"
)
if exist ".\temp" (
	rd .\temp /s /q
)
goto finish

:finish
if exist ".\temp.bat" (
	del ".\temp.bat"
)
echo The installer creation has been cancelled.
ping -n 3 127.0.0.1 > NUL
exit 1