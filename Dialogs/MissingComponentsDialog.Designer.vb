<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MissingComponentsDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MissingComponentsDialog))
        Me.closeBox = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.closeToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CheckPic1 = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.CheckPic2 = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckPic3 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.closeBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckPic1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckPic2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckPic3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'closeBox
        '
        Me.closeBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.closeBox.Image = CType(resources.GetObject("closeBox.Image"), System.Drawing.Image)
        Me.closeBox.Location = New System.Drawing.Point(553, 0)
        Me.closeBox.Name = "closeBox"
        Me.closeBox.Size = New System.Drawing.Size(46, 32)
        Me.closeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.closeBox.TabIndex = 20
        Me.closeBox.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 15)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Missing components"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(84, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(490, 102)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = resources.GetString("Label2.Text")
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Windows_11_Manual_Installer_2._0.My.Resources.Resources.components
        Me.PictureBox1.Location = New System.Drawing.Point(30, 48)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(48, 48)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 23
        Me.PictureBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(27, 150)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 15)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Component name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(407, 150)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 15)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Status"
        '
        'CheckPic1
        '
        Me.CheckPic1.Image = Global.Windows_11_Manual_Installer_2._0.My.Resources.Resources.check
        Me.CheckPic1.Location = New System.Drawing.Point(483, 175)
        Me.CheckPic1.Name = "CheckPic1"
        Me.CheckPic1.Size = New System.Drawing.Size(24, 24)
        Me.CheckPic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.CheckPic1.TabIndex = 25
        Me.CheckPic1.TabStop = False
        Me.CheckPic1.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(27, 179)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 15)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "7-Zip"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Button2.Location = New System.Drawing.Point(410, 175)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(171, 25)
        Me.Button2.TabIndex = 24
        Me.Button2.Text = "Download and Install"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'CheckPic2
        '
        Me.CheckPic2.Image = Global.Windows_11_Manual_Installer_2._0.My.Resources.Resources.check
        Me.CheckPic2.Location = New System.Drawing.Point(483, 208)
        Me.CheckPic2.Name = "CheckPic2"
        Me.CheckPic2.Size = New System.Drawing.Size(24, 24)
        Me.CheckPic2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.CheckPic2.TabIndex = 25
        Me.CheckPic2.TabStop = False
        Me.CheckPic2.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(27, 212)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(295, 15)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Deployment Image Servicing and Management (DISM)"
        '
        'CheckPic3
        '
        Me.CheckPic3.Image = Global.Windows_11_Manual_Installer_2._0.My.Resources.Resources.check
        Me.CheckPic3.Location = New System.Drawing.Point(483, 241)
        Me.CheckPic3.Name = "CheckPic3"
        Me.CheckPic3.Size = New System.Drawing.Size(24, 24)
        Me.CheckPic3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.CheckPic3.TabIndex = 25
        Me.CheckPic3.TabStop = False
        Me.CheckPic3.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(27, 245)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 15)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "OSCDIMG"
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Button3.Location = New System.Drawing.Point(410, 241)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(171, 25)
        Me.Button3.TabIndex = 24
        Me.Button3.Text = "Download and Install"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(27, 273)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(137, 15)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Component description"
        '
        'Label9
        '
        Me.Label9.AutoEllipsis = True
        Me.Label9.Location = New System.Drawing.Point(27, 297)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(554, 41)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "Point your mouse cursor over an entry to show its description."
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(27, 361)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(236, 15)
        Me.Label10.TabIndex = 26
        Me.Label10.Text = "You may continue program execution now."
        Me.Label10.Visible = False
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Button4.Location = New System.Drawing.Point(504, 144)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(77, 25)
        Me.Button4.TabIndex = 24
        Me.Button4.Text = "Refresh"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(27, 338)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(285, 15)
        Me.Label11.TabIndex = 27
        Me.Label11.Text = "Please extract the downloaded file to the ""bin"" folder"
        Me.Label11.Visible = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Button1.Location = New System.Drawing.Point(461, 355)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(120, 25)
        Me.Button1.TabIndex = 24
        Me.Button1.Text = "Launch"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'MissingComponentsDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(600, 400)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.CheckPic3)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.CheckPic2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CheckPic1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.closeBox)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MissingComponentsDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Missing components"
        CType(Me.closeBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckPic1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckPic2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckPic3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents closeBox As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents closeToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CheckPic1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents CheckPic2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CheckPic3 As System.Windows.Forms.PictureBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
