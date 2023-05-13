Imports System.Data.OleDb
Public Class frmDefCategory
    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If Not Me.DataGridView1.GetRow Is Nothing Then
            Me.editrecord()
        End If
    End Sub
    Dim CurrentId As Integer
    Dim IsLoadedForm As Boolean = False
    Sub RefreshForm()
        Me.BtnSave.Text = "&Save"
        Me.uitxtName.Text = ""
        Me.txtCateCode.Text = String.Empty
        Me.uitxtName.Focus()
        Me.uitxtComments.Text = ""
        Me.uitxtSortOrder.Text = 1
        Me.uichkActive.Checked = True
        FillCombo()
        Me.BindGrid()
        GetSecurityRights()
    End Sub

    Sub BindGrid()
        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable

        adp = New OleDbDataAdapter("SELECT ArticleCompanyId,IsNull(ArticleTypeDefTable.ArticleTypeId,0) As ArticleTypeId,IsNull(ArticleTypeDefTable.ArticleTypeName,'') As Type, ArticleCompanyName, CategoryCode, ArticleCompanyDefTable.Comments, ArticleCompanyDefTable.Active, ArticleCompanyDefTable.SortOrder FROM         ArticleCompanyDefTable Left Outer Join ArticleTypeDefTable on ArticleTypeDefTable.ArticleTypeId = ArticleCompanyDefTable.ArticleTypeId  order by ArticleCompanyDefTable.sortorder ", Con)
        adp.Fill(dt)
        Me.DataGridView1.DataSource = dt
        Me.DataGridView1.RetrieveStructure()
        Me.DataGridView1.RootTable.Columns(0).Visible = False
        Me.DataGridView1.RootTable.Columns(1).Visible = False ''TFS4884
        For c As Integer = 0 To Me.DataGridView1.RootTable.Columns.Count - 1
            DataGridView1.RootTable.Columns(c).FilterEditType = Janus.Windows.GridEX.EditType.TextBox
        Next
    End Sub
    Sub FillCombo()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "select ArticleTypeId as Id, ArticleTypeName as Name, TypeCode from ArticleTypeDefTable where active=1 order by sortOrder"
            FillUltraDropDown(cmbType, strSQL)
            Me.cmbType.Rows(0).Activate()
            Me.cmbType.DisplayLayout.Bands(0).Columns("Id").Hidden = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub EditRecord()
        Me.uitxtName.Text = DataGridView1.CurrentRow.Cells("ArticleCompanyName").Value.ToString
        Me.uitxtComments.Text = DataGridView1.CurrentRow.Cells("Comments").Value.ToString
        Me.cmbType.Value = Val(DataGridView1.CurrentRow.Cells("ArticleTypeId").Value.ToString) ''TFS4884
        Me.uitxtSortOrder.Text = Val(DataGridView1.CurrentRow.Cells("SortOrder").Value.ToString)
        Me.uichkActive.Checked = IIf(DataGridView1.CurrentRow.Cells("Active").Value = 0, False, True)
        Me.txtCateCode.Text = DataGridView1.GetRow.Cells("CategoryCode").Value.ToString
        Me.CurrentId = Val(Me.DataGridView1.CurrentRow.Cells(0).Value.ToString)
        Me.BtnSave.Text = "&Update"
        GetSecurityRights()
    End Sub

    Private Sub frmDefCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 11 -1-14
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
    Private Sub frmDefGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

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
        If Me.uitxtName.Text = "" Then
            'MsgBox("Please enter valid Group", MsgBoxStyle.Critical, str_MessageHeader)
            ShowErrorMessage("Please enter valid group")
            Me.uitxtName.Focus()
            Exit Sub
        End If

        'If MsgBox("Do you want to save ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, str_MessageHeader) = MsgBoxResult.No Then
        If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "&Save" Then
            If Not msg_Confirm(str_ConfirmSave) = True Then
                Me.uitxtName.Focus()
                Exit Sub
            End If
        Else
            If Not msg_Confirm(str_ConfirmUpdate) = True Then
                Me.uitxtName.Focus()
                Exit Sub
            End If
        End If

        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            Dim str As String = "Select Count(*), ArticleCompanyName,CategoryCode From ArticleCompanyDefTable WHERE (CategoryCode=N'" & Me.txtCateCode.Text.Replace("'", "''") & "' Or ArticleCompanyName=N'" & Me.uitxtName.Text.Replace("'", "''") & "') AND ArticleCompanyId <> " & Me.CurrentId & " GROUP BY ArticleCompanyName,CategoryCode"
            Dim dt As New DataTable
            dt = GetDataTable(str)
            dt.AcceptChanges()

            If dt.HasErrors = False Then
                If dt.Rows.Count > 0 Then
                    If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                        If Me.uitxtName.Text.ToUpper = dt.Rows(0).Item(1).ToString.ToUpper Then
                            Throw New Exception("[Category: " & dt.Rows(0).Item(1).ToString.Replace("'", "''") & "] is already exist")
                        ElseIf Me.txtCateCode.Text.ToUpper = dt.Rows(0).Item(2).ToString.ToUpper Then
                            Throw New Exception("[Category Code: " & dt.Rows(0).Item(2).ToString.Replace("'", "''") & "] is already exist")
                        End If
                    End If
                End If
            End If

            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "&Save" Then
                cm.CommandText = "insert into ArticleCompanyDefTable(ArticleCompanyName,CategoryCode, Comments,sortorder, Active,ArticleTypeId ) values(N'" & Me.uitxtName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtCateCode.Text.Replace("'", "''") & "',N'" & Me.uitxtComments.Text.ToString.Replace("'", "''") & "',N'" & Me.uitxtSortOrder.Text.ToString.Replace("'", "''") & "'," & IIf(Me.uichkActive.Checked = False, 0, 1) & "," & cmbType.Value & ")Select @@Identity"
            Else
                cm.CommandText = "update ArticleCompanyDefTable set ArticleCompanyName=N'" & Me.uitxtName.Text.ToString.Replace("'", "''") & "', CategoryCode=N'" & Me.txtCateCode.Text.Replace("'", "''") & "',Comments=N'" & Me.uitxtComments.Text.ToString.Replace("'", "''") & "', sortorder=N'" & Me.uitxtSortOrder.Text.ToString.Replace("'", "''") & "',Active=" & IIf(Me.uichkActive.Checked = False, 0, 1) & ",ArticleTypeId=" & cmbType.Value & " where ArticleCompanyId=" & Me.CurrentId
            End If
            Dim identity As Integer = Convert.ToInt32(cm.ExecuteScalar())
            'MsgBox("Record Saved Successfully", MsgBoxStyle.Information, str_MessageHeader)
            '   msg_Information(str_informSave)

            Try
                ''insert Activity Log
                SaveActivityLog("Config", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", identity, Me.CurrentId), True)
            Catch ex As Exception

            End Try

            Me.CurrentId = 0



        Catch ex As Exception
            'MsgBox("Error occured while saving record: " & ex.Message)
            ShowErrorMessage("Error occured while saving record " & ex.Message)
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
        If IsValidToDelete("ArticleLPODefTable", "ArticleCompanyId", Me.DataGridView1.CurrentRow.Cells("ArticleCompanyId").Value.ToString) = True Then
            If msg_Confirm(str_ConfirmDelete) = True Then
                Try

                    'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    Dim cm As New OleDbCommand
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "delete from ArticleCompanyDefTable where ArticleCompanyId=" & Me.DataGridView1.CurrentRow.Cells("ArticleCompanyId").Value.ToString
                    cm.ExecuteNonQuery()
                    'msg_Information(str_informDelete)
                    Me.CurrentId = 0

                    Try
                        ''insert Activity Log
                        SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.DataGridView1.CurrentRow.Cells("ArticleCompanyId").Value.ToString, True)
                    Catch ex As Exception
                    Finally
                        Me.lblProgress.Visible = False
                    End Try


                Catch ex As Exception
                    msg_Error("Error occured while deleting record " & ex.Message)
                Finally
                    Con.Close()
                End Try
                Me.RefreshForm()
            End If
        Else
            msg_Error(str_ErrorDependentRecordFound)
        End If
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
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefCompany)
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
    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From ArticleCompanyDefTable WHERE ArticleCompanyId=N'" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.uitxtName.Text = dt.Rows(0).Item("ArticleCompanyName").ToString 'DataGridView1.CurrentRow.Cells("ArticleCompanyName").Value
                        Me.uitxtComments.Text = dt.Rows(0).Item("Comments").ToString 'DataGridView1.CurrentRow.Cells("Comments").Value.ToString
                        Me.uitxtSortOrder.Text = dt.Rows(0).Item("SortOrder").ToString 'DataGridView1.CurrentRow.Cells("SortOrder").Value
                        Me.uichkActive.Checked = dt.Rows(0).Item("Active") 'IIf(DataGridView1.CurrentRow.Cells("Active").Value = 0, False, True)
                        Me.cmbType.Value = Val(dt.Rows(0).Item("ArticleTypeId").ToString)
                        Me.CurrentId = dt.Rows(0).Item("ArticleCompanyId") 'Me.DataGridView1.CurrentRow.Cells(0).Value
                        Me.txtCateCode.Text = dt.Rows(0).Item("CategoryCode").ToString
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
        'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14
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