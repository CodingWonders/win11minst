Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars

Public Class LowSpacePanel

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub LowSpacePanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label1.Text = "Low space on selected target directory"
            Label2.Text = "The drive where the target directory you have specified is located does not have enough space to store temporary files for installer creation." & CrLf & CrLf & _
                "Please choose another directory to save the custom installer, run the program from another location, or free up some space on the drive." & CrLf & _
                "Disk space information:" & CrLf & CrLf & _
                "- Total size of installers: " & MainForm.TotalISO_Str & " GB" & CrLf & _
                "- Available disk space on " & Quote & MainForm.drLetter & "\" & Quote & ": " & MainForm.GBStr & " GB"
            OK_Button.Text = "OK"
            LinkLabel1.Text = "If this is an active Windows installation (where you have booted your computer to), you may consider using Disk Cleanup."
            LinkLabel1.LinkArea = New LinkArea(101, 18)
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label1.Text = "Poco espacio en directorio de destino señalado"
            Label2.Text = "El dispositivo donde el directorio de destino que especificó está ubicado no tiene espacio suficiente para almacenar los archivos temporales para la creación del instalador." & CrLf & CrLf & _
                "Por favor, elija otro directorio para guardar el instalador modificado, ejecute el programa desde otra ubicación, o libere algo de espacio en el dispositivo." & CrLf & _
                "Información del espacio en disco:" & CrLf & CrLf & _
                "- Tamaño total de los instaladores: " & MainForm.TotalISO_Str & " GB" & CrLf & _
                "- Espacio de disco disponible en " & Quote & MainForm.drLetter & "\" & Quote & ": " & MainForm.GBStr & " GB"
            OK_Button.Text = "Aceptar"
            LinkLabel1.Text = "Si esta es una instalación de Windows activa (donde usted inició su equipo), puede considerar utilizando Liberador de espacio en disco."
            LinkLabel1.LinkArea = New LinkArea(105, 28)
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label1.Text = "Espace insuffisant dans le répertoire cible sélectionné"
            Label2.Text = "Le disque où se trouve le répertoire cible que vous avez spécifié n'a pas assez d'espace pour stocker les fichiers temporaires pour la création de l'installateur." & CrLf & CrLf & _
                "Veuillez choisir un autre répertoire pour enregistrer l'installateur personnalisé, exécuter le programme depuis un autre endroit ou libérer de l'espace sur le disque." & CrLf & _
                "Informations sur l'espace disque :" & CrLf & CrLf & _
                "- Taille totale des installateurs : " & MainForm.TotalISO_Str & " Go" & CrLf & _
                "- Espace disque disponible sur " & Quote & MainForm.drLetter & "\" & Quote & ": " & MainForm.GBStr & " Go"
            OK_Button.Text = "OK"
            LinkLabel1.Text = "S'il s'agit d'une installation Windows active (où vous avez démarré votre ordinateur), vous pouvez envisager d'utiliser " & Quote & "Disk Cleanup" & Quote & "."
            LinkLabel1.LinkArea = New LinkArea(111, 23)
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Low space on selected target directory"
                Label2.Text = "The drive where the target directory you have specified is located does not have enough space to store temporary files for installer creation." & CrLf & CrLf & _
                    "Please choose another directory to save the custom installer, run the program from another location, or free up some space on the drive." & CrLf & _
                    "Disk space information:" & CrLf & CrLf & _
                    "- Total size of installers: " & MainForm.TotalISO_Str & " GB" & CrLf & _
                    "- Available disk space on " & Quote & MainForm.drLetter & "\" & Quote & ": " & MainForm.GBStr & " GB"
                OK_Button.Text = "OK"
                LinkLabel1.Text = "If this is an active Windows installation (where you have booted your computer to), you may consider using Disk Cleanup."
                LinkLabel1.LinkArea = New LinkArea(101, 18)
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Poco espacio en directorio de destino señalado"
                Label2.Text = "El dispositivo donde el directorio de destino que especificó está ubicado no tiene espacio suficiente para almacenar los archivos temporales para la creación del instalador." & CrLf & CrLf & _
                    "Por favor, elija otro directorio para guardar el instalador modificado, ejecute el programa desde otra ubicación, o libere algo de espacio en el dispositivo." & CrLf & _
                    "Información del espacio en disco:" & CrLf & CrLf & _
                    "- Tamaño total de los instaladores: " & MainForm.TotalISO_Str & " GB" & CrLf & _
                    "- Espacio de disco disponible en " & Quote & MainForm.drLetter & "\" & Quote & ": " & MainForm.GBStr & " GB"
                OK_Button.Text = "Aceptar"
                LinkLabel1.Text = "Si esta es una instalación de Windows activa (donde usted inició su equipo), puede considerar utilizando Liberador de espacio en disco."
                LinkLabel1.LinkArea = New LinkArea(105, 28)
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label1.Text = "Espace insuffisant dans le répertoire cible sélectionné"
                Label2.Text = "Le disque où se trouve le répertoire cible que vous avez spécifié n'a pas assez d'espace pour stocker les fichiers temporaires pour la création de l'installateur." & CrLf & CrLf & _
                    "Veuillez choisir un autre répertoire pour enregistrer l'installateur personnalisé, exécuter le programme depuis un autre endroit ou libérer de l'espace sur le disque." & CrLf & _
                    "Informations sur l'espace disque :" & CrLf & CrLf & _
                    "- Taille totale des installateurs : " & MainForm.TotalISO_Str & " Go" & CrLf & _
                    "- Espace disque disponible sur " & Quote & MainForm.drLetter & "\" & Quote & ": " & MainForm.GBStr & " Go"
                OK_Button.Text = "OK"
                LinkLabel1.Text = "S'il s'agit d'une installation Windows active (où vous avez démarré votre ordinateur), vous pouvez envisager d'utiliser " & Quote & "Disk Cleanup" & Quote & "."
                LinkLabel1.LinkArea = New LinkArea(111, 23)
            End If
        End If
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.White
            ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            OK_Button.BackColor = Color.FromArgb(1, 92, 186)
            OK_Button.ForeColor = Color.White
            LinkLabel1.LinkColor = Color.FromArgb(1, 92, 186)
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(43, 43, 43)
            ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            OK_Button.BackColor = Color.FromArgb(76, 194, 255)
            OK_Button.ForeColor = Color.Black
            LinkLabel1.LinkColor = Color.FromArgb(76, 194, 255)
        End If
        Text = Label1.Text
        Beep()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("cleanmgr")
    End Sub
End Class
