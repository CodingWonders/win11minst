﻿Imports System.Windows.Forms

Public Class DebugPanel

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click, Button1.Click
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub DebugPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
            TabPage1.BackColor = Color.White
            TabPage1.ForeColor = Color.Black
            TabPage2.BackColor = Color.White
            TabPage2.ForeColor = Color.Black
            TabPage3.BackColor = Color.White
            TabPage3.ForeColor = Color.Black
            TextBox1.BackColor = Color.White
            TextBox1.ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            Me.BackColor = Color.FromArgb(43, 43, 43)
            Me.ForeColor = Color.White
            TabPage1.BackColor = Color.FromArgb(43, 43, 43)
            TabPage1.ForeColor = Color.Black
            TabPage2.BackColor = Color.FromArgb(43, 43, 43)
            TabPage2.ForeColor = Color.Black
            TabPage3.BackColor = Color.FromArgb(43, 43, 43)
            TabPage3.ForeColor = Color.Black
            TextBox1.BackColor = Color.FromArgb(43, 43, 43)
            TextBox1.ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
        End If
    End Sub
End Class
