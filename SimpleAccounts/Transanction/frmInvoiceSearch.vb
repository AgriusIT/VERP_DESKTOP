Imports SBModel
Imports SBDal
Imports System.Data.SqlClient

Public Class frmInvoiceSearch
    Implements IGeneral

    Public StoreIssue As Boolean
    Public ItemsConsumption As Boolean
    Dim LocationId As Integer = 0
    Public dtMerging As DataTable
    Public IsWIPAccount As Boolean = False
    Public PlanId As Integer = 0
    Public TicketId As Integer = 0

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
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    str = "SELECT   coa_detail_id, detail_title AS Name FROM vwCOADetail WHERE (account_type in ( 'Customer','Vendor' )) AND (coa_detail_id IS NOT NULL) AND (Active = 1) ORDER BY Name"
                Else
                    str = "SELECT   coa_detail_id, detail_title AS Name FROM vwCOADetail WHERE (account_type = 'Customer') AND (coa_detail_id IS NOT NULL) AND (Active = 1) ORDER BY Name"
                End If
                FillUltraDropDown(cmbCustomer, str)
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Name").Width = 300
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            grdItems.DataSource = Nothing
            cmbCustomer.Value = frmCustomerCollection.cmbAccounts.Value
            If cmbCustomer.Value > 0 Then
                cmbCustomer_ValueChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

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
            FillCombos("Customer")
            FillCombos("Invoice")
            UltraDropDownSearching(cmbCustomer, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub


    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'Try
        '    If StoreIssue = True Then
        '        'If Me.cmbStage.SelectedValue > 0 Then

        '        'End If
        '        Dim dt As New DataTable
        '        dt = PlanTicketsStandardDAL.GetTicketRecordForStoreIssuance(Me.cmbSalesOrder.SelectedValue, Me.cmbPlanNo.SelectedValue, Me.cmbTicketNo.SelectedValue, Me.cmbStage.SelectedValue)
        '        For Each Row As DataRow In dt.Rows
        '            SetWIPAccount(Row, dt)
        '        Next
        '        Me.grdItems.DataSource = dt
        '        Me.grdItems.RootTable.Columns("PendingQty").FormatString = "N" & DecimalPointInValue
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)

        'End Try
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
                'If IsWIPAccount = False Then
                '    IsWIPAccount = True
                'End If
                'dt.Rows.Remove(_Row)
                'dt.AcceptChanges()
                'dtMerging.Rows.Add(_Row)
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

            Me.grdItems.RootTable.Columns("PendingQty").FormatString = "N" & DecimalPointInValue
            
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'Private Sub grdItems_KeyDown(sender As Object, e As KeyEventArgs)

    '    Dim dt As New DataTable
    '    Dim dt1 As New DataTable
    '    Try
    '        If StoreIssue = True Then
    '            dt.Columns.Add("LocationID", System.Type.GetType("System.Int32"))
    '            dt.Columns.Add("ArticleCode", System.Type.GetType("System.String"))
    '            dt.Columns.Add("item", System.Type.GetType("System.String"))
    '            dt.Columns.Add("Color", System.Type.GetType("System.String"))
    '            dt.Columns.Add("BatchNo", System.Type.GetType("System.String"))
    '            dt.Columns.Add("unit", System.Type.GetType("System.String"))
    '            dt.Columns.Add("Qty", System.Type.GetType("System.Double"))
    '            dt.Columns.Add("Rate", System.Type.GetType("System.Double"))
    '            dt.Columns.Add("Total", System.Type.GetType("System.Double"))
    '            dt.Columns.Add("CategoryId", System.Type.GetType("System.Int32"))
    '            dt.Columns.Add("ItemId", System.Type.GetType("System.Int32"))
    '            dt.Columns.Add("PackQty", System.Type.GetType("System.Double"))
    '            dt.Columns.Add("CurrentPrice", System.Type.GetType("System.Double"))
    '            dt.Columns.Add("PackPrice", System.Type.GetType("System.Double"))
    '            dt.Columns.Add("BatchID", System.Type.GetType("System.Int32"))
    '            dt.Columns.Add("ArticleDefMasterId", System.Type.GetType("System.Int32"))
    '            dt.Columns.Add("ArticleDescriptionMaster", System.Type.GetType("System.String"))
    '            dt.Columns.Add("Pack_Desc", System.Type.GetType("System.String"))
    '            dt.Columns.Add("PurchaseAccountId", System.Type.GetType("System.Int32"))
    '            dt.Columns.Add("CGSAccountId", System.Type.GetType("System.Int32"))
    '            dt.Columns.Add("CostPrice", System.Type.GetType("System.Double"))
    '            dt.Columns.Add("PlanUnit", System.Type.GetType("System.String"))
    '            dt.Columns.Add("PlanNo", System.Type.GetType("System.String"))
    '            dt.Columns.Add("PlanQty", System.Type.GetType("System.Double"))
    '            dt.Columns.Add("TicketNo", System.Type.GetType("System.String"))
    '            dt.Columns.Add("TicketQty", System.Type.GetType("System.Double"))
    '            dt.Columns.Add("LotNo", System.Type.GetType("System.String"))
    '            dt.Columns.Add("Rack_No", System.Type.GetType("System.String"))
    '            dt.Columns.Add("Comments", System.Type.GetType("System.String"))

    '            dt.Columns.Add("Stock", System.Type.GetType("System.Double"))

    '            dt.Columns.Add("TotalQty", System.Type.GetType("System.Double"))

    '            dt.Columns.Add("SubDepartmentID", System.Type.GetType("System.Int32"))
    '            dt.Columns.Add("SubDepartment", System.Type.GetType("System.String"))
    '            dt.Columns.Add("AllocationDetailId", System.Type.GetType("System.Int32"))
    '            dt.Columns.Add("ParentId", System.Type.GetType("System.Int32"))
    '            dt.Columns.Add("EstimationId", System.Type.GetType("System.Int32"))
    '            dt.Columns.Add("EstimatedQty", System.Type.GetType("System.Double"))
    '            dt.Columns.Add("DispatchId", System.Type.GetType("System.Int32"))
    '            dt.Columns.Add("DispatchDetailId", System.Type.GetType("System.Int32"))
    '            dt.Columns.Add("CheckQty", System.Type.GetType("System.Double"))
    '            dt.Columns.Add("IssuedQty", System.Type.GetType("System.Double"))
    '            dt.Columns.Add("SubItem", GetType(Boolean))
    '            dt.Columns.Add("TicketId", System.Type.GetType("System.Int32"))
    '            dt.Columns.Add("WIPAccountId", System.Type.GetType("System.Int32"))

    '            'dt.Columns.Add("TicketNo", System.Type.GetType("System.String"))
    '            ''
    '            'dt.Columns.Add("FinishGood", System.Type.GetType("System.String"))
    '            'dt.Columns.Add("PlanQty", System.Type.GetType("System.Double"))
    '            'dt.Columns.Add("TicketQty", System.Type.GetType("System.Double"))
    '        ElseIf ItemsConsumption = True Then

    '            dt1.Columns.Add("LocationId")
    '            dt1.Columns.Add("Location")
    '            dt1.Columns.Add("ConsumptionDetailId")
    '            dt1.Columns.Add("ConsumptionId")
    '            dt1.Columns.Add("ArticleId")
    '            dt1.Columns.Add("ArticleCode")
    '            dt1.Columns.Add("ArticleDescription")
    '            dt1.Columns.Add("Color")
    '            dt1.Columns.Add("Qty")
    '            dt1.Columns.Add("ConsumedQty")
    '            dt1.Columns.Add("AvailableQty")
    '            dt1.Columns.Add("Rate")
    '            dt1.Columns.Add("Total")
    '            dt1.Columns.Add("DispatchId")
    '            dt1.Columns.Add("DispatchDetailId")
    '            dt1.Columns.Add("CGAccountId")
    '            dt1.Columns.Add("Comments")
    '            dt1.Columns.Add("CheckQty")
    '            dt1.Columns.Add("DepartmentId")
    '            dt1.Columns.Add("TotalIssuedQty")
    '            dt1.Columns.Add("TotalConsumedQty")
    '            dt1.Columns.Add("TotalReturnedQty")

    '        End If

    '        Dim TotalRows As Integer = grdItems.GetCheckedRows.Length
    '        For Each row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows

    '            Dim R As DataRow = dt.NewRow

    '            R("LocationID") = Val(LocationId)
    '            R("ArticleCode") = row.Cells("Code").Value.ToString
    '            R("Item") = row.Cells("ProductName").Value.ToString
    '            R("Color") = row.Cells("Color").Value.ToString
    '            R("BatchNo") = String.Empty
    '            R("Unit") = String.Empty
    '            R("Qty") = Val(row.Cells("PendingQty").Value.ToString)
    '            R("Rate") = Val(row.Cells("Rate").Value.ToString)
    '            R("Total") = Val(row.Cells("Total").Value.ToString)
    '            R("CategoryId") = 0
    '            R("ItemId") = Val(row.Cells("ArticleId").Value.ToString)
    '            R("PackQty") = 0
    '            R("CurrentPrice") = 0
    '            R("PackPrice") = 0
    '            R("BatchId") = 0
    '            R("ArticleDefMasterId") = Val(row.Cells("MasterArticleId").Value.ToString)
    '            R("ArticleDescriptionMaster") = row.Cells("FinishGood").Value.ToString
    '            R("Pack_Desc") = String.Empty
    '            R("PurchaseAccountId") = Val(row.Cells("PurchaseAccountId").Value.ToString)
    '            R("CGSAccountId") = Val(row.Cells("CGSAccountId").Value.ToString)
    '            R("CostPrice") = 0
    '            R("PlanUnit") = String.Empty
    '            R("PlanNo") = row.Cells("PlanNo").Value.ToString
    '            R("PlanQty") = Val(row.Cells("PlanQty").Value.ToString)
    '            R("TicketNo") = row.Cells("BatchNo").Value.ToString
    '            R("TicketQty") = Val(row.Cells("TicketQty").Value.ToString)
    '            R("LotNo") = String.Empty
    '            R("Rack_No") = String.Empty
    '            R("Comments") = String.Empty
    '            R("Stock") = 0
    '            R("TotalQty") = Val(row.Cells("PendingQty").Value.ToString)
    '            R("SubDepartmentID") = Val(row.Cells("DepartmentId").Value.ToString)
    '            R("SubDepartment") = row.Cells("Stage").Value
    '            R("AllocationDetailId") = 0
    '            R("ParentId") = 0
    '            R("EstimationId") = 0
    '            R("EstimatedQty") = Val(row.Cells("EstimatedQty").Value.ToString)
    '            R("DispatchId") = 0
    '            R("DispatchDetailId") = 0
    '            R("CheckQty") = Val(row.Cells("CheckQty").Value.ToString)
    '            R("IssuedQty") = Val(row.Cells("IssuedQty").Value.ToString)
    '            R("SubItem") = 0
    '            'R("DepartmentId") = Val(row.Cells("DepartmentId").Value.ToString)
    '            'R("Department") = row.Cells("Stage").Value
    '            R("TicketId") = Val(row.Cells("TicketId").Value.ToString)
    '            R("TicketNo") = row.Cells("BatchNo").Value.ToString
    '            R("WIPAccountId") = Val(row.Cells("WIPAccountId").Value.ToString)
    '            'R("TicketNo") = row.Cells("TicketNo").Value.ToString

    '            If R("WIPAccountId") > 0 Then
    '                If IsWIPAccount = False Then
    '                    IsWIPAccount = True
    '                End If
    '            End If
    '            'TotalRows -= 1
    '            'If TotalRows < 1 Then
    '            If Val(row.Cells("PlanId").Value.ToString) > 0 Then
    '                PlanId = Val(row.Cells("PlanId").Value.ToString)
    '            Else
    '                PlanId = 0
    '            End If
    '            If Val(row.Cells("TicketId").Value.ToString) > 0 Then
    '                TicketId = Val(row.Cells("TicketId").Value.ToString)
    '            Else
    '                TicketId = 0
    '            End If
    '            'If Val(row.Cells("CostCenterId").Value.ToString) > 0 Then
    '            '    CostCenterId = Val(row.Cells("CostCenterId").Value.ToString)
    '            'Else
    '            '    CostCenterId = 0
    '            'End If

    '            'End If
    '            ''
    '            'R("FinishGood") = row.Cells("FinishGood").Value.ToString
    '            'R("PlanQty") = Val(row.Cells("PlanQty").Value.ToString)
    '            'R("TicketQty") = Val(row.Cells("TicketQty").Value.ToString)
    '            dt.Rows.Add(R)
    '        Next
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    '    Try
    '        If ItemsConsumption = True Then
    '            frmItemsConsumption.fillItemsConsumptionGrid(dt1, 0, 0) 'cmbTicketNo.SelectedValue, CType(cmbTicketNo.SelectedItem, DataRowView).Item("PlanID")
    '            Me.Close()
    '        ElseIf StoreIssue = True Then
    '            frmStoreIssuenceNew.fillReturnStoreIssuenceGrid(dt, TicketId, PlanId, IsWIPAccount)
    '            frmStoreIssuenceNew.IsEstimation = True
    '            IsWIPAccount = False
    '            Me.Close()
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try

    'End Sub

    
    Private Sub frmTicketProducts_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnLoad_Click(sender As Object, e As EventArgs) Handles BtnLoad.Click
        Try
            'frmCustomerCollection.cmbInvoice.Visible = True
            'frmCustomerCollection.lblInvoice.Visible = True
            For Each row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows
                frmCustomerCollection.cmbAccounts.Value = cmbCustomer.Value
                Dim str As String = "SELECT SalesTaxAccId from tblDefSalesTaxAccount where AccountId = " & Val(getConfigValueByType("SalesTaxCreditAccount").ToString) & ""
                Dim dt As DataTable = GetDataTable(str)
                Dim SalesTaxAccount As Integer
                If dt.Rows.Count > 0 Then
                    SalesTaxAccount = dt.Rows(0).Item("SalesTaxAccId")
                End If
                frmCustomerCollection.cmbCurrency.SelectedValue = row.Cells("CurrencyId").Value.ToString
                frmCustomerCollection.txtCurrencyRate.Text = row.Cells("CurrencyRate").Value.ToString
                frmCustomerCollection.InvoiceCurrencyRate = row.Cells("CurrencyRate").Value.ToString
                'frmCustomerCollection.cmbSalesTax.SelectedValue = SalesTaxAccount
                frmCustomerCollection.txtAmount.Text = Math.Round(Val(row.Cells("Price").Value.ToString), TotalAmountRounding)
                frmCustomerCollection.InvoiceTotalAmount = Math.Round(Val(row.Cells("Price").Value.ToString), TotalAmountRounding) * row.Cells("CurrencyRate").Value.ToString
                'frmCustomerCollection.txtDiscount.Text = row.Cells("DiscountValue").Value.ToString ''Commented agianst TFS4683 to avoid sales discount on receiving
                'frmCustomerCollection.txtSalesTax.Text = row.Cells("SalesTax").Value.ToString
                frmCustomerCollection.cmbInvoice.Value = row.Cells("SalesId").Value.ToString
                frmCustomerCollection.txtReference.Text = "Payment Rec against Invoice No. " & row.Cells("SalesNo").Value.ToString & ""
                ''frmCustomerCollection.btnAdd_Click(Nothing, Nothing)
            Next
            Me.Close()
            ' frmcustomercollection.txtamount.text = 
            'grdItems_KeyDown(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCustomer_ValueChanged(sender As Object, e As EventArgs) Handles cmbCustomer.ValueChanged
        Try
            Dim str As String
            Dim dt As DataTable
            'str = "SELECT SalesMasterTable.SalesId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, Price.Qty, Price.Price, Price.DiscountValue, Price.SalesTax FROM SalesMasterTable INNER JOIN (SELECT SalesDetailTable.SalesId, (SUM(ISNULL(SalesDetailTable.CurrencyAmount, 0)) + (SUM(ISNULL(SalesDetailTable.CurrencyAmount, 0)) * ISNULL(SalesDetailTable.TaxPercent, 0) / 100) + (SUM(ISNULL(SalesDetailTable.CurrencyAmount, 0)) * ISNULL(SalesDetailTable.SEDPercent, 0) / 100) - ISNULL(SalesMasterTable.TotalOutwardExpense, 0)) AS Price, SUM(ISNULL(SalesDetailTable.Qty, 0)) AS Qty, ISNULL(SUM(SalesDetailTable.DiscountValue), 0) AS DiscountValue, (SUM(ISNULL(SalesDetailTable.TaxPercent, 0)) + SUM(ISNULL(SalesDetailTable.SEDPercent, 0))) / COUNT(ISNULL(SalesDetailTable.TaxPercent, 0)) AS SalesTax FROM SalesDetailTable LEFT OUTER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId GROUP BY SalesDetailTable.SalesId, SalesDetailTable.TaxPercent, SalesDetailTable.SEDPercent, SalesMasterTable.TotalOutwardExpense) AS Price ON SalesMasterTable.SalesId = Price.SalesId where CustomerCode = " & cmbCustomer.Value & "AND SalesmasterTable.SalesId not in (select isnull(InvoiceId,0) from tblVoucherDetail)"
            ''TFS4683 : Ayesha Rehman :02-10-2018 : Query Changes to get Voucher Effect also on partially receiving the Sales Invoice
            'Ali Faisal : Net Amount can not be Tax Excluding on Receipt.
            str = "Select SalesId, a.SalesNo, a.SalesDate, a.Qty-a.ReturnQty as Qty, a.SalesAmount-a.[Return Amount] as SalesAmount ,DiscountValue, a.SalesTax ,a.CurrencyId, a.[Return Amount], a.CurrencyRate,  " _
        & " (Isnull(ReceiptAmount,0)+IsNull(Adj.Adj_Amount,0)) As ReceivedAmount, a.ConvertedSalesAmount, " _
        & " (((Isnull(a.SalesAmount,0)*a.CurrencyRate)-(Isnull(ReceiptAmount,0)+IsNull(Adj.Adj_Amount,0)+ISNULL([Return Amount],0)))) as [Converted Price] , " _
        & " (((Isnull(a.SalesAmount,0)*a.CurrencyRate)-(Isnull(ReceiptAmount,0)+IsNull(Adj.Adj_Amount,0)+ISNULL([Return Amount],0))))/a.CurrencyRate as [Price]  " _
        & " From ( SELECT SalesDetailTable.SalesId,SalesMasterTable.SalesNo, SalesMasterTable.SalesDate,  SUM(ISNULL(SalesDetailTable.Qty, 0)) AS Qty, SUM(isnull(srdt.Qty,0))as ReturnQty,ISNULL(SUM(SalesDetailTable.DiscountValue), 0) AS DiscountValue,SUM((ISNULL(TaxPercent, 0) / 100) * (ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0))) AS TaxAmount, " _
        & " SUM((ISNULL(SEDPercent, 0) / 100) * (ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0))) AS SedTaxAmount, Sum(SalesDetailTable.PostDiscountPrice*SalesDetailTable.Qty) + ((SalesDetailTable.TaxPercent/100)*Sum(SalesDetailTable.PostDiscountPrice*SalesDetailTable.Qty)) as SalesAmount, Sum(SalesDetailTable.PostDiscountPrice*SalesDetailTable.Qty*SalesDetailTable.CurrencyRate) + ((SalesDetailTable.TaxPercent/100)*Sum(SalesDetailTable.PostDiscountPrice*SalesDetailTable.Qty*SalesDetailTable.CurrencyRate)) as ConvertedSalesAmount ,(SUM(ISNULL(SalesDetailTable.TaxPercent, 0)) + SUM(ISNULL(SalesDetailTable.SEDPercent, 0))) / COUNT(ISNULL(SalesDetailTable.TaxPercent, 0)) AS SalesTax, ISNULL(SalesDetailTable.CurrencyId, 1) as CurrencyId, ISNULL(SalesDetailTable.CurrencyRate, 1) as CurrencyRate " _
        & " ,Sum(SRDT.Price*SRDT.Qty) + ((SRDT.Tax_Percent/100)*Sum(SRDT.Price*SRDT.Qty)) as [Return Amount] ,Sum(SRDT.Price*SRDT.Qty*SRDT.CurrencyRate) + ((SRDT.Tax_Percent/100)*Sum(SRDT.Price*SRDT.Qty*SRDT.CurrencyRate)) as [Converted Return Amount] " _
        & " FROM SalesDetailTable INNER JOIN SalesMasterTable on SalesMasterTable.SalesId = SalesDetailTable.SalesId " _
        & " And  CustomerCode =  " & cmbCustomer.Value & " " _
        & " LEFT JOIN SalesReturnMasterTable SRMT ON SRMT.POId=SalesMasterTable.SalesId " _
        & " LEFT JOIN SalesReturnDetailTable SRDT on SRDT.SalesReturnId=SRMT.SalesReturnId  " _
        & " GROUP BY SalesDetailTable.SalesId,SalesNo,SalesDate,SalesDetailTable.CurrencyId,SalesDetailTable.CurrencyRate,Isnull(SalesAmount,0),SalesDetailTable.TaxPercent , srdt.Tax_Percent " _
        & " HAVING   (IsNull(SalesAmount,0) <> 0)) a  " _
        & " LEFT JOIN (SELECT     InvoiceId, IsNull(IsNull(SUM(ReceiptAmount),0),0) AS ReceiptAmount FROM  dbo.InvoiceBasedReceiptsDetails  " _
        & " GROUP BY InvoiceId ) Receipt on Receipt.InvoiceId = a.SalesId  " _
        & " LEFT JOIN (SELECT     InvoiceId, IsNull(IsNull(SUM(NetAmount),0),0) AS VocherAmount FROM  dbo.tblVoucherDetail  " _
        & " GROUP BY InvoiceId ) Vocher on Vocher.InvoiceId = a.SalesId  " _
        & " LEFT JOIN(Select InvoiceId, coa_detail_id, IsNull(SUM(IsNull(AdjustmentAmount,0)),0) as Adj_Amount from InvoiceAdjustmentTable WHERE InvoiceType='Sales' Group By InvoiceId, coa_detail_id) " _
        & " Adj on Adj.InvoiceId = a.SalesId " _
        & " where a.ConvertedSalesAmount > (Isnull(ReceiptAmount, 0) + IsNull(Adj.Adj_Amount, 0)) "
            '" Select SalesId, a.SalesNo, a.SalesDate, a.Qty, SalesAmount ,DiscountValue, a.SalesTax ,a.CurrencyId, a.CurrencyRate,(Isnull(ReceiptAmount,0)+IsNull(Adj.Adj_Amount,0)) As ReceivedAmount," _
            '  & " ((Isnull(SalesAmount,0))-(Isnull(ReceiptAmount,0)+IsNull(Adj.Adj_Amount,0))) as [Price] " _
            '  & " From ( SELECT SalesDetailTable.SalesId,SalesMasterTable.SalesNo, SalesMasterTable.SalesDate,  SUM(ISNULL(SalesDetailTable.Qty, 0)) AS Qty,ISNULL(SUM(SalesDetailTable.DiscountValue), 0) AS DiscountValue,SUM((ISNULL(TaxPercent, 0) / 100) * (ISNULL(Qty, 0) * ISNULL(Price, 0))) AS TaxAmount, " _
            '  & " SUM((ISNULL(SEDPercent, 0) / 100) * (ISNULL(Qty, 0) * ISNULL(Price, 0))) AS SedTaxAmount, (Isnull(SalesAmount,0)/ISNULL(SalesDetailTable.CurrencyRate, 1)) + SUM((ISNULL(TaxPercent, 0) / 100) * (ISNULL(Qty, 0) * ISNULL(Price, 0))) as SalesAmount, (SUM(ISNULL(SalesDetailTable.TaxPercent, 0)) + SUM(ISNULL(SalesDetailTable.SEDPercent, 0))) / COUNT(ISNULL(SalesDetailTable.TaxPercent, 0)) AS SalesTax, ISNULL(SalesDetailTable.CurrencyId, 1) as CurrencyId, ISNULL(SalesDetailTable.CurrencyRate, 1) as CurrencyRate " _
            '  & " FROM  dbo.SalesDetailTable INNER JOIN SalesMasterTable on SalesMasterTable.SalesId = SalesDetailTable.SalesId " _
            '  & " And  CustomerCode = " & cmbCustomer.Value & " GROUP BY SalesDetailTable.SalesId,SalesNo,SalesDate,CurrencyId,CurrencyRate,Isnull(SalesAmount,0) " _
            '  & " HAVING   (IsNull(SalesAmount,0) <> 0)) a " _
            '  & " LEFT OUTER JOIN (SELECT     InvoiceId, IsNull(IsNull(SUM(ReceiptAmount),0),0) AS ReceiptAmount FROM  dbo.InvoiceBasedReceiptsDetails " _
            '  & " GROUP BY InvoiceId ) Receipt on Receipt.InvoiceId = a.SalesId " _
            '  & " LEFT OUTER JOIN (SELECT     InvoiceId, IsNull(IsNull(SUM(NetAmount),0),0) AS VocherAmount FROM  dbo.tblVoucherDetail " _
            '  & " GROUP BY InvoiceId ) Vocher on Vocher.InvoiceId = a.SalesId " _
            '  & " LEFT OUTER JOIN(Select InvoiceId, coa_detail_id, IsNull(SUM(IsNull(AdjustmentAmount,0)),0) as Adj_Amount from InvoiceAdjustmentTable WHERE InvoiceType='Sales' Group By InvoiceId, coa_detail_id) " _
            '  & " Adj on Adj.InvoiceId = a.SalesId " _
            '  & " where SalesAmount > (Isnull(ReceiptAmount,0)+IsNull(Adj.Adj_Amount,0)) "

            If Me.dtpFromDate.Checked = True Then
                str += " AND SalesDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
                    End If
            If Me.dtpToDate.Checked = True Then
                str += " AND SalesDate <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
                    End If
            str += "Order By  a.SalesId desc"
            dt = GetDataTable(str)
            Me.grdItems.DataSource = dt
            Me.grdItems.RootTable.Columns("SalesDate").FormatString = str_DisplayDateFormat
                    'grdItems.RetrieveStructure()
                    'grdItems.AutoSizeColumns()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub cmbCustomer_ValueChanged(sender As Object, e As EventArgs) Handles cmbCustomer.ValueChanged
    '    Try
    '        FillCombos("Invoice")
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub grdItems_KeyDown(sender As Object, e As KeyEventArgs) Handles grdItems.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                BtnLoad_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class