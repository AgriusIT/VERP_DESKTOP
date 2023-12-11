<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHolidySetup
    Inherits System.Windows.Forms.Form



    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHolidySetup))
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.plnEmp = New System.Windows.Forms.Panel()
        Me.lstAddedEmployees = New SimpleAccounts.uiListControl()
        Me.btnRemoveEmp = New System.Windows.Forms.Button()
        Me.btnAddedEmp = New System.Windows.Forms.Button()
        Me.PnlCriteria = New System.Windows.Forms.Panel()
        Me.RdbCostCenter = New System.Windows.Forms.RadioButton()
        Me.LblApplyTo = New System.Windows.Forms.Label()
        Me.Label120 = New System.Windows.Forms.Label()
        Me.RdbEmp = New System.Windows.Forms.RadioButton()
        Me.RdbDesig = New System.Windows.Forms.RadioButton()
        Me.RdbDept = New System.Windows.Forms.RadioButton()
        Me.RdbAll = New System.Windows.Forms.RadioButton()
        Me.LblRemarks = New System.Windows.Forms.Label()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.LblTotDays = New System.Windows.Forms.Label()
        Me.TxtTotDays = New System.Windows.Forms.TextBox()
        Me.LblEndDate = New System.Windows.Forms.Label()
        Me.DtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.LblStartDate = New System.Windows.Forms.Label()
        Me.DtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.LblStatus = New System.Windows.Forms.Label()
        Me.CmbStatus = New System.Windows.Forms.ComboBox()
        Me.PnlDat = New System.Windows.Forms.Panel()
        Me.LstCostCenter = New System.Windows.Forms.ListBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.LstEmployees = New SimpleAccounts.uiListControl()
        Me.LstDepartment = New System.Windows.Forms.ListBox()
        Me.LstDesignation = New System.Windows.Forms.ListBox()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GrdHistory = New Janus.Windows.GridEX.GridEX()
        Me.grdAttendanceHistory = New Janus.Windows.GridEX.GridEX()
        Me.LstDesig = New System.Windows.Forms.ListBox()
        Me.LstEmployee = New System.Windows.Forms.ListBox()
        Me.LstDepart = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbBelt = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.cmbZone = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.cmbRegion = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmbDirector = New System.Windows.Forms.ComboBox()
        Me.cmbSaleman = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbManager = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtCNG = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmbRootPlan = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.lblNTNNo = New System.Windows.Forms.Label()
        Me.lblSalesTax = New System.Windows.Forms.Label()
        Me.txtNTNNo = New System.Windows.Forms.TextBox()
        Me.txtSalesTaxNo = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.CmbCustomerTypes = New System.Windows.Forms.ComboBox()
        Me.TxtOtherExpn = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TxtFuel = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtDiscPer = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.uitxtName = New System.Windows.Forms.TextBox()
        Me.dtpExpiryDate = New System.Windows.Forms.DateTimePicker()
        Me.cmbState = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.uitxtPhone = New System.Windows.Forms.TextBox()
        Me.uichkActive = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.uitxtMobile = New System.Windows.Forms.TextBox()
        Me.uitxtSortOrder = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.uitxtAddress = New System.Windows.Forms.TextBox()
        Me.uitxtcomments = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.cmbCity = New System.Windows.Forms.ComboBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.uitxtCrditLimit = New System.Windows.Forms.TextBox()
        Me.cmbTerritory = New System.Windows.Forms.ComboBox()
        Me.uitxtEmail = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.ComboBox5 = New System.Windows.Forms.ComboBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.ComboBox6 = New System.Windows.Forms.ComboBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.ComboBox7 = New System.Windows.Forms.ComboBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.ComboBox8 = New System.Windows.Forms.ComboBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.ComboBox9 = New System.Windows.Forms.ComboBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.TextBox10 = New System.Windows.Forms.TextBox()
        Me.TextBox11 = New System.Windows.Forms.TextBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.TextBox12 = New System.Windows.Forms.TextBox()
        Me.TextBox13 = New System.Windows.Forms.TextBox()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.ComboBox10 = New System.Windows.Forms.ComboBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.TextBox14 = New System.Windows.Forms.TextBox()
        Me.ComboBox11 = New System.Windows.Forms.ComboBox()
        Me.TextBox15 = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.ComboBox12 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBox13 = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ComboBox14 = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ComboBox15 = New System.Windows.Forms.ComboBox()
        Me.ComboBox16 = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ComboBox17 = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBox16 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.ComboBox18 = New System.Windows.Forms.ComboBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.TextBox17 = New System.Windows.Forms.TextBox()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.TextBox18 = New System.Windows.Forms.TextBox()
        Me.TextBox19 = New System.Windows.Forms.TextBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.ComboBox19 = New System.Windows.Forms.ComboBox()
        Me.TextBox20 = New System.Windows.Forms.TextBox()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.TextBox21 = New System.Windows.Forms.TextBox()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.TextBox22 = New System.Windows.Forms.TextBox()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.TextBox23 = New System.Windows.Forms.TextBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.ComboBox20 = New System.Windows.Forms.ComboBox()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.TextBox24 = New System.Windows.Forms.TextBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.TextBox25 = New System.Windows.Forms.TextBox()
        Me.TextBox26 = New System.Windows.Forms.TextBox()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.TextBox27 = New System.Windows.Forms.TextBox()
        Me.TextBox28 = New System.Windows.Forms.TextBox()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.ComboBox21 = New System.Windows.Forms.ComboBox()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.TextBox29 = New System.Windows.Forms.TextBox()
        Me.ComboBox22 = New System.Windows.Forms.ComboBox()
        Me.TextBox30 = New System.Windows.Forms.TextBox()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.ComboBox23 = New System.Windows.Forms.ComboBox()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.ComboBox24 = New System.Windows.Forms.ComboBox()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.ComboBox25 = New System.Windows.Forms.ComboBox()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.ComboBox26 = New System.Windows.Forms.ComboBox()
        Me.ComboBox27 = New System.Windows.Forms.ComboBox()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.ComboBox28 = New System.Windows.Forms.ComboBox()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.TextBox31 = New System.Windows.Forms.TextBox()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.ComboBox29 = New System.Windows.Forms.ComboBox()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.TextBox32 = New System.Windows.Forms.TextBox()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.TextBox33 = New System.Windows.Forms.TextBox()
        Me.TextBox34 = New System.Windows.Forms.TextBox()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.ComboBox30 = New System.Windows.Forms.ComboBox()
        Me.TextBox35 = New System.Windows.Forms.TextBox()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.TextBox36 = New System.Windows.Forms.TextBox()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.TextBox37 = New System.Windows.Forms.TextBox()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.TextBox38 = New System.Windows.Forms.TextBox()
        Me.DateTimePicker3 = New System.Windows.Forms.DateTimePicker()
        Me.ComboBox31 = New System.Windows.Forms.ComboBox()
        Me.Label104 = New System.Windows.Forms.Label()
        Me.TextBox39 = New System.Windows.Forms.TextBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.Label105 = New System.Windows.Forms.Label()
        Me.Label106 = New System.Windows.Forms.Label()
        Me.TextBox40 = New System.Windows.Forms.TextBox()
        Me.TextBox41 = New System.Windows.Forms.TextBox()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.TextBox42 = New System.Windows.Forms.TextBox()
        Me.TextBox43 = New System.Windows.Forms.TextBox()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.ComboBox32 = New System.Windows.Forms.ComboBox()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.TextBox44 = New System.Windows.Forms.TextBox()
        Me.ComboBox33 = New System.Windows.Forms.ComboBox()
        Me.TextBox45 = New System.Windows.Forms.TextBox()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.PnDat = New System.Windows.Forms.Panel()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.BtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.plnEmp.SuspendLayout()
        Me.PnlCriteria.SuspendLayout()
        Me.PnlDat.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.GrdHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAttendanceHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.PnDat.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.plnEmp)
        Me.UltraTabPageControl1.Controls.Add(Me.PnlCriteria)
        Me.UltraTabPageControl1.Controls.Add(Me.PnlDat)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(2, 2)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1262, 662)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1262, 55)
        Me.pnlHeader.TabIndex = 0
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(9, 11)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(259, 36)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Attendance Setup"
        '
        'plnEmp
        '
        Me.plnEmp.BackColor = System.Drawing.Color.Transparent
        Me.plnEmp.Controls.Add(Me.lstAddedEmployees)
        Me.plnEmp.Controls.Add(Me.btnRemoveEmp)
        Me.plnEmp.Controls.Add(Me.btnAddedEmp)
        Me.plnEmp.Location = New System.Drawing.Point(842, 69)
        Me.plnEmp.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.plnEmp.Name = "plnEmp"
        Me.plnEmp.Size = New System.Drawing.Size(322, 538)
        Me.plnEmp.TabIndex = 2
        '
        'lstAddedEmployees
        '
        Me.lstAddedEmployees.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstAddedEmployees.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstAddedEmployees.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstAddedEmployees.BackColor = System.Drawing.Color.Transparent
        Me.lstAddedEmployees.disableWhenChecked = False
        Me.lstAddedEmployees.HeadingLabelName = Nothing
        Me.lstAddedEmployees.HeadingText = "Employee Added"
        Me.lstAddedEmployees.Location = New System.Drawing.Point(78, 0)
        Me.lstAddedEmployees.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstAddedEmployees.Name = "lstAddedEmployees"
        Me.lstAddedEmployees.ShowAddNewButton = False
        Me.lstAddedEmployees.ShowInverse = True
        Me.lstAddedEmployees.ShowMagnifierButton = False
        Me.lstAddedEmployees.ShowNoCheck = False
        Me.lstAddedEmployees.ShowResetAllButton = False
        Me.lstAddedEmployees.ShowSelectall = True
        Me.lstAddedEmployees.Size = New System.Drawing.Size(244, 538)
        Me.lstAddedEmployees.TabIndex = 36
        Me.lstAddedEmployees.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'btnRemoveEmp
        '
        Me.btnRemoveEmp.Location = New System.Drawing.Point(22, 232)
        Me.btnRemoveEmp.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnRemoveEmp.Name = "btnRemoveEmp"
        Me.btnRemoveEmp.Size = New System.Drawing.Size(46, 35)
        Me.btnRemoveEmp.TabIndex = 3
        Me.btnRemoveEmp.Text = "<<"
        Me.btnRemoveEmp.UseVisualStyleBackColor = True
        '
        'btnAddedEmp
        '
        Me.btnAddedEmp.Location = New System.Drawing.Point(22, 188)
        Me.btnAddedEmp.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAddedEmp.Name = "btnAddedEmp"
        Me.btnAddedEmp.Size = New System.Drawing.Size(46, 35)
        Me.btnAddedEmp.TabIndex = 2
        Me.btnAddedEmp.Text = ">>"
        Me.btnAddedEmp.UseVisualStyleBackColor = True
        '
        'PnlCriteria
        '
        Me.PnlCriteria.BackColor = System.Drawing.Color.Transparent
        Me.PnlCriteria.Controls.Add(Me.RdbCostCenter)
        Me.PnlCriteria.Controls.Add(Me.LblApplyTo)
        Me.PnlCriteria.Controls.Add(Me.Label120)
        Me.PnlCriteria.Controls.Add(Me.RdbEmp)
        Me.PnlCriteria.Controls.Add(Me.RdbDesig)
        Me.PnlCriteria.Controls.Add(Me.RdbDept)
        Me.PnlCriteria.Controls.Add(Me.RdbAll)
        Me.PnlCriteria.Controls.Add(Me.LblRemarks)
        Me.PnlCriteria.Controls.Add(Me.TxtRemarks)
        Me.PnlCriteria.Controls.Add(Me.LblTotDays)
        Me.PnlCriteria.Controls.Add(Me.TxtTotDays)
        Me.PnlCriteria.Controls.Add(Me.LblEndDate)
        Me.PnlCriteria.Controls.Add(Me.DtpEndDate)
        Me.PnlCriteria.Controls.Add(Me.LblStartDate)
        Me.PnlCriteria.Controls.Add(Me.DtpStartDate)
        Me.PnlCriteria.Controls.Add(Me.LblStatus)
        Me.PnlCriteria.Controls.Add(Me.CmbStatus)
        Me.PnlCriteria.Location = New System.Drawing.Point(18, 69)
        Me.PnlCriteria.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PnlCriteria.Name = "PnlCriteria"
        Me.PnlCriteria.Size = New System.Drawing.Size(396, 538)
        Me.PnlCriteria.TabIndex = 1
        '
        'RdbCostCenter
        '
        Me.RdbCostCenter.AutoSize = True
        Me.RdbCostCenter.Location = New System.Drawing.Point(172, 372)
        Me.RdbCostCenter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RdbCostCenter.Name = "RdbCostCenter"
        Me.RdbCostCenter.Size = New System.Drawing.Size(122, 24)
        Me.RdbCostCenter.TabIndex = 15
        Me.RdbCostCenter.Text = "Project Wise"
        Me.RdbCostCenter.UseVisualStyleBackColor = True
        '
        'LblApplyTo
        '
        Me.LblApplyTo.AutoSize = True
        Me.LblApplyTo.Location = New System.Drawing.Point(90, 231)
        Me.LblApplyTo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblApplyTo.Name = "LblApplyTo"
        Me.LblApplyTo.Size = New System.Drawing.Size(70, 20)
        Me.LblApplyTo.TabIndex = 10
        Me.LblApplyTo.Text = "Apply To"
        '
        'Label120
        '
        Me.Label120.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Label120.ForeColor = System.Drawing.Color.Navy
        Me.Label120.Location = New System.Drawing.Point(8, 443)
        Me.Label120.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(354, 69)
        Me.Label120.TabIndex = 16
        Me.Label120.Text = "Processing please wait ..."
        Me.Label120.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label120.Visible = False
        '
        'RdbEmp
        '
        Me.RdbEmp.AutoSize = True
        Me.RdbEmp.Location = New System.Drawing.Point(172, 337)
        Me.RdbEmp.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RdbEmp.Name = "RdbEmp"
        Me.RdbEmp.Size = New System.Drawing.Size(143, 24)
        Me.RdbEmp.TabIndex = 14
        Me.RdbEmp.Text = "Employee Wise"
        Me.RdbEmp.UseVisualStyleBackColor = True
        '
        'RdbDesig
        '
        Me.RdbDesig.AutoSize = True
        Me.RdbDesig.Location = New System.Drawing.Point(172, 302)
        Me.RdbDesig.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RdbDesig.Name = "RdbDesig"
        Me.RdbDesig.Size = New System.Drawing.Size(158, 24)
        Me.RdbDesig.TabIndex = 13
        Me.RdbDesig.Text = "Designation Wise"
        Me.RdbDesig.UseVisualStyleBackColor = True
        '
        'RdbDept
        '
        Me.RdbDept.AutoSize = True
        Me.RdbDept.Location = New System.Drawing.Point(172, 266)
        Me.RdbDept.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RdbDept.Name = "RdbDept"
        Me.RdbDept.Size = New System.Drawing.Size(158, 24)
        Me.RdbDept.TabIndex = 12
        Me.RdbDept.Text = "Department Wise"
        Me.RdbDept.UseVisualStyleBackColor = True
        '
        'RdbAll
        '
        Me.RdbAll.AutoSize = True
        Me.RdbAll.Checked = True
        Me.RdbAll.Location = New System.Drawing.Point(172, 231)
        Me.RdbAll.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RdbAll.Name = "RdbAll"
        Me.RdbAll.Size = New System.Drawing.Size(133, 24)
        Me.RdbAll.TabIndex = 11
        Me.RdbAll.TabStop = True
        Me.RdbAll.Text = "All Employees"
        Me.RdbAll.UseVisualStyleBackColor = True
        '
        'LblRemarks
        '
        Me.LblRemarks.AutoSize = True
        Me.LblRemarks.Location = New System.Drawing.Point(76, 195)
        Me.LblRemarks.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.Size = New System.Drawing.Size(73, 20)
        Me.LblRemarks.TabIndex = 8
        Me.LblRemarks.Text = "Remarks"
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AcceptsTab = True
        Me.TxtRemarks.Location = New System.Drawing.Point(172, 191)
        Me.TxtRemarks.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TxtRemarks.MaxLength = 100
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(205, 26)
        Me.TxtRemarks.TabIndex = 9
        '
        'LblTotDays
        '
        Me.LblTotDays.AutoSize = True
        Me.LblTotDays.Location = New System.Drawing.Point(63, 155)
        Me.LblTotDays.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblTotDays.Name = "LblTotDays"
        Me.LblTotDays.Size = New System.Drawing.Size(84, 20)
        Me.LblTotDays.TabIndex = 6
        Me.LblTotDays.Text = "Total Days"
        '
        'TxtTotDays
        '
        Me.TxtTotDays.Enabled = False
        Me.TxtTotDays.Location = New System.Drawing.Point(172, 151)
        Me.TxtTotDays.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TxtTotDays.MaxLength = 4
        Me.TxtTotDays.Name = "TxtTotDays"
        Me.TxtTotDays.Size = New System.Drawing.Size(205, 26)
        Me.TxtTotDays.TabIndex = 7
        '
        'LblEndDate
        '
        Me.LblEndDate.AutoSize = True
        Me.LblEndDate.Location = New System.Drawing.Point(72, 114)
        Me.LblEndDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblEndDate.Name = "LblEndDate"
        Me.LblEndDate.Size = New System.Drawing.Size(77, 20)
        Me.LblEndDate.TabIndex = 4
        Me.LblEndDate.Text = "End Date"
        '
        'DtpEndDate
        '
        Me.DtpEndDate.CustomFormat = "dd/MMM/yyyy"
        Me.DtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpEndDate.Location = New System.Drawing.Point(172, 105)
        Me.DtpEndDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DtpEndDate.Name = "DtpEndDate"
        Me.DtpEndDate.Size = New System.Drawing.Size(205, 26)
        Me.DtpEndDate.TabIndex = 5
        '
        'LblStartDate
        '
        Me.LblStartDate.AutoSize = True
        Me.LblStartDate.Location = New System.Drawing.Point(68, 65)
        Me.LblStartDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblStartDate.Name = "LblStartDate"
        Me.LblStartDate.Size = New System.Drawing.Size(83, 20)
        Me.LblStartDate.TabIndex = 2
        Me.LblStartDate.Text = "Start Date"
        '
        'DtpStartDate
        '
        Me.DtpStartDate.CustomFormat = "dd/MMM/yyyy"
        Me.DtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpStartDate.Location = New System.Drawing.Point(172, 65)
        Me.DtpStartDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DtpStartDate.Name = "DtpStartDate"
        Me.DtpStartDate.Size = New System.Drawing.Size(205, 26)
        Me.DtpStartDate.TabIndex = 3
        '
        'LblStatus
        '
        Me.LblStatus.AutoSize = True
        Me.LblStatus.Location = New System.Drawing.Point(8, 25)
        Me.LblStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.Size = New System.Drawing.Size(143, 20)
        Me.LblStatus.TabIndex = 0
        Me.LblStatus.Text = "Attendance Status"
        '
        'CmbStatus
        '
        Me.CmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbStatus.FormattingEnabled = True
        Me.CmbStatus.Items.AddRange(New Object() {"Present", "Absent", "Leave", "Half Leave", "Short Leave", "Casual Leave", "Anual Leave", "Sick Leave", "OD", "Break", "Day Off"})
        Me.CmbStatus.Location = New System.Drawing.Point(172, 20)
        Me.CmbStatus.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CmbStatus.Name = "CmbStatus"
        Me.CmbStatus.Size = New System.Drawing.Size(205, 28)
        Me.CmbStatus.TabIndex = 1
        '
        'PnlDat
        '
        Me.PnlDat.Controls.Add(Me.LstCostCenter)
        Me.PnlDat.Controls.Add(Me.txtSearch)
        Me.PnlDat.Controls.Add(Me.LstEmployees)
        Me.PnlDat.Controls.Add(Me.LstDepartment)
        Me.PnlDat.Controls.Add(Me.LstDesignation)
        Me.PnlDat.Location = New System.Drawing.Point(423, 69)
        Me.PnlDat.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PnlDat.Name = "PnlDat"
        Me.PnlDat.Size = New System.Drawing.Size(366, 538)
        Me.PnlDat.TabIndex = 1
        '
        'LstCostCenter
        '
        Me.LstCostCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LstCostCenter.FormattingEnabled = True
        Me.LstCostCenter.ItemHeight = 20
        Me.LstCostCenter.Location = New System.Drawing.Point(0, 512)
        Me.LstCostCenter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.LstCostCenter.Name = "LstCostCenter"
        Me.LstCostCenter.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.LstCostCenter.Size = New System.Drawing.Size(366, 0)
        Me.LstCostCenter.TabIndex = 37
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtSearch.Location = New System.Drawing.Point(0, 512)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(366, 26)
        Me.txtSearch.TabIndex = 36
        '
        'LstEmployees
        '
        Me.LstEmployees.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.LstEmployees.AutoScroll = True
        Me.LstEmployees.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.LstEmployees.BackColor = System.Drawing.Color.Transparent
        Me.LstEmployees.disableWhenChecked = False
        Me.LstEmployees.Dock = System.Windows.Forms.DockStyle.Top
        Me.LstEmployees.HeadingLabelName = Nothing
        Me.LstEmployees.HeadingText = "Employee List"
        Me.LstEmployees.Location = New System.Drawing.Point(0, 0)
        Me.LstEmployees.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.LstEmployees.Name = "LstEmployees"
        Me.LstEmployees.ShowAddNewButton = False
        Me.LstEmployees.ShowInverse = True
        Me.LstEmployees.ShowMagnifierButton = False
        Me.LstEmployees.ShowNoCheck = False
        Me.LstEmployees.ShowResetAllButton = False
        Me.LstEmployees.ShowSelectall = True
        Me.LstEmployees.Size = New System.Drawing.Size(366, 512)
        Me.LstEmployees.TabIndex = 0
        Me.LstEmployees.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'LstDepartment
        '
        Me.LstDepartment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LstDepartment.FormattingEnabled = True
        Me.LstDepartment.ItemHeight = 20
        Me.LstDepartment.Location = New System.Drawing.Point(0, 0)
        Me.LstDepartment.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.LstDepartment.Name = "LstDepartment"
        Me.LstDepartment.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.LstDepartment.Size = New System.Drawing.Size(366, 538)
        Me.LstDepartment.TabIndex = 0
        '
        'LstDesignation
        '
        Me.LstDesignation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LstDesignation.FormattingEnabled = True
        Me.LstDesignation.ItemHeight = 20
        Me.LstDesignation.Location = New System.Drawing.Point(0, 0)
        Me.LstDesignation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.LstDesignation.Name = "LstDesignation"
        Me.LstDesignation.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.LstDesignation.Size = New System.Drawing.Size(366, 538)
        Me.LstDesignation.TabIndex = 34
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.SplitContainer1)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1262, 662)
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GrdHistory)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.grdAttendanceHistory)
        Me.SplitContainer1.Size = New System.Drawing.Size(1262, 662)
        Me.SplitContainer1.SplitterDistance = 331
        Me.SplitContainer1.SplitterWidth = 6
        Me.SplitContainer1.TabIndex = 1
        '
        'GrdHistory
        '
        Me.GrdHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GrdHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrdHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GrdHistory.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GrdHistory.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GrdHistory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrdHistory.Location = New System.Drawing.Point(0, 0)
        Me.GrdHistory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GrdHistory.Name = "GrdHistory"
        Me.GrdHistory.RecordNavigator = True
        Me.GrdHistory.Size = New System.Drawing.Size(1262, 331)
        Me.GrdHistory.TabIndex = 0
        Me.GrdHistory.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'grdAttendanceHistory
        '
        Me.grdAttendanceHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAttendanceHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdAttendanceHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdAttendanceHistory.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdAttendanceHistory.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdAttendanceHistory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAttendanceHistory.Location = New System.Drawing.Point(0, 0)
        Me.grdAttendanceHistory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdAttendanceHistory.Name = "grdAttendanceHistory"
        Me.grdAttendanceHistory.RecordNavigator = True
        Me.grdAttendanceHistory.Size = New System.Drawing.Size(1262, 325)
        Me.grdAttendanceHistory.TabIndex = 1
        Me.grdAttendanceHistory.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'LstDesig
        '
        Me.LstDesig.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LstDesig.FormattingEnabled = True
        Me.LstDesig.Location = New System.Drawing.Point(0, 0)
        Me.LstDesig.Name = "LstDesig"
        Me.LstDesig.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.LstDesig.Size = New System.Drawing.Size(310, 376)
        Me.LstDesig.TabIndex = 34
        '
        'LstEmployee
        '
        Me.LstEmployee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LstEmployee.FormattingEnabled = True
        Me.LstEmployee.Location = New System.Drawing.Point(0, 0)
        Me.LstEmployee.Name = "LstEmployee"
        Me.LstEmployee.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.LstEmployee.Size = New System.Drawing.Size(310, 376)
        Me.LstEmployee.TabIndex = 33
        '
        'LstDepart
        '
        Me.LstDepart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LstDepart.FormattingEnabled = True
        Me.LstDepart.Location = New System.Drawing.Point(0, 0)
        Me.LstDepart.Name = "LstDepart"
        Me.LstDepart.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.LstDepart.Size = New System.Drawing.Size(310, 376)
        Me.LstDepart.TabIndex = 35
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.LightYellow
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(274, 138)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(263, 45)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Processing please wait ..."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label1.Visible = False
        '
        'cmbBelt
        '
        Me.cmbBelt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBelt.FormattingEnabled = True
        Me.cmbBelt.Location = New System.Drawing.Point(123, 202)
        Me.cmbBelt.Name = "cmbBelt"
        Me.cmbBelt.Size = New System.Drawing.Size(237, 28)
        Me.cmbBelt.TabIndex = 14
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(12, 205)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(25, 13)
        Me.Label26.TabIndex = 13
        Me.Label26.Text = "Belt"
        '
        'cmbZone
        '
        Me.cmbZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbZone.FormattingEnabled = True
        Me.cmbZone.Location = New System.Drawing.Point(123, 175)
        Me.cmbZone.Name = "cmbZone"
        Me.cmbZone.Size = New System.Drawing.Size(237, 28)
        Me.cmbZone.TabIndex = 12
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(12, 178)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(32, 13)
        Me.Label25.TabIndex = 11
        Me.Label25.Text = "Zone"
        '
        'cmbRegion
        '
        Me.cmbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRegion.FormattingEnabled = True
        Me.cmbRegion.Location = New System.Drawing.Point(123, 148)
        Me.cmbRegion.Name = "cmbRegion"
        Me.cmbRegion.Size = New System.Drawing.Size(237, 28)
        Me.cmbRegion.TabIndex = 10
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(12, 151)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(41, 13)
        Me.Label24.TabIndex = 9
        Me.Label24.Text = "Region"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(12, 367)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(91, 13)
        Me.Label21.TabIndex = 25
        Me.Label21.Text = "Report to Director"
        '
        'cmbDirector
        '
        Me.cmbDirector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDirector.FormattingEnabled = True
        Me.cmbDirector.Location = New System.Drawing.Point(123, 364)
        Me.cmbDirector.Name = "cmbDirector"
        Me.cmbDirector.Size = New System.Drawing.Size(237, 28)
        Me.cmbDirector.TabIndex = 26
        '
        'cmbSaleman
        '
        Me.cmbSaleman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSaleman.FormattingEnabled = True
        Me.cmbSaleman.Location = New System.Drawing.Point(123, 310)
        Me.cmbSaleman.Name = "cmbSaleman"
        Me.cmbSaleman.Size = New System.Drawing.Size(237, 28)
        Me.cmbSaleman.TabIndex = 22
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(12, 313)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(48, 13)
        Me.Label22.TabIndex = 21
        Me.Label22.Text = "Saleman"
        '
        'cmbManager
        '
        Me.cmbManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbManager.FormattingEnabled = True
        Me.cmbManager.Location = New System.Drawing.Point(123, 337)
        Me.cmbManager.Name = "cmbManager"
        Me.cmbManager.Size = New System.Drawing.Size(237, 28)
        Me.cmbManager.TabIndex = 24
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(12, 337)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(96, 13)
        Me.Label23.TabIndex = 23
        Me.Label23.Text = "Report to Manager"
        '
        'txtCNG
        '
        Me.txtCNG.Location = New System.Drawing.Point(557, 258)
        Me.txtCNG.Name = "txtCNG"
        Me.txtCNG.Size = New System.Drawing.Size(92, 26)
        Me.txtCNG.TabIndex = 50
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(522, 261)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(30, 13)
        Me.Label20.TabIndex = 49
        Me.Label20.Text = "CNG"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(12, 286)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(60, 13)
        Me.Label19.TabIndex = 19
        Me.Label19.Text = "Route Plan"
        '
        'cmbRootPlan
        '
        Me.cmbRootPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRootPlan.FormattingEnabled = True
        Me.cmbRootPlan.Location = New System.Drawing.Point(123, 283)
        Me.cmbRootPlan.Name = "cmbRootPlan"
        Me.cmbRootPlan.Size = New System.Drawing.Size(237, 28)
        Me.cmbRootPlan.TabIndex = 20
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(12, 71)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(79, 13)
        Me.Label18.TabIndex = 3
        Me.Label18.Text = "Customer Code"
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerCode.Location = New System.Drawing.Point(123, 68)
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(237, 26)
        Me.txtCustomerCode.TabIndex = 4
        '
        'lblNTNNo
        '
        Me.lblNTNNo.AutoSize = True
        Me.lblNTNNo.Location = New System.Drawing.Point(373, 149)
        Me.lblNTNNo.Name = "lblNTNNo"
        Me.lblNTNNo.Size = New System.Drawing.Size(50, 13)
        Me.lblNTNNo.TabIndex = 41
        Me.lblNTNNo.Text = "NTN No."
        '
        'lblSalesTax
        '
        Me.lblSalesTax.AutoSize = True
        Me.lblSalesTax.Location = New System.Drawing.Point(373, 175)
        Me.lblSalesTax.Name = "lblSalesTax"
        Me.lblSalesTax.Size = New System.Drawing.Size(74, 13)
        Me.lblSalesTax.TabIndex = 43
        Me.lblSalesTax.Text = "Sales Tax No."
        '
        'txtNTNNo
        '
        Me.txtNTNNo.Location = New System.Drawing.Point(450, 146)
        Me.txtNTNNo.Name = "txtNTNNo"
        Me.txtNTNNo.Size = New System.Drawing.Size(199, 26)
        Me.txtNTNNo.TabIndex = 42
        '
        'txtSalesTaxNo
        '
        Me.txtSalesTaxNo.Location = New System.Drawing.Point(449, 172)
        Me.txtSalesTaxNo.Name = "txtSalesTaxNo"
        Me.txtSalesTaxNo.Size = New System.Drawing.Size(199, 26)
        Me.txtSalesTaxNo.TabIndex = 44
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(12, 97)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(78, 13)
        Me.Label17.TabIndex = 5
        Me.Label17.Text = "Customer Type"
        '
        'CmbCustomerTypes
        '
        Me.CmbCustomerTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCustomerTypes.FormattingEnabled = True
        Me.CmbCustomerTypes.Location = New System.Drawing.Point(123, 94)
        Me.CmbCustomerTypes.Name = "CmbCustomerTypes"
        Me.CmbCustomerTypes.Size = New System.Drawing.Size(237, 28)
        Me.CmbCustomerTypes.TabIndex = 6
        '
        'TxtOtherExpn
        '
        Me.TxtOtherExpn.Location = New System.Drawing.Point(123, 477)
        Me.TxtOtherExpn.Name = "TxtOtherExpn"
        Me.TxtOtherExpn.Size = New System.Drawing.Size(237, 26)
        Me.TxtOtherExpn.TabIndex = 32
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(12, 480)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(82, 13)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "Other Expenses"
        '
        'TxtFuel
        '
        Me.TxtFuel.Location = New System.Drawing.Point(450, 258)
        Me.TxtFuel.Name = "TxtFuel"
        Me.TxtFuel.Size = New System.Drawing.Size(66, 26)
        Me.TxtFuel.TabIndex = 48
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(373, 261)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(30, 13)
        Me.Label15.TabIndex = 47
        Me.Label15.Text = "Fuel "
        '
        'txtDiscPer
        '
        Me.txtDiscPer.Location = New System.Drawing.Point(122, 451)
        Me.txtDiscPer.Name = "txtDiscPer"
        Me.txtDiscPer.Size = New System.Drawing.Size(237, 26)
        Me.txtDiscPer.TabIndex = 30
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 454)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Discount %age"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Customer Name"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Navy
        Me.Label14.Location = New System.Drawing.Point(9, 12)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(261, 23)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Customers Information"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(373, 315)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 54
        Me.Label5.Text = "End Date"
        '
        'uitxtName
        '
        Me.uitxtName.BackColor = System.Drawing.SystemColors.Window
        Me.uitxtName.Location = New System.Drawing.Point(123, 42)
        Me.uitxtName.Name = "uitxtName"
        Me.uitxtName.Size = New System.Drawing.Size(237, 26)
        Me.uitxtName.TabIndex = 2
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.Checked = False
        Me.dtpExpiryDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpiryDate.Location = New System.Drawing.Point(450, 312)
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.ShowCheckBox = True
        Me.dtpExpiryDate.Size = New System.Drawing.Size(102, 26)
        Me.dtpExpiryDate.TabIndex = 55
        '
        'cmbState
        '
        Me.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbState.FormattingEnabled = True
        Me.cmbState.Location = New System.Drawing.Point(123, 121)
        Me.cmbState.Name = "cmbState"
        Me.cmbState.Size = New System.Drawing.Size(237, 28)
        Me.cmbState.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 124)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Province Name"
        '
        'uitxtPhone
        '
        Me.uitxtPhone.Location = New System.Drawing.Point(450, 42)
        Me.uitxtPhone.Name = "uitxtPhone"
        Me.uitxtPhone.Size = New System.Drawing.Size(199, 26)
        Me.uitxtPhone.TabIndex = 34
        '
        'uichkActive
        '
        Me.uichkActive.AutoSize = True
        Me.uichkActive.Checked = True
        Me.uichkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.uichkActive.Location = New System.Drawing.Point(496, 286)
        Me.uichkActive.Name = "uichkActive"
        Me.uichkActive.Size = New System.Drawing.Size(56, 17)
        Me.uichkActive.TabIndex = 53
        Me.uichkActive.Text = "Active"
        Me.uichkActive.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(373, 45)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(38, 13)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Phone"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(373, 287)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(55, 13)
        Me.Label27.TabIndex = 51
        Me.Label27.Text = "Sort Order"
        '
        'uitxtMobile
        '
        Me.uitxtMobile.Location = New System.Drawing.Point(450, 68)
        Me.uitxtMobile.Name = "uitxtMobile"
        Me.uitxtMobile.Size = New System.Drawing.Size(199, 26)
        Me.uitxtMobile.TabIndex = 36
        '
        'uitxtSortOrder
        '
        Me.uitxtSortOrder.Location = New System.Drawing.Point(451, 284)
        Me.uitxtSortOrder.MaxLength = 50
        Me.uitxtSortOrder.Name = "uitxtSortOrder"
        Me.uitxtSortOrder.Size = New System.Drawing.Size(42, 26)
        Me.uitxtSortOrder.TabIndex = 52
        Me.uitxtSortOrder.Text = "1"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(373, 71)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(38, 13)
        Me.Label28.TabIndex = 35
        Me.Label28.Text = "Mobile"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(373, 201)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(59, 13)
        Me.Label29.TabIndex = 45
        Me.Label29.Text = "Comments:"
        '
        'uitxtAddress
        '
        Me.uitxtAddress.Location = New System.Drawing.Point(123, 391)
        Me.uitxtAddress.Multiline = True
        Me.uitxtAddress.Name = "uitxtAddress"
        Me.uitxtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.uitxtAddress.Size = New System.Drawing.Size(237, 54)
        Me.uitxtAddress.TabIndex = 28
        '
        'uitxtcomments
        '
        Me.uitxtcomments.Location = New System.Drawing.Point(450, 198)
        Me.uitxtcomments.Multiline = True
        Me.uitxtcomments.Name = "uitxtcomments"
        Me.uitxtcomments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.uitxtcomments.Size = New System.Drawing.Size(199, 54)
        Me.uitxtcomments.TabIndex = 46
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(12, 394)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(45, 13)
        Me.Label30.TabIndex = 27
        Me.Label30.Text = "Address"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(373, 123)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(58, 13)
        Me.Label31.TabIndex = 39
        Me.Label31.Text = "Credit Limit"
        '
        'cmbCity
        '
        Me.cmbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCity.FormattingEnabled = True
        Me.cmbCity.Location = New System.Drawing.Point(123, 229)
        Me.cmbCity.Name = "cmbCity"
        Me.cmbCity.Size = New System.Drawing.Size(237, 28)
        Me.cmbCity.TabIndex = 16
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(373, 97)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(36, 13)
        Me.Label32.TabIndex = 37
        Me.Label32.Text = "E-Mail"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(12, 232)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(55, 13)
        Me.Label33.TabIndex = 15
        Me.Label33.Text = "City Name"
        '
        'uitxtCrditLimit
        '
        Me.uitxtCrditLimit.Location = New System.Drawing.Point(450, 120)
        Me.uitxtCrditLimit.Name = "uitxtCrditLimit"
        Me.uitxtCrditLimit.Size = New System.Drawing.Size(199, 26)
        Me.uitxtCrditLimit.TabIndex = 40
        '
        'cmbTerritory
        '
        Me.cmbTerritory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTerritory.FormattingEnabled = True
        Me.cmbTerritory.Location = New System.Drawing.Point(123, 256)
        Me.cmbTerritory.Name = "cmbTerritory"
        Me.cmbTerritory.Size = New System.Drawing.Size(237, 28)
        Me.cmbTerritory.TabIndex = 18
        '
        'uitxtEmail
        '
        Me.uitxtEmail.Location = New System.Drawing.Point(450, 94)
        Me.uitxtEmail.Name = "uitxtEmail"
        Me.uitxtEmail.Size = New System.Drawing.Size(199, 26)
        Me.uitxtEmail.TabIndex = 38
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(12, 256)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(45, 13)
        Me.Label34.TabIndex = 17
        Me.Label34.Text = "Territory"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.LightYellow
        Me.Label35.ForeColor = System.Drawing.Color.Navy
        Me.Label35.Location = New System.Drawing.Point(274, 138)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(263, 45)
        Me.Label35.TabIndex = 56
        Me.Label35.Text = "Processing please wait ..."
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label35.Visible = False
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(123, 202)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox1.TabIndex = 14
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(12, 205)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(25, 13)
        Me.Label36.TabIndex = 13
        Me.Label36.Text = "Belt"
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(123, 175)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox2.TabIndex = 12
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(12, 178)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(32, 13)
        Me.Label37.TabIndex = 11
        Me.Label37.Text = "Zone"
        '
        'ComboBox3
        '
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(123, 148)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox3.TabIndex = 10
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(12, 151)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(41, 13)
        Me.Label38.TabIndex = 9
        Me.Label38.Text = "Region"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(12, 367)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(91, 13)
        Me.Label39.TabIndex = 25
        Me.Label39.Text = "Report to Director"
        '
        'ComboBox4
        '
        Me.ComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(123, 364)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox4.TabIndex = 26
        '
        'ComboBox5
        '
        Me.ComboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox5.FormattingEnabled = True
        Me.ComboBox5.Location = New System.Drawing.Point(123, 310)
        Me.ComboBox5.Name = "ComboBox5"
        Me.ComboBox5.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox5.TabIndex = 22
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(12, 313)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(48, 13)
        Me.Label40.TabIndex = 21
        Me.Label40.Text = "Saleman"
        '
        'ComboBox6
        '
        Me.ComboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox6.FormattingEnabled = True
        Me.ComboBox6.Location = New System.Drawing.Point(123, 337)
        Me.ComboBox6.Name = "ComboBox6"
        Me.ComboBox6.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox6.TabIndex = 24
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(12, 337)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(96, 13)
        Me.Label41.TabIndex = 23
        Me.Label41.Text = "Report to Manager"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(557, 258)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(92, 26)
        Me.TextBox1.TabIndex = 50
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(522, 261)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(30, 13)
        Me.Label42.TabIndex = 49
        Me.Label42.Text = "CNG"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(12, 286)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(60, 13)
        Me.Label43.TabIndex = 19
        Me.Label43.Text = "Route Plan"
        '
        'ComboBox7
        '
        Me.ComboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox7.FormattingEnabled = True
        Me.ComboBox7.Location = New System.Drawing.Point(123, 283)
        Me.ComboBox7.Name = "ComboBox7"
        Me.ComboBox7.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox7.TabIndex = 20
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(12, 71)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(79, 13)
        Me.Label44.TabIndex = 3
        Me.Label44.Text = "Customer Code"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox2.Location = New System.Drawing.Point(123, 68)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(237, 26)
        Me.TextBox2.TabIndex = 4
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(373, 149)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(50, 13)
        Me.Label45.TabIndex = 41
        Me.Label45.Text = "NTN No."
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(373, 175)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(74, 13)
        Me.Label46.TabIndex = 43
        Me.Label46.Text = "Sales Tax No."
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(450, 146)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(199, 26)
        Me.TextBox3.TabIndex = 42
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(449, 172)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(199, 26)
        Me.TextBox4.TabIndex = 44
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(12, 97)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(78, 13)
        Me.Label47.TabIndex = 5
        Me.Label47.Text = "Customer Type"
        '
        'ComboBox8
        '
        Me.ComboBox8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox8.FormattingEnabled = True
        Me.ComboBox8.Location = New System.Drawing.Point(123, 94)
        Me.ComboBox8.Name = "ComboBox8"
        Me.ComboBox8.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox8.TabIndex = 6
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(123, 477)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(237, 26)
        Me.TextBox5.TabIndex = 32
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(12, 480)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(82, 13)
        Me.Label48.TabIndex = 31
        Me.Label48.Text = "Other Expenses"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(450, 258)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(66, 26)
        Me.TextBox6.TabIndex = 48
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(373, 261)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(30, 13)
        Me.Label49.TabIndex = 47
        Me.Label49.Text = "Fuel "
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(122, 451)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(237, 26)
        Me.TextBox7.TabIndex = 30
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(12, 454)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(78, 13)
        Me.Label50.TabIndex = 29
        Me.Label50.Text = "Discount %age"
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(12, 45)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(82, 13)
        Me.Label51.TabIndex = 1
        Me.Label51.Text = "Customer Name"
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.Color.Navy
        Me.Label52.Location = New System.Drawing.Point(9, 12)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(261, 23)
        Me.Label52.TabIndex = 0
        Me.Label52.Text = "Customers Information"
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(373, 315)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(52, 13)
        Me.Label53.TabIndex = 54
        Me.Label53.Text = "End Date"
        '
        'TextBox8
        '
        Me.TextBox8.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox8.Location = New System.Drawing.Point(123, 42)
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(237, 26)
        Me.TextBox8.TabIndex = 2
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Checked = False
        Me.DateTimePicker1.Cursor = System.Windows.Forms.Cursors.Default
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(450, 312)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.ShowCheckBox = True
        Me.DateTimePicker1.Size = New System.Drawing.Size(102, 26)
        Me.DateTimePicker1.TabIndex = 55
        '
        'ComboBox9
        '
        Me.ComboBox9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox9.FormattingEnabled = True
        Me.ComboBox9.Location = New System.Drawing.Point(123, 121)
        Me.ComboBox9.Name = "ComboBox9"
        Me.ComboBox9.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox9.TabIndex = 8
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(12, 124)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(80, 13)
        Me.Label54.TabIndex = 7
        Me.Label54.Text = "Province Name"
        '
        'TextBox9
        '
        Me.TextBox9.Location = New System.Drawing.Point(450, 42)
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(199, 26)
        Me.TextBox9.TabIndex = 34
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Location = New System.Drawing.Point(496, 286)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(56, 17)
        Me.CheckBox1.TabIndex = 53
        Me.CheckBox1.Text = "Active"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(373, 45)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(38, 13)
        Me.Label55.TabIndex = 33
        Me.Label55.Text = "Phone"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(373, 287)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(55, 13)
        Me.Label56.TabIndex = 51
        Me.Label56.Text = "Sort Order"
        '
        'TextBox10
        '
        Me.TextBox10.Location = New System.Drawing.Point(450, 68)
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(199, 26)
        Me.TextBox10.TabIndex = 36
        '
        'TextBox11
        '
        Me.TextBox11.Location = New System.Drawing.Point(451, 284)
        Me.TextBox11.MaxLength = 50
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Size = New System.Drawing.Size(42, 26)
        Me.TextBox11.TabIndex = 52
        Me.TextBox11.Text = "1"
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(373, 71)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(38, 13)
        Me.Label57.TabIndex = 35
        Me.Label57.Text = "Mobile"
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(373, 201)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(59, 13)
        Me.Label58.TabIndex = 45
        Me.Label58.Text = "Comments:"
        '
        'TextBox12
        '
        Me.TextBox12.Location = New System.Drawing.Point(123, 391)
        Me.TextBox12.Multiline = True
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox12.Size = New System.Drawing.Size(237, 54)
        Me.TextBox12.TabIndex = 28
        '
        'TextBox13
        '
        Me.TextBox13.Location = New System.Drawing.Point(450, 198)
        Me.TextBox13.Multiline = True
        Me.TextBox13.Name = "TextBox13"
        Me.TextBox13.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox13.Size = New System.Drawing.Size(199, 54)
        Me.TextBox13.TabIndex = 46
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(12, 394)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(45, 13)
        Me.Label59.TabIndex = 27
        Me.Label59.Text = "Address"
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(373, 123)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(58, 13)
        Me.Label60.TabIndex = 39
        Me.Label60.Text = "Credit Limit"
        '
        'ComboBox10
        '
        Me.ComboBox10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox10.FormattingEnabled = True
        Me.ComboBox10.Location = New System.Drawing.Point(123, 229)
        Me.ComboBox10.Name = "ComboBox10"
        Me.ComboBox10.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox10.TabIndex = 16
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(373, 97)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(36, 13)
        Me.Label61.TabIndex = 37
        Me.Label61.Text = "E-Mail"
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(12, 232)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(55, 13)
        Me.Label62.TabIndex = 15
        Me.Label62.Text = "City Name"
        '
        'TextBox14
        '
        Me.TextBox14.Location = New System.Drawing.Point(450, 120)
        Me.TextBox14.Name = "TextBox14"
        Me.TextBox14.Size = New System.Drawing.Size(199, 26)
        Me.TextBox14.TabIndex = 40
        '
        'ComboBox11
        '
        Me.ComboBox11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox11.FormattingEnabled = True
        Me.ComboBox11.Location = New System.Drawing.Point(123, 256)
        Me.ComboBox11.Name = "ComboBox11"
        Me.ComboBox11.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox11.TabIndex = 18
        '
        'TextBox15
        '
        Me.TextBox15.Location = New System.Drawing.Point(450, 94)
        Me.TextBox15.Name = "TextBox15"
        Me.TextBox15.Size = New System.Drawing.Size(199, 26)
        Me.TextBox15.TabIndex = 38
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(12, 256)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(45, 13)
        Me.Label63.TabIndex = 17
        Me.Label63.Text = "Territory"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(274, 138)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 56
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'ComboBox12
        '
        Me.ComboBox12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox12.FormattingEnabled = True
        Me.ComboBox12.Location = New System.Drawing.Point(123, 202)
        Me.ComboBox12.Name = "ComboBox12"
        Me.ComboBox12.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox12.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 205)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(25, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Belt"
        '
        'ComboBox13
        '
        Me.ComboBox13.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox13.FormattingEnabled = True
        Me.ComboBox13.Location = New System.Drawing.Point(123, 175)
        Me.ComboBox13.Name = "ComboBox13"
        Me.ComboBox13.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox13.TabIndex = 12
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 178)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 13)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Zone"
        '
        'ComboBox14
        '
        Me.ComboBox14.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox14.FormattingEnabled = True
        Me.ComboBox14.Location = New System.Drawing.Point(123, 148)
        Me.ComboBox14.Name = "ComboBox14"
        Me.ComboBox14.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox14.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 151)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 13)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "Region"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 367)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(91, 13)
        Me.Label10.TabIndex = 25
        Me.Label10.Text = "Report to Director"
        '
        'ComboBox15
        '
        Me.ComboBox15.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox15.FormattingEnabled = True
        Me.ComboBox15.Location = New System.Drawing.Point(123, 364)
        Me.ComboBox15.Name = "ComboBox15"
        Me.ComboBox15.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox15.TabIndex = 26
        '
        'ComboBox16
        '
        Me.ComboBox16.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox16.FormattingEnabled = True
        Me.ComboBox16.Location = New System.Drawing.Point(123, 310)
        Me.ComboBox16.Name = "ComboBox16"
        Me.ComboBox16.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox16.TabIndex = 22
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 313)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(48, 13)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Saleman"
        '
        'ComboBox17
        '
        Me.ComboBox17.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox17.FormattingEnabled = True
        Me.ComboBox17.Location = New System.Drawing.Point(123, 337)
        Me.ComboBox17.Name = "ComboBox17"
        Me.ComboBox17.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox17.TabIndex = 24
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 337)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(96, 13)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Report to Manager"
        '
        'TextBox16
        '
        Me.TextBox16.Location = New System.Drawing.Point(557, 258)
        Me.TextBox16.Name = "TextBox16"
        Me.TextBox16.Size = New System.Drawing.Size(92, 26)
        Me.TextBox16.TabIndex = 50
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(522, 261)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(30, 13)
        Me.Label13.TabIndex = 49
        Me.Label13.Text = "CNG"
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(12, 286)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(60, 13)
        Me.Label64.TabIndex = 19
        Me.Label64.Text = "Route Plan"
        '
        'ComboBox18
        '
        Me.ComboBox18.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox18.FormattingEnabled = True
        Me.ComboBox18.Location = New System.Drawing.Point(123, 283)
        Me.ComboBox18.Name = "ComboBox18"
        Me.ComboBox18.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox18.TabIndex = 20
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(12, 71)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(79, 13)
        Me.Label65.TabIndex = 3
        Me.Label65.Text = "Customer Code"
        '
        'TextBox17
        '
        Me.TextBox17.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox17.Location = New System.Drawing.Point(123, 68)
        Me.TextBox17.Name = "TextBox17"
        Me.TextBox17.Size = New System.Drawing.Size(237, 26)
        Me.TextBox17.TabIndex = 4
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(373, 149)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(50, 13)
        Me.Label66.TabIndex = 41
        Me.Label66.Text = "NTN No."
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(373, 175)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(74, 13)
        Me.Label67.TabIndex = 43
        Me.Label67.Text = "Sales Tax No."
        '
        'TextBox18
        '
        Me.TextBox18.Location = New System.Drawing.Point(450, 146)
        Me.TextBox18.Name = "TextBox18"
        Me.TextBox18.Size = New System.Drawing.Size(199, 26)
        Me.TextBox18.TabIndex = 42
        '
        'TextBox19
        '
        Me.TextBox19.Location = New System.Drawing.Point(449, 172)
        Me.TextBox19.Name = "TextBox19"
        Me.TextBox19.Size = New System.Drawing.Size(199, 26)
        Me.TextBox19.TabIndex = 44
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(12, 97)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(78, 13)
        Me.Label68.TabIndex = 5
        Me.Label68.Text = "Customer Type"
        '
        'ComboBox19
        '
        Me.ComboBox19.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox19.FormattingEnabled = True
        Me.ComboBox19.Location = New System.Drawing.Point(123, 94)
        Me.ComboBox19.Name = "ComboBox19"
        Me.ComboBox19.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox19.TabIndex = 6
        '
        'TextBox20
        '
        Me.TextBox20.Location = New System.Drawing.Point(123, 477)
        Me.TextBox20.Name = "TextBox20"
        Me.TextBox20.Size = New System.Drawing.Size(237, 26)
        Me.TextBox20.TabIndex = 32
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(12, 480)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(82, 13)
        Me.Label69.TabIndex = 31
        Me.Label69.Text = "Other Expenses"
        '
        'TextBox21
        '
        Me.TextBox21.Location = New System.Drawing.Point(450, 258)
        Me.TextBox21.Name = "TextBox21"
        Me.TextBox21.Size = New System.Drawing.Size(66, 26)
        Me.TextBox21.TabIndex = 48
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(373, 261)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(30, 13)
        Me.Label70.TabIndex = 47
        Me.Label70.Text = "Fuel "
        '
        'TextBox22
        '
        Me.TextBox22.Location = New System.Drawing.Point(122, 451)
        Me.TextBox22.Name = "TextBox22"
        Me.TextBox22.Size = New System.Drawing.Size(237, 26)
        Me.TextBox22.TabIndex = 30
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(12, 454)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(78, 13)
        Me.Label71.TabIndex = 29
        Me.Label71.Text = "Discount %age"
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Location = New System.Drawing.Point(12, 45)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(82, 13)
        Me.Label72.TabIndex = 1
        Me.Label72.Text = "Customer Name"
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.ForeColor = System.Drawing.Color.Navy
        Me.Label73.Location = New System.Drawing.Point(9, 12)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(261, 23)
        Me.Label73.TabIndex = 0
        Me.Label73.Text = "Customers Information"
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Location = New System.Drawing.Point(373, 315)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(52, 13)
        Me.Label74.TabIndex = 54
        Me.Label74.Text = "End Date"
        '
        'TextBox23
        '
        Me.TextBox23.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox23.Location = New System.Drawing.Point(123, 42)
        Me.TextBox23.Name = "TextBox23"
        Me.TextBox23.Size = New System.Drawing.Size(237, 26)
        Me.TextBox23.TabIndex = 2
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Checked = False
        Me.DateTimePicker2.Cursor = System.Windows.Forms.Cursors.Default
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(450, 312)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.ShowCheckBox = True
        Me.DateTimePicker2.Size = New System.Drawing.Size(102, 26)
        Me.DateTimePicker2.TabIndex = 55
        '
        'ComboBox20
        '
        Me.ComboBox20.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox20.FormattingEnabled = True
        Me.ComboBox20.Location = New System.Drawing.Point(123, 121)
        Me.ComboBox20.Name = "ComboBox20"
        Me.ComboBox20.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox20.TabIndex = 8
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.Location = New System.Drawing.Point(12, 124)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(80, 13)
        Me.Label75.TabIndex = 7
        Me.Label75.Text = "Province Name"
        '
        'TextBox24
        '
        Me.TextBox24.Location = New System.Drawing.Point(450, 42)
        Me.TextBox24.Name = "TextBox24"
        Me.TextBox24.Size = New System.Drawing.Size(199, 26)
        Me.TextBox24.TabIndex = 34
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Checked = True
        Me.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox2.Location = New System.Drawing.Point(496, 286)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(56, 17)
        Me.CheckBox2.TabIndex = 53
        Me.CheckBox2.Text = "Active"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.Location = New System.Drawing.Point(373, 45)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(38, 13)
        Me.Label76.TabIndex = 33
        Me.Label76.Text = "Phone"
        '
        'Label77
        '
        Me.Label77.AutoSize = True
        Me.Label77.Location = New System.Drawing.Point(373, 287)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(55, 13)
        Me.Label77.TabIndex = 51
        Me.Label77.Text = "Sort Order"
        '
        'TextBox25
        '
        Me.TextBox25.Location = New System.Drawing.Point(450, 68)
        Me.TextBox25.Name = "TextBox25"
        Me.TextBox25.Size = New System.Drawing.Size(199, 26)
        Me.TextBox25.TabIndex = 36
        '
        'TextBox26
        '
        Me.TextBox26.Location = New System.Drawing.Point(451, 284)
        Me.TextBox26.MaxLength = 50
        Me.TextBox26.Name = "TextBox26"
        Me.TextBox26.Size = New System.Drawing.Size(42, 26)
        Me.TextBox26.TabIndex = 52
        Me.TextBox26.Text = "1"
        '
        'Label78
        '
        Me.Label78.AutoSize = True
        Me.Label78.Location = New System.Drawing.Point(373, 71)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(38, 13)
        Me.Label78.TabIndex = 35
        Me.Label78.Text = "Mobile"
        '
        'Label79
        '
        Me.Label79.AutoSize = True
        Me.Label79.Location = New System.Drawing.Point(373, 201)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(59, 13)
        Me.Label79.TabIndex = 45
        Me.Label79.Text = "Comments:"
        '
        'TextBox27
        '
        Me.TextBox27.Location = New System.Drawing.Point(123, 391)
        Me.TextBox27.Multiline = True
        Me.TextBox27.Name = "TextBox27"
        Me.TextBox27.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox27.Size = New System.Drawing.Size(237, 54)
        Me.TextBox27.TabIndex = 28
        '
        'TextBox28
        '
        Me.TextBox28.Location = New System.Drawing.Point(450, 198)
        Me.TextBox28.Multiline = True
        Me.TextBox28.Name = "TextBox28"
        Me.TextBox28.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox28.Size = New System.Drawing.Size(199, 54)
        Me.TextBox28.TabIndex = 46
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.Location = New System.Drawing.Point(12, 394)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(45, 13)
        Me.Label80.TabIndex = 27
        Me.Label80.Text = "Address"
        '
        'Label81
        '
        Me.Label81.AutoSize = True
        Me.Label81.Location = New System.Drawing.Point(373, 123)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(58, 13)
        Me.Label81.TabIndex = 39
        Me.Label81.Text = "Credit Limit"
        '
        'ComboBox21
        '
        Me.ComboBox21.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox21.FormattingEnabled = True
        Me.ComboBox21.Location = New System.Drawing.Point(123, 229)
        Me.ComboBox21.Name = "ComboBox21"
        Me.ComboBox21.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox21.TabIndex = 16
        '
        'Label82
        '
        Me.Label82.AutoSize = True
        Me.Label82.Location = New System.Drawing.Point(373, 97)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(36, 13)
        Me.Label82.TabIndex = 37
        Me.Label82.Text = "E-Mail"
        '
        'Label83
        '
        Me.Label83.AutoSize = True
        Me.Label83.Location = New System.Drawing.Point(12, 232)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(55, 13)
        Me.Label83.TabIndex = 15
        Me.Label83.Text = "City Name"
        '
        'TextBox29
        '
        Me.TextBox29.Location = New System.Drawing.Point(450, 120)
        Me.TextBox29.Name = "TextBox29"
        Me.TextBox29.Size = New System.Drawing.Size(199, 26)
        Me.TextBox29.TabIndex = 40
        '
        'ComboBox22
        '
        Me.ComboBox22.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox22.FormattingEnabled = True
        Me.ComboBox22.Location = New System.Drawing.Point(123, 256)
        Me.ComboBox22.Name = "ComboBox22"
        Me.ComboBox22.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox22.TabIndex = 18
        '
        'TextBox30
        '
        Me.TextBox30.Location = New System.Drawing.Point(450, 94)
        Me.TextBox30.Name = "TextBox30"
        Me.TextBox30.Size = New System.Drawing.Size(199, 26)
        Me.TextBox30.TabIndex = 38
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.Location = New System.Drawing.Point(12, 256)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(45, 13)
        Me.Label84.TabIndex = 17
        Me.Label84.Text = "Territory"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 32)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1266, 691)
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Holiday Setup"
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
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1262, 662)
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.LightYellow
        Me.Label85.ForeColor = System.Drawing.Color.Navy
        Me.Label85.Location = New System.Drawing.Point(274, 138)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(263, 45)
        Me.Label85.TabIndex = 56
        Me.Label85.Text = "Processing please wait ..."
        Me.Label85.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label85.Visible = False
        '
        'ComboBox23
        '
        Me.ComboBox23.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox23.FormattingEnabled = True
        Me.ComboBox23.Location = New System.Drawing.Point(123, 202)
        Me.ComboBox23.Name = "ComboBox23"
        Me.ComboBox23.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox23.TabIndex = 14
        '
        'Label86
        '
        Me.Label86.AutoSize = True
        Me.Label86.Location = New System.Drawing.Point(12, 205)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(25, 13)
        Me.Label86.TabIndex = 13
        Me.Label86.Text = "Belt"
        '
        'ComboBox24
        '
        Me.ComboBox24.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox24.FormattingEnabled = True
        Me.ComboBox24.Location = New System.Drawing.Point(123, 175)
        Me.ComboBox24.Name = "ComboBox24"
        Me.ComboBox24.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox24.TabIndex = 12
        '
        'Label87
        '
        Me.Label87.AutoSize = True
        Me.Label87.Location = New System.Drawing.Point(12, 178)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(32, 13)
        Me.Label87.TabIndex = 11
        Me.Label87.Text = "Zone"
        '
        'ComboBox25
        '
        Me.ComboBox25.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox25.FormattingEnabled = True
        Me.ComboBox25.Location = New System.Drawing.Point(123, 148)
        Me.ComboBox25.Name = "ComboBox25"
        Me.ComboBox25.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox25.TabIndex = 10
        '
        'Label88
        '
        Me.Label88.AutoSize = True
        Me.Label88.Location = New System.Drawing.Point(12, 151)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(41, 13)
        Me.Label88.TabIndex = 9
        Me.Label88.Text = "Region"
        '
        'Label89
        '
        Me.Label89.AutoSize = True
        Me.Label89.Location = New System.Drawing.Point(12, 367)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(91, 13)
        Me.Label89.TabIndex = 25
        Me.Label89.Text = "Report to Director"
        '
        'ComboBox26
        '
        Me.ComboBox26.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox26.FormattingEnabled = True
        Me.ComboBox26.Location = New System.Drawing.Point(123, 364)
        Me.ComboBox26.Name = "ComboBox26"
        Me.ComboBox26.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox26.TabIndex = 26
        '
        'ComboBox27
        '
        Me.ComboBox27.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox27.FormattingEnabled = True
        Me.ComboBox27.Location = New System.Drawing.Point(123, 310)
        Me.ComboBox27.Name = "ComboBox27"
        Me.ComboBox27.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox27.TabIndex = 22
        '
        'Label90
        '
        Me.Label90.AutoSize = True
        Me.Label90.Location = New System.Drawing.Point(12, 313)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(48, 13)
        Me.Label90.TabIndex = 21
        Me.Label90.Text = "Saleman"
        '
        'ComboBox28
        '
        Me.ComboBox28.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox28.FormattingEnabled = True
        Me.ComboBox28.Location = New System.Drawing.Point(123, 337)
        Me.ComboBox28.Name = "ComboBox28"
        Me.ComboBox28.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox28.TabIndex = 24
        '
        'Label91
        '
        Me.Label91.AutoSize = True
        Me.Label91.Location = New System.Drawing.Point(12, 337)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(96, 13)
        Me.Label91.TabIndex = 23
        Me.Label91.Text = "Report to Manager"
        '
        'TextBox31
        '
        Me.TextBox31.Location = New System.Drawing.Point(557, 258)
        Me.TextBox31.Name = "TextBox31"
        Me.TextBox31.Size = New System.Drawing.Size(92, 26)
        Me.TextBox31.TabIndex = 50
        '
        'Label92
        '
        Me.Label92.AutoSize = True
        Me.Label92.Location = New System.Drawing.Point(522, 261)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(30, 13)
        Me.Label92.TabIndex = 49
        Me.Label92.Text = "CNG"
        '
        'Label93
        '
        Me.Label93.AutoSize = True
        Me.Label93.Location = New System.Drawing.Point(12, 286)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(60, 13)
        Me.Label93.TabIndex = 19
        Me.Label93.Text = "Route Plan"
        '
        'ComboBox29
        '
        Me.ComboBox29.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox29.FormattingEnabled = True
        Me.ComboBox29.Location = New System.Drawing.Point(123, 283)
        Me.ComboBox29.Name = "ComboBox29"
        Me.ComboBox29.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox29.TabIndex = 20
        '
        'Label94
        '
        Me.Label94.AutoSize = True
        Me.Label94.Location = New System.Drawing.Point(12, 71)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(79, 13)
        Me.Label94.TabIndex = 3
        Me.Label94.Text = "Customer Code"
        '
        'TextBox32
        '
        Me.TextBox32.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox32.Location = New System.Drawing.Point(123, 68)
        Me.TextBox32.Name = "TextBox32"
        Me.TextBox32.Size = New System.Drawing.Size(237, 26)
        Me.TextBox32.TabIndex = 4
        '
        'Label95
        '
        Me.Label95.AutoSize = True
        Me.Label95.Location = New System.Drawing.Point(373, 149)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(50, 13)
        Me.Label95.TabIndex = 41
        Me.Label95.Text = "NTN No."
        '
        'Label96
        '
        Me.Label96.AutoSize = True
        Me.Label96.Location = New System.Drawing.Point(373, 175)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(74, 13)
        Me.Label96.TabIndex = 43
        Me.Label96.Text = "Sales Tax No."
        '
        'TextBox33
        '
        Me.TextBox33.Location = New System.Drawing.Point(450, 146)
        Me.TextBox33.Name = "TextBox33"
        Me.TextBox33.Size = New System.Drawing.Size(199, 26)
        Me.TextBox33.TabIndex = 42
        '
        'TextBox34
        '
        Me.TextBox34.Location = New System.Drawing.Point(449, 172)
        Me.TextBox34.Name = "TextBox34"
        Me.TextBox34.Size = New System.Drawing.Size(199, 26)
        Me.TextBox34.TabIndex = 44
        '
        'Label97
        '
        Me.Label97.AutoSize = True
        Me.Label97.Location = New System.Drawing.Point(12, 97)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(78, 13)
        Me.Label97.TabIndex = 5
        Me.Label97.Text = "Customer Type"
        '
        'ComboBox30
        '
        Me.ComboBox30.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox30.FormattingEnabled = True
        Me.ComboBox30.Location = New System.Drawing.Point(123, 94)
        Me.ComboBox30.Name = "ComboBox30"
        Me.ComboBox30.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox30.TabIndex = 6
        '
        'TextBox35
        '
        Me.TextBox35.Location = New System.Drawing.Point(123, 477)
        Me.TextBox35.Name = "TextBox35"
        Me.TextBox35.Size = New System.Drawing.Size(237, 26)
        Me.TextBox35.TabIndex = 32
        '
        'Label98
        '
        Me.Label98.AutoSize = True
        Me.Label98.Location = New System.Drawing.Point(12, 480)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(82, 13)
        Me.Label98.TabIndex = 31
        Me.Label98.Text = "Other Expenses"
        '
        'TextBox36
        '
        Me.TextBox36.Location = New System.Drawing.Point(450, 258)
        Me.TextBox36.Name = "TextBox36"
        Me.TextBox36.Size = New System.Drawing.Size(66, 26)
        Me.TextBox36.TabIndex = 48
        '
        'Label99
        '
        Me.Label99.AutoSize = True
        Me.Label99.Location = New System.Drawing.Point(373, 261)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(30, 13)
        Me.Label99.TabIndex = 47
        Me.Label99.Text = "Fuel "
        '
        'TextBox37
        '
        Me.TextBox37.Location = New System.Drawing.Point(122, 451)
        Me.TextBox37.Name = "TextBox37"
        Me.TextBox37.Size = New System.Drawing.Size(237, 26)
        Me.TextBox37.TabIndex = 30
        '
        'Label100
        '
        Me.Label100.AutoSize = True
        Me.Label100.Location = New System.Drawing.Point(12, 454)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(78, 13)
        Me.Label100.TabIndex = 29
        Me.Label100.Text = "Discount %age"
        '
        'Label101
        '
        Me.Label101.AutoSize = True
        Me.Label101.Location = New System.Drawing.Point(12, 45)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(82, 13)
        Me.Label101.TabIndex = 1
        Me.Label101.Text = "Customer Name"
        '
        'Label102
        '
        Me.Label102.AutoSize = True
        Me.Label102.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label102.ForeColor = System.Drawing.Color.Navy
        Me.Label102.Location = New System.Drawing.Point(9, 12)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(261, 23)
        Me.Label102.TabIndex = 0
        Me.Label102.Text = "Customers Information"
        '
        'Label103
        '
        Me.Label103.AutoSize = True
        Me.Label103.Location = New System.Drawing.Point(373, 315)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(52, 13)
        Me.Label103.TabIndex = 54
        Me.Label103.Text = "End Date"
        '
        'TextBox38
        '
        Me.TextBox38.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox38.Location = New System.Drawing.Point(123, 42)
        Me.TextBox38.Name = "TextBox38"
        Me.TextBox38.Size = New System.Drawing.Size(237, 26)
        Me.TextBox38.TabIndex = 2
        '
        'DateTimePicker3
        '
        Me.DateTimePicker3.Checked = False
        Me.DateTimePicker3.Cursor = System.Windows.Forms.Cursors.Default
        Me.DateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker3.Location = New System.Drawing.Point(450, 312)
        Me.DateTimePicker3.Name = "DateTimePicker3"
        Me.DateTimePicker3.ShowCheckBox = True
        Me.DateTimePicker3.Size = New System.Drawing.Size(102, 26)
        Me.DateTimePicker3.TabIndex = 55
        '
        'ComboBox31
        '
        Me.ComboBox31.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox31.FormattingEnabled = True
        Me.ComboBox31.Location = New System.Drawing.Point(123, 121)
        Me.ComboBox31.Name = "ComboBox31"
        Me.ComboBox31.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox31.TabIndex = 8
        '
        'Label104
        '
        Me.Label104.AutoSize = True
        Me.Label104.Location = New System.Drawing.Point(12, 124)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(80, 13)
        Me.Label104.TabIndex = 7
        Me.Label104.Text = "Province Name"
        '
        'TextBox39
        '
        Me.TextBox39.Location = New System.Drawing.Point(450, 42)
        Me.TextBox39.Name = "TextBox39"
        Me.TextBox39.Size = New System.Drawing.Size(199, 26)
        Me.TextBox39.TabIndex = 34
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Checked = True
        Me.CheckBox3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox3.Location = New System.Drawing.Point(496, 286)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(56, 17)
        Me.CheckBox3.TabIndex = 53
        Me.CheckBox3.Text = "Active"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'Label105
        '
        Me.Label105.AutoSize = True
        Me.Label105.Location = New System.Drawing.Point(373, 45)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(38, 13)
        Me.Label105.TabIndex = 33
        Me.Label105.Text = "Phone"
        '
        'Label106
        '
        Me.Label106.AutoSize = True
        Me.Label106.Location = New System.Drawing.Point(373, 287)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(55, 13)
        Me.Label106.TabIndex = 51
        Me.Label106.Text = "Sort Order"
        '
        'TextBox40
        '
        Me.TextBox40.Location = New System.Drawing.Point(450, 68)
        Me.TextBox40.Name = "TextBox40"
        Me.TextBox40.Size = New System.Drawing.Size(199, 26)
        Me.TextBox40.TabIndex = 36
        '
        'TextBox41
        '
        Me.TextBox41.Location = New System.Drawing.Point(451, 284)
        Me.TextBox41.MaxLength = 50
        Me.TextBox41.Name = "TextBox41"
        Me.TextBox41.Size = New System.Drawing.Size(42, 26)
        Me.TextBox41.TabIndex = 52
        Me.TextBox41.Text = "1"
        '
        'Label107
        '
        Me.Label107.AutoSize = True
        Me.Label107.Location = New System.Drawing.Point(373, 71)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(38, 13)
        Me.Label107.TabIndex = 35
        Me.Label107.Text = "Mobile"
        '
        'Label108
        '
        Me.Label108.AutoSize = True
        Me.Label108.Location = New System.Drawing.Point(373, 201)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(59, 13)
        Me.Label108.TabIndex = 45
        Me.Label108.Text = "Comments:"
        '
        'TextBox42
        '
        Me.TextBox42.Location = New System.Drawing.Point(123, 391)
        Me.TextBox42.Multiline = True
        Me.TextBox42.Name = "TextBox42"
        Me.TextBox42.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox42.Size = New System.Drawing.Size(237, 54)
        Me.TextBox42.TabIndex = 28
        '
        'TextBox43
        '
        Me.TextBox43.Location = New System.Drawing.Point(450, 198)
        Me.TextBox43.Multiline = True
        Me.TextBox43.Name = "TextBox43"
        Me.TextBox43.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox43.Size = New System.Drawing.Size(199, 54)
        Me.TextBox43.TabIndex = 46
        '
        'Label109
        '
        Me.Label109.AutoSize = True
        Me.Label109.Location = New System.Drawing.Point(12, 394)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(45, 13)
        Me.Label109.TabIndex = 27
        Me.Label109.Text = "Address"
        '
        'Label110
        '
        Me.Label110.AutoSize = True
        Me.Label110.Location = New System.Drawing.Point(373, 123)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(58, 13)
        Me.Label110.TabIndex = 39
        Me.Label110.Text = "Credit Limit"
        '
        'ComboBox32
        '
        Me.ComboBox32.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox32.FormattingEnabled = True
        Me.ComboBox32.Location = New System.Drawing.Point(123, 229)
        Me.ComboBox32.Name = "ComboBox32"
        Me.ComboBox32.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox32.TabIndex = 16
        '
        'Label111
        '
        Me.Label111.AutoSize = True
        Me.Label111.Location = New System.Drawing.Point(373, 97)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(36, 13)
        Me.Label111.TabIndex = 37
        Me.Label111.Text = "E-Mail"
        '
        'Label112
        '
        Me.Label112.AutoSize = True
        Me.Label112.Location = New System.Drawing.Point(12, 232)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(55, 13)
        Me.Label112.TabIndex = 15
        Me.Label112.Text = "City Name"
        '
        'TextBox44
        '
        Me.TextBox44.Location = New System.Drawing.Point(450, 120)
        Me.TextBox44.Name = "TextBox44"
        Me.TextBox44.Size = New System.Drawing.Size(199, 26)
        Me.TextBox44.TabIndex = 40
        '
        'ComboBox33
        '
        Me.ComboBox33.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox33.FormattingEnabled = True
        Me.ComboBox33.Location = New System.Drawing.Point(123, 256)
        Me.ComboBox33.Name = "ComboBox33"
        Me.ComboBox33.Size = New System.Drawing.Size(237, 28)
        Me.ComboBox33.TabIndex = 18
        '
        'TextBox45
        '
        Me.TextBox45.Location = New System.Drawing.Point(450, 94)
        Me.TextBox45.Name = "TextBox45"
        Me.TextBox45.Size = New System.Drawing.Size(199, 26)
        Me.TextBox45.TabIndex = 38
        '
        'Label113
        '
        Me.Label113.AutoSize = True
        Me.Label113.Location = New System.Drawing.Point(12, 256)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(45, 13)
        Me.Label113.TabIndex = 17
        Me.Label113.Text = "Territory"
        '
        'PnDat
        '
        Me.PnDat.Controls.Add(Me.LstDesig)
        Me.PnDat.Controls.Add(Me.LstEmployee)
        Me.PnDat.Controls.Add(Me.LstDepart)
        Me.PnDat.Location = New System.Drawing.Point(416, 89)
        Me.PnDat.Name = "PnDat"
        Me.PnDat.Size = New System.Drawing.Size(310, 376)
        Me.PnDat.TabIndex = 1
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnEdit, Me.BtnSave, Me.BtnDelete, Me.BtnPrint, Me.ToolStripSeparator2, Me.ToolStripButton6})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1266, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BtnNew
        '
        Me.BtnNew.Image = CType(resources.GetObject("BtnNew.Image"), System.Drawing.Image)
        Me.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(75, 29)
        Me.BtnNew.Text = "&New"
        '
        'BtnEdit
        '
        Me.BtnEdit.Image = CType(resources.GetObject("BtnEdit.Image"), System.Drawing.Image)
        Me.BtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(70, 29)
        Me.BtnEdit.Text = "&Edit"
        '
        'BtnSave
        '
        Me.BtnSave.Image = CType(resources.GetObject("BtnSave.Image"), System.Drawing.Image)
        Me.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(77, 29)
        Me.BtnSave.Text = "&Save"
        '
        'BtnDelete
        '
        Me.BtnDelete.Image = CType(resources.GetObject("BtnDelete.Image"), System.Drawing.Image)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.RightToLeftAutoMirrorImage = True
        Me.BtnDelete.Size = New System.Drawing.Size(90, 29)
        Me.BtnDelete.Text = "&Delete"
        '
        'BtnPrint
        '
        Me.BtnPrint.Image = CType(resources.GetObject("BtnPrint.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(76, 29)
        Me.BtnPrint.Text = "&Print"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 32)
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(28, 29)
        Me.ToolStripButton6.Text = "He&lp"
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1215, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grdAttendanceHistory
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(51, 38)
        Me.CtrlGrdBar1.TabIndex = 2
        '
        'frmHolidySetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(1266, 723)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmHolidySetup"
        Me.Text = "Attendance Setup"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.plnEmp.ResumeLayout(False)
        Me.PnlCriteria.ResumeLayout(False)
        Me.PnlCriteria.PerformLayout()
        Me.PnlDat.ResumeLayout(False)
        Me.PnlDat.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.GrdHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAttendanceHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.PnDat.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbBelt As System.Windows.Forms.ComboBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents cmbZone As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents cmbRegion As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cmbDirector As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSaleman As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmbManager As System.Windows.Forms.ComboBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtCNG As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cmbRootPlan As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Friend WithEvents lblNTNNo As System.Windows.Forms.Label
    Friend WithEvents lblSalesTax As System.Windows.Forms.Label
    Friend WithEvents txtNTNNo As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesTaxNo As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents CmbCustomerTypes As System.Windows.Forms.ComboBox
    Friend WithEvents TxtOtherExpn As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TxtFuel As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtDiscPer As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents uitxtName As System.Windows.Forms.TextBox
    Friend WithEvents dtpExpiryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbState As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents uitxtPhone As System.Windows.Forms.TextBox
    Friend WithEvents uichkActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents uitxtMobile As System.Windows.Forms.TextBox
    Friend WithEvents uitxtSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents uitxtAddress As System.Windows.Forms.TextBox
    Friend WithEvents uitxtcomments As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents cmbCity As System.Windows.Forms.ComboBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents uitxtCrditLimit As System.Windows.Forms.TextBox
    Friend WithEvents cmbTerritory As System.Windows.Forms.ComboBox
    Friend WithEvents uitxtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox5 As System.Windows.Forms.ComboBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents ComboBox6 As System.Windows.Forms.ComboBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents ComboBox7 As System.Windows.Forms.ComboBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents ComboBox8 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ComboBox9 As System.Windows.Forms.ComboBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents TextBox9 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents TextBox10 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox11 As System.Windows.Forms.TextBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents TextBox12 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox13 As System.Windows.Forms.TextBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents ComboBox10 As System.Windows.Forms.ComboBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents TextBox14 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox11 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox15 As System.Windows.Forms.TextBox
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents ComboBox12 As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboBox13 As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ComboBox14 As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ComboBox15 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox16 As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ComboBox17 As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TextBox16 As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents ComboBox18 As System.Windows.Forms.ComboBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents TextBox17 As System.Windows.Forms.TextBox
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents TextBox18 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox19 As System.Windows.Forms.TextBox
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents ComboBox19 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox20 As System.Windows.Forms.TextBox
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents TextBox21 As System.Windows.Forms.TextBox
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents TextBox22 As System.Windows.Forms.TextBox
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents TextBox23 As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ComboBox20 As System.Windows.Forms.ComboBox
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents TextBox24 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents TextBox25 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox26 As System.Windows.Forms.TextBox
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents TextBox27 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox28 As System.Windows.Forms.TextBox
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents ComboBox21 As System.Windows.Forms.ComboBox
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents TextBox29 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox22 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox30 As System.Windows.Forms.TextBox
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents PnlDat As System.Windows.Forms.Panel
    Friend WithEvents LstDesig As System.Windows.Forms.ListBox
    Friend WithEvents LstEmployee As System.Windows.Forms.ListBox
    Friend WithEvents PnlCriteria As System.Windows.Forms.Panel
    Friend WithEvents LblApplyTo As System.Windows.Forms.Label
    Friend WithEvents RdbEmp As System.Windows.Forms.RadioButton
    Friend WithEvents RdbDesig As System.Windows.Forms.RadioButton
    Friend WithEvents RdbDept As System.Windows.Forms.RadioButton
    Friend WithEvents RdbAll As System.Windows.Forms.RadioButton
    Friend WithEvents LblRemarks As System.Windows.Forms.Label
    Friend WithEvents TxtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents LblTotDays As System.Windows.Forms.Label
    Friend WithEvents TxtTotDays As System.Windows.Forms.TextBox
    Friend WithEvents LblEndDate As System.Windows.Forms.Label
    Friend WithEvents DtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents LblStartDate As System.Windows.Forms.Label
    Friend WithEvents DtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents LblStatus As System.Windows.Forms.Label
    Friend WithEvents CmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Friend WithEvents ComboBox23 As System.Windows.Forms.ComboBox
    Friend WithEvents Label86 As System.Windows.Forms.Label
    Friend WithEvents ComboBox24 As System.Windows.Forms.ComboBox
    Friend WithEvents Label87 As System.Windows.Forms.Label
    Friend WithEvents ComboBox25 As System.Windows.Forms.ComboBox
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents Label89 As System.Windows.Forms.Label
    Friend WithEvents ComboBox26 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox27 As System.Windows.Forms.ComboBox
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents ComboBox28 As System.Windows.Forms.ComboBox
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents TextBox31 As System.Windows.Forms.TextBox
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents ComboBox29 As System.Windows.Forms.ComboBox
    Friend WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents TextBox32 As System.Windows.Forms.TextBox
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents TextBox33 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox34 As System.Windows.Forms.TextBox
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents ComboBox30 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox35 As System.Windows.Forms.TextBox
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents TextBox36 As System.Windows.Forms.TextBox
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Friend WithEvents TextBox37 As System.Windows.Forms.TextBox
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents TextBox38 As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker3 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ComboBox31 As System.Windows.Forms.ComboBox
    Friend WithEvents Label104 As System.Windows.Forms.Label
    Friend WithEvents TextBox39 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents Label105 As System.Windows.Forms.Label
    Friend WithEvents Label106 As System.Windows.Forms.Label
    Friend WithEvents TextBox40 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox41 As System.Windows.Forms.TextBox
    Friend WithEvents Label107 As System.Windows.Forms.Label
    Friend WithEvents Label108 As System.Windows.Forms.Label
    Friend WithEvents TextBox42 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox43 As System.Windows.Forms.TextBox
    Friend WithEvents Label109 As System.Windows.Forms.Label
    Friend WithEvents Label110 As System.Windows.Forms.Label
    Friend WithEvents ComboBox32 As System.Windows.Forms.ComboBox
    Friend WithEvents Label111 As System.Windows.Forms.Label
    Friend WithEvents Label112 As System.Windows.Forms.Label
    Friend WithEvents TextBox44 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox33 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox45 As System.Windows.Forms.TextBox
    Friend WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents Label120 As System.Windows.Forms.Label
    Friend WithEvents LstDepart As System.Windows.Forms.ListBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents PnDat As System.Windows.Forms.Panel
    Friend WithEvents GrdHistory As Janus.Windows.GridEX.GridEX
    Friend WithEvents LstDepartment As System.Windows.Forms.ListBox
    Friend WithEvents LstDesignation As System.Windows.Forms.ListBox
    'Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    'Friend WithEvents BtnEdit As System.Windows.Forms.ToolStripButton
    'Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    'Friend WithEvents BtnDelete As System.Windows.Forms.ToolStripButton
    'Friend WithEvents BtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents grdAttendanceHistory As Janus.Windows.GridEX.GridEX
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents plnEmp As System.Windows.Forms.Panel
    Friend WithEvents btnRemoveEmp As System.Windows.Forms.Button
    Friend WithEvents btnAddedEmp As System.Windows.Forms.Button
    Friend WithEvents lstAddedEmployees As SimpleAccounts.uiListControl
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents LstEmployees As SimpleAccounts.uiListControl
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents RdbCostCenter As System.Windows.Forms.RadioButton
    Friend WithEvents LstCostCenter As System.Windows.Forms.ListBox
  
End Class
