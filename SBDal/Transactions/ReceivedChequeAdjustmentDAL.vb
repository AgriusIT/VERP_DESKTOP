Imports SBDal
Imports SBModel
Imports SBUtility
Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class ReceivedChequeAdjustmentDAL

    Public Function Save(ByVal objMod As ReceivedChequeAdjustmentBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try


            objMod.DocNo = GetDocNo(objMod.Prefix, objMod.Length, trans)
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO ReceivedChequeAdjustmentTable(DocNo,DocDate,coa_detail_id,cheque_voucher_id,cheque_voucher_detail_id,cheque_voucher_no,cheque_no,cheque_date,Adjustment_Amount,Adjsted_Voucher_Id,User_Name) " _
            & " VALUES(N'" & objMod.DocNo.Replace("'", "''") & "', Convert(DateTime,N'" & objMod.DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & objMod.coa_detail_id & ", " & objMod.cheque_voucher_id & "," & objMod.cheque_voucher_detail_id & ", N'" & objMod.cheque_voucher_no.Replace("'", "''") & "', N'" & objMod.cheque_no.Replace("'", "''") & "', Convert(DateTime,N'" & objMod.cheque_date.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & objMod.Adjustment_Amount & ", " & objMod.Adjusted_voucher_id & ", N'" & objMod.User_Name.Replace("'", "''") & "') Select @@Identity"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Commit()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Update(ByVal objMod As ReceivedChequeAdjustmentBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction

        Try

            Dim strSQL As String = String.Empty
            strSQL = "UPDATE ReceivedChequeAdjustmentTable SET DocNo=N'" & objMod.DocNo.Replace("'", "''") & "',DocDate= Convert(DateTime,N'" & objMod.DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),coa_detail_id=" & objMod.coa_detail_id & ",cheque_voucher_id=" & objMod.cheque_voucher_id & ",cheque_voucher_detail_id=" & objMod.cheque_voucher_detail_id & ",cheque_voucher_no=N'" & objMod.cheque_voucher_no.Replace("'", "''") & "',cheque_no=N'" & objMod.cheque_no.Replace("'", "''") & "',cheque_date=Convert(DateTime,N'" & objMod.cheque_date.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Adjustment_Amount=" & objMod.Adjustment_Amount & ",Adjsted_Voucher_Id= " & objMod.Adjusted_voucher_id & ",User_Name=N'" & objMod.User_Name.Replace("'", "''") & "' WHERE PK_ID=" & objMod.PK_ID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Commit()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Delete(ByVal objMod As ReceivedChequeAdjustmentBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "DELETE FROM ReceivedChequeAdjustmentTable WHERE PK_ID=" & objMod.PK_ID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Commit()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAllRecords(Optional ByVal Condition As String = "") As DataTable
        Try
            Dim dt As New DataTable
            Dim strSQL As String = "SELECT R_Cheq.PK_ID, R_Cheq.DocNo, R_Cheq.DocDate, R_Cheq.coa_detail_id, COA.detail_code AS [Account Code], COA.detail_title AS [Account Title], " _
                     & " R_Cheq.cheque_voucher_id, R_Cheq.cheque_voucher_detail_id, R_Cheq.cheque_voucher_no AS [Cheque Voucher], V.voucher_date AS [Cheque Voucher Date],  " _
                     & " R_Cheq.cheque_no AS [Cheque No], R_Cheq.cheque_date AS [Cheque Date], R_Cheq.Adjustment_Amount, R_Cheq.Adjsted_Voucher_Id,  " _
                     & " A_V.voucher_no AS [Adjusted Voucher No], A_V.voucher_date AS [Adjusted Voucher Date], R_Cheq.User_Name " _
                     & " FROM dbo.tblVoucher AS V INNER JOIN " _
                     & " dbo.tblVoucherDetail AS V_D INNER JOIN " _
                     & " dbo.ReceivedChequeAdjustmentTable AS R_Cheq INNER JOIN " _
                     & " dbo.vwCOADetail AS COA ON R_Cheq.coa_detail_id = COA.coa_detail_id ON V_D.voucher_detail_id = R_Cheq.cheque_voucher_detail_id ON  " _
                     & " V.voucher_id = R_Cheq.cheque_voucher_id LEFT OUTER JOIN " _
                     & " dbo.tblVoucher AS A_V ON R_Cheq.Adjsted_Voucher_Id = A_V.voucher_id " _
                     & " ORDER BY R_Cheq.PK_ID DESC "
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetDocNo(ByVal Prefix As String, ByVal Length As Integer, Optional ByVal trans As SqlTransaction = Nothing) As String

        Try

            Dim Serial As Integer = 0I
            Dim strSerialNo As String = String.Empty

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select (IsNull(Max(RIGHT(DocNo," & Length & ")),0)+1) as Cont From ReceivedChequeAdjustmentTable WHERE LEFT(DocNo," & Prefix.Length & ")=N'" & Prefix & "'", trans)
            dt.AcceptChanges()

            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    Serial = Val(dt.Rows(0).Item(0).ToString)
                Else
                    Serial = 1
                End If
            Else
                Serial = 1
            End If
            Dim strSerial As String = String.Empty

            For i As Integer = 0 To Length
                If i = 0 Then
                    strSerial = "0"
                Else
                    strSerial += "0"
                End If
            Next


            strSerialNo = Prefix & Right(strSerial + CStr(Serial), Length)

            Return strSerialNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
