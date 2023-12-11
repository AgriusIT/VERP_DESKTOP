''TASK:1011 to build new report named Consumption Estimation Report done by Ameen 
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConsumptionEstimationReport
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
        Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn23 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn24 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Name")
        Dim UltraGridColumn25 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Territory")
        Dim UltraGridColumn26 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City")
        Dim UltraGridColumn27 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("State")
        Dim UltraGridColumn28 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AcId")
        Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance34 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance35 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance37 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance38 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConsumptionEstimationReport))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.gbCriteria = New System.Windows.Forms.GroupBox()
        Me.Tag = New System.Windows.Forms.GroupBox()
        Me.cmbTag = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.gbCriteria1 = New System.Windows.Forms.GroupBox()
        Me.rbConsumed = New System.Windows.Forms.RadioButton()
        Me.rbAll = New System.Windows.Forms.RadioButton()
        Me.rbProduct = New System.Windows.Forms.RadioButton()
        Me.rbTag = New System.Windows.Forms.RadioButton()
        Me.lblTag = New System.Windows.Forms.Label()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.cmbEstimation = New System.Windows.Forms.ComboBox()
        Me.cmbTicket = New System.Windows.Forms.ComboBox()
        Me.cmbPlan = New System.Windows.Forms.ComboBox()
        Me.lblEstimation = New System.Windows.Forms.Label()
        Me.lblTicket = New System.Windows.Forms.Label()
        Me.lblPlan = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        Me.gbCriteria.SuspendLayout()
        Me.Tag.SuspendLayout()
        CType(Me.cmbTag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbCriteria1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(699, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.SimpleAccounts.My.Resources.Resources.BtnNew_Image
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "New"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "&Refresh"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(12, 10)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(264, 20)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Consumption Estimation Report"
        '
        'gbCriteria
        '
        Me.gbCriteria.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbCriteria.Controls.Add(Me.Tag)
        Me.gbCriteria.Controls.Add(Me.cmbEstimation)
        Me.gbCriteria.Controls.Add(Me.cmbTicket)
        Me.gbCriteria.Controls.Add(Me.cmbPlan)
        Me.gbCriteria.Controls.Add(Me.lblEstimation)
        Me.gbCriteria.Controls.Add(Me.lblTicket)
        Me.gbCriteria.Controls.Add(Me.lblPlan)
        Me.gbCriteria.Location = New System.Drawing.Point(5, 58)
        Me.gbCriteria.Name = "gbCriteria"
        Me.gbCriteria.Size = New System.Drawing.Size(675, 140)
        Me.gbCriteria.TabIndex = 2
        Me.gbCriteria.TabStop = False
        '
        'Tag
        '
        Me.Tag.Controls.Add(Me.cmbTag)
        Me.Tag.Controls.Add(Me.gbCriteria1)
        Me.Tag.Controls.Add(Me.rbProduct)
        Me.Tag.Controls.Add(Me.rbTag)
        Me.Tag.Controls.Add(Me.lblTag)
        Me.Tag.Controls.Add(Me.btnShow)
        Me.Tag.Location = New System.Drawing.Point(6, 71)
        Me.Tag.Name = "Tag"
        Me.Tag.Size = New System.Drawing.Size(527, 62)
        Me.Tag.TabIndex = 6
        Me.Tag.TabStop = False
        Me.Tag.Text = "Tags"
        '
        'cmbTag
        '
        Me.cmbTag.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbTag.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbTag.CheckedListSettings.CheckStateMember = ""
        Appearance32.BackColor = System.Drawing.Color.White
        Appearance32.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbTag.DisplayLayout.Appearance = Appearance32
        UltraGridColumn23.Header.VisiblePosition = 0
        UltraGridColumn23.Hidden = True
        UltraGridColumn24.Header.VisiblePosition = 1
        UltraGridColumn24.Width = 141
        UltraGridColumn25.Header.VisiblePosition = 2
        UltraGridColumn26.Header.VisiblePosition = 3
        UltraGridColumn27.Header.VisiblePosition = 4
        UltraGridColumn28.Header.VisiblePosition = 5
        UltraGridColumn28.Hidden = True
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn23, UltraGridColumn24, UltraGridColumn25, UltraGridColumn26, UltraGridColumn27, UltraGridColumn28})
        Appearance33.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        UltraGridBand1.Override.HeaderAppearance = Appearance33
        Appearance34.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance34.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        UltraGridBand1.Override.SelectedRowAppearance = Appearance34
        Me.cmbTag.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbTag.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbTag.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbTag.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbTag.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance35.BackColor = System.Drawing.Color.Transparent
        Me.cmbTag.DisplayLayout.Override.CardAreaAppearance = Appearance35
        Me.cmbTag.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbTag.DisplayLayout.Override.CellPadding = 3
        Me.cmbTag.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance36.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance36.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance36.TextHAlignAsString = "Left"
        Me.cmbTag.DisplayLayout.Override.HeaderAppearance = Appearance36
        Me.cmbTag.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance37.BorderColor = System.Drawing.Color.LightGray
        Appearance37.TextVAlignAsString = "Middle"
        Me.cmbTag.DisplayLayout.Override.RowAppearance = Appearance37
        Appearance38.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance38.BackColor2 = System.Drawing.Color.White
        Appearance38.BorderColor = System.Drawing.Color.Black
        Appearance38.ForeColor = System.Drawing.Color.Black
        Me.cmbTag.DisplayLayout.Override.SelectedRowAppearance = Appearance38
        Me.cmbTag.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbTag.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbTag.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbTag.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbTag.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbTag.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbTag.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbTag.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbTag.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTag.LimitToList = True
        Me.cmbTag.Location = New System.Drawing.Point(61, 29)
        Me.cmbTag.Name = "cmbTag"
        Me.cmbTag.Size = New System.Drawing.Size(217, 23)
        Me.cmbTag.TabIndex = 1
        '
        'gbCriteria1
        '
        Me.gbCriteria1.Controls.Add(Me.rbConsumed)
        Me.gbCriteria1.Controls.Add(Me.rbAll)
        Me.gbCriteria1.Location = New System.Drawing.Point(284, 20)
        Me.gbCriteria1.Name = "gbCriteria1"
        Me.gbCriteria1.Size = New System.Drawing.Size(127, 35)
        Me.gbCriteria1.TabIndex = 4
        Me.gbCriteria1.TabStop = False
        '
        'rbConsumed
        '
        Me.rbConsumed.AutoSize = True
        Me.rbConsumed.Location = New System.Drawing.Point(48, 12)
        Me.rbConsumed.Name = "rbConsumed"
        Me.rbConsumed.Size = New System.Drawing.Size(75, 17)
        Me.rbConsumed.TabIndex = 1
        Me.rbConsumed.TabStop = True
        Me.rbConsumed.Text = "Consumed"
        Me.rbConsumed.UseVisualStyleBackColor = True
        '
        'rbAll
        '
        Me.rbAll.AutoSize = True
        Me.rbAll.Location = New System.Drawing.Point(6, 12)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(36, 17)
        Me.rbAll.TabIndex = 0
        Me.rbAll.TabStop = True
        Me.rbAll.Text = "All"
        Me.rbAll.UseVisualStyleBackColor = True
        '
        'rbProduct
        '
        Me.rbProduct.AutoSize = True
        Me.rbProduct.Location = New System.Drawing.Point(216, 11)
        Me.rbProduct.Name = "rbProduct"
        Me.rbProduct.Size = New System.Drawing.Size(62, 17)
        Me.rbProduct.TabIndex = 3
        Me.rbProduct.TabStop = True
        Me.rbProduct.Text = "Product"
        Me.rbProduct.UseVisualStyleBackColor = True
        '
        'rbTag
        '
        Me.rbTag.AutoSize = True
        Me.rbTag.Location = New System.Drawing.Point(166, 11)
        Me.rbTag.Name = "rbTag"
        Me.rbTag.Size = New System.Drawing.Size(44, 17)
        Me.rbTag.TabIndex = 2
        Me.rbTag.TabStop = True
        Me.rbTag.Text = "Tag"
        Me.rbTag.UseVisualStyleBackColor = True
        '
        'lblTag
        '
        Me.lblTag.AutoSize = True
        Me.lblTag.Location = New System.Drawing.Point(2, 32)
        Me.lblTag.Name = "lblTag"
        Me.lblTag.Size = New System.Drawing.Size(26, 13)
        Me.lblTag.TabIndex = 0
        Me.lblTag.Text = "Tag"
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(417, 29)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(45, 23)
        Me.btnShow.TabIndex = 5
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'cmbEstimation
        '
        Me.cmbEstimation.FormattingEnabled = True
        Me.cmbEstimation.Location = New System.Drawing.Point(67, 50)
        Me.cmbEstimation.Name = "cmbEstimation"
        Me.cmbEstimation.Size = New System.Drawing.Size(217, 21)
        Me.cmbEstimation.TabIndex = 5
        '
        'cmbTicket
        '
        Me.cmbTicket.FormattingEnabled = True
        Me.cmbTicket.Location = New System.Drawing.Point(333, 23)
        Me.cmbTicket.Name = "cmbTicket"
        Me.cmbTicket.Size = New System.Drawing.Size(200, 21)
        Me.cmbTicket.TabIndex = 3
        '
        'cmbPlan
        '
        Me.cmbPlan.FormattingEnabled = True
        Me.cmbPlan.Location = New System.Drawing.Point(67, 23)
        Me.cmbPlan.Name = "cmbPlan"
        Me.cmbPlan.Size = New System.Drawing.Size(217, 21)
        Me.cmbPlan.TabIndex = 1
        '
        'lblEstimation
        '
        Me.lblEstimation.AutoSize = True
        Me.lblEstimation.Location = New System.Drawing.Point(6, 55)
        Me.lblEstimation.Name = "lblEstimation"
        Me.lblEstimation.Size = New System.Drawing.Size(55, 13)
        Me.lblEstimation.TabIndex = 4
        Me.lblEstimation.Text = "Estimation"
        '
        'lblTicket
        '
        Me.lblTicket.AutoSize = True
        Me.lblTicket.Location = New System.Drawing.Point(290, 26)
        Me.lblTicket.Name = "lblTicket"
        Me.lblTicket.Size = New System.Drawing.Size(37, 13)
        Me.lblTicket.TabIndex = 2
        Me.lblTicket.Text = "Ticket"
        '
        'lblPlan
        '
        Me.lblPlan.AutoSize = True
        Me.lblPlan.Location = New System.Drawing.Point(6, 26)
        Me.lblPlan.Name = "lblPlan"
        Me.lblPlan.Size = New System.Drawing.Size(28, 13)
        Me.lblPlan.TabIndex = 0
        Me.lblPlan.Text = "Plan"
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grd.Location = New System.Drawing.Point(0, 204)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(687, 209)
        Me.grd.TabIndex = 4
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Nothing
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(666, 0)
        Me.CtrlGrdBar1.MyGrid = Nothing
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(33, 25)
        Me.CtrlGrdBar1.TabIndex = 3
        Me.CtrlGrdBar1.TabStop = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(699, 42)
        Me.pnlHeader.TabIndex = 12
        '
        'frmConsumptionEstimationReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(699, 414)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.gbCriteria)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmConsumptionEstimationReport"
        Me.Text = "Consumption Estimation Report"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.gbCriteria.ResumeLayout(False)
        Me.gbCriteria.PerformLayout()
        Me.Tag.ResumeLayout(False)
        Me.Tag.PerformLayout()
        CType(Me.cmbTag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbCriteria1.ResumeLayout(False)
        Me.gbCriteria1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents gbCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents cmbEstimation As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTicket As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPlan As System.Windows.Forms.ComboBox
    Friend WithEvents lblEstimation As System.Windows.Forms.Label
    Friend WithEvents lblTicket As System.Windows.Forms.Label
    Friend WithEvents lblPlan As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblTag As System.Windows.Forms.Label
    Friend WithEvents rbAll As System.Windows.Forms.RadioButton
    Friend WithEvents Tag As System.Windows.Forms.GroupBox
    Friend WithEvents gbCriteria1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbProduct As System.Windows.Forms.RadioButton
    Friend WithEvents rbTag As System.Windows.Forms.RadioButton
    Friend WithEvents rbConsumed As System.Windows.Forms.RadioButton
    Friend WithEvents cmbTag As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class
