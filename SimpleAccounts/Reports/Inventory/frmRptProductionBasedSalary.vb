Imports SBModel

Public Class frmRptProductionBasedSalary

    Private Sub FillCombo(Optional ByVal Condition As String = "")
        Dim Str As String = ""
        Try
            If Condition = "Product" Then
                Str = "SELECT DISTINCT ArticleDefView.ArticleId as Id, ArticleCode as Code, ArticleDescription as Item,  ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit, ISNULL(PurchasePrice,0) as Price,  ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model , ArticleDefView.SizeRangeID as [Size ID], Isnull(ArticleDefView.SubSubId,0) as AccountId, ArticleDefView.SortOrder, ArticleDefView.MasterId " & _
                  " From ArticleDefView INNER JOIN tblEmployeeArticleCostRate AS ArticleCost ON ArticleDefView.ArticleId = ArticleCost.ArticleDefId INNER JOIN (SELECT DISTINCT ArticleDefId As ArticleId FROM ProductionDetailTable AS Detail INNER JOIN ProductionMasterTable AS Production ON Detail.Production_ID = Production.Production_ID WHERE Convert(varchar, Production_date, 102) BETWEEN Convert(datetime, '" & DateTimePicker1.Value.ToString("yyyy-M-dd 00:00:0") & "', 102) AND Convert(datetime, '" & DateTimePicker2.Value.ToString("yyyy-M-dd 23:59:59") & "', 102)) AS Production ON ArticleDefView.ArticleId = Production.ArticleId WHERE ArticleDefView.Active=1 AND ArticleCost.Rate > 0 " & IIf(Me.cmbEmployee.SelectedValue > 0, " AND ArticleCost.Employee_ID = " & Me.cmbEmployee.SelectedValue & "", "") & " "
                FillUltraDropDown(Me.cmbProduct, Str)
                Me.cmbProduct.Rows(0).Activate()
                If Me.cmbProduct.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbProduct.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                    Me.cmbProduct.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                    Me.cmbProduct.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                    Me.cmbProduct.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                    Me.cmbProduct.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True
                    If Me.rbCode.Checked = True Then
                        Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells(1).Column.Key.ToString
                    Else
                        Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells(2).Column.Key.ToString
                    End If
                End If
            ElseIf Condition = "Project" Then
                FillDropDown(Me.cmbProject, "Select CostCenterId, Name From tblDefCostCenter WHERE Active=1")
            ElseIf Condition = "Employee" Then
                FillDropDown(Me.cmbEmployee, "Select Employee_ID, Employee_Name From tblDefEmployee Where Active = 1 " & IIf(Me.cmbDepartment.SelectedValue > 0, " AND Dept_ID = " & Me.cmbDepartment.SelectedValue & "", "") & " ORDER BY 2 ASC")
            ElseIf Condition = "Department" Then
                Str = "Select EmployeeDeptID, EmployeeDeptName, IsNull(DeptAccountHeadId,0) as DeptAccountHeadId from EmployeeDeptDefTable order by 2"
                FillDropDown(cmbDepartment, Str)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub frmRptProductionBasedSalary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            FillCombo("Project")
            FillCombo("Department")
            FillCombo("Employee")
            FillCombo("Product")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbProject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProject.SelectedIndexChanged
        Try
            If Not cmbProject.SelectedIndex = -1 Then
                FillCombo("Employee")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged
        Try
            If Not Me.cmbDepartment.SelectedIndex = -1 Then
                FillCombo("Employee")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbEmployee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEmployee.SelectedIndexChanged
        Try
            If Not Me.cmbEmployee.SelectedIndex = -1 Then
                FillCombo("Product")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Dim dt As DataTable
        Dim Str As String = ""
        Try
            Str = "Exec GetProductionBasedSalary '" & DateTimePicker1.Value.ToString("yyyy-M-dd 00:00:0") & "', '" & DateTimePicker2.Value.ToString("yyyy-M-dd 23:59:59") & "', " & IIf(Me.cmbProject.SelectedIndex = -1, 0, Me.cmbProject.SelectedValue) & ", " & IIf(Me.cmbEmployee.SelectedIndex = -1, 0, Me.cmbEmployee.SelectedValue) & ", " & IIf(Me.cmbProduct.Value = Nothing, 0, Me.cmbProduct.Value) & " , " & IIf(Me.cmbDepartment.SelectedValue = Nothing, 0, Me.cmbDepartment.SelectedValue) & ""
            dt = GetDataTable(Str)
            dt.AcceptChanges()
            GridEX1.DataSource = dt
            GridEX1.RetrieveStructure()
            GridEX1.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Total").FormatString = "N" & DecimalPointInValue

            GridEX1.RootTable.Columns("Qty").TotalFormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("Rate").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("Total").TotalFormatString = "N" & DecimalPointInValue

            GridEX1.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Rate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Total").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            GridEX1.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Rate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("Total").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Production Based Salary " & Chr(10) & "From Date: " & DateTimePicker1.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & DateTimePicker1.Value.ToString("dd-MM-yyyy").ToString & ""

            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GridEX1_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles GridEX1.FormattingRow

    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Exit Sub
                End If
            Else
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                        ''TASK TFS1384 replaced Report Print and Report Export with Report Print and Report Export on 07-09-2017
                    ElseIf RightsDt.FormControlName = "Report Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Report Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                        ''End TASK TFS1384
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Try
            FillCombo("Product")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim Id As Integer = 0
        Try
            Id = Me.cmbProject.SelectedValue
            FillCombo("Project")
            Me.cmbProject.SelectedValue = Id

            Id = Me.cmbDepartment.SelectedValue
            FillCombo("Department")
            Me.cmbDepartment.SelectedValue = Id

            Id = Me.cmbEmployee.SelectedValue
            FillCombo("Employee")
            Me.cmbEmployee.SelectedValue = Id

            Id = Me.cmbProduct.Value
            FillCombo("Product")
            Me.cmbProduct.Value = Id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class