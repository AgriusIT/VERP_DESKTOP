''20-May-2014 TASK:2637 Imran Ali All account Chek on Purcase and purchase return.
Imports System.Windows.Forms

Public Class frmDeliveryChalanStatus
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
            'Me.GetSecurityRights()
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

        str = "SELECT dbo.DeliveryChalanMasterTable.DeliveryNo, dbo.DeliveryChalanMasterTable.DeliveryDate, dbo.SalesOrderMasterTable.SalesOrderNo AS [SO No], " _
                        & " dbo.SalesOrderMasterTable.SalesOrderDate AS [So Date], dbo.vwCOADetail.detail_code AS [Account Code], dbo.vwCOADetail.detail_title AS Customer, " _
                        & " dbo.DeliveryChalanMasterTable.Remarks, ISNULL(DeliveryChalan.IssuedQty, 0) AS [Issued Qty], ISNULL(Invoice.InvoiceQty, 0) AS [Invoice Qty], " _
                        & " ISNULL(DeliveryChalan.IssuedQty, 0) - ISNULL(Invoice.InvoiceQty, 0) AS Diff, dbo.DeliveryChalanMasterTable.[Status]," _
                        & "   dbo.DeliveryChalanMasterTable.DeliveryId " _
                        & " FROM dbo.DeliveryChalanMasterTable LEFT OUTER JOIN" _
                        & "    dbo.SalesOrderMasterTable ON dbo.DeliveryChalanMasterTable.POId = dbo.SalesOrderMasterTable.SalesOrderId LEFT OUTER JOIN " _
                        & "   dbo.vwCOADetail ON dbo.DeliveryChalanMasterTable.CustomerCode = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN" _
                        & "      (SELECT     DeliveryId, SUM(ISNULL(qty, 0)) AS IssuedQty" _
                        & " FROM dbo.DeliveryChalanDetailTable" _
                        & " GROUP BY DeliveryId) AS DeliveryChalan ON DeliveryChalan.DeliveryId = dbo.DeliveryChalanMasterTable.DeliveryId LEFT OUTER JOIN " _
                        & " (SELECT     CASE WHEN IsNull(dbo.SalesMasterTable.DeliveryChalanId,0) = 0 THEN IsNull(dbo.SalesDetailTable.DeliveryChalanID ,0) ELSE IsNull(dbo.SalesMasterTable.DeliveryChalanId,0) END  " _
                        & "  AS DeliveryChalanId, SUM(ISNULL(dbo.SalesDetailTable.qty, 0)) AS InvoiceQty" _
                        & "  FROM          dbo.SalesDetailTable INNER JOIN " _
                        & " dbo.SalesMasterTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId" _
                        & "  GROUP BY CASE WHEN IsNull(dbo.SalesMasterTable.DeliveryChalanId,0) = 0 THEN IsNull(dbo.SalesDetailTable.DeliveryChalanID ,0) ELSE IsNull(dbo.SalesMasterTable.DeliveryChalanId,0) END ) " _
                        & " AS Invoice ON Invoice.DeliveryChalanId = dbo.DeliveryChalanMasterTable.DeliveryId " _
                        & " " & IIf(Me.cmbStatus.Text = "All", " WHERE DeliveryChalanMasterTable.[Status] In('Open', 'Close')", " WHERE DeliveryChalanMasterTable.[Status] = '" & Me.cmbStatus.Text & "'") & ""

        If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
            str = str & " AND dbo.DeliveryChalanMasterTable.CustomerCode  = " & Me.cmbCustomer.ActiveRow.Cells(0).Value & ""
        End If
        If Me.dtpFromDate.Checked Then
            str = str & " AND (Convert(varchar, dbo.DeliveryChalanMasterTable.DeliveryDate ,102) >= Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) "
        End If

        If Me.dtpToDate.Checked Then
            str = str & " AND (Convert(varchar, dbo.DeliveryChalanMasterTable.DeliveryDate, 102) <= Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) "
        End If

        FillGridEx(grdSaved, str, False)
        grdSaved.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.False
        grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
        grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        grdSaved.RootTable.Columns(11).Visible = False
        Me.grdSaved.RootTable.Columns("DeliveryDate").FormatString = str_DisplayDateFormat
        Me.grdSaved.RootTable.Columns("SO Date").FormatString = str_DisplayDateFormat
        'Me.grdSaved.RootTable.Columns("Issued Qty").FormatString = "N" & DecimalPointInQty
        'Me.grdSaved.RootTable.Columns("Invoice Qty").FormatString = "N" & DecimalPointInQty
        'Me.grdSaved.RootTable.Columns("Diff").FormatString = "N" & DecimalPointInQty

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
        'Me.GetSecurityRights()
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
        If getConfigValueByType("Show Vendor On Sales") = "True" Then
            Str += "WHERE (dbo.vwCOADetail.account_type IN ('Vendor','Customer')) "
        Else
            Str += "WHERE (dbo.vwCOADetail.account_type = 'Customer') "
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
        'GetSecurityRights()
    End Sub


    Private Sub btnSearh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearh.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.DisplayRecord()
            'Me.chkAll_CheckedChanged(sender, e)
            Me.GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function IsValidate() As Boolean

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
                For Each RightsDt As SBModel.GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub grdSaved_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.SelectionChanged
        Try
            If Me.grdSaved.Row < 0 Then Exit Sub
            If Me.grdSaved.DataSource Is Nothing Then Exit Sub
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If Me.SplitContainer1.Panel2Collapsed = True Then Exit Sub
            If Me.grdSaved.RowCount = 0 Then
                Exit Sub
            End If

            Dim str As String = String.Empty
            str = "SELECT DL.DeliveryNo, DL.DeliveryDate, COA.detail_title AS Customer, COA.detail_code AS [Account Code], DL.Remarks, Loc.location_name AS Location, " _
                        & " Art.ArticleCode AS [Item Code], Art.ArticleDescription AS Item, ISNULL(DL_D.qty, 0) AS [Issued Qty], ISNULL(DL_D.DeliveredtotalQty, 0) AS [Delivered Qty],  (ISNULL(DL_D.qty, 0) - ISNULL(DL_D.DeliveredtotalQty, 0)) as Diff, " _
                        & " CASE WHEN (ISNULL(DL_D.qty, 0) - ISNULL(DL_D.DeliveredtotalQty, 0)) > 0 THEN 'Open' ELSE 'Close' END AS Status " _
                        & "  FROM dbo.DeliveryChalanDetailTable AS DL_D INNER JOIN " _
                        & " dbo.DeliveryChalanMasterTable AS DL ON DL_D.DeliveryId = DL.DeliveryId INNER JOIN " _
                        & " dbo.ArticleDefView AS Art ON DL_D.ArticleDefId = Art.ArticleId LEFT OUTER JOIN " _
                        & " dbo.tblDefLocation AS Loc ON DL_D.LocationId = Loc.location_id LEFT OUTER JOIN " _
                        & " dbo.vwCOADetail AS COA ON DL.CustomerCode = COA.coa_detail_id WHERE DL.DeliveryID=" & Val(Me.grdSaved.GetRow.Cells("DeliveryId").Value.ToString) & " Order By DL.DeliveryNo ASC"

            Dim dt As New DataTable
            dt = GetDataTable(str)
            dt.AcceptChanges()

            Me.grdDetail.DataSource = dt
            Me.grdDetail.RetrieveStructure()

            Me.grdDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed

            Me.grdDetail.RootTable.Columns("DeliveryDate").FormatString = "dd/MMM/yyyy"
            Me.grdDetail.RootTable.Columns("Issued Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdDetail.RootTable.Columns("Diff").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdDetail.RootTable.Columns("Delivered Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Me.grdDetail.RootTable.Columns("Issued Qty").FormatString = "N" & DecimalPointInQty
            'Me.grdDetail.RootTable.Columns("Delivered Qty").FormatString = "N" & DecimalPointInQty
            'Me.grdDetail.RootTable.Columns("Issued Qty").TotalFormatString = "N" & DecimalPointInQty
            'Me.grdDetail.RootTable.Columns("Delivered Qty").TotalFormatString = "N" & DecimalPointInQty
            Me.grdDetail.RootTable.Columns("Issued Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns("Delivered Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns("Issued Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns("Delivered Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns("Diff").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns("Diff").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.CtrlGrdBar2_Load(Nothing, Nothing)
            Me.grdSaved.AutoSizeColumns()

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
                'If Me.SplitContainer1.Panel2.Controls.Count > 0 Then
                '    frmRptPurchaseOrderDetail.Close()
                'End If
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
    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdDetail.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
