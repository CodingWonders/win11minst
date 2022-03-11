Imports System.Windows.Forms

Public Class AdvancedOptionsPanel

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub AdvancedOptionsPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' This was a condition, but when I opened this solution in VS2022, IntelliCode suggested this.
        ' It's more efficient, and it works!
        CheckBox1.Enabled = My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator)
        CheckBox2.Enabled = My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator)

        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            Me.BackColor = Color.FromArgb(43, 43, 43)
            Me.ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
        End If
    End Sub
End Class
