'Ali Faisal : TFS2053 : Add report to show the PL Sub Sub Account Wise Summary
''TASK TFS3425 Muhammad Amin done on 05-06-2018 : Show record of zero or null cost centers.
Imports System
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmPLSubSubAccountCostCenterWiseSummary
    Implements IGeneral

    Dim IsOpenForm As Boolean = False
    Dim CompanyId As Integer
    ''' <summary>
    ''' Ali Faisal : TFS2053 : Set indexing of Records grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grd
        CostCenterGroup
        CostCenterId
        PLNoteId
        PLNoteTitle
        AccountType
        SubId
        SubCode
        SubSubId
        SubSubTitle
        Count
    End Enum
    ''' <summary>
    ''' Ali Faisal : TFS2053 : Apply grid settings to hide some columns and formating too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns(grd.CostCenterId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.PLNoteId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SubId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SubSubId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.CostCenterGroup).Visible = False
            'Me.grdSaved.RootTable.Columns(grd.SubTitle).Visible = False
            'Me.grdSaved.RootTable.Columns(grd.SubSubCode).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SubSubTitle).Width = 250

            Dim grdGroupBy As New Janus.Windows.GridEX.GridEXGroup(Me.grdSaved.RootTable.Columns(grd.PLNoteTitle))
            grdGroupBy.GroupPrefix = String.Empty
            Me.grdSaved.RootTable.Groups.Add(grdGroupBy)

            For Each col As Janus.Windows.GridEX.GridEXColumn In grdSaved.RootTable.Columns
                If col.Index > 8 Then
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
            Dim FormatCondition As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdSaved.RootTable.Columns(grd.PLNoteTitle), Janus.Windows.GridEX.ConditionOperator.Equal, "03-Gross Profit")
            FormatCondition.FormatStyle = FormatStype
            Me.grdSaved.RootTable.FormatConditions.Add(FormatCondition)
            ''2
            Dim FormatStype1 As New Janus.Windows.GridEX.GridEXFormatStyle()
            FormatStype1.ForeColor = Color.Black
            FormatStype1.FontBold = Janus.Windows.GridEX.TriState.True
            Dim FormatCondition2 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdSaved.RootTable.Columns(grd.PLNoteTitle), Janus.Windows.GridEX.ConditionOperator.Equal, "06-Total OH")
            FormatCondition2.FormatStyle = FormatStype1
            Me.grdSaved.RootTable.FormatConditions.Add(FormatCondition2)
            ''3
            Dim FormatStype3 As New Janus.Windows.GridEX.GridEXFormatStyle()
            FormatStype3.ForeColor = Color.Black
            FormatStype3.FontBold = Janus.Windows.GridEX.TriState.True
            Dim FormatCondition3 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdSaved.RootTable.Columns(grd.PLNoteTitle), Janus.Windows.GridEX.ConditionOperator.Equal, "07-Operating Profit")
            FormatCondition3.FormatStyle = FormatStype3
            Me.grdSaved.RootTable.FormatConditions.Add(FormatCondition3)
            ''4
            Dim FormatStype4 As New Janus.Windows.GridEX.GridEXFormatStyle()
            FormatStype4.ForeColor = Color.Black
            FormatStype4.FontBold = Janus.Windows.GridEX.TriState.True
            Dim FormatCondition4 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdSaved.RootTable.Columns(grd.PLNoteTitle), Janus.Windows.GridEX.ConditionOperator.Equal, "09-Total Profit")
            FormatCondition4.FormatStyle = FormatStype4
            Me.grdSaved.RootTable.FormatConditions.Add(FormatCondition4)
            ''5
            Dim FormatStype5 As New Janus.Windows.GridEX.GridEXFormatStyle()
            FormatStype5.ForeColor = Color.Black
            FormatStype5.FontBold = Janus.Windows.GridEX.TriState.True
            Dim FormatCondition5 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdSaved.RootTable.Columns(grd.PLNoteTitle), Janus.Windows.GridEX.ConditionOperator.Equal, "11-Profit Before Tax")
            FormatCondition5.FormatStyle = FormatStype5
            Me.grdSaved.RootTable.FormatConditions.Add(FormatCondition5)
            ''6
            Dim FormatStype6 As New Janus.Windows.GridEX.GridEXFormatStyle()
            FormatStype6.ForeColor = Color.Black
            FormatStype6.FontBold = Janus.Windows.GridEX.TriState.True
            Dim FormatCondition6 As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdSaved.RootTable.Columns(grd.PLNoteTitle), Janus.Windows.GridEX.ConditionOperator.Equal, "13-Profit After Tax")
            FormatCondition6.FormatStyle = FormatStype6
            Me.grdSaved.RootTable.FormatConditions.Add(FormatCondition6)
            'TFS3343: Waqar Raza: Added this line to make all column link for tracking
            'Start TFS3343
            Me.grdSaved.RootTable.Columns("TotalAmount").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            'End TFS3343
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
        Try
            Dim dt As New DataTable
            Dim str As String = ""
            str = "SELECT IsNull(Proj.CostCenterGroup,'') CostCenterGroup, ISNULL(VD.CostCenterID, 0) AS CostCenterId, IsNull(Proj.Name,'none') AS CostCenter, CONVERT(varchar(20),COA.PL_note_id) + '-' + COA.PL_Note_Title AS PLNoteId, COA.PL_Note_Title AS PlNoteTitle, COA.account_type AS AccountType, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle, SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0)) AS NetAmount " _
                & "FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID " _
                & "WHERE (COA.PL_Note_Title <> '') AND ISNULL(Proj.IsLogical, 0) = 0 AND Proj.Active = 1 AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                str += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                str += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425

            str += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, ISNULL(VD.CostCenterID, 0), Proj.Name, Proj.CostCenterGroup, COA.sub_code, COA.sub_title, COA.account_type, COA.main_sub_id " _
                & "ORDER BY PLNoteId"
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
            Dim strExpression As String = ""
            For q = 0 To dtCC.Rows.Count - 1 Step 0
                If strCC.ToString = "[]" Or strCCIsNull.ToString = "[]" Then
                    strCC = "[(none)]"
                    strCCIsNull = "ISNULL([(none)],0) AS [(none)]"
                    strExpression = "[(none)]"
                End If
                If q <> 0 AndAlso strCC.ToString <> "" Then
                    strCC = strCC & ", "
                    strCCIsNull = strCCIsNull & ", "
                    strExpression = strExpression & " + "
                End If
                strCC = strCC & "[" & dtCC.Rows(q).Item(0) & "]"
                strCCIsNull = strCCIsNull & "ISNULL([" & dtCC.Rows(q).Item(0) & "],0) AS [" & dtCC.Rows(q).Item(0) & "]"
                strExpression = strExpression & "[" & dtCC.Rows(q).Item(0) & "]"
                q += 1
            Next
            Dim strData As String = ""
            Dim dtData As DataTable

            ''''''''''''''''First Query for Revenue'''''''''''''''''

            strData = "SELECT CostCenterGroup, CostCenterId, PLNoteId, PLNoteTitle, AccountType, SubId, SubCode, SubSubId, SubSubTitle, 0 AS TotalAmount, Convert(float, 0) AS GrossAmount, Convert(float, 0) AS TotalProfit, Convert(float, 0) AS TotalOH, Convert(float, 0) AS OperatingProfit, Convert(float, 0) AS ProfitBeforeTaxation, Convert(float, 0) AS ProfitAfterTaxation, " & strCCIsNull & " FROM ( " _
                    & "SELECT '' CostCenterGroup, 0 AS CostCenterId, IsNull(Proj.Name,'none') AS CostCenter, 01 AS PLNoteId, '01-Revenue' AS PlNoteTitle, COA.account_type AS AccountType, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle, -(1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS NetAmount FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title = 'Sales Net') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            If Me.cmbCompany.SelectedValue > 0 Then
                strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            End If

            ''''''''''''''''''''''Second Query'''''''''''''''''''''''
            strData += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_title, COA.main_sub_sub_id, ISNULL(VD.CostCenterID, 0), Proj.Name, Proj.CostCenterGroup, COA.sub_code, COA.sub_title, COA.account_type, COA.main_sub_id "
            strData += "UNION ALL SELECT '' CostCenterGroup, 0 AS CostCenterId, IsNull(Proj.Name,'none') AS CostCenter, 02 AS PLNoteId, '02-Cost Of Sales' AS PlNoteTitle, COA.account_type AS AccountType, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle, -(1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS NetAmount FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title = 'Cost Of Goods Sold') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            If Me.cmbCompany.SelectedValue > 0 Then
                strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            End If
            strData += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_title, COA.main_sub_sub_id, ISNULL(VD.CostCenterID, 0), Proj.Name, Proj.CostCenterGroup, COA.sub_code, COA.sub_title, COA.account_type, COA.main_sub_id "


            '''''''''''''''''''''Third Query'''''''''''''''''''''''''''''

            strData += "UNION ALL SELECT '' CostCenterGroup, 0 AS CostCenterId, IsNull(Proj.Name,'none') AS CostCenter, 04 AS PLNoteId, '04-Overheads-Direct' AS PlNoteTitle, COA.account_type AS AccountType, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle, -(1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS NetAmount FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title IN ('Administrative Exp', 'Selling Exp', 'Operating and Other Expences.')) AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            If Me.cmbCompany.SelectedValue > 0 Then
                strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            End If
            strData += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_title, COA.main_sub_sub_id, ISNULL(VD.CostCenterID, 0), Proj.Name, Proj.CostCenterGroup, COA.sub_code, COA.sub_title, COA.account_type, COA.main_sub_id "

            '''''''''''''''''''''Forth Query''''''''''''''''''''''''''''''

            strData += "UNION ALL SELECT '' CostCenterGroup, 0 AS CostCenterId, IsNull(CC.Name,'none') AS CostCenter, 05 AS PLNoteId, '05-Overheads-Indirect' AS PlNoteTitle, COA.account_type AS AccountType, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle, (-1*SUM( CASE WHEN ISNULL(Per, 0) > 0 THEN ISNULL(VD.debit_amount, 0)*Per/100-ISNULL(VD.credit_amount, 0)*Per/100 ELSE ISNULL(VD.debit_amount, 0)-ISNULL(VD.credit_amount, 0) END)) AS NetAmount FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID LEFT OUTER JOIN (SELECT Detail.ToCostCenterId As ToCostCenterId, Sum(IsNull(Detail.AmountPercentage, 0)) AS Per ,  Bifurcation.FromCostCenterId FROM LogicalBifurcationDetail AS Detail INNER JOIN LogicalBifurcation AS Bifurcation ON Detail.LogicalBifurcationId = Bifurcation.LogicalBifurcationId WHERE Bifurcation.LogicalBifurcationId IN (SELECT Max(LogicalBifurcationId) AS LogicalBifurcationId FROM LogicalBifurcation Group By FromCostCenterId) Group By Detail.ToCostCenterId, FromCostCenterId) AS Logical ON VD.CostCenterID = Logical.FromCostCenterId LEFT OUTER JOIN tblDefCostCenter AS CC ON Logical.ToCostCenterId =  CC.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title IN ('Administrative Exp', 'Selling Exp', 'Operating and Other Expences.', 'Financial & Other Charges-Net')) And IsNull(Proj.IsLogical, 0) = 1 AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            If Me.cmbCompany.SelectedValue > 0 Then
                strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            End If
            strData += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_title, COA.main_sub_sub_id, ISNULL(VD.CostCenterID, 0), CC.Name, Proj.CostCenterGroup, COA.sub_code, COA.sub_title, COA.account_type, COA.main_sub_id "

            '''''''''''''''''''''''Fifth Query''''''''''''''''''''''''''''''''

            strData += "UNION ALL SELECT '' CostCenterGroup, 0 AS CostCenterId, IsNull(Proj.Name,'none') AS CostCenter, 08 AS PLNoteId, '08-Not Operating income' AS PlNoteTitle, COA.account_type AS AccountType, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle, -(1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS NetAmount FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_note_id = 3) AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            If Me.cmbCompany.SelectedValue > 0 Then
                strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            End If
            strData += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_title, COA.main_sub_sub_id, ISNULL(VD.CostCenterID, 0), Proj.Name, Proj.CostCenterGroup, COA.sub_code, COA.sub_title, COA.account_type, COA.main_sub_id "

            '''''''''''''''''''''''Sixth Query'''''''''''''''''''''''''''''''''''

            strData += "UNION ALL SELECT '' CostCenterGroup, 0 AS CostCenterId, IsNull(Proj.Name,'none') AS CostCenter, 10 AS PLNoteId, '10-Finance Cost' AS PlNoteTitle, COA.account_type AS AccountType, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle, -(1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS NetAmount FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_note_id = 7) AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            If Me.cmbCompany.SelectedValue > 0 Then
                strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            End If
            strData += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_title, COA.main_sub_sub_id, ISNULL(VD.CostCenterID, 0), Proj.Name, Proj.CostCenterGroup, COA.sub_code, COA.sub_title, COA.account_type, COA.main_sub_id "

            '''''''''''''''''''''''Seventh Query'''''''''''''''''''''''''''''''''''

            strData += "UNION ALL SELECT '' CostCenterGroup, 0 AS CostCenterId, IsNull(Proj.Name,'none') AS CostCenter, 12 AS PLNoteId, '12-Taxation' AS PlNoteTitle, COA.account_type AS AccountType, COA.main_sub_id AS SubId, COA.sub_code AS SubCode, COA.main_sub_sub_id AS SubSubId, COA.sub_sub_title AS SubSubTitle, -(1*SUM(ISNULL(VD.debit_amount, 0) - ISNULL(VD.credit_amount, 0))) AS NetAmount FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID " _
                    & "WHERE (COA.PL_Note_Title = 'Taxation') AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso Me.chkShowWithOutCostCenter.Checked = False Then
                strData += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''TASK TFS3425
            If Me.chkShowWithOutCostCenter.Checked = True Then
                strData += "AND ISNULL(VD.CostCenterID, 0) IN (" & 0 & ") "
            End If
            ''END TASK TFS3425
            If Me.cmbCompany.SelectedValue > 0 Then
                strData += "AND V.location_id = " & Me.cmbCompany.SelectedValue & " "
            End If

            strData += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_title, COA.main_sub_sub_id, ISNULL(VD.CostCenterID, 0), Proj.Name, Proj.CostCenterGroup, COA.sub_code, COA.sub_title, COA.account_type, COA.main_sub_id ) AS CostCenters " _
                    & "PIVOT (SUM(NetAmount) FOR CostCenter IN (" & strCC & ")) AS PVT " _
                    & "ORDER BY PLNoteId"
            dtData = GetDataTable(strData)

            dtData.Columns(9).Expression = strExpression
            dtData.AcceptChanges()
            Me.grdSaved.DataSource = dtData
            Me.grdSaved.RetrieveStructure()

            Me.grdSaved.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            Dim ColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
            Me.grdSaved.RootTable.ColumnSetRowCount = 1
            ColumnSet = Me.grdSaved.RootTable.ColumnSets.Add
            ColumnSet.ColumnCount = 2
            ColumnSet.Caption = "Account Summary"
            'ColumnSet.Add(Me.grdSaved.RootTable.Columns("PLNoteTitle"), 0, 0)
            'ColumnSet.Add(Me.grdSaved.RootTable.Columns("AccountType"), 0, 1)
            'ColumnSet.Add(Me.grdSaved.RootTable.Columns("SubCode"), 0, 2)
            'ColumnSet.Add(Me.grdSaved.RootTable.Columns("SubTitle"), 0, 0)
            'ColumnSet.Add(Me.grdSaved.RootTable.Columns("SubSubCode"), 0, 1)
            ColumnSet.Add(Me.grdSaved.RootTable.Columns("SubSubTitle"), 0, 0)
            ColumnSet.Add(Me.grdSaved.RootTable.Columns("TotalAmount"), 0, 1)

            For Each RowCCH As DataRow In dtCCH.Rows
                Dim strCCFilter As String = ""
                Dim dtCCFilter As DataTable
                strCCFilter = "SELECT ISNULL(Proj.Name,'none') AS CostCenter " _
                            & "FROM tblVoucherDetail AS VD INNER JOIN tblVoucher AS V ON VD.voucher_id = V.voucher_id INNER JOIN vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter AS Proj ON Proj.CostCenterID = VD.CostCenterID " _
                            & "WHERE (COA.PL_Note_Title <> '') AND IsNull(Proj.IsLogical, 0) = 0 AND Proj.Active = 1 AND (CONVERT(Varchar, V.voucher_date, 102) BETWEEN CONVERT(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102))  " & IIf(RowCCH.Item(0).ToString = "", " AND Proj.CostCenterGroup Is Null ", " AND Proj.CostCenterGroup = '" & RowCCH.Item(0).ToString & "'") & " "
                If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                    strCCFilter += "AND VD.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
                Else
                    strCCFilter += " "
                End If
                strCCFilter += "GROUP BY COA.PL_note_id, COA.PL_Note_Title, COA.sub_sub_code, COA.sub_sub_title, COA.main_sub_sub_id, ISNULL(VD.CostCenterID, 0), Proj.Name, Proj.CostCenterGroup, COA.sub_code, COA.sub_title, COA.account_type, COA.main_sub_id " _
                    & ""
                dtCCFilter = GetDataTable(strCCFilter)
                dtCCFilter = dtCCFilter.DefaultView.ToTable(True, "CostCenter")
                Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
                Me.grdSaved.RootTable.ColumnSetRowCount = 1
                ColumnSet1 = Me.grdSaved.RootTable.ColumnSets.Add
                ColumnSet1.Caption = RowCCH.Item(0).ToString
                Dim i As Integer = 0

                ''Below lines are commented on 18-05-2018
                'For Each RowCC As DataRow In dtCCFilter.Rows
                '    ColumnSet1.ColumnCount = dtCCFilter.Rows.Count
                '    ColumnSet1.Add(Me.grdSaved.RootTable.Columns(RowCC.Item(0).ToString), 0, i)
                '    ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                '    i += 1
                'Next

                ''TASK TFS3339
                Dim TotalColumm As String = RowCCH.Item(0).ToString & "Total"
                Dim NewColumn As New Janus.Windows.GridEX.GridEXColumn(TotalColumm, Janus.Windows.GridEX.ColumnType.Text)
                NewColumn.Caption = "Total"
                Me.grdSaved.RootTable.Columns.Add(NewColumn)

                Dim dtColumn As DataTable = CType(Me.grdSaved.DataSource, DataTable)

                Dim column As New DataColumn(TotalColumm)
                column.DataType = System.Type.GetType("System.Double")
                column.DefaultValue = 0
                If dtCCFilter.Rows.Count > 0 Then
                    dtColumn.Columns.Add(column)
                    dtColumn.AcceptChanges()
                End If
                Dim dtColumn1 As DataTable = CType(Me.grdSaved.DataSource, DataTable)

                Dim Colummns As Janus.Windows.GridEX.GridEXColumnCollection = Me.grdSaved.RootTable.Columns
                'For Each column2 As Janus.Windows.GridEX.GridEXColumn In Colummns
                '    Dim name As String = column2.Caption
                'Next
                For Each RowCC As DataRow In dtCCFilter.Rows
                    ColumnSet1.ColumnCount = dtCCFilter.Rows.Count + 1
                    ColumnSet1.Add(Me.grdSaved.RootTable.Columns(RowCC.Item(0).ToString), 0, i)
                    ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    i += 1
                Next
                If dtCCFilter.Rows.Count > 0 Then
                    ColumnSet1.ColumnCount = dtCCFilter.Rows.Count + 1
                    ColumnSet1.Add(Me.grdSaved.RootTable.Columns(TotalColumm), 0, i)
                    ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    i += 1
                End If
                For Each RowCC As DataRow In dtCCFilter.Rows
                    Dim dtEdit As DataTable = CType(Me.grdSaved.DataSource, DataTable)
                    For Each Row As DataRow In dtEdit.Rows
                        Row.BeginEdit()
                        Row.Item(TotalColumm) += Row.Item(RowCC.Item(0).ToString)
                        Row.EndEdit()
                    Next
                    ''
                    i += 1
                Next
                Me.grdSaved.UpdateData()
                ''END TASK TFS3339

                'Dim Column As New Janus.Windows.GridEX.GridEXColumn("Total")
                'If dtCCFilter.Rows.Count > 0 Then
                '    ColumnSet1.ColumnCount = dtCCFilter.Rows.Count + 1
                '    ColumnSet1.Add(grdSaved.RootTable.Columns("Total"), 0, i)
                '    ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

                'End If


            Next
            'GetColumnSetTotal()

            If chkExcludeZeroTransactionValue.Checked = True Then
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetRows
                    If row.Cells("TotalAmount").Value = 0 Then
                        row.Delete()
                        grdSaved.UpdateData()
                    End If
                Next
            End If
            Dim dtSaved As DataTable = Me.grdSaved.DataSource

            ApplyGridSettings()


            'Me.grdSaved.GetTotal()
            'Dim Total1 As Double = Me.grdSaved.G
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
                    Dim PLNote1 As String = _DataRow.Cells("PLNoteTitle").Value.ToString
                    If PLNote1.ToUpper = "01-REVENUE" Then
                        TotalRows = Row.GetChildRows.Length
                        Revenue = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                        For i As Integer = 16 To dtSaved.Columns.Count - 1
                            If IsDBNull(_row.Item(i)) = True Then
                                _row.Item(i) = 0
                            End If
                            _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                        Next
                    ElseIf PLNote1.ToUpper = "02-COST OF SALES" Then
                        TotalRows += Row.GetChildRows.Length
                        CostOfSales = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                        For i As Integer = 16 To dtSaved.Columns.Count - 1
                            If IsDBNull(_row.Item(i)) = True Then
                                _row.Item(i) = 0
                            End If
                            _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                        Next
                    End If
                End If
            Next

            _row.Item(0) = ""
            _row.Item(1) = 0
            _row.Item(2) = "03"
            _row.Item(3) = "03-Gross Profit"
            _row.Item(4) = ""
            _row.Item(5) = 0
            _row.Item(6) = ""
            _row.Item(7) = 0
            _row.Item(8) = "Gross Profit"
            GrossPercentage = Revenue - CostOfSales
            GrossPercentage = GrossPercentage / Revenue
            GrossPercentage = GrossPercentage * 100
            _row.Item(9) = 0
            _row.Item(10) = Revenue + CostOfSales

            _row.Item(11) = 0
            _row.Item(12) = 0
            _row.Item(13) = 0
            _row.Item(14) = 0
            _row.Item(15) = 0
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
                Dim PLNote1 As String = _DataRow.Cells("PLNoteTitle").Value.ToString
                If PLNote1.ToUpper = "04-OVERHEADS-DIRECT" Then
                    TotalRows = Row.GetChildRows.Length
                    OverheadsDirect = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 16 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "05-OVERHEADS-INDIRECT" Then
                    TotalRows += Row.GetChildRows.Length
                    OverheadsIndirect = Row.GetSubTotal(grdSaved.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 16 To dtSaved.Columns.Count - 1
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
            _row.Item(0) = ""
            _row.Item(1) = 0
            _row.Item(2) = "06"
            _row.Item(3) = "06-Total OH"
            _row.Item(4) = ""
            _row.Item(5) = 0
            _row.Item(6) = ""
            _row.Item(7) = 0
            _row.Item(8) = "Total OH"
            _row.Item(9) = 0
            _row.Item(10) = 0
            _row.Item(11) = 0
            _row.Item(12) = OverheadsDirect + OverheadsIndirect
            _row.Item(13) = 0
            _row.Item(14) = 0
            _row.Item(15) = 0
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
            Dim GrossProfit As Double = 0
            Dim GrossPercentage As Double = 0
            Dim TotalRows As Integer = 0
            Dim TotalOH As Double = 0
            Dim OverheadsIndirect As Double = 0
            Dim NotOperatingIncome As Double = 0

            Dim dtSaved As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            Dim _row As DataRow = dtSaved.NewRow

            For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetRows
                Dim _DataRow As Janus.Windows.GridEX.GridEXRow = Row.GetChildRows.GetValue(0)
                Dim PLNote1 As String = _DataRow.Cells("PLNoteTitle").Value.ToString
                If PLNote1.ToUpper = "03-GROSS PROFIT" Then
                    TotalRows += Row.GetChildRows.Length
                    GrossProfit = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 16 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "06-TOTAL OH" Then
                    TotalRows = Row.GetChildRows.Length
                    TotalOH = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 16 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                End If
            Next

            _row.Item(0) = ""
            _row.Item(1) = 0
            _row.Item(2) = "07"
            _row.Item(3) = "07-Operating Profit"
            _row.Item(4) = ""
            _row.Item(5) = 0
            _row.Item(6) = ""
            _row.Item(7) = 0
            _row.Item(8) = "Operating Profit"
            _row.Item(9) = 0
            _row.Item(10) = 0
            _row.Item(11) = 0
            _row.Item(12) = 0
            _row.Item(13) = GrossProfit + TotalOH
            _row.Item(14) = 0
            _row.Item(15) = 0
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
                Dim PLNote1 As String = _DataRow.Cells("PLNoteTitle").Value.ToString
                If PLNote1.ToUpper = "07-OPERATING PROFIT" Then
                    TotalRows = Row.GetChildRows.Length
                    OperatingProfit = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 16 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "08-NOT OPERATING INCOME" Then ''Not Operating income
                    TotalRows += Row.GetChildRows.Length
                    TotalProfit = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 16 To dtSaved.Columns.Count - 1
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

            _row.Item(0) = ""
            _row.Item(1) = 0
            _row.Item(2) = "09"
            _row.Item(3) = "09-Total Profit"
            _row.Item(4) = ""
            _row.Item(5) = 0
            _row.Item(6) = ""
            _row.Item(7) = 0
            _row.Item(8) = "Total Profit"
            _row.Item(9) = 0
            _row.Item(10) = 0
            _row.Item(11) = OperatingProfit + TotalProfit
            _row.Item(12) = 0
            _row.Item(13) = 0
            _row.Item(14) = 0
            _row.Item(15) = 0
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
                Dim PLNote1 As String = _DataRow.Cells("PLNoteTitle").Value.ToString
                If PLNote1.ToUpper = "09-TOTAL PROFIT" Then
                    TotalRows += Row.GetChildRows.Length
                    OperatingProfit = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 16 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "10-FINANCE COST" Then
                    TotalRows = Row.GetChildRows.Length
                    FinanceCost = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 16 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                Else
                End If
            Next

            _row.Item(0) = ""
            _row.Item(1) = 0
            _row.Item(2) = "11"
            _row.Item(3) = "11-Profit Before Tax"
            _row.Item(4) = ""
            _row.Item(5) = 0
            _row.Item(6) = ""
            _row.Item(7) = 0
            _row.Item(8) = "Profit Before Tax"
            _row.Item(9) = 0
            _row.Item(10) = 0
            _row.Item(11) = 0
            _row.Item(12) = 0
            _row.Item(13) = 0
            _row.Item(14) = OperatingProfit + FinanceCost
            _row.Item(15) = 0
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
                Dim PLNote1 As String = _DataRow.Cells("PLNoteTitle").Value.ToString
                If PLNote1.ToUpper = "11-PROFIT BEFORE TAX" Then
                    TotalRows = Row.GetChildRows.Length
                    PROFITBEFORETAX = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 16 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                ElseIf PLNote1.ToUpper = "12-TAXATION" Then
                    TotalRows += Row.GetChildRows.Length
                    Taxation = Row.GetSubTotal(grdSaved.RootTable.Columns("Total ~ Actual"), Janus.Windows.GridEX.AggregateFunction.Sum)
                    For i As Integer = 16 To dtSaved.Columns.Count - 1
                        If IsDBNull(_row.Item(i)) = True Then
                            _row.Item(i) = 0
                        End If
                        _row.Item(i) += Row.GetSubTotal(grdSaved.RootTable.Columns(i), Janus.Windows.GridEX.AggregateFunction.Sum)
                    Next
                End If
            Next

            _row.Item(0) = ""
            _row.Item(1) = 0
            _row.Item(2) = "13"
            _row.Item(3) = "13-Profit After Tax"
            _row.Item(4) = ""
            _row.Item(5) = 0
            _row.Item(6) = ""
            _row.Item(7) = 0
            _row.Item(8) = "Profit After Tax"
            _row.Item(9) = 0
            _row.Item(10) = 0
            _row.Item(11) = 0
            _row.Item(12) = 0
            _row.Item(13) = 0
            _row.Item(14) = 0
            _row.Item(15) = PROFITBEFORETAX + Taxation
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
            Label3.Text = ""
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
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Project Wise Profit and Loss Summary" & Chr(10) & "From Date : " & Me.dtpFromDate.Value.ToString("dd-MM-yyyy") & " To Date : " & Me.dtpToDate.Value.ToString("dd-MM-yyyy")
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
                msg_Error("No records found in given date range")
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
            If e.Column.Key = "TotalAmount" Then
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
                If cmbCompany.SelectedValue > 0 Then
                    CompanyId = cmbCompany.SelectedValue
                    rptTrialBalance.Company = CompanyId
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
                If cmbCompany.SelectedValue > 0 Then
                    CompanyId = cmbCompany.SelectedValue
                    rptTrialBalance.Company = CompanyId
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


    ''' <summary>
    ''' TASK TFS3339 ON 21-05-2018
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetColumnSetTotal()
        Try
            Me.grdSaved.UpdateData()
            Dim columnCount As Integer = 0
            Dim StartIndex As Integer = 16
            'For Each ColumnSet1 As Janus.Windows.GridEX.GridEXColumnSet In Me.grdSaved.RootTable.ColumnSets
            '    If Not ColumnSet1.Caption = "Account Summary" Then
            '        columnCount += ColumnSet1.ColumnCount
            '        'Counter = ColumnSet1.ColumnCount
            '        'ColumnSet1.
            '        For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetDataRows
            '            Dim TotalValue As Double = 0
            '            Row.BeginEdit()
            '            Row.
            '            For i As Integer = StartIndex + Counter To StartIndex + Counter
            '                'Counter += 1
            '                TotalValue += Val(Row.Cells(i).Value.ToString)
            '            Next
            '            'Counter += 1

            '            Dim TotalColumn As String = ColumnSet1.Caption & "Total"
            '            Row.Cells(TotalColumn).Value = TotalValue
            '            Row.EndEdit()
            '        Next
            '    End If
            'Next

            'columnCount += ColumnSet1.ColumnCount
            'Counter = ColumnSet1.ColumnCount
            'ColumnSet1.


            Dim dt As DataTable = Me.grdSaved.DataSource

            Dim TotalColumnSets As Integer = Me.grdSaved.RootTable.ColumnSets.Count




            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetDataRows
                Row.BeginEdit()
                Dim Counter As Integer = 0
                Dim CellPosition As Integer = 0

                CellPosition = 15
                For Each ColumnSet1 As Janus.Windows.GridEX.GridEXColumnSet In Me.grdSaved.RootTable.ColumnSets





                    If Not ColumnSet1.Caption = "Account Summary" Then

                        'Dim table As Janus.Windows.GridEX.GridEXTable = ColumnSet1.Table

                        'Dim Counts As Integer = table.Columns.Count
                        'For Each _COLUMN As Janus.Windows.GridEX.GridEXColumn In table.Columns


                        '    Dim name As String = _COLUMN.Caption


                        'Next




                        columnCount = ColumnSet1.ColumnCount - 1
                        Dim Position As Integer = ColumnSet1.Position
                        Dim Index As Integer = ColumnSet1.Index
                        Dim Column As String = ColumnSet1.Caption & "Total"
                        Dim TotalValue As Double = 0
                        Dim StartIndex1 As Integer = Counter + 16
                        Dim Counter1 As Integer = 0
                        For i As Integer = 1 To columnCount
                            Dim key As String = Row.Cells(CellPosition + i).Column.Key

                            TotalValue += Val(Row.Cells(CellPosition + i).Value.ToString)
                            Counter1 += 1
                        Next
                        Row.Cells(Column).Value = TotalValue
                        CellPosition += Counter1





                        'ColumnSet1.
                        'Dim TotalValue As Double = 0
                        'Row.BeginEdit()
                        'Row.
                        'For i As Integer = StartIndex + Counter To StartIndex + Counter
                        ''Counter += 1
                        'TotalValue += Val(Row.Cells(i).Value.ToString)


                    End If
                Next
                'Counter += 1
                'Dim TotalColumn As String = ColumnSet1.Caption & "Total"
                'Row.Cells(TotalColumn).Value = TotalValue
                Row.EndEdit()
            Next
            Me.grdSaved.UpdateData()
            'Dim dtSaved As DataTable = Me.grdSaved.DataSource
            'Dim Columset As Janus.Windows.GridEX.GridEXColumnSetCollection = Me.grdSaved.RootTable.ColumnSets
            'Dim Sets As Integer = Me.grdSaved.RootTable.ColumnSets.Count
            'Dim Column As Janus.Windows.GridEX.GridEXColumn = Me.grdSaved.RootTable.ColumnSets.Item(0).Item(0, 0)
            ''Dim Sets As Integer = Me.grdSaved.RootTable.ColumnSets.
            'Dim Total As Double = Me.grdSaved.GetTotal(Me.grdSaved.RootTable.Columns("abc"), Janus.Windows.GridEX.AggregateFunction.Sum)
            ''Dim FilterCondition As New Janus.Windows.GridEX.GridEX
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkShowWithOutCostCenter_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowWithOutCostCenter.CheckedChanged, chkExcludeZeroTransactionValue.CheckedChanged
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