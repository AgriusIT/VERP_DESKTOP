' 06-Dec-2013   RId 892     Imran Ali       Login take much time
' 23-Dec-2013   ReqId=966   M Ijaz Javed    Security Rights Scripts
''30-Dec-2013     R:955       Imran Ali           Cheque name (pay to) should be Manuel 
''13-Jan-2014   Task:2375        Imran Ali        Covnerter Problems And Development
''15-Jan-2014 Task:2377             Imran Ali         Release 2.1.0.3 Bug  
''17-Mar-2014 TASK:M28 Imran Ali Payee Tile Editable in Post Dated Cheque Summary
''01-Apr-2014 TASK:2534 Imran Ali Automobile Development/Problem
''26-May-2014 TASK:2647 Imran Ali New Enhancement 
''26-Aug-2014 Task:2809 Imran Ali Add more option cheque print of MCB, NBP @Add New Collections of NBP AND MCB In Layout ComboBox
Imports SBModel
Public Class frmGrdRptPostDatedChequesSummary
    Dim dv As New DataView
    Dim BankAccountId As Integer = 0I
    Dim FromDate, ToDate As DateTime
    Private Account_Name As String = String.Empty
    Private Cheque_Date As DateTime
    Private Amount As Double = 0I


    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.dtpFrom.Value = Date.Today
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-1)
                Me.dtpTo.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
                Me.dtpTo.Value = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub frmGrdRptPostDatedChequesSummary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.P AndAlso e.Control = True Then
            btnPrint_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub frmGrdRptPostDatedChequesSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            FillDropDown(Me.cmbBank, "Select coa_detail_id, detail_title From vwcoadetail where account_type='Bank' AND Active=1")
            Me.cmbLayout.SelectedIndex = 0
            GetSecurityRights()
            Me.dtpFrom.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0
            id = Me.cmbBank.SelectedValue
            FillDropDown(Me.cmbBank, "Select coa_detail_id, detail_title From vwcoadetail where account_type='Bank' And Active=1")
            Me.cmbBank.SelectedValue = id
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function FillGrid(ByVal FromDate As DateTime, ByVal ToDate As DateTime, Optional ByVal BankAccountId As Integer = Nothing) As DataView
        Try
            Dim str As String = "SP_PostDatedChequeSummary '" & FromDate.ToString("yyyy-M-d 00:00:00") & "', '" & ToDate.ToString("yyyy-M-d 23:59:59") & "'"
            Dim dt As DataTable = GetDataTable(str)
            dt.TableName = "PostDatedCheques"
            dv.Table = dt
            If BankAccountId > 0 Then
                dv.RowFilter = "BankaccountId='" & BankAccountId & "'"
            End If
            Return dv
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try

            Dim _dv As DataView = FillGrid(Me.dtpFrom.Value, Me.dtpTo.Value, IIf(Me.cmbBank.SelectedIndex > 0, Me.cmbBank.SelectedValue, Nothing))
            If _dv IsNot Nothing Then
                'Task:2377 Distinct All Records
                Dim dt As DataTable = _dv.ToTable
                Me.grd.DataSource = dt.DefaultView.ToTable(True)
                'End Task:2377
                'Me.grd.RetrieveStructure()
                ApplyGridSettings()
                'Me.grd.AutoSizeColumns()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub frmGrdRptPostDatedChequesSummary_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    '    Try
    '        Dim _dv As DataView = FillGrid(Me.dtpFrom.Value, Me.dtpTo.Value, IIf(Me.cmbBank.SelectedIndex > 0, Me.cmbBank.SelectedValue, Nothing))
    '        If _dv IsNot Nothing Then
    '            Me.grd.DataSource = _dv.ToTable
    '            Me.grd.RetrieveStructure()
    '            ApplyGridSettings()
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Public Sub ApplyGridSettings()
        Try
            'Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            'Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            'Me.grd.RootTable.Columns("voucher_id").Visible = False
            'Me.grd.RootTable.Columns("BankaccountId").Visible = False
            'Me.grd.RootTable.Columns("Debit").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Me.grd.RootTable.Columns("Credit").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Me.grd.RootTable.Columns("Credit").FormatString = "N"
            'Me.grd.RootTable.Columns("Debit").FormatString = "N"
            'Me.grd.RootTable.Columns("Credit").TotalFormatString = "N"
            'Me.grd.RootTable.Columns("Debit").TotalFormatString = "N"
            'Me.grd.RootTable.Columns("Debit").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grd.RootTable.Columns("Credit").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grd.RootTable.Columns("Debit").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grd.RootTable.Columns("Credit").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            ''17-Mar-2014 TASK:M28 Imran Ali Payee Tile Editable in Post Dated Cheque Summary
            For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                If Me.grd.RootTable.Columns(c).DataMember <> "PayeeTitle" Then
                    Me.grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                    Me.grd.RootTable.Columns(c).FilterEditType = Janus.Windows.GridEX.EditType.TextBox 'Task:2534 #TODO Filter Record
                End If
            Next
            'end Task:M28
            Me.grd.AutoSizeColumns() 'Task:2377 Auto Size Columns
            'CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            AddRptParam("@FromDate", Me.dtpFrom.Value)
            AddRptParam("@ToDate", Me.dtpTo.Value)
            ShowReport("rptPostDatedChequeSummary", IIf(Me.cmbBank.SelectedIndex > 0, "{SP_PostDatedChequeSummary;1.BankAccountId}=" & Me.cmbBank.SelectedValue & "", ""))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Probable Sales Report" & vbCrLf & "Date From:" & Me.dtpFrom.Value.ToString("dd-MMM-yyyy") & " Month: " & Me.dtpTo.Value.ToString("dd-MMM-yyyy") & " "

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ChequePrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.grd.UpdateData()
            Me.grd.GetCheckedRows()
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetCheckedRows
                Account_Name = r.Cells("Account Description").Value.ToString
                Cheque_Date = r.Cells("Cheque Date").Value
                If r.Cells("Debit").Value.ToString = "0.00" Then
                    Amount = r.Cells("Credit").Value.ToString
                Else
                    Amount = r.Cells("Debit").Value.ToString
                End If
                AddRptParam("@Account_Name", Account_Name)
                AddRptParam("@Cheque_Date", Cheque_Date)
                AddRptParam("@Amount", Amount)
                ShowReport("rptChequePrint", , , , False)
            Next
            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetCheckedRows
            '    Account_Name = r.Cells(5).Value
            '    Cheque_Date = r.Cells(8).Value
            '    If r.Cells(10).Value.ToString = "0.00" Then
            '        Amount = r.Cells(11).Value.ToString
            '    Else
            '        Amount = r.Cells(10).Value.ToString
            '    End If
            '    AddRptParam("@Account_Name", Account_Name)
            '    AddRptParam("@Cheque_Date", Cheque_Date)
            '    AddRptParam("@Amount", Amount)
            '    ShowReport("rptChequePrint", , , , False)

            'Next

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ' RId 892   Imran Ali
    ' New event added, printing report of cheque
    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try

            If Not Me.grd.GetRow.Cells("PayeeTitle").Value.ToString <> "" Then
                Account_Name = Me.grd.GetRow.Cells("Account Description").Value.ToString
            Else
                Account_Name = Me.grd.GetRow.Cells("PayeeTitle").Value.ToString
            End If
            Cheque_Date = Me.grd.GetRow.Cells("Cheque Date").Value
            'Comment Against task:21377
            'If Me.grd.GetRow.Cells("Debit").Value.ToString = "0.00" Then
            '    Amount = Me.grd.GetRow.Cells("Amount").Value.ToString
            'Else
            '    Amount = Me.grd.GetRow.Cells("Debit").Value.ToString
            'End If
            AddRptParam("@Account_Name", Account_Name)
            'AddRptParam("@Amount", Amount) 'Comment against task:2377
            AddRptParam("@Amount", Val(Me.grd.GetRow.Cells("Amount").Value.ToString)) 'Task:2377 change parameter value
            AddRptParam("@Cheque_Date", Cheque_Date)
            ''26-May-2014 TASK:2647 Imran Ali New Enhancement 
            AddRptParam("@CrossCheque", IIf(Me.grd.GetRow.Cells("CrossCheq").Value = True, 1, 0))
            'End Task:2647
            'ShowReport("rptChequePrint", , , , False)
            'R:955 Show Report Cheque Print 
            'frmRptChequePrintReportViewer.ReportName = "rptChequePrint"

            frmRptChequePrintReportViewer.ReportName = "rptChequePrint" & Me.cmbLayout.SelectedIndex
            frmRptChequePrintReportViewer.Show()
            'End R:955
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ' 23-Dec-2013   ReqId=966   M Ijaz Javed    Security Rights Scripts
    Private Sub GetSecurityRights()
        Try
            Me.btnPrint.Enabled = False

            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnPrint.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmPurchaseOrder)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                UserPriceAllowedRights = GetUserPriceAllowedRights(LoginUserId)
            Else
                Me.btnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        '   Me.chkPost.Visible = True
                    ElseIf RightsDt.FormControlName = "Grid Print" Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


End Class