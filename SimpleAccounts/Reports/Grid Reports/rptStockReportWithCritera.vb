Imports System.Data.OleDb

Public Class rptStockReportWithCritera

    Dim dt As DataTable
#Region "Combo Fills"
    Private Sub FillCombo()
        Try
            FillListBox(Me.UiListTypes.ListItem, "select ArticleTypeId as Id, ArticleTypeName as Name from ArticleTypeDefTable where active=1 order by sortOrder")
            FillListBox(Me.UiListSizes.ListItem, "select ArticleSizeId as Id, ArticleSizeName as Name from ArticleSizeDefTable where active=1 order by sortOrder")
            FillListBox(Me.UiListCombination.ListItem, "select ArticleColorId as Id,ArticleColorName as Name from ArticleColorDefTable where active=1 order by sortOrder")
            FillListBox(Me.UiListCategories.ListItem, "select ArticleGroupId as Id, ArticleGroupName as Name from ArticleGroupDefTable where active=1 order by sortOrder")
            FillListBox(Me.UiListGender.ListItem, "select ArticleGenderId as Id, ArticleGenderName as Name from ArticleGenderDefTable where active=1 order by sortOrder")
        Catch ex As Exception

        End Try


    End Sub
    Private Sub GetSecurityRights()
        Try
            Dim dt As DataTable = GetFormRights(EnumForms.rptStockReportWithCritera)
            If Not dt Is Nothing Then
                If Not dt.Rows.Count = 0 Then
                    Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                End If
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
#End Region

