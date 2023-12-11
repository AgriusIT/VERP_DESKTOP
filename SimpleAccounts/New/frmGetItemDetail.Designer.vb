<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGetItemDetail
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtTotalAmount = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtRate = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtTotalQty = New System.Windows.Forms.TextBox
        Me.txtQty = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtPackQty = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbUnit = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(414, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "&Refresh"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtTotalAmount)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtRate)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtTotalQty)
        Me.GroupBox1.Controls.Add(Me.txtQty)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtPackQty)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbUnit)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbItem)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(390, 224)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 187)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Total Amount"
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.Location = New System.Drawing.Point(98, 184)
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.Size = New System.Drawing.Size(121, 21)
        Me.txtTotalAmount.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(59, 160)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(33, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Rate"
        '
        'txtRate
        '
        Me.txtRate.Location = New System.Drawing.Point(98, 157)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(77, 21)
        Me.txtRate.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(33, 133)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Total Qty"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(65, 106)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Qty"
        '
        'txtTotalQty
        '
        Me.txtTotalQty.Location = New System.Drawing.Point(98, 130)
        Me.txtTotalQty.Name = "txtTotalQty"
        Me.txtTotalQty.Size = New System.Drawing.Size(77, 21)
        Me.txtTotalQty.TabIndex = 9
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(98, 103)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(77, 21)
        Me.txtQty.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(34, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Pack Qty"
        '
        'txtPackQty
        '
        Me.txtPackQty.Location = New System.Drawing.Point(98, 76)
        Me.txtPackQty.Name = "txtPackQty"
        Me.txtPackQty.Size = New System.Drawing.Size(77, 21)
        Me.txtPackQty.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(63, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Unit"
        '
        'cmbUnit
        '
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.FormattingEnabled = True
        Me.cmbUnit.Location = New System.Drawing.Point(98, 49)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.Size = New System.Drawing.Size(188, 21)
        Me.cmbUnit.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(58, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Item"
        '
        'cmbItem
        '
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        Me.cmbItem.Location = New System.Drawing.Point(98, 20)
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(286, 23)
        Me.cmbItem.TabIndex = 1
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(321, 283)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 3
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(240, 283)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 2
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'frmGetItemDetail
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(414, 318)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGetItemDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Item"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTotalQty As System.Windows.Forms.TextBox
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPackQty As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTotalAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
End Class
