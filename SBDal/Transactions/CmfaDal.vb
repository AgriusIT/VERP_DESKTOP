''19-June-2014 TASK:2697 IMRAN ALI Optional Note  Entry On CMFA Document(Ravi)
''20-June-2014 TASK:2701 IMRAN ALI Expense Entry on CMFA Document(Ravi)
''25-June-2014 TASK:2703 IMRAN ALI Enhancement In CMFA (RAVI)
''07-Jul-2014 TASK:2723 IMRAN ALI Add Column Comments In CMFA Detail (Ravi)
''11-Jul-2014 Task:2734 IMRAN ALI Ehancement CMFA Document
''13-Aug-2014 Task:2783 Imran Ali  Frequently CMFA on Home Page (Ravi)
''03-Sep-2014 Task:2824 Imran Ali Apply all rights of admin on CMFA Document
''27-Sep-2014 Task:2856 Imran Ali CMFA Detail Report
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility

Public Class CmfaDal
    Public Function Save(ByVal ObjCmf As CmfaBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            ObjCmf.docNo = UtilityDAL.GetNextDocNo("CMFA-" & ObjCmf.DocDate.ToString("yyMM") & "", 6, "CMFAMasterTable", "DocNo", trans)
            Dim strSQL As String = String.Empty
            'Before against task:2703
            ''19-June-2014 TASK:2697 Optional Entry Expected Completion Job e.g
            'strSQL = "INSERT INTO CMFAMasterTable(DocNo,DocDate,LocationId,CustomerCode,ProjectId,POId,Remarks,InvoiceNo,TotalQty,TotalAmount,ExptJobCompDate,ExptPaymentFromClient,JobStartingTime,TentiveInvoiceDate,VerificationPeriodAfterCompletionJob,Status,UserName,EntryDate,ApprovedUserId, ApprovedBudget, TaxPercent,WHTaxPercent,Approved, OPEX_Sale_Percent) " _
            '         & " VALUES(N'" & ObjCmf.docNo.Replace("'", "''") & "', Convert(DateTime, N'" & ObjCmf.DocDate & "', 102), " & ObjCmf.locationId & ", " & ObjCmf.CustomerCode & ", " & ObjCmf.ProjectId & ", " & ObjCmf.POId & ", N'" & ObjCmf.Remarks.Replace("'", "''") & "', N'" & ObjCmf.PONo.Replace("'", "''") & "', " & ObjCmf.TotalQty & "," & ObjCmf.TotalAmount & ", " & IIf(ObjCmf.ExptJobCompDate = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.ExptJobCompDate & "',102)") & ", N'" & ObjCmf.ExptPaymentFromClient.Replace("'", "''") & "', " & IIf(ObjCmf.JobStartingTime = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.JobStartingTime & "', 102)") & ", " & IIf(ObjCmf.TentiveInvoiceDate = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.TentiveInvoiceDate & "',102)") & ", " & IIf(ObjCmf.VerificationPeriodAfterCompletionJob = Nothing, "NULL", "N'" & ObjCmf.VerificationPeriodAfterCompletionJob.ToString("yyyy-MM-dd hh:mm:ss tt") & "'") & ", " & IIf(ObjCmf.Status = True, 1, 0) & ", N'" & ObjCmf.UserName.Replace("'", "''") & "',Convert(DateTime, N'" & ObjCmf.EntryDate & "', 102), " & ObjCmf.ApprovedUserId & ", " & ObjCmf.ApproveBudget & ", " & ObjCmf.TaxPercent & ", " & ObjCmf.WHTaxPercent & ", " & IIf(ObjCmf.Approved = True, 1, 0) & "," & ObjCmf.OpexSalePercent & ") Select @@Identity"
            'End Task:2697
            'Before against task:2734
            'TAsk:2703 Added Fields EstimateExpense, ReturnComment, ReturnStatus
            'strSQL = "INSERT INTO CMFAMasterTable(DocNo,DocDate,LocationId,CustomerCode,ProjectId,POId,Remarks,InvoiceNo,TotalQty,TotalAmount,ExptJobCompDate,ExptPaymentFromClient,JobStartingTime,TentiveInvoiceDate,VerificationPeriodAfterCompletionJob,Status,UserName,EntryDate,ApprovedUserId, ApprovedBudget, TaxPercent,WHTaxPercent,Approved, OPEX_Sale_Percent,EstimateExpense,ReturnComment,ReturnStatus,CMFAType,UserId, CheckedByUserId) " _
            '        & " VALUES(N'" & ObjCmf.docNo.Replace("'", "''") & "', Convert(DateTime, N'" & ObjCmf.DocDate & "', 102), " & ObjCmf.locationId & ", " & ObjCmf.CustomerCode & ", " & ObjCmf.ProjectId & ", " & ObjCmf.POId & ", N'" & ObjCmf.Remarks.Replace("'", "''") & "', N'" & ObjCmf.PONo.Replace("'", "''") & "', " & ObjCmf.TotalQty & "," & ObjCmf.TotalAmount & ", " & IIf(ObjCmf.ExptJobCompDate = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.ExptJobCompDate & "',102)") & ", N'" & ObjCmf.ExptPaymentFromClient.Replace("'", "''") & "', " & IIf(ObjCmf.JobStartingTime = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.JobStartingTime & "', 102)") & ", " & IIf(ObjCmf.TentiveInvoiceDate = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.TentiveInvoiceDate & "',102)") & ", " & IIf(ObjCmf.VerificationPeriodAfterCompletionJob = Nothing, "NULL", "N'" & ObjCmf.VerificationPeriodAfterCompletionJob.ToString("yyyy-MM-dd hh:mm:ss tt") & "'") & ", " & IIf(ObjCmf.Status = True, 1, 0) & ", N'" & ObjCmf.UserName.Replace("'", "''") & "',Convert(DateTime, N'" & ObjCmf.EntryDate & "', 102), " & ObjCmf.ApprovedUserId & ", " & ObjCmf.ApproveBudget & ", " & ObjCmf.TaxPercent & ", " & ObjCmf.WHTaxPercent & ", " & IIf(ObjCmf.Approved = True, 1, 0) & "," & ObjCmf.OpexSalePercent & "," & ObjCmf.EstimateExpense & ", N'" & ObjCmf.ReturnComments.Replace("'", "''") & "', " & IIf(ObjCmf.ReturnStatus = True, 1, 0) & ",N'" & ObjCmf.CMFAType.Replace("'", "''") & "', " & ObjCmf.UserID & ", " & ObjCmf.CheckedByUserID & ") Select @@Identity"
            'End Task:2703
            'Task:2734 Added Column Projected Exp Amount

            strSQL = "INSERT INTO CMFAMasterTable(DocNo,DocDate,LocationId,CustomerCode,ProjectId,POId,Remarks,InvoiceNo,TotalQty,TotalAmount,ExptJobCompDate,ExptPaymentFromClient,JobStartingTime,TentiveInvoiceDate,VerificationPeriodAfterCompletionJob,Status,UserName,EntryDate, ApprovedBudget, TaxPercent,WHTaxPercent,OPEX_Sale_Percent,EstimateExpense,CMFAType,UserId, ProjectedExpAmount) " _
                   & " VALUES(N'" & ObjCmf.docNo.Replace("'", "''") & "',Convert(DateTime, N'" & ObjCmf.DocDate & "', 102), " & ObjCmf.locationId & ", " & ObjCmf.CustomerCode & ", " & ObjCmf.ProjectId & ", " & ObjCmf.POId & ", N'" & ObjCmf.Remarks.Replace("'", "''") & "', N'" & ObjCmf.PONo.Replace("'", "''") & "', " & ObjCmf.TotalQty & "," & ObjCmf.TotalAmount & ", " & IIf(ObjCmf.ExptJobCompDate = Nothing, "NULL", " Convert(DateTime, N'" & ObjCmf.ExptJobCompDate & "',102)") & ", N'" & ObjCmf.ExptPaymentFromClient.Replace("'", "''") & "', " & IIf(ObjCmf.JobStartingTime = Nothing, "NULL", " Convert(DateTime, N'" & ObjCmf.JobStartingTime & "', 102)") & ", " & IIf(ObjCmf.TentiveInvoiceDate = Nothing, "NULL", " Convert(DateTime, N'" & ObjCmf.TentiveInvoiceDate & "',102)") & ", " & IIf(ObjCmf.VerificationPeriodAfterCompletionJob = Nothing, "NULL", "N'" & ObjCmf.VerificationPeriodAfterCompletionJob.ToString("yyyy-MM-dd hh:mm:ss tt") & "'") & ", " & IIf(ObjCmf.Status = True, 1, 0) & ", N'" & ObjCmf.UserName.Replace("'", "''") & "',Convert(DateTime, N'" & ObjCmf.EntryDate & "', 102)," & ObjCmf.ApproveBudget & ", " & ObjCmf.TaxPercent & ", " & ObjCmf.WHTaxPercent & "," & ObjCmf.OpexSalePercent & "," & ObjCmf.EstimateExpense & ",N'" & ObjCmf.CMFAType.Replace("'", "''") & "', " & ObjCmf.UserID & "," & ObjCmf.ProjectedExpAmount & ") Select @@Identity"
            'End Task:2734
            ObjCmf.DocId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            ''27-Sep-2014 Task:2856 Imran Ali CMFA Detail Report
            If ObjCmf.ApprovedUserName.Length > 0 Then
                strSQL = String.Empty
                strSQL = "Update CMFAMasterTable Set ApprovedUserName=N'" & ObjCmf.ApprovedUserName.Replace("'", "''") & "' WHERE DocId=" & ObjCmf.DocId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            If ObjCmf.CheckedUserName.Length > 0 Then
                strSQL = String.Empty
                strSQL = "Update CMFAMasterTable Set CheckedUserName=N'" & ObjCmf.CheckedUserName.Replace("'", "''") & "' WHERE DocId=" & ObjCmf.DocId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            'End Task:2856



            Call AddDetail(ObjCmf, trans)

            SaveCMFAExpense(ObjCmf, trans) 'Task:2701 Configured Function Save CMFA Expense Detail

            ''13-Aug-2014 Task:2783 Imran Ali  Frequently CMFA on Home Page (Ravi)
            ObjCmf.ActivityLog = New ActivityLog
            ObjCmf.ActivityLog.ActivityName = "Save"
            ObjCmf.ActivityLog.ApplicationName = "Production"
            ObjCmf.ActivityLog.FormCaption = "CMFA Document"
            ObjCmf.ActivityLog.RecordType = "Production"
            ObjCmf.ActivityLog.RefNo = ObjCmf.docNo
            ObjCmf.ActivityLog.Source = "frmCmfa"
            ObjCmf.ActivityLog.UserID = ObjCmf.UserID
            ObjCmf.ActivityLog.LogDateTime = Now
            ObjCmf.ActivityLog.LogComments = String.Empty
            Call UtilityDAL.BuildActivityLog(ObjCmf.ActivityLog, trans)
            'End Task:2783

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    'Task:2701 Added Save Function CMFA Expense Detail
    Public Function SaveCMFAExpense(ByVal CMFA As CmfaBE, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            If CMFA.CMFAExpVoucher IsNot Nothing Then
                If CMFA.CMFAExpVoucher.Count > 0 Then
                    If CMFA.DocId > 0 Then
                        strSQL = "DELETE From CMFAExpenseTable WHERE DocID=" & CMFA.DocId & ""
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    End If
                    For Each CMFAExp As CMFAExpVoucherBE In CMFA.CMFAExpVoucher
                        strSQL = String.Empty
                        strSQL = "INSERT INTO CMFAExpenseTable(DocId, coa_detail_id, Amount) VALUES(" & CMFA.DocId & ", " & CMFAExp.coa_detail_id & ", " & CMFAExp.Amount & ")"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    Next
                End If
            End If
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
            Return False
        End Try
    End Function
    'End Task:2701
    Public Function Update(ByVal ObjCmf As CmfaBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            'ObjCmf.docNo = UtilityDAL.GetNextDocNo("CMFA-" & ObjCmf.DocDate.ToString("yyMM") & "", 6, "CMFAMasterTable", "DocNo", trans)
            Dim strSQL As String = String.Empty
            'Before against task:2703
            ''19-June-2014 TASK:2697 IMRAN ALI Optional Note  Expected Completion Job E.g
            'strSQL = "UPDATE CMFAMasterTable SET DocNo=N'" & ObjCmf.docNo.Replace("'", "''") & "',DocDate=Convert(DateTime, N'" & ObjCmf.DocDate & "', 102),LocationId=" & ObjCmf.locationId & ",CustomerCode=" & ObjCmf.CustomerCode & ",ProjectId=" & ObjCmf.ProjectId & ",POId=" & ObjCmf.POId & ",Remarks=N'" & ObjCmf.Remarks.Replace("'", "''") & "',InvoiceNo=N'" & ObjCmf.PONo.Replace("'", "''") & "', TotalQty=" & ObjCmf.TotalQty & ",TotalAmount=" & ObjCmf.TotalAmount & ",ExptJobCompDate=" & IIf(ObjCmf.ExptJobCompDate = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.ExptJobCompDate & "',102)") & ",ExptPaymentFromClient=N'" & ObjCmf.ExptPaymentFromClient.Replace("'", "''") & "',JobStartingTime=" & IIf(ObjCmf.JobStartingTime = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.JobStartingTime & "', 102)") & ",TentiveInvoiceDate=" & IIf(ObjCmf.TentiveInvoiceDate = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.TentiveInvoiceDate & "',102)") & ",VerificationPeriodAfterCompletionJob=" & IIf(ObjCmf.VerificationPeriodAfterCompletionJob = Nothing, "NULL", "N'" & ObjCmf.VerificationPeriodAfterCompletionJob.ToString("yyyy-MM-dd hh:mm:ss tt") & "'") & ",Status=" & IIf(ObjCmf.Status = True, 1, 0) & ",UserName=N'" & ObjCmf.UserName.Replace("'", "''") & "',EntryDate=Convert(DateTime, N'" & ObjCmf.EntryDate & "', 102), ApprovedUserId=" & ObjCmf.ApprovedUserId & ", ApprovedBudget=" & ObjCmf.ApproveBudget & ", TaxPercent=" & ObjCmf.TaxPercent & ", WHTaxPercent=" & ObjCmf.WHTaxPercent & ", Approved=" & IIf(ObjCmf.Approved = True, 1, 0) & ", Opex_Sale_Percent=" & ObjCmf.OpexSalePercent & " WHERE DocId=" & ObjCmf.DocId & ""
            'End Task:2697
            'Task:2703 Added Fields EstimateExpense, ReturnComment, ReturnStatus
            'Before aginst task:2734
            'strSQL = "UPDATE CMFAMasterTable SET DocNo=N'" & ObjCmf.docNo.Replace("'", "''") & "',DocDate=Convert(DateTime, N'" & ObjCmf.DocDate & "', 102),LocationId=" & ObjCmf.locationId & ",CustomerCode=" & ObjCmf.CustomerCode & ",ProjectId=" & ObjCmf.ProjectId & ",POId=" & ObjCmf.POId & ",Remarks=N'" & ObjCmf.Remarks.Replace("'", "''") & "',InvoiceNo=N'" & ObjCmf.PONo.Replace("'", "''") & "', TotalQty=" & ObjCmf.TotalQty & ",TotalAmount=" & ObjCmf.TotalAmount & ",ExptJobCompDate=" & IIf(ObjCmf.ExptJobCompDate = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.ExptJobCompDate & "',102)") & ",ExptPaymentFromClient=N'" & ObjCmf.ExptPaymentFromClient.Replace("'", "''") & "',JobStartingTime=" & IIf(ObjCmf.JobStartingTime = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.JobStartingTime & "', 102)") & ",TentiveInvoiceDate=" & IIf(ObjCmf.TentiveInvoiceDate = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.TentiveInvoiceDate & "',102)") & ",VerificationPeriodAfterCompletionJob=" & IIf(ObjCmf.VerificationPeriodAfterCompletionJob = Nothing, "NULL", "N'" & ObjCmf.VerificationPeriodAfterCompletionJob.ToString("yyyy-MM-dd hh:mm:ss tt") & "'") & ",Status=" & IIf(ObjCmf.Status = True, 1, 0) & ",EntryDate=Convert(DateTime, N'" & ObjCmf.EntryDate & "', 102), ApprovedBudget=" & ObjCmf.ApproveBudget & ", TaxPercent=" & ObjCmf.TaxPercent & ", WHTaxPercent=" & ObjCmf.WHTaxPercent & ", Approved=" & IIf(ObjCmf.Approved = True, 1, 0) & ", Opex_Sale_Percent=" & ObjCmf.OpexSalePercent & ", EstimateExpense=" & ObjCmf.EstimateExpense & ", ReturnComment=N'" & ObjCmf.ReturnComments.Replace("'", "''") & "', ReturnStatus=" & IIf(ObjCmf.ReturnStatus = True, 1, 0) & ", CMFAType=N'" & ObjCmf.CMFAType.Replace("'", "''") & "'  "
            'Task:2734 Added Column Projected Exp Amount
            strSQL = "UPDATE CMFAMasterTable SET DocNo=N'" & ObjCmf.docNo.Replace("'", "''") & "',DocDate=Convert(DateTime, N'" & ObjCmf.DocDate & "', 102),LocationId=" & ObjCmf.locationId & ",CustomerCode=" & ObjCmf.CustomerCode & ",ProjectId=" & ObjCmf.ProjectId & ",POId=" & ObjCmf.POId & ",Remarks=N'" & ObjCmf.Remarks.Replace("'", "''") & "',InvoiceNo=N'" & ObjCmf.PONo.Replace("'", "''") & "', TotalQty=" & ObjCmf.TotalQty & ",TotalAmount=" & ObjCmf.TotalAmount & ",ExptJobCompDate=" & IIf(ObjCmf.ExptJobCompDate = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.ExptJobCompDate & "',102)") & ",ExptPaymentFromClient=N'" & ObjCmf.ExptPaymentFromClient.Replace("'", "''") & "',JobStartingTime=" & IIf(ObjCmf.JobStartingTime = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.JobStartingTime & "', 102)") & ",TentiveInvoiceDate=" & IIf(ObjCmf.TentiveInvoiceDate = Nothing, "NULL", "Convert(DateTime, N'" & ObjCmf.TentiveInvoiceDate & "',102)") & ",VerificationPeriodAfterCompletionJob=" & IIf(ObjCmf.VerificationPeriodAfterCompletionJob = Nothing, "NULL", "N'" & ObjCmf.VerificationPeriodAfterCompletionJob.ToString("yyyy-MM-dd hh:mm:ss tt") & "'") & ",Status=" & IIf(ObjCmf.Status = True, 1, 0) & ",EntryDate=Convert(DateTime, N'" & ObjCmf.EntryDate & "', 102), ApprovedBudget=" & ObjCmf.ApproveBudget & ", TaxPercent=" & ObjCmf.TaxPercent & ", WHTaxPercent=" & ObjCmf.WHTaxPercent & ", Opex_Sale_Percent=" & ObjCmf.OpexSalePercent & ", EstimateExpense=" & ObjCmf.EstimateExpense & ", CMFAType=N'" & ObjCmf.CMFAType.Replace("'", "''") & "',ProjectedExpAmount=" & ObjCmf.ProjectedExpAmount & ""
            ''03-Sep-2014 Task:2824 Imran Ali Apply all rights of admin on CMFA Document
            'End Task:2734
            'If ObjCmf.CheckedByUserID > 0 Then
            '    strSQL += ", CheckedByUserId=" & ObjCmf.CheckedByUserID & ""
            'End If
            'If ObjCmf.ApprovedUserId > 0 Then
            '    strSQL += ", ApprovedUserId=" & ObjCmf.ApprovedUserId & ""

            'End If
            'eND tASK:2824
            strSQL += " WHERE(DocId = " & ObjCmf.DocId & ")"
            'End Task:2703
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            ''27-Sep-2014 Task:2856 Imran Ali CMFA Detail Report
            If ObjCmf.ApprovedUserName.Length > 0 Then
                strSQL = String.Empty
                strSQL = "Update CMFAMasterTable Set ApprovedUserName=N'" & ObjCmf.ApprovedUserName.Replace("'", "''") & "' WHERE DocId=" & ObjCmf.DocId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            If ObjCmf.CheckedUserName.Length > 0 Then
                strSQL = String.Empty
                strSQL = "Update CMFAMasterTable Set CheckedUserName=N'" & ObjCmf.CheckedUserName.Replace("'", "''") & "' WHERE DocId=" & ObjCmf.DocId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            'End Task:2856


            strSQL = String.Empty
            strSQL = "Delete From CMFADetailTable WHERE DocId=" & ObjCmf.DocId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            Call AddDetail(ObjCmf, trans)

            SaveCMFAExpense(ObjCmf, trans) 'Task:2701 Configured Function Save CMFA Expense Detail

            ''13-Aug-2014 Task:2783 Imran Ali  Frequently CMFA on Home Page (Ravi)
            ObjCmf.ActivityLog = New ActivityLog
            ObjCmf.ActivityLog.ActivityName = "Save"
            ObjCmf.ActivityLog.ApplicationName = "Production"
            ObjCmf.ActivityLog.FormCaption = "CMFA Document"
            ObjCmf.ActivityLog.RecordType = "Production"
            ObjCmf.ActivityLog.RefNo = ObjCmf.docNo
            ObjCmf.ActivityLog.Source = "frmCmfa"
            ObjCmf.ActivityLog.UserID = ObjCmf.UserID
            ObjCmf.ActivityLog.LogDateTime = Now
            ObjCmf.ActivityLog.LogComments = String.Empty
            Call UtilityDAL.BuildActivityLog(ObjCmf.ActivityLog, trans)
            'End Task:2783
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    ''03-Sep-2014 Task:2824 Imran Ali Apply all rights of admin on CMFA Document
    Public Function UpdateApprovedStatus(ByVal ObjCmf As CmfaBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            'Task:2856 Before 
            'strSQL = "UPDATE CMFAMasterTable SET Approved=" & IIf(ObjCmf.Approved = True, 1, 0) & ", ApprovedUserId=" & ObjCmf.ApprovedUserId & ",CheckedByUserId=0,ReturnStatus=0,UserId=" & ObjCmf.UserID & ",CheckedStatus=0 WHERE DocId=" & ObjCmf.DocId & ""
            'Task:2856 Update Approved User Name
            strSQL = "UPDATE CMFAMasterTable SET Approved=" & IIf(ObjCmf.Approved = True, 1, 0) & ", ApprovedUserId=" & ObjCmf.ApprovedUserId & ",ApprovedUserName=N'" & ObjCmf.ApprovedUserName.Replace("'", "''") & "', CheckedByUserId=0,ReturnStatus=0,UserId=" & ObjCmf.UserID & ",CheckedStatus=0 WHERE DocId=" & ObjCmf.DocId & ""
            'End task:2856
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    ''03-Sep-2014 Task:2824 Imran Ali Apply all rights of admin on CMFA Document
    Public Function UpdateCheckByManager(ByVal ObjCmf As CmfaBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            'Task:2856 Before 
            'strSQL = "UPDATE CMFAMasterTable SET CheckedByUserId=" & ObjCmf.CheckedByUserID & ", Approved=0,ApprovedUserId=0,ReturnStatus=0,UserId=" & ObjCmf.UserID & ", CheckedStatus=" & IIf(ObjCmf.CheckedStatus = True, 1, 0) & " WHERE DocId=" & ObjCmf.DocId & ""
            'Task:2856 Update Checked User Name
            strSQL = "UPDATE CMFAMasterTable SET CheckedByUserId=" & ObjCmf.CheckedByUserID & ",CheckedUserName=N'" & ObjCmf.CheckedUserName.Replace("'", "''") & "', Approved=0,ApprovedUserId=0,ReturnStatus=0,UserId=" & ObjCmf.UserID & ", CheckedStatus=" & IIf(ObjCmf.CheckedStatus = True, 1, 0) & " WHERE DocId=" & ObjCmf.DocId & ""
            'End TAsk:2856
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    'eND tASK:2824
    Public Function Delete(ByVal ObjCmf As CmfaBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            'Task:2701 Delete CMFA Expense History By DocID
            strSQL = String.Empty
            strSQL = "Delete From CMFAExpenseTable WHERE DocId=" & ObjCmf.DocId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'End Task:2701 

            strSQL = String.Empty
            strSQL = "Delete From CMFADetailTable WHERE DocId=" & ObjCmf.DocId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From CMFAMasterTable WHERE DocId=" & ObjCmf.DocId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            ''13-Aug-2014 Task:2783 Imran Ali  Frequently CMFA on Home Page (Ravi)
            ObjCmf.ActivityLog = New ActivityLog
            ObjCmf.ActivityLog.ActivityName = "Save"
            ObjCmf.ActivityLog.ApplicationName = "Production"
            ObjCmf.ActivityLog.FormCaption = "CMFA Document"
            ObjCmf.ActivityLog.RecordType = "Production"
            ObjCmf.ActivityLog.RefNo = ObjCmf.docNo
            ObjCmf.ActivityLog.Source = "frmCmfa"
            ObjCmf.ActivityLog.UserID = ObjCmf.UserID
            ObjCmf.ActivityLog.LogDateTime = Now
            ObjCmf.ActivityLog.LogComments = String.Empty
            Call UtilityDAL.BuildActivityLog(ObjCmf.ActivityLog, trans)
            'End Task:2783

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddDetail(ByVal objcmf As CmfaBE, ByVal objTrans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            If objcmf.CMFADetail IsNot Nothing Then
                If objcmf.CMFADetail.Count > 0 Then
                    For Each objCMFADetail As CMFADetailBE In objcmf.CMFADetail
                        'Before against task:2723
                        'strSQL = "INSERT INTO CMFADetailTable(DocId,LocationId,ArticleDefId,ArticleSize,Sz1,Sz2,Sz7,Qty,POQty,Price,Current_Price,Tax_Percent,VendorId,PackDesc, InvoicePrice) " _
                        '& " VALUES(" & objcmf.DocId & ", " & objCMFADetail.LocationId & ", " & objCMFADetail.ArticleDefId & ", N'" & objCMFADetail.ArticleSize.Replace("'", "''") & "', " & Val(objCMFADetail.Sz1) & ", " & Val(objCMFADetail.Sz2) & ", " & Val(objCMFADetail.Sz7) & ", " & Val(objCMFADetail.Qty) & ", " & Val(objCMFADetail.POQty) & ", " & Val(objCMFADetail.Price) & ", " & Val(objCMFADetail.Current_Price) & ", " & Val(objCMFADetail.Tax_Percent) & ", " & Val(objCMFADetail.VendorId) & ",N'" & objCMFADetail.PackDesc.Replace("'", "''") & "', " & objCMFADetail.InvoicePrice & ")"
                        'Task:2723 Added Field Comments
                        strSQL = "INSERT INTO CMFADetailTable(DocId,LocationId,ArticleDefId,ArticleSize,Sz1,Sz2,Sz7,Qty,POQty,Price,Current_Price,Tax_Percent,VendorId,PackDesc, InvoicePrice,Comments) " _
                        & " VALUES(" & objcmf.DocId & ", " & objCMFADetail.LocationId & ", " & objCMFADetail.ArticleDefId & ", N'" & objCMFADetail.ArticleSize.Replace("'", "''") & "', " & Val(objCMFADetail.Sz1) & ", " & Val(objCMFADetail.Sz2) & ", " & Val(objCMFADetail.Sz7) & ", " & Val(objCMFADetail.Qty) & ", " & Val(objCMFADetail.POQty) & ", " & Val(objCMFADetail.Price) & ", " & Val(objCMFADetail.Current_Price) & ", " & Val(objCMFADetail.Tax_Percent) & ", " & Val(objCMFADetail.VendorId) & ",N'" & objCMFADetail.PackDesc.Replace("'", "''") & "', " & objCMFADetail.InvoicePrice & ",N'" & objCMFADetail.Comments.Replace("'", "''") & "')"
                        'End Task:2723
                        SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
                    Next
                End If
            End If
            Return True
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex

        End Try
    End Function
    Public Function DetailRecord(ByVal DocId As Integer)
        Try

            Dim objDt As New DataTable
            Dim strSQL As String = String.Empty
            'Before against task:2723
            'strSQL = "SELECT dbo.CMFADetailTable.LocationId, dbo.CMFADetailTable.ArticleDefId, dbo.ArticleDefView.ArticleCode AS Code, dbo.ArticleDefView.ArticleDescription AS Item, " _
            '         & " dbo.ArticleDefView.ArticleSizeName AS Size, dbo.ArticleDefView.ArticleColorName AS Color, ArticleDefView.ArticleUnitName as UOM, dbo.CMFADetailTable.ArticleSize AS Unit,  " _
            '         & " ISNULL(dbo.CMFADetailTable.Sz7, 0) AS PackQty, ISNULL(dbo.CMFADetailTable.Sz1, 0) AS Qty, ISNULL(dbo.CMFADetailTable.Price, 0) AS Price," _
            '         & " Convert(float,0) as Total,ISNULL(dbo.CMFADetailTable.Current_Price, 0) AS Current_Price, dbo.CMFADetailTable.VendorId, dbo.CMFADetailTable.PackDesc, ISNULL(dbo.CMFADetailTable.InvoicePrice,0) as InvoicePrice " _
            '         & " FROM dbo.CMFADetailTable LEFT OUTER JOIN " _
            '         & " dbo.ArticleDefView ON dbo.CMFADetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId " _
            '         & " WHERE (dbo.CMFADetailTable.DocId=" & DocId & ") ORDER BY CMFADetailTable.DocDetailId ASC"
            'Task:2723 Added Field Comments
            strSQL = "SELECT dbo.CMFADetailTable.LocationId, dbo.CMFADetailTable.ArticleDefId, dbo.ArticleDefView.ArticleCode AS Code, dbo.ArticleDefView.ArticleDescription AS Item, " _
                     & " dbo.ArticleDefView.ArticleSizeName AS Size, dbo.ArticleDefView.ArticleColorName AS Color, ArticleDefView.ArticleUnitName as UOM, dbo.CMFADetailTable.ArticleSize AS Unit,  " _
                     & " ISNULL(dbo.CMFADetailTable.Sz7, 0) AS PackQty, ISNULL(dbo.CMFADetailTable.Sz1, 0) AS Qty, ISNULL(dbo.CMFADetailTable.Price, 0) AS Price," _
                     & " Convert(float,0) as Total,ISNULL(dbo.CMFADetailTable.Current_Price, 0) AS Current_Price, dbo.CMFADetailTable.VendorId, dbo.CMFADetailTable.PackDesc, ISNULL(dbo.CMFADetailTable.InvoicePrice,0) as InvoicePrice, dbo.CMFADetailTable.Comments " _
                     & " FROM dbo.CMFADetailTable LEFT OUTER JOIN " _
                     & " dbo.ArticleDefView ON dbo.CMFADetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId " _
                     & " WHERE (dbo.CMFADetailTable.DocId=" & DocId & ") ORDER BY CMFADetailTable.DocDetailId ASC"
            'End Task:2723
            objDt = UtilityDAL.GetDataTable(strSQL)
            Return objDt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SalesOrderDetailRecord(ByVal POId As Integer)
        Try

            Dim objDt As New DataTable
            Dim strSQL As String = String.Empty
            'Before against task:2723
            'strSQL = "SELECT dbo.SalesOrderDetailTable.LocationId, dbo.SalesOrderDetailTable.ArticleDefId, dbo.ArticleDefView.ArticleCode AS Code, dbo.ArticleDefView.ArticleDescription AS Item, " _
            '         & " dbo.ArticleDefView.ArticleSizeName AS Size, dbo.ArticleDefView.ArticleColorName AS Color, dbo.SalesOrderDetailTable.ArticleSize AS Unit,  " _
            '         & " ISNULL(dbo.SalesOrderDetailTable.Sz1, 0) AS PackQty, ISNULL(dbo.SalesOrderDetailTable.Sz7, 0) AS Qty, ISNULL(dbo.SalesOrderDetailTable.PurchasePrice, 0) AS Price," _
            '         & " Convert(float,0) as Total,ISNULL(dbo.SalesOrderDetailTable.PurchasePrice, 0) AS Current_Price, 0 as VendorId, dbo.SalesOrderDetailTable.Pack_Desc  as PackDesc, Isnull(ArticleDefView.SalePrice,0) as InvoicePrice " _
            '         & " FROM dbo.SalesOrderDetailTable LEFT OUTER JOIN " _
            '         & " dbo.ArticleDefView ON dbo.SalesOrderDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId " _
            '         & " WHERE (dbo.SalesOrderDetailTable.SalesOrderId=" & POId & ")"
            'Task:2723 Added Field Comments
            strSQL = "SELECT dbo.SalesOrderDetailTable.LocationId, dbo.SalesOrderDetailTable.ArticleDefId, dbo.ArticleDefView.ArticleCode AS Code, dbo.ArticleDefView.ArticleDescription AS Item, " _
                 & " dbo.ArticleDefView.ArticleSizeName AS Size, dbo.ArticleDefView.ArticleColorName AS Color, dbo.SalesOrderDetailTable.ArticleSize AS Unit,  " _
                 & " ISNULL(dbo.SalesOrderDetailTable.Sz1, 0) AS PackQty, ISNULL(dbo.SalesOrderDetailTable.Sz7, 0) AS Qty, ISNULL(dbo.SalesOrderDetailTable.PurchasePrice, 0) AS Price," _
                 & " Convert(float,0) as Total,ISNULL(dbo.SalesOrderDetailTable.PurchasePrice, 0) AS Current_Price, 0 as VendorId, dbo.SalesOrderDetailTable.Pack_Desc  as PackDesc, Isnull(ArticleDefView.SalePrice,0) as InvoicePrice, dbo.SalesOrderDetailTable.Comments " _
                 & " FROM dbo.SalesOrderDetailTable LEFT OUTER JOIN " _
                 & " dbo.ArticleDefView ON dbo.SalesOrderDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId " _
                 & " WHERE (dbo.SalesOrderDetailTable.SalesOrderId=" & POId & ")"
            'End Task:2723
            objDt = UtilityDAL.GetDataTable(strSQL)
            Return objDt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAllRecords(Optional ByVal CheckedBy As Boolean = False, Optional ByVal Condition As String = "") As DataTable
        Try
            Dim strSQL As String = String.Empty
            Dim objDt As New DataTable
            'Before against task:2703
            'strSQL = "SELECT CMFA.DocId,CMFA.LocationId,CMFA.ProjectId,COA.coa_detail_id, Isnull(CMFA.POId,0) as POId, CMFA.DocNo, CMFA.DocDate, Comp.CompanyName as Company, COA.detail_title as [Customer], Project.Name as Project, SO.SalesOrderNo as [SO No], CMFA.Remarks, CMFA.InvoiceNo, CMFA.ExptJobCompDate, " _
            '         & " CMFA.ExptPaymentFromClient, CMFA.JobStartingTime, CMFA.TentiveInvoiceDate, CMFA.VerificationPeriodAfterCompletionJob, CMFA.Status, IsNull(CMFA.ApprovedBudget,0) as ApprovedBudget, IsNull(CMFA.TaxPercent,0) as TaxPercent, Isnull(CMFA.WHTaxPercent,0) as WHTaxPercent, IsNull(CMFA.ApprovedUserId,0) as ApprovedUserId, IsNull(CMFA.Approved,0) as Approved, ISNULL(CMFA.OPEX_Sale_Percent,0) as OPEX_Sale_Percent " _
            '         & " FROM dbo.CMFAMasterTable AS CMFA LEFT OUTER JOIN " _
            '         & " dbo.tblDefCostCenter AS Project ON CMFA.ProjectId = Project.CostCenterID LEFT OUTER JOIN " _
            '         & " dbo.vwCOADetail AS COA ON CMFA.CustomerCode = COA.coa_detail_id LEFT OUTER JOIN " _
            '         & " dbo.CompanyDefTable AS Comp ON CMFA.LocationId = Comp.CompanyId LEFT OUTER JOIN " _
            '         & " dbo.SalesOrderMasterTable AS SO ON CMFA.POId = SO.SalesOrderId ORDER BY CMFA.DocNo DESC "
            'Task:2703 Added Fields EstimateExpense, ReturnComment, ReturnStatus
            strSQL = "SELECT CMFA.DocId,CMFA.LocationId,CMFA.ProjectId,COA.coa_detail_id, Isnull(CMFA.POId,0) as POId, CMFA.DocNo, CMFA.DocDate, Comp.CompanyName as Company, COA.detail_title as [Customer], Project.Name as Project, SO.SalesOrderNo as [SO No], CMFA.Remarks, CMFA.InvoiceNo, CMFA.ExptJobCompDate, " _
                    & " CMFA.ExptPaymentFromClient, CMFA.JobStartingTime, CMFA.TentiveInvoiceDate, CMFA.VerificationPeriodAfterCompletionJob, CMFA.Status, IsNull(CMFA.ApprovedBudget,0) as ApprovedBudget, IsNull(CMFA.TaxPercent,0) as TaxPercent, Isnull(CMFA.WHTaxPercent,0) as WHTaxPercent, IsNull(CMFA.ApprovedUserId,0) as ApprovedUserId, IsNull(CMFA.Approved,0) as Approved, ISNULL(CMFA.OPEX_Sale_Percent,0) as OPEX_Sale_Percent, Isnull(CMFA.EstimateExpense,0) as EstimateExpense, CMFA.ReturnComment, ISNULL(CMFA.ReturnStatus,0) as ReturnStatus, CMFA.CMFAType, CMFA.UserName  " _
                    & " FROM dbo.CMFAMasterTable AS CMFA LEFT OUTER JOIN " _
                    & " dbo.tblDefCostCenter AS Project ON CMFA.ProjectId = Project.CostCenterID LEFT OUTER JOIN " _
                    & " dbo.vwCOADetail AS COA ON CMFA.CustomerCode = COA.coa_detail_id LEFT OUTER JOIN " _
                    & " dbo.CompanyDefTable AS Comp ON CMFA.LocationId = Comp.CompanyId LEFT OUTER JOIN " _
                    & " dbo.SalesOrderMasterTable AS SO ON CMFA.POId = SO.SalesOrderId  WHERE  CMFA.DocId <> 0"

            If CheckedBy = False Then
                strSQL += " AND CMFA.UserID"
            End If
            strSQL += " ORDER BY CMFA.DocNo DESC "
            'End Task:2703
            objDt = UtilityDAL.GetDataTable(strSQL)
            Return objDt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateReturn(ByVal DocID As Integer, ByVal ReturnComment As String, ByVal ReturnStatus As Boolean) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "UPDATE CMFAMasterTable SET ReturnComment=N'" & ReturnComment.Replace("'", "''") & "', ReturnStatus=" & IIf(ReturnStatus = True, 1, 0) & ",Approved=0,ApprovedUserId=0,CheckedByUserId=0,CheckedStatus=0 WHERE DocId=" & DocID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function CMFADocumentSave(ByVal DocId As Integer) As Integer

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO CMFAAttachDocumentsTable([DocID]) VALUES(" & DocId & ")Select @@Identity"
            Dim obj As Object = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return Val(obj)
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function CMFADocumentUpdate(ByVal ID As Integer, ByVal File_Name As String, ByVal File_Path As String) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update CMFAAttachDocumentsTable  SET File_Name=N'" & File_Name.Replace("'", "''") & "',File_Path=N'" & File_Path.Replace("'", "''") & "' WHERE ID=" & ID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function CMFAGetAllDocument(ByVal DocId As Integer) As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Select ID, DocId, File_Name, File_Path, File_Path + '\' + File_Name as strFilePath From CMFAAttachDocumentsTable WHERE DocID=" & DocId & " AND File_Name <> ''"
            Dim objdt As New DataTable
            objdt = UtilityDAL.GetDataTable(strSQL)
            Return objdt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
