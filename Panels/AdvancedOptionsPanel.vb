﻿Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars

Public Class AdvancedOptionsPanel

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If CheckBox1.Checked = True Then
            MainForm.UseBypassNRO = True
        Else
            MainForm.UseBypassNRO = False
        End If
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
        CheckBox1.Enabled = My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator)
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            CheckBox1.Text = "Bypass Microsoft Account sign-in and forced Internet connection setup (22557+)"
            Label1.Text = "Advanced options"
            Label2.Text = "Bypasses Microsoft Account sign-in and forced Internet connection setup on Windows 11 Pro (Nickel builds 22557 onwards)"
            OK_Button.Text = "OK"
            Cancel_Button.Text = "Cancel"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            CheckBox1.Text = "Omitir inicio de sesión con la cuenta de Microsoft y configuración forzada de Internet (22557+)"
            Label1.Text = "Opciones avanzadas"
            Label2.Text = "Omite el inicio de sesión con la cuenta de Microsoft y la configuración forzada de Internet en Windows 11 Pro (compilaciones de Nickel 22557 en adelante)"
            OK_Button.Text = "Aceptar"
            Cancel_Button.Text = "Cancelar"
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            CheckBox1.Text = "Contourner l'ouverture de session du compte Microsoft et la configuration forcée de la connexion Internet (22557+)"
            Label1.Text = "Options avancées"
            Label2.Text = "Contournement de l'ouverture de session du compte Microsoft et de la configuration forcée de la connexion Internet sur Windows 11 Pro (Nickel builds 22557 et suivants)"
            OK_Button.Text = "OK"
            Cancel_Button.Text = "Annuler"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                CheckBox1.Text = "Bypass Microsoft Account sign-in and forced Internet connection setup (22557+)"
                Label1.Text = "Advanced options"
                Label2.Text = "Bypasses Microsoft Account sign-in and forced Internet connection setup on Windows 11 Pro (Nickel builds 22557 onwards)"
                OK_Button.Text = "OK"
                Cancel_Button.Text = "Cancel"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                CheckBox1.Text = "Omitir inicio de sesión con la cuenta de Microsoft y configuración forzada de Internet (22557+)"
                Label1.Text = "Opciones avanzadas"
                Label2.Text = "Omite el inicio de sesión con la cuenta de Microsoft y la configuración forzada de Internet en Windows 11 Pro (compilaciones de Nickel 22557 en adelante)"
                OK_Button.Text = "Aceptar"
                Cancel_Button.Text = "Cancelar"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                CheckBox1.Text = "Contourner l'ouverture de session du compte Microsoft et la configuration forcée de la connexion Internet (22557+)"
                Label1.Text = "Options avancées"
                Label2.Text = "Contournement de l'ouverture de session du compte Microsoft et de la configuration forcée de la connexion Internet sur Windows 11 Pro (Nickel builds 22557 et suivants)"
                OK_Button.Text = "OK"
                Cancel_Button.Text = "Annuler"
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
    End Sub
End Class
