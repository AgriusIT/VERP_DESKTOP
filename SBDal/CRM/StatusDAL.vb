Imports SBDal
Imports SBModel
Imports SBUtility
Public Class StatusDAL
    Public Function Add(ByVal ObjMod As Status) As Boolean
        Dim str As String = String.Empty
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            str = "Insert Into tblDefStatus Values(N'" & ObjMod.StatusName.Trim.Replace("'", "''") & "', N'" & ObjMod.StatusRemarks.Trim.Replace("'", "''") & "', " & ObjMod.SortOrder & ", " & ObjMod.Active & ") Select @@Identity"
            ObjMod.StatusId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            ObjMod.ActivitLog.ActivityName = "CRM"
            ObjMod.ActivitLog.RecordType = "Save"
            ObjMod.ActivitLog.RefNo = ObjMod.StatusId
            UtilityDAL.BuildActivityLog(ObjMod.ActivitLog, trans)
            trans.Commit()
        Catch ex As SqlClient.SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal ObjMod As Status) As Boolean
        Dim str As String = String.Empty
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            str = "Update tblDefStatus Set Name=N'" & ObjMod.StatusName.Trim.Replace("'", "''") & "', Remarks=N'" & ObjMod.StatusRemarks.Trim.Replace("'", "'") & "', SortOrder=" & ObjMod.SortOrder & ", Active=" & ObjMod.Active & " WHERE StatusID=" & ObjMod.StatusId & ""
            ObjMod.StatusId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            ObjMod.ActivitLog.ActivityName = "CRM"
            ObjMod.ActivitLog.RecordType = "Update"
            ObjMod.ActivitLog.RefNo = ObjMod.StatusId
            UtilityDAL.BuildActivityLog(ObjMod.ActivitLog, trans)
            trans.Commit()
        Catch ex As SqlClient.SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal ObjMod As Status) As Boolean
        Dim str As String = String.Empty
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            str = "Delete From tblDefStatus WHERE StatusId=" & ObjMod.StatusId & ""
            ObjMod.StatusId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            ObjMod.ActivitLog.ActivityName = "CRM"
            ObjMod.ActivitLog.RecordType = "Delete"
            ObjMod.ActivitLog.RefNo = ObjMod.StatusId
            UtilityDAL.BuildActivityLog(ObjMod.ActivitLog, trans)
            trans.Commit()
        Catch ex As SqlClient.SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAllRecords() As DataTable
        Dim dt As New DataTable
        Dim da As SqlClient.SqlDataAdapter
        Dim str As String = String.Empty
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Try
            str = "Select * From tblDefStatus"
            da = New SqlClient.SqlDataAdapter(str, Con)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function


End Class
