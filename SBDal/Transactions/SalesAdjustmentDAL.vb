'021-Nov-2017 TFS1828 : Ali Faisal : Add save,update and delete functions to save update and remove data of Sales Adjustment
Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class SalesAdjustmentDAL
    ''' <summary>
    ''' Ali Faisal : Save the Data of Master and Detail in Sales Adjustment tables.
    ''' </summary>
    ''' <param name="Adjustment"></param>
    ''' <returns></returns>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Function Save(ByVal Adjustment As SalesAdjustmentBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert Master records
            str = "INSERT INTO SalesAdjustmentMaster (DocNo, DocDate, CompanyId, CostCenterId, CustomerId, Remarks) VALUES(N'" & Adjustment.DocNo & "',N'" & Adjustment.DocDate.ToString("yyyy-MM-dd hh:mm:ss") & "'," & Adjustment.CompanyId & "," & Adjustment.CostCenterId & "," & Adjustment.CustomerId & ",N'" & Adjustment.Remarks & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Detail records
            For Each obj As SalesAdjustmentDetailBE In Adjustment.Detail
                str = "INSERT INTO SalesAdjustmentDetail (AdjustmentId, InvoiceId, ItemId, Amount, Reason) VALUES(" & "ident_current('SalesAdjustmentMaster')" & "," & obj.InvoiceId & "," & obj.ItemId & "," & obj.Amount & ",N'" & obj.Reason & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next

            'Insert Master Vouchers
            str = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) " _
                & " Values(1,1,7,N'" & Adjustment.DocNo & "',N'" & Adjustment.DocDate.ToString("yyyy-MM-dd hh:mm:ss") & "',1,N'frmSalesAdjustmentVoucher',N'" & Adjustment.DocNo & "',N'" & Adjustment.Remarks & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            For Each obj As SalesAdjustmentDetailBE In Adjustment.Detail

                If obj.Amount < 0 Then

                    obj.Amount *= (-1)

                    'Task 3154 If User enter -ve amonut on Sales adj voucher then voucher should be reverse then customer will be debit and sales account will be credit'

                    'Insert Details Debit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(ident_current('tblVoucher'),1," & Adjustment.CustomerId & "," & obj.Amount & ",0," & Adjustment.CostCenterId & ",N'" & obj.Reason & "'," & obj.Amount & ",0,1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    'Insert Details Credit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(ident_current('tblVoucher'),1," & obj.ItemAccountId & ",0," & obj.Amount & "," & Adjustment.CostCenterId & ",N'" & obj.Reason & "',0," & obj.Amount & ",1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                Else

                    'Insert Details Debit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(ident_current('tblVoucher'),1," & obj.ItemAccountId & "," & obj.Amount & ",0," & Adjustment.CostCenterId & ",N'" & obj.Reason & "'," & obj.Amount & ",0,1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    'Insert Details Credit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(ident_current('tblVoucher'),1," & Adjustment.CustomerId & ",0," & obj.Amount & "," & Adjustment.CostCenterId & ",N'" & obj.Reason & "',0," & obj.Amount & ",1,1,1,1)"
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
    ''' <summary>
    ''' Ali Faisal : Update Master and Details records and also Check if detail records exists already then update else insert.
    ''' </summary>
    ''' <param name="Adjustment"></param>
    ''' <returns></returns>
    ''' <remarks>21-Nov-2017 TFS1828 : Ali Faisal</remarks>
    Public Function Update(ByVal Adjustment As SalesAdjustmentBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update Master records
            str = "UPDATE SalesAdjustmentMaster SET DocNo=N'" & Adjustment.DocNo & "' , DocDate=N'" & Adjustment.DocDate.ToString("yyyy-MM-dd hh:mm:ss") & "' , CompanyId = " & Adjustment.CompanyId & " , CostCenterId = " & Adjustment.CostCenterId & " , CustomerId=" & Adjustment.CustomerId & ", Remarks=N'" & Adjustment.Remarks & "' WHERE AdjustmentId= " & Adjustment.AdjustmentId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Update/Insert Detail records
            For Each obj As SalesAdjustmentDetailBE In Adjustment.Detail
                str = "IF EXISTS(SELECT AdjustmentDetailId FROM SalesAdjustmentDetail WHERE AdjustmentDetailId=" & obj.AdjustmentDetailId & ")  UPDATE SalesAdjustmentDetail SET AdjustmentId =" & Adjustment.AdjustmentId & ", InvoiceId = " & obj.InvoiceId & ", ItemId = " & obj.ItemId & ", Amount = " & obj.Amount & ", Reason = N'" & obj.Reason & "' WHERE AdjustmentDetailId = " & obj.AdjustmentDetailId & "" _
                   & " ELSE INSERT INTO SalesAdjustmentDetail (AdjustmentId, InvoiceId, ItemId, Amount, Reason) VALUES(" & Adjustment.AdjustmentId & "," & obj.InvoiceId & "," & obj.ItemId & "," & obj.Amount & ",N'" & obj.Reason & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next

            'Update Master Voucher
            str = "Update tblVoucher Set location_id = 1, finiancial_year_id = 1, voucher_type_id = 7, voucher_no = N'" & Adjustment.DocNo & "', voucher_date =N'" & Adjustment.DocDate.ToString("yyyy-MM-dd hh:mm:ss") & "', post = 1, Source = N'frmSalesAdjustmentVoucher', voucher_code = N'" & Adjustment.DocNo & "', Remarks = N'" & Adjustment.Remarks & "' Where voucher_Id = " & Adjustment.VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'Delete Detail Voucher
            str = "Delete from tblVoucherDetail Where voucher_id=" & Adjustment.VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            For Each obj As SalesAdjustmentDetailBE In Adjustment.Detail

                If obj.Amount < 0 Then

                    obj.Amount *= (-1)

                    'Insert Details Debit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(" & Adjustment.VoucherId & ",1," & Adjustment.CustomerId & "," & obj.Amount & ",0," & Adjustment.CostCenterId & ",N'" & obj.Reason & "'," & obj.Amount & ",0,1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    'Insert Details Credit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(" & Adjustment.VoucherId & ",1," & obj.ItemAccountId & ",0," & obj.Amount & "," & Adjustment.CostCenterId & ",N'" & obj.Reason & "',0," & obj.Amount & ",1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                Else

                    'Insert Details Debit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(" & Adjustment.VoucherId & ",1," & obj.ItemAccountId & "," & obj.Amount & ",0," & Adjustment.CostCenterId & ",N'" & obj.Reason & "'," & obj.Amount & ",0,1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    'Insert Details Credit
                    str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) " _
                        & " Values(" & Adjustment.VoucherId & ",1," & Adjustment.CustomerId & ",0," & obj.Amount & "," & Adjustment.CostCenterId & ",N'" & obj.Reason & "',0," & obj.Amount & ",1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                End If
            Next
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
    Public Function Delete(ByVal Id As Integer, ByVal VoucherId As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete Detail records
            str = "Delete from SalesAdjustmentDetail Where AdjustmentId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Master records
            str = "Delete from SalesAdjustmentMaster Where AdjustmentId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'Delete Detail Voucher
            str = "Delete from tblVoucherDetail Where voucher_id=" & VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Master Voucher
            str = "Delete from tblVoucher Where voucher_id= " & VoucherId & ""
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
            str = "Delete from SalesAdjustmentDetail Where AdjustmentDetailId = " & Id & ""
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
