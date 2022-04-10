Imports System.Windows.Forms

Public Class InstHistPanel

    Private Sub InstHistPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            Label1.Text = "Installer history"
            InstallerEntryLabel.Text = "Installer history entries: " & InstallerListView.Items.Count
            ColumnHeader1.Text = "Installer name and path"
            ColumnHeader2.Text = "Creation time and date"
            OK_Button.Text = "OK"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            Label1.Text = "Historial de instaladores"
            InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstallerListView.Items.Count
            ColumnHeader1.Text = "Nombre y ruta del instalador"
            ColumnHeader2.Text = "Fecha y hora de creación"
            OK_Button.Text = "Aceptar"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Installer history"
                InstallerEntryLabel.Text = "Installer history entries: " & InstallerListView.Items.Count
                ColumnHeader1.Text = "Installer name and path"
                ColumnHeader2.Text = "Creation time and date"
                OK_Button.Text = "OK"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Historial de instaladores"
                InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstallerListView.Items.Count
                ColumnHeader1.Text = "Nombre y ruta del instalador"
                ColumnHeader2.Text = "Fecha y hora de creación"
                OK_Button.Text = "Aceptar"
            End If
        End If
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            InstallerListView.ForeColor = Color.Black
            InstallerListView.BackColor = Color.White
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            Me.BackColor = Color.FromArgb(43, 43, 43)
            Me.ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            InstallerListView.ForeColor = Color.White
            InstallerListView.BackColor = Color.FromArgb(43, 43, 43)
        End If
        If InstallerListView.Items.Count = 0 Then
            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                InstallerEntryLabel.Text = "Installer history entries: " & InstallerListView.Items.Count & ". No installer history data is available."
            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstallerListView.Items.Count & ". No hay datos disponibles sobre el historial de instaladores."
            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    InstallerEntryLabel.Text = "Installer history entries: " & InstallerListView.Items.Count & ". No installer history data is available."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstallerListView.Items.Count & ". No hay datos disponibles sobre el historial de instaladores."
                End If
            End If
        End If
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Me.Close()
        BackSubPanel.Close()
    End Sub
End Class
