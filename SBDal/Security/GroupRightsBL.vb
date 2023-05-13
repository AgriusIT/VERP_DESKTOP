Imports SBModel
Imports SBDAL
Imports SBUtility.Utility
Public Class GroupRightsBL
    Public Enum EnmGroupRights
        FormId
        FormName
        FormControlId
        FormControlName
        Rights
        UserId
    End Enum
    Public Function GetRights(ByVal UserId As Integer) As List(Of GroupRights)
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()

        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim CommandParameter As SqlClient.SqlParameter() = {SQLHelper.CreateParameter("@UserId", SqlDbType.Int, UserId)}
            Dim dr As SqlClient.SqlDataReader = SQLHelper.ExecuteReader(trans, CommandType.StoredProcedure, "SP_GetRights", CommandParameter)

            Dim GroupRightsList As New List(Of GroupRights)
            'GroupRightsList = New List(Of GroupRights)
            Dim GroupRights As GroupRights
            If Not dr Is Nothing Then
                If dr.HasRows Then
                    Do While dr.Read
                        GroupRights = New GroupRights
                        GroupRights.FormId = dr.GetValue(EnmGroupRights.FormId)
                        GroupRights.FormName = dr.GetValue(EnmGroupRights.FormName)
                        GroupRights.FormControlId = dr.GetValue(EnmGroupRights.FormControlId)
                        GroupRights.FormControlName = dr.GetValue(EnmGroupRights.FormControlName)
                        GroupRights.Rights = dr.GetValue(EnmGroupRights.Rights)
                        GroupRights.UserId = dr.GetValue(EnmGroupRights.UserId)
                        GroupRightsList.Add(GroupRights)
                    Loop
                End If
            End If
            'trans.Commit()
            Return GroupRightsList
        Catch ex As SqlClient.SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Shared Function ApplySecurity(ByVal GroupRights As List(Of GroupRights), ByVal FormName As String, ByVal RightsName As String) As Boolean
        Try
            For Each Rights As GroupRights In GroupRights
                If Rights.FormName = FormName AndAlso Rights.FormControlName = RightsName Then
                    Return True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
