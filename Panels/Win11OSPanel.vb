Imports System.Windows.Forms
Imports System.IO

Public Class Win11OSPanel

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub Win11OSPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.White
            ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            OK_Button.BackColor = Color.FromArgb(1, 92, 186)
            OK_Button.ForeColor = Color.White
            LinkLabel1.LinkColor = Color.FromArgb(1, 92, 186)
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(43, 43, 43)
            ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            OK_Button.BackColor = Color.FromArgb(76, 194, 255)
            OK_Button.ForeColor = Color.Black
            LinkLabel1.LinkColor = Color.FromArgb(76, 194, 255)
        End If
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label1.Text = "This system is already running Windows 11"
            Label2.Text = "The program has detected that your computer (or device) is already running Windows 11. You should not use custom installers to install or upgrade to Windows 11 on your system, but may use them to install or upgrade to Windows 11 on unsupported systems."
            OK_Button.Text = "OK"
            LinkLabel1.Text = "To view more detailed operating system information, click here."
            LinkLabel1.LinkArea = New LinkArea(58, 4)
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label1.Text = "Este sistema ya está ejecutando Windows 11"
            Label2.Text = "El programa ha detectado que su ordenador (o dispositivo) ya está ejecutando Windows 11. Usted no debería utilizar instaladores modificados para instalar o actualizar a Windows 11 en su sistema, pero puede utilizarlos para instalar o actualizar a Windows 11 en sistemas no soportados."
            OK_Button.Text = "Aceptar"
            LinkLabel1.Text = "Para ver información más detallada del sistema operativo, haga clic aquí."
            LinkLabel1.LinkArea = New LinkArea(68, 4)
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label1.Text = "Ce système fonctionne déjà sous Windows 11"
            Label2.Text = "Le programme a détecté que votre ordinateur (ou périphérique) exécute déjà Windows 11. Vous ne devez pas utiliser les installateurs personnalisés pour installer ou mettre à niveau Windows 11 sur votre système, mais vous pouvez les utiliser pour installer ou mettre à niveau Windows 11 sur des systèmes non pris en charge."
            OK_Button.Text = "OK"
            LinkLabel1.Text = "Pour afficher des informations plus détaillées sur le système opérationnel, cliquez ici."
            LinkLabel1.LinkArea = New LinkArea(84, 3)
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "This system is already running Windows 11"
                Label2.Text = "The program has detected that your computer (or device) is already running Windows 11. You should not use custom installers to install or upgrade to Windows 11 on your system, but may use them to install or upgrade to Windows 11 on unsupported systems."
                OK_Button.Text = "OK"
                LinkLabel1.Text = "To view more detailed operating system information, click here."
                LinkLabel1.LinkArea = New LinkArea(58, 4)
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Este sistema ya está ejecutando Windows 11"
                Label2.Text = "El programa ha detectado que su ordenador (o dispositivo) ya está ejecutando Windows 11. Usted no debería utilizar instaladores modificados para instalar o actualizar a Windows 11 en su sistema, pero puede utilizarlos para instalar o actualizar a Windows 11 en sistemas no soportados."
                OK_Button.Text = "Aceptar"
                LinkLabel1.Text = "Para ver información más detallada del sistema operativo, haga clic aquí."
                LinkLabel1.LinkArea = New LinkArea(68, 4)
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label1.Text = "Ce système fonctionne déjà sous Windows 11"
                Label2.Text = "Le programme a détecté que votre ordinateur (ou périphérique) exécute déjà Windows 11. Vous ne devez pas utiliser les installateurs personnalisés pour installer ou mettre à niveau Windows 11 sur votre système, mais vous pouvez les utiliser pour installer ou mettre à niveau Windows 11 sur des systèmes non pris en charge."
                OK_Button.Text = "OK"
                LinkLabel1.Text = "Pour afficher des informations plus détaillées sur le système opérationnel, cliquez ici."
                LinkLabel1.LinkArea = New LinkArea(84, 3)
            End If
        End If
        Text = Label1.Text
        Beep()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Process.Start("ms-settings:about")
        Catch ex As Exception
            Process.Start(Path.GetPathRoot(Environment.SpecialFolder.UserProfile) & "\Windows\system32\msinfo32.exe")
        End Try
    End Sub
End Class
