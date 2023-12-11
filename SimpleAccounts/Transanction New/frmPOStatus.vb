''20-May-2014 TASK:2637 Imran Ali All account Chek on Purcase and purchase return.
Imports System.Windows.Forms

Public Class frmPOStatus
    Private _Dt As DataTable
    Enum POSStatus
        PurchaseOrderNo
        PurchaseOrderDate
        detail_title
        Remarks
        Qty
        PurchaseQty
        DeliveredQty
        Status
        PurchaseOrderId
    End Enum
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
        Dim trans As OleDb.OleDbTransaction
        If Con.State = ConnectionState.Open Then Con.Close()
        Con.Open()
        trans = Con.BeginTransaction
        Me.Cursor = Cursors.WaitCursor
        Try

            Dim cmd As New OleDb.OleDbCommand
            cmd.Connection = Con

            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans

            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If Not Me.IsValidate() Then Exit Sub
            If Not msg_Confirm("Are you sure you want to update the status of selected records") Then Exit Sub
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                If r.Cells(0).Value = True Then
                    cmd.CommandText = "Update PurchaseOrderMasterTable set Status = '" & Me.cmbSetTo.SelectedItem.ToString & "' where PurchaseOrderID = " & r.Cells("PurchaseOrderID").Value & ""
                    cmd.ExecuteNonQuery()
                End If
            Next
            trans.Commit()
            'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14 
            ' msg_Information("Records has been updated successfully")

            ''insert Activity Log
            SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Purchase, "abc", True)

            Me.DisplayRecord()
            Me.GetSecurityRights()
        Catch ex As Exception
            trans.Rollback()
            msg_Error(ex.Message)
        Finally
            Con.Close()
            Me.Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DisplayRecord()
        Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
        Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
        Dim str As String
        'str = "SELECT     Recv.SalesNo, CONVERT(varchar, Recv.SalesDate, 103) AS Date, V.CustomerName, Recv.SalesQty, Recv.SalesAmount, " _
        '       & " Recv.SalesId, Recv.CustomerCode, Recv.Remarks, convert(varchar, Recv.CashPaid) as CashPaid FROM         dbo.SalesMasterTable Recv INNER JOIN dbo.tblCustomer V ON Recv.CustomerCode = V.AccountId"

        str = "SELECT TOP 100 PERCENT dbo.PurchaseOrderMasterTable.PurchaseOrderNo, dbo.PurchaseOrderMasterTable.PurchaseOrderDate, " _
                & "       dbo.vwCOADetail.detail_title, PurchaseOrderMasterTable.Remarks, SUM(dbo.PurchaseOrderDetailTable.Qty) AS POQty, ISNULL(SUM(dbo.PurchaseOrderDetailTable.ReceivedTotalQty), 0) " _
                & "   AS PurchaseQty, SUM(dbo.PurchaseOrderDetailTable.Qty) - ISNULL(SUM(dbo.PurchaseOrderDetailTable.ReceivedTotalQty), 0) AS DifferenceQty, PurchaseOrderMasterTable.Status," _
                & " dbo.PurchaseOrderMasterTable.PurchaseOrderId" _
                & " FROM         dbo.PurchaseOrderDetailTable INNER JOIN " _
                & "        dbo.PurchaseOrderMasterTable ON dbo.PurchaseOrderDetailTable.PurchaseOrderId = dbo.PurchaseOrderMasterTable.PurchaseOrderId INNER JOIN " _
                & "     dbo.vwCOADetail ON dbo.PurchaseOrderMasterTable.VendorID = dbo.vwCOADetail.coa_detail_id  " _
                & " " & IIf(Me.cmbStatus.Text = "All", "WHERE PurchaseOrderMasterTable.Status In('Open', 'Close', 'Reject', 'DeActive')", "WHERE dbo.PurchaseOrderMasterTable.Status = '" & Me.cmbStatus.SelectedItem.ToString & "' ") & " "
        If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
            str = str & " AND dbo.PurchaseOrderMasterTable.VendorID = " & Me.cmbCustomer.ActiveRow.Cells(0).Value & ""
        End If


        str = str & " GROUP BY dbo.PurchaseOrderMasterTable.PurchaseOrderNo, dbo.PurchaseOrderMasterTable.PurchaseOrderDate, dbo.vwCOADetail.detail_title , PurchaseOrderMasterTable.Remarks, " _
                & "     dbo.PurchaseOrderMasterTable.PurchaseOrderId, dbo.PurchaseOrderMasterTable.Status " _
                & " HAVING   1 = 1    "

        If Me.dtpFromDate.Checked Then
            str = str & " AND (Convert(varchar, dbo.PurchaseOrderMasterTable.PurchaseOrderDate,102) >= Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) "
        End If

        If Me.dtpToDate.Checked Then
            str = str & " AND (Convert(varchar, dbo.PurchaseOrderMasterTable.PurchaseOrderDate, 102) <= Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) "
        End If
        str = str & "  " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, PurchaseOrderMasterTable.PurchaseOrderDate, 102) > Convert(Datetime, '" & ClosingDate.ToString("yyyy-M-d 23:59:59") & "', 102))") & " "
        str = str & " ORDER BY dbo.PurchaseOrderMasterTable.PurchaseOrderNo"
        FillGridEx(grdSaved, str, False)
        _Dt = CType(Me.grdSaved.DataSource, DataTable)
        ' grdSaved.Columns(11).Visible = False
        'grdSaved.Columns(4).Visible = False
        '  grdSaved.Columns(7).Visible = False
        ' grdSaved.Columns(8).Visible = False
        'grdSaved.Columns("EmployeeCode").Visible = False
        'grdSaved.Columns("PoId").Visible = False
        'grdSaved.Columns("TransporterId").Visible = False
        'grdSaved.RootTable.Columns(0).Visible = False
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
            col.Table.Columns(5).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            col.Table.Columns(6).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            col.Table.Columns(7).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            col.Table.Columns(5).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            col.Table.Columns(6).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            col.Table.Columns(7).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            col.Table.Columns(5).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            col.Table.Columns(6).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            col.Table.Columns(7).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Next
        Me.grdSaved.RootTable.Columns(0).EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdSaved.RootTable.Columns("PurchaseOrderDate").FormatString = str_DisplayDateFormat
    End Sub

    Private Sub frmPOStatus_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            OK_Button_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Escape Then
            btnnew_Click(Nothing, Nothing)
        End If
    End Sub


    Private Sub frmPrintInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.FillCombo()
        Me.RefreshControls()
        Me.GetSecurityRights()
        Me.lblProgress.Visible = False
    End Sub
    Private Sub FillCombo()

        Dim strStatus() As String = System.Enum.GetNames(GetType(EnumStatus))
        Me.cmbStatus.Items.Clear()
        For Each sts As String In strStatus
            'If sts <> Me.cmbStatus.SelectedItem.ToString Then
            Me.cmbStatus.Items.Add(sts)
            'End If
        Next
        Me.cmbStatus.SelectedIndex = 0
        'Before against task:2637
        'Dim Str As String = "SELECT dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
        '                        "dbo.tblListTerritory.TerritoryName as Territory " & _
        '                        "FROM         dbo.tblCustomer INNER JOIN " & _
        '                        "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
        '                        "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
        '                        "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
        '                        "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
        '                        "WHERE     (dbo.vwCOADetail.account_type = 'Vendor') order by tblCustomer.Sortorder, vwCOADetail.detail_title "
        ''20-May-2014 TASK:2637 Imran Ali All account Chek on Purcase and purchase return.
        Dim Str As String = "SELECT dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                             "dbo.tblListTerritory.TerritoryName as Territory, dbo.vwCOADetail.Account_Type as [Account Type] " & _
                             "FROM         dbo.tblCustomer INNER JOIN " & _
                             "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                             "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                             "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                             "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id "
        If getConfigValueByType("Show Customer On Purchase") = "True" Then
            Str += "WHERE (dbo.vwCOADetail.account_type IN ('Vendor','Customer')) "
        Else
            Str += "WHERE (dbo.vwCOADetail.account_type = 'Vendor') "
        End If
        Str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title "
        'End Task:2637

        FillUltraDropDown(cmbCustomer, Str)
        If Me.cmbCustomer.DisplayLayout.Bands(0).Columns.Count > 0 Then
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
        End If

        Me.cmbCustomer.Rows(0).Activate()

    End Sub

    Private Sub RefreshControls()
        Me.dtpFromDate.Value = Date.Today.AddDays(-30)
        Me.dtpToDate.Value = Date.Today
        Me.dtpFromDate.Checked = False
        Me.dtpToDate.Checked = False
        'Me.cmbCustomer.SelectedIndex = 0
        Me.cmbStatus.SelectedIndex = 0
        GetSecurityRights()
    End Sub


    Private Sub btnSearh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearh.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.DisplayRecord()
            Me.chkAll_CheckedChanged(sender, e)
            Me.GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
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
                r.BeginEdit()
                intIsChecked = 1
                r.EndEdit()
                Exit For
            End If
        Next

        If intIsChecked = 0 Then
            msg_Error("You must select at least one record from grid")
            Me.grdSaved.Focus()
            Return False
        End If
        Return True
    End Function
    Private Sub cmbStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged
        Me.cmbSetTo.Items.Clear()
        Dim strStatus() As String = System.Enum.GetNames(GetType(EnumStatus))
        For Each sts As String In strStatus
            If sts <> Me.cmbStatus.SelectedItem.ToString Then
                Me.cmbSetTo.Items.Add(sts)
            End If
        Next
        If Me.cmbSetTo.Items.Count > 0 Then
            Me.cmbSetTo.SelectedIndex = 0
        End If
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                'Me.btnDelete.Enabled = True
                'Me.btnPrint.Enabled = True
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
    Private Sub grdSaved_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.SelectionChanged
        Try
            ' Me.lblSelectedPONo.Text = "Purchase Order No:- [" & Me.grdSaved.CurrentRow.Cells("PurchaseOrderNo").Value & "]"
            If Me.SplitContainer1.Panel2Collapsed = True Then Exit Sub
            If Me.SplitContainer1.Panel2.Controls.Count > 0 Then
                frmRptPurchaseOrderDetail.Close()
            End If
            If Me.grdSaved.RowCount = 0 Then
                Exit Sub
            End If
            frmRptPurchaseOrderDetail.POId = Me.grdSaved.GetRow.Cells("PurchaseOrderNo").Value
            frmRptPurchaseOrderDetail.TopLevel = False
            frmRptPurchaseOrderDetail.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            frmRptPurchaseOrderDetail.Dock = DockStyle.Fill
            Me.SplitContainer1.Panel2.Controls.Add(frmRptPurchaseOrderDetail)
            ApplyStyleSheet(frmRptPurchaseOrderDetail)
            frmRptPurchaseOrderDetail.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.SplitContainer1.Panel2Collapsed = True Then
                Me.SplitContainer1.Panel2Collapsed = False
                Me.grdSaved_SelectionChanged(grdSaved, Nothing)
            Else
                Me.SplitContainer1.Panel2Collapsed = True
                If Me.SplitContainer1.Panel2.Controls.Count > 0 Then
                    frmRptPurchaseOrderDetail.Close()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Try
            Dim id As Integer = 0
            id = Me.cmbCustomer.Value
            FillCombo()
            Me.cmbCustomer.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Try
            RefreshControls()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try

            ShowReport("rptPOStatus", , , , , , , _Dt)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
