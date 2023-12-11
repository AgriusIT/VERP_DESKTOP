''TASK TFS3767 Done by Amin build a new screen 'Item Wise Discount' on 08-03-2018
Imports SBDal
Imports SBModel
Public Class frmItemWiseDiscount
    Implements IGeneral
    Dim Obj As ItemWiseDiscountMasterBE
    Dim ObjDAL As ItemWiseDiscountMasterDAL
    Dim ID As Integer = 0
    Dim IsEditMode As Boolean = False
    Dim HasDeleteRights As Boolean = False
    Structure Detail
        'ID, ItemWiseDiscountId, Article.ArticleId, Article.ArticleCode AS Code, Article.ArticleDescription AS Article, Article.ArticleGroupName As Department, Article.ArticleTypeName AS [Type], Article.ArticleCompanyName AS Category, Article.ArticleGenderName AS Origin, Article.ArticleUnitName AS Unit, Article.SalePrice
        Public Shared Property ID As String = "ID"
        Public Shared Property ItemWiseDiscountId As String = "ItemWiseDiscountId"
        Public Shared Property ArticleId As String = "ArticleId"
        Public Shared Property Code As String = "Code"
        Public Shared Property Article As String = "Article"
        Public Shared Property Department As String = "Department"
        Public Shared Property Type As String = "Type"
        Public Shared Property Category As String = "Category"
        Public Shared Property Origin As String = "Origin"
        Public Shared Property Unit As String = "Unit"
        Public Shared Property SalePrice As String = "SalePrice"

    End Structure

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim Query As String = String.Empty
        Try
            If Condition = "Vendor" Then
                Query = " SELECT ArticleGenderId as Id, ArticleGenderName as Name from ArticleGenderDefTable WHERE active=1 order by sortOrder"
                FillUltraDropDown(Me.cmbVendor, Query)
                Me.cmbVendor.Rows(0).Activate()
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Name").Width = 300
            ElseIf Condition = "Category" Then
                Query = " SELECT   ArticleCompanyId AS ID, ArticleCompanyName AS Name, CategoryCode " & _
                     " FROM ArticleCompanyDefTable " & _
                     " WHERE Active = 1"
                FillUltraDropDown(Me.cmbCategory, Query)
                Me.cmbCategory.Rows(0).Activate()
                Me.cmbCategory.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                Me.cmbCategory.DisplayLayout.Bands(0).Columns("Name").Width = 200

            ElseIf Condition = "DiscountType" Then
                Query = "SELECT DiscountID, DiscountType FROM tblDiscountType ORDER BY DiscountID"
                'Query = "SELECT 1 AS ID, 'Flat' AS DiscountType Union All SELECT 2 AS ID, 'Percentage' AS DiscountType "
                FillUltraDropDown(Me.cmbDiscountType, Query, False)
                Me.cmbDiscountType.Rows(0).Activate()
                Me.cmbDiscountType.DisplayLayout.Bands(0).Columns("DiscountID").Hidden = True
                Me.cmbDiscountType.DisplayLayout.Bands(0).Columns("DiscountType").Width = 200
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Obj = New ItemWiseDiscountMasterBE()
            Obj.ID = ID
            Obj.FromDate = dtpFromDate.Value
            Obj.ToDate = dtpToDate.Value
            Obj.VendorId = Me.cmbVendor.Value
            Obj.CategoryId = Me.cmbCategory.Value
            Obj.DiscountType = cmbDiscountType.Value
            Obj.Discount = Val(txtDiscount.Text)
            Obj.RepeatingNextYear = IIf(rbYes.Checked, 1, 0)
            For Each _Row As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetCheckedRows
                Dim ObjDetail As New ItemWiseDiscountDetailBE
                ObjDetail.ID = _Row.Cells(Detail.ID).Value
                ObjDetail.ItemWiseDiscountId = _Row.Cells(Detail.ItemWiseDiscountId).Value
                ObjDetail.ArticleId = _Row.Cells(Detail.ArticleId).Value
                ObjDetail.IsDeleted = False
                Obj.Detail.Add(ObjDetail)
            Next
            If IsEditMode = True Then
                For Each _Row As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetUncheckedRows
                    Dim ObjDetail As New ItemWiseDiscountDetailBE
                    ObjDetail.ID = _Row.Cells(Detail.ID).Value
                    ObjDetail.ItemWiseDiscountId = _Row.Cells(Detail.ItemWiseDiscountId).Value
                    ObjDetail.ArticleId = _Row.Cells(Detail.ArticleId).Value
                    ObjDetail.IsDeleted = True
                    Obj.Detail.Add(ObjDetail)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            ObjDAL = New ItemWiseDiscountMasterDAL()
            Me.grdSaved.DataSource = ObjDAL.GetAll()
            Me.grdSaved.RetrieveStructure()
            If Me.grdSaved.RootTable.Columns.Contains("Delete") = False Then
                Me.grdSaved.RootTable.Columns.Add("Delete")
                Me.grdSaved.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdSaved.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdSaved.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grdSaved.RootTable.Columns("Delete").Key = "Delete"
                Me.grdSaved.RootTable.Columns("Delete").Caption = "Action"
                Me.grdSaved.RootTable.Columns("Delete").Width = 70
            End If
            Me.grdSaved.RootTable.Columns("VendorId").Visible = False
            Me.grdSaved.RootTable.Columns("CategoryId").Visible = False
            Me.grdSaved.RootTable.Columns("ID").Visible = False
            Me.grdSaved.RootTable.Columns("DiscountId").Visible = False
            Me.grdSaved.RootTable.Columns("FromDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("ToDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("Discount").FormatString = "N" & DecimalPointInValue

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbDiscountType.Value < 1 Then
                ShowErrorMessage("Please select an account type.")
                Me.cmbDiscountType.Focus()
                Return False
            End If
            If Val(Me.txtDiscount.Text) < 1 Then
                ShowErrorMessage("Discount value is required")
                Me.txtDiscount.Focus()
                Return False
            End If
            If grdDetail.RowCount = 0 Then
                ShowErrorMessage("Grid is empty")
                Me.grdDetail.Focus()
                Return False
            End If
            If grdDetail.GetCheckedRows.Length = 0 Then
                ShowErrorMessage("At least one checked row is required.")
                Me.grdDetail.Focus()
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
            FillCombos("Vendor")
            FillCombos("Category")
            FillCombos("DiscountType")
            ID = 0
            IsEditMode = False
            Me.dtpFromDate.Value = Now
            Me.dtpToDate.Value = Now
            Me.cmbVendor.Rows(0).Activate()
            Me.cmbCategory.Rows(0).Activate()
            Me.txtDiscount.Text = 0
            rbNo.Checked = True
            Me.cmbVendor.Enabled = True
            Me.cmbCategory.Enabled = True
            GetAll(0, 0)
            'DisplayDetail(-1)
            GetAllRecords()
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            ObjDAL = New ItemWiseDiscountMasterDAL()
            Obj.ActivityLog.ActivityName = "Save"
            Obj.ActivityLog.ApplicationName = String.Empty
            Obj.ActivityLog.FormCaption = "Item Wise Discount"
            Obj.ActivityLog.FormName = "frmItemWiseDiscount"
            Obj.ActivityLog.LogDateTime = Now
            Obj.ActivityLog.RecordType = String.Empty
            Obj.ActivityLog.RefNo = String.Empty
            Obj.ActivityLog.Source = "frmItemWiseDiscount"
            Obj.ActivityLog.User_Name = LoginUserName
            Obj.ActivityLog.UserID = LoginUserId
            If ObjDAL.Add(Obj) Then
                msg_Information("Record has been saved successfully.")
                ReSetControls()
            Else
                msg_Information("Record could not save.")
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
            ObjDAL = New ItemWiseDiscountMasterDAL()
            Obj.ActivityLog.ActivityName = "Update"
            Obj.ActivityLog.ApplicationName = String.Empty
            Obj.ActivityLog.FormCaption = "Item Wise Discount"
            Obj.ActivityLog.FormName = "frmItemWiseDiscount"
            Obj.ActivityLog.LogDateTime = Now
            Obj.ActivityLog.RecordType = String.Empty
            Obj.ActivityLog.RefNo = String.Empty
            Obj.ActivityLog.Source = "frmItemWiseDiscount"
            Obj.ActivityLog.User_Name = LoginUserName
            Obj.ActivityLog.UserID = LoginUserId
            If ObjDAL.Update(Obj) Then
                msg_Information("Record has been updated successfully.")
                ReSetControls()
            Else
                msg_Information("Record could not update.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub DisplayDetail(ByVal ID As Integer)
        Try
            Me.grdDetail.DataSource = New ItemWiseDiscountDetailDAL().DisplayDetail(ID)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAll(ByVal VendorId As Integer, ByVal CategoryId As Integer)
        Try
            Me.grdDetail.DataSource = New ItemWiseDiscountDetailDAL().GetAll(VendorId, CategoryId)


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnApplyDiscount_Click(sender As Object, e As EventArgs) Handles btnApplyDiscount.Click
        Try
            If IsValidate() Then
                If IsEditMode = False Then
                    Save()
                Else
                    If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                    Update1()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub EditRecord()
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            RemoveHandler cmbVendor.ValueChanged, AddressOf cmbVendor_ValueChanged
            RemoveHandler cmbCategory.ValueChanged, AddressOf cmbCategory_ValueChanged
            IsEditMode = True
            ID = Me.grdSaved.GetRow.Cells("ID").Value
            Me.dtpFromDate.Value = Me.grdSaved.GetRow.Cells("FromDate").Value
            Me.dtpToDate.Value = Me.grdSaved.GetRow.Cells("ToDate").Value
            Me.cmbVendor.Value = Me.grdSaved.GetRow.Cells("VendorId").Value
            Me.cmbCategory.Value = Me.grdSaved.GetRow.Cells("CategoryId").Value
            Me.cmbDiscountType.Value = Me.grdSaved.GetRow.Cells("DiscountId").Value
            Me.txtDiscount.Text = Me.grdSaved.GetRow.Cells("Discount").Value
            If CBool(Me.grdSaved.GetRow.Cells("RepeatingNextYear").Value) Then
                Me.rbYes.Checked = True
            Else
                Me.rbNo.Checked = True
            End If
            Me.cmbVendor.Enabled = False
            Me.cmbCategory.Enabled = False
            AddHandler cmbVendor.ValueChanged, AddressOf cmbVendor_ValueChanged
            AddHandler cmbCategory.ValueChanged, AddressOf cmbCategory_ValueChanged
            DisplayDetail(ID)
            Me.grdDetail.CheckAllRecords()
            GetSecurityRights()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmItemWiseDiscount_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDiscount_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDiscount.KeyDown
        'Try
        '    NumValidation(sender, e)
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtDiscount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiscount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVendor_ValueChanged(sender As Object, e As EventArgs) Handles cmbVendor.ValueChanged
        Try
            'If Me.cmbVendor.Value > 0 AndAlso IsEditMode = False Then
            GetAll(Me.cmbVendor.Value, Me.cmbCategory.Value)
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCategory_ValueChanged(sender As Object, e As EventArgs) Handles cmbCategory.ValueChanged
        Try
            'If Me.cmbVendor.Value > 0 AndAlso IsEditMode = False Then
            GetAll(Me.cmbVendor.Value, Me.cmbCategory.Value)
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnApplyDiscount.Enabled = True
                HasDeleteRights = True
                CtrlGrdBar2.mGridPrint.Enabled = True
                CtrlGrdBar2.mGridExport.Enabled = True
                Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnApplyDiscount.Enabled = False
                    HasDeleteRights = False
                    CtrlGrdBar2.mGridPrint.Enabled = False
                    CtrlGrdBar2.mGridExport.Enabled = False
                    Me.CtrlGrdBar2.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmStoreIssuence)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If IsEditMode = False Or IsEditMode = False Then
                            Me.btnApplyDiscount.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnApplyDiscount.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString

                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnApplyDiscount.Enabled = False
                HasDeleteRights = False
                CtrlGrdBar2.mGridPrint.Enabled = False
                CtrlGrdBar2.mGridExport.Enabled = False
                Me.CtrlGrdBar2.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If IsEditMode = False Then btnApplyDiscount.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If IsEditMode = True Then btnApplyDiscount.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        HasDeleteRights = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        CtrlGrdBar2.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar2.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVendor_Leave(sender As Object, e As EventArgs) Handles cmbVendor.Leave
        'Try
        '    If Me.cmbVendor.Value > 0 Then
        '        GetAll(Me.cmbVendor.Value, Me.cmbCategory.Value)
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub cmbCategory_Leave(sender As Object, e As EventArgs) Handles cmbCategory.Leave
        'Try
        '    If Me.cmbVendor.Value > 0 Then
        '        GetAll(Me.cmbVendor.Value, Me.cmbCategory.Value)
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If UltraTabControl1.SelectedTab.Index = 0 Then
                Me.CtrlGrdBar2.FormName = Me
                Me.CtrlGrdBar2.MyGrid = Me.grdDetail
            ElseIf UltraTabControl1.SelectedTab.Index = 1 Then
                Me.CtrlGrdBar2.FormName = Me
                Me.CtrlGrdBar2.MyGrid = Me.grdSaved
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.ColumnButtonClick
        If e.Column.Key = "Delete" Then
            If HasDeleteRights = False Then
                ShowErrorMessage("You do not have delete rights.")
                Exit Sub
            End If
            If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
            Obj = New ItemWiseDiscountMasterBE()
            Obj.ID = Me.grdSaved.GetRow.Cells("ID").Value
            Obj.ActivityLog.ActivityName = "Delete"
            Obj.ActivityLog.ApplicationName = String.Empty
            Obj.ActivityLog.FormCaption = "Item Wise Discount"
            Obj.ActivityLog.FormName = "frmItemWiseDiscount"
            Obj.ActivityLog.LogDateTime = Now
            Obj.ActivityLog.RecordType = String.Empty
            Obj.ActivityLog.RefNo = String.Empty
            Obj.ActivityLog.Source = "frmItemWiseDiscount"
            Obj.ActivityLog.User_Name = LoginUserName
            Obj.ActivityLog.UserID = LoginUserId
            ObjDAL = New ItemWiseDiscountMasterDAL()
            If ObjDAL.Delete(Obj) Then
                msg_Information("Record has been deleted successfully.")
                Me.grdSaved.GetRow.Delete()
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
                ReSetControls()
            Else
                msg_Information("Record could not delete.")
            End If
        End If
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Item Wise Discount"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class