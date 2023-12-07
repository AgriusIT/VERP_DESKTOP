<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefLeaveTypes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDefLeaveTypes))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.cmbEmpType = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbCostCenter = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnlLeave = New System.Windows.Forms.Panel()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.cmbCarriedForward = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbCatagory = New System.Windows.Forms.ComboBox()
        Me.txtAllowedPerYear = New System.Windows.Forms.TextBox()
        Me.cmbLeaveInCashment = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSortOrder = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbLeaveApproval = New System.Windows.Forms.ComboBox()
        Me.txtCarriedForward = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbLeaveType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbLeaveAccrual = New System.Windows.Forms.ComboBox()
        Me.cmbGenderRes = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTypeTitle = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblTypeTitle = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlLeave.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.Panel6)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlLeave)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(991, 361)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblProgress)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(991, 50)
        Me.pnlHeader.TabIndex = 0
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(360, -2)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(351, 55)
        Me.lblProgress.TabIndex = 1
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(4, 11)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(274, 29)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Define Leave Types"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel6.Controls.Add(Me.cmbEmpType)
        Me.Panel6.Controls.Add(Me.Label13)
        Me.Panel6.Controls.Add(Me.cmbCostCenter)
        Me.Panel6.Controls.Add(Me.Label11)
        Me.Panel6.Controls.Add(Me.cmbCompany)
        Me.Panel6.Controls.Add(Me.Label12)
        Me.Panel6.Location = New System.Drawing.Point(15, 58)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(960, 48)
        Me.Panel6.TabIndex = 1
        '
        'cmbEmpType
        '
        Me.cmbEmpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEmpType.FormattingEnabled = True
        Me.cmbEmpType.Items.AddRange(New Object() {"Paid", "UnPaid"})
        Me.cmbEmpType.Location = New System.Drawing.Point(748, 11)
        Me.cmbEmpType.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbEmpType.Name = "cmbEmpType"
        Me.cmbEmpType.Size = New System.Drawing.Size(180, 24)
        Me.cmbEmpType.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbEmpType, "Select Employee Type")
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(632, 15)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(72, 17)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "Emp Type"
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCostCenter.FormattingEnabled = True
        Me.cmbCostCenter.Items.AddRange(New Object() {"Paid", "UnPaid"})
        Me.cmbCostCenter.Location = New System.Drawing.Point(443, 11)
        Me.cmbCostCenter.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(180, 24)
        Me.cmbCostCenter.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbCostCenter, "Select Cost Center")
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(325, 15)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(82, 17)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "Cost Center"
        '
        'cmbCompany
        '
        Me.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Items.AddRange(New Object() {"Paid", "UnPaid"})
        Me.cmbCompany.Location = New System.Drawing.Point(136, 11)
        Me.cmbCompany.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(180, 24)
        Me.cmbCompany.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbCompany, "Select Company")
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(7, 15)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(108, 17)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Company Name"
        '
        'pnlLeave
        '
        Me.pnlLeave.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlLeave.Controls.Add(Me.chkActive)
        Me.pnlLeave.Controls.Add(Me.cmbCarriedForward)
        Me.pnlLeave.Controls.Add(Me.Label15)
        Me.pnlLeave.Controls.Add(Me.Label14)
        Me.pnlLeave.Controls.Add(Me.cmbCatagory)
        Me.pnlLeave.Controls.Add(Me.txtAllowedPerYear)
        Me.pnlLeave.Controls.Add(Me.cmbLeaveInCashment)
        Me.pnlLeave.Controls.Add(Me.Label9)
        Me.pnlLeave.Controls.Add(Me.txtSortOrder)
        Me.pnlLeave.Controls.Add(Me.Label6)
        Me.pnlLeave.Controls.Add(Me.cmbLeaveApproval)
        Me.pnlLeave.Controls.Add(Me.txtCarriedForward)
        Me.pnlLeave.Controls.Add(Me.Label10)
        Me.pnlLeave.Controls.Add(Me.Label8)
        Me.pnlLeave.Controls.Add(Me.cmbLeaveType)
        Me.pnlLeave.Controls.Add(Me.Label3)
        Me.pnlLeave.Controls.Add(Me.Label7)
        Me.pnlLeave.Controls.Add(Me.cmbLeaveAccrual)
        Me.pnlLeave.Controls.Add(Me.cmbGenderRes)
        Me.pnlLeave.Controls.Add(Me.Label1)
        Me.pnlLeave.Controls.Add(Me.txtTypeTitle)
        Me.pnlLeave.Controls.Add(Me.Label2)
        Me.pnlLeave.Controls.Add(Me.lblTypeTitle)
        Me.pnlLeave.Location = New System.Drawing.Point(15, 113)
        Me.pnlLeave.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlLeave.Name = "pnlLeave"
        Me.pnlLeave.Size = New System.Drawing.Size(960, 225)
        Me.pnlLeave.TabIndex = 0
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.Location = New System.Drawing.Point(621, 185)
        Me.chkActive.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(68, 21)
        Me.chkActive.TabIndex = 22
        Me.chkActive.Text = "Active"
        Me.ToolTip1.SetToolTip(Me.chkActive, "Active")
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'cmbCarriedForward
        '
        Me.cmbCarriedForward.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCarriedForward.FormattingEnabled = True
        Me.cmbCarriedForward.Items.AddRange(New Object() {"True", "False"})
        Me.cmbCarriedForward.Location = New System.Drawing.Point(136, 149)
        Me.cmbCarriedForward.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbCarriedForward.Name = "cmbCarriedForward"
        Me.cmbCarriedForward.Size = New System.Drawing.Size(307, 24)
        Me.cmbCarriedForward.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.cmbCarriedForward, "Select Carried Forward")
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(7, 154)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(109, 17)
        Me.Label15.TabIndex = 16
        Me.Label15.Text = "Carried Forward"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(452, 21)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(108, 17)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "Leave Catagory"
        '
        'cmbCatagory
        '
        Me.cmbCatagory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCatagory.FormattingEnabled = True
        Me.cmbCatagory.Items.AddRange(New Object() {"Paid", "UnPaid"})
        Me.cmbCatagory.Location = New System.Drawing.Point(621, 17)
        Me.cmbCatagory.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbCatagory.Name = "cmbCatagory"
        Me.cmbCatagory.Size = New System.Drawing.Size(307, 24)
        Me.cmbCatagory.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbCatagory, "Select Leave Catagory")
        '
        'txtAllowedPerYear
        '
        Me.txtAllowedPerYear.Location = New System.Drawing.Point(621, 117)
        Me.txtAllowedPerYear.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtAllowedPerYear.Name = "txtAllowedPerYear"
        Me.txtAllowedPerYear.Size = New System.Drawing.Size(307, 22)
        Me.txtAllowedPerYear.TabIndex = 15
        Me.txtAllowedPerYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtAllowedPerYear, "Leaves Allowed per year")
        '
        'cmbLeaveInCashment
        '
        Me.cmbLeaveInCashment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLeaveInCashment.FormattingEnabled = True
        Me.cmbLeaveInCashment.Items.AddRange(New Object() {"True", "False"})
        Me.cmbLeaveInCashment.Location = New System.Drawing.Point(136, 116)
        Me.cmbLeaveInCashment.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbLeaveInCashment.Name = "cmbLeaveInCashment"
        Me.cmbLeaveInCashment.Size = New System.Drawing.Size(307, 24)
        Me.cmbLeaveInCashment.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.cmbLeaveInCashment, "Select Leave InCashment")
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 121)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(125, 17)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Leave InCashment"
        '
        'txtSortOrder
        '
        Me.txtSortOrder.Location = New System.Drawing.Point(136, 182)
        Me.txtSortOrder.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtSortOrder.Name = "txtSortOrder"
        Me.txtSortOrder.Size = New System.Drawing.Size(307, 22)
        Me.txtSortOrder.TabIndex = 21
        Me.txtSortOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtSortOrder, "Sort Order")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 186)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 17)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Sort Order"
        '
        'cmbLeaveApproval
        '
        Me.cmbLeaveApproval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLeaveApproval.FormattingEnabled = True
        Me.cmbLeaveApproval.Items.AddRange(New Object() {"... Select any value ...", "Project Manager", "Department Head", "Specific Employee"})
        Me.cmbLeaveApproval.Location = New System.Drawing.Point(136, 82)
        Me.cmbLeaveApproval.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbLeaveApproval.Name = "cmbLeaveApproval"
        Me.cmbLeaveApproval.Size = New System.Drawing.Size(307, 24)
        Me.cmbLeaveApproval.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cmbLeaveApproval, "Select Leave Approval")
        '
        'txtCarriedForward
        '
        Me.txtCarriedForward.Location = New System.Drawing.Point(621, 149)
        Me.txtCarriedForward.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCarriedForward.Name = "txtCarriedForward"
        Me.txtCarriedForward.Size = New System.Drawing.Size(307, 22)
        Me.txtCarriedForward.TabIndex = 19
        Me.txtCarriedForward.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtCarriedForward, "Carried Forward Days")
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(7, 86)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(126, 17)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Leave Approval by"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(452, 158)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(145, 17)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Carried Forward Days"
        '
        'cmbLeaveType
        '
        Me.cmbLeaveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLeaveType.FormattingEnabled = True
        Me.cmbLeaveType.Items.AddRange(New Object() {"... Select any value ...", "Paid", "UnPaid"})
        Me.cmbLeaveType.Location = New System.Drawing.Point(136, 49)
        Me.cmbLeaveType.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbLeaveType.Name = "cmbLeaveType"
        Me.cmbLeaveType.Size = New System.Drawing.Size(307, 24)
        Me.cmbLeaveType.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbLeaveType, "Select Leave Type")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(452, 53)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Leave Accrual"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(452, 121)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(115, 17)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Allowed per Year"
        '
        'cmbLeaveAccrual
        '
        Me.cmbLeaveAccrual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLeaveAccrual.FormattingEnabled = True
        Me.cmbLeaveAccrual.Items.AddRange(New Object() {"... Select any value ...", "Daily", "Monthly", "Yearly"})
        Me.cmbLeaveAccrual.Location = New System.Drawing.Point(621, 49)
        Me.cmbLeaveAccrual.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbLeaveAccrual.Name = "cmbLeaveAccrual"
        Me.cmbLeaveAccrual.Size = New System.Drawing.Size(307, 24)
        Me.cmbLeaveAccrual.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbLeaveAccrual, "Select Leave Accrual")
        '
        'cmbGenderRes
        '
        Me.cmbGenderRes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGenderRes.FormattingEnabled = True
        Me.cmbGenderRes.Items.AddRange(New Object() {"Any Gender", "Male", "Female"})
        Me.cmbGenderRes.Location = New System.Drawing.Point(621, 82)
        Me.cmbGenderRes.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbGenderRes.Name = "cmbGenderRes"
        Me.cmbGenderRes.Size = New System.Drawing.Size(307, 24)
        Me.cmbGenderRes.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.cmbGenderRes, "Select Gender Restriction ")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 53)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Leave Type"
        '
        'txtTypeTitle
        '
        Me.txtTypeTitle.Location = New System.Drawing.Point(136, 17)
        Me.txtTypeTitle.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtTypeTitle.Name = "txtTypeTitle"
        Me.txtTypeTitle.Size = New System.Drawing.Size(307, 22)
        Me.txtTypeTitle.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtTypeTitle, "Leave Type Title")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(452, 87)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(127, 17)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Gender Restriction"
        '
        'lblTypeTitle
        '
        Me.lblTypeTitle.AutoSize = True
        Me.lblTypeTitle.Location = New System.Drawing.Point(7, 21)
        Me.lblTypeTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTypeTitle.Name = "lblTypeTitle"
        Me.lblTypeTitle.Size = New System.Drawing.Size(71, 17)
        Me.lblTypeTitle.TabIndex = 0
        Me.lblTypeTitle.Text = "Type Title"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-13333, -12308)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(991, 361)
        '
        'grdSaved
        '
        Me.grdSaved.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.CardWidth = 804
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(991, 361)
        Me.grdSaved.TabIndex = 7
        Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.btnRefresh, Me.btnPrint, Me.toolStripSeparator1, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(993, 27)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(63, 24)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(59, 24)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(64, 24)
        Me.btnSave.Text = "&Save"
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.RightToLeftAutoMirrorImage = True
        Me.btnDelete.Size = New System.Drawing.Size(77, 24)
        Me.btnDelete.Text = "&Delete"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(82, 24)
        Me.btnRefresh.Text = "&Refresh"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(63, 24)
        Me.btnPrint.Text = "&Print"
        Me.btnPrint.Visible = False
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'btnHelp
        '
        Me.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(24, 24)
        Me.btnHelp.Text = "He&lp"
        Me.btnHelp.Visible = False
        '
        'UltraTabControl2
        '
        Me.UltraTabControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl2.Controls.Add(Me.UltraTabSharedControlsPage2)
        Me.UltraTabControl2.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl2.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl2.Location = New System.Drawing.Point(0, 34)
        Me.UltraTabControl2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabControl2.Name = "UltraTabControl2"
        Me.UltraTabControl2.SharedControlsPage = Me.UltraTabSharedControlsPage2
        Me.UltraTabControl2.Size = New System.Drawing.Size(993, 383)
        Me.UltraTabControl2.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl2.TabIndex = 2
        Me.UltraTabControl2.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "LeaveType"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl2.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl2.TabStop = False
        Me.UltraTabControl2.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage2
        '
        Me.UltraTabSharedControlsPage2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabSharedControlsPage2.Name = "UltraTabSharedControlsPage2"
        Me.UltraTabSharedControlsPage2.Size = New System.Drawing.Size(991, 361)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(944, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CtrlGrdBar1.MyGrid = Me.grdSaved
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(49, 31)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'frmDefLeaveTypes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(993, 416)
        Me.Controls.Add(Me.UltraTabControl2)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmDefLeaveTypes"
        Me.Text = "Define Leave Types"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.pnlLeave.ResumeLayout(False)
        Me.pnlLeave.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlLeave As System.Windows.Forms.Panel
    Friend WithEvents txtTypeTitle As System.Windows.Forms.TextBox
    Friend WithEvents lblTypeTitle As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbLeaveType As System.Windows.Forms.ComboBox
    Friend WithEvents txtCarriedForward As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbGenderRes As System.Windows.Forms.ComboBox
    Friend WithEvents cmbLeaveApproval As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbLeaveAccrual As System.Windows.Forms.ComboBox
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtAllowedPerYear As System.Windows.Forms.TextBox
    Friend WithEvents cmbLeaveInCashment As System.Windows.Forms.ComboBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents cmbEmpType As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbCatagory As System.Windows.Forms.ComboBox
    Friend WithEvents UltraTabControl2 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents cmbCarriedForward As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
End Class
