''TFS2639 : Ayesha Rehman : Asset account filter only those accounts that have main type of assets
Imports SBDal
Imports SBModel

Public Class frmFixedAssetCategory
    Implements IGeneral

    Dim FixedAssetCategoryId As Integer = 0
    Dim Obj As AssetCategoryBE
    Dim IsEditMode As Boolean = True


    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim _Query As String = String.Empty
        Try
            If Condition = "AssetsAccount" Then
                _Query = "SELECT coa_detail_id AS AccountId, detail_title AS Account FROM vwCOADetail where main_type = 'Assets'" ''TFS2639
                FillDropDown(Me.cmbAssetAccount, _Query)
            ElseIf Condition = "ExpenseAccount" Then
                _Query = "SELECT coa_detail_id AS AccountId, detail_title AS Account FROM vwCOADetail"
                FillDropDown(Me.cmbExpenseAccount, _Query)
            ElseIf Condition = "AccumulativeAccount" Then
                _Query = "SELECT coa_detail_id AS AccountId, detail_title AS Account FROM vwCOADetail"
                FillDropDown(Me.cmbAccumulativeAccount, _Query)
            ElseIf Condition = "DepreciationMethod" Then
            ElseIf Condition = "Frequency" Then


            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Obj = New AssetCategoryBE()
            Obj.Asset_Category_Id = FixedAssetCategoryId
            Obj.Code = Me.txtCode.Text
            Obj.Asset_Category_Name = Me.txtTitle.Text
            Obj.Asset_Category_Description = Me.txtRemarks.Text
            Obj.AssetAccount_coa_detail_id = Me.cmbAssetAccount.SelectedValue
            Obj.DepreciationMethod = Me.cmbDepreciationMethod.Text
            Obj.ExpenseAccount_coa_detail_id = Me.cmbExpenseAccount.SelectedValue
            Obj.Frequency = Me.cmbFrequency.Text
            Obj.AccumulativeAccount_coa_detail_id = Me.cmbAccumulativeAccount.SelectedValue
            Obj.AccumulativeAccount_coa_detail_id = Me.cmbAccumulativeAccount.SelectedValue
            Obj.Sort_Order = Val(Me.txtSortOrder.Text)
            Obj.Remarks = Me.txtRemarks.Text
            Obj.Rate = Val(Me.txtRate.Text)
            Obj.Active = Me.cbActive.Checked
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            'strSQL = " Select Category.Asset_Category_Id AS AssetCategoryId, Category.Asset_Category_Name, Category.Asset_Category_Description, Category.Sort_Order, Category.Active, Category.Code, Category.AssetAccount_coa_detail_id AS AssetAccountId, AssetAccount.detail_title AS AssetAccount, Category.ExpenseAccount_coa_detail_id AS ExpenseAccountId, ExpenseAccount.detail_title AS ExpenseAccount, Category.AccumulativeAccount_coa_detail_id AS AccumulativeAccountId, AccumulativeAccount.detail_title AS AccumulativeAccount, Category.DepreciationMethod, Category.Frequency, Category.Rate, Category.Remarks " _
            '          & " from tblAssetCategory AS Category LEFT OUTER JOIN vwCOADetail AS AssetAccount ON Category.AssetAccount_coa_detail_id = AssetAccount.coa_detail_id LEFT OUTER JOIN vwCOADetail AS ExpenseAccount ON Category.ExpenseAccount_coa_detail_id = ExpenseAccount.coa_detail_id " _
            '          & " LEFT OUTER JOIN vwCOADetail AS AccumulativeAccount ON Category.AccumulativeAccount_coa_detail_id = AccumulativeAccount.coa_detail_id"
            Me.grdSaved.DataSource = FixedAssetCategoryDAL.GetAll()
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns(GridSaved._AssetCategoryId).Visible = False
            Me.grdSaved.RootTable.Columns(GridSaved._Title).Caption = "Title"
            Me.grdSaved.RootTable.Columns(GridSaved._AssetCategoryDescription).Visible = False
            Me.grdSaved.RootTable.Columns(GridSaved._SortOrder).Caption = "Sort Order"
            Me.grdSaved.RootTable.Columns(GridSaved._AssetAccountId).Visible = False
            Me.grdSaved.RootTable.Columns(GridSaved._ExpenseAccountId).Visible = False
            Me.grdSaved.RootTable.Columns(GridSaved._AccumulativeAccountId).Visible = False
            Me.grdSaved.RootTable.Columns(GridSaved._DepreciationMethod).Caption = "Depreciation Method"
            Me.grdSaved.RootTable.Columns(GridSaved._AssetAccount).Caption = "Asset Account"
            Me.grdSaved.RootTable.Columns(GridSaved._ExpenseAccount).Caption = "Expense Account"
            Me.grdSaved.RootTable.Columns(GridSaved._Active).Width = 50
            Me.grdSaved.RootTable.Columns(GridSaved._Active).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(GridSaved._Active).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(GridSaved._Rate).FormatString = "N" & DecimalPointInValue
            'Me.grdSaved.RootTable.Columns(GridSaved._Active). = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtTitle.Text = String.Empty Then
                ShowErrorMessage("Title is required")
                Me.txtTitle.Focus()
                Return False
            End If
            If Val(Me.txtRate.Text) < Val(0) Then
                ShowErrorMessage("Rate should be greator than 0 ")
                Me.txtRate.Focus()
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
            IsEditMode = False
            Me.btnSave.Text = "&Save"
            FixedAssetCategoryId = 0
            Me.txtTitle.Text = String.Empty
            Me.txtCode.Text = String.Empty
            Me.txtRate.Text = String.Empty
            Me.txtRemarks.Text = String.Empty
            Me.txtSortOrder.Text = 1
            Me.cbActive.Checked = True
            If Not cmbAccumulativeAccount.SelectedIndex = -1 Then
                cmbAccumulativeAccount.SelectedIndex = 0
            End If
            If Not cmbExpenseAccount.SelectedIndex = -1 Then
                cmbExpenseAccount.SelectedIndex = 0
            End If
            If Not cmbAssetAccount.SelectedIndex = -1 Then
                cmbAssetAccount.SelectedIndex = 0
            End If
            'If Not cmbFrequency.SelectedIndex = -1 Then
            cmbFrequency.Text = "Monthly"
            'End If
            'If Not cmbDepreciationMethod.SelectedIndex = -1 Then
            cmbDepreciationMethod.Text = "Straight"
            'End If
            GetAllRecords()
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try

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

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmFixedAssetCategory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillCombos("AssetsAccount")
            FillCombos("ExpenseAccount")
            FillCombos("AccumulativeAccount")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = False Then Exit Sub
            Obj.ActivityLog = New ActivityLog()

            If IsEditMode = False Then
                Obj.ActivityLog.ActivityName = "Save"
                Obj.ActivityLog.ApplicationName = "Accounts"
                Obj.ActivityLog.FormCaption = Me.Text
                Obj.ActivityLog.FormName = Me.Name
                Obj.ActivityLog.LogDateTime = Now
                Obj.ActivityLog.RefNo = ""
                Obj.ActivityLog.User_Name = LoginUserName
                Obj.ActivityLog.UserID = LoginUserId
                Obj.ActivityLog.LogComments = ""
                If FixedAssetCategoryDAL.Add(Obj) Then
                    msg_Information("Record has been saved successfully.")
                    ReSetControls()
                End If
            Else
                If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                Obj.ActivityLog.ActivityName = "Update"
                Obj.ActivityLog.ApplicationName = "Accounts"
                Obj.ActivityLog.FormCaption = Me.Text
                Obj.ActivityLog.FormName = Me.Name
                Obj.ActivityLog.LogDateTime = Now
                Obj.ActivityLog.RefNo = ""
                Obj.ActivityLog.User_Name = LoginUserName
                Obj.ActivityLog.UserID = LoginUserId
                Obj.ActivityLog.LogComments = ""
                If FixedAssetCategoryDAL.Update(Obj) Then
                    msg_Information("Record has been updated successfully.")
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub EditRecord()
        'strSQL = " Select Category.Asset_Category_Id AS AssetCategoryId, Category.Asset_Category_Name, Category.Asset_Category_Description, Category.Sort_Order, Category.Active, Category.Code, Category.AssetAccount_coa_detail_id AS AssetAccountId, AssetAccount.detail_title AS AssetAccount, Category.ExpenseAccount_coa_detail_id AS ExpenseAccountId, ExpenseAccount.detail_title AS ExpenseAccount, Category.AccumulativeAccount_coa_detail_id AS AccumulativeAccountId, AccumulativeAccount.detail_title AS AccumulativeAccount, Category.DepreciationMethod, Category.Frequency, Category.Rate, Category.Remarks " _
        '          & " from tblAssetCategory AS Category LEFT OUTER JOIN vwCOADetail AS AssetAccount ON Category.AssetAccount_coa_detail_id = AssetAccount.coa_detail_id LEFT OUTER JOIN vwCOADetail AS ExpenseAccount ON Category.ExpenseAccount_coa_detail_id = ExpenseAccount.coa_detail_id " _
        '          & " LEFT OUTER JOIN vwCOADetail AS AccumulativeAccount ON Category.AccumulativeAccount_coa_detail_id = AccumulativeAccount.coa_detail_id"
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            IsEditMode = True
            Me.btnSave.Text = "&Update"

            FixedAssetCategoryId = Me.grdSaved.CurrentRow.Cells("AssetCategoryId").Value
            Me.txtCode.Text = Me.grdSaved.CurrentRow.Cells("Code").Value.ToString
            Me.txtTitle.Text = Me.grdSaved.CurrentRow.Cells("Asset_Category_Name").Value.ToString
            Me.txtRemarks.Text = Me.grdSaved.CurrentRow.Cells("Asset_Category_Description").Value.ToString
            Me.txtSortOrder.Text = Val(Me.grdSaved.CurrentRow.Cells("Sort_Order").Value.ToString)
            Me.cbActive.Checked = Me.grdSaved.CurrentRow.Cells("Active").Value
            'Me.txtCode.Text = Me.grdSaved.CurrentRow.Cells("Code").Value.ToString
            Me.txtRemarks.Text = Me.grdSaved.CurrentRow.Cells("Asset_Category_Description").Value.ToString
            Me.cmbAssetAccount.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("AssetAccountId").Value.ToString)
            Me.cmbExpenseAccount.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("ExpenseAccountId").Value.ToString)
            Me.cmbAccumulativeAccount.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("AccumulativeAccountId").Value.ToString)
            Me.cmbFrequency.Text = Me.grdSaved.CurrentRow.Cells("Frequency").Value.ToString
            Me.cmbDepreciationMethod.Text = Me.grdSaved.CurrentRow.Cells("DepreciationMethod").Value.ToString
            Me.txtRate.Text = Val(Me.grdSaved.CurrentRow.Cells(GridSaved._Rate).Value.ToString)
            GetSecurityRights()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
            Obj = New AssetCategoryBE()
            Obj.Asset_Category_Id = Val(Me.grdSaved.CurrentRow.Cells(GridSaved._AssetCategoryId).Value.ToString)
            Obj.ActivityLog = New ActivityLog()
            Obj.ActivityLog.ActivityName = "Delete"
            Obj.ActivityLog.ApplicationName = "Accounts"
            Obj.ActivityLog.FormCaption = Me.Text
            Obj.ActivityLog.FormName = Me.Name
            Obj.ActivityLog.LogDateTime = Now
            Obj.ActivityLog.RefNo = ""
            Obj.ActivityLog.User_Name = LoginUserName
            Obj.ActivityLog.UserID = LoginUserId
            Obj.ActivityLog.LogComments = ""
            If FixedAssetCategoryDAL.Delete(Obj) Then
                msg_Information("Record has been deleted successfully.")
                ReSetControls()
            End If
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
                    Me.btnDelete.Enabled = False
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
                        If Me.btnSave.Text = "&Save" Then Me.btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then Me.btnSave.Enabled = True
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

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 0 Then
                Me.btnSave.Visible = True
                Me.btnRefresh.Visible = True
                Me.btnNew.Visible = True
                Me.CtrlGrdBar1.Visible = False
            ElseIf e.Tab.Index = 1 Then
                Me.btnSave.Visible = False
                Me.btnRefresh.Visible = False
                Me.btnNew.Visible = False
                Me.CtrlGrdBar1.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim Id As Integer = 0
        Try
            Id = Me.cmbAssetAccount.SelectedValue
            FillCombos("AssetsAccount")
            Me.cmbAssetAccount.SelectedValue = Id
            Id = Me.cmbExpenseAccount.SelectedValue
            FillCombos("ExpenseAccount")
            Me.cmbExpenseAccount.SelectedValue = Id
            Id = Me.cmbAccumulativeAccount.SelectedValue
            FillCombos("AccumulativeAccount")
            Me.cmbAccumulativeAccount.SelectedValue = Id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub grdSaved_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdSaved.RowDoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRate_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRate.KeyDown
        'Try
        '    NumValidation(sender, )
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRate.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class

