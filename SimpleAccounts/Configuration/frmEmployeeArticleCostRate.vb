Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Net

Public Class frmEmployeeArticleCostRate

    Implements IGeneral
    Dim IsOpenForm As Boolean = False
    Dim CostList As List(Of EmployeeArticleCostRateBE)
    Enum enmCostRate
        EmployeeId
        ArticleId
        ArticleCode
        ArticleDescription
        Color
        Size
        Rate
    End Enum

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                If Me.grd.RootTable.Columns(c).Index <> enmCostRate.Rate Then
                    Me.grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New EmployeeArticleCostRateDAL().Delete(Me.cmbEmployee.SelectedValue) = True Then
                Return True
            Else
                Throw New Exception("Some of data is not provided.")
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            FillDropDown(Me.cmbEmployee, "Select Employee_ID, Employee_Name From tblDefEmployee Where Active = 1 ORDER BY 2 ASC") ''TASKTFS75 added and set active =1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            CostList = New List(Of EmployeeArticleCostRateBE)
            Dim Cost As EmployeeArticleCostRateBE
            Dim dblValidateAmount As Double = 0D
            For Each objRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                If Val(objRow.Cells(enmCostRate.Rate).Value.ToString) <> 0 Then
                    Cost = New EmployeeArticleCostRateBE
                    Cost.ID = 0.0
                    Cost.Employee_ID = Me.cmbEmployee.SelectedValue
                    Cost.ArticleDefId = Val(objRow.Cells(enmCostRate.ArticleId).Value.ToString)
                    Cost.Rate = Val(objRow.Cells(enmCostRate.Rate).Value.ToString)
                    CostList.Add(Cost)
                    dblValidateAmount += Val(objRow.Cells(enmCostRate.Rate).Value.ToString)
                End If
            Next
            If dblValidateAmount <= 0 Then
                Throw New Exception("Please enter Cost Rate.")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If Me.cmbEmployee.SelectedIndex = -1 Then Exit Sub
            Dim strSQL As String = String.Empty
            If Condition = String.Empty Then
                strSQL = "SELECT ISNULL(Cost.Employee_ID, 0) AS Employee_ID, dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, " _
                         & " dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName, ISNULL(Cost.Rate, 0) AS Rate " _
                         & " FROM dbo.ArticleDefView LEFT OUTER JOIN " _
                         & " (SELECT ArticleDefId, Employee_ID, Rate " _
                         & " FROM dbo.tblEmployeeArticleCostRate WHERE ISNULL(Employee_ID, 0)=" & Me.cmbEmployee.SelectedValue & ") AS Cost ON Cost.ArticleDefId = dbo.ArticleDefView.ArticleId "
                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                dt.AcceptChanges()
                Me.grd.DataSource = dt
                ApplyGridSettings()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbEmployee.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select Employee.")
                Me.cmbEmployee.Focus()
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
            If Not Me.cmbEmployee.SelectedIndex = -1 Then Me.cmbEmployee.SelectedIndex = 0
            GetAllRecords(String.Empty)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New EmployeeArticleCostRateDAL().Add(CostList) = True Then
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

    End Function
    Private Sub cmbEmployee_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEmployee.SelectedIndexChanged
        Try
            If IsOpenForm = True Then GetAllRecords(String.Empty) Else Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmEmployeeArticleCostRate_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            FillCombos()
            ReSetControls()
            IsOpenForm = True
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
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Save() = True Then
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.cmbEmployee.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select Employee.")
                Exit Sub
            End If

            If Delete() = True Then
                ReSetControls()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Dim id As Integer = 0I
            id = Me.cmbEmployee.SelectedIndex
            FillCombos()
            Me.cmbEmployee.SelectedIndex = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


End Class