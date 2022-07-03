Imports Microsoft.VisualBasic.ControlChars
Imports System.IO
Imports System.Threading
Module Module1

    Sub Main()
        Console.Title = "Passive Update Check System"
        Console.WriteLine("Passive Update Check System (PUCS) version {0}", My.Application.Info.Version.ToString() & ", for the Windows 11 Manual Installer")
        Console.WriteLine(CrLf & _
                          "  Beginning program update. Please wait...")
        Try
            Dim OldVer As FileVersionInfo = FileVersionInfo.GetVersionInfo(".\win11minst.exe")
            Dim OldVerStr As String = OldVer.FileVersion.ToString()
            Dim NewVer As FileVersionInfo = FileVersionInfo.GetVersionInfo(".\win11minst_new.exe")
            Dim NewVerStr As String = NewVer.FileVersion.ToString()
            Console.WriteLine(CrLf & _
                              "    Old version: " & OldVerStr & CrLf & _
                              "    New version: " & NewVerStr)
            Thread.Sleep(2000)
            Console.WriteLine(CrLf & _
                          "  Updating the program...")
            File.Delete(".\version")
            File.Delete(".\latest")
            For Each dll In My.Computer.FileSystem.GetFiles(Directory.GetCurrentDirectory.ToString(), FileIO.SearchOption.SearchTopLevelOnly, "*.dll")
                File.Delete(dll)
            Next
            Process.Start("powershell", "Move-Item -Path .\new\*.dll -Destination . -Force").WaitForExit()
            If File.Exists(".\new\win11minst.exe") Then
                File.Delete(".\new\win11minst.exe")
            End If
            File.Move(".\win11minst.exe", ".\win11minst_old_v" & OldVerStr & ".exe")
            BackUpSettings()
            Process.Start("powershell", "Copy-Item -Path .\new\*.* -Destination . -Recurse -Force").WaitForExit()
            Process.Start("powershell", "Remove-Item -Path .\Resources -Recurse -Force").WaitForExit()
            Directory.CreateDirectory(".\Resources")
            Process.Start("powershell", "Copy-Item -Path .\new\Resources\* -Destination .\Resources -Recurse -Force").WaitForExit()
            File.Move(".\win11minst_new.exe", ".\win11minst.exe")
            Process.Start("powershell", "Remove-Item -Path .\new -Recurse -Force").WaitForExit()
            DeleteTmpSettings()
            RestoreSettings()
            Thread.Sleep(3000)
            Process.Start(".\win11minst.exe")
            Console.WriteLine(CrLf & _
                              "  The update process has completed. Launching the Windows 11 Manual Installer, version " & NewVerStr & "...")
            End
        Catch ex As Exception
            Console.WriteLine(CrLf & _
                              "An exception ocurred. Please read the exception details below" & CrLf & _
                              "Exception: {0}", ex.GetType().ToString() & CrLf & _
                              "No changes have been made.")
            Console.ReadKey()
            End
        End Try
    End Sub

    Sub BackUpSettings()
        File.Move(".\settings.ini", ".\settings.ini.back")
        If File.Exists(".\InstName.ini") And _
            File.Exists(".\TargetInstaller.ini") And _
            File.Exists(".\Win11Inst.ini") And _
            File.Exists(".\Win10Inst.ini") Then
            ' Check this if settings.ini contains "ReuseSI=1". If it doesn't, skip this
            File.Move(".\InstName.ini", ".\InstName.ini.back")
            File.Move(".\TargetInstaller.ini", ".\TargetInstaller.ini.back")
            File.Move(".\Win11Inst.ini", ".\Win11Inst.ini.back")
            File.Move(".\Win10Inst.ini", ".\Win10Inst.ini.back")
        End If
    End Sub

    Sub DeleteTmpSettings()
        File.Delete(".\settings.ini")
        If File.Exists(".\InstName.ini") And _
            File.Exists(".\TargetInstaller.ini") And _
            File.Exists(".\Win11Inst.ini") And _
            File.Exists(".\Win10Inst.ini") Then
            File.Delete(".\InstName.ini")
            File.Delete(".\TargetInstaller.ini")
            File.Delete(".\Win11Inst.ini")
            File.Delete(".\Win10Inst.ini")
        End If
    End Sub

    Sub RestoreSettings()
        File.Move(".\settings.ini.back", ".\settings.ini")
        If File.Exists(".\InstName.ini.back") And _
            File.Exists(".\TargetInstaller.ini.back") And _
            File.Exists(".\Win11Inst.ini.back") And _
            File.Exists(".\Win10Inst.ini.back") Then
            File.Move(".\InstName.ini.back", ".\InstName.ini")
            File.Move(".\TargetInstaller.ini.back", ".\TargetInstaller.ini")
            File.Move(".\Win11Inst.ini.back", ".\Win11Inst.ini")
            File.Move(".\Win10Inst.ini.back", ".\Win10Inst.ini")
        End If
    End Sub
End Module
