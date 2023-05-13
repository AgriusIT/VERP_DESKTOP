Imports SBModel
Imports SBDal
Imports System.Data.SqlClient

Public Class frmAdd

    Implements IGeneral
    Public Enum SelectCombo
        Category
        Type
        Condition
        Status
        Location
        Vendor
        Employee
    End Enum
    Public _Combo As SelectCombo
    Dim cat As AssetCategoryBE
    Dim type As AssetTypeBE
    Dim loc As AssetLocationBE
    Dim status As AssetStatusBE
    Dim con As AssetConditionBE
    Dim id As Integer

    Private Sub frmAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If _Combo = SelectCombo.Category Then
                Me.Text = "Asset Category"      ' Set Name on Form
            ElseIf _Combo = SelectCombo.Condition Then
                Me.Text = "Asset Condition"
                'ElseIf _Combo = SelectCombo.Employee Then
                '    Me.Text = "Employee's"
            ElseIf _Combo = SelectCombo.Location Then
                Me.Text = "Asset Location"
                Me.txtDescription.Visible = False
                Me.Label2.Visible = False
            ElseIf _Combo = SelectCombo.Status Then
                Me.Text = "Asset Status"
            ElseIf _Combo = SelectCombo.Type Then
                Me.Text = "Asset Type"
                'ElseIf _Combo = SelectCombo.Vendor Then
                'Me.Text = "Vendor"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtName.Text = String.Empty
            Me.txtDescription.Text = String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If _Combo = SelectCombo.Category Then
                cat = New AssetCategoryBE
                cat.Asset_Category_Id = id
                cat.Asset_Category_Name = Me.txtName.Text
                cat.Sort_Order = 1
                cat.Active = True
                cat.Asset_Category_Description = Me.txtDescription.Text
                If New AssetCategoryDAL().Save(cat) = True Then
                    Return True
                Else
                    Return False
                End If
            End If

            If _Combo = SelectCombo.Condition Then
                con = New AssetConditionBE
                con.Asset_Condition_Id = id
                con.Asset_Condition_Name = Me.txtName.Text
                con.Sort_Order = 1
                con.Active = True
                con.Asset_Condition_Description = Me.txtDescription.Text
                If New AssetConditionDAL().Save(con) = True Then
                    Return True
                Else
                    Return False
                End If
            End If

            If _Combo = SelectCombo.Location Then
                loc = New AssetLocationBE
                loc.Asset_Location_Id = id
                loc.Asset_Location_Name = Me.txtName.Text
                loc.Sort_Order = 1
                loc.Active = True
                If New AssetLocationDAL().Save(loc) = True Then
                    Return True
                Else
                    Return False
                End If
            End If

            If _Combo = SelectCombo.Status Then
                status = New AssetStatusBE
                status.Asset_Status_Id = id
                status.Asset_Status_Name = Me.txtName.Text
                status.Asset_Status_Description = Me.txtDescription.Text
                status.Sort_Order = 1
                status.Active = True
                If New AssetStatusDAL().Save(status) = True Then
                    Return True
                Else
                    Return False
                End If
            End If

            If _Combo = SelectCombo.Type Then
                type = New AssetTypeBE
                type.Asset_Type_Id = id
                type.Asset_Type_Name = Me.txtName.Text
                type.Sort_Order = 1
                type.Active = True
                type.Asset_Type_Description = Me.txtDescription.Text
                type.Asset_Category_Id = frmAsset.cmbcategory.SelectedValue
                If New AssetTypeDAL().Save(type) = True Then
                    Return True
                Else
                    Return False
                End If
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

    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Save() = True Then
                DialogResult = Windows.Forms.DialogResult.OK
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class