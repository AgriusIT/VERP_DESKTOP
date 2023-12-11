<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAgreement
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
        Me.components = New System.ComponentModel.Container()
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAgreement))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Color")
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn26 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn27 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Name")
        Dim UltraGridColumn28 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Territory")
        Dim UltraGridColumn29 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City")
        Dim UltraGridColumn30 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("State")
        Dim UltraGridColumn31 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AcId")
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtDeployementSchedule = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtWarnty = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtTermsAndCondition = New System.Windows.Forms.TextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtProudctAndCategory = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTerminationCondition = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtWarrantyCondition = New System.Windows.Forms.TextBox()
        Me.lblTermandCondition = New System.Windows.Forms.Label()
        Me.txtTermAndCondition = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblPQty = New System.Windows.Forms.Label()
        Me.rdoName = New System.Windows.Forms.RadioButton()
        Me.rdoCode = New System.Windows.Forms.RadioButton()
        Me.txtPackQty = New System.Windows.Forms.TextBox()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbVendor = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDiscount = New System.Windows.Forms.TextBox()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.lblFax = New System.Windows.Forms.Label()
        Me.lblPhone = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtFaxNo = New System.Windows.Forms.TextBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.lblTerritory = New System.Windows.Forms.Label()
        Me.lblCity = New System.Windows.Forms.Label()
        Me.lblState = New System.Windows.Forms.Label()
        Me.cmbTerritory = New System.Windows.Forms.ComboBox()
        Me.cmbCity = New System.Windows.Forms.ComboBox()
        Me.cmbState = New System.Windows.Forms.ComboBox()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.lblBusinessType = New System.Windows.Forms.Label()
        Me.cmbBusinessType = New System.Windows.Forms.ComboBox()
        Me.lblContactName = New System.Windows.Forms.Label()
        Me.txtContactName = New System.Windows.Forms.TextBox()
        Me.lblBusinessName = New System.Windows.Forms.Label()
        Me.txtBusinessName = New System.Windows.Forms.TextBox()
        Me.grp = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtFirstPayment = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpDeliveryDate = New System.Windows.Forms.DateTimePicker()
        Me.lblAgreementType = New System.Windows.Forms.Label()
        Me.cmbAgreementType = New System.Windows.Forms.ComboBox()
        Me.chkPost = New System.Windows.Forms.CheckBox()
        Me.txtAgreementNo = New System.Windows.Forms.TextBox()
        Me.lblAgreementDate = New System.Windows.Forms.Label()
        Me.dtpAgreementDate = New System.Windows.Forms.DateTimePicker()
        Me.lblAgreementNo = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAddItem = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAttachments = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnLoadAll = New System.Windows.Forms.ToolStripButton()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grp.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox4)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox3)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox2)
        Me.UltraTabPageControl1.Controls.Add(Me.SplitContainer1)
        Me.UltraTabPageControl1.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Controls.Add(Me.grp)
        Me.UltraTabPageControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1249, 983)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1249, 77)
        Me.pnlHeader.TabIndex = 26
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(27, 22)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(184, 41)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Agreement"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.txtDeployementSchedule)
        Me.GroupBox4.Location = New System.Drawing.Point(710, 357)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox4.Size = New System.Drawing.Size(370, 154)
        Me.GroupBox4.TabIndex = 5
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Deployement Schedule"
        '
        'txtDeployementSchedule
        '
        Me.txtDeployementSchedule.Location = New System.Drawing.Point(9, 29)
        Me.txtDeployementSchedule.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDeployementSchedule.Multiline = True
        Me.txtDeployementSchedule.Name = "txtDeployementSchedule"
        Me.txtDeployementSchedule.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDeployementSchedule.Size = New System.Drawing.Size(319, 113)
        Me.txtDeployementSchedule.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.txtWarnty)
        Me.GroupBox3.Location = New System.Drawing.Point(370, 357)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox3.Size = New System.Drawing.Size(327, 154)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Warnty Period"
        '
        'txtWarnty
        '
        Me.txtWarnty.Location = New System.Drawing.Point(9, 29)
        Me.txtWarnty.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtWarnty.Multiline = True
        Me.txtWarnty.Name = "txtWarnty"
        Me.txtWarnty.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWarnty.Size = New System.Drawing.Size(304, 113)
        Me.txtWarnty.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.txtTermsAndCondition)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 357)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(342, 154)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Terms and Condition"
        '
        'txtTermsAndCondition
        '
        Me.txtTermsAndCondition.Location = New System.Drawing.Point(9, 29)
        Me.txtTermsAndCondition.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTermsAndCondition.Multiline = True
        Me.txtTermsAndCondition.Name = "txtTermsAndCondition"
        Me.txtTermsAndCondition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTermsAndCondition.Size = New System.Drawing.Size(314, 113)
        Me.txtTermsAndCondition.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 628)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.grd)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtProudctAndCategory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtTerminationCondition)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtWarrantyCondition)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblTermandCondition)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtTermAndCondition)
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Size = New System.Drawing.Size(1246, 363)
        Me.SplitContainer1.SplitterDistance = 173
        Me.SplitContainer1.SplitterWidth = 6
        Me.SplitContainer1.TabIndex = 4
        '
        'grd
        '
        Me.grd.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(0, 0)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1246, 363)
        Me.grd.TabIndex = 0
        Me.grd.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grd.TabStop = False
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(758, 37)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(165, 20)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Product And Category"
        '
        'txtProudctAndCategory
        '
        Me.txtProudctAndCategory.Location = New System.Drawing.Point(972, 32)
        Me.txtProudctAndCategory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtProudctAndCategory.Multiline = True
        Me.txtProudctAndCategory.Name = "txtProudctAndCategory"
        Me.txtProudctAndCategory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtProudctAndCategory.Size = New System.Drawing.Size(492, 96)
        Me.txtProudctAndCategory.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(758, 145)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(193, 20)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Termination of Agreement"
        '
        'txtTerminationCondition
        '
        Me.txtTerminationCondition.Location = New System.Drawing.Point(972, 140)
        Me.txtTerminationCondition.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTerminationCondition.Multiline = True
        Me.txtTerminationCondition.Name = "txtTerminationCondition"
        Me.txtTerminationCondition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTerminationCondition.Size = New System.Drawing.Size(492, 96)
        Me.txtTerminationCondition.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 145)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Warranty"
        '
        'txtWarrantyCondition
        '
        Me.txtWarrantyCondition.Location = New System.Drawing.Point(190, 140)
        Me.txtWarrantyCondition.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtWarrantyCondition.Multiline = True
        Me.txtWarrantyCondition.Name = "txtWarrantyCondition"
        Me.txtWarrantyCondition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWarrantyCondition.Size = New System.Drawing.Size(492, 96)
        Me.txtWarrantyCondition.TabIndex = 3
        '
        'lblTermandCondition
        '
        Me.lblTermandCondition.AutoSize = True
        Me.lblTermandCondition.Location = New System.Drawing.Point(27, 32)
        Me.lblTermandCondition.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTermandCondition.Name = "lblTermandCondition"
        Me.lblTermandCondition.Size = New System.Drawing.Size(149, 20)
        Me.lblTermandCondition.TabIndex = 0
        Me.lblTermandCondition.Text = "Term And Condition"
        '
        'txtTermAndCondition
        '
        Me.txtTermAndCondition.Location = New System.Drawing.Point(190, 32)
        Me.txtTermAndCondition.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTermAndCondition.Multiline = True
        Me.txtTermAndCondition.Name = "txtTermAndCondition"
        Me.txtTermAndCondition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTermAndCondition.Size = New System.Drawing.Size(492, 96)
        Me.txtTermAndCondition.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.lblPQty)
        Me.Panel1.Controls.Add(Me.rdoName)
        Me.Panel1.Controls.Add(Me.rdoCode)
        Me.Panel1.Controls.Add(Me.txtPackQty)
        Me.Panel1.Controls.Add(Me.lblUnit)
        Me.Panel1.Controls.Add(Me.cmbItem)
        Me.Panel1.Controls.Add(Me.lblLocation)
        Me.Panel1.Controls.Add(Me.lblItem)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.cmbCategory)
        Me.Panel1.Controls.Add(Me.cmbUnit)
        Me.Panel1.Controls.Add(Me.txtQty)
        Me.Panel1.Controls.Add(Me.txtRate)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.txtTotal)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Location = New System.Drawing.Point(20, 552)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1034, 72)
        Me.Panel1.TabIndex = 6
        '
        'lblPQty
        '
        Me.lblPQty.AutoSize = True
        Me.lblPQty.Location = New System.Drawing.Point(512, 6)
        Me.lblPQty.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPQty.Name = "lblPQty"
        Me.lblPQty.Size = New System.Drawing.Size(51, 20)
        Me.lblPQty.TabIndex = 8
        Me.lblPQty.Text = "P Qty:"
        '
        'rdoName
        '
        Me.rdoName.AutoSize = True
        Me.rdoName.Location = New System.Drawing.Point(282, 2)
        Me.rdoName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdoName.Name = "rdoName"
        Me.rdoName.Size = New System.Drawing.Size(76, 24)
        Me.rdoName.TabIndex = 4
        Me.rdoName.Text = "Name"
        Me.rdoName.UseVisualStyleBackColor = True
        '
        'rdoCode
        '
        Me.rdoCode.AutoSize = True
        Me.rdoCode.Checked = True
        Me.rdoCode.Location = New System.Drawing.Point(198, 2)
        Me.rdoCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdoCode.Name = "rdoCode"
        Me.rdoCode.Size = New System.Drawing.Size(72, 24)
        Me.rdoCode.TabIndex = 3
        Me.rdoCode.TabStop = True
        Me.rdoCode.Text = "Code"
        Me.rdoCode.UseVisualStyleBackColor = True
        '
        'txtPackQty
        '
        Me.txtPackQty.Location = New System.Drawing.Point(516, 29)
        Me.txtPackQty.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPackQty.Name = "txtPackQty"
        Me.txtPackQty.Size = New System.Drawing.Size(79, 26)
        Me.txtPackQty.TabIndex = 9
        Me.txtPackQty.TabStop = False
        '
        'lblUnit
        '
        Me.lblUnit.AutoSize = True
        Me.lblUnit.Location = New System.Drawing.Point(394, 3)
        Me.lblUnit.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(42, 20)
        Me.lblUnit.TabIndex = 6
        Me.lblUnit.Text = "Unit:"
        '
        'cmbItem
        '
        Me.cmbItem.AlwaysInEditMode = True
        Me.cmbItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.Color.White
        Appearance1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbItem.DisplayLayout.Appearance = Appearance1
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4})
        Me.cmbItem.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbItem.DisplayLayout.InterBandSpacing = 10
        Me.cmbItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbItem.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbItem.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.Color.Transparent
        Me.cmbItem.DisplayLayout.Override.CardAreaAppearance = Appearance2
        Me.cmbItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbItem.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance3.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance3.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance3.ForeColor = System.Drawing.Color.White
        Appearance3.TextHAlignAsString = "Left"
        Appearance3.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbItem.DisplayLayout.Override.HeaderAppearance = Appearance3
        Me.cmbItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem.DisplayLayout.Override.RowAppearance = Appearance4
        Appearance5.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance5.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbItem.DisplayLayout.Override.RowSelectorAppearance = Appearance5
        Me.cmbItem.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbItem.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance6.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance6.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.cmbItem.DisplayLayout.Override.SelectedRowAppearance = Appearance6
        Me.cmbItem.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbItem.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbItem.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbItem.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbItem.LimitToList = True
        Me.cmbItem.Location = New System.Drawing.Point(148, 28)
        Me.cmbItem.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbItem.MaxDropDownItems = 20
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(242, 29)
        Me.cmbItem.TabIndex = 5
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = True
        Me.lblLocation.Location = New System.Drawing.Point(9, 3)
        Me.lblLocation.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(74, 20)
        Me.lblLocation.TabIndex = 0
        Me.lblLocation.Text = "Location:"
        '
        'lblItem
        '
        Me.lblItem.AutoSize = True
        Me.lblItem.Location = New System.Drawing.Point(144, 5)
        Me.lblItem.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(45, 20)
        Me.lblItem.TabIndex = 2
        Me.lblItem.Text = "Item:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(600, 6)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(37, 20)
        Me.Label18.TabIndex = 10
        Me.Label18.Text = "Qty:"
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(14, 28)
        Me.cmbCategory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(124, 28)
        Me.cmbCategory.TabIndex = 1
        '
        'cmbUnit
        '
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.FormattingEnabled = True
        Me.cmbUnit.Items.AddRange(New Object() {"Loose", "Pack"})
        Me.cmbUnit.Location = New System.Drawing.Point(399, 29)
        Me.cmbUnit.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.Size = New System.Drawing.Size(110, 28)
        Me.cmbUnit.TabIndex = 7
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(602, 29)
        Me.txtQty.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(74, 26)
        Me.txtQty.TabIndex = 11
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRate
        '
        Me.txtRate.Location = New System.Drawing.Point(686, 29)
        Me.txtRate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(90, 26)
        Me.txtRate.TabIndex = 13
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(897, 26)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(64, 35)
        Me.btnAdd.TabIndex = 16
        Me.btnAdd.Text = "+"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(786, 29)
        Me.txtTotal.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(100, 26)
        Me.txtTotal.TabIndex = 15
        Me.txtTotal.TabStop = False
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(782, 6)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(48, 20)
        Me.Label20.TabIndex = 14
        Me.Label20.Text = "Total:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(681, 6)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(48, 20)
        Me.Label21.TabIndex = 12
        Me.Label21.Text = "Rate:"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.cmbVendor)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtDiscount)
        Me.GroupBox1.Controls.Add(Me.lblEmail)
        Me.GroupBox1.Controls.Add(Me.lblFax)
        Me.GroupBox1.Controls.Add(Me.lblPhone)
        Me.GroupBox1.Controls.Add(Me.txtEmail)
        Me.GroupBox1.Controls.Add(Me.txtFaxNo)
        Me.GroupBox1.Controls.Add(Me.txtPhone)
        Me.GroupBox1.Controls.Add(Me.lblTerritory)
        Me.GroupBox1.Controls.Add(Me.lblCity)
        Me.GroupBox1.Controls.Add(Me.lblState)
        Me.GroupBox1.Controls.Add(Me.cmbTerritory)
        Me.GroupBox1.Controls.Add(Me.cmbCity)
        Me.GroupBox1.Controls.Add(Me.cmbState)
        Me.GroupBox1.Controls.Add(Me.lblAddress)
        Me.GroupBox1.Controls.Add(Me.txtAddress)
        Me.GroupBox1.Controls.Add(Me.lblBusinessType)
        Me.GroupBox1.Controls.Add(Me.cmbBusinessType)
        Me.GroupBox1.Controls.Add(Me.lblContactName)
        Me.GroupBox1.Controls.Add(Me.txtContactName)
        Me.GroupBox1.Controls.Add(Me.lblBusinessName)
        Me.GroupBox1.Controls.Add(Me.txtBusinessName)
        Me.GroupBox1.Location = New System.Drawing.Point(387, 77)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(693, 271)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'cmbVendor
        '
        Me.cmbVendor.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbVendor.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbVendor.CheckedListSettings.CheckStateMember = ""
        Appearance22.BackColor = System.Drawing.Color.White
        Me.cmbVendor.DisplayLayout.Appearance = Appearance22
        UltraGridColumn26.Header.VisiblePosition = 0
        UltraGridColumn26.Hidden = True
        UltraGridColumn27.Header.VisiblePosition = 1
        UltraGridColumn27.Width = 141
        UltraGridColumn28.Header.VisiblePosition = 2
        UltraGridColumn29.Header.VisiblePosition = 3
        UltraGridColumn30.Header.VisiblePosition = 4
        UltraGridColumn31.Header.VisiblePosition = 5
        UltraGridColumn31.Hidden = True
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn26, UltraGridColumn27, UltraGridColumn28, UltraGridColumn29, UltraGridColumn30, UltraGridColumn31})
        Me.cmbVendor.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.cmbVendor.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbVendor.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbVendor.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance23.BackColor = System.Drawing.Color.Transparent
        Me.cmbVendor.DisplayLayout.Override.CardAreaAppearance = Appearance23
        Me.cmbVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbVendor.DisplayLayout.Override.CellPadding = 3
        Me.cmbVendor.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance24.TextHAlignAsString = "Left"
        Me.cmbVendor.DisplayLayout.Override.HeaderAppearance = Appearance24
        Me.cmbVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance25.BorderColor = System.Drawing.Color.LightGray
        Appearance25.TextVAlignAsString = "Middle"
        Me.cmbVendor.DisplayLayout.Override.RowAppearance = Appearance25
        Appearance26.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance26.BorderColor = System.Drawing.Color.Black
        Appearance26.ForeColor = System.Drawing.Color.Black
        Me.cmbVendor.DisplayLayout.Override.SelectedRowAppearance = Appearance26
        Me.cmbVendor.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbVendor.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbVendor.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbVendor.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbVendor.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbVendor.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear
        Me.cmbVendor.LimitToList = True
        Me.cmbVendor.Location = New System.Drawing.Point(144, 106)
        Me.cmbVendor.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbVendor.Name = "cmbVendor"
        Me.cmbVendor.Size = New System.Drawing.Size(171, 29)
        Me.cmbVendor.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbVendor, "Select Customer To Sales")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 114)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(124, 20)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Customer Name"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(334, 234)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 20)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Discount"
        '
        'txtDiscount
        '
        Me.txtDiscount.Location = New System.Drawing.Point(464, 229)
        Me.txtDiscount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.Size = New System.Drawing.Size(187, 26)
        Me.txtDiscount.TabIndex = 23
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(334, 195)
        Me.lblEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(48, 20)
        Me.lblEmail.TabIndex = 20
        Me.lblEmail.Text = "Email"
        '
        'lblFax
        '
        Me.lblFax.AutoSize = True
        Me.lblFax.Location = New System.Drawing.Point(334, 154)
        Me.lblFax.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFax.Name = "lblFax"
        Me.lblFax.Size = New System.Drawing.Size(35, 20)
        Me.lblFax.TabIndex = 18
        Me.lblFax.Text = "Fax"
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Location = New System.Drawing.Point(334, 114)
        Me.lblPhone.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(55, 20)
        Me.lblPhone.TabIndex = 16
        Me.lblPhone.Text = "Phone"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(464, 189)
        Me.txtEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(187, 26)
        Me.txtEmail.TabIndex = 21
        '
        'txtFaxNo
        '
        Me.txtFaxNo.Location = New System.Drawing.Point(464, 149)
        Me.txtFaxNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFaxNo.Name = "txtFaxNo"
        Me.txtFaxNo.Size = New System.Drawing.Size(187, 26)
        Me.txtFaxNo.TabIndex = 19
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(464, 109)
        Me.txtPhone.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(187, 26)
        Me.txtPhone.TabIndex = 17
        '
        'lblTerritory
        '
        Me.lblTerritory.AutoSize = True
        Me.lblTerritory.Location = New System.Drawing.Point(15, 231)
        Me.lblTerritory.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTerritory.Name = "lblTerritory"
        Me.lblTerritory.Size = New System.Drawing.Size(66, 20)
        Me.lblTerritory.TabIndex = 10
        Me.lblTerritory.Text = "Territory"
        '
        'lblCity
        '
        Me.lblCity.AutoSize = True
        Me.lblCity.Location = New System.Drawing.Point(15, 191)
        Me.lblCity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(35, 20)
        Me.lblCity.TabIndex = 8
        Me.lblCity.Text = "City"
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.Location = New System.Drawing.Point(15, 151)
        Me.lblState.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(48, 20)
        Me.lblState.TabIndex = 6
        Me.lblState.Text = "State"
        '
        'cmbTerritory
        '
        Me.cmbTerritory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbTerritory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbTerritory.FormattingEnabled = True
        Me.cmbTerritory.Location = New System.Drawing.Point(144, 229)
        Me.cmbTerritory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbTerritory.Name = "cmbTerritory"
        Me.cmbTerritory.Size = New System.Drawing.Size(169, 28)
        Me.cmbTerritory.TabIndex = 11
        '
        'cmbCity
        '
        Me.cmbCity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbCity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCity.FormattingEnabled = True
        Me.cmbCity.Location = New System.Drawing.Point(144, 188)
        Me.cmbCity.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCity.Name = "cmbCity"
        Me.cmbCity.Size = New System.Drawing.Size(169, 28)
        Me.cmbCity.TabIndex = 9
        '
        'cmbState
        '
        Me.cmbState.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbState.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbState.FormattingEnabled = True
        Me.cmbState.Location = New System.Drawing.Point(144, 146)
        Me.cmbState.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbState.Name = "cmbState"
        Me.cmbState.Size = New System.Drawing.Size(169, 28)
        Me.cmbState.TabIndex = 7
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.Location = New System.Drawing.Point(334, 74)
        Me.lblAddress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(68, 20)
        Me.lblAddress.TabIndex = 14
        Me.lblAddress.Text = "Address"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(464, 69)
        Me.txtAddress.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(187, 26)
        Me.txtAddress.TabIndex = 15
        '
        'lblBusinessType
        '
        Me.lblBusinessType.AutoSize = True
        Me.lblBusinessType.Location = New System.Drawing.Point(334, 34)
        Me.lblBusinessType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBusinessType.Name = "lblBusinessType"
        Me.lblBusinessType.Size = New System.Drawing.Size(112, 20)
        Me.lblBusinessType.TabIndex = 12
        Me.lblBusinessType.Text = "Business Type"
        '
        'cmbBusinessType
        '
        Me.cmbBusinessType.FormattingEnabled = True
        Me.cmbBusinessType.Location = New System.Drawing.Point(464, 29)
        Me.cmbBusinessType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbBusinessType.Name = "cmbBusinessType"
        Me.cmbBusinessType.Size = New System.Drawing.Size(187, 28)
        Me.cmbBusinessType.TabIndex = 13
        '
        'lblContactName
        '
        Me.lblContactName.AutoSize = True
        Me.lblContactName.Location = New System.Drawing.Point(15, 74)
        Me.lblContactName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblContactName.Name = "lblContactName"
        Me.lblContactName.Size = New System.Drawing.Size(111, 20)
        Me.lblContactName.TabIndex = 2
        Me.lblContactName.Text = "Contact Name"
        '
        'txtContactName
        '
        Me.txtContactName.Location = New System.Drawing.Point(144, 69)
        Me.txtContactName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtContactName.Name = "txtContactName"
        Me.txtContactName.Size = New System.Drawing.Size(169, 26)
        Me.txtContactName.TabIndex = 3
        '
        'lblBusinessName
        '
        Me.lblBusinessName.AutoSize = True
        Me.lblBusinessName.Location = New System.Drawing.Point(15, 34)
        Me.lblBusinessName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBusinessName.Name = "lblBusinessName"
        Me.lblBusinessName.Size = New System.Drawing.Size(120, 20)
        Me.lblBusinessName.TabIndex = 0
        Me.lblBusinessName.Text = "Business Name"
        '
        'txtBusinessName
        '
        Me.txtBusinessName.Location = New System.Drawing.Point(144, 29)
        Me.txtBusinessName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBusinessName.Name = "txtBusinessName"
        Me.txtBusinessName.Size = New System.Drawing.Size(169, 26)
        Me.txtBusinessName.TabIndex = 1
        '
        'grp
        '
        Me.grp.BackColor = System.Drawing.Color.Transparent
        Me.grp.Controls.Add(Me.Label6)
        Me.grp.Controls.Add(Me.txtFirstPayment)
        Me.grp.Controls.Add(Me.Label5)
        Me.grp.Controls.Add(Me.dtpDeliveryDate)
        Me.grp.Controls.Add(Me.lblAgreementType)
        Me.grp.Controls.Add(Me.cmbAgreementType)
        Me.grp.Controls.Add(Me.chkPost)
        Me.grp.Controls.Add(Me.txtAgreementNo)
        Me.grp.Controls.Add(Me.lblAgreementDate)
        Me.grp.Controls.Add(Me.dtpAgreementDate)
        Me.grp.Controls.Add(Me.lblAgreementNo)
        Me.grp.Location = New System.Drawing.Point(20, 77)
        Me.grp.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grp.Name = "grp"
        Me.grp.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grp.Size = New System.Drawing.Size(358, 271)
        Me.grp.TabIndex = 0
        Me.grp.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 154)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(106, 20)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "First Payment"
        '
        'txtFirstPayment
        '
        Me.txtFirstPayment.Location = New System.Drawing.Point(171, 151)
        Me.txtFirstPayment.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFirstPayment.Name = "txtFirstPayment"
        Me.txtFirstPayment.Size = New System.Drawing.Size(157, 26)
        Me.txtFirstPayment.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 117)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 20)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Delivery Date"
        '
        'dtpDeliveryDate
        '
        Me.dtpDeliveryDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDeliveryDate.Location = New System.Drawing.Point(171, 111)
        Me.dtpDeliveryDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDeliveryDate.Name = "dtpDeliveryDate"
        Me.dtpDeliveryDate.Size = New System.Drawing.Size(157, 26)
        Me.dtpDeliveryDate.TabIndex = 5
        '
        'lblAgreementType
        '
        Me.lblAgreementType.AutoSize = True
        Me.lblAgreementType.Location = New System.Drawing.Point(12, 194)
        Me.lblAgreementType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAgreementType.Name = "lblAgreementType"
        Me.lblAgreementType.Size = New System.Drawing.Size(126, 20)
        Me.lblAgreementType.TabIndex = 8
        Me.lblAgreementType.Text = "Agreement Type"
        '
        'cmbAgreementType
        '
        Me.cmbAgreementType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbAgreementType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAgreementType.FormattingEnabled = True
        Me.cmbAgreementType.Items.AddRange(New Object() {"New Contract", "Repaire and Mantinance Contract", ""})
        Me.cmbAgreementType.Location = New System.Drawing.Point(171, 191)
        Me.cmbAgreementType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbAgreementType.Name = "cmbAgreementType"
        Me.cmbAgreementType.Size = New System.Drawing.Size(157, 28)
        Me.cmbAgreementType.TabIndex = 9
        '
        'chkPost
        '
        Me.chkPost.AutoSize = True
        Me.chkPost.Checked = True
        Me.chkPost.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPost.Location = New System.Drawing.Point(172, 231)
        Me.chkPost.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPost.Name = "chkPost"
        Me.chkPost.Size = New System.Drawing.Size(67, 24)
        Me.chkPost.TabIndex = 10
        Me.chkPost.Text = "Post"
        Me.chkPost.UseVisualStyleBackColor = True
        '
        'txtAgreementNo
        '
        Me.txtAgreementNo.Location = New System.Drawing.Point(171, 29)
        Me.txtAgreementNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAgreementNo.Name = "txtAgreementNo"
        Me.txtAgreementNo.ReadOnly = True
        Me.txtAgreementNo.Size = New System.Drawing.Size(157, 26)
        Me.txtAgreementNo.TabIndex = 1
        '
        'lblAgreementDate
        '
        Me.lblAgreementDate.AutoSize = True
        Me.lblAgreementDate.Location = New System.Drawing.Point(12, 75)
        Me.lblAgreementDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAgreementDate.Name = "lblAgreementDate"
        Me.lblAgreementDate.Size = New System.Drawing.Size(127, 20)
        Me.lblAgreementDate.TabIndex = 2
        Me.lblAgreementDate.Text = "Agreement Date"
        '
        'dtpAgreementDate
        '
        Me.dtpAgreementDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpAgreementDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAgreementDate.Location = New System.Drawing.Point(171, 69)
        Me.dtpAgreementDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpAgreementDate.Name = "dtpAgreementDate"
        Me.dtpAgreementDate.Size = New System.Drawing.Size(157, 26)
        Me.dtpAgreementDate.TabIndex = 3
        '
        'lblAgreementNo
        '
        Me.lblAgreementNo.AutoSize = True
        Me.lblAgreementNo.Location = New System.Drawing.Point(12, 35)
        Me.lblAgreementNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAgreementNo.Name = "lblAgreementNo"
        Me.lblAgreementNo.Size = New System.Drawing.Size(112, 20)
        Me.lblAgreementNo.TabIndex = 0
        Me.lblAgreementNo.Text = "Agreement No"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1249, 983)
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(1249, 983)
        Me.grdSaved.TabIndex = 0
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnPrint, Me.toolStripSeparator, Me.btnDelete, Me.toolStripSeparator1, Me.btnAddItem, Me.ToolStripSeparator2, Me.btnAttachments, Me.btnRefresh, Me.btnLoadAll, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1251, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 29)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(70, 29)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 29)
        Me.btnSave.Text = "&Save"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 29)
        Me.btnPrint.Text = "&Print"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 32)
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 29)
        Me.btnDelete.Text = "D&elete"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'btnAddItem
        '
        Me.btnAddItem.Image = Global.SimpleAccounts.My.Resources.Resources.Attendanceadd
        Me.btnAddItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAddItem.Name = "btnAddItem"
        Me.btnAddItem.Size = New System.Drawing.Size(115, 29)
        Me.btnAddItem.Text = "Add Item"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 32)
        '
        'btnAttachments
        '
        Me.btnAttachments.Image = Global.SimpleAccounts.My.Resources.Resources.attachment
        Me.btnAttachments.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAttachments.Name = "btnAttachments"
        Me.btnAttachments.Size = New System.Drawing.Size(132, 29)
        Me.btnAttachments.Text = "Attachment"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "Refresh"
        '
        'btnLoadAll
        '
        Me.btnLoadAll.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.btnLoadAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLoadAll.Name = "btnLoadAll"
        Me.btnLoadAll.Size = New System.Drawing.Size(104, 29)
        Me.btnLoadAll.Text = "Load All"
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(77, 29)
        Me.btnHelp.Text = "He&lp"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 32)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1251, 1010)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Agreement"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1249, 983)
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmAgreement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1251, 1042)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmAgreement"
        Me.Text = "Agreement"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grp.ResumeLayout(False)
        Me.grp.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblAgreementNo As System.Windows.Forms.Label
    Friend WithEvents dtpAgreementDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtAgreementNo As System.Windows.Forms.TextBox
    Friend WithEvents lblAgreementDate As System.Windows.Forms.Label
    Friend WithEvents grp As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtContactName As System.Windows.Forms.TextBox
    Friend WithEvents lblBusinessName As System.Windows.Forms.Label
    Friend WithEvents txtBusinessName As System.Windows.Forms.TextBox
    Friend WithEvents lblBusinessType As System.Windows.Forms.Label
    Friend WithEvents cmbBusinessType As System.Windows.Forms.ComboBox
    Friend WithEvents lblContactName As System.Windows.Forms.Label
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtFaxNo As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents lblTerritory As System.Windows.Forms.Label
    Friend WithEvents lblCity As System.Windows.Forms.Label
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents cmbTerritory As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCity As System.Windows.Forms.ComboBox
    Friend WithEvents cmbState As System.Windows.Forms.ComboBox
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents lblFax As System.Windows.Forms.Label
    Friend WithEvents lblPhone As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblPQty As System.Windows.Forms.Label
    Friend WithEvents rdoName As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCode As System.Windows.Forms.RadioButton
    Friend WithEvents txtPackQty As System.Windows.Forms.TextBox
    Friend WithEvents lblUnit As System.Windows.Forms.Label
    Friend WithEvents cmbItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents lblItem As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnLoadAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblTermandCondition As System.Windows.Forms.Label
    Friend WithEvents txtTermAndCondition As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTerminationCondition As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtWarrantyCondition As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtProudctAndCategory As System.Windows.Forms.TextBox
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnAddItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblAgreementType As System.Windows.Forms.Label
    Friend WithEvents cmbAgreementType As System.Windows.Forms.ComboBox
    Friend WithEvents chkPost As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpDeliveryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFirstPayment As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTermsAndCondition As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDeployementSchedule As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtWarnty As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnAttachments As System.Windows.Forms.ToolStripButton
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmbVendor As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class
