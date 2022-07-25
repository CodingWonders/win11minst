Imports System.Windows.Forms

Public Class LogExistsPanel

    Private Sub LogExistsPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label1.Text = "A log file already exists"
            Label2.Text = "Do you want to append the current log contents to the log file, or do you want to delete the existing log file?"
            Yes_Button.Text = "Append to existing log file"
            No_Button.Text = "Delete existing log file"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label1.Text = "Un archivo de registro ya existe"
            Label2.Text = "¿Desea anexar los contenidos del registro actual al archivo de registro, o desea borrar el archivo de registro existente?"
            Yes_Button.Text = "Anexar al archivo de registro existente"
            No_Button.Text = "Borrar archivo de registro existente"
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label1.Text = "Un fichier journal existe déjà"
            Label2.Text = "Voulez-vous ajouter le contenu du journal actuel au fichier journal, ou voulez-vous supprimer le fichier journal existant ?"
            Yes_Button.Text = "Ajouter au fichier journal existant"
            No_Button.Text = "Supprimer le fichier journal existant"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "A log file already exists"
                Label2.Text = "Do you want to append the current log contents to the log file, or do you want to delete the existing log file?"
                Yes_Button.Text = "Append to existing log file"
                No_Button.Text = "Delete existing log file"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Un archivo de registro ya existe"
                Label2.Text = "¿Desea anexar los contenidos del registro actual al archivo de registro, o desea borrar el archivo de registro existente?"
                Yes_Button.Text = "Anexar al archivo de registro existente"
                No_Button.Text = "Borrar archivo de registro existente"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label1.Text = "Un fichier journal existe déjà"
                Label2.Text = "Voulez-vous ajouter le contenu du journal actuel au fichier journal, ou voulez-vous supprimer le fichier journal existant ?"
                Yes_Button.Text = "Ajouter au fichier journal existant"
                No_Button.Text = "Supprimer le fichier journal existant"
            End If
        End If
        Text = Label1.Text
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.White
            ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            Yes_Button.BackColor = Color.FromArgb(1, 92, 186)
            Yes_Button.ForeColor = Color.White
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(43, 43, 43)
            ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            Yes_Button.BackColor = Color.FromArgb(76, 194, 255)
            Yes_Button.ForeColor = Color.Black
        End If
        Beep()
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