Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ControlChars
Imports System.Net
Imports System.Windows.Forms.MouseEventArgs

Public Class ISOFileDownloadPanel

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.No
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub ISOFileDownloadPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            Label1.Text = "Download ISO files..."
            Label2.Text = "Download"
            Label4.Text = "Loading web component. Please wait..."
            Label5.Text = "We couldn 't load the web component." & CrLf & CrLf & "This can be caused by an unavailable Internet connection, or by a fault on the website backend. Please try again." & CrLf & "If the problem persists, please try to download the files manually by searching for " & Quote & "download windows 11" & Quote & " or " & Quote & "download windows 10" & Quote & " on the Internet."
            GroupBox1.Text = "Error status"
            Button1.Text = "Retry"
            OK_Button.Text = "OK"
            Cancel_Button.Text = "Cancel"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            Label1.Text = "Descargar archivos ISO..."
            Label2.Text = "Descargar"
            Label4.Text = "Cargando componente web. Por favor, espere..."
            Label5.Text = "No pudimos cargar el componente web." & CrLf & CrLf & "Esto puede ser causado por una conexión a Internet no disponible, o por un error en el backend de la página web. Por favor, inténtelo de nuevo." & CrLf & "Si el problema persiste, por favor, intente descargar los archivos manualmente buscando " & Quote & "descargar windows 11" & Quote & " o " & Quote & "descargar windows 10" & Quote & " en Internet."
            GroupBox1.Text = "Estado de error"
            Button1.Text = "Reintentar"
            OK_Button.Text = "Aceptar"
            Cancel_Button.Text = "Cancelar"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Download ISO files..."
                Label2.Text = "Download"
                Label4.Text = "Loading web component. Please wait..."
                Label5.Text = "We couldn 't load the web component." & CrLf & CrLf & "This can be caused by an unavailable Internet connection, or by a fault on the website backend. Please try again." & CrLf & "If the problem persists, please try to download the files manually by searching for " & Quote & "download windows 11" & Quote & " or " & Quote & "download windows 10" & Quote & " on the Internet."
                GroupBox1.Text = "Error status"
                Button1.Text = "Retry"
                OK_Button.Text = "OK"
                Cancel_Button.Text = "Cancel"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Descargar archivos ISO..."
                Label2.Text = "Descargar"
                Label4.Text = "Cargando componente web. Por favor, espere..."
                Label5.Text = "No pudimos cargar el componente web." & CrLf & CrLf & "Esto puede ser causado por una conexión a Internet no disponible, o por un error en el backend de la página web. Por favor, inténtelo de nuevo." & CrLf & "Si el problema persiste, por favor, intente descargar los archivos manualmente buscando " & Quote & "descargar windows 11" & Quote & " o " & Quote & "descargar windows 10" & Quote & " en Internet."
                GroupBox1.Text = "Estado de error"
                Button1.Text = "Reintentar"
                OK_Button.Text = "Aceptar"
                Cancel_Button.Text = "Cancelar"
            End If
        End If
        Text = Label1.Text
        UserAgentChanger.SetUserAgent("Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36")
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.White
            ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(43, 43, 43)
            ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
        End If
        Try
            If ModeSelectComboBox.SelectedItem = "Windows 11" Then
                WebComponent.Navigate("https://www.microsoft.com/software-download/windows11")
            ElseIf ModeSelectComboBox.SelectedItem = "Windows 10" Then
                WebComponent.Navigate("https://www.microsoft.com/software-download/windows10")
            End If
            WebComponentLoadPanel.Visible = False
            WebComponentPanel.Visible = True
        Catch WebEx As WebException
            WebComponentLoadPanel.Visible = False
            WebComponentLoadErrorPanel.Visible = True
            Label6.Text = "Error code: " & CType(WebEx.Response, HttpWebResponse).StatusCode & CrLf & CrLf & CType(WebEx.Response, HttpWebResponse).StatusDescription
        End Try
    End Sub

    Private Sub ModeSelectComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ModeSelectComboBox.SelectedIndexChanged
        Try
            If ModeSelectComboBox.SelectedItem = "Windows 11" Then
                WebComponent.Navigate("https://www.microsoft.com/software-download/windows11")
            ElseIf ModeSelectComboBox.SelectedItem = "Windows 10" Then
                WebComponent.Navigate("https://www.microsoft.com/software-download/windows10")
            End If
            WebComponentLoadPanel.Visible = False
            WebComponentPanel.Visible = True
        Catch WebEx As WebException
            WebComponentLoadPanel.Visible = False
            WebComponentLoadErrorPanel.Visible = True
            Label6.Text = "Error code: " & CType(WebEx.Response, HttpWebResponse).StatusCode & CrLf & CrLf & CType(WebEx.Response, HttpWebResponse).StatusDescription
        End Try
    End Sub

    Private Sub UAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UAToolStripMenuItem.Click
        System.Diagnostics.Process.Start("https://developers.whatismybrowser.com/useragents/parse/118164231chrome-linux-blink")
    End Sub

    Private Sub Label3_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label3.Click
        ContextMenuStrip1.Show(CType(sender, Control), e.Location)
    End Sub
End Class
