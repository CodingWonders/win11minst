Imports System.Windows.Forms

Public Class LogMigratePanel

    Private Sub LogMigratePanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label1.Text = "Migrate old log files"
            Label2.Text = "The Windows 11 Manual Installer has detected log files created by previous versions of the program. Version 2.0 uses a different log format. Do you want to migrate the contents of old files, and append current log contents to the log file?"
            Yes_Button.Text = "Migrate"
            No_Button.Text = "Don't migrate"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label1.Text = "Migrar archivos de registro antiguos"
            Label2.Text = "El Instalador manual de Windows 11 ha detectado archivos de registro creados por versiones antiguas del programa. La versión 2.0 utiliza un formato de registro diferente. ¿Desea migrar los contenidos de los archivos antiguos, y anexar los contenidos actuales del registro al archivo de registro?"
            Yes_Button.Text = "Migrar"
            No_Button.Text = "No migrar"
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label1.Text = "Migrer les anciens fichiers journaux"
            Label2.Text = "L'installateur manuel de Windows 11 a détecté les fichiers journaux créés par les versions précédentes du programme. La version 2.0 utilise un format de journal différent. Voulez-vous migrer le contenu des anciens fichiers, et ajouter le contenu du journal actuel au fichier journal ?"
            Yes_Button.Text = "Migrer"
            No_Button.Text = "Ne pas migrer"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Migrate old log files"
                Label2.Text = "The Windows 11 Manual Installer has detected log files created by previous versions of the program. Version 2.0 uses a different log format. Do you want to migrate the contents of old files, and append current log contents to the log file?"
                Yes_Button.Text = "Migrate"
                No_Button.Text = "Don't migrate"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Migrar archivos de registro antiguos"
                Label2.Text = "El Instalador manual de Windows 11 ha detectado archivos de registro creados por versiones antiguas del programa. La versión 2.0 utiliza un formato de registro diferente. ¿Desea migrar los contenidos de los archivos antiguos, y anexar los contenidos actuales del registro al archivo de registro?"
                Yes_Button.Text = "Migrar"
                No_Button.Text = "No migrar"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label1.Text = "Migrer les anciens fichiers journaux"
                Label2.Text = "L'installateur manuel de Windows 11 a détecté les fichiers journaux créés par les versions précédentes du programme. La version 2.0 utilise un format de journal différent. Voulez-vous migrer le contenu des anciens fichiers, et ajouter le contenu du journal actuel au fichier journal ?"
                Yes_Button.Text = "Migrer"
                No_Button.Text = "Ne pas migrer"
            End If
        End If
        Text = Label1.Text
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            Yes_Button.BackColor = Color.FromArgb(1, 92, 186)
            Yes_Button.ForeColor = Color.White
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            Me.BackColor = Color.FromArgb(43, 43, 43)
            Me.ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            Yes_Button.BackColor = Color.FromArgb(76, 194, 255)
            Yes_Button.ForeColor = Color.Black
        End If
        My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation)
    End Sub

    Private Sub No_Button_Click(sender As Object, e As EventArgs) Handles No_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.No
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub Yes_Button_Click(sender As Object, e As EventArgs) Handles Yes_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.Close()
        BackSubPanel.Close()
    End Sub
End Class
