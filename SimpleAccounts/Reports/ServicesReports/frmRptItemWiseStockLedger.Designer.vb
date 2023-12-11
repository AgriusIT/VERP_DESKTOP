<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptItemWiseStockLedger
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptItemWiseStockLedger))
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtpTo = New System.Windows.Forms.DateTimePicker
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo
        Me.btnShow = New System.Windows.Forms.Button
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnReset = New System.Windows.Forms.ToolStripButton
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbCustomer = New Infragistics.Win.UltraWinGrid.UltraCombo
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Date From"
        '
        'dtpFrom
        '
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(92, 34)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(282, 20)
        Me.dtpFrom.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Item"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Date To"
        '
        'dtpTo
        '
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(92, 60)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(282, 20)
        Me.dtpTo.TabIndex = 4
        '
        'cmbItem
        '
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        Me.cmbItem.Location = New System.Drawing.Point(92, 114)
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(282, 22)
        Me.cmbItem.TabIndex = 8
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(299, 142)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 23)
        Me.btnShow.TabIndex = 9
        Me.btnShow.Text = "&Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnReset})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(419, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnReset
        '
        Me.btnReset.Image = CType(resources.GetObject("btnReset.Image"), System.Drawing.Image)
        Me.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(55, 22)
        Me.btnReset.Text = "Reset"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(30, 86)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Customer"
        '
        'cmbCustomer
        '
        Me.cmbCustomer.CheckedListSettings.CheckStateMember = ""
        Me.cmbCustomer.Location = New System.Drawing.Point(92, 86)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(282, 22)
        Me.cmbCustomer.TabIndex = 6
        '
        'frmRptItemWiseStockLedger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(419, 183)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbCustomer)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.btnShow)
        Me.Controls.Add(Me.cmbItem)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRptItemWiseStockLedger"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Item Wise Stock Ledger"
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnReset As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbCustomer As Infragistics.Win.UltraWinGrid.UltraCombo
End Class
