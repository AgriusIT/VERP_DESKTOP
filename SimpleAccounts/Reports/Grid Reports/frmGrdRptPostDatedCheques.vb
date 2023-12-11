'23-Aug-2014 TAsk:2803 Imran Ali Revised Post Dated Cheque Summary On Daashboard/Reports
Imports SBModel
Public Class frmGrdRptPostDatedCheques
    Dim _DateFrom As DateTime
    Dim _DateTo As DateTime
    Dim _SelectMonth As String = String.Empty
    Dim _SelectYear As String = String.Empty
    Dim dtData As New DataTable
    Dim flgIssued As Boolean = False
    Dim flgReceived As Boolean = False
    Enum enmDetail
        coa_detail_id
        coa_detail_code
        detail_title
        count
    End Enum
    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmGrdRptPostDatedCheques_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            Me.txtYear.Text = Date.Now.Year
            Dim strMonth() As String = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "November", "October", "December"}
            Me.cmbMonth.DataSource = strMonth
            Me.cmbMonth.SelectedIndex = 0
            Me.cmbMonth.Text = GetCurrentMonth(Date.Now.Month)
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
                ElseIf RightsDt.FormControlName = "Grid Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Grid Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Function GetCurrentMonth(ByVal Month As Integer) As String
        Try
            Dim strMonth As String = String.Empty
            Select Case Month
                Case 1
                    strMonth = "January"
                Case 2
                    strMonth = "February"
                Case 3
                    strMonth = "March"
                Case 4
                    strMonth = "April"
                Case 5
                    strMonth = "May"
                Case 6
                    strMonth = "June"
                Case 7
                    strMonth = "July"
                Case 8
                    strMonth = "August"
                Case 9
                    strMonth = "September"
                Case 10
                    strMonth = "October"
                Case 11
                    strMonth = "November"
                Case 12
                    strMonth = "December"
            End Select

            If strMonth.Length > 1 Then
                Return strMonth
            Else
                Return "January"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function GetBankAccounts() As DataTable
        Try

            dtData = GetDataTable("Select coa_detail_Id, detail_code as [Account Code], detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null and Account_Type='Bank' and active=1")
            For i As Integer = 1 To _DateTo.Day
                dtData.Columns.Add(i, GetType(System.Double))
            Next

            For Each row As DataRow In dtData.Rows
                For c As Integer = 3 To dtData.Columns.Count - 1 Step 1
                    row.BeginEdit()
                    row(c) = 0
                    row.EndEdit()
                Next
            Next
            dtData.AcceptChanges()
            Dim dt As New DataTable
            If rbtIssuedCheques.Checked = True Then
                dt = GetDataTable("SELECT DISTINCT DateName(Day, tblVoucherDetail.Cheque_Date) as [Day],  dbo.tblVoucherDetail.coa_detail_id, SUM(dbo.tblVoucherDetail.credit_amount) as Amount, Count(DateName(Day, tblVoucher.Voucher_Date)) as DayCont " _
                                                   & " FROM  dbo.tblVoucher INNER JOIN dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id WHERE (Convert(Varchar, tblVoucherDetail.Cheque_Date, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "',102)) AND voucher_type_id=4 Group By dbo.tblVoucherDetail.coa_detail_id, DateName(Day, tblVoucherDetail.Cheque_Date) " _
                                                   & " ORDER BY 1 asc ")
            Else
                dt = GetDataTable("SELECT DateName(Day, tblVoucherDetail.Cheque_Date) as [Day],  dbo.tblVoucherDetail.coa_detail_id, SUM(dbo.tblVoucherDetail.debit_amount) as Amount, Count(DateName(Day, tblVoucher.Voucher_Date)) as DayCont " _
                                                           & " FROM  dbo.tblVoucher INNER JOIN dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id WHERE (Convert(Varchar, tblVoucherDetail.Cheque_Date, 102) BETWEEN Convert(Datetime, '" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & _DateTo.ToString("yyyy-M-d 23:59:59") & "',102)) AND voucher_type_id=5 Group By dbo.tblVoucherDetail.coa_detail_id, DateName(Day, tblVoucherDetail.Cheque_Date) " _
                                                           & " ORDER BY 1 asc ")
            End If
            Dim dr() As DataRow
            For Each r As DataRow In dtData.Rows
                dr = dt.Select("coa_detail_id='" & r.Item(0) & "'")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            r.BeginEdit()
                            r(dtData.Columns.IndexOf(drFound(0))) = drFound(2)
                            r.EndEdit()
                        Next
                    End If
                End If
            Next
            dtData.Columns.Add("Total", GetType(System.Double))
            Dim strTotal As String = String.Empty
            For col As Integer = 3 To dtData.Columns.Count - 2 Step 1
                If strTotal.Length > 0 Then
                    strTotal = strTotal & "+" & "[" & dtData.Columns(col).ColumnName.ToString & "]"
                Else
                    strTotal = "[" & dtData.Columns(col).ColumnName.ToString & "]"
                End If
            Next
            dtData.Columns("Total").Expression = strTotal.ToString
            Return dtData
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try
            cmbMonth_SelectedIndexChanged(Nothing, Nothing)
            '_DateFrom = _DateFrom
            '_DateTo = _DateTo
            flgIssued = flgIssued
            flgReceived = flgReceived
            GetBankAccounts()
            If dtData IsNot Nothing Then
                Me.GridEX1.DataSource = dtData
                Me.GridEX1.RetrieveStructure()
                ApplyGridSettings()
                CtrlGrdBar1_Load(Nothing, Nothing)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            _SelectYear = Me.txtYear.Text
            _SelectMonth = Me.cmbMonth.Text
            Select Case Me.cmbMonth.Text
                Case "January"
                    _DateFrom = New Date(_SelectYear, 1, 1)
                    _DateTo = New Date(_SelectYear, 1, 31)
                Case "February"
                    _DateFrom = New Date(_SelectYear, 2, 1)
                    If Date.IsLeapYear(Me.txtYear.Text) = True Then
                        _DateTo = New Date(_SelectYear, 2, 29)
                    Else
                        _DateTo = New Date(_SelectYear, 2, 28)
                    End If
                Case "March"
                    _DateFrom = New Date(_SelectYear, 3, 1)
                    _DateTo = New Date(_SelectYear, 3, 31)
                Case "April"
                    _DateFrom = New Date(_SelectYear, 4, 1)
                    _DateTo = New Date(_SelectYear, 4, 30)
                Case "May"
                    _DateFrom = New Date(_SelectYear, 5, 1)
                    _DateTo = New Date(_SelectYear, 5, 31)
                Case "June"
                    _DateFrom = New Date(_SelectYear, 6, 1)
                    _DateTo = New Date(_SelectYear, 6, 30)
                Case "July"
                    _DateFrom = New Date(_SelectYear, 7, 1)
                    _DateTo = New Date(_SelectYear, 7, 31)
                Case "August"
                    _DateFrom = New Date(_SelectYear, 8, 31)
                    _DateTo = New Date(_SelectYear, 8, 31)
                Case "September"
                    _DateFrom = New Date(_SelectYear, 9, 1)
                    _DateTo = New Date(_SelectYear, 9, 30)
                Case "October"
                    _DateFrom = New Date(_SelectYear, 10, 1)
                    _DateTo = New Date(_SelectYear, 10, 31)
                Case "November"
                    _DateFrom = New Date(_SelectYear, 11, 1)
                    _DateTo = New Date(_SelectYear, 11, 30)
                Case "December"
                    _DateFrom = New Date(_SelectYear, 12, 1)
                    _DateTo = New Date(_SelectYear, 12, 31)
                Case "Year"
                    _DateFrom = New Date(_SelectYear, 12, 1)
                    _DateTo = New Date(_SelectYear, 12, 31)
            End Select
            Me.CtrlGrdBar1.txtGridTitle.Text = "" & CompanyTitle & Chr(10) & "Post Dated Cheques " & IIf(flgIssued = True, " Issued Detail", " Received Detail") & "Date From: " & _DateFrom & " Date To: " & _DateTo & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ApplyGridSettings(Optional ByVal Condition As String = "")
        Try
            Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.GridEX1.RootTable.Columns(0).Visible = False
            For col As Integer = enmDetail.count To Me.GridEX1.RootTable.Columns.Count - 2 Step 1
                Me.GridEX1.RootTable.Columns(col).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.GridEX1.RootTable.Columns(col).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(col).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(col).ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Next
            Me.GridEX1.RootTable.Columns("Total").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridEX1.RootTable.Columns("Total").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Total").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rbtIssuedCheques_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtIssuedCheques.CheckedChanged
        Try
            If Me.rbtIssuedCheques.Checked = True Then flgIssued = True Else flgIssued = False
            Me.CtrlGrdBar1.txtGridTitle.Text = "" & CompanyTitle & Chr(10) & "Post Dated Cheques " & IIf(flgIssued = True, " Issued Detail", " Received Detail") & " Date From: " & _DateFrom & " Date To: " & _DateTo & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtReceivedCheques_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtReceivedCheques.CheckedChanged
        Try
            If Me.rbtReceivedCheques.Checked = True Then flgReceived = True Else flgReceived = False
            Me.CtrlGrdBar1.txtGridTitle.Text = "" & CompanyTitle & Chr(10) & "Post Dated Cheques " & IIf(flgIssued = True, " Issued Detail", " Received Detail") & " Date From: " & _DateFrom & " Date To: " & _DateTo & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetAll(ByVal DayNo As Integer) As DataTable
        Try
            Dim chequeDate As DateTime = "" & DayNo & "/" & Me.cmbMonth.Text & "/" & Me.txtYear.Text & ""
            'Before against task:2803
            'Dim str As String = "SELECT Isnull(Opening.opning,0) as Opening,  Coa.Detail_Code, V_D.coa_detail_id, " _
            '                          & "   COA.detail_title, V.voucher_No as voucher_code, V_Type.voucher_type, V.voucher_date, V_D.comments, V_D.debit_amount, V_D.credit_amount, COA.account_type, COA.main_sub_id, coa.CityName,  isnull(V_D.CostCenterID,0) as CostCenterID, V_D.Cheque_No, V_D.Cheque_Date FROM dbo.tblVoucher V INNER JOIN " _
            '                          & "   dbo.tblVoucherDetail V_D ON V.voucher_id = V_D.voucher_id INNER JOIN " _
            '                          & "   dbo.vwcoadetail COA ON V_D.coa_detail_id = COA.coa_detail_id INNER JOIN " _
            '                          & "   dbo.tblDefVoucherType V_Type ON V.voucher_type_id = V_Type.voucher_type_id left outer join " _
            '                          & "  (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning FROM dbo.tblVoucher V INNER JOIN " _
            '                          & "   dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
            '                          & "   WHERE (CONVERT(varchar, V.voucher_date, 102) < Convert(Datetime, '" & chequeDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " _
            '                          & "   GROUP BY VD.coa_detail_id) Opening On OPening.COA_Detail_ID = V_D.COA_Detail_ID " _
            '                          & "   Where (convert(varchar, v.cheque_date,102) = Convert(Datetime, '" & chequeDate.ToString("yyyy-M-d 00:00:00") & "', 102)) AND V_D.coa_detail_id='" & Me.GridEX1.GetRow.Cells(0).Value & "' AND V.Voucher_Type_Id=" & IIf(Me.rbtIssuedCheques.Checked = True, 4, 5) & " ORDER BY V.Voucher_code asc "
            'Task:2803 Change Query
            Dim str As String = "SELECT Isnull(Opening.opning,0) as Opening,  Coa.Detail_Code, V_D.coa_detail_id, " _
                                      & "   COA.detail_title, V.voucher_No as voucher_code, V_Type.voucher_type, V.voucher_date, V_D.comments, V_D.debit_amount, V_D.credit_amount, COA.account_type, COA.main_sub_id, coa.CityName,  isnull(V_D.CostCenterID,0) as CostCenterID, V_D.Cheque_No, V_D.Cheque_Date FROM dbo.tblVoucher V INNER JOIN " _
                                      & "   dbo.tblVoucherDetail V_D ON V.voucher_id = V_D.voucher_id INNER JOIN " _
                                      & "   dbo.vwcoadetail COA ON V_D.coa_detail_id = COA.coa_detail_id INNER JOIN " _
                                      & "   dbo.tblDefVoucherType V_Type ON V.voucher_type_id = V_Type.voucher_type_id left outer join " _
                                      & "  (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning FROM dbo.tblVoucher V INNER JOIN " _
                                      & "   dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                                      & "   WHERE (CONVERT(varchar, V.voucher_date, 102) < Convert(Datetime, '" & chequeDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " _
                                      & "   GROUP BY VD.coa_detail_id) Opening On OPening.COA_Detail_ID = V_D.COA_Detail_ID " _
                                      & "   Where (convert(varchar, V_D.cheque_date,102) = Convert(Datetime, '" & chequeDate.ToString("yyyy-M-d 00:00:00") & "', 102)) AND V_D.coa_detail_id='" & Me.GridEX1.GetRow.Cells(0).Value & "' AND V.Voucher_Type_Id=" & IIf(Me.rbtIssuedCheques.Checked = True, 4, 5) & " ORDER BY V.Voucher_code asc "
            'End Task:2803
            Return GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GridEX1_LinkClicked(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.LinkClicked
        Try

            Dim flg As Boolean = False
            For c As Integer = enmDetail.count To Me.GridEX1.RootTable.Columns.Count - 2
                If flg = True Then Exit For
                If e.Column.Key = Me.GridEX1.RootTable.Columns(c).Key.ToString Then
                    If Me.GridEX1.CurrentRow.Cells(c).Value.ToString = "" Then Exit Sub
                    flg = True
                    ShowReport("PostDatedCheques", , , , False, , , GetAll(Me.GridEX1.RootTable.Columns(c).Key))
                End If
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = "" & CompanyTitle & Chr(10) & "Post Dated Cheques " & IIf(flgIssued = True, " Issued Detail", " Received Detail") & " Date From: " & _DateFrom & " Date To: " & _DateTo & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

    End Sub
End Class