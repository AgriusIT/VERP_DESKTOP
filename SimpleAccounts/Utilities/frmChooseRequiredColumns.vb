Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Public Class frmChooseRequiredColumns
    Implements IGeneral
    Dim ColumnList As List(Of String)
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim strSQL As String = String.Empty
        Try
            If Condition = "Forms" Then
                strSQL = "Select FormId, FormName, FormCaption, FormModule From tblForms"
                FillUltraDropDown(Me.cmbForms, strSQL)
                Me.cmbForms.Rows(0).Activate()
                Me.cmbForms.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Condition = "Grids" Then
                Select Case Me.cmbForms.Text
                    Case "frmPurchaseInquiry"
                        Me.cmbGrids.Text = frmPurchaseInquiry.grdItems.Name
                End Select
            ElseIf Condition = "Columns" Then
                ColumnList = New List(Of String)
                Select Case Me.cmbForms.Text
                    Case "frmPurchaseInquiry"
                        If Me.cmbGrids.Text = "grdItems" Then
                            For Each Column As Janus.Windows.GridEX.GridEXColumn In frmPurchaseInquiry.grdItems.RootTable.Columns
                                ColumnList.Add("@" & Column.Key.ToString())
                            Next
                        End If
                        'For Each Control As Control In frmPurchaseInquiry.Controls
                        '    If (TypeOf Control Is Janus.Windows.GridEX) Then

                        '    End If
                        'Next
                End Select
                Me.ListBox1.DataSource = ColumnList
                ColumnList.Clear()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

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

    Private Sub frmChooseRequiredColumns_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FillCombos("Forms")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbForms_ValueChanged(sender As Object, e As EventArgs) Handles cmbForms.ValueChanged
        Try
            If cmbForms.Value > 0 Then
                FillCombos("Grids")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbGrids_Leave(sender As Object, e As EventArgs) Handles cmbGrids.Leave
        Try
            If cmbGrids.Text.Length > 0 Then
                FillCombos("Columns")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            Dim EmailTemplate As String = frmEmailConfiguration.txtHtmlTemplate.Text
            Dim i, j As Integer
            i = EmailTemplate.IndexOf("<Fields>") + "<Fields>".Length
            j = EmailTemplate.IndexOf("</Fields>") - i
            'Fields = EmailTemplate.Substring(i, j)
            'Dim WOAtTheRate As String = Fields.Replace("@", "")
            'Dim WOSpace As String = WOAtTheRate.Replace(" ", "")
            'EmailTemplate.Substring()
            frmEmailConfiguration.txtHtmlTemplate.Text.Insert(i, Me.ListBox1.SelectedItems.ToString)
            'EmailTemplate = EmailTemplate.Remove(i, j)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class