Imports Microsoft.VisualBasic.ControlChars
Imports System.Text.Encoding
Imports System.IO
Public Class VersionDetector
    Dim OffEcho As String = "@echo off"
    Dim commandStr As String = "reg query " & Quote & "HKLM\Software\Microsoft\Windows NT\CurrentVersion" & Quote & " /v BuildLabEx > .\info.txt"
    Private Sub VersionDetector_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Visible = False
        File.WriteAllText(".\info.bat", OffEcho & CrLf & commandStr, ASCII)
        Process.Start(".\info.bat").WaitForExit()
        TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\info.txt")
        Try
            File.Delete(".\info.bat")
            File.Delete(".\info.txt")
        Catch ex As Exception
            Do
                File.Delete(".\info.bat")
                File.Delete(".\info.txt")
            Loop
        End Try
    End Sub
End Class