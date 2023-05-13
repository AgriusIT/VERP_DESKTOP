'2015-06-15 Task#201506013 Ali Ansari to display quotation status against Sales Order
'06-Aug-2015 Task#06082015 Ahmad Sharif Add Two new columns Posted and Apprved in Select query

Imports System.Windows.Forms
Public Class frmGrdRptQuotationStatus
    Private _dt As DataTable
    Private flgCompanyRights As Boolean = False

    Private Sub FillCombo()
        'Dim strStatus() As String = System.Enum.GetNames(GetType(EnumStatus))
        'Me.CmbStatus.Items.Clear()
        'For Each sts As String In strStatus
        '    Me.CmbStatus.Items.Add(sts)
        'Next
        Me.CmbStatus.SelectedIndex = 0
        'Task#201506013 Fill Customer Combo
        Dim Str As String = "SELECT vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                "tblListTerritory.TerritoryName as Territory " & _
                                "FROM         tblCustomer INNER JOIN " & _
                                "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId INNER JOIN " & _
                                "tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " & _
                                "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                "WHERE     (vwCOADetail.account_type = 'Customer' and vwCOADetail.detail_title <> '')  order by tblCustomer.Sortorder, vwCOADetail.detail_title "
        '"WHERE     (vwCOADetail.account_type = 'Customer') " & IIf(flgCompanyRights = True, " AND vwcoadetail.companyid=" & MyCompanyId & "", "") & " order by tblCustomer.Sortorder, vwCOADetail.detail_title "
        FillUltraDropDown(cmbCustomer, Str)
        If Me.cmbCustomer.DisplayLayout.Bands(0).Columns.Count > 0 Then
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
        End If
        Me.cmbCustomer.Rows(0).Activate()
        'Task#201506013 Fill Customer Combo
    End Sub
    Private Sub DisplayRecord(Optional ByVal Condition As String = "")
        Try


            'Task#201506013 Display Record of Quotation Against Sales Order on behalf of balance qty
            Dim str As String
            str = String.Empty
            'str = "select QuotationId,QuotationNo, QuotationDate, Customer ,remarks,QtQty,SOQty,BalQty,Status,vendorid, a.Posted " _
            '    & "from ( select b.vendorid,b.QuotationId, b.QuotationNo,b.QuotationDate, c.detail_title as Customer, " _
            '     & "b.remarks, b.Posted ,sum(a.Qty) as QtQty,isnull(sum(f.qty),0)  as SOQty , " _
            '    & "isnull(sum(a.qty),0) - isnull(sum(f.qty),0) as BalQty, " _
            '& "CASE WHEN isnull(sum(f.qty),0) < sum(a.Qty) THEN 'Open' when isnull(sum(f.qty),0) >= sum(a.Qty) Then 'Close' END  as Status " _
            ' & "from QuotationDetailTable a  INNER JOIN  QuotationMasterTable b on a.QuotationId = b.QuotationId   " _
            '& "INNER JOIN  vwCOADetail C ON b.VendorID = C.coa_detail_id     " _
            '& "LEFT JOIN SalesOrdermastertable E on b.quotationid = e.quotationid  " _
            '& "LEFT JOIN SalesOrderdetailTable f on e.SalesOrderId = f.SalesOrderId  " _
            '& "WHERE b.QuotationId in (select QuotationId from salesordermastertable)  " _
            '& "group by  b.QuotationId, b.QuotationDate,c.detail_title,b.remarks,b.QuotationNo,b.vendorid) as a  "

            'Task#06082015 Add Two new columns Posted and Apprved in Select query
            str = "select QuotationId,QuotationNo, QuotationDate, Customer ,remarks,QtQty,SOQty,BalQty,Status,vendorid,a.Posted,a.Apprved " _
            & " from ( select b.vendorid,b.QuotationId, b.QuotationNo,b.QuotationDate, c.detail_title as Customer, b.remarks,b.Posted,b.Apprved ," _
            & " sum(a.Qty) as QtQty,isnull(sum(f.qty),0)  as SOQty , isnull(sum(a.qty),0) - isnull(sum(f.qty),0) as BalQty, " _
            & " CASE WHEN isnull(sum(f.qty),0) < sum(a.Qty) THEN 'Open' when isnull(sum(f.qty),0) >= sum(a.Qty) Then 'Close' END  as Status " _
            & " from QuotationDetailTable a  INNER JOIN  QuotationMasterTable b on a.QuotationId = b.QuotationId   INNER JOIN  vwCOADetail C ON b.VendorID = C.coa_detail_id " _
            & " LEFT JOIN SalesOrdermastertable E on b.quotationid = e.quotationid  " _
            & " LEFT JOIN SalesOrderdetailTable f on e.SalesOrderId = f.SalesOrderId  " _
            & " group by  b.QuotationId, b.QuotationDate,c.detail_title,b.remarks,b.Posted,b.Apprved,b.QuotationNo,b.vendorid) as a  "
            ''  & " WHERE b.QuotationId in (select QuotationId from salesordermastertable)  " _

            str = str & "where QuotationId <> ''"


            If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
                str = str & " AND VendorID = " & Me.cmbCustomer.ActiveRow.Cells(0).Value & ""
            End If


            If Me.dtpFromDate.Checked Then
                str = str & " AND (Convert(varchar, QuotationDate,102) >= Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) "
            End If

            If Me.dtpToDate.Checked Then
                str = str & " AND (Convert(varchar,QuotationDate,102) <= Convert(Datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) "
            End If

            str = str & IIf(CmbStatus.Text = "All", "", " AND status='" & CmbStatus.Text & "'")

            str = str & " ORDER BY QuotationNo"

            _dt = GetDataTable(str)

            _dt.AcceptChanges()

            Me.grdQuotationStatus.ClearStructure()
            Me.grdQuotationStatus.DataSource = _dt
            Me.grdQuotationStatus.RetrieveStructure()

            If grdQuotationStatus.RowCount = 0 Then Exit Sub
            ApplyGridSetting()
            'Task#201506013 Display Record of Quotation Against Sales Order on behalf of balance qty
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmGrdRptQuotationStatus_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            Me.cmbPeriod.Text = "Current Month"
            Me.FillCombo()
            Me.RefreshControls()
            Me.GetSecurityRights()
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub
    Private Sub RefreshControls()
        Try


            Me.dtpFromDate.Value = Date.Today.AddDays(-30)
            Me.dtpToDate.Value = Date.Today
            Me.dtpFromDate.Checked = False
            Me.dtpToDate.Checked = False
            Me.CmbStatus.SelectedIndex = 0
            DisplayRecord()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                'Me.btnSave.Enabled = True
                ' Me.btnDelete.Enabled = True
                'Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmSOStatus)
                If Not dt Is Nothing Then
                    '   If Not dt.Rows.Count = 0 Then
                    'Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                    'Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    ' End If
                End If
            Else


                For Each RightsDt As SBModel.GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub


    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        If Not Me.grdQuotationStatus.RowCount > 0 Then Exit Sub
        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdQuotationStatus.GetRows
            r.BeginEdit()
            r.Cells(0).Value = Me.chkAll.Checked
            r.EndEdit()
        Next
    End Sub


    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.dtpFromDate.Value = Date.Today
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.dtpFromDate.Value = Date.Today.AddDays(-1)
                Me.dtpToDate.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
                Me.dtpToDate.Value = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim id As Integer = 0

            id = Me.cmbCustomer.SelectedRow.Cells(0).Value
            FillCombo()
            RefreshControls()
            Me.cmbCustomer.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False

        End Try
    End Sub

    Private Sub btnSearh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearh.Click
        Try
            Me.DisplayRecord()
            Me.chkAll_CheckedChanged(sender, e)
            Me.GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSetting()
        Try
            grdQuotationStatus.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.False
            grdQuotationStatus.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            grdQuotationStatus.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed


            Me.grdQuotationStatus.RootTable.Columns(0).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdQuotationStatus.RootTable.Columns(1).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdQuotationStatus.RootTable.Columns(2).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdQuotationStatus.RootTable.Columns(3).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdQuotationStatus.RootTable.Columns(4).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdQuotationStatus.RootTable.Columns(5).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdQuotationStatus.RootTable.Columns(6).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdQuotationStatus.RootTable.Columns(7).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdQuotationStatus.RootTable.Columns(8).EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Task#06082015 Posted and Apprved can't be checked or unchecked
            Me.grdQuotationStatus.RootTable.Columns(10).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdQuotationStatus.RootTable.Columns(11).EditType = Janus.Windows.GridEX.EditType.NoEdit
            'End Task#06082015
            Me.grdQuotationStatus.RootTable.Columns("QuotationDate").FormatString = str_DisplayDateFormat
            Me.grdQuotationStatus.RootTable.Columns("QuotationId").Visible = False
            Me.grdQuotationStatus.RootTable.Columns("QuotationDate").Caption = "Quotation Date"
            Me.grdQuotationStatus.RootTable.Columns("QtQty").Caption = "Quotation Qty"
            Me.grdQuotationStatus.RootTable.Columns("SOQty").Caption = "Sales Order Qty"
            'Task#06082015 Change Column Apprved caption to Approved
            Me.grdQuotationStatus.RootTable.Columns("Apprved").Caption = "Approved"
            'End Task#06082015
            Me.grdQuotationStatus.RootTable.Columns("VendorId").Visible = False
            Me.grdQuotationStatus.RootTable.Columns("QtQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdQuotationStatus.RootTable.Columns("QtQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdQuotationStatus.RootTable.Columns("SOQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdQuotationStatus.RootTable.Columns("SOQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            Me.grdQuotationStatus.AutoSizeColumns()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCustomer_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbCustomer.InitializeLayout

    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdQuotationStatus.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdQuotationStatus.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdQuotationStatus.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Quotation Status" & vbCrLf & "Date From: " & Me.dtpFromDate.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpToDate.Value.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
