<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSMSTemplate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSMSTemplate))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmbKey = New System.Windows.Forms.ComboBox()
        Me.lstParameters = New System.Windows.Forms.ListBox()
        Me.txtSMSTemplate = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnSave, Me.toolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(555, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "&New"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'cmbKey
        '
        Me.cmbKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKey.FormattingEnabled = True
        Me.cmbKey.Items.AddRange(New Object() {"Bank Payment", "Cash Payment", "Bank Receipt", "Cash Receipt", "Expense", "Invoice Based Bank Payment", "Invoice Based  BankReceipt", "Invoice Based Cash Receipt", "Invoice Based Cash Payment", "Journal Voucher", "Sales Order", "Sale Invoice", "Delivery Chalan", "Purchase Order", "Purchase Invoice", "Receiving Note", "Dispatch Stock", "Receiving Stock", "Store Issuance", "Stock Adjustment", "CMFA Document", "Inward / Outward Gate Pass"})
        Me.cmbKey.Location = New System.Drawing.Point(343, 33)
        Me.cmbKey.Name = "cmbKey"
        Me.cmbKey.Size = New System.Drawing.Size(182, 21)
        Me.cmbKey.TabIndex = 2
        '
        'lstParameters
        '
        Me.lstParameters.FormattingEnabled = True
        Me.lstParameters.Items.AddRange(New Object() {"@AccountCode", "@AccountTitle", "@DocumentNo", "@DocumentDate", "@OtherDocNo", "@Remarks", "@Amount", "@Quantity", "@ChequeNo", "@ChequeDate", "@CompanyName", "@CellNo", "@SIRIUS"})
        Me.lstParameters.Location = New System.Drawing.Point(343, 72)
        Me.lstParameters.Name = "lstParameters"
        Me.lstParameters.Size = New System.Drawing.Size(182, 212)
        Me.lstParameters.TabIndex = 4
        '
        'txtSMSTemplate
        '
        Me.txtSMSTemplate.Location = New System.Drawing.Point(6, 19)
        Me.txtSMSTemplate.Multiline = True
        Me.txtSMSTemplate.Name = "txtSMSTemplate"
        Me.txtSMSTemplate.Size = New System.Drawing.Size(331, 270)
        Me.txtSMSTemplate.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtSMSTemplate)
        Me.GroupBox1.Controls.Add(Me.cmbKey)
        Me.GroupBox1.Controls.Add(Me.lstParameters)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 40)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(531, 297)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Template Setting"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(340, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Parameters"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(343, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Key"
        '
        'frmSMSTemplate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 349)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSMSTemplate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SMS Template"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmbKey As System.Windows.Forms.ComboBox
    Friend WithEvents lstParameters As System.Windows.Forms.ListBox
    Friend WithEvents txtSMSTemplate As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
End Class
