Public Class frmConfigMain

    'CompanyConfig Labels
    Dim lblCompany As Label
    Dim lblSecurityRights As Label
    Dim lblSMS As Label
    Dim lblInfo As Label
    Dim lblPath As Label
    Dim CompanyLabelBit As Integer = 0

    'SalesConfig Labels
    Dim lblSales As Label
    Dim lblSalesAccount As Label
    Dim lblSalesItem As Label
    Dim lblSalesSecurity As Label
    Dim SalesLabelBit As Integer = 0

    'EmailConfig Labels
    Dim lblEmail As Label


    'DBConfig Labels
    Dim lblDB As Label


    'InventoryConfig Labels
    Dim lblInventory As Label
    Dim lblInvAccounts As Label
    Dim lblInvSecurity As Label
    Dim InventoryLabelBit As Integer = 0

    'ApprovalConfig Labels
    Dim lblApproval As Label

    'HRConfig Labels
    Dim lblHR As Label
    Dim lblHRAttendance As Label
    Dim lblHREmployeeAccount As Label
    Dim lblHROvertime As Label
    Dim lblHRProvidentFund As Label
    Dim HRLabelBit As Integer = 0

    'OtherConfig Labels
    Dim lblProperty As Label
    Dim lblProjectManagement As Label
    Dim lblBookingTickets As Label
    Dim PropertyLabelBit As Integer = 0

    'PurchaseConfig Labels
    Dim lblPurchase As Label
    Dim lblPurAccounts As Label
    Dim lblPurSecurity As Label
    Dim PurchaseLabelBit As Integer = 0

    'AccountsConfig Labels
    Dim lblAccounts As Label
    Dim lblAccountsAcc As Label
    Dim lblAccountsSecurity As Label
    Dim AccountsLabelBit As Integer = 0

    'InventoryConfig Labels
    Dim lblImports As Label

    ''Start TFS3763 : Ayesha Rehman
    'BARCodeConfig Labels
    Dim lblBARCode As Label
    ''End TFS3763
    Private Sub frmConfigMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        btnAccounts.FlatAppearance.BorderSize = 0
        btnImports.FlatAppearance.BorderSize = 0
        btnSales.FlatAppearance.BorderSize = 0
        btnPurchase.FlatAppearance.BorderSize = 0
        btnInventory.FlatAppearance.BorderSize = 0
        btnHR.FlatAppearance.BorderSize = 0
        btnCompany.FlatAppearance.BorderSize = 0
        btnEmail.FlatAppearance.BorderSize = 0
        btnDB.FlatAppearance.BorderSize = 0
        btnProperty.FlatAppearance.BorderSize = 0
        btnApproval.FlatAppearance.BorderSize = 0
        btnBarCode.FlatAppearance.BorderSize = 0 ''TFS3764

        frmConfigHome.TopLevel = False
        Me.pnlLoadForm.Controls.Add(frmConfigHome)
        frmConfigHome.Show()
        frmConfigHome.BringToFront()
        Me.pnlLoadForm.VerticalScroll.Enabled = True
        frmConfigHome.FormBorderStyle = FormBorderStyle.None
        frmConfigHome.Dock = DockStyle.Fill

        getAllCobfigLabels()

    End Sub

    Public Sub OpenForm(ByVal formName As Form)

        formName.TopLevel = False
        Me.pnlLoadForm.Controls.Add(formName)
        formName.Show()
        formName.BringToFront()
        Me.pnlLoadForm.VerticalScroll.Enabled = True
        formName.FormBorderStyle = FormBorderStyle.None
        formName.Dock = DockStyle.Fill

    End Sub


    Public Sub getAllCobfigLabels()

        ' Company Config Labels Start

        Me.lblCompany = New Label()
        Me.lblCompany.Text = CompanyConfig.Company
        Me.lblCompany.Tag = CompanyConfig.CompanyTag
        Me.lblCompany.Location = New Point(23, 16)
        Me.lblCompany.AutoSize = True
        Me.lblCompany.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblCompany.ForeColor = Color.Black
        AddHandler Me.lblCompany.Click, AddressOf lblClick
        AddHandler Me.lblCompany.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblCompany.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblCompany)


        Me.lblSecurityRights = New Label()
        Me.lblSecurityRights.Text = CompanyConfig.SecurityRights
        Me.lblSecurityRights.Tag = CompanyConfig.CompanySecurityRightsTag
        Me.lblSecurityRights.Location = New Point(23, 62)   ' yaxis is 16 + 46 = 62 addition in each label
        Me.lblSecurityRights.AutoSize = True
        Me.lblSecurityRights.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblSecurityRights.ForeColor = Color.Black
        AddHandler lblSecurityRights.Click, AddressOf lblClick
        AddHandler Me.lblSecurityRights.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblSecurityRights.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblSecurityRights)

        Me.lblSMS = New Label()
        Me.lblSMS.Text = CompanyConfig.SMS
        Me.lblSMS.Tag = CompanyConfig.CompanySMSTag
        Me.lblSMS.Location = New Point(23, 108)       ' yaxis is 62 + 46 = 62 addition in each label
        Me.lblSMS.AutoSize = True
        Me.lblSMS.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblSMS.ForeColor = Color.Black
        AddHandler Me.lblSMS.Click, AddressOf lblClick
        AddHandler Me.lblSMS.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblSMS.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblSMS)

        Me.lblInfo = New Label()
        Me.lblInfo.Text = CompanyConfig.CompanyInfo
        Me.lblInfo.Tag = CompanyConfig.CompanyInfoTag
        Me.lblInfo.Location = New Point(23, 154)       ' yaxis is 62 + 46 = 62 addition in each label
        Me.lblInfo.AutoSize = True
        Me.lblInfo.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblInfo.ForeColor = Color.Black
        AddHandler Me.lblInfo.Click, AddressOf lblClick
        AddHandler Me.lblInfo.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblInfo.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblInfo)


        Me.lblPath = New Label()
        Me.lblPath.Text = CompanyConfig.CompanyPath
        Me.lblPath.Tag = CompanyConfig.CompanyPathTag
        Me.lblPath.Location = New Point(23, 200)       ' yaxis is 62 + 46 = 62 addition in each label
        Me.lblPath.AutoSize = True
        Me.lblPath.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblPath.ForeColor = Color.Black
        AddHandler Me.lblPath.Click, AddressOf lblClick
        AddHandler Me.lblPath.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblPath.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblPath)

        hideConfigLabels("Company")

        ' Company Config Labels End


        ' Sales Config Labels Start

        Me.lblSales = New Label()
        Me.lblSales.Text = SalesConfig.Sales
        Me.lblSales.Tag = SalesConfig.SalesTag
        Me.lblSales.Location = New Point(23, 16)
        Me.lblSales.AutoSize = True
        Me.lblSales.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblSales.ForeColor = Color.Black
        AddHandler Me.lblSales.Click, AddressOf lblClick
        AddHandler Me.lblSales.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblSales.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblSales)


        Me.lblSalesAccount = New Label()
        Me.lblSalesAccount.Text = SalesConfig.SalesAccount
        Me.lblSalesAccount.Tag = SalesConfig.SalesAccountTag
        Me.lblSalesAccount.Location = New Point(23, 62)   ' yaxis is 16 + 46 = 62 addition in each label
        Me.lblSalesAccount.AutoSize = True
        Me.lblSalesAccount.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblSalesAccount.ForeColor = Color.Black
        AddHandler lblSalesAccount.Click, AddressOf lblClick
        AddHandler Me.lblSalesAccount.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblSalesAccount.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblSalesAccount)

        Me.lblSalesItem = New Label()
        Me.lblSalesItem.Text = SalesConfig.SalesItems
        Me.lblSalesItem.Tag = SalesConfig.SalesItemsTag
        Me.lblSalesItem.Location = New Point(23, 108)     ' yaxis is 16 + 46 = 62 addition in each label
        Me.lblSalesItem.AutoSize = True
        Me.lblSalesItem.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblSalesItem.ForeColor = Color.Black
        AddHandler Me.lblSalesItem.Click, AddressOf lblClick
        AddHandler Me.lblSalesItem.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblSalesItem.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblSalesItem)


        Me.lblSalesSecurity = New Label()
        Me.lblSalesSecurity.Text = SalesConfig.SalesSecurityRights
        Me.lblSalesSecurity.Tag = SalesConfig.SalesSecurityRightsTag
        Me.lblSalesSecurity.Location = New Point(23, 154)     ' yaxis is 16 + 46 = 62 addition in each label
        Me.lblSalesSecurity.AutoSize = True
        Me.lblSalesSecurity.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblSalesSecurity.ForeColor = Color.Black
        AddHandler Me.lblSalesSecurity.Click, AddressOf lblClick
        AddHandler Me.lblSalesSecurity.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblSalesSecurity.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblSalesSecurity)

        hideConfigLabels("Sales")

        ' Sales Config Labels End


        ' Email Config Labels Start

        Me.lblEmail = New Label()
        Me.lblEmail.Text = EmailConfig.Email
        Me.lblEmail.Location = New Point(23, 16)
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblEmail.ForeColor = Color.FromArgb(0, 120, 215)
        pnlSubNav.Controls.Add(Me.lblEmail)

        hideConfigLabels("Email")

        ' Email Config Labels End


        ' DB Config Labels Start

        Me.lblDB = New Label()
        Me.lblDB.Text = DBConfig.DB
        Me.lblDB.Location = New Point(23, 16)
        Me.lblDB.AutoSize = True
        Me.lblDB.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblDB.ForeColor = Color.FromArgb(0, 120, 215)
        pnlSubNav.Controls.Add(Me.lblDB)

        hideConfigLabels("DB")

        ' DB Config Labels End


        ' Inventory Config Labels Start

        Me.lblInventory = New Label()
        Me.lblInventory.Text = InventoryConfig.Inventory
        Me.lblInventory.Tag = InventoryConfig.InventoryTag
        Me.lblInventory.Location = New Point(23, 16)
        Me.lblInventory.AutoSize = True
        Me.lblInventory.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblInventory.ForeColor = Color.Black
        AddHandler Me.lblInventory.Click, AddressOf lblClick
        AddHandler Me.lblInventory.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblInventory.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblInventory)

        Me.lblInvAccounts = New Label()
        Me.lblInvAccounts.Text = InventoryConfig.InventoryAccounts
        Me.lblInvAccounts.Tag = InventoryConfig.InventoryAccountsTag
        Me.lblInvAccounts.Location = New Point(23, 62)
        Me.lblInvAccounts.AutoSize = True
        Me.lblInvAccounts.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblInvAccounts.ForeColor = Color.Black
        AddHandler Me.lblInvAccounts.Click, AddressOf lblClick
        AddHandler Me.lblInvAccounts.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblInvAccounts.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblInvAccounts)


        Me.lblInvSecurity = New Label()
        Me.lblInvSecurity.Text = InventoryConfig.InventorySecurity
        Me.lblInvSecurity.Tag = InventoryConfig.InventorySecurityRightsTag
        Me.lblInvSecurity.Location = New Point(23, 108)
        Me.lblInvSecurity.AutoSize = True
        Me.lblInvSecurity.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblInvSecurity.ForeColor = Color.Black
        AddHandler Me.lblInvSecurity.Click, AddressOf lblClick
        AddHandler Me.lblInvSecurity.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblInvSecurity.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblInvSecurity)

        hideConfigLabels("Inventory")

        'Inventory Config Labels End


        ' Approval Config Labels Start

        Me.lblApproval = New Label()
        Me.lblApproval.Text = ApprovalConfig.Approval
        Me.lblApproval.Location = New Point(23, 16)
        Me.lblApproval.AutoSize = True
        Me.lblApproval.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblApproval.ForeColor = Color.FromArgb(0, 120, 215)
        pnlSubNav.Controls.Add(Me.lblApproval)

        hideConfigLabels("Approval")

        ' Approval Config Labels End



        ' HR Config Labels Start

        Me.lblHR = New Label()
        Me.lblHR.Text = HRConfig.HR
        Me.lblHR.Tag = HRConfig.HRTag
        Me.lblHR.Location = New Point(23, 16)
        Me.lblHR.AutoSize = True
        Me.lblHR.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblHR.ForeColor = Color.Black
        AddHandler Me.lblHR.Click, AddressOf lblClick
        AddHandler Me.lblHR.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblHR.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblHR)

        Me.lblHRAttendance = New Label()
        Me.lblHRAttendance.Text = HRConfig.HRAttendance
        Me.lblHRAttendance.Tag = HRConfig.HRAttendanceTag
        Me.lblHRAttendance.Location = New Point(23, 62)
        Me.lblHRAttendance.AutoSize = True
        Me.lblHRAttendance.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblHRAttendance.ForeColor = Color.Black
        AddHandler Me.lblHRAttendance.Click, AddressOf lblClick
        AddHandler Me.lblHRAttendance.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblHRAttendance.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblHRAttendance)


        Me.lblHREmployeeAccount = New Label()
        Me.lblHREmployeeAccount.Text = HRConfig.HREmployeeAccount
        Me.lblHREmployeeAccount.Tag = HRConfig.HREmployeeAccountTag
        Me.lblHREmployeeAccount.Location = New Point(23, 108)     ' yaxis is 16 + 46 = 62 addition in each label
        Me.lblHREmployeeAccount.AutoSize = True
        Me.lblHREmployeeAccount.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblHREmployeeAccount.ForeColor = Color.Black
        AddHandler Me.lblHREmployeeAccount.Click, AddressOf lblClick
        AddHandler Me.lblHREmployeeAccount.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblHREmployeeAccount.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblHREmployeeAccount)


        Me.lblHROvertime = New Label()
        Me.lblHROvertime.Text = HRConfig.HROvertime
        Me.lblHROvertime.Tag = HRConfig.HROvertimeTag
        Me.lblHROvertime.Location = New Point(23, 154)     ' yaxis is 16 + 46 = 62 addition in each label
        Me.lblHROvertime.AutoSize = True
        Me.lblHROvertime.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblHROvertime.ForeColor = Color.Black
        AddHandler Me.lblHROvertime.Click, AddressOf lblClick
        AddHandler Me.lblHROvertime.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblHROvertime.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblHROvertime)


        Me.lblHRProvidentFund = New Label()
        Me.lblHRProvidentFund.Text = HRConfig.HRProvidentFund
        Me.lblHRProvidentFund.Tag = HRConfig.HRProvidentFundTag
        Me.lblHRProvidentFund.Location = New Point(23, 200)     ' yaxis is 16 + 46 = 62 addition in each label
        Me.lblHRProvidentFund.AutoSize = True
        Me.lblHRProvidentFund.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblHRProvidentFund.ForeColor = Color.Black
        AddHandler Me.lblHRProvidentFund.Click, AddressOf lblClick
        AddHandler Me.lblHRProvidentFund.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblHRProvidentFund.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblHRProvidentFund)

        hideConfigLabels("HR")

        'HR Config Labels End


        ' Property Config Labels Start

        Me.lblProperty = New Label()
        Me.lblProperty.Text = PropertyConfig._Property
        Me.lblProperty.Tag = PropertyConfig.PropertyTag
        Me.lblProperty.Location = New Point(23, 16)
        Me.lblProperty.AutoSize = True
        Me.lblProperty.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblProperty.ForeColor = Color.Black
        AddHandler Me.lblProperty.Click, AddressOf lblClick
        AddHandler Me.lblProperty.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblProperty.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblProperty)

        Me.lblProjectManagement = New Label()
        Me.lblProjectManagement.Text = PropertyConfig.ProjectManagement
        Me.lblProjectManagement.Tag = PropertyConfig.ProjectManagementTag
        Me.lblProjectManagement.Location = New Point(23, 62)
        Me.lblProjectManagement.AutoSize = True
        Me.lblProjectManagement.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblProjectManagement.ForeColor = Color.Black
        AddHandler Me.lblProjectManagement.Click, AddressOf lblClick
        AddHandler Me.lblProjectManagement.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblProjectManagement.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblProjectManagement)

        Me.lblBookingTickets = New Label()
        Me.lblBookingTickets.Text = PropertyConfig.BookingTickets
        Me.lblBookingTickets.Tag = PropertyConfig.BookingTicketsTag
        Me.lblBookingTickets.Location = New Point(23, 108)
        Me.lblBookingTickets.AutoSize = True
        Me.lblBookingTickets.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblBookingTickets.ForeColor = Color.Black
        AddHandler Me.lblBookingTickets.Click, AddressOf lblClick
        AddHandler Me.lblBookingTickets.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblBookingTickets.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblBookingTickets)

        hideConfigLabels("Property")

        ' Property Config Labels End


        ' Purchase Config Labels Start

        Me.lblPurchase = New Label()
        Me.lblPurchase.Text = PurchaseConfig.Purchase
        Me.lblPurchase.Tag = PurchaseConfig.PurchaseTag
        Me.lblPurchase.Location = New Point(23, 16)
        Me.lblPurchase.AutoSize = True
        Me.lblPurchase.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblPurchase.ForeColor = Color.Black
        AddHandler Me.lblPurchase.Click, AddressOf lblClick
        AddHandler Me.lblPurchase.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblPurchase.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblPurchase)

        Me.lblPurAccounts = New Label()
        Me.lblPurAccounts.Text = PurchaseConfig.PurchaseAccounts
        Me.lblPurAccounts.Tag = PurchaseConfig.PurchaseAccountsTag
        Me.lblPurAccounts.Location = New Point(23, 62)
        Me.lblPurAccounts.AutoSize = True
        Me.lblPurAccounts.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblPurAccounts.ForeColor = Color.Black
        AddHandler Me.lblPurAccounts.Click, AddressOf lblClick
        AddHandler Me.lblPurAccounts.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblPurAccounts.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblPurAccounts)


        Me.lblPurSecurity = New Label()
        Me.lblPurSecurity.Text = PurchaseConfig.PurchaseSecurity
        Me.lblPurSecurity.Tag = PurchaseConfig.PurchaseSecurityRightsTag
        Me.lblPurSecurity.Location = New Point(23, 108)
        Me.lblPurSecurity.AutoSize = True
        Me.lblPurSecurity.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblPurSecurity.ForeColor = Color.Black
        AddHandler Me.lblPurSecurity.Click, AddressOf lblClick
        AddHandler Me.lblPurSecurity.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblPurSecurity.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblPurSecurity)

        hideConfigLabels("Purchase")

        'Purchase Config Labels End


        ' Accounts Config Labels Start

        Me.lblAccounts = New Label()
        Me.lblAccounts.Text = AccountsConfig.Accounts
        Me.lblAccounts.Tag = AccountsConfig.AccountsTag
        Me.lblAccounts.Location = New Point(23, 16)
        Me.lblAccounts.AutoSize = True
        Me.lblAccounts.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblAccounts.ForeColor = Color.Black
        AddHandler Me.lblAccounts.Click, AddressOf lblClick
        AddHandler Me.lblAccounts.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblAccounts.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblAccounts)

        Me.lblAccountsAcc = New Label()
        Me.lblAccountsAcc.Text = AccountsConfig.AccountsAcc
        Me.lblAccountsAcc.Tag = AccountsConfig.AccountsAccTag
        Me.lblAccountsAcc.Location = New Point(23, 62)
        Me.lblAccountsAcc.AutoSize = True
        Me.lblAccountsAcc.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblAccountsAcc.ForeColor = Color.Black
        AddHandler Me.lblAccountsAcc.Click, AddressOf lblClick
        AddHandler Me.lblAccountsAcc.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblAccountsAcc.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblAccountsAcc)


        Me.lblAccountsSecurity = New Label()
        Me.lblAccountsSecurity.Text = AccountsConfig.AccountsSecurity
        Me.lblAccountsSecurity.Tag = AccountsConfig.AccountsSecurityRightsTag
        Me.lblAccountsSecurity.Location = New Point(23, 108)
        Me.lblAccountsSecurity.AutoSize = True
        Me.lblAccountsSecurity.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblAccountsSecurity.ForeColor = Color.Black
        AddHandler Me.lblAccountsSecurity.Click, AddressOf lblClick
        AddHandler Me.lblAccountsSecurity.MouseHover, AddressOf lblMouseHover
        AddHandler Me.lblAccountsSecurity.MouseLeave, AddressOf lblMouseLeave
        pnlSubNav.Controls.Add(Me.lblAccountsSecurity)

        hideConfigLabels("Accounts")

        'Accounts Config Labels End


        ' Imports Config Labels Start

        Me.lblImports = New Label()
        Me.lblImports.Text = ImportsConfig._Imports
        Me.lblImports.Location = New Point(23, 16)
        Me.lblImports.AutoSize = True
        Me.lblImports.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblImports.ForeColor = Color.FromArgb(0, 120, 215)
        pnlSubNav.Controls.Add(Me.lblImports)

        hideConfigLabels("Imports")

        ' Imports Config Labels End

        ' BarCode Config Labels Start
        Me.lblBARCode = New Label()
        Me.lblBARCode.Text = BARCodeConfig._BARCode
        Me.lblBARCode.Location = New Point(23, 16)
        Me.lblBARCode.AutoSize = True
        Me.lblBARCode.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
        Me.lblBARCode.ForeColor = Color.FromArgb(0, 120, 215)
        pnlSubNav.Controls.Add(Me.lblBARCode)

        hideConfigLabels("BARCode")

        ' BarCode Config Labels End

    End Sub

    Public Sub hideConfigLabels(ByVal labelConfig As String)

        If labelConfig = "Company" Then

            Me.lblCompany.Visible = False
            Me.lblSecurityRights.Visible = False
            Me.lblSMS.Visible = False
            Me.lblInfo.Visible = False
            Me.lblPath.Visible = False

        ElseIf labelConfig = "Sales" Then

            Me.lblSales.Visible = False
            Me.lblSalesAccount.Visible = False
            Me.lblSalesItem.Visible = False
            Me.lblSalesSecurity.Visible = False

        ElseIf labelConfig = "Email" Then

            Me.lblEmail.Visible = False

        ElseIf labelConfig = "DB" Then

            Me.lblDB.Visible = False

        ElseIf labelConfig = "Inventory" Then

            Me.lblInventory.Visible = False
            Me.lblInvAccounts.Visible = False
            Me.lblInvSecurity.Visible = False

        ElseIf labelConfig = "Approval" Then

            Me.lblApproval.Visible = False

        ElseIf labelConfig = "HR" Then

            Me.lblHR.Visible = False
            Me.lblHRAttendance.Visible = False
            Me.lblHREmployeeAccount.Visible = False
            Me.lblHROvertime.Visible = False
            Me.lblHRProvidentFund.Visible = False

        ElseIf labelConfig = "Property" Then

            Me.lblProperty.Visible = False
            Me.lblProjectManagement.Visible = False
            Me.lblBookingTickets.Visible = False

        ElseIf labelConfig = "Purchase" Then

            Me.lblPurchase.Visible = False
            Me.lblPurAccounts.Visible = False
            Me.lblPurSecurity.Visible = False

        ElseIf labelConfig = "Accounts" Then

            Me.lblAccounts.Visible = False
            Me.lblAccountsAcc.Visible = False
            Me.lblAccountsSecurity.Visible = False

        ElseIf labelConfig = "Imports" Then

            Me.lblImports.Visible = False

        ElseIf labelConfig = "BARCode" Then

            Me.lblBARCode.Visible = False

        End If

    End Sub

    Public Sub showConfigLabels(ByVal labelConfig As String)

        If labelConfig = "Company" Then

            Me.lblCompany.Visible = True
            Me.lblSecurityRights.Visible = True
            Me.lblSMS.Visible = True
            Me.lblInfo.Visible = True
            Me.lblPath.Visible = True

            hideConfigLabels("Sales")
            hideConfigLabels("Email")
            hideConfigLabels("DB")
            hideConfigLabels("Inventory")
            hideConfigLabels("Approval")
            hideConfigLabels("HR")
            hideConfigLabels("Property")
            hideConfigLabels("Purchase")
            hideConfigLabels("Accounts")
            hideConfigLabels("Imports")
            hideConfigLabels("BARCode") ''TFS3763

        ElseIf labelConfig = "Sales" Then

            Me.lblSales.Visible = True
            Me.lblSalesAccount.Visible = True
            Me.lblSalesItem.Visible = True
            Me.lblSalesSecurity.Visible = True

            hideConfigLabels("Company")
            hideConfigLabels("Email")
            hideConfigLabels("DB")
            hideConfigLabels("Inventory")
            hideConfigLabels("Approval")
            hideConfigLabels("HR")
            hideConfigLabels("Property")
            hideConfigLabels("Purchase")
            hideConfigLabels("Accounts")
            hideConfigLabels("Imports")
            hideConfigLabels("BARCode") ''TFS3763

        ElseIf labelConfig = "Email" Then

            Me.lblEmail.Visible = True

            hideConfigLabels("Company")
            hideConfigLabels("Sales")
            hideConfigLabels("DB")
            hideConfigLabels("Inventory")
            hideConfigLabels("Approval")
            hideConfigLabels("HR")
            hideConfigLabels("Property")
            hideConfigLabels("Purchase")
            hideConfigLabels("Accounts")
            hideConfigLabels("Imports")
            hideConfigLabels("BARCode") ''TFS3763

        ElseIf labelConfig = "DB" Then

            Me.lblDB.Visible = True

            hideConfigLabels("Company")
            hideConfigLabels("Sales")
            hideConfigLabels("Email")
            hideConfigLabels("Inventory")
            hideConfigLabels("Approval")
            hideConfigLabels("HR")
            hideConfigLabels("Property")
            hideConfigLabels("Purchase")
            hideConfigLabels("Accounts")
            hideConfigLabels("Imports")
            hideConfigLabels("BARCode") ''TFS3763

        ElseIf labelConfig = "Inventory" Then

            Me.lblInventory.Visible = True
            Me.lblInvAccounts.Visible = True
            Me.lblInvSecurity.Visible = True

            hideConfigLabels("Company")
            hideConfigLabels("Sales")
            hideConfigLabels("Email")
            hideConfigLabels("DB")
            hideConfigLabels("Approval")
            hideConfigLabels("HR")
            hideConfigLabels("Property")
            hideConfigLabels("Purchase")
            hideConfigLabels("Accounts")
            hideConfigLabels("Imports")
            hideConfigLabels("BARCode") ''TFS3763

        ElseIf labelConfig = "Approval" Then

            Me.lblApproval.Visible = True

            hideConfigLabels("Company")
            hideConfigLabels("Sales")
            hideConfigLabels("Email")
            hideConfigLabels("DB")
            hideConfigLabels("Inventory")
            hideConfigLabels("HR")
            hideConfigLabels("Property")
            hideConfigLabels("Purchase")
            hideConfigLabels("Accounts")
            hideConfigLabels("Imports")
            hideConfigLabels("BARCode") ''TFS3763

        ElseIf labelConfig = "HR" Then

            Me.lblHR.Visible = True
            Me.lblHRAttendance.Visible = True
            Me.lblHREmployeeAccount.Visible = True
            Me.lblHROvertime.Visible = True
            Me.lblHRProvidentFund.Visible = True

            hideConfigLabels("Company")
            hideConfigLabels("Sales")
            hideConfigLabels("Email")
            hideConfigLabels("DB")
            hideConfigLabels("Inventory")
            hideConfigLabels("Approval")
            hideConfigLabels("Property")
            hideConfigLabels("Purchase")
            hideConfigLabels("Accounts")
            hideConfigLabels("Imports")
            hideConfigLabels("BARCode") ''TFS3763

        ElseIf labelConfig = "Property" Then

            Me.lblProperty.Visible = True
            Me.lblProjectManagement.Visible = True
            Me.lblBookingTickets.Visible = True

            hideConfigLabels("Company")
            hideConfigLabels("Sales")
            hideConfigLabels("Email")
            hideConfigLabels("DB")
            hideConfigLabels("Inventory")
            hideConfigLabels("Approval")
            hideConfigLabels("HR")
            hideConfigLabels("Purchase")
            hideConfigLabels("Accounts")
            hideConfigLabels("Imports")
            hideConfigLabels("BARCode") ''TFS3763

        ElseIf labelConfig = "Purchase" Then

            Me.lblPurchase.Visible = True
            Me.lblPurAccounts.Visible = True
            Me.lblPurSecurity.Visible = True

            hideConfigLabels("Company")
            hideConfigLabels("Sales")
            hideConfigLabels("Email")
            hideConfigLabels("DB")
            hideConfigLabels("Inventory")
            hideConfigLabels("Approval")
            hideConfigLabels("HR")
            hideConfigLabels("Property")
            hideConfigLabels("Accounts")
            hideConfigLabels("Imports")
            hideConfigLabels("BARCode") ''TFS3763

        ElseIf labelConfig = "Accounts" Then

            Me.lblAccounts.Visible = True
            Me.lblAccountsAcc.Visible = True
            Me.lblAccountsSecurity.Visible = True

            hideConfigLabels("Company")
            hideConfigLabels("Sales")
            hideConfigLabels("Email")
            hideConfigLabels("DB")
            hideConfigLabels("Inventory")
            hideConfigLabels("Approval")
            hideConfigLabels("HR")
            hideConfigLabels("Property")
            hideConfigLabels("Purchase")
            hideConfigLabels("Imports")
            hideConfigLabels("BARCode") ''TFS3763

        ElseIf labelConfig = "Imports" Then

            Me.lblImports.Visible = True

            hideConfigLabels("Company")
            hideConfigLabels("Sales")
            hideConfigLabels("Email")
            hideConfigLabels("DB")
            hideConfigLabels("Inventory")
            hideConfigLabels("Approval")
            hideConfigLabels("HR")
            hideConfigLabels("Property")
            hideConfigLabels("Purchase")
            hideConfigLabels("Accounts")
            hideConfigLabels("BARCode") ''TFS3763

            ''Start TFS3763 
        ElseIf labelConfig = "BarCode" Then

            Me.lblBARCode.Visible = True

            hideConfigLabels("Company")
            hideConfigLabels("Sales")
            hideConfigLabels("Email")
            hideConfigLabels("DB")
            hideConfigLabels("Inventory")
            hideConfigLabels("Approval")
            hideConfigLabels("HR")
            hideConfigLabels("Property")
            hideConfigLabels("Purchase")
            hideConfigLabels("Accounts")
            hideConfigLabels("Imports")
            ''End TFS3763

        End If

    End Sub

    Public Sub lblClick(sender As Object, e As EventArgs)

        Try
            Dim lbl As Windows.Forms.Label
            lbl = CType(sender, Label)

            'Company Labels Click Start

            If lbl.Tag.ToString = CompanyConfig.CompanyTag Then

                Me.CompanyLabelBit = 1

                Me.lblCompany.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblSecurityRights.ForeColor = Color.Black
                Me.lblSMS.ForeColor = Color.Black
                Me.lblInfo.ForeColor = Color.Black
                Me.lblPath.ForeColor = Color.Black

                OpenForm(frmConfigCompany)

            ElseIf lbl.Tag.ToString = CompanyConfig.CompanySecurityRightsTag Then

                Me.CompanyLabelBit = 2

                Me.lblSecurityRights.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblCompany.ForeColor = Color.Black
                Me.lblSMS.ForeColor = Color.Black
                Me.lblInfo.ForeColor = Color.Black
                Me.lblPath.ForeColor = Color.Black

                OpenForm(frmConfigSecurityRights)

            ElseIf lbl.Tag.ToString = CompanyConfig.CompanySMSTag Then

                Me.CompanyLabelBit = 3

                Me.lblSMS.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblCompany.ForeColor = Color.Black
                Me.lblSecurityRights.ForeColor = Color.Black
                Me.lblInfo.ForeColor = Color.Black
                Me.lblPath.ForeColor = Color.Black

                OpenForm(frmConfigSMS)


            ElseIf lbl.Tag.ToString = CompanyConfig.CompanyInfoTag Then

                Me.CompanyLabelBit = 4

                Me.lblSMS.ForeColor = Color.Black
                Me.lblCompany.ForeColor = Color.Black
                Me.lblSecurityRights.ForeColor = Color.Black
                Me.lblInfo.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblPath.ForeColor = Color.Black

                OpenForm(frmConfigCompanyInfo)


            ElseIf lbl.Tag.ToString = CompanyConfig.CompanyPathTag Then

                Me.CompanyLabelBit = 5

                Me.lblSMS.ForeColor = Color.Black
                Me.lblCompany.ForeColor = Color.Black
                Me.lblSecurityRights.ForeColor = Color.Black
                Me.lblInfo.ForeColor = Color.Black
                Me.lblPath.ForeColor = Color.FromArgb(0, 120, 215)

                OpenForm(frmConfigCompanyPath)

                'Company Labels Click End


                'Sales Labels Click Start

            ElseIf lbl.Tag.ToString = SalesConfig.SalesTag Then

                Me.SalesLabelBit = 1

                Me.lblSales.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblSalesAccount.ForeColor = Color.Black
                Me.lblSalesItem.ForeColor = Color.Black
                Me.lblSalesSecurity.ForeColor = Color.Black

                OpenForm(frmConfigSales)

            ElseIf lbl.Tag.ToString = SalesConfig.SalesAccountTag Then

                Me.SalesLabelBit = 2

                Me.lblSales.ForeColor = Color.Black
                Me.lblSalesAccount.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblSalesItem.ForeColor = Color.Black
                Me.lblSalesSecurity.ForeColor = Color.Black

                OpenForm(frmConfigSalesAccount)

            ElseIf lbl.Tag.ToString = SalesConfig.SalesItemsTag Then

                Me.SalesLabelBit = 3

                Me.lblSales.ForeColor = Color.Black
                Me.lblSalesAccount.ForeColor = Color.Black
                Me.lblSalesItem.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblSalesSecurity.ForeColor = Color.Black

                OpenForm(frmConfigSalesItems)

            ElseIf lbl.Tag.ToString = SalesConfig.SalesSecurityRightsTag Then

                Me.SalesLabelBit = 4

                Me.lblSales.ForeColor = Color.Black
                Me.lblSalesAccount.ForeColor = Color.Black
                Me.lblSalesItem.ForeColor = Color.Black
                Me.lblSalesSecurity.ForeColor = Color.FromArgb(0, 120, 215)

                OpenForm(frmconfigSalesSecurity)

                'Sales Labels Click End


                'Inventory Labels Click Start

            ElseIf lbl.Tag.ToString = InventoryConfig.InventoryTag Then

                Me.InventoryLabelBit = 1

                Me.lblInventory.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblInvAccounts.ForeColor = Color.Black
                Me.lblInvSecurity.ForeColor = Color.Black

                OpenForm(frmConfigInventory)

            ElseIf lbl.Tag.ToString = InventoryConfig.InventoryAccountsTag Then

                Me.InventoryLabelBit = 2

                Me.lblInventory.ForeColor = Color.Black
                Me.lblInvAccounts.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblInvSecurity.ForeColor = Color.Black

                OpenForm(frmConfigInvAccounts)

            ElseIf lbl.Tag.ToString = InventoryConfig.InventorySecurityRightsTag Then

                Me.InventoryLabelBit = 3

                Me.lblInventory.ForeColor = Color.Black
                Me.lblInvAccounts.ForeColor = Color.Black
                Me.lblInvSecurity.ForeColor = Color.FromArgb(0, 120, 215)

                OpenForm(frmConfigInvSecurity)

                'Inventory Labels Click End


                'HR Labels Click Start

            ElseIf lbl.Tag.ToString = HRConfig.HRTag Then

                Me.HRLabelBit = 1

                Me.lblHR.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblHRAttendance.ForeColor = Color.Black
                Me.lblHREmployeeAccount.ForeColor = Color.Black
                Me.lblHROvertime.ForeColor = Color.Black
                Me.lblHRProvidentFund.ForeColor = Color.Black

                OpenForm(frmConfigHR)

            ElseIf lbl.Tag.ToString = HRConfig.HRAttendanceTag Then

                Me.HRLabelBit = 2

                Me.lblHR.ForeColor = Color.Black
                Me.lblHRAttendance.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblHREmployeeAccount.ForeColor = Color.Black
                Me.lblHROvertime.ForeColor = Color.Black
                Me.lblHRProvidentFund.ForeColor = Color.Black

                OpenForm(frmConfigHRAttendance)

            ElseIf lbl.Tag.ToString = HRConfig.HREmployeeAccountTag Then

                Me.HRLabelBit = 3

                Me.lblHR.ForeColor = Color.Black
                Me.lblHRAttendance.ForeColor = Color.Black
                Me.lblHREmployeeAccount.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblHROvertime.ForeColor = Color.Black
                Me.lblHRProvidentFund.ForeColor = Color.Black

                OpenForm(frmConfigHREmployeeAccount)

            ElseIf lbl.Tag.ToString = HRConfig.HROvertimeTag Then

                Me.HRLabelBit = 4

                Me.lblHR.ForeColor = Color.Black
                Me.lblHRAttendance.ForeColor = Color.Black
                Me.lblHREmployeeAccount.ForeColor = Color.Black
                Me.lblHROvertime.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblHRProvidentFund.ForeColor = Color.Black

                OpenForm(frmConfigHROvertime)

            ElseIf lbl.Tag.ToString = HRConfig.HRProvidentFundTag Then

                Me.HRLabelBit = 5

                Me.lblHR.ForeColor = Color.Black
                Me.lblHRAttendance.ForeColor = Color.Black
                Me.lblHREmployeeAccount.ForeColor = Color.Black
                Me.lblHROvertime.ForeColor = Color.Black
                Me.lblHRProvidentFund.ForeColor = Color.FromArgb(0, 120, 215)

                OpenForm(frmConfigHRProvidentFund)

                'HR Labels Clic kEnd


                'Purchase Labels Click Start

            ElseIf lbl.Tag.ToString = PurchaseConfig.PurchaseTag Then

                Me.PurchaseLabelBit = 1

                Me.lblPurchase.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblPurAccounts.ForeColor = Color.Black
                Me.lblPurSecurity.ForeColor = Color.Black

                OpenForm(frmConfigPurchase)

            ElseIf lbl.Tag.ToString = PurchaseConfig.PurchaseAccountsTag Then

                Me.PurchaseLabelBit = 2

                Me.lblPurchase.ForeColor = Color.Black
                Me.lblPurAccounts.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblPurSecurity.ForeColor = Color.Black

                OpenForm(frmConfigPurchaseAccount)

            ElseIf lbl.Tag.ToString = PurchaseConfig.PurchaseSecurityRightsTag Then

                Me.PurchaseLabelBit = 3

                Me.lblPurchase.ForeColor = Color.Black
                Me.lblPurAccounts.ForeColor = Color.Black
                Me.lblPurSecurity.ForeColor = Color.FromArgb(0, 120, 215)

                OpenForm(frmConfigPurchaseSecurity)

                'Purchase Labels Click End


                'Accounts Labels Click Start

            ElseIf lbl.Tag.ToString = AccountsConfig.AccountsTag Then

                Me.AccountsLabelBit = 1

                Me.lblAccounts.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblAccountsAcc.ForeColor = Color.Black
                Me.lblAccountsSecurity.ForeColor = Color.Black

                OpenForm(frmConfigAccounts)

            ElseIf lbl.Tag.ToString = AccountsConfig.AccountsAccTag Then

                Me.AccountsLabelBit = 2

                Me.lblAccounts.ForeColor = Color.Black
                Me.lblAccountsAcc.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblAccountsSecurity.ForeColor = Color.Black

                OpenForm(frmConfigAccountsAcc)

            ElseIf lbl.Tag.ToString = AccountsConfig.AccountsSecurityRightsTag Then

                Me.AccountsLabelBit = 3

                Me.lblAccounts.ForeColor = Color.Black
                Me.lblAccountsAcc.ForeColor = Color.Black
                Me.lblAccountsSecurity.ForeColor = Color.FromArgb(0, 120, 215)

                OpenForm(frmConfigAccountsSecurity)

                'Accounts Labels Click End


                'Property Labels Click Start
            ElseIf lbl.Tag.ToString = PropertyConfig.PropertyTag Then

                Me.PropertyLabelBit = 1

                Me.lblProperty.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblProjectManagement.ForeColor = Color.Black
                Me.lblBookingTickets.ForeColor = Color.Black

                OpenForm(frmConfigProperty)

            ElseIf lbl.Tag.ToString = PropertyConfig.ProjectManagementTag Then

                Me.PropertyLabelBit = 2

                Me.lblProperty.ForeColor = Color.Black
                Me.lblProjectManagement.ForeColor = Color.FromArgb(0, 120, 215)
                Me.lblBookingTickets.ForeColor = Color.Black

                OpenForm(frmConfigProjectManagment)


            ElseIf lbl.Tag.ToString = PropertyConfig.BookingTicketsTag Then

                Me.PropertyLabelBit = 3

                Me.lblProperty.ForeColor = Color.Black
                Me.lblProjectManagement.ForeColor = Color.Black
                Me.lblBookingTickets.ForeColor = Color.FromArgb(0, 120, 215)

                OpenForm(frmConfigBookingTickets)

                'Property Labels Click End

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub


    Public Sub lblMouseHover(sender As Object, e As EventArgs)

        Try
            Dim lbl As Windows.Forms.Label
            lbl = CType(sender, Label)

            'Company Labels MouseHover Start

            If lbl.Tag.ToString = CompanyConfig.CompanyTag Then

                Me.lblCompany.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = CompanyConfig.CompanySecurityRightsTag Then

                Me.lblSecurityRights.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = CompanyConfig.CompanySMSTag Then

                Me.lblSMS.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = CompanyConfig.CompanyInfoTag Then

                Me.lblInfo.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = CompanyConfig.CompanyPathTag Then

                Me.lblPath.ForeColor = Color.FromArgb(0, 120, 215)

                'Company Labels MouseHover End


                'Sales Labels MouseHover Start

            ElseIf lbl.Tag.ToString = SalesConfig.SalesTag Then

                Me.lblSales.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = SalesConfig.SalesAccountTag Then

                Me.lblSalesAccount.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = SalesConfig.SalesItemsTag Then

                Me.lblSalesItem.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = SalesConfig.SalesSecurityRightsTag Then

                Me.lblSalesSecurity.ForeColor = Color.FromArgb(0, 120, 215)

                'Sales Labels MouseHover End


                'Inventory Labels MouseHover Start

            ElseIf lbl.Tag.ToString = InventoryConfig.InventoryTag Then

                Me.lblInventory.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = InventoryConfig.InventoryAccountsTag Then

                Me.lblInvAccounts.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = InventoryConfig.InventorySecurityRightsTag Then

                Me.lblInvSecurity.ForeColor = Color.FromArgb(0, 120, 215)

                'Inventory Labels MouseHover End


                'HR Labels MouseHover Start

            ElseIf lbl.Tag.ToString = HRConfig.HRTag Then

                Me.lblHR.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = HRConfig.HRAttendanceTag Then

                Me.lblHRAttendance.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = HRConfig.HREmployeeAccountTag Then

                Me.lblHREmployeeAccount.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = HRConfig.HROvertimeTag Then

                Me.lblHROvertime.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = HRConfig.HRProvidentFundTag Then

                Me.lblHRProvidentFund.ForeColor = Color.FromArgb(0, 120, 215)

                'HR Labels MouseHover End


                'Purchase Labels MouseHover Start

            ElseIf lbl.Tag.ToString = PurchaseConfig.PurchaseTag Then

                Me.lblPurchase.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = PurchaseConfig.PurchaseAccountsTag Then

                Me.lblPurAccounts.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = PurchaseConfig.PurchaseSecurityRightsTag Then

                Me.lblPurSecurity.ForeColor = Color.FromArgb(0, 120, 215)

                'Purchase Labels MouseHover End


                'Accounts Labels MouseHover Start

            ElseIf lbl.Tag.ToString = AccountsConfig.AccountsTag Then

                Me.lblAccounts.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = AccountsConfig.AccountsAccTag Then

                Me.lblAccountsAcc.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = AccountsConfig.AccountsSecurityRightsTag Then

                Me.lblAccountsSecurity.ForeColor = Color.FromArgb(0, 120, 215)

                'Accounts Labels MouseHover End



                'Property Labels MouseHover Start

            ElseIf lbl.Tag.ToString = PropertyConfig.PropertyTag Then

                Me.lblProperty.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = PropertyConfig.ProjectManagementTag Then

                Me.lblProjectManagement.ForeColor = Color.FromArgb(0, 120, 215)

            ElseIf lbl.Tag.ToString = PropertyConfig.BookingTicketsTag Then

                Me.lblBookingTickets.ForeColor = Color.FromArgb(0, 120, 215)

                'Property Labels MouseHover End

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub


    Public Sub lblMouseLeave(sender As Object, e As EventArgs)

        Try
            Dim lbl As Windows.Forms.Label
            lbl = CType(sender, Label)

            'Company Labels MouseLeave Start

            If lbl.Tag.ToString = CompanyConfig.CompanyTag Then

                If Me.CompanyLabelBit = 1 Then
                    Me.lblCompany.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblCompany.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = CompanyConfig.CompanySecurityRightsTag Then

                If Me.CompanyLabelBit = 2 Then
                    Me.lblSecurityRights.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblSecurityRights.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = CompanyConfig.CompanySMSTag Then

                If Me.CompanyLabelBit = 3 Then
                    Me.lblSMS.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblSMS.ForeColor = Color.Black
                End If


            ElseIf lbl.Tag.ToString = CompanyConfig.CompanyInfoTag Then

                If Me.CompanyLabelBit = 4 Then
                    Me.lblInfo.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblInfo.ForeColor = Color.Black
                End If


            ElseIf lbl.Tag.ToString = CompanyConfig.CompanyPathTag Then

                If Me.CompanyLabelBit = 5 Then
                    Me.lblPath.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblPath.ForeColor = Color.Black
                End If

                'Company Labels MouseLeave End


                'Sales Labels MouseLeave Start

            ElseIf lbl.Tag.ToString = SalesConfig.SalesTag Then

                If Me.SalesLabelBit = 1 Then
                    Me.lblSales.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblSales.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = SalesConfig.SalesAccountTag Then

                If Me.SalesLabelBit = 2 Then
                    Me.lblSalesAccount.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblSalesAccount.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = SalesConfig.SalesItemsTag Then

                If Me.SalesLabelBit = 3 Then
                    Me.lblSalesItem.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblSalesItem.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = SalesConfig.SalesSecurityRightsTag Then

                If Me.SalesLabelBit = 4 Then
                    Me.lblSalesSecurity.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblSalesSecurity.ForeColor = Color.Black
                End If

                'Sales Labels MouseLeave End


                'Inventory Labels MouseLeave Start

            ElseIf lbl.Tag.ToString = InventoryConfig.InventoryTag Then

                If Me.InventoryLabelBit = 1 Then
                    Me.lblInventory.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblInventory.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = InventoryConfig.InventoryAccountsTag Then

                If Me.InventoryLabelBit = 2 Then
                    Me.lblInvAccounts.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblInvAccounts.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = InventoryConfig.InventorySecurityRightsTag Then

                If Me.InventoryLabelBit = 3 Then
                    Me.lblInvSecurity.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblInvSecurity.ForeColor = Color.Black
                End If

                'Inventory Labels MouseLeave End


                'HR Labels MouseLeave Start

            ElseIf lbl.Tag.ToString = HRConfig.HRTag Then

                If Me.HRLabelBit = 1 Then
                    Me.lblHR.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblHR.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = HRConfig.HRAttendanceTag Then

                If Me.HRLabelBit = 2 Then
                    Me.lblHRAttendance.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblHRAttendance.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = HRConfig.HREmployeeAccountTag Then

                If Me.HRLabelBit = 3 Then
                    Me.lblHREmployeeAccount.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblHREmployeeAccount.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = HRConfig.HROvertimeTag Then

                If Me.HRLabelBit = 4 Then
                    Me.lblHROvertime.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblHROvertime.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = HRConfig.HRProvidentFundTag Then

                If Me.HRLabelBit = 5 Then
                    Me.lblHRProvidentFund.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblHRProvidentFund.ForeColor = Color.Black
                End If

                'HR Labels MouseLeave End


                'Purchase Labels MouseLeave Start

            ElseIf lbl.Tag.ToString = PurchaseConfig.PurchaseTag Then

                If Me.PurchaseLabelBit = 1 Then
                    Me.lblPurchase.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblPurchase.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = PurchaseConfig.PurchaseAccountsTag Then

                If Me.PurchaseLabelBit = 2 Then
                    Me.lblPurAccounts.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblPurAccounts.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = PurchaseConfig.PurchaseSecurityRightsTag Then

                If Me.PurchaseLabelBit = 3 Then
                    Me.lblPurSecurity.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblPurSecurity.ForeColor = Color.Black
                End If

                'Purchase Labels MouseLeave End


                'Accounts Labels MouseLeave Start

            ElseIf lbl.Tag.ToString = AccountsConfig.AccountsTag Then

                If Me.AccountsLabelBit = 1 Then
                    Me.lblAccounts.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblAccounts.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = AccountsConfig.AccountsAccTag Then

                If Me.AccountsLabelBit = 2 Then
                    Me.lblAccountsAcc.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblAccountsAcc.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = AccountsConfig.AccountsSecurityRightsTag Then

                If Me.AccountsLabelBit = 3 Then
                    Me.lblAccountsSecurity.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblAccountsSecurity.ForeColor = Color.Black
                End If

                'Accounts Labels MouseLeave End

                'Property Labels MouseLeave Start

            ElseIf lbl.Tag.ToString = PropertyConfig.PropertyTag Then

                If Me.PropertyLabelBit = 1 Then
                    Me.lblProperty.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblProperty.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = PropertyConfig.ProjectManagementTag Then

                If Me.PropertyLabelBit = 2 Then
                    Me.lblProjectManagement.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblProjectManagement.ForeColor = Color.Black
                End If

            ElseIf lbl.Tag.ToString = PropertyConfig.BookingTicketsTag Then

                If Me.PropertyLabelBit = 3 Then
                    Me.lblBookingTickets.ForeColor = Color.FromArgb(0, 120, 215)
                Else
                    Me.lblBookingTickets.ForeColor = Color.Black
                End If

                'Property Labels MouseLeave End

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    'Company Configuration Start
    Private Sub btnCompany_Click(sender As Object, e As EventArgs) Handles btnCompany.Click

        pnlSubNav.Visible = True

        Me.lblCompany.ForeColor = Color.FromArgb(0, 120, 215)
        Me.lblSecurityRights.ForeColor = Color.Black
        Me.lblSMS.ForeColor = Color.Black
        Me.lblInfo.ForeColor = Color.Black
        Me.lblPath.ForeColor = Color.Black

        Me.CompanyLabelBit = 1

        showConfigLabels("Company")

        OpenForm(frmConfigCompany)

    End Sub

    'Company Configuration End


    'Sales Configuration Start
    Private Sub btnSales_Click(sender As Object, e As EventArgs) Handles btnSales.Click
        pnlSubNav.Visible = True

        Me.lblSales.ForeColor = Color.FromArgb(0, 120, 215)
        Me.lblSalesAccount.ForeColor = Color.Black

        Me.SalesLabelBit = 1

        showConfigLabels("Sales")


        OpenForm(frmConfigSales)

    End Sub

    'Sales Config End


    'Email Configuration Start
    Private Sub btnEmail_Click(sender As Object, e As EventArgs) Handles btnEmail.Click
        pnlSubNav.Visible = True

        showConfigLabels("Email")

        OpenForm(frmConfigEmail)

    End Sub

    'Email Configuration End


    'DB Configuration Start

    Private Sub btnDB_Click(sender As Object, e As EventArgs) Handles btnDB.Click

        pnlSubNav.Visible = True

        showConfigLabels("DB")

        OpenForm(frmConfigDB)

    End Sub

    'DB Configuration End


    'Inventory Configuration Start

    Private Sub btnInventory_Click(sender As Object, e As EventArgs) Handles btnInventory.Click
        pnlSubNav.Visible = True

        Me.lblInventory.ForeColor = Color.FromArgb(0, 120, 215)
        Me.lblInvAccounts.ForeColor = Color.Black
        Me.lblInvSecurity.ForeColor = Color.Black

        Me.InventoryLabelBit = 1

        showConfigLabels("Inventory")

        OpenForm(frmConfigInventory)

    End Sub

    'Inventory Configuration End

    Private Sub btnApproval_Click(sender As Object, e As EventArgs) Handles btnApproval.Click

        pnlSubNav.Visible = True

        showConfigLabels("Approval")

        OpenForm(frmConfigApproval)

    End Sub

    Private Sub btnHR_Click(sender As Object, e As EventArgs) Handles btnHR.Click

        pnlSubNav.Visible = True

        Me.lblHR.ForeColor = Color.FromArgb(0, 120, 215)
        Me.lblHRAttendance.ForeColor = Color.Black
        Me.lblHREmployeeAccount.ForeColor = Color.Black
        Me.lblHROvertime.ForeColor = Color.Black
        Me.lblHRProvidentFund.ForeColor = Color.Black

        Me.HRLabelBit = 1

        showConfigLabels("HR")

        OpenForm(frmConfigHR)

    End Sub

    Private Sub btnProperty_Click(sender As Object, e As EventArgs) Handles btnProperty.Click

        pnlSubNav.Visible = True

        Me.lblProperty.ForeColor = Color.FromArgb(0, 120, 215)
        Me.lblProjectManagement.ForeColor = Color.Black

        Me.PropertyLabelBit = 1

        showConfigLabels("Property")

        OpenForm(frmConfigProperty)

    End Sub

    Private Sub btnPurchase_Click(sender As Object, e As EventArgs) Handles btnPurchase.Click

        pnlSubNav.Visible = True

        Me.lblPurchase.ForeColor = Color.FromArgb(0, 120, 215)
        Me.lblPurAccounts.ForeColor = Color.Black
        Me.lblPurSecurity.ForeColor = Color.Black

        Me.PurchaseLabelBit = 1

        showConfigLabels("Purchase")

        OpenForm(frmConfigPurchase)

    End Sub

    Private Sub btnAccounts_Click(sender As Object, e As EventArgs) Handles btnAccounts.Click
        pnlSubNav.Visible = True

        Me.lblAccounts.ForeColor = Color.FromArgb(0, 120, 215)
        Me.lblAccountsAcc.ForeColor = Color.Black
        Me.lblAccountsSecurity.ForeColor = Color.Black

        Me.AccountsLabelBit = 1

        showConfigLabels("Accounts")

        OpenForm(frmConfigAccounts)

    End Sub

    Private Sub btnImports_Click(sender As Object, e As EventArgs) Handles btnImports.Click
        'pnlSubNav.Visible = True

        'showConfigLabels("Imports")

        'OpenForm(frmConfigImports)
    End Sub

    Private Sub btnBarCode_Click(sender As Object, e As EventArgs) Handles btnBarCode.Click
        Try
            pnlSubNav.Visible = True

            showConfigLabels("BarCode")

            OpenForm(frmConfigBarcode)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class

