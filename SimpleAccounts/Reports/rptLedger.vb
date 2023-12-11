''31-Dec-2013 Tsk:2363 Imran Ali         Multi Ledger Option For Print
''09-Jul-2014 TASK:2728 IMRAN ALI Comments layout on ledger (Ravi)
''23-Sep-14 TASK:2852 Imran Ali Ledger Short Description Problem Fixed
''21-Feb-18 TASK:1961 User Wise Chart of Account Groups
Imports System.Windows.Forms
Imports SBDal
Imports SBModel
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class rptLedger
    Dim isFormLoaded As Boolean = False
    Public Tracking As Boolean = False
    Public Costid As String = ""
    Public CoaDetailId As Integer = 0
    Public ProjectId As Integer = 0
    Public dptFromDate As DateTime
    Public dptToDate As DateTime
    Dim debit_Amount As Double = 0D
    Dim credit_Amount As Double = 0D
    Dim Currency_debit_Amount As Double = 0D
    Dim Currency_credit_Amount As Double = 0D
    Dim OpeningBal As Double = 0D
    Dim CurrOpeningBal As Double = 0D
    Dim dtData As DataTable
    Public _CostCenter As Integer = 0
    Public _AccountId As Integer = 0
    Public company As Integer = 0
    Dim dview As DataView
    'Private _Post As Boolean = False
    Public _Post As Boolean = False
    Private _COAList As List(Of COABE)
    Private COAList As List(Of COABE)
    Private _Id As Integer = 0I
    Private _AddCOA As New List(Of COABE)
    Private _SearchText As String = String.Empty
    Dim BaseCurrencyId As Integer
    Dim BaseCurrencyName As String = String.Empty
    Dim CurrencySymbol As String = String.Empty
    Dim CurrencyId As Integer
    Public NotesId As Integer
    Public PropertyType As Integer
    Public _CommissionVoucher As Boolean = False

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Cursor = Cursors.WaitCursor
        Try
            'Me.DialogResult = System.Windows.Forms.DialogResult.OK
            '        Me.Close()
            'Dim Opening As String = GetOpeningBalance(Me.cmbAccount.Value, Me.DateTimePicker1.Value.Date.ToString("yyyy-M-d 23:59:59"))
            Dim fromDate As String = Me.DateTimePicker1.Value.Date '.Year & "." & Me.DateTimePicker1.Value.Month & "." & Me.DateTimePicker1.Value.Day
            Dim ToDate As String = Me.DateTimePicker2.Value.Date  '.Year & "." & Me.DateTimePicker2.Value.Month & "." & Me.DateTimePicker2.Value.Day
            AddRptParam("@FromDate", fromDate)
            AddRptParam("@ToDate", ToDate)
            'If Me.Text = "Ledger Report" Then ShowReport(IIf(Me.optDetail.Checked, "Ledger", "LedgerSummary"), IIf(Me.cmbAccount.ActiveRow.Cells(0).Value > 0, "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value & IIf(Me.cmbCostCenter.SelectedIndex > 0, " and  {SP_Rpt_Ledger.CostCenterID} = " & Me.cmbCostCenter.SelectedValue, ""), IIf(Me.cmbCostCenter.SelectedIndex > 0, " {SP_Rpt_Ledger.CostCenterID} = " & Me.cmbCostCenter.SelectedValue, "Nothing")), fromDate, ToDate) Else ShowReport("Trial", IIf(Me.cmbAccount.ActiveRow.Cells(0).Value > 0, "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value, "Nothing"))
            If Me.Text = "Ledger Report" Then ShowReport(IIf(Me.optDetail.Checked, "Ledger", "LedgerSummary"), IIf(Me.cmbAccount.ActiveRow.Cells(0).Value > 0, "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value & IIf(Me.cmbCostCenter.SelectedIndex > 0, " and  {SP_Rpt_Ledger.CostCenterID} = " & Me.cmbCostCenter.SelectedValue, ""), IIf(Me.cmbCostCenter.SelectedIndex > 0, " {SP_Rpt_Ledger.CostCenterID} = " & Me.cmbCostCenter.SelectedValue, "Nothing")), fromDate, ToDate) Else ShowReport("Trial", IIf(Me.cmbAccount.ActiveRow.Cells(0).Value > 0, "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value, "Nothing"), , , , , , , , , , , , "Ledger", Me.cmbAccount.ActiveRow.Cells("detail_title").Text.Replace("'", "''"))

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Dim Opening As String = GetOpeningBalance(Me.cmbAccount.Value, Me.DateTimePicker1.Value.Date.ToString("yyyy-M-d 23:59:59"))
            Dim fromDate As String = Me.DateTimePicker1.Value.Date '.Year & "." & Me.DateTimePicker1.Value.Month & "." & Me.DateTimePicker1.Value.Day
            Dim ToDate As String = Me.DateTimePicker2.Value.Date  '.Year & "." & Me.DateTimePicker2.Value.Month & "." & Me.DateTimePicker2.Value.Day
            AddRptParam("@FromDate", fromDate)
            AddRptParam("@ToDate", ToDate)
            If Me.Text = "Ledger Report" Then ShowReport(IIf(Me.optDetail.Checked, "Ledger", "LedgerSummary"), IIf(Me.cmbAccount.ActiveRow.Cells(0).Value > 0, "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value & IIf(Me.cmbCostCenter.SelectedIndex > 0, " and  {SP_Rpt_Ledger.CostCenterID} = " & Me.cmbCostCenter.SelectedValue, ""), IIf(Me.cmbCostCenter.SelectedIndex > 0, " {SP_Rpt_Ledger.CostCenterID} = " & Me.cmbCostCenter.SelectedValue, "Nothing")), fromDate, ToDate, True) Else ShowReport("Trial", IIf(Me.cmbAccount.ActiveRow.Cells(0).Value > 0, "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value, "Nothing"), fromDate, ToDate, True, , , , , , , , , "Ledger", Me.cmbAccount.ActiveRow.Cells("detail_title").Text.Replace("'", "''"))

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rptLedger_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                btnNew_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.F5 Then
                lnkRefresh_LinkClicked(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rptLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lbl As New Label
        Dim lbl2 As New Label
        Try
            Dim flgCompanyRights As Boolean = False
            If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                flgCompanyRights = GetConfigValue("CompanyRights")
            End If
            If lbl.Visible = False Then lbl.Visible = True
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading please wait..."
            Me.UltraTabControl1.Tabs(0).TabPage.Controls.Add(lbl)
            lbl.BringToFront()

            Me.lblOpeningBalance.Text = 0
            Me.cmbPeriod.Text = "Current Month"
            Me.cmbAccount.Enabled = True
            Me.cmbCostCenterHead.Enabled = True
            Me.cmbCostCenter.Enabled = True
            Me.DateTimePicker1.Enabled = True
            Me.DateTimePicker2.Enabled = True
            ''Tsk:2363 Set Default Value
            Me.dtpFrom.Value = Now.AddMonths(-1)
            Me.dtpTo.Value = Now
            ''End Tsk:2363
            'CompanyAddHeader()
            'CompanyHeader()
            'ShowCompanyAddress = GetConfigValue("ShowCompanyAddressOnPageHeader").ToString
            'Me.DateTimePicker1.Value = Date.Now.AddMonths(-1)
            ''Commented Against TFS1961 
            Dim Str As String = String.Empty
            If LoginGroup = "Administrator" Then
                Str = "SELECT TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City, TerritoryName as Territory, isnull(Active,0) as Active,dbo.vwCOADetail.Contact_Email as Email FROM dbo.vwCOADetail WHERE     (coa_detail_id > 0) " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & "  " & IIf(LoginGroup = "Administrator", "", " and Isnull(AccessLevel,'Everyone')='Everyone'") & " order by detail_title"
            ElseIf GetMappedUserId() <= 0 Then
                Str = "SELECT TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City, TerritoryName as Territory, isnull(Active,0) as Active,dbo.vwCOADetail.Contact_Email as Email FROM dbo.vwCOADetail WHERE     (coa_detail_id > 0) " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & "  " & IIf(LoginGroup = "Administrator", "", " and Isnull(AccessLevel,'Everyone')='Everyone'") & " order by detail_title"
            Else
                Str = "SELECT TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City, TerritoryName as Territory, isnull(Active,0) as Active,dbo.vwCOADetail.Contact_Email as Email from vwCOADetail" _
                                    & " where coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & ")" _
                                    & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]=" & LoginGroupId & ") " _
                                    & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]=" & LoginGroupId & ")" _
                                    & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]=" & LoginGroupId & ") " _
                                    & " " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId, "") & "  " & IIf(LoginGroup = "Administrator", "", " and Isnull(AccessLevel,'Everyone')='Everyone'") & "   And (coa_detail_id > 0)  order by detail_title"
            End If
            FillUltraDropDown(cmbAccount, Str)
            If Me.cmbAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("Active").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("Email").Hidden = True
            End If
            For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.cmbAccount.Rows
                If row.Index > 0 Then
                    If row.Cells("Active").Value = False Then
                        row.Appearance.BackColor = Color.Red
                    End If
                End If
            Next
            Me.cmbAccount.DisplayLayout.Bands(0).Columns("Active").Hidden = True
            Me.cmbAccount.Rows(0).Activate()
            If Me.Text = "Ledger Report" Then
                Me.pnlCost.Visible = True
                FillCombo("CostCenter")

                Str = "select distinct CostCenterGroup, CostCenterGroup from tbldefCostCenter where CostCenterGroup is Not null"
                FillDropDown(Me.cmbCostCenterHead, Str, True)
            Else
                Me.pnlCost.Visible = False
            End If

            lbl.Visible = False

            If Not rptTrialBalance.DrillDown = False Then
                lbl2.AutoSize = False
                lbl2.Dock = DockStyle.Fill
                lbl2.Text = "Loading please wait..."
                Me.UltraTabControl1.Tabs(1).TabPage.Controls.Add(lbl2)
                lbl2.BringToFront()
                Me.cmbAccount.Value = CoaDetailId
                Me.cmbAccount.Enabled = False
                Me.cmbCostCenterHead.Enabled = False
                Me.cmbCostCenter.Enabled = False
                Me.DateTimePicker1.Enabled = False
                Me.DateTimePicker2.Enabled = False
                Me.cmbPeriod.Text = "Current Month"
                Me.DateTimePicker1.Value = dptFromDate
                Me.DateTimePicker2.Value = dptToDate
                'Me.cmbAccount.Value = CoaDetailId
                dptFromDate = Me.DateTimePicker1.Value
                dptToDate = Me.DateTimePicker2.Value
                _AccountId = Me.cmbAccount.Value
                _CostCenter = Me.cmbCostCenter.SelectedValue
                BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
                BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)
                Me.lblCurrency.Text = cmbCurrency.SelectedText & BaseCurrencyName

                FillCombo("Currency") ''CurrCostCenter
                FillCombo("CurrCostCenter") ''CurrCostCenter
                GetLedger()

                lbl2.Visible = False
            End If
            'Me.cmbAccount.Value = CoaDetailId
            BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)
            Me.lblCurrency.Text = cmbCurrency.SelectedText & BaseCurrencyName

            FillCombo("Currency") ''CurrCostCenter
            FillCombo("CurrCostCenter") ''CurrCostCenter
            UltraDropDownSearching(cmbAccount, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            isFormLoaded = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
            lbl2.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' This Function is made to check if the login user is mapped with any accounts or not
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Ayesha Rehman :TFS1961 :User Wise Chart of Account Groups</remarks>
    Function GetMappedUserId() As Integer
        Dim Count As Integer = 0
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction

            str = "Select COUNT(*) from COAUserMapping where [User_Id] = " & LoginGroupId
            Count = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            trans.Commit()
            Return Count
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Private Sub optByCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optByCode.CheckedChanged
        Try
            If isFormLoaded = False Then Exit Sub
            'Dim Str As String = "SELECT OP 100 PERCENT coa_detail_id,detail_code ,detail_title , account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE (coa_detail_id > 0) order by detail_title"
            'FillUltraDropDown(cmbAccount, Str)
            Me.cmbAccount.DisplayMember = Me.cmbAccount.Rows(0).Cells(2).Column.Key.ToString
            Me.cmbAccount.Rows(0).Activate()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub optByName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optByName.CheckedChanged
        Try
            If isFormLoaded = False Then Exit Sub
            'Dim Str As String = "SELECT TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE (coa_detail_id > 0) order by detail_title"
            'FillUltraDropDown(cmbAccount, Str)
            Me.cmbAccount.DisplayMember = Me.cmbAccount.Rows(0).Cells(1).Column.Key.ToString
            Me.cmbAccount.Rows(0).Activate()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()
        Try
            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ' Add any initialization after the InitializeComponent() call.
            'ApplyStyleSheet(Me)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub cmbCostCenterHead_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCostCenterHead.SelectedIndexChanged
        Try
            If Me.pnlCost.Visible = True Then
                Dim Str As String = String.Empty
                If Me.cmbCostCenterHead.SelectedIndex > 0 Then
                    Str = "select * from tbldefCostCenter where CostCenterGroup='" & Me.cmbCostCenterHead.Text & "'"
                    If Me.chkIncludeCostCenter.Checked = True Then
                        Str += " AND Active IN(1,0,NULL)"
                    Else
                        Str += " AND Active=1"
                    End If
                    Str += " order by sortorder , name"
                    FillDropDown(Me.cmbCostCenter, Str, True)
                Else
                    Str = "select * from tbldefCostCenter "
                    If Me.chkIncludeCostCenter.Checked = True Then
                        Str += " WHERE Active IN(1,0,NULL)"
                    Else
                        Str += " WHERE Active=1"
                    End If
                    Str += " order by sortorder , name"
                    FillDropDown(Me.cmbCostCenter, Str, True)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub chkIncludeCostCenter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIncludeCostCenter.CheckedChanged
        Try
            FillCombo("CostCenter")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            Dim str As String = String.Empty
            If Condition = "CostCenter" Then
                str = "select * from tbldefCostCenter "
                If Me.chkIncludeCostCenter.Checked = True Then
                    str += " WHERE Active IN(1,0,NULL)"
                Else
                    str += " WHERE Active=1"
                End If
                str += " order by sortorder , name"
                FillDropDown(Me.cmbCostCenter, str)
            ElseIf Condition = "Currency" Then
                str = String.Empty
                str = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
                FillDropDown(Me.cmbCurrency, str, False)
                Me.cmbCurrency.SelectedValue = BaseCurrencyId
            ElseIf Condition = "CurrCostCenter" Then
                str = "select * from tbldefCostCenter order by sortorder , name "
                FillDropDown(Me.cmbCurrCostCentre, str)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.DateTimePicker1.Value = Date.Today
                Me.DateTimePicker2.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.DateTimePicker1.Value = Date.Today.AddDays(-1)
                Me.DateTimePicker2.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.DateTimePicker1.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.DateTimePicker2.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.DateTimePicker1.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.DateTimePicker2.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.DateTimePicker1.Value = New Date(Date.Now.Year, 1, 1)
                Me.DateTimePicker2.Value = Date.Today
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Sub GetLedger()
        Dim lbl1 As New Label
        Try
            CurrencyId = cmbCurrency.SelectedValue
            If lbl1.Visible = False Then lbl1.Visible = True
            lbl1.AutoSize = False
            lbl1.Dock = DockStyle.Fill
            lbl1.Text = "Loading..."
            Me.UltraTabControl1.Tabs(1).TabPage.Controls.Add(lbl1)
            lbl1.BringToFront()

            Me.dptFromDate = Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00")
            Me.dptToDate = Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59")
            _AccountId = Me.cmbAccount.Value
            _CostCenter = Me.cmbCostCenter.SelectedValue
            _Post = Me.chkUnPostedVouchers.Checked
            _CommissionVoucher = chkCommisionVoucher.Checked
            CurrencySymbol = Me.cmbCurrency.Text

            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            'Me.grdLedger.DataSource = Nothing
            If Not dview Is Nothing Then
                dview.ToTable.AcceptChanges()
                Me.cmbAccount.Value = Val(dview.ToTable.Rows(0).Item(1).ToString)
                _AccountId = Me.cmbAccount.Value
            End If
            Me.grdLedger.DataSource = dview.ToTable
            Me.lblOpeningBalance.Text = 0
            'If Me.grdLedger.RowCount = 0 Then Exit Sub

            'If Me.grdLedger.RowCount > 0 Then
            '    OpeningBal = Math.Round(Convert.ToDouble(Me.grdLedger.GetRow.Cells("Opening").Value), 2)
            '    Me.lblOpeningBalance.Text = OpeningBal
            '    Application.DoEvents()
            '    debit_Amount = 0D
            '    credit_Amount = 0D
            '    For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdLedger.GetRows
            '        debit_Amount += r.Cells("Debit_Amount").Value
            '        credit_Amount += r.Cells("Credit_Amount").Value
            '        r.BeginEdit()
            '        r.Cells("Balance").Value = (OpeningBal + (debit_Amount - credit_Amount))
            '        r.EndEdit()
            '    Next
            'End If
            Dim CurrencyRate As Double

            Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
            CurrencyRate = dr.Row.Item("CurrencyRate").ToString
            If CurrencyRate = 0 Then CurrencyRate = 1

            If Me.grdLedger.RowCount > 0 Then
                OpeningBal = Math.Round(Convert.ToDouble(Me.grdLedger.GetRow.Cells("Opening").Value), 2)
                CurrOpeningBal = Math.Round(Convert.ToDouble(Me.grdLedger.GetRow.Cells("CurrencyOpening").Value), 2)
                'If OpeningBal > 0 Then OpeningBal = OpeningBal / CurrencyRate
                If OpeningBal <> 0 Then OpeningBal = OpeningBal / CurrencyRate

                If Me.cmbCurrency.Text = "PKR" Then
                    Me.lblOpeningBalance.Text = OpeningBal.ToString("N")
                Else
                    Me.lblOpeningBalance.Text = CurrOpeningBal.ToString("N")
                End If
                'Me.lblOpeningBalance.Text = OpeningBal.ToString("N")
                Application.DoEvents()
                debit_Amount = 0D
                credit_Amount = 0D
                Currency_debit_Amount = 0D
                Currency_credit_Amount = 0D
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdLedger.GetRows
                    r.BeginEdit()
                    If r.Cells("Debit_Amount").Value <> 0 Then
                        r.Cells("Debit_Amount").Value = (r.Cells("Debit_Amount").Value / CurrencyRate)
                    End If

                    If r.Cells("Credit_Amount").Value <> 0 Then
                        r.Cells("Credit_Amount").Value = (r.Cells("Credit_Amount").Value / CurrencyRate)
                    End If

                    debit_Amount += r.Cells("Debit_Amount").Value
                    credit_Amount += r.Cells("Credit_Amount").Value

                    Currency_debit_Amount += Val(r.Cells("Currency_Debit_Amount").Value.ToString)
                    Currency_credit_Amount += Val(r.Cells("Currency_Credit_Amount").Value.ToString)

                    r.Cells("Balance").Value = (OpeningBal + (debit_Amount - credit_Amount))

                    r.Cells("CurrencyBalance").Value = (CurrOpeningBal + (Currency_debit_Amount - Currency_credit_Amount))
                    r.EndEdit()

                Next
            End If

            If rptTrialBalance.DrillDown = True Then
                Me.cmbAccount.Enabled = False
                Me.cmbCostCenterHead.Enabled = False
                Me.cmbCostCenter.Enabled = False
                Me.DateTimePicker1.Enabled = False
                Me.DateTimePicker2.Enabled = False

            Else

                Me.cmbAccount.Enabled = True
                Me.cmbCostCenterHead.Enabled = True
                Me.cmbCostCenter.Enabled = True
                Me.DateTimePicker1.Enabled = True
                Me.DateTimePicker2.Enabled = True
            End If

            'Me.lblAccount_Title.Text = Me.cmbAccount.Text
            'Me.lblAccount_Code.Text = Me.cmbAccount.ActiveRow.Cells(2).Text.ToString
            'Me.lblFromDate.Text = Me.dptFromDate.ToString("dd/MMM/yyyy")
            'Me.lblToDate.Text = Me.dptToDate.ToString("dd/MMM/yyyy")
            'Me.lblTotalDebit.Text = Math.Round(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum), 2).ToString("N")
            'Me.lblTotalCredit.Text = Math.Round(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum), 2)
            'Me.lblBalance.Text = Math.Round((OpeningBal + (Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) - Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum))), 2)


            Me.lblAccount_Title.Text = Me.cmbAccount.Text
            Me.lblAccount_Code.Text = Me.cmbAccount.SelectedRow.Cells(2).Text
            Me.lblFromDate.Text = Me.dptFromDate.ToString("dd/MMM/yyyy")
            Me.lblToDate.Text = Me.dptToDate.ToString("dd/MMM/yyyy")

            If Me.cmbCurrency.Text = "PKR" Then
                Me.lblTotalDebit.Text = Val(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)).ToString("N")
                Me.lblTotalCredit.Text = Val(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)).ToString("N")
                Me.lblBalance.Text = Val((OpeningBal + (Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) - Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)))).ToString("N")
            Else
                Me.lblTotalDebit.Text = Val(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Currency_Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)).ToString("N")
                Me.lblTotalCredit.Text = Val(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Currency_Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)).ToString("N")
                Me.lblBalance.Text = Val((CurrOpeningBal + (Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Currency_Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) - Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Currency_Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)))).ToString("N")
            End If

            'End If


            grdLedger.RootTable.Columns("Debit_Amount").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("Debit_Amount").TotalFormatString = "N" & TotalAmountRounding


            grdLedger.RootTable.Columns("Credit_Amount").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("Credit_Amount").TotalFormatString = "N" & TotalAmountRounding

            grdLedger.RootTable.Columns("Currency_Debit_Amount").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("Currency_Debit_Amount").TotalFormatString = "N" & TotalAmountRounding


            grdLedger.RootTable.Columns("Currency_Credit_Amount").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("Currency_Credit_Amount").TotalFormatString = "N" & TotalAmountRounding

            grdLedger.RootTable.Columns("CurrencyRate").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("CurrencyRate").TotalFormatString = "N" & TotalAmountRounding

            If Me.chkMultiCurrency.Checked = False Then
                If Me.cmbCurrency.Text <> "PKR" Then
                    grdLedger.RootTable.Columns("Currency_Symbol").Visible = True
                Else
                    grdLedger.RootTable.Columns("Currency_Symbol").Visible = False
                End If
                grdLedger.RootTable.Columns("CurrencyRate").Visible = False
                grdLedger.RootTable.Columns("Currency_Credit_Amount").Visible = False
                grdLedger.RootTable.Columns("Currency_Debit_Amount").Visible = False
                grdLedger.RootTable.Columns("CurrencyBalance").Visible = False
                If Me.cmbCurrency.Text = "PKR" Then
                    grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit (" & Me.cmbCurrency.Text & ")"
                    grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit (" & Me.cmbCurrency.Text & ")"
                    grdLedger.RootTable.Columns("Balance").Caption = "Balance (" & Me.cmbCurrency.Text & ")"
                    grdLedger.RootTable.Columns("Debit_Amount").Visible = True
                    grdLedger.RootTable.Columns("Credit_Amount").Visible = True
                    grdLedger.RootTable.Columns("Balance").Visible = True
                Else
                    grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit"
                    grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit"
                    grdLedger.RootTable.Columns("CurrencyBalance").Caption = "Balance"
                    grdLedger.RootTable.Columns("CurrencyBalance").Visible = True
                    grdLedger.RootTable.Columns("Balance").Visible = False
                    grdLedger.RootTable.Columns("Currency_Credit_Amount").Visible = True
                    grdLedger.RootTable.Columns("Currency_Debit_Amount").Visible = True
                    grdLedger.RootTable.Columns("CurrencyBalance").Visible = True
                    grdLedger.RootTable.Columns("Debit_Amount").Visible = False
                    grdLedger.RootTable.Columns("Credit_Amount").Visible = False
                End If
            Else

                grdLedger.RootTable.Columns("Currency_Symbol").Visible = True
                grdLedger.RootTable.Columns("CurrencyRate").Visible = True
                grdLedger.RootTable.Columns("Currency_Credit_Amount").Visible = True
                grdLedger.RootTable.Columns("Currency_Debit_Amount").Visible = True
                grdLedger.RootTable.Columns("CurrencyBalance").Visible = True
                grdLedger.RootTable.Columns("Debit_Amount").Visible = True
                grdLedger.RootTable.Columns("Credit_Amount").Visible = True
                grdLedger.RootTable.Columns("Balance").Visible = True
                'If Me.cmbCurrency.Text = "PKR" Then
                '    grdLedger.RootTable.Columns("Currency_Debit_Amount").Caption = "Debit"
                '    grdLedger.RootTable.Columns("Currency_Credit_Amount").Caption = "Credit"
                'Else
                '    grdLedger.RootTable.Columns("Currency_Debit_Amount").Caption = "Debit (" & Me.cmbCurrency.Text & ")"
                '    grdLedger.RootTable.Columns("Currency_Credit_Amount").Caption = "Credit (" & Me.cmbCurrency.Text & ")"
                'End If
                grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit (" & Me.BaseCurrencyName & ")"
                grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit (" & Me.BaseCurrencyName & ")"
                grdLedger.RootTable.Columns("Balance").Caption = "Balance (" & Me.BaseCurrencyName & ")"

            End If

            'grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit (" & Me.BaseCurrencyName & ")"
            'grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit (" & Me.BaseCurrencyName & ")"
            'grdLedger.RootTable.Columns("Balance").Caption = "Balance (" & Me.BaseCurrencyName & ")"

            ''18-04-2017
            'grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit (" & Me.cmbCurrency.Text & ")"
            'grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit (" & Me.cmbCurrency.Text & ")"
            'grdLedger.RootTable.Columns("Balance").Caption = "Balance (" & Me.cmbCurrency.Text & ")"

            Me.lblCurrency.Text = "Currency: " & cmbCurrency.Text

            Me.grdLedger.AutoSizeColumns()
            grdLedger.RootTable.Columns("Comments").Width = 275
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        Finally
            lbl1.Visible = False
            _AccountId = 0
            _CostCenter = 0
        End Try
    End Sub

    Public Sub GetLedgerPayables()
        Dim lbl1 As New Label
        Try
            If lbl1.Visible = False Then lbl1.Visible = True
            lbl1.AutoSize = False
            lbl1.Dock = DockStyle.Fill
            lbl1.Text = "Loading..."
            Me.UltraTabControl1.Tabs(1).TabPage.Controls.Add(lbl1)
            lbl1.BringToFront()

            Me.dptFromDate = Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00")
            Me.dptToDate = Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59")
            _AccountId = Me.cmbAccount.Value
            _CostCenter = Me.cmbCostCenter.SelectedValue
            _Post = Me.chkUnPostedVouchers.Checked
            _CommissionVoucher = Me.chkCommisionVoucher.Checked
            CurrencySymbol = Me.cmbCurrency.Text
            If BackgroundWorker3.IsBusy Then Exit Sub
            BackgroundWorker3.RunWorkerAsync()
            Do While BackgroundWorker3.IsBusy
                Application.DoEvents()
            Loop
            'Me.grdLedger.DataSource = Nothing
            If Not dview Is Nothing Then
                dview.ToTable.AcceptChanges()
            End If
            Me.grdLedger.DataSource = dview.ToTable
            Me.lblOpeningBalance.Text = 0
            'If Me.grdLedger.RowCount = 0 Then Exit Sub

            'If Me.grdLedger.RowCount > 0 Then
            '    OpeningBal = Math.Round(Convert.ToDouble(Me.grdLedger.GetRow.Cells("Opening").Value), 2)
            '    Me.lblOpeningBalance.Text = OpeningBal
            '    Application.DoEvents()
            '    debit_Amount = 0D
            '    credit_Amount = 0D
            '    For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdLedger.GetRows
            '        debit_Amount += r.Cells("Debit_Amount").Value
            '        credit_Amount += r.Cells("Credit_Amount").Value
            '        r.BeginEdit()
            '        r.Cells("Balance").Value = (OpeningBal + (debit_Amount - credit_Amount))
            '        r.EndEdit()
            '    Next
            'End If
            Dim CurrencyRate As Double

            Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
            CurrencyRate = dr.Row.Item("CurrencyRate").ToString
            If CurrencyRate = 0 Then CurrencyRate = 1

            If Me.grdLedger.RowCount > 0 Then
                OpeningBal = Math.Round(Convert.ToDouble(Me.grdLedger.GetRow.Cells("Opening").Value), 2)
                CurrOpeningBal = Math.Round(Convert.ToDouble(Me.grdLedger.GetRow.Cells("CurrencyOpening").Value), 2)
                'If OpeningBal > 0 Then OpeningBal = OpeningBal / CurrencyRate
                If OpeningBal <> 0 Then OpeningBal = OpeningBal / CurrencyRate

                If Me.cmbCurrency.Text = "PKR" Then
                    Me.lblOpeningBalance.Text = OpeningBal.ToString("N")
                Else
                    Me.lblOpeningBalance.Text = CurrOpeningBal.ToString("N")
                End If
                'Me.lblOpeningBalance.Text = OpeningBal.ToString("N")
                Application.DoEvents()
                debit_Amount = 0D
                credit_Amount = 0D
                Currency_debit_Amount = 0D
                Currency_credit_Amount = 0D
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdLedger.GetRows
                    r.BeginEdit()
                    If r.Cells("Debit_Amount").Value <> 0 Then
                        r.Cells("Debit_Amount").Value = (r.Cells("Debit_Amount").Value / CurrencyRate)
                    End If

                    If r.Cells("Credit_Amount").Value <> 0 Then
                        r.Cells("Credit_Amount").Value = (r.Cells("Credit_Amount").Value / CurrencyRate)
                    End If

                    debit_Amount += r.Cells("Debit_Amount").Value
                    credit_Amount += r.Cells("Credit_Amount").Value

                    Currency_debit_Amount += Val(r.Cells("Currency_Debit_Amount").Value.ToString)
                    Currency_credit_Amount += Val(r.Cells("Currency_Credit_Amount").Value.ToString)

                    r.Cells("Balance").Value = (OpeningBal + (debit_Amount - credit_Amount))

                    r.Cells("CurrencyBalance").Value = (CurrOpeningBal + (Currency_debit_Amount - Currency_credit_Amount))
                    r.EndEdit()

                Next
            End If

            If rptTrialBalance.DrillDown = True Then
                Me.cmbAccount.Enabled = False
                Me.cmbCostCenterHead.Enabled = False
                Me.cmbCostCenter.Enabled = False
                Me.DateTimePicker1.Enabled = False
                Me.DateTimePicker2.Enabled = False

            Else

                Me.cmbAccount.Enabled = True
                Me.cmbCostCenterHead.Enabled = True
                Me.cmbCostCenter.Enabled = True
                Me.DateTimePicker1.Enabled = True
                Me.DateTimePicker2.Enabled = True
            End If

            'Me.lblAccount_Title.Text = Me.cmbAccount.Text
            'Me.lblAccount_Code.Text = Me.cmbAccount.ActiveRow.Cells(2).Text.ToString
            'Me.lblFromDate.Text = Me.dptFromDate.ToString("dd/MMM/yyyy")
            'Me.lblToDate.Text = Me.dptToDate.ToString("dd/MMM/yyyy")
            'Me.lblTotalDebit.Text = Math.Round(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum), 2).ToString("N")
            'Me.lblTotalCredit.Text = Math.Round(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum), 2)
            'Me.lblBalance.Text = Math.Round((OpeningBal + (Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) - Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum))), 2)


            Me.lblAccount_Title.Text = Me.cmbAccount.Text
            Me.lblAccount_Code.Text = Me.cmbAccount.SelectedRow.Cells(2).Text
            Me.lblFromDate.Text = Me.dptFromDate.ToString("dd/MMM/yyyy")
            Me.lblToDate.Text = Me.dptToDate.ToString("dd/MMM/yyyy")

            If Me.cmbCurrency.Text = "PKR" Then
                Me.lblTotalDebit.Text = Val(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)).ToString("N")
                Me.lblTotalCredit.Text = Val(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)).ToString("N")
                Me.lblBalance.Text = Val((OpeningBal + (Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) - Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)))).ToString("N")
            Else
                Me.lblTotalDebit.Text = Val(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Currency_Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)).ToString("N")
                Me.lblTotalCredit.Text = Val(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Currency_Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)).ToString("N")
                Me.lblBalance.Text = Val((CurrOpeningBal + (Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Currency_Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) - Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Currency_Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)))).ToString("N")
            End If

            'End If


            grdLedger.RootTable.Columns("Debit_Amount").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("Debit_Amount").TotalFormatString = "N" & TotalAmountRounding


            grdLedger.RootTable.Columns("Credit_Amount").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("Credit_Amount").TotalFormatString = "N" & TotalAmountRounding

            grdLedger.RootTable.Columns("Currency_Debit_Amount").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("Currency_Debit_Amount").TotalFormatString = "N" & TotalAmountRounding


            grdLedger.RootTable.Columns("Currency_Credit_Amount").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("Currency_Credit_Amount").TotalFormatString = "N" & TotalAmountRounding

            grdLedger.RootTable.Columns("CurrencyRate").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("CurrencyRate").TotalFormatString = "N" & TotalAmountRounding

            If Me.chkMultiCurrency.Checked = False Then
                If Me.cmbCurrency.Text <> "PKR" Then
                    grdLedger.RootTable.Columns("Currency_Symbol").Visible = True
                Else
                    grdLedger.RootTable.Columns("Currency_Symbol").Visible = False
                End If
                grdLedger.RootTable.Columns("CurrencyRate").Visible = False
                grdLedger.RootTable.Columns("Currency_Credit_Amount").Visible = False
                grdLedger.RootTable.Columns("Currency_Debit_Amount").Visible = False
                grdLedger.RootTable.Columns("CurrencyBalance").Visible = False
                If Me.cmbCurrency.Text = "PKR" Then
                    grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit (" & Me.cmbCurrency.Text & ")"
                    grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit (" & Me.cmbCurrency.Text & ")"
                    grdLedger.RootTable.Columns("Balance").Caption = "Balance (" & Me.cmbCurrency.Text & ")"
                    grdLedger.RootTable.Columns("Debit_Amount").Visible = True
                    grdLedger.RootTable.Columns("Credit_Amount").Visible = True
                    grdLedger.RootTable.Columns("Balance").Visible = True
                Else
                    grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit"
                    grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit"
                    grdLedger.RootTable.Columns("CurrencyBalance").Caption = "Balance"
                    grdLedger.RootTable.Columns("CurrencyBalance").Visible = True
                    grdLedger.RootTable.Columns("Balance").Visible = False
                    grdLedger.RootTable.Columns("Currency_Credit_Amount").Visible = True
                    grdLedger.RootTable.Columns("Currency_Debit_Amount").Visible = True
                    grdLedger.RootTable.Columns("CurrencyBalance").Visible = True
                    grdLedger.RootTable.Columns("Debit_Amount").Visible = False
                    grdLedger.RootTable.Columns("Credit_Amount").Visible = False
                End If
            Else

                grdLedger.RootTable.Columns("Currency_Symbol").Visible = True
                grdLedger.RootTable.Columns("CurrencyRate").Visible = True
                grdLedger.RootTable.Columns("Currency_Credit_Amount").Visible = True
                grdLedger.RootTable.Columns("Currency_Debit_Amount").Visible = True
                grdLedger.RootTable.Columns("CurrencyBalance").Visible = True
                grdLedger.RootTable.Columns("Debit_Amount").Visible = True
                grdLedger.RootTable.Columns("Credit_Amount").Visible = True
                grdLedger.RootTable.Columns("Balance").Visible = True
                'If Me.cmbCurrency.Text = "PKR" Then
                '    grdLedger.RootTable.Columns("Currency_Debit_Amount").Caption = "Debit"
                '    grdLedger.RootTable.Columns("Currency_Credit_Amount").Caption = "Credit"
                'Else
                '    grdLedger.RootTable.Columns("Currency_Debit_Amount").Caption = "Debit (" & Me.cmbCurrency.Text & ")"
                '    grdLedger.RootTable.Columns("Currency_Credit_Amount").Caption = "Credit (" & Me.cmbCurrency.Text & ")"
                'End If
                grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit (" & Me.BaseCurrencyName & ")"
                grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit (" & Me.BaseCurrencyName & ")"
                grdLedger.RootTable.Columns("Balance").Caption = "Balance (" & Me.BaseCurrencyName & ")"

            End If

            'grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit (" & Me.BaseCurrencyName & ")"
            'grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit (" & Me.BaseCurrencyName & ")"
            'grdLedger.RootTable.Columns("Balance").Caption = "Balance (" & Me.BaseCurrencyName & ")"

            ''18-04-2017
            'grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit (" & Me.cmbCurrency.Text & ")"
            'grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit (" & Me.cmbCurrency.Text & ")"
            'grdLedger.RootTable.Columns("Balance").Caption = "Balance (" & Me.cmbCurrency.Text & ")"

            Me.lblCurrency.Text = "Currency: " & cmbCurrency.Text

            Me.grdLedger.AutoSizeColumns()
            grdLedger.RootTable.Columns("Comments").Width = 275
        Catch ex As Exception
            Throw ex
        Finally
            lbl1.Visible = False
            _AccountId = 0
            _CostCenter = 0
        End Try
    End Sub



    Public Sub GetLedgerReceiveables()
        Dim lbl1 As New Label
        Try
            If lbl1.Visible = False Then lbl1.Visible = True
            lbl1.AutoSize = False
            lbl1.Dock = DockStyle.Fill
            lbl1.Text = "Loading..."
            Me.UltraTabControl1.Tabs(1).TabPage.Controls.Add(lbl1)
            lbl1.BringToFront()

            Me.dptFromDate = Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00")
            Me.dptToDate = Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59")
            _AccountId = Me.cmbAccount.Value
            '_CostCenter = Costid
            _Post = Me.chkUnPostedVouchers.Checked
            _CommissionVoucher = Me.chkCommisionVoucher.Checked
            CurrencySymbol = Me.cmbCurrency.Text
            If BackgroundWorker4.IsBusy Then Exit Sub
            BackgroundWorker4.RunWorkerAsync()
            Do While BackgroundWorker4.IsBusy
                Application.DoEvents()
            Loop
            'Me.grdLedger.DataSource = Nothing
            If Not dview Is Nothing Then
                dview.ToTable.AcceptChanges()
            End If
            Me.grdLedger.DataSource = dview.ToTable
            Me.lblOpeningBalance.Text = 0
            'If Me.grdLedger.RowCount = 0 Then Exit Sub

            'If Me.grdLedger.RowCount > 0 Then
            '    OpeningBal = Math.Round(Convert.ToDouble(Me.grdLedger.GetRow.Cells("Opening").Value), 2)
            '    Me.lblOpeningBalance.Text = OpeningBal
            '    Application.DoEvents()
            '    debit_Amount = 0D
            '    credit_Amount = 0D
            '    For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdLedger.GetRows
            '        debit_Amount += r.Cells("Debit_Amount").Value
            '        credit_Amount += r.Cells("Credit_Amount").Value
            '        r.BeginEdit()
            '        r.Cells("Balance").Value = (OpeningBal + (debit_Amount - credit_Amount))
            '        r.EndEdit()
            '    Next
            'End If
            Dim CurrencyRate As Double

            Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
            CurrencyRate = dr.Row.Item("CurrencyRate").ToString
            If CurrencyRate = 0 Then CurrencyRate = 1

            If Me.grdLedger.RowCount > 0 Then
                OpeningBal = Math.Round(Convert.ToDouble(Me.grdLedger.GetRow.Cells("Opening").Value), 2)
                CurrOpeningBal = Math.Round(Convert.ToDouble(Me.grdLedger.GetRow.Cells("CurrencyOpening").Value), 2)
                'If OpeningBal > 0 Then OpeningBal = OpeningBal / CurrencyRate
                If OpeningBal <> 0 Then OpeningBal = OpeningBal / CurrencyRate

                If Me.cmbCurrency.Text = "PKR" Then
                    Me.lblOpeningBalance.Text = OpeningBal.ToString("N")
                Else
                    Me.lblOpeningBalance.Text = CurrOpeningBal.ToString("N")
                End If
                'Me.lblOpeningBalance.Text = OpeningBal.ToString("N")
                Application.DoEvents()
                debit_Amount = 0D
                credit_Amount = 0D
                Currency_debit_Amount = 0D
                Currency_credit_Amount = 0D
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdLedger.GetRows
                    r.BeginEdit()
                    If r.Cells("Debit_Amount").Value <> 0 Then
                        r.Cells("Debit_Amount").Value = (r.Cells("Debit_Amount").Value / CurrencyRate)
                    End If

                    If r.Cells("Credit_Amount").Value <> 0 Then
                        r.Cells("Credit_Amount").Value = (r.Cells("Credit_Amount").Value / CurrencyRate)
                    End If

                    debit_Amount += r.Cells("Debit_Amount").Value
                    credit_Amount += r.Cells("Credit_Amount").Value

                    Currency_debit_Amount += Val(r.Cells("Currency_Debit_Amount").Value.ToString)
                    Currency_credit_Amount += Val(r.Cells("Currency_Credit_Amount").Value.ToString)

                    r.Cells("Balance").Value = (OpeningBal + (debit_Amount - credit_Amount))

                    r.Cells("CurrencyBalance").Value = (CurrOpeningBal + (Currency_debit_Amount - Currency_credit_Amount))
                    r.EndEdit()

                Next
            End If

            If rptTrialBalance.DrillDown = True Then
                Me.cmbAccount.Enabled = False
                Me.cmbCostCenterHead.Enabled = False
                Me.cmbCostCenter.Enabled = False
                Me.DateTimePicker1.Enabled = False
                Me.DateTimePicker2.Enabled = False

            Else

                Me.cmbAccount.Enabled = True
                Me.cmbCostCenterHead.Enabled = True
                Me.cmbCostCenter.Enabled = True
                Me.DateTimePicker1.Enabled = True
                Me.DateTimePicker2.Enabled = True
            End If

            'Me.lblAccount_Title.Text = Me.cmbAccount.Text
            'Me.lblAccount_Code.Text = Me.cmbAccount.ActiveRow.Cells(2).Text.ToString
            'Me.lblFromDate.Text = Me.dptFromDate.ToString("dd/MMM/yyyy")
            'Me.lblToDate.Text = Me.dptToDate.ToString("dd/MMM/yyyy")
            'Me.lblTotalDebit.Text = Math.Round(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum), 2).ToString("N")
            'Me.lblTotalCredit.Text = Math.Round(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum), 2)
            'Me.lblBalance.Text = Math.Round((OpeningBal + (Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) - Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum))), 2)


            Me.lblAccount_Title.Text = Me.cmbAccount.Text
            Me.lblAccount_Code.Text = Me.cmbAccount.SelectedRow.Cells(2).Text
            Me.lblFromDate.Text = Me.dptFromDate.ToString("dd/MMM/yyyy")
            Me.lblToDate.Text = Me.dptToDate.ToString("dd/MMM/yyyy")

            If Me.cmbCurrency.Text = "PKR" Then
                Me.lblTotalDebit.Text = Val(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)).ToString("N")
                Me.lblTotalCredit.Text = Val(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)).ToString("N")
                Me.lblBalance.Text = Val((OpeningBal + (Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) - Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)))).ToString("N")
            Else
                Me.lblTotalDebit.Text = Val(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Currency_Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)).ToString("N")
                Me.lblTotalCredit.Text = Val(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Currency_Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)).ToString("N")
                Me.lblBalance.Text = Val((CurrOpeningBal + (Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Currency_Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) - Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Currency_Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)))).ToString("N")
            End If

            'End If


            grdLedger.RootTable.Columns("Debit_Amount").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("Debit_Amount").TotalFormatString = "N" & TotalAmountRounding


            grdLedger.RootTable.Columns("Credit_Amount").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("Credit_Amount").TotalFormatString = "N" & TotalAmountRounding

            grdLedger.RootTable.Columns("Currency_Debit_Amount").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("Currency_Debit_Amount").TotalFormatString = "N" & TotalAmountRounding


            grdLedger.RootTable.Columns("Currency_Credit_Amount").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("Currency_Credit_Amount").TotalFormatString = "N" & TotalAmountRounding

            grdLedger.RootTable.Columns("CurrencyRate").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("CurrencyRate").TotalFormatString = "N" & TotalAmountRounding

            If Me.chkMultiCurrency.Checked = False Then
                If Me.cmbCurrency.Text <> "PKR" Then
                    grdLedger.RootTable.Columns("Currency_Symbol").Visible = True
                Else
                    grdLedger.RootTable.Columns("Currency_Symbol").Visible = False
                End If
                grdLedger.RootTable.Columns("CurrencyRate").Visible = False
                grdLedger.RootTable.Columns("Currency_Credit_Amount").Visible = False
                grdLedger.RootTable.Columns("Currency_Debit_Amount").Visible = False
                grdLedger.RootTable.Columns("CurrencyBalance").Visible = False
                If Me.cmbCurrency.Text = "PKR" Then
                    grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit (" & Me.cmbCurrency.Text & ")"
                    grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit (" & Me.cmbCurrency.Text & ")"
                    grdLedger.RootTable.Columns("Balance").Caption = "Balance (" & Me.cmbCurrency.Text & ")"
                    grdLedger.RootTable.Columns("Debit_Amount").Visible = True
                    grdLedger.RootTable.Columns("Credit_Amount").Visible = True
                    grdLedger.RootTable.Columns("Balance").Visible = True
                Else
                    grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit"
                    grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit"
                    grdLedger.RootTable.Columns("CurrencyBalance").Caption = "Balance"
                    grdLedger.RootTable.Columns("CurrencyBalance").Visible = True
                    grdLedger.RootTable.Columns("Balance").Visible = False
                    grdLedger.RootTable.Columns("Currency_Credit_Amount").Visible = True
                    grdLedger.RootTable.Columns("Currency_Debit_Amount").Visible = True
                    grdLedger.RootTable.Columns("CurrencyBalance").Visible = True
                    grdLedger.RootTable.Columns("Debit_Amount").Visible = False
                    grdLedger.RootTable.Columns("Credit_Amount").Visible = False
                End If
            Else

                grdLedger.RootTable.Columns("Currency_Symbol").Visible = True
                grdLedger.RootTable.Columns("CurrencyRate").Visible = True
                grdLedger.RootTable.Columns("Currency_Credit_Amount").Visible = True
                grdLedger.RootTable.Columns("Currency_Debit_Amount").Visible = True
                grdLedger.RootTable.Columns("CurrencyBalance").Visible = True
                grdLedger.RootTable.Columns("Debit_Amount").Visible = True
                grdLedger.RootTable.Columns("Credit_Amount").Visible = True
                grdLedger.RootTable.Columns("Balance").Visible = True
                'If Me.cmbCurrency.Text = "PKR" Then
                '    grdLedger.RootTable.Columns("Currency_Debit_Amount").Caption = "Debit"
                '    grdLedger.RootTable.Columns("Currency_Credit_Amount").Caption = "Credit"
                'Else
                '    grdLedger.RootTable.Columns("Currency_Debit_Amount").Caption = "Debit (" & Me.cmbCurrency.Text & ")"
                '    grdLedger.RootTable.Columns("Currency_Credit_Amount").Caption = "Credit (" & Me.cmbCurrency.Text & ")"
                'End If
                grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit (" & Me.BaseCurrencyName & ")"
                grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit (" & Me.BaseCurrencyName & ")"
                grdLedger.RootTable.Columns("Balance").Caption = "Balance (" & Me.BaseCurrencyName & ")"

            End If

            'grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit (" & Me.BaseCurrencyName & ")"
            'grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit (" & Me.BaseCurrencyName & ")"
            'grdLedger.RootTable.Columns("Balance").Caption = "Balance (" & Me.BaseCurrencyName & ")"

            ''18-04-2017
            'grdLedger.RootTable.Columns("Debit_Amount").Caption = "Debit (" & Me.cmbCurrency.Text & ")"
            'grdLedger.RootTable.Columns("Credit_Amount").Caption = "Credit (" & Me.cmbCurrency.Text & ")"
            'grdLedger.RootTable.Columns("Balance").Caption = "Balance (" & Me.cmbCurrency.Text & ")"

            Me.lblCurrency.Text = "Currency: " & cmbCurrency.Text

            Me.grdLedger.AutoSizeColumns()
            grdLedger.RootTable.Columns("Comments").Width = 275
        Catch ex As Exception
            Throw ex
        Finally
            lbl1.Visible = False
            _AccountId = 0
            _CostCenter = 0
        End Try
    End Sub




    Public Sub GetMultiCurrencyLedger()
        Dim lbl1 As New Label
        Try
            If lbl1.Visible = False Then lbl1.Visible = True
            lbl1.AutoSize = False
            lbl1.Dock = DockStyle.Fill
            lbl1.Text = "Loading..."
            Me.UltraTabControl1.Tabs(1).TabPage.Controls.Add(lbl1)
            lbl1.BringToFront()

            Me.dptFromDate = Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00")
            Me.dptToDate = Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59")
            _AccountId = Me.cmbAccount.Value
            _CostCenter = Me.cmbCostCenter.SelectedValue
            _Post = Me.chkUnPostedVouchers.Checked
            _CommissionVoucher = Me.chkCommisionVoucher.Checked

            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
            Me.grdLedger.DataSource = Nothing
            dview.ToTable.AcceptChanges()
            Me.grdLedger.DataSource = dview.ToTable
            Me.lblOpeningBalance.Text = 0
            'If Me.grdLedger.RowCount = 0 Then Exit Sub
            If Me.grdLedger.RowCount > 0 Then
                OpeningBal = Math.Round(Convert.ToDouble(Me.grdLedger.GetRow.Cells("Opening").Value), 2)
                Me.lblOpeningBalance.Text = OpeningBal.ToString("N")
                Application.DoEvents()
                debit_Amount = 0D
                credit_Amount = 0D
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdLedger.GetRows
                    debit_Amount += r.Cells("Debit_Amount").Value
                    credit_Amount += r.Cells("Credit_Amount").Value
                    r.BeginEdit()
                    r.Cells("Balance").Value = (OpeningBal + (debit_Amount - credit_Amount))
                    r.EndEdit()
                Next
            End If

            If rptTrialBalance.DrillDown = True Then
                Me.cmbAccount.Enabled = False
                Me.cmbCostCenterHead.Enabled = False
                Me.cmbCostCenter.Enabled = False
                Me.DateTimePicker1.Enabled = False
                Me.DateTimePicker2.Enabled = False

                Me.lblAccount_Title.Text = Me.cmbAccount.Text
                Me.lblAccount_Code.Text = Me.cmbAccount.ActiveRow.Cells(2).Text.ToString
                Me.lblFromDate.Text = Me.dptFromDate.ToString("dd/MMM/yyyy")
                Me.lblToDate.Text = Me.dptToDate.ToString("dd/MMM/yyyy")
                Me.lblTotalDebit.Text = Math.Round(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum), 2).ToString("N")
                Me.lblTotalCredit.Text = Math.Round(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum), 2)
                Me.lblBalance.Text = Math.Round((OpeningBal + (Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) - Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum))), 2)

            Else

                Me.cmbAccount.Enabled = True
                Me.cmbCostCenterHead.Enabled = True
                Me.cmbCostCenter.Enabled = True
                Me.DateTimePicker1.Enabled = True
                Me.DateTimePicker2.Enabled = True

                Me.lblAccount_Title.Text = Me.cmbAccount.Text
                Me.lblAccount_Code.Text = Me.cmbAccount.SelectedRow.Cells(2).Text
                Me.lblFromDate.Text = Me.dptFromDate.ToString("dd/MMM/yyyy")
                Me.lblToDate.Text = Me.dptToDate.ToString("dd/MMM/yyyy")
                Me.lblTotalDebit.Text = Math.Round(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum), 2)
                Me.lblTotalCredit.Text = Math.Round(Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum), 2)
                Me.lblBalance.Text = Math.Round((OpeningBal + (Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) - Me.grdLedger.GetTotal(Me.grdLedger.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum))), 2)

            End If


            grdLedger.RootTable.Columns("Debit_Amount").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("Debit_Amount").TotalFormatString = "N" & TotalAmountRounding


            grdLedger.RootTable.Columns("Credit_Amount").FormatString = "N" & DecimalPointInValue
            grdLedger.RootTable.Columns("Credit_Amount").TotalFormatString = "N" & TotalAmountRounding

            'Me.grdLedger.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        Finally
            lbl1.Visible = False
            _AccountId = 0
            _CostCenter = 0
        End Try
    End Sub
    Public Function GetData() As DataView
        Try
            Dim str As String = String.Empty

            ''09-Jul-2014 TASK:2728 IMRAN ALI Comments layout on ledger (Ravi)
            'str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
            '& " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Trans.comments,  " _
            '& " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post " _
            '& " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
            '& " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening " _
            '& " FROM tblVoucher V INNER JOIN " _
            '& " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
            '& " WHERE (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " _
            '& " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
            '& " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
            '& " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, V_D.comments, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post " _
            '& " FROM tblVoucher V INNER JOIN " _
            '& " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
            '& " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source " _
            '& " WHERE (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & ")  " _
            '& " Trans ON Trans.coa_detail_id = COA.coa_detail_id ORDER By Convert(Datetime, Voucher_Date, 102) Asc "

            'str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
            '& " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Trans.comments,  " _
            '& " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post " _
            '& " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
            '& " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening " _
            '& " FROM tblVoucher V INNER JOIN " _
            '& " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
            '& " WHERE (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " _
            '& " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
            '& " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
            '& " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, V_D.comments, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post " _
            '& " FROM tblVoucher V INNER JOIN " _
            '& " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
            '& " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source " _
            '& " WHERE (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & ")  " _
            '& " Trans ON Trans.coa_detail_id = COA.coa_detail_id ORDER By Convert(Datetime, Voucher_Date, 102) Asc "
            ''09-Jul-2014 TASK:2728 IMRAN ALI Comments layout on ledger (Ravi)


            '  str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
            '& " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Convert(varchar,(Convert(varchar,Trans.comments) + ' ' + Convert(Varchar,IsNull(Trans.ChequeDescription,'')))) as comments,  " _
            '& " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post " _
            '& " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
            '& " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening " _
            '& " FROM tblVoucher V INNER JOIN " _
            '& " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
            '& " WHERE (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " _
            '& " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
            '& " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
            '& " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, V_D.comments, V_D.ChequeDescription, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post " _
            '& " FROM tblVoucher V INNER JOIN " _
            '& " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
            '& " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source " _
            '& " WHERE (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & ")  " _
            '& " Trans ON Trans.coa_detail_id = COA.coa_detail_id ORDER By Convert(Datetime, Voucher_Date, 102) Asc "
            'End Task:2728

            ''23-Sep-14 TASK:2852 Imran Ali Ledger Short Description Problem Fixed
            'str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
            ' & " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Convert(nvarchar(3000),(Convert(nvarchar(3000),Trans.comments) + ' ' + Convert(nvarchar(3000),IsNull(Trans.ChequeDescription,'')))) as comments,  " _
            ' & " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post " _
            ' & " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
            ' & " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening " _
            ' & " FROM tblVoucher V INNER JOIN " _
            ' & " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
            ' & " WHERE (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " _
            ' & " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
            ' & " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
            ' & " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, Case When IsNull(V_D.LoanRequestId,0)=0 then V_D.comments else V_D.comments + ' Loan Request No:' + AdvanceRequestTable.RequestNo + ',' + Convert(varchar,AdvanceRequestTable.RequestDate) end as comments, V_D.ChequeDescription, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post " _
            ' & " FROM tblVoucher V INNER JOIN " _
            ' & " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
            ' & " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source LEFT OUTER JOIN AdvanceRequestTable on AdvanceRequestTable.RequestId = V_D.LoanRequestId " _
            ' & " WHERE (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & ")  " _
            ' & " Trans ON Trans.coa_detail_id = COA.coa_detail_id ORDER By Convert(Datetime, Voucher_Date, 102) Asc "
            'End Task:2852

            If chkMultiCurrency.Checked = False Then
                '' Commented on 19-04-2017
                'str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
                '          & " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Convert(nvarchar(3000),(Convert(nvarchar(3000),Trans.comments) + ' ' + Convert(nvarchar(3000),IsNull(Trans.ChequeDescription,'')))) as comments,  " _
                '          & " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post, 0 as Currency_Debit_Amount, 0 as Currency_Credit_Amount, 0 as CurrencyRate, 0 as CurrencyId, 'PKR' as Currency_Symbol, 0 AS CurrencyOpening " _
                '          & " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
                '          & " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening " _
                '          & " FROM tblVoucher V INNER JOIN " _
                '          & " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
                '          & " WHERE VD.coa_detail_id=" & _AccountId & " And (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " & IIf(_CostCenter > 0, " AND VD.CostCenterID=" & _CostCenter & "", "") & " " _
                '          & " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
                '          & " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
                '          & " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, Case When IsNull(V_D.LoanRequestId,0)=0 then V_D.comments else V_D.comments + ' Loan Request No:' + AdvanceRequestTable.RequestNo + ',' + Convert(varchar,AdvanceRequestTable.RequestDate) end as comments, V_D.ChequeDescription, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post " _
                '          & " FROM tblVoucher V INNER JOIN " _
                '          & " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
                '          & " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source LEFT OUTER JOIN AdvanceRequestTable on AdvanceRequestTable.RequestId = V_D.LoanRequestId " _
                '          & " WHERE V_D.coa_detail_id=" & _AccountId & " And (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " & IIf(_CostCenter > 0, " AND V_D.CostCenterId=" & _CostCenter & "", "") & ")  " _
                '          & " Trans ON Trans.coa_detail_id = COA.coa_detail_id WHERE Coa.coa_detail_id=" & _AccountId & " ORDER By Convert(Datetime, Voucher_Date, 102) Asc "
                str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, " & IIf(CurrencySymbol = "PKR", " ISNULL(Trans.Debit_Amount, 0) As debit_amount", " Case When IsNull(Trans.Currency_Debit_Amount, 0) > 0 Then Trans.Currency_Debit_Amount Else ISNULL(Trans.Debit_Amount, 0) End AS debit_amount ") & ", " _
                          & " " & IIf(CurrencySymbol = "PKR", " ISNULL(Trans.Credit_Amount, 0) As credit_amount", " Case When IsNull(Trans.Currency_Credit_Amount, 0) > 0 Then Trans.Currency_Credit_Amount Else ISNULL(Trans.Credit_Amount, 0) End AS credit_amount ") & ", Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Convert(nvarchar(3000),(Convert(nvarchar(3000),Trans.comments) + ' ' + Convert(nvarchar(3000),IsNull(Trans.ChequeDescription,'')))) as comments,  " _
                          & " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post, IsNull(Trans.Currency_Debit_Amount, 0) As Currency_Debit_Amount, IsNull(Trans.Currency_Credit_Amount, 0) As Currency_Credit_Amount, IsNull(Trans.CurrencyRate, 0) As CurrencyRate, IsNull(Trans.CurrencyId, 0) As CurrencyId, Trans.Currency_Symbol, IsNull(Opening.CurrencyOpening, 0) As CurrencyOpening " _
                          & " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
                          & " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening , SUM(ISNULL(VD.Currency_Debit_Amount, 0) - ISNULL(VD.Currency_Credit_Amount, 0)) AS CurrencyOpening " _
                          & " FROM tblVoucher V INNER JOIN " _
                          & " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
                          & " WHERE VD.coa_detail_id=" & _AccountId & " And (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(chkShowSelectCurrency.Checked = True, "AND VD.CurrencyId = " & CurrencyId & "", "") & " " & IIf(_Post = False, " AND V.Post=1 ", "") & " " & IIf(_CommissionVoucher = False, " AND V.voucher_type_id <> 12 ", "") & " " & IIf(_CostCenter > 0, " AND VD.CostCenterID=" & _CostCenter & "", "") & " " & IIf(PropertyType > 0, " And v.voucher_no IN (SELECT VoucherNo FROM PropertySales WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL UNION ALL SELECT VoucherNo FROM PropertyPurchase WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL)", "") & " " _
                          & " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
                          & " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
                          & " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, Case When IsNull(V_D.LoanRequestId,0)=0 then V_D.comments else V_D.comments + ' Loan Request No:' + AdvanceRequestTable.RequestNo + ',' + Convert(varchar,AdvanceRequestTable.RequestDate) end as comments, V_D.ChequeDescription, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post, ISNULL(V_D.Currency_Debit_Amount, 0) AS Currency_Debit_Amount, ISNULL(V_D.Currency_Credit_Amount, 0) AS Currency_Credit_Amount, ISNULL(CurrencyRate, 0) as CurrencyRate, ISNULL(CurrencyId, 0) As CurrencyId, Currency_Symbol " _
                          & " FROM tblVoucher V INNER JOIN " _
                          & " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
                          & " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source LEFT OUTER JOIN AdvanceRequestTable on AdvanceRequestTable.RequestId = V_D.LoanRequestId " _
                          & " WHERE V_D.coa_detail_id=" & _AccountId & " And (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(chkShowSelectCurrency.Checked = True, "AND V_D.CurrencyId = " & CurrencyId & "", "") & " " & IIf(_Post = False, " AND V.Post=1 ", "") & "  " & IIf(_CommissionVoucher = False, " AND V.voucher_type_id <> 12 ", "") & "  " & IIf(_CostCenter > 0, " AND V_D.CostCenterId=" & _CostCenter & "", "") & " " & IIf(PropertyType > 0, " And v.voucher_no IN (SELECT VoucherNo FROM PropertySales WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL UNION ALL SELECT VoucherNo FROM PropertyPurchase WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL)", "") & ")   " _
                          & " Trans ON Trans.coa_detail_id = COA.coa_detail_id WHERE Coa.coa_detail_id=" & _AccountId & " " & IIf(chkShowSelectCurrency.Checked = True, "OR Trans.CurrencyId = " & CurrencyId & "", "") & " ORDER By Convert(Datetime, Voucher_Date, 102) Asc "

            Else

                str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
                          & " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Convert(nvarchar(3000),(Convert(nvarchar(3000),Trans.comments) + ' ' + Convert(nvarchar(3000),IsNull(Trans.ChequeDescription,'')))) as comments,  " _
                          & " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post, Trans.Currency_Debit_Amount, Trans.Currency_Credit_Amount, Trans.CurrencyRate, Trans.CurrencyId, Trans.Currency_Symbol, ISNULL(Opening.CurrencyOpening, 0) AS CurrencyOpening " _
                          & " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
                          & " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening , SUM(ISNULL(VD.Currency_Debit_Amount, 0) - ISNULL(VD.Currency_Credit_Amount, 0)) AS CurrencyOpening " _
                          & " FROM tblVoucher V INNER JOIN " _
                          & " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
                          & " WHERE VD.coa_detail_id=" & _AccountId & " And (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(chkShowSelectCurrency.Checked = True, "AND VD.CurrencyId = " & CurrencyId & "", "") & " " & IIf(_Post = False, " AND V.Post=1 ", "") & "  " & IIf(_CommissionVoucher = False, " AND V.voucher_type_id <> 12 ", "") & "  " & IIf(_CostCenter > 0, " AND VD.CostCenterID=" & _CostCenter & "", "") & " " & IIf(PropertyType > 0, " And v.voucher_no IN (SELECT VoucherNo FROM PropertySales WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL UNION ALL SELECT VoucherNo FROM PropertyPurchase WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL)", "") & " " _
                          & " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
                          & " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
                          & " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, Case When IsNull(V_D.LoanRequestId,0)=0 then V_D.comments else V_D.comments + ' Loan Request No:' + AdvanceRequestTable.RequestNo + ',' + Convert(varchar,AdvanceRequestTable.RequestDate) end as comments, V_D.ChequeDescription, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post, ISNULL(V_D.Currency_Debit_Amount, 0) AS Currency_Debit_Amount, ISNULL(V_D.Currency_Credit_Amount, 0) AS Currency_Credit_Amount, ISNULL(CurrencyRate, 0) as CurrencyRate, ISNULL(CurrencyId, 0) As CurrencyId, Currency_Symbol " _
                          & " FROM tblVoucher V INNER JOIN " _
                          & " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
                          & " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source LEFT OUTER JOIN AdvanceRequestTable on AdvanceRequestTable.RequestId = V_D.LoanRequestId " _
                          & " WHERE V_D.coa_detail_id=" & _AccountId & " And (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(chkShowSelectCurrency.Checked = True, "AND V_D.CurrencyId = " & CurrencyId & "", "") & " " & IIf(_Post = False, " AND V.Post=1 ", "") & "  " & IIf(_CommissionVoucher = False, " AND V.voucher_type_id <> 12 ", "") & "  " & IIf(_CostCenter > 0, " AND V_D.CostCenterId=" & _CostCenter & "", "") & " " & IIf(PropertyType > 0, " And v.voucher_no IN (SELECT VoucherNo FROM PropertySales WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL UNION ALL SELECT VoucherNo FROM PropertyPurchase WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL)", "") & " )   " _
                          & " Trans ON Trans.coa_detail_id = COA.coa_detail_id WHERE Coa.coa_detail_id=" & _AccountId & " " & IIf(chkShowSelectCurrency.Checked = True, "OR Trans.CurrencyId = " & CurrencyId & "", "") & " ORDER By Convert(Datetime, Voucher_Date, 102) Asc "

            End If
            If Not dtData Is Nothing Then
                dtData.Clear()
            End If
            If Not dtData Is Nothing Then
                dview.Table.Clear()
            End If
            dtData = GetDataTable(str)
            dtData.Columns.Add("Balance", GetType(System.Double))
            dtData.Columns.Add("CurrencyBalance", GetType(System.Double))
            dtData.AcceptChanges()
            'Dim dv As New DataView
            dview = dtData.DefaultView
            'dview.RowFilter = "coa_detail_id=" & _AccountId & " " & IIf(_CostCenter > 0, " AND CostCenterId=" & _CostCenter & "", "") & ""
            'dview.Table.AcceptChanges()
            Return dview
        Catch ex As Exception
            'ShowErrorMessage("Error occurred while geting data: " & ex.Message)
            Throw ex
        End Try
    End Function

    'Public Function GetMultiCostCenterRecord(ByVal _AccountId As Integer, Optional ByVal CostCenterIds As String = "") As DataView
    '    Try
    '        Dim str As String = ""
    '        str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, " & IIf(CurrencySymbol = "PKR", " ISNULL(Trans.Debit_Amount, 0) As debit_amount", " Case When IsNull(Trans.Currency_Debit_Amount, 0) > 0 Then Trans.Currency_Debit_Amount Else ISNULL(Trans.Debit_Amount, 0) End AS debit_amount ") & ", " _
    '                      & " " & IIf(CurrencySymbol = "PKR", " ISNULL(Trans.Credit_Amount, 0) As credit_amount", " Case When IsNull(Trans.Currency_Credit_Amount, 0) > 0 Then Trans.Currency_Credit_Amount Else ISNULL(Trans.Credit_Amount, 0) End AS credit_amount ") & ", Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Convert(nvarchar(3000),(Convert(nvarchar(3000),Trans.comments) + ' ' + Convert(nvarchar(3000),IsNull(Trans.ChequeDescription,'')))) as comments,  " _
    '                      & " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post, IsNull(Trans.Currency_Debit_Amount, 0) As Currency_Debit_Amount, IsNull(Trans.Currency_Credit_Amount, 0) As Currency_Credit_Amount, IsNull(Trans.CurrencyRate, 0) As CurrencyRate, IsNull(Trans.CurrencyId, 0) As CurrencyId, Trans.Currency_Symbol, IsNull(Opening.CurrencyOpening, 0) As CurrencyOpening " _
    '                      & " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
    '                      & " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening , SUM(ISNULL(VD.Currency_Debit_Amount, 0) - ISNULL(VD.Currency_Credit_Amount, 0)) AS CurrencyOpening " _
    '                      & " FROM tblVoucher V INNER JOIN " _
    '                      & " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
    '                      & " WHERE VD.coa_detail_id=" & _AccountId & " And (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " & IIf(CostCenterIds = "" Or CostCenterIds Is Nothing Or CostCenterIds = "0", "", "And VD.CostCenterID IN (" & CostCenterIds & ")") & " " _
    '                      & " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
    '                      & " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
    '                      & " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, Case When IsNull(V_D.LoanRequestId,0)=0 then V_D.comments else V_D.comments + ' Loan Request No:' + AdvanceRequestTable.RequestNo + ',' + Convert(varchar,AdvanceRequestTable.RequestDate) end as comments, V_D.ChequeDescription, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post, ISNULL(V_D.Currency_Debit_Amount, 0) AS Currency_Debit_Amount, ISNULL(V_D.Currency_Credit_Amount, 0) AS Currency_Credit_Amount, ISNULL(CurrencyRate, 0) as CurrencyRate, ISNULL(CurrencyId, 0) As CurrencyId, Currency_Symbol " _
    '                      & " FROM tblVoucher V INNER JOIN " _
    '                      & " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
    '                      & " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source LEFT OUTER JOIN AdvanceRequestTable on AdvanceRequestTable.RequestId = V_D.LoanRequestId " _
    '                      & " WHERE V_D.coa_detail_id=" & _AccountId & " And (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " & IIf(CostCenterIds = "" Or CostCenterIds Is Nothing Or CostCenterIds = "0", "", "And V_D.CostCenterID IN (" & CostCenterIds & ")") & ")  " _
    '                      & " Trans ON Trans.coa_detail_id = COA.coa_detail_id WHERE Coa.coa_detail_id=" & _AccountId & " ORDER By Convert(Datetime, Voucher_Date, 102) Asc "
    '        If Not dtData Is Nothing Then
    '            dtData.Clear()
    '        End If
    '        If Not dtData Is Nothing Then
    '            dview.Table.Clear()
    '        End If
    '        dtData = GetDataTable(str)
    '        dtData.Columns.Add("Balance", GetType(System.Double))
    '        dtData.Columns.Add("CurrencyBalance", GetType(System.Double))
    '        dtData.AcceptChanges()
    '        'Dim dv As New DataView
    '        dview = dtData.DefaultView
    '        'dview.RowFilter = "coa_detail_id=" & _AccountId & " " & IIf(_CostCenter > 0, " AND CostCenterId=" & _CostCenter & "", "") & ""
    '        'dview.Table.AcceptChanges()
    '        Return dview
    '        'Tracking = False
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function



    Public Function GetMultiCostCenterRecord(ByVal _AccountId As Integer, ByVal PropertyType As Integer, Optional ByVal CostCenterIds As String = "") As DataView
        Try
            Dim str As String = ""
            str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, " & IIf(CurrencySymbol = "PKR", " ISNULL(Trans.Debit_Amount, 0) As debit_amount", " Case When IsNull(Trans.Currency_Debit_Amount, 0) > 0 Then Trans.Currency_Debit_Amount Else ISNULL(Trans.Debit_Amount, 0) End AS debit_amount ") & ", " _
                          & " " & IIf(CurrencySymbol = "PKR", " ISNULL(Trans.Credit_Amount, 0) As credit_amount", " Case When IsNull(Trans.Currency_Credit_Amount, 0) > 0 Then Trans.Currency_Credit_Amount Else ISNULL(Trans.Credit_Amount, 0) End AS credit_amount ") & ", Trans.Voucher_Code, Trans.InvoiceId, Trans.Voucher_Type, Trans.Voucher_Date, Convert(nvarchar(3000),(Convert(nvarchar(3000),Trans.comments) + ' ' + Convert(nvarchar(3000),IsNull(Trans.ChequeDescription,'')))) as comments,  " _
                          & " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post, IsNull(Trans.Currency_Debit_Amount, 0) As Currency_Debit_Amount, IsNull(Trans.Currency_Credit_Amount, 0) As Currency_Credit_Amount, IsNull(Trans.CurrencyRate, 0) As CurrencyRate, IsNull(Trans.CurrencyId, 0) As CurrencyId, Trans.Currency_Symbol, IsNull(Opening.CurrencyOpening, 0) As CurrencyOpening " _
                          & " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
                          & " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening , SUM(ISNULL(VD.Currency_Debit_Amount, 0) - ISNULL(VD.Currency_Credit_Amount, 0)) AS CurrencyOpening " _
                          & " FROM tblVoucher V INNER JOIN " _
                          & " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
                          & " WHERE VD.coa_detail_id=" & _AccountId & " And (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " & IIf(CostCenterIds = "" Or CostCenterIds Is Nothing, "", "And ISNULL(VD.CostCenterID, 0) IN (" & CostCenterIds & ")") & "  " & IIf(company = 0, "", "And isnull(V.location_id, 0) = " & company) & "  " & IIf(PropertyType > 0, " And v.voucher_no IN (SELECT VoucherNo FROM PropertySales WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL UNION ALL SELECT VoucherNo FROM PropertyPurchase WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL)", "") & " " _
                          & " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
                          & " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
                          & " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, Case When IsNull(V_D.LoanRequestId,0)=0 then V_D.comments else V_D.comments + ' Loan Request No:' + AdvanceRequestTable.RequestNo + ',' + Convert(varchar,AdvanceRequestTable.RequestDate) end as comments, V_D.ChequeDescription, ISNULL(V_D.InvoiceId, 0) as InvoiceId, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post, ISNULL(V_D.Currency_Debit_Amount, 0) AS Currency_Debit_Amount, ISNULL(V_D.Currency_Credit_Amount, 0) AS Currency_Credit_Amount, ISNULL(CurrencyRate, 0) as CurrencyRate, ISNULL(CurrencyId, 0) As CurrencyId, Currency_Symbol " _
                          & " FROM tblVoucher V INNER JOIN " _
                          & " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
                          & " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source LEFT OUTER JOIN AdvanceRequestTable on AdvanceRequestTable.RequestId = V_D.LoanRequestId " _
                          & " WHERE V_D.coa_detail_id=" & _AccountId & " And (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & "  " & IIf(_CommissionVoucher = False, " AND V.voucher_type_id <> 12 ", "") & "  " & IIf(CostCenterIds = "" Or CostCenterIds Is Nothing, "", "And ISNULL(V_D.CostCenterID, 0) IN (" & CostCenterIds & ")") & "  " & IIf(company = 0, "", "And isnull(V.location_id, 0) = " & company) & "  " & IIf(PropertyType > 0, " And v.voucher_no IN (SELECT VoucherNo FROM PropertySales WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL UNION ALL SELECT VoucherNo FROM PropertyPurchase WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL)", "") & " " _
                          & " ) Trans ON Trans.coa_detail_id = COA.coa_detail_id WHERE Coa.coa_detail_id=" & _AccountId & " ORDER By Convert(Datetime, Voucher_Date, 102) Asc "
            If Not dtData Is Nothing Then
                dtData.Clear()
            End If
            If Not dtData Is Nothing Then
                dview.Table.Clear()
            End If
            dtData = GetDataTable(str)
            'Dim TotalRows As Integer = dtData.Rows.Count
            'Dim FilterExpression As String
            'Dim dtUniqueInvoiceID As DataTable = dtData.DefaultView.ToTable(True, "InvoiceId")
            'For Each Invoice As DataRow In dtUniqueInvoiceID.Rows
            '    For Each Data As DataRow In dtData.Rows
            '        FilterExpression = Invoice.Item("InvoiceId") & " = " & Data.Item("InvoiceId") & " AND " & dtData.Compute("Sum(Debit_Amount)", "InvoiceId = " & Invoice.Item("InvoiceId")) & " = " & dtData.Compute("Sum(Credit_Amount)", "InvoiceId = " & Invoice.Item("InvoiceId"))
            '            Dim DataForVoucher() As DataRow
            '        DataForVoucher = dtData.Select(FilterExpression)
            '        Dim TotalRows1 As Integer = DataForVoucher.Length
            '        'DataForVoucher.CopyToDataTable.Rows.Clear()
            '        If DataForVoucher.Length > 0 Then
            '            For Each Row As DataRow In dtData.Select(FilterExpression).CopyToDataTable.Rows

            '                dtData.Rows.Remove(Row)
            '                'Row.BeginEdit()
            '                'Row.Delete()
            '                'Row.EndEdit()
            '                dtData.AcceptChanges()
            '            Next
            '        End If

            '    Next


            'Next
            dtData.Columns.Add("Balance", GetType(System.Double))
            dtData.Columns.Add("CurrencyBalance", GetType(System.Double))
            dtData.AcceptChanges()
            'Dim dv As New DataView
            dview = dtData.DefaultView
            'dview.RowFilter = "coa_detail_id=" & _AccountId & " " & IIf(_CostCenter > 0, " AND CostCenterId=" & _CostCenter & "", "") & ""
            'dview.Table.AcceptChanges()
            Return dview
            'Tracking = False
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Function GetDataReceiveables() As DataView
        Try
            'TFS3406: Waqar Raza: Modify this Query because in this query i was comparing String with Double value which shows the error
            'Start TFS3406
            Dim str As String = String.Empty
            If chkMultiCurrency.Checked = False Then
                str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, " & IIf(CurrencySymbol = "PKR", " ISNULL(Trans.Debit_Amount, 0) As debit_amount", " Case When IsNull(Trans.Currency_Debit_Amount, 0) > 0 Then Trans.Currency_Debit_Amount Else ISNULL(Trans.Debit_Amount, 0) End AS debit_amount ") & ", " _
                          & " " & IIf(CurrencySymbol = "PKR", " ISNULL(Trans.Credit_Amount, 0) As Credit_Amount", " Case When IsNull(Trans.Currency_Credit_Amount, 0) > 0 Then Trans.Currency_Credit_Amount Else ISNULL(Trans.Credit_Amount, 0) End AS credit_amount ") & ", Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Convert(nvarchar(3000),(Convert(nvarchar(3000),Trans.comments) + ' ' + Convert(nvarchar(3000),IsNull(Trans.ChequeDescription,'')))) as comments,  " _
                          & " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post, IsNull(Trans.Currency_Debit_Amount, 0) As Currency_Debit_Amount, IsNull(Trans.Currency_Credit_Amount, 0) As Currency_Credit_Amount, IsNull(Trans.CurrencyRate, 0) As CurrencyRate, IsNull(Trans.CurrencyId, 0) As CurrencyId, Trans.Currency_Symbol, IsNull(Opening.CurrencyOpening, 0) As CurrencyOpening " _
                          & " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
                          & " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening , SUM(ISNULL(VD.Currency_Debit_Amount, 0) - ISNULL(VD.Currency_Credit_Amount, 0)) AS CurrencyOpening " _
                          & " FROM tblVoucher V INNER JOIN " _
                          & " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
                          & " WHERE VD.coa_detail_id=" & _AccountId & " And (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " & IIf(Costid <> "", " AND ISNULL(VD.CostCenterID, 0) IN (" & Costid & ")", "") & " " & IIf(PropertyType > 0, " And v.voucher_no IN (SELECT VoucherNo FROM PropertySales WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL UNION ALL SELECT VoucherNo FROM PropertyPurchase WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL)", "") & " " _
                          & " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
                          & " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
                          & " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, Case When IsNull(V_D.LoanRequestId,0)=0 then V_D.comments else V_D.comments + ' Loan Request No:' + AdvanceRequestTable.RequestNo + ',' + Convert(varchar,AdvanceRequestTable.RequestDate) end as comments, V_D.ChequeDescription, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post, ISNULL(V_D.Currency_Debit_Amount, 0) AS Currency_Debit_Amount, ISNULL(V_D.Currency_Credit_Amount, 0) AS Currency_Credit_Amount, ISNULL(CurrencyRate, 0) as CurrencyRate, ISNULL(CurrencyId, 0) As CurrencyId, Currency_Symbol " _
                          & " FROM tblVoucher V INNER JOIN " _
                          & " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
                          & " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source LEFT OUTER JOIN AdvanceRequestTable on AdvanceRequestTable.RequestId = V_D.LoanRequestId " _
                          & " WHERE V_D.coa_detail_id=" & _AccountId & " And (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & "  " & IIf(_CommissionVoucher = False, " AND V.voucher_type_id <> 12 ", "") & "  " & IIf(Costid <> "", " AND ISNULL(V_D.CostCenterId, 0) IN (" & Costid & ")", "") & " " & IIf(PropertyType > 0, " And v.voucher_no IN (SELECT VoucherNo FROM PropertySales WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL UNION ALL SELECT VoucherNo FROM PropertyPurchase WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL)", "") & " " _
                          & " ) Trans ON Trans.coa_detail_id = COA.coa_detail_id WHERE Coa.coa_detail_id=" & _AccountId & " AND ISNULL(Trans.Debit_Amount, 0) > 0 ORDER By Convert(Datetime, Voucher_Date, 102) Asc "

            Else

                str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
                          & " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Convert(nvarchar(3000),(Convert(nvarchar(3000),Trans.comments) + ' ' + Convert(nvarchar(3000),IsNull(Trans.ChequeDescription,'')))) as comments,  " _
                          & " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post, Trans.Currency_Debit_Amount, Trans.Currency_Credit_Amount, Trans.CurrencyRate, Trans.CurrencyId, Trans.Currency_Symbol, ISNULL(Opening.CurrencyOpening, 0) AS CurrencyOpening " _
                          & " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
                          & " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening , SUM(ISNULL(VD.Currency_Debit_Amount, 0) - ISNULL(VD.Currency_Credit_Amount, 0)) AS CurrencyOpening " _
                          & " FROM tblVoucher V INNER JOIN " _
                          & " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
                          & " WHERE VD.coa_detail_id=" & _AccountId & " And (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " & IIf(Costid <> "", " AND ISNULL(VD.CostCenterID, 0) IN (" & Costid & ")", "") & " " & IIf(PropertyType > 0, " And v.voucher_no IN (SELECT VoucherNo FROM PropertySales WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL UNION ALL SELECT VoucherNo FROM PropertyPurchase WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL)", "") & " " _
                          & " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
                          & " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
                          & " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, Case When IsNull(V_D.LoanRequestId,0)=0 then V_D.comments else V_D.comments + ' Loan Request No:' + AdvanceRequestTable.RequestNo + ',' + Convert(varchar,AdvanceRequestTable.RequestDate) end as comments, V_D.ChequeDescription, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post, ISNULL(V_D.Currency_Debit_Amount, 0) AS Currency_Debit_Amount, ISNULL(V_D.Currency_Credit_Amount, 0) AS Currency_Credit_Amount, ISNULL(CurrencyRate, 0) as CurrencyRate, ISNULL(CurrencyId, 0) As CurrencyId, Currency_Symbol " _
                          & " FROM tblVoucher V INNER JOIN " _
                          & " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
                          & " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source LEFT OUTER JOIN AdvanceRequestTable on AdvanceRequestTable.RequestId = V_D.LoanRequestId " _
                          & " WHERE V_D.coa_detail_id=" & _AccountId & " And (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & "  " & IIf(_CommissionVoucher = False, " AND V.voucher_type_id <> 12 ", "") & "  " & IIf(Costid <> "", " AND ISNULL(V_D.CostCenterId, 0) IN (" & Costid & ")", "") & " " & IIf(PropertyType > 0, " And v.voucher_no IN (SELECT VoucherNo FROM PropertySales WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL UNION ALL SELECT VoucherNo FROM PropertyPurchase WHERE SerialNo IN (SELECT DocNo FROM PropertyProfile INNER JOIN PropertyItem on PropertyProfile.InvId = PropertyItem.PropertyItemId WHERE PropertyItem.PropertyTypeId = " & PropertyType & ") AND VoucherNo IS NOT NULL)", "") & "  " _
                          & " ) Trans ON Trans.coa_detail_id = COA.coa_detail_id WHERE Coa.coa_detail_id=" & _AccountId & " AND ISNULL(Trans.Debit_Amount, 0) > 0 ORDER By Convert(Datetime, Voucher_Date, 102) Asc "
                'End TFS3406 
            End If
            If Not dtData Is Nothing Then
                dtData.Clear()
            End If
            If Not dtData Is Nothing Then
                dview.Table.Clear()
            End If
            dtData = GetDataTable(str)
            dtData.Columns.Add("Balance", GetType(System.Double))
            dtData.Columns.Add("CurrencyBalance", GetType(System.Double))
            dtData.AcceptChanges()
            'Dim dv As New DataView
            dview = dtData.DefaultView
            'dview.RowFilter = "coa_detail_id=" & _AccountId & " " & IIf(_CostCenter > 0, " AND CostCenterId=" & _CostCenter & "", "") & ""
            'dview.Table.AcceptChanges()
            Return dview
        Catch ex As Exception
            'ShowErrorMessage("Error occurred while geting data: " & ex.Message)
            Throw ex
        End Try
    End Function



    Public Function GetDataPayables() As DataView
        Try
            Dim str As String = String.Empty

            ''09-Jul-2014 TASK:2728 IMRAN ALI Comments layout on ledger (Ravi)
            'str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
            '& " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Trans.comments,  " _
            '& " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post " _
            '& " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
            '& " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening " _
            '& " FROM tblVoucher V INNER JOIN " _
            '& " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
            '& " WHERE (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " _
            '& " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
            '& " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
            '& " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, V_D.comments, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post " _
            '& " FROM tblVoucher V INNER JOIN " _
            '& " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
            '& " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source " _
            '& " WHERE (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & ")  " _
            '& " Trans ON Trans.coa_detail_id = COA.coa_detail_id ORDER By Convert(Datetime, Voucher_Date, 102) Asc "

            'str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
            '& " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Trans.comments,  " _
            '& " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post " _
            '& " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
            '& " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening " _
            '& " FROM tblVoucher V INNER JOIN " _
            '& " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
            '& " WHERE (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " _
            '& " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
            '& " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
            '& " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, V_D.comments, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post " _
            '& " FROM tblVoucher V INNER JOIN " _
            '& " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
            '& " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source " _
            '& " WHERE (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & ")  " _
            '& " Trans ON Trans.coa_detail_id = COA.coa_detail_id ORDER By Convert(Datetime, Voucher_Date, 102) Asc "
            ''09-Jul-2014 TASK:2728 IMRAN ALI Comments layout on ledger (Ravi)


            '  str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
            '& " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Convert(varchar,(Convert(varchar,Trans.comments) + ' ' + Convert(Varchar,IsNull(Trans.ChequeDescription,'')))) as comments,  " _
            '& " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post " _
            '& " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
            '& " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening " _
            '& " FROM tblVoucher V INNER JOIN " _
            '& " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
            '& " WHERE (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " _
            '& " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
            '& " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
            '& " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, V_D.comments, V_D.ChequeDescription, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post " _
            '& " FROM tblVoucher V INNER JOIN " _
            '& " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
            '& " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source " _
            '& " WHERE (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & ")  " _
            '& " Trans ON Trans.coa_detail_id = COA.coa_detail_id ORDER By Convert(Datetime, Voucher_Date, 102) Asc "
            'End Task:2728

            ''23-Sep-14 TASK:2852 Imran Ali Ledger Short Description Problem Fixed
            'str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
            ' & " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Convert(nvarchar(3000),(Convert(nvarchar(3000),Trans.comments) + ' ' + Convert(nvarchar(3000),IsNull(Trans.ChequeDescription,'')))) as comments,  " _
            ' & " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post " _
            ' & " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
            ' & " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening " _
            ' & " FROM tblVoucher V INNER JOIN " _
            ' & " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
            ' & " WHERE (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " _
            ' & " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
            ' & " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
            ' & " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, Case When IsNull(V_D.LoanRequestId,0)=0 then V_D.comments else V_D.comments + ' Loan Request No:' + AdvanceRequestTable.RequestNo + ',' + Convert(varchar,AdvanceRequestTable.RequestDate) end as comments, V_D.ChequeDescription, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post " _
            ' & " FROM tblVoucher V INNER JOIN " _
            ' & " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
            ' & " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source LEFT OUTER JOIN AdvanceRequestTable on AdvanceRequestTable.RequestId = V_D.LoanRequestId " _
            ' & " WHERE (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & ")  " _
            ' & " Trans ON Trans.coa_detail_id = COA.coa_detail_id ORDER By Convert(Datetime, Voucher_Date, 102) Asc "
            'End Task:2852

            If chkMultiCurrency.Checked = False Then
                '' Commented on 19-04-2017
                'str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
                '          & " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Convert(nvarchar(3000),(Convert(nvarchar(3000),Trans.comments) + ' ' + Convert(nvarchar(3000),IsNull(Trans.ChequeDescription,'')))) as comments,  " _
                '          & " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post, 0 as Currency_Debit_Amount, 0 as Currency_Credit_Amount, 0 as CurrencyRate, 0 as CurrencyId, 'PKR' as Currency_Symbol, 0 AS CurrencyOpening " _
                '          & " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
                '          & " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening " _
                '          & " FROM tblVoucher V INNER JOIN " _
                '          & " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
                '          & " WHERE VD.coa_detail_id=" & _AccountId & " And (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " & IIf(_CostCenter > 0, " AND VD.CostCenterID=" & _CostCenter & "", "") & " " _
                '          & " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
                '          & " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
                '          & " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, Case When IsNull(V_D.LoanRequestId,0)=0 then V_D.comments else V_D.comments + ' Loan Request No:' + AdvanceRequestTable.RequestNo + ',' + Convert(varchar,AdvanceRequestTable.RequestDate) end as comments, V_D.ChequeDescription, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post " _
                '          & " FROM tblVoucher V INNER JOIN " _
                '          & " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
                '          & " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source LEFT OUTER JOIN AdvanceRequestTable on AdvanceRequestTable.RequestId = V_D.LoanRequestId " _
                '          & " WHERE V_D.coa_detail_id=" & _AccountId & " And (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & " " & IIf(_CostCenter > 0, " AND V_D.CostCenterId=" & _CostCenter & "", "") & ")  " _
                '          & " Trans ON Trans.coa_detail_id = COA.coa_detail_id WHERE Coa.coa_detail_id=" & _AccountId & " ORDER By Convert(Datetime, Voucher_Date, 102) Asc "
                str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, " & IIf(CurrencySymbol = "PKR", " ISNULL(Trans.Debit_Amount, 0) As debit_amount", " Case When IsNull(Trans.Currency_Debit_Amount, 0) > 0 Then Trans.Currency_Debit_Amount Else ISNULL(Trans.Debit_Amount, 0) End AS debit_amount ") & ", " _
                          & " " & IIf(CurrencySymbol = "PKR", " ISNULL(Trans.Credit_Amount, 0) As credit_amount", " Case When IsNull(Trans.Currency_Credit_Amount, 0) > 0 Then Trans.Currency_Credit_Amount Else ISNULL(Trans.Credit_Amount, 0) End AS credit_amount ") & ", Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Convert(nvarchar(3000),(Convert(nvarchar(3000),Trans.comments) + ' ' + Convert(nvarchar(3000),IsNull(Trans.ChequeDescription,'')))) as comments,  " _
                          & " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post, IsNull(Trans.Currency_Debit_Amount, 0) As Currency_Debit_Amount, IsNull(Trans.Currency_Credit_Amount, 0) As Currency_Credit_Amount, IsNull(Trans.CurrencyRate, 0) As CurrencyRate, IsNull(Trans.CurrencyId, 0) As CurrencyId, Trans.Currency_Symbol, IsNull(Opening.CurrencyOpening, 0) As CurrencyOpening " _
                          & " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
                          & " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening , SUM(ISNULL(VD.Currency_Debit_Amount, 0) - ISNULL(VD.Currency_Credit_Amount, 0)) AS CurrencyOpening " _
                          & " FROM tblVoucher V INNER JOIN " _
                          & " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
                          & " WHERE VD.coa_detail_id=" & _AccountId & " And (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & "  " & IIf(_CommissionVoucher = False, " AND V.voucher_type_id <> 12 ", "") & "  " & IIf(_CostCenter > 0, " AND VD.CostCenterID=" & _CostCenter & "", "") & " " _
                          & " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
                          & " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
                          & " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, Case When IsNull(V_D.LoanRequestId,0)=0 then V_D.comments else V_D.comments + ' Loan Request No:' + AdvanceRequestTable.RequestNo + ',' + Convert(varchar,AdvanceRequestTable.RequestDate) end as comments, V_D.ChequeDescription, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post, ISNULL(V_D.Currency_Debit_Amount, 0) AS Currency_Debit_Amount, ISNULL(V_D.Currency_Credit_Amount, 0) AS Currency_Credit_Amount, ISNULL(CurrencyRate, 0) as CurrencyRate, ISNULL(CurrencyId, 0) As CurrencyId, Currency_Symbol " _
                          & " FROM tblVoucher V INNER JOIN " _
                          & " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
                          & " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source LEFT OUTER JOIN AdvanceRequestTable on AdvanceRequestTable.RequestId = V_D.LoanRequestId " _
                          & " WHERE V_D.coa_detail_id=" & _AccountId & " And (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & "  " & IIf(_CommissionVoucher = False, " AND V.voucher_type_id <> 12 ", "") & "  " & IIf(_CostCenter > 0, " AND V_D.CostCenterId=" & _CostCenter & "", "") & ")  " _
                          & " Trans ON Trans.coa_detail_id = COA.coa_detail_id WHERE Coa.coa_detail_id=" & _AccountId & " AND ISNULL(Trans.Credit_Amount, 0) > 0 ORDER By Convert(Datetime, Voucher_Date, 102) Asc "

            Else

                str = "  SELECT  ISNULL(Opening.Opening, 0) AS Opening, COA.coa_detail_id, COA.detail_title, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
                          & " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Convert(nvarchar(3000),(Convert(nvarchar(3000),Trans.comments) + ' ' + Convert(nvarchar(3000),IsNull(Trans.ChequeDescription,'')))) as comments,  " _
                          & " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.sp_refrence, COA.detail_code, Trans.Source, Isnull(Trans.Post,0) as Post, Trans.Currency_Debit_Amount, Trans.Currency_Credit_Amount, Trans.CurrencyRate, Trans.CurrencyId, Trans.Currency_Symbol, ISNULL(Opening.CurrencyOpening, 0) AS CurrencyOpening " _
                          & " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
                          & " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening , SUM(ISNULL(VD.Currency_Debit_Amount, 0) - ISNULL(VD.Currency_Credit_Amount, 0)) AS CurrencyOpening " _
                          & " FROM tblVoucher V INNER JOIN " _
                          & " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source  " _
                          & " WHERE VD.coa_detail_id=" & _AccountId & " And (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & "  " & IIf(_CommissionVoucher = False, " AND V.voucher_type_id <> 12 ", "") & "  " & IIf(_CostCenter > 0, " AND isnull(VD.CostCenterID, 0)=" & _CostCenter & "", "") & " " _
                          & " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
                          & " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
                          & " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, Case When IsNull(V_D.LoanRequestId,0)=0 then V_D.comments else V_D.comments + ' Loan Request No:' + AdvanceRequestTable.RequestNo + ',' + Convert(varchar,AdvanceRequestTable.RequestDate) end as comments, V_D.ChequeDescription, ISNULL(V_D.CostCenterId, 0) AS CostCenterId, V_D.sp_refrence, frm.AccessKey as Source, isnull(V.Post,0) as Post, ISNULL(V_D.Currency_Debit_Amount, 0) AS Currency_Debit_Amount, ISNULL(V_D.Currency_Credit_Amount, 0) AS Currency_Credit_Amount, ISNULL(CurrencyRate, 0) as CurrencyRate, ISNULL(CurrencyId, 0) As CurrencyId, Currency_Symbol " _
                          & " FROM tblVoucher V INNER JOIN " _
                          & " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
                          & " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblForm frm ON frm.Form_Name = V.Source LEFT OUTER JOIN AdvanceRequestTable on AdvanceRequestTable.RequestId = V_D.LoanRequestId " _
                          & " WHERE V_D.coa_detail_id=" & _AccountId & " And (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & dptFromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & dptToDate.ToString("yyyy-M-d 23:23:59") & "', 102)) " & IIf(_Post = False, " AND V.Post=1 ", "") & "  " & IIf(_CommissionVoucher = False, " AND V.voucher_type_id <> 12 ", "") & "  " & IIf(_CostCenter > 0, " AND isnull(V_D.CostCenterId, 0)=" & _CostCenter & "", "") & ")  " _
                          & " Trans ON Trans.coa_detail_id = COA.coa_detail_id WHERE Coa.coa_detail_id=" & _AccountId & " AND ISNULL(Trans.Credit_Amount, 0) > 0 ORDER By Convert(Datetime, Voucher_Date, 102) Asc "

            End If
            If Not dtData Is Nothing Then
                dtData.Clear()
            End If
            If Not dtData Is Nothing Then
                dview.Table.Clear()
            End If
            dtData = GetDataTable(str)
            dtData.Columns.Add("Balance", GetType(System.Double))
            dtData.Columns.Add("CurrencyBalance", GetType(System.Double))
            dtData.AcceptChanges()
            'Dim dv As New DataView
            dview = dtData.DefaultView
            'dview.RowFilter = "coa_detail_id=" & _AccountId & " " & IIf(_CostCenter > 0, " AND CostCenterId=" & _CostCenter & "", "") & ""
            'dview.Table.AcceptChanges()
            Return dview
        Catch ex As Exception
            'ShowErrorMessage("Error occurred while geting data: " & ex.Message)
            Throw ex
        End Try
    End Function






    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.cmbAccount.ActiveRow Is Nothing Then
            ShowErrorMessage("Please select any Account")
            Me.cmbAccount.Focus()
            Exit Sub
        End If
        If Me.cmbAccount.ActiveRow.Cells(0).Value = 0 Then
            ShowErrorMessage("Please select any Account")
            Me.cmbAccount.Focus()
            Exit Sub
        End If
        'Ali Faisal : Clear Grid before Generating Ledger if any record exist on 30-Jan-2017
        If grdLedger.DataSource IsNot Nothing Then
            grdLedger.DataSource = Nothing
        End If
        Me.Cursor = Cursors.WaitCursor
        Dim lbl As New Label
        Try
            CurrencyId = cmbCurrency.SelectedValue
            lbl.AutoSize = False
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()

            'If Me.cmbAccount.SelectedRow.Cells(0).Value = 0 Then Exit Sub
            If Me.optDetail.Checked Then
                Me.optDetail.Checked = True
            Else
                Me.optDetail.Checked = True
            End If
            rptTrialBalance.DrillDown = False


            'Me.lblAccount_Title.Text = String.Empty
            'Me.lblAccount_Code.Text = String.Empty
            'Me.lblFromDate.Text = Me.dptFromDate.ToString("dd/MMM/yyyy")
            'Me.lblToDate.Text = Me.dptToDate.ToString("dd/MMM/yyyy")
            'Me.lblTotalDebit.Text = 0D
            'Me.lblTotalCredit.Text = 0D '
            'Me.lblBalance.Text = 0D


            GetLedger()
            'Before against task no. 2363
            'Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
            'Tsk:2363 tab's index change
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(2).TabPage.Tab
            'End Tsk:2363


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            rptTrialBalance.DrillDown = False
            Me.cmbAccount.Enabled = True
            Me.cmbCostCenterHead.Enabled = True
            Me.cmbCostCenter.Enabled = True
            Me.DateTimePicker1.Enabled = True
            Me.DateTimePicker2.Enabled = True
            Me.cmbAccount.Value = 0
            Me.cmbPeriod.Text = "Current Month"
            Me.cmbCostCenterHead.SelectedIndex = 0
            Me.cmbCostCenter.SelectedIndex = 0
            Me.chkIncludeCostCenter.Checked = False
            Me.optDetail.Checked = True
            Me.lblAccount_Title.Text = String.Empty
            Me.lblAccount_Code.Text = String.Empty
            Me.lblFromDate.Text = String.Empty
            Me.lblToDate.Text = String.Empty
            Me.lblTotalDebit.Text = 0
            Me.lblTotalCredit.Text = 0
            Me.lblBalance.Text = 0
            Me.lblOpeningBalance.Text = 0
            Me.chkShowSelectCurrency.Checked = False
            Tracking = False
            'GetLedger()
            'Me.grdLedger.DataSource = Nothing
            If grdLedger.DataSource IsNot Nothing Then
                grdLedger.DataSource = Nothing
                'GetData.Table.Clear()
            End If
            If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                Me.lstAddedAccounts.ListItem.DataSource = Nothing
                Me.TextBox2.Text = String.Empty
                Me.TextBox1.Text = String.Empty
                _AddCOA.Clear()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub grdLedger_LinkClicked(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdLedger.LinkClicked
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdLedger.RowCount = 0 Then Exit Sub
            frmModProperty.Tags = String.Empty
            If Me.cmbAccount.ActiveRow.Cells(0).Value = 0 Then
                ShowErrorMessage("Please select any account")
                Me.cmbAccount.Focus()
                Exit Sub
            End If
            If e.Column.Key = "Voucher_Code" Then
                frmMain.Tags = Me.grdLedger.GetRow.Cells("Voucher_Code").Text
                If IsDrillDown = True Then
                    frmMain.LoadControl(Me.grdLedger.GetRow.Cells("Source").Text.ToString)
                    System.Threading.Thread.Sleep(500)
                Else
                    frmMain.LoadControl(Me.grdLedger.GetRow.Cells("Source").Text.ToString)
                    System.Threading.Thread.Sleep(500)
                    frmMain.Tags = Me.grdLedger.GetRow.Cells("Voucher_Code").Text
                    frmMain.LoadControl(Me.grdLedger.GetRow.Cells("Source").Text.ToString)
                    System.Threading.Thread.Sleep(500)
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 2 Then
                Me.btnLedgerPrint.Visible = True
            ElseIf e.Tab.Index = 0 Then
                Me.btnLedgerPrint.Visible = False
            ElseIf e.Tab.Index = 1 Then
                Me.btnLedgerPrint.Visible = False
                If Me.BackgroundWorker2.IsBusy Then Exit Sub
                BackgroundWorker2.RunWorkerAsync()
                Do While BackgroundWorker2.IsBusy
                    Application.DoEvents()
                Loop
                FillDropDown(Me.ComboBox1, "Select main_sub_sub_id, sub_sub_title, sub_sub_code from tblCoaMainSubSub")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub DetailLederToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DetailLederToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            If chkMultiCurrency.Checked = True Then

                Dim fromDate As String = Me.DateTimePicker1.Value.Date '.Year & "." & Me.DateTimePicker1.Value.Month & "." & Me.DateTimePicker1.Value.Day
                Dim ToDate As String = Me.DateTimePicker2.Value.Date  '.Year & "." & Me.DateTimePicker2.Value.Month & "." & Me.DateTimePicker2.Value.Day
                AddRptParam("@FromDate", fromDate)
                AddRptParam("@ToDate", ToDate)
                AddRptParam("@Post", IIf(Me.chkUnPostedVouchers.Checked = True, 1, 0))
                AddRptParam("@CostCenterId", IIf(Me.cmbCostCenter.SelectedIndex > 0, Me.cmbCostCenter.SelectedValue, 0))
                AddRptParam("BaseCurrency", Me.cmbCurrency.SelectedText)
                'ShowReport("Ledger", IIf(Me.cmbAccount.ActiveRow.Cells(0).Value > 0, "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value & IIf(Me.cmbCostCenter.SelectedIndex > 0, " and  {SP_Rpt_Ledger.CostCenterID} = " & Me.cmbCostCenter.SelectedValue, ""), IIf(Me.cmbCostCenter.SelectedIndex > 0, " {SP_Rpt_Ledger.CostCenterID} = " & Me.cmbCostCenter.SelectedValue, "Nothing")), , , , , , , , , , Me.cmbAccount.ActiveRow.Cells("Email").Value.ToString, , "Ledger", Me.cmbAccount.ActiveRow.Cells("detail_title").Text.Replace("'", "''"))
                ShowReport("LedgerMultiCurrency", "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value & "", , , , , , , , , , Me.cmbAccount.ActiveRow.Cells("Email").Value.ToString, , "Ledger", Me.cmbAccount.ActiveRow.Cells("detail_title").Text.Replace("'", "''"))

            Else
                GetCrystalReportRights()
                Dim fromDate As String = Me.DateTimePicker1.Value.Date '.Year & "." & Me.DateTimePicker1.Value.Month & "." & Me.DateTimePicker1.Value.Day
                Dim ToDate As String = Me.DateTimePicker2.Value.Date  '.Year & "." & Me.DateTimePicker2.Value.Month & "." & Me.DateTimePicker2.Value.Day
                AddRptParam("@FromDate", fromDate)
                AddRptParam("@ToDate", ToDate)
                AddRptParam("@Post", IIf(Me.chkUnPostedVouchers.Checked = True, 1, 0))
                AddRptParam("@CostCenterId", IIf(Me.cmbCostCenter.SelectedIndex > 0, Me.cmbCostCenter.SelectedValue, 0))
                AddRptParam("@CurrencySymbol", Me.cmbCurrency.Text)
                AddRptParam("@CurrencyId", IIf(Me.chkShowSelectCurrency.Checked = True, Me.cmbCurrency.SelectedValue, 0))
                'ShowReport("Ledger", IIf(Me.cmbAccount.ActiveRow.Cells(0).Value > 0, "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value & IIf(Me.cmbCostCenter.SelectedIndex > 0, " and  {SP_Rpt_Ledger.CostCenterID} = " & Me.cmbCostCenter.SelectedValue, ""), IIf(Me.cmbCostCenter.SelectedIndex > 0, " {SP_Rpt_Ledger.CostCenterID} = " & Me.cmbCostCenter.SelectedValue, "Nothing")), , , , , , , , , , Me.cmbAccount.ActiveRow.Cells("Email").Value.ToString, , "Ledger", Me.cmbAccount.ActiveRow.Cells("detail_title").Text.Replace("'", "''"))
                ShowReport("Ledger", "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value & "", , , , , , , , , , Me.cmbAccount.ActiveRow.Cells("Email").Value.ToString, , "Ledger", Me.cmbAccount.ActiveRow.Cells("detail_title").Text.Replace("'", "''"))

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SummaryLedgerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SummaryLedgerToolStripMenuItem.Click
        Try
            GetCrystalReportRights()
            Dim fromDate As String = Me.DateTimePicker1.Value.Date '.Year & "." & Me.DateTimePicker1.Value.Month & "." & Me.DateTimePicker1.Value.Day
            Dim ToDate As String = Me.DateTimePicker2.Value.Date  '.Year & "." & Me.DateTimePicker2.Value.Month & "." & Me.DateTimePicker2.Value.Day
            AddRptParam("@FromDate", fromDate)
            AddRptParam("@ToDate", ToDate)
            AddRptParam("@Post", IIf(Me.chkUnPostedVouchers.Checked = True, 1, 0))
            AddRptParam("@CostCenterId", IIf(Me.cmbCostCenter.SelectedIndex > 0, Me.cmbCostCenter.SelectedValue, 0))
            AddRptParam("@CurrencyId", IIf(Me.chkShowSelectCurrency.Checked = True, Me.cmbCurrency.SelectedValue, 0))
            'ShowReport("LedgerSummary", IIf(Me.cmbAccount.ActiveRow.Cells(0).Value > 0, "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value & IIf(Me.cmbCostCenter.SelectedIndex > 0, " and  {SP_Rpt_Ledger.CostCenterID} = " & Me.cmbCostCenter.SelectedValue, ""), IIf(Me.cmbCostCenter.SelectedIndex > 0, " {SP_Rpt_Ledger.CostCenterID} = " & Me.cmbCostCenter.SelectedValue, "Nothing")), , , , , , , , , , Me.cmbAccount.ActiveRow.Cells("Email").Value.ToString, , "Ledger Summary", Me.cmbAccount.ActiveRow.Cells("detail_title").Text.Replace("'", "''"))
            ShowReport("LedgerSummary", "{SP_Rpt_Ledger.coa_detail_id}=" & Me.cmbAccount.ActiveRow.Cells(0).Value & "", , , , , , , , , , Me.cmbAccount.ActiveRow.Cells("Email").Value.ToString, , "Ledger", Me.cmbAccount.ActiveRow.Cells("detail_title").Text.Replace("'", "''"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdbDetail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            If Tracking = False Then
                GetData()
            Else
                GetMultiCostCenterRecord(CoaDetailId, PropertyType, Costid)
                Tracking = False
            End If

        Catch ex As Exception
            'msg_Error("Error occurred while geting data: " & ex.Message)
            'ShowErrorMessage("Error occurred while geting data: " & ex.Message)
            'Throw New Exception("Error occurred while geting data: " & ex.Message, ex.InnerException)
        End Try
    End Sub

    Private Sub lnkRefresh_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRefresh.LinkClicked
        Try
            Dim id As Integer = 0
            Dim Str As String = String.Empty
            id = Me.cmbAccount.SelectedRow.Cells(0).Value
            If LoginGroup = "Administrator" Then
                Str = "SELECT TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City, isnull(Active,0) as Active,dbo.vwCOADetail.Contact_Email as Email FROM dbo.vwCOADetail WHERE     (coa_detail_id > 0)  " & IIf(LoginGroup = "Administrator", "", " and Isnull(AccessLevel,'Everyone')='Everyone'") & " order by detail_title"
            ElseIf GetMappedUserId() <= 0 Then
                Str = "SELECT TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City, isnull(Active,0) as Active,dbo.vwCOADetail.Contact_Email as Email FROM dbo.vwCOADetail WHERE     (coa_detail_id > 0)  " & IIf(LoginGroup = "Administrator", "", " and Isnull(AccessLevel,'Everyone')='Everyone'") & " order by detail_title"
            Else
                Str = "SELECT TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City, TerritoryName as Territory, isnull(Active,0) as Active,dbo.vwCOADetail.Contact_Email as Email from vwCOADetail" _
                                    & " where coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & ")" _
                                    & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]=" & LoginGroupId & ") " _
                                    & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]=" & LoginGroupId & ")" _
                                    & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]=" & LoginGroupId & ") " _
                                    & "  " & IIf(LoginGroup = "Administrator", "", " and Isnull(AccessLevel,'Everyone')='Everyone'") & "   And (coa_detail_id > 0)  order by detail_title"
            End If
            FillUltraDropDown(cmbAccount, Str)
            If Me.cmbAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("Active").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("Email").Hidden = True
            End If
            For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.cmbAccount.Rows
                If row.Index > 0 Then
                    If row.Cells("Active").Value = False Then
                        row.Appearance.BackColor = Color.Red
                    End If
                End If
            Next
            Me.cmbAccount.DisplayLayout.Bands(0).Columns("Active").Hidden = True
            Me.cmbAccount.SelectedRow.Cells(0).Value = id

            If Me.Text = "Ledger Report" Then
                Me.pnlCost.Visible = True
                id = Me.cmbCostCenter.SelectedValue
                FillCombo("CostCenter")
                FillCombo("Currency")
                Me.cmbCostCenter.SelectedValue = id

                id = Me.cmbCostCenterHead.SelectedValue
                Str = "select distinct CostCenterGroup, CostCenterGroup from tbldefCostCenter where CostCenterGroup is Not null"
                FillDropDown(Me.cmbCostCenterHead, Str, True)
                Me.cmbCostCenterHead.SelectedValue = id
            Else
                Me.pnlCost.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        Try
            Me.ToolStripProgressBar1.Value = e.ProgressPercentage
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccount.Click
        Try
            Me.cmbAccount.Text = String.Empty
            If Me.cmbAccount.ActiveRow Is Nothing Then
                Exit Sub
            End If
            If Me.cmbAccount.IsItemInList = False Then Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdLedger_LoadingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdLedger.LoadingRow
        Try
            If Me.grdLedger.RowCount = 0 Then Exit Sub
            Dim RowFormat As New Janus.Windows.GridEX.GridEXFormatStyle
            If e.Row.Cells("Post").Value = False Then
                RowFormat.BackColor = Color.LightYellow
                e.Row.RowStyle = RowFormat
            Else
                RowFormat.BackColor = Color.White
                e.Row.RowStyle = RowFormat
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Tsk:2363 Added DoWork Event For Return List Collection
    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            _COAList = New COADAL().GetCOAList
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Tsk:2363 Added DoWork Completed Event 
    Private Sub BackgroundWorker2_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        Try

            If _COAList.Count > 0 Then
                For Each coa As COABE In _COAList
                    coa.detail_title = coa.detail_code & "-" & coa.detail_title
                Next
            End If
            Me.lstAccounts.ListItem.DataSource = _COAList
            Me.lstAccounts.ListItem.ValueMember = "coa_detail_id"
            Me.lstAccounts.ListItem.DisplayMember = "detail_title"


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Task:2363 Added Function Filter List By Sub Sub Id
    Public Function FilterCOA(ByVal COA As COABE) As Boolean
        Try

            If Not Me.ComboBox1.SelectedIndex = -1 Then
                If Me.ComboBox1.SelectedIndex > 0 Then
                    If COA.main_sub_sub_id = Me.ComboBox1.SelectedValue Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''Task:2363 Added Selected Index Change Event For Filter List By Sub Sub Id
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            If isFormLoaded = False Then Exit Sub
            If Me.ComboBox1.SelectedIndex = -1 Then Exit Sub
            If Me.ComboBox1.SelectedIndex > 0 Then
                Dim FilteredCOA As List(Of COABE) = _COAList.FindAll(AddressOf FilterCOA)
                Me.lstAccounts.ListItem.DataSource = FilteredCOA
                Me.lstAccounts.ListItem.ValueMember = "coa_detail_id"
                Me.lstAccounts.ListItem.DisplayMember = "detail_title"
            Else
                Me.lstAccounts.ListItem.DataSource = _COAList
                Me.lstAccounts.ListItem.ValueMember = "coa_detail_id"
                Me.lstAccounts.ListItem.DisplayMember = "detail_title"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Task:2363 Added Event For Selected Items Add To Other ListBox
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If Me.lstAccounts.ListItem.SelectedItems.Count = 0 Then Exit Sub
            For Each obj As Object In Me.lstAccounts.ListItem.SelectedItems
                If TypeOf obj Is COABE Then
                    Dim _coa As COABE = CType(obj, COABE)
                    If _coa IsNot Nothing Then
                        If Not _AddCOA.Contains(_coa) Then
                            _AddCOA.Add(_coa)
                        End If
                    End If
                End If
            Next
            Me.lstAddedAccounts.ListItem.DataSource = Nothing
            Me.lstAddedAccounts.ListItem.DataSource = _AddCOA
            Me.lstAddedAccounts.ListItem.ValueMember = "coa_detail_id"
            Me.lstAddedAccounts.ListItem.DisplayMember = "detail_title"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Task:2363 Added Event For Selected Items Remove On ListBox
    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Try
            If Me.lstAddedAccounts.ListItem.SelectedItems.Count = 0 Then Exit Sub
            For Each obj As Object In Me.lstAddedAccounts.ListItem.SelectedItems
                If TypeOf obj Is COABE Then
                    Dim _coa As COABE = CType(obj, COABE)
                    If _coa IsNot Nothing Then
                        'If Not _AddCOA.Contains(_coa) Then
                        _AddCOA.Remove(_coa)
                        'End If
                    End If
                End If
            Next
            Me.lstAddedAccounts.ListItem.DataSource = Nothing
            Me.lstAddedAccounts.ListItem.DataSource = _AddCOA
            Me.lstAddedAccounts.ListItem.ValueMember = "coa_detail_id"
            Me.lstAddedAccounts.ListItem.DisplayMember = "detail_title"

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Tsk:2363 Added Event For Generate Report
    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try

            Dim Ids As String = String.Empty
            For Each obj As Object In Me.lstAddedAccounts.ListItem.Items
                If TypeOf obj Is COABE Then
                    Dim COA As COABE = CType(obj, COABE)
                    If Ids.Length > 0 Then
                        Ids += "," & COA.coa_detail_id
                    Else
                        Ids = COA.coa_detail_id
                    End If
                End If
            Next
            If Ids.Length > 0 Then
                Dim fromDate As String = Me.dtpFrom.Value.Date '.Year & "." & Me.DateTimePicker1.Value.Month & "." & Me.DateTimePicker1.Value.Day
                Dim ToDate As String = Me.dtpTo.Value.Date  '.Year & "." & Me.DateTimePicker2.Value.Month & "." & Me.DateTimePicker2.Value.Day
                AddRptParam("@FromDate", fromDate)
                AddRptParam("@ToDate", ToDate)
                AddRptParam("@Post", IIf(Me.chkIncUnPosted.Checked = True, 1, 0))
                'AddRptParam("@CostCenterId", IIf(Me.cmbCostCenter.SelectedIndex > 0, 1, 0))
                AddRptParam("@CostCenterId", IIf(Me.cmbCurrCostCentre.SelectedIndex = -1, 0, Me.cmbCurrCostCentre.SelectedValue))

                If Me.rbtIndividual.Checked = True Then
                    ShowReport("" & IIf(Me.rbtDetail.Checked = True, "LedgerIndividual", "LedgerSummaryIndividual") & "", "{SP_Rpt_Ledger.coa_detail_id} in [" & Ids & "]", , , IIf(Me.rbtView.Checked = True, False, True), , , , , , , , , "Ledger Summary", Me.cmbAccount.ActiveRow.Cells("detail_title").Text.Replace("'", "''"))
                Else
                    ShowReport("" & IIf(Me.rbtDetail.Checked = True, "LedgerContinues", "LedgerSummaryContinues") & "", "{SP_Rpt_Ledger.coa_detail_id} in [" & Ids & "]", , , IIf(Me.rbtView.Checked = True, False, True), , , , , , , , , "Ledger Summary", Me.cmbAccount.ActiveRow.Cells("detail_title").Text.Replace("'", "''"))
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Task:2363 Search Account List
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo1.Click, btnGo2.Click
        Try
            If Me.lstAccounts.ListItem.DataSource Is Nothing Then Exit Sub 'Check Datasource is nothing then exit method
            Dim SearchButton As Button = CType(sender, Button) 'Set Casting Button Object
            ' Set Value
            Dim MySelectedCOA As New List(Of COABE)
            If SearchButton.Name = Me.btnGo1.Name Then
                _SearchText = Me.TextBox1.Text
                MySelectedCOA = CType(Me.lstAccounts.ListItem.DataSource, List(Of COABE)) 'Set Casting List of Object 
            ElseIf SearchButton.Name = Me.btnGo2.Name Then
                If Me.lstAddedAccounts.ListItem.DataSource Is Nothing Then Exit Sub
                _SearchText = Me.TextBox2.Text
                MySelectedCOA = CType(Me.lstAddedAccounts.ListItem.DataSource, List(Of COABE)) 'Set Casting List of Object 
            End If
            Dim MyCOA As New List(Of COABE) 'Create Object 
            MyCOA = MySelectedCOA.FindAll(AddressOf FilterCOAByAcTitle) 'Filter Collection
            If MyCOA.Count > 0 Then  'Check Collection
                For Each COA As COABE In MyCOA 'Start Loop With Check Value
                    If SearchButton.Name = Me.btnGo1.Name Then
                        Me.lstAccounts.ListItem.SelectedValue = COA.coa_detail_id 'Set Value on Selected Value
                    ElseIf SearchButton.Name = Me.btnGo2.Name Then
                        Me.lstAddedAccounts.ListItem.SelectedValue = COA.coa_detail_id 'Set Value on Selected Value
                    End If
                Next 'Repeat Loop
            End If 'End Check Collection
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Task:2363 Filter COA List
    Public Function FilterCOAByAcTitle(ByVal COA As COABE) As Boolean
        Try
            If COA.detail_title.Substring(17).StartsWith(_SearchText) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''''''''''''''''''''''''''''''''''''''''''''' End Task:2363 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub btnOpenVoucher_Click(sender As Object, e As EventArgs) Handles btnOpenVoucher.Click
        Try
            frmMain.LoadControl("frmVoucher")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker3_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Try
            GetDataPayables()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker4_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker4.DoWork
        Try
            GetDataReceiveables()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdLedger.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdLedger.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdLedger.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & cmbAccount.Text & vbCrLf & "Ledger" & vbCrLf & "Date From:" & Me.DateTimePicker1.Value & " Date To: " & Me.DateTimePicker2.Value & ""
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
