Imports SBDal
Imports SBModel
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class OpeningBalanceDAL
    Public Function AddOpeningBalance(ByVal OpeningBalance As OpeningBalance)
        Try
            Dim str As String = String.Empty
            Dim Con As New SqlConnection(SQLHelper.CON_STR)
            If Not Con.State = ConnectionState.Open Then Con.Open()
            Dim trans As SqlTransaction = Con.BeginTransaction
            str = "INSERT INTO tblVoucher(Location_Id, Voucher_code, Voucher_no, Voucher_date, Voucher_type_id, coa_detail_id, Reference, UserName) Values(" & OpeningBalance.LocationId & ", N'" & OpeningBalance.voucher_code & "', N'" & OpeningBalance.voucher_no & "', N'" & OpeningBalance.voucher_date & "', " & OpeningBalance.voucher_type & ", " & OpeningBalance.detail_Id & ", N'" & OpeningBalance.Reference & "', N'" & OpeningBalance.UserName & "')Select @@Identity"
            OpeningBalance.Voucher_Id = SQLHelper.ExecuteScaler(trans, CommandType.Text, str, Nothing)
            AddOpeningBalanceDt(trans, OpeningBalance, OpeningBalance.Voucher_Id)
            trans.Commit()
            Con.Close()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AddOpeningBalanceDt(ByVal Trans As SqlTransaction, ByVal OpeningBalance As OpeningBalance, ByVal MasterVoucherId As Integer)
        Try
            Dim str As String = String.Empty
            Dim OpeningBalanceDt As List(Of OpeningBalanceDt) = OpeningBalance.OpeningBalanceDt
            For Each OpeningBalDt As OpeningBalanceDt In OpeningBalanceDt
                str = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_id, coa_detail_id, Comments, Debit_Amount, Credit_Amount) " _
                & " Values(" & MasterVoucherId & ", " & OpeningBalDt.location_id & ", " & OpeningBalDt.coa_detail_id & ", 'Opening Balance', " & OpeningBalDt.debit_amount & ", " & OpeningBalDt.credit_amount & " )"
                SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, str, Nothing)
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetOpeningBalanceById(ByVal AccountId As Integer) As DataTable
        Try
            Dim str As String = String.Empty
            Dim dt As New DataTable
            Dim adp As SqlDataAdapter
            Dim OpeningBalance As New OpeningBalance
            str = "Select a.Voucher_id From tblVoucher a INNER JOIN tblVoucherDetail b ON a.Voucher_id = b.Voucher_id WHERE b.coa_detail_id=" & AccountId & " AND Voucher_code='Opening'"
            adp = New SqlDataAdapter(str, SQLHelper.CON_STR)
            adp.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
