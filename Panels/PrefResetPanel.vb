Imports System.Windows.Forms

Public Class PrefResetPanel

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub PrefResetPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            Me.BackColor = Color.FromArgb(43, 43, 43)
            Me.ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
        End If
        No_Button.Text = "No"
        Yes_Button.Visible = True
        ProgressBar1.Value = 0
        ProgressBar1.Visible = False
        Label3.Visible = False
    End Sub

    Private Sub No_Button_Click(sender As Object, e As EventArgs) Handles No_Button.Click
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub Yes_Button_Click(sender As Object, e As EventArgs) Handles Yes_Button.Click
        ' Begin resetting preferences
        Yes_Button.Visible = False
        If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
            No_Button.Text = "Aceptar"
        ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
            No_Button.Text = "OK"
        End If
        ProgressBar1.Value = 0
        ProgressBar1.Visible = True
        Label3.Visible = True
        MainForm.ComboBox1.SelectedItem = "Automatic"
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.White
            ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(43, 43, 43)
            ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
        End If
        MainForm.ComboBox4.SelectedItem = "Automatic"
        MainForm.LabelText.Text = "Windows11"
        MainForm.LabelSetButton.PerformClick()
        ProgressBar1.Value = 100
    End Sub
End Class
