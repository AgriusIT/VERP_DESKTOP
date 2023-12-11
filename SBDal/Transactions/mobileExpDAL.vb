Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal
Public Class mobileExpDAL
    Public Function save(ByVal MOB As List(Of mobileExpBE)) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim str As String = String.Empty
            For Each mobExp As mobileExpBE In MOB
                str = "insert into tblmobileExp(mobileBill_date,month,year,employee_Id,usedBill,paidBill,limit,paidByEmp,paidByCo,userName,entryDate) values(N'" & mobExp.mobileBill_date & "',N'" & mobExp.month & "',N'" & mobExp.year & "',N'" & mobExp.employee_Id & "',N'" & mobExp.usedBill & "',N'" & mobExp.paidBill & "',N'" & mobExp.limit & "',N'" & mobExp.paidByEmp & "',N'" & mobExp.paidByCo & "',N'" & mobExp.userName & "',N'" & mobExp.entryDate & "')"
                '' SQL HELPER IS NEEDED HERE 
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            '' ROLLBACK is needed here 
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Public Function delete(ByVal month As Integer, ByVal year As Integer) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "delete from tblmobileExp where month = " & month & " and year= " & year & " "
            ''sql helper is needed here
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            ''rollBACK is needed here
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Public Function GetEmpData() As DataTable
        Try
            Dim str As String = "Select tblDefEmployee.Employee_Id as Emp_Id, Employee_Code as Emp_Code, Employee_Name as Emp_Name, Mobile as Emp_Cell, 0 as Total_Bill, 0 as Paid_Bill, ISNULL(MobileExp.Limit,0) as Limit, 0 as PayableByEmp, 0 as PayableByCompany From tblDefEmployee LEFT OUTER JOIN(Select Employee_Id, Max(Limit) as Limit From tblMobileExp Group by Employee_Id) MobileExp On MobileExp.Employee_Id = tblDefEmployee.Employee_Id WHERE Active=1"
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAllRecords(ByVal Month As Integer, ByVal Year As Integer) As DataTable
        Try
            Dim str As String = "Select Employee_Id, Isnull(usedBill,0) as Total_Bill, Isnull(paidBill,0) as Paid_Bill, Isnull(Limit,0) as Limit, (Isnull(usedBill,0)-Isnull(Limit,0)) as PayableByEmp, Isnull(PaidByCo,0) as PayableByCompany From tblMobileExp WHERE [Month]=" & Month & " AND [Year]=" & Year & ""
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
