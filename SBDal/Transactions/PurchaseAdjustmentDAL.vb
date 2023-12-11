'21-Nov-2017 TFS1828 : Ali Faisal : Add save,update and delete functions to save update and remove data of Purchase Adjustment
Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class PurchaseAdjustmentDAL
    ''' <summary>
    ''' Ali Faisal : Save the Data of Master and Detail in Purchase Adjustment tables.
    ''' </summary>
    ''' <param name="Adjustment"></param>
    ''' <returns></returns>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Function Save(ByVal Adjustment As PurchaseAdjustmentBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert Master records
            str = "INSERT INTO PurchaseAdjustmentMaster (DocNo, DocDate, CompanyId, CostCenterId, CustomerId, Remarks) VALUES(N'" & Adjustment.DocNo & "',N'" & Adjustment.DocDate.ToString("yyyy-MM-dd hh:mm:ss") & "'," & Adjustment.CompanyId & "," & Adjustment.CostCenterId & "," & Adjustment.CustomerId & ",N'" & Adjustment.Remarks & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Detail records
            ''Start TFS4860 : Added Column LocationId
            For Each obj As PurchaseAdjustmentDetailBE In Adjustment.Detail
                str = "INSERT INTO PurchaseAdjustmentDetail (AdjustmentId, InvoiceId, ItemId,LocationId, Amount, Reason) VALUES(" & "ident_current('PurchaseAdjustmentMaster')" & "," & obj.InvoiceId & "," & obj.ItemId & "," & obj.LocationId & "," & obj.Amount & ",N'" & obj.Reason & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next

            'Insert Master Vouchers
            str = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) " _
                & " Values(1,1,7,N'" & Adjustment.DocNo & "',N'" & Adjustment.DocDate.ToString("yyyy-MM-dd hh:mm:ss") & "',1,N'frmPurchaseAdjustmentVoucher',N'" & Adjustment.DocNo & "',N'" & Adjustment.Remarks & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            For Each obj As PurchaseAdjustmentDetailBE In Adjustment.Detail

                'Task 3153 If User enter -ve amonut on Purchase adj voucher then voucher should be reverse then vendor will be credit and purchase account will be debit'

                If obj.Amount < 0 Then
                    Dim Amount As Double = 0
                    Amount = obj.Amount * (-1)
                    'obj.Amount *= (-1)
                    'Insert Details Debit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(ident_current('tblVoucher'),1," & obj.ItemAccountId & "," & Amount & ",0," & Adjustment.CostCenterId & ",N'" & obj.Reason & "'," & Amount & ",0,1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    'Insert Details Credit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(ident_current('tblVoucher'),1," & Adjustment.CustomerId & ",0," & Amount & "," & Adjustment.CostCenterId & ",N'" & obj.Reason & "',0," & Amount & ",1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    'Insert Details Debit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(ident_current('tblVoucher'),1," & Adjustment.CustomerId & "," & obj.Amount & ",0," & Adjustment.CostCenterId & ",N'" & obj.Reason & "'," & obj.Amount & ",0,1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    'Insert Details Credit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(ident_current('tblVoucher'),1," & obj.ItemAccountId & ",0," & obj.Amount & "," & Adjustment.CostCenterId & ",N'" & obj.Reason & "',0," & obj.Amount & ",1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            'Insert Master Stock 
            str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks, Project,Account_Id) " _
                   & " Values(N'" & Adjustment.DocNo & "', N'" & Adjustment.DocDate.ToString("yyyy-MM-dd hh:mm:ss") & "', " & 1 & ", N'" & Adjustment.Remarks.Replace("'", "''") & "', " & Adjustment.CostCenterId & "," & Adjustment.CustomerId & ") Select @@Identity "
            Dim StockTransId As Integer
            StockTransId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            For Each obj As PurchaseAdjustmentDetailBE In Adjustment.Detail
                ''Insert Stock Out
                Dim BalanceQty As Double = GetItemBalanceQty(obj.ItemId, StockTransId, trans)
                Dim BalanceAmount As Double = GetItemBalanceAmount(obj.ItemId, StockTransId, trans)
                Dim ItemRate As Double = GetItemRate(obj.ItemId, StockTransId, trans)

                If obj.Amount < 0 Then
                    Dim Amount As Double = 0
                    Amount = obj.Amount * (-1)
                    'obj.Amount *= (-1)
                    str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks) " _
                      & " Values (" & StockTransId & ", " & obj.LocationId & ",  " & obj.ItemId & ", " & 0 & ", " & BalanceQty & ", " & ItemRate & ", " & 0 & ", " & ItemRate * BalanceQty & ", N'" & obj.Reason.Replace("'", "''") & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    ''Insert Stock In
                    str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks) " _
                         & " Values (" & StockTransId & ", " & obj.LocationId & ",  " & obj.ItemId & ", " & BalanceQty & ", " & 0 & ", " & (BalanceAmount + Amount) / BalanceQty & ", " & ((BalanceAmount + Amount) / BalanceQty) * BalanceQty & ", " & 0 & ", N'" & obj.Reason.Replace("'", "''") & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks) " _
                      & " Values (" & StockTransId & ", " & obj.LocationId & ",  " & obj.ItemId & ", " & 0 & ", " & BalanceQty & ", " & ItemRate & ", " & 0 & ", " & ItemRate * BalanceQty & ", N'" & obj.Reason.Replace("'", "''") & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    ''Insert Stock In
                    str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks) " _
                         & " Values (" & StockTransId & ", " & obj.LocationId & ",  " & obj.ItemId & ", " & BalanceQty & ", " & 0 & ", " & (BalanceAmount - obj.Amount) / BalanceQty & ", " & ((BalanceAmount - obj.Amount) / BalanceQty) * BalanceQty & ", " & 0 & ", N'" & obj.Reason.Replace("'", "''") & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    'Start TFS4860 : Ayesha Rehman : 23-10-2018 : This Function is made to calculate the Item Rate
    Public Function GetItemRate(ByVal ArticleDefId As Integer, ByVal StockTransId As Integer, ByVal trans As SqlTransaction) As Double
        Try


            Dim strData As String = "Select ArticleDefID,IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0)  AS BalanceQty,SUM((IsNull(Rate,0)*IsNull(InQty,0))-(IsNull(Rate,0)*IsNull(OutQty,0))) AS BalanceAmount From StockDetailTable WHERE ArticleDefID=" & ArticleDefId & " And StockTransId <> " & StockTransId & " Group By ArticleDefId "
            Dim dblPrice As Double = 0D
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strData, trans)
            dt.AcceptChanges()

            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(1).ToString) > 0 Then
                    If Val(dt.Rows(0).Item(2).ToString) > 0 Then
                        Return Val(dt.Rows(0).Item(2).ToString) / Val(dt.Rows(0).Item(1).ToString)
                    Else
                        Return 0
                    End If
                Else
                    If Val(dt.Rows(0).Item(2).ToString) > 0 Then
                        Return Val(dt.Rows(0).Item(2).ToString) / 1
                    Else
                        Return 0
                    End If
                End If
            End If
            Return dblPrice
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

    Public Function GetItemBalanceQty(ByVal ArticleDefId As Integer, ByVal StockTransId As Integer, ByVal trans As SqlTransaction) As Double
        Try
            Dim strData As String = "Select ArticleDefID,IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0)  AS BalanceQty,SUM((IsNull(Rate,0)*IsNull(InQty,0))-(IsNull(Rate,0)*IsNull(OutQty,0))) AS BalanceAmount From StockDetailTable WHERE ArticleDefID=" & ArticleDefId & " And StockTransId <> " & StockTransId & " Group By ArticleDefId "
            Dim dblBalanceQty As Double = 1D
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strData, trans)
            dt.AcceptChanges()

            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(1).ToString) > 0 Then
                    Return Val(dt.Rows(0).Item(1).ToString)
                Else
                    Return 1
                End If
            End If
            Return dblBalanceQty
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function GetItemBalanceAmount(ByVal ArticleDefId As Integer, ByVal StockTransId As Integer, ByVal trans As SqlTransaction) As Double
        Try
            Dim strData As String = "Select ArticleDefID,IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0)  AS BalanceQty,SUM((IsNull(Rate,0)*IsNull(InQty,0))-(IsNull(Rate,0)*IsNull(OutQty,0))) AS BalanceAmount From StockDetailTable WHERE ArticleDefID=" & ArticleDefId & " And StockTransId <> " & StockTransId & " Group By ArticleDefId "
            Dim dblBalanceAmount As Double = 0D
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strData, trans)
            dt.AcceptChanges()

            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(2).ToString) > 0 Then
                    Return Val(dt.Rows(0).Item(2).ToString)
                Else
                    Return 0
                End If
            End If
            Return dblBalanceAmount
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

    ''End TFS4234
    ''' <summary>
    ''' Ali Faisal : Update Master and Details records and also Check if detail records exists already then update else insert.
    ''' </summary>
    ''' <param name="Adjustment"></param>
    ''' <returns></returns>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Function Update(ByVal Adjustment As PurchaseAdjustmentBE) As Boolean

        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update Master records
            str = "UPDATE PurchaseAdjustmentMaster SET DocNo=N'" & Adjustment.DocNo & "' , DocDate=N'" & Adjustment.DocDate.ToString("yyyy-MM-dd hh:mm:ss") & "' , CompanyId = " & Adjustment.CompanyId & " , CostCenterId = " & Adjustment.CostCenterId & " , CustomerId=" & Adjustment.CustomerId & ", Remarks=N'" & Adjustment.Remarks & "' WHERE AdjustmentId= " & Adjustment.AdjustmentId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Update/Insert Detail records
            ''Start TFS4860 : Added Column LocationId
            For Each obj As PurchaseAdjustmentDetailBE In Adjustment.Detail
                str = "IF EXISTS(SELECT AdjustmentDetailId FROM PurchaseAdjustmentDetail WHERE AdjustmentDetailId=" & obj.AdjustmentDetailId & ")  UPDATE PurchaseAdjustmentDetail SET AdjustmentId =" & Adjustment.AdjustmentId & ", InvoiceId = " & obj.InvoiceId & ", ItemId = " & obj.ItemId & ", LocationId = " & obj.LocationId & ", Amount = " & obj.Amount & ", Reason = N'" & obj.Reason & "' WHERE AdjustmentDetailId = " & obj.AdjustmentDetailId & "" _
                   & " ELSE INSERT INTO PurchaseAdjustmentDetail (AdjustmentId, InvoiceId, ItemId,LocationId, Amount, Reason) VALUES(" & Adjustment.AdjustmentId & "," & obj.InvoiceId & "," & obj.ItemId & "," & obj.LocationId & "," & obj.Amount & ",N'" & obj.Reason & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next

            'Update Master Voucher
            str = "Update tblVoucher Set location_id = 1, finiancial_year_id = 1, voucher_type_id = 7, voucher_no = N'" & Adjustment.DocNo & "', voucher_date =N'" & Adjustment.DocDate.ToString("yyyy-MM-dd hh:mm:ss") & "', post = 1, Source = N'frmPurchaseAdjustmentVoucher', voucher_code = N'" & Adjustment.DocNo & "', Remarks = N'" & Adjustment.Remarks & "' Where voucher_Id = " & Adjustment.VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'Delete Detail Voucher
            str = "Delete from tblVoucherDetail Where voucher_id=" & Adjustment.VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            For Each obj As PurchaseAdjustmentDetailBE In Adjustment.Detail

                'Task 3153 If User enter -ve amonut on Purchase adj voucher then voucher should be reverse then vendor will be credit and purchase account will be debit'

                If obj.Amount < 0 Then
                    Dim Amount As Double = 0
                    Amount = obj.Amount * (-1)
                    'obj.Amount *= (-1)

                    'Insert Details Debit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(" & Adjustment.VoucherId & ",1," & obj.ItemAccountId & "," & Amount & ",0," & Adjustment.CostCenterId & ",N'" & obj.Reason & "'," & Amount & ",0,1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    'Insert Details Credit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(" & Adjustment.VoucherId & ",1," & Adjustment.CustomerId & ",0," & Amount & "," & Adjustment.CostCenterId & ",N'" & obj.Reason & "',0," & Amount & ",1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                Else

                    'Insert Details Debit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(" & Adjustment.VoucherId & ",1," & Adjustment.CustomerId & "," & obj.Amount & ",0," & Adjustment.CostCenterId & ",N'" & obj.Reason & "'," & obj.Amount & ",0,1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    'Insert Details Credit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(" & Adjustment.VoucherId & ",1," & obj.ItemAccountId & ",0," & obj.Amount & "," & Adjustment.CostCenterId & ",N'" & obj.Reason & "',0," & obj.Amount & ",1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                End If
            Next
            ''Start TFS4860

            str = "IF EXISTS(SELECT StockTransId FROM StockMasterTable WHERE DocNo ='" & Adjustment.DocNo & "')  Update StockMasterTable Set DocNo=N'" & Adjustment.DocNo & "', DocDate=N'" & Adjustment.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', DocType= " & 1 & ", Remarks=N'" & Adjustment.Remarks.Replace("'", "''") & "', Project=" & Adjustment.CostCenterId & ",Account_Id=" & Adjustment.CustomerId & " WHERE DocNo = '" & Adjustment.DocNo & "'" _
                 & " ELSE Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks, Project,Account_Id) " _
                   & " Values(N'" & Adjustment.DocNo & "', N'" & Adjustment.DocDate.ToString("yyyy-MM-dd hh:mm:ss") & "', " & 1 & ", N'" & Adjustment.Remarks.Replace("'", "''") & "', " & Adjustment.CostCenterId & "," & Adjustment.CustomerId & ") "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Previouce Data from Stock Detail
            str = "Delete From StockDetailTable WHERE StockTransId in (Select StockTransId from StockMasterTable WHERE DocNo ='" & Adjustment.DocNo & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = " Select StockTransId from StockMasterTable WHERE DocNo = '" & Adjustment.DocNo & "'"
            Dim StockTransId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            For Each obj As PurchaseAdjustmentDetailBE In Adjustment.Detail
                ''Insert Stock Out
                Dim BalanceQty As Double = GetItemBalanceQty(obj.ItemId, StockTransId, trans)
                Dim BalanceAmount As Double = GetItemBalanceAmount(obj.ItemId, StockTransId, trans)
                Dim ItemRate As Double = GetItemRate(obj.ItemId, StockTransId, trans)

                If obj.Amount < 0 Then
                    Dim Amount As Double = 0
                    Amount = obj.Amount * (-1)
                    'obj.Amount *= (-1)
                    str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks) " _
                      & " Values (" & StockTransId & ", " & obj.LocationId & ",  " & obj.ItemId & ", " & 0 & ", " & BalanceQty & ", " & ItemRate & ", " & 0 & ", " & ItemRate * BalanceQty & ", N'" & obj.Reason.Replace("'", "''") & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    ''Insert Stock In
                    str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks) " _
                         & " Values (" & StockTransId & ", " & obj.LocationId & ",  " & obj.ItemId & ", " & BalanceQty & ", " & 0 & ", " & (BalanceAmount + Amount) / BalanceQty & ", " & ((BalanceAmount + Amount) / BalanceQty) * BalanceQty & ", " & 0 & ", N'" & obj.Reason.Replace("'", "''") & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks) " _
                      & " Values (" & StockTransId & ", " & obj.LocationId & ",  " & obj.ItemId & ", " & 0 & ", " & BalanceQty & ", " & ItemRate & ", " & 0 & ", " & ItemRate * BalanceQty & ", N'" & obj.Reason.Replace("'", "''") & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    ''Insert Stock In
                    str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks) " _
                         & " Values (" & StockTransId & ", " & obj.LocationId & ",  " & obj.ItemId & ", " & BalanceQty & ", " & 0 & ", " & (BalanceAmount - obj.Amount) / BalanceQty & ", " & ((BalanceAmount - obj.Amount) / BalanceQty) * BalanceQty & ", " & 0 & ", N'" & obj.Reason.Replace("'", "''") & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            ''End TFS4860
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Delete the Detail and Master records.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Function Delete(ByVal Id As Integer, ByVal VoucherId As Integer, ByVal DocNo As String) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete Detail records
            str = "Delete from PurchaseAdjustmentDetail Where AdjustmentId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Master records
            str = "Delete from PurchaseAdjustmentMaster Where AdjustmentId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'Delete Detail Voucher
            str = "Delete from tblVoucherDetail Where voucher_id=" & VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Master Voucher
            str = "Delete from tblVoucher Where voucher_id= " & VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ''Delete Record from Stock
            str = "Delete From StockDetailTable WHERE StockTransId in (Select StockTransId from StockMasterTable WHERE DocNo ='" & DocNo & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = "Delete from StockMasterTable WHERE DocNo ='" & DocNo & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Delete the detail record.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Sub DeleteDetail(ByVal Id As Integer)
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete Detail records
            str = "Delete from PurchaseAdjustmentDetail Where AdjustmentDetailId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub
End Class
