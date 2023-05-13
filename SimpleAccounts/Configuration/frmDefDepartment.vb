Imports System.Data.OleDb


Public Class frmDefDepartment
    Implements IGeneral

    Dim CurrentId As Integer
    Dim IsLoadedForm As Boolean = False
    Dim DeptAccountHeadId As Integer = 0I
    Dim DeptAccountMainId As Integer = 0I
#Region "Local Function & Procedures"

    Sub RefreshForm()
        Me.BtnSave.Text = "&Save"
        DeptAccountHeadId = 0I
        Me.CurrentId = 0I
        Me.txtAccountDescription.Text = String.Empty
        Me.uitxtName.Text = ""
        Me.uitxtName.Focus()
        Me.uitxtComments.Text = ""
        Me.uitxtSortOrder.Text = 1
        Me.uichkActive.Checked = True
        Me.txtSalaryExpense.Tag = String.Empty
        Me.txtSalaryExpense.Text = String.Empty
        If getConfigValueByType("EmployeeDeptHeadAccountId").ToString <> "Error" Then
            DeptAccountMainId = getConfigValueByType("EmployeeDeptHeadAccountId")
        End If
        Me.BindGrid()
        Me.GetSecurityRights()
    End Sub

    Sub BindGrid()
        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable

        adp = New OleDb.OleDbDataAdapter("SELECT EmployeeDeptId,EmployeeDeptName,EmployeeDeptDefTable.Comments,DeptAccountHeadId, COAMain.sub_sub_title as [Account Head], coa.detail_title as [Salary Exp Ac], EmployeeDeptDefTable.Active,EmployeeDeptDefTable.SortOrder, isnull(SalaryExpAcID,0) as SalaryExpAcID FROM EmployeeDeptDefTable LEFT OUTER JOIN tblCOAMainSubSub COAMain on COAMain.main_sub_sub_Id = EmployeeDeptDefTable.DeptAccountHeadId LEFT OUTER JOIN vwCOADetail coa on coa.coa_detail_id = EmployeeDeptDefTable.SalaryExpAcId  Order By EmployeeDeptDefTable.SortOrder", Con)
        adp.Fill(dt)
        Me.DataGridView1.DataSource = dt
        Me.DataGridView1.RetrieveStructure()
        Me.DataGridView1.RootTable.Columns(0).Visible = False
        Me.DataGridView1.RootTable.Columns("DeptAccountHeadId").Visible = False
        Me.DataGridView1.RootTable.Columns("SalaryExpAcID").Visible = False
    End Sub

    Sub EditRecord()
        Me.uitxtName.Text = DataGridView1.CurrentRow.Cells("EmployeeDeptName").Value.ToString
        Me.uitxtComments.Text = DataGridView1.CurrentRow.Cells("Comments").Value.ToString
        Me.uitxtSortOrder.Text = DataGridView1.CurrentRow.Cells("SortOrder").Value.ToString
        Me.uichkActive.Checked = IIf(CType(DataGridView1.CurrentRow.Cells("Active").Value.ToString, Boolean) = False, False, True)
        Me.CurrentId = Me.DataGridView1.CurrentRow.Cells(0).Value.ToString
        Me.DeptAccountHeadId = Val(DataGridView1.CurrentRow.Cells("DeptAccountHeadId").Value.ToString)
        Me.txtAccountDescription.Text = DataGridView1.CurrentRow.Cells("Account Head").Value.ToString
        Me.txtSalaryExpense.Tag = Val(DataGridView1.CurrentRow.Cells("SalaryExpAcID").Value.ToString)
        Me.txtSalaryExpense.Text = DataGridView1.CurrentRow.Cells("Salary Exp Ac").Value.ToString
        Me.BtnSave.Text = "&Update"
        Me.GetSecurityRights()
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefColor)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
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

