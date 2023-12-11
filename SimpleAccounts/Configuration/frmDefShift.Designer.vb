<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefShift
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDefShift))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblShiftCode = New System.Windows.Forms.Label()
        Me.lblShiftName = New System.Windows.Forms.Label()
        Me.lblStartDate = New System.Windows.Forms.Label()
        Me.lblShiftEndDate = New System.Windows.Forms.Label()
        Me.lblShiftStartTime = New System.Windows.Forms.Label()
        Me.lblShiftEndTime = New System.Windows.Forms.Label()
        Me.lblComments = New System.Windows.Forms.Label()
        Me.txtshiftcode = New System.Windows.Forms.TextBox()
        Me.txtshiftname = New System.Windows.Forms.TextBox()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.dtpShiftStartDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpshiftendDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpshiftStartTime = New System.Windows.Forms.DateTimePicker()
        Me.dtpShiftEndTime = New System.Windows.Forms.DateTimePicker()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.txtshortorder = New System.Windows.Forms.TextBox()
        Me.lblSortOrder = New System.Windows.Forms.Label()
        Me.grdShifttype = New Janus.Windows.GridEX.GridEX()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.dtpTxtFlexOut = New System.Windows.Forms.DateTimePicker()
        Me.dtpTxtFlexIn = New System.Windows.Forms.DateTimePicker()
        Me.txtOverTimeRate = New System.Windows.Forms.TextBox()
        Me.dtpBreakEndTime = New System.Windows.Forms.DateTimePicker()
        Me.dtpBreakStartTime = New System.Windows.Forms.DateTimePicker()
        Me.dtpSpecialDayBreakEndTime = New System.Windows.Forms.DateTimePicker()
        Me.dtpSpecialDayBreakStartTime = New System.Windows.Forms.DateTimePicker()
        Me.chkShiftNight = New System.Windows.Forms.CheckBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblFlexOutTime = New System.Windows.Forms.Label()
        Me.lblFlexInTime = New System.Windows.Forms.Label()
        Me.lblOvertimeRate = New System.Windows.Forms.Label()
        Me.lblSpecialBreakEndTime = New System.Windows.Forms.Label()
        Me.lblBreakStartTime = New System.Windows.Forms.Label()
        Me.cmbSpecialBreakDay = New System.Windows.Forms.ComboBox()
        Me.lblBreakEndTime = New System.Windows.Forms.Label()
        Me.lblBreakDayStartTime = New System.Windows.Forms.Label()
        Me.lblBreakDay = New System.Windows.Forms.Label()
        Me.lblOverTimeStartFrom = New System.Windows.Forms.Label()
        Me.dtpOTStart = New System.Windows.Forms.DateTimePicker()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grdShifttype, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1028, 32)
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
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 29)
        Me.btnDelete.Text = "D&elete"
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(28, 29)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(26, 12)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(181, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Define Shift"
        '
        'lblShiftCode
        '
        Me.lblShiftCode.AutoSize = True
        Me.lblShiftCode.Location = New System.Drawing.Point(44, 155)
        Me.lblShiftCode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblShiftCode.Name = "lblShiftCode"
        Me.lblShiftCode.Size = New System.Drawing.Size(84, 20)
        Me.lblShiftCode.TabIndex = 4
        Me.lblShiftCode.Text = "Shfit Code"
        '
        'lblShiftName
        '
        Me.lblShiftName.AutoSize = True
        Me.lblShiftName.Location = New System.Drawing.Point(44, 115)
        Me.lblShiftName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblShiftName.Name = "lblShiftName"
        Me.lblShiftName.Size = New System.Drawing.Size(88, 20)
        Me.lblShiftName.TabIndex = 2
        Me.lblShiftName.Text = "Shift Name"
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Location = New System.Drawing.Point(44, 394)
        Me.lblStartDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(120, 20)
        Me.lblStartDate.TabIndex = 23
        Me.lblStartDate.Text = "Shift Start Date"
        '
        'lblShiftEndDate
        '
        Me.lblShiftEndDate.AutoSize = True
        Me.lblShiftEndDate.Location = New System.Drawing.Point(454, 391)
        Me.lblShiftEndDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblShiftEndDate.Name = "lblShiftEndDate"
        Me.lblShiftEndDate.Size = New System.Drawing.Size(114, 20)
        Me.lblShiftEndDate.TabIndex = 25
        Me.lblShiftEndDate.Text = "Shift End Date"
        '
        'lblShiftStartTime
        '
        Me.lblShiftStartTime.AutoSize = True
        Me.lblShiftStartTime.Location = New System.Drawing.Point(44, 232)
        Me.lblShiftStartTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblShiftStartTime.Name = "lblShiftStartTime"
        Me.lblShiftStartTime.Size = New System.Drawing.Size(119, 20)
        Me.lblShiftStartTime.TabIndex = 9
        Me.lblShiftStartTime.Text = "Shift Start Time"
        '
        'lblShiftEndTime
        '
        Me.lblShiftEndTime.AutoSize = True
        Me.lblShiftEndTime.Location = New System.Drawing.Point(454, 232)
        Me.lblShiftEndTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblShiftEndTime.Name = "lblShiftEndTime"
        Me.lblShiftEndTime.Size = New System.Drawing.Size(113, 20)
        Me.lblShiftEndTime.TabIndex = 11
        Me.lblShiftEndTime.Text = "Shift End Time"
        '
        'lblComments
        '
        Me.lblComments.AutoSize = True
        Me.lblComments.Location = New System.Drawing.Point(44, 483)
        Me.lblComments.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(78, 20)
        Me.lblComments.TabIndex = 31
        Me.lblComments.Text = "Comment"
        '
        'txtshiftcode
        '
        Me.txtshiftcode.Location = New System.Drawing.Point(219, 151)
        Me.txtshiftcode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtshiftcode.MaxLength = 5
        Me.txtshiftcode.Name = "txtshiftcode"
        Me.txtshiftcode.Size = New System.Drawing.Size(170, 26)
        Me.txtshiftcode.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtshiftcode, "Shift Code")
        '
        'txtshiftname
        '
        Me.txtshiftname.Location = New System.Drawing.Point(219, 111)
        Me.txtshiftname.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtshiftname.Name = "txtshiftname"
        Me.txtshiftname.Size = New System.Drawing.Size(625, 26)
        Me.txtshiftname.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtshiftname, "Shift Name")
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(219, 468)
        Me.txtComment.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComment.Size = New System.Drawing.Size(625, 106)
        Me.txtComment.TabIndex = 32
        Me.ToolTip1.SetToolTip(Me.txtComment, "Comments")
        '
        'dtpShiftStartDate
        '
        Me.dtpShiftStartDate.Checked = False
        Me.dtpShiftStartDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpShiftStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpShiftStartDate.Location = New System.Drawing.Point(219, 388)
        Me.dtpShiftStartDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpShiftStartDate.Name = "dtpShiftStartDate"
        Me.dtpShiftStartDate.ShowCheckBox = True
        Me.dtpShiftStartDate.Size = New System.Drawing.Size(170, 26)
        Me.dtpShiftStartDate.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.dtpShiftStartDate, "Optional Shift Start Date")
        '
        'dtpshiftendDate
        '
        Me.dtpshiftendDate.Checked = False
        Me.dtpshiftendDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpshiftendDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpshiftendDate.Location = New System.Drawing.Point(664, 382)
        Me.dtpshiftendDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpshiftendDate.Name = "dtpshiftendDate"
        Me.dtpshiftendDate.ShowCheckBox = True
        Me.dtpshiftendDate.Size = New System.Drawing.Size(180, 26)
        Me.dtpshiftendDate.TabIndex = 26
        Me.ToolTip1.SetToolTip(Me.dtpshiftendDate, "Optional Shift End Date")
        '
        'dtpshiftStartTime
        '
        Me.dtpshiftStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpshiftStartTime.Location = New System.Drawing.Point(219, 226)
        Me.dtpshiftStartTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpshiftStartTime.Name = "dtpshiftStartTime"
        Me.dtpshiftStartTime.Size = New System.Drawing.Size(170, 26)
        Me.dtpshiftStartTime.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.dtpshiftStartTime, "Shift Start Time")
        '
        'dtpShiftEndTime
        '
        Me.dtpShiftEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpShiftEndTime.Location = New System.Drawing.Point(664, 226)
        Me.dtpShiftEndTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpShiftEndTime.Name = "dtpShiftEndTime"
        Me.dtpShiftEndTime.Size = New System.Drawing.Size(180, 26)
        Me.dtpShiftEndTime.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.dtpShiftEndTime, "Shift End Time")
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.Location = New System.Drawing.Point(286, 585)
        Me.chkActive.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(78, 24)
        Me.chkActive.TabIndex = 35
        Me.chkActive.Text = "Active"
        Me.ToolTip1.SetToolTip(Me.chkActive, "Shift Status Active Or Inactive")
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'txtshortorder
        '
        Me.txtshortorder.Location = New System.Drawing.Point(219, 585)
        Me.txtshortorder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtshortorder.Name = "txtshortorder"
        Me.txtshortorder.Size = New System.Drawing.Size(56, 26)
        Me.txtshortorder.TabIndex = 34
        Me.txtshortorder.Text = "1"
        Me.ToolTip1.SetToolTip(Me.txtshortorder, "Sort Order")
        '
        'lblSortOrder
        '
        Me.lblSortOrder.AutoSize = True
        Me.lblSortOrder.Location = New System.Drawing.Point(44, 586)
        Me.lblSortOrder.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSortOrder.Name = "lblSortOrder"
        Me.lblSortOrder.Size = New System.Drawing.Size(83, 20)
        Me.lblSortOrder.TabIndex = 33
        Me.lblSortOrder.Text = "Sort Order"
        '
        'grdShifttype
        '
        Me.grdShifttype.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdShifttype.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdShifttype.GroupByBoxVisible = False
        Me.grdShifttype.Location = New System.Drawing.Point(2, 643)
        Me.grdShifttype.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdShifttype.Name = "grdShifttype"
        Me.grdShifttype.RecordNavigator = True
        Me.grdShifttype.Size = New System.Drawing.Size(1024, 215)
        Me.grdShifttype.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.grdShifttype, "Define Shifts")
        Me.grdShifttype.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Timer1
        '
        '
        'dtpTxtFlexOut
        '
        Me.dtpTxtFlexOut.Checked = False
        Me.dtpTxtFlexOut.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpTxtFlexOut.Location = New System.Drawing.Point(664, 425)
        Me.dtpTxtFlexOut.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTxtFlexOut.Name = "dtpTxtFlexOut"
        Me.dtpTxtFlexOut.ShowCheckBox = True
        Me.dtpTxtFlexOut.Size = New System.Drawing.Size(180, 26)
        Me.dtpTxtFlexOut.TabIndex = 30
        Me.ToolTip1.SetToolTip(Me.dtpTxtFlexOut, "Shift End Time")
        '
        'dtpTxtFlexIn
        '
        Me.dtpTxtFlexIn.Checked = False
        Me.dtpTxtFlexIn.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpTxtFlexIn.Location = New System.Drawing.Point(219, 428)
        Me.dtpTxtFlexIn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTxtFlexIn.Name = "dtpTxtFlexIn"
        Me.dtpTxtFlexIn.ShowCheckBox = True
        Me.dtpTxtFlexIn.Size = New System.Drawing.Size(170, 26)
        Me.dtpTxtFlexIn.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.dtpTxtFlexIn, "Shift Start Time")
        '
        'txtOverTimeRate
        '
        Me.txtOverTimeRate.Location = New System.Drawing.Point(604, 151)
        Me.txtOverTimeRate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtOverTimeRate.MaxLength = 5
        Me.txtOverTimeRate.Name = "txtOverTimeRate"
        Me.txtOverTimeRate.Size = New System.Drawing.Size(240, 26)
        Me.txtOverTimeRate.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtOverTimeRate, "Shift Code")
        Me.txtOverTimeRate.Visible = False
        '
        'dtpBreakEndTime
        '
        Me.dtpBreakEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpBreakEndTime.Location = New System.Drawing.Point(664, 342)
        Me.dtpBreakEndTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpBreakEndTime.Name = "dtpBreakEndTime"
        Me.dtpBreakEndTime.Size = New System.Drawing.Size(180, 26)
        Me.dtpBreakEndTime.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.dtpBreakEndTime, "Shift End Time")
        '
        'dtpBreakStartTime
        '
        Me.dtpBreakStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpBreakStartTime.Location = New System.Drawing.Point(219, 266)
        Me.dtpBreakStartTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpBreakStartTime.Name = "dtpBreakStartTime"
        Me.dtpBreakStartTime.Size = New System.Drawing.Size(170, 26)
        Me.dtpBreakStartTime.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.dtpBreakStartTime, "Shift Start Time")
        '
        'dtpSpecialDayBreakEndTime
        '
        Me.dtpSpecialDayBreakEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpSpecialDayBreakEndTime.Location = New System.Drawing.Point(664, 302)
        Me.dtpSpecialDayBreakEndTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpSpecialDayBreakEndTime.Name = "dtpSpecialDayBreakEndTime"
        Me.dtpSpecialDayBreakEndTime.Size = New System.Drawing.Size(180, 26)
        Me.dtpSpecialDayBreakEndTime.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.dtpSpecialDayBreakEndTime, "Shift End Time")
        '
        'dtpSpecialDayBreakStartTime
        '
        Me.dtpSpecialDayBreakStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpSpecialDayBreakStartTime.Location = New System.Drawing.Point(219, 348)
        Me.dtpSpecialDayBreakStartTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpSpecialDayBreakStartTime.Name = "dtpSpecialDayBreakStartTime"
        Me.dtpSpecialDayBreakStartTime.Size = New System.Drawing.Size(170, 26)
        Me.dtpSpecialDayBreakStartTime.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.dtpSpecialDayBreakStartTime, "Shift Start Time")
        '
        'chkShiftNight
        '
        Me.chkShiftNight.AutoSize = True
        Me.chkShiftNight.Location = New System.Drawing.Point(219, 191)
        Me.chkShiftNight.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkShiftNight.Name = "chkShiftNight"
        Me.chkShiftNight.Size = New System.Drawing.Size(109, 24)
        Me.chkShiftNight.TabIndex = 8
        Me.chkShiftNight.Text = "Night Shift"
        Me.ToolTip1.SetToolTip(Me.chkShiftNight, "Shift Status Active Or Inactive")
        Me.chkShiftNight.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(282, 705)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(394, 69)
        Me.lblProgress.TabIndex = 37
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblFlexOutTime
        '
        Me.lblFlexOutTime.AutoSize = True
        Me.lblFlexOutTime.Location = New System.Drawing.Point(454, 431)
        Me.lblFlexOutTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFlexOutTime.Name = "lblFlexOutTime"
        Me.lblFlexOutTime.Size = New System.Drawing.Size(139, 20)
        Me.lblFlexOutTime.TabIndex = 29
        Me.lblFlexOutTime.Text = "Flexibility Out Time"
        '
        'lblFlexInTime
        '
        Me.lblFlexInTime.AutoSize = True
        Me.lblFlexInTime.Location = New System.Drawing.Point(44, 434)
        Me.lblFlexInTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFlexInTime.Name = "lblFlexInTime"
        Me.lblFlexInTime.Size = New System.Drawing.Size(127, 20)
        Me.lblFlexInTime.TabIndex = 27
        Me.lblFlexInTime.Text = "Flexibility In Time"
        '
        'lblOvertimeRate
        '
        Me.lblOvertimeRate.AutoSize = True
        Me.lblOvertimeRate.Location = New System.Drawing.Point(454, 155)
        Me.lblOvertimeRate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblOvertimeRate.Name = "lblOvertimeRate"
        Me.lblOvertimeRate.Size = New System.Drawing.Size(119, 20)
        Me.lblOvertimeRate.TabIndex = 6
        Me.lblOvertimeRate.Text = "Over Time Rate"
        Me.lblOvertimeRate.Visible = False
        '
        'lblSpecialBreakEndTime
        '
        Me.lblSpecialBreakEndTime.AutoSize = True
        Me.lblSpecialBreakEndTime.Location = New System.Drawing.Point(454, 308)
        Me.lblSpecialBreakEndTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSpecialBreakEndTime.Name = "lblSpecialBreakEndTime"
        Me.lblSpecialBreakEndTime.Size = New System.Drawing.Size(178, 20)
        Me.lblSpecialBreakEndTime.TabIndex = 17
        Me.lblSpecialBreakEndTime.Text = "Special Break End Time"
        '
        'lblBreakStartTime
        '
        Me.lblBreakStartTime.AutoSize = True
        Me.lblBreakStartTime.Location = New System.Drawing.Point(44, 272)
        Me.lblBreakStartTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBreakStartTime.Name = "lblBreakStartTime"
        Me.lblBreakStartTime.Size = New System.Drawing.Size(128, 20)
        Me.lblBreakStartTime.TabIndex = 13
        Me.lblBreakStartTime.Text = "Break Start Time"
        '
        'cmbSpecialBreakDay
        '
        Me.cmbSpecialBreakDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpecialBreakDay.FormattingEnabled = True
        Me.cmbSpecialBreakDay.Items.AddRange(New Object() {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Satureday", "", ""})
        Me.cmbSpecialBreakDay.Location = New System.Drawing.Point(219, 306)
        Me.cmbSpecialBreakDay.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbSpecialBreakDay.Name = "cmbSpecialBreakDay"
        Me.cmbSpecialBreakDay.Size = New System.Drawing.Size(170, 28)
        Me.cmbSpecialBreakDay.TabIndex = 16
        '
        'lblBreakEndTime
        '
        Me.lblBreakEndTime.AutoSize = True
        Me.lblBreakEndTime.Location = New System.Drawing.Point(454, 348)
        Me.lblBreakEndTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBreakEndTime.Name = "lblBreakEndTime"
        Me.lblBreakEndTime.Size = New System.Drawing.Size(122, 20)
        Me.lblBreakEndTime.TabIndex = 21
        Me.lblBreakEndTime.Text = "Break End Time"
        '
        'lblBreakDayStartTime
        '
        Me.lblBreakDayStartTime.AutoSize = True
        Me.lblBreakDayStartTime.Location = New System.Drawing.Point(44, 354)
        Me.lblBreakDayStartTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBreakDayStartTime.Name = "lblBreakDayStartTime"
        Me.lblBreakDayStartTime.Size = New System.Drawing.Size(160, 20)
        Me.lblBreakDayStartTime.TabIndex = 19
        Me.lblBreakDayStartTime.Text = "Break Day Start Time"
        '
        'lblBreakDay
        '
        Me.lblBreakDay.AutoSize = True
        Me.lblBreakDay.Location = New System.Drawing.Point(44, 311)
        Me.lblBreakDay.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBreakDay.Name = "lblBreakDay"
        Me.lblBreakDay.Size = New System.Drawing.Size(139, 20)
        Me.lblBreakDay.TabIndex = 15
        Me.lblBreakDay.Text = "Special Break Day"
        '
        'lblOverTimeStartFrom
        '
        Me.lblOverTimeStartFrom.AutoSize = True
        Me.lblOverTimeStartFrom.Location = New System.Drawing.Point(454, 269)
        Me.lblOverTimeStartFrom.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblOverTimeStartFrom.Name = "lblOverTimeStartFrom"
        Me.lblOverTimeStartFrom.Size = New System.Drawing.Size(161, 20)
        Me.lblOverTimeStartFrom.TabIndex = 39
        Me.lblOverTimeStartFrom.Text = "Over Time Start Time "
        '
        'dtpOTStart
        '
        Me.dtpOTStart.Checked = False
        Me.dtpOTStart.CustomFormat = ""
        Me.dtpOTStart.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpOTStart.Location = New System.Drawing.Point(664, 263)
        Me.dtpOTStart.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpOTStart.Name = "dtpOTStart"
        Me.dtpOTStart.ShowCheckBox = True
        Me.dtpOTStart.Size = New System.Drawing.Size(180, 26)
        Me.dtpOTStart.TabIndex = 38
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1028, 57)
        Me.pnlHeader.TabIndex = 40
        '
        'frmDefShift
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(1028, 860)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblOverTimeStartFrom)
        Me.Controls.Add(Me.dtpOTStart)
        Me.Controls.Add(Me.chkShiftNight)
        Me.Controls.Add(Me.lblBreakDay)
        Me.Controls.Add(Me.dtpSpecialDayBreakEndTime)
        Me.Controls.Add(Me.dtpSpecialDayBreakStartTime)
        Me.Controls.Add(Me.lblBreakEndTime)
        Me.Controls.Add(Me.lblBreakDayStartTime)
        Me.Controls.Add(Me.cmbSpecialBreakDay)
        Me.Controls.Add(Me.dtpBreakEndTime)
        Me.Controls.Add(Me.dtpBreakStartTime)
        Me.Controls.Add(Me.lblSpecialBreakEndTime)
        Me.Controls.Add(Me.lblBreakStartTime)
        Me.Controls.Add(Me.txtOverTimeRate)
        Me.Controls.Add(Me.lblOvertimeRate)
        Me.Controls.Add(Me.dtpTxtFlexOut)
        Me.Controls.Add(Me.dtpTxtFlexIn)
        Me.Controls.Add(Me.lblFlexOutTime)
        Me.Controls.Add(Me.lblFlexInTime)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.grdShifttype)
        Me.Controls.Add(Me.lblSortOrder)
        Me.Controls.Add(Me.txtshortorder)
        Me.Controls.Add(Me.chkActive)
        Me.Controls.Add(Me.dtpShiftEndTime)
        Me.Controls.Add(Me.dtpshiftStartTime)
        Me.Controls.Add(Me.dtpshiftendDate)
        Me.Controls.Add(Me.dtpShiftStartDate)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.txtshiftname)
        Me.Controls.Add(Me.txtshiftcode)
        Me.Controls.Add(Me.lblComments)
        Me.Controls.Add(Me.lblShiftEndTime)
        Me.Controls.Add(Me.lblShiftStartTime)
        Me.Controls.Add(Me.lblShiftEndDate)
        Me.Controls.Add(Me.lblStartDate)
        Me.Controls.Add(Me.lblShiftName)
        Me.Controls.Add(Me.lblShiftCode)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmDefShift"
        Me.Text = "Define Shift"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grdShifttype, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents lblShiftCode As System.Windows.Forms.Label
    Friend WithEvents lblShiftName As System.Windows.Forms.Label
    Friend WithEvents lblStartDate As System.Windows.Forms.Label
    Friend WithEvents lblShiftEndDate As System.Windows.Forms.Label
    Friend WithEvents lblShiftStartTime As System.Windows.Forms.Label
    Friend WithEvents lblShiftEndTime As System.Windows.Forms.Label
    Friend WithEvents lblComments As System.Windows.Forms.Label
    Friend WithEvents txtshiftcode As System.Windows.Forms.TextBox
    Friend WithEvents txtshiftname As System.Windows.Forms.TextBox
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents dtpShiftStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpshiftendDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpshiftStartTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpShiftEndTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents txtshortorder As System.Windows.Forms.TextBox
    Friend WithEvents lblSortOrder As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents grdShifttype As Janus.Windows.GridEX.GridEX
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents dtpTxtFlexOut As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTxtFlexIn As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFlexOutTime As System.Windows.Forms.Label
    Friend WithEvents lblFlexInTime As System.Windows.Forms.Label
    Friend WithEvents txtOverTimeRate As System.Windows.Forms.TextBox
    Friend WithEvents lblOvertimeRate As System.Windows.Forms.Label
    Friend WithEvents dtpBreakEndTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpBreakStartTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblSpecialBreakEndTime As System.Windows.Forms.Label
    Friend WithEvents lblBreakStartTime As System.Windows.Forms.Label
    Friend WithEvents cmbSpecialBreakDay As System.Windows.Forms.ComboBox
    Friend WithEvents dtpSpecialDayBreakEndTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpSpecialDayBreakStartTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblBreakEndTime As System.Windows.Forms.Label
    Friend WithEvents lblBreakDayStartTime As System.Windows.Forms.Label
    Friend WithEvents lblBreakDay As System.Windows.Forms.Label
    Friend WithEvents chkShiftNight As System.Windows.Forms.CheckBox
    Friend WithEvents lblOverTimeStartFrom As System.Windows.Forms.Label
    Friend WithEvents dtpOTStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class
