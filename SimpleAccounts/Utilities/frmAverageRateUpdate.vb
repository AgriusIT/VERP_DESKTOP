Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Collections.Generic
Public Class frmAverageRateUpdate
    Dim MyFromDate, MyToDate As DateTime


    Dim dt As DataTable
    Dim IsFormOpend As Boolean = False
    Dim IsEditMode As Boolean = False
    Dim ExistingBalance As Double = 0D
    Dim Mode As String = "Normal"
    ' Dim Spech As New SpeechSynthesizer

    Dim InvId As Integer = 0
    Dim SelectCategory As Integer
    Dim SelectBarcodes As Integer
    Dim SelectVendor As Integer
    Dim SelectSaleMan As Integer
    Dim SelectCompany As Integer
    Dim SelectTrans As Integer
    Dim CostId As Integer
    Dim FuelExpAccount As Integer
    Dim AdjustmentExpAccount As Integer
    Dim OtherExpAccount As Integer
    Dim EditCustomerListOnSale As String
    Dim StockMaster As StockMaster
    Dim StockDetail As StockDetail
    Dim VNo As String = String.Empty
    Dim ExistingVoucherFlg As Boolean = False
    Dim VoucherId As Integer = 0
    Dim Email As Email
    Dim TradePrice As Double = 0
    Dim SalesTax_Percentage As Double = 0
    Dim SchemeQty As Double = 0
    Dim Discount_Percentage As Double = 0
    Dim Freight As Double = 0
    Dim MarketReturns As Double = 0D
    Dim IsSalesOrderAnalysis As Boolean = False
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim setVoucherNo As String = String.Empty
    Dim Total_Amount As Double = 0D
    Dim setEditMode As Boolean = False
    Dim getVoucher_Id As Integer = 0
    Dim companyId As Integer = 0
    Dim Previouse_Amount As Double = 0D
    'Dim ServicesItem As String = 0D
    Dim TransitInssuranceTax As Double = 0D
    Dim WHTax As Double = 0D
    Dim PrintLog As PrintLogBE
    Dim CompanyBasePrefix As Boolean = False
    Dim flgMultipleSalesOrder As Boolean = False
    Dim flgLoadAllItems As Boolean = False
    Dim DefaultTax As Double = 0D
    Dim flgCompanyRights As Boolean = False
    Dim LoadQty As Double = 0D
    Public flgAutoInvoiceGenerate As Boolean = False
    Public DeliveryChalanId As Integer = 0I
    Dim Adjustment As Double = 0D
    Dim flgCgsVoucher As Boolean = False
    Dim flgPrintLog As Boolean = False
    Dim flgLocationWiseItems As Boolean = False
    Dim _RefDocId As Integer = 0I
    Dim _RefDocNo As String = String.Empty
    Dim StockList As List(Of StockDetail)
    Dim flgExcludeTaxPrice As Boolean = False
    ''Task:2369 Declare Boolean Variables
    Dim flgCommentCustomerFormat As Boolean = False
    Dim flgCommentArticleFormat As Boolean = False
    Dim flgCommentArticleSizeFormat As Boolean = False
    Dim flgCommentArticleColorFormat As Boolean = False
    Dim flgCommentQtyFormat As Boolean = False
    Dim flgCommentPriceFormat As Boolean = False
    Dim flgCommentRemarksFormat As Boolean = False
    'End Task:2369
    Dim CreditSales As Boolean = True 'Task:2380 Added Flag Credit Sales
    Dim dtpPODate As DateTime
    Dim strRemarks As String = String.Empty
    Dim VendorName As String = String.Empty
    Enum EnumGridDetail
        'Category
        LocationID
        ArticleCode
        Item
        Size
        Color
        Unit
        Qty
        'ServiceQty
        Price
        Total
        GroupID
        ArticleID
        PackQty
        CurrentPrice
        PackPrice
        SaleDetailID
        BatchID
        'LocationID
        TradePrice
        Tax
        TaxAmount 'Task:2374 Added Index
        SED
        TotalAmount 'Task:2374 Added Index
        SavedQty
        SampleQty
        Discount_Percentage
        Freight
        MarketReturns
        SO_ID
        BatchNo
        LoadQty
        PurchasePrice
        NetBill
        Comments
        Pack_Desc
        AccountId
        SalesTax
        SalesExcTax
        SEDTax
        DeleteButton
    End Enum
    Enum enmMaster
        SalesId
        LocationId
        SalesNo
        SalesDate
        SalesTime
        CustomerCode
        EmployeeCode
        POId
        SalesQty
        SalesAmount
        CashPaid
        Balance
        Remarks
        UserName
        TransporterId
        BiltyNo
        PreviousBalance
        InvoiceDiscount
        FuelExpense
        OtherExpense
        Adjustment
        CostCenterId
        Post
        ServiceItemSale
        DeliveryDate
        Delivered
        TransitInsurance
        DeliveryChalanId
        DcNo
        Adj_Flag
        Adj_Percentage
        RefDocId
        Detail_Title
    End Enum
    Enum enmStockDetail
        StockTransId
        LocationId
        ArticleDefId
        InQty
        OutQty
        Rate
        InAmount
        OutAmount
        Remarks
        Engine_No
        Chassis_No
    End Enum
    Private Sub frmAverageRateUpdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.dtpDateFrom.Value = Now.AddMonths(-1)
            Me.dtpDateTo.Value = Now
            Me.prbInvoices.Value = 0
            Me.prbOverall.Value = 0
            Me.btnUpdate.Enabled = True
            Me.dtpDateFrom.Focus()
            Me.chkSales.Checked = True
            Me.chkSalesReturn.Checked = False
            Me.chkStoreIssuence.Checked = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function SetComments(ByVal GridExRow As DataRow) As String
        Try
            Dim Comments As String = String.Empty
            If GridExRow IsNot Nothing Then
                If flgCommentCustomerFormat = True Then
                    Comments += VendorName.Replace("'", "''") & ","
                End If
                If flgCommentArticleFormat = True Then
                    Comments += " " & GridExRow.Item(EnumGridDetail.Item).ToString & ","
                End If
                If flgCommentArticleSizeFormat = True Then
                    Comments += " " & GridExRow.Item(EnumGridDetail.Size).ToString & ","
                End If
                If flgCommentArticleColorFormat = True Then
                    Comments += " " & GridExRow.Item(EnumGridDetail.Color).ToString & ","
                End If
                If flgCommentQtyFormat = True Then
                    Comments += " " & IIf(Val(GridExRow.Item(EnumGridDetail.PackQty).ToString) = 0, Val(GridExRow.Item(EnumGridDetail.Qty).ToString), Val(GridExRow.Item(EnumGridDetail.Qty).ToString) * Val(GridExRow.Item(EnumGridDetail.PackQty).ToString))
                End If
                If flgCommentPriceFormat = True AndAlso flgCommentQtyFormat = True Then
                    Comments += " X " & Val(GridExRow.Item(EnumGridDetail.Price).ToString)
                ElseIf flgCommentPriceFormat = True Then
                    Comments += " " & Val(GridExRow.Item(EnumGridDetail.Price).ToString) & ","
                End If
                If flgCommentRemarksFormat = True Then
                    Comments += " " & strRemarks.Replace("'", "''")
                End If
            End If

            Return Comments
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function GetVoucherNo() As String
        Dim docNo As String = String.Empty
        Dim VType As String = String.Empty
        Dim i As Integer = 0
        If i > 0 Then
            VType = "BRV"
        Else
            VType = "CRV"
        End If
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo(VType + "-" + Microsoft.VisualBasic.Right(dtpPODate.Year, 2) + "-", "tblVoucher", "voucher_no")
            Else
                Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
                Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
                If Not dr Is Nothing Then
                    If dr("config_Value") = "Monthly" Then
                        Return GetNextDocNo(VType & "-" & Format(dtpPODate, "yy") & dtpPODate.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
                    Else
                        docNo = GetNextDocNo(VType, 6, "tblVoucher", "voucher_no")
                        Return docNo
                    End If
                Else
                    docNo = GetNextDocNo(VType, 6, "tblVoucher", "voucher_no")
                    Return docNo
                End If
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try

            MyFromDate = dtpDateFrom.Value
            MyToDate = dtpDateTo.Value
            Me.btnUpdate.Enabled = False
            If Me.chkSales.Checked = True Then
                If Update_Record() = True Then
                    msg_Information("Update Record Successfully")
                Else
                    Throw New Exception("Can't Record Update.")
                End If
            ElseIf Me.chkSalesReturn.Checked = True Then
                Dim blnUpdate As Boolean = New UpdateAvgRateSalesReturn().Update_Record()
                If blnUpdate = True Then
                    msg_Information("Update Record Successfully")
                Else
                    Throw New Exception("Can't Record Update.")
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.btnUpdate.Enabled = True
        End Try
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If

            If Not getConfigValueByType("CGSVoucher").ToString = "Error" Then
                flgCgsVoucher = getConfigValueByType("CGSVoucher")
            End If

            DefaultTax = Val(getConfigValueByType("Default_Tax_Percentage").ToString)

            If Not getConfigValueByType("TransitInssuranceTax").ToString = "Error" Then
                TransitInssuranceTax = Val(getConfigValueByType("TransitInssuranceTax").ToString)
            Else
                TransitInssuranceTax = 0
            End If

            If Not getConfigValueByType("WHTax_Percentage").ToString = "Error" Then
                WHTax = Val(getConfigValueByType("WHTax_Percentage").ToString)
            Else
                WHTax = 0
            End If
            If Not getConfigValueByType("Company-Based-Prefix").ToString = "Error" Then
                CompanyBasePrefix = Convert.ToBoolean(getConfigValueByType("Company-Based-Prefix").ToString)
            End If
            If Not getConfigValueByType("MultipleSalesOrder").ToString = "Error" Then
                flgMultipleSalesOrder = Convert.ToBoolean(getConfigValueByType("MultipleSalesOrder").ToString)
            Else
                flgMultipleSalesOrder = False
            End If
            ''Task:2369 Get Comments Configurations
            If Not getConfigValueByType("CommentCustomerFormat").ToString = "Error" Then
                flgCommentCustomerFormat = Convert.ToBoolean(getConfigValueByType("CommentCustomerFormat").ToString)
            Else
                flgCommentCustomerFormat = False
            End If
            If Not getConfigValueByType("CommentArticleFormat").ToString = "Error" Then
                flgCommentArticleFormat = Convert.ToBoolean(getConfigValueByType("CommentArticleFormat").ToString)
            Else
                flgCommentArticleFormat = False
            End If
            If Not getConfigValueByType("CommentArticleSizeFormat").ToString = "Error" Then
                flgCommentArticleSizeFormat = Convert.ToBoolean(getConfigValueByType("CommentArticleSizeFormat").ToString)
            Else
                flgCommentArticleSizeFormat = False
            End If
            If Not getConfigValueByType("CommentArticleColorFormat").ToString = "Error" Then
                flgCommentArticleColorFormat = Convert.ToBoolean(getConfigValueByType("CommentArticleColorFormat").ToString)
            Else
                flgCommentArticleColorFormat = False
            End If
            If Not getConfigValueByType("CommentQtyFormat").ToString = "Error" Then
                flgCommentQtyFormat = Convert.ToBoolean(getConfigValueByType("CommentQtyFormat").ToString)
            Else
                flgCommentQtyFormat = False
            End If
            If Not getConfigValueByType("CommentPriceFormat").ToString = "Error" Then
                flgCommentPriceFormat = Convert.ToBoolean(getConfigValueByType("CommentPriceFormat").ToString)
            Else
                flgCommentPriceFormat = False
            End If
            If Not getConfigValueByType("CommentRemarksFormat").ToString = "Error" Then
                flgCommentRemarksFormat = Convert.ToBoolean(getConfigValueByType("CommentRemarksFormat").ToString)
            Else
                flgCommentRemarksFormat = False
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmDateLock_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If Me.BackgroundWorker2.IsBusy Then Exit Sub
            Me.BackgroundWorker2.RunWorkerAsync()
            Do While Me.BackgroundWorker2.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Function Update_Record() As Boolean
        Dim objCommand As New OleDbCommand
        Dim objCon As New OleDbConnection
        Dim trans As OleDbTransaction = Nothing


        Try



            Dim VendorId As Integer
            Dim InvAccountId As Integer = Val(getConfigValueByType("InvAccountId").ToString) 'GetConfigValue("InvAccountId") 'Inventory Account
            Dim CgsAccountId As Integer = Val(getConfigValueByType("CGSAccountId").ToString) 'GetConfigValue("CGSAccountId") 'Cost Of Good Sold Account
            Dim AccountId As Integer = Val(getConfigValueByType("SalesCreditAccount").ToString) 'GetConfigValue("SalesCreditAccount")
            Dim SalesTaxId As Integer = Val(getConfigValueByType("SalesTaxCreditAccount").ToString) 'GetConfigValue("SalesTaxCreditAccount")
            Dim SEDAccountId As Integer = Val(getConfigValueByType("SEDAccountId").ToString) 'Val(GetConfigValue("SEDAccountId").ToString)
            Dim InsuranceAccountId As Integer = Val(getConfigValueByType("TransitInsuranceAccountId").ToString) 'Val(GetConfigValue("TransitInsuranceAccountId").ToString)
            FuelExpAccount = Val(getConfigValueByType("FuelExpAccount").ToString)
            AdjustmentExpAccount = Val(getConfigValueByType("AdjustmentExpAccount").ToString)
            OtherExpAccount = Val(getConfigValueByType("OtherExpAccount").ToString)
            Dim IsDiscountVoucher As Boolean = Convert.ToBoolean(getConfigValueByType("DiscountVoucherOnSale").ToString) 'Convert.ToBoolean(GetConfigValue("DiscountVoucherOnSale").ToString)
            Dim SalesDiscountAccount As Integer = Val(getConfigValueByType("SalesDiscountAccount").ToString) 'Val(GetConfigValue("SalesDiscountAccount").ToString)
            Dim GLAccountArticleDepartment As Boolean
            If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
                GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
            Else
                GLAccountArticleDepartment = False
            End If
            Dim lngVoucherMasterId As Integer = 0I
            'Validtion on Configuration Account Id 
            '25-9-2013 by imran ali....

            'If flgCgsVoucher = True Then
            '    If InvAccountId <= 0 Then
            '        ShowErrorMessage("Purchase account is not map.")
            '        Me.dtpDateFrom.Focus()
            '        Return False
            '    ElseIf CgsAccountId <= 0 Then
            '        ShowErrorMessage("Cost of good sold account is not map.")
            '        Me.dtpDateFrom.Focus()
            '        Return False
            '    End If
            'End If
            'If AccountId <= 0 Then
            '    ShowErrorMessage("Sales account is not map.")
            '    Me.dtpDateFrom.Focus()
            '    Return False
            'End If

            Dim ReceiptVoucherFlg As String = Convert.ToString(getConfigValueByType("ReceiptVoucherOnSales").ToString) 'GetConfigValue("ReceiptVoucherOnSales").ToString
            Dim VoucherNo As String = GetVoucherNo()
            Dim DiscountedPrice As Double = 0
            Dim CurrentBalance As Double = CDbl(GetAccountBalance(VendorId)) - Me.ExistingBalance
            Dim ExpenseChargeToCustomer As Boolean
            'If Not GetConfigValue("ExpenseChargeToCustomer").ToString = "Error" Then
            ExpenseChargeToCustomer = Convert.ToBoolean(getConfigValueByType("ExpenseChargeToCustomer").ToString) 'Convert.ToBoolean(GetConfigValue("ExpenseChargeToCustomer").ToString)
            Dim flgExcludeTaxPrice As Boolean = Convert.ToBoolean(getConfigValueByType("ExcludeTaxPrice").ToString)


            Dim strQuery As String = "Select StockTransId, DocNo, DocDate From StockMasterTable WHERE LEFT(DocNo,2)='SI' AND (Convert(Varchar, DocDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) ORDER BY (Convert(varchar, DocDate, 102)) ASC"
            Dim dtStock As New DataTable
            dtStock = GetDataTable(strQuery)


            Dim strQuery2 As String = "Select ArticleId From ArticleDefTable WHERE ArticleId In(Select DISTINCT ArticleDefId From SalesDetailTable) ORDER BY ArticleId ASC"
            Dim dtArticle As New DataTable
            dtArticle = GetDataTable(strQuery2)

            If dtArticle Is Nothing Then Return False
            If dtArticle.Rows.Count > 0 Then

                prbOverall.Minimum = 0
                prbOverall.Maximum = dtArticle.Rows.Count
                prbOverall.Value = 0

                For Each rArticle As DataRow In dtArticle.Rows
                    '
                    Dim strQuery1 As String = "Select  DISTINCT (Convert(Varchar,ReceivingDate,102)) as ReceivingDate From ReceivingMasterTable WHERE (Convert(Varchar, ReceivingDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND ReceivingId In (Select ReceivingId From ReceivingDetailTable WHERE ArticleDefId=" & rArticle.Item(0).ToString & ")  Union Select GetDate() ORDER BY ReceivingDate ASC"
                    Dim dtPur As New DataTable
                    dtPur = GetDataTable(strQuery1)
                    If dtPur IsNot Nothing Then
                        If dtStock.Rows.Count > 0 Then
                            For i As Integer = 0 To dtPur.Rows.Count - 1
                                For j As Integer = i + 1 To dtPur.Rows.Count - 1
                                    MyFromDate = CDate(dtPur.Rows(i).Item(0))
                                    MyToDate = CDate(dtPur.Rows(j).Item(0))
                                    Dim strSQL As String = "Select Isnull(SalesId,0) as SalesId, Isnull(LocationId,0) as LocationId, SalesNo, SalesDate, SalesTime, Isnull(SalesMasterTable.CustomerCode,0) as CustomerCode, Isnull(EmployeeCode,0) as EmployeeCode, Isnull(POId,0) as POId, Isnull(SalesQty,0) as SalesQty, Isnull(SalesAmount,0) as SalesAmount, Isnull(CashPaid,0) as CashPaid, Isnull(Balance,0) as Balane, Remarks, UserName, Isnull(TransporterId,0) as TransporterId, BiltyNo, Isnull(PreviousBalance,0) as PreviousBalance, IsNull(InvoiceDiscount,0) as InvoiceDiscount, Isnull(FuelExpense,0) as FuelExpense, Isnull(OtherExpense,0) as OtherExpense, Isnull(Adjustment,0) as Adjustment, Isnull(CostCenterId,0) as CostCenterId, Isnull(Post,0) as Post, Isnull(ServiceItemSale,0) as ServiceItemSale, DeliveryDate, Isnull(Delivered,0) as Delivered, Isnull(TransitInsurance,0) as TransitIncurance, Isnull(DeliveryChalanId,0) as DeliveryChalanId, DcNo, Isnull(Adj_Flag,0) as Adj_Flag, Isnull(Adj_Percentage,0) as Adj_Percentage, IsNull(RefDocId,0) as RefDocId, vw.detail_title From SalesMasterTable INNER JOIN vwCOADetail vw On vw.coa_detail_id = SalesMasterTable.CustomerCode WHERE (Convert(Varchar, SalesDate, 102) BETWEEN Convert(DateTime, '" & MyFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & MyToDate.ToString("yyyy-M-d 23:59:59") & "', 102)) AND SalesId In (Select SalesId From SalesDetailTable WHERE ArticleDefId=" & Val(rArticle.Item(0).ToString) & ") ORDER BY (Convert(varchar,SalesDate,102)), SalesNo ASC"
                                    Dim dtInv As DataTable = GetDataTable(strSQL)
                                    dtInv.TableName = "Invoice"
                                    If dtInv IsNot Nothing Then
                                        If dtInv.Rows.Count > 0 Then
                                            Me.prbInvoices.Minimum = 0
                                            Me.prbInvoices.Maximum = dtInv.Rows.Count
                                            Me.prbInvoices.Step = dtInv.Rows.Count
                                            Me.prbInvoices.Value = 0
                                            Dim y As Integer = 0I
                                            For Each objMasterRow As DataRow In dtInv.Rows
                                                Me.Label5.Text = "Invoice No. " & objMasterRow.Item(enmMaster.SalesNo).ToString & "   Date: " & objMasterRow.Item(enmMaster.SalesDate).ToString & ""
                                                Application.DoEvents()

                                                objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")
                                                If objCon.State = ConnectionState.Open Then objCon.Close()
                                                objCon.Open()

                                                Dim cmd As New OleDbCommand
                                                cmd.Connection = objCon
                                                trans = objCon.BeginTransaction

                                                objCommand.Connection = objCon
                                                objCommand.Transaction = trans
                                                objCommand.CommandType = CommandType.Text

                                                strRemarks = objMasterRow(enmMaster.Remarks).ToString
                                                VendorName = objMasterRow(enmMaster.Detail_Title).ToString
                                                Dim transId As Integer = 0
                                                If GetFilterDataFromDataTable(dtStock, "[DocNo]='" & objMasterRow.Item(enmMaster.SalesNo).ToString & "'").ToTable("StockTrans").Rows.Count < 1 Then
                                                    transId = 0
                                                Else
                                                    transId = GetFilterDataFromDataTable(dtStock, "[DocNo]='" & objMasterRow.Item(enmMaster.SalesNo).ToString & "'").ToTable("StockTrans").Rows(0).Item("StockTransId").ToString()
                                                End If

                                                transId = transId
                                                StockMaster = New StockMaster
                                                StockMaster.StockTransId = transId
                                                StockMaster.DocNo = objMasterRow.Item(enmMaster.SalesNo).ToString.Replace("'", "''")
                                                StockMaster.DocDate = objMasterRow.Item(enmMaster.SalesDate)
                                                StockMaster.DocType = 3 'Convert.ToInt32(GetStockDocTypeId("Sales").ToString)
                                                StockMaster.Remaks = objMasterRow.Item(enmMaster.Remarks).ToString.Replace("'", "''")
                                                StockMaster.Project = Val(objMasterRow.Item(enmMaster.CostCenterId).ToString)

                                                strSQL = String.Empty
                                                strSQL = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, ArticleSizeDefTable.ArticleSizeName AS Size, ArticleColorDefTable.ArticleColorName AS Color , Recv_D.ArticleSize AS Unit, Recv_D.Sz1 AS Qty, Recv_D.Price as Rate, " _
                                                                      & " CASE WHEN recv_d.articlesize = 'Loose' THEN (Recv_D.Sz1 * Recv_D.Price)  ELSE ((Recv_D.Sz1 * Recv_D.Price) * Article.PackQty) END AS Total, " _
                                                                      & " Article.ArticleGroupId,Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty], ISNULL(Recv_D.CurrentPrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice,  Recv_D.SaleDetailId, Recv_D.BatchID, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.TaxPercent,0) as Tax, Convert(float,0) as [Tax Amount], ISNULL(Recv_D.SEDPercent,0) As SED, Convert(float,0) as [Total Amount],0 as savedqty , SampleQty as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, ISNULL(So_ID,0) as So_Id, isnull(Recv_D.BatchNo,'') as [Batch No], isnull(Recv_D.LoadQty,0) as LoadQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0 as NetBill, Recv_D.Comments, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(ArticleGroupDefTable.SubSubId,0) as SalesAccountId,  ((Isnull(TaxPercent,0)/100) * (Isnull(Qty,0) * Isnull(Price,0))) SalesTax, (((Isnull(Qty,0) * Isnull(Price,0))/(Isnull(TaxPercent,0)+100))*Isnull(TaxPercent,0)) SalesExcTax, ((Isnull(SEDPercent,0)/100) * (Isnull(Qty,0) * Isnull(Price,0))) SEDTax    FROM SalesDetailTable Recv_D INNER JOIN " _
                                                                      & " ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId inner JOIN " _
                                                                      & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id inner join " _
                                                                      & " articledeftable on articledeftable.articleid = recv_d.articleDefId INNER JOIN " _
                                                                      & " ArticleSizeDefTable ON ArticleSizeDefTable.ArticleSizeId = ArticleDefTable.SizeRangeId INNER JOIN " _
                                                                      & " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId LEFT OUTER JOIN ArticleGroupDefTable on ArticleGroupDefTable.ArticleGroupId = Article.ArticleGroupId " _
                                                                      & " Where Recv_D.SalesID =" & Val(objMasterRow.Item(enmMaster.SalesId).ToString) & " ORDER BY Recv_D.SaleDetailId Asc"
                                                Dim dtData As DataTable = GetDataTable(strSQL, trans)
                                                dtData.TableName = "Detail"


                                                strSQL = String.Empty
                                                strSQL = "Select ArticleDefId, Rate From StockDetailTable WHERE StockTransId In (Select StockTransId From StockMasterTable  WHERE DocNo='" & objMasterRow.Item(enmMaster.SalesNo).ToString & "') Group by ArticleDefId, Rate"
                                                Dim dtStockDetail As New DataTable
                                                dtStockDetail = GetDataTable(strSQL, trans)

                                                If dtData IsNot Nothing Then

                                                    Dim dtVoucher As DataTable = GetDataTable("SELECT Isnull(Voucher_ID,0) as Voucher_Id FROM tblVoucher where source='frmSales' and voucher_code='" & objMasterRow.Item(enmMaster.SalesNo).ToString & "'", trans)
                                                    If dtVoucher IsNot Nothing Then
                                                        If dtVoucher.Rows.Count > 0 Then
                                                            lngVoucherMasterId = Val(dtVoucher.Rows(0).Item(0).ToString) 'GetVoucherId("frmSales", objMasterRow(enmMaster.SalesNo).ToString, trans)
                                                        Else
                                                            lngVoucherMasterId = 0I
                                                        End If
                                                    End If

                                                    '***********************
                                                    'Deleting Detail
                                                    '***********************
                                                    objCommand.CommandText = ""
                                                    objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
                                                    objCommand.ExecuteNonQuery()

                                                    StockList = New List(Of StockDetail)
                                                    For Each grd As DataRow In dtData.Rows

                                                        Dim chkPost As Boolean = True
                                                        'Dim i As Integer
                                                        gobjLocationId = MyCompanyId
                                                        dtpPODate = objMasterRow.Item(enmMaster.SalesDate)
                                                        VendorId = objMasterRow.Item(enmMaster.CustomerCode)

                                                        DiscountedPrice = Val(grd.Item(EnumGridDetail.CurrentPrice).ToString) - Val(grd.Item(EnumGridDetail.Price))
                                                        If GLAccountArticleDepartment = True Then
                                                            'Before against task:2390
                                                            'AccountId = Val(grd.Item("SalesAccountId").ToString)
                                                            'Task:2390 Change Inventory Account
                                                            InvAccountId = Val(grd.Item("SalesAccountId").ToString)
                                                            'End Task:2390
                                                        End If

                                                        Dim CostPrice As Double = 0D
                                                        Dim CrrStock As Double = 0D
                                                        If flgCgsVoucher = True Then
                                                            Dim dr() As DataRow = Nothing



                                                            If Convert.ToInt32(Val(rArticle.Item(0).ToString)) = Convert.ToInt32(Val(grd.Item(EnumGridDetail.ArticleID).ToString)) Then
                                                                objCommand.CommandText = ""
                                                                objCommand.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, 0 as PurchasePrice, ABS(Convert(float, SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0)))) AS qty, ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))) as Amount  " _
                                                                                                & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                                                                & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId=" & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & " AND StockTransId in (Select StockTransId From StockMasterTable WHERE DocNo <> '" & objMasterRow.Item(enmMaster.SalesNo).ToString & "'  AND (Convert(Varchar, DocDate, 102) <= Convert(DateTime, '" & MyFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)))" _
                                                                                                & " GROUP BY dbo.StockDetailTable.ArticleDefId "
                                                                Dim dtCrrStock As New DataTable
                                                                Dim daCrrStock As New OleDbDataAdapter(objCommand)
                                                                daCrrStock.Fill(dtCrrStock)
                                                                If dtCrrStock IsNot Nothing Then
                                                                    If dtCrrStock.Rows.Count > 0 Then
                                                                        If Val(grd.Item("rate").ToString) <> 0 Then
                                                                            CrrStock = dtCrrStock.Rows(0).Item(2)
                                                                            CostPrice = IIf(dtCrrStock.Rows(0).Item(3) + CrrStock = 0, 0, dtCrrStock.Rows(0).Item(3) / CrrStock)
                                                                        Else
                                                                            CostPrice = Val(grd.Item("PurchasePrice").ToString)
                                                                        End If
                                                                    Else
                                                                        CostPrice = Val(grd.Item("PurchasePrice").ToString)
                                                                    End If
                                                                End If
                                                            Else
                                                                CostPrice = 0
                                                                If dtStockDetail IsNot Nothing Then
                                                                    dr = dtStockDetail.Select("ArticleDefId=" & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & "")
                                                                End If
                                                                If dr IsNot Nothing Then
                                                                    If dr.Length = 1 Then
                                                                        CostPrice = Val(dr(0).Item(1).ToString)
                                                                    Else
                                                                        Dim str As String = String.Empty
                                                                    End If
                                                                End If

                                                            End If

                                                        End If

                                                        If (Val(grd.Item("Qty").ToString.ToString) > 0 Or Val(grd.Item("Sample Qty").ToString.ToString) > 0) Then
                                                            StockDetail = New StockDetail
                                                            StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                                                            StockDetail.LocationId = grd.Item("LocationId").ToString
                                                            StockDetail.ArticleDefId = grd.Item("ArticleDefId").ToString
                                                            StockDetail.InQty = 0
                                                            If IsSalesOrderAnalysis = True Then
                                                                StockDetail.OutQty = IIf(grd.Item("Unit").ToString = "Loose", Val(grd.Item("Qty").ToString) + Val(grd.Item("Sample Qty").ToString), ((Val(grd.Item("Qty").ToString) + Val(grd.Item("Sample Qty").ToString)) * Val(grd.Item("Pack Qty").ToString)))
                                                            Else
                                                                StockDetail.OutQty = IIf(grd.Item("Unit").ToString = "Loose", Val(grd.Item("Qty").ToString) + Val(grd.Item("Sample Qty").ToString), ((Val(grd.Item("Qty").ToString) + Val(grd.Item("Sample Qty").ToString)) * Val(grd.Item("Pack Qty").ToString)))
                                                            End If
                                                            StockDetail.Rate = IIf(CostPrice = 0, Val(grd.Item(EnumGridDetail.PurchasePrice).ToString), CostPrice)
                                                            StockDetail.InAmount = 0
                                                            StockDetail.OutAmount = IIf(grd.Item("Unit").ToString = "Loose", ((Val(grd.Item("Qty").ToString) + Val(grd.Item("Sample Qty").ToString)) * IIf(CostPrice = 0, Val(grd.Item(EnumGridDetail.PurchasePrice).ToString), CostPrice)), (((Val(grd.Item("Qty").ToString) + Val(grd.Item("Sample Qty").ToString)) * Val(grd.Item("Pack Qty").ToString)) * IIf(CostPrice = 0, Val(grd.Item(EnumGridDetail.PurchasePrice).ToString), CostPrice)))
                                                            StockDetail.Remarks = String.Empty
                                                            StockList.Add(StockDetail)
                                                        End If


                                                        objCommand.CommandText = ""
                                                        objCommand.CommandText = "Update SalesDetailTable Set CostPrice=" & Val(StockDetail.Rate) & " WHERE ArticleDefId=" & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & " AND SalesId=" & Val(objMasterRow.Item(enmMaster.SalesId).ToString) & ""
                                                        objCommand.ExecuteNonQuery()

                                                        'objCommand.CommandText = "Insert into SalesDetailTable (SalesId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice,BatchNo, BatchID, LocationId, TaxPercent, SampleQty, SEDPercent,TradePrice, Discount_Percentage, Freight, MarketReturns,SO_ID,LoadQty, PurchasePrice, PackPrice, Comments,Pack_Desc) values( " _
                                                        '                     & " " & Val(objMasterRow(enmMaster.SalesId).ToString) & " ," & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ",'" & (grd.Item(EnumGridDetail.Unit).ToString) & "'," & Val(grd.Item(EnumGridDetail.Qty).ToString) & ", " _
                                                        '                     & " " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString))) & ", " _
                                                        '                     & Val(grd.Item(EnumGridDetail.Price).ToString) & ", " & Val(grd.Item(EnumGridDetail.PackQty).ToString) & " , " & Val(grd.Item(EnumGridDetail.CurrentPrice).ToString) & ",'" & grd.Item(EnumGridDetail.BatchNo).ToString.Replace("'", "''") & "', " _
                                                        '                     & grd.Item(EnumGridDetail.BatchID).ToString & " , " & grd.Item(EnumGridDetail.LocationID).ToString & ", " & IIf(grd.Item(EnumGridDetail.Tax).ToString = "", 0, grd.Item(EnumGridDetail.Tax).ToString) & ", " & Val(grd.Item(EnumGridDetail.SampleQty).ToString()) & ", " & Val(grd.Item(EnumGridDetail.SED).ToString) & ", " & Val(grd.Item(EnumGridDetail.TradePrice).ToString) & ", " & Val(grd.Item(EnumGridDetail.Discount_Percentage).ToString) & ", " & Val(grd.Item(EnumGridDetail.Freight).ToString) & "," & Val(grd.Item(EnumGridDetail.MarketReturns).ToString) & ", " & Val(grd.Item(EnumGridDetail.SO_ID).ToString) & ", " & Val(grd.Item(EnumGridDetail.LoadQty).ToString) & "," & Val(grd.Item("PurchasePrice").ToString) & ", " & Val(grd.Item(EnumGridDetail.PackPrice).ToString) & ", '" & grd.Item(EnumGridDetail.Comments).ToString.Replace("'", "''") & "', '" & grd.Item(EnumGridDetail.Pack_Desc).ToString.Replace("'", "''") & "') "
                                                        'objCommand.ExecuteNonQuery()


                                                        'Val(grd.Rows(i).Cells(5).ToString)
                                                        'Update SO and SO Detail
                                                        'If Val(grd.Item("Qty").ToString) <> 0 Or Val(grd.Item("Sample Qty").ToString) <> 0 Then
                                                        '    If flgMultipleSalesOrder = False Then
                                                        '        If Not Me.cmbPo.SelectedIndex = -1 Then
                                                        '            objCommand.CommandText = "UPDATE SalesOrderDetailTable " _
                                                        '                                                    & " SET              DeliveredQty = isnull(DeliveredQty,0) + " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString))) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(grd.Item(EnumGridDetail.SampleQty).ToString) & ") " _
                                                        '                                                   & " WHERE     (SalesOrderId = " & Me.cmbPo.SelectedValue & ") AND (ArticleDefId = " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                        '            objCommand.ExecuteNonQuery()

                                                        '            'Else
                                                        '        End If
                                                        '    Else
                                                        '        If Val(grd.Item(EnumGridDetail.SO_ID).ToString) > 0 Then
                                                        '            objCommand.CommandText = "UPDATE SalesOrderDetailTable " _
                                                        '                                                   & " SET              DeliveredQty = isnull(DeliveredQty,0) + " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString))) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(grd.Item(EnumGridDetail.SampleQty).ToString) & ") " _
                                                        '                                                  & " WHERE     (SalesOrderId = " & Val(grd.Item(EnumGridDetail.SO_ID).ToString) & ") AND (ArticleDefId = " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                        '            objCommand.ExecuteNonQuery()
                                                        '        End If
                                                        '    End If
                                                        '    If DeliveryChalanId > 0 Then
                                                        '        objCommand.CommandText = "UPDATE DeliveryChalanDetailTable " _
                                                        '                                                & " SET              DeliveredQty = isnull(DeliveredQty,0) + " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString))) & ", DeliveredSchemeQty=abs(ISNULL(DeliveredSchemeQty,0) + " & Val(grd.Item(EnumGridDetail.SampleQty).ToString) & ") " _
                                                        '                                               & " WHERE     (DeliveryId = " & DeliveryChalanId & ") AND (ArticleDefId = " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                        '        objCommand.ExecuteNonQuery()
                                                        '        'Else
                                                        '    End If
                                                        'End If

                                                        'If Not ServiceItem = "True" Then
                                                        If Val(grd.Item("Qty").ToString) <> 0 Or Val(grd.Item("Sample Qty").ToString) <> 0 Then
                                                            If IsDiscountVoucher = False Then
                                                                If IsSalesOrderAnalysis = False Then
                                                                    '***********************
                                                                    'Inserting Debit Amount
                                                                    '***********************
                                                                    objCommand.CommandText = ""
                                                                    'Task:2391 Added Column ArticleDefId
                                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, Direction, sp_refrence,ArticleDefId) " _
                                                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.Price).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.Price).ToString)) & ", 0, '" & SetComments(grd).Replace("" & VendorName & ",", "").Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & "," & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & " )"
                                                                    'End Task:2391
                                                                    objCommand.ExecuteNonQuery()

                                                                    '***********************
                                                                    'Inserting Credit Amount
                                                                    '***********************


                                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                                                                    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", 0,  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.Price).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.Price).ToString)) & ", '" & grd.Item(EnumGridDetail.Item).ToString & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & ")' )"
                                                                    If flgExcludeTaxPrice = False Then
                                                                        objCommand.CommandText = ""
                                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                                                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", 0,  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.Price).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.Price).ToString)) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                        'End Task:2391
                                                                        objCommand.ExecuteNonQuery()
                                                                        'End Task:2369
                                                                    Else

                                                                        'Task:2391 Added Column ArticleDefId
                                                                        objCommand.CommandText = ""
                                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                                                                                                                 & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString)) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                        'End Task:2391
                                                                        objCommand.ExecuteNonQuery()
                                                                    End If

                                                                Else
                                                                    objCommand.CommandText = ""

                                                                    'Task:2391 Added Column ArticleDefId
                                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                                                                                                                                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Val(grd.Item(EnumGridDetail.NetBill).ToString) - IIf(grd.Item(EnumGridDetail.Tax).ToString = 0, 0, (Val(grd.Item(EnumGridDetail.Tax).ToString) / 100) * IIf(grd.Item(EnumGridDetail.Unit).Text = "Loose", ((grd.Item(EnumGridDetail.Qty).ToString + grd.Item(EnumGridDetail.SampleQty).ToString) * grd.Item(EnumGridDetail.CurrentPrice).ToString), (((grd.Item(EnumGridDetail.Qty).ToString * grd.Item(EnumGridDetail.PackQty).ToString) + grd.Item(EnumGridDetail.SampleQty).ToString) * grd.Item(EnumGridDetail.CurrentPrice).ToString))) & ", 0, '" & SetComments(grd).Replace("" & VendorName & ",", "").Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                    'End Task:2391
                                                                    'objCommand.Transaction = trans
                                                                    objCommand.ExecuteNonQuery()

                                                                    '***********************
                                                                    'Inserting Credit Amount
                                                                    '***********************
                                                                    'objCommand = New OleDbCommand
                                                                    ' objCommand.Connection = Con


                                                                    '******************* Change For Cost Center ******************
                                                                    ' By Imran Ali
                                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                                                                    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & AccountId & ", " & 0 & ",  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.Price).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.Price).ToString)) & ", '" & grd.Item(EnumGridDetail.Item).ToString & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & ")' )"
                                                                    If flgExcludeTaxPrice = False Then
                                                                        objCommand.CommandText = ""
                                                                        'Task:2391 Added Column ArticleDefId
                                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                                                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & Val(grd.Item(EnumGridDetail.NetBill).ToString) - IIf(grd.Item(EnumGridDetail.Tax).ToString = 0, 0, (Val(grd.Item(EnumGridDetail.Tax).ToString) / 100) * IIf(grd.Item(EnumGridDetail.Unit).Text = "Loose", ((grd.Item(EnumGridDetail.Qty).ToString + grd.Item(EnumGridDetail.SampleQty).ToString) * grd.Item(EnumGridDetail.CurrentPrice).ToString), (((grd.Item(EnumGridDetail.Qty).ToString * grd.Item(EnumGridDetail.PackQty).ToString) + grd.Item(EnumGridDetail.SampleQty).ToString) * grd.Item(EnumGridDetail.CurrentPrice).ToString))) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                        'End Task:2391
                                                                        objCommand.ExecuteNonQuery()
                                                                        'End Task:2369

                                                                    Else

                                                                        'Task:2391 Added Column ArticleDefId
                                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                                                                                                                                 & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString)) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                        'End Task:2391
                                                                        objCommand.ExecuteNonQuery()
                                                                        'End Task:2369



                                                                    End If



                                                                    '''''''''''''''''''''' Includ Discount Voucher ''''''''''''''''''''''''''''''''''''''''''

                                                                    If DiscountedPrice > 0 Then
                                                                        objCommand.CommandText = ""

                                                                        'Task:2391 Added Column ArticleDefId
                                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                                                                                                                                 & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Val(SalesDiscountAccount) & ", " & IIf(grd.Item(EnumGridDetail.Unit).Text = "Loose", (((Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.CurrentPrice).ToString)) * Val(grd.Item(EnumGridDetail.Discount_Percentage).ToString)) / 100), ((((Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.CurrentPrice).ToString)) * Val(grd.Item(EnumGridDetail.Discount_Percentage).ToString)) / 100)) & ", 0, '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "'," & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                        'End Task:2391
                                                                        'End Task:2369
                                                                        objCommand.ExecuteNonQuery()
                                                                    End If
                                                                End If

                                                            Else

                                                                objCommand.CommandText = ""

                                                                'Task:2391 Added Column ArticleDefId
                                                                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                                                                          & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.Price).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.Price).ToString)) & ",0,'" & SetComments(grd).Replace("" & VendorName & ",", "").Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                'End Task:2391
                                                                'End Task:2369
                                                                objCommand.ExecuteNonQuery()

                                                                If DiscountedPrice > 0 Then


                                                                    objCommand.CommandText = ""
                                                                    'Before against task:2391
                                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction,sp_refrence) " _
                                                                    '                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Val(SalesDiscountAccount) & ", " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * DiscountedPrice, (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * DiscountedPrice) & ", 0, '" & grd.Item(EnumGridDetail.Item).ToString & "  " & grd.Item(EnumGridDetail.Size).ToString & " " & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & " X " & Math.Round((Val(grd.Item(EnumGridDetail.Price).ToString)), 2) & ")', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "')"
                                                                    'Task:2391 Added Column ArticleDefId
                                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction,sp_refrence,ArticleDefId) " _
                                                                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Val(SalesDiscountAccount) & ", " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * DiscountedPrice, (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * DiscountedPrice) & ", 0, '" & grd.Item(EnumGridDetail.Item).ToString & "  " & grd.Item(EnumGridDetail.Size).ToString & " " & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & " X " & Math.Round((Val(grd.Item(EnumGridDetail.Price).ToString)), 2) & ")', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                    'End Task:2391
                                                                    objCommand.ExecuteNonQuery()

                                                                    objCommand.CommandText = ""

                                                                    'Task:2391 Added Column ArticleDefId
                                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                                                                               & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", 0," & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * DiscountedPrice, (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * DiscountedPrice) & ",'Ref Discount On Sales: " & grd.Item(EnumGridDetail.Item).ToString & "  " & grd.Item(EnumGridDetail.Size).ToString & " " & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & " X " & (DiscountedPrice) & ")', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                    'End Task:2391
                                                                    objCommand.ExecuteNonQuery()

                                                                    If flgExcludeTaxPrice = False Then
                                                                        objCommand.CommandText = ""

                                                                        'Task:2391 added column articledefid
                                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence, ArticleDefId) " _
                                                                                                                          & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.CurrentPrice).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.CurrentPrice).ToString)) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                        'End Task:2391
                                                                        objCommand.ExecuteNonQuery()
                                                                    Else

                                                                        'Task:2391 Added column articledefid
                                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                                                                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString)) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                        'End Task:2391
                                                                        objCommand.ExecuteNonQuery()


                                                                    End If
                                                                    'objCommand.ExecuteNonQuery()

                                                                Else
                                                                    If flgExcludeTaxPrice = False Then
                                                                        objCommand.CommandText = ""

                                                                        'Task:2391 Added Column ArticleDefId
                                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                                                                                    & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.Price).ToString), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * Val(grd.Item(EnumGridDetail.Price).ToString)) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                        'End Task:2391
                                                                        objCommand.ExecuteNonQuery()
                                                                    Else
                                                                        objCommand.CommandText = ""

                                                                        'Task:2391 Added column ArticleDefId
                                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                                                                                                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & 0 & ",  " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))), (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * (Val(grd.Item(EnumGridDetail.Price).ToString) - (Val(grd.Item(EnumGridDetail.Price).ToString) * Val(grd.Item(EnumGridDetail.Tax).ToString)) / (Val(grd.Item(EnumGridDetail.Tax).ToString) + 100))) & ", '" & SetComments(grd).Replace("'", "''") & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                        'End Task:2391
                                                                        objCommand.ExecuteNonQuery()
                                                                        'End Task:2369


                                                                    End If
                                                                    'objCommand.ExecuteNonQuery()
                                                                End If
                                                            End If

                                                            ''''''''''''''''''' Preparing Cost Of Sale Voucher ''''''''''''''''''''''''''''''
                                                            ''''''''''''''''''' Preparing Cost Of Sale Voucher ''''''''''''''''''''''''''''''
                                                            If flgCgsVoucher = True Then
                                                                objCommand.CommandText = ""
                                                                'Before against Task:2391                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence) " _
                                                                '                                                                          & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & CgsAccountId & ", " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * CostPrice, (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * CostPrice) & ", 0, '" & grd.Item(EnumGridDetail.Item).ToString & "  " & grd.Item(EnumGridDetail.Size).ToString & " " & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & " X " & CostPrice & ")', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "')"
                                                                'Task:2391 Added column ArticleDefId
                                                                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                                                                                                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & CgsAccountId & ", " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * CostPrice, (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * CostPrice) & ", 0, '" & grd.Item(EnumGridDetail.Item).ToString & "  " & grd.Item(EnumGridDetail.Size).ToString & " " & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & " X " & CostPrice & ")', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                'End Task:2391
                                                                objCommand.ExecuteNonQuery()

                                                                objCommand.CommandText = ""
                                                                'Before against task:2391
                                                                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence) " _
                                                                '                                                                          & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & InvAccountId & ", 0, " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * CostPrice, (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * CostPrice) & ", '" & grd.Item(EnumGridDetail.Item).ToString & "  " & grd.Item(EnumGridDetail.Size).ToString & " " & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & " X " & CostPrice & ")', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "')"\
                                                                'Task:2391 Added column ArticleDefId
                                                                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                                                                                                                          & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & InvAccountId & ", 0, " & IIf(grd.Item(EnumGridDetail.Unit).ToString = "Loose", Val(grd.Item(EnumGridDetail.Qty).ToString) * CostPrice, (Val(grd.Item(EnumGridDetail.Qty).ToString) * Val(grd.Item(EnumGridDetail.PackQty).ToString)) * CostPrice) & ", '" & grd.Item(EnumGridDetail.Item).ToString & "  " & grd.Item(EnumGridDetail.Size).ToString & " " & "(" & Val(grd.Item(EnumGridDetail.Qty).ToString) & " X " & CostPrice & ")', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", " & grd.Item(EnumGridDetail.ArticleID).ToString & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "', " & Val(grd.Item(EnumGridDetail.ArticleID).ToString) & ")"
                                                                'End Task:2391
                                                                objCommand.ExecuteNonQuery()
                                                            End If
                                                            '''''''''''''''''''''''''''''' Code By Imran Ali 03/06/2013 '''''''''''''''''''''''''''''''''''''''
                                                            'End If
                                                        End If

                                                    Next
                                                End If
                                                '***********************
                                                '01-Feb-2011    Added for tax calculation
                                                '***********************
                                                Dim objIncTax As Object = dtData.Compute("SUM(SalesTax)", "")
                                                Dim objExcTax As Object = dtData.Compute("SUM(SalesExcTax)", "")
                                                Dim objSEDTax As Object = dtData.Compute("SUM(SEDTax)", "")

                                                If objIncTax > 0 Then
                                                    If flgExcludeTaxPrice = False Then
                                                        objCommand.CommandText = ""
                                                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                                                        '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & Val(Me.txtTaxTotal.Text) & ", 0, 'Tax Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"
                                                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                                        '                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Val(objIncTax) & ", 0, 'Tax Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"


                                                        ''objCommand.Transaction = trans
                                                        'objCommand.ExecuteNonQuery()
                                                        'End If
                                                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                                                        '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & SalesTaxId & ", " & 0 & ",  " & Val(Me.txtTaxTotal.Text) & ", 'Tax Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                                             & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & SalesTaxId & ", " & 0 & ",  " & Val(objIncTax) & ", 'Tax Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

                                                        ' objCommand.Transaction = trans
                                                        objCommand.ExecuteNonQuery()

                                                    Else

                                                        'objCommand.CommandText = ""
                                                        ''objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                                                        ''                       & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & Val(Me.txtTaxTotal.Text) & ", 0, 'Tax Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"
                                                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                                        '                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Val(objExcTax) & ", 0, 'Tax Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"


                                                        ''objCommand.Transaction = trans
                                                        'objCommand.ExecuteNonQuery()
                                                        'End If
                                                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                                                        '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & SalesTaxId & ", " & 0 & ",  " & Val(Me.txtTaxTotal.Text) & ", 'Tax Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                                             & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & SalesTaxId & ", " & 0 & ",  " & Val(objExcTax) & ", 'Tax Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

                                                        ' objCommand.Transaction = trans
                                                        objCommand.ExecuteNonQuery()
                                                    End If
                                                End If


                                                '
                                                'SED Tax Apply 01-07-2012
                                                '


                                                If objSEDTax > 0 Then


                                                    objCommand.CommandText = ""
                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Val(objSEDTax) & ", 0, 'W.H Tax Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

                                                    'objCommand.Transaction = trans
                                                    objCommand.ExecuteNonQuery()


                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                                                    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Val(SEDAccountId) & ", " & 0 & ",  " & Val(objSEDTax) & ", 'W.H Tax Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
                                                    ' objCommand.Transaction = trans
                                                    objCommand.ExecuteNonQuery()

                                                End If


                                                If Val(objMasterRow(enmMaster.TransitInsurance).ToString) > 0 Then

                                                    objCommand.CommandText = ""
                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId,sp_refrence) " _
                                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Val(objMasterRow(enmMaster.TransitInsurance).ToString) & ", 0, 'Transit Insurace Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

                                                    'objCommand.Transaction = trans
                                                    objCommand.ExecuteNonQuery()


                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                                                    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Val(InsuranceAccountId) & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.TransitInsurance).ToString) & ", 'Transit Insurace Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
                                                    ' objCommand.Transaction = trans
                                                    objCommand.ExecuteNonQuery()


                                                End If
                                                '***********************

                                                '***********************
                                                '06-Dec-2011    Added for Fuel, Other Expense, Adjustment
                                                '***********************


                                                ''Fuel
                                                If Val(objMasterRow(enmMaster.FuelExpense).ToString) > 0 Then

                                                    objCommand.CommandText = ""
                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                                                    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.FuelExpAccount & ", " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 0, 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.FuelExpAccount & ", " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 0, 'Fuel Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
                                                    'objCommand.Transaction = trans
                                                    objCommand.ExecuteNonQuery()
                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                                                    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
                                                    ' objCommand.Transaction = trans
                                                    objCommand.ExecuteNonQuery()

                                                End If


                                                '---------------------------------- Fuel Expense Credit  
                                                If ExpenseChargeToCustomer = True Then
                                                    'If Val(objMasterRow(enmMaster.FuelExpense).ToString) < 0 Then
                                                    objCommand.CommandText = ""
                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                                                    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.FuelExpAccount & ", " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 0, 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Math.Abs(Val(objMasterRow(enmMaster.FuelExpense).ToString)) & ", 0, 'Fuel Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
                                                    'objCommand.Transaction = trans
                                                    objCommand.ExecuteNonQuery()
                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                                                    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.FuelExpense).ToString) & ", 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.FuelExpAccount & ", " & 0 & ",  " & Math.Abs(Val(objMasterRow(enmMaster.FuelExpense).ToString)) & ", 'Fuel Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
                                                    ' objCommand.Transaction = trans
                                                    objCommand.ExecuteNonQuery()

                                                    'End If
                                                End If

                                                ''end Fuel


                                                ''Other Exp
                                                If Val(objMasterRow(enmMaster.OtherExpense).ToString) > 0 Then

                                                    objCommand.CommandText = ""
                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                                                    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.OtherExpAccount & ", " & Val(objMasterRow(enmMaster.OtherExpense).ToString) & ", 0, 'Expense Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.OtherExpAccount & ", " & Val(objMasterRow(enmMaster.OtherExpense).ToString) & ", 0, 'Expense Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"


                                                    'objCommand.Transaction = trans
                                                    objCommand.ExecuteNonQuery()
                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                                                    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.OtherExpense).ToString) & ", 'Expense Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.OtherExpense).ToString) & ", 'Expense Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
                                                    ' objCommand.Transaction = trans
                                                    objCommand.ExecuteNonQuery()

                                                End If
                                                '--------------------------  Credit Other Expense 
                                                If ExpenseChargeToCustomer = True Then
                                                    If Val(objMasterRow(enmMaster.OtherExpense).ToString) < 0 Then

                                                        objCommand.CommandText = ""
                                                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                                                        '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.OtherExpAccount & ", " & Val(objMasterRow(enmMaster.OtherExpense).ToString) & ", 0, 'Expense Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                                                                               & " VALUES(" & lngVoucherMasterId & "," & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Math.Abs(Val(objMasterRow(enmMaster.OtherExpense).ToString)) & ", 0, 'Expense Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"


                                                        'objCommand.Transaction = trans
                                                        objCommand.ExecuteNonQuery()
                                                        'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                                                        '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.OtherExpense).ToString) & ", 'Expense Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                                             & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.OtherExpAccount & ", " & 0 & ",  " & Math.Abs(Val(objMasterRow(enmMaster.OtherExpense).ToString)) & ", 'Expense Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
                                                        ' objCommand.Transaction = trans
                                                        objCommand.ExecuteNonQuery()

                                                    End If
                                                End If
                                                ''end Other Exp


                                                ''Adjustment
                                                If Val(objMasterRow(enmMaster.Adjustment).ToString) > 0 Then

                                                    objCommand.CommandText = ""
                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                                                    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.AdjustmentExpAccount & ", " & Val(objMasterRow(enmMaster.Adjustment).ToString) & ", 0, 'Adjustment Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.AdjustmentExpAccount & ", " & IIf(objMasterRow(enmMaster.Adj_Flag).ToString = True, Val(objMasterRow(enmMaster.Adjustment).ToString), Adjustment) & ", 0, 'Adjustment Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

                                                    'objCommand.Transaction = trans
                                                    objCommand.ExecuteNonQuery()
                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                                                    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.Adjustment).ToString) & ", 'Adjustment Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & 0 & ",  " & IIf(objMasterRow(enmMaster.Adj_Flag).ToString = True, Val(objMasterRow(enmMaster.Adjustment).ToString), Adjustment) & ", 'Adjustment Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
                                                    ' objCommand.Transaction = trans
                                                    objCommand.ExecuteNonQuery()

                                                ElseIf Val(objMasterRow(enmMaster.Adjustment).ToString) < 0 Then
                                                    objCommand.CommandText = ""
                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments) " _
                                                    '                       & " VALUES(" & lngVoucherMasterId & ", 1, " & Me.AdjustmentExpAccount & ", " & Val(objMasterRow(enmMaster.Adjustment).ToString) & ", 0, 'Adjustment Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                                                                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.AdjustmentExpAccount & ", 0, " & Math.Abs(IIf(objMasterRow(enmMaster.Adj_Flag).ToString = True, Val(objMasterRow(enmMaster.Adjustment).ToString), Adjustment)) & ", 'Adjustment Ref By " & VendorName & ": " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"

                                                    'objCommand.Transaction = trans
                                                    objCommand.ExecuteNonQuery()
                                                    'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments) " _
                                                    '                     & " VALUES(" & lngVoucherMasterId & ", 1, " & VendorId & ", " & 0 & ",  " & Val(objMasterRow(enmMaster.Adjustment).ToString) & ", 'Adjustment Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "' )"

                                                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & VendorId & ", " & Math.Abs(IIf(objMasterRow(enmMaster.Adj_Flag).ToString = True, Val(objMasterRow(enmMaster.Adjustment).ToString), Adjustment)) & ", 0, 'Adjustment Ref: " & objMasterRow(enmMaster.SalesNo).ToString & "', " & Val(objMasterRow(enmMaster.CostCenterId).ToString) & ", '" & objMasterRow(enmMaster.Remarks).ToString.Replace("'", "''") & "' )"
                                                    ' objCommand.Transaction = trans
                                                    objCommand.ExecuteNonQuery()

                                                End If

                                                StockMaster.StockDetailList = StockList
                                                'Call New StockDAL().UpdateByTrans(StockMaster, trans)
                                                Call New StockDAL().Update(StockMaster, trans)
                                                trans.Commit()
                                                Update_Record = True
                                                SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Sales, objMasterRow(enmMaster.SalesNo).ToString.Trim, True)
                                                SaveActivityLog("Accounts", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, objMasterRow(enmMaster.SalesNo).ToString.Trim, True)
                                                prbInvoices.PerformStep()
                                            Next
                                        End If
                                    End If
                                Next
                            Next
                        End If
                    End If
                    prbOverall.Value += 1
                Next
            End If
        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Private Sub chkSales_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSales.CheckedChanged
        Try
            If Me.chkSales.Checked = True Then
                Me.chkSalesReturn.Checked = False
                Me.chkStoreIssuence.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkSalesReturn_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSalesReturn.CheckedChanged
        Try
            If Me.chkSalesReturn.Checked = True Then
                Me.chkSales.Checked = False
                Me.chkStoreIssuence.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkStoreIssuence_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkStoreIssuence.CheckedChanged
        Try
            If Me.chkStoreIssuence.Checked = True Then
                Me.chkSales.Checked = False
                Me.chkSalesReturn.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class



