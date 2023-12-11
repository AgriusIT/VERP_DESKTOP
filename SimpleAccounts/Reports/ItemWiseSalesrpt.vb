Imports System.Data
Imports System.Data.OleDb

Public Class ItemWiseSalesrpt

    Private Sub ItemWiseSalesrpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BindGrid()
    End Sub
    Sub BindGrid()
        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable

        adp = New OleDbDataAdapter("SELECT     TOP 100 PERCENT dbo.ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, SUM(dbo.SalesDetailTable.Qty) AS QTY, " & _
                      "SUM(dbo.SalesDetailTable.Price * dbo.SalesDetailTable.Qty) AS Value " & _
" FROM         dbo.SalesDetailTable INNER JOIN " & _
                      " dbo.ArticleDefTable ON dbo.SalesDetailTable.ArticleDefId = dbo.ArticleDefTable.ArticleId " & _
"GROUP BY dbo.ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription " & _
"ORDER BY dbo.ArticleDefTable.ArticleCode ", Con)
        adp.Fill(dt)
        Me.GridEX2.DataSource = dt
        Me.GridEX2.RetrieveStructure()


    End Sub

End Class