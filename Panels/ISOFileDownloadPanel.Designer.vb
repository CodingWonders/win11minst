<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ISOFileDownloadPanel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ISOFileDownloadPanel))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ModeSelectComboBox = New System.Windows.Forms.ComboBox()
        Me.WebComponentLoadPanel = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.WebComponentLoadErrorPanel = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.WebComponentPanel = New System.Windows.Forms.Panel()
        Me.WebComponent = New System.Windows.Forms.WebBrowser()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UAToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuildModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuildModePanel = New System.Windows.Forms.Panel()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.BuildPB = New System.Windows.Forms.ProgressBar()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.BuildSTLabel = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.BuildBW = New System.ComponentModel.BackgroundWorker()
        Me.Panel1.SuspendLayout()
        Me.WebComponentLoadPanel.SuspendLayout()
        Me.WebComponentLoadErrorPanel.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.WebComponentPanel.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.BuildModePanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(26, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(184, 26)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Download ISO files..."
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.OK_Button)
        Me.Panel1.Controls.Add(Me.Cancel_Button)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 340)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(720, 80)
        Me.Panel1.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label3.Location = New System.Drawing.Point(27, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 20)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = ". . ."
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK_Button.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.OK_Button.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OK_Button.ForeColor = System.Drawing.Color.White
        Me.OK_Button.Location = New System.Drawing.Point(509, 24)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(91, 32)
        Me.OK_Button.TabIndex = 1
        Me.OK_Button.Text = "OK"
        Me.OK_Button.UseVisualStyleBackColor = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cancel_Button.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel_Button.Location = New System.Drawing.Point(606, 24)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(91, 32)
        Me.Cancel_Button.TabIndex = 3
        Me.Cancel_Button.Text = "Cancel"
        Me.Cancel_Button.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label2.Location = New System.Drawing.Point(370, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(181, 20)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Download"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ModeSelectComboBox
        '
        Me.ModeSelectComboBox.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.ModeSelectComboBox.FormattingEnabled = True
        Me.ModeSelectComboBox.Items.AddRange(New Object() {"Windows 11", "Windows 10"})
        Me.ModeSelectComboBox.Location = New System.Drawing.Point(557, 28)
        Me.ModeSelectComboBox.Name = "ModeSelectComboBox"
        Me.ModeSelectComboBox.Size = New System.Drawing.Size(121, 28)
        Me.ModeSelectComboBox.TabIndex = 9
        Me.ModeSelectComboBox.Text = "Windows 11"
        '
        'WebComponentLoadPanel
        '
        Me.WebComponentLoadPanel.Controls.Add(Me.Label4)
        Me.WebComponentLoadPanel.Location = New System.Drawing.Point(43, 66)
        Me.WebComponentLoadPanel.Name = "WebComponentLoadPanel"
        Me.WebComponentLoadPanel.Size = New System.Drawing.Size(635, 256)
        Me.WebComponentLoadPanel.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(211, 121)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(213, 15)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Loading web component. Please wait..."
        '
        'WebComponentLoadErrorPanel
        '
        Me.WebComponentLoadErrorPanel.Controls.Add(Me.GroupBox1)
        Me.WebComponentLoadErrorPanel.Controls.Add(Me.Button1)
        Me.WebComponentLoadErrorPanel.Controls.Add(Me.Label5)
        Me.WebComponentLoadErrorPanel.Location = New System.Drawing.Point(43, 66)
        Me.WebComponentLoadErrorPanel.Name = "WebComponentLoadErrorPanel"
        Me.WebComponentLoadErrorPanel.Size = New System.Drawing.Size(635, 256)
        Me.WebComponentLoadErrorPanel.TabIndex = 10
        Me.WebComponentLoadErrorPanel.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 105)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(593, 91)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Error status"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(17, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(560, 59)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "error_status"
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button1.Location = New System.Drawing.Point(280, 212)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Retry"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Crimson
        Me.Label5.Location = New System.Drawing.Point(19, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(596, 75)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = resources.GetString("Label5.Text")
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'WebComponentPanel
        '
        Me.WebComponentPanel.Controls.Add(Me.WebComponent)
        Me.WebComponentPanel.Location = New System.Drawing.Point(43, 66)
        Me.WebComponentPanel.Name = "WebComponentPanel"
        Me.WebComponentPanel.Size = New System.Drawing.Size(635, 256)
        Me.WebComponentPanel.TabIndex = 10
        Me.WebComponentPanel.Visible = False
        '
        'WebComponent
        '
        Me.WebComponent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebComponent.IsWebBrowserContextMenuEnabled = False
        Me.WebComponent.Location = New System.Drawing.Point(0, 0)
        Me.WebComponent.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebComponent.Name = "WebComponent"
        Me.WebComponent.ScriptErrorsSuppressed = True
        Me.WebComponent.Size = New System.Drawing.Size(635, 256)
        Me.WebComponent.TabIndex = 0
        Me.WebComponent.WebBrowserShortcutsEnabled = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UAToolStripMenuItem, Me.BuildModeToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(156, 52)
        '
        'UAToolStripMenuItem
        '
        Me.UAToolStripMenuItem.Image = Global.Windows_11_Manual_Installer_2._0.My.Resources.Resources.linux_ua
        Me.UAToolStripMenuItem.Name = "UAToolStripMenuItem"
        Me.UAToolStripMenuItem.Size = New System.Drawing.Size(155, 24)
        Me.UAToolStripMenuItem.Text = "<ua>"
        '
        'BuildModeToolStripMenuItem
        '
        Me.BuildModeToolStripMenuItem.Image = Global.Windows_11_Manual_Installer_2._0.My.Resources.Resources.build_mode
        Me.BuildModeToolStripMenuItem.Name = "BuildModeToolStripMenuItem"
        Me.BuildModeToolStripMenuItem.Size = New System.Drawing.Size(155, 24)
        Me.BuildModeToolStripMenuItem.Text = "Build mode"
        '
        'BuildModePanel
        '
        Me.BuildModePanel.Controls.Add(Me.Button4)
        Me.BuildModePanel.Controls.Add(Me.BuildPB)
        Me.BuildModePanel.Controls.Add(Me.Button2)
        Me.BuildModePanel.Controls.Add(Me.Button3)
        Me.BuildModePanel.Controls.Add(Me.TextBox1)
        Me.BuildModePanel.Controls.Add(Me.ComboBox1)
        Me.BuildModePanel.Controls.Add(Me.BuildSTLabel)
        Me.BuildModePanel.Controls.Add(Me.Label9)
        Me.BuildModePanel.Controls.Add(Me.Label8)
        Me.BuildModePanel.Controls.Add(Me.Label7)
        Me.BuildModePanel.Location = New System.Drawing.Point(43, 66)
        Me.BuildModePanel.Name = "BuildModePanel"
        Me.BuildModePanel.Size = New System.Drawing.Size(635, 256)
        Me.BuildModePanel.TabIndex = 10
        Me.BuildModePanel.Visible = False
        '
        'Button4
        '
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button4.Location = New System.Drawing.Point(531, 114)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "Browse..."
        Me.Button4.UseVisualStyleBackColor = True
        '
        'BuildPB
        '
        Me.BuildPB.Location = New System.Drawing.Point(32, 166)
        Me.BuildPB.MarqueeAnimationSpeed = 10
        Me.BuildPB.Name = "BuildPB"
        Me.BuildPB.Size = New System.Drawing.Size(574, 23)
        Me.BuildPB.TabIndex = 4
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button2.Location = New System.Drawing.Point(256, 85)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(122, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Open link"
        Me.Button2.UseMnemonic = False
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Enabled = False
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button3.Location = New System.Drawing.Point(256, 208)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(122, 23)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Build"
        Me.Button3.UseMnemonic = False
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.AllowDrop = True
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(148, 114)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(377, 23)
        Me.TextBox1.TabIndex = 2
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Public", "Release Preview", "Beta", "Dev"})
        Me.ComboBox1.Location = New System.Drawing.Point(251, 56)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(355, 23)
        Me.ComboBox1.TabIndex = 1
        '
        'BuildSTLabel
        '
        Me.BuildSTLabel.AutoSize = True
        Me.BuildSTLabel.Location = New System.Drawing.Point(29, 148)
        Me.BuildSTLabel.Name = "BuildSTLabel"
        Me.BuildSTLabel.Size = New System.Drawing.Size(39, 15)
        Me.BuildSTLabel.TabIndex = 0
        Me.BuildSTLabel.Text = "Status"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(29, 117)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(113, 15)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Path to UUP ZIP file:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(29, 59)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(216, 15)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Channel (follows WIP channel naming):"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(29, 26)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(376, 15)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "In here you can specify the options to create a stock Windows installer"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "ZIP files|*.zip"
        Me.OpenFileDialog1.Title = "Please specify the path to the UUP ZIP file..."
        '
        'BuildBW
        '
        '
        'ISOFileDownloadPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(720, 420)
        Me.Controls.Add(Me.BuildModePanel)
        Me.Controls.Add(Me.WebComponentPanel)
        Me.Controls.Add(Me.WebComponentLoadErrorPanel)
        Me.Controls.Add(Me.WebComponentLoadPanel)
        Me.Controls.Add(Me.ModeSelectComboBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ISOFileDownloadPanel"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Download ISO files..."
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.WebComponentLoadPanel.ResumeLayout(False)
        Me.WebComponentLoadPanel.PerformLayout()
        Me.WebComponentLoadErrorPanel.ResumeLayout(False)
        Me.WebComponentLoadErrorPanel.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.WebComponentPanel.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.BuildModePanel.ResumeLayout(False)
        Me.BuildModePanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ModeSelectComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents WebComponentLoadPanel As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents WebComponentLoadErrorPanel As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents WebComponentPanel As System.Windows.Forms.Panel
    Friend WithEvents WebComponent As System.Windows.Forms.WebBrowser
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents UAToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuildModeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuildModePanel As System.Windows.Forms.Panel
    Friend WithEvents BuildPB As System.Windows.Forms.ProgressBar
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents BuildSTLabel As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BuildBW As System.ComponentModel.BackgroundWorker

End Class
