Public Class frmInquiryItem

    Private Sub frmStockLocationWise_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmStockLocationWise_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim str As String
            str = "SELECT Convert(Int, Detail.SerialNo) As SerialNo,  Detail.RequirementDescription,  Article.ArticleCode As Code, Article.ArticleDescription,  Unit.ArticleUnitName As Unit, Type.ArticleTypeName As Type, Category.ArticleCompanyName As Category,  SubCategory.ArticleLpoName As SubCategory, Origin.ArticleGenderName As Origin, IsNull( Detail.Qty, 0) As Qty,  Detail.Comments FROM SalesInquiryDetail As Detail  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where SalesInquiryId =" & frmInquiryComparisonStatement.grdSaved.CurrentRow.Cells("SalesInquiryId").Value & "Order By Detail.SerialNo "
            Dim dt As DataTable
            dt = GetDataTable(str)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class