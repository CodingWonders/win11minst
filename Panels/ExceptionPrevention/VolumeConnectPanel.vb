Imports System.Windows.Forms
Imports System.IO

Public Class VolumeConnectPanel
    Dim Counter As Integer = 0
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        OK_Button.Enabled = False
        Label3.Visible = True
        Do Until Counter = 1000
            Counter = Counter + 1
        Loop
        If Counter = 1000 Then
            If Directory.Exists(MainForm.TextBox4.Text) Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
                OK_Button.Enabled = True
                Label3.Visible = False
                Counter = 0
                BackSubPanel.Close()
            Else
                Counter = 0
                OK_Button.Enabled = True
                Label3.Visible = False
            End If
        End If

    End Sub

    Private Sub FileCopyPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            OK_Button.BackColor = Color.FromArgb(1, 92, 186)
            OK_Button.ForeColor = Color.White
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            Me.BackColor = Color.FromArgb(43, 43, 43)
            Me.ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            OK_Button.BackColor = Color.FromArgb(76, 194, 255)
            OK_Button.ForeColor = Color.Black
        End If
        My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation)
    End Sub
End Class
