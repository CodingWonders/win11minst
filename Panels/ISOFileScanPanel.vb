Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars
Imports System.IO


Public Class ISOFileScanPanel
    Dim FoundFileNum As Integer
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            MsgBox("You have decided to scan subdirectories for ISO files. This might take A LONG time, depending on the ISO images on the directory.", vbOKOnly + vbExclamation, "Scan for ISO files")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, Button2.Click
        ISOFolderScanner.ShowDialog()
        If DialogResult.OK Then
            Label3.Visible = True
            CounterLabel.Visible = True
            TextBox1.Text = ISOFolderScanner.SelectedPath
            ListBox1.Items.Clear()
            FoundFileNum = 0
            If CheckBox1.Checked = True Then
                Try
                    If Directory.Exists(TextBox1.Text & "$RECYCLE.BIN") Then
                        If Directory.Exists(TextBox1.Text & "System Volume Information") Then
                            MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                            If DialogResult.OK Then
                                CheckBox1.Checked = False
                                ListBox1.Items.Clear()
                                For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                    FoundFileNum = FoundFileNum + 1
                                    CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ListBox1.Items.Add(foundFile)
                                Next
                            End If
                        Else
                            MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                            If DialogResult.OK Then
                                CheckBox1.Checked = False
                                ListBox1.Items.Clear()
                                For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                    FoundFileNum = FoundFileNum + 1
                                    CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ListBox1.Items.Add(foundFile)
                                Next
                            End If
                        End If
                    ElseIf Directory.Exists(TextBox1.Text & "System Volume Information") Then
                        MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                        If DialogResult.OK Then
                            CheckBox1.Checked = False
                            ListBox1.Items.Clear()
                            For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                FoundFileNum = FoundFileNum + 1
                                CounterLabel.Text = "Files found so far: " & FoundFileNum
                                ListBox1.Items.Add(foundFile)
                            Next
                        End If
                    Else
                        For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchAllSubDirectories, "*.iso")
                            If Not foundFile.Contains("System Volume Information") Then
                                FoundFileNum = FoundFileNum + 1
                                CounterLabel.Text = "Files found so far: " & FoundFileNum
                                ListBox1.Items.Add(foundFile)
                            End If
                        Next
                    End If
                Catch ex As UnauthorizedAccessException
                    MsgBox("Oops. An exception occured whilst scanning for ISO images. The process will be repeated again, however, this time, searching for the specified directory, without considering subdirectories.", vbOKOnly + vbCritical, "Scan for ISO files")
                    If DialogResult.OK Then
                        CheckBox1.Checked = False
                        ListBox1.Items.Clear()
                        For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                            FoundFileNum = FoundFileNum + 1
                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                            ListBox1.Items.Add(foundFile)
                        Next
                    End If
                End Try
            Else
                Try
                    For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                        FoundFileNum = FoundFileNum + 1
                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                        ListBox1.Items.Add(foundFile)
                    Next
                Catch ex As ArgumentException
                    ' Do not do anything
                End Try

            End If
            Label3.Visible = False
            If ListBox1.Items.Count = 0 Then
                CounterLabel.Text = "Files found: 0"
                MsgBox("There are no ISO files on the directory: " & ControlChars.Quote & TextBox1.Text & ControlChars.Quote & Environment.NewLine & "Please look at other directories. You might also find searching on subdirectories useful.", vbOKOnly + vbInformation, "Scan for ISO files")
            Else
                CounterLabel.Text = "Files found: " & FoundFileNum
            End If
        End If
    End Sub

    Private Sub OK_Button_Click_1(sender As Object, e As EventArgs) Handles OK_Button.Click
        Me.Close()
        If RadioButton1.Checked = True Then
            MainForm.TextBox1.Text = ListBox1.SelectedItem
        Else
            MainForm.TextBox2.Text = ListBox1.SelectedItem
        End If
        BackSubPanel.Close()
    End Sub

    Private Sub Cancel_Button_Click_1(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub ISOFileScanPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Computer.Info.OSFullName.Contains("Windows 7") Or My.Computer.Info.OSFullName.Contains("Windows 8") Or My.Computer.Info.OSFullName.Contains("Windows 8.1") Or My.Computer.Info.OSFullName.Contains("Windows 10") Then
            Button1.FlatStyle = FlatStyle.System
            Button2.FlatStyle = FlatStyle.System
        End If
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
            TextBox1.BackColor = Color.White
            TextBox1.ForeColor = Color.Black
            ListBox1.BackColor = Color.White
            ListBox1.ForeColor = Color.Black
            PictureBox1.Image = New Bitmap(My.Resources.pref_reset)
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            Me.BackColor = Color.FromArgb(43, 43, 43)
            TextBox1.BackColor = Color.FromArgb(43, 43, 43)
            TextBox1.ForeColor = Color.White
            ListBox1.BackColor = Color.FromArgb(43, 43, 43)
            ListBox1.ForeColor = Color.White
            Me.ForeColor = Color.White
            PictureBox1.Image = New Bitmap(My.Resources.pref_reset_dark)
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedItem = "" Then
            OK_Button.Enabled = False
        Else
            OK_Button.Enabled = True
        End If
    End Sub

    Private Sub PictureBox1_MouseHover(sender As Object, e As EventArgs) Handles PictureBox1.MouseHover
        RefreshToolTip.SetToolTip(PictureBox1, "Click here to reload the file search results.")
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If Directory.Exists(TextBox1.Text) Then
            Label3.Visible = True
            CounterLabel.Visible = True
            TextBox1.Text = ISOFolderScanner.SelectedPath
            ListBox1.Items.Clear()
            FoundFileNum = 0
            If CheckBox1.Checked = True Then
                Try
                    If Directory.Exists(TextBox1.Text & "$RECYCLE.BIN") Then
                        If Directory.Exists(TextBox1.Text & "System Volume Information") Then
                            MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                            If DialogResult.OK Then
                                CheckBox1.Checked = False
                                ListBox1.Items.Clear()
                                For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                    FoundFileNum = FoundFileNum + 1
                                    CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ListBox1.Items.Add(foundFile)
                                Next
                            End If
                        Else
                            MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                            If DialogResult.OK Then
                                CheckBox1.Checked = False
                                ListBox1.Items.Clear()
                                For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                    FoundFileNum = FoundFileNum + 1
                                    CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ListBox1.Items.Add(foundFile)
                                Next
                            End If
                        End If
                    ElseIf Directory.Exists(TextBox1.Text & "System Volume Information") Then
                        MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                        If DialogResult.OK Then
                            CheckBox1.Checked = False
                            ListBox1.Items.Clear()
                            For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                FoundFileNum = FoundFileNum + 1
                                CounterLabel.Text = "Files found so far: " & FoundFileNum
                                ListBox1.Items.Add(foundFile)
                            Next
                        End If
                    Else
                        For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchAllSubDirectories, "*.iso")
                            If Not foundFile.Contains("System Volume Information") Then
                                FoundFileNum = FoundFileNum + 1
                                CounterLabel.Text = "Files found so far: " & FoundFileNum
                                ListBox1.Items.Add(foundFile)
                            End If
                        Next
                    End If
                Catch ex As UnauthorizedAccessException
                    MsgBox("Oops. An exception occured whilst scanning for ISO images. The process will be repeated again, however, this time, searching for the specified directory, without considering subdirectories.", vbOKOnly + vbCritical, "Scan for ISO files")
                    If DialogResult.OK Then
                        CheckBox1.Checked = False
                        ListBox1.Items.Clear()
                        For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                            FoundFileNum = FoundFileNum + 1
                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                            ListBox1.Items.Add(foundFile)
                        Next
                    End If
                End Try
            Else
                For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                    FoundFileNum = FoundFileNum + 1
                    CounterLabel.Text = "Files found so far: " & FoundFileNum
                    ListBox1.Items.Add(foundFile)
                Next
            End If
            Label3.Visible = False
            If ListBox1.Items.Count = 0 Then
                CounterLabel.Text = "Files found: 0"
                MsgBox("There are no ISO files on the directory: " & ControlChars.Quote & TextBox1.Text & ControlChars.Quote & Environment.NewLine & "Please look at other directories. You might also find searching on subdirectories useful.", vbOKOnly + vbInformation, "Scan for ISO files")
            Else
                CounterLabel.Text = "Files found: " & FoundFileNum
            End If
        Else
            MsgBox("The directory: " & Quote & TextBox1.Text & Quote & " cannot be scanned because it was not found. Please specify a different folder now.", vbOKOnly + vbInformation, "Scan for ISO images")
            If DialogResult.OK Then
                ISOFolderScanner.ShowDialog()
                If DialogResult.OK Then
                    Label3.Visible = True
                    CounterLabel.Visible = True
                    TextBox1.Text = ISOFolderScanner.SelectedPath
                    ListBox1.Items.Clear()
                    FoundFileNum = 0
                    If CheckBox1.Checked = True Then
                        Try
                            If Directory.Exists(TextBox1.Text & "$RECYCLE.BIN") Then
                                If Directory.Exists(TextBox1.Text & "System Volume Information") Then
                                    MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                                    If DialogResult.OK Then
                                        CheckBox1.Checked = False
                                        ListBox1.Items.Clear()
                                        For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                            FoundFileNum = FoundFileNum + 1
                                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                                            ListBox1.Items.Add(foundFile)
                                        Next
                                    End If
                                Else
                                    MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                                    If DialogResult.OK Then
                                        CheckBox1.Checked = False
                                        ListBox1.Items.Clear()
                                        For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                            FoundFileNum = FoundFileNum + 1
                                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                                            ListBox1.Items.Add(foundFile)
                                        Next
                                    End If
                                End If
                            ElseIf Directory.Exists(TextBox1.Text & "System Volume Information") Then
                                MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                                If DialogResult.OK Then
                                    CheckBox1.Checked = False
                                    ListBox1.Items.Clear()
                                    For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                        FoundFileNum = FoundFileNum + 1
                                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                                        ListBox1.Items.Add(foundFile)
                                    Next
                                End If
                            Else
                                For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchAllSubDirectories, "*.iso")
                                    If Not foundFile.Contains("System Volume Information") Then
                                        FoundFileNum = FoundFileNum + 1
                                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                                        ListBox1.Items.Add(foundFile)
                                    End If
                                Next
                            End If
                        Catch ex As UnauthorizedAccessException
                            MsgBox("Oops. An exception occured whilst scanning for ISO images. The process will be repeated again, however, this time, searching for the specified directory, without considering subdirectories.", vbOKOnly + vbCritical, "Scan for ISO files")
                            If DialogResult.OK Then
                                CheckBox1.Checked = False
                                ListBox1.Items.Clear()
                                For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                    FoundFileNum = FoundFileNum + 1
                                    CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ListBox1.Items.Add(foundFile)
                                Next
                            End If
                        End Try
                    Else
                        For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                            FoundFileNum = FoundFileNum + 1
                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                            ListBox1.Items.Add(foundFile)
                        Next
                    End If
                    Label3.Visible = False
                    If ListBox1.Items.Count = 0 Then
                        CounterLabel.Text = "Files found: 0"
                        MsgBox("There are no ISO files on the directory: " & ControlChars.Quote & TextBox1.Text & ControlChars.Quote & Environment.NewLine & "Please look at other directories. You might also find searching on subdirectories useful.", vbOKOnly + vbInformation, "Scan for ISO files")
                    Else
                        CounterLabel.Text = "Files found: " & FoundFileNum
                    End If
                End If
            End If
        End If
    End Sub
End Class
