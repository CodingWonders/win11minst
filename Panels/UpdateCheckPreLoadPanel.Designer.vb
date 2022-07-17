<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UpdateCheckPreLoadPanel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UpdateCheckPreLoadPanel))
        Me.ProgressRingPic = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UpdateCancelButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RelNotesLink = New System.Windows.Forms.LinkLabel()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.closeBox = New System.Windows.Forms.PictureBox()
        Me.closeToolTip = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.ProgressRingPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.closeBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ProgressRingPic
        '
        Me.ProgressRingPic.Image = Global.Windows_11_Manual_Installer.My.Resources.Resources.progressring_indeterminate
        Me.ProgressRingPic.Location = New System.Drawing.Point(12, 7)
        Me.ProgressRingPic.Name = "ProgressRingPic"
        Me.ProgressRingPic.Size = New System.Drawing.Size(42, 50)
        Me.ProgressRingPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ProgressRingPic.TabIndex = 15
        Me.ProgressRingPic.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(53, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(158, 20)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Checking for updates..."
        '
        'UpdateCancelButton
        '
        Me.UpdateCancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UpdateCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.UpdateCancelButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.UpdateCancelButton.Location = New System.Drawing.Point(396, 20)
        Me.UpdateCancelButton.Name = "UpdateCancelButton"
        Me.UpdateCancelButton.Size = New System.Drawing.Size(125, 25)
        Me.UpdateCancelButton.TabIndex = 17
        Me.UpdateCancelButton.Text = "Cancel"
        Me.UpdateCancelButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RelNotesLink)
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(508, 242)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Version information"
        '
        'RelNotesLink
        '
        Me.RelNotesLink.AutoEllipsis = True
        Me.RelNotesLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.RelNotesLink.LinkColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(92, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.RelNotesLink.Location = New System.Drawing.Point(268, 181)
        Me.RelNotesLink.Name = "RelNotesLink"
        Me.RelNotesLink.Size = New System.Drawing.Size(217, 23)
        Me.RelNotesLink.TabIndex = 18
        Me.RelNotesLink.TabStop = True
        Me.RelNotesLink.Text = "View release notes"
        Me.RelNotesLink.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(268, 55)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(217, 120)
        Me.TextBox2.TabIndex = 2
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(24, 55)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(217, 120)
        Me.TextBox1.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(264, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(138, 20)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Up-to-date version:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(3, 207)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(502, 32)
        Me.Panel1.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Button2.Location = New System.Drawing.Point(243, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(125, 25)
        Me.Button2.TabIndex = 17
        Me.Button2.Text = "Install now"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Button1.Location = New System.Drawing.Point(374, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(125, 25)
        Me.Button1.TabIndex = 17
        Me.Button1.Text = "Install later"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(7, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 20)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Actions:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(21, 181)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(209, 20)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Version channel: hummingbird"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 20)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Your version:"
        '
        'closeBox
        '
        Me.closeBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.closeBox.Image = CType(resources.GetObject("closeBox.Image"), System.Drawing.Image)
        Me.closeBox.Location = New System.Drawing.Point(486, 0)
        Me.closeBox.Name = "closeBox"
        Me.closeBox.Size = New System.Drawing.Size(47, 32)
        Me.closeBox.TabIndex = 19
        Me.closeBox.TabStop = False
        Me.closeBox.Visible = False
        '
        'UpdateCheckPreLoadPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(533, 64)
        Me.Controls.Add(Me.closeBox)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.UpdateCancelButton)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ProgressRingPic)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UpdateCheckPreLoadPanel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Passive Update Check System - Checking for updates..."
        CType(Me.ProgressRingPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.closeBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ProgressRingPic As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UpdateCancelButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents closeBox As System.Windows.Forms.PictureBox
    Friend WithEvents closeToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents RelNotesLink As System.Windows.Forms.LinkLabel

End Class
