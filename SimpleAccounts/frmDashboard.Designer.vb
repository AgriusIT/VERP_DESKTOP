<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDashboard
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
        Me.bgwCash = New System.ComponentModel.BackgroundWorker()
        Me.bgwSalePurchase = New System.ComponentModel.BackgroundWorker()
        Me.bgwExpense = New System.ComponentModel.BackgroundWorker()
        Me.bgwPayableReceivable = New System.ComponentModel.BackgroundWorker()
        Me.bgwSMSBalance = New System.ComponentModel.BackgroundWorker()
        Me.bgwPostDatedCheque = New System.ComponentModel.BackgroundWorker()
        Me.bgwAttendance = New System.ComponentModel.BackgroundWorker()
        Me.bgwTasks = New System.ComponentModel.BackgroundWorker()
        Me.bgwStockLevel = New System.ComponentModel.BackgroundWorker()
        Me.bgwNotificationCount = New System.ComponentModel.BackgroundWorker()
        Me.tmrAlerts = New System.Windows.Forms.Timer(Me.components)
        Me.bgwUpdateNotifications = New System.ComponentModel.BackgroundWorker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblStockValue = New System.Windows.Forms.Label()
        Me.lstNotifications = New System.Windows.Forms.ListView()
        Me.lblStockCount = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.lblTotalTasks = New System.Windows.Forms.Label()
        Me.lblUpcomingTasks = New System.Windows.Forms.Label()
        Me.lblOverdueTasks = New System.Windows.Forms.Label()
        Me.lblTodayTasks = New System.Windows.Forms.Label()
        Me.lblTotalAttendance = New System.Windows.Forms.Label()
        Me.lblPresent = New System.Windows.Forms.Label()
        Me.lblAbsent = New System.Windows.Forms.Label()
        Me.pnlDateRange = New System.Windows.Forms.Panel()
        Me.chkIncludeTaxAmount = New System.Windows.Forms.CheckBox()
        Me.chkIncludeUnpostedVoucher = New System.Windows.Forms.CheckBox()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.lblchequetoday = New System.Windows.Forms.Label()
        Me.lblchequeTommorow = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.pnlApp = New System.Windows.Forms.Panel()
        Me.pnlAlert = New System.Windows.Forms.Panel()
        Me.lblNotificationCount = New System.Windows.Forms.Label()
        Me.pnlRefresh = New System.Windows.Forms.Panel()
        Me.pnlDate = New System.Windows.Forms.Panel()
        Me.lblReceivable = New System.Windows.Forms.Label()
        Me.lblPayable = New System.Windows.Forms.Label()
        Me.lblSMSBalance = New System.Windows.Forms.Label()
        Me.lblExpense = New System.Windows.Forms.Label()
        Me.lblTotalPurchase = New System.Windows.Forms.Label()
        Me.lblPurchaseReturn = New System.Windows.Forms.Label()
        Me.lblPurchase = New System.Windows.Forms.Label()
        Me.lblTotalSales = New System.Windows.Forms.Label()
        Me.lblSalesReturn = New System.Windows.Forms.Label()
        Me.lblSales = New System.Windows.Forms.Label()
        Me.lblTotalPayment = New System.Windows.Forms.Label()
        Me.lblTotalReceipt = New System.Windows.Forms.Label()
        Me.lblTotalBalance = New System.Windows.Forms.Label()
        Me.lblBankPayment = New System.Windows.Forms.Label()
        Me.lblBankBalance = New System.Windows.Forms.Label()
        Me.lblBankReceipt = New System.Windows.Forms.Label()
        Me.lblCashPayment = New System.Windows.Forms.Label()
        Me.lblCashReceipt = New System.Windows.Forms.Label()
        Me.lblCashBalance = New System.Windows.Forms.Label()
        Me.bgwStockValue = New System.ComponentModel.BackgroundWorker()
        Me.Panel1.SuspendLayout()
        Me.pnlDateRange.SuspendLayout()
        Me.pnlAlert.SuspendLayout()
        Me.SuspendLayout()
        '
        'bgwCash
        '
        '
        'bgwSalePurchase
        '
        '
        'bgwExpense
        '
        '
        'bgwPayableReceivable
        '
        '
        'bgwSMSBalance
        '
        '
        'bgwPostDatedCheque
        '
        '
        'bgwAttendance
        '
        '
        'bgwTasks
        '
        '
        'bgwStockLevel
        '
        '
        'bgwNotificationCount
        '
        '
        'tmrAlerts
        '
        '
        'bgwUpdateNotifications
        '
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.SimpleAccounts.My.Resources.Resources.dashboard
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.lblStockValue)
        Me.Panel1.Controls.Add(Me.lstNotifications)
        Me.Panel1.Controls.Add(Me.lblStockCount)
        Me.Panel1.Controls.Add(Me.ComboBox1)
        Me.Panel1.Controls.Add(Me.lblTotalTasks)
        Me.Panel1.Controls.Add(Me.lblUpcomingTasks)
        Me.Panel1.Controls.Add(Me.lblOverdueTasks)
        Me.Panel1.Controls.Add(Me.lblTodayTasks)
        Me.Panel1.Controls.Add(Me.lblTotalAttendance)
        Me.Panel1.Controls.Add(Me.lblPresent)
        Me.Panel1.Controls.Add(Me.lblAbsent)
        Me.Panel1.Controls.Add(Me.pnlDateRange)
        Me.Panel1.Controls.Add(Me.lblchequetoday)
        Me.Panel1.Controls.Add(Me.lblchequeTommorow)
        Me.Panel1.Controls.Add(Me.txtSearch)
        Me.Panel1.Controls.Add(Me.pnlApp)
        Me.Panel1.Controls.Add(Me.pnlAlert)
        Me.Panel1.Controls.Add(Me.pnlRefresh)
        Me.Panel1.Controls.Add(Me.pnlDate)
        Me.Panel1.Controls.Add(Me.lblReceivable)
        Me.Panel1.Controls.Add(Me.lblPayable)
        Me.Panel1.Controls.Add(Me.lblSMSBalance)
        Me.Panel1.Controls.Add(Me.lblExpense)
        Me.Panel1.Controls.Add(Me.lblTotalPurchase)
        Me.Panel1.Controls.Add(Me.lblPurchaseReturn)
        Me.Panel1.Controls.Add(Me.lblPurchase)
        Me.Panel1.Controls.Add(Me.lblTotalSales)
        Me.Panel1.Controls.Add(Me.lblSalesReturn)
        Me.Panel1.Controls.Add(Me.lblSales)
        Me.Panel1.Controls.Add(Me.lblTotalPayment)
        Me.Panel1.Controls.Add(Me.lblTotalReceipt)
        Me.Panel1.Controls.Add(Me.lblTotalBalance)
        Me.Panel1.Controls.Add(Me.lblBankPayment)
        Me.Panel1.Controls.Add(Me.lblBankBalance)
        Me.Panel1.Controls.Add(Me.lblBankReceipt)
        Me.Panel1.Controls.Add(Me.lblCashPayment)
        Me.Panel1.Controls.Add(Me.lblCashReceipt)
        Me.Panel1.Controls.Add(Me.lblCashBalance)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(918, 562)
        Me.Panel1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(120, 536)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(132, 17)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Available Stock Value"
        '
        'lblStockValue
        '
        Me.lblStockValue.BackColor = System.Drawing.Color.Transparent
        Me.lblStockValue.ForeColor = System.Drawing.Color.White
        Me.lblStockValue.Location = New System.Drawing.Point(253, 536)
        Me.lblStockValue.Name = "lblStockValue"
        Me.lblStockValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblStockValue.Size = New System.Drawing.Size(158, 17)
        Me.lblStockValue.TabIndex = 37
        Me.lblStockValue.Text = "0"
        '
        'lstNotifications
        '
        Me.lstNotifications.Location = New System.Drawing.Point(61, 55)
        Me.lstNotifications.Name = "lstNotifications"
        Me.lstNotifications.Size = New System.Drawing.Size(795, 245)
        Me.lstNotifications.TabIndex = 7
        Me.lstNotifications.UseCompatibleStateImageBehavior = False
        Me.lstNotifications.View = System.Windows.Forms.View.List
        '
        'lblStockCount
        '
        Me.lblStockCount.BackColor = System.Drawing.Color.Transparent
        Me.lblStockCount.ForeColor = System.Drawing.Color.White
        Me.lblStockCount.Location = New System.Drawing.Point(551, 524)
        Me.lblStockCount.Name = "lblStockCount"
        Me.lblStockCount.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblStockCount.Size = New System.Drawing.Size(32, 17)
        Me.lblStockCount.TabIndex = 35
        Me.lblStockCount.Text = "0"
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Minimum", "Optimal", "Maximum"})
        Me.ComboBox1.Location = New System.Drawing.Point(503, 500)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(80, 21)
        Me.ComboBox1.TabIndex = 34
        '
        'lblTotalTasks
        '
        Me.lblTotalTasks.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalTasks.ForeColor = System.Drawing.Color.White
        Me.lblTotalTasks.Location = New System.Drawing.Point(551, 432)
        Me.lblTotalTasks.Name = "lblTotalTasks"
        Me.lblTotalTasks.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTotalTasks.Size = New System.Drawing.Size(70, 17)
        Me.lblTotalTasks.TabIndex = 33
        Me.lblTotalTasks.Text = "0"
        '
        'lblUpcomingTasks
        '
        Me.lblUpcomingTasks.BackColor = System.Drawing.Color.Transparent
        Me.lblUpcomingTasks.ForeColor = System.Drawing.Color.White
        Me.lblUpcomingTasks.Location = New System.Drawing.Point(551, 402)
        Me.lblUpcomingTasks.Name = "lblUpcomingTasks"
        Me.lblUpcomingTasks.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblUpcomingTasks.Size = New System.Drawing.Size(70, 17)
        Me.lblUpcomingTasks.TabIndex = 32
        Me.lblUpcomingTasks.Text = "0"
        '
        'lblOverdueTasks
        '
        Me.lblOverdueTasks.BackColor = System.Drawing.Color.Transparent
        Me.lblOverdueTasks.ForeColor = System.Drawing.Color.White
        Me.lblOverdueTasks.Location = New System.Drawing.Point(551, 385)
        Me.lblOverdueTasks.Name = "lblOverdueTasks"
        Me.lblOverdueTasks.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblOverdueTasks.Size = New System.Drawing.Size(70, 17)
        Me.lblOverdueTasks.TabIndex = 31
        Me.lblOverdueTasks.Text = "0"
        '
        'lblTodayTasks
        '
        Me.lblTodayTasks.BackColor = System.Drawing.Color.Transparent
        Me.lblTodayTasks.ForeColor = System.Drawing.Color.White
        Me.lblTodayTasks.Location = New System.Drawing.Point(551, 368)
        Me.lblTodayTasks.Name = "lblTodayTasks"
        Me.lblTodayTasks.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTodayTasks.Size = New System.Drawing.Size(70, 17)
        Me.lblTodayTasks.TabIndex = 30
        Me.lblTodayTasks.Text = "0"
        '
        'lblTotalAttendance
        '
        Me.lblTotalAttendance.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalAttendance.ForeColor = System.Drawing.Color.White
        Me.lblTotalAttendance.Location = New System.Drawing.Point(734, 427)
        Me.lblTotalAttendance.Name = "lblTotalAttendance"
        Me.lblTotalAttendance.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTotalAttendance.Size = New System.Drawing.Size(71, 17)
        Me.lblTotalAttendance.TabIndex = 29
        Me.lblTotalAttendance.Text = "0"
        '
        'lblPresent
        '
        Me.lblPresent.BackColor = System.Drawing.Color.Transparent
        Me.lblPresent.ForeColor = System.Drawing.Color.White
        Me.lblPresent.Location = New System.Drawing.Point(734, 394)
        Me.lblPresent.Name = "lblPresent"
        Me.lblPresent.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblPresent.Size = New System.Drawing.Size(71, 17)
        Me.lblPresent.TabIndex = 28
        Me.lblPresent.Text = "0"
        '
        'lblAbsent
        '
        Me.lblAbsent.BackColor = System.Drawing.Color.Transparent
        Me.lblAbsent.ForeColor = System.Drawing.Color.White
        Me.lblAbsent.Location = New System.Drawing.Point(734, 368)
        Me.lblAbsent.Name = "lblAbsent"
        Me.lblAbsent.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblAbsent.Size = New System.Drawing.Size(70, 17)
        Me.lblAbsent.TabIndex = 27
        Me.lblAbsent.Text = "0"
        '
        'pnlDateRange
        '
        Me.pnlDateRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDateRange.Controls.Add(Me.chkIncludeTaxAmount)
        Me.pnlDateRange.Controls.Add(Me.chkIncludeUnpostedVoucher)
        Me.pnlDateRange.Controls.Add(Me.btnShow)
        Me.pnlDateRange.Controls.Add(Me.Label1)
        Me.pnlDateRange.Controls.Add(Me.lblFromDate)
        Me.pnlDateRange.Controls.Add(Me.dtpToDate)
        Me.pnlDateRange.Controls.Add(Me.dtpFromDate)
        Me.pnlDateRange.Location = New System.Drawing.Point(657, 56)
        Me.pnlDateRange.Name = "pnlDateRange"
        Me.pnlDateRange.Size = New System.Drawing.Size(199, 226)
        Me.pnlDateRange.TabIndex = 26
        '
        'chkIncludeTaxAmount
        '
        Me.chkIncludeTaxAmount.AutoSize = True
        Me.chkIncludeTaxAmount.Location = New System.Drawing.Point(60, 90)
        Me.chkIncludeTaxAmount.Name = "chkIncludeTaxAmount"
        Me.chkIncludeTaxAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkIncludeTaxAmount.Size = New System.Drawing.Size(123, 17)
        Me.chkIncludeTaxAmount.TabIndex = 6
        Me.chkIncludeTaxAmount.Text = "Incl. Tax Amount"
        Me.chkIncludeTaxAmount.UseVisualStyleBackColor = True
        '
        'chkIncludeUnpostedVoucher
        '
        Me.chkIncludeUnpostedVoucher.AutoSize = True
        Me.chkIncludeUnpostedVoucher.Checked = True
        Me.chkIncludeUnpostedVoucher.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeUnpostedVoucher.Location = New System.Drawing.Point(21, 68)
        Me.chkIncludeUnpostedVoucher.Name = "chkIncludeUnpostedVoucher"
        Me.chkIncludeUnpostedVoucher.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkIncludeUnpostedVoucher.Size = New System.Drawing.Size(162, 17)
        Me.chkIncludeUnpostedVoucher.TabIndex = 5
        Me.chkIncludeUnpostedVoucher.Text = " Incl. Unposted Voucher"
        Me.chkIncludeUnpostedVoucher.UseVisualStyleBackColor = True
        '
        'btnShow
        '
        Me.btnShow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnShow.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShow.Location = New System.Drawing.Point(133, 191)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(51, 23)
        Me.btnShow.TabIndex = 4
        Me.btnShow.Text = "Apply"
        Me.btnShow.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(20, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "To"
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Location = New System.Drawing.Point(13, 18)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(36, 13)
        Me.lblFromDate.TabIndex = 2
        Me.lblFromDate.Text = "From"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(63, 41)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(121, 21)
        Me.dtpToDate.TabIndex = 1
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(63, 14)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(121, 21)
        Me.dtpFromDate.TabIndex = 0
        '
        'lblchequetoday
        '
        Me.lblchequetoday.BackColor = System.Drawing.Color.Transparent
        Me.lblchequetoday.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchequetoday.ForeColor = System.Drawing.SystemColors.Window
        Me.lblchequetoday.Location = New System.Drawing.Point(737, 148)
        Me.lblchequetoday.Name = "lblchequetoday"
        Me.lblchequetoday.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblchequetoday.Size = New System.Drawing.Size(91, 17)
        Me.lblchequetoday.TabIndex = 25
        Me.lblchequetoday.Text = "0"
        '
        'lblchequeTommorow
        '
        Me.lblchequeTommorow.BackColor = System.Drawing.Color.Transparent
        Me.lblchequeTommorow.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchequeTommorow.ForeColor = System.Drawing.SystemColors.Window
        Me.lblchequeTommorow.Location = New System.Drawing.Point(737, 131)
        Me.lblchequeTommorow.Name = "lblchequeTommorow"
        Me.lblchequeTommorow.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblchequeTommorow.Size = New System.Drawing.Size(91, 17)
        Me.lblchequeTommorow.TabIndex = 24
        Me.lblchequeTommorow.Text = "0"
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.DarkGray
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(154, 17)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(211, 16)
        Me.txtSearch.TabIndex = 22
        '
        'pnlApp
        '
        Me.pnlApp.BackColor = System.Drawing.Color.Transparent
        Me.pnlApp.Location = New System.Drawing.Point(808, 13)
        Me.pnlApp.Name = "pnlApp"
        Me.pnlApp.Size = New System.Drawing.Size(21, 21)
        Me.pnlApp.TabIndex = 20
        '
        'pnlAlert
        '
        Me.pnlAlert.BackColor = System.Drawing.Color.Transparent
        Me.pnlAlert.Controls.Add(Me.lblNotificationCount)
        Me.pnlAlert.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlAlert.Location = New System.Drawing.Point(778, 13)
        Me.pnlAlert.Name = "pnlAlert"
        Me.pnlAlert.Size = New System.Drawing.Size(21, 21)
        Me.pnlAlert.TabIndex = 21
        '
        'lblNotificationCount
        '
        Me.lblNotificationCount.AutoSize = True
        Me.lblNotificationCount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblNotificationCount.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotificationCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNotificationCount.Location = New System.Drawing.Point(0, 0)
        Me.lblNotificationCount.Name = "lblNotificationCount"
        Me.lblNotificationCount.Size = New System.Drawing.Size(0, 16)
        Me.lblNotificationCount.TabIndex = 0
        Me.lblNotificationCount.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'pnlRefresh
        '
        Me.pnlRefresh.BackColor = System.Drawing.Color.Transparent
        Me.pnlRefresh.Location = New System.Drawing.Point(751, 13)
        Me.pnlRefresh.Name = "pnlRefresh"
        Me.pnlRefresh.Size = New System.Drawing.Size(21, 21)
        Me.pnlRefresh.TabIndex = 20
        '
        'pnlDate
        '
        Me.pnlDate.BackColor = System.Drawing.Color.Transparent
        Me.pnlDate.Location = New System.Drawing.Point(724, 13)
        Me.pnlDate.Name = "pnlDate"
        Me.pnlDate.Size = New System.Drawing.Size(21, 21)
        Me.pnlDate.TabIndex = 19
        '
        'lblReceivable
        '
        Me.lblReceivable.BackColor = System.Drawing.Color.Transparent
        Me.lblReceivable.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceivable.ForeColor = System.Drawing.SystemColors.Window
        Me.lblReceivable.Location = New System.Drawing.Point(740, 253)
        Me.lblReceivable.Name = "lblReceivable"
        Me.lblReceivable.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblReceivable.Size = New System.Drawing.Size(91, 17)
        Me.lblReceivable.TabIndex = 18
        Me.lblReceivable.Text = "0"
        '
        'lblPayable
        '
        Me.lblPayable.BackColor = System.Drawing.Color.Transparent
        Me.lblPayable.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayable.ForeColor = System.Drawing.SystemColors.Window
        Me.lblPayable.Location = New System.Drawing.Point(740, 230)
        Me.lblPayable.Name = "lblPayable"
        Me.lblPayable.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblPayable.Size = New System.Drawing.Size(91, 17)
        Me.lblPayable.TabIndex = 17
        Me.lblPayable.Text = "0"
        '
        'lblSMSBalance
        '
        Me.lblSMSBalance.BackColor = System.Drawing.Color.Transparent
        Me.lblSMSBalance.ForeColor = System.Drawing.SystemColors.Window
        Me.lblSMSBalance.Location = New System.Drawing.Point(724, 525)
        Me.lblSMSBalance.Name = "lblSMSBalance"
        Me.lblSMSBalance.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblSMSBalance.Size = New System.Drawing.Size(104, 17)
        Me.lblSMSBalance.TabIndex = 16
        Me.lblSMSBalance.Text = "0"
        '
        'lblExpense
        '
        Me.lblExpense.BackColor = System.Drawing.Color.Transparent
        Me.lblExpense.ForeColor = System.Drawing.SystemColors.Window
        Me.lblExpense.Location = New System.Drawing.Point(598, 525)
        Me.lblExpense.Name = "lblExpense"
        Me.lblExpense.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblExpense.Size = New System.Drawing.Size(110, 17)
        Me.lblExpense.TabIndex = 15
        Me.lblExpense.Text = "0"
        '
        'lblTotalPurchase
        '
        Me.lblTotalPurchase.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalPurchase.ForeColor = System.Drawing.SystemColors.Window
        Me.lblTotalPurchase.Location = New System.Drawing.Point(330, 455)
        Me.lblTotalPurchase.Name = "lblTotalPurchase"
        Me.lblTotalPurchase.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTotalPurchase.Size = New System.Drawing.Size(107, 13)
        Me.lblTotalPurchase.TabIndex = 14
        Me.lblTotalPurchase.Text = "0"
        '
        'lblPurchaseReturn
        '
        Me.lblPurchaseReturn.BackColor = System.Drawing.Color.Transparent
        Me.lblPurchaseReturn.ForeColor = System.Drawing.SystemColors.Window
        Me.lblPurchaseReturn.Location = New System.Drawing.Point(330, 424)
        Me.lblPurchaseReturn.Name = "lblPurchaseReturn"
        Me.lblPurchaseReturn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblPurchaseReturn.Size = New System.Drawing.Size(107, 13)
        Me.lblPurchaseReturn.TabIndex = 13
        Me.lblPurchaseReturn.Text = "0"
        '
        'lblPurchase
        '
        Me.lblPurchase.BackColor = System.Drawing.Color.Transparent
        Me.lblPurchase.ForeColor = System.Drawing.SystemColors.Window
        Me.lblPurchase.Location = New System.Drawing.Point(330, 394)
        Me.lblPurchase.Name = "lblPurchase"
        Me.lblPurchase.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblPurchase.Size = New System.Drawing.Size(107, 13)
        Me.lblPurchase.TabIndex = 12
        Me.lblPurchase.Text = "0"
        '
        'lblTotalSales
        '
        Me.lblTotalSales.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalSales.ForeColor = System.Drawing.SystemColors.Window
        Me.lblTotalSales.Location = New System.Drawing.Point(131, 455)
        Me.lblTotalSales.Name = "lblTotalSales"
        Me.lblTotalSales.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTotalSales.Size = New System.Drawing.Size(107, 13)
        Me.lblTotalSales.TabIndex = 11
        Me.lblTotalSales.Text = "0"
        '
        'lblSalesReturn
        '
        Me.lblSalesReturn.BackColor = System.Drawing.Color.Transparent
        Me.lblSalesReturn.ForeColor = System.Drawing.SystemColors.Window
        Me.lblSalesReturn.Location = New System.Drawing.Point(131, 424)
        Me.lblSalesReturn.Name = "lblSalesReturn"
        Me.lblSalesReturn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblSalesReturn.Size = New System.Drawing.Size(107, 13)
        Me.lblSalesReturn.TabIndex = 10
        Me.lblSalesReturn.Text = "0"
        '
        'lblSales
        '
        Me.lblSales.BackColor = System.Drawing.Color.Transparent
        Me.lblSales.ForeColor = System.Drawing.SystemColors.Window
        Me.lblSales.Location = New System.Drawing.Point(131, 394)
        Me.lblSales.Name = "lblSales"
        Me.lblSales.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblSales.Size = New System.Drawing.Size(107, 13)
        Me.lblSales.TabIndex = 9
        Me.lblSales.Text = "0"
        '
        'lblTotalPayment
        '
        Me.lblTotalPayment.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalPayment.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPayment.ForeColor = System.Drawing.SystemColors.Window
        Me.lblTotalPayment.Location = New System.Drawing.Point(483, 217)
        Me.lblTotalPayment.Name = "lblTotalPayment"
        Me.lblTotalPayment.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTotalPayment.Size = New System.Drawing.Size(107, 13)
        Me.lblTotalPayment.TabIndex = 8
        Me.lblTotalPayment.Text = "0"
        '
        'lblTotalReceipt
        '
        Me.lblTotalReceipt.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalReceipt.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalReceipt.ForeColor = System.Drawing.SystemColors.Window
        Me.lblTotalReceipt.Location = New System.Drawing.Point(483, 186)
        Me.lblTotalReceipt.Name = "lblTotalReceipt"
        Me.lblTotalReceipt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTotalReceipt.Size = New System.Drawing.Size(107, 13)
        Me.lblTotalReceipt.TabIndex = 7
        Me.lblTotalReceipt.Text = "0"
        '
        'lblTotalBalance
        '
        Me.lblTotalBalance.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalBalance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalBalance.ForeColor = System.Drawing.SystemColors.Window
        Me.lblTotalBalance.Location = New System.Drawing.Point(483, 252)
        Me.lblTotalBalance.Name = "lblTotalBalance"
        Me.lblTotalBalance.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTotalBalance.Size = New System.Drawing.Size(107, 13)
        Me.lblTotalBalance.TabIndex = 6
        Me.lblTotalBalance.Text = "0"
        '
        'lblBankPayment
        '
        Me.lblBankPayment.BackColor = System.Drawing.Color.Transparent
        Me.lblBankPayment.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankPayment.ForeColor = System.Drawing.SystemColors.Window
        Me.lblBankPayment.Location = New System.Drawing.Point(321, 217)
        Me.lblBankPayment.Name = "lblBankPayment"
        Me.lblBankPayment.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblBankPayment.Size = New System.Drawing.Size(107, 13)
        Me.lblBankPayment.TabIndex = 5
        Me.lblBankPayment.Text = "0"
        '
        'lblBankBalance
        '
        Me.lblBankBalance.BackColor = System.Drawing.Color.Transparent
        Me.lblBankBalance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankBalance.ForeColor = System.Drawing.SystemColors.Window
        Me.lblBankBalance.Location = New System.Drawing.Point(321, 252)
        Me.lblBankBalance.Name = "lblBankBalance"
        Me.lblBankBalance.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblBankBalance.Size = New System.Drawing.Size(107, 13)
        Me.lblBankBalance.TabIndex = 4
        Me.lblBankBalance.Text = "0"
        '
        'lblBankReceipt
        '
        Me.lblBankReceipt.BackColor = System.Drawing.Color.Transparent
        Me.lblBankReceipt.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankReceipt.ForeColor = System.Drawing.SystemColors.Window
        Me.lblBankReceipt.Location = New System.Drawing.Point(321, 186)
        Me.lblBankReceipt.Name = "lblBankReceipt"
        Me.lblBankReceipt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblBankReceipt.Size = New System.Drawing.Size(107, 13)
        Me.lblBankReceipt.TabIndex = 3
        Me.lblBankReceipt.Text = "0"
        '
        'lblCashPayment
        '
        Me.lblCashPayment.BackColor = System.Drawing.Color.Transparent
        Me.lblCashPayment.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCashPayment.ForeColor = System.Drawing.SystemColors.Window
        Me.lblCashPayment.Location = New System.Drawing.Point(169, 217)
        Me.lblCashPayment.Name = "lblCashPayment"
        Me.lblCashPayment.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblCashPayment.Size = New System.Drawing.Size(107, 13)
        Me.lblCashPayment.TabIndex = 2
        Me.lblCashPayment.Text = "0"
        '
        'lblCashReceipt
        '
        Me.lblCashReceipt.BackColor = System.Drawing.Color.Transparent
        Me.lblCashReceipt.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCashReceipt.ForeColor = System.Drawing.SystemColors.Window
        Me.lblCashReceipt.Location = New System.Drawing.Point(169, 186)
        Me.lblCashReceipt.Name = "lblCashReceipt"
        Me.lblCashReceipt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblCashReceipt.Size = New System.Drawing.Size(107, 13)
        Me.lblCashReceipt.TabIndex = 1
        Me.lblCashReceipt.Text = "0"
        '
        'lblCashBalance
        '
        Me.lblCashBalance.BackColor = System.Drawing.Color.Transparent
        Me.lblCashBalance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCashBalance.ForeColor = System.Drawing.SystemColors.Window
        Me.lblCashBalance.Location = New System.Drawing.Point(169, 252)
        Me.lblCashBalance.Name = "lblCashBalance"
        Me.lblCashBalance.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblCashBalance.Size = New System.Drawing.Size(107, 13)
        Me.lblCashBalance.TabIndex = 0
        Me.lblCashBalance.Text = "0"
        '
        'bgwStockValue
        '
        '
        'frmDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(919, 562)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmDashboard"
        Me.Text = "Dashboard"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlDateRange.ResumeLayout(False)
        Me.pnlDateRange.PerformLayout()
        Me.pnlAlert.ResumeLayout(False)
        Me.pnlAlert.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents bgwCash As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblCashBalance As System.Windows.Forms.Label
    Friend WithEvents lblTotalPayment As System.Windows.Forms.Label
    Friend WithEvents lblTotalReceipt As System.Windows.Forms.Label
    Friend WithEvents lblTotalBalance As System.Windows.Forms.Label
    Friend WithEvents lblBankPayment As System.Windows.Forms.Label
    Friend WithEvents lblBankBalance As System.Windows.Forms.Label
    Friend WithEvents lblBankReceipt As System.Windows.Forms.Label
    Friend WithEvents lblCashPayment As System.Windows.Forms.Label
    Friend WithEvents lblCashReceipt As System.Windows.Forms.Label
    Friend WithEvents bgwSalePurchase As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblSales As System.Windows.Forms.Label
    Friend WithEvents lblTotalSales As System.Windows.Forms.Label
    Friend WithEvents lblSalesReturn As System.Windows.Forms.Label
    Friend WithEvents lblTotalPurchase As System.Windows.Forms.Label
    Friend WithEvents lblPurchaseReturn As System.Windows.Forms.Label
    Friend WithEvents lblPurchase As System.Windows.Forms.Label
    Friend WithEvents lblSMSBalance As System.Windows.Forms.Label
    Friend WithEvents lblExpense As System.Windows.Forms.Label
    Friend WithEvents bgwExpense As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwPayableReceivable As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblReceivable As System.Windows.Forms.Label
    Friend WithEvents lblPayable As System.Windows.Forms.Label
    Friend WithEvents bgwSMSBalance As System.ComponentModel.BackgroundWorker
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents pnlApp As System.Windows.Forms.Panel
    Friend WithEvents pnlAlert As System.Windows.Forms.Panel
    Friend WithEvents pnlRefresh As System.Windows.Forms.Panel
    Friend WithEvents pnlDate As System.Windows.Forms.Panel
    Friend WithEvents lblchequetoday As System.Windows.Forms.Label
    Friend WithEvents lblchequeTommorow As System.Windows.Forms.Label
    Friend WithEvents pnlDateRange As System.Windows.Forms.Panel
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkIncludeTaxAmount As System.Windows.Forms.CheckBox
    Friend WithEvents chkIncludeUnpostedVoucher As System.Windows.Forms.CheckBox
    Friend WithEvents bgwPostDatedCheque As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwAttendance As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblTotalAttendance As System.Windows.Forms.Label
    Friend WithEvents lblPresent As System.Windows.Forms.Label
    Friend WithEvents lblAbsent As System.Windows.Forms.Label
    Friend WithEvents bgwTasks As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblUpcomingTasks As System.Windows.Forms.Label
    Friend WithEvents lblOverdueTasks As System.Windows.Forms.Label
    Friend WithEvents lblTodayTasks As System.Windows.Forms.Label
    Friend WithEvents lblTotalTasks As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents lblStockCount As System.Windows.Forms.Label
    Friend WithEvents bgwStockLevel As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblNotificationCount As System.Windows.Forms.Label
    Friend WithEvents bgwNotificationCount As System.ComponentModel.BackgroundWorker
    Friend WithEvents tmrAlerts As System.Windows.Forms.Timer
    Friend WithEvents bgwUpdateNotifications As System.ComponentModel.BackgroundWorker
    Friend WithEvents lstNotifications As System.Windows.Forms.ListView
    Friend WithEvents lblStockValue As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents bgwStockValue As System.ComponentModel.BackgroundWorker
End Class
