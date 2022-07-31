Imports System.Threading
Public Class BackSubPanel

    Dim opacityFade As Single

    Private Sub BackSubPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.isHidden = True Then
            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
                MainForm.Notify.ShowBalloonTip(15, "The program is requesting your interaction", "Please respond to any question shown to continue installer creation", ToolTipIcon.Info)
            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
                MainForm.Notify.ShowBalloonTip(15, "El programa está solicitando su interacción", "Por favor, responda a cualquier pregunta mostrada para continuar la creación del instalador", ToolTipIcon.Info)
            ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
                MainForm.Notify.ShowBalloonTip(15, "Le programme demande votre interaction", "Veuillez répondre aux questions affichées pour poursuivre la création de l'installateur.", ToolTipIcon.Info)
            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    MainForm.Notify.ShowBalloonTip(15, "The program is requesting your interaction", "Please respond to any question shown to continue installer creation", ToolTipIcon.Info)
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    MainForm.Notify.ShowBalloonTip(15, "El programa está solicitando su interacción", "Por favor, responda a cualquier pregunta mostrada para continuar la creación del instalador", ToolTipIcon.Info)
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    MainForm.Notify.ShowBalloonTip(15, "Le programme demande votre interaction", "Veuillez répondre aux questions affichées pour poursuivre la création de l'installateur.", ToolTipIcon.Info)
                End If
            End If
            MainForm.Activate()
            MainForm.isHidden = False
            MainForm.ShowInTaskbar = True
            MiniModeDialog.Hide()
            If MainForm.WasMaximized = True Then
                MainForm.WindowState = FormWindowState.Maximized
            Else
                MainForm.WindowState = FormWindowState.Normal
            End If
            If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
                MainForm.closeBox.Image = New Bitmap(My.Resources.closebox)
            Else
                MainForm.closeBox.Image = New Bitmap(My.Resources.closebox_dark)
            End If
        End If
        Me.Size = MainForm.Size
        Me.Location = MainForm.Location
        If MainForm.CheckBox5.Checked = True Then
            FadeInTimer.Enabled = False
            FadeInTimer.Enabled = True
        Else
            Opacity = 0.5
        End If
    End Sub

    Private Sub FadeInTimer_Tick(sender As Object, e As EventArgs) Handles FadeInTimer.Tick
        Opacity = 0
        For Me.opacityFade = 0 To 0.5 Step 0.05
            Opacity = opacityFade
            Refresh()
            Thread.Sleep(10)
        Next opacityFade
        Opacity = 0.5
        FadeInTimer.Stop()
        FadeInTimer.Enabled = False
    End Sub

    Private Sub FadeOutTimer_Tick(sender As Object, e As EventArgs) Handles FadeOutTimer.Tick
        Opacity -= 0.05
        If Opacity = 0 Then
            Dispose()
        End If
    End Sub

    Private Sub BackSubPanel_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If MainForm.CheckBox5.Checked = True Then
            FadeOutTimer.Enabled = True
            e.Cancel = True
        Else
            Exit Sub
        End If
    End Sub
End Class