' CompanyConfig Class
Public Class CompanyConfig

    Public Shared Company As String = "General"
    Public Shared SecurityRights As String = "Security Rights"
    Public Shared SMS As String = "SMS"
    Public Shared CompanyInfo As String = "Company Info"
    Public Shared CompanyPath As String = "Path Setting"

    Public Shared CompanyTag = "Company"
    Public Shared CompanySecurityRightsTag = "CompanySecurityRights"
    Public Shared CompanySMSTag = "CompanySMS"
    Public Shared CompanyInfoTag = "CompanyInfo"
    Public Shared CompanyPathTag = "CompanyPath"

End Class

' SalesConfig Class
Public Class SalesConfig

    Public Shared Sales As String = "General"
    Public Shared SalesAccount As String = "Account"
    Public Shared SalesItems As String = "Items"
    Public Shared SalesSecurityRights As String = "Security"

    Public Shared SalesTag = "Sale"
    Public Shared SalesAccountTag = "SalesAccount"
    Public Shared SalesItemsTag = "SalesItems"
    Public Shared SalesSecurityRightsTag = "SalesSecurityRights"

End Class

' EmailConfig Class
Public Class EmailConfig

    Public Shared Email As String = "Email"

End Class

' DBConfig Class
Public Class DBConfig

    Public Shared DB As String = "DataBase Backup"

