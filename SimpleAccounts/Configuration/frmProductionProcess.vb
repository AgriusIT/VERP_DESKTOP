Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDAL

Public Class frmProductionProcess
    Implements IGeneral
    Dim ProductionProcess As BEProductionProcess
    Dim ProductionProcesId As Integer = 0I
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            grdDetail.RootTable.Columns("ProductionProcessDetailId").Visible = False
            Me.grdDetail.RootTable.Columns("ProductionStepId").Visible = False
            Me.grdDetail.RootTable.Columns("State").Visible = False
            Me.grdDetail.RootTable.Columns("ProductionStep").Caption = "Production Step"
            Me.grdDetail.RootTable.Columns("SortOrder").Caption = "Sort Order"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New ProductionProcessDAL().Delete(ProductionProcesId) Then
                SaveActivityLog("Production", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, "", True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            FillDropDown(cmbProductionStep, "SELECT ProdStep_Id AS ProductionStepId, tblproSteps.prod_step AS [Production Step] FROM tblproSteps")
            FillDropDown(Me.cmbWIPAccount, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code] from vwCOADetail where main_type in ('Assets','Expense', 'Liability')  and detail_title <> ''")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Me.grdDetail.UpdateData()
            ProductionProcess = New BEProductionProcess
            ProductionProcess.ProductionProcessId = ProductionProcesId
            ProductionProcess.ProcessName = Me.txtProcessName.Text.Replace("'", "''")
            ProductionProcess.Remarks = Me.txtRemarks.Text.Replace("'", "''")
            ProductionProcess.WIPAccountId = Me.cmbWIPAccount.SelectedValue
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                Dim Detail As New BEProductionProcessDetail()
                Detail.ProductionProcessDetailId = Row.Cells("ProductionProcessDetailId").Value
                Detail.ProductionProcessId = Row.Cells("ProductionProcessId").Value
                Detail.ProductionStepId = Row.Cells("ProductionStepId").Value
                Detail.SortOrder = Val(Row.Cells("SortOrder").Value.ToString)
                Detail.State = Row.Cells("State").Value.ToString
                ProductionProcess.Detail.Add(Detail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As New DataTable
            dt = New ProductionProcessDAL().GetAll()
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("ProductionProcessId").Visible = False
            Me.grdSaved.RootTable.Columns("WIPAccountId").Visible = False
            Me.grdSaved.RootTable.Columns("ProcessName").Caption = "Process Name"
            Me.grdSaved.RootTable.Columns("WIPAccount").Caption = "WIP Account"
            Me.grdSaved.RootTable.Columns("ProcessName").Width = 200
            Me.grdSaved.RootTable.Columns("Remarks").Width = 300
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtProcessName.Text = String.Empty Then
                ShowErrorMessage("Please enter process name")
                Me.txtProcessName.Focus()
                Return False
            End If
            If Me.grdDetail.RowCount < 1 Then
                ShowErrorMessage("No row is found in the Grid")
                Me.cmbProductionStep.Focus()
                Return False
            End If

            'If Me.cmbWIPAccount.SelectedValue < 1 Then
            '    ShowErrorMessage("Please select WIP Account")
            '    Me.cmbWIPAccount.Focus()
            '    Return False
            'End If

            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            GetSecurityRights()
            ProductionProcesId = 0
            Me.btnSave.Text = "&Save"
            Me.txtProcessName.Text = String.Empty
            Me.txtRemarks.Text = ""
            Me.txtSortOrder.Text = 1
            Me.txtProcessName.Enabled = True
            If Not cmbWIPAccount.SelectedIndex = -1 Then
                Me.cmbWIPAccount.SelectedIndex = 0
            End If
            If Not cmbProductionStep.SelectedIndex = -1 Then
                Me.cmbProductionStep.SelectedIndex = 0
            End If
            GetAllRecords()
            DispalyDetail(-1)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New ProductionProcessDAL().Save(ProductionProcess) Then
                SaveActivityLog("Production", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, "", True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If New ProductionProcessDAL().Update(ProductionProcess) Then
                SaveActivityLog("Production", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, "", True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmProductionProcess_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub frmProductionProcess_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            FillCombos()
            Me.txtProcessName.Focus()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            If Me.grdDetail.RowCount = 0 Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            'msg_Information(str_informDelete)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If ProductionProcessDAL.ValidateProcessName(Me.txtProcessName.Text) = True Then
                        ShowErrorMessage("Process name already exists.")
                        Me.txtProcessName.Focus()
                        Exit Sub
                    End If
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information("Please record has been saved successfully.")
                    ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information("Please record has been updated successfully.")
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdDetail.DoubleClick
        Try
            If Me.grdDetail.RowCount = 0 Then Exit Sub
            ProductionProcesId = Me.grdDetail.GetRow.Cells("ProductionProcesId").Value
            Me.txtProcessName.Text = Me.grdDetail.GetRow.Cells("ProcessName").Value.ToString
            Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells("Remarks").Value.ToString
            Me.btnSave.Text = "&Update"
            Me.txtProcessName.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            grdSaved_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdDetail.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If

    End Sub

    Private Sub AddToGrid()
        Try
            Dim dt As DataTable = CType(Me.grdDetail.DataSource, DataTable)
            Dim dr() As DataRow = dt.Select("ProductionStepId=" & Me.cmbProductionStep.SelectedValue & "")
            If dr.Length > 0 Then
                ShowErrorMessage("Selected production step already exists.")
                Me.cmbProductionStep.Focus()
                Exit Sub
            End If
            Dim Row As DataRow
            Row = dt.NewRow
            Row.Item("ProductionProcessDetailId") = 0
            Row.Item("ProductionProcessId") = 0
            Row.Item("State") = "New"
            Row.Item("ProductionStepId") = Me.cmbProductionStep.SelectedValue
            Row.Item("ProductionStep") = CType(Me.cmbProductionStep.SelectedItem, DataRowView).Item("Production Step").ToString
            Row.Item("SortOrder") = Val(Me.txtSortOrder.Text)
            dt.Rows.Add(Row)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Me.cmbProductionStep.SelectedIndex < 1 Then
                ShowErrorMessage("Please select production step.")
                cmbProductionStep.Focus()
                Exit Sub
            End If
            AddToGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True

                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Me.btnSave.Enabled = False
                    Exit Sub
                End If
            Else
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        Me.btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        Me.btnEdit.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                        ''End TASK TFS1384
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DispalyDetail(ByVal ProductionProcessId As Integer)
        Try
            Me.grdDetail.DataSource = New ProductionProcessDAL().GetAllDetail(ProductionProcessId)
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdSaved_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdSaved.RowDoubleClick
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            ProductionProcesId = Me.grdSaved.GetRow.Cells("ProductionProcessId").Value
            Me.txtProcessName.Text = Me.grdSaved.GetRow.Cells("ProcessName").Value.ToString
            Me.txtProcessName.Enabled = False
            Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells("Remarks").Value.ToString
            Me.cmbWIPAccount.SelectedValue = Val(Me.grdSaved.GetRow.Cells("WIPAccountId").Value.ToString)
            Me.btnSave.Text = "&Update"
            Me.txtProcessName.Focus()
            DispalyDetail(ProductionProcesId)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetail_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.ColumnButtonClick
        Try
            If grdDetail.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "Delete" Then
                    If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                    If Not Me.grdDetail.GetRow.Cells("State").Value = "Delete" Then
                        Me.grdDetail.GetRow.BeginEdit()
                        Me.grdDetail.GetRow.Cells("State").Value = "Delete"
                        Me.grdDetail.GetRow.Cells("State").Column.ButtonText = "Deleted"
                        Me.grdDetail.GetRow.EndEdit()
                    Else
                        ShowErrorMessage("This row has already been deleted.")
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdDetail.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        FillCombos()

    End Sub
End Class