'Ali Faisal : TFS1481 : DAL for save, update and delete functions
Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class LeaveTypesDAL
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Insert records
    ''' </summary>
    ''' <param name="Leaves"></param>
    ''' <returns></returns>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Public Function Save(ByVal Leaves As LeaveTypesBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert records
            str = "Insert Into tblDefLeaveTypes (CompanyId, CostCenterId, EmpTypeId, LeaveTypeTitle, LeaveCatagoryId, LeaveType, LeaveAccrual, LeaveApproval, GenderRestriction, LeavesInCashment, AllowedPerYear, CarriedForward, CarriedForwardDays, SortOrder, Active) " _
                & "Values (" & Leaves.CompanyId & "," & Leaves.CostCenterId & "," & Leaves.EmpTypeId & ",N'" & Leaves.LeaveTypeTitle & "'," & Leaves.LeaveCatagoryId & ",N'" & Leaves.LeaveType & "',N'" & Leaves.LeaveAccrual & "',N'" & Leaves.LeaveApproval & "',N'" & Leaves.GenderRestriction & "','" & Leaves.LeavesInCashment & "','" & Leaves.AllowedPerYear & "','" & Leaves.CarriedForward & "','" & Leaves.CarriedForwardDays & "','" & Leaves.SortOrder & "','" & Leaves.Active & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Update records
    ''' </summary>
    ''' <param name="Leaves"></param>
    ''' <returns></returns>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Public Function Update(ByVal Leaves As LeaveTypesBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update records
            str = "Update tblDefLeaveTypes Set CompanyId = " & Leaves.CompanyId & ", CostCenterId = " & Leaves.CostCenterId & ", EmpTypeId = " & Leaves.EmpTypeId & ", LeaveTypeTitle = N'" & Leaves.LeaveTypeTitle & "', LeaveCatagoryId = " & Leaves.LeaveCatagoryId & ", LeaveType = N'" & Leaves.LeaveType & "', LeaveAccrual = N'" & Leaves.LeaveAccrual & "', LeaveApproval = N'" & Leaves.LeaveApproval & "', GenderRestriction = N'" & Leaves.GenderRestriction & "', LeavesInCashment = '" & Leaves.LeavesInCashment & "', AllowedPerYear = '" & Leaves.AllowedPerYear & "', CarriedForward = '" & Leaves.CarriedForward & "', CarriedForwardDays = '" & Leaves.CarriedForwardDays & "', SortOrder = '" & Leaves.SortOrder & "', Active = '" & Leaves.Active & "' Where Id = " & Leaves.Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Delete records
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks>Ali Faisal : TFS1481 : 20-Sep-2017</remarks>
    Public Function Delete(ByVal Id As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete records
            str = "Delete from tblDefLeaveTypes Where Id = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
End Class
