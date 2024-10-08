﻿Imports System.Windows.Forms

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
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label1.Text = "Cancel installer creation?"
            Label2.Text = "Are you sure you want to cancel the installer creation process? This will delete any file modifications done at this time."
            Yes_Button.Text = "Yes"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label1.Text = "¿Cancelar la creación del instalador?"
            Label2.Text = "¿Está seguro de que quiere cancelar el proceso de creación del instalador? Esto borrará todas las modificaciones de archivos realizados en este momento."
            Yes_Button.Text = "Sí"
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label1.Text = "Annuler la création de l'installateur ?"
            Label2.Text = "Êtes-vous sûr de vouloir annuler le processus de création de l'installateur ? Cela supprimera toute modification de fichier effectuée à ce moment-là."
            Yes_Button.Text = "Oui"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Cancel installer creation?"
                Label2.Text = "Are you sure you want to cancel the installer creation process? This will delete any file modifications done at this time."
                Yes_Button.Text = "Yes"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "¿Cancelar la creación del instalador?"
                Label2.Text = "¿Está seguro de que quiere cancelar el proceso de creación del instalador? Esto borrará todas las modificaciones de archivos realizados en este momento."
                Yes_Button.Text = "Sí"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label1.Text = "Annuler la création de l'installateur ?"
                Label2.Text = "Êtes-vous sûr de vouloir annuler le processus de création de l'installateur ? Cela supprimera toute modification de fichier effectuée à ce moment-là."
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
            No_Button.BackColor = Color.FromArgb(72, 194, 255)
            No_Button.ForeColor = Color.Black
        End If
    End Sub
End Class
