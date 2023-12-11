'13-Jun-2015 Task#1 13-06-2015 Ahmad Sharif: add policy and reasons combo box,reset controls,add search by code and name
'2015-06-22 Task#2015060025 to only load Active Employees Ali Ansari
'12-Sep-2015 Task#12092015 Ahmad Sharif, Loan Request Print, Display Previous Receiveables of Employee

Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Net
Imports SBModel
Imports SBDal
Imports SBUtility.Utility
Public Class frmAdvanceRequest


    Implements IGeneral
    Dim isLoadedForm As Boolean = False
    Dim _RequestID As Integer = 0I
    Dim CostCentre As Integer = 0I

    ''TFS3113 : Abubakar Siddiq : This Variable is Added to check ApprovalProcessId ,if it is mapped against the document
    Dim ApprovalProcessId As Integer = 0
    Dim IsEditMode As Boolean = False
    ''TFS3113 : Abubakar Siddiq :End

    'TFS3564: Waqar Raza: Added this Variables to apply right based Cost Center
    Dim flgCostCenterRights As Boolean = False

    Enum enmgrd
        RequestID
        RequestNo
        RequestDate
        EmployeeID
        CostCentre
        Employee_Name
        Employee_Code
        EmployeeDesignationName
        CostCentreName
        EmployeeDeptName
        AdvanceAmount
        PeriodStartDate
        PeriodEndDate
        PreReceiveables
        PerMonthDeduction
        RequestStatus
        Start_Ded_Month
        Start_Ded_Year
        Loan_PolicyID
        Loan_ReasonID
        Advance_TypeID ''TFS2040
        AdvanceType ''TFS2040
        Loan_Details

    End Enum


    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try


            Me.grd.RootTable.Columns("RequestID").Visible = False
            Me.grd.RootTable.Columns("EmployeeID").Visible = False
            Me.grd.RootTable.Columns("CostCentre").Visible = False
            Me.grd.RootTable.Columns("Loan_PolicyID").Visible = False
            Me.grd.RootTable.Columns("Loan_ReasonID").Visible = False
            Me.grd.RootTable.Columns("Advance_TypeID").Visible = False
            Me.grd.RootTable.Columns(enmgrd.PeriodEndDate).Visible = False
            Me.grd.RootTable.Columns("Start_Ded_Month").Caption = "Month"
            Me.grd.RootTable.Columns("Start_Ded_Year").Caption = "Year"

            Me.grd.RootTable.Columns("AdvanceType").EditType = Janus.Windows.GridEX.EditType.NoEdit   ''TFS2040

            Me.grd.RootTable.Columns(enmgrd.RequestDate).FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns(enmgrd.AdvanceAmount).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(enmgrd.PeriodStartDate).FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns(enmgrd.PeriodEndDate).FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns(enmgrd.PreReceiveables).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(enmgrd.PerMonthDeduction).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(enmgrd.AdvanceAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(enmgrd.AdvanceAmount).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(enmgrd.AdvanceAmount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(enmgrd.AdvanceAmount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
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
                            Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                            Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                            Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                        End If
                    End If
                    'GetSecurityByPostingUser(UserPostingRights, BtnSave, BtnDelete)
                Else
                    'Me.Visible = False
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    Me.btnPrint.Enabled = False
                    For Each RightsDt As GroupRights In Rights
                        If RightsDt.FormControlName = "View" Then
                            'Me.Visible = True
                        ElseIf RightsDt.FormControlName = "Save" Then
                            If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                        ElseIf RightsDt.FormControlName = "Update" Then
                            If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                        ElseIf RightsDt.FormControlName = "Delete" Then
                            Me.btnDelete.Enabled = True
                        ElseIf RightsDt.FormControlName = "Print" Then
                            Me.btnPrint.Enabled = True
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDbCommand
        Try


            cmd.Connection = Con
            cmd.Transaction = objTrans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text


            cmd.CommandText = "Select RequestId From DeductionsDetailTable WHERE RequestID=" & Val(Me.grd.GetRow.Cells("RequestID").Value.ToString) & ""
            Dim intID As Integer = cmd.ExecuteScalar

            If intID > 0 Then
                ShowErrorMessage(str_ErrorDependentRecordFound)
                Return False
            End If

            cmd.CommandText = ""
            cmd.CommandText = "Delete From tblvoucherdetail where voucher_id  in(Select voucher_id from tblvoucher where voucher_no='" & Me.grd.GetRow.Cells("RequestNo").Value.ToString & "')"
            cmd.ExecuteNonQuery()


            cmd.CommandText = ""
            cmd.CommandText = "Delete from tblvoucher where voucher_no='" & Me.grd.GetRow.Cells("RequestNo").Value.ToString & "'"
            cmd.ExecuteNonQuery()


            cmd.CommandText = ""
            cmd.CommandText = "Delete From  AdvanceRequestTable WHERE RequestID=" & Val(Me.grd.GetRow.Cells("RequestID").Value.ToString) & ""
            cmd.ExecuteNonQuery()


            objTrans.Commit()
            ''Start TFS3113 : Ayesha Rehman : 09-04-2018
            If IsEditMode = True Then
                If ValidateApprovalProcessMapped(Me.txtReqNo.Text.Trim) Then
                    If ValidateApprovalProcessInProgress(Me.txtReqNo.Text.Trim) Then
                        msg_Error("Document is in Approval Process ") : Exit Function
                    End If
                End If
            End If
            ''End TFS3113
            Return True


        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try

            If Condition = "" Then
                'Marked Against Task#2015060025 to only load Active Employees Ali Ansari
                'FillUltraDropDown(Me.cmbEmployees, "Select Employee_Id, Employee_Name, Employee_Code, EmployeeDesignationName as Designation,EmployeeDeptName as Department From EmployeesView ")
                'Marked Against Task#2015060025 to only load Active Employees Ali Ansari
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                Dim str As String = "Select Employee_Id, Employee_Name, Employee_Code, EmployeeDesignationName as Designation,EmployeeDeptName as Department From EmployeesView where active = 1 " & IIf(flgCostCenterRights = True, "AND costcentre IN (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ")", "") & " "
                FillUltraDropDown(Me.cmbEmployees, str)
                'Altered Against Task#2015060025 to only load Active Employees Ali Ansari
                cmbEmployees.Rows(0).Activate()
                Me.cmbEmployees.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'Task#1 13-06-2015 Fill combo box of policy and reasons
            ElseIf Condition = "Policy" Then
                FillUltraDropDown(Me.cmbPolicy, "select LoanPolicyID,LoanPolicyName from tblLoanPolicy")
                Me.cmbPolicy.Rows(0).Activate()             'Selecting the first index
                Me.cmbPolicy.DisplayLayout.Bands(0).Columns("LoanPolicyID").Hidden = True                   'Hiding the LoanPolicyID column in cmbPolicy  
                Me.cmbPolicy.DisplayLayout.Bands(0).Columns("LoanPolicyName").Header.Caption = "Load Policy Name"   'Changing the Caption of LoanPolicyName in cmbPolicy
            ElseIf Condition = "AdvanceType" Then ''TFS2040
                FillUltraDropDown(Me.cmbAdvanceType, "select Id, Title,SalaryDeduct from tblDefAdvancesType where Active = 1")
                Me.cmbAdvanceType.Rows(0).Activate()             'Selecting the first index
                Me.cmbAdvanceType.DisplayLayout.Bands(0).Columns("Id").Hidden = True 'Hiding the AdvanceTypeId column in AdvanceType
                Me.cmbAdvanceType.DisplayLayout.Bands(0).Columns("SalaryDeduct").Hidden = True 'Hiding the AdvanceTypeId column in AdvanceType



            ElseIf Condition = "Reasons" Then
                FillUltraDropDown(Me.cmbReason, "select LoanReasonID,LoanReason from tblLoanReasons")
                Me.cmbReason.Rows(0).Activate()     'Selecting the first index
                Me.cmbReason.DisplayLayout.Bands(0).Columns("LoanReasonID").Hidden = True       'Hiding the LoanPolicyID column in cmbReason
                Me.cmbReason.DisplayLayout.Bands(0).Columns("LoanReason").Header.Caption = "Load Reason"    'Changing the Caption of LoanPolicyName in cmbReason
                'End Task#1 13-06-2015
            ElseIf Condition = "CostCentre" Then
                FillDropDown(Me.cmbCostCentre, "Select CostCenterID, Name, Code, SortOrder From tblDefCostCenter Where Active = 1 And CostCenterID = " & CostCentre & " ORDER BY 2 ASC", False) ''TASKTFS75 added and set active =1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Dim dt As New DataTable
            'dt = GetDataTable("SELECT " & IIf(Condition = "All", "", " TOP 50") & " dbo.AdvanceRequestTable.RequestID, dbo.AdvanceRequestTable.RequestNo, dbo.AdvanceRequestTable.RequestDate,  " _
            '         & " dbo.AdvanceRequestTable.EmployeeID, dbo.EmployeesView.Employee_Name, dbo.EmployeesView.Employee_Code AS [Emp Code],   " _
            '         & " dbo.EmployeesView.EmployeeDesignationName AS Designation, dbo.EmployeesView.EmployeeDeptName AS Department,  " _
            '         & " dbo.AdvanceRequestTable.AdvanceAmount AS [Loan Amount], dbo.AdvanceRequestTable.PeriodStartDate, dbo.AdvanceRequestTable.PeriodEndDate,  " _
            '         & " dbo.AdvanceRequestTable.PerMonthDeduction, dbo.AdvanceRequestTable.RequestStatus " _
            '         & " FROM dbo.AdvanceRequestTable INNER JOIN dbo.EmployeesView ON dbo.AdvanceRequestTable.EmployeeID = dbo.EmployeesView.Employee_ID ORDER BY dbo.AdvanceRequestTable.RequestID DESC")


            dt = GetDataTable("SELECT " & IIf(Condition = "All", "", " TOP 50") & " dbo.AdvanceRequestTable.RequestID, dbo.AdvanceRequestTable.RequestNo, dbo.AdvanceRequestTable.RequestDate,  " _
                   & " dbo.AdvanceRequestTable.EmployeeID, IsNull(dbo.AdvanceRequestTable.CostCentre, 0) As CostCentre, dbo.EmployeesView.Employee_Name, dbo.EmployeesView.Employee_Code AS [Emp Code],   " _
                   & " dbo.EmployeesView.EmployeeDesignationName AS Designation, tblDefCostCenter.Name As [Cost Center Name], dbo.EmployeesView.EmployeeDeptName AS Department,  " _
                   & " dbo.AdvanceRequestTable.AdvanceAmount AS [Loan Amount], dbo.AdvanceRequestTable.PeriodStartDate, dbo.AdvanceRequestTable.PeriodEndDate,IsNull(dbo.AdvanceRequestTable.Pre_Receiveables,0) as PreReceiveables,  " _
                   & " dbo.AdvanceRequestTable.PerMonthDeduction, dbo.AdvanceRequestTable.RequestStatus, Start_Ded_Month,Start_Ded_Year,IsNull(Loan_PolicyID,0) as Loan_PolicyID,IsNull(Loan_ReasonID,0) as Loan_ReasonID, IsNull(Advance_TypeID,0) as Advance_TypeID ,AdvanceType ,Loan_Details " _
                   & " FROM dbo.AdvanceRequestTable INNER JOIN dbo.EmployeesView ON dbo.AdvanceRequestTable.EmployeeID = dbo.EmployeesView.Employee_ID LEFT JOIN tblDefCostCenter ON dbo.AdvanceRequestTable.CostCentre = tblDefCostCenter.CostCenterID " & IIf(flgCostCenterRights = True, "WHERE AdvanceRequestTable.CostCentre IN (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ")", "") & " ORDER BY dbo.AdvanceRequestTable.RequestID DESC")

            dt.AcceptChanges()

            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()

            ApplyGridSettings()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbEmployees.ActiveRow Is Nothing Then Return False

            If Val(Me.txtYear.Text) < 4 Then
                ShowErrorMessage("Invalid Year.")
                Me.txtYear.Focus()
                Return False
            End If
            ''TFS2024


            'start task 3205: Abubakar Siddiq
            If cmbStartMonth.SelectedValue < dtpReqDate.Value.Month AndAlso Me.txtYear.Text <= dtpReqDate.Value.Year Then
                ShowErrorMessage("Deduction month should be greater or equal to request month")
                Return False
            End If

            If Me.txtYear.Text < dtpReqDate.Value.Year Then
                ShowErrorMessage("Deduction year should be greater or equal to request year")
                Return False
            End If
            'end task 3205: Abubakar Siddiq


            'start task 3113 Added by abubakar Siddiq
            If IsEditMode = True Then
                If ValidateApprovalProcessMapped(Me.txtReqNo.Text.Trim) Then
                    If ValidateApprovalProcessInProgress(Me.txtReqNo.Text.Trim) Then
                        msg_Error("Document is in Approval Process") : Return False : Exit Function
                    End If
                End If
            End If
            'end task 3113, Added by Abubakar Siddiq

            If Me.cmbAdvanceType.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please select AdvanceType")
                Me.cmbEmployees.Focus()
                Return False
            End If
            ''End TFS2040
            If Me.cmbEmployees.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please select Employee")
                Me.cmbEmployees.Focus()
                Return False
            End If

            If Val(Me.txtLoanAmount.Text) <= 0 Then
                ShowErrorMessage("Please enter loan amount.")
                Me.txtLoanAmount.Focus()
                Return False
            End If

            'Task#1 13-06-2015 comapre loan amount with  Deduction amount 
            If Val(Me.txtPermonthdeduction.Text) > Val(Me.txtLoanAmount.Text) Then
                ShowErrorMessage("Deduction amount should be less than Loan Amount")
                Me.txtPermonthdeduction.Focus()
                Return False
            End If
            'End Task#1 13-06-2015

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            'TFS3564: 20/Jun/2018: Waqar Raza: Added these to get the configuration
            'Start TFS3564
            If Not getConfigValueByType("RightBasedCostCenters").ToString = "Error" Then
                flgCostCenterRights = getConfigValueByType("RightBasedCostCenters")
            End If
            'End TFS3564
            Me.btnSave.Text = "&Save"
            _RequestID = 0
            Me.txtReqNo.Text = GetNextDocNo("LR-" & Me.dtpReqDate.Value.ToString("MMyy") & "", 3, "AdvanceRequestTable", "RequestNo")
            Me.cmbRequestStatus.SelectedIndex = 0
            Me.txtLoanAmount.Text = String.Empty
            Me.txtLoanAmount.Enabled = True
            Me.cmbEmployees.Rows(0).Activate()
            If Not Me.cmbCostCentre.SelectedIndex = -1 Then Me.cmbCostCentre.SelectedIndex = 0
            'Me.dtpPeriodStartDate.Value = Date.Now
            'Me.dtpPeriodEndDate.Value = Date.Now
            Me.txtPermonthdeduction.Text = String.Empty
            'Task#1 13-06-2015 resetting those controls
            Me.cmbPolicy.Rows(0).Activate()
            Me.cmbAdvanceType.Rows(1).Activate() ''TFS2040
            Me.cmbReason.Rows(0).Activate()
            Me.txtLoanDetails.Text = String.Empty
            Me.cmbStartMonth.Text = Date.Now.ToString("MMMM")
            Me.txtReceiveable.Text = String.Empty
            Me.PnlSalaryDeduction.Visible = True
            Me.PnlSalaryDeduction.Location = New Point(360, 20)
            Me.pnlDetail.Location = New Point(360, 114)
            'End Task#1 13-06-2015
            GetAllRecords("All")


            'Abubakar Siddiq : TFS3113 : Enable Approval History button only in Eidt Mode
            If IsEditMode = True Then
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
            Else
                Me.btnApprovalHistory.Visible = False
            End If
            'Abubakar Siddiq : TFS3113 : End


            ApplySecurity(EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As OleDbTransaction = objCon.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = objTrans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text


            Dim strSQL As String = String.Empty
            Dim stDate As String = Me.txtYear.Text & "-" & Me.cmbStartMonth.Text & "-1"

            'TFS3564: 20/Jun/2018: Waqar Raza: Added these to get Right Based CostCenters
            'Start TFS3564
            If flgCostCenterRights = True Then
                If Not Me.cmbCostCentre.SelectedValue > 0 Then
                    msg_Error("Please select a Cost Center") : Me.cmbCostCentre.Focus() : Return False : Exit Function
                End If
            End If
            'End TFS3564
            'Saving Employee Advance 
            ''TFS2040 Saving Data in a new column AdvanceTypeId
            strSQL = "INSERT INTO AdvanceRequestTable(RequestNo,RequestDate,EmployeeID,AdvanceAmount,PeriodStartDate,PeriodEndDate,PerMonthDeduction,RequestStatus,UserName,EntryDate,Start_Ded_Month,Start_Ded_Year,Loan_PolicyID,Loan_ReasonID,Advance_TypeID,Loan_Details,Pre_Receiveables, CostCentre,AdvanceType) " _
            & " VALUES('" & Me.txtReqNo.Text.Replace("'", "''") & "', " _
            & " Convert(Datetime,'" & Me.dtpReqDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " _
            & " " & Me.cmbEmployees.Value & ", " _
            & " " & Val(Me.txtLoanAmount.Text) & ", " _
            & " Convert(Datetime,'" & CDate(stDate).ToString("yyyy-M-d hh:mm:ss tt") & "',102), " _
            & "  null , " _
            & " " & Val(Me.txtPermonthdeduction.Text) & ", " _
            & " 'Open', " _
            & " '" & LoginUserName.Replace("'", "''") & "', " _
            & "Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " _
            & " '" & Me.cmbStartMonth.Text.Replace("'", "''") & "', " _
            & " '" & Me.txtYear.Text.Replace("'", "''") & "', " _
            & " " & Me.cmbPolicy.Value & ", " _
            & " " & Me.cmbReason.Value & ", " _
            & " " & Me.cmbAdvanceType.Value & ", " _
            & " '" & Me.txtLoanDetails.Text.Replace("'", "''") & "'," _
            & " " & Val(Me.txtReceiveable.Text) & ", " & IIf(Me.cmbCostCentre.SelectedIndex = -1, 0, Me.cmbCostCentre.SelectedValue) & ",N'" & cmbAdvanceType.Text & " ') Select @@Identity"

            cmd.CommandText = strSQL
            _RequestID = CInt(cmd.ExecuteScalar())


            ''Start TFS3113
            ''insert Approval Log
            SaveApprovalLog(EnumReferenceType.EmployeeLoanRequest, _RequestID, Me.txtReqNo.Text.Trim, Me.dtpReqDate.Value.Date, "Employee Loan Request ," & cmbAdvanceType.Text & "", Me.Name)
            ''End TFS3113


            objTrans.Commit()
            Return True


        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDbCommand
        Try


            cmd.Connection = Con
            cmd.Transaction = objTrans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text


            Dim dtpStartDeductionDate As DateTime = CStr(Val(Me.txtYear.Text) & "-" & Me.cmbStartMonth.Text & "-1")


            'Ahmad Sharif comment query
            cmd.CommandText = "UPDATE AdvanceRequestTable SET RequestNo='" & Me.txtReqNo.Text.Replace("'", "''") & "',RequestDate=Convert(Datetime,'" & Me.dtpReqDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),EmployeeID=" & Me.cmbEmployees.Value & ",AdvanceAmount=" & Val(Me.txtLoanAmount.Text) & ",PeriodStartDate=Convert(Datetime,'" & dtpStartDeductionDate.ToString("yyyy-M-d") & "',102),PeriodEndDate=null,PerMonthDeduction=" & Val(Me.txtPermonthdeduction.Text) & ", UserName='" & LoginUserName.Replace("'", "''") & "',EntryDate=Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102), Start_Ded_Month='" & Me.cmbStartMonth.Text.Replace("'", "''") & "',Start_Ded_Year='" & Me.txtYear.Text.Replace("'", "''") & "',Loan_PolicyID=" & Me.cmbPolicy.Value & ",Loan_ReasonID=" & Me.cmbReason.Value & ",Advance_TypeID=" & Me.cmbAdvanceType.Value & ",Loan_Details='" & Me.txtLoanDetails.Text.Replace("'", "''") & "',Pre_Receiveables = " & Val(Me.txtReceiveable.Text) & ", CostCentre = " & IIf(Me.cmbCostCentre.SelectedIndex = -1, 0, Me.cmbCostCentre.SelectedValue) & " WHERE RequestID=" & _RequestID & ""
            cmd.ExecuteNonQuery()

            ''Start TFS3113
            If ValidateApprovalProcessMapped(Me.txtReqNo.Text.Trim, Me.Name) Then
                If ValidateApprovalProcessIsInProgressAgain(Me.txtReqNo.Text.Trim, Me.Name) = False Then
                    SaveApprovalLog(EnumReferenceType.EmployeeLoanRequest, _RequestID, Me.txtReqNo.Text.Trim, Me.dtpReqDate.Value.Date, "Employee Loan Request ," & cmbAdvanceType.Text & "", Me.Name)
                End If
            End If
            ''End TFS3113

            objTrans.Commit()
            Return True


        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub EditRecords()
        Try

            IsEditMode = True

            If Me.grd.RowCount = 0 Then Exit Sub
            _RequestID = Me.grd.GetRow.Cells(enmgrd.RequestID).Value
            Me.txtReqNo.Text = Me.grd.GetRow.Cells(enmgrd.RequestNo).Value.ToString
            Me.dtpReqDate.Value = Me.grd.GetRow.Cells(enmgrd.RequestDate).Value
            Me.cmbEmployees.Value = Me.grd.GetRow.Cells(enmgrd.EmployeeID).Value
            Me.cmbCostCentre.SelectedValue = Me.grd.GetRow.Cells(enmgrd.CostCentre).Value
            If Me.grd.GetRow.Cells(enmgrd.RequestStatus).Value.ToString = "Open" Then
                Me.txtLoanAmount.Text = Me.grd.GetRow.Cells(enmgrd.AdvanceAmount).Value
                Me.txtLoanAmount.Enabled = True
            Else
                Me.txtLoanAmount.Text = Me.grd.GetRow.Cells(enmgrd.AdvanceAmount).Value
                Me.txtLoanAmount.Enabled = False
            End If
            'Ahmad Sharif comment
            'Me.dtpPeriodStartDate.Value = Me.grd.GetRow.Cells(enmgrd.PeriodStartDate).Value
            'Me.dtpPeriodEndDate.Value = Me.grd.GetRow.Cells(enmgrd.PeriodEndDate).Value
            Me.txtPermonthdeduction.Text = Val(Me.grd.GetRow.Cells(enmgrd.PerMonthDeduction).Value.ToString)
            Me.cmbStartMonth.Text = Me.grd.GetRow.Cells("Start_Ded_Month").Value.ToString
            Me.txtYear.Text = Me.grd.GetRow.Cells("Start_Ded_Year").Value.ToString
            Me.cmbPolicy.Value = Val(Me.grd.GetRow.Cells("Loan_PolicyID").Value.ToString)
            Me.cmbReason.Value = Val(Me.grd.GetRow.Cells("Loan_ReasonID").Value.ToString)
            Me.cmbAdvanceType.Value = Val(Me.grd.GetRow.Cells("Advance_TypeID").Value.ToString) ''TFS2040
            Me.txtReceiveable.Text = Val(Me.grd.GetRow.Cells("PreReceiveables").Value.ToString)
            Me.btnSave.Text = "&Update"
            ApplySecurity(EnumDataMode.Edit)
            Me.txtLoanAmount.Focus()


            ''TASK 4902 DONE ON 14-11-2018
            If Me.cmbAdvanceType.Value > 0 AndAlso CType(Me.cmbAdvanceType.ActiveRow.Cells("SalaryDeduct").Value, Boolean) = True Then
                Me.PnlSalaryDeduction.Visible = True
                Me.pnlDetail.Location = New Point(360, 114)
            Else
                Me.PnlSalaryDeduction.Visible = False
                Me.pnlDetail.Location = New Point(360, 20)
            End If
            ''END TASK 4902

            ''Abubakar Siddiq :TFS3113 :Making Approval Button Enable in Edit Mode
            Me.btnApprovalHistory.Visible = True
            Me.btnApprovalHistory.Enabled = True
            ''Abubakar Siddiq :TFS3113 :End

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' This Function is Added to open the form in edit mode ,when the its link clicked from any where
    ''' </summary>
    ''' <param name="RequestNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_All(ByVal RequestNo As String)
        Try
            frmAdvanceRequest_Shown(Nothing, Nothing)
            Get_All = Nothing
            If Not RequestNo.Length > 0 Then Exit Try
            'If IsOpenForm = True Then
            If RequestNo.Length > 0 Then
                Dim str As String = "Select * from AdvanceRequestTable where RequestNo =N'" & RequestNo & "'"
                Dim dt As DataTable = GetDataTable(str)
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Dim flag As Boolean = False
                        flag = Me.grd.FindAll(Me.grd.RootTable.Columns(enmgrd.RequestNo), Janus.Windows.GridEX.ConditionOperator.Equal, RequestNo)
                        'If flag = True Then
                        Me.grd_DoubleClick(Nothing, Nothing)
                    End If
                Else
                    Exit Function
                End If
            Else
                Exit Function
                'End If
            End If
            'IsDrillDown = False
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
            Delete()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = False Then Exit Sub
            If btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                Save()
                ReSetControls()
            Else
                If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                Update1()
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmAdvanceRequest_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'TFS3564: 20/Jun/2018: Waqar Raza: Added these to get the configuration
            'Start TFS3564
            If Not getConfigValueByType("RightBasedCostCenters").ToString = "Error" Then
                flgCostCenterRights = getConfigValueByType("RightBasedCostCenters")
            End If
            'End TFS3564
            Me.txtYear.Text = Date.Today.Year.ToString

            FillCombos()
            FillCombos("Policy")        'Task#1 13-06-2015  filling policy combo box
            FillCombos("Reasons")
            FillCombos("AdvanceType") ''TFS2040
            cmbStartMonth.DropDownStyle = ComboBoxStyle.DropDownList
            Me.cmbStartMonth.ValueMember = "Month"
            Me.cmbStartMonth.DisplayMember = "Month_Name"
            Me.cmbStartMonth.DataSource = GetMonths()

            Me.cmbStartMonth.Text = Date.Now.ToString("MMMMM")
            isLoadedForm = True 'Task#1 13-06-2015  filling reasons combo box
            ReSetControls()
            ''TFS3497: Aashir: Added Smart search on drop downs
            UltraDropDownSearching(cmbEmployees, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.DoubleClick
        Try
            ''Task#1 13-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
            If Me.grd.Row < 0 Then
                Exit Sub
            Else
                EditRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        ''End Task#1 13-06-2015
    End Sub


    ''Task#1 13-06-2015 Add Radio Btn checked event for search by Employee code and Employee Name
    Private Sub rbtByCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnByCode.CheckedChanged
        Try

            If isLoadedForm = False Then
                Exit Sub
            End If
            If rbtnByCode.Checked = True Then
                Me.cmbEmployees.DisplayMember = Me.cmbEmployees.DisplayLayout.Bands(0).Columns("Employee_Code").Key.ToString
            Else
                Me.cmbEmployees.DisplayMember = Me.cmbEmployees.DisplayLayout.Bands(0).Columns("Employee_Name").Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtByName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnByName.CheckedChanged
        Try
            If isLoadedForm = False Then
                Exit Sub
            End If

            If rbtnByName.Checked = True Then
                Me.cmbEmployees.DisplayMember = Me.cmbEmployees.DisplayLayout.Bands(0).Columns("Employee_Name").Key.ToString
            Else
                Me.cmbEmployees.DisplayMember = Me.cmbEmployees.DisplayLayout.Bands(0).Columns("Employee_Code").Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''End Task#1 13-06-2015


    ''Task#1 13-06-2015 add Key down for save and cancel with F4 and Esc button form keyboard
    Private Sub FrmAdvanceRequest_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                ReSetControls()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    ''End Task#1 13-06-2015
    Private Sub txtYear_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtYear.KeyPress, txtLoanAmount.KeyPress, txtPermonthdeduction.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task#12092015  Print of Loan Request 
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            AddRptParam("@RequestID", Me.grd.CurrentRow.Cells("RequestID").Value.ToString)
            ShowReport("rptEmployeeLoanRequest")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#12092015 


    'Getting Employee Receiveable
    Private Sub btnRefresReceiveable_Click(sender As Object, e As EventArgs) Handles btnRefresReceiveable.Click
        Try
            GetEmployeeReceiveable()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task#12092015 Getting Employee Wise Receiveable by ahmad sharif
    Private Sub GetEmployeeReceiveable()

        Try

            Dim strSQL As String = String.Empty
            Dim dt As New DataTable

            'Getting Accout Id against Employee Id
            Dim AccId As Integer = 0I
            strSQL = String.Empty
            strSQL = "select IsNull(EmpSalaryAccountId,0) as AccId, IsNull(CostCentre, 0) As CostCentre from tblDefEmployee where Employee_ID = " & Val(Me.cmbEmployees.ActiveRow.Cells(0).Value.ToString)
            dt = GetDataTable(strSQL)

            If dt.Rows.Count > 0 Then
                AccId = Val(dt.Rows(0).Item(0).ToString)
                CostCentre = Val(dt.Rows(0).Item(1).ToString)
            Else
                AccId = 0
                CostCentre = 0
            End If
            dt = Nothing

            'Getting Previous Receiveables against Employee Id and Account Id
            strSQL = String.Empty
            strSQL = "select (IsNull(debit_amount,0) - IsNull(credit_amount,0)) as Receiveables from tblVoucherDetail where coa_detail_id = " & AccId & " And EmpID = " & Val(Me.cmbEmployees.ActiveRow.Cells(0).Value.ToString)
            dt = GetDataTable(strSQL)

            If dt.Rows.Count > 0 Then
                Me.txtReceiveable.Text = Val(dt.Rows(0).Item(0).ToString())
            Else
                Me.txtReceiveable.Text = "0"
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally

        End Try
    End Sub

    Private Sub cmbEmployees_ValueChanged(sender As Object, e As EventArgs) Handles cmbEmployees.ValueChanged
        Try
            If isLoadedForm = False Then Exit Sub
            If Me.cmbEmployees.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbEmployees.IsItemInList = False Then Exit Sub

            GetEmployeeReceiveable()
            FillCombos("CostCentre")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#12092015

    Private Sub frmAdvanceRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click
        Try

            If Not frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
                frmMain.LoadControl("frmSystemConfiguration")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Accounts
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not grd.GetRow Is Nothing AndAlso grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frmAdavnceRequest"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grd.CurrentRow.Cells(1).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Loan request (" & frmtask.Ref_No & ") "
                frmtask.Width = 950
                frmtask.ShowDialog()
                frmtask.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
                'frmtask.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbAdvanceType_Leave(sender As Object, e As EventArgs) Handles cmbAdvanceType.Leave
        'Try
        '    If Me.cmbAdvanceType.Value > 0 AndAlso CType(Me.cmbAdvanceType.ActiveRow.Cells("SalaryDeduct").Value, Boolean) = True Then
        '        Me.PnlSalaryDeduction.Visible = True
        '        Me.pnlDetail.Location = New Point(360, 114)
        '    Else
        '        Me.PnlSalaryDeduction.Visible = False
        '        Me.pnlDetail.Location = New Point(360, 20)
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        Try
            Dim id As Integer = 0I

            id = Me.cmbEmployees.Value
            FillCombos(" ")
            Me.cmbEmployees.Value = id

            id = Me.cmbAdvanceType.Value
            FillCombos("AdvanceType")
            Me.cmbAdvanceType.Value = id

            id = Me.cmbCostCentre.SelectedIndex
            FillCombos("CostCentre")
            Me.cmbCostCentre.SelectedIndex = id


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnApprovalHistory_Click(sender As Object, e As EventArgs) Handles btnApprovalHistory.Click
        Try
            frmApprovalHistory.DocumentNo = Me.txtReqNo.Text
            frmApprovalHistory.Source = Me.Name
            frmApprovalHistory.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "EmployeeAdvanceRequest"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbAdvanceType_ValueChanged(sender As Object, e As EventArgs) Handles cmbAdvanceType.ValueChanged
        Try
            If isLoadedForm = False Then Exit Sub
            If Me.cmbAdvanceType.Value > 0 AndAlso CType(Me.cmbAdvanceType.ActiveRow.Cells("SalaryDeduct").Value, Boolean) = True Then
                Me.PnlSalaryDeduction.Visible = True
                Me.pnlDetail.Location = New Point(360, 114)
            Else
                Me.PnlSalaryDeduction.Visible = False
                Me.pnlDetail.Location = New Point(360, 20)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class