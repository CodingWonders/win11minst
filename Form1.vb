Imports Microsoft.Win32
Imports System.IO
Imports Microsoft.VisualBasic.ControlChars
Imports System.Windows.Forms.FlatStyle      ' This imports the various FlatStyle settings


Public Class MainForm
    Private isMouseDown As Boolean = False
    Private mouseOffset As Point
    Dim OffEcho As String = "@echo off"
    Dim wmiget As String
    Dim UEFIget As String = OffEcho & CrLf & "powershell $env:firmware_type > uefi.txt"
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



    ' Left mouse button pressed
    Private Sub titlePanel_MouseDown(sender As Object, e As MouseEventArgs) Handles titlePanel.MouseDown, ProgramTitleLabel.MouseDown, Label12.MouseDown, AdminLabel.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ' Get the new position
            mouseOffset = New Point(-e.X, -e.Y)
            ' Set that left button is pressed
            isMouseDown = True
        End If
    End Sub



    'Private Sub roundCorners(obj As Form)

    '    obj.FormBorderStyle = FormBorderStyle.None
    '    If BackColor = Color.FromArgb(32, 32, 32) Then
    '        obj.BackColor = Color.FromArgb(32, 32, 32)
    '    ElseIf BackColor = Color.FromArgb(243, 243, 243) Then
    '        obj.BackColor = Color.FromArgb(243, 243, 243)
    '    End If


    '    Dim DGP As New Drawing2D.GraphicsPath
    '    DGP.StartFigure()
    '    'top left corner
    '    DGP.AddArc(New Rectangle(0, 0, 16, 16), 180, 90)
    '    DGP.AddLine(40, 0, obj.Width - 40, 0)

    '    'top right corner
    '    DGP.AddArc(New Rectangle(obj.Width - 12, 0, 16, 16), -90, 90)
    '    DGP.AddLine(obj.Width, 40, obj.Width, obj.Height - 40)

    '    'buttom right corner
    '    DGP.AddArc(New Rectangle(obj.Width - 40, obj.Height - 40, 40, 40), 0, 90)
    '    DGP.AddLine(obj.Width - 40, obj.Height, 40, obj.Height)

    '    'buttom left corner
    '    DGP.AddArc(New Rectangle(0, obj.Height - 40, 40, 40), 90, 90)
    '    DGP.CloseFigure()

    '    obj.Region = New Region(DGP)


    'End Sub

    ' MouseMove used to check if mouse cursor is moving
    Private Sub titlePanel_MouseMove(sender As Object, e As MouseEventArgs) Handles titlePanel.MouseMove, ProgramTitleLabel.MouseMove, Label12.MouseMove, AdminLabel.MouseMove
        If isMouseDown Then
            Dim mousePos As Point = Control.MousePosition
            ' Get the new form position
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Location = mousePos
        End If
    End Sub

    ' Left mouse button released, form should stop moving
    Private Sub titlePanel_MouseUp(sender As Object, e As MouseEventArgs) Handles titlePanel.MouseUp, ProgramTitleLabel.MouseUp, Label12.MouseUp, AdminLabel.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            isMouseDown = False
        End If
    End Sub

    Private Sub closeBox_Click(sender As Object, e As EventArgs) Handles closeBox.Click
        If CheckBox3.Checked = True Then
            ' The following code snippet determines the check state of "Don't show this again"
            If MiniModeDialog.CheckBox1.Checked = False Then
                MiniModeDialog.Show()
            End If
            RunningInBGNotify.ShowBalloonTip(5)
            WindowState = FormWindowState.Minimized
            ShowInTaskbar = False
        Else
            If InstCreateInt = 2 Then
                BackSubPanel.Show()
                InstCreateAbortPanel.ShowDialog()
                InstCreateAbortPanel.Visible = True
                InstCreateAbortPanel.Visible = False
                BringToFront()
                If InstCreateAbortPanel.DialogResult = Windows.Forms.DialogResult.OK Then
                    ' Abort EVERYTHING
                    KillExtCmd = "taskkill /f /im cmd.exe /t"
                    File.WriteAllText(".\kill.bat", OffEcho & CrLf & KillExtCmd, System.Text.Encoding.ASCII)
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
                            File.WriteAllText(".\temp.bat", OffEcho & CrLf & RegUnload & CrLf & DismUnmount, System.Text.Encoding.ASCII)
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
                            File.WriteAllText(".\temp.bat", OffEcho & CrLf & EmergencyFolderDelete, System.Text.Encoding.ASCII)
                            Process.Start(".\temp.bat").WaitForExit()
                        End Try
                    End If
                    If Debugger.IsAttached = True Then
                        EnDebugTime = Now
                        MsgBox("Debug started at: " & StDebugTime & CrLf & "Debug ended at: " & EnDebugTime & CrLf & "Machine information:" & CrLf & "Name: " & My.Computer.Name & CrLf & "Total memory: " & My.Computer.Info.TotalPhysicalMemory & "KB" & CrLf & "Operating system: " & My.Computer.Info.OSFullName, vbOKOnly + vbInformation, "Debug mode")
                        If DialogResult.OK Then
                            RunningInBGNotify.Visible = False
                            End
                        End If
                    End If
                    RunningInBGNotify.Visible = False
                    End
                ElseIf InstCreateAbortPanel.DialogResult = Windows.Forms.DialogResult.Cancel Then

                End If
            Else
                If Debugger.IsAttached = True Then
                    EnDebugTime = Now
                    MsgBox("Debug started at: " & StDebugTime & CrLf & "Debug ended at: " & EnDebugTime & CrLf & "Machine information:" & CrLf & "Name: " & My.Computer.Name & CrLf & "Total memory: " & My.Computer.Info.TotalPhysicalMemory & "KB" & CrLf & "Operating system: " & My.Computer.Info.OSFullName, vbOKOnly + vbInformation, "Debug mode")
                    If DialogResult.OK Then
                        RunningInBGNotify.Visible = False
                        End
                    End If
                End If
                RunningInBGNotify.Visible = False
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

    Private Sub titlePanel_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles titlePanel.MouseDoubleClick, ProgramTitleLabel.MouseDoubleClick, Label12.MouseDoubleClick, AdminLabel.MouseDoubleClick
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
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim LaunchPSI As New ProcessStartInfo
        'roundCorners(Me)
        'My.Settings.Reload()
        ComboBox1.SelectedItem = "Automatic"
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
        Dim HTMLname As String = "HelpPage.html"
        With WebBrowser1
            .ScriptErrorsSuppressed = True
            .Url = New Uri(String.Format("file:///{0}{1}{2}/", Directory.GetCurrentDirectory(), "/Resources/", HTMLname))
        End With

        NameLabel.Text = "Name: " & TextBox3.Text
        Label28.Text = "DPI scale to be applied: " & TrackBar1.Value & "%"
        InstCreateInt = 0
        TextBox4.Text = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        ImgPathLabel.Text = "The image will be saved to: " & Quote & TextBox4.Text & "\" & TextBox3.Text & ".iso" & Quote
        ' This condition is used for debugging purposes
        If Debugger.IsAttached = True Then
            CheckBox3.Checked = False
            ' This will close the program during debugging and testing. This will not hide it to the
            ' system tray
            MsgBox("Debug started at: " & StDebugTime & CrLf & "Machine information:" & CrLf & "Name: " & My.Computer.Name & CrLf & "Total memory: " & My.Computer.Info.TotalPhysicalMemory & "KB" & CrLf & "Operating system: " & My.Computer.Info.OSFullName, vbOKOnly + vbInformation, "Debug mode")
        End If
        ' The following condition will determine the operating system, and set the Button FlatStyle,
        ' due to a visual artifact in earlier Windows versions than Windows 11 when setting to Dark mode
        If My.Computer.Info.OSFullName.Contains("Windows 7") Or My.Computer.Info.OSFullName.Contains("Windows 8") Or My.Computer.Info.OSFullName.Contains("Windows 8.1") Or My.Computer.Info.OSFullName.Contains("Windows 10") Then
            Button1.FlatStyle = Flat            ' This sets that button's property to Flat. However, it does not say "FlatStyle", because the library was imported (shown at the beginning of the file)
            Button2.FlatStyle = Flat
            Button3.FlatStyle = Flat
            Button4.FlatStyle = Flat
            Button5.FlatStyle = Flat
            Button6.FlatStyle = Flat
            Button7.FlatStyle = Flat
            Button8.FlatStyle = Flat
            Button9.FlatStyle = Flat
            Button10.FlatStyle = Flat
            Button11.FlatStyle = Flat

            ' Space intentionally left for more buttons
            ScanButton.FlatStyle = Flat
            LabelSetButton.FlatStyle = Flat
            SetDefaultButton.FlatStyle = Flat
        End If
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
        File.WriteAllText(".\wmi.bat", wmiget.Trim(), System.Text.Encoding.ASCII)
        File.WriteAllText(".\uefi.bat", UEFIget.Trim(), System.Text.Encoding.ASCII)
        'LaunchPSI.WorkingDirectory = Application.StartupPath
        'LaunchPSI.FileName = ".\wmi.bat"
        'LaunchPSI.CreateNoWindow = True
        'Dim wmigetproc As Process = Process.Start(LaunchPSI)
        Process.Start(".\wmi.bat").WaitForExit()
        Process.Start(".\uefi.bat").WaitForExit()
        wmiform.TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\model.txt")
        wmiform.TextBox1.Text = wmiform.TextBox1.Text.Replace("Model", "").Trim()
        uefitypeform.TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\uefi.txt")
        If uefitypeform.TextBox1.Text = "UEFI" Then
            RadioButton1.Checked = False
            RadioButton2.Checked = True
        ElseIf uefitypeform.TextBox1.Text = "Legacy" Then
            RadioButton1.Checked = True
            RadioButton2.Checked = False
        End If
        modelLabel.Text = wmiform.TextBox1.Text
        MaximumSize = Screen.FromControl(Me).WorkingArea.Size
        File.Delete(".\wmi.bat")
        File.Delete(".\uefi.bat")
        File.Delete(".\uefi.txt")
        File.Delete(".\model.txt")
        computerLabel.Text = My.Computer.Name

        CenterToScreen()        ' This is done to prevent a bug where it would not center to the screen!
        Visible = True
        BackSubPanel.Show()
        DisclaimerPanel.ShowDialog()
        DisclaimerPanel.Visible = True
        DisclaimerPanel.Visible = False

        If My.Computer.Info.OSFullName.Contains("Windows 11") Then
            MsgBox("This computer (or device) is already running Windows 11. You will not be able to use this tool to upgrade your system, but you can still use it to upgrade other systems", vbOKOnly + vbInformation, "Already running Windows 11")
        End If
        If Environment.Is64BitOperatingSystem = False Then
            MsgBox("The program has detected a 32-bit processor or a 32-bit operating system. The tool will still work, but the installer won't. You'll need a computer with a 64-bit processor to install Windows 11." & CrLf & "If it's the latter (you have a 32-bit OS on a 64-bit processor), you'll need to reinstall Windows.", vbOKOnly + vbInformation, "Incompatible installer architecture")
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
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

    Private Sub InfoPic_Click(sender As Object, e As EventArgs) Handles InfoPic.Click
        WelcomePanel.Visible = False
        InfoPanel.Visible = True
        WelcomePic.Image = New Bitmap(My.Resources.home)
        InfoPic.Image = New Bitmap(My.Resources.info_filled)
    End Sub

    Private Sub WelcomePic_Click(sender As Object, e As EventArgs) Handles WelcomePic.Click
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
    End Sub

    Private Sub LogoPic_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles LogoPic.MouseDoubleClick
        If CheckBox3.Checked = True Then
            RunningInBGNotify.ShowBalloonTip(5)
            WindowState = FormWindowState.Minimized
            ShowInTaskbar = False
        Else
            RunningInBGNotify.Visible = False
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
        If ComboBox1.SelectedItem = "Custom" Then
            GroupBox1.Enabled = True
        Else
            GroupBox1.Enabled = False
        End If
        If ComboBox1.SelectedItem = "Dark" Then
            BackColor = Color.FromArgb(32, 32, 32)
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
            maxBox.Image = New Bitmap(My.Resources.maxbox_dark)
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
            back_Pic.Image = New Bitmap(My.Resources.back_arrow_dark)
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
            PanelIndicatorPic.Image = New Bitmap(My.Resources.panel_indicator_dark)

            ' This is for the Group Box
            GroupBox1.ForeColor = Color.White
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
            If Settings_PersonalizationPanel.Visible = True Then
                SettingsPic.Image = New Bitmap(My.Resources.settings_dark_filled)
            Else
                SettingsPic.Image = New Bitmap(My.Resources.settings_dark)
            End If
            InstructionPic.Image = New Bitmap(My.Resources.instructions_dark)
            HelpPic.Image = New Bitmap(My.Resources.help_dark)
            InfoPic.Image = New Bitmap(My.Resources.info_dark)

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

            RunningInBGNotify.Icon = My.Resources.NotifyIconRes_Dark

            ' ComboBoxes
            ComboBox1.BackColor = Color.FromArgb(39, 39, 39)
            ComboBox2.BackColor = Color.FromArgb(39, 39, 39)
            ComboBox3.BackColor = Color.FromArgb(39, 39, 39)
            ComboBox4.BackColor = Color.FromArgb(39, 39, 39)
            ComboBox5.BackColor = Color.FromArgb(39, 39, 39)
            ComboBox1.ForeColor = Color.White
            ComboBox2.ForeColor = Color.White
            ComboBox3.ForeColor = Color.White
            ComboBox4.ForeColor = Color.White
            ComboBox5.ForeColor = Color.White

        ElseIf ComboBox1.SelectedItem = "Light" Then
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
            maxBox.Image = New Bitmap(My.Resources.maxbox)
            closeBox.Image = New Bitmap(My.Resources.closebox)
            back_Pic.Image = New Bitmap(My.Resources.back_arrow)
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

            GroupBox1.ForeColor = Color.Black
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
            Else
                WelcomePic.Image = New Bitmap(My.Resources.home)
            End If
            InstCreatePic.Image = New Bitmap(My.Resources.inst_create)
            If Settings_PersonalizationPanel.Visible = True Then
                SettingsPic.Image = New Bitmap(My.Resources.settings_filled)
            Else
                SettingsPic.Image = New Bitmap(My.Resources.settings)
            End If
            InstructionPic.Image = New Bitmap(My.Resources.instructions)
            HelpPic.Image = New Bitmap(My.Resources.help)
            InfoPic.Image = New Bitmap(My.Resources.info)
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
            ComboBox2.BackColor = Color.FromArgb(249, 249, 249)
            ComboBox3.BackColor = Color.FromArgb(249, 249, 249)
            ComboBox4.BackColor = Color.FromArgb(249, 249, 249)
            ComboBox5.BackColor = Color.FromArgb(249, 249, 249)
            ComboBox1.ForeColor = Color.Black
            ComboBox2.ForeColor = Color.Black
            ComboBox3.ForeColor = Color.Black
            ComboBox4.ForeColor = Color.Black
            ComboBox5.ForeColor = Color.Black

            RunningInBGNotify.Icon = My.Resources.NotifyIconRes_Light
        ElseIf ComboBox1.SelectedItem = "Automatic" Then
            Try
                If My.Computer.Info.OSFullName.Contains("Windows 7") Or My.Computer.Info.OSFullName.Contains("Windows 8") Then
                    ' Don't do this again!!!
                Else
                    Dim SysColor As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", False)
                    Dim SysColorStr As String = SysColor.GetValue("SystemUsesLightTheme").ToString()
                    SysColor.Close()
                    If SysColorStr = "1" Then
                        RunningInBGNotify.Icon = My.Resources.NotifyIconRes_Light
                    ElseIf SysColorStr = "0" Then
                        RunningInBGNotify.Icon = My.Resources.NotifyIconRes_Dark
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
                        BackColor = Color.FromArgb(32, 32, 32)
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
                        maxBox.Image = New Bitmap(My.Resources.maxbox_dark)
                        closeBox.Image = New Bitmap(My.Resources.closebox_dark)
                        back_Pic.Image = New Bitmap(My.Resources.back_arrow_dark)
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
                        GroupBox1.ForeColor = Color.White
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
                        If Settings_PersonalizationPanel.Visible = True Then
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
                        ComboBox2.BackColor = Color.FromArgb(39, 39, 39)
                        ComboBox3.BackColor = Color.FromArgb(39, 39, 39)
                        ComboBox4.BackColor = Color.FromArgb(39, 39, 39)
                        ComboBox5.BackColor = Color.FromArgb(39, 39, 39)
                        ComboBox1.ForeColor = Color.White
                        ComboBox2.ForeColor = Color.White
                        ComboBox3.ForeColor = Color.White
                        ComboBox4.ForeColor = Color.White
                        ComboBox5.ForeColor = Color.White
                    ElseIf ColorStr = "1" Then
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
                        maxBox.Image = New Bitmap(My.Resources.maxbox)
                        closeBox.Image = New Bitmap(My.Resources.closebox)
                        back_Pic.Image = New Bitmap(My.Resources.back_arrow)
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
                        If Settings_PersonalizationPanel.Visible = True Then
                            SettingsPic.Image = New Bitmap(My.Resources.settings_filled)
                        Else
                            SettingsPic.Image = New Bitmap(My.Resources.settings)
                        End If
                        InstructionPic.Image = New Bitmap(My.Resources.instructions)
                        HelpPic.Image = New Bitmap(My.Resources.help)
                        InfoPic.Image = New Bitmap(My.Resources.info)
                        CheckPic1.Image = New Bitmap(My.Resources.check)
                        CheckPic2.Image = New Bitmap(My.Resources.check)
                        CheckPic3.Image = New Bitmap(My.Resources.check)
                        CheckPic4.Image = New Bitmap(My.Resources.check)
                        CheckPic5.Image = New Bitmap(My.Resources.check)
                        GroupBox1.ForeColor = Color.Black
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
                        ComboBox2.BackColor = Color.FromArgb(249, 249, 249)
                        ComboBox3.BackColor = Color.FromArgb(249, 249, 249)
                        ComboBox4.BackColor = Color.FromArgb(249, 249, 249)
                        ComboBox5.BackColor = Color.FromArgb(249, 249, 249)
                        ComboBox1.ForeColor = Color.Black
                        ComboBox2.ForeColor = Color.Black
                        ComboBox3.ForeColor = Color.Black
                        ComboBox4.ForeColor = Color.Black
                        ComboBox5.ForeColor = Color.Black
                    End If
                Else
                    If My.Computer.Info.OSFullName.Contains("Windows 7") Then
                        MsgBox("This feature is not supported on Windows 7", vbOKOnly + vbInformation, "Automatic color mode")
                    ElseIf My.Computer.Info.OSFullName.Contains("Windows 8") Or My.Computer.Info.OSFullName.Contains("Windows 8.1") Then
                        MsgBox("This feature is not supported on Windows 8/8.1", vbOKOnly + vbInformation, "Automatic color mode")
                    End If
                End If
            Catch NREx As NullReferenceException
                MsgBox("Cannot set the color mode to match the system's. Perhaps the registry key is not available.", vbOKOnly + vbExclamation, "Automatic color mode")
            End Try
        End If
    End Sub

    Private Sub SettingsPic_Click(sender As Object, e As EventArgs) Handles SettingsPic.Click
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
    End Sub

    Private Sub PictureBox13_Click(sender As Object, e As EventArgs) Handles PictureBox13.Click, PictureBox15.Click, Label35.Click, Label36.Click
        back_Pic.Visible = True
        LogoPic.Left = 48
        ProgramTitleLabel.Left = 102
        SettingPanel.Visible = False
        Settings_PersonalizationPanel.Visible = True
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Label28.Text = "DPI scale to be applied: " & TrackBar1.Value & "%"
    End Sub

    Private Sub back_Pic_Click(sender As Object, e As EventArgs) Handles back_Pic.Click
        If Settings_PersonalizationPanel.Visible = True Then
            SettingPanel.Visible = True
            Settings_PersonalizationPanel.Visible = False
        End If
        If Settings_FunctionalityPanel.Visible = True Then
            SettingPanel.Visible = True
            Settings_FunctionalityPanel.Visible = False
        End If
        back_Pic.Visible = False
        LogoPic.Left = 19
        ProgramTitleLabel.Left = 73
    End Sub

    Private Sub PictureBox14_Click(sender As Object, e As EventArgs) Handles PictureBox14.Click
        back_Pic.Visible = True
        LogoPic.Left = 48
        ProgramTitleLabel.Left = 102
        SettingPanel.Visible = False
        Settings_FunctionalityPanel.Visible = True
    End Sub

    Private Sub RunningInBGNotify_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles RunningInBGNotify.MouseDoubleClick
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

    Private Sub RunningInBGNotify_BalloonTipClicked(sender As Object, e As EventArgs) Handles RunningInBGNotify.BalloonTipClicked
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
        RunningInBGNotify.Visible = False
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
        BackSubPanel.Show()
        ISOFileScanPanel.ShowDialog()
        ISOFileScanPanel.Visible = True
        ISOFileScanPanel.Visible = False
        BringToFront()
    End Sub

    Private Sub PictureBox19_Click(sender As Object, e As EventArgs) Handles PictureBox19.Click, Label42.Click, Label41.Click, PictureBox18.Click
        BackSubPanel.Show()
        PrefResetPanel.ShowDialog()
        PrefResetPanel.Visible = True
        PrefResetPanel.Visible = False
        BringToFront()
    End Sub

    Private Sub InstCreatePic_Click(sender As Object, e As EventArgs) Handles InstCreatePic.Click
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
        AdminModeToolTip.SetToolTip(AdminLabel, "This program is running with administrative privileges")
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
        minToolTip.SetToolTip(minBox, "Minimize")
    End Sub

    Private Sub maxBox_MouseHover(sender As Object, e As EventArgs) Handles maxBox.MouseHover
        If WindowState = FormWindowState.Normal Then
            maxToolTip.SetToolTip(maxBox, "Maximize")
        ElseIf WindowState = FormWindowState.Maximized Then
            maxToolTip.SetToolTip(maxBox, "Restore down")
        End If
    End Sub

    Private Sub closeBox_MouseHover(sender As Object, e As EventArgs) Handles closeBox.MouseHover
        closeToolTip.SetToolTip(closeBox, "Close")
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
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
                        BackSubPanel.Show()
                        VolumeConnectPanel.ShowDialog()
                        VolumeConnectPanel.Visible = True
                        VolumeConnectPanel.Visible = False
                        BringToFront()
                    Catch FNFEx2 As FileNotFoundException
                        LogBox.AppendText(" Failed")
                        InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
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
                        BackSubPanel.Show()
                        VolumeConnectPanel.ShowDialog()
                        VolumeConnectPanel.Visible = True
                        VolumeConnectPanel.Visible = False
                        BringToFront()
                    Catch FNFEx2 As FileNotFoundException
                        LogBox.AppendText(" Failed")
                        InstSTLabel.Text = "Failed to copy the ISO files to the local disk."
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
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11IWIMISOEx_Local & CrLf & W10ISOEx_Local, System.Text.Encoding.ASCII)
            Else
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11IWIMISOEx & CrLf & W10ISOEx, System.Text.Encoding.ASCII)
            End If
            Process.Start(".\temp.bat").WaitForExit()
        ElseIf ComboBox5.SelectedItem = "DLLR" Then
            If File.Exists(".\Win11.iso") And File.Exists(".\Win10.iso") Then
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11ISOEx_Local & CrLf & W10AR_ARRDLLEx_Local & CrLf & W10AR_ARRDLLEx_2_Local, System.Text.Encoding.ASCII)
            Else
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11ISOEx & CrLf & W10AR_ARRDLLEx & CrLf & W10AR_ARRDLLEx_2, System.Text.Encoding.ASCII)
            End If
            Process.Start(".\temp.bat").WaitForExit()
        ElseIf ComboBox5.SelectedItem = "REGTWEAK" Then
            If File.Exists(".\Win11.iso") Then
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11ISOEx_Local, System.Text.Encoding.ASCII)
            Else
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & W11ISOEx, System.Text.Encoding.ASCII)
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
            Process.Start(".\bin\regtweak.bat").WaitForExit()       ' Preparing boot.wim for surgery
            LogBox.AppendText(" Done" & CrLf & "Moving " & Quote & "boot.wim" & Quote & " to the Windows 11 installer...")
            File.Move(".\boot.wim", ".\temp\sources\boot.wim")      ' Move boot.wim after operations done
            LogBox.AppendText(" Done")
            InstallerProgressBar.Value = 50
        End If
        LogBox.AppendText(CrLf & "Creating the installer using OSCDIMG...")
        If RadioButton1.Checked = True Then
            File.WriteAllText(".\temp.bat", OffEcho & CrLf & OSCDIMG_CSM, System.Text.Encoding.ASCII)
            Process.Start(".\temp.bat").WaitForExit()
        Else
            If File.Exists(".\temp\boot\Efisys.bin") Then
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & OSCDIMG_UEFI, System.Text.Encoding.ASCII)
            Else
                LogBox.AppendText(CrLf & "WARNING: file: " & Quote & "\boot\Efisys.bin " & Quote & "is not present in the temporary installer. Using the fallback BIOS/UEFI-CSM method...")
                WarnCount = WarnCount + 1
                File.WriteAllText(".\temp.bat", OffEcho & CrLf & OSCDIMG_CSM, System.Text.Encoding.ASCII)
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
                File.WriteAllText(".\emergencydelete.bat", OffEcho & "del " & Quote & deletedFile & Quote & " /f /q", System.Text.Encoding.ASCII)
                Process.Start(".\emergencydelete.bat").WaitForExit()
                File.Delete(".\emergencydelete.bat")
                LogBox.AppendText(CrLf & "Deleted file: " & DelFileCount & "/" & FileCount)
            Catch DNFDelEx As DirectoryNotFoundException
                File.WriteAllText(".\emergencydelete.bat", OffEcho & "del " & Quote & deletedFile & Quote & " /f /q", System.Text.Encoding.ASCII)
                Process.Start(".\emergencydelete.bat").WaitForExit()
                File.Delete(".\emergencydelete.bat")
                LogBox.AppendText(CrLf & "Deleted file: " & DelFileCount & "/" & FileCount)
            End Try
        Next
        LogBox.AppendText(CrLf & "Temp folder cleaned. Deleting it...")
        Try
            Directory.Delete(".\temp")
        Catch IOEx As IOException
            LogBox.AppendText(CrLf & "Exception: 'IOException' caught at runtime, performing emergency method...")
            WarnCount = WarnCount + 1
            File.WriteAllText(".\temp.bat", OffEcho & CrLf & EmergencyFolderDelete, System.Text.Encoding.ASCII)
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
        File.WriteAllText(".\viewinexplorer.bat", OffEcho & CrLf & "explorer " & Quote & TextBox4.Text & Quote, System.Text.Encoding.ASCII)
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
                RunningInBGNotify.Visible = False
                End
            ElseIf CheckBox4.Checked = False Then
                InstCreateInt = 0
                ProgressPanel.Visible = False
                InstCreatePanel.Visible = True
            End If
        End If
    End Sub

    Private Sub GroupBox8_MouseHover(sender As Object, e As EventArgs) Handles GroupBox8.MouseHover
        ImageGroupBoxTT.SetToolTip(GroupBox8, "These are the images used to create the custom installer")
    End Sub

    Private Sub GroupBox9_MouseHover(sender As Object, e As EventArgs) Handles GroupBox9.MouseHover
        InstCreateOptnGBTT.SetToolTip(GroupBox9, "These are the settings used to create the custom installer. To change those, click " & Quote & "No" & Quote & ", and go to Settings (by clicking the gear) > Functionality")
    End Sub

    Private Sub GroupBox11_MouseHover(sender As Object, e As EventArgs) Handles GroupBox11.MouseHover
        TargetImageGBTT.SetToolTip(GroupBox11, "This is where the custom installer will be saved. To change this setting, click " & Quote & "No" & Quote & ", and select a different path and name")
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

    Private Sub HelpPic_Click(sender As Object, e As EventArgs) Handles HelpPic.Click
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
        ISOFileToolTip.SetToolTip(GroupBox6, "The images you specify here will be used by the program to create the custom installer")
    End Sub

    Private Sub GroupBox7_MouseHover(sender As Object, e As EventArgs) Handles GroupBox7.MouseHover
        TIToolTip.SetToolTip(GroupBox7, "Here you can specify the name and location of the target installer")
    End Sub

    Private Sub GroupBox2_MouseHover(sender As Object, e As EventArgs) Handles GroupBox2.MouseHover
        MethodHelpToolTip.SetToolTip(GroupBox2, "Here is a description of all the methods that can be used to create the installer")
    End Sub

    Private Sub Label22_MouseHover(sender As Object, e As EventArgs) Handles Label22.MouseHover, RadioButton1.MouseHover, RadioButton2.MouseHover
        PTFCompatToolTip.SetToolTip(Label22, "Here you can set the platform compatibility for the target installer. If you set " & Quote & "BIOS/UEFI-CSM" & Quote & " the installer will be guaranteed for broader hardware compatibility. View the platform compatibility details for more information")
    End Sub

    Private Sub GroupBox3_MouseHover(sender As Object, e As EventArgs) Handles GroupBox3.MouseHover
        PTFCompatDetailToolTip.SetToolTip(GroupBox3, "Here you can see the details of your selected platform compatibility option")
    End Sub

    Private Sub GroupBox4_MouseHover(sender As Object, e As EventArgs) Handles GroupBox4.MouseHover, Label24.MouseHover
        LabelOptnToolTip.SetToolTip(GroupBox4, "In this section you can specify the target installer's label. The maximum amount of characters you can set as the label are 15")    ' , so please be creative when putting a custom label!!!
    End Sub

    Private Sub Label25_MouseHover(sender As Object, e As EventArgs) Handles Label25.MouseHover
        LabelOptnToolTip.SetToolTip(Label25, "This is the label used on the custom installer")
    End Sub

    Private Sub LabelSetButton_MouseHover(sender As Object, e As EventArgs) Handles LabelSetButton.MouseHover
        LabelOptnToolTip.SetToolTip(LabelSetButton, "Click here to set this label")
    End Sub

    Private Sub SetDefaultButton_MouseHover(sender As Object, e As EventArgs) Handles SetDefaultButton.MouseHover
        LabelOptnToolTip.SetToolTip(SetDefaultButton, "Click here to set the default label, " & Quote & "Windows11" & Quote & ", for the custom installer")
    End Sub

    Private Sub LabelText_TextChanged(sender As Object, e As EventArgs) Handles LabelText.TextChanged
        If LabelText.Text = "Windows11" Then
            SetDefaultButton.Enabled = False
        Else
            SetDefaultButton.Enabled = True
        End If
    End Sub

    Private Sub TableLayoutPanel1_MouseHover(sender As Object, e As EventArgs) Handles TableLayoutPanel1.MouseHover
        CSMHelpToolTip.SetToolTip(TableLayoutPanel1, "Click on one of these links to see how to enable the Compatibility Support Module (CSM) on your system")
    End Sub

    Private Sub LinkLabel3_MouseHover(sender As Object, e As EventArgs) Handles LinkLabel3.MouseHover
        CSMHelpToolTip.SetToolTip(LinkLabel3, "Click on one of these links to see how to enable the Compatibility Support Module (CSM) on your system")
    End Sub

    Private Sub LinkLabel4_MouseHover(sender As Object, e As EventArgs) Handles LinkLabel4.MouseHover
        CSMHelpToolTip.SetToolTip(LinkLabel4, "Click on one of these links to see how to enable the Compatibility Support Module (CSM) on your system")
    End Sub

    Private Sub GroupBox1_MouseHover(sender As Object, e As EventArgs) Handles GroupBox1.MouseHover
        CustomColorToolTip.SetToolTip(GroupBox1, "Here you can set the custom color mode for the program")
    End Sub

    Private Sub GroupBox5_MouseHover(sender As Object, e As EventArgs) Handles GroupBox5.MouseHover
        DPIScaleToolTip.SetToolTip(GroupBox5, "Here you can set a custom DPI scale for the program, ideal for devices with bigger displays and DPI scales")
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
End Class
