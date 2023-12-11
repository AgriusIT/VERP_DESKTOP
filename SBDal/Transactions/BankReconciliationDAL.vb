Imports SBModel
Imports System
Imports System.Data.SqlClient
Public Class BankReconciliationDAL
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Update voucher detail coulmns for cheque status
    ''' </summary>
    ''' <param name="vlist"></param>
    ''' <param name="Statement"></param>
    ''' <returns></returns>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Function UpdateChequeStatus(ByVal vlist As List(Of VouchersDetail), ByVal Statement As BankReconciliationBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim str As String = String.Empty
            For Each voucher As VouchersDetail In vlist
                str = "Update tblVoucherDetail set Cheque_Status=" & IIf(voucher.Cheque_Status = True, 1, 0) & ", Cheque_Clearance_Date=" & IIf(voucher.Cheque_Clearance_Date = Date.MinValue, "Null", "'" & voucher.Cheque_Clearance_Date.ToString("yyyy-M-d hh:mm:ss tt") & "'") & " , StatementId = " & "ident_current('tblBankStatement')" & " Where Voucher_detail_Id = " & voucher.VoucherDetailId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Save Statement data
    ''' </summary>
    ''' <param name="Statement"></param>
    ''' <returns></returns>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Function Save(ByVal Statement As BankReconciliationBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Dim str As String = ""
        Try
            str = "Insert Into tblBankStatement (BankId,StatementDate,StatementTitle,EndingBalance,ReconciledBalance,StatementStatus) Values(" & Statement.BankId & ",'" & Statement.StatementDate & "',N'" & Statement.StatementTitle & "','" & Statement.EndingBalance & "','" & Statement.ReconciledBalance & "','0')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Update Statement status
    ''' </summary>
    ''' <param name="Statement"></param>
    ''' <returns></returns>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Function Update(ByVal Statement As BankReconciliationBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Dim str As String = ""
        Try
            str = "Update tblBankStatement Set StatementStatus = '1' Where StatementId = " & Statement.StatementId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Delete Statement
    ''' </summary>
    ''' <param name="vlist"></param>
    ''' <param name="Statement"></param>
    ''' <returns></returns>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Function Delete(ByVal vlist As List(Of VouchersDetail), ByVal Statement As BankReconciliationBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Dim str As String = ""
        Try
            str = "Delete From tblBankStatement Where StatementId = " & Statement.StatementId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
            For Each voucher As VouchersDetail In vlist
                str = "Update tblVoucherDetail set Cheque_Status=NULL, Cheque_Clearance_Date=NULL, StatementId = NULL Where Voucher_detail_Id = " & voucher.VoucherDetailId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        Finally
            con.Close()
        End Try
    End Function
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Update voucher details to reset if records are deleted
    ''' </summary>
    ''' <param name="vlist"></param>
    ''' <param name="Statement"></param>
    ''' <returns></returns>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Function UpdateVDetail(ByVal vlist As List(Of VouchersDetail), ByVal Statement As BankReconciliationBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Dim str As String = ""
        Try
            For Each voucher As VouchersDetail In vlist
                str = "Update tblVoucherDetail set Cheque_Status=NULL, Cheque_Clearance_Date=NULL, StatementId = NULL Where Voucher_detail_Id = " & voucher.VoucherDetailId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str, Nothing)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        Finally
            con.Close()
        End Try
    End Function
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Status validation
    ''' </summary>
    ''' <param name="BankId"></param>
    ''' <returns></returns>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Function ValidateStatus(ByVal BankId As Integer) As Boolean
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "Select StatementId from tblBankStatement Where BankId = " & BankId & " And StatementStatus = 0 "
            dt = UtilityDAL.GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
