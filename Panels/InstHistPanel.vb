Imports System.Windows.Forms

Public Class InstHistPanel

    Private Sub InstHistPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            InstallerEntryLabel.Text = "Installer history entries: " & InstallerListView.Items.Count & ". No installer history data is available."
        End If
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Me.Close()
        BackSubPanel.Close()
    End Sub
End Class
