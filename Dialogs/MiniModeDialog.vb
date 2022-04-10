Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars

Public Class MiniModeDialog
    Dim HideWnd As Boolean
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub MiniModeDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If HideWnd = True Then
            Hide()
        End If
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            Text = "System Tray"
            Label1.Text = "The program was hidden in the system tray. This means the program is still running, but in the background." & CrLf & _
                "To bring the program back, you can:" & CrLf & _
                "- Click the message that shows up when you hide it to the system tray" & CrLf & _
                "- Double-click the system tray icon" & CrLf & _
                "- Right-click the system tray icon and click " & Quote & "Open" & Quote & CrLf & _
                "If you want to close the program instead of hiding it to the system tray:" & CrLf & _
                "- Right-click the system tray icon and click " & Quote & "Exit" & Quote & CrLf & _
                "or" & CrLf & _
                "- Go to Settings > Functionality and untick " & Quote & "When closing, hide in system tray" & Quote
            CheckBox1.Text = "Do not show this again"
            OK_Button.Text = "OK"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            Text = "Bandeja del sistema"
            Label1.Text = "El programa se ha ocultado en la bandeja del sistema. Esto quiere decir que el programa sigue ejecutándose, pero en segundo plano." & CrLf & _
                "Para traer al programa de vuelta, puede:" & CrLf & _
                "- Hacer clic en el mensaje que aparece al ocultar el programa en la bandeja del sistema" & CrLf & _
                "- Hacer doble clic en el icono de la bandeja del sistema" & CrLf & _
                "- Hacer clic derecho en el icono de la bandeja del sistema y haciendo clic en " & Quote & "Abrir" & Quote & CrLf & _
                "Si desea cerrar el programa en vez de ocultarlo en la bandeja del sistema:" & CrLf & _
                "- Haz clic derecho en el icono de la bandeja del sistema y haz clic en " & Quote & "Salir" & Quote & CrLf & _
                "o" & CrLf & _
                "- Ve a Configuración > Funcionalidad y desmarque " & Quote & "Al cerrarse, ocultar en la bandeja del sistema" & Quote
            CheckBox1.Text = "No mostrar esto de nuevo"
            OK_Button.Text = "Aceptar"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Text = "System Tray"
                Label1.Text = "The program was hidden in the system tray. This means the program is still running, but in the background." & CrLf & _
                    "To bring the program back, you can:" & CrLf & _
                    "- Click the message that shows up when you hide it to the system tray" & CrLf & _
                    "- Double-click the system tray icon" & CrLf & _
                    "- Right-click the system tray icon and click " & Quote & "Open" & Quote & CrLf & _
                    "If you want to close the program instead of hiding it to the system tray:" & CrLf & _
                    "- Right-click the system tray icon and click " & Quote & "Exit" & Quote & CrLf & _
                    "or" & CrLf & _
                    "- Go to Settings > Functionality and untick " & Quote & "When closing, hide in system tray" & Quote
                CheckBox1.Text = "Do not show this again"
                OK_Button.Text = "OK"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Text = "Bandeja del sistema"
                Label1.Text = "El programa se ha ocultado en la bandeja del sistema. Esto quiere decir que el programa sigue ejecutándose, pero en segundo plano." & CrLf & _
                    "Para traer al programa de vuelta, puede:" & CrLf & _
                    "- Hacer clic en el mensaje que aparece al ocultar el programa en la bandeja del sistema" & CrLf & _
                    "- Hacer doble clic en el icono de la bandeja del sistema" & CrLf & _
                    "- Hacer clic derecho en el icono de la bandeja del sistema y haciendo clic en " & Quote & "Abrir" & Quote & CrLf & _
                    "Si desea cerrar el programa en vez de ocultarlo en la bandeja del sistema:" & CrLf & _
                    "- Haz clic derecho en el icono de la bandeja del sistema y haz clic en " & Quote & "Salir" & Quote & CrLf & _
                    "o" & CrLf & _
                    "- Ve a Configuración > Funcionalidad y desmarque " & Quote & "Al cerrarse, ocultar en la bandeja del sistema" & Quote
                CheckBox1.Text = "No mostrar esto de nuevo"
                OK_Button.Text = "Aceptar"
            End If
        End If
        tbPic.Parent = backPic
        PictureBox2.Parent = backPic
        Label1.Parent = backPic
        CheckBox1.Parent = backPic
        OK_Button.Parent = backPic
        tbPic.BackColor = Color.Transparent
        PictureBox2.BackColor = Color.Transparent
        Label1.BackColor = Color.Transparent
        CheckBox1.BackColor = Color.Transparent
        OK_Button.BackColor = Color.Transparent
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            backPic.Image = New Bitmap(My.Resources.Bloom_Light)
            tbPic.Image = New Bitmap(My.Resources.tb_White)
            CheckBox1.ForeColor = Color.Black
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            backPic.Image = New Bitmap(My.Resources.Bloom_Dark)
            tbPic.Image = New Bitmap(My.Resources.tb_Black)
            CheckBox1.ForeColor = Color.White
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            HideWnd = True
        Else
            HideWnd = False
        End If
    End Sub
End Class
