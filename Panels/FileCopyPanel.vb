Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars

Public Class FileCopyPanel

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.No
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub FileCopyPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            Label1.Text = "File copy"
            Label2.Text = "To prevent file access errors while creating the custom installer, the source files will be copied to the local disk. These files will be deleted after the program has finished, to save disk space." & CrLf & "Do you want to do so?"
            OK_Button.Text = "Yes"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            Label1.Text = "Copia de archivos"
            Label2.Text = "Para prevenir errores de acceso de archivo al crear el instalador modificado, los archivos de origen serán copiados al disco local. Éstos archivos serán borrados después de que el programa haya terminado, para ahorrar espacio en el disco." & CrLf & "¿Desea hacer esto?"
            OK_Button.Text = "Sí"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "File copy"
                Label2.Text = "To prevent file access errors while creating the custom installer, the source files will be copied to the local disk. These files will be deleted after the program has finished, to save disk space." & CrLf & "Do you want to do so?"
                OK_Button.Text = "Yes"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Copia de archivos"
                Label2.Text = "Para prevenir errores de acceso de archivo al crear el instalador modificado, los archivos de origen serán copiados al disco local. Éstos archivos serán borrados después de que el programa haya terminado, para ahorrar espacio en el disco." & CrLf & "¿Desea hacer esto?"
                OK_Button.Text = "Sí"
            End If
        End If
        Text = Label1.Text
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
