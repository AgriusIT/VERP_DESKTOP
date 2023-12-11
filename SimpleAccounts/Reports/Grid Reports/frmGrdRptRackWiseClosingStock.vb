Public Class frmGrdRptRackWiseClosingStock
    Dim _SearchDt As New DataTable
    Public str_ReportParam As String = ""

    Private Sub FillCombos()
        Try
            Dim str As String = String.Empty
            '04-July-2017: Task # TFS1037: Waqar Raza: Added query to fill dropdown of location by checking user location rights for HFR.
            'Start Task # TFS1037:
            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & " And Location_Id Is Not Null) " _
                    & " Select Location_Id, Location_Code from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                    & " Else " _
                    & " Select Location_Id, Location_Code from tblDefLocation order by sort_order"
            FillDropDown(cmbLocation, str, False)
            'End Task # TFS1037:
            FillListBox(Me.UiListControl1.ListItem, "Select ArticleId, ArticleCode + ' ~ ' + ArticleDescription as ArticleDescription,ArticleCode From ArticleDefView WHERE Active=1 Order By ArticleDefView.SortOrder, ArticleDefView.ArticleCode ASC")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillGrid(Optional Condition As String = "")
        Try
            Dim dtData As New DataTable
            '04-July-2017: Task # TFS1037: Waqar Raza: To pass parameter value of selected location dropdown to SP_RackWiseClosingStock
            Dim str As String = "Exec SP_RackWiseClosingStock " & Me.cmbLocation.SelectedValue & ""
            'End Task # TFS1037:
            dtData = GetDataTable(str)
            dtData.AcceptChanges()

            If Condition.Length > 0 Then
                Dim dv As New DataView
                dtData.TableName = "Default"
                dv.Table = dtData
                dv.RowFilter = "ArticleID in(" & Condition & ")"
                Me.grd.DataSource = dv.ToTable
            Else
                Me.grd.DataSource = dtData
            End If

            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns(0).Visible = False


            ApplyGridSetting()

            Me.CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ApplyGridSetting()
        Try

            Dim grp1 As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Department"))
            Dim grp2 As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Type"))
            Dim grp3 As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Brand"))
            Dim grp4 As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Category"))
            Dim grp5 As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Sub Category"))

            Me.grd.RootTable.Groups.Add(grp1)
            Me.grd.RootTable.Groups.Add(grp2)
            Me.grd.RootTable.Groups.Add(grp3)
            Me.grd.RootTable.Groups.Add(grp4)
            Me.grd.RootTable.Groups.Add(grp5)

            Me.grd.RootTable.Columns("Closing").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Closing").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Closing").TotalFormatString = "N" & DecimalPointInQty

            Me.grd.RootTable.Columns("Closing").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Closing").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("ClosingAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("ClosingAmount").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("ClosingAmount").TotalFormatString = "N" & DecimalPointInQty

            Me.grd.RootTable.Columns("ClosingAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ClosingAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '04-July-2017: Task # TFS: Waqar Raza: Added  Security Rights on HFR demand..
    'Start Task # TFS1036:
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptRackWiseClosingStock)
            Else
                Me.Visible = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    'Start Task # TFS1036:

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        Try

            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt
            dv.RowFilter = "ArticleDescription Like '%" & Me.TextBox1.Text & " %'"
            Me.UiListControl1.ListItem.DataSource = dv.ToTable

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdRptRackWiseClosingStock_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            FillCombos()
            _SearchDt = CType(Me.UiListControl1.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            FillGrid()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Rack Wise Closing Stock" & vbCrLf & "Location: " & Me.cmbLocation.Text & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try

            Dim strIDs As String = String.Empty
            strIDs = Me.UiListControl1.SelectedIDs
            FillGrid(strIDs)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    '12-July-2017: Task # TFS1042: Waqar Raza: To set visiblity on tab index changed.
    'Start TSF1063:
    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                Me.CtrlGrdBar1.Visible = True
                Me.btnPrint.Visible = True
            Else
                Me.CtrlGrdBar1.Visible = False
                Me.btnPrint.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End TSF1063:
    '12-July-2017: Task # TFS1063: Waqar Raza: To Print Report by clicking on Print Button
    'Start TFS1063:
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@LocationId", Me.cmbLocation.SelectedValue)
            ShowReport("rptRackWiseStockStatement")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End TFS1063:
End Class