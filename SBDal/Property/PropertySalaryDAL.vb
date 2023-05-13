Imports SBModel
Imports System
Imports System.Data.SqlClient
Public Class PropertySalaryDAL

    Dim VoucherId As Integer = 0I
    Function Add(ByVal objModel As PropertySalaryBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, trans)
            AddVoucher(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Add(ByVal objModel As PropertySalaryBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO PropertySalary (SalaryDate, SalaryMonth, SalaryYear, SalaryNo, Remarks, UserName) VALUES ('" & objModel.SalaryDate & "', " & objModel.SalaryMonth & ", " & objModel.SalaryYear & ", N'" & objModel.SalaryNo.Replace("'", "''") & "', N'" & objModel.Remarks.Replace("'", "''") & "', N'" & objModel.UserName & "') Select @@Identity"
            objModel.SalaryId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Property Salary"
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.SalaryNo.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function AddVoucher(ByVal objModel As PropertySalaryBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date,post,Source,voucher_code,Remarks) Values(1,1,9,N'" & objModel.SalaryNo & "',N'" & objModel.SalaryDate & "',1,N'frmProSalarySheet',N'" & objModel.SalaryNo & "',N'" & objModel.Remarks & "') Select @@Identity"
            objModel.VoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            AddVoucherDetail(objModel, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function AddVoucherDetail(ByVal objModel As PropertySalaryBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each obj As PropertySalaryDetailBE In objModel.Detail
                If obj.Salary > 0 Then
                    'Debit Salary Expense Account
                    strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, EmpID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & objModel.VoucherId & ",1," & obj.ExpenseAccountId & "," & obj.Salary & ",0," & obj.CostCenterId & "," & obj.EmployeeId & ",N'" & objModel.Remarks & "'," & obj.Salary & ",0,1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    'Credit Salary Payable Account
                    strSQL = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, CostCenterID, EmpID, comments, Currency_Debit_Amount, Currency_Credit_Amount, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate) Values(" & objModel.VoucherId & ",1," & obj.AccountId & ",0," & obj.Salary & "," & obj.CostCenterId & "," & obj.EmployeeId & ",'Salary Paid to " & obj.EmployeeName & "',0," & obj.Salary & ",1,1,1,1)"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As PropertySalaryBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            DeleteVoucher(objModel, trans)
            AddVoucher(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As PropertySalaryBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "UPDATE PropertySalary SET SalaryDate= '" & objModel.SalaryDate & "', SalaryMonth= " & objModel.SalaryMonth & ", SalaryYear= " & objModel.SalaryYear & ", SalaryNo= N'" & objModel.SalaryNo.Replace("'", "''") & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "', UserName= N'" & objModel.UserName & "' WHERE SalaryId=" & objModel.SalaryId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Property Salary"
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = objModel.SalaryNo.ToString
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal SalaryId As Integer, ByVal VoucherId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(SalaryId, trans)
            DeleteVoucher(VoucherId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal SalaryId As Integer, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "DELETE FROM PropertySalary WHERE SalaryId= " & SalaryId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function DeleteVoucher(ByVal objModel As PropertySalaryBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "DELETE FROM tblVoucherDetail WHERE voucher_id= " & objModel.VoucherId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "DELETE FROM tblVoucher WHERE voucher_id= " & objModel.VoucherId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function DeleteVoucher(ByVal VoucherId As Integer, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "DELETE FROM tblVoucherDetail WHERE voucher_id= " & VoucherId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "DELETE FROM tblVoucher WHERE voucher_id= " & VoucherId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT PropertySalary.SalaryId, PropertySalary.SalaryDate, PropertySalary.SalaryNo, CASE WHEN PropertySalary.SalaryMonth = 1 THEN 'January' WHEN PropertySalary.SalaryMonth = 2 THEN 'February' WHEN PropertySalary.SalaryMonth = 3 THEN 'March' WHEN PropertySalary.SalaryMonth = 4 THEN 'April' WHEN PropertySalary.SalaryMonth = 5 THEN 'May' WHEN PropertySalary.SalaryMonth = 6 THEN 'June' WHEN PropertySalary.SalaryMonth = 7 THEN 'July' WHEN PropertySalary.SalaryMonth = 8 THEN 'August' WHEN PropertySalary.SalaryMonth = 9 THEN 'September' WHEN PropertySalary.SalaryMonth = 10 THEN 'October' WHEN PropertySalary.SalaryMonth = 11 THEN 'November' WHEN PropertySalary.SalaryMonth = 12 THEN 'December' END AS SalaryMonth, PropertySalary.SalaryYear, PropertySalary.Remarks, PropertySalary.UserName FROM PropertySalary ORDER BY 1 DESC"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT PropertySalary.SalaryId, PropertySalary.SalaryDate, PropertySalary.SalaryMonth, PropertySalary.SalaryYear, PropertySalary.SalaryNo, PropertySalary.Remarks, PropertySalary.UserName FROM PropertySalary WHERE PropertySalary.SalaryId = " & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetSalaryRecords() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT EmployeesView.Employee_ID AS EmployeeId, EmployeesView.Employee_Code AS EmployeeCode, EmployeesView.Employee_Name AS EmployeeName, ISNULL(EmployeesView.EmpSalaryAccountId,0) AS AccountId, ISNULL(tblCOAMainSubSubDetail.detail_code,'') AS AccountCode, EmployeesView.Mobile, EmployeesView.Dept_ID AS DepartmentId, EmployeesView.EmployeeDeptName AS Department, EmployeesView.Desig_ID AS DesignationId, EmployeesView.EmployeeDesignationName AS Designation, EmployeesView.ShiftGroupName AS Shift, ISNULL(EmployeesView.CostCentre,0) AS CostCenterId, ISNULL(tblDefCostCenter.Name,'') AS CostCenter, ISNULL(EmployeesView.Salary,0) AS Salary, ISNULL(EmployeesView.SalaryExpAcID,0) AS ExpenseAccountId FROM EmployeesView LEFT OUTER JOIN tblDefCostCenter ON EmployeesView.CostCentre = tblDefCostCenter.CostCenterID LEFT OUTER JOIN tblCOAMainSubSubDetail ON EmployeesView.EmpSalaryAccountId = tblCOAMainSubSubDetail.coa_detail_id WHERE EmployeesView.Active = 1"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetVoucherRecords(ByVal objModel As PropertySalaryBE) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT EmployeesView.Employee_ID AS EmployeeId, EmployeesView.Employee_Code AS EmployeeCode, EmployeesView.Employee_Name AS EmployeeName, ISNULL(EmployeesView.EmpSalaryAccountId, 0) AS AccountId, ISNULL(tblCOAMainSubSubDetail.detail_code, '') AS AccountCode, EmployeesView.Mobile, EmployeesView.Dept_ID AS DepartmentId, EmployeesView.EmployeeDeptName AS Department, EmployeesView.Desig_ID AS DesignationId, EmployeesView.EmployeeDesignationName AS Designation, EmployeesView.ShiftGroupName AS Shift, ISNULL(EmployeesView.CostCentre, 0) AS CostCenterId, ISNULL(tblDefCostCenter.Name, '') AS CostCenter, ISNULL(Salary.Salary, 0) AS Salary, ISNULL(EmployeesView.SalaryExpAcID, 0) AS ExpenseAccountId FROM EmployeesView INNER JOIN (SELECT ISNULL(debit_amount, 0) AS Salary, EmpID FROM tblVoucherDetail WHERE (voucher_id = " & objModel.VoucherId & ") AND (debit_amount > 0)) AS Salary ON EmployeesView.Employee_ID = Salary.EmpID LEFT OUTER JOIN tblDefCostCenter ON EmployeesView.CostCentre = tblDefCostCenter.CostCenterID LEFT OUTER JOIN tblCOAMainSubSubDetail ON EmployeesView.EmpSalaryAccountId = tblCOAMainSubSubDetail.coa_detail_id WHERE (EmployeesView.Active = 1)"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class