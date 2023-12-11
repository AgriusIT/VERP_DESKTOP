Imports CRUFLIDAutomation
Public Class frmRptArticleBarcode

    Private Sub frmRptArticleBarcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillCombo()
        Try
            Dim Str As String = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], 0 as Stock, Isnull(PurchasePrice,0) as PurchasePrice FROM ArticleDefView where Active=1"
            'If flgCompanyRights = True Then
            '    Str += " AND ArticleDefView.CompanyId=" & MyCompanyId
            'End If
            'If GetConfigValue("ArticleFilterByLocation") = "True" Then
            '    If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
            '        Str += " AND ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
            '    End If
            'End If
            'If Me.rbtCustomer.Checked = True Then
            '    If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            '    Str += " AND MasterId in(Select ArticleDefId From ArticleDefCustomers WHERE CustomerId='" & Me.cmbVendor.Value & "')"
            'End If
            Str += " ORDER By ArticleDefView.SortOrder Asc "
            FillUltraDropDown(Me.cmbItem, Str)
            Me.cmbItem.Rows(0).Activate()
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("Stock").Hidden = True
            Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
            If rdoName.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RdoCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdoCode.CheckedChanged
        Try
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            FillCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoName.CheckedChanged
        Try
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            FillCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try
            GetCrystalReportRights()
            Dim ds As New dsArticleBarcode
            Dim dr As DataRow
            Dim objCrfu As New CRUFLIDAutomation.FontEncoder
            For i As Integer = 0 To 6
                dr = ds.Tables(0).NewRow
                dr(0) = Me.cmbItem.Value
                dr(1) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
                dr(2) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
                dr(3) = objCrfu.Code128("$" & Me.cmbItem.ActiveRow.Cells("Code").Text.ToString, 0)
                ds.Tables(0).Rows.InsertAt(dr, 0)
            Next
            ShowReport("rptArticleBarcode", , , , , , , ds.Tables(0))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class