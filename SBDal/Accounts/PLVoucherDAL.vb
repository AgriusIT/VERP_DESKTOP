Imports SBDal
Imports SBModel
Imports System.Data.SqlClient
Public Class PLVoucherDAL
    Public Function Add(ByVal PLVoucher As PLVoucher) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim str As String = "INSERT INTO tblVoucher (location_id, voucher_code, voucher_type_id, voucher_no,voucher_date,post,source,UserName) " _
            & " Values(1, N'" & PLVoucher.voucher_code & "', " & Val(PLVoucher.voucher_type_id) & ",  N'" & PLVoucher.voucher_no & "', N'" & PLVoucher.voucher_date.ToString("yyyy-M-d hh:mm:ss tt") & "', " & IIf(PLVoucher.post = True, 1, 0) & ", 'frmVoucher', N'" & PLVoucher.UserName & "' ) SELECT @@Identity"
            PLVoucher.voucher_id = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            AddDetail(PLVoucher, PLVoucher.voucher_id, trans)
            AddRef(PLVoucher, trans)
            trans.Commit()
            Return True
        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal PLVoucher As PLVoucher) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = "UPDATE tblVoucher set location_id=1, " _
                                & " voucher_code = N'" & PLVoucher.voucher_code & "',  " _
                                & " voucher_type_id = " & Val(PLVoucher.voucher_type_id) & ", " _
                                & " voucher_no = N'" & PLVoucher.voucher_no & "', " _
                                & " voucher_date = N'" & PLVoucher.voucher_date.ToString("yyyy-M-d hh:mm:ss tt") & "' , " _
                                & " post = " & IIf(PLVoucher.post = True, 1, 0) & ", " _
                                & " source = 'frmVoucher', " _
                                & " UserName =N'" & PLVoucher.UserName & "' " _
                                & " WHERE Voucher_Id=" & PLVoucher.voucher_id & "   "
            SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            str = String.Empty
            str = "Update ConfigValuesTable SET Config_Value=N'" & PLVoucher.RefDate.Date & "' WHERE Config_Type='EndOfDate'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = String.Empty
            str = "Delete From tblVoucherDetail WHERE Voucher_Id=" & PLVoucher.voucher_id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = String.Empty
            str = "Delete From tblFinancialYearCloseStatus WHERE YearCloseId =" & PLVoucher.YearCloseId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            AddDetail(PLVoucher, PLVoucher.voucher_id, trans)
            AddRef(PLVoucher, trans)
            trans.Commit()
            Return True
        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddDetail(ByVal PLVoucher As PLVoucher, ByVal Voucher_id As Integer, ByVal trans As SqlTransaction) As Boolean
        Try


            Dim PLAccountId As Integer = Convert.ToInt16(UtilityDAL.GetConfigValue("PLAccountId").ToString)
            Dim str As String = String.Empty
            For Each PLVoucherList As PLVoucherDetail In PLVoucher.PLVoucherDetail
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount) " _
                & " Values(" & Voucher_id & ", 1, " & PLVoucherList.coa_detail_id & ", N'" & "Ref By :" & PLVoucher.voucher_code & "" & "', " & Val(PLVoucherList.debit_amount) & ", " & Val(PLVoucherList.credit_amount) & ") "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next

            'PL Account Debit Here .... 
            If Val(PLVoucher.PLDebitAmount) < 0 Then
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount) " _
                                & " Values(" & Voucher_id & ", 1, " & PLAccountId & ", N'" & "Ref By :" & PLVoucher.voucher_code & "" & "', " & Val(Math.Abs(PLVoucher.PLDebitAmount)) & ", " & Val(0) & ") "
            Else
                str = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount) " _
                                & " Values(" & Voucher_id & ", 1, " & PLAccountId & ", N'" & "Ref By :" & PLVoucher.voucher_code & "" & "', " & Val(0) & ", " & Val(Math.Abs(PLVoucher.PLDebitAmount)) & ") "
            End If
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function UpdateClosingDate(ByVal ClosingDate As DateTime) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = "UPDATE ConfigValuesTable SET config_Value=N'" & ClosingDate.Date.ToString("yyyy-M-d h:mm:ss tt") & "' WHERE config_Type='EndOfDate'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddRef(ByVal PLVoucher As PLVoucher, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim str As String = "INSERT INTO tblFinancialYearCloseStatus(RefDate, RefVoucher_No) Values(N'" & PLVoucher.RefDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & PLVoucher.voucher_no & "')Select @@Identity"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function GetAllRecords(ByVal startDate As DateTime, ByVal EndDate As DateTime) As DataTable
        Try
            Dim str As String = "SP_Financial_Year_Closing N'" & startDate.ToString("yyyy-M-d 00:00:00") & "', N'" & EndDate.ToString("yyyy-M-d 23:59:59") & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetMasterRecord() As DataTable
        Try
            Dim str As String = "SELECT a.YearCloseId, b.Voucher_id, a.RefDate as [Start Date], b.voucher_date as [Closing Date], a.RefVoucher_No, ISNULL(b.post,0) as Post,  CASE WHEN ISNULL(b.Post,0)=0 THEN 'UnPosted' ELSE 'Posted' end as Status FROM  dbo.tblFinancialYearCloseStatus a INNER JOIN  dbo.tblVoucher b ON a.RefVoucher_No = b.voucher_no ORDER BY a.YearCloseId DESC"
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Delete(ByVal PLVoucher As PLVoucher) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Delete From tblVoucherDetail WHERE Voucher_Id=" & PLVoucher.voucher_id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = String.Empty
            str = "Delete From tblVoucher WHERE Voucher_Id =" & PLVoucher.voucher_id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = String.Empty
            str = "Delete From tblFinancialYearCloseStatus WHERE YearCloseId =" & PLVoucher.YearCloseId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = String.Empty
            str = "Update ConfigValuesTable SET Config_Value=N'" & PLVoucher.RefDate.Date.ToString("yyyy-M-d h:mm:ss tt") & "' WHERE Config_Type='EndOfDate'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

End Class
