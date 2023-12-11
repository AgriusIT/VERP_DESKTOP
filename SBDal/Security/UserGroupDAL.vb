''03-Jul-2014 TASK:M61 Imran Ali Diplay Active User Group In List
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class UserGroupDAL
    Public Function add(ByVal usergroup As UserGroup) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim _strqueery As String = "insert into tblusergroup (GroupName ,SortOrder , Active) values (N'" & usergroup.GroupName & "',N'" & usergroup.SortOrder & "',N'" & IIf(usergroup.Active = True, 1, 0) & "')"
            usergroup.GroupId = SQLHelper.ExecuteScaler(trans, CommandType.Text, _strqueery)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function DetModuleRecord(Optional ByVal GroupFilter As String = "") As DataTable
        Try
            'Before against task:M61
            'Dim _strquery As String = "select * from tblUserGroup WHERE GroupName Like N'" & GroupFilter & "%' "
            'Task:M61 Active Group
            Dim _strquery As String = "select ModuleName FROM tblmodule"
            'End Task:M61
            Dim dt As DataTable = UtilityDAL.GetDataTable(_strquery)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function update(ByVal usergroup As UserGroup) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then
            con.Open()

        End If
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim _strquery As String = "update tblusergroup set GroupType=N'" & usergroup.GroupType & "',  GroupName=N'" & usergroup.GroupName & "',  SortOrder = N'" & usergroup.SortOrder & "', Active = " & IIf(usergroup.Active = True, 1, 0) & " WHERE GroupId=" & usergroup.GroupId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, _strquery)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function Delete(ByVal usergroup As UserGroup) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then
            con.Open()

        End If
        Dim trans As SqlTransaction = con.BeginTransaction
        Try

            Dim _strQuery As String
            _strQuery = "delete from tblusergroup where GroupId = N'" & usergroup.GroupId & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, _strQuery)

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Getallrecord(Optional ByVal GroupFilter As String = "") As DataTable
        Try
            'Before against task:M61
            'Dim _strquery As String = "select * from tblUserGroup WHERE GroupName Like N'" & GroupFilter & "%' "
            'Task:M61 Active Group
            Dim _strquery As String = "select * from tblUserGroup WHERE GroupName Like N'" & GroupFilter & "%'  AND Active=1  ORDER BY SortOrder"
            'End Task:M61
            Dim dt As DataTable = UtilityDAL.GetDataTable(_strquery)
            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetGroupsByIds(ByVal GroupIds As String) As DataTable
        Try

            Dim _strquery As String = "select * from tblUserGroup WHERE GroupId in('" & GroupIds & "')  AND Active=1"
            Dim dt As DataTable = UtilityDAL.GetDataTable(_strquery)

            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class


