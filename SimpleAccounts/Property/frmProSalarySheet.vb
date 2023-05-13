Imports SBDal
Imports SBModel
Public Class frmProSalarySheet
    Implements IGeneral
    Dim objModel As PropertySalaryBE
    Dim objDAL As PropertySalaryDAL
    Dim blnEditMode As Boolean = False
    Public Shared ProSalaryId As Integer = 0I
    Private Sub frmProSalarySheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            btnSave.FlatAppearance.BorderSize = 0
            btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

            btnCancel.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor

            'Tooltip
            Ttip.SetToolTip(Me.dtpTransactionDate, "Select transaction date")
            Ttip.SetToolTip(Me.dtpMonth, "Select month")
            Ttip.SetToolTip(Me.txtVoucherNo, "Enter voucher number")
            Ttip.SetToolTip(Me.txtRemarks, "Enter remarks")
            Ttip.SetToolTip(Me.btnCancel, "Click to close the window")
            Ttip.SetToolTip(Me.btnSave, "Click to save the data")

            ReSetControls()

            If ProSalaryId > 0 Then
                blnEditMode = True
                GetAllRecords("VoucherHistory")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns("EmployeeId").Visible = False
            Me.grd.RootTable.Columns("AccountId").Visible = False
            Me.grd.RootTable.Columns("DepartmentId").Visible = False
            Me.grd.RootTable.Columns("DesignationId").Visible = False
            Me.grd.RootTable.Columns("CostCenterId").Visible = False
            Me.grd.RootTable.Columns("ExpenseAccountId").Visible = False

            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add("Delete")
                Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("Delete").Key = "Delete"
                Me.grd.RootTable.Columns("Delete").Caption = "Action"
            End If

            Me.grd.RootTable.Columns("EmployeeId").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("EmployeeCode").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("EmployeeName").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("AccountId").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("AccountCode").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Mobile").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("DepartmentId").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Department").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("DesignationId").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Designation").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Shift").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("CostCenterId").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("CostCenter").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("ExpenseAccountId").EditType = Janus.Windows.GridEX.EditType.NoEdit

            Me.grd.RootTable.Columns("Salary").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Salary").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Salary").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Salary").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Salary").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New PropertySalaryBE
            objModel.Detail = New List(Of PropertySalaryDetailBE)
            If blnEditMode = False Then
                objModel.SalaryId = 0
                objModel.SalaryNo = GetDocumentNo()
            Else
                objModel.SalaryId = ProSalaryId
                objModel.SalaryNo = Me.txtVoucherNo.Text
            End If
            objModel.VoucherId = GetVoucherId("frmProSalarySheet", objModel.SalaryNo)
            objModel.SalaryDate = Me.dtpTransactionDate.Value
            objModel.SalaryMonth = CType(Me.dtpMonth.Value.Month, Integer)
            objModel.SalaryYear = CType(Me.dtpMonth.Value.Year, Integer)
            objModel.Remarks = Me.txtRemarks.Text
            objModel.UserName = LoginUserName
            objModel.ActivityLog = New ActivityLog
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                Dim Detail As New PropertySalaryDetailBE
                Detail.EmployeeId = Val(Row.Cells("EmployeeId").Value.ToString)
                Detail.EmployeeName = Row.Cells("EmployeeName").Value.ToString
                Detail.AccountId = Val(Row.Cells("AccountId").Value.ToString)
                Detail.CostCenterId = Val(Row.Cells("CostCenterId").Value.ToString)
                Detail.Salary = Val(Row.Cells("Salary").Value.ToString)
                Detail.ExpenseAccountId = Val(Row.Cells("ExpenseAccountId").Value.ToString)
                objModel.Detail.Add(Detail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            objDAL = New PropertySalaryDAL
            If Not Condition = "VoucherHistory" Then
                Me.grd.DataSource = objDAL.GetSalaryRecords()
                Me.grd.RetrieveStructure()
                ApplyGridSettings()
            Else
                Dim dt As DataTable = objDAL.GetById(ProSalaryId)
                If dt.Rows.Count > 0 Then
                    Me.dtpTransactionDate.Value = dt.Rows(0).Item("SalaryDate").ToString
                    Me.txtVoucherNo.Text = dt.Rows(0).Item("SalaryNo").ToString
                    Me.dtpMonth.Value = CType("01-" & dt.Rows(0).Item("SalaryMonth").ToString & "-" & dt.Rows(0).Item("SalaryYear").ToString, Date)
                    Me.txtRemarks.Text = dt.Rows(0).Item("Remarks").ToString
                    blnEditMode = True
                End If
                FillModel()
                Me.grd.DataSource = objDAL.GetVoucherRecords(objModel)
                Me.grd.RetrieveStructure()
                ApplyGridSettings()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.dtpTransactionDate.Value = Date.MinValue Then
                msg_Error("Please Select valid Transcation Date")
                Return False
            ElseIf Me.txtVoucherNo.Text = "" Then
                msg_Error("Voucher Number is missing")
                Return False
            ElseIf Me.grd.RowCount = 0 Then
                msg_Error("Detail grid is empty")
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.dtpTransactionDate.Value = Date.Now
            Me.dtpMonth.Value = Date.Now
            Me.txtVoucherNo.Text = GetDocumentNo()
            Me.txtRemarks.Text = ""
            GetAllRecords()
            blnEditMode = False
            Me.dtpTransactionDate.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New PropertySalaryDAL
            If IsValidate() = True Then
                objDAL.Add(objModel)
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New PropertySalaryDAL
            If IsValidate() = True Then
                objDAL.Update(objModel)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("EPS-" + Microsoft.VisualBasic.Right(Me.dtpTransactionDate.Value.Year, 2) + "-", "PropertySalary", "SalaryNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("EPS-" & Format(Me.dtpTransactionDate.Value, "yy") & Me.dtpTransactionDate.Value.Month.ToString("00"), 4, "PropertySalary", "SalaryNo")
            Else
                Return GetNextDocNo("EPS-", 6, "PropertySalary", "SalaryNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If blnEditMode = False Then
                If frmProSalaryList.SaveRight = True Then
                    If SalaryMonthValidation() = True Then
                        If Save() = True Then
                            ReSetControls()
                            GetAllRecords()
                            msg_Information(str_informSave)
                            Me.Close()
                        End If
                    End If
                Else
                    msg_Error("Sorry! You don't have access rights")
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                If frmProSalaryList.UpdateRight = True Then
                    Update1()
                    ReSetControls()
                    GetAllRecords()
                    ProSalaryId = 0
                    blnEditMode = False
                    msg_Information(str_informUpdate)
                    Me.Close()
                Else
                    msg_Error("Sorry! You don't have access rights")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            objDAL = New PropertySalaryDAL
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                Me.grd.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function SalaryMonthValidation() As Boolean
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT SalaryId FROM PropertySalary WHERE SalaryMonth = " & CType(Me.dtpMonth.Value.Month, Integer) & " AND SalaryYear = " & CType(Me.dtpMonth.Value.Year, Integer) & " AND SalaryId > 0"
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                msg_Error("Salary for month - " & Me.dtpMonth.Value.ToString("MMM") & " exists already.")
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class