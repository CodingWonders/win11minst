Public Class BackSubPanel

    Private Sub BackSubPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = MainForm.Size
        Me.Location = MainForm.Location
    End Sub
End Class