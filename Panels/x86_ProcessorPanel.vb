Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars

Public Class x86_ProcessorPanel

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub x86_ProcessorPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            Label1.Text = "Incompatible installer architecture"
            Label2.Text = "The program has detected a 32-bit processor or operating system. The installer will not work on your hardware, because it requires 64-bit hardware. However, the tool will still work." & CrLf & _
                "You may have a 32-bit operating system on 64-bit hardware. In that case, you will need to reinstall Windows." & CrLf & CrLf & _
                "The program knows which architecture your system has and, to indicate a platform incompatibility, a " & Quote & "processor" & Quote & " icon is displayed on the title bar."
            OK_Button.Text = "OK"
            LinkLabel1.Text = "To see which processor architecture is on your system, click here."
            LinkLabel1.LinkArea = New LinkArea(61, 4)
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label1.Text = "Arquitectura del instalador incompatible"
            Label2.Text = "El programa ha detectado un procesador o un sistema operativo de 32 bits. El instalador no funcionará en su hardware, porque requiere un hardware de 64 bits. Aun así, la herramienta seguirá funcionando." & CrLf & _
                "Podría tener un sistema operativo de 32 bits en un hardware de 64 bits. En ese caso, deberá reinstalar Windows." & CrLf & CrLf & _
                "El programa sabe qué arquitectura tiene su sistema y, para indicar una incompatibilidad de plataformas, un icono de un " & Quote & "processor" & Quote & " se muestra en la barra de título."
            OK_Button.Text = "Aceptar"
            LinkLabel1.Text = "Para ver qué arquitectura del procesador hay en su sistema, haga clic aquí."
            LinkLabel1.LinkArea = New LinkArea(70, 4)
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label1.Text = "Architecture de l'installateur incompatible"
            Label2.Text = "Le programme a détecté un processeur ou un système opérationnel de 32 bits. L'installateur ne fonctionnera pas sur votre matériel, car il nécessite un matériel de 64 bits. Toutefois, l'outil fonctionnera quand même." & CrLf & _
                "Il se peut que vous ayez un système opérationnel de 32 bits sur un matériel de 64 bits. Dans ce cas, vous devrez réinstaller Windows." & CrLf & CrLf & _
                "Le programme sait quelle est l'architecture de votre système et, pour indiquer une incompatibilité de plate-forme, une icône d'un " & Quote & "processeur" & Quote & " s'affiche dans la barre de titre."
            OK_Button.Text = "OK"
            LinkLabel1.Text = "Pour savoir quelle architecture de processeur est présente sur votre système, cliquez ici."
            LinkLabel1.LinkArea = New LinkArea(86, 3)
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Incompatible installer architecture"
                Label2.Text = "The program has detected a 32-bit processor or operating system. The installer will not work on your hardware, because it requires 64-bit hardware. However, the tool will still work." & CrLf & _
                    "You may have a 32-bit operating system on 64-bit hardware. In that case, you will need to reinstall Windows." & CrLf & CrLf & _
                    "The program knows which architecture your system has and, to indicate a platform incompatibility, a " & Quote & "processor" & Quote & " icon is displayed on the title bar."
                OK_Button.Text = "OK"
                LinkLabel1.Text = "To see which processor architecture is on your system, click here."
                LinkLabel1.LinkArea = New LinkArea(61, 4)
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Arquitectura del instalador incompatible"
                Label2.Text = "El programa ha detectado un procesador o un sistema operativo de 32 bits. El instalador no funcionará en su hardware, porque requiere un hardware de 64 bits. Aun así, la herramienta seguirá funcionando." & CrLf & _
                    "Podría tener un sistema operativo de 32 bits en un hardware de 64 bits. En ese caso, deberá reinstalar Windows." & CrLf & CrLf & _
                    "El programa sabe qué arquitectura tiene su sistema y, para indicar una incompatibilidad de plataformas, un icono de un " & Quote & "processor" & Quote & " se muestra en la barra de título."
                OK_Button.Text = "Aceptar"
                LinkLabel1.Text = "Para ver qué arquitectura del procesador hay en su sistema, haga clic aquí."
                LinkLabel1.LinkArea = New LinkArea(70, 4)
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label1.Text = "Architecture de l'installateur incompatible"
                Label2.Text = "Le programme a détecté un processeur ou un système opérationnel de 32 bits. L'installateur ne fonctionnera pas sur votre matériel, car il nécessite un matériel de 64 bits. Toutefois, l'outil fonctionnera quand même." & CrLf & _
                    "Il se peut que vous ayez un système opérationnel de 32 bits sur un matériel de 64 bits. Dans ce cas, vous devrez réinstaller Windows." & CrLf & CrLf & _
                    "Le programme sait quelle est l'architecture de votre système et, pour indiquer une incompatibilité de plate-forme, une icône d'un " & Quote & "processeur" & Quote & " s'affiche dans la barre de titre."
                OK_Button.Text = "OK"
                LinkLabel1.Text = "Pour savoir quelle architecture de processeur est présente sur votre système, cliquez ici."
                LinkLabel1.LinkArea = New LinkArea(86, 3)
            End If
        End If
        Text = Label1.Text
        Beep()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If My.Computer.Info.OSFullName.Contains("Windows 7") Or My.Computer.Info.OSFullName.Contains("Windows 8") Then
            Process.Start("control", "system")
        Else
            Process.Start("ms-settings:about")
        End If
    End Sub
End Class