End Class


' InventoryConfig Class
Public Class InventoryConfig

    Public Shared Inventory As String = "General"
    Public Shared InventoryAccounts As String = "Accounts"
    Public Shared InventorySecurity As String = "Security"

    Public Shared InventoryTag = "Inventory"
    Public Shared InventoryAccountsTag = "InventoryAccounts"
    Public Shared InventorySecurityRightsTag = "InventorySecurityRights"

End Class

'Approval Class

Public Class ApprovalConfig

    Public Shared Approval As String = "Approval"

End Class

'HR Class
Public Class HRConfig

    Public Shared HR As String = "General"
    Public Shared HRAttendance As String = "Attendance"
    Public Shared HREmployeeAccount As String = "Account"
    Public Shared HROvertime As String = "Overtime"
    Public Shared HRProvidentFund As String = "ProvidentFund"

    Public Shared HRTag = "HR"
    Public Shared HRAttendanceTag = "HRAccounts"
    Public Shared HREmployeeAccountTag = "HRAccount"
    Public Shared HROvertimeTag = "HROvertime"
    Public Shared HRProvidentFundTag = "HRProvidentFund"

End Class



'Property Class

Public Class PropertyConfig

    Public Shared _Property As String = "Property"
    Public Shared ProjectManagement As String = "Project Management"
    Public Shared BookingTickets As String = "Ticket Booking"

    Public Shared PropertyTag = "PropertyTag"
    Public Shared ProjectManagementTag = "ProjectManagementTag"
    Public Shared BookingTicketsTag = "BookingTicketsTag"



End Class
'BARCode Class

Public Class BARCodeConfig

    Public Shared _BARCode As String = "BAR Code"

End Class

' PurchaseConfig Class
Public Class PurchaseConfig

    Public Shared Purchase As String = "General"
    Public Shared PurchaseAccounts As String = "Accounts"
    Public Shared PurchaseSecurity As String = "Security"

    Public Shared PurchaseTag = "Purchase"
    Public Shared PurchaseAccountsTag = "PurchaseAccounts"
    Public Shared PurchaseSecurityRightsTag = "PurchaseSecurityRights"

End Class


' AccountsConfig Class
Public Class AccountsConfig

    Public Shared Accounts As String = "General"
    Public Shared AccountsAcc As String = "Accounts"
    Public Shared AccountsSecurity As String = "Security"

    Public Shared AccountsTag = "Accounts"
    Public Shared AccountsAccTag = "AccountsAcc"
    Public Shared AccountsSecurityRightsTag = "AccountsSecurityRights"

End Class


'Inventory Class

Public Class ImportsConfig

    Public Shared _Imports As String = "Imports"

End Class