'Ali Faisal : TFS2053 : Add report to show the PL Sub Sub Account Wise Summary
''TASK TFS3425 Muhammad Amin done on 05-06-2018 : Show record of zero or null cost centers.
Imports System
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmPLSubSubAccountWiseSummary
    Implements IGeneral

    Dim RowCounter As Integer = 0
    Dim IsOpenForm As Boolean = False
    ''' <summary>
    ''' Ali Faisal : TFS2053 : Set indexing of Records grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grd
        SubId
        SubCode
        AccountType
        PLNoteId
        PLNote
        SubSubId
        SubSubTitle
        NetAmount
        GrossAmount
        TotalProfit
        TotalOH
        OperatingProfit
        ProfitBeforeTaxation
        ProfitAfterTaxation
        Count
    End Enum
    ''' <summary>
    ''' Ali Faisal : TFS2053 : Apply grid settings to hide some columns and formating too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns(grd.SubId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SubCode).Visible = False
            Me.grdSaved.RootTable.Columns(grd.AccountType).Visible = False
            Me.grdSaved.RootTable.Columns(grd.PLNote).Visible = False
            Me.grdSaved.RootTable.Columns(grd.PLNoteId).Visible = False
            'Me.grdSaved.RootTable.Columns(grd.SubTitle).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SubSubId).Visible = False
            'Me.grdSaved.RootTable.Columns(grd.GrossAmount).Visible = False
            Me.grdSaved.RootTable.Columns(grd.GrossAmount).Visible = False
            Me.grdSaved.RootTable.Columns(grd.TotalProfit).Visible = False
            Me.grdSaved.RootTable.Columns(grd.TotalOH).Visible = False
            Me.grdSaved.RootTable.Columns(grd.OperatingProfit).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ProfitBeforeTaxation).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ProfitAfterTaxation).Visible = False
            'TotalProfit()
            'TotalOH()
            'OperatingProfit()
            'ProfitBeforeTaxation()
            'ProfitAfterTaxation()
            Me.grdSaved.RootTable.Columns(grd.SubSubTitle).Width = 250
            'Me.grdSaved.RootTable.Columns(grd.NetAmount).FormatString = "N" & DecimalPointInValue
            'Me.grdSaved.RootTable.Columns(grd.NetAmount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdSaved.RootTable.Columns(grd.NetAmount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Me.grdSaved.RootTable.Columns(grd.NetAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            ''Below three rows are commented on 11-05-2018
            'Dim grdGroupBy As New Janus.Windows.GridEX.GridEXGroup(Me.grdSaved.RootTable.Columns(grd.PLNoteId))
            'grdGroupBy.GroupPrefix = String.Empty
            'Me.grdSaved.RootTable.Groups.Add(grdGroupBy)

            Dim grdPLNote As New Janus.Windows.GridEX.GridEXGroup(Me.grdSaved.RootTable.Columns(grd.PLNote))
            grdPLNote.GroupPrefix = String.Empty
            Me.grdSaved.RootTable.Groups.Add(grdPLNote)
            'For Each Group As Janus.Windows.GridEX.GridEXGroup In Me.grdSaved.RootTable.Groups
            '    Group.Column.v()
            'Next
            For Each col As Janus.Windows.GridEX.GridEXColumn In grdSaved.RootTable.Columns
                If col.Index > 6 Then
                    col.FormatString = "N" & DecimalPointInValue
                    col.TotalFormatString = "N" & DecimalPointInValue
                    col.Width = 80
                    col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.ColumnType = Janus.Windows.GridEX.ColumnType.Link
                End If
            Next
            ''1
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
            ''
            ''
            'Me.grdSaved.RootTable.Columns("03-Gross Profit").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            'Me.grdSaved.RootTable.Columns(grd.SubSubTitle).ColumnType = Janus.Windows.GridEX.ColumnType.Link
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
            ElseIf Condition = "Company" Then
                FillDropDown(Me.cmbCompany, "SELECT ISNULL(CompanyId,0) CompanyId, CompanyName FROM CompanyDefTable ORDER BY CompanyId ASC", True)
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
        Dim strMonths As String = ""
        Dim strMonthsIsNull As String = ""
        Dim strExpression As String = ""
        Try
            For i As Integer = 0 To DateDiff(DateInterval.Month, Me.dtpFromDate.Value, Me.dtpToDate.Value)
                If i <> 0 Then
                    strMonths = strMonths & ", "
                    strMonthsIsNull = strMonthsIsNull & ", "
                    strExpression = strExpression & " + "
                End If

                strMonthsIsNull = strMonthsIsNull & "ISNULL([" & dtpFromDate.Value.AddMonths(i).ToString("MMMM-yyyy") & "],0) as [" & dtpFromDate.Value.AddMonths(i).ToString("MMMM-yyyy") & "]"
                strMonths = strMonths & "[" & dtpFromDate.Value.AddMonths(i).ToString("MMMM-yyyy") & "]"
                strExpression = strExpression & "[" & dtpFromDate.Value.AddMonths(i).ToString("MMMM-yyyy") & "]"
            Next
            'Dim strData As String = ""
            'Dim dtData As New DataTable
            'strData = "SELECT SubId, SubCode, AccountType, PLNoteId, PLNote, SubSubId, SubSubTitle, Convert(float, 0) AS TotalAmount , Convert(float, 0) AS GrossAmount, " & strMonthsIsNull & " FROM ( SELECT COA.main_sub_id SubId, COA.sub_code SubCode, COA.account_type AccountType, CONVERT(varchar(20),COA.PL_note_id) + '-' + COA.PL_Note_Title AS PLNoteId, COA.PL_Note_Title PLNote, COA.main_sub_sub_id SubSubId, COA.sub_sub_title SubSubTitle, (-1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS NetAmount, DATENAME(M, V.voucher_date) + '-' + CONVERT(varchar(4), DATEPART(yyyy, V.voucher_date)) AS Month_Year " _
            '            & "FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID " _
            '            & "WHERE (COA.PL_Note_Title <> '') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            'If Me.lstCostCenter.SelectedIDs.Length > 0 Then
            '    strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            'End If
            'If Me.cmbCompany.SelectedValue > 0 Then
            '    strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            'End If
            'strData += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, V.voucher_date ) AS Account " _
            '            & "PIVOT ( Sum(NetAmount) for Month_Year IN (" & strMonths & ")) AS PVT " _
            '            & "ORDER BY PLNoteId"
            'dtData = GetDataTable(strData)



            Dim strData As String = ""
            Dim dtData As New DataTable

            ''Revenue section
            strData = "SELECT SubId, SubCode, AccountType, PLNoteId, PLNote, SubSubId, SubSubTitle, Convert(float, 0) AS TotalAmount , Convert(float, 0) AS GrossAmount, Convert(float, 0) AS TotalProfit, Convert(float, 0) AS TotalOH, Convert(float, 0) AS OperatingProfit, Convert(float, 0) AS ProfitBeforeTaxation, Convert(float, 0) AS ProfitAfterTaxation, " & strMonthsIsNull & " FROM ( SELECT COA.main_sub_id SubId, COA.sub_code SubCode, COA.account_type AccountType, 01 AS PLNoteId, '01-Revenue' PLNote, COA.main_sub_sub_id SubSubId, COA.sub_sub_title SubSubTitle, (-1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS NetAmount, DATENAME(M, V.voucher_date) + '-' + CONVERT(varchar(4), DATEPART(yyyy, V.voucher_date)) AS Month_Year " _
                        & "FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID " _
                        & "WHERE (COA.PL_Note_Title = 'Sales Net') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If

            ''Start TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''End TASK TFS3425




            If Me.cmbCompany.SelectedValue > 0 Then
                strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            End If
            ''Cost Of Goods Sold
            strData += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, V.voucher_date " _
              & " Union All SELECT COA.main_sub_id SubId, COA.sub_code SubCode, COA.account_type AccountType, 02 AS PLNoteId , '02-Cost Of Sales' AS PLNote, COA.main_sub_sub_id SubSubId, COA.sub_sub_title SubSubTitle, (-1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS NetAmount, DATENAME(M, V.voucher_date) + '-' + CONVERT(varchar(4), DATEPART(yyyy, V.voucher_date)) AS Month_Year " _
                        & "FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID  " _
                        & "WHERE (COA.PL_Note_Title = 'Cost Of Goods Sold')  AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If

            ''Start TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''End TASK TFS3425
            If Me.cmbCompany.SelectedValue > 0 Then
                strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            End If

            ''OverHeads Direct
            strData += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, V.voucher_date " _
              & " Union All SELECT COA.main_sub_id SubId, COA.sub_code SubCode, COA.account_type AccountType, 04 AS PLNoteId , '04-Overheads-Direct' AS PLNote, COA.main_sub_sub_id SubSubId, COA.sub_sub_title SubSubTitle, (-1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS NetAmount, DATENAME(M, V.voucher_date) + '-' + CONVERT(varchar(4), DATEPART(yyyy, V.voucher_date)) AS Month_Year " _
                        & "FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID  " _
                        & "WHERE (COA.PL_Note_Title IN ('Administrative Exp', 'Selling Exp', 'Operating and Other Expences.')) AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If

            ''Start TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''End TASK TFS3425
            If Me.cmbCompany.SelectedValue > 0 Then
                strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            End If

            ''OverHeads Indirect
            strData += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, V.voucher_date " _
                      & " Union All SELECT COA.main_sub_id SubId, COA.sub_code SubCode, COA.account_type AccountType, 05 AS PLNoteId, '05-Overheads-Indirect' AS  PLNote, COA.main_sub_sub_id SubSubId, COA.sub_sub_title SubSubTitle, (-1*SUM( CASE WHEN ISNULL(Per, 0) > 0 THEN ISNULL(VD.debit_amount, 0)*Per/100-ISNULL(VD.credit_amount, 0)*Per/100 ELSE ISNULL(VD.debit_amount, 0)-ISNULL(VD.credit_amount, 0) END)) AS NetAmount, DATENAME(M, V.voucher_date) + '-' + CONVERT(varchar(4), DATEPART(yyyy, V.voucher_date)) AS Month_Year " _
                        & "FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id Inner JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID LEFT OUTER JOIN (SELECT Bifurcation.FromCostCenterId As LogicalCostCenterId, Sum(IsNull(Detail.AmountPercentage, 0)) AS Per FROM LogicalBifurcationDetail AS Detail INNER JOIN LogicalBifurcation AS Bifurcation ON Detail.LogicalBifurcationId = Bifurcation.LogicalBifurcationId WHERE Bifurcation.LogicalBifurcationId IN (SELECT Max(LogicalBifurcationId) AS LogicalBifurcationId FROM LogicalBifurcation Group By FromCostCenterId) " & IIf(Me.lstCostCenter.SelectedIDs.Length > 0, " AND ToCostCenterId IN (" & Me.lstCostCenter.SelectedIDs & ") ", "") & " Group By Bifurcation.FromCostCenterId) AS Logical ON Proj.CostCenterID = Logical.LogicalCostCenterId " _
                        & "WHERE (COA.PL_Note_Title  IN ('Administrative Exp', 'Selling Exp', 'Operating and Other Expences.', 'Financial & Other Charges-Net')) And IsNull(Proj.IsLogical, 0) = 1 AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If

            ''Start TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''End TASK TFS3425
            If Me.cmbCompany.SelectedValue > 0 Then
                strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            End If

            '' Not operating income
            strData += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, V.voucher_date " _
                 & " Union All SELECT COA.main_sub_id SubId, COA.sub_code SubCode, COA.account_type AccountType, 08 AS PLNoteId,  '08-None Operating Income' AS PLNote, COA.main_sub_sub_id SubSubId, COA.sub_sub_title SubSubTitle, (-1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS NetAmount, DATENAME(M, V.voucher_date) + '-' + CONVERT(varchar(4), DATEPART(yyyy, V.voucher_date)) AS Month_Year " _
                        & "FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID  " _
                        & "WHERE (COA.PL_note_id = 3)  AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If

            ''Start TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''End TASK TFS3425
            If Me.cmbCompany.SelectedValue > 0 Then
                strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            End If

            ''Financial
            strData += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, V.voucher_date " _
                 & " Union All SELECT COA.main_sub_id SubId, COA.sub_code SubCode, COA.account_type AccountType, 10 AS PLNoteId, '10-Finance Cost' AS PLNote, COA.main_sub_sub_id SubSubId, COA.sub_sub_title SubSubTitle, (-1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS NetAmount, DATENAME(M, V.voucher_date) + '-' + CONVERT(varchar(4), DATEPART(yyyy, V.voucher_date)) AS Month_Year " _
                        & "FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID  " _
                        & "WHERE (COA.PL_note_id = 7) AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If

            ''Start TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''End TASK TFS3425
            If Me.cmbCompany.SelectedValue > 0 Then
                strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            End If
            ''Taxation
            strData += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, V.voucher_date " _
                 & " Union All SELECT COA.main_sub_id SubId, COA.sub_code SubCode, COA.account_type AccountType, 12 AS PLNoteId , '12-Taxation' AS PLNote, COA.main_sub_sub_id SubSubId, COA.sub_sub_title SubSubTitle, (-1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS NetAmount, DATENAME(M, V.voucher_date) + '-' + CONVERT(varchar(4), DATEPART(yyyy, V.voucher_date)) AS Month_Year " _
                        & "FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID  " _
                        & "WHERE (COA.PL_Note_Title = 'Taxation') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If

            ''Start TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''End TASK TFS3425
            If Me.cmbCompany.SelectedValue > 0 Then
                strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            End If




            strData += " GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, COA.account_type, COA.sub_code, COA.sub_title, COA.main_sub_id, V.voucher_date) AS Account " _
                        & "PIVOT (Sum(NetAmount) for Month_Year IN (" & strMonths & ")) AS PVT " _
                        & "ORDER BY PLNoteId"
            dtData = GetDataTable(strData)

            'strExpression += " + " & "GrossAmount"
            'strExpression += " + " & "TotalOH"
            'strExpression += " + " & "TotalProfit"
            'strExpression += " + " & "OperatingProfit"
            'strExpression += " + " & "ProfitBeforeTaxation"
            'strExpression += " + " & "ProfitAfterTaxation"

            ' Convert(float, 0) AS OperatingProfit, Convert(float, 0) AS ProfitBeforeTaxation, Convert(float, 0) AS ProfitAfterTaxation






            dtData.Columns(7).Expression = strExpression



            dtData.AcceptChanges()
            Me.grdSaved.DataSource = dtData
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()

            CreateColumnGrossProfit()
            CreateColumnTotalOH()
            CreateColumnOperatingProfit()
            CreateColumnTotalProfit()
            CreateColumnProfitBeforeTax()
            CreateColumnProfitAfterTax()
            'Me.grdSaved.RootTable.Groups.Clear()
            'Dim dtSort As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            'dtSort.DefaultView.Sort = "PLNoteId ASC"
            'dtSort = dtSort.DefaultView.ToTable()
            'dtSort.AcceptChanges()
            'Me.grdSaved.DataSource = dtSort
            ''

            'UltraGridColumn col = new UltraGridColumn();
            'ultraGrid1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            'col.Caption = "column1"
            'col.DataMember = ISOCntrTable.IsoCode.ColumnName;
            'col.Key = "column1";
            '    col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'col.FormatString = "c";
            'col.TotalFormatMode = Janus.Windows.GridEX.FormatMode.UseStringFormat;
            'col.TotalFormatString = "SUM={0:c}";
            'ultraGrid1.Columns.Add(col);
            'grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            'grd.RootTable.Columns[10].AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum;


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
            FillCombos("Company")
            Me.cmbCompany.SelectedValue = 0
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Monthly Profit and Loss Summary" & Chr(10) & "From Date : " & Me.dtpFromDate.Value.ToString("dd-MM-yyyy") & " To Date : " & Me.dtpToDate.Value.ToString("dd-MM-yyyy")
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
            Label3.Text = dtpFromDate.Value.ToString("dd-MM-yyyy") + " To " + dtpToDate.Value.ToString("dd-MM-yyyy")
            Me.UltraTabControl1.Tabs(1).Selected = True
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
            If e.Column.Key = "TotalAmount" Then
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
            Else
                Dim str As String
                str = e.Column.Key
                Dim words As String() = str.Split("-")
                Dim Month As String = words(0)
                Dim Year As String = words(1)
                Dim YearNo As Integer = Val(Year)
                Dim MonthNo As Integer
                If Month = "January" Then
                    MonthNo = 1
                ElseIf Month = "February" Then
                    MonthNo = 2
                ElseIf Month = "March" Then
                    MonthNo = 3
                ElseIf Month = "April" Then
                    MonthNo = 4
                ElseIf Month = "May" Then
                    MonthNo = 5
                ElseIf Month = "June" Then
                    MonthNo = 6
                ElseIf Month = "July" Then
                    MonthNo = 7
                ElseIf Month = "August" Then
                    MonthNo = 8
                ElseIf Month = "September" Then
                    MonthNo = 9
                ElseIf Month = "October" Then
                    MonthNo = 10
                ElseIf Month = "November" Then
                    MonthNo = 11
                ElseIf Month = "December" Then
                    MonthNo = 12
                End If
                If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                    frmMain.LoadControl("rptTrialBalance")
                End If
                rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
                rptTrialBalance.cmbAccountTo.Rows(0).Activate()
                rptTrialBalance.cmbAcLevel.Text = "Sub Sub A/c"
                Dim dtpFromDate As DateTime = CDate(Val(Year) & "-" & Month & "-1")
                Dim dtpToDate As DateTime = CDate(Val(Year) & "-" & Month & "-" & GetDayOfEndMonth(MonthNo, Val(Year)))
                rptTrialBalance.DateTimePicker1.Value = dtpFromDate
                rptTrialBalance.DateTimePicker2.Value = dtpToDate
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
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CreateColumnGrossProfit()
        Try
            Dim Counter As Integer = 0
            Dim Revenue As Double = 0
            Dim CostOfSales As Double = 0
            Dim GrossPercentage As Double = 0
            Dim TotalRows As Integer = 0
            'Dim RevenueMonthsValue As Double = 0
            'Dim CostOfSalesValue As Double = 0

            Dim RevenueMonthsValue As New List(Of KeyValuePair(Of Int32, Double))
            Dim CostOfSalesValue As New List(Of KeyValuePair(Of Int32, Double))

            'Dim CostOfSalesValue As New KeyValuePair(Of Int32, Double)

            Dim dtSaved As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            Dim _row As DataRow = dtSaved.NewRow

            For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetRows

                'PLNote
                'If Counter < 1 Then
                '    TotalRows = Row.GetChildRows.Length
                '    Revenue = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                'Else
                '    TotalRows += Row.GetChildRows.Length

                '    CostOfSales = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                'End If

                'Dim dr As DataRow = Row.DataRow
                'Revenue = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                'Row.BeginEdit()
                'Dim PLNote1 As String = Row.Cells("PLNote").Value.ToString
                'Row.EndEdit()
                Dim _DataRow As Janus.Windows.GridEX.GridEXRow = Row.GetChildRows.GetValue(0)
                Dim PLNote1 As String = _DataRow.Cells("PLNote").Value.ToString
                If PLNote1.ToUpper = "01-REVENUE" Then
                    TotalRows = Row.GetChildRows.Length
                    Revenue = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 14 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "02-COST OF SALES" Then
                    TotalRows += Row.GetChildRows.Length
                    CostOfSales = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 14 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) Then
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
            _row.Item(0) = 0
            _row.Item(1) = ""
            _row.Item(2) = ""
            _row.Item(3) = "03"
            _row.Item(4) = "03-Gross Profit"
            _row.Item(5) = 0
            _row.Item(6) = "Gross Profit"
            GrossPercentage = Revenue - CostOfSales
            GrossPercentage = GrossPercentage / Revenue
            GrossPercentage = GrossPercentage * 100
            _row.Item(7) = 0
            _row.Item(8) = Revenue + CostOfSales
            _row.Item(9) = 0
            _row.Item(10) = 0
            _row.Item(11) = 0
            _row.Item(12) = 0
            _row.Item(13) = 0

            '_row.Item(8) = 0
            'For i As Integer = 14 To dtSaved.Columns.Count - 1
            '    _row.Item(i) = 0
            'Next
            RowCounter = grdSaved.RecordCount
            RowCounter = grdSaved.RowCount
            RowCounter = grdSaved.GetRows.Length

            dtSaved.Rows.InsertAt(_row, RowCounter)
            'dtSaved.Rows.Add(_row)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 8
    ''' </summary>
    ''' <remarks></remarks>
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
                    OverheadsDirect = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 14 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "05-OVERHEADS-INDIRECT" Then
                    TotalRows += Row.GetChildRows.Length
                    OverheadsIndirect = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 14 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) Then
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
          
            _row.Item(0) = 0
            _row.Item(1) = ""
            _row.Item(2) = ""
            _row.Item(3) = "06"
            _row.Item(4) = "06-Total OH"
            _row.Item(5) = 0
            _row.Item(6) = "Total OH"
            'GrossPercentage = Revenue - CostOfSales
            'GrossPercentage = GrossPercentage / Revenue
            'GrossPercentage = GrossPercentage * 100
            _row.Item(7) = 0
            _row.Item(8) = 0
            _row.Item(9) = OverheadsDirect + OverheadsIndirect
            _row.Item(10) = 0
            _row.Item(11) = 0
            _row.Item(12) = 0
            _row.Item(13) = 0

            '_row.Item(8) = 0
           
            RowCounter = grdSaved.RecordCount
            RowCounter = grdSaved.RowCount
            RowCounter = grdSaved.GetRows.Length
            dtSaved.Rows.InsertAt(_row, RowCounter)
            'dtSaved.Rows.Add(_row)

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
            Dim TotalOH As Double = 0
            Dim GrossProfit As Double = 0



            Dim dtSaved As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            Dim _row As DataRow = dtSaved.NewRow
            For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetRows
                Dim _DataRow As Janus.Windows.GridEX.GridEXRow = Row.GetChildRows.GetValue(0)
                Dim PLNote1 As String = _DataRow.Cells("PLNote").Value.ToString
                If PLNote1.ToUpper = "03-GROSS PROFIT" Then
                    TotalRows = Row.GetChildRows.Length
                    GrossProfit = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 14 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "06-TOTAL OH" Then
                    TotalRows += Row.GetChildRows.Length
                    TotalOH = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 14 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                    'ElseIf PLNote1.ToUpper = "04-OVERHEADS-DIRECT" Then
                    '    TotalRows = Row.GetChildRows.Length
                    '    OverheadsDirect = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    '    For i As Integer = 14 To dtSaved.Columns.Count - 1
                    '        If IsDBNull(_row.Item(i)) Then
                    '            _row.Item(i) = 0
                    '        End If
                    '        _row.Item(i) -= Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    '    Next
                    'ElseIf PLNote1.ToUpper = "05-OVERHEADS-INDIRECT" Then
                    '    TotalRows += Row.GetChildRows.Length
                    '    OverheadsIndirect = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    '    For i As Integer = 14 To dtSaved.Columns.Count - 1
                    '        If IsDBNull(_row.Item(i)) Then
                    '            _row.Item(i) = 0
                    '        End If
                    '        _row.Item(i) -= Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    '    Next
                    'ElseIf PLNote1.ToUpper = "08-NOT OPERATING INCOME" Then
                    '    TotalRows += Row.GetChildRows.Length
                    '    NotOperatingIncome = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    '    For i As Integer = 14 To dtSaved.Columns.Count - 1
                    '        If IsDBNull(_row.Item(i)) Then
                    '            _row.Item(i) = 0
                    '        End If
                    '        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    '    Next
                End If
            Next
         
            _row.Item(0) = 0
            _row.Item(1) = ""
            _row.Item(2) = ""
            _row.Item(3) = "07"
            _row.Item(4) = "07-Operating Profit"
            _row.Item(5) = 0
            _row.Item(6) = "Operating Profit"
            'GrossPercentage = Revenue - CostOfSales
            'GrossPercentage = GrossPercentage / Revenue
            'GrossPercentage = GrossPercentage * 100
            _row.Item(7) = 0
            _row.Item(8) = 0
            _row.Item(9) = 0
            _row.Item(10) = 0
            _row.Item(11) = GrossProfit + TotalOH
            _row.Item(12) = 0
            _row.Item(13) = 0
            '_row.Item(14) = 0
            '_row.Item(8) = 0
            'For i As Integer = 12 To dtSaved.Columns.Count - 1
            '    _row.Item(i) = 0
            'Next
            RowCounter = grdSaved.RecordCount
            RowCounter = grdSaved.RowCount
            RowCounter = grdSaved.GetRows.Length

            dtSaved.Rows.InsertAt(_row, RowCounter)
            'dtSaved.Rows.Add(_row)

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
            Dim Revenue As Double = 0
            Dim OperatingProfit As Double = 0
            Dim NotOperatingIncome As Double = 0
            Dim TotalRows As Integer = 0

            Dim TotalProfit As Integer = 0
            'Dim dt As DataTable = Me.grdSaved.DataSource
            Dim dtSaved As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            Dim _row As DataRow = dtSaved.NewRow
            For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetRows

                Dim _DataRow As Janus.Windows.GridEX.GridEXRow = Row.GetChildRows.GetValue(0)
                Dim PLNote1 As String = _DataRow.Cells("PLNote").Value.ToString
                If PLNote1.ToUpper = "08-NONE OPERATING INCOME" Then ''Not Operating income
                    TotalRows += Row.GetChildRows.Length
                    NotOperatingIncome = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 14 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "07-OPERATING PROFIT" Then
                    TotalRows = Row.GetChildRows.Length
                    OperatingProfit = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 14 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                    'ElseIf PLNote1.ToUpper = "02-COST OF SALES" Then
                    '    TotalRows += Row.GetChildRows.Length
                    '    CostOfSales = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    '    For i As Integer = 14 To dtSaved.Columns.Count - 1
                    '        If IsDBNull(_row.Item(i)) Then
                    '            _row.Item(i) = 0
                    '        End If
                    '        _row.Item(i) -= Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    '    Next
                End If
                'Dim Index As Integer = Row.RowIndex
                'Counter += 1
                'If Counter > 1 Then
                '    Exit For
                'End If
            Next
            'Dim dtSaved As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            'Dim _row As DataRow = dtSaved.NewRow
            _row.Item(0) = 0
            _row.Item(1) = ""
            _row.Item(2) = ""
            _row.Item(3) = "09"
            _row.Item(4) = "09-Total Profit"
            _row.Item(5) = 0
            _row.Item(6) = "Total Profit"
            'GrossPercentage = Revenue - CostOfSales
            'GrossPercentage = GrossPercentage / Revenue
            'GrossPercentage = GrossPercentage * 100
            _row.Item(7) = 0
            _row.Item(8) = 0
            _row.Item(9) = NotOperatingIncome + OperatingProfit
            _row.Item(10) = 0
            _row.Item(11) = 0
            _row.Item(12) = 0
            _row.Item(13) = 0
            '_row.Item(8) = 0
            'For i As Integer = 10 To dtSaved.Columns.Count - 1
            '    _row.Item(i) = 0
            'Next
            RowCounter = grdSaved.RecordCount
            RowCounter = grdSaved.RowCount
            RowCounter = grdSaved.GetRows.Length
            dtSaved.Rows.InsertAt(_row, RowCounter)


            Dim dt1 As DataTable = grdSaved.DataSource
            'dtSaved.Rows.Add(_row)

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
            Dim TotalProfit As Double = 0
            Dim NotOperatingIncome As Double = 0
            Dim dtSaved As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            Dim _row As DataRow = dtSaved.NewRow
            For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetRows
                Dim _DataRow As Janus.Windows.GridEX.GridEXRow = Row.GetChildRows.GetValue(0)
                Dim PLNote1 As String = _DataRow.Cells("PLNote").Value.ToString
                If PLNote1.ToUpper = "10-FINANCE COST" Then
                    TotalRows = Row.GetChildRows.Length
                    FinanceCost = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 14 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "09-TOTAL PROFIT" Then
                    TotalRows += Row.GetChildRows.Length
                    TotalProfit = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 14 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                End If
            Next
           
            _row.Item(0) = 0
            _row.Item(1) = ""
            _row.Item(2) = ""
            _row.Item(3) = "11"
            _row.Item(4) = "11-Profit Before Tax"
            _row.Item(5) = 0
            _row.Item(6) = "Profit Before Tax"
            'GrossPercentage = Revenue - CostOfSales
            'GrossPercentage = GrossPercentage / Revenue
            'GrossPercentage = GrossPercentage * 100
            _row.Item(7) = 0
            _row.Item(8) = 0
            _row.Item(9) = 0
            _row.Item(10) = 0
            _row.Item(11) = 0
            _row.Item(12) = TotalProfit + FinanceCost
            _row.Item(13) = 0
            '_row.Item(8) = 0
            'For i As Integer = 13 To dtSaved.Columns.Count - 1
            '    _row.Item(i) = 0
            'Next
            RowCounter = grdSaved.RecordCount
            RowCounter = grdSaved.RowCount
            RowCounter = grdSaved.GetRows.Length
            dtSaved.Rows.InsertAt(_row, RowCounter)
            'dtSaved.Rows.Add(_row)
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
                    PROFITBEFORETAX = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 14 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "12-TAXATION" Then
                    TotalRows += Row.GetChildRows.Length
                    Taxation = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 14 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                End If
            Next
           
            _row.Item(0) = 0
            _row.Item(1) = ""
            _row.Item(2) = ""
            _row.Item(3) = "13"
            _row.Item(4) = "13-Profit After Tax"
            _row.Item(5) = 0
            _row.Item(6) = "Profit After Tax"
            'GrossPercentage = Revenue - CostOfSales
            'GrossPercentage = GrossPercentage / Revenue
            'GrossPercentage = GrossPercentage * 100
            _row.Item(7) = 0
            _row.Item(8) = 0
            _row.Item(9) = 0
            _row.Item(10) = 0
            _row.Item(11) = 0
            _row.Item(12) = 0
            _row.Item(13) = PROFITBEFORETAX + Taxation
            '_row.Item(14) = 0
            '_row.Item(8) = 0
            'For i As Integer = 14 To dtSaved.Columns.Count - 1
            '    _row.Item(i) = 0
            'Next
            RowCounter = grdSaved.RecordCount
            RowCounter = grdSaved.RowCount
            RowCounter = grdSaved.GetRows.Length
            dtSaved.Rows.InsertAt(_row, RowCounter)
            'dtSaved.Rows.Add(_row)
        
        Catch ex As Exception
            Throw ex
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