Imports SBUtility
Imports SBModel
Imports System.Data.SqlClient
Public Class rptSalesManTargetDal

    Public Function GetDataTable(ByVal ToDate As Date, Optional ByVal SalesMan As String = "ALL") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim StrSQL As New System.Text.StringBuilder
            Dim cn As New SqlClient.SqlConnection(SQLHelper.CON_STR)
            Dim cm As New SqlClient.SqlCommand
            cm.CommandType = CommandType.Text

            Dim ParmFromDate As Date = "01-" & ToDate.ToString("MMM") & "-" & ToDate.Date.Year
            Dim ParmToDate As Date = Date.DaysInMonth(ToDate.Year, ToDate.Month) & "-" & ToDate.ToString("MMM") & "-" & ToDate.Date.Year

            cm.CommandText = "select Employee_Name as [Sales Man], salesDate as [Date],SalesManQty as [Target Qty], SalesQty as [Sales Qty], SaleManValue as [Target Value], SalesAmount as [Sales Amount] from V_RPT_SalesMan_Target where SalesDate >='" & ParmFromDate & "' and SalesDate <= '" & ParmToDate & "' "
            If Not SalesMan = "ALL" Then
                cm.CommandText = cm.CommandText & " and Employee_Name='" & SalesMan & "' "
            End If
            cm.Connection = cn
            cm.CommandTimeout = 500
            'cm.Parameters.Add("@Date", SqlDbType.DateTime)

            'cm.Parameters.Item("@Date").Value = ToDate

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


End Class
