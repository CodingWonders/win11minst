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
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label1.Text = "Reset preferences?"
            Label2.Text = "This will reset ALL preferences to their default values (e.g., language or color mode)"
            Yes_Button.Text = "Yes"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label1.Text = "¿Restablecer preferencias?"
            Label2.Text = "Esto restablecerá TODAS las preferencias a sus valores predeterminados (p.ej., el idioma o el modo de color)"
            Yes_Button.Text = "Sí"
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label1.Text = "Réinitialiser les préférences ?"
            Label2.Text = "Cela réinitialisera TOUTES les préférences à leurs valeurs par défaut (par exemple, la langue ou le mode de couleur)."
            Yes_Button.Text = "Oui"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Reset preferences?"
                Label2.Text = "This will reset ALL preferences to their default values (e.g., language or color mode)"
                Yes_Button.Text = "Yes"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "¿Restablecer preferencias?"
                Label2.Text = "Esto restablecerá TODAS las preferencias a sus valores predeterminados (p.ej., el idioma o el modo de color)"
                Yes_Button.Text = "Sí"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label1.Text = "Réinitialiser les préférences ?"
                Label2.Text = "Cela réinitialisera TOUTES les préférences à leurs valeurs par défaut (par exemple, la langue ou le mode de couleur)."
                Yes_Button.Text = "Oui"
            End If
        End If
        Text = Label1.Text
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.White
            ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            No_Button.BackColor = Color.FromArgb(1, 92, 186)
            No_Button.ForeColor = Color.White
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(43, 43, 43)
            ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            No_Button.BackColor = Color.FromArgb(76, 194, 255)
            No_Button.ForeColor = Color.Black
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
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label3.Text = "Resetting preferences. Please wait..."
            No_Button.Text = "OK"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label3.Text = "Restableciendo preferencias. Por favor, espere..."
            No_Button.Text = "Aceptar"
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label3.Text = "Réinitialisation des paramètres en cours. Veuillez patienter..."
            No_Button.Text = "OK"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label3.Text = "Resetting preferences. Please wait..."
                No_Button.Text = "OK"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label3.Text = "Restableciendo preferencias. Por favor, espere..."
                No_Button.Text = "Aceptar"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label3.Text = "Réinitialisation des paramètres en cours. Veuillez patienter..."
                No_Button.Text = "OK"
            End If
        End If
        ProgressBar1.Value = 0
        ProgressBar1.Visible = True
        Label3.Visible = True
        MainForm.ResetSettings()
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label3.Text = "The preferences have been reset successfully."
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label3.Text = "Las preferencias han sido restablecidas satisfactoriamente."
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label3.Text = "Les paramètres ont été réinitialisés avec succès."
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label3.Text = "The preferences have been reset successfully."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label3.Text = "Las preferencias han sido restablecidas satisfactoriamente."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label3.Text = "Les paramètres ont été réinitialisés avec succès."
            End If
        End If
    End Sub
End Class
