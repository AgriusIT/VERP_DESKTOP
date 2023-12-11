Imports System.Windows.Forms

Public Class frmSOStatus
    Private _dt As DataTable
    Private _SOHistory As Boolean = True
    Enum SalesOrderEnm
        SalesOrderNo
        SalesOrderDate
        Delivery_Date
        detail_title
        Remarks
        SoQty
        SalesQty
        DifferenceQty
        Status
        SalesOrderId
    End Enum
    Private flgCompanyRights As Boolean = False

    'Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    '    Dim trans As OleDb.OleDbTransaction
    '    Try
    '        Dim cmd As New OleDb.OleDbCommand
    '        cmd.Connection = Con
    '        If Con.State = ConnectionState.Open Then Con.Close()

    '        Con.Open()

    '        trans = Con.BeginTransaction

    '        cmd.CommandType = CommandType.Text
    '        cmd.Transaction = trans

    '        If Me.grdSaved.RowCount = 0 Then Exit Sub
    '        If Not Me.IsValidate() Then Exit Sub
    '        If Not msg_Confirm("Are you sure you want to update the status of selected records") Then Exit Sub
    '        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
    '            If r.Cells(0).Value = True Then
    '                cmd.CommandText = "Update SalesOrderMasterTable set Status = '" & Me.cmbSetTo.SelectedItem.ToString & "' where SalesOrderID = " & r.Cells("SalesOrderID").Value & ""
    '                cmd.ExecuteNonQuery()
    '            End If
    '        Next
    '        trans.Commit()

    '        msg_Information("Records has been updated successfully")
    '        ''insert Activity Log
    '        SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Sales, "abc")
    '        Me.DisplayRecord()
    '        Me.GetSecurityRights()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        msg_Error(ex.Message)
    '    Finally
    '        Con.Close()
    '    End Try

    'End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DisplayRecord(Optional ByVal Condition As String = "")
        Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
        Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
        Dim str As String
        'str = "SELECT     Recv.SalesNo, CONVERT(varchar, Recv.SalesDate, 103) AS Date, V.CustomerName, Recv.SalesQty, Recv.SalesAmount, " _
        '       & " Recv.SalesId, Recv.CustomerCode, Recv.Remarks, convert(varchar, Recv.CashPaid) as CashPaid FROM         SalesMasterTable Recv INNER JOIN tblCustomer V ON Recv.CustomerCode = V.AccountId"

        str = "SELECT SalesOrderMasterTable.SalesOrderNo, SalesOrderMasterTable.SalesOrderDate, SalesOrderMasterTable.PONo ,SalesOrderMasterTable.Delivery_Date as [Dev Date], " _
                & " vwCOADetail.detail_title, SalesOrderMasterTable.Remarks, SUM(SalesOrderDetailTable.qty) AS SOQty, ISNULL(SUM(SalesOrderDetailTable.DeliveredTotalQty), 0) " _
                & " AS SalesQty, SUM(SalesOrderDetailTable.qty) - ISNULL(SUM(SalesOrderDetailTable.DeliveredTotalQty), 0) AS DifferenceQty,  SalesOrderMasterTable.Status," _
                & " SalesOrderMasterTable.SalesOrderId, SalesOrderStatusTable.OrderStatus as Type " _
                & " FROM SalesOrderDetailTable INNER JOIN " _
                & " SalesOrderMasterTable ON SalesOrderDetailTable.SalesOrderId = SalesOrderMasterTable.SalesOrderId INNER JOIN " _
                & " vwCOADetail ON SalesOrderMasterTable.VendorID = vwCOADetail.coa_detail_id LEFT OUTER JOIN  SalesOrderStatusTable on SalesOrderStatusTable.OrderStatusID  = SalesOrderMasterTable.OrderType  " _
                & " " & IIf(Me.cmbStatus.Text = "All", "WHERE  SalesOrderMasterTable.Status In('Open', 'Close', 'Reject', 'DeActive')", "WHERE  SalesOrderMasterTable.Status = '" & Me.cmbStatus.Text & "'") & ""

        If flgCompanyRights = True Then
            str += " AND vwCOADetail.CompanyId=" & MyCompanyId & ""
        End If
        If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
            str = str & " AND SalesOrderMasterTable.VendorID = " & Me.cmbCustomer.ActiveRow.Cells(0).Value & ""
        End If
        If Me.cmbOrderType.SelectedIndex > 0 Then
            str += " AND IsNull(SalesOrderMasterTable.OrderType,0)=" & Me.cmbOrderType.SelectedValue & ""
        End If

        str = str & " GROUP BY SalesOrderMasterTable.SalesOrderNo, SalesOrderMasterTable.SalesOrderNo, SalesOrderMasterTable.SalesOrderDate, vwCOADetail.detail_title,  SalesOrderMasterTable.Remarks," _
                & "  SalesOrderMasterTable.SalesOrderId, SalesOrderMasterTable.Status, SalesOrderMasterTable.Delivery_Date,  SalesOrderMasterTable.PONo , SalesOrderStatusTable.OrderStatus " _
                & " HAVING 1 = 1 "

        If Me.dtpFromDate.Checked Then
            str = str & " AND (Convert(varchar, SalesOrderMasterTable.SalesOrderDate,102) >= Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) "
        End If

        If Me.dtpToDate.Checked Then
            str = str & " AND (Convert(varchar, SalesOrderMasterTable.SalesOrderDate,102) <= Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) "
        End If
        str = str & " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar , SalesOrderMasterTable.SalesOrderDate,102) > Convert(Datetime, '" & ClosingDate.ToString("yyyy-M-d 23:59:59") & "',102))") & " "
        str = str & " ORDER BY SalesOrderMasterTable.SalesOrderNo"
        FillGridEx(grdSaved, str, False)
        _dt = CType(Me.grdSaved.DataSource, DataTable)
        ' grdSaved.Columns(11).Visible = False
        'grdSaved.Columns(4).Visible = False
        '  grdSaved.Columns(7).Visible = False
        ' grdSaved.Columns(8).Visible = False
        'grdSaved.Columns("EmployeeCode").Visible = False
        'grdSaved.Columns("PoId").Visible = False
        'grdSaved.Columns("TransporterId").Visible = False
        'grdSaved.RootTable.Columns("SalesOrderId").Visible = False
        'grdSaved.Columns(1).HeaderText = "Issue No"
        'grdSaved.Columns(2).HeaderText = "Date"
        'grdSaved.Columns(3).HeaderText = "Customer"
        'grdSaved.Columns(4).HeaderText = "S-Order"
        'grdSaved.Columns(5).HeaderText = "Qty"
        'grdSaved.Columns(6).HeaderText = "Amount"
        'grdSaved.Columns(9).HeaderText = "Employee"
        'grdSaved.Columns(0).Width = 30
        'grdSaved.Columns(1).Width = 100
        'grdSaved.Columns(2).Width = 150
        'grdSaved.Columns(3).Width = 200
        'grdSaved.Columns(4).Width = 40
        'grdSaved.Columns(5).Width = 40
        'grdSaved.Columns(6).Width = 40
        grdSaved.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.False
        grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
        grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdSaved.RootTable.Columns
            col.AutoSize()
            col.Table.Columns(6).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            col.Table.Columns(7).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            col.Table.Columns(8).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            col.Table.Columns(5).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            col.Table.Columns(6).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            col.Table.Columns(7).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            col.Table.Columns(8).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            col.Table.Columns(6).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            col.Table.Columns(7).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            col.Table.Columns(8).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Next
        'Me.grdSaved.RootTable.Columns("Dev Date").ColumnType = Janus.Windows.GridEX.ColumnType.Link
        Me.grdSaved.RootTable.Columns(0).EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdSaved.RootTable.Columns("SalesOrderDate").FormatString = str_DisplayDateFormat
    End Sub

    Private Sub frmSOStatus_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8 -1-14
            If e.KeyCode = Keys.F4 Then
                SaveToolStripButton_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmPrintInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            flgCompanyRights = getConfigValueByType("CompanyRights")
        End If
        Me.FillCombo()
        Me.FillCombo("OrderType")
        Me.RefreshControls()
        Me.GetSecurityRights()
        Me.lblProgress.Visible = False
        Me.Cursor = Cursors.Default

    End Sub
    Private Sub FillCombo(Optional condition As String = "")

        If condition = String.Empty Then

            Dim strStatus() As String = System.Enum.GetNames(GetType(EnumStatus))
            Me.cmbStatus.Items.Clear()
            For Each sts As String In strStatus
                ' If sts <> Me.cmbStatus.SelectedItem.ToString Then
                Me.cmbStatus.Items.Add(sts)
                'End If
            Next
            Me.cmbStatus.SelectedIndex = 0
            Dim ShowVendorOnSales As Boolean = False
            Dim ShowMiscAccountsOnSales As Boolean = False

            If Not getConfigValueByType("Show Vendor On Sales") = "Error" Then
                ShowVendorOnSales = CBool(getConfigValueByType("Show Vendor On Sales"))
            End If
            If Not getConfigValueByType("ShowMiscAccountsOnSales") = "Error" Then
                ShowMiscAccountsOnSales = CBool(getConfigValueByType("ShowMiscAccountsOnSales"))
            End If
            Dim Str As String = "SELECT vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                    "tblListTerritory.TerritoryName as Territory " & _
                                    "FROM         tblCustomer INNER JOIN " & _
                                    "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId INNER JOIN " & _
                                    "tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " & _
                                    "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                    "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                      " WHERE dbo.vwCOADetail.detail_title Is Not NULL " & IIf(ShowVendorOnSales = True, " AND (dbo.vwCOADetail.account_type in ('Customer','Vendor'))", " AND (dbo.vwCOADetail.account_type in ('Customer'))") & "" _
                                       & "" & IIf(ShowMiscAccountsOnSales = True, " OR vwCOADetail.coa_detail_id IN (SELECT  DISTINCT tblCOAMainSubSubDetail.coa_detail_id " & _
                                      "FROM tblMiscAccountsonSales INNER JOIN   tblCOAMainSubSubDetail ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSubDetail.main_sub_sub_id where tblMiscAccountsonSales.Active = 1) ", "") & ""
            Str += " Order By tblCustomer.Sortorder, vwCOADetail.detail_title "
            FillUltraDropDown(cmbCustomer, Str)
            If Me.cmbCustomer.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If
            Me.cmbCustomer.Rows(0).Activate()
        ElseIf condition = "OrderType" Then
            FillDropDown(Me.cmbOrderType, "Select OrderTypeID, OrderType, OrderGroup From SalesOrderTypeTable ORDER BY OrderType ASC")
        End If
    End Sub
    Private Sub RefreshControls()
        Me.dtpFromDate.Value = Date.Today.AddDays(-30)
        Me.dtpToDate.Value = Date.Today
        Me.dtpFromDate.Checked = False
        Me.dtpToDate.Checked = False
        'Me.cmbCustomer.SelectedIndex = 0
        Me.cmbStatus.SelectedIndex = 0
        Me.cmbOrderType.SelectedIndex = 0
    End Sub
    Private Sub btnSearh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearh.Click
        Me.DisplayRecord()
        Me.chkAll_CheckedChanged(sender, e)
        Me.GetSecurityRights()
    End Sub
    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        If Not Me.grdSaved.RowCount > 0 Then Exit Sub
        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetRows
            r.BeginEdit()
            r.Cells(0).Value = Me.chkAll.Checked
            r.EndEdit()
        Next
    End Sub
    Private Function IsValidate() As Boolean
        'If Me.dtpFromDate.Checked AndAlso Me.dtpToDate.Checked Then
        '    If Me.dtpFromDate.Value > Me.dtpToDate.Value Then
        '        msg_Error("From Date Can't be greate than To Date")
        '        Me.dtpFromDate.Focus()
        '        Return False
        '    End If
        'End If

        Dim intIsChecked As Integer = 0

        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetRows
            If r.Cells(0).Value = True Then
                intIsChecked = 1
                Exit For
            End If
        Next

        If intIsChecked = 0 Then
            msg_Error("Please select a record from grid")
            Me.grdSaved.Focus()
            Return False
        End If
        Return True
    End Function
    Private Sub cmbStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged
        cmbSetTo.Items.Clear()
        Dim strStatus() As String = System.Enum.GetNames(GetType(EnumStatus))
        For Each sts As String In strStatus
            'If sts <> EnumStatus.Close.ToString AndAlso sts <> Me.cmbStatus.SelectedItem.ToString Then
            '    cmbSetTo.Items.Add(sts)
            'End If

            If sts <> Me.cmbStatus.SelectedItem.ToString AndAlso sts <> EnumStatus.All.ToString Then
                cmbSetTo.Items.Add(sts)
            End If
        Next
        If cmbSetTo.Items.Count > 0 Then
            cmbSetTo.SelectedIndex = 0
        End If
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                ' Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmSOStatus)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                'Me.BtnDelete.Enabled = False
                'Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As SBModel.GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        '    Me.BtnDelete.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Print" Then
                        '    Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmRptSalesOrderDetail.BackColor = Me.BackColor
        frmRptSalesOrderDetail.ShowDialog()
    End Sub
    Private Sub grdSaved_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.SelectionChanged
        Try
            If Me.SplitContainer1.Panel2Collapsed = True Then Exit Sub
            If Me.SplitContainer1.Panel2.Controls.Count > 0 Then
                frmRptSalesOrderDetail.Close()
            End If
            If Me.grdSaved.RowCount = 0 Then
                Exit Sub
            End If
            If Not (Me.grdSaved.GetRow.Cells("SalesOrderNo").Value) Is Nothing Then
                If _SOHistory = True Then
                    frmRptSalesOrderDetail.SONO = Me.grdSaved.GetRow.Cells("SalesOrderNo").Value.ToString
                Else
                    frmRptSalesOrderDetail.SONO = Me.grdSaved.GetRow.Cells("SalesOrderId").Value.ToString
                End If
            End If
            If _SOHistory = True Then
                frmRptSalesOrderDetail.ReportName = frmRptSalesOrderDetail.enmReportList.SOHistory
            Else
                frmRptSalesOrderDetail.ReportName = frmRptSalesOrderDetail.enmReportList.SOPlanHistory
            End If
            frmRptSalesOrderDetail.TopLevel = False
            frmRptSalesOrderDetail.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            frmRptSalesOrderDetail.Dock = DockStyle.Fill
            Me.SplitContainer1.Panel2.Controls.Add(frmRptSalesOrderDetail)
            ApplyStyleSheet(frmRptSalesOrderDetail)
            frmRptSalesOrderDetail.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
        Dim trans As OleDb.OleDbTransaction
        If Con.State = ConnectionState.Open Then Con.Close()
        Con.Open()
        trans = Con.BeginTransaction
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim cmd As New OleDb.OleDbCommand
            cmd.Connection = Con



            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans

            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If Not Me.IsValidate() Then Exit Sub
            If Not msg_Confirm("Do you want to update selected records?") Then Exit Sub
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                If r.Cells(0).Value = True Then
                    cmd.CommandText = "Update SalesOrderMasterTable set Status = '" & cmbSetTo.SelectedItem.ToString & "' where SalesOrderID = " & r.Cells("SalesOrderID").Value & ""
                    cmd.ExecuteNonQuery()
                End If
            Next
            trans.Commit()

            'msg_Information("Records updated successfully")
            ''insert Activity Log
            SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Sales, "abc", True)
            Me.DisplayRecord()
            Me.GetSecurityRights()
        Catch ex As Exception
            trans.Rollback()
            msg_Error(ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            RefreshControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub BtnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        frmRptSalesOrderDetail.BackColor = Me.BackColor
    '        frmRptSalesOrderDetail.ShowDialog()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub btnDetail_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try

            frmRptSalesOrderDetailOld.BackColor = Me.BackColor
            frmRptSalesOrderDetailOld.ShowDialog()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            _SOHistory = True
            If Me.SplitContainer1.Panel2Collapsed = True Then
                Me.SplitContainer1.Panel2Collapsed = False
                Me.grdSaved_SelectionChanged(Me.grdSaved, Nothing)
            Else
                Me.SplitContainer1.Panel2Collapsed = True
                If Me.SplitContainer1.Panel2.Controls.Count > 0 Then
                    frmRptSalesOrderDetail.Close()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim id As Integer = 0
            id = Me.cmbCustomer.Value
            FillCombo()
            Me.cmbCustomer.Value = id
            id = Me.cmbOrderType.SelectedIndex
            Me.FillCombo("OrderType")
            Me.cmbOrderType.SelectedIndex = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSummary.Click
        Try
            FrmSOStatusSummary.ShowDialog()
            If FrmSOStatusSummary.Acid = 0 Then
                Me.cmbCustomer.Value = 0
            Else
                Me.cmbCustomer.Value = FrmSOStatusSummary.Acid
            End If
            btnSearh_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            ShowReport("rptSOStatus", , , , , , , _dt)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try
            _SOHistory = False
            If Me.SplitContainer1.Panel2Collapsed = True Then
                Me.SplitContainer1.Panel2Collapsed = False
                Me.grdSaved_SelectionChanged(Me.grdSaved, Nothing)
            Else
                Me.SplitContainer1.Panel2Collapsed = True
                If Me.SplitContainer1.Panel2.Controls.Count > 0 Then
                    frmRptSalesOrderDetail.Close()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
