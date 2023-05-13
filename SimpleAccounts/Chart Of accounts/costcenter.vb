Imports System.data.SqlClient
Imports System.data
Public Class CostCenterDAL
    Public Function GetAll() As DataTable
        Try
            Dim strSQL As String = " SELECT dbo.tblDefCostCenter.* " _
          & " FROM dbo.tblDefCostCenter"
            Return GetDataTable(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetDataTable(ByVal strSql As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim ObjCon As SqlClient.SqlConnection
        Dim objDA As SqlClient.SqlDataAdapter
        Dim Objcmd As SqlClient.SqlCommand
        'ObjCon = New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If trans Is Nothing Then
            ObjCon = New SqlClient.SqlConnection(My.Settings.Database1ConnectionString)
        Else
            ObjCon = trans.Connection
        End If
        Objcmd = New SqlClient.SqlCommand(strSql)
        Try
            Objcmd.CommandTimeout = 300
            Objcmd.Connection = ObjCon
            Objcmd.Transaction = trans

            objDA = New SqlClient.SqlDataAdapter
            objDA.SelectCommand = Objcmd
            Dim MyCollectionList As New DataTable
            objDA.Fill(MyCollectionList)
            Return MyCollectionList
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
            If trans Is Nothing Then
                If ObjCon.State = ConnectionState.Open Then ObjCon.Close()
                ObjCon.Dispose()
            End If
End Try
    End Function
End Class
