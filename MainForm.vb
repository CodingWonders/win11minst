Imports Microsoft.Win32
Imports System.IO
Imports Microsoft.VisualBasic.ControlChars
Imports System.Windows.Forms.FlatStyle      ' This imports the various FlatStyle settings
Imports System.Text.Encoding


Public Class MainForm
    Private isMouseDown As Boolean = False
    Private mouseOffset As Point
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
    ' These variables are used by the LogBox
    Dim StInstCreateTime As Date    ' These variables aren't set when the program loads, only on the installer creation process
    Dim EnInstCreateTime As Date    ' <---|
    ' This variable is used to kill external processes
    Dim KillExtCmd As String
    ' These variables are used when aborting an installer creation when using REGTWEAK
    Dim RegUnload As String
    Dim DismUnmount As String
    ' These _Local variables are used when the ISO files are copied to the local disk
    Dim W11IWIMISOEx_Local As String = "echo off" & CrLf & ".\bin\7z e " & Quote & ".\Win11.iso" & Quote & " " & Quote & "sources\install.wim" & Quote & " -o."
    Dim W10ISOEx_Local As String = "echo off" & CrLf & ".\bin\7z x " & Quote & ".\Win10.iso" & Quote & " " & Quote & "-o.\temp" & Quote
    Dim W11ISOEx_Local As String = "echo off" & CrLf & ".\bin\7z x " & Quote & ".\Win11.iso" & Quote & " " & Quote & "-o.\temp" & Quote
    Dim W10AR_ARRDLLEx_Local As String = "echo off" & CrLf & ".\bin\7z e " & Quote & ".\Win10.iso" & Quote & " " & Quote & "sources\appraiser.dll" & Quote & " -o."
    Dim W10AR_ARRDLLEx_2_Local As String = "echo off" & CrLf & ".\bin\7z e " & Quote & ".\Win10.iso" & Quote & " " & Quote & "sources\appraiserres.dll" & Quote & " -o."
    ' These variables are used when deleting temporary files (as integers)
    Dim FileCount As Integer
    Dim DelFileCount As Integer
    Dim EmergencyFileDeleteCount As Integer

    Dim UpdateCheckDate As Date
    Dim DialogInt As Integer

    ' Left mouse button pressed
    Private Sub titlePanel_MouseDown(sender As Object, e As MouseEventArgs) Handles titlePanel.MouseDown, Label12.MouseDown, TitleBar.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ' Get the new position
            mouseOffset = New Point(-e.X, -e.Y)
            ' Set that left button is pressed
            isMouseDown = True
        End If
    End Sub

    '' Source from "sourcecodester.com"
    'Private Sub Round(obj As Form)

    '    obj.FormBorderStyle = FormBorderStyle.None
    '    If BackColor = Color.FromArgb(243, 243, 243) Then
    '        obj.BackColor = Color.FromArgb(243, 243, 243)
    '    ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
    '        obj.BackColor = Color.FromArgb(32, 32, 32)
    '    End If


    '    Dim DGP As New Drawing2D.GraphicsPath
    '    DGP.StartFigure()
    '    'top left corner
    '    DGP.AddArc(New Rectangle(0, 0, 8, 8), 180, 90)
    '    DGP.AddLine(40, 0, obj.Width - 40, 0)

    '    'top right corner
    '    DGP.AddArc(New Rectangle(obj.Width - 8, 0, 8, 20), -90, 90)
    '    DGP.AddLine(obj.Width, 40, obj.Width, obj.Height - 40)

    '    'bottom right corner
    '    DGP.AddArc(New Rectangle(obj.Width - 40, obj.Height - 5, 8, 8), 0, 90)
    '    DGP.AddLine(obj.Width - 40, obj.Height, 40, obj.Height)

    '    'bottom left corner
    '    DGP.AddArc(New Rectangle(0, obj.Height - 40, 40, 40), 90, 90)
    '    DGP.CloseFigure()

    '    obj.Region = New Region(DGP)
    'End Sub

    ' MouseMove used to check if mouse cursor is moving
    Private Sub titlePanel_MouseMove(sender As Object, e As MouseEventArgs) Handles titlePanel.MouseMove, Label12.MouseMove, TitleBar.MouseMove
        If isMouseDown Then
            Dim mousePos As Point = Control.MousePosition
            ' Get the new form position
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Location = mousePos
        End If
    End Sub

    ' Left mouse button released, form should stop moving
    Private Sub titlePanel_MouseUp(sender As Object, e As MouseEventArgs) Handles titlePanel.MouseUp, Label12.MouseUp, TitleBar.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            isMouseDown = False
        End If
    End Sub

    Private Sub closeBox_Click(sender As Object, e As EventArgs) Handles closeBox.Click, LogoPic.MouseDoubleClick
        SaveSettingsFile()
        If CheckBox3.Checked = True Then
            ' The following code snippet determines the check state of "Don't show this again"
            If MiniModeDialog.CheckBox1.Checked = False Then
                MiniModeDialog.Show()
            End If
            Notify.ShowBalloonTip(5)
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
                End If
                Notify.Visible = False
                End
            End If
        End If
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
            If SettingLoadForm.TextBox1.Text.Contains("ColorMode=0") Then       ' This does color mode checkup
                ComboBox1.SelectedItem = "Light"
            ElseIf SettingLoadForm.TextBox1.Text.Contains("ColorMode=1") Then
                ComboBox1.SelectedItem = "Dark"
            ElseIf SettingLoadForm.TextBox1.Text.Contains("ColorMode=2") Then
                If My.Computer.Info.OSFullName.Contains("Windows 7") Or My.Computer.Info.OSFullName.Contains("Windows 8") Or My.Computer.Info.OSFullName.Contains("Windows Server 2008") Or My.Computer.Info.OSFullName.Contains("Windows Server 2012") Then      ' If the program detects these settings, it will default to Light mode as a fallback mechanism
                    ComboBox1.SelectedItem = "Light"
                Else
                    ComboBox1.SelectedItem = "Automatic"
                End If
            End If
            If SettingLoadForm.TextBox1.Text.Contains("Language=0") Then
                ComboBox4.SelectedItem = "English"
            ElseIf SettingLoadForm.TextBox1.Text.Contains("Language=1") Then
                ComboBox4.SelectedItem = "Spanish"
            ElseIf SettingLoadForm.TextBox1.Text.Contains("Language=2") Then
                ComboBox4.SelectedItem = "Automatic"
            End If
            If SettingLoadForm.TextBox1.Text.Contains("NavBarPos=0") Then
                RadioButton3.Checked = True
            ElseIf SettingLoadForm.TextBox1.Text.Contains("NavBarPos=1") Then
                RadioButton3.Checked = False
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
        If ComboBox1.SelectedItem = "Light" Or ComboBox1.SelectedItem = "Claro" Then
            SettingLoadForm.TextBox2.AppendText("ColorMode=0")
        ElseIf ComboBox1.SelectedItem = "Dark" Or ComboBox1.SelectedItem = "Oscuro" Then
            SettingLoadForm.TextBox2.AppendText("ColorMode=1")
        ElseIf ComboBox1.SelectedItem = "Automatic" Or ComboBox1.SelectedItem = "Automático" Then
            SettingLoadForm.TextBox2.AppendText("ColorMode=2")
        End If
        If ComboBox4.SelectedItem = "English" Or ComboBox4.SelectedItem = "Inglés" Then
            SettingLoadForm.TextBox2.AppendText(CrLf & "Language=0")
        ElseIf ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Then
            SettingLoadForm.TextBox2.AppendText(CrLf & "Language=1")
        ElseIf ComboBox4.SelectedItem = "Automatic" Or ComboBox4.SelectedItem = "Automático" Then
            SettingLoadForm.TextBox2.AppendText(CrLf & "Language=2")
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
        If CheckBox1.Checked = True Then
            SettingLoadForm.TextBox2.AppendText(CrLf & "WIMReq=1")
        Else
            SettingLoadForm.TextBox2.AppendText(CrLf & "WIMReq=0")
        End If
        If CheckBox2.Checked = True Then
            SettingLoadForm.TextBox2.AppendText(CrLf & "TempWin10=1")
        Else
            SettingLoadForm.TextBox2.AppendText(CrLf & "TempWin10=0")
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

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim LaunchPSI As New ProcessStartInfo
        'My.Settings.Reload()
        Notify.Visible = False
        If My.Computer.Info.OSFullName.Contains("Windows 7") Or My.Computer.Info.OSFullName.Contains("Windows 8") Or My.Computer.Info.OSFullName.Contains("Windows Server 2008") Or My.Computer.Info.OSFullName.Contains("Windows Server 2012") Then      ' This is done to not show the "Not supported" dialog on Windows 7 and 8/8.1
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Light")
            ComboBox1.Items.Add("Dark")
            ComboBox1.SelectedItem = "Light"
            SettingsPic.Image = New Bitmap(My.Resources.settings)
        Else
            ComboBox1.SelectedItem = "Automatic"
        End If

        If Not Directory.Exists(".\bin") Then
            MissingComponentsDialog.ShowDialog()
        Else
            If Not File.Exists(".\bin\7z.exe") And Not File.Exists(".\bin\7z.dll") Or Not File.Exists(".\bin\oscdimg.exe") Then
                MissingComponentsDialog.ShowDialog()
            End If
        End If
        If File.Exists(".\version") Then
            UpdateCheckPreLoadPanel.ShowDialog()
        Else
            My.Computer.FileSystem.WriteAllText(".\version", "2.0.0100", True, ASCII)
        End If


        ErrorCount = 0
        WarnCount = 0
        MessageCount = 0
        If My.User.IsAuthenticated = True Then
            If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
                AdminLabel.Visible = True
            Else
                AdminLabel.Visible = False
                ComboBox5.Items.Clear()
                ComboBox5.Items.Add("WIMR")
                ComboBox5.Items.Add("DLLR")
                ComboBox5.SelectedItem = "WIMR"
                ' Set the REGTWEAK CMS element invisible
                REGTWEAKToolStripMenuItem.Visible = False
            End If
        End If
        LoadSettingsFile()
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
            ' Position changes
            LineShape1.Y1 = 452
            LineShape1.Y2 = 452
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
        Button11.FlatStyle = FlatStyle.System
        Button12.FlatStyle = FlatStyle.System
        Button13.FlatStyle = FlatStyle.System

        ' Space intentionally left for more buttons
        ScanButton.FlatStyle = FlatStyle.System
        LabelSetButton.FlatStyle = FlatStyle.System
        SetDefaultButton.FlatStyle = FlatStyle.System
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
        StatusTSI.Text = "No installers are being created at this time"
        Dim rkWallPaper As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", False)
        Dim WallpaperPath As String = rkWallPaper.GetValue("WallPaper").ToString()
        rkWallPaper.Close()
        backgroundPic.Image = New Bitmap(WallpaperPath)
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
        ElseIf My.Computer.Info.OSFullName.Contains("Windows 10") Or My.Computer.Info.OSFullName.Contains("Windows Server 2016") Then
            VersionDetector.Show()
            VersionDetector.Hide()
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
            End If
        End If
        CenterToScreen()        ' This is done to prevent a bug where it would not center to the screen!
        Visible = True
        BringToFront()
        BackSubPanel.Show()
        DisclaimerPanel.ShowDialog()
        DisclaimerPanel.Visible = True
        DisclaimerPanel.Visible = False

        If My.Computer.Info.OSFullName.Contains("Windows 11") Then
            MsgBox("This computer (or device) is already running Windows 11. You will not be able to use this tool to upgrade to Windows 11/install Windows 11 on your system, but you can still use it to upgrade to Windows 11/install Windows 11 on other systems.", vbOKOnly + vbInformation, "Already running Windows 11")
        End If
        If Environment.Is64BitOperatingSystem = False Then
            MsgBox("The program has detected a 32-bit processor or a 32-bit operating system. The tool will still work, but the installer won't. You'll need a computer with a 64-bit processor to install Windows 11." & CrLf & "If it's the latter (you have a 32-bit OS on a 64-bit processor), you'll need to reinstall Windows.", vbOKOnly + vbInformation, "Incompatible installer architecture")
        End If
        If My.Computer.Info.OSFullName.Contains("Windows Server") Then
            MsgBox("This computer is running a Windows Server operating system. To achieve optimal results on incompatible servers, please create Copper or Nickel build installers of Windows Server.", vbOKOnly + vbInformation, "Windows Server detected")
        End If
        LineShape1.Visible = True
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
    End Sub

    Private Sub closeBox_MouseUp(sender As Object, e As MouseEventArgs) Handles closeBox.MouseUp
        closeBox.Image = New Bitmap(My.Resources.closebox_focus)
    End Sub

    Private Sub InfoPic_Click(sender As Object, e As EventArgs) Handles InfoPic.Click, PictureBox34.Click, Label102.Click
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
            InfoPic.Image = New Bitmap(My.Resources.info_filled)
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            WelcomePic.Image = New Bitmap(My.Resources.home_dark)
            InfoPic.Image = New Bitmap(My.Resources.info_dark_filled)
        End If
        PanelIndicatorPic.Top = InfoPic.Top + 2
        WelcomeTopBarPic.Visible = False
        InstCreateTopBarPic.Visible = False
        InstructionsTopBarPic.Visible = False
        HelpTopBarPic.Visible = False
        AboutTopBarPic.Visible = True
        SettingsTopBarPic.Visible = False
        ApplyNavBarImages()
    End Sub

    Private Sub WelcomePic_Click(sender As Object, e As EventArgs) Handles WelcomePic.Click, PictureBox8.Click, Label15.Click
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
        ElseIf SettingPanel.Visible = True Then
            WelcomePanel.Visible = True
            SettingPanel.Visible = False
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
        System.Diagnostics.Process.Start("https://www.google.com/search?q=enable+csm+uefi&pccc=1")
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        System.Diagnostics.Process.Start("https://duckduckgo.com/?q=enable+csm+uefi&atb=v297-6&ia=web")
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            PC_DETAIL_Label.Text = "Select this option for broader hardware compatibility"
            LinkLabel3.Visible = False
            LinkLabel4.Visible = False
            Label76.Text = "BIOS/UEFI-CSM (ID: 0x00)"
        Else
            PC_DETAIL_Label.Text = "Select this option for systems that only support UEFI" & CrLf & "NOTE: you can enable UEFI-CSM on your system. If you don't know how, check one of the links below."
            LinkLabel3.Visible = True
            LinkLabel4.Visible = True
            Label76.Text = "UEFI (ID: 0xEF)"
        End If
    End Sub
    'Sub ChangeSettings(ByVal ProgColMode As String)
    '    If ComboBox1.SelectedItem = "Custom" Then
    '        My.Settings.Item(ProgColMode) = "Custom"
    '    ElseIf ComboBox1.SelectedItem = "Light" Then
    '        My.Settings.Item(ProgColMode) = "Light"
    '    ElseIf ComboBox1.SelectedItem = "Dark" Then
    '        My.Settings.Item(ProgColMode) = "Dark"
    '    ElseIf ComboBox1.SelectedItem = "Automatic" Then
    '        My.Settings.Item(ProgColMode) = "Auto"
    '    End If
    '    My.Settings.Save()
    'End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Label31.Text = "Color mode: " & ComboBox1.SelectedItem & " mode"
        If ComboBox1.SelectedItem = "Dark" Then
            If RadioButton3.Checked = True Then
                PictureBox2.Image = New Bitmap(My.Resources.NavBar_LeftPos_Dark)
            Else
                PictureBox2.Image = New Bitmap(My.Resources.NavBar_TopPos_Dark)
            End If
            PictureBox11.Image = New Bitmap(My.Resources.Win11_LogoDark)
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
            minBox.Image = New Bitmap(My.Resources.minBox_dark)
            ' This has changed to determine window state
            If WindowState = FormWindowState.Maximized Then
                maxBox.Image = New Bitmap(My.Resources.restdownbox_dark)
            Else
                maxBox.Image = New Bitmap(My.Resources.maxbox_dark)
            End If
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
            back_Pic.Image = New Bitmap(My.Resources.back_arrow_dark)
            PictureBox11.Image = New Bitmap(My.Resources.Win11_LogoDark)
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



            ' This is for the Group Box
            GroupBox2.ForeColor = Color.White
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
            LogBox.BackColor = Color.FromArgb(43, 43, 43)
            LogBox.ForeColor = Color.White
            LabelText.BackColor = Color.FromArgb(43, 43, 43)
            LabelText.ForeColor = Color.White

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

            LineShape1.BorderColor = Color.FromArgb(50, 50, 50)
            TabPage1.BackColor = Color.FromArgb(39, 39, 39)
            TabPage2.BackColor = Color.FromArgb(39, 39, 39)
            TabPage3.BackColor = Color.FromArgb(39, 39, 39)

            NavBar.ForeColor = Color.White
        ElseIf ComboBox1.SelectedItem = "Light" Then
            If RadioButton3.Checked = True Then
                PictureBox2.Image = New Bitmap(My.Resources.NavBar_LeftPos_Light)
            Else
                PictureBox2.Image = New Bitmap(My.Resources.NavBar_TopPos_Light)
            End If
            PictureBox11.Image = New Bitmap(My.Resources.Win11_LogoWhite)
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
            minBox.Image = New Bitmap(My.Resources.minBox)
            If WindowState = FormWindowState.Maximized Then
                maxBox.Image = New Bitmap(My.Resources.restdownbox)
            Else
                maxBox.Image = New Bitmap(My.Resources.maxbox)
            End If
            closeBox.Image = New Bitmap(My.Resources.closebox)
            back_Pic.Image = New Bitmap(My.Resources.back_arrow)
            PictureBox11.Image = New Bitmap(My.Resources.Win11_LogoWhite)
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
            PanelIndicatorPic.Image = New Bitmap(My.Resources.panel_indicator_light)
            CompPic.Image = New Bitmap(My.Resources.comp_light)

            GroupBox2.ForeColor = Color.Black
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
            LogBox.BackColor = Color.FromArgb(249, 249, 249)
            LogBox.ForeColor = Color.Black
            LabelText.BackColor = Color.FromArgb(249, 249, 249)
            LabelText.ForeColor = Color.Black

            ComboBox1.BackColor = Color.FromArgb(249, 249, 249)
            ComboBox4.BackColor = Color.FromArgb(249, 249, 249)
            ComboBox5.BackColor = Color.FromArgb(249, 249, 249)
            ComboBox1.ForeColor = Color.Black
            ComboBox4.ForeColor = Color.Black
            ComboBox5.ForeColor = Color.Black

            Notify.Icon = My.Resources.NotifyIconRes_Light
            LineShape1.BorderColor = Color.FromArgb(205, 205, 205)
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
        ElseIf ComboBox1.SelectedItem = "Automatic" Then
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
                        minBox.Image = New Bitmap(My.Resources.minBox_dark)
                        If WindowState = FormWindowState.Maximized Then
                            maxBox.Image = New Bitmap(My.Resources.restdownbox_dark)
                        Else
                            maxBox.Image = New Bitmap(My.Resources.maxbox_dark)
                        End If
                        closeBox.Image = New Bitmap(My.Resources.closebox_dark)
                        back_Pic.Image = New Bitmap(My.Resources.back_arrow_dark)
                        PictureBox11.Image = New Bitmap(My.Resources.Win11_LogoDark)
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
                        GroupBox2.ForeColor = Color.White
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
                        LogBox.BackColor = Color.FromArgb(43, 43, 43)
                        LogBox.ForeColor = Color.White
                        LabelText.BackColor = Color.FromArgb(43, 43, 43)
                        LabelText.ForeColor = Color.White
                        ComboBox1.BackColor = Color.FromArgb(39, 39, 39)
                        ComboBox4.BackColor = Color.FromArgb(39, 39, 39)
                        ComboBox5.BackColor = Color.FromArgb(39, 39, 39)
                        ComboBox1.ForeColor = Color.White
                        ComboBox4.ForeColor = Color.White
                        ComboBox5.ForeColor = Color.White
                        LineShape1.BorderColor = Color.FromArgb(50, 50, 50)
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
                        If WindowState = FormWindowState.Maximized Then
                            maxBox.Image = New Bitmap(My.Resources.restdownbox)
                        Else
                            maxBox.Image = New Bitmap(My.Resources.maxbox)
                        End If
                        maxBox.Image = New Bitmap(My.Resources.maxbox)
                        closeBox.Image = New Bitmap(My.Resources.closebox)
                        back_Pic.Image = New Bitmap(My.Resources.back_arrow)
                        PictureBox11.Image = New Bitmap(My.Resources.Win11_LogoWhite)
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
                        PanelIndicatorPic.Image = New Bitmap(My.Resources.panel_indicator_light)
                        CompPic.Image = New Bitmap(My.Resources.comp_light)
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
                        GroupBox2.ForeColor = Color.Black
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
                        LogBox.BackColor = Color.FromArgb(249, 249, 249)
                        LogBox.ForeColor = Color.Black
                        LabelText.BackColor = Color.FromArgb(249, 249, 249)
                        LabelText.ForeColor = Color.Black
                        ComboBox1.BackColor = Color.FromArgb(249, 249, 249)
                        ComboBox4.BackColor = Color.FromArgb(249, 249, 249)
                        ComboBox5.BackColor = Color.FromArgb(249, 249, 249)
                        ComboBox1.ForeColor = Color.Black
                        ComboBox4.ForeColor = Color.Black
                        ComboBox5.ForeColor = Color.Black
                        LineShape1.BorderColor = Color.FromArgb(205, 205, 205)
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
                MsgBox("Cannot set the color mode to match the system's. Perhaps the registry key is not available.", vbOKOnly + vbExclamation, "Automatic color mode")
            End Try
        End If
    End Sub

    Private Sub SettingsPic_Click(sender As Object, e As EventArgs) Handles SettingsPic.Click, PictureBox7.Click
        PanelIndicatorPic.Top = SettingsPic.Top + 2
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
        back_Pic.Visible = True
        If BackColor = Color.FromArgb(243, 243, 243) Then
            NavBarBackPic.Image = New Bitmap(My.Resources.back_arrow)
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            NavBarBackPic.Image = New Bitmap(My.Resources.back_arrow_dark)
        End If
        LogoPic.Left = 48
        ProgramTitleLabel.Left = 102
        SettingPanel.Visible = False
        Settings_PersonalizationPanel.Visible = True
        If DialogInt = 0 Then
            DialogInt += 1
            If My.Computer.Info.OSFullName.Contains("Windows 7") Or My.Computer.Info.OSFullName.Contains("Windows 8") Then
                MsgBox("The automatic color mode option is not accessible under Windows 7 and Windows 8/8.1, because they do not support dark mode." & CrLf & "The way this program sets the color mode is by looking at the " & Quote & "AppsUseLightMode" & Quote & " registry key, which is present since Windows 10 version 1809 (October 2018 Update), and is carried on by further versions of Windows 10, and Windows 11." & CrLf & "- If its value is 0, then the program will use dark mode" & CrLf & "- If its value is 1, then the program will use light mode" & CrLf & CrLf & "You can still set the color mode manually.", vbOKOnly + vbInformation, "Automatic color mode")
            End If
        End If

    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Label28.Text = "DPI scale to be applied: " & TrackBar1.Value & "%"
    End Sub

    Private Sub back_Pic_Click(sender As Object, e As EventArgs) Handles back_Pic.Click, NavBarBackPic.Click
        If Settings_PersonalizationPanel.Visible = True Then
            SettingPanel.Visible = True
            Settings_PersonalizationPanel.Visible = False
        End If
        If Settings_FunctionalityPanel.Visible = True Then
            SettingPanel.Visible = True
            Settings_FunctionalityPanel.Visible = False
        End If
        back_Pic.Visible = False
        NavBarBackPic.Image = New Bitmap(My.Resources.back_arrow_disabled)
        LogoPic.Left = 19
        ProgramTitleLabel.Left = 73
    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click
        back_Pic.Visible = True
        If BackColor = Color.FromArgb(243, 243, 243) Then
            NavBarBackPic.Image = New Bitmap(My.Resources.back_arrow)
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            NavBarBackPic.Image = New Bitmap(My.Resources.back_arrow_dark)
        End If
        LogoPic.Left = 48
        ProgramTitleLabel.Left = 102
        SettingPanel.Visible = False
        Settings_FunctionalityPanel.Visible = True
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
        Activate()
        ShowInTaskbar = True
        WindowState = FormWindowState.Normal
        MiniModeDialog.Hide()
        BringToFront()
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
        PanelIndicatorPic.Top = InstCreatePic.Top + 2
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
        ElseIf InstCreateInt = 2 Then
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
            Win11PresenceSTLabel.Text = "Presence status: this file exists"
        Else
            Win11PresenceSTLabel.Text = "Presence status: this file doesn't exist"
        End If
        If TextBox1.Text = "" Then
            Win11PresenceSTLabel.Text = "Presence status: unknown"
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If File.Exists(TextBox2.Text) Then
            Win10PresenceSTLabel.Text = "Presence status: this file exists"
        Else
            Win10PresenceSTLabel.Text = "Presence status: this file doesn't exist"
        End If
        If TextBox2.Text = "" Then
            Win10PresenceSTLabel.Text = "Presence status: unknown"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Win11FileSpecDialog.ShowDialog()
        If DialogResult.OK Then
            TextBox1.Text = Win11FileSpecDialog.FileName
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Win10FileSpecDialog.ShowDialog()
        If DialogResult.OK Then
            TextBox2.Text = Win10FileSpecDialog.FileName
        End If
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
        If TextBox3.Text = "" Then
            MsgBox("The file name cannot be nothing. Please type a file name and try again.", vbOKOnly + vbCritical, "File name error")
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
        back_Pic.Visible = True
        LogoPic.Left = 48
        ProgramTitleLabel.Left = 102
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
        Label61.ForeColor = Color.Gray
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        NameLabel.Text = "Name: " & TextBox3.Text

        If TextBox4.Text.EndsWith("\") Then
            If TextBox4.Text.Contains(" ") Then
                ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes."
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
        If TextBox3.Text = "" Then
            Button6.Enabled = False
            MsgBox("The file name cannot be nothing. Please specify a file name and try again.", vbOKOnly + vbInformation, "File name")
        Else
            Button6.Enabled = True
        End If
    End Sub

    Private Sub AdminLabel_MouseHover(sender As Object, e As EventArgs) Handles AdminLabel.MouseHover
        ConglomerateToolTip.SetToolTip(AdminLabel, "This program is running with administrative privileges")
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
        If BackColor = Color.FromArgb(243, 243, 243) Then
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create_filled)
            WelcomePic.Image = New Bitmap(My.Resources.home)
        Else
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create_dark_filled)
            WelcomePic.Image = New Bitmap(My.Resources.home_dark)
        End If
    End Sub

    Private Sub minBox_MouseHover(sender As Object, e As EventArgs) Handles minBox.MouseHover
        ConglomerateToolTip.SetToolTip(minBox, "Minimize")
    End Sub

    Private Sub maxBox_MouseHover(sender As Object, e As EventArgs) Handles maxBox.MouseHover
        If WindowState = FormWindowState.Normal Then
            ConglomerateToolTip.SetToolTip(maxBox, "Maximize")
        ElseIf WindowState = FormWindowState.Maximized Then
            ConglomerateToolTip.SetToolTip(maxBox, "Restore down")
        End If
    End Sub

    Private Sub closeBox_MouseHover(sender As Object, e As EventArgs) Handles closeBox.MouseHover
        ConglomerateToolTip.SetToolTip(closeBox, "Close")
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        InstallerCreationMethodToolStripMenuItem.Enabled = False
        StInstCreateTime = Now
        InstCreateInt = 2
        SettingReviewPanel.Visible = False
        ProgressPanel.Visible = True
        ' The following lines of code reset the *Count variables (declared at the beginning of the file)
        ErrorCount = 0
        WarnCount = 0
        MessageCount = 0
        ' The following line of code clears all text from the LogBox (in case if there's text)...
        LogBox.Clear()
        ' ...and this line of code prints version information on the LogBox
        If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
            LogBox.AppendText("Windows 11 Manual Installer (administrator mode)" & CrLf & "version 2.0.0100")
        Else
            LogBox.AppendText("Windows 11 Manual Installer" & CrLf & "version 2.0.0100")
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
        W11IWIMISOEx = "echo off" & CrLf & ".\bin\7z e " & Quote & TextBox1.Text & Quote & " " & Quote & "sources\install.wim" & Quote & " -o."
        W10ISOEx = "echo off" & CrLf & ".\bin\7z x " & Quote & TextBox2.Text & Quote & " " & Quote & "-o.\temp" & Quote
        W11ISOEx = "echo off" & CrLf & ".\bin\7z x " & Quote & TextBox1.Text & Quote & " " & Quote & "-o.\temp" & Quote
        W10AR_ARRDLLEx = "echo off" & CrLf & ".\bin\7z e " & Quote & TextBox2.Text & Quote & " " & Quote & "sources\appraiser.dll" & Quote & " -o."
        W10AR_ARRDLLEx_2 = "echo off" & CrLf & ".\bin\7z e " & Quote & TextBox2.Text & Quote & " " & Quote & "sources\appraiserres.dll" & Quote & " -o."
        If TextBox4.Text.EndsWith("\") Then
            OSCDIMG_CSM = ".\bin\oscdimg.exe -l" & LabelText.Text & " -m -u2 -b.\temp\boot\etfsboot.com .\temp " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
            OSCDIMG_UEFI = ".\bin\oscdimg.exe -l" & LabelText.Text & " -m -u2 -b.\temp\boot\Efisys.bin -pEF .\temp " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote
        Else
            OSCDIMG_CSM = ".\bin\oscdimg.exe -l" & LabelText.Text & " -m -u2 -b.\temp\boot\etfsboot.com .\temp " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
            OSCDIMG_UEFI = ".\bin\oscdimg.exe -l" & LabelText.Text & " -m -u2 -b.\temp\boot\Efisys.bin -pEF .\temp " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
        End If
        EmergencyFolderDelete = "echo off" & CrLf & "rd " & Quote & ".\temp" & Quote & " /s /q"
        Label84.ForeColor = Color.Gray
        Label84.Font = New Font("Segoe UI", 9.75)
        Label85.ForeColor = Color.Gray
        Label85.Font = New Font("Segoe UI", 9.75)
        Label86.ForeColor = Color.Gray
        Label86.Font = New Font("Segoe UI", 9.75)
        Label87.ForeColor = Color.Gray
        Label87.Font = New Font("Segoe UI", 9.75)
        Label88.ForeColor = Color.Gray
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
        If CheckBox1.Enabled = True Then
            ' Detect if CheckBox1 is checked
            If CheckBox1.Checked = True Then
                LogBox.AppendText(CrLf & "- Require WIM files on both discs? Yes")
            Else
                LogBox.AppendText(CrLf & "- Require WIM files on both discs? No")
            End If
        Else
            LogBox.AppendText(CrLf & "- Require WIM files on both discs? No, using other installer creation method")
        End If
        If CheckBox2.Checked = True Then
            LogBox.AppendText(CrLf & "- Make a temporary Windows 10 installer? Yes")
        Else
            LogBox.AppendText(CrLf & "- Make a temporary Windows 10 installer? No")
        End If
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
        InstSTLabel.Text = "Ready to copy the files to the local disk"
        BringToFront()
        BackSubPanel.Show()
        FileCopyPanel.ShowDialog()
        FileCopyPanel.Visible = True
        FileCopyPanel.Visible = False
        BringToFront()
        If FileCopyPanel.DialogResult = Windows.Forms.DialogResult.Yes Then
            InstSTLabel.Text = "Copying the ISO files to the local disk..."
            LogBox.AppendText(CrLf & "Copying the ISO files to the local disk...")
            Try
                If ComboBox5.SelectedItem = "REGTWEAK" Then
                    File.Copy(TextBox1.Text, ".\Win11.iso")
                    LogBox.AppendText(" Done")
                Else
                    File.Copy(TextBox2.Text, ".\Win10.iso")
                    LogBox.AppendText(" 2/2 Done")
                End If
            Catch DNFEx As DirectoryNotFoundException
                LogBox.AppendText(" Failed")
                InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
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
                            File.Copy(TextBox2.Text, ".\Win10.iso")
                            LogBox.AppendText(" 2/2 Done")
                        End If
                    Catch DNFEx2 As DirectoryNotFoundException
                        LogBox.AppendText(" Failed")
                        InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
                        BringToFront()
                        BackSubPanel.Show()
                        VolumeConnectPanel.ShowDialog()
                        VolumeConnectPanel.Visible = True
                        VolumeConnectPanel.Visible = False
                        BringToFront()
                    Catch FNFEx2 As FileNotFoundException
                        LogBox.AppendText(" Failed")
                        InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
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
                InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
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
                            File.Copy(TextBox2.Text, ".\Win10.iso")
                            LogBox.AppendText(" 2/2 Done")
                        End If
                    Catch DNFEx2 As DirectoryNotFoundException
                        LogBox.AppendText(" Failed")
                        InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
                        BringToFront()
                        BackSubPanel.Show()
                        VolumeConnectPanel.ShowDialog()
                        VolumeConnectPanel.Visible = True
                        VolumeConnectPanel.Visible = False
                        BringToFront()
                    Catch FNFEx2 As FileNotFoundException
                        LogBox.AppendText(" Failed")
                        InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
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
            InstSTLabel.Text = "Skipping file copy..."
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
        Label84.ForeColor = Color.Gray
        Label84.Font = New Font("Segoe UI", 9.75)
        If BackColor = Color.FromArgb(243, 243, 243) Then
            Label85.ForeColor = Color.Black
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            Label85.ForeColor = Color.White
        End If
        Label85.Font = New Font("Segoe UI", 9.75, FontStyle.Bold)
        InstSTLabel.Text = "Gathering instructions..."
        LogBox.AppendText(CrLf & "Gathering instructions needed to create the installer...")
        LogBox.AppendText(" Done")
        InstSTLabel.Text = "Instructions gathered"
        InstallerProgressBar.Value = 15
        CheckPic2.Visible = True
        Label85.ForeColor = Color.Gray
        Label85.Font = New Font("Segoe UI", 9.75)
        If BackColor = Color.FromArgb(243, 243, 243) Then
            Label86.ForeColor = Color.Black
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            Label86.ForeColor = Color.White
        End If
        Label86.Font = New Font("Segoe UI", 9.75, FontStyle.Bold)
        InstSTLabel.Text = "Extracting the necessary files from the ISO images..."
        LogBox.AppendText(CrLf & "Extracting the necessary contents from the ISO images using the " & ComboBox5.SelectedItem & " method...")
        If ComboBox5.SelectedItem = "WIMR" Then
            If File.Exists(".\Win11.iso") And File.Exists(".\Win10.iso") Then
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11IWIMISOEx_Local & CrLf & W10ISOEx_Local, ASCII)
            Else
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11IWIMISOEx & CrLf & W10ISOEx, ASCII)
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
        InstSTLabel.Text = "Necessary files extracted"
        LogBox.AppendText(" Done")
        InstallerProgressBar.Value = 25
        CheckPic3.Visible = True
        Label86.Font = New Font("Segoe UI", 9.75)
        Label86.ForeColor = Color.Gray
        If BackColor = Color.FromArgb(243, 243, 243) Then
            Label87.ForeColor = Color.Black
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            Label87.ForeColor = Color.White
        End If
        Label87.Font = New Font("Segoe UI", 9.75, FontStyle.Bold)
        InstSTLabel.Text = "Creating the custom installer..."
        LogBox.AppendText(CrLf & "Creating the custom installer using the " & ComboBox5.SelectedItem & " method...")
        If ComboBox5.SelectedItem = "WIMR" Then
            LogBox.AppendText(CrLf & "Deleting " & Quote & "install.wim" & Quote & " from the Windows 10 installation media...")
            File.Delete(".\temp\sources\install.wim")
            LogBox.AppendText(" Done")
            LogBox.AppendText(CrLf & "Moving " & Quote & "install.wim" & Quote & " from the Windows 11 installation media to the Windows 10 installer...")
            File.Move(".\install.wim", ".\temp\sources\install.wim")
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
            If AdvancedOptionsPanel.CheckBox1.Checked = True Then   ' Prepare boot.wim (and install.wim) for surgery
                Process.Start(".\bin\regtweak.bat", "/bypassnro").WaitForExit()
            Else
                Process.Start(".\bin\regtweak.bat").WaitForExit()
            End If

            LogBox.AppendText(" Done" & CrLf & "Moving " & Quote & "boot.wim" & Quote & " to the Windows 11 installer...")
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
                WarnCount = WarnCount + 1
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & OSCDIMG_CSM, ASCII)
            End If
            Process.Start(".\temp.bat").WaitForExit()
        End If
        LogBox.AppendText(" Done")
        InstSTLabel.Text = "Finished creating the installer. Now deleting temporary files..."
        InstallerProgressBar.Value = 75
        CheckPic4.Visible = True
        Label87.ForeColor = Color.Gray
        Label87.Font = New Font("Segoe UI", 9.75)
        LogBox.AppendText(CrLf & "Gathering file count...")
        If BackColor = Color.FromArgb(243, 243, 243) Then
            Label88.ForeColor = Color.Black
        ElseIf BackColor = Color.FromArgb(32, 32, 32) Then
            Label88.ForeColor = Color.White
        End If
        Label88.Font = New Font("Segoe UI", 9.75, FontStyle.Bold)
        For Each fileGather In My.Computer.FileSystem.GetFiles(".\temp", FileIO.SearchOption.SearchAllSubDirectories)
            FileCount = FileCount + 1
        Next
        For Each deletedFile In My.Computer.FileSystem.GetFiles(".\temp", FileIO.SearchOption.SearchAllSubDirectories)
            DelFileCount = DelFileCount + 1
            MessageCount = MessageCount + 1
            Try
                LogBox.AppendText(CrLf & "Deleted file: " & DelFileCount & "/" & FileCount)
                File.Delete(deletedFile)
            Catch PTLEx As PathTooLongException
                EmergencyFileDeleteCount = EmergencyFileDeleteCount + 1
                WarnCount = WarnCount + 1
                File.WriteAllText(".\emergencydelete.bat", OffEcho & "del " & Quote & deletedFile & Quote & " /f /q", ASCII)
                Process.Start(".\emergencydelete.bat").WaitForExit()
                File.Delete(".\emergencydelete.bat")
                LogBox.AppendText(CrLf & "Deleted file: " & DelFileCount & "/" & FileCount & ". Files that had to be deleted manually: " & EmergencyFileDeleteCount)
            Catch DNFDelEx As DirectoryNotFoundException
                WarnCount = WarnCount + 1
                EmergencyFileDeleteCount = EmergencyFileDeleteCount + 1
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
            WarnCount = WarnCount + 1
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
                        File.Delete(".\inst.log.bak")
                    Else
                        File.Move(".\log.txt", ".\inst.log")
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf, True)
                        My.Computer.FileSystem.WriteAllText(".\inst.log", "----------------------------------------------------------------", True)
                        My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf, True)
                        My.Computer.FileSystem.WriteAllText(".\inst.log", LogBox.Text, True)
                    End If
                ElseIf LogMigratePanel.DialogResult = Windows.Forms.DialogResult.No Then
                    My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf, True)
                    My.Computer.FileSystem.WriteAllText(".\inst.log", "----------------------------------------------------------------", True)
                    My.Computer.FileSystem.WriteAllText(".\inst.log", CrLf, True)
                    My.Computer.FileSystem.WriteAllText(".\inst.log", LogBox.Text, True)
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
                Else
                    My.Computer.FileSystem.WriteAllText(".\inst.log", LogBox.Text, True)
                End If
            End If
        Else
            My.Computer.FileSystem.WriteAllText(".\inst.log", LogBox.Text, True)
        End If
        Notify.ShowBalloonTip(5, "Finished creating the custom installer", "Details: " & CrLf & "Installer creation started at: " & StInstCreateTime & CrLf & "Installer creation finished at: " & EnInstCreateTime & CrLf & "Installer name: " & TextBox3.Text & CrLf & "Installer location: " & TextBox4.Text & CrLf & "Please review the log for further details.", ToolTipIcon.Info)
        CheckPic5.Visible = True
        InstCreateInt = 3
        Label88.Font = New Font("Segoe UI", 9.75)
        Label88.ForeColor = Color.Gray
        My.Computer.Audio.Play(My.Resources.Win11, AudioPlayMode.Background)
        Label82.Text = "Finish"
        CompPic.Visible = True
        Button11.Visible = True
        Label83.Text = "The custom installer was created at the specified location." & CrLf & "Just as a reminder, you saved the installer on:" & CrLf & CrLf & TextBox4.Text
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
        Button10.Text = "OK"
        If TextBox4.Text.EndsWith("\") Then
            InstHistPanel.InstallerListView.Items.Add(TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso").SubItems.Add(EnInstCreateTime)
        Else
            InstHistPanel.InstallerListView.Items.Add(TextBox4.Text & "\" & TextBox3.Text & ".iso").SubItems.Add(EnInstCreateTime)
        End If
        InstHistPanel.InstallerEntryLabel.Text = "Installer history entries: " & InstHistPanel.InstallerListView.Items.Count
    End Sub

    Private Sub Label61_Click(sender As Object, e As EventArgs) Handles Label61.Click
        InstCreateInt = 0
        SettingReviewPanel.Visible = False
        InstCreatePanel.Visible = True
        back_Pic.Visible = False
        LogoPic.Left = 19
        ProgramTitleLabel.Left = 73
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        InstCreateInt = 0
        SettingReviewPanel.Visible = False
        InstCreatePanel.Visible = True
        back_Pic.Visible = False
        LogoPic.Left = 19
        ProgramTitleLabel.Left = 73
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
            System.Diagnostics.Process.Start("ms-settings:about")
        Else
            System.Diagnostics.Process.Start("sysdm.cpl")
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        File.WriteAllText(".\viewinexplorer.bat", OffEcho & CrLf & "explorer " & Quote & TextBox4.Text & Quote, ASCII)
        Process.Start(".\viewinexplorer.bat").WaitForExit()
        File.Delete(".\viewinexplorer.bat")
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If TextBox4.Text.EndsWith("\") Then
            If TextBox4.Text.Contains(" ") Then
                ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text.TrimEnd("\") & "\" & TextBox3.Text & ".iso" & Quote & ". Path will contain quotes."
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
        If TextBox4.Text = "" Then
            Button6.Enabled = False
            MsgBox("The path cannot be nothing. The program will instead use the user folder to store the target installer.", vbOKOnly + vbInformation, "Target installer path")
            If DialogResult.OK Then
                TextBox4.Text = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            End If
        Else
            Button6.Enabled = True
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ImageFolderBrowser.ShowDialog()
        If DialogResult.OK Then
            TextBox4.Text = ImageFolderBrowser.SelectedPath
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If Button10.Text = "Cancel" Then

        ElseIf Button10.Text = "OK" Then
            If CheckBox4.Checked = True Then
                ' Leave the program
                Notify.Visible = False
                End
            ElseIf CheckBox4.Checked = False Then
                InstCreateInt = 0
                ProgressPanel.Visible = False
                InstCreatePanel.Visible = True
            End If
        End If
    End Sub

    Private Sub GroupBox8_MouseHover(sender As Object, e As EventArgs) Handles GroupBox8.MouseHover
        ConglomerateToolTip.SetToolTip(GroupBox8, "These are the images used to create the custom installer")
    End Sub

    Private Sub GroupBox9_MouseHover(sender As Object, e As EventArgs) Handles GroupBox9.MouseHover
        ConglomerateToolTip.SetToolTip(GroupBox9, "These are the settings used to create the custom installer. To change those, click " & Quote & "No" & Quote & ", and go to Settings (by clicking the gear) > Functionality")
    End Sub

    Private Sub GroupBox11_MouseHover(sender As Object, e As EventArgs) Handles GroupBox11.MouseHover
        ConglomerateToolTip.SetToolTip(GroupBox11, "This is where the custom installer will be saved. To change this setting, click " & Quote & "No" & Quote & ", and select a different path and name")
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        Label70.Text = ComboBox5.SelectedItem
        Label33.Text = "Installer creation method: " & ComboBox5.SelectedItem
        ' This condition will set values for the Context Menu Strip (CMS) on the system tray icon
        If ComboBox5.SelectedItem = "WIMR" Then
            WIMRToolStripMenuItem.Checked = True
            DLLRToolStripMenuItem.Checked = False
            REGTWEAKToolStripMenuItem.Checked = False
        ElseIf ComboBox5.SelectedItem = "DLLR" Then
            WIMRToolStripMenuItem.Checked = False
            DLLRToolStripMenuItem.Checked = True
            REGTWEAKToolStripMenuItem.Checked = False
        ElseIf ComboBox5.SelectedItem = "REGTWEAK" Then
            WIMRToolStripMenuItem.Checked = False
            DLLRToolStripMenuItem.Checked = False
            REGTWEAKToolStripMenuItem.Checked = True
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Label72.Text = "Yes"
        ElseIf CheckBox1.Checked = False Then
            Label72.Text = "No"
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            Label74.Text = "Yes"
        ElseIf CheckBox2.Checked = False Then
            Label74.Text = "No"
        End If
    End Sub

    Private Sub LabelSetButton_Click(sender As Object, e As EventArgs) Handles LabelSetButton.Click
        Label78.Text = LabelText.Text
        Label34.Text = "Label: " & LabelText.Text
    End Sub

    Private Sub HelpPic_Click(sender As Object, e As EventArgs) Handles HelpPic.Click, PictureBox33.Click, Label101.Click
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

    End Sub

    Private Sub PictureBox27_Click(sender As Object, e As EventArgs) Handles PictureBox27.Click, Label50.Click, Label49.Click, PictureBox26.Click

    End Sub

    Private Sub PictureBox29_Click(sender As Object, e As EventArgs) Handles PictureBox29.Click, Label2.Click, Label51.Click, PictureBox28.Click

    End Sub

    Private Sub SetDefaultButton_Click(sender As Object, e As EventArgs) Handles SetDefaultButton.Click
        LabelText.Text = "Windows11"
        Label34.Text = "Label: " & LabelText.Text
    End Sub

    Private Sub GroupBox6_MouseHover(sender As Object, e As EventArgs) Handles GroupBox6.MouseHover
        ConglomerateToolTip.SetToolTip(GroupBox6, "The images you specify here will be used by the program to create the custom installer")
    End Sub

    Private Sub GroupBox7_MouseHover(sender As Object, e As EventArgs) Handles GroupBox7.MouseHover
        ConglomerateToolTip.SetToolTip(GroupBox7, "Here you can specify the name and location of the target installer")
    End Sub

    Private Sub GroupBox2_MouseHover(sender As Object, e As EventArgs) Handles GroupBox2.MouseHover
        ConglomerateToolTip.SetToolTip(GroupBox2, "Here is a description of all the methods that can be used to create the installer")
    End Sub

    Private Sub Label22_MouseHover(sender As Object, e As EventArgs) Handles Label22.MouseHover, RadioButton1.MouseHover, RadioButton2.MouseHover
        ConglomerateToolTip.SetToolTip(Label22, "Here you can set the platform compatibility for the target installer. If you set " & Quote & "BIOS/UEFI-CSM" & Quote & " the installer will be guaranteed for broader hardware compatibility. View the platform compatibility details for more information")
    End Sub

    Private Sub GroupBox3_MouseHover(sender As Object, e As EventArgs) Handles GroupBox3.MouseHover
        ConglomerateToolTip.SetToolTip(GroupBox3, "Here you can see the details of your selected platform compatibility option")
    End Sub

    Private Sub GroupBox4_MouseHover(sender As Object, e As EventArgs) Handles GroupBox4.MouseHover, Label24.MouseHover
        ConglomerateToolTip.SetToolTip(GroupBox4, "In this section you can specify the target installer's label. The maximum amount of characters you can set as the label is 32")    ' , so please be creative when putting a custom label!!!
    End Sub

    Private Sub Label25_MouseHover(sender As Object, e As EventArgs) Handles Label25.MouseHover
        ConglomerateToolTip.SetToolTip(Label25, "This is the label used on the custom installer")
    End Sub

    Private Sub LabelSetButton_MouseHover(sender As Object, e As EventArgs) Handles LabelSetButton.MouseHover
        ConglomerateToolTip.SetToolTip(LabelSetButton, "Click here to set this label")
    End Sub

    Private Sub SetDefaultButton_MouseHover(sender As Object, e As EventArgs) Handles SetDefaultButton.MouseHover, Button13.MouseHover
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
        If ComboBox4.SelectedItem = "Spanish" Or ComboBox4.SelectedItem = "Español" Then
            Text = "Instalador manual de Windows 11"
            Notify.Text = "Instalador manual de Windows 11 - Listo"
            ' Labels
            Label1.Text = "Bienvenido"
            ' Label11 doesn't go here, as it's a ">". It will change its Left property instead.

            Label12.Text = "Personalización"

            Label13.Text = "Modo de color:"
            ' Label16 will also change its Left property
            Label17.Text = "Funcionalidad"
            Label19.Text = "Idioma:"
            Label2.Text = "Muestra información sobre el programa"
            Label20.Text = "Método de creación del instalador:"
            Label21.Text = "WIMR: WIM-Replace - reemplaza " & Quote & "install.wim" & Quote & " del instalador de Windows 10 con el de Windows 11" & CrLf & "DLLR: DLL-Replace - reemplaza " & Quote & "appraiser.dll" & Quote & " y " & Quote & "appraiserres.dll" & Quote & " del instalador de Windows 11 con los de Windows 10" & CrLf & "REGTWEAK - hace cambios al registro del instalador de Windows 11. No se necesita un instalador de Windows 10" & CrLf & "Dese cuenta de que REGTWEAK está disponible con privilegios de administrador"
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
            ' These lines of code affect Label5 and PictureBox11
            Label5.Text = "Instalador manual de"
            Label5.Top = 14
            PictureBox11.Top = Label5.Top + Label5.Height
            Label50.Text = "Ayuda"
            Label51.Text = "Acerca de"
            Label53.Text = "Instalador de Windows 11"
            Label54.Text = "Instalador de Windows 10"
            Label55.Text = "Instalador de Windows 11:"
            Label56.Text = "Instalador de Windows 10:"
            Label57.Text = "Opciones del archivo ISO"
            Label58.Text = "Nombre del archivo ISO:"
            Label59.Text = "Ubicación del archivo ISO:"
            Label6.Text = "Instalador manual de Windows 11"
            Label60.Text = "Cuando esté listo, haga clic en " & Quote & "Crear" & Quote
            Label63.Text = "Revise sus configuraciones"
            Label64.Text = "Ubicación y nombre"
            Label65.Text = "Imagen de Windows 11:"
            Label66.Text = "Imagen de Windows 10:"
            Label69.Text = "Método:"
            Label7.Text = "versión 2.0.0100"
            Label71.Text = "¿Requerir que ambas imágenes tengan archivos *.wim?"
            If CheckBox1.Checked = True Then
                Label72.Text = "Sí"
            Else
                Label72.Text = "No"
            End If
            Label73.Text = "¿Hacer un instalador temporal de Windows 10?"
            If CheckBox2.Checked = True Then
                Label74.Text = "Sí"
            Else
                Label74.Text = "No"
            End If
            Label75.Text = "Compatibilidad de plataformas:"
            Label77.Text = "Etiqueta del instalador:"
            Label79.Text = "¿Estas configuraciones son las correctas?"
            Label82.Text = "Progreso"
            Label83.Text = "Ésta es toda la información que necesitamos en este momento. Esto tardará unos minutos, por lo que sea paciente, por favor."
            Label84.Text = "Preparando el espacio de trabajo"
            Label85.Text = "Obteniendo instrucciones"
            Label86.Text = "Extrayendo archivos de los instaladores"
            Label87.Text = "Creando el instalador"
            Label88.Text = "Finalizando"
            Label89.Text = "La acción realizada en este momento no puede ser cancelada. Por favor, espere."
            Label9.Text = "Configuración"
            Label91.Text = "Para detectar actualizaciones del programa, haga clic en " & Quote & "Comprobar actualizaciones" & Quote
            Label92.Text = "Ésta es la información detallada de los componentes usados por el programa:"
            Label97.Text = "Todos los componentes mostrados aquí son protegidos por sus propios términos de licencia, y este programa SOLO puede redistribuir componentes de código abierto."
            AdminLabel.Text = "MODO DE ADMINISTRADOR"
            ProgramTitleLabel.Text = "Instalador manual de Windows 11"


            ' Labels that belong to main sections will adopt the text from the main panels
            Label10.Text = Label9.Text
            Label18.Text = Label9.Text
            Label35.Text = Label12.Text
            Label37.Text = Label17.Text
            Label52.Text = Label50.Text
            Label61.Text = Label44.Text
            Label8.Text = Label44.Text
            Label80.Text = Label44.Text

            ' Miscelaneous labels
            Label29.Text = Label12.Text
            Label32.Text = Label17.Text
            Label46.Text = Label9.Text
            NameLabel.Text = "Nombre:"
            If InstHistPanel.InstallerListView.Items.Count = 0 Then
                Last_Inst_Time_Label.Text = "Ningún dato temporal disponible"
            End If


            ' LinkLabels
            LinkLabel1.Text = "Ver historial del instalador"
            LinkLabel2.Text = "Continuar creación del instalador"
            LinkLabel3.Text = "Buscar en Google"
            LinkLabel4.Text = "Buscar en DuckDuckGo"
            LinkLabel5.Text = "Cambiar nombre"

            ' GroupBoxes
            GroupBox2.Text = "Ayuda de métodos"
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
            Button10.Text = "Cancelar"
            Button11.Text = "Ver en el Explorador"
            Button12.Text = "Comprobar actualizaciones"
            ScanButton.Text = "Escanear..."
            LabelSetButton.Text = "Establecer"
            SetDefaultButton.Text = "Predeterminado"

            ' CheckBoxes
            CheckBox1.Text = "Requerir que ambas imágenes tengan archivos *.wim"
            CheckBox2.Text = "Crear un instalador temporal de Windows 10"
            CheckBox3.Text = "Al cerrarse, ocultar en la bandeja del sistema"
            CheckBox4.Text = "Cerrar programa después de hacer clic en Aceptar"

            ' MenuStrip items
            Windows11ManualInstallerToolStripMenuItem.Text = "Instalador manual de Windows 11"
            VersionToolStripMenuItem.Text = "versión 2.0.0100"
            InstSTLabel.Text = "Estado del instalador"
            StatusTSI.Text = "No se está creando ningún instalador en este momento"
            LastInstallerCreatedAtToolStripMenuItem.Text = "Último instalador creado a las:"
            NoInstallerHistoryDataIsAvailableAtThisTimeToolStripMenuItem.Text = "No hay datos del historial del instalador en este momento"
            ViewInstallerHistoryToolStripMenuItem.Text = "Ver historial del instalador"
            LanguageToolStripMenuItem.Text = "Idioma"
            AutomaticLanguageToolStripMenuItem.Text = "Automático"
            AutomaticLanguageToolStripMenuItem.Checked = False
            EnglishToolStripMenuItem.Text = "Inglés"
            EnglishToolStripMenuItem.Checked = False
            SpanishToolStripMenuItem.Text = "Español"
            SpanishToolStripMenuItem.Checked = True
            ColorModeToolStripMenuItem.Text = "Modo del color"
            AutomaticToolStripMenuItem.Text = "Automático"
            LightToolStripMenuItem.Text = "Claro"
            DarkToolStripMenuItem.Text = "Oscuro"
            If ComboBox1.SelectedItem = "Automatic" Or ComboBox1.SelectedItem = "Automático" Then
                AutomaticToolStripMenuItem.Checked = True
                LightToolStripMenuItem.Checked = False
                DarkToolStripMenuItem.Checked = False
            ElseIf ComboBox1.SelectedItem = "Light" Or ComboBox1.SelectedItem = "Claro" Then
                AutomaticToolStripMenuItem.Checked = False
                LightToolStripMenuItem.Checked = True
                DarkToolStripMenuItem.Checked = False
            ElseIf ComboBox1.SelectedItem = "Dark" Or ComboBox1.SelectedItem = "Oscuro" Then
                AutomaticToolStripMenuItem.Checked = False
                LightToolStripMenuItem.Checked = False
                DarkToolStripMenuItem.Checked = True
            ElseIf ComboBox1.SelectedItem = "Custom" Or ComboBox1.SelectedItem = "Personalizado" Then
                'If ComboBox2.SelectedItem = "Light" Or ComboBox2.SelectedItem = "Claro" Then
                '    UseLightModeForTheMainWindowToolStripMenuItem.Checked = True
                'Else
                '    UseLightModeForTheMainWindowToolStripMenuItem.Checked = False
                'End If
                'If ComboBox3.SelectedItem = "Light" Or ComboBox3.SelectedItem = "Claro" Then
                '    UseLightModeForDialogsToolStripMenuItem.Checked = True
                'Else
                '    UseLightModeForDialogsToolStripMenuItem.Checked = False
                'End If
            End If
            InstallerCreationMethodToolStripMenuItem.Text = "Método de creación del instalador"
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
        ElseIf ComboBox4.SelectedItem = "English" Then

        ElseIf ComboBox4.SelectedItem = "Automatic" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then

            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then

            End If
        End If
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
        Label91.Text = "Checking for updates..."
        ProgressRingPic.Visible = True
        If My.Computer.Network.IsAvailable = False Then
            MsgBox("You need to connect to a network to check for updates. Please connect your system to a network and try again", vbOKOnly + vbInformation, "Update check error")
        Else
            MsgBox("No updates available", vbOKOnly + vbInformation, "Update check")
        End If
        ProgressRingPic.Visible = False
        Label91.Left = 57
        Label91.Text = "To check for any program updates, click " & Quote & "Check for updates" & Quote & CrLf & "Last update check performed on: " & UpdateCheckDate
    End Sub

    Private Sub Label103_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label103.Click
        AdditionalToolsCMS.Show(CType(sender, Control), e.Location)
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
            End If
        Else
            FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
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

    End Sub

    Private Sub LinkLabel6_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel6.LinkClicked
        MsgBox("We could not get this computer's model" & CrLf & "This might be because, the component used to get the model (Windows Management Instrumentation, WMI), is missing from your system or has returned an error code." & CrLf & CrLf & "This is not critical, as it only affects the user experience.", vbOKOnly + vbExclamation, "Computer model gather process failure")
    End Sub

    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If InstCreateInt = 2 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        BringToFront()
        BackSubPanel.Show()
        AdvancedOptionsPanel.ShowDialog()
        AdvancedOptionsPanel.Visible = True
        AdvancedOptionsPanel.Visible = False
        BringToFront()
    End Sub

    Private Sub LinkLabel7_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel7.LinkClicked
        BringToFront()
        BackSubPanel.Show()
        UpdateChoicePanel.ShowDialog()
        UpdateChoicePanel.Visible = True
        UpdateChoicePanel.Visible = False
        BringToFront()
    End Sub
End Class