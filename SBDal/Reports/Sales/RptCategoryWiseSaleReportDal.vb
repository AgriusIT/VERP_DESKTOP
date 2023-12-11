Imports SBUtility
Imports SBModel
Imports System.Data.SqlClient

Public Class RptCategoryWiseSaleReportDal

    Public Function GetDataTable(ByVal FromDate As String, ByVal ToDate As String, Optional ByVal Type As String = "Qty") As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try

            Dim StrSQL As String
            Dim cn As New SqlClient.SqlConnection(SQLHelper.CON_STR)
            Dim cm As New SqlClient.SqlCommand
            cm.CommandType = CommandType.Text



            Dim strCol As String = String.Empty
            If Not Type = "Qty" Then
                strCol = "SUM(dbo.SalesDetailTable.Qty)"
            Else
                strCol = "SUM(dbo.SalesDetailTable.Qty * dbo.SalesDetailTable.Price)"
            End If

            StrSQL = "SELECT      convert(datetime , left(convert(varchar , dbo.SalesMasterTable.SalesDate),11), 102) as SalesDate , isnull(" & strCol & " , 0) AS Col, dbo.ArticleGroupDefTable.ArticleGroupName  " _
                    & " FROM         dbo.SalesMasterTable INNER JOIN" _
                    & "                   dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId INNER JOIN " _
                    & "               dbo.ArticleDefTable ON dbo.SalesDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId INNER JOIN " _
                    & "           dbo.ArticleGroupDefTable ON dbo.ArticleDefTable.ArticleGroupId = dbo.ArticleGroupDefTable.ArticleGroupId " _
                    & " WHERE  (dbo.SalesMasterTable.SalesDate BETWEEN CONVERT(DATETIME, '" & FromDate & "', 102) AND CONVERT(DATETIME, '" & ToDate.ToString & "', 102))" _
                    & " GROUP BY  convert(datetime , left(convert(varchar , dbo.SalesMasterTable.SalesDate),11), 102), dbo.ArticleGroupDefTable.ArticleGroupName " _
                    & " order by convert(datetime , left(convert(varchar , dbo.SalesMasterTable.SalesDate),11), 102)  "

            cm.CommandText = StrSQL.ToString
            cm.Connection = cn
            cm.CommandTimeout = 500

            objDA = New SqlClient.SqlDataAdapter(cm)
            Dim MyCollectionList As New DataTable("GetRecords")

            objDA.Fill(MyCollectionList)

            If Not MyCollectionList Is Nothing Then
                If Not MyCollectionList.Rows.Count = 0 Then
                    Dim objDtSales As New DataTable
                    Dim dc As DataColumn
                    dc = New DataColumn("Date")
                    dc.Caption = "Date"
                    dc.DataType = GetType(DateTime)
                    objDtSales.Columns.Add(dc)

                    For Each r As DataRow In MyCollectionList.Rows
                        If Not objDtSales.Columns.Contains(r.Item("ArticleGroupName")) Then
                            objDtSales.Columns.Add(New DataColumn(r.Item("ArticleGroupName"), GetType(System.Double)))
                        End If
                    Next

                    dc = New DataColumn("Total Sale", GetType(System.Double))
                    objDtSales.Columns.Add(dc)

                    Dim dtp As Date = Date.MinValue
                    Dim dr As DataRow
                    dr = MyCollectionList.NewRow
                    For Each r As DataRow In MyCollectionList.Rows
                        If r.Item("SalesDate") = dtp Then
                            dr.Item(0) = r.Item("SalesDate").ToString
                            dr.Item(r.Item("ArticleGroupName")) = r.Item("Col")
                            dtp = r.Item("SalesDate")
                            dr.Item("Total Sale") = dr.Item("Total Sale") + r.Item("Col")
                        Else
                            dr = objDtSales.NewRow
                            dr.Item(0) = r.Item("SalesDate")
                            dr.Item(r.Item("ArticleGroupName")) = r.Item("Col")
                            dr.Item("Total Sale") = r.Item("Col")
                            objDtSales.Rows.Add(dr)
                            dtp = r.Item("SalesDate")
                        End If
                    Next
                    Return objDtSales
                Else
                    Return MyCollectionList
                End If
            Else
                Return MyCollectionList
            End If

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
        End Try

    End Function
End Class
