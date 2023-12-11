''26-June-2014 TASK2704 Imran Ali Add new functionality cash request in erp.
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System
Imports System.Data
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports SBUtility.Utility
Public Class CashrequestDAL
    Public Function Add(ByVal CashReq As CashRequestHeadBE, Optional ByRef requestId As Integer = 0) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            CashReq.RequestNo = GetDocumentNo(CashReq.RequestNo.ToString.Substring(0, CashReq.RequestNo.ToString.LastIndexOf("-") + 1), trans)
            Dim strSQL As String = String.Empty
            strSQL = " INSERT INTO CashRequestHead(RequestNo, RequestDate, Remarks,Approved,UserId,ApprovedUserId,ModifiUserId,EntryDate,ApprovedDate,ModifiDate,CMFADocId,ProjectId,EmpID,Total_Amount ) " _
                   & " VALUES(N'" & CashReq.RequestNo.ToString.Replace("'", "''") & "', Convert(datetime, N'" & CashReq.RequestDate & "',102), N'" & CashReq.Remarks.Replace("'", "''") & "', " & IIf(CashReq.Approved = True, 1, 0) & ", " & CashReq.UserId & ", " & IIf(CashReq.Approved = True, CashReq.ApprovedUserId, "NULL") & ", NULL,Convert(datetime,GetDate(),102)," & IIf(CashReq.Approved = True, "Convert(datetime, N'" & CashReq.ApprovedDate & "',102)", "NULL") & ",NULL, " & CashReq.CMFADocId & ", " & CashReq.ProjectId & ", " & CashReq.EmpID & ", " & CashReq.Total_Amount & ")Select @@Identity"
            CashReq.RequestId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            requestId = CashReq.RequestId

            AddList(CashReq, trans)
            ''TASK TFS4439
            SaveDocument(CashReq.RequestId, CashReq.Source, CashReq.AttachmentPath, CashReq.ArrFile, CashReq.EntryDate, trans)
            ''END TASK 4439
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function AddList(ByVal CashReq As CashRequestHeadBE, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            If CashReq.CashRequstDetail IsNot Nothing Then
                If CashReq.CashRequstDetail.Count > 0 Then
                    For Each CashReqDt As CashRequestDetailBE In CashReq.CashRequstDetail
                        strSQL = String.Empty
                        strSQL = "INSERT INTO CashRequestDetail(RequestID,coa_detail_id,Amount,Paid_Amount,Comments, CostCenterId, EmployeeID) " _
                                 & " VALUES(" & CashReq.RequestId & ", " & CashReqDt.coa_detail_id & ", " & CashReqDt.Amount & ",NULL, N'" & CashReqDt.Comments.Replace("'", "''") & "', " & CashReqDt.CostCenterId & ", " & CashReqDt.EmployeeId & ")"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    Next
                End If
            End If
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function Modify(ByVal CashReq As CashRequestHeadBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "UPDATE CashRequestHead  SET RequestNo=N'" & CashReq.RequestNo.ToString.Replace("'", "''") & "', RequestDate=Convert(datetime, N'" & CashReq.RequestDate & "',102), Remarks=N'" & CashReq.Remarks.Replace("'", "''") & "',Approved=" & IIf(CashReq.Approved = True, 1, 0) & ",UserId=" & CashReq.UserId & ",ApprovedUserId=" & IIf(CashReq.Approved = True, CashReq.ApprovedUserId, "NULL") & ",ModifiUserId=" & CashReq.ModifyUserId & ",ApprovedDate=" & IIf(CashReq.Approved = True, "Convert(datetime, N'" & CashReq.ApprovedDate & "',102)", "NULL") & ",ModifiDate=Convert(datetime,getDate(),102),CMFADocId= " & CashReq.CMFADocId & ",ProjectId=" & CashReq.ProjectId & ",EmpID=" & CashReq.EmpID & ",Total_Amount= " & CashReq.Total_Amount & " WHERE RequestID=" & CashReq.RequestId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "Delete From CashRequestDetail  WHERE RequestID=" & CashReq.RequestId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            AddList(CashReq, trans)
            ''TASK TFS4439
            SaveDocument(CashReq.RequestId, CashReq.Source, CashReq.AttachmentPath, CashReq.ArrFile, CashReq.EntryDate, trans)
            ''END TASK 4439
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Delete(ByVal CashReq As CashRequestHeadBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Select Count(*) From tblVoucher WHERE CashRequestID=" & CashReq.RequestId & ""
            Dim intRequestId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            If intRequestId > 0 Then
                Throw New Exception("you cannot delete this record, because it dependent exist")
                Return False
            End If

            strSQL = "Delete From CashRequestDetail  WHERE RequestID=" & CashReq.RequestId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "Delete From CashRequestHead  WHERE RequestID=" & CashReq.RequestId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            AddList(CashReq, trans)
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    ''' <summary>
    ''' This Fucntion is made to save Attachments with document
    ''' </summary>
    ''' <param name="DocId"></param>
    ''' <param name="Source"></param>
    ''' <param name="objPath"></param>
    ''' <param name="arrFile"></param>
    ''' <param name="Date1"></param>
    ''' <param name="objTrans"></param>
    ''' <returns></returns>
    ''' <remarks>TFS4439 :  Ayesha Rehman : 07-09-2018</remarks>
    Public Function SaveDocument(ByVal DocId As Integer, ByVal Source As String, objPath As String, ByVal arrFile As List(Of String), ByVal Date1 As DateTime, ByVal objTrans As SqlTransaction) As Boolean
        Dim cmd As New SqlCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        Try

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            dt.AcceptChanges()


            Dim objdt As New DataTable
            objdt = UtilityDAL.GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            Dim intId As Integer = objdt.Rows(0)(0)

            Dim strSQL As String = String.Empty
            cmd.CommandText = String.Empty
            strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            'Dim objPath As String = getConfigValueByType("FileAttachmentPath").ToString

            If Not arrFile Is Nothing AndAlso arrFile.Count > 0 Then ''TFS1772
                'Altered Against Task#2015060001 Ali Ansari
                'Marked Against Task#2015060001 Ali Ansari
                '            If arrFile.Length > 0 Then
                'Marked Against Task#2015060001 Ali Ansari
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If
                        'Dim New_Files As String = intId & "_" & DocId & "_SI" & CompanyId & "_" & Date1.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
                        Dim New_Files As String = intId & "_" & DocId & "_" & Date1.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)

                        Dim dr As DataRow
                        dr = dt.NewRow
                        dr(0) = DocId
                        dr(1) = Source
                        dr(2) = objPath & "\" & New_Files
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                        If IO.File.Exists(objPath & "\" & New_Files) Then
                            IO.File.Delete(objPath & "\" & New_Files)
                        End If
                        IO.File.Copy(objFile, objPath & "\" & New_Files)
                        intId += 1
                    End If
                Next
            End If


            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        Dim strPath As String = objPath
                        Dim strFileName As String = r.Item("FileNames").ToString.Substring(r.Item("FileNames").ToString.LastIndexOf("\") + 1)
                        cmd.CommandText = String.Empty
                        strSQL = "INSERT INTO DocumentAttachment(DocId, Source, FileName, Path) Values(" & Val(r("DocId").ToString) & ",N'" & r.Item("Source").ToString.Replace("'", "''") & "', N'" & strFileName.Replace("'", "''") & "', N'" & strPath.Replace("'", "''") & "')"
                        cmd.CommandText = strSQL
                        cmd.ExecuteNonQuery()
                    Next
                End If
            End If


        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function GetDocumentNo(ByVal Prefix As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            Dim Serial As Integer = 0I
            Dim SerialNo As String = String.Empty
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select ISNULL(Max(Right(RequestNo,6)),0)+1 as Cont From CashRequestHead WHERE LEFT(RequestNo," & Prefix.Length & ")=N'" & Prefix & "'", trans)
            If dt IsNot Nothing Then
                Serial = Val(dt.Rows(0).Item(0).ToString)
            Else
                Serial = 1
            End If
            Dim strSerial As String = "000000" & CStr(Serial)
            SerialNo = Prefix & Microsoft.VisualBasic.Right(strSerial, 6)
            Return SerialNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateStatus(ByVal RequestId As Integer, Optional ByVal trans As OleDb.OleDbTransaction = Nothing)

        Try

            Dim cmd As New OleDb.OleDbCommand
            cmd.Connection = trans.Connection
            cmd.Transaction = trans

            Dim strSQL As String = String.Empty
            strSQL = "UPDATE CashRequestDetail " _
            & " SET CashRequestDetail.Paid_Amount = a.Paid_Amount " _
            & " FROM (SELECT dbo.tblVoucher.CashRequestID, dbo.tblVoucherDetail.coa_detail_id, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0)) AS Paid_Amount " _
            & " FROM  dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id WHERE (dbo.tblVoucher.CashRequestID <> 0) " _
            & " AND dbo.tblVoucher.CashRequestID=" & RequestId & " " _
            & " GROUP BY dbo.tblVoucher.CashRequestID, dbo.tblVoucherDetail.coa_detail_id) AS a WHERE a.CashRequestId = CashRequestDetail.RequestID And a.coa_detail_id = CashRequestDetail.coa_detail_id AND CashRequestDetail.RequestID=" & RequestId & ""
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            strSQL = String.Empty
            strSQL = "Update CashRequestHead SET Status=Case When Balance > 0 Then '1' ELSE '0' End From(SELECT     RequestID, SUM(ISNULL(Amount, 0) - ISNULL(Paid_Amount, 0)) AS Balance FROM dbo.CashRequestDetail WHERE CashRequestDetail.RequestId=" & RequestId & "  GROUP BY RequestID) a WHERE a.RequestId = CashRequestHead.RequestId AND CashRequestHead.RequestId=" & RequestId & ""
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

    Public Function DeleteUpdateStatus(ByVal VoucherId As Integer, ByVal RequestId As Integer, Optional ByVal trans As OleDb.OleDbTransaction = Nothing)

        Try
            Dim cmd As New OleDb.OleDbCommand
            cmd.Connection = trans.Connection
            cmd.Transaction = trans


            Dim strSQL As String = String.Empty
            strSQL = "UPDATE CashRequestDetail " _
            & " SET CashRequestDetail.Paid_Amount = CashRequestDetail.Paid_Amount- a.Paid_Amount " _
            & " FROM (SELECT dbo.tblVoucher.CashRequestID, dbo.tblVoucherDetail.coa_detail_id, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0)) AS Paid_Amount " _
            & " FROM  dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id WHERE (dbo.tblVoucher.CashRequestID <> 0) " _
            & " AND dbo.tblVoucher.CashRequestID=" & RequestId & " AND dbo.tblVoucher.Voucher_Id=" & VoucherId & "" _
            & " GROUP BY dbo.tblVoucher.CashRequestID, dbo.tblVoucherDetail.coa_detail_id) AS a WHERE a.CashRequestId = CashRequestDetail.RequestID And a.coa_detail_id = CashRequestDetail.coa_detail_id AND CashRequestDetail.RequestID=" & RequestId & ""
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            strSQL = String.Empty
            strSQL = "Update CashRequestHead SET Status=Case When Balance > 0 Then '1' ELSE '0' End From(SELECT     RequestID, SUM(ISNULL(Amount, 0) - ISNULL(Paid_Amount, 0)) AS Balance FROM dbo.CashRequestDetail WHERE CashRequestDetail.RequestId=" & RequestId & "  GROUP BY RequestID) a WHERE a.RequestId = CashRequestHead.RequestId AND CashRequestHead.RequestId=" & RequestId & ""
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

End Class
