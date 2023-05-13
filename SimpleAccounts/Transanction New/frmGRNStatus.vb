''20-May-2014 TASK:2637 Imran Ali All account Chek on Purcase and purchase return.
Imports System.Windows.Forms

Public Class frmGRNStatus
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
                    cmd.CommandText = "Update ReceivingNoteMasterTable set Status = '" & Me.cmbSetTo.SelectedItem.ToString & "' where ReceivingNoteId = " & r.Cells("ReceivingNoteId").Value & ""
                    cmd.ExecuteNonQuery()
                End If
            Next
            trans.Commit()
            'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14 
            ' msg_Information("Records has been updated successfully")

            ''insert Activity Log
            SaveActivityLog("GRN", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Purchase, "GRN Status", True)

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
        'str = "SELECT     Recv.SalesNo, CONVERT(varchar, Recv.SalesDate, 103) AS Date, V.CustomerName, Recv.SalesQty, Recv.SalesAmount, " _
        '       & " Recv.SalesId, Recv.CustomerCode, Recv.Remarks, convert(varchar, Recv.CashPaid) as CashPaid FROM         dbo.SalesMasterTable Recv INNER JOIN dbo.tblCustomer V ON Recv.CustomerCode = V.AccountId"

        'str = "SELECT TOP 100 PERCENT dbo.PurchaseOrderMasterTable.PurchaseOrderNo, dbo.PurchaseOrderMasterTable.PurchaseOrderDate, " _
        '        & "       dbo.vwCOADetail.detail_title, PurchaseOrderMasterTable.Remarks, SUM(dbo.PurchaseOrderDetailTable.Qty) AS POQty, ISNULL(SUM(dbo.PurchaseOrderDetailTable.DeliveredQty), 0) " _
        '        & "   AS PurchaseQty, SUM(dbo.PurchaseOrderDetailTable.Qty) - ISNULL(SUM(dbo.PurchaseOrderDetailTable.DeliveredQty), 0) AS DifferenceQty, PurchaseOrderMasterTable.Status," _
        '        & " dbo.PurchaseOrderMasterTable.PurchaseOrderId" _
        '        & " FROM         dbo.PurchaseOrderDetailTable INNER JOIN " _
        '        & "        dbo.PurchaseOrderMasterTable ON dbo.PurchaseOrderDetailTable.PurchaseOrderId = dbo.PurchaseOrderMasterTable.PurchaseOrderId INNER JOIN " _
        '        & "     dbo.vwCOADetail ON dbo.PurchaseOrderMasterTable.VendorID = dbo.vwCOADetail.coa_detail_id  " _
        '        & " " & IIf(Me.cmbStatus.Text = "All", "WHERE PurchaseOrderMasterTable.Status In('Open', 'Close', 'Reject', 'DeActive')", "WHERE dbo.PurchaseOrderMasterTable.Status = '" & Me.cmbStatus.SelectedItem.ToString & "' ") & " "

        ''TASK TFS1557 to also check status of GRN over import.
        ''TASK TFS1753 GRN Status is now tracked by LC Detail wise in import case. on 15-11-2017

        ''Commented Against TFS2576
        'str = "SELECT  dbo.ReceivingNoteMasterTable.ReceivingNo as [GRN No], dbo.ReceivingNoteMasterTable.ReceivingDate AS [GRN Date], " _
        '              & "   dbo.PurchaseOrderMasterTable.PurchaseOrderNo AS [PO No], dbo.PurchaseOrderMasterTable.PurchaseOrderDate AS [PO Date], dbo.vwCOADetail.detail_code AS [Account Code], " _
        '              & "   dbo.vwCOADetail.detail_title AS Vendor, dbo.ReceivingNoteMasterTable.Remarks, ISNULL(ReceivingNote.ReceivedQty, 0) AS [GRN Qty],   " _
        '              & "   Case When IsNull(Import.ImportQty, 0) > 0 Then IsNull(Import.ImportQty, 0) Else  ISNULL(Invoice.InvoiceQty, 0) End AS [Invoice Qty], ISNULL(ReceivingNote.ReceivedQty, 0) - Case When IsNull(Import.ImportQty, 0) > 0 Then IsNull(Import.ImportQty, 0) Else  ISNULL(Invoice.InvoiceQty, 0) End AS Diff,  " _
        '              & "   CASE WHEN (ISNULL(ReceivingNote.ReceivedQty, 0) - ISNULL(Invoice.InvoiceQty, 0)) > 0 And IsNull(Import.ImportQty, 0) = 0 THEN 'Open' ELSE 'Close' END AS Status, " _
        '              & "   dbo.ReceivingNoteMasterTable.ReceivingNoteId " _
        '              & "   FROM dbo.ReceivingNoteMasterTable LEFT OUTER JOIN " _
        '              & "   dbo.PurchaseOrderMasterTable ON dbo.ReceivingNoteMasterTable.PurchaseOrderID = dbo.PurchaseOrderMasterTable.PurchaseOrderId LEFT OUTER JOIN " _
        '              & "   dbo.vwCOADetail ON dbo.ReceivingNoteMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
        '              & "   (SELECT ReceivingNoteId, SUM(ISNULL(Qty, 0)) AS ReceivedQty " _
        '              & "   FROM dbo.ReceivingNoteDetailTable  " _
        '              & "   GROUP BY ReceivingNoteId) AS ReceivingNote ON ReceivingNote.ReceivingNoteId = dbo.ReceivingNoteMasterTable.ReceivingNoteId LEFT OUTER JOIN " _
        '              & "   (SELECT DISTINCT IsNull(dbo.LCDetailTable.ReceivingNoteId, 0) AS ReceivingNoteId, SUM(ISNULL(dbo.LCDetailTable.Qty, 0)) AS ImportQty " _
        '              & "    FROM dbo.LCDetailTable GROUP BY IsNull(dbo.LCDetailTable.ReceivingNoteId, 0)) AS Import ON Import.ReceivingNoteId = dbo.ReceivingNoteMasterTable.ReceivingNoteId LEFT OUTER JOIN " _
        '              & "   (SELECT  Case When IsNull(dbo.ReceivingDetailTable.ReceivingNoteId, 0) > 0 Then dbo.ReceivingDetailTable.ReceivingNoteId Else dbo.ReceivingMasterTable.ReceivingNoteId END AS ReceivingNoteId, SUM(ISNULL(dbo.ReceivingDetailTable.Qty, 0)) AS InvoiceQty " _
        '              & "   FROM   dbo.ReceivingDetailTable INNER JOIN " _
        '              & "   dbo.ReceivingMasterTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId " _
        '              & "   GROUP BY Case When IsNull(dbo.ReceivingDetailTable.ReceivingNoteId, 0) > 0 Then dbo.ReceivingDetailTable.ReceivingNoteId Else dbo.ReceivingMasterTable.ReceivingNoteId END) AS Invoice ON Invoice.ReceivingNoteId = dbo.ReceivingNoteMasterTable.ReceivingNoteId " _
        '              & " " & IIf(Me.cmbStatus.Text = "All", "WHERE ReceivingNoteMasterTable.Status In('Open', 'Close', 'Reject', 'DeActive')", "WHERE dbo.ReceivingNoteMasterTable.Status = '" & Me.cmbStatus.SelectedItem.ToString & "' ") & " "
        '& " " & IIf(Me.cmbStatus.Text = "All", " WHERE (CASE WHEN (ISNULL(ReceivingNote.ReceivedQty, 0) - ISNULL(Invoice.InvoiceQty, 0)) > 0 And ReceivingNote.ReceivingNoteId Not In (Select DISTINCT IsNull(ReceivingNoteId, 0) AS ReceivingNoteId  From LCDetailTable) THEN 'Open' ELSE 'Close' END) In('Open', 'Close')", " WHERE (CASE WHEN (ISNULL(ReceivingNote.ReceivedQty, 0) - ISNULL(Invoice.InvoiceQty, 0)) > 0 And ReceivingNote.ReceivingNoteId Not In (Select DISTINCT IsNull(ReceivingNoteId, 0) AS ReceivingNoteId From LCDetailTable) THEN 'Open' ELSE 'Close' END) = '" & Me.cmbStatus.Text.ToString & "' ") & "" _

        ''TFS3099 : Added Column IGPNO In GRN Status Report
        str = "SELECT  dbo.ReceivingNoteMasterTable.ReceivingNo as [GRN No], dbo.ReceivingNoteMasterTable.ReceivingDate AS [GRN Date], " _
                     & "   dbo.PurchaseOrderMasterTable.PurchaseOrderNo AS [PO No], dbo.PurchaseOrderMasterTable.PurchaseOrderDate AS [PO Date], dbo.vwCOADetail.detail_code AS [Account Code], " _
                     & "   dbo.vwCOADetail.detail_title AS Vendor, dbo.ReceivingNoteMasterTable.Remarks,ReceivingNoteMastertable.IGPNo As [IGP No], ISNULL(ReceivingNote.ReceivedQty, 0) AS [GRN Qty],   " _
                     & "   Case When IsNull(Import.ImportQty, 0) > 0 Then IsNull(Import.ImportQty, 0) Else  ISNULL(Invoice.InvoiceQty, 0) End AS [Invoice Qty], ISNULL(ReceivingNote.ReceivedQty, 0) - Case When IsNull(Import.ImportQty, 0) > 0 Then IsNull(Import.ImportQty, 0) Else  ISNULL(Invoice.InvoiceQty, 0) End AS Diff,  " _
                     & "    ReceivingNoteMasterTable.Status AS Status, " _
                     & "   dbo.ReceivingNoteMasterTable.ReceivingNoteId " _
                     & "   FROM dbo.ReceivingNoteMasterTable LEFT OUTER JOIN " _
                     & "   dbo.PurchaseOrderMasterTable ON dbo.ReceivingNoteMasterTable.PurchaseOrderID = dbo.PurchaseOrderMasterTable.PurchaseOrderId LEFT OUTER JOIN " _
                     & "   dbo.vwCOADetail ON dbo.ReceivingNoteMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                     & "   (SELECT ReceivingNoteId, SUM(ISNULL(Qty, 0)) AS ReceivedQty " _
                     & "   FROM dbo.ReceivingNoteDetailTable  " _
                     & "   GROUP BY ReceivingNoteId) AS ReceivingNote ON ReceivingNote.ReceivingNoteId = dbo.ReceivingNoteMasterTable.ReceivingNoteId LEFT OUTER JOIN " _
                     & "   (SELECT DISTINCT IsNull(dbo.LCDetailTable.ReceivingNoteId, 0) AS ReceivingNoteId, SUM(ISNULL(dbo.LCDetailTable.Qty, 0)) AS ImportQty " _
                     & "    FROM dbo.LCDetailTable GROUP BY IsNull(dbo.LCDetailTable.ReceivingNoteId, 0)) AS Import ON Import.ReceivingNoteId = dbo.ReceivingNoteMasterTable.ReceivingNoteId LEFT OUTER JOIN " _
                     & "   (SELECT  Case When IsNull(dbo.ReceivingDetailTable.ReceivingNoteId, 0) > 0 Then dbo.ReceivingDetailTable.ReceivingNoteId Else dbo.ReceivingMasterTable.ReceivingNoteId END AS ReceivingNoteId, SUM(ISNULL(dbo.ReceivingDetailTable.Qty, 0)) AS InvoiceQty " _
                     & "   FROM   dbo.ReceivingDetailTable INNER JOIN " _
                     & "   dbo.ReceivingMasterTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId " _
                     & "   GROUP BY Case When IsNull(dbo.ReceivingDetailTable.ReceivingNoteId, 0) > 0 Then dbo.ReceivingDetailTable.ReceivingNoteId Else dbo.ReceivingMasterTable.ReceivingNoteId END) AS Invoice ON Invoice.ReceivingNoteId = dbo.ReceivingNoteMasterTable.ReceivingNoteId " _
                     & " " & IIf(Me.cmbStatus.Text = "All", "WHERE ReceivingNoteMasterTable.Status In('Open', 'Close', 'Reject', 'DeActive')", "WHERE dbo.ReceivingNoteMasterTable.Status = '" & Me.cmbStatus.SelectedItem.ToString & "' ") & " "

        If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
            str = str & " AND dbo.ReceivingNoteMasterTable.VendorID = " & Me.cmbCustomer.ActiveRow.Cells(0).Value & ""
        End If

        If Me.dtpFromDate.Checked Then
            str = str & " AND (Convert(varchar, dbo.ReceivingNoteMasterTable.ReceivingDate,102) >= Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) "
        End If

        If Me.dtpToDate.Checked Then
            str = str & " AND (Convert(varchar, dbo.ReceivingNoteMasterTable.ReceivingDate, 102) <= Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) "
        End If
        str = str & "  " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, dbo.ReceivingNoteMasterTable.ReceivingDate, 102) > Convert(Datetime, '" & ClosingDate.ToString("yyyy-M-d 23:59:59") & "', 102))") & " "
        str = str & " ORDER BY dbo.ReceivingNoteMasterTable.ReceivingNo"

        FillGridEx(grdSaved, str, False)

        grdSaved.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.False
        grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
        grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        ' Me.grdSaved.RootTable.Columns(0).EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdSaved.RootTable.Columns("GRN Date").FormatString = str_DisplayDateFormat
        Me.grdSaved.RootTable.Columns("PO Date").FormatString = str_DisplayDateFormat
        'Me.grdSaved.RootTable.Columns("GRN Qty").FormatString = "N" & DecimalPointInQty
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
            'str = "SELECT  dbo.ReceivingMasterTable.ReceivingNo AS [Receiving No], dbo.ReceivingMasterTable.ReceivingDate AS [Receiving Date], " _
            '             & " dbo.ReceivingNoteMasterTable.ReceivingNo AS [GRN No], dbo.ReceivingNoteMasterTable.ReceivingDate AS [GRN Date], " _
            '             & " dbo.tblDefCostCenter.Name AS [Cost Center], dbo.vwCOADetail.detail_code AS [Account Code], dbo.vwCOADetail.detail_title AS Vendor,   " _
            '             & " dbo.tblDefLocation.location_name AS Location, dbo.ArticleDefView.ArticleCode AS [Item Code], dbo.ArticleDefView.ArticleDescription AS Item, " _
            '             & " ISNULL(dbo.ReceivingDetailTable.Sz1, 0) AS Qty, ISNULL(dbo.ReceivingDetailTable.Sz7, 0) AS [Pack Qty], ISNULL(dbo.ReceivingDetailTable.Qty, 0) AS [Total Qty], " _
            '             & " dbo.ReceivingDetailTable.Price, ISNULL(dbo.ReceivingDetailTable.Price, 0) * ISNULL(dbo.ReceivingDetailTable.Qty, 0) AS [Total Amount],  " _
            '             & " dbo.ReceivingDetailTable.TaxPercent AS [Tax %], (ISNULL(dbo.ReceivingDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.ReceivingDetailTable.Price, 0) " _
            '             & " * ISNULL(dbo.ReceivingDetailTable.Qty, 0)) AS [Tax Amount], ISNULL(dbo.ReceivingDetailTable.AdTax_Percent, 0) AS [Ad Tax %],  " _
            '             & " (ISNULL(dbo.ReceivingDetailTable.AdTax_Percent, 0) / 100) * (ISNULL(dbo.ReceivingDetailTable.Price, 0) * ISNULL(dbo.ReceivingDetailTable.Qty, 0)) " _
            '             & " AS [Ad Tax Amount], (ISNULL(dbo.ReceivingDetailTable.Price, 0) * ISNULL(dbo.ReceivingDetailTable.Qty, 0) + (ISNULL(dbo.ReceivingDetailTable.TaxPercent, " _
            '             & " 0) / 100) * (ISNULL(dbo.ReceivingDetailTable.Price, 0) * ISNULL(dbo.ReceivingDetailTable.Qty, 0))) + (ISNULL(dbo.ReceivingDetailTable.AdTax_Percent, 0) / 100)  " _
            '             & " * (ISNULL(dbo.ReceivingDetailTable.Price, 0) * ISNULL(dbo.ReceivingDetailTable.Qty, 0)) AS [Net Amount], 0 AS SampleQty " _
            '             & " FROM   dbo.ReceivingMasterTable INNER JOIN " _
            '             & " dbo.ReceivingDetailTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId INNER JOIN " _
            '             & " dbo.ReceivingNoteMasterTable ON dbo.ReceivingMasterTable.ReceivingNoteId = dbo.ReceivingNoteMasterTable.ReceivingNoteId INNER JOIN " _
            '             & " dbo.ArticleDefView ON dbo.ReceivingDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId INNER JOIN " _
            '             & " dbo.tblDefLocation ON dbo.ReceivingDetailTable.LocationId = dbo.tblDefLocation.location_id INNER JOIN " _
            '             & " dbo.vwCOADetail ON dbo.ReceivingMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
            '             & " dbo.tblDefCostCenter ON dbo.ReceivingMasterTable.CostCenterId = dbo.tblDefCostCenter.CostCenterID WHERE dbo.ReceivingMasterTable.ReceivingNoteID=" & Val(Me.grdSaved.GetRow.Cells("ReceivingNoteID").Value.ToString) & ""


            str = "SELECT DL.ReceivingNo, DL.ReceivingDate, COA.detail_title AS Customer, COA.detail_code AS [Account Code], DL.Remarks, Loc.location_name AS Location, " _
                       & "  Art.ArticleCode AS [Item Code], Art.ArticleDescription AS Item, ISNULL(DL_D.Qty, 0) AS [Received Qty], ISNULL(Recv_D.InvoiceQty, 0) AS [Invoice Qty], (ISNULL(DL_D.Qty, 0) - ISNULL(Recv_D.InvoiceQty, 0)) as Diff, " _
                       & "  CASE WHEN (ISNULL(DL_D.Qty, 0) - ISNULL(Recv_D.InvoiceQty, 0)) > 0 THEN 'Open' ELSE 'Close' END AS Status  " _
                       & "   FROM dbo.ReceivingNoteDetailTable AS DL_D INNER JOIN  " _
                       & "  dbo.ReceivingNoteMasterTable AS DL ON DL_D.ReceivingNoteId = DL.ReceivingNoteId INNER JOIN " _
                       & "  dbo.ArticleDefView AS Art ON DL_D.ArticleDefId = Art.ArticleId LEFT OUTER JOIN  " _
                       & "  dbo.tblDefLocation AS Loc ON DL_D.LocationId = Loc.location_id LEFT OUTER JOIN  " _
                       & "  dbo.vwCOADetail AS COA ON DL.VendorId = COA.coa_detail_id LEFT OUTER JOIN (Select IsNull(ReceivingMasterTable.ReceivingNoteId,0) as ReceivingNoteId, ReceivingDetailTable.ArticleDefId, SUM(IsNull(ReceivingDetailTable.Qty,0))  as InvoiceQty From ReceivingDetailTable INNER JOIN ReceivingMasterTable on ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId Group By IsNull(ReceivingMasterTable.ReceivingNoteId,0),ReceivingDetailTable.ArticleDefId) Recv_D on Recv_D.ReceivingNoteId = DL.ReceivingNoteId AND DL_D.ArticleDefId = Recv_D.ArticleDefId WHERE DL.ReceivingNoteID=" & Val(Me.grdSaved.GetRow.Cells("ReceivingNoteID").Value.ToString) & " Order By DL.ReceivingNo ASC"

            Dim dt As New DataTable
            dt = GetDataTable(str)
            dt.AcceptChanges()


            Me.grdDetail.DataSource = dt
            Me.grdDetail.RetrieveStructure()

            Me.grdDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed


            'Me.grdDetail.RootTable.Columns("Total Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Me.grdDetail.RootTable.Columns("Total Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Me.grdDetail.RootTable.Columns("Tax Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Me.grdDetail.RootTable.Columns("Ad Tax Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Me.grdDetail.RootTable.Columns("Net Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Me.grdDetail.RootTable.Columns("SampleQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            'Me.grdDetail.RootTable.Columns("Receiving Date").FormatString = "dd/MMM/yyyy"
            'Me.grdDetail.RootTable.Columns("GRN Date").FormatString = "dd/MMM/yyyy"
            'Me.grdDetail.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            'Me.grdDetail.RootTable.Columns("Pack Qty").FormatString = "N" & DecimalPointInQty
            'Me.grdDetail.RootTable.Columns("Total Qty").FormatString = "N" & DecimalPointInQty
            'Me.grdDetail.RootTable.Columns("Price").FormatString = "N" & DecimalPointInValue
            'Me.grdDetail.RootTable.Columns("Total Amount").FormatString = "N" & DecimalPointInValue
            'Me.grdDetail.RootTable.Columns("Tax %").FormatString = "N" & DecimalPointInValue
            'Me.grdDetail.RootTable.Columns("Tax Amount").FormatString = "N" & DecimalPointInValue
            'Me.grdDetail.RootTable.Columns("Ad Tax %").FormatString = "N" & DecimalPointInValue
            'Me.grdDetail.RootTable.Columns("Ad Tax Amount").FormatString = "N" & DecimalPointInValue
            'Me.grdDetail.RootTable.Columns("Net Amount").FormatString = "N" & DecimalPointInValue
            'Me.grdDetail.RootTable.Columns("SampleQty").FormatString = "N" & DecimalPointInValue

            'Me.grdDetail.RootTable.Columns("Pack Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Total Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Total Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Tax %").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Tax Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Ad Tax %").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Ad Tax Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Net Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("SampleQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            'Me.grdDetail.RootTable.Columns("Pack Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Total Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Total Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Tax %").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Tax Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Ad Tax %").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Ad Tax Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("Net Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdDetail.RootTable.Columns("SampleQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grdDetail.RootTable.Columns("ReceivingDate").FormatString = "dd/MMM/yyyy"
            Me.grdDetail.RootTable.Columns("Received Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdDetail.RootTable.Columns("Invoice Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdDetail.RootTable.Columns("Diff").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Me.grdDetail.RootTable.Columns("Invoice Qty").FormatString = "N" & DecimalPointInQty
            'Me.grdDetail.RootTable.Columns("Received Qty").FormatString = "N" & DecimalPointInQty
            'Me.grdDetail.RootTable.Columns("Invoice Qty").TotalFormatString = "N" & DecimalPointInQty
            'Me.grdDetail.RootTable.Columns("Received Qty").TotalFormatString = "N" & DecimalPointInQty
            Me.grdDetail.RootTable.Columns("Invoice Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns("Received Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns("Invoice Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns("Received Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns("Diff").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns("Diff").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.CtrlGrdBar2_Load(Nothing, Nothing)
            Me.grdDetail.AutoSizeColumns()


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
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & " GRN Status"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
