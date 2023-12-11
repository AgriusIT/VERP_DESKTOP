<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProjectPortFolio
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProjectPortFolio))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnLoadAll = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnProjectVisit = New System.Windows.Forms.ToolStripButton()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.MainTab = New System.Windows.Forms.TabControl()
        Me.DataTab = New System.Windows.Forms.TabPage()
        Me.DetailTab = New System.Windows.Forms.TabControl()
        Me.ProjectTab = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbTechSupervisor = New System.Windows.Forms.ComboBox()
        Me.cmbSiteSupervisor = New System.Windows.Forms.ComboBox()
        Me.cmbResPerson = New System.Windows.Forms.ComboBox()
        Me.cmbManager = New System.Windows.Forms.ComboBox()
        Me.cmbASM = New System.Windows.Forms.ComboBox()
        Me.cmbGManager = New System.Windows.Forms.ComboBox()
        Me.cmbComDirector = New System.Windows.Forms.ComboBox()
        Me.txtAllQuotationValue = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtProjectEstValue = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbProjSize = New System.Windows.Forms.ComboBox()
        Me.cmbProjType = New System.Windows.Forms.ComboBox()
        Me.lbltxtProjSize = New System.Windows.Forms.Label()
        Me.lbltxtProjType = New System.Windows.Forms.Label()
        Me.lblComSS = New System.Windows.Forms.Label()
        Me.lblComTS = New System.Windows.Forms.Label()
        Me.lblComSE = New System.Windows.Forms.Label()
        Me.lblComManager = New System.Windows.Forms.Label()
        Me.lblComDirector = New System.Windows.Forms.Label()
        Me.lblComGM = New System.Windows.Forms.Label()
        Me.lblComASM = New System.Windows.Forms.Label()
        Me.gboxGuardInformation = New System.Windows.Forms.GroupBox()
        Me.txtGaurdEmail = New System.Windows.Forms.TextBox()
        Me.txtGaurdMbNo = New System.Windows.Forms.TextBox()
        Me.txtGaurdName = New System.Windows.Forms.TextBox()
        Me.lblGaurdEmail = New System.Windows.Forms.Label()
        Me.lblGaurdMbNo = New System.Windows.Forms.Label()
        Me.lblGaurdName = New System.Windows.Forms.Label()
        Me.gboxRespresentativeInformation = New System.Windows.Forms.GroupBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.txtRepEmail = New System.Windows.Forms.TextBox()
        Me.txtRepMbNo = New System.Windows.Forms.TextBox()
        Me.txtRepName = New System.Windows.Forms.TextBox()
        Me.lblRepEmail = New System.Windows.Forms.Label()
        Me.lblRepMbNo = New System.Windows.Forms.Label()
        Me.lblRepName = New System.Windows.Forms.Label()
        Me.gboxCustomerInformation = New System.Windows.Forms.GroupBox()
        Me.txtCustEmail = New System.Windows.Forms.TextBox()
        Me.txtCustMob = New System.Windows.Forms.TextBox()
        Me.txtCustOffAdd = New System.Windows.Forms.TextBox()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.lblCustEmail = New System.Windows.Forms.Label()
        Me.lblCustMob = New System.Windows.Forms.Label()
        Me.lblCustOffAdd = New System.Windows.Forms.Label()
        Me.lblCustName = New System.Windows.Forms.Label()
        Me.gboxProjectInformation = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpProjDate = New System.Windows.Forms.DateTimePicker()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.txtArea = New System.Windows.Forms.TextBox()
        Me.txtPhase = New System.Windows.Forms.TextBox()
        Me.txtBlockNo = New System.Windows.Forms.TextBox()
        Me.txtSiteAddress = New System.Windows.Forms.TextBox()
        Me.txtProjectName = New System.Windows.Forms.TextBox()
        Me.txtProjectCode = New System.Windows.Forms.TextBox()
        Me.lblCity = New System.Windows.Forms.Label()
        Me.lblArea = New System.Windows.Forms.Label()
        Me.lblPhase = New System.Windows.Forms.Label()
        Me.lblBlockNo = New System.Windows.Forms.Label()
        Me.lblSiteAddress = New System.Windows.Forms.Label()
        Me.lblProjectName = New System.Windows.Forms.Label()
        Me.lblProjectCode = New System.Windows.Forms.Label()
        Me.OtherTab = New System.Windows.Forms.TabPage()
        Me.gboxConsultantInformation = New System.Windows.Forms.GroupBox()
        Me.txtConConEmail = New System.Windows.Forms.TextBox()
        Me.txtConConMbNo = New System.Windows.Forms.TextBox()
        Me.txtConConName = New System.Windows.Forms.TextBox()
        Me.lblConConEmail = New System.Windows.Forms.Label()
        Me.lblConConMbNo = New System.Windows.Forms.Label()
        Me.lblConConName = New System.Windows.Forms.Label()
        Me.txtConMAINEmail = New System.Windows.Forms.TextBox()
        Me.txtConMainMbNo = New System.Windows.Forms.TextBox()
        Me.txtConMainName = New System.Windows.Forms.TextBox()
        Me.lblConMAINEmail = New System.Windows.Forms.Label()
        Me.lblConMainMbNo = New System.Windows.Forms.Label()
        Me.lblConMainName = New System.Windows.Forms.Label()
        Me.txtConEmail = New System.Windows.Forms.TextBox()
        Me.txtConMBNo = New System.Windows.Forms.TextBox()
        Me.txtConOwner = New System.Windows.Forms.TextBox()
        Me.lblConEmail = New System.Windows.Forms.Label()
        Me.lblConMBNo = New System.Windows.Forms.Label()
        Me.lblConOwner = New System.Windows.Forms.Label()
        Me.txtConPhNo = New System.Windows.Forms.TextBox()
        Me.txtConAdd = New System.Windows.Forms.TextBox()
        Me.txtConName = New System.Windows.Forms.TextBox()
        Me.lblConPhNo = New System.Windows.Forms.Label()
        Me.lblConAdd = New System.Windows.Forms.Label()
        Me.lblConName = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtArcPMEmail = New System.Windows.Forms.TextBox()
        Me.txtArcPMMbNo = New System.Windows.Forms.TextBox()
        Me.txtArcPManager = New System.Windows.Forms.TextBox()
        Me.lblArcPMEmail = New System.Windows.Forms.Label()
        Me.lblArcPMMbNo = New System.Windows.Forms.Label()
        Me.lblArcPManager = New System.Windows.Forms.Label()
        Me.txtArcGMEmail = New System.Windows.Forms.TextBox()
        Me.txtArcGMMbNo = New System.Windows.Forms.TextBox()
        Me.txtArcGManager = New System.Windows.Forms.TextBox()
        Me.lblArcGMEmail = New System.Windows.Forms.Label()
        Me.lblArcGMMbNo = New System.Windows.Forms.Label()
        Me.lblArcGManager = New System.Windows.Forms.Label()
        Me.txtArchEmail = New System.Windows.Forms.TextBox()
        Me.txtArcMBNo = New System.Windows.Forms.TextBox()
        Me.txtArcOwner = New System.Windows.Forms.TextBox()
        Me.lblArchEmail = New System.Windows.Forms.Label()
        Me.lblArcMBNo = New System.Windows.Forms.Label()
        Me.lblArcOwner = New System.Windows.Forms.Label()
        Me.txtArcPhNo = New System.Windows.Forms.TextBox()
        Me.txtArcAdd = New System.Windows.Forms.TextBox()
        Me.txtArcName = New System.Windows.Forms.TextBox()
        Me.lblArcPhNo = New System.Windows.Forms.Label()
        Me.lblArcAdd = New System.Windows.Forms.Label()
        Me.lblArcName = New System.Windows.Forms.Label()
        Me.gboxBuilderInformation = New System.Windows.Forms.GroupBox()
        Me.lblBulPMEmail = New System.Windows.Forms.Label()
        Me.txtBulPMEmail = New System.Windows.Forms.TextBox()
        Me.txtBulPMMbNo = New System.Windows.Forms.TextBox()
        Me.txtBulPManager = New System.Windows.Forms.TextBox()
        Me.lblBulPMMbNo = New System.Windows.Forms.Label()
        Me.lblBulPManager = New System.Windows.Forms.Label()
        Me.txtBulGMEmail = New System.Windows.Forms.TextBox()
        Me.txtBulGMMbNo = New System.Windows.Forms.TextBox()
        Me.txtBulGManager = New System.Windows.Forms.TextBox()
        Me.lblBulGMEmail = New System.Windows.Forms.Label()
        Me.lblBulGMMbNo = New System.Windows.Forms.Label()
        Me.lblBulGManager = New System.Windows.Forms.Label()
        Me.txtBulEmail = New System.Windows.Forms.TextBox()
        Me.txtBulMbNo = New System.Windows.Forms.TextBox()
        Me.txtBulOwner = New System.Windows.Forms.TextBox()
        Me.lblBulEmail = New System.Windows.Forms.Label()
        Me.lblBulMbNo = New System.Windows.Forms.Label()
        Me.lblBulOwner = New System.Windows.Forms.Label()
        Me.txtBulPhNo = New System.Windows.Forms.TextBox()
        Me.txtBulAdd = New System.Windows.Forms.TextBox()
        Me.txtBulName = New System.Windows.Forms.TextBox()
        Me.lblBulPhNo = New System.Windows.Forms.Label()
        Me.lblBulAdd = New System.Windows.Forms.Label()
        Me.lblBulName = New System.Windows.Forms.Label()
        Me.HistoryTab = New System.Windows.Forms.TabPage()
        Me.grdHistory = New Janus.Windows.GridEX.GridEX()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.cmbGM = New System.Windows.Forms.ComboBox()
        Me.ToolStrip1.SuspendLayout()
        Me.MainTab.SuspendLayout()
        Me.DataTab.SuspendLayout()
        Me.DetailTab.SuspendLayout()
        Me.ProjectTab.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gboxGuardInformation.SuspendLayout()
        Me.gboxRespresentativeInformation.SuspendLayout()
        Me.gboxCustomerInformation.SuspendLayout()
        Me.gboxProjectInformation.SuspendLayout()
        Me.OtherTab.SuspendLayout()
        Me.gboxConsultantInformation.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.gboxBuilderInformation.SuspendLayout()
        Me.HistoryTab.SuspendLayout()
        CType(Me.grdHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnPrint, Me.toolStripSeparator, Me.btnDelete, Me.toolStripSeparator1, Me.btnLoadAll, Me.ToolStripSeparator2, Me.btnProjectVisit, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1491, 32)
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
        Me.btnPrint.Enabled = False
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
        'btnLoadAll
        '
        Me.btnLoadAll.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.btnLoadAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLoadAll.Name = "btnLoadAll"
        Me.btnLoadAll.Size = New System.Drawing.Size(104, 29)
        Me.btnLoadAll.Text = "Load All"
        Me.btnLoadAll.Visible = False
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 32)
        '
        'btnProjectVisit
        '
        Me.btnProjectVisit.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.btnProjectVisit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnProjectVisit.Name = "btnProjectVisit"
        Me.btnProjectVisit.Size = New System.Drawing.Size(132, 29)
        Me.btnProjectVisit.Text = "Project Visit"
        '
        'btnHelp
        '
        Me.btnHelp.Enabled = False
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(77, 29)
        Me.btnHelp.Text = "He&lp"
        Me.btnHelp.Visible = False
        '
        'MainTab
        '
        Me.MainTab.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.MainTab.Controls.Add(Me.DataTab)
        Me.MainTab.Controls.Add(Me.HistoryTab)
        Me.MainTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTab.Location = New System.Drawing.Point(0, 32)
        Me.MainTab.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MainTab.Multiline = True
        Me.MainTab.Name = "MainTab"
        Me.MainTab.SelectedIndex = 0
        Me.MainTab.Size = New System.Drawing.Size(1491, 928)
        Me.MainTab.TabIndex = 1
        '
        'DataTab
        '
        Me.DataTab.Controls.Add(Me.DetailTab)
        Me.DataTab.Location = New System.Drawing.Point(4, 4)
        Me.DataTab.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataTab.Name = "DataTab"
        Me.DataTab.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataTab.Size = New System.Drawing.Size(1483, 895)
        Me.DataTab.TabIndex = 0
        Me.DataTab.Text = "Data"
        Me.DataTab.UseVisualStyleBackColor = True
        '
        'DetailTab
        '
        Me.DetailTab.Controls.Add(Me.ProjectTab)
        Me.DetailTab.Controls.Add(Me.OtherTab)
        Me.DetailTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DetailTab.Location = New System.Drawing.Point(4, 5)
        Me.DetailTab.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DetailTab.Name = "DetailTab"
        Me.DetailTab.SelectedIndex = 0
        Me.DetailTab.Size = New System.Drawing.Size(1475, 885)
        Me.DetailTab.TabIndex = 0
        '
        'ProjectTab
        '
        Me.ProjectTab.BackColor = System.Drawing.Color.Transparent
        Me.ProjectTab.Controls.Add(Me.GroupBox1)
        Me.ProjectTab.Controls.Add(Me.gboxGuardInformation)
        Me.ProjectTab.Controls.Add(Me.gboxRespresentativeInformation)
        Me.ProjectTab.Controls.Add(Me.gboxCustomerInformation)
        Me.ProjectTab.Controls.Add(Me.gboxProjectInformation)
        Me.ProjectTab.Location = New System.Drawing.Point(4, 29)
        Me.ProjectTab.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ProjectTab.Name = "ProjectTab"
        Me.ProjectTab.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ProjectTab.Size = New System.Drawing.Size(1467, 852)
        Me.ProjectTab.TabIndex = 0
        Me.ProjectTab.Text = "Project Info"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbTechSupervisor)
        Me.GroupBox1.Controls.Add(Me.cmbSiteSupervisor)
        Me.GroupBox1.Controls.Add(Me.cmbResPerson)
        Me.GroupBox1.Controls.Add(Me.cmbManager)
        Me.GroupBox1.Controls.Add(Me.cmbASM)
        Me.GroupBox1.Controls.Add(Me.cmbGManager)
        Me.GroupBox1.Controls.Add(Me.cmbComDirector)
        Me.GroupBox1.Controls.Add(Me.txtAllQuotationValue)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtProjectEstValue)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbProjSize)
        Me.GroupBox1.Controls.Add(Me.cmbProjType)
        Me.GroupBox1.Controls.Add(Me.lbltxtProjSize)
        Me.GroupBox1.Controls.Add(Me.lbltxtProjType)
        Me.GroupBox1.Controls.Add(Me.lblComSS)
        Me.GroupBox1.Controls.Add(Me.lblComTS)
        Me.GroupBox1.Controls.Add(Me.lblComSE)
        Me.GroupBox1.Controls.Add(Me.lblComManager)
        Me.GroupBox1.Controls.Add(Me.lblComDirector)
        Me.GroupBox1.Controls.Add(Me.lblComGM)
        Me.GroupBox1.Controls.Add(Me.lblComASM)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 403)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(1329, 212)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "5. Company Representatives"
        '
        'cmbTechSupervisor
        '
        Me.cmbTechSupervisor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbTechSupervisor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbTechSupervisor.FormattingEnabled = True
        Me.cmbTechSupervisor.Location = New System.Drawing.Point(675, 117)
        Me.cmbTechSupervisor.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbTechSupervisor.Name = "cmbTechSupervisor"
        Me.cmbTechSupervisor.Size = New System.Drawing.Size(216, 28)
        Me.cmbTechSupervisor.TabIndex = 15
        '
        'cmbSiteSupervisor
        '
        Me.cmbSiteSupervisor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbSiteSupervisor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSiteSupervisor.FormattingEnabled = True
        Me.cmbSiteSupervisor.Location = New System.Drawing.Point(1102, 40)
        Me.cmbSiteSupervisor.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbSiteSupervisor.Name = "cmbSiteSupervisor"
        Me.cmbSiteSupervisor.Size = New System.Drawing.Size(202, 28)
        Me.cmbSiteSupervisor.TabIndex = 5
        '
        'cmbResPerson
        '
        Me.cmbResPerson.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbResPerson.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbResPerson.FormattingEnabled = True
        Me.cmbResPerson.Location = New System.Drawing.Point(675, 78)
        Me.cmbResPerson.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbResPerson.Name = "cmbResPerson"
        Me.cmbResPerson.Size = New System.Drawing.Size(216, 28)
        Me.cmbResPerson.TabIndex = 9
        '
        'cmbManager
        '
        Me.cmbManager.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbManager.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbManager.FormattingEnabled = True
        Me.cmbManager.Location = New System.Drawing.Point(675, 38)
        Me.cmbManager.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbManager.Name = "cmbManager"
        Me.cmbManager.Size = New System.Drawing.Size(216, 28)
        Me.cmbManager.TabIndex = 3
        '
        'cmbASM
        '
        Me.cmbASM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbASM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbASM.FormattingEnabled = True
        Me.cmbASM.Location = New System.Drawing.Point(192, 117)
        Me.cmbASM.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbASM.Name = "cmbASM"
        Me.cmbASM.Size = New System.Drawing.Size(272, 28)
        Me.cmbASM.TabIndex = 13
        '
        'cmbGManager
        '
        Me.cmbGManager.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbGManager.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbGManager.FormattingEnabled = True
        Me.cmbGManager.Location = New System.Drawing.Point(192, 78)
        Me.cmbGManager.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbGManager.Name = "cmbGManager"
        Me.cmbGManager.Size = New System.Drawing.Size(272, 28)
        Me.cmbGManager.TabIndex = 7
        '
        'cmbComDirector
        '
        Me.cmbComDirector.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbComDirector.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbComDirector.FormattingEnabled = True
        Me.cmbComDirector.Location = New System.Drawing.Point(192, 40)
        Me.cmbComDirector.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbComDirector.Name = "cmbComDirector"
        Me.cmbComDirector.Size = New System.Drawing.Size(272, 28)
        Me.cmbComDirector.TabIndex = 1
        '
        'txtAllQuotationValue
        '
        Me.txtAllQuotationValue.Location = New System.Drawing.Point(675, 155)
        Me.txtAllQuotationValue.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAllQuotationValue.Name = "txtAllQuotationValue"
        Me.txtAllQuotationValue.ReadOnly = True
        Me.txtAllQuotationValue.Size = New System.Drawing.Size(202, 26)
        Me.txtAllQuotationValue.TabIndex = 21
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(506, 162)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(145, 20)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "All Quotation Value"
        '
        'txtProjectEstValue
        '
        Me.txtProjectEstValue.Location = New System.Drawing.Point(192, 155)
        Me.txtProjectEstValue.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtProjectEstValue.Name = "txtProjectEstValue"
        Me.txtProjectEstValue.Size = New System.Drawing.Size(272, 26)
        Me.txtProjectEstValue.TabIndex = 19
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 162)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(135, 20)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Project Est. Value"
        '
        'cmbProjSize
        '
        Me.cmbProjSize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbProjSize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProjSize.FormattingEnabled = True
        Me.cmbProjSize.Location = New System.Drawing.Point(1102, 120)
        Me.cmbProjSize.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbProjSize.Name = "cmbProjSize"
        Me.cmbProjSize.Size = New System.Drawing.Size(202, 28)
        Me.cmbProjSize.TabIndex = 17
        '
        'cmbProjType
        '
        Me.cmbProjType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbProjType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProjType.FormattingEnabled = True
        Me.cmbProjType.Location = New System.Drawing.Point(1102, 78)
        Me.cmbProjType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbProjType.Name = "cmbProjType"
        Me.cmbProjType.Size = New System.Drawing.Size(202, 28)
        Me.cmbProjType.TabIndex = 11
        '
        'lbltxtProjSize
        '
        Me.lbltxtProjSize.AutoSize = True
        Me.lbltxtProjSize.Location = New System.Drawing.Point(933, 126)
        Me.lbltxtProjSize.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbltxtProjSize.Name = "lbltxtProjSize"
        Me.lbltxtProjSize.Size = New System.Drawing.Size(101, 20)
        Me.lbltxtProjSize.TabIndex = 16
        Me.lbltxtProjSize.Text = "Project Size :"
        '
        'lbltxtProjType
        '
        Me.lbltxtProjType.AutoSize = True
        Me.lbltxtProjType.Location = New System.Drawing.Point(933, 85)
        Me.lbltxtProjType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbltxtProjType.Name = "lbltxtProjType"
        Me.lbltxtProjType.Size = New System.Drawing.Size(104, 20)
        Me.lbltxtProjType.TabIndex = 10
        Me.lbltxtProjType.Text = "Project Type :"
        '
        'lblComSS
        '
        Me.lblComSS.AutoSize = True
        Me.lblComSS.Location = New System.Drawing.Point(933, 45)
        Me.lblComSS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblComSS.Name = "lblComSS"
        Me.lblComSS.Size = New System.Drawing.Size(120, 20)
        Me.lblComSS.TabIndex = 4
        Me.lblComSS.Text = "Site Supervisor:"
        '
        'lblComTS
        '
        Me.lblComTS.AutoSize = True
        Me.lblComTS.Location = New System.Drawing.Point(507, 122)
        Me.lblComTS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblComTS.Name = "lblComTS"
        Me.lblComTS.Size = New System.Drawing.Size(131, 20)
        Me.lblComTS.TabIndex = 14
        Me.lblComTS.Text = "Tech. Supervisor:"
        '
        'lblComSE
        '
        Me.lblComSE.AutoSize = True
        Me.lblComSE.Location = New System.Drawing.Point(507, 83)
        Me.lblComSE.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblComSE.Name = "lblComSE"
        Me.lblComSE.Size = New System.Drawing.Size(159, 20)
        Me.lblComSE.TabIndex = 8
        Me.lblComSE.Text = "Responsible Person :"
        '
        'lblComManager
        '
        Me.lblComManager.AutoSize = True
        Me.lblComManager.Location = New System.Drawing.Point(507, 45)
        Me.lblComManager.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblComManager.Name = "lblComManager"
        Me.lblComManager.Size = New System.Drawing.Size(80, 20)
        Me.lblComManager.TabIndex = 2
        Me.lblComManager.Text = "Manager :"
        '
        'lblComDirector
        '
        Me.lblComDirector.AutoSize = True
        Me.lblComDirector.Location = New System.Drawing.Point(22, 45)
        Me.lblComDirector.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblComDirector.Name = "lblComDirector"
        Me.lblComDirector.Size = New System.Drawing.Size(73, 20)
        Me.lblComDirector.TabIndex = 0
        Me.lblComDirector.Text = "Director :"
        '
        'lblComGM
        '
        Me.lblComGM.AutoSize = True
        Me.lblComGM.Location = New System.Drawing.Point(22, 83)
        Me.lblComGM.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblComGM.Name = "lblComGM"
        Me.lblComGM.Size = New System.Drawing.Size(141, 20)
        Me.lblComGM.TabIndex = 6
        Me.lblComGM.Text = "General Manager :"
        '
        'lblComASM
        '
        Me.lblComASM.AutoSize = True
        Me.lblComASM.Location = New System.Drawing.Point(22, 122)
        Me.lblComASM.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblComASM.Name = "lblComASM"
        Me.lblComASM.Size = New System.Drawing.Size(162, 20)
        Me.lblComASM.TabIndex = 12
        Me.lblComASM.Text = "Area Sales Manager :"
        '
        'gboxGuardInformation
        '
        Me.gboxGuardInformation.Controls.Add(Me.txtGaurdEmail)
        Me.gboxGuardInformation.Controls.Add(Me.txtGaurdMbNo)
        Me.gboxGuardInformation.Controls.Add(Me.txtGaurdName)
        Me.gboxGuardInformation.Controls.Add(Me.lblGaurdEmail)
        Me.gboxGuardInformation.Controls.Add(Me.lblGaurdMbNo)
        Me.gboxGuardInformation.Controls.Add(Me.lblGaurdName)
        Me.gboxGuardInformation.Location = New System.Drawing.Point(928, 191)
        Me.gboxGuardInformation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboxGuardInformation.Name = "gboxGuardInformation"
        Me.gboxGuardInformation.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboxGuardInformation.Size = New System.Drawing.Size(418, 203)
        Me.gboxGuardInformation.TabIndex = 3
        Me.gboxGuardInformation.TabStop = False
        Me.gboxGuardInformation.Text = "4. Guard Information"
        '
        'txtGaurdEmail
        '
        Me.txtGaurdEmail.Location = New System.Drawing.Point(192, 115)
        Me.txtGaurdEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtGaurdEmail.Name = "txtGaurdEmail"
        Me.txtGaurdEmail.Size = New System.Drawing.Size(202, 26)
        Me.txtGaurdEmail.TabIndex = 5
        '
        'txtGaurdMbNo
        '
        Me.txtGaurdMbNo.Location = New System.Drawing.Point(192, 77)
        Me.txtGaurdMbNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtGaurdMbNo.MaxLength = 25
        Me.txtGaurdMbNo.Name = "txtGaurdMbNo"
        Me.txtGaurdMbNo.Size = New System.Drawing.Size(202, 26)
        Me.txtGaurdMbNo.TabIndex = 3
        '
        'txtGaurdName
        '
        Me.txtGaurdName.Location = New System.Drawing.Point(192, 38)
        Me.txtGaurdName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtGaurdName.Name = "txtGaurdName"
        Me.txtGaurdName.Size = New System.Drawing.Size(202, 26)
        Me.txtGaurdName.TabIndex = 1
        '
        'lblGaurdEmail
        '
        Me.lblGaurdEmail.AutoSize = True
        Me.lblGaurdEmail.Location = New System.Drawing.Point(22, 122)
        Me.lblGaurdEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblGaurdEmail.Name = "lblGaurdEmail"
        Me.lblGaurdEmail.Size = New System.Drawing.Size(56, 20)
        Me.lblGaurdEmail.TabIndex = 4
        Me.lblGaurdEmail.Text = "Email :"
        '
        'lblGaurdMbNo
        '
        Me.lblGaurdMbNo.AutoSize = True
        Me.lblGaurdMbNo.Location = New System.Drawing.Point(22, 83)
        Me.lblGaurdMbNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblGaurdMbNo.Name = "lblGaurdMbNo"
        Me.lblGaurdMbNo.Size = New System.Drawing.Size(87, 20)
        Me.lblGaurdMbNo.TabIndex = 2
        Me.lblGaurdMbNo.Text = "Mobile No :"
        '
        'lblGaurdName
        '
        Me.lblGaurdName.AutoSize = True
        Me.lblGaurdName.Location = New System.Drawing.Point(22, 45)
        Me.lblGaurdName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblGaurdName.Name = "lblGaurdName"
        Me.lblGaurdName.Size = New System.Drawing.Size(59, 20)
        Me.lblGaurdName.TabIndex = 0
        Me.lblGaurdName.Text = "Name :"
        '
        'gboxRespresentativeInformation
        '
        Me.gboxRespresentativeInformation.Controls.Add(Me.lblProgress)
        Me.gboxRespresentativeInformation.Controls.Add(Me.txtRepEmail)
        Me.gboxRespresentativeInformation.Controls.Add(Me.txtRepMbNo)
        Me.gboxRespresentativeInformation.Controls.Add(Me.txtRepName)
        Me.gboxRespresentativeInformation.Controls.Add(Me.lblRepEmail)
        Me.gboxRespresentativeInformation.Controls.Add(Me.lblRepMbNo)
        Me.gboxRespresentativeInformation.Controls.Add(Me.lblRepName)
        Me.gboxRespresentativeInformation.Location = New System.Drawing.Point(502, 191)
        Me.gboxRespresentativeInformation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboxRespresentativeInformation.Name = "gboxRespresentativeInformation"
        Me.gboxRespresentativeInformation.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboxRespresentativeInformation.Size = New System.Drawing.Size(417, 203)
        Me.gboxRespresentativeInformation.TabIndex = 2
        Me.gboxRespresentativeInformation.TabStop = False
        Me.gboxRespresentativeInformation.Text = "3. Customer Representative"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(14, 52)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(394, 69)
        Me.lblProgress.TabIndex = 6
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'txtRepEmail
        '
        Me.txtRepEmail.Location = New System.Drawing.Point(165, 115)
        Me.txtRepEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRepEmail.Name = "txtRepEmail"
        Me.txtRepEmail.Size = New System.Drawing.Size(228, 26)
        Me.txtRepEmail.TabIndex = 5
        '
        'txtRepMbNo
        '
        Me.txtRepMbNo.Location = New System.Drawing.Point(165, 77)
        Me.txtRepMbNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRepMbNo.MaxLength = 25
        Me.txtRepMbNo.Name = "txtRepMbNo"
        Me.txtRepMbNo.Size = New System.Drawing.Size(228, 26)
        Me.txtRepMbNo.TabIndex = 3
        '
        'txtRepName
        '
        Me.txtRepName.Location = New System.Drawing.Point(165, 38)
        Me.txtRepName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRepName.Name = "txtRepName"
        Me.txtRepName.Size = New System.Drawing.Size(228, 26)
        Me.txtRepName.TabIndex = 1
        '
        'lblRepEmail
        '
        Me.lblRepEmail.AutoSize = True
        Me.lblRepEmail.Location = New System.Drawing.Point(22, 122)
        Me.lblRepEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRepEmail.Name = "lblRepEmail"
        Me.lblRepEmail.Size = New System.Drawing.Size(56, 20)
        Me.lblRepEmail.TabIndex = 4
        Me.lblRepEmail.Text = "Email :"
        '
        'lblRepMbNo
        '
        Me.lblRepMbNo.AutoSize = True
        Me.lblRepMbNo.Location = New System.Drawing.Point(22, 83)
        Me.lblRepMbNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRepMbNo.Name = "lblRepMbNo"
        Me.lblRepMbNo.Size = New System.Drawing.Size(87, 20)
        Me.lblRepMbNo.TabIndex = 2
        Me.lblRepMbNo.Text = "Mobile No :"
        '
        'lblRepName
        '
        Me.lblRepName.AutoSize = True
        Me.lblRepName.Location = New System.Drawing.Point(22, 45)
        Me.lblRepName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRepName.Name = "lblRepName"
        Me.lblRepName.Size = New System.Drawing.Size(59, 20)
        Me.lblRepName.TabIndex = 0
        Me.lblRepName.Text = "Name :"
        '
        'gboxCustomerInformation
        '
        Me.gboxCustomerInformation.Controls.Add(Me.txtCustEmail)
        Me.gboxCustomerInformation.Controls.Add(Me.txtCustMob)
        Me.gboxCustomerInformation.Controls.Add(Me.txtCustOffAdd)
        Me.gboxCustomerInformation.Controls.Add(Me.txtCustName)
        Me.gboxCustomerInformation.Controls.Add(Me.lblCustEmail)
        Me.gboxCustomerInformation.Controls.Add(Me.lblCustMob)
        Me.gboxCustomerInformation.Controls.Add(Me.lblCustOffAdd)
        Me.gboxCustomerInformation.Controls.Add(Me.lblCustName)
        Me.gboxCustomerInformation.Location = New System.Drawing.Point(502, 18)
        Me.gboxCustomerInformation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboxCustomerInformation.Name = "gboxCustomerInformation"
        Me.gboxCustomerInformation.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboxCustomerInformation.Size = New System.Drawing.Size(844, 163)
        Me.gboxCustomerInformation.TabIndex = 1
        Me.gboxCustomerInformation.TabStop = False
        Me.gboxCustomerInformation.Text = "2. Customer Information"
        '
        'txtCustEmail
        '
        Me.txtCustEmail.Location = New System.Drawing.Point(602, 77)
        Me.txtCustEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCustEmail.Name = "txtCustEmail"
        Me.txtCustEmail.Size = New System.Drawing.Size(218, 26)
        Me.txtCustEmail.TabIndex = 7
        '
        'txtCustMob
        '
        Me.txtCustMob.Location = New System.Drawing.Point(602, 38)
        Me.txtCustMob.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCustMob.MaxLength = 25
        Me.txtCustMob.Name = "txtCustMob"
        Me.txtCustMob.Size = New System.Drawing.Size(218, 26)
        Me.txtCustMob.TabIndex = 3
        '
        'txtCustOffAdd
        '
        Me.txtCustOffAdd.Location = New System.Drawing.Point(165, 77)
        Me.txtCustOffAdd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCustOffAdd.Name = "txtCustOffAdd"
        Me.txtCustOffAdd.Size = New System.Drawing.Size(228, 26)
        Me.txtCustOffAdd.TabIndex = 5
        '
        'txtCustName
        '
        Me.txtCustName.Location = New System.Drawing.Point(165, 38)
        Me.txtCustName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.Size = New System.Drawing.Size(228, 26)
        Me.txtCustName.TabIndex = 1
        '
        'lblCustEmail
        '
        Me.lblCustEmail.AutoSize = True
        Me.lblCustEmail.Location = New System.Drawing.Point(434, 83)
        Me.lblCustEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustEmail.Name = "lblCustEmail"
        Me.lblCustEmail.Size = New System.Drawing.Size(56, 20)
        Me.lblCustEmail.TabIndex = 6
        Me.lblCustEmail.Text = "Email :"
        '
        'lblCustMob
        '
        Me.lblCustMob.AutoSize = True
        Me.lblCustMob.Location = New System.Drawing.Point(434, 45)
        Me.lblCustMob.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustMob.Name = "lblCustMob"
        Me.lblCustMob.Size = New System.Drawing.Size(87, 20)
        Me.lblCustMob.TabIndex = 2
        Me.lblCustMob.Text = "Mobile No :"
        '
        'lblCustOffAdd
        '
        Me.lblCustOffAdd.AutoSize = True
        Me.lblCustOffAdd.Location = New System.Drawing.Point(22, 83)
        Me.lblCustOffAdd.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustOffAdd.Name = "lblCustOffAdd"
        Me.lblCustOffAdd.Size = New System.Drawing.Size(122, 20)
        Me.lblCustOffAdd.TabIndex = 4
        Me.lblCustOffAdd.Text = "Office Address :"
        '
        'lblCustName
        '
        Me.lblCustName.AutoSize = True
        Me.lblCustName.Location = New System.Drawing.Point(22, 45)
        Me.lblCustName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustName.Name = "lblCustName"
        Me.lblCustName.Size = New System.Drawing.Size(132, 20)
        Me.lblCustName.TabIndex = 0
        Me.lblCustName.Text = "Customer Name :"
        '
        'gboxProjectInformation
        '
        Me.gboxProjectInformation.Controls.Add(Me.Label3)
        Me.gboxProjectInformation.Controls.Add(Me.dtpProjDate)
        Me.gboxProjectInformation.Controls.Add(Me.txtCity)
        Me.gboxProjectInformation.Controls.Add(Me.txtArea)
        Me.gboxProjectInformation.Controls.Add(Me.txtPhase)
        Me.gboxProjectInformation.Controls.Add(Me.txtBlockNo)
        Me.gboxProjectInformation.Controls.Add(Me.txtSiteAddress)
        Me.gboxProjectInformation.Controls.Add(Me.txtProjectName)
        Me.gboxProjectInformation.Controls.Add(Me.txtProjectCode)
        Me.gboxProjectInformation.Controls.Add(Me.lblCity)
        Me.gboxProjectInformation.Controls.Add(Me.lblArea)
        Me.gboxProjectInformation.Controls.Add(Me.lblPhase)
        Me.gboxProjectInformation.Controls.Add(Me.lblBlockNo)
        Me.gboxProjectInformation.Controls.Add(Me.lblSiteAddress)
        Me.gboxProjectInformation.Controls.Add(Me.lblProjectName)
        Me.gboxProjectInformation.Controls.Add(Me.lblProjectCode)
        Me.gboxProjectInformation.Location = New System.Drawing.Point(18, 18)
        Me.gboxProjectInformation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboxProjectInformation.Name = "gboxProjectInformation"
        Me.gboxProjectInformation.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboxProjectInformation.Size = New System.Drawing.Size(476, 375)
        Me.gboxProjectInformation.TabIndex = 0
        Me.gboxProjectInformation.TabStop = False
        Me.gboxProjectInformation.Text = "1. Project Information"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 85)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(138, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Project Initial Date"
        '
        'dtpProjDate
        '
        Me.dtpProjDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpProjDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpProjDate.Location = New System.Drawing.Point(192, 78)
        Me.dtpProjDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpProjDate.Name = "dtpProjDate"
        Me.dtpProjDate.Size = New System.Drawing.Size(272, 26)
        Me.dtpProjDate.TabIndex = 3
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(192, 311)
        Me.txtCity.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(272, 26)
        Me.txtCity.TabIndex = 15
        '
        'txtArea
        '
        Me.txtArea.Location = New System.Drawing.Point(192, 272)
        Me.txtArea.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtArea.Name = "txtArea"
        Me.txtArea.Size = New System.Drawing.Size(272, 26)
        Me.txtArea.TabIndex = 13
        '
        'txtPhase
        '
        Me.txtPhase.Location = New System.Drawing.Point(192, 234)
        Me.txtPhase.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPhase.Name = "txtPhase"
        Me.txtPhase.Size = New System.Drawing.Size(272, 26)
        Me.txtPhase.TabIndex = 11
        '
        'txtBlockNo
        '
        Me.txtBlockNo.Location = New System.Drawing.Point(192, 195)
        Me.txtBlockNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBlockNo.Name = "txtBlockNo"
        Me.txtBlockNo.Size = New System.Drawing.Size(272, 26)
        Me.txtBlockNo.TabIndex = 9
        '
        'txtSiteAddress
        '
        Me.txtSiteAddress.Location = New System.Drawing.Point(192, 157)
        Me.txtSiteAddress.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSiteAddress.Name = "txtSiteAddress"
        Me.txtSiteAddress.Size = New System.Drawing.Size(272, 26)
        Me.txtSiteAddress.TabIndex = 7
        '
        'txtProjectName
        '
        Me.txtProjectName.Location = New System.Drawing.Point(192, 118)
        Me.txtProjectName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtProjectName.Name = "txtProjectName"
        Me.txtProjectName.Size = New System.Drawing.Size(272, 26)
        Me.txtProjectName.TabIndex = 5
        '
        'txtProjectCode
        '
        Me.txtProjectCode.Location = New System.Drawing.Point(192, 38)
        Me.txtProjectCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtProjectCode.Name = "txtProjectCode"
        Me.txtProjectCode.ReadOnly = True
        Me.txtProjectCode.Size = New System.Drawing.Size(272, 26)
        Me.txtProjectCode.TabIndex = 1
        Me.txtProjectCode.TabStop = False
        '
        'lblCity
        '
        Me.lblCity.AutoSize = True
        Me.lblCity.Location = New System.Drawing.Point(22, 317)
        Me.lblCity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(43, 20)
        Me.lblCity.TabIndex = 14
        Me.lblCity.Text = "City :"
        '
        'lblArea
        '
        Me.lblArea.AutoSize = True
        Me.lblArea.Location = New System.Drawing.Point(22, 278)
        Me.lblArea.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblArea.Name = "lblArea"
        Me.lblArea.Size = New System.Drawing.Size(51, 20)
        Me.lblArea.TabIndex = 12
        Me.lblArea.Text = "Area :"
        '
        'lblPhase
        '
        Me.lblPhase.AutoSize = True
        Me.lblPhase.Location = New System.Drawing.Point(22, 240)
        Me.lblPhase.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPhase.Name = "lblPhase"
        Me.lblPhase.Size = New System.Drawing.Size(62, 20)
        Me.lblPhase.TabIndex = 10
        Me.lblPhase.Text = "Phase :"
        '
        'lblBlockNo
        '
        Me.lblBlockNo.AutoSize = True
        Me.lblBlockNo.Location = New System.Drawing.Point(22, 202)
        Me.lblBlockNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBlockNo.Name = "lblBlockNo"
        Me.lblBlockNo.Size = New System.Drawing.Size(80, 20)
        Me.lblBlockNo.TabIndex = 8
        Me.lblBlockNo.Text = "Block No :"
        '
        'lblSiteAddress
        '
        Me.lblSiteAddress.AutoSize = True
        Me.lblSiteAddress.Location = New System.Drawing.Point(22, 163)
        Me.lblSiteAddress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSiteAddress.Name = "lblSiteAddress"
        Me.lblSiteAddress.Size = New System.Drawing.Size(108, 20)
        Me.lblSiteAddress.TabIndex = 6
        Me.lblSiteAddress.Text = "Site Address :"
        '
        'lblProjectName
        '
        Me.lblProjectName.AutoSize = True
        Me.lblProjectName.Location = New System.Drawing.Point(22, 125)
        Me.lblProjectName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProjectName.Name = "lblProjectName"
        Me.lblProjectName.Size = New System.Drawing.Size(112, 20)
        Me.lblProjectName.TabIndex = 4
        Me.lblProjectName.Text = "Project Name :"
        '
        'lblProjectCode
        '
        Me.lblProjectCode.AutoSize = True
        Me.lblProjectCode.Location = New System.Drawing.Point(22, 45)
        Me.lblProjectCode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProjectCode.Name = "lblProjectCode"
        Me.lblProjectCode.Size = New System.Drawing.Size(108, 20)
        Me.lblProjectCode.TabIndex = 0
        Me.lblProjectCode.Text = "Project Code :"
        '
        'OtherTab
        '
        Me.OtherTab.BackColor = System.Drawing.Color.Transparent
        Me.OtherTab.Controls.Add(Me.gboxConsultantInformation)
        Me.OtherTab.Controls.Add(Me.GroupBox2)
        Me.OtherTab.Controls.Add(Me.gboxBuilderInformation)
        Me.OtherTab.Location = New System.Drawing.Point(4, 29)
        Me.OtherTab.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.OtherTab.Name = "OtherTab"
        Me.OtherTab.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.OtherTab.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OtherTab.Size = New System.Drawing.Size(1467, 852)
        Me.OtherTab.TabIndex = 1
        Me.OtherTab.Text = "Other Info"
        '
        'gboxConsultantInformation
        '
        Me.gboxConsultantInformation.Controls.Add(Me.txtConConEmail)
        Me.gboxConsultantInformation.Controls.Add(Me.txtConConMbNo)
        Me.gboxConsultantInformation.Controls.Add(Me.txtConConName)
        Me.gboxConsultantInformation.Controls.Add(Me.lblConConEmail)
        Me.gboxConsultantInformation.Controls.Add(Me.lblConConMbNo)
        Me.gboxConsultantInformation.Controls.Add(Me.lblConConName)
        Me.gboxConsultantInformation.Controls.Add(Me.txtConMAINEmail)
        Me.gboxConsultantInformation.Controls.Add(Me.txtConMainMbNo)
        Me.gboxConsultantInformation.Controls.Add(Me.txtConMainName)
        Me.gboxConsultantInformation.Controls.Add(Me.lblConMAINEmail)
        Me.gboxConsultantInformation.Controls.Add(Me.lblConMainMbNo)
        Me.gboxConsultantInformation.Controls.Add(Me.lblConMainName)
        Me.gboxConsultantInformation.Controls.Add(Me.txtConEmail)
        Me.gboxConsultantInformation.Controls.Add(Me.txtConMBNo)
        Me.gboxConsultantInformation.Controls.Add(Me.txtConOwner)
        Me.gboxConsultantInformation.Controls.Add(Me.lblConEmail)
        Me.gboxConsultantInformation.Controls.Add(Me.lblConMBNo)
        Me.gboxConsultantInformation.Controls.Add(Me.lblConOwner)
        Me.gboxConsultantInformation.Controls.Add(Me.txtConPhNo)
        Me.gboxConsultantInformation.Controls.Add(Me.txtConAdd)
        Me.gboxConsultantInformation.Controls.Add(Me.txtConName)
        Me.gboxConsultantInformation.Controls.Add(Me.lblConPhNo)
        Me.gboxConsultantInformation.Controls.Add(Me.lblConAdd)
        Me.gboxConsultantInformation.Controls.Add(Me.lblConName)
        Me.gboxConsultantInformation.Location = New System.Drawing.Point(18, 411)
        Me.gboxConsultantInformation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboxConsultantInformation.Name = "gboxConsultantInformation"
        Me.gboxConsultantInformation.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboxConsultantInformation.Size = New System.Drawing.Size(1358, 189)
        Me.gboxConsultantInformation.TabIndex = 2
        Me.gboxConsultantInformation.TabStop = False
        Me.gboxConsultantInformation.Text = "3. Consultant Information"
        '
        'txtConConEmail
        '
        Me.txtConConEmail.Location = New System.Drawing.Point(1084, 146)
        Me.txtConConEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtConConEmail.Name = "txtConConEmail"
        Me.txtConConEmail.Size = New System.Drawing.Size(202, 26)
        Me.txtConConEmail.TabIndex = 23
        '
        'txtConConMbNo
        '
        Me.txtConConMbNo.Location = New System.Drawing.Point(622, 146)
        Me.txtConConMbNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtConConMbNo.MaxLength = 25
        Me.txtConConMbNo.Name = "txtConConMbNo"
        Me.txtConConMbNo.Size = New System.Drawing.Size(202, 26)
        Me.txtConConMbNo.TabIndex = 21
        '
        'txtConConName
        '
        Me.txtConConName.Location = New System.Drawing.Point(238, 146)
        Me.txtConConName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtConConName.Name = "txtConConName"
        Me.txtConConName.Size = New System.Drawing.Size(202, 26)
        Me.txtConConName.TabIndex = 19
        '
        'lblConConEmail
        '
        Me.lblConConEmail.AutoSize = True
        Me.lblConConEmail.Location = New System.Drawing.Point(884, 146)
        Me.lblConConEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConConEmail.Name = "lblConConEmail"
        Me.lblConConEmail.Size = New System.Drawing.Size(56, 20)
        Me.lblConConEmail.TabIndex = 22
        Me.lblConConEmail.Text = "Email :"
        '
        'lblConConMbNo
        '
        Me.lblConConMbNo.AutoSize = True
        Me.lblConConMbNo.Location = New System.Drawing.Point(452, 146)
        Me.lblConConMbNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConConMbNo.Name = "lblConConMbNo"
        Me.lblConConMbNo.Size = New System.Drawing.Size(87, 20)
        Me.lblConConMbNo.TabIndex = 20
        Me.lblConConMbNo.Text = "Mobile No :"
        '
        'lblConConName
        '
        Me.lblConConName.AutoSize = True
        Me.lblConConName.Location = New System.Drawing.Point(22, 146)
        Me.lblConConName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConConName.Name = "lblConConName"
        Me.lblConConName.Size = New System.Drawing.Size(222, 20)
        Me.lblConConName.TabIndex = 18
        Me.lblConConName.Text = "Concerned Consultant Name :"
        '
        'txtConMAINEmail
        '
        Me.txtConMAINEmail.Location = New System.Drawing.Point(1084, 108)
        Me.txtConMAINEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtConMAINEmail.Name = "txtConMAINEmail"
        Me.txtConMAINEmail.Size = New System.Drawing.Size(202, 26)
        Me.txtConMAINEmail.TabIndex = 17
        '
        'txtConMainMbNo
        '
        Me.txtConMainMbNo.Location = New System.Drawing.Point(622, 108)
        Me.txtConMainMbNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtConMainMbNo.MaxLength = 25
        Me.txtConMainMbNo.Name = "txtConMainMbNo"
        Me.txtConMainMbNo.Size = New System.Drawing.Size(202, 26)
        Me.txtConMainMbNo.TabIndex = 15
        '
        'txtConMainName
        '
        Me.txtConMainName.Location = New System.Drawing.Point(238, 108)
        Me.txtConMainName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtConMainName.Name = "txtConMainName"
        Me.txtConMainName.Size = New System.Drawing.Size(202, 26)
        Me.txtConMainName.TabIndex = 13
        '
        'lblConMAINEmail
        '
        Me.lblConMAINEmail.AutoSize = True
        Me.lblConMAINEmail.Location = New System.Drawing.Point(884, 108)
        Me.lblConMAINEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConMAINEmail.Name = "lblConMAINEmail"
        Me.lblConMAINEmail.Size = New System.Drawing.Size(56, 20)
        Me.lblConMAINEmail.TabIndex = 16
        Me.lblConMAINEmail.Text = "Email :"
        '
        'lblConMainMbNo
        '
        Me.lblConMainMbNo.AutoSize = True
        Me.lblConMainMbNo.Location = New System.Drawing.Point(452, 108)
        Me.lblConMainMbNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConMainMbNo.Name = "lblConMainMbNo"
        Me.lblConMainMbNo.Size = New System.Drawing.Size(87, 20)
        Me.lblConMainMbNo.TabIndex = 14
        Me.lblConMainMbNo.Text = "Mobile No :"
        '
        'lblConMainName
        '
        Me.lblConMainName.AutoSize = True
        Me.lblConMainName.Location = New System.Drawing.Point(22, 108)
        Me.lblConMainName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConMainName.Name = "lblConMainName"
        Me.lblConMainName.Size = New System.Drawing.Size(178, 20)
        Me.lblConMainName.TabIndex = 12
        Me.lblConMainName.Text = "Main Consultant Name :"
        '
        'txtConEmail
        '
        Me.txtConEmail.Location = New System.Drawing.Point(1084, 69)
        Me.txtConEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtConEmail.Name = "txtConEmail"
        Me.txtConEmail.Size = New System.Drawing.Size(202, 26)
        Me.txtConEmail.TabIndex = 11
        '
        'txtConMBNo
        '
        Me.txtConMBNo.Location = New System.Drawing.Point(622, 69)
        Me.txtConMBNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtConMBNo.MaxLength = 25
        Me.txtConMBNo.Name = "txtConMBNo"
        Me.txtConMBNo.Size = New System.Drawing.Size(202, 26)
        Me.txtConMBNo.TabIndex = 9
        '
        'txtConOwner
        '
        Me.txtConOwner.Location = New System.Drawing.Point(238, 69)
        Me.txtConOwner.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtConOwner.Name = "txtConOwner"
        Me.txtConOwner.Size = New System.Drawing.Size(202, 26)
        Me.txtConOwner.TabIndex = 7
        '
        'lblConEmail
        '
        Me.lblConEmail.AutoSize = True
        Me.lblConEmail.Location = New System.Drawing.Point(884, 69)
        Me.lblConEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConEmail.Name = "lblConEmail"
        Me.lblConEmail.Size = New System.Drawing.Size(56, 20)
        Me.lblConEmail.TabIndex = 10
        Me.lblConEmail.Text = "Email :"
        '
        'lblConMBNo
        '
        Me.lblConMBNo.AutoSize = True
        Me.lblConMBNo.Location = New System.Drawing.Point(452, 69)
        Me.lblConMBNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConMBNo.Name = "lblConMBNo"
        Me.lblConMBNo.Size = New System.Drawing.Size(87, 20)
        Me.lblConMBNo.TabIndex = 8
        Me.lblConMBNo.Text = "Mobile No :"
        '
        'lblConOwner
        '
        Me.lblConOwner.AutoSize = True
        Me.lblConOwner.Location = New System.Drawing.Point(22, 69)
        Me.lblConOwner.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConOwner.Name = "lblConOwner"
        Me.lblConOwner.Size = New System.Drawing.Size(105, 20)
        Me.lblConOwner.TabIndex = 6
        Me.lblConOwner.Text = "Owner Name:"
        '
        'txtConPhNo
        '
        Me.txtConPhNo.Location = New System.Drawing.Point(1084, 31)
        Me.txtConPhNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtConPhNo.Name = "txtConPhNo"
        Me.txtConPhNo.Size = New System.Drawing.Size(202, 26)
        Me.txtConPhNo.TabIndex = 5
        '
        'txtConAdd
        '
        Me.txtConAdd.Location = New System.Drawing.Point(622, 31)
        Me.txtConAdd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtConAdd.Name = "txtConAdd"
        Me.txtConAdd.Size = New System.Drawing.Size(202, 26)
        Me.txtConAdd.TabIndex = 3
        '
        'txtConName
        '
        Me.txtConName.Location = New System.Drawing.Point(238, 31)
        Me.txtConName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtConName.Name = "txtConName"
        Me.txtConName.Size = New System.Drawing.Size(202, 26)
        Me.txtConName.TabIndex = 1
        '
        'lblConPhNo
        '
        Me.lblConPhNo.AutoSize = True
        Me.lblConPhNo.Location = New System.Drawing.Point(884, 31)
        Me.lblConPhNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConPhNo.Name = "lblConPhNo"
        Me.lblConPhNo.Size = New System.Drawing.Size(158, 20)
        Me.lblConPhNo.TabIndex = 4
        Me.lblConPhNo.Text = "Company Phone No :"
        '
        'lblConAdd
        '
        Me.lblConAdd.AutoSize = True
        Me.lblConAdd.Location = New System.Drawing.Point(452, 31)
        Me.lblConAdd.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConAdd.Name = "lblConAdd"
        Me.lblConAdd.Size = New System.Drawing.Size(147, 20)
        Me.lblConAdd.TabIndex = 2
        Me.lblConAdd.Text = "Company Address :"
        '
        'lblConName
        '
        Me.lblConName.AutoSize = True
        Me.lblConName.Location = New System.Drawing.Point(22, 31)
        Me.lblConName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblConName.Name = "lblConName"
        Me.lblConName.Size = New System.Drawing.Size(130, 20)
        Me.lblConName.TabIndex = 0
        Me.lblConName.Text = "Company Name :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtArcPMEmail)
        Me.GroupBox2.Controls.Add(Me.txtArcPMMbNo)
        Me.GroupBox2.Controls.Add(Me.txtArcPManager)
        Me.GroupBox2.Controls.Add(Me.lblArcPMEmail)
        Me.GroupBox2.Controls.Add(Me.lblArcPMMbNo)
        Me.GroupBox2.Controls.Add(Me.lblArcPManager)
        Me.GroupBox2.Controls.Add(Me.txtArcGMEmail)
        Me.GroupBox2.Controls.Add(Me.txtArcGMMbNo)
        Me.GroupBox2.Controls.Add(Me.txtArcGManager)
        Me.GroupBox2.Controls.Add(Me.lblArcGMEmail)
        Me.GroupBox2.Controls.Add(Me.lblArcGMMbNo)
        Me.GroupBox2.Controls.Add(Me.lblArcGManager)
        Me.GroupBox2.Controls.Add(Me.txtArchEmail)
        Me.GroupBox2.Controls.Add(Me.txtArcMBNo)
        Me.GroupBox2.Controls.Add(Me.txtArcOwner)
        Me.GroupBox2.Controls.Add(Me.lblArchEmail)
        Me.GroupBox2.Controls.Add(Me.lblArcMBNo)
        Me.GroupBox2.Controls.Add(Me.lblArcOwner)
        Me.GroupBox2.Controls.Add(Me.txtArcPhNo)
        Me.GroupBox2.Controls.Add(Me.txtArcAdd)
        Me.GroupBox2.Controls.Add(Me.txtArcName)
        Me.GroupBox2.Controls.Add(Me.lblArcPhNo)
        Me.GroupBox2.Controls.Add(Me.lblArcAdd)
        Me.GroupBox2.Controls.Add(Me.lblArcName)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 212)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(1358, 189)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "2. Architect Information"
        '
        'txtArcPMEmail
        '
        Me.txtArcPMEmail.Location = New System.Drawing.Point(1084, 146)
        Me.txtArcPMEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtArcPMEmail.Name = "txtArcPMEmail"
        Me.txtArcPMEmail.Size = New System.Drawing.Size(202, 26)
        Me.txtArcPMEmail.TabIndex = 23
        '
        'txtArcPMMbNo
        '
        Me.txtArcPMMbNo.Location = New System.Drawing.Point(622, 146)
        Me.txtArcPMMbNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtArcPMMbNo.MaxLength = 25
        Me.txtArcPMMbNo.Name = "txtArcPMMbNo"
        Me.txtArcPMMbNo.Size = New System.Drawing.Size(202, 26)
        Me.txtArcPMMbNo.TabIndex = 21
        '
        'txtArcPManager
        '
        Me.txtArcPManager.Location = New System.Drawing.Point(238, 146)
        Me.txtArcPManager.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtArcPManager.Name = "txtArcPManager"
        Me.txtArcPManager.Size = New System.Drawing.Size(202, 26)
        Me.txtArcPManager.TabIndex = 19
        '
        'lblArcPMEmail
        '
        Me.lblArcPMEmail.AutoSize = True
        Me.lblArcPMEmail.Location = New System.Drawing.Point(884, 146)
        Me.lblArcPMEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblArcPMEmail.Name = "lblArcPMEmail"
        Me.lblArcPMEmail.Size = New System.Drawing.Size(56, 20)
        Me.lblArcPMEmail.TabIndex = 22
        Me.lblArcPMEmail.Text = "Email :"
        '
        'lblArcPMMbNo
        '
        Me.lblArcPMMbNo.AutoSize = True
        Me.lblArcPMMbNo.Location = New System.Drawing.Point(452, 146)
        Me.lblArcPMMbNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblArcPMMbNo.Name = "lblArcPMMbNo"
        Me.lblArcPMMbNo.Size = New System.Drawing.Size(87, 20)
        Me.lblArcPMMbNo.TabIndex = 20
        Me.lblArcPMMbNo.Text = "Mobile No :"
        '
        'lblArcPManager
        '
        Me.lblArcPManager.AutoSize = True
        Me.lblArcPManager.Location = New System.Drawing.Point(22, 146)
        Me.lblArcPManager.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblArcPManager.Name = "lblArcPManager"
        Me.lblArcPManager.Size = New System.Drawing.Size(208, 20)
        Me.lblArcPManager.TabIndex = 18
        Me.lblArcPManager.Text = "Concerned Architect Name :"
        '
        'txtArcGMEmail
        '
        Me.txtArcGMEmail.Location = New System.Drawing.Point(1084, 108)
        Me.txtArcGMEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtArcGMEmail.Name = "txtArcGMEmail"
        Me.txtArcGMEmail.Size = New System.Drawing.Size(202, 26)
        Me.txtArcGMEmail.TabIndex = 17
        '
        'txtArcGMMbNo
        '
        Me.txtArcGMMbNo.Location = New System.Drawing.Point(622, 108)
        Me.txtArcGMMbNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtArcGMMbNo.MaxLength = 25
        Me.txtArcGMMbNo.Name = "txtArcGMMbNo"
        Me.txtArcGMMbNo.Size = New System.Drawing.Size(202, 26)
        Me.txtArcGMMbNo.TabIndex = 15
        '
        'txtArcGManager
        '
        Me.txtArcGManager.Location = New System.Drawing.Point(238, 108)
        Me.txtArcGManager.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtArcGManager.Name = "txtArcGManager"
        Me.txtArcGManager.Size = New System.Drawing.Size(202, 26)
        Me.txtArcGManager.TabIndex = 13
        '
        'lblArcGMEmail
        '
        Me.lblArcGMEmail.AutoSize = True
        Me.lblArcGMEmail.Location = New System.Drawing.Point(884, 108)
        Me.lblArcGMEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblArcGMEmail.Name = "lblArcGMEmail"
        Me.lblArcGMEmail.Size = New System.Drawing.Size(56, 20)
        Me.lblArcGMEmail.TabIndex = 16
        Me.lblArcGMEmail.Text = "Email :"
        '
        'lblArcGMMbNo
        '
        Me.lblArcGMMbNo.AutoSize = True
        Me.lblArcGMMbNo.Location = New System.Drawing.Point(452, 108)
        Me.lblArcGMMbNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblArcGMMbNo.Name = "lblArcGMMbNo"
        Me.lblArcGMMbNo.Size = New System.Drawing.Size(87, 20)
        Me.lblArcGMMbNo.TabIndex = 14
        Me.lblArcGMMbNo.Text = "Mobile No :"
        '
        'lblArcGManager
        '
        Me.lblArcGManager.AutoSize = True
        Me.lblArcGManager.Location = New System.Drawing.Point(22, 108)
        Me.lblArcGManager.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblArcGManager.Name = "lblArcGManager"
        Me.lblArcGManager.Size = New System.Drawing.Size(164, 20)
        Me.lblArcGManager.TabIndex = 12
        Me.lblArcGManager.Text = "Main Architect Name :"
        '
        'txtArchEmail
        '
        Me.txtArchEmail.Location = New System.Drawing.Point(1084, 69)
        Me.txtArchEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtArchEmail.Name = "txtArchEmail"
        Me.txtArchEmail.Size = New System.Drawing.Size(202, 26)
        Me.txtArchEmail.TabIndex = 11
        '
        'txtArcMBNo
        '
        Me.txtArcMBNo.Location = New System.Drawing.Point(622, 69)
        Me.txtArcMBNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtArcMBNo.MaxLength = 25
        Me.txtArcMBNo.Name = "txtArcMBNo"
        Me.txtArcMBNo.Size = New System.Drawing.Size(202, 26)
        Me.txtArcMBNo.TabIndex = 9
        '
        'txtArcOwner
        '
        Me.txtArcOwner.Location = New System.Drawing.Point(238, 69)
        Me.txtArcOwner.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtArcOwner.Name = "txtArcOwner"
        Me.txtArcOwner.Size = New System.Drawing.Size(202, 26)
        Me.txtArcOwner.TabIndex = 7
        '
        'lblArchEmail
        '
        Me.lblArchEmail.AutoSize = True
        Me.lblArchEmail.Location = New System.Drawing.Point(884, 69)
        Me.lblArchEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblArchEmail.Name = "lblArchEmail"
        Me.lblArchEmail.Size = New System.Drawing.Size(56, 20)
        Me.lblArchEmail.TabIndex = 10
        Me.lblArchEmail.Text = "Email :"
        '
        'lblArcMBNo
        '
        Me.lblArcMBNo.AutoSize = True
        Me.lblArcMBNo.Location = New System.Drawing.Point(452, 69)
        Me.lblArcMBNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblArcMBNo.Name = "lblArcMBNo"
        Me.lblArcMBNo.Size = New System.Drawing.Size(87, 20)
        Me.lblArcMBNo.TabIndex = 8
        Me.lblArcMBNo.Text = "Mobile No :"
        '
        'lblArcOwner
        '
        Me.lblArcOwner.AutoSize = True
        Me.lblArcOwner.Location = New System.Drawing.Point(22, 69)
        Me.lblArcOwner.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblArcOwner.Name = "lblArcOwner"
        Me.lblArcOwner.Size = New System.Drawing.Size(105, 20)
        Me.lblArcOwner.TabIndex = 6
        Me.lblArcOwner.Text = "Owner Name:"
        '
        'txtArcPhNo
        '
        Me.txtArcPhNo.Location = New System.Drawing.Point(1084, 31)
        Me.txtArcPhNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtArcPhNo.Name = "txtArcPhNo"
        Me.txtArcPhNo.Size = New System.Drawing.Size(202, 26)
        Me.txtArcPhNo.TabIndex = 5
        '
        'txtArcAdd
        '
        Me.txtArcAdd.Location = New System.Drawing.Point(622, 31)
        Me.txtArcAdd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtArcAdd.Name = "txtArcAdd"
        Me.txtArcAdd.Size = New System.Drawing.Size(202, 26)
        Me.txtArcAdd.TabIndex = 3
        '
        'txtArcName
        '
        Me.txtArcName.Location = New System.Drawing.Point(238, 31)
        Me.txtArcName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtArcName.Name = "txtArcName"
        Me.txtArcName.Size = New System.Drawing.Size(202, 26)
        Me.txtArcName.TabIndex = 1
        '
        'lblArcPhNo
        '
        Me.lblArcPhNo.AutoSize = True
        Me.lblArcPhNo.Location = New System.Drawing.Point(884, 31)
        Me.lblArcPhNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblArcPhNo.Name = "lblArcPhNo"
        Me.lblArcPhNo.Size = New System.Drawing.Size(158, 20)
        Me.lblArcPhNo.TabIndex = 4
        Me.lblArcPhNo.Text = "Company Phone No :"
        '
        'lblArcAdd
        '
        Me.lblArcAdd.AutoSize = True
        Me.lblArcAdd.Location = New System.Drawing.Point(452, 31)
        Me.lblArcAdd.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblArcAdd.Name = "lblArcAdd"
        Me.lblArcAdd.Size = New System.Drawing.Size(147, 20)
        Me.lblArcAdd.TabIndex = 2
        Me.lblArcAdd.Text = "Company Address :"
        '
        'lblArcName
        '
        Me.lblArcName.AutoSize = True
        Me.lblArcName.Location = New System.Drawing.Point(22, 31)
        Me.lblArcName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblArcName.Name = "lblArcName"
        Me.lblArcName.Size = New System.Drawing.Size(130, 20)
        Me.lblArcName.TabIndex = 0
        Me.lblArcName.Text = "Company Name :"
        '
        'gboxBuilderInformation
        '
        Me.gboxBuilderInformation.Controls.Add(Me.lblBulPMEmail)
        Me.gboxBuilderInformation.Controls.Add(Me.txtBulPMEmail)
        Me.gboxBuilderInformation.Controls.Add(Me.txtBulPMMbNo)
        Me.gboxBuilderInformation.Controls.Add(Me.txtBulPManager)
        Me.gboxBuilderInformation.Controls.Add(Me.lblBulPMMbNo)
        Me.gboxBuilderInformation.Controls.Add(Me.lblBulPManager)
        Me.gboxBuilderInformation.Controls.Add(Me.txtBulGMEmail)
        Me.gboxBuilderInformation.Controls.Add(Me.txtBulGMMbNo)
        Me.gboxBuilderInformation.Controls.Add(Me.txtBulGManager)
        Me.gboxBuilderInformation.Controls.Add(Me.lblBulGMEmail)
        Me.gboxBuilderInformation.Controls.Add(Me.lblBulGMMbNo)
        Me.gboxBuilderInformation.Controls.Add(Me.lblBulGManager)
        Me.gboxBuilderInformation.Controls.Add(Me.txtBulEmail)
        Me.gboxBuilderInformation.Controls.Add(Me.txtBulMbNo)
        Me.gboxBuilderInformation.Controls.Add(Me.txtBulOwner)
        Me.gboxBuilderInformation.Controls.Add(Me.lblBulEmail)
        Me.gboxBuilderInformation.Controls.Add(Me.lblBulMbNo)
        Me.gboxBuilderInformation.Controls.Add(Me.lblBulOwner)
        Me.gboxBuilderInformation.Controls.Add(Me.txtBulPhNo)
        Me.gboxBuilderInformation.Controls.Add(Me.txtBulAdd)
        Me.gboxBuilderInformation.Controls.Add(Me.txtBulName)
        Me.gboxBuilderInformation.Controls.Add(Me.lblBulPhNo)
        Me.gboxBuilderInformation.Controls.Add(Me.lblBulAdd)
        Me.gboxBuilderInformation.Controls.Add(Me.lblBulName)
        Me.gboxBuilderInformation.Location = New System.Drawing.Point(18, 18)
        Me.gboxBuilderInformation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboxBuilderInformation.Name = "gboxBuilderInformation"
        Me.gboxBuilderInformation.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gboxBuilderInformation.Size = New System.Drawing.Size(1358, 185)
        Me.gboxBuilderInformation.TabIndex = 0
        Me.gboxBuilderInformation.TabStop = False
        Me.gboxBuilderInformation.Text = "1. Builder Information"
        '
        'lblBulPMEmail
        '
        Me.lblBulPMEmail.AutoSize = True
        Me.lblBulPMEmail.Location = New System.Drawing.Point(884, 146)
        Me.lblBulPMEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBulPMEmail.Name = "lblBulPMEmail"
        Me.lblBulPMEmail.Size = New System.Drawing.Size(56, 20)
        Me.lblBulPMEmail.TabIndex = 22
        Me.lblBulPMEmail.Text = "Email :"
        '
        'txtBulPMEmail
        '
        Me.txtBulPMEmail.Location = New System.Drawing.Point(1084, 146)
        Me.txtBulPMEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBulPMEmail.Name = "txtBulPMEmail"
        Me.txtBulPMEmail.Size = New System.Drawing.Size(202, 26)
        Me.txtBulPMEmail.TabIndex = 23
        '
        'txtBulPMMbNo
        '
        Me.txtBulPMMbNo.Location = New System.Drawing.Point(622, 146)
        Me.txtBulPMMbNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBulPMMbNo.MaxLength = 25
        Me.txtBulPMMbNo.Name = "txtBulPMMbNo"
        Me.txtBulPMMbNo.Size = New System.Drawing.Size(202, 26)
        Me.txtBulPMMbNo.TabIndex = 21
        '
        'txtBulPManager
        '
        Me.txtBulPManager.Location = New System.Drawing.Point(238, 146)
        Me.txtBulPManager.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBulPManager.Name = "txtBulPManager"
        Me.txtBulPManager.Size = New System.Drawing.Size(202, 26)
        Me.txtBulPManager.TabIndex = 19
        '
        'lblBulPMMbNo
        '
        Me.lblBulPMMbNo.AutoSize = True
        Me.lblBulPMMbNo.Location = New System.Drawing.Point(452, 146)
        Me.lblBulPMMbNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBulPMMbNo.Name = "lblBulPMMbNo"
        Me.lblBulPMMbNo.Size = New System.Drawing.Size(87, 20)
        Me.lblBulPMMbNo.TabIndex = 20
        Me.lblBulPMMbNo.Text = "Mobile No :"
        '
        'lblBulPManager
        '
        Me.lblBulPManager.AutoSize = True
        Me.lblBulPManager.Location = New System.Drawing.Point(22, 146)
        Me.lblBulPManager.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBulPManager.Name = "lblBulPManager"
        Me.lblBulPManager.Size = New System.Drawing.Size(179, 20)
        Me.lblBulPManager.TabIndex = 18
        Me.lblBulPManager.Text = "Project Manager Name :"
        '
        'txtBulGMEmail
        '
        Me.txtBulGMEmail.Location = New System.Drawing.Point(1084, 108)
        Me.txtBulGMEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBulGMEmail.Name = "txtBulGMEmail"
        Me.txtBulGMEmail.Size = New System.Drawing.Size(202, 26)
        Me.txtBulGMEmail.TabIndex = 17
        '
        'txtBulGMMbNo
        '
        Me.txtBulGMMbNo.Location = New System.Drawing.Point(622, 108)
        Me.txtBulGMMbNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBulGMMbNo.MaxLength = 25
        Me.txtBulGMMbNo.Name = "txtBulGMMbNo"
        Me.txtBulGMMbNo.Size = New System.Drawing.Size(202, 26)
        Me.txtBulGMMbNo.TabIndex = 15
        '
        'txtBulGManager
        '
        Me.txtBulGManager.Location = New System.Drawing.Point(238, 108)
        Me.txtBulGManager.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBulGManager.Name = "txtBulGManager"
        Me.txtBulGManager.Size = New System.Drawing.Size(202, 26)
        Me.txtBulGManager.TabIndex = 13
        '
        'lblBulGMEmail
        '
        Me.lblBulGMEmail.AutoSize = True
        Me.lblBulGMEmail.Location = New System.Drawing.Point(884, 108)
        Me.lblBulGMEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBulGMEmail.Name = "lblBulGMEmail"
        Me.lblBulGMEmail.Size = New System.Drawing.Size(56, 20)
        Me.lblBulGMEmail.TabIndex = 16
        Me.lblBulGMEmail.Text = "Email :"
        '
        'lblBulGMMbNo
        '
        Me.lblBulGMMbNo.AutoSize = True
        Me.lblBulGMMbNo.Location = New System.Drawing.Point(452, 108)
        Me.lblBulGMMbNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBulGMMbNo.Name = "lblBulGMMbNo"
        Me.lblBulGMMbNo.Size = New System.Drawing.Size(87, 20)
        Me.lblBulGMMbNo.TabIndex = 14
        Me.lblBulGMMbNo.Text = "Mobile No :"
        '
        'lblBulGManager
        '
        Me.lblBulGManager.AutoSize = True
        Me.lblBulGManager.Location = New System.Drawing.Point(22, 108)
        Me.lblBulGManager.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBulGManager.Name = "lblBulGManager"
        Me.lblBulGManager.Size = New System.Drawing.Size(194, 20)
        Me.lblBulGManager.TabIndex = 12
        Me.lblBulGManager.Text = "Project General Manager :"
        '
        'txtBulEmail
        '
        Me.txtBulEmail.Location = New System.Drawing.Point(1084, 69)
        Me.txtBulEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBulEmail.Name = "txtBulEmail"
        Me.txtBulEmail.Size = New System.Drawing.Size(202, 26)
        Me.txtBulEmail.TabIndex = 11
        '
        'txtBulMbNo
        '
        Me.txtBulMbNo.Location = New System.Drawing.Point(622, 69)
        Me.txtBulMbNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBulMbNo.MaxLength = 25
        Me.txtBulMbNo.Name = "txtBulMbNo"
        Me.txtBulMbNo.Size = New System.Drawing.Size(202, 26)
        Me.txtBulMbNo.TabIndex = 9
        '
        'txtBulOwner
        '
        Me.txtBulOwner.Location = New System.Drawing.Point(238, 69)
        Me.txtBulOwner.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBulOwner.Name = "txtBulOwner"
        Me.txtBulOwner.Size = New System.Drawing.Size(202, 26)
        Me.txtBulOwner.TabIndex = 7
        '
        'lblBulEmail
        '
        Me.lblBulEmail.AutoSize = True
        Me.lblBulEmail.Location = New System.Drawing.Point(884, 69)
        Me.lblBulEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBulEmail.Name = "lblBulEmail"
        Me.lblBulEmail.Size = New System.Drawing.Size(56, 20)
        Me.lblBulEmail.TabIndex = 10
        Me.lblBulEmail.Text = "Email :"
        '
        'lblBulMbNo
        '
        Me.lblBulMbNo.AutoSize = True
        Me.lblBulMbNo.Location = New System.Drawing.Point(452, 69)
        Me.lblBulMbNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBulMbNo.Name = "lblBulMbNo"
        Me.lblBulMbNo.Size = New System.Drawing.Size(87, 20)
        Me.lblBulMbNo.TabIndex = 8
        Me.lblBulMbNo.Text = "Mobile No :"
        '
        'lblBulOwner
        '
        Me.lblBulOwner.AutoSize = True
        Me.lblBulOwner.Location = New System.Drawing.Point(22, 69)
        Me.lblBulOwner.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBulOwner.Name = "lblBulOwner"
        Me.lblBulOwner.Size = New System.Drawing.Size(105, 20)
        Me.lblBulOwner.TabIndex = 6
        Me.lblBulOwner.Text = "Owner Name:"
        '
        'txtBulPhNo
        '
        Me.txtBulPhNo.Location = New System.Drawing.Point(1084, 31)
        Me.txtBulPhNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBulPhNo.Name = "txtBulPhNo"
        Me.txtBulPhNo.Size = New System.Drawing.Size(202, 26)
        Me.txtBulPhNo.TabIndex = 5
        '
        'txtBulAdd
        '
        Me.txtBulAdd.Location = New System.Drawing.Point(622, 31)
        Me.txtBulAdd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBulAdd.Name = "txtBulAdd"
        Me.txtBulAdd.Size = New System.Drawing.Size(202, 26)
        Me.txtBulAdd.TabIndex = 3
        '
        'txtBulName
        '
        Me.txtBulName.Location = New System.Drawing.Point(238, 31)
        Me.txtBulName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBulName.Name = "txtBulName"
        Me.txtBulName.Size = New System.Drawing.Size(202, 26)
        Me.txtBulName.TabIndex = 1
        '
        'lblBulPhNo
        '
        Me.lblBulPhNo.AutoSize = True
        Me.lblBulPhNo.Location = New System.Drawing.Point(884, 31)
        Me.lblBulPhNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBulPhNo.Name = "lblBulPhNo"
        Me.lblBulPhNo.Size = New System.Drawing.Size(158, 20)
        Me.lblBulPhNo.TabIndex = 4
        Me.lblBulPhNo.Text = "Company Phone No :"
        '
        'lblBulAdd
        '
        Me.lblBulAdd.AutoSize = True
        Me.lblBulAdd.Location = New System.Drawing.Point(452, 31)
        Me.lblBulAdd.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBulAdd.Name = "lblBulAdd"
        Me.lblBulAdd.Size = New System.Drawing.Size(147, 20)
        Me.lblBulAdd.TabIndex = 2
        Me.lblBulAdd.Text = "Company Address :"
        '
        'lblBulName
        '
        Me.lblBulName.AutoSize = True
        Me.lblBulName.Location = New System.Drawing.Point(22, 31)
        Me.lblBulName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBulName.Name = "lblBulName"
        Me.lblBulName.Size = New System.Drawing.Size(130, 20)
        Me.lblBulName.TabIndex = 0
        Me.lblBulName.Text = "Company Name :"
        '
        'HistoryTab
        '
        Me.HistoryTab.Controls.Add(Me.grdHistory)
        Me.HistoryTab.Location = New System.Drawing.Point(4, 4)
        Me.HistoryTab.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.HistoryTab.Name = "HistoryTab"
        Me.HistoryTab.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.HistoryTab.Size = New System.Drawing.Size(1483, 895)
        Me.HistoryTab.TabIndex = 1
        Me.HistoryTab.Text = "History"
        Me.HistoryTab.UseVisualStyleBackColor = True
        '
        'grdHistory
        '
        Me.grdHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdHistory.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdHistory.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdHistory.Location = New System.Drawing.Point(4, 5)
        Me.grdHistory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdHistory.Name = "grdHistory"
        Me.grdHistory.RecordNavigator = True
        Me.grdHistory.Size = New System.Drawing.Size(1475, 885)
        Me.grdHistory.TabIndex = 1
        Me.grdHistory.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Me
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(1434, 0)
        Me.CtrlGrdBar2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar2.MyGrid = Me.grdHistory
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar2.TabIndex = 2
        '
        'cmbGM
        '
        Me.cmbGM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbGM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbGM.FormattingEnabled = True
        Me.cmbGM.Location = New System.Drawing.Point(128, 51)
        Me.cmbGM.Name = "cmbGM"
        Me.cmbGM.Size = New System.Drawing.Size(183, 28)
        Me.cmbGM.TabIndex = 23
        '
        'frmProjectPortFolio
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoScrollMargin = New System.Drawing.Size(0, 50)
        Me.ClientSize = New System.Drawing.Size(1491, 960)
        Me.Controls.Add(Me.CtrlGrdBar2)
        Me.Controls.Add(Me.MainTab)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmProjectPortFolio"
        Me.Text = "Project PortFolio"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.MainTab.ResumeLayout(False)
        Me.DataTab.ResumeLayout(False)
        Me.DetailTab.ResumeLayout(False)
        Me.ProjectTab.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gboxGuardInformation.ResumeLayout(False)
        Me.gboxGuardInformation.PerformLayout()
        Me.gboxRespresentativeInformation.ResumeLayout(False)
        Me.gboxRespresentativeInformation.PerformLayout()
        Me.gboxCustomerInformation.ResumeLayout(False)
        Me.gboxCustomerInformation.PerformLayout()
        Me.gboxProjectInformation.ResumeLayout(False)
        Me.gboxProjectInformation.PerformLayout()
        Me.OtherTab.ResumeLayout(False)
        Me.gboxConsultantInformation.ResumeLayout(False)
        Me.gboxConsultantInformation.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.gboxBuilderInformation.ResumeLayout(False)
        Me.gboxBuilderInformation.PerformLayout()
        Me.HistoryTab.ResumeLayout(False)
        CType(Me.grdHistory, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents btnLoadAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents MainTab As System.Windows.Forms.TabControl
    Friend WithEvents DataTab As System.Windows.Forms.TabPage
    Friend WithEvents DetailTab As System.Windows.Forms.TabControl
    Friend WithEvents ProjectTab As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lbltxtProjSize As System.Windows.Forms.Label
    Friend WithEvents lbltxtProjType As System.Windows.Forms.Label
    Friend WithEvents lblComSS As System.Windows.Forms.Label
    Friend WithEvents lblComTS As System.Windows.Forms.Label
    Friend WithEvents lblComSE As System.Windows.Forms.Label
    Friend WithEvents lblComManager As System.Windows.Forms.Label
    Friend WithEvents lblComDirector As System.Windows.Forms.Label
    Friend WithEvents lblComGM As System.Windows.Forms.Label
    Friend WithEvents lblComASM As System.Windows.Forms.Label
    Friend WithEvents gboxGuardInformation As System.Windows.Forms.GroupBox
    Friend WithEvents txtGaurdEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtGaurdMbNo As System.Windows.Forms.TextBox
    Friend WithEvents txtGaurdName As System.Windows.Forms.TextBox
    Friend WithEvents lblGaurdEmail As System.Windows.Forms.Label
    Friend WithEvents lblGaurdMbNo As System.Windows.Forms.Label
    Friend WithEvents lblGaurdName As System.Windows.Forms.Label
    Friend WithEvents gboxRespresentativeInformation As System.Windows.Forms.GroupBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents txtRepEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtRepMbNo As System.Windows.Forms.TextBox
    Friend WithEvents txtRepName As System.Windows.Forms.TextBox
    Friend WithEvents lblRepEmail As System.Windows.Forms.Label
    Friend WithEvents lblRepMbNo As System.Windows.Forms.Label
    Friend WithEvents lblRepName As System.Windows.Forms.Label
    Friend WithEvents gboxCustomerInformation As System.Windows.Forms.GroupBox
    Friend WithEvents txtCustEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtCustMob As System.Windows.Forms.TextBox
    Friend WithEvents txtCustOffAdd As System.Windows.Forms.TextBox
    Friend WithEvents txtCustName As System.Windows.Forms.TextBox
    Friend WithEvents lblCustEmail As System.Windows.Forms.Label
    Friend WithEvents lblCustMob As System.Windows.Forms.Label
    Friend WithEvents lblCustOffAdd As System.Windows.Forms.Label
    Friend WithEvents lblCustName As System.Windows.Forms.Label
    Friend WithEvents gboxProjectInformation As System.Windows.Forms.GroupBox
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtArea As System.Windows.Forms.TextBox
    Friend WithEvents txtPhase As System.Windows.Forms.TextBox
    Friend WithEvents txtBlockNo As System.Windows.Forms.TextBox
    Friend WithEvents txtSiteAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtProjectName As System.Windows.Forms.TextBox
    Friend WithEvents txtProjectCode As System.Windows.Forms.TextBox
    Friend WithEvents lblCity As System.Windows.Forms.Label
    Friend WithEvents lblArea As System.Windows.Forms.Label
    Friend WithEvents lblPhase As System.Windows.Forms.Label
    Friend WithEvents lblBlockNo As System.Windows.Forms.Label
    Friend WithEvents lblSiteAddress As System.Windows.Forms.Label
    Friend WithEvents lblProjectName As System.Windows.Forms.Label
    Friend WithEvents lblProjectCode As System.Windows.Forms.Label
    Friend WithEvents OtherTab As System.Windows.Forms.TabPage
    Friend WithEvents gboxConsultantInformation As System.Windows.Forms.GroupBox
    Friend WithEvents txtConConEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtConConMbNo As System.Windows.Forms.TextBox
    Friend WithEvents txtConConName As System.Windows.Forms.TextBox
    Friend WithEvents lblConConEmail As System.Windows.Forms.Label
    Friend WithEvents lblConConMbNo As System.Windows.Forms.Label
    Friend WithEvents lblConConName As System.Windows.Forms.Label
    Friend WithEvents txtConMAINEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtConMainMbNo As System.Windows.Forms.TextBox
    Friend WithEvents txtConMainName As System.Windows.Forms.TextBox
    Friend WithEvents lblConMAINEmail As System.Windows.Forms.Label
    Friend WithEvents lblConMainMbNo As System.Windows.Forms.Label
    Friend WithEvents lblConMainName As System.Windows.Forms.Label
    Friend WithEvents txtConEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtConMBNo As System.Windows.Forms.TextBox
    Friend WithEvents txtConOwner As System.Windows.Forms.TextBox
    Friend WithEvents lblConEmail As System.Windows.Forms.Label
    Friend WithEvents lblConMBNo As System.Windows.Forms.Label
    Friend WithEvents lblConOwner As System.Windows.Forms.Label
    Friend WithEvents txtConPhNo As System.Windows.Forms.TextBox
    Friend WithEvents txtConAdd As System.Windows.Forms.TextBox
    Friend WithEvents txtConName As System.Windows.Forms.TextBox
    Friend WithEvents lblConPhNo As System.Windows.Forms.Label
    Friend WithEvents lblConAdd As System.Windows.Forms.Label
    Friend WithEvents lblConName As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtArcPMEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtArcPMMbNo As System.Windows.Forms.TextBox
    Friend WithEvents txtArcPManager As System.Windows.Forms.TextBox
    Friend WithEvents lblArcPMEmail As System.Windows.Forms.Label
    Friend WithEvents lblArcPMMbNo As System.Windows.Forms.Label
    Friend WithEvents lblArcPManager As System.Windows.Forms.Label
    Friend WithEvents txtArcGMEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtArcGMMbNo As System.Windows.Forms.TextBox
    Friend WithEvents txtArcGManager As System.Windows.Forms.TextBox
    Friend WithEvents lblArcGMEmail As System.Windows.Forms.Label
    Friend WithEvents lblArcGMMbNo As System.Windows.Forms.Label
    Friend WithEvents lblArcGManager As System.Windows.Forms.Label
    Friend WithEvents txtArchEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtArcMBNo As System.Windows.Forms.TextBox
    Friend WithEvents txtArcOwner As System.Windows.Forms.TextBox
    Friend WithEvents lblArchEmail As System.Windows.Forms.Label
    Friend WithEvents lblArcMBNo As System.Windows.Forms.Label
    Friend WithEvents lblArcOwner As System.Windows.Forms.Label
    Friend WithEvents txtArcPhNo As System.Windows.Forms.TextBox
    Friend WithEvents txtArcAdd As System.Windows.Forms.TextBox
    Friend WithEvents txtArcName As System.Windows.Forms.TextBox
    Friend WithEvents lblArcPhNo As System.Windows.Forms.Label
    Friend WithEvents lblArcAdd As System.Windows.Forms.Label
    Friend WithEvents lblArcName As System.Windows.Forms.Label
    Friend WithEvents gboxBuilderInformation As System.Windows.Forms.GroupBox
    Friend WithEvents lblBulPMEmail As System.Windows.Forms.Label
    Friend WithEvents txtBulPMEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtBulPMMbNo As System.Windows.Forms.TextBox
    Friend WithEvents txtBulPManager As System.Windows.Forms.TextBox
    Friend WithEvents lblBulPMMbNo As System.Windows.Forms.Label
    Friend WithEvents lblBulPManager As System.Windows.Forms.Label
    Friend WithEvents txtBulGMEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtBulGMMbNo As System.Windows.Forms.TextBox
    Friend WithEvents txtBulGManager As System.Windows.Forms.TextBox
    Friend WithEvents lblBulGMEmail As System.Windows.Forms.Label
    Friend WithEvents lblBulGMMbNo As System.Windows.Forms.Label
    Friend WithEvents lblBulGManager As System.Windows.Forms.Label
    Friend WithEvents txtBulEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtBulMbNo As System.Windows.Forms.TextBox
    Friend WithEvents txtBulOwner As System.Windows.Forms.TextBox
    Friend WithEvents lblBulEmail As System.Windows.Forms.Label
    Friend WithEvents lblBulMbNo As System.Windows.Forms.Label
    Friend WithEvents lblBulOwner As System.Windows.Forms.Label
    Friend WithEvents txtBulPhNo As System.Windows.Forms.TextBox
    Friend WithEvents txtBulAdd As System.Windows.Forms.TextBox
    Friend WithEvents txtBulName As System.Windows.Forms.TextBox
    Friend WithEvents lblBulPhNo As System.Windows.Forms.Label
    Friend WithEvents lblBulAdd As System.Windows.Forms.Label
    Friend WithEvents lblBulName As System.Windows.Forms.Label
    Friend WithEvents HistoryTab As System.Windows.Forms.TabPage
    Friend WithEvents grdHistory As Janus.Windows.GridEX.GridEX
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnProjectVisit As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbProjSize As System.Windows.Forms.ComboBox
    Friend WithEvents cmbProjType As System.Windows.Forms.ComboBox
    Friend WithEvents txtAllQuotationValue As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtProjectEstValue As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpProjDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbComDirector As System.Windows.Forms.ComboBox
    Friend WithEvents cmbGManager As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSiteSupervisor As System.Windows.Forms.ComboBox
    Friend WithEvents cmbResPerson As System.Windows.Forms.ComboBox
    Friend WithEvents cmbManager As System.Windows.Forms.ComboBox
    Friend WithEvents cmbASM As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTechSupervisor As System.Windows.Forms.ComboBox
    Friend WithEvents cmbGM As System.Windows.Forms.ComboBox
End Class
