Imports SBModel
Public Class frmGrdRptLocationWiseStockStatement

    Private Sub frmGrdRptLocationWiseStockStatement_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Cursor = Cursors.WaitCursor
        Try
            GetSecurityRights()

            Me.dtpDateFrom.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpDateTo.Value = Date.Now

            FillListBox(Me.lstLocations.ListItem, "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order")



        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            FillListBox(Me.lstLocations.ListItem, "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order Else Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order")
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
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptLocationWiseStockStatement)
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
    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
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
            Dim strSQL As String = String.Empty
            strSQL = "SP_LocationWiseStock '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "','" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "'"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.TableName = "Stock Report"

            dt.Columns.Add("Closing Stock", GetType(System.Double))
            dt.Columns.Add("Stock Amount", GetType(System.Double))


            dt.Columns("Closing Stock").Expression = "(([Opening]+[Stock In])-[Stock Out])"
            dt.Columns("Stock Amount").Expression = "(([OpeningAmount]+[InAmount])-[OutAmount])"

            dt.AcceptChanges()

            Dim dv As New DataView
            dv.Table = dt


            dv.RowFilter = " [Item] <> ''"
            If Me.lstLocations.SelectedIDs.Length > 0 Then
                dv.RowFilter += " AND Location_Id IN (" & Me.lstLocations.SelectedIDs & ")"
            End If

            dv.ToTable.AcceptChanges()

            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab

            Me.grdReport.ClearStructure()
            Me.grdReport.DataSource = dv.ToTable
            Me.grdReport.RetrieveStructure()

            Me.grdReport.RootTable.Columns("Location_ID").Visible = False
            Me.grdReport.RootTable.Columns("MASTERID").Visible = False
            Me.grdReport.RootTable.Columns("ArticleId").Visible = False
            Me.grdReport.RootTable.Columns("Add").Visible = False
            Me.grdReport.FrozenColumns = 9
            Dim grpItemGroup As New Janus.Windows.GridEX.GridEXGroup
            grpItemGroup.Column = Me.grdReport.RootTable.Columns("Department")
            Me.grdReport.RootTable.Groups.Add(grpItemGroup)

            Dim grpItemType As New Janus.Windows.GridEX.GridEXGroup
            grpItemType.Column = Me.grdReport.RootTable.Columns("Item Type")
            Me.grdReport.RootTable.Groups.Add(grpItemType)

            Me.grdReport.RootTable.Columns("Opening").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("OpeningAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Stock Out").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("OutAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Stock In").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("InAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Closing Stock").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdReport.RootTable.Columns("Stock Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdReport.RootTable.Columns("Opening").FormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("OpeningAmount").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Stock Out").FormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("OutAmount").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Stock In").FormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("InAmount").FormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Closing Stock").FormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("Stock Amount").FormatString = "N" & DecimalPointInValue

            Me.grdReport.RootTable.Columns("Opening").TotalFormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("OpeningAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Stock Out").TotalFormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("OutAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Stock In").TotalFormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("InAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdReport.RootTable.Columns("Closing Stock").TotalFormatString = "N" & DecimalPointInQty
            Me.grdReport.RootTable.Columns("Stock Amount").TotalFormatString = "N" & DecimalPointInValue


            Me.grdReport.RootTable.Columns("Opening").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("OpeningAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Stock Out").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("OutAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Stock In").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("InAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Closing Stock").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Stock Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdReport.RootTable.Columns("Opening").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("OpeningAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Stock Out").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("OutAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Stock In").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("InAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Closing Stock").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdReport.RootTable.Columns("Stock Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdReport.RootTable.Columns("OpeningAmount").Caption = "Amount"
            Me.grdReport.RootTable.Columns("OutAmount").Caption = "Amount"
            Me.grdReport.RootTable.Columns("InAmount").Caption = "Amount"


            Me.grdReport.RootTable.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdReport.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always

            CtrlGrdBar1_Load(Nothing, Nothing)

            Me.grdReport.AutoSizeColumns()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdReport.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdReport.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdReport.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Location Wise Stock Statement" & vbCrLf & "Date From: " & Me.dtpDateFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpDateTo.Value.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                Me.btnRefresh.Visible = False
            Else
                Me.btnRefresh.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class