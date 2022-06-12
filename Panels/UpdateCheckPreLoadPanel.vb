Imports System.Windows.Forms
Imports System.Net
Imports System.IO
Imports Microsoft.VisualBasic.ControlChars
Imports System.Text.Encoding

Public Class UpdateCheckPreLoadPanel
    Private isMouseDown As Boolean = False
    Private mouseOffset As Point
    Dim VerTag As String
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub UpdateCancelButton_Click(sender As Object, e As EventArgs) Handles UpdateCancelButton.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub UpdateCheckPreLoadPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Computer.Info.OSFullName.Contains("Windows 7") Or My.Computer.Info.OSFullName.Contains("Windows 8") Or My.Computer.Info.OSFullName.Contains("Windows 10") Then
            UpdateCancelButton.FlatStyle = FlatStyle.System
            Button1.FlatStyle = FlatStyle.System
            Button2.FlatStyle = FlatStyle.System
        End If
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.FromArgb(243, 243, 243)
            ForeColor = Color.Black
            GroupBox1.ForeColor = Color.Black
            closeBox.Image = New Bitmap(My.Resources.closebox)
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(32, 32, 32)
            ForeColor = Color.White
            GroupBox1.ForeColor = Color.White
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
        End If
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            Text = "Passive Update Check System - Checking for updates..."
            Label1.Text = "Checking for updates..."
            Label2.Text = "Your version:"
            Label3.Text = "Up-to-date version:"
            Label4.Text = "Version channel: hummingbird"
            Label5.Text = "Actions:"
            GroupBox1.Text = "Version information"
            Button1.Text = "Install later"
            Button2.Text = "Install now"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            Text = "Sistema de comprobación pasiva de actualizaciones - Comprobando actualizaciones..."
            Label1.Text = "Comprobando actualizaciones..."
            Label2.Text = "Su versión:"
            Label3.Text = "Versión actualizada:"
            Label4.Text = "Canal de versiones: hummingbird"
            Label5.Text = "Acciones:"
            GroupBox1.Text = "Información de versiones"
            Button1.Text = "Instalar después"
            Button2.Text = "Instalar ahora"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Text = "Passive Update Check System - Checking for updates..."
                Label1.Text = "Checking for updates..."
                Label2.Text = "Your version:"
                Label3.Text = "Up-to-date version:"
                Label4.Text = "Version channel: hummingbird"
                Label5.Text = "Actions:"
                GroupBox1.Text = "Version information"
                Button1.Text = "Install later"
                Button2.Text = "Install now"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Text = "Sistema de comprobación pasiva de actualizaciones - Comprobando actualizaciones..."
                Label1.Text = "Comprobando actualizaciones..."
                Label2.Text = "Su versión:"
                Label3.Text = "Versión actualizada:"
                Label4.Text = "Canal de versiones: hummingbird"
                Label5.Text = "Acciones:"
                GroupBox1.Text = "Información de versiones"
                Button1.Text = "Instalar después"
                Button2.Text = "Instalar ahora"
            End If
        End If
        CenterToScreen()
        Visible = True
        ' ACTIONS
        If My.Computer.Network.IsAvailable = False Then
            Me.Close()
        Else
            If System.IO.File.Exists(".\latest") Then
                TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\version")
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
                TextBox2.Text = My.Computer.FileSystem.ReadAllText(".\latest")
                If TextBox1.Text = TextBox2.Text Then
                    Me.Close()
                Else
                    Height = 318
                    CenterToScreen()
                    ProgressRingPic.Visible = False
                    Label1.Left = 12
                    If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                        Text = "Passive Update Check System - Updates available"
                        Label1.Text = "A new version is available. Do you want to install it now?"
                    ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                        Text = "Sistema de comprobación pasiva de actualizaciones - Actualizaciones disponibles"
                        Label1.Text = "Hay una nueva versión disponible. ¿Desea instalarla ahora?"
                    ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                            Text = "Passive Update Check System - Updates available"
                            Label1.Text = "A new version is available. Do you want to install it now?"
                        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                            Text = "Sistema de comprobación pasiva de actualizaciones - Actualizaciones disponibles"
                            Label1.Text = "Hay una nueva versión disponible. ¿Desea instalarla ahora?"
                        End If
                    End If
                    UpdateCancelButton.Visible = False
                    closeBox.Visible = True
                End If
            Else
                Using LATEST As New WebClient()
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                    Try
                        LATEST.DownloadFile("https://raw.githubusercontent.com/CodingWonders/win11minst/hummingbird/latest", ".\latest")
                        TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\version")
                        TextBox2.Text = My.Computer.FileSystem.ReadAllText(".\latest")
                        If TextBox1.Text = TextBox2.Text Then
                            Me.Close()
                        Else
                            Height = 318
                            CenterToScreen()
                            ProgressRingPic.Visible = False
                            Label1.Left = 12
                            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                                Text = "Passive Update Check System - Updates available"
                                Label1.Text = "A new version is available. Do you want to install it now?"
                            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                                Text = "Sistema de comprobación pasiva de actualizaciones - Actualizaciones disponibles"
                                Label1.Text = "Hay una nueva versión disponible. ¿Desea instalarla ahora?"
                            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                                    Text = "Passive Update Check System - Updates available"
                                    Label1.Text = "A new version is available. Do you want to install it now?"
                                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                                    Text = "Sistema de comprobación pasiva de actualizaciones - Actualizaciones disponibles"
                                    Label1.Text = "Hay una nueva versión disponible. ¿Desea instalarla ahora?"
                                End If
                            End If
                            UpdateCancelButton.Visible = False
                            closeBox.Visible = True
                        End If
                    Catch ex As Exception
                        Me.Close()
                    End Try
                End Using
            End If
        End If
        VerTag = TextBox2.Text.Replace("2.0.0100_", "beta_").ToString()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Using Win11MinstDown As New WebClient()
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Try
                Win11MinstDown.DownloadFile("https://github.com/CodingWonders/win11minst/blob/hummingbird/bin/Debug/win11minst.exe?raw=true", ".\win11minst_new.exe")
            Catch ex As Exception
                If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                    MsgBox("We could not download the new version for you. You will have to do this manually. You can still use the current version.", vbOKOnly + vbCritical, "Update download failure")
                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                    MsgBox("No pudimos descargar la nueva versión por usted. Tendrá que hacer esto manualmente. Aun así, todavía puede utilizar la versión actual.", vbOKOnly + vbCritical, "Error de descarga de la actualización")
                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        MsgBox("We could not download the new version for you. You will have to do this manually. You can still use the current version.", vbOKOnly + vbCritical, "Update download failure")
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        MsgBox("No pudimos descargar la nueva versión por usted. Tendrá que hacer esto manualmente. Aun así, todavía puede utilizar la versión actual.", vbOKOnly + vbCritical, "Error de descarga de la actualización")
                    End If
                End If
                If DialogResult.OK Then
                    Me.Close()
                End If
            End Try
        End Using
        Using RefDown As New WebClient()
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Try
                If File.Exists(".\new.zip") Then
                    File.Delete(".\new.zip")
                End If
                RefDown.DownloadFile("https://github.com/CodingWonders/win11minst/releases/download/" & VerTag & "/win11minst.zip", ".\new.zip")
                File.SetAttributes(".\new.zip", FileAttributes.Hidden)
                Directory.CreateDirectory(".\ref")
                File.WriteAllText(".\ex.bat", "@echo off" & CrLf & ".\prog_bin\7z e .\new.zip " & Quote & "*.dll" & Quote & " -o.\ref", ASCII)
                Process.Start(".\ex.bat").WaitForExit()
                File.Delete(".\ex.bat")
            Catch ex As Exception

            End Try
        End Using
        My.Computer.FileSystem.WriteAllText(".\upd.bat", "@echo off" & CrLf & _
                                                    "echo Updating the program. Please wait..." & CrLf & _
                                                    "del .\version && del .\latest" & CrLf & _
                                                    "move /y .\ref\*.dll ." & CrLf & _
                                                    "move .\win11minst.exe .\win11minst_old_v" & My.Application.Info.Version.ToString() & ".exe" & CrLf & _
                                                    "move .\win11minst_new.exe .\win11minst.exe" & CrLf & _
                                                    "ping -n 3 127.0.0.1 > nul" & CrLf & _
                                                    "rd .\ref" & CrLf & _
                                                    "win11minst & del .\upd.bat & exit", False, ASCII)
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            MsgBox("The program update process will take place when you click " & Quote & "OK" & Quote & "." & CrLf & CrLf & "The program will exit, and the update will proceed." & CrLf & "If you do not like the up-to-date version, or if the update did not go successfully, a backup of the old version will be made.", vbOKOnly + vbInformation, "Beginning the update...")
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            MsgBox("El proceso de actualización del programa ocurrirá cuando haga clic en " & Quote & "Aceptar" & Quote & "." & CrLf & CrLf & "El programa se cerrará, y la actualización comenzará." & CrLf & "Si no le gusta la versión actualizada, o si la actualización no fue como se esperaba, una copia de la versión antigua será realizada.", vbOKOnly + vbInformation, "Comenzando la actualización...")
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                MsgBox("The program update process will take place when you click " & Quote & "OK" & Quote & "." & CrLf & CrLf & "The program will exit, and the update will proceed." & CrLf & "If you do not like the up-to-date version, or if the update did not go successfully, a backup of the old version will be made.", vbOKOnly + vbInformation, "Beginning the update...")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                MsgBox("El proceso de actualización del programa ocurrirá cuando haga clic en " & Quote & "Aceptar" & Quote & "." & CrLf & CrLf & "El programa se cerrará, y la actualización comenzará." & CrLf & "Si no le gusta la versión actualizada, o si la actualización no fue como se esperaba, una copia de la versión antigua será realizada.", vbOKOnly + vbInformation, "Comenzando la actualización...")
            End If
        End If
        If DialogResult.OK Then
            Process.Start(".\upd.bat")
            End
        End If
    End Sub

    Private Sub UpdateCheckPreLoadPanel_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ' Get the new position
            mouseOffset = New Point(-e.X, -e.Y)
            ' Set that left button is pressed
            isMouseDown = True
        End If
    End Sub

    Private Sub UpdateCheckPreLoadPanel_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If isMouseDown Then
            Dim mousePos As Point = Control.MousePosition
            ' Get the new form position
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Location = mousePos
        End If
    End Sub

    Private Sub UpdateCheckPreLoadPanel_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            isMouseDown = False
        End If
    End Sub

    Private Sub closeBox_Click(sender As Object, e As EventArgs) Handles closeBox.Click
        MainForm.Notify.Visible = False
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

    Private Sub closeBox_MouseHover(sender As Object, e As EventArgs) Handles closeBox.MouseHover
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            closeToolTip.SetToolTip(closeBox, "Exit")
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            closeToolTip.SetToolTip(closeBox, "Salir")
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                closeToolTip.SetToolTip(closeBox, "Exit")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                closeToolTip.SetToolTip(closeBox, "Salir")
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
End Class
