﻿Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars
Imports System.Net
Imports System.IO
Imports System.Text.Encoding
Imports System.Windows.Forms.MouseEventArgs

Public Class ISOFileDownloadPanel

    Dim WinUUP As ProcessStartInfo

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.No
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub ISOFileDownloadPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label1.Text = "Download ISO files..."
            Label2.Text = "Download"
            Label4.Text = "Loading web component. Please wait..."
            Label5.Text = "We couldn't load the web component." & CrLf & CrLf & "This can be caused by an unavailable Internet connection, or by a fault on the website backend. Please try again." & CrLf & "If the problem persists, please try to download the files manually by searching for " & Quote & "download windows 11" & Quote & " or " & Quote & "download windows 10" & Quote & " on the Internet."
            GroupBox1.Text = "Error status"
            Button1.Text = "Retry"
            OK_Button.Text = "OK"
            Cancel_Button.Text = "Cancel"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label1.Text = "Descargar archivos ISO..."
            Label2.Text = "Descargar"
            Label4.Text = "Cargando componente web. Por favor, espere..."
            Label5.Text = "No pudimos cargar el componente web." & CrLf & CrLf & "Esto puede ser causado por una conexión a Internet no disponible, o por un error en el backend de la página web. Por favor, inténtelo de nuevo." & CrLf & "Si el problema persiste, por favor, intente descargar los archivos manualmente buscando " & Quote & "descargar windows 11" & Quote & " o " & Quote & "descargar windows 10" & Quote & " en Internet."
            GroupBox1.Text = "Estado de error"
            Button1.Text = "Reintentar"
            OK_Button.Text = "Aceptar"
            Cancel_Button.Text = "Cancelar"
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label1.Text = "Télécharger les fichiers ISO..."
            Label2.Text = "Télécharger"
            Label4.Text = "Chargement du composant web. Veuillez patienter..."
            Label5.Text = "Nous n'avons pas pu charger le composant web." & CrLf & CrLf & "Cela peut être dû à une connexion Internet indisponible ou à une défaillance du backend du site Web. Veuillez réessayer." & CrLf & "Si le problème persiste, essayez de télécharger les fichiers manuellement en recherchant " & Quote & "télécharger windows 11" & Quote & " ou " & Quote & "télécharger windows 10" & Quote & " sur Internet."
            GroupBox1.Text = "État d'erreur"
            Button1.Text = "Réesayer"
            OK_Button.Text = "OK"
            Cancel_Button.Text = "Annuler"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Download ISO files..."
                Label2.Text = "Download"
                Label4.Text = "Loading web component. Please wait..."
                Label5.Text = "We couldn't load the web component." & CrLf & CrLf & "This can be caused by an unavailable Internet connection, or by a fault on the website backend. Please try again." & CrLf & "If the problem persists, please try to download the files manually by searching for " & Quote & "download windows 11" & Quote & " or " & Quote & "download windows 10" & Quote & " on the Internet."
                GroupBox1.Text = "Error status"
                Button1.Text = "Retry"
                OK_Button.Text = "OK"
                Cancel_Button.Text = "Cancel"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Descargar archivos ISO..."
                Label2.Text = "Descargar"
                Label4.Text = "Cargando componente web. Por favor, espere..."
                Label5.Text = "No pudimos cargar el componente web." & CrLf & CrLf & "Esto puede ser causado por una conexión a Internet no disponible, o por un error en el backend de la página web. Por favor, inténtelo de nuevo." & CrLf & "Si el problema persiste, por favor, intente descargar los archivos manualmente buscando " & Quote & "descargar windows 11" & Quote & " o " & Quote & "descargar windows 10" & Quote & " en Internet."
                GroupBox1.Text = "Estado de error"
                Button1.Text = "Reintentar"
                OK_Button.Text = "Aceptar"
                Cancel_Button.Text = "Cancelar"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label1.Text = "Télécharger les fichiers ISO..."
                Label2.Text = "Télécharger"
                Label4.Text = "Chargement du composant web. Veuillez patienter..."
                Label5.Text = "Nous n'avons pas pu charger le composant web." & CrLf & CrLf & "Cela peut être dû à une connexion Internet indisponible ou à une défaillance du backend du site Web. Veuillez réessayer." & CrLf & "Si le problème persiste, essayez de télécharger les fichiers manuellement en recherchant " & Quote & "télécharger windows 11" & Quote & " ou " & Quote & "télécharger windows 10" & Quote & " sur Internet."
                GroupBox1.Text = "État d'erreur"
                Button1.Text = "Réesayer"
                OK_Button.Text = "OK"
                Cancel_Button.Text = "Annuler"
            End If
        End If
        Text = Label1.Text
        If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
            BuildModeToolStripMenuItem.Visible = True
        Else
            BuildModeToolStripMenuItem.Visible = False
        End If
        UserAgentChanger.SetUserAgent("Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.5060.114 Safari/537.36")
        UAToolStripMenuItem.Text = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.5060.114 Safari/537.36"
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.White
            ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            OK_Button.BackColor = Color.FromArgb(1, 92, 186)
            OK_Button.ForeColor = Color.White
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(43, 43, 43)
            ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            OK_Button.BackColor = Color.FromArgb(76, 194, 255)
            OK_Button.ForeColor = Color.Black
        End If
        Try
            If ModeSelectComboBox.SelectedItem = "Windows 11" Then
                WebComponent.Navigate("https://www.microsoft.com/" & MainForm.RegionalCode & "/software-download/windows11")
            ElseIf ModeSelectComboBox.SelectedItem = "Windows 10" Then
                WebComponent.Navigate("https://www.microsoft.com/" & MainForm.RegionalCode & "/software-download/windows10")
            End If
            WebComponentLoadPanel.Visible = False
            WebComponentPanel.Visible = True
        Catch WebEx As WebException
            WebComponentLoadPanel.Visible = False
            WebComponentLoadErrorPanel.Visible = True
            Label6.Text = "Error code: " & CType(WebEx.Response, HttpWebResponse).StatusCode & CrLf & CrLf & CType(WebEx.Response, HttpWebResponse).StatusDescription
        End Try
    End Sub

    Private Sub ModeSelectComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModeSelectComboBox.SelectedIndexChanged
        Try
            If ModeSelectComboBox.SelectedItem = "Windows 11" Then
                WebComponent.Navigate("https://www.microsoft.com/" & MainForm.RegionalCode & "/software-download/windows11")
            ElseIf ModeSelectComboBox.SelectedItem = "Windows 10" Then
                WebComponent.Navigate("https://www.microsoft.com/" & MainForm.RegionalCode & "/software-download/windows10")
            End If
            WebComponentLoadPanel.Visible = False
            WebComponentPanel.Visible = True
        Catch WebEx As WebException
            WebComponentLoadPanel.Visible = False
            WebComponentLoadErrorPanel.Visible = True
            Label6.Text = "Error code: " & CType(WebEx.Response, HttpWebResponse).StatusCode & CrLf & CrLf & CType(WebEx.Response, HttpWebResponse).StatusDescription
        End Try
    End Sub

    Private Sub UAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UAToolStripMenuItem.Click
        Process.Start("https://developers.whatismybrowser.com/useragents/parse/118164231chrome-linux-blink")
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        ContextMenuStrip1.Show(Label3, New Point(4, Label3.Height))
    End Sub

    Private Sub BuildModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuildModeToolStripMenuItem.Click
        If BuildModeToolStripMenuItem.Text = "Build mode" Or BuildModeToolStripMenuItem.Text = "Modo de compilación" Then
            WebComponentPanel.Visible = False
            BuildModePanel.Visible = True
            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                BuildModeToolStripMenuItem.Text = "Exit build mode"
            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                BuildModeToolStripMenuItem.Text = "Salir del modo de compilación"
            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    BuildModeToolStripMenuItem.Text = "Exit build mode"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    BuildModeToolStripMenuItem.Text = "Salir del modo de compilación"
                End If
            End If
            ModeSelectComboBox.Enabled = False
        ElseIf BuildModeToolStripMenuItem.Text = "Exit build mode" Or BuildModeToolStripMenuItem.Text = "Salir del modo de compilación" Then
            WebComponentPanel.Visible = True
            BuildModePanel.Visible = False
            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                BuildModeToolStripMenuItem.Text = "Build mode"
            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                BuildModeToolStripMenuItem.Text = "Modo de compilación"
            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    BuildModeToolStripMenuItem.Text = "Build mode"
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    BuildModeToolStripMenuItem.Text = "Modo de compilación"
                End If
            End If
            ModeSelectComboBox.Enabled = True
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If BuildBW.IsBusy = True Then
            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                MsgBox("The build process has already started. You cannot start this process again until after completion", vbOKOnly + vbExclamation, "Build process")
            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                MsgBox("El proceso de compilación ya ha comenzado. No puede empezar de nuevo este proceso hasta después de finalizar", vbOKOnly + vbExclamation, "Proceso de compilación")
            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    MsgBox("The build process has already started. You cannot start this process again until after completion", vbOKOnly + vbExclamation, "Build process")
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    MsgBox("El proceso de compilación ya ha comenzado. No puede empezar de nuevo este proceso hasta después de finalizar", vbOKOnly + vbExclamation, "Proceso de compilación")
                End If
            End If
        Else
            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                BuildSTLabel.Text = "Building image..."
            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                BuildSTLabel.Text = "Compilando imagen..."
            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    BuildSTLabel.Text = "Building image..."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    BuildSTLabel.Text = "Compilando imagen..."
                End If
            End If
            BuildPB.Style = ProgressBarStyle.Marquee
            BuildBW.RunWorkerAsync()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem = "" Then
            Button2.Enabled = False
        Else
            Button2.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox1.SelectedItem = "Public" Then
            Process.Start("https://uupdump.net/fetchupd.php?arch=amd64&ring=retail&build=19043.330")
        ElseIf ComboBox1.SelectedItem = "Release Preview" Then
            Process.Start("https://uupdump.net/fetchupd.php?arch=amd64&ring=rp&build=19044.1")
        ElseIf ComboBox1.SelectedItem = "Beta" Then
            Process.Start("https://uupdump.net/fetchupd.php?arch=amd64&ring=wis&build=19042.330")
        ElseIf ComboBox1.SelectedItem = "Dev" Then
            Process.Start("https://uupdump.net/fetchupd.php?arch=amd64&ring=wif&build=latest")
        End If
        TextBox1.Enabled = True
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            Button3.Enabled = False
        Else
            Button3.Enabled = True
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub BuildBW_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BuildBW.DoWork
        If Not Directory.Exists(".\uup") Then
            Directory.CreateDirectory(".\uup")
        End If
        If File.Exists(".\uup\uup.zip") Then
            File.Delete(".\uup\uup.zip")
        End If
        File.Copy(TextBox1.Text, ".\uup\uup.zip")
        Try
            File.WriteAllText(".\extract.bat", "@echo off" & CrLf & ".\prog_bin\7z x " & Quote & ".\uup\uup.zip" & Quote & " -o" & Quote & ".\uup" & Quote, ASCII)
            Process.Start(".\extract.bat").WaitForExit()
            File.Delete(".\extract.bat")
            File.WriteAllText(".\uup.bat", "@echo off" & CrLf & "echo 0 | .\uup\uup_download_windows.cmd", ASCII)
            Process.Start(".\uup.bat").WaitForExit()
            File.Delete(".\uup.bat")
        Catch Ex As Exception
            BuildBW.CancelAsync()
        End Try
        Try
            For Each ISOFileAttempt In My.Computer.FileSystem.GetFiles(".\uup", FileIO.SearchOption.SearchTopLevelOnly, "*.iso")
                File.Move(".\uup\*.iso", Environment.CurrentDirectory)
            Next
        Catch ex As Exception
            File.WriteAllText(".\temp.bat", "@echo off" & CrLf & "move .\uup\*.iso .", ASCII)
            Process.Start(".\temp.bat").WaitForExit()
            File.Delete(".\temp.bat")
        End Try
        For Each DeletedFile In My.Computer.FileSystem.GetFiles(".\uup", FileIO.SearchOption.SearchAllSubDirectories)
            File.Delete(DeletedFile)
        Next
        Try
            For Each DelDir In My.Computer.FileSystem.GetDirectories(".\uup", FileIO.SearchOption.SearchAllSubDirectories)
                Directory.Delete(DelDir)
            Next
        Catch ex As Exception
            File.WriteAllText(".\temp.bat", "@echo off" & CrLf & "rd .\uup /s /q", ASCII)
            Process.Start(".\temp.bat").WaitForExit()
            File.Delete(".\temp.bat")
        End Try
    End Sub

    Private Sub BuildBW_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BuildBW.RunWorkerCompleted
        BuildPB.Style = ProgressBarStyle.Blocks
        BuildPB.Value = 100
        MsgBox("ISO file built successfully. You can find it in the application runtime directory: " & Directory.GetCurrentDirectory(), vbOKOnly + vbInformation, "File build completion")
        BuildSTLabel.Text = "The build process has completed."
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        TextBox1.Text = OpenFileDialog1.FileName
    End Sub
End Class
