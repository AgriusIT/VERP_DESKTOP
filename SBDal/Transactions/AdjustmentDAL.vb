Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class AdjustmentDAL
    Private AdjNo As String = String.Empty
    Public Function Add(ByVal Adjustment As AdjustmentVoucher) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            AdjNo = GetDocumentNo(trans)
            Dim str As String = String.Empty
            str = "INSERT INTO tblAdjustmentDetail(AdjNo, AdjDate, Customercode,MarketReturns, MarketReturnsAcId, Remarks, User_Name, EntryDate) " _
            & " VALUES(N'" & AdjNo & "', N'" & Adjustment.AdjDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & Adjustment.CustomerCode & "," & Adjustment.MarketReturns & ", " & Adjustment.MarketReturnAcId & ", N'" & Adjustment.Remarks & "', N'" & Adjustment.UserName & "', N'" & Adjustment.EntryDate.ToString("yyyy-M-d h:mm:ss tt") & "' )Select @@Identity"
            Adjustment.AdjId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            SaveVoucher(Adjustment, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
            AdjNo = String.Empty
        End Try
    End Function
    Public Function Update(ByVal Adjustment As AdjustmentVoucher) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "UPDATE tblAdjustmentDetail SET AdjNo=N'" & Adjustment.AdjNo & "', AdjDate=N'" & Adjustment.AdjDate.ToString("yyyy-M-d h:mm:ss tt") & "'," _
                & " Customercode=" & Adjustment.CustomerCode & ", " _
                & " MarketReturns=" & Adjustment.MarketReturns & ", " _
                & " MarketReturnsAcId=" & Adjustment.MarketReturnAcId & ", " _
                & " Remarks=N'" & Adjustment.Remarks & "', " _
                & " User_Name=N'" & Adjustment.UserName & "',  " _
                & " EntryDate=N'" & Adjustment.EntryDate.ToString("yyyy-M-d h:mm:ss tt") & "' WHERE AdjId=" & Adjustment.AdjId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            UpdateVoucher(Adjustment, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
            AdjNo = String.Empty
        End Try
    End Function
    Public Function Delete(ByVal Adjustment As AdjustmentVoucher) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            AdjNo = Adjustment.AdjNo
            Dim str As String = String.Empty
            str = "Delete from tblAdjustmentDetail  WHERE AdjId=" & Adjustment.AdjId
            Adjustment.AdjId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            str = "Delete From tblVoucherDetail WHERE Voucher_Id=" & Val(CheckVoucherId(AdjNo, trans))
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = "Delete From tblVoucher WHERE Voucher_Id=" & Val(CheckVoucherId(AdjNo, trans))
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
            AdjNo = String.Empty
        End Try
    End Function
    Public Function GetAll(Optional ByVal Condition As String = "") As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select " & IIf(Condition = "All", "", "TOP 50") & " a.AdjId, a.AdjNo, a.AdjDate, a.CustomerCode, b.detail_code, b.detail_title, a.Remarks, ISNULL(a.MarketReturns,0) as MarketReturns From tblAdjustmentDetail a INNER JOIN vwCOADetail b on a.CustomerCode = b.coa_detail_Id ORDER BY 1 Desc"
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveVoucher(ByVal adj As AdjustmentVoucher, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim str As String = String.Empty
            str = "INSERT INTO tblVoucher(Location_Id, vucher_type_id, voucher_No,voucher_date,Post, source, Reference, UserName) " _
            & " VALUES(1,1,N'" & AdjNo & "', N'" & adj.AdjDate & "', 1,N'" & adj.source & "', N'" & adj.Remarks & "', N'" & adj.UserName & "') SELECT @@Identity"
            Dim obj As Object = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            SaveVoucherDetail(obj, adj, trans)
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function UpdateVoucher(ByVal adj As AdjustmentVoucher, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = String.Empty
            Dim VId As Integer = 0
            AdjNo = adj.AdjNo
            VId = Val(CheckVoucherId(AdjNo, trans))
            str = "Update tblVoucher SET Location_Id=1," _
                 & " voucher_type_id=1, " _
                 & " voucher_No=N'" & adj.AdjNo & "', " _
                 & " voucher_date=N'" & adj.AdjDate & "', " _
                 & " Post=1, " _
                 & " source=N'" & adj.source & "', " _
                 & " Reference=N'" & adj.Remarks & "', " _
                 & " UserName=N'" & adj.UserName & "' WHERE Voucher_Id=" & VId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = String.Empty
            str = "Delete From tblVoucherDetail WHERE Voucher_Id=" & VId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            SaveVoucherDetail(VId, adj, trans)
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function SaveVoucherDetail(ByVal Obj As Object, ByVal adj As AdjustmentVoucher, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim str As String = String.Empty
            str = "INSERT INTO tblVoucherDetail(Voucher_Id,Location_Id, coa_detail_id, Comments, debit_amount, credit_amount) " _
            & " Values(" & Val(Obj) & ", 0, " & adj.MarketReturnAcId & ", 'Market Returns Ref By: " & adj.CustomerName & "', 0, " & adj.MarketReturns & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "INSERT INTO tblVoucherDetail(Voucher_Id,Location_Id, coa_detail_id, Comments, debit_amount, credit_amount) " _
                       & " Values(" & Val(Obj) & ", 0, " & adj.CustomerCode & ", 'Market Returns Ref By: " & AdjNo & "', " & adj.MarketReturns & ", 0 )"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function CheckVoucherId(ByVal AdjNo As String, ByVal sqltrans As SqlTransaction) As String
        Try
            Dim str As String = String.Empty
            str = "Select Voucher_Id From tblVoucher WHERE Voucher_No=N'" & AdjNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str, sqltrans)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0)
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetDocumentNo(Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            Return UtilityDAL.GetSerialNo("MR-" & Right(Date.Now.Year, 2) & "-", "tblAdjustmentDetail", "AdjNo", trans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
