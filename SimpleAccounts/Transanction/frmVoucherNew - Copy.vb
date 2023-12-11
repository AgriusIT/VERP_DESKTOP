''16-Dec-2013 R933   Imran Ali           Slow working save/update in transaction forms
''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''29-Jan-2014 TASK:2398           Imran Ali        Update, Delete Problem in Cash Management
''30-Jan-2014   TASK:2400 Imran Ali  Attach Multi Files In Voucher Entry
''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
''27-Feb-2014 Task:2443   Imran Ali  7-cheque no. on voucher history window
''10-Mar-2014  Task:2484  Imran Ali  Load History On Voucher Take Too Time
''01-Jul-2014 TASK:2707 Imran Ali AvgRate Adjustment Voucher In Adjustment Average Rate
''16-Jul-2014 TASK:2745 Imran Ali Cheque Comments On Ledger (Ravi)
''13-Aug-2014 TASK:2780 Imran Ali Add new column CMFA Doc on Voucher Entry (Ravi)
''04-Sep-2014 Task:2826 Imran Ali Checked Status Option on  Voucher
''23-Sep-2014 Task:2854 Imran Ali Attachment Count In History And Preview On Payment/Receipt/Voucher
'26-06-2015 Task#201506026 Ali Ansari to block exceed payments
'06-07-2015 Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
'05-Aug-2015 Task#05082015 Ahmad Sharif Company wise serial no, based on configuration
''27-10-15 TASKM2710151 Imran Ali: Create Duplication Voucher On Update Mode.
' 12-11-2015 TASK42 Muhammad Ameen: 64 bit bar-code printing is not working (Item, voucher)
''16-12-2015 TASKTFS147 Muhammad Ameen: Multi Selected Voucher Print Execption
''12-05-2016 TASK-407 Muhammad Ameen : Dollar Account

Imports System.Data.Sql
Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports System.Collections.Generic
Imports Infragistics.Win.FormattedLinkLabel
Imports System.Collections
Imports Neodynamic.SDK.Barcode

