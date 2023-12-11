Imports SBDal
Imports SBModel
Imports System.Data
Imports System.Data.SqlClient

Public Class frmBudgetDefinition
    Implements IGeneral
    Dim Id As Integer = 0I
    Public Shared BudgetId As Integer
    Dim Budget As DefBudgetBE
    Public BudgetDAL As DefBudgetDAL = New DefBudgetDAL()
    Dim PropertyTypeId As Integer
    Dim CostCenterList As List(Of BudgetCostCenterBE)
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False

    Private Sub frmProPurchase_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.KeyCode = Keys.Escape) Then
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If BudgetId = -1 Then
                    If BudgetDAL.Add(Budget) Then
                        If BudgetDAL.AddPurchaseType(CostCenterList) = True Then
                            msg_Information("Record has been saved successfully.")
                            ReSetControls()
                            Me.Close()
                            'SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtPOSTitle.Text, True)
                        End If
                    End If
                Else
                    If BudgetDAL.Update(Budget) Then
                        If BudgetDAL.UpdatePurchaseType(CostCenterList) = True Then
                            msg_Information("Record has been updated successfully.")
                            ReSetControls()
                            Me.Close()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProPurchase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombos()
            Dim dt As DataTable
            dt = New DefBudgetDAL().GetById(BudgetId)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    Me.txtName.Text = dt.Rows(0).Item("BudgetName").ToString
                    Me.txtCode.Text = dt.Rows(0).Item("BudgetCode").ToString
                    Me.dtpFromDate.Value = dt.Rows(0).Item("FromDate")
                    dtpToDate.Value = dt.Rows(0).Item("ToDate")
                    txtRemarks.Text = dt.Rows(0).Item("Remarks").ToString
                    cmbReport.SelectedValue = dt.Rows(0).Item("ReportId")
                    txtSortOrder.Text = dt.Rows(0).Item("SortOrder")
                    chkActive.Checked = dt.Rows(0).Item("Active")
                    btnSave.Enabled = DoHaveUpdateRights
                Next
            Else
                ReSetControls()
            End If

            BudgetCostCenter(BudgetId)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Function BudgetCostCenter(ByVal BudgetId As Integer) As Boolean
        Try
            Dim str As String
            Dim dt As DataTable
            str = "SELECT tblBudgetCostCenter.RecordId, tblBudgetCostCenter.BudgetId, tblBudgetCostCenter.CostCenterId, tblDefCostCenter.Name FROM tblBudgetCostCenter LEFT OUTER JOIN tblDefCostCenter ON tblBudgetCostCenter.CostCenterId = tblDefCostCenter.CostCenterID where BudgetId = " & BudgetId & ""
            dt = GetDataTable(str)
            grdPayment.DataSource = dt
            grdPayment.RetrieveStructure()
            Me.grdPayment.RootTable.Columns("RecordId").Visible = False
                Me.grdPayment.RootTable.Columns("BudgetId").Visible = False
                Me.grdPayment.RootTable.Columns("CostCenterId").Visible = False

                '// Adding Delete Button in the grid
                If Me.grdPayment.RootTable.Columns.Contains("Delete") = False Then
                    Me.grdPayment.RootTable.Columns.Add("Delete")
                    Me.grdPayment.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdPayment.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdPayment.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdPayment.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                    Me.grdPayment.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grdPayment.RootTable.Columns("Delete").Key = "Delete"
                    Me.grdPayment.RootTable.Columns("Delete").Caption = "Action"
                End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ReSetControls()
        Me.Close()
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim str As String
        Try
            str = "SELECT CostCenterID, Name, CostCenterGroup, ISNULL(SortOrder,0) AS SortOrder FROM tblDefCostCenter WHERE (Active = 1) ORDER BY CostCenterID ASC"
            FillDropDown(cmbCostCenter, str, True)
            str = "SELECT ReportTemplateId, Title, Remarks, Type FROM ReportTemplate Where Active = 1 ORDER BY SortOrder"
            FillDropDown(cmbReport, str, True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Budget = New DefBudgetBE
            Budget.BudgetId = BudgetId
            Budget.BudgetName = Me.txtName.Text
            Budget.BudgetCode = Me.txtCode.Text
            Budget.FromDate = Me.dtpFromDate.Value
            Budget.ToDate = dtpToDate.Value
            Budget.Remarks = txtRemarks.Text
            Budget.ReportId = cmbReport.SelectedValue
            Budget.SortOrder = txtSortOrder.Text
            Budget.Active = chkActive.Checked
            CostCenterList = New List(Of BudgetCostCenterBE)
            For i As Integer = 0 To grdPayment.RowCount - 1
                Dim CDetail As New BudgetCostCenterBE
                CDetail.RecordId = grdPayment.GetRows(i).Cells("RecordId").Value
                CDetail.CostCenterId = grdPayment.GetRows(i).Cells("CostCenterId").Value.ToString
                CDetail.BudgetId = grdPayment.GetRows(i).Cells("BudgetId").Value.ToString
                CostCenterList.Add(CDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.txtName.Text = String.Empty Then
                ShowErrorMessage("Please enter Serial No")
                Me.txtName.Focus()
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
            txtName.Focus()
            BudgetId = -1
            Me.txtName.Text = ""
            Me.txtCode.Text = ""
            Me.dtpFromDate.Value = Date.Now
            Me.dtpToDate.Value = Date.Now
            cmbReport.SelectedIndex = 0
            txtRemarks.Text = ""
            cmbCostCenter.SelectedIndex = 0
            txtSortOrder.Text = 1
            chkActive.Checked = True
            btnSave.Enabled = DoHaveSaveRights
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Validate_AddToGrid() Then
                Dim dt As DataTable
                dt = CType(Me.grdPayment.DataSource, DataTable)
                Dim dr As DataRow
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdPayment.GetDataRows
                    If Val(row.Cells("CostCenterId").Value) = Me.cmbCostCenter.SelectedValue Then
                        msg_Error("Selected Cost Center Exists Already")
                        Exit Sub
                    End If
                Next
                dr = dt.NewRow
                dr("RecordId") = 0
                dr("BudgetId") = 0
                dr("CostCenterId") = Me.cmbCostCenter.SelectedValue
                dr("Name") = Me.cmbCostCenter.Text
                dt.Rows.Add(dr)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function Validate_AddToGrid() As Boolean
        Try
            If cmbCostCenter.SelectedValue = 0 Then
                msg_Error("You must select a Cost Center")
                cmbCostCenter.Focus() : Validate_AddToGrid = False : Exit Function
            End If
            Validate_AddToGrid = True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class