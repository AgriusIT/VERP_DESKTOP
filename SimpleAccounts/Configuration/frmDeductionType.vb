Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class frmDeductionType
    Implements IGeneral
    Private _DeductionTypeId As Integer = 0
    Private _DeductionType As DeductionTypeBE
    Private IsEditMode As Boolean = False
    Private dtEmpData As DataTable
    Private _DeductionDetail As List(Of EmployeesDeductionDetail)
    Enum enmEmp
        EmpId
        EmpCode
        EmpName
        Designation
        Department
        Count
    End Enum
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdEmployeesDeduction.RootTable.Columns(0).Visible = False

            For col As Integer = enmEmp.Count To Me.grdEmployeesDeduction.RootTable.Columns.Count - 2 Step 2
                Me.grdEmployeesDeduction.RootTable.Columns(col).Visible = False
                Me.grdEmployeesDeduction.RootTable.Columns(col + 1).AllowSort = False
            Next
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdEmployeesDeduction.RootTable.Columns
                If col.Index < enmEmp.Count Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            Me.grdEmployeesDeduction.AutoSizeColumns()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If Condition = "DeductionType" Then
                If IsValidate() = True Then
                    If New DeductionTypeDAL().Delete(_DeductionType) = True Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim Str As String = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                            "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as TypeId, tblCustomer.Email, tblCustomer.Phone " & _
                                            "FROM dbo.tblCustomer INNER JOIN " & _
                                            "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                            "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                            "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                            "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id WHERE vwcoadetail.detail_title is not null"
            If IsEditMode = False Then
                Str += " AND vwCOADetail.Active=1"
            Else
                Str += " AND vwCOADetail.Active in(0,1,Null)"
            End If
            Str += "order by tblCustomer.Sortorder, vwCOADetail.detail_title"
            FillUltraDropDown(Me.cmbAccounts, Str)
            If Me.cmbAccounts.DisplayLayout.Bands.Count > 0 Then
                Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbAccounts.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbAccounts.DisplayLayout.Bands(0).Columns("TypeId").Hidden = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            If Condition = "DeductionType" Then
                _DeductionType = New DeductionTypeBE
                _DeductionType.DeductionTypeId = _DeductionTypeId
                _DeductionType.DeductionType = Me.txtDeductionType.Text.ToString.Replace("'", "''")
                _DeductionType.AccountId = Me.cmbAccounts.ActiveRow.Cells(0).Value
                _DeductionType.Active = Me.chkActive.Checked
            ElseIf Condition = "EmployeesDeduction" Then
                _DeductionDetail = New List(Of EmployeesDeductionDetail)
                Dim empDeduction As EmployeesDeductionDetail
                Dim dt As DataTable = CType(Me.grdEmployeesDeduction.DataSource, DataTable)
                Dim dblDeductionAmount As Double = 0D

                For Each row As DataRow In dt.Rows
                    For c As Integer = enmEmp.Count To Me.grdEmployeesDeduction.RootTable.Columns.Count - 2 Step 2
                        empDeduction = New EmployeesDeductionDetail
                        empDeduction.DeductionDetailId = 0
                        empDeduction.DeductionTypeId = row(c)
                        empDeduction.EmployeeId = row("EmpId")
                        Double.TryParse(row(c + 1).ToString, dblDeductionAmount)
                        empDeduction.Deduction_Amount = dblDeductionAmount
                        _DeductionDetail.Add(empDeduction)
                    Next
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            If Condition = "EmployeesDeduction" Then
                Me.grdEmployeesDeduction.DataSource = dtEmpData
                Me.grdEmployeesDeduction.RetrieveStructure()
                ApplyGridSettings()
            ElseIf Condition = "DeductionType" Then
                Dim dt As DataTable = New DeductionTypeDAL().GetAll
                Me.grd.DataSource = dt
                Me.grd.AutoSizeColumns()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.txtDeductionType.Text = String.Empty Then
                ShowErrorMessage("Please enter deduction type")
                Me.txtDeductionType.Focus()
                Return False
            End If
            If Me.cmbAccounts.ActiveRow.Cells(0).Value = 0 Then
                ShowErrorMessage("Please select any deduction account")
                Me.cmbAccounts.Focus()
                Return False
            End If

            FillModel("DeductionType")

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnSave.Text = "&Save"
            _DeductionTypeId = 0
            IsEditMode = False
            Me.txtDeductionType.Text = String.Empty
            Me.cmbAccounts.Rows(0).Activate()
            Me.chkActive.Checked = True
            GetAllRecords("DeductionType")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If Condition = "DeductionType" Then
                If IsValidate() = True Then
                    If New DeductionTypeDAL().Add(_DeductionType) = True Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            ElseIf Condition = "EmployeesDeduction" Then
                FillModel("EmployeesDeduction")
                If New EmployeesDeductionDetailDAL().Add(_DeductionDetail) = True Then
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
        Try
            If Condition = "DeductionType" Then
                If IsValidate() = True Then
                    If New DeductionTypeDAL().Update(_DeductionType) = True Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmDeductionType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

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
    Private Sub frmAllownaceType_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False

        End Try
    End Sub
    Private Sub EditRecords()
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            IsEditMode = True
            FillCombos()
            _DeductionTypeId = Me.grd.GetRow.Cells("DeductionTypeId").Value
            Me.txtDeductionType.Text = Me.grd.GetRow.Cells("DeductionType").Value
            Me.cmbAccounts.Value = Me.grd.GetRow.Cells("AccountId").Value
            Me.chkActive.Checked = Me.grd.GetRow.Cells("Active").Value
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.btnSave.Text = "&Update"
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
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If UltraTabControl1.SelectedTab.Index = 0 Then
                If Me.btnSave.Text = "&Save" Then

                    'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()

                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save("DeductionType") = True Then
                        'msg_Information(str_informSave)
                        ReSetControls()
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub

                    'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()

                    If Update1("DeductionType") = True Then
                        ' msg_Information(str_informSave)
                        ReSetControls()
                    End If
                End If
            Else
                If Save("EmployeesDeduction") = True Then
                    msg_Information(str_informSave)
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage("Error occured while saving record: " & ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If _DeductionTypeId = 0 Then
                ShowErrorMessage("can not delete")
                Me.txtDeductionType.Focus()
                Exit Sub
            End If
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

            'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            If Delete("DeductionType") = True Then
                'msg_Information(str_informSave)
                ReSetControls()
            End If
        Catch ex As Exception
        Finally
            Me.lblProgress.Visible = False

        End Try
    End Sub
    Private Sub grd_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage("Error occured while edting record: " & ex.Message)
        End Try
    End Sub
    Private Function FillEmpDeduction() As DataTable
        Try
            dtEmpData = New DeductionTypeDAL().EmployeesData
            Dim dtDeductionTypeData As DataTable = New DeductionTypeDAL().DeductionTypeData
            For Each r As DataRow In dtDeductionTypeData.Rows
                If Not dtEmpData.Columns.Contains(r.Item(0)) Then
                    dtEmpData.Columns.Add(r(0), GetType(System.Int32), r(0))
                    dtEmpData.Columns.Add(r(1), GetType(System.String))
                End If
            Next
            Dim EmpDeductionData As DataTable = New EmployeesDeductionDetailDAL().GetAll
            Dim dr() As DataRow
            For Each row As DataRow In dtEmpData.Rows
                dr = EmpDeductionData.Select("EmployeeId='" & row.Item(0) & "'")
                If dr.Length > 0 Then
                    For Each drfound As DataRow In dr
                        row.BeginEdit()
                        row(dtEmpData.Columns.IndexOf(drfound(1)) + 1) = drfound(2)
                        row.EndEdit()
                    Next
                End If
            Next
            dtEmpData.AcceptChanges()
            Return dtEmpData
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                If BackgroundWorker1.IsBusy Then Exit Sub
                BackgroundWorker1.RunWorkerAsync()
                Do While BackgroundWorker1.IsBusy
                    Application.DoEvents()
                Loop
                Me.btnSave.Text = "&Save"
                Me.btnEdit.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnNew.Enabled = False
                GetAllRecords("EmployeesDeduction")
            Else
                Me.btnEdit.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnNew.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            FillEmpDeduction()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
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
End Class