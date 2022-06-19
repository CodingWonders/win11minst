﻿' Visual Basic .NET is still not dead!

Imports Microsoft.Win32
Imports System.IO
Imports Microsoft.VisualBasic.ControlChars
Imports System.Windows.Forms.FlatStyle      ' This imports the various FlatStyle settings
Imports System.Text.Encoding
Imports System.Net
Imports System.Threading


Public Class MainForm
    Private isMouseDown As Boolean = False
    Private mouseOffset As Point
    Dim VerStr As String = "2.0.0100_220619"    ' Reported version
    Dim AVerStr As String = My.Application.Info.Version.ToString()     ' Assembly version
    Dim VDescStr As String = "Even though we all hated Internet Explorer, it brought the Internet to a mainstream audience"
    Dim OffEcho As String = "@echo off"
    Dim wmiget As String
    Dim StDebugTime As Date = Now
    Dim InstCreateInt As Integer
    Dim SettingsInt As Integer
    ' The following line of code dims the end debug time variable, but does NOT set it
    Dim EnDebugTime As Date
    ' Progress panel variables
    Dim ErrorCount As Integer
    Dim WarnCount As Integer
    Dim MessageCount As Integer
    ' These variables are reused from 1.0.x builds
    Dim W11IWIMISOEx As String
    Dim W10ISOEx As String
    Dim W11ISOEx As String
    Dim W10AR_ARRDLLEx As String
    Dim W10AR_ARRDLLEx_2 As String
    Dim OSCDIMG_CSM As String
    Dim OSCDIMG_UEFI As String
    Dim EmergencyFolderDelete As String
    ' These variables are used when images have an install.esd file
    Dim W11IESDISOEx As String
    Dim W11IESDISOEx_Local As String = ".\prog_bin\7z e " & Quote & ".\Win11.iso" & Quote & " " & Quote & "sources\install.esd" & " -o."    ' "echo off" is now gone because of OffEcho
    ' These variables are used by the LogBox
    Dim StInstCreateTime As Date    ' These variables aren't set when the program loads, only on the installer creation process
    Dim EnInstCreateTime As Date    ' <---|
    ' This variable is used to kill external processes
    Dim KillExtCmd As String
    ' These variables are used when aborting an installer creation when using REGTWEAK
    Dim RegUnload As String
    Dim DismUnmount As String
    ' This variable is used to check for WIM or ESD existence on the provided installers
    Dim WimEsd As String
    Dim Win11ESD As Integer = 0
    ' These _Local variables are used when the ISO files are copied to the local disk
    Dim W11IWIMISOEx_Local As String = ".\prog_bin\7z e " & Quote & ".\Win11.iso" & Quote & " " & Quote & "sources\install.wim" & Quote & " -o."
    Dim W10ISOEx_Local As String = ".\prog_bin\7z x " & Quote & ".\Win10.iso" & Quote & " " & Quote & "-o.\temp" & Quote
    Dim W11ISOEx_Local As String = ".\prog_bin\7z x " & Quote & ".\Win11.iso" & Quote & " " & Quote & "-o.\temp" & Quote
    Dim W10AR_ARRDLLEx_Local As String = ".\prog_bin\7z e " & Quote & ".\Win10.iso" & Quote & " " & Quote & "sources\appraiser.dll" & Quote & " -o."
    Dim W10AR_ARRDLLEx_2_Local As String = ".\prog_bin\7z e " & Quote & ".\Win10.iso" & Quote & " " & Quote & "sources\appraiserres.dll" & Quote & " -o."
    ' These variables are used when deleting temporary files (as integers)
    Dim FileCount As Integer
    Dim DelFileCount As Integer
    Dim EmergencyFileDeleteCount As Integer
    Dim UpdateCheckDate As Date
    Dim DialogInt As Integer
    Dim ColorInt As Integer
    Dim LangInt As Integer
    Dim NotifyNum As Integer
    Dim isHummingbird As Boolean = True
    Dim needsUpdates As Boolean
    Dim WndLeft As Point
    Dim WndTop As Point
    Dim SevenZipVer As FileVersionInfo
    Dim SevenZipStr As String
    Dim OSCDIMGVer As FileVersionInfo
    Dim OSCDIMGStr As String
    Dim DismVer As FileVersionInfo
    Dim DismStr As String
    Dim MOLinkIsClicked As Boolean

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
    ' Left mouse button pressed
    Private Sub LogoPic_MouseDown(sender As Object, e As MouseEventArgs) Handles LogoPic.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ' Get the new position
            mouseOffset = New Point(-e.X, -e.Y)
            ' Set that left button is pressed
            isMouseDown = True
        End If
    End Sub

    ' MouseMove used to check if mouse cursor is moving
    Private Sub LogoPic_MouseMove(sender As Object, e As MouseEventArgs) Handles LogoPic.MouseMove
        If isMouseDown Then
            Dim mousePos As Point = Control.MousePosition
            ' Get the new form position
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Location = mousePos
        End If
    End Sub

    ' Left mouse button released, form should stop moving
    Private Sub LogoPic_MouseUp(sender As Object, e As MouseEventArgs) Handles LogoPic.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            isMouseDown = False
        End If
    End Sub
    ' Left mouse button pressed
    Private Sub ProgramTitleLabel_MouseDown(sender As Object, e As MouseEventArgs) Handles ProgramTitleLabel.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ' Get the new position
            mouseOffset = New Point(-e.X, -e.Y)
            ' Set that left button is pressed
            isMouseDown = True
        End If
    End Sub

    ' MouseMove used to check if mouse cursor is moving
    Private Sub ProgramTitleLabel_MouseMove(sender As Object, e As MouseEventArgs) Handles ProgramTitleLabel.MouseMove
        If isMouseDown Then
            Dim mousePos As Point = Control.MousePosition
            ' Get the new form position
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Location = mousePos
        End If
    End Sub

    ' Left mouse button released, form should stop moving
    Private Sub ProgramTitleLabel_MouseUp(sender As Object, e As MouseEventArgs) Handles ProgramTitleLabel.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            isMouseDown = False
        End If
    End Sub
    ' Left mouse button pressed
    Private Sub AdminLabel_MouseDown(sender As Object, e As MouseEventArgs) Handles AdminLabel.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ' Get the new position
            mouseOffset = New Point(-e.X, -e.Y)
            ' Set that left button is pressed
            isMouseDown = True
        End If
    End Sub

    ' MouseMove used to check if mouse cursor is moving
    Private Sub AdminLabel_MouseMove(sender As Object, e As MouseEventArgs) Handles AdminLabel.MouseMove
        If isMouseDown Then
            Dim mousePos As Point = Control.MousePosition
            ' Get the new form position
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Location = mousePos
        End If
    End Sub

    ' Left mouse button released, form should stop moving
    Private Sub AdminLabel_MouseUp(sender As Object, e As MouseEventArgs) Handles AdminLabel.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            isMouseDown = False
        End If
    End Sub

    Private Sub closeBox_Click(sender As Object, e As EventArgs) Handles closeBox.Click, LogoPic.MouseDoubleClick
        If CheckBox3.Checked = True Then
            ' The following code snippet determines the check state of "Don't show this again"
            If Not File.Exists(".\noshow") Then
                MiniModeDialog.Show()
            End If
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                Notify.BalloonTipTitle = "The program is running in the background."
                Notify.BalloonTipText = "Click this message to bring it back."
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                Notify.BalloonTipTitle = "El programa se está ejecutando en segundo plano."
                Notify.BalloonTipText = "Haga clic en este mensaje para traerlo de vuelta."
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                Notify.BalloonTipTitle = "Le programme fonctionne en arrière-plan."
                Notify.BalloonTipText = "Cliquez sur ce message pour le faire réapparaître."
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    Notify.BalloonTipTitle = "The program is running in the background."
                    Notify.BalloonTipText = "Click this message to bring it back."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    Notify.BalloonTipTitle = "El programa se está ejecutando en segundo plano."
                    Notify.BalloonTipText = "Haga clic en este mensaje para traerlo de vuelta."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    Notify.BalloonTipTitle = "Le programme fonctionne en arrière-plan."
                    Notify.BalloonTipText = "Cliquez sur ce message pour le faire réapparaître."
                End If
            End If
            If CheckBox1.Checked = True And CheckBox1.Enabled = True Then
                If NotifyNum = 0 Then
                    NotifyNum += 1
                    Notify.ShowBalloonTip(5)
                ElseIf NotifyNum = 1 Then
                    ' Do not show the notification
                End If
            Else
                Notify.ShowBalloonTip(5)
            End If
            WindowState = FormWindowState.Minimized
            ShowInTaskbar = False
        Else
            If InstCreateInt = 2 Then
                BringToFront()
                BackSubPanel.Show()
                InstCreateAbortPanel.ShowDialog()
                InstCreateAbortPanel.Visible = True
                InstCreateAbortPanel.Visible = False
                BringToFront()
                If InstCreateAbortPanel.DialogResult = Windows.Forms.DialogResult.OK Then
                    ' Abort EVERYTHING
                    KillExtCmd = "taskkill /f /im cmd.exe /t"
                    File.WriteAllText(".\kill.bat", OffEcho & CrLf & KillExtCmd, ASCII)
                    Process.Start(".\kill.bat").WaitForExit()
                    If File.Exists(".\Win11.iso") Then
                        If File.Exists(".\Win10.iso") Then
                            File.Delete(".\Win11.iso")
                            File.Delete(".\Win10.iso")
                        Else
                            File.Delete(".\Win11.iso")
                        End If
                    End If
                    If File.Exists(".\install.wim") Then
                        File.Delete(".\install.wim")
                    End If
                    If File.Exists(".\boot.wim") Then
                        If Directory.Exists(".\wimmount") Then
                            RegUnload = "reg unload HKLM\W11SYS"
                            DismUnmount = "dism /English /unmount-wim /mountdir=.\wimmount /discard"
                            File.WriteAllText(".\temp.bat", OffEcho & CrLf & RegUnload & CrLf & DismUnmount, ASCII)
                            Process.Start(".\temp.bat").WaitForExit()
                            Directory.Delete(".\wimmount")
                        End If
                        File.Delete(".\boot.wim")
                    End If
                    If Directory.Exists(".\temp") Then
                        For Each deletedFile In My.Computer.FileSystem.GetFiles(".\temp", FileIO.SearchOption.SearchAllSubDirectories)
                            Try
                                LogBox.AppendText(CrLf & "Deleted file: " & deletedFile)
                                File.Delete(deletedFile)
                            Catch PTLEx As PathTooLongException
                                LogBox.AppendText(CrLf & "Cannot display the file being deleted right now, because its path length surpasses the allowed one")
                            End Try
                        Next
                        LogBox.AppendText(CrLf & "Temp folder cleaned. Deleting it...")
                        Try
                            Directory.Delete(".\temp")
                        Catch IOEx As IOException
                            LogBox.AppendText(CrLf & "Exception: 'IOException' caught at runtime, performing emergency method...")
                            File.WriteAllText(".\temp.bat", OffEcho & CrLf & EmergencyFolderDelete, ASCII)
                            Process.Start(".\temp.bat").WaitForExit()
                        End Try
                    End If
                    If Debugger.IsAttached = True Then
                        EnDebugTime = Now
                        MsgBox("Debug started at: " & StDebugTime & CrLf & "Debug ended at: " & EnDebugTime & CrLf & "Machine information:" & CrLf & "Name: " & My.Computer.Name & CrLf & "Total memory: " & My.Computer.Info.TotalPhysicalMemory & "KB" & CrLf & "Operating system: " & My.Computer.Info.OSFullName, vbOKOnly + vbInformation, "Debug mode")
                        If DialogResult.OK Then
                            Notify.Visible = False
                            End
                        End If
                    End If
                    Notify.Visible = False
                    End
                ElseIf InstCreateAbortPanel.DialogResult = Windows.Forms.DialogResult.Cancel Then

                End If
            Else
                If Debugger.IsAttached = True Then
                    EnDebugTime = Now
                    MsgBox("Debug started at: " & StDebugTime & CrLf & "Debug ended at: " & EnDebugTime & CrLf & "Machine information:" & CrLf & "Name: " & My.Computer.Name & CrLf & "Total memory: " & My.Computer.Info.TotalPhysicalMemory & " KB" & CrLf & "Operating system: " & My.Computer.Info.OSFullName & CrLf & "Reported OS build: " & My.Computer.Info.OSVersion, vbOKOnly + vbInformation, "Debug mode")
                    If DialogResult.OK Then
                        Notify.Visible = False
                        End
                    End If
                Else
                    Notify.Visible = False
                    SaveSettingsFile()
                    End
                End If
            End If
        End If
    End Sub

    Private Sub closeBox_MouseEnter(sender As Object, e As EventArgs) Handles closeBox.MouseEnter
        closeBox.Image = New Bitmap(My.Resources.closebox_focus)
        TopRightResizePanel.BackColor = Color.FromArgb(196, 43, 28)
    End Sub

    Private Sub closeBox_MouseLeave(sender As Object, e As EventArgs) Handles closeBox.MouseLeave
        If BackColor = Color.FromArgb(243, 243, 243) Then
            closeBox.Image = New Bitmap(My.Resources.closebox)
        Else
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
        End If
        TopRightResizePanel.BackColor = BackColor
    End Sub

    Private Sub minBox_Click(sender As Object, e As EventArgs) Handles minBox.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub minBox_MouseEnter(sender As Object, e As EventArgs) Handles minBox.MouseEnter
        If BackColor = Color.FromArgb(243, 243, 243) Then
            minBox.Image = New Bitmap(My.Resources.minBox_focus)
        Else
            minBox.Image = New Bitmap(My.Resources.minBox_dark_focus)
        End If
    End Sub

    Private Sub minBox_MouseLeave(sender As Object, e As EventArgs) Handles minBox.MouseLeave
        If BackColor = Color.FromArgb(243, 243, 243) Then
            minBox.Image = New Bitmap(My.Resources.minBox)
        Else
            minBox.Image = New Bitmap(My.Resources.minBox_dark)
        End If
    End Sub

    Private Sub maxBox_Click(sender As Object, e As EventArgs) Handles maxBox.Click
        If WindowState = FormWindowState.Normal Then
            WndLeft = New Point(Left)
            WndTop = New Point(Top)
            MaximumSize = Screen.FromControl(Me).WorkingArea.Size
            WindowState = FormWindowState.Maximized
            If BackColor = Color.FromArgb(243, 243, 243) Then
                maxBox.Image = New Bitmap(My.Resources.restdownbox)
            Else
                maxBox.Image = New Bitmap(My.Resources.restdownbox_dark)
            End If
        ElseIf WindowState = FormWindowState.Maximized Then
            If Left = 0 And Top = 0 Then
                Location = New Point(WndLeft.X, WndTop.Y)
            End If
            WindowState = FormWindowState.Normal
            If BackColor = Color.FromArgb(243, 243, 243) Then
                maxBox.Image = New Bitmap(My.Resources.maxbox)
            Else
                maxBox.Image = New Bitmap(My.Resources.maxbox_dark)
            End If
        End If
        If SettingPanel.Visible = True Or Settings_PersonalizationPanel.Visible = True Or Settings_FunctionalityPanel.Visible = True Then
            PanelIndicatorPic.Top = SettingsPic.Top + 2
        End If
        If InfoPanel.Visible = True Then
            PanelIndicatorPic.Top = InfoPic.Top + 2
        End If
    End Sub

    Private Sub maxBox_MouseEnter(sender As Object, e As EventArgs) Handles maxBox.MouseEnter
        If WindowState = FormWindowState.Maximized Then
            If BackColor = Color.FromArgb(243, 243, 243) Then
                maxBox.Image = New Bitmap(My.Resources.restdownbox_focus)
            Else
                maxBox.Image = New Bitmap(My.Resources.restdownbox_dark_focus)
            End If
        ElseIf WindowState = FormWindowState.Normal Then
            If BackColor = Color.FromArgb(243, 243, 243) Then
                maxBox.Image = New Bitmap(My.Resources.maxbox_focus)
            Else
                maxBox.Image = New Bitmap(My.Resources.maxbox_dark_focus)
            End If
        End If
    End Sub

    Private Sub maxBox_MouseLeave(sender As Object, e As EventArgs) Handles maxBox.MouseLeave
        If WindowState = FormWindowState.Maximized Then
            If BackColor = Color.FromArgb(243, 243, 243) Then
                maxBox.Image = New Bitmap(My.Resources.restdownbox)
            Else
                maxBox.Image = New Bitmap(My.Resources.restdownbox_dark)
            End If
        ElseIf WindowState = FormWindowState.Normal Then
            If BackColor = Color.FromArgb(243, 243, 243) Then
                maxBox.Image = New Bitmap(My.Resources.maxbox)
            Else
                maxBox.Image = New Bitmap(My.Resources.maxbox_dark)
            End If
        End If
    End Sub

    Private Sub titlePanel_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles titlePanel.MouseDoubleClick, Label12.MouseDoubleClick, TitleBar.MouseDoubleClick
        If WindowState = FormWindowState.Normal Then
            MaximumSize = Screen.FromControl(Me).WorkingArea.Size
            WindowState = FormWindowState.Maximized
            If BackColor = Color.FromArgb(243, 243, 243) Then
                maxBox.Image = New Bitmap(My.Resources.restdownbox)
            Else
                maxBox.Image = New Bitmap(My.Resources.restdownbox_dark)
            End If
        ElseIf WindowState = FormWindowState.Maximized Then
            WindowState = FormWindowState.Normal
            If BackColor = Color.FromArgb(243, 243, 243) Then
                maxBox.Image = New Bitmap(My.Resources.maxbox)
            Else
                maxBox.Image = New Bitmap(My.Resources.maxbox_dark)
            End If
        End If
        If SettingPanel.Visible = True Or Settings_PersonalizationPanel.Visible = True Or Settings_FunctionalityPanel.Visible = True Then
            PanelIndicatorPic.Top = SettingsPic.Top + 2
        End If
        If InfoPanel.Visible = True Then
            PanelIndicatorPic.Top = InfoPic.Top + 2
        End If
    End Sub

    Sub LoadSettingsFile()
        If File.Exists(".\settings.ini") Then
            SettingLoadForm.TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\settings.ini", UTF8)
            If SettingLoadForm.TextBox1.Text.Contains("ColorMode=2") Then       ' This does color mode checkup
                ComboBox1.SelectedItem = "Light"
            ElseIf SettingLoadForm.TextBox1.Text.Contains("ColorMode=1") Then
                ComboBox1.SelectedItem = "Dark"
            ElseIf SettingLoadForm.TextBox1.Text.Contains("ColorMode=0") Then
                If My.Computer.Info.OSFullName.Contains("Windows 7") Or My.Computer.Info.OSFullName.Contains("Windows 8") Or My.Computer.Info.OSFullName.Contains("Windows Server 2008") Or My.Computer.Info.OSFullName.Contains("Windows Server 2012") Then      ' If the program detects these settings, it will default to Light mode as a fallback mechanism
                    ComboBox1.SelectedItem = "Light"
                Else
                    ComboBox1.SelectedItem = "Automatic"
                End If
            End If
            If SettingLoadForm.TextBox1.Text.Contains("Language=1") Then
                LangInt = 1
                ComboBox4.SelectedItem = "English"
                AutomaticLanguageToolStripMenuItem.Checked = False
                EnglishToolStripMenuItem.Checked = True
                SpanishToolStripMenuItem.Checked = False
                FrenchToolStripMenuItem.Checked = False
            ElseIf SettingLoadForm.TextBox1.Text.Contains("Language=2") Then
                LangInt = 2
                ComboBox4.SelectedItem = "Spanish"
                AutomaticLanguageToolStripMenuItem.Checked = False
                EnglishToolStripMenuItem.Checked = False
                SpanishToolStripMenuItem.Checked = True
                FrenchToolStripMenuItem.Checked = False
            ElseIf SettingLoadForm.TextBox1.Text.Contains("Language=3") Then
                LangInt = 3
                ComboBox4.SelectedItem = "French"
                AutomaticLanguageToolStripMenuItem.Checked = False
                EnglishToolStripMenuItem.Checked = False
                SpanishToolStripMenuItem.Checked = False
                FrenchToolStripMenuItem.Checked = True
            ElseIf SettingLoadForm.TextBox1.Text.Contains("Language=0") Then
                LangInt = 0
                ComboBox4.SelectedItem = "Automatic"
                AutomaticLanguageToolStripMenuItem.Checked = True
                EnglishToolStripMenuItem.Checked = False
                SpanishToolStripMenuItem.Checked = False
                FrenchToolStripMenuItem.Checked = False
            End If
            If SettingLoadForm.TextBox1.Text.Contains("NavBarPos=0") Then
                RadioButton3.Checked = True
            ElseIf SettingLoadForm.TextBox1.Text.Contains("NavBarPos=1") Then
                RadioButton3.Checked = False
            End If
            ' Functionality
            If SettingLoadForm.TextBox1.Text.Contains("AdminMode=1") Then
                If Not My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
                    ComboBox5.Items.Clear()
                    ComboBox5.Items.Add("WIMR")
                    ComboBox5.Items.Add("DLLR")
                    ComboBox5.SelectedItem = "WIMR"
                    WIMRToolStripMenuItem.Checked = True
                    REGTWEAKToolStripMenuItem.Visible = False
                End If
            ElseIf SettingLoadForm.TextBox1.Text.Contains("AdminMode=0") Then
                If SettingLoadForm.TextBox1.Text.Contains("Method=REGTWEAK") Then
                    ComboBox5.Items.Clear()
                    ComboBox5.Items.Add("WIMR")
                    ComboBox5.Items.Add("DLLR")
                    ComboBox5.SelectedItem = "WIMR"
                    WIMRToolStripMenuItem.Checked = True
                    REGTWEAKToolStripMenuItem.Visible = False
                End If
            End If
            If SettingLoadForm.TextBox1.Text.Contains("BiosCompat=1") Then
                RadioButton1.Checked = True
                RadioButton2.Checked = False
            ElseIf SettingLoadForm.TextBox1.Text.Contains("BiosCompat=0") Then
                RadioButton1.Checked = False
                RadioButton2.Checked = True
            End If
            If SettingLoadForm.TextBox1.Text.Contains("HideSysTray=1") Then
                CheckBox3.Checked = True
            ElseIf SettingLoadForm.TextBox1.Text.Contains("HideSysTray=0") Then
                CheckBox3.Checked = False
            End If
            If SettingLoadForm.TextBox1.Text.Contains("ExitProgramAfterFinish=1") Then
                CheckBox4.Checked = True
            ElseIf SettingLoadForm.TextBox1.Text.Contains("ExitProgramAfterFinish=0") Then
                CheckBox4.Checked = False
            End If
            If SettingLoadForm.TextBox1.Text.Contains("ShowOnce=1") Then
                If CheckBox3.Checked = True Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Enabled = False
                    CheckBox1.Checked = False
                End If
            ElseIf SettingLoadForm.TextBox1.Text.Contains("ShowOnce=0") Then
                CheckBox1.Checked = False
            End If
            If SettingLoadForm.TextBox1.Text.Contains("ReuseSI=1") Then
                InstProjectReuseDialog.ShowDialog()
            End If

        Else
            SaveSettingsFile()
        End If
    End Sub

    Sub SaveSettingsFile()
        If File.Exists(".\settings.ini") Then
            File.Delete(".\settings.ini")
        End If
        SettingLoadForm.TextBox2.Clear()
        SettingLoadForm.TextBox2.AppendText("# Settings file for the Windows 11 Manual Installer" & CrLf & CrLf & "[Personalization]" & CrLf)
        If ComboBox1.SelectedItem = "Light" Or ComboBox1.SelectedItem = "Claro" Or ComboBox1.SelectedItem = "Lumière" Then
            SettingLoadForm.TextBox2.AppendText("ColorMode=2")
        ElseIf ComboBox1.SelectedItem = "Dark" Or ComboBox1.SelectedItem = "Oscuro" Or ComboBox1.SelectedItem = "Sombre" Then
            SettingLoadForm.TextBox2.AppendText("ColorMode=1")
        ElseIf ComboBox1.SelectedItem = "Automatic" Or ComboBox1.SelectedItem = "Automático" Or ComboBox1.SelectedItem = "Automatique" Then
            SettingLoadForm.TextBox2.AppendText("ColorMode=0")
        End If
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            SettingLoadForm.TextBox2.AppendText(CrLf & "Language=1")
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            SettingLoadForm.TextBox2.AppendText(CrLf & "Language=2")
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            SettingLoadForm.TextBox2.AppendText(CrLf & "Language=3")
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            SettingLoadForm.TextBox2.AppendText(CrLf & "Language=0")
        End If
        If RadioButton3.Checked = True Then
            SettingLoadForm.TextBox2.AppendText(CrLf & "NavBarPos=0")
        Else
            SettingLoadForm.TextBox2.AppendText(CrLf & "NavBarPos=1")
        End If
        SettingLoadForm.TextBox2.AppendText(CrLf & CrLf & "[Functionality]" & CrLf)
        If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
            SettingLoadForm.TextBox2.AppendText("AdminMode=1")
        Else
            SettingLoadForm.TextBox2.AppendText("AdminMode=0")
        End If
        SettingLoadForm.TextBox2.AppendText(CrLf & "Method=" & ComboBox5.SelectedItem)
        If RadioButton1.Checked = True Then
            SettingLoadForm.TextBox2.AppendText(CrLf & "BiosCompat=1")
        Else
            SettingLoadForm.TextBox2.AppendText(CrLf & "BiosCompat=0")
        End If
        ' This condition doesn't change this setting on debug sessions
        If Debugger.IsAttached = True Then
            SettingLoadForm.TextBox2.AppendText(CrLf & "HideSysTray=0")
        Else
            If CheckBox3.Checked = True Then
                SettingLoadForm.TextBox2.AppendText(CrLf & "HideSysTray=1")
            Else
                SettingLoadForm.TextBox2.AppendText(CrLf & "HideSysTray=0")
            End If
        End If
        If CheckBox4.Checked = True Then
            SettingLoadForm.TextBox2.AppendText(CrLf & "ExitProgramAfterFinish=1")
        Else
            SettingLoadForm.TextBox2.AppendText(CrLf & "ExitProgramAfterFinish=0")
        End If
        If CheckBox1.Enabled = True Then
            If CheckBox1.Checked = True Then
                SettingLoadForm.TextBox2.AppendText(CrLf & "ShowOnce=1")
            Else
                SettingLoadForm.TextBox2.AppendText(CrLf & "ShowOnce=0")
            End If
        Else
            SettingLoadForm.TextBox2.AppendText(CrLf & "ShowOnce=0")
        End If
        SettingLoadForm.TextBox2.AppendText(CrLf & CrLf & "[ICOptn]" & CrLf)

        If SettingReviewPanel.Visible = True Then
            My.Computer.FileSystem.WriteAllText(".\InstName.ini", TextBox3.Text, False)
            My.Computer.FileSystem.WriteAllText(".\Win11Inst.ini", TextBox1.Text, False)
            My.Computer.FileSystem.WriteAllText(".\Win10Inst.ini", TextBox2.Text, False)
            My.Computer.FileSystem.WriteAllText(".\TargetInstaller.ini", TextBox4.Text, False)
            SettingLoadForm.TextBox2.AppendText("ReuseSI=1")
        Else
            SettingLoadForm.TextBox2.AppendText("ReuseSI=0")
        End If
        File.WriteAllText(".\settings.ini", SettingLoadForm.TextBox2.Text, ASCII)
    End Sub

    Sub KillCmdWnd()    ' After updating to a newer version, a CMD window will still be open. This solves it
        File.WriteAllText(".\kill.bat", "@echo off" & CrLf & "taskkill /f /im cmd.exe /t", ASCII)
        Dim KillCmd As New ProcessStartInfo()
        KillCmd.WorkingDirectory = Directory.GetCurrentDirectory()
        KillCmd.CreateNoWindow = True
        KillCmd.FileName = ".\kill.bat"
        Dim KillCmdProc As Process = Process.Start(KillCmd)
    End Sub

    Public Sub DetectCompVersion()
        SevenZipVer = FileVersionInfo.GetVersionInfo(".\prog_bin\7z.exe")
        SevenZipStr = SevenZipVer.FileVersion
        OSCDIMGVer = FileVersionInfo.GetVersionInfo(".\prog_bin\oscdimg.exe")
        OSCDIMGStr = OSCDIMGVer.FileVersion
        DismVer = FileVersionInfo.GetVersionInfo("\Windows\System32\dism.exe")
        DismStr = DismVer.FileVersion
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KillCmdWnd()
        VersionToolStripMenuItem.Text = "version " & VerStr & " (assembly version " & AVerStr & ")"
        Label72.Text = "version " & VerStr & " (assembly version " & AVerStr & ")"
        Label74.Visible = True
        If VDescStr = "" Then
            Label74.Text = "Blame Microsoft for pushing the system requirements, not your computer for not meeting them."
        Else
            Label74.Text = VDescStr
        End If
        Notify.Visible = False
        If isHummingbird Then
            BranchPic.Visible = True
            Label106.Visible = True
        Else
            BranchPic.Visible = False
            Label106.Visible = False
        End If
        If My.Computer.Info.OSFullName.Contains("Windows 7") Or My.Computer.Info.OSFullName.Contains("Windows 8") Or My.Computer.Info.OSFullName.Contains("Windows Server 2008") Or My.Computer.Info.OSFullName.Contains("Windows Server 2012") Then      ' This is done to not show the "Not supported" dialog on Windows 7 and 8/8.1
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Light")
            ComboBox1.Items.Add("Dark")
            ComboBox1.SelectedItem = "Light"
            SettingsPic.Image = New Bitmap(My.Resources.settings)
        Else
            ComboBox1.SelectedItem = "Automatic"
        End If
        LoadSettingsFile()
        If Not Directory.Exists(".\prog_bin") Then
            MissingComponentsDialog.ShowDialog()
        Else
            If Not File.Exists(".\prog_bin\7z.exe") And Not File.Exists(".\prog_bin\7z.dll") Or Not File.Exists(".\prog_bin\oscdimg.exe") Then
                MissingComponentsDialog.ShowDialog()
            End If
        End If
        If File.Exists(".\version") Then
            needsUpdates = False
            UpdateCheckPreLoadPanel.ShowDialog()
        Else
            needsUpdates = True
            My.Computer.FileSystem.WriteAllText(".\version", VerStr, False, ASCII)
        End If
        If needsUpdates Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                Label91.Text = "Updates bring new features and bugfixes to this program. Click " & Quote & "Check for updates" & Quote & " to check for program updates."
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                Label91.Text = "Las actualizaciones aportan nuevas características y correcciones de errores al programa. Haga clic en " & Quote & "Comprobar actualizaciones" & Quote & " para comprobar actualizaciones del programa."
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                Label91.Text = "Les mises à jour apportent de nouvelles fonctionnalités et des corrections de bogues à ce programme. Cliquez sur " & Quote & "Vérifier les mises à jour" & Quote & " pour vérifier les mises à jour du programme."
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    Label91.Text = "Updates bring new features and bugfixes to this program. Click " & Quote & "Check for updates" & Quote & " to check for program updates."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    Label91.Text = "Las actualizaciones aportan nuevas características y correcciones de errores al programa. Haga clic en " & Quote & "Comprobar actualizaciones" & Quote & " para comprobar actualizaciones del programa."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    Label91.Text = "Les mises à jour apportent de nouvelles fonctionnalités et des corrections de bogues à ce programme. Cliquez sur " & Quote & "Vérifier les mises à jour" & Quote & " pour vérifier les mises à jour du programme."
                End If
            End If
        End If
        DetectCompVersion()
        Label94.Text = Label94.Text & " " & SevenZipStr
        Label95.Text = Label95.Text & " " & DismStr
        Label96.Text = Label96.Text & " " & OSCDIMGStr
        ErrorCount = 0
        WarnCount = 0
        MessageCount = 0
        If My.User.IsAuthenticated = True Then
            If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
                AdminLabel.Visible = True
                Text = "Windows 11 Manual Installer (administrator mode)"
            Else
                Button13.Visible = False
                AdminLabel.Visible = False
                ComboBox5.Items.Clear()
                ComboBox5.Items.Add("WIMR")
                ComboBox5.Items.Add("DLLR")
                ComboBox5.SelectedItem = "WIMR"
                ' Set the REGTWEAK CMS element invisible
                WIMRToolStripMenuItem.Checked = True
                REGTWEAKToolStripMenuItem.Visible = False
                AOTSMI.Visible = False
            End If
        End If
        Notify.Visible = True
        Dim HTMLname As String = "HelpPage.html"
        With WebBrowser1
            .ScriptErrorsSuppressed = True
            .Url = New Uri(String.Format("file:///{0}{1}{2}/", Directory.GetCurrentDirectory(), "/Resources/HTMLHelp/", HTMLname))
        End With
        NameLabel.Text = "Name: " & TextBox3.Text
        Label28.Text = "DPI scale to be applied: " & TrackBar1.Value & "%"
        InstCreateInt = 0
        If Not InstProjectReuseDialog.DialogResult = Windows.Forms.DialogResult.OK Then
            TextBox4.Text = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        End If
        ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
        ' This condition is used for debugging purposes
        If Debugger.IsAttached = True Then
            SettingsPic.Top = 460
            InfoPic.Top = 490
            ' This will close the program during debugging and testing. This will not hide it to the
            ' system tray
            CheckBox3.Checked = False
            DebugPic.Visible = True
            Label103.Visible = True
            DebugPanel.TextBox1.AppendText("Debug mode initiated" & CrLf & "Start time: " & StDebugTime & CrLf & "Machine information:" & CrLf & "Name: " & My.Computer.Name & CrLf & "Total memory: " & My.Computer.Info.TotalPhysicalMemory & " KB" & CrLf & "OS: " & My.Computer.Info.OSFullName & CrLf & "Reported OS build: " & My.Computer.Info.OSVersion & CrLf)
        End If
        ' The following condition will determine the operating system*, and set the Button FlatStyle,
        ' due to a visual artifact in earlier Windows versions than Windows 11 when setting to Dark mode**

        ' * ** This condition is now gone. I have tested build 22557 of Windows 11 and the buttons's foreground colors are white on program dark mode
        Button1.FlatStyle = FlatStyle.System
        Button2.FlatStyle = FlatStyle.System
        Button3.FlatStyle = FlatStyle.System
        Button4.FlatStyle = FlatStyle.System
        Button5.FlatStyle = FlatStyle.System
        Button6.FlatStyle = FlatStyle.System
        Button7.FlatStyle = FlatStyle.System
        Button8.FlatStyle = FlatStyle.System
        Button9.FlatStyle = FlatStyle.System
        Button10.FlatStyle = FlatStyle.System
        ' Button11.FlatStyle = FlatStyle.System
        Button12.FlatStyle = FlatStyle.System
        Button13.FlatStyle = FlatStyle.System

        ' Space intentionally left for more buttons
        ScanButton.FlatStyle = FlatStyle.System
        LabelSetButton.FlatStyle = FlatStyle.System
        SetDefaultButton.FlatStyle = FlatStyle.System

        ' Set Parent properties for labels and pictureboxes
        Label44.Parent = PictureBox21
        Label43.Parent = PictureBox21
        PictureBox20.Parent = PictureBox21
        Label46.Parent = PictureBox23
        Label45.Parent = PictureBox23
        PictureBox22.Parent = PictureBox23
        Label48.Parent = PictureBox25
        Label47.Parent = PictureBox25
        PictureBox24.Parent = PictureBox25
        Label50.Parent = PictureBox27
        Label49.Parent = PictureBox27
        PictureBox26.Parent = PictureBox27
        Label51.Parent = PictureBox29
        Label2.Parent = PictureBox29
        PictureBox28.Parent = PictureBox29
        Label35.Parent = PictureBox13
        Label36.Parent = PictureBox13
        Label37.Parent = PictureBox14
        Label38.Parent = PictureBox14
        Label39.Parent = PictureBox17
        Label40.Parent = PictureBox17
        Label41.Parent = PictureBox19
        Label42.Parent = PictureBox19
        PictureBox15.Parent = PictureBox13
        PictureBox16.Parent = PictureBox14
        PictureBox4.Parent = PictureBox17
        PictureBox18.Parent = PictureBox19
        Label44.BackColor = Color.Transparent
        Label43.BackColor = Color.Transparent
        PictureBox20.BackColor = Color.Transparent
        Label46.BackColor = Color.Transparent
        Label45.BackColor = Color.Transparent
        PictureBox22.BackColor = Color.Transparent
        Label48.BackColor = Color.Transparent
        Label47.BackColor = Color.Transparent
        PictureBox24.BackColor = Color.Transparent
        Label50.BackColor = Color.Transparent
        Label49.BackColor = Color.Transparent
        PictureBox26.BackColor = Color.Transparent
        Label51.BackColor = Color.Transparent
        Label2.BackColor = Color.Transparent
        PictureBox28.BackColor = Color.Transparent
        Label35.BackColor = Color.Transparent
        Label36.BackColor = Color.Transparent
        Label37.BackColor = Color.Transparent
        Label38.BackColor = Color.Transparent
        Label39.BackColor = Color.Transparent
        Label40.BackColor = Color.Transparent
        Label41.BackColor = Color.Transparent
        Label42.BackColor = Color.Transparent
        PictureBox15.BackColor = Color.Transparent
        PictureBox16.BackColor = Color.Transparent
        PictureBox4.BackColor = Color.Transparent
        PictureBox18.BackColor = Color.Transparent

        ' This is for the top-right resize panel, to not make the
        ' thought that someone has bitten the Close button (bite into an apple, not into a close button)
        TopRightResizePanel.BackColor = BackColor
        BottomRightResizePanel.BackColor = WelcomePanel.BackColor

        StatusTSI.Text = "No installers are being created at this time"
        Dim rkWallPaper As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", False)
        Dim WallpaperPath As String = rkWallPaper.GetValue("WallPaper").ToString()
        rkWallPaper.Close()
        If File.Exists(WallpaperPath) Then
            backgroundPic.Image = New Bitmap(WallpaperPath)
        Else
            backgroundPic.Image = Nothing
            backgroundPic.BackColor = BackColor
        End If
        wmiget = CrLf & ":: Modern, fresh, clean, beautiful" & CrLf & "@echo off" & CrLf & "wmic computersystem get model > .\model.txt"
        File.WriteAllText(".\wmi.bat", wmiget.Trim(), ASCII)
        'LaunchPSI.WorkingDirectory = Application.StartupPath
        'LaunchPSI.FileName = ".\wmi.bat"
        'LaunchPSI.CreateNoWindow = True
        'Dim wmigetproc As Process = Process.Start(LaunchPSI)
        Process.Start(".\wmi.bat").WaitForExit()
        wmiform.TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\model.txt")
        If wmiform.TextBox1.Text = "" Then
            PictureBox6.Visible = True
            LinkLabel6.Visible = True
            modelLabel.Visible = False
        Else
            wmiform.TextBox1.Text = wmiform.TextBox1.Text.Replace("Model", "").Trim()
        End If
        Button6.Enabled = False

        modelLabel.Text = wmiform.TextBox1.Text
        MaximumSize = Screen.FromControl(Me).WorkingArea.Size
        File.Delete(".\wmi.bat")
        'File.Delete(".\uefi.bat")
        'File.Delete(".\uefi.txt")
        File.Delete(".\model.txt")
        computerLabel.Text = My.Computer.Name
        'Round(Me)
        ' Top bar PictureBoxes (these lines of code set the initial Visible property)
        WelcomeTopBarPic.Visible = True
        InstCreateTopBarPic.Visible = False
        InstructionsTopBarPic.Visible = False
        HelpTopBarPic.Visible = False
        AboutTopBarPic.Visible = False
        SettingsTopBarPic.Visible = False
        If My.Computer.Info.OSFullName.Contains("Windows 7") Or My.Computer.Info.OSFullName.Contains("Windows 8") Or My.Computer.Info.OSFullName.Contains("Windows Server 2008") Or My.Computer.Info.OSFullName.Contains("Windows Server 2012") Then
            Label1.Font = New Font("Segoe UI", 18)
            computerLabel.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            modelLabel.Font = New Font("Segoe UI", 12)
            Label8.Font = New Font("Segoe UI", 18)
            Label53.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            Label54.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            Label57.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            Label61.Font = New Font("Segoe UI", 18)
            Label62.Font = New Font("Segoe UI", 18)
            Label63.Font = New Font("Segoe UI", 18)
            Label80.Font = New Font("Segoe UI", 18)
            Label81.Font = New Font("Segoe UI", 18)
            Label82.Font = New Font("Segoe UI", 18)
            Label9.Font = New Font("Segoe UI", 18)
            Label29.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            Label32.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            Label10.Font = New Font("Segoe UI", 18)
            Label11.Font = New Font("Segoe UI", 18)
            Label12.Font = New Font("Segoe UI", 18)
            Label16.Font = New Font("Segoe UI", 18)
            Label17.Font = New Font("Segoe UI", 18)
            Label18.Font = New Font("Segoe UI", 18)
            Label104.Font = New Font("Segoe UI", 18)
            Label52.Font = New Font("Segoe UI", 18)
            Label4.Font = New Font("Segoe UI", 18)
            ' Do additional labels
            AdvancedOptionsPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            DebugPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            DisclaimerPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            FileCopyPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            InstCreateAbortPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            InstHistPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            ISOFileDownloadPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            ISOFileScanPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            LogExistsPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            LogMigratePanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            PrefResetPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            UpdateChoicePanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            FileNotFoundPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            VolumeConnectPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
        ElseIf My.Computer.Info.OSFullName.Contains("Windows 10") Or My.Computer.Info.OSFullName.Contains("Windows Server 2016") Then
            Dim commandStr As String = "reg query " & Quote & "HKLM\Software\Microsoft\Windows NT\CurrentVersion" & Quote & " /v BuildLabEx > .\info.txt"
            File.WriteAllText(".\info.bat", OffEcho & CrLf & commandStr, ASCII)
            Process.Start(".\info.bat").WaitForExit()
            VersionDetector.TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\info.txt")
            Try
                File.Delete(".\info.bat")
                File.Delete(".\info.txt")
            Catch ex As Exception
                Do Until Not File.Exists(".\info.bat") And Not File.Exists(".\info.txt")
                    File.Delete(".\info.bat")
                    File.Delete(".\info.txt")
                Loop
            End Try
            If VersionDetector.TextBox1.Text.Contains("10240") Then
                Label1.Font = New Font("Segoe UI", 18)
                computerLabel.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                modelLabel.Font = New Font("Segoe UI", 12)
                Label8.Font = New Font("Segoe UI", 18)
                Label53.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                Label54.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                Label57.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                Label61.Font = New Font("Segoe UI", 18)
                Label62.Font = New Font("Segoe UI", 18)
                Label63.Font = New Font("Segoe UI", 18)
                Label80.Font = New Font("Segoe UI", 18)
                Label81.Font = New Font("Segoe UI", 18)
                Label82.Font = New Font("Segoe UI", 18)
                Label9.Font = New Font("Segoe UI", 18)
                Label29.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                Label32.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                Label10.Font = New Font("Segoe UI", 18)
                Label11.Font = New Font("Segoe UI", 18)
                Label12.Font = New Font("Segoe UI", 18)
                Label16.Font = New Font("Segoe UI", 18)
                Label17.Font = New Font("Segoe UI", 18)
                Label18.Font = New Font("Segoe UI", 18)
                Label104.Font = New Font("Segoe UI", 18)
                Label52.Font = New Font("Segoe UI", 18)
                Label4.Font = New Font("Segoe UI", 18)
                AdvancedOptionsPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                DebugPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                DisclaimerPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                FileCopyPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                InstCreateAbortPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                InstHistPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                ISOFileDownloadPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                ISOFileScanPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                LogExistsPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                LogMigratePanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                PrefResetPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                UpdateChoicePanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                FileNotFoundPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                VolumeConnectPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            ElseIf VersionDetector.TextBox1.Text.Contains("10586") Then
                Label1.Font = New Font("Segoe UI", 18)
                computerLabel.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                modelLabel.Font = New Font("Segoe UI", 12)
                Label8.Font = New Font("Segoe UI", 18)
                Label53.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                Label54.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                Label57.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                Label61.Font = New Font("Segoe UI", 18)
                Label62.Font = New Font("Segoe UI", 18)
                Label63.Font = New Font("Segoe UI", 18)
                Label80.Font = New Font("Segoe UI", 18)
                Label81.Font = New Font("Segoe UI", 18)
                Label82.Font = New Font("Segoe UI", 18)
                Label9.Font = New Font("Segoe UI", 18)
                Label29.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                Label32.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                Label10.Font = New Font("Segoe UI", 18)
                Label11.Font = New Font("Segoe UI", 18)
                Label12.Font = New Font("Segoe UI", 18)
                Label16.Font = New Font("Segoe UI", 18)
                Label17.Font = New Font("Segoe UI", 18)
                Label18.Font = New Font("Segoe UI", 18)
                Label104.Font = New Font("Segoe UI", 18)
                Label52.Font = New Font("Segoe UI", 18)
                Label4.Font = New Font("Segoe UI", 18)
                AdvancedOptionsPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                DebugPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                DisclaimerPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                FileCopyPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                InstCreateAbortPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                InstHistPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                ISOFileDownloadPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                ISOFileScanPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                LogExistsPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                LogMigratePanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                PrefResetPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                UpdateChoicePanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                FileNotFoundPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
                VolumeConnectPanel.Label1.Font = New Font("Segoe UI", 14.25, FontStyle.Bold)
            End If
        End If
        If File.Exists(".\kill.bat") Then
            File.Delete(".\kill.bat")
        End If
        CenterToScreen()        ' This is done to prevent a bug where it would not center to the screen!
        If Environment.Is64BitOperatingSystem = False Then
            x86_Pic.Visible = True
            AdminLabel.Left = 832
        End If
        Visible = True
        BringToFront()
        BackSubPanel.Show()
        DisclaimerPanel.ShowDialog()
        DisclaimerPanel.Visible = True
        DisclaimerPanel.Visible = False
        If My.Computer.Info.OSFullName.Contains("Windows 11") Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                MsgBox("This computer (or device) is already running Windows 11. You will not be able to use this tool to upgrade to/install Windows 11 on your system, but you can still use it to upgrade to/install Windows 11 on other systems.", vbOKOnly + vbInformation, "Already running Windows 11")
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                MsgBox("Este ordenador (o dispositivo) ya está ejecutando Windows 11. No será posible usar la herramienta para actualizar a/instalar Windows 11 en su sistema, pero todavía puede usarlo para actualizar a/instalar Windows 11 en otros sistemas.", vbOKOnly + vbInformation, "Ya se está ejecutando Windows 11")
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                MsgBox("Cet ordinateur (ou appareil) exécute déjà Windows 11. Vous ne pourrez pas utiliser cet outil pour mettre à niveau vers/installer Windows 11 sur votre système, mais vous pouvez toujours l'utiliser pour mettre à niveau vers/installer Windows 11 sur d'autres systèmes.", vbOKOnly + vbInformation, "Vous utilisez déjà Windows 11")
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    MsgBox("This computer (or device) is already running Windows 11. You will not be able to use this tool to upgrade to/install Windows 11 on your system, but you can still use it to upgrade to/install Windows 11 on other systems.", vbOKOnly + vbInformation, "Already running Windows 11")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    MsgBox("Este ordenador (o dispositivo) ya está ejecutando Windows 11. No será posible usar la herramienta para actualizar a/instalar Windows 11 en su sistema, pero todavía puede usarlo para actualizar a/instalar Windows 11 en otros sistemas.", vbOKOnly + vbInformation, "Ya se está ejecutando Windows 11")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    MsgBox("Cet ordinateur (ou appareil) exécute déjà Windows 11. Vous ne pourrez pas utiliser cet outil pour mettre à niveau vers/installer Windows 11 sur votre système, mais vous pouvez toujours l'utiliser pour mettre à niveau vers/installer Windows 11 sur d'autres systèmes.", vbOKOnly + vbInformation, "Vous utilisez déjà Windows 11")
                End If
            End If
        End If

        ' Windows Server (Nickel) and Server (Copper) builds still don't check servers to meet system reqs (tested build 25083).
        ' Hold this for a bit (do not remove this, use this in the future if M$ wants to block Windows Server Nickel or Copper
        ' on unsupported servers, also, remove this comment if that happens)
        'If My.Computer.Info.OSFullName.Contains("Windows Server") Then
        '    If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
        '        MsgBox("This computer is running a Windows Server operating system. To achieve optimal results on incompatible servers, please create Copper or Nickel build installers of Windows Server.", vbOKOnly + vbInformation, "Windows Server detected")
        '    ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
        '        MsgBox("Este ordenador está ejecutando un sistema operativo Windows Server. Para lograr resultados óptimos en servidores incompatibles, por favor, cree instaladores de compilaciones Nickel y Copper de Windows Server.", vbOKOnly + vbInformation, "Se ha detectado Windows Server")
        '    ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
        '        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
        '            MsgBox("This computer is running a Windows Server operating system. To achieve optimal results on incompatible servers, please create Copper or Nickel build installers of Windows Server.", vbOKOnly + vbInformation, "Windows Server detected")
        '        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
        '            MsgBox("Este ordenador está ejecutando un sistema operativo Windows Server. Para lograr resultados óptimos en servidores incompatibles, por favor, cree instaladores de compilaciones Nickel y Copper de Windows Server.", vbOKOnly + vbInformation, "Se ha detectado Windows Server")
        '        End If
        '    End If
        'End If

        ' It ALSO looks like, at the time of writing this comment (thx for reading), MS released build 25115 of WINDOWS 11 (Copper). Right now,
        ' it does not add more system requirement blockades (but that does not mean they won't add them later)
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        BringToFront()
        BackSubPanel.Show()
        InstHistPanel.ShowDialog()
        InstHistPanel.Visible = True
        InstHistPanel.Visible = False
        BringToFront()
    End Sub

    Private Sub minBox_MouseDown(sender As Object, e As MouseEventArgs) Handles minBox.MouseDown
        If BackColor = Color.FromArgb(243, 243, 243) Then
            minBox.Image = New Bitmap(My.Resources.minBox_down)
        Else
            minBox.Image = New Bitmap(My.Resources.minBox_dark_down)
        End If
    End Sub

    Private Sub minBox_MouseUp(sender As Object, e As MouseEventArgs) Handles minBox.MouseUp
        If BackColor = Color.FromArgb(243, 243, 243) Then
            minBox.Image = New Bitmap(My.Resources.minBox_focus)
        Else
            minBox.Image = New Bitmap(My.Resources.minBox_dark_focus)
        End If
    End Sub

    Private Sub maxBox_MouseDown(sender As Object, e As MouseEventArgs) Handles maxBox.MouseDown
        If WindowState = FormWindowState.Normal Then
            If BackColor = Color.FromArgb(243, 243, 243) Then
                maxBox.Image = New Bitmap(My.Resources.maxbox_down)
            Else
                maxBox.Image = New Bitmap(My.Resources.maxbox_dark_down)
            End If
        ElseIf WindowState = FormWindowState.Maximized Then
            If BackColor = Color.FromArgb(243, 243, 243) Then
                maxBox.Image = New Bitmap(My.Resources.restdownbox_down)
            Else
                maxBox.Image = New Bitmap(My.Resources.restdownbox_dark_down)
            End If
        End If
    End Sub

    Private Sub maxBox_MouseUp(sender As Object, e As MouseEventArgs) Handles maxBox.MouseUp
        If WindowState = FormWindowState.Normal Then
            If BackColor = Color.FromArgb(243, 243, 243) Then
                maxBox.Image = New Bitmap(My.Resources.maxbox_focus)
            Else
                maxBox.Image = New Bitmap(My.Resources.maxbox_dark_focus)
            End If
        ElseIf WindowState = FormWindowState.Maximized Then
            maxBox.Image = New Bitmap(My.Resources.restdownbox_focus)
            If BackColor = Color.FromArgb(243, 243, 243) Then
                maxBox.Image = New Bitmap(My.Resources.restdownbox_focus)
            Else
                maxBox.Image = New Bitmap(My.Resources.restdownbox_dark_focus)
            End If
        End If
    End Sub

    Private Sub closeBox_MouseDown(sender As Object, e As MouseEventArgs) Handles closeBox.MouseDown
        If BackColor = Color.FromArgb(243, 243, 243) Then
            closeBox.Image = New Bitmap(My.Resources.closebox_down)
        Else
            closeBox.Image = New Bitmap(My.Resources.closebox_dark_down)
        End If
        TopRightResizePanel.BackColor = Color.FromArgb(200, 60, 49)
    End Sub

    Private Sub closeBox_MouseUp(sender As Object, e As MouseEventArgs) Handles closeBox.MouseUp
        closeBox.Image = New Bitmap(My.Resources.closebox_focus)
        TopRightResizePanel.BackColor = Color.FromArgb(196, 43, 28)
    End Sub

    Private Sub InfoPic_Click(sender As Object, e As EventArgs) Handles InfoPic.Click, PictureBox34.Click, Label102.Click
        DisableBackPic()
        WelcomePanel.Visible = False
        InstCreatePanel.Visible = False
        SettingReviewPanel.Visible = False
        ProgressPanel.Visible = False
        SettingPanel.Visible = False
        Settings_PersonalizationPanel.Visible = False
        Settings_FunctionalityPanel.Visible = False
        InstrPanel.Visible = False
        HelpPanel.Visible = False
        InfoPanel.Visible = True
        If BackColor = Color.FromArgb(243, 243, 243) Then
            WelcomePic.Image = New Bitmap(My.Resources.home)
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create)
            InstructionPic.Image = New Bitmap(My.Resources.instructions)
            SettingsPic.Image = New Bitmap(My.Resources.settings)
            HelpPic.Image = New Bitmap(My.Resources.help)
            InfoPic.Image = New Bitmap(My.Resources.info_filled)
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            WelcomePic.Image = New Bitmap(My.Resources.home_dark)
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark)
            InstructionPic.Image = New Bitmap(My.Resources.instructions_dark)
            SettingsPic.Image = New Bitmap(My.Resources.settings_dark)
            HelpPic.Image = New Bitmap(My.Resources.help_dark)
            InfoPic.Image = New Bitmap(My.Resources.info_dark_filled)
        End If
        PanelIndicatorPic.Top = InfoPic.Top + 2
        PanelIndicatorPic.Anchor = CType((AnchorStyles.Bottom Or AnchorStyles.Left), AnchorStyles)
        WelcomeTopBarPic.Visible = False
        InstCreateTopBarPic.Visible = False
        InstructionsTopBarPic.Visible = False
        HelpTopBarPic.Visible = False
        AboutTopBarPic.Visible = True
        SettingsTopBarPic.Visible = False
        ApplyNavBarImages()
    End Sub

    Private Sub WelcomePic_Click(sender As Object, e As EventArgs) Handles WelcomePic.Click, PictureBox8.Click, Label15.Click
        DisableBackPic()
        PanelIndicatorPic.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Left), AnchorStyles)
        PanelIndicatorPic.Top = WelcomePic.Top + 2
        If InfoPanel.Visible = True Then
            WelcomePanel.Visible = True
            InfoPanel.Visible = False
            If BackColor = Color.FromArgb(243, 243, 243) Then
                WelcomePic.Image = New Bitmap(My.Resources.home_filled)
                InfoPic.Image = New Bitmap(My.Resources.info)
            ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
                WelcomePic.Image = New Bitmap(My.Resources.home_dark_filled)
                InfoPic.Image = New Bitmap(My.Resources.info_dark)
            End If
        ElseIf SettingPanel.Visible = True Or Settings_PersonalizationPanel.Visible = True Or Settings_FunctionalityPanel.Visible = True Then
            WelcomePanel.Visible = True
            SettingPanel.Visible = False
            Settings_PersonalizationPanel.Visible = False
            Settings_FunctionalityPanel.Visible = False
            If BackColor = Color.FromArgb(243, 243, 243) Then
                WelcomePic.Image = New Bitmap(My.Resources.home_filled)
                SettingsPic.Image = New Bitmap(My.Resources.settings)
            ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
                WelcomePic.Image = New Bitmap(My.Resources.home_dark_filled)
                SettingsPic.Image = New Bitmap(My.Resources.settings_dark)
            End If
        ElseIf InstCreatePanel.Visible = True Then
            WelcomePanel.Visible = True
            InstCreatePanel.Visible = False
            If BackColor = Color.FromArgb(243, 243, 243) Then
                WelcomePic.Image = New Bitmap(My.Resources.home_filled)
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create)
            ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
                WelcomePic.Image = New Bitmap(My.Resources.home_dark_filled)
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark)
            End If
        ElseIf SettingReviewPanel.Visible = True Then
            WelcomePanel.Visible = True
            SettingReviewPanel.Visible = False
            If BackColor = Color.FromArgb(243, 243, 243) Then
                WelcomePic.Image = New Bitmap(My.Resources.home_filled)
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create)
            ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
                WelcomePic.Image = New Bitmap(My.Resources.home_dark_filled)
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark)
            End If
        ElseIf ProgressPanel.Visible = True Then
            WelcomePanel.Visible = True
            ProgressPanel.Visible = False
            If BackColor = Color.FromArgb(243, 243, 243) Then
                WelcomePic.Image = New Bitmap(My.Resources.home_filled)
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create)
            ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
                WelcomePic.Image = New Bitmap(My.Resources.home_dark_filled)
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark)
            End If
        End If
        WelcomeTopBarPic.Visible = True
        InstCreateTopBarPic.Visible = False
        InstructionsTopBarPic.Visible = False
        HelpTopBarPic.Visible = False
        AboutTopBarPic.Visible = False
        SettingsTopBarPic.Visible = False
        ApplyNavBarImages()
    End Sub

    Sub ApplyNavBarImages()
        If BackColor = Color.FromArgb(243, 243, 243) Then
            If WelcomePanel.Visible = True Then
                WelcomePic.Image = New Bitmap(My.Resources.home_filled)
            End If
            If InstCreatePanel.Visible = True Or SettingReviewPanel.Visible = True Or ProgressPanel.Visible = True Then
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_filled)
            End If
            If SettingPanel.Visible = True Or Settings_PersonalizationPanel.Visible = True Or Settings_FunctionalityPanel.Visible = True Then
                SettingsPic.Image = New Bitmap(My.Resources.settings_filled)
            End If
            If InstrPanel.Visible = True Then
                InstructionPic.Image = New Bitmap(My.Resources.instructions_filled)
            End If
            If HelpPanel.Visible = True Then
                HelpPic.Image = New Bitmap(My.Resources.help_filled)
            End If
            If InfoPanel.Visible = True Then
                InfoPic.Image = New Bitmap(My.Resources.info_filled)
            End If
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            If WelcomePanel.Visible = True Then
                WelcomePic.Image = New Bitmap(My.Resources.home_dark_filled)
            End If
            If InstCreatePanel.Visible = True Or SettingReviewPanel.Visible = True Or ProgressPanel.Visible = True Then
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark_filled)
            End If
            If SettingPanel.Visible = True Or Settings_PersonalizationPanel.Visible = True Or Settings_FunctionalityPanel.Visible = True Then
                SettingsPic.Image = New Bitmap(My.Resources.settings_dark_filled)
            End If
            If InstrPanel.Visible = True Then
                InstructionPic.Image = New Bitmap(My.Resources.instructions_dark_filled)
            End If
            If HelpPanel.Visible = True Then
                HelpPic.Image = New Bitmap(My.Resources.help_dark_filled)
            End If
            If InfoPanel.Visible = True Then
                InfoPic.Image = New Bitmap(My.Resources.info_dark_filled)
            End If
        End If
        PictureBox7.Image = SettingsPic.Image
        PictureBox8.Image = WelcomePic.Image
        PictureBox9.Image = InstCreatePic.Image
        PictureBox10.Image = InstructionPic.Image
        PictureBox33.Image = HelpPic.Image
        PictureBox34.Image = InfoPic.Image
    End Sub

    Private Sub LogoPic_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles LogoPic.MouseDoubleClick
        If CheckBox3.Checked = True Then
            Notify.ShowBalloonTip(5)
            WindowState = FormWindowState.Minimized
            ShowInTaskbar = False
        Else
            Notify.Visible = False
            End
        End If
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Process.Start("https://www.google.com/search?q=enable+csm+uefi&pccc=1")
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        Process.Start("https://duckduckgo.com/?q=enable+csm+uefi&atb=v297-6&ia=web")
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                PC_DETAIL_Label.Text = "Select this option for broader hardware compatibility"
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                PC_DETAIL_Label.Text = "Seleccione esta opción para una compatibilidad de hardware más amplia"
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                PC_DETAIL_Label.Text = "Sélectionnez cette option pour une compatibilité matérielle plus large"
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    PC_DETAIL_Label.Text = "Select this option for broader hardware compatibility"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    PC_DETAIL_Label.Text = "Seleccione esta opción para una compatibilidad de hardware más amplia"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    PC_DETAIL_Label.Text = "Sélectionnez cette option pour une compatibilité matérielle plus large"
                End If
            End If
            LinkLabel3.Visible = False
            LinkLabel4.Visible = False
            Label76.Text = "BIOS/UEFI-CSM (ID: 0x00)"
        Else
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                PC_DETAIL_Label.Text = "Select this option for systems that only support UEFI" & CrLf & "NOTE: you can enable UEFI-CSM on your system. If you don't know how, check one of the links below."
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                PC_DETAIL_Label.Text = "Seleccione esta opción para sistemas que solo soporten UEFI" & CrLf & "NOTA: usted puede habilitar UEFI-CSM en su sistema. Si no sabe cómo, compruebe uno de los enlaces de abajo."
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                PC_DETAIL_Label.Text = "Sélectionnez cette option pour les systèmes qui ne prennent en charge que l'UEFI." & CrLf & "NOTE : vous pouvez activer UEFI-CSM sur votre système. Si vous ne savez pas comment faire, consultez l'un des liens ci-dessous."
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    PC_DETAIL_Label.Text = "Select this option for systems that only support UEFI" & CrLf & "NOTE: you can enable UEFI-CSM on your system. If you don't know how, check one of the links below."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    PC_DETAIL_Label.Text = "Seleccione esta opción para sistemas que solo soporten UEFI" & CrLf & "NOTA: usted puede habilitar UEFI-CSM en su sistema. Si no sabe cómo, compruebe uno de los enlaces de abajo."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    PC_DETAIL_Label.Text = "Sélectionnez cette option pour les systèmes qui ne prennent en charge que l'UEFI." & CrLf & "NOTE : vous pouvez activer UEFI-CSM sur votre système. Si vous ne savez pas comment faire, consultez l'un des liens ci-dessous."
                End If
            End If
            LinkLabel3.Visible = True
            LinkLabel4.Visible = True
            Label76.Text = "UEFI (ID: 0xEF)"
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            Label31.Text = "Color mode: " & ComboBox1.Text.ToLower & " mode"
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            Label31.Text = "Modo de color: modo " & ComboBox1.Text.ToLower
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            Label31.Text = "Mode couleur: mode " & ComboBox1.Text.ToLower
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label31.Text = "Color mode: " & ComboBox1.Text.ToLower & " mode"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label31.Text = "Modo de color: modo " & ComboBox1.Text.ToLower
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label31.Text = "Mode couleur: mode " & ComboBox1.Text.ToLower
            End If
        End If
        If ComboBox1.SelectedItem = "Dark" Or ComboBox1.SelectedItem = "Oscuro" Or ComboBox1.SelectedItem = "Sombre" Then
            ColorInt = 1
            If RadioButton3.Checked = True Then
                PictureBox2.Image = New Bitmap(My.Resources.NavBar_LeftPos_Dark)
            Else
                PictureBox2.Image = New Bitmap(My.Resources.NavBar_TopPos_Dark)
            End If
            PictureBox11.Image = New Bitmap(My.Resources.logo_dark)
            BackColor = Color.FromArgb(32, 32, 32)
            NavBar.ForeColor = Color.White
            Settings_PersonalizationPanel.BackColor = Color.FromArgb(39, 39, 39)
            Settings_FunctionalityPanel.BackColor = Color.FromArgb(39, 39, 39)
            HelpPanel.BackColor = Color.FromArgb(39, 39, 39)
            InfoPanel.BackColor = Color.FromArgb(39, 39, 39)
            InstCreatePanel.BackColor = Color.FromArgb(39, 39, 39)
            SettingReviewPanel.BackColor = Color.FromArgb(39, 39, 39)
            ProgressPanel.BackColor = Color.FromArgb(39, 39, 39)
            SettingPanel.BackColor = Color.FromArgb(39, 39, 39)
            WelcomePanel.BackColor = Color.FromArgb(39, 39, 39)
            ForeColor = Color.White
            Panel_Border_Pic.Image = New Bitmap(My.Resources.panel_corner_black)
            x86_Pic.Image = New Bitmap(My.Resources.x86_dark)
            minBox.Image = New Bitmap(My.Resources.minBox_dark)
            ' This has changed to determine window state
            If WindowState = FormWindowState.Maximized Then
                maxBox.Image = New Bitmap(My.Resources.restdownbox_dark)
            Else
                maxBox.Image = New Bitmap(My.Resources.maxbox_dark)
            End If
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
            back_Pic.Image = New Bitmap(My.Resources.back_arrow_dark)
            PictureBox11.Image = New Bitmap(My.Resources.logo_dark)
            PictureBox13.Image = New Bitmap(My.Resources.blackPanelBack)
            PictureBox14.Image = New Bitmap(My.Resources.blackPanelBack)
            PictureBox17.Image = New Bitmap(My.Resources.blackPanelBack)
            PictureBox19.Image = New Bitmap(My.Resources.blackPanelBack)
            PictureBox21.Image = New Bitmap(My.Resources.blackPanelBack)
            PictureBox23.Image = New Bitmap(My.Resources.blackPanelBack)
            PictureBox25.Image = New Bitmap(My.Resources.blackPanelBack)
            PictureBox27.Image = New Bitmap(My.Resources.blackPanelBack)
            PictureBox29.Image = New Bitmap(My.Resources.blackPanelBack)
            PictureBox20.Image = New Bitmap(My.Resources.inst_create_dark)
            PictureBox22.Image = New Bitmap(My.Resources.settings_dark)
            PictureBox24.Image = New Bitmap(My.Resources.instructions_dark)
            PictureBox26.Image = New Bitmap(My.Resources.help_dark)
            PictureBox28.Image = New Bitmap(My.Resources.info_dark)
            PictureBox15.Image = New Bitmap(My.Resources.personalization_icon_dark)
            PictureBox16.Image = New Bitmap(My.Resources.functionality_icon_dark)
            PictureBox4.Image = New Bitmap(My.Resources.log_del_dark)
            PictureBox18.Image = New Bitmap(My.Resources.pref_reset_dark)
            BranchPic.Image = New Bitmap(My.Resources.hummingbird_dark)
            PictureBox35.Image = New Bitmap(My.Resources.file_download_dark)


            ' This is for the Group Box
            GroupBox1.ForeColor = Color.White
            GroupBox12.ForeColor = Color.White
            GroupBox3.ForeColor = Color.White
            GroupBox4.ForeColor = Color.White
            GroupBox5.ForeColor = Color.White
            GroupBox6.ForeColor = Color.White
            GroupBox7.ForeColor = Color.White
            GroupBox8.ForeColor = Color.White
            GroupBox9.ForeColor = Color.White
            GroupBox10.ForeColor = Color.White
            GroupBox11.ForeColor = Color.White


            ' Side panel PictureBoxes
            WelcomePic.Image = New Bitmap(My.Resources.home_dark)
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark)
            If Settings_PersonalizationPanel.Visible = True Or SettingPanel.Visible = True Then
                SettingsPic.Image = New Bitmap(My.Resources.settings_dark_filled)
            Else
                SettingsPic.Image = New Bitmap(My.Resources.settings_dark)
            End If
            InstructionPic.Image = New Bitmap(My.Resources.instructions_dark)
            HelpPic.Image = New Bitmap(My.Resources.help_dark)
            InfoPic.Image = New Bitmap(My.Resources.info_dark)
            DebugPic.Image = New Bitmap(My.Resources.debug_dark)

            ' Nav Bar PictureBoxes
            NavBarBackPic.Image = back_Pic.Image
            PanelIndicatorPic.Image = New Bitmap(My.Resources.panel_indicator_dark)
            WelcomeTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_dark_top_navbar)
            InstructionsTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_dark_top_navbar)
            HelpTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_dark_top_navbar)
            AboutTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_dark_top_navbar)
            SettingsTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_dark_top_navbar)

            PictureBox8.Image = WelcomePic.Image
            PictureBox9.Image = InstCreatePic.Image
            PictureBox10.Image = InstructionPic.Image
            PictureBox33.Image = HelpPic.Image
            PictureBox34.Image = InfoPic.Image
            PictureBox7.Image = SettingsPic.Image

            ' TextBoxes
            TextBox1.BackColor = Color.FromArgb(43, 43, 43)
            TextBox1.ForeColor = Color.White
            TextBox2.BackColor = Color.FromArgb(43, 43, 43)
            TextBox2.ForeColor = Color.White
            TextBox3.BackColor = Color.FromArgb(43, 43, 43)
            TextBox3.ForeColor = Color.White
            TextBox4.BackColor = Color.FromArgb(43, 43, 43)
            TextBox4.ForeColor = Color.White
            WarningText.BackColor = Color.FromArgb(43, 43, 43)
            WarningText.ForeColor = Color.White
            ErrorText.BackColor = Color.FromArgb(43, 43, 43)
            ErrorText.ForeColor = Color.White
            LogBox.BackColor = Color.FromArgb(43, 43, 43)
            LogBox.ForeColor = Color.White
            LabelText.BackColor = Color.FromArgb(43, 43, 43)
            LabelText.ForeColor = Color.White
            scText.BackColor = Color.FromArgb(43, 43, 43)
            scText.ForeColor = Color.White

            ' Check box PictureBoxes for the progress panel
            CheckPic1.Image = New Bitmap(My.Resources.check_dark)
            CheckPic2.Image = New Bitmap(My.Resources.check_dark)
            CheckPic3.Image = New Bitmap(My.Resources.check_dark)
            CheckPic4.Image = New Bitmap(My.Resources.check_dark)
            CheckPic5.Image = New Bitmap(My.Resources.check_dark)
            ' Computer PictureBox
            CompPic.Image = New Bitmap(My.Resources.comp_dark)

            Notify.Icon = My.Resources.NotifyIconRes_Dark

            ' ComboBoxes
            ComboBox1.BackColor = Color.FromArgb(39, 39, 39)
            ComboBox4.BackColor = Color.FromArgb(39, 39, 39)
            ComboBox5.BackColor = Color.FromArgb(39, 39, 39)
            ComboBox1.ForeColor = Color.White
            ComboBox4.ForeColor = Color.White
            ComboBox5.ForeColor = Color.White

            TabPage1.BackColor = Color.FromArgb(39, 39, 39)
            TabPage2.BackColor = Color.FromArgb(39, 39, 39)
            TabPage3.BackColor = Color.FromArgb(39, 39, 39)

            NavBar.ForeColor = Color.White

            ' LinkLabels
            LinkLabel1.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel2.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel3.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel4.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel5.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel6.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel7.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel8.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel9.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel10.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel11.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel12.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel13.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel14.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel16.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel17.LinkColor = Color.FromArgb(76, 194, 255)
            LinkLabel18.LinkColor = Color.FromArgb(76, 194, 255)
            TargetInstallerLinkLabel.LinkColor = Color.FromArgb(76, 194, 255)
            LogViewLink.LinkColor = Color.FromArgb(76, 194, 255)
        ElseIf ComboBox1.SelectedItem = "Light" Or ComboBox1.SelectedItem = "Claro" Or ComboBox1.SelectedItem = "Lumière" Then
            ColorInt = 2
            If RadioButton3.Checked = True Then
                PictureBox2.Image = New Bitmap(My.Resources.NavBar_LeftPos_Light)
            Else
                PictureBox2.Image = New Bitmap(My.Resources.NavBar_TopPos_Light)
            End If
            PictureBox11.Image = New Bitmap(My.Resources.logo_light)
            NavBar.ForeColor = Color.Black
            BackColor = Color.FromArgb(243, 243, 243)
            Settings_PersonalizationPanel.BackColor = Color.FromArgb(249, 249, 249)
            Settings_FunctionalityPanel.BackColor = Color.FromArgb(249, 249, 249)
            HelpPanel.BackColor = Color.FromArgb(249, 249, 249)
            InfoPanel.BackColor = Color.FromArgb(249, 249, 249)
            InstCreatePanel.BackColor = Color.FromArgb(249, 249, 249)
            SettingReviewPanel.BackColor = Color.FromArgb(249, 249, 249)
            ProgressPanel.BackColor = Color.FromArgb(249, 249, 249)
            SettingPanel.BackColor = Color.FromArgb(249, 249, 249)
            WelcomePanel.BackColor = Color.FromArgb(249, 249, 249)
            ForeColor = Color.Black
            Panel_Border_Pic.Image = New Bitmap(My.Resources.panel_corner_white)
            x86_Pic.Image = New Bitmap(My.Resources.x86_light)
            minBox.Image = New Bitmap(My.Resources.minBox)
            If WindowState = FormWindowState.Maximized Then
                maxBox.Image = New Bitmap(My.Resources.restdownbox)
            Else
                maxBox.Image = New Bitmap(My.Resources.maxbox)
            End If
            closeBox.Image = New Bitmap(My.Resources.closebox)
            back_Pic.Image = New Bitmap(My.Resources.back_arrow)
            PictureBox11.Image = New Bitmap(My.Resources.logo_light)
            PictureBox13.Image = New Bitmap(My.Resources.whitePanelBack)
            PictureBox14.Image = New Bitmap(My.Resources.whitePanelBack)
            PictureBox17.Image = New Bitmap(My.Resources.whitePanelBack)
            PictureBox19.Image = New Bitmap(My.Resources.whitePanelBack)
            PictureBox21.Image = New Bitmap(My.Resources.whitePanelBack)
            PictureBox23.Image = New Bitmap(My.Resources.whitePanelBack)
            PictureBox25.Image = New Bitmap(My.Resources.whitePanelBack)
            PictureBox27.Image = New Bitmap(My.Resources.whitePanelBack)
            PictureBox29.Image = New Bitmap(My.Resources.whitePanelBack)
            PictureBox20.Image = New Bitmap(My.Resources.inst_create)
            PictureBox22.Image = New Bitmap(My.Resources.settings)
            PictureBox24.Image = New Bitmap(My.Resources.instructions)
            PictureBox26.Image = New Bitmap(My.Resources.help)
            PictureBox28.Image = New Bitmap(My.Resources.info)
            PictureBox15.Image = New Bitmap(My.Resources.personalization_icon)
            PictureBox16.Image = New Bitmap(My.Resources.functionality_icon)
            PictureBox4.Image = New Bitmap(My.Resources.log_del)
            PictureBox18.Image = New Bitmap(My.Resources.pref_reset)
            BranchPic.Image = New Bitmap(My.Resources.hummingbird)
            PanelIndicatorPic.Image = New Bitmap(My.Resources.panel_indicator_light)
            CompPic.Image = New Bitmap(My.Resources.comp_light)
            PictureBox35.Image = New Bitmap(My.Resources.file_download_light)
            GroupBox1.ForeColor = Color.Black
            GroupBox12.ForeColor = Color.Black
            GroupBox3.ForeColor = Color.Black
            GroupBox4.ForeColor = Color.Black
            GroupBox5.ForeColor = Color.Black
            GroupBox6.ForeColor = Color.Black
            GroupBox7.ForeColor = Color.Black
            GroupBox8.ForeColor = Color.Black
            GroupBox9.ForeColor = Color.Black
            GroupBox10.ForeColor = Color.Black
            GroupBox11.ForeColor = Color.Black

            ' This condition is used because this is the default fallback color mode
            If WelcomePanel.Visible = True Then
                WelcomePic.Image = New Bitmap(My.Resources.home_filled)
                SettingsPic.Image = New Bitmap(My.Resources.settings)
            Else
                WelcomePic.Image = New Bitmap(My.Resources.home)
                SettingsPic.Image = New Bitmap(My.Resources.settings_filled)
            End If
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create)
            If Settings_PersonalizationPanel.Visible = True Or SettingPanel.Visible = True Then
                SettingsPic.Image = New Bitmap(My.Resources.settings_filled)
            Else
                SettingsPic.Image = New Bitmap(My.Resources.settings)
            End If
            InstructionPic.Image = New Bitmap(My.Resources.instructions)
            HelpPic.Image = New Bitmap(My.Resources.help)
            InfoPic.Image = New Bitmap(My.Resources.info)
            DebugPic.Image = New Bitmap(My.Resources.debug_light)
            CheckPic1.Image = New Bitmap(My.Resources.check)
            CheckPic2.Image = New Bitmap(My.Resources.check)
            CheckPic3.Image = New Bitmap(My.Resources.check)
            CheckPic4.Image = New Bitmap(My.Resources.check)
            CheckPic5.Image = New Bitmap(My.Resources.check)

            TextBox1.BackColor = Color.FromArgb(249, 249, 249)
            TextBox1.ForeColor = Color.Black
            TextBox2.BackColor = Color.FromArgb(249, 249, 249)
            TextBox2.ForeColor = Color.Black
            TextBox3.BackColor = Color.FromArgb(249, 249, 249)
            TextBox3.ForeColor = Color.Black
            TextBox4.BackColor = Color.FromArgb(249, 249, 249)
            TextBox4.ForeColor = Color.Black
            WarningText.BackColor = Color.FromArgb(249, 249, 249)
            WarningText.ForeColor = Color.Black
            ErrorText.BackColor = Color.FromArgb(249, 249, 249)
            ErrorText.ForeColor = Color.Black
            LogBox.BackColor = Color.FromArgb(249, 249, 249)
            LogBox.ForeColor = Color.Black
            LabelText.BackColor = Color.FromArgb(249, 249, 249)
            LabelText.ForeColor = Color.Black
            scText.BackColor = Color.FromArgb(249, 249, 249)
            scText.ForeColor = Color.Black

            ComboBox1.BackColor = Color.FromArgb(249, 249, 249)
            ComboBox4.BackColor = Color.FromArgb(249, 249, 249)
            ComboBox5.BackColor = Color.FromArgb(249, 249, 249)
            ComboBox1.ForeColor = Color.Black
            ComboBox4.ForeColor = Color.Black
            ComboBox5.ForeColor = Color.Black

            Notify.Icon = My.Resources.NotifyIconRes_Light
            TabPage1.BackColor = Color.FromArgb(249, 249, 249)
            TabPage2.BackColor = Color.FromArgb(249, 249, 249)
            TabPage3.BackColor = Color.FromArgb(249, 249, 249)
            NavBarBackPic.Image = back_Pic.Image
            WelcomeTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_light_top_navbar)
            InstructionsTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_light_top_navbar)
            HelpTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_light_top_navbar)
            AboutTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_light_top_navbar)
            SettingsTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_light_top_navbar)
            PictureBox8.Image = WelcomePic.Image
            PictureBox9.Image = InstCreatePic.Image
            PictureBox10.Image = InstructionPic.Image
            PictureBox33.Image = HelpPic.Image
            PictureBox34.Image = InfoPic.Image
            PictureBox7.Image = SettingsPic.Image

            ' LinkLabels
            LinkLabel1.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel2.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel3.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel4.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel5.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel6.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel7.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel8.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel9.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel10.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel11.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel12.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel13.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel14.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel16.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel17.LinkColor = Color.FromArgb(1, 92, 186)
            LinkLabel18.LinkColor = Color.FromArgb(1, 92, 186)
            TargetInstallerLinkLabel.LinkColor = Color.FromArgb(1, 92, 186)
            LogViewLink.LinkColor = Color.FromArgb(1, 92, 186)
        ElseIf ComboBox1.SelectedItem = "Automatic" Or ComboBox1.SelectedItem = "Automático" Or ComboBox1.SelectedItem = "Automatique" Then
            ColorInt = 0
            Try
                If My.Computer.Info.OSFullName.Contains("Windows 7") Or My.Computer.Info.OSFullName.Contains("Windows 8") Or My.Computer.Info.OSFullName.Contains("Windows Server 2008") Or My.Computer.Info.OSFullName.Contains("Windows Server 2012") Then
                    ' Don't do this again!!!
                Else
                    Dim SysColor As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", False)
                    Dim SysColorStr As String = SysColor.GetValue("SystemUsesLightTheme").ToString()
                    SysColor.Close()
                    If SysColorStr = "1" Then
                        Notify.Icon = My.Resources.NotifyIconRes_Light
                    ElseIf SysColorStr = "0" Then
                        Notify.Icon = My.Resources.NotifyIconRes_Dark
                    End If
                End If
            Catch ex As NullReferenceException
                ' Don't do this thing!!!
            End Try
            Try
                If My.Computer.Info.OSFullName.Contains("Windows 10") Or My.Computer.Info.OSFullName.Contains("Windows 11") Then
                    Dim rkColorMode As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", False)
                    Dim ColorStr As String = rkColorMode.GetValue("AppsUseLightTheme").ToString()
                    rkColorMode.Close()

                    If ColorStr = "0" Then
                        If RadioButton3.Checked = True Then
                            PictureBox2.Image = New Bitmap(My.Resources.NavBar_LeftPos_Dark)
                        Else
                            PictureBox2.Image = New Bitmap(My.Resources.NavBar_TopPos_Dark)
                        End If
                        BackColor = Color.FromArgb(32, 32, 32)
                        NavBar.ForeColor = Color.White
                        Settings_PersonalizationPanel.BackColor = Color.FromArgb(39, 39, 39)
                        Settings_FunctionalityPanel.BackColor = Color.FromArgb(39, 39, 39)
                        HelpPanel.BackColor = Color.FromArgb(39, 39, 39)
                        InfoPanel.BackColor = Color.FromArgb(39, 39, 39)
                        InstCreatePanel.BackColor = Color.FromArgb(39, 39, 39)
                        SettingReviewPanel.BackColor = Color.FromArgb(39, 39, 39)
                        ProgressPanel.BackColor = Color.FromArgb(39, 39, 39)
                        SettingPanel.BackColor = Color.FromArgb(39, 39, 39)
                        WelcomePanel.BackColor = Color.FromArgb(39, 39, 39)
                        ForeColor = Color.White
                        Panel_Border_Pic.Image = New Bitmap(My.Resources.panel_corner_black)
                        x86_Pic.Image = New Bitmap(My.Resources.x86_dark)
                        minBox.Image = New Bitmap(My.Resources.minBox_dark)
                        If WindowState = FormWindowState.Maximized Then
                            maxBox.Image = New Bitmap(My.Resources.restdownbox_dark)
                        Else
                            maxBox.Image = New Bitmap(My.Resources.maxbox_dark)
                        End If
                        closeBox.Image = New Bitmap(My.Resources.closebox_dark)
                        back_Pic.Image = New Bitmap(My.Resources.back_arrow_dark)
                        PictureBox11.Image = New Bitmap(My.Resources.logo_dark)
                        PictureBox13.Image = New Bitmap(My.Resources.blackPanelBack)
                        PictureBox14.Image = New Bitmap(My.Resources.blackPanelBack)
                        PictureBox17.Image = New Bitmap(My.Resources.blackPanelBack)
                        PictureBox19.Image = New Bitmap(My.Resources.blackPanelBack)
                        PictureBox21.Image = New Bitmap(My.Resources.blackPanelBack)
                        PictureBox23.Image = New Bitmap(My.Resources.blackPanelBack)
                        PictureBox25.Image = New Bitmap(My.Resources.blackPanelBack)
                        PictureBox27.Image = New Bitmap(My.Resources.blackPanelBack)
                        PictureBox29.Image = New Bitmap(My.Resources.blackPanelBack)
                        PictureBox20.Image = New Bitmap(My.Resources.inst_create_dark)
                        PictureBox22.Image = New Bitmap(My.Resources.settings_dark)
                        PictureBox24.Image = New Bitmap(My.Resources.instructions_dark)
                        PictureBox26.Image = New Bitmap(My.Resources.help_dark)
                        PictureBox28.Image = New Bitmap(My.Resources.info_dark)
                        PictureBox15.Image = New Bitmap(My.Resources.personalization_icon_dark)
                        PictureBox16.Image = New Bitmap(My.Resources.functionality_icon_dark)
                        PictureBox4.Image = New Bitmap(My.Resources.log_del_dark)
                        PictureBox18.Image = New Bitmap(My.Resources.pref_reset_dark)
                        PictureBox35.Image = New Bitmap(My.Resources.file_download_dark)
                        BranchPic.Image = New Bitmap(My.Resources.hummingbird_dark)
                        GroupBox1.ForeColor = Color.White
                        GroupBox12.ForeColor = Color.White
                        GroupBox3.ForeColor = Color.White
                        GroupBox4.ForeColor = Color.White
                        GroupBox5.ForeColor = Color.White
                        GroupBox6.ForeColor = Color.White
                        GroupBox7.ForeColor = Color.White
                        GroupBox8.ForeColor = Color.White
                        GroupBox9.ForeColor = Color.White
                        GroupBox10.ForeColor = Color.White
                        GroupBox11.ForeColor = Color.White
                        PanelIndicatorPic.Image = New Bitmap(My.Resources.panel_indicator_dark)
                        CompPic.Image = New Bitmap(My.Resources.comp_dark)
                        If WelcomePanel.Visible = True Then
                            WelcomePic.Image = New Bitmap(My.Resources.home_dark_filled)
                        Else
                            WelcomePic.Image = New Bitmap(My.Resources.home_dark)
                        End If
                        InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark)
                        If Settings_PersonalizationPanel.Visible = True Or SettingPanel.Visible = True Then
                            SettingsPic.Image = New Bitmap(My.Resources.settings_dark_filled)
                        Else
                            SettingsPic.Image = New Bitmap(My.Resources.settings_dark)
                        End If
                        CheckPic1.Image = New Bitmap(My.Resources.check_dark)
                        CheckPic2.Image = New Bitmap(My.Resources.check_dark)
                        CheckPic3.Image = New Bitmap(My.Resources.check_dark)
                        CheckPic4.Image = New Bitmap(My.Resources.check_dark)
                        CheckPic5.Image = New Bitmap(My.Resources.check_dark)
                        InstructionPic.Image = New Bitmap(My.Resources.instructions_dark)
                        HelpPic.Image = New Bitmap(My.Resources.help_dark)
                        InfoPic.Image = New Bitmap(My.Resources.info_dark)
                        DebugPic.Image = New Bitmap(My.Resources.debug_dark)
                        TextBox1.BackColor = Color.FromArgb(43, 43, 43)
                        TextBox1.ForeColor = Color.White
                        TextBox2.BackColor = Color.FromArgb(43, 43, 43)
                        TextBox2.ForeColor = Color.White
                        TextBox3.BackColor = Color.FromArgb(43, 43, 43)
                        TextBox3.ForeColor = Color.White
                        TextBox4.BackColor = Color.FromArgb(43, 43, 43)
                        TextBox4.ForeColor = Color.White
                        WarningText.BackColor = Color.FromArgb(43, 43, 43)
                        WarningText.ForeColor = Color.White
                        ErrorText.BackColor = Color.FromArgb(43, 43, 43)
                        ErrorText.ForeColor = Color.White
                        LogBox.BackColor = Color.FromArgb(43, 43, 43)
                        LogBox.ForeColor = Color.White
                        scText.BackColor = Color.FromArgb(43, 43, 43)
                        scText.ForeColor = Color.White
                        LabelText.BackColor = Color.FromArgb(43, 43, 43)
                        LabelText.ForeColor = Color.White
                        ComboBox1.BackColor = Color.FromArgb(39, 39, 39)
                        ComboBox4.BackColor = Color.FromArgb(39, 39, 39)
                        ComboBox5.BackColor = Color.FromArgb(39, 39, 39)
                        ComboBox1.ForeColor = Color.White
                        ComboBox4.ForeColor = Color.White
                        ComboBox5.ForeColor = Color.White
                        TabPage1.BackColor = Color.FromArgb(39, 39, 39)
                        TabPage2.BackColor = Color.FromArgb(39, 39, 39)
                        TabPage3.BackColor = Color.FromArgb(39, 39, 39)
                        NavBarBackPic.Image = back_Pic.Image
                        WelcomeTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_dark_top_navbar)
                        InstructionsTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_dark_top_navbar)
                        HelpTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_dark_top_navbar)
                        AboutTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_dark_top_navbar)
                        SettingsTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_dark_top_navbar)
                        PictureBox8.Image = WelcomePic.Image
                        PictureBox9.Image = InstCreatePic.Image
                        PictureBox10.Image = InstructionPic.Image
                        PictureBox33.Image = HelpPic.Image
                        PictureBox34.Image = InfoPic.Image
                        PictureBox7.Image = SettingsPic.Image
                        ' LinkLabels
                        LinkLabel1.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel2.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel3.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel4.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel5.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel6.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel7.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel8.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel9.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel10.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel11.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel12.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel13.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel14.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel16.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel17.LinkColor = Color.FromArgb(76, 194, 255)
                        LinkLabel18.LinkColor = Color.FromArgb(76, 194, 255)
                        TargetInstallerLinkLabel.LinkColor = Color.FromArgb(76, 194, 255)
                        LogViewLink.LinkColor = Color.FromArgb(76, 194, 255)
                    ElseIf ColorStr = "1" Then
                        If RadioButton3.Checked = True Then
                            PictureBox2.Image = New Bitmap(My.Resources.NavBar_LeftPos_Light)
                        Else
                            PictureBox2.Image = New Bitmap(My.Resources.NavBar_TopPos_Light)
                        End If
                        NavBar.ForeColor = Color.Black
                        BackColor = Color.FromArgb(243, 243, 243)
                        Settings_PersonalizationPanel.BackColor = Color.FromArgb(249, 249, 249)
                        Settings_FunctionalityPanel.BackColor = Color.FromArgb(249, 249, 249)
                        HelpPanel.BackColor = Color.FromArgb(249, 249, 249)
                        InfoPanel.BackColor = Color.FromArgb(249, 249, 249)
                        InstCreatePanel.BackColor = Color.FromArgb(249, 249, 249)
                        SettingReviewPanel.BackColor = Color.FromArgb(249, 249, 249)
                        ProgressPanel.BackColor = Color.FromArgb(249, 249, 249)
                        SettingPanel.BackColor = Color.FromArgb(249, 249, 249)
                        WelcomePanel.BackColor = Color.FromArgb(249, 249, 249)
                        ForeColor = Color.Black
                        Panel_Border_Pic.Image = New Bitmap(My.Resources.panel_corner_white)
                        x86_Pic.Image = New Bitmap(My.Resources.x86_light)
                        If WindowState = FormWindowState.Maximized Then
                            maxBox.Image = New Bitmap(My.Resources.restdownbox)
                        Else
                            maxBox.Image = New Bitmap(My.Resources.maxbox)
                        End If
                        minBox.Image = New Bitmap(My.Resources.minBox)
                        closeBox.Image = New Bitmap(My.Resources.closebox)
                        back_Pic.Image = New Bitmap(My.Resources.back_arrow)
                        PictureBox11.Image = New Bitmap(My.Resources.logo_light)
                        PictureBox13.Image = New Bitmap(My.Resources.whitePanelBack)
                        PictureBox14.Image = New Bitmap(My.Resources.whitePanelBack)
                        PictureBox17.Image = New Bitmap(My.Resources.whitePanelBack)
                        PictureBox19.Image = New Bitmap(My.Resources.whitePanelBack)
                        PictureBox21.Image = New Bitmap(My.Resources.whitePanelBack)
                        PictureBox23.Image = New Bitmap(My.Resources.whitePanelBack)
                        PictureBox25.Image = New Bitmap(My.Resources.whitePanelBack)
                        PictureBox27.Image = New Bitmap(My.Resources.whitePanelBack)
                        PictureBox29.Image = New Bitmap(My.Resources.whitePanelBack)
                        PictureBox20.Image = New Bitmap(My.Resources.inst_create)
                        PictureBox22.Image = New Bitmap(My.Resources.settings)
                        PictureBox24.Image = New Bitmap(My.Resources.instructions)
                        PictureBox26.Image = New Bitmap(My.Resources.help)
                        PictureBox28.Image = New Bitmap(My.Resources.info)
                        PictureBox15.Image = New Bitmap(My.Resources.personalization_icon)
                        PictureBox16.Image = New Bitmap(My.Resources.functionality_icon)
                        PictureBox4.Image = New Bitmap(My.Resources.log_del)
                        PictureBox18.Image = New Bitmap(My.Resources.pref_reset)
                        PictureBox35.Image = New Bitmap(My.Resources.file_download_light)
                        PanelIndicatorPic.Image = New Bitmap(My.Resources.panel_indicator_light)
                        CompPic.Image = New Bitmap(My.Resources.comp_light)
                        BranchPic.Image = New Bitmap(My.Resources.hummingbird)
                        If WelcomePanel.Visible = True Then
                            WelcomePic.Image = New Bitmap(My.Resources.home_filled)
                        Else
                            WelcomePic.Image = New Bitmap(My.Resources.home)
                        End If
                        InstCreatePic.Image = New Bitmap(My.Resources.inst_create)
                        If Settings_PersonalizationPanel.Visible = True Or SettingPanel.Visible = True Then
                            SettingsPic.Image = New Bitmap(My.Resources.settings_filled)
                        Else
                            SettingsPic.Image = New Bitmap(My.Resources.settings)
                        End If
                        InstructionPic.Image = New Bitmap(My.Resources.instructions)
                        HelpPic.Image = New Bitmap(My.Resources.help)
                        InfoPic.Image = New Bitmap(My.Resources.info)
                        DebugPic.Image = New Bitmap(My.Resources.debug_light)
                        CheckPic1.Image = New Bitmap(My.Resources.check)
                        CheckPic2.Image = New Bitmap(My.Resources.check)
                        CheckPic3.Image = New Bitmap(My.Resources.check)
                        CheckPic4.Image = New Bitmap(My.Resources.check)
                        CheckPic5.Image = New Bitmap(My.Resources.check)
                        GroupBox1.ForeColor = Color.Black
                        GroupBox12.ForeColor = Color.Black
                        GroupBox3.ForeColor = Color.Black
                        GroupBox4.ForeColor = Color.Black
                        GroupBox5.ForeColor = Color.Black
                        GroupBox6.ForeColor = Color.Black
                        GroupBox7.ForeColor = Color.Black
                        GroupBox8.ForeColor = Color.Black
                        GroupBox9.ForeColor = Color.Black
                        GroupBox10.ForeColor = Color.Black
                        GroupBox11.ForeColor = Color.Black
                        TextBox1.BackColor = Color.FromArgb(249, 249, 249)
                        TextBox1.ForeColor = Color.Black
                        TextBox2.BackColor = Color.FromArgb(249, 249, 249)
                        TextBox2.ForeColor = Color.Black
                        TextBox3.BackColor = Color.FromArgb(249, 249, 249)
                        TextBox3.ForeColor = Color.Black
                        TextBox4.BackColor = Color.FromArgb(249, 249, 249)
                        TextBox4.ForeColor = Color.Black
                        WarningText.BackColor = Color.FromArgb(249, 249, 249)
                        WarningText.ForeColor = Color.Black
                        ErrorText.BackColor = Color.FromArgb(249, 249, 249)
                        ErrorText.ForeColor = Color.Black
                        LogBox.BackColor = Color.FromArgb(249, 249, 249)
                        LogBox.ForeColor = Color.Black
                        LabelText.BackColor = Color.FromArgb(249, 249, 249)
                        LabelText.ForeColor = Color.Black
                        scText.BackColor = Color.FromArgb(249, 249, 249)
                        scText.ForeColor = Color.Black
                        ComboBox1.BackColor = Color.FromArgb(249, 249, 249)
                        ComboBox4.BackColor = Color.FromArgb(249, 249, 249)
                        ComboBox5.BackColor = Color.FromArgb(249, 249, 249)
                        ComboBox1.ForeColor = Color.Black
                        ComboBox4.ForeColor = Color.Black
                        ComboBox5.ForeColor = Color.Black
                        TabPage1.BackColor = Color.FromArgb(249, 249, 249)
                        TabPage2.BackColor = Color.FromArgb(249, 249, 249)
                        TabPage3.BackColor = Color.FromArgb(249, 249, 249)
                        NavBarBackPic.Image = back_Pic.Image
                        WelcomeTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_light_top_navbar)
                        InstructionsTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_light_top_navbar)
                        HelpTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_light_top_navbar)
                        AboutTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_light_top_navbar)
                        SettingsTopBarPic.Image = New Bitmap(My.Resources.panel_indicator_light_top_navbar)
                        PictureBox8.Image = WelcomePic.Image
                        PictureBox9.Image = InstCreatePic.Image
                        PictureBox10.Image = InstructionPic.Image
                        PictureBox33.Image = HelpPic.Image
                        PictureBox34.Image = InfoPic.Image
                        PictureBox7.Image = SettingsPic.Image
                        ' LinkLabels
                        LinkLabel1.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel2.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel3.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel4.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel5.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel6.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel7.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel8.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel9.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel10.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel11.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel12.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel13.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel14.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel16.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel17.LinkColor = Color.FromArgb(1, 92, 186)
                        LinkLabel18.LinkColor = Color.FromArgb(1, 92, 186)
                        TargetInstallerLinkLabel.LinkColor = Color.FromArgb(1, 92, 186)
                        LogViewLink.LinkColor = Color.FromArgb(1, 92, 186)
                    End If
                Else
                    If My.Computer.Info.OSFullName.Contains("Windows 7") Then
                        MsgBox("This feature is not supported on Windows 7", vbOKOnly + vbInformation, "Automatic color mode")
                    ElseIf My.Computer.Info.OSFullName.Contains("Windows 8") Or My.Computer.Info.OSFullName.Contains("Windows 8.1") Then
                        MsgBox("This feature is not supported on Windows 8/8.1", vbOKOnly + vbInformation, "Automatic color mode")
                    ElseIf My.Computer.Info.OSFullName.Contains("Windows Server 2008") Or My.Computer.Info.OSFullName.Contains("Windows Server 2012") Then
                        MsgBox("This feature is not supported on Windows Server 2008/2012", vbOKOnly + vbInformation, "Automatic color mode")
                    End If
                End If
            Catch NREx As NullReferenceException
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    MsgBox("Cannot set the color mode to match the system's. Perhaps the registry key is not available.", vbOKOnly + vbExclamation, "Automatic color mode")
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    MsgBox("No se pudo establecer el modo de color para combinar con el del sistema. Quizá la clave de registro no está disponible.", vbOKOnly + vbExclamation, "Modo de color automático")
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    MsgBox("Impossible de régler le mode de couleur pour qu'il corresponde à celui du système. La clé de registre n'est peut-être pas disponible.", vbOKOnly + vbExclamation, "Mode couleur automatique")
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        MsgBox("Cannot set the color mode to match the system's. Perhaps the registry key is not available.", vbOKOnly + vbExclamation, "Automatic color mode")
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        MsgBox("No se pudo establecer el modo de color para combinar con el del sistema. Quizá la clave de registro no está disponible.", vbOKOnly + vbExclamation, "Modo de color automático")
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        MsgBox("Impossible de régler le mode de couleur pour qu'il corresponde à celui du système. La clé de registre n'est peut-être pas disponible.", vbOKOnly + vbExclamation, "Mode couleur automatique")
                    End If
                End If
            End Try
        End If
        ApplyNavBarImages()
        ' These lines are ubiquitous
        TopRightResizePanel.BackColor = BackColor
        BottomRightResizePanel.BackColor = WelcomePanel.BackColor
        UpdatePanelProperties()
    End Sub

    Private Sub SettingsPic_Click(sender As Object, e As EventArgs) Handles SettingsPic.Click, PictureBox7.Click
        PanelIndicatorPic.Top = SettingsPic.Top + 2
        PanelIndicatorPic.Anchor = CType((AnchorStyles.Bottom Or AnchorStyles.Left), AnchorStyles)
        WelcomePanel.Visible = False
        InstCreatePanel.Visible = False
        HelpPanel.Visible = False
        InstrPanel.Visible = False
        SettingPanel.Visible = True
        InfoPanel.Visible = False
        If BackColor = Color.FromArgb(243, 243, 243) Then
            SettingsPic.Image = New Bitmap(My.Resources.settings_filled)
            WelcomePic.Image = New Bitmap(My.Resources.home)
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create)
            InstructionPic.Image = New Bitmap(My.Resources.instructions)
            HelpPic.Image = New Bitmap(My.Resources.help)
            InfoPic.Image = New Bitmap(My.Resources.info)
        Else
            SettingsPic.Image = New Bitmap(My.Resources.settings_dark_filled)
            WelcomePic.Image = New Bitmap(My.Resources.home_dark)
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark)
            InstructionPic.Image = New Bitmap(My.Resources.instructions_dark)
            HelpPic.Image = New Bitmap(My.Resources.help_dark)
            InfoPic.Image = New Bitmap(My.Resources.info_dark)
        End If
        WelcomeTopBarPic.Visible = False
        InstCreateTopBarPic.Visible = False
        InstructionsTopBarPic.Visible = False
        HelpTopBarPic.Visible = False
        AboutTopBarPic.Visible = False
        SettingsTopBarPic.Visible = True
        ApplyNavBarImages()
    End Sub

    Private Sub PictureBox13_Click(sender As Object, e As EventArgs) Handles PictureBox13.Click, PictureBox15.Click, Label35.Click, Label36.Click
        EnableBackPic()
        SettingPanel.Visible = False
        Settings_PersonalizationPanel.Visible = True
        If DialogInt = 0 Then
            DialogInt += 1
            If My.Computer.Info.OSFullName.Contains("Windows 7") Or My.Computer.Info.OSFullName.Contains("Windows 8") Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    MsgBox("The automatic color mode option is not accessible under Windows 7 and Windows 8/8.1, because they do not support dark mode." & CrLf & "The way this program sets the color mode is by looking at the " & Quote & "AppsUseLightMode" & Quote & " registry key, which is present since Windows 10 version 1809 (October 2018 Update), and is carried on by further versions of Windows 10, and Windows 11." & CrLf & "- If its value is 0, then the program will use dark mode" & CrLf & "- If its value is 1, then the program will use light mode" & CrLf & CrLf & "You can still set the color mode manually.", vbOKOnly + vbInformation, "Automatic color mode")
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    MsgBox("La opción de modo de color automático no es accesible en Windows 7 y Windows 8/8.1, porque no soportan el modo oscuro." & CrLf & "La manera en la que este programa establece el modo de color es observando la clave del registro " & Quote & "AppsUseLightMode" & Quote & " , que está presente desde Windows 10 versión 1809 (actualización de Octubre de 2018), y se mantiene en versiones próximas de Windows 10, y Windows 11." & CrLf & "- Si su valor es 0, el programa utilizará el modo oscuro" & CrLf & "- Si su valor es 1, el programa utilizará el modo claro" & CrLf & CrLf & "Todavía puede establecer el modo de color manualmente.", vbOKOnly + vbInformation, "Modo de color automático")
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    MsgBox("L'option de mode couleur automatique n'est pas accessible sous Windows 7 et Windows 8/8.1, car ils ne prennent pas en charge le mode sombre." & CrLf & "La façon dont ce programme définit le mode de couleur est en regardant la clé de registre " & Quote & "AppsUseLightMode" & Quote & ", qui est présente depuis la version 1809 de Windows 10 (mise à jour d'octobre 2018), et est reprise par les versions ultérieures de Windows 10, et Windows 11." & CrLf & "- Si sa valeur est 0, alors le programme utilisera le mode sombre." & CrLf & "- Si sa valeur est 1, alors le programme utilisera le mode clair." & CrLf & CrLf & "Vous pouvez toujours définir le mode couleur manuellement.", vbOKOnly + vbInformation, "Mode couleur automatique")
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        MsgBox("The automatic color mode option is not accessible under Windows 7 and Windows 8/8.1, because they do not support dark mode." & CrLf & "The way this program sets the color mode is by looking at the " & Quote & "AppsUseLightMode" & Quote & " registry key, which is present since Windows 10 version 1809 (October 2018 Update), and is carried on by further versions of Windows 10, and Windows 11." & CrLf & "- If its value is 0, then the program will use dark mode" & CrLf & "- If its value is 1, then the program will use light mode" & CrLf & CrLf & "You can still set the color mode manually.", vbOKOnly + vbInformation, "Automatic color mode")
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        MsgBox("La opción de modo de color automático no es accesible en Windows 7 y Windows 8/8.1, porque no soportan el modo oscuro." & CrLf & "La manera en la que este programa establece el modo de color es observando la clave del registro " & Quote & "AppsUseLightMode" & Quote & " , que está presente desde Windows 10 versión 1809 (actualización de Octubre de 2018), y se mantiene en versiones próximas de Windows 10, y Windows 11." & CrLf & "- Si su valor es 0, el programa utilizará el modo oscuro" & CrLf & "- Si su valor es 1, el programa utilizará el modo claro" & CrLf & CrLf & "Todavía puede establecer el modo de color manualmente.", vbOKOnly + vbInformation, "Modo de color automático")
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        MsgBox("L'option de mode couleur automatique n'est pas accessible sous Windows 7 et Windows 8/8.1, car ils ne prennent pas en charge le mode sombre." & CrLf & "La façon dont ce programme définit le mode de couleur est en regardant la clé de registre " & Quote & "AppsUseLightMode" & Quote & ", qui est présente depuis la version 1809 de Windows 10 (mise à jour d'octobre 2018), et est reprise par les versions ultérieures de Windows 10, et Windows 11." & CrLf & "- Si sa valeur est 0, alors le programme utilisera le mode sombre." & CrLf & "- Si sa valeur est 1, alors le programme utilisera le mode clair." & CrLf & CrLf & "Vous pouvez toujours définir le mode couleur manuellement.", vbOKOnly + vbInformation, "Mode couleur automatique")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            Label28.Text = "DPI scale to be applied: " & TrackBar1.Value & "%"
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            Label28.Text = "Escala DPI a ser aplicada: " & TrackBar1.Value & "%"
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            Label28.Text = "Échelle DPI à appliquer : " & TrackBar1.Value & "%"
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label28.Text = "DPI scale to be applied: " & TrackBar1.Value & "%"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label28.Text = "Escala DPI a ser aplicada: " & TrackBar1.Value & "%"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label28.Text = "Échelle DPI à appliquer : " & TrackBar1.Value & "%"
            End If
        End If
    End Sub

    Private Sub back_Pic_Click(sender As Object, e As EventArgs) Handles back_Pic.Click, NavBarBackPic.Click
        If Settings_PersonalizationPanel.Visible = True Then
            SettingPanel.Visible = True
            Settings_PersonalizationPanel.Visible = False
        End If
        If Settings_FunctionalityPanel.Visible = True Then
            If MOLinkIsClicked Then
                InstCreatePanel.Visible = True
                Settings_FunctionalityPanel.Visible = False
                ApplyNavBarImages()
                PanelIndicatorPic.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Left), AnchorStyles)
                PanelIndicatorPic.Top = InstCreatePic.Top + 2
                If BackColor = Color.FromArgb(243, 243, 243) Then
                    SettingsPic.Image = New Bitmap(My.Resources.settings)
                ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
                    SettingsPic.Image = New Bitmap(My.Resources.settings_dark)
                End If
            Else
                SettingPanel.Visible = True
                Settings_FunctionalityPanel.Visible = False
            End If
        End If
        If SettingReviewPanel.Visible = True Then
            InstCreateInt = 0
            SettingReviewPanel.Visible = False
            InstCreatePanel.Visible = True
        End If
        DisableBackPic()
    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click
        MOLinkIsClicked = False
        EnableBackPic()
        LogoPic.Left = 48
        ProgramTitleLabel.Left = 102
        SettingPanel.Visible = False
        Settings_FunctionalityPanel.Visible = True
        If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
            If Not File.Exists(".\prog_bin\regtweak.bat") Then
                ComboBox5.Items.Remove("REGTWEAK")
                ComboBox5.SelectedItem = "WIMR"
                REGTWEAKToolStripMenuItem.Visible = False
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    MsgBox("REGTWEAK is not available on this copy of the program. This can occur if the script isn't found. Make sure you have copied all essential files to the runtime directory.", vbOKOnly + vbExclamation, "A method is unavailable")
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    MsgBox("REGTWEAK no está disponible en esta copia del programa. Esto puede ocurrir si el script no ha sido encontrado. Asegúrese de que tiene copiado todos los archivos esenciales al directorio de ejecución.", vbOKOnly + vbExclamation, "Un método no está disponible")
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    MsgBox("REGTWEAK n'est pas disponible sur cette copie du programme. Cela peut se produire si le script est introuvable. Assurez-vous que vous avez copié tous les fichiers essentiels dans le répertoire d'exécution.", vbOKOnly + vbExclamation, "Une méthode n'est pas disponible")
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        MsgBox("REGTWEAK is not available on this copy of the program. This can occur if the script isn't found. Make sure you have copied all essential files to the runtime directory.", vbOKOnly + vbExclamation, "A method is unavailable")
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        MsgBox("REGTWEAK no está disponible en esta copia del programa. Esto puede ocurrir si el script no ha sido encontrado. Asegúrese de que tiene copiado todos los archivos esenciales al directorio de ejecución.", vbOKOnly + vbExclamation, "Un método no está disponible")
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        MsgBox("REGTWEAK n'est pas disponible sur cette copie du programme. Cela peut se produire si le script est introuvable. Assurez-vous que vous avez copié tous les fichiers essentiels dans le répertoire d'exécution.", vbOKOnly + vbExclamation, "Une méthode n'est pas disponible")
                    End If
                End If
            Else
                If Not ComboBox5.Items.Contains("REGTWEAK") Then
                    ComboBox5.Items.Add("REGTWEAK")
                    REGTWEAKToolStripMenuItem.Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub LinkLabel17_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel17.LinkClicked
        MOLinkIsClicked = True
        EnableBackPic()
        LogoPic.Left = 48
        ProgramTitleLabel.Left = 102
        InstCreatePanel.Visible = False
        Settings_FunctionalityPanel.Visible = True
        PanelIndicatorPic.Anchor = CType((AnchorStyles.Bottom Or AnchorStyles.Left), AnchorStyles)
        PanelIndicatorPic.Top = SettingsPic.Top + 2
        If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
            If Not File.Exists(".\prog_bin\regtweak.bat") Then
                ComboBox5.Items.Remove("REGTWEAK")
                ComboBox5.SelectedItem = "WIMR"
                REGTWEAKToolStripMenuItem.Visible = False
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    MsgBox("REGTWEAK is not available on this copy of the program. This can occur if the script isn't found. Make sure you have copied all essential files to the runtime directory.", vbOKOnly + vbExclamation, "A method is unavailable")
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    MsgBox("REGTWEAK no está disponible en esta copia del programa. Esto puede ocurrir si el script no ha sido encontrado. Asegúrese de que tiene copiado todos los archivos esenciales al directorio de ejecución.", vbOKOnly + vbExclamation, "Un método no está disponible")
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    MsgBox("REGTWEAK n'est pas disponible sur cette copie du programme. Cela peut se produire si le script est introuvable. Assurez-vous que vous avez copié tous les fichiers essentiels dans le répertoire d'exécution.", vbOKOnly + vbExclamation, "Une méthode n'est pas disponible")
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        MsgBox("REGTWEAK is not available on this copy of the program. This can occur if the script isn't found. Make sure you have copied all essential files to the runtime directory.", vbOKOnly + vbExclamation, "A method is unavailable")
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        MsgBox("REGTWEAK no está disponible en esta copia del programa. Esto puede ocurrir si el script no ha sido encontrado. Asegúrese de que tiene copiado todos los archivos esenciales al directorio de ejecución.", vbOKOnly + vbExclamation, "Un método no está disponible")
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        MsgBox("REGTWEAK n'est pas disponible sur cette copie du programme. Cela peut se produire si le script est introuvable. Assurez-vous que vous avez copié tous les fichiers essentiels dans le répertoire d'exécution.", vbOKOnly + vbExclamation, "Une méthode n'est pas disponible")
                    End If
                End If
            Else
                If Not ComboBox5.Items.Contains("REGTWEAK") Then
                    ComboBox5.Items.Add("REGTWEAK")
                    REGTWEAKToolStripMenuItem.Visible = True
                End If
            End If
        End If
        ApplyNavBarImages()
        If BackColor = Color.FromArgb(243, 243, 243) Then
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create)
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark)
        End If
    End Sub

    Private Sub Notify_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Notify.MouseDoubleClick
        Activate()
        ShowInTaskbar = True
        MiniModeDialog.Hide()
        WindowState = FormWindowState.Normal
        If BackColor = Color.FromArgb(243, 243, 243) Then
            closeBox.Image = New Bitmap(My.Resources.closebox)
        Else
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
        End If
    End Sub

    Private Sub Notify_BalloonTipClicked(sender As Object, e As EventArgs) Handles Notify.BalloonTipClicked
        Activate()
        ShowInTaskbar = True
        MiniModeDialog.Hide()
        WindowState = FormWindowState.Normal
        If BackColor = Color.FromArgb(243, 243, 243) Then
            closeBox.Image = New Bitmap(My.Resources.closebox)
        Else
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
        End If
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Activate()
        ShowInTaskbar = True
        MiniModeDialog.Hide()
        WindowState = FormWindowState.Normal
        If BackColor = Color.FromArgb(243, 243, 243) Then
            closeBox.Image = New Bitmap(My.Resources.closebox)
        Else
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        SaveSettingsFile()
        Notify.Visible = False
        End
    End Sub

    Private Sub NotifyIconCMS_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles NotifyIconCMS.Opening
        If WindowState = FormWindowState.Normal Or WindowState = FormWindowState.Maximized Then
            OpenToolStripMenuItem.Enabled = False
        Else
            OpenToolStripMenuItem.Enabled = True
        End If
        If BackSubPanel.Visible = True Then
            ViewInstallerHistoryToolStripMenuItem.Enabled = False
        Else
            ViewInstallerHistoryToolStripMenuItem.Enabled = True
        End If
    End Sub

    Private Sub PictureBox17_Click(sender As Object, e As EventArgs) Handles PictureBox17.Click
        InstHistPanel.InstallerListView.Items.Clear()
        InstHistPanel.InstallerEntryLabel.Text = "Installer history entries: " & InstHistPanel.InstallerListView.Items.Count
    End Sub

    Private Sub ViewInstallerHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewInstallerHistoryToolStripMenuItem.Click
        If Visible = False Or WindowState = FormWindowState.Minimized Then
            Activate()
            ShowInTaskbar = True
            WindowState = FormWindowState.Normal
            MiniModeDialog.Hide()
            BringToFront()
        End If
        BackSubPanel.Show()
        InstHistPanel.ShowDialog()
        InstHistPanel.Visible = True
        InstHistPanel.Visible = False
        BringToFront()
        If BackColor = Color.FromArgb(243, 243, 243) Then
            closeBox.Image = New Bitmap(My.Resources.closebox)
        Else
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
        End If
    End Sub

    Private Sub ScanButton_Click(sender As Object, e As EventArgs) Handles ScanButton.Click
        BringToFront()
        BackSubPanel.Show()
        ISOFileScanPanel.ShowDialog()
        ISOFileScanPanel.Visible = True
        ISOFileScanPanel.Visible = False
        BringToFront()
    End Sub

    Private Sub PictureBox19_Click(sender As Object, e As EventArgs) Handles PictureBox19.Click, Label42.Click, Label41.Click, PictureBox18.Click
        BringToFront()
        BackSubPanel.Show()
        PrefResetPanel.ShowDialog()
        PrefResetPanel.Visible = True
        PrefResetPanel.Visible = False
        BringToFront()
    End Sub

    Private Sub InstCreatePic_Click(sender As Object, e As EventArgs) Handles InstCreatePic.Click, PictureBox9.Click, Label99.Click
        DisableBackPic()
        PanelIndicatorPic.Top = InstCreatePic.Top + 2
        PanelIndicatorPic.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Left), AnchorStyles)
        If InstCreateInt = 0 Then
            WelcomePanel.Visible = False
            SettingPanel.Visible = False
            Settings_FunctionalityPanel.Visible = False
            Settings_PersonalizationPanel.Visible = False
            HelpPanel.Visible = False
            InfoPanel.Visible = False
            InstrPanel.Visible = False
            InstCreatePanel.Visible = True
            If BackColor = Color.FromArgb(243, 243, 243) Then
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_filled)
                WelcomePic.Image = New Bitmap(My.Resources.home)
                SettingsPic.Image = New Bitmap(My.Resources.settings)
                InstructionPic.Image = New Bitmap(My.Resources.instructions)
                HelpPic.Image = New Bitmap(My.Resources.help)
                InfoPic.Image = New Bitmap(My.Resources.info)
            Else
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark_filled)
                WelcomePic.Image = New Bitmap(My.Resources.home_dark)
                SettingsPic.Image = New Bitmap(My.Resources.settings_dark)
                InstructionPic.Image = New Bitmap(My.Resources.instructions_dark)
                HelpPic.Image = New Bitmap(My.Resources.help_dark)
                InfoPic.Image = New Bitmap(My.Resources.info_dark)
            End If
        ElseIf InstCreateInt = 1 Then
            WelcomePanel.Visible = False
            SettingPanel.Visible = False
            Settings_FunctionalityPanel.Visible = False
            Settings_PersonalizationPanel.Visible = False
            HelpPanel.Visible = False
            InfoPanel.Visible = False
            InstrPanel.Visible = False
            SettingReviewPanel.Visible = True
            If BackColor = Color.FromArgb(243, 243, 243) Then
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_filled)
                WelcomePic.Image = New Bitmap(My.Resources.home)
                SettingsPic.Image = New Bitmap(My.Resources.settings)
                InstructionPic.Image = New Bitmap(My.Resources.instructions)
                HelpPic.Image = New Bitmap(My.Resources.help)
                InfoPic.Image = New Bitmap(My.Resources.info)
            Else
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark_filled)
                WelcomePic.Image = New Bitmap(My.Resources.home_dark)
                SettingsPic.Image = New Bitmap(My.Resources.settings_dark)
                InstructionPic.Image = New Bitmap(My.Resources.instructions_dark)
                HelpPic.Image = New Bitmap(My.Resources.help_dark)
                InfoPic.Image = New Bitmap(My.Resources.info_dark)
            End If
        ElseIf InstCreateInt = 2 Or InstCreateInt = 3 Then
            WelcomePanel.Visible = False
            SettingPanel.Visible = False
            Settings_FunctionalityPanel.Visible = False
            Settings_PersonalizationPanel.Visible = False
            HelpPanel.Visible = False
            InfoPanel.Visible = False
            InstrPanel.Visible = False
            ProgressPanel.Visible = True
            If BackColor = Color.FromArgb(243, 243, 243) Then
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_filled)
                WelcomePic.Image = New Bitmap(My.Resources.home)
                SettingsPic.Image = New Bitmap(My.Resources.settings)
                InstructionPic.Image = New Bitmap(My.Resources.instructions)
                HelpPic.Image = New Bitmap(My.Resources.help)
                InfoPic.Image = New Bitmap(My.Resources.info)
            Else
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark_filled)
                WelcomePic.Image = New Bitmap(My.Resources.home_dark)
                SettingsPic.Image = New Bitmap(My.Resources.settings_dark)
                InstructionPic.Image = New Bitmap(My.Resources.instructions_dark)
                HelpPic.Image = New Bitmap(My.Resources.help_dark)
                InfoPic.Image = New Bitmap(My.Resources.info_dark)
            End If
        End If
        WelcomeTopBarPic.Visible = False
        InstCreateTopBarPic.Visible = True
        InstructionsTopBarPic.Visible = False
        HelpTopBarPic.Visible = False
        AboutTopBarPic.Visible = False
        SettingsTopBarPic.Visible = False
        ApplyNavBarImages()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If File.Exists(TextBox1.Text) Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                Win11PresenceSTLabel.Text = "Presence status: this file exists"
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                Win11PresenceSTLabel.Text = "Estado de presencia: este archivo existe"
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                Win11PresenceSTLabel.Text = "Statut de présence : ce fichier existe"
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    Win11PresenceSTLabel.Text = "Presence status: this file exists"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    Win11PresenceSTLabel.Text = "Estado de presencia: este archivo existe"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    Win11PresenceSTLabel.Text = "Statut de présence : ce fichier existe"
                End If
            End If
            TextBox1.ForeColor = ForeColor
            If Not ComboBox5.SelectedItem = "REGTWEAK" Then
                If File.Exists(TextBox2.Text) Then
                    Button6.Enabled = True
                Else
                    Button6.Enabled = False
                End If
            End If
        Else
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                Win11PresenceSTLabel.Text = "Presence status: this file does not exist"
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                Win11PresenceSTLabel.Text = "Estado de presencia: este archivo no existe"
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                Win11PresenceSTLabel.Text = "Statut de présence : ce fichier n'existe pas"
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    Win11PresenceSTLabel.Text = "Presence status: this file does not exist"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    Win11PresenceSTLabel.Text = "Estado de presencia: este archivo no existe"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    Win11PresenceSTLabel.Text = "Statut de présence : ce fichier n'existe pas"
                End If
            End If
            TextBox1.ForeColor = Color.Crimson
            Button6.Enabled = False
        End If
        If TextBox1.Text = "" Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                Win11PresenceSTLabel.Text = "Presence status: unknown"
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                Win11PresenceSTLabel.Text = "Estado de presencia: desconocido"
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                Win11PresenceSTLabel.Text = "Statut de présence : inconnu"
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    Win11PresenceSTLabel.Text = "Presence status: unknown"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    Win11PresenceSTLabel.Text = "Estado de presencia: desconocido"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    Win11PresenceSTLabel.Text = "Statut de présence : inconnu"
                End If
            End If
            Button6.Enabled = False
        End If
        If Not TextBox1.Text = "" And TextBox1.Text = TextBox2.Text Then
            TextBox1.ForeColor = Color.Crimson
            TextBox2.ForeColor = Color.Crimson
            Button6.Enabled = False
        Else
            If File.Exists(TextBox1.Text) Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    Win11PresenceSTLabel.Text = "Presence status: this file exists"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    Win11PresenceSTLabel.Text = "Estado de presencia: este archivo existe"
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    Win11PresenceSTLabel.Text = "Statut de présence : ce fichier existe"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        Win11PresenceSTLabel.Text = "Presence status: this file exists"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        Win11PresenceSTLabel.Text = "Estado de presencia: este archivo existe"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        Win11PresenceSTLabel.Text = "Statut de présence : ce fichier existe"
                    End If
                End If
                TextBox1.ForeColor = ForeColor
                If Not ComboBox5.SelectedItem = "REGTWEAK" Then
                    If File.Exists(TextBox2.Text) Then
                        Button6.Enabled = True
                    Else
                        Button6.Enabled = False
                    End If
                End If
            Else
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    Win11PresenceSTLabel.Text = "Presence status: this file does not exist"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    Win11PresenceSTLabel.Text = "Estado de presencia: este archivo no existe"
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    Win11PresenceSTLabel.Text = "Statut de présence : ce fichier n'existe pas"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        Win11PresenceSTLabel.Text = "Presence status: this file does not exist"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        Win11PresenceSTLabel.Text = "Estado de presencia: este archivo no existe"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        Win11PresenceSTLabel.Text = "Statut de présence : ce fichier n'existe pas"
                    End If
                End If
                TextBox1.ForeColor = Color.Crimson
                Button6.Enabled = False
            End If
            If TextBox1.Text = "" Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    Win11PresenceSTLabel.Text = "Presence status: unknown"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    Win11PresenceSTLabel.Text = "Estado de presencia: desconocido"
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    Win11PresenceSTLabel.Text = "Statut de présence : inconnu"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        Win11PresenceSTLabel.Text = "Presence status: unknown"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        Win11PresenceSTLabel.Text = "Estado de presencia: desconocido"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        Win11PresenceSTLabel.Text = "Statut de présence : inconnu"
                    End If
                End If
                Button6.Enabled = False
            End If
        End If
        If TextBox1.ForeColor = Color.Crimson Then
            Label60.Visible = False
            LinkLabel12.Visible = True
        Else
            If TextBox2.ForeColor = Color.Crimson Or TextBox3.ForeColor = Color.Crimson Or TextBox4.ForeColor = Color.Crimson Then
                Label60.Visible = False
                LinkLabel12.Visible = True
            Else
                Label60.Visible = True
                LinkLabel12.Visible = False
            End If
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If File.Exists(TextBox2.Text) Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                Win10PresenceSTLabel.Text = "Presence status: this file exists"
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                Win10PresenceSTLabel.Text = "Estado de presencia: este archivo existe"
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                Win10PresenceSTLabel.Text = "Statut de présence : ce fichier existe"
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    Win10PresenceSTLabel.Text = "Presence status: this file exists"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    Win10PresenceSTLabel.Text = "Estado de presencia: este archivo existe"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    Win10PresenceSTLabel.Text = "Statut de présence : ce fichier existe"
                End If
            End If
            TextBox2.ForeColor = ForeColor
            If File.Exists(TextBox1.Text) Then
                Button6.Enabled = True
            Else
                Button6.Enabled = False
            End If
        Else
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                Win10PresenceSTLabel.Text = "Presence status: this file does not exist"
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                Win10PresenceSTLabel.Text = "Estado de presencia: este archivo no existe"
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                Win10PresenceSTLabel.Text = "Statut de présence : ce fichier n'existe pas"
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    Win10PresenceSTLabel.Text = "Presence status: this file does not exist"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    Win10PresenceSTLabel.Text = "Estado de presencia: este archivo no existe"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    Win10PresenceSTLabel.Text = "Statut de présence : ce fichier n'existe pas"
                End If
            End If
            TextBox2.ForeColor = Color.Crimson
            Button6.Enabled = False
        End If
        If TextBox2.Text = "" Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                Win10PresenceSTLabel.Text = "Presence status: unknown"
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                Win10PresenceSTLabel.Text = "Estado de presencia: desconocido"
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                Win10PresenceSTLabel.Text = "Statut de présence : inconnu"
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    Win10PresenceSTLabel.Text = "Presence status: unknown"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    Win10PresenceSTLabel.Text = "Estado de presencia: desconocido"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    Win10PresenceSTLabel.Text = "Statut de présence : inconnu"
                End If
            End If
            If Not ComboBox5.SelectedItem = "REGTWEAK" Then
                Button6.Enabled = False
            End If
        End If
        If Not ComboBox5.SelectedItem = "REGTWEAK" Then
            If Not TextBox2.Text = "" And TextBox2.Text = TextBox1.Text Then
                TextBox1.ForeColor = Color.Crimson
                TextBox2.ForeColor = Color.Crimson
                Button6.Enabled = False
            Else
                If File.Exists(TextBox2.Text) Then
                    If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                        Win10PresenceSTLabel.Text = "Presence status: this file exists"
                    ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                        Win10PresenceSTLabel.Text = "Estado de presencia: este archivo existe"
                    ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                        Win10PresenceSTLabel.Text = "Statut de présence : ce fichier existe"
                    ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                            Win10PresenceSTLabel.Text = "Presence status: this file exists"
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                            Win10PresenceSTLabel.Text = "Estado de presencia: este archivo existe"
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                            Win10PresenceSTLabel.Text = "Statut de présence : ce fichier existe"
                        End If
                    End If
                    TextBox2.ForeColor = ForeColor
                    If File.Exists(TextBox1.Text) Then
                        Button6.Enabled = True
                    Else
                        Button6.Enabled = False
                    End If
                Else
                    If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                        Win10PresenceSTLabel.Text = "Presence status: this file does not exist"
                    ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                        Win10PresenceSTLabel.Text = "Estado de presencia: este archivo no existe"
                    ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                        Win10PresenceSTLabel.Text = "Statut de présence : ce fichier n'existe pas"
                    ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                            Win10PresenceSTLabel.Text = "Presence status: this file does not exist"
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                            Win10PresenceSTLabel.Text = "Estado de presencia: este archivo no existe"
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                            Win10PresenceSTLabel.Text = "Statut de présence : ce fichier n'existe pas"
                        End If
                    End If
                    TextBox2.ForeColor = Color.Crimson
                    Button6.Enabled = False
                End If
                If TextBox2.Text = "" Then
                    If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                        Win10PresenceSTLabel.Text = "Presence status: unknown"
                    ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                        Win10PresenceSTLabel.Text = "Estado de presencia: desconocido"
                    ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                        Win10PresenceSTLabel.Text = "Statut de présence : inconnu"
                    ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                            Win10PresenceSTLabel.Text = "Presence status: unknown"
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                            Win10PresenceSTLabel.Text = "Estado de presencia: desconocido"
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                            Win10PresenceSTLabel.Text = "Statut de présence : inconnu"
                        End If
                    End If
                    If Not ComboBox5.SelectedItem = "REGTWEAK" Then
                        Button6.Enabled = False
                    End If
                End If
            End If
            If TextBox2.ForeColor = Color.Crimson Then
                Label60.Visible = False
                LinkLabel12.Visible = True
            Else
                If TextBox1.ForeColor = Color.Crimson Or TextBox3.ForeColor = Color.Crimson Or TextBox4.ForeColor = Color.Crimson Then
                    Label60.Visible = False
                    LinkLabel12.Visible = True
                Else
                    Label60.Visible = True
                    LinkLabel12.Visible = False
                End If
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Win11FileSpecDialog.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Win10FileSpecDialog.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox3.Text = "Win11"
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If TextBox1.Text = "" Then
            Do Until Not TextBox1.Text = ""
                Win11FileSpecDialog.ShowDialog()
                If DialogResult.OK Then
                    TextBox1.Text = Win11FileSpecDialog.FileName
                    If Not File.Exists(TextBox1.Text) Then
                        TextBox1.Text = ""
                    End If
                End If
            Loop
        End If
        If Not ComboBox5.SelectedItem = "REGTWEAK" Then
            If TextBox2.Text = "" Then
                Do Until Not TextBox2.Text = ""
                    Win10FileSpecDialog.ShowDialog()
                    If DialogResult.OK Then
                        TextBox2.Text = Win10FileSpecDialog.FileName
                        If Not File.Exists(TextBox2.Text) Then
                            TextBox2.Text = ""
                        End If
                    End If
                Loop
            End If
        End If

        If TextBox3.Text = "" Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                MsgBox("The file name cannot be nothing. Please specify a file name and try again.", vbOKOnly + vbInformation, "File name")
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                MsgBox("El nombre del archivo no puede ser nada. Por favor, especifique un nombre de archivo e inténtelo de nuevo", vbOKOnly + vbInformation, "Nombre de archivo")
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                MsgBox("Le nom du fichier ne peut être rien. Veuillez spécifier un nom de fichier et réessayer.", vbOKOnly + vbInformation, "Nom de fichier")
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    MsgBox("The file name cannot be nothing. Please specify a file name and try again.", vbOKOnly + vbInformation, "File name")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    MsgBox("El nombre del archivo no puede ser nada. Por favor, especifique un nombre de archivo e inténtelo de nuevo", vbOKOnly + vbInformation, "Nombre de archivo")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    MsgBox("Le nom du fichier ne peut être rien. Veuillez spécifier un nom de fichier et réessayer.", vbOKOnly + vbInformation, "Nom de fichier")
                End If
            End If
        End If
        ' The following condition determines whether TextBox4 is "" or a path. If it is nothing, or
        ' the path does not exist, it will use the user folder to store the target image.
        ' This is not recommended if the end user doesn't have enough space on his/her local disk, so
        ' he/she must consider putting the target image on a different path
        If TextBox4.Text = "" Or Not Directory.Exists(TextBox4.Text) Then
            TextBox4.Text = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        End If
        Label67.Text = TextBox1.Text
        Label68.Text = TextBox2.Text
        InstCreateInt = 1
        InstCreatePanel.Visible = False
        EnableBackPic()
        SettingReviewPanel.Visible = True
        Label3.Visible = True
        LinkLabel2.Visible = True
        PictureBox5.Visible = True
        If TextBox4.Text.EndsWith("\") Then
            Label90.Text = TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso"
        Else
            Label90.Text = TextBox4.Text & "\" & TextBox3.Text & ".iso"
        End If

    End Sub

    Private Sub Label61_MouseEnter(sender As Object, e As EventArgs) Handles Label61.MouseEnter
        If BackColor = Color.FromArgb(243, 243, 243) Then
            Label61.ForeColor = Color.Black
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            Label61.ForeColor = Color.White
        End If
    End Sub

    Private Sub Label61_MouseLeave(sender As Object, e As EventArgs) Handles Label61.MouseLeave
        Label61.ForeColor = Color.DimGray
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            NameLabel.Text = "Name: " & TextBox3.Text
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            NameLabel.Text = "Nombre: " & TextBox3.Text
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            NameLabel.Text = "Nom : " & TextBox3.Text
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                NameLabel.Text = "Name: " & TextBox3.Text
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                NameLabel.Text = "Nombre: " & TextBox3.Text
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                NameLabel.Text = "Nom : " & TextBox3.Text
            End If
        End If
        If TextBox4.Text.EndsWith("\") Then
            If TextBox4.Text.Contains(" ") Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                    End If
                End If
            Else
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
                    End If
                End If
            End If
        Else
            If TextBox4.Text.Contains(" ") Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                    End If
                End If
            Else
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
                    End If
                End If
            End If
        End If
        If TextBox3.Text = "" Then
            Button6.Enabled = False
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                MsgBox("The file name cannot be nothing. Please specify a file name and try again.", vbOKOnly + vbInformation, "File name")
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                MsgBox("El nombre del archivo no puede ser nada. Por favor, especifique un nombre de archivo e inténtelo de nuevo", vbOKOnly + vbInformation, "Nombre de archivo")
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                MsgBox("Le nom du fichier ne peut être rien. Veuillez spécifier un nom de fichier et réessayer.", vbOKOnly + vbInformation, "Nom de fichier")
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    MsgBox("The file name cannot be nothing. Please specify a file name and try again.", vbOKOnly + vbInformation, "File name")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    MsgBox("El nombre del archivo no puede ser nada. Por favor, especifique un nombre de archivo e inténtelo de nuevo", vbOKOnly + vbInformation, "Nombre de archivo")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    MsgBox("Le nom du fichier ne peut être rien. Veuillez spécifier un nom de fichier et réessayer.", vbOKOnly + vbInformation, "Nom de fichier")
                End If
            End If
        Else
            Button6.Enabled = True
        End If
        If TextBox3.Text = "con" Or _
            TextBox3.Text = "CON" Or _
            TextBox3.Text = "aux" Or _
            TextBox3.Text = "AUX" Or _
            TextBox3.Text = "prn" Or _
            TextBox3.Text = "PRN" Or _
            TextBox3.Text = "nul" Or _
            TextBox3.Text = "NUL" Or _
            TextBox3.Text = "com1" Or _
            TextBox3.Text = "com2" Or _
            TextBox3.Text = "com3" Or _
            TextBox3.Text = "com4" Or _
            TextBox3.Text = "com5" Or _
            TextBox3.Text = "com6" Or _
            TextBox3.Text = "com7" Or _
            TextBox3.Text = "com8" Or _
            TextBox3.Text = "com9" Or _
            TextBox3.Text = "COM1" Or _
            TextBox3.Text = "COM2" Or _
            TextBox3.Text = "COM3" Or _
            TextBox3.Text = "COM4" Or _
            TextBox3.Text = "COM5" Or _
            TextBox3.Text = "COM6" Or _
            TextBox3.Text = "COM7" Or _
            TextBox3.Text = "COM8" Or _
            TextBox3.Text = "COM9" Or _
            TextBox3.Text = "lpt1" Or _
            TextBox3.Text = "lpt2" Or _
            TextBox3.Text = "lpt3" Or _
            TextBox3.Text = "lpt4" Or _
            TextBox3.Text = "lpt5" Or _
            TextBox3.Text = "lpt6" Or _
            TextBox3.Text = "lpt7" Or _
            TextBox3.Text = "lpt8" Or _
            TextBox3.Text = "lpt9" Or _
            TextBox3.Text = "LPT1" Or _
            TextBox3.Text = "LPT2" Or _
            TextBox3.Text = "LPT3" Or _
            TextBox3.Text = "LPT4" Or _
            TextBox3.Text = "LPT5" Or _
            TextBox3.Text = "LPT6" Or _
            TextBox3.Text = "LPT7" Or _
            TextBox3.Text = "LPT8" Or _
            TextBox3.Text = "LPT9" Or _
            TextBox3.Text.Contains("<") Or _
            TextBox3.Text.Contains(">") Or _
            TextBox3.Text.Contains(":") Or _
            TextBox3.Text.Contains(Quote) Or _
            TextBox3.Text.Contains("/") Or _
            TextBox3.Text.Contains("\") Or _
            TextBox3.Text.Contains("|") Or _
            TextBox3.Text.Contains("?") Or _
            TextBox3.Text.Contains("*") Then
            ' This is to prevent installer creation issues. If the installer name contains "con" for example, and is not just "con", then the program will allow it
            TextBox3.ForeColor = Color.Crimson
            Button6.Enabled = False
        Else
            TextBox3.ForeColor = ForeColor
            Button6.Enabled = True
        End If
        If TextBox3.ForeColor = Color.Crimson Then
            Label60.Visible = False
            LinkLabel12.Visible = True
        Else
            If TextBox2.ForeColor = Color.Crimson Or TextBox1.ForeColor = Color.Crimson Or TextBox4.ForeColor = Color.Crimson Then
                Label60.Visible = False
                LinkLabel12.Visible = True
            Else
                Label60.Visible = True
                LinkLabel12.Visible = False
            End If
        End If
    End Sub

    Private Sub AdminLabel_MouseHover(sender As Object, e As EventArgs) Handles AdminLabel.MouseHover
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            ConglomerateToolTip.SetToolTip(AdminLabel, "This program is running with administrative privileges")
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            ConglomerateToolTip.SetToolTip(AdminLabel, "Este programa se está ejecutando con privilegios administrativos")
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            ConglomerateToolTip.SetToolTip(AdminLabel, "Ce programme est exécuté avec des privilèges administratifs")
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                ConglomerateToolTip.SetToolTip(AdminLabel, "This program is running with administrative privileges")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                ConglomerateToolTip.SetToolTip(AdminLabel, "Este programa se está ejecutando con privilegios administrativos")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                ConglomerateToolTip.SetToolTip(AdminLabel, "Ce programme est exécuté avec des privilèges administratifs")
            End If
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        WelcomePanel.Visible = False
        SettingPanel.Visible = False
        Settings_FunctionalityPanel.Visible = False
        Settings_PersonalizationPanel.Visible = False
        HelpPanel.Visible = False
        InfoPanel.Visible = False
        InstrPanel.Visible = False
        SettingReviewPanel.Visible = True
        WelcomeTopBarPic.Visible = False
        InstCreateTopBarPic.Visible = True
        InstructionsTopBarPic.Visible = False
        HelpTopBarPic.Visible = False
        AboutTopBarPic.Visible = False
        SettingsTopBarPic.Visible = False
        PanelIndicatorPic.Top = InstCreatePic.Top + 2
        EnableBackPic()
        If BackColor = Color.FromArgb(243, 243, 243) Then
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create_filled)
            WelcomePic.Image = New Bitmap(My.Resources.home)
        Else
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark_filled)
            WelcomePic.Image = New Bitmap(My.Resources.home_dark)
        End If
        ApplyNavBarImages()
    End Sub

    Private Sub minBox_MouseHover(sender As Object, e As EventArgs) Handles minBox.MouseHover
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            ConglomerateToolTip.SetToolTip(minBox, "Minimize")
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            ConglomerateToolTip.SetToolTip(minBox, "Minimizar")
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            ConglomerateToolTip.SetToolTip(minBox, "Minimiser")
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                ConglomerateToolTip.SetToolTip(minBox, "Minimize")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                ConglomerateToolTip.SetToolTip(minBox, "Minimizar")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                ConglomerateToolTip.SetToolTip(minBox, "Minimiser")
            End If
        End If
    End Sub

    Private Sub maxBox_MouseHover(sender As Object, e As EventArgs) Handles maxBox.MouseHover
        If WindowState = FormWindowState.Normal Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                ConglomerateToolTip.SetToolTip(maxBox, "Maximize")
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                ConglomerateToolTip.SetToolTip(maxBox, "Maximizar")
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                ConglomerateToolTip.SetToolTip(maxBox, "Maximiser")
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    ConglomerateToolTip.SetToolTip(maxBox, "Maximize")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    ConglomerateToolTip.SetToolTip(maxBox, "Maximizar")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    ConglomerateToolTip.SetToolTip(maxBox, "Maximiser")
                End If
            End If
        ElseIf WindowState = FormWindowState.Maximized Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                ConglomerateToolTip.SetToolTip(maxBox, "Restore down")
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                ConglomerateToolTip.SetToolTip(maxBox, "Restaurar tamaño")
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                ConglomerateToolTip.SetToolTip(maxBox, "Restaurer en bas")
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    ConglomerateToolTip.SetToolTip(maxBox, "Restore down")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    ConglomerateToolTip.SetToolTip(maxBox, "Restaurar tamaño")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    ConglomerateToolTip.SetToolTip(maxBox, "Restaurer en bas")
                End If
            End If
        End If
    End Sub

    Private Sub closeBox_MouseHover(sender As Object, e As EventArgs) Handles closeBox.MouseHover
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            ConglomerateToolTip.SetToolTip(closeBox, "Close")
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            ConglomerateToolTip.SetToolTip(closeBox, "Cerrar")
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            ConglomerateToolTip.SetToolTip(closeBox, "Fermer")
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                ConglomerateToolTip.SetToolTip(closeBox, "Close")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                ConglomerateToolTip.SetToolTip(closeBox, "Cerrar")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                ConglomerateToolTip.SetToolTip(closeBox, "Fermer")
            End If
        End If
    End Sub

    Sub CheckWimEsdExistence()
        ' Check ISO files for WIM or ESD file presence
        WimEsd = ".\prog_bin\7z l " & Quote & TextBox1.Text & Quote    ' Beginning with Windows 11
        File.WriteAllText(".\temp.bat", OffEcho & CrLf & WimEsd & " > " & Quote & ".\temp.txt" & Quote, ASCII)
        Process.Start(".\temp.bat").WaitForExit()
        ImageFileListDialog.TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\temp.txt")
        If File.Exists(".\temp.txt") Then
            File.Delete(".\temp.txt")
        End If
        If ImageFileListDialog.TextBox1.Text.Contains("install.wim") Then
            Win11ESD = 0
        ElseIf ImageFileListDialog.TextBox1.Text.Contains("install.esd") Then
            Win11ESD = 1
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        FileCount = 0
        DelFileCount = 0
        WarningText.Clear()
        ErrorText.Clear()
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            Label82.Text = "Progress"
            Label83.Text = "That's all the information we need right now. The installer creation will take a few minutes, so please be patient."
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            Label82.Text = "Progreso"
            Label83.Text = "Ésta es toda la información que necesitamos en este momento. Esto tardará unos minutos, por lo que sea paciente."
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            Label82.Text = "Progrès"
            Label83.Text = "C'est toute l'information dont nous avons besoin pour le moment. La création de l'installateur prendra quelques minutes, alors soyez patient."
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label82.Text = "Progress"
                Label83.Text = "That's all the information we need right now. The installer creation will take a few minutes, so please be patient."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label82.Text = "Progreso"
                Label83.Text = "Ésta es toda la información que necesitamos en este momento. Esto tardará unos minutos, por lo que sea paciente."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label82.Text = "Progrès"
                Label83.Text = "C'est toute l'information dont nous avons besoin pour le moment. La création de l'installateur prendra quelques minutes, alors soyez patient."
            End If
        End If
        DisableBackPic()
        TableLayoutPanel3.Visible = False
        InstallerCreationMethodToolStripMenuItem.Enabled = False
        StInstCreateTime = Now
        InstCreateInt = 2
        SettingReviewPanel.Visible = False
        Label84.Visible = True
        Label85.Visible = True
        Label86.Visible = True
        Label87.Visible = True
        Label88.Visible = True
        Button9.Visible = True
        GroupBox10.Visible = True
        InstSTLabel.Visible = True
        InstallerProgressBar.Visible = True
        CheckBox4.Visible = False
        LogViewLink.Visible = False
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            Button10.Text = "Cancel"
            InstSTLabel.Text = "The installer might take some time to create"
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            Button10.Text = "Cancelar"
            InstSTLabel.Text = "El instalador podría tardar algo de tiempo para crearse"
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            Button10.Text = "Annuler"
            InstSTLabel.Text = "L'installateur peut prendre un certain temps pour créer"
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Button10.Text = "Cancel"
                InstSTLabel.Text = "The installer might take some time to create"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Button10.Text = "Cancelar"
                InstSTLabel.Text = "El instalador podría tardar algo de tiempo para crearse"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Button10.Text = "Annuler"
                InstSTLabel.Text = "L'installateur peut prendre un certain temps pour créer"
            End If
        End If
        ProgressPanel.Visible = True
        ' The following lines of code reset the *Count variables (declared at the beginning of the file)
        ErrorCount = 0
        WarnCount = 0
        MessageCount = 0
        ' The following line of code clears all text from the LogBox (in case if there's text)...
        LogBox.Clear()
        ' ...and this line of code prints version information on the LogBox
        If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
            LogBox.AppendText("Windows 11 Manual Installer (administrator mode)" & CrLf & "version " & VerStr)
        Else
            LogBox.AppendText("Windows 11 Manual Installer" & CrLf & "version " & VerStr)
        End If
        ' If an installer creation process is incomplete (due to an exception or user cancellation), delete
        ' everything
        If Directory.Exists(".\temp") Then
            LogBox.AppendText(CrLf & "Looks like an installer was being created, but something interrumpted the process. Deleting everything...")
            For Each deletedFile In My.Computer.FileSystem.GetFiles(".\temp", FileIO.SearchOption.SearchAllSubDirectories)
                Try
                    File.Delete(deletedFile)
                Catch ex As Exception
                    File.WriteAllText(".\temp.bat", OffEcho & CrLf & "del " & Quote & deletedFile & Quote & " /f /q", ASCII)
                    Process.Start(".\temp.bat").WaitForExit()
                End Try
            Next
            File.Delete(".\temp.bat")
            Try
                Directory.Delete(".\temp")
            Catch ex As Exception
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & EmergencyFolderDelete, ASCII)
                Process.Start(".\temp.bat").WaitForExit()
                File.Delete(".\temp.bat")
            End Try
        End If
        If File.Exists(".\install.esd") Then
            File.Delete(".\install.esd")
        End If
        If File.Exists(".\install.wim") Then
            File.Delete(".\install.wim")
        End If
        If File.Exists(".\appraiser.dll") And File.Exists(".\appraiserres.dll") Then
            File.Delete(".\appraiser.dll")
            File.Delete(".\appraiserres.dll")
        End If
        If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
            If Directory.Exists(".\wimmount") Then
                If Directory.Exists(".\wimmount\Windows") Then
                    File.WriteAllText(".\temp.bat", OffEcho & CrLf & DismUnmount, ASCII)
                    Process.Start(".\temp.bat").WaitForExit()
                End If
                Directory.Delete(".\wimmount")
            End If
            If File.Exists(".\boot.wim") Then
                File.Delete(".\boot.wim")
            End If
        End If
        If File.Exists(".\Win11.iso") Then
            File.Delete(".\Win11.iso")
        End If
        If File.Exists(".\Win10.iso") Then
            File.Delete(".\Win10.iso")
        End If
        ' The following lines of code reset everything in this panel (ProgressPanel)
        CheckPic1.Visible = False
        CheckPic2.Visible = False
        CheckPic3.Visible = False
        CheckPic4.Visible = False
        CheckPic5.Visible = False
        InstallerProgressBar.Value = 0
        CompPic.Visible = False
        ' The following lines of code set the reused variables (declared at the beginning of the file)
        W11IWIMISOEx = ".\prog_bin\7z e " & Quote & TextBox1.Text & Quote & " " & Quote & "sources\install.wim" & Quote & " -o."
        W10ISOEx = ".\prog_bin\7z x " & Quote & TextBox2.Text & Quote & " " & Quote & "-o.\temp" & Quote
        W11ISOEx = ".\prog_bin\7z x " & Quote & TextBox1.Text & Quote & " " & Quote & "-o.\temp" & Quote
        W10AR_ARRDLLEx = ".\prog_bin\7z e " & Quote & TextBox2.Text & Quote & " " & Quote & "sources\appraiser.dll" & Quote & " -o."
        W10AR_ARRDLLEx_2 = ".\prog_bin\7z e " & Quote & TextBox2.Text & Quote & " " & Quote & "sources\appraiserres.dll" & Quote & " -o."
        ' New variables
        W11IESDISOEx = ".\prog_bin\7z e " & Quote & TextBox1.Text & Quote & " " & Quote & "sources\install.esd" & Quote & " -o."
        If TextBox4.Text.EndsWith("\") Then
            OSCDIMG_CSM = ".\prog_bin\oscdimg.exe -l" & LabelText.Text & " -m -u2 -b.\temp\boot\etfsboot.com .\temp " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
            OSCDIMG_UEFI = ".\prog_bin\oscdimg.exe -l" & LabelText.Text & " -m -u2 -b.\temp\boot\Efisys.bin -pEF .\temp " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
        Else
            OSCDIMG_CSM = ".\prog_bin\oscdimg.exe -l" & LabelText.Text & " -m -u2 -b.\temp\boot\etfsboot.com .\temp " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
            OSCDIMG_UEFI = ".\prog_bin\oscdimg.exe -l" & LabelText.Text & " -m -u2 -b.\temp\boot\Efisys.bin -pEF .\temp " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
        End If
        EmergencyFolderDelete = "rd " & Quote & ".\temp" & Quote & " /s /q"
        Label84.ForeColor = Color.DimGray
        Label84.Font = New Font("Segoe UI", 9.75)
        Label85.ForeColor = Color.DimGray
        Label85.Font = New Font("Segoe UI", 9.75)
        Label86.ForeColor = Color.DimGray
        Label86.Font = New Font("Segoe UI", 9.75)
        Label87.ForeColor = Color.DimGray
        Label87.Font = New Font("Segoe UI", 9.75)
        Label88.ForeColor = Color.DimGray
        Label88.Font = New Font("Segoe UI", 9.75)
        LogBox.AppendText(CrLf & "Started installer creation at: " & StInstCreateTime)
        LogBox.AppendText(CrLf & CrLf & "Installer creation options:")      ' Show inst. creation options
        LogBox.AppendText(CrLf & "- Windows 11 image: " & ControlChars.Quote & TextBox1.Text & ControlChars.Quote)
        LogBox.AppendText(CrLf & "- Windows 10 image: " & ControlChars.Quote & TextBox2.Text & ControlChars.Quote)
        If RadioButton1.Checked = True Then
            LogBox.AppendText(CrLf & "- Platform compatibility: BIOS/UEFI-CSM (platform ID: 0x00)")
        Else
            LogBox.AppendText(CrLf & "- Platform compatibility: UEFI (platform ID: 0xEF)")
        End If
        LogBox.AppendText(CrLf & "- Installer creation method: " & ComboBox5.SelectedItem)
        LogBox.AppendText(CrLf & "- Installer label: " & LabelText.Text)
        If TextBox4.Text.EndsWith("\") Then
            LogBox.AppendText(CrLf & "- Target image (location and name): " & ControlChars.Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & ControlChars.Quote)
        Else
            LogBox.AppendText(CrLf & "- Target image (location and name): " & ControlChars.Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & ControlChars.Quote)
        End If

        If BackColor = Color.FromArgb(243, 243, 243) Then
            Label84.ForeColor = Color.Black
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            Label84.ForeColor = Color.White
        End If
        Label84.Font = New Font("Segoe UI", 9.75, FontStyle.Bold)
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            InstSTLabel.Text = "Ready to copy the files to the local disk"
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            InstSTLabel.Text = "Preparados para copiar los archivos al disco local"
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            InstSTLabel.Text = "Prêt à copier les fichiers sur le disque local"
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                InstSTLabel.Text = "Ready to copy the files to the local disk"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                InstSTLabel.Text = "Preparados para copiar los archivos al disco local"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                InstSTLabel.Text = "Prêt à copier les fichiers sur le disque local"
            End If
        End If
        BringToFront()
        BackSubPanel.Show()
        FileCopyPanel.ShowDialog()
        FileCopyPanel.Visible = True
        FileCopyPanel.Visible = False
        BringToFront()
        If FileCopyPanel.DialogResult = Windows.Forms.DialogResult.Yes Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                InstSTLabel.Text = "Copying the ISO files to the local disk..."
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                InstSTLabel.Text = "Copiando los archivos ISO al disco local..."
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                InstSTLabel.Text = "Copier les fichiers ISO sur le disque local..."
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    InstSTLabel.Text = "Copying the ISO files to the local disk..."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    InstSTLabel.Text = "Copiando los archivos ISO al disco local..."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    InstSTLabel.Text = "Copier les fichiers ISO sur le disque local..."
                End If
            End If
            LogBox.AppendText(CrLf & "Copying the ISO files to the local disk...")
            Try
                If ComboBox5.SelectedItem = "REGTWEAK" Then
                    File.Copy(TextBox1.Text, ".\Win11.iso")
                    LogBox.AppendText(" Done")
                Else
                    File.Copy(TextBox1.Text, ".\Win11.iso")
                    LogBox.AppendText(" 1/2...")
                    File.Copy(TextBox2.Text, ".\Win10.iso")
                    LogBox.AppendText(" 2/2 Done")
                End If
            Catch DNFEx As DirectoryNotFoundException
                LogBox.AppendText(" Failed")
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    InstSTLabel.Text = "Falló la copia de los archivos ISO al disco local."
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    InstSTLabel.Text = "Échec de la copie des fichiers ISO sur le disque local."
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        InstSTLabel.Text = "Falló la copia de los archivos ISO al disco local."
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        InstSTLabel.Text = "Échec de la copie des fichiers ISO sur le disque local."
                    End If
                End If
                BringToFront()
                BackSubPanel.Show()
                FileNotFoundPanel.ShowDialog()
                FileNotFoundPanel.Visible = True
                FileNotFoundPanel.Visible = False
                BringToFront()
                If FileNotFoundPanel.DialogResult = Windows.Forms.DialogResult.Cancel Then
                    ' This is done for testing if the source drive is still mounted
                    If Directory.Exists(TextBox4.Text) Then
                        ' OK
                    Else
                        BringToFront()
                        BackSubPanel.Show()
                        VolumeConnectPanel.ShowDialog()
                        VolumeConnectPanel.Visible = True
                        VolumeConnectPanel.Visible = False
                        BringToFront()
                    End If
                ElseIf FileNotFoundPanel.DialogResult = Windows.Forms.DialogResult.OK Then
                    Try
                        If ComboBox5.SelectedItem = "REGTWEAK" Then
                            File.Copy(TextBox1.Text, ".\Win11.iso")
                            LogBox.AppendText(" Done")
                        Else
                            File.Copy(TextBox1.Text, ".\Win11.iso")
                            LogBox.AppendText(" 1/2...")
                            File.Copy(TextBox2.Text, ".\Win10.iso")
                            LogBox.AppendText(" 2/2 Done")
                        End If
                    Catch DNFEx2 As DirectoryNotFoundException
                        LogBox.AppendText(" Failed")
                        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                            InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
                        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                            InstSTLabel.Text = "Falló la copia de los archivos ISO al disco local."
                        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                            InstSTLabel.Text = "Échec de la copie des fichiers ISO sur le disque local."
                        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                InstSTLabel.Text = "Falló la copia de los archivos ISO al disco local."
                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                                InstSTLabel.Text = "Échec de la copie des fichiers ISO sur le disque local."
                            End If
                        End If
                        BringToFront()
                        BackSubPanel.Show()
                        VolumeConnectPanel.ShowDialog()
                        VolumeConnectPanel.Visible = True
                        VolumeConnectPanel.Visible = False
                        BringToFront()
                    Catch FNFEx2 As FileNotFoundException
                        LogBox.AppendText(" Failed")
                        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                            InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
                        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                            InstSTLabel.Text = "Falló la copia de los archivos ISO al disco local."
                        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                            InstSTLabel.Text = "Échec de la copie des fichiers ISO sur le disque local."
                        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                InstSTLabel.Text = "Falló la copia de los archivos ISO al disco local."
                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                                InstSTLabel.Text = "Échec de la copie des fichiers ISO sur le disque local."
                            End If
                        End If
                        BringToFront()
                        BackSubPanel.Show()
                        VolumeConnectPanel.ShowDialog()
                        VolumeConnectPanel.Visible = True
                        VolumeConnectPanel.Visible = False
                        BringToFront()
                    End Try
                End If
            Catch FNFEx As FileNotFoundException
                LogBox.AppendText(" Failed")
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    InstSTLabel.Text = "Falló la copia de los archivos ISO al disco local."
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    InstSTLabel.Text = "Échec de la copie des fichiers ISO sur le disque local."
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        InstSTLabel.Text = "Falló la copia de los archivos ISO al disco local."
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        InstSTLabel.Text = "Échec de la copie des fichiers ISO sur le disque local."
                    End If
                End If
                BringToFront()
                BackSubPanel.Show()
                FileNotFoundPanel.ShowDialog()
                FileNotFoundPanel.Visible = True
                FileNotFoundPanel.Visible = False
                BringToFront()
                If FileNotFoundPanel.DialogResult = Windows.Forms.DialogResult.Cancel Then
                    ' This is done for testing if the source drive is still mounted
                    If Directory.Exists(TextBox4.Text) Then
                        ' OK
                    Else
                        BringToFront()
                        BackSubPanel.Show()
                        VolumeConnectPanel.ShowDialog()
                        VolumeConnectPanel.Visible = True
                        VolumeConnectPanel.Visible = False
                        BringToFront()
                    End If
                ElseIf FileNotFoundPanel.DialogResult = Windows.Forms.DialogResult.OK Then
                    Try
                        If ComboBox5.SelectedItem = "REGTWEAK" Then
                            File.Copy(TextBox1.Text, ".\Win11.iso")
                            LogBox.AppendText(" Done")
                        Else
                            File.Copy(TextBox1.Text, ".\Win11.iso")
                            LogBox.AppendText(" 1/2...")
                            File.Copy(TextBox2.Text, ".\Win10.iso")
                            LogBox.AppendText(" 2/2 Done")
                        End If
                    Catch DNFEx2 As DirectoryNotFoundException
                        LogBox.AppendText(" Failed")
                        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                            InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
                        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                            InstSTLabel.Text = "Falló la copia de los archivos ISO al disco local."
                        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                            InstSTLabel.Text = "Échec de la copie des fichiers ISO sur le disque local."
                        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                InstSTLabel.Text = "Falló la copia de los archivos ISO al disco local."
                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                                InstSTLabel.Text = "Échec de la copie des fichiers ISO sur le disque local."
                            End If
                        End If
                        BringToFront()
                        BackSubPanel.Show()
                        VolumeConnectPanel.ShowDialog()
                        VolumeConnectPanel.Visible = True
                        VolumeConnectPanel.Visible = False
                        BringToFront()
                    Catch FNFEx2 As FileNotFoundException
                        LogBox.AppendText(" Failed")
                        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                            InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
                        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                            InstSTLabel.Text = "Falló la copia de los archivos ISO al disco local."
                        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                            InstSTLabel.Text = "Échec de la copie des fichiers ISO sur le disque local."
                        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                InstSTLabel.Text = "Falló la copia de los archivos ISO al disco local."
                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                                InstSTLabel.Text = "Échec de la copie des fichiers ISO sur le disque local."
                            End If
                        End If
                        BringToFront()
                        BackSubPanel.Show()
                        VolumeConnectPanel.ShowDialog()
                        VolumeConnectPanel.Visible = True
                        VolumeConnectPanel.Visible = False
                        BringToFront()
                    End Try
                End If
            End Try
        ElseIf FileCopyPanel.DialogResult = Windows.Forms.DialogResult.No Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                InstSTLabel.Text = "Skipping file copy..."
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                InstSTLabel.Text = "Omitiendo la copia de archivos..."
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                InstSTLabel.Text = "Sauter la copie du fichier..."
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    InstSTLabel.Text = "Skipping file copy..."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    InstSTLabel.Text = "Omitiendo la copia de archivos..."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    InstSTLabel.Text = "Sauter la copie du fichier..."
                End If
            End If
            ' This is done for testing if the source drive is still mounted
            If Directory.Exists(TextBox4.Text) Then
                ' OK
            Else
                BringToFront()
                BackSubPanel.Show()
                VolumeConnectPanel.ShowDialog()
                VolumeConnectPanel.Visible = True
                VolumeConnectPanel.Visible = False
                BringToFront()
            End If
        End If
        InstallerProgressBar.Value = 10
        CheckPic1.Visible = True
        Label84.ForeColor = Color.DimGray
        Label84.Font = New Font("Segoe UI", 9.75)
        If BackColor = Color.FromArgb(243, 243, 243) Then
            Label85.ForeColor = Color.Black
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            Label85.ForeColor = Color.White
        End If
        Label85.Font = New Font("Segoe UI", 9.75, FontStyle.Bold)
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            InstSTLabel.Text = "Gathering instructions..."
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            InstSTLabel.Text = "Recopilando instrucciones..."
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            InstSTLabel.Text = "Rassembler les instructions..."
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                InstSTLabel.Text = "Gathering instructions..."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                InstSTLabel.Text = "Recopilando instrucciones..."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                InstSTLabel.Text = "Rassembler les instructions..."
            End If
        End If
        LogBox.AppendText(CrLf & "Gathering instructions necessary to create the installer...")
        LogBox.AppendText(" Done")
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            InstSTLabel.Text = "Instructions gathered"
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            InstSTLabel.Text = "Instrucciones recopiladas"
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            InstSTLabel.Text = "Instructions recueillies"
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                InstSTLabel.Text = "Instructions gathered"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                InstSTLabel.Text = "Instrucciones recopiladas"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                InstSTLabel.Text = "Instructions recueillies"
            End If
        End If
        InstallerProgressBar.Value = 15
        CheckPic2.Visible = True
        Label85.ForeColor = Color.DimGray
        CheckWimEsdExistence()
        Label85.Font = New Font("Segoe UI", 9.75)
        If BackColor = Color.FromArgb(243, 243, 243) Then
            Label86.ForeColor = Color.Black
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            Label86.ForeColor = Color.White
        End If
        Label86.Font = New Font("Segoe UI", 9.75, FontStyle.Bold)
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            InstSTLabel.Text = "Extracting the necessary files from the ISO images..."
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            InstSTLabel.Text = "Extrayendo los archivos necesarios de las imágenes ISO..."
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            InstSTLabel.Text = "Extraire les fichiers nécessaires des images ISO..."
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                InstSTLabel.Text = "Extracting the necessary files from the ISO images..."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                InstSTLabel.Text = "Extrayendo los archivos necesarios de las imágenes ISO..."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                InstSTLabel.Text = "Extraire les fichiers nécessaires des images ISO..."
            End If
        End If
        LogBox.AppendText(CrLf & "Extracting the necessary contents from the ISO images using the " & ComboBox5.SelectedItem & " method...")
        If ComboBox5.SelectedItem = "WIMR" Then
            If File.Exists(".\Win11.iso") And File.Exists(".\Win10.iso") Then
                If Win11ESD = 1 Then
                    LogBox.AppendText(CrLf & Quote & "There are data after the end of archive" & Quote & " warnings can be safely ignored.")
                    File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11IESDISOEx_Local & CrLf & W10ISOEx_Local, ASCII)
                ElseIf Win11ESD = 0 Then
                    File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11IWIMISOEx_Local & CrLf & W10ISOEx_Local, ASCII)
                End If
            Else
                If Win11ESD = 1 Then
                    LogBox.AppendText(CrLf & Quote & "There are data after the end of archive" & Quote & " warnings can be safely ignored.")
                    File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11IESDISOEx & CrLf & W10ISOEx, ASCII)
                ElseIf Win11ESD = 0 Then
                    File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11IWIMISOEx & CrLf & W10ISOEx, ASCII)
                End If

            End If
            Process.Start(".\temp.bat").WaitForExit()
        ElseIf ComboBox5.SelectedItem = "DLLR" Then
            If File.Exists(".\Win11.iso") And File.Exists(".\Win10.iso") Then
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11ISOEx_Local & CrLf & W10AR_ARRDLLEx_Local & CrLf & W10AR_ARRDLLEx_2_Local, ASCII)
            Else
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11ISOEx & CrLf & W10AR_ARRDLLEx & CrLf & W10AR_ARRDLLEx_2, ASCII)
            End If
            Process.Start(".\temp.bat").WaitForExit()
        ElseIf ComboBox5.SelectedItem = "REGTWEAK" Then
            If File.Exists(".\Win11.iso") Then
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11ISOEx_Local, ASCII)
            Else
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11ISOEx, ASCII)
            End If
            Process.Start(".\temp.bat").WaitForExit()
        End If
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            InstSTLabel.Text = "Necessary files extracted"
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            InstSTLabel.Text = "Archivos necesarios extraídos"
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            InstSTLabel.Text = "Fichiers nécessaires extraits"
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                InstSTLabel.Text = "Necessary files extracted"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                InstSTLabel.Text = "Archivos necesarios extraídos"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                InstSTLabel.Text = "Fichiers nécessaires extraits"
            End If
        End If
        LogBox.AppendText(CrLf & "Finished extracting files.")
        InstallerProgressBar.Value = 25
        CheckPic3.Visible = True
        Label86.Font = New Font("Segoe UI", 9.75)
        Label86.ForeColor = Color.DimGray
        If BackColor = Color.FromArgb(243, 243, 243) Then
            Label87.ForeColor = Color.Black
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            Label87.ForeColor = Color.White
        End If
        Label87.Font = New Font("Segoe UI", 9.75, FontStyle.Bold)
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            InstSTLabel.Text = "Creating the custom installer..."
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            InstSTLabel.Text = "Creando el instalador personalizado..."
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            InstSTLabel.Text = "Création de l'installateur personnalisé..."
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                InstSTLabel.Text = "Creating the custom installer..."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                InstSTLabel.Text = "Creando el instalador personalizado..."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                InstSTLabel.Text = "Création de l'installateur personnalisé..."
            End If
        End If
        LogBox.AppendText(CrLf & "Creating the custom installer using the " & ComboBox5.SelectedItem & " method...")
        If ComboBox5.SelectedItem = "WIMR" Then
            If File.Exists(".\temp\sources\install.wim") Then
                LogBox.AppendText(CrLf & "Deleting " & Quote & "install.wim" & Quote & " from the Windows 10 installation media...")
                File.Delete(".\temp\sources\install.wim")
            ElseIf File.Exists(".\temp\sources\install.esd") Then
                LogBox.AppendText(CrLf & "Deleting " & Quote & "install.esd" & Quote & " from the Windows 10 installation media...")
                File.Delete(".\temp\sources\install.esd")
            End If
            LogBox.AppendText(" Done")
            If Win11ESD = 1 Then
                LogBox.AppendText(CrLf & "Moving " & Quote & "install.esd" & Quote & " from the Windows 11 installation media to the Windows 10 installer...")
                File.Move(".\install.esd", ".\temp\sources\install.esd")
            ElseIf Win11ESD = 0 Then
                LogBox.AppendText(CrLf & "Moving " & Quote & "install.wim" & Quote & " from the Windows 11 installation media to the Windows 10 installer...")
                File.Move(".\install.wim", ".\temp\sources\install.wim")
            End If
            LogBox.AppendText(" Done")
            InstallerProgressBar.Value = 50
        ElseIf ComboBox5.SelectedItem = "DLLR" Then
            LogBox.AppendText(CrLf & "Deleting " & Quote & "appraiser.dll" & Quote & " and " & Quote & "appraiserres.dll" & Quote & " from the Windows 11 installation media...")
            File.Delete(".\temp\sources\appraiser.dll")
            LogBox.AppendText(" 1/2...")
            File.Delete(".\temp\sources\appraiserres.dll")
            LogBox.AppendText(" 2/2 Done")
            LogBox.AppendText(CrLf & "Moving " & Quote & "appraiser.dll" & Quote & " and " & Quote & "appraiserres.dll" & Quote & " from the Windows 10 installation media to the Windows 11 installer...")
            File.Move(".\appraiser.dll", ".\temp\sources\appraiser.dll")
            LogBox.AppendText(" 1/2...")
            File.Move(".\appraiserres.dll", ".\temp\sources\appraiserres.dll")
            LogBox.AppendText(" 2/2 Done")
            InstallerProgressBar.Value = 50
        ElseIf ComboBox5.SelectedItem = "REGTWEAK" Then
            LogBox.AppendText(CrLf & "Moving " & Quote & "boot.wim" & Quote & " from the Windows 11 installation media...")
            File.Move(".\temp\sources\boot.wim", ".\boot.wim")
            LogBox.AppendText(" Done" & CrLf & "Creating the WIM file mount point folder...")
            Directory.CreateDirectory(".\wimmount")
            LogBox.AppendText(" Done" & CrLf & "Launching the REGTWEAK script...")
            If AdvancedOptionsPanel.CheckBox1.Checked = True Then   ' Prepare boot.wim (and install.wim, if necessary) for surgery
                If AdvancedOptionsPanel.CheckBox2.Checked = True Then
                    If Win11ESD = 1 Then
                        LogBox.AppendText(CrLf & "WARNING: the program has detected ESD files on the Windows 11 image. Proceeding with normal options...")
                        WarnCount += 1
                        WarningText.AppendText(Now & " - The program attempted to run the REGTWEAK script with advanced options, but one of the source images contains ESD files. These cannot be mounted by DISM")
                        AdvancedOptionsPanel.CheckBox1.Checked = False
                        AdvancedOptionsPanel.CheckBox2.Checked = False
                        Process.Start(".\prog_bin\regtweak.bat").WaitForExit()
                    ElseIf Win11ESD = 0 Then
                        LogBox.AppendText(" Additional flags: /bypassnro; /sv2")
                        Process.Start(".\prog_bin\regtweak.bat", "/bypassnro /sv2").WaitForExit()
                    End If
                Else
                    If Win11ESD = 1 Then
                        LogBox.AppendText(CrLf & "WARNING: the program has detected ESD files on the Windows 11 image. Proceeding with normal options...")
                        WarnCount += 1
                        WarningText.AppendText(Now & " - The program attempted to run the REGTWEAK script with advanced options, but one of the source images contains ESD files. These cannot be mounted by DISM")
                        AdvancedOptionsPanel.CheckBox1.Checked = False
                        Process.Start(".\prog_bin\regtweak.bat").WaitForExit()
                    ElseIf Win11ESD = 0 Then
                        LogBox.AppendText(" Additional flags: /bypassnro")
                        Process.Start(".\prog_bin\regtweak.bat", "/bypassnro").WaitForExit()
                    End If
                End If
            Else
                If AdvancedOptionsPanel.CheckBox2.Checked = True Then
                    If Win11ESD = 1 Then
                        LogBox.AppendText(CrLf & "WARNING: the program has detected ESD files on the Windows 11 image. Proceeding with normal options...")
                        WarnCount += 1
                        WarningText.AppendText(Now & " - The program attempted to run the REGTWEAK script with advanced options, but one of the source images contains ESD files. These cannot be mounted by DISM")
                        AdvancedOptionsPanel.CheckBox2.Checked = False
                        Process.Start(".\prog_bin\regtweak.bat").WaitForExit()
                    ElseIf Win11ESD = 0 Then
                        LogBox.AppendText(" Additional flags: /sv2")
                        Process.Start(".\prog_bin\regtweak.bat", "/sv2").WaitForExit()
                    End If
                Else
                    Process.Start(".\prog_bin\regtweak.bat").WaitForExit()
                End If
            End If
            LogBox.AppendText(CrLf & "Finished running the REGTWEAK script." & CrLf & "Moving " & Quote & "boot.wim" & Quote & " to the Windows 11 installer...")
            File.Move(".\boot.wim", ".\temp\sources\boot.wim")      ' Move boot.wim after operations done
            LogBox.AppendText(" Done")
            InstallerProgressBar.Value = 50
        End If
        LogBox.AppendText(CrLf & "Creating the installer using OSCDIMG...")
        If RadioButton1.Checked = True Then
            File.WriteAllText(".\temp.bat", OffEcho & CrLf & OSCDIMG_CSM, ASCII)
            Process.Start(".\temp.bat").WaitForExit()
        Else
            If File.Exists(".\temp\boot\Efisys.bin") Then
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & OSCDIMG_UEFI, ASCII)
            Else
                LogBox.AppendText(CrLf & "WARNING: file: " & Quote & "\boot\Efisys.bin" & Quote & " is not present in the temporary installer. Using the fallback BIOS/UEFI-CSM method...")
                WarnCount += 1
                If WarningText.Text = "" Then
                    WarningText.AppendText(Now & " - To make installers compatible with modern systems (UEFI), the program needs " & Quote & "\boot\Efisys.bin" & Quote & ", which it could not find")
                Else
                    WarningText.AppendText(CrLf & Now & " - To make installers compatible with modern systems (UEFI), the program needs " & Quote & "\boot\Efisys.bin" & Quote & ", which it could not find")
                End If
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & OSCDIMG_CSM, ASCII)
            End If
            Process.Start(".\temp.bat").WaitForExit()
        End If
        LogBox.AppendText(" Done")
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            InstSTLabel.Text = "Finished creating the installer. Now deleting temporary files..."
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            InstSTLabel.Text = "Se finalizó la creación del instalador. Ahora se están eliminando los archivos temporales..."
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            InstSTLabel.Text = "A fini la création de l'installateur. Maintenant, supprimer les fichiers temporaires..."
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                InstSTLabel.Text = "Finished creating the installer. Now deleting temporary files..."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                InstSTLabel.Text = "Se finalizó la creación del instalador. Ahora se están eliminando los archivos temporales..."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                InstSTLabel.Text = "A fini la création de l'installateur. Maintenant, supprimer les fichiers temporaires..."
            End If
        End If
        InstallerProgressBar.Value = 75
        CheckPic4.Visible = True
        Label87.ForeColor = Color.DimGray
        Label87.Font = New Font("Segoe UI", 9.75)
        LogBox.AppendText(CrLf & "Gathering file count...")
        If BackColor = Color.FromArgb(243, 243, 243) Then
            Label88.ForeColor = Color.Black
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            Label88.ForeColor = Color.White
        End If
        Label88.Font = New Font("Segoe UI", 9.75, FontStyle.Bold)
        For Each fileGather In My.Computer.FileSystem.GetFiles(".\temp", FileIO.SearchOption.SearchAllSubDirectories)
            FileCount += 1
        Next
        For Each deletedFile In My.Computer.FileSystem.GetFiles(".\temp", FileIO.SearchOption.SearchAllSubDirectories)
            DelFileCount += 1
            MessageCount += 1
            Try
                LogBox.AppendText(CrLf & "Deleted file: " & DelFileCount & "/" & FileCount)
                File.Delete(deletedFile)
            Catch PTLEx As PathTooLongException
                EmergencyFileDeleteCount += 1
                WarnCount += 1
                If WarningText.Text = "" Then
                    WarningText.AppendText(Now & " - An exception ocurred while deleting files. The program has attempted an alternative file deletion method")
                Else
                    WarningText.AppendText(CrLf & Now & " - An exception ocurred while deleting files. The program has attempted an alternative file deletion method")
                End If
                File.WriteAllText(".\emergencydelete.bat", OffEcho & "del " & Quote & deletedFile & Quote & " /f /q", ASCII)
                Process.Start(".\emergencydelete.bat").WaitForExit()
                File.Delete(".\emergencydelete.bat")
                LogBox.AppendText(CrLf & "Deleted file: " & DelFileCount & "/" & FileCount & ". Files that had to be deleted manually: " & EmergencyFileDeleteCount)
            Catch DNFDelEx As DirectoryNotFoundException
                WarnCount += 1
                If WarningText.Text = "" Then
                    WarningText.AppendText(Now & " - An exception ocurred while deleting files. The program has attempted an alternative file deletion method")
                Else
                    WarningText.AppendText(CrLf & Now & " - An exception ocurred while deleting files. The program has attempted an alternative file deletion method")
                End If
                EmergencyFileDeleteCount += 1
                File.WriteAllText(".\emergencydelete.bat", OffEcho & "del " & Quote & deletedFile & Quote & " /f /q", ASCII)
                Process.Start(".\emergencydelete.bat").WaitForExit()
                File.Delete(".\emergencydelete.bat")
                LogBox.AppendText(CrLf & "Deleted file: " & DelFileCount & "/" & FileCount & ". Files that had to be deleted manually: " & EmergencyFileDeleteCount)
            End Try
        Next
        LogBox.AppendText(CrLf & "Temporary folder cleaned. Deleting it...")
        Try
            Directory.Delete(".\temp")
        Catch IOEx As IOException
            LogBox.AppendText(CrLf & "Exception: 'IOException' caught at runtime, performing emergency method...")
            WarnCount += 1
            If WarningText.Text = "" Then
                WarningText.AppendText(Now & " - An exception ocurred while deleting the temporary directory. The program has attempted an alternative folder deletion method")
            Else
                WarningText.AppendText(CrLf & Now & " - An exception ocurred while deleting the temporary directory. The program has attempted an alternative folder deletion method")
            End If
            File.WriteAllText(".\temp.bat", OffEcho & CrLf & EmergencyFolderDelete, ASCII)
            Process.Start(".\temp.bat").WaitForExit()
        End Try
        LogBox.AppendText(" Done")
        If File.Exists(".\Win11.iso") And File.Exists(".\Win10.iso") Then
            LogBox.AppendText(CrLf & "Deleting previously copied installers from the local disk...")
            File.Delete(".\Win11.iso")
            LogBox.AppendText(" 1/2...")
            File.Delete(".\Win10.iso")
            LogBox.AppendText(" 2/2 Done")
        End If
        If Directory.Exists(".\wimmount") Then
            Directory.Delete(".\wimmount")
        End If
        LogBox.AppendText(CrLf & "Deleting temporarily created scripts...")
        File.Delete(".\temp.bat")
        LogBox.AppendText(" Done")
        InstallerProgressBar.Value = 100
        EnInstCreateTime = Now
        LogBox.AppendText(CrLf & "Finished creating the installer. Details:" & CrLf & "Began installer creation at: " & StInstCreateTime & CrLf & "Ended installer creation at: " & EnInstCreateTime & CrLf & "Message(s): " & MessageCount & ". Warning(s): " & WarnCount & ". Error(s): " & ErrorCount & ".")
        If File.Exists(".\inst.log") Then
            If File.Exists(".\log.txt") Then
                BringToFront()
                BackSubPanel.Show()
                LogMigratePanel.ShowDialog()
                LogMigratePanel.Visible = True
                LogMigratePanel.Visible = False
                BringToFront()
                If LogMigratePanel.DialogResult = Windows.Forms.DialogResult.Yes Then
                    If File.Exists(".\inst.log") Then
                        File.Move(".\inst.log", ".\inst.log.bak")
                        File.Move(".\log.txt", ".\inst.log")
                        logOpen.TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\inst.log.bak")
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & "----------------------------------------------------------------" & CrLf & logOpen.TextBox1.Text, True)
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf, True)
                        My.Computer.FileSystem.WriteAllText(".\inst.log", "----------------------------------------------------------------", True)
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf, True)
                        My.Computer.FileSystem.WriteAllText(".\inst.log", LogBox.Text, True)
                        If WarningText.Text = "" Then
                            My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & "Warnings: none", True)
                        Else
                            My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & "Warnings: " & CrLf & WarningText.Text, True)
                        End If
                        If ErrorText.Text = "" Then
                            My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & CrLf & "Errors: none", True)
                        Else
                            My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & CrLf & "Errors: " & CrLf & ErrorText.Text, True)
                        End If
                        File.Delete(".\inst.log.bak")
                    Else
                        File.Move(".\log.txt", ".\inst.log")
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf, True)
                        My.Computer.FileSystem.WriteAllText(".\inst.log", "----------------------------------------------------------------", True)
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf, True)
                        My.Computer.FileSystem.WriteAllText(".\inst.log", LogBox.Text, True)
                        If WarningText.Text = "" Then
                            My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & "Warnings: none", True)
                        Else
                            My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & "Warnings: " & CrLf & WarningText.Text, True)
                        End If
                        If ErrorText.Text = "" Then
                            My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & CrLf & "Errors: none", True)
                        Else
                            My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & CrLf & "Errors: " & CrLf & ErrorText.Text, True)
                        End If
                    End If
                ElseIf LogMigratePanel.DialogResult = Windows.Forms.DialogResult.No Then
                    My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf, True)
                    My.Computer.FileSystem.WriteAllText(".\inst.log", "----------------------------------------------------------------", True)
                    My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf, True)
                    My.Computer.FileSystem.WriteAllText(".\inst.log", LogBox.Text, True)
                    If WarningText.Text = "" Then
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & "Warnings: none", True)
                    Else
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & "Warnings: " & CrLf & WarningText.Text, True)
                    End If
                    If ErrorText.Text = "" Then
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & CrLf & "Errors: none", True)
                    Else
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & CrLf & "Errors: " & CrLf & ErrorText.Text, True)
                    End If
                End If
            Else
                BringToFront()
                BackSubPanel.Show()
                LogExistsPanel.ShowDialog()
                LogExistsPanel.Visible = True
                LogExistsPanel.Visible = False
                BringToFront()
                If LogExistsPanel.DialogResult = Windows.Forms.DialogResult.Yes Then
                    My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf, True)
                    My.Computer.FileSystem.WriteAllText(".\inst.log", "----------------------------------------------------------------", True)
                    My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf, True)
                    My.Computer.FileSystem.WriteAllText(".\inst.log", LogBox.Text, True)
                    If WarningText.Text = "" Then
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & "Warnings: none", True)
                    Else
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & "Warnings: " & CrLf & WarningText.Text, True)
                    End If
                    If ErrorText.Text = "" Then
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & CrLf & "Errors: none", True)
                    Else
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & CrLf & "Errors: " & CrLf & ErrorText.Text, True)
                    End If
                Else
                    File.Delete(".\inst.log")
                    My.Computer.FileSystem.WriteAllText(".\inst.log", LogBox.Text, True)
                    If WarningText.Text = "" Then
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & "Warnings: none", True)
                    Else
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & "Warnings: " & CrLf & WarningText.Text, True)
                    End If
                    If ErrorText.Text = "" Then
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & CrLf & "Errors: none", True)
                    Else
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & CrLf & "Errors: " & CrLf & ErrorText.Text, True)
                    End If
                End If
            End If
        Else
            My.Computer.FileSystem.WriteAllText(".\inst.log", LogBox.Text, True)
            If WarningText.Text = "" Then
                My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & "Warnings: none", True)
            Else
                My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & "Warnings: " & CrLf & WarningText.Text, True)
            End If
            If ErrorText.Text = "" Then
                My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & CrLf & "Errors: none", True)
            Else
                My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf & CrLf & "Errors: " & CrLf & ErrorText.Text, True)
            End If
        End If
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            Notify.ShowBalloonTip(5, "Finished creating the custom installer", "Please read the details in the main window", ToolTipIcon.Info)
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            Notify.ShowBalloonTip(5, "Se terminó la creación del instalador", "Por favor, lea los detalles en la ventana principal", ToolTipIcon.Info)
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            Notify.ShowBalloonTip(5, "A fini la création de l'installateur personnalisé.", "Veuillez lire les détails dans la fenêtre principale", ToolTipIcon.Info)
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Notify.ShowBalloonTip(5, "Finished creating the custom installer", "Please read the details in the main window", ToolTipIcon.Info)
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Notify.ShowBalloonTip(5, "Se terminó la creación del instalador", "Por favor, lea los detalles en la ventana principal", ToolTipIcon.Info)
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Notify.ShowBalloonTip(5, "A fini la création de l'installateur personnalisé.", "Veuillez lire les détails dans la fenêtre principale", ToolTipIcon.Info)
            End If
        End If
        CheckPic5.Visible = True
        InstCreateInt = 3
        Label88.Font = New Font("Segoe UI", 9.75)
        Label88.ForeColor = Color.DimGray
        My.Computer.Audio.Play(My.Resources.Win11, AudioPlayMode.Background)
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            Label82.Text = "Finish"
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            Label82.Text = "Final"
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            Label82.Text = "Fin"
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label82.Text = "Finish"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label82.Text = "Final"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label82.Text = "Fin"
            End If
        End If
        CompPic.Visible = True
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            Label83.Text = "The custom installer was created at the specified location. Please read the details below."
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            Label83.Text = "El instalador modificado fue creado en la ubicación especificada. Por favor, lea los detalles abajo."
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            Label83.Text = "L'installateur personnalisé a été créé à l'emplacement spécifié. Veuillez lire les détails ci-dessous."
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label83.Text = "The custom installer was created at the specified location. Please read the details below."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label83.Text = "El instalador modificado fue creado en la ubicación especificada. Por favor, lea los detalles abajo."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label83.Text = "L'installateur personnalisé a été créé à l'emplacement spécifié. Veuillez lire les détails ci-dessous."
            End If
        End If
        Label112.Text = TextBox3.Text & ".iso"
        TargetInstallerLinkLabel.Text = TextBox4.Text
        Label113.Text = EnInstCreateTime
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            Label114.Text = "Warnings: " & WarnCount
            Label115.Text = "Errors: " & ErrorCount
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            Label114.Text = "Advertencias: " & WarnCount
            Label115.Text = "Errores: " & ErrorCount
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            Label114.Text = "Avertissements : " & WarnCount
            Label115.Text = "Erreurs : " & ErrorCount
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label114.Text = "Warnings: " & WarnCount
                Label115.Text = "Errors: " & ErrorCount
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label114.Text = "Advertencias: " & WarnCount
                Label115.Text = "Errores: " & ErrorCount
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label114.Text = "Avertissements : " & WarnCount
                Label115.Text = "Erreurs : " & ErrorCount
            End If
        End If
        TableLayoutPanel3.Visible = True
        CheckPic1.Visible = False
        CheckPic2.Visible = False
        CheckPic3.Visible = False
        CheckPic4.Visible = False
        CheckPic5.Visible = False
        Label84.Visible = False
        Label85.Visible = False
        Label86.Visible = False
        Label87.Visible = False
        Label88.Visible = False
        GroupBox10.Visible = False
        InstSTLabel.Visible = False
        InstallerProgressBar.Visible = False
        Label89.Visible = False
        CheckBox4.Visible = True
        Button9.Visible = False
        LogViewLink.Visible = True
        InstallerCreationMethodToolStripMenuItem.Enabled = True
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            Button10.Text = "OK"
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            Button10.Text = "Aceptar"
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            Button10.Text = "OK"
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Button10.Text = "OK"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Button10.Text = "Aceptar"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Button10.Text = "OK"
            End If
        End If
        If TextBox4.Text.EndsWith("\") Then
            InstHistPanel.InstallerListView.Items.Add(TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso").SubItems.Add(EnInstCreateTime)
        Else
            InstHistPanel.InstallerListView.Items.Add(TextBox4.Text & "\" & TextBox3.Text & ".iso").SubItems.Add(EnInstCreateTime)
        End If
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            InstHistPanel.InstallerEntryLabel.Text = "Installer history entries: " & InstHistPanel.InstallerListView.Items.Count
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            InstHistPanel.InstallerEntryLabel.Text = "Entradas en el historial del instalador: " & InstHistPanel.InstallerListView.Items.Count
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            InstHistPanel.InstallerEntryLabel.Text = "Entrées de l'historique de l'installateur : " & InstHistPanel.InstallerListView.Items.Count
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                InstHistPanel.InstallerEntryLabel.Text = "Installer history entries: " & InstHistPanel.InstallerListView.Items.Count
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                InstHistPanel.InstallerEntryLabel.Text = "Entradas en el historial del instalador: " & InstHistPanel.InstallerListView.Items.Count
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                InstHistPanel.InstallerEntryLabel.Text = "Entrées de l'historique de l'installateur : " & InstHistPanel.InstallerListView.Items.Count
            End If
        End If
        PictureBox5.Visible = False
        Label3.Visible = False
        LinkLabel2.Visible = False
    End Sub

    Private Sub Label61_Click(sender As Object, e As EventArgs) Handles Label61.Click
        InstCreateInt = 0
        SettingReviewPanel.Visible = False
        InstCreatePanel.Visible = True
        DisableBackPic()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        InstCreateInt = 0
        SettingReviewPanel.Visible = False
        InstCreatePanel.Visible = True
        DisableBackPic()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If Button9.Text = "Hide log" Then
            Button9.Text = "Show log"
            GroupBox10.Visible = False
            Button9.Top = GroupBox10.Top + GroupBox10.Height
        ElseIf Button9.Text = "Show log" Then
            Button9.Text = "Hide log"
            GroupBox10.Visible = True
            Button9.Top = GroupBox10.Top - 4
        End If
    End Sub

    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        If My.Computer.Info.OSFullName.Contains("Windows 11") Or My.Computer.Info.OSFullName.Contains("Windows 10") Then
            Process.Start("ms-settings:about")
        Else
            Process.Start("sysdm.cpl")
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If TextBox4.Text.EndsWith("\") Then
            If TextBox4.Text.Contains(" ") Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                    End If
                End If
            Else
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                    End If
                End If
            End If
        Else
            If TextBox4.Text.Contains(" ") Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                    End If
                End If
            Else
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                    ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                    End If
                End If
            End If
        End If
        If TextBox4.Text = "" Then
            Button6.Enabled = False
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                MsgBox("The path cannot be nothing. The program will instead use the user folder to store the target installer.", vbOKOnly + vbInformation, "Target installer path")
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                MsgBox("La ruta no puede ser nada. El programa utilizará la carpeta de usuario para almacenar el instalador de destino", vbOKOnly + vbInformation, "Ruta del instalador de destino")
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                MsgBox("Le chemin ne peut pas être rien. Le programme utilisera plutôt le dossier de l'utilisateur pour enregistrer le programme d'installation cible.", vbOKOnly + vbInformation, "Chemin de l'installateur cible")
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    MsgBox("The path cannot be nothing. The program will instead use the user folder to store the target installer.", vbOKOnly + vbInformation, "Target installer path")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    MsgBox("La ruta no puede ser nada. El programa utilizará la carpeta de usuario para almacenar el instalador de destino", vbOKOnly + vbInformation, "Ruta del instalador de destino")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    MsgBox("Le chemin ne peut pas être rien. Le programme utilisera plutôt le dossier de l'utilisateur pour enregistrer le programme d'installation cible.", vbOKOnly + vbInformation, "Chemin de l'installateur cible")
                End If
            End If
            If DialogResult.OK Then
                TextBox4.Text = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            End If
        Else
            Button6.Enabled = True
        End If
        If TextBox4.Text.EndsWith("con") Or _
            TextBox4.Text.EndsWith("CON") Or _
            TextBox4.Text.EndsWith("aux") Or _
            TextBox4.Text.EndsWith("AUX") Or _
            TextBox4.Text.EndsWith("prn") Or _
            TextBox4.Text.EndsWith("PRN") Or _
            TextBox4.Text.EndsWith("nul") Or _
            TextBox4.Text.EndsWith("NUL") Or _
            TextBox4.Text.EndsWith("com1") Or _
            TextBox4.Text.EndsWith("com2") Or _
            TextBox4.Text.EndsWith("com3") Or _
            TextBox4.Text.EndsWith("com4") Or _
            TextBox4.Text.EndsWith("com5") Or _
            TextBox4.Text.EndsWith("com6") Or _
            TextBox4.Text.EndsWith("com7") Or _
            TextBox4.Text.EndsWith("com8") Or _
            TextBox4.Text.EndsWith("com9") Or _
            TextBox4.Text.EndsWith("COM1") Or _
            TextBox4.Text.EndsWith("COM2") Or _
            TextBox4.Text.EndsWith("COM3") Or _
            TextBox4.Text.EndsWith("COM4") Or _
            TextBox4.Text.EndsWith("COM5") Or _
            TextBox4.Text.EndsWith("COM6") Or _
            TextBox4.Text.EndsWith("COM7") Or _
            TextBox4.Text.EndsWith("COM8") Or _
            TextBox4.Text.EndsWith("COM9") Or _
            TextBox4.Text.EndsWith("lpt1") Or _
            TextBox4.Text.EndsWith("lpt2") Or _
            TextBox4.Text.EndsWith("lpt3") Or _
            TextBox4.Text.EndsWith("lpt4") Or _
            TextBox4.Text.EndsWith("lpt5") Or _
            TextBox4.Text.EndsWith("lpt6") Or _
            TextBox4.Text.EndsWith("lpt7") Or _
            TextBox4.Text.EndsWith("lpt8") Or _
            TextBox4.Text.EndsWith("lpt9") Or _
            TextBox4.Text.EndsWith("LPT1") Or _
            TextBox4.Text.EndsWith("LPT2") Or _
            TextBox4.Text.EndsWith("LPT3") Or _
            TextBox4.Text.EndsWith("LPT4") Or _
            TextBox4.Text.EndsWith("LPT5") Or _
            TextBox4.Text.EndsWith("LPT6") Or _
            TextBox4.Text.EndsWith("LPT7") Or _
            TextBox4.Text.EndsWith("LPT8") Or _
            TextBox4.Text.EndsWith("LPT9") Or _
            TextBox4.Text.EndsWith("<") Or _
            TextBox4.Text.EndsWith(">") Or _
            TextBox4.Text.EndsWith(Quote) Or _
            TextBox4.Text.EndsWith("|") Or _
            TextBox4.Text.EndsWith("?") Or _
            TextBox4.Text.EndsWith("*") Or _
            TextBox4.Text.EndsWith("con\") Or _
            TextBox4.Text.EndsWith("CON\") Or _
            TextBox4.Text.EndsWith("aux\") Or _
            TextBox4.Text.EndsWith("AUX\") Or _
            TextBox4.Text.EndsWith("prn\") Or _
            TextBox4.Text.EndsWith("PRN\") Or _
            TextBox4.Text.EndsWith("nul\") Or _
            TextBox4.Text.EndsWith("NUL\") Or _
            TextBox4.Text.EndsWith("com1\") Or _
            TextBox4.Text.EndsWith("com2\") Or _
            TextBox4.Text.EndsWith("com3\") Or _
            TextBox4.Text.EndsWith("com4\") Or _
            TextBox4.Text.EndsWith("com5\") Or _
            TextBox4.Text.EndsWith("com6\") Or _
            TextBox4.Text.EndsWith("com7\") Or _
            TextBox4.Text.EndsWith("com8\") Or _
            TextBox4.Text.EndsWith("com9\") Or _
            TextBox4.Text.EndsWith("COM1\") Or _
            TextBox4.Text.EndsWith("COM2\") Or _
            TextBox4.Text.EndsWith("COM3\") Or _
            TextBox4.Text.EndsWith("COM4\") Or _
            TextBox4.Text.EndsWith("COM5\") Or _
            TextBox4.Text.EndsWith("COM6\") Or _
            TextBox4.Text.EndsWith("COM7\") Or _
            TextBox4.Text.EndsWith("COM8\") Or _
            TextBox4.Text.EndsWith("COM9\") Or _
            TextBox4.Text.EndsWith("lpt1\") Or _
            TextBox4.Text.EndsWith("lpt2\") Or _
            TextBox4.Text.EndsWith("lpt3\") Or _
            TextBox4.Text.EndsWith("lpt4\") Or _
            TextBox4.Text.EndsWith("lpt5\") Or _
            TextBox4.Text.EndsWith("lpt6\") Or _
            TextBox4.Text.EndsWith("lpt7\") Or _
            TextBox4.Text.EndsWith("lpt8\") Or _
            TextBox4.Text.EndsWith("lpt9\") Or _
            TextBox4.Text.EndsWith("LPT1\") Or _
            TextBox4.Text.EndsWith("LPT2\") Or _
            TextBox4.Text.EndsWith("LPT3\") Or _
            TextBox4.Text.EndsWith("LPT4\") Or _
            TextBox4.Text.EndsWith("LPT5\") Or _
            TextBox4.Text.EndsWith("LPT6\") Or _
            TextBox4.Text.EndsWith("LPT7\") Or _
            TextBox4.Text.EndsWith("LPT8\") Or _
            TextBox4.Text.EndsWith("LPT9\") Or _
            TextBox4.Text.EndsWith("<\") Or _
            TextBox4.Text.EndsWith(">\") Or _
            TextBox4.Text.EndsWith(Quote & "\") Or _
            TextBox4.Text.EndsWith("|\") Or _
            TextBox4.Text.EndsWith("?\") Or _
            TextBox4.Text.EndsWith("*\") Then   ' This is to prevent installer creation issues. If the installer path ends with "con" for example, or the absolute end ("con\"), then the program will block it. This is BY NO MEANS a pretty solution, but it is the most effective
            TextBox4.ForeColor = Color.Crimson
            Button6.Enabled = False
        Else
            TextBox4.ForeColor = ForeColor
            Button6.Enabled = True
        End If
        If TextBox4.ForeColor = Color.Crimson Then
            Label60.Visible = False
            LinkLabel12.Visible = True
        Else
            If TextBox2.ForeColor = Color.Crimson Or TextBox3.ForeColor = Color.Crimson Or TextBox1.ForeColor = Color.Crimson Then
                Label60.Visible = False
                LinkLabel12.Visible = True
            Else
                Label60.Visible = True
                LinkLabel12.Visible = False
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ImageFolderBrowser.ShowDialog()
        If DialogResult.OK Then
            TextBox4.Text = ImageFolderBrowser.SelectedPath
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If Button10.Text = "Cancel" Or Button10.Text = "Cancelar" Then

        ElseIf Button10.Text = "OK" Or Button10.Text = "Aceptar" Then
            If CheckBox4.Checked = True Then
                ' Leave the program
                Notify.Visible = False
                SaveSettingsFile()
                End
            ElseIf CheckBox4.Checked = False Then
                InstCreateInt = 0
                ProgressPanel.Visible = False
                InstCreatePanel.Visible = True
            End If
        End If
    End Sub

    Private Sub GroupBox8_MouseHover(sender As Object, e As EventArgs) Handles GroupBox8.MouseHover
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            ConglomerateToolTip.SetToolTip(GroupBox8, "These are the images used to create the custom installer")
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            ConglomerateToolTip.SetToolTip(GroupBox8, "Éstas son las imágenes utilizadas para crear el instalador modificado")
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            ConglomerateToolTip.SetToolTip(GroupBox8, "Voici les images utilisées pour créer l'installateur personnalisé")
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                ConglomerateToolTip.SetToolTip(GroupBox8, "These are the images used to create the custom installer")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                ConglomerateToolTip.SetToolTip(GroupBox8, "Éstas son las imágenes utilizadas para crear el instalador modificado")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                ConglomerateToolTip.SetToolTip(GroupBox8, "Voici les images utilisées pour créer l'installateur personnalisé")
            End If
        End If
    End Sub

    Private Sub GroupBox9_MouseHover(sender As Object, e As EventArgs) Handles GroupBox9.MouseHover
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            ConglomerateToolTip.SetToolTip(GroupBox9, "These are the settings used to create the custom installer. To change those, click " & Quote & "No" & Quote & ", and go to Settings (by clicking the gear) > Functionality")
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            ConglomerateToolTip.SetToolTip(GroupBox9, "Éstas son las configuraciones empleadas para crear el instalador modificado. Para cambiarlas, haga clic en " & Quote & "No" & Quote & ", y ve a Configuración (haciendo clic en la tuerca) > Funcionalidad")
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            ConglomerateToolTip.SetToolTip(GroupBox9, "Il s'agit des paramètres utilisés pour créer le programme d'installation personnalisé. Pour les modifier, cliquez sur " & Quote & "Non" & Quote & ", puis allez dans Paramètres (en cliquant sur l'engrenage) > Fonctionnalité")
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                ConglomerateToolTip.SetToolTip(GroupBox9, "These are the settings used to create the custom installer. To change those, click " & Quote & "No" & Quote & ", and go to Settings (by clicking the gear) > Functionality")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                ConglomerateToolTip.SetToolTip(GroupBox9, "Éstas son las configuraciones empleadas para crear el instalador modificado. Para cambiarlas, haga clic en " & Quote & "No" & Quote & ", y ve a Configuración (haciendo clic en la tuerca) > Funcionalidad")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                ConglomerateToolTip.SetToolTip(GroupBox9, "Il s'agit des paramètres utilisés pour créer le programme d'installation personnalisé. Pour les modifier, cliquez sur " & Quote & "Non" & Quote & ", puis allez dans Paramètres (en cliquant sur l'engrenage) > Fonctionnalité")
            End If
        End If
    End Sub

    Private Sub GroupBox11_MouseHover(sender As Object, e As EventArgs) Handles GroupBox11.MouseHover
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            ConglomerateToolTip.SetToolTip(GroupBox11, "This is where the custom installer will be saved. To change this setting, click " & Quote & "No" & Quote & ", and select a different path and name")
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            ConglomerateToolTip.SetToolTip(GroupBox11, "En esta ubicación se guardará el instalador modificado. Para cambiar esta opción, haga clic en " & Quote & "No" & Quote & ", y especifique una ruta y nombre distintos")
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            ConglomerateToolTip.SetToolTip(GroupBox11, "C'est l'endroit où l'installateur personnalisé sera enregistré. Pour modifier ce paramètre, cliquez sur " & Quote & "Non" & Quote & ", puis sélectionnez un chemin et un nom différents")
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                ConglomerateToolTip.SetToolTip(GroupBox11, "This is where the custom installer will be saved. To change this setting, click " & Quote & "No" & Quote & ", and select a different path and name")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                ConglomerateToolTip.SetToolTip(GroupBox11, "En esta ubicación se guardará el instalador modificado. Para cambiar esta opción, haga clic en " & Quote & "No" & Quote & ", y especifique una ruta y nombre distintos")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                ConglomerateToolTip.SetToolTip(GroupBox11, "C'est l'endroit où l'installateur personnalisé sera enregistré. Pour modifier ce paramètre, cliquez sur " & Quote & "Non" & Quote & ", puis sélectionnez un chemin et un nom différents")
            End If
        End If
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        Label70.Text = ComboBox5.SelectedItem
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            Label33.Text = "Installer creation method: " & ComboBox5.SelectedItem
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            Label33.Text = "Método de creación del instalador: " & ComboBox5.SelectedItem
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            Label33.Text = "Méthode de création de l'installateur : " & ComboBox5.SelectedItem
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label33.Text = "Installer creation method: " & ComboBox5.SelectedItem
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label33.Text = "Método de creación del instalador: " & ComboBox5.SelectedItem
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label33.Text = "Méthode de création de l'installateur : " & ComboBox5.SelectedItem
            End If
        End If

        ' This condition will set values for the Context Menu Strip (CMS) on the system tray icon
        If ComboBox5.SelectedItem = "WIMR" Then
            WIMRToolStripMenuItem.Checked = True
            DLLRToolStripMenuItem.Checked = False
            REGTWEAKToolStripMenuItem.Checked = False
            If File.Exists(TextBox1.Text) And File.Exists(TextBox2.Text) Then
                Button6.Enabled = True
            Else
                Button6.Enabled = False
            End If
        ElseIf ComboBox5.SelectedItem = "DLLR" Then
            WIMRToolStripMenuItem.Checked = False
            DLLRToolStripMenuItem.Checked = True
            REGTWEAKToolStripMenuItem.Checked = False
            If File.Exists(TextBox1.Text) And File.Exists(TextBox2.Text) Then
                Button6.Enabled = True
            Else
                Button6.Enabled = False
            End If
        ElseIf ComboBox5.SelectedItem = "REGTWEAK" Then
            WIMRToolStripMenuItem.Checked = False
            DLLRToolStripMenuItem.Checked = False
            REGTWEAKToolStripMenuItem.Checked = True
        End If
        If ComboBox5.SelectedItem = "REGTWEAK" Then
            TextBox2.Enabled = False
            If TextBox1.Text = "" Then
                Button6.Enabled = False
            Else
                If File.Exists(TextBox1.Text) Then
                    Button6.Enabled = True
                Else
                    Button6.Enabled = False
                End If
            End If
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                Win10PresenceSTLabel.Text = "This file is not necessary"
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                Win10PresenceSTLabel.Text = "Este archivo no es necesario"
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                Win10PresenceSTLabel.Text = "Ce fichier n'est pas nécessaire"
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    Win10PresenceSTLabel.Text = "This file is not necessary"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    Win10PresenceSTLabel.Text = "Este archivo no es necesario"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    Win10PresenceSTLabel.Text = "Ce fichier n'est pas nécessaire"
                End If
            End If
        Else
            TextBox2.Enabled = True
            If TextBox2.Text = "" Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    Win10PresenceSTLabel.Text = "Presence status: unknown"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    Win10PresenceSTLabel.Text = "Estado de presencia: desconocido"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        Win10PresenceSTLabel.Text = "Presence status: unknown"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        Win10PresenceSTLabel.Text = "Estado de presencia: desconocido"
                    End If
                End If
            Else
                If File.Exists(TextBox2.Text) Then
                    If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                        Win11PresenceSTLabel.Text = "Presence status: this file exists"
                    ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                        Win11PresenceSTLabel.Text = "Estado de presencia: este archivo existe"
                    ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                        Win11PresenceSTLabel.Text = "Statut de présence : ce fichier existe"
                    ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                            Win11PresenceSTLabel.Text = "Presence status: this file exists"
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                            Win11PresenceSTLabel.Text = "Estado de presencia: este archivo existe"
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                            Win11PresenceSTLabel.Text = "Statut de présence : ce fichier existe"
                        End If
                    End If
                Else
                    If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                        Win11PresenceSTLabel.Text = "Presence status: this file does not exist"
                    ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                        Win11PresenceSTLabel.Text = "Estado de presencia: este archivo no existe"
                    ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                        Win11PresenceSTLabel.Text = "Statut de présence : ce fichier n'existe pas"
                    ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                            Win11PresenceSTLabel.Text = "Presence status: this file does not exist"
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                            Win11PresenceSTLabel.Text = "Estado de presencia: este archivo no existe"
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                            Win11PresenceSTLabel.Text = "Statut de présence : ce fichier n'existe pas"
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub LabelSetButton_Click(sender As Object, e As EventArgs) Handles LabelSetButton.Click
        Label78.Text = LabelText.Text
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            Label34.Text = "Label: " & LabelText.Text
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            Label34.Text = "Etiqueta: " & LabelText.Text
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            Label34.Text = "Étiquette : " & LabelText.Text
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label34.Text = "Label: " & LabelText.Text
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label34.Text = "Etiqueta: " & LabelText.Text
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label34.Text = "Étiquette : " & LabelText.Text
            End If
        End If
    End Sub

    Private Sub HelpPic_Click(sender As Object, e As EventArgs) Handles HelpPic.Click, PictureBox33.Click, Label101.Click
        DisableBackPic()
        WelcomePanel.Visible = False
        InstCreatePanel.Visible = False
        SettingPanel.Visible = False
        Settings_PersonalizationPanel.Visible = False
        Settings_FunctionalityPanel.Visible = False
        InstrPanel.Visible = False
        HelpPanel.Visible = True
        InfoPanel.Visible = False
        SettingReviewPanel.Visible = False
        ProgressPanel.Visible = False
        PanelIndicatorPic.Top = HelpPic.Top + 2
        PanelIndicatorPic.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Left), AnchorStyles)
        If BackColor = Color.FromArgb(243, 243, 243) Then
            WelcomePic.Image = New Bitmap(My.Resources.home)
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create)
            SettingsPic.Image = New Bitmap(My.Resources.settings)
            InstructionPic.Image = New Bitmap(My.Resources.instructions)
            HelpPic.Image = New Bitmap(My.Resources.help_filled)
            InfoPic.Image = New Bitmap(My.Resources.info)
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            WelcomePic.Image = New Bitmap(My.Resources.home_dark)
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark)
            SettingsPic.Image = New Bitmap(My.Resources.settings_dark)
            InstructionPic.Image = New Bitmap(My.Resources.instructions_dark)
            HelpPic.Image = New Bitmap(My.Resources.help_dark_filled)
            InfoPic.Image = New Bitmap(My.Resources.info_dark)
        End If
        WelcomeTopBarPic.Visible = False
        InstCreateTopBarPic.Visible = False
        InstructionsTopBarPic.Visible = False
        HelpTopBarPic.Visible = True
        AboutTopBarPic.Visible = False
        SettingsTopBarPic.Visible = False
        ApplyNavBarImages()
    End Sub

    Private Sub PictureBox21_Click(sender As Object, e As EventArgs) Handles PictureBox21.Click, Label44.Click, Label43.Click, PictureBox20.Click
        PanelIndicatorPic.Top = InstCreatePic.Top + 2
        WelcomeTopBarPic.Visible = False
        InstCreateTopBarPic.Visible = True
        InstructionsTopBarPic.Visible = False
        HelpTopBarPic.Visible = False
        AboutTopBarPic.Visible = False
        SettingsTopBarPic.Visible = False

        If InstCreateInt = 0 Then
            WelcomePanel.Visible = False
            SettingPanel.Visible = False
            Settings_FunctionalityPanel.Visible = False
            Settings_PersonalizationPanel.Visible = False
            HelpPanel.Visible = False
            InfoPanel.Visible = False
            InstrPanel.Visible = False
            InstCreatePanel.Visible = True
            If BackColor = Color.FromArgb(243, 243, 243) Then
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_filled)
                WelcomePic.Image = New Bitmap(My.Resources.home)
            Else
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark_filled)
                WelcomePic.Image = New Bitmap(My.Resources.home_dark)
            End If
        ElseIf InstCreateInt = 1 Then
            EnableBackPic()
            WelcomePanel.Visible = False
            SettingPanel.Visible = False
            Settings_FunctionalityPanel.Visible = False
            Settings_PersonalizationPanel.Visible = False
            HelpPanel.Visible = False
            InfoPanel.Visible = False
            InstrPanel.Visible = False
            SettingReviewPanel.Visible = True
            If BackColor = Color.FromArgb(243, 243, 243) Then
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_filled)
                WelcomePic.Image = New Bitmap(My.Resources.home)
            Else
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark_filled)
                WelcomePic.Image = New Bitmap(My.Resources.home_dark)
            End If
        ElseIf InstCreateInt = 2 Then
            EnableBackPic()
            WelcomePanel.Visible = False
            SettingPanel.Visible = False
            Settings_FunctionalityPanel.Visible = False
            Settings_PersonalizationPanel.Visible = False
            HelpPanel.Visible = False
            InfoPanel.Visible = False
            InstrPanel.Visible = False
            ProgressPanel.Visible = True
            If BackColor = Color.FromArgb(243, 243, 243) Then
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_filled)
                WelcomePic.Image = New Bitmap(My.Resources.home)
            Else
                InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark_filled)
                WelcomePic.Image = New Bitmap(My.Resources.home_dark)
            End If
        End If
        ApplyNavBarImages()
    End Sub

    Private Sub PictureBox23_Click(sender As Object, e As EventArgs) Handles PictureBox23.Click, Label46.Click, Label45.Click, PictureBox22.Click
        PanelIndicatorPic.Top = SettingsPic.Top + 2
        WelcomeTopBarPic.Left = PictureBox7.Left + 2
        WelcomePanel.Visible = False
        InstCreatePanel.Visible = False
        HelpPanel.Visible = False
        InstrPanel.Visible = False
        SettingPanel.Visible = True
        InfoPanel.Visible = False
        If BackColor = Color.FromArgb(243, 243, 243) Then
            SettingsPic.Image = New Bitmap(My.Resources.settings_filled)
            WelcomePic.Image = New Bitmap(My.Resources.home)
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create)
            InstructionPic.Image = New Bitmap(My.Resources.instructions)
            HelpPic.Image = New Bitmap(My.Resources.help)
            InfoPic.Image = New Bitmap(My.Resources.info)
        Else
            SettingsPic.Image = New Bitmap(My.Resources.settings_dark_filled)
            WelcomePic.Image = New Bitmap(My.Resources.home_dark)
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark)
            InstructionPic.Image = New Bitmap(My.Resources.instructions_dark)
            HelpPic.Image = New Bitmap(My.Resources.help_dark)
            InfoPic.Image = New Bitmap(My.Resources.info_dark)
        End If
    End Sub

    Private Sub PictureBox25_Click(sender As Object, e As EventArgs) Handles PictureBox25.Click, Label48.Click, Label47.Click, PictureBox24.Click
        PanelIndicatorPic.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Left), AnchorStyles)
    End Sub

    Private Sub PictureBox27_Click(sender As Object, e As EventArgs) Handles PictureBox27.Click, Label50.Click, Label49.Click, PictureBox26.Click
        WelcomePanel.Visible = False
        InstCreatePanel.Visible = False
        SettingPanel.Visible = False
        Settings_PersonalizationPanel.Visible = False
        Settings_FunctionalityPanel.Visible = False
        InstrPanel.Visible = False
        HelpPanel.Visible = True
        InfoPanel.Visible = False
        SettingReviewPanel.Visible = False
        ProgressPanel.Visible = False
        PanelIndicatorPic.Top = HelpPic.Top + 2
        PanelIndicatorPic.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Left), AnchorStyles)
        If BackColor = Color.FromArgb(243, 243, 243) Then
            WelcomePic.Image = New Bitmap(My.Resources.home)
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create)
            SettingsPic.Image = New Bitmap(My.Resources.settings)
            InstructionPic.Image = New Bitmap(My.Resources.instructions)
            HelpPic.Image = New Bitmap(My.Resources.help_filled)
            InfoPic.Image = New Bitmap(My.Resources.info)
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            WelcomePic.Image = New Bitmap(My.Resources.home_dark)
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark)
            SettingsPic.Image = New Bitmap(My.Resources.settings_dark)
            InstructionPic.Image = New Bitmap(My.Resources.instructions_dark)
            HelpPic.Image = New Bitmap(My.Resources.help_dark_filled)
            InfoPic.Image = New Bitmap(My.Resources.info_dark)
        End If
        WelcomeTopBarPic.Visible = False
        InstCreateTopBarPic.Visible = False
        InstructionsTopBarPic.Visible = False
        HelpTopBarPic.Visible = True
        AboutTopBarPic.Visible = False
        SettingsTopBarPic.Visible = False
        ApplyNavBarImages()
    End Sub

    Private Sub PictureBox29_Click(sender As Object, e As EventArgs) Handles PictureBox29.Click, Label2.Click, Label51.Click, PictureBox28.Click
        WelcomePanel.Visible = False
        InstCreatePanel.Visible = False
        SettingReviewPanel.Visible = False
        ProgressPanel.Visible = False
        SettingPanel.Visible = False
        Settings_PersonalizationPanel.Visible = False
        Settings_FunctionalityPanel.Visible = False
        InstrPanel.Visible = False
        HelpPanel.Visible = False
        InfoPanel.Visible = True
        If BackColor = Color.FromArgb(243, 243, 243) Then
            WelcomePic.Image = New Bitmap(My.Resources.home)
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create)
            InstructionPic.Image = New Bitmap(My.Resources.instructions)
            SettingsPic.Image = New Bitmap(My.Resources.settings)
            HelpPic.Image = New Bitmap(My.Resources.help)
            InfoPic.Image = New Bitmap(My.Resources.info_filled)
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            WelcomePic.Image = New Bitmap(My.Resources.home_dark)
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark)
            InstructionPic.Image = New Bitmap(My.Resources.instructions_dark)
            SettingsPic.Image = New Bitmap(My.Resources.settings_dark)
            HelpPic.Image = New Bitmap(My.Resources.help_dark)
            InfoPic.Image = New Bitmap(My.Resources.info_dark_filled)
        End If
        PanelIndicatorPic.Top = InfoPic.Top + 2
        PanelIndicatorPic.Anchor = CType((AnchorStyles.Bottom Or AnchorStyles.Left), AnchorStyles)
        WelcomeTopBarPic.Visible = False
        InstCreateTopBarPic.Visible = False
        InstructionsTopBarPic.Visible = False
        HelpTopBarPic.Visible = False
        AboutTopBarPic.Visible = True
        SettingsTopBarPic.Visible = False
        ApplyNavBarImages()
    End Sub

    Private Sub SetDefaultButton_Click(sender As Object, e As EventArgs) Handles SetDefaultButton.Click
        LabelText.Text = "Windows11"
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            Label34.Text = "Label: " & LabelText.Text
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            Label34.Text = "Etiqueta: " & LabelText.Text
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            Label34.Text = "Étiquette : " & LabelText.Text
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label34.Text = "Label: " & LabelText.Text
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label34.Text = "Etiqueta: " & LabelText.Text
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label34.Text = "Étiquette : " & LabelText.Text
            End If
        End If
    End Sub

    Private Sub GroupBox6_MouseHover(sender As Object, e As EventArgs) Handles GroupBox6.MouseHover
        ConglomerateToolTip.SetToolTip(GroupBox6, "The images you specify here will be used by the program to create the custom installer")
    End Sub

    Private Sub GroupBox7_MouseHover(sender As Object, e As EventArgs) Handles GroupBox7.MouseHover
        ConglomerateToolTip.SetToolTip(GroupBox7, "Here you can specify the name and location of the target installer")
    End Sub

    Private Sub Label22_MouseHover(sender As Object, e As EventArgs) Handles Label22.MouseHover, RadioButton1.MouseHover, RadioButton2.MouseHover
        ConglomerateToolTip.SetToolTip(Label22, "Here you can set the platform compatibility for the target installer. If you set " & Quote & "BIOS/UEFI-CSM" & Quote & " the installer will be guaranteed for broader hardware compatibility. View the platform compatibility details for more information")
    End Sub

    Private Sub GroupBox3_MouseHover(sender As Object, e As EventArgs) Handles GroupBox3.MouseHover
        ConglomerateToolTip.SetToolTip(GroupBox3, "Here you can see the details of your selected platform compatibility option")
    End Sub

    Private Sub GroupBox4_MouseHover(sender As Object, e As EventArgs) Handles GroupBox4.MouseHover, Label24.MouseHover, GroupBox12.MouseHover
        ConglomerateToolTip.SetToolTip(GroupBox4, "In this section you can specify the target installer's label. The maximum amount of characters you can set as the label is 32")    ' , so please be creative when putting a custom label!!!
    End Sub

    Private Sub Label25_MouseHover(sender As Object, e As EventArgs) Handles Label25.MouseHover
        ConglomerateToolTip.SetToolTip(Label25, "This is the label used on the custom installer")
    End Sub

    Private Sub LabelSetButton_MouseHover(sender As Object, e As EventArgs) Handles LabelSetButton.MouseHover
        ConglomerateToolTip.SetToolTip(LabelSetButton, "Click here to set this label")
    End Sub

    Private Sub SetDefaultButton_MouseHover(sender As Object, e As EventArgs) Handles SetDefaultButton.MouseHover, Button13.MouseHover, Button11.MouseHover
        ConglomerateToolTip.SetToolTip(SetDefaultButton, "Click here to set the default label, " & Quote & "Windows11" & Quote & ", for the custom installer")
    End Sub

    Private Sub LabelText_TextChanged(sender As Object, e As EventArgs) Handles LabelText.TextChanged
        If LabelText.Text = "Windows11" Then
            SetDefaultButton.Enabled = False
        Else
            SetDefaultButton.Enabled = True
        End If
    End Sub

    Private Sub TableLayoutPanel1_MouseHover(sender As Object, e As EventArgs) Handles TableLayoutPanel1.MouseHover
        ConglomerateToolTip.SetToolTip(TableLayoutPanel1, "Click on one of these links to see how to enable the Compatibility Support Module (CSM) on your system")
    End Sub

    Private Sub LinkLabel3_MouseHover(sender As Object, e As EventArgs) Handles LinkLabel3.MouseHover
        ConglomerateToolTip.SetToolTip(LinkLabel3, "Click on one of these links to see how to enable the Compatibility Support Module (CSM) on your system")
    End Sub

    Private Sub LinkLabel4_MouseHover(sender As Object, e As EventArgs) Handles LinkLabel4.MouseHover
        ConglomerateToolTip.SetToolTip(LinkLabel4, "Click on one of these links to see how to enable the Compatibility Support Module (CSM) on your system")
    End Sub

    Private Sub GroupBox5_MouseHover(sender As Object, e As EventArgs) Handles GroupBox5.MouseHover
        ConglomerateToolTip.SetToolTip(GroupBox5, "Here you can set a custom DPI scale for the program, ideal for devices with bigger displays and DPI scales")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TrackBar1.Value = 100 Then
            Font = New Font("Segoe UI", 8.75)
        ElseIf TrackBar1.Value = 125 Then
            Font = New Font("Segoe UI", 8.75 * 1.125)
        ElseIf TrackBar1.Value = 150 Then
            Font = New Font("Segoe UI", 8.75 * 1.25)
        ElseIf TrackBar1.Value = 175 Then
            Font = New Font("Segoe UI", 8.75 * 1.375)
        ElseIf TrackBar1.Value = 200 Then
            Font = New Font("Segoe UI", 8.75 * 1.5)
        End If
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            LangInt = 2
            If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
                Text = "Instalador manual de Windows 11 (modo de administrador)"
            Else
                Text = "Instalador manual de Windows 11"
            End If
            Notify.Text = "Instalador manual de Windows 11 - Listo"
            ' Labels
            Label1.Text = "Bienvenido"
            Label100.Text = "Instrucciones"
            Label101.Text = "Ayuda"
            Label102.Text = "Acerca de"
            Label104.Text = "Instrucciones"
            scText.Text = "El código fuente de este programa lo puede encontrar en GitHub." & CrLf & CrLf & "- Este proyecto puede ser abierto en Visual Studio 2012 y más reciente, sin necesidad de conversión" & CrLf & "- El Pack de Desarrolladores de .NET Framework 4.8 debe ser instalado para abrir este proyecto" & CrLf & CrLf & "¿Desea sugerir alguna nueva característica para ser incluida en una versión futura? ¿Ha notado algún error del que desea informar? No dude en reportar errores o (incluso mejor) contribuye al proyecto (necesita una cuenta de GitHub). Sus opiniones son cruciales." & CrLf & CrLf & "¿Desea disfrutar de las últimas características? ¡Compruebe la rama Hummingbird! Actualizaciones semanales, características nuevas y una vista previa del futuro del programa"
            Label106.Text = "Lanzamiento Hummingbird"
            Label108.Text = "Instalador de destino:"
            Label109.Text = "Ubicación del instalador de destino:"

            ' Label11 doesn't go here, as it's a ">". It will change its Left property instead.
            Label110.Text = "Fecha de creación del instalador de destino:"
            Label111.Text = "Advertencias y errores del instalador:"
            Label114.Text = "Advertencias: " & WarnCount
            Label115.Text = "Errores: " & ErrorCount

            Label12.Text = "Personalización"

            Label13.Text = "Modo de color:"
            Label14.Text = "Aquí puede cambiar la posición de la barra de navegación. Posición:"
            ' Label16 will also change its Left property
            Label17.Text = "Funcionalidad"
            Label19.Text = "Idioma:"
            Label2.Text = "Muestra información sobre el programa"
            Label20.Text = "Método de creación del instalador:"
            Label22.Text = "Compatibilidad de plataformas:"
            ' Label23, 26 and 27 aren't affected in any matter
            Label24.Text = "Aquí puede asignar una etiqueta personalizada"
            Label25.Text = "Etiqueta:"
            Label28.Text = "Escala DPI a ser aplicada: " & TrackBar1.Value & "%"
            Label3.Text = "Al menos un instalador está a punto de ser creado"
            Label30.Text = "Idioma: " & ComboBox4.SelectedItem
            Label31.Text = "Modo de color: " & ComboBox1.SelectedItem
            Label33.Text = "Método de creación del instalador: " & ComboBox5.SelectedItem
            Label34.Text = "Etiqueta: " & LabelText.Text
            Label36.Text = "Cambia las opciones de apariencia, como el idioma, el modo de color, o la escala DPI"
            Label38.Text = "Cambia configuraciones relacionadas a la creación del instalador personalizado"
            Label39.Text = "Limpia el registro del historial de instaladores, el cual es accesible haciendo clic en " & Quote & "Ver historial del instalador" & Quote & " en el menú principal"
            Label4.Text = "Acerca de"
            Label40.Text = "Limpiar registro"
            Label41.Text = "Restablece todas las preferencias a los valores predeterminados"
            Label42.Text = "Restablecer preferencias"
            Label43.Text = "Crea un instalador modificado para instalar Windows 11 en sistemas no soportados"
            Label44.Text = "Crear un instalador modificado"
            Label45.Text = "Le ayuda a personalizar la apariencia y el comportamiento del programa"
            Label47.Text = "Le ayuda a crear un instalador modificado de Windows 11 por usted mismo"
            Label48.Text = "Instrucciones"
            Label49.Text = "Aprenda a cómo usar este programa"
            ' These lines of code affect Label5 and PictureBox11
            Label50.Text = "Ayuda"
            Label51.Text = "Acerca de"
            Label53.Text = "Instalador de Windows 11"
            Label54.Text = "Instalador de Windows 10"
            Label55.Text = "Instalador de Windows 11:"
            Label56.Text = "Instalador de Windows 10:"
            Label57.Text = "Opciones del archivo ISO"
            Label58.Text = "Nombre del archivo ISO:"
            Label59.Text = "Ubicación del archivo ISO:"
            Label60.Text = "Cuando esté listo, haga clic en " & Quote & "Crear" & Quote
            Label63.Text = "Revise sus configuraciones"
            Label64.Text = "Ubicación y nombre:"
            Label65.Text = "Imagen de Windows 11:"
            Label66.Text = "Imagen de Windows 10:"
            Label69.Text = "Método:"
            ' Labels71 through 74 were deleted due to lack of functionality
            Label75.Text = "Compatibilidad de plataformas:"
            Label77.Text = "Etiqueta del instalador:"
            Label78.Text = LabelText.Text
            Label79.Text = "¿Estas configuraciones son las correctas?"
            If InstCreateInt = 3 Then
                Label82.Text = "Final"
                Label83.Text = "El instalador modificado fue creado en la ubicación especificada. Por favor, lea los detalles abajo."
            Else
                Label82.Text = "Progreso"
                Label83.Text = "Ésta es toda la información que necesitamos en este momento. Esto tardará unos minutos, por lo que sea paciente."
            End If
            Label84.Text = "Preparando el espacio de trabajo"
            Label85.Text = "Obteniendo instrucciones"
            Label86.Text = "Extrayendo archivos de los instaladores"
            Label87.Text = "Creando el instalador"
            Label88.Text = "Finalizando"
            Label89.Text = "La acción realizada en este momento no puede ser cancelada. Por favor, espere."
            Label9.Text = "Configuración"
            If needsUpdates Then
                Label91.Text = "Las actualizaciones aportan nuevas características y correcciones de errores al programa. Haga clic en " & Quote & "Comprobar actualizaciones" & Quote & " para comprobar actualizaciones del programa."
            Else
                If UpdateCheckDate = Nothing Then
                    Label91.Text = "Para comprobar actualizaciones del programa, haga clic en " & Quote & "Comprobar actualizaciones" & Quote
                Else
                    If UpdateCheckDate.Day.Equals(14) And UpdateCheckDate.Month.Equals(3) Then
                        Label91.Text = "Para comprobar actualizaciones del programa, haga clic en " & Quote & "Comprobar actualizaciones" & Quote & CrLf & "Última comprobación de actualizaciones realizada en: día π en " & UpdateCheckDate.ToLongTimeString
                    Else
                        Label91.Text = "Para comprobar actualizaciones del programa, haga clic en " & Quote & "Comprobar actualizaciones" & Quote & CrLf & "Última comprobación de actualizaciones realizada en: " & UpdateCheckDate
                    End If
                End If
            End If
            Label92.Text = "Ésta es la información detallada de los componentes usados por el programa:"
            Label97.Text = "Todos los componentes mostrados aquí son protegidos por sus propios términos de licencia, y este programa SOLO puede redistribuir componentes de código abierto."
            AdminLabel.Text = "MODO DE ADMINISTRADOR"
            ProgramTitleLabel.Text = "Instalador manual de Windows 11"
            Last_Created_Inst_Label.Text = "Último instalador creado a las:"


            ' Labels that belong to main sections will adopt the text from the main panels
            Label10.Text = Label9.Text
            Label18.Text = Label9.Text
            Label35.Text = Label12.Text
            Label37.Text = Label17.Text
            Label52.Text = Label50.Text
            Label61.Text = Label44.Text
            Label8.Text = Label44.Text
            Label80.Text = Label44.Text
            Label15.Text = Label1.Text
            Label99.Text = Label44.Text

            ' Miscelaneous labels
            Label29.Text = Label12.Text
            Label32.Text = Label17.Text
            Label46.Text = Label9.Text
            NameLabel.Text = "Nombre: " & TextBox3.Text
            If InstHistPanel.InstallerListView.Items.Count = 0 Then
                Last_Inst_Time_Label.Text = "Ningún dato temporal disponible"
            End If
            If File.Exists(TextBox1.Text) Then
                Win11PresenceSTLabel.Text = "Estado de presencia: este archivo existe"
            Else
                Win11PresenceSTLabel.Text = "Estado de presencia: este archivo no existe"
            End If
            If TextBox1.Text = "" Then
                Win11PresenceSTLabel.Text = "Estado de presencia: desconocido"
            End If
            If File.Exists(TextBox2.Text) Then
                Win10PresenceSTLabel.Text = "Estado de presencia: este archivo existe"
            Else
                Win10PresenceSTLabel.Text = "Estado de presencia: este archivo no existe"
            End If
            If TextBox2.Text = "" Then
                Win10PresenceSTLabel.Text = "Estado de presencia: desconocido"
            End If
            If TextBox4.Text.EndsWith("\") Then
                If TextBox4.Text.Contains(" ") Then
                    ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                Else
                    ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
                End If
            Else
                If TextBox4.Text.Contains(" ") Then
                    ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                Else
                    ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
                End If
            End If

            ' LinkLabels
            LinkLabel1.Text = "Ver historial del instalador"
            LinkLabel2.Text = "Continuar creación del instalador"
            LinkLabel3.Text = "Buscar en Google"
            LinkLabel4.Text = "Buscar en DuckDuckGo"
            LinkLabel5.Text = "Cambiar nombre"
            LinkLabel6.Text = "No se pudo obtener el modelo del equipo"
            LinkLabel7.Text = "Hay actualizaciones disponibles. Haga clic aquí para saber más."
            LinkLabel8.Text = "Compruebe el proyecto"
            LinkLabel9.Text = "Descargue el Pack de Desarrolladores"
            LinkLabel10.Text = "Compruebe la página de Errores"
            LinkLabel11.Text = "Compruebe la rama Hummingbird"
            LinkLabel12.Text = "Hay algunas cosas que valen la pena revisar antes de continuar. Haga clic aquí para saber más."
            LinkLabel17.Text = "Más opciones..."
            LogViewLink.Text = "Ver archivo de registro"

            ' GroupBoxes
            GroupBox1.Text = "Posición de navegación"
            GroupBox12.Text = "Opciones de bandeja del sistema"
            GroupBox3.Text = "Detalles de compatibilidad de plataformas"
            GroupBox4.Text = "Opciones de etiqueta"
            GroupBox5.Text = "Opciones de escala DPI"
            GroupBox6.Text = "Archivos ISO"
            GroupBox7.Text = "Opciones de imagen de destino"
            GroupBox8.Text = "Imágenes"
            GroupBox9.Text = "Opciones de creación del instalador"
            GroupBox10.Text = "Registro"
            GroupBox11.Text = "Imagen de destino"

            ' Buttons
            Button1.Text = "Aplicar escala DPI"
            Button2.Text = "Examinar..."
            Button3.Text = "Restablecer"
            Button4.Text = "Examinar..."
            Button5.Text = "Examinar..."
            Button6.Text = "Crear"
            Button8.Text = "Sí"
            Button9.Text = "Ocultar registro"
            If InstCreateInt = 3 Then
                Button10.Text = "Aceptar"
            Else
                Button10.Text = "Cancelar"
            End If
            Button11.Text = "Ayuda de métodos"
            Button12.Text = "Comprobar actualizaciones"
            Button13.Text = "Opciones avanzadas"
            ScanButton.Text = "Escanear..."
            LabelSetButton.Text = "Establecer"
            LabelSetButton.PerformClick()
            SetDefaultButton.Text = "Predeterminado"

            ' CheckBoxes
            CheckBox1.Text = "Mostrar notificación de bandeja del sistema una vez"
            CheckBox3.Text = "Al cerrarse, ocultar en la bandeja del sistema"
            CheckBox4.Text = "Cerrar programa después de hacer clic en Aceptar"

            ' MenuStrip items
            Windows11ManualInstallerToolStripMenuItem.Text = "Instalador manual de Windows 11"
            VersionToolStripMenuItem.Text = "versión " & VerStr & " (versión de ensamblado " & AVerStr & ")"
            InstSTLabel.Text = "Estado del instalador"
            StatusTSI.Text = "No se está creando ningún instalador en este momento"
            LastInstallerCreatedAtToolStripMenuItem.Text = "Último instalador creado a las:"
            If InstHistPanel.InstallerListView.Items.Count = 0 Then
                IHDataToolStripMenuItem.Text = "No hay datos del historial del instalador en este momento"
            End If
            ViewInstallerHistoryToolStripMenuItem.Text = "Ver historial del instalador"
            LanguageToolStripMenuItem.Text = "Idioma"
            AutomaticLanguageToolStripMenuItem.Text = "Automático"
            AutomaticLanguageToolStripMenuItem.Checked = False
            EnglishToolStripMenuItem.Text = "Inglés"
            EnglishToolStripMenuItem.Checked = False
            SpanishToolStripMenuItem.Text = "Español"
            SpanishToolStripMenuItem.Checked = True
            FrenchToolStripMenuItem.Text = "Francés"
            FrenchToolStripMenuItem.Checked = False
            ColorModeToolStripMenuItem.Text = "Modo del color"
            AutomaticToolStripMenuItem.Text = "Automático"
            LightToolStripMenuItem.Text = "Claro"
            DarkToolStripMenuItem.Text = "Oscuro"
            If ComboBox1.SelectedItem = "Automatic" Or ComboBox1.SelectedItem = "Automático" Or ComboBox1.SelectedItem = "Automatique" Then
                AutomaticToolStripMenuItem.Checked = True
                LightToolStripMenuItem.Checked = False
                DarkToolStripMenuItem.Checked = False
            ElseIf ComboBox1.SelectedItem = "Light" Or ComboBox1.SelectedItem = "Claro" Or ComboBox1.SelectedItem = "Lumière" Then
                AutomaticToolStripMenuItem.Checked = False
                LightToolStripMenuItem.Checked = True
                DarkToolStripMenuItem.Checked = False
            ElseIf ComboBox1.SelectedItem = "Dark" Or ComboBox1.SelectedItem = "Oscuro" Or ComboBox1.SelectedItem = "Sombre" Then
                AutomaticToolStripMenuItem.Checked = False
                LightToolStripMenuItem.Checked = False
                DarkToolStripMenuItem.Checked = True
            End If
            InstallerCreationMethodToolStripMenuItem.Text = "Método de creación del instalador"
            AOTSMI.Text = "Opciones avanzadas"
            OpenToolStripMenuItem.Text = "Abrir"
            ExitToolStripMenuItem.Text = "Salir"


            ' Positioning
            Label11.Left = Label10.Left + Label10.Width + 6
            Label12.Left = Label11.Left + Label11.Width
            Label16.Left = Label18.Left + Label18.Width + 6
            Label17.Left = Label16.Left + Label16.Width
            Label62.Left = Label61.Left + Label61.Width + 6
            Label63.Left = Label62.Left + Label62.Width + 4
            Label81.Left = Label80.Left + Label80.Width + 6
            Label82.Left = Label81.Left + Label81.Width + 4

            ' TabPages
            TabPage1.Text = "General"
            TabPage2.Text = "Información de componentes"
            TabPage3.Text = "Código fuente"

            ' RadioButtons
            RadioButton3.Text = "Izquierda"
            RadioButton4.Text = "Arriba"

            ' TextBox positioning and size
            TextBox1.Left = 232
            TextBox1.Width = 348
            TextBox2.Left = 232
            TextBox2.Width = 348
            TextBox3.Left = 221
            TextBox3.Width = 457
            TextBox4.Left = 229
            TextBox4.Width = 483
            LabelText.Left = 93
            LabelText.Width = 240

            ' Miscellaneous positioning and size
            ComboBox5.Left = 326
            ComboBox5.Width = 346

            ' ComboBox relabelling
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Automático")
            ComboBox1.Items.Add("Claro")
            ComboBox1.Items.Add("Oscuro")
            If ColorInt = 0 Then
                ComboBox1.SelectedItem = "Automático"
            ElseIf ColorInt = 1 Then
                ComboBox1.SelectedItem = "Oscuro"
            ElseIf ColorInt = 2 Then
                ComboBox1.SelectedItem = "Claro"
            End If
            ' This line of code freezes the program. Maybe because it's cold in Alaska now?
            'ComboBox4.Items.Clear()
            ComboBox4.Items.Add("Automático")
            ComboBox4.Items.Add("Inglés")
            ComboBox4.Items.Add("Español")
            ComboBox4.SelectedItem = "Español"
            ComboBox4.Items.Remove("Automatic")
            ComboBox4.Items.Remove("English")
            ComboBox4.Items.Remove("Spanish")
            If ComboBox4.Items.Count > 4 Then   ' There is a bug in this procedure where it would make duplicate items of the ComboBox. 
                Do Until ComboBox4.Items.Count = 4  ' This fixes it
                    ComboBox4.Items.RemoveAt(4)
                Loop
            End If

            ' FolderBrowserDialog
            ImageFolderBrowser.Description = "Especifique un directorio para guardar el instalador de destino:"
        ElseIf ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            LangInt = 1
            If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
                Text = "Windows 11 Manual Installer (administrator mode)"
            Else
                Text = "Windows 11 Manual Installer"
            End If
            Notify.Text = "Windows 11 Manual Installer - Ready"
            ' Labels
            Label1.Text = "Welcome"
            Label100.Text = "Instructions"
            Label101.Text = "Help"
            Label102.Text = "About"
            Label104.Text = "Instructions"
            scText.Text = "The source code of this program can be found on GitHub." & CrLf & CrLf & "- This project can be opened on Visual Studio 2012 and newer, without the need of conversion" & CrLf & "- The .NET Framework 4.8 Developer Pack must be installed to open this project" & CrLf & CrLf & "Do you want to suggest a new feature to be included in a future version? Have you experienced a bug you want to report? Don't hesitate on reporting feedback or (even better) contributing (you'll need a GitHub account). Your feedback is critical." & CrLf & CrLf & "Do you want to enjoy the latest features? Check the Hummingbird branch! Weekly updates, new features and a sneak peek of the program's future"
            Label106.Text = "Hummingbird release"
            Label108.Text = "Target installer:"
            Label109.Text = "Target installer path:"

            ' Label11 doesn't go here, as it's a ">". It will change its Left property instead.
            Label110.Text = "Target installer creation date:"
            Label111.Text = "Installer warnings and errors:"
            Label114.Text = "Warnings: " & WarnCount
            Label115.Text = "Errors: " & ErrorCount

            Label12.Text = "Personalization"

            Label13.Text = "Color mode:"
            Label14.Text = "Here you can change the position of the navigation bar. Position:"
            ' Label16 will also change its Left property
            Label17.Text = "Functionality"
            Label19.Text = "Language:"
            Label2.Text = "Shows program information"
            Label20.Text = "Installer creation method:"
            Label22.Text = "Platform compatibility:"
            ' Label23, 26 and 27 aren't affected in any matter
            Label24.Text = "Here you can set a custom label for your image"
            Label25.Text = "Label:"
            Label28.Text = "DPI scale to be applied: " & TrackBar1.Value & "%"
            Label3.Text = "At least one installer is about to be created"
            Label30.Text = "Language: " & ComboBox4.SelectedItem
            Label31.Text = "Color mode: " & ComboBox1.SelectedItem
            Label33.Text = "Installer creation method: " & ComboBox5.SelectedItem
            Label34.Text = "Label: " & LabelText.Text
            LabelSetButton.PerformClick()
            Label36.Text = "Change appearance settings, such as the language, the color mode, or the DPI scale"
            Label38.Text = "Change settings related to how the program makes the custom installer"
            Label39.Text = "Clears the installer history log, which is accessible by clicking " & Quote & "View installer history" & Quote & " on the main menu"
            Label4.Text = "About"
            Label40.Text = "Clear log"
            Label41.Text = "Resets all the preferences to their default values"
            Label42.Text = "Reset preferences"
            Label43.Text = "Creates a modified installer to install Windows 11 on unsupported systems"
            Label44.Text = "Create a custom installer"
            Label45.Text = "Allows you to customize the program's appearance or behavior"
            Label47.Text = "Allows you to create a custom Windows 11 installer by yourself"
            Label48.Text = "Instructions"
            Label49.Text = "Learn how to use this program"
            ' These lines of code affect Label5 and PictureBox11
            Label50.Text = "Help"
            Label51.Text = "About"
            Label53.Text = "Windows 11 installer"
            Label54.Text = "Windows 10 installer"
            Label55.Text = "Windows 11 installer:"
            Label56.Text = "Windows 10 installer:"
            Label57.Text = "ISO file options"
            Label58.Text = "ISO file name:"
            Label59.Text = "ISO file location:"
            Label60.Text = "When you are ready, click " & Quote & "Create" & Quote
            Label63.Text = "Review your settings"
            Label64.Text = "Location and name:"
            Label65.Text = "Windows 11 image:"
            Label66.Text = "Windows 10 image:"
            Label69.Text = "Method:"
            ' Labels71 through 74 were deleted due to lack of functionality
            Label75.Text = "Platform compatibility:"
            Label77.Text = "Installer label:"
            Label78.Text = LabelText.Text
            Label79.Text = "Are these settings OK?"
            Label82.Text = "Progress"
            If InstCreateInt = 3 Then
                Label82.Text = "Finish"
                Label83.Text = "The custom installer was created at the specified location. Please read the details below."
            Else
                Label82.Text = "Progress"
                Label83.Text = "That's all the information we need right now. The installer creation will take a few minutes, so please be patient."
            End If
            Label84.Text = "Preparing the workspace"
            Label85.Text = "Gathering instructions"
            Label86.Text = "Extracting installer files"
            Label87.Text = "Creating the installer"
            Label88.Text = "Finishing up"
            Label89.Text = "The action performed right now cannot be cancelled. Please wait."
            Label9.Text = "Settings"
            If needsUpdates Then
                Label91.Text = "Updates bring new features and bugfixes to this program. Click " & Quote & "Check for updates" & Quote & " to check for program updates."
            Else
                If UpdateCheckDate = Nothing Then
                    Label91.Text = "To check for any program updates, click " & Quote & "Check for updates" & Quote
                Else
                    If UpdateCheckDate.Day.Equals(14) And UpdateCheckDate.Month.Equals(3) Then
                        Label91.Text = "To check for any program updates, click " & Quote & "Check for updates" & Quote & CrLf & "Last update check performed on: π day at " & UpdateCheckDate.ToLongTimeString
                    Else
                        Label91.Text = "To check for any program updates, click " & Quote & "Check for updates" & Quote & CrLf & "Last update check performed on: " & UpdateCheckDate
                    End If
                End If
            End If
            Label92.Text = "Here is detailed information for components used by the program:"
            Label97.Text = "All components shown herein are covered by their own license terms, and this program can ONLY redistribute open-source components."
            AdminLabel.Text = "ADMINISTRATOR MODE"
            ProgramTitleLabel.Text = "Windows 11 Manual Installer"
            Last_Created_Inst_Label.Text = "Last installer created on:"

            ' Labels that belong to main sections will adopt the text from the main panels
            Label10.Text = Label9.Text
            Label18.Text = Label9.Text
            Label35.Text = Label12.Text
            Label37.Text = Label17.Text
            Label52.Text = Label50.Text
            Label61.Text = Label44.Text
            Label8.Text = Label44.Text
            Label80.Text = Label44.Text
            Label15.Text = Label1.Text
            Label99.Text = Label44.Text

            ' Miscelaneous labels
            Label29.Text = Label12.Text
            Label32.Text = Label17.Text
            Label46.Text = Label9.Text
            NameLabel.Text = "Name: " & TextBox3.Text
            If InstHistPanel.InstallerListView.Items.Count = 0 Then
                Last_Inst_Time_Label.Text = "No time data is available"
            End If
            If File.Exists(TextBox1.Text) Then
                Win11PresenceSTLabel.Text = "Presence status: this file exists"
            Else
                Win11PresenceSTLabel.Text = "Presence status: this file does not exist"
            End If
            If TextBox1.Text = "" Then
                Win11PresenceSTLabel.Text = "Presence status: unknown"
            End If
            If File.Exists(TextBox2.Text) Then
                Win10PresenceSTLabel.Text = "Presence status: this file exists"
            Else
                Win10PresenceSTLabel.Text = "Presence status: this file does not exist"
            End If
            If TextBox2.Text = "" Then
                Win10PresenceSTLabel.Text = "Presence status: unknown"
            End If
            If TextBox4.Text.EndsWith("\") Then
                If TextBox4.Text.Contains(" ") Then
                    ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                Else
                    ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
                End If
            Else
                If TextBox4.Text.Contains(" ") Then
                    ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                Else
                    ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
                End If
            End If

            ' LinkLabels
            LinkLabel1.Text = "View installer history"
            LinkLabel2.Text = "Continue installer creation"
            LinkLabel3.Text = "Search on Google"
            LinkLabel4.Text = "Search on DuckDuckGo"
            LinkLabel5.Text = "Rename"
            LinkLabel6.Text = "Could not get computer model"
            LinkLabel7.Text = "Updates are available. Click here to learn more."
            LinkLabel8.Text = "Check this program's project"
            LinkLabel9.Text = "Download the Developer Pack"
            LinkLabel10.Text = "Check the Issues page"
            LinkLabel11.Text = "Check the Hummingbird branch"
            LinkLabel12.Text = "There are some things that are worth revising before continuing. Click here to learn more."
            LinkLabel17.Text = "More options..."
            LogViewLink.Text = "View log file"

            ' GroupBoxes
            GroupBox1.Text = "Navigation position"
            GroupBox12.Text = "System tray options"
            GroupBox3.Text = "Platform compatibility details"
            GroupBox4.Text = "Label options"
            GroupBox5.Text = "DPI scaling options"
            GroupBox6.Text = "ISO files"
            GroupBox7.Text = "Target image options"
            GroupBox8.Text = "Images"
            GroupBox9.Text = "Installer creation options"
            GroupBox10.Text = "Log"
            GroupBox11.Text = "Target image"

            ' Buttons
            Button1.Text = "Apply DPI scale"
            Button2.Text = "Browse..."
            Button3.Text = "Restore"
            Button4.Text = "Browse..."
            Button5.Text = "Browse..."
            Button6.Text = "Create"
            Button8.Text = "Yes"
            Button9.Text = "Hide log"
            If InstCreateInt = 3 Then
                Button10.Text = "OK"
            Else
                Button10.Text = "Cancel"
            End If
            Button11.Text = "Method help"
            Button12.Text = "Check for updates"
            Button13.Text = "Advanced options"
            ScanButton.Text = "Scan..."
            LabelSetButton.Text = "Set"
            SetDefaultButton.Text = "Set default"

            ' CheckBoxes
            CheckBox1.Text = "Show system tray notification once"
            CheckBox3.Text = "When closing, hide in system tray"
            CheckBox4.Text = "Exit the program after I click OK"

            ' MenuStrip items
            Windows11ManualInstallerToolStripMenuItem.Text = "Windows 11 Manual Installer"
            VersionToolStripMenuItem.Text = "version " & VerStr & " (assembly version " & AVerStr & ")"
            InstSTLabel.Text = "Installer status"
            StatusTSI.Text = "No installers are being created at this time"
            LastInstallerCreatedAtToolStripMenuItem.Text = "Last installer created at:"
            If InstHistPanel.InstallerListView.Items.Count = 0 Then
                IHDataToolStripMenuItem.Text = "No installer history data is available at this time"
            End If
            ViewInstallerHistoryToolStripMenuItem.Text = "View installer history"
            LanguageToolStripMenuItem.Text = "Language"
            AutomaticLanguageToolStripMenuItem.Text = "Automatic"
            AutomaticLanguageToolStripMenuItem.Checked = False
            EnglishToolStripMenuItem.Text = "English"
            EnglishToolStripMenuItem.Checked = True
            SpanishToolStripMenuItem.Text = "Spanish"
            SpanishToolStripMenuItem.Checked = False
            FrenchToolStripMenuItem.Text = "French"
            FrenchToolStripMenuItem.Checked = False
            ColorModeToolStripMenuItem.Text = "Color mode"
            AutomaticToolStripMenuItem.Text = "Automatic"
            LightToolStripMenuItem.Text = "Light"
            DarkToolStripMenuItem.Text = "Dark"
            If ComboBox1.SelectedItem = "Automatic" Or ComboBox1.SelectedItem = "Automático" Or ComboBox1.SelectedItem = "Automatique" Then
                AutomaticToolStripMenuItem.Checked = True
                LightToolStripMenuItem.Checked = False
                DarkToolStripMenuItem.Checked = False
            ElseIf ComboBox1.SelectedItem = "Light" Or ComboBox1.SelectedItem = "Claro" Or ComboBox1.SelectedItem = "Lumière" Then
                AutomaticToolStripMenuItem.Checked = False
                LightToolStripMenuItem.Checked = True
                DarkToolStripMenuItem.Checked = False
            ElseIf ComboBox1.SelectedItem = "Dark" Or ComboBox1.SelectedItem = "Oscuro" Or ComboBox1.SelectedItem = "Sombre" Then
                AutomaticToolStripMenuItem.Checked = False
                LightToolStripMenuItem.Checked = False
                DarkToolStripMenuItem.Checked = True
            End If
            InstallerCreationMethodToolStripMenuItem.Text = "Installer creation method"
            AOTSMI.Text = "Advanced options"
            OpenToolStripMenuItem.Text = "Open"
            ExitToolStripMenuItem.Text = "Exit"


            ' Positioning
            Label11.Left = Label10.Left + Label10.Width + 6
            Label12.Left = Label11.Left + Label11.Width
            Label16.Left = Label18.Left + Label18.Width + 6
            Label17.Left = Label16.Left + Label16.Width
            Label62.Left = Label61.Left + Label61.Width + 6
            Label63.Left = Label62.Left + Label62.Width + 4
            Label81.Left = Label80.Left + Label80.Width + 6
            Label82.Left = Label81.Left + Label81.Width + 4

            ' TabPages
            TabPage1.Text = "Overview"
            TabPage2.Text = "Component information"
            TabPage3.Text = "Source code"

            ' RadioButtons
            RadioButton3.Text = "Left"
            RadioButton4.Text = "Top"

            ' TextBox positioning and size
            TextBox1.Left = 200
            TextBox1.Width = 380
            TextBox2.Left = 200
            TextBox2.Width = 380
            TextBox3.Left = 157
            TextBox3.Width = 521
            TextBox4.Left = 167
            TextBox4.Width = 545
            LabelText.Left = 77
            LabelText.Width = 256

            ' Miscellaneous positioning and size
            ComboBox5.Left = 268
            ComboBox5.Width = 403

            ' ComboBox relabelling
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Automatic")
            ComboBox1.Items.Add("Light")
            ComboBox1.Items.Add("Dark")
            If ColorInt = 0 Then
                ComboBox1.SelectedItem = "Automatic"
            ElseIf ColorInt = 1 Then
                ComboBox1.SelectedItem = "Light"
            ElseIf ColorInt = 2 Then
                ComboBox1.SelectedItem = "Dark"
            End If
            ' This line of code freezes the program. Maybe because it's cold in Alaska now?
            'ComboBox4.Items.Clear()
            ComboBox4.Items.Add("Automatic")
            ComboBox4.Items.Add("English")
            ComboBox4.Items.Add("Spanish")
            ComboBox4.Items.Add("French")
            ComboBox4.SelectedItem = "English"
            ComboBox4.Items.Remove("Automático")
            ComboBox4.Items.Remove("Inglés")
            ComboBox4.Items.Remove("Español")
            ComboBox4.Items.Remove("Francés")
            ComboBox4.Items.Remove("Automatique")
            ComboBox4.Items.Remove("Anglais")
            ComboBox4.Items.Remove("Espagnol")
            ComboBox4.Items.Remove("Français")
            If ComboBox4.Items.Count > 4 Then   ' There is a bug in this procedure where it would make duplicate items of the ComboBox. 
                Do Until ComboBox4.Items.Count = 4  ' This fixes it
                    ComboBox4.Items.RemoveAt(4)
                Loop
            End If
            ImageFolderBrowser.Description = "Please specify the path to save the custom installer:"
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            LangInt = 3
            If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
                Text = "Installateur manuel de Windows 11 (mode administrateur)"
            Else
                Text = "Installateur manuel de Windows 11"
            End If
            Notify.Text = "Installateur manuel de Windows 11 - Prêt"
            ' Labels
            Label1.Text = "Bienvenue"
            Label100.Text = "Instructions"
            Label101.Text = "Aide"
            Label102.Text = "À propos de"
            Label104.Text = "Instructions"
            scText.Text = "Le code source de ce programme peut être trouvé sur GitHub." & CrLf & CrLf & "- Ce projet peut être ouvert sur Visual Studio 2012 et plus récent, sans besoin de conversion." & CrLf & "- Le .NET Framework 4.8 Developer Pack doit être installé pour ouvrir ce projet." & CrLf & CrLf & "Vous souhaitez suggérer une nouvelle fonctionnalité à inclure dans une future version ? Vous avez rencontré un bogue que vous souhaitez signaler ? N'hésitez pas à faire part de vos commentaires ou (encore mieux) à contribuer (vous aurez besoin d'un compte GitHub). Vos commentaires sont essentiels." & CrLf & CrLf & "Vous voulez profiter des dernières fonctionnalités ? Consultez la branche Hummingbird ! Mises à jour hebdomadaires, nouvelles fonctionnalités et aperçu de l'avenir du programme."
            Label106.Text = "Hummingbird version"
            Label108.Text = "Installateur cible :"
            Label109.Text = "Chemin de l'installateur cible :"

            ' Label11 doesn't go here, as it's a ">". It will change its Left property instead.
            Label110.Text = "Date de création de l'installateur cible :"
            Label111.Text = "Avertissements et erreurs de l'installateur :"
            Label114.Text = "Avertissements : " & WarnCount
            Label115.Text = "Erreurs : " & ErrorCount

            Label12.Text = "Personnalisation"

            Label13.Text = "Mode couleur :"
            Label14.Text = "Vous pouvez modifier ici la position de la barre de navigation. Position :"
            ' Label16 will also change its Left property
            Label17.Text = "Fonctionnalité"
            Label19.Text = "Langue:"
            Label2.Text = "Affiche des informations sur le programme"
            Label20.Text = "Méthode de création de l'installateur :"
            Label22.Text = "Compatibilité avec les plates-formes :"
            ' Label23, 26 and 27 aren't affected in any matter
            Label24.Text = "Vous pouvez définir ici une étiquette personnalisée pour votre image."
            Label25.Text = "Étiquette :"
            Label28.Text = "Échelle DPI à appliquer: " & TrackBar1.Value & "%"
            Label3.Text = "Au moins un installateur est sur le point d'être créé"
            Label30.Text = "Langue: " & ComboBox4.SelectedItem
            Label31.Text = "Mode couleur: " & ComboBox1.SelectedItem
            Label33.Text = "Méthode de création de l'installateur : " & ComboBox5.SelectedItem
            Label34.Text = "Étiquette : " & LabelText.Text
            LabelSetButton.PerformClick()
            Label36.Text = "Modifiez les paramètres d'apparence, tels que la langue, le mode de couleur ou l'échelle DPI"
            Label38.Text = "Modifier les paramètres relatifs à la façon dont le programme réalise l'installateur personnalisé"
            Label39.Text = "Efface le journal de l'historique de l'installateur, qui est accessible en cliquant sur " & Quote & "Voir l'historique de l'installateur" & Quote & " dans le menu principal."
            Label4.Text = "À propos de"
            Label40.Text = "Effacer le journal"
            Label41.Text = "Réinitialise toutes les préférences à leurs valeurs par défaut"
            Label42.Text = "Réinitialiser les préférences"
            Label43.Text = "Créer un installateur modifié pour installer Windows 11 sur des systèmes non pris en charge"
            Label44.Text = "Créer un installateur personnalisé"
            Label45.Text = "Permet de personnaliser l'apparence ou le comportement du programme"
            Label47.Text = "Permet de créer soi-même un installateur personnalisé de Windows 11"
            Label48.Text = "Instructions"
            Label49.Text = "Apprenez à utiliser ce programme"
            ' These lines of code affect Label5 and PictureBox11
            Label50.Text = "Aide"
            Label51.Text = "À propos de"
            Label53.Text = "Installateur de Windows 11"
            Label54.Text = "Installateur de Windows 10"
            Label55.Text = "Installateur de Windows 11 :"
            Label56.Text = "Installateur de Windows 10 :"
            Label57.Text = "Options du fichier ISO"
            Label58.Text = "Nom du fichier ISO :"
            Label59.Text = "Lieu du fichier ISO :"
            Label60.Text = "Lorsque vous êtes prêt, cliquez sur " & Quote & "Créer" & Quote
            Label63.Text = "Vérifiez vos paramètres"
            Label64.Text = "Lieu et nom :"
            Label65.Text = "Image du Windows 11 :"
            Label66.Text = "Image du Windows 10 :"
            Label69.Text = "Méthode :"
            ' Labels71 through 74 were deleted due to lack of functionality
            Label75.Text = "Compatibilité avec les plates-formes :"
            Label77.Text = "Étiquette de l'installateur :"
            Label78.Text = LabelText.Text
            Label79.Text = "Ces paramètres sont-ils corrects ?"
            If InstCreateInt = 3 Then
                Label82.Text = "Fin"
                Label83.Text = "Le installateur personnalisé a été créé à l'emplacement spécifié. Veuillez lire les détails ci-dessous."
            Else
                Label82.Text = "Progrès"
                Label83.Text = "C'est toute l'information dont nous avons besoin pour le moment. La création de l'installateur prendra quelques minutes, alors soyez patient."
            End If
            Label84.Text = "Préparation de l'espace de travail"
            Label85.Text = "Rassemblement des instructions"
            Label86.Text = "Extraction des fichiers d'installation"
            Label87.Text = "Création de l'installateur"
            Label88.Text = "Finir"
            Label89.Text = "L'action effectuée en ce moment ne peut être annulée. Veuillez patienter..."
            Label9.Text = "Paramètres"
            If needsUpdates Then
                Label91.Text = "Les mises à jour apportent de nouvelles fonctionnalités et des corrections de bogues à ce programme. Cliquez sur " & Quote & "Vérifier les mises à jour" & Quote & " pour vérifier les mises à jour du programme."
            Else
                If UpdateCheckDate = Nothing Then
                    Label91.Text = "Pour vérifier les mises à jour du programme, cliquez sur " & Quote & "Vérifier les mises à jour" & Quote
                Else
                    If UpdateCheckDate.Day.Equals(14) And UpdateCheckDate.Month.Equals(3) Then
                        Label91.Text = "Pour vérifier les mises à jour du programme, cliquez sur " & Quote & "Vérifier les mises à jour" & Quote & CrLf & "Dernière vérification des mises à jour effectuée le : π jour à " & UpdateCheckDate.ToLongTimeString
                    Else
                        Label91.Text = "Pour vérifier les mises à jour du programme, cliquez sur " & Quote & "Vérifier les mises à jour" & Quote & CrLf & "Dernière vérification des mises à jour effectuée le : " & UpdateCheckDate
                    End If
                End If
            End If
            Label92.Text = "Vous trouverez ici des informations détaillées sur les composants utilisés par le programme :"
            Label97.Text = "Tous les composants présentés ici sont couverts par leurs propres termes de licence, et ce programme peut UNIQUEMENT redistribuer redistribuer des composants open-source."
            AdminLabel.Text = "MODE ADMINISTRATEUR"
            ProgramTitleLabel.Text = "Installateur manuel de Windows 11"
            Last_Created_Inst_Label.Text = "Dernier installateur créé le :"

            ' Labels that belong to main sections will adopt the text from the main panels
            Label10.Text = Label9.Text
            Label18.Text = Label9.Text
            Label35.Text = Label12.Text
            Label37.Text = Label17.Text
            Label52.Text = Label50.Text
            Label61.Text = Label44.Text
            Label8.Text = Label44.Text
            Label80.Text = Label44.Text
            Label15.Text = Label1.Text
            Label99.Text = Label44.Text

            ' Miscelaneous labels
            Label29.Text = Label12.Text
            Label32.Text = Label17.Text
            Label46.Text = Label9.Text
            NameLabel.Text = "Nom : " & TextBox3.Text
            If InstHistPanel.InstallerListView.Items.Count = 0 Then
                Last_Inst_Time_Label.Text = "Aucune date n'est disponible"
            End If
            If File.Exists(TextBox1.Text) Then
                Win11PresenceSTLabel.Text = "Statut de présence : ce fichier existe"
            Else
                Win11PresenceSTLabel.Text = "Statut de présence : ce fichier n'existe pas"
            End If
            If TextBox1.Text = "" Then
                Win11PresenceSTLabel.Text = "Statut de présence : inconnu"
            End If
            If File.Exists(TextBox2.Text) Then
                Win10PresenceSTLabel.Text = "Statut de présence : ce fichier existe"
            Else
                Win10PresenceSTLabel.Text = "Statut de présence : ce fichier n'existe pas"
            End If
            If TextBox2.Text = "" Then
                Win10PresenceSTLabel.Text = "Statut de présence : inconnu"
            End If
            If TextBox4.Text.EndsWith("\") Then
                If TextBox4.Text.Contains(" ") Then
                    ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                Else
                    ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
                End If
            Else
                If TextBox4.Text.Contains(" ") Then
                    ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                Else
                    ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
                End If
            End If

            ' LinkLabels
            LinkLabel1.Text = "Voir l'historique de l'installateur"
            LinkLabel2.Text = "Continuer la creation de l'installateur"
            LinkLabel3.Text = "Recherche sur Google"
            LinkLabel4.Text = "Recherche sur DuckDuckGo"
            LinkLabel5.Text = "Renommer"
            LinkLabel6.Text = "Impossible d'obtenir le modèle d'ordinateur"
            LinkLabel7.Text = "Les mises à jour sont disponibles. Cliquez ici pour en savoir plus."
            LinkLabel8.Text = "Vérifiez le projet de ce programme"
            LinkLabel9.Text = "Télécharger le Developer Pack"
            LinkLabel10.Text = "Vérifiez la page des problèmes"
            LinkLabel11.Text = "Vérifiez la branche Hummingbird"
            LinkLabel12.Text = "Il y a certaines choses qui méritent d'être révisées avant de continuer. Cliquez ici pour en savoir plus."
            LinkLabel17.Text = "Options supplémentaires..."
            LogViewLink.Text = "Afficher le fichier journal"

            ' GroupBoxes
            GroupBox1.Text = "Position de la navigation"
            GroupBox12.Text = "Options de la barre d'état système"
            GroupBox3.Text = "Détails de la compatibilité avec la plate-forme"
            GroupBox4.Text = "Options de l'étiquette"
            GroupBox5.Text = "Options de mise à l'échelle DPI"
            GroupBox6.Text = "Fichiers ISO"
            GroupBox7.Text = "Options de l'image cible"
            GroupBox8.Text = "Images"
            GroupBox9.Text = "Options de création de l'installateur"
            GroupBox10.Text = "Journal"
            GroupBox11.Text = "Image cible"

            ' Buttons
            Button1.Text = "Appliquer l'échelle DPI"
            Button2.Text = "Parcourir..."
            Button3.Text = "Restaurer"
            Button4.Text = "Parcourir..."
            Button5.Text = "Parcourir..."
            Button6.Text = "Créer"
            Button8.Text = "Oui"
            Button9.Text = "Cacher le journal"
            If InstCreateInt = 3 Then
                Button10.Text = "OK"
            Else
                Button10.Text = "Annuler"
            End If
            Button11.Text = "Aide à la méthode"
            Button12.Text = "Vérifier les mises à jour"
            Button13.Text = "Options avancées"
            ScanButton.Text = "Scanner..."
            LabelSetButton.Text = "Définir"
            SetDefaultButton.Text = "Valeur par défaut"

            ' CheckBoxes
            CheckBox1.Text = "Afficher une fois la notification de la barre d'état système"
            CheckBox3.Text = "À la fermeture, masquer dans la barre d'état système"
            CheckBox4.Text = "Quitter le programme après avoir cliqué sur OK"

            ' MenuStrip items
            Windows11ManualInstallerToolStripMenuItem.Text = "Installateur manuel de Windows 11"
            VersionToolStripMenuItem.Text = "version " & VerStr & " (version assemblée " & AVerStr & ")"
            InstSTLabel.Text = "Statut de l'installateur"
            StatusTSI.Text = "Aucun installateur n'est créé pour l'instant"
            LastInstallerCreatedAtToolStripMenuItem.Text = "Dernier installateur créé à :"
            If InstHistPanel.InstallerListView.Items.Count = 0 Then
                IHDataToolStripMenuItem.Text = "Aucune donnée sur l'historique des installateurs n'est disponible pour l'instant"
            End If
            ViewInstallerHistoryToolStripMenuItem.Text = "Voir l'historique de l'installateur"
            LanguageToolStripMenuItem.Text = "Langue"
            AutomaticLanguageToolStripMenuItem.Text = "Automatique"
            AutomaticLanguageToolStripMenuItem.Checked = False
            EnglishToolStripMenuItem.Text = "Anglais"
            EnglishToolStripMenuItem.Checked = False
            SpanishToolStripMenuItem.Text = "Espagnol"
            SpanishToolStripMenuItem.Checked = False
            FrenchToolStripMenuItem.Text = "Français"
            FrenchToolStripMenuItem.Checked = True
            ColorModeToolStripMenuItem.Text = "Mode couleur"
            AutomaticToolStripMenuItem.Text = "Automatique"
            LightToolStripMenuItem.Text = "Lumière"
            DarkToolStripMenuItem.Text = "Sombre"
            If ComboBox1.SelectedItem = "Automatic" Or ComboBox1.SelectedItem = "Automático" Or ComboBox1.SelectedItem = "Automatique" Then
                AutomaticToolStripMenuItem.Checked = True
                LightToolStripMenuItem.Checked = False
                DarkToolStripMenuItem.Checked = False
            ElseIf ComboBox1.SelectedItem = "Light" Or ComboBox1.SelectedItem = "Claro" Or ComboBox1.SelectedItem = "Lumière" Then
                AutomaticToolStripMenuItem.Checked = False
                LightToolStripMenuItem.Checked = True
                DarkToolStripMenuItem.Checked = False
            ElseIf ComboBox1.SelectedItem = "Dark" Or ComboBox1.SelectedItem = "Oscuro" Or ComboBox1.SelectedItem = "Sombre" Then
                AutomaticToolStripMenuItem.Checked = False
                LightToolStripMenuItem.Checked = False
                DarkToolStripMenuItem.Checked = True
            End If
            InstallerCreationMethodToolStripMenuItem.Text = "Méthode de création de l'installateur"
            AOTSMI.Text = "Options avancées"
            OpenToolStripMenuItem.Text = "Ouvrir"
            ExitToolStripMenuItem.Text = "Sortir"


            ' Positioning
            Label11.Left = Label10.Left + Label10.Width + 6
            Label12.Left = Label11.Left + Label11.Width
            Label16.Left = Label18.Left + Label18.Width + 6
            Label17.Left = Label16.Left + Label16.Width
            Label62.Left = Label61.Left + Label61.Width + 6
            Label63.Left = Label62.Left + Label62.Width + 4
            Label81.Left = Label80.Left + Label80.Width + 6
            Label82.Left = Label81.Left + Label81.Width + 4

            ' TabPages
            TabPage1.Text = "Aperçu"
            TabPage2.Text = "Information sur les composants"
            TabPage3.Text = "Code source"

            ' RadioButtons
            RadioButton3.Text = "Gauche"
            RadioButton4.Text = "Haut"

            ' TextBox positioning and size
            TextBox1.Left = 241
            TextBox1.Width = 339
            TextBox2.Left = 241
            TextBox2.Width = 339
            TextBox3.Left = 195
            TextBox3.Width = 483
            TextBox4.Left = 189
            TextBox4.Width = 523
            LabelText.Left = 102
            LabelText.Width = 231

            ' Miscellaneous positioning and size
            ComboBox5.Left = 342
            ComboBox5.Width = 329

            ' ComboBox relabelling
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Automatique")
            ComboBox1.Items.Add("Lumière")
            ComboBox1.Items.Add("Sombre")
            If ColorInt = 0 Then
                ComboBox1.SelectedItem = "Automatique"
            ElseIf ColorInt = 1 Then
                ComboBox1.SelectedItem = "Lumière"
            ElseIf ColorInt = 2 Then
                ComboBox1.SelectedItem = "Sombre"
            End If
            ' This line of code freezes the program. Maybe because it's cold in Alaska now?
            'ComboBox4.Items.Clear()
            ComboBox4.Items.Add("Automatique")
            ComboBox4.Items.Add("Anglais")
            ComboBox4.Items.Add("Espagnol")
            ComboBox4.Items.Add("Français")
            ComboBox4.SelectedItem = "Français"
            ComboBox4.Items.Remove("Automático")
            ComboBox4.Items.Remove("Inglés")
            ComboBox4.Items.Remove("Español")
            ComboBox4.Items.Remove("Francés")
            ComboBox4.Items.Remove("Automatic")
            ComboBox4.Items.Remove("English")
            ComboBox4.Items.Remove("Spanish")
            ComboBox4.Items.Remove("French")
            If ComboBox4.Items.Count > 4 Then   ' There is a bug in this procedure where it would make duplicate items of the ComboBox. 
                Do Until ComboBox4.Items.Count = 4  ' This fixes it
                    ComboBox4.Items.RemoveAt(4)
                Loop
            End If
            ImageFolderBrowser.Description = "Veuillez spécifier le chemin pour enregistrer l'installateur personnalisé :"
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            LangInt = 0
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
                    Text = "Instalador manual de Windows 11 (modo de administrador)"
                Else
                    Text = "Instalador manual de Windows 11"
                End If
                Notify.Text = "Instalador manual de Windows 11 - Listo"
                ' Labels
                Label1.Text = "Bienvenido"
                Label100.Text = "Instrucciones"
                Label101.Text = "Ayuda"
                Label102.Text = "Acerca de"
                Label104.Text = "Instrucciones"
                scText.Text = "El código fuente de este programa lo puede encontrar en GitHub." & CrLf & CrLf & "- Este proyecto puede ser abierto en Visual Studio 2012 y más reciente, sin necesidad de conversión" & CrLf & "- El Pack de Desarrolladores de .NET Framework 4.8 debe ser instalado para abrir este proyecto" & CrLf & CrLf & "¿Desea sugerir alguna nueva característica para ser incluida en una versión futura? ¿Ha notado algún error del que desea informar? No dude en reportar errores o (incluso mejor) contribuye al proyecto (necesita una cuenta de GitHub). Sus opiniones son cruciales." & CrLf & CrLf & "¿Desea disfrutar de las últimas características? ¡Compruebe la rama Hummingbird! Actualizaciones semanales, características nuevas y una vista previa del futuro del programa"
                Label106.Text = "Lanzamiento Hummingbird"
                Label108.Text = "Instalador de destino:"
                Label109.Text = "Ubicación del instalador de destino:"

                ' Label11 doesn't go here, as it's a ">". It will change its Left property instead.
                Label110.Text = "Fecha de creación del instalador de destino:"
                Label111.Text = "Advertencias y errores del instalador:"
                Label114.Text = "Advertencias: " & WarnCount
                Label115.Text = "Errores: " & ErrorCount

                Label12.Text = "Personalización"

                Label13.Text = "Modo de color:"
                Label14.Text = "Aquí puede cambiar la posición de la barra de navegación. Posición:"
                ' Label16 will also change its Left property
                Label17.Text = "Funcionalidad"
                Label19.Text = "Idioma:"
                Label2.Text = "Muestra información sobre el programa"
                Label20.Text = "Método de creación del instalador:"
                Label22.Text = "Compatibilidad de plataformas:"
                ' Label23, 26 and 27 aren't affected in any matter
                Label24.Text = "Aquí puede asignar una etiqueta personalizada"
                Label25.Text = "Etiqueta:"
                Label28.Text = "Escala DPI a ser aplicada: " & TrackBar1.Value & "%"
                Label3.Text = "Al menos un instalador está a punto de ser creado"
                Label30.Text = "Idioma: " & ComboBox4.SelectedItem
                Label31.Text = "Modo de color: " & ComboBox1.SelectedItem
                Label33.Text = "Método de creación del instalador: " & ComboBox5.SelectedItem
                Label34.Text = "Etiqueta: " & LabelText.Text
                LabelSetButton.PerformClick()
                Label36.Text = "Cambia las opciones de apariencia, como el idioma, el modo de color, o la escala DPI"
                Label38.Text = "Cambia configuraciones relacionadas a la creación del instalador personalizado"
                Label39.Text = "Limpia el registro del historial de instaladores, el cual es accesible haciendo clic en " & Quote & "Ver historial del instalador" & Quote & " en el menú principal"
                Label4.Text = "Acerca de"
                Label40.Text = "Limpiar registro"
                Label41.Text = "Restablece todas las preferencias a los valores predeterminados"
                Label42.Text = "Restablecer preferencias"
                Label43.Text = "Crea un instalador modificado para instalar Windows 11 en sistemas no soportados"
                Label44.Text = "Crear un instalador modificado"
                Label45.Text = "Le ayuda a personalizar la apariencia y el comportamiento del programa"
                Label47.Text = "Le ayuda a crear un instalador modificado de Windows 11 por usted mismo"
                Label48.Text = "Instrucciones"
                Label49.Text = "Aprenda a cómo usar este programa"
                Label50.Text = "Ayuda"
                Label51.Text = "Acerca de"
                Label53.Text = "Instalador de Windows 11"
                Label54.Text = "Instalador de Windows 10"
                Label55.Text = "Instalador de Windows 11:"
                Label56.Text = "Instalador de Windows 10:"
                Label57.Text = "Opciones del archivo ISO"
                Label58.Text = "Nombre del archivo ISO:"
                Label59.Text = "Ubicación del archivo ISO:"
                Label60.Text = "Cuando esté listo, haga clic en " & Quote & "Crear" & Quote
                Label63.Text = "Revise sus configuraciones"
                Label64.Text = "Ubicación y nombre:"
                Label65.Text = "Imagen de Windows 11:"
                Label66.Text = "Imagen de Windows 10:"
                Label69.Text = "Método:"
                ' Labels71 through 74 were deleted due to lack of functionality
                Label75.Text = "Compatibilidad de plataformas:"
                Label77.Text = "Etiqueta del instalador:"
                Label78.Text = LabelText.Text
                Label79.Text = "¿Estas configuraciones son las correctas?"
                Label82.Text = "Progreso"
                If InstCreateInt = 3 Then
                    Label82.Text = "Final"
                    Label83.Text = "El instalador modificado fue creado en la ubicación especificada. Por favor, lea los detalles abajo."
                Else
                    Label82.Text = "Progreso"
                    Label83.Text = "Ésta es toda la información que necesitamos en este momento. Esto tardará unos minutos, por lo que sea paciente."
                End If
                Label84.Text = "Preparando el espacio de trabajo"
                Label85.Text = "Obteniendo instrucciones"
                Label86.Text = "Extrayendo archivos de los instaladores"
                Label87.Text = "Creando el instalador"
                Label88.Text = "Finalizando"
                Label89.Text = "La acción realizada en este momento no puede ser cancelada. Por favor, espere."
                Label9.Text = "Configuración"
                If needsUpdates Then
                    Label91.Text = "Las actualizaciones aportan nuevas características y correcciones de errores al programa. Haga clic en " & Quote & "Comprobar actualizaciones" & Quote & " para comprobar actualizaciones del programa."
                Else
                    If UpdateCheckDate = Nothing Then
                        Label91.Text = "Para comprobar actualizaciones del programa, haga clic en " & Quote & "Comprobar actualizaciones" & Quote
                    Else
                        If UpdateCheckDate.Day.Equals(14) And UpdateCheckDate.Month.Equals(3) Then
                            Label91.Text = "Para comprobar actualizaciones del programa, haga clic en " & Quote & "Comprobar actualizaciones" & Quote & CrLf & "Última comprobación de actualizaciones realizada en: día π en " & UpdateCheckDate.ToLongTimeString
                        Else
                            Label91.Text = "Para comprobar actualizaciones del programa, haga clic en " & Quote & "Comprobar actualizaciones" & Quote & CrLf & "Última comprobación de actualizaciones realizada en: " & UpdateCheckDate
                        End If
                    End If
                End If
                Label92.Text = "Ésta es la información detallada de los componentes usados por el programa:"
                Label97.Text = "Todos los componentes mostrados aquí son protegidos por sus propios términos de licencia, y este programa SOLO puede redistribuir componentes de código abierto."
                AdminLabel.Text = "MODO DE ADMINISTRADOR"
                ProgramTitleLabel.Text = "Instalador manual de Windows 11"
                Last_Created_Inst_Label.Text = "Último instalador creado a las:"

                ' Labels that belong to main sections will adopt the text from the main panels
                Label10.Text = Label9.Text
                Label18.Text = Label9.Text
                Label35.Text = Label12.Text
                Label37.Text = Label17.Text
                Label52.Text = Label50.Text
                Label61.Text = Label44.Text
                Label8.Text = Label44.Text
                Label80.Text = Label44.Text
                Label15.Text = Label1.Text
                Label99.Text = Label44.Text

                ' Miscelaneous labels
                Label29.Text = Label12.Text
                Label32.Text = Label17.Text
                Label46.Text = Label9.Text
                NameLabel.Text = "Nombre: " & TextBox3.Text
                If InstHistPanel.InstallerListView.Items.Count = 0 Then
                    Last_Inst_Time_Label.Text = "Ningún dato temporal disponible"
                End If
                If File.Exists(TextBox1.Text) Then
                    Win11PresenceSTLabel.Text = "Estado de presencia: este archivo existe"
                Else
                    Win11PresenceSTLabel.Text = "Estado de presencia: este archivo no existe"
                End If
                If TextBox1.Text = "" Then
                    Win11PresenceSTLabel.Text = "Estado de presencia: desconocido"
                End If
                If File.Exists(TextBox2.Text) Then
                    Win10PresenceSTLabel.Text = "Estado de presencia: este archivo existe"
                Else
                    Win10PresenceSTLabel.Text = "Estado de presencia: este archivo no existe"
                End If
                If TextBox2.Text = "" Then
                    Win10PresenceSTLabel.Text = "Estado de presencia: desconocido"
                End If
                If TextBox4.Text.EndsWith("\") Then
                    If TextBox4.Text.Contains(" ") Then
                        ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                    Else
                        ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
                    End If
                Else
                    If TextBox4.Text.Contains(" ") Then
                        ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote & ". La ruta contendrá comillas"
                    Else
                        ImgPathLabel.Text = "La imagen será guardada en: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
                    End If
                End If

                ' LinkLabels
                LinkLabel1.Text = "Ver historial del instalador"
                LinkLabel2.Text = "Continuar creación del instalador"
                LinkLabel3.Text = "Buscar en Google"
                LinkLabel4.Text = "Buscar en DuckDuckGo"
                LinkLabel5.Text = "Cambiar nombre"
                LinkLabel6.Text = "No se pudo obtener el modelo del equipo"
                LinkLabel7.Text = "Hay actualizaciones disponibles. Haga clic aquí para saber más."
                LinkLabel8.Text = "Compruebe el proyecto"
                LinkLabel9.Text = "Descargue el Pack de Desarrolladores"
                LinkLabel10.Text = "Compruebe la página de Errores"
                LinkLabel11.Text = "Compruebe la rama Hummingbird"
                LinkLabel12.Text = "Hay algunas cosas que valen la pena revisar antes de continuar. Haga clic aquí para saber más."
                LinkLabel17.Text = "Más opciones..."
                LogViewLink.Text = "Ver archivo de registro"
                ' GroupBoxes
                GroupBox1.Text = "Posición de navegación"
                GroupBox12.Text = "Opciones de bandeja del sistema"
                GroupBox3.Text = "Detalles de compatibilidad de plataformas"
                GroupBox4.Text = "Opciones de etiqueta"
                GroupBox5.Text = "Opciones de escala DPI"
                GroupBox6.Text = "Archivos ISO"
                GroupBox7.Text = "Opciones de imagen de destino"
                GroupBox8.Text = "Imágenes"
                GroupBox9.Text = "Opciones de creación del instalador"
                GroupBox10.Text = "Registro"
                GroupBox11.Text = "Imagen de destino"

                ' Buttons
                Button1.Text = "Aplicar escala DPI"
                Button2.Text = "Examinar..."
                Button3.Text = "Restablecer"
                Button4.Text = "Examinar..."
                Button5.Text = "Examinar..."
                Button6.Text = "Crear"
                Button8.Text = "Sí"
                Button9.Text = "Ocultar registro"
                If InstCreateInt = 3 Then
                    Button10.Text = "Aceptar"
                Else
                    Button10.Text = "Cancelar"
                End If
                Button11.Text = "Ayuda de métodos"
                Button12.Text = "Comprobar actualizaciones"
                Button13.Text = "Opciones avanzadas"
                ScanButton.Text = "Escanear..."
                LabelSetButton.Text = "Establecer"
                SetDefaultButton.Text = "Predeterminado"

                ' CheckBoxes
                CheckBox1.Text = "Mostrar notificación de bandeja del sistema una vez"
                CheckBox3.Text = "Al cerrarse, ocultar en la bandeja del sistema"
                CheckBox4.Text = "Cerrar programa después de hacer clic en Aceptar"

                ' MenuStrip items
                Windows11ManualInstallerToolStripMenuItem.Text = "Instalador manual de Windows 11"
                VersionToolStripMenuItem.Text = "versión " & VerStr & " (versión de ensamblado " & AVerStr & ")"
                InstSTLabel.Text = "Estado del instalador"
                StatusTSI.Text = "No se está creando ningún instalador en este momento"
                LastInstallerCreatedAtToolStripMenuItem.Text = "Último instalador creado a las:"
                If InstHistPanel.InstallerListView.Items.Count = 0 Then
                    IHDataToolStripMenuItem.Text = "No hay datos del historial del instalador en este momento"
                End If
                ViewInstallerHistoryToolStripMenuItem.Text = "Ver historial del instalador"
                LanguageToolStripMenuItem.Text = "Idioma"
                AutomaticLanguageToolStripMenuItem.Text = "Automático"
                AutomaticLanguageToolStripMenuItem.Checked = True
                EnglishToolStripMenuItem.Text = "Inglés"
                EnglishToolStripMenuItem.Checked = False
                SpanishToolStripMenuItem.Text = "Español"
                SpanishToolStripMenuItem.Checked = False
                FrenchToolStripMenuItem.Text = "Francés"
                FrenchToolStripMenuItem.Checked = False
                ColorModeToolStripMenuItem.Text = "Modo del color"
                AutomaticToolStripMenuItem.Text = "Automático"
                LightToolStripMenuItem.Text = "Claro"
                DarkToolStripMenuItem.Text = "Oscuro"
                If ComboBox1.SelectedItem = "Automatic" Or ComboBox1.SelectedItem = "Automático" Or ComboBox1.SelectedItem = "Automatique" Then
                    AutomaticToolStripMenuItem.Checked = True
                    LightToolStripMenuItem.Checked = False
                    DarkToolStripMenuItem.Checked = False
                ElseIf ComboBox1.SelectedItem = "Light" Or ComboBox1.SelectedItem = "Claro" Or ComboBox1.SelectedItem = "Lumière" Then
                    AutomaticToolStripMenuItem.Checked = False
                    LightToolStripMenuItem.Checked = True
                    DarkToolStripMenuItem.Checked = False
                ElseIf ComboBox1.SelectedItem = "Dark" Or ComboBox1.SelectedItem = "Oscuro" Or ComboBox1.SelectedItem = "Sombre" Then
                    AutomaticToolStripMenuItem.Checked = False
                    LightToolStripMenuItem.Checked = False
                    DarkToolStripMenuItem.Checked = True
                End If
                InstallerCreationMethodToolStripMenuItem.Text = "Método de creación del instalador"
                AOTSMI.Text = "Opciones avanzadas"
                OpenToolStripMenuItem.Text = "Abrir"
                ExitToolStripMenuItem.Text = "Salir"


                ' Positioning
                Label11.Left = Label10.Left + Label10.Width + 6
                Label12.Left = Label11.Left + Label11.Width
                Label16.Left = Label18.Left + Label18.Width + 6
                Label17.Left = Label16.Left + Label16.Width
                Label62.Left = Label61.Left + Label61.Width + 6
                Label63.Left = Label62.Left + Label62.Width + 4
                Label81.Left = Label80.Left + Label80.Width + 6
                Label82.Left = Label81.Left + Label81.Width + 4

                ' TabPages
                TabPage1.Text = "General"
                TabPage2.Text = "Información de componentes"
                TabPage3.Text = "Código fuente"

                ' RadioButtons
                RadioButton3.Text = "Izquierda"
                RadioButton4.Text = "Arriba"

                ' TextBox positioning and size
                TextBox1.Left = 232
                TextBox1.Width = 348
                TextBox2.Left = 232
                TextBox2.Width = 348
                TextBox3.Left = 221
                TextBox3.Width = 457
                TextBox4.Left = 229
                TextBox4.Width = 483
                LabelText.Left = 93
                LabelText.Width = 240

                ' Miscellaneous positioning and size
                ComboBox5.Left = 326
                ComboBox5.Width = 346

                ' ComboBox relabelling
                ComboBox1.Items.Clear()
                ComboBox1.Items.Add("Automático")
                ComboBox1.Items.Add("Claro")
                ComboBox1.Items.Add("Oscuro")
                If ColorInt = 0 Then
                    ComboBox1.SelectedItem = "Automático"
                ElseIf ColorInt = 1 Then
                    ComboBox1.SelectedItem = "Oscuro"
                ElseIf ColorInt = 2 Then
                    ComboBox1.SelectedItem = "Claro"
                End If
                ' This line of code freezes the program. Maybe because it's cold in Alaska now?
                'ComboBox4.Items.Clear()
                ComboBox4.Items.Add("Automático")
                ComboBox4.Items.Add("Inglés")
                ComboBox4.Items.Add("Español")
                ComboBox4.SelectedItem = "Automático"
                ComboBox4.Items.Remove("Automatic")
                ComboBox4.Items.Remove("English")
                ComboBox4.Items.Remove("Spanish")
                If ComboBox4.Items.Count > 4 Then   ' There is a bug in this procedure where it would make duplicate items of the ComboBox. 
                    Do Until ComboBox4.Items.Count = 4  ' This fixes it
                        ComboBox4.Items.RemoveAt(4)
                    Loop
                End If
                ImageFolderBrowser.Description = "Especifique un directorio para guardar el instalador de destino:"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
                    Text = "Windows 11 Manual Installer (administrator mode)"
                Else
                    Text = "Windows 11 Manual Installer"
                End If
                Notify.Text = "Windows 11 Manual Installer - Ready"
                ' Labels
                Label1.Text = "Welcome"
                Label100.Text = "Instructions"
                Label101.Text = "Help"
                Label102.Text = "About"
                Label104.Text = "Instructions"
                scText.Text = "The source code of this program can be found on GitHub." & CrLf & CrLf & "- This project can be opened on Visual Studio 2012 and newer, without the need of conversion" & CrLf & "- The .NET Framework 4.8 Developer Pack must be installed to open this project" & CrLf & CrLf & "Do you want to suggest a new feature to be included in a future version? Have you experienced a bug you want to report? Don't hesitate on reporting feedback or (even better) contributing (you'll need a GitHub account). Your feedback is critical." & CrLf & CrLf & "Do you want to enjoy the latest features? Check the Hummingbird branch! Weekly updates, new features and a sneak peek of the program's future"
                Label106.Text = "Hummingbird release"
                Label108.Text = "Target installer:"
                Label109.Text = "Target installer path:"

                ' Label11 doesn't go here, as it's a ">". It will change its Left property instead.
                Label110.Text = "Target installer creation date:"
                Label111.Text = "Installer warnings and errors:"
                Label114.Text = "Warnings: " & WarnCount
                Label115.Text = "Errors: " & ErrorCount

                Label12.Text = "Personalization"

                Label13.Text = "Color mode:"
                Label14.Text = "Here you can change the position of the navigation bar. Position:"
                ' Label16 will also change its Left property
                Label17.Text = "Functionality"
                Label19.Text = "Language:"
                Label2.Text = "Shows program information"
                Label20.Text = "Installer creation method:"
                Label22.Text = "Platform compatibility:"
                ' Label23, 26 and 27 aren't affected in any matter
                Label24.Text = "Here you can set a custom label for your image"
                Label25.Text = "Label:"
                Label28.Text = "DPI scale to be applied: " & TrackBar1.Value & "%"
                Label3.Text = "At least one installer is about to be created"
                Label30.Text = "Language: " & ComboBox4.SelectedItem
                Label31.Text = "Color mode: " & ComboBox1.SelectedItem
                Label33.Text = "Installer creation method: " & ComboBox5.SelectedItem
                Label34.Text = "Label: " & LabelText.Text
                LabelSetButton.PerformClick()
                Label36.Text = "Change appearance settings, such as the language, the color mode, or the DPI scale"
                Label38.Text = "Change settings related to how the program makes the custom installer"
                Label39.Text = "Clears the installer history log, which is accessible by clicking " & Quote & "View installer history" & Quote & " on the main menu"
                Label4.Text = "About"
                Label40.Text = "Clear log"
                Label41.Text = "Resets all the preferences to their default values"
                Label42.Text = "Reset preferences"
                Label43.Text = "Creates a modified installer to install Windows 11 on unsupported systems"
                Label44.Text = "Create a custom installer"
                Label45.Text = "Allows you to customize the program's appearance or behavior"
                Label47.Text = "Allows you to create a custom Windows 11 installer by yourself"
                Label48.Text = "Instructions"
                Label49.Text = "Learn how to use this program"
                Label50.Text = "Help"
                Label51.Text = "About"
                Label53.Text = "Windows 11 installer"
                Label54.Text = "Windows 10 installer"
                Label55.Text = "Windows 11 installer:"
                Label56.Text = "Windows 10 installer:"
                Label57.Text = "ISO file options"
                Label58.Text = "ISO file name:"
                Label59.Text = "ISO file location:"
                Label60.Text = "When you are ready, click " & Quote & "Create" & Quote
                Label63.Text = "Review your settings"
                Label64.Text = "Location and name:"
                Label65.Text = "Windows 11 image:"
                Label66.Text = "Windows 10 image:"
                Label69.Text = "Method:"
                ' Labels71 through 74 were deleted due to lack of functionality
                Label75.Text = "Platform compatibility:"
                Label77.Text = "Installer label:"
                Label78.Text = LabelText.Text
                Label79.Text = "Are these settings OK?"
                Label82.Text = "Progress"
                If InstCreateInt = 3 Then
                    Label82.Text = "Finish"
                    Label83.Text = "The custom installer was created at the specified location. Please read the details below."
                Else
                    Label82.Text = "Progress"
                    Label83.Text = "That's all the information we need right now. The installer creation will take a few minutes, so please be patient."
                End If
                Label84.Text = "Preparing the workspace"
                Label85.Text = "Gathering instructions"
                Label86.Text = "Extracting installer files"
                Label87.Text = "Creating the installer"
                Label88.Text = "Finishing up"
                Label89.Text = "The action performed right now cannot be cancelled. Please wait."
                Label9.Text = "Settings"
                If needsUpdates Then
                    Label91.Text = "Updates bring new features and bugfixes to this program. Click " & Quote & "Check for updates" & Quote & " to check for program updates."
                Else
                    If UpdateCheckDate = Nothing Then
                        Label91.Text = "To check for any program updates, click " & Quote & "Check for updates" & Quote
                    Else
                        If UpdateCheckDate.Day.Equals(14) And UpdateCheckDate.Month.Equals(3) Then
                            Label91.Text = "To check for any program updates, click " & Quote & "Check for updates" & Quote & CrLf & "Last update check performed on: π day at " & UpdateCheckDate.ToLongTimeString
                        Else
                            Label91.Text = "To check for any program updates, click " & Quote & "Check for updates" & Quote & CrLf & "Last update check performed on: " & UpdateCheckDate
                        End If
                    End If
                End If
                Label92.Text = "Here is detailed information for components used by the program:"
                Label97.Text = "All components shown herein are covered by their own license terms, and this program can ONLY redistribute open-source components."
                AdminLabel.Text = "ADMINISTRATOR MODE"
                ProgramTitleLabel.Text = "Windows 11 Manual Installer"
                Last_Created_Inst_Label.Text = "Last installer created on:"

                ' Labels that belong to main sections will adopt the text from the main panels
                Label10.Text = Label9.Text
                Label18.Text = Label9.Text
                Label35.Text = Label12.Text
                Label37.Text = Label17.Text
                Label52.Text = Label50.Text
                Label61.Text = Label44.Text
                Label8.Text = Label44.Text
                Label80.Text = Label44.Text
                Label15.Text = Label1.Text
                Label99.Text = Label44.Text

                ' Miscelaneous labels
                Label29.Text = Label12.Text
                Label32.Text = Label17.Text
                Label46.Text = Label9.Text
                NameLabel.Text = "Name: " & TextBox3.Text
                If InstHistPanel.InstallerListView.Items.Count = 0 Then
                    Last_Inst_Time_Label.Text = "No time data is available"
                End If
                If File.Exists(TextBox1.Text) Then
                    Win11PresenceSTLabel.Text = "Presence status: this file exists"
                Else
                    Win11PresenceSTLabel.Text = "Presence status: this file does not exist"
                End If
                If TextBox1.Text = "" Then
                    Win11PresenceSTLabel.Text = "Presence status: unknown"
                End If
                If File.Exists(TextBox2.Text) Then
                    Win10PresenceSTLabel.Text = "Presence status: this file exists"
                Else
                    Win10PresenceSTLabel.Text = "Presence status: this file does not exist"
                End If
                If TextBox2.Text = "" Then
                    Win10PresenceSTLabel.Text = "Presence status: unknown"
                End If
                If TextBox4.Text.EndsWith("\") Then
                    If TextBox4.Text.Contains(" ") Then
                        ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                    Else
                        ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
                    End If
                Else
                    If TextBox4.Text.Contains(" ") Then
                        ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes"
                    Else
                        ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
                    End If
                End If

                ' LinkLabels
                LinkLabel1.Text = "View installer history"
                LinkLabel2.Text = "Continue installer creation"
                LinkLabel3.Text = "Search on Google"
                LinkLabel4.Text = "Search on DuckDuckGo"
                LinkLabel5.Text = "Rename"
                LinkLabel6.Text = "Could not get computer model"
                LinkLabel7.Text = "Updates are available. Click here to learn more."
                LinkLabel8.Text = "Check this program's project"
                LinkLabel9.Text = "Download the Developer Pack"
                LinkLabel10.Text = "Check the Issues page"
                LinkLabel11.Text = "Check the Hummingbird branch"
                LinkLabel12.Text = "There are some things that are worth revising before continuing. Click here to learn more."
                LinkLabel17.Text = "More options..."
                LogViewLink.Text = "View log file"

                ' GroupBoxes
                GroupBox1.Text = "Navigation position"
                GroupBox12.Text = "System tray options"
                GroupBox3.Text = "Platform compatibility details"
                GroupBox4.Text = "Label options"
                GroupBox5.Text = "DPI scaling options"
                GroupBox6.Text = "ISO files"
                GroupBox7.Text = "Target image options"
                GroupBox8.Text = "Images"
                GroupBox9.Text = "Installer creation options"
                GroupBox10.Text = "Log"
                GroupBox11.Text = "Target image"

                ' Buttons
                Button1.Text = "Apply DPI scale"
                Button2.Text = "Browse..."
                Button3.Text = "Restore"
                Button4.Text = "Browse..."
                Button5.Text = "Browse..."
                Button6.Text = "Create"
                Button8.Text = "Yes"
                Button9.Text = "Hide log"
                If InstCreateInt = 3 Then
                    Button10.Text = "OK"
                Else
                    Button10.Text = "Cancel"
                End If
                Button11.Text = "Method help"
                Button12.Text = "Check for updates"
                Button13.Text = "Advanced options"
                ScanButton.Text = "Scan..."
                LabelSetButton.Text = "Set"
                SetDefaultButton.Text = "Set default"

                ' CheckBoxes
                CheckBox1.Text = "Show system tray notification once"
                CheckBox3.Text = "When closing, hide in system tray"
                CheckBox4.Text = "Exit the program after I click OK"

                ' MenuStrip items
                Windows11ManualInstallerToolStripMenuItem.Text = "Windows 11 Manual Installer"
                VersionToolStripMenuItem.Text = "version " & VerStr & " (assembly version " & AVerStr & ")"
                InstSTLabel.Text = "Installer status"
                StatusTSI.Text = "No installers are being created at this time"
                LastInstallerCreatedAtToolStripMenuItem.Text = "Last installer created at:"
                If InstHistPanel.InstallerListView.Items.Count = 0 Then
                    IHDataToolStripMenuItem.Text = "No installer history data is available at this time"
                End If
                ViewInstallerHistoryToolStripMenuItem.Text = "View installer history"
                LanguageToolStripMenuItem.Text = "Language"
                AutomaticLanguageToolStripMenuItem.Text = "Automatic"
                AutomaticLanguageToolStripMenuItem.Checked = True
                EnglishToolStripMenuItem.Text = "English"
                EnglishToolStripMenuItem.Checked = False
                SpanishToolStripMenuItem.Text = "Spanish"
                SpanishToolStripMenuItem.Checked = False
                FrenchToolStripMenuItem.Text = "French"
                FrenchToolStripMenuItem.Checked = False
                ColorModeToolStripMenuItem.Text = "Color mode"
                AutomaticToolStripMenuItem.Text = "Automatic"
                LightToolStripMenuItem.Text = "Light"
                DarkToolStripMenuItem.Text = "Dark"
                If ComboBox1.SelectedItem = "Automatic" Or ComboBox1.SelectedItem = "Automático" Or ComboBox1.SelectedItem = "Automatique" Then
                    AutomaticToolStripMenuItem.Checked = True
                    LightToolStripMenuItem.Checked = False
                    DarkToolStripMenuItem.Checked = False
                ElseIf ComboBox1.SelectedItem = "Light" Or ComboBox1.SelectedItem = "Claro" Or ComboBox1.SelectedItem = "Lumière" Then
                    AutomaticToolStripMenuItem.Checked = False
                    LightToolStripMenuItem.Checked = True
                    DarkToolStripMenuItem.Checked = False
                ElseIf ComboBox1.SelectedItem = "Dark" Or ComboBox1.SelectedItem = "Oscuro" Or ComboBox1.SelectedItem = "Sombre" Then
                    AutomaticToolStripMenuItem.Checked = False
                    LightToolStripMenuItem.Checked = False
                    DarkToolStripMenuItem.Checked = True
                End If
                InstallerCreationMethodToolStripMenuItem.Text = "Installer creation method"
                AOTSMI.Text = "Advanced options"
                OpenToolStripMenuItem.Text = "Open"
                ExitToolStripMenuItem.Text = "Exit"


                ' Positioning
                Label11.Left = Label10.Left + Label10.Width + 6
                Label12.Left = Label11.Left + Label11.Width
                Label16.Left = Label18.Left + Label18.Width + 6
                Label17.Left = Label16.Left + Label16.Width
                Label62.Left = Label61.Left + Label61.Width + 6
                Label63.Left = Label62.Left + Label62.Width + 4
                Label81.Left = Label80.Left + Label80.Width + 6
                Label82.Left = Label81.Left + Label81.Width + 4

                ' TabPages
                TabPage1.Text = "Overview"
                TabPage2.Text = "Component information"
                TabPage3.Text = "Source code"

                ' RadioButtons
                RadioButton3.Text = "Left"
                RadioButton4.Text = "Top"

                ' TextBox positioning and size
                TextBox1.Left = 200
                TextBox1.Width = 380
                TextBox2.Left = 200
                TextBox2.Width = 380
                TextBox3.Left = 157
                TextBox3.Width = 521
                TextBox4.Left = 167
                TextBox4.Width = 545
                LabelText.Left = 77
                LabelText.Width = 256

                ' Miscellaneous positioning and size
                ComboBox5.Left = 268
                ComboBox5.Width = 403

                ' ComboBox relabelling
                ComboBox1.Items.Clear()
                ComboBox1.Items.Add("Automatic")
                ComboBox1.Items.Add("Light")
                ComboBox1.Items.Add("Dark")
                If ColorInt = 0 Then
                    ComboBox1.SelectedItem = "Automatic"
                ElseIf ColorInt = 1 Then
                    ComboBox1.SelectedItem = "Light"
                ElseIf ColorInt = 2 Then
                    ComboBox1.SelectedItem = "Dark"
                End If
                ' This line of code freezes the program. Maybe because it's cold in Alaska now?
                'ComboBox4.Items.Clear()
                ComboBox4.Items.Add("Automatic")
                ComboBox4.Items.Add("English")
                ComboBox4.Items.Add("Spanish")
                ComboBox4.SelectedItem = "Automatic"
                ComboBox4.Items.Remove("Automático")
                ComboBox4.Items.Remove("Inglés")
                ComboBox4.Items.Remove("Español")
                If ComboBox4.Items.Count > 4 Then   ' There is a bug in this procedure where it would make duplicate items of the ComboBox. 
                    Do Until ComboBox4.Items.Count = 4  ' This fixes it
                        ComboBox4.Items.RemoveAt(4)
                    Loop
                End If
                ImageFolderBrowser.Description = "Please specify the path to save the custom installer:"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
                    Text = "Installateur manuel de Windows 11 (mode administrateur)"
                Else
                    Text = "Installateur manuel de Windows 11"
                End If
                Notify.Text = "Installateur manuel de Windows 11 - Prêt"
                ' Labels
                Label1.Text = "Bienvenue"
                Label100.Text = "Instructions"
                Label101.Text = "Aide"
                Label102.Text = "À propos de"
                Label104.Text = "Instructions"
                scText.Text = "Le code source de ce programme peut être trouvé sur GitHub." & CrLf & CrLf & "- Ce projet peut être ouvert sur Visual Studio 2012 et plus récent, sans besoin de conversion." & CrLf & "- Le .NET Framework 4.8 Developer Pack doit être installé pour ouvrir ce projet." & CrLf & CrLf & "Vous souhaitez suggérer une nouvelle fonctionnalité à inclure dans une future version ? Vous avez rencontré un bogue que vous souhaitez signaler ? N'hésitez pas à faire part de vos commentaires ou (encore mieux) à contribuer (vous aurez besoin d'un compte GitHub). Vos commentaires sont essentiels." & CrLf & CrLf & "Vous voulez profiter des dernières fonctionnalités ? Consultez la branche Hummingbird ! Mises à jour hebdomadaires, nouvelles fonctionnalités et aperçu de l'avenir du programme."
                Label106.Text = "Hummingbird version"
                Label108.Text = "Installateur cible :"
                Label109.Text = "Chemin de l'installateur cible :"

                ' Label11 doesn't go here, as it's a ">". It will change its Left property instead.
                Label110.Text = "Date de création de l'installateur cible :"
                Label111.Text = "Avertissements et erreurs de l'installateur :"
                Label114.Text = "Avertissements : " & WarnCount
                Label115.Text = "Erreurs : " & ErrorCount

                Label12.Text = "Personnalisation"

                Label13.Text = "Mode couleur :"
                Label14.Text = "Vous pouvez modifier ici la position de la barre de navigation. Position :"
                ' Label16 will also change its Left property
                Label17.Text = "Fonctionnalité"
                Label19.Text = "Langue:"
                Label2.Text = "Affiche des informations sur le programme"
                Label20.Text = "Méthode de création de l'installateur :"
                Label22.Text = "Compatibilité avec les plates-formes :"
                ' Label23, 26 and 27 aren't affected in any matter
                Label24.Text = "Vous pouvez définir ici une étiquette personnalisée pour votre image."
                Label25.Text = "Étiquette :"
                Label28.Text = "Échelle DPI à appliquer: " & TrackBar1.Value & "%"
                Label3.Text = "Au moins un installateur est sur le point d'être créé"
                Label30.Text = "Langue: " & ComboBox4.SelectedItem
                Label31.Text = "Mode couleur: " & ComboBox1.SelectedItem
                Label33.Text = "Méthode de création de l'installateur : " & ComboBox5.SelectedItem
                Label34.Text = "Étiquette : " & LabelText.Text
                LabelSetButton.PerformClick()
                Label36.Text = "Modifiez les paramètres d'apparence, tels que la langue, le mode de couleur ou l'échelle DPI"
                Label38.Text = "Modifier les paramètres relatifs à la façon dont le programme réalise l'installateur personnalisé"
                Label39.Text = "Efface le journal de l'historique de l'installateur, qui est accessible en cliquant sur " & Quote & "Voir l'historique de l'installateur" & Quote & " dans le menu principal."
                Label4.Text = "À propos de"
                Label40.Text = "Effacer le journal"
                Label41.Text = "Réinitialise toutes les préférences à leurs valeurs par défaut"
                Label42.Text = "Réinitialiser les préférences"
                Label43.Text = "Créer un installateur modifié pour installer Windows 11 sur des systèmes non pris en charge"
                Label44.Text = "Créer un installateur personnalisé"
                Label45.Text = "Permet de personnaliser l'apparence ou le comportement du programme"
                Label47.Text = "Permet de créer soi-même un installateur personnalisé de Windows 11"
                Label48.Text = "Instructions"
                Label49.Text = "Apprenez à utiliser ce programme"
                ' These lines of code affect Label5 and PictureBox11
                Label50.Text = "Aide"
                Label51.Text = "À propos de"
                Label53.Text = "Installateur de Windows 11"
                Label54.Text = "Installateur de Windows 10"
                Label55.Text = "Installateur de Windows 11 :"
                Label56.Text = "Installateur de Windows 10 :"
                Label57.Text = "Options du fichier ISO"
                Label58.Text = "Nom du fichier ISO :"
                Label59.Text = "Lieu du fichier ISO :"
                Label60.Text = "Lorsque vous êtes prêt, cliquez sur " & Quote & "Créer" & Quote
                Label63.Text = "Vérifiez vos paramètres"
                Label64.Text = "Lieu et nom :"
                Label65.Text = "Image du Windows 11 :"
                Label66.Text = "Image du Windows 10 :"
                Label69.Text = "Méthode :"
                ' Labels71 through 74 were deleted due to lack of functionality
                Label75.Text = "Compatibilité avec les plates-formes :"
                Label77.Text = "Étiquette de l'installateur :"
                Label78.Text = LabelText.Text
                Label79.Text = "Ces paramètres sont-ils corrects ?"
                If InstCreateInt = 3 Then
                    Label82.Text = "Fin"
                    Label83.Text = "Le installateur personnalisé a été créé à l'emplacement spécifié. Veuillez lire les détails ci-dessous."
                Else
                    Label82.Text = "Progrès"
                    Label83.Text = "C'est toute l'information dont nous avons besoin pour le moment. La création de l'installateur prendra quelques minutes, alors soyez patient."
                End If
                Label84.Text = "Préparation de l'espace de travail"
                Label85.Text = "Rassemblement des instructions"
                Label86.Text = "Extraction des fichiers d'installation"
                Label87.Text = "Création de l'installateur"
                Label88.Text = "Finir"
                Label89.Text = "L'action effectuée en ce moment ne peut être annulée. Veuillez patienter..."
                Label9.Text = "Paramètres"
                If needsUpdates Then
                    Label91.Text = "Les mises à jour apportent de nouvelles fonctionnalités et des corrections de bogues à ce programme. Cliquez sur " & Quote & "Vérifier les mises à jour" & Quote & " pour vérifier les mises à jour du programme."
                Else
                    If UpdateCheckDate = Nothing Then
                        Label91.Text = "Pour vérifier les mises à jour du programme, cliquez sur " & Quote & "Vérifier les mises à jour" & Quote
                    Else
                        If UpdateCheckDate.Day.Equals(14) And UpdateCheckDate.Month.Equals(3) Then
                            Label91.Text = "Pour vérifier les mises à jour du programme, cliquez sur " & Quote & "Vérifier les mises à jour" & Quote & CrLf & "Dernière vérification des mises à jour effectuée le : π jour à " & UpdateCheckDate.ToLongTimeString
                        Else
                            Label91.Text = "Pour vérifier les mises à jour du programme, cliquez sur " & Quote & "Vérifier les mises à jour" & Quote & CrLf & "Dernière vérification des mises à jour effectuée le : " & UpdateCheckDate
                        End If
                    End If
                End If
                Label92.Text = "Vous trouverez ici des informations détaillées sur les composants utilisés par le programme :"
                Label97.Text = "Tous les composants présentés ici sont couverts par leurs propres termes de licence, et ce programme peut UNIQUEMENT redistribuer redistribuer des composants open-source."
                AdminLabel.Text = "MODE ADMINISTRATEUR"
                ProgramTitleLabel.Text = "Installateur manuel de Windows 11"
                Last_Created_Inst_Label.Text = "Dernier installateur créé le :"

                ' Labels that belong to main sections will adopt the text from the main panels
                Label10.Text = Label9.Text
                Label18.Text = Label9.Text
                Label35.Text = Label12.Text
                Label37.Text = Label17.Text
                Label52.Text = Label50.Text
                Label61.Text = Label44.Text
                Label8.Text = Label44.Text
                Label80.Text = Label44.Text
                Label15.Text = Label1.Text
                Label99.Text = Label44.Text

                ' Miscelaneous labels
                Label29.Text = Label12.Text
                Label32.Text = Label17.Text
                Label46.Text = Label9.Text
                NameLabel.Text = "Nom : " & TextBox3.Text
                If InstHistPanel.InstallerListView.Items.Count = 0 Then
                    Last_Inst_Time_Label.Text = "Aucune date n'est disponible"
                End If
                If File.Exists(TextBox1.Text) Then
                    Win11PresenceSTLabel.Text = "Statut de présence : ce fichier existe"
                Else
                    Win11PresenceSTLabel.Text = "Statut de présence : ce fichier n'existe pas"
                End If
                If TextBox1.Text = "" Then
                    Win11PresenceSTLabel.Text = "Statut de présence : inconnu"
                End If
                If File.Exists(TextBox2.Text) Then
                    Win10PresenceSTLabel.Text = "Statut de présence : ce fichier existe"
                Else
                    Win10PresenceSTLabel.Text = "Statut de présence : ce fichier n'existe pas"
                End If
                If TextBox2.Text = "" Then
                    Win10PresenceSTLabel.Text = "Statut de présence : inconnu"
                End If
                If TextBox4.Text.EndsWith("\") Then
                    If TextBox4.Text.Contains(" ") Then
                        ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                    Else
                        ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
                    End If
                Else
                    If TextBox4.Text.Contains(" ") Then
                        ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote & ". Le chemin contiendra des guillemets"
                    Else
                        ImgPathLabel.Text = "L'image sera enregistrée sur : " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
                    End If
                End If

                ' LinkLabels
                LinkLabel1.Text = "Voir l'historique de l'installateur"
                LinkLabel2.Text = "Continuer la creation de l'installateur"
                LinkLabel3.Text = "Recherche sur Google"
                LinkLabel4.Text = "Recherche sur DuckDuckGo"
                LinkLabel5.Text = "Renommer"
                LinkLabel6.Text = "Impossible d'obtenir le modèle d'ordinateur"
                LinkLabel7.Text = "Les mises à jour sont disponibles. Cliquez ici pour en savoir plus."
                LinkLabel8.Text = "Vérifiez le projet de ce programme"
                LinkLabel9.Text = "Télécharger le Developer Pack"
                LinkLabel10.Text = "Vérifiez la page des problèmes"
                LinkLabel11.Text = "Vérifiez la branche Hummingbird"
                LinkLabel12.Text = "Il y a certaines choses qui méritent d'être révisées avant de continuer. Cliquez ici pour en savoir plus."
                LinkLabel17.Text = "Options supplémentaires..."
                LogViewLink.Text = "Afficher le fichier journal"

                ' GroupBoxes
                GroupBox1.Text = "Position de la navigation"
                GroupBox12.Text = "Options de la barre d'état système"
                GroupBox3.Text = "Détails de la compatibilité avec la plate-forme"
                GroupBox4.Text = "Options de l'étiquette"
                GroupBox5.Text = "Options de mise à l'échelle DPI"
                GroupBox6.Text = "Fichiers ISO"
                GroupBox7.Text = "Options de l'image cible"
                GroupBox8.Text = "Images"
                GroupBox9.Text = "Options de création de l'installateur"
                GroupBox10.Text = "Journal"
                GroupBox11.Text = "Image cible"

                ' Buttons
                Button1.Text = "Appliquer l'échelle DPI"
                Button2.Text = "Parcourir..."
                Button3.Text = "Restaurer"
                Button4.Text = "Parcourir..."
                Button5.Text = "Parcourir..."
                Button6.Text = "Créer"
                Button8.Text = "Oui"
                Button9.Text = "Cacher le journal"
                If InstCreateInt = 3 Then
                    Button10.Text = "OK"
                Else
                    Button10.Text = "Annuler"
                End If
                Button11.Text = "Aide à la méthode"
                Button12.Text = "Vérifier les mises à jour"
                Button13.Text = "Options avancées"
                ScanButton.Text = "Scanner..."
                LabelSetButton.Text = "Définir"
                SetDefaultButton.Text = "Valeur par défaut"

                ' CheckBoxes
                CheckBox1.Text = "Afficher une fois la notification de la barre d'état système"
                CheckBox3.Text = "À la fermeture, masquer dans la barre d'état système"
                CheckBox4.Text = "Quitter le programme après avoir cliqué sur OK"

                ' MenuStrip items
                Windows11ManualInstallerToolStripMenuItem.Text = "Installateur manuel de Windows 11"
                VersionToolStripMenuItem.Text = "version " & VerStr & " (version assemblée " & AVerStr & ")"
                InstSTLabel.Text = "Statut de l'installateur"
                StatusTSI.Text = "Aucun installateur n'est créé pour l'instant"
                LastInstallerCreatedAtToolStripMenuItem.Text = "Dernier installateur créé à :"
                If InstHistPanel.InstallerListView.Items.Count = 0 Then
                    IHDataToolStripMenuItem.Text = "Aucune donnée sur l'historique des installateurs n'est disponible pour l'instant"
                End If
                ViewInstallerHistoryToolStripMenuItem.Text = "Voir l'historique de l'installateur"
                LanguageToolStripMenuItem.Text = "Langue"
                AutomaticLanguageToolStripMenuItem.Text = "Automatique"
                AutomaticLanguageToolStripMenuItem.Checked = False
                EnglishToolStripMenuItem.Text = "Anglais"
                EnglishToolStripMenuItem.Checked = False
                SpanishToolStripMenuItem.Text = "Espagnol"
                SpanishToolStripMenuItem.Checked = False
                FrenchToolStripMenuItem.Text = "Français"
                FrenchToolStripMenuItem.Checked = True
                ColorModeToolStripMenuItem.Text = "Mode couleur"
                AutomaticToolStripMenuItem.Text = "Automatique"
                LightToolStripMenuItem.Text = "Lumière"
                DarkToolStripMenuItem.Text = "Sombre"
                If ComboBox1.SelectedItem = "Automatic" Or ComboBox1.SelectedItem = "Automático" Or ComboBox1.SelectedItem = "Automatique" Then
                    AutomaticToolStripMenuItem.Checked = True
                    LightToolStripMenuItem.Checked = False
                    DarkToolStripMenuItem.Checked = False
                ElseIf ComboBox1.SelectedItem = "Light" Or ComboBox1.SelectedItem = "Claro" Or ComboBox1.SelectedItem = "Lumière" Then
                    AutomaticToolStripMenuItem.Checked = False
                    LightToolStripMenuItem.Checked = True
                    DarkToolStripMenuItem.Checked = False
                ElseIf ComboBox1.SelectedItem = "Dark" Or ComboBox1.SelectedItem = "Oscuro" Or ComboBox1.SelectedItem = "Sombre" Then
                    AutomaticToolStripMenuItem.Checked = False
                    LightToolStripMenuItem.Checked = False
                    DarkToolStripMenuItem.Checked = True
                End If
                InstallerCreationMethodToolStripMenuItem.Text = "Méthode de création de l'installateur"
                AOTSMI.Text = "Options avancées"
                OpenToolStripMenuItem.Text = "Ouvrir"
                ExitToolStripMenuItem.Text = "Sortir"


                ' Positioning
                Label11.Left = Label10.Left + Label10.Width + 6
                Label12.Left = Label11.Left + Label11.Width
                Label16.Left = Label18.Left + Label18.Width + 6
                Label17.Left = Label16.Left + Label16.Width
                Label62.Left = Label61.Left + Label61.Width + 6
                Label63.Left = Label62.Left + Label62.Width + 4
                Label81.Left = Label80.Left + Label80.Width + 6
                Label82.Left = Label81.Left + Label81.Width + 4

                ' TabPages
                TabPage1.Text = "Aperçu"
                TabPage2.Text = "Information sur les composants"
                TabPage3.Text = "Code source"

                ' RadioButtons
                RadioButton3.Text = "Gauche"
                RadioButton4.Text = "Haut"

                ' TextBox positioning and size
                TextBox1.Left = 241
                TextBox1.Width = 339
                TextBox2.Left = 241
                TextBox2.Width = 339
                TextBox3.Left = 195
                TextBox3.Width = 483
                TextBox4.Left = 189
                TextBox4.Width = 523
                LabelText.Left = 77
                LabelText.Width = 256

                ' Miscellaneous positioning and size
                ComboBox5.Left = 342
                ComboBox5.Width = 329

                ' ComboBox relabelling
                ComboBox1.Items.Clear()
                ComboBox1.Items.Add("Automatique")
                ComboBox1.Items.Add("Lumière")
                ComboBox1.Items.Add("Sombre")
                If ColorInt = 0 Then
                    ComboBox1.SelectedItem = "Automatique"
                ElseIf ColorInt = 1 Then
                    ComboBox1.SelectedItem = "Lumière"
                ElseIf ColorInt = 2 Then
                    ComboBox1.SelectedItem = "Sombre"
                End If
                ' This line of code freezes the program. Maybe because it's cold in Alaska now?
                'ComboBox4.Items.Clear()
                ComboBox4.Items.Add("Automatique")
                ComboBox4.Items.Add("Anglais")
                ComboBox4.Items.Add("Espagnol")
                ComboBox4.Items.Add("Français")
                ComboBox4.SelectedItem = "Français"
                ComboBox4.Items.Remove("Automático")
                ComboBox4.Items.Remove("Inglés")
                ComboBox4.Items.Remove("Español")
                ComboBox4.Items.Remove("Francés")
                ComboBox4.Items.Remove("Automatic")
                ComboBox4.Items.Remove("English")
                ComboBox4.Items.Remove("Spanish")
                ComboBox4.Items.Remove("French")
                If ComboBox4.Items.Count > 4 Then   ' There is a bug in this procedure where it would make duplicate items of the ComboBox. 
                    Do Until ComboBox4.Items.Count = 4  ' This fixes it
                        ComboBox4.Items.RemoveAt(4)
                    Loop
                End If
                ImageFolderBrowser.Description = "Veuillez spécifier le chemin pour enregistrer l'installateur personnalisé :"
            End If
        End If
        UpdatePanelProperties()
    End Sub

    Private Sub DebugPic_Click(sender As Object, e As EventArgs) Handles DebugPic.Click
        BringToFront()
        BackSubPanel.Show()
        DebugPanel.ShowDialog()
        DebugPanel.Visible = True
        DebugPanel.Visible = False
        BringToFront()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        UpdateCheckDate = Now
        Label91.Left = 96
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            Label91.Text = "Checking for updates..."
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            Label91.Text = "Comprobando actualizaciones..."
        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
            Label91.Text = "Vérifier les mises à jour..."
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label91.Text = "Checking for updates..."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label91.Text = "Comprobando actualizaciones..."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label91.Text = "Vérifier les mises à jour..."
            End If
        End If
        ProgressRingPic.Visible = True
        If My.Computer.Network.IsAvailable = False Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                MsgBox("You need to connect to a network to check for updates. Please connect your system to a network and try again", vbOKOnly + vbInformation, "Update check error")
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                MsgBox("Necesita conectarse a una red para comprobar actualizaciones. Por favor, conecte su sistema a una red e inténtelo de nuevo.", vbOKOnly + vbInformation, "Error de comprobación de actualizaciones")
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                MsgBox("Vous devez vous connecter à un réseau pour vérifier les mises à jour. Veuillez connecter votre système à un réseau et réessayer", vbOKOnly + vbInformation, "Erreur de la vérification de la mise à jour")
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    MsgBox("You need to connect to a network to check for updates. Please connect your system to a network and try again", vbOKOnly + vbInformation, "Update check error")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    MsgBox("Necesita conectarse a una red para comprobar actualizaciones. Por favor, conecte su sistema a una red e inténtelo de nuevo.", vbOKOnly + vbInformation, "Error de comprobación de actualizaciones")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    MsgBox("Vous devez vous connecter à un réseau pour vérifier les mises à jour. Veuillez connecter votre système à un réseau et réessayer", vbOKOnly + vbInformation, "Erreur de la vérification de la mise à jour")
                End If
            End If
        Else
            If File.Exists(".\latest") Then
                UpdateChoicePanel.TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\version")
                File.Move(".\latest", ".\latest_old")
                File.SetAttributes(".\latest_old", FileAttributes.Hidden)
                Using LATEST As New WebClient()
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                    Try
                        LATEST.DownloadFile("https://raw.githubusercontent.com/CodingWonders/win11minst/hummingbird/latest", ".\latest")
                        File.Delete(".\latest_old")
                    Catch ex As Exception
                        If File.Exists(".\latest") Then
                            File.Delete(".\latest")
                        End If
                        File.SetAttributes(".\latest_old", FileAttributes.Normal)
                        File.Move(".\latest_old", ".\latest")
                    End Try
                End Using
                UpdateChoicePanel.TextBox2.Text = My.Computer.FileSystem.ReadAllText(".\latest")
                If UpdateChoicePanel.TextBox1.Text = UpdateChoicePanel.TextBox2.Text Then
                    If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                        MsgBox("No updates available", vbOKOnly + vbInformation, "Update check")
                    ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                        MsgBox("No hay actualizaciones disponibles", vbOKOnly + vbInformation, "Comprobación de actualizaciones")
                    ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                        MsgBox("Aucune mise à jour disponible", vbOKOnly + vbInformation, "Vérification de la mise à jour")
                    ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                            MsgBox("No updates available", vbOKOnly + vbInformation, "Update check")
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                            MsgBox("No hay actualizaciones disponibles", vbOKOnly + vbInformation, "Comprobación de actualizaciones")
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                            MsgBox("Aucune mise à jour disponible", vbOKOnly + vbInformation, "Vérification de la mise à jour")
                        End If
                    End If
                    Label91.Visible = True
                    LinkLabel7.Visible = False
                Else
                    Label91.Visible = False
                    LinkLabel7.Visible = True
                End If
            Else
                Using LATEST As New WebClient()
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                    Try
                        LATEST.DownloadFile("https://raw.githubusercontent.com/CodingWonders/win11minst/hummingbird/latest", ".\latest")
                        UpdateChoicePanel.TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\version")
                        UpdateChoicePanel.TextBox2.Text = My.Computer.FileSystem.ReadAllText(".\latest")
                        If UpdateChoicePanel.TextBox1.Text = UpdateChoicePanel.TextBox2.Text Then
                            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                                MsgBox("No updates available", vbOKOnly + vbInformation, "Update check")
                            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                                MsgBox("No hay actualizaciones disponibles", vbOKOnly + vbInformation, "Comprobación de actualizaciones")
                            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                                MsgBox("Aucune mise à jour disponible", vbOKOnly + vbInformation, "Vérification de la mise à jour")
                            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                    MsgBox("No updates available", vbOKOnly + vbInformation, "Update check")
                                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                    MsgBox("No hay actualizaciones disponibles", vbOKOnly + vbInformation, "Comprobación de actualizaciones")
                                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                                    MsgBox("Aucune mise à jour disponible", vbOKOnly + vbInformation, "Vérification de la mise à jour")
                                End If
                            End If
                            Label91.Visible = True
                            LinkLabel7.Visible = False
                        Else
                            Label91.Visible = False
                            LinkLabel7.Visible = True
                        End If
                    Catch ex As Exception
                        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                            MsgBox("No updates available", vbOKOnly + vbInformation, "Update check")
                        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                            MsgBox("No hay actualizaciones disponibles", vbOKOnly + vbInformation, "Comprobación de actualizaciones")
                        ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                            MsgBox("Aucune mise à jour disponible", vbOKOnly + vbInformation, "Vérification de la mise à jour")
                        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                MsgBox("No updates available", vbOKOnly + vbInformation, "Update check")
                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                MsgBox("No hay actualizaciones disponibles", vbOKOnly + vbInformation, "Comprobación de actualizaciones")
                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                                MsgBox("Aucune mise à jour disponible", vbOKOnly + vbInformation, "Vérification de la mise à jour")
                            End If
                        End If
                        Label91.Visible = True
                        LinkLabel7.Visible = False
                    End Try
                End Using
            End If
        End If
        ProgressRingPic.Visible = False
        Label91.Left = 57
        If UpdateCheckDate.Day.Equals(14) And UpdateCheckDate.Month.Equals(3) Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                Label91.Text = "To check for any program updates, click " & Quote & "Check for updates" & Quote & CrLf & "Last update check performed on: π day at " & UpdateCheckDate.ToLongTimeString
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                Label91.Text = "Para comprobar actualizaciones del programa, haga clic en " & Quote & "Comprobar actualizaciones" & Quote & CrLf & "Última comprobación de actualizaciones realizada en: día π en " & UpdateCheckDate.ToLongTimeString
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                Label91.Text = "Pour vérifier les mises à jour du programme, cliquez sur " & Quote & "Vérifier les mises à jour" & Quote & CrLf & "Dernière vérification des mises à jour effectuée le : π jour à " & UpdateCheckDate.ToLongTimeString
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    Label91.Text = "To check for any program updates, click " & Quote & "Check for updates" & Quote & CrLf & "Last update check performed on: π day at " & UpdateCheckDate.ToLongTimeString
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    Label91.Text = "Para comprobar actualizaciones del programa, haga clic en " & Quote & "Comprobar actualizaciones" & Quote & CrLf & "Última comprobación de actualizaciones realizada en: día π en " & UpdateCheckDate.ToLongTimeString
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    Label91.Text = "Pour vérifier les mises à jour du programme, cliquez sur " & Quote & "Vérifier les mises à jour" & Quote & CrLf & "Dernière vérification des mises à jour effectuée le : π jour à " & UpdateCheckDate.ToLongTimeString
                End If
            End If
        Else
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                Label91.Text = "To check for any program updates, click " & Quote & "Check for updates" & Quote & CrLf & "Last update check performed on: " & UpdateCheckDate
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                Label91.Text = "Para comprobar actualizaciones del programa, haga clic en " & Quote & "Comprobar actualizaciones" & Quote & CrLf & "Última comprobación de actualizaciones realizada en: " & UpdateCheckDate
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                Label91.Text = "Pour vérifier les mises à jour du programme, cliquez sur " & Quote & "Vérifier les mises à jour" & Quote & CrLf & "Dernière vérification des mises à jour effectuée le : " & UpdateCheckDate
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    Label91.Text = "To check for any program updates, click " & Quote & "Check for updates" & Quote & CrLf & "Last update check performed on: " & UpdateCheckDate
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    Label91.Text = "Para comprobar actualizaciones del programa, haga clic en " & Quote & "Comprobar actualizaciones" & Quote & CrLf & "Última comprobación de actualizaciones realizada en: " & UpdateCheckDate
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    Label91.Text = "Pour vérifier les mises à jour du programme, cliquez sur " & Quote & "Vérifier les mises à jour" & Quote & CrLf & "Dernière vérification des mises à jour effectuée le : " & UpdateCheckDate
                End If
            End If
        End If
        needsUpdates = False
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        BringToFront()
        BackSubPanel.Show()
        DebugPanel.ShowDialog()
        DebugPanel.Visible = True
        DebugPanel.Visible = False
        BringToFront()
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            MinimumSize = New Size(1024, 600)
            Size = New Size(MinimumSize)
            FormBorderStyle = Windows.Forms.FormBorderStyle.None
            Panel_Border_Pic.Visible = True
            sidePanel.Visible = True
            NavBar.Visible = False
            TitleBar.Visible = True
            If WindowState = FormWindowState.Maximized Then
                WindowState = FormWindowState.Normal
                MaximumSize = Screen.FromControl(Me).WorkingArea.Size
                WindowState = FormWindowState.Maximized
                If BackColor = Color.FromArgb(243, 243, 243) Then
                    maxBox.Image = New Bitmap(My.Resources.restdownbox)
                Else
                    maxBox.Image = New Bitmap(My.Resources.restdownbox_dark)
                End If
            ElseIf WindowState = FormWindowState.Normal Then
                If BackColor = Color.FromArgb(243, 243, 243) Then
                    maxBox.Image = New Bitmap(My.Resources.maxbox)
                Else
                    maxBox.Image = New Bitmap(My.Resources.maxbox_dark)
                End If
            End If
        Else
            MinimumSize = New Size(1038, 664)
            FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
            ShowIcon = True
            Panel_Border_Pic.Visible = False
            sidePanel.Visible = False
            NavBar.Visible = True
            TitleBar.Visible = False
        End If
        If BackColor = Color.FromArgb(243, 243, 243) Then
            If RadioButton3.Checked = True Then
                PictureBox2.Image = New Bitmap(My.Resources.NavBar_LeftPos_Light)
            Else
                PictureBox2.Image = New Bitmap(My.Resources.NavBar_TopPos_Light)
            End If
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            If RadioButton3.Checked = True Then
                PictureBox2.Image = New Bitmap(My.Resources.NavBar_LeftPos_Dark)
            Else
                PictureBox2.Image = New Bitmap(My.Resources.NavBar_TopPos_Dark)
            End If
        End If
    End Sub

    Private Sub InstructionPic_Click(sender As Object, e As EventArgs) Handles InstructionPic.Click
        PanelIndicatorPic.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Left), AnchorStyles)
    End Sub

    Private Sub LinkLabel6_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel6.LinkClicked
        MsgBox("We could not get this computer's model" & CrLf & "This might be because, the component used to get the model (Windows Management Instrumentation, WMI), is missing from your system or has returned an error code." & CrLf & CrLf & "This is not critical, as it only affects the user experience.", vbOKOnly + vbExclamation, "Computer model gather process failure")
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click, AOTSMI.Click
        If Visible = False Or WindowState = FormWindowState.Minimized Then
            Activate()
            ShowInTaskbar = True
            WindowState = FormWindowState.Normal
            MiniModeDialog.Hide()
            BringToFront()
        End If
        BackSubPanel.Show()
        AdvancedOptionsPanel.ShowDialog()
        AdvancedOptionsPanel.Visible = True
        AdvancedOptionsPanel.Visible = False
        BringToFront()
        If BackColor = Color.FromArgb(243, 243, 243) Then
            closeBox.Image = New Bitmap(My.Resources.closebox)
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
        End If
    End Sub

    Private Sub LinkLabel7_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel7.LinkClicked
        SaveSettingsFile()
        BringToFront()
        BackSubPanel.Show()
        UpdateChoicePanel.ShowDialog()
        UpdateChoicePanel.Visible = True
        UpdateChoicePanel.Visible = False
        BringToFront()
    End Sub

    Private Sub LinkLabel8_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel8.LinkClicked
        Process.Start("https://www.github.com/CodingWonders/win11minst")
    End Sub

    Private Sub LinkLabel9_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel9.LinkClicked
        Process.Start("https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net48-developer-pack-offline-installer")
    End Sub

    Private Sub LinkLabel10_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel10.LinkClicked
        Process.Start("https://github.com/CodingWonders/win11minst/issues")
    End Sub

    Private Sub LinkLabel11_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel11.LinkClicked
        Process.Start("https://github.com/CodingWonders/win11minst/tree/hummingbird")
    End Sub

    Private Sub PictureBox35_Click(sender As Object, e As EventArgs) Handles PictureBox35.Click
        If My.Computer.Network.IsAvailable = True Then
            BringToFront()
            BackSubPanel.Show()
            ISOFileDownloadPanel.ShowDialog()
            ISOFileDownloadPanel.Visible = True
            ISOFileDownloadPanel.Visible = False
            BringToFront()
        Else
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                MsgBox("No network is available. Please connect your system to the Internet to access file downloads.", vbOKOnly + vbInformation, "A network connection is unavailable")
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                MsgBox("No hay ninguna red disponible. Por favor, conecte su sistema a Internet para acceder a descargas de archivos.", vbOKOnly + vbInformation, "Una conexión de red no está disponible")
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                MsgBox("Aucun réseau n'est disponible. Veuillez connecter votre système à l'Internet pour accéder aux téléchargements de fichiers.", vbOKOnly + vbInformation, "Une connexion réseau est indisponible")
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    MsgBox("No network is available. Please connect your system to the Internet to access file downloads.", vbOKOnly + vbInformation, "A network connection is unavailable")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    MsgBox("No hay ninguna red disponible. Por favor, conecte su sistema a Internet para acceder a descargas de archivos.", vbOKOnly + vbInformation, "Una conexión de red no está disponible")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    MsgBox("Aucun réseau n'est disponible. Veuillez connecter votre système à l'Internet pour accéder aux téléchargements de fichiers.", vbOKOnly + vbInformation, "Une connexion réseau est indisponible")
                End If
            End If
        End If
    End Sub

    Sub EnableBackPic()
        back_Pic.Visible = True
        If BackColor = Color.FromArgb(243, 243, 243) Then
            NavBarBackPic.Image = New Bitmap(My.Resources.back_arrow)
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            NavBarBackPic.Image = New Bitmap(My.Resources.back_arrow_dark)
        End If
        LogoPic.Left = 48
        ProgramTitleLabel.Left = 102
    End Sub

    Sub DisableBackPic()
        back_Pic.Visible = False
        NavBarBackPic.Image = New Bitmap(My.Resources.back_arrow_disabled)
        LogoPic.Left = 19
        ProgramTitleLabel.Left = 73
    End Sub

    Private Sub TargetInstallerLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles TargetInstallerLinkLabel.LinkClicked
        Process.Start("\Windows\explorer.exe", TextBox4.Text)
    End Sub

    Private Sub Label103_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label103.Click
        AdditionalToolsCMS.Show(CType(sender, Control), e.Location)
    End Sub

    Private Sub WIMRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WIMRToolStripMenuItem.Click
        WIMRToolStripMenuItem.Checked = True
        DLLRToolStripMenuItem.Checked = False
        REGTWEAKToolStripMenuItem.Checked = False
        ComboBox5.SelectedItem = "WIMR"
    End Sub

    Private Sub DLLRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DLLRToolStripMenuItem.Click
        WIMRToolStripMenuItem.Checked = False
        DLLRToolStripMenuItem.Checked = True
        REGTWEAKToolStripMenuItem.Checked = False
        ComboBox5.SelectedItem = "DLLR"
    End Sub

    Private Sub REGTWEAKToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles REGTWEAKToolStripMenuItem.Click
        WIMRToolStripMenuItem.Checked = False
        DLLRToolStripMenuItem.Checked = False
        REGTWEAKToolStripMenuItem.Checked = True
        ComboBox5.SelectedItem = "REGTWEAK"
    End Sub

    Sub ChangeColorInt()
        If ColorInt = 2 Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                ComboBox1.SelectedItem = "Light"
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                ComboBox1.SelectedItem = "Claro"
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                ComboBox1.SelectedItem = "Lumière"
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    ComboBox1.SelectedItem = "Light"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    ComboBox1.SelectedItem = "Claro"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    ComboBox1.SelectedItem = "Lumière"
                End If
            End If
        ElseIf ColorInt = 1 Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                ComboBox1.SelectedItem = "Dark"
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                ComboBox1.SelectedItem = "Oscuro"
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                ComboBox1.SelectedItem = "Sombre"
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    ComboBox1.SelectedItem = "Dark"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    ComboBox1.SelectedItem = "Oscuro"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    ComboBox1.SelectedItem = "Sombre"
                End If
            End If
        ElseIf ColorInt = 0 Then
            If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                ComboBox1.SelectedItem = "Automatic"
            ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                ComboBox1.SelectedItem = "Automático"
            ElseIf ComboBox4.SelectedItem = "French" Or ComboBox4.SelectedItem = "Francés" Or ComboBox4.SelectedItem = "Français" Then
                ComboBox1.SelectedItem = "Automatique"
            ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    ComboBox1.SelectedItem = "Automatic"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    ComboBox1.SelectedItem = "Automático"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    ComboBox1.SelectedItem = "Automatique"
                End If
            End If
        End If
    End Sub

    Private Sub AutomaticToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutomaticToolStripMenuItem.Click
        AutomaticToolStripMenuItem.Checked = True
        LightToolStripMenuItem.Checked = False
        DarkToolStripMenuItem.Checked = False
        ColorInt = 0
        ChangeColorInt()
    End Sub

    Private Sub LightToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LightToolStripMenuItem.Click
        AutomaticToolStripMenuItem.Checked = False
        LightToolStripMenuItem.Checked = True
        DarkToolStripMenuItem.Checked = False
        ColorInt = 2
        ChangeColorInt()
    End Sub

    Private Sub DarkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DarkToolStripMenuItem.Click
        AutomaticToolStripMenuItem.Checked = False
        LightToolStripMenuItem.Checked = False
        DarkToolStripMenuItem.Checked = True
        ColorInt = 1
        ChangeColorInt()
    End Sub

    Private Sub TopLeftResizePanel_MouseDown(sender As Object, e As MouseEventArgs) Handles TopLeftResizePanel.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            mouseOffset = New Point(-e.X, -e.Y)
            isMouseDown = True
        End If
    End Sub

    Private Sub TopLeftResizePanel_MouseMove(sender As Object, e As MouseEventArgs) Handles TopLeftResizePanel.MouseMove
        If isMouseDown Then
            Dim XOffset, YOffset As Integer
            XOffset = MousePosition.X - Location.X
            YOffset = MousePosition.Y - Location.Y
            Size = New Size(Location.X + XOffset, Location.Y + YOffset)
        End If
    End Sub

    Private Sub TopLeftResizePanel_MouseUp(sender As Object, e As MouseEventArgs) Handles TopLeftResizePanel.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            isMouseDown = False
        End If
    End Sub

    Private Sub LinkLabel12_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel12.LinkClicked
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            If TextBox1.ForeColor = Color.Crimson Then
                If TextBox2.ForeColor = Color.Crimson Then
                    If TextBox3.ForeColor = Color.Crimson Then
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                   "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                   "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "For the third and fourth errors, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                   "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Naming and path issues")
                        Else
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                   "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                   "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "For the third error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                   "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Naming and path issues")
                        End If
                    Else
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                   "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                   "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "For the third error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                   "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Naming and path issues")
                        Else
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                   "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists.", _
                                   vbOKOnly + vbCritical, "Path issues")
                        End If
                    End If
                Else
                    If TextBox3.ForeColor = Color.Crimson Then
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                   "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "For the second and third errors, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                   "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Naming and path issues")
                        Else
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                   "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "For the second error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                   "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Naming and path issues")
                        End If
                    Else
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                   "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "For the second error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                   "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Naming and path issues")
                        Else
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists.", _
                                   vbOKOnly + vbCritical, "Path issues")
                        End If
                    End If
                End If
            Else
                If TextBox2.ForeColor = Color.Crimson Then
                    If TextBox3.ForeColor = Color.Crimson Then
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                   "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "For the second and third errors, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                   "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Naming and path issues")
                        Else
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                   "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "For the second error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                   "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Naming and path issues")
                        End If
                    Else
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                   "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "For the second error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                   "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Naming and path issues")
                        Else
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists.", _
                                   vbOKOnly + vbCritical, "Path issues")
                        End If
                    End If
                Else
                    If TextBox3.ForeColor = Color.Crimson Then
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "For these errors, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                   "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Naming and path issues")
                        Else
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "For this error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                   "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Naming issues")
                        End If
                    Else
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                   "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                   "For this error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                   "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Path issues")
                        Else
                            ' Assume that everything is OK. If something does not work afterwards, notify a bug
                            ' on my Issues page
                        End If
                    End If
                End If
            End If
            If TextBox1.Text = TextBox2.Text Then
                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                       "- The Windows 11 and 10 installers you've provided are the same. Please provide a different one for both installers", _
                       vbOKOnly + vbCritical, "Path issues")
            End If
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            If TextBox1.ForeColor = Color.Crimson Then
                If TextBox2.ForeColor = Color.Crimson Then
                    If TextBox3.ForeColor = Color.Crimson Then
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                   "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "- Hay caracteres inválidos en la ruta del instalador de destino. No puede contener: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o: <, >, " & Quote & ", |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "Para los errores tercero y cuarto, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                   "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                        Else
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                   "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "Para el tercer error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                   "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                        End If
                    Else
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                   "- Hay caracteres inválidos en la ruta del instalador de destino. No puede contener: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o: <, >, " & Quote & ", |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "Para el tercer error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                   "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                        Else
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista", _
                                   vbOKOnly + vbCritical, "Errores de ruta")
                        End If
                    End If
                Else
                    If TextBox3.ForeColor = Color.Crimson Then
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                   "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "- Hay caracteres inválidos en la ruta del instalador de destino. No puede contener: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o: <, >, " & Quote & ", |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "Para los errores segundo y tercero, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                   "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                        Else
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                   "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "Para el segundo error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                   "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                        End If
                    Else
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                   "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "Para el segundo error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                   "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                        Else
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista", _
                                   vbOKOnly + vbCritical, "Errores de ruta")
                        End If
                    End If
                End If
            Else
                If TextBox2.ForeColor = Color.Crimson Then
                    If TextBox3.ForeColor = Color.Crimson Then
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                   "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "- Hay caracteres inválidos en la ruta del instalador de destino. No puede contener: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o: <, >, " & Quote & ", |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "Para los errores segundo y tercero, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                   "https://docs.microsoft.com/es-ss/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                        Else
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                   "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "Para el segundo error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                   "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                        End If
                    Else
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                   "- Hay caracteres inválidos en la ruta del instalador de destino. No puede contener: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o: <, >, " & Quote & ", |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "Para el segundo error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                   "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                        Else
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista", _
                                   vbOKOnly + vbCritical, "Errores de ruta")
                        End If
                    End If
                Else
                    If TextBox3.ForeColor = Color.Crimson Then
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "- Hay caracteres inválidos en la ruta del instalador de destino. No puede contener: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o: <, >, " & Quote & ", |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "Para estos errores, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                   "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                        Else
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "Para este error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                   "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Errores de nomenclatura")
                        End If
                    Else
                        If TextBox4.ForeColor = Color.Crimson Then
                            MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                   "- Hay caracteres inválidos en la ruta del instalador de destino. No puede contener: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o: <, >, " & Quote & ", |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                   "Para este error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                   "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                   vbOKOnly + vbCritical, "Errores de ruta")
                        Else
                            ' Assume that everything is OK. If something does not work afterwards, notify a bug
                            ' on my Issues page (https://github.com/CodingWonders/win11minst/issues)
                        End If
                    End If
                End If
            End If
            If TextBox1.Text = TextBox2.Text Then
                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                       "- Los instaladores proveídos de Windows 11 y 10 son iguales. Por favor, especifique archivos diferentes para ambos instaladores", _
                       vbOKOnly + vbCritical, "Errores de ruta")
            End If
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                If TextBox1.ForeColor = Color.Crimson Then
                    If TextBox2.ForeColor = Color.Crimson Then
                        If TextBox3.ForeColor = Color.Crimson Then
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                       "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                       "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "For the third and fourth errors, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                       "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Naming and path issues")
                            Else
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                       "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                       "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "For the third error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                       "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Naming and path issues")
                            End If
                        Else
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                       "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                       "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "For the third error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                       "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Naming and path issues")
                            Else
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                       "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists.", _
                                       vbOKOnly + vbCritical, "Path issues")
                            End If
                        End If
                    Else
                        If TextBox3.ForeColor = Color.Crimson Then
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                       "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "For the second and third errors, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                       "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Naming and path issues")
                            Else
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                       "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "For the second error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                       "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Naming and path issues")
                            End If
                        Else
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                       "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "For the second error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                       "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Naming and path issues")
                            Else
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- The Windows 11 installer location you have provided does not exist. Please specify a file that exists.", _
                                       vbOKOnly + vbCritical, "Path issues")
                            End If
                        End If
                    End If
                Else
                    If TextBox2.ForeColor = Color.Crimson Then
                        If TextBox3.ForeColor = Color.Crimson Then
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                       "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "For the second and third errors, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                       "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Naming and path issues")
                            Else
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                       "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "For the second error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                       "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Naming and path issues")
                            End If
                        Else
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists." & CrLf & _
                                       "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "For the second error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                       "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Naming and path issues")
                            Else
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- The Windows 10 installer location you have provided does not exist. Please specify a file that exists.", _
                                       vbOKOnly + vbCritical, "Path issues")
                            End If
                        End If
                    Else
                        If TextBox3.ForeColor = Color.Crimson Then
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "For these errors, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                       "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Naming and path issues")
                            Else
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- There are invalid characters on the target installer name. It must not be: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or contain: <, >, :, " & Quote & ", /, \, |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "For this error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                       "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Naming issues")
                            End If
                        Else
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                                       "- There are invalid characters on the target installer path. It must not contain: CON, AUX, PRN, NUL, COM{1-9} or LPT{1-9}, or: <, >, " & Quote & ", |, ? or *, because these are names reserved by Windows" & CrLf & _
                                       "For this error, if you are not sure about what are reserved names, please refer to Microsoft documentation on naming files, paths, and namespaces on:" & CrLf & _
                                       "https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Path issues")
                            Else
                                ' Assume that everything is OK. If something does not work afterwards, notify a bug
                                ' on my Issues page
                            End If
                        End If
                    End If
                End If
                If TextBox1.Text = TextBox2.Text Then
                    MsgBox("You cannot create the installer unless you fix these issues:" & CrLf & _
                           "- The Windows 11 and 10 installers you've provided are the same. Please provide a different one for both installers", _
                           vbOKOnly + vbCritical, "Path issues")
                End If
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                If TextBox1.ForeColor = Color.Crimson Then
                    If TextBox2.ForeColor = Color.Crimson Then
                        If TextBox3.ForeColor = Color.Crimson Then
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                       "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "- Hay caracteres inválidos en la ruta del instalador de destino. No puede contener: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o: <, >, " & Quote & ", |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "Para los errores tercero y cuarto, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                       "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                            Else
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                       "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "Para el tercer error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                       "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                            End If
                        Else
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                       "- Hay caracteres inválidos en la ruta del instalador de destino. No puede contener: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o: <, >, " & Quote & ", |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "Para el tercer error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                       "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                            Else
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista", _
                                       vbOKOnly + vbCritical, "Errores de ruta")
                            End If
                        End If
                    Else
                        If TextBox3.ForeColor = Color.Crimson Then
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                       "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "- Hay caracteres inválidos en la ruta del instalador de destino. No puede contener: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o: <, >, " & Quote & ", |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "Para los errores segundo y tercero, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                       "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                            Else
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                       "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "Para el segundo error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                       "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                            End If
                        Else
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                       "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "Para el segundo error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                       "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                            Else
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 11 no existe. Por favor, especifique un archivo que exista", _
                                       vbOKOnly + vbCritical, "Errores de ruta")
                            End If
                        End If
                    End If
                Else
                    If TextBox2.ForeColor = Color.Crimson Then
                        If TextBox3.ForeColor = Color.Crimson Then
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                       "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "- Hay caracteres inválidos en la ruta del instalador de destino. No puede contener: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o: <, >, " & Quote & ", |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "Para los errores segundo y tercero, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                       "https://docs.microsoft.com/es-ss/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                            Else
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                       "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "Para el segundo error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                       "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                            End If
                        Else
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista" & CrLf & _
                                       "- Hay caracteres inválidos en la ruta del instalador de destino. No puede contener: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o: <, >, " & Quote & ", |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "Para el segundo error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                       "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Errores de ruta y nomenclatura")
                            Else
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- La ubicación proveída del instalador de Windows 10 no existe. Por favor, especifique un archivo que exista", _
                                       vbOKOnly + vbCritical, "Errores de ruta")
                            End If
                        End If
                    Else
                        If TextBox3.ForeColor = Color.Crimson Then
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "- Hay caracteres inválidos en la ruta del instalador de destino. No puede contener: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o: <, >, " & Quote & ", |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "Para estos errores, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                       "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Errores de ruta")
                            Else
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- Hay caracteres inválidos en el nombre del instalador de destino. No puede ser: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o contener: <, >, :, " & Quote & ", /, \, |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "Para este error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                       "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Errores de nomenclatura")
                            End If
                        Else
                            If TextBox4.ForeColor = Color.Crimson Then
                                MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                                       "- Hay caracteres inválidos en la ruta del instalador de destino. No puede contener: CON, AUX, PRN, NUL, COM{1-9} o LPT{1-9}, o: <, >, " & Quote & ", |, ? o *, porque esos son nombres reservados por Windows" & CrLf & _
                                       "Para este error, si no está seguro de lo que son nombres reservados, por favor consulte la documentación de Microsoft para nombrar archivos, rutas y espacios de nombres en:" & CrLf & _
                                       "https://docs.microsoft.com/es-es/windows/win32/fileio/naming-a-file#naming-conventions", _
                                       vbOKOnly + vbCritical, "Errores de ruta")
                            Else
                                ' Assume that everything is OK. If something does not work afterwards, notify a bug
                                ' on my Issues page (https://github.com/CodingWonders/win11minst/issues)
                            End If
                        End If
                    End If
                End If
                If TextBox1.Text = TextBox2.Text Then
                    MsgBox("No puede crear el instalador a menos de que corrija estos errores:" & CrLf & _
                           "- Los instaladores proveídos de Windows 11 y 10 son iguales. Por favor, especifique archivos diferentes para ambos instaladores", _
                           vbOKOnly + vbCritical, "Errores de ruta")
                End If
            End If
        End If
    End Sub

    Private Sub MainForm_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If Left > Screen.PrimaryScreen.Bounds.Width Or Left <= Screen.PrimaryScreen.Bounds.Width Then
            MaximumSize = Screen.FromControl(Me).WorkingArea.Size
        End If
    End Sub

    Private Sub Label18_MouseEnter(sender As Object, e As EventArgs) Handles Label18.MouseEnter
        If BackColor = Color.FromArgb(243, 243, 243) Then
            Label18.ForeColor = Color.Black
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            Label18.ForeColor = Color.White
        End If
    End Sub

    Private Sub Label18_MouseLeave(sender As Object, e As EventArgs) Handles Label18.MouseLeave
        Label18.ForeColor = Color.DimGray
    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click
        If MOLinkIsClicked Then
            Settings_FunctionalityPanel.Visible = False
            InstCreatePanel.Visible = True
            ApplyNavBarImages()
            PanelIndicatorPic.Anchor = CType((AnchorStyles.Top Or AnchorStyles.Left), AnchorStyles)
            PanelIndicatorPic.Top = InstCreatePic.Top + 2
            If BackColor = Color.FromArgb(243, 243, 243) Then
                SettingsPic.Image = New Bitmap(My.Resources.settings)
            ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
                SettingsPic.Image = New Bitmap(My.Resources.settings_dark)
            End If
        Else
            Settings_FunctionalityPanel.Visible = False
            SettingPanel.Visible = True
        End If
        DisableBackPic()
    End Sub

    Private Sub Label10_MouseEnter(sender As Object, e As EventArgs) Handles Label10.MouseEnter
        If BackColor = Color.FromArgb(243, 243, 243) Then
            Label10.ForeColor = Color.Black
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            Label10.ForeColor = Color.White
        End If
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        Settings_PersonalizationPanel.Visible = False
        SettingPanel.Visible = True
        DisableBackPic()
    End Sub

    Private Sub Label10_MouseLeave(sender As Object, e As EventArgs) Handles Label10.MouseLeave
        Label10.ForeColor = Color.DimGray
    End Sub

    Private Sub LinkLabel13_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel13.LinkClicked
        Process.Start("https://github.com/microsoft/fluentui-system-icons")
    End Sub

    Private Sub LinkLabel14_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel14.LinkClicked
        Process.Start("https://icons8.com/icons/fluency")
    End Sub

    Private Sub LinkLabel16_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel16.LinkClicked
        Process.Start("https://github.com/rcmaehl/whynotwin11")
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        CheckBox1.Enabled = CheckBox3.Checked = True
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        BackSubPanel.Show()
        MethodHelpPanel.ShowDialog()
        MethodHelpPanel.Visible = True
        MethodHelpPanel.Visible = False
        BringToFront()
    End Sub

    Private Sub AutomaticLanguageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutomaticLanguageToolStripMenuItem.Click
        LangInt = 0
        If ComboBox4.Items.Contains("Automatic") Then
            ComboBox4.SelectedItem = "Automatic"
        ElseIf ComboBox4.Items.Contains("Automático") Then
            ComboBox4.SelectedItem = "Automático"
        ElseIf ComboBox4.Items.Contains("Automatique") Then
            ComboBox4.SelectedItem = "Automatique"
        End If
    End Sub

    Private Sub EnglishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnglishToolStripMenuItem.Click
        LangInt = 1
        If ComboBox4.Items.Contains("English") Then
            ComboBox4.SelectedItem = "English"
        ElseIf ComboBox4.Items.Contains("Inglés") Then
            ComboBox4.SelectedItem = "Inglés"
        ElseIf ComboBox4.Items.Contains("Anglais") Then
            ComboBox4.SelectedItem = "Anglais"
        End If
    End Sub

    Private Sub SpanishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpanishToolStripMenuItem.Click
        LangInt = 2
        If ComboBox4.Items.Contains("Spanish") Then
            ComboBox4.SelectedItem = "Spanish"
        ElseIf ComboBox4.Items.Contains("Español") Then
            ComboBox4.SelectedItem = "Español"
        ElseIf ComboBox4.Items.Contains("Espagnol") Then
            ComboBox4.SelectedItem = "Espagnol"
        End If
    End Sub

    Private Sub LogViewLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LogViewLink.LinkClicked
        Process.Start(".\inst.log")
    End Sub

    Private Sub TextBox1_DragEnter(sender As Object, e As DragEventArgs) Handles TextBox1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub TextBox1_DragDrop(sender As Object, e As DragEventArgs) Handles TextBox1.DragDrop
        Dim IsoFiles() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each Path In IsoFiles
            TextBox1.Text = Path
        Next
    End Sub

    Private Sub TextBox2_DragEnter(sender As Object, e As DragEventArgs) Handles TextBox2.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub TextBox2_DragDrop(sender As Object, e As DragEventArgs) Handles TextBox2.DragDrop
        Dim IsoFiles() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each Path In IsoFiles
            TextBox2.Text = Path
        Next
    End Sub

    Sub UpdatePanelProperties()
        ' Color modes
        If BackSubPanel.Visible = True Then
            If BackColor = Color.FromArgb(243, 243, 243) Then
                If AdvancedOptionsPanel.Visible = True Then
                    AdvancedOptionsPanel.BackColor = Color.White
                    AdvancedOptionsPanel.ForeColor = Color.Black
                    AdvancedOptionsPanel.Panel1.BackColor = Color.FromArgb(243, 243, 243)
                    AdvancedOptionsPanel.LinkLabel1.LinkColor = Color.FromArgb(1, 92, 186)
                    AdvancedOptionsPanel.OK_Button.BackColor = Color.FromArgb(1, 92, 186)
                    AdvancedOptionsPanel.OK_Button.ForeColor = Color.White
                ElseIf DisclaimerPanel.Visible = True Then
                    DisclaimerPanel.BackColor = Color.White
                    DisclaimerPanel.ForeColor = Color.Black
                    DisclaimerPanel.Panel1.BackColor = Color.FromArgb(243, 243, 243)
                    DisclaimerPanel.TextBox1.BackColor = Color.White
                    DisclaimerPanel.TextBox1.ForeColor = Color.Black
                    DisclaimerPanel.BackColor = Color.FromArgb(1, 92, 186)
                    DisclaimerPanel.OK_Button.ForeColor = Color.White
                ElseIf FileCopyPanel.Visible = True Then
                    FileCopyPanel.BackColor = Color.White
                    FileCopyPanel.ForeColor = Color.Black
                    FileCopyPanel.Panel1.BackColor = Color.FromArgb(243, 243, 243)
                    FileCopyPanel.OK_Button.BackColor = Color.FromArgb(1, 92, 186)
                    FileCopyPanel.OK_Button.ForeColor = Color.White
                ElseIf InstCreateAbortPanel.Visible = True Then
                    InstCreateAbortPanel.BackColor = Color.White
                    InstCreateAbortPanel.ForeColor = Color.Black
                    InstCreateAbortPanel.Panel1.BackColor = Color.FromArgb(243, 243, 243)
                    InstCreateAbortPanel.No_Button.BackColor = Color.FromArgb(1, 92, 186)
                    InstCreateAbortPanel.No_Button.ForeColor = Color.White
                ElseIf InstHistPanel.Visible = True Then
                    InstHistPanel.BackColor = Color.White
                    InstHistPanel.ForeColor = Color.Black
                    InstHistPanel.Panel1.BackColor = Color.FromArgb(243, 243, 243)
                    InstHistPanel.InstallerListView.ForeColor = Color.Black
                    InstHistPanel.InstallerListView.BackColor = Color.White
                    InstHistPanel.OK_Button.BackColor = Color.FromArgb(1, 92, 186)
                    InstHistPanel.OK_Button.ForeColor = Color.White
                    InstHistPanel.ExportOptnBtn.Image = New Bitmap(My.Resources.export_light)
                    InstHistPanel.ExportOptnBtn.BackColor = Color.FromArgb(243, 243, 243)
                    InstHistPanel.ExportOptnBtn.ForeColor = Color.Black
                ElseIf ISOFileDownloadPanel.Visible = True Then
                    ISOFileDownloadPanel.BackColor = Color.White
                    ISOFileDownloadPanel.ForeColor = Color.Black
                    ISOFileDownloadPanel.Panel1.BackColor = Color.FromArgb(243, 243, 243)
                    ISOFileDownloadPanel.OK_Button.BackColor = Color.FromArgb(1, 92, 186)
                    ISOFileDownloadPanel.OK_Button.ForeColor = Color.White
                ElseIf ISOFileScanPanel.Visible = True Then
                    ISOFileScanPanel.BackColor = Color.White
                    ISOFileScanPanel.ForeColor = Color.Black
                    ISOFileScanPanel.TextBox1.BackColor = Color.White
                    ISOFileScanPanel.TextBox1.ForeColor = Color.Black
                    ISOFileScanPanel.ListBox1.BackColor = Color.White
                    ISOFileScanPanel.ListBox1.ForeColor = Color.Black
                    ISOFileScanPanel.PictureBox1.Image = New Bitmap(My.Resources.pref_reset)
                    ISOFileScanPanel.Panel1.BackColor = Color.FromArgb(243, 243, 243)
                    ISOFileScanPanel.OK_Button.BackColor = Color.FromArgb(1, 92, 186)
                    ISOFileScanPanel.OK_Button.ForeColor = Color.White
                ElseIf LogExistsPanel.Visible = True Then
                    LogExistsPanel.BackColor = Color.White
                    LogExistsPanel.ForeColor = Color.Black
                    LogExistsPanel.Panel1.BackColor = Color.FromArgb(243, 243, 243)
                    LogExistsPanel.Yes_Button.BackColor = Color.FromArgb(1, 92, 186)
                    LogExistsPanel.Yes_Button.ForeColor = Color.White
                ElseIf LogMigratePanel.Visible = True Then
                    LogMigratePanel.BackColor = Color.White
                    LogMigratePanel.ForeColor = Color.Black
                    LogMigratePanel.Panel1.BackColor = Color.FromArgb(243, 243, 243)
                    LogMigratePanel.Yes_Button.BackColor = Color.FromArgb(1, 92, 186)
                    LogMigratePanel.Yes_Button.ForeColor = Color.White
                ElseIf MethodHelpPanel.Visible = True Then
                    MethodHelpPanel.BackColor = Color.White
                    MethodHelpPanel.ForeColor = Color.Black
                    MethodHelpPanel.Panel1.BackColor = Color.FromArgb(243, 243, 243)
                    MethodHelpPanel.OK_Button.BackColor = Color.FromArgb(1, 92, 186)
                    MethodHelpPanel.OK_Button.ForeColor = Color.White
                ElseIf PrefResetPanel.Visible = True Then
                    PrefResetPanel.BackColor = Color.White
                    PrefResetPanel.ForeColor = Color.Black
                    PrefResetPanel.Panel1.BackColor = Color.FromArgb(243, 243, 243)
                    PrefResetPanel.No_Button.BackColor = Color.FromArgb(1, 92, 186)
                    PrefResetPanel.No_Button.ForeColor = Color.White
                ElseIf UpdateChoicePanel.Visible = True Then
                    UpdateChoicePanel.BackColor = Color.White
                    UpdateChoicePanel.ForeColor = Color.Black
                    UpdateChoicePanel.Panel1.BackColor = Color.FromArgb(243, 243, 243)
                    UpdateChoicePanel.OK_Button.BackColor = Color.FromArgb(1, 92, 186)
                    UpdateChoicePanel.OK_Button.ForeColor = Color.White
                    UpdateChoicePanel.PictureBox1.Image = New Bitmap(My.Resources.update_screen_light)
                    UpdateChoicePanel.RelNotesLink.LinkColor = Color.FromArgb(1, 92, 186)
                End If
            ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
                If AdvancedOptionsPanel.Visible = True Then
                    AdvancedOptionsPanel.BackColor = Color.FromArgb(43, 43, 43)
                    AdvancedOptionsPanel.ForeColor = Color.White
                    AdvancedOptionsPanel.Panel1.BackColor = Color.FromArgb(32, 32, 32)
                    AdvancedOptionsPanel.LinkLabel1.LinkColor = Color.FromArgb(76, 194, 255)
                    AdvancedOptionsPanel.OK_Button.BackColor = Color.FromArgb(76, 194, 255)
                    AdvancedOptionsPanel.OK_Button.ForeColor = Color.Black
                ElseIf DisclaimerPanel.Visible = True Then
                    DisclaimerPanel.BackColor = Color.FromArgb(43, 43, 43)
                    DisclaimerPanel.ForeColor = Color.White
                    DisclaimerPanel.Panel1.BackColor = Color.FromArgb(32, 32, 32)
                    DisclaimerPanel.TextBox1.BackColor = Color.FromArgb(43, 43, 43)
                    DisclaimerPanel.TextBox1.ForeColor = Color.White
                    DisclaimerPanel.BackColor = Color.FromArgb(76, 194, 255)
                    DisclaimerPanel.OK_Button.ForeColor = Color.Black
                ElseIf FileCopyPanel.Visible = True Then
                    FileCopyPanel.BackColor = Color.FromArgb(43, 43, 43)
                    FileCopyPanel.ForeColor = Color.White
                    FileCopyPanel.Panel1.BackColor = Color.FromArgb(32, 32, 32)
                    FileCopyPanel.OK_Button.BackColor = Color.FromArgb(76, 194, 255)
                    FileCopyPanel.OK_Button.ForeColor = Color.Black
                ElseIf InstCreateAbortPanel.Visible = True Then
                    InstCreateAbortPanel.BackColor = Color.FromArgb(43, 43, 43)
                    InstCreateAbortPanel.ForeColor = Color.White
                    InstCreateAbortPanel.Panel1.BackColor = Color.FromArgb(32, 32, 32)
                    InstCreateAbortPanel.No_Button.BackColor = Color.FromArgb(76, 194, 255)
                    InstCreateAbortPanel.No_Button.ForeColor = Color.Black
                ElseIf InstHistPanel.Visible = True Then
                    InstHistPanel.BackColor = Color.FromArgb(43, 43, 43)
                    InstHistPanel.ForeColor = Color.White
                    InstHistPanel.Panel1.BackColor = Color.FromArgb(32, 32, 32)
                    InstHistPanel.InstallerListView.ForeColor = Color.White
                    InstHistPanel.InstallerListView.BackColor = Color.FromArgb(43, 43, 43)
                    InstHistPanel.OK_Button.BackColor = Color.FromArgb(76, 194, 255)
                    InstHistPanel.OK_Button.ForeColor = Color.Black
                    InstHistPanel.ExportOptnBtn.Image = New Bitmap(My.Resources.export_dark)
                    InstHistPanel.ExportOptnBtn.BackColor = Color.FromArgb(32, 32, 32)
                    InstHistPanel.ExportOptnBtn.ForeColor = Color.White
                ElseIf ISOFileDownloadPanel.Visible = True Then
                    ISOFileDownloadPanel.BackColor = Color.FromArgb(43, 43, 43)
                    ISOFileDownloadPanel.ForeColor = Color.White
                    ISOFileDownloadPanel.Panel1.BackColor = Color.FromArgb(32, 32, 32)
                    ISOFileDownloadPanel.OK_Button.BackColor = Color.FromArgb(76, 194, 255)
                    ISOFileDownloadPanel.OK_Button.ForeColor = Color.Black
                ElseIf ISOFileScanPanel.Visible = True Then
                    ISOFileScanPanel.BackColor = Color.FromArgb(43, 43, 43)
                    ISOFileScanPanel.ForeColor = Color.White
                    ISOFileScanPanel.TextBox1.BackColor = Color.FromArgb(43, 43, 43)
                    ISOFileScanPanel.TextBox1.ForeColor = Color.White
                    ISOFileScanPanel.ListBox1.BackColor = Color.FromArgb(43, 43, 43)
                    ISOFileScanPanel.ListBox1.ForeColor = Color.White
                    ISOFileScanPanel.PictureBox1.Image = New Bitmap(My.Resources.pref_reset_dark)
                    ISOFileScanPanel.Panel1.BackColor = Color.FromArgb(32, 32, 32)
                    ISOFileScanPanel.OK_Button.BackColor = Color.FromArgb(76, 194, 255)
                    ISOFileScanPanel.OK_Button.ForeColor = Color.Black
                ElseIf LogExistsPanel.Visible = True Then
                    LogExistsPanel.BackColor = Color.FromArgb(43, 43, 43)
                    LogExistsPanel.ForeColor = Color.White
                    LogExistsPanel.Panel1.BackColor = Color.FromArgb(32, 32, 32)
                    LogExistsPanel.Yes_Button.BackColor = Color.FromArgb(76, 194, 255)
                    LogExistsPanel.Yes_Button.ForeColor = Color.Black
                ElseIf LogMigratePanel.Visible = True Then
                    LogMigratePanel.BackColor = Color.FromArgb(43, 43, 43)
                    LogMigratePanel.ForeColor = Color.White
                    LogMigratePanel.Panel1.BackColor = Color.FromArgb(32, 32, 32)
                    LogMigratePanel.Yes_Button.BackColor = Color.FromArgb(76, 194, 255)
                    LogMigratePanel.Yes_Button.ForeColor = Color.Black
                ElseIf MethodHelpPanel.Visible = True Then
                    MethodHelpPanel.BackColor = Color.FromArgb(43, 43, 43)
                    MethodHelpPanel.ForeColor = Color.White
                    MethodHelpPanel.Panel1.BackColor = Color.FromArgb(32, 32, 32)
                    MethodHelpPanel.OK_Button.BackColor = Color.FromArgb(76, 194, 255)
                    MethodHelpPanel.OK_Button.ForeColor = Color.Black
                ElseIf PrefResetPanel.Visible = True Then
                    PrefResetPanel.BackColor = Color.FromArgb(43, 43, 43)
                    PrefResetPanel.ForeColor = Color.White
                    PrefResetPanel.Panel1.BackColor = Color.FromArgb(32, 32, 32)
                    PrefResetPanel.No_Button.BackColor = Color.FromArgb(76, 194, 255)
                    PrefResetPanel.No_Button.ForeColor = Color.Black
                ElseIf UpdateChoicePanel.Visible = True Then
                    UpdateChoicePanel.BackColor = Color.FromArgb(43, 43, 43)
                    UpdateChoicePanel.ForeColor = Color.White
                    UpdateChoicePanel.Panel1.BackColor = Color.FromArgb(32, 32, 32)
                    UpdateChoicePanel.OK_Button.BackColor = Color.FromArgb(76, 194, 255)
                    UpdateChoicePanel.OK_Button.ForeColor = Color.Black
                    UpdateChoicePanel.PictureBox1.Image = New Bitmap(My.Resources.update_screen_dark)
                End If
            End If
            If AdvancedOptionsPanel.Visible = True Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    AdvancedOptionsPanel.CheckBox1.Text = "Bypass Microsoft Account sign-in and forced Internet connection setup (22557+)"
                    AdvancedOptionsPanel.CheckBox2.Text = "Hide " & Quote & "System requirements not met" & Quote & " watermark (22557+)"
                    AdvancedOptionsPanel.Label1.Text = "Advanced options"
                    AdvancedOptionsPanel.Label2.Text = "Bypasses Microsoft Account sign-in and forced Internet connection setup on Windows 11 Pro (Nickel builds 22557 onwards)"
                    AdvancedOptionsPanel.Label3.Text = "Note: the program must be run with administrative privileges"
                    AdvancedOptionsPanel.Label4.Text = "Hides the " & Quote & "System requirements not met" & Quote & " watermark on Nickel builds 22557 onwards and Windows Server" & Quote & "Copper" & Quote & " builds 25057 onwards"
                    AdvancedOptionsPanel.Label5.Text = "Enabling this option is not recommended yet, as it doesn't work as intended."
                    AdvancedOptionsPanel.LinkLabel1.Text = "Read the full issue and a possible workaround"
                    AdvancedOptionsPanel.OK_Button.Text = "OK"
                    AdvancedOptionsPanel.Cancel_Button.Text = "Cancel"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    AdvancedOptionsPanel.CheckBox1.Text = "Omitir inicio de sesión con la cuenta de Microsoft y configuración forzada de Internet (22557+)"
                    AdvancedOptionsPanel.CheckBox2.Text = "Ocultar la marca de agua " & Quote & "Requisitos de sistema no cumplidos" & Quote & " (22557+)"
                    AdvancedOptionsPanel.Label1.Text = "Opciones avanzadas"
                    AdvancedOptionsPanel.Label2.Text = "Omite el inicio de sesión con la cuenta de Microsoft y la configuración forzada de Internet en Windows 11 Pro (compilaciones de Nickel 22557 en adelante)"
                    AdvancedOptionsPanel.Label3.Text = "Nota: el programa debe ejecutarse con privilegios administrativos"
                    AdvancedOptionsPanel.Label4.Text = "Oculta la marca de agua " & Quote & "Requisitos de sistema no cumplidos" & Quote & " en compilaciones de Nickel 22557 en adelante y en compilaciones Windows Server " & Quote & "Copper" & Quote & " 25057 en adelante"
                    AdvancedOptionsPanel.Label5.Text = "Todavía no es recomendable habilitar esta opción, ya que no funciona como se esperaba."
                    AdvancedOptionsPanel.LinkLabel1.Text = "Lea la publicación completa y una posible solución"
                    AdvancedOptionsPanel.OK_Button.Text = "Aceptar"
                    AdvancedOptionsPanel.Cancel_Button.Text = "Cancelar"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        AdvancedOptionsPanel.CheckBox1.Text = "Bypass Microsoft Account sign-in and forced Internet connection setup (22557+)"
                        AdvancedOptionsPanel.CheckBox2.Text = "Hide " & Quote & "System requirements not met" & Quote & " watermark (22557+)"
                        AdvancedOptionsPanel.Label1.Text = "Advanced options"
                        AdvancedOptionsPanel.Label2.Text = "Bypasses Microsoft Account sign-in and forced Internet connection setup on Windows 11 Pro (Nickel builds 22557 onwards)"
                        AdvancedOptionsPanel.Label3.Text = "Note: the program must be run with administrative privileges"
                        AdvancedOptionsPanel.Label4.Text = "Hides the " & Quote & "System requirements not met" & Quote & " watermark on Nickel builds 22557 onwards and Windows Server" & Quote & "Copper" & Quote & " builds 25057 onwards"
                        AdvancedOptionsPanel.Label5.Text = "Enabling this option is not recommended yet, as it doesn't work as intended."
                        AdvancedOptionsPanel.LinkLabel1.Text = "Read the full issue and a possible workaround"
                        AdvancedOptionsPanel.OK_Button.Text = "OK"
                        AdvancedOptionsPanel.Cancel_Button.Text = "Cancel"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        AdvancedOptionsPanel.CheckBox1.Text = "Omitir inicio de sesión con la cuenta de Microsoft y configuración forzada de Internet (22557+)"
                        AdvancedOptionsPanel.CheckBox2.Text = "Ocultar la marca de agua " & Quote & "Requisitos de sistema no cumplidos" & Quote & " (22557+)"
                        AdvancedOptionsPanel.Label1.Text = "Opciones avanzadas"
                        AdvancedOptionsPanel.Label2.Text = "Omite el inicio de sesión con la cuenta de Microsoft y la configuración forzada de Internet en Windows 11 Pro (compilaciones de Nickel 22557 en adelante)"
                        AdvancedOptionsPanel.Label3.Text = "Nota: el programa debe ejecutarse con privilegios administrativos"
                        AdvancedOptionsPanel.Label4.Text = "Oculta la marca de agua " & Quote & "Requisitos de sistema no cumplidos" & Quote & " en compilaciones de Nickel 22557 en adelante y en compilaciones Windows Server " & Quote & "Copper" & Quote & " 25057 en adelante"
                        AdvancedOptionsPanel.Label5.Text = "Todavía no es recomendable habilitar esta opción, ya que no funciona como se esperaba."
                        AdvancedOptionsPanel.LinkLabel1.Text = "Lea la publicación completa y una posible solución"
                        AdvancedOptionsPanel.OK_Button.Text = "Aceptar"
                        AdvancedOptionsPanel.Cancel_Button.Text = "Cancelar"
                    End If
                End If
            ElseIf DisclaimerPanel.Visible = True Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    DisclaimerPanel.Label1.Text = "Disclaimer notice"
                    DisclaimerPanel.OK_Button.Text = "OK"
                    DisclaimerPanel.Exit_Button.Text = "Exit"
                    DisclaimerPanel.TextBox1.Text = "You must only use this tool on a system that you don't use productively." & CrLf & "Microsoft has warned that unsupported systems running Windows 11 might not recieve updates in the future." & CrLf & CrLf & "The modified installation images you create will also work on supported systems, but you can natively install Windows 11 on them, without performing modifications to the installation image." & CrLf & "If you have an unsupported system, don't upgrade it to Windows 11. Instead, you can perform a dual-boot, or use another system (that would be the best option anyway)" & CrLf & CrLf & "This tool MUST NOT be used to pirate Windows images, and the program developer recommends you get Windows legally." & CrLf & "The components used by the program are covered by their license agreements. These specify the rules for their use and redistribution." & CrLf & CrLf & "If you agree to this disclaimer notice and want to continue using the software, click OK. Otherwise, click Exit."
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    DisclaimerPanel.Label1.Text = "Descargo de responsabilidad"
                    DisclaimerPanel.OK_Button.Text = "Aceptar"
                    DisclaimerPanel.Exit_Button.Text = "Salir"
                    DisclaimerPanel.TextBox1.Text = "Usted solo debe utilizar esta herramienta en un sistema que no use productivamente." & CrLf & "Microsoft ha avisado de que sistemas no soportados ejecutando Windows 11 podrían no recibir actualizaciones en el futuro." & CrLf & CrLf & "Las imágenes de instalación modificadas que usted cree también funcionarán en sistemas soportados, pero usted puede instalar Windows 11 de forma nativa en ellos, sin realizar modificaciones a la imagen de instalación." & CrLf & "Si usted tiene un sistema no soportado, no lo actualice a Windows 11. En vez de eso, puede realizar un arranque dual, o usar otro sistema (ésta sería la mejor opción de todas formas)" & CrLf & CrLf & "Esta herramienta NO DEBE ser usada para piratear imágenes de Windows, y el desarrollador del programa le recomienda obtener Windows legalmente." & CrLf & "Los componentes utilizados por el programa están protegidos por sus acuerdos de licencia. Éstos especifican las reglas de su uso y redistribución." & CrLf & CrLf & "Si acepta este descargo de responsabilidad y quiere continuar usando el software, haga clic en Aceptar. En caso contrario, haga clic en Salir."
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        DisclaimerPanel.Label1.Text = "Disclaimer notice"
                        DisclaimerPanel.OK_Button.Text = "OK"
                        DisclaimerPanel.Exit_Button.Text = "Exit"
                        DisclaimerPanel.TextBox1.Text = "You must only use this tool on a system that you don't use productively." & CrLf & "Microsoft has warned that unsupported systems running Windows 11 might not recieve updates in the future." & CrLf & CrLf & "The modified installation images you create will also work on supported systems, but you can natively install Windows 11 on them, without performing modifications to the installation image." & CrLf & "If you have an unsupported system, don't upgrade it to Windows 11. Instead, you can perform a dual-boot, or use another system (that would be the best option anyway)" & CrLf & CrLf & "This tool MUST NOT be used to pirate Windows images, and the program developer recommends you get Windows legally." & CrLf & "The components used by the program are covered by their license agreements. These specify the rules for their use and redistribution." & CrLf & CrLf & "If you agree to this disclaimer notice and want to continue using the software, click OK. Otherwise, click Exit."
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        DisclaimerPanel.Label1.Text = "Descargo de responsabilidad"
                        DisclaimerPanel.OK_Button.Text = "Aceptar"
                        DisclaimerPanel.Exit_Button.Text = "Salir"
                        DisclaimerPanel.TextBox1.Text = "Usted solo debe utilizar esta herramienta en un sistema que no use productivamente." & CrLf & "Microsoft ha avisado de que sistemas no soportados ejecutando Windows 11 podrían no recibir actualizaciones en el futuro." & CrLf & CrLf & "Las imágenes de instalación modificadas que usted cree también funcionarán en sistemas soportados, pero usted puede instalar Windows 11 de forma nativa en ellos, sin realizar modificaciones a la imagen de instalación." & CrLf & "Si usted tiene un sistema no soportado, no lo actualice a Windows 11. En vez de eso, puede realizar un arranque dual, o usar otro sistema (ésta sería la mejor opción de todas formas)" & CrLf & CrLf & "Esta herramienta NO DEBE ser usada para piratear imágenes de Windows, y el desarrollador del programa le recomienda obtener Windows legalmente." & CrLf & "Los componentes utilizados por el programa están protegidos por sus acuerdos de licencia. Éstos especifican las reglas de su uso y redistribución." & CrLf & CrLf & "Si acepta este descargo de responsabilidad y quiere continuar usando el software, haga clic en Aceptar. En caso contrario, haga clic en Salir."
                    End If
                End If
            ElseIf FileCopyPanel.Visible = True Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    FileCopyPanel.Label1.Text = "File copy"
                    FileCopyPanel.Label2.Text = "To prevent file access errors while creating the custom installer, the source files will be copied to the local disk. These files will be deleted after the program has finished, to save disk space." & CrLf & "Do you want to do so?"
                    FileCopyPanel.OK_Button.Text = "Yes"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    FileCopyPanel.Label1.Text = "Copia de archivos"
                    FileCopyPanel.Label2.Text = "Para prevenir errores de acceso de archivo al crear el instalador modificado, los archivos de origen serán copiados al disco local. Éstos archivos serán borrados después de que el programa haya terminado, para ahorrar espacio en el disco." & CrLf & "¿Desea hacer esto?"
                    FileCopyPanel.OK_Button.Text = "Sí"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        FileCopyPanel.Label1.Text = "File copy"
                        FileCopyPanel.Label2.Text = "To prevent file access errors while creating the custom installer, the source files will be copied to the local disk. These files will be deleted after the program has finished, to save disk space." & CrLf & "Do you want to do so?"
                        FileCopyPanel.OK_Button.Text = "Yes"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        FileCopyPanel.Label1.Text = "Copia de archivos"
                        FileCopyPanel.Label2.Text = "Para prevenir errores de acceso de archivo al crear el instalador modificado, los archivos de origen serán copiados al disco local. Éstos archivos serán borrados después de que el programa haya terminado, para ahorrar espacio en el disco." & CrLf & "¿Desea hacer esto?"
                        FileCopyPanel.OK_Button.Text = "Sí"
                    End If
                End If
            ElseIf InstCreateAbortPanel.Visible = True Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    InstCreateAbortPanel.Label1.Text = "Cancel installer creation?"
                    InstCreateAbortPanel.Label2.Text = "Are you sure you want to cancel the installer creation process? This will delete any file modifications done at this time."
                    InstCreateAbortPanel.Yes_Button.Text = "Yes"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    InstCreateAbortPanel.Label1.Text = "¿Cancelar la creación del instalador?"
                    InstCreateAbortPanel.Label2.Text = "¿Está seguro de que quiere cancelar el proceso de creación del instalador? Esto borrará todas las modificaciones de archivos realizados en este momento."
                    InstCreateAbortPanel.Yes_Button.Text = "Sí"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        InstCreateAbortPanel.Label1.Text = "Cancel installer creation?"
                        InstCreateAbortPanel.Label2.Text = "Are you sure you want to cancel the installer creation process? This will delete any file modifications done at this time."
                        InstCreateAbortPanel.Yes_Button.Text = "Yes"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        InstCreateAbortPanel.Label1.Text = "¿Cancelar la creación del instalador?"
                        InstCreateAbortPanel.Label2.Text = "¿Está seguro de que quiere cancelar el proceso de creación del instalador? Esto borrará todas las modificaciones de archivos realizados en este momento."
                        InstCreateAbortPanel.Yes_Button.Text = "Sí"
                    End If
                End If
            ElseIf InstHistPanel.Visible = True Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    InstHistPanel.Label1.Text = "Installer history"
                    InstHistPanel.InstallerEntryLabel.Text = "Installer history entries: " & InstHistPanel.InstallerListView.Items.Count
                    InstHistPanel.ColumnHeader1.Text = "Installer name and path"
                    InstHistPanel.ColumnHeader2.Text = "Creation time and date"
                    InstHistPanel.OK_Button.Text = "OK"
                    InstHistPanel.XMLExportOptn.Text = "Export to XML file..."
                    InstHistPanel.HTMLExportOptn.Text = "Export to HTML file..."
                    InstHistPanel.ExportOptnBtn.Text = "Export options"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    InstHistPanel.Label1.Text = "Historial de instaladores"
                    InstHistPanel.InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstHistPanel.InstallerListView.Items.Count
                    InstHistPanel.ColumnHeader1.Text = "Nombre y ruta del instalador"
                    InstHistPanel.ColumnHeader2.Text = "Fecha y hora de creación"
                    InstHistPanel.OK_Button.Text = "Aceptar"
                    InstHistPanel.XMLExportOptn.Text = "Exportar a archivo XML..."
                    InstHistPanel.HTMLExportOptn.Text = "Exportar a archivo HTML..."
                    InstHistPanel.ExportOptnBtn.Text = "Opciones de exportación"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        InstHistPanel.Label1.Text = "Installer history"
                        InstHistPanel.InstallerEntryLabel.Text = "Installer history entries: " & InstHistPanel.InstallerListView.Items.Count
                        InstHistPanel.ColumnHeader1.Text = "Installer name and path"
                        InstHistPanel.ColumnHeader2.Text = "Creation time and date"
                        InstHistPanel.OK_Button.Text = "OK"
                        InstHistPanel.XMLExportOptn.Text = "Export to XML file..."
                        InstHistPanel.HTMLExportOptn.Text = "Export to HTML file..."
                        InstHistPanel.ExportOptnBtn.Text = "Export options"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        InstHistPanel.Label1.Text = "Historial de instaladores"
                        InstHistPanel.InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstHistPanel.InstallerListView.Items.Count
                        InstHistPanel.ColumnHeader1.Text = "Nombre y ruta del instalador"
                        InstHistPanel.ColumnHeader2.Text = "Fecha y hora de creación"
                        InstHistPanel.OK_Button.Text = "Aceptar"
                        InstHistPanel.XMLExportOptn.Text = "Exportar a archivo XML..."
                        InstHistPanel.HTMLExportOptn.Text = "Exportar a archivo HTML..."
                        InstHistPanel.ExportOptnBtn.Text = "Opciones de exportación"
                    End If
                End If
                If InstHistPanel.InstallerListView.Items.Count = 0 Then
                    If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                        InstHistPanel.InstallerEntryLabel.Text = "Installer history entries: " & InstHistPanel.InstallerListView.Items.Count & ". No installer history data is available."
                    ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                        InstHistPanel.InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstHistPanel.InstallerListView.Items.Count & ". No hay datos disponibles sobre el historial de instaladores."
                    ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                            InstHistPanel.InstallerEntryLabel.Text = "Installer history entries: " & InstHistPanel.InstallerListView.Items.Count & ". No installer history data is available."
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                            InstHistPanel.InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstHistPanel.InstallerListView.Items.Count & ". No hay datos disponibles sobre el historial de instaladores."
                        End If
                    End If
                End If
            ElseIf ISOFileDownloadPanel.Visible = True Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    ISOFileDownloadPanel.Label1.Text = "Download ISO files..."
                    ISOFileDownloadPanel.Label2.Text = "Download"
                    ISOFileDownloadPanel.Label4.Text = "Loading web component. Please wait..."
                    ISOFileDownloadPanel.Label5.Text = "We couldn 't load the web component." & CrLf & CrLf & "This can be caused by an unavailable Internet connection, or by a fault on the website backend. Please try again." & CrLf & "If the problem persists, please try to download the files manually by searching for " & Quote & "download windows 11" & Quote & " or " & Quote & "download windows 10" & Quote & " on the Internet."
                    ISOFileDownloadPanel.GroupBox1.Text = "Error status"
                    ISOFileDownloadPanel.Button1.Text = "Retry"
                    ISOFileDownloadPanel.OK_Button.Text = "OK"
                    ISOFileDownloadPanel.Cancel_Button.Text = "Cancel"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    ISOFileDownloadPanel.Label1.Text = "Descargar archivos ISO..."
                    ISOFileDownloadPanel.Label2.Text = "Descargar"
                    ISOFileDownloadPanel.Label4.Text = "Cargando componente web. Por favor, espere..."
                    ISOFileDownloadPanel.Label5.Text = "No pudimos cargar el componente web." & CrLf & CrLf & "Esto puede ser causado por una conexión a Internet no disponible, o por un error en el backend de la página web. Por favor, inténtelo de nuevo." & CrLf & "Si el problema persiste, por favor, intente descargar los archivos manualmente buscando " & Quote & "descargar windows 11" & Quote & " o " & Quote & "descargar windows 10" & Quote & " en Internet."
                    ISOFileDownloadPanel.GroupBox1.Text = "Estado de error"
                    ISOFileDownloadPanel.Button1.Text = "Reintentar"
                    ISOFileDownloadPanel.OK_Button.Text = "Aceptar"
                    ISOFileDownloadPanel.Cancel_Button.Text = "Cancelar"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        ISOFileDownloadPanel.Label1.Text = "Download ISO files..."
                        ISOFileDownloadPanel.Label2.Text = "Download"
                        ISOFileDownloadPanel.Label4.Text = "Loading web component. Please wait..."
                        ISOFileDownloadPanel.Label5.Text = "We couldn 't load the web component." & CrLf & CrLf & "This can be caused by an unavailable Internet connection, or by a fault on the website backend. Please try again." & CrLf & "If the problem persists, please try to download the files manually by searching for " & Quote & "download windows 11" & Quote & " or " & Quote & "download windows 10" & Quote & " on the Internet."
                        ISOFileDownloadPanel.GroupBox1.Text = "Error status"
                        ISOFileDownloadPanel.Button1.Text = "Retry"
                        ISOFileDownloadPanel.OK_Button.Text = "OK"
                        ISOFileDownloadPanel.Cancel_Button.Text = "Cancel"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        ISOFileDownloadPanel.Label1.Text = "Descargar archivos ISO..."
                        ISOFileDownloadPanel.Label2.Text = "Descargar"
                        ISOFileDownloadPanel.Label4.Text = "Cargando componente web. Por favor, espere..."
                        ISOFileDownloadPanel.Label5.Text = "No pudimos cargar el componente web." & CrLf & CrLf & "Esto puede ser causado por una conexión a Internet no disponible, o por un error en el backend de la página web. Por favor, inténtelo de nuevo." & CrLf & "Si el problema persiste, por favor, intente descargar los archivos manualmente buscando " & Quote & "descargar windows 11" & Quote & " o " & Quote & "descargar windows 10" & Quote & " en Internet."
                        ISOFileDownloadPanel.GroupBox1.Text = "Estado de error"
                        ISOFileDownloadPanel.Button1.Text = "Reintentar"
                        ISOFileDownloadPanel.OK_Button.Text = "Aceptar"
                        ISOFileDownloadPanel.Cancel_Button.Text = "Cancelar"
                    End If
                End If
            ElseIf ISOFileScanPanel.Visible = True Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    ISOFileScanPanel.Label1.Text = "Scan for ISO images"
                    ISOFileScanPanel.Label2.Text = "Directory to scan:"
                    ISOFileScanPanel.TextBox1.Left = 150
                    ISOFileScanPanel.TextBox1.Width = 468
                    ISOFileScanPanel.Label3.Text = "Scanning directory for ISO files..."
                    ISOFileScanPanel.Label5.Text = "This is a"
                    ISOFileScanPanel.Label4.Visible = True
                    ISOFileScanPanel.RadioButton1.Left = 504
                    ISOFileScanPanel.RadioButton2.Left = 599
                    ISOFileScanPanel.CounterLabel.Text = "Files found so far: " & ISOFileScanPanel.FoundFileNum
                    ISOFileScanPanel.CheckBox1.Text = "Search subdirectories for ISO images"
                    ISOFileScanPanel.Button1.Text = "Browse..."
                    ISOFileScanPanel.OK_Button.Text = "OK"
                    ISOFileScanPanel.Cancel_Button.Text = "Cancel"
                    ISOFileScanPanel.ISOFolderScanner.Description = "Please select a directory to scan for ISO files:"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    ISOFileScanPanel.Label1.Text = "Escanear imágenes ISO"
                    ISOFileScanPanel.Label2.Text = "Directorio a escanear:"
                    ISOFileScanPanel.TextBox1.Left = 171
                    ISOFileScanPanel.TextBox1.Width = 447
                    ISOFileScanPanel.Label3.Text = "Escaneando directorio por archivos ISO..."
                    ISOFileScanPanel.Label5.Text = "Este es un instalador de"
                    ISOFileScanPanel.Label4.Visible = False
                    ISOFileScanPanel.RadioButton1.Left = 586
                    ISOFileScanPanel.RadioButton2.Left = 681
                    ISOFileScanPanel.CounterLabel.Text = "Archivos encontrados hasta ahora: " & ISOFileScanPanel.FoundFileNum
                    ISOFileScanPanel.CheckBox1.Text = "Buscar en subdirectorios por archivos ISO"
                    ISOFileScanPanel.Button1.Text = "Examinar..."
                    ISOFileScanPanel.OK_Button.Text = "Aceptar"
                    ISOFileScanPanel.Cancel_Button.Text = "Cancelar"
                    ISOFileScanPanel.ISOFolderScanner.Description = "Por favor, elija un directorio para escanear archivos ISO:"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        ISOFileScanPanel.Label1.Text = "Scan for ISO images"
                        ISOFileScanPanel.Label2.Text = "Directory to scan:"
                        ISOFileScanPanel.TextBox1.Left = 150
                        ISOFileScanPanel.TextBox1.Width = 468
                        ISOFileScanPanel.Label3.Text = "Scanning directory for ISO files..."
                        ISOFileScanPanel.Label5.Text = "This is a"
                        ISOFileScanPanel.Label4.Visible = True
                        ISOFileScanPanel.RadioButton1.Left = 504
                        ISOFileScanPanel.RadioButton2.Left = 599
                        ISOFileScanPanel.CounterLabel.Text = "Files found so far: " & ISOFileScanPanel.FoundFileNum
                        ISOFileScanPanel.CheckBox1.Text = "Search subdirectories for ISO images"
                        ISOFileScanPanel.Button1.Text = "Browse..."
                        ISOFileScanPanel.OK_Button.Text = "OK"
                        ISOFileScanPanel.Cancel_Button.Text = "Cancel"
                        ISOFileScanPanel.ISOFolderScanner.Description = "Please select a directory to scan for ISO files:"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        ISOFileScanPanel.Label1.Text = "Escanear imágenes ISO"
                        ISOFileScanPanel.Label2.Text = "Directorio a escanear:"
                        ISOFileScanPanel.TextBox1.Left = 171
                        ISOFileScanPanel.TextBox1.Width = 447
                        ISOFileScanPanel.Label3.Text = "Escaneando directorio por archivos ISO..."
                        ISOFileScanPanel.Label5.Text = "Este es un instalador de"
                        ISOFileScanPanel.Label4.Visible = False
                        ISOFileScanPanel.RadioButton1.Left = 586
                        ISOFileScanPanel.RadioButton2.Left = 681
                        ISOFileScanPanel.CounterLabel.Text = "Archivos encontrados hasta ahora: " & ISOFileScanPanel.FoundFileNum
                        ISOFileScanPanel.CheckBox1.Text = "Buscar en subdirectorios por archivos ISO"
                        ISOFileScanPanel.Button1.Text = "Examinar..."
                        ISOFileScanPanel.OK_Button.Text = "Aceptar"
                        ISOFileScanPanel.Cancel_Button.Text = "Cancelar"
                        ISOFileScanPanel.ISOFolderScanner.Description = "Por favor, elija un directorio para escanear archivos ISO:"
                    End If
                End If
            ElseIf LogExistsPanel.Visible = True Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    LogExistsPanel.Label1.Text = "A log file already exists"
                    LogExistsPanel.Label2.Text = "Do you want to append the current log contents to the log file, or do you want to delete the existing log file?"
                    LogExistsPanel.Yes_Button.Text = "Append to existing log file"
                    LogExistsPanel.No_Button.Text = "Delete existing log file"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    LogExistsPanel.Label1.Text = "Un archivo de registro ya existe"
                    LogExistsPanel.Label2.Text = "¿Desea anexar los contenidos del registro actual al archivo de registro, o desea borrar el archivo de registro existente?"
                    LogExistsPanel.Yes_Button.Text = "Anexar al archivo de registro existente"
                    LogExistsPanel.No_Button.Text = "Borrar archivo de registro existente"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        LogExistsPanel.Label1.Text = "A log file already exists"
                        LogExistsPanel.Label2.Text = "Do you want to append the current log contents to the log file, or do you want to delete the existing log file?"
                        LogExistsPanel.Yes_Button.Text = "Append to existing log file"
                        LogExistsPanel.No_Button.Text = "Delete existing log file"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        LogExistsPanel.Label1.Text = "Un archivo de registro ya existe"
                        LogExistsPanel.Label2.Text = "¿Desea anexar los contenidos del registro actual al archivo de registro, o desea borrar el archivo de registro existente?"
                        LogExistsPanel.Yes_Button.Text = "Anexar al archivo de registro existente"
                        LogExistsPanel.No_Button.Text = "Borrar archivo de registro existente"
                    End If
                End If
            ElseIf LogMigratePanel.Visible = True Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    LogMigratePanel.Label1.Text = "Migrate old log files"
                    LogMigratePanel.Label2.Text = "The Windows 11 Manual Installer has detected log files created by previous versions of the program. Version 2.0 uses a different log format. Do you want to migrate the contents of old files, and append current log contents to the log file?"
                    LogMigratePanel.Yes_Button.Text = "Migrate"
                    LogMigratePanel.No_Button.Text = "Don't migrate"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    LogMigratePanel.Label1.Text = "Migrar archivos de registro antiguos"
                    LogMigratePanel.Label2.Text = "El Instalador manual de Windows 11 ha detectado archivos de registro creados por versiones antiguas del programa. La versión 2.0 utiliza un formato de registro diferente. ¿Desea migrar los contenidos de los archivos antiguos, y anexar los contenidos actuales del registro al archivo de registro?"
                    LogMigratePanel.Yes_Button.Text = "Migrar"
                    LogMigratePanel.No_Button.Text = "No migrar"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        LogMigratePanel.Label1.Text = "Migrate old log files"
                        LogMigratePanel.Label2.Text = "The Windows 11 Manual Installer has detected log files created by previous versions of the program. Version 2.0 uses a different log format. Do you want to migrate the contents of old files, and append current log contents to the log file?"
                        LogMigratePanel.Yes_Button.Text = "Migrate"
                        LogMigratePanel.No_Button.Text = "Don't migrate"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        LogMigratePanel.Label1.Text = "Migrar archivos de registro antiguos"
                        LogMigratePanel.Label2.Text = "El Instalador manual de Windows 11 ha detectado archivos de registro creados por versiones antiguas del programa. La versión 2.0 utiliza un formato de registro diferente. ¿Desea migrar los contenidos de los archivos antiguos, y anexar los contenidos actuales del registro al archivo de registro?"
                        LogMigratePanel.Yes_Button.Text = "Migrar"
                        LogMigratePanel.No_Button.Text = "No migrar"
                    End If
                End If
            ElseIf MethodHelpPanel.Visible = True Then
                ' If this is still present on stable release, fill this section
            ElseIf PrefResetPanel.Visible = True Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    PrefResetPanel.Label1.Text = "Reset preferences?"
                    PrefResetPanel.Label2.Text = "This will reset ALL preferences to their default values (e.g., language or color mode)"
                    PrefResetPanel.Yes_Button.Text = "Yes"
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    PrefResetPanel.Label1.Text = "¿Restablecer preferencias?"
                    PrefResetPanel.Label2.Text = "Esto restablecerá TODAS las preferencias a sus valores predeterminados (p.ej., el idioma o el modo de color)"
                    PrefResetPanel.Yes_Button.Text = "Sí"
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        PrefResetPanel.Label1.Text = "Reset preferences?"
                        PrefResetPanel.Label2.Text = "This will reset ALL preferences to their default values (e.g., language or color mode)"
                        PrefResetPanel.Yes_Button.Text = "Yes"
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        PrefResetPanel.Label1.Text = "¿Restablecer preferencias?"
                        PrefResetPanel.Label2.Text = "Esto restablecerá TODAS las preferencias a sus valores predeterminados (p.ej., el idioma o el modo de color)"
                        PrefResetPanel.Yes_Button.Text = "Sí"
                    End If
                End If
            ElseIf UpdateChoicePanel.Visible = True Then
                If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
                    UpdateChoicePanel.Label1.Text = "Updates are available"
                    UpdateChoicePanel.Label2.Text = "You can decide when do you want to install this update"
                    UpdateChoicePanel.Label3.Text = "This version:"
                    UpdateChoicePanel.Label4.Text = "Up-to-date version:"
                    UpdateChoicePanel.Label5.Text = "When you click " & Quote & "Install now" & Quote & ", the program will exit and update to the latest version."
                    UpdateChoicePanel.OK_Button.Text = "Install now"
                    UpdateChoicePanel.Cancel_Button.Text = "Install later"
                    UpdateChoicePanel.RelNotesLink.Text = "View release notes"
                    UpdateChoicePanel.TextBox1.Left = 177
                    UpdateChoicePanel.TextBox1.Width = 702
                    UpdateChoicePanel.TextBox2.Left = 226
                    UpdateChoicePanel.TextBox2.Width = 653
                ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
                    UpdateChoicePanel.Label1.Text = "Hay actualizaciones disponibles"
                    UpdateChoicePanel.Label2.Text = "Usted puede decidir cuándo quiere instalar esta actualización"
                    UpdateChoicePanel.Label3.Text = "Esta versión:"
                    UpdateChoicePanel.Label4.Text = "Versión actualizada:"
                    UpdateChoicePanel.Label5.Text = "Cuando haga clic en " & Quote & "Instalar ahora" & Quote & ", el programa se cerrará y se actualizará a la última versión."
                    UpdateChoicePanel.OK_Button.Text = "Instalar ahora"
                    UpdateChoicePanel.Cancel_Button.Text = "Instalar después"
                    UpdateChoicePanel.RelNotesLink.Text = "Ver notas de la versión"
                    UpdateChoicePanel.TextBox1.Left = 178
                    UpdateChoicePanel.TextBox1.Width = 701
                    UpdateChoicePanel.TextBox2.Left = 228
                    UpdateChoicePanel.TextBox2.Width = 651
                ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        UpdateChoicePanel.Label1.Text = "Updates are available"
                        UpdateChoicePanel.Label2.Text = "You can decide when do you want to install this update"
                        UpdateChoicePanel.Label3.Text = "This version:"
                        UpdateChoicePanel.Label4.Text = "Up-to-date version:"
                        UpdateChoicePanel.Label5.Text = "When you click " & Quote & "Install now" & Quote & ", the program will exit and update to the latest version."
                        UpdateChoicePanel.OK_Button.Text = "Install now"
                        UpdateChoicePanel.Cancel_Button.Text = "Install later"
                        UpdateChoicePanel.RelNotesLink.Text = "View release notes"
                        UpdateChoicePanel.TextBox1.Left = 177
                        UpdateChoicePanel.TextBox1.Width = 702
                        UpdateChoicePanel.TextBox2.Left = 226
                        UpdateChoicePanel.TextBox2.Width = 653
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        UpdateChoicePanel.Label1.Text = "Hay actualizaciones disponibles"
                        UpdateChoicePanel.Label2.Text = "Usted puede decidir cuándo quiere instalar esta actualización"
                        UpdateChoicePanel.Label3.Text = "Esta versión:"
                        UpdateChoicePanel.Label4.Text = "Versión actualizada:"
                        UpdateChoicePanel.Label5.Text = "Cuando haga clic en " & Quote & "Instalar ahora" & Quote & ", el programa se cerrará y se actualizará a la última versión."
                        UpdateChoicePanel.OK_Button.Text = "Instalar ahora"
                        UpdateChoicePanel.Cancel_Button.Text = "Instalar después"
                        UpdateChoicePanel.RelNotesLink.Text = "Ver notas de la versión"
                        UpdateChoicePanel.TextBox1.Left = 178
                        UpdateChoicePanel.TextBox1.Width = 701
                        UpdateChoicePanel.TextBox2.Left = 228
                        UpdateChoicePanel.TextBox2.Width = 651
                    End If
                End If
            End If
        End If
        If MiniModeDialog.Visible = True Then
            Try
                MiniModeDialog.tbPic.Left = MiniModeDialog.TrueLoc
                MiniModeDialog.CheckBox1.Left = MiniModeDialog.TrueLoc + 12
                MiniModeDialog.PictureBox2.Left = MiniModeDialog.TrueLoc + 39
                MiniModeDialog.Label1.Left = MiniModeDialog.TrueLoc + 104
                MiniModeDialog.OK_Button.Left = MiniModeDialog.TrueLoc + 741
                MiniModeDialog.backPic.SizeMode = PictureBoxSizeMode.Zoom
                MiniModeDialog.backPic.Image = backgroundPic.Image
                If BackColor = Color.FromArgb(243, 243, 243) Then
                    MiniModeDialog.tbPic.Image = New Bitmap(My.Resources.tb_White)
                    MiniModeDialog.CheckBox1.ForeColor = Color.Black
                ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
                    MiniModeDialog.tbPic.Image = New Bitmap(My.Resources.tb_Black)
                    MiniModeDialog.CheckBox1.ForeColor = Color.White
                End If
            Catch ex As Exception
                If BackColor = Color.FromArgb(243, 243, 243) Then
                    MiniModeDialog.backPic.Image = New Bitmap(My.Resources.Bloom_Light)
                    MiniModeDialog.tbPic.Image = New Bitmap(My.Resources.tb_White)
                    MiniModeDialog.CheckBox1.ForeColor = Color.Black
                ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
                    MiniModeDialog.backPic.Image = New Bitmap(My.Resources.Bloom_Dark)
                    MiniModeDialog.tbPic.Image = New Bitmap(My.Resources.tb_Black)
                    MiniModeDialog.CheckBox1.ForeColor = Color.White
                End If
            End Try
            MiniModeDialog.backPic.BackColor = BackColor
        End If
    End Sub

    Private Sub WelcomePic_MouseHover(sender As Object, e As EventArgs) Handles WelcomePic.MouseHover
        ConglomerateToolTip.SetToolTip(WelcomePic, "Welcome page")
    End Sub

    Private Sub InstCreatePic_MouseHover(sender As Object, e As EventArgs) Handles InstCreatePic.MouseHover
        ConglomerateToolTip.SetToolTip(InstCreatePic, "Create a custom installer")
    End Sub

    Private Sub InstructionPic_MouseHover(sender As Object, e As EventArgs) Handles InstructionPic.MouseHover
        ConglomerateToolTip.SetToolTip(InstructionPic, "Instructions")
    End Sub

    Private Sub HelpPic_MouseHover(sender As Object, e As EventArgs) Handles HelpPic.MouseHover
        ConglomerateToolTip.SetToolTip(HelpPic, "Help")
    End Sub

    Private Sub SettingsPic_MouseHover(sender As Object, e As EventArgs) Handles SettingsPic.MouseHover
        ConglomerateToolTip.SetToolTip(SettingsPic, "Settings")
    End Sub

    Private Sub InfoPic_MouseHover(sender As Object, e As EventArgs) Handles InfoPic.MouseHover
        ConglomerateToolTip.SetToolTip(InfoPic, "About")
    End Sub

    Private Sub FrenchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FrenchToolStripMenuItem.Click
        LangInt = 3
        If ComboBox4.Items.Contains("French") Then
            ComboBox4.SelectedItem = "French"
        ElseIf ComboBox4.Items.Contains("Francés") Then
            ComboBox4.SelectedItem = "Francés"
        ElseIf ComboBox4.Items.Contains("Français") Then
            ComboBox4.SelectedItem = "Français"
        End If
    End Sub

    Private Sub x86_Pic_MouseHover(sender As Object, e As EventArgs) Handles x86_Pic.MouseHover
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            MsgBox("The program has detected a 32-bit processor or a 32-bit operating system. The tool will still work, but the installer won't. You'll need a computer with a 64-bit processor to install Windows 11." & CrLf & "If it's the latter (you have a 32-bit OS on a 64-bit processor), you'll need to reinstall Windows.", vbOKOnly + vbInformation, "Incompatible installer architecture")
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            MsgBox("El programa ha detectado un procesador o un sistema operativo de 32 bits. La herramienta todavía funcionará, pero el instalador no lo hará. Necesitará un ordenador con un procesador de 64 bits para instalar Windows 11." & CrLf & "Si es lo último (tiene un sistema operativo de 32 bits en un procesador de 64 bits), deberá reinstalar Windows.", vbOKOnly + vbInformation, "Arquitectura del instalador incompatible")
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                MsgBox("The program has detected a 32-bit processor or a 32-bit operating system. The tool will still work, but the installer won't. You'll need a computer with a 64-bit processor to install Windows 11." & CrLf & "If it's the latter (you have a 32-bit OS on a 64-bit processor), you'll need to reinstall Windows.", vbOKOnly + vbInformation, "Incompatible installer architecture")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                MsgBox("El programa ha detectado un procesador o un sistema operativo de 32 bits. La herramienta todavía funcionará, pero el instalador no lo hará. Necesitará un ordenador con un procesador de 64 bits para instalar Windows 11." & CrLf & "Si es lo último (tiene un sistema operativo de 32 bits en un procesador de 64 bits), deberá reinstalar Windows.", vbOKOnly + vbInformation, "Arquitectura del instalador incompatible")
            End If
        End If
        ConglomerateToolTip.SetToolTip(x86_Pic, "This system contains a 32-bit processor or operating system. Click here to learn more.")
    End Sub

    Private Sub x86_Pic_Click(sender As Object, e As EventArgs) Handles x86_Pic.Click
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Or ComboBox4.SelectedItem = "Anglais" Then
            MsgBox("The program has detected a 32-bit processor or a 32-bit operating system. The tool will still work, but the installer won't. You'll need a computer with a 64-bit processor to install Windows 11." & CrLf & "If it's the latter (you have a 32-bit OS on a 64-bit processor), you'll need to reinstall Windows.", vbOKOnly + vbInformation, "Incompatible installer architecture")
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Or ComboBox4.SelectedItem = "Espagnol" Then
            MsgBox("El programa ha detectado un procesador o un sistema operativo de 32 bits. La herramienta todavía funcionará, pero el instalador no lo hará. Necesitará un ordenador con un procesador de 64 bits para instalar Windows 11." & CrLf & "Si es lo último (tiene un sistema operativo de 32 bits en un procesador de 64 bits), deberá reinstalar Windows.", vbOKOnly + vbInformation, "Arquitectura del instalador incompatible")
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Or ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                MsgBox("The program has detected a 32-bit processor or a 32-bit operating system. The tool will still work, but the installer won't. You'll need a computer with a 64-bit processor to install Windows 11." & CrLf & "If it's the latter (you have a 32-bit OS on a 64-bit processor), you'll need to reinstall Windows.", vbOKOnly + vbInformation, "Incompatible installer architecture")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                MsgBox("El programa ha detectado un procesador o un sistema operativo de 32 bits. La herramienta todavía funcionará, pero el instalador no lo hará. Necesitará un ordenador con un procesador de 64 bits para instalar Windows 11." & CrLf & "Si es lo último (tiene un sistema operativo de 32 bits en un procesador de 64 bits), deberá reinstalar Windows.", vbOKOnly + vbInformation, "Arquitectura del instalador incompatible")
            End If
        End If
    End Sub

    Private Sub Win11FileSpecDialog_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Win11FileSpecDialog.FileOk
        TextBox1.Text = Win11FileSpecDialog.FileName
    End Sub

    Private Sub Win10FileSpecDialog_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Win10FileSpecDialog.FileOk
        TextBox2.Text = Win10FileSpecDialog.FileName
    End Sub

    Private Sub LinkLabel18_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel18.LinkClicked
        MsgBox("Most of the functionality settings are disabled because an installer is being created. If you changed them right now, unexpected things related to the installer creation, which may negatively affect the result, will occur. After installer creation, these settings will be enabled once again", vbOKOnly + vbInformation, "Functionality settings")
    End Sub
End Class
