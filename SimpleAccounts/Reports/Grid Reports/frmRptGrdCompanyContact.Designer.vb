<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptGrdCompanyContact
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
        Me.btnShow = New System.Windows.Forms.Button
        Me.cmbAlphabets = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbSpecificCustomer = New Infragistics.Win.UltraWinGrid.UltraCombo
        Me.txtIndexNo = New System.Windows.Forms.TextBox
        Me.cmbTypes = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnReset = New System.Windows.Forms.ToolStripButton
        Me.rbtnAll = New System.Windows.Forms.RadioButton
        Me.rbtnSelective = New System.Windows.Forms.RadioButton
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbSpecificCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(235, 218)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(92, 23)
        Me.btnShow.TabIndex = 2
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'cmbAlphabets
        '
        Me.cmbAlphabets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlphabets.FormattingEnabled = True
        Me.cmbAlphabets.Items.AddRange(New Object() {"--- Select Any Value ---", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.cmbAlphabets.Location = New System.Drawing.Point(113, 19)
        Me.cmbAlphabets.Name = "cmbAlphabets"
        Me.cmbAlphabets.Size = New System.Drawing.Size(202, 21)
        Me.cmbAlphabets.TabIndex = 4
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbSpecificCustomer)
        Me.GroupBox1.Controls.Add(Me.txtIndexNo)
        Me.GroupBox1.Controls.Add(Me.cmbTypes)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbAlphabets)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 72)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(335, 140)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Criteria"
        '
        'cmbSpecificCustomer
        '
        Me.cmbSpecificCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbSpecificCustomer.CheckedListSettings.CheckStateMember = ""
        Me.cmbSpecificCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbSpecificCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSpecificCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbSpecificCustomer.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbSpecificCustomer.DisplayLayout.MaxRowScrollRegions = 1
        Me.cmbSpecificCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmbSpecificCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmbSpecificCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmbSpecificCustomer.DisplayLayout.Override.CellPadding = 0
        Me.cmbSpecificCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbSpecificCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Me.cmbSpecificCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSpecificCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbSpecificCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbSpecificCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbSpecificCustomer.Location = New System.Drawing.Point(113, 99)
        Me.cmbSpecificCustomer.Name = "cmbSpecificCustomer"
        Me.cmbSpecificCustomer.Size = New System.Drawing.Size(202, 22)
        Me.cmbSpecificCustomer.TabIndex = 6
        '
        'txtIndexNo
        '
        Me.txtIndexNo.Location = New System.Drawing.Point(113, 46)
        Me.txtIndexNo.Name = "txtIndexNo"
        Me.txtIndexNo.ReadOnly = True
        Me.txtIndexNo.Size = New System.Drawing.Size(202, 20)
        Me.txtIndexNo.TabIndex = 10
        '
        'cmbTypes
        '
        Me.cmbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTypes.FormattingEnabled = True
        Me.cmbTypes.Items.AddRange(New Object() {"--- Select Any Value ---", "Customer", "Vendor", "Bank", "Clearing Agent", "Travel Agent", "Friends & Family", "Others"})
        Me.cmbTypes.Location = New System.Drawing.Point(113, 72)
        Me.cmbTypes.Name = "cmbTypes"
        Me.cmbTypes.Size = New System.Drawing.Size(202, 21)
        Me.cmbTypes.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Type"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Specific Cutomer"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Index No"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Alphabetically"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnReset})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(366, 25)
        Me.ToolStrip1.TabIndex = 6
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnReset
        '
        Me.btnReset.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(55, 22)
        Me.btnReset.Text = "&Reset"
        '
        'rbtnAll
        '
        Me.rbtnAll.AutoSize = True
        Me.rbtnAll.Checked = True
        Me.rbtnAll.Location = New System.Drawing.Point(124, 49)
        Me.rbtnAll.Name = "rbtnAll"
        Me.rbtnAll.Size = New System.Drawing.Size(36, 17)
        Me.rbtnAll.TabIndex = 11
        Me.rbtnAll.TabStop = True
        Me.rbtnAll.Text = "All"
        Me.rbtnAll.UseVisualStyleBackColor = True
        '
        'rbtnSelective
        '
        Me.rbtnSelective.AutoSize = True
        Me.rbtnSelective.Location = New System.Drawing.Point(188, 49)
        Me.rbtnSelective.Name = "rbtnSelective"
        Me.rbtnSelective.Size = New System.Drawing.Size(69, 17)
        Me.rbtnSelective.TabIndex = 12
        Me.rbtnSelective.Text = "Selective"
        Me.rbtnSelective.UseVisualStyleBackColor = True
        '
        'frmRptGrdCompanyContact
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(366, 254)
        Me.Controls.Add(Me.rbtnSelective)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.rbtnAll)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnShow)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRptGrdCompanyContact"
        Me.Text = "Report Company Contact"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbSpecificCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents cmbAlphabets As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbTypes As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIndexNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbSpecificCustomer As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnReset As System.Windows.Forms.ToolStripButton
    Friend WithEvents rbtnSelective As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnAll As System.Windows.Forms.RadioButton
End Class
