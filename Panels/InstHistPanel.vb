﻿Imports System.Windows.Forms
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
            XMLExportOptn.Text = "Export to XML file..."
            HTMLExportOptn.Text = "Export to HTML file..."
            ExportOptnBtn.Text = "Export options"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Then
            Label1.Text = "Historial de instaladores"
            InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstallerListView.Items.Count
            ColumnHeader1.Text = "Nombre y ruta del instalador"
            ColumnHeader2.Text = "Fecha y hora de creación"
            OK_Button.Text = "Aceptar"
            XMLExportOptn.Text = "Exportar a archivo XML..."
            HTMLExportOptn.Text = "Exportar a archivo HTML..."
            ExportOptnBtn.Text = "Opciones de exportación"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Installer history"
                InstallerEntryLabel.Text = "Installer history entries: " & InstallerListView.Items.Count
                ColumnHeader1.Text = "Installer name and path"
                ColumnHeader2.Text = "Creation time and date"
                OK_Button.Text = "OK"
                XMLExportOptn.Text = "Export to XML file..."
                HTMLExportOptn.Text = "Export to HTML file..."
                ExportOptnBtn.Text = "Export options"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Historial de instaladores"
                InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstallerListView.Items.Count
                ColumnHeader1.Text = "Nombre y ruta del instalador"
                ColumnHeader2.Text = "Fecha y hora de creación"
                OK_Button.Text = "Aceptar"
                XMLExportOptn.Text = "Exportar a archivo XML..."
                HTMLExportOptn.Text = "Exportar a archivo HTML..."
                ExportOptnBtn.Text = "Opciones de exportación"
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
            ExportOptnBtn.Image = New Bitmap(My.Resources.export_light)
            ExportOptnBtn.BackColor = Color.FromArgb(243, 243, 243)
            ExportOptnBtn.ForeColor = Color.Black
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            Me.BackColor = Color.FromArgb(43, 43, 43)
            Me.ForeColor = Color.White
            Panel1.BackColor = Color.FromArgb(32, 32, 32)
            InstallerListView.ForeColor = Color.White
            InstallerListView.BackColor = Color.FromArgb(43, 43, 43)
            OK_Button.BackColor = Color.FromArgb(76, 194, 255)
            OK_Button.ForeColor = Color.Black
            ExportOptnBtn.Image = New Bitmap(My.Resources.export_dark)
            ExportOptnBtn.BackColor = Color.FromArgb(32, 32, 32)
            ExportOptnBtn.ForeColor = Color.White
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

    Private Sub ExportOptnBtn_Click(sender As Object, e As MouseEventArgs) Handles ExportOptnBtn.Click
        ExportOptionsCMS.Show(CType(sender, Control), e.Location)
    End Sub

    Private Sub XMLExportOptn_Click(sender As Object, e As EventArgs) Handles XMLExportOptn.Click
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

    Private Sub HTMLExportOptn_Click(sender As Object, e As EventArgs) Handles HTMLExportOptn.Click
        GenHtml()
    End Sub

    Sub GenHtml()
        If File.Exists(".\inst.html") Then
            File.Delete(".\inst.html")
        End If
        My.Computer.FileSystem.WriteAllText(".\inst.html", _
                                            "<html>" & CrLf & _
                                            "<head>" & CrLf & _
                                            "   <title>Windows 11 Manual Installer: installer history</title>" & CrLf & _
                                            "   <style>" & CrLf & _
                                            "       body {" & CrLf & _
                                            "           background-color: rgb(249,249,249);" & CrLf & _
                                            "       }" & CrLf & _
                                            "   </style>" & CrLf & _
                                            "   <style>" & CrLf & _
                                            "       table {" & CrLf & _
                                            "           font-family: arial, helvetica, sans-serif;" & CrLf & _
                                            "           border-collapse: collapse;" & CrLf & _
                                            "           width: 100%" & CrLf & _
                                            "       }" & CrLf & _
                                            CrLf & _
                                            "       th {" & CrLf & _
                                            "           border: 1px solid #ff000000;" & CrLf & _
                                            "           text-align: center;" & CrLf & _
                                            "           padding: 8px;" & CrLf & _
                                            "       }" & CrLf & _
                                            CrLf & _
                                            "       td {" & CrLf & _
                                            "           border: 1px solid #ff000000;" & CrLf & _
                                            "           text-align: left;" & CrLf & _
                                            "           padding: 8px;" & CrLf & _
                                            "       }" & CrLf & _
                                            "   </style>" & CrLf & _
                                            "</head>" & CrLf & _
                                            "<body>" & CrLf & _
                                            "   <img src=" & Quote & "./Resources/HTMLHelp/Resources/helpbanner.gif" & Quote & " class=" & Quote & "center" & Quote & " />" & CrLf & _
                                            "   <style>" & CrLf & _
                                            "       .center {" & CrLf & _
                                            "           display: block;" & CrLf & _
                                            "           margin-left: auto;" & CrLf & _
                                            "           margin-right: auto;" & CrLf & _
                                            "       }" & CrLf & _
                                            "   </style>" & CrLf & _
                                            "   <h3 style=" & Quote & "font-family: Arial, Helvetica, sans-serif; text-transform: none; text-align: center" & Quote & ">Installer history</h3>" & CrLf & _
                                            "   <p style=" & Quote & "font-family: Arial, Helvetica, sans-serif; text-transform: none; text-align: center" & Quote & ">Here you can see the installer full path and creation time</p>" & CrLf & _
                                            "   <table style=" & Quote & "font-family: Arial, Helvetica, sans-serif;" & Quote & ">" & CrLf & _
                                            "       <tr>" & CrLf & _
                                            "           <th class=" & Quote & "auto-style1" & Quote & " style=" & Quote & "border-spacing: 1px; text-align: center; font-family: Arial, Helvetica, sans-serif; font-weight: bold;" & Quote & ">Installer name and path</th>" & CrLf & _
                                            "           <th class=" & Quote & "auto-style2" & Quote & " style=" & Quote & "border-spacing: 1px; text-align: center; font-family: Arial, Helvetica, sans-serif; font-weight: bold;" & Quote & ">Creation time and date</th>" & CrLf & _
                                            "       </tr>", True)
        For Each LVI As ListViewItem In InstallerListView.Items
            My.Computer.FileSystem.WriteAllText(".\inst.html", _
                                    "       <tr>" & CrLf & _
                                    "           <th class=" & Quote & "auto-style1" & Quote & " style=" & Quote & "border-spacing: 1px; text-align: left; font-family: Arial, Helvetica, sans-serif;" & Quote & ">" & LVI.Text & "</th>" & CrLf & _
                                    "           <th class=" & Quote & "auto-style2" & Quote & " style=" & Quote & "border-spacing: 1px; text-align: left; font-family: Arial, Helvetica, sans-serif;" & Quote & ">" & LVI.SubItems(1).Text & "</th>" & CrLf & _
                                    "       </tr>" & CrLf, True)
        Next
        My.Computer.FileSystem.WriteAllText(".\inst.html", _
                                            "   </table>" & CrLf & _
                                            "</body>" & CrLf & _
                                            "</html>", True)
    End Sub
End Class
