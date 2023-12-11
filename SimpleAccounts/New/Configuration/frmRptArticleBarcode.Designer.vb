<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptArticleBarcode
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
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Combination")
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Price", 0)
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Stock", 1)
        Dim UltraGridColumn13 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("PurchasePrice", 2)
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptArticleBarcode))
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.RdoCode = New System.Windows.Forms.RadioButton()
        Me.rdoName = New System.Windows.Forms.RadioButton()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlInvType = New System.Windows.Forms.Panel()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInvType.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbItem
        '
        Me.cmbItem.AlwaysInEditMode = True
        Me.cmbItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        Appearance6.BackColor = System.Drawing.Color.White
        Appearance6.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbItem.DisplayLayout.Appearance = Appearance6
        UltraGridColumn7.Header.VisiblePosition = 0
        UltraGridColumn7.Hidden = True
        UltraGridColumn8.Header.VisiblePosition = 1
        UltraGridColumn9.Header.VisiblePosition = 2
        UltraGridColumn9.Width = 70
        UltraGridColumn10.Header.VisiblePosition = 3
        UltraGridColumn10.Width = 80
        UltraGridColumn11.Header.VisiblePosition = 4
        UltraGridColumn11.MaxWidth = 80
        UltraGridColumn12.Header.VisiblePosition = 5
        UltraGridColumn12.MaxWidth = 60
        UltraGridColumn13.Header.VisiblePosition = 6
        UltraGridColumn13.Hidden = True
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn7, UltraGridColumn8, UltraGridColumn9, UltraGridColumn10, UltraGridColumn11, UltraGridColumn12, UltraGridColumn13})
        Me.cmbItem.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbItem.DisplayLayout.InterBandSpacing = 10
        Me.cmbItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbItem.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbItem.DisplayLayout.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.[True]
        Me.cmbItem.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance7.BackColor = System.Drawing.Color.Transparent
        Me.cmbItem.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Me.cmbItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbItem.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance8.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance8.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance8.ForeColor = System.Drawing.Color.White
        Appearance8.TextHAlignAsString = "Left"
        Appearance8.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbItem.DisplayLayout.Override.HeaderAppearance = Appearance8
        Me.cmbItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance9.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem.DisplayLayout.Override.RowAppearance = Appearance9
        Appearance10.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance10.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbItem.DisplayLayout.Override.RowSelectorAppearance = Appearance10
        Me.cmbItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbItem.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbItem.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance11.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance11.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance11.ForeColor = System.Drawing.Color.Black
        Me.cmbItem.DisplayLayout.Override.SelectedRowAppearance = Appearance11
        Me.cmbItem.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem.DisplayLayout.Override.SelectTypeGroupByRow = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended
        Me.cmbItem.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbItem.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell
        Me.cmbItem.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbItem.LimitToList = True
        Me.cmbItem.Location = New System.Drawing.Point(48, 20)
        Me.cmbItem.MaxDropDownItems = 20
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(289, 29)
        Me.cmbItem.TabIndex = 36
        '
        'RdoCode
        '
        Me.RdoCode.AutoSize = True
        Me.RdoCode.Checked = True
        Me.RdoCode.Location = New System.Drawing.Point(221, 3)
        Me.RdoCode.Name = "RdoCode"
        Me.RdoCode.Size = New System.Drawing.Size(72, 24)
        Me.RdoCode.TabIndex = 37
        Me.RdoCode.TabStop = True
        Me.RdoCode.Text = "Code"
        Me.RdoCode.UseVisualStyleBackColor = True
        '
        'rdoName
        '
        Me.rdoName.AutoSize = True
        Me.rdoName.Location = New System.Drawing.Point(284, 3)
        Me.rdoName.Name = "rdoName"
        Me.rdoName.Size = New System.Drawing.Size(76, 24)
        Me.rdoName.TabIndex = 38
        Me.rdoName.Text = "Name"
        Me.rdoName.UseVisualStyleBackColor = True
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(302, 110)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(75, 23)
        Me.btnGenerate.TabIndex = 40
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 20)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "Item"
        '
        'pnlInvType
        '
        Me.pnlInvType.BackColor = System.Drawing.Color.Transparent
        Me.pnlInvType.Controls.Add(Me.Label1)
        Me.pnlInvType.Controls.Add(Me.cmbItem)
        Me.pnlInvType.Controls.Add(Me.RdoCode)
        Me.pnlInvType.Controls.Add(Me.rdoName)
        Me.pnlInvType.Location = New System.Drawing.Point(12, 48)
        Me.pnlInvType.Name = "pnlInvType"
        Me.pnlInvType.Size = New System.Drawing.Size(365, 56)
        Me.pnlInvType.TabIndex = 116
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(389, 42)
        Me.pnlHeader.TabIndex = 115
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(341, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(36, 30)
        Me.btnClose.TabIndex = 117
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(3, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(283, 35)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Print Article Barcode"
        '
        'frmRptArticleBarcode
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(389, 139)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.pnlInvType)
        Me.Controls.Add(Me.pnlHeader)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRptArticleBarcode"
        Me.Text = "Print Article Barcode"
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInvType.ResumeLayout(False)
        Me.pnlInvType.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents RdoCode As System.Windows.Forms.RadioButton
    Friend WithEvents rdoName As System.Windows.Forms.RadioButton
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlInvType As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
End Class
