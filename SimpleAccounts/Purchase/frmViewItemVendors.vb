Public Class frmViewItemVendors

    Public isFormOpen As Boolean = False
    Public MasterArticleId As Integer = 0

    Private Sub frmViewItemVendors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True

        ReSetControls()

    End Sub

    Public Sub FillCombos(Optional Condition As String = "")

        Try
            If Condition = "Vendors" Then
                FillListBox(Me.lstVendors.ListItem, "select vwCOADetail.coa_detail_id , vwCOADetail.detail_code + ' ~ ' + vwCOADetail.detail_title As Vendor from ArticleDefTable " _
                           & "Left Outer Join ArticleDefVendors On ArticleDefTable.MasterId = ArticleDefVendors.ArticleDefId " _
                           & "Left Outer Join vwCOADetail On ArticleDefVendors.VendorId = vwCOADetail.coa_detail_id " _
                           & "where ArticleDefTable.ArticleId = " & MasterArticleId)
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ReSetControls(Optional Condition As String = "")
        Try
            If MasterArticleId > 0 Then
                FillCombos("Vendors")
            End If
            Me.lstVendors.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint














































    End Sub
End Class