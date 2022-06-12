Imports System.Windows.Forms

Public Class LogExistsPanel

    Private Sub LogExistsPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            Label1.Text = "A log file already exists"
            Label2.Text = "Do you want to append the current log contents to the log file, or do you want to delete the existing log file?"
            Yes_Button.Text = "Append to existing log file"
            No_Button.Text = "Delete existing log file"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            Label1.Text = "Un archivo de registro ya existe"
            Label2.Text = "¿Desea anexar los contenidos del registro actual al archivo de registro, o desea borrar el archivo de registro existente?"
            Yes_Button.Text = "Anexar al archivo de registro existente"
            No_Button.Text = "Borrar archivo de registro existente"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
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