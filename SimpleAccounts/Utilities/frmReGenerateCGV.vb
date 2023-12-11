'17-Dec-2013 R-947      Imran Ali       Cost of Goods Voucher Regenerate   
Public Class frmReGenerateCGV
    ''R-947 Added Update Event
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Me.ToolStripStatusLabel1.Visible = True
        Me.ToolStripProgressBar1.Visible = True
        Try




            Me.ToolStripStatusLabel1.Visible = False
            Me.ToolStripProgressBar1.Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''R-947 Added Function ReGenerate CG
    Public Function CGRegenerate(ByVal FromDate As DateTime, ByVal ToDate As DateTime) As Boolean
        Dim Cmd As New OleDb.OleDbCommand
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As OleDb.OleDbTransaction = Con.BeginTransaction
        Cmd.Connection = Con
        Cmd.Transaction = objTrans
        Try
            Dim Query As String = String.Empty
            'Update AVG Rate In Stock Detail Table .... 
            Query = String.Empty
            Query = "UPDATE StockDetailTable SET Rate=Isnull(VRate,0), InAmount=(Isnull(InQty,0)*IsNull(VRate,0)), OutAmount=(IsNull(OutQty,0)*IsNull(VRate,0)) " _
                    & " --Select DocNo, DocDate, StockMasterTable.StockTransId, ArticleDefId, Rate, Isnull(Avg_Rate.VRate,0) as VRate, InQty, OutQty, InAmount, OutAmount " _
                    & " From StockMasterTable INNER JOIN StockDetailTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId  " _
                    & " LEFT OUTER JOIN (SELECT  ArticleDefId AS Id, (ABS(ISNULL(Amount, 0))/ABS(ISNULL(Qty, 0))) AS VRate FROM " _
                    & "(SELECT dbo.StockDetailTable.ArticleDefId, SUM(ISNULL(dbo.StockDetailTable.InQty, 0)) AS Qty,   " _
                    & " SUM(ISNULL(dbo.StockDetailTable.InAmount,0)) AS Amount   " _
                    & " FROM  dbo.StockMasterTable INNER JOIN   " _
                    & " dbo.StockDetailTable ON dbo.StockMasterTable.StockTransId = dbo.StockDetailTable.StockTransId  " _
                    & " WHERE (Convert(Varchar, DocDate, 102) <= Convert(DateTime, '" & ToDate.ToString("yyyy-M-d 00:00:00") & "', 102))   " _
                    & " GROUP BY dbo.StockDetailTable.ArticleDefId Having  (SUM(ISNULL(dbo.StockDetailTable.InAmount,0)) <> 0 And SUM(ISNULL(dbo.StockDetailTable.InQty, 0)) <> 0) " _
                    & " ) AvgRate)AVG_Rate On AVG_Rate.Id = StockDetailTable.ArticleDefId " _
                    & " WHERE(LEFT(DocNo,2)='SI' Or LEFT(DocNo,2)='SR') AND LEFT(DocNo,3) <> 'SRN' AND (Convert(Varchar, DocDate, 102) BETWEEN Convert(DateTime, '" & FromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & ToDate.ToString("yyyy-M-d 23:59:59") & "', 102)) --ORDER BY DocNo Asc  "
            Cmd.CommandText = Query
            Cmd.CommandType = CommandType.Text
            Cmd.ExecuteNonQuery()
            objTrans.Commit()
            ''Customer Sales Invoice Query
            Query = " Select DocNo, DocDate, StockTransId, CASE WHEN Isnull(Sales.CustomerCode,0)=0 THEN Isnull(SalesReturn.CustomerCode,0) ELSE Isnull(Sales.CustomerCode,0) End As AccountId, CASE WHEN Isnull(Sales.CustomerCode,0)=0 THEN 'SalesReturn' ELSE 'RecordSales' End  as Source " _
                  & " From StockMasterTable  LEFT OUTER JOIN SalesMasterTable Sales on Sales.SalesNo = StockMasterTable.DocNo " _
                  & " LEFT OUTER JOIN SalesReturnMasterTable SalesReturn on SalesReturn.SalesReturnNo = StockMasterTable.DocNo " _
                  & " WHERE(LEFT(DocNo,2)='SI' Or LEFT(DocNo,2)='SR') AND LEFT(DocNo,3) <> 'SRN' AND (Convert(Varchar, DocDate, 102) BETWEEN Convert(DateTime, '" & FromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & ToDate.ToString("yyyy-M-d 23:59:59") & "', 102)) ORDER BY DocNo Asc "
            Dim dtCust As New DataTable
            dtCust = GetDataTable(Query)
            If dtCust IsNot Nothing Then
                If dtCust.Rows.Count > 0 Then
                    For Each objRow As DataRow In dtCust.Rows
                        frmModProperty.Tags = objRow.Item("DocNo")
                        frmMain.LoadControl(objRow.Item("Source"))
                        If objRow.Item("Source").ToString = "RecordSales" Then
                            frmSales.SaveToolStripButton_Click(Nothing, Nothing)
                        ElseIf objRow.Item("Source").ToString = "SalesReturn" Then
                            frmSalesReturn.SaveToolStripButton_Click(Nothing, Nothing)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

End Class