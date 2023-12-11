Imports System.Data.OleDb

Public Class frmDefEmpDesignation

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If Not Me.DataGridView1.GetRow Is Nothing Then
            Me.editrecord()
        End If
    End Sub
    Dim CurrentId As Integer
    Dim IsLoadedForm As Boolean = False

#Region "Local Function & Procedures"

    Sub RefreshForm()
        Me.BtnSave.Text = "&Save"
        Me.uitxtName.Text = ""
        Me.uitxtName.Focus()
        Me.uitxtComments.Text = ""
        Me.uitxtSortOrder.Text = 1
        Me.uichkActive.Checked = True
        Me.BindGrid()
        Me.GetSecurityRights()
    End Sub

    Sub BindGrid()
        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable

        adp = New OleDb.OleDbDataAdapter("SELECT  EmployeeDesignationId,EmployeeDesignationName,Comments,Active,SortOrder FROM EmployeeDesignationDefTable Order By SortOrder", Con)
        adp.Fill(dt)
        Me.DataGridView1.DataSource = dt
        Me.DataGridView1.RetrieveStructure()
        Me.DataGridView1.RootTable.Columns(0).Visible = False
    End Sub

    Sub EditRecord()
        Me.uitxtName.Text = DataGridView1.CurrentRow.Cells("EmployeeDesignationName").Value.ToString
        Me.uitxtComments.Text = DataGridView1.CurrentRow.Cells("Comments").Value.ToString
        Me.uitxtSortOrder.Text = DataGridView1.CurrentRow.Cells("SortOrder").Value.ToString
        Me.uichkActive.Checked = IIf(CType(DataGridView1.CurrentRow.Cells("Active").Value.ToString, Boolean) = False, False, True)
        Me.CurrentId = Me.DataGridView1.CurrentRow.Cells(0).Value.ToString
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

    Private Sub frmDefEmpDesignation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

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
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Me.RefreshForm()
            IsLoadedForm = True
            Get_All(frmModProperty.Tags)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally

            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        End Try
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

        If Me.uitxtName.Text = "" Then
            ShowErrorMessage("Please enter valid designation")
            Me.uitxtName.Focus()
            Exit Sub
        End If

        ' If Not msg_Confirm(str_ConfirmSave) = True Then
        Me.uitxtName.Focus()
        'Exit Sub
        '  End If


        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con
        Try
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "&Save" Then
                cm.CommandText = "INSERT INTO EmployeeDesignationDefTable (EmployeeDesignationName,Comments,Active,SortOrder,IsDate) " & _
                    "Values (N'" & Me.uitxtName.Text & "',N'" & Me.uitxtComments.Text & "'," & IIf(Me.uichkActive.Checked, 1, 0) & _
                    ",N'" & Me.uitxtSortOrder.Text & "',GetDate()) Select @@Identity"
            Else
                cm.CommandText = "Update EmployeeDesignationDefTable Set EmployeeDesignationName=N'" & Me.uitxtName.Text & "',Comments=N'" & Me.uitxtComments.Text & _
                    "',Active=" & IIf(Me.uichkActive.Checked, 1, 0) & ",SortOrder=N'" & Me.uitxtSortOrder.Text & "' Where EmployeeDesignationId=" & Me.CurrentId
            End If

            Dim identity As Integer = Convert.ToInt32(cm.ExecuteScalar())
            'MsgBox("Record Saved Successfully", MsgBoxStyle.Information, str_MessageHeader)
            ' msg_Information(str_informSave)

            Try
                ''insert Activity Log
                SaveActivityLog("Config", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", identity, Me.CurrentId), True)
            Catch ex As Exception

            End Try

            Me.CurrentId = 0
        Catch ex As Exception
            ShowErrorMessage("Error occured while saving record: " & ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
        End Try
        Me.RefreshForm()
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not DataGridView1.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If IsValidToDelete("tblDefEmployee", "Desig_ID", Me.DataGridView1.CurrentRow.Cells("EmployeeDesignationId").Value.ToString) = True Then
            If msg_Confirm(str_ConfirmDelete) = True Then
                Try
                    Dim cm As New OleDbCommand

                    'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "Delete from EmployeeDesignationDefTable where EmployeeDesignationId=" & Me.DataGridView1.CurrentRow.Cells("EmployeeDesignationId").Value.ToString
                    cm.ExecuteNonQuery()
                    'msg_Information(str_informDelete)
                    Me.CurrentId = 0

                    Try
                        ''insert Activity Log
                        SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.DataGridView1.CurrentRow.Cells("EmployeeDesignationId").Value.ToString, True)
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
    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From EmployeeDesignationDefTable WHERE EmployeeDesignationId=N'" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.uitxtName.Text = dt.Rows(0).Item("EmployeeDesignationName").ToString 'DataGridView1.CurrentRow.Cells("CityName").Value
                        Me.uitxtComments.Text = dt.Rows(0).Item("Comments").ToString 'DataGridView1.CurrentRow.Cells("Comments").Value.ToString
                        Me.uitxtSortOrder.Text = dt.Rows(0).Item("SortOrder").ToString 'DataGridView1.CurrentRow.Cells("SortOrder").Value
                        Me.uichkActive.Checked = dt.Rows(0).Item("Active") 'IIf(DataGridView1.CurrentRow.Cells("Active").Value = 0, False, True)
                        Me.CurrentId = dt.Rows(0).Item("EmployeeDesignationId") 'Me.DataGridView1.CurrentRow.Cells(0).Value
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
End Class