#End Region

    Private Sub frmDefDepartment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
            If e.KeyCode = Keys.F4 Then
                SaveToolStripButton_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub frmDefDepartment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()



        Me.RefreshForm()
        IsLoadedForm = True
        Get_All(frmModProperty.Tags)

        Me.lblProgress.Visible = False
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        If Not Me.DataGridView1.GetRow Is Nothing Then
            Me.EditRecord()

        End If
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.RefreshForm()
    End Sub
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim DeptName As String = String.Empty
        If Me.uitxtName.Text = "" Then
            ShowErrorMessage("Please enter valid department")
            Me.uitxtName.Focus()
            Exit Sub
        End If

        'If Not msg_Confirm(str_ConfirmSave) Then
        Me.uitxtName.Focus()
        'Exit Sub
        'End If

        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 
        If DeptAccountMainId = 0 Then
            ShowErrorMessage("Sub account must be configured from configuration")
            Exit Sub
        End If

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()




        Dim cm As New OleDbCommand
        Dim identity As Integer = 0I
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        cm.Connection = Con
        cm.Transaction = trans
        cm.CommandTimeout = 120

        Try
            If IsValidate() = True Then
                If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "&Save" Then

                    cm.CommandText = "INSERT INTO EmployeeDeptDefTable (EmployeeDeptName,Comments,Active,SortOrder,IsDate, SalaryExpAcID) " & _
                        "Values (N'" & Me.uitxtName.Text.ToString.Replace("'", "''") & "',N'" & Me.uitxtComments.Text.ToString.Replace("'", "''") & "'," & IIf(Me.uichkActive.Checked, 1, 0) & _
                        ",N'" & Me.uitxtSortOrder.Text & "',GetDate(),'" & Me.txtSalaryExpense.Tag.ToString & "') Select @@Identity"
                    identity = Convert.ToInt32(cm.ExecuteScalar())
                    If getConfigValueByType("EmpDepartmentAccountHead").ToString = "True" Then
                        If DeptAccountHeadId = 0 Then
                            cm.CommandText = ""

                            DeptName = Me.uitxtName.Text.Replace("'", "''") & " Salary Payable"
                            'cm.CommandText = "INSERT INTO tblCOAMainSubSub(Main_Sub_Id,Sub_Sub_Code,Sub_Sub_Title,Account_Type,DrBS_Note_Id,CrBS_Note_Id,PL_Note_Id) " _
                            '& " VALUES(" & DeptAccountMainId & ",'" & GetSubSubCode(DeptAccountMainId, trans) & "','" & Me.uitxtName.Text.Replace("'", "''") & "','General',0,0,0)Select @@Identity"
                            cm.CommandText = "INSERT INTO tblCOAMainSubSub(Main_Sub_Id,Sub_Sub_Code,Sub_Sub_Title,Account_Type,DrBS_Note_Id,CrBS_Note_Id,PL_Note_Id) " _
                            & " VALUES(" & DeptAccountMainId & ",'" & GetSubSubCode(DeptAccountMainId, trans) & "','" & DeptName & "','General',0,0,0)Select @@Identity"
                            Dim SubSubAcId As Integer = cm.ExecuteScalar()


                            cm.CommandText = ""
                            cm.CommandText = "Update EmployeeDeptDefTable Set DeptAccountHeadId=" & SubSubAcId & " WHERE EmployeeDeptID=" & identity & ""
                            cm.ExecuteNonQuery()

                        Else

                            cm.CommandText = ""
                            cm.CommandText = "Update EmployeeDeptDefTable Set DeptAccountHeadId=" & DeptAccountMainId & " WHERE EmployeeDeptID=" & identity & ""
                            cm.ExecuteNonQuery()

                        End If
                    End If

                Else

                    cm.CommandText = "Update EmployeeDeptDefTable Set EmployeeDeptName=N'" & Me.uitxtName.Text.ToString.Replace("'", "''") & "',Comments=N'" & Me.uitxtComments.Text.ToString.Replace("'", "''") & _
                        "',Active=" & IIf(Me.uichkActive.Checked, 1, 0) & ",SortOrder=N'" & Me.uitxtSortOrder.Text.ToString.Replace("'", "''") & "', SalaryExpAcID='" & Me.txtSalaryExpense.Tag.ToString & "' Where EmployeeDeptID=" & Me.CurrentId
                    cm.ExecuteNonQuery()

                    If getConfigValueByType("EmpDepartmentAccountHead").ToString = "True" Then
                        If DeptAccountHeadId = 0 Then
                            DeptName = Me.uitxtName.Text.Replace("'", "''") & " Salary Payable"
                            cm.CommandText = ""
                            'cm.CommandText = "INSERT INTO tblCOAMainSubSub(Main_Sub_Id,Sub_Sub_Code,Sub_Sub_Title,Account_Type,DrBS_Note_Id,CrBS_Note_Id,PL_Note_Id) " _
                            '& " VALUES(" & DeptAccountMainId & ",'" & GetSubSubCode(DeptAccountMainId, trans) & "','" & Me.uitxtName.Text.Replace("'", "''") & "','General',0,0,0) Select @@Identity"
                            cm.CommandText = "INSERT INTO tblCOAMainSubSub(Main_Sub_Id,Sub_Sub_Code,Sub_Sub_Title,Account_Type,DrBS_Note_Id,CrBS_Note_Id,PL_Note_Id) " _
                            & " VALUES(" & DeptAccountMainId & ",'" & GetSubSubCode(DeptAccountMainId, trans) & "','" & DeptName & "','General',0,0,0) Select @@Identity"
                            Dim SubSubAcId As Integer = cm.ExecuteScalar()

                            cm.CommandText = ""
                            cm.CommandText = "Update EmployeeDeptDefTable Set DeptAccountHeadId=" & SubSubAcId & " WHERE EmployeeDeptID=" & Me.CurrentId & ""
                            cm.ExecuteNonQuery()
                        Else
                            cm.CommandText = ""
                            cm.CommandText = "Select * From EmployeeDeptDefTable WHERE DeptAccountHeadId=" & DeptAccountHeadId & " and EmployeeDeptID=" & Me.CurrentId & ""
                            Dim int As Integer = cm.ExecuteScalar
                            If int = 0 Then
                                cm.CommandText = ""
                                cm.CommandText = "Update EmployeeDeptDefTable Set DeptAccountHeadId=" & DeptAccountHeadId & " WHERE EmployeeDeptID=" & Me.CurrentId & ""
                                cm.ExecuteNonQuery()
                            End If
                        End If
                    End If
                End If


                ' MsgBox("Record Saved Successfully", MsgBoxStyle.Information, str_MessageHeader)
                'msg_Information(str_informSave)
                trans.Commit()
                Me.RefreshForm()
            End If
            Try
                ''insert Activity Log

            Catch ex As Exception

            End Try

            Me.CurrentId = 0
        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage("Error occured while saving record: " & ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
        End Try
        SaveActivityLog("Config", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", identity, Me.CurrentId), True)

    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not DataGridView1.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If IsValidToDelete("tblDefEmployee", "Dept_ID", Me.DataGridView1.CurrentRow.Cells("EmployeeDeptId").Value.ToString) = True Then
            If msg_Confirm(str_ConfirmDelete) = True Then

                'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                Try
                    Dim cm As New OleDbCommand

                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "Delete from EmployeeDeptDefTable where EmployeeDeptId=" & Me.DataGridView1.CurrentRow.Cells("EmployeeDeptId").Value.ToString
                    cm.ExecuteNonQuery()
                    'msg_Information(str_informDelete)
                    Me.CurrentId = 0

                    Try
                        ''insert Activity Log
                        SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.DataGridView1.CurrentRow.Cells("EmployeeDeptId").Value.ToString, True)
                    Catch ex As Exception

                    End Try

                Catch ex As Exception
                    msg_Error("Error occured while deleting record: " & ex.Message)
                Finally
                    Con.Close()
                    Me.lblProgress.Visible = False
                End Try
                Me.RefreshForm()


            End If
        Else
            msg_Error(str_ErrorDependentRecordFound)
        End If
    End Sub
    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If Not Me.DataGridView1.GetRow Is Nothing Then
            Me.EditRecord()
        End If
    End Sub
    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From EmployeeDeptDefTable WHERE EmployeeDeptId=N'" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.uitxtName.Text = dt.Rows(0).Item("EmployeeDeptName").ToString 'DataGridView1.CurrentRow.Cells("CityName").Value
                        Me.uitxtComments.Text = dt.Rows(0).Item("Comments").ToString 'DataGridView1.CurrentRow.Cells("Comments").Value.ToString
                        Me.uitxtSortOrder.Text = Val(dt.Rows(0).Item("SortOrder").ToString) 'DataGridView1.CurrentRow.Cells("SortOrder").Value
                        Me.uichkActive.Checked = dt.Rows(0).Item("Active") 'IIf(DataGridView1.CurrentRow.Cells("Active").Value = 0, False, True)
                        Me.CurrentId = dt.Rows(0).Item("EmployeeDeptId") 'Me.DataGridView1.CurrentRow.Cells(0).Value
                        Me.DeptAccountHeadId = Val(dt.Rows(0).Item("DeptAccountHeadId").ToString)
                        Me.txtAccountDescription.Text = dt.Rows(0).Item("Account Head").ToString
                        Me.BtnSave.Text = "&Update"
                        Me.GetSecurityRights()
                        IsDrillDown = True
                    End If
                End If
                IsDrillDown = False
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            DeleteToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            ApplyStyleSheet(frmAddAccount)
            frmAddAccount.AccountType = "SubSubAccount"
            If frmAddAccount.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If frmAddAccount.AccountId > 0 Then
                    DeptAccountHeadId = frmAddAccount.AccountId
                    Me.txtAccountDescription.Tag = frmAddAccount.AccountId
                    Me.txtAccountDescription.Text = frmAddAccount.AccountDesc
                    frmAddAccount.AccountId = 0
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function GetSubSubCode(ByVal MainAcId As Integer, Optional ByVal trans As OleDb.OleDbTransaction = Nothing) As String
        Try

            Dim dt As New DataTable
            Dim strSubCode As String = String.Empty
            Dim strsubSubCode As String = String.Empty
            dt = GetDataTable("Select Sub_Code from tblCOAMainSub WHERE Main_Sub_Id=" & MainAcId & "", trans)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                strSubCode = dt.Rows(0).Item(0).ToString
            End If

            Dim dtSerial As New DataTable
            dtSerial = GetDataTable("Select IsNull(Max(Right(Sub_Sub_Code,3)),0)+1 Cont From tblCOAMainSubSub WHERE Left(sub_sub_code," & strSubCode.Length & ")='" & strSubCode & "' AND main_sub_id=" & MainAcId & "", trans)
            dtSerial.AcceptChanges()

            If dtSerial.Rows.Count > 0 Then
                strsubSubCode = strSubCode & "-" & CStr(Microsoft.VisualBasic.Right("000" & CStr(dtSerial.Rows(0).Item(0).ToString), 3))
            End If


            Return strsubSubCode

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

        '''''''''''''''''''''''''''''''''''''''''''''
        Dim a As Integer
        Try
            If Me.BtnSave.Text = "Update" Or Me.BtnSave.Text = "&Update" Then
                For a = 1 To DataGridView1.RowCount - 1
                    If DataGridView1.GetRow(a).Cells("EmployeeDeptName").Value.ToString.ToUpper = (Me.uitxtName.Text).ToUpper And Convert.ToInt32(DataGridView1.GetRow(a).Cells("EmployeeDeptId").Value.ToString) <> Me.CurrentId Then
                        ShowErrorMessage("Department with same name is already registered")
                        Return False
                    End If
                Next
            Else


                For a = 1 To DataGridView1.RowCount - 1
                    If DataGridView1.GetRow(a).Cells("EmployeeDeptName").Value.ToString.ToUpper = (Me.uitxtName.Text).ToUpper Then
                        ShowErrorMessage("Department with same name is already registered")
                        Return False
                    End If
                Next
            End If


            If Me.BtnSave.Text = "Update" Or Me.BtnSave.Text = "&Update" Then
                For a = 1 To DataGridView1.RowCount - 1
                    If DataGridView1.GetRow(a).Cells("Account Head").Value.ToString.ToUpper = (Me.uitxtName.Text).ToUpper & " SALARY PAYABLE" Then
                        ShowErrorMessage("Account Head with same name is already registered")
                        Return False
                    End If
                Next
            Else


                For a = 1 To DataGridView1.RowCount - 1
                    If DataGridView1.GetRow(a).Cells("Account Head").Value.ToString.ToUpper = (Me.uitxtName.Text).ToUpper & " SALARY PAYABLE" Then
                        ShowErrorMessage("Account Head with same name is already registered")
                        Return False
                    End If
                Next
            End If
            ''    '''''''''''''''''''''''''''''''''''''''''''''

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnAddSalaryExpenseAc_Click(sender As Object, e As EventArgs) Handles btnAddSalaryExpenseAc.Click
        Try
            ApplyStyleSheet(frmAddAccount)
            frmAddAccount.AccountType = String.Empty
            If frmAddAccount.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If frmAddAccount.AccountId > 0 Then
                    Me.txtSalaryExpense.Tag = frmAddAccount.AccountId
                    Me.txtSalaryExpense.Text = frmAddAccount.AccountDesc
                    frmAddAccount.AccountId = 0
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class