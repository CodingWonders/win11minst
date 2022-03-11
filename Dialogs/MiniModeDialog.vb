Imports System.Windows.Forms

Public Class MiniModeDialog

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Hide()
    End Sub

    Private Sub MiniModeDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox2.Parent = BackPic
        Label1.Parent = BackPic
        CheckBox1.Parent = BackPic
        OK_Button.Parent = BackPic
        PictureBox2.BackColor = Color.Transparent
        Label1.BackColor = Color.Transparent
        CheckBox1.BackColor = Color.Transparent
        OK_Button.BackColor = Color.Transparent
    End Sub
End Class
