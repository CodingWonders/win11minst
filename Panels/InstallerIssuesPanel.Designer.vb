<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InstallerIssuesPanel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InstallerIssuesPanel))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Issue1 = New System.Windows.Forms.Label()
        Me.Issue2 = New System.Windows.Forms.Label()
        Me.Issue3 = New System.Windows.Forms.Label()
        Me.Issue4 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.InvalidTextFieldPic = New System.Windows.Forms.PictureBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.IssueFix1 = New System.Windows.Forms.LinkLabel()
        Me.IssueFix2 = New System.Windows.Forms.LinkLabel()
        Me.IssueFix3 = New System.Windows.Forms.LinkLabel()
        Me.IssueFix4 = New System.Windows.Forms.LinkLabel()
        Me.IssuePic1 = New System.Windows.Forms.PictureBox()
        Me.IssuePic2 = New System.Windows.Forms.PictureBox()
        Me.IssuePic3 = New System.Windows.Forms.PictureBox()
        Me.IssuePic4 = New System.Windows.Forms.PictureBox()
        Me.Issue5 = New System.Windows.Forms.Label()
        Me.IssueFix5 = New System.Windows.Forms.LinkLabel()
        Me.IssuePic5 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.InvalidTextFieldPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IssuePic1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IssuePic2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IssuePic3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IssuePic4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IssuePic5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Panel1.Controls.Add(Me.OK_Button)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Panel1.Location = New System.Drawing.Point(0, 520)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(960, 80)
        Me.Panel1.TabIndex = 3
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK_Button.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.OK_Button.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OK_Button.ForeColor = System.Drawing.Color.White
        Me.OK_Button.Location = New System.Drawing.Point(846, 24)
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
        Me.Label1.Size = New System.Drawing.Size(213, 26)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Installer creation issues"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label2.Location = New System.Drawing.Point(146, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(742, 62)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "You cannot create the installer unless you fix these issues:"
        '
        'Issue1
        '
        Me.Issue1.AutoEllipsis = True
        Me.Issue1.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Issue1.Location = New System.Drawing.Point(106, 156)
        Me.Issue1.Name = "Issue1"
        Me.Issue1.Size = New System.Drawing.Size(701, 22)
        Me.Issue1.TabIndex = 5
        Me.Issue1.Text = "The Windows 11 installer does not exist"
        '
        'Issue2
        '
        Me.Issue2.AutoEllipsis = True
        Me.Issue2.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Issue2.Location = New System.Drawing.Point(106, 180)
        Me.Issue2.Name = "Issue2"
        Me.Issue2.Size = New System.Drawing.Size(701, 22)
        Me.Issue2.TabIndex = 5
        Me.Issue2.Text = "The Windows 10 installer does not exist"
        '
        'Issue3
        '
        Me.Issue3.AutoEllipsis = True
        Me.Issue3.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Issue3.Location = New System.Drawing.Point(106, 204)
        Me.Issue3.Name = "Issue3"
        Me.Issue3.Size = New System.Drawing.Size(701, 22)
        Me.Issue3.TabIndex = 5
        Me.Issue3.Text = "There are invalid characters on the target installer name"
        '
        'Issue4
        '
        Me.Issue4.AutoEllipsis = True
        Me.Issue4.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Issue4.Location = New System.Drawing.Point(106, 228)
        Me.Issue4.Name = "Issue4"
        Me.Issue4.Size = New System.Drawing.Size(701, 22)
        Me.Issue4.TabIndex = 5
        Me.Issue4.Text = "There are invalid characters on the target installer path"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel1.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.LinkLabel1.LinkArea = New System.Windows.Forms.LinkArea(50, 30)
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.LinkLabel1.Location = New System.Drawing.Point(20, 66)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(772, 41)
        Me.LinkLabel1.TabIndex = 6
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "If you don't know what reserved names are, please read Microsoft's documentation"
        Me.LinkLabel1.UseCompatibleTextRendering = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LinkLabel1)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(76, 288)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(812, 121)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Windows reserved character list"
        '
        'Label7
        '
        Me.Label7.AutoEllipsis = True
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label7.Location = New System.Drawing.Point(20, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(615, 20)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Reserved names and characters: CON, AUX, PRN, NUL, LPT{1-9}, COM{1-9}, <, >, :, """ & _
    ", *, /, \, |, ?"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.InvalidTextFieldPic)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.GroupBox2.Location = New System.Drawing.Point(76, 415)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(812, 80)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "More information"
        '
        'InvalidTextFieldPic
        '
        Me.InvalidTextFieldPic.Image = Global.Windows_11_Manual_Installer.My.Resources.Resources.invalidtext_light
        Me.InvalidTextFieldPic.Location = New System.Drawing.Point(511, 34)
        Me.InvalidTextFieldPic.Name = "InvalidTextFieldPic"
        Me.InvalidTextFieldPic.Size = New System.Drawing.Size(281, 25)
        Me.InvalidTextFieldPic.TabIndex = 6
        Me.InvalidTextFieldPic.TabStop = False
        '
        'Label8
        '
        Me.Label8.AutoEllipsis = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Label8.Location = New System.Drawing.Point(20, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(466, 45)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "To see whether an issue is encountered, invalid text boxes are shown in red."
        '
        'IssueFix1
        '
        Me.IssueFix1.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.IssueFix1.LinkArea = New System.Windows.Forms.LinkArea(0, 32)
        Me.IssueFix1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.IssueFix1.LinkColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.IssueFix1.Location = New System.Drawing.Point(815, 156)
        Me.IssueFix1.Name = "IssueFix1"
        Me.IssueFix1.Size = New System.Drawing.Size(73, 22)
        Me.IssueFix1.TabIndex = 6
        Me.IssueFix1.TabStop = True
        Me.IssueFix1.Text = "Fix"
        Me.IssueFix1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IssueFix1.UseCompatibleTextRendering = True
        '
        'IssueFix2
        '
        Me.IssueFix2.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.IssueFix2.LinkArea = New System.Windows.Forms.LinkArea(0, 32)
        Me.IssueFix2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.IssueFix2.LinkColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.IssueFix2.Location = New System.Drawing.Point(815, 180)
        Me.IssueFix2.Name = "IssueFix2"
        Me.IssueFix2.Size = New System.Drawing.Size(73, 22)
        Me.IssueFix2.TabIndex = 6
        Me.IssueFix2.TabStop = True
        Me.IssueFix2.Text = "Fix"
        Me.IssueFix2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IssueFix2.UseCompatibleTextRendering = True
        '
        'IssueFix3
        '
        Me.IssueFix3.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.IssueFix3.LinkArea = New System.Windows.Forms.LinkArea(0, 32)
        Me.IssueFix3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.IssueFix3.LinkColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.IssueFix3.Location = New System.Drawing.Point(815, 204)
        Me.IssueFix3.Name = "IssueFix3"
        Me.IssueFix3.Size = New System.Drawing.Size(73, 22)
        Me.IssueFix3.TabIndex = 6
        Me.IssueFix3.TabStop = True
        Me.IssueFix3.Text = "Fix"
        Me.IssueFix3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IssueFix3.UseCompatibleTextRendering = True
        '
        'IssueFix4
        '
        Me.IssueFix4.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.IssueFix4.LinkArea = New System.Windows.Forms.LinkArea(0, 32)
        Me.IssueFix4.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.IssueFix4.LinkColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.IssueFix4.Location = New System.Drawing.Point(815, 228)
        Me.IssueFix4.Name = "IssueFix4"
        Me.IssueFix4.Size = New System.Drawing.Size(73, 22)
        Me.IssueFix4.TabIndex = 6
        Me.IssueFix4.TabStop = True
        Me.IssueFix4.Text = "Fix"
        Me.IssueFix4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IssueFix4.UseCompatibleTextRendering = True
        '
        'IssuePic1
        '
        Me.IssuePic1.Image = Global.Windows_11_Manual_Installer.My.Resources.Resources.cross_light
        Me.IssuePic1.Location = New System.Drawing.Point(76, 155)
        Me.IssuePic1.Name = "IssuePic1"
        Me.IssuePic1.Size = New System.Drawing.Size(24, 24)
        Me.IssuePic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.IssuePic1.TabIndex = 8
        Me.IssuePic1.TabStop = False
        '
        'IssuePic2
        '
        Me.IssuePic2.Image = Global.Windows_11_Manual_Installer.My.Resources.Resources.cross_light
        Me.IssuePic2.Location = New System.Drawing.Point(76, 179)
        Me.IssuePic2.Name = "IssuePic2"
        Me.IssuePic2.Size = New System.Drawing.Size(24, 24)
        Me.IssuePic2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.IssuePic2.TabIndex = 8
        Me.IssuePic2.TabStop = False
        '
        'IssuePic3
        '
        Me.IssuePic3.Image = Global.Windows_11_Manual_Installer.My.Resources.Resources.cross_light
        Me.IssuePic3.Location = New System.Drawing.Point(76, 203)
        Me.IssuePic3.Name = "IssuePic3"
        Me.IssuePic3.Size = New System.Drawing.Size(24, 24)
        Me.IssuePic3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.IssuePic3.TabIndex = 8
        Me.IssuePic3.TabStop = False
        '
        'IssuePic4
        '
        Me.IssuePic4.Image = Global.Windows_11_Manual_Installer.My.Resources.Resources.cross_light
        Me.IssuePic4.Location = New System.Drawing.Point(76, 227)
        Me.IssuePic4.Name = "IssuePic4"
        Me.IssuePic4.Size = New System.Drawing.Size(24, 24)
        Me.IssuePic4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.IssuePic4.TabIndex = 8
        Me.IssuePic4.TabStop = False
        '
        'Issue5
        '
        Me.Issue5.AutoEllipsis = True
        Me.Issue5.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.Issue5.Location = New System.Drawing.Point(106, 252)
        Me.Issue5.Name = "Issue5"
        Me.Issue5.Size = New System.Drawing.Size(701, 22)
        Me.Issue5.TabIndex = 5
        Me.Issue5.Text = "The Windows 11 and Windows 10 installers are the same"
        '
        'IssueFix5
        '
        Me.IssueFix5.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.IssueFix5.LinkArea = New System.Windows.Forms.LinkArea(0, 32)
        Me.IssueFix5.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.IssueFix5.LinkColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.IssueFix5.Location = New System.Drawing.Point(815, 252)
        Me.IssueFix5.Name = "IssueFix5"
        Me.IssueFix5.Size = New System.Drawing.Size(73, 22)
        Me.IssueFix5.TabIndex = 6
        Me.IssueFix5.TabStop = True
        Me.IssueFix5.Text = "Fix"
        Me.IssueFix5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IssueFix5.UseCompatibleTextRendering = True
        '
        'IssuePic5
        '
        Me.IssuePic5.Image = Global.Windows_11_Manual_Installer.My.Resources.Resources.cross_light
        Me.IssuePic5.Location = New System.Drawing.Point(76, 251)
        Me.IssuePic5.Name = "IssuePic5"
        Me.IssuePic5.Size = New System.Drawing.Size(24, 24)
        Me.IssuePic5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.IssuePic5.TabIndex = 8
        Me.IssuePic5.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Windows_11_Manual_Installer.My.Resources.Resources.instissues
        Me.PictureBox1.Location = New System.Drawing.Point(76, 83)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(64, 64)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'InstallerIssuesPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(960, 600)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.IssuePic5)
        Me.Controls.Add(Me.IssuePic4)
        Me.Controls.Add(Me.IssuePic3)
        Me.Controls.Add(Me.IssuePic2)
        Me.Controls.Add(Me.IssueFix5)
        Me.Controls.Add(Me.IssuePic1)
        Me.Controls.Add(Me.IssueFix4)
        Me.Controls.Add(Me.IssueFix3)
        Me.Controls.Add(Me.IssueFix2)
        Me.Controls.Add(Me.IssueFix1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Issue5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Issue4)
        Me.Controls.Add(Me.Issue3)
        Me.Controls.Add(Me.Issue2)
        Me.Controls.Add(Me.Issue1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InstallerIssuesPanel"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "InstallerIssuesPanel"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.InvalidTextFieldPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IssuePic1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IssuePic2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IssuePic3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IssuePic4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IssuePic5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Issue1 As System.Windows.Forms.Label
    Friend WithEvents Issue2 As System.Windows.Forms.Label
    Friend WithEvents Issue3 As System.Windows.Forms.Label
    Friend WithEvents Issue4 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents IssueFix1 As System.Windows.Forms.LinkLabel
    Friend WithEvents IssueFix2 As System.Windows.Forms.LinkLabel
    Friend WithEvents IssueFix3 As System.Windows.Forms.LinkLabel
    Friend WithEvents IssueFix4 As System.Windows.Forms.LinkLabel
    Friend WithEvents IssuePic1 As System.Windows.Forms.PictureBox
    Friend WithEvents IssuePic2 As System.Windows.Forms.PictureBox
    Friend WithEvents IssuePic3 As System.Windows.Forms.PictureBox
    Friend WithEvents IssuePic4 As System.Windows.Forms.PictureBox
    Friend WithEvents InvalidTextFieldPic As System.Windows.Forms.PictureBox
    Friend WithEvents Issue5 As System.Windows.Forms.Label
    Friend WithEvents IssueFix5 As System.Windows.Forms.LinkLabel
    Friend WithEvents IssuePic5 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

End Class
