<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmObservationSample
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
        Dim grdLTR_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmObservationSample))
        Dim grdObservationParameters_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdSaved_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.grdLTR = New Janus.Windows.GridEX.GridEX()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblLabTestingRequest = New System.Windows.Forms.Label()
        Me.lblPOLCNo = New System.Windows.Forms.Label()
        Me.cmbTicket = New System.Windows.Forms.ComboBox()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.txtRemarsk = New System.Windows.Forms.TextBox()
        Me.txtPOLCNo = New System.Windows.Forms.TextBox()
        Me.lblQtyBatchSize = New System.Windows.Forms.Label()
        Me.txtQtyBatchSize = New System.Windows.Forms.TextBox()
        Me.dtpRefreshDate = New System.Windows.Forms.DateTimePicker()
        Me.lblRefresh = New System.Windows.Forms.Label()
        Me.dtpMfgDate = New System.Windows.Forms.DateTimePicker()
        Me.lblMfgDate = New System.Windows.Forms.Label()
        Me.dtoExpDate = New System.Windows.Forms.DateTimePicker()
        Me.lblExpDate = New System.Windows.Forms.Label()
        Me.lblBatchNo = New System.Windows.Forms.Label()
        Me.txtBatchNo = New System.Windows.Forms.TextBox()
        Me.lblGRN = New System.Windows.Forms.Label()
        Me.txtGRNNo = New System.Windows.Forms.TextBox()
        Me.lblSuppliers = New System.Windows.Forms.Label()
        Me.txtSuppliers = New System.Windows.Forms.TextBox()
        Me.lblStage = New System.Windows.Forms.Label()
        Me.cmbStage = New System.Windows.Forms.ComboBox()
        Me.lblProduct = New System.Windows.Forms.Label()
        Me.txtProductMatName = New System.Windows.Forms.TextBox()
        Me.lblRequestNo = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.dtpTime = New System.Windows.Forms.DateTimePicker()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.txtRequestNo = New System.Windows.Forms.TextBox()
        Me.cmbBy = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblForQualityControlDepartment = New System.Windows.Forms.Label()
        Me.dtpQCTime = New System.Windows.Forms.DateTimePicker()
        Me.dtpSamplecollectedon = New System.Windows.Forms.DateTimePicker()
        Me.lblSamplecollectedon = New System.Windows.Forms.Label()
        Me.lblAt = New System.Windows.Forms.Label()
        Me.lblContainerNo = New System.Windows.Forms.Label()
        Me.lblBy = New System.Windows.Forms.Label()
        Me.txtContainerNo = New System.Windows.Forms.TextBox()
        Me.lblQuantityOfSample = New System.Windows.Forms.Label()
        Me.txtQuantityOfSample = New System.Windows.Forms.TextBox()
        Me.dtpObserDate = New System.Windows.Forms.DateTimePicker()
        Me.lblObservationDate = New System.Windows.Forms.Label()
        Me.lblQCNumber = New System.Windows.Forms.Label()
        Me.txtQCNumber = New System.Windows.Forms.TextBox()
        Me.grdObservationParameters = New Janus.Windows.GridEX.GridEX()
        Me.pnlObervation = New System.Windows.Forms.Panel()
        Me.lblObservation = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnLabTestRequest = New System.Windows.Forms.ToolStripButton()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnFormPrint = New System.Windows.Forms.ToolStripButton()
        Me.btnPrintSticker = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnNotification = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.VisualStyleManager1 = New Janus.Windows.Common.VisualStyleManager(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblSample = New System.Windows.Forms.Label()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.grdLTR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.grdObservationParameters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlObervation.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.SplitContainer1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1547, 765)
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.grdLTR)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1547, 765)
        Me.SplitContainer1.SplitterDistance = 514
        Me.SplitContainer1.SplitterWidth = 6
        Me.SplitContainer1.TabIndex = 23
        '
        'grdLTR
        '
        Me.grdLTR.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdLTR.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLTR.AutoEdit = True
        grdLTR_DesignTimeLayout.LayoutString = resources.GetString("grdLTR_DesignTimeLayout.LayoutString")
        Me.grdLTR.DesignTimeLayout = grdLTR_DesignTimeLayout
        Me.grdLTR.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdLTR.GroupByBoxVisible = False
        Me.grdLTR.Location = New System.Drawing.Point(3, 5)
        Me.grdLTR.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdLTR.Name = "grdLTR"
        Me.grdLTR.Size = New System.Drawing.Size(507, 756)
        Me.grdLTR.TabIndex = 22
        Me.grdLTR.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.grdObservationParameters)
        Me.SplitContainer2.Panel2.Controls.Add(Me.pnlObervation)
        Me.SplitContainer2.Size = New System.Drawing.Size(948, 789)
        Me.SplitContainer2.SplitterDistance = 498
        Me.SplitContainer2.SplitterWidth = 6
        Me.SplitContainer2.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Panel2)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblPOLCNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.cmbTicket)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblRemarks)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtRemarsk)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtPOLCNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblQtyBatchSize)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtQtyBatchSize)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpRefreshDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblRefresh)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpMfgDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblMfgDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtoExpDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblExpDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblBatchNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtBatchNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblGRN)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtGRNNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblSuppliers)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtSuppliers)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblStage)
        Me.SplitContainer3.Panel1.Controls.Add(Me.cmbStage)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblProduct)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtProductMatName)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblRequestNo)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.dtpTime)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblTime)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtRequestNo)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.SplitContainer3.Panel2.Controls.Add(Me.cmbBy)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer3.Panel2.Controls.Add(Me.dtpQCTime)
        Me.SplitContainer3.Panel2.Controls.Add(Me.dtpSamplecollectedon)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblSamplecollectedon)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblAt)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblContainerNo)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblBy)
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtContainerNo)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblQuantityOfSample)
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtQuantityOfSample)
        Me.SplitContainer3.Panel2.Controls.Add(Me.dtpObserDate)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblObservationDate)
        Me.SplitContainer3.Panel2.Controls.Add(Me.lblQCNumber)
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtQCNumber)
        Me.SplitContainer3.Size = New System.Drawing.Size(948, 498)
        Me.SplitContainer3.SplitterDistance = 296
        Me.SplitContainer3.SplitterWidth = 6
        Me.SplitContainer3.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(290, 123)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 20)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Ticket"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Panel2.Controls.Add(Me.lblLabTestingRequest)
        Me.Panel2.Location = New System.Drawing.Point(0, 5)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(944, 55)
        Me.Panel2.TabIndex = 0
        '
        'lblLabTestingRequest
        '
        Me.lblLabTestingRequest.AutoSize = True
        Me.lblLabTestingRequest.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLabTestingRequest.ForeColor = System.Drawing.Color.Black
        Me.lblLabTestingRequest.Location = New System.Drawing.Point(4, 15)
        Me.lblLabTestingRequest.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLabTestingRequest.Name = "lblLabTestingRequest"
        Me.lblLabTestingRequest.Size = New System.Drawing.Size(220, 26)
        Me.lblLabTestingRequest.TabIndex = 0
        Me.lblLabTestingRequest.Text = "Lab Testing Request"
        '
        'lblPOLCNo
        '
        Me.lblPOLCNo.AutoSize = True
        Me.lblPOLCNo.Location = New System.Drawing.Point(10, 208)
        Me.lblPOLCNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPOLCNo.Name = "lblPOLCNo"
        Me.lblPOLCNo.Size = New System.Drawing.Size(79, 20)
        Me.lblPOLCNo.TabIndex = 19
        Me.lblPOLCNo.Text = "PO/LC No"
        '
        'cmbTicket
        '
        Me.cmbTicket.Enabled = False
        Me.cmbTicket.Location = New System.Drawing.Point(368, 118)
        Me.cmbTicket.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbTicket.Name = "cmbTicket"
        Me.cmbTicket.Size = New System.Drawing.Size(188, 28)
        Me.cmbTicket.TabIndex = 5
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Location = New System.Drawing.Point(290, 248)
        Me.lblRemarks.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(73, 20)
        Me.lblRemarks.TabIndex = 27
        Me.lblRemarks.Text = "Remarks"
        '
        'txtRemarsk
        '
        Me.txtRemarsk.Location = New System.Drawing.Point(368, 238)
        Me.txtRemarsk.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRemarsk.Multiline = True
        Me.txtRemarsk.Name = "txtRemarsk"
        Me.txtRemarsk.Size = New System.Drawing.Size(188, 30)
        Me.txtRemarsk.TabIndex = 14
        '
        'txtPOLCNo
        '
        Me.txtPOLCNo.Location = New System.Drawing.Point(122, 198)
        Me.txtPOLCNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPOLCNo.Name = "txtPOLCNo"
        Me.txtPOLCNo.Size = New System.Drawing.Size(158, 26)
        Me.txtPOLCNo.TabIndex = 10
        '
        'lblQtyBatchSize
        '
        Me.lblQtyBatchSize.AutoSize = True
        Me.lblQtyBatchSize.Location = New System.Drawing.Point(564, 163)
        Me.lblQtyBatchSize.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblQtyBatchSize.Name = "lblQtyBatchSize"
        Me.lblQtyBatchSize.Size = New System.Drawing.Size(114, 20)
        Me.lblQtyBatchSize.TabIndex = 17
        Me.lblQtyBatchSize.Text = "Qty Batch Size"
        '
        'txtQtyBatchSize
        '
        Me.txtQtyBatchSize.Location = New System.Drawing.Point(687, 158)
        Me.txtQtyBatchSize.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtQtyBatchSize.Name = "txtQtyBatchSize"
        Me.txtQtyBatchSize.Size = New System.Drawing.Size(176, 26)
        Me.txtQtyBatchSize.TabIndex = 9
        '
        'dtpRefreshDate
        '
        Me.dtpRefreshDate.Checked = False
        Me.dtpRefreshDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpRefreshDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRefreshDate.Location = New System.Drawing.Point(122, 238)
        Me.dtpRefreshDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpRefreshDate.Name = "dtpRefreshDate"
        Me.dtpRefreshDate.ShowCheckBox = True
        Me.dtpRefreshDate.Size = New System.Drawing.Size(158, 26)
        Me.dtpRefreshDate.TabIndex = 13
        '
        'lblRefresh
        '
        Me.lblRefresh.AutoSize = True
        Me.lblRefresh.Location = New System.Drawing.Point(10, 248)
        Me.lblRefresh.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRefresh.Name = "lblRefresh"
        Me.lblRefresh.Size = New System.Drawing.Size(105, 20)
        Me.lblRefresh.TabIndex = 25
        Me.lblRefresh.Text = "Re-Test Date"
        '
        'dtpMfgDate
        '
        Me.dtpMfgDate.Checked = False
        Me.dtpMfgDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpMfgDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMfgDate.Location = New System.Drawing.Point(368, 197)
        Me.dtpMfgDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpMfgDate.Name = "dtpMfgDate"
        Me.dtpMfgDate.ShowCheckBox = True
        Me.dtpMfgDate.Size = New System.Drawing.Size(188, 26)
        Me.dtpMfgDate.TabIndex = 11
        '
        'lblMfgDate
        '
        Me.lblMfgDate.AutoSize = True
        Me.lblMfgDate.Location = New System.Drawing.Point(290, 202)
        Me.lblMfgDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMfgDate.Name = "lblMfgDate"
        Me.lblMfgDate.Size = New System.Drawing.Size(79, 20)
        Me.lblMfgDate.TabIndex = 21
        Me.lblMfgDate.Text = "Mfg. Date"
        '
        'dtoExpDate
        '
        Me.dtoExpDate.Checked = False
        Me.dtoExpDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtoExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtoExpDate.Location = New System.Drawing.Point(687, 198)
        Me.dtoExpDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtoExpDate.Name = "dtoExpDate"
        Me.dtoExpDate.ShowCheckBox = True
        Me.dtoExpDate.Size = New System.Drawing.Size(176, 26)
        Me.dtoExpDate.TabIndex = 12
        '
        'lblExpDate
        '
        Me.lblExpDate.AutoSize = True
        Me.lblExpDate.Location = New System.Drawing.Point(567, 203)
        Me.lblExpDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblExpDate.Name = "lblExpDate"
        Me.lblExpDate.Size = New System.Drawing.Size(79, 20)
        Me.lblExpDate.TabIndex = 23
        Me.lblExpDate.Text = "Exp. Date"
        '
        'lblBatchNo
        '
        Me.lblBatchNo.AutoSize = True
        Me.lblBatchNo.Location = New System.Drawing.Point(290, 162)
        Me.lblBatchNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBatchNo.Name = "lblBatchNo"
        Me.lblBatchNo.Size = New System.Drawing.Size(75, 20)
        Me.lblBatchNo.TabIndex = 15
        Me.lblBatchNo.Text = "Batch No"
        '
        'txtBatchNo
        '
        Me.txtBatchNo.Location = New System.Drawing.Point(368, 157)
        Me.txtBatchNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBatchNo.Name = "txtBatchNo"
        Me.txtBatchNo.Size = New System.Drawing.Size(188, 26)
        Me.txtBatchNo.TabIndex = 8
        '
        'lblGRN
        '
        Me.lblGRN.AutoSize = True
        Me.lblGRN.Location = New System.Drawing.Point(10, 163)
        Me.lblGRN.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblGRN.Name = "lblGRN"
        Me.lblGRN.Size = New System.Drawing.Size(69, 20)
        Me.lblGRN.TabIndex = 13
        Me.lblGRN.Text = "GRN No"
        '
        'txtGRNNo
        '
        Me.txtGRNNo.Location = New System.Drawing.Point(122, 158)
        Me.txtGRNNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtGRNNo.Name = "txtGRNNo"
        Me.txtGRNNo.Size = New System.Drawing.Size(158, 26)
        Me.txtGRNNo.TabIndex = 7
        '
        'lblSuppliers
        '
        Me.lblSuppliers.AutoSize = True
        Me.lblSuppliers.Location = New System.Drawing.Point(564, 246)
        Me.lblSuppliers.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSuppliers.Name = "lblSuppliers"
        Me.lblSuppliers.Size = New System.Drawing.Size(75, 20)
        Me.lblSuppliers.TabIndex = 11
        Me.lblSuppliers.Text = "Suppliers"
        '
        'txtSuppliers
        '
        Me.txtSuppliers.Location = New System.Drawing.Point(687, 240)
        Me.txtSuppliers.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSuppliers.Name = "txtSuppliers"
        Me.txtSuppliers.Size = New System.Drawing.Size(176, 26)
        Me.txtSuppliers.TabIndex = 15
        '
        'lblStage
        '
        Me.lblStage.AutoSize = True
        Me.lblStage.Location = New System.Drawing.Point(570, 123)
        Me.lblStage.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStage.Name = "lblStage"
        Me.lblStage.Size = New System.Drawing.Size(52, 20)
        Me.lblStage.TabIndex = 9
        Me.lblStage.Text = "Stage"
        '
        'cmbStage
        '
        Me.cmbStage.Enabled = False
        Me.cmbStage.Location = New System.Drawing.Point(687, 118)
        Me.cmbStage.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbStage.Name = "cmbStage"
        Me.cmbStage.Size = New System.Drawing.Size(180, 28)
        Me.cmbStage.TabIndex = 6
        '
        'lblProduct
        '
        Me.lblProduct.AutoSize = True
        Me.lblProduct.Location = New System.Drawing.Point(9, 123)
        Me.lblProduct.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProduct.Name = "lblProduct"
        Me.lblProduct.Size = New System.Drawing.Size(110, 20)
        Me.lblProduct.TabIndex = 7
        Me.lblProduct.Text = "Product Name"
        '
        'txtProductMatName
        '
        Me.txtProductMatName.Location = New System.Drawing.Point(122, 118)
        Me.txtProductMatName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtProductMatName.Name = "txtProductMatName"
        Me.txtProductMatName.Size = New System.Drawing.Size(158, 26)
        Me.txtProductMatName.TabIndex = 4
        '
        'lblRequestNo
        '
        Me.lblRequestNo.AutoSize = True
        Me.lblRequestNo.Location = New System.Drawing.Point(10, 83)
        Me.lblRequestNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRequestNo.Name = "lblRequestNo"
        Me.lblRequestNo.Size = New System.Drawing.Size(94, 20)
        Me.lblRequestNo.TabIndex = 1
        Me.lblRequestNo.Text = "Request No"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(368, 78)
        Me.dtpDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(188, 26)
        Me.dtpDate.TabIndex = 2
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(290, 83)
        Me.lblDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(44, 20)
        Me.lblDate.TabIndex = 3
        Me.lblDate.Text = "Date"
        '
        'dtpTime
        '
        Me.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpTime.Location = New System.Drawing.Point(687, 78)
        Me.dtpTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTime.Name = "dtpTime"
        Me.dtpTime.Size = New System.Drawing.Size(176, 26)
        Me.dtpTime.TabIndex = 3
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Location = New System.Drawing.Point(564, 83)
        Me.lblTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(43, 20)
        Me.lblTime.TabIndex = 5
        Me.lblTime.Text = "Time"
        '
        'txtRequestNo
        '
        Me.txtRequestNo.Location = New System.Drawing.Point(122, 78)
        Me.txtRequestNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRequestNo.Name = "txtRequestNo"
        Me.txtRequestNo.Size = New System.Drawing.Size(158, 26)
        Me.txtRequestNo.TabIndex = 1
        '
        'cmbBy
        '
        Me.cmbBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbBy.Location = New System.Drawing.Point(662, 105)
        Me.cmbBy.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbBy.Name = "cmbBy"
        Me.cmbBy.Size = New System.Drawing.Size(211, 28)
        Me.cmbBy.TabIndex = 20
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Panel1.Controls.Add(Me.lblForQualityControlDepartment)
        Me.Panel1.Location = New System.Drawing.Point(4, 5)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(939, 49)
        Me.Panel1.TabIndex = 0
        '
        'lblForQualityControlDepartment
        '
        Me.lblForQualityControlDepartment.AutoSize = True
        Me.lblForQualityControlDepartment.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblForQualityControlDepartment.ForeColor = System.Drawing.Color.Black
        Me.lblForQualityControlDepartment.Location = New System.Drawing.Point(4, 15)
        Me.lblForQualityControlDepartment.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblForQualityControlDepartment.Name = "lblForQualityControlDepartment"
        Me.lblForQualityControlDepartment.Size = New System.Drawing.Size(449, 26)
        Me.lblForQualityControlDepartment.TabIndex = 0
        Me.lblForQualityControlDepartment.Text = "For Quality Control Department Use Only"
        '
        'dtpQCTime
        '
        Me.dtpQCTime.CustomFormat = ""
        Me.dtpQCTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpQCTime.Location = New System.Drawing.Point(405, 106)
        Me.dtpQCTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpQCTime.Name = "dtpQCTime"
        Me.dtpQCTime.Size = New System.Drawing.Size(211, 26)
        Me.dtpQCTime.TabIndex = 19
        '
        'dtpSamplecollectedon
        '
        Me.dtpSamplecollectedon.CustomFormat = "dd/MMM/yyyy"
        Me.dtpSamplecollectedon.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSamplecollectedon.Location = New System.Drawing.Point(168, 106)
        Me.dtpSamplecollectedon.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpSamplecollectedon.Name = "dtpSamplecollectedon"
        Me.dtpSamplecollectedon.Size = New System.Drawing.Size(193, 26)
        Me.dtpSamplecollectedon.TabIndex = 18
        '
        'lblSamplecollectedon
        '
        Me.lblSamplecollectedon.AutoSize = True
        Me.lblSamplecollectedon.Location = New System.Drawing.Point(4, 111)
        Me.lblSamplecollectedon.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSamplecollectedon.Name = "lblSamplecollectedon"
        Me.lblSamplecollectedon.Size = New System.Drawing.Size(152, 20)
        Me.lblSamplecollectedon.TabIndex = 5
        Me.lblSamplecollectedon.Text = "Sample collected on"
        '
        'lblAt
        '
        Me.lblAt.AutoSize = True
        Me.lblAt.Location = New System.Drawing.Point(369, 111)
        Me.lblAt.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAt.Name = "lblAt"
        Me.lblAt.Size = New System.Drawing.Size(25, 20)
        Me.lblAt.TabIndex = 7
        Me.lblAt.Text = "At"
        '
        'lblContainerNo
        '
        Me.lblContainerNo.AutoSize = True
        Me.lblContainerNo.Location = New System.Drawing.Point(546, 154)
        Me.lblContainerNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblContainerNo.Name = "lblContainerNo"
        Me.lblContainerNo.Size = New System.Drawing.Size(102, 20)
        Me.lblContainerNo.TabIndex = 13
        Me.lblContainerNo.Text = "Container No"
        '
        'lblBy
        '
        Me.lblBy.AutoSize = True
        Me.lblBy.Location = New System.Drawing.Point(630, 111)
        Me.lblBy.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBy.Name = "lblBy"
        Me.lblBy.Size = New System.Drawing.Size(27, 20)
        Me.lblBy.TabIndex = 9
        Me.lblBy.Text = "By"
        '
        'txtContainerNo
        '
        Me.txtContainerNo.Location = New System.Drawing.Point(662, 149)
        Me.txtContainerNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtContainerNo.Name = "txtContainerNo"
        Me.txtContainerNo.Size = New System.Drawing.Size(211, 26)
        Me.txtContainerNo.TabIndex = 22
        '
        'lblQuantityOfSample
        '
        Me.lblQuantityOfSample.AutoSize = True
        Me.lblQuantityOfSample.Location = New System.Drawing.Point(10, 154)
        Me.lblQuantityOfSample.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblQuantityOfSample.Name = "lblQuantityOfSample"
        Me.lblQuantityOfSample.Size = New System.Drawing.Size(147, 20)
        Me.lblQuantityOfSample.TabIndex = 11
        Me.lblQuantityOfSample.Text = "Quantity Of Sample"
        '
        'txtQuantityOfSample
        '
        Me.txtQuantityOfSample.Location = New System.Drawing.Point(168, 148)
        Me.txtQuantityOfSample.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtQuantityOfSample.Name = "txtQuantityOfSample"
        Me.txtQuantityOfSample.Size = New System.Drawing.Size(193, 26)
        Me.txtQuantityOfSample.TabIndex = 21
        Me.txtQuantityOfSample.TabStop = False
        '
        'dtpObserDate
        '
        Me.dtpObserDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpObserDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpObserDate.Location = New System.Drawing.Point(662, 63)
        Me.dtpObserDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpObserDate.Name = "dtpObserDate"
        Me.dtpObserDate.Size = New System.Drawing.Size(211, 26)
        Me.dtpObserDate.TabIndex = 17
        '
        'lblObservationDate
        '
        Me.lblObservationDate.AutoSize = True
        Me.lblObservationDate.Location = New System.Drawing.Point(556, 68)
        Me.lblObservationDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblObservationDate.Name = "lblObservationDate"
        Me.lblObservationDate.Size = New System.Drawing.Size(89, 20)
        Me.lblObservationDate.TabIndex = 3
        Me.lblObservationDate.Text = "Obsrv Date"
        '
        'lblQCNumber
        '
        Me.lblQCNumber.AutoSize = True
        Me.lblQCNumber.Location = New System.Drawing.Point(10, 68)
        Me.lblQCNumber.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblQCNumber.Name = "lblQCNumber"
        Me.lblQCNumber.Size = New System.Drawing.Size(92, 20)
        Me.lblQCNumber.TabIndex = 1
        Me.lblQCNumber.Text = "QC Number"
        '
        'txtQCNumber
        '
        Me.txtQCNumber.Location = New System.Drawing.Point(168, 63)
        Me.txtQCNumber.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtQCNumber.Name = "txtQCNumber"
        Me.txtQCNumber.Size = New System.Drawing.Size(193, 26)
        Me.txtQCNumber.TabIndex = 16
        '
        'grdObservationParameters
        '
        Me.grdObservationParameters.AutoEdit = True
        Me.grdObservationParameters.ColumnAutoResize = True
        grdObservationParameters_DesignTimeLayout.LayoutString = resources.GetString("grdObservationParameters_DesignTimeLayout.LayoutString")
        Me.grdObservationParameters.DesignTimeLayout = grdObservationParameters_DesignTimeLayout
        Me.grdObservationParameters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdObservationParameters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdObservationParameters.GroupByBoxVisible = False
        Me.grdObservationParameters.Location = New System.Drawing.Point(0, 0)
        Me.grdObservationParameters.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdObservationParameters.Name = "grdObservationParameters"
        Me.grdObservationParameters.Size = New System.Drawing.Size(948, 285)
        Me.grdObservationParameters.TabIndex = 0
        Me.grdObservationParameters.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'pnlObervation
        '
        Me.pnlObervation.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pnlObervation.Controls.Add(Me.lblObservation)
        Me.pnlObervation.Location = New System.Drawing.Point(4, 5)
        Me.pnlObervation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlObervation.Name = "pnlObervation"
        Me.pnlObervation.Size = New System.Drawing.Size(939, 42)
        Me.pnlObervation.TabIndex = 0
        '
        'lblObservation
        '
        Me.lblObservation.AutoSize = True
        Me.lblObservation.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblObservation.ForeColor = System.Drawing.Color.White
        Me.lblObservation.Location = New System.Drawing.Point(4, 5)
        Me.lblObservation.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblObservation.Name = "lblObservation"
        Me.lblObservation.Size = New System.Drawing.Size(294, 25)
        Me.lblObservation.TabIndex = 0
        Me.lblObservation.Text = "Observation During Sampling"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(2, 2)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1547, 765)
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSaved.ColumnAutoResize = True
        grdSaved_DesignTimeLayout.LayoutString = resources.GetString("grdSaved_DesignTimeLayout.LayoutString")
        Me.grdSaved.DesignTimeLayout = grdSaved_DesignTimeLayout
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(-2, -2)
        Me.grdSaved.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.Size = New System.Drawing.Size(1431, 805)
        Me.grdSaved.TabIndex = 23
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1547, 765)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.btnRefresh, Me.btnLabTestRequest, Me.btnHelp, Me.ToolStripSeparator1, Me.btnFormPrint, Me.btnPrintSticker, Me.ToolStripSeparator2, Me.btnNotification})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1551, 38)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 35)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(70, 35)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 35)
        Me.btnSave.Text = "&Save"
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 35)
        Me.btnDelete.Text = "&Delete"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 35)
        Me.btnRefresh.Text = "&Refresh"
        '
        'btnLabTestRequest
        '
        Me.btnLabTestRequest.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnLabTestRequest.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLabTestRequest.Name = "btnLabTestRequest"
        Me.btnLabTestRequest.Size = New System.Drawing.Size(171, 35)
        Me.btnLabTestRequest.Text = "Lab Test Request"
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(77, 35)
        Me.btnHelp.Text = "&Help"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 38)
        '
        'btnFormPrint
        '
        Me.btnFormPrint.Image = CType(resources.GetObject("btnFormPrint.Image"), System.Drawing.Image)
        Me.btnFormPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnFormPrint.Name = "btnFormPrint"
        Me.btnFormPrint.Size = New System.Drawing.Size(123, 35)
        Me.btnFormPrint.Text = "Print Form"
        '
        'btnPrintSticker
        '
        Me.btnPrintSticker.Image = CType(resources.GetObject("btnPrintSticker.Image"), System.Drawing.Image)
        Me.btnPrintSticker.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrintSticker.Name = "btnPrintSticker"
        Me.btnPrintSticker.Size = New System.Drawing.Size(155, 35)
        Me.btnPrintSticker.Text = "Sample Sticker"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 38)
        '
        'btnNotification
        '
        Me.btnNotification.Image = CType(resources.GetObject("btnNotification.Image"), System.Drawing.Image)
        Me.btnNotification.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNotification.Name = "btnNotification"
        Me.btnNotification.Size = New System.Drawing.Size(200, 35)
        Me.btnNotification.Text = "Request Notification"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 114)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage2
        Me.UltraTabControl1.Size = New System.Drawing.Size(1551, 794)
        Me.UltraTabControl1.TabIndex = 39
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Sampling"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        UltraTab3.TabPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2, UltraTab3})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage2
        '
        Me.UltraTabSharedControlsPage2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage2.Name = "UltraTabSharedControlsPage2"
        Me.UltraTabSharedControlsPage2.Size = New System.Drawing.Size(1547, 765)
        '
        'BackgroundWorker1
        '
        '
        'Timer1
        '
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1500, 2)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grdSaved
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(50, 38)
        Me.CtrlGrdBar1.TabIndex = 40
        Me.CtrlGrdBar1.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Panel3.Controls.Add(Me.lblSample)
        Me.Panel3.Location = New System.Drawing.Point(-2, 43)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1552, 72)
        Me.Panel3.TabIndex = 41
        '
        'lblSample
        '
        Me.lblSample.AutoSize = True
        Me.lblSample.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSample.ForeColor = System.Drawing.Color.Black
        Me.lblSample.Location = New System.Drawing.Point(4, 12)
        Me.lblSample.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSample.Name = "lblSample"
        Me.lblSample.Size = New System.Drawing.Size(326, 41)
        Me.lblSample.TabIndex = 1
        Me.lblSample.Text = "Sample Observation"
        '
        'frmObservationSample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1551, 914)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmObservationSample"
        Me.Text = "Observation During Sampling"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.grdLTR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.grdObservationParameters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlObervation.ResumeLayout(False)
        Me.pnlObervation.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnFormPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrintSticker As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents grdLTR As Janus.Windows.GridEX.GridEX
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnNotification As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents grdObservationParameters As Janus.Windows.GridEX.GridEX
    Friend WithEvents VisualStyleManager1 As Janus.Windows.Common.VisualStyleManager
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblForQualityControlDepartment As System.Windows.Forms.Label
    Friend WithEvents lblPOLCNo As System.Windows.Forms.Label
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents txtRemarsk As System.Windows.Forms.TextBox
    Friend WithEvents txtPOLCNo As System.Windows.Forms.TextBox
    Friend WithEvents lblQtyBatchSize As System.Windows.Forms.Label
    Friend WithEvents txtQtyBatchSize As System.Windows.Forms.TextBox
    Friend WithEvents dtpRefreshDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblRefresh As System.Windows.Forms.Label
    Friend WithEvents dtpMfgDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblMfgDate As System.Windows.Forms.Label
    Friend WithEvents dtoExpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblExpDate As System.Windows.Forms.Label
    Friend WithEvents lblBatchNo As System.Windows.Forms.Label
    Friend WithEvents txtBatchNo As System.Windows.Forms.TextBox
    Friend WithEvents lblGRN As System.Windows.Forms.Label
    Friend WithEvents txtGRNNo As System.Windows.Forms.TextBox
    Friend WithEvents lblSuppliers As System.Windows.Forms.Label
    Friend WithEvents txtSuppliers As System.Windows.Forms.TextBox
    Friend WithEvents lblStage As System.Windows.Forms.Label
    Friend WithEvents cmbStage As System.Windows.Forms.ComboBox
    Friend WithEvents lblProduct As System.Windows.Forms.Label
    Friend WithEvents txtProductMatName As System.Windows.Forms.TextBox
    Friend WithEvents lblRequestNo As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents dtpTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents txtRequestNo As System.Windows.Forms.TextBox
    Friend WithEvents dtpQCTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpSamplecollectedon As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblSamplecollectedon As System.Windows.Forms.Label
    Friend WithEvents lblAt As System.Windows.Forms.Label
    Friend WithEvents lblContainerNo As System.Windows.Forms.Label
    Friend WithEvents lblBy As System.Windows.Forms.Label
    Friend WithEvents txtContainerNo As System.Windows.Forms.TextBox
    Friend WithEvents lblQuantityOfSample As System.Windows.Forms.Label
    Friend WithEvents txtQuantityOfSample As System.Windows.Forms.TextBox
    Friend WithEvents pnlObervation As System.Windows.Forms.Panel
    Friend WithEvents lblObservation As System.Windows.Forms.Label
    Friend WithEvents dtpObserDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblObservationDate As System.Windows.Forms.Label
    Friend WithEvents lblQCNumber As System.Windows.Forms.Label
    Friend WithEvents txtQCNumber As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblLabTestingRequest As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnLabTestRequest As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblSample As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbTicket As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBy As System.Windows.Forms.ComboBox
End Class
