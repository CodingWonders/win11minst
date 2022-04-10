<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LogExistsPanel
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Yes_Button = New System.Windows.Forms.Button()
        Me.No_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Yes_Button)
        Me.Panel1.Controls.Add(Me.No_Button)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 183)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(873, 80)
        Me.Panel1.TabIndex = 3
        '
        'Yes_Button
        '
        Me.Yes_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Yes_Button.BackColor = System.Drawing.Color.DodgerBlue
        Me.Yes_Button.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue
        Me.Yes_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Yes_Button.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Yes_Button.ForeColor = System.Drawing.Color.White
        Me.Yes_Button.Location = New System.Drawing.Point(244, 24)
        Me.Yes_Button.Name = "Yes_Button"
        Me.Yes_Button.Size = New System.Drawing.Size(300, 32)
        Me.Yes_Button.TabIndex = 2
        Me.Yes_Button.Text = "Append to existing log file"
        Me.Yes_Button.UseVisualStyleBackColor = False
        '
        'No_Button
        '
        Me.No_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.No_Button.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.No_Button.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.No_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.No_Button.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.No_Button.ForeColor = System.Drawing.Color.Black
        Me.No_Button.Location = New System.Drawing.Point(550, 24)
        Me.No_Button.Name = "No_Button"
        Me.No_Button.Size = New System.Drawing.Size(300, 32)
        Me.No_Button.TabIndex = 3
        Me.No_Button.Text = "Delete existing log file"
        Me.No_Button.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(26, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(207, 26)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "A log file already exists"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(71, 86)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(730, 72)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Do you want to append the current log contents to the log file, or do you want to" & _
    " delete the existing log file?"
        '
        'LogExistsPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(873, 263)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LogExistsPanel"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "A log file already exists"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Yes_Button As System.Windows.Forms.Button
    Friend WithEvents No_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
