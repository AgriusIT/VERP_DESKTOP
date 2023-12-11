Imports SBModel
Imports System
Imports System.Data.SqlClient
Public Class InvestmentBookingDAL
    Dim costCenterId As Integer
    Function Add(ByVal objModel As InvestmentBookingBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, trans)
            AddVoucher(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Add(ByVal objModel As InvestmentBookingBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  InvestmentBooking (PropertyProfileId, BookingDate, VoucherNo, InvestorId, InvestmentAmount, Remarks, ProfitPercentage, InvestmentRequired) values (" & objModel.PropertyProfileId & ", '" & objModel.BookingDate.ToString("yyyy-MM-dd hh:mm:ss") & "', N'" & objModel.VoucherNo.Replace("'", "''") & "', " & objModel.InvestorId & ", " & objModel.InvestmentAmount & ", N'" & objModel.Remarks.Replace("'", "''") & "', " & objModel.ProfitPercentage & ", " & objModel.InvestmentRequired & ") Select @@Identity"
            objModel.InvestmentBookingId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.VoucherNo
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function AddVoucher(ByVal objModel As InvestmentBookingBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,1,N'" & objModel.VoucherNo & "',N'" & objModel.BookingDate.ToString("yyyy-MM-dd hh:mm:ss") & "',1,N'frmProInvestmentBooking',N'" & objModel.VoucherNo & "',N' Serial Number: " & objModel.PropertyProfile & " Investor: " & objModel.Investor & " Remarks: " & objModel.Remarks & "') Select @@Identity"
            objModel.InvestmentBookingId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            AddVoucherDetail(objModel, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function AddVoucherDetail(ByVal objModel As InvestmentBookingBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            'Debit Salary Expense Account
            strSQL = " select CostCenterId  from PropertyProfile where PropertyProfileId = " & objModel.PropertyProfileId & ""
            costCenterId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            'strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & objModel.InvestmentBookingId & ",1," & objModel.InvestorId & "," & objModel.InvestmentAmount & ",0," & objModel.PropertyProfileId & ",N' Serial Number: " & objModel.PropertyProfile & " Investor: " & objModel.Investor & " Remarks: " & objModel.Remarks & "'," & objModel.InvestmentAmount & ",0,1,1,1,1)"
            strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & objModel.InvestmentBookingId & ",1," & objModel.InvestorId & "," & objModel.InvestmentAmount & ",0," & costCenterId & ",N' Serial Number: " & objModel.PropertyProfile & " Investor: " & objModel.Investor & " PlotNo: " & objModel.PLotNo & " Remarks: " & objModel.Remarks & "'," & objModel.InvestmentAmount & ",0,1,1,1,1)"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'Credit Investor Account
            strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & objModel.InvestmentBookingId & ",1," & objModel.InvestmentAccountId & ",0," & objModel.InvestmentAmount & "," & costCenterId & ",N' Serial Number: " & objModel.PropertyProfile & " Investor: " & objModel.Investor & " PlotNo: " & objModel.PLotNo & " Remarks: " & objModel.Remarks & "',0," & objModel.InvestmentAmount & ",1,1,1,1)"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As InvestmentBookingBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            DeleteVoucher(objModel, trans)
            AddVoucher(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As InvestmentBookingBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update InvestmentBooking set PropertyProfileId= " & objModel.PropertyProfileId & ", BookingDate= '" & objModel.BookingDate.ToString("yyyy-MM-dd hh:mm:ss") & "', VoucherNo= N'" & objModel.VoucherNo.Replace("'", "''") & "', InvestorId= " & objModel.InvestorId & ", InvestmentAmount= " & objModel.InvestmentAmount & ", Remarks= N'" & objModel.Remarks.Replace("'", "''") & "', ProfitPercentage = " & objModel.ProfitPercentage & " , InvestmentRequired = " & objModel.InvestmentRequired & " WHERE InvestmentBookingId=" & objModel.InvestmentBookingId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.VoucherNo
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As InvestmentBookingBE, Optional ByVal RowCount As Integer = 0) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            DeleteProfit(objModel, trans, RowCount)
            Delete(objModel, trans)
            DeleteVoucher(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal objModel As InvestmentBookingBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from InvestmentBooking  where InvestmentBookingId= " & objModel.InvestmentBookingId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function DeleteVoucher(ByVal objModel As InvestmentBookingBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            ''Below lines are commented on 06-11-2018
            'strSQL = "DELETE FROM tblVoucherDetail WHERE voucher_id = " & objModel.VoucherId
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'strSQL = "DELETE FROM tblVoucher WHERE voucher_id = " & objModel.VoucherId
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "DELETE FROM tblVoucherDetail WHERE voucher_id IN ( SELECT voucher_id FROM tblVoucher WHERE voucher_no = '" & objModel.VoucherNo & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "DELETE FROM tblVoucher WHERE voucher_no = '" & objModel.VoucherNo & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' TASK TFS4897 Done on 07-11-2018
    ''' </summary>
    ''' <param name="objModel"></param>
    ''' <param name="trans"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteProfit(ByVal objModel As InvestmentBookingBE, trans As SqlTransaction, ByVal RowCount As Integer) As Boolean
        Dim strSQL As String = String.Empty
        Try
            ''Below lines are commented on 06-11-2018
            'strSQL = "DELETE FROM tblVoucherDetail WHERE voucher_id = " & objModel.VoucherId
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'strSQL = "DELETE FROM tblVoucher WHERE voucher_id = " & objModel.VoucherId
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            If RowCount > 1 Then
                strSQL = "UPDATE tblVoucherDetail SET debit_amount -= ISNULL(ProfitSharingDetail.NetProfitAmount, 0),  Currency_debit_amount -= ISNULL(ProfitSharingDetail.NetProfitAmount, 0) FROM tblVoucherDetail VD INNER JOIN ProfitSharingDetail ON ProfitSharingDetail.ProfitExpenseAccountId =  VD.coa_detail_id  WHERE ProfitSharingDetail.InvestmentBookingId = " & objModel.InvestmentBookingId & " And VD.voucher_id  IN (SELECT voucher_id FROM tblVoucher WHERE voucher_no IN (SELECT DISTINCT ProfitSharingMaster.voucher_no FROM ProfitSharingDetail INNER JOIN ProfitSharingMaster ON ProfitSharingDetail.ProfitSharingId = ProfitSharingMaster.ProfitSharingId  WHERE  ISNULL(ProfitSharingDetail.InvestmentBookingId, 0) = " & objModel.InvestmentBookingId & ")) AND ISNULL(VD.debit_amount, 0) > 0"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                'strSQL = "UPDATE tblVoucherDetail SET debit_amount -= ISNULL(InvestmentBooking.InvestmentAmount, 0),  Currency_debit_amount -= ISNULL(InvestmentBooking.InvestmentAmount, 0) FROM tblVoucherDetail VD INNER JOIN ProfitSharingDetail ON ProfitSharingDetail.InvestmentAccountId =  VD.coa_detail_id INNER JOIN InvestmentBooking ON ProfitSharingDetail.InvestmentBookingId = InvestmentBooking.InvestmentBookingId  WHERE ProfitSharingDetail.InvestmentBookingId = " & objModel.InvestmentBookingId & " And VD.voucher_id  IN (SELECT voucher_id FROM tblVoucher WHERE voucher_no IN (SELECT DISTINCT ProfitSharingMaster.voucher_no FROM ProfitSharingDetail INNER JOIN ProfitSharingMaster ON ProfitSharingDetail.ProfitSharingId = ProfitSharingMaster.ProfitSharingId  WHERE  ISNULL(ProfitSharingDetail.InvestmentBookingId, 0) = " & objModel.InvestmentBookingId & ")) AND ISNULL(VD.debit_amount, 0) > 0"
                strSQL = "UPDATE tblVoucherDetail SET debit_amount -= ISNULL(InvestmentBooking.InvestmentAmount, 0),  Currency_debit_amount -= ISNULL(InvestmentBooking.InvestmentAmount, 0) FROM tblVoucherDetail VD INNER JOIN ProfitSharingDetail ON ProfitSharingDetail.InvestmentAccountId =  VD.coa_detail_id INNER JOIN InvestmentBooking ON ProfitSharingDetail.InvestmentBookingId = InvestmentBooking.InvestmentBookingId  WHERE InvestmentBooking.InvestmentBookingId = " & objModel.InvestmentBookingId & " And VD.voucher_id  IN (SELECT voucher_id FROM tblVoucher WHERE voucher_no IN (SELECT DISTINCT ProfitSharingMaster.voucher_no FROM ProfitSharingDetail INNER JOIN ProfitSharingMaster ON ProfitSharingDetail.ProfitSharingId = ProfitSharingMaster.ProfitSharingId  WHERE  ISNULL(ProfitSharingDetail.InvestmentBookingId, 0) = " & objModel.InvestmentBookingId & ")) AND ISNULL(VD.debit_amount, 0) > 0"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                strSQL = "DELETE FROM tblVoucherDetail WHERE voucher_id IN (SELECT voucher_id FROM tblVoucher WHERE voucher_no IN (SELECT DISTINCT ProfitSharingMaster.voucher_no FROM ProfitSharingDetail INNER JOIN ProfitSharingMaster ON ProfitSharingDetail.ProfitSharingId = ProfitSharingMaster.ProfitSharingId  WHERE  ISNULL(ProfitSharingDetail.InvestmentBookingId, 0) = " & objModel.InvestmentBookingId & ")) AND coa_detail_id IN (SELECT InvestorId FROM ProfitSharingDetail WHERE  ISNULL(ProfitSharingDetail.InvestmentBookingId, 0) = " & objModel.InvestmentBookingId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                'strSQL = "DELETE FROM tblVoucher WHERE voucher_no IN (SELECT VoucherNo FROM ProfitSharingDetail WHERE  ISNULL(InvestmentBookingId, 0) = " & objModel.InvestmentBookingId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                strSQL = "DELETE FROM ProfitSharingDetail WHERE ISNULL(InvestmentBookingId, 0) = " & objModel.InvestmentBookingId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ElseIf RowCount = 1 Then
                strSQL = "DELETE FROM tblVoucherDetail WHERE voucher_id IN (SELECT voucher_id FROM tblVoucher WHERE voucher_no IN (SELECT DISTINCT ProfitSharingMaster.voucher_no FROM ProfitSharingDetail INNER JOIN ProfitSharingMaster ON ProfitSharingDetail.ProfitSharingId = ProfitSharingMaster.ProfitSharingId  WHERE  ISNULL(ProfitSharingDetail.InvestmentBookingId, 0) = " & objModel.InvestmentBookingId & ")) "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                strSQL = "DELETE FROM tblVoucher WHERE ISNULL(voucher_no, '') IN (SELECT DISTINCT ProfitSharingMaster.voucher_no FROM ProfitSharingDetail INNER JOIN ProfitSharingMaster ON ProfitSharingDetail.ProfitSharingId = ProfitSharingMaster.ProfitSharingId  WHERE  ISNULL(ProfitSharingDetail.InvestmentBookingId, 0) = " & objModel.InvestmentBookingId & " AND ISNULL(voucher_no, '') <> '')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                strSQL = "DELETE FROM ProfitSharingMaster WHERE ProfitSharingId IN (SELECT DISTINCT ProfitSharingMaster.ProfitSharingId FROM ProfitSharingDetail INNER JOIN ProfitSharingMaster ON ProfitSharingDetail.ProfitSharingId = ProfitSharingMaster.ProfitSharingId  WHERE  ISNULL(ProfitSharingDetail.InvestmentBookingId, 0) = " & objModel.InvestmentBookingId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                strSQL = "DELETE FROM ProfitSharingDetail WHERE ISNULL(InvestmentBookingId, 0) = " & objModel.InvestmentBookingId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select InvestmentBookingId, PropertyProfileId, BookingDate, VoucherNo, InvestorId, ProfitPercentage, InvestmentAmount, InvestmentRequired, Remarks from InvestmentBooking  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select InvestmentBookingId, PropertyProfileId, BookingDate, VoucherNo, InvestorId, ProfitPercentage, InvestmentAmount, InvestmentRequired,  Remarks from InvestmentBooking  where InvestmentBookingId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class