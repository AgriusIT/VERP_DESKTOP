'Ali Faisal : TFS2053 : Add report to show the PL Sub Sub Account Wise Summary
'' Muhammad Amin TFS3342: Removal of BUDGET and VARIANCE COST CENTERS and addition of ACTUAL, BUDGET and VARIANCE columns with every COST CENTER GROUP. DATED : 22-05-2018
''TASK TFS3425 Muhammad Amin done on 06-06-2018 : Show record of zero or null cost centers.
Imports System
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmAcctualVsBudgetedPLReport
    Implements IGeneral
    Dim IsOpenForm As Boolean = False
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
        SubSubId
        SubSubTitle
        GrossAmount
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
            Me.grdSaved.RootTable.Columns(grd.GrossAmount).Visible = True

            Dim grdGroupBy As New Janus.Windows.GridEX.GridEXGroup(Me.grdSaved.RootTable.Columns(grd.PLNote))
            grdGroupBy.GroupPrefix = String.Empty
            Me.grdSaved.RootTable.Groups.Add(grdGroupBy)

            For Each col As Janus.Windows.GridEX.GridEXColumn In grdSaved.RootTable.Columns
                If col.Index > 6 Then
                    col.FormatString = "N" & DecimalPointInValue
                    col.TotalFormatString = "N" & DecimalPointInValue
                    col.Width = 80
                    col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    'TFS3343: Waqar Raza: Added this line to make all column link for tracking
                    'Start TFS3343
                    col.ColumnType = Janus.Windows.GridEX.ColumnType.Link
                    'End TFS3343
                End If
            Next

            Dim FormatStype As New Janus.Windows.GridEX.GridEXFormatStyle()
            FormatStype.ForeColor = Color.Black
            FormatStype.FontBold = Janus.Windows.GridEX.TriState.True
            Dim FormatCondition As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdSaved.RootTable.Columns(grd.PLNote), Janus.Windows.GridEX.ConditionOperator.Equal, "03-Gross Profit")
            FormatCondition.FormatStyle = FormatStype
            Me.grdSaved.RootTable.FormatConditions.Add(FormatCondition)
            ''2
            Dim FormatStype1 As New Janus.Windows.GridEX.GridEXFormatStyle()
            FormatStype1.ForeColor = Color.Black
            FormatStype1.FontBold = Janus.Windows.GridEX.TriState.True
            Dim FormatCondition2 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdSaved.RootTable.Columns(grd.PLNote), Janus.Windows.GridEX.ConditionOperator.Equal, "06-Total OH")
            FormatCondition2.FormatStyle = FormatStype1
            Me.grdSaved.RootTable.FormatConditions.Add(FormatCondition2)
            ''3
            Dim FormatStype3 As New Janus.Windows.GridEX.GridEXFormatStyle()
            FormatStype3.ForeColor = Color.Black
            FormatStype3.FontBold = Janus.Windows.GridEX.TriState.True
            Dim FormatCondition3 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdSaved.RootTable.Columns(grd.PLNote), Janus.Windows.GridEX.ConditionOperator.Equal, "07-Operating Profit")
            FormatCondition3.FormatStyle = FormatStype3
            Me.grdSaved.RootTable.FormatConditions.Add(FormatCondition3)
            ''4
            Dim FormatStype4 As New Janus.Windows.GridEX.GridEXFormatStyle()
            FormatStype4.ForeColor = Color.Black
            FormatStype4.FontBold = Janus.Windows.GridEX.TriState.True
            Dim FormatCondition4 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdSaved.RootTable.Columns(grd.PLNote), Janus.Windows.GridEX.ConditionOperator.Equal, "09-Total Profit")
            FormatCondition4.FormatStyle = FormatStype4
            Me.grdSaved.RootTable.FormatConditions.Add(FormatCondition4)
            ''5
            Dim FormatStype5 As New Janus.Windows.GridEX.GridEXFormatStyle()
            FormatStype5.ForeColor = Color.Black
            FormatStype5.FontBold = Janus.Windows.GridEX.TriState.True
            Dim FormatCondition5 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdSaved.RootTable.Columns(grd.PLNote), Janus.Windows.GridEX.ConditionOperator.Equal, "11-Profit Before Tax")
            FormatCondition5.FormatStyle = FormatStype5
            Me.grdSaved.RootTable.FormatConditions.Add(FormatCondition5)
            ''6
            Dim FormatStype6 As New Janus.Windows.GridEX.GridEXFormatStyle()
            FormatStype6.ForeColor = Color.Black
            FormatStype6.FontBold = Janus.Windows.GridEX.TriState.True
            Dim FormatCondition6 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdSaved.RootTable.Columns(grd.PLNote), Janus.Windows.GridEX.ConditionOperator.Equal, "13-Profit After Tax")
            FormatCondition6.FormatStyle = FormatStype6
            Me.grdSaved.RootTable.FormatConditions.Add(FormatCondition6)

            'Me.grdSaved.RootTable.Columns("Total ~ Actual").ColumnType = Janus.Windows.GridEX.ColumnType.Link
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
                'TFS3412: Waqar Raza: Addedby Waqar to Deselect the List for User Friendly
                'Start TFS3412
                lstHeadCostCenter.DeSelect()
                'End TFS3412
            ElseIf Condition = "CostCenter" Then
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1")
                'TFS3412: Waqar Raza: Addedby Waqar to Deselect the List for User Friendly
                'Start TFS3412
                lstCostCenter.DeSelect()
                'End TFS3412
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
            str = "SELECT ISNULL(Proj.CostCenterGroup,'') AS CostCenterGroup, ISNULL(VD.CostCenterID,0) AS CostCenterID, ISNULL(Proj.Name,'none') AS CostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, CONVERT(varchar(20),COA.PL_note_id) + '-' + COA.PL_Note_Title AS PLNoteId, COA.PL_Note_Title AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle, SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0)) AS AcctualAmount, ISNULL(Budget.BudgetAmount, 0) AS BudgetAmount, 0 AS Variance " _
                & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.FromDate, AccountBudgetMaster.ToDate, AccountBudgetMaster.Amount,  AccountBudgetMaster.Remarks, AccountBudgetMaster.CostCenterId) AS Budget ON COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                & "WHERE (COA.PL_Note_Title <> '') AND ISNULL(Proj.IsLogical, 0) = 0 AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                str += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                str += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425


            str += "GROUP BY Proj.CostCenterGroup, VD.CostCenterID, Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Dim dtCC As DataTable = dt.DefaultView.ToTable(True, "CostCenter")
            Dim dtCCH As DataTable = dt.DefaultView.ToTable(True, "CostCenterGroup")
            If dtCC.Rows.Count = 0 Then
                msg_Error("No records found in given date range")
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

            '''''''''''''''''''First Query for Revenue'''''''''''''''''''''''''''''''''

            strData = "SELECT PLNoteId, PLNote, AccountType, SubId, SubCode, SubSubId, SubSubTitle, Convert(float, 0) AS GrossAmount, Convert(float, 0) AS TotalOH, Convert(float, 0) AS TotalProfit, Convert(float, 0) AS OperatingProfit, Convert(float, 0) AS ProfitBeforeTaxation, Convert(float, 0) AS ProfitAfterTaxation," & strCCIsNull & ", 0 AS [Total ~ Actual], 0 AS [Total ~ Budget], 0 AS [Total ~ Variance] FROM ( " _
                    & "SELECT IsNULL(Proj.Name,'none') + ' ~ Actual' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 01 AS PLNoteId, '01-Revenue' As PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  (-1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title = 'Sales Net') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Budget' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 01 AS PLNoteId, '01-Revenue' As PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  ISNULL(Budget.BudgetAmount, 0) AS Amount " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title = 'Sales Net') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Variance' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType,  01 AS PLNoteId, '01-Revenue' As PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0)) - ISNULL(Budget.BudgetAmount, 0) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title = 'Sales Net') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425


            ''''''''''''''''''''''''Second Query for Cash of Sales'''''''''''''''''''''''''''''''

            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Actual' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 02 AS PLNoteId , '02-Cost Of Sales' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  (-1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title = 'Cost Of Goods Sold') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Budget' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 02 AS PLNoteId , '02-Cost Of Sales' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  ISNULL(Budget.BudgetAmount, 0) AS Amount " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title = 'Cost Of Goods Sold') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Variance' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType,  02 AS PLNoteId , '02-Cost Of Sales' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0)) - ISNULL(Budget.BudgetAmount, 0) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title = 'Cost Of Goods Sold') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425


            '''''''''''''''''''Second Query for Overhead InDirect'''''''''''''''''''''''

            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount " _
                    & "UNION ALL SELECT IsNULL(CC.Name,'none') + ' ~ Actual' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 05 AS PLNoteId, '05-Overheads-Indirect' AS  PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle, (-1*SUM( CASE WHEN ISNULL(Per, 0) > 0 THEN ISNULL(VD.debit_amount, 0)*Per/100-ISNULL(VD.credit_amount, 0)*Per/100 ELSE ISNULL(VD.debit_amount, 0)-ISNULL(VD.credit_amount, 0) END)) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID LEFT OUTER JOIN (SELECT Detail.ToCostCenterId As ToCostCenterId, Sum(IsNull(Detail.AmountPercentage, 0)) AS Per ,  Bifurcation.FromCostCenterId FROM LogicalBifurcationDetail AS Detail INNER JOIN LogicalBifurcation AS Bifurcation ON Detail.LogicalBifurcationId = Bifurcation.LogicalBifurcationId WHERE Bifurcation.LogicalBifurcationId IN (SELECT Max(LogicalBifurcationId) AS LogicalBifurcationId FROM LogicalBifurcation Group By FromCostCenterId) Group By Detail.ToCostCenterId, FromCostCenterId) AS Logical ON VD.CostCenterID = Logical.FromCostCenterId LEFT OUTER JOIN tblDefCostCenter AS CC ON Logical.ToCostCenterId =  CC.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title IN ('Administrative Exp', 'Selling Exp', 'Operating and Other Expences.', 'Financial & Other Charges-Net')) And IsNull(Proj.IsLogical, 0) = 1 AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            strData += "GROUP BY CC.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(CC.Name,'none') + ' ~ Budget' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 05 AS PLNoteId, '05-Overheads-Indirect' AS  PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  ISNULL(Budget.BudgetAmount, 0) AS Amount " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID LEFT OUTER JOIN (SELECT Detail.ToCostCenterId As ToCostCenterId, Sum(IsNull(Detail.AmountPercentage, 0)) AS Per ,  Bifurcation.FromCostCenterId FROM LogicalBifurcationDetail AS Detail INNER JOIN LogicalBifurcation AS Bifurcation ON Detail.LogicalBifurcationId = Bifurcation.LogicalBifurcationId WHERE Bifurcation.LogicalBifurcationId IN (SELECT Max(LogicalBifurcationId) AS LogicalBifurcationId FROM LogicalBifurcation Group By FromCostCenterId) Group By Detail.ToCostCenterId, FromCostCenterId) AS Logical ON VD.CostCenterID = Logical.FromCostCenterId LEFT OUTER JOIN tblDefCostCenter AS CC ON Logical.ToCostCenterId =  CC.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title IN ('Administrative Exp', 'Selling Exp', 'Operating and Other Expences.', 'Financial & Other Charges-Net')) And IsNull(Proj.IsLogical, 0) = 1 AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            strData += "GROUP BY CC.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(CC.Name,'none') + ' ~ Variance' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 05 AS PLNoteId, '05-Overheads-Indirect' AS  PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  SUM( CASE WHEN ISNULL(Per, 0) > 0 THEN ISNULL(VD.debit_amount, 0)*Per/100-ISNULL(VD.credit_amount, 0)*Per/100 ELSE ISNULL(VD.debit_amount, 0)-ISNULL(VD.credit_amount, 0) END) - ISNULL(Budget.BudgetAmount, 0) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID LEFT OUTER JOIN (SELECT Detail.ToCostCenterId As ToCostCenterId, Sum(IsNull(Detail.AmountPercentage, 0)) AS Per ,  Bifurcation.FromCostCenterId FROM LogicalBifurcationDetail AS Detail INNER JOIN LogicalBifurcation AS Bifurcation ON Detail.LogicalBifurcationId = Bifurcation.LogicalBifurcationId WHERE Bifurcation.LogicalBifurcationId IN (SELECT Max(LogicalBifurcationId) AS LogicalBifurcationId FROM LogicalBifurcation Group By FromCostCenterId) Group By Detail.ToCostCenterId, FromCostCenterId) AS Logical ON VD.CostCenterID = Logical.FromCostCenterId LEFT OUTER JOIN tblDefCostCenter AS CC ON Logical.ToCostCenterId =  CC.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title IN ('Administrative Exp', 'Selling Exp', 'Operating and Other Expences.', 'Financial & Other Charges-Net')) And IsNull(Proj.IsLogical, 0) = 1 AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            ''''''''''''''''''''''''''Third Query for Overhead Directs'''''''''''''''''''''''''''''''
            strData += "GROUP BY CC.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount " _
                    & "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Actual' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 04 AS PLNoteId , '04-Overheads-Direct' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  (-1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title IN ('Administrative Exp', 'Selling Exp', 'Operating and Other Expences.'))  AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Budget' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 04 AS PLNoteId , '04-Overheads-Direct' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  ISNULL(Budget.BudgetAmount, 0) AS Amount " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title IN ('Administrative Exp', 'Selling Exp', 'Operating and Other Expences.'))  AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Variance' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 04 AS PLNoteId , '04-Overheads-Direct' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0)) - ISNULL(Budget.BudgetAmount, 0) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title IN ('Administrative Exp', 'Selling Exp', 'Operating and Other Expences.')) AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            ''''''''''''''''''''''''Fourth Query for Non Operating Income'''''''''''''''''''''''''''''''
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Actual' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 08 AS PLNoteId,  '08-Not Operating income' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  (-1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_note_id = 3) AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Budget' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 08 AS PLNoteId,  '08-Not Operating income' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  ISNULL(Budget.BudgetAmount, 0) AS Amount " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_note_id = 3) AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Variance' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType,  08 AS PLNoteId,  '08-Not Operating income' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0)) - ISNULL(Budget.BudgetAmount, 0) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_note_id = 3) AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425


            ''''''''''''''''''''''''Fifth Query for Finance Cost'''''''''''''''''''''''''''''''
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Actual' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 10 AS PLNoteId, '10-Finance Cost' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  (-1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_note_id = 7) AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Budget' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 10 AS PLNoteId, '10-Finance Cost' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  ISNULL(Budget.BudgetAmount, 0) AS Amount " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_note_id = 7) AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Variance' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType,  10 AS PLNoteId, '10-Finance Cost' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0)) - ISNULL(Budget.BudgetAmount, 0) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_note_id = 7) AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425

            ''''''''''''''''''''''''Sixth Query for Taxation'''''''''''''''''''''''''''''''

            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Actual' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 12 AS PLNoteId , '12-Taxation' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  (-1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title = 'Taxation') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Budget' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType, 12 AS PLNoteId , '12-Taxation' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  ISNULL(Budget.BudgetAmount, 0) AS Amount " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title = 'Taxation') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount "

            strData += "UNION ALL SELECT IsNULL(Proj.Name,'none') + ' ~ Variance' AS AllCostCenter, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.account_type AS AccountType,  12 AS PLNoteId , '12-Taxation' AS PLNote, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle,  SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0)) - ISNULL(Budget.BudgetAmount, 0) AS Amount  " _
                    & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN " _
                    & "(SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId WHERE (CONVERT(Varchar, AccountBudgetMaster.FromDate, 102) >= CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(Varchar, AccountBudgetMaster.ToDate, 102) <= CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title, AccountBudgetMaster.CostCenterId) AS Budget ON VD.CostCenterID = Budget.CostCenterId AND COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title = 'Taxation') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425


            strData += "GROUP BY Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount ) AS CostCenters " _
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
            ColumnSet.ColumnCount = 1
            ColumnSet.Caption = "Account Summary"
            'ColumnSet.Add(Me.grdSaved.RootTable.Columns("PLNote"), 0, 0)
            'ColumnSet.Add(Me.grdSaved.RootTable.Columns("AccountType"), 0, 1)
            'ColumnSet.Add(Me.grdSaved.RootTable.Columns("SubCode"), 0, 2)
            ColumnSet.Add(Me.grdSaved.RootTable.Columns("SubSubTitle"), 0, 0)
            For Each RowCCH As DataRow In dtCCH.Rows
                Dim strCCFilter As String = ""
                Dim dtCCFilter As DataTable
                strCCFilter = "SELECT ISNULL(Proj.Name, 'none') AS CostCenter " _
                            & "FROM tblDefCostCenter AS Proj RIGHT OUTER JOIN tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN (SELECT AccountBudgetDetail.AccountId, SUM(AccountBudgetDetail.BudgetAmount) AS BudgetAmount, AccountBudgetMaster.Title FROM AccountBudgetDetail INNER JOIN AccountBudgetMaster ON AccountBudgetDetail.AccountBudgetMasterID = AccountBudgetMaster.AccountBudgetMasterId GROUP BY AccountBudgetDetail.AccountId, AccountBudgetMaster.Title) AS Budget ON COA.main_sub_sub_id = Budget.AccountId ON Proj.CostCenterID = VD.CostCenterID " _
                            & "WHERE (COA.PL_Note_Title <> '') AND ISNULL(Proj.IsLogical, 0) = 0 AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102))  " & IIf(RowCCH.Item(0).ToString = "", " AND (Proj.CostCenterGroup Is Null Or Proj.CostCenterGroup = '') ", " AND Proj.CostCenterGroup = '" & RowCCH.Item(0).ToString & "'") & " "
                If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                    strCCFilter += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
                End If
                ''TASK TFS3425
                If Me.chkShowWithOutCostCenter.Checked = True Then
                    strCCFilter += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
                End If
                ''END TASK TFS3425
                strCCFilter += "GROUP BY Proj.CostCenterGroup, VD.CostCenterID, Proj.Name, COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, Budget.BudgetAmount " _
                    & ""
                dtCCFilter = GetDataTable(strCCFilter)
                dtCCFilter = dtCCFilter.DefaultView.ToTable(True, "CostCenter")
                Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
                Me.grdSaved.RootTable.ColumnSetRowCount = 1
                ColumnSet1 = Me.grdSaved.RootTable.ColumnSets.Add
                ColumnSet1.Caption = RowCCH.Item(0).ToString
                ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Dim strExpression As String = ""
                Dim i As Integer = 0
                strExpression = ""
                For q = 0 To dtCCFilter.Rows.Count - 1 Step 0
                    If q <> 0 AndAlso strCC.ToString <> "" Then
                        strExpression = strExpression & "|"
                    End If
                    strExpression = strExpression & "" & dtCCFilter.Rows(q).Item(0) & " ~ Actual|" & "" & dtCCFilter.Rows(q).Item(0) & " ~ Budget|" & "" & dtCCFilter.Rows(q).Item(0) & " ~ Variance"
                    q += 1
                Next
                Dim strNew() As String = strExpression.Split("|")

                '' Start TASK TFS3342 : Creation of GROUP COST CENTER GROUP WISE COLUMNS Actual, Budget and Variance as well removal of BUDGET AND VARIANCE COST CENTERS.
                Dim ActualCount As Integer = 0
                For Each RowCC As String In strNew
                    Dim Extention As String = RowCC.Substring(RowCC.LastIndexOf("~") + 1).Replace(" ", "")
                    Dim ActualPrefix As String = RowCC.Substring(0, RowCC.LastIndexOf("~") - 1).Replace(" ", "")
                    If Extention.ToUpper = "ACTUAL" Then
                        ActualCount += 1
                    End If
                Next
                ''1 - Actual
                Dim ActualColumm As String = RowCCH.Item(0).ToString & "Actual"
                Dim NewColumn1 As New Janus.Windows.GridEX.GridEXColumn(ActualColumm, Janus.Windows.GridEX.ColumnType.Text)
                NewColumn1.Caption = "Actual"
                Me.grdSaved.RootTable.Columns.Add(NewColumn1)

                Dim dtColumn1 As DataTable = CType(Me.grdSaved.DataSource, DataTable)
                Dim column1 As New DataColumn(ActualColumm)
                column1.DataType = System.Type.GetType("System.Double")
                column1.DefaultValue = 0
                If dtCCFilter.Rows.Count > 0 Then
                    dtColumn1.Columns.Add(column1)
                    dtColumn1.AcceptChanges()
                End If

                ''2 - Budget
                Dim BudgetColumm As String = RowCCH.Item(0).ToString & "Budget"
                Dim NewColumn2 As New Janus.Windows.GridEX.GridEXColumn(BudgetColumm, Janus.Windows.GridEX.ColumnType.Text)
                NewColumn2.Caption = "Budget"
                Me.grdSaved.RootTable.Columns.Add(NewColumn2)

                Dim dtColumn2 As DataTable = CType(Me.grdSaved.DataSource, DataTable)
                Dim column2 As New DataColumn(BudgetColumm)
                column2.DataType = System.Type.GetType("System.Double")
                column2.DefaultValue = 0
                If dtCCFilter.Rows.Count > 0 Then
                    dtColumn2.Columns.Add(column2)
                    dtColumn2.AcceptChanges()
                End If


                ''2 - Variance
                Dim VarianceColumm As String = RowCCH.Item(0).ToString & "Variance"
                Dim NewColumn3 As New Janus.Windows.GridEX.GridEXColumn(VarianceColumm, Janus.Windows.GridEX.ColumnType.Text)
                NewColumn3.Caption = "Variance"
                Me.grdSaved.RootTable.Columns.Add(NewColumn3)

                Dim dtColumn3 As DataTable = CType(Me.grdSaved.DataSource, DataTable)
                Dim column3 As New DataColumn(VarianceColumm)
                column3.DataType = System.Type.GetType("System.Double")
                column3.DefaultValue = 0
                If dtCCFilter.Rows.Count > 0 Then
                    dtColumn3.Columns.Add(column3)
                    dtColumn3.AcceptChanges()
                End If
                ''END TASK TFS3342
                For Each RowCC As String In strNew
                    Dim Extention As String = RowCC.Substring(RowCC.LastIndexOf("~") + 1).Replace(" ", "")
                    Dim ActualPrefix As String = RowCC.Substring(0, RowCC.LastIndexOf("~") - 1).Replace(" ", "")
                    If Extention.ToUpper = "ACTUAL" Then
                        Me.grdSaved.RootTable.Columns(RowCC).Caption = ActualPrefix
                        ColumnSet1.ColumnCount = ActualCount + 3
                        ColumnSet1.Add(Me.grdSaved.RootTable.Columns(RowCC), 0, i)
                        ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                        i += 1
                    ElseIf Extention.ToUpper = "BUDGET" Then
                        Me.grdSaved.RootTable.Columns(RowCC).Visible = False
                    ElseIf Extention.ToUpper = "VARIANCE" Then
                        Me.grdSaved.RootTable.Columns(RowCC).Visible = False
                    End If
                Next


                ''' Actual 
                If dtCCFilter.Rows.Count > 0 Then
                    ColumnSet1.ColumnCount = dtCCFilter.Rows.Count + 3
                    ColumnSet1.Add(Me.grdSaved.RootTable.Columns(ActualColumm), 0, i)
                    ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    i += 1
                End If
                ''' Budget
                If dtCCFilter.Rows.Count > 0 Then
                    ColumnSet1.ColumnCount = dtCCFilter.Rows.Count + 3
                    ColumnSet1.Add(Me.grdSaved.RootTable.Columns(BudgetColumm), 0, i)
                    ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    i += 1
                End If
                ''' Variance
                If dtCCFilter.Rows.Count > 0 Then
                    ColumnSet1.ColumnCount = dtCCFilter.Rows.Count + 3
                    ColumnSet1.Add(Me.grdSaved.RootTable.Columns(VarianceColumm), 0, i)
                    ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    i += 1
                End If
                For Each RowCC As DataRow In dtCCFilter.Rows
                    Dim dtEdit As DataTable = CType(Me.grdSaved.DataSource, DataTable)
                    Dim ActualName As String = RowCC.Item(0).ToString & " ~ " & "Actual"
                    Dim BudgetName As String = RowCC.Item(0).ToString & " ~ " & "Budget"
                    Dim VarianceName As String = RowCC.Item(0).ToString & " ~ " & "Variance"
                    For Each Row As DataRow In dtEdit.Rows
                        Row.BeginEdit()
                        If Row.Table.Columns.Contains(ActualName) Then
                            Row.Item(ActualColumm) += Row.Item(ActualName)
                        End If
                        If Row.Table.Columns.Contains(BudgetName) Then
                            Row.Item(BudgetColumm) += Row.Item(BudgetName)
                        End If
                        If Row.Table.Columns.Contains(VarianceName) Then
                            Row.Item(VarianceColumm) += Row.Item(VarianceName)
                        End If
                        Row.EndEdit()
                    Next
                    ''
                    i += 1
                Next
                Me.grdSaved.UpdateData()
                ''' END TASK TFS3342
            Next
            Me.grdSaved.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            Dim ColumnSet2 As New Janus.Windows.GridEX.GridEXColumnSet
            Me.grdSaved.RootTable.ColumnSetRowCount = 1
            ColumnSet2 = Me.grdSaved.RootTable.ColumnSets.Add
            ColumnSet2.ColumnCount = 3
            ColumnSet2.Caption = "Totals"
            ColumnSet2.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Total ~ Actual"), 0, 0)
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Total ~ Budget"), 0, 1)
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Total ~ Variance"), 0, 2)


            'CreateColumnTotalOH()
            'CreateColumnOperatingProfit()
            'Dim Counter As Integer = 0
            'Dim Revenue As Double = 0
            'Dim CostOfSales As Double = 0
            'Dim GrossPercentage As Double = 0
            'Dim TotalRows As Integer = 0
            'For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetRows
            '    If Row. < 1 Then
            '        TotalRows = Row.GetChildRows.Length
            '        Revenue = Row.GetSubTotal(grdSaved.RootTable.Columns("[Total ~ Actual]"), Janus.Windows.GridEX.AggregateFunction.Sum)
            '    Else
            '        TotalRows += Row.GetChildRows.Length

            '        CostOfSales = Row.GetSubTotal(grdSaved.RootTable.Columns("[Total ~ Actual]"), Janus.Windows.GridEX.AggregateFunction.Sum)
            '    End If
            '    Dim Index As Integer = Row.RowIndex
            '    Counter += 1
            '    If Counter > 1 Then
            '        Exit For
            '    End If
            'Next
            'Dim dtSaved As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            'Dim _row As DataRow = dtSaved.NewRow
            '_row.Item(0) = "3-Gross Profit"
            '_row.Item(1) = "3-Gross Profit"
            '_row.Item(2) = ""
            '_row.Item(3) = 0
            '_row.Item(4) = ""
            '_row.Item(5) = 0
            '_row.Item(6) = "Gross Profit"

            'GrossPercentage = Revenue - CostOfSales
            'GrossPercentage = GrossPercentage / Revenue
            'GrossPercentage = GrossPercentage * 100
            '_row.Item(7) = Revenue - CostOfSales

            'For i As Integer = 8 To dtSaved.Columns.Count - 1
            '    _row.Item(i) = 0
            'Next

            'dtSaved.Rows.InsertAt(_row, TotalRows)



            Dim values As Double = 0
            ApplyGridSettings()
            CreateColumnGrossProfit()
            CreateColumnTotalOH()
            CreateColumnOperatingProfit()
            CreateColumnTotalProfit()
            CreateColumnProfitBeforeTax()
            CreateColumnProfitAfterTax()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreateColumnGrossProfit()
        Try
            Dim Counter As Integer = 0
            Dim Revenue As Double = 0
            Dim CostOfSales As Double = 0
            Dim GrossPercentage As Double = 0
            Dim TotalRows As Integer = 0

            Dim RevenueMonthsValue As New List(Of KeyValuePair(Of Int32, Double))
            Dim CostOfSalesValue As New List(Of KeyValuePair(Of Int32, Double))

            Dim dtSaved As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            Dim _row As DataRow = dtSaved.NewRow

            For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetRows
                If Row.GetChildRows.Length > 0 Then
                    Dim _DataRow As Janus.Windows.GridEX.GridEXRow = Row.GetChildRows.GetValue(0)
                    Dim PLNote1 As String = _DataRow.Cells("PLNote").Value.ToString
                    If PLNote1.ToUpper = "01-REVENUE" Then
                        TotalRows = Row.GetChildRows.Length
                        Revenue = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                        For i As Integer = 13 To dtSaved.Columns.Count - 4
                            If IsDBNull(_row.Item(i)) = True Then
                                _row.Item(i) = 0
                            End If
                            _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                        Next
                    ElseIf PLNote1.ToUpper = "02-COST OF SALES" Then
                        TotalRows += Row.GetChildRows.Length
                        CostOfSales = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                        For i As Integer = 13 To dtSaved.Columns.Count - 4
                            If IsDBNull(_row.Item(i)) = True Then
                                _row.Item(i) = 0
                            End If
                            _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                        Next
                    End If
                End If
            Next

            _row.Item(0) = "03"
            _row.Item(1) = "03-Gross Profit"
            _row.Item(2) = ""
            _row.Item(3) = 0
            _row.Item(4) = ""
            _row.Item(5) = 0
            _row.Item(6) = "Gross Profit"

            GrossPercentage = Revenue - CostOfSales
            GrossPercentage = GrossPercentage / Revenue
            GrossPercentage = GrossPercentage * 100
            _row.Item(7) = Revenue + CostOfSales
            _row.Item(8) = 0
            _row.Item(9) = 0
            _row.Item(10) = 0

            _row.Item(11) = 0
            _row.Item(12) = 0

            'For i As Integer = 13 To dtSaved.Columns.Count - 1
            '    _row.Item(i) = 0
            'Next

            dtSaved.Rows.InsertAt(_row, TotalRows)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateColumnTotalOH()
        Try
            Dim Counter As Integer = 0
            Dim OverheadsDirect As Double = 0
            Dim OverheadsIndirect As Double = 0
            Dim GrossPercentage As Double = 0
            Dim TotalRows As Integer = 0
            Dim dtSaved As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            Dim _row As DataRow = dtSaved.NewRow

            For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetRows
                Dim _DataRow As Janus.Windows.GridEX.GridEXRow = Row.GetChildRows.GetValue(0)
                Dim PLNote1 As String = _DataRow.Cells("PLNote").Value.ToString
                If PLNote1.ToUpper = "04-OVERHEADS-DIRECT" Then
                    TotalRows = Row.GetChildRows.Length
                    OverheadsDirect = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 13 To dtSaved.Columns.Count - 4
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "05-OVERHEADS-INDIRECT" Then
                    TotalRows += Row.GetChildRows.Length
                    OverheadsIndirect = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 13 To dtSaved.Columns.Count - 4
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                End If
                'Dim Index As Integer = Row.RowIndex
                'Counter += 1
                'If Counter > 1 Then
                '    Exit For
                'End If
            Next
            _row.Item(0) = "06"
            _row.Item(1) = "06-Total OH"
            _row.Item(2) = ""
            _row.Item(3) = 0
            _row.Item(4) = ""
            _row.Item(5) = 0
            _row.Item(6) = "Total OH"

            'GrossPercentage = Revenue - CostOfSales
            'GrossPercentage = GrossPercentage / Revenue
            'GrossPercentage = GrossPercentage * 100
            _row.Item(7) = 0
            _row.Item(8) = OverheadsDirect + OverheadsIndirect
            _row.Item(9) = 0
            _row.Item(10) = 0
            _row.Item(11) = 0
            _row.Item(12) = 0
            'For i As Integer = 9 To dtSaved.Columns.Count - 1
            '    _row.Item(i) = 0
            'Next
            dtSaved.Rows.InsertAt(_row, TotalRows)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub CreateColumnOperatingProfit()
        Try
            Dim Counter As Integer = 0
            Dim Revenue As Double = 0
            Dim CostOfSales As Double = 0
            Dim GrossPercentage As Double = 0
            Dim TotalRows As Integer = 0
            Dim OverheadsDirect As Double = 0
            Dim OverheadsIndirect As Double = 0
            Dim NotOperatingIncome As Double = 0
            Dim GrossProfit As Double = 0
            Dim TotalOH As Double = 0

            Dim dtSaved As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            Dim _row As DataRow = dtSaved.NewRow

            For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetRows
                Dim _DataRow As Janus.Windows.GridEX.GridEXRow = Row.GetChildRows.GetValue(0)
                Dim PLNote1 As String = _DataRow.Cells("PLNote").Value.ToString

                If PLNote1.ToUpper = "03-GROSS PROFIT" Then
                    TotalRows += Row.GetChildRows.Length
                    GrossProfit = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 13 To dtSaved.Columns.Count - 4
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "06-TOTAL OH" Then
                    TotalRows = Row.GetChildRows.Length
                    TotalOH = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 13 To dtSaved.Columns.Count - 4
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                End If
            Next

            _row.Item(0) = "07"
            _row.Item(1) = "07-Operating Profit"
            _row.Item(2) = ""
            _row.Item(3) = 0
            _row.Item(4) = ""
            _row.Item(5) = 0
            _row.Item(6) = "Operating Profit"
            _row.Item(7) = 0
            _row.Item(8) = 0
            _row.Item(9) = 0
            _row.Item(10) = GrossProfit + TotalOH
            _row.Item(11) = 0
            _row.Item(12) = 0
            'For i As Integer = 13 To dtSaved.Columns.Count - 1
            '    _row.Item(i) = 0
            'Next
            dtSaved.Rows.Add(_row)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 9
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateColumnTotalProfit()
        Try
            Dim Counter As Integer = 0
            Dim OperatingProfit As Double = 0
            Dim CostOfSales As Double = 0
            Dim GrossPercentage As Double = 0
            Dim TotalRows As Integer = 0

            Dim TotalProfit As Integer = 0
            Dim dt As DataTable = Me.grdSaved.DataSource


            Dim dtSaved As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            Dim _row As DataRow = dtSaved.NewRow
            For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetRows

                Dim _DataRow As Janus.Windows.GridEX.GridEXRow = Row.GetChildRows.GetValue(0)
                Dim PLNote1 As String = _DataRow.Cells("PLNote").Value.ToString

                If PLNote1.ToUpper = "07-OPERATING PROFIT" Then
                    TotalRows = Row.GetChildRows.Length
                    OperatingProfit = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 13 To dtSaved.Columns.Count - 4
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "08-NOT OPERATING INCOME" Then ''Not Operating income
                    TotalRows += Row.GetChildRows.Length
                    TotalProfit = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 13 To dtSaved.Columns.Count - 4
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                End If
                'Dim Index As Integer = Row.RowIndex
                'Counter += 1
                'If Counter > 1 Then
                '    Exit For
                'End If
            Next

            _row.Item(0) = "09"
            _row.Item(1) = "09-Total Profit"
            _row.Item(2) = ""
            _row.Item(3) = 0
            _row.Item(4) = ""
            _row.Item(5) = 0
            _row.Item(6) = "Total Profit"
            GrossPercentage = OperatingProfit + CostOfSales
            GrossPercentage = GrossPercentage / OperatingProfit
            GrossPercentage = GrossPercentage * 100
            _row.Item(7) = 0
            _row.Item(8) = 0
            _row.Item(9) = OperatingProfit + TotalProfit
            _row.Item(10) = 0
            _row.Item(11) = 0
            _row.Item(12) = 0
            'For i As Integer = 10 To dtSaved.Columns.Count - 1
            '    _row.Item(i) = 0
            'Next
            dtSaved.Rows.Add(_row)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 12
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateColumnProfitBeforeTax()
        Try
            Dim Counter As Integer = 0
            Dim Revenue As Double = 0
            Dim CostOfSales As Double = 0
            Dim GrossPercentage As Double = 0
            Dim TotalRows As Integer = 0
            Dim OperatingProfit As Double = 0
            Dim FinanceCost As Double = 0
            Dim NotOperatingIncome As Double = 0
            Dim dtSaved As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            Dim _row As DataRow = dtSaved.NewRow
            For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetRows
                Dim _DataRow As Janus.Windows.GridEX.GridEXRow = Row.GetChildRows.GetValue(0)
                Dim PLNote1 As String = _DataRow.Cells("PLNote").Value.ToString
                If PLNote1.ToUpper = "09-TOTAL PROFIT" Then
                    TotalRows += Row.GetChildRows.Length
                    OperatingProfit = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 13 To dtSaved.Columns.Count - 4
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "10-FINANCE COST" Then
                    TotalRows = Row.GetChildRows.Length
                    FinanceCost = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 13 To dtSaved.Columns.Count - 4
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                Else
                End If
            Next

            _row.Item(0) = "11"
            _row.Item(1) = "11-Profit Before Tax"
            _row.Item(2) = ""
            _row.Item(3) = 0
            _row.Item(4) = ""
            _row.Item(5) = 0
            _row.Item(6) = "Profit Before Tax"
            _row.Item(7) = 0
            _row.Item(8) = 0
            _row.Item(9) = 0
            _row.Item(10) = 0
            _row.Item(11) = OperatingProfit + FinanceCost
            _row.Item(12) = 0
            'For i As Integer = 12 To dtSaved.Columns.Count - 1
            '    _row.Item(i) = 0
            'Next
            dtSaved.Rows.Add(_row)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '''

    Private Sub CreateColumnProfitAfterTax()
        Try
            Dim Counter As Integer = 0
            Dim Revenue As Double = 0
            Dim CostOfSales As Double = 0
            Dim GrossPercentage As Double = 0
            Dim TotalRows As Integer = 0
            Dim OperatingProfit As Double = 0
            Dim PROFITBEFORETAX As Double = 0
            Dim Taxation As Double = 0
            Dim dtSaved As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            Dim _row As DataRow = dtSaved.NewRow
            For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetRows
                Dim _DataRow As Janus.Windows.GridEX.GridEXRow = Row.GetChildRows.GetValue(0)
                Dim PLNote1 As String = _DataRow.Cells("PLNote").Value.ToString
                If PLNote1.ToUpper = "11-PROFIT BEFORE TAX" Then
                    TotalRows = Row.GetChildRows.Length
                    PROFITBEFORETAX = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 13 To dtSaved.Columns.Count - 4
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "12-TAXATION" Then
                    TotalRows += Row.GetChildRows.Length
                    Taxation = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 13 To dtSaved.Columns.Count - 4
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) -= Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                End If
            Next

            _row.Item(0) = "13"
            _row.Item(1) = "13-Profit After Tax"
            _row.Item(2) = ""
            _row.Item(3) = 0
            _row.Item(4) = ""
            _row.Item(5) = 0
            _row.Item(6) = "Profit After Tax"
            _row.Item(7) = 0
            _row.Item(8) = 0
            _row.Item(9) = 0
            _row.Item(10) = 0
            _row.Item(11) = 0
            _row.Item(12) = PROFITBEFORETAX + Taxation

            'For i As Integer = 13 To dtSaved.Columns.Count - 1
            '    _row.Item(i) = 0
            'Next
            dtSaved.Rows.Add(_row)

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
            Me.cmbPeriod.SelectedIndex = 0
            Me.cmbPeriod.Text = "Current Month"
            FillCombos("HeadCostCenter")
            FillCombos("CostCenter")
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Head Wise Budget Comparison PL Summary" & Chr(10) & "From Date : " & Me.dtpFromDate.Value.ToString("dd-MM-yyyy") & " To Date : " & Me.dtpToDate.Value.ToString("dd-MM-yyyy")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPLSubSubAccountWiseSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            IsOpenForm = True
            btnNew_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            Label3.Text = ""
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
            'TFS3343: Waqar Raza: Added this line to make all column link for tracking
            'Start TFS3343
            If e.Column.Key = "Total ~ Actual" Then
                'End TFS3343
                If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                    frmMain.LoadControl("rptTrialBalance")
                End If
                rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
                rptTrialBalance.cmbAccountTo.Rows(0).Activate()
                rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
                rptTrialBalance.DateTimePicker1.Value = Me.dtpFromDate.Value
                rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
                Dim CostCenterIds As String = ""
                If chkShowWithOutCostCenter.Checked = True Then
                    If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso lstCostCenter.Enabled = True Then
                        CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                        rptTrialBalance.CostID = "0," + CostCenterIds
                    Else
                        CostCenterIds = "0"
                    End If
                Else
                    If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                        CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                        rptTrialBalance.CostID = "0," + CostCenterIds
                    Else
                        CostCenterIds = ""
                    End If
                End If
                rptTrialBalance.Tracking = True
                rptTrialBalance.GetSubSubWiseDetailAccountsTrial(Val(Me.grdSaved.GetRow.Cells(grd.SubSubId).Value.ToString), CostCenterIds)
                frmMain.LoadControl("rptTrialBalance")
                'TFS3343: Waqar Raza: Added this line to make all column link for tracking
                'Start TFS3343
            Else
                Dim str As String
                str = e.Column.Key
                Dim words As String() = str.Split("~")
                Dim CostCenterName As String = words(0)
                Dim Query As String
                Dim dt As DataTable
                Dim CCID As String
                Query = "select CostCenterId from tblDefCostCenter where Name = '" & CostCenterName & "'"
                dt = GetDataTable(Query)
                If dt.Rows.Count > 0 Then
                    CCID = dt.Rows(0).Item("CostCenterId")
                Else
                    msg_Information("Tracking is not applied on Totals.")
                    Exit Sub
                End If

                If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                    frmMain.LoadControl("rptTrialBalance")
                End If
                rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
                rptTrialBalance.cmbAccountTo.Rows(0).Activate()
                rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
                rptTrialBalance.DateTimePicker1.Value = Me.dtpFromDate.Value
                rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
                Dim CostCenterIds As String = ""
                If CCID > 0 Then
                    rptTrialBalance.CostID = CCID
                Else
                    CCID = ""
                End If
                rptTrialBalance.Tracking = True
                rptTrialBalance.GetSubSubWiseDetailAccountsTrial(Val(Me.grdSaved.GetRow.Cells(grd.SubSubId).Value.ToString), CCID)
                frmMain.LoadControl("rptTrialBalance")
            End If
            'End TFS3343
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkShowWithOutCostCenter_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowWithOutCostCenter.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If chkShowWithOutCostCenter.Checked Then
                Me.lstHeadCostCenter.Enabled = False
                Me.lstCostCenter.Enabled = False
            Else
                Me.lstHeadCostCenter.Enabled = True
                Me.lstCostCenter.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class