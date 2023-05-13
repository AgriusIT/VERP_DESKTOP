Imports SBModel
Imports System.Data.SqlClient

Public Class TerminalConfigurationDAL
    Dim TCMId As Integer = 0
    Public Function Add(ByVal Master As TerminalConfigurationMaster) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()

        Dim TCDId As Integer = 0
        Try

            Dim strSQL As String = String.Empty

            strSQL = "insert into TerminalConfigurationMaster(Title ,Layout, FormId)" _
           & " Values(N'" & Master.Title.Trim.Replace("'", "''") & "',N'" & Master.Layout.Trim.Replace("'", "''") & "'," & _
            Master.FormId & ") Select @@Identity"
            Master.TCMId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL) ''Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL))
            TCMId = Master.TCMId
            ''add deltail
            If Not Master.Detail.Count = 0 Then
                If Me.AddDetail(Master, trans) Then
                End If
            End If
            Dim listSystems As List(Of TerminalConfigurationSystems) = Master.Systems
            For Each _object As TerminalConfigurationSystems In listSystems
                strSQL = "Insert Into TerminalConfigurationSystems(SystemName, TCMId, TCDId, SystemId ) Values (N'" & _object.SystemName.Trim.Replace("'", "''") & "'," & TCMId & ", '" & _object.TCDId & "', '" & _object.SystemId & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            Dim listUsers As List(Of TerminalConfigurationUsers) = Master.Users
            For Each _object As TerminalConfigurationUsers In listUsers
                strSQL = "Insert Into TerminalConfigurationUsers(UserName, TCMId, TCDId, UserId ) Values (N'" & _object.UserName.Trim.Replace("'", "''") & "'," & TCMId & ", '" & _object.TCDId & "', '" & _object.UserId & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
            Master = Nothing
        End Try

    End Function
    Private Function AddDetail(ByVal Master As TerminalConfigurationMaster, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim strSQL As String = String.Empty
            Dim listObject As List(Of TerminalConfigurationDetail) = Master.Detail

            If listObject Is Nothing Then Return False
            If listObject.Count = 0 Then Return False
            Dim obj As Object = Nothing
            For Each _object As TerminalConfigurationDetail In listObject
                strSQL = "insert into TerminalConfigurationDetail(TCMId, FormId, ManuTitle," _
                                      & " FormCaption) values('" & TCMId & "', '" & _object.FormId & "', '" & _object.ManuTitle.Trim.Replace("'", "''") & "', '" & _object.FormCaption.Trim.Replace("'", "''") & "') Select @@Identity "
                obj = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Next
            Return True
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
