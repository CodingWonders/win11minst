Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports Microsoft.VisualBasic.ControlChars

Public Class InstHistPanel
    Dim LVData As New DataTable("InstallerHistory")

    Private Sub InstHistPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
            Label1.Text = "Installer history"
            InstallerEntryLabel.Text = "Installer history entries: " & InstallerListView.Items.Count
            ColumnHeader1.Text = "Installer name and path"
            ColumnHeader2.Text = "Creation time and date"
            OK_Button.Text = "OK"
            XMLExportOptn.Text = "Export to XML file..."
            HTMLExportOptn.Text = "Export to HTML file..."
            CSVExportOptn.Text = "Export to CSV file..."
            ExportOptnBtn.Text = "Export options"
        ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
            Label1.Text = "Historial de instaladores"
            InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstallerListView.Items.Count
            ColumnHeader1.Text = "Nombre y ruta del instalador"
            ColumnHeader2.Text = "Fecha y hora de creación"
            OK_Button.Text = "Aceptar"
            XMLExportOptn.Text = "Exportar a archivo XML..."
            HTMLExportOptn.Text = "Exportar a archivo HTML..."
            CSVExportOptn.Text = "Exportar a archivo CSV..."
            ExportOptnBtn.Text = "Opciones de exportación"
        ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
            Label1.Text = "Historique de l'installateur"
            InstallerEntryLabel.Text = "Entrées de l'historique de l'installateur : " & InstallerListView.Items.Count
            ColumnHeader1.Text = "Nom et chemin de l'installateur"
            ColumnHeader2.Text = "Date et heure de création"
            OK_Button.Text = "OK"
            XMLExportOptn.Text = "Exporter vers un fichier XML..."
            HTMLExportOptn.Text = "Exporter vers un fichier HTML..."
            CSVExportOptn.Text = "Exporter vers un fichier CSV..."
            ExportOptnBtn.Text = "Options d'exportation"
        ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
            If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                Label1.Text = "Installer history"
                InstallerEntryLabel.Text = "Installer history entries: " & InstallerListView.Items.Count
                ColumnHeader1.Text = "Installer name and path"
                ColumnHeader2.Text = "Creation time and date"
                OK_Button.Text = "OK"
                XMLExportOptn.Text = "Export to XML file..."
                HTMLExportOptn.Text = "Export to HTML file..."
                CSVExportOptn.Text = "Export to CSV file..."
                ExportOptnBtn.Text = "Export options"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                Label1.Text = "Historial de instaladores"
                InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstallerListView.Items.Count
                ColumnHeader1.Text = "Nombre y ruta del instalador"
                ColumnHeader2.Text = "Fecha y hora de creación"
                OK_Button.Text = "Aceptar"
                XMLExportOptn.Text = "Exportar a archivo XML..."
                HTMLExportOptn.Text = "Exportar a archivo HTML..."
                CSVExportOptn.Text = "Exportar a archivo CSV..."
                ExportOptnBtn.Text = "Opciones de exportación"
            ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                Label1.Text = "Historique de l'installateur"
                InstallerEntryLabel.Text = "Entrées de l'historique de l'installateur : " & InstallerListView.Items.Count
                ColumnHeader1.Text = "Nom et chemin de l'installateur"
                ColumnHeader2.Text = "Date et heure de création"
                OK_Button.Text = "OK"
                XMLExportOptn.Text = "Exporter vers un fichier XML..."
                HTMLExportOptn.Text = "Exporter vers un fichier HTML..."
                CSVExportOptn.Text = "Exporter vers un fichier CSV..."
                ExportOptnBtn.Text = "Options d'exportation"
            End If
        End If
        Text = Label1.Text
        If MainForm.BackColor = Color.FromArgb(243, 243, 243) Then
            BackColor = Color.White
            ForeColor = Color.Black
            Panel1.BackColor = Color.FromArgb(243, 243, 243)
            InstallerListView.ForeColor = Color.Black
            InstallerListView.BackColor = Color.White
            OK_Button.BackColor = Color.FromArgb(1, 92, 186)
            OK_Button.ForeColor = Color.White
            ExportOptnBtn.Image = New Bitmap(My.Resources.export_light)
            ExportOptnBtn.BackColor = Color.FromArgb(243, 243, 243)
            ExportOptnBtn.ForeColor = Color.Black
        ElseIf MainForm.BackColor = Color.FromArgb(32, 32, 32) Then
            BackColor = Color.FromArgb(43, 43, 43)
            ForeColor = Color.White
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
            If MainForm.ComboBox4.SelectedItem = "English" Or MainForm.ComboBox4.SelectedItem = "Inglés" Or MainForm.ComboBox4.SelectedItem = "Anglais" Then
                InstallerEntryLabel.Text = "Installer history entries: " & InstallerListView.Items.Count & ". No installer history data is available."
            ElseIf MainForm.ComboBox4.SelectedItem = "Spanish" Or MainForm.ComboBox4.SelectedItem = "Español" Or MainForm.ComboBox4.SelectedItem = "Espagnol" Then
                InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstallerListView.Items.Count & ". No hay datos disponibles sobre el historial de instaladores."
            ElseIf MainForm.ComboBox4.SelectedItem = "French" Or MainForm.ComboBox4.SelectedItem = "Francés" Or MainForm.ComboBox4.SelectedItem = "Français" Then
                InstallerEntryLabel.Text = "Entrées de l'historique de l'installateur : " & InstallerListView.Items.Count & ". Aucune donnée sur l'historique de l'installateur n'est disponible."
            ElseIf MainForm.ComboBox4.SelectedItem = "Automatic" Or MainForm.ComboBox4.SelectedItem = "Automático" Or MainForm.ComboBox4.SelectedItem = "Automatique" Then
                If My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ENG" Then
                    InstallerEntryLabel.Text = "Installer history entries: " & InstallerListView.Items.Count & ". No installer history data is available."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "ESN" Then
                    InstallerEntryLabel.Text = "Entradas en el historial de instaladores: " & InstallerListView.Items.Count & ". No hay datos disponibles sobre el historial de instaladores."
                ElseIf My.Computer.Info.InstalledUICulture.ThreeLetterWindowsLanguageName = "FRA" Then
                    InstallerEntryLabel.Text = "Entrées de l'historique de l'installateur : " & InstallerListView.Items.Count & ". Aucune donnée sur l'historique de l'installateur n'est disponible."
                End If
            End If
            ExportOptionsCMS.Enabled = False
        End If
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Me.Close()
        BackSubPanel.Close()
    End Sub

    Private Sub ExportOptnBtn_Click(sender As Object, e As EventArgs) Handles ExportOptnBtn.Click
        ExportOptionsCMS.Show(ExportOptnBtn, New Point(26, ExportOptnBtn.Height))
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
        If MainForm.RegionalCode = "en-us" Then
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
        ElseIf MainForm.RegionalCode = "es-es" Then
            My.Computer.FileSystem.WriteAllText(".\inst.html", _
                                                            "<html>" & CrLf & _
                                                            "<head>" & CrLf & _
                                                            "   <title>Instalador manual de Windows 11: historial de instaladores</title>" & CrLf & _
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
                                                            "   <h3 style=" & Quote & "font-family: Arial, Helvetica, sans-serif; text-transform: none; text-align: center" & Quote & ">Historial de instaladores</h3>" & CrLf & _
                                                            "   <p style=" & Quote & "font-family: Arial, Helvetica, sans-serif; text-transform: none; text-align: center" & Quote & ">Aquí puede ver la ruta completa y el tiempo de creación de los instaladores</p>" & CrLf & _
                                                            "   <table style=" & Quote & "font-family: Arial, Helvetica, sans-serif;" & Quote & ">" & CrLf & _
                                                            "       <tr>" & CrLf & _
                                                            "           <th class=" & Quote & "auto-style1" & Quote & " style=" & Quote & "border-spacing: 1px; text-align: center; font-family: Arial, Helvetica, sans-serif; font-weight: bold;" & Quote & ">Nombre y ruta del instalador</th>" & CrLf & _
                                                            "           <th class=" & Quote & "auto-style2" & Quote & " style=" & Quote & "border-spacing: 1px; text-align: center; font-family: Arial, Helvetica, sans-serif; font-weight: bold;" & Quote & ">Fecha y hora de creación</th>" & CrLf & _
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
        ElseIf MainForm.RegionalCode = "fr-fr" Then
            My.Computer.FileSystem.WriteAllText(".\inst.html", _
                                                            "<html>" & CrLf & _
                                                            "<head>" & CrLf & _
                                                            "   <title>Installateur manuel de Windows 11: historique de l'installateur</title>" & CrLf & _
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
                                                            "   <h3 style=" & Quote & "font-family: Arial, Helvetica, sans-serif; text-transform: none; text-align: center" & Quote & ">Historique de l'installateur</h3>" & CrLf & _
                                                            "   <p style=" & Quote & "font-family: Arial, Helvetica, sans-serif; text-transform: none; text-align: center" & Quote & ">Vous pouvez voir ici le chemin complet et l'heure de création des installateurs.</p>" & CrLf & _
                                                            "   <table style=" & Quote & "font-family: Arial, Helvetica, sans-serif;" & Quote & ">" & CrLf & _
                                                            "       <tr>" & CrLf & _
                                                            "           <th class=" & Quote & "auto-style1" & Quote & " style=" & Quote & "border-spacing: 1px; text-align: center; font-family: Arial, Helvetica, sans-serif; font-weight: bold;" & Quote & ">Nom et chemin de l'installateur</th>" & CrLf & _
                                                            "           <th class=" & Quote & "auto-style2" & Quote & " style=" & Quote & "border-spacing: 1px; text-align: center; font-family: Arial, Helvetica, sans-serif; font-weight: bold;" & Quote & ">Date et heure de création</th>" & CrLf & _
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
        Else
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
        End If
    End Sub

    Private Sub CSVExportOptn_Click(sender As Object, e As EventArgs) Handles CSVExportOptn.Click
        GenCsv()
    End Sub

    Sub GenCsv()
        If MainForm.RegionalCode = "en-us" Then
            My.Computer.FileSystem.WriteAllText(".\inst.csv", "Installer name and path,Creation time and date" & CrLf, False)
        ElseIf MainForm.RegionalCode = "es-es" Then
            My.Computer.FileSystem.WriteAllText(".\inst.csv", "Nombre y ruta del instalador,Fecha y hora de creación" & CrLf, False)
        ElseIf MainForm.RegionalCode = "fr-fr" Then
            My.Computer.FileSystem.WriteAllText(".\inst.csv", "Nom et chemin de l'installateur,Date et heure de création" & CrLf, False)
        Else
            My.Computer.FileSystem.WriteAllText(".\inst.csv", "Installer name and path,Creation time and date" & CrLf, False)
        End If
        For Each LVI As ListViewItem In InstallerListView.Items
            My.Computer.FileSystem.WriteAllText(".\inst.csv", LVI.Text & "," & LVI.SubItems(1).Text, True)
        Next
    End Sub
End Class
