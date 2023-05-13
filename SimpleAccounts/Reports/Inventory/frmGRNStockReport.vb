Imports SBModel
Public Class frmGRNStockReport

    Private Sub frmGRNStockReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            FillDropDown(Me.cmbLocation, "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order")
            Dim str As String
            str = "select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ""
            Dim dtlocation As DataTable
            dtlocation = GetDataTable(str)
            If IsDBNull(dtlocation.Rows(0).Item("Location_Id")) = True Then
                ShowErrorMessage("You Don't have rights to any location, Please Contact to the Authorized Person")
                Exit Sub
            End If
            GetStock()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetStock()
        Dim Sqlquery As String = ""
        Try
            Sqlquery = "Exec StockGRNReport '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', " & IIf(Me.cmbLocation.SelectedIndex = -1, 0, Me.cmbLocation.SelectedValue) & ", " & LoginUserId & ""
            Dim dt As DataTable = GetDataTable(Sqlquery)
            dt.AcceptChanges()
            Me.grdStock.DataSource = dt
            Me.grdStock.RetrieveStructure()
            '            Select (OpeningStock.Qty + OpeningGRN.Qty - OpeningDelivery.Qty) As OpeningQty, ArticleDefView.ArticleId, ArticleDefView.ArticleCode, 
            'ArticleDefView.ArticleDescription, ArticleDefView.ArticleSizeName, ArticleDefView.ArticleColorName, ArticleDefView.ArticleGenderName, ArticleDefView.ArticleBrandName,
            'ArticleDefView.ArticleCompanyName, ArticleDefView.ArticleGroupName, ArticleDefView.ArticleLpoName, ArticleDefView.ArticleUnitName, ArticleDefView.ArticleTypeName,
            'Stock.Qty As StockQty, GRN.Qty As GRNQty, Delivery.Qty As DCQty, (Stock.Qty + GRN.Qty - Delivery.Qty) As ClosingQty
            Me.grdStock.RootTable.Columns("OpeningQty").Caption = "Opening Quantity"
            Me.grdStock.RootTable.Columns("ArticleCode").Caption = "Code"
            Me.grdStock.RootTable.Columns("ArticleDescription").Caption = "Article Description"
            Me.grdStock.RootTable.Columns("ArticleSizeName").Caption = "Size"
            Me.grdStock.RootTable.Columns("ArticleColorName").Caption = "Color"
            Me.grdStock.RootTable.Columns("ArticleGenderName").Caption = "Gender"
            Me.grdStock.RootTable.Columns("ArticleUnitName").Caption = "Unit"
            Me.grdStock.RootTable.Columns("StockQty").Caption = "Stock Qty"
            Me.grdStock.RootTable.Columns("GRNQty").Caption = "GRN Qty"
            Me.grdStock.RootTable.Columns("DCQty").Caption = "DC Qty"
            Me.grdStock.RootTable.Columns("EstimatedQty").Caption = "Estimated Qty"
            Me.grdStock.RootTable.Columns("ClosingQty").Caption = "Closing Qty"
            Me.grdStock.RootTable.Columns("ArticleBrandName").Visible = False
            Me.grdStock.RootTable.Columns("ArticleCompanyName").Visible = False
            Me.grdStock.RootTable.Columns("ArticleGroupName").Visible = False
            Me.grdStock.RootTable.Columns("ArticleLpoName").Visible = False
            Me.grdStock.RootTable.Columns("ArticleTypeName").Visible = False
            Me.grdStock.RootTable.Columns("ArticleId").Visible = False
            ''
            Me.grdStock.RootTable.Columns("StockQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdStock.RootTable.Columns("GRNQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdStock.RootTable.Columns("DCQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdStock.RootTable.Columns("ClosingQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdStock.RootTable.Columns("OpeningQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdStock.RootTable.Columns("EstimatedQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            ''
            Me.grdStock.RootTable.Columns("StockQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdStock.RootTable.Columns("GRNQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdStock.RootTable.Columns("DCQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdStock.RootTable.Columns("ClosingQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdStock.RootTable.Columns("OpeningQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdStock.RootTable.Columns("EstimatedQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            ''
            Me.grdStock.RootTable.Columns("StockQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdStock.RootTable.Columns("GRNQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdStock.RootTable.Columns("DCQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdStock.RootTable.Columns("ClosingQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdStock.RootTable.Columns("OpeningQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdStock.RootTable.Columns("EstimatedQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            Dim str As String
            str = "select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ""
            Dim dtlocation As DataTable
            dtlocation = GetDataTable(str)
            If IsDBNull(dtlocation.Rows(0).Item("Location_Id")) = True Then
                ShowErrorMessage("You Don't have rights to any location, Please Contact to the Authorized Person")
                Exit Sub
            End If
            GetStock()
            'CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            Dim ID As Integer = 0
            ID = Me.cmbLocation.SelectedValue
            FillDropDown(Me.cmbLocation, "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order")
            Me.cmbLocation.SelectedValue = ID
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class