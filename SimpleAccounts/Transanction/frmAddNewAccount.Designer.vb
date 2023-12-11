<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddNewAccount
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.txtAccountName = New System.Windows.Forms.TextBox()
        Me.cmbSubSubAc = New System.Windows.Forms.ComboBox()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblAcName = New System.Windows.Forms.Label()
        Me.lblSubSubac = New System.Windows.Forms.Label()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtAccountName
        '
        Me.txtAccountName.Location = New System.Drawing.Point(107, 96)
        Me.txtAccountName.Name = "txtAccountName"
        Me.txtAccountName.Size = New System.Drawing.Size(300, 21)
        Me.txtAccountName.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtAccountName, "Account Name")
        '
        'cmbSubSubAc
        '
        Me.cmbSubSubAc.FormattingEnabled = True
        Me.cmbSubSubAc.Location = New System.Drawing.Point(107, 69)
        Me.cmbSubSubAc.Name = "cmbSubSubAc"
        Me.cmbSubSubAc.Size = New System.Drawing.Size(300, 21)
        Me.cmbSubSubAc.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.cmbSubSubAc, "Select Any Account Head")
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(3, 8)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(200, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Add New Account"
        '
        'lblAcName
        '
        Me.lblAcName.AutoSize = True
        Me.lblAcName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcName.Location = New System.Drawing.Point(12, 99)
        Me.lblAcName.Name = "lblAcName"
        Me.lblAcName.Size = New System.Drawing.Size(94, 13)
        Me.lblAcName.TabIndex = 5
        Me.lblAcName.Text = "Account Name:"
        '
        'lblSubSubac
        '
        Me.lblSubSubac.AutoSize = True
        Me.lblSubSubac.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubSubac.Location = New System.Drawing.Point(12, 72)
        Me.lblSubSubac.Name = "lblSubSubac"
        Me.lblSubSubac.Size = New System.Drawing.Size(86, 13)
        Me.lblSubSubac.TabIndex = 3
        Me.lblSubSubac.Text = "Sub Sub A/C:"
        '
        'BtnSave
        '
        Me.BtnSave.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.Location = New System.Drawing.Point(332, 123)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(75, 23)
        Me.BtnSave.TabIndex = 7
        Me.BtnSave.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.BtnSave, "Save")
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'cmbType
        '
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Items.AddRange(New Object() {"General", "Cash", "Bank", "Customer", "Vendor", "Expense", "Inventory"})
        Me.cmbType.Location = New System.Drawing.Point(107, 42)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(300, 21)
        Me.cmbType.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.cmbType, "Select Account Type")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Account Type:"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(112, 72)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 17
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(420, 38)
        Me.pnlHeader.TabIndex = 23
        '
        'frmAddNewAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(420, 157)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbType)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.lblSubSubac)
        Me.Controls.Add(Me.lblAcName)
        Me.Controls.Add(Me.cmbSubSubAc)
        Me.Controls.Add(Me.txtAccountName)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmAddNewAccount"
        Me.Text = "Add New Account"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtAccountName As System.Windows.Forms.TextBox
    Friend WithEvents cmbSubSubAc As System.Windows.Forms.ComboBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents lblAcName As System.Windows.Forms.Label
    Friend WithEvents lblSubSubac As System.Windows.Forms.Label
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class
