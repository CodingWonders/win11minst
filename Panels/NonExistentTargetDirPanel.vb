Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars

Public Class NonExistentTargetDirPanel

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

    Private Sub Retry_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Retry_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Retry
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub NonExistentTargetDirPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label1.Text = "Non-existent target directory"
            Label2.Text = "The directory: " & Quote & MainForm.TextBox4.Text & Quote & " does not exist. The drive containing it may be unavailable or the directory may not have been created yet. In cases like this, by default, the program will use the user directory. What do you want to do?"
            OK_Button.Text = "Use the user directory"
            Cancel_Button.Text = "Cancel"
            Retry_Button.Text = "Specify another directory"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label1.Text = "Directorio de destino inexistente"
            Label2.Text = "El directorio: " & Quote & MainForm.TextBox4.Text & Quote & " no existe. Quizá el disco que lo contiene no esté disponible o el directorio no haya sido creado todavía. En casos como este, por defecto, el programa utilizará el directorio de usuario. ¿Qué desea hacer?"
            OK_Button.Text = "Utilizar el directorio de usuario"
            Cancel_Button.Text = "Cancelar"
            Retry_Button.Text = "Especificar otro directorio"
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label1.Text = "Répertoire cible non existant"
            Label2.Text = "Le répertoire : " & Quote & MainForm.TextBox4.Text & Quote & " n'existe pas. Le disque qui le contient est peut-être indisponible ou le répertoire n'a peut-être pas encore été créé. Dans des cas comme celui-ci, le programme utilisera par défaut le répertoire de l'utilisateur. Que voulez-vous faire ?"
            OK_Button.Text = "Utiliser le répertoire de l'utilisateur"
            Cancel_Button.Text = "Annuler"
            Retry_Button.Text = "Spécifier un autre répertoire"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Non-existent target directory"
                Label2.Text = "The directory: " & Quote & MainForm.TextBox4.Text & Quote & " does not exist. The drive containing it may be unavailable or the directory may not have been created yet. In cases like this, by default, the program will use the user directory. What do you want to do?"
                OK_Button.Text = "Use the user directory"
                Cancel_Button.Text = "Cancel"
                Retry_Button.Text = "Specify another directory"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Directorio de destino inexistente"
                Label2.Text = "El directorio: " & Quote & MainForm.TextBox4.Text & Quote & " no existe. Quizá el disco que lo contiene no esté disponible o el directorio no haya sido creado todavía. En casos como este, por defecto, el programa utilizará el directorio de usuario. ¿Qué desea hacer?"
                OK_Button.Text = "Utilizar el directorio de usuario"
                Cancel_Button.Text = "Cancelar"
                Retry_Button.Text = "Especificar otro directorio"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label1.Text = "Répertoire cible non existant"
                Label2.Text = "Le répertoire : " & Quote & MainForm.TextBox4.Text & Quote & " n'existe pas. Le disque qui le contient est peut-être indisponible ou le répertoire n'a peut-être pas encore été créé. Dans des cas comme celui-ci, le programme utilisera par défaut le répertoire de l'utilisateur. Que voulez-vous faire ?"
                OK_Button.Text = "Utiliser le répertoire de l'utilisateur"
                Cancel_Button.Text = "Annuler"
                Retry_Button.Text = "Spécifier un autre répertoire"
            End If
        End If
        Text = Label1.Text
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.White
            ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            OK_Button.BackColor = Color.FromArgb(1, 92, 186)
            OK_Button.ForeColor = Color.White
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(43, 43, 43)
            ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            OK_Button.BackColor = Color.FromArgb(76, 194, 255)
            OK_Button.ForeColor = Color.Black
        End If
        Beep()
    End Sub
End Class