Public Class frmVoucherNew
    Private Enum EnumGrid
        Head
        Account
        Description
        CostCenterID
        Cheque_No
        Cheque_Date
        ''TASK-407
        CurrencyId
        CurrencyDr
        CurrencyCr
        CurrencyRate
        BaseCurrencyId
        BaseCurrencyRate
        ''END TASK-407
        Debit
        Credit
        AccountID
        Type
        Delete
    End Enum
    Enum EnumGridMaster
        VoucherType
        VoucherNo
        VoucherDate
        ChequeNo
        ChequeDate
        Voucher_Id
        Reference
        OtherVoucher
        Amount
        Form_Caption
        Post
        Status
        Checked
        AccessKey
        PrintStatus
        Attachment
        CMFADoc 'Task:2780 Added index 
        Selector
        Detail
    End Enum
    Dim Mode As String = "Normal"
    Dim IsFormLoaded As Boolean = False
    Dim blnEditMode As Boolean = False
    Dim Row_Index As Integer = 0
    Dim Email As Email
    Dim ChangeVoucherOnUpdate As Boolean = True
    Dim GetMethod As String
    Dim GetVoucherId As Integer = 0
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim crpt As New ReportDocument
    Dim setVoucherNo As String = String.Empty
    Dim setVoucherType As String = String.Empty
    Dim setEditMode As Boolean = False
    Dim Total_Amount As Double = 0D
    Dim Previouse_Amount As Double = 0D
    Dim PrintLog As PrintLogBE
    Dim AccountHeadReadOnly As Boolean = False
    Dim flgCompanyRights As Boolean = False
    Dim EnableChequeDetail As Boolean = False

    'Marked Against Task#2015060001 Ali Ansari
    'Dim arrfile As String
    'Marked Against Task#2015060001 Ali Ansari
    'Altered Against Task#2015060001 Ali Ansari
    ' Convert string into List of string for making proper count
    Dim arrFile As List(Of String)
    'Altered Against Task#2015060001 Ali Ansari
    Dim CurrentVoucherNo As String = String.Empty
    Dim BaseCurrencyId As Integer
    Dim BaseCurrencyName As String = String.Empty

    Private Sub frmVoucherNew_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
        Try
            If e.KeyCode = Keys.F4 Then
                If BtnSave.Enabled = True Then
                    SaveToolStripButton_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                If Me.grd.GetRows.Length > 0 Then
                    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                End If
                NewToolStripButton_Click(BtnNew, Nothing)
                Exit Sub
            End If

            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                BtnPrint_ButtonClick(BtnPrint, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.F5 Then
                BtnRefresh_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Insert Then
                btnAdd_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.U AndAlso e.Alt Then
                If Me.BtnSave.Text = "&Update" Then
                    If Me.BtnSave.Enabled = False Then
                        RemoveHandler Me.BtnSave.Click, AddressOf Me.SaveToolStripButton_Click
                    End If
                End If
            End If
            If e.KeyCode = Keys.D AndAlso e.Alt Then
                If Me.BtnSave.Text = "&Delete" Then
                    If Me.BtnDelete.Enabled = False Then
                        RemoveHandler Me.BtnDelete.Click, AddressOf Me.DeleteToolStripButton_Click
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    ''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
    Private Sub txtDebit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDebit.KeyPress
        Try
            If (Char.IsDigit(e.KeyChar) Or Keys.Back = AscW(e.KeyChar) Or e.KeyChar.Equals("."c)) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtCredit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCredit.KeyPress
        Try
            If (Char.IsDigit(e.KeyChar) Or Keys.Back = AscW(e.KeyChar) Or e.KeyChar.Equals("."c)) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2491
    Private Sub frmVoucherNew_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            If Not IsDBNull(getConfigValueByType("AccountHeadReadOnly")) Or Not getConfigValueByType("AccountHeadReadOnly").ToString = "Error" Then
                AccountHeadReadOnly = Convert.ToBoolean(getConfigValueByType("AccountHeadReadOnly"))
            End If
            If Not getConfigValueByType("EnableChequeDetailOnVoucherEntry").ToString = "Error" Then
                EnableChequeDetail = getConfigValueByType("EnableChequeDetailOnVoucherEntry")
            End If



            Me.Cursor = Cursors.WaitCursor

            'Task#05082015 fill combo box with company names
            FillCombo("Company")
            Me.cmbCurrency.Enabled = True
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            'End Task#05082015

            FillCombo("VType")
            FillCombo("Head")
            FillCombo("Account")
            FillCombo("CMFA")
            FillCombo(Me.cmbCostCenter.Name)


            RefreshControls()
            'FillCombo("Currency")
            'DisplayRecord() R933 Commented History Data
            'DisplayDetail(-1)
            IsFormLoaded = True

            Get_All(frmMain.Tags)

            ' Selecting base currency by default
            Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub Voucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseKey = "KD83KNYYMA6XA6E7HNEL7QEUSGXGLKPQVCH7YVMEPXD7BG5FUCPA"
            Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseOwner = "LumenSoft Technologies-Standard Edition-OEM Developer License"
            BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub
    Private Sub FillCombo(ByVal strCondition As String)
        Dim str As String = String.Empty
        If strCondition = "VType" Then
            str = "Select Voucher_Type_ID, Voucher_Type from tblDefVoucherType"
            FillDropDown(cmbVoucherType, str)
            FillDropDown(Me.cmbVType, str)
        ElseIf strCondition = "Head" Then
            str = "Select main_sub_sub_id, sub_sub_title from tblCOAMainSubSub " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId & "", "") & " order by sub_sub_title "
            FillDropDown(cmbACHead, str)
            If AccountHeadReadOnly = True Then
                Me.cmbACHead.DropDownStyle = ComboBoxStyle.DropDownList
            Else
                Me.cmbACHead.DropDownStyle = ComboBoxStyle.DropDown
            End If
        ElseIf strCondition = "Account" Then
            'str = "     SELECT dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, dbo.vwCOADetail.detail_code, dbo.vwCOADetail.account_type, " & _
            '            "dbo.vwCOADetail.sub_sub_title, dbo.vwCOADetail.sub_title, dbo.vwCOADetail.main_title, dbo.vwCOADetail.main_type, dbo.tblListCity.CityName " & _
            '            "FROM         dbo.tblListTerritory LEFT OUTER JOIN " & _
            '            "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId RIGHT OUTER JOIN " & _
            '            "dbo.tblCustomer ON dbo.tblListTerritory.TerritoryId = dbo.tblCustomer.Territory RIGHT OUTER JOIN " & _
            '            "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '            "WHERE(dbo.vwCOADetail.coa_detail_id > 0) "
            str = "SELECT dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, dbo.vwCOADetail.detail_code, dbo.vwCOADetail.account_type, " & _
                      "dbo.vwCOADetail.sub_sub_title, dbo.vwCOADetail.sub_title, dbo.vwCOADetail.main_title, dbo.vwCOADetail.main_type, dbo.tblListCity.CityName " & _
                      "FROM dbo.tblListTerritory LEFT OUTER JOIN " & _
                      "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId RIGHT OUTER JOIN " & _
                      "dbo.tblCustomer ON dbo.tblListTerritory.TerritoryId = dbo.tblCustomer.Territory RIGHT OUTER JOIN " & _
                      "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                      "WHERE(dbo.vwCOADetail.coa_detail_id > 0) "
            If flgCompanyRights = True Then
                str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            End If
            If blnEditMode = False Then
                str += " AND vwCOADetail.Active=1 "
            Else
                str += " AND vwCOADetail.Active in(0,1,NULL) "
            End If
            str += " ORDER BY dbo.vwCOADetail.detail_title"
            FillUltraDropDown(cmbAccount, str)
            Me.cmbAccount.Rows(0).Activate()

        ElseIf strCondition = "SearchAccount" Then
            str = "     SELECT dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, dbo.vwCOADetail.detail_code, dbo.vwCOADetail.account_type, " & _
                        "dbo.vwCOADetail.sub_sub_title, dbo.vwCOADetail.sub_title, dbo.vwCOADetail.main_title, dbo.vwCOADetail.main_type, dbo.tblListCity.CityName " & _
                        "FROM  dbo.tblListTerritory LEFT OUTER JOIN " & _
                        "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId RIGHT OUTER JOIN " & _
                        "dbo.tblCustomer ON dbo.tblListTerritory.TerritoryId = dbo.tblCustomer.Territory RIGHT OUTER JOIN " & _
                        "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                        "WHERE(dbo.vwCOADetail.coa_detail_id > 0) "
            If flgCompanyRights = True Then
                str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            End If
            If Not Me.cmbSearchAccountType.SelectedIndex = 0 Then
                str += " and vwCOADetail.Account_Type=N'" & Me.cmbSearchAccountType.Text & "'"
            End If
            FillUltraDropDown(Me.cmbAccountTitle, str)
            If Me.cmbAccountTitle.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbAccountTitle.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbAccountTitle.DisplayLayout.Bands(0).Columns(4).Hidden = True
                Me.cmbAccountTitle.DisplayLayout.Bands(0).Columns(5).Hidden = True
                Me.cmbAccountTitle.DisplayLayout.Bands(0).Columns(6).Hidden = True
                Me.cmbAccountTitle.DisplayLayout.Bands(0).Columns(7).Hidden = True
                Me.cmbAccountTitle.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Account Title"
                Me.cmbAccountTitle.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Code"
                Me.cmbAccountTitle.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Account Type"
                Me.cmbAccountTitle.DisplayLayout.Bands(0).Columns(8).Header.Caption = "City"
            End If
            Me.cmbAccountTitle.Rows(0).Activate()
        ElseIf strCondition = "ACFilter" Then
            str = "     SELECT     TOP 100 PERCENT dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, dbo.vwCOADetail.detail_code, dbo.vwCOADetail.account_type, " & _
             "dbo.vwCOADetail.sub_sub_title, dbo.vwCOADetail.sub_title, dbo.vwCOADetail.main_title, dbo.vwCOADetail.main_type, dbo.tblListCity.CityName " & _
             "FROM         dbo.tblListTerritory LEFT OUTER JOIN " & _
             "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId RIGHT OUTER JOIN " & _
             "dbo.tblCustomer ON dbo.tblListTerritory.TerritoryId = dbo.tblCustomer.Territory RIGHT OUTER JOIN " & _
             "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
             " WHERE vwCOADetail.coa_detail_id > 0 "
            If flgCompanyRights = True Then
                str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            End If
            If Me.cmbACHead.SelectedIndex > 0 Then
                str += " AND (dbo.vwCOADetail.main_sub_sub_id =" & Me.cmbACHead.SelectedValue & ")"
            End If
            If blnEditMode = False Then
                str += " AND vwCOADetail.Active=1"
            End If
            str += " ORDER BY dbo.vwCOADetail.detail_title"
            FillUltraDropDown(cmbAccount, str)
            Me.cmbAccount.Rows(0).Activate()
        ElseIf strCondition = Me.cmbCostCenter.Name Then
            str = "select * from tbldefCostCenter WHERE "
            If blnEditMode = False Then
                str += " Active=1"
            Else
                str += " Active In (1,0,NULL)"
            End If
            str += " order by sortorder, name"
            FillDropDown(Me.cmbCostCenter, str)
        ElseIf strCondition = "SearchProject" Then
            str = "select * from tbldefCostCenter "
            FillDropDown(Me.cmbSearchProject, str)
        ElseIf strCondition = "Source" Then
            str = "Select Distinct Form_Name, Form_Caption From tblVoucher INNER JOIN tblForm  on tblForm.Form_Name = tblVoucher.Source ORDER By 1 "
            FillDropDown(Me.cmbSource, str)
            ''13-Aug-2014 TASK:2780 Imran Ali Add new column CMFA Doc on Voucher Entry (Ravi)
        ElseIf strCondition = "CMFA" Then
            FillDropDown(Me.cmbCMFADoc, "Select DocId, DocNo From CMFAMasterTable WHERE Approved=1 ORDER BY 1 DESC")
            'End TAsk:2780
            'Task#05082015 fill combo box with companies (Ahmad Sharif)
        ElseIf strCondition = "Company" Then
            str = String.Empty
            str = "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""
            FillDropDown(Me.cmbCompany, str, False)
            'End Task#05082015
        ElseIf strCondition = "Currency" Then ''TASK-407
            str = String.Empty
            str = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
            FillDropDown(Me.cmbCurrency, str, False)
            Me.cmbCurrency.SelectedValue = BaseCurrencyId
        End If
    End Sub
    Private Sub RefreshControls()
        Try
            blnEditMode = False
            If BtnSave.Text = "&Update" Then
                FillCombo("Account")
            End If
            'Clear Attached file records
            arrFile = New List(Of String)
            Me.btnAttachment.Text = "Attachment (" & arrFile.Count & ")"
            'Altered Against Task#2015060001 Ali Ansri
            'Array.Clear(arrFile, 0, arrFile.Length)
            Me.btnAttachment.Text = "Attachment"
            txtVoucherNo.Text = ""
            dtpVoucherDate.Value = Now
            Me.dtpVoucherDate.Enabled = True
            dtpChequeDate.Value = Now
            cmbVoucherType.SelectedIndex = 0
            txtTotalCredit.Text = 0
            txtTotalDebit.Text = 0
            txtChequeNo.Text = ""
            Me.txtDebit.Text = 0
            Me.txtCredit.Text = 0
            Me.txtDescription.Text = String.Empty
            Me.txtVoucherID.Text = 0
            Me.uitxtReference.Text = ""
            Me.cmbACHead.SelectedIndex = 0
            Me.cmbAccount.Rows(0).Activate()
            Me.txtCurrentBalance.Text = 0
            Me.BtnSave.Text = "&Save"
            'chkSearch.Checked = False
            Me.dtpfrom.Value = Date.Today.AddMonths(-1)
            Me.dtpto.Value = Date.Now
            Me.dtpfrom.Checked = False
            Me.dtpto.Checked = False
            'Me.cmbAccountTitle.Rows(0).Activate()
            Me.cmbSearchAccountType.SelectedIndex = 0
            Me.cmbVType.SelectedIndex = 0
            Me.TxtSearchVocherno.Text = String.Empty
            Me.TxtfromAmount.Text = String.Empty
            Me.TxtToAmount.Text = String.Empty
            Me.txtSearchChequeNo.Text = String.Empty
            Me.txtComments.Text = String.Empty
            'Me.cmbSearchProject.SelectedIndex = 0
            'Me.cmbSource.SelectedIndex = 0
            Me.SplitContainer1.Panel1Collapsed = True
            Me.cmbVoucherType.Enabled = True
            'cmbVTypeSearch.SelectedIndex = 0
            Me.chkOtherVoucher.Checked = False
            If Me.cmbCostCenter.Items.Count > 0 Then Me.cmbCostCenter.SelectedIndex = 0
            Me.DisplayRecord()
            DisplayDetail(-1)
            GetSecurityRights()
            Me.btnReplace.Visible = False
            Me.btnCancel.Visible = False
            Me.btnAdd.Visible = True
            Me.grd.Enabled = True
            FillCombo(Me.cmbCostCenter.Name)
            FillCombo("Currency")
            CostCenterGrdCombo()
            Me.lblPrintStatus.Text = String.Empty
            'Task#05082015 fill combo box with company names
            Me.cmbCompany.SelectedIndex = 0
            'End Task#05082015
            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnEdit.Visible = False
            Me.BtnPrint.Visible = False
            Me.BtnDelete.Visible = False
            btnUpdateTimes.Text = "No of update times"
            Me.btnUpdateTimes.Visible = False
            Me.cmbCurrency.Enabled = True
            Me.txtCurrencyRate.Enabled = False
            SaveAsTemplateToolStripMenuItem.Text = "Save as &template"
            SaveAsTemplateToolStripMenuItem.Tag = String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub DisplayVoucherTemplates()
        Try
            Me.lblTemplateProgress.Visible = True
            Dim str As String = String.Empty
            'str = "SELECT VT.voucher_type, V.voucher_no,  V.voucher_date, V.voucher_id, V.Reference, V.other_voucher, V.Cheque_No, ISNULL(Voucher_Amt.NetAmount,0) as Amount, form_caption as [Source], IsNull(V.Post,0) as Post, Case When IsNull(V.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, IsNull(V.Checked,0) as Checked,  tblForm.AccessKey, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], V.Attachment, IsNull(V.CMFADocId,0) as CMFADocId, ISNULL([No Of Attachment],0) as [No Of Attachment],V.USERNAME  AS 'User Name',companyDefTable.CompanyName as [Company Name] FROM  dbo.tblDefVoucherType VT INNER JOIN " _
            '                & "  dbo.tblVoucherTemplate V ON VT.voucher_type_id = V.voucher_type_id inner join dbo.tblForm ON V.source = dbo.tblForm.Form_Name inner join tblvoucherTemplatedetail Vd on vd.voucher_Id = v.voucher_id inner join vwcoadetail Vw on vw.coa_detail_Id = vd.coa_detail_id LEFT OUTER JOIN companyDefTable on V.location_id = companyDefTable.CompanyId  INNER JOIN(Select tblVoucherDetail.Voucher_Id, Sum(ISNULL(Credit_Amount,0)) as NetAmount From tblVoucherTemplateDetail as tblVoucherDetail Group By tblVoucherDetail.Voucher_Id) Voucher_Amt ON  Voucher_Amt.Voucher_Id = v.Voucher_Id LEFT OUTER JOIN(Select Count(*) as [No Of Attachment],DocId  From DocumentAttachment Group By DocId) Att On Att.DocId = V.Voucher_Id   LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = v.voucher_no WHERE 1=1" '' 

            str = " select voucher_id,Voucher_Type as VoucherType, voucher_no as [TemplateName], Reference, voucher_date as [CreationDate] from tblVoucherTemplate LEFT OUTER JOIN  tblDefVoucherType ON tblVoucherTemplate.voucher_type_id = tblDefVoucherType.voucher_type_id order by voucher_id desc "
            FillGridEx(grdTemplates, str, True)

            Me.grdTemplates.RootTable.Columns("voucher_id").Visible = False
            Me.grdTemplates.RootTable.Columns("CreationDate").FormatString = "dd/MMM/yyyy"
            Me.grdTemplates.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        Finally
            Me.lblTemplateProgress.Visible = False
        End Try
    End Sub
    Private Sub DisplayRecord(Optional ByVal Criteria As String = "")
        Try

            Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
            Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
            Dim str As String = String.Empty
            If Mode = "Normal" Then
                ''30-Jan-2014   TASK:2400 Imran Ali  Attach Multi Files In Voucher Entry
                'str = "SELECT DISTINCT " & IIf(Criteria = "All", "", "Top 50") & " VT.voucher_type, V.voucher_no,  V.voucher_date, V.voucher_id, V.Reference, V.other_voucher, ISNULL(Voucher_Amt.NetAmount,0) as Amount, form_caption as [Source], IsNull(V.Post,0) as Post, Case When IsNull(V.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, tblForm.AccessKey, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status] FROM  dbo.tblDefVoucherType VT INNER JOIN " _
                '        & "  dbo.tblVoucher V ON VT.voucher_type_id = V.voucher_type_id inner join dbo.tblForm ON V.source = dbo.tblForm.Form_Name inner join tblvoucherdetail Vd on vd.voucher_Id = v.voucher_id inner join vwcoadetail Vw on vw.coa_detail_Id = vd.coa_detail_id  LEFT OUTER JOIN(Select tblVoucherDetail.Voucher_Id, Sum(ISNULL(Credit_Amount,0)) as NetAmount From tblVoucherDetail Group By tblVoucherDetail.Voucher_Id) Voucher_Amt ON  Voucher_Amt.Voucher_Id = v.Voucher_Id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = v.voucher_no WHERE 1=1"
                'If flgCompanyRights = True Then
                '    str += " AND V.Location_Id=" & MyCompanyId
                'End If
                ''Task:2400 Added Column Attachment
                'str = "SELECT DISTINCT " & IIf(Criteria = "All", "", "Top 50") & " VT.voucher_type, V.voucher_no,  V.voucher_date, V.voucher_id, V.Reference, V.other_voucher, ISNULL(Voucher_Amt.NetAmount,0) as Amount, form_caption as [Source], IsNull(V.Post,0) as Post, Case When IsNull(V.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, tblForm.AccessKey, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], V.Attachment FROM  dbo.tblDefVoucherType VT INNER JOIN " _
                '     & "  dbo.tblVoucher V ON VT.voucher_type_id = V.voucher_type_id inner join dbo.tblForm ON V.source = dbo.tblForm.Form_Name inner join tblvoucherdetail Vd on vd.voucher_Id = v.voucher_id inner join vwcoadetail Vw on vw.coa_detail_Id = vd.coa_detail_id  LEFT OUTER JOIN(Select tblVoucherDetail.Voucher_Id, Sum(ISNULL(Credit_Amount,0)) as NetAmount From tblVoucherDetail Group By tblVoucherDetail.Voucher_Id) Voucher_Amt ON  Voucher_Amt.Voucher_Id = v.Voucher_Id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = v.voucher_no WHERE 1=1"
                'If flgCompanyRights = True Then
                '    str += " AND V.Location_Id=" & MyCompanyId
                'End If
                'End Task:2400 
                'Before against task:M25
                'Task:2443 Added Field Cheque_No In This Query 
                'str = "SELECT DISTINCT " & IIf(Criteria = "All", "", "Top 50") & " VT.voucher_type, V.voucher_no,  V.voucher_date, V.voucher_id, V.Reference, V.other_voucher, V.Cheque_No, ISNULL(Voucher_Amt.NetAmount,0) as Amount, form_caption as [Source], IsNull(V.Post,0) as Post, Case When IsNull(V.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, tblForm.AccessKey, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], V.Attachment FROM  dbo.tblDefVoucherType VT INNER JOIN " _
                '    & "  dbo.tblVoucher V ON VT.voucher_type_id = V.voucher_type_id inner join dbo.tblForm ON V.source = dbo.tblForm.Form_Name inner join tblvoucherdetail Vd on vd.voucher_Id = v.voucher_id inner join vwcoadetail Vw on vw.coa_detail_Id = vd.coa_detail_id  LEFT OUTER JOIN(Select tblVoucherDetail.Voucher_Id, Sum(ISNULL(Credit_Amount,0)) as NetAmount From tblVoucherDetail Group By tblVoucherDetail.Voucher_Id) Voucher_Amt ON  Voucher_Amt.Voucher_Id = v.Voucher_Id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = v.voucher_no WHERE 1=1"
                'Before against task:2707
                'Before agains task:2780
                'Task:2484 change Join In This Query
                'str = "SELECT DISTINCT " & IIf(Criteria = "All", "", "Top 50") & " VT.voucher_type, V.voucher_no,  V.voucher_date, V.voucher_id, V.Reference, V.other_voucher, V.Cheque_No, ISNULL(Voucher_Amt.NetAmount,0) as Amount, form_caption as [Source], IsNull(V.Post,0) as Post, Case When IsNull(V.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, tblForm.AccessKey, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], V.Attachment FROM  dbo.tblDefVoucherType VT INNER JOIN " _
                '               & "  dbo.tblVoucher V ON VT.voucher_type_id = V.voucher_type_id inner join dbo.tblForm ON V.source = dbo.tblForm.Form_Name inner join tblvoucherdetail Vd on vd.voucher_Id = v.voucher_id inner join vwcoadetail Vw on vw.coa_detail_Id = vd.coa_detail_id  INNER JOIN(Select tblVoucherDetail.Voucher_Id, Sum(ISNULL(Credit_Amount,0)) as NetAmount From tblVoucherDetail Group By tblVoucherDetail.Voucher_Id) Voucher_Amt ON  Voucher_Amt.Voucher_Id = v.Voucher_Id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = v.voucher_no WHERE 1=1"
                'End Task:2484
                'End Task:2443
                'Task:2780 Added Column CMFADocId
                'Before against task:2826
                'str = "SELECT DISTINCT " & IIf(Criteria = "All", "", "Top 50") & " VT.voucher_type, V.voucher_no,  V.voucher_date, V.voucher_id, V.Reference, V.other_voucher, V.Cheque_No, ISNULL(Voucher_Amt.NetAmount,0) as Amount, form_caption as [Source], IsNull(V.Post,0) as Post, Case When IsNull(V.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, tblForm.AccessKey, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], V.Attachment, IsNull(V.CMFADocId,0) as CMFADocId FROM  dbo.tblDefVoucherType VT INNER JOIN " _
                '              & "  dbo.tblVoucher V ON VT.voucher_type_id = V.voucher_type_id inner join dbo.tblForm ON V.source = dbo.tblForm.Form_Name inner join tblvoucherdetail Vd on vd.voucher_Id = v.voucher_id inner join vwcoadetail Vw on vw.coa_detail_Id = vd.coa_detail_id  INNER JOIN(Select tblVoucherDetail.Voucher_Id, Sum(ISNULL(Credit_Amount,0)) as NetAmount From tblVoucherDetail Group By tblVoucherDetail.Voucher_Id) Voucher_Amt ON  Voucher_Amt.Voucher_Id = v.Voucher_Id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = v.voucher_no WHERE 1=1"
                'End Task:2780
                'TAsk:2826 Added Field Checked
                'Before against task:2854
                'str = "SELECT DISTINCT " & IIf(Criteria = "All", "", "Top 50") & " VT.voucher_type, V.voucher_no,  V.voucher_date, V.voucher_id, V.Reference, V.other_voucher, V.Cheque_No, ISNULL(Voucher_Amt.NetAmount,0) as Amount, form_caption as [Source], IsNull(V.Post,0) as Post, Case When IsNull(V.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, IsNull(V.Checked,0) as Checked,  tblForm.AccessKey, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], V.Attachment, IsNull(V.CMFADocId,0) as CMFADocId FROM  dbo.tblDefVoucherType VT INNER JOIN " _
                '             & "  dbo.tblVoucher V ON VT.voucher_type_id = V.voucher_type_id inner join dbo.tblForm ON V.source = dbo.tblForm.Form_Name inner join tblvoucherdetail Vd on vd.voucher_Id = v.voucher_id inner join vwcoadetail Vw on vw.coa_detail_Id = vd.coa_detail_id  INNER JOIN(Select tblVoucherDetail.Voucher_Id, Sum(ISNULL(Credit_Amount,0)) as NetAmount From tblVoucherDetail Group By tblVoucherDetail.Voucher_Id) Voucher_Amt ON  Voucher_Amt.Voucher_Id = v.Voucher_Id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = v.voucher_no WHERE 1=1"
                'End Task:2826
                'Task:2854 Added Field No Of Attachment
                'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
                'str = "SELECT DISTINCT " & IIf(Criteria = "All", "", "Top 50") & " VT.voucher_type, V.voucher_no,  V.voucher_date, V.voucher_id, V.Reference, V.other_voucher, V.Cheque_No, ISNULL(Voucher_Amt.NetAmount,0) as Amount, form_caption as [Source], IsNull(V.Post,0) as Post, Case When IsNull(V.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, IsNull(V.Checked,0) as Checked,  tblForm.AccessKey, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], V.Attachment, IsNull(V.CMFADocId,0) as CMFADocId, ISNULL([No Of Attachment],0) as [No Of Attachment] FROM  dbo.tblDefVoucherType VT INNER JOIN " _
                '           & "  dbo.tblVoucher V ON VT.voucher_type_id = V.voucher_type_id inner join dbo.tblForm ON V.source = dbo.tblForm.Form_Name inner join tblvoucherdetail Vd on vd.voucher_Id = v.voucher_id inner join vwcoadetail Vw on vw.coa_detail_Id = vd.coa_detail_id  INNER JOIN(Select tblVoucherDetail.Voucher_Id, Sum(ISNULL(Credit_Amount,0)) as NetAmount From tblVoucherDetail Group By tblVoucherDetail.Voucher_Id) Voucher_Amt ON  Voucher_Amt.Voucher_Id = v.Voucher_Id LEFT OUTER JOIN(Select Count(*) as [No Of Attachment],DocId  From DocumentAttachment WHERE Source='frmVouchernew' Group By DocId) Att On Att.DocId = V.Voucher_Id   LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = v.voucher_no WHERE 1=1"
                'End Task:2854
                'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
                'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
                str = "SELECT DISTINCT " & IIf(Criteria = "All", "", "Top 50") & " VT.voucher_type, V.voucher_no,  V.voucher_date, V.voucher_id, V.Reference, V.other_voucher, V.Cheque_No, ISNULL(Voucher_Amt.NetAmount,0) as Amount, form_caption as [Source], IsNull(V.Post,0) as Post, Case When IsNull(V.Post,0)=1 then 'Posted' else 'UnPosted' end as Status, IsNull(V.Checked,0) as Checked,  tblForm.AccessKey, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], V.Attachment, IsNull(V.CMFADocId,0) as CMFADocId, ISNULL([No Of Attachment],0) as [No Of Attachment],V.USERNAME  AS 'User Name',companyDefTable.CompanyName as [Company Name] FROM  dbo.tblDefVoucherType VT INNER JOIN " _
                            & "  dbo.tblVoucher V ON VT.voucher_type_id = V.voucher_type_id inner join dbo.tblForm ON V.source = dbo.tblForm.Form_Name inner join tblvoucherdetail Vd on vd.voucher_Id = v.voucher_id inner join vwcoadetail Vw on vw.coa_detail_Id = vd.coa_detail_id LEFT OUTER JOIN companyDefTable on V.location_id = companyDefTable.CompanyId  INNER JOIN(Select tblVoucherDetail.Voucher_Id, Sum(ISNULL(Credit_Amount,0)) as NetAmount From tblVoucherDetail Group By tblVoucherDetail.Voucher_Id) Voucher_Amt ON  Voucher_Amt.Voucher_Id = v.Voucher_Id LEFT OUTER JOIN(Select Count(*) as [No Of Attachment],DocId  From DocumentAttachment Group By DocId) Att On Att.DocId = V.Voucher_Id   LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = v.voucher_no WHERE 1=1" '' 
                'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            End If
            If Len(Criteria.ToString.Trim) > 0 AndAlso Criteria = " 1=1 " Then
                str = str & " and " & Criteria
                str = str.Replace("Top 50", "")
            Else
                str = str & " and V.source='frmVoucher' "
            End If
            str = str & "  " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, v.Voucher_date, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            If IsFormLoaded = True Then
                If Me.dtpfrom.Checked = True Then
                    str += " and v.voucher_date >= Convert(Datetime, N'" & dtpfrom.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102)"
                End If
                If Me.dtpto.Checked = True Then
                    str += " and v.voucher_date <= Convert(Datetime, N'" & dtpto.Value.Date.ToString("yyyy-M-d 23:59:59") & "',102)"
                End If
                If Not Me.cmbSearchAccountType.SelectedIndex = 0 Then
                    str += " and vw.account_type = N'" & Me.cmbSearchAccountType.Text & "'"
                End If
                If Not Me.cmbAccountTitle.SelectedRow.Cells(0).Value = 0 Then
                    str += " and vd.coa_detail_id=" & Me.cmbAccountTitle.Value & ""
                End If
                If Not Me.cmbVType.SelectedIndex = 0 Then
                    str += " and v.voucher_type_id=" & Me.cmbVType.SelectedValue & ""
                End If
                If Not Me.TxtSearchVocherno.Text = String.Empty Then
                    str += " and v.voucher_no like '%" & Me.TxtSearchVocherno.Text & "%'"
                End If
                If Not Me.TxtfromAmount.Text = String.Empty Then
                    str += " and Voucher_Amt.NetAmount >=" & Val(Me.TxtfromAmount.Text) & ""
                End If
                If Not Me.TxtToAmount.Text = String.Empty Then
                    str += " and Voucher_Amt.NetAmount <=" & Val(Me.TxtToAmount.Text) & ""
                End If
                If Not Me.txtSearchChequeNo.Text = String.Empty Then
                    str += " and vd.Cheque_No Like '%" & Me.txtSearchChequeNo.Text & "%'"
                End If
                If Not Me.txtComments.Text = String.Empty Then
                    str += " and vd.Comments Like '%" & Me.txtComments.Text & "%'"
                End If
                If Not Me.cmbSearchProject.SelectedIndex = -1 Then
                    If Me.cmbSearchProject.SelectedIndex > 0 Then
                        str += " and vd.costCenterId=" & Me.cmbSearchProject.SelectedValue & ""
                    End If
                End If
                If Not Me.cmbSource.SelectedIndex = -1 Then
                    If Me.cmbSource.SelectedIndex > 0 Then
                        str += " and v.Source =N'" & Me.cmbSource.SelectedValue & "'"
                    End If
                End If
            End If
            str = str & " order by v.voucher_id desc "
            FillGridEx(grdSaved, str, False)
            'Task:2854 Setting Link Column type
            Me.grdSaved.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            'End Task:2854
            Me.grdSaved.RootTable.Columns("AccessKey").Visible = False

            Me.grdSaved.RootTable.Columns("Attachment").Visible = False ' TASK:2400 Imran Ali  Attach Multi Files In Voucher Entry
            Me.grdSaved.RootTable.Columns("CMFADocId").Visible = False 'Task:2780 Set Hidden Column

            Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdSaved.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'grdSaved.RootTable.Columns(5).Visible = False
            'grdSaved.RootTable.Columns("other_voucher").Visible = False
            'grdSaved.RootTable.Columns("Post").Visible = False
            'grdSaved.RootTable.Columns(0).Caption = "V Type"
            'grdSaved.RootTable.Columns(1).Caption = "Voucher No"
            'grdSaved.RootTable.Columns(2).Caption = "Date"
            'grdSaved.RootTable.Columns(3).Caption = "Cheque No."
            'grdSaved.RootTable.Columns(4).Caption = "Cheque Date"

            'grdSaved.RootTable.Columns(0).Width = 100
            'grdSaved.RootTable.Columns(1).Width = 100
            'grdSaved.RootTable.Columns(2).Width = 120
            'grdSaved.RootTable.Columns(3).Width = 180
            'grdSaved.RootTable.Columns(4).Width = 120
            'grdSaved.RootTable.Columns("Reference").Visible = True
            'grdSaved.RootTable.Columns("Source").Width = 130
            Me.grdSaved.RootTable.Columns("Voucher_Date").FormatString = str_DisplayDateFormat
            CtrlGrdBar2_Load(Nothing, Nothing)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function FormValidate() As Boolean
        Me.grd.UpdateData()
        If cmbVoucherType.SelectedIndex < 1 Then
            msg_Error("Please enter voucher type")
            cmbVoucherType.Focus() : FormValidate = False : Exit Function
        End If

        If Me.grd.GetTotal(Me.grd.RootTable.Columns("Debit"), Janus.Windows.GridEX.AggregateFunction.Sum) = 0 Or Me.grd.GetTotal(Me.grd.RootTable.Columns("Credit"), Janus.Windows.GridEX.AggregateFunction.Sum) = 0 Then 'Val(txtTotalCredit.Text) = 0 Or Val(txtTotalDebit.Text) = 0 Then
            msg_Error(str_ErrorNoRecordFound)
            cmbAccount.Focus() : FormValidate = False : Exit Function
        End If

        If Me.grd.GetTotal(Me.grd.RootTable.Columns("Debit"), Janus.Windows.GridEX.AggregateFunction.Sum) <> Me.grd.GetTotal(Me.grd.RootTable.Columns("Credit"), Janus.Windows.GridEX.AggregateFunction.Sum) Then 'Val(txtTotalCredit.Text) <> Val(txtTotalDebit.Text) Then
            msg_Error("Debit and credit must be equal")
            cmbAccount.Focus() : FormValidate = False : Exit Function
        End If

        If Me.cmbVoucherType.SelectedValue = 5 Then
            If Not Me.BackendValidation() AndAlso Not Me.txtChequeNo.Text = "" Then
                msg_Error("Cheque No already Exist")
                Me.txtChequeNo.Focus()
                Return False
            End If
        End If
        'BRV()
        Return True
    End Function
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Cursor = Cursors.WaitCursor
        Try
            If Validate_AddToGrid() Then
                AddItemToGrid()
                GetTotal()
                ClearDetailControls()
                cmbACHead.Focus()
                Me.cmbCurrency.Enabled = False

                ' R@!   11-Jun-2016     Dollor account
                'Me.grd.RootTable.Columns("CurrencyAmount").Caption = "" & Me.cmbCurrency.Text & " Amount"
            End If
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function Validate_AddToGrid() As Boolean

        'BRV Validation Comment by Imran 18-01-2012  
        'Reason Different Cost Center's Receiving From Customer
        '**************************************************
        'If Me.cmbVoucherType.Text = "BRV" Then
        '    If Me.grd.Rows.Count >= 2 Then
        '        MsgBox("Only one entry allwed for debit and one for credit", MsgBoxStyle.Critical)
        '        Me.grd.Focus() : Validate_AddToGrid = False : Exit Function
        '    End If
        'End If
        'If Val(Me.txtPaymentBeforeBalance.Text) < (Val(txtAmount.Text) + Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) Then
        ''''''''''''''''''''''''''''
        'Altered Against Task#201506026 Ali Ansari to block payments exceeding available balanace
        If IsAllowPayment() = False Then
            If cmbAccount.ActiveRow.Cells(3).Value.ToString = "Bank" Or cmbAccount.ActiveRow.Cells(3).Value.ToString = "Cash" And Val(txtCredit.Text) > 0 Then
                If Val(txtCurrentBalance.Text) < Val(txtCredit.Text) Then
                    ShowErrorMessage("Amount exceeds from available balance")
                    txtCredit.Focus() : Validate_AddToGrid = False : Exit Function
                End If
            End If
        End If
        'Altered Against Task#201506026 Ali Ansari to block payments exceeding available balanace


        ''''''''''''''''''''''''''''''
        If cmbAccount.ActiveRow.Cells(0).Value <= 0 Then
            ShowErrorMessage("Please select an Account")
            cmbAccount.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Val(txtDebit.Text) <= 0 And Val(txtCredit.Text) <= 0 Then
            ShowErrorMessage("Please enter value as debit or credit")
            txtDebit.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Val(txtDebit.Text) > 0 And Val(txtCredit.Text) > 0 Then
            ShowErrorMessage("You can only enter as Debit or Credit")
            txtCredit.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Me.cmbAccount.ActiveRow.Cells("Account_type").Value.ToString.Trim.ToUpper = "LC" Then
            If Me.cmbCostCenter.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select cost center.")
                Me.cmbCostCenter.Focus()
                Validate_AddToGrid = False : Exit Function
            End If
        End If
        'TASK-407
        If Val(txtCurrencyRate.Text) = 0 Then
            ShowErrorMessage("Currency rate value more than 0 is required")
            txtCurrencyRate.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        'END TASK-407

        Validate_AddToGrid = True
    End Function

    Private Sub AddItemToGrid()
        Try
            'grd.Rows.Add(cmbACHead.Text, cmbAccount.Text, txtDescription.Text, IIf(Me.cmbCostCenter.SelectedIndex > 0, Me.cmbCostCenter.Text.ToString(), ""), txtDebit.Text, txtCredit.Text, cmbAccount.ActiveRow.Cells(0).Value, Me.cmbCostCenter.SelectedValue)
            'grd.GetRows.Add(cmbACHead.Text, cmbAccount.Text, txtDescription.Text, IIf(Me.cmbCostCenter.SelectedIndex > 0, Me.cmbCostCenter.Text.ToString(), ""), txtDebit.Text, txtCredit.Text, cmbAccount.ActiveRow.Cells(0).Value, Me.cmbCostCenter.SelectedValue)
            ''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
            If CheckNumericValue(Me.txtDebit.Text, Me.txtDebit) = False Then
                Throw New Exception("Amount is not valid.")
            End If
            If CheckNumericValue(Me.txtCredit.Text, Me.txtCredit) = False Then
                Throw New Exception("Amount is not valid.")
            End If
            'End Task:2491

            Dim dtGrd As DataTable = CType(Me.grd.DataSource, DataTable)
            dtGrd.AcceptChanges()
            Dim dr As DataRow
            dr = dtGrd.NewRow
            dr.Item(EnumGrid.Head) = Me.cmbACHead.Text
            dr.Item(EnumGrid.Account) = Me.cmbAccount.Text
            dr.Item(EnumGrid.Description) = Me.txtDescription.Text
            dr.Item(EnumGrid.CostCenterID) = IIf(Me.cmbCostCenter.SelectedIndex > 0, Me.cmbCostCenter.SelectedValue, 0)
            dr.Item(EnumGrid.Cheque_No) = IIf(Me.txtChequeNo.Enabled = True, Me.txtChequeNo.Text, DBNull.Value)
            dr.Item(EnumGrid.Cheque_Date) = IIf(Me.dtpChequeDate.Enabled = True, Me.dtpChequeDate.Value, DBNull.Value)
            dr.Item(EnumGrid.CurrencyId) = Me.cmbCurrency.SelectedValue

            'R@!    11-Jun-2016
            'dr.Item(EnumGrid.CurrencyAmount) = IIf(Me.txtDebit.Text = 0, Val(Me.txtCredit.Text), Val(Me.txtDebit.Text))
            dr.Item(EnumGrid.CurrencyDr) = Val(Me.txtDebit.Text)
            dr.Item(EnumGrid.CurrencyCr) = Val(Me.txtCredit.Text)

            dr.Item(EnumGrid.CurrencyRate) = Val(Me.txtCurrencyRate.Text)
            Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
            If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
                dr.Item(EnumGrid.BaseCurrencyId) = Val(ConfigCurrencyVal)
                dr.Item(EnumGrid.BaseCurrencyRate) = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
            End If
            dr.Item(EnumGrid.Debit) = IIf(Me.txtDebit.Text = "", 0, Val(Me.txtDebit.Text) * Val(Me.txtCurrencyRate.Text))
            dr.Item(EnumGrid.Credit) = IIf(Me.txtCredit.Text = "", 0, Val(Me.txtCredit.Text) * Val(Me.txtCurrencyRate.Text))
            dr.Item(EnumGrid.AccountID) = Me.cmbAccount.ActiveRow.Cells(0).Value
            dtGrd.Rows.InsertAt(dr, 0)

            CostCenterGrdCombo()
            GetTotal()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetTotal()
        Dim i As Integer
        Dim dblTotalDebit As Double
        Dim dblTotalCredit As Double
        'For i = 0 To grd.Rows.Count - 1
        For i = 0 To grd.RowCount - 1
            'dblTotalDebit = dblTotalDebit + Val(grd.Rows(i).Cells(EnumGrid.Debit).Value)
            'dblTotalCredit = dblTotalCredit + Val(grd.Rows(i).Cells(EnumGrid.Credit).Value)
            dblTotalDebit = dblTotalDebit + Val(grd.GetRows(i).Cells(EnumGrid.Debit).Value.ToString)
            dblTotalCredit = dblTotalCredit + Val(grd.GetRows(i).Cells(EnumGrid.Credit).Value.ToString)
        Next
        txtTotalDebit.Text = dblTotalDebit
        txtTotalCredit.Text = dblTotalCredit

    End Sub
    Private Sub ClearDetailControls()
        Try
            'cmbACHead.SelectedIndex = 0
            cmbAccount.Rows(0).Activate()
            ' txtDescription.Text = ""
            txtDebit.Text = 0
            txtCredit.Text = 0
            txtCurrentBalance.Text = 0
            Me.txtChequeNo.Text = String.Empty
            Me.dtpChequeDate.Value = Now
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbACHead_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbACHead.SelectedIndexChanged
        'If cmbACHead.SelectedIndex > 0 Then
        FillCombo("ACFilter")
        'End If
    End Sub
    Private Function SaveTemplate() As Boolean
        If Me.chkPost.Visible = False Then
            Me.chkPost.Checked = False
        End If

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Dim objCommand As New OleDbCommand

        Dim i As Integer


        Me.grd.UpdateData()
        If Con.State = ConnectionState.Open Then Con.Close()

        Con.Open()
        objCommand.Connection = Con

        Dim trans As OleDbTransaction = Con.BeginTransaction


        Try
            objCommand.CommandType = CommandType.Text

            txtVoucherNo.Text = GetNextDocNo("Template", 3, "tblVoucherTemplate", "voucher_no")
            objCommand.Transaction = trans

            Dim intVoucherId As Integer = 0I

            If Val(SaveAsTemplateToolStripMenuItem.Tag.ToString) > 0 Then

                intVoucherId = Val(SaveAsTemplateToolStripMenuItem.Tag.ToString)

                objCommand.CommandText = "Update tblVoucherTemplate set location_id=" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", Me.cmbCompany.SelectedValue.ToString) & ", voucher_type_id=" & Me.cmbVoucherType.SelectedValue & ", Reference='" & Me.uitxtReference.Text & "' where voucher_id=" & intVoucherId
                objCommand.ExecuteNonQuery()
            Else

                objCommand.CommandText = "Insert into tblVoucherTemplate (location_id,finiancial_year_id,voucher_type_id,voucher_no,voucher_date, post,source,Reference,UserName, other_voucher, coa_detail_id, Posted_UserName, CMFADocId, Checked,CheckedByUser) values( " _
                                 & " " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", Me.cmbCompany.SelectedValue.ToString) & ",1, " & cmbVoucherType.SelectedValue & " ,N'" & txtVoucherNo.Text.Replace("'", "''") & "', N'" & Date.Now.ToString("yyyy-M-d h:mm:ss tt") & "'," & IIf(Me.chkPost.Checked = True, 1, 0) & ",'frmVoucher',N'" & Me.uitxtReference.Text.Replace("'", "''") & "',N'" & LoginUserName & "', " & IIf(Me.chkOtherVoucher.Checked, "1", "0") & ", " & 0 & ", " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", " NULL") & "," & IIf(Me.cmbCMFADoc.SelectedIndex > 0, Me.cmbCMFADoc.SelectedValue, "NULL") & ", " & IIf(Me.chkChecked.Checked = True, 1, 0) & "," & IIf(Me.chkChecked.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & ") Select @@Identity"
                intVoucherId = objCommand.ExecuteScalar
            End If

            objCommand.CommandText = "delete from tblVoucherTemplateDetail where voucher_id=" & intVoucherId
            objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""

            Dim strMultiChequeDetail As String = String.Empty

            For i = 0 To grd.RowCount - 1
                objCommand.CommandText = ""



                If grd.GetRows(i).Cells("Type").Value.ToString.Trim.ToUpper = "LC" Then
                    If Val(grd.GetRows(i).Cells("CostCenterId").Value.ToString) <= 0 Then
                        Throw New Exception("Please select cost center.")
                    End If
                End If

                objCommand.CommandText = "Insert into tblVoucherTemplateDetail (voucher_id, location_id,coa_detail_id, comments,debit_amount,credit_amount, CostCenterID, Cheque_No, Cheque_Date,ChequeDescription, CurrencyId, CurrencyRate, BaseCurrencyId, BaseCurrencyRate, Currency_Debit_Amount, Currency_Credit_Amount, Currency_Symbol) values( " _
                                   & " " & intVoucherId & ", " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(grd.GetRows(i).Cells(EnumGrid.AccountID).Value) & ",N'" & (grd.GetRows(i).Cells(EnumGrid.Description).Value.ToString.Replace("'", "''")) & "'," & Val(grd.GetRows(i).Cells(EnumGrid.Debit).Value) & ", " _
                                   & " " & Val(grd.GetRows(i).Cells(EnumGrid.Credit).Value) & ", " & grd.GetRows(i).Cells(EnumGrid.CostCenterID).Value & ", " & IIf(grd.GetRows(i).Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & grd.GetRows(i).Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(grd.GetRows(i).Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(grd.GetRows(i).Cells("Cheque_Date").Value), Now, grd.GetRows(i).Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & GetComments(grd.GetRows(i)).Replace("'", "''") & "', " & Val(grd.GetRow(i).Cells("CurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("currencyDr").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("CurrencyCr").Value.ToString) & ", '" & Me.cmbCurrency.Text & "')Select @@Identity"

                objCommand.ExecuteScalar()


            Next

            If strMultiChequeDetail.Length > 0 AndAlso strMultiChequeDetail.Length < 8000 Then
                objCommand.CommandText = ""
                objCommand.CommandText = "Update tblVoucherTemplate SET Cheque_No=N'" & strMultiChequeDetail.Replace("'", "''") & "' WHERE Voucher_Id=" & intVoucherId
                objCommand.ExecuteNonQuery()
            End If

            If arrFile.Count > 0 Then
                SaveDocument(intVoucherId, Me.Name, trans)
            End If

            trans.Commit()
            SaveTemplate = True
            SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, txtVoucherNo.Text.Trim, True)
            setEditMode = False
            setVoucherNo = Me.txtVoucherNo.Text
        Catch ex As Exception
            trans.Rollback()
            SaveTemplate = False
        Finally

            Me.lblProgress.Visible = False
            objCommand = Nothing

        End Try


    End Function
    Private Function Save() As Boolean
        If Me.chkPost.Visible = False Then
            Me.chkPost.Checked = False
        End If
        'If Me.cmbVoucherType.SelectedIndex > 0 Then
        '    Me.txtVoucherNo.Text = GetNextDocNo(Me.cmbVoucherType.Text, 6, "tblVoucher", "voucher_no")

        'Else
        '    Me.txtVoucherNo.Text = ""
        'End If

        Me.txtVoucherNo.Text = GetVoucherNo()
        'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Dim objCommand As New OleDbCommand

        Dim i As Integer


        Me.grd.UpdateData()
        If Con.State = ConnectionState.Open Then Con.Close()

        Con.Open()
        objCommand.Connection = Con

        Dim trans As OleDbTransaction = Con.BeginTransaction


        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'objCon.BeginTransaction()


            'Dim coaID As Integer = 0
            'If Me.cmbVoucherType.Text = "BRV" Then
            '    'For Each r As DataGridViewRow In Me.grd.Rows
            '    For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            '        If Val(r.Cells("Credit").Value) > 0 Then
            '            coaID = r.Cells("AccountId").Value
            '        End If
            '    Next
            'End If

            'objCommand.CommandText = "Insert into tblVoucher (location_id,finiancial_year_id,voucher_type_id,voucher_no,voucher_date, cheque_no, cheque_date,post,source,Reference,UserName, other_voucher, coa_detail_id, Posted_UserName) values( " _
            '                        & " " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ",1, " & cmbVoucherType.SelectedValue & " ,N'" & txtVoucherNo.Text & "', N'" & Me.dtpVoucherDate.Value & "',N'" & IIf(txtChequeNo.Visible, txtChequeNo.Text, "") & "', " & IIf(Me.dtpChequeDate.Visible, "N'" & dtpChequeDate.Value & "'", "NULL") & ", " & IIf(Me.chkPost.Checked = True, 1, 0) & ",'frmVoucher',N'" & Me.uitxtReference.Text & "',N'" & LoginUserName & "', " & IIf(Me.chkOtherVoucher.Checked, "1", "0") & ", " & coaID & ", " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", " NULL") & ") Select @@Identity"
            'GetVoucherId = objCommand.ExecuteScalar

            'objCommand.CommandText = "Insert into tblVoucher (location_id,finiancial_year_id,voucher_type_id,voucher_no,voucher_date, post,source,Reference,UserName, other_voucher, coa_detail_id, Posted_UserName) values( " _
            '                       & " " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ",1, " & cmbVoucherType.SelectedValue & " ,N'" & txtVoucherNo.Text & "', N'" & Me.dtpVoucherDate.Value & "'," & IIf(Me.chkPost.Checked = True, 1, 0) & ",'frmVoucher',N'" & Me.uitxtReference.Text & "',N'" & LoginUserName & "', " & IIf(Me.chkOtherVoucher.Checked, "1", "0") & ", " & coaID & ", " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", " NULL") & ") Select @@Identity"
            'GetVoucherId = objCommand.ExecuteScalar
            ''Before against ReqId-934 
            'objCommand.CommandText = "Insert into tblVoucher (location_id,finiancial_year_id,voucher_type_id,voucher_no,voucher_date, post,source,Reference,UserName, other_voucher, coa_detail_id, Posted_UserName) values( " _
            '                      & " " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ",1, " & cmbVoucherType.SelectedValue & " ,N'" & txtVoucherNo.Text & "', N'" & Me.dtpVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & IIf(Me.chkPost.Checked = True, 1, 0) & ",'frmVoucher',N'" & Me.uitxtReference.Text & "',N'" & LoginUserName & "', " & IIf(Me.chkOtherVoucher.Checked, "1", "0") & ", " & 0 & ", " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", " NULL") & ") Select @@Identity"
            'GetVoucherId = objCommand.ExecuteScalar
            ''ReqId-934 Resolve Comma Error 
            'Before against task:2780
            'objCommand.CommandText = "Insert into tblVoucher (location_id,finiancial_year_id,voucher_type_id,voucher_no,voucher_date, post,source,Reference,UserName, other_voucher, coa_detail_id, Posted_UserName) values( " _
            '                   & " " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ",1, " & cmbVoucherType.SelectedValue & " ,N'" & txtVoucherNo.Text.Replace("'", "''") & "', N'" & Me.dtpVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & IIf(Me.chkPost.Checked = True, 1, 0) & ",'frmVoucher',N'" & Me.uitxtReference.Text.Replace("'", "''") & "',N'" & LoginUserName & "', " & IIf(Me.chkOtherVoucher.Checked, "1", "0") & ", " & 0 & ", " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", " NULL") & ") Select @@Identity"
            'Task:2780 This column is added to the cmfa document
            'Before against task:2826
            'objCommand.CommandText = "Insert into tblVoucher (location_id,finiancial_year_id,voucher_type_id,voucher_no,voucher_date, post,source,Reference,UserName, other_voucher, coa_detail_id, Posted_UserName, CMFADocId) values( " _
            '                 & " " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ",1, " & cmbVoucherType.SelectedValue & " ,N'" & txtVoucherNo.Text.Replace("'", "''") & "', N'" & Me.dtpVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & IIf(Me.chkPost.Checked = True, 1, 0) & ",'frmVoucher',N'" & Me.uitxtReference.Text.Replace("'", "''") & "',N'" & LoginUserName & "', " & IIf(Me.chkOtherVoucher.Checked, "1", "0") & ", " & 0 & ", " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", " NULL") & "," & IIf(Me.cmbCMFADoc.SelectedIndex > 0, Me.cmbCMFADoc.SelectedValue, "NULL") & ") Select @@Identity"
            'End Task:2780
            'Task:2826 Added Field Checked
            Dim intVoucherId As Integer = 0I
            objCommand.CommandText = "Insert into tblVoucher (location_id,finiancial_year_id,voucher_type_id,voucher_no,voucher_date, post,source,Reference,UserName, other_voucher, coa_detail_id, Posted_UserName, CMFADocId, Checked,CheckedByUser) values( " _
                             & " " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", Me.cmbCompany.SelectedValue.ToString) & ",1, " & cmbVoucherType.SelectedValue & " ,N'" & txtVoucherNo.Text.Replace("'", "''") & "', N'" & Me.dtpVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & IIf(Me.chkPost.Checked = True, 1, 0) & ",'frmVoucher',N'" & Me.uitxtReference.Text.Replace("'", "''") & "',N'" & LoginUserName & "', " & IIf(Me.chkOtherVoucher.Checked, "1", "0") & ", " & 0 & ", " & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", " NULL") & "," & IIf(Me.cmbCMFADoc.SelectedIndex > 0, Me.cmbCMFADoc.SelectedValue, "NULL") & ", " & IIf(Me.chkChecked.Checked = True, 1, 0) & "," & IIf(Me.chkChecked.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & ") Select @@Identity"
            'End Task:2826
            intVoucherId = objCommand.ExecuteScalar
            'End Task:2443
            'Marked agaisnt  Task#2015060005 to save multiple files
            '            If arrFile.Length > 0 Then SaveDocument(GetVoucherId, "frmVoucher", trans)
            'Marked agaisnt  Task#2015060005 to save multiple files
            'Altered agaisnt  Task#2015060005 to save multiple files
            'If arrFile.Count > 0 Then
            '    SaveDocument(intVoucherId, Me.Name, trans)
            'End If
            'Altered agaisnt  Task#2015060005 to save multiple files


            objCommand.CommandText = ""

            Dim strMultiChequeDetail As String = String.Empty 'Task:2443  e.g Multi Cheque Detail Value Store 

            'For i = 0 To grd.Rows.Count - 1
            For i = 0 To grd.RowCount - 1
                objCommand.CommandText = ""



                If grd.GetRows(i).Cells("Type").Value.ToString.Trim.ToUpper = "LC" Then
                    If Val(grd.GetRows(i).Cells("CostCenterId").Value.ToString) <= 0 Then
                        Throw New Exception("Please select cost center.")
                    End If
                End If

                'objCommand.CommandText = "Insert into tblVoucherDetail (voucher_id, location_id,coa_detail_id, comments,debit_amount,credit_amount, CostCenterID) values( " _
                '                        & " ident_current('tblVoucher'), 1, " & Val(grd.Rows(i).Cells(EnumGrid.AccountID).Value) & ",N'" & (grd.Rows(i).Cells(EnumGrid.Description).Value) & "'," & Val(grd.Rows(i).Cells(EnumGrid.Debit).Value) & ", " _
                '                        & " " & Val(grd.Rows(i).Cells(EnumGrid.Credit).Value) & ", " & grd.Rows(i).Cells(EnumGrid.CostCenterID).Value & " ) "
                'Before against task:2745
                'objCommand.CommandText = "Insert into tblVoucherDetail (voucher_id, location_id,coa_detail_id, comments,debit_amount,credit_amount, CostCenterID, Cheque_No, Cheque_Date) values( " _
                '                        & " ident_current('tblVoucher'), " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(grd.GetRows(i).Cells(EnumGrid.AccountID).Value) & ",N'" & (grd.GetRows(i).Cells(EnumGrid.Description).Value.ToString.Replace("'", "''")) & "'," & Val(grd.GetRows(i).Cells(EnumGrid.Debit).Value) & ", " _
                '                        & " " & Val(grd.GetRows(i).Cells(EnumGrid.Credit).Value) & ", " & grd.GetRows(i).Cells(EnumGrid.CostCenterID).Value & ", " & IIf(grd.GetRows(i).Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & grd.GetRows(i).Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(grd.GetRows(i).Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(grd.GetRows(i).Cells("Cheque_Date").Value), Now, grd.GetRows(i).Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ")"
                'Task:2745 Added Field Cheque No. 
                'CurrencyId()
                'CurrencyAmount()
                'CurrencyRate()
                'BaseCurrencyId()
                'BaseCurrencyRate()

                ' R@! Shahid    11-Jun-2016     Dollor Account
                ' Old code commented
                'objCommand.CommandText = "Insert into tblVoucherDetail (voucher_id, location_id,coa_detail_id, comments,debit_amount,credit_amount, CostCenterID, Cheque_No, Cheque_Date,ChequeDescription, CurrencyId, CurrencyAmount, CurrencyRate, BaseCurrencyId, BaseCurrencyRate) values( " _
                '                        & " ident_current('tblVoucher'), " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(grd.GetRows(i).Cells(EnumGrid.AccountID).Value) & ",N'" & (grd.GetRows(i).Cells(EnumGrid.Description).Value.ToString.Replace("'", "''")) & "'," & Val(grd.GetRows(i).Cells(EnumGrid.Debit).Value) & ", " _
                '                        & " " & Val(grd.GetRows(i).Cells(EnumGrid.Credit).Value) & ", " & grd.GetRows(i).Cells(EnumGrid.CostCenterID).Value & ", " & IIf(grd.GetRows(i).Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & grd.GetRows(i).Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(grd.GetRows(i).Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(grd.GetRows(i).Cells("Cheque_Date").Value), Now, grd.GetRows(i).Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & GetComments(grd.GetRows(i)).Replace("'", "''") & "', " & Val(grd.GetRow(i).Cells("CurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("CurrencyAmount").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("BaseCurrencyRate").Value.ToString) & ")Select @@Identity"
                ''End Task:2745

                ' Added 2 new columns Currency_debit_amount, Currency_Credit_Amount
                objCommand.CommandText = "Insert into tblVoucherDetail (voucher_id, location_id,coa_detail_id, comments,debit_amount,credit_amount, CostCenterID, Cheque_No, Cheque_Date,ChequeDescription, CurrencyId, CurrencyRate, BaseCurrencyId, BaseCurrencyRate, Currency_Debit_Amount, Currency_Credit_Amount, Currency_Symbol) values( " _
                                   & " ident_current('tblVoucher'), " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(grd.GetRows(i).Cells(EnumGrid.AccountID).Value) & ",N'" & (grd.GetRows(i).Cells(EnumGrid.Description).Value.ToString.Replace("'", "''")) & "'," & Val(grd.GetRows(i).Cells(EnumGrid.Debit).Value) & ", " _
                                   & " " & Val(grd.GetRows(i).Cells(EnumGrid.Credit).Value) & ", " & grd.GetRows(i).Cells(EnumGrid.CostCenterID).Value & ", " & IIf(grd.GetRows(i).Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & grd.GetRows(i).Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(grd.GetRows(i).Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(grd.GetRows(i).Cells("Cheque_Date").Value), Now, grd.GetRows(i).Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & GetComments(grd.GetRows(i)).Replace("'", "''") & "', " & Val(grd.GetRow(i).Cells("CurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("currencyDr").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("CurrencyCr").Value.ToString) & ", '" & Me.cmbCurrency.Text & "')Select @@Identity"

                objCommand.ExecuteScalar()
                'Val(grd.Rows(i).Cells(5).Value)


                'Task:2443  e.g Multi Cheque Detail Value Store 
                If grd.GetRows(i).Cells("Cheque_No").Value.ToString.Length > 0 Then
                    If strMultiChequeDetail.Length > 0 Then
                        strMultiChequeDetail += "|" & grd.GetRows(i).Cells("Cheque_No").Value.ToString & ":" & IIf(grd.GetRows(i).Cells("Cheque_Date").Value.ToString <> "", CDate(grd.GetRows(i).Cells("Cheque_Date").Value.ToString).ToString("d/MMM/yy"), "")
                    Else
                        strMultiChequeDetail = grd.GetRows(i).Cells("Cheque_No").Value.ToString & ":" & IIf(grd.GetRows(i).Cells("Cheque_Date").Value.ToString <> "", CDate(grd.GetRows(i).Cells("Cheque_Date").Value.ToString).ToString("d/MMM/yy"), "")
                    End If
                End If
                'end Task:4243

            Next


            'Task:2443 Update Multi Cheque Date On Master Record
            If strMultiChequeDetail.Length > 0 AndAlso strMultiChequeDetail.Length < 8000 Then
                objCommand.CommandText = ""
                objCommand.CommandText = "Update tblVoucher SET Cheque_No=N'" & strMultiChequeDetail.Replace("'", "''") & "' WHERE Voucher_Id=" & intVoucherId
                objCommand.ExecuteNonQuery()
            End If
            'End Task:2443
            'Marked agaisnt  Task#2015060005 to save multiple files
            '            If arrFile.Length > 0 Then SaveDocument(GetVoucherId, "frmVoucher", trans)
            'Marked agaisnt  Task#2015060005 to save multiple files
            'Altered agaisnt  Task#2015060005 to save multiple files
            If arrFile.Count > 0 Then
                SaveDocument(intVoucherId, Me.Name, trans)
            End If
            'Altered agaisnt  Task#2015060005 to save multiple files

            trans.Commit()
            Save = True
            SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, txtVoucherNo.Text.Trim, True)
            setEditMode = False
            setVoucherNo = Me.txtVoucherNo.Text
        Catch ex As Exception
            trans.Rollback()
            Save = False
        Finally
            'Con = Nothing
            Me.lblProgress.Visible = False
            objCommand = Nothing

        End Try


    End Function
    Private Function Update_Record() As Boolean
        If Me.chkPost.Visible = False Then
            Me.chkPost.Checked = False
        End If
        Me.grd.UpdateData()
        Dim objCommand As New OleDbCommand
        Dim i As Integer

        If Con.State = ConnectionState.Open Then Con.Close()
        Con.Open()
        objCommand.Connection = Con
        Dim trans As OleDbTransaction = Con.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            'objCon.BeginTransaction()

            Dim coaID As Integer = 0
            'For Each r As DataGridViewRow In Me.grd.Rows
            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            '    If Val(r.Cells("Credit").Value) > 0 Then
            '        coaID = r.Cells("AccountId").Value
            '    End If
            'Next
            If getConfigValueByType("EnabledDuplicateVoucherLog").ToString.ToUpper = "TRUE" Then
                Call CreateDuplicationVoucher(Val(txtVoucherID.Text), "Update", trans) 'TASKM2710151
            End If
            'objCommand.CommandText = "Update tblVoucher set Location_Id=" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", voucher_type_id=" & cmbVoucherType.SelectedValue & ",voucher_no=N'" & txtVoucherNo.Text & "',voucher_date=N'" & dtpVoucherDate.Value & "', " _
            '                        & " cheque_no=N'" & IIf(txtChequeNo.Visible, txtChequeNo.Text, "") & "', cheque_date=" & IIf(dtpChequeDate.Visible, "N'" & dtpChequeDate.Value & "'", "NULL") & ", Reference=N'" & Me.uitxtReference.Text.ToString.Replace("'", "''") & "', other_voucher = " & IIf(Me.chkOtherVoucher.Checked, "1", "0") & ", coa_detail_id = " & coaID & ", Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", Posted_UserName=" & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & " Where Voucher_ID= " & txtVoucherID.Text & " "

            'objCommand.ExecuteNonQuery()
            'Before against task:2780
            'objCommand.CommandText = "Update tblVoucher set Location_Id=" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", voucher_type_id=" & cmbVoucherType.SelectedValue & ",voucher_no=N'" & txtVoucherNo.Text.Replace("'", "''") & "',voucher_date=N'" & dtpVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                   & "  Reference=N'" & Me.uitxtReference.Text.ToString.Replace("'", "''") & "', other_voucher = " & IIf(Me.chkOtherVoucher.Checked, "1", "0") & ", coa_detail_id = " & 0 & ", Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", Posted_UserName=" & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & " Where Voucher_ID= " & txtVoucherID.Text & " "
            'TAsk:2780 This Column is added to the cmfa document
            'Before against task:2826
            'objCommand.CommandText = "Update tblVoucher set Location_Id=" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", voucher_type_id=" & cmbVoucherType.SelectedValue & ",voucher_no=N'" & txtVoucherNo.Text.Replace("'", "''") & "',voucher_date=N'" & dtpVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            '                               & "  Reference=N'" & Me.uitxtReference.Text.ToString.Replace("'", "''") & "', other_voucher = " & IIf(Me.chkOtherVoucher.Checked, "1", "0") & ", coa_detail_id = " & 0 & ", Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", Posted_UserName=" & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ", CMFADocId=" & IIf(Me.cmbCMFADoc.SelectedIndex > 0, Me.cmbCMFADoc.SelectedValue, "NULL") & " Where Voucher_ID= " & txtVoucherID.Text & " "
            'End Task:2780
            'Task:2826 Added Field Checked
            'Task#05082015 Add Company location id selected from cmbCompany combo box
            objCommand.CommandText = "Update tblVoucher set Location_Id=" & IIf(flgCompanyRights = True, "" & MyCompanyId & "", Me.cmbCompany.SelectedValue.ToString) & ", voucher_type_id=" & cmbVoucherType.SelectedValue & ",voucher_no=N'" & txtVoucherNo.Text.Replace("'", "''") & "',voucher_date=N'" & dtpVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                          & "  Reference=N'" & Me.uitxtReference.Text.ToString.Replace("'", "''") & "', other_voucher = " & IIf(Me.chkOtherVoucher.Checked, "1", "0") & ", coa_detail_id = " & 0 & ", Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ", Posted_UserName=" & IIf(Me.chkPost.Checked = True, "N'" & LoginUserName & "'", "NULL") & ", CMFADocId=" & IIf(Me.cmbCMFADoc.SelectedIndex > 0, Me.cmbCMFADoc.SelectedValue, "NULL") & ",Checked=" & IIf(Me.chkChecked.Checked = True, 1, 0) & ",CheckedByUser=" & IIf(Me.chkChecked.Checked = True, "N'" & LoginUserName.Replace("'", "''") & "'", "NULL") & " Where Voucher_ID= " & txtVoucherID.Text & ""
            'End Task:2826
            objCommand.ExecuteNonQuery()

            'If arrFile.Length > 0 Then
            '    SaveDocument(Val(txtReceivingID.Text), Me.Name, trans)
            'End If
            'Marked Against Task#2015060001 Ali Ansari
            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                SaveDocument(Val(txtVoucherID.Text), Me.Name, trans)
            End If
            'Altered Against Task#2015060001 Ali Ansari
            objCommand.CommandText = ""
            objCommand.CommandText = "Delete from tblVoucherDetail where voucher_id = " & txtVoucherID.Text
            objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""
            Dim strMultiChequeDetail As String = String.Empty 'Task:2443  e.g Multi Cheque Detail Value Store 
            'For i = 0 To grd.Rows.Count - 1
            For i = 0 To grd.RowCount - 1
                objCommand.CommandText = ""

                If grd.GetRows(i).Cells("Type").Value.ToString.Trim.ToUpper = "LC" Then
                    If Val(grd.GetRows(i).Cells("CostCenterId").Value.ToString) <= 0 Then
                        Throw New Exception("Please select cost center.")
                    End If
                End If

                'objCommand.CommandText = "Insert into tblVoucherDetail (voucher_id, location_id,coa_detail_id, comments,debit_amount,credit_amount, CostCenterID) values( " _
                '                        & txtVoucherID.Text & " , 1, " & Val(grd.Rows(i).Cells(EnumGrid.AccountID).Value) & ",N'" & (grd.Rows(i).Cells(EnumGrid.Description).Value) & "'," & Val(grd.Rows(i).Cells(EnumGrid.Debit).Value) & ", " _
                '                        & " " & Val(grd.Rows(i).Cells(EnumGrid.Credit).Value) & ", " & grd.Rows(i).Cells(EnumGrid.CostCenterID).Value & " ) "
                'objCommand.CommandText = "Insert into tblVoucherDetail (voucher_id, location_id,coa_detail_id, comments,debit_amount,credit_amount, CostCenterID) values( " _
                '                        & txtVoucherID.Text & " , 1, " & Val(grd.GetRows(i).Cells(EnumGrid.AccountID).Value) & ",N'" & (grd.GetRows(i).Cells(EnumGrid.Description).Value) & "'," & Val(grd.GetRows(i).Cells(EnumGrid.Debit).Value) & ", " _
                '                        & " " & Val(grd.GetRows(i).Cells(EnumGrid.Credit).Value) & ", " & grd.GetRows(i).Cells(EnumGrid.CostCenterID).Value & " ) "
                'Before against task:2745
                'objCommand.CommandText = "Insert into tblVoucherDetail (voucher_id, location_id,coa_detail_id, comments,debit_amount,credit_amount, CostCenterID, Cheque_No, Cheque_Date) values( " _
                '                        & txtVoucherID.Text & " , " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(grd.GetRows(i).Cells(EnumGrid.AccountID).Value) & ",N'" & (grd.GetRows(i).Cells(EnumGrid.Description).Value.ToString.Replace("'", "''")) & "'," & Val(grd.GetRows(i).Cells(EnumGrid.Debit).Value) & ", " _
                '                        & " " & Val(grd.GetRows(i).Cells(EnumGrid.Credit).Value) & ", " & Me.grd.GetRows(i).Cells(EnumGrid.CostCenterID).Value & ", " & IIf(grd.GetRows(i).Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & grd.GetRows(i).Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(grd.GetRows(i).Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells("Cheque_Date").Value Is DBNull.Value, Now, grd.GetRows(i).Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ") "
                'Task:2745 Added Field Cheque No. Date Comments

                ' R@! Shahid    11-Jun-2016     Dollor Account
                ' Old code commented

                'objCommand.CommandText = "Insert into tblVoucherDetail (voucher_id, location_id,coa_detail_id, comments,debit_amount,credit_amount, CostCenterID, Cheque_No, Cheque_Date,ChequeDescription, CurrencyId, CurrencyAmount, CurrencyRate, BaseCurrencyId, BaseCurrencyRate) values( " _
                '                       & txtVoucherID.Text & " , " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(grd.GetRows(i).Cells(EnumGrid.AccountID).Value) & ",N'" & (grd.GetRows(i).Cells(EnumGrid.Description).Value.ToString.Replace("'", "''")) & "'," & Val(grd.GetRows(i).Cells(EnumGrid.Debit).Value) & ", " _
                '                       & " " & Val(grd.GetRows(i).Cells(EnumGrid.Credit).Value) & ", " & Me.grd.GetRows(i).Cells(EnumGrid.CostCenterID).Value & ", " & IIf(grd.GetRows(i).Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & grd.GetRows(i).Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(grd.GetRows(i).Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & CDate(IIf(grd.GetRows(i).Cells("Cheque_Date").Value Is DBNull.Value, Now, grd.GetRows(i).Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ",N'" & GetComments(grd.GetRows(i)).Replace("'", "''") & "', " & Val(Me.grd.GetRow(i).Cells("CurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("CurrencyAmount").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("BaseCurrencyRate").Value.ToString) & ") Select @@Identity"
                ''End Task:2745

                ' Added 2 new columns Currency_debit_amount, Currency_Credit_Amount
                objCommand.CommandText = "Insert into tblVoucherDetail (voucher_id, location_id,coa_detail_id, comments,debit_amount,credit_amount, CostCenterID, Cheque_No, Cheque_Date,ChequeDescription, CurrencyId, CurrencyRate, BaseCurrencyId, BaseCurrencyRate, Currency_Debit_Amount, Currency_Credit_Amount, Currency_Symbol) values( " _
                                   & txtVoucherID.Text & " , " & IIf(flgCompanyRights = True, "" & MyCompanyId & "", "1") & ", " & Val(grd.GetRows(i).Cells(EnumGrid.AccountID).Value) & ",N'" & (grd.GetRows(i).Cells(EnumGrid.Description).Value.ToString.Replace("'", "''")) & "'," & Val(grd.GetRows(i).Cells(EnumGrid.Debit).Value) & ", " _
                                   & " " & Val(grd.GetRows(i).Cells(EnumGrid.Credit).Value) & ", " & grd.GetRows(i).Cells(EnumGrid.CostCenterID).Value & ", " & IIf(grd.GetRows(i).Cells("Cheque_No").Value.ToString = "", "NULL", "N'" & grd.GetRows(i).Cells("Cheque_No").Value.ToString.Replace("'", "''") & "'") & ", " & IIf(grd.GetRows(i).Cells("Cheque_No").Value.ToString.Length = 0, "NULL", "N'" & CDate(IIf(IsDBNull(grd.GetRows(i).Cells("Cheque_Date").Value), Now, grd.GetRows(i).Cells("Cheque_Date").Value)).ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & GetComments(grd.GetRows(i)).Replace("'", "''") & "', " & Val(grd.GetRow(i).Cells("CurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("CurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("BaseCurrencyId").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("BaseCurrencyRate").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("currencyDr").Value.ToString) & ", " & Val(Me.grd.GetRow(i).Cells("CurrencyCr").Value.ToString) & ", '" & Me.cmbCurrency.Text & "')Select @@Identity"



                Dim objId As Object = objCommand.ExecuteScalar()
                'Marked Against Task#2015060001 Ali Ansari


                If Val(Me.grd.GetRows(i).Cells("VoucherDetailId").Value.ToString) > 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update InvoiceAdjustmentTable Set VoucherDetailId=" & objId & " WHERE VoucherDetailId=" & Val(Me.grd.GetRows(i).Cells("VoucherDetailId").Value.ToString) & ""
                    objCommand.ExecuteNonQuery()
                End If



                'Task:2443  e.g Multi Cheque Detail Value Store 
                If grd.GetRows(i).Cells("Cheque_No").Value.ToString.Length > 0 Then
                    If strMultiChequeDetail.Length > 0 Then
                        strMultiChequeDetail += "|" & grd.GetRows(i).Cells("Cheque_No").Value.ToString & ":" & IIf(grd.GetRows(i).Cells("Cheque_Date").Value.ToString <> "", CDate(grd.GetRows(i).Cells("Cheque_Date").Value.ToString).ToString("d/MMM/yy"), "")
                    Else
                        strMultiChequeDetail = grd.GetRows(i).Cells("Cheque_No").Value.ToString & ":" & IIf(grd.GetRows(i).Cells("Cheque_Date").Value.ToString <> "", CDate(grd.GetRows(i).Cells("Cheque_Date").Value.ToString).ToString("d/MMM/yy"), "")
                    End If
                End If
                'end Task:4243


            Next

            'Task:2443 Update Multi Cheque Date On Master Record
            If strMultiChequeDetail.Length > 0 AndAlso strMultiChequeDetail.Length < 8000 Then
                objCommand.CommandText = ""
                objCommand.CommandText = "Update tblVoucher SET Cheque_No=N'" & strMultiChequeDetail.Replace("'", "''") & "' WHERE Voucher_Id=" & Val(txtVoucherID.Text)
                objCommand.ExecuteNonQuery()
            End If
            'End Task:2443

            'If arrFile.Length > 0 Then SaveDocument(Val(Me.txtVoucherID.Text), "frmVoucher", trans)


            trans.Commit()
            Update_Record = True
            SaveActivityLog("Accounts", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, txtVoucherNo.Text.Trim, True)
            setEditMode = True
            setVoucherNo = Me.txtVoucherNo.Text
            GetVoucherId = Val(Me.txtVoucherID.Text)
        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
        Finally
            'Con = Nothing
            objCommand = Nothing
        End Try

    End Function
    Sub EditRecord()
        Try


            blnEditMode = True
            FillCombo("Account")
            FillCombo(Me.cmbCostCenter.Name)
            dtpVoucherDate.Value = grdSaved.CurrentRow.Cells(2).Value
            txtVoucherID.Text = grdSaved.CurrentRow.Cells(5).Value
            cmbVoucherType.SelectedIndex = cmbVoucherType.FindStringExact((grdSaved.CurrentRow.Cells(0).Value))

            Me.chkChecked.Checked = Me.grdSaved.CurrentRow.Cells("Checked").Value 'Task:2826 Retrive Checked Seucrity
            'txtChequeNo.Text = grdSaved.CurrentRow.Cells(3).Value & ""
            'dtpChequeDate.Text = grdSaved.CurrentRow.Cells(4).Value & ""
            txtVoucherNo.Text = grdSaved.CurrentRow.Cells(1).Value & ""
            CurrentVoucherNo = Me.txtVoucherNo.Text
            Me.uitxtReference.Text = grdSaved.CurrentRow.Cells("Reference").Value.ToString
            Me.chkOtherVoucher.Checked = grdSaved.CurrentRow.Cells("other_voucher").Value
            Me.cmbCMFADoc.SelectedValue = grdSaved.CurrentRow.Cells("CMFADocId").Value 'Task:2780 Double-click to get the cmfa document
            Call DisplayDetail(grdSaved.CurrentRow.Cells(5).Value)
            Previouse_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Credit"), Janus.Windows.GridEX.AggregateFunction.Sum)
            GetTotal()
            Me.BtnSave.Text = "&Update"
            'Me.cmbVoucherType.Enabled = True
            GetSecurityRights()
            Me.chkPost.Checked = Me.grdSaved.CurrentRow.Cells("Post").Value
            If Not grdSaved.CurrentRow.Cells("Source").Value & "" = "Voucher" Then
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
            End If
            Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
            Me.btnAdd.Visible = True
            Me.btnCancel.Visible = True
            Me.btnReplace.Visible = True
            Me.lblPrintStatus.Text = "Print Status: " & Me.grdSaved.GetRow.Cells("Print Status").Text.ToString
            Me.btnAttachment.Text = "Attachment (" & Me.grdSaved.GetRow.Cells("No Of Attachment").Value.ToString & ")"
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpVoucherDate.Value.ToString("yyyy-M-d 00:00:00") Then
                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    Me.dtpVoucherDate.Enabled = False
                Else
                    Me.dtpVoucherDate.Enabled = True
                End If
            Else
                Me.dtpVoucherDate.Enabled = True
            End If
            If getConfigValueByType("ChangeVoucherType").ToString.ToUpper <> "ERROR" Then
                If getConfigValueByType("ChangeVoucherType").ToString = "True" Then
                    Me.cmbVoucherType.Enabled = True
                Else
                    Me.cmbVoucherType.Enabled = False
                End If
            End If
            'Task#05082015 Edit company name
            Me.cmbCompany.Text = Me.grdSaved.GetRow.Cells("Company Name").Value.ToString
            'End Task#05082015
            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            'Me.BtnDelete.Visible = True
            'Me.BtnPrint.Visible = True

            If getConfigValueByType("EnabledDuplicateVoucherLog").ToString.ToUpper = "TRUE" Then

                Dim intCountVouchers As Integer = 0
                Dim dtCountVouches As New DataTable
                dtCountVouches = GetDuplicateVouchers(Convert.ToInt32(Val(Me.txtVoucherID.Text)))
                dtCountVouches.AcceptChanges()
                If dtCountVouches.Rows.Count > 0 Then
                    intCountVouchers = dtCountVouches.Rows.Count
                    Me.btnUpdateTimes.Visible = True
                    btnUpdateTimes.Text = "No of update times (" & intCountVouchers & ")"
                    Call CreateContextMenu(Val(Me.grdSaved.GetRow.Cells("Voucher_Id").Value.ToString), btnUpdateTimes)
                Else
                    Me.btnUpdateTimes.Visible = False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayVoucherTemplateDetail(ByVal VoucherID As Integer)
        Try

            RefreshControls()

            cmbVoucherType.SelectedIndex = cmbVoucherType.FindStringExact((grdTemplates.CurrentRow.Cells("VoucherType").Value.ToString))

            Me.uitxtReference.Text = grdTemplates.CurrentRow.Cells("Reference").Value.ToString

            Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab


            'Me.cmbCompany.Text = Me.grdSaved.GetRow.Cells("Company Name").Value.ToString

            '------------------- End master load --------------

            Dim str As String
            Dim i As Integer = 0

            str = " SELECT ACDetail.sub_sub_title As [Head], ACDetail.detail_title As [Account], VD.comments as [Description], IsNull(VD.CostCenterID,0) as CostCenterId, VD.Cheque_No, Isnull(VD.Cheque_Date,GetDate()) as Cheque_Date, IsNull(VD.CurrencyId, 0) As CurrencyId,  IsNull(VD.Currency_Debit_Amount, debit_amount)  as CurrencyDr,  IsNull(VD.Currency_Credit_Amount, Credit_amount)  as CurrencyCr, IsNull(VD.CurrencyRate, 1) As CurrencyRate, IsNull(VD.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(VD.BaseCurrencyRate, 0) As BaseCurrencyRate, convert(money,VD.debit_amount) as Debit, convert(money,VD.credit_amount) as Credit, VD.coa_detail_id  as [AccountId], ACDetail.Account_Type as Type,IsNull(InvAdj.VoucherDetailId,0) as VoucherDetailId " _
                       & " FROM dbo.tblVoucherTemplate V INNER JOIN  dbo.tblVoucherTemplateDetail VD ON V.voucher_id = VD.voucher_id INNER JOIN " _
                       & " dbo.vwCOADetail ACDetail ON VD.coa_detail_id = ACDetail.coa_detail_id left outer join " _
                       & " tblDefCostCenter on vd.CostCenterID =  tblDefCostCenter.CostCenterID LEFT OUTER JOIN (Select Distinct VoucherDetailId From InvoiceAdjustmentTable) as InvAdj on InvAdj.VoucherDetailId = VD.Voucher_Detail_Id " _
                       & " Where VD.Voucher_ID =" & VoucherID & " order by VD.Voucher_Detail_Id ASC "

            Dim objCommand As New OleDbCommand

            Dim objDataAdapter As New OleDbDataAdapter
            Dim objDataSet As New DataSet

            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = str

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(objDataSet)
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = objDataSet.Tables(0)
            If objDataSet.Tables(0).Rows.Count > 0 Then
                If IsDBNull(objDataSet.Tables(0).Rows.Item(0).Item("CurrencyId")) Or Val(objDataSet.Tables(0).Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                Else
                    Me.cmbCurrency.SelectedValue = Val(objDataSet.Tables(0).Rows.Item(0).Item("CurrencyId").ToString)
                    Me.cmbCurrency.Enabled = False
                End If
            End If
            CostCenterGrdCombo()

            GridApplySetting()

            Me.grd.RootTable.Columns(EnumGrid.CurrencyDr).Caption = "" & Me.cmbCurrency.Text & " Dr"
            Me.grd.RootTable.Columns(EnumGrid.CurrencyDr).Caption = "" & Me.cmbCurrency.Text & " Cr"

            Me.grd.RootTable.Columns("Debit").Caption = "" & BaseCurrencyName & " Debit"
            Me.grd.RootTable.Columns("Credit").Caption = "" & BaseCurrencyName & " Credit"

            CType(grd.DataSource, DataTable).Columns("debit").Expression = "[CurrencyRate]*[CurrencyDr]"
            CType(grd.DataSource, DataTable).Columns("credit").Expression = "[CurrencyRate]*[CurrencyCr]"

            Me.GetTotal()
            Me.SaveAsTemplateToolStripMenuItem.Text = "Update template " & Me.grdTemplates.CurrentRow.Cells("TemplateName").Value.ToString
            SaveAsTemplateToolStripMenuItem.Tag = VoucherID
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub DisplayDetail(ByVal VoucherID As Integer)
        Try

            Dim str As String
            Dim i As Integer = 0

            'str = " SELECT AC_Sub.sub_sub_title As [Head], ACDetail.detail_title As [Account], VD.comments as [Description], IsNull(VD.CostCenterID,0) as CostCenterId, VD.Cheque_No, Isnull(VD.Cheque_Date,GetDate()) as Cheque_Date, convert(money,VD.debit_amount) as Debit, convert(money,VD.credit_amount) as Credit, VD.coa_detail_id  as [AccountId], ACDetail.Account_Type as Type " _
            '      & " FROM dbo.tblVoucher V INNER JOIN  dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id INNER JOIN " _
            '      & " dbo.vwCOADetail ACDetail ON VD.coa_detail_id = ACDetail.coa_detail_id INNER JOIN " _
            '      & " dbo.tblCOAMainSubSub AC_Sub ON ACDetail.main_sub_sub_id = AC_Sub.main_sub_sub_id left outer join" _
            '      & " tblDefCostCenter on vd.CostCenterID =  tblDefCostCenter.CostCenterID" _
            '      & " Where VD.Voucher_ID =" & VoucherID & " order by 4 desc"
            'CurrencyId()
            'CurrencyAmount()
            'CurrencyRate()
            'BaseCurrencyId()
            'BaseCurrencyRate()

            'R@!    11-Jun-2016     Dollor Account
            'Commented old code

            'str = " SELECT ACDetail.sub_sub_title As [Head], ACDetail.detail_title As [Account], VD.comments as [Description], IsNull(VD.CostCenterID,0) as CostCenterId, VD.Cheque_No, Isnull(VD.Cheque_Date,GetDate()) as Cheque_Date, IsNull(VD.CurrencyId, 0) As CurrencyId, IsNull(VD.CurrencyAmount, 0) As CurrencyAmount, IsNull(VD.CurrencyRate, 0) As CurrencyRate, IsNull(VD.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(VD.BaseCurrencyRate, 0) As BaseCurrencyRate, convert(money,VD.debit_amount) as Debit, convert(money,VD.credit_amount) as Credit, VD.coa_detail_id  as [AccountId], ACDetail.Account_Type as Type,IsNull(InvAdj.VoucherDetailId,0) as VoucherDetailId " _
            '     & " FROM dbo.tblVoucher V INNER JOIN  dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id INNER JOIN " _
            '     & " dbo.vwCOADetail ACDetail ON VD.coa_detail_id = ACDetail.coa_detail_id left outer join " _
            '     & " tblDefCostCenter on vd.CostCenterID =  tblDefCostCenter.CostCenterID LEFT OUTER JOIN (Select Distinct VoucherDetailId From InvoiceAdjustmentTable) as InvAdj on InvAdj.VoucherDetailId = VD.Voucher_Detail_Id " _
            '     & " Where VD.Voucher_ID =" & VoucherID & " order by VD.Voucher_Detail_Id ASC "

            '// Adding 2 new columns of CurrencyDr, CurrecnyCr after Currency id
            str = " SELECT ACDetail.sub_sub_title As [Head], ACDetail.detail_title As [Account], VD.comments as [Description], IsNull(VD.CostCenterID,0) as CostCenterId, VD.Cheque_No, Isnull(VD.Cheque_Date,GetDate()) as Cheque_Date, IsNull(VD.CurrencyId, 0) As CurrencyId,  IsNull(VD.Currency_Debit_Amount, debit_amount)  as CurrencyDr,  IsNull(VD.Currency_Credit_Amount, Credit_amount)  as CurrencyCr, IsNull(VD.CurrencyRate, 1) As CurrencyRate, IsNull(VD.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(VD.BaseCurrencyRate, 0) As BaseCurrencyRate, convert(money,VD.debit_amount) as Debit, convert(money,VD.credit_amount) as Credit, VD.coa_detail_id  as [AccountId], ACDetail.Account_Type as Type,IsNull(InvAdj.VoucherDetailId,0) as VoucherDetailId " _
                        & " FROM dbo.tblVoucher V INNER JOIN  dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id INNER JOIN " _
                        & " dbo.vwCOADetail ACDetail ON VD.coa_detail_id = ACDetail.coa_detail_id left outer join " _
                        & " tblDefCostCenter on vd.CostCenterID =  tblDefCostCenter.CostCenterID LEFT OUTER JOIN (Select Distinct VoucherDetailId From InvoiceAdjustmentTable) as InvAdj on InvAdj.VoucherDetailId = VD.Voucher_Detail_Id " _
                        & " Where VD.Voucher_ID =" & VoucherID & " order by VD.Voucher_Detail_Id ASC "

            Dim objCommand As New OleDbCommand

            Dim objDataAdapter As New OleDbDataAdapter
            Dim objDataSet As New DataSet

            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = str

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(objDataSet)
            'Me.grd.DataBindings.Clear()
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = objDataSet.Tables(0)
            If objDataSet.Tables(0).Rows.Count > 0 Then
                If IsDBNull(objDataSet.Tables(0).Rows.Item(0).Item("CurrencyId")) Or Val(objDataSet.Tables(0).Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                    'Me.cmbCurrency.SelectedValue = Nothing
                    'Me.cmbCurrency.Enabled = False
                Else
                    Me.cmbCurrency.SelectedValue = Val(objDataSet.Tables(0).Rows.Item(0).Item("CurrencyId").ToString)
                    Me.cmbCurrency.Enabled = False
                End If
            End If
            CostCenterGrdCombo()

            GridApplySetting()

            ' R@!   11-Jun-2016   Dollor Account
            'Me.grd.RootTable.Columns("CurrencyAmount").Caption = "" & Me.cmbCurrency.Text & " Amount"
            Me.grd.RootTable.Columns(EnumGrid.CurrencyDr).Caption = "" & Me.cmbCurrency.Text & " Dr"
            Me.grd.RootTable.Columns(EnumGrid.CurrencyCr).Caption = "" & Me.cmbCurrency.Text & " Cr"

            'Me.grd.RootTable.Columns("Debit").Caption = "" & GetBasicCurrencyName(Val(getConfigValueByType("Currency").ToString)) & " Debit"
            'Me.grd.RootTable.Columns("Credit").Caption = "" & GetBasicCurrencyName(Val(getConfigValueByType("Currency").ToString)) & " Credit"

            Me.grd.RootTable.Columns("Debit").Caption = "" & BaseCurrencyName & " Debit"
            Me.grd.RootTable.Columns("Credit").Caption = "" & BaseCurrencyName & " Credit"

            ' R@!   11-Jun-2016   Dollor Account
            CType(grd.DataSource, DataTable).Columns("debit").Expression = "[CurrencyRate]*[CurrencyDr]"
            CType(grd.DataSource, DataTable).Columns("credit").Expression = "[CurrencyRate]*[CurrencyCr]"

            Me.GetTotal()
            'grd.Rows.Clear()
            'For i = 0 To objDataSet.Tables(0).Rows.Count - 1
            'grd.Rows.Add(objDataSet.Tables(0).Rows(i)(EnumGrid.Head), objDataSet.Tables(0).Rows(i)(EnumGrid.Account), objDataSet.Tables(0).Rows(i)(EnumGrid.Description), objDataSet.Tables(0).Rows(i)(EnumGrid.CostCenter), objDataSet.Tables(0).Rows(i)(EnumGrid.Debit), objDataSet.Tables(0).Rows(i)(EnumGrid.Credit), objDataSet.Tables(0).Rows(i)(EnumGrid.AccountID), objDataSet.Tables(0).Rows(i)(EnumGrid.CostCenterID))
            'Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub grd_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        GetTotal()
    End Sub


    Private Sub cmbVoucherType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'If Me.BtnSave.Text = "Update" Or Me.BtnSave.Text = "&Update" Then Exit Sub
            Me.lblVTypeHeading.Text = String.Empty
            If Me.cmbVoucherType.SelectedValue = 4 Or Me.cmbVoucherType.SelectedValue = 5 Or Me.cmbVoucherType.SelectedValue = 1 Then
                If Me.cmbVoucherType.Text = "JV" Then
                    If EnableChequeDetail = True Then
                        Me.dtpChequeDate.Enabled = True
                        Me.txtChequeNo.Enabled = True
                        Me.grd.RootTable.Columns("Cheque_No").Visible = True
                        Me.grd.RootTable.Columns("Cheque_Date").Visible = True
                        Me.Label4.Visible = True
                        Me.Label5.Visible = True
                    End If
                Else
                    Me.dtpChequeDate.Enabled = True
                    Me.txtChequeNo.Enabled = True
                    Me.grd.RootTable.Columns("Cheque_No").Visible = True
                    Me.grd.RootTable.Columns("Cheque_Date").Visible = True
                    Me.Label4.Visible = True
                    Me.Label5.Visible = True
                End If
            Else
                Me.dtpChequeDate.Enabled = False
                Me.txtChequeNo.Enabled = False
                Me.grd.RootTable.Columns("Cheque_No").Visible = False
                Me.grd.RootTable.Columns("Cheque_Date").Visible = False
                Me.Label4.Visible = True
                Me.Label5.Visible = True
            End If
            setVoucherType = String.Empty
            Select Case Me.cmbVoucherType.Text
                Case Is = "CPV"
                    setVoucherType = "Cash Payment"
                Case Is = "CRV"
                    setVoucherType = "Cash Receipt"
                Case Is = "BPV"
                    setVoucherType = "Bank Payment"
                Case Is = "BRV"
                    setVoucherType = "Bank Receipt"
                Case Is = "JV"
                    setVoucherType = "Journal Voucher"
                Case Is = "SV"
                    setVoucherType = "Sales Voucher"
                Case Is = "PV"
                    setVoucherType = "Purchase Voucher"
                Case Is = strZeroIndexItem
                    setVoucherType = ""

            End Select


            If BtnSave.Text = "&Update" Or Me.BtnSave.Text = "Update" Then
                If Me.cmbVoucherType.SelectedIndex <> cmbVoucherType.FindStringExact((grdSaved.CurrentRow.Cells(0).Value)) Then
                    Me.txtVoucherNo.Text = GetVoucherNo()
                Else
                    Me.txtVoucherNo.Text = CurrentVoucherNo.ToString
                End If
            Else
                Me.txtVoucherNo.Text = GetVoucherNo()
            End If

            Me.lblVTypeHeading.Text = setVoucherType
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Function GetVoucherNo() As String
        Dim VoucherNo As String = String.Empty
        If Me.cmbVoucherType.SelectedIndex > 0 Then


            'Task#05082015 if companywise prefix configuration on then company location id attach/concatenate with voucher no
            Dim compWisePrefix As String = String.Empty
            If getConfigValueByType("CompanyWisePrefix").ToString = "True" Then
                compWisePrefix = Me.cmbVoucherType.Text & Me.cmbCompany.SelectedValue
            Else
                compWisePrefix = Me.cmbVoucherType.Text
            End If

            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo(compWisePrefix.ToString + "-" + Microsoft.VisualBasic.Right(Me.dtpVoucherDate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
            Else
                Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
                Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
                If Not dr Is Nothing Then
                    If dr("config_Value") = "Monthly" Then
                        Return GetNextDocNo(compWisePrefix & "-" & Format(Me.dtpVoucherDate.Value, "yy") & Me.dtpVoucherDate.Value.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
                    Else
                        VoucherNo = GetNextDocNo(compWisePrefix, 6, "tblVoucher", "voucher_no")
                        Return VoucherNo
                    End If
                Else
                    VoucherNo = GetNextDocNo(compWisePrefix, 6, "tblVoucher", "voucher_no")
                    Return VoucherNo
                End If
                'End Task#05082015
            End If
        Else
            Return ""
        End If

    End Function
    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'chkSearch.Checked = False
        dtpfrom.Value = Now
        dtpto.Value = Now
        'cmbVTypeSearch.SelectedIndex = 0
        Call DisplayRecord()

    End Sub

    'Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim Criteria As String
    '    If chkSearch.Checked = False And cmbVTypeSearch.SelectedIndex = 0 Then
    '        MsgBox("You must enter any criteria")
    '        cmbVTypeSearch.Focus()
    '        Exit Sub

    '    End If
    '    Criteria = "1=1"
    '    If chkSearch.Checked = True Then
    '        Criteria = Criteria & " and CONVERT(varchar, V.voucher_date, 103) between N'" & Format(dtpFrom.Value, "dd/MM/yyyy") & "' and N'" & Format(dtpTo.Value, "dd/MM/yyyy") & "'"
    '    End If
    '    If cmbVTypeSearch.SelectedIndex > 0 Then
    '        Criteria = Criteria & "and V.Voucher_type_id = " & cmbVTypeSearch.SelectedValue

    '    End If
    '    Call DisplayRecord(Criteria)

    'End Sub
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.ButtonClick, Button13.Click

        If Me.BtnSave.Enabled = False Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            'If flgDateLock = True Then
            '    If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpVoucherDate.Value.ToString("yyyy-M-d 00:00:00"))) Then
            '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '    End If
            'End If
            If IsDateLock(Me.dtpVoucherDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If

            If Me.dtpVoucherDate.Value <= Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)) Then
                ShowErrorMessage("Your can not change this becuase financial year is closed")
                Me.dtpVoucherDate.Focus()
                Exit Sub
            End If

            '------------------------------ Cheque Validation ----------------------------------------
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                'If r.Cells("Cheque_No").Value.ToString.Length > 0 Then
                Dim rowStyle As Janus.Windows.GridEX.GridEXFormatStyle = New Janus.Windows.GridEX.GridEXFormatStyle()
                Dim strCheque As String = r.Cells("Cheque_No").Value.ToString
                Dim vendorID As Integer = r.Cells("AccountId").Value
                If strCheque.Length > 0 Then
                    If Me.cmbVoucherType.Text.Contains("BPV") Then
                        If Val(r.Cells("Debit").Value.ToString) > 0 Then
                            If IsValidateChequePayment(strCheque, Val(txtVoucherID.Text)) = False Then
                                rowStyle.BackColor = Color.Ivory
                                r.RowStyle = rowStyle
                                Exit Sub
                            Else
                                rowStyle.BackColor = Color.White
                                r.RowStyle = rowStyle
                            End If
                        End If
                    End If
                    If Me.cmbVoucherType.Text.Contains("BRV") Then
                        If Val(r.Cells("Credit").Value.ToString) > 0 Then
                            If IsValidateChequeReceipt(strCheque, vendorID, Val(txtVoucherID.Text)) = False Then
                                rowStyle.BackColor = Color.Ivory
                                r.RowStyle = rowStyle
                                Exit Sub
                            Else
                                rowStyle.BackColor = Color.White
                                r.RowStyle = rowStyle
                            End If
                        End If
                    End If
                Else
                    rowStyle.BackColor = Color.White
                    r.RowStyle = rowStyle
                End If
                'Else
                'Exit For
                'End If
            Next
            '---------------------------------------------- End Cheque Validation ---------------------------------------



            If FormValidate() Then
                Me.grd.UpdateData()
                If Me.BtnSave.Text = "&Save" Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    setEditMode = True
                    Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Credit"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    If Save() Then
                        'MsgBox("Record has been added successfully", MsgBoxStyle.Information, str_MessageHeader)
                        'msg_Information(str_informSave)
                        RefreshControls()
                        ClearDetailControls()
                        DisplayDetail(-1)
                        'grd.Rows.Clear()
                        'DisplayRecord() 'R933 Commented History Data
                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                        'Do While BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop
                        'EmailSave()

                        If BackgroundWorker2.IsBusy Then Exit Sub
                        BackgroundWorker2.RunWorkerAsync()
                        'Do While BackgroundWorker2.IsBusy
                        '    Application.DoEvents()
                        'Loop


                        If msg_Confirm(str_ConfirmPrintVoucher) = False Then Exit Sub
                        If Me.grdSaved.RowCount = 0 Then Exit Sub
                        'AddRptParam("@VoucherId", Me.grdSaved.CurrentRow.Cells("voucher_id").Value)
                        'ShowReport("rptVoucher", , , , True)
                        PrintVoucherBC(Me.grdSaved.GetRow.Cells("voucher_id").Value, Me.grdSaved.GetRow.Cells("Voucher_No").Value.ToString(), True)

                    Else
                        ShowErrorMessage("Error! Record not added")

                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    setEditMode = False
                    Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Credit"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    If Update_Record() Then
                        'MsgBox("Record has been updated successfully", MsgBoxStyle.Information, str_MessageHeader)
                        'msg_Information(str_informUpdate)

                        If msg_Confirm(str_ConfirmPrintVoucher) = True Then
                            If Me.grdSaved.RowCount = 0 Then Exit Sub
                            'AddRptParam("@VoucherId", Me.grdSaved.CurrentRow.Cells("voucher_id").Value)
                            'ShowReport("rptVoucher", , , , True)
                            PrintVoucherBC(Me.grdSaved.GetRow.Cells("voucher_id").Value, Me.grdSaved.GetRow.Cells("Voucher_No").Value.ToString(), True)
                        End If
                        If Me.grdSaved.RowCount = 0 Then Exit Sub
                        'grd.Rows.Clear()
                        'DisplayRecord() ' R933 Commented History Data
                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                        'Do While BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop
                        'EmailSave()
                        If BackgroundWorker2.IsBusy Then Exit Sub
                        BackgroundWorker2.RunWorkerAsync()
                        'Do While BackgroundWorker2.IsBusy
                        '    Application.DoEvents()
                        'Loop

                        RefreshControls()
                        ClearDetailControls()
                        DisplayDetail(-1)

                    Else
                        ShowErrorMessage("Error! Record not updated")
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click, Button6.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            RefreshControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click, Button4.Click

        Try
            Me.EditRecord()
        Catch ex As Exception
            msg_Error(ex.Message)
        Finally

        End Try
    End Sub
    'Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
    '    ShowReport("rptVoucher", "{VwGlVoucherSingle.VoucherId}=" & Me.grdSaved.CurrentRow.Cells(5).Value & "")
    'End Sub

    Private Sub cmbAccount_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccount.Enter
        Me.cmbAccount.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.Dropdown)

    End Sub


    Private Sub grd_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
        Me.GetTotal()
    End Sub
    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click, Button12.Click

        'ValidateDateLock()
        'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
        'If flgDateLock = True Then
        '    If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpVoucherDate.Value.ToString("yyyy-M-d 00:00:00") Then
        '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
        '    End If
        'End If
        If IsDateLock(Me.dtpVoucherDate.Value) = True Then
            ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
        End If
        Dim dtRefDate As DataTable = GetDataTable("Select RefDate From tblFinancialYearCloseStatus WHERE RefVoucher_No=N'" & Me.txtVoucherNo.Text & "'")

        If Not Me.grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

        'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()

        If Me.grdSaved.RowCount = 0 Then Exit Sub
        If CheckInvAdjustmentDependedVoucher(Val(Me.grdSaved.GetRow.Cells("Voucher_Id").Value.ToString)) = True Then
            ShowErrorMessage("Record can't be deleted, voucher adjusted against invoice.")
            Exit Sub
        End If

        Dim lngVoucherMasterId As Integer = grdSaved.CurrentRow.Cells("Voucher_ID").Value 'GetVoucherId(Me.Name, grdSaved.CurrentRow.Cells(0).Value.ToString)
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim cm As New OleDbCommand
            Dim objTrans As OleDbTransaction
            If Con.State = ConnectionState.Closed Then Con.Open()
            objTrans = Con.BeginTransaction



            If getConfigValueByType("EnabledDuplicateVoucherLog").ToString.ToUpper = "TRUE" Then
                Call CreateDuplicationVoucher(Val(lngVoucherMasterId), "Delete", objTrans) 'TASKM2710151
            End If

            cm = New OleDbCommand
            cm.Connection = Con
            cm.CommandText = "delete from tblvoucherdetail where voucher_id=" & lngVoucherMasterId

            cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            cm = New OleDbCommand
            cm.Connection = Con
            cm.CommandText = "delete from tblvoucher where voucher_id=" & lngVoucherMasterId

            cm.Transaction = objTrans
            cm.ExecuteNonQuery()
            If Not dtRefDate Is Nothing Then
                If dtRefDate.Rows.Count > 0 Then
                    If Microsoft.VisualBasic.Left(Me.txtVoucherNo.Text, 3) = "CLS" Then
                        cm = New OleDbCommand
                        cm.Connection = Con
                        cm.Transaction = objTrans
                        cm.CommandText = "Update ConfigValuesTable set config_Value=N'" & dtRefDate.Rows(0).ItemArray(0) & "' WHERE config_Type='EndOfDate'"
                        cm.ExecuteNonQuery()
                        cm.CommandText = ""
                        cm.CommandText = "Delete From  tblFinancialYearCloseStatus WHERE RefVoucher_No=N'" & Me.txtVoucherNo.Text & "'"
                        cm.ExecuteNonQuery()
                    End If
                End If
            End If

            objTrans.Commit()
            'msg_Information(str_informDelete)

            SaveActivityLog("Accounts", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, grdSaved.CurrentRow.Cells(1).Value & "", True)
            Me.grdSaved.CurrentRow.Delete()
            Call DisplayRecord()  'Task 2389 Ehtisham ul Haq, reload history after delete record 21-1-14
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 
        Catch ex As Exception
            msg_Error("Error occured while deleting record: " & ex.Message)

        Finally
            Me.lblProgress.Visible = False
            Con.Close()
            Me.Cursor = Cursors.Default
        End Try
        Me.RefreshControls()
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.btnSearchDelete.Enabled = True ''R934 Added Dlete Button
                Me.btnSearchPrint.Enabled = True   ''R934 Added Print Button
                'Task:2826 Apply Security Checked
                If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = True
                Me.chkPost.Visible = True
                If Me.BtnSave.Text = "&Save" Then Me.chkChecked.Checked = True
                Me.chkChecked.Visible = True
                'End Task:2826
                Exit Sub
            End If

            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.BtnPrint.Enabled = False
                    Me.btnSearchDelete.Enabled = False ''R934 Added Dlete Button
                    Me.btnSearchPrint.Enabled = False  ''R934 Added Print Button
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmVoucher)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString ''R934 Added Dlete Button
                        Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString   ''R934 Added Print Button
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                UserPostingRights = GetUserPostingRights(LoginUserId)
                If UserPostingRights = True Then
                    Me.chkPost.Visible = True
                Else
                    Me.chkPost.Visible = False
                    Me.chkPost.Checked = False
                End If
                GetSecurityByPostingUser(UserPostingRights, BtnSave, BtnDelete)
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                If BtnSave.Text = "&Save" Then Me.chkPost.Checked = False
                Me.chkPost.Visible = False
                If Me.BtnSave.Text = "&Save" Then Me.chkChecked.Checked = False
                Me.chkChecked.Visible = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                Me.btnSearchDelete.Enabled = False ''R934 Added Dlete Button
                Me.btnSearchPrint.Enabled = False  ''R934 Added Print Button

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    'If Rights.Item(i).FormControlName = "View" Then
                    '    Me.Visible = True
                    If RightsDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                        Me.btnSearchDelete.Enabled = True ''R934 Added Dlete Button
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        Me.btnSearchPrint.Enabled = True  ''R934 Added Print Button
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Post" Then
                        If Me.BtnSave.Text = "&Save" Then Me.chkPost.Checked = True
                        Me.chkPost.Visible = True
                        'Task:2826 Apply Security Checked Rights
                    ElseIf RightsDt.FormControlName = "Checked" Then
                        If Me.BtnSave.Text = "&Save" Then Me.chkChecked.Checked = True
                        Me.chkChecked.Visible = True
                        'End Task:2826
                    End If
                Next
                If getConfigValueByType("UpdatePostedVoucher").ToString = "False" Then
                    If Me.BtnSave.Text <> "&Save" Then
                        If Me.grdSaved.GetRow.Cells("Post").Value = True Then
                            Me.BtnSave.Enabled = False
                            'ShowErrorMessage("Your not update this record.")
                            Exit Sub
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function BackendValidation() As Boolean
        Try
            ''Before against ReqId-934 
            'Dim str As String = "Select voucher_id from tblvoucher where voucher_id <> " & Me.txtVoucherID.Text & "  and cheque_no = N'" & Me.txtChequeNo.Text.Trim & "' and voucher_type_id = 5 "
            ''ReqId-934 Resolve Comma Error Problem On Cheque No
            Dim str As String = "Select voucher_id from tblvoucher where voucher_id <> " & Me.txtVoucherID.Text & "  and cheque_no = N'" & Me.txtChequeNo.Text.Replace("'", "''") & "' and voucher_type_id = 5 "
            Dim cmd As New OleDbCommand
            cmd.Connection = Con
            cmd.CommandText = str

            Dim da As New OleDbDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If

        Catch ex As OleDbException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function
    Private Sub ChartsOfAccountToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChartsOfAccountToolStripMenuItem1.Click
        frmMain.LoadControl("ChartOfAccounts")
    End Sub

    Private Sub CashFlowStatmentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashFlowStatmentToolStripMenuItem.Click
        frmMain.LoadControl("rptCashFlow")
    End Sub
    Private Sub StanderCashFlowStatementToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StanderCashFlowStatementToolStripMenuItem1.Click
        frmMain.LoadControl("rptCashFlowStander")
    End Sub
    Private Sub PayablesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PayablesToolStripMenuItem.Click
        frmMain.LoadControl("rptPayables")
    End Sub
    Private Sub ReciveablesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReciveablesToolStripMenuItem.Click
        frmMain.LoadControl("RcvReport")
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try

            If grdSaved.RecordCount > 0 Then Me.EditRecord()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    'Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
    '    If Me.UltraTabControl1.SelectedTab Is Me.UltraTabPageControl2.Tab Then
    '        ToolStripDisplay.Visible = True
    '    Else
    '        ToolStripDisplay.Visible = False
    '    End If

    '    If Me.UltraTabControl1.SelectedTab.Index = 0 Then
    '        Me.BtnLoadAll.Visible = False
    '        Me.btnColapse.Visible = False
    '    Else
    '        Me.BtnLoadAll.Visible = True
    '        Me.btnColapse.Visible = True
    '    End If
    'End Sub


    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If Me.cmbCostCenter.Items.Count > 0 Then Me.cmbCostCenter.SelectedIndex = 0
        FillCombo(Me.cmbCostCenter.Name)

    End Sub

    Private Sub DisplayAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            DisplayRecord(" 1=1 ")
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ToolStripSplitButton1_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DisplayRecord()
    End Sub
    Private Sub BlanceSheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BlanceSheetToolStripMenuItem.Click
        rptDateRange.ReportName = rptDateRange.ReportList.rptBSFomated
        ApplyStyleSheet(rptDateRange)
        rptDateRange.ShowDialog()
    End Sub
    'Private Sub BtnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoadAll.Click
    '    Me.Cursor = Cursors.WaitCursor
    '    Me.DisplayRecord("All")
    '    Me.DisplayDetail(-1)
    '    Me.Cursor = Cursors.Default
    'End Sub
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click, Button11.Click
        'If Not msg_Confirm(str_ConfirmRefresh) = True Then Exit Sub
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Dim id As Integer = 0

        id = Me.cmbVoucherType.SelectedValue
        FillCombo("VType")
        Me.cmbVoucherType.SelectedValue = id

        id = Me.cmbACHead.SelectedValue
        FillCombo("Head")
        Me.cmbACHead.SelectedValue = id

        id = Me.cmbAccount.SelectedRow.Cells(0).Value
        FillCombo("Account")
        Me.cmbAccount.Value = id

        id = Me.cmbCostCenter.SelectedValue
        FillCombo(Me.cmbCostCenter.Name)
        Me.cmbCostCenter.SelectedValue = id

        id = Me.cmbCMFADoc.SelectedIndex
        FillCombo("CMFA")
        Me.cmbCMFADoc.SelectedIndex = id

        id = Me.cmbAccountTitle.SelectedRow.Cells(0).Value
        FillCombo("SearchAccount")
        Me.cmbAccountTitle.Value = id

        'Task#05082015 fill combo box with company names
        FillCombo("Company")
        'End Task#05082015


        If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            flgCompanyRights = getConfigValueByType("CompanyRights")
        End If
        If Not IsDBNull(getConfigValueByType("AccountHeadReadOnly")) Or Not getConfigValueByType("AccountHeadReadOnly").ToString = "Error" Then
            AccountHeadReadOnly = Convert.ToBoolean(getConfigValueByType("AccountHeadReadOnly"))
        End If
        If Not getConfigValueByType("EnableChequeDetailOnVoucherEntry").ToString = "Error" Then
            EnableChequeDetail = getConfigValueByType("EnableChequeDetailOnVoucherEntry")
        End If

        Me.lblProgress.Visible = False
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click, Button5.Click
        'ContextMenuStrip4.Show(Button5, 0, Button5.Height)

        'ApplyStyleSheet(frmAddNewAccount)
        'frmAddNewAccount.ShowDialog()
    End Sub
    Private Sub GeneralToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GeneralToolStripMenuItem.Click
        FrmAddCustomers.FormType = "General"
        ApplyStyleSheet(FrmAddCustomers)
        FrmAddCustomers.ShowDialog()
    End Sub
    Private Sub CashToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashToolStripMenuItem.Click
        FrmAddCustomers.FormType = "Cash"
        ApplyStyleSheet(FrmAddCustomers)
        FrmAddCustomers.ShowDialog()
    End Sub
    Private Sub BankToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankToolStripMenuItem.Click
        FrmAddCustomers.FormType = "Bank"
        ApplyStyleSheet(FrmAddCustomers)
        FrmAddCustomers.ShowDialog()
    End Sub
    Private Sub CustomerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomerToolStripMenuItem.Click
        FrmAddCustomers.FormType = "Customer"
        ApplyStyleSheet(FrmAddCustomers)
        FrmAddCustomers.ShowDialog()
    End Sub
    Private Sub VendorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VendorToolStripMenuItem.Click
        FrmAddCustomers.FormType = "Vendor"
        ApplyStyleSheet(FrmAddCustomers)
        FrmAddCustomers.ShowDialog()
    End Sub
    Private Sub ExpenseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpenseToolStripMenuItem.Click
        FrmAddCustomers.FormType = "Expense"
        ApplyStyleSheet(FrmAddCustomers)
        FrmAddCustomers.ShowDialog()
    End Sub
    Private Sub cmbAccount_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccount.Leave
        Try
            If Me.cmbAccount.IsItemInList = False Then Exit Sub
            If cmbAccount.ActiveRow.Cells(0).Value > 0 Then
                Try
                    Dim objCommand As New OleDbCommand("SELECT SUM(tblVoucherDetail.debit_amount) - SUM(tblVoucherDetail.credit_amount) FROM tblVoucherDetail INNER JOIN tblVoucher On tblVoucherDetail.Voucher_Id = tblVoucher.Voucher_Id " _
                                                                           & " WHERE tblVoucherDetail.coa_detail_id = " & Me.cmbAccount.Value _
                                                                            & " AND tblVoucher.Post=1", Con)
                    If Con.State = ConnectionState.Closed Or Con.State = ConnectionState.Broken Then Con.Open()
                    objCommand.Connection = Con

                    Me.txtCurrentBalance.Text = Math.Round(Val(objCommand.ExecuteScalar), 0)
                    If Val(txtCurrentBalance.Text) < 0 Then
                        Me.txtCurrentBalance.Text = "(" & Replace(Me.txtCurrentBalance.Text, "-", "") & ")"
                    End If

                Catch ex As Exception
                    txtCurrentBalance.Text = 0
                End Try
            Else
                txtCurrentBalance.Text = 0
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GridApplySetting(Optional ByVal Condition As String = "")
        Try
            Me.grd.AutomaticSort = False
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                'R@!    11-Jun-2016 Dollor Account
                ' Added Currency Dr, Cr columns in filter
                'If col.Index <> EnumGrid.Description AndAlso col.Index <> EnumGrid.CostCenterID AndAlso col.Index <> EnumGrid.Cheque_No AndAlso col.Index <> EnumGrid.Cheque_Date AndAlso col.Index <> EnumGrid.CurrencyAmount AndAlso col.Index <> EnumGrid.CurrencyRate Then
                If col.Index <> EnumGrid.Description AndAlso col.Index <> EnumGrid.CostCenterID AndAlso col.Index <> EnumGrid.Cheque_No AndAlso col.Index <> EnumGrid.Cheque_Date AndAlso col.Index <> EnumGrid.CurrencyDr AndAlso col.Index <> EnumGrid.CurrencyCr AndAlso col.Index <> EnumGrid.CurrencyRate Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            'R@!    11-Jun-2016 Dollor Account
            ' Commented previous code
            'Me.grd.RootTable.Columns("CurrencyAmount").Caption = "C Amount"
            ' Added Currency Dr, Cr columns
            Me.grd.RootTable.Columns(EnumGrid.CurrencyDr).Caption = Me.cmbCurrency.Text & " Dr"
            Me.grd.RootTable.Columns(EnumGrid.CurrencyCr).Caption = Me.cmbCurrency.Text & " Cr"

            Me.grd.RootTable.Columns("Debit").Caption = "Debit"
            Me.grd.RootTable.Columns("Credit").Caption = "Credit"
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grd_CellUpdated(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CostCenterGrdCombo()
        Try
            Dim strSQL As String = String.Empty
            Dim dt As New DataTable("Cost")
            strSQL = "Select CostCenterId, Name AS CostCenter From tblDefCostCenter "
            If blnEditMode = False Then
                strSQL += " WHERE Active=1"
            Else
                strSQL += " WHERE Active IN (1,0,NULL)"
            End If
            strSQL += " Union Select 0, '...Select any value...' ORDER BY Name"
            dt = GetDataTable(strSQL)
            dt.TableName = "Cost"
            If Me.grd.RowCount > 0 Then
                Me.grd.RootTable.Columns(EnumGrid.CostCenterID).ValueList.PopulateValueList(dt.DefaultView, "CostCenterId", "CostCenter")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd.Leave
        Me.grd.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.NextRow
    End Sub
    Private Sub grd_EndCustomEdit(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.EndCustomEditEventArgs) Handles grd.EndCustomEdit
        Try
            Me.grd.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.NextRow
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub FillComboByEditMode()
    '    Try
    '        FillCombo("Account")
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Private Sub CostCenterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CostCenterToolStripMenuItem.Click
        Try
            Dim id As Integer = 0
            frmAddCostCenter.ShowDialog()
            id = Me.cmbCostCenter.SelectedValue
            FillCombo(Me.cmbCostCenter.Name)
            Me.cmbCostCenter.SelectedValue = id
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                If Not Val(Me.grd.GetRow.Cells("VoucherDetailId").Value.ToString) > 0 Then
                    Me.grd.GetRow.Delete()
                    grd.UpdateData()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd.DoubleClick
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            Row_Index = Me.grd.CurrentRow.RowIndex 'Me.grd.GetRow.Cells("AccountId").Row.RowIndex
            Me.cmbACHead.Text = Me.grd.GetRow.Cells("Head").Text.ToString
            Me.cmbAccount.Value = Me.grd.GetRow.Cells("AccountId").Value
            Me.txtDescription.Text = Me.grd.GetRow.Cells("Description").Text.ToString
            Me.cmbCostCenter.SelectedValue = Me.grd.GetRow.Cells("CostCenterId").Value
            Me.txtDebit.Text = Val(Me.grd.GetRow.Cells("Debit").Value)
            Me.txtCredit.Text = Val(Me.grd.GetRow.Cells("Credit").Value)

            Dim rowCol As New Janus.Windows.GridEX.GridEXFormatStyle
            rowCol.BackColor = Color.AntiqueWhite
            Me.grd.CurrentRow.RowStyle = rowCol

            Me.grd.Enabled = False
            Me.btnAdd.Visible = False
            Me.btnReplace.Visible = True
            Me.btnCancel.Visible = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim dtGrd As DataTable = CType(Me.grd.DataSource, DataTable)
            dtGrd.Rows.RemoveAt(Row_Index)
            dtGrd.AcceptChanges()
            Dim dr As DataRow
            dr = dtGrd.NewRow
            dr.Item(EnumGrid.Head) = Me.cmbACHead.Text
            dr.Item(EnumGrid.Account) = Me.cmbAccount.Text
            dr.Item(EnumGrid.Description) = Me.txtDescription.Text
            dr.Item(EnumGrid.CostCenterID) = IIf(Me.cmbCostCenter.SelectedIndex > 0, Me.cmbCostCenter.SelectedValue, 0)
            dr.Item(EnumGrid.Debit) = IIf(Me.txtDebit.Text = "", 0, Me.txtDebit.Text)
            dr.Item(EnumGrid.Credit) = IIf(Me.txtCredit.Text = "", 0, Me.txtCredit.Text)
            dr.Item(EnumGrid.AccountID) = Me.cmbAccount.ActiveRow.Cells(0).Value
            dtGrd.Rows.InsertAt(dr, Row_Index)
            CostCenterGrdCombo()
            GetTotal()
            Me.grd.Enabled = True
            Me.btnAdd.Visible = True
            Me.btnCancel.Visible = False
            Me.btnReplace.Visible = False
            ClearDetailControls()
            cmbACHead.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ClearDetailControls()
            cmbACHead.Focus()
            Me.btnCancel.Visible = False
            Me.btnReplace.Visible = False
            Me.btnAdd.Visible = True
            Me.grd.Enabled = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub grdSaved_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.ColumnButtonClick
    '    Try
    '        If e.Column.Key = "Column2" Then
    '            'GetMethod = Me.grdSaved.GetRow.Cells("Script").Text.ToString
    '            frmMain.LoadControl("RecordSales")
    '            frmSales.GetDetail(Me.grdSaved.GetRow.Cells("Voucher_No").Text)

    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub grdSaved_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdSaved.MouseUp
        Try

            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If Me.grdSaved.GetRow.Cells(EnumGridMaster.Selector).Value = True Then
                Dim RowCol As New Janus.Windows.GridEX.GridEXFormatStyle
                RowCol.BackColor = Color.LightYellow
                Me.grdSaved.CurrentRow.RowStyle = RowCol
            ElseIf Me.grdSaved.GetRow.Cells(EnumGridMaster.Selector).Value = False Then
                Dim RowCol As New Janus.Windows.GridEX.GridEXFormatStyle
                RowCol.BackColor = Color.White
                Me.grdSaved.CurrentRow.RowStyle = RowCol
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintVoucherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintVoucherToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub

            Dim checkRows As Integer = Me.grdSaved.GetCheckedRows.Length.ToString
            Dim rows As Integer = Me.grdSaved.GetRows.Length.ToString
            If checkRows > 0 Then


                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows

                    PrintVoucherBC(r.Cells(EnumGridMaster.Voucher_Id).Value, r.Cells("Voucher_No").Value.ToString)

                    ''For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetRows
                    ''If r.Cells(EnumGridMaster.Selector).Value = True Then

                    'Dim DT As New DataTable
                    'DT = DTFromGrid(r.Cells(EnumGridMaster.Voucher_Id).Value) 'GetDataTable("SP_RptVoucher " & r.Cells(EnumGridMaster.Voucher_Id).Value & "")
                    'DT.AcceptChanges()


                    ''   AddRptParam("@VoucherId", r.Cells("voucher_id").Value)
                    'ShowReport("rptVoucher", , , , , , , DT)
                    'PrintLog = New SBModel.PrintLogBE
                    'PrintLog.DocumentNo = r.Cells("Voucher_No").Value.ToString
                    'PrintLog.UserName = LoginUserName
                    'PrintLog.PrintDateTime = Date.Now
                    'Call SBDal.PrintLogDAL.PrintLog(PrintLog)

                Next
            Else
                PrintVoucherBC(Me.grdSaved.GetRow.Cells(EnumGridMaster.Voucher_Id).Value, Me.grdSaved.GetRow.Cells("Voucher_No").Value.ToString())
                'Dim DT As New DataTable
                'DT = DTFromGrid(r.Cells(EnumGridMaster.Voucher_Id).Value) 'GetDataTable("SP_RptVoucher " & r.Cells(EnumGridMaster.Voucher_Id).Value & "")
                'DT.AcceptChanges()


                ''   AddRptParam("@VoucherId", r.Cells("voucher_id").Value)
                'ShowReport("rptVoucher", , , , , , , DT)
                'PrintLog = New SBModel.PrintLogBE
                'PrintLog.DocumentNo = r.Cells("Voucher_No").Value.ToString
                'PrintLog.UserName = LoginUserName
                'PrintLog.PrintDateTime = Date.Now
                'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            End If

            'If Not Me.TabControl1.SelectedIndex = Me.TabControl1.TabCount - 1 Then Me.TabControl1.SelectedIndex = Me.TabControl1.SelectedIndex + 1
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)

        Finally
            Me.Cursor = Cursors.Default
        End Try
        'Me.Cursor = Cursors.WaitCursor
        'Try
        '    If Me.grdSaved.RowCount = 0 Then Exit Sub
        '    'Task Against Request No 798
        '    'ShowReport("rptVoucher", "{VwGlVoucherSingle.VoucherId}=" & Me.grdSaved.CurrentRow.Cells(EnumGridMaster.Voucher_Id).Value & "")
        '    AddRptParam("@VoucherId", Me.grdSaved.CurrentRow.Cells(EnumGridMaster.Voucher_Id).Value)
        '    ShowReport("rptVoucher")
        '    PrintLog = New SBModel.PrintLogBE
        '    PrintLog.DocumentNo = grdSaved.GetRow.Cells("Voucher_No").Value.ToString
        '    PrintLog.UserName = LoginUserName
        '    PrintLog.PrintDateTime = Date.Now
        '    Call SBDal.PrintLogDAL.PrintLog(PrintLog)
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'Finally
        '    Me.Cursor = Cursors.Default
        'End Try
    End Sub
    Private Sub PrintVoucherBC(ByVal voucherID As Integer, Optional ByVal voucherNo As String = Nothing, Optional print As Boolean = False) 'TASK42

        Dim DT As New DataTable
        DT = DTFromGrid(voucherID) 'GetDataTable("SP_RptVoucher " & r.Cells(EnumGridMaster.Voucher_Id).Value & "")
        DT.AcceptChanges()

        If Val(DT.Rows(0).Item("CurrencyId").ToString) > 0 AndAlso Val(DT.Rows(0).Item("CurrencyId").ToString) <> Me.BaseCurrencyId Then
            AddRptParam("BaseCurrency", Me.BaseCurrencyName)
            ShowReport("rptVoucherMultiCurrency", , , , print, , , DT)

        Else

            '   AddRptParam("@VoucherId", r.Cells("voucher_id").Value)
            ShowReport("rptVoucher", , , , print, , , DT)

        End If
        PrintLog = New SBModel.PrintLogBE
        PrintLog.DocumentNo = voucherNo 'r.Cells("Voucher_No").Value.ToString
        PrintLog.UserName = LoginUserName
        PrintLog.PrintDateTime = Date.Now
        Call SBDal.PrintLogDAL.PrintLog(PrintLog)

    End Sub
    Private Sub PrintSelectedVoucherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintSelectedVoucherToolStripMenuItem.Click
        'Try
        '    If Me.grdSaved.RowCount = 0 Then Exit Sub



        '    For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
        '        If r.Cells(EnumGridMaster.Selector).Value = True Then

        '            Dim DT As New DataTable
        '            DT = DTFromGrid(r.Cells(EnumGridMaster.Voucher_Id).Value) 'GetDataTable("SP_RptVoucher " & r.Cells(EnumGridMaster.Voucher_Id).Value & "")
        '            DT.AcceptChanges()


        '            AddRptParam("@VoucherId", r.Cells("voucher_id").Value)
        '            ShowReport("rptVoucher", , , , , , , DT)
        '            PrintLog = New SBModel.PrintLogBE
        '            PrintLog.DocumentNo = r.Cells("Voucher_No").Value.ToString
        '            PrintLog.UserName = LoginUserName
        '            PrintLog.PrintDateTime = Date.Now
        '            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
        '        End If
        '    Next

        '    'If Not Me.TabControl1.SelectedIndex = Me.TabControl1.TabCount - 1 Then Me.TabControl1.SelectedIndex = Me.TabControl1.SelectedIndex + 1
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical)


        'End Try
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                'If r.Cells(EnumGridMaster.Selector).Value = True Then
                'ShowReport("rptVoucher", "{VwGlVoucherSingle.VoucherId}=" & r.Cells(EnumGridMaster.Voucher_Id).Value & "", "Nothing", "Nothing", True)
                'Change Against Request No 798
                'If getConfigValueByType("") Then
                If getConfigValueByType("PreviewInvoice") Then
                    PrintVoucherBC(r.Cells("voucher_id").Value, , False) ''TASKTFS147
                Else
                    PrintVoucherBC(r.Cells("voucher_id").Value, , True) ''TASKTFS147
                End If


                'AddRptParam("@VoucherId", r.Cells("voucher_id").Value)
                'ShowReport("rptVoucher", , "", "Nothing", False)
                'PrintLog = New SBModel.PrintLogBE
                'PrintLog.DocumentNo = r.Cells("Voucher_No").Value.ToString
                'PrintLog.UserName = LoginUserName
                'PrintLog.PrintDateTime = Date.Now
                'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
                ''End If
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function DTFromGrid(ByVal voucherID As Int32) As DataTable 'TASK42
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Function
            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
            Dim DT As New DataTable
            'DT = GetDataTable("SP_RptVoucher" & voucherID & "") ' r.Cells(EnumGridMaster.Voucher_Id).Value
            DT = GetDataTable("SP_RptVoucherMultiCurrency " & voucherID & "") ' r.Cells(EnumGridMaster.Voucher_Id).Value
            DT.AcceptChanges()
            'DT.Columns.Add("Convert(image, null) as BarCode")
            'Next
            For Each DR As DataRow In DT.Rows
                DR.BeginEdit()
                Dim bcp As New BarcodeProfessional()
                'bcp.Symbology = Symbology.Code39
                bcp.Symbology = Symbology.Code128
                'bcp.Symbology = Symbology.Code93



                bcp.Extended = True
                bcp.DisplayCode = False

                ' bcp.Text=Symbology.
                'bcp.Text = String.Empty
                'bcp.Symbology = Symbology.Code128

                bcp.AddChecksum = False

                'bcp.BarWidth = 3
                'bcp.BarHeight = 0.04F
                'DR.Item("Convert(image, null) as BarCode")
                bcp.Code = "?" & DR.Item("voucher_no").ToString
                'bcp.Code = DR.Item("Employee_Code").ToString
                DR.Item("BarCode") = bcp.GetBarcodeImage(System.Drawing.Imaging.ImageFormat.Png)
                'LoadPicture(DR, "Picture", DR.Item("EmpPicture"))


                DR.EndEdit()


            Next
            Return DT
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Function
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function EmailSave()
        EmailSave = Nothing
        Dim toEmail As String = String.Empty
        Dim flg As Boolean = False
        If IsEmailAlert = True Then
            Dim dtForm As DataTable = GetDataTable("Select ISNULL(EmailAlert,0) as EmailAlert  From tblForm WHERE Form_Name='frmVoucher' AND EmailAlert=1")
            If dtForm.Rows.Count > 0 Then
                flg = True
            Else
                flg = False
            End If
            If flg = True Then
                If AdminEmail <> "" Then
                    Email = New SBModel.Email
                    Email.ToEmail = AdminEmail
                    Email.CCEmail = String.Empty
                    Email.BccEmail = String.Empty
                    Email.Attachment = SourceFile
                    Email.Subject = "" & setVoucherType & " " & setVoucherNo & " "
                    Email.Body = "" & setVoucherType & " " _
                    & " " & IIf(setEditMode = False, "of amount " & Total_Amount & " is made", "of amount " & Previouse_Amount & " is updated to " & Total_Amount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By Softbeats ERP System"
                    Email.Status = "Pending"
                    Call New MailSentDAL().Add(Email)
                End If
            End If
        End If
        SourceFile = String.Empty
        setVoucherNo = String.Empty
        Return EmailSave
    End Function

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub UltraCombo1_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs)

    End Sub

    Private Sub grdSaved_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdSaved.FormattingRow

    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub SplitContainer1_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel1.Paint, UltraTabPageControl1.Paint

    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub cmbSearchAccountType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            FillCombo("SearchAccount")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            DisplayRecord(" 1=1 ")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub btnColapse_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColapse.ButtonClick
    '    If Me.SplitContainer1.Panel1Collapsed = True Then
    '        Me.SplitContainer1.Panel1Collapsed = False
    '    Else
    '        Me.SplitContainer1.Panel1Collapsed = True
    '    End If
    'End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Me.dtpfrom.Value = Date.Today.AddMonths(-1)
            Me.dtpto.Value = Date.Now
            Me.dtpfrom.Checked = False
            Me.dtpto.Checked = False
            Me.cmbAccountTitle.Rows(0).Activate()
            Me.cmbSearchAccountType.SelectedIndex = 0
            Me.cmbVType.SelectedIndex = 0
            Me.TxtSearchVocherno.Text = String.Empty
            Me.TxtfromAmount.Text = String.Empty
            Me.TxtToAmount.Text = String.Empty
            Me.txtSearchChequeNo.Text = String.Empty
            Me.txtComments.Text = String.Empty
            Me.cmbSearchProject.SelectedIndex = 0
            Me.cmbSource.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Public Function Get_All(ByVal Voucher_No As String)
        Try
            Get_All = Nothing
            If Not Voucher_No.Length > 0 Then Exit Function 'Task:2780 In the case of empty
            If IsFormLoaded = True Then
                'Dim dt As DataTable = GetDataTable("Select * From tblVoucher WHERE Voucher_No=N'" & Voucher_No & "'")
                'If dt IsNot Nothing Then
                '    If dt.Rows.Count > 0 Then


                '        blnEditMode = True
                '        Me.txtVoucherID.Text = dt.Rows(0).Item("Voucher_Id")
                '        'Me.txtChequeNo.Text = dt.Rows(0).Item("cheque_no").ToString
                '        'If Not IsDBNull(dt.Rows(0).Item("cheque_date")) Then
                '        '    If dt.Rows(0).Item("cheque_date") <> DateTime.MinValue Then
                '        '        Me.dtpChequeDate.Value = dt.Rows(0).Item("cheque_date")
                '        '    End If
                '        'End If
                '        cmbVoucherType.SelectedValue = dt.Rows(0).Item("Voucher_Type_Id").ToString     'Payment Method
                '        txtVoucherNo.Text = dt.Rows(0).Item("Voucher_No").ToString  'VoucherNo
                '        Me.dtpVoucherDate.Value = dt.Rows(0).Item("Voucher_Date").ToString  'VoucherDate
                '        'cmbCashAccount.SelectedValue = grdVouchers.CurrentRow.Cells(6).Value.ToString 'Paid To
                '        'Me.cmbBank.Text = dt.Rows(0).Item("BankDesc").Text.ToString
                '        Me.DisplayDetail(Val(Me.txtVoucherID.Text))
                '        'Me.cmbVoucherType.Enabled = False
                '        If getConfigValueByType("ChangeVoucherType").ToString.ToUpper <> "ERROR" Then
                '            If getConfigValueByType("ChangeVoucherType").ToString = "True" Then
                '                Me.cmbVoucherType.Enabled = True
                '            Else
                '                Me.cmbVoucherType.Enabled = False
                '            End If
                '        End If
                '        'Me.cmbCashAccount.Enabled = False
                '        Me.txtChequeNo.Enabled = True
                '        Me.dtpChequeDate.Enabled = True
                '        Me.chkPost.Checked = dt.Rows(0).Item("Post")
                '        'Me.chkAll.Checked = True
                '        Me.BtnSave.Text = "&Update"
                '        GetSecurityRights()
                '        'Me.GetTotal()
                '        Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
                '        IsDrillDown = True
                '        Me.cmbAccount.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)

                '        If flgDateLock = True Then
                '            If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpVoucherDate.Value.ToString("yyyy-M-d 00:00:00") Then
                '                'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                '                Me.dtpVoucherDate.Enabled = False
                '            Else
                '                Me.dtpVoucherDate.Enabled = True
                '            End If
                '        Else
                '            Me.dtpVoucherDate.Enabled = True
                '        End If

                '    End If
                'End If
                If Me.grdSaved.RowCount <= 50 Then
                    Me.ToolStripButton2_Click(Nothing, Nothing)
                End If
                Me.grdSaved.Find(Me.grdSaved.RootTable.Columns("Voucher_No"), Janus.Windows.GridEX.ConditionOperator.Equal, Voucher_No, 0, 1)
                Me.grdSaved_DoubleClick(Nothing, Nothing)
                IsDrillDown = False
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub grdSaved_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.ColumnButtonClick
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If e.Column.Key = "Detail" Then
                frmMain.Tags = Me.grdSaved.GetRow().Cells("Voucher_No").Text.ToString
                If IsDrillDown = True Then
                    frmMain.LoadControl(Me.grdSaved.GetRow().Cells("AccessKey").Text.ToString)
                Else
                    frmMain.LoadControl(Me.grdSaved.GetRow().Cells("AccessKey").Text.ToString)
                    frmMain.Tags = Me.grdSaved.GetRow().Cells("Voucher_No").Text.ToString
                    frmMain.LoadControl(Me.grdSaved.GetRow().Cells("AccessKey").Text.ToString)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearchEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchEdit.Click
        Try
            OpenToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSearchDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDelete.Click
        Try
            DeleteToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchLoadAll.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.DisplayRecord("All")
            Me.DisplayDetail(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnSearchDocument_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDocument.Click
        Try
            If Not Me.cmbAccountTitle.IsItemInList Then
                FillCombo("SearchAccount")
                Me.cmbAccountTitle.Rows(0).Activate()
            Else
                Me.cmbAccountTitle.Rows(0).Activate()
            End If
            If Not Me.cmbSearchProject.Items.Count > 0 Then
                FillCombo("SearchProject")
                If Not Me.cmbSearchProject.SelectedIndex = -1 Then Me.cmbSearchProject.SelectedIndex = 0
            Else
                If Not Me.cmbSearchProject.SelectedIndex = -1 Then Me.cmbSearchProject.SelectedIndex = 0
            End If
            If Not Me.cmbSource.Items.Count > 0 Then
                FillCombo("Source")
                If Not Me.cmbSource.SelectedIndex = -1 Then Me.cmbSource.SelectedIndex = 0
            Else
                If Not Me.cmbSource.SelectedIndex = -1 Then Me.cmbSource.SelectedIndex = 0
            End If
            If Me.SplitContainer1.Panel1Collapsed = True Then
                Me.SplitContainer1.Panel1Collapsed = False
            Else
                Me.SplitContainer1.Panel1Collapsed = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintVoucherToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintVoucherToolStripMenuItem1.Click
        Try
            PrintVoucherToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintSelectedVoucherToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintSelectedVoucherToolStripMenuItem1.Click
        Try
            PrintSelectedVoucherToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ToolStripSplitButton1_ButtonClick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSplitButton1.ButtonClick
        Me.Cursor = Cursors.WaitCursor
        Try
            DisplayRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub DisplayAllVoucherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisplayAllVoucherToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            DisplayRecord(" 1=1 ")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                'DisplayRecord()
                ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                Me.BtnPrint.Visible = True
                Me.BtnDelete.Visible = True
            ElseIf e.Tab.Index = 0 Then
                If blnEditMode = False Then Me.BtnDelete.Visible = False
                If blnEditMode = False Then Me.BtnPrint.Visible = False
                Me.BtnEdit.Visible = False
            ElseIf e.Tab.Index = 2 Then
                Me.DisplayVoucherTemplates()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbACHead_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbACHead.Enter
        Try
            Me.cmbACHead.DroppedDown = False
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ExportFile(ByVal VoucherId As Integer)
        Try
            If IsEmailAlert = True Then
                If IsAttachmentFile = True Then
                    crpt = New ReportDocument
                    If IO.File.Exists(str_ApplicationStartUpPath & "\Reports\rptVoucher.rpt") = False Then Exit Sub
                    crpt.Load(str_ApplicationStartUpPath & "\Reports\rptVoucher.rpt", DBServerName)
                    If DBUserName <> "" Then
                        crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, DBUserName, DBPassword)
                        crpt.DataSourceConnections.Item(0).SetLogon(DBName, DBPassword)
                    Else
                        crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, True)
                    End If

                    Dim ConnectionInfo As New ConnectionInfo
                    With ConnectionInfo
                        .ServerName = DBServerName
                        .DatabaseName = DBName
                        If DBUserName <> "" Then
                            .UserID = DBUserName
                            .Password = DBPassword
                            .IntegratedSecurity = False
                        Else
                            .IntegratedSecurity = True
                        End If
                    End With
                    Dim tbLogOnInfo As New TableLogOnInfo
                    For Each dt As Table In crpt.Database.Tables
                        tbLogOnInfo = dt.LogOnInfo
                        tbLogOnInfo.ConnectionInfo = ConnectionInfo
                        dt.ApplyLogOnInfo(tbLogOnInfo)
                    Next

                    'crpt.RecordSelectionFormula = "{VwGlVoucherSingle.VoucherId}=" & VoucherId & ""
                    crpt.SetParameterValue("@VoucherId", Me.grdSaved.CurrentRow.Cells(0).Value) '"{VwGlVoucherSingle.VoucherId}=" & VoucherId & ""
                    Dim crExportOps As New ExportOptions
                    Dim crDiskOps As New DiskFileDestinationOptions
                    Dim crExportType As New PdfRtfWordFormatOptions


                    If Not IO.Directory.Exists(str_ApplicationStartUpPath & "\EmailAttachments\") Then
                        IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\EmailAttachments\")
                    Else
                    End If
                    FileName = String.Empty
                    FileName = "Voucher" & "-" & setVoucherNo & ""
                    SourceFile = String.Empty
                    SourceFile = _FileExportPath & "\" & FileName & ".pdf"
                    crDiskOps.DiskFileName = SourceFile
                    crExportOps = crpt.ExportOptions
                    With crExportOps
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                        .ExportDestinationOptions = crDiskOps
                        .ExportFormatOptions = crExportType
                    End With
                    'crpt.Refresh()
                    crpt.Export()

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            ExportFile(GetVoucherId)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            EmailSave()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdSaved_LoadingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdSaved.LoadingRow
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
            If e.Row.Cells(EnumGridMaster.Selector).Value = True Then
                rowStyle.BackColor = Color.LightYellow
                e.Row.RowStyle = rowStyle
            ElseIf e.Row.Cells(EnumGridMaster.Selector).Value = False Then
                rowStyle.BackColor = Color.White
                e.Row.RowStyle = rowStyle
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraSpellChecker1_SpellCheckDialogClosed(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinSpellChecker.SpellCheckDialogClosedEventArgs)

    End Sub
    Private Sub txtDescription_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtDescription.Validating
        Try
            SpellChecker(Me.txtDescription)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub DebitCreditNoteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DebitCreditNoteToolStripMenuItem.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("Voucher_No").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            ShowReport("rptVoucherNote", "{VwGlVoucherSingle.VoucherId}=" & Me.grdSaved.CurrentRow.Cells(EnumGridMaster.Voucher_Id).Value & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub DebitCreditNoteToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DebitCreditNoteToolStripMenuItem1.Click
        Try
            DebitCreditNoteToolStripMenuItem_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnReminder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReminder.Click, Button10.Click
        Try
            Dim frm As New frmreminder
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            frm.txtSubject.Text = "" & IIf(Me.cmbVoucherType.Text = "Cash", "Cash Receipt", "Bank Receipt") & " " & Me.grdSaved.GetRow.Cells("Voucher_No").Text & " "
            frm.txtmessage.Text = "" & IIf(setVoucherType = "Cash", "Cash Receipt", "Bank Receipt") & " " _
                    & " " & IIf(setEditMode = False, "of amount " & Me.grdSaved.GetRow.Cells("Amount").Value & " is made", "of amount " & Me.grdSaved.GetRow.Cells("Amount").Value & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By Softbeats ERP System"
            frm.ShowDialForm = True
            frm.ComboBox1.Text = "Only My"
            frm.ShowDialog()
            frm.Close()
            Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnReminder1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReminder1.Click
        Try
            btnReminder_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.dtpVoucherDate.Value.ToString("yyyy-M-d 00:00:00") Then
                If DateLock.Lock = True Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Sub ValidateDateLock()
        Try
            Dim dateLock As New SBModel.DateLockBE
            dateLock = DateLockList.Find(AddressOf chkDateLock)
            If dateLock IsNot Nothing Then
                If dateLock.DateLock.ToString.Length > 0 Then
                    flgDateLock = True
                Else
                    flgDateLock = False
                End If
            Else
                flgDateLock = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BtnPrint_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.ButtonClick, Button9.Click
        ' ContextMenuStrip2.Show(Button9, 0, Button9.Height)
        Me.Cursor = Cursors.WaitCursor
        Try
            'If Me.grdSaved.RowCount = 0 Then Exit Sub
            'PrintLog = New SBModel.PrintLogBE
            'PrintLog.DocumentNo = grdSaved.GetRow.Cells("Voucher_No").Value.ToString
            'PrintLog.UserName = LoginUserName
            'PrintLog.PrintDateTime = Date.Now
            'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            ''Task Against Request No 798
            ''ShowReport("rptVoucher", "{VwGlVoucherSingle.VoucherId}=" & Me.grdSaved.CurrentRow.Cells(EnumGridMaster.Voucher_Id).Value & "")
            'AddRptParam("@VoucherId", Me.grdSaved.CurrentRow.Cells(EnumGridMaster.Voucher_Id).Value)
            'ShowReport("rptVoucher")
            PrintVoucherBC(Me.grdSaved.GetRow.Cells("voucher_id").Value, Me.grdSaved.GetRow.Cells("Voucher_No").Value.ToString())
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub PrintReceiptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintReceiptToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("Voucher_No").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            'ShowReport("rptVoucher", "{VwGlVoucherSingle.VoucherId}=" & Me.grdVouchers.CurrentRow.Cells(0).Value & "")
            'Chaning Against Request No 798
            AddRptParam("@vid", Me.grdSaved.CurrentRow.Cells("Voucher_Id").Value)
            ShowReport("rptCashReceipt")
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub PrintPaymentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPaymentToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("Voucher_No").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            'ShowReport("rptVoucher", "{VwGlVoucherSingle.VoucherId}=" & Me.grdVouchers.CurrentRow.Cells(0).Value & "")
            'Chaning Against Request No 798
            AddRptParam("@vid", Me.grdSaved.CurrentRow.Cells("Voucher_Id").Value)
            ShowReport("rptCashPayment")
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub PrintReceiptToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintReceiptToolStripMenuItem1.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("Voucher_No").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            'ShowReport("rptVoucher", "{VwGlVoucherSingle.VoucherId}=" & Me.grdVouchers.CurrentRow.Cells(0).Value & "")
            'Chaning Against Request No 798
            AddRptParam("@vid", Me.grdSaved.CurrentRow.Cells("Voucher_Id").Value)
            ShowReport("rptCashReceipt")
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub PrintPaymentToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPaymentToolStripMenuItem1.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@vid", Me.grdSaved.CurrentRow.Cells("Voucher_Id").Value)
            ShowReport("rptCashPayment")
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("Voucher_No").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 31-12-13
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
        'TASK:2398           Imran Ali        Update, Delete Problem in Cash Management
        'If e.KeyCode = Keys.Delete Then
        '    DeleteToolStripButton_Click(Nothing, Nothing)
        '    Exit Sub
        'End If
        If e.KeyCode = Keys.F5 Then
            BtnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnSearchPrint_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPrint.ButtonClick

    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.grdSaved.RowCount <= 0 Then Exit Sub
            DeleteToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
    ''Task:2400 Attach Multi Files In Voucher Entry
    Private Sub btnAttachmentFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachmentFile.Click

        Try
            Dim OpenFile As New OpenFileDialog
            OpenFile.FileName = String.Empty
            OpenFile.Multiselect = True
            Dim strFileName As String = String.Empty
            Dim FilesEnum As IEnumerator
            If OpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then
                FilesEnum = OpenFile.FileNames.GetEnumerator()
                Dim FilePath As String = getConfigValueByType("FileAttachmentPath").ToString
                Dim i As Integer = 1I
                While FilesEnum.MoveNext
                    If strFileName.Length > 0 Then
                        strFileName += ";" & grdSaved.CurrentRow.Cells(1).Value.ToString & "_" & i & "." & FilesEnum.Current.ToString.Substring(FilesEnum.Current.ToString.LastIndexOf(".") + 1)
                    Else
                        strFileName = grdSaved.CurrentRow.Cells(1).Value.ToString & "_" & i & "." & FilesEnum.Current.ToString.Substring(FilesEnum.Current.ToString.LastIndexOf(".") + 1)
                    End If
                    If IO.Directory.Exists(FilePath) Then
                        IO.File.Copy(FilesEnum.Current.ToString, FilePath & "\" & grdSaved.CurrentRow.Cells(1).Value.ToString & "_" & i & "." & FilesEnum.Current.ToString.Substring(FilesEnum.Current.ToString.LastIndexOf(".") + 1), True)
                    End If
                    i += 1
                End While

                If strFileName.Length > 0 AndAlso strFileName.Length <= 8000 Then
                    Dim cmd As New OleDbCommand
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    Dim objTrans As OleDbTransaction = Con.BeginTransaction
                    Try
                        cmd.Connection = Con
                        cmd.Transaction = objTrans
                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = "Update tblVoucher Set Attachment=N'" & strFileName & "' WHERE Voucher_Id=" & grdSaved.CurrentRow.Cells(5).Value
                        cmd.ExecuteNonQuery()
                        objTrans.Commit()
                    Catch ex As Exception
                        objTrans.Rollback()
                        Dim strFiles() As String = strFileName.Split(";")
                        For Each strFile As String In strFiles
                            If IO.File.Exists(FilePath & "\" & strFile) Then
                                IO.File.Delete(FilePath & "\" & strFile)
                            End If
                        Next
                        Throw ex
                    Finally
                        Con.Close()
                    End Try
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2400
    ''Task:2400 Attach Multi Files In Voucher Entry
    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Try
            Dim frmDownload As New Form
            frmDownload.Text = "Download File"
            frmDownload.StartPosition = FormStartPosition.CenterParent
            frmDownload.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
            ApplyStyleSheet(frmDownload)
            Dim lstBox As New ListBox
            lstBox.Dock = DockStyle.Fill
            frmDownload.Controls.Add(lstBox)
            Dim strFiles As New List(Of String)
            For Each str As String In Me.grdSaved.GetRow.Cells("Attachment").Value.ToString.Split(";")
                strFiles.Add(str)
            Next
            If strFiles IsNot Nothing Then
                lstBox.DataSource = strFiles
            End If
            AddHandler lstBox.SelectedIndexChanged, AddressOf lstBox_SelectedIndex
            frmDownload.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2400
    ''Task:2400 Attach Multi Files In Voucher Entry
    Public Sub lstBox_SelectedIndex(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.IsFormLoaded = False Then Exit Sub
            Dim lst As ListBox = CType(sender, ListBox)
            If lst.SelectedIndex = -1 Then Exit Sub
            Dim FilePath As String = getConfigValueByType("FileAttachmentPath").ToString
            Dim FolderDialog As New FolderBrowserDialog
            If FolderDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                If IO.File.Exists(FilePath & "\" & lst.Text.ToString) Then
                    My.Computer.Network.DownloadFile(FilePath & "\" & lst.Text.ToString, FolderDialog.SelectedPath & "\" & lst.Text.ToString, "", "", True, 120, True)
                    ShowInformationMessage("Downloading Completed.")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2400

    Private Sub txtDebit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDebit.TextChanged
        Try
            ''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
            If CheckNumericValue(Me.txtDebit.Text, Me.txtDebit) = False Then
                Throw New Exception("Amount is not valid.")
            End If
            'End Task:2491
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCredit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCredit.TextChanged
        Try
            ''14-Mar-2014 TASK:2491  Imran Ali  Numeric Validation 
            If CheckNumericValue(Me.txtCredit.Text, Me.txtCredit) = False Then
                Throw New Exception("Amount is not valid.")
            End If
            'End Task:2491
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton2_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            ' Dim frm As New frmFileview
            'For Each str As String In Me.grdSaved.GetRow.Cells("Attachment").Value.ToString.Split(";")
            'If Str.Length > 0 Then
            'frm.strFileList.Add(Str)
            'End If
            'Next
            'frm.ShowDialog()
            'Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''16-Jul-2014 TASK:2745 Imran Ali Cheque Comments On Ledger (Ravi)
    Public Function GetComments(ByVal Row As Janus.Windows.GridEX.GridEXRow) As String
        Try
            Dim str As String = String.Empty
            Dim blnCommentsChequeNo As Boolean = Boolean.Parse(getConfigValueByType("CommentsChequeNo").ToString)
            Dim blnCommentsChequeDate As Boolean = Boolean.Parse(getConfigValueByType("CommentsChequeDate").ToString)
            Dim blnCommentsPartyName As Boolean = Boolean.Parse(getConfigValueByType("CommentsPartyName").ToString)
            If Me.cmbVoucherType.Text = "BRV" Or Me.cmbVoucherType.Text = "BPV" Then
                If Row IsNot Nothing Then
                    If blnCommentsChequeNo = True Then
                        str += " Chq No. " & Row.Cells("Cheque_No").Value.ToString & ""
                    End If
                    If blnCommentsChequeDate = True Then
                        str += " Chq Date. " & Row.Cells("Cheque_Date").Value.ToString & ""
                    End If
                    If blnCommentsPartyName = True Then
                        str += " Party Name. " & Row.Cells("Account").Value.ToString & ""
                    End If
                End If
            End If
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task:2745
    Private Sub btnAttachment_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachment.ButtonClick, Button2.Click
        Try
            '    OpenFileDialog1.FileName = String.Empty
            '    'Marked Against Task#2015060005 
            '    'OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG"
            '    'Altered Against Task#2015060006 to make all files attachement physible
            '    OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
            '    "All files (*.*)|*.*"
            '    'Altered Against Task#2015060006 to make all files attachement physible
            '    If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            '        arrFile = OpenFileDialog1.FileNames
            '        Dim intCountAttachedFiles As Integer = 0I
            '        If Me.BtnSave.Text <> "&Save" Then
            '            If Me.grdSaved.RowCount > 0 Then
            '                intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
            '            End If
            '        End If
            '        Me.btnAttachment.Text = "Attachment (" & arrFile.Length + intCountAttachedFiles & ")"
            '    End If
            'Catch ex As Exception
            '    ShowErrorMessage(ex.Message)
            'End Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty
            'Marked Against Task#2015060005 
            'OpenFileDialog1.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG"
            'Marked Against Task#2015060005 

            '            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG"
            'Altered Against Task#2015060006 to make all files attachement physible
            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
            "All files (*.*)|*.*"
            'Altered Against Task#2015060006 to make all files attachement physible
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                'Altered Against Task#2015060001 Ali Ansari
                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a
                'Altered Against Task#2015060001 Ali Ansari

                If Me.BtnSave.Text <> "&Save" Then
                    If Me.grdSaved.RowCount > 0 Then
                        intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
                    End If
                End If

                'Marked Against Task#2015060001 Ali Ansari
                'Me.btnAttachment.Text = "Attachment (" & arrFile.Length + intCountAttachedFiles & ")"
                'Marked Against Task#2015060001 Ali Ansari
                'Altered Against Task#2015060001 Ali Ansari
                Me.btnAttachment.Text = "Attachment (" & arrFile.Count + intCountAttachedFiles & ")"
                'Altered Against Task#2015060001 Ali Ansari
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function SaveDocument(ByVal DocId As Integer, ByVal Source As String, ByVal objTrans As OleDb.OleDbTransaction) As Boolean
        Dim cmd As New OleDbCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        Try

            'Dim dt As New DataTable
            'dt = GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            'dt.AcceptChanges()


            'Dim objdt As New DataTable
            'objdt = GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            'Dim intId As Integer = objdt.Rows(0)(0)

            'Dim strSQL As String = String.Empty
            'cmd.CommandText = String.Empty
            'strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            'cmd.CommandText = strSQL
            'cmd.ExecuteNonQuery()

            'Dim objPath As String = getConfigValueByType("FileAttachmentPath").ToString

            ''If arrFile.Length > 0 Then

            'For Each objFile As String In arrFile
            '    If IO.File.Exists(objFile) Then
            '        If IO.Directory.Exists(objPath) = False Then
            '            IO.Directory.CreateDirectory(objPath)
            '        End If
            '        Dim New_Files As String = intId & "_" & DocId & "_" & Me.dtpVoucherDate.Value.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
            '        Dim dr As DataRow
            '        dr = dt.NewRow
            '        dr(0) = DocId
            '        dr(1) = Source
            '        dr(2) = objPath & "\" & New_Files
            '        dt.Rows.Add(dr)
            '        dt.AcceptChanges()
            '        If IO.File.Exists(objPath & "\" & New_Files) Then
            '            IO.File.Delete(objPath & "\" & New_Files)
            '        End If
            '        IO.File.Copy(objFile, objPath & "\" & New_Files)
            '        intId += 1
            '    End If
            'Next
            'End If


            'If dt IsNot Nothing Then
            '    If dt.Rows.Count > 0 Then
            '        For Each r As DataRow In dt.Rows
            '            Dim strPath As String = objPath
            '            Dim strFileName As String = r.Item("FileNames").ToString.Substring(r.Item("FileNames").ToString.LastIndexOf("\") + 1)
            '            cmd.CommandText = String.Empty
            '            strSQL = "INSERT INTO DocumentAttachment(DocId, Source, FileName, Path) Values(" & Val(r("DocId").ToString) & ",N'" & r.Item("Source").ToString.Replace("'", "''") & "', N'" & strFileName.Replace("'", "''") & "', N'" & strPath.Replace("'", "''") & "')"
            '            cmd.CommandText = strSQL
            '            cmd.ExecuteNonQuery()
            '        Next
            '    End If
            'End If
            Dim dt As New DataTable
            dt = GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            dt.AcceptChanges()


            Dim objdt As New DataTable
            objdt = GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            Dim intId As Integer = objdt.Rows(0)(0)

            Dim strSQL As String = String.Empty
            cmd.CommandText = String.Empty
            strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            Dim objPath As String = getConfigValueByType("FileAttachmentPath").ToString

            'If arrFile.Length > 0 Then
            If arrFile.Count > 0 Then
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If
                        Dim New_Files As String = intId & "_" & DocId & "_" & Me.dtpVoucherDate.Value.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
                        Dim dr As DataRow
                        dr = dt.NewRow
                        dr(0) = DocId
                        dr(1) = Source
                        dr(2) = objPath & "\" & New_Files
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                        If IO.File.Exists(objPath & "\" & New_Files) Then
                            IO.File.Delete(objPath & "\" & New_Files)
                        End If
                        IO.File.Copy(objFile, objPath & "\" & New_Files)
                        intId += 1
                    End If
                Next
            End If


            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        Dim strPath As String = objPath
                        Dim strFileName As String = r.Item("FileNames").ToString.Substring(r.Item("FileNames").ToString.LastIndexOf("\") + 1)
                        cmd.CommandText = String.Empty
                        strSQL = "INSERT INTO DocumentAttachment(DocId, Source, FileName, Path) Values(" & Val(r("DocId").ToString) & ",N'" & r.Item("Source").ToString.Replace("'", "''") & "', N'" & strFileName.Replace("'", "''") & "', N'" & strPath.Replace("'", "''") & "')"
                        cmd.CommandText = strSQL
                        cmd.ExecuteNonQuery()
                    Next
                End If
            End If


        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function
    Private Sub PrintAttachmentVoucherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintAttachmentVoucherToolStripMenuItem.Click, PrintAttachmentVoucherToolStripMenuItem1.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            'AddRptParam("Pm-dtVoucher.Voucher_Id", Me.grdVouchers.GetRow.Cells(0).Value)
            DataSetShowReport("RptVoucherDocument", GetVoucherRecord())
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function GetVoucherRecord() As DataSet
        Try

            Dim strSQL As String = String.Empty
            Dim ds As New dsVoucherDocumentAttachment
            ds.Tables.Clear()
            strSQL = "SELECT  TOP 100 PERCENT V.voucher_id, V.voucher_no, V.voucher_date, V.voucher_code, VTP.voucher_type, V.Reference, V.post, V.BankDesc, V.UserName, " _
                    & " V.Posted_UserName, V.CheckedByUser, V.Checked, VD.voucher_detail_id, VD.coa_detail_id, COA.detail_code, COA.detail_title, VD.comments, VD.debit_amount,  " _
                    & " VD.credit_amount, VD.sp_refrence, VD.direction, VD.CostCenterID, VD.Adjustment, VD.Cheque_No, VD.Cheque_Date, VD.BankDescription, VD.Tax_Percent,  " _
                    & " VD.Tax_Amount, VD.Cheque_Clearance_Date, VD.PayeeTitle, VD.Cheque_Status, VD.ChequeDescription, COA.sub_sub_code, COA.sub_sub_title " _
                    & " FROM dbo.tblVoucher AS V INNER JOIN " _
                    & " dbo.tblVoucherDetail AS VD ON V.voucher_id = VD.voucher_id INNER JOIN " _
                    & " dbo.vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN  " _
                    & " dbo.tblDefVoucherType AS VTP ON V.voucher_type_id = VTP.voucher_type_id WHERE (V.voucher_id=" & Me.grdSaved.GetRow.Cells("voucher_id").Value & ") " _
                    & " ORDER BY VD.voucher_detail_id "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            ds.Tables.Add(dt)
            ds.Tables(0).TableName = "dtVoucher"


            strSQL = String.Empty
            strSQL = "Select DocId,FileName,Path,Convert(Image,'') as Attachment_Image From DocumentAttachment WHERE (DocId=" & Me.grdSaved.GetRow.Cells("voucher_id").Value & ") AND Source=N'" & Me.Name.Replace("frmVoucherNew", "frmVouchernew") & "' and right(filename,3) in ('BMP','DIB','RLE','JPG','JPEG','JPE','JFIF','GIF','TIF','TIFF','PNG') "
            Dim dtAttach As New DataTable
            dtAttach.TableName = "dtAttachment"
            dtAttach = GetDataTable(strSQL)

            If dtAttach IsNot Nothing Then
                If dtAttach.Rows.Count > 0 Then
                    For Each r As DataRow In dtAttach.Rows
                        r.BeginEdit()
                        LoadPicture(r, "Attachment_Image", CStr(r("Path").ToString & "\" & r("FileName").ToString))
                        r.EndEdit()
                    Next
                End If
            End If

            ds.Tables.Add(dtAttach)
            ds.Tables(1).TableName = "dtAttachment"
            ds.AcceptChanges()

            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''23-Sep-2014 Task:2854 Imran Ali Attachment Count In History And Preview On Payment/Receipt/Voucher
    Private Sub grdSaved_LinkClicked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked

        Try
            If e.Column.Key = "No Of Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = "frmVouchernew"
                frm._VoucherId = Me.grdSaved.GetRow.Cells("Voucher_Id").Value.ToString
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2854

    Private Sub CtrlGrdBar2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub uitxtReference_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

        If Me.txtDescription.Text.Trim.ToString.Length = 0 Then
            Me.txtDescription.Text = uitxtReference.Text
        End If

    End Sub
    Private Sub Btn_SaveAttachment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_SaveAttachment.Click

        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDbTransaction = Con.BeginTransaction

        Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty

            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
                        "All files (*.*)|*.*"

            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a

                If arrFile.Count > 0 Then
                    SaveDocument(Me.grdSaved.CurrentRow.Cells("voucher_id").Value, Me.Name, trans)
                    trans.Commit()
                    DisplayRecord()

                End If

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnUpdatedVoucher_Click(sender As Object, e As EventArgs) Handles btnUpdatedVoucher.Click
        Try
            If grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@VoucherId", Val(Me.grdSaved.GetRow.Cells("Voucher_Id").Value.ToString))
            ShowReport("rptVoucherUpdated")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintUpdateVoucherToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintUpdateVoucherToolStripMenuItem.Click
        Try
            Me.btnUpdatedVoucher_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtMemo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Sub txtReference_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Sub grd_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs)

    End Sub

    Private Sub cmbCashAccount_SelectedValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs)

    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnSMSTemplate_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ExpensesDetailToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PrintUpdatedVoucherToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PrintVoucherAttachmentToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub chkEnableDepositAc_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmbCompany_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmbCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurrency.SelectedIndexChanged
        Try
            If Not Me.cmbCurrency.SelectedItem Is Nothing Then
                Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
                Me.txtCurrencyRate.Text = dr.Row.Item("CurrencyRate").ToString

                ' R@!    11-Jun-2016 Dollor account
                ' Setting default rate to zero
                If Val(Me.txtCurrencyRate.Text) = 0 Then
                    Me.txtCurrencyRate.Text = 1
                End If

                'R@!    11-Jun-2016 Dollor account
                'Code Commented
                ' Me.grd.RootTable.Columns("CurrencyAmount").Caption = "" & Me.cmbCurrency.Text & " Amount"
                ' Added 2 coloumns and changed caption
                Me.grd.RootTable.Columns("CurrencyDr").Caption = "Debit (" & Me.cmbCurrency.Text & ")"
                Me.grd.RootTable.Columns("CurrencyCr").Caption = " Credit (" & Me.cmbCurrency.Text & ")"

                Me.grd.RootTable.Columns("Debit").Caption = "Debit (" & Me.BaseCurrencyName & ")"
                Me.grd.RootTable.Columns("Credit").Caption = "Credit (" & Me.BaseCurrencyName & ")"

                grd.AutoSizeColumns()
                If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                    Me.grd.RootTable.Columns(EnumGrid.Debit).Visible = False
                    Me.grd.RootTable.Columns(EnumGrid.Credit).Visible = False
                Else
                    Me.grd.RootTable.Columns(EnumGrid.Debit).Visible = True
                    Me.grd.RootTable.Columns(EnumGrid.Credit).Visible = True

                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetCurrencyRate(ByVal currencyId As Integer) As Double ''TAKS-407
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Dim currencyRate As Double = 0
        Try
            str = " Select CurrencyRate, CurrencyId From tblCurrencyRate Where CurrencyRateId in ( Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId) And CurrencyId = " & currencyId & ""
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                currencyRate = Val(dt.Rows.Item(0).Item(0).ToString)
            End If

            Return currencyRate

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub grd_CellEdited_1(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellEdited
        ' R@!   11-Jun-2016     Dollor Account
        ' Code commented
        'Try
        '    'If Me.grd.GetRow.Cells(EnumGrid.CurrencyAmount).Value > 0 Then
        '    If Me.grd.GetRow.Cells(EnumGrid.Debit).Value > 0 Or Debit = True Then
        '        Me.grd.GetRow.Cells(EnumGrid.Debit).Value = Me.grd.GetRow.Cells(EnumGrid.CurrencyAmount).Value * Me.grd.GetRow.Cells(EnumGrid.CurrencyRate).Value
        '        'Me.grd.GetRow.Cells(EnumGrid.Credit).Value = 0
        '        Debit = True
        '        Credit = False
        '        'Me.grd.GetRow.Cells(EnumGrid.CurrencyAmount).Value = Me.grd.GetRow.Cells(EnumGrid.Debit).Value / Me.grd.GetRow.Cells(EnumGrid.CurrencyRate).Value
        '    End If
        '    If Me.grd.GetRow.Cells(EnumGrid.Credit).Value > 0 Or Credit = True Then
        '        Me.grd.GetRow.Cells(EnumGrid.Credit).Value = Me.grd.GetRow.Cells(EnumGrid.CurrencyAmount).Value * Me.grd.GetRow.Cells(EnumGrid.CurrencyRate).Value
        '        'Me.grd.GetRow.Cells(EnumGrid.Debit).Value = 0
        '        Credit = True
        '        Debit = False
        '        'Me.grd.GetRow.Cells(EnumGrid.CurrencyAmount).Value = Me.grd.GetRow.Cells(EnumGrid.Credit).Value / Me.grd.GetRow.Cells(EnumGrid.CurrencyRate).Value
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub


    Private Sub SaveAsTemplateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsTemplateToolStripMenuItem.Click, ToolStripMenuItem1.Click

        Try
            If SaveTemplate() Then
                RefreshControls()
                ClearDetailControls()
                DisplayDetail(-1)
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdTemplates_DoubleClick(sender As Object, e As EventArgs) Handles grdTemplates.DoubleClick
        Try
            If grdTemplates.RowCount > 0 AndAlso grdTemplates.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then

                If Me.grd.GetRows.Length > 0 Then
                    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                End If

                Me.DisplayVoucherTemplateDetail(Val(grdTemplates.CurrentRow.Cells("voucher_id").Value.ToString))
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnLoadTemplate_Click(sender As Object, e As EventArgs) Handles btnLoadTemplate.Click
        grdTemplates_DoubleClick(Me, Nothing)
    End Sub

    Private Sub btnDeleteTemplate_Click(sender As Object, e As EventArgs) Handles btnDeleteTemplate.Click

        If Not Me.grdTemplates.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

        Me.lblTemplateProgress.Text = "Processing Please Wait ..."
        Me.lblTemplateProgress.Visible = True
        Application.DoEvents()


        Dim lngVoucherMasterId As Integer = grdTemplates.CurrentRow.Cells("Voucher_ID").Value
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim cm As New OleDbCommand
            Dim objTrans As OleDbTransaction
            If Con.State = ConnectionState.Closed Then Con.Open()
            objTrans = Con.BeginTransaction


            cm = New OleDbCommand
            cm.Connection = Con
            cm.CommandText = "delete from tblvoucherTemplatedetail where voucher_id=" & lngVoucherMasterId

            cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            cm = New OleDbCommand
            cm.Connection = Con
            cm.CommandText = "delete from tblvoucherTemplate where voucher_id=" & lngVoucherMasterId

            cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            objTrans.Commit()

            SaveActivityLog("Accounts", Me.Text & " Template", EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, grdTemplates.CurrentRow.Cells(1).Value & "", True)
            Me.grdTemplates.CurrentRow.Delete()
            Call DisplayVoucherTemplates()

        Catch ex As Exception
            msg_Error("Error occured while deleting record: " & ex.Message)

        Finally
            Me.lblProgress.Visible = False
            Con.Close()
            Me.Cursor = Cursors.Default
        End Try
        Me.RefreshControls()
    End Sub

    Private Sub lblPrintStatus_Click(sender As Object, e As EventArgs)

    End Sub






































































    Private Sub lblVTypeHeading_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmbVoucherType_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cmbVoucherType.SelectedIndexChanged
        Try
            'If Me.BtnSave.Text = "Update" Or Me.BtnSave.Text = "&Update" Then Exit Sub
            Me.lblVTypeHeading.Text = String.Empty
            If Me.cmbVoucherType.SelectedValue = 4 Or Me.cmbVoucherType.SelectedValue = 5 Then
                If Me.cmbVoucherType.Text = "JV" Then
                    If EnableChequeDetail = True Then
                        Me.dtpChequeDate.Visible = False
                        Me.txtChequeNo.Visible = False
                        Me.grd.RootTable.Columns("Cheque_No").Visible = True
                        Me.grd.RootTable.Columns("Cheque_Date").Visible = True
                        Me.Label4.Visible = False
                        Me.Label5.Visible = False
                    End If
                Else
                    Me.dtpChequeDate.Visible = True
                    Me.txtChequeNo.Visible = True
                    Me.grd.RootTable.Columns("Cheque_No").Visible = True
                    Me.grd.RootTable.Columns("Cheque_Date").Visible = True
                    Me.Label4.Visible = True
                    Me.Label5.Visible = True
                End If
            Else
                Me.dtpChequeDate.Visible = False
                Me.txtChequeNo.Visible = False
                Me.grd.RootTable.Columns("Cheque_No").Visible = False
                Me.grd.RootTable.Columns("Cheque_Date").Visible = False
                Me.Label4.Visible = False
                Me.Label5.Visible = False
            End If
            setVoucherType = String.Empty
            Select Case Me.cmbVoucherType.Text
                Case Is = "CPV"
                    setVoucherType = "Cash Payment"
                Case Is = "CRV"
                    setVoucherType = "Cash Receipt"
                Case Is = "BPV"
                    setVoucherType = "Bank Payment"
                Case Is = "BRV"
                    setVoucherType = "Bank Receipt"
                Case Is = "JV"
                    setVoucherType = "Journal Voucher"
                Case Is = "SV"
                    setVoucherType = "Sales Voucher"
                Case Is = "PV"
                    setVoucherType = "Purchase Voucher"
                Case Is = strZeroIndexItem
                    setVoucherType = ""

            End Select


            If BtnSave.Text = "&Update" Or Me.BtnSave.Text = "Update" Then
                If Me.cmbVoucherType.SelectedIndex <> cmbVoucherType.FindStringExact((grdSaved.CurrentRow.Cells(0).Value)) Then
                    Me.txtVoucherNo.Text = GetVoucherNo()
                Else
                    Me.txtVoucherNo.Text = CurrentVoucherNo.ToString
                End If
            Else
                Me.txtVoucherNo.Text = GetVoucherNo()
            End If

            Me.lblVTypeHeading.Text = setVoucherType
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCMFADoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCMFADoc.SelectedIndexChanged

    End Sub

    Private Sub dtpChequeDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpChequeDate.ValueChanged

    End Sub


    Private Sub ToolStripButton1_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripButton1.ButtonClick

    End Sub


    Private Sub Button13_Click(sender As Object, e As EventArgs)
        'ContextMenuStrip1.Show(Button13, 0, Button13.Height)
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub SplitContainer1_Panel2_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub

    Private Sub btnAdd_Click_1(sender As Object, e As EventArgs) Handles btnAdd.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Validate_AddToGrid() Then
                AddItemToGrid()
                GetTotal()
                ClearDetailControls()
                cmbACHead.Focus()
                Me.cmbCurrency.Enabled = False

                ' R@!   11-Jun-2016     Dollor account
                'Me.grd.RootTable.Columns("CurrencyAmount").Caption = "" & Me.cmbCurrency.Text & " Amount"
            End If
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnReplace_Click_1(sender As Object, e As EventArgs) Handles btnReplace.Click
        Try
            Dim dtGrd As DataTable = CType(Me.grd.DataSource, DataTable)
            dtGrd.Rows.RemoveAt(Row_Index)
            dtGrd.AcceptChanges()
            Dim dr As DataRow
            dr = dtGrd.NewRow
            dr.Item(EnumGrid.Head) = Me.cmbACHead.Text
            dr.Item(EnumGrid.Account) = Me.cmbAccount.Text
            dr.Item(EnumGrid.Description) = Me.txtDescription.Text
            dr.Item(EnumGrid.CostCenterID) = IIf(Me.cmbCostCenter.SelectedIndex > 0, Me.cmbCostCenter.SelectedValue, 0)
            dr.Item(EnumGrid.Debit) = IIf(Me.txtDebit.Text = "", 0, Me.txtDebit.Text)
            dr.Item(EnumGrid.Credit) = IIf(Me.txtCredit.Text = "", 0, Me.txtCredit.Text)
            dr.Item(EnumGrid.AccountID) = Me.cmbAccount.ActiveRow.Cells(0).Value
            dtGrd.Rows.InsertAt(dr, Row_Index)
            CostCenterGrdCombo()
            GetTotal()
            Me.grd.Enabled = True
            Me.btnAdd.Visible = True
            Me.btnCancel.Visible = False
            Me.btnReplace.Visible = False
            ClearDetailControls()
            cmbACHead.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click_1(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            ClearDetailControls()
            cmbACHead.Focus()
            Me.btnCancel.Visible = False
            Me.btnReplace.Visible = False
            Me.btnAdd.Visible = True
            Me.grd.Enabled = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub

    Private Sub Panel9_MouseHover(sender As Object, e As EventArgs) Handles Panel9.MouseHover

        Button13.FlatStyle = FlatStyle.Popup
        Button27.FlatStyle = FlatStyle.Popup
    End Sub

    Private Sub Panel9_MouseLeave(sender As Object, e As EventArgs) Handles Panel9.MouseLeave
        Panel9.BorderStyle = BorderStyle.None
        Button13.FlatStyle = FlatStyle.Flat
        Button27.FlatStyle = FlatStyle.Flat
    End Sub

    Private Sub Panel9_Paint(sender As Object, e As PaintEventArgs) Handles Panel9.Paint

    End Sub

    
    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        ContextMenuStrip1.Show(Button27 , 0 , Button27.Height)
    End Sub

    Private Sub cmbAccountTitle_InitializeLayout(sender As Object, e As Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbAccountTitle.InitializeLayout

    End Sub
End Class