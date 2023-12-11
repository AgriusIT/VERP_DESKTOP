''14-Mar-2014 TASK2490 Imran Ali New Bug In Release 2.1.1.6
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data.SqlClient
Public Class ChequeSerialDAL


    Public Function Add(ByVal ChequeSerial As ChequeSerialBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO ChequeMasterTable(BankAcId, BranchName, Cheque_No_From, Cheque_No_To, Status, UserName, EntryDate) VALUES (" & ChequeSerial.BankAcId & ", N'" & ChequeSerial.BranchName.Replace("'", "''") & "', N'" & ChequeSerial.Cheque_No_From.Replace("'", "''") & "', N'" & ChequeSerial.Cheque_No_To.Replace("'", "''") & "', " & IIf(ChequeSerial.Status = True, 1, 0) & ", N'" & ChequeSerial.UserName.Replace("'", "''") & "', N'" & ChequeSerial.EntryDate.ToString("yyyy-M-d hh:mm:ss tt") & "')Select @@Identity "
            ChequeSerial.ChequeSerialId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Call AddDetail(ChequeSerial, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal ChequeSerial As ChequeSerialBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = String.Empty
            strSQL = "Select Count(*) From ChequeDetailTable WHERE ChequeSErialId=" & ChequeSerial.ChequeSerialId & " AND VoucherDetailId <> 0 "
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL, trans)
            If dt Is Nothing Then Throw New Exception("Some data is not provided")
            If Not Val(dt.Rows(0).Item(0)) > 0 Then

                strSQL = "UPDATE ChequeMasterTable SET BankAcId=" & ChequeSerial.BankAcId & ", BranchName=N'" & ChequeSerial.BranchName.Replace("'", "''") & "', Cheque_No_From=N'" & ChequeSerial.Cheque_No_From.Replace("'", "''") & "', Cheque_No_To=N'" & ChequeSerial.Cheque_No_To.Replace("'", "''") & "', Status=" & IIf(ChequeSerial.Status = True, 1, 0) & ", UserName=N'" & ChequeSerial.UserName.Replace("'", "''") & "', EntryDate=N'" & ChequeSerial.EntryDate.ToString("yyyy-M-d hh:mm:ss tt") & "' WHERE ChequeSerialId=" & ChequeSerial.ChequeSerialId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


                strSQL = "Delete From ChequeDetailTable WHERE ChequeSerialId=" & ChequeSerial.ChequeSerialId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                Call AddDetail(ChequeSerial, trans)

                trans.Commit()
                Return True
            Else
                Throw New Exception("You can not delete record because it dependant record")
                Return False
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal ChequeSerial As ChequeSerialBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = String.Empty
            strSQL = "Select Count(*) From ChequeDetailTable WHERE ChequeSErialId=" & ChequeSerial.ChequeSerialId & " AND VoucherDetailId <> 0"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL, trans)
            If dt Is Nothing Then Throw New Exception("Some data is not provided")
            If Not Val(dt.Rows(0).Item(0)) > 0 Then


                strSQL = "Delete From ChequeDetailTable WHERE ChequeSerialId=" & ChequeSerial.ChequeSerialId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                strSQL = "Delete From ChequeMasterTable WHERE ChequeSerialId=" & ChequeSerial.ChequeSerialId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                trans.Commit()
                Return True
            Else
                Throw New Exception("You can not delete record because it dependant record")
                Return False
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddDetail(ByVal ChequeSerial As ChequeSerialBE, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each ChequeBook As ChequeBookDetailBE In ChequeSerial.ChequeBookDetail
                strSQL = String.Empty
                strSQL = "INSERT INTO ChequeDetailTable(ChequeSerialId, ChequeNo, Cheque_Issued,VoucherDetailId)" _
                & " VALUES(" & ChequeSerial.ChequeSerialId & ", N'" & ChequeBook.ChequeNo.Replace("'", "''") & "', " & IIf(ChequeBook.Cheque_Issued = True, 1, 0) & ", NULL)"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAllRecords() As DataTable
        Try
            Dim str As String = String.Empty
            'Before against atsk:2490 
            'str = "SELECT     dbo.ChequeMasterTable.ChequeSerialId,dbo.ChequeMasterTable.BankAcId, dbo.tblCOAMainSubSubDetail.detail_title, dbo.ChequeMasterTable.BranchName, dbo.ChequeMasterTable.Cheque_No_From, " _
            '& " dbo.ChequeMasterTable.Cheque_No_To, dbo.ChequeMasterTable.Status, dbo.ChequeMasterTable.UserName, dbo.ChequeMasterTable.EntryDate " _
            '& " FROM         dbo.ChequeMasterTable LEFT OUTER JOIN " _
            '& " dbo.tblCOAMainSubSubDetail ON dbo.ChequeMasterTable.BankAcId = dbo.tblCOAMainSubSubDetail.coa_detail_id "
            ''14-Mar-2014 TASK2490 Imran Ali New Bug In Release 2.1.1.6
            str = "SELECT     dbo.ChequeMasterTable.ChequeSerialId,dbo.ChequeMasterTable.BankAcId, dbo.tblCOAMainSubSubDetail.detail_title, dbo.ChequeMasterTable.BranchName, dbo.ChequeMasterTable.Cheque_No_From, " _
           & " dbo.ChequeMasterTable.Cheque_No_To, dbo.ChequeMasterTable.Status " _
           & " FROM         dbo.ChequeMasterTable LEFT OUTER JOIN " _
           & " dbo.tblCOAMainSubSubDetail ON dbo.ChequeMasterTable.BankAcId = dbo.tblCOAMainSubSubDetail.coa_detail_id "
            'End Task:2490
            Return UtilityDAL.GetDataTable(str)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
  
End Class
