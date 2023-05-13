Imports SBUtility
Imports SBModel
Imports System.Data.SqlClient

Public Class RptStockStatmentDal

    Public Function GetDataTable(ByVal FromDate As String, ByVal ToDate As String) As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim StrSQL As New System.Text.StringBuilder
            Dim cn As New SqlClient.SqlConnection(SQLHelper.CON_STR)
            Dim cm As New SqlClient.SqlCommand
            cm.CommandType = CommandType.StoredProcedure

            cm.CommandText = "SP_Rpt_StockStatement"

            cm.Connection = cn
            cm.CommandTimeout = 500
            cm.Parameters.Add("@FromDate", SqlDbType.DateTime)
            cm.Parameters.Add("@ToDate", SqlDbType.DateTime)

            cm.Parameters.Item("@FromDate").Value = FromDate
            cm.Parameters.Item("@ToDate").Value = ToDate

            objDA = New SqlClient.SqlDataAdapter(cm)
            Dim MyCollectionList As New DataTable("GetRecords")

            objDA.Fill(MyCollectionList)

            Return MyCollectionList
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function
    Public Function GetStockStatment(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Location_Id As Integer, ByVal Pack As Boolean) As DataTable
        Try
            Dim str As String
            str = "SP_StockStatementNew '" & FromDate.ToString("yyyy-M-d 00:00:00") & "', '" & ToDate.ToString("yyyy-M-d 23:59:59") & "', " & Location_Id & ", " & IIf(Pack = True, 1, 0) & ""
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
