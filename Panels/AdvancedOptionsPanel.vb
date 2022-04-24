Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars

Public Class AdvancedOptionsPanel

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
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
        ' This was a condition, but when I opened this solution in VS2022, IntelliCode suggested this.
        ' It's more efficient, and it works!
        CheckBox1.Enabled = My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator)
        CheckBox2.Enabled = My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator)
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            CheckBox1.Text = "Bypass Microsoft Account sign-in and forced Internet connection setup (22557+)"
            CheckBox2.Text = "Hide " & Quote & "System requirements not met" & Quote & " watermark (22557+)"
            Label1.Text = "Advanced options"
            Label2.Text = "Bypasses Microsoft Account sign-in and forced Internet connection setup on Windows 11 Pro (Nickel builds 22557 onwards)"
            Label3.Text = "Note: the program must be run with administrative privileges"
            Label4.Text = "Hides the " & Quote & "System requirements not met" & Quote & " watermark on Nickel builds 22557 onwards and Windows Server" & Quote & "Copper" & Quote & " builds 25057 onwards"
            Label5.Text = "Enabling this option is not recommended yet, as it doesn't work as intended."
            LinkLabel1.Text = "Read the full issue and a possible workaround"
            OK_Button.Text = "OK"
            Cancel_Button.Text = "Cancel"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            CheckBox1.Text = "Omitir inicio de sesión con la cuenta de Microsoft y configuración forzada de Internet (22557+)"
            CheckBox2.Text = "Ocultar la marca de agua " & Quote & "Requisitos de sistema no cumplidos" & Quote & " (22557+)"
            Label1.Text = "Opciones avanzadas"
            Label2.Text = "Omite el inicio de sesión con la cuenta de Microsoft y la configuración forzada de Internet en Windows 11 Pro (compilaciones de Nickel 22557 en adelante)"
            Label3.Text = "Nota: el programa debe ejecutarse con privilegios administrativos"
            Label4.Text = "Oculta la marca de agua " & Quote & "Requisitos de sistema no cumplidos" & Quote & " en compilaciones de Nickel 22557 en adelante y en compilaciones Windows Server " & Quote & "Copper" & Quote & " 25057 en adelante"
            Label5.Text = "Todavía no es recomendable habilitar esta opción, ya que no funciona como se esperaba."
            LinkLabel1.Text = "Lea la publicación completa y una posible solución"
            OK_Button.Text = "Aceptar"
            Cancel_Button.Text = "Cancelar"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                CheckBox1.Text = "Bypass Microsoft Account sign-in and forced Internet connection setup (22557+)"
                CheckBox2.Text = "Hide " & Quote & "System requirements not met" & Quote & " watermark (22557+)"
                Label1.Text = "Advanced options"
                Label2.Text = "Bypasses Microsoft Account sign-in and forced Internet connection setup on Windows 11 Pro (Nickel builds 22557 onwards)"
                Label3.Text = "Note: the program must be run with administrative privileges"
                Label4.Text = "Hides the " & Quote & "System requirements not met" & Quote & " watermark on Nickel builds 22557 onwards and Windows Server" & Quote & "Copper" & Quote & " builds 25057 onwards"
                Label5.Text = "Enabling this option is not recommended yet, as it doesn't work as intended."
                LinkLabel1.Text = "Read the full issue and a possible workaround"
                OK_Button.Text = "OK"
                Cancel_Button.Text = "Cancel"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                CheckBox1.Text = "Omitir inicio de sesión con la cuenta de Microsoft y configuración forzada de Internet (22557+)"
                CheckBox2.Text = "Ocultar la marca de agua " & Quote & "Requisitos de sistema no cumplidos" & Quote & " (22557+)"
                Label1.Text = "Opciones avanzadas"
                Label2.Text = "Omite el inicio de sesión con la cuenta de Microsoft y la configuración forzada de Internet en Windows 11 Pro (compilaciones de Nickel 22557 en adelante)"
                Label3.Text = "Nota: el programa debe ejecutarse con privilegios administrativos"
                Label4.Text = "Oculta la marca de agua " & Quote & "Requisitos de sistema no cumplidos" & Quote & " en compilaciones de Nickel 22557 en adelante y en compilaciones Windows Server " & Quote & "Copper" & Quote & " 25057 en adelante"
                Label5.Text = "Todavía no es recomendable habilitar esta opción, ya que no funciona como se esperaba."
                LinkLabel1.Text = "Lea la publicación completa y una posible solución"
                OK_Button.Text = "Aceptar"
                Cancel_Button.Text = "Cancelar"
            End If
        End If
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            LinkLabel1.LinkColor = Color.FromArgb(1, 92, 186)
            OK_Button.BackColor = Color.FromArgb(1, 92, 186)
            OK_Button.ForeColor = Color.White
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            Me.BackColor = Color.FromArgb(43, 43, 43)
            Me.ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            LinkLabel1.LinkColor = Color.FromArgb(76, 194, 255)
            OK_Button.BackColor = Color.FromArgb(76, 194, 255)
            OK_Button.ForeColor = Color.Black
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            MsgBox("This option is experimental, and it doesn't work as intended. Please expect this to work in a future update.", vbOKOnly + vbInformation, "Experimental option")
            If DialogResult.OK Then
                CheckBox2.Checked = True
            End If
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("https://github.com/CodingWonders/win11minst/issues/2")
    End Sub
End Class
