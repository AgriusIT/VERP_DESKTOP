<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptAgingReceiveables
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
        Me.components = New System.ComponentModel.Container()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("coa_detail_id")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_title")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_code")
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Color")
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrdRptAgingReceiveables))
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.chkShowWithOutCostCenter = New System.Windows.Forms.CheckBox()
        Me.lstBelt = New SimpleAccounts.uiListControl()
        Me.lstRegion = New SimpleAccounts.uiListControl()
        Me.lstZone = New SimpleAccounts.uiListControl()
        Me.lstCostCenter = New SimpleAccounts.uiListControl()
        Me.lstSubSubAccount = New SimpleAccounts.uiListControl()
        Me.lstHeadCostCenter = New SimpleAccounts.uiListControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnLoadOld = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblPropertyType = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbPropertyType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbZone = New System.Windows.Forms.ComboBox()
        Me.cmbBelt = New System.Windows.Forms.ComboBox()
        Me.cmbRegion = New System.Windows.Forms.ComboBox()
        Me.lblCostCenter = New System.Windows.Forms.Label()
        Me.cmbCostCenter = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblSubSub = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.cmbSubSub = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.chkIncludeUnPosted = New System.Windows.Forms.CheckBox()
        Me.lnkRefresh = New System.Windows.Forms.LinkLabel()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.CtrlGrdBar3 = New SimpleAccounts.CtrlGrdBar()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.cmbFormate = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAddTemplate = New System.Windows.Forms.ToolStripButton()
        Me.btnSMSTempSettings = New System.Windows.Forms.ToolStripButton()
        Me.btnSendSMS = New System.Windows.Forms.ToolStripButton()
        Me.pnlFollowUp = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.lbldtpFollowUp = New System.Windows.Forms.Label()
        Me.dtpFollowUp = New System.Windows.Forms.DateTimePicker()
        Me.lblFollowUpRemakrs = New System.Windows.Forms.Label()
        Me.txtFollowUpRemarks = New System.Windows.Forms.TextBox()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.btnBack = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabPageControl3.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.cmbCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSubSub, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlFollowUp.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip3.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.chkShowWithOutCostCenter)
        Me.UltraTabPageControl3.Controls.Add(Me.lstBelt)
        Me.UltraTabPageControl3.Controls.Add(Me.lstRegion)
        Me.UltraTabPageControl3.Controls.Add(Me.lstZone)
        Me.UltraTabPageControl3.Controls.Add(Me.lstCostCenter)
        Me.UltraTabPageControl3.Controls.Add(Me.lstSubSubAccount)
        Me.UltraTabPageControl3.Controls.Add(Me.lstHeadCostCenter)
        Me.UltraTabPageControl3.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl3.Controls.Add(Me.lblPropertyType)
        Me.UltraTabPageControl3.Controls.Add(Me.Label6)
        Me.UltraTabPageControl3.Controls.Add(Me.Label4)
        Me.UltraTabPageControl3.Controls.Add(Me.cmbPropertyType)
        Me.UltraTabPageControl3.Controls.Add(Me.Label3)
        Me.UltraTabPageControl3.Controls.Add(Me.cmbZone)
        Me.UltraTabPageControl3.Controls.Add(Me.cmbBelt)
        Me.UltraTabPageControl3.Controls.Add(Me.cmbRegion)
        Me.UltraTabPageControl3.Controls.Add(Me.lblCostCenter)
        Me.UltraTabPageControl3.Controls.Add(Me.cmbCostCenter)
        Me.UltraTabPageControl3.Controls.Add(Me.lblSubSub)
        Me.UltraTabPageControl3.Controls.Add(Me.btnSearch)
        Me.UltraTabPageControl3.Controls.Add(Me.cmbSubSub)
        Me.UltraTabPageControl3.Controls.Add(Me.chkIncludeUnPosted)
        Me.UltraTabPageControl3.Controls.Add(Me.lnkRefresh)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(1215, 576)
        '
        'chkShowWithOutCostCenter
        '
        Me.chkShowWithOutCostCenter.AutoSize = True
        Me.chkShowWithOutCostCenter.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.chkShowWithOutCostCenter.Location = New System.Drawing.Point(669, 439)
        Me.chkShowWithOutCostCenter.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkShowWithOutCostCenter.Name = "chkShowWithOutCostCenter"
        Me.chkShowWithOutCostCenter.Size = New System.Drawing.Size(186, 21)
        Me.chkShowWithOutCostCenter.TabIndex = 38
        Me.chkShowWithOutCostCenter.Text = "Show without cost center"
        Me.chkShowWithOutCostCenter.UseVisualStyleBackColor = False
        '
        'lstBelt
        '
        Me.lstBelt.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstBelt.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstBelt.BackColor = System.Drawing.Color.Transparent
        Me.lstBelt.disableWhenChecked = False
        Me.lstBelt.HeadingLabelName = "lstBelt"
        Me.lstBelt.HeadingText = "Belt"
        Me.lstBelt.Location = New System.Drawing.Point(451, 302)
        Me.lstBelt.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.lstBelt.Name = "lstBelt"
        Me.lstBelt.ShowAddNewButton = False
        Me.lstBelt.ShowInverse = True
        Me.lstBelt.ShowMagnifierButton = False
        Me.lstBelt.ShowNoCheck = False
        Me.lstBelt.ShowResetAllButton = False
        Me.lstBelt.ShowSelectall = True
        Me.lstBelt.Size = New System.Drawing.Size(203, 196)
        Me.lstBelt.TabIndex = 37
        Me.lstBelt.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstRegion
        '
        Me.lstRegion.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstRegion.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstRegion.BackColor = System.Drawing.Color.Transparent
        Me.lstRegion.disableWhenChecked = False
        Me.lstRegion.HeadingLabelName = "lstRegion"
        Me.lstRegion.HeadingText = "Region"
        Me.lstRegion.Location = New System.Drawing.Point(29, 302)
        Me.lstRegion.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.lstRegion.Name = "lstRegion"
        Me.lstRegion.ShowAddNewButton = False
        Me.lstRegion.ShowInverse = True
        Me.lstRegion.ShowMagnifierButton = False
        Me.lstRegion.ShowNoCheck = False
        Me.lstRegion.ShowResetAllButton = False
        Me.lstRegion.ShowSelectall = True
        Me.lstRegion.Size = New System.Drawing.Size(203, 196)
        Me.lstRegion.TabIndex = 37
        Me.lstRegion.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstZone
        '
        Me.lstZone.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstZone.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstZone.BackColor = System.Drawing.Color.Transparent
        Me.lstZone.disableWhenChecked = False
        Me.lstZone.HeadingLabelName = "lstZone"
        Me.lstZone.HeadingText = "Zone"
        Me.lstZone.Location = New System.Drawing.Point(240, 302)
        Me.lstZone.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.lstZone.Name = "lstZone"
        Me.lstZone.ShowAddNewButton = False
        Me.lstZone.ShowInverse = True
        Me.lstZone.ShowMagnifierButton = False
        Me.lstZone.ShowNoCheck = False
        Me.lstZone.ShowResetAllButton = False
        Me.lstZone.ShowSelectall = True
        Me.lstZone.Size = New System.Drawing.Size(203, 196)
        Me.lstZone.TabIndex = 36
        Me.lstZone.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstCostCenter
        '
        Me.lstCostCenter.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCostCenter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.lstCostCenter.disableWhenChecked = False
        Me.lstCostCenter.HeadingLabelName = "lstCostCenter"
        Me.lstCostCenter.HeadingText = "Cost Center"
        Me.lstCostCenter.Location = New System.Drawing.Point(240, 71)
        Me.lstCostCenter.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.lstCostCenter.Name = "lstCostCenter"
        Me.lstCostCenter.ShowAddNewButton = False
        Me.lstCostCenter.ShowInverse = True
        Me.lstCostCenter.ShowMagnifierButton = False
        Me.lstCostCenter.ShowNoCheck = False
        Me.lstCostCenter.ShowResetAllButton = False
        Me.lstCostCenter.ShowSelectall = True
        Me.lstCostCenter.Size = New System.Drawing.Size(203, 196)
        Me.lstCostCenter.TabIndex = 37
        Me.ToolTip1.SetToolTip(Me.lstCostCenter, "Employee Cost Center list")
        Me.lstCostCenter.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstSubSubAccount
        '
        Me.lstSubSubAccount.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstSubSubAccount.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstSubSubAccount.BackColor = System.Drawing.Color.Transparent
        Me.lstSubSubAccount.disableWhenChecked = False
        Me.lstSubSubAccount.HeadingLabelName = "lstSubSubAccount"
        Me.lstSubSubAccount.HeadingText = "Sub Sub Account"
        Me.lstSubSubAccount.Location = New System.Drawing.Point(451, 71)
        Me.lstSubSubAccount.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.lstSubSubAccount.Name = "lstSubSubAccount"
        Me.lstSubSubAccount.ShowAddNewButton = False
        Me.lstSubSubAccount.ShowInverse = True
        Me.lstSubSubAccount.ShowMagnifierButton = False
        Me.lstSubSubAccount.ShowNoCheck = False
        Me.lstSubSubAccount.ShowResetAllButton = False
        Me.lstSubSubAccount.ShowSelectall = True
        Me.lstSubSubAccount.Size = New System.Drawing.Size(413, 196)
        Me.lstSubSubAccount.TabIndex = 36
        Me.lstSubSubAccount.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstHeadCostCenter
        '
        Me.lstHeadCostCenter.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstHeadCostCenter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstHeadCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.lstHeadCostCenter.disableWhenChecked = False
        Me.lstHeadCostCenter.HeadingLabelName = "lstHeadCostCenter"
        Me.lstHeadCostCenter.HeadingText = "Head Cost Center"
        Me.lstHeadCostCenter.Location = New System.Drawing.Point(29, 71)
        Me.lstHeadCostCenter.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.lstHeadCostCenter.Name = "lstHeadCostCenter"
        Me.lstHeadCostCenter.ShowAddNewButton = False
        Me.lstHeadCostCenter.ShowInverse = True
        Me.lstHeadCostCenter.ShowMagnifierButton = False
        Me.lstHeadCostCenter.ShowNoCheck = False
        Me.lstHeadCostCenter.ShowResetAllButton = False
        Me.lstHeadCostCenter.ShowSelectall = True
        Me.lstHeadCostCenter.Size = New System.Drawing.Size(203, 196)
        Me.lstHeadCostCenter.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.lstHeadCostCenter, "Employee Head Cost Center list")
        Me.lstHeadCostCenter.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnLoadOld)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1215, 43)
        Me.pnlHeader.TabIndex = 35
        '
        'btnLoadOld
        '
        Me.btnLoadOld.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoadOld.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnLoadOld.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoadOld.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadOld.ForeColor = System.Drawing.Color.Black
        Me.btnLoadOld.Location = New System.Drawing.Point(1111, 7)
        Me.btnLoadOld.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnLoadOld.Name = "btnLoadOld"
        Me.btnLoadOld.Size = New System.Drawing.Size(100, 28)
        Me.btnLoadOld.TabIndex = 2
        Me.btnLoadOld.Text = "Old"
        Me.btnLoadOld.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(24, 5)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(245, 35)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Aging Receivables"
        '
        'lblPropertyType
        '
        Me.lblPropertyType.AutoSize = True
        Me.lblPropertyType.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.lblPropertyType.Location = New System.Drawing.Point(665, 342)
        Me.lblPropertyType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPropertyType.Name = "lblPropertyType"
        Me.lblPropertyType.Size = New System.Drawing.Size(98, 17)
        Me.lblPropertyType.TabIndex = 32
        Me.lblPropertyType.Text = "Property Type"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(947, 559)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 17)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Zone"
        Me.Label6.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(591, 559)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 17)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Region"
        Me.Label4.Visible = False
        '
        'cmbPropertyType
        '
        Me.cmbPropertyType.FormattingEnabled = True
        Me.cmbPropertyType.Location = New System.Drawing.Point(669, 362)
        Me.cmbPropertyType.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbPropertyType.Name = "cmbPropertyType"
        Me.cmbPropertyType.Size = New System.Drawing.Size(192, 24)
        Me.cmbPropertyType.TabIndex = 31
        Me.cmbPropertyType.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(591, 592)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 17)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Belt"
        Me.Label3.Visible = False
        '
        'cmbZone
        '
        Me.cmbZone.FormattingEnabled = True
        Me.cmbZone.Location = New System.Drawing.Point(1037, 555)
        Me.cmbZone.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbZone.Name = "cmbZone"
        Me.cmbZone.Size = New System.Drawing.Size(192, 24)
        Me.cmbZone.TabIndex = 31
        Me.ToolTip1.SetToolTip(Me.cmbZone, "Zone")
        Me.cmbZone.Visible = False
        '
        'cmbBelt
        '
        Me.cmbBelt.FormattingEnabled = True
        Me.cmbBelt.Location = New System.Drawing.Point(659, 588)
        Me.cmbBelt.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbBelt.Name = "cmbBelt"
        Me.cmbBelt.Size = New System.Drawing.Size(267, 24)
        Me.cmbBelt.TabIndex = 29
        Me.ToolTip1.SetToolTip(Me.cmbBelt, "Blet")
        Me.cmbBelt.Visible = False
        '
        'cmbRegion
        '
        Me.cmbRegion.FormattingEnabled = True
        Me.cmbRegion.Location = New System.Drawing.Point(659, 555)
        Me.cmbRegion.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbRegion.Name = "cmbRegion"
        Me.cmbRegion.Size = New System.Drawing.Size(267, 24)
        Me.cmbRegion.TabIndex = 30
        Me.ToolTip1.SetToolTip(Me.cmbRegion, "Region")
        Me.cmbRegion.Visible = False
        '
        'lblCostCenter
        '
        Me.lblCostCenter.AutoSize = True
        Me.lblCostCenter.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.lblCostCenter.Location = New System.Drawing.Point(947, 526)
        Me.lblCostCenter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(82, 17)
        Me.lblCostCenter.TabIndex = 28
        Me.lblCostCenter.Text = "Cost Centre"
        Me.lblCostCenter.Visible = False
        '
        'cmbCostCenter
        '
        Appearance1.BackColor = System.Drawing.Color.White
        Me.cmbCostCenter.Appearance = Appearance1
        Me.cmbCostCenter.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend
        Me.cmbCostCenter.CheckedListSettings.CheckStateMember = ""
        Appearance2.BackColor = System.Drawing.Color.White
        Me.cmbCostCenter.DisplayLayout.Appearance = Appearance2
        UltraGridColumn1.Header.Caption = "ID"
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn1.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(8, 0)
        UltraGridColumn2.Header.Caption = "Account"
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(128, 0)
        UltraGridColumn3.Header.Caption = "Code"
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3})
        Me.cmbCostCenter.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbCostCenter.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbCostCenter.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCostCenter.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCostCenter.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance3.BackColor = System.Drawing.Color.Transparent
        Me.cmbCostCenter.DisplayLayout.Override.CardAreaAppearance = Appearance3
        Me.cmbCostCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbCostCenter.DisplayLayout.Override.CellPadding = 3
        Me.cmbCostCenter.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance4.TextHAlignAsString = "Left"
        Me.cmbCostCenter.DisplayLayout.Override.HeaderAppearance = Appearance4
        Me.cmbCostCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance5.BorderColor = System.Drawing.Color.LightGray
        Appearance5.TextVAlignAsString = "Middle"
        Me.cmbCostCenter.DisplayLayout.Override.RowAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance6.BorderColor = System.Drawing.Color.Black
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.cmbCostCenter.DisplayLayout.Override.SelectedRowAppearance = Appearance6
        Me.cmbCostCenter.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCostCenter.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCostCenter.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbCostCenter.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbCostCenter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbCostCenter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbCostCenter.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbCostCenter.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbCostCenter.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.VisualStudio2005
        Me.cmbCostCenter.LimitToList = True
        Me.cmbCostCenter.Location = New System.Drawing.Point(1037, 521)
        Me.cmbCostCenter.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbCostCenter.MaxDropDownItems = 16
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(192, 25)
        Me.cmbCostCenter.TabIndex = 27
        Me.cmbCostCenter.Visible = False
        '
        'lblSubSub
        '
        Me.lblSubSub.AutoSize = True
        Me.lblSubSub.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.lblSubSub.Location = New System.Drawing.Point(591, 526)
        Me.lblSubSub.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSubSub.Name = "lblSubSub"
        Me.lblSubSub.Size = New System.Drawing.Size(62, 17)
        Me.lblSubSub.TabIndex = 26
        Me.lblSubSub.Text = "Sub Sub"
        Me.lblSubSub.Visible = False
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(768, 468)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 28)
        Me.btnSearch.TabIndex = 25
        Me.btnSearch.Text = "Show"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'cmbSubSub
        '
        Me.cmbSubSub.AlwaysInEditMode = True
        Me.cmbSubSub.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbSubSub.CheckedListSettings.CheckStateMember = ""
        Appearance13.BackColor = System.Drawing.Color.White
        Appearance13.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbSubSub.DisplayLayout.Appearance = Appearance13
        UltraGridColumn9.Header.VisiblePosition = 0
        UltraGridColumn9.Hidden = True
        UltraGridColumn10.Header.VisiblePosition = 1
        UltraGridColumn11.Header.VisiblePosition = 2
        UltraGridColumn12.Header.VisiblePosition = 3
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn9, UltraGridColumn10, UltraGridColumn11, UltraGridColumn12})
        Me.cmbSubSub.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.cmbSubSub.DisplayLayout.InterBandSpacing = 10
        Me.cmbSubSub.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbSubSub.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSubSub.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance14.BackColor = System.Drawing.Color.Transparent
        Me.cmbSubSub.DisplayLayout.Override.CardAreaAppearance = Appearance14
        Me.cmbSubSub.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbSubSub.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance15.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance15.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance15.ForeColor = System.Drawing.Color.White
        Appearance15.TextHAlignAsString = "Left"
        Appearance15.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbSubSub.DisplayLayout.Override.HeaderAppearance = Appearance15
        Me.cmbSubSub.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance16.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbSubSub.DisplayLayout.Override.RowAppearance = Appearance16
        Appearance17.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance17.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbSubSub.DisplayLayout.Override.RowSelectorAppearance = Appearance17
        Me.cmbSubSub.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbSubSub.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance18.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance18.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance18.ForeColor = System.Drawing.Color.Black
        Me.cmbSubSub.DisplayLayout.Override.SelectedRowAppearance = Appearance18
        Me.cmbSubSub.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSubSub.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSubSub.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbSubSub.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbSubSub.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbSubSub.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbSubSub.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbSubSub.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbSubSub.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbSubSub.LimitToList = True
        Me.cmbSubSub.Location = New System.Drawing.Point(659, 521)
        Me.cmbSubSub.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbSubSub.MaxDropDownItems = 20
        Me.cmbSubSub.Name = "cmbSubSub"
        Me.cmbSubSub.Size = New System.Drawing.Size(268, 25)
        Me.cmbSubSub.TabIndex = 24
        Me.cmbSubSub.Visible = False
        '
        'chkIncludeUnPosted
        '
        Me.chkIncludeUnPosted.AutoSize = True
        Me.chkIncludeUnPosted.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.chkIncludeUnPosted.Checked = True
        Me.chkIncludeUnPosted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeUnPosted.Location = New System.Drawing.Point(669, 415)
        Me.chkIncludeUnPosted.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkIncludeUnPosted.Name = "chkIncludeUnPosted"
        Me.chkIncludeUnPosted.Size = New System.Drawing.Size(197, 21)
        Me.chkIncludeUnPosted.TabIndex = 23
        Me.chkIncludeUnPosted.Text = "Include Unposted Voucher"
        Me.chkIncludeUnPosted.UseVisualStyleBackColor = False
        '
        'lnkRefresh
        '
        Me.lnkRefresh.AutoSize = True
        Me.lnkRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.lnkRefresh.Location = New System.Drawing.Point(665, 391)
        Me.lnkRefresh.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkRefresh.Name = "lnkRefresh"
        Me.lnkRefresh.Size = New System.Drawing.Size(58, 17)
        Me.lnkRefresh.TabIndex = 22
        Me.lnkRefresh.TabStop = True
        Me.lnkRefresh.Text = "Refresh"
        Me.lnkRefresh.Visible = False
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.CtrlGrdBar3)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbFormate)
        Me.UltraTabPageControl1.Controls.Add(Me.Label2)
        Me.UltraTabPageControl1.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip1)
        Me.UltraTabPageControl1.Controls.Add(Me.grd)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlFollowUp)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-13333, -12308)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1215, 576)
        '
        'CtrlGrdBar3
        '
        Me.CtrlGrdBar3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar3.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar3.Email = Nothing
        Me.CtrlGrdBar3.FormName = Me
        Me.CtrlGrdBar3.Location = New System.Drawing.Point(1165, 0)
        Me.CtrlGrdBar3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CtrlGrdBar3.MyGrid = Me.grd
        Me.CtrlGrdBar3.Name = "CtrlGrdBar3"
        Me.CtrlGrdBar3.Size = New System.Drawing.Size(51, 31)
        Me.CtrlGrdBar3.TabIndex = 24
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
        Me.grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grd.Location = New System.Drawing.Point(0, 34)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1215, 427)
        Me.grd.TabIndex = 2
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'cmbFormate
        '
        Me.cmbFormate.FormattingEnabled = True
        Me.cmbFormate.Location = New System.Drawing.Point(760, 4)
        Me.cmbFormate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbFormate.Name = "cmbFormate"
        Me.cmbFormate.Size = New System.Drawing.Size(267, 24)
        Me.cmbFormate.TabIndex = 22
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(692, 7)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 17)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Layout"
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1273, -1)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(51, 31)
        Me.CtrlGrdBar1.TabIndex = 8
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.btnPrint, Me.ToolStripSeparator1, Me.btnAddTemplate, Me.btnSMSTempSettings, Me.btnSendSMS})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1215, 31)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(86, 28)
        Me.btnRefresh.Text = "Refresh"
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(67, 28)
        Me.btnPrint.Text = "Print"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 31)
        '
        'btnAddTemplate
        '
        Me.btnAddTemplate.Image = Global.SimpleAccounts.My.Resources.Resources.addIcon
        Me.btnAddTemplate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAddTemplate.Name = "btnAddTemplate"
        Me.btnAddTemplate.Size = New System.Drawing.Size(152, 28)
        Me.btnAddTemplate.Text = "Add aging layout"
        '
        'btnSMSTempSettings
        '
        Me.btnSMSTempSettings.Image = Global.SimpleAccounts.My.Resources.Resources.Attach
        Me.btnSMSTempSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSMSTempSettings.Name = "btnSMSTempSettings"
        Me.btnSMSTempSettings.Size = New System.Drawing.Size(179, 28)
        Me.btnSMSTempSettings.Text = "SMS template setting"
        '
        'btnSendSMS
        '
        Me.btnSendSMS.Image = CType(resources.GetObject("btnSendSMS.Image"), System.Drawing.Image)
        Me.btnSendSMS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSendSMS.Name = "btnSendSMS"
        Me.btnSendSMS.Size = New System.Drawing.Size(103, 28)
        Me.btnSendSMS.Text = "Send SMS"
        '
        'pnlFollowUp
        '
        Me.pnlFollowUp.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.pnlFollowUp.Controls.Add(Me.btnSave)
        Me.pnlFollowUp.Controls.Add(Me.lbldtpFollowUp)
        Me.pnlFollowUp.Controls.Add(Me.dtpFollowUp)
        Me.pnlFollowUp.Controls.Add(Me.lblFollowUpRemakrs)
        Me.pnlFollowUp.Controls.Add(Me.txtFollowUpRemarks)
        Me.pnlFollowUp.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFollowUp.Location = New System.Drawing.Point(0, 457)
        Me.pnlFollowUp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlFollowUp.Name = "pnlFollowUp"
        Me.pnlFollowUp.Size = New System.Drawing.Size(1215, 119)
        Me.pnlFollowUp.TabIndex = 15
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(905, 68)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 33)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lbldtpFollowUp
        '
        Me.lbldtpFollowUp.AutoSize = True
        Me.lbldtpFollowUp.Location = New System.Drawing.Point(733, 16)
        Me.lbldtpFollowUp.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbldtpFollowUp.Name = "lbldtpFollowUp"
        Me.lbldtpFollowUp.Size = New System.Drawing.Size(99, 17)
        Me.lbldtpFollowUp.TabIndex = 3
        Me.lbldtpFollowUp.Text = "FollowUp Date"
        '
        'dtpFollowUp
        '
        Me.dtpFollowUp.CustomFormat = "dd/MM/yyyy"
        Me.dtpFollowUp.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFollowUp.Location = New System.Drawing.Point(737, 36)
        Me.dtpFollowUp.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtpFollowUp.Name = "dtpFollowUp"
        Me.dtpFollowUp.Size = New System.Drawing.Size(265, 22)
        Me.dtpFollowUp.TabIndex = 2
        '
        'lblFollowUpRemakrs
        '
        Me.lblFollowUpRemakrs.AutoSize = True
        Me.lblFollowUpRemakrs.Location = New System.Drawing.Point(4, 16)
        Me.lblFollowUpRemakrs.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFollowUpRemakrs.Name = "lblFollowUpRemakrs"
        Me.lblFollowUpRemakrs.Size = New System.Drawing.Size(125, 17)
        Me.lblFollowUpRemakrs.TabIndex = 1
        Me.lblFollowUpRemakrs.Text = "FollowUp Remarks"
        '
        'txtFollowUpRemarks
        '
        Me.txtFollowUpRemarks.Location = New System.Drawing.Point(140, 12)
        Me.txtFollowUpRemarks.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtFollowUpRemarks.Multiline = True
        Me.txtFollowUpRemarks.Name = "txtFollowUpRemarks"
        Me.txtFollowUpRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFollowUpRemarks.Size = New System.Drawing.Size(576, 88)
        Me.txtFollowUpRemarks.TabIndex = 0
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.Button1)
        Me.UltraTabPageControl2.Controls.Add(Me.DateTimePicker2)
        Me.UltraTabPageControl2.Controls.Add(Me.DateTimePicker1)
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar2)
        Me.UltraTabPageControl2.Controls.Add(Me.GridEX1)
        Me.UltraTabPageControl2.Controls.Add(Me.ToolStrip3)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-13333, -12308)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1215, 576)
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(356, 34)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 28)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Show"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(185, 38)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(161, 22)
        Me.DateTimePicker2.TabIndex = 9
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(15, 38)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(161, 22)
        Me.DateTimePicker1.TabIndex = 8
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Nothing
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(1160, 0)
        Me.CtrlGrdBar2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CtrlGrdBar2.MyGrid = Me.GridEX1
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(51, 31)
        Me.CtrlGrdBar2.TabIndex = 1
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEX1.Location = New System.Drawing.Point(0, 70)
        Me.GridEX1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(1204, 608)
        Me.GridEX1.TabIndex = 2
        Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip3
        '
        Me.ToolStrip3.AutoSize = False
        Me.ToolStrip3.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnBack, Me.ToolStripButton2})
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Size = New System.Drawing.Size(1215, 31)
        Me.ToolStrip3.TabIndex = 0
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'btnBack
        '
        Me.btnBack.Image = Global.SimpleAccounts.My.Resources.Resources.back
        Me.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(68, 28)
        Me.btnBack.Text = "Back"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(67, 28)
        Me.ToolStripButton2.Text = "Print"
        '
        'BackgroundWorker1
        '
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(79, 22)
        Me.ToolStripButton1.Text = "Send SMS"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1217, 599)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 13
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab3.TabPage = Me.UltraTabPageControl3
        UltraTab3.Text = "Criteria"
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Result"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Invoice wise"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab3, UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1215, 576)
        '
        'frmGrdRptAgingReceiveables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1217, 599)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmGrdRptAgingReceiveables"
        Me.Text = "Aging Receivable"
        Me.UltraTabPageControl3.ResumeLayout(False)
        Me.UltraTabPageControl3.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.cmbCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSubSub, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlFollowUp.ResumeLayout(False)
        Me.pnlFollowUp.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnAddTemplate As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents btnSMSTempSettings As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSendSMS As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip3 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlFollowUp As System.Windows.Forms.Panel
    Friend WithEvents lblFollowUpRemakrs As System.Windows.Forms.Label
    Friend WithEvents txtFollowUpRemarks As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lbldtpFollowUp As System.Windows.Forms.Label
    Friend WithEvents dtpFollowUp As System.Windows.Forms.DateTimePicker
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents chkIncludeUnPosted As System.Windows.Forms.CheckBox
    Friend WithEvents lnkRefresh As System.Windows.Forms.LinkLabel
    Friend WithEvents lstBelt As SimpleAccounts.uiListControl
    Friend WithEvents lstRegion As SimpleAccounts.uiListControl
    Friend WithEvents lstZone As SimpleAccounts.uiListControl
    Friend WithEvents lstCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstSubSubAccount As SimpleAccounts.uiListControl
    Friend WithEvents lstHeadCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents cmbFormate As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbZone As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBelt As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRegion As System.Windows.Forms.ComboBox
    Friend WithEvents lblCostCenter As System.Windows.Forms.Label
    Friend WithEvents cmbCostCenter As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblSubSub As System.Windows.Forms.Label
    Friend WithEvents cmbSubSub As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents CtrlGrdBar3 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents chkShowWithOutCostCenter As System.Windows.Forms.CheckBox
    Friend WithEvents lblPropertyType As System.Windows.Forms.Label
    Friend WithEvents cmbPropertyType As System.Windows.Forms.ComboBox
    Friend WithEvents btnLoadOld As System.Windows.Forms.Button
End Class