#Region "Form Events"
    Private Sub rptStockReportWithCritera_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillCombo()
        Me.UiListCategories.DeSelect()
        Me.UiListCombination.DeSelect()
        Me.UiListGender.DeSelect()
        Me.UiListSizes.DeSelect()
        Me.UiListTypes.DeSelect()
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        BindGrid()
        If Me.grdStock.Rows.Count > 0 Then
            Me.TabStockReport.SelectTab(Me.TbResult)
        End If

    End Sub
    Sub BindGrid()
        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable
        dt.Clear()
        Me.grdStock.DataSource = dt
        Dim strSql As String
        Try
            If Me.ItemCode.Text = "" And Me.ItemCode2.Text = "" Then
                strSql = " SELECT     TOP 100 PERCENT  dbo.ArticleDefView.ArticleGroupName As [Group] , dbo.ArticleDefView.ArticleDescription As Description, "
                strSql = strSql + " dbo.ArticleDefView.ArticleTypeName As Type, dbo.ArticleDefView.ArticleColorName As Color, dbo.ArticleDefView.ArticleSizeName As Size,dbo.ArticleDefView.ArticleGenderName as Gender, "
                If Me.rdbBatchWise.Checked = True Then
                    strSql = strSql + "  dbo.vw_Batch_Stock.BatchNo As BatchNo , "
                End If
                strSql = strSql + " dbo.vw_Batch_Stock.Stock As Stock, dbo.vw_Batch_Stock.Stock * dbo.ArticleDefView.PurchasePrice AS Value "
                strSql = strSql + " FROM         dbo.ArticleDefView INNER JOIN "
                strSql = strSql + " dbo.vw_Batch_Stock ON dbo.ArticleDefView.ArticleId = dbo.vw_Batch_Stock.ArticleId INNER JOIN  "
                strSql = strSql + " dbo.ArticleGenderDefTable ON dbo.ArticleDefView.ArticleGenderId = dbo.ArticleGenderDefTable.ArticleGenderId "
                strSql = strSql + " Where 1=1 "
                If Me.UiListCategories.ListItem.SelectedItems.Count > 0 Then
                    strSql = strSql + " And dbo.ArticleDefView.ArticleGroupName in ( " & Me.UiListCategories.SelectedItems & ") "
                End If
                If Me.UiListCombination.ListItem.SelectedItems.Count > 0 Then
                    strSql = strSql + " And dbo.ArticleDefView.ArticleColorName in ( " & Me.UiListCombination.SelectedItems & ") "
                End If
                If Me.UiListSizes.ListItem.SelectedItems.Count > 0 Then
                    strSql = strSql + " And dbo.ArticleDefView.ArticleSizeName in (" & Me.UiListSizes.SelectedItems & ") "
                End If
                If Me.UiListTypes.ListItem.SelectedItems.Count > 0 Then
                    strSql = strSql + " And  dbo.ArticleDefView.ArticleTypeName in ( " & Me.UiListTypes.SelectedItems & " ) "
                End If
                If Me.UiListGender.ListItem.SelectedItems.Count > 0 Then
                    strSql = strSql + " AND dbo.ArticleDefView.ArticleGenderName in (" & Me.UiListGender.SelectedItems & ") "
                End If
                If Me.rdbBatchWise.Checked = True Then
                    strSql = strSql + " ORDER BY dbo.vw_Batch_Stock.Stock DESC "
                Else
                    strSql = strSql + " ORDER BY dbo.vw_ArticleStock.Stock DESC "
                End If
            Else

                strSql = " SELECT     TOP 100 PERCENT  dbo.ArticleDefView.ArticleGroupName AS [Group], dbo.ArticleDefView.ArticleDescription As Description, "
                strSql = strSql + " dbo.ArticleDefView.ArticleTypeName as Type, dbo.ArticleDefView.ArticleColorName as Color, dbo.ArticleDefView.ArticleSizeName as Size ,dbo.ArticleDefView.ArticleGenderName As Gender, "
                If Me.rdbBatchWise.Checked = True Then
                    strSql = strSql + "  dbo.vw_Batch_Stock.BatchNo As BatchNo , "
                End If
                strSql = strSql + " dbo.vw_Batch_Stock.Stock As Stock, (dbo.vw_Batch_Stock.Stock * dbo.ArticleDefView.PurchasePrice) AS Value "
                strSql = strSql + " FROM         dbo.ArticleDefView INNER JOIN "
                strSql = strSql + " dbo.vw_Batch_Stock ON dbo.ArticleDefView.ArticleId = dbo.vw_Batch_Stock.ArticleId INNER JOIN  "
                strSql = strSql + " dbo.ArticleGenderDefTable ON dbo.ArticleDefView.ArticleGenderId = dbo.ArticleGenderDefTable.ArticleGenderId "
                strSql = strSql + " Where 1=1 "
                If Me.UiListCategories.ListItem.SelectedItems.Count > 0 Then
                    strSql = strSql + " And dbo.ArticleDefView.ArticleGroupName in ( " & Me.UiListCategories.SelectedItems & ") "
                End If
                If Me.UiListCombination.ListItem.SelectedItems.Count > 0 Then
                    strSql = strSql + " And dbo.ArticleDefView.ArticleColorName in ( " & Me.UiListCombination.SelectedItems & ") "
                End If
                If Me.UiListSizes.ListItem.SelectedItems.Count > 0 Then
                    strSql = strSql + " And dbo.ArticleDefView.ArticleSizeName in (" & Me.UiListSizes.SelectedItems & ") "
                End If
                If Me.UiListTypes.ListItem.SelectedItems.Count > 0 Then
                    strSql = strSql + " And  dbo.ArticleDefView.ArticleTypeName in ( " & Me.UiListTypes.SelectedItems & " ) "
                End If
                If Me.UiListGender.ListItem.SelectedItems.Count > 0 Then
                    strSql = strSql + " AND dbo.ArticleDefView.ArticleGenderName in (" & Me.UiListGender.SelectedItems & ") "
                End If
                If Me.rdbBatchWise.Checked = True Then
                    strSql = strSql + " ORDER BY dbo.vw_Batch_Stock.Stock DESC "
                Else
                    strSql = strSql + " ORDER BY dbo.vw_ArticleStock.Stock DESC "
                End If

            End If



            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            If dt.Rows.Count > 0 Then
                Me.grdStock.DataSource = dt
            Else
                ShowErrorMessage("No Data Found.... ! Please Try Again.")
            End If
            'Me.grdStock.DisplayLayout.Bands(0).Columns(0). = Infragistics.Win.UltraWinGrid.AllowRowSummaries.True
        Catch ex As Exception

        End Try
    End Sub
#End Region

End Class