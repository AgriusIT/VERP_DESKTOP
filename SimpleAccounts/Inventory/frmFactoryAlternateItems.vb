''

Public Class frmFactoryAlternateItems
    Dim IsFormOpened As Boolean = False
    Public ItemId As Integer = 0
    Public ItemName As String = String.Empty
    Private Sub FillCombo()
        Try
            Dim Str As String = String.Empty

            Str = "SELECT ArticleDefView.ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, ArticleDefView.ArticleBrandName As Grade, LastPurchasePrice.Rate AS Price, Isnull(SalePrice,0) as SalePrice, ArticleDefView.SizeRangeID as [Size ID], Isnull(ArticleDefView.ArticleTaxId,0) as [Tax ID], Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SubSubId,0) as AccountId, Isnull(ServiceItem,0) as ServiceItem, ArticleDefView.SortOrder, ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand],Isnull(LogicalItem,0) as LogicalItem, SalesAccountId, CGSAccountId, MasterID , IsNull(Cost_Price,0) as Cost_Price, IsNull(TradePrice,0) as [Trade Price], IsNull(PrintedRetailPrice,0) as [Retail Price], Isnull(StockDetail.Stock,0) as Stock, IsNull(dbo.ArticleDefView.ApplyAdjustmentFuelExp,1) as  ApplyAdjustmentFuelExp FROM ArticleDefView Left Outer Join (SELECT MAX(StockTransDetailId) AS StockTransDetailId, ArticleDefId AS ArticleId, IsNull(Rate, 0) AS Rate FROM StockDetailTable WHERE InQty > 0 GROUP BY ArticleDefId, Rate) AS LastPurchasePrice ON ArticleDefView.ArticleId= LastPurchasePrice.ArticleId LEFT OUTER JOIN  (Select ArticleDefId, Sum(IsNull(InQty, 0)-IsNull(OutQty, 0)) As Stock From StockDetailTable Group By ArticleDefId) As StockDetail ON ArticleDefView.ArticleId = StockDetail.ArticleDefId Where ArticleDefView.Active=1  "
            If ItemSortOrder = True Then
                Str += " ORDER By ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByCode = True Then
                Str += " ORDER By ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByName = True Then
                Str += " ORDER By ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            Else
                Str += " ORDER By ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            End If
            FillUltraDropDown(Me.cmbProduct, Str)

            If cmbProduct.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbProduct.Rows(0).Activate()
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("ApplyAdjustmentFuelExp").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Tax ID").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("CGSAccountId").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("SalesAccountId").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("MasterID").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Type").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Dept").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Origin").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Brand").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("SalePrice").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Stock").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("LogicalItem").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("LogicalItem").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Cost_Price").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Trade Price").Hidden = True
                Me.cmbProduct.DisplayLayout.Bands(0).Columns("Retail Price").Hidden = True
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Name").Width = 300
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Limit").Width = 80
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Discount").Width = 80
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Width = 200
                'Me.cmbParty.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rbCode_CheckedChanged(sender As Object, e As EventArgs) Handles rbCode.CheckedChanged
        Try
            If IsFormOpened = False Then Exit Sub
            If Me.rbCode.Checked = True Then Me.cmbProduct.DisplayMember = "Code"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbName_CheckedChanged(sender As Object, e As EventArgs) Handles rbName.CheckedChanged
        Try
            If IsFormOpened = False Then Exit Sub
            If Me.rbName.Checked = True Then Me.cmbProduct.DisplayMember = "Item"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Me.ItemId = 0
            Me.ItemName = String.Empty
            If Me.cmbProduct.Value > 0 Then
                Me.ItemId = Me.cmbProduct.Value
                Me.ItemName = Me.cmbProduct.ActiveRow.Cells("Item").Value.ToString
                Me.Close()
            Else
                ShowErrorMessage("Please select an item.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmFactoryAlternateItems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            IsFormOpened = True
            FillCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmFactoryAlternateItems_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            'Me.ItemId = 0
            'Me.ItemName = String.Empty
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class