'11-Jun-2015 End Task# 1-11-06-2015 Ahmad Sharif: Add grid ,that display only summary of stock
'29-Jun-2015 Task# 201506030 Ali Ansari add location wise summary option 
'06-July-2017 : TFS3805 : Ayesha Rehman : Need Pack Stock Information also

Imports SBModel
Imports SBDal
Public Class frmGrdRptLocationWiseStockStatementNew
    Dim ColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
    Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
    Dim ColumnSet2 As New Janus.Windows.GridEX.GridEXColumnSet
    Dim ColumnSet3 As New Janus.Windows.GridEX.GridEXColumnSet ''TFS2823
    Dim ColumnSet4 As New Janus.Windows.GridEX.GridEXColumnSet ''TFS3805
    Enum enmItems
        ArticleId
        Code
        Item
        Size
        Color
        Department
        Type
        Brand
        Count
    End Enum

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim str As String
            str = "select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ""
            Dim dtlocation As DataTable
            dtlocation = GetDataTable(str)
            If IsDBNull(dtlocation.Rows(0).Item("Location_Id")) = True Then
                ShowErrorMessage("You Don't have rights to any location, Please Contact to the Authorized Person")
                Exit Sub
            End If
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Sub FillList()
        Try
            Dim strSQL = String.Empty
            strSQL = "select ArticleStatusID as ID, ArticleStatusName as Name from ArticleStatus where active=1"
            Me.cmbStatus.DisplayMember = "Name"
            Me.cmbStatus.ValueMember = "ID"
            Me.cmbStatus.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
            FillListBox(Me.lstLocation.ListItem, "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order")
            FillDropDown(Me.cmbProject, "Select CostCenterId, Name From tblDefCostCenter Where Active=1")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub filldetailgrid()
        Try
            grd.UpdateData()
            grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grd.RootTable.ColumnSetRowCount = 1
            Me.grd.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            ColumnSet1 = Me.grd.RootTable.ColumnSets.Add
            ColumnSet1.ColumnCount = 7
            ColumnSet1.Caption = "Item Description"
            ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ColumnSet1.Add(Me.grd.RootTable.Columns(enmItems.Code), 0, 0)
            ColumnSet1.Add(Me.grd.RootTable.Columns(enmItems.Item), 0, 1)
            ColumnSet1.Add(Me.grd.RootTable.Columns(enmItems.Color), 0, 2)
            ColumnSet1.Add(Me.grd.RootTable.Columns(enmItems.Size), 0, 3)
            ColumnSet1.Add(Me.grd.RootTable.Columns(enmItems.Department), 0, 4)
            ColumnSet1.Add(Me.grd.RootTable.Columns(enmItems.Type), 0, 5)
            ColumnSet1.Add(Me.grd.RootTable.Columns(enmItems.Brand), 0, 6)
            ColumnSet1.Item(0, 0).Width = 100
            ColumnSet1.Item(0, 1).Width = 250
            ColumnSet1.Item(0, 2).Width = 50
            ColumnSet1.Item(0, 3).Width = 50
            ColumnSet1.Item(0, 4).Width = 50
            ColumnSet1.Item(0, 5).Width = 50
            ColumnSet1.Item(0, 6).Width = 50
            Me.grd.RootTable.Columns("Item").Width = 200
            '' TFS3805 : Ayesha Rehman : 07-06-2018 :Count Step to 14 ,bcz 4 new columns are added 
            For c As Integer = enmItems.Count To Me.grd.RootTable.Columns.Count - 14 Step 14
                If Me.grd.RootTable.Columns(c).DataMember <> "Opening" And Me.grd.RootTable.Columns(c).DataMember <> "In" And Me.grd.RootTable.Columns(c).DataMember <> "Out" And Me.grd.RootTable.Columns(c).DataMember <> "Close" And Me.grd.RootTable.Columns(c).DataMember <> "OpeningAmount" And Me.grd.RootTable.Columns(c).DataMember <> "InAmount" And Me.grd.RootTable.Columns(c).DataMember <> "OutAmount" And Me.grd.RootTable.Columns(c).DataMember <> "Amount" And Me.grd.RootTable.Columns(c).DataMember <> "OpeningPack" And Me.grd.RootTable.Columns(c).DataMember <> "InPack" And Me.grd.RootTable.Columns(c).DataMember <> "OutPack" And Me.grd.RootTable.Columns(c).DataMember <> "ClosePack" Then
                    Me.grd.RootTable.Columns(c).Visible = False
                    Me.grd.RootTable.Columns(c + 2).Caption = "Opening"
                    Me.grd.RootTable.Columns(c + 3).Caption = "In"
                    Me.grd.RootTable.Columns(c + 4).Caption = "Out"
                    Me.grd.RootTable.Columns(c + 5).Caption = "Close"
                    Me.grd.RootTable.Columns(c + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns(c + 3).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns(c + 4).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns(c + 5).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 4).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 5).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 3).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 4).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 5).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 2).FormatString = "N" & DecimalPointInQty
                    Me.grd.RootTable.Columns(c + 3).FormatString = "N" & DecimalPointInQty
                    Me.grd.RootTable.Columns(c + 4).FormatString = "N" & DecimalPointInQty
                    Me.grd.RootTable.Columns(c + 5).FormatString = "N" & DecimalPointInQty
                    Me.grd.RootTable.Columns(c + 2).TotalFormatString = "N" & DecimalPointInQty
                    Me.grd.RootTable.Columns(c + 3).TotalFormatString = "N" & DecimalPointInQty
                    Me.grd.RootTable.Columns(c + 4).TotalFormatString = "N" & DecimalPointInQty
                    Me.grd.RootTable.Columns(c + 5).TotalFormatString = "N" & DecimalPointInQty
                    Me.grd.RootTable.Columns(c + 5).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
                    ''Start TFS2823
                    '' TFS3805 : Indexing of Opening Amount,InAmount,Out Amount Changes in query thus Corresponding Code is changed
                    Me.grd.RootTable.Columns(c + 6).Caption = "Opening Pack"
                    Me.grd.RootTable.Columns(c + 7).Caption = "In Pack"
                    Me.grd.RootTable.Columns(c + 8).Caption = "Out Pack"
                    Me.grd.RootTable.Columns(c + 9).Caption = "Close Pack"
                    Me.grd.RootTable.Columns(c + 6).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns(c + 7).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns(c + 8).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns(c + 9).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns(c + 6).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 7).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 8).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 9).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 6).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 7).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 8).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 9).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 6).FormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 7).FormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 8).FormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 9).FormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 6).TotalFormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 7).TotalFormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 8).TotalFormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 9).TotalFormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 9).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
                    ''End TFS2823
                    ''Start TFS3805
                    Me.grd.RootTable.Columns(c + 10).Caption = "Opening Amount"
                    Me.grd.RootTable.Columns(c + 11).Caption = "In Amount"
                    Me.grd.RootTable.Columns(c + 12).Caption = "Out Amount"
                    Me.grd.RootTable.Columns(c + 13).Caption = "Amount"
                    Me.grd.RootTable.Columns(c + 10).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns(c + 11).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns(c + 12).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns(c + 13).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns(c + 10).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 11).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 12).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 13).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 10).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 11).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 12).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 13).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns(c + 10).FormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 11).FormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 12).FormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 13).FormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 10).TotalFormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 11).TotalFormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 12).TotalFormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 13).TotalFormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns(c + 13).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
                    ''End TFS3805
                    ColumnSet = Me.grd.RootTable.ColumnSets.Add
                    ''Edit Against TFS2823
                    'ColumnSet.ColumnCount = 4
                    ''Edit Against TFS3805
                    ' ColumnSet.ColumnCount = 8
                    '' TFS3805 : Ayesha Rehman : 07-06-2018 : ColumnCount = 12 ,bcz 4 new columns are added 
                    ColumnSet.ColumnCount = 12
                    ColumnSet.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    ColumnSet.Caption = Me.grd.RootTable.Columns(c + 1).Caption
                    ColumnSet.Add(Me.grd.RootTable.Columns(c + 2), 0, 0)
                    ColumnSet.Add(Me.grd.RootTable.Columns(c + 3), 0, 1)
                    ColumnSet.Add(Me.grd.RootTable.Columns(c + 4), 0, 2)
                    ColumnSet.Add(Me.grd.RootTable.Columns(c + 5), 0, 3)
                    ''Start TFS2823
                    ColumnSet.Add(Me.grd.RootTable.Columns(c + 6), 0, 4)
                    ColumnSet.Add(Me.grd.RootTable.Columns(c + 7), 0, 5)
                    ColumnSet.Add(Me.grd.RootTable.Columns(c + 8), 0, 6)
                    ColumnSet.Add(Me.grd.RootTable.Columns(c + 9), 0, 7)
                    ''End TFS2823
                    ''Start TFS3805
                    ColumnSet.Add(Me.grd.RootTable.Columns(c + 10), 0, 8)
                    ColumnSet.Add(Me.grd.RootTable.Columns(c + 11), 0, 9)
                    ColumnSet.Add(Me.grd.RootTable.Columns(c + 12), 0, 10)
                    ColumnSet.Add(Me.grd.RootTable.Columns(c + 13), 0, 11)
                    ''End TFS3805
                End If
            Next

            Me.grd.RootTable.Columns("Opening").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("In").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Out").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Close").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Opening").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("In").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Out").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Close").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Opening").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("In").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Out").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Close").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Opening").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("In").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Out").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Close").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Opening").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("In").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Out").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Close").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Close").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox


            ''Start TFS2823
            Me.grd.RootTable.Columns("OpeningAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("InAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("OutAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("OpeningAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("InAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("OutAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("OpeningAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("InAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("OutAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("OpeningAmount").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("InAmount").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("OutAmount").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("OpeningAmount").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("InAmount").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("OutAmount").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Amount").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Amount").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            ''End TFS2823

            ''Start TFS3805
            Me.grd.RootTable.Columns("OpeningPack").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("InPack").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("OutPack").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("ClosePack").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("OpeningPack").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("InPack").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("OutPack").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ClosePack").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("OpeningPack").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("InPack").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("OutPack").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ClosePack").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("OpeningPack").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("InPack").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("OutPack").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("ClosePack").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("OpeningPack").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("InPack").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("OutPack").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("ClosePack").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("ClosePack").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            ''End TFS3805

            ColumnSet2 = Me.grd.RootTable.ColumnSets.Add
            '' TFS3805 : Ayesha Rehman : 07-06-2018 : ColumnCount = 5 ,bcz Closing Stock of Pack is added 
            ColumnSet2.ColumnCount = 5
            ColumnSet2.Caption = "Total Stock"
            ColumnSet2.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ColumnSet2.Add(Me.grd.RootTable.Columns("Opening"), 0, 0)
            ColumnSet2.Add(Me.grd.RootTable.Columns("In"), 0, 1)
            ColumnSet2.Add(Me.grd.RootTable.Columns("Out"), 0, 2)
            ColumnSet2.Add(Me.grd.RootTable.Columns("Close"), 0, 3)
            ''TFS3805 : Add Closing Stock of Pack to Column Set Of Total Stock
            ColumnSet2.Add(Me.grd.RootTable.Columns("ClosePack"), 0, 4)
            ''Start TFS2823
            ColumnSet3 = Me.grd.RootTable.ColumnSets.Add
            ColumnSet3.ColumnCount = 4
            ColumnSet3.Caption = "Total Amount"
            ColumnSet3.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ColumnSet3.Add(Me.grd.RootTable.Columns("OpeningAmount"), 0, 0)
            ColumnSet3.Add(Me.grd.RootTable.Columns("InAmount"), 0, 1)
            ColumnSet3.Add(Me.grd.RootTable.Columns("OutAmount"), 0, 2)
            ColumnSet3.Add(Me.grd.RootTable.Columns("Amount"), 0, 3)
            ''End TFS2823

            Me.grdSummary.Visible = False
            Me.grd.Visible = True
            '  Me.grd.AutoSizeColumns()
            Me.GrdLocationWiseSummary.Visible = False
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            Throw ex
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
    Public Sub filllLocationWiseSummaryGrid()
        Try
            'Altered by Ali Ansari against Task#201506030 to show location wiss closing data
            GrdLocationWiseSummary.UpdateData()
            GrdLocationWiseSummary.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            GrdLocationWiseSummary.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.GrdLocationWiseSummary.RootTable.ColumnSetRowCount = 1
            Me.GrdLocationWiseSummary.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            ColumnSet1 = Me.GrdLocationWiseSummary.RootTable.ColumnSets.Add
            ColumnSet1.ColumnCount = 7
            ColumnSet1.Caption = "Item Description"
            ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ColumnSet1.Add(Me.GrdLocationWiseSummary.RootTable.Columns(enmItems.Code), 0, 0)
            ColumnSet1.Add(Me.GrdLocationWiseSummary.RootTable.Columns(enmItems.Item), 0, 1)
            ColumnSet1.Add(Me.GrdLocationWiseSummary.RootTable.Columns(enmItems.Color), 0, 2)
            ColumnSet1.Add(Me.GrdLocationWiseSummary.RootTable.Columns(enmItems.Size), 0, 3)
            ColumnSet1.Add(Me.GrdLocationWiseSummary.RootTable.Columns(enmItems.Department), 0, 4)
            ColumnSet1.Add(Me.GrdLocationWiseSummary.RootTable.Columns(enmItems.Type), 0, 5)
            ColumnSet1.Add(Me.GrdLocationWiseSummary.RootTable.Columns(enmItems.Brand), 0, 6)
            ColumnSet1.Item(0, 0).Width = 100
            ColumnSet1.Item(0, 1).Width = 25
            ColumnSet1.Item(0, 2).Width = 50
            ColumnSet1.Item(0, 3).Width = 50
            ColumnSet1.Item(0, 4).Width = 50
            ColumnSet1.Item(0, 5).Width = 50
            ColumnSet1.Item(0, 6).Width = 50
            Me.GrdLocationWiseSummary.RootTable.Columns("Item").Width = 200
            '' TFS3805 : Ayesha Rehman : 07-06-2018 :Count Step to 14 ,bcz 4 new columns are added  
            For c As Integer = enmItems.Count To Me.GrdLocationWiseSummary.RootTable.Columns.Count - 14 Step 14

                If Me.GrdLocationWiseSummary.RootTable.Columns(c).DataMember <> "Opening" And Me.GrdLocationWiseSummary.RootTable.Columns(c).DataMember <> "In" And Me.GrdLocationWiseSummary.RootTable.Columns(c).DataMember <> "Out" And Me.GrdLocationWiseSummary.RootTable.Columns(c).DataMember <> "Close" And Me.GrdLocationWiseSummary.RootTable.Columns(c).DataMember <> "OpeningPack" And Me.GrdLocationWiseSummary.RootTable.Columns(c).DataMember <> "InPack" And Me.GrdLocationWiseSummary.RootTable.Columns(c).DataMember <> "OutPack" And Me.GrdLocationWiseSummary.RootTable.Columns(c).DataMember <> "ClosePack" And Me.GrdLocationWiseSummary.RootTable.Columns(c).DataMember <> "OpeningAmount" And Me.GrdLocationWiseSummary.RootTable.Columns(c).DataMember <> "InAmount" And Me.GrdLocationWiseSummary.RootTable.Columns(c).DataMember <> "OutAmount" And Me.GrdLocationWiseSummary.RootTable.Columns(c).DataMember <> "Amount" Then
                    Me.GrdLocationWiseSummary.RootTable.Columns(c).Visible = False
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 2).Caption = "Opening"
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 2).Visible = False
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 3).Caption = "In"
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 3).Visible = False
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 4).Caption = "Out"
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 4).Visible = False
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 5).Caption = "Close"
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 5).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 5).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 5).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 5).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 5).FormatString = "N" & DecimalPointInQty
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 5).TotalFormatString = "N" & DecimalPointInQty
                    ''Start TFS2823
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 6).Caption = "Opening Pack"
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 6).Visible = False
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 7).Caption = "In Pack"
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 7).Visible = False
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 8).Caption = "Out Pack"
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 8).Visible = False
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 9).Caption = "Close Pack"
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 9).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 9).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 9).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 9).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 9).FormatString = "N" & DecimalPointInValue
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 9).TotalFormatString = "N" & DecimalPointInValue
                    ''End TFS2823
                    ''Start TFS3805
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 10).Caption = "Opening Amount"
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 10).Visible = False
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 11).Caption = "In Amount"
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 11).Visible = False
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 12).Caption = "Out Amount"
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 12).Visible = False
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 13).Caption = "Amount"
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 13).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 13).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 13).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 13).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 13).FormatString = "N" & DecimalPointInValue
                    Me.GrdLocationWiseSummary.RootTable.Columns(c + 13).TotalFormatString = "N" & DecimalPointInValue
                    ''End TFS3805
                    ColumnSet = Me.GrdLocationWiseSummary.RootTable.ColumnSets.Add
                    ''Start TFS3805 : Column Count Changes to 3 ,bcz ClosePack Also Added
                    ColumnSet.ColumnCount = 3
                    ColumnSet.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    ColumnSet.Caption = Me.GrdLocationWiseSummary.RootTable.Columns(c + 1).Caption
                    ColumnSet.Add(Me.GrdLocationWiseSummary.RootTable.Columns(c + 5), 0, 0)
                    ColumnSet.Add(Me.GrdLocationWiseSummary.RootTable.Columns(c + 9), 0, 1)
                    ColumnSet.Add(Me.GrdLocationWiseSummary.RootTable.Columns(c + 13), 0, 2) ''TFS3805
                End If
            Next

            Me.GrdLocationWiseSummary.RootTable.Columns("Close").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GrdLocationWiseSummary.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GrdLocationWiseSummary.RootTable.Columns("ClosePack").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum ''TFS3805
            ColumnSet2 = Me.GrdLocationWiseSummary.RootTable.ColumnSets.Add
            ''Start TFS3805 : Column Count Changes to 2 ,bcz ClosePack Also Added
            ColumnSet2.ColumnCount = 2
            ColumnSet2.Caption = "Total Stock"
            ColumnSet2.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ColumnSet2.Add(Me.GrdLocationWiseSummary.RootTable.Columns("Close"), 0, 0)
            ColumnSet2.Add(Me.GrdLocationWiseSummary.RootTable.Columns("ClosePack"), 0, 1)

            Me.GrdLocationWiseSummary.RootTable.Columns("Close").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GrdLocationWiseSummary.RootTable.Columns("Close").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdLocationWiseSummary.RootTable.Columns("Close").FormatString = "N" & DecimalPointInQty
            Me.GrdLocationWiseSummary.RootTable.Columns("Close").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdLocationWiseSummary.RootTable.Columns("Close").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox

            Me.GrdLocationWiseSummary.RootTable.Columns("ClosePack").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GrdLocationWiseSummary.RootTable.Columns("ClosePack").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdLocationWiseSummary.RootTable.Columns("ClosePack").FormatString = "N" & DecimalPointInQty
            Me.GrdLocationWiseSummary.RootTable.Columns("ClosePack").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdLocationWiseSummary.RootTable.Columns("ClosePack").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox

            ColumnSet3 = Me.GrdLocationWiseSummary.RootTable.ColumnSets.Add
            ''Start TFS2823
            ColumnSet3.ColumnCount = 1
            ColumnSet3.Caption = "Total Amount"
            ColumnSet3.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ColumnSet3.Add(Me.GrdLocationWiseSummary.RootTable.Columns("Amount"), 0, 0)

            Me.GrdLocationWiseSummary.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GrdLocationWiseSummary.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdLocationWiseSummary.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInQty
            Me.GrdLocationWiseSummary.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GrdLocationWiseSummary.RootTable.Columns("Amount").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox

            ''End TFS2823
            'Altered by Ali Ansari against Task#201506030 Text Allignment and sum of close column
            '------------------------------------------------------------'
            Me.grdSummary.Visible = False
            Me.GrdLocationWiseSummary.Visible = True
            'Me.GrdLocationWiseSummary.AutoSizeColumns()
            Me.grd.Visible = False
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillSummaryGrid()
        Try

            'Task# 1-11-06-2015 Loop through grdSummary for Hide all the columns in grdSummary
            For col As Integer = 0 To Me.grdSummary.RootTable.Columns.Count - 1
                Me.grdSummary.RootTable.Columns(col).Visible = False
            Next
            Me.grdSummary.RootTable.Columns("Item").Width = 200

            'Task# 1-11-06-2015 Show some columns from grdSummary
            Me.grdSummary.RootTable.Columns("Code").Visible = True
            Me.grdSummary.RootTable.Columns("Item").Visible = True
            Me.grdSummary.RootTable.Columns("Opening").Visible = True
            Me.grdSummary.RootTable.Columns("In").Visible = True
            Me.grdSummary.RootTable.Columns("Out").Visible = True
            Me.grdSummary.RootTable.Columns("Close").Visible = True


            'Task# 1-11-06-2015 SET format string to grdSummary grid
            Me.grdSummary.RootTable.Columns("Opening").FormatString = "N" & DecimalPointInQty
            Me.grdSummary.RootTable.Columns("In").FormatString = "N" & DecimalPointInQty
            Me.grdSummary.RootTable.Columns("Out").FormatString = "N" & DecimalPointInQty
            Me.grdSummary.RootTable.Columns("Close").FormatString = "N" & DecimalPointInQty

            Me.grdSummary.RootTable.Columns("Opening").TotalFormatString = "N" & DecimalPointInQty
            Me.grdSummary.RootTable.Columns("In").TotalFormatString = "N" & DecimalPointInQty
            Me.grdSummary.RootTable.Columns("Out").TotalFormatString = "N" & DecimalPointInQty
            Me.grdSummary.RootTable.Columns("Close").TotalFormatString = "N" & DecimalPointInQty

            'Task# 1-11-06-2015 calculate total result
            Me.grdSummary.RootTable.Columns("Opening").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSummary.RootTable.Columns("In").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSummary.RootTable.Columns("Out").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSummary.RootTable.Columns("Close").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            'Task# 1-11-06-2015 set text alignment in grdSummmary
            Me.grdSummary.RootTable.Columns("Opening").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("In").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("Out").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("Close").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSummary.RootTable.Columns("Opening").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("In").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("Out").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("Close").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("Close").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox

            'Task#2823  02-04-2018 Show some columns from grdSummary
            Me.grdSummary.RootTable.Columns("OpeningAmount").Visible = True
            Me.grdSummary.RootTable.Columns("InAmount").Visible = True
            Me.grdSummary.RootTable.Columns("OutAmount").Visible = True
            Me.grdSummary.RootTable.Columns("Amount").Visible = True


            'Task#2823 02-04-2018 SET format string to grdSummary grid
            Me.grdSummary.RootTable.Columns("OpeningAmount").FormatString = "N" & DecimalPointInValue
            Me.grdSummary.RootTable.Columns("InAmount").FormatString = "N" & DecimalPointInValue
            Me.grdSummary.RootTable.Columns("OutAmount").FormatString = "N" & DecimalPointInValue
            Me.grdSummary.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue

            Me.grdSummary.RootTable.Columns("OpeningAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSummary.RootTable.Columns("InAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSummary.RootTable.Columns("OutAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSummary.RootTable.Columns("Amount").TotalFormatString = "N" & DecimalPointInValue

            'Task#2823 02-04-2018 calculate total result
            Me.grdSummary.RootTable.Columns("OpeningAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSummary.RootTable.Columns("InAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSummary.RootTable.Columns("OutAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSummary.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            'Task#2823 02-04-2018 set text alignment in grdSummmary
            Me.grdSummary.RootTable.Columns("OpeningAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("InAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("OutAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSummary.RootTable.Columns("OpeningAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("InAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("OutAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("Amount").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox

            'Task#3805  06-07-2018 Show some columns from grdSummary
            Me.grdSummary.RootTable.Columns("OpeningPack").Visible = False
            Me.grdSummary.RootTable.Columns("InPack").Visible = False
            Me.grdSummary.RootTable.Columns("OutPack").Visible = False
            Me.grdSummary.RootTable.Columns("ClosePack").Visible = False


            'Task#3805 06-07-2018 SET format string to grdSummary grid
            Me.grdSummary.RootTable.Columns("OpeningPack").FormatString = "N" & DecimalPointInValue
            Me.grdSummary.RootTable.Columns("InPack").FormatString = "N" & DecimalPointInValue
            Me.grdSummary.RootTable.Columns("OutPack").FormatString = "N" & DecimalPointInValue
            Me.grdSummary.RootTable.Columns("ClosePack").FormatString = "N" & DecimalPointInValue

            Me.grdSummary.RootTable.Columns("OpeningPack").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSummary.RootTable.Columns("InPack").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSummary.RootTable.Columns("OutPack").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSummary.RootTable.Columns("ClosePack").TotalFormatString = "N" & DecimalPointInValue

            'Task#3805 06-07-2018 calculate total result
            Me.grdSummary.RootTable.Columns("OpeningPack").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSummary.RootTable.Columns("InPack").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSummary.RootTable.Columns("OutPack").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSummary.RootTable.Columns("ClosePack").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            'Task#3805 06-07-2018 set text alignment in grdSummmary
            Me.grdSummary.RootTable.Columns("OpeningPack").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("InPack").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("OutPack").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("ClosePack").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSummary.RootTable.Columns("OpeningPack").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("InPack").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("OutPack").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("ClosePack").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSummary.RootTable.Columns("ClosePack").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox

            Me.grd.Visible = False
            Me.grdSummary.Visible = True
            ' Me.grdSummary.AutoSizeColumns()
            Me.GrdLocationWiseSummary.Visible = False
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillGrid(Optional ByVal Condition As String = "")
        Try

            'If rbtnDetailView.Checked = True Then
            Dim dtItems As New DataTable
            'dtItems = GetDataTable("Select ArticleId, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Color,ArticleGroupName as Department, ArticleTypeName as [Type], ArticleBrandName as [Brand] From ArticleDefView Order By ArticleDescription ASC")

            dtItems = GetDataTable("Select ArticleId, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Color,ArticleGroupName as Department, ArticleTypeName as [Type], ArticleBrandName as [Brand] From ArticleDefView where ArticleStatusID= " & cmbStatus.SelectedValue & " Order By ArticleDescription ASC")
            dtItems.AcceptChanges()

            Dim IDs As String = String.Empty
            For Each obj As Object In Me.lstLocation.ListItem.SelectedItems
                If TypeOf obj Is DataRowView Then
                    Dim dr As DataRowView = CType(obj, DataRowView)
                    IDs = IDs & IIf(IDs.Length > 0, ",", "") & dr.Row.Item(dr.Row.Table.Columns(Me.lstLocation.ListItem.ValueMember).ColumnName)
                End If
            Next

            Dim dtLocations As New DataTable
            dtLocations = GetDataTable("Select Location_Id, Location_Name,Location_Type From tblDefLocation Where Location_Id In(" & IDs & ") Order BY Location_Code ASC")
            dtLocations.AcceptChanges()

            If dtItems IsNot Nothing Then
                If dtItems.Rows.Count > 0 Then
                    If dtLocations IsNot Nothing Then
                        If dtLocations.Rows.Count > 0 Then
                            For Each dRow As DataRow In dtLocations.Rows
                                If Not dtItems.Columns.Contains(dRow.Item("Location_Name")) Then
                                    dtItems.Columns.Add(dRow.Item("Location_Id").ToString, GetType(System.Int32), dRow.Item("Location_Id").ToString)
                                    dtItems.Columns.Add(dRow.Item("Location_Name").ToString, GetType(System.String))
                                    dtItems.Columns.Add(dRow.Item("Location_Id").ToString & "^" & "Open", GetType(System.Double))
                                    dtItems.Columns.Add(dRow.Item("Location_Id").ToString & "^" & "In", GetType(System.Double))
                                    dtItems.Columns.Add(dRow.Item("Location_Id").ToString & "^" & "Out", GetType(System.Double))
                                    dtItems.Columns.Add(dRow.Item("Location_Id").ToString & "^" & "Close", GetType(System.Double))
                                    dtItems.Columns.Add(dRow.Item("Location_Id").ToString & "^" & "OpenPack", GetType(System.Double)) ''TFS3805
                                    dtItems.Columns.Add(dRow.Item("Location_Id").ToString & "^" & "InPack", GetType(System.Double)) ''TFS3805
                                    dtItems.Columns.Add(dRow.Item("Location_Id").ToString & "^" & "OutPack", GetType(System.Double)) ''TFS3805
                                    dtItems.Columns.Add(dRow.Item("Location_Id").ToString & "^" & "ClosePack", GetType(System.Double)) ''TFS3805
                                    dtItems.Columns.Add(dRow.Item("Location_Id").ToString & "^" & "OpenAmount", GetType(System.Double)) ''TFS2823
                                    dtItems.Columns.Add(dRow.Item("Location_Id").ToString & "^" & "InAmount", GetType(System.Double)) ''TFS2823
                                    dtItems.Columns.Add(dRow.Item("Location_Id").ToString & "^" & "OutAmount", GetType(System.Double)) ''TFS2823
                                    dtItems.Columns.Add(dRow.Item("Location_Id").ToString & "^" & "Amount", GetType(System.Double)) ''TFS2823
                                    dtItems.AcceptChanges()
                                End If
                            Next
                        End If
                    End If
                End If
            End If


            For Each dRow As DataRow In dtItems.Rows
                '' TFS3805 : Ayesha Rehman : 07-06-2018 :Count Step to 14 ,bcz 4 new columns are added 
                For c As Integer = enmItems.Count To dtItems.Columns.Count - 14 Step 14
                    dRow.BeginEdit()
                    dRow.Item(c + 2) = 0
                    dRow.Item(c + 3) = 0
                    dRow.Item(c + 4) = 0
                    dtItems.Columns(c + 5).Expression = "(([" & dtItems.Columns(c + 2).ColumnName.ToString & "]" & "+" & "[" & dtItems.Columns(c + 3).ColumnName.ToString & "])-[" & dtItems.Columns(c + 4).ColumnName.ToString & "])"
                    dRow.Item(c + 6) = 0 ''TFS2823
                    dRow.Item(c + 7) = 0 ''TFS2823
                    dRow.Item(c + 8) = 0 ''TFS2823
                    dtItems.Columns(c + 9).Expression = "(([" & dtItems.Columns(c + 6).ColumnName.ToString & "]" & "+" & "[" & dtItems.Columns(c + 7).ColumnName.ToString & "])-[" & dtItems.Columns(c + 8).ColumnName.ToString & "])" ''TFS2823
                    dRow.Item(c + 10) = 0 ''TFS3805
                    dRow.Item(c + 11) = 0 ''TFS3805
                    dRow.Item(c + 12) = 0 ''TFS3805
                    dtItems.Columns(c + 13).Expression = "(([" & dtItems.Columns(c + 10).ColumnName.ToString & "]" & "+" & "[" & dtItems.Columns(c + 11).ColumnName.ToString & "])-[" & dtItems.Columns(c + 12).ColumnName.ToString & "])" ''TFS3805
                    dRow.EndEdit()
                Next
            Next


            'Dim IDs As String = String.Empty
            'For Each obj As Object In Me.lstLocation.SelectedItems
            '    If TypeOf obj Is DataRowView Then
            '        Dim dr As DataRowView = CType(obj, DataRowView)
            '        IDs = IDs & IIf(IDs.Length > 0, ",", "") & dr.Row.Item(dr.Row.Table.Columns(Me.lstLocation.ValueMember).ColumnName)
            '    End If
            'Next

            Dim dtData As New DataTable
            ''Commented Against TFS2823
            'dtData = GetDataTable("Select ArticleDefID,LocationId, Sum(Opening) as Opening,Sum(In_Stock) as In_Stock,Sum(Out_Stock) as Out_Stock From(Select Type,ArticleDefId,LocationId,Case When Type='Opening' Then OpeningStock Else 0 End as Opening, Case When Type='InStock' Then OpeningStock Else 0 End as In_Stock,Case When Type='OutStock' Then OpeningStock Else 0 End as Out_Stock From( " _
            '  & " Select 'Opening' as Type, ArticleDefId, LocationId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) as OpeningStock From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) < Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102)) AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & " Group By ArticleDefId, LocationId " _
            '  & " Union All " _
            '  & " Select 'InStock' as Type, ArticleDefId, LocationId, SUM(IsNull(InQty,0)) as InStock From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & "  Group By ArticleDefId, LocationId " _
            '  & " Union All " _
            '  & " Select 'OutStock' as Type, ArticleDefId, LocationId, SUM(IsNull(OutQty,0)) as OutStock From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & " Group By ArticleDefId, LocationId " _
            '  & " ) aStock) a Group By ArticleDefID,LocationId")

            ''Commented Against TFS3805
            '   dtData = GetDataTable("Select ArticleDefID,LocationId, Sum(Opening) as Opening,Sum(In_Stock) as In_Stock,Sum(Out_Stock) as Out_Stock , Sum(OpeningAmount) as OpeningAmount,Sum(In_Amount) as In_Amount, Sum(Out_Amount) as Out_Amount From(Select Type,ArticleDefId,LocationId,Case When Type='Opening' Then OpeningStock Else 0 End as Opening, Case When Type='InStock' Then OpeningStock Else 0 End as In_Stock,Case When Type='OutStock' Then OpeningStock Else 0 End as Out_Stock , " _
            '& " Case When Type='OpeningAmount' Then OpeningStock Else 0 End as OpeningAmount,Case When Type='InAmount' Then OpeningStock Else 0 End as In_Amount,Case When Type='OutAmount' Then OpeningStock Else 0 End as Out_Amount  From( " _
            '& " Select 'Opening' as Type, ArticleDefId, LocationId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) as OpeningStock From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) < Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102)) AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & " Group By ArticleDefId, LocationId " _
            '& " Union All " _
            '& " Select 'InStock' as Type, ArticleDefId, LocationId, SUM(IsNull(InQty,0)) as InStock From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & "  Group By ArticleDefId, LocationId " _
            '& " Union All " _
            '& " Select 'OutStock' as Type, ArticleDefId, LocationId, SUM(IsNull(OutQty,0)) as OutStock From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & " Group By ArticleDefId, LocationId " _
            '& " Union All " _
            '& " Select 'OpeningAmount' as Type, ArticleDefId, LocationId, SUM(IsNull(InAmount,0)-IsNull(OutAmount,0)) as OpeningAmount From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) < Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102))  AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & "  Group By ArticleDefId, LocationId " _
            '& " Union All " _
            '& " Select 'InAmount' as Type, ArticleDefId, LocationId, SUM(IsNull(InAmount,0)) as InAmount From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & " Group By ArticleDefId, LocationId " _
            '& " Union All " _
            '& " Select 'OutAmount' as Type, ArticleDefId, LocationId, SUM(IsNull(OutAmount,0)) as OutAmount From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & "  Group By ArticleDefId, LocationId " _
            '& " ) aStock) a Group By ArticleDefID,LocationId")
            ''TFS3805 :  Ayesha Rehman : Added Pack info in the query
            dtData = GetDataTable("Select ArticleDefID,LocationId, Sum(Opening) as Opening,Sum(In_Stock) as In_Stock,Sum(Out_Stock) as Out_Stock , Sum(OpeningPack) as OpeningPack , Sum(In_Pack) as In_Pack , Sum(Out_Pack) as Out_Pack , Sum(OpeningAmount) as OpeningAmount,Sum(In_Amount) as In_Amount, Sum(Out_Amount) as Out_Amount From(Select Type,ArticleDefId,LocationId,Case When Type='Opening' Then OpeningStock Else 0 End as Opening, Case When Type='InStock' Then OpeningStock Else 0 End as In_Stock,Case When Type='OutStock' Then OpeningStock Else 0 End as Out_Stock , " _
          & " Case When Type='OpeningPack' Then OpeningStock Else 0 End as OpeningPack,Case When Type='InPack' Then OpeningStock Else 0 End as In_Pack, Case When Type='OutPack' Then OpeningStock Else 0 End as Out_Pack , " _
          & " Case When Type='OpeningAmount' Then OpeningStock Else 0 End as OpeningAmount,Case When Type='InAmount' Then OpeningStock Else 0 End as In_Amount,Case When Type='OutAmount' Then OpeningStock Else 0 End as Out_Amount  From( " _
          & " Select 'Opening' as Type, ArticleDefId, LocationId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) as OpeningStock From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) < Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102)) AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & " Group By ArticleDefId, LocationId " _
          & " Union All " _
          & " Select 'InStock' as Type, ArticleDefId, LocationId, SUM(IsNull(InQty,0)) as InStock From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & "  Group By ArticleDefId, LocationId " _
          & " Union All " _
          & " Select 'OutStock' as Type, ArticleDefId, LocationId, SUM(IsNull(OutQty,0)) as OutStock From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & " Group By ArticleDefId, LocationId " _
          & " Union All " _
          & " Select 'OpeningAmount' as Type, ArticleDefId, LocationId, SUM(IsNull(InAmount,0)-IsNull(OutAmount,0)) as OpeningAmount From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) < Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102))  AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & "  Group By ArticleDefId, LocationId " _
          & " Union All " _
          & " Select 'InAmount' as Type, ArticleDefId, LocationId, SUM(IsNull(InAmount,0)) as InAmount From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & " Group By ArticleDefId, LocationId " _
          & " Union All " _
          & " Select 'OutAmount' as Type, ArticleDefId, LocationId, SUM(IsNull(OutAmount,0)) as OutAmount From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE (Convert(Varchar,DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & "  Group By ArticleDefId, LocationId " _
          & " Union All  " _
          & " Select 'OpeningPack' as Type, ArticleDefId, LocationId, (Case when StockDetailTable.Pack_Qty  = 1 and In_PackQty> 0 and InQty > 0 then  Sum(IsNull((InQty/ArticleDefView.PackQty),0)) else SUM(IsNull(In_PackQty,0)) end  ) - " _
          & "(Case when StockDetailTable.Pack_Qty = 1 and Out_PackQty > 0 and OutQty > 0 then  Sum(IsNull((OutQty/ArticleDefView.PackQty),0)) else SUM(IsNull(Out_PackQty,0)) end ) as OpeningPack From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId Left Outer Join ArticleDefView on StockDetailTable.ArticleDefId = ArticleDefView.ArticleId  WHERE (Convert(Varchar,DocDate,102) < Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102))  AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & "   Group By ArticleDefId, LocationId ,StockDetailTable.Pack_Qty ,StockDetailTable.In_PackQty  ,StockDetailTable.Out_PackQty  ,StockDetailTable.InQty  ,StockDetailTable.OutQty  " _
          & " Union All " _
          & " Select 'InPack' as Type, ArticleDefId, LocationId, Case when StockDetailTable.Pack_Qty  = 1 and In_PackQty > 0 and InQty > 0 " _
          & " then  Sum(IsNull((InQty/ArticleDefView.PackQty),0)) else SUM(IsNull(In_PackQty,0)) end as InPack From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId Left Outer Join ArticleDefView on StockDetailTable.ArticleDefId = ArticleDefView.ArticleId WHERE (Convert(Varchar,DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & " Group By ArticleDefId, LocationId ,StockDetailTable.Pack_Qty ,StockDetailTable.In_PackQty ,StockDetailTable.InQty  " _
          & " Union All  " _
          & " Select 'OutPack' as Type, ArticleDefId, LocationId, Case when StockDetailTable.Pack_Qty  = 1 and Out_PackQty > 0 and OutQty > 0 " _
          & " then  Sum(IsNull((OutQty/ArticleDefView.PackQty),0)) else SUM(IsNull(Out_PackQty,0)) end as OutPack From StockDetailTable LEFT OUTER JOIN StockMasterTable on StockMasterTable.StockTransId = StockDetailTable.StockTransId Left Outer Join ArticleDefView on StockDetailTable.ArticleDefId = ArticleDefView.ArticleId WHERE (Convert(Varchar,DocDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)) AND LocationId In(" & IDs & ") " & IIf(Me.cmbProject.SelectedValue > 0, " And StockMasterTable.Project =" & Me.cmbProject.SelectedValue & "", "") & "   Group By ArticleDefId, LocationId , StockDetailTable.Pack_Qty ,StockDetailTable.Out_PackQty ,StockDetailTable.OutQty ) aStock) a Group By ArticleDefID,LocationId")


            For Each dRow As DataRow In dtItems.Rows

                Dim dr() As DataRow = dtData.Select("ArticleDefID=" & Val(dRow.Item("ArticleId").ToString) & "")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            dRow.BeginEdit()
                            '' TFS3805 : Indexing of Opening Amount,InAmount,Out Amount Changes in query thus Corresponding Code is changed
                            dRow(dtItems.Columns.IndexOf(drFound(1)) + 2) = Val(drFound(2).ToString)
                            dRow(dtItems.Columns.IndexOf(drFound(1)) + 3) = Val(drFound(3).ToString)
                            dRow(dtItems.Columns.IndexOf(drFound(1)) + 4) = Val(drFound(4).ToString)
                            dRow(dtItems.Columns.IndexOf(drFound(1)) + 6) = Val(drFound(5).ToString) ''TFS2823
                            dRow(dtItems.Columns.IndexOf(drFound(1)) + 7) = Val(drFound(6).ToString) ''TFS2823
                            dRow(dtItems.Columns.IndexOf(drFound(1)) + 8) = Val(drFound(7).ToString) ''TFS2823
                            dRow(dtItems.Columns.IndexOf(drFound(1)) + 10) = Val(drFound(8).ToString) ''TFS3805
                            dRow(dtItems.Columns.IndexOf(drFound(1)) + 11) = Val(drFound(9).ToString) ''TFS3805
                            dRow(dtItems.Columns.IndexOf(drFound(1)) + 12) = Val(drFound(10).ToString) ''TFS3805
                            dRow.EndEdit()
                        Next
                    End If
                End If
            Next
            dtItems.AcceptChanges()
            dtItems.Columns.Add("Opening", GetType(System.Double))
            dtItems.Columns.Add("In", GetType(System.Double))
            dtItems.Columns.Add("Out", GetType(System.Double))
            dtItems.Columns.Add("Close", GetType(System.Double))
            dtItems.Columns.Add("OpeningPack", GetType(System.Double)) ''TFS3805
            dtItems.Columns.Add("InPack", GetType(System.Double)) ''TFS3805
            dtItems.Columns.Add("OutPack", GetType(System.Double)) ''TFS3805
            dtItems.Columns.Add("ClosePack", GetType(System.Double)) ''TFS3805
            dtItems.Columns.Add("OpeningAmount", GetType(System.Double)) ''TFS2823
            dtItems.Columns.Add("InAmount", GetType(System.Double)) ''TFS2823
            dtItems.Columns.Add("OutAmount", GetType(System.Double)) ''TFS2823
            dtItems.Columns.Add("Amount", GetType(System.Double)) ''TFS2823

            dtItems.AcceptChanges()

            Dim strTotalOpening As String = String.Empty
            Dim strTotalIn As String = String.Empty
            Dim strTotalOut As String = String.Empty

            Dim strTotalOpeningAmount As String = String.Empty ''TFS2823
            Dim strTotalInAmount As String = String.Empty ''TFS2823
            Dim strTotalOutAmount As String = String.Empty ''TFS2823

            Dim strTotalOpeningPack As String = String.Empty ''TFS3805
            Dim strTotalInPack As String = String.Empty ''TFS3805
            Dim strTotalOutPack As String = String.Empty ''TFS3805

            '' TFS3805 : Ayesha Rehman : 07-06-2018 :Count Step to 14 ,bcz 4 new columns are added 
            '' TFS3805 : Indexing of Opening Amount,InAmount,Out Amount Changes in query thus Corresponding Code is changed
            For c As Integer = enmItems.Count To dtItems.Columns.Count - 14 Step 14
                strTotalOpening += "[" & dtItems.Columns(c + 2).ColumnName.ToString & "]+"
                strTotalIn += "[" & dtItems.Columns(c + 3).ColumnName.ToString & "]+"
                strTotalOut += "[" & dtItems.Columns(c + 4).ColumnName.ToString & "]+"

                strTotalOpeningPack += "[" & dtItems.Columns(c + 6).ColumnName.ToString & "]+" ''TFS3805
                strTotalInPack += "[" & dtItems.Columns(c + 7).ColumnName.ToString & "]+" ''TFS3805
                strTotalOutPack += "[" & dtItems.Columns(c + 8).ColumnName.ToString & "]+" ''TFS3805

                strTotalOpeningAmount += "[" & dtItems.Columns(c + 10).ColumnName.ToString & "]+" ''TFS2823
                strTotalInAmount += "[" & dtItems.Columns(c + 11).ColumnName.ToString & "]+" ''TFS2823
                strTotalOutAmount += "[" & dtItems.Columns(c + 12).ColumnName.ToString & "]+" ''TFS2823

            Next

            strTotalOpening = strTotalOpening.Substring(0, strTotalOpening.LastIndexOf("+"))
            strTotalIn = strTotalIn.Substring(0, strTotalIn.LastIndexOf("+"))
            strTotalOut = strTotalOut.Substring(0, strTotalOut.LastIndexOf("+"))

            strTotalOpeningAmount = strTotalOpeningAmount.Substring(0, strTotalOpeningAmount.LastIndexOf("+")) ''TFS2823
            strTotalInAmount = strTotalInAmount.Substring(0, strTotalInAmount.LastIndexOf("+")) ''TFS2823
            strTotalOutAmount = strTotalOutAmount.Substring(0, strTotalOutAmount.LastIndexOf("+")) ''TFS2823

            strTotalOpeningPack = strTotalOpeningPack.Substring(0, strTotalOpeningPack.LastIndexOf("+")) ''TFS3805
            strTotalInPack = strTotalInPack.Substring(0, strTotalInPack.LastIndexOf("+")) ''TFS3805
            strTotalOutPack = strTotalOutPack.Substring(0, strTotalOutPack.LastIndexOf("+")) ''TFS3805

            dtItems.Columns("Opening").Expression = strTotalOpening.ToString
            dtItems.Columns("In").Expression = strTotalIn.ToString
            dtItems.Columns("Out").Expression = strTotalOut.ToString
            dtItems.Columns("Close").Expression = "(([Opening]+[In])-[Out])"

            dtItems.Columns("OpeningAmount").Expression = strTotalOpeningAmount.ToString ''TFS2823
            dtItems.Columns("InAmount").Expression = strTotalInAmount.ToString ''TFS2823
            dtItems.Columns("OutAmount").Expression = strTotalOutAmount.ToString ''TFS2823
            dtItems.Columns("Amount").Expression = "(([OpeningAmount]+[InAmount])-[OutAmount])" ''TFS2823

            dtItems.Columns("OpeningPack").Expression = strTotalOpeningPack.ToString ''TFS3805
            dtItems.Columns("InPack").Expression = strTotalInPack.ToString ''TFS3805
            dtItems.Columns("OutPack").Expression = strTotalOutPack.ToString ''TFS3805
            dtItems.Columns("ClosePack").Expression = "(([OpeningPack]+[InPack])-[OutPack])" ''TFS3805
            dtItems.AcceptChanges()




            'Task# 1-11-06-2015, Check if rbtnDetailView is true ,then display  stock in detail if false then display stock in summarized form
            'Marked by Ali Ansari against Task#201506030 to add option of location wise grid
            'If rbtnDetailView.Checked = True Then
            '    Me.grdSummary.Visible = False
            '    Me.grd.Visible = True
            '    Me.GrdLocationWiseSummary.Visible = False
            '    Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
            'Else
            '    Me.grd.Visible = False
            '    Me.grdSummary.Visible = True
            '    Me.GrdLocationWiseSummary.Visible = False
            '    Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
            'End If
            'Marked by Ali Ansari against Task#201506030 to add option of location wise grid

            'Altered by Ali Ansari against Task#201506030 to add option of location wise grid
            If rbtnDetailView.Checked = True Then
                Me.grd.DataSource = dtItems
                Me.grd.RetrieveStructure()
                Call filldetailgrid()
            ElseIf rbtnSummaryView.Checked = True Then
                Me.grdSummary.DataSource = dtItems
                Me.grdSummary.RetrieveStructure()
                Call FillSummaryGrid()
            ElseIf RbtLocationWiseSummary.Checked = True Then
                Me.GrdLocationWiseSummary.DataSource = dtItems
                Me.GrdLocationWiseSummary.RetrieveStructure()
                Call filllLocationWiseSummaryGrid()
            End If
            'Altered by Ali Ansari against Task#201506030 to add option of location wise grid




            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab

            'End Task# 1-11-06-2015

            CtrlGrdBar1_Load(Nothing, Nothing)




        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmGrdRptLocationWiseStockStatementNew_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            GetSecurityRights()
            Me.dtpFrom.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpTo.Value = Date.Now
            FillList()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If rbtnDetailView.Checked = True Then
                CtrlGrdBar1.MyGrid = Me.grd
                CtrlGrdBar1.FormName = Me
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                    Me.grd.LoadLayoutFile(fs)
                    fs.Dispose()
                    fs.Close()
                End If
            ElseIf RbtLocationWiseSummary.Checked = True Then
                'GrdLocationWiseSummary
                CtrlGrdBar1.MyGrid = Me.GrdLocationWiseSummary
                CtrlGrdBar1.FormName = Me
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdLocationWiseSummary.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdLocationWiseSummary.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                    Me.grd.LoadLayoutFile(fs)
                    fs.Dispose()
                    fs.Close()
                End If
            Else
                CtrlGrdBar1.MyGrid = Me.grdSummary
                CtrlGrdBar1.FormName = Me
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSummary.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSummary.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                    Me.grd.LoadLayoutFile(fs)
                    fs.Dispose()
                    fs.Close()
                End If
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Location Wise Stock" & Chr(10) & "From Date: " & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & " To Date: " & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 0 Then
                Me.CtrlGrdBar1.Visible = False
            Else
                Me.CtrlGrdBar1.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RptLocationWiseSummary_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtLocationWiseSummary.CheckedChanged

    End Sub

    Private Sub rbtnDetailView_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDetailView.CheckedChanged

    End Sub

    Private Sub rbtnDetailView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnDetailView.Click

    End Sub

    Private Sub rbtnSummaryView_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSummaryView.CheckedChanged

    End Sub
End Class