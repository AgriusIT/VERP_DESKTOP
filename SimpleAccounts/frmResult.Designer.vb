<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResult
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
        Dim grdProductParameters_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResult))
        Dim grdSaved_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdProductParameters = New Janus.Windows.GridEX.GridEX()
        Me.GrpResult = New System.Windows.Forms.GroupBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtResultRemarks = New System.Windows.Forms.TextBox()
        Me.dtpSampledDate = New System.Windows.Forms.DateTimePicker()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtQCSampled = New System.Windows.Forms.TextBox()
        Me.dtpExpDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpMfgDate = New System.Windows.Forms.DateTimePicker()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtNoofContainer = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtContainerType = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbItemName = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbGRNNo = New System.Windows.Forms.ComboBox()
        Me.btnParametersMapping = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtAnalyticalMethod = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtProSpecNo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtDRNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPackSize = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtResultNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBatchSize = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbQCNo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbSupplierName = New System.Windows.Forms.ComboBox()
        Me.txtBatchNo = New System.Windows.Forms.TextBox()
        Me.dtpResultDate = New System.Windows.Forms.DateTimePicker()
        Me.pnlResultEntry = New System.Windows.Forms.Panel()
        Me.lblObservationSample = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPrintForm = New System.Windows.Forms.ToolStripButton()
        Me.btnPrintSticker = New System.Windows.Forms.ToolStripSplitButton()
        Me.SampleStrickerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReleasedStickerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.grdProductParameters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpResult.SuspendLayout()
        Me.pnlResultEntry.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.grdProductParameters)
        Me.UltraTabPageControl1.Controls.Add(Me.GrpResult)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlResultEntry)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(871, 567)
        '
        'grdProductParameters
        '
        Me.grdProductParameters.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdProductParameters.AutoEdit = True
        Me.grdProductParameters.ColumnAutoResize = True
        grdProductParameters_DesignTimeLayout.LayoutString = resources.GetString("grdProductParameters_DesignTimeLayout.LayoutString")
        Me.grdProductParameters.DesignTimeLayout = grdProductParameters_DesignTimeLayout
        Me.grdProductParameters.Location = New System.Drawing.Point(3, 421)
        Me.grdProductParameters.Name = "grdProductParameters"
        Me.grdProductParameters.Size = New System.Drawing.Size(865, 146)
        Me.grdProductParameters.TabIndex = 28
        Me.grdProductParameters.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GrpResult
        '
        Me.GrpResult.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrpResult.Controls.Add(Me.Label19)
        Me.GrpResult.Controls.Add(Me.txtResultRemarks)
        Me.GrpResult.Controls.Add(Me.dtpSampledDate)
        Me.GrpResult.Controls.Add(Me.Label18)
        Me.GrpResult.Controls.Add(Me.Label17)
        Me.GrpResult.Controls.Add(Me.txtQCSampled)
        Me.GrpResult.Controls.Add(Me.dtpExpDate)
        Me.GrpResult.Controls.Add(Me.dtpMfgDate)
        Me.GrpResult.Controls.Add(Me.Label16)
        Me.GrpResult.Controls.Add(Me.txtNoofContainer)
        Me.GrpResult.Controls.Add(Me.Label15)
        Me.GrpResult.Controls.Add(Me.txtContainerType)
        Me.GrpResult.Controls.Add(Me.Label14)
        Me.GrpResult.Controls.Add(Me.cmbItemName)
        Me.GrpResult.Controls.Add(Me.Label13)
        Me.GrpResult.Controls.Add(Me.cmbGRNNo)
        Me.GrpResult.Controls.Add(Me.btnParametersMapping)
        Me.GrpResult.Controls.Add(Me.Label12)
        Me.GrpResult.Controls.Add(Me.txtAnalyticalMethod)
        Me.GrpResult.Controls.Add(Me.Label11)
        Me.GrpResult.Controls.Add(Me.txtProSpecNo)
        Me.GrpResult.Controls.Add(Me.Label10)
        Me.GrpResult.Controls.Add(Me.txtDRNo)
        Me.GrpResult.Controls.Add(Me.Label9)
        Me.GrpResult.Controls.Add(Me.txtPackSize)
        Me.GrpResult.Controls.Add(Me.Label8)
        Me.GrpResult.Controls.Add(Me.Label7)
        Me.GrpResult.Controls.Add(Me.Label6)
        Me.GrpResult.Controls.Add(Me.txtResultNo)
        Me.GrpResult.Controls.Add(Me.Label5)
        Me.GrpResult.Controls.Add(Me.txtBatchSize)
        Me.GrpResult.Controls.Add(Me.Label4)
        Me.GrpResult.Controls.Add(Me.Label3)
        Me.GrpResult.Controls.Add(Me.cmbQCNo)
        Me.GrpResult.Controls.Add(Me.Label2)
        Me.GrpResult.Controls.Add(Me.Label1)
        Me.GrpResult.Controls.Add(Me.cmbSupplierName)
        Me.GrpResult.Controls.Add(Me.txtBatchNo)
        Me.GrpResult.Controls.Add(Me.dtpResultDate)
        Me.GrpResult.Location = New System.Drawing.Point(3, 53)
        Me.GrpResult.Name = "GrpResult"
        Me.GrpResult.Size = New System.Drawing.Size(865, 362)
        Me.GrpResult.TabIndex = 27
        Me.GrpResult.TabStop = False
        Me.GrpResult.Text = "Product Description"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(8, 338)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(49, 13)
        Me.Label19.TabIndex = 64
        Me.Label19.Text = "Remarks"
        '
        'txtResultRemarks
        '
        Me.txtResultRemarks.Location = New System.Drawing.Point(124, 335)
        Me.txtResultRemarks.Name = "txtResultRemarks"
        Me.txtResultRemarks.Size = New System.Drawing.Size(485, 20)
        Me.txtResultRemarks.TabIndex = 63
        '
        'dtpSampledDate
        '
        Me.dtpSampledDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpSampledDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSampledDate.Location = New System.Drawing.Point(410, 253)
        Me.dtpSampledDate.Name = "dtpSampledDate"
        Me.dtpSampledDate.Size = New System.Drawing.Size(199, 20)
        Me.dtpSampledDate.TabIndex = 62
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(329, 259)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(74, 13)
        Me.Label18.TabIndex = 61
        Me.Label18.Text = "Date Sampled"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(8, 260)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(99, 13)
        Me.Label17.TabIndex = 60
        Me.Label17.Text = "Qty Sampled by QC"
        '
        'txtQCSampled
        '
        Me.txtQCSampled.Location = New System.Drawing.Point(124, 257)
        Me.txtQCSampled.Name = "txtQCSampled"
        Me.txtQCSampled.Size = New System.Drawing.Size(199, 20)
        Me.txtQCSampled.TabIndex = 59
        '
        'dtpExpDate
        '
        Me.dtpExpDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpDate.Location = New System.Drawing.Point(410, 306)
        Me.dtpExpDate.Name = "dtpExpDate"
        Me.dtpExpDate.Size = New System.Drawing.Size(199, 20)
        Me.dtpExpDate.TabIndex = 58
        '
        'dtpMfgDate
        '
        Me.dtpMfgDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpMfgDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMfgDate.Location = New System.Drawing.Point(410, 280)
        Me.dtpMfgDate.Name = "dtpMfgDate"
        Me.dtpMfgDate.Size = New System.Drawing.Size(199, 20)
        Me.dtpMfgDate.TabIndex = 57
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(329, 182)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(81, 13)
        Me.Label16.TabIndex = 56
        Me.Label16.Text = "No of Container"
        '
        'txtNoofContainer
        '
        Me.txtNoofContainer.Location = New System.Drawing.Point(410, 179)
        Me.txtNoofContainer.Name = "txtNoofContainer"
        Me.txtNoofContainer.Size = New System.Drawing.Size(199, 20)
        Me.txtNoofContainer.TabIndex = 55
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(8, 182)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(91, 13)
        Me.Label15.TabIndex = 54
        Me.Label15.Text = "Type of Container"
        '
        'txtContainerType
        '
        Me.txtContainerType.Location = New System.Drawing.Point(124, 179)
        Me.txtContainerType.Name = "txtContainerType"
        Me.txtContainerType.Size = New System.Drawing.Size(199, 20)
        Me.txtContainerType.TabIndex = 53
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(7, 101)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(51, 13)
        Me.Label14.TabIndex = 52
        Me.Label14.Text = "GRN No."
        '
        'cmbItemName
        '
        Me.cmbItemName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItemName.FormattingEnabled = True
        Me.cmbItemName.Location = New System.Drawing.Point(124, 152)
        Me.cmbItemName.Name = "cmbItemName"
        Me.cmbItemName.Size = New System.Drawing.Size(199, 21)
        Me.cmbItemName.TabIndex = 51
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(8, 74)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(45, 13)
        Me.Label13.TabIndex = 50
        Me.Label13.Text = "Supplier"
        '
        'cmbGRNNo
        '
        Me.cmbGRNNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGRNNo.FormattingEnabled = True
        Me.cmbGRNNo.Location = New System.Drawing.Point(124, 98)
        Me.cmbGRNNo.Name = "cmbGRNNo"
        Me.cmbGRNNo.Size = New System.Drawing.Size(199, 21)
        Me.cmbGRNNo.TabIndex = 49
        '
        'btnParametersMapping
        '
        Me.btnParametersMapping.Location = New System.Drawing.Point(615, 309)
        Me.btnParametersMapping.Name = "btnParametersMapping"
        Me.btnParametersMapping.Size = New System.Drawing.Size(199, 46)
        Me.btnParametersMapping.TabIndex = 48
        Me.btnParametersMapping.Text = "Product Parameters Mapping"
        Me.btnParametersMapping.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(8, 315)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(101, 13)
        Me.Label12.TabIndex = 47
        Me.Label12.Text = "Analytical Method #"
        '
        'txtAnalyticalMethod
        '
        Me.txtAnalyticalMethod.Location = New System.Drawing.Point(124, 309)
        Me.txtAnalyticalMethod.Name = "txtAnalyticalMethod"
        Me.txtAnalyticalMethod.Size = New System.Drawing.Size(199, 20)
        Me.txtAnalyticalMethod.TabIndex = 46
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(8, 286)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(118, 13)
        Me.Label11.TabIndex = 45
        Me.Label11.Text = "Product Specification #"
        '
        'txtProSpecNo
        '
        Me.txtProSpecNo.Location = New System.Drawing.Point(124, 283)
        Me.txtProSpecNo.Name = "txtProSpecNo"
        Me.txtProSpecNo.Size = New System.Drawing.Size(199, 20)
        Me.txtProSpecNo.TabIndex = 44
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 234)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(43, 13)
        Me.Label10.TabIndex = 43
        Me.Label10.Text = "DR No."
        '
        'txtDRNo
        '
        Me.txtDRNo.Location = New System.Drawing.Point(124, 231)
        Me.txtDRNo.Name = "txtDRNo"
        Me.txtDRNo.Size = New System.Drawing.Size(199, 20)
        Me.txtDRNo.TabIndex = 42
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(329, 234)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(55, 13)
        Me.Label9.TabIndex = 41
        Me.Label9.Text = "Pack Size"
        '
        'txtPackSize
        '
        Me.txtPackSize.Location = New System.Drawing.Point(410, 231)
        Me.txtPackSize.Name = "txtPackSize"
        Me.txtPackSize.Size = New System.Drawing.Size(199, 20)
        Me.txtPackSize.TabIndex = 40
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(329, 309)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(60, 13)
        Me.Label8.TabIndex = 39
        Me.Label8.Text = "Exp. Date: "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(329, 286)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "Mfg. Date: "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "Result No."
        '
        'txtResultNo
        '
        Me.txtResultNo.Location = New System.Drawing.Point(124, 45)
        Me.txtResultNo.Name = "txtResultNo"
        Me.txtResultNo.Size = New System.Drawing.Size(120, 20)
        Me.txtResultNo.TabIndex = 34
        Me.txtResultNo.Text = "RE-1606-00001"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(329, 208)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Batch Size"
        '
        'txtBatchSize
        '
        Me.txtBatchSize.Location = New System.Drawing.Point(410, 205)
        Me.txtBatchSize.Name = "txtBatchSize"
        Me.txtBatchSize.Size = New System.Drawing.Size(199, 20)
        Me.txtBatchSize.TabIndex = 32
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 208)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Batch No."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 155)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Product Name"
        '
        'cmbQCNo
        '
        Me.cmbQCNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbQCNo.FormattingEnabled = True
        Me.cmbQCNo.Location = New System.Drawing.Point(124, 125)
        Me.cmbQCNo.Name = "cmbQCNo"
        Me.cmbQCNo.Size = New System.Drawing.Size(199, 21)
        Me.cmbQCNo.TabIndex = 29
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 128)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "QC No."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Result Date: "
        '
        'cmbSupplierName
        '
        Me.cmbSupplierName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSupplierName.FormattingEnabled = True
        Me.cmbSupplierName.Location = New System.Drawing.Point(124, 71)
        Me.cmbSupplierName.Name = "cmbSupplierName"
        Me.cmbSupplierName.Size = New System.Drawing.Size(199, 21)
        Me.cmbSupplierName.TabIndex = 25
        '
        'txtBatchNo
        '
        Me.txtBatchNo.Location = New System.Drawing.Point(124, 205)
        Me.txtBatchNo.Name = "txtBatchNo"
        Me.txtBatchNo.Size = New System.Drawing.Size(199, 20)
        Me.txtBatchNo.TabIndex = 22
        '
        'dtpResultDate
        '
        Me.dtpResultDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpResultDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpResultDate.Location = New System.Drawing.Point(124, 19)
        Me.dtpResultDate.MaximumSize = New System.Drawing.Size(120, 20)
        Me.dtpResultDate.Name = "dtpResultDate"
        Me.dtpResultDate.Size = New System.Drawing.Size(120, 20)
        Me.dtpResultDate.TabIndex = 26
        '
        'pnlResultEntry
        '
        Me.pnlResultEntry.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlResultEntry.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlResultEntry.Controls.Add(Me.lblObservationSample)
        Me.pnlResultEntry.Location = New System.Drawing.Point(0, 0)
        Me.pnlResultEntry.Name = "pnlResultEntry"
        Me.pnlResultEntry.Size = New System.Drawing.Size(872, 47)
        Me.pnlResultEntry.TabIndex = 20
        '
        'lblObservationSample
        '
        Me.lblObservationSample.AutoSize = True
        Me.lblObservationSample.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblObservationSample.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblObservationSample.Location = New System.Drawing.Point(3, 9)
        Me.lblObservationSample.Name = "lblObservationSample"
        Me.lblObservationSample.Size = New System.Drawing.Size(143, 23)
        Me.lblObservationSample.TabIndex = 4
        Me.lblObservationSample.Text = "Result Entry"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(871, 567)
        '
        'grdSaved
        '
        Me.grdSaved.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSaved.AutoEdit = True
        Me.grdSaved.ColumnAutoResize = True
        grdSaved_DesignTimeLayout.LayoutString = resources.GetString("grdSaved_DesignTimeLayout.LayoutString")
        Me.grdSaved.DesignTimeLayout = grdSaved_DesignTimeLayout
        Me.grdSaved.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdSaved.Location = New System.Drawing.Point(-1, 0)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.Size = New System.Drawing.Size(873, 567)
        Me.grdSaved.TabIndex = 29
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.btnRefresh, Me.btnHelp, Me.ToolStripSeparator1, Me.btnPrintForm, Me.btnPrintSticker})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(873, 25)
        Me.ToolStrip1.TabIndex = 21
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(47, 22)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "&Refresh"
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(52, 22)
        Me.btnHelp.Text = "&Help"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnPrintForm
        '
        Me.btnPrintForm.Image = CType(resources.GetObject("btnPrintForm.Image"), System.Drawing.Image)
        Me.btnPrintForm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrintForm.Name = "btnPrintForm"
        Me.btnPrintForm.Size = New System.Drawing.Size(83, 22)
        Me.btnPrintForm.Text = "Print Form"
        '
        'btnPrintSticker
        '
        Me.btnPrintSticker.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SampleStrickerToolStripMenuItem, Me.ReleasedStickerToolStripMenuItem})
        Me.btnPrintSticker.Image = CType(resources.GetObject("btnPrintSticker.Image"), System.Drawing.Image)
        Me.btnPrintSticker.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrintSticker.Name = "btnPrintSticker"
        Me.btnPrintSticker.Size = New System.Drawing.Size(102, 22)
        Me.btnPrintSticker.Text = "Print Sticker"
        '
        'SampleStrickerToolStripMenuItem
        '
        Me.SampleStrickerToolStripMenuItem.Name = "SampleStrickerToolStripMenuItem"
        Me.SampleStrickerToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.SampleStrickerToolStripMenuItem.Text = "Sample Sticker"
        '
        'ReleasedStickerToolStripMenuItem
        '
        Me.ReleasedStickerToolStripMenuItem.Name = "ReleasedStickerToolStripMenuItem"
        Me.ReleasedStickerToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.ReleasedStickerToolStripMenuItem.Text = "Released Sticker"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 28)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(873, 590)
        Me.UltraTabControl1.TabIndex = 28
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Result"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(871, 567)
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "&Delete"
        '
        'frmResult
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(873, 617)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmResult"
        Me.Text = "Result Entry"
        Me.UltraTabPageControl1.ResumeLayout(False)
        CType(Me.grdProductParameters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpResult.ResumeLayout(False)
        Me.GrpResult.PerformLayout()
        Me.pnlResultEntry.ResumeLayout(False)
        Me.pnlResultEntry.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlResultEntry As System.Windows.Forms.Panel
    Friend WithEvents lblObservationSample As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPrintForm As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPrintSticker As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents SampleStrickerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReleasedStickerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtBatchNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbSupplierName As System.Windows.Forms.ComboBox
    Friend WithEvents dtpResultDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents GrpResult As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtAnalyticalMethod As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtProSpecNo As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtDRNo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtPackSize As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtResultNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtBatchSize As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbQCNo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents btnParametersMapping As System.Windows.Forms.Button
    Friend WithEvents grdProductParameters As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtResultRemarks As System.Windows.Forms.TextBox
    Friend WithEvents dtpSampledDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtQCSampled As System.Windows.Forms.TextBox
    Friend WithEvents dtpExpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpMfgDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtNoofContainer As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtContainerType As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbItemName As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbGRNNo As System.Windows.Forms.ComboBox
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
End Class
