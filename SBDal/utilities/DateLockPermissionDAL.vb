Imports SBDal
Imports SBModel
Imports SBUtility
Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class DateLockPermissionDAL

    Public Function Add(ByVal objMod As DateLockPermissionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction

        Try
            Dim strQuery As String = String.Empty
            strQuery = "INSERT INTO tblDateLockPermission(DateLockFrom,DateLockTo,Lock,UserID,PermissionUserName) VALUES(Convert(DateTime,'" & objMod.DateFromLock.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Convert(DateTime,'" & objMod.DateToLock.ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & IIf(objMod.Lock = True, 1, 0) & ",'" & objMod.UserIDs.Replace("'", "''") & "','" & objMod.PermissionUserName.Replace("'", "''") & "')Select @@Identity"

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            Con.Close()

        End Try
    End Function


    Public Function Update(ByVal objMod As DateLockPermissionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction

        Try
            Dim strQuery As String = String.Empty
            strQuery = "UPDATE tblDateLockPermission SET DateLockFrom=Convert(DateTime,'" & objMod.DateFromLock.ToString("yyyy-M-d hh:mm:ss tt") & "',102),DateLockTo=Convert(DateTime,'" & objMod.DateToLock.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Lock=" & IIf(objMod.Lock = True, 1, 0) & ",UserID='" & objMod.UserIDs.Replace("'", "''") & "',PermissionUserName='" & objMod.PermissionUserName.Replace("'", "''") & "' WHERE ID=" & objMod.ID & ""

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            Con.Close()

        End Try
    End Function

    Public Function Delete(ByVal objMod As DateLockPermissionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction

        Try
            Dim strQuery As String = String.Empty
            strQuery = "Delete From tblDateLockPermission WHERE ID=" & objMod.ID & ""

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            Con.Close()

        End Try
    End Function


    Public Function GetAll() As DataTable
        Try
            Return UtilityDAL.GetDataTable("Select * from tblDateLockPermission ORDER BY ID DESC")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
