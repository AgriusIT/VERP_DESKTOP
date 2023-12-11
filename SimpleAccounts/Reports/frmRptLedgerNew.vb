Public Class frmRptLedgerNew

    Implements IReportsInterface
    Dim IsFormLoaded As Boolean = False
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IReportsInterface.FillCombos
        Try
            Dim str As String = String.Empty
            If Condition = "Accounts" Then
                If Me.cmbAccountLevel.Text = "Main" Then
                    str = "Select coa_main_id as ID, main_title as Account From tblCOAMain"
                    Me.cmbAccounts.DataSource = Nothing
                    FillUltraDropDown(Me.cmbAccounts, str)
                    Me.cmbAccounts.Rows(0).Activate()
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                ElseIf Me.cmbAccountLevel.Text = "Sub" Then
                    str = "Select main_sub_id as ID, sub_title as Account From tblCOAMainSub"
                    Me.cmbAccounts.DataSource = Nothing
                    FillUltraDropDown(cmbAccounts, str)
                    Me.cmbAccounts.Rows(0).Activate()
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                ElseIf Me.cmbAccountLevel.Text = "SubSub" Then
                    str = "Select main_sub_sub_id as ID, sub_sub_title as Account From tblCOAMainSubSub"
                    str = str & "" & IIf(Me.cmbAccountType.SelectedIndex > 0, " WHERE account_type='" & Me.cmbAccountType.Text & "'", "") & ""
                    Me.cmbAccounts.DataSource = Nothing
                    FillUltraDropDown(Me.cmbAccounts, str)
                    Me.cmbAccounts.Rows(0).Activate()
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                ElseIf Me.cmbAccountLevel.Text = "Detail" Then
                    If Me.Panel2.Visible = True Then
                        If Me.rbtByName.Checked = True Then
                            str = "SELECT coa_detail_id as ID, detail_title As Account, detail_code as [Code], account_type as [Type], cityname as City FROM dbo.vwCOADetail WHERE (coa_detail_id > 0) AND vwCOADetail.Active " & IIf(Me.chkIncludeActiveAccount.Checked = True, "IN(1,0,NULL)", "=1") & " " & IIf(LoginGroup = "Administrator", "", " and Isnull(AccessLevel,'Everyone')='Everyone'")
                        Else
                            str = "SELECT coa_detail_id as ID, detail_code as [Code] , detail_title as Account,  account_type as [Type], cityname as City FROM dbo.vwCOADetail WHERE (coa_detail_id > 0) AND vwCOADetail.Active " & IIf(Me.chkIncludeActiveAccount.Checked = True, "IN(1,0,NULL)", "=1") & " " & IIf(LoginGroup = "Administrator", "", " and Isnull(AccessLevel,'Everyone')='Everyone'")
                        End If
                        str = str & "" & IIf(Me.cmbAccountType.SelectedIndex > 0, "  AND Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                        str = str & " order by detail_title"
                    End If
                    Me.cmbAccounts.DataSource = Nothing
                    FillUltraDropDown(Me.cmbAccounts, str)
                    Me.cmbAccounts.Rows(0).Activate()
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                Else
                    If Me.rbtByName.Checked = True Then
                        str = "SELECT coa_detail_id as ID, detail_title As Account, detail_code as [Code], account_type as [Type], cityname as City FROM dbo.vwCOADetail WHERE (coa_detail_id > 0) AND vwCOADetail.Active " & IIf(Me.chkIncludeActiveAccount.Checked = True, "IN(1,0,NULL)", "=1") & " " & IIf(LoginGroup = "Administrator", "", " and Isnull(AccessLevel,'Everyone')='Everyone'")
                    Else
                        str = "SELECT coa_detail_id as ID, detail_code as [Code] , detail_title as Account,  account_type as [Type], cityname as City FROM dbo.vwCOADetail WHERE (coa_detail_id > 0) AND vwCOADetail.Active " & IIf(Me.chkIncludeActiveAccount.Checked = True, "IN(1,0,NULL)", "=1") & " " & IIf(LoginGroup = "Administrator", "", " and Isnull(AccessLevel,'Everyone')='Everyone'")
                    End If
                    str = str & "" & IIf(Me.cmbAccountType.SelectedIndex > 0, "  AND Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                    str = str & " order by detail_title"
                    Me.cmbAccounts.DataSource = Nothing
                    FillUltraDropDown(Me.cmbAccounts, str)
                    Me.cmbAccounts.Rows(0).Activate()
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                End If
            ElseIf Condition = "ProjectHead" Then
                str = "Select DISTINCT CostCenterGroup, CostCenterGroup as [Project Group] From tblDefCostCenter WHERE (CostCenterGroup IS NOT NULL AND CostCenterGroup <> '')"
                FillDropDown(Me.CmbCostHead, str, True)
            ElseIf Condition = "Projects" Then
                str = "Select DISTINCT CostCenterId as [Project Id], Name as [Project] From tblDefCostCenter "
                If Me.CheckBox1.Checked = True Then
                    str += " WHERE Active IN (1,0,NULL)"
                Else
                    str += " WHERE Active=1"
                End If
                str += "" & IIf((Me.CmbCostHead.SelectedValue = "0"), "", "WHERE CostCenterGroup='" & Me.CmbCostHead.Text & "'") & " ORDER BY Name"
                FillDropDown(Me.CmbCostCenter, str, True)
                ''TASK TFS4889
            ElseIf Condition = "Company" Then
                str = "Select  CompanyId, CompanyName FROM CompanyDefTable "
                FillDropDown(Me.cmbCompany, str)
                ''END TASK TFS4889
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function FunAddReportCriteria() As String Implements IReportsInterface.FunAddReportCriteria
        Return ""
    End Function
    Public Sub FunAddReportPramaters() Implements IReportsInterface.FunAddReportPramaters

    End Sub
    Public Sub ReportQuery(Optional ByVal Condition As String = "") Implements IReportsInterface.ReportQuery

    End Sub

    Private Sub frmRptLedgerNew_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmRptLedgerNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Me.Cursor = Cursors.WaitCursor
        Try
            'Me.dtpFrom.Value = (Date.Now.AddMonths(-1))
            Me.cmbPeriod.Text = "Current Month"
            Me.cmbAccountLevel.SelectedIndex = 0
            Me.cmbAccountType.SelectedIndex = 0
            FillCombos("Accounts")
            FillCombos("ProjectHead")
            FillCombos("Projects")

            ''TASK TFS4889
            FillCombos("Company")
            ''END TASK TFS4889
            IsFormLoaded = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub BtnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGenerate.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            'If Me.RbtDetailLgr.Checked = True Then
            'AddRptParam("@To_Date", Me.dtpTo.Value)
            'AddRptParam("AccountId", Me.cmbAccountLevel.SelectedValue)
            'ShowReport("rptLegendLedger", , , , False, , , Me.RptDataTable())
            'str_ReportParam = "@To_Date| " & Me.dtpTo.Value & " & @AccountId| " & Me.cmbAccounts.SelectedValue & ""
            'AddRptParam("@To_Date", Me.dtpTo.Value)
            'AddRptParam("@AccountId", Me.cmbAccounts.SelectedValue)
            'Dim Opening As String = GetOpeningBalance(Me.cmbAccounts.Value, Me.dtpFrom.Value.Date.ToString("yyyy-M-d 23:59:59"))
            Dim Dt As DataTable = GetAgingBalance(Me.dtpTo.Value.Date.ToString("yyyy-M-d 00:00:00"), Val(Me.cmbAccounts.ActiveRow.Cells(0).Value))
            If Not Dt Is Nothing Then
                If Dt.Rows.Count > 0 Then
                    AddRptParam("Val30", Val(Dt.Rows(0).Item(2)))
                    AddRptParam("Val3160", Val(Dt.Rows(0).Item(3)))
                    AddRptParam("Val6190", Val(Dt.Rows(0).Item(4)))
                    AddRptParam("Val90Plus", Val(Dt.Rows(0).Item(5)))
                Else
                    AddRptParam("Val30", 0)
                    AddRptParam("Val3160", 0)
                    AddRptParam("Val6190", 0)
                    AddRptParam("Val90Plus", 0)
                End If
            Else
                AddRptParam("Val30", 0)
                AddRptParam("Val3160", 0)
                AddRptParam("Val6190", 0)
                AddRptParam("Val90Plus", 0)
            End If
            AddRptParam("FromDate", Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy"))
            AddRptParam("ToDate", Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy"))

            If Me.RbtDetailLgr.Checked = True Then
                If Me.chkProjectWiseGroup.Checked = False Then
                    ShowReport("rptLegendLedger", , , , False, , "New", Me.RptDataTable())
                Else
                    ShowReport("rptLegendLedgerProjectWise", , , , False, , "New", Me.RptDataTable())
                End If
            Else
                If Me.chkProjectWiseGroup.Checked = False Then
                    ShowReport("rptLegendLedgerSummary", , , , False, , "New", Me.RptDataTable())
                Else
                    ShowReport("rptLegendLedgerSummaryProjectWise", , , , False, , "New", Me.RptDataTable())
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Function RptDataTable() As System.Data.DataTable Implements IReportsInterface.RptDataTable
        Try
            Dim Dt As New DataTable
            Dim str As String = String.Empty

            'str = " SELECT Isnull(Opening.opning,0) as Opening,  Coa.Detail_Code, V_D.coa_detail_id, " _
            '       & "   COA.detail_title, V.voucher_No as voucher_code, V_Type.voucher_type, V.voucher_date, V_D.comments, V_D.debit_amount, V_D.credit_amount, COA.account_type, COA.main_sub_id, coa.CityName,  isnull(V_D.CostCenterID,0) as CostCenterID, V.Cheque_No, V.Post, C_C.Name as CostCenter, COA.Sub_Sub_Title as SubSubTitle FROM dbo.tblVoucher V INNER JOIN " _
            '       & "   dbo.tblVoucherDetail V_D ON V.voucher_id = V_D.voucher_id LEFT OUTER JOIN tblDefCostCenter C_C ON V_D.CostCenterID = C_C.CostCenterID LEFT OUTER JOIN " _
            '       & "   dbo.vwcoadetail COA ON V_D.coa_detail_id = COA.coa_detail_id INNER JOIN " _
            '       & "   dbo.tblDefVoucherType V_Type ON V.voucher_type_id = V_Type.voucher_type_id left outer join " _
            '       & "   (SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning FROM dbo.tblVoucher V INNER JOIN " _
            '       & "   dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
            '       & "   WHERE (CONVERT(Varchar, V.voucher_date, 102) < convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "',102)) " _
            '       & "   GROUP BY VD.coa_detail_id) Opening On OPening.COA_Detail_ID = V_D.COA_Detail_ID " _
            '       & "   Where (convert(Varchar, v.Voucher_Date,102) between convert(Datetime,'" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) and  convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102))"
            'If Me.cmbAccountLevel.Text = "Main" Then
            '    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.coa_main_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & ""
            'ElseIf Me.cmbAccountLevel.Text = "Sub" Then
            '    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.main_sub_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & ""
            'ElseIf Me.cmbAccountLevel.Text = "SubSub" Then
            '    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.main_sub_sub_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & ""
            '    str = str & " " & IIf(Me.cmbAccountType.SelectedIndex > 0, " AND COA.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
            'ElseIf Me.cmbAccountLevel.Text = "Detail" Then
            '    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.coa_detail_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & " "
            '    str = str & " " & IIf(Me.cmbAccountType.SelectedIndex > 0, " AND COA.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
            'Else
            '    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.coa_detail_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & " "
            '    str = str & " " & IIf(Me.cmbAccountType.SelectedIndex > 0, " AND COA.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
            'End If
            'str = str & " " & IIf(Me.CmbCostCenter.SelectedIndex > 0, " AND V_D.CostCenterID=" & Me.CmbCostCenter.SelectedValue & " ", "") & " "
            'str = str & " " & IIf(Me.chkPostDatedCheque.Checked = True, "", " AND (Convert(varchar, V.Cheque_Date,102) < GetDate() OR Convert(varchar, V.Cheque_Date,102) IS NULL) ") & " "
            'str = str & " " & IIf(Me.chkUnPostedVoucher.Checked = True, " AND (V.Post In(1,0))", " AND (V.Post=1) ") & " "
            'str += " ORDER BY v.Voucher_Id"
            If Me.CmbCostCenter.SelectedIndex > 0 Then
                str = "SELECT ISNULL(Opening.Opening, 0) AS Opening,  COA.detail_code, COA.coa_detail_id, COA.detail_title, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Trans.comments, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
                     & " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, COA.Account_Type,      " _
                     & " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.Cheque_No, ISNULL(Trans.Post,0) as Post, Trans.CostCenter, COA.Sub_Sub_Title as SubSubTitle " _
                     & " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
                     & " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening " _
                     & " FROM tblVoucher V INNER JOIN " _
                     & " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id " _
                     & " WHERE VD.CostCenterId= '" & Me.CmbCostCenter.SelectedValue & "' and (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & Me.dtpFrom.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me.chkUnPostedVoucher.Checked = False, " AND V.Post=1", " AND V.Post IN(1,0,NULL)") & "" _
                     & " " & IIf(Me.chkPostDatedCheque.Checked = False, "  AND VD.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & " " _
                     & " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
                     & " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
                     & " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, V_D.comments, ISNULL(V_D.CostCenterId, 0) AS CostCenterId,  " _
                     & " V_D.sp_refrence, V.Post, V.Cheque_Date, V.Cheque_No, C_C.Name as CostCenter " _
                     & " FROM tblVoucher V INNER JOIN " _
                     & " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
                     & " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblDefCostCenter C_C on C_C.CostCenterId = V_D.CostCenterId WHERE (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & Me.dtpFrom.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpTo.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) "
                str = str & " " & IIf(Me.CmbCostCenter.SelectedIndex > 0, " AND V_D.CostCenterID=" & Me.CmbCostCenter.SelectedValue & "", "") & " "
                'str = str & " " & IIf(Me.chkPostDatedCheque.Checked = True, "", " AND (Convert(varchar, V.Cheque_Date,102) < GetDate() OR Convert(varchar, V.Cheque_Date,102) IS NULL) ") & " "

                str = str & " " & IIf(Me.chkPostDatedCheque.Checked = False, "  AND V_D.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & ""
                str = str & " " & IIf(Me.chkUnPostedVoucher.Checked = True, " AND (V.Post In(1,0,NULL))", " AND (V.Post=1) ") & " "
                'str = str & " AND V.Voucher_Code <> '' "
                ''TASK TFS4889
                str = str & " " & IIf(Me.cmbCompany.SelectedIndex > 0, " AND ISNULL(V.location_id, 0) =" & Me.cmbCompany.SelectedValue & "", "") & " "
                ''END TASK TFS4889
                str = str & ") " _
                & " Trans ON Trans.coa_detail_id = COA.coa_detail_id " _
                & " WHERE COA.detail_title is not null AND COA.Active " & IIf(Me.chkIncludeActiveAccount.Checked = True, " IN (1,0,NULL) ", "=1") & " "
                str += " AND (Trans.Debit_Amount <> 0 Or Trans.Credit_Amount <> 0)"
                If Me.cmbAccountLevel.Text = "Main" Then
                    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.coa_main_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & ""
                ElseIf Me.cmbAccountLevel.Text = "Sub" Then
                    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.main_sub_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & ""
                ElseIf Me.cmbAccountLevel.Text = "SubSub" Then
                    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.main_sub_sub_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & ""
                    str = str & " " & IIf((Me.cmbAccountType.SelectedIndex > 0 And Me.cmbAccountType.Text <> "Inventory"), " AND COA.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                ElseIf Me.cmbAccountLevel.Text = "Detail" Then
                    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.coa_detail_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & " "
                    str = str & " " & IIf((Me.cmbAccountType.SelectedIndex > 0 And Me.cmbAccountType.Text <> "Inventory"), " AND COA.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                Else
                    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.coa_detail_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & " "
                    str = str & " " & IIf((Me.cmbAccountType.SelectedIndex > 0 And Me.cmbAccountType.Text <> "Inventory"), " AND COA.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                End If
                str += " ORDER BY Trans.Voucher_Date Asc "
            Else
                str = "SELECT ISNULL(Opening.Opening, 0) AS Opening,  COA.detail_code, COA.coa_detail_id, COA.detail_title, Trans.Voucher_Code, Trans.Voucher_Type, Trans.Voucher_Date, Trans.comments, ISNULL(Trans.Debit_Amount, 0) AS debit_amount, " _
                     & " ISNULL(Trans.Credit_Amount, 0) AS credit_amount, COA.Account_Type,      " _
                     & " COA.main_sub_id, COA.CityName, Trans.CostCenterId, Trans.Cheque_No, ISNULL(Trans.Post,0) as Post, Trans.CostCenter, COA.Sub_Sub_Title as SubSubTitle " _
                     & " FROM dbo.vwCOADetail COA LEFT OUTER JOIN " _
                     & " (SELECT VD.coa_Detail_Id, SUM(ISNULL(VD.Debit_Amount, 0) - ISNULL(VD.Credit_Amount, 0)) AS Opening " _
                     & " FROM tblVoucher V INNER JOIN " _
                     & " tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id " _
                     & " WHERE (CONVERT(varchar, V.Voucher_Date, 102) < CONVERT(Datetime, '" & Me.dtpFrom.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102)) " & IIf(Me.chkUnPostedVoucher.Checked = False, " AND V.Post=1", " AND V.Post IN(1,0,NULL)") & "" _
                     & " " & IIf(Me.chkPostDatedCheque.Checked = False, "  AND VD.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & " " _
                     & " GROUP BY VD.coa_detail_id) Opening ON Opening.coa_Detail_Id = COA.coa_detail_id LEFT OUTER JOIN " _
                     & " (SELECT V_D.coa_detail_id, ISNULL(V_D.Debit_Amount, 0) AS Debit_Amount, ISNULL(V_D.Credit_Amount, 0) AS Credit_Amount,  " _
                     & " V.Voucher_No AS Voucher_Code, V_Type.Voucher_Type, V.Voucher_Date, V_D.comments, ISNULL(V_D.CostCenterId, 0) AS CostCenterId,  " _
                     & " V_D.sp_refrence, V.Post, V.Cheque_Date, V.Cheque_No, C_C.Name as CostCenter " _
                     & " FROM tblVoucher V INNER JOIN " _
                     & " tblVoucherDetail V_D ON V.Voucher_Id = V_D.Voucher_Id INNER JOIN " _
                     & " tblDefVoucherType V_Type ON V_Type.Voucher_Type_Id = V.Voucher_Type_Id LEFT OUTER JOIN tblDefCostCenter C_C on C_C.CostCenterId = V_D.CostCenterId WHERE (CONVERT(Varchar, V.Voucher_Date, 102) BETWEEN CONVERT(Datetime, '" & Me.dtpFrom.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpTo.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) "
                str = str & " " & IIf(Me.CmbCostCenter.SelectedIndex > 0, " AND V_D.CostCenterID=" & Me.CmbCostCenter.SelectedValue & "", "") & " "
                'str = str & " " & IIf(Me.chkPostDatedCheque.Checked = True, "", " AND (Convert(varchar, V.Cheque_Date,102) < GetDate() OR Convert(varchar, V.Cheque_Date,102) IS NULL) ") & " "
                str = str & " " & IIf(Me.CmbCostHead.SelectedIndex > 0, " AND C_C.CostCenterGroup='" & Me.CmbCostHead.Text & "'", "") & " "
                str = str & " " & IIf(Me.chkPostDatedCheque.Checked = False, "  AND V_D.voucher_id not in (select Distinct voucher_id From tblVoucherDetail where  cheque_no <> '' AND (convert(varchar,cheque_date,102) > = Convert(datetime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)))", "") & ""
                str = str & " " & IIf(Me.chkUnPostedVoucher.Checked = True, " AND (V.Post In(1,0,NULL))", " AND (V.Post=1) ") & " "
                'str = str & " AND V.Voucher_Code <> '' "
                ''TASK TFS4889
                str = str & " " & IIf(Me.cmbCompany.SelectedIndex > 0, " AND ISNULL(V.location_id, 0) =" & Me.cmbCompany.SelectedValue & "", "") & " "
                ''END TASK TFS4889
                str = str & ") " _
                & " Trans ON Trans.coa_detail_id = COA.coa_detail_id " _
                & " WHERE COA.detail_title is not null AND COA.Active " & IIf(Me.chkIncludeActiveAccount.Checked = True, " IN (1,0,NULL) ", "=1") & " "
                str += " AND (Trans.Debit_Amount <> 0 Or Trans.Credit_Amount <> 0)"
                If Me.cmbAccountLevel.Text = "Main" Then
                    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.coa_main_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & ""
                ElseIf Me.cmbAccountLevel.Text = "Sub" Then
                    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.main_sub_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & ""
                ElseIf Me.cmbAccountLevel.Text = "SubSub" Then
                    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.main_sub_sub_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & ""
                    str = str & " " & IIf((Me.cmbAccountType.SelectedIndex > 0 And Me.cmbAccountType.Text <> "Inventory"), " AND COA.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                ElseIf Me.cmbAccountLevel.Text = "Detail" Then
                    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.coa_detail_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & " "
                    str = str & " " & IIf((Me.cmbAccountType.SelectedIndex > 0 And Me.cmbAccountType.Text <> "Inventory"), " AND COA.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                Else
                    str = str & " " & IIf(Me.cmbAccounts.ActiveRow.Cells(0).Value > 0, " AND COA.coa_detail_id=" & Me.cmbAccounts.ActiveRow.Cells(0).Value & " ", "") & " "
                    str = str & " " & IIf((Me.cmbAccountType.SelectedIndex > 0 And Me.cmbAccountType.Text <> "Inventory"), " AND COA.Account_Type='" & Me.cmbAccountType.Text & "'", "") & ""
                End If
                str += " ORDER BY Trans.Voucher_Date Asc "
            End If
            Dt = GetDataTable(str)
            Return Dt
            Con.Close()
            Dt = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub CmbCostHead_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbCostHead.Leave
        Try
            If Me.IsFormLoaded = True Then FillCombos("Projects")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub rbtByName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        FillCombos("Accounts")
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub cmbAccountLevel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccountLevel.SelectedIndexChanged
        Try
            If Me.cmbAccountLevel.Text = "Main" Or Me.cmbAccountLevel.Text = "Sub" Then
                Me.Panel1.Visible = False
                Me.Panel2.Visible = False
            Else
                Me.Panel1.Visible = True
                Me.Panel2.Visible = True
            End If
            Me.FillCombos("Accounts")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmbAccountType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccountType.SelectedIndexChanged
        Try
            If Me.IsFormLoaded = True Then FillCombos("Accounts")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rbtAccountName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            FillCombos("Accounts")
            Me.cmbAccounts.DisplayLayout.Bands(0).Columns("ID").Hidden = True
        Catch ex As Exception

        End Try
    End Sub
    Function GetAgingBalance(ByVal ToDate As DateTime, ByVal AccountId As Integer) As DataTable
        Try
            Dim str As String = String.Empty
            If Me.cmbCompany.SelectedIndex > 0 Then
                ''TASK TFS4889
                str = "SP_Lgr_Aging_Balance_CompanyWise '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', " & Me.cmbAccounts.ActiveRow.Cells(0).Value & ", " & Me.cmbCompany.SelectedValue & ""
                ''END TASK TFS4889
            Else
                str = "SP_Lgr_Aging_Balance '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', " & Me.cmbAccounts.ActiveRow.Cells(0).Value & " "
            End If
            Dim dt As DataTable = GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub rbtByCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtByCode.CheckedChanged, rbtByName.CheckedChanged
        Try
            If Me.IsFormLoaded = True Then FillCombos("Accounts")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Try
            Dim id As Integer = 0
            Dim strDisplayMember As String = String.Empty
            id = Me.cmbAccounts.Value
            FillCombos("Accounts")
            Me.cmbAccounts.Value = id

            strDisplayMember = Me.CmbCostHead.Text
            FillCombos("ProjectHead")
            Me.CmbCostHead.Text = strDisplayMember

            id = Me.CmbCostCenter.SelectedValue
            FillCombos("Projects")
            Me.CmbCostCenter.SelectedValue = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Try
            FillCombos("Projects")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub chkIncludeActiveAccount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIncludeActiveAccount.CheckedChanged
        Try
            FillCombos("Accounts")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
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
    End Sub

End Class