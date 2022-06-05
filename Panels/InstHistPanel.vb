Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports Microsoft.VisualBasic.ControlChars

Public Class InstHistPanel
    Dim LVData As New DataTable("InstallerHistory")

    Private Sub InstHistPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
            Label1.Text = "Installer history"
            InstallerEntryLabel.Text = "Installer history entries: " & InstallerListView.Items.Count
            ColumnHeader1.Text = "Installer name and path"
            ColumnHeader2.Text = "Creation time and date"
            OK_Button.Text = "OK"
            XMLExportLink.Text = "Export to XML file..."
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            Label1.Text = "Historial de instaladores"
            InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstallerListView.Items.Count
            ColumnHeader1.Text = "Nombre y ruta del instalador"
            ColumnHeader2.Text = "Fecha y hora de creación"
            OK_Button.Text = "Aceptar"
            XMLExportLink.Text = "Exportar a archivo XML..."
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Installer history"
                InstallerEntryLabel.Text = "Installer history entries: " & InstallerListView.Items.Count
                ColumnHeader1.Text = "Installer name and path"
                ColumnHeader2.Text = "Creation time and date"
                OK_Button.Text = "OK"
                XMLExportLink.Text = "Export to XML file..."
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Historial de instaladores"
                InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstallerListView.Items.Count
                ColumnHeader1.Text = "Nombre y ruta del instalador"
                ColumnHeader2.Text = "Fecha y hora de creación"
                OK_Button.Text = "Aceptar"
                XMLExportLink.Text = "Exportar a archivo XML..."
            End If
        End If
        Text = Label1.Text
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            Me.BackColor = Color.White
            Me.ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            InstallerListView.ForeColor = Color.Black
            InstallerListView.BackColor = Color.White
            OK_Button.BackColor = Color.FromArgb(1, 92, 186)
            OK_Button.ForeColor = Color.White
            XMLExportLink.LinkColor = Color.FromArgb(1, 92, 186)
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            Me.BackColor = Color.FromArgb(43, 43, 43)
            Me.ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            InstallerListView.ForeColor = Color.White
            InstallerListView.BackColor = Color.FromArgb(43, 43, 43)
            OK_Button.BackColor = Color.FromArgb(76, 194, 255)
            OK_Button.ForeColor = Color.Black
            XMLExportLink.LinkColor = Color.FromArgb(76, 194, 255)
        End If
        If InstallerListView.Items.Count = 0 Then
            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Then
                InstallerEntryLabel.Text = "Installer history entries: " & InstallerListView.Items.Count & ". No installer history data is available."
            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
                InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstallerListView.Items.Count & ". No hay datos disponibles sobre el historial de instaladores."
            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    InstallerEntryLabel.Text = "Installer history entries: " & InstallerListView.Items.Count & ". No installer history data is available."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstallerListView.Items.Count & ". No hay datos disponibles sobre el historial de instaladores."
                End If
            End If
        End If
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub XMLExportLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles XMLExportLink.LinkClicked
        LVData = New DataTable("InstallerHistory")
        With LVData
            .Columns.Add(New DataColumn("InstallerNameAndPath", GetType(String)))
            .Columns.Add(New DataColumn("CreationTimeAndDate", GetType(String)))
        End With
        For Each LVI As ListViewItem In InstallerListView.Items
            Dim DataRow As DataRow = LVData.NewRow
            With DataRow
                .Item(0) = LVI.Text
                .Item(1) = LVI.SubItems(1).Text
            End With
            LVData.Rows.Add(DataRow)
        Next
        If File.Exists(".\inst.xml") Then
            File.Delete(".\inst.xml")
        End If
        LVData.WriteXml(".\inst.xml", XmlWriteMode.WriteSchema)
    End Sub
End Class
