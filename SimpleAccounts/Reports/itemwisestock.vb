Imports System.Data
Imports System.Data.OleDb
Public Class itemwisestock

    Private Sub itemwisestock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Getall()
    End Sub
    Sub Getall()

        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable

        adp = New OleDbDataAdapter("SELECT     dbo.ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription, SUM(dbo.tmpStock.Qty) AS Qty" & _
"FROM         dbo.ArticleDefTable INNER JOIN" & _
                     " dbo.tmpStock ON dbo.ArticleDefTable.ArticleId = dbo.tmpStock.Qty" & _
"GROUP BY dbo.ArticleDefTable.ArticleCode, dbo.ArticleDefTable.ArticleDescription ", Con)
        adp.Fill(dt)
        Me.GridEX1.DataSource = dt
        Me.GridEX1.RetrieveStructure()


    End Sub

End Class