'Task# 20150508 Ali Ansari adding to date criteria in serach and load 'Approved' Status Only
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports SBUtility.Utility

Public Class frmEmployeeDeductions
    'TFS3565: Waqar Raza: Added this Variables to apply right based Cost Center
    Dim flgCostCenterRights As Boolean = False
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode)
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnSave.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                '    Exit Sub
                'End If
            Else

                If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                    Dim dt As DataTable = GetFormRights(EnumForms.frmStockDispatch)
                    If Not dt Is Nothing Then
                        If Not dt.Rows.Count = 0 Then
                            If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                                Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                            Else
                                Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                            End If
                            Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                        End If
                    End If
                    'GetSecurityByPostingUser(UserPostingRights, BtnSave, BtnDelete)
                Else
                    'Me.Visible = False
                    Me.btnSave.Enabled = False
                    CtrlGrdBar1.mGridPrint.Enabled = False
                    CtrlGrdBar1.mGridExport.Enabled = False
                    CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    For Each RightsDt As GroupRights In Rights
                        If RightsDt.FormControlName = "View" Then
                            Me.Visible = True
                        ElseIf RightsDt.FormControlName = "Save" Then
                            If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                        ElseIf RightsDt.FormControlName = "Print" Then
                            CtrlGrdBar1.mGridPrint.Enabled = True
                        ElseIf RightsDt.FormControlName = "Export" Then
                            CtrlGrdBar1.mGridExport.Enabled = True
                        ElseIf RightsDt.FormControlName = "Field Chooser" Then
                            CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        End If
                    Next
                End If
            End If
            If (Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save") AndAlso Me.UltraTabControl2.SelectedTab.Index = 0 Then
                Me.btnSave.Visible = True
            Else
                If Me.UltraTabControl2.SelectedTab.Index = 1 Then
                    Me.btnSave.Visible = False
                Else
                    Me.btnSave.Visible = True
                End If
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            FillGridDeductionDetail()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillGridDeductionDetail()
        Try
            If Con.State = ConnectionState.Closed Then Con.Open()
            'Dim objTrans As OleDbTransaction
            Dim cmd As New OleDbCommand
            Dim MonthYear As New DateTime

            'If Len(Me.txtYear.Text) < 4 Then
            '    ShowErrorMessage("Invalid Year")
            '    Me.txtYear.Focus()
            '    Exit Sub
            'End If

            Dim strSQL As String = String.Empty

            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 120
            cmd.Connection = Con
            'cmd.Transaction = objTrans
            Dim dt As New DataTable
            'Marked Against Task#20150508 Ali Ansari
            'dt = GetDataTable("Select IsNull(empDeductions.RequestID,0) as RequestId,Employee_ID, Employee_Name as [Employee Name], Employee_Code as [Code], EmployeeDesignationName as Designation,EmployeeDeptName as Department,RequestNo as [Request No],RequestDate as [Request Date], IsNull(AdvanceAmount,0) as [Request Amount],Isnull(DeductionDt.Deduction,0) as Issued, Case When IsNull(PermonthDeduction,0) = 0 Then (Isnull(AdvanceAmount,0)-Isnull(DeductionDt.Deduction,0)) ELSE IsNull(PermonthDeduction,0) End  as Deduction From EmployeesView LEFT OUTER JOIN( " _
            '             & " Select EmployeeID, RequestID,RequestNo,RequestDate, AdvanceAmount,PermonthDeduction From AdvanceRequestTable WHERE Convert(DateTime,'" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102) Between PeriodStartDate And PeriodEndDate AND RequestStatus='Open' " _
            '             & " ) empDeductions on empDeductions.EmployeeID = EmployeesView.Employee_Id " _
            '             & " LEFT OUTER JOIN (Select EmployeeId, RequestId, SUM(IsNull(DeductionAmount,0)) as Deduction From DeductionsDetailTable Group By EmployeeId, RequestId) " _
            '             & " DeductionDt on DeductionDt.EmployeeID = EmployeesView.Employee_Id and DeductionDt.RequestId = empDeductions.RequestID " _
            '             & " WHERE RequestNo <> ''")
            'Marked Against Task#20150508 Ali Ansari

            'Altered Against Task#20150508 Ali Ansari
            '' Add datefrom and dateto criteria in ser         
            Dim fromdate As DateTime
            'Dim todate As DateTime

            fromdate = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.Text & "-" & "1")
            Dim str As String
            ''Below lines are deducted on 08-01-2018
            ' dt = GetDataTable("Select IsNull(empDeductions.RequestID,0) as RequestId,Employee_ID, Employee_Name as [Employee Name], Employee_Code as [Code], EmployeeDesignationName as Designation,EmployeeDeptName as Department,RequestNo as [Request No],RequestDate as [Request Date], IsNull(AdvanceAmount,0) as [Request Amount],Isnull(DeductionDt.Deduction,0) as Issued, Case When (Isnull(AdvanceAmount,0)-Isnull(DeductionDt.Deduction,0)) > IsNull(PermonthDeduction,0) Then IsNull(PermonthDeduction,0) ELSE (Isnull(AdvanceAmount,0)-Isnull(DeductionDt.Deduction,0))  End  as Deduction,0 as Balance From EmployeesView LEFT OUTER JOIN( " _
            '& " Select EmployeeID, RequestID,RequestNo,RequestDate, AdvanceAmount,PermonthDeduction From AdvanceRequestTable WHERE (Convert(DateTime, '" & fromdate.ToString("yyyy-M-d 00:00:00") & "',102) >= Convert(dateTime,Convert(Varchar,PeriodStartDate,102),102)) AND RequestStatus='Approved'  " _
            '& " ) empDeductions on empDeductions.EmployeeID = EmployeesView.Employee_Id " _
            '& " LEFT OUTER JOIN (Select EmployeeId, RequestId, SUM(IsNull(DeductionAmount,0)) as Deduction From DeductionsDetailTable Group By EmployeeId, RequestId) " _
            '& " DeductionDt on DeductionDt.EmployeeID = EmployeesView.Employee_Id and DeductionDt.RequestId = empDeductions.RequestID " _
            '& " WHERE RequestNo <> ''")

            ''TASK TFS2040 DONE BY MUHAMMAD AMEEN. ONLY SHOW THOSE ADVANCES WHICH ADVANCE TYPE'S SALARYDEDUCT SHOULD BE EQUALED TO 1. 
            'TFS3564: 21/June/2018: Waqar Raza: Show Those records which are related to cost centers you have rights
            str = "Select IsNull(empDeductions.RequestID,0) as RequestId,Employee_ID, Employee_Name as [Employee Name], Employee_Code as [Code], EmployeeDesignationName as Designation,EmployeeDeptName as Department,RequestNo as [Request No],RequestDate as [Request Date], IsNull(AdvanceAmount,0) as [Request Amount],Isnull(DeductionDt.Deduction,0) as Issued, Case When (Isnull(AdvanceAmount,0)-Isnull(DeductionDt.Deduction,0)) > IsNull(PermonthDeduction,0) Then IsNull(PermonthDeduction,0) ELSE (Isnull(AdvanceAmount,0)-Isnull(DeductionDt.Deduction,0))  End  as Deduction,0 as Balance From EmployeesView LEFT OUTER JOIN( " _
        & " Select EmployeeID, RequestID,RequestNo,RequestDate, AdvanceAmount,PermonthDeduction, CostCentre From AdvanceRequestTable WHERE (Convert(DateTime, '" & fromdate.ToString("yyyy-M-d 00:00:00") & "',102) >= Convert(dateTime,Convert(Varchar,PeriodStartDate,102),102)) AND RequestStatus='Approved'  " _
        & " ) empDeductions on empDeductions.EmployeeID = EmployeesView.Employee_Id " _
        & " LEFT OUTER JOIN (Select EmployeeId, RequestId, SUM(IsNull(DeductionAmount,0)) as Deduction From DeductionsDetailTable Group By EmployeeId, RequestId) " _
        & " DeductionDt on DeductionDt.EmployeeID = EmployeesView.Employee_Id and DeductionDt.RequestId = empDeductions.RequestID " _
        & " WHERE RequestNo <> '' " & IIf(flgCostCenterRights = True, "AND empDeductions.CostCentre IN (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ")", "") & " "
            dt = GetDataTable(str)
            'Altered Against Task#20150508 Ali Ansari
            dt.AcceptChanges()


            dt.Columns("Balance").Expression = "[Request Amount]-([Issued]+[Deduction])"
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            For Each jRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                If Val(jRow.Cells("Balance").Value.ToString) < 0 Then
                    Throw New Exception("Deduction out of bound")
                    'Return False
                End If
                MonthYear = CDate(MyToDate(Val(Me.cmbMonth.SelectedValue), Val(Me.txtYear.Text)))
                strSQL = String.Empty
                'TFS3501: Waqar Raza: To delete that specific row which returns the False Value.
                'Start TFS3501
                If IsThisMonthDeductionDone(Me.cmbMonth.SelectedValue, Val(Me.txtYear.Text), Val(jRow.Cells("Employee_ID").Value.ToString), cmd, Val(jRow.Cells("RequestId").Value.ToString)) = True Then
                    jRow.Delete()
                End If
                'End TFS3501
            Next
            Me.grd.EditMode = Janus.Windows.GridEX.EditMode.EditOn
            Me.grd.RootTable.Columns("RequestID").Visible = False
            Me.grd.RootTable.Columns("Employee_Id").Visible = False
            'For c As Integer = 0 To Me.grd.RootTable.Columns.Count
            '    If Me.grd.RootTable.Columns(c).DataMember <> "Deduction" Then
            '        Me.grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
            '    End If
            'Next
            Me.grd.RootTable.Columns("Deduction").FormatString = "N"
            Me.grd.RootTable.Columns("Issued").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Request Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Request Date").FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns("Deduction").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Issued").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Request Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns("Deduction").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Issued").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Request Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Deduction").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Issued").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Request Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Issued").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Request Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInValue


            Me.grd.RootTable.Columns("Issued").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Request Amount").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False

            'For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns

            '    If c.Index <> 7 Then
            '        c.EditType = Janus.Windows.GridEX.EditType.NoEdit
            '    End If
            'Next
            Me.grd.RootTable.Columns("Deduction").GridEX.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.RootTable.Columns("Deduction").EditType = Janus.Windows.GridEX.EditType.TextBox
            'Me.grd.RootTable.Columns("Employee Name").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grd.RootTable.Columns("Code").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save() As Boolean
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDbCommand
        Dim MonthYear As New DateTime

        Try


            Dim strSQL As String = String.Empty

            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 120
            cmd.Connection = Con
            cmd.Transaction = objTrans

            For Each jRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows


                If Val(jRow.Cells("Balance").Value.ToString) < 0 Then
                    Throw New Exception("Deduction out of bound")
                    'Return False
                End If
                MonthYear = CDate(MyToDate(Val(Me.cmbMonth.SelectedValue), Val(Me.txtYear.Text)))
                strSQL = String.Empty
                'strSQL = "INSERT INTO DeductionsDetailTable(EntryDate,EmployeeId,RequestID,DeductionAmount,UserName) " _
                '& " VALUES(Convert(DateTime,'" & Me.dtpEntryDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & Val(jRow.Cells("Employee_ID").Value.ToString) & ", " _

                If Val(jRow.Cells("Deduction").Value.ToString) > 0 Then
                    'TFS3501: Waqar Raza: Added RequestID as Parameter to Load multiple Records
                    'Start TFS3501
                    If IsThisMonthDeductionDone(Me.cmbMonth.SelectedValue, Val(Me.txtYear.Text), Val(jRow.Cells("Employee_ID").Value.ToString), cmd, Val(jRow.Cells("RequestID").Value.ToString)) = False Then
                        'End TFS3501
                        strSQL = "INSERT INTO DeductionsDetailTable(EntryDate, EmployeeId, RequestID, DeductionAmount, UserName) " _
                   & " VALUES(Convert(DateTime,'" & MonthYear.ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & Val(jRow.Cells("Employee_ID").Value.ToString) & ", " _
                   & " " & Val(jRow.Cells("RequestID").Value.ToString) & "," & Val(jRow.Cells("Deduction").Value.ToString) & ", '" & LoginUserName.Replace("'", "''") & "')"
                        cmd.CommandText = strSQL
                        cmd.ExecuteNonQuery()


                        If (Val(jRow.Cells("Request Amount").Value.ToString) - (Val(jRow.Cells("Issued").Value.ToString) + Val(jRow.Cells("Deduction").Value.ToString))) <= 0 Then
                            strSQL = String.Empty
                            strSQL = "Update AdvanceRequestTable Set RequestStatus='Closed' WHERE RequestID=" & Val(jRow.Cells("RequestID").Value.ToString) & ""
                            cmd.CommandText = strSQL
                            cmd.ExecuteNonQuery()
                        End If
                    End If
                End If

            Next


            objTrans.Commit()
            'Insert Activity Log by Ali Faisal 
            SaveActivityLog("Payroll", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, "Employee Advance Deduction", True)
            Return True

        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    'This fuction will check the issued amount specific the same month
    'TFS3501: Waqar Raza: Added RequestID as Parameter to Load multiple Records
    'Start TFS3501
    Private Function IsThisMonthDeductionDone(ByVal Month As Integer, ByVal Year As Integer, ByVal EmployeeID As Integer, ByVal cmd As OleDbCommand, ByVal RequestID As Integer) As Boolean
        'End TFS3501
        Dim Query As String = String.Empty
        Try
            'Start TFS3501
            Query = "Select Count(*) AS Count1 FROM DeductionsDetailTable WHERE Month(EntryDate) = " & Month & " And Year(EntryDate) = " & Year & " AND EmployeeId = " & EmployeeID & " AND RequestID = " & RequestID & "  AND IsNull(DeductionAmount, 0) > 0 "
            'End TFS3501
            Dim dt As New DataTable
            cmd.CommandText = Query
            Dim Adaptor As New OleDbDataAdapter(cmd)
            Adaptor.Fill(dt)
            cmd.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            If Me.grd.RowCount = 0 Then
                ShowErrorMessage("There is no record in grid")
                Me.dtpEntryDate.Focus()
                Exit Sub
            End If

            If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            If Me.grd IsNot Nothing Then Me.grd.ClearStructure()
            FillHistory()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillHistory(Optional ByVal Condition As String = "")
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select DeductionDetailID,DeductionsDetailTable.EmployeeId,DeductionsDetailTable.RequestId,DeductionsDetailTable.EntryDate,RequestNo,RequestDate,DeductionAmount,Emp.Employee_Name as [Employee Name], Emp.Employee_Code as [Emp Code] From DeductionsDetailTable INNER JOIN tblDefEmployee Emp on Emp.Employee_Id = DeductionsDetailTable.EmployeeId INNER JOIN AdvanceRequestTable Req on Req.RequestId = DeductionsDetailTable.RequestId " & IIf(flgCostCenterRights = True, "Where Req.CostCentre IN (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ")", "") & " Order By DeductionDetailID DESC")
            dt.AcceptChanges()

            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("DeductionDetailID").Visible = False
            Me.grdSaved.RootTable.Columns("EmployeeId").Visible = False
            Me.grdSaved.RootTable.Columns("RequestId").Visible = False
            Me.grdSaved.RootTable.Columns("EntryDate").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("RequestDate").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("DeductionAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("DeductionAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("DeductionAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSaved.RootTable.Columns("DeductionAmount").FormatString = "N"
            Me.grdSaved.RootTable.Columns("DeductionAmount").TotalFormatString = "N"


            Me.grdSaved.RootTable.Columns.Add("btnUpdate")
            Me.grdSaved.RootTable.Columns.Add("btnDelete")


            Me.grdSaved.RootTable.Columns("btnUpdate").Caption = "Action"
            Me.grdSaved.RootTable.Columns("btnDelete").Caption = "Action"

            Me.grdSaved.RootTable.Columns("btnUpdate").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            Me.grdSaved.RootTable.Columns("btnUpdate").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            Me.grdSaved.RootTable.Columns("btnUpdate").ButtonText = "Update"


            Me.grdSaved.RootTable.Columns("btnDelete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            Me.grdSaved.RootTable.Columns("btnDelete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            Me.grdSaved.RootTable.Columns("btnDelete").ButtonText = "Delete"


            Me.grdSaved.AutoSizeColumns()

            ApplySecurity(EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmEmployeeDeductions_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'TFS3565: 21/Jun/2018: Waqar Raza: Added these to get the configuration
            'Start TFS3564
            If Not getConfigValueByType("RightBasedCostCenters").ToString = "Error" Then
                flgCostCenterRights = getConfigValueByType("RightBasedCostCenters")
            End If
            'End TFS3565
            Me.cmbMonth.ValueMember = "Month"
            Me.cmbMonth.DisplayMember = "Month_Name"
            Me.cmbMonth.DataSource = GetMonths()
            Me.cmbMonth.Text = Date.Now.ToString("MMMMM")

            Me.dtpEntryDate.Value = Date.Now
            Me.txtYear.Text = Date.Now.Year
            FillHistory()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            'TFS3565: 21/Jun/2018: Waqar Raza: Added these to get the configuration
            'Start TFS3564
            If Not getConfigValueByType("RightBasedCostCenters").ToString = "Error" Then
                flgCostCenterRights = getConfigValueByType("RightBasedCostCenters")
            End If
            'End TFS3565
            FillHistory()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.ColumnButtonClick

        grdSaved.UpdateData()
        If Me.grdSaved.RowCount = 0 Then Exit Sub
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDbCommand
        Try

            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            Dim strSQL As String = String.Empty
            If e.Column.Key = "btnUpdate" Then
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub

                strSQL = String.Empty
                strSQL = "Update DeductionsDetailTable SET DeductionAmount=" & Val(Me.grdSaved.GetRow.Cells("DeductionAmount").Value.ToString) & " WHERE DeductionDetailID=" & Val(Me.grdSaved.GetRow.Cells("DeductionDetailID").Value.ToString) & ""
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()



                strSQL = String.Empty
                strSQL = "Update AdvanceRequestTable Set RequestStatus= Case When IsNull(AdvanceAmount,0) > IsNull(xyz.Deduction,0) Then 'Approved' Else 'Closed' End From AdvanceRequestTable, (Select RequestId, SUM(DeductionAmount) as Deduction From DeductionsDetailTable WHERE  DeductionsDetailTable.RequestId=" & Val(Me.grdSaved.GetRow.Cells("RequestID").Value.ToString) & " Group By DeductionsDetailTable.RequestId) xyz WHERE xyz.RequestId = AdvanceRequestTable.RequestId AND AdvanceRequestTable.RequestID=" & Val(Me.grdSaved.GetRow.Cells("RequestID").Value.ToString) & ""
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()



                trans.Commit()
                FillHistory()
                Exit Sub
            End If

            If e.Column.Key = "btnDelete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub


                strSQL = String.Empty
                strSQL = "Delete From DeductionsDetailTable WHERE DeductionDetailID=" & Val(Me.grdSaved.GetRow.Cells("DeductionDetailID").Value.ToString) & ""
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()



                strSQL = String.Empty
                strSQL = "Update AdvanceRequestTable Set RequestStatus= Case When IsNull(AdvanceAmount,0) > IsNull(xyz.Deduction,0) Then 'Approved' Else 'Closed' End From AdvanceRequestTable, (Select RequestId, SUM(DeductionAmount) as Deduction From DeductionsDetailTable WHERE  DeductionsDetailTable.RequestId=" & Val(Me.grdSaved.GetRow.Cells("RequestID").Value.ToString) & " Group By DeductionsDetailTable.RequestId) xyz WHERE xyz.RequestId = AdvanceRequestTable.RequestId AND AdvanceRequestTable.RequestID=" & Val(Me.grdSaved.GetRow.Cells("RequestID").Value.ToString) & ""
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()


                trans.Commit()
                FillHistory()
                Exit Sub
            End If





        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
        End Try
    End Sub
    Private Sub btnLoad_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click

        Try

            FillGridDeductionDetail()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl2_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl2.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                Me.btnSave.Visible = False
                Me.CtrlGrdBar1.Visible = False
                Me.CtrlGrdBar1.Enabled = False
                Me.CtrlGrdBar2.Visible = True
                Me.CtrlGrdBar2.Enabled = True
            Else
                Me.btnSave.Visible = True
                Me.CtrlGrdBar1.Visible = True
                Me.CtrlGrdBar1.Enabled = True
                Me.CtrlGrdBar2.Visible = False
                Me.CtrlGrdBar2.Enabled = False
            End If
            ApplySecurity(EnumDataMode.ReadOnly)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function MyToDate(ByVal Month As Integer, ByVal Year As Integer) As DateTime
        Try
            Dim myDate As DateTime
            If Month = 2 Then
                If Date.IsLeapYear(Year) Then
                    myDate = CDate(Year & "-" & Month & "-29") 'Feb Last Date
                Else
                    myDate = CDate(Year & "-" & Month & "-28") 'Feb Last Date
                End If
            ElseIf Month = 1 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Jan Last Date
            ElseIf Month = 3 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Mar Last Date
            ElseIf Month = 4 Then
                myDate = CDate(Year & "-" & Month & "-30") 'April Last Date
            ElseIf Month = 5 Then
                myDate = CDate(Year & "-" & Month & "-31") 'May Last Date
            ElseIf Month = 6 Then
                myDate = CDate(Year & "-" & Month & "-30") 'June Last Date
            ElseIf Month = 7 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Jully Last Date
            ElseIf Month = 8 Then
                myDate = CDate(Year & "-" & Month & "-31") 'August Last Date
            ElseIf Month = 9 Then
                myDate = CDate(Year & "-" & Month & "-30") 'Sep Last Date
            ElseIf Month = 10 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Oct Last Date
            ElseIf Month = 11 Then
                myDate = CDate(Year & "-" & Month & "-30") 'Nov Last Date
            ElseIf Month = 12 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Dec Last Date
            End If
            Return myDate
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Employee Deduction"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class