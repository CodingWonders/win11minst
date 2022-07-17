Imports System.Windows.Forms

Public Class DebugPanel

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click, Button1.Click
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub DebugPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.White
            ForeColor = Color.Black
            TabPage1.BackColor = Color.White
            TabPage1.ForeColor = Color.Black
            TabPage2.BackColor = Color.White
            TabPage2.ForeColor = Color.Black
            TextBox1.BackColor = Color.White
            TextBox1.ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            OK_Button.BackColor = Color.FromArgb(1, 92, 186)
            OK_Button.ForeColor = Color.White
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(43, 43, 43)
            ForeColor = Color.White
            TabPage1.BackColor = Color.FromArgb(43, 43, 43)
            TabPage1.ForeColor = Color.White
            TabPage2.BackColor = Color.FromArgb(43, 43, 43)
            TabPage2.ForeColor = Color.White
            TextBox1.BackColor = Color.FromArgb(43, 43, 43)
            TextBox1.ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            OK_Button.BackColor = Color.FromArgb(76, 194, 255)
            OK_Button.ForeColor = Color.Black
        End If
    End Sub
End Class
