Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class LeadActivityDAL
    Function Add(ByVal objModel As LeadActivityBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Add(ByVal objModel As LeadActivityBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  LeadActivity (LeadId, LeadContactId, LeadOfficeId, LeadActivityTypeID, ActivityDate, ActivityTime, IsConfirmed, ResponsiblePerson_Employee_Id, InsideSalesPerson_Employee_Id, Manager_Employee_Id, Objective, Address, ProjectId) values (N'" & objModel.LeadId & "', N'" & objModel.LeadContactId & "', N'" & objModel.LeadOfficeId & "', N'" & objModel.LeadActivityTypeID & "', N'" & objModel.ActivityDate & "',N'" & objModel.ActivityTime & "', N'" & objModel.IsConfirmed & "', N'" & objModel.ResponsiblePerson_Employee_Id & "', N'" & objModel.InsideSalesPerson_Employee_Id & "', N'" & objModel.Manager_Employee_Id & "', N'" & objModel.Objective.Replace("'", "''") & "', N'" & objModel.Address.Replace("'", "''") & "', N'" & objModel.ProjectId & "') Select @@Identity"
            objModel.ActivityId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Lead Activity"
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "CRM"
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As LeadActivityBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As LeadActivityBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update LeadActivity set LeadId= N'" & objModel.LeadId & "', LeadContactId= N'" & objModel.LeadContactId & "', LeadOfficeId= N'" & objModel.LeadOfficeId & "', LeadActivityTypeID= N'" & objModel.LeadActivityTypeID & "', ActivityDate= N'" & objModel.ActivityDate & "', ActivityTime= N'" & objModel.ActivityTime & "', IsConfirmed= N'" & objModel.IsConfirmed & "', ResponsiblePerson_Employee_Id= N'" & objModel.ResponsiblePerson_Employee_Id & "', InsideSalesPerson_Employee_Id= N'" & objModel.InsideSalesPerson_Employee_Id & "', Manager_Employee_Id= N'" & objModel.Manager_Employee_Id & "', Objective= N'" & objModel.Objective.Replace("'", "''") & "', Address= N'" & objModel.Address.Replace("'", "''") & "', ProjectId= N'" & objModel.ProjectId & "' where ActivityId=" & objModel.ActivityId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.UserID = LoginUser.LoginUserId
            objModel.ActivityLog.FormCaption = "Lead Activity"
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "CRM"
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As LeadActivityBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal objModel As LeadActivityBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from LeadActivity  where ActivityId= " & objModel.ActivityId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.UserID = LoginUser.LoginUserId
            'objModel.ActivityLog.FormCaption = "Lead Activity"
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "CRM"
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ActivityId, LeadId, LeadContactId, LeadOfficeId, LeadActivityTypeID, ActivityDateTime, IsConfirmed, ResponsiblePerson_Employee_Id, InsideSalesPerson_Employee_Id, Manager_Employee_Id, Objective, ProjectId from LeadActivity  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ActivityId, LeadId, LeadContactId, LeadOfficeId, LeadActivityTypeID, ActivityDate, ActivityTime, IsConfirmed, ResponsiblePerson_Employee_Id, InsideSalesPerson_Employee_Id, Manager_Employee_Id, Objective, Address, ProjectId from LeadActivity  where ActivityId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
