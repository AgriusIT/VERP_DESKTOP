Imports SBModel
Imports SBDal
Imports SBUtility
Imports System.Windows.Forms
Imports System.Data.OleDb
Public Class frmGrdRptEmployeeSalarySheetDetail
    Implements IGeneral

    Dim GrossSalaryCalcByFormula As Boolean = False 'Declare Variable
    Dim GrossSalaryFormula As String = String.Empty 'Declare Variable
    Dim _SearchDt As New DataTable
    Public Enum enmSalary
        Dept_Id
        Desig_Id
        ShiftGroupId
        City_ID
        EmployeeId
        EmployeeCode
        EmployeeName
        Designation
        Department
        CostCentre
        PvBalance
        Advance
        TotalReceivable
        Deduction
        Payable
        SalaryRate
        BasicSalaryAfterFormula
        Count
    End Enum
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



    Private Sub frmGrdRptEmployeeSalarySheetDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation and DeSelect all the lists at Load Time
            FillCombo("Employees")
            Me.lstEmployee.DeSelect()
            FillCombo("Department")
            Me.lstDepartment.DeSelect()
            FillCombo("Designation")
            Me.lstDesignation.DeSelect()
            FillCombo("Shift")
            Me.lstShiftGroup.DeSelect()
            FillCombo("City")
            Me.lstCity.DeSelect()
            FillCombo("HeadCostCentre")
            Me.lstHeadCostCenter.DeSelect()
            FillCombo("CostCentre")
            Me.lstCostCenter.DeSelect()
            _SearchDt = CType(Me.lstEmployee.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            Me.lstEmployee.DeSelect()
            If Not getConfigValueByType("GrossSalaryCalcByFormula").ToString = "Error" Then
                GrossSalaryCalcByFormula = getConfigValueByType("GrossSalaryCalcByFormula")
            Else
                GrossSalaryCalcByFormula = False
            End If

            If Not getConfigValueByType("GrossSalaryFormula").ToString = "Error" Then
                GrossSalaryFormula = getConfigValueByType("GrossSalaryFormula")
            Else
                GrossSalaryFormula = ""
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            Dim strQuery As String = String.Empty
            If Condition = "Employees" Then
                strQuery = "SELECT Employee_Id, Employee_Code + ' ~ ' + Employee_Name Employee_Name FROM tblDefEmployee WHERE Active = 1" ''TASKTFS75 added and set active =1
                FillListBox(Me.lstEmployee.ListItem, strQuery)
                'ElseIf Condition = "Employee1" Then
                '    strQuery = "Select Employee_Id, Employee_Name From tblDefEmployee Where Active='1' And CostCentre = " & Me.cmbEmployee.selectedValue & " Order By 2 Asc" ''TASKTFS75 added and set active =1
                '    FillListBox(Me.lstEmployee.ListItem, strQuery)
            ElseIf Condition = "Department" Then
                strQuery = "Select EmployeeDeptId, EmployeeDeptName From EmployeeDeptDefTable  WHERE Active = 1 Order By 2 Asc"
                FillListBox(Me.lstDepartment.ListItem, strQuery)
            ElseIf Condition = "Designation" Then
                strQuery = "Select EmployeeDesignationId, EmployeeDesignationName From EmployeeDesignationDefTable  WHERE Active = 1 Order By 2 Asc"
                FillListBox(Me.lstDesignation.ListItem, strQuery)
            ElseIf Condition = "Shift" Then
                strQuery = "Select ShiftGroupId, ShiftGroupName From ShiftGroupTable  WHERE Active = 1 Order By 2 Asc"
                FillListBox(Me.lstShiftGroup.ListItem, strQuery)
            ElseIf Condition = "City" Then
                strQuery = "Select CityId, CityName From tblListCity  WHERE Active = 1 Order By 2 Asc"
                FillListBox(Me.lstCity.ListItem, strQuery)
            ElseIf Condition = "CostCentre" Then
                '' FillListBox(Me.lstCostCenter.ListItem, "Select *  From tblDefCostCenter  WHERE Active = 1 Order by SortOrder, Name") ''TFS3320
                ''TFS3320 : Ayesha Rehman : Cost Center Rights implementation
                FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT * FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Order by SortOrder, Name Else SELECT  * FROM tblDefCostCenter WHERE Active=1 Order by SortOrder, Name ")
            ElseIf Condition = "HeadCostCentre" Then
                FillListBox(Me.lstHeadCostCenter.ListItem, "Select DISTINCT CostCenterGroup, CostCenterGroup From tblDefCostCenter  WHERE Active = 1 ")
                'FillListBox(Me.lstHeadCostCenter.ListItem, "SELECT DISTINCT CostCenterGroup, CostCenterGroup FROM tblDefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Me.tblProgressbar.Visible = True
        Try
            FillGrid()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.tblProgressbar.Visible = False
        End Try
    End Sub
    Public Sub FillGrid()

        Try

            Dim strFilter As String = String.Empty
            ''Query Edit Against TFS3418 : Ayesha Rehman : 28-05-2018
            Dim sp As String = "Select Dept_Id,Desig_Id,tblDefEmployee.ShiftGroupId,City_ID,tblDefEmployee.Employee_Id, " _
                              & " Employee_Code, Employee_Name, EmployeeDesignationName as Designation, EmployeeDeptName as Department, tblDefCostCenter.Name As [Cost Centre],  " _
                              & " ISNULL(EmpOpening.Opening,0) as Previouse_Receivable, ISNULL(EmpRecv.New_Receivable,0) as [New Receivable], " _
                              & " 0 as [Total Receivable],ISNULL(EmpDec.Deduction,0) as [Deduction],  0 as [Net Receivable]," _
                              & " ISNULL(tblDefEmployee.Salary,0) as SalaryRate, " & IIf(GrossSalaryCalcByFormula = True, " (ISNULL(tblDefEmployee.Salary,0) " & GrossSalaryFormula & ")", "0") & " as [Basic Salary] " _
                              & " From tblDefEmployee LEFT JOIN tblDefCostCenter ON tblDefEmployee.CostCentre = tblDefCostCenter.CostCenterID LEFT OUTER JOIN  EmployeeDeptDefTable  on EmployeeDeptDefTable.EmployeeDeptId = tblDefEmployee.Dept_ID " _
                              & " LEFT OUTER JOIN EmployeeDesignationDefTable On EmployeeDesignationDefTable.EmployeeDesignationId = tblDefEmployee.Desig_ID " _
                              & "  LEFT OUTER JOIN ShiftGroupTable ON tblDefEmployee.ShiftGroupId = ShiftGroupTable.ShiftGroupId " _
                              & " LEFT OUTER JOIN  tblListCity ON tblDefEmployee.City_ID = tblListCity.CityId " _
                              & " LEFT OUTER JOIN (SELECT Employee_Id, SUM(ISNULL(Opening, 0)) AS Opening " _
                              & " FROM  (SELECT dbo.tblEmployeeAccounts.Employee_Id, ISNULL(Recv.Opening, 0) AS Opening FROM dbo.tblEmployeeAccounts " _
                              & " LEFT OUTER JOIN (SELECT dbo.tblVoucherDetail.coa_detail_id, SUM(dbo.tblVoucherDetail.debit_amount - dbo.tblVoucherDetail.credit_amount) AS Opening " _
                              & " FROM dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id " _
                              & " WHERE (CONVERT(Varchar, dbo.tblVoucher.voucher_date, 102) < CONVERT(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) GROUP BY dbo.tblVoucherDetail.coa_detail_id) AS Recv " _
                              & " ON dbo.tblEmployeeAccounts.Account_Id = Recv.coa_detail_id WHERE IsNull(tblEmployeeAccounts.flgReceivable,0) =1) AS EmpRecOpening GROUP BY Employee_Id) EmpOpening on EmpOpening.Employee_Id = tblDefEmployee.Employee_Id " _
                              & " LEFT OUTER JOIN (SELECT Employee_Id, SUM(ISNULL(New_Receivable, 0)) AS New_Receivable FROM  (SELECT     dbo.tblEmployeeAccounts.Employee_Id, ISNULL(Recv.New_Receivable, 0) AS New_Receivable FROM dbo.tblEmployeeAccounts " _
                              & " LEFT OUTER JOIN (SELECT dbo.tblVoucherDetail.coa_detail_id, SUM(Isnull(dbo.tblVoucherDetail.debit_amount,0)) as New_Receivable FROM   dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id " _
                              & " WHERE (Convert(Varchar, Voucher_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.tblVoucherDetail.coa_detail_id) AS Recv " _
                              & " ON dbo.tblEmployeeAccounts.Account_Id = Recv.coa_detail_id WHERE IsNull(tblEmployeeAccounts.flgReceivable,0) =1) AS EmpRecOpening GROUP BY Employee_Id) EmpRecv on EmpRecv.Employee_Id = tblDefEmployee.Employee_Id " _
                              & " LEFT OUTER JOIN (SELECT Employee_Id, SUM(ISNULL(Deduction, 0)) AS Deduction FROM  (SELECT     dbo.tblEmployeeAccounts.Employee_Id, ISNULL(Recv.Deduction, 0) AS Deduction FROM dbo.tblEmployeeAccounts " _
                              & " LEFT OUTER JOIN (SELECT dbo.tblVoucherDetail.coa_detail_id, SUM(Isnull(dbo.tblVoucherDetail.credit_amount,0)) as Deduction FROM   dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id " _
                              & " WHERE (Convert(Varchar, Voucher_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.tblVoucherDetail.coa_detail_id) AS Recv " _
                              & " ON dbo.tblEmployeeAccounts.Account_Id = Recv.coa_detail_id WHERE IsNull(tblEmployeeAccounts.flgReceivable,0) =1) AS EmpRecOpening GROUP BY Employee_Id) EmpDec on EmpDec.Employee_Id = tblDefEmployee.Employee_Id  WHERE Employee_Name <> '' "

            strFilter = " Employee_Name <> ''"
            If Me.lstEmployee.SelectedIDs.Length > 0 Then
                ''Commented Against TFS3418 : Ayesha Rehman
                'sp += " AND tblDefEmployee.Employee_Id =" & Me.lstEmployee.SelectedIDs & "" 
                sp += " AND tblDefEmployee.Employee_Id in (" & Me.lstEmployee.SelectedIDs & ")" ''TFS3418 
            End If
            If Me.lstDepartment.SelectedIDs.Length > 0 Then
                sp += " AND Dept_Id in(" & Me.lstDepartment.SelectedIDs & ")"
            End If
            If Me.lstDesignation.SelectedIDs.Length > 0 Then
                sp += " AND Desig_Id in(" & Me.lstDesignation.SelectedIDs & ")"
            End If
            If Me.lstShiftGroup.SelectedIDs.Length > 0 Then
                sp += " AND tblDefEmployee.ShiftGroupId in(" & Me.lstShiftGroup.SelectedIDs & ")"
            End If
            If Me.lstCity.SelectedIDs.Length > 0 Then
                sp += " AND City_ID in(" & Me.lstCity.SelectedIDs & ")"
            End If
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                sp += " AND tblDefEmployee.CostCentre in (" & Me.lstCostCenter.SelectedIDs & ") "
            End If
            ''Commented Against TFS3418 : Ayesha Rehman
            ''add by zainab
            'If Me.lstHeadCostCenter.SelectedIDs.Length > 0 Then
            '    sp += " AND tblDefEmployee.CostCentre in (" & Me.lstHeadCostCenter.SelectedIDs & ") "
            'End If
            sp += " ORDER BY 2 Asc"

            '"SP_EmployeeSalarySheet '" & dtpFrom.Value & "','" & dtpTo.Value & "'"
            Dim dt As DataTable = GetDataTable(sp)
            'dt.Columns("Paid_Salary").Expression = "MonthlySalary-Allowance-Insurance-Gratuity_Fund-Advance-WHTax-ESSI-EOBI"
            dt.TableName = "SalarySheet"

            Dim strSalaryType As String = "Select SalaryExpTypeId, SalaryDeduction, Convert(Varchar, SalaryExpType)  From SalaryExpenseType WHERE Active=1  "
            Dim dtSalaryType As New DataTable
            dtSalaryType = GetDataTable(strSalaryType)

            For Each row As DataRow In dtSalaryType.Rows
                If Not dtSalaryType.Columns.Contains(row(1)) Then
                    dt.Columns.Add(row(0), GetType(System.Int16), row(0))
                    dt.Columns.Add(row(1) & "-" & row(0), GetType(System.String), row(1))
                    dt.Columns.Add(row(2), GetType(System.Double))
                End If
            Next

            For Each row As DataRow In dt.Rows
                For c As Integer = enmSalary.Count To dt.Columns.Count - 3 Step 3
                    row.BeginEdit()
                    row(c + 2) = 0
                    row.EndEdit()
                Next
            Next

            Dim str As String = "SELECT dbo.SalariesExpenseMasterTable.EmployeeId, dbo.SalariesExpenseDetailTable.SalaryExpTypeId, abs(Sum(isnull(dbo.SalariesExpenseDetailTable.Earning,0))-SUM(isnull(dbo.SalariesExpenseDetailTable.Deduction,0))) as Amount " _
                               & " FROM dbo.SalariesExpenseMasterTable INNER JOIN " _
                               & " dbo.SalariesExpenseDetailTable ON dbo.SalariesExpenseMasterTable.SalaryExpId = dbo.SalariesExpenseDetailTable.SalaryExpId WHERE (Convert(Varchar, SalariesExpenseMasterTable.SalaryExpDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102))  Group By dbo.SalariesExpenseMasterTable.EmployeeId, dbo.SalariesExpenseDetailTable.SalaryExpTypeId  Having abs(Sum(isnull(dbo.SalariesExpenseDetailTable.Earning,0))-SUM(isnull(dbo.SalariesExpenseDetailTable.Deduction,0))) <> 0 "

            Dim dt_Data As New DataTable
            dt_Data = GetDataTable(str)
            Dim dr() As DataRow
            For Each r As DataRow In dt.Rows
                dr = dt_Data.Select("EmployeeId='" & r("Employee_Id") & "'")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            r.BeginEdit()
                            r(dt.Columns.IndexOf(drFound(1)) + 2) = drFound(2)
                            r.EndEdit()
                        Next
                    End If
                End If
            Next
            dt.AcceptChanges()
            dt.Columns.Add("Total Salary", GetType(System.Double))
            Dim strTotal As String = String.Empty


            For c As Integer = enmSalary.Count To dt.Columns.Count - 3 Step 3
                If strTotal.Length > 0 Then
                    Dim flg As Boolean = Convert.ToBoolean(Microsoft.VisualBasic.Left(dt.Columns(c + 1).ColumnName, dt.Columns(c + 1).ColumnName.LastIndexOf("-") - 1 + 1))
                    strTotal = strTotal & IIf(flg = False, "+", "-") & "[" & dt.Columns(c + 2).ColumnName & "]"

                Else
                    strTotal = "[" & dt.Columns(c + 2).ColumnName & "]"
                End If
            Next

            dt.Columns("Total Salary").Expression = strTotal.ToString
            dt.Columns.Add("Cash Paid", GetType(System.Double))
            dt.Columns.Add("Bank Paid", GetType(System.Double))
            dt.Columns.Add("Balance", GetType(System.Double))

            For Each row As DataRow In dt.Rows
                row.BeginEdit()
                row("Cash Paid") = 0
                row("Bank Paid") = 0
                row("Balance") = 0
                row.EndEdit()
            Next

            Dim strQuery As String = "Select tblDefEmployee.Employee_Id, tblvoucherdetail.coa_detail_id, SUM(debit_amount) as CashPaid From tblVoucher INNER JOIN tblVoucherDetail On tblVoucher.Voucher_Id =  tblVoucherDetail.Voucher_Id INNER JOIN tblDefEmployee on tblDefEmployee.EmpSalaryAccountId = tblVoucherDetail.coa_detail_id WHERE tblVoucher.Voucher_Type_Id=2 AND (Convert(varchar, Voucher_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) Group By tblDefEmployee.Employee_Id, tblvoucherdetail.coa_detail_id"
            Dim dtCash As New DataTable
            dtCash = GetDataTable(strQuery)

            Dim strQuery1 As String = "Select tblDefEmployee.Employee_Id, tblvoucherdetail.coa_detail_id, SUM(debit_amount) as BankPaid From tblVoucher INNER JOIN tblVoucherDetail On tblVoucher.Voucher_Id =  tblVoucherDetail.Voucher_Id INNER JOIN tblDefEmployee on tblDefEmployee.EmpSalaryAccountId = tblVoucherDetail.coa_detail_id WHERE tblVoucher.Voucher_Type_Id=4 AND (Convert(varchar, Voucher_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) Group By tblDefEmployee.Employee_Id, tblvoucherdetail.coa_detail_id"
            Dim dtBank As New DataTable
            dtBank = GetDataTable(strQuery1)
            Dim drCash() As DataRow
            For Each r As DataRow In dt.Rows
                drCash = dtCash.Select("Employee_Id=" & r("Employee_Id") & "")
                If drCash IsNot Nothing Then
                    If drCash.Length > 0 Then
                        For Each drFound As DataRow In drCash
                            r.BeginEdit()
                            r("Cash Paid") = drFound(2)
                            r.EndEdit()
                        Next
                    End If
                End If
            Next

            Dim drBank() As DataRow
            For Each r As DataRow In dt.Rows
                drBank = dtBank.Select("Employee_Id=" & r("Employee_Id") & "")
                If drBank IsNot Nothing Then
                    If drBank.Length > 0 Then
                        For Each drFound As DataRow In drBank
                            r.BeginEdit()
                            r("Bank Paid") = drFound(2)
                            r.EndEdit()
                        Next
                    End If
                End If
            Next

            'Dim strQuerys As String = "Select Employee_Id, ((ISNULL(OpeningRevc.Receiveable,0) + ISNULL(Advance.Advance,0)) - ISNULL(Deduction.Deduction,0)) as Balance From tblDefEmployee LEFT OUTER JOIN (Select tblVoucherDetail.coa_detail_id, SUM(debit_amount-credit_amount) as Receiveable From tblVoucher INNER JOIN tblVoucherDetail on tblVoucher.Voucher_Id = tblVoucherDetail.voucher_id WHERE (Convert(Varchar, Voucher_Date, 102) < Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) Group By tblVoucherDetail.coa_detail_id) OpeningRevc On OpeningRevc.coa_detail_Id = tblDefEmployee.ReceiveableAccountId LEFT OUTER JOIN (Select tblVoucherDetail.coa_detail_id, SUM(debit_amount) as Advance From tblVoucher INNER JOIN tblVoucherDetail on tblVoucher.Voucher_Id = tblVoucherDetail.voucher_id WHERE (Convert(Varchar, Voucher_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) Group By tblVoucherDetail.coa_detail_id) Advance On Advance.coa_detail_Id = tblDefEmployee.ReceiveableAccountId LEFT OUTER JOIN (Select tblVoucherDetail.coa_detail_id, SUM(credit_amount) as Deduction From tblVoucher INNER JOIN tblVoucherDetail on tblVoucher.Voucher_Id = tblVoucherDetail.voucher_id WHERE (Convert(Varchar, Voucher_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) Group By tblVoucherDetail.coa_detail_id) Deduction On Deduction.coa_detail_Id = tblDefEmployee.ReceiveableAccountId ORDER BY 2 Asc"
            'Dim strQuerys As String = "Select tblDefEmployee.Employee_Id, (ISNULL(EmpOpening.Opening,0)+ ISNULL(EmpRecv.Receivable,0) - Isnull(EmpDeduction.Deduction,0)) as [Balance] From tblDefEmployee INNER JOIN  EmployeeDeptDefTable  on EmployeeDeptDefTable.EmployeeDeptId = tblDefEmployee.Dept_ID INNER JOIN EmployeeDesignationDefTable On EmployeeDesignationDefTable.EmployeeDesignationId = tblDefEmployee.Desig_ID LEFT OUTER JOIN (SELECT     Employee_Id, SUM(ISNULL(Opening, 0)) AS Opening FROM         (SELECT     dbo.tblEmployeeAccounts.Employee_Id, ISNULL(Recv.Opening, 0) AS Opening FROM          dbo.tblEmployeeAccounts LEFT OUTER JOIN (SELECT     dbo.tblVoucherDetail.coa_detail_id, SUM(dbo.tblVoucherDetail.debit_amount - dbo.tblVoucherDetail.credit_amount) AS Opening FROM          dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id WHERE      (CONVERT(Varchar, dbo.tblVoucher.voucher_date, 102) < CONVERT(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) GROUP BY dbo.tblVoucherDetail.coa_detail_id) AS Recv ON dbo.tblEmployeeAccounts.Account_Id = Recv.coa_detail_id) AS EmpRecOpening GROUP BY Employee_Id) EmpOpening on EmpOpening.Employee_Id = tblDefEmployee.Employee_Id LEFT OUTER JOIN (SELECT  Employee_Id, SUM(ISNULL(Receivable, 0)) AS Receivable FROM  (SELECT     dbo.tblEmployeeAccounts.Employee_Id, ISNULL(Recv.Receivable, 0) AS Receivable FROM dbo.tblEmployeeAccounts LEFT OUTER JOIN (SELECT     dbo.tblVoucherDetail.coa_detail_id, SUM(dbo.tblVoucherDetail.debit_amount) AS Receivable FROM          dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id WHERE (Convert(Varchar, Voucher_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.tblVoucherDetail.coa_detail_id) AS Recv ON dbo.tblEmployeeAccounts.Account_Id = Recv.coa_detail_id) AS EmpRecOpening GROUP BY Employee_Id) EmpRecv on EmpRecv.Employee_Id = tblDefEmployee.Employee_Id   LEFT OUTER JOIN (SELECT  Employee_Id, SUM(ISNULL(Deduction, 0)) AS Deduction FROM  (SELECT     dbo.tblDefEmployee.Employee_Id, ISNULL(Recv.Deduction, 0) AS Deduction FROM dbo.tblDefEmployee LEFT OUTER JOIN (SELECT     dbo.tblVoucherDetail.coa_detail_id, SUM(dbo.tblVoucherDetail.credit_amount) AS Deduction FROM  dbo.tblVoucherDetail INNER JOIN dbo.tblVoucher ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id WHERE (Convert(Varchar, Voucher_Date, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY dbo.tblVoucherDetail.coa_detail_id) AS Recv ON dbo.tblDefEmployee.EmpSalaryAccountId = Recv.coa_detail_id) AS EmpRecOpening GROUP BY Employee_Id) EmpDeduction on EmpDeduction.Employee_Id = tblDefEmployee.Employee_Id "
            'Dim dtBalance As New DataTable
            'dtBalance = GetDataTable(strQuerys)

            'Dim drDeduction() As DataRow
            'For Each r As DataRow In dt.Rows
            '    drDeduction = dtBalance.Select("Employee_Id=" & r("Employee_Id") & "")
            '    If drDeduction IsNot Nothing Then
            '        If drDeduction.Length > 0 Then
            '            For Each drFound As DataRow In drDeduction
            '                r.BeginEdit()
            '                r("Balance") = drFound(1)
            '                r.EndEdit()
            '            Next
            '        End If
            '    End If
            'Next
            dt.Columns("Total Receivable").Expression = "Previouse_Receivable+[New Receivable]"
            dt.Columns("Net Receivable").Expression = "(Previouse_Receivable+[New Receivable])-Deduction"
            dt.Columns("Balance").Expression = "([Total Salary]-([Cash Paid]+[Bank Paid]))"
            'Dim dv As New DataView
            'dv.Table = dt

            'strFilter = " Employee_Name <> ''"
            'If Me.cmbEmployee.SelectedIndex > 0 Then
            '    strFilter += " AND Employee_Id =" & Me.cmbEmployee.SelectedValue & ""
            'End If
            'If Me.lstDepartment.SelectedIDs.Length > 0 Then
            '    strFilter += " AND Dept_Id in(" & Me.lstDepartment.SelectedIDs & ")"
            'End If
            'If Me.lstDesignation.SelectedIDs.Length > 0 Then
            '    strFilter += " AND Desig_Id in(" & Me.lstDesignation.SelectedIDs & ")"
            'End If
            'If Me.lstShiftGroup.SelectedIDs.Length > 0 Then
            '    strFilter += " AND ShiftGroupId in(" & Me.lstShiftGroup.SelectedIDs & ")"
            'End If
            'If Me.lstCity.SelectedIDs.Length > 0 Then
            '    strFilter += " AND City_ID in(" & Me.lstCity.SelectedIDs & ")"
            'End If
            'dv.RowFilter = strFilter
            dt.AcceptChanges()
            Me.grdEmployeeSalarySheet.DataSource = dt
            Me.grdEmployeeSalarySheet.RetrieveStructure()
            ApplyGridSetting()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            Me.grdEmployeeSalarySheet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdEmployeeSalarySheet.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdEmployeeSalarySheet.RootTable.Columns(0).Visible = False
            Me.grdEmployeeSalarySheet.RootTable.Columns(1).Visible = False
            Me.grdEmployeeSalarySheet.RootTable.Columns(2).Visible = False
            Me.grdEmployeeSalarySheet.RootTable.Columns(3).Visible = False
            Me.grdEmployeeSalarySheet.RootTable.Columns(4).Visible = False
            For c As Integer = enmSalary.Count To Me.grdEmployeeSalarySheet.RootTable.Columns.Count - 3 Step 3
                If Me.grdEmployeeSalarySheet.RootTable.Columns(c).Key <> "Total Salary" AndAlso Me.grdEmployeeSalarySheet.RootTable.Columns(c).Key <> "Cash Paid" AndAlso Me.grdEmployeeSalarySheet.RootTable.Columns(c).Key <> "Bank Paid" AndAlso Me.grdEmployeeSalarySheet.RootTable.Columns(c).Key <> "Balance" Then
                    Me.grdEmployeeSalarySheet.RootTable.Columns(c).Visible = False
                    Me.grdEmployeeSalarySheet.RootTable.Columns(c + 1).Visible = False
                    Me.grdEmployeeSalarySheet.RootTable.Columns(c + 2).AllowSort = False
                    Me.grdEmployeeSalarySheet.RootTable.Columns(c + 2).CellStyle.BackColor = Color.WhiteSmoke

                    Me.grdEmployeeSalarySheet.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdEmployeeSalarySheet.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdEmployeeSalarySheet.RootTable.Columns(c + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                End If
            Next
            Me.grdEmployeeSalarySheet.RootTable.Columns("Total Salary").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdEmployeeSalarySheet.RootTable.Columns("Total Salary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployeeSalarySheet.RootTable.Columns("Total Salary").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdEmployeeSalarySheet.RootTable.Columns("New Receivable").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdEmployeeSalarySheet.RootTable.Columns("New Receivable").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployeeSalarySheet.RootTable.Columns("New Receivable").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdEmployeeSalarySheet.RootTable.Columns("Total Receivable").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdEmployeeSalarySheet.RootTable.Columns("Total Receivable").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployeeSalarySheet.RootTable.Columns("Total Receivable").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdEmployeeSalarySheet.RootTable.Columns("Deduction").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdEmployeeSalarySheet.RootTable.Columns("Deduction").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployeeSalarySheet.RootTable.Columns("Deduction").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdEmployeeSalarySheet.RootTable.Columns("Net Receivable").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdEmployeeSalarySheet.RootTable.Columns("Net Receivable").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployeeSalarySheet.RootTable.Columns("Net Receivable").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdEmployeeSalarySheet.RootTable.Columns(enmSalary.PvBalance).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdEmployeeSalarySheet.RootTable.Columns(enmSalary.PvBalance).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployeeSalarySheet.RootTable.Columns(enmSalary.PvBalance).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grdEmployeeSalarySheet.RootTable.Columns("New Receivable").FormatString = "N"
            Me.grdEmployeeSalarySheet.RootTable.Columns("Net Receivable").FormatString = "N"
            Me.grdEmployeeSalarySheet.RootTable.Columns(enmSalary.PvBalance).FormatString = "N"
            Me.grdEmployeeSalarySheet.RootTable.Columns("Deduction").FormatString = "N"
            Me.grdEmployeeSalarySheet.RootTable.Columns("Total Receivable").FormatString = "N"
            Me.grdEmployeeSalarySheet.RootTable.Columns("Basic Salary").FormatString = "N"


            Me.grdEmployeeSalarySheet.RootTable.Columns("New Receivable").CellStyle.BackColor = Color.Ivory
            Me.grdEmployeeSalarySheet.RootTable.Columns("Net Receivable").CellStyle.BackColor = Color.Ivory
            Me.grdEmployeeSalarySheet.RootTable.Columns("Total Receivable").CellStyle.BackColor = Color.Snow
            Me.grdEmployeeSalarySheet.RootTable.Columns(enmSalary.PvBalance).CellStyle.BackColor = Color.Ivory
            Me.grdEmployeeSalarySheet.RootTable.Columns("Deduction").CellStyle.BackColor = Color.Ivory
            Me.grdEmployeeSalarySheet.RootTable.Columns("Total Salary").CellStyle.BackColor = Color.Honeydew
            Me.grdEmployeeSalarySheet.RootTable.Columns("Cash Paid").CellStyle.BackColor = Color.AliceBlue
            Me.grdEmployeeSalarySheet.RootTable.Columns("Bank Paid").CellStyle.BackColor = Color.AliceBlue


            Me.grdEmployeeSalarySheet.RootTable.Columns("Cash Paid").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdEmployeeSalarySheet.RootTable.Columns("Cash Paid").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployeeSalarySheet.RootTable.Columns("Cash Paid").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grdEmployeeSalarySheet.RootTable.Columns("Bank Paid").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdEmployeeSalarySheet.RootTable.Columns("Bank Paid").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployeeSalarySheet.RootTable.Columns("Bank Paid").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grdEmployeeSalarySheet.RootTable.Columns("Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdEmployeeSalarySheet.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployeeSalarySheet.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grdEmployeeSalarySheet.RootTable.Columns("Basic Salary").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdEmployeeSalarySheet.RootTable.Columns("Basic Salary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployeeSalarySheet.RootTable.Columns("Basic Salary").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdEmployeeSalarySheet.RootTable.Columns("Basic Salary").Visible = False
            Me.grdEmployeeSalarySheet.RootTable.Columns("SalaryRate").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdEmployeeSalarySheet.RootTable.Columns("SalaryRate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployeeSalarySheet.RootTable.Columns("SalaryRate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployeeSalarySheet.RootTable.Columns("SalaryRate").FormatString = "N"

            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdEmployeeSalarySheet.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try

            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdEmployeeSalarySheet.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdEmployeeSalarySheet.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.grdEmployeeSalarySheet.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Employee Wise Monthly Salary Sheet " & vbCrLf & " Date From: " & Me.dtpFrom.Value & "  Date To: " & Me.dtpTo.Value & ""


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            AddRptParam("@FromDate", Me.dtpFrom.Value)
            AddRptParam("@ToDate", Me.dtpTo.Value)
            AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
            AddRptParam("@DesignationsIds", Me.lstDesignation.SelectedIDs)
            AddRptParam("@DepartmentIds", Me.lstDepartment.SelectedIDs)
            AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
            AddRptParam("@CityIds", Me.lstCity.SelectedIDs)
            AddRptParam("@ShiftGroupIds", Me.lstShiftGroup.SelectedIDs)
            AddRptParam("@EmployeeId", Me.lstEmployee.SelectedIDs)
            ShowReport("rptEmpSalarySheet")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub lstCostCenter_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Try
        '    If Me.lstCostCenter.SelectedValue > 0 Then
        '        FillCombo("Employee1")
        '    Else
        '        FillCombo("Employee")
        '    End If

        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        Try
            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt
            dv.RowFilter = "Employee_Name Like '%" & Me.txtSearch.Text & "%'"
            Me.lstEmployee.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnShow.Enabled = True
                Me.btnPrint.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnShow.Enabled = False
            Me.btnPrint.Enabled = False
            Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Report Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Report Print" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

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

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            FillCombo("Employees")
            Me.lstEmployee.DeSelect()
            FillCombo("Department")
            Me.lstDepartment.DeSelect()
            FillCombo("Designation")
            Me.lstDesignation.DeSelect()
            FillCombo("Shift")
            Me.lstShiftGroup.DeSelect()
            FillCombo("City")
            Me.lstCity.DeSelect()
            FillCombo("HeadCostCentre")
            Me.lstHeadCostCenter.DeSelect()
            FillCombo("CostCentre")
            Me.lstCostCenter.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtnNightShift_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnNightShift.CheckedChanged

    End Sub

    Private Sub rbtnNormalShift_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnNormalShift.CheckedChanged

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()

    End Sub
End Class