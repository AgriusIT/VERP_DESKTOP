Imports SBModel
Imports SBDal
Imports System.Data.SqlClient

Public Class frmAsset
    Implements IGeneral

    Dim asset As AssetBE
    Dim aid As Integer
    'For Image
    Dim strPath As String = String.Empty
    Dim strImagePath As String = String.Empty
    Dim a As New OpenFileDialog

    Private Sub frmAsset_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillDropDown(cmbcategory, "Select Asset_Category_Id, Asset_Category_Name from tblAssetCategory")
            FillDropDown(cmbtype, "Select Asset_Type_Id, Asset_Type_Name from tblAssetType WHERE Asset_Category_Id=" & Me.cmbcategory.SelectedValue & "")
            FillDropDown(cmbLocation, "Select Asset_Location_id,Asset_Location_Name from tblAssetLocation")
            FillDropDown(cmbCondition, "Select Asset_Condition_id,Asset_Condition_Name from tblAssetCondition")
            FillDropDown(cmbStatus, "Select Asset_Status_id, Asset_Status_Name from tblAssetStatus")
            FillDropDown(cmbVendor, "Select coa_detail_id, Detail_Title from dbo.vwCOADetail where Account_Type = 'Vendor' ")
            FillDropDown(cmbEmployee, "Select Employee_Id,Employee_Name from tblDefEmployee Where Active = 1") ''TASKTFS75 added and set active = 1
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings


    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDetailAccount)
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
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New AssetsDAL().Delete(asset) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            asset = New AssetBE
            asset.Asset_Id = aid
            asset.Active = True
            asset.Asset_Brand = txtBrand.Text
            asset.Asset_Category_Id = Me.cmbcategory.SelectedValue
            asset.Asset_Condition_Id = Me.cmbCondition.SelectedValue
            asset.Asset_Description = Me.txtDescription.Text
            asset.Asset_Detail = Me.txtDescription.Text
            asset.Asset_Location = Me.cmbLocation.SelectedValue
            asset.Asset_Manufacturer = Me.txtManufacturer.Text
            asset.Asset_Model = Me.txtModel.Text
            asset.Asset_Name = Me.txtAssetName.Text
            asset.Asset_Number = Me.txtNumber.Text
            asset.Asset_Picture = strImagePath   ' Asset Image
            asset.Asset_Serial_No = Me.txtSerialNumber.Text
            asset.Asset_Status_Id = Me.cmbStatus.SelectedValue
            asset.Asset_Type_Id = Me.cmbtype.SelectedValue
            asset.VendorId = Me.cmbVendor.SelectedValue
            asset.PurchaseDate = Me.dtpPurchase.Value
            asset.PurchasePrice = Val(Me.txtPurchasePrice.Text)
            asset.CurrentValue = Val(Me.txtCurrentValue.Text)
            asset.Warranty_Expire_Date = Me.dtpExpire.Value
            asset.EmployeeId = Me.cmbEmployee.SelectedValue

            asset.Sort_Order = 1

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

        Try
            Me.grdAssets.DataSource = New AssetsDAL().GetRecords()
            Me.grdAssets.RetrieveStructure()
            Me.grdAssets.RootTable.Columns("Asset_Id").Visible = False
            Me.grdAssets.RootTable.Columns("Asset_Category_Id").Visible = False
            Me.grdAssets.RootTable.Columns("Asset_Type_Id").Visible = False
            Me.grdAssets.RootTable.Columns("Asset_Status_Id").Visible = False
            Me.grdAssets.RootTable.Columns("Asset_Condition_Id").Visible = False
            Me.grdAssets.RootTable.Columns("VendorId").Visible = False
            Me.grdAssets.RootTable.Columns("EmployeeId").Visible = False
            Me.grdAssets.RootTable.Columns("Asset_Picture").Visible = False
            Me.grdAssets.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtAssetName.Text = String.Empty Then
                ShowErrorMessage("Please Enter Asset Name ")
                Me.txtAssetName.Focus()
                Return False
            End If

            If Me.cmbcategory.SelectedIndex = 0 Then
                ShowErrorMessage("Please Select Asset Category ")
                Me.cmbcategory.Focus()
                Return False
            End If

            If Me.cmbtype.SelectedIndex = 0 Then
                ShowErrorMessage("Please Select Asset Type ")
                Me.cmbtype.Focus()
                Return False
            End If

            If Me.cmbCondition.SelectedIndex = 0 Then
                ShowErrorMessage("Please Select Asset Condition ")
                Me.cmbCondition.Focus()
                Return False
            End If

            If Me.cmbStatus.SelectedIndex = 0 Then
                ShowErrorMessage("Please Select Asset Status ")
                Me.cmbStatus.Focus()
                Return False
            End If

            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            aid = 0
            strPath = getConfigValueByType("AssetPicturePath").ToString
            Me.btnSave.Text = "&Save"
            Me.txtAssetName.Text = String.Empty
            Me.txtNumber.Text = GetNextDocNo("AST", 5, "tblAssets", "Asset_Number")
            Me.cmbcategory.SelectedIndex = 0
            Me.cmbtype.SelectedIndex = 0
            Me.txtBrand.Text = String.Empty
            Me.txtModel.Text = String.Empty
            Me.txtSerialNumber.Text = String.Empty
            Me.cmbLocation.SelectedIndex = 0
            Me.txtManufacturer.Text = String.Empty
            Me.txtDescription.Text = String.Empty
            Me.btnBrowse.Enabled = True
            Me.cmbCondition.SelectedIndex = 0
            Me.cmbStatus.SelectedIndex = 0
            Me.cmbVendor.SelectedIndex = 0
            Me.dtpPurchase.Value = Date.Now
            Me.txtPurchasePrice.Text = String.Empty
            Me.txtCurrentValue.Text = String.Empty
            Me.dtpExpire.Value = Date.Now
            Me.cmbEmployee.SelectedIndex = 0
            strImagePath = String.Empty
            assetPic.ImageLocation = Nothing
            Me.Assets.SelectedTab = Assets.Tabs(0).TabPage.Tab
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Dim assetid As Integer = New AssetsDAL().Save(asset)    'Current Id store in assetid
            If assetid > 0 Then
                ' For Create Directory Path
                If IO.Directory.Exists(strPath) = False Then
                    IO.Directory.CreateDirectory(strPath)
                End If
                If a.SafeFileName.Length > 0 Then
                    strImagePath = strPath & "\" & a.SafeFileName.Replace(a.SafeFileName, assetid & ".jpg")  'a.SafeFileName = Actual file name Transfer into assetid and extension is jpg
                    If IO.File.Exists(strImagePath) Then
                        IO.File.Delete(strImagePath)
                        assetPic.Image.Save(strImagePath, System.Drawing.Imaging.ImageFormat.Jpeg) ' Save Pic in Physical Path
                    Else
                        assetPic.Image.Save(strImagePath, System.Drawing.Imaging.ImageFormat.Jpeg) ' Save Pic in Physical Path
                    End If
                    Call New AssetsDAL().SaveImage(assetid, strImagePath)   ' Save in Database Path
                End If
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

    'Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    'End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If New AssetsDAL().Update(asset) = True Then
                If IO.Directory.Exists(strPath) = False Then
                    IO.Directory.CreateDirectory(strPath)
                End If

                If strImagePath.Length > 1 Then
                    If IO.File.Exists(strImagePath) Then
                        IO.File.Delete(strImagePath)
                        assetPic.Image.Save(strImagePath, System.Drawing.Imaging.ImageFormat.Jpeg) ' Save Pic in Physical Path
                    Else
                        assetPic.Image.Save(strImagePath, System.Drawing.Imaging.ImageFormat.Jpeg) ' Save Pic in Physical Path
                    End If
                    Call New AssetsDAL().SaveImage(asset.Asset_Id, strImagePath)
                End If
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub EditRecords(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons
        Try
            If Me.grdAssets.RowCount = 0 Then Exit Sub
            aid = Me.grdAssets.CurrentRow.Cells(0).Value
            Me.txtAssetName.Text = Me.grdAssets.CurrentRow.Cells("Asset_Name").Value
            Me.txtNumber.Text = Me.grdAssets.CurrentRow.Cells("Asset_Number").Value
            Me.txtDescription.Text = Me.grdAssets.CurrentRow.Cells("Asset_Description").Value
            Me.cmbLocation.SelectedValue = Me.grdAssets.CurrentRow.Cells("Asset_Location").Value
            Me.txtManufacturer.Text = Me.grdAssets.CurrentRow.Cells("Asset_Manufacturer").Value
            Me.txtBrand.Text = Me.grdAssets.CurrentRow.Cells("Asset_Brand").Value
            Me.txtModel.Text = Me.grdAssets.CurrentRow.Cells("Asset_Model").Value
            Me.txtSerialNumber.Text = Me.grdAssets.CurrentRow.Cells("Asset_Serial_No").Value
            Me.assetPic.ImageLocation = Me.grdAssets.CurrentRow.Cells("Asset_Picture").Value.ToString
            strImagePath = Me.grdAssets.CurrentRow.Cells("Asset_Picture").Value.ToString
            'Me.txtDescription.Text = Me.grdAssets.CurrentRow.Cells(10).Value
            Me.cmbcategory.SelectedValue = Me.grdAssets.CurrentRow.Cells("Asset_Category_Id").Value
            Me.cmbtype.SelectedValue = Me.grdAssets.CurrentRow.Cells("Asset_Type_Id").Value
            Me.cmbStatus.SelectedValue = Me.grdAssets.CurrentRow.Cells("Asset_Status_Id").Value
            Me.cmbCondition.SelectedValue = Me.grdAssets.CurrentRow.Cells("Asset_Condition_Id").Value
            Me.cmbVendor.SelectedValue = Me.grdAssets.CurrentRow.Cells("VendorId").Value
            Me.cmbEmployee.SelectedValue = Me.grdAssets.CurrentRow.Cells("EmployeeId").Value
            Me.txtPurchasePrice.Text = Me.grdAssets.CurrentRow.Cells("PurchasePrice").Value
            Me.txtCurrentValue.Text = Me.grdAssets.CurrentRow.Cells("CurrentValue").Value
            Me.dtpPurchase.Value = Me.grdAssets.CurrentRow.Cells("PurchaseDate").Value
            Me.dtpExpire.Value = Me.grdAssets.CurrentRow.Cells("Warranty_Expire_Date").Value
            btnSave.Text = "&Update"
            Me.Assets.SelectedTab = Assets.Tabs(0).TabPage.Tab
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Then
                    If Not msg_Confirm("Do you want to save this information ") = True Then
                        Exit Sub
                    End If
                    If Save() = True Then
                        DialogResult = Windows.Forms.DialogResult.Yes

                        msg_Information("information Saved Successfully ")
                        ReSetControls()
                    End If
                Else
                    If Not msg_Confirm("Do you want to update this Information? ") = True Then
                        Exit Sub
                    End If
                    If Update1() = True Then
                        DialogResult = Windows.Forms.DialogResult.Yes
                        'If imgPath.Length > 1 Then
                        '    pbxStPhoto.Image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg)
                        'End If
                        msg_Information("Information Update Successfully")
                        ReSetControls()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm("Do you want to delete this information ") = True Then
                Exit Sub
            End If
            asset = New AssetBE
            asset.Asset_Id = Me.grdAssets.CurrentRow.Cells("Asset_Id").Value
            If Delete() = True Then
                DialogResult = Windows.Forms.DialogResult.Yes
                msg_Information("Information Deleted Successfully")
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdAssets_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAssets.DoubleClick
        Try
            EditRecords(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0
            id = Me.cmbcategory.SelectedValue
            FillDropDown(cmbcategory, "Select Asset_Category_Id, Asset_Category_Name from tblAssetCategory")
            Me.cmbcategory.SelectedValue = id
            id = Me.cmbtype.SelectedValue
            FillDropDown(cmbtype, "Select Asset_Type_Id, Asset_Type_Name from tblAssetType")
            Me.cmbtype.SelectedValue = id
            id = Me.cmbLocation.SelectedValue
            FillDropDown(cmbLocation, "Select Asset_Location_id,Asset_Location_Name from tblAssetLocation")
            Me.cmbLocation.SelectedValue = id
            id = Me.cmbCondition.SelectedValue
            FillDropDown(cmbCondition, "Select Asset_Condition_id,Asset_Condition_Name from tblAssetCondition")
            Me.cmbCondition.SelectedValue = id
            id = Me.cmbStatus.SelectedValue
            FillDropDown(cmbStatus, "Select Asset_Status_id,Asset_Status_Name from tblAssetStatus")
            Me.cmbStatus.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            If Not IO.Directory.Exists(strPath) Then
                ShowErrorMessage("Folder not exist")
                Me.btnBrowse.Focus()
                Exit Sub
            End If
            a.Filter = "Image File |*.*" ' Filter only Image Files
            If a.ShowDialog = Windows.Forms.DialogResult.OK Then    ' If Select the Pic then Go Inside otherwise goto EndIf
                If IO.Directory.Exists(strPath) = False Then
                    IO.Directory.CreateDirectory(strPath)   'Create Directory for images and AssetPicture is the name of the Folder/Directory
                End If
                If btnSave.Text = "&Update" Then
                    strImagePath = strPath & "\" + a.SafeFileName.Replace(a.SafeFileName, aid & ".jpg")  ' Update ImagePath
                End If
                assetPic.ImageLocation = a.FileName     ' Image Set
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCategory.Click, btnCondition.Click, btnLocation.Click, btnStatus.Click, btnType.Click
        Try
            Dim id As Integer = 0

            Dim btn As Button = CType(sender, Button)
            Dim frmadd As New frmAdd
            Select Case btn.Name
                Case btnCategory.Name   ' Case 1
                    frmadd._Combo = SimpleAccounts.frmAdd.SelectCombo.Category
                    If frmadd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        id = Me.cmbcategory.SelectedValue
                        FillDropDown(cmbcategory, "Select Asset_Category_Id, Asset_Category_Name from tblAssetCategory")
                        Me.cmbcategory.SelectedValue = id
                    End If
                Case btnType.Name   'Case 2
                    frmadd._Combo = SimpleAccounts.frmAdd.SelectCombo.Type
                    If frmadd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        id = Me.cmbtype.SelectedValue
                        FillDropDown(cmbtype, "Select Asset_Type_Id, Asset_Type_Name from tblAssetType WHERE Asset_Category_Id=" & Me.cmbcategory.SelectedValue & "")
                        Me.cmbtype.SelectedValue = id
                    End If
                Case btnCondition.Name  'Case 3
                    frmadd._Combo = SimpleAccounts.frmAdd.SelectCombo.Condition
                    If frmadd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        id = Me.cmbCondition.SelectedValue
                        FillDropDown(cmbCondition, "Select Asset_Condition_id,Asset_Condition_Name from tblAssetCondition")
                        Me.cmbCondition.SelectedValue = id
                    End If
                Case btnStatus.Name     'Case 4
                    frmadd._Combo = SimpleAccounts.frmAdd.SelectCombo.Status
                    If frmadd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        id = Me.cmbStatus.SelectedValue
                        FillDropDown(cmbStatus, "Select Asset_Status_id,Asset_Status_Name from tblAssetStatus")
                        Me.cmbStatus.SelectedValue = id
                    End If
                Case btnLocation.Name   'Case 5
                    frmadd._Combo = SimpleAccounts.frmAdd.SelectCombo.Location
                    If frmadd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        id = Me.cmbLocation.SelectedValue
                        FillDropDown(cmbLocation, "Select Asset_Location_id,Asset_Location_Name from tblAssetLocation")
                        Me.cmbLocation.SelectedValue = id
                    End If
            End Select

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbcategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcategory.SelectedIndexChanged
        Try
            If Not Me.cmbcategory.SelectedIndex = -1 Then FillDropDown(cmbtype, "Select Asset_Type_Id, Asset_Type_Name from tblAssetType WHERE Asset_Category_Id=" & Me.cmbcategory.SelectedValue & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim CustId As Integer = 0
            CustId = Me.cmbVendor.SelectedValue
            FrmAddCustomers.FormType = "Vendor"
            FrmAddCustomers.ShowDialog()
            Me.FillCombos("Vendor")
            Me.cmbVendor.SelectedValue = CustId
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim CurrentId As Integer = 0
            If frmAddEmployee.ShowDialog = Windows.Forms.DialogResult.OK Then
                CurrentId = Me.cmbEmployee.SelectedValue
                FillDropDown(cmbEmployee, "Select Employee_Id,Employee_Name from tblDefEmployee Where Active = 1 ") ''TASKTFS75 added and set active =1
                Me.cmbEmployee.SelectedValue = CurrentId
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class