Public Class frmBudgetConfiguration
    Implements IGeneral
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            str = "SELECT BudgetId, BudgetName from tblDefBudget where Active = 1"
            FillDropDown(cmbBudget, str, True)
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

    Private Sub frmBudgetConfiguration_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FillCombos()
            grdPayment.BringToFront()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            GetPivotRecord()
            GetSimpleRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub GetPivotRecord()
        Try
            Dim dt As DataTable
            Dim str As String
            str = "SELECT ReportTemplateNotesCategory.CategoryTitle, '' as SubSub, tblCOAMainSubSubDetail.detail_title as Detail, tblDefCostCenter.Name as CostCenter, 0 AS Amount FROM ReportTemplateDetail INNER JOIN ReportTemplate ON ReportTemplateDetail.ReportTemplateId = ReportTemplate.ReportTemplateId INNER JOIN tblDefBudget ON ReportTemplate.ReportTemplateId = tblDefBudget.ReportId INNER JOIN ReportTemplateNotesCategory ON ReportTemplateDetail.CategoryId = ReportTemplateNotesCategory.CategoryId INNER JOIN tblCOAMainSubSubDetail ON ReportTemplateDetail.AccountId = tblCOAMainSubSubDetail.coa_detail_id INNER JOIN tblBudgetCostCenter ON tblDefBudget.BudgetId = tblBudgetCostCenter.BudgetId INNER JOIN tblDefCostCenter ON tblBudgetCostCenter.CostCenterId = tblDefCostCenter.CostCenterID WHERE ReportTemplateDetail.AccountLevel = 'Detail' and tbldefbudget.BudgetId = " & cmbBudget.SelectedValue & "" & _
            "Union ALL " & _
            "SELECT ReportTemplateNotesCategory.CategoryTitle, tblCOAMainSubSub.sub_sub_title as SubSub, '' AS Detail, tblDefCostCenter.Name as CostCenter, 0 AS Amount FROM ReportTemplateDetail INNER JOIN ReportTemplate ON ReportTemplateDetail.ReportTemplateId = ReportTemplate.ReportTemplateId INNER JOIN tblDefBudget ON ReportTemplate.ReportTemplateId = tblDefBudget.ReportId INNER JOIN ReportTemplateNotesCategory ON ReportTemplateDetail.CategoryId = ReportTemplateNotesCategory.CategoryId INNER JOIN tblBudgetCostCenter ON tblDefBudget.BudgetId = tblBudgetCostCenter.BudgetId INNER JOIN tblCOAMainSubSub ON ReportTemplateDetail.AccountId = tblCOAMainSubSub.main_sub_sub_id LEFT OUTER JOIN tblDefCostCenter ON tblBudgetCostCenter.CostCenterId = tblDefCostCenter.CostCenterID WHERE (ReportTemplateDetail.AccountLevel = 'Sub Sub') AND (tblDefBudget.BudgetId = " & cmbBudget.SelectedValue & ")"
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Dim dtCC As DataTable = dt.DefaultView.ToTable(True, "CostCenter")
            If dtCC.Rows.Count = 0 Then
                msg_Error("No records found in given date range")
                Exit Sub
            End If
            Dim q As Integer = 0
            Dim strCC As String = ""
            Dim strCCIsNull As String = ""
            Dim strExpression As String = ""
            For q = 0 To dtCC.Rows.Count - 1 Step 0
                If strCC.ToString = "[]" Or strCCIsNull.ToString = "[]" Then
                    strCC = "[(none)]"
                    strCCIsNull = "ISNULL([(none)],0) AS [(none)]"
                    strExpression = "[(none)]"
                End If
                If q <> 0 AndAlso strCC.ToString <> "" Then
                    strCC = strCC & ", "
                    strCCIsNull = strCCIsNull & ", "
                    strExpression = strExpression & " + "
                End If
                strCC = strCC & "[" & dtCC.Rows(q).Item(0) & "]"
                strCCIsNull = strCCIsNull & "ISNULL([" & dtCC.Rows(q).Item(0) & "],0) AS [" & dtCC.Rows(q).Item(0) & "]"
                strExpression = strExpression & "[" & dtCC.Rows(q).Item(0) & "]"
                q += 1
            Next
            Dim strData As String = ""
            Dim dtData As DataTable
            strData = "SELECT CategoryTitle, SubSub, Detail, " & strCCIsNull & ", 0 AS TotalAmount FROM " & _
                      "(SELECT ReportTemplateNotesCategory.CategoryTitle, '' as SubSub, tblCOAMainSubSubDetail.detail_title as Detail, tblDefCostCenter.Name as CostCenter, 0 AS Amount FROM ReportTemplateDetail INNER JOIN ReportTemplate ON ReportTemplateDetail.ReportTemplateId = ReportTemplate.ReportTemplateId INNER JOIN tblDefBudget ON ReportTemplate.ReportTemplateId = tblDefBudget.ReportId INNER JOIN ReportTemplateNotesCategory ON ReportTemplateDetail.CategoryId = ReportTemplateNotesCategory.CategoryId INNER JOIN tblCOAMainSubSubDetail ON ReportTemplateDetail.AccountId = tblCOAMainSubSubDetail.coa_detail_id INNER JOIN tblBudgetCostCenter ON tblDefBudget.BudgetId = tblBudgetCostCenter.BudgetId INNER JOIN tblDefCostCenter ON tblBudgetCostCenter.CostCenterId = tblDefCostCenter.CostCenterID WHERE ReportTemplateDetail.AccountLevel = 'Detail' and tbldefbudget.BudgetId = " & cmbBudget.SelectedValue & "" & _
                      "Union ALL " & _
                      "SELECT ReportTemplateNotesCategory.CategoryTitle, tblCOAMainSubSub.sub_sub_title as SubSub, '' AS Detail, tblDefCostCenter.Name as CostCenter, 0 AS Amount FROM ReportTemplateDetail INNER JOIN ReportTemplate ON ReportTemplateDetail.ReportTemplateId = ReportTemplate.ReportTemplateId INNER JOIN tblDefBudget ON ReportTemplate.ReportTemplateId = tblDefBudget.ReportId INNER JOIN ReportTemplateNotesCategory ON ReportTemplateDetail.CategoryId = ReportTemplateNotesCategory.CategoryId INNER JOIN tblBudgetCostCenter ON tblDefBudget.BudgetId = tblBudgetCostCenter.BudgetId INNER JOIN tblCOAMainSubSub ON ReportTemplateDetail.AccountId = tblCOAMainSubSub.main_sub_sub_id LEFT OUTER JOIN tblDefCostCenter ON tblBudgetCostCenter.CostCenterId = tblDefCostCenter.CostCenterID WHERE (ReportTemplateDetail.AccountLevel = 'Sub Sub') AND (tblDefBudget.BudgetId = " & cmbBudget.SelectedValue & ")) AS Budget " & _
                      "PIVOT (SUM(Amount) for CostCenter IN(" & strCC & ")) As PVT"
            dtData = GetDataTable(strData)
            dtData.Columns("TotalAmount").Expression = strExpression
            dtData.AcceptChanges()
            Me.grdPayment.DataSource = dtData
            Me.grdPayment.RetrieveStructure()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetSimpleRecord()
        Try
            Dim dt As DataTable
            Dim str As String
            str = "SELECT ReportTemplateNotesCategory.CategoryTitle, '' as SubSub, tblCOAMainSubSubDetail.detail_title as Detail, tblDefCostCenter.Name as CostCenter, 0 as Amount FROM ReportTemplateDetail INNER JOIN ReportTemplate ON ReportTemplateDetail.ReportTemplateId = ReportTemplate.ReportTemplateId INNER JOIN tblDefBudget ON ReportTemplate.ReportTemplateId = tblDefBudget.ReportId INNER JOIN ReportTemplateNotesCategory ON ReportTemplateDetail.CategoryId = ReportTemplateNotesCategory.CategoryId INNER JOIN tblCOAMainSubSubDetail ON ReportTemplateDetail.AccountId = tblCOAMainSubSubDetail.coa_detail_id INNER JOIN tblBudgetCostCenter ON tblDefBudget.BudgetId = tblBudgetCostCenter.BudgetId INNER JOIN tblDefCostCenter ON tblBudgetCostCenter.CostCenterId = tblDefCostCenter.CostCenterID WHERE ReportTemplateDetail.AccountLevel = 'Detail' and tbldefbudget.BudgetId = " & cmbBudget.SelectedValue & "" & _
                  "Union ALL " & _
                  "SELECT ReportTemplateNotesCategory.CategoryTitle, tblCOAMainSubSub.sub_sub_title as SubSub, '' AS Detail, tblDefCostCenter.Name as CostCenter, 0 as Amount FROM ReportTemplateDetail INNER JOIN ReportTemplate ON ReportTemplateDetail.ReportTemplateId = ReportTemplate.ReportTemplateId INNER JOIN tblDefBudget ON ReportTemplate.ReportTemplateId = tblDefBudget.ReportId INNER JOIN ReportTemplateNotesCategory ON ReportTemplateDetail.CategoryId = ReportTemplateNotesCategory.CategoryId INNER JOIN tblBudgetCostCenter ON tblDefBudget.BudgetId = tblBudgetCostCenter.BudgetId INNER JOIN tblCOAMainSubSub ON ReportTemplateDetail.AccountId = tblCOAMainSubSub.main_sub_sub_id LEFT OUTER JOIN tblDefCostCenter ON tblBudgetCostCenter.CostCenterId = tblDefCostCenter.CostCenterID WHERE (ReportTemplateDetail.AccountLevel = 'Sub Sub') AND (tblDefBudget.BudgetId = " & cmbBudget.SelectedValue & ")"
            dt = GetDataTable(str)
            grdSimple.DataSource = dt
            Me.grdSimple.RetrieveStructure()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class