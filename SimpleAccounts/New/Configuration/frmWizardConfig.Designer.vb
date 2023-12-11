<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWizardConfig
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
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWizardConfig))
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblCompanyName = New System.Windows.Forms.Label()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.txtDefaultTax = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.txtWHTax = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.txtTransitInssuranceTax = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.chkAutoUpdate = New System.Windows.Forms.CheckBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.chkPreviewInvoice = New System.Windows.Forms.CheckBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.chkShowMasterGrid = New System.Windows.Forms.CheckBox()
        Me.lblStockNavigationAllow = New System.Windows.Forms.Label()
        Me.chkAllowMinusStock = New System.Windows.Forms.CheckBox()
        Me.cmbVoucherFormat = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.btnLocationSave = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtLocationAddress = New System.Windows.Forms.TextBox()
        Me.lblLocationType = New System.Windows.Forms.Label()
        Me.cmbLocationType = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtLocationName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtLocationCode = New System.Windows.Forms.TextBox()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.UltraTabPageControl4.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.Label1)
        Me.UltraTabPageControl1.Controls.Add(Me.txtAddress)
        Me.UltraTabPageControl1.Controls.Add(Me.lblCompanyName)
        Me.UltraTabPageControl1.Controls.Add(Me.txtCompanyName)
        Me.UltraTabPageControl1.Controls.Add(Me.btnClose)
        Me.UltraTabPageControl1.Controls.Add(Me.btnNext)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(703, 400)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 85)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Address"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(114, 82)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(258, 20)
        Me.txtAddress.TabIndex = 4
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(10, 7)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(246, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Company Information"
        '
        'lblCompanyName
        '
        Me.lblCompanyName.AutoSize = True
        Me.lblCompanyName.Location = New System.Drawing.Point(12, 59)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(82, 13)
        Me.lblCompanyName.TabIndex = 1
        Me.lblCompanyName.Text = "Company Name"
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Location = New System.Drawing.Point(114, 56)
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(258, 20)
        Me.txtCompanyName.TabIndex = 2
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(660, 374)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(40, 23)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Location = New System.Drawing.Point(618, 374)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(40, 23)
        Me.btnNext.TabIndex = 5
        Me.btnNext.Text = "Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Controls.Add(Me.Button3)
        Me.UltraTabPageControl4.Controls.Add(Me.Button4)
        Me.UltraTabPageControl4.Controls.Add(Me.Label47)
        Me.UltraTabPageControl4.Controls.Add(Me.txtDefaultTax)
        Me.UltraTabPageControl4.Controls.Add(Me.Label46)
        Me.UltraTabPageControl4.Controls.Add(Me.txtWHTax)
        Me.UltraTabPageControl4.Controls.Add(Me.Label45)
        Me.UltraTabPageControl4.Controls.Add(Me.txtTransitInssuranceTax)
        Me.UltraTabPageControl4.Controls.Add(Me.Label44)
        Me.UltraTabPageControl4.Controls.Add(Me.chkAutoUpdate)
        Me.UltraTabPageControl4.Controls.Add(Me.Label16)
        Me.UltraTabPageControl4.Controls.Add(Me.chkPreviewInvoice)
        Me.UltraTabPageControl4.Controls.Add(Me.Label20)
        Me.UltraTabPageControl4.Controls.Add(Me.chkShowMasterGrid)
        Me.UltraTabPageControl4.Controls.Add(Me.lblStockNavigationAllow)
        Me.UltraTabPageControl4.Controls.Add(Me.chkAllowMinusStock)
        Me.UltraTabPageControl4.Controls.Add(Me.cmbVoucherFormat)
        Me.UltraTabPageControl4.Controls.Add(Me.Label11)
        Me.UltraTabPageControl4.Controls.Add(Me.Label10)
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(703, 400)
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Location = New System.Drawing.Point(660, 374)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(40, 23)
        Me.Button3.TabIndex = 18
        Me.Button3.Text = "Close"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Location = New System.Drawing.Point(618, 374)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(40, 23)
        Me.Button4.TabIndex = 17
        Me.Button4.Text = "Next"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(50, 224)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(73, 13)
        Me.Label47.TabIndex = 15
        Me.Label47.Text = "Default Tax %"
        '
        'txtDefaultTax
        '
        Me.txtDefaultTax.Location = New System.Drawing.Point(197, 221)
        Me.txtDefaultTax.Name = "txtDefaultTax"
        Me.txtDefaultTax.Size = New System.Drawing.Size(189, 20)
        Me.txtDefaultTax.TabIndex = 16
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(50, 198)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(100, 13)
        Me.Label46.TabIndex = 13
        Me.Label46.Text = "With Holding Tax %"
        '
        'txtWHTax
        '
        Me.txtWHTax.Location = New System.Drawing.Point(197, 195)
        Me.txtWHTax.Name = "txtWHTax"
        Me.txtWHTax.Size = New System.Drawing.Size(189, 20)
        Me.txtWHTax.TabIndex = 14
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(50, 172)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(126, 13)
        Me.Label45.TabIndex = 11
        Me.Label45.Text = "Transit Inssurance Tax %"
        '
        'txtTransitInssuranceTax
        '
        Me.txtTransitInssuranceTax.Location = New System.Drawing.Point(197, 169)
        Me.txtTransitInssuranceTax.Name = "txtTransitInssuranceTax"
        Me.txtTransitInssuranceTax.Size = New System.Drawing.Size(189, 20)
        Me.txtTransitInssuranceTax.TabIndex = 12
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(50, 149)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(103, 13)
        Me.Label44.TabIndex = 9
        Me.Label44.Text = "Enable Auto Update"
        '
        'chkAutoUpdate
        '
        Me.chkAutoUpdate.AutoSize = True
        Me.chkAutoUpdate.Location = New System.Drawing.Point(197, 149)
        Me.chkAutoUpdate.Name = "chkAutoUpdate"
        Me.chkAutoUpdate.Size = New System.Drawing.Size(15, 14)
        Me.chkAutoUpdate.TabIndex = 10
        Me.chkAutoUpdate.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(50, 129)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(83, 13)
        Me.Label16.TabIndex = 7
        Me.Label16.Text = "Preview Invoice"
        '
        'chkPreviewInvoice
        '
        Me.chkPreviewInvoice.AutoSize = True
        Me.chkPreviewInvoice.Location = New System.Drawing.Point(197, 129)
        Me.chkPreviewInvoice.Name = "chkPreviewInvoice"
        Me.chkPreviewInvoice.Size = New System.Drawing.Size(15, 14)
        Me.chkPreviewInvoice.TabIndex = 8
        Me.chkPreviewInvoice.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(50, 109)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(91, 13)
        Me.Label20.TabIndex = 5
        Me.Label20.Text = "Show Master Grid"
        '
        'chkShowMasterGrid
        '
        Me.chkShowMasterGrid.AutoSize = True
        Me.chkShowMasterGrid.Location = New System.Drawing.Point(197, 109)
        Me.chkShowMasterGrid.Name = "chkShowMasterGrid"
        Me.chkShowMasterGrid.Size = New System.Drawing.Size(15, 14)
        Me.chkShowMasterGrid.TabIndex = 6
        Me.chkShowMasterGrid.UseVisualStyleBackColor = True
        '
        'lblStockNavigationAllow
        '
        Me.lblStockNavigationAllow.AutoSize = True
        Me.lblStockNavigationAllow.Location = New System.Drawing.Point(50, 89)
        Me.lblStockNavigationAllow.Name = "lblStockNavigationAllow"
        Me.lblStockNavigationAllow.Size = New System.Drawing.Size(94, 13)
        Me.lblStockNavigationAllow.TabIndex = 3
        Me.lblStockNavigationAllow.Text = "Allow Minus Stock"
        '
        'chkAllowMinusStock
        '
        Me.chkAllowMinusStock.AutoSize = True
        Me.chkAllowMinusStock.Location = New System.Drawing.Point(197, 89)
        Me.chkAllowMinusStock.Name = "chkAllowMinusStock"
        Me.chkAllowMinusStock.Size = New System.Drawing.Size(15, 14)
        Me.chkAllowMinusStock.TabIndex = 4
        Me.chkAllowMinusStock.UseVisualStyleBackColor = True
        '
        'cmbVoucherFormat
        '
        Me.cmbVoucherFormat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbVoucherFormat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbVoucherFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVoucherFormat.FormattingEnabled = True
        Me.cmbVoucherFormat.Items.AddRange(New Object() {"Normal", "Monthly", "Yearly"})
        Me.cmbVoucherFormat.Location = New System.Drawing.Point(197, 62)
        Me.cmbVoucherFormat.Name = "cmbVoucherFormat"
        Me.cmbVoucherFormat.Size = New System.Drawing.Size(189, 21)
        Me.cmbVoucherFormat.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(50, 66)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(97, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Voucher Format"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Navy
        Me.Label10.Location = New System.Drawing.Point(9, 15)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(244, 23)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "System Configuration"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.btnLocationSave)
        Me.UltraTabPageControl2.Controls.Add(Me.Button1)
        Me.UltraTabPageControl2.Controls.Add(Me.Button2)
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Controls.Add(Me.Label6)
        Me.UltraTabPageControl2.Controls.Add(Me.txtLocationAddress)
        Me.UltraTabPageControl2.Controls.Add(Me.lblLocationType)
        Me.UltraTabPageControl2.Controls.Add(Me.cmbLocationType)
        Me.UltraTabPageControl2.Controls.Add(Me.Label5)
        Me.UltraTabPageControl2.Controls.Add(Me.txtLocationName)
        Me.UltraTabPageControl2.Controls.Add(Me.Label4)
        Me.UltraTabPageControl2.Controls.Add(Me.Label3)
        Me.UltraTabPageControl2.Controls.Add(Me.txtLocationCode)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(703, 400)
        '
        'btnLocationSave
        '
        Me.btnLocationSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLocationSave.Location = New System.Drawing.Point(442, 147)
        Me.btnLocationSave.Name = "btnLocationSave"
        Me.btnLocationSave.Size = New System.Drawing.Size(60, 23)
        Me.btnLocationSave.TabIndex = 9
        Me.btnLocationSave.Text = "Save"
        Me.btnLocationSave.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(660, 374)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(40, 23)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Close"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(618, 374)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(40, 23)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "Next"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(3, 176)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(697, 192)
        Me.grdSaved.TabIndex = 10
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(25, 119)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Location Address"
        '
        'txtLocationAddress
        '
        Me.txtLocationAddress.Location = New System.Drawing.Point(127, 116)
        Me.txtLocationAddress.Multiline = True
        Me.txtLocationAddress.Name = "txtLocationAddress"
        Me.txtLocationAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLocationAddress.Size = New System.Drawing.Size(375, 25)
        Me.txtLocationAddress.TabIndex = 8
        '
        'lblLocationType
        '
        Me.lblLocationType.AutoSize = True
        Me.lblLocationType.Location = New System.Drawing.Point(261, 68)
        Me.lblLocationType.Name = "lblLocationType"
        Me.lblLocationType.Size = New System.Drawing.Size(75, 13)
        Me.lblLocationType.TabIndex = 3
        Me.lblLocationType.Text = "Location Type"
        '
        'cmbLocationType
        '
        Me.cmbLocationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLocationType.FormattingEnabled = True
        Me.cmbLocationType.Items.AddRange(New Object() {"General", "Receiving", "Raw Material", "Finsh Goods", "WIP", "Quality", "Damage", "Production"})
        Me.cmbLocationType.Location = New System.Drawing.Point(342, 64)
        Me.cmbLocationType.Name = "cmbLocationType"
        Me.cmbLocationType.Size = New System.Drawing.Size(160, 21)
        Me.cmbLocationType.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(25, 93)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Location Name"
        '
        'txtLocationName
        '
        Me.txtLocationName.Location = New System.Drawing.Point(127, 90)
        Me.txtLocationName.Name = "txtLocationName"
        Me.txtLocationName.Size = New System.Drawing.Size(375, 20)
        Me.txtLocationName.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Navy
        Me.Label4.Location = New System.Drawing.Point(9, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(226, 23)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Loction Information"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Location Code"
        '
        'txtLocationCode
        '
        Me.txtLocationCode.Location = New System.Drawing.Point(127, 64)
        Me.txtLocationCode.Name = "txtLocationCode"
        Me.txtLocationCode.Size = New System.Drawing.Size(128, 20)
        Me.txtLocationCode.TabIndex = 2
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(703, 400)
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl4)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(705, 421)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Company Info"
        UltraTab4.TabPage = Me.UltraTabPageControl4
        UltraTab4.Text = "System Config"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Location"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab4, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(703, 38)
        Me.pnlHeader.TabIndex = 16
        '
        'frmWizardConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(705, 421)
        Me.ControlBox = False
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWizardConfig"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuration"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.UltraTabPageControl4.ResumeLayout(False)
        Me.UltraTabPageControl4.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.UltraTabPageControl2.PerformLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents txtDefaultTax As System.Windows.Forms.TextBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents txtWHTax As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents txtTransitInssuranceTax As System.Windows.Forms.TextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents chkAutoUpdate As System.Windows.Forms.CheckBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chkPreviewInvoice As System.Windows.Forms.CheckBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents chkShowMasterGrid As System.Windows.Forms.CheckBox
    Friend WithEvents lblStockNavigationAllow As System.Windows.Forms.Label
    Friend WithEvents chkAllowMinusStock As System.Windows.Forms.CheckBox
    Friend WithEvents cmbVoucherFormat As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents btnLocationSave As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtLocationAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblLocationType As System.Windows.Forms.Label
    Friend WithEvents cmbLocationType As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtLocationName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLocationCode As System.Windows.Forms.TextBox
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents lblCompanyName As System.Windows.Forms.Label
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class
