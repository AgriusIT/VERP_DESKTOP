Imports SBDal
Imports SBModel
Imports SBUtility
Public Class TypeDAL
    Public Function Add(ByVal ObjMod As Type) As Boolean
        Dim str As String = String.Empty
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            str = "Insert Into tblDefTypes Values(N'" & ObjMod.TypeName.Trim.Replace("'", "''") & "', N'" & ObjMod.TypeRemarks.Trim.Replace("'", "''") & "', " & ObjMod.SortOrder & ", " & IIf(ObjMod.Active = True, 1, 0) & ") Select @@Identity"
            ObjMod.TypeId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            ObjMod.ActivityLog.ActivityName = "CRM"
            ObjMod.ActivityLog.RecordType = "Save"
            ObjMod.ActivityLog.RefNo = ObjMod.TypeId
            UtilityDAL.BuildActivityLog(ObjMod.ActivityLog, trans)
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
    Public Function Update(ByVal ObjMod As Type) As Boolean
        Dim str As String = String.Empty
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            str = "Update tblDefTypes Set Name=N'" & ObjMod.TypeName.Trim.Replace("'", "''") & "', Remarks=N'" & ObjMod.TypeRemarks.Trim.Replace("'", "'") & "', SortOrder=" & ObjMod.SortOrder & ", Active=" & IIf(ObjMod.Active = True, 1, 0) & " WHERE TypeId=" & ObjMod.TypeId & ""
            ObjMod.TypeId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            ObjMod.ActivityLog.ActivityName = "CRM"
            ObjMod.ActivityLog.RecordType = "Update"
            ObjMod.ActivityLog.RefNo = ObjMod.TypeId
            UtilityDAL.BuildActivityLog(ObjMod.ActivityLog, trans)
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
    Public Function Delete(ByVal ObjMod As Type) As Boolean
        Dim str As String = String.Empty
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            str = "Delete From tblDefTypes WHERE TypeId=" & ObjMod.TypeId & ""
            ObjMod.TypeId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
            ObjMod.ActivityLog.ActivityName = "CRM"
            ObjMod.ActivityLog.RecordType = "Delete"
            ObjMod.ActivityLog.RefNo = ObjMod.TypeId
            UtilityDAL.BuildActivityLog(ObjMod.ActivityLog, trans)
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
            str = "Select * From tblDefTypes"
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
