Imports SBModel
Imports SBDal
Imports System.Data.SqlClient
Imports SBUtility.Utility
Imports SBDal.StockDAL
Imports SBDal.StockDocTypeDAL
Imports System.Data.OleDb
'Imports System.Speech.Synt
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports Infragistics.Win.UltraWinTabControl
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win
'07-July-2017: Task # TFS1042: Waqar Raza: Import to check the Maximum value of JobCardId
Imports System.Linq
Imports System
Imports System.Data


Public Class frmContractOpening
    Implements IGeneral

    Public StoreIssue As Boolean
    Dim arrFile As List(Of String)
    Public ItemsConsumption As Boolean
    Dim LocationId As Integer = 0
    Public dtMerging As DataTable
    Public IsWIPAccount As Boolean = False
    Public PlanId As Integer = 0
    Public TicketId As Integer = 0
    Public ContractId As Integer = 0
    Dim DurationofMonth As String = ""
    Dim InvoicePattern As String = ""
    Dim dtinvoicedetails As New DataTable
    'Public CostCenterId As Integer = 0

    'Sub New(ByVal frmStoreIssue As Boolean, frmItemsConsumption As Boolean, ByVal LocationId As Integer)
    '    Try
    '        InitializeComponent()
    '        Me.StoreIssue = frmStoreIssue
    '        Me.ItemsConsumption = frmItemsConsumption
    '        Me.LocationId = LocationId
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Function IsValidate(Optional ByVal Mode As EnumDataMode = EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            'Rafay:Task Start:To check serial no if serial no is dublicate then show error
            If CheckDuplicateSerialNo() = True Then
                Me.grdItems.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub grdItems_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs)

    End Sub
    Private Sub dtpDate_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub grdItems_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs)

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            If Condition = "Customer" Then
                str = "SELECT coa_detail_id, detail_title from vwCOADetail  where account_type = 'customer' ORDER BY detail_title"
                FillUltraDropDown(cmbCustomer, str, True)
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns("detail_title").Width = 300
            ElseIf Condition = "Opportunity" Then
                str = "SELECT tblDefOpportunity.OpportunityId, tblDefOpportunity.DocNo, tblDefOpportunity.SupportTypeId, tblDefOpportunity.MaintenanceId, tblDefOpportunityMaintenance.Title AS PreventionMaintenance, tblDefOpportunitySupportType.Title AS SLAType, tblDefOpportunity.StartDate, tblDefOpportunityType.Title as Status, tblDefOpportunityPayment.Title AS PaymentTerms, tblDefOpportunityFrequency.Title AS InvoicingFrequency, tblDefOpportunity.OpportunityOwner, tblDefOpportunity.CurrencyId,tblDefOpportunity.ChkBoxBatteriesIncluded,tblDefOpportunity.TotalAmount, tblDefOpportunity.DurationofMonth, tblDefOpportunity.InvoicePattern, tblDefOpportunity.ArticleId, tblDefOpportunity.TaxAmount FROM tblDefOpportunityMaintenance RIGHT OUTER JOIN tblDefOpportunity LEFT OUTER JOIN tblDefOpportunityFrequency ON tblDefOpportunity.FrequencyId = tblDefOpportunityFrequency.Id LEFT OUTER JOIN tblDefOpportunityPayment ON tblDefOpportunity.PaymentId = tblDefOpportunityPayment.Id LEFT OUTER JOIN tblDefOpportunityType ON tblDefOpportunity.TypeId = tblDefOpportunityType.Id LEFT OUTER JOIN tblDefOpportunitySupportType ON tblDefOpportunity.SupportTypeId = tblDefOpportunitySupportType.Id ON tblDefOpportunityMaintenance.Id = tblDefOpportunity.MaintenanceId where tblDefOpportunity.OpportunityType = 'Support' and tblDefOpportunity.StageId = 8 and tblDefOpportunity.DocNo not in (SELECT OpportunityId from ContractMasterTable)"
                FillUltraDropDown(cmbOpportunity, str, True)
                Me.cmbOpportunity.DisplayLayout.Bands(0).Columns("OpportunityId").Hidden = True
                Me.cmbOpportunity.DisplayLayout.Bands(0).Columns("DocNo").Width = 300
                Me.cmbOpportunity.DisplayLayout.Bands(0).Columns("SupportTypeId").Hidden = True
                Me.cmbOpportunity.DisplayLayout.Bands(0).Columns("MaintenanceId").Hidden = True
            ElseIf Condition = "Item" Then
                str = "SELECT ArticleId AS Id, ArticleCode AS Code, ArticleDescription AS Item FROM ArticleDefView Where ArticleDefView.Active=1  and ArticleDefView.ArticleGroupId = 8"
                FillUltraDropDown(Me.cmbItem, str, True)
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "Currency" Then
                str = "select currency_id AS Id , currency_code AS Currency From tblCurrency "
                FillDropDown(Me.cmbCurrency, str, False)
            ElseIf Condition = "Employee" Then
                str = "SELECT User_ID, FULLNAME from tblUSER "
                FillDropDown(Me.cmbEmployee, str, True)
                'Rafay
            ElseIf Condition = "PreviousContracts" Then
                str = "select Top 1 ContractId,ContractNo From ContractMasterTable where CustomerId = N'" & Me.cmbCustomer.Text & "' order by ContractMasterTable.StartDate Desc "
                FillDropDown(Me.cmbRenew, str, True)
                'Rafay
            
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = String.Empty
            Dim strFilter As String = String.Empty
            ''//Get all and Get top 50//'' TASK # 975
            'Ali Faisal : TFS1606 : Added CompanyId column in history
            'Rafay:add user rights agar tou login user admin hai tou sab record show karay ga aur agar tou normal user hai tou else wali str show karay ga
            'Rafay :add 2 column (ContractStatus,PreviousContracts,isnull(ChkBoxBatteriesIncluded,0) As Batteries Included) 
            If LoginUser.LoginUserGroup = "Administrator" Then
                str = "SELECT ContractMasterTable.ContractId, ContractMasterTable.ContractNo, ContractMasterTable.StartDate, ContractMasterTable.EndDate, ContractMasterTable.SLAType, ContractMasterTable.PreventionMaintenance, ContractMasterTable.CustomerId, ContractMasterTable.OpportunityId, ContractMasterTable.Status, ContractMasterTable.EndCustomer, ContractMasterTable.PONumber, ContractMasterTable.ContractType, ContractMasterTable.ContactofNotification, ContractMasterTable.Site, ContractMasterTable.InvoicingFrequency, ContractMasterTable.PaymentTerms, ContractMasterTable.Comments, ContractMasterTable.Employee , IsNull(Doc_Att.[No Of Attachment],0) as  [No Of Attachment], ContractMasterTable.Amount, ContractMasterTable.Currency, ContractMasterTable.ContractStatus, ContractMasterTable.PreviousContracts,TerminateStatus,GETDATE() as CurrentDate,isnull(HoldReason,'') as HoldReason,isnull(OthersDescription,'') as OthersDescription,isnull(HoldCheckBox,0) as HoldCheckBox,isnull(ChkBoxBatteriesIncluded,0) As BatteriesIncluded, ArticleId, DurationofMonth,InvoicePattern, Tax FROM ContractMasterTable LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId,source) Doc_Att on Doc_Att.DocId = ContractMasterTable.ContractId  order by ContractMasterTable.ContractId DESC "
                'str = "SELECT ContractMasterTable.ContractId, ContractMasterTable.ContractNo, ContractMasterTable.StartDate, ContractMasterTable.EndDate, ContractMasterTable.SLAType, ContractMasterTable.PreventionMaintenance, ContractMasterTable.CustomerId, ContractMasterTable.OpportunityId, ContractMasterTable.Status, ContractMasterTable.EndCustomer, ContractMasterTable.PONumber, ContractMasterTable.ContractType, ContractMasterTable.ContactofNotification, ContractMasterTable.Site, ContractMasterTable.InvoicingFrequency, ContractMasterTable.PaymentTerms, ContractMasterTable.Comments, ContractMasterTable.Employee , IsNull(Doc_Att.[No Of Attachment],0) as  [No Of Attachment], ContractMasterTable.Amount, ContractMasterTable.Currency, ContractMasterTable.ContractStatus, ContractMasterTable.PreviousContracts,TerminateStatus,GETDATE() as CurrentDate,ContractMasterTable.HoldReason,ContractMasterTable.OthersDescription,ContractMasterTable.HoldCheckBox as HoldCheckBox  FROM ContractMasterTable LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId,source) Doc_Att on Doc_Att.DocId = ContractMasterTable.ContractId  order by ContractMasterTable.ContractId DESC "
            Else
                str = "SELECT ContractMasterTable.ContractId, ContractMasterTable.ContractNo, ContractMasterTable.StartDate, ContractMasterTable.EndDate, ContractMasterTable.SLAType, ContractMasterTable.PreventionMaintenance, ContractMasterTable.CustomerId, ContractMasterTable.OpportunityId, ContractMasterTable.Status, ContractMasterTable.EndCustomer, ContractMasterTable.PONumber, ContractMasterTable.ContractType, ContractMasterTable.ContactofNotification, ContractMasterTable.Site, ContractMasterTable.InvoicingFrequency, ContractMasterTable.PaymentTerms, ContractMasterTable.Comments, ContractMasterTable.Employee , IsNull(Doc_Att.[No Of Attachment],0) as  [No Of Attachment], ContractMasterTable.Amount, ContractMasterTable.Currency, ContractMasterTable.ContractStatus, ContractMasterTable.PreviousContracts,TerminateStatus,GETDATE() as CurrentDate,isnull(HoldReason,'') as HoldReason,isnull(OthersDescription,'') as OthersDescription,isnull(HoldCheckBox,0) as HoldCheckBox,isnull(ChkBoxBatteriesIncluded,0) As BatteriesIncluded, ArticleId, DurationofMonth,InvoicePattern, Tax  FROM ContractMasterTable LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId,source) Doc_Att on Doc_Att.DocId = ContractMasterTable.ContractId where ContractStatus <> 'Terminate' order by ContractMasterTable.ContractId DESC"
            End If
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            '    dt.Columns("ContractStatus").Expression = "IIF(EndDate < CurrentDate,'Expired',ContractStatus)"
            Me.GrdStatus.DataSource = dt
            Me.GrdStatus.RetrieveStructure()
            Me.ApplyGridSettings()
            Me.GrdStatus.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.GrdStatus.RootTable.Columns("No Of Attachment").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            GrdStatus.RootTable.Columns("ContractId").Visible = False
            GrdStatus.RootTable.Columns("CurrentDate").Visible = False
            GrdStatus.RootTable.Columns("Amount").Visible = False
            'GrdStatus.RootTable.Columns("CustomerId").Visible = False
            'GrdStatus.RootTable.Columns("OpportunityId").Visible = False
            CtrlGrdBar2_Load(Nothing, Nothing)
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            arrFile = New List(Of String)
            grdItems.DataSource = Nothing
            FillCombos("Customer")
            FillCombos("Opportunity")
            FillCombos("Item")
            'FillCombos("PreviousContracts")
            If Me.cmbCustomer.Rows.Count > 0 Then cmbCustomer.Rows(0).Activate()
            If Me.cmbOpportunity.Rows.Count > 0 Then cmbOpportunity.Rows(0).Activate()
            If Me.cmbItem.Rows.Count > 0 Then cmbItem.Rows(0).Activate()
            dtpFromDate.Value = Date.Now
            dtpToDate.Value = Date.Now
            txtPreventionMaintenance.Text = ""
            txtSLAType.Text = ""
            txtStatus.Text = ""
            txtEndCustomer.Text = ""
            txtPONumber.Text = ""
            txtAmount.Text = ""
            txtTax.Text = ""
            cmbCurrency.SelectedIndex = 0
            txtContactofNotifictaion.Text = ""
            cmbSite.SelectedIndex = 0
            cmbSLAType.SelectedIndex = 0
            cmbPreventionMaintenance.SelectedIndex = 0
            cmbOpportunityStatus.SelectedIndex = 0
            cmbPaymentTerms.SelectedIndex = 0
            cmbInvoiceFrequency.SelectedIndex = 0
            cmbDurationofMonth.SelectedIndex = 0
            txtInvoicingFrequency.Text = ""
            txtPaymentTems.Text = ""
            'rafay reset attachment when click new 
            ToolStripButton1.Text = "Attachment (" & arrFile.Count & ")"
            ''companyinitials = ""
            'If Me.cmbRenew.Items.Count > 1 Then Me.cmbRenew.SelectedIndex = 1 Else Me.cmbRenew.SelectedIndex = 0
            'If Me.cmbStatus.Items.Count > 1 Then Me.cmbStatus.SelectedIndex = 1 Else Me.cmbStatus.SelectedIndex = 0
            'cmbRenew.SelectedIndex = 0
            'cmbStatus.SelectedIndex = 0
            cmbRenew.SelectedText = ""
            cmbStatus.SelectedIndex = -1
            'cmbRenew.SelectedIndex = -1
            cmbterminate.SelectedIndex = -1
            cmbHold.SelectedIndex = -1
            txtOthersDescription.Text = ""
            If HoldCheckbox.Checked = True Then
                HoldCheckbox.Checked = False
            End If
            If ChkBatteriesIncluded.Checked = True Then
                ChkBatteriesIncluded.Checked = False
            End If
            'rafay
            txtComments.Text = ""
            cmbEmployee.SelectedIndex = 0
            btnSave.Text = "&Save"
            GetAllRecords()
            Dim newdt As New DataTable
            newdt.Columns.Add("InvoiceDate")
            newdt.Columns.Add("InvoiceAmount")
            'dtinvoicedetails.Columns.Remove("InvoiceDate")
            'dtinvoicedetails.Columns.Remove("InvoiceAmount")
            'dtinvoicedetails.Columns.Add("InvoiceDate")
            'dtinvoicedetails.Columns.Add("InvoiceAmount")
            grdInvoiceDetails.DataSource = newdt
            cmbCustomer.Value = frmCustomerCollection.cmbAccounts.Value
            Me.txtContractNo.Text = GetDocumentNo()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Rafay Created this function to get serial no 
    Function GetDocumentNo() As String
        Try
            Dim PreFix As String = ""
            Dim CompanyWisePrefix As Boolean = False
            CompanyWisePrefix = Convert.ToBoolean(getConfigValueByType("ShowCompanyWisePrefix").ToString)
            If CompanyPrefix = "V-ERP (UAE)" Then
                'companyinitials = "UE"
                Return GetNextDocNo("MC-" & Format(Me.dtpFromDate.Value, "yy") & Me.dtpFromDate.Value.Month.ToString("00") & Me.dtpFromDate.Value.Day.ToString("00"), 4, "ContractMasterTable", "ContractNo")
            Else
                ''companyinitials = "PK"
                Return GetNextDocNo("MC-" & companyinitials & "-" & Format(Me.dtpFromDate.Value, "yy") & Me.dtpFromDate.Value.Month.ToString("00") & Me.dtpFromDate.Value.Day.ToString("00"), 4, "ContractMasterTable", "ContractNo")
            End If
            If CompanyPrefix = "V-ERP (UAE)" Then
                'companyinitials = "UE"
                Return GetNextDocNo("MC-" & Format(Me.dtpFromDate.Value, "yy") & Me.dtpFromDate.Value.Month.ToString("00") & Me.dtpFromDate.Value.Day.ToString("00"), 4, "ContractMasterTable", "ContractNo")
            Else
                ''companyinitials = "PK"
                Return GetNextDocNo("MC-" & companyinitials & "-" & Format(Me.dtpFromDate.Value, "yy") & Me.dtpFromDate.Value.Month.ToString("00") & Me.dtpFromDate.Value.Day.ToString("00"), 4, "ContractMasterTable", "ContractNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub frmTicketProducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            FillCombos("Customer")
            FillCombos("Opportunity")
            FillCombos("Item")
            FillCombos("Invoice")
            FillCombos("Currency")
            FillCombos("Employee")
            'rafay
            Terminate.Visible = False
            cmbRenew.Visible = False
            cmbterminate.Visible = False
            PreviousContracts.Visible = False
            ''HoldCheckbox.Visible = False
            cmbHold.Visible = False
            lblOthersDescription.Visible = False
            txtOthersDescription.Visible = False
            'txtCurrentDate.Text = DateTime.Now.ToString("dd/MM/yyyy")
            'rafay
            UltraDropDownSearching(cmbOpportunity, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbCustomer, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            dtinvoicedetails.Columns.Add("InvoiceDate")
            dtinvoicedetails.Columns.Add("InvoiceAmount")
            grdInvoiceDetails.DataSource = dtinvoicedetails
            ReSetControls()
            GetAllRecords()
            'grdInvoiceDetails.DataSource = dtinvoicedetails
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub


    Private Sub btnSearch_Click(sender As Object, e As EventArgs)

    End Sub
    Public Function RowHasWIPAccount(ByVal Row As DataRow) As Boolean
        Try
            If Row.Item("WIPAccountId") > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS2668 done 
    ''' </summary>
    ''' <param name="_Row"></param>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Public Sub SetWIPAccount(ByVal _Row As DataRow, ByVal dt As DataTable)
        Try
            If RowHasWIPAccount(_Row) = True Then
                Dim dr() As DataRow = dt.Select(" ParentTicketNo ='" & _Row.Item("BatchNo").ToString & "'")
                If dr.Length > 0 Then
                    For Each Row As DataRow In dr
                        If Val(Row.Item("WIPAccountId").ToString) < 1 Then
                            Row.BeginEdit()
                            Row.Item("WIPAccountId") = _Row.Item("WIPAccountId")
                            Row.EndEdit()
                            SetWIPAccount(Row, dt)
                        End If
                    Next
                End If
                'Else
                'dt.Rows.Remove(_Row)
                'dtMerging.Rows.Add(_Row)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub fillGrid(query As String)

        Try
            Dim dt As New DataTable
            dt = New PlanTicketsDAL().GetDeatilsByTickets(query)
            Me.grdItems.DataSource = dt
            ''Me.grdItems.RootTable.Columns("InvoiceAmount").Visible = False
            Me.grdItems.RootTable.Columns("PendingQty").FormatString = "N" & DecimalPointInValue
            Dim str As String = ""
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Sub frmTicketProducts_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnLoad_Click(sender As Object, e As EventArgs) Handles BtnLoad.Click, Button1.Click
        Try
            '    'frmCustomerCollection.cmbInvoice.Visible = True
            '    'frmCustomerCollection.lblInvoice.Visible = True
            '    For Each row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows
            '        frmCustomerCollection.cmbAccounts.Value = cmbCustomer.Value
            '        Dim str As String = "SELECT SalesTaxAccId from tblDefSalesTaxAccount where AccountId = " & Val(getConfigValueByType("SalesTaxCreditAccount").ToString) & ""
            '        Dim dt As DataTable = GetDataTable(str)
            '        Dim SalesTaxAccount As Integer
            '        If dt.Rows.Count > 0 Then
            '            SalesTaxAccount = dt.Rows(0).Item("SalesTaxAccId")
            '        End If
            '        'frmCustomerCollection.cmbSalesTax.SelectedValue = SalesTaxAccount
            '        frmCustomerCollection.txtAmount.Text = Math.Round(Val(row.Cells("Price").Value.ToString), TotalAmountRounding)
            '        'frmCustomerCollection.txtDiscount.Text = row.Cells("DiscountValue").Value.ToString ''Commented agianst TFS4683 to avoid sales discount on receiving
            '        'frmCustomerCollection.txtSalesTax.Text = row.Cells("SalesTax").Value.ToString
            '        frmCustomerCollection.cmbInvoice.Value = row.Cells("SalesId").Value.ToString
            '        frmCustomerCollection.txtReference.Text = "Payment Rec against Invoice No. " & row.Cells("SalesNo").Value.ToString & ""
            '        frmCustomerCollection.btnAdd_Click(Nothing, Nothing)
            '    Next
            '    Me.Close()
            '    ' frmcustomercollection.txtamount.text = 
            '    'grdItems_KeyDown(Nothing, Nothing)
            Dim dtHardware As DataTable = CType(Me.grdItems.DataSource, DataTable)
            'dtHardware.Columns("Tax_Amount").Expression = "(((Amount-Discount)*Tax)/100)"
            Dim dr As DataRow = dtHardware.NewRow()
            dtHardware.Rows.InsertAt(dr, 0)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbOpportunity_ValueChanged(sender As Object, e As EventArgs) Handles cmbOpportunity.Leave
        Try
            If cmbOpportunity.Value > 0 Then
                Dim str As String
                Dim dt As DataTable
                Me.cmbPreventionMaintenance.Text = cmbOpportunity.ActiveRow.Cells("PreventionMaintenance").Value.ToString
                Me.cmbSLAType.Text = cmbOpportunity.ActiveRow.Cells("SLAType").Value.ToString
                dtpFromDate.Value = cmbOpportunity.ActiveRow.Cells("StartDate").Value.ToString
                Me.cmbOpportunityStatus.Text = cmbOpportunity.ActiveRow.Cells("Status").Value.ToString
                Me.cmbInvoiceFrequency.Text = cmbOpportunity.ActiveRow.Cells("InvoicingFrequency").Value.ToString
                Me.cmbPaymentTerms.Text = cmbOpportunity.ActiveRow.Cells("PaymentTerms").Value.ToString
                Me.cmbEmployee.Text = cmbOpportunity.ActiveRow.Cells("OpportunityOwner").Value.ToString
                Me.cmbCurrency.SelectedValue = cmbOpportunity.ActiveRow.Cells("CurrencyId").Value
                Me.txtAmount.Text = cmbOpportunity.ActiveRow.Cells("TotalAmount").Value.ToString
                'Rafay 11-4-22'
                Me.ChkBatteriesIncluded.Checked = cmbOpportunity.ActiveRow.Cells("ChkBoxBatteriesIncluded").Value.ToString
                'Rafay 11-4-22
                Me.cmbDurationofMonth.Text = cmbOpportunity.ActiveRow.Cells("DurationofMonth").Value.ToString
                Me.cmbInvoicePattern.Text = cmbOpportunity.ActiveRow.Cells("InvoicePattern").Value.ToString
                Me.cmbItem.Value = cmbOpportunity.ActiveRow.Cells("ArticleId").Value.ToString
                Me.txtTax.Text = cmbOpportunity.ActiveRow.Cells("TaxAmount").Value.ToString
                'str = "SELECT SalesMasterTable.SalesId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, Price.Qty, Price.Price, Price.DiscountValue, Price.SalesTax FROM SalesMasterTable INNER JOIN (SELECT SalesDetailTable.SalesId, (SUM(ISNULL(SalesDetailTable.CurrencyAmount, 0)) + (SUM(ISNULL(SalesDetailTable.CurrencyAmount, 0)) * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) + (SUM(ISNULL(SalesDetailTable.CurrencyAmount, 0)) * ISNULL(SalesDetailTable.SEDPercent, 0) / 100) - ISNULL(SalesMasterTable.TotalOutwardExpense, 0)) AS Price, SUM(ISNULL(SalesDetailTable.Qty, 0)) AS Qty, ISNULL(SUM(SalesDetailTable.DiscountValue), 0) AS DiscountValue, (SUM(ISNULL(SalesDetailTable.TaxPercent, 0)) + SUM(ISNULL(SalesDetailTable.SEDPercent, 0))) / COUNT(ISNULL(SalesDetailTable.TaxPercent, 0)) AS SalesTax FROM SalesDetailTable LEFT OUTER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId GROUP BY SalesDetailTable.SalesId, SalesDetailTable.TaxPercent, SalesDetailTable.SEDPercent, SalesMasterTable.TotalOutwardExpense) AS Price ON SalesMasterTable.SalesId = Price.SalesId where CustomerCode = " & cmbCustomer.Value & "AND SalesmasterTable.SalesId not in (select isnull(InvoiceId,0) from tblVoucherDetail)"
                ''TFS4683 : Ayesha Rehman :02-10-2018 : Query Changes to get Voucher Effect also on partially receiving the Sales Invoice
                'Ali Faisal : Net Amount can not be Tax Excluding on Receipt.
                str = " SELECT OpportunitySupportDetailId, OpportunityId, Brand, ModelNo, SerialNo, SLACoverage, Address, City, Province, Country, StartDate, EndDate, Type, UnitPrice, FilePath, SLA, SLAInterventionTime, SLAFixTime, OnsiteIntervention, '' as Operator FROM tblDefOpportunitySupportDetail where OpportunityId = " & cmbOpportunity.Value & ""
                dt = GetDataTable(str)
                Me.grdItems.DataSource = dt
                'Me.grdItems.RootTable.Columns("StartDate").FormatString = str_DisplayDateFormat
                'Me.grdItems.RootTable.Columns("EndDate").FormatString = str_DisplayDateFormat
                grdItems.RetrieveStructure()
                grdItems.AutoSizeColumns()
                Me.grdItems.RootTable.Columns("UnitPrice").Visible = False
                If Me.grdItems.RootTable.Columns.Contains("Delete") = False Then
                    Me.grdItems.RootTable.Columns.Add("Delete")
                    Me.grdItems.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdItems.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdItems.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdItems.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdItems.RootTable.Columns("Delete").Width = 70
                    Me.grdItems.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grdItems.RootTable.Columns("Delete").Key = "Delete"
                    Me.grdItems.RootTable.Columns("Delete").Caption = "Action"
                End If
                Me.grdItems.RootTable.Columns("StartDate").FormatString = str_DisplayDateFormat
                Me.grdItems.RootTable.Columns("EndDate").FormatString = str_DisplayDateFormat
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_ColumnButtonClick1(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItems.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                Me.grdItems.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_KeyDown(sender As Object, e As KeyEventArgs) Handles grdItems.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                BtnLoad_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCustomer_ValueChanged(sender As Object, e As EventArgs) Handles cmbCustomer.Leave
        Try
            'If cmbCustomer.Value > 0 Then
            '    Dim str As String
            '    str = "SELECT tblDefOpportunity.OpportunityId,tblDefOpportunity.DocNo from tblDefOpportunity where OpportunityType = 'Support' and CompanyId = " & cmbCustomer.Value & ""
            '    FillUltraDropDown(cmbOpportunity, str)
            'End If

            'Rafay : when combobox leave then cmbrenew will fill
            FillCombos("PreviousContracts")
            'cmbRenew.Visible = True
            'PreviousContracts.Visible = True
            'Rafay
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Rafay:Task Start:function is create to check serial no if serial no is duplicate then show message serial no already exist
    Public Function CheckDuplicateSerialNo() As Boolean
        Try
            Dim Display As String
            If Me.grdItems.RowCount = 0 Then Return False
            For i As Int32 = 0 To Me.grdItems.RowCount - 1
                For j As Int32 = i + 1 To Me.grdItems.RowCount - 1
                    If Me.grdItems.GetRows(j).Cells("SerialNo").Value.ToString.Length > 0 Then
                        If Me.grdItems.GetRows(j).Cells("SerialNo").Value.ToString = Me.grdItems.GetRows(i).Cells("SerialNo").Value.ToString Then
                            Display = Me.grdItems.GetRows(i).Cells("SerialNo").Value.ToString
                            ShowErrorMessage("SerialNo [ " & Display & " ] already exist ")
                            Return True
                        End If
                    End If
                Next
            Next
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'rafay:Task End
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            objCon = Con

            If objCon.State = ConnectionState.Open Then objCon.Close()

            objCon.Open()
            objCommand.Connection = objCon

            Dim trans As OleDbTransaction = objCon.BeginTransaction
            objCommand.CommandType = CommandType.Text

            'Rafay:Add checkbox (terminated)
            'If HoldCheckbox.Checked = True Then
            '    GetAllRecords()
            'End If
            objCommand.Transaction = trans
            Dim DaysAddInStartDate As Double = 0
            Dim PreviousDate As Double
            Dim NumberOfLoop As Decimal
            Dim Amount As Double
            If IsValidate() Then
                If btnSave.Text = "&Save" Then
                    'Rafay": Modify query add these field to save (Terminated,PreviousContracts,cmbStatus,cmbhold,othersdescription,ChkBoxBatteriesIncluded)
                    objCommand.CommandText = "Insert into ContractMasterTable(ContractNo,StartDate,EndDate,SLAType,PreventionMaintenance,CustomerId,OpportunityId, Status, EndCustomer, PONumber, ContactofNotification, Site, InvoicingFrequency, PaymentTerms, Comments, Employee, Currency, Amount, TerminateStatus, ContractStatus, PreviousContracts, HoldReason, OthersDescription, HoldCheckBox, ChkBoxBatteriesIncluded, DurationofMonth, InvoicePattern, ArticleId, Tax) values( " _
                  & "N'" & txtContractNo.Text & "',N'" & dtpFromDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & dtpToDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & cmbSLAType.Text & "',N'" & cmbPreventionMaintenance.Text & "', N'" & cmbCustomer.Text & "', N'" & cmbOpportunity.Text & "', N'" & cmbOpportunityStatus.Text & "', N'" & txtEndCustomer.Text & "', N'" & txtPONumber.Text & "', N'" & txtContactofNotifictaion.Text & "', N'" & cmbSite.Text & "', N'" & cmbInvoiceFrequency.Text & "', N'" & cmbPaymentTerms.Text & "', N'" & txtComments.Text & "', N'" & cmbEmployee.Text & "', N'" & cmbCurrency.Text & "', N'" & txtAmount.Text & "', N'" & cmbterminate.Text & "', N'" & cmbStatus.Text & "', N'" & cmbRenew.Text & "' , N'" & cmbHold.Text & "', N'" & txtOthersDescription.Text & "'," & IIf(Me.HoldCheckbox.Checked = True, 1, 0) & "," & IIf(Me.ChkBatteriesIncluded.Checked = True, 1, 0) & " , N'" & cmbDurationofMonth.Text & "', N'" & cmbInvoicePattern.Text & "', " & cmbItem.Value & ", " & Val(txtTax.Text) & ")SELECT @@IDENTITY"
                    Dim CId As Integer = objCommand.ExecuteScalar
                    objCommand.CommandText = "insert into tblDefCostCenter(Name,Code,sortorder, CostCenterGroup, Active, OutwardGatepass, DayShift, IsLogical, Contract) values(N'" & txtContractNo.Text.Replace("'", "''") & "','" & txtContractNo.Text.Replace("'", "''") & "',1,'', 1, 0, 0, 0,1)"
                    objCommand.ExecuteNonQuery()
                    If Me.cmbRenew.SelectedValue > 0 Then
                        objCommand.CommandText = ""
                        objCommand.CommandText = "update ContractMasterTable Set  ContractStatus ='Terminate' Where ContractNo =N'" & cmbRenew.Text & "'"
                    End If
                    For i As Integer = 0 To grdItems.GetRows.Length - 1
                        objCommand.CommandText = "Insert into ContractDetailTable(ContractId,Brand,ModelNo,SerialNo,SLACoverage,Address,City,Province,Country,StartDate,EndDate,Type,UnitPrice,FilePath,SLA,SLAInterventionTime,SLAFixTime,OnsiteIntervention, Operator) Values(" _
                            & CId & ",N'" & Me.grdItems.GetRows(i).Cells("Brand").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("ModelNo").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("SerialNo").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("SLACoverage").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("Address").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("City").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("Province").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("Country").Value.ToString & "', " & IIf(grdItems.GetRows(i).Cells("StartDate").Value.ToString = "", "NULL", "Convert(DateTime,N'" & CDate(IIf(Me.grdItems.GetRows(i).Cells("StartDate").Value.ToString = "", Date.Now, Me.grdItems.GetRows(i).Cells("StartDate").Value)).ToString("yyyy-M-d hh:mm:ss tt") & "',102) ") _
                            & ", " & IIf(grdItems.GetRows(i).Cells("EndDate").Value.ToString = "", "NULL", "Convert(DateTime,N'" & CDate(IIf(Me.grdItems.GetRows(i).Cells("EndDate").Value.ToString = "", Date.Now, Me.grdItems.GetRows(i).Cells("EndDate").Value)).ToString("yyyy-M-d hh:mm:ss tt") & "',102) ") _
                            & ",N'" & Me.grdItems.GetRows(i).Cells("Type").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("UnitPrice").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("FilePath").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("SLA").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("SLAInterventionTime").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("SLAFixTime").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("OnsiteIntervention").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("Operator").Value.ToString & "')"
                        objCommand.ExecuteNonQuery()
                    Next
                    Dim fromdate As Date = Me.dtpFromDate.Value
                    Dim todate As Date = Me.dtpToDate.Value
                    If rdoDifferentAmount.Checked = True Then
                        For i As Integer = 0 To grdInvoiceDetails.GetRows.Length - 1
                            Dim strtodate As String
                            If i = grdInvoiceDetails.GetRows.Length - 1 Then
                                todate = Me.dtpToDate.Value
                            Else
                                strtodate = "SELECT DATEADD(day, " & 365 & ", '" & fromdate & "') as ToDate"
                                Dim dttodate As DataTable = GetDataTable(strtodate)
                                If dttodate.Rows.Count > 0 Then
                                    todate = dttodate.Rows(0).Item("ToDate")
                                End If
                            End If

                            If cmbInvoiceFrequency.Text = "Full in advance" Then
                                objCommand.CommandText = "Insert into ContractInvoiceDetailTable(ContractId,InvoiceDate,InvoiceAmount) Values(" & CId & ",(SELECT DATEADD(day, " & 1 & ", '" & Date.Now & "')), " & Val(Me.grdInvoiceDetails.GetRows(i).Cells("InvoiceAmount").Value.ToString) & ")"
                                objCommand.ExecuteNonQuery()
                            Else
                                Dim FrequencyDays As Integer
                                If cmbInvoiceFrequency.Text = "Monthly" Then
                                    FrequencyDays = 1
                                    If cmbInvoicePattern.Text = "Arrears" Then
                                        DaysAddInStartDate = 30
                                        PreviousDate = 0
                                    Else
                                        PreviousDate = 0
                                        DaysAddInStartDate = 0
                                    End If

                                    Dim str As String = "select datediff(mm,'" & fromdate & "','" & todate & "') as DateDiffrence, datediff(dd,'" & fromdate & "','" & todate & "') as DateDiffrenceDays "
                                    Dim dt As DataTable = GetDataTable(str)
                                    If dt.Rows.Count > 0 Then
                                        NumberOfLoop = dt.Rows(0).Item("DateDiffrence") / 1
                                        Amount = (Val(Me.grdInvoiceDetails.GetRows(i).Cells("InvoiceAmount").Value.ToString) / dt.Rows(0).Item("DateDiffrence"))
                                    End If
                                ElseIf cmbInvoiceFrequency.Text = "Quarterly" Then
                                    FrequencyDays = 3
                                    If cmbInvoicePattern.Text = "Arrears" Then
                                        PreviousDate = 0
                                        DaysAddInStartDate = 91
                                    Else
                                        DaysAddInStartDate = 0
                                        PreviousDate = 0
                                    End If

                                    Dim str As String = "select datediff(mm,'" & fromdate & "','" & todate & "') as DateDiffrence, datediff(dd,'" & fromdate & "','" & todate & "') as DateDiffrenceDays "
                                    Dim dt As DataTable = GetDataTable(str)
                                    If dt.Rows.Count > 0 Then
                                        NumberOfLoop = dt.Rows(0).Item("DateDiffrence") / 3
                                        Amount = (Val(Me.grdInvoiceDetails.GetRows(i).Cells("InvoiceAmount").Value.ToString) / dt.Rows(0).Item("DateDiffrence"))
                                    End If
                                ElseIf cmbInvoiceFrequency.Text = "Half Yearly" Then
                                    FrequencyDays = 6
                                    If cmbInvoicePattern.Text = "Arrears" Then
                                        DaysAddInStartDate = 182
                                        PreviousDate = 0
                                    Else
                                        DaysAddInStartDate = 0
                                        PreviousDate = 0
                                    End If
                                    Dim str As String = "select datediff(mm,'" & fromdate & "','" & todate & "') as DateDiffrence, datediff(dd,'" & fromdate & "','" & todate & "') as DateDiffrenceDays "
                                    Dim dt As DataTable = GetDataTable(str)
                                    If dt.Rows.Count > 0 Then
                                        NumberOfLoop = dt.Rows(0).Item("DateDiffrence") / 6
                                        Amount = (Val(Me.grdInvoiceDetails.GetRows(i).Cells("InvoiceAmount").Value.ToString) / dt.Rows(0).Item("DateDiffrence"))
                                    End If
                                ElseIf cmbInvoiceFrequency.Text = "Yearly" Then
                                    FrequencyDays = 12
                                    If cmbInvoicePattern.Text = "Arrears" Then
                                        DaysAddInStartDate = DaysAddInStartDate + 364
                                        PreviousDate = 0
                                    Else
                                        DaysAddInStartDate = DaysAddInStartDate + 0
                                        PreviousDate = 0
                                    End If
                                    Dim str As String = "select datediff(mm,'" & fromdate & "','" & todate & "') as DateDiffrence, datediff(dd,'" & fromdate & "','" & todate & "') as DateDiffrenceDays "
                                    Dim dt As DataTable = GetDataTable(str)
                                    If dt.Rows.Count > 0 Then
                                        NumberOfLoop = dt.Rows(0).Item("DateDiffrence") / 12
                                        Amount = (Val(Me.grdInvoiceDetails.GetRows(i).Cells("InvoiceAmount").Value.ToString) / dt.Rows(0).Item("DateDiffrence"))
                                    End If
                                End If
                                Dim fromdatedays As Double
                                For j As Double = 1.0 To NumberOfLoop
                                    objCommand.CommandText = "Insert into ContractInvoiceDetailTable(ContractId,InvoiceDate,InvoiceAmount,PreviousDate) Values(" & CId & "," & IIf(cmbInvoicePattern.Text = "Advance", IIf(j = 1, "(SELECT DATEADD(day, " & 1 & ", '" & Date.Now & "'))", "(SELECT DATEADD(day, " & DaysAddInStartDate & ", '" & fromdate & "'))"), "(SELECT DATEADD(day, " & DaysAddInStartDate & ", '" & fromdate & "'))") & ", " & Amount * FrequencyDays & ",(SELECT DATEADD(day, " & PreviousDate & ", '" & dtpFromDate.Value & "')))"
                                    objCommand.ExecuteNonQuery()
                                    If NumberOfLoop - j < 1 And NumberOfLoop - j > 0 Then
                                        If cmbInvoiceFrequency.Text = "Monthly" And cmbInvoicePattern.Text = "Advance" Then
                                            DaysAddInStartDate = DaysAddInStartDate + 31
                                        ElseIf cmbInvoiceFrequency.Text = "Quarterly" And cmbInvoicePattern.Text = "Advance" Then
                                            DaysAddInStartDate = DaysAddInStartDate + 92
                                        ElseIf cmbInvoiceFrequency.Text = "Half Yearly" And cmbInvoicePattern.Text = "Advance" Then
                                            DaysAddInStartDate = DaysAddInStartDate + 183
                                        ElseIf cmbInvoiceFrequency.Text = "Yearly" And cmbInvoicePattern.Text = "Advance" Then
                                            DaysAddInStartDate = DaysAddInStartDate + 365
                                        End If
                                        If cmbInvoiceFrequency.Text = "Monthly" Then
                                            PreviousDate = PreviousDate + 30
                                        ElseIf cmbInvoiceFrequency.Text = "Quarterly" Then
                                            PreviousDate = PreviousDate + 91
                                        ElseIf cmbInvoiceFrequency.Text = "Half Yearly" Then
                                            PreviousDate = PreviousDate + 182
                                        ElseIf cmbInvoiceFrequency.Text = "Yearly" Then
                                            PreviousDate = PreviousDate + 364
                                        End If
                                        Dim remaingdays As Integer
                                        Dim str As String = "select datediff(mm,(SELECT DATEADD(day, " & DaysAddInStartDate & ", '" & fromdate & "')),'" & todate & "') as RemainingDays"
                                        Dim dt As DataTable = GetDataTable(str)
                                        If dt.Rows.Count > 0 Then
                                            remaingdays = dt.Rows(0).Item("RemainingDays")
                                        End If
                                        If cmbInvoicePattern.Text = "Arrears" Then
                                            objCommand.CommandText = "Insert into ContractInvoiceDetailTable(ContractId,InvoiceDate,InvoiceAmount, PreviousDate) Values(" & CId & ",'" & todate & "', " & Amount * remaingdays & ",(SELECT DATEADD(day, " & PreviousDate & ", '" & dtpFromDate.Value & "')))"
                                            objCommand.ExecuteNonQuery()
                                        Else
                                            'Dim AdvanceDate As String
                                            'Dim str1 As String = "select datediff(dd,(SELECT DATEADD(day, " & DaysAddInStartDate & ", '" & dtpFromDate.Value & "')),'" & todate & "') as AdvanceDate"
                                            'Dim dt1 As DataTable = GetDataTable(str1)
                                            'If dt1.Rows.Count > 0 Then
                                            '    AdvanceDate = dt1.Rows(0).Item("AdvanceDate")
                                            'End If
                                            objCommand.CommandText = "Insert into ContractInvoiceDetailTable(ContractId,InvoiceDate,InvoiceAmount, PreviousDate) Values(" & CId & ",(SELECT DATEADD(day, " & DaysAddInStartDate & ", '" & fromdate & "')), " & Amount * remaingdays & ",(SELECT DATEADD(day, " & PreviousDate & ", '" & dtpFromDate.Value & "')))"
                                            objCommand.ExecuteNonQuery()
                                        End If
                                    End If

                                    If cmbInvoiceFrequency.Text = "Monthly" Then
                                        DaysAddInStartDate = DaysAddInStartDate + 30
                                        PreviousDate = PreviousDate + 30
                                        fromdatedays = DaysAddInStartDate
                                    ElseIf cmbInvoiceFrequency.Text = "Quarterly" Then
                                        fromdatedays = DaysAddInStartDate
                                        DaysAddInStartDate = DaysAddInStartDate + 91
                                        PreviousDate = PreviousDate + 91
                                    ElseIf cmbInvoiceFrequency.Text = "Half Yearly" Then
                                        fromdatedays = DaysAddInStartDate
                                        DaysAddInStartDate = DaysAddInStartDate + 182
                                        PreviousDate = PreviousDate + 182
                                    ElseIf cmbInvoiceFrequency.Text = "Yearly" Then
                                        fromdatedays = DaysAddInStartDate
                                        DaysAddInStartDate = DaysAddInStartDate + 364
                                        PreviousDate = PreviousDate + 364
                                    End If
                                Next
                                Dim strfromdates As String
                                If cmbInvoicePattern.Text = "Arrears" Then
                                    strfromdates = "SELECT DATEADD(day, " & fromdatedays & ", '" & fromdate & "') as FromDate"
                                Else
                                    strfromdates = "SELECT DATEADD(day, " & DaysAddInStartDate & ", '" & fromdate & "') as FromDate"
                                End If
                                Dim dtfromdate As DataTable = GetDataTable(strfromdates)
                                If dtfromdate.Rows.Count > 0 Then
                                    fromdate = dtfromdate.Rows(0).Item("FromDate")
                                End If
                            End If
                        Next
                    ElseIf rdoSameAmount.Checked = True Then
                        If cmbInvoiceFrequency.Text = "Full in advance" Then
                            objCommand.CommandText = "Insert into ContractInvoiceDetailTable(ContractId,InvoiceDate,InvoiceAmount) Values(" & CId & ",(SELECT DATEADD(day, " & 1 & ", '" & Date.Now & "')), " & Val(txtAmount.Text) & ")"
                            objCommand.ExecuteNonQuery()
                        Else
                            Dim FrequencyDays As Integer
                            If cmbInvoiceFrequency.Text = "Monthly" Then
                                FrequencyDays = 1
                                If cmbInvoicePattern.Text = "Arrears" Then
                                    PreviousDate = 0
                                    DaysAddInStartDate = 30
                                Else
                                    DaysAddInStartDate = 0
                                    PreviousDate = 0
                                End If

                                Dim str As String = "select datediff(mm,'" & dtpFromDate.Value & "'," & "DATEADD(day, " & 1 & ", '" & dtpToDate.Value & "')" & ") as DateDiffrence, datediff(dd,'" & dtpFromDate.Value & "','" & dtpToDate.Value & "') as DateDiffrenceDays "
                                Dim dt As DataTable = GetDataTable(str)
                                If dt.Rows.Count > 0 Then
                                    NumberOfLoop = dt.Rows(0).Item("DateDiffrence") / 1
                                    Amount = (Val(txtAmount.Text) / dt.Rows(0).Item("DateDiffrence"))
                                End If
                            ElseIf cmbInvoiceFrequency.Text = "Quarterly" Then
                                FrequencyDays = 3
                                If cmbInvoicePattern.Text = "Arrears" Then
                                    DaysAddInStartDate = 91
                                    PreviousDate = 0
                                Else
                                    DaysAddInStartDate = 0
                                    PreviousDate = 0
                                End If

                                Dim str As String = "select datediff(mm,'" & dtpFromDate.Value & "'," & "DATEADD(day, " & 1 & ", '" & dtpToDate.Value & "')" & ") as DateDiffrence, datediff(dd,'" & dtpFromDate.Value & "','" & dtpToDate.Value & "') as DateDiffrenceDays "
                                Dim dt As DataTable = GetDataTable(str)
                                If dt.Rows.Count > 0 Then
                                    NumberOfLoop = dt.Rows(0).Item("DateDiffrence") / 3
                                    Amount = (Val(txtAmount.Text) / dt.Rows(0).Item("DateDiffrence"))
                                End If
                            ElseIf cmbInvoiceFrequency.Text = "Half Yearly" Then
                                FrequencyDays = 6
                                If cmbInvoicePattern.Text = "Arrears" Then
                                    DaysAddInStartDate = 182
                                    PreviousDate = 0
                                Else
                                    DaysAddInStartDate = 0
                                    PreviousDate = 0
                                End If
                                Dim str As String = "select datediff(mm,'" & dtpFromDate.Value & "'," & "DATEADD(day, " & 1 & ", '" & dtpToDate.Value & "')" & ") as DateDiffrence, datediff(dd,'" & dtpFromDate.Value & "','" & dtpToDate.Value & "') as DateDiffrenceDays "
                                Dim dt As DataTable = GetDataTable(str)
                                If dt.Rows.Count > 0 Then
                                    NumberOfLoop = dt.Rows(0).Item("DateDiffrence") / 6
                                    Amount = (Val(txtAmount.Text) / dt.Rows(0).Item("DateDiffrence"))
                                End If
                            ElseIf cmbInvoiceFrequency.Text = "Yearly" Then
                                FrequencyDays = 12
                                If cmbInvoicePattern.Text = "Arrears" Then
                                    DaysAddInStartDate = 364
                                    PreviousDate = 0
                                Else
                                    DaysAddInStartDate = 0
                                    PreviousDate = 0
                                End If
                                Dim str As String = "select datediff(mm,'" & dtpFromDate.Value & "'," & "DATEADD(day, " & 1 & ", '" & dtpToDate.Value & "')" & ") as DateDiffrence, datediff(dd,'" & dtpFromDate.Value & "','" & dtpToDate.Value & "') as DateDiffrenceDays "
                                Dim dt As DataTable = GetDataTable(str)
                                If dt.Rows.Count > 0 Then
                                    NumberOfLoop = dt.Rows(0).Item("DateDiffrence") / 12
                                    Amount = (Val(txtAmount.Text) / dt.Rows(0).Item("DateDiffrence"))
                                End If
                            End If
                            For j As Double = 1.0 To NumberOfLoop
                                Dim strpreviousdays As String
                                Dim dtpreviousdays As DataTable
                                If cmbInvoicePattern.Text = "Advance" Then
                                    strpreviousdays = "SELECT CASE WHEN  (SELECT DATEADD(day, 1 ,  '" & Date.Now & "' )) >= (SELECT DATEADD(day, " & DaysAddInStartDate & " ,  '" & dtpFromDate.Value & "' )) THEN  (SELECT DATEADD(day, 1 ,  '" & Date.Now & "' )) ELSE (SELECT DATEADD(day, " & DaysAddInStartDate & " ,  '" & dtpFromDate.Value & "' )) END"
                                    dtpreviousdays = GetDataTable(strpreviousdays)
                                    If dtpreviousdays.Rows.Count > 0 Then
                                        objCommand.CommandText = "Insert into ContractInvoiceDetailTable(ContractId,InvoiceDate,InvoiceAmount, PreviousDate) Values(" & CId & ",'" & dtpreviousdays.Rows(0).Item(0) & "', " & Amount * FrequencyDays & ",(SELECT DATEADD(day, " & PreviousDate & ", '" & dtpFromDate.Value & "')))"
                                        objCommand.ExecuteNonQuery()
                                    End If
                                    'if 

                                    'Else
                                    '    objCommand.CommandText = "Insert into ContractInvoiceDetailTable(ContractId,InvoiceDate,InvoiceAmount, PreviousDate) Values(" & CId & "," & IIf(cmbInvoicePattern.Text = "Advance", IIf(j = 1, "(SELECT DATEADD(day, " & 1 & ", '" & Date.Now & "'))", "(SELECT DATEADD(day, " & DaysAddInStartDate & ", '" & dtpFromDate.Value & "'))"), "(SELECT DATEADD(day, " & DaysAddInStartDate & ", '" & dtpFromDate.Value & "'))") & ", " & Amount * FrequencyDays & ",(SELECT DATEADD(day, " & PreviousDate & ", '" & dtpFromDate.Value & "')))"
                                    '    objCommand.ExecuteNonQuery()
                                    'End If
                                    'ElseIf cmbInvoicePattern.Text = "Arrears" Then
                                    '    strpreviousdays = "SELECT CASE WHEN  (SELECT DATEADD(day, 1 ,  '" & Date.Now & "' )) > (SELECT DATEADD(day, " & DaysAddInStartDate & " ,  '" & dtpFromDate.Value & "' )) THEN  (SELECT DATEADD(day, 1 ,  '" & Date.Now & "' )) ELSE (SELECT DATEADD(day, " & DaysAddInStartDate & " ,  '" & dtpFromDate.Value & "' )) END"
                                    '    dtpreviousdays = GetDataTable(strpreviousdays)
                                    '    If dtpreviousdays.Rows.Count > 0 Then
                                    '        objCommand.CommandText = "Insert into ContractInvoiceDetailTable(ContractId,InvoiceDate,InvoiceAmount, PreviousDate) Values(" & CId & ",'" & dtpreviousdays.Rows(0).Item(0) & "', " & Amount * FrequencyDays & ",(SELECT DATEADD(day, " & PreviousDate & ", '" & dtpFromDate.Value & "')))"
                                    '        objCommand.ExecuteNonQuery()
                                    '    End If
                                End If

                                'objCommand.CommandText = "Insert into ContractInvoiceDetailTable(ContractId,InvoiceDate,InvoiceAmount, PreviousDate) Values(" & CId & "," & IIf(cmbInvoicePattern.Text = "Advance", IIf(j = 1, "(SELECT DATEADD(day, " & 1 & ", '" & Date.Now & "'))", "(SELECT DATEADD(day, " & DaysAddInStartDate & ", '" & dtpFromDate.Value & "'))"), "(SELECT DATEADD(day, " & DaysAddInStartDate & ", '" & dtpFromDate.Value & "'))") & ", " & Amount * FrequencyDays & ",(SELECT DATEADD(day, " & PreviousDate & ", '" & dtpFromDate.Value & "')))"
                                'objCommand.ExecuteNonQuery()
                                If NumberOfLoop - j < 1 And NumberOfLoop - j > 0 Then
                                    If cmbInvoiceFrequency.Text = "Monthly" And cmbInvoicePattern.Text = "Advance" Then
                                        DaysAddInStartDate = DaysAddInStartDate + 30
                                    ElseIf cmbInvoiceFrequency.Text = "Quarterly" And cmbInvoicePattern.Text = "Advance" Then
                                        DaysAddInStartDate = DaysAddInStartDate + 91
                                    ElseIf cmbInvoiceFrequency.Text = "Half Yearly" And cmbInvoicePattern.Text = "Advance" Then
                                        DaysAddInStartDate = DaysAddInStartDate + 182
                                    ElseIf cmbInvoiceFrequency.Text = "Yearly" And cmbInvoicePattern.Text = "Advance" Then
                                        DaysAddInStartDate = DaysAddInStartDate + 364
                                    End If
                                    If cmbInvoiceFrequency.Text = "Monthly" Then
                                        PreviousDate = PreviousDate + 30
                                    ElseIf cmbInvoiceFrequency.Text = "Quarterly" Then
                                        PreviousDate = PreviousDate + 91
                                    ElseIf cmbInvoiceFrequency.Text = "Half Yearly" Then
                                        PreviousDate = PreviousDate + 182
                                    ElseIf cmbInvoiceFrequency.Text = "Yearly" Then
                                        PreviousDate = PreviousDate + 364
                                    End If
                                    Dim remaingdays As Integer
                                    Dim str As String = "select datediff(mm,(SELECT DATEADD(day, " & DaysAddInStartDate & ", '" & dtpFromDate.Value & "')),'" & dtpToDate.Value & "') as RemainingDays"
                                    Dim dt As DataTable = GetDataTable(str)
                                    If dt.Rows.Count > 0 Then
                                        remaingdays = dt.Rows(0).Item("RemainingDays")
                                    End If
                                    If cmbInvoicePattern.Text = "Arrears" Then
                                        objCommand.CommandText = "Insert into ContractInvoiceDetailTable(ContractId,InvoiceDate,InvoiceAmount,PreviousDate) Values(" & CId & ",'" & dtpToDate.Value & "', " & Amount * remaingdays & ",(SELECT DATEADD(day, " & PreviousDate & ", '" & dtpFromDate.Value & "')))"
                                        objCommand.ExecuteNonQuery()
                                    Else
                                        'Dim AdvanceDate As String
                                        'Dim str1 As String = "select datediff(dd,(SELECT DATEADD(day, " & DaysAddInStartDate & ", '" & dtpFromDate.Value & "')),'" & dtpToDate.Value & "') as AdvanceDate"
                                        'Dim dt1 As DataTable = GetDataTable(str1)
                                        'If dt1.Rows.Count > 0 Then
                                        '    AdvanceDate = dt1.Rows(0).Item("AdvanceDate")
                                        'End If
                                        objCommand.CommandText = "Insert into ContractInvoiceDetailTable(ContractId,InvoiceDate,InvoiceAmount) Values(" & CId & ",(SELECT DATEADD(day, " & DaysAddInStartDate & ", '" & dtpFromDate.Value & "')), " & Amount * remaingdays & ")"
                                        objCommand.ExecuteNonQuery()
                                    End If
                                End If
                                If cmbInvoiceFrequency.Text = "Monthly" Then
                                    DaysAddInStartDate = DaysAddInStartDate + 30
                                    PreviousDate = PreviousDate + 30
                                ElseIf cmbInvoiceFrequency.Text = "Quarterly" Then
                                    DaysAddInStartDate = DaysAddInStartDate + 91
                                    PreviousDate = PreviousDate + 91
                                ElseIf cmbInvoiceFrequency.Text = "Half Yearly" Then
                                    DaysAddInStartDate = DaysAddInStartDate + 182
                                    PreviousDate = PreviousDate + 182
                                ElseIf cmbInvoiceFrequency.Text = "Yearly" Then
                                    DaysAddInStartDate = DaysAddInStartDate + 364
                                    PreviousDate = PreviousDate + 364
                                End If
                            Next
                        End If
                    ElseIf rdoMannualAmount.Checked = True Then
                        For i As Integer = 0 To grdInvoiceDetails.GetRows.Length - 1

                            Dim strtodate As String = "SELECT DATEADD(day, " & 365 & ", '" & fromdate & "') as ToDate"
                            Dim dttodate As DataTable = GetDataTable(strtodate)
                            If dttodate.Rows.Count > 0 Then
                                todate = dttodate.Rows(0).Item("ToDate")
                            End If
                            If i = 0 Then
                                objCommand.CommandText = "Insert into ContractInvoiceDetailTable(ContractId,InvoiceDate,InvoiceAmount, PreviousDate) Values(" & CId & ",'" & Me.grdInvoiceDetails.GetRows(i).Cells("InvoiceDate").Value.ToString & "', " & Val(Me.grdInvoiceDetails.GetRows(i).Cells("InvoiceAmount").Value.ToString) & ",'" & fromdate & "')"
                                objCommand.ExecuteNonQuery()
                            Else
                                objCommand.CommandText = "Insert into ContractInvoiceDetailTable(ContractId,InvoiceDate,InvoiceAmount, PreviousDate) Values(" & CId & ",'" & Me.grdInvoiceDetails.GetRows(i).Cells("InvoiceDate").Value.ToString & "', " & Val(Me.grdInvoiceDetails.GetRows(i).Cells("InvoiceAmount").Value.ToString) & ",'" & Me.grdInvoiceDetails.GetRows(i - 1).Cells("InvoiceDate").Value.ToString & "')"
                                objCommand.ExecuteNonQuery()
                            End If
                            
                        Next
                    End If
                            SaveDocument(CId, Me.Name, trans)
                            trans.Commit()
                            'rafay
                            ReSetControls()
                            cmbRenew.Visible = False
                            PreviousContracts.Visible = False
                            Terminate.Visible = False
                            cmbterminate.Visible = False
                            lblOthersDescription.Visible = False
                            txtOthersDescription.Visible = False
                            'rafay
                ElseIf btnSave.Text = "&Update" Then
                            If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                            'Rafay": Modify query add these field to save (TerminateStatus,PreviousContracts,cmbStatus,cmbHold,OthersDescription,HoldCHeckBox,ChkBoxBatteriesIncluded)
                    objCommand.CommandText = "Update ContractMasterTable set ContractNo = N'" & txtContractNo.Text & "', StartDate = N'" & dtpFromDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',EndDate = N'" & dtpToDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',SLAType = N'" & cmbSLAType.Text & "',PreventionMaintenance = N'" & cmbPreventionMaintenance.Text & "',CustomerId = N'" & cmbCustomer.Text & "',OpportunityId = N'" & cmbOpportunity.Text & "', Status = N'" & cmbOpportunityStatus.Text & "', EndCustomer = N'" & txtEndCustomer.Text & "', PONumber = N'" & txtPONumber.Text & "', ContactofNotification = N'" & txtContactofNotifictaion.Text & "', Site = N'" & cmbSite.Text & "', InvoicingFrequency = N'" & cmbInvoiceFrequency.Text & "', PaymentTerms = N'" & cmbPaymentTerms.Text & "', Comments = N'" & txtComments.Text & "', Employee = N'" & cmbEmployee.Text & "', Currency = N'" & cmbCurrency.Text & "', Amount = N'" & Val(txtAmount.Text) & "', TerminateStatus= N'" & cmbterminate.Text & "', ContractStatus = N'" & cmbStatus.Text & "', PreviousContracts = N'" & cmbRenew.Text & "' , HoldReason = N'" & cmbHold.Text & "',OthersDescription = N'" & txtOthersDescription.Text & "', HoldCheckBox=" & IIf(Me.HoldCheckbox.Checked = True, 1, 0) & ", ChkBoxBatteriesIncluded=" & IIf(Me.ChkBatteriesIncluded.Checked = True, 1, 0) & ",DurationofMonth = N'" & cmbDurationofMonth.Text & "',InvoicePattern = N'" & cmbInvoicePattern.Text & "',ArticleId = " & Val(cmbItem.Value) & ", tax = " & Val(txtTax.Text) & "   Where ContractId = " & ContractId
                            objCommand.ExecuteNonQuery()
                            objCommand.CommandText = ""
                            objCommand.CommandText = "Delete from ContractDetailTable where ContractId = " & ContractId
                            objCommand.ExecuteNonQuery()
                            For i As Integer = 0 To grdItems.GetRows.Length - 1
                                objCommand.CommandText = "Insert into ContractDetailTable(ContractId,Brand,ModelNo,SerialNo,SLACoverage,Address,City,Province,Country,StartDate,EndDate,Type,UnitPrice,FilePath,SLA,SLAInterventionTime,SLAFixTime,OnsiteIntervention, Operator) Values(" _
                                    & ContractId & ",N'" & Me.grdItems.GetRows(i).Cells("Brand").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("ModelNo").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("SerialNo").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("SLACoverage").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("Address").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("City").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("Province").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("Country").Value.ToString & "', " & IIf(grdItems.GetRows(i).Cells("StartDate").Value.ToString = "", "NULL", "Convert(DateTime,N'" & CDate(IIf(Me.grdItems.GetRows(i).Cells("StartDate").Value.ToString = "", Date.Now, Me.grdItems.GetRows(i).Cells("StartDate").Value)).ToString("yyyy-M-d hh:mm:ss tt") & "',102) ") _
                                    & ", " & IIf(grdItems.GetRows(i).Cells("EndDate").Value.ToString = "", "NULL", "Convert(DateTime,N'" & CDate(IIf(Me.grdItems.GetRows(i).Cells("EndDate").Value.ToString = "", Date.Now, Me.grdItems.GetRows(i).Cells("EndDate").Value)).ToString("yyyy-M-d hh:mm:ss tt") & "',102) ") _
                                    & ",N'" & Me.grdItems.GetRows(i).Cells("Type").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("UnitPrice").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("FilePath").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("SLA").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("SLAInterventionTime").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("SLAFixTime").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("OnsiteIntervention").Value.ToString & "',N'" & Me.grdItems.GetRows(i).Cells("Operator").Value.ToString & "')"
                                objCommand.ExecuteNonQuery()
                            Next
                            If arrFile.Count > 0 Then
                                SaveDocument(ContractId, Me.Name, trans)
                            End If
                            trans.Commit()
                            'rafay
                            ReSetControls()
                            cmbRenew.Visible = False
                            PreviousContracts.Visible = False
                            Terminate.Visible = False
                            lblOthersDescription.Visible = False
                            txtOthersDescription.Visible = False
                            cmbterminate.Visible = False
                            'rafay
                End If
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GrdStatus_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles GrdStatus.RowDoubleClick
        Try
            'rafay
            GetSecurityRights()
            'rafay
            arrFile = New List(Of String)
            If Not Val(GrdStatus.CurrentRow.Cells("ContractId").Value.ToString) > 0 Then Exit Sub
            ContractId = Val(GrdStatus.CurrentRow.Cells("ContractId").Value.ToString)
            Me.txtContractNo.Text = GrdStatus.CurrentRow.Cells("ContractNo").Value.ToString
            Me.dtpFromDate.Value = IIf(IsDBNull(GrdStatus.CurrentRow.Cells("StartDate").Value), DateTime.Today, GrdStatus.CurrentRow.Cells("StartDate").Value)
            Me.dtpToDate.Value = IIf(IsDBNull(GrdStatus.CurrentRow.Cells("EndDate").Value), DateTime.Today, GrdStatus.CurrentRow.Cells("EndDate").Value)
            Me.cmbSLAType.Text = GrdStatus.CurrentRow.Cells("SLAType").Value.ToString
            Me.cmbPreventionMaintenance.Text = GrdStatus.CurrentRow.Cells("PreventionMaintenance").Value.ToString
            Me.cmbOpportunityStatus.Text = GrdStatus.CurrentRow.Cells("Status").Value.ToString
            Me.txtEndCustomer.Text = GrdStatus.CurrentRow.Cells("EndCustomer").Value.ToString
            Me.txtPONumber.Text = GrdStatus.CurrentRow.Cells("PONumber").Value.ToString
            Me.txtContactofNotifictaion.Text = GrdStatus.CurrentRow.Cells("ContactofNotification").Value.ToString
            Me.cmbSite.Text = GrdStatus.CurrentRow.Cells("Site").Value.ToString
            Me.cmbInvoiceFrequency.Text = GrdStatus.CurrentRow.Cells("InvoicingFrequency").Value.ToString
            Me.cmbPaymentTerms.Text = GrdStatus.CurrentRow.Cells("PaymentTerms").Value.ToString
            Me.txtComments.Text = GrdStatus.CurrentRow.Cells("Comments").Value.ToString
            Me.cmbEmployee.Text = GrdStatus.CurrentRow.Cells("Employee").Value.ToString
            Me.cmbCustomer.Text = GrdStatus.CurrentRow.Cells("CustomerId").Value.ToString
            Me.cmbOpportunity.Text = GrdStatus.CurrentRow.Cells("OpportunityId").Value.ToString
            Me.cmbCurrency.Text = GrdStatus.CurrentRow.Cells("Currency").Value.ToString
            Me.txtAmount.Text = GrdStatus.CurrentRow.Cells("Amount").Value.ToString
            Me.cmbDurationofMonth.Text = GrdStatus.CurrentRow.Cells("DurationofMonth").Value.ToString
            Me.cmbInvoicePattern.Text = GrdStatus.CurrentRow.Cells("InvoicePattern").Value.ToString
            Me.cmbItem.Value = GrdStatus.CurrentRow.Cells("ArticleId").Value.ToString
            Me.txtTax.Text = GrdStatus.CurrentRow.Cells("Tax").Value.ToString
            'Rafay
            Me.cmbterminate.Text = GrdStatus.CurrentRow.Cells("TerminateStatus").Value.ToString
            Me.cmbStatus.Text = GrdStatus.CurrentRow.Cells("ContractStatus").Value.ToString
            Me.cmbRenew.Text = GrdStatus.CurrentRow.Cells("PreviousContracts").Value.ToString
            Me.cmbHold.Text = GrdStatus.CurrentRow.Cells("HoldReason").Value.ToString
            Me.txtOthersDescription.Text = GrdStatus.CurrentRow.Cells("OthersDescription").Value.ToString
            Me.HoldCheckbox.Checked = GrdStatus.CurrentRow.Cells("HoldCheckBox").Value
            '11-4-22 this line is added'
            Me.ChkBatteriesIncluded.Checked = GrdStatus.CurrentRow.Cells("BatteriesIncluded").Value
            'Rafay
            Me.ToolStripButton1.Text = "Attachments (" & Me.GrdStatus.GetRow.Cells("No Of Attachment").Value.ToString & ")"
            Dim str As String
            Dim dt As DataTable
            str = " select * from ContractDetailTable where ContractId = " & Val(GrdStatus.CurrentRow.Cells("ContractId").Value.ToString) & ""
            dt = GetDataTable(str)
            Me.grdItems.DataSource = dt
            grdItems.RetrieveStructure()
            grdItems.AutoSizeColumns()
            If Me.grdItems.RootTable.Columns.Contains("Delete") = False Then
                Me.grdItems.RootTable.Columns.Add("Delete")
                Me.grdItems.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdItems.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdItems.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdItems.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdItems.RootTable.Columns("Delete").Width = 70
                Me.grdItems.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grdItems.RootTable.Columns("Delete").Key = "Delete"
                Me.grdItems.RootTable.Columns("Delete").Caption = "Action"
            End If
            Me.grdItems.RootTable.Columns("StartDate").FormatString = str_DisplayDateFormat
            Me.grdItems.RootTable.Columns("EndDate").FormatString = str_DisplayDateFormat
            Me.btnSave.Text = "&Update"
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Or LoginUser.LoginGroupId = 8 Then
                Me.Visible = True
                Me.btnSave.Enabled = True
                'Rafay:give rights to admin to view print ,export,choosefielder 
                Me.CtrlGrdBar2.mGridPrint.Enabled = True
                Me.CtrlGrdBar2.mGridExport.Enabled = True
                Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                HoldCheckbox.Visible = True
                'rafay :Task Start :If user have save rights then amount field is shown
                Me.txtAmount.Visible = True
                Me.Label9.Visible = True
                'Rafay:Task End
                Exit Sub
            End If
            Me.Visible = False
            Me.btnSave.Enabled = False
            'Rafay:Task Start
            Me.CtrlGrdBar2.mGridPrint.Enabled = False
            Me.CtrlGrdBar2.mGridExport.Enabled = False
            Me.CtrlGrdBar2.mGridChooseFielder.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            'rafay :Task Start :If user have save rights then amount field is shown
            Me.txtAmount.Visible = False
            Me.Label9.Visible = False
            Me.HoldCheckbox.Visible = False
            'Rafay:Task End
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Save" Then
                    btnSave.Enabled = True
                    'rafay :Task Start :If user have save rights then amount field is shown
                    Me.txtAmount.Visible = True
                    Me.Label9.Visible = True
                    ''rafay
                ElseIf Rights.Item(i).FormControlName = "Update" Then
                    btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Delete" Then
                    'DoHaveDeleteRights = True
                ElseIf Rights.Item(i).FormControlName = "Export" Then
                    Me.CtrlGrdBar2.mGridExport.Enabled = True
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                    'Rafay:Task End
                    'Changes add by Murtaza Ahmad (11/21/2022)
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "InvoiceAmount" Then
                    Me.txtAmount.Visible = True
                    'Changes add by Murtaza Ahmad (11/21/2022)
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            FillCombos("Customer")
            FillCombos("Currency")
            FillCombos("Opportunity")
            FillCombos("Item")
            FillCombos("Invoice")
            FillCombos("Employee")
            'rafay
            FillCombos("PreviousContracts")
            Terminate.Visible = False
            cmbRenew.Visible = False
            cmbterminate.Visible = False
            PreviousContracts.Visible = False
            cmbHold.Visible = False
            lblOthersDescription.Visible = False
            txtOthersDescription.Visible = False
            'rafay
            UltraDropDownSearching(cmbOpportunity, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbCustomer, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            ReSetControls()
            GetAllRecords()
            
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty

            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
            "All files (.)|*.*"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a
                If Me.btnSave.Text <> "&Save" Then
                    If Me.GrdStatus.RowCount > 0 Then
                        intCountAttachedFiles = Val(GrdStatus.CurrentRow.Cells("No Of Attachment").Value)
                    End If
                End If
                Me.ToolStripButton1.Text = "Attachment (" & arrFile.Count + intCountAttachedFiles & ")"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function SaveDocument(ByVal DocId As Integer, ByVal Source As String, ByVal objTrans As OleDb.OleDbTransaction) As Boolean
        Dim cmd As New OleDbCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            dt.AcceptChanges()


            Dim objdt As New DataTable
            objdt = GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            Dim intId As Integer = objdt.Rows(0)(0)

            Dim strSQL As String = String.Empty
            cmd.CommandText = String.Empty
            strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            Dim objPath As String = getConfigValueByType("FileAttachmentPath").ToString

            If arrFile.Count > 0 Then
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If
                        Dim New_Files As String = intId & "_" & DocId & "_CO_" & Date.Now.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
                        Dim dr As DataRow
                        dr = dt.NewRow
                        dr(0) = DocId
                        dr(1) = Source
                        dr(2) = objPath & "\" & New_Files
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                        If IO.File.Exists(objPath & "\" & New_Files) Then
                            IO.File.Delete(objPath & "\" & New_Files)
                        End If
                        IO.File.Copy(objFile, objPath & "\" & New_Files)
                        intId += 1
                    End If
                Next
            End If


            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        Dim strPath As String = objPath
                        Dim strFileName As String = r.Item("FileNames").ToString.Substring(r.Item("FileNames").ToString.LastIndexOf("\") + 1)
                        cmd.CommandText = String.Empty
                        strSQL = "INSERT INTO DocumentAttachment(DocId, Source, FileName, Path) Values(" & Val(r("DocId").ToString) & ",N'" & r.Item("Source").ToString.Replace("'", "''") & "', N'" & strFileName.Replace("'", "''") & "', N'" & strPath.Replace("'", "''") & "')"
                        cmd.CommandText = strSQL
                        cmd.ExecuteNonQuery()
                    Next
                End If
            End If


        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function

    Private Sub GrdStatus_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GrdStatus.LinkClicked
        Try

            If e.Column.Key = "No Of Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.Name
                frm._VoucherId = Me.GrdStatus.GetRow.Cells("ContractId").Value.ToString
                'frm.RemoveAttachmentForSalesOrder = flgRemoveAttachment
                frm.ShowDialog()
                GetAllRecords()
                Exit Sub
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PurchaseInvoiceToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub AMCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AMCToolStripMenuItem.Click
        'Abdul Rafay: show the report of amc (task given by adil bhai)
        AddRptParam("@id", Val(Me.GrdStatus.CurrentRow.Cells("ContractId").Value))
        ShowReport("contract")
        'ShowReport("contract", "{contractmastertable.contractdetailid}=" & grdSaved.CurrentRow.Cells("PurchaseOrderId").Value
    End Sub

    Private Sub CtrlGrdBar2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdStatus.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdStatus.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.GrdStatus.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            ' CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdItems.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdItems.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdItems.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            ' CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub GrdStatus_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles GrdStatus.FormattingRow

    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged

    End Sub

    Private Sub cmbStatus_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbStatus.SelectedValueChanged
        'Rafay
        Try
            If cmbStatus.SelectedItem = "Renew" Then
                cmbRenew.Visible = True
                PreviousContracts.Visible = True
                cmbterminate.Visible = False
                Terminate.Visible = False
            ElseIf cmbStatus.SelectedItem = "Terminate" Then
                cmbterminate.Visible = True
                Terminate.Visible = True
            ElseIf cmbStatus.SelectedItem = "New" Then
                cmbRenew.Visible = False
                PreviousContracts.Visible = False
                cmbterminate.Visible = False
                Terminate.Visible = False
            End If
        Catch ex As Exception

        End Try
        'Rafay
    End Sub

    
    Private Sub HoldCheckbox_CheckedChanged_1(sender As Object, e As EventArgs) Handles HoldCheckbox.CheckedChanged
        'rafay
        If HoldCheckbox.Checked = True Then
            cmbHold.Visible = True
        Else
            cmbHold.Visible = False
        End If
        ''rafay
    End Sub
    'rafay
    Private Sub cmbHold_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbHold.SelectedValueChanged
        If cmbHold.SelectedItem = "Others" Then
            lblOthersDescription.Visible = True
            txtOthersDescription.Visible = True
        Else
            lblOthersDescription.Visible = False
            txtOthersDescription.Visible = False
        End If
    End Sub
    'rafay

    Private Sub cmbStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStatus.SelectedIndexChanged

    End Sub

    Private Sub cmbRenew_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRenew.SelectedIndexChanged

    End Sub

    Private Sub cmbOpportunity_InitializeLayout(sender As Object, e As InitializeLayoutEventArgs)

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim dtinvoicedetails1 As DataTable = CType(Me.grdInvoiceDetails.DataSource, DataTable)
            'dtinvoicedetails1.Columns.Add("InvoiceDate")
            'dtinvoicedetails1.Columns.Add("InvoiceAmount")
            'grdInvoiceDetails.DataSource = dtinvoicedetails1
            Dim dr As DataRow = dtinvoicedetails1.NewRow()
            dtinvoicedetails1.Rows.InsertAt(dr, 0)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoSameAmount_CheckedChanged(sender As Object, e As EventArgs) Handles rdoSameAmount.CheckedChanged
        Try
            If rdoSameAmount.Checked = True Then
                pnlInvoiceGrd.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoDifferentAmount_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDifferentAmount.CheckedChanged
        Try
            If rdoDifferentAmount.Checked = True Then
                pnlInvoiceGrd.Visible = True
                Me.grdInvoiceDetails.RootTable.Columns("InvoiceDate").Visible = False
                Me.grdInvoiceDetails.RootTable.Columns("InvoiceAmount").Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdoMannualAmount_CheckedChanged(sender As Object, e As EventArgs) Handles rdoMannualAmount.CheckedChanged
        Try
            If rdoMannualAmount.Checked = True Then
                pnlInvoiceGrd.Visible = True
                Me.grdInvoiceDetails.RootTable.Columns("InvoiceDate").Visible = True
                Me.grdInvoiceDetails.RootTable.Columns("InvoiceAmount").Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

