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
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            MsgBox("You must agree to this disclaimer notice to use this program.", vbOKOnly + MsgBoxStyle.Information, "Disclaimer notice")
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            MsgBox("Debe aceptar este descargo de responsabilidad para usar este programa.", vbOKOnly + MsgBoxStyle.Information, "Descargo de responsabilidad")
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            MsgBox("Vous devez accepter cet avis de non-responsabilité pour utiliser ce programme.", vbOKOnly + MsgBoxStyle.Information, "Avis de non-responsabilité")
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                MsgBox("You must agree to this disclaimer notice to use this program.", vbOKOnly + MsgBoxStyle.Information, "Disclaimer notice")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                MsgBox("Debe aceptar este descargo de responsabilidad para usar este programa.", vbOKOnly + MsgBoxStyle.Information, "Descargo de responsabilidad")
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                MsgBox("Vous devez accepter cet avis de non-responsabilité pour utiliser ce programme.", vbOKOnly + MsgBoxStyle.Information, "Avis de non-responsabilité")
            End If
        End If
        If DialogResult.OK Then
            MainForm.Notify.Visible = False
            MainForm.SaveSettingsFile()
            End
        End If
    End Sub

    Private Sub DisclaimerPanel_Load(sender As Object, e As EventArgs) Handles Me.Load
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label1.Text = "Disclaimer notice"
            OK_Button.Text = "OK"
            Exit_Button.Text = "Exit"
            TextBox1.Text = "You must only use this tool on a system that you don't use productively." & CrLf & "Microsoft has warned that unsupported systems running Windows 11 might not recieve updates in the future." & CrLf & CrLf & "The modified installation images you create will also work on supported systems, but you can natively install Windows 11 on them, without performing modifications to the installation image." & CrLf & "If you have an unsupported system, don't upgrade it to Windows 11. Instead, you can perform a dual-boot, or use another system (that would be the best option anyway)" & CrLf & CrLf & "This tool MUST NOT be used to pirate Windows images, and the program developer recommends you get Windows legally." & CrLf & "The components used by the program are covered by their license terms. These specify the rules for their use and redistribution." & CrLf & CrLf & "If you agree to this disclaimer notice and want to continue using the software, click OK. Otherwise, click Exit."
            CheckBox1.Text = "Do not show this again"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label1.Text = "Descargo de responsabilidad"
            OK_Button.Text = "Aceptar"
            Exit_Button.Text = "Salir"
            TextBox1.Text = "Usted solo debe utilizar esta herramienta en un sistema que no use productivamente." & CrLf & "Microsoft ha avisado de que sistemas no soportados ejecutando Windows 11 podrían no recibir actualizaciones en el futuro." & CrLf & CrLf & "Las imágenes de instalación modificadas que usted cree también funcionarán en sistemas soportados, pero usted puede instalar Windows 11 de forma nativa en ellos, sin realizar modificaciones a la imagen de instalación." & CrLf & "Si usted tiene un sistema no soportado, no lo actualice a Windows 11. En vez de eso, puede realizar un arranque dual, o usar otro sistema (ésta sería la mejor opción de todas formas)" & CrLf & CrLf & "Esta herramienta NO DEBE ser usada para piratear imágenes de Windows, y el desarrollador del programa le recomienda obtener Windows legalmente." & CrLf & "Los componentes utilizados por el programa están protegidos por sus acuerdos de licencia. Éstos especifican las reglas de su uso y redistribución." & CrLf & CrLf & "Si acepta este descargo de responsabilidad y quiere continuar usando el software, haga clic en Aceptar. En caso contrario, haga clic en Salir."
            CheckBox1.Text = "No mostrar esto de nuevo"
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label1.Text = "Avis de non-responsabilité"
            OK_Button.Text = "OK"
            Exit_Button.Text = "Quitter"
            TextBox1.Text = "Vous ne devez utiliser cet outil que sur un système que vous n'utilisez pas de manière productive." & CrLf & "Microsoft a averti que les systèmes non pris en charge fonctionnant sous Windows 11 pourraient ne pas recevoir de mises à jour à l'avenir." & CrLf & CrLf & "Les images d'installation modifiées que vous créez fonctionneront également sur les systèmes pris en charge, mais vous pourrez y installer Windows 11 en mode natif, sans avoir à modifier l'image d'installation." & CrLf & "Si vous avez un système non pris en charge, ne le mettez pas à niveau vers Windows 11. Au lieu de cela, vous pouvez effectuer un dual-boot, ou utiliser un autre système (ce qui serait de toute façon la meilleure option)." & CrLf & CrLf & "Cet outil NE DOIT PAS être utilisé pour pirater des images Windows, et le développeur du programme vous recommande d'obtenir Windows légalement." & CrLf & "Les composants utilisés par le programme sont couverts par leurs conditions de licence. Ceux-ci spécifient les règles pour leur utilisation et leur redistribution." & CrLf & CrLf & "Si vous acceptez cet avis de non-responsabilité et souhaitez continuer à utiliser le logiciel, cliquez sur OK. Sinon, cliquez sur Quitter."
            CheckBox1.Text = "Ne le montrez plus jamais"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Disclaimer notice"
                OK_Button.Text = "OK"
                Exit_Button.Text = "Exit"
                TextBox1.Text = "You must only use this tool on a system that you don't use productively." & CrLf & "Microsoft has warned that unsupported systems running Windows 11 might not recieve updates in the future." & CrLf & CrLf & "The modified installation images you create will also work on supported systems, but you can natively install Windows 11 on them, without performing modifications to the installation image." & CrLf & "If you have an unsupported system, don't upgrade it to Windows 11. Instead, you can perform a dual-boot, or use another system (that would be the best option anyway)" & CrLf & CrLf & "This tool MUST NOT be used to pirate Windows images, and the program developer recommends you get Windows legally." & CrLf & "The components used by the program are covered by their license terms. These specify the rules for their use and redistribution." & CrLf & CrLf & "If you agree to this disclaimer notice and want to continue using the software, click OK. Otherwise, click Exit."
                CheckBox1.Text = "Do not show this again"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Descargo de responsabilidad"
                OK_Button.Text = "Aceptar"
                Exit_Button.Text = "Salir"
                TextBox1.Text = "Usted solo debe utilizar esta herramienta en un sistema que no use productivamente." & CrLf & "Microsoft ha avisado de que sistemas no soportados ejecutando Windows 11 podrían no recibir actualizaciones en el futuro." & CrLf & CrLf & "Las imágenes de instalación modificadas que usted cree también funcionarán en sistemas soportados, pero usted puede instalar Windows 11 de forma nativa en ellos, sin realizar modificaciones a la imagen de instalación." & CrLf & "Si usted tiene un sistema no soportado, no lo actualice a Windows 11. En vez de eso, puede realizar un arranque dual, o usar otro sistema (ésta sería la mejor opción de todas formas)" & CrLf & CrLf & "Esta herramienta NO DEBE ser usada para piratear imágenes de Windows, y el desarrollador del programa le recomienda obtener Windows legalmente." & CrLf & "Los componentes utilizados por el programa están protegidos por sus acuerdos de licencia. Éstos especifican las reglas de su uso y redistribución." & CrLf & CrLf & "Si acepta este descargo de responsabilidad y quiere continuar usando el software, haga clic en Aceptar. En caso contrario, haga clic en Salir."
                CheckBox1.Text = "No mostrar esto de nuevo"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label1.Text = "Avis de non-responsabilité"
                OK_Button.Text = "OK"
                Exit_Button.Text = "Quitter"
                TextBox1.Text = "Vous ne devez utiliser cet outil que sur un système que vous n'utilisez pas de manière productive." & CrLf & "Microsoft a averti que les systèmes non pris en charge fonctionnant sous Windows 11 pourraient ne pas recevoir de mises à jour à l'avenir." & CrLf & CrLf & "Les images d'installation modifiées que vous créez fonctionneront également sur les systèmes pris en charge, mais vous pourrez y installer Windows 11 en mode natif, sans avoir à modifier l'image d'installation." & CrLf & "Si vous avez un système non pris en charge, ne le mettez pas à niveau vers Windows 11. Au lieu de cela, vous pouvez effectuer un dual-boot, ou utiliser un autre système (ce qui serait de toute façon la meilleure option)." & CrLf & CrLf & "Cet outil NE DOIT PAS être utilisé pour pirater des images Windows, et le développeur du programme vous recommande d'obtenir Windows légalement." & CrLf & "Les composants utilisés par le programme sont couverts par leurs conditions de licence. Ceux-ci spécifient les règles pour leur utilisation et leur redistribution." & CrLf & CrLf & "Si vous acceptez cet avis de non-responsabilité et souhaitez continuer à utiliser le logiciel, cliquez sur OK. Sinon, cliquez sur Quitter."
                CheckBox1.Text = "Ne le montrez plus jamais"
            End If
        End If
        Text = Label1.Text
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.White
            ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            TextBox1.BackColor = Color.White
            TextBox1.ForeColor = Color.Black
            OK_Button.BackColor = Color.FromArgb(1, 92, 186)
            OK_Button.ForeColor = Color.White
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(43, 43, 43)
            ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            TextBox1.BackColor = Color.FromArgb(43, 43, 43)
            TextBox1.ForeColor = Color.White
            OK_Button.BackColor = Color.FromArgb(76, 194, 255)
            OK_Button.ForeColor = Color.Black
        End If
    End Sub
End Class