Structure GridSaved
    'strSQL = " Select     Category.Remarks " _
    '          & " from tblAssetCategory AS Category LEFT OUTER JOIN vwCOADetail AS AssetAccount ON Category.AssetAccount_coa_detail_id = AssetAccount.coa_detail_id LEFT OUTER JOIN vwCOADetail AS ExpenseAccount ON Category.ExpenseAccount_coa_detail_id = ExpenseAccount.coa_detail_id " _
    '          & " LEFT OUTER JOIN vwCOADetail AS AccumulativeAccount ON Category.AccumulativeAccount_coa_detail_id = AccumulativeAccount.coa_detail_id"
    Public Shared _AssetCategoryId As String = "AssetCategoryId"
    Public Shared _Title As String = "Asset_Category_Name"
    Public Shared _AssetCategoryDescription As String = "Asset_Category_Description"
    Public Shared _SortOrder As String = "Sort_Order"
    Public Shared _Active As String = "Active"
    Public Shared _Code As String = "Code"
    Public Shared _AssetAccountId As String = "AssetAccountId"
    Public Shared _AssetAccount As String = "AssetAccount"
    Public Shared _ExpenseAccountId As String = "ExpenseAccountId"
    Public Shared _ExpenseAccount As String = "ExpenseAccount"
    Public Shared _AccumulativeAccountId As String = "AccumulativeAccountId"
    Public Shared _AccumulativeAccount As String = "AccumulativeAccount"
    Public Shared _DepreciationMethod As String = "DepreciationMethod"
    Public Shared _Frequency As String = "Frequency"
    Public Shared _Rate As String = "Rate"
    Public Shared _Remarks As String = "Remarks"
End Structure