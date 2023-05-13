Public Class frmConfigHREmployeeAccount

    Public isFormOpen As Boolean = False
    Dim KeyType As String = String.Empty
    Dim KeyValue As String = String.Empty

    Private Sub frmConfigHREmployeeAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True

        FillCombos("EmployeeHeadAccount")
        FillCombos("EmpDeparmentAcHead")

        getConfigValueList()
        GetAllRecords()
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")
        Try

            Me.txtEmployeePicturePath.Text = getConfigValueByType("EmployeePicturePath").ToString

            Me.rbtSimpleEmpAc.Checked = Convert.ToBoolean(getConfigValueByType("EmpSimpleAccountHead").ToString)
            Me.rbtDeptEmpAc.Checked = Convert.ToBoolean(getConfigValueByType("EmpDepartmentAccountHead").ToString)

            Me.cmbEmployeeHeadAccountId.SelectedValue = Val(getConfigValueByType("EmployeeHeadAccountId").ToString)

            Me.cmbEmpDeptHeadAccountId.SelectedValue = Val(getConfigValueByType("EmployeeDeptHeadAccountId").ToString)

            If Me.rbtSimpleEmpAc.Checked = True Then
                Me.lblAccountLevel.Text = "Sub Sub Account From Chart of Account"
                Me.cmbEmpDeptHeadAccountId.Visible = True
                Me.cmbEmpDeptHeadAccountId.Visible = False
            ElseIf rbtDeptEmpAc.Checked = True Then
                Me.lblAccountLevel.Text = "Sub Account From Chart of Account"
                Me.cmbEmpDeptHeadAccountId.Visible = False
                Me.cmbEmpDeptHeadAccountId.Visible = True
            Else
                Me.lblAccountLevel.Text = "Sub Sub Account From Chart of Account"
                Me.cmbEmpDeptHeadAccountId.Visible = True
                Me.cmbEmpDeptHeadAccountId.Visible = False
            End If

            If getConfigValueByType("DayOff").ToString <> "Error" Then
                Dim str() As String = getConfigValueByType("DayOff").ToString.Split(",")
                If str.Length > 0 Then
                    For Each obj As String In str
                        For i As Integer = 0 To Me.cmbDayOff.Items.Count - 1
                            If Me.cmbDayOff.Items(i).ToString = obj.ToString Then
                                Me.cmbDayOff.SetItemChecked(i, True)
                            End If
                        Next
                    Next
                End If
            End If

            Me.nudLeaveEncashment.Value = Val(getConfigValueByType("LeaveEncashment").ToString)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub FillCombos(Optional Condition As String = "")
        Dim strSQL As String = String.Empty
        Select Case Condition
            Case "EmployeeHeadAccount"
                FillDropDown(Me.cmbEmployeeHeadAccountId, "select distinct main_sub_sub_id, Sub_sub_title, sub_sub_code From vwCOADetail ORder by sub_sub_title ASC")
            Case "EmpDeparmentAcHead"
                FillDropDown(Me.cmbEmpDeptHeadAccountId, "Select DISTINCT main_sub_Id, sub_title, sub_code from vwCOADetail where sub_title <> ''")
        End Select
    End Sub

    Private Sub btnEmpPicPath_Click(sender As Object, e As EventArgs) Handles btnEmpPicPath.Click
        Try
            If Me.FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtEmployeePicturePath.Text = FolderBrowserDialog1.SelectedPath
                frmConfigCompany.SaveConfiguration("EmployeePicturePath", Me.txtEmployeePicturePath.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtSimpleEmpAc_CheckedChanged(sender As Object, e As EventArgs) Handles rbtSimpleEmpAc.CheckedChanged

        Try
            If Me.rbtSimpleEmpAc.Checked = True Then
                Me.cmbEmpDeptHeadAccountId.Visible = False
                Me.cmbEmployeeHeadAccountId.Visible = True
                Me.lblAccountLevel.Text = "Sub Sub Account From Chart of Accout"
                Me.lblAccountLevel.Location = New Point(252, 119)
            Else
                Me.cmbEmpDeptHeadAccountId.Visible = True
                Me.cmbEmployeeHeadAccountId.Visible = False
                Me.lblAccountLevel.Text = "Sub Account From Chart of Account"
                Me.lblAccountLevel.Location = New Point(252, 152)
            End If

            frmConfigCompany.saveRadioBtnConfig(sender)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtDeptEmpAc_CheckedChanged(sender As Object, e As EventArgs) Handles rbtDeptEmpAc.CheckedChanged

        If Me.rbtSimpleEmpAc.Checked = True Then
            Me.cmbEmpDeptHeadAccountId.Visible = False
            Me.cmbEmployeeHeadAccountId.Visible = True
            Me.lblAccountLevel.Text = "Sub Sub Account From Chart of Accout"
            Me.lblAccountLevel.Location = New Point(252, 119)
        Else
            Me.cmbEmpDeptHeadAccountId.Visible = True
            Me.cmbEmployeeHeadAccountId.Visible = False
            Me.lblAccountLevel.Text = "Sub Account From Chart of Account"
            Me.lblAccountLevel.Location = New Point(252, 152)
        End If

        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub cmbEmpDeptHeadAccountId_Leave(sender As Object, e As EventArgs) Handles cmbEmpDeptHeadAccountId.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub cmbEmployeeHeadAccountId_Leave(sender As Object, e As EventArgs) Handles cmbEmployeeHeadAccountId.Leave
        frmConfigCompany.saveComboBoxValueConfig(sender)
    End Sub

    Private Sub cmbDayOff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDayOff.SelectedIndexChanged
        Try
            Dim str As String = String.Empty
            For i As Integer = 0 To Me.cmbDayOff.CheckedItems.Count - 1
                str += cmbDayOff.CheckedItems(i).ToString & ","
            Next
            If Not str = String.Empty Then
                str = str.Substring(0, str.LastIndexOf(","))
            End If

            frmConfigCompany.SaveConfiguration(Me.cmbDayOff.Tag, str)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub nudLeaveEncashment_ValueChanged(sender As Object, e As EventArgs) Handles nudLeaveEncashment.ValueChanged
        If isFormOpen = False Then Exit Sub
        Dim nud As NumericUpDown = CType(sender, NumericUpDown)
        frmConfigCompany.SaveConfiguration(nud.Tag, nud.Value)
    End Sub

    Private Sub lblSalary_Click(sender As Object, e As EventArgs) Handles lblSalary.Click
        Try
            If frmConfigHR.isFormOpen = True Then
                frmConfigHR.Dispose()
            End If

            frmConfigHR.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblOverTime_Click(sender As Object, e As EventArgs) Handles lblOverTime.Click
        Try
            If frmConfigHROvertime.isFormOpen = True Then
                frmConfigHROvertime.Dispose()
            End If

            frmConfigHROvertime.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblProvidentFund_Click(sender As Object, e As EventArgs) Handles lblProvidentFund.Click
        Try
            If frmConfigHRProvidentFund.isFormOpen = True Then
                frmConfigHRProvidentFund.Dispose()
            End If

            frmConfigHRProvidentFund.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblAttendance_Click(sender As Object, e As EventArgs) Handles lblAttendance.Click
        Try
            If frmConfigHRAttendance.isFormOpen = True Then
                frmConfigHRAttendance.Dispose()
            End If

            frmConfigHRAttendance.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class