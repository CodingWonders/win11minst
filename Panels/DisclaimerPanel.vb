Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars

Public Class DisclaimerPanel

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Exit_Button_Click(sender As Object, e As EventArgs) Handles Exit_Button.Click
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            MsgBox("You must agree to this disclaimer notice to use this program.", vbOKOnly + MsgBoxStyle.Information, "Disclaimer notice")
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            MsgBox("Debe aceptar este descargo de responsabilidad para usar este programa.", vbOKOnly + MsgBoxStyle.Information, "Descargo de responsabilidad")
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                MsgBox("You must agree to this disclaimer notice to use this program.", vbOKOnly + MsgBoxStyle.Information, "Disclaimer notice")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                MsgBox("Debe aceptar este descargo de responsabilidad para usar este programa.", vbOKOnly + MsgBoxStyle.Information, "Descargo de responsabilidad")
            End If
        End If

        If DialogResult.OK Then
            MainForm.Notify.Visible = False
            End
        End If
    End Sub

    Private Sub DisclaimerPanel_Load(sender As Object, e As EventArgs) Handles Me.Load
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            Label1.Text = "Disclaimer notice"
            OK_Button.Text = "OK"
            Exit_Button.Text = "Exit"
            TextBox1.Text = "You must only use this tool on a system that you don't use productively." & CrLf & "Microsoft has recently warned that unsupported systems running Windows 11 might not recieve updates in the future." & CrLf & CrLf & "The modified installation images you create will also work on supported systems, but you can natively install Windows 11 on them, without performing modifications to the installation image." & CrLf & "If you have an unsupported system, don't upgrade it to Windows 11. Instead, you can perform a dual-boot, or use another system (that would be the best option anyway)"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            Label1.Text = "Descargo de responsabilidad"
            OK_Button.Text = "Aceptar"
            Exit_Button.Text = "Salir"
            TextBox1.Text = "Usted solo debe utilizar esta herramienta en un sistema que no use productivamente." & CrLf & "Microsoft ha avisado recientemente de que sistemas no soportados ejecutando Windows 11 podrían no recibir actualizaciones en el futuro." & CrLf & CrLf & "Las imágenes de instalación modificadas que usted cree también funcionarán en sistemas soportados, pero usted puede instalar Windows 11 de forma nativa en ellos, sin realizar modificaciones a la imagen de instalación." & CrLf & "Si usted tiene un sistema no soportado, no lo actualice a Windows 11. En vez de eso, puede realizar un arranque dual, o usar otro sistema (ésta sería la mejor opción de todas formas)"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Disclaimer notice"
                OK_Button.Text = "OK"
                Exit_Button.Text = "Exit"
                TextBox1.Text = "You must only use this tool on a system that you don't use productively." & CrLf & "Microsoft has recently warned that unsupported systems running Windows 11 might not recieve updates in the future." & CrLf & CrLf & "The modified installation images you create will also work on supported systems, but you can natively install Windows 11 on them, without performing modifications to the installation image." & CrLf & "If you have an unsupported system, don't upgrade it to Windows 11. Instead, you can perform a dual-boot, or use another system (that would be the best option anyway)"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Descargo de responsabilidad"
                OK_Button.Text = "Aceptar"
                Exit_Button.Text = "Salir"
                TextBox1.Text = "Usted solo debe utilizar esta herramienta en un sistema que no use productivamente." & CrLf & "Microsoft ha avisado recientemente de que sistemas no soportados ejecutando Windows 11 podrían no recibir actualizaciones en el futuro." & CrLf & CrLf & "Las imágenes de instalación modificadas que usted cree también funcionarán en sistemas soportados, pero usted puede instalar Windows 11 de forma nativa en ellos, sin realizar modificaciones a la imagen de instalación." & CrLf & "Si usted tiene un sistema no soportado, no lo actualice a Windows 11. En vez de eso, puede realizar un arranque dual, o usar otro sistema (ésta sería la mejor opción de todas formas)"
            End If
        End If
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            TextBox1.BackColor = Color.White
            TextBox1.ForeColor = Color.Black
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            Me.BackColor = Color.FromArgb(43, 43, 43)
            Me.ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            TextBox1.BackColor = Color.FromArgb(43, 43, 43)
            TextBox1.ForeColor = Color.White
        End If
        Text = Label1.Text
    End Sub
End Class
