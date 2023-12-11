''15-9-2015 TASKM159151 IMRAN Employee's Receivable Voucher
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient

Public Class SalariesExpenseDAL
    Dim VoucherMaster As VouchersMaster
    Dim VoucherDetailDebit As VouchersDetail
    Dim VoucherDetailCredit As VouchersDetail
    Dim VoucherDetailNetSalary As VouchersDetail
    Dim VoucherDetailCreditCashBank As VouchersDetail
    Dim VoucherDetailDebitSalary As VouchersDetail
    Dim ReceiveableAccountId As Integer = 0
    Public Function AddSalariesExp(ByVal SalariesExp As SalariesExpenseMaster) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try



            Dim str As String = String.Empty
            SalariesExp.SalaryExpNo = GetDocumentNo(SalariesExp.SalaryExpDate, trans)

            'Insert Salaries Expense Master Information here...
            str = "INSERT INTO SalariesExpenseMasterTable(SalaryExpDate,SalaryExpNo,EmployeeId,CostCenterId, Post, NetSalary, Remarks,UserName,FDate, GrossSalary) " _
            & " Values(N'" & SalariesExp.SalaryExpDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & SalariesExp.SalaryExpNo & "', " & SalariesExp.EmployeeId & ", " & SalariesExp.CostCenterId & ", " & IIf(SalariesExp.Post = True, 1, 0) & ",  " & SalariesExp.NetSalary & ",  N'" & SalariesExp.Remarks & "', N'" & SalariesExp.UserName & "', N'" & SalariesExp.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & SalariesExp.GrossSalary & ") Select @@Identity"
            SalariesExp.SalaryExpId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            'Insert Salaries Expense Detail Information here...
            AddSalariesExpDetail(SalariesExp.SalaryExpId, SalariesExp, trans)
            'Model Fill of Voucher here...
            VoucherMaster = New VouchersMaster
            VoucherMaster.VoucherId = 0
            VoucherMaster.LocationId = 0
            VoucherMaster.VoucherCode = SalariesExp.SalaryExpNo
            VoucherMaster.FinancialYearId = 1
            VoucherMaster.VoucherTypeId = 1
            VoucherMaster.VoucherMonth = SalariesExp.SalaryExpDate.Date.Month
            VoucherMaster.VoucherNo = SalariesExp.SalaryExpNo
            VoucherMaster.VNo = SalariesExp.SalaryExpNo
            VoucherMaster.VoucherDate = SalariesExp.SalaryExpDate.ToString("yyyy-M-d h:mm:ss tt")
            VoucherMaster.CoaDetailId = 0
            'VoucherMaster.ChequeNo = "NULL"
            VoucherMaster.ChequeNo = String.Empty
            'VoucherMaster.ChequeDate = "NULL"
            VoucherMaster.ChequeDate = Nothing
            VoucherMaster.Post = SalariesExp.Post
            VoucherMaster.Source = "frmEmployeeSalaryVoucher"
            VoucherMaster.References = SalariesExp.Remarks
            VoucherMaster.UserName = SalariesExp.UserName

            VoucherMaster.VoucherDatail = New List(Of VouchersDetail)
            Dim SalariesExpList As List(Of SalariesExpenseDetail) = SalariesExp.SalariesExpenseDetail
            For Each SalariesExpDetail As SalariesExpenseDetail In SalariesExpList
                If SalariesExpDetail.DeductionFlag = False Then
                    VoucherDetailDebit = New VouchersDetail
                    VoucherDetailDebit.VoucherId = 0
                    VoucherDetailDebit.LocationId = 0
                    'VoucherDetailDebit.CoaDetailId = SalariesExpDetail.AccountId 'IIf(SalariesExpDetail.Advance = True, GetEmpReceiveableAccountId(SalariesExp.EmployeeId, SalariesExpDetail.SalaryTypeId, trans), SalariesExpDetail.AccountId) 'SalariesExp.SalaryAccountId 'Employee Salary Account Id
                    ''15-9-2015 TASKM159151 IMRAN Employee's Receivable Voucher
                    VoucherDetailDebit.CoaDetailId = IIf(SalariesExpDetail.Advance = True, GetEmpReceiveableAccountId(SalariesExp.EmployeeId, SalariesExpDetail.SalaryTypeId, trans), SalariesExpDetail.AccountId) 'SalariesExp.SalaryAccountId 'Employee Salary Account Id
                    VoucherDetailDebit.Comments = "" & SalariesExpDetail.SalaryType & " for the month of " & SalariesExp.SalaryExpDate.ToString("MMM-yyyy") & ""
                    VoucherDetailDebit.DebitAmount = SalariesExpDetail.Earning 'SalariesExp.NetSalary
                    VoucherDetailDebit.CreditAmount = 0
                    VoucherDetailDebit.CostCenter = SalariesExp.CostCenterId
                    VoucherDetailDebit.SPReference = String.Empty
                    VoucherDetailDebit.Cheque_No = String.Empty
                    VoucherDetailDebit.Cheque_Date = Nothing
                    VoucherDetailDebit.contra_coa_detail_id = SalariesExp.EmpSalaryPayableAccountId
                    VoucherDetailDebit.EmpId = SalariesExp.EmployeeId
                    VoucherMaster.VoucherDatail.Add(VoucherDetailDebit)
                Else
                    VoucherDetailCredit = New VouchersDetail
                    VoucherDetailCredit.VoucherId = 0
                    VoucherDetailCredit.LocationId = 0
                    'VoucherDetailCredit.CoaDetailId = SalariesExpDetail.AccountId 'IIf(SalariesExpDetail.Advance = True, GetEmpReceiveableAccountId(SalariesExp.EmployeeId, SalariesExpDetail.SalaryTypeId, trans), SalariesExpDetail.AccountId) 'SalariesExp.EmpSalaryPayableAccountId  'Salary Account Id
                    ''15-9-2015 TASKM159151 IMRAN Employee's Receivable Voucher
                    VoucherDetailCredit.CoaDetailId = IIf(SalariesExpDetail.Advance = True, GetEmpReceiveableAccountId(SalariesExp.EmployeeId, SalariesExpDetail.SalaryTypeId, trans), SalariesExpDetail.AccountId) 'SalariesExp.EmpSalaryPayableAccountId  'Salary Account Id
                    VoucherDetailCredit.Comments = "" & SalariesExpDetail.SalaryType & " for the month of " & SalariesExp.SalaryExpDate.ToString("MMM-yyyy") & ""
                    VoucherDetailCredit.DebitAmount = 0
                    VoucherDetailCredit.CreditAmount = SalariesExpDetail.Deduction  'SalariesExp.NetSalary
                    VoucherDetailCredit.CostCenter = SalariesExp.CostCenterId
                    VoucherDetailCredit.SPReference = String.Empty
                    VoucherDetailCredit.Cheque_No = String.Empty
                    VoucherDetailCredit.Cheque_Date = Nothing
                    VoucherDetailCredit.contra_coa_detail_id = SalariesExp.EmpSalaryPayableAccountId
                    VoucherDetailCredit.EmpId = SalariesExp.EmployeeId
                    VoucherMaster.VoucherDatail.Add(VoucherDetailCredit)
                End If
            Next

            VoucherDetailNetSalary = New VouchersDetail
            VoucherDetailNetSalary.VoucherId = 0
            VoucherDetailNetSalary.LocationId = 0
            VoucherDetailNetSalary.CoaDetailId = SalariesExp.EmpSalaryPayableAccountId  'Salary Account Id
            VoucherDetailNetSalary.Comments = "Net Salary for the month of " & SalariesExp.SalaryExpDate.ToString("MMM-yyyy") & ""
            VoucherDetailNetSalary.DebitAmount = 0
            VoucherDetailNetSalary.CreditAmount = SalariesExp.NetSalary
            VoucherDetailNetSalary.CostCenter = SalariesExp.CostCenterId
            VoucherDetailNetSalary.SPReference = String.Empty
            VoucherDetailNetSalary.Cheque_No = String.Empty
            VoucherDetailNetSalary.Cheque_Date = Nothing
            VoucherDetailNetSalary.contra_coa_detail_id = SalariesExp.EmpSalaryPayableAccountId
            VoucherDetailNetSalary.EmpId = SalariesExp.EmployeeId
            VoucherMaster.VoucherDatail.Add(VoucherDetailNetSalary)

            VoucherMaster.ActivityLog = New ActivityLog
            VoucherMaster.ActivityLog.ActivityName = "Save"
            VoucherMaster.ActivityLog.RecordType = "Accounts"
            VoucherMaster.ActivityLog.RefNo = SalariesExp.SalaryExpNo
            VoucherMaster.ActivityLog.ApplicationName = "Salaries Expense"
            VoucherMaster.ActivityLog.FormCaption = "Salaries Expense"
            VoucherMaster.ActivityLog.UserID = 1
            VoucherMaster.ActivityLog.LogDateTime = Date.Now
            'Call Voucher DAL for Voucher ...
            Call New VouchersDAL().Add(VoucherMaster, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddSalariesExpDetail(ByVal SalariesExpId As Integer, ByVal SalariesExp As SalariesExpenseMaster, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Try
            Dim str As String = String.Empty
            Dim SalariesExpList As List(Of SalariesExpenseDetail) = SalariesExp.SalariesExpenseDetail
            For Each SalariesExpDetail As SalariesExpenseDetail In SalariesExpList
                str = "INSERT INTO SalariesExpenseDetailTable(SalaryExpId,SalaryExpTypeId,Earning,Deduction, AccountId)  " _
                & " Values (" & SalariesExpId & ", " & SalariesExpDetail.SalaryTypeId & ", " & SalariesExpDetail.Earning & ", " & SalariesExpDetail.Deduction & ", " & SalariesExpDetail.AccountId & ") "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
        End Try
    End Function
    Public Function UpdateSalariesExp(ByVal SalariesExp As SalariesExpenseMaster) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try

            Dim VoucherId As Integer = Convert.ToInt32(UtilityDAL.GetVoucherIdByVoucherNo("" & SalariesExp.SalaryExpNo & "").ToString)
            Dim str As String = String.Empty
            'Update Salaries Expense Master Infomation
            str = "Update SalariesExpenseMasterTable Set SalaryExpDate=N'" & SalariesExp.SalaryExpDate.ToString("yyyy-M-d h:mm:ss tt") & "', SalaryExpNo=N'" & SalariesExp.SalaryExpNo & "', EmployeeId=" & SalariesExp.EmployeeId & ", Remarks=N'" & SalariesExp.Remarks & "', CostCenterId=" & SalariesExp.CostCenterId & ", Post=" & IIf(SalariesExp.Post = True, 1, 0) & ",NetSalary=" & SalariesExp.NetSalary & ",  UserName=N'" & SalariesExp.UserName & "', FDate=N'" & SalariesExp.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', GrossSalary=" & SalariesExp.GrossSalary & " WHERE SalaryExpId=" & SalariesExp.SalaryExpId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Salaries Expense Detail Information here...
            str = String.Empty
            str = "Delete From SalariesExpenseDetailTable WHERE SalaryExpId=" & SalariesExp.SalaryExpId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Salaries Expense Detail Information here...
            AddSalariesExpDetail(SalariesExp.SalaryExpId, SalariesExp, trans)
            str = String.Empty
            str = "Delete From tblVoucherDetail WHERE Voucher_Id=" & VoucherId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'str = String.Empty
            'str = "Delete From tblVoucher WHERE Voucher_Id=" & VoucherId
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'Model Fill of Voucher here...
            VoucherMaster = New VouchersMaster
            VoucherMaster.VoucherId = 0
            VoucherMaster.LocationId = 0
            VoucherMaster.VoucherCode = SalariesExp.SalaryExpNo
            VoucherMaster.FinancialYearId = 1
            VoucherMaster.VoucherTypeId = 1
            VoucherMaster.VoucherMonth = SalariesExp.SalaryExpDate.Date.Month
            VoucherMaster.VoucherNo = SalariesExp.SalaryExpNo
            VoucherMaster.VNo = SalariesExp.SalaryExpNo
            VoucherMaster.VoucherDate = SalariesExp.SalaryExpDate.ToString("yyyy-M-d h:mm:ss tt")
            VoucherMaster.CoaDetailId = 0
            'VoucherMaster.ChequeNo = "NULL"
            'VoucherMaster.ChequeDate = "NULL"
            VoucherMaster.ChequeNo = String.Empty
            VoucherMaster.ChequeDate = Nothing
            VoucherMaster.Post = SalariesExp.Post
            VoucherMaster.Source = "frmEmployeeSalaryVoucher"
            VoucherMaster.References = SalariesExp.Remarks
            VoucherMaster.UserName = SalariesExp.UserName
            VoucherMaster.Posted_UserName = String.Empty
            VoucherMaster.VoucherDatail = New List(Of VouchersDetail)
            Dim SalariesExpList As List(Of SalariesExpenseDetail) = SalariesExp.SalariesExpenseDetail
            For Each SalariesExpDetail As SalariesExpenseDetail In SalariesExpList
                If SalariesExpDetail.DeductionFlag = False Then
                    VoucherDetailDebit = New VouchersDetail
                    VoucherDetailDebit.VoucherId = 0
                    VoucherDetailDebit.LocationId = 0
                    VoucherDetailDebit.CoaDetailId = IIf(SalariesExpDetail.Advance = True, GetEmpReceiveableAccountId(SalariesExp.EmployeeId, SalariesExpDetail.SalaryTypeId, trans), SalariesExpDetail.AccountId) 'SalariesExp.SalaryAccountId 'Employee Salary Account Id
                    VoucherDetailDebit.Comments = "" & SalariesExpDetail.SalaryType & " for the month of " & SalariesExp.SalaryExpDate.ToString("MMM-yyyy") & ""
                    VoucherDetailDebit.DebitAmount = SalariesExpDetail.Earning 'SalariesExp.NetSalary
                    VoucherDetailDebit.CreditAmount = 0
                    VoucherDetailDebit.CostCenter = SalariesExp.CostCenterId
                    VoucherDetailDebit.SPReference = String.Empty
                    VoucherDetailDebit.Cheque_No = String.Empty
                    VoucherDetailDebit.Cheque_Date = Nothing
                    VoucherDetailDebit.contra_coa_detail_id = SalariesExp.EmpSalaryPayableAccountId
                    VoucherMaster.VoucherDatail.Add(VoucherDetailDebit)
                Else
                    VoucherDetailCredit = New VouchersDetail
                    VoucherDetailCredit.VoucherId = 0
                    VoucherDetailCredit.LocationId = 0
                    VoucherDetailCredit.CoaDetailId = IIf(SalariesExpDetail.Advance = True, GetEmpReceiveableAccountId(SalariesExp.EmployeeId, SalariesExpDetail.SalaryTypeId, trans), SalariesExpDetail.AccountId) 'SalariesExp.EmpSalaryPayableAccountId  'Salary Account Id
                    VoucherDetailCredit.Comments = "" & SalariesExpDetail.SalaryType & " for the month of " & SalariesExp.SalaryExpDate.ToString("MMM-yyyy") & ""
                    VoucherDetailCredit.DebitAmount = 0
                    VoucherDetailCredit.CreditAmount = SalariesExpDetail.Deduction  'SalariesExp.NetSalary
                    VoucherDetailCredit.CostCenter = SalariesExp.CostCenterId
                    VoucherDetailCredit.SPReference = String.Empty
                    VoucherDetailCredit.Cheque_No = String.Empty
                    VoucherDetailCredit.Cheque_Date = Nothing
                    VoucherDetailCredit.contra_coa_detail_id = SalariesExp.EmpSalaryPayableAccountId
                    VoucherMaster.VoucherDatail.Add(VoucherDetailCredit)
                End If
            Next

            VoucherDetailNetSalary = New VouchersDetail
            VoucherDetailNetSalary.VoucherId = 0
            VoucherDetailNetSalary.LocationId = 0
            VoucherDetailNetSalary.CoaDetailId = SalariesExp.EmpSalaryPayableAccountId  'Salary Account Id
            VoucherDetailNetSalary.Comments = "Net Salary for the month of " & SalariesExp.SalaryExpDate.ToString("MMM-yyyy") & ""
            VoucherDetailNetSalary.DebitAmount = 0
            VoucherDetailNetSalary.CreditAmount = SalariesExp.NetSalary
            VoucherDetailNetSalary.CostCenter = SalariesExp.CostCenterId
            VoucherDetailNetSalary.SPReference = String.Empty
            VoucherDetailNetSalary.Cheque_No = String.Empty
            VoucherDetailNetSalary.Cheque_Date = Nothing
            VoucherDetailNetSalary.contra_coa_detail_id = SalariesExp.EmpSalaryPayableAccountId
            VoucherMaster.VoucherDatail.Add(VoucherDetailNetSalary)


            VoucherMaster.ActivityLog = New ActivityLog
            VoucherMaster.ActivityLog.ActivityName = "Save"
            VoucherMaster.ActivityLog.RecordType = "Accounts"
            VoucherMaster.ActivityLog.RefNo = SalariesExp.SalaryExpNo
            VoucherMaster.ActivityLog.ApplicationName = "Salaries Expense"
            VoucherMaster.ActivityLog.FormCaption = "Salaries Expense"
            VoucherMaster.ActivityLog.UserID = 1
            VoucherMaster.ActivityLog.LogDateTime = Date.Now
            'Call Voucher DAL for Voucher ...
            Call New VouchersDAL().Update(VoucherMaster, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function DeleteSalariesExp(ByVal SalariesExp As SalariesExpenseMaster) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try

            Dim VoucherId As Integer = Convert.ToInt32(UtilityDAL.GetVoucherIdByVoucherNo("" & SalariesExp.SalaryExpNo & ""))
            Dim str As String = String.Empty
            'Delete Salaries Expense Detail Information here...
            str = "Delete From SalariesExpenseDetailTable WHERE SalaryExpId=" & SalariesExp.SalaryExpId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Salaries Expense Master Information here...
            str = String.Empty
            str = "Delete From SalariesExpenseMasterTable WHERE SalaryExpId=" & SalariesExp.SalaryExpId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Voucher 
            str = String.Empty
            str = "Delete From tblVoucherDetail WHERE Voucher_Id=" & VoucherId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = String.Empty
            str = "Delete From tblVoucher WHERE Voucher_Id=" & VoucherId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            VoucherMaster = New VouchersMaster
            VoucherMaster.VNo = SalariesExp.SalaryExpNo
            VoucherMaster.ActivityLog = New ActivityLog
            VoucherMaster.ActivityLog.ActivityName = "Delete"
            VoucherMaster.ActivityLog.RecordType = "Accounts"
            VoucherMaster.ActivityLog.RefNo = SalariesExp.SalaryExpNo
            VoucherMaster.ActivityLog.ApplicationName = "Salaries Expense"
            VoucherMaster.ActivityLog.FormCaption = "Salaries Expense"
            VoucherMaster.ActivityLog.UserID = 1
            VoucherMaster.ActivityLog.LogDateTime = Date.Now
            Call New VouchersDAL().Delete(VoucherMaster, trans)
            If Not trans.Connection Is Nothing Then
                trans.Commit()
            End If
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAllRecord(Optional ByVal Condition As String = "") As DataTable
        Try
            Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(UtilityDAL.GetConfigValue("PreviouseRecordShow").ToString)
            Dim ClosingDate As DateTime = Convert.ToDateTime(UtilityDAL.GetConfigValue("EndOfDate").ToString)
            Dim str As String = String.Empty
            str = " SELECT " & IIf(Condition = "ALL", "", "TOP 50") & " SalariesExpenseMasterTable.SalaryExpId, SalariesExpenseMasterTable.SalaryExpDate, SalariesExpenseMasterTable.SalaryExpNo, " _
                  & "    SalariesExpenseMasterTable.EmployeeId, tblDefEmployee.Employee_Name, EmployeeDeptDefTable.EmployeeDeptName as Department, SalariesExpenseMasterTable.Remarks, SalariesExpenseMasterTable.CostCenterId, tblDefCostCenter.Name AS [Cost Center], " _
                  & "    Isnull(NetSalary,0) as NetSalary, SalariesExpenseMasterTable.Post, IsNull(tblDefEmployee.EmpSalaryAccountId,0) as EmpSalaryAccountId, isnull(GrossSalary,0) as GrossSalary  " _
                  & "    FROM tblDefEmployee INNER JOIN " _
                  & "    SalariesExpenseMasterTable ON tblDefEmployee.Employee_ID = SalariesExpenseMasterTable.EmployeeId LEFT OUTER JOIN " _
                  & "    tblDefCostCenter ON SalariesExpenseMasterTable.CostCenterId = tblDefCostCenter.CostCenterID LEFT JOIN EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptID  " & IIf(PreviouseRecordShow = True, "", " WHERE (Convert(varchar, SalariesExpenseMasterTable.SalaryExpDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102)) ") & " ORDER BY SalariesExpenseMasterTable.SalaryExpNo Desc "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetFilterWise(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal EmployeeId As Integer, ByVal DepartmentId As Integer, ByVal DesignationId As Integer) As DataTable
        Try
            'Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(UtilityDAL.GetConfigValue("PreviouseRecordShow").ToString)
            'Dim ClosingDate As DateTime = Convert.ToDateTime(UtilityDAL.GetConfigValue("EndOfDate").ToString)
            Dim str As String = String.Empty
            str = " SELECT SalariesExpenseMasterTable.SalaryExpId, SalariesExpenseMasterTable.SalaryExpDate, SalariesExpenseMasterTable.SalaryExpNo, " _
                  & "    SalariesExpenseMasterTable.EmployeeId, tblDefEmployee.Employee_Name, EmployeeDeptDefTable.EmployeeDeptName as Department, SalariesExpenseMasterTable.Remarks, SalariesExpenseMasterTable.CostCenterId, tblDefCostCenter.Name AS [Cost Center], " _
                  & "    Isnull(NetSalary,0) as NetSalary, SalariesExpenseMasterTable.Post, IsNull(tblDefEmployee.EmpSalaryAccountId,0) as EmpSalaryAccountId, isnull(GrossSalary,0) as GrossSalary  " _
                  & "    FROM tblDefEmployee INNER JOIN " _
                  & "    SalariesExpenseMasterTable ON tblDefEmployee.Employee_ID = SalariesExpenseMasterTable.EmployeeId LEFT OUTER JOIN " _
                  & "    tblDefCostCenter ON SalariesExpenseMasterTable.CostCenterId = tblDefCostCenter.CostCenterID LEFT JOIN EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptID WHERE (Convert(varchar, SalariesExpenseMasterTable.SalaryExpDate, 102) Between Convert(Datetime, N'" & FromDate.ToString("yyyy-M-dd 00:00:00") & "', 102) And Convert(Datetime, N'" & ToDate.ToString("yyyy-M-dd 23:59:59") & "', 102)) "
            If EmployeeId > 0 Then
                str += " And tblDefEmployee.Employee_ID = " & EmployeeId & ""
            End If
            If DepartmentId > 0 Then
                str += " And tblDefEmployee.Dept_ID = " & DepartmentId & ""
            End If
            If DesignationId > 0 Then
                str += " And tblDefEmployee.Desig_ID =" & DesignationId & ""
            End If
            str += " ORDER BY SalariesExpenseMasterTable.SalaryExpNo Desc"
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetRecordById(ByVal Voucher_Id As Integer) As DataTable
        Try
            Dim str As String = String.Empty
            str = " SELECT SalaryExpenseType.SalaryExpTypeId, SalaryExpenseType.SalaryExpType, ISNULL(SalariesDetail.Earning, 0) AS Earning, ISNULL(SalariesDetail.Deduction, 0) AS Deduction, SalaryExpenseType.SalaryDeduction, Isnull(SalaryExpenseType.AccountId,0) as AccountId, " _
                  & " ISNULL(SalaryExpenseType.flgAdvance,0) as Advance FROM  SalaryExpenseType LEFT OUTER JOIN (SELECT SalaryExpId, SalaryExpTypeId, ISNULL(SalariesExpenseDetailTable.Earning, 0) AS Earning, ISNULL(SalariesExpenseDetailTable.Deduction, 0) AS Deduction FROM SalariesExpenseDetailTable WHERE (SalariesExpenseDetailTable.SalaryExpId = " & Voucher_Id & ")) SalariesDetail ON SalaryExpenseType.SalaryExpTypeId = SalariesDetail.SalaryExpTypeId "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetEmpReceiveableAccountId(ByVal EmpId As Integer, ByVal SalaryType As Integer, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As Int16
        Try

            'Changed By Imran Ali
            '26-6-2013

            Dim str As String = String.Empty
            'str= "Select Isnull(ReceiveableAccountId,0) as ReceiveableAccountId From tblDefEmployee WHERE Employee_ID=" & EmpId
            str = "Select isnull(Account_Id,0) as ReceiveableAccountId From tblEmployeeAccounts WHERE Employee_Id=" & EmpId & " AND Type_ID=" & SalaryType & ""
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function GetDocumentNo(dtpPODate As DateTime, trans As SqlTransaction) As String
        Try
            'If Me.txtPONo.Text = "" Then
            If UtilityDAL.GetConfigValue("VoucherNo", trans).ToString = "Yearly" Then
                Return UtilityDAL.GetSerialNo("ES" + "-" + Microsoft.VisualBasic.Right(dtpPODate.Year, 2) + "-", "SalariesExpenseMasterTable", "SalaryExpNo", trans)
            ElseIf UtilityDAL.GetConfigValue("VoucherNo", trans).ToString = "Monthly" Then
                Return UtilityDAL.GetNextDocNo("ES" & "-" & Format(dtpPODate, "yy") & dtpPODate.Month.ToString("00"), 4, "SalariesExpenseMasterTable", "SalaryExpNo", trans)
            Else
                Return UtilityDAL.GetNextDocNo("ES", 6, "SalariesExpenseMasterTable", "SalaryExpNo", trans)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetFilterWise(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal EmployeeId As Integer, ByVal DepartmentId As Integer, ByVal DesignationId As Integer, ByVal IsFromDate As Boolean, ByVal IsToDate As Boolean) As DataTable
        Try
            'Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(UtilityDAL.GetConfigValue("PreviouseRecordShow").ToString)
            'Dim ClosingDate As DateTime = Convert.ToDateTime(UtilityDAL.GetConfigValue("EndOfDate").ToString)
            Dim str As String = String.Empty
            str = " SELECT SalariesExpenseMasterTable.SalaryExpId, SalariesExpenseMasterTable.SalaryExpDate, SalariesExpenseMasterTable.SalaryExpNo, " _
                  & "    SalariesExpenseMasterTable.EmployeeId, tblDefEmployee.Employee_Name, EmployeeDeptDefTable.EmployeeDeptName as Department, SalariesExpenseMasterTable.Remarks, SalariesExpenseMasterTable.CostCenterId, tblDefCostCenter.Name AS [Cost Center], " _
                  & "    Isnull(NetSalary,0) as NetSalary, SalariesExpenseMasterTable.Post, IsNull(tblDefEmployee.EmpSalaryAccountId,0) as EmpSalaryAccountId, isnull(GrossSalary,0) as GrossSalary  " _
                  & "    FROM tblDefEmployee INNER JOIN " _
                  & "    SalariesExpenseMasterTable ON tblDefEmployee.Employee_ID = SalariesExpenseMasterTable.EmployeeId LEFT OUTER JOIN " _
                  & "    tblDefCostCenter ON SalariesExpenseMasterTable.CostCenterId = tblDefCostCenter.CostCenterID LEFT JOIN EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptID Where tblDefEmployee.Employee_ID > 0 "
            ''WHERE (Convert(varchar, SalariesExpenseMasterTable.SalaryExpDate, 102) Between Convert(Datetime, N'" & FromDate.ToString("yyyy-M-dd 00:00:00") & "', 102) And Convert(Datetime, N'" & ToDate.ToString("yyyy-M-dd 23:59:59") & "', 102)) "
            If IsFromDate = True Then
                str += " And (Convert(varchar, SalariesExpenseMasterTable.SalaryExpDate, 102) >= Convert(Datetime, N'" & FromDate.ToString("yyyy-M-dd 00:00:00") & "', 102))"
            End If
            If IsToDate = True Then
                str += " And (Convert(varchar, SalariesExpenseMasterTable.SalaryExpDate, 102) <= Convert(Datetime, N'" & ToDate.ToString("yyyy-M-dd 23:59:59") & "', 102))"
            End If
            If EmployeeId > 0 Then
                str += " And tblDefEmployee.Employee_ID = " & EmployeeId & ""
            End If
            If DepartmentId > 0 Then
                str += " And tblDefEmployee.Dept_ID = " & DepartmentId & ""
            End If
            If DesignationId > 0 Then
                str += " And tblDefEmployee.Desig_ID =" & DesignationId & ""
            End If
            str += " ORDER BY SalariesExpenseMasterTable.SalaryExpNo Desc"
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
