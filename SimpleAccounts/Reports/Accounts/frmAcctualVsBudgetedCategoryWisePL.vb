'Ali Faisal : TFS2053 : Add report to show the PL Sub Sub Account Wise Summary
Imports System
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmAcctualVsBudgetedCategoryWisePL
    Implements IGeneral
    Private COSTID As String
    Dim dt As DataTable
    ''' <summary>
    ''' Ali Faisal : TFS2053 : Set indexing of Records grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grd
        PLNoteId
        PLNote
        AccountType
        SubId
        SubCode
        SubTitle
        SubSubId
        SubSubCode
        SubSubTitle
        CategoryTitle
    End Enum
    ''' <summary>
    ''' Ali Faisal : TFS2053 : Apply grid settings to hide some columns and formating too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns(grd.SubId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.PLNoteId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SubSubId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SubSubTitle).Width = 250

            Dim grdGroupBy As New Janus.Windows.GridEX.GridEXGroup(Me.grdSaved.RootTable.Columns(grd.PLNoteId))
            grdGroupBy.GroupPrefix = String.Empty
            Me.grdSaved.RootTable.Groups.Add(grdGroupBy)

            For Each col As Janus.Windows.GridEX.GridEXColumn In grdSaved.RootTable.Columns
                If col.Index > 9 Then
                    col.FormatString = "N" & DecimalPointInValue
                    col.TotalFormatString = "N" & DecimalPointInValue
                    col.Width = 125
                    col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                End If
            Next
            Me.grdSaved.RootTable.Columns(grd.CategoryTitle).Caption = "Head"
            Me.grdSaved.RootTable.Columns(grd.SubSubTitle).ColumnType = Janus.Windows.GridEX.ColumnType.Link
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS2053 : Apply security to show specific controls to standard users
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnPrint.Enabled = True
                Me.btnShow.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnPrint.Enabled = False
            Me.btnShow.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    Me.btnShow.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    If Me.btnPrint.Text = "&Print" Then btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    ''' <summary>
    ''' Ali Faisal : TFS2053 : Fill list bosex
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "HeadCostCenter" Then
                FillListBox(Me.lstHeadCostCenter.ListItem, "SELECT DISTINCT CostCenterGroup, CostCenterGroup FROM tblDefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")
            ElseIf Condition = "CostCenter" Then
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1")
            ElseIf Condition = "Report" Then
                FillUltraDropDown(Me.cmbBSCategory, "SELECT ReportTemplateId, Title, Remarks, Type FROM ReportTemplate WHERE Type = 'BS' AND Active = 1 ORDER BY SortOrder", True)
                Me.cmbBSCategory.Rows(0).Activate()
                If Me.cmbBSCategory.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbBSCategory.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS2053 : Get all records to get data in given duration
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As New DataTable
            Dim str As String = ""
            str = "SELECT ISNULL(VD.CostCenterId, 0) AS CostCenterID, tblDefCostCenter.Name as CostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.sub_title AS SubTitle, COA.account_type AS AccountType, COA.PL_note_id AS PLNoteId, COA.PL_Note_Title AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_code AS SubSubCode, COA.sub_sub_title AS SubSubTitle, Budget.CategoryTitle, SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0)) AS AcctualAmount, ISNULL(Budget.BudgetAmount, 0) AS BudgetAmount, 0 AS Variance" & _
                  "FROM (SELECT tblBudgetConfiguration.SubSubId AS AccountId, SUM(ISNULL(tblBudgetConfiguration.Amount, 0)) AS BudgetAmount, tblDefBudget.BudgetName, ReportTemplateNotesCategory.CategoryTitle, tblDefBudget.ReportId, tblBudgetConfiguration.CostCenterId FROM ReportTemplateNotesCategory INNER JOIN tblBudgetConfiguration ON ReportTemplateNotesCategory.CategoryId = tblBudgetConfiguration.CategoryId INNER JOIN tblDefBudget ON tblBudgetConfiguration.BudgetId = tblDefBudget.BudgetId WHERE (tblBudgetConfiguration.SubSubId > 0) AND (tblBudgetConfiguration.CostCenterId IN (SELECT DISTINCT tblBudgetConfiguration_4.CostCenterId " & _
                  "FROM tblDefBudget AS tblDefBudget_4 INNER JOIN tblBudgetConfiguration AS tblBudgetConfiguration_4 ON tblDefBudget_4.BudgetId = tblBudgetConfiguration_4.BudgetId WHERE (tblDefBudget_4.ReportId = " & cmbBSCategory.Value & ")))" & _
                  "GROUP BY tblBudgetConfiguration.SubSubId, tblBudgetConfiguration.Amount, tblDefBudget.BudgetName, ReportTemplateNotesCategory.CategoryTitle, tblDefBudget.ReportId, tblBudgetConfiguration.CostCenterId" & _
                  "UNION ALL" & _
                  "SELECT tblBudgetConfiguration_1.DetailId AS AccountId, SUM(ISNULL(tblBudgetConfiguration_1.Amount, 0)) AS BudgetAmount, tblDefBudget_1.BudgetName, ReportTemplateNotesCategory_1.CategoryTitle, tblDefBudget_1.ReportId, tblBudgetConfiguration_1.CostCenterId" & _
                  "FROM ReportTemplateNotesCategory AS ReportTemplateNotesCategory_1 INNER JOIN tblBudgetConfiguration AS tblBudgetConfiguration_1 ON ReportTemplateNotesCategory_1.CategoryId = tblBudgetConfiguration_1.CategoryId INNER JOIN tblDefBudget AS tblDefBudget_1 ON tblBudgetConfiguration_1.BudgetId = tblDefBudget_1.BudgetId" & _
                  "WHERE (tblBudgetConfiguration_1.DetailId > 0) AND (tblBudgetConfiguration_1.CostCenterId IN (SELECT DISTINCT tblBudgetConfiguration_3.CostCenterId FROM tblDefBudget AS tblDefBudget_3 INNER JOIN tblBudgetConfiguration AS tblBudgetConfiguration_3 ON tblDefBudget_3.BudgetId = tblBudgetConfiguration_3.BudgetId WHERE (tblDefBudget_3.ReportId = " & cmbBSCategory.Value & ")))" & _
                  "GROUP BY tblBudgetConfiguration_1.DetailId, tblBudgetConfiguration_1.Amount, tblDefBudget_1.BudgetName, ReportTemplateNotesCategory_1.CategoryTitle, tblDefBudget_1.ReportId, tblBudgetConfiguration_1.CostCenterId) AS Budget" & _
                  "INNER JOIN vwCOADetail AS COA ON Budget.AccountId = COA.main_sub_sub_id INNER JOIN tblDefCostCenter ON Budget.CostCenterId = tblDefCostCenter.CostCenterID LEFT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id ON COA.coa_detail_id = VD.coa_detail_id WHERE (Budget.ReportId = 4) AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime, " & dtpFromDate.Value & ", 102) AND CONVERT(DateTime, " & dtpToDate.Value & ", 102)) AND  (VD.CostCenterID IN (SELECT DISTINCT tblBudgetConfiguration_2.CostCenterId FROM tblDefBudget AS tblDefBudget_2 INNER JOIN tblBudgetConfiguration AS tblBudgetConfiguration_2 ON tblDefBudget_2.BudgetId = tblBudgetConfiguration_2.BudgetId WHERE (tblDefBudget_2.ReportId = " & cmbBSCategory.Value & ")))" & _
                  "GROUP BY VD.CostCenterId, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount, Budget.CategoryTitle, tblDefCostCenter.Name" & _
                  "Union all" & _
                  "SELECT ISNULL(VD.CostCenterId, 0) AS CostCenterID, tblDefCostCenter.Name as CostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.sub_title AS SubTitle, COA.account_type AS AccountType, COA.PL_note_id AS PLNoteId, COA.PL_Note_Title AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_code AS SubSubCode, COA.sub_sub_title AS SubSubTitle, Budget.CategoryTitle, SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0)) AS AcctualAmount, ISNULL(Budget.BudgetAmount, 0) AS BudgetAmount, 0 AS Variance" & _
                  "FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id INNER JOIN" & _
                  "(SELECT tblBudgetConfiguration.SubSubId AS AccountId, SUM(ISNULL(tblBudgetConfiguration.Amount, 0)) AS BudgetAmount, tblDefBudget.BudgetName, ReportTemplateNotesCategory.CategoryTitle, tblDefBudget.ReportId, tblBudgetConfiguration.CostCenterId FROM ReportTemplateNotesCategory INNER JOIN tblBudgetConfiguration ON ReportTemplateNotesCategory.CategoryId = tblBudgetConfiguration.CategoryId INNER JOIN tblDefBudget ON tblBudgetConfiguration.BudgetId = tblDefBudget.BudgetId WHERE (tblBudgetConfiguration.SubSubId > 0) AND (tblBudgetConfiguration.CostCenterId IN (SELECT DISTINCT tblBudgetConfiguration_4.CostCenterId" & _
                  "FROM tblDefBudget AS tblDefBudget_4 INNER JOIN tblBudgetConfiguration AS tblBudgetConfiguration_4 ON tblDefBudget_4.BudgetId = tblBudgetConfiguration_4.BudgetId WHERE (tblDefBudget_4.ReportId = " & cmbBSCategory.Value & ")))" & _
                  "GROUP BY tblBudgetConfiguration.SubSubId, tblBudgetConfiguration.Amount, tblDefBudget.BudgetName, ReportTemplateNotesCategory.CategoryTitle, tblDefBudget.ReportId, tblBudgetConfiguration.CostCenterId" & _
                  "UNION ALL" & _
                  "SELECT tblBudgetConfiguration_3.DetailId AS AccountId, SUM(ISNULL(tblBudgetConfiguration_3.Amount, 0)) AS BudgetAmount, tblDefBudget_3.BudgetName, ReportTemplateNotesCategory_1.CategoryTitle, tblDefBudget_3.ReportId, tblBudgetConfiguration_3.CostCenterId FROM ReportTemplateNotesCategory AS ReportTemplateNotesCategory_1 INNER JOIN tblBudgetConfiguration AS tblBudgetConfiguration_3 ON ReportTemplateNotesCategory_1.CategoryId = tblBudgetConfiguration_3.CategoryId INNER JOIN tblDefBudget AS tblDefBudget_3 ON tblBudgetConfiguration_3.BudgetId = tblDefBudget_3.BudgetId WHERE (tblBudgetConfiguration_3.DetailId > 0) AND (tblBudgetConfiguration_3.CostCenterId IN (SELECT DISTINCT tblBudgetConfiguration_2.CostCenterId" & _
                  "FROM tblDefBudget AS tblDefBudget_2 INNER JOIN tblBudgetConfiguration AS tblBudgetConfiguration_2 ON tblDefBudget_2.BudgetId = tblBudgetConfiguration_2.BudgetId WHERE (tblDefBudget_2.ReportId = " & cmbBSCategory.Value & ")))" & _
                  "GROUP BY tblBudgetConfiguration_3.DetailId, tblBudgetConfiguration_3.Amount, tblDefBudget_3.BudgetName, ReportTemplateNotesCategory_1.CategoryTitle, tblDefBudget_3.ReportId,  tblBudgetConfiguration_3.CostCenterId) AS Budget ON COA.coa_detail_id = Budget.AccountId INNER JOIN tblDefCostCenter ON Budget.CostCenterId = tblDefCostCenter.CostCenterID WHERE (Budget.ReportId = " & cmbBSCategory.Value & ") AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime, " & dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & ", 102) AND CONVERT(DateTime, " & dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & ", 102)) AND (VD.CostCenterID IN (SELECT DISTINCT tblBudgetConfiguration_1.CostCenterId " & _
                  "FROM tblDefBudget AS tblDefBudget_1 INNER JOIN tblBudgetConfiguration AS tblBudgetConfiguration_1 ON tblDefBudget_1.BudgetId = tblBudgetConfiguration_1.BudgetId WHERE (tblDefBudget_1.ReportId = " & cmbBSCategory.Value & ")))" & _
                  "GROUP BY VD.CostCenterId, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount, Budget.CategoryTitle, tblDefCostCenter.Name"
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Dim dtCC As DataTable = dt.DefaultView.ToTable(True, "CostCenter")
            'Dim dtCCH As DataTable = dt.DefaultView.ToTable(True, "CostCenterGroup")
            If dtCC.Rows.Count = 0 Then
                msg_Error("No records found in given date range")
                grdSaved.DataSource = Nothing
                Exit Sub
            End If
            Dim q As Integer = 0
            Dim strCC As String = ""
            Dim strCCIsNull As String = ""
            Dim strExpressionActual As String = ""
            Dim strExpressionBudget As String = ""
            Dim strExpressionVariance As String = ""
            For q = 0 To dtCC.Rows.Count - 1 Step 0
                If strCC.ToString = "[]" Or strCCIsNull.ToString = "[]" Then
                    strCC = "[(none)]"
                    strCCIsNull = "ISNULL([(none)],0) AS [(none)]"
                    strExpressionActual = "[(none)]"
                    strExpressionBudget = "[(none)]"
                    strExpressionVariance = "[(none)]"
                End If
                If q <> 0 AndAlso strCC.ToString <> "" Then
                    strCC = strCC & ", "
                    strCCIsNull = strCCIsNull & ", "
                    strExpressionActual = strExpressionActual & "+"
                    strExpressionBudget = strExpressionBudget & "+"
                    strExpressionVariance = strExpressionVariance & "+"
                End If
                strCC = strCC & "[" & dtCC.Rows(q).Item(0) & " ~ Actual]," & "[" & dtCC.Rows(q).Item(0) & " ~ Budget]," & "[" & dtCC.Rows(q).Item(0) & " ~ Variance]"
                strCCIsNull = strCCIsNull & "ISNULL([" & dtCC.Rows(q).Item(0) & " ~ Actual],0) AS [" & dtCC.Rows(q).Item(0) & " ~ Actual]," & "ISNULL([" & dtCC.Rows(q).Item(0) & " ~ Budget],0) AS [" & dtCC.Rows(q).Item(0) & " ~ Budget]," & "ISNULL([" & dtCC.Rows(q).Item(0) & " ~ Variance],0) AS [" & dtCC.Rows(q).Item(0) & " ~ Variance]"
                strExpressionActual = strExpressionActual & "[" & dtCC.Rows(q).Item(0) & " ~ Actual]"
                strExpressionBudget = strExpressionBudget & "[" & dtCC.Rows(q).Item(0) & " ~ Budget]"
                strExpressionVariance = strExpressionVariance & "[" & dtCC.Rows(q).Item(0) & " ~ Variance]"
                q += 1
            Next
            Dim strData As String = ""
            Dim dtData As DataTable
            'For Sub Sub account
            strData = "SELECT PLNoteId, PLNote, AccountType, SubId, SubCode, SubTitle, SubSubId, SubSubCode, SubSubTitle, CategoryTitle," & strCCIsNull & ", 0 AS [Total ~ Actual], 0 AS [Total ~ Budget], 0 AS [Total ~ Variance] FROM ( " _
                    & "SELECT IsNULL(Proj.Name,'none') + ' ~ Actual' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.sub_title AS SubTitle, COA.account_type AS AccountType, COA.PL_note_id AS PLNoteId, COA.PL_Note_Title AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_code AS SubSubCode, COA.sub_sub_title AS SubSubTitle, Budget.CategoryTitle,  SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0)) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id INNER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId, ReportTemplateNotesCategory.CategoryTitle FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId INNER JOIN ReportTemplateNotesCategory ON AccountBudgetDetail.CategoryId = ReportTemplateNotesCategory.CategoryId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) AND (AccountBudgetDetail.AccountLevel = 2) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, ReportTemplateNotesCategory.CategoryTitle, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title <> '') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            Else
                strData += " "
            End If
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount, Budget.CategoryTitle "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Budget' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.sub_title AS SubTitle, COA.account_type AS AccountType, COA.PL_note_id AS PLNoteId, COA.PL_Note_Title AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_code AS SubSubCode, COA.sub_sub_title AS SubSubTitle, Budget.CategoryTitle,  ISNULL(Budget.BudgetAmount, 0) AS Amount " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id INNER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId, ReportTemplateNotesCategory.CategoryTitle FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId INNER JOIN ReportTemplateNotesCategory ON AccountBudgetDetail.CategoryId = ReportTemplateNotesCategory.CategoryId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) AND (AccountBudgetDetail.AccountLevel = 2) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, ReportTemplateNotesCategory.CategoryTitle, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title <> '') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            Else
                strData += " "
            End If
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount, Budget.CategoryTitle "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Variance' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.sub_title AS SubTitle, COA.account_type AS AccountType, COA.PL_note_id AS PLNoteId, COA.PL_Note_Title AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_code AS SubSubCode, COA.sub_sub_title AS SubSubTitle, Budget.CategoryTitle,  SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0)) - ISNULL(Budget.BudgetAmount, 0) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id INNER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId, ReportTemplateNotesCategory.CategoryTitle FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId INNER JOIN ReportTemplateNotesCategory ON AccountBudgetDetail.CategoryId = ReportTemplateNotesCategory.CategoryId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) AND (AccountBudgetDetail.AccountLevel = 2) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, ReportTemplateNotesCategory.CategoryTitle, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title <> '') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            Else
                strData += " "
            End If
            'For Detail account
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount, Budget.CategoryTitle " _
                        & " UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Actual' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.sub_title AS SubTitle, COA.account_type AS AccountType, COA.PL_note_id AS PLNoteId, COA.PL_Note_Title AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_code AS SubSubCode, COA.sub_sub_title AS SubSubTitle, Budget.CategoryTitle,  SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0)) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id INNER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId, ReportTemplateNotesCategory.CategoryTitle FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId INNER JOIN ReportTemplateNotesCategory ON AccountBudgetDetail.CategoryId = ReportTemplateNotesCategory.CategoryId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) AND (AccountBudgetDetail.AccountLevel = 3) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, ReportTemplateNotesCategory.CategoryTitle, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.coa_detail_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title <> '') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            Else
                strData += " "
            End If
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount, Budget.CategoryTitle "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Budget' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.sub_title AS SubTitle, COA.account_type AS AccountType, COA.PL_note_id AS PLNoteId, COA.PL_Note_Title AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_code AS SubSubCode, COA.sub_sub_title AS SubSubTitle, Budget.CategoryTitle,  ISNULL(Budget.BudgetAmount, 0) AS Amount " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id INNER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId, ReportTemplateNotesCategory.CategoryTitle FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId INNER JOIN ReportTemplateNotesCategory ON AccountBudgetDetail.CategoryId = ReportTemplateNotesCategory.CategoryId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) AND (AccountBudgetDetail.AccountLevel = 3) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, ReportTemplateNotesCategory.CategoryTitle, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.coa_detail_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title <> '') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            Else
                strData += " "
            End If
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount, Budget.CategoryTitle "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Variance' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.sub_title AS SubTitle, COA.account_type AS AccountType, COA.PL_note_id AS PLNoteId, COA.PL_Note_Title AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_code AS SubSubCode, COA.sub_sub_title AS SubSubTitle, Budget.CategoryTitle,  SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0)) - ISNULL(Budget.BudgetAmount, 0) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id INNER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId, ReportTemplateNotesCategory.CategoryTitle FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId INNER JOIN ReportTemplateNotesCategory ON AccountBudgetDetail.CategoryId = ReportTemplateNotesCategory.CategoryId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) AND (AccountBudgetDetail.AccountLevel = 3) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, ReportTemplateNotesCategory.CategoryTitle, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.coa_detail_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title <> '') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            Else
                strData += " "
            End If
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount, Budget.CategoryTitle " _
                    & ") AS CostCenters " _
                    & "PIVOT (SUM(Amount) FOR AllCostCenter IN (" & strCC & ")) AS PVT " _
                    & "ORDER BY PLNoteId"
            dtData = GetDataTable(strData)
            dtData.AcceptChanges()
            dtData.Columns("Total ~ Actual").Expression = strExpressionActual
            dtData.Columns("Total ~ Budget").Expression = strExpressionBudget
            dtData.Columns("Total ~ Variance").Expression = strExpressionVariance
            Me.grdSaved.DataSource = dtData
            Me.grdSaved.RetrieveStructure()

            Me.grdSaved.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            Dim ColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
            Me.grdSaved.RootTable.ColumnSetRowCount = 1
            ColumnSet = Me.grdSaved.RootTable.ColumnSets.Add
            ColumnSet.ColumnCount = 3
            ColumnSet.Caption = "Account Summary"
            'ColumnSet.Add(Me.grdSaved.RootTable.Columns("PLNote"), 0, 0)
            'ColumnSet.Add(Me.grdSaved.RootTable.Columns("AccountType"), 0, 1)
            'ColumnSet.Add(Me.grdSaved.RootTable.Columns("SubCode"), 0, 2)
            ColumnSet.Add(Me.grdSaved.RootTable.Columns("CategoryTitle"), 0, 0)
            ColumnSet.Add(Me.grdSaved.RootTable.Columns("SubSubCode"), 0, 1)
            ColumnSet.Add(Me.grdSaved.RootTable.Columns("SubSubTitle"), 0, 2)

            'For Each RowCCH As DataRow In dtCCH.Rows
            '    Dim strCCFilter As String = ""
            '    Dim dtCCFilter As DataTable
            '    strCCFilter = "SELECT ISNULL(Proj.Name, 'none') AS CostCenter, VD.CostCenterID " _
            '                & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id INNER JOIN (SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title) AS Budget ON COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
            '                & "WHERE (COA.PL_Note_Title <> '') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102))  " & IIf(RowCCH.Item(0).ToString = "", " AND (Proj.CostCenterGroup Is Null Or Proj.CostCenterGroup = '') ", " AND Proj.CostCenterGroup = '" & RowCCH.Item(0).ToString & "'") & " "
            '    If Me.lstCostCenter.SelectedIDs.Length > 0 Then
            '        strCCFilter += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            '    Else
            '        strCCFilter += " "
            '    End If
            '    strCCFilter += "GROUP BY Proj.CostCenterGroup, VD.CostCenterID, Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount " _
            '        & ""
            '    dtCCFilter = GetDataTable(strCCFilter)
            '    COSTID = ModGlobel.GetRowValuesIntoString(dtCCFilter, "CostCenterID")
            '    dtCCFilter = dtCCFilter.DefaultView.ToTable(True, "CostCenter")
            '    Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
            '    Me.grdSaved.RootTable.ColumnSetRowCount = 1
            '    ColumnSet1 = Me.grdSaved.RootTable.ColumnSets.Add
            '    ColumnSet1.Caption = RowCCH.Item(0).ToString
            '    ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '    Dim strExpression As String = ""
            '    Dim i As Integer = 0
            '    strExpression = ""
            '    For q = 0 To dtCCFilter.Rows.Count - 1 Step 0
            '        If q <> 0 AndAlso strCC.ToString <> "" Then
            '            strExpression = strExpression & "|"
            '        End If
            '        strExpression = strExpression & "" & dtCCFilter.Rows(q).Item(0) & " ~ Actual|" & "" & dtCCFilter.Rows(q).Item(0) & " ~ Budget|" & "" & dtCCFilter.Rows(q).Item(0) & " ~ Variance"
            '        q += 1
            '    Next
            '    'strExpression = strExpression & "|Total ~ Actual|Total ~ Budget|Total ~ Variance"
            '    Dim strNew() As String = strExpression.Split("|")
            '    For Each RowCC As String In strNew
            '        ColumnSet1.ColumnCount = strNew.Length
            '        ColumnSet1.Add(Me.grdSaved.RootTable.Columns(RowCC), 0, i)
            '        ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '        i += 1
            '    Next
            'Next
            Me.grdSaved.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            Dim ColumnSet2 As New Janus.Windows.GridEX.GridEXColumnSet
            Me.grdSaved.RootTable.ColumnSetRowCount = 1
            ColumnSet2 = Me.grdSaved.RootTable.ColumnSets.Add
            ColumnSet2.ColumnCount = 3
            ColumnSet2.Caption = "Totals"
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Total ~ Actual"), 0, 0)
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Total ~ Budget"), 0, 1)
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Total ~ Variance"), 0, 2)
            ColumnSet2.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional Mode As Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function
    ''' <summary>
    ''' Ali Faisal : TFS2053 : Reset all controls to their default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Label3.Text = ""
            Me.cmbPeriod.SelectedIndex = 0
            Me.cmbPeriod.Text = "Current Month"
            FillCombos("HeadCostCenter")
            FillCombos("CostCenter")
            FillCombos("Report")
            Me.grdSaved.DataSource = Nothing
            Me.grdSaved.RetrieveStructure()
            Me.UltraTabControl1.Tabs(0).Selected = True
            CtrlGrdBar1_Load(Nothing, Nothing)
            ApplySecurity(Utility.EnumDataMode.New)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
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
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Category Wise Budget Comparison PL Summary" & Chr(10) & "From Date : " & Me.dtpFromDate.Value.ToString("dd-MM-yyyy") & " To Date : " & Me.dtpToDate.Value.ToString("dd-MM-yyyy")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPLSubSubAccountWiseSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            btnNew_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS2053 : Show crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            'GetCrystalReportRights()
            'Me.lblProgress.Text = "Processing Please Wait ..."
            'Me.lblProgress.Visible = True
            'Application.DoEvents()
            'Me.Cursor = Cursors.WaitCursor
            'Dim dt As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            'dt.AcceptChanges()
            'ShowReport("rptPLSubSubAccountWiseSummary", , , , , , , dt, , , , , , , )
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            GetAllRecords()
            If Me.grdSaved.RowCount > 0 Then
                Label3.Text = dtpFromDate.Value.ToString("dd-MM-yyyy") + " To " + dtpToDate.Value.ToString("dd-MM-yyyy")
                Me.UltraTabControl1.Tabs(1).Selected = True
            Else
                Me.UltraTabControl1.Tabs(0).Selected = True
            End If
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.Tabs(0).Selected = True Then
                Me.btnPrint.Visible = False
            End If
            If Me.UltraTabControl1.Tabs(1).Selected = True Then
                Me.btnPrint.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstHeadCostCenter_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstHeadCostCenter.SelectedIndexChaned
        Try
            If Me.lstHeadCostCenter.SelectedIDs.Length > 0 Then
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1  AND CostCenterGroup IN (" & Me.lstHeadCostCenter.SelectedItems & ")")
            Else
                FillCombos("CostCenter")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
            rptTrialBalance.DateTimePicker1.Value = Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            Dim CostCenterIds As String = ""
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                rptTrialBalance.CostID = CostCenterIds
            Else
                CostCenterIds = ""
            End If
            rptTrialBalance.Tracking = True
            rptTrialBalance.GetSubSubWiseDetailAccountsTrial(Val(Me.grdSaved.GetRow.Cells(grd.SubSubId).Value.ToString), COSTID)
            frmMain.LoadControl("rptTrialBalance")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class