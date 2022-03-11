Imports System.Windows.Forms

Public Class UpdateCheckPreLoadPanel
    Private isMouseDown As Boolean = False
    Private mouseOffset As Point
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub UpdateCancelButton_Click(sender As Object, e As EventArgs) Handles UpdateCancelButton.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub UpdateCheckPreLoadPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Computer.Info.OSFullName.Contains("Windows 7") Or My.Computer.Info.OSFullName.Contains("Windows 8") Or My.Computer.Info.OSFullName.Contains("Windows 10") Then
            UpdateCancelButton.FlatStyle = FlatStyle.System
            Button1.FlatStyle = FlatStyle.System
            Button2.FlatStyle = FlatStyle.System
        End If
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.FromArgb(243, 243, 243)
            ForeColor = Color.Black
            GroupBox1.ForeColor = Color.Black
            closeBox.Image = New Bitmap(My.Resources.closebox)
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(32, 32, 32)
            ForeColor = Color.White
            GroupBox1.ForeColor = Color.White
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
        End If
        CenterToScreen()
        Visible = True
        ' ACTIONS
        If My.Computer.Network.IsAvailable = False Then
            Me.Close()
        Else
            If System.IO.File.Exists(".\latest") Then
                TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\version")
                TextBox2.Text = My.Computer.FileSystem.ReadAllText(".\latest")
                If TextBox1.Text = TextBox2.Text Then
                    Me.Close()
                Else
                    Height = 318
                    CenterToScreen()
                    ProgressRingPic.Visible = False
                    Label1.Left = 12
                    Text = "Passive Update Check System - Updates available"
                    Label1.Text = "A new version is available. Do you want to install it now?"
                    UpdateCancelButton.Visible = False
                    closeBox.Visible = True
                End If
            Else
                Me.Close()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, Button2.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub UpdateCheckPreLoadPanel_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ' Get the new position
            mouseOffset = New Point(-e.X, -e.Y)
            ' Set that left button is pressed
            isMouseDown = True
        End If
    End Sub

    Private Sub UpdateCheckPreLoadPanel_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If isMouseDown Then
            Dim mousePos As Point = Control.MousePosition
            ' Get the new form position
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Location = mousePos
        End If
    End Sub

    Private Sub UpdateCheckPreLoadPanel_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            isMouseDown = False
        End If
    End Sub

    Private Sub closeBox_Click(sender As Object, e As EventArgs) Handles closeBox.Click
        MainForm.Notify.Visible = False
        End
    End Sub

    Private Sub closeBox_MouseEnter(sender As Object, e As EventArgs) Handles closeBox.MouseEnter
        closeBox.Image = New Bitmap(My.Resources.closebox_focus)
    End Sub

    Private Sub closeBox_MouseLeave(sender As Object, e As EventArgs) Handles closeBox.MouseLeave
        If BackColor = Color.FromArgb(243, 243, 243) Then
            closeBox.Image = New Bitmap(My.Resources.closebox)
        Else
            closeBox.Image = New Bitmap(My.Resources.closebox_dark)
        End If
    End Sub

    Private Sub closeBox_MouseHover(sender As Object, e As EventArgs) Handles closeBox.MouseHover
        closeToolTip.SetToolTip(closeBox, "Exit")
    End Sub

    Private Sub closeBox_MouseDown(sender As Object, e As MouseEventArgs) Handles closeBox.MouseDown
        If BackColor = Color.FromArgb(243, 243, 243) Then
            closeBox.Image = New Bitmap(My.Resources.closebox_down)
        Else
            closeBox.Image = New Bitmap(My.Resources.closebox_dark_down)
        End If
    End Sub

    Private Sub closeBox_MouseUp(sender As Object, e As MouseEventArgs) Handles closeBox.MouseUp
        closeBox.Image = New Bitmap(My.Resources.closebox_focus)
    End Sub
End Class
