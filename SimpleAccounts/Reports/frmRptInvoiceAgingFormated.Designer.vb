<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptInvoiceAgingFormated
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Account")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Code")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Type")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City", 0)
        Dim RowLayout1 As Infragistics.Win.UltraWinGrid.RowLayout = New Infragistics.Win.UltraWinGrid.RowLayout("cmbAccountLegendLedger")
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.cmbAccounts = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.btnShow)
        Me.GroupBox1.Controls.Add(Me.cmbAccounts)
        Me.GroupBox1.Location = New System.Drawing.Point(-6, 75)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(578, 127)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Tag = ""
        Me.GroupBox1.Text = "Accounts List"
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(354, 72)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(172, 35)
        Me.btnShow.TabIndex = 1
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'cmbAccounts
        '
        Appearance1.BackColor = System.Drawing.SystemColors.Info
        Me.cmbAccounts.Appearance = Appearance1
        Me.cmbAccounts.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbAccounts.CheckedListSettings.CheckStateMember = ""
        Appearance2.BackColor = System.Drawing.Color.White
        Me.cmbAccounts.DisplayLayout.Appearance = Appearance2
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn1.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(8, 0)
        UltraGridColumn2.AutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.VisibleRows
        UltraGridColumn2.Header.Caption = "Account Description"
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(128, 0)
        UltraGridColumn3.Header.Caption = "Account Code"
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn4.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(96, 0)
        UltraGridColumn5.Header.VisiblePosition = 4
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5})
        UltraGridBand1.RowLayouts.AddRange(New Infragistics.Win.UltraWinGrid.RowLayout() {RowLayout1})
        Me.cmbAccounts.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbAccounts.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbAccounts.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbAccounts.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbAccounts.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance3.BackColor = System.Drawing.Color.Transparent
        Me.cmbAccounts.DisplayLayout.Override.CardAreaAppearance = Appearance3
        Me.cmbAccounts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbAccounts.DisplayLayout.Override.CellPadding = 3
        Me.cmbAccounts.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance4.TextHAlignAsString = "Left"
        Me.cmbAccounts.DisplayLayout.Override.HeaderAppearance = Appearance4
        Me.cmbAccounts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance5.BorderColor = System.Drawing.Color.LightGray
        Appearance5.TextVAlignAsString = "Middle"
        Me.cmbAccounts.DisplayLayout.Override.RowAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance6.BorderColor = System.Drawing.Color.Black
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.cmbAccounts.DisplayLayout.Override.SelectedRowAppearance = Appearance6
        Me.cmbAccounts.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbAccounts.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbAccounts.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbAccounts.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbAccounts.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbAccounts.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbAccounts.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbAccounts.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbAccounts.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005
        Me.cmbAccounts.LimitToList = True
        Me.cmbAccounts.Location = New System.Drawing.Point(9, 29)
        Me.cmbAccounts.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbAccounts.MaxDropDownItems = 16
        Me.cmbAccounts.Name = "cmbAccounts"
        Me.cmbAccounts.Size = New System.Drawing.Size(518, 29)
        Me.cmbAccounts.TabIndex = 0
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(572, 65)
        Me.pnlHeader.TabIndex = 113
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(4, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(223, 41)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Invoice Aging"
        '
        'frmRptInvoiceAgingFormated
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(572, 217)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRptInvoiceAgingFormated"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Invoice Aging"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbAccounts As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
End Class
