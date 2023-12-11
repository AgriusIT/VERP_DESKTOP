<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPurchaseOrderList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPurchaseOrderList))
        Me.cmbPurchaseOrder = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmbPurchaseOrder
        '
        Me.cmbPurchaseOrder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbPurchaseOrder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPurchaseOrder.FormattingEnabled = True
        Me.cmbPurchaseOrder.Location = New System.Drawing.Point(120, 40)
        Me.cmbPurchaseOrder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPurchaseOrder.Name = "cmbPurchaseOrder"
        Me.cmbPurchaseOrder.Size = New System.Drawing.Size(350, 28)
        Me.cmbPurchaseOrder.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 45)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Order No."
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(360, 120)
        Me.btnLoad.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(112, 35)
        Me.btnLoad.TabIndex = 2
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(92, 40)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(394, 69)
        Me.lblProgress.TabIndex = 14
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'frmPurchaseOrderList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(490, 174)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbPurchaseOrder)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPurchaseOrderList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Purchase Order List"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbPurchaseOrder As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents lblProgress As System.Windows.Forms.Label
End Class
