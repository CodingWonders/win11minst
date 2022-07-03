Imports System.IO
Imports System.Text.Encoding
Imports Microsoft.VisualBasic.ControlChars
Imports System.IO.DriveInfo

Public Class MainForm

    Private isMouseDown As Boolean = False
    Private mouseOffset As Point

    Dim IndexCountStr As String
    Dim WinInstInt As Integer = 0
    Public OffEcho As String = "@echo off"
    Dim VerStr As String = "2.0.0100_220703"
    Dim AVerStr As String = "2.0.100_2271"
    Dim PeInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo("\Windows\system32\ntoskrnl.exe")
    Dim PeBuild As String = PeInfo.FileVersion
    Dim PePrgVer As String = My.Application.Info.Version.ToString()

    ' Left mouse button pressed
    Private Sub titlePanel_MouseDown(sender As Object, e As MouseEventArgs) Handles titlePanel.MouseDown, TitleBar.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ' Get the new position
            mouseOffset = New Point(-e.X, -e.Y)
            ' Set that left button is pressed
            isMouseDown = True
        End If
    End Sub

    ' MouseMove used to check if mouse cursor is moving
    Private Sub titlePanel_MouseMove(sender As Object, e As MouseEventArgs) Handles titlePanel.MouseMove, TitleBar.MouseMove
        If isMouseDown Then
            Dim mousePos As Point = Control.MousePosition
            ' Get the new form position
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Location = mousePos
        End If
    End Sub

    ' Left mouse button released, form should stop moving
    Private Sub titlePanel_MouseUp(sender As Object, e As MouseEventArgs) Handles titlePanel.MouseUp, TitleBar.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            isMouseDown = False
        End If
    End Sub

    Private Sub closeBox_Click(sender As Object, e As EventArgs) Handles closeBox.Click
        End
    End Sub

    Private Sub closeBox_MouseEnter(sender As Object, e As EventArgs) Handles closeBox.MouseEnter
        closeBox.Image = New Bitmap(My.Resources.closebox_focus)
    End Sub

    Private Sub closeBox_MouseLeave(sender As Object, e As EventArgs) Handles closeBox.MouseLeave
        If BackColor = Color.FromArgb(243, 243, 243) Then
            closeBox.Image = New Bitmap(My.Resources.closebox)
        Else
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
        End If
    End Sub

    Private Sub closeBox_MouseDown(sender As Object, e As MouseEventArgs) Handles closeBox.MouseDown
        If BackColor = Color.FromArgb(243, 243, 243) Then
            closeBox.Image = New Bitmap(My.Resources.closebox_down)
        Else
            closeBox.Image = New Bitmap(My.Resources.closebox_dark_down)
        End If
    End Sub

    Private Sub closeBox_MouseUp(sender As Object, e As MouseEventArgs) Handles closeBox.MouseUp
        closeBox.Image = New Bitmap(My.Resources.closebox_focus)
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label72.Text = "version " & VerStr & " (assembly version " & AVerStr & ")"
        Label32.Text = "Pre-installation Environment build: " & PeBuild & ". Pre-installation Mode version: " & PePrgVer
    End Sub

    Sub EnableBackPic()
        back_Pic.Visible = True
        LogoPic.Left = 48
        ProgramTitleLabel.Left = 102
    End Sub

    Sub DisableBackPic()
        back_Pic.Visible = False
        LogoPic.Left = 19
        ProgramTitleLabel.Left = 73
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        EnableBackPic()
        WinInstInt += 1
        InstWelcomePanel.Visible = False
        DisclaimerPanel.Visible = True
    End Sub

    Private Sub back_Pic_Click(sender As Object, e As EventArgs) Handles back_Pic.Click
        If WinInstInt = 1 Then
            DisableBackPic()
            InstWelcomePanel.Visible = True
            DisclaimerPanel.Visible = False
        End If
    End Sub

    Private Sub DisclCheck_CheckedChanged(sender As Object, e As EventArgs) Handles DisclCheck.CheckedChanged
        If DisclCheck.Checked = True And GRUBCheck.Checked = True Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If
    End Sub

    Private Sub GRUBCheck_CheckedChanged(sender As Object, e As EventArgs) Handles GRUBCheck.CheckedChanged
        If DisclCheck.Checked = True And GRUBCheck.Checked = True Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If
    End Sub

    Sub DISMIndexes()
        If File.Exists(".\sources\install.wim") Then
            File.WriteAllText(".\index.bat", OffEcho & CrLf & "dism /English /get-wiminfo /wimfile=.\sources\install.wim | findstr Ind > .\index.txt", ASCII)
            Process.Start(".\index.bat").WaitForExit()
            File.Delete(".\index.bat")
            File.WriteAllText(".\indexfull.bat", OffEcho & CrLf & "dism /English /get-wiminfo /wimfile=.\sources\install.wim > .\indexfull.txt", ASCII)
            Process.Start(".\indexfull.bat").WaitForExit()
            File.Delete(".\indexfull.bat")
        ElseIf File.Exists(".\sources\install.esd") Then
            File.WriteAllText(".\index.bat", OffEcho & CrLf & "dism /English /get-wiminfo /wimfile=.\sources\install.esd | findstr Ind > .\index.txt", ASCII)
            Process.Start(".\index.bat").WaitForExit()
            File.Delete(".\index.bat")
            File.WriteAllText(".\indexfull.bat", OffEcho & CrLf & "dism /English /get-wiminfo /wimfile=.\sources\install.esd > .\indexfull.txt", ASCII)
            Process.Start(".\indexfull.bat").WaitForExit()
            File.Delete(".\indexfull.bat")
        End If
        IndexCountStr = My.Computer.FileSystem.ReadAllText(".\index.txt")
        IndexCountStr.Replace(" ", "").TrimEnd()
        WinEditionRTB.Text = My.Computer.FileSystem.ReadAllText(".\indexfull.txt")
        If IndexCountStr = "Index : 1" & vbCrLf & "" Then
            WinInstInt += 2
            DisclaimerPanel.Visible = False
            WinDestPanel.Visible = True
        Else
            WinInstInt += 1
            DisclaimerPanel.Visible = False
            WinEditionPanel.Visible = True
        End If
        File.Delete(".\index.txt")
        File.Delete(".\indexfull.txt")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DISMIndexes()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Process.Start("wpeutil", "reboot")
    End Sub

    Private Sub InfoPic_Click(sender As Object, e As EventArgs) Handles InfoPic.Click
        InstWelcomePanel.Visible = False
        DisclaimerPanel.Visible = False
        WinEditionPanel.Visible = False
        WinDestPanel.Visible = False
        InstPanel.Visible = False
        RestartPanel.Visible = False
        SettingPanel.Visible = False
        InfoPanel.Visible = True
        PanelIndicatorPic.Top = InfoPic.Top + 2
        PanelIndicatorPic.Anchor = CType((AnchorStyles.Bottom Or AnchorStyles.Left), AnchorStyles)
    End Sub

    Private Sub InstallerPic_Click(sender As Object, e As EventArgs) Handles InstallerPic.Click
        If WinInstInt = 0 Then
            InstWelcomePanel.Visible = True
            DisclaimerPanel.Visible = False
            WinEditionPanel.Visible = False
            WinDestPanel.Visible = False
            InstPanel.Visible = False
            RestartPanel.Visible = False
        ElseIf WinInstInt = 1 Then
            InstWelcomePanel.Visible = False
            DisclaimerPanel.Visible = True
            WinEditionPanel.Visible = False
            WinDestPanel.Visible = False
            InstPanel.Visible = False
            RestartPanel.Visible = False
        ElseIf WinInstInt = 2 Then
            InstWelcomePanel.Visible = False
            DisclaimerPanel.Visible = False
            WinEditionPanel.Visible = True
            WinDestPanel.Visible = False
            InstPanel.Visible = False
            RestartPanel.Visible = False
        ElseIf WinInstInt = 3 Then
            InstWelcomePanel.Visible = False
            DisclaimerPanel.Visible = False
            WinEditionPanel.Visible = False
            WinDestPanel.Visible = True
            InstPanel.Visible = False
            RestartPanel.Visible = False
        ElseIf WinInstInt = 4 Then
            InstWelcomePanel.Visible = False
            DisclaimerPanel.Visible = False
            WinEditionPanel.Visible = False
            WinDestPanel.Visible = False
            InstPanel.Visible = True
            RestartPanel.Visible = False
        ElseIf WinInstInt = 5 Then
            InstWelcomePanel.Visible = False
            DisclaimerPanel.Visible = False
            WinEditionPanel.Visible = False
            WinDestPanel.Visible = False
            InstPanel.Visible = False
            RestartPanel.Visible = True
        End If
        SettingPanel.Visible = False
        InfoPanel.Visible = False
        PanelIndicatorPic.Top = InstallerPic.Top + 2
        PanelIndicatorPic.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Left), AnchorStyles)
    End Sub

    Private Sub SettingsPic_Click(sender As Object, e As EventArgs) Handles SettingsPic.Click
        InstWelcomePanel.Visible = False
        DisclaimerPanel.Visible = False
        WinEditionPanel.Visible = False
        WinDestPanel.Visible = False
        InstPanel.Visible = False
        RestartPanel.Visible = False
        SettingPanel.Visible = True
        InfoPanel.Visible = False
        PanelIndicatorPic.Top = SettingsPic.Top + 2
        PanelIndicatorPic.Anchor = CType((AnchorStyles.Bottom Or AnchorStyles.Left), AnchorStyles)
    End Sub
End Class
