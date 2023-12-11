<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddInventoryDept
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddInventoryDept))
        Me.cmbDept = New System.Windows.Forms.ComboBox
        Me.txtMainCode = New System.Windows.Forms.TextBox
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.txtInventoryDept = New System.Windows.Forms.TextBox
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.chkSalesItem = New System.Windows.Forms.CheckBox
        Me.chkServiceItem = New System.Windows.Forms.CheckBox
        Me.lblProgress = New System.Windows.Forms.Label
        Me.txtDeptCode = New System.Windows.Forms.TextBox
        Me.lblCode = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cmbDept
        '
        Me.cmbDept.FormattingEnabled = True
        Me.cmbDept.Location = New System.Drawing.Point(93, 21)
        Me.cmbDept.Name = "cmbDept"
        Me.cmbDept.Size = New System.Drawing.Size(268, 21)
        Me.cmbDept.TabIndex = 1
        '
        'txtMainCode
        '
        Me.txtMainCode.Location = New System.Drawing.Point(8, 128)
        Me.txtMainCode.Name = "txtMainCode"
        Me.txtMainCode.Size = New System.Drawing.Size(18, 20)
        Me.txtMainCode.TabIndex = 8
        Me.txtMainCode.TabStop = False
        Me.txtMainCode.Visible = False
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(8, 151)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(18, 20)
        Me.txtCode.TabIndex = 9
        Me.txtCode.TabStop = False
        Me.txtCode.Visible = False
        '
        'txtInventoryDept
        '
        Me.txtInventoryDept.Location = New System.Drawing.Point(93, 48)
        Me.txtInventoryDept.Name = "txtInventoryDept"
        Me.txtInventoryDept.Size = New System.Drawing.Size(268, 20)
        Me.txtInventoryDept.TabIndex = 3
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(205, 101)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 10
        Me.btnAdd.Text = "Save"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(286, 101)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "Reset"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Department"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Sub Sub A / c"
        '
        'chkSalesItem
        '
        Me.chkSalesItem.AutoSize = True
        Me.chkSalesItem.Checked = True
        Me.chkSalesItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSalesItem.Location = New System.Drawing.Point(185, 76)
        Me.chkSalesItem.Name = "chkSalesItem"
        Me.chkSalesItem.Size = New System.Drawing.Size(75, 17)
        Me.chkSalesItem.TabIndex = 6
        Me.chkSalesItem.TabStop = False
        Me.chkSalesItem.Text = "Sales Item"
        Me.chkSalesItem.UseVisualStyleBackColor = True
        '
        'chkServiceItem
        '
        Me.chkServiceItem.AutoSize = True
        Me.chkServiceItem.Location = New System.Drawing.Point(266, 76)
        Me.chkServiceItem.Name = "chkServiceItem"
        Me.chkServiceItem.Size = New System.Drawing.Size(85, 17)
        Me.chkServiceItem.TabIndex = 7
        Me.chkServiceItem.TabStop = False
        Me.chkServiceItem.Text = "Service Item"
        Me.chkServiceItem.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(94, 28)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 12
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'txtDeptCode
        '
        Me.txtDeptCode.Location = New System.Drawing.Point(93, 74)
        Me.txtDeptCode.Name = "txtDeptCode"
        Me.txtDeptCode.Size = New System.Drawing.Size(86, 20)
        Me.txtDeptCode.TabIndex = 5
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.Location = New System.Drawing.Point(12, 77)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(32, 13)
        Me.lblCode.TabIndex = 4
        Me.lblCode.Text = "Code"
        '
        'AddInventoryDept
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(369, 136)
        Me.Controls.Add(Me.lblCode)
        Me.Controls.Add(Me.txtDeptCode)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.chkServiceItem)
        Me.Controls.Add(Me.chkSalesItem)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtInventoryDept)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.txtMainCode)
        Me.Controls.Add(Me.cmbDept)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddInventoryDept"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Inventory Department"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbDept As System.Windows.Forms.ComboBox
    Friend WithEvents txtMainCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents txtInventoryDept As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkSalesItem As System.Windows.Forms.CheckBox
    Friend WithEvents chkServiceItem As System.Windows.Forms.CheckBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents txtDeptCode As System.Windows.Forms.TextBox
    Friend WithEvents lblCode As System.Windows.Forms.Label
End Class
