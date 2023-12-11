<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReturnStoreIssuenceReport
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Combination")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Price", 0)
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Stock", 1)
        Dim UltraGridColumn13 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("PurchasePrice", 2)
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.cmbEmployee = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblEmployee = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.cmbLocation = New System.Windows.Forms.ComboBox()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        CType(Me.cmbEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbEmployee
        '
        Me.cmbEmployee.AlwaysInEditMode = True
        Me.cmbEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbEmployee.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbEmployee.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.Color.White
        Appearance1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbEmployee.DisplayLayout.Appearance = Appearance1
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn3.Width = 70
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn4.Width = 80
        UltraGridColumn5.Header.VisiblePosition = 4
        UltraGridColumn5.MaxWidth = 80
        UltraGridColumn6.Header.VisiblePosition = 5
        UltraGridColumn6.MaxWidth = 60
        UltraGridColumn13.Header.VisiblePosition = 6
        UltraGridColumn13.Hidden = True
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6, UltraGridColumn13})
        Me.cmbEmployee.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbEmployee.DisplayLayout.InterBandSpacing = 10
        Me.cmbEmployee.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbEmployee.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbEmployee.DisplayLayout.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.[True]
        Me.cmbEmployee.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.Color.Transparent
        Me.cmbEmployee.DisplayLayout.Override.CardAreaAppearance = Appearance2
        Me.cmbEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbEmployee.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance3.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance3.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance3.ForeColor = System.Drawing.Color.White
        Appearance3.TextHAlignAsString = "Left"
        Appearance3.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbEmployee.DisplayLayout.Override.HeaderAppearance = Appearance3
        Me.cmbEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance6.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbEmployee.DisplayLayout.Override.RowAppearance = Appearance6
        Appearance12.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance12.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbEmployee.DisplayLayout.Override.RowSelectorAppearance = Appearance12
        Me.cmbEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbEmployee.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbEmployee.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance18.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance18.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance18.ForeColor = System.Drawing.Color.Black
        Me.cmbEmployee.DisplayLayout.Override.SelectedRowAppearance = Appearance18
        Me.cmbEmployee.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbEmployee.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbEmployee.DisplayLayout.Override.SelectTypeGroupByRow = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbEmployee.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended
        Me.cmbEmployee.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbEmployee.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbEmployee.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell
        Me.cmbEmployee.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbEmployee.LimitToList = True
        Me.cmbEmployee.Location = New System.Drawing.Point(146, 75)
        Me.cmbEmployee.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbEmployee.MaxDropDownItems = 20
        Me.cmbEmployee.Name = "cmbEmployee"
        Me.cmbEmployee.Size = New System.Drawing.Size(327, 29)
        Me.cmbEmployee.TabIndex = 5
        '
        'lblEmployee
        '
        Me.lblEmployee.AutoSize = True
        Me.lblEmployee.Location = New System.Drawing.Point(45, 82)
        Me.lblEmployee.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEmployee.Name = "lblEmployee"
        Me.lblEmployee.Size = New System.Drawing.Size(91, 20)
        Me.lblEmployee.TabIndex = 6
        Me.lblEmployee.Text = "Employee : "
        '
        'dtpFrom
        '
        Me.dtpFrom.Location = New System.Drawing.Point(146, 118)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(325, 26)
        Me.dtpFrom.TabIndex = 7
        '
        'dtpTo
        '
        Me.dtpTo.Location = New System.Drawing.Point(146, 158)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(325, 26)
        Me.dtpTo.TabIndex = 8
        '
        'cmbLocation
        '
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Location = New System.Drawing.Point(146, 198)
        Me.cmbLocation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(325, 28)
        Me.cmbLocation.TabIndex = 9
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(45, 123)
        Me.lblFrom.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(58, 20)
        Me.lblFrom.TabIndex = 10
        Me.lblFrom.Text = "From : "
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(45, 162)
        Me.lblTo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(39, 20)
        Me.lblTo.TabIndex = 11
        Me.lblTo.Text = "To : "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(45, 203)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 20)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Location :"
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(340, 240)
        Me.btnGenerate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(132, 35)
        Me.btnGenerate.TabIndex = 13
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(4, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(314, 36)
        Me.lblHeader.TabIndex = 14
        Me.lblHeader.Text = "Return Store Issuence"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(502, 66)
        Me.pnlHeader.TabIndex = 15
        '
        'frmReturnStoreIssuenceReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(502, 305)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblTo)
        Me.Controls.Add(Me.lblFrom)
        Me.Controls.Add(Me.cmbLocation)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.lblEmployee)
        Me.Controls.Add(Me.cmbEmployee)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmReturnStoreIssuenceReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Return Store Issuence Report"
        CType(Me.cmbEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbEmployee As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblEmployee As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class
