Imports System.Windows.Forms
Imports System.Net
Imports Microsoft.VisualBasic.ControlChars
Imports System.IO
Imports System.Text.Encoding

Public Class UpdateChoicePanel
    Dim VerTag As String
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Using Win11MinstDown As New WebClient()
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Try
                Win11MinstDown.DownloadFile("https://github.com/CodingWonders/win11minst/blob/bin/Debug/win11minst.exe?raw=true", ".\win11minst_new.exe")
            Catch ex As Exception
                If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
                    MsgBox("We could not download the new version for you. You will have to do this manually. You can still use the current version.", vbOKOnly + vbCritical, "Update download failure")
                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
                    MsgBox("No pudimos descargar la nueva versión por usted. Tendrá que hacer esto manualmente. Aun así, todavía puede utilizar la versión actual.", vbOKOnly + vbCritical, "Error de descarga de la actualización")
                ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
                    MsgBox("Nous n'avons pas pu télécharger la nouvelle version pour vous. Vous devrez le faire manuellement. Vous pouvez toujours utiliser la version actuelle.", vbOKOnly + vbCritical, "Échec du téléchargement de la mise à jour")
                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        MsgBox("We could not download the new version for you. You will have to do this manually. You can still use the current version.", vbOKOnly + vbCritical, "Update download failure")
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        MsgBox("No pudimos descargar la nueva versión por usted. Tendrá que hacer esto manualmente. Aun así, todavía puede utilizar la versión actual.", vbOKOnly + vbCritical, "Error de descarga de la actualización")
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        MsgBox("Nous n'avons pas pu télécharger la nouvelle version pour vous. Vous devrez le faire manuellement. Vous pouvez toujours utiliser la version actuelle.", vbOKOnly + vbCritical, "Échec du téléchargement de la mise à jour")
                    End If
                    If DialogResult.OK Then
                        Me.Close()
                    End If
                End If
            End Try
        End Using
        ' NEW version of PUCS
        Using NewVerDown As New WebClient()
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Try
                If File.Exists(".\new.zip") Then
                    File.Delete(".\new.zip")
                End If
                NewVerDown.DownloadFile("https://github.com/CodingWonders/win11minst/releases/download/" & VerTag & "/win11minst.zip", ".\new.zip")
                File.SetAttributes(".\new.zip", FileAttributes.Hidden)
                Directory.CreateDirectory(".\new")
                File.WriteAllText(".\ex.bat", "@echo off" & CrLf & ".\prog_bin\7z x .\new.zip -o.\new", ASCII)
                Process.Start(".\ex.bat").WaitForExit()
                File.Delete(".\ex.bat")
                File.Delete(".\new.zip")
            Catch ex As Exception

            End Try
        End Using
        Using PUCSUpdate As New WebClient()
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Try
                PUCSUpdate.DownloadFile("https://github.com/CodingWonders/win11minst/blob/upd/PassiveUpdateCheckSys/Win11Minst/upd.exe?raw=true", ".\upd.exe")
            Catch ex As Exception
                If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
                    MsgBox("We could not download the new version for you. You will have to do this manually. You can still use the current version.", vbOKOnly + vbCritical, "Update download failure")
                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
                    MsgBox("No pudimos descargar la nueva versión por usted. Tendrá que hacer esto manualmente. Aun así, todavía puede utilizar la versión actual.", vbOKOnly + vbCritical, "Error de descarga de la actualización")
                ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
                    MsgBox("Nous n'avons pas pu télécharger la nouvelle version pour vous. Vous devrez le faire manuellement. Vous pouvez toujours utiliser la version actuelle.", vbOKOnly + vbCritical, "Échec du téléchargement de la mise à jour")
                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        MsgBox("We could not download the new version for you. You will have to do this manually. You can still use the current version.", vbOKOnly + vbCritical, "Update download failure")
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                        MsgBox("No pudimos descargar la nueva versión por usted. Tendrá que hacer esto manualmente. Aun así, todavía puede utilizar la versión actual.", vbOKOnly + vbCritical, "Error de descarga de la actualización")
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                        MsgBox("Nous n'avons pas pu télécharger la nouvelle version pour vous. Vous devrez le faire manuellement. Vous pouvez toujours utiliser la version actuelle.", vbOKOnly + vbCritical, "Échec du téléchargement de la mise à jour")
                    End If
                End If
                If DialogResult.OK Then
                    Me.Close()
                End If
            End Try
        End Using
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            MsgBox("The program update process will take place when you click " & Quote & "OK" & Quote & "." & CrLf & CrLf & "The program will exit, and the update will proceed." & CrLf & "If you do not like the up-to-date version, or if the update did not go successfully, a backup of the old version will be made.", vbOKOnly + vbInformation, "Beginning the update...")
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            MsgBox("El proceso de actualización del programa ocurrirá cuando haga clic en " & Quote & "Aceptar" & Quote & "." & CrLf & CrLf & "El programa se cerrará, y la actualización comenzará." & CrLf & "Si no le gusta la versión actualizada, o si la actualización no fue como se esperaba, una copia de la versión antigua será realizada.", vbOKOnly + vbInformation, "Comenzando la actualización...")
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            MsgBox("Le processus de mise à jour du programme aura lieu lorsque vous cliquerez sur " & Quote & "OK" & Quote & "." & CrLf & CrLf & "Le programme se fermera et la mise à jour se poursuivra." & CrLf & "Si la version actualisée ne vous convient pas, ou si la mise à jour ne s'est pas déroulée avec succès, une sauvegarde de l'ancienne version sera effectuée.", vbOKOnly + vbInformation, "Début de la mise à jour...")
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                MsgBox("The program update process will take place when you click " & Quote & "OK" & Quote & "." & CrLf & CrLf & "The program will exit, and the update will proceed." & CrLf & "If you do not like the up-to-date version, or if the update did not go successfully, a backup of the old version will be made.", vbOKOnly + vbInformation, "Beginning the update...")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                MsgBox("El proceso de actualización del programa ocurrirá cuando haga clic en " & Quote & "Aceptar" & Quote & "." & CrLf & CrLf & "El programa se cerrará, y la actualización comenzará." & CrLf & "Si no le gusta la versión actualizada, o si la actualización no fue como se esperaba, una copia de la versión antigua será realizada.", vbOKOnly + vbInformation, "Comenzando la actualización...")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                MsgBox("Le processus de mise à jour du programme aura lieu lorsque vous cliquerez sur " & Quote & "OK" & Quote & "." & CrLf & CrLf & "Le programme se fermera et la mise à jour se poursuivra." & CrLf & "Si la version actualisée ne vous convient pas, ou si la mise à jour ne s'est pas déroulée avec succès, une sauvegarde de l'ancienne version sera effectuée.", vbOKOnly + vbInformation, "Début de la mise à jour...")
            End If
        End If
        If DialogResult.OK Then
            Process.Start(".\upd.exe")
            MainForm.SaveSettingsFile()
            MainForm.Notify.Visible = False
            End
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub UpdateChoicePanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        VerTag = TextBox2.Text.Replace("2.0.010_", "2.0_").ToString()
        Label1.Parent = PictureBox1
        Label1.BackColor = Color.Transparent
        Label2.Parent = PictureBox1
        Label2.BackColor = Color.Transparent
        Label3.Parent = PictureBox1
        Label3.BackColor = Color.Transparent
        Label4.Parent = PictureBox1
        Label4.BackColor = Color.Transparent
        PictureBox2.Parent = PictureBox1
        PictureBox2.BackColor = Color.Transparent
        RelNotesLink.Parent = PictureBox1
        RelNotesLink.BackColor = Color.Transparent
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label1.Text = "Updates are available"
            Label2.Text = "You can decide when do you want to install this update"
            Label3.Text = "This version:"
            Label4.Text = "Up-to-date version:"
            Label5.Text = "When you click " & Quote & "Install now" & Quote & ", the program will exit and update to the latest version."
            OK_Button.Text = "Install now"
            Cancel_Button.Text = "Install later"
            RelNotesLink.Text = "View release notes"
            TextBox1.Left = 177
            TextBox1.Width = 702
            TextBox2.Left = 226
            TextBox2.Width = 653
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label1.Text = "Hay actualizaciones disponibles"
            Label2.Text = "Usted puede decidir cuándo quiere instalar esta actualización"
            Label3.Text = "Esta versión:"
            Label4.Text = "Versión actualizada:"
            Label5.Text = "Cuando haga clic en " & Quote & "Instalar ahora" & Quote & ", el programa se cerrará y se actualizará a la última versión."
            OK_Button.Text = "Instalar ahora"
            Cancel_Button.Text = "Instalar después"
            RelNotesLink.Text = "Ver notas de la versión"
            TextBox1.Left = 178
            TextBox1.Width = 701
            TextBox2.Left = 228
            TextBox2.Width = 651
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label1.Text = "Mises à jour disponibles"
            Label2.Text = "Vous pouvez décider quand vous voulez installer cette mise à jour."
            Label3.Text = "Cette version :"
            Label4.Text = "Version actualisée :"
            Label5.Text = "Lorsque vous cliquez sur " & Quote & "Installer maintenant" & Quote & ", le programme se ferme et se met à jour avec la dernière version."
            OK_Button.Text = "Installer maintenant"
            Cancel_Button.Text = "Installer plus tard"
            RelNotesLink.Text = "Voir les notes de publication"
            TextBox1.Left = 190
            TextBox1.Width = 689
            TextBox2.Left = 222
            TextBox2.Width = 657
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Updates are available"
                Label2.Text = "You can decide when do you want to install this update"
                Label3.Text = "This version:"
                Label4.Text = "Up-to-date version:"
                Label5.Text = "When you click " & Quote & "Install now" & Quote & ", the program will exit and update to the latest version."
                OK_Button.Text = "Install now"
                Cancel_Button.Text = "Install later"
                RelNotesLink.Text = "View release notes"
                TextBox1.Left = 177
                TextBox1.Width = 702
                TextBox2.Left = 226
                TextBox2.Width = 653
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Hay actualizaciones disponibles"
                Label2.Text = "Usted puede decidir cuándo quiere instalar esta actualización"
                Label3.Text = "Esta versión:"
                Label4.Text = "Versión actualizada:"
                Label5.Text = "Cuando haga clic en " & Quote & "Instalar ahora" & Quote & ", el programa se cerrará y se actualizará a la última versión."
                OK_Button.Text = "Instalar ahora"
                Cancel_Button.Text = "Instalar después"
                RelNotesLink.Text = "Ver notas de la versión"
                TextBox1.Left = 178
                TextBox1.Width = 701
                TextBox2.Left = 228
                TextBox2.Width = 651
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label1.Text = "Mises à jour disponibles"
                Label2.Text = "Vous pouvez décider quand vous voulez installer cette mise à jour."
                Label3.Text = "Cette version :"
                Label4.Text = "Version actualisée :"
                Label5.Text = "Lorsque vous cliquez sur " & Quote & "Installer maintenant" & Quote & ", le programme se ferme et se met à jour avec la dernière version."
                OK_Button.Text = "Installer maintenant"
                Cancel_Button.Text = "Installer plus tard"
                RelNotesLink.Text = "Voir les notes de publication"
                TextBox1.Left = 190
                TextBox1.Width = 689
                TextBox2.Left = 222
                TextBox2.Width = 657
            End If
        End If
        Text = Label1.Text
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.White
            ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            OK_Button.BackColor = Color.FromArgb(1, 92, 186)
            OK_Button.ForeColor = Color.White
            PictureBox1.Image = New Bitmap(My.Resources.update_screen_light)
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(43, 43, 43)
            ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            OK_Button.BackColor = Color.FromArgb(76, 194, 255)
            OK_Button.ForeColor = Color.Black
            PictureBox1.Image = New Bitmap(My.Resources.update_screen_dark)
        End If
    End Sub

    Private Sub RelNotesLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles RelNotesLink.LinkClicked
        Process.Start("https://github.com/CodingWonders/win11minst/blob/relnotes.md")
    End Sub
End Class
