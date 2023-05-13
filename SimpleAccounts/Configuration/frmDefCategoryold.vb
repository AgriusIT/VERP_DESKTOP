Imports System.Data.OleDb
Public Class frmDefCategoryold
    Dim CurrentId As Integer

    Sub RefreshForm()
        Me.BtnSave.Text = "&Save"
        Me.uitxtName.Text = ""
        Me.uitxtName.Focus()
        Me.uitxtComments.Text = ""
        Me.uitxtSortOrder.Text = 1
        Me.uichkActive.Checked = True
        Me.BindGrid()
        GetSecurityRights()
    End Sub
    Sub BindGrid()
        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable

        adp = New OleDbDataAdapter("SELECT ArticleGroupId, ArticleGroupName, Comments, Active, SortOrder FROM         ArticleGroupDefTable order by sortorder", Con)
        adp.Fill(dt)
        Me.DataGridView1.DataSource = dt
    End Sub
    Sub EditRecord()
        Me.uitxtName.Text = DataGridView1.CurrentRow.Cells("ArticleGroupName").Value
        Me.uitxtComments.Text = DataGridView1.CurrentRow.Cells("Comments").Value
        Me.uitxtSortOrder.Text = DataGridView1.CurrentRow.Cells("SortOrder").Value
        Me.uichkActive.Checked = IIf(DataGridView1.CurrentRow.Cells("Active").Value = 0, False, True)
        Me.CurrentId = Me.DataGridView1.CurrentRow.Cells(0).Value
        Me.BtnSave.Text = "&Update"
        GetSecurityRights()
    End Sub

    Private Sub frmDefCategoryold_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

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
            ShowErrorMessage("Please enter valid group")
            Me.uitxtName.Focus()
            Exit Sub
        End If

        'If Not msg_Confirm(str_ConfirmSave) = True Then
        Me.uitxtName.Focus()
        'Exit Sub
        ' End If

        'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con
        Try
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "&Save" Then
                cm.CommandText = "insert into ArticleGroupDefTable(ArticleGroupName, Comments,sortorder, Active ) values('" & Me.uitxtName.Text & "','" & Me.uitxtComments.Text & "','" & Me.uitxtSortOrder.Text & "'," & IIf(Me.uichkActive.Checked = False, 0, 1) & ")Select @@Identity"
            Else
                cm.CommandText = "update ArticleGroupDefTable set ArticleGroupName='" & Me.uitxtName.Text & "',Comments='" & Me.uitxtComments.Text & "', sortorder='" & Me.uitxtSortOrder.Text & "',Active=" & IIf(Me.uichkActive.Checked = False, 0, 1) & " where ArticleGroupId=" & Me.CurrentId
            End If
            Dim identity As Integer = Convert.ToInt32(cm.ExecuteScalar())
            'MsgBox("Record Saved Successfully", MsgBoxStyle.Information, str_MessageHeader)
            ' msg_Information(str_informSave)
            Try
                ''insert Activity Log
                SaveActivityLog("Config", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", identity, Me.CurrentId))
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
        If IsValidToDelete("ArticleDefTable", "ArticleGroupId", Me.DataGridView1.CurrentRow.Cells("ArticleGroupId").Value.ToString) = True Then
            If msg_Confirm(str_ConfirmDelete) = True Then
                Try
                    'R-974 Ehtisham ul Haq user friendly system modification on 11-1-14 

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    Dim cm As New OleDbCommand

                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "delete from ArticleGroupDefTable where ArticleGroupId=" & Me.DataGridView1.CurrentRow.Cells("ArticleGroupId").Value.ToString
                    cm.ExecuteNonQuery()
                    'msg_Information(str_informDelete)
                    Me.CurrentId = 0

                    Try
                        ''insert Activity Log
                        SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.DataGridView1.CurrentRow.Cells("ArticleGroupId").Value.ToString, True)
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

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            'Dim dt As DataTable = GetFormRights(EnumForms.frmDefCategory)
            'If Not dt Is Nothing Then
            '    If Not dt.Rows.Count = 0 Then
            '        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
            '            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
            '        Else
            '            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
            '        End If
            '        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
            '        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
            '        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
            '    End If
            'End If
            Me.Visible = False
            Me.BtnSave.Enabled = False
            Me.BtnDelete.Enabled = False
            Me.BtnPrint.Enabled = False
            'CtrlGrdBar1.mGridPrint.Enabled = False
            'CtrlGrdBar1.mGridExport.Enabled = False

            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
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
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If Not Me.DataGridView1.GetRow Is Nothing Then
            Me.EditRecord()
        End If
    End Sub

    Private Sub DataGridView1_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles DataGridView1.FormattingRow

    End Sub

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