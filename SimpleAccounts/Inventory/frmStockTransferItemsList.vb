''TFS3777 : Ayesha Rehman : Added a new form to add Ite in the grid for Stock Dispatch,option of adding Multiple Items

Public Class frmStockTransferItemsList
    Public Shared ArticleId As Integer
    Public Shared Qty As Integer
    Public Shared dt As DataTable
    Public Shared PackName As String
    Public Shared PackQty As Integer
    Public Shared Rate As Integer
    Public Shared LocationId As Integer = 1
    Dim flgLocationWiseItem As Boolean = False
    Dim str As String = ""
    Public dv As New DataView

    Public Sub GetAll()
        Try

            If getConfigValueByType("BagStock").ToString = "False" Then
                str = " Select ArticleId, ArticleCode as [Item Code] , ArticleDescription as [Item Name] ,ArticleSizeName As Size, ArticleColorName As Color ,PurchasePrice as Price , SalePrice,1 As Qty,1 as Unit, PackQty as [PackQty],IsNull(stock.AvailableStock,0)  As AvailableStock , 0 AS Column1  from ArticleDefView  LEFT OUTER JOIN  " _
                        & " (Select ArticleDefId ,(Sum(Isnull(InQty,0)-IsNull(OutQty,0))) As AvailableStock from StockDetailTable where 1=1 and LocationId = " & IIf(getConfigValueByType("CheckCurrentStockByLocation").ToString = "True", LocationId, 0) & " group by ArticleDefId  ) As Stock on  Stock.ArticleDefId = ArticleDefView.ArticleId "
            Else
                str = " Select ArticleId, ArticleCode as [Item Code] , ArticleDescription as [Item Name] ,ArticleSizeName As Size, ArticleColorName As Color ,PurchasePrice as Price , SalePrice,1 As Qty, PackQty as [PackQty], IsNull(stock.AvailableStock,0) As AvailableStock , 0 AS Column1  from ArticleDefView  LEFT OUTER JOIN " _
                     & " (Select ArticleDefId ,Sum(Isnull(In_PackQty,0)-IsNull(Out_PackQty,0)) As AvailableStock from StockDetailTable where 1=1 and LocationId = " & IIf(getConfigValueByType("CheckCurrentStockByLocation").ToString = "True", LocationId, 0) & " group by ArticleDefId  ) As Stock on Stock.ArticleDefId = ArticleDefView.ArticleId "
            End If
            If flgLocationWiseItem = True Then
                str += " AND ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & LocationId & " AND Restricted=1)"
            End If
            str += " Group By ArticleId,ArticleCode,ArticleSizeName,ArticleDescription,ArticleColorName,SalePrice,PurchasePrice,[PackQty],AvailableStock "
                str += " order by ArticleId Desc"
            dt = GetDataTable(str)
            dt.TableName = "Item"
            dv.Table = dt
            Me.grd.DataSource = dv.ToTable
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                Me.grd.RootTable.Columns("Column1").ActAsSelector = True
                Me.grd.RootTable.Columns("Column1").UseHeaderSelector = True
            Me.grd.RootTable.Columns("Column1").Width = 50
                Me.grd.RootTable.Columns("Unit").HasValueList = True
                Me.grd.RootTable.Columns("Unit").LimitToList = True
                Me.grd.RootTable.Columns("Unit").EditType = Janus.Windows.GridEX.EditType.Combo
                FillCombo()
                ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ApplyGridSettings()
        Try
            Me.grd.RootTable.Columns("ArticleId").Visible = False
            Me.grd.RootTable.Columns("SalePrice").Visible = False

            'For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
            '    If col.Key = "Unit" Then
            '        col.EditType = Janus.Windows.GridEX.EditType.Combo
            '    Else
            '        col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            '    End If
            'Next
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If col.Key <> "Unit" Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            Me.grd.RootTable.Columns("AvailableStock").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PackQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("SalePrice").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("AvailableStock").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PackQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("SalePrice").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillCombo()
        Try
            str = "Select 1 as Id , 'Loose' As Unit union Select 2 as Id , 'Pack' As Unit"
            Dim dt As DataTable = GetDataTable(str)
            Me.grd.RootTable.Columns("Unit").ValueList.PopulateValueList(dt.DefaultView, "Id", "Unit")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            LoadRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub LoadRecord()
        Try
            Dim CheckedRows() As Janus.Windows.GridEX.GridEXRow = Me.grd.GetCheckedRows
            If Not CheckedRows.Length > 0 Then
                ShowErrorMessage("At least one selected row is required.")
                Exit Sub
            Else
                For Each _Row As Janus.Windows.GridEX.GridEXRow In CheckedRows
                    'Stock Dispatch commented for rebuild
                    'If frmStockDispatch.Validate_AddToGridForPopUp(Val(_Row.Cells("ArticleId").Value.ToString), Val(_Row.Cells("AvailableStock").Value.ToString), Val(_Row.Cells("Price").Value.ToString), Val(_Row.Cells("SalePrice").Value.ToString), Val(_Row.Cells("Unit").Value.ToString), Val(_Row.Cells("PackQty").Value.ToString), Val(_Row.Cells("AvailableStock").Value.ToString)) Then
                    '    frmStockDispatch.LoadItemFromPopup(Val(_Row.Cells("ArticleId").Value.ToString), Val(_Row.Cells("AvailableStock").Value.ToString), Val(_Row.Cells("Price").Value.ToString), Val(_Row.Cells("SalePrice").Value.ToString), Val(_Row.Cells("Unit").Value.ToString), Val(_Row.Cells("PackQty").Value.ToString), Val(_Row.Cells("AvailableStock").Value.ToString))
                    'End If
                Next
                Me.txtSearch.Text = ""
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmStockTransferItemsList_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If Not getConfigValueByType("ArticleFilterByLocation").ToString = "Error" Then
                flgLocationWiseItem = getConfigValueByType("ArticleFilterByLocation")
            End If
            Me.txtSearch.Text = ""
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try
            If e.Column.Key = "Unit" Then
                Dim Unit As Integer = Me.grd.GetRow.Cells("Unit").Value
                Me.grd.GetRow.Cells("AvailableStock").Value = Convert.ToDouble(GetStockById(grd.GetRow.Cells("ArticleId").Value, LocationId, IIf(Unit = 1, "Loose", "Pack")))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_KeyDown(sender As Object, e As KeyEventArgs) Handles grd.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                LoadRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
        If Me.txtSearch.Text <> String.Empty Then
                dv.RowFilter = "[Item Name] LIKE '%" & Me.txtSearch.Text & "%' or  [Item Code] LIKE '%" & Me.txtSearch.Text & "%' or  Color LIKE '%" & Me.txtSearch.Text & "%' or  Size LIKE '%" & Me.txtSearch.Text & "%'  "
            Me.grd.DataSource = dv
        Else
            Me.grd.DataSource = dt
            End If
                 Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class