Public Class UpdateAvgRateSalesReturn

    Dim dt As DataTable
    Dim Mode As String = "Normal"
    Dim blnEditMode As Boolean = False
    Dim IsFormOpen As Boolean = False
    Dim StockMaster As StockMaster
    Dim StockDetail As StockDetail
    Dim Email As Email
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim setVoucherNo As String = String.Empty
    Dim Total_Amount As Double = 0D
    Dim getVoucher_Id As Integer = 0
    Dim setEditMode As Boolean = False
    Dim Previouse_Amount As Double = 0D
    Dim TaxAmount As Double = 0
    Dim PrintLog As PrintLogBE
    Dim MarketReturnAccountId As Integer = 0
    Dim flgMarketReturnVoucher As Boolean = False
    Dim flgCompanyRights As Boolean = False
    Dim flgLoadAllItems As Boolean = False
    Dim flgLocationWiseItems As Boolean = False
    Dim StockList As List(Of StockDetail)
    Dim flgOnetimeSalesReturn As Boolean = False
    ''Task:2369 Declare Boolean Variables
    Dim flgCommentCustomerFormat As Boolean = False
    Dim flgCommentArticleFormat As Boolean = False
    Dim flgCommentArticleSizeFormat As Boolean = False
    Dim flgCommentArticleColorFormat As Boolean = False
    Dim flgCommentQtyFormat As Boolean = False
    Dim flgCommentPriceFormat As Boolean = False
    Dim flgCommentRemarksFormat As Boolean = False
    Dim flgCommentEngineNo As Boolean = False
    'End Task:2369
    Dim flgVehicleIdentificationInfo As Boolean = False 'Task:M16 Added Flag Vehicle Identification
    Dim flgMargeItem As Boolean = False ''20-Feb-2014   TASK:2432 Imran Ali 1 Delivery Chalan Multi Record of Same Item 
    Dim ItemLoadByCustomer As Boolean = False
    Dim objint As Integer = 0I
    Enum GrdEnum
        LocationId
        ArticleCode
        Item
        BatchNo
        Unit
        Qty
        Rate
        Total
        CategoryId
        ItemId
        PackQty
        CurrentPrice
        PackPrice
        BatchId
        'SampleQty 'Task:2374
        Tax_Percent
        Tax_Amount
        TotalAmount 'Task:2374 Added Index
        SampleQty 'Task:2374 Change Index Position
        PurchasePrice
        Pack_Desc
        AccountId
        ''Added Index Comments
        Comments
        Engine_No 'Task:M16 Added Index
        Chassis_No 'Task:M16 Added Index
        CostPrice 'Task:2435 Added Index
    End Enum

    Enum enmMaster
        SalesReturnNo
        SalesReturnDate
        CustomerName
        SalesNo
        SalesReturnQty
        SalesReturnAmount
        SalesReturnId
        CustomerCode
        EmployeeName
        Remarks
        CashPaid
        EmployeeCode
        PoId
        AdjPercent
        Adjustment
        MarketReturns
        Damage_Budget
        Post
        CostCenterId
        Status
        PrintStatus
        CostCenterName
        Email
        Company
    End Enum

    Public Function Update_Record() As Boolean


        Dim objCommand As New OleDbCommand
        Dim objCon As New OleDbConnection
        Dim trans As OleDbTransaction = Nothing

        Try

            'Me.grd.UpdateData()
            'Dim objCommand As New OleDbCommand
            'Dim objCon As OleDbConnection
            'Dim i As Integer
            'Dim cmbProject.SelectedValue As Integer = GetCostCenterId(Me.cmbCompany.SelectedValue)

            If Not getConfigValueByType("CommentCustomerFormat").ToString = "Error" Then
                flgCommentCustomerFormat = Convert.ToBoolean(getConfigValueByType("CommentCustomerFormat").ToString)
            Else
                flgCommentCustomerFormat = False
            End If
            If Not getConfigValueByType("CommentArticleFormat").ToString = "Error" Then
                flgCommentArticleFormat = Convert.ToBoolean(getConfigValueByType("CommentArticleFormat").ToString)
            Else
                flgCommentArticleFormat = False
            End If
            If Not getConfigValueByType("CommentArticleSizeFormat").ToString = "Error" Then
                flgCommentArticleSizeFormat = Convert.ToBoolean(getConfigValueByType("CommentArticleSizeFormat").ToString)
            Else
                flgCommentArticleSizeFormat = False
            End If
            If Not getConfigValueByType("CommentArticleColorFormat").ToString = "Error" Then
                flgCommentArticleColorFormat = Convert.ToBoolean(getConfigValueByType("CommentArticleColorFormat").ToString)
            Else
                flgCommentArticleColorFormat = False
            End If
            If Not getConfigValueByType("CommentQtyFormat").ToString = "Error" Then
                flgCommentQtyFormat = Convert.ToBoolean(getConfigValueByType("CommentQtyFormat").ToString)
            Else
                flgCommentQtyFormat = False
            End If
            If Not getConfigValueByType("CommentPriceFormat").ToString = "Error" Then
                flgCommentPriceFormat = Convert.ToBoolean(getConfigValueByType("CommentPriceFormat").ToString)
            Else
                flgCommentPriceFormat = False
            End If
            If Not getConfigValueByType("CommentRemarksFormat").ToString = "Error" Then
                flgCommentRemarksFormat = Convert.ToBoolean(getConfigValueByType("CommentRemarksFormat").ToString)
            Else
                flgCommentRemarksFormat = False
            End If

            Dim AccountId As Integer = getConfigValueByType("SalesCreditAccount")
            Dim SalesTaxAccountId As Integer = getConfigValueByType("SalesTaxCreditAccount")
            'Dim SalesOrderAnalysis As Boolean = False
            'If Not GetConfigValue("SalesOrderAnalysis").ToString = "Error" Then
            '    SalesOrderAnalysis = Convert.ToBoolean(GetConfigValue("SalesOrderAnalysis").ToString)
            'End If
            If Not getConfigValueByType("OtherExpAccount").ToString = "Error" Then
                MarketReturnAccountId = CInt(getConfigValueByType("OtherExpAccount"))
            Else
                MarketReturnAccountId = 0
            End If
            If Not getConfigValueByType("MarketReturnVoucher").ToString = "Error" Then
                flgMarketReturnVoucher = Convert.ToBoolean(getConfigValueByType("MarketReturnVoucher").ToString)
            Else
                flgMarketReturnVoucher = False
            End If
            Dim flgCgsVoucher As Boolean = False
            If Not getConfigValueByType("CGSVoucher").ToString = "Error" Then
                flgCgsVoucher = getConfigValueByType("CGSVoucher")
            End If
            Dim InvAccountId As Integer = Val(getConfigValueByType("InvAccountId").ToString) 'GetConfigValue("InvAccountId") 'Inventory Account
            Dim CgsAccountId As Integer = Val(getConfigValueByType("CGSAccountId").ToString)
            Dim GLAccountArticleDepartment As Boolean
            If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
                GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
            Else
                GLAccountArticleDepartment = False
            End If



            Dim strQuery As String = "Select StockTransId, DocNo, DocDate From StockMasterTable WHERE LEFT(DocNo,2)='SR' AND (Convert(Varchar, DocDate, 102) BETWEEN Convert(DateTime, '" & frmAverageRateUpdate.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & frmAverageRateUpdate.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) ORDER BY (Convert(varchar, DocDate, 102)) ASC"
            Dim dtStock As New DataTable
            dtStock = GetDataTable(strQuery)


            Dim strQuery2 As String = "Select ArticleId From ArticleDefTable WHERE ArticleId In(Select DISTINCT ArticleDefId From SalesReturnDetailTable) ORDER BY ArticleId ASC"
            Dim dtArticle As New DataTable
            dtArticle = GetDataTable(strQuery2)

            If dtArticle IsNot Nothing Then
                If dtArticle.Rows.Count > 0 Then

                    frmAverageRateUpdate.prbOverall.Minimum = 0
                    frmAverageRateUpdate.prbOverall.Maximum = dtArticle.Rows.Count
                    frmAverageRateUpdate.prbOverall.Value = 0

                    For Each rArticle As DataRow In dtArticle.Rows

                        Dim strQuery1 As String = "Select  DISTINCT (Convert(Varchar,ReceivingDate,102)) as ReceivingDate From ReceivingMasterTable WHERE (Convert(Varchar, ReceivingDate, 102) BETWEEN Convert(DateTime, '" & frmAverageRateUpdate.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & frmAverageRateUpdate.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND ReceivingId In (Select ReceivingId From ReceivingDetailTable WHERE ArticleDefId=" & rArticle.Item(0).ToString & ")  Union Select GetDate() ORDER BY ReceivingDate ASC"
                        Dim dtPur As New DataTable
                        dtPur = GetDataTable(strQuery1)
                        If dtPur IsNot Nothing Then
                            If dtStock.Rows.Count > 0 Then
                                For i As Integer = 0 To dtPur.Rows.Count - 1
                                    For j As Integer = i + 1 To dtPur.Rows.Count - 1

                                        Dim MyFromDate As DateTime = CDate(dtPur.Rows(i).Item(0))
                                        Dim MyToDate As DateTime = CDate(dtPur.Rows(j).Item(0))

                                        Dim dtInv As New DataTable
                                        dtInv = GetDataTable("SELECT Recv.SalesReturnNo, Recv.SalesReturnDate AS Date, vwCOADetail.detail_title as CustomerName, V.SalesNo, Recv.SalesReturnQty, Recv.SalesReturnAmount, Recv.SalesReturnId," & _
                                                  " Recv.CustomerCode, EmployeeDefTable.EmployeeName, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId, isnull(Recv.AdjPercent,0) as AdjPercent, isnull(Recv.Adjustment,0) as Adjustment, isnull(Recv.MarketReturns,0) as [Market Returns], ISNULL(Recv.Damage_Budget,0) as Damage_Budget, IsNull(Recv.Post,0) as Post, Isnull(Recv.CostCenterId,0) as CostCenterId, Case When IsNull(Recv.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], tblDefCostCenter.Name, dbo.vwCOADetail.Contact_Email as Email, IsNull(Recv.LocationId,0) as Company " & _
                                                  " FROM tblDefCostCenter Right Outer JOIN SalesReturnMasterTable Recv ON tblDefCostCenter.CostCenterID=Recv.CostCenterId INNER JOIN  " & _
                                                  " vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " & _
                                                  " EmployeeDefTable ON Recv.EmployeeCode = EmployeeDefTable.EmployeeId LEFT OUTER JOIN " & _
                                                  " SalesMasterTable V ON Recv.POId = V.SalesId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.SalesReturnNo  WHERE (Convert(DateTime, Recv.SalesReturnDate,102) BETWEEN Convert(DateTime,'" & MyFromDate.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & MyToDate.ToString("yyyy-M-d 23:59:59") & "',102)) AND Recv.SalesReturnId In(Select Distinct SalesReturnId From SalesReturnDetailTable WHERE ArticleDefId=" & Val(rArticle.Item(0).ToString) & ") ORDER BY Recv.SalesReturnDate ASC")

                                        dtInv.AcceptChanges()
                                        dtInv.TableName = "Invoice"
                                        If dtInv IsNot Nothing Then
                                            If dtInv.Rows.Count > 0 Then

                                                frmAverageRateUpdate.prbInvoices.Minimum = 1
                                                frmAverageRateUpdate.prbInvoices.Maximum = dtInv.Rows.Count
                                                frmAverageRateUpdate.prbInvoices.Step = dtInv.Rows.Count
                                                frmAverageRateUpdate.prbInvoices.Value = 1

                                                Dim y As Integer = 0I


                                                For Each objMasterRow As DataRow In dtInv.Rows


                                                    frmAverageRateUpdate.Label5.Text = "Invoice No. " & objMasterRow.Item(enmMaster.SalesReturnNo).ToString & "   Date: " & objMasterRow.Item(enmMaster.SalesReturnDate).ToString & ""
                                                    Application.DoEvents()

                                                    objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")
                                                    If objCon.State = ConnectionState.Open Then objCon.Close()
                                                    objCon.Open()
                                                    trans = objCon.BeginTransaction

                                                    Dim cmd As New OleDbCommand
                                                    cmd.Connection = objCon

                                                    objCommand.Connection = objCon
                                                    objCommand.Transaction = trans
                                                    objCommand.CommandType = CommandType.Text


                                                    Dim lngVoucherMasterId As Integer = GetVoucherId("frmSalesReturn", objMasterRow.Item(enmMaster.SalesReturnNo).ToString, trans)


                                                    StockMaster = New StockMaster
                                                    Dim drFound() As DataRow = dtStock.Select("DocNo='" & objMasterRow.Item(enmMaster.SalesReturnNo).ToString & "'", "")
                                                    If drFound.Length > 0 Then
                                                        StockMaster.StockTransId = Val(drFound(0).Item(0).ToString)
                                                    Else
                                                        StockMaster.StockTransId = 0I
                                                    End If
                                                    StockMaster.DocNo = objMasterRow.Item(enmMaster.SalesReturnNo).ToString.Replace("'", "''")
                                                    StockMaster.DocDate = objMasterRow.Item(enmMaster.SalesReturnDate)
                                                    StockMaster.DocType = 4 'Convert.ToInt32(GetStockDocTypeId("Sales").ToString)
                                                    StockMaster.Remaks = objMasterRow.Item(enmMaster.Remarks).ToString.Replace("'", "''")
                                                    StockMaster.Project = Val(objMasterRow.Item(enmMaster.CostCenterId).ToString)
                                                    StockMaster.StockDetailList = New List(Of StockDetail)

                                                    Dim strVoucherNo As String = String.Empty
                                                    Dim MarketReturnsRate As Double = 0D

                                                    '***********************
                                                    'Deleting Detail
                                                    '***********************
                                                    objCommand.CommandText = String.Empty
                                                    objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
                                                    objCommand.ExecuteNonQuery()

                                                    Dim LocationType As String = String.Empty
                                                    Dim dblAdj As Double = 0D
                                                    Dim dblNet As Double = 0D
                                                    Dim dblDmgBudget As Double = 0D
                                                    'dblDmgBudget = (Val(Me.txtDamageBudget.Text) / Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum))
                                                    'dblNet = (((Val(Me.txtAdjPercent.Text) * Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum))) / 100)) + Math.Abs(Val(Me.txtAdjustment.Text))
                                                    'dblAdj = Math.Abs(((Val(Me.txtAdjPercent.Text) * Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum))) / 100)) + Math.Abs(Val(Me.txtAdjustment.Text))
                                                    'dblAdj = (dblAdj / Me.grd.RecordCount)

                                                    Dim dblMarketReturns As Double = 0D
                                                    Dim CrrStock As Double = 0D
                                                    Dim CostPrice As Double = 0D

                                                    dblMarketReturns = Val(objMasterRow.Item(enmMaster.MarketReturns).ToString)

                                                    Dim strQry As String = String.Empty
                                                    strQry = "SELECT Recv_D.LocationID, Article.ArticleCode, Article.ArticleDescription AS item, isnull(Recv_D.BatchNo,'xxxx') as BatchNo, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
                                                            & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
                                                            & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Recv_D.BatchID, IsNull(Recv_D.Tax_Percent,0) AS Tax_Percent, Convert(float, 0) as TaxAmount, Convert(float,0) as [Total Amount], IsNull(Recv_D.SampleQty,0) as SampleQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article_Group.SubSubId,0) as SalesAccountId,Recv_D.Comments,Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.CostPrice,0) as CostPrice   FROM dbo.SalesReturnDetailTable Recv_D LEFT OUTER JOIN " _
                                                            & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                                                            & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
                                                            & " LEFT OUTER JOIN tblDefLocation ON Recv_D.LocationID = tblDefLocation.Location_ID Where Recv_D.SalesReturnID =" & Val(objMasterRow.Item(enmMaster.SalesReturnId).ToString) & ""

                                                    Dim dtData As New DataTable
                                                    dtData = GetDataTable(strQry, trans)
                                                    dtData.AcceptChanges()
                                                    StockList = New List(Of StockDetail)



                                                    'objCommand.CommandText = String.Empty
                                                    'objCommand.CommandText = "Delete from SalesReturnDetailTable where SalesReturnID = " & Val(objMasterRow.Item(enmMaster.SalesReturnId).ToString)
                                                    'objCommand.ExecuteNonQuery()


                                                    For f As Integer = 0 To dtData.Rows.Count - 1
                                                        If Val(dtData.Rows(f).Item("Qty").ToString) <> 0 Or Val(dtData.Rows(f).Item("SampleQty").ToString) <> 0 Then
                                                            If GLAccountArticleDepartment = True Then
                                                                'Before against task:2390
                                                                'AccountId = Val(dtData.Rows(f).Item("SalesAccountId").ToString)
                                                                'Task:2390 Change Inventory Account
                                                                InvAccountId = Val(dtData.Rows(f).Item("SalesAccountId").ToString)
                                                                'End Task:2390
                                                            End If

                                                            If flgCgsVoucher = True Then
                                                                Try
                                                                    CostPrice = Val(dtData.Rows(f).Item("CostPrice").ToString) 'Task:2435 Set CostPrice Value
                                                                    If CostPrice = 0 Then
                                                                        objCommand.CommandText = ""
                                                                        'Before against task:2435
                                                                        'objCommand.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, 0 as PurchasePrice, ABS(SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0))) AS qty, ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))) as Amount  " _
                                                                        '                                & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                                        '                                & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId=" & dtData.Rows(f).Item("ArticleDefId").ToString & " AND StockTransId IN(Select StockTransId From StockMasterTable WHERE StockTransId in (Select StockTransId From StockMasterTable WHERE DocNo <> '" & Me.txtPONo.Text.Replace("'", "''") & "'))" _
                                                                        '                                & " GROUP BY dbo.StockDetailTable.ArticleDefId "
                                                                        'Before against task:2712
                                                                        'Task:2435 Filter By Date
                                                                        'objCommand.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, 0 as PurchasePrice, ABS(SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0))) AS qty, ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))) as Amount  " _
                                                                        '                 & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                                        '                 & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId=" & dtData.Rows(f).Item("ArticleDefId").ToString & " AND StockTransId IN(Select StockTransId From StockMasterTable WHERE StockTransId in (Select StockTransId From StockMasterTable WHERE DocNo <> '" & Me.txtPONo.Text.Replace("'", "''") & "') AND (Convert(Varchar, DocDate, 102) <= Convert(Datetime, '" & Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)))" _
                                                                        '                 & " GROUP BY dbo.StockDetailTable.ArticleDefId "
                                                                        'End Task:2435
                                                                        'Task:2712 Rounded Amount
                                                                        objCommand.CommandText = "SELECT dbo.StockDetailTable.ArticleDefId, 0 as PurchasePrice, ABS(SUM(Isnull(dbo.StockDetailTable.InQty,0) - Isnull(dbo.StockDetailTable.OutQty,0))) AS qty, Rounded(ABS(SUM(Isnull(dbo.StockDetailTable.InAmount,0) - Isnull(dbo.StockDetailTable.OutAmount,0))),1) as Amount  " _
                                                                                        & " FROM dbo.ArticleDefTable INNER JOIN " _
                                                                                        & " dbo.StockDetailTable ON dbo.ArticleDefTable.ArticleId = dbo.StockDetailTable.ArticleDefId WHERE dbo.ArticleDefTable.ArticleId=" & dtData.Rows(f).Item("ArticleDefId").ToString & " AND StockTransId IN(Select StockTransId From StockMasterTable WHERE StockTransId in (Select StockTransId From StockMasterTable WHERE DocNo <> '" & objMasterRow.Item(enmMaster.SalesReturnNo).ToString.Replace("'", "''") & "') AND (Convert(Varchar, DocDate, 102) <= Convert(Datetime, '" & CDate(objMasterRow.Item(enmMaster.SalesReturnDate).ToString).ToString("yyyy-M-d 00:00:00") & "', 102)))" _
                                                                                        & " GROUP BY dbo.StockDetailTable.ArticleDefId "
                                                                        'End Task:2712
                                                                        Dim dtCrrStock As New DataTable
                                                                        Dim daCrrStock As New OleDbDataAdapter(objCommand)
                                                                        daCrrStock.Fill(dtCrrStock)

                                                                        If dtCrrStock IsNot Nothing Then
                                                                            If dtCrrStock.Rows.Count > 0 Then
                                                                                'Before against task:2401
                                                                                'If Val(dtData.Rows(f).Item("Price").ToString) <> 0 Then
                                                                                If Val(dtData.Rows(f).Item("Price").ToString) <> 0 AndAlso Val(dtCrrStock.Rows(0).Item("qty").ToString) <> 0 Then
                                                                                    'End Task:2401
                                                                                    CrrStock = dtCrrStock.Rows(0).Item(2)
                                                                                    CostPrice = IIf(dtCrrStock.Rows(0).Item(3) + CrrStock = 0, 0, dtCrrStock.Rows(0).Item(3) / CrrStock)
                                                                                Else
                                                                                    CostPrice = Val(dtData.Rows(f).Item("PurchasePrice").ToString)
                                                                                End If
                                                                            Else
                                                                                CostPrice = Val(dtData.Rows(f).Item("PurchasePrice").ToString)
                                                                            End If
                                                                        End If
                                                                    End If
                                                                Catch ex As Exception

                                                                End Try


                                                            End If


                                                            StockDetail = New StockDetail
                                                            StockDetail.StockTransId = 0 'Convert.ToInt32(GetStockTransId(Me.txtPONo.Text).ToString)
                                                            StockDetail.LocationId = dtData.Rows(f).Item("LocationId").ToString
                                                            StockDetail.ArticleDefId = dtData.Rows(f).Item("ArticleDefId").ToString
                                                            StockDetail.InQty = IIf(dtData.Rows(f).Item("Unit").ToString = "Loose", Val(dtData.Rows(f).Item("Qty").ToString) + Val(dtData.Rows(f).Item("SampleQty").ToString), ((Val(dtData.Rows(f).Item("Qty").ToString) + Val(dtData.Rows(f).Item("SampleQty").ToString)) * Val(dtData.Rows(f).Item("PackQty").ToString)))
                                                            StockDetail.OutQty = 0
                                                            StockDetail.Rate = IIf(CostPrice = 0, Val(dtData.Rows(f).Item("PurchasePrice").ToString), CostPrice)
                                                            StockDetail.InAmount = IIf(dtData.Rows(f).Item("Unit").ToString = "Loose", ((Val(dtData.Rows(f).Item("Qty").ToString) + Val(dtData.Rows(f).Item("SampleQty").ToString)) * IIf(CostPrice = 0, Val(dtData.Rows(f).Item("PurchasePrice").ToString), CostPrice)), (((Val(dtData.Rows(f).Item("Qty").ToString) + Val(dtData.Rows(f).Item("SampleQty").ToString)) * Val(dtData.Rows(f).Item("PackQty").ToString)) * IIf(CostPrice = 0, Val(dtData.Rows(f).Item("PurchasePrice").ToString), CostPrice)))
                                                            StockDetail.OutAmount = 0
                                                            StockDetail.Remarks = String.Empty
                                                            'Task:M16 Set Values In Engine_No and Chassis_No 
                                                            StockDetail.Engine_No = dtData.Rows(f).Item("Engine_No").ToString
                                                            StockDetail.Chassis_No = dtData.Rows(f).Item("Chassis_No").ToString
                                                            'End Task:M16
                                                            StockList.Add(StockDetail)



                                                            LocationType = GetDataTable("Select Location_Type From tblDefLocation WHERE Location_Id=" & dtData.Rows(f).Item("LocationId").ToString, trans).Rows(0).Item(0).ToString
                                                            objCommand.CommandText = ""
                                                            'objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice, BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc) values( " _
                                                            '                         & " " & txtReceivingID.Text & " ," & Val(dtData.Rows(f).Item("ArticleDefId").ToString) & ",'" & (dtData.Rows(f).Item("Unit").ToString) & "'," & Val(dtData.Rows(f).Item("Qty").ToString) & ", " _
                                                            '                         & " " & IIf(dtData.Rows(f).Item("Unit").ToString = "Loose", Val(dtData.Rows(f).Item("qty").ToString), (Val(dtData.Rows(f).Item("Qty").ToString) * Val(dtData.Rows(f).Item("PackQty").ToString))) & ", " & Val(dtData.Rows(f).Item("Price").ToString) & ", " & Val(dtData.Rows(f).Item("PackQty").ToString) & " , " & Val(dtData.Rows(f).Item("CurrentPrice").ToString) & ",'" & dtData.Rows(f).Item("BatchNo").ToString & "', " & dtData.Rows(f).Item("BatchId").ToString & "," & dtData.Rows(f).Item("LocationID").ToString & ", " & Val(dtData.Rows(f).Item("Tax_Percent").ToString) & ", " & Val(dtData.Rows(f).Item("SampleQty").ToString) & ", " & Val(dtData.Rows(f).Item("PackPrice").ToString) & ", " & Val(dtData.Rows(f).Item("PurchasePrice").ToString) & ",'" & dtData.Rows(f).Item("Pack_Desc").ToString.Replace("'", "''") & "') "
                                                            'objCommand.ExecuteNonQuery()
                                                            'Before against task:2435
                                                            'R916 Added Column Comments
                                                            'objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice, BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc, Comments) values( " _
                                                            '                 & " " & txtReceivingID.Text & " ," & Val(dtData.Rows(f).Item("ArticleDefId").ToString) & ",'" & (dtData.Rows(f).Item("Unit").ToString) & "'," & Val(dtData.Rows(f).Item("Qty").ToString) & ", " _
                                                            '                 & " " & IIf(dtData.Rows(f).Item("Unit").ToString = "Loose", Val(dtData.Rows(f).Item("qty").ToString), (Val(dtData.Rows(f).Item("Qty").ToString) * Val(dtData.Rows(f).Item("PackQty").ToString))) & ", " & Val(dtData.Rows(f).Item("Price").ToString) & ", " & Val(dtData.Rows(f).Item("PackQty").ToString) & " , " & Val(dtData.Rows(f).Item("CurrentPrice").ToString) & ",'" & dtData.Rows(f).Item("BatchNo").ToString & "', " & dtData.Rows(f).Item("BatchId").ToString & "," & dtData.Rows(f).Item("LocationID").ToString & ", " & Val(dtData.Rows(f).Item("Tax_Percent").ToString) & ", " & Val(dtData.Rows(f).Item("SampleQty").ToString) & ", " & Val(dtData.Rows(f).Item("PackPrice").ToString) & ", " & Val(dtData.Rows(f).Item("PurchasePrice").ToString) & ",'" & dtData.Rows(f).Item("Pack_Desc").ToString.Replace("'", "''") & "', '" & dtData.Rows(f).Item("Comments").ToString.Replace("'", "''") & "') "
                                                            'Task:2435 Added Column CostPrice
                                                            'Before against task:2527 
                                                            'objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice, BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc, Comments, CostPrice) values( " _
                                                            '                & " " & txtReceivingID.Text & " ," & Val(dtData.Rows(f).Item("ArticleDefId").ToString) & ",'" & (dtData.Rows(f).Item("Unit").ToString) & "'," & Val(dtData.Rows(f).Item("Qty").ToString) & ", " _
                                                            '                & " " & IIf(dtData.Rows(f).Item("Unit").ToString = "Loose", Val(dtData.Rows(f).Item("qty").ToString), (Val(dtData.Rows(f).Item("Qty").ToString) * Val(dtData.Rows(f).Item("PackQty").ToString))) & ", " & Val(dtData.Rows(f).Item("Price").ToString) & ", " & Val(dtData.Rows(f).Item("PackQty").ToString) & " , " & Val(dtData.Rows(f).Item("CurrentPrice").ToString) & ",'" & dtData.Rows(f).Item("BatchNo").ToString & "', " & dtData.Rows(f).Item("BatchId").ToString & "," & dtData.Rows(f).Item("LocationID").ToString & ", " & Val(dtData.Rows(f).Item("Tax_Percent").ToString) & ", " & Val(dtData.Rows(f).Item("SampleQty").ToString) & ", " & Val(dtData.Rows(f).Item("PackPrice").ToString) & ", " & Val(dtData.Rows(f).Item("PurchasePrice").ToString) & ",'" & dtData.Rows(f).Item("Pack_Desc").ToString.Replace("'", "''") & "', '" & dtData.Rows(f).Item("Comments").ToString.Replace("'", "''") & "', " & Val(StockDetail.Rate) & ") "
                                                            'Task:2527 Added Column Engine_No, Chassis_No
                                                            'objCommand.CommandText = "Insert into SalesReturnDetailTable (SalesReturnId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice, BatchNo, BatchID,LocationID, Tax_Percent, SampleQty,PackPrice,PurchasePrice, Pack_Desc, Comments, CostPrice,Engine_No,Chassis_No) values( " _
                                                            '                & " " & Val(objMasterRow.Item(enmMaster.SalesReturnId).ToString) & " ," & Val(dtData.Rows(f).Item("ArticleDefId").ToString) & ",'" & (dtData.Rows(f).Item("Unit").ToString) & "'," & Val(dtData.Rows(f).Item("Qty").ToString) & ", " _
                                                            '                & " " & IIf(dtData.Rows(f).Item("Unit").ToString = "Loose", Val(dtData.Rows(f).Item("qty").ToString), (Val(dtData.Rows(f).Item("Qty").ToString) * Val(dtData.Rows(f).Item("PackQty").ToString))) & ", " & Val(dtData.Rows(f).Item("Price").ToString) & ", " & Val(dtData.Rows(f).Item("PackQty").ToString) & " , " & Val(dtData.Rows(f).Item("CurrentPrice").ToString) & ",'" & dtData.Rows(f).Item("BatchNo").ToString & "', " & dtData.Rows(f).Item("BatchId").ToString & "," & dtData.Rows(f).Item("LocationID").ToString & ", " & Val(dtData.Rows(f).Item("Tax_Percent").ToString) & ", " & Val(dtData.Rows(f).Item("SampleQty").ToString) & ", " & Val(dtData.Rows(f).Item("PackPrice").ToString) & ", " & Val(dtData.Rows(f).Item("PurchasePrice").ToString) & ",'" & dtData.Rows(f).Item("Pack_Desc").ToString.Replace("'", "''") & "', '" & dtData.Rows(f).Item("Comments").ToString.Replace("'", "''") & "', " & Val(StockDetail.Rate) & ", '" & dtData.Rows(f).Item("Engine_No").ToString.Replace("'", "''") & "','" & dtData.Rows(f).Item("Chassis_No").ToString.Replace("'", "''") & "') "

                                                            'objCommand.ExecuteNonQuery()

                                                            objCommand.CommandText = ""
                                                            objCommand.CommandText = "Update SalesReturnDetailTable Set CostPrice=" & Val(StockDetail.Rate) & " WHERE ArticleDefId=" & Val(dtData.Rows(f).Item(GrdEnum.ItemId).ToString) & " AND SalesReturnId=" & Val(objMasterRow.Item(enmMaster.SalesReturnId).ToString) & ""
                                                            objCommand.ExecuteNonQuery()


                                                            'Val(grd.Rows(i).Cells(5).Value)
                                                            '***********************
                                                            'Inserting Debit Amount
                                                            '***********************

                                                            If Not flgMarketReturnVoucher = True Then
                                                                '***********************
                                                                'Inserting Debit Amount
                                                                '***********************
                                                                'Before against task:2369
                                                                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId) " _
                                                                '                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & (IIf(dtData.Rows(f).Item("Unit").ToString = "Loose", Val(dtData.Rows(f).Item("Qty").ToString), (Val(dtData.Rows(f).Item("Qty").ToString) * Val(dtData.Rows(f).Item("PackQty").ToString))) * Val(dtData.Rows(f).Item("Price").ToString)) & ", 0, '" & dtData.Rows(f).Item("item").ToString & "(" & Val(dtData.Rows(f).Item("Qty").ToString) & ")', " & dtData.Rows(f).Item("ArticleDefId").ToString & ", " & cmbProject.SelectedValue & ")"
                                                                'objCommand.ExecuteNonQuery()
                                                                'Task:2369 Change Comments
                                                                objCommand.CommandText = String.Empty
                                                                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, direction, CostCenterId) " _
                                                                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & (IIf(dtData.Rows(f).Item("Unit").ToString = "Loose", Val(dtData.Rows(f).Item("Qty").ToString), (Val(dtData.Rows(f).Item("Qty").ToString) * Val(dtData.Rows(f).Item("PackQty").ToString))) * Val(dtData.Rows(f).Item("Price").ToString)) & ", 0, '" & SetComments(dtData.Rows(f), objMasterRow.Item(enmMaster.CustomerName).ToString, objMasterRow.Item(enmMaster.Remarks).ToString).Replace("'", "''") & "', " & dtData.Rows(f).Item("ArticleDefId").ToString & ", " & Val(objMasterRow.Item(enmMaster.CostCenterId).ToString) & ")"
                                                                objCommand.ExecuteNonQuery()
                                                                'End Task:2369

                                                                '***********************
                                                                'Inserting Credit Amount
                                                                '***********************
                                                                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, direction,CostCenterId) " _
                                                                '                           & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Me.cmbVendor.ActiveRow.Cells(0).Value & ",0, " & (IIf(dtData.Rows(f).Item("Unit").ToString = "Loose", Val(dtData.Rows(f).Item("Qty").ToString), (Val(dtData.Rows(f).Item("Qty").ToString) * Val(dtData.Rows(f).Item("PackQty").ToString))) * Val(dtData.Rows(f).Item("Price").ToString)) & ", '" & dtData.Rows(f).Item("item").ToString & "(" & Val(dtData.Rows(f).Item("Qty").ToString) & ")', " & dtData.Rows(f).Item("ArticleDefId").ToString & ", " & cmbProject.SelectedValue & ")"
                                                                'objCommand.ExecuteNonQuery()
                                                                objCommand.CommandText = String.Empty
                                                                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, direction,CostCenterId) " _
                                                                                         & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Val(objMasterRow.Item(enmMaster.CustomerCode).ToString) & ",0, " & (IIf(dtData.Rows(f).Item("Unit").ToString = "Loose", Val(dtData.Rows(f).Item("Qty").ToString), (Val(dtData.Rows(f).Item("Qty").ToString) * Val(dtData.Rows(f).Item("PackQty").ToString))) * Val(dtData.Rows(f).Item("Price").ToString)) & ", '" & SetComments(dtData.Rows(f), objMasterRow.Item(enmMaster.CustomerName).ToString, objMasterRow.Item(enmMaster.Remarks).ToString).Replace("" & objMasterRow.Item(enmMaster.CustomerName).ToString & "", "").Replace("'", "''") & "', " & dtData.Rows(f).Item("ArticleDefId").ToString & ", " & Val(objMasterRow.Item(enmMaster.CostCenterId).ToString) & ")"
                                                                objCommand.ExecuteNonQuery()
                                                                'End Task:23639

                                                            End If


                                                            'Try



                                                            '    If SalesOrderId > 0 Then

                                                            '        objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                                            '                                                           & " SET  DeliveredQty = (isnull(DeliveredQty,0) -  " & IIf(dtData.Rows(f).Item("Unit").ToString = "Loose", Val(dtData.Rows(f).Item("Qty").ToString), (Val(dtData.Rows(f).Item("Qty").ToString) * Val(dtData.Rows(f).Item("PackQty").ToString))) & ") " _
                                                            '                                                           & " WHERE     (SalesOrderId = " & SalesOrderId & ") AND (ArticleDefId = " & Val(dtData.Rows(f).Item("ArticleDefId").ToString) & ")"
                                                            '        objCommand.ExecuteNonQuery()



                                                            '    End If
                                                            'Catch ex As Exception

                                                            'End Try



                                                        End If



                                                        If flgCgsVoucher = True Then

                                                            objCommand.CommandText = ""
                                                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount, debit_amount, comments, CostCenterId, direction, sp_refrence) " _
                                                                                                                                      & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & CgsAccountId & ", " & IIf(dtData.Rows(f).Item("Unit").ToString = "Loose", Val(dtData.Rows(f).Item("Qty").ToString) * CostPrice, (Val(dtData.Rows(f).Item("Qty").ToString) * Val(dtData.Rows(f).Item("PackQty").ToString)) * CostPrice) & ", 0, '" & dtData.Rows(f).Item("Item").ToString & " " & "(" & Val(dtData.Rows(f).Item("Qty").ToString) & " X " & CostPrice & ")', " & Val(objMasterRow.Item(enmMaster.CostCenterId).ToString) & ", " & dtData.Rows(f).Item("ArticleDefId").ToString & ", '" & objMasterRow.Item(enmMaster.Remarks).ToString.Replace("'", "''") & "')"
                                                            objCommand.ExecuteNonQuery()

                                                            objCommand.CommandText = ""
                                                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount, debit_amount,  comments, CostCenterId, direction, sp_refrence) " _
                                                                                                                                      & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & InvAccountId & ", 0, " & IIf(dtData.Rows(f).Item("Unit").ToString = "Loose", Val(dtData.Rows(f).Item("Qty").ToString) * CostPrice, (Val(dtData.Rows(f).Item("Qty").ToString) * Val(dtData.Rows(f).Item("PackQty").ToString)) * CostPrice) & ", '" & dtData.Rows(f).Item("Item").ToString & " " & "(" & Val(dtData.Rows(f).Item("Qty").ToString) & " X " & CostPrice & ")', " & Val(objMasterRow.Item(enmMaster.CostCenterId).ToString) & ", " & dtData.Rows(f).Item("ArticleDefId").ToString & ", '" & objMasterRow.Item(enmMaster.Remarks).ToString.Replace("'", "''") & "')"
                                                            objCommand.ExecuteNonQuery()
                                                        End If
                                                        '''''''''''''''''''''''''''''' Code By Imran Ali 03/06/2013 '''''''''''''''''''''''''''''''''''''''
                                                        'End If



                                                    Next

                                                    Dim dblTotalAmount As Object = Val(objMasterRow.Item(enmMaster.SalesReturnAmount).ToString)
                                                    Dim dblTotalQty As Object = Val(objMasterRow.Item(enmMaster.SalesReturnQty).ToString)
                                                    Dim dblTaxAmount As Object = dtData.Compute("SUM(TaxAmount)", "")


                                                    If flgMarketReturnVoucher = True Then
                                                        If Val(dblTotalAmount) > 0 Then
                                                            '***********************
                                                            'Inserting Debit Amount
                                                            '***********************
                                                            objCommand.CommandText = String.Empty
                                                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId) " _
                                                                                   & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ", " & Val(dblTotalAmount) & ", 0, 'Damage Budget " & "(Total Qty" & Val(dblTotalQty) & ")', " & Val(objMasterRow.Item(enmMaster.CostCenterId).ToString) & ")"
                                                            objCommand.ExecuteNonQuery()

                                                            '***********************
                                                            'Inserting Credit Amount
                                                            '***********************
                                                            objCommand.CommandText = String.Empty
                                                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
                                                                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Val(objMasterRow.Item(enmMaster.CustomerCode).ToString) & ",0, " & ((Val(dblTotalAmount))) & ", 'Damage Budget " & "(Total Qty" & Val(dblTotalQty) & ")', " & Val(objMasterRow.Item(enmMaster.CostCenterId).ToString) & ")"
                                                            objCommand.ExecuteNonQuery()
                                                        Else

                                                            objCommand.CommandText = String.Empty
                                                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments,CostCenterId) " _
                                                                                                      & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & AccountId & ",0, " & Math.Abs(Val(dblTotalAmount)) & ",  'Damage Budget" & "(Total Qty" & Val(dblTotalQty) & ")', " & Val(objMasterRow.Item(enmMaster.CostCenterId).ToString) & " )"
                                                            objCommand.ExecuteNonQuery()

                                                            '***********************
                                                            'Inserting Credit Amount
                                                            '***********************
                                                            objCommand.CommandText = String.Empty
                                                            objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId) " _
                                                                                       & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Val(objMasterRow.Item(enmMaster.CustomerCode).ToString) & ", " & Math.Abs(Val(dblTotalAmount)) & ",0, 'Damage Budget " & "(Total Qty" & Val(dblTotalQty) & ")', " & Val(objMasterRow.Item(enmMaster.CostCenterId).ToString) & ")"
                                                            objCommand.ExecuteNonQuery()
                                                        End If
                                                    End If


                                                    If Val(dblTaxAmount) > 0 Then
                                                        objCommand.CommandText = String.Empty
                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount,debit_amount,  comments, CostCenterId) " _
                                                                                                  & " VALUES(" & lngVoucherMasterId & ", " & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & Val(objMasterRow.Item(enmMaster.CustomerCode).ToString) & ", " & Val(dblTaxAmount) & ", 0, 'Ref Tax Sales Return: " & objMasterRow.Item(enmMaster.SalesReturnNo).ToString & "', " & Val(objMasterRow.Item(enmMaster.CostCenterId).ToString) & ")"
                                                        objCommand.ExecuteNonQuery()

                                                        objCommand.CommandText = String.Empty
                                                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, credit_amount,debit_amount,comments, CostCenterId) " _
                                                                               & " VALUES(" & lngVoucherMasterId & "," & IIf(flgCompanyRights = True, " " & MyCompanyId & "", "1") & ", " & SalesTaxAccountId & ", 0,  " & Val(dblTaxAmount) & ", 'Ref Tax Sales Return: " & objMasterRow.Item(enmMaster.SalesReturnNo).ToString & "', " & Val(objMasterRow.Item(enmMaster.CostCenterId).ToString) & ")"
                                                        objCommand.ExecuteNonQuery()

                                                    End If


                                                    'Try


                                                    '    objCommand.CommandText = "Select SUM(isnull(Qty,0)-isnull(DeliveredQty , 0)) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & SalesOrderId & " Having SUM(isnull(Qty,0)-isnull(DeliveredQty , 0)) > 0 "
                                                    '    Dim daPOQty As New OleDbDataAdapter(objCommand)
                                                    '    Dim dtPOQty As New DataTable
                                                    '    daPOQty.Fill(dtPOQty)
                                                    '    Dim blnEqual1 As Boolean = True
                                                    '    If dtPOQty.Rows.Count > 0 Then
                                                    '        'For Each r As DataRow In dtPOQty.Rows
                                                    '        'If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
                                                    '        blnEqual1 = True
                                                    '        'Exit For
                                                    '        'End If
                                                    '        ' Next
                                                    '    End If
                                                    '    If blnEqual1 = True Then
                                                    '        objCommand.CommandText = "Update SalesOrderMasterTable set Status = '" & EnumStatus.Open.ToString & "' where SalesOrderID = " & SalesOrderId & ""
                                                    '        objCommand.ExecuteNonQuery()
                                                    '    End If
                                                    'Catch ex As Exception

                                                    'End Try

                                                    objCommand.CommandText = String.Empty
                                                    objCommand.CommandText = "Update SalesReturnMasterTable SET MarketReturns=" & dblMarketReturns & " WHERE SalesReturnId=" & Val(objMasterRow.Item(enmMaster.SalesReturnId).ToString)
                                                    objCommand.ExecuteNonQuery()


                                                    StockMaster.StockDetailList = StockList
                                                    'Call New StockDAL().UpdateByTrans(StockMaster, trans)
                                                    Call New StockDAL().Update(StockMaster, trans)

                                                    trans.Commit()
                                                    Update_Record = True
                                                    'SaveActivityLog("POS", "Sales Return", EnumActions.Update, LoginUserId, EnumRecordType.Sales, Me.txtPONo.Text.Trim, True)
                                                    'SaveActivityLog("Accounts", "Sales Return", EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, strVoucherNo, True)
                                                    'insertvoucher()
                                                    'Call Update1() 'Upgrading Stock ...
                                                    setEditMode = True
                                                    'setVoucherNo = Me.txtPONo.Text
                                                    'getVoucher_Id = Me.txtReceivingID.Text
                                                    'Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
                                                    'TaxAmount = Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                                                    dblMarketReturns = 0D
                                                    'SendSMS()
                                                    frmAverageRateUpdate.prbInvoices.PerformStep()
                                                Next
                                            End If
                                        End If
                                    Next
                                Next
                            End If
                        End If
                        frmAverageRateUpdate.prbOverall.Value += 1
                    Next
                End If
            End If


        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try



    End Function
    Public Function SetComments(ByVal GridExRow As DataRow, Optional ByVal CustomerName As String = "", Optional ByVal Remarks As String = "") As String
        Try
            Dim Comments As String = String.Empty
            If GridExRow IsNot Nothing Then
                If flgCommentCustomerFormat = True Then
                    Comments += CustomerName.Replace("'", "''") & ","
                End If
                If flgCommentArticleFormat = True Then
                    Comments += " " & GridExRow.Item(GrdEnum.Item).ToString & ","
                End If
                'If flgCommentArticleSizeFormat = True Then
                '    Comments += " " & GridExRow.Cells("Size").ToString & ","
                'End If
                'If flgCommentArticleColorFormat = True Then
                '    Comments += " " & GridExRow.Cells("Color").ToString & ","
                'End If
                If flgCommentQtyFormat = True Then
                    'Before against task:2583
                    'Comments += " " & IIf(Val(GridExRow.Cells("PackQty").ToString) = 0, Val(GridExRow.Cells("Qty").ToString), Val(GridExRow.Cells("Qty").ToString) * Val(GridExRow.Cells("PackQty").ToString))
                    'Task:2583 Changed Format Of Qty And Price
                    Comments += " (" & IIf(GridExRow.Item(GrdEnum.Unit).ToString = "Loose", Val(GridExRow.Item(GrdEnum.Qty).ToString), Val(GridExRow.Item(GrdEnum.Qty).ToString) * Val(GridExRow.Item(GrdEnum.PackQty).ToString))
                End If
                If flgCommentPriceFormat = True AndAlso flgCommentQtyFormat = True Then
                    'Task No 2608 Update The One Line Code To Get The RoundOff Functionality
                    'Comments += " X " & Val(GridExRow.Cells("Price").ToString) & ")"
                    Comments += " X " & Math.Round(Val(GridExRow.Item(GrdEnum.Rate).ToString), DecimalPointInValue) & ")"
                    'End Task 2608
                    'ElseIf flgCommentPriceFormat = True Then
                    '    Comments += " " & Val(GridExRow.Cells("Price").ToString)
                End If
                If flgCommentRemarksFormat = True Then
                    If Remarks.Length > 0 Then Comments += " " & Remarks.Replace("'", "''")
                End If

                If flgCommentEngineNo = True Then
                    If GridExRow.Item(GrdEnum.Engine_No).Value.ToString.Length > 0 Then Comments += " Engin No: " & GridExRow.Item(GrdEnum.Engine_No).Value.ToString & ","
                End If
            End If

            Comments += " " & GridExRow.Item(GrdEnum.Comments).ToString
            If Comments.Trim.Length > 0 Then
                Dim str As String = Comments.Substring(Comments.LastIndexOf(","))
                If str.Length > 1 Then
                    Comments = Comments
                Else
                    Comments = Comments.Substring(0, Comments.LastIndexOf(",") - 1)
                End If
            End If
            Return Comments
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class