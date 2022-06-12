<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InstHistPanel
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InstHistPanel))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ExportOptnBtn = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.InstallerListView = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.InstallerEntryLabel = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.ExportOptionsCMS = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.XMLExportOptn = New System.Windows.Forms.ToolStripMenuItem()
        Me.HTMLExportOptn = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.ExportOptionsCMS.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Panel1.Controls.Add(Me.ExportOptnBtn)
        Me.Panel1.Controls.Add(Me.OK_Button)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Panel1.Location = New System.Drawing.Point(0, 520)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 80)
        Me.Panel1.TabIndex = 1
        '
        'ExportOptnBtn
        '
        Me.ExportOptnBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExportOptnBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.ExportOptnBtn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Desktop
        Me.ExportOptnBtn.FlatAppearance.BorderSize = 0
        Me.ExportOptnBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExportOptnBtn.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExportOptnBtn.ForeColor = System.Drawing.Color.Black
        Me.ExportOptnBtn.Image = Global.Windows_11_Manual_Installer_2._0.My.Resources.Resources.export_light
        Me.ExportOptnBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExportOptnBtn.Location = New System.Drawing.Point(22, 24)
        Me.ExportOptnBtn.Name = "ExportOptnBtn"
        Me.ExportOptnBtn.Size = New System.Drawing.Size(231, 32)
        Me.ExportOptnBtn.TabIndex = 1
        Me.ExportOptnBtn.Text = "Export options"
        Me.ExportOptnBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExportOptnBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ExportOptnBtn.UseVisualStyleBackColor = False
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK_Button.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.OK_Button.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OK_Button.ForeColor = System.Drawing.Color.White
        Me.OK_Button.Location = New System.Drawing.Point(686, 24)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(91, 32)
        Me.OK_Button.TabIndex = 1
        Me.OK_Button.Text = "OK"
        Me.OK_Button.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(26, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 26)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Installer history"
        '
        'InstallerListView
        '
        Me.InstallerListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.InstallerListView.Location = New System.Drawing.Point(57, 90)
        Me.InstallerListView.MultiSelect = False
        Me.InstallerListView.Name = "InstallerListView"
        Me.InstallerListView.Size = New System.Drawing.Size(686, 368)
        Me.InstallerListView.TabIndex = 3
        Me.InstallerListView.UseCompatibleStateImageBehavior = False
        Me.InstallerListView.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Installer name and path"
        Me.ColumnHeader1.Width = 323
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Creation time and date"
        Me.ColumnHeader2.Width = 339
        '
        'InstallerEntryLabel
        '
        Me.InstallerEntryLabel.AutoSize = True
        Me.InstallerEntryLabel.Location = New System.Drawing.Point(54, 476)
        Me.InstallerEntryLabel.Name = "InstallerEntryLabel"
        Me.InstallerEntryLabel.Size = New System.Drawing.Size(128, 15)
        Me.InstallerEntryLabel.TabIndex = 4
        Me.InstallerEntryLabel.Text = "Installer history entries:"
        '
        'ExportOptionsCMS
        '
        Me.ExportOptionsCMS.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.ExportOptionsCMS.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.XMLExportOptn, Me.HTMLExportOptn})
        Me.ExportOptionsCMS.Name = "ExportOptionsCMS"
        Me.ExportOptionsCMS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ExportOptionsCMS.Size = New System.Drawing.Size(217, 52)
        '
        'XMLExportOptn
        '
        Me.XMLExportOptn.Name = "XMLExportOptn"
        Me.XMLExportOptn.Size = New System.Drawing.Size(216, 24)
        Me.XMLExportOptn.Text = "Export to XML file..."
        '
        'HTMLExportOptn
        '
        Me.HTMLExportOptn.Name = "HTMLExportOptn"
        Me.HTMLExportOptn.Size = New System.Drawing.Size(216, 24)
        Me.HTMLExportOptn.Text = "Export to HTML file..."
        '
        'InstHistPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.InstallerEntryLabel)
        Me.Controls.Add(Me.InstallerListView)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InstHistPanel"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Installer history"
        Me.Panel1.ResumeLayout(False)
        Me.ExportOptionsCMS.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents InstallerListView As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents InstallerEntryLabel As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ExportOptnBtn As System.Windows.Forms.Button
    Friend WithEvents ExportOptionsCMS As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents XMLExportOptn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HTMLExportOptn As System.Windows.Forms.ToolStripMenuItem

End Class
