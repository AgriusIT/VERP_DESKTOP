''TASK TFS3670 Muhammad Amin : Bifurcation configuration does not allow value like 0.1254. Dated 25-06-2018

Imports SBDal
Imports SBModel
Public Class frmLogicalBifurcation
    Implements IGeneral

    Dim LogicalB As LogicalBifurcationBE
    Dim LogicalBDAL As LogicalBifurcationDAL
    Dim LogicalBDetailDAL As LogicalBifurcationDetailDAL
    Public LogicalBifurcationId As Integer = 0
    Public IsEditMode As Boolean = False

    'LogicalBifurcationDetailId, LogicalBifurcationId, ToCostCenterId, tblDefCostCenter.Name AS ToCostCenter, AmountPercentage, Comments
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        LogicalB = New LogicalBifurcationBE()
        LogicalBDAL = New LogicalBifurcationDAL()
        LogicalBDetailDAL = New LogicalBifurcationDetailDAL()
        ' Add any initialization after the InitializeComponent() call.
        ReSetControls()
        IsEditMode = False
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor
    End Sub
    Public Sub New(ByVal HaveSaveRights As Boolean)

        ' This call is required by the designer.
        InitializeComponent()
        LogicalB = New LogicalBifurcationBE()
        LogicalBDAL = New LogicalBifurcationDAL()
        LogicalBDetailDAL = New LogicalBifurcationDetailDAL()
        ' Add any initialization after the InitializeComponent() call.
        ReSetControls()
        Me.btnSave.Enabled = HaveSaveRights
        IsEditMode = False
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor
    End Sub
    Public Sub New(ByVal Obj As LogicalBifurcationBE, ByVal HaveUpdateRights As Boolean)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        LogicalB = New LogicalBifurcationBE()
        LogicalBDAL = New LogicalBifurcationDAL()
        LogicalBDetailDAL = New LogicalBifurcationDetailDAL()
        ReSetControls()
        EditRecord(Obj)
        Me.btnSave.Enabled = HaveUpdateRights
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor
    End Sub
    Structure Detail
        Shared LogicalBifurcationDetailId As String = "LogicalBifurcationDetailId"
        Shared LogicalBifurcationId As String = "LogicalBifurcationId"
        Shared ToCostCenterId As String = "ToCostCenterId"
        Shared ToCostCenter As String = "ToCostCenter"
        Shared AmountPercentage As String = "AmountPercentage"
        Shared Comments As String = "Comments"
        Shared IsDeleted As String = "IsDeleted"
    End Structure
    Private Function GetBifurcationNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("BOC" + "-" + Microsoft.VisualBasic.Right(dtpDocumentDate.Value.Year, 2) + "-", "LogicalBifurcation", "DocumentNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("BOC" & "-" & Format(dtpDocumentDate.Value, "yy") & dtpDocumentDate.Value.Month.ToString("00"), 4, "LogicalBifurcation", "DocumentNo")
            Else
                Return GetNextDocNo("BOC", 6, "LogicalBifurcation", "DocumentNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim SqlQuery As String = ""
        Try
            SqlQuery = "Select CostCenterID AS ID, Name FROM tblDefCostCenter WHERE ISNULL(IsLogical, 0)=1 AND ISNULL(Active, 0)=1"
            FillUltraDropDown(Me.cmbFromCostCenter, SqlQuery)
            Me.cmbFromCostCenter.Rows(0).Activate()
            Me.cmbFromCostCenter.DisplayLayout.Bands(0).Columns(0).Hidden = True
            Me.cmbFromCostCenter.DisplayLayout.Bands(0).Columns(1).Width = 300

            SqlQuery = "Select CostCenterID AS ID, Name FROM tblDefCostCenter WHERE ISNULL(IsLogical, 0)=0 AND ISNULL(Active, 0)=1 "
            FillUltraDropDown(Me.cmbToCostCenter, SqlQuery)
            Me.cmbToCostCenter.Rows(0).Activate()
            Me.cmbToCostCenter.DisplayLayout.Bands(0).Columns(0).Hidden = True
            Me.cmbToCostCenter.DisplayLayout.Bands(0).Columns(1).Width = 300

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            LogicalB = New LogicalBifurcationBE()
            LogicalB.DocumentNo = Me.txtDocumentNo.Text
            LogicalB.DocumentDate = Me.dtpDocumentDate.Value
            LogicalB.FromCostCenterId = Me.cmbFromCostCenter.Value
            LogicalB.StartDate = Me.dtpStartDate.Value
            LogicalB.Remarks = Me.txtRemarks.Text
            LogicalB.LogicalBifurcationId = LogicalBifurcationId
            LogicalB.Detail = New List(Of LogicalBifurcationDetailBE)()
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                Dim SingleObj As New LogicalBifurcationDetailBE
                SingleObj.LogicalBifurcationDetailId = Row.Cells(Detail.LogicalBifurcationDetailId).Value
                SingleObj.LogicalBifurcationId = Row.Cells(Detail.LogicalBifurcationId).Value
                SingleObj.ToCostCenterId = Row.Cells(Detail.ToCostCenterId).Value
                SingleObj.Amount = Math.Round(Row.Cells(Detail.AmountPercentage).Value, DecimalPointInValue)
                SingleObj.Comments = Row.Cells(Detail.Comments).Value
                SingleObj.IsDeleted = Row.Cells(Detail.IsDeleted).Value
                LogicalB.Detail.Add(SingleObj)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtDocumentNo.Text = "" Then
                ShowErrorMessage("Document No is required")
                txtDocumentNo.Focus()
                Return False
            End If
            If Me.cmbFromCostCenter.Value < 1 Then
                ShowErrorMessage("Logical Cost Center is required")
                Me.cmbFromCostCenter.Focus()
                Return False
            End If
            If Me.grdDetail.RowCount = 0 Then
                ShowErrorMessage("Grid is empty")
                Me.grdDetail.Focus()
                Return False
            End If
            Dim dt As DataTable = Me.grdDetail.DataSource
            Dim FilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("IsDeleted"), Janus.Windows.GridEX.ConditionOperator.Equal, 0)
            Dim AmountPercentage As Double = Math.Round(Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("AmountPercentage"), Janus.Windows.GridEX.AggregateFunction.Sum, FilterCondition), DecimalPointInValue)
            If AmountPercentage < 100 Then
                ShowErrorMessage("Sum of amount should be 100%")
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
            Me.txtDocumentNo.Text = GetBifurcationNo()
            Me.dtpDocumentDate.Value = Now
            Me.dtpStartDate.Value = Now
            Me.txtRemarks.Text = String.Empty
            FillCombos()
            Me.cmbFromCostCenter.Rows(0).Activate()
            ResetDetail()
            GetDetail(-1)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ResetDetail()
        Try
            Me.cmbToCostCenter.Rows(0).Activate()
            Me.txtAmount.Text = String.Empty
            Me.txtComments.Text = String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            LogicalB.ActivityLog.ActivityName = "Save"
            LogicalB.ActivityLog.ApplicationName = "Accounts"
            LogicalB.ActivityLog.RefNo = LogicalB.DocumentNo
            LogicalB.ActivityLog.FormCaption = "Logical Bifurcation"
            LogicalB.ActivityLog.FormName = Me.Name
            LogicalB.ActivityLog.LogDateTime = Now
            LogicalB.ActivityLog.User_Name = LoginUserName
            LogicalB.ActivityLog.UserID = LoginUserId
            LogicalB.ActivityLog.Source = Me.Name
            If LogicalBDAL.Add(LogicalB) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception

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
            LogicalB.ActivityLog.ActivityName = "Update"
            LogicalB.ActivityLog.ApplicationName = "Accounts"
            LogicalB.ActivityLog.RefNo = LogicalB.DocumentNo
            LogicalB.ActivityLog.FormCaption = "Logical Bifurcation"
            LogicalB.ActivityLog.FormName = Me.Name
            LogicalB.ActivityLog.LogDateTime = Now
            LogicalB.ActivityLog.User_Name = LoginUserName
            LogicalB.ActivityLog.UserID = LoginUserId
            LogicalB.ActivityLog.Source = Me.Name
            If LogicalBDAL.Update(LogicalB) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub AddToGrid()
        Try
            If Me.cmbToCostCenter.Value < 1 Then
                ShowErrorMessage("Cost Center is required.")
                Exit Sub
            End If
            ''TASK TFS3670 removed the condition of Val(Me.txtAmount.Text) < 1 and replaced it with Val(Me.txtAmount.Text) = 0 Or Val(Me.txtAmount.Text) < 0. Dated 25-06-2018
            If Val(Me.txtAmount.Text) = 0 Or Val(Me.txtAmount.Text) < 0 Then
                ShowErrorMessage("Amount is required.")
                Exit Sub
            End If
            Dim dt As DataTable = CType(Me.grdDetail.DataSource, DataTable)
            Dim Rows() As DataRow = dt.Select("ToCostCenterId = " & Me.cmbToCostCenter.Value & "")
            If Rows.Length > 0 Then
                ShowErrorMessage("Cost Center has already been added.")
                Exit Sub
            End If
            Dim FilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition(Me.grdDetail.RootTable.Columns("IsDeleted"), Janus.Windows.GridEX.ConditionOperator.Equal, 0)
            Dim AmountPercentage As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("AmountPercentage"), Janus.Windows.GridEX.AggregateFunction.Sum, FilterCondition)
            AmountPercentage += Val(Me.txtAmount.Text)
            If AmountPercentage > 100 Then
                ShowErrorMessage("Total amount should not exceed 100")
                Exit Sub
            End If
            Dim Row As DataRow = dt.NewRow
            Row(Detail.LogicalBifurcationDetailId) = 0
            Row(Detail.LogicalBifurcationId) = 0
            Row(Detail.ToCostCenterId) = Me.cmbToCostCenter.Value
            Row(Detail.ToCostCenter) = Me.cmbToCostCenter.Text
            Row(Detail.AmountPercentage) = Val(Me.txtAmount.Text)
            Row(Detail.Comments) = Me.txtComments.Text
            Row(Detail.IsDeleted) = 0

            dt.Rows.Add(Row)
            ResetDetail()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditRecord(ByVal Obj As LogicalBifurcationBE)
        Try
            IsEditMode = True
            LogicalBifurcationId = Obj.LogicalBifurcationId
            Me.txtDocumentNo.Text = Obj.DocumentNo
            Me.dtpDocumentDate.Value = Obj.DocumentDate
            Me.cmbFromCostCenter.Value = Obj.FromCostCenterId
            Me.dtpStartDate.Value = Obj.StartDate
            Me.txtRemarks.Text = Obj.Remarks
            GetDetail(Obj.LogicalBifurcationId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetDetail(ByVal LogicalBifurcationId As Integer)
        Try
            Me.grdDetail.DataSource = LogicalBDetailDAL.GetDetail(LogicalBifurcationId)
            Me.grdDetail.RootTable.Columns("AmountPercentage").FormatString = "N" & DecimalPointInValue
            Me.grdDetail.RootTable.Columns("AmountPercentage").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns("AmountPercentage").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdDetail.RootTable.Columns("AmountPercentage").TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() Then
                If IsEditMode = False Then
                    If Save() Then
                        msg_Information("Record has been saved successfully.")
                        'ReSetControls()
                        Me.DialogResult = Windows.Forms.DialogResult.Yes
                        Me.Close()
                    Else
                        ShowErrorMessage("Record has been failed to save")
                    End If
                Else
                    If msg_Confirm("Do you want to update the record?") = False Then Exit Sub
                    If Update1() Then
                        msg_Information("Record has been updated successfully.")
                        'ReSetControls()
                        Me.DialogResult = Windows.Forms.DialogResult.Yes
                        Me.Close()
                    Else
                        ShowErrorMessage("Record has been failed to update.")
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetail_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.ColumnButtonClick
        Try

            If e.Column.Key = "Delete" Then
                'If IsEditMode = True Then
                '    ShowErrorMessage("You can not delete saved row.")
                '    Exit Sub
                'End If
                If msg_Confirm("Do you want to delete record?") = False Then Exit Sub
                'Dim AmountPercentage As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("AmountPercentage"), Janus.Windows.GridEX.AggregateFunction.Sum)

                'Dim Obj As New LogicalBifurcationDetailBE
                'Obj.LogicalBifurcationDetailId = Val(Me.grdDetail.GetRow.Cells("LogicalBifurcationDetailId").Value.ToString)
                'Call New LogicalBifurcationDetailDAL().Delete(Obj)
                Me.grdDetail.GetRow.BeginEdit()
                Me.grdDetail.GetRow.Cells(Detail.IsDeleted).Value = 1
                Me.grdDetail.GetRow.EndEdit()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmLogicalBifurcation_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.DialogResult = Windows.Forms.DialogResult.Yes Then

        ElseIf e.CloseReason = CloseReason.UserClosing Then

        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Yes
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            AddToGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class