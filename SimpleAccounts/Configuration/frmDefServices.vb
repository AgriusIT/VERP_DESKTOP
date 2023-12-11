Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility

Public Class frmDefServices
    Implements IGeneral
    Dim Services As ServicesBE
    Dim ServicesID As Integer = 0I
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            Services = New ServicesBE
            Services.ServicesID = Val(Me.grdSaved.GetRow.Cells("ServicesID").Value.ToString)
            If New ServicesDAL().Delete(Services) = True Then
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
            Services = New ServicesBE
            Services.ServicesID = ServicesID
            Services.ServicesType = Me.txtType.Text
            Services.Tax_Percent = Val(Me.txtTax.Text)
            Services.WHTax = Val(Me.txtWHTax.Text)
            Services.Opex = Val(Me.txtOpex.Text)
            Services.Region = Me.txtRegion.Text
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Dim objDt As New DataTable
            objDt = New ServicesDAL().GetAllRecord
            Me.grdSaved.DataSource = objDt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("ServicesID").Visible = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.txtType.Text = String.Empty Then
                ShowErrorMessage("Please enter Type.")
                Me.txtType.Focus()
                Return False
            End If

            If Me.txtTax.Text = String.Empty Then
                ShowErrorMessage("Please enter Tax percent.")
                Me.txtTax.Focus()
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
            ServicesID = 0I
            Me.btnSave.Text = "&Save"
            Me.txtType.Text = String.Empty
            Me.txtTax.Text = String.Empty
            Me.txtWHTax.Text = String.Empty
            Me.txtOpex.Text = String.Empty
            Me.txtRegion.Text = String.Empty
            Me.txtType.Focus()
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New ServicesDAL().Add(Services) = True Then
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
            If New ServicesDAL().Update(Services) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    If Save() = True Then ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            ServicesID = Me.grdSaved.GetRow.Cells("ServicesID").Value
            Me.txtType.Text = Me.grdSaved.GetRow.Cells("ServicesType").Value.ToString
            Me.txtTax.Text = Val(Me.grdSaved.GetRow.Cells("Tax_Percent").Value.ToString)
            Me.txtWHTax.Text = Val(Me.grdSaved.GetRow.Cells("WHT_Percent").Value.ToString)
            Me.txtOpex.Text = Val(Me.grdSaved.GetRow.Cells("Opex_Sale_Percent").Value.ToString)
            Me.txtRegion.Text = Me.grdSaved.GetRow.Cells("Region").Value.ToString
            Me.btnSave.Text = "&Update"
            Me.txtType.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdShifttype_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            btnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmDefServices_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtOpex_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOpex.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTax.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtWHTax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWHTax.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class