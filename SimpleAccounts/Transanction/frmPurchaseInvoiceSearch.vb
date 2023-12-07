Imports SBModel
Imports SBDal
Imports System.Data.SqlClient

Public Class frmPurchaseInvoiceSearch
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
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    str = "SELECT   coa_detail_id, detail_title AS Name FROM vwCOADetail WHERE (account_type in ( 'Customer','Vendor' )) AND (coa_detail_id IS NOT NULL) AND (Active = 1) ORDER BY Name"
                Else
                    str = "SELECT   coa_detail_id, detail_title AS Name FROM vwCOADetail WHERE (account_type = 'Vendor') AND (coa_detail_id IS NOT NULL) AND (Active = 1) ORDER BY Name"
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
            cmbCustomer.Value = frmPaymentNew.cmbAccounts.Value
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
            'frmPaymentNew.cmbInvoice.Visible = True
            'frmPaymentNew.lblInvoice.Visible = True
            For Each row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows
                frmPaymentNew.cmbAccounts.Value = cmbCustomer.Value
                Dim str As String = "SELECT SalesTaxAccId from tblDefSalesTaxAccount where AccountId = " & Val(getConfigValueByType("SalesTaxCreditAccount").ToString) & ""
                Dim dt As DataTable = GetDataTable(str)
                Dim SalesTaxAccount As Integer
                If dt.Rows.Count > 0 Then
                    SalesTaxAccount = dt.Rows(0).Item("SalesTaxAccId")
                End If
                frmPaymentNew.cmbCurrency.SelectedValue = row.Cells("CurrencyId").Value.ToString
                frmPaymentNew.txtCurrencyRate.Text = row.Cells("CurrencyRate").Value.ToString
                frmPaymentNew.InvoiceCurrencyRate = row.Cells("CurrencyRate").Value.ToString
                'frmPaymentNew.cmbSalesTax.SelectedValue = SalesTaxAccount
                'frmPaymentNew.txtAmount.Text = row.Cells("Price").Value.ToString
                If rbtAll.Checked = True Then
                    frmPaymentNew.txtAmount.Text = Math.Round(Val(row.Cells("Price").Value.ToString), TotalAmountRounding)
                    frmPaymentNew.InvoiceTotalAmount = Math.Round(Val(row.Cells("Price").Value.ToString), TotalAmountRounding) * row.Cells("CurrencyRate").Value.ToString
                Else
                    frmPaymentNew.txtAmount.Text = Math.Round(Val(txtPartialAmount.Text), TotalAmountRounding)
                    frmPaymentNew.InvoiceTotalAmount = Math.Round(Val(txtPartialAmount.Text), TotalAmountRounding) * row.Cells("CurrencyRate").Value.ToString
                End If
                
                'frmPaymentNew.txtDiscount.Text = row.Cells("DiscountValue").Value.ToString
                'frmPaymentNew.txtSalesTax.Text = row.Cells("PurchaseTax").Value.ToString
                frmPaymentNew.cmbInvoice.Value = row.Cells("ReceivingId").Value.ToString
                frmPaymentNew.txtReference.Text = "Payment submit against Invoice No. " & row.Cells("ReceivingNo").Value.ToString & ""
                'frmPaymentNew.btnAdd_Click(Nothing, Nothing)
            Next
            Me.Close()
            ' frmPaymentNew.txtamount.text = 
            'grdItems_KeyDown(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCustomer_ValueChanged(sender As Object, e As EventArgs) Handles cmbCustomer.ValueChanged
        Try
            Dim str As String
            Dim dt As DataTable
            ' str = "SELECT ReceivingMasterTable.ReceivingId, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.ReceivingDate, Price.Qty, Price.Price, Price.PurchaseTax FROM ReceivingMasterTable INNER JOIN (SELECT ReceivingDetailTable.ReceivingId, (SUM(ISNULL(ReceivingDetailTable.CurrencyAmount, 0)) + (SUM(ISNULL(ReceivingDetailTable.CurrencyAmount, 0)) * ISNULL(ReceivingDetailTable.TaxPercent, 0) / 100) - ISNULL(ReceivingMasterTable.TotalInwardExpense, 0)) AS Price, SUM(ISNULL(ReceivingDetailTable.Qty, 0)) AS Qty, SUM(ISNULL(ReceivingDetailTable.TaxPercent, 0)) AS PurchaseTax FROM ReceivingDetailTable LEFT OUTER JOIN ReceivingMasterTable ON ReceivingDetailTable.ReceivingId = ReceivingMasterTable.ReceivingId GROUP BY ReceivingDetailTable.ReceivingId, ReceivingDetailTable.TaxPercent, ReceivingMasterTable.TotalInwardExpense) AS Price ON ReceivingMasterTable.ReceivingId = Price.ReceivingId where VendorId = " & cmbCustomer.Value & "AND ReceivingMasterTable.ReceivingId not in (select isnull(InvoiceId,0) from tblVoucherDetail)"
            ''TFS4683 : Ayesha Rehman :02-10-2018 : Query Changes to get Voucher Effect also on partially receiving the Purchase Invoice
            'str = " Select ReceivingId, ReceivingNo, ReceivingDate, a.Qty,ReceivingAmount,a.PurchaseTax ,(Isnull(PaymentAmount,0)+IsNull(Adj.Adj_Amount,0) + IsNull(Vocher.VocherAmount,0)) As ReceivedAmount,((Isnull(ReceivingAmount,0))-(Isnull(PaymentAmount,0)+IsNull(Adj.Adj_Amount,0)+ IsNull(Vocher.VocherAmount,0))) as [Price] " _
            '      & " From (SELECT    ReceivingDetailTable.ReceivingId,ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.ReceivingDate,  SUM(ISNULL(ReceivingDetailTable.Qty, 0)) AS Qty," _
            '      & " Isnull(ReceivingAmount,0) as ReceivingAmount, SUM(ISNULL(ReceivingDetailTable.TaxPercent, 0)) AS PurchaseTax " _
            '      & " FROM         dbo.ReceivingDetailTable INNER JOIN ReceivingMasterTable on ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId " _
            '      & " And  VendorId = " & cmbCustomer.Value & " GROUP BY ReceivingDetailTable.ReceivingId,ReceivingNo,ReceivingDate,Isnull(ReceivingAmount,0)" _
            '      & " HAVING(IsNull(ReceivingAmount, 0) <> 0)) a " _
            '      & " LEFT OUTER JOIN (SELECT     InvoiceId,  SUM(PaymentAmount) AS PaymentAmount " _
            '      & " FROM  dbo.InvoiceBasedPaymentsDetail" _
            '      & " GROUP BY InvoiceId ) Receipt on Receipt.InvoiceId = a.ReceivingId " _
            '      & " LEFT OUTER JOIN (SELECT     InvoiceId, IsNull(IsNull(SUM(NetAmount),0),0) AS VocherAmount " _
            '      & " FROM  dbo.tblVoucherDetail " _
            '      & " GROUP BY InvoiceId ) Vocher on Vocher.InvoiceId = a.ReceivingId " _
            '      & " LEFT OUTER JOIN(Select InvoiceId, coa_detail_id, IsNull(SUM(IsNull(AdjustmentAmount,0)),0) as Adj_Amount from InvoiceAdjustmentTable WHERE InvoiceType='Purchase' Group By InvoiceId, coa_detail_id) " _
            '      & " Adj on Adj.InvoiceId = a.ReceivingId " _
            '      & " where ReceivingAmount > (Isnull(PaymentAmount,0)+IsNull(Adj.Adj_Amount,0) + IsNull(Vocher.VocherAmount,0))"
            str = "Select ReceivingId, a.ReceivingNo, a.ReceivingDate, a.Qty-a.ReturnQty as Qty, a.PurchaseAmount-a.[Return Amount] as PurchaseAmount , a.Tax ,a.CurrencyId, a.[Return Amount], a.CurrencyRate,   (Isnull(ReceiptAmount,0)+IsNull(Adj.Adj_Amount,0)) As ReceivedAmount, a.ConvertedSalesAmount,  (((Isnull(a.PurchaseAmount,0)*a.CurrencyRate)-(Isnull(ReceiptAmount,0)+IsNull(Adj.Adj_Amount,0)+ISNULL([Return Amount],0)))) as [Converted Price] ,  (((Isnull(a.PurchaseAmount,0)*a.CurrencyRate)-(Isnull(ReceiptAmount,0)+IsNull(Adj.Adj_Amount,0)+ISNULL([Return Amount],0))))/a.CurrencyRate as [Price]   From ( SELECT ReceivingDetailTable.ReceivingId,ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.ReceivingDate,  SUM(ISNULL(ReceivingDetailTable.Qty, 0)) AS Qty, SUM(isnull(srdt.Qty,0))as ReturnQty, SUM((ISNULL(TaxPercent, 0) / 100) * (ISNULL(ReceivingDetailTable.Qty, 0) * ISNULL(ReceivingDetailTable.Price, 0))) AS TaxAmount,  Sum(ReceivingDetailTable.Price*ReceivingDetailTable.Qty) + ((ReceivingDetailTable.TaxPercent/100)*Sum(ReceivingDetailTable.Price*ReceivingDetailTable.Qty)) as PurchaseAmount, Sum(ReceivingDetailTable.Price*ReceivingDetailTable.Qty*ReceivingDetailTable.CurrencyRate) + ((ReceivingDetailTable.TaxPercent/100)*Sum(ReceivingDetailTable.Price*ReceivingDetailTable.Qty*ReceivingDetailTable.CurrencyRate)) as ConvertedSalesAmount , (SUM(ISNULL(ReceivingDetailTable.TaxPercent, 0))) / COUNT(ISNULL(ReceivingDetailTable.TaxPercent, 0)) AS Tax,  ISNULL(ReceivingDetailTable.CurrencyId, 1) as CurrencyId, ISNULL(ReceivingDetailTable.CurrencyRate, 1) as CurrencyRate  , Sum(SRDT.Price*SRDT.Qty) + ((SRDT.Tax_Percent/100)*Sum(SRDT.Price*SRDT.Qty)) as [Return Amount] , Sum(SRDT.Price*SRDT.Qty*SRDT.CurrencyRate) + ((SRDT.Tax_Percent/100)*Sum(SRDT.Price*SRDT.Qty*SRDT.CurrencyRate)) as [Converted Return Amount]   FROM ReceivingDetailTable INNER JOIN ReceivingMasterTable on ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId  LEFT JOIN  PurchaseReturnMasterTable SRMT ON SRMT.PurchaseOrderID=ReceivingMasterTable.ReceivingId  LEFT JOIN PurchaseReturnDetailTable SRDT on SRDT.PurchaseReturnId=SRMT.PurchaseReturnId WHERE ReceivingMasterTable.VendorId =  " & cmbCustomer.Value & "   GROUP BY ReceivingDetailTable.ReceivingId,ReceivingNo,ReceivingDate,ReceivingDetailTable.CurrencyId,ReceivingDetailTable.CurrencyRate,Isnull(ReceivingAmount,0),ReceivingDetailTable.TaxPercent ,  srdt.Tax_Percent  HAVING   (IsNull(ReceivingAmount,0) <> 0)) a   LEFT JOIN (SELECT     InvoiceId, IsNull(IsNull(SUM(ReceiptAmount),0),0) AS ReceiptAmount  FROM  dbo.InvoiceBasedReceiptsDetails   GROUP BY InvoiceId ) Receipt on Receipt.InvoiceId = a.ReceivingId   LEFT JOIN  (SELECT     InvoiceId, IsNull(IsNull(SUM(NetAmount),0),0) AS VocherAmount FROM  dbo.tblVoucherDetail   GROUP BY InvoiceId ) Vocher on Vocher.InvoiceId = a.ReceivingId  LEFT JOIN(Select InvoiceId, coa_detail_id, IsNull(SUM(IsNull(AdjustmentAmount,0)),0) as Adj_Amount from InvoiceAdjustmentTable WHERE InvoiceType='Purchase'  Group By InvoiceId, coa_detail_id)  Adj on Adj.InvoiceId = a.ReceivingId  where a.ConvertedSalesAmount > (Isnull(ReceiptAmount, 0) + IsNull(Adj.Adj_Amount, 0))  Order By  a.ReceivingId desc "
            If Me.dtpFromDate.Checked = True Then
                str += " AND a.ReceivingDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
            End If
            If Me.dtpToDate.Checked = True Then
                str += " AND a.ReceivingDate <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
            End If
            str += "Order By  a.ReceivingId desc"
            dt = GetDataTable(str)
            Me.grdItems.DataSource = dt
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

    Private Sub rbtCustomer_CheckedChanged(sender As Object, e As EventArgs) Handles rbtCustomer.CheckedChanged, rbtAll.CheckedChanged
        Try
            If rbtCustomer.Checked = True Then
                lblPartialAmount.Visible = True
                txtPartialAmount.Visible = True
            Else
                lblPartialAmount.Visible = False
                txtPartialAmount.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class