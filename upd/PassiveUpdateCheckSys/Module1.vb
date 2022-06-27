Imports Microsoft.VisualBasic.ControlChars
Imports System.IO
Imports System.Threading
Module Module1

    Sub Main()
        Console.Title = "Passive Update Check System"
        Console.WriteLine("Passive Update Check System (PUCS) version 0.5, for the Windows 11 Manual Installer")
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
            Process.Start("powershell", "Copy-Item -Path .\new\*.* -Destination . -Recurse -Force").WaitForExit()
            Process.Start("powershell", "Remove-Item -Path .\Resources -Recurse -Force").WaitForExit()
            Directory.CreateDirectory(".\Resources")
            Process.Start("powershell", "Copy-Item -Path .\new\Resources\* -Destination .\Resources -Recurse -Force").WaitForExit()
            File.Move(".\win11minst_new.exe", ".\win11minst.exe")
            Process.Start("powershell", "Remove-Item -Path .\new -Recurse -Force").WaitForExit()
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

End Module
