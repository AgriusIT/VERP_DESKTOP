'2015-06-22 Task#2015060025 to only load Active Employees Ali Ansari
''20-01-2018 Muhammad Ameen TASK TFS2170. Addition and handling of two new fields like Production Stages and Labour Type
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class dailysalariesdal
    Dim Voucher As VouchersMaster
    Dim VoucherDetail As VouchersDetail
    Dim VoucherDebit As VouchersDetail
    Dim VoucherCredit As VouchersDetail
    Public Function GetRecordbyId(ByVal dailysalaryid As Integer) As DataTable
        Try
            'EmployeeId()
            'CostCenterId()
            'ProductionStageId()
            'ProductionStage()
            'EmployeeName()
            'LabourTypeId()
            'LabourType()
            'WageType()
            'Rate()
            'Time()
            'Amount()
            'Remarks()


            Dim _strqurry As String
            _strqurry = "SELECT dbo.tblDefEmployee.Employee_ID AS EmployeeId, DailySalariesDetailtbl.CostCenterId, IsNull(DailySalariesDetailtbl.ProductionStageId, 0) AS ProductionStageId, ProductionStep.prod_step AS ProductionStage, dbo.tblDefEmployee.Employee_Name AS EmployeeName, " _
                & "      IsNull(DailySalariesDetailtbl.LabourTypeId, 0) AS LabourTypeId, LabourType.LabourType, dbo.DailySalariesDetailtbl.DailyWage AS ChargeType, dbo.DailySalariesDetailtbl.Salary AS Rate, dbo.DailySalariesDetailtbl.WorkingTime AS Unit,  " _
                  & "    0 AS Amount, DailySalariesDetailtbl.Remarks " _
                     & "  FROM         dbo.DailySalariesmastertbl INNER JOIN " _
                   & "          dbo.DailySalariesDetailtbl ON dbo.DailySalariesmastertbl.DailySalariesId = dbo.DailySalariesDetailtbl.DailySalariesId INNER JOIN " _
                & "         dbo.tblDefEmployee ON dbo.DailySalariesDetailtbl.EmployId = dbo.tblDefEmployee.Employee_ID LEFT OUTER JOIN " _
              & "          dbo.tblDefCostCenter ON dbo.DailySalariesDetailtbl.CostCenterId = dbo.tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
              & "          dbo.tblProSteps As ProductionStep ON dbo.DailySalariesDetailtbl.ProductionStageId = ProductionStep.ProdStep_Id LEFT OUTER JOIN " _
              & "          dbo.tblLabourType As LabourType ON dbo.DailySalariesDetailtbl.LabourTypeId = LabourType.Id  " _
            & " WHERE DailySalariesmastertbl.DailySalariesId=" & dailysalaryid & " ORDER BY DailySalariesDetailtbl.DailySalariesDetailId asc"
            Return SBDal.UtilityDAL.GetDataTable(_strqurry)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Adddailysalary(ByVal dailysalary As SBModel.dailysalarymaster) As Boolean
        Dim con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not con.State = 1 Then con.Open()
        Dim trans As SqlClient.SqlTransaction = con.BeginTransaction

        Try
            Dim _strqurry As String
            _strqurry = "insert into dailysalariesmastertbl (DcNo, DcDate, Amount, Reference, posted, UserName,  EntryDate) values (N'" & dailysalary.DcNo & "',N'" & dailysalary.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & dailysalary.Amount & "', N'" & dailysalary.Reference & "', " & IIf(dailysalary.posted = True, 1, 0) & ",N'" & dailysalary.UserName & "',N'" & dailysalary.EntryDate.ToString("yyyy-M-d h:mm:ss tt") & "')Select @@identity "
            dailysalary.DailySalariesId = SQLHelper.ExecuteScaler(trans, CommandType.Text, _strqurry)
            adddailysalarydetail(dailysalary, dailysalary.DailySalariesId, trans)

            Voucher = New VouchersMaster
            Voucher.VoucherId = 0
            Voucher.LocationId = 0
            Voucher.VNo = dailysalary.DcNo
            Voucher.VoucherCode = dailysalary.DcNo
            Voucher.FinancialYearId = 1
            Voucher.VoucherTypeId = 1
            Voucher.VoucherNo = dailysalary.DcNo
            Voucher.VoucherDate = dailysalary.DcDate.ToString("yyyy-M-d h:mm:ss tt")
            Voucher.ChequeDate = Nothing
            Voucher.ChequeNo = "NULL"
            Voucher.CoaDetailId = 0
            Voucher.VoucherMonth = Date.Now.Month
            Voucher.Post = dailysalary.posted
            Voucher.Source = "frmDailySalaries"
            Voucher.References = dailysalary.Reference
            Voucher.UserName = dailysalary.UserName
            Voucher.VoucherDatail = New List(Of VouchersDetail)

            Dim DailySalaryDetailList As List(Of DailySalarydetail) = dailysalary.DailySalaryDetail
            For Each SalaryList As DailySalarydetail In DailySalaryDetailList
                VoucherDebit = New VouchersDetail
                VoucherDebit.VoucherId = 0
                VoucherDebit.LocationId = 0
                VoucherDebit.CoaDetailId = UtilityDAL.GetConfigValue("SalariesAccountId")
                VoucherDebit.Comments = SalaryList.Remarks
                VoucherDebit.DebitAmount = (Val(SalaryList.Salary) * Val(SalaryList.WorkingTime))
                VoucherDebit.CreditAmount = 0
                VoucherDebit.CostCenter = SalaryList.CostCenterId
                VoucherDebit.SPReference = String.Empty
                VoucherDebit.Cheque_No = String.Empty
                VoucherDebit.Cheque_Date = Nothing
                Voucher.VoucherDatail.Add(VoucherDebit)

                VoucherCredit = New VouchersDetail
                VoucherCredit.VoucherId = 0
                VoucherCredit.LocationId = 0
                VoucherCredit.CoaDetailId = UtilityDAL.GetConfigValue("SalariesPayableAccountId")
                VoucherCredit.Comments = SalaryList.Remarks
                VoucherCredit.DebitAmount = 0
                VoucherCredit.CreditAmount = (Val(SalaryList.Salary) * Val(SalaryList.WorkingTime))
                VoucherCredit.CostCenter = SalaryList.CostCenterId
                VoucherCredit.SPReference = String.Empty
                VoucherCredit.Cheque_No = String.Empty
                VoucherCredit.Cheque_Date = Nothing
                Voucher.VoucherDatail.Add(VoucherCredit)
            Next

            Voucher.ActivityLog = New ActivityLog
            Voucher.ActivityLog.ActivityName = "Save"
            Voucher.ActivityLog.RecordType = "Accounts"
            Voucher.ActivityLog.RefNo = dailysalary.DcNo
            Voucher.ActivityLog.ApplicationName = "Salaries Expense"
            Voucher.ActivityLog.FormCaption = "Salaries Expense"
            Voucher.ActivityLog.UserID = 1
            Voucher.ActivityLog.LogDateTime = Date.Now
            Voucher.ActivityLog.FormName = "frmDailySalaries"

            Call New VouchersDAL().Add(Voucher, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function adddailysalarydetail(ByVal dailysalary As SBModel.dailysalarymaster, ByVal MasterId As Integer, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Try

            Dim DailySalaryDetailList As List(Of DailySalarydetail) = dailysalary.DailySalaryDetail
            Dim _strqurry As String
            For Each SalaryList As DailySalarydetail In DailySalaryDetailList
                _strqurry = "insert into dailySalariesDetailtbl (DailySalariesId, EmployId, CostCenterId ,DailyWage, Salary ,WorkingTime, Remarks, ProductionStageId, LabourTypeId) values (" & MasterId & ", " & SalaryList.EmployId & ", " & SalaryList.CostCenterId & ", N'" & SalaryList.DailyWage & "', " & SalaryList.Salary & ",  " & SalaryList.WorkingTime & ", N'" & SalaryList.Remarks & "', " & SalaryList.ProductionStepId & ", " & SalaryList.LabourTypeId & " )"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, _strqurry)
            Next
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function UpdateDailySalary(ByVal DailySalary As dailysalarymaster) As Boolean
        Dim con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not con.State = 1 Then con.Open()
        Dim trans As SqlClient.SqlTransaction = con.BeginTransaction
        Try

            Dim VoucherId As Integer = Convert.ToInt32(UtilityDAL.GetVoucherIdByVoucherNo("" & DailySalary.DcNo & ""))
            Dim str As String = String.Empty
            str = "Update DailySalariesmastertbl set DcNo=N'" & DailySalary.DcNo & "', DcDate=N'" & DailySalary.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "', Amount= " & DailySalary.Amount & ", posted=" & IIf(DailySalary.posted = True, 1, 0) & ", UserName=N'" & DailySalary.UserName & "', EntryDate=N'" & DailySalary.EntryDate.ToString("yyyy-M-d h:mm:ss tt") & "'  WHERE DailySalariesId=N'" & DailySalary.DailySalariesId & "' "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = String.Empty
            str = "Delete From DailySalariesDetailtbl WHERE  DailySalariesId=N'" & DailySalary.DailySalariesId & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = String.Empty
            str = "Delete From tblVoucherDetail WHERE voucher_Id=" & VoucherId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'str = String.Empty
            'str = "Delete From tblVoucher WHERE voucher_Id=" & VoucherId
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            adddailysalarydetail(DailySalary, DailySalary.DailySalariesId, trans)
            Voucher = New VouchersMaster
            Voucher.VoucherId = 0
            Voucher.LocationId = 0
            Voucher.VNo = DailySalary.DcNo
            Voucher.VoucherCode = DailySalary.DcNo
            Voucher.FinancialYearId = 1
            Voucher.VoucherTypeId = 1
            Voucher.VoucherNo = DailySalary.DcNo
            Voucher.VoucherDate = DailySalary.DcDate.ToString("yyyy-M-d h:mm:ss tt")
            Voucher.ChequeDate = Nothing
            Voucher.ChequeNo = "NULL"
            Voucher.CoaDetailId = 0
            Voucher.VoucherMonth = Date.Now.Month
            Voucher.Post = DailySalary.posted
            Voucher.Source = "frmDailySalaries"
            Voucher.References = DailySalary.Reference
            Voucher.UserName = DailySalary.UserName
            Voucher.VoucherDatail = New List(Of VouchersDetail)

            Dim DailySalaryDetailList As List(Of DailySalarydetail) = DailySalary.DailySalaryDetail
            For Each SalaryList As DailySalarydetail In DailySalaryDetailList
                VoucherDebit = New VouchersDetail
                VoucherDebit.VoucherId = 0
                VoucherDebit.LocationId = 0
                VoucherDebit.CoaDetailId = UtilityDAL.GetConfigValue("SalariesAccountId")
                VoucherDebit.Comments = SalaryList.Remarks
                VoucherDebit.DebitAmount = (Val(SalaryList.Salary) * Val(SalaryList.WorkingTime))
                VoucherDebit.CreditAmount = 0
                VoucherDebit.CostCenter = SalaryList.CostCenterId
                VoucherDebit.SPReference = String.Empty
                VoucherDebit.Cheque_No = String.Empty
                VoucherDebit.Cheque_Date = Nothing
                Voucher.VoucherDatail.Add(VoucherDebit)

                VoucherCredit = New VouchersDetail
                VoucherCredit.VoucherId = 0
                VoucherCredit.LocationId = 0
                VoucherCredit.CoaDetailId = UtilityDAL.GetConfigValue("SalariesPayableAccountId")
                VoucherCredit.Comments = SalaryList.Remarks
                VoucherCredit.DebitAmount = 0
                VoucherCredit.CreditAmount = (Val(SalaryList.Salary) * Val(SalaryList.WorkingTime))
                VoucherCredit.CostCenter = SalaryList.CostCenterId
                VoucherCredit.SPReference = String.Empty
                VoucherCredit.Cheque_No = String.Empty
                VoucherCredit.Cheque_Date = Nothing
                Voucher.VoucherDatail.Add(VoucherCredit)
            Next

            Voucher.ActivityLog = New ActivityLog
            Voucher.ActivityLog.ActivityName = "Save"
            Voucher.ActivityLog.RecordType = "Accounts"
            Voucher.ActivityLog.RefNo = DailySalary.DcNo
            Voucher.ActivityLog.ApplicationName = "Salaries Expense"
            Voucher.ActivityLog.FormCaption = "Salaries Expense"
            Voucher.ActivityLog.UserID = 1
            Voucher.ActivityLog.LogDateTime = Date.Now

            Call New VouchersDAL().Update(Voucher, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function DeleteDailySalary(ByVal DailySalary As dailysalarymaster) As Boolean
        Dim con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not con.State = 1 Then con.Open()
        Dim trans As SqlClient.SqlTransaction = con.BeginTransaction
        Try
            Dim VoucherId As Integer = Convert.ToInt32(UtilityDAL.GetVoucherIdByVoucherNo("" & DailySalary.DcNo & "").ToString)
            Dim str As String = String.Empty
            str = "Delete From DailySalariesDetailtbl WHERE  DailySalariesId=N'" & DailySalary.DailySalariesId & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = String.Empty
            str = "Delete From DailySalariesmastertbl WHERE  DailySalariesId=N'" & DailySalary.DailySalariesId & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = String.Empty
            str = "Delete From tblVoucherDetail WHERE Voucher_Id=" & VoucherId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = String.Empty
            str = "Delete From tblVoucher WHERE Voucher_Id=" & VoucherId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            Voucher = New VouchersMaster
            Voucher.ActivityLog = New ActivityLog
            Voucher.ActivityLog.ActivityName = "Delete"
            Voucher.ActivityLog.RecordType = "Accounts"
            Voucher.ActivityLog.RefNo = DailySalary.DcNo
            Voucher.ActivityLog.ApplicationName = "Salaries Expense"
            Voucher.ActivityLog.FormCaption = "Salaries Expense"
            Voucher.ActivityLog.UserID = 1
            Voucher.ActivityLog.LogDateTime = Date.Now
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function GetCostCenter() As String
        Try
            Dim str As String = "select CostCenterId, Name as CostCenter From tblDefCostCenter"
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetEmployee() As String
        Try
            'Marked Against Task#2015060025 to only load Active Employees Ali Ansari
            'Dim str As String = "SELECT dbo.tblDefEmployee.Employee_ID, dbo.tblDefEmployee.Employee_Name, dbo.EmployeeDesignationDefTable.EmployeeDesignationName, " _
            ' & "                EmployeeDeptDefTable.EmployeeDeptName, tblDefEmployee.Salary " _
            ' & "              FROM tblDefEmployee INNER JOIN " _
            ' & "         EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId INNER JOIN " _
            ' & "         EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId"
            'Marked Against Task#2015060025 to only load Active Employees Ali Ansari
            'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
            'TASK-332 
            Dim str As String = "SELECT dbo.tblDefEmployee.Employee_ID, dbo.tblDefEmployee.Employee_Name, dbo.tblDefEmployee.Father_Name, dbo.EmployeeDesignationDefTable.EmployeeDesignationName, " _
             & "                EmployeeDeptDefTable.EmployeeDeptName, tblDefEmployee.Salary " _
             & "              FROM tblDefEmployee INNER JOIN " _
             & "         EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId INNER JOIN " _
             & "         EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId where tblDefEmployee.active = 1 And tblDefEmployee.IsDailyWages = 1"
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetEmployeeById(ByVal EmpId As Integer) As DataTable
        Try
            Dim str As String = "SELECT dbo.tblDefEmployee.Employee_ID, dbo.tblDefEmployee.Employee_Name, dbo.EmployeeDesignationDefTable.EmployeeDesignationName, " _
             & "                EmployeeDeptDefTable.EmployeeDeptName, tblDefEmployee.Salary " _
             & "              FROM tblDefEmployee INNER JOIN " _
             & "            EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId INNER JOIN " _
             & "         EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId " _
             & " WHERE (tblDefEmployee.Employee_ID=" & EmpId & ")"
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAllRecords(Optional ByVal Condition As String = "") As DataTable
        Try
            Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(UtilityDAL.GetConfigValue("PreviouseRecordShow").ToString)
            Dim ClosingDate As DateTime = Convert.ToDateTime(UtilityDAL.GetConfigValue("EndOfDate").ToString)
            Dim str As String = String.Empty
            str = "Select " & IIf(Condition = "All", "", "Top 50") & " DailySalariesId, DcNo, DcDate, Reference, Amount, CASE WHEN Posted=1 then 'Posted' else 'UnPosted' end as Post From DailySalariesmastertbl " & IIf(PreviouseRecordShow = True, "", " WHERE (Convert(varchar, DCDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102)) ") & " ORDER BY DcNo Desc"
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
