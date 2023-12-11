Imports SBDal
Imports SBModel
Imports System
Public Class frmDefArticleDepartment
    Implements IGeneral
    Dim DeptId As Integer = 0I
    Dim Dept As ArticleDepartmentBE
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.GridEX1.RootTable.Columns("ArticleGroupId").Visible = False
            Me.GridEX1.RootTable.Columns("SubSubId").Visible = False
            'Me.GridEX1.RootTable.Columns("ServiceItem").Visible = False
            'Me.GridEX1.RootTable.Columns("SalesItem").Visible = False
            Me.GridEX1.RootTable.Columns("IsDate").Visible = False
            Me.GridEX1.RootTable.Columns("SalesAccountId").Visible = False
            Me.GridEX1.RootTable.Columns("CGSAccountId").Visible = False
            Me.GridEX1.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New ArticleDepartmentDAL().Delete(Dept) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            'FillDropDown(Me.cmbDepartment, "Select main_sub_sub_id, sub_sub_title From tblCOAMainSubSub WHERE Account_Type='Inventory'")
            FillDropDown(Me.cmbDepartment, "SELECT  v.coa_detail_Id as sub_sub_Id,  v.detail_title FROM  dbo.vwCOADetail v WHERE (v.account_type = 'Inventory') AND v.detail_title <> '' or (v.main_type = 'Assets') AND v.detail_title <> ''")
            FillDropDown(Me.cmbSalesAccount, "SELECT  v.coa_detail_Id as sub_sub_Id,  v.detail_title FROM  dbo.vwCOADetail v WHERE (v.main_type = 'Income') AND v.detail_title <> ''")
            FillDropDown(Me.cmbCSG, "SELECT  v.coa_detail_Id as sub_sub_Id,  v.detail_title FROM  dbo.vwCOADetail v WHERE (v.main_type = 'Expense') AND v.detail_title <> ''")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Dept = New ArticleDepartmentBE
            Dept.ArticleGroupId = DeptId
            Dept.ArticleGroupName = Me.txtDepartment.Text
            Dept.Comments = Me.txtComments.Text
            Dept.Active = True
            Dept.SortOrder = Val(Me.txtSortOrder.Text)
            Dept.ServiceItem = Me.chkServiceItem.Checked
            Dept.SalesItem = Me.chkSalesItem.Checked
            Dept.SubSubId = Me.cmbDepartment.SelectedValue
            Dept.GroupCode = Me.txtDeptCode.Text
            Dept.SalesAccountId = IIf(Me.cmbSalesAccount.SelectedIndex > 0, Me.cmbSalesAccount.SelectedValue, 0)
            Dept.CGSAccountId = IIf(Me.cmbCSG.SelectedIndex > 0, Me.cmbCSG.SelectedValue, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.GridEX1.DataSource = New ArticleDepartmentDAL().GetAll
            Me.GridEX1.RetrieveStructure()
            ApplyGridSettings()
            CtrlGrdBar3_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbDepartment.SelectedIndex = -1 Then
                ShowErrorMessage("Please Define Account")
                Me.cmbDepartment.Focus()
                Return False
            End If
            If Me.cmbDepartment.SelectedIndex = 0 Then
                ShowErrorMessage("Please Select Account")
                Me.cmbDepartment.Focus()
                Return False
            End If
            If Me.txtDepartment.Text = String.Empty Then
                ShowErrorMessage("Please Enter Department Name")
                Me.txtDepartment.Focus()
                Return False
            End If


            Dim str As String = "Select Count(*), ArticleGroupName,GroupCode From ArticleGroupDefTable WHERE (GroupCode='" & Me.txtDeptCode.Text.Replace("'", "''") & "' Or ArticleGroupName='" & Me.txtDepartment.Text.Replace("'", "''") & "')  And ArticleGroupId <> " & DeptId & "  GROUP BY ArticleGroupName,GroupCode"
            Dim dt As New DataTable
            dt = GetDataTable(str)
            dt.AcceptChanges()

            If dt.HasErrors = False Then
                If dt.Rows.Count > 0 Then
                    If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                        If Me.txtDepartment.Text.ToUpper = dt.Rows(0).Item(1).ToString.ToUpper Then
                            ShowErrorMessage("[Category: " & dt.Rows(0).Item(1).ToString.Replace("'", "''") & "] is already exist")
                            Return False
                        ElseIf Me.txtDeptCode.Text.ToUpper = dt.Rows(0).Item(2).ToString.ToUpper Then
                            ShowErrorMessage("[Category Code: " & dt.Rows(0).Item(2).ToString.Replace("'", "''") & "] is already exist")
                            Return False
                        End If
                    End If
                End If
            End If


            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            DeptId = 0
            Me.btnSave.Text = "&Save"
            Me.cmbDepartment.SelectedIndex = 0
            Me.txtDepartment.Text = String.Empty
            Me.txtComments.Text = String.Empty
            Me.txtSortOrder.Text = 1
            Me.chkSalesItem.Checked = True
            Me.chkServiceItem.Checked = False
            'TFS4726: Waqar: Added this line to reset Code filed on resets
            Me.txtDeptCode.Text = String.Empty
            If Not Me.cmbSalesAccount.SelectedIndex = -1 Then Me.cmbSalesAccount.SelectedIndex = 0
            If Not Me.cmbCSG.SelectedIndex = -1 Then Me.cmbCSG.SelectedIndex = 0
            GetAllRecords()
            CtrlGrdBar3_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New ArticleDepartmentDAL().Save(Dept) = True Then
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
            If New ArticleDepartmentDAL().Update(Dept) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmDefArticleDepartment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 10 -1-14
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If

          
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmDefArticleDepartment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            FillCombos()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

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
            If Me.GridEX1.RowCount = 0 Then Exit Sub
            DeptId = Val(Me.GridEX1.GetRow.Cells(0).Value.ToString)
            Me.cmbDepartment.SelectedValue = Val(Me.GridEX1.GetRow.Cells("SubSubId").Value.ToString)
            Me.txtDepartment.Text = Me.GridEX1.GetRow.Cells("ArticleGroupName").Value.ToString
            Me.txtComments.Text = Me.GridEX1.GetRow.Cells("Comments").Value.ToString
            Me.txtSortOrder.Text = Val(Me.GridEX1.GetRow.Cells("SortOrder").Value.ToString)
            Me.chkServiceItem.Checked = Me.GridEX1.GetRow.Cells("ServiceItem").Value.ToString
            Me.chkSalesItem.Checked = Me.GridEX1.GetRow.Cells("SalesItem").Value.ToString
            Me.cmbSalesAccount.SelectedValue = Val(Me.GridEX1.GetRow.Cells("SalesAccountId").Value.ToString)
            Me.cmbCSG.SelectedValue = Val(Me.GridEX1.GetRow.Cells("CGSAccountId").Value.ToString)
            'TFS4726: Waqar: Added This line to add Code in Edit mode
            Me.txtDeptCode.Text = Me.GridEX1.GetRow.Cells("GroupCode").Value.ToString
            Me.btnSave.Text = "&Update"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GridEX1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick
        Try
            btnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Then
                    If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub

                    'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Save() = True Then
                        'msg_Information(str_informSave)
                        ReSetControls()
                    End If
                Else

                    'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then
                        'msg_Information(str_informUpdate)
                        ReSetControls()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If IsValidate() = True Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

                'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Delete() = True Then
                    'msg_Information(str_informDelete)
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim id As Integer = 0I
            id = Me.cmbDepartment.SelectedValue
            'FillDropDown(Me.cmbDepartment, "Select main_sub_sub_id, sub_sub_title From tblCOAMainSubSub WHERE Account_Type='Inventory'")
            FillDropDown(Me.cmbDepartment, "SELECT  v.coa_detail_Id as sub_sub_Id,  v.detail_title FROM  dbo.vwCOADetail v WHERE (v.account_type = 'Inventory') AND v.detail_title <> '' or (v.main_type = 'Assets') AND v.detail_title <> ''")
            Me.cmbDepartment.SelectedValue = id

            id = Me.cmbSalesAccount.SelectedIndex
            FillDropDown(Me.cmbSalesAccount, "SELECT  v.coa_detail_Id as sub_sub_Id,  v.detail_title FROM  dbo.vwCOADetail v WHERE (v.main_type = 'Income')")
            Me.cmbSalesAccount.SelectedIndex = id

            id = Me.cmbCSG.SelectedIndex
            FillDropDown(Me.cmbCSG, "SELECT  v.coa_detail_Id as sub_sub_Id,  v.detail_title FROM  dbo.vwCOADetail v WHERE (v.main_type = 'Expense')")
            Me.cmbCSG.SelectedIndex = id


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub GridEX1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridEX1.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lblHeader.Click

    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Article Department"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class