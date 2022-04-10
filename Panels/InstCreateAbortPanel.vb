Imports System.Windows.Forms

Public Class InstCreateAbortPanel

    Private Sub Yes_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Yes_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub No_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles No_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub FileNotFoundPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            Label1.Text = "Cancel installer creation?"
            Label2.Text = "Are you sure you want to cancel the installer creation process? This will delete any file modifications done at this time."
            Yes_Button.Text = "Yes"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            Label1.Text = "¿Cancelar la creación del instalador?"
            Label2.Text = "¿Está seguro de que quiere cancelar el proceso de creación del instalador? Esto borrará todas las modificaciones de archivos realizados en este momento."
            Yes_Button.Text = "Sí"
        End If
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
