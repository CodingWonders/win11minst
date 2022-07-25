Imports Microsoft.VisualBasic.ControlChars
Imports System.IO
Public Class InstProjectReuseDialog
    Private isMouseDown As Boolean = False
    Private mouseOffset As Point

    Private Sub InstProjectReuseDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.FlatStyle = FlatStyle.System
        Button2.FlatStyle = FlatStyle.System
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.FromArgb(243, 243, 243)
            ForeColor = Color.Black
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(32, 32, 32)
            ForeColor = Color.White
        End If
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            Label1.Text = "Reuse source installer files?"
            Label2.Text = "The Windows 11 Manual Installer has detected that some installer files were going to be used to create a custom installer. The program has remembered these files if you want to use them now."
            Label3.Text = "Do you want to continue where you left off, or do you want to start from scratch?"
            Button1.Text = "Reuse installer files"
            Button2.Text = "Continue without reusing"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            Label1.Text = "¿Reutilizar archivos de instaladores de origen?"
            Label2.Text = "El Instalador manual de Windows 11 ha detectado que algunos archivos de instaladores iban a ser utilizados para crear un instalador modificado. El programa los ha recordado si los quiere usar ahora."
            Label3.Text = "¿Desea continuar desde donde lo dejó, o prefiere empezar de cero?"
            Button1.Text = "Reutilizar archivos de instaladores"
            Button2.Text = "Continuar sin reutilizar"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Reuse source installer files?"
                Label2.Text = "The Windows 11 Manual Installer has detected that some installer files were going to be used to create a custom installer. The program has remembered these files if you want to use them now."
                Label3.Text = "Do you want to continue where you left off, or do you want to start from scratch?"
                Button1.Text = "Reuse installer files"
                Button2.Text = "Continue without reusing"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "¿Reutilizar archivos de instaladores de origen?"
                Label2.Text = "El Instalador manual de Windows 11 ha detectado que algunos archivos de instaladores iban a ser utilizados para crear un instalador modificado. El programa los ha recordado si los quiere usar ahora."
                Label3.Text = "¿Desea continuar desde donde lo dejó, o prefiere empezar de cero?"
                Button1.Text = "Reutilizar archivos de instaladores"
                Button2.Text = "Continuar sin reutilizar"
            End If
        End If
        Text = Label1.Text
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Reuse installer files" Or Button1.Text = "Reutilizar instaladores" Then
            If Not File.Exists(My.Computer.FileSystem.ReadAllText(".\Win11Inst.ini")) And Not File.Exists(My.Computer.FileSystem.ReadAllText(".\Win10Inst.ini")) Then
                PictureBox1.Image = New Bitmap(My.Resources.unknown_drive)
                If MainForm.ComboBox4.SelectedItem = "English" Then
                    Button1.Text = "Retry"
                    Label1.Text = "The drive containing the source files is unavailable"
                    Label3.Text = "Please connect the drive containing these files, and click " & Quote & "Retry" & Quote
                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Then
                    Button1.Text = "Reintentar"
                    Label1.Text = "El disco conteniento los archivos de origen no está disponible"
                    Label3.Text = "Por favor, conecte el disco conteniendo esos archivos, y haga clic en " & Quote & "Reintentar" & Quote
                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        Button1.Text = "Retry"
                        Label1.Text = "The drive containing the source files is unavailable"
                        Label3.Text = "Please connect the drive containing these files, and click " & Quote & "Retry" & Quote
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESP" Then
                        Button1.Text = "Reintentar"
                        Label1.Text = "El disco conteniento los archivos de origen no está disponible"
                        Label3.Text = "Por favor, conecte el disco conteniendo esos archivos, y haga clic en " & Quote & "Reintentar" & Quote
                    End If
                End If
            Else
                DialogResult = Windows.Forms.DialogResult.OK
                MainForm.TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\Win11Inst.ini")
                MainForm.TextBox2.Text = My.Computer.FileSystem.ReadAllText(".\Win10Inst.ini")
                MainForm.TextBox3.Text = My.Computer.FileSystem.ReadAllText(".\InstName.ini")
                MainForm.TextBox4.Text = My.Computer.FileSystem.ReadAllText(".\TargetInstaller.ini")
                MainForm.Label67.Text = MainForm.TextBox1.Text
                MainForm.Label68.Text = MainForm.TextBox2.Text
                If MainForm.TextBox4.Text.EndsWith("\") Then
                    MainForm.Label90.Text = MainForm.TextBox4.Text.TrimEnd("\") & "\" & MainForm.TextBox3.Text & ".iso"
                Else
                    MainForm.Label90.Text = MainForm.TextBox4.Text & "\" & MainForm.TextBox3.Text & ".iso"
                End If
                MainForm.LinkLabel2.Visible = True
                MainForm.Label3.Visible = True
                MainForm.PictureBox5.Visible = True
                Me.Close()
            End If
            Text = Label1.Text
        ElseIf Button1.Text = "Retry" Or Button1.Text = "Reintentar" Then
            If Not File.Exists(My.Computer.FileSystem.ReadAllText(".\Win11Inst.ini")) And Not File.Exists(My.Computer.FileSystem.ReadAllText(".\Win10Inst.ini")) Then
                PictureBox1.Image = New Bitmap(My.Resources.unknown_drive)
                If MainForm.ComboBox4.SelectedItem = "English" Then
                    Button1.Text = "Retry"
                    Label1.Text = "The drive containing the source files is unavailable"
                    Label3.Text = "Please connect the drive containing these files, and click " & Quote & "Retry" & Quote
                ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Then
                    Button1.Text = "Reintentar"
                    Label1.Text = "El disco conteniento los archivos de origen no está disponible"
                    Label3.Text = "Por favor, conecte el disco conteniendo esos archivos, y haga clic en " & Quote & "Reintentar" & Quote
                ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Then
                    If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                        Button1.Text = "Retry"
                        Label1.Text = "The drive containing the source files is unavailable"
                        Label3.Text = "Please connect the drive containing these files, and click " & Quote & "Retry" & Quote
                    ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESP" Then
                        Button1.Text = "Reintentar"
                        Label1.Text = "El disco conteniento los archivos de origen no está disponible"
                        Label3.Text = "Por favor, conecte el disco conteniendo esos archivos, y haga clic en " & Quote & "Reintentar" & Quote
                    End If
                End If
            Else
                DialogResult = Windows.Forms.DialogResult.OK
                MainForm.TextBox1.Text = My.Computer.FileSystem.ReadAllText(".\Win11Inst.ini")
                MainForm.TextBox2.Text = My.Computer.FileSystem.ReadAllText(".\Win10Inst.ini")
                MainForm.TextBox3.Text = My.Computer.FileSystem.ReadAllText(".\InstName.ini")
                MainForm.TextBox4.Text = My.Computer.FileSystem.ReadAllText(".\TargetInstaller.ini")
                MainForm.Label67.Text = MainForm.TextBox1.Text
                MainForm.Label68.Text = MainForm.TextBox2.Text
                If MainForm.TextBox4.Text.EndsWith("\") Then
                    MainForm.Label90.Text = MainForm.TextBox4.Text.TrimEnd("\") & "\" & MainForm.TextBox3.Text & ".iso"
                Else
                    MainForm.Label90.Text = MainForm.TextBox4.Text & "\" & MainForm.TextBox3.Text & ".iso"
                End If
                MainForm.LinkLabel2.Visible = True
                MainForm.Label3.Visible = True
                MainForm.PictureBox5.Visible = True
                Me.Close()
            End If
            Text = Label1.Text
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub InstProjectReuseDialog_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ' Get the new position
            mouseOffset = New Point(-e.X, -e.Y)
            ' Set that left button is pressed
            isMouseDown = True
        End If
    End Sub

    Private Sub InstProjectReuseDialog_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If isMouseDown Then
            Dim mousePos As Point = Control.MousePosition
            ' Get the new form position
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Location = mousePos
        End If
    End Sub

    Private Sub InstProjectReuseDialog_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then
            isMouseDown = False
        End If
    End Sub
End Class