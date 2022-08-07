Imports Microsoft.VisualBasic.ControlChars
Imports System.IO
Imports System.Threading
Imports System.Net
Imports System.Text.Encoding

Module Module1


    Dim OldVer As FileVersionInfo
    Dim OldVerStr As String
    Dim NewVer As FileVersionInfo
    Dim NewVerStr As String
    Dim VerTag As String
    Dim VerForm As New VerTag()

    Public Sub Main()
        Console.Title = "Passive Update Check System"
        Console.WriteLine("Passive Update Check System (PUCS) version {0}", My.Application.Info.Version.ToString() & ", for the Windows 11 Manual Installer")
        Console.WriteLine(CrLf & _
                          "  Beginning program update. Please wait...")
        If Not Directory.Exists(".\new") Or Not File.Exists(".\win11minst_new.exe") Then
            DownloadNewWin11MInstVersion()
        End If
        Try
            OldVer = FileVersionInfo.GetVersionInfo(".\win11minst.exe")
            OldVerStr = OldVer.FileVersion.ToString()
            NewVer = FileVersionInfo.GetVersionInfo(".\win11minst_new.exe")
            NewVerStr = NewVer.FileVersion.ToString()
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
            If File.Exists(".\new\prog_bin\regtweak.bat") Then
                File.Delete(".\prog_bin\regtweak.bat")
                File.Move(".\new\prog_bin\regtweak.bat", ".\prog_bin\regtweak.bat")
            End If
            Process.Start("powershell", "Remove-Item -Path .\new -Recurse -Force").WaitForExit()
            DeleteTmpSettings()
            RestoreSettings()
            If File.Exists(".\version") Then
                File.Delete(".\version")
            End If
            Thread.Sleep(3000)
            Process.Start(".\win11minst.exe")
            Console.WriteLine(CrLf & _
                              "The update process has completed. Launching the Windows 11 Manual Installer, version " & NewVerStr & "...")
            Thread.Sleep(2000)
            End
        Catch ex As Exception
            Console.WriteLine(CrLf & _
                              "An error has ocurred. Please read the details below" & CrLf & _
                              "Error: {0}", ex.GetType().ToString() & CrLf & _
                              "       " & Err.Description & CrLf & _
                              "No changes have been made.")
            Console.ReadKey()
            End
        End Try
    End Sub

    Public Sub DownloadNewWin11MInstVersion()      ' Call this subprocedure if the .\new folder (with the new version of win11minst) does not exist
        Console.WriteLine("  Some update files were not found. Attempting to download them now...")
        Try
            VerForm.Validate()
            VerForm.TextBox1.Text = File.ReadAllText(".\latest").ToString()
            If VerForm.TextBox1.Text.Contains("2.0.0100_") Then
                VerForm.TextBox1.Text = VerForm.TextBox1.Text.Replace("2.0.0100_", "stable_").Trim()
            ElseIf VerForm.TextBox1.Text.Contains("2.0.0101_") Then
                VerForm.TextBox1.Text = VerForm.TextBox1.Text.Replace("2.0.0101_", "stable_").Trim()
            End If
            VerTag = VerForm.TextBox1.Text
            If Not File.Exists(".\win11minst_new.exe") Then
                Using Win11MinstDown As New WebClient()
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                    Win11MinstDown.DownloadFile("https://github.com/CodingWonders/win11minst/blob/stable/bin/Debug/win11minst.exe?raw=true", ".\win11minst_new.exe")
                End Using
            End If
            If Not Directory.Exists(".\new") Then
                Using NewVersionDown As New WebClient()
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                    If File.Exists(".\new.zip") Then
                        File.Delete(".\new.zip")
                    End If
                    NewVersionDown.DownloadFile("https://github.com/CodingWonders/win11minst/releases/download/" & VerTag & "/win11minst.zip", ".\new.zip")
                    File.SetAttributes(".\new.zip", FileAttributes.Hidden)
                    Try
                        Process.Start(".\prog_bin\7z", "x .\new.zip -o.\new").WaitForExit()
                    Catch ex As Exception
                        File.WriteAllText(".\ex.bat", "@echo off" & CrLf & ".\prog_bin\7z x .\new.zip -o.\new", ASCII)
                        Process.Start(".\ex.bat").WaitForExit()
                        File.Delete(".\ex.bat")
                    End Try
                    File.Delete(".\new.zip")
                End Using
            End If
        Catch ex As Exception
            Console.WriteLine(CrLf & _
                              "An error has ocurred. Please read the details below" & CrLf & _
                              "Error: {0}", ex.GetType().ToString() & CrLf & _
                              "       " & Err.Description & CrLf & _
                              "No changes have been made.")
            Console.ReadKey()
            UndoChanges()
            End
        End Try
    End Sub

    Public Sub UndoChanges()
        If File.Exists(".\win11minst_new.exe") Then
            File.Delete(".\win11minst_new.exe")
        End If
        If File.Exists(".\win11minst_old_v" & OldVerStr & ".exe") Then
            File.Move(".\win11minst_old_v" & OldVerStr & ".exe", ".\win11minst.exe")
        End If
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
