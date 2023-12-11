<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProSales
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProSales))
        Dim grdPayment_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdTask_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Color")
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtSerialNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Ttip = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.txtAmountPaid = New System.Windows.Forms.TextBox()
        Me.txtNetPayables = New System.Windows.Forms.TextBox()
        Me.txtPlotNo = New System.Windows.Forms.TextBox()
        Me.txtBlockNo = New System.Windows.Forms.TextBox()
        Me.txtPropertyType = New System.Windows.Forms.TextBox()
        Me.txtLandArea = New System.Windows.Forms.TextBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.txtSector = New System.Windows.Forms.TextBox()
        Me.txtCellNo = New System.Windows.Forms.TextBox()
        Me.lblLandArea = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.grdPayment = New Janus.Windows.GridEX.GridEX()
        Me.grdTask = New Janus.Windows.GridEX.GridEX()
        Me.lblNumberConverter = New System.Windows.Forms.Label()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.cmbName = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.dtpSalesDate = New System.Windows.Forms.DateTimePicker()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.grdPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTask, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(771, 46)
        Me.pnlHeader.TabIndex = 0
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(8, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(56, 25)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Sales"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 589)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(771, 68)
        Me.Panel2.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackgroundImage = CType(resources.GetObject("btnCancel.BackgroundImage"), System.Drawing.Image)
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(635, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(54, 62)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackgroundImage = CType(resources.GetObject("btnSave.BackgroundImage"), System.Drawing.Image)
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(708, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(54, 62)
        Me.btnSave.TabIndex = 1
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(269, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 15)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Title"
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtTitle.Location = New System.Drawing.Point(272, 68)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.ReadOnly = True
        Me.txtTitle.Size = New System.Drawing.Size(196, 23)
        Me.txtTitle.TabIndex = 6
        Me.Ttip.SetToolTip(Me.txtTitle, "Title")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(471, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 15)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Plot No"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(618, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 15)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Block No"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(10, 94)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 15)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Sector"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(13, 136)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(52, 21)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Buyer"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel3.Location = New System.Drawing.Point(17, 157)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(746, 1)
        Me.Panel3.TabIndex = 25
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(14, 161)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(46, 15)
        Me.Label14.TabIndex = 24
        Me.Label14.Text = "Cell No"
        '
        'txtSerialNo
        '
        Me.txtSerialNo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtSerialNo.Location = New System.Drawing.Point(13, 68)
        Me.txtSerialNo.Name = "txtSerialNo"
        Me.txtSerialNo.Size = New System.Drawing.Size(128, 23)
        Me.txtSerialNo.TabIndex = 2
        Me.Ttip.SetToolTip(Me.txtSerialNo, "Serial No")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(10, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 15)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Serial No"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(157, 162)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 15)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Name"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(14, 202)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 15)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtRemarks.Location = New System.Drawing.Point(13, 220)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(749, 42)
        Me.txtRemarks.TabIndex = 36
        Me.Ttip.SetToolTip(Me.txtRemarks, "Remarks")
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(618, 94)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 15)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Property Type"
        '
        'txtPrice
        '
        Me.txtPrice.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtPrice.Location = New System.Drawing.Point(327, 177)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(141, 23)
        Me.txtPrice.TabIndex = 30
        Me.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Ttip.SetToolTip(Me.txtPrice, "Price")
        '
        'txtAmountPaid
        '
        Me.txtAmountPaid.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtAmountPaid.Location = New System.Drawing.Point(474, 177)
        Me.txtAmountPaid.Name = "txtAmountPaid"
        Me.txtAmountPaid.ReadOnly = True
        Me.txtAmountPaid.Size = New System.Drawing.Size(141, 23)
        Me.txtAmountPaid.TabIndex = 32
        Me.txtAmountPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Ttip.SetToolTip(Me.txtAmountPaid, "Amount Paid")
        '
        'txtNetPayables
        '
        Me.txtNetPayables.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtNetPayables.Location = New System.Drawing.Point(621, 177)
        Me.txtNetPayables.Name = "txtNetPayables"
        Me.txtNetPayables.ReadOnly = True
        Me.txtNetPayables.Size = New System.Drawing.Size(141, 23)
        Me.txtNetPayables.TabIndex = 34
        Me.txtNetPayables.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Ttip.SetToolTip(Me.txtNetPayables, "Net Payables")
        '
        'txtPlotNo
        '
        Me.txtPlotNo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtPlotNo.Location = New System.Drawing.Point(474, 68)
        Me.txtPlotNo.Name = "txtPlotNo"
        Me.txtPlotNo.ReadOnly = True
        Me.txtPlotNo.Size = New System.Drawing.Size(141, 23)
        Me.txtPlotNo.TabIndex = 8
        Me.Ttip.SetToolTip(Me.txtPlotNo, "Plot No.")
        '
        'txtBlockNo
        '
        Me.txtBlockNo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtBlockNo.Location = New System.Drawing.Point(618, 68)
        Me.txtBlockNo.Name = "txtBlockNo"
        Me.txtBlockNo.ReadOnly = True
        Me.txtBlockNo.Size = New System.Drawing.Size(141, 23)
        Me.txtBlockNo.TabIndex = 10
        Me.Ttip.SetToolTip(Me.txtBlockNo, "Block No")
        '
        'txtPropertyType
        '
        Me.txtPropertyType.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtPropertyType.Location = New System.Drawing.Point(618, 112)
        Me.txtPropertyType.Name = "txtPropertyType"
        Me.txtPropertyType.ReadOnly = True
        Me.txtPropertyType.Size = New System.Drawing.Size(141, 23)
        Me.txtPropertyType.TabIndex = 20
        Me.Ttip.SetToolTip(Me.txtPropertyType, "Property Type")
        '
        'txtLandArea
        '
        Me.txtLandArea.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtLandArea.Location = New System.Drawing.Point(474, 112)
        Me.txtLandArea.Name = "txtLandArea"
        Me.txtLandArea.ReadOnly = True
        Me.txtLandArea.Size = New System.Drawing.Size(141, 23)
        Me.txtLandArea.TabIndex = 18
        Me.Ttip.SetToolTip(Me.txtLandArea, "land Area")
        '
        'txtCity
        '
        Me.txtCity.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtCity.Location = New System.Drawing.Point(327, 112)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.ReadOnly = True
        Me.txtCity.Size = New System.Drawing.Size(141, 23)
        Me.txtCity.TabIndex = 16
        Me.Ttip.SetToolTip(Me.txtCity, "City")
        '
        'txtLocation
        '
        Me.txtLocation.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtLocation.Location = New System.Drawing.Point(147, 112)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReadOnly = True
        Me.txtLocation.Size = New System.Drawing.Size(174, 23)
        Me.txtLocation.TabIndex = 14
        Me.Ttip.SetToolTip(Me.txtLocation, "Location")
        '
        'txtSector
        '
        Me.txtSector.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtSector.Location = New System.Drawing.Point(12, 112)
        Me.txtSector.Name = "txtSector"
        Me.txtSector.ReadOnly = True
        Me.txtSector.Size = New System.Drawing.Size(129, 23)
        Me.txtSector.TabIndex = 12
        Me.Ttip.SetToolTip(Me.txtSector, "Sector")
        '
        'txtCellNo
        '
        Me.txtCellNo.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtCellNo.Location = New System.Drawing.Point(12, 177)
        Me.txtCellNo.Name = "txtCellNo"
        Me.txtCellNo.Size = New System.Drawing.Size(142, 23)
        Me.txtCellNo.TabIndex = 26
        Me.Ttip.SetToolTip(Me.txtCellNo, "Cell no")
        '
        'lblLandArea
        '
        Me.lblLandArea.AutoSize = True
        Me.lblLandArea.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblLandArea.Location = New System.Drawing.Point(471, 94)
        Me.lblLandArea.Name = "lblLandArea"
        Me.lblLandArea.Size = New System.Drawing.Size(60, 15)
        Me.lblLandArea.TabIndex = 17
        Me.lblLandArea.Text = "Land Area"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(324, 94)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(28, 15)
        Me.Label12.TabIndex = 15
        Me.Label12.Text = "City"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(144, 94)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 15)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "Location"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(324, 161)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(33, 15)
        Me.Label10.TabIndex = 29
        Me.Label10.Text = "Price"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(471, 161)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(77, 15)
        Me.Label15.TabIndex = 31
        Me.Label15.Text = "Amount Paid"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(618, 161)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(75, 15)
        Me.Label16.TabIndex = 33
        Me.Label16.Text = "Net Payables"
        '
        'grdPayment
        '
        Me.grdPayment.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdPayment.ColumnAutoResize = True
        grdPayment_DesignTimeLayout.LayoutString = resources.GetString("grdPayment_DesignTimeLayout.LayoutString")
        Me.grdPayment.DesignTimeLayout = grdPayment_DesignTimeLayout
        Me.grdPayment.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdPayment.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdPayment.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdPayment.GroupByBoxVisible = False
        Me.grdPayment.Location = New System.Drawing.Point(12, 268)
        Me.grdPayment.Name = "grdPayment"
        Me.grdPayment.RecordNavigator = True
        Me.grdPayment.Size = New System.Drawing.Size(750, 154)
        Me.grdPayment.TabIndex = 38
        Me.grdPayment.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPayment.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdPayment.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grdTask
        '
        Me.grdTask.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdTask.ColumnAutoResize = True
        grdTask_DesignTimeLayout.LayoutString = resources.GetString("grdTask_DesignTimeLayout.LayoutString")
        Me.grdTask.DesignTimeLayout = grdTask_DesignTimeLayout
        Me.grdTask.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdTask.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdTask.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdTask.GroupByBoxVisible = False
        Me.grdTask.Location = New System.Drawing.Point(13, 429)
        Me.grdTask.Name = "grdTask"
        Me.grdTask.RecordNavigator = True
        Me.grdTask.Size = New System.Drawing.Size(750, 154)
        Me.grdTask.TabIndex = 0
        Me.grdTask.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblNumberConverter
        '
        Me.lblNumberConverter.AutoSize = True
        Me.lblNumberConverter.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblNumberConverter.Location = New System.Drawing.Point(327, 202)
        Me.lblNumberConverter.Name = "lblNumberConverter"
        Me.lblNumberConverter.Size = New System.Drawing.Size(10, 13)
        Me.lblNumberConverter.TabIndex = 37
        Me.lblNumberConverter.Text = "-"
        '
        'cmbName
        '
        Me.cmbName.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbName.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbName.CheckedListSettings.CheckStateMember = ""
        Appearance7.BackColor = System.Drawing.Color.White
        Appearance7.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbName.DisplayLayout.Appearance = Appearance7
        UltraGridColumn5.Header.VisiblePosition = 0
        UltraGridColumn5.Hidden = True
        UltraGridColumn6.Header.VisiblePosition = 1
        UltraGridColumn7.Header.VisiblePosition = 2
        UltraGridColumn8.Header.VisiblePosition = 3
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn5, UltraGridColumn6, UltraGridColumn7, UltraGridColumn8})
        Me.cmbName.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbName.DisplayLayout.InterBandSpacing = 10
        Me.cmbName.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbName.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbName.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance8.BackColor = System.Drawing.Color.Transparent
        Me.cmbName.DisplayLayout.Override.CardAreaAppearance = Appearance8
        Me.cmbName.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbName.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance9.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance9.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance9.ForeColor = System.Drawing.Color.White
        Appearance9.TextHAlignAsString = "Left"
        Appearance9.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbName.DisplayLayout.Override.HeaderAppearance = Appearance9
        Me.cmbName.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance10.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbName.DisplayLayout.Override.RowAppearance = Appearance10
        Appearance11.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance11.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbName.DisplayLayout.Override.RowSelectorAppearance = Appearance11
        Me.cmbName.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbName.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance12.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance12.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance12.ForeColor = System.Drawing.Color.Black
        Me.cmbName.DisplayLayout.Override.SelectedRowAppearance = Appearance12
        Me.cmbName.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbName.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbName.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbName.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbName.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbName.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbName.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbName.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbName.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbName.Location = New System.Drawing.Point(160, 177)
        Me.cmbName.MaxDropDownItems = 20
        Me.cmbName.Name = "cmbName"
        Me.cmbName.Size = New System.Drawing.Size(161, 24)
        Me.cmbName.TabIndex = 28
        '
        'dtpSalesDate
        '
        Me.dtpSalesDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpSalesDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSalesDate.Location = New System.Drawing.Point(147, 70)
        Me.dtpSalesDate.Name = "dtpSalesDate"
        Me.dtpSalesDate.Size = New System.Drawing.Size(119, 20)
        Me.dtpSalesDate.TabIndex = 4
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblDate.Location = New System.Drawing.Point(144, 52)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(31, 15)
        Me.lblDate.TabIndex = 3
        Me.lblDate.Text = "Date"
        '
        'frmProSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(771, 657)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.dtpSalesDate)
        Me.Controls.Add(Me.cmbName)
        Me.Controls.Add(Me.lblNumberConverter)
        Me.Controls.Add(Me.txtSector)
        Me.Controls.Add(Me.txtLocation)
        Me.Controls.Add(Me.txtCity)
        Me.Controls.Add(Me.txtLandArea)
        Me.Controls.Add(Me.txtPropertyType)
        Me.Controls.Add(Me.txtBlockNo)
        Me.Controls.Add(Me.txtPlotNo)
        Me.Controls.Add(Me.grdTask)
        Me.Controls.Add(Me.grdPayment)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.lblLandArea)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtNetPayables)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtAmountPaid)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtPrice)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtCellNo)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTitle)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.txtSerialNo)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmProSales"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Property Sales"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.grdPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTask, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtSerialNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Ttip As System.Windows.Forms.ToolTip
    Friend WithEvents lblLandArea As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtAmountPaid As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtNetPayables As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents grdPayment As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtPlotNo As System.Windows.Forms.TextBox
    Friend WithEvents txtBlockNo As System.Windows.Forms.TextBox
    Friend WithEvents txtPropertyType As System.Windows.Forms.TextBox
    Friend WithEvents txtLandArea As System.Windows.Forms.TextBox
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtSector As System.Windows.Forms.TextBox
    Friend WithEvents grdTask As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblNumberConverter As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents txtCellNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbName As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents dtpSalesDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
End Class
