Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars
Imports System.IO


Public Class ISOFileScanPanel
    Public FoundFileNum As Integer
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
            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                MsgBox("You have decided to scan subdirectories for ISO files. This might take A LONG time, depending on the ISO images on the directory.", vbOKOnly + vbExclamation, Text)
            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                MsgBox("Decidió escanear subdirectorios por archivos ISO. Esto podría tardar MUCHO tiempo, dependiendo de las imágenes ISO en el directorio.", vbOKOnly + vbExclamation, Text)
            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    MsgBox("You have decided to scan subdirectories for ISO files. This might take A LONG time, depending on the ISO images on the directory.", vbOKOnly + vbExclamation, Text)
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    MsgBox("Decidió escanear subdirectorios por archivos ISO. Esto podría tardar MUCHO tiempo, dependiendo de las imágenes ISO en el directorio.", vbOKOnly + vbExclamation, Text)
                End If
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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
                            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                    MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                    MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                                End If
                            End If
                            If DialogResult.OK Then
                                CheckBox1.Checked = False
                                ListBox1.Items.Clear()
                                For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                    FoundFileNum = FoundFileNum + 1
                                    If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                        CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                    ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                            CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                        End If
                                    End If
                                    ListBox1.Items.Add(foundFile)
                                Next
                            End If
                        Else
                            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- $RECYCLE.BIN" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                    MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                    MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- $RECYCLE.BIN" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                                End If
                            End If
                            If DialogResult.OK Then
                                CheckBox1.Checked = False
                                ListBox1.Items.Clear()
                                For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                    FoundFileNum = FoundFileNum + 1
                                    If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                        CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                    ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                            CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                        End If
                                    End If
                                    ListBox1.Items.Add(foundFile)
                                Next
                            End If
                        End If
                    ElseIf Directory.Exists(TextBox1.Text & "System Volume Information") Then
                        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                            MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                            MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- System Volume Information" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- System Volume Information" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                            End If
                        End If
                        If DialogResult.OK Then
                            CheckBox1.Checked = False
                            ListBox1.Items.Clear()
                            For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                FoundFileNum = FoundFileNum + 1
                                If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                    CounterLabel.Text = "Files found so far: " & FoundFileNum
                                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                    CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                        CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                    End If
                                End If
                                ListBox1.Items.Add(foundFile)
                            Next
                        End If
                    Else
                        For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchAllSubDirectories, "*.iso")
                            If Not foundFile.Contains("System Volume Information") Then
                                FoundFileNum = FoundFileNum + 1
                                If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                    CounterLabel.Text = "Files found so far: " & FoundFileNum
                                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                    CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                        CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                    End If
                                End If
                                ListBox1.Items.Add(foundFile)
                            End If
                        Next
                    End If
                Catch ex As UnauthorizedAccessException
                    If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                        MsgBox("Oops. An exception occured whilst scanning for ISO images. The process will be repeated again, however, this time, searching for the specified directory, without considering subdirectories.", vbOKOnly + vbCritical, Text)
                    ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                        MsgBox("Upss. Ha ocurrido una excepción mientras se escaneaban por imágenes ISO. El proceso se repetirá de nuevo pero, esta vez, buscando en el directorio especificado, sin considerar subdirectorios.", vbOKOnly + vbCritical, Text)
                    ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                            MsgBox("Oops. An exception occured whilst scanning for ISO images. The process will be repeated again, however, this time, searching for the specified directory, without considering subdirectories.", vbOKOnly + vbCritical, Text)
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                            MsgBox("Upss. Ha ocurrido una excepción mientras se escaneaban por imágenes ISO. El proceso se repetirá de nuevo pero, esta vez, buscando en el directorio especificado, sin considerar subdirectorios.", vbOKOnly + vbCritical, Text)
                        End If
                    End If

                    If DialogResult.OK Then
                        CheckBox1.Checked = False
                        ListBox1.Items.Clear()
                        For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                            FoundFileNum = FoundFileNum + 1
                            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                CounterLabel.Text = "Files found so far: " & FoundFileNum
                            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                    CounterLabel.Text = "Files found so far: " & FoundFileNum
                                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                    CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                End If
                            End If
                            ListBox1.Items.Add(foundFile)
                        Next
                    End If
                End Try
            Else
                Try
                    For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                        FoundFileNum = FoundFileNum + 1
                        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                            CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                CounterLabel.Text = "Files found so far: " & FoundFileNum
                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                            End If
                        End If
                        ListBox1.Items.Add(foundFile)
                    Next
                Catch ex As ArgumentException
                    ' Do not do anything
                End Try

            End If
            Label3.Visible = False
            If ListBox1.Items.Count = 0 Then
                If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                    CounterLabel.Text = "Files found so far: 0"
                    MsgBox("There are no ISO files on the directory: " & Quote & TextBox1.Text & Quote & CrLf & "Please look at other directories. You might also find searching on subdirectories useful.", vbOKOnly + vbInformation, Text)
                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                    CounterLabel.Text = "Archivos encontrados hasta ahora: 0"
                    MsgBox("No hay archivos ISO en el directorio: " & Quote & TextBox1.Text & Quote & CrLf & "Por favor, busque en otros directorios. Puede serle útil si también busca en subdirectorios.", vbOKOnly + vbInformation, Text)
                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        CounterLabel.Text = "Files found so far: 0"
                        MsgBox("There are no ISO files on the directory: " & Quote & TextBox1.Text & Quote & CrLf & "Please look at other directories. You might also find searching on subdirectories useful.", vbOKOnly + vbInformation, Text)
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        CounterLabel.Text = "Archivos encontrados hasta ahora: 0"
                        MsgBox("No hay archivos ISO en el directorio: " & Quote & TextBox1.Text & Quote & CrLf & "Por favor, busque en otros directorios. Puede serle útil si también busca en subdirectorios.", vbOKOnly + vbInformation, Text)
                    End If
                End If
            Else
                If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                    CounterLabel.Text = "Files found so far: " & FoundFileNum
                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                    CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                    End If
                End If
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
        Button1.FlatStyle = FlatStyle.System
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            Label1.Text = "Scan for ISO images"
            Label2.Text = "Directory to scan:"
            TextBox1.Left = 150
            TextBox1.Width = 468
            Label3.Text = "Scanning directory for ISO files..."
            Label5.Text = "This is a"
            Label4.Visible = True
            RadioButton1.Left = 504
            RadioButton2.Left = 599
            CounterLabel.Text = "Files found so far: " & FoundFileNum
            CheckBox1.Text = "Search subdirectories for ISO images"
            Button1.Text = "Browse..."
            OK_Button.Text = "OK"
            Cancel_Button.Text = "Cancel"
            ISOFolderScanner.Description = "Please select a directory to scan for ISO files:"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            Label1.Text = "Escanear imágenes ISO"
            Label2.Text = "Directorio a escanear:"
            TextBox1.Left = 171
            TextBox1.Width = 447
            Label3.Text = "Escaneando directorio por archivos ISO..."
            Label5.Text = "Este es un instalador de"
            Label4.Visible = False
            RadioButton1.Left = 586
            RadioButton2.Left = 681
            CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
            CheckBox1.Text = "Buscar en subdirectorios por archivos ISO"
            Button1.Text = "Examinar..."
            OK_Button.Text = "Aceptar"
            Cancel_Button.Text = "Cancelar"
            ISOFolderScanner.Description = "Por favor, elija un directorio para escanear archivos ISO:"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Scan for ISO images"
                Label2.Text = "Directory to scan:"
                TextBox1.Left = 150
                TextBox1.Width = 468
                Label3.Text = "Scanning directory for ISO files..."
                Label5.Text = "This is a"
                Label4.Visible = True
                RadioButton1.Left = 504
                RadioButton2.Left = 599
                CounterLabel.Text = "Files found so far: " & FoundFileNum
                CheckBox1.Text = "Search subdirectories for ISO images"
                Button1.Text = "Browse..."
                OK_Button.Text = "OK"
                Cancel_Button.Text = "Cancel"
                ISOFolderScanner.Description = "Please select a directory to scan for ISO files:"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Escanear imágenes ISO"
                Label2.Text = "Directorio a escanear:"
                TextBox1.Left = 171
                TextBox1.Width = 447
                Label3.Text = "Escaneando directorio por archivos ISO..."
                Label5.Text = "Este es un instalador de"
                Label4.Visible = False
                RadioButton1.Left = 586
                RadioButton2.Left = 681
                CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                CheckBox1.Text = "Buscar en subdirectorios por archivos ISO"
                Button1.Text = "Examinar..."
                OK_Button.Text = "Aceptar"
                Cancel_Button.Text = "Cancelar"
                ISOFolderScanner.Description = "Por favor, elija un directorio para escanear archivos ISO:"
            End If
        End If
        Text = Label1.Text
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
            TextBox1.BackColor = Color.White
            TextBox1.ForeColor = Color.Black
            ListBox1.BackColor = Color.White
            ListBox1.ForeColor = Color.Black
            PictureBox1.Image = New Bitmap(My.Resources.pref_reset)
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            OK_Button.BackColor = Color.FromArgb(1, 92, 186)
            OK_Button.ForeColor = Color.White
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            Me.BackColor = Color.FromArgb(43, 43, 43)
            TextBox1.BackColor = Color.FromArgb(43, 43, 43)
            TextBox1.ForeColor = Color.White
            ListBox1.BackColor = Color.FromArgb(43, 43, 43)
            ListBox1.ForeColor = Color.White
            Me.ForeColor = Color.White
            PictureBox1.Image = New Bitmap(My.Resources.pref_reset_dark)
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            OK_Button.BackColor = Color.FromArgb(76, 194, 255)
            OK_Button.ForeColor = Color.Black
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
                            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                    MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                    MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                                End If
                            End If
                            If DialogResult.OK Then
                                CheckBox1.Checked = False
                                ListBox1.Items.Clear()
                                For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                    FoundFileNum = FoundFileNum + 1
                                    If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                        CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                    ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                            CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                        End If
                                    End If
                                    ListBox1.Items.Add(foundFile)
                                Next
                            End If
                        Else
                            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- $RECYCLE.BIN" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                    MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                    MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- $RECYCLE.BIN" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                                End If
                            End If
                            If DialogResult.OK Then
                                CheckBox1.Checked = False
                                ListBox1.Items.Clear()
                                For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                    FoundFileNum = FoundFileNum + 1
                                    If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                        CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                    ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                            CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                        End If
                                    End If
                                    ListBox1.Items.Add(foundFile)
                                Next
                            End If
                        End If
                    ElseIf Directory.Exists(TextBox1.Text & "System Volume Information") Then
                        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                            MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                            MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- System Volume Information" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- System Volume Information" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                            End If
                        End If
                        If DialogResult.OK Then
                            CheckBox1.Checked = False
                            ListBox1.Items.Clear()
                            For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                FoundFileNum = FoundFileNum + 1
                                If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                    CounterLabel.Text = "Files found so far: " & FoundFileNum
                                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                    CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                        CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                    End If
                                End If
                                ListBox1.Items.Add(foundFile)
                            Next
                        End If
                    End If
                Catch ex As UnauthorizedAccessException
                    MsgBox("Oops. An exception occured whilst scanning for ISO images. The process will be repeated again, however, this time, searching for the specified directory, without considering subdirectories.", vbOKOnly + vbCritical, Text)
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
                    If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                    ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                        CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                    ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                            CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                        End If
                    End If
                    ListBox1.Items.Add(foundFile)
                Next
            End If
            Label3.Visible = False
            If ListBox1.Items.Count = 0 Then
                If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                    CounterLabel.Text = "Files found so far: 0"
                    MsgBox("There are no ISO files on the directory: " & Quote & TextBox1.Text & Quote & CrLf & "Please look at other directories. You might also find searching on subdirectories useful.", vbOKOnly + vbInformation, Text)
                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                    CounterLabel.Text = "Archivos encontrados hasta ahora: 0"
                    MsgBox("No hay archivos ISO en el directorio: " & Quote & TextBox1.Text & Quote & CrLf & "Por favor, busque en otros directorios. Puede serle útil si también busca en subdirectorios.", vbOKOnly + vbInformation, Text)
                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        CounterLabel.Text = "Files found so far: 0"
                        MsgBox("There are no ISO files on the directory: " & Quote & TextBox1.Text & Quote & CrLf & "Please look at other directories. You might also find searching on subdirectories useful.", vbOKOnly + vbInformation, Text)
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        CounterLabel.Text = "Archivos encontrados hasta ahora: 0"
                        MsgBox("No hay archivos ISO en el directorio: " & Quote & TextBox1.Text & Quote & CrLf & "Por favor, busque en otros directorios. Puede serle útil si también busca en subdirectorios.", vbOKOnly + vbInformation, Text)
                    End If
                End If
            Else
                If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                    CounterLabel.Text = "Files found so far: " & FoundFileNum
                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                    CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                    End If
                End If
            End If
        Else
            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                MsgBox("The directory: " & Quote & TextBox1.Text & Quote & " cannot be scanned because it was not found. Please specify a different folder now.", vbOKOnly + vbInformation, Text)
            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                MsgBox("El directorio: " & Quote & TextBox1.Text & Quote & " no pudo ser escaneado porque no ha sido encontrado. Por favor, especifique una carpeta distinta ahora.", vbOKOnly + vbInformation, Text)
            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    MsgBox("The directory: " & Quote & TextBox1.Text & Quote & " cannot be scanned because it was not found. Please specify a different folder now.", vbOKOnly + vbInformation, Text)
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    MsgBox("El directorio: " & Quote & TextBox1.Text & Quote & " no pudo ser escaneado porque no ha sido encontrado. Por favor, especifique una carpeta distinta ahora.", vbOKOnly + vbInformation, Text)
                End If
            End If
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
                                If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                    MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                    MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                        MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                        MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- $RECYCLE.BIN" & CrLf & "- System Volume Information" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                                    End If
                                End If
                                If DialogResult.OK Then
                                    CheckBox1.Checked = False
                                    ListBox1.Items.Clear()
                                    For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                        FoundFileNum = FoundFileNum + 1
                                        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                                        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                            CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                                CounterLabel.Text = "Files found so far: " & FoundFileNum
                                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                                CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                            End If
                                        End If
                                        ListBox1.Items.Add(foundFile)
                                    Next
                                End If
                            Else
                                If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                    MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                    MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- $RECYCLE.BIN" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                        MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- $RECYCLE.BIN" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                        MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- $RECYCLE.BIN" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                                    End If
                                End If
                                If DialogResult.OK Then
                                    CheckBox1.Checked = False
                                    ListBox1.Items.Clear()
                                    For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                        FoundFileNum = FoundFileNum + 1
                                        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                                        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                            CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                                CounterLabel.Text = "Files found so far: " & FoundFileNum
                                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                                CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                            End If
                                        End If
                                        ListBox1.Items.Add(foundFile)
                                    Next
                                End If
                            End If
                        ElseIf Directory.Exists(TextBox1.Text & "System Volume Information") Then
                            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- System Volume Information" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                    MsgBox("The directory you are about to scan contains these folders:" & CrLf & "- System Volume Information" & CrLf & "This will cause an unauthorized access exception. Please specify another directory, or scan for files on the directory without scanning on subdirectories", vbOKOnly + vbCritical, "Invalid directory")
                                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                    MsgBox("El directorio que está a punto de escanear contiene estas carpetas:" & CrLf & "- System Volume Information" & CrLf & "Esto causará una excepción de acceso no autorizado. Por favor, especifique otro directorio, o escanee por archivos en el directorio sin escanear en subdirectorios", vbOKOnly + vbCritical, "Directorio inválido")
                                End If
                            End If
                            If DialogResult.OK Then
                                CheckBox1.Checked = False
                                ListBox1.Items.Clear()
                                For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                    FoundFileNum = FoundFileNum + 1
                                    If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                        CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                    ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                            CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                        End If
                                    End If
                                    ListBox1.Items.Add(foundFile)
                                Next
                            End If
                        Else
                            For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchAllSubDirectories, "*.iso")
                                If Not foundFile.Contains("System Volume Information") Then
                                    FoundFileNum = FoundFileNum + 1
                                    If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                        CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                    ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                            CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                        End If
                                    End If
                                    ListBox1.Items.Add(foundFile)
                                End If
                            Next
                        End If
                    Catch ex As UnauthorizedAccessException
                        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                            MsgBox("Oops. An exception occured whilst scanning for ISO images. The process will be repeated again, however, this time, searching for the specified directory, without considering subdirectories.", vbOKOnly + vbCritical, Text)
                        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                            MsgBox("Upss. Ha ocurrido una excepción mientras se escaneaban por imágenes ISO. El proceso se repetirá de nuevo pero, esta vez, buscando en el directorio especificado, sin considerar subdirectorios.", vbOKOnly + vbCritical, Text)
                        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                MsgBox("Oops. An exception occured whilst scanning for ISO images. The process will be repeated again, however, this time, searching for the specified directory, without considering subdirectories.", vbOKOnly + vbCritical, Text)
                            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                MsgBox("Upss. Ha ocurrido una excepción mientras se escaneaban por imágenes ISO. El proceso se repetirá de nuevo pero, esta vez, buscando en el directorio especificado, sin considerar subdirectorios.", vbOKOnly + vbCritical, Text)
                            End If
                        End If

                        If DialogResult.OK Then
                            CheckBox1.Checked = False
                            ListBox1.Items.Clear()
                            For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                                FoundFileNum = FoundFileNum + 1
                                If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                    CounterLabel.Text = "Files found so far: " & FoundFileNum
                                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                    CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                        CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                    End If
                                End If
                                ListBox1.Items.Add(foundFile)
                            Next
                        End If
                    End Try
                Else
                    Try
                        For Each foundFile In My.Computer.FileSystem.GetFiles(ISOFolderScanner.SelectedPath, FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                            FoundFileNum = FoundFileNum + 1
                            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                CounterLabel.Text = "Files found so far: " & FoundFileNum
                            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                    CounterLabel.Text = "Files found so far: " & FoundFileNum
                                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                    CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                                End If
                            End If
                            ListBox1.Items.Add(foundFile)
                        Next
                    Catch ex As ArgumentException
                        ' Do not do anything
                    End Try
                End If
                Label3.Visible = False
                If ListBox1.Items.Count = 0 Then
                    If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                        CounterLabel.Text = "Files found so far: 0"
                        MsgBox("There are no ISO files on the directory: " & Quote & TextBox1.Text & Quote & CrLf & "Please look at other directories. You might also find searching on subdirectories useful.", vbOKOnly + vbInformation, Text)
                    ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                        CounterLabel.Text = "Archivos encontrados hasta ahora: 0"
                        MsgBox("No hay archivos ISO en el directorio: " & Quote & TextBox1.Text & Quote & CrLf & "Por favor, busque en otros directorios. Puede serle útil si también busca en subdirectorios.", vbOKOnly + vbInformation, Text)
                    ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                            CounterLabel.Text = "Files found so far: 0"
                            MsgBox("There are no ISO files on the directory: " & Quote & TextBox1.Text & Quote & CrLf & "Please look at other directories. You might also find searching on subdirectories useful.", vbOKOnly + vbInformation, Text)
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                            CounterLabel.Text = "Archivos encontrados hasta ahora: 0"
                            MsgBox("No hay archivos ISO en el directorio: " & Quote & TextBox1.Text & Quote & CrLf & "Por favor, busque en otros directorios. Puede serle útil si también busca en subdirectorios.", vbOKOnly + vbInformation, Text)
                        End If
                    End If
                Else
                    If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                        CounterLabel.Text = "Files found so far: " & FoundFileNum
                    ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                        CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                    ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                            CounterLabel.Text = "Files found so far: " & FoundFileNum
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                            CounterLabel.Text = "Archivos encontrados hasta ahora: " & FoundFileNum
                        End If
                    End If
                End If
            End If
        End If
    End Sub
End Class
