'' 13-Dec-2013 ReqId-925 Imran Ali Item code not update and increase length
Imports System.Data.OleDb
Public Class frmDefSize
    Dim CurrentId As Integer
    Dim strBatchIDs As String = String.Empty
    Private localiDs As String = String.Empty
    Dim IsLoadedForm As Boolean = False
    Sub RefreshForm()
        Me.BtnSave.Text = "&Save"
        Me.uitxtName.Text = ""
        Me.uitxtName.Focus()
        Me.txtSizeCode.Text = String.Empty
        Me.uitxtComments.Text = ""
        Me.uitxtSortOrder.Text = 1
        Me.uichkActive.Checked = True
        Me.BindGrid()
        Me.lstBatches.SelectedIndex = -1
        GetSecurityRights()
        Me.SetConfigurationBaseSettings()
    End Sub
    Private Sub SetConfigurationBaseSettings()
        Try
            If Convert.ToBoolean(getConfigValueByType("WithSizeRange")) = True Then
                Me.grpBatches.Visible = True
            Else
                Me.grpBatches.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub BindGrid()
        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable

        adp = New OleDbDataAdapter("SELECT ArticleSizeId, ArticleSizeName, SizeCode, Comments, Active, SortOrder FROM ArticleSizeDefTable order by sortorder", Con)
        adp.Fill(dt)
        Me.DataGridView1.DataSource = dt
        Me.DataGridView1.RetrieveStructure()
        Me.DataGridView1.RootTable.Columns(0).Visible = False
    End Sub
    Sub EditRecord()
        Try

            Me.uitxtName.Text = DataGridView1.CurrentRow.Cells("ArticleSizeName").Value
            Me.txtSizeCode.Text = Me.DataGridView1.GetRow.Cells("SizeCode").Text.ToString
            Me.uitxtComments.Text = DataGridView1.CurrentRow.Cells("Comments").Value.ToString
            Me.uitxtSortOrder.Text = DataGridView1.CurrentRow.Cells("SortOrder").Value.ToString
            Me.uichkActive.Checked = IIf(DataGridView1.CurrentRow.Cells("Active").Value = 0, False, True)
            Me.CurrentId = Me.DataGridView1.CurrentRow.Cells(0).Value
            Me.GetBatches()
            Me.BtnSave.Text = "&Update"
            GetSecurityRights()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmDefSize_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    Private Sub frmDefSize_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Me.RefreshForm()
        Me.FillCombo()
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
    'Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
    '    Me.EditRecord()
    'End Sub
    '' ReqId-925 Set Size Code Max 5 Length
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click

        If Me.uitxtName.Text = "" Then
            ShowErrorMessage("Please enter valid size")
            Me.uitxtName.Focus()
            Exit Sub

        End If

        If Me.lstBatches.Visible = True Then
            Dim intMaxSize As Integer = getConfigValueByType("MaxSizeSelection")
            If Me.lstBatches.SelectedItems.Count > intMaxSize Then
                msg_Error("You can select maximum " & intMaxSize & " batch.")
                Exit Sub
            End If
        End If

        If Me.BtnSave.Text = "&Save" Then
            Dim str As String = String.Empty
            str = "Select SizeCode From ArticleSizeDefTable WHERE SizeCode=N'" & Me.txtSizeCode.Text.ToString.Replace("'", "''") & "'"
            Dim dtColorCodeValidate As DataTable = GetDataTable(str)
            If dtColorCodeValidate.Rows.Count > 0 Then
                ShowErrorMessage("Size code already exist")
                Me.txtSizeCode.Focus()
                Exit Sub
            End If
        End If
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()

        'If Not msg_Confirm(str_ConfirmSave) = True Then 'If MsgBox("Do you want to save ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, str_MessageHeader) = MsgBoxResult.No Then
        Me.uitxtName.Focus()
        'Exit Sub
        'End If

        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con
        Dim identity As Integer = 0I
        Try
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "&Save" Then
                cm.CommandText = "insert into ArticleSizeDefTable(ArticleSizeName, Comments,sortorder, Active, SizeCode) values(N'" & Me.uitxtName.Text.ToString.Replace("'", "''") & "',N'" & Me.uitxtComments.Text.ToString.Replace("'", "''") & "',N'" & Me.uitxtSortOrder.Text.ToString.Replace("'", "''") & "'," & IIf(Me.uichkActive.Checked = False, 0, 1) & ", N'" & Me.txtSizeCode.Text.ToString.Replace("'", "''") & "') Select @@Identity"
                identity = Convert.ToInt32(cm.ExecuteScalar())

                If Me.lstBatches.Visible = True Then
                    If Me.lstBatches.SelectedItems.Count > 0 Then
                        Dim strIDs() As String = Me.GetSelectedIDs().Split(",")
                        For Each Id As String In strIDs
                            cm.CommandText = " INSERT INTO SizeRangeTable  (SizeID, BatchID)  VALUES(" & identity & "," & Id & " )"
                            cm.ExecuteNonQuery()
                        Next
                    End If
                End If
            Else

                cm.CommandText = "update ArticleSizeDefTable set ArticleSizeName=N'" & Me.uitxtName.Text.ToString.Replace("'", "''") & "',Comments=N'" & Me.uitxtComments.Text.ToString.Replace("'", "''") & "', sortorder=N'" & Me.uitxtSortOrder.Text.ToString.Replace("'", "''") & "',Active=" & IIf(Me.uichkActive.Checked = False, 0, 1) & ", SizeCode=N'" & Me.txtSizeCode.Text & "' where ArticleSizeId=" & Me.CurrentId
                cm.ExecuteNonQuery()
                ''925 Size Code Update In Article Detail Table 
                cm.CommandText = ""
                cm.CommandText = "update  ArticleDefTable set  ArticleDefTable.ArticleCode =     ArticleDefTableMaster.ArticleCode +'-'+ ArticleSizeDefTable.SizeCode +'-'+ ArticleColorDefTable.ColorCode " _
                    & "  FROM ArticleColorDefTable INNER JOIN " _
                    & "  ArticleDefTable ON ArticleColorDefTable.ArticleColorId = ArticleDefTable.ArticleColorId INNER JOIN " _
                    & "  ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId INNER JOIN " _
                    & "  ArticleDefTableMaster ON ArticleDefTable.MasterID = ArticleDefTableMaster.ArticleId " _
                    & "  where(ArticleSizeDefTable.ArticleSizeId = " & Me.CurrentId & ") "
                cm.ExecuteNonQuery()

                If Me.lstBatches.Visible = True Then
                    cm.CommandText = "Delete From SizeRangeTable where SizeID= " & Me.CurrentId
                    cm.ExecuteNonQuery()
                    If Me.lstBatches.SelectedItems.Count > 0 Then
                        Dim strIDs() As String = Me.GetSelectedIDs().Split(",")
                        For Each Id As String In strIDs
                            cm.CommandText = " INSERT INTO SizeRangeTable  (SizeID, BatchID)  VALUES(" & Me.CurrentId & "," & Id & " )"
                            cm.ExecuteNonQuery()
                        Next
                    End If
                End If



            End If

            '  MsgBox("Record Saved Successfully", MsgBoxStyle.Information, str_MessageHeader)

            'msg_Information(str_informSave)
            Try
                ''insert Activity Log
                SaveActivityLog("Config", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", identity, Me.CurrentId), True)
            Catch ex As Exception

            End Try
            Me.CurrentId = 0
        Catch ex As Exception
            ShowErrorMessage("Error occured while saving record: " & ex.Message)
            Con.Close()
        Finally
            Me.lblProgress.Visible = False
        End Try
        Me.RefreshForm()
    End Sub
    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not DataGridView1.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If IsValidToDelete("ArticleDefTable", "SizeRangeId", Me.DataGridView1.CurrentRow.Cells("ArticleSizeId").Value.ToString) = True Then
            If msg_Confirm(str_ConfirmDelete) = True Then
                Try
                    Dim cm As New OleDbCommand

                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "delete from ArticleSizeDefTable where ArticleSizeId=" & Me.DataGridView1.CurrentRow.Cells("ArticleSizeId").Value.ToString
                    cm.ExecuteNonQuery()

                    If Me.lstBatches.Visible = True Then
                        cm.CommandText = "Delete From SizeRangeTable where SizeID= " & Me.DataGridView1.CurrentRow.Cells("ArticleSizeId").Value.ToString
                        cm.ExecuteNonQuery()
                    End If

                    msg_Information(str_informDelete)
                    Me.CurrentId = 0
                Catch ex As Exception
                    msg_Error("Error occured while deleting record: " & ex.Message)
                Finally
                    Con.Close()
                End Try

                Try
                    ''insert Activity Log
                    SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.DataGridView1.CurrentRow.Cells("Id").Value.ToString, True)
                Catch ex As Exception

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
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefSize)
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

    Private Sub FillCombo(Optional ByVal Condition As String = "")
        If Me.lstBatches.Visible = False Then Exit Sub
        Try
            If Me.lstBatches.Visible = True Then
                Dim strSQL As String = "SELECT BatchID, BatchNo FROM PurchaseBatchTable "
                Dim dt As DataTable = GetDataTable(strSQL)
                Me.lstBatches.DisplayMember = "BatchNo"
                Me.lstBatches.ValueMember = "BatchID"
                Me.lstBatches.DataSource = dt
                Me.lstBatches.SelectedIndex = -1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GetSelectedIDs() As String
        Try
            Dim strIDs As String = String.Empty
            For Each obj As Object In Me.lstBatches.SelectedItems
                Dim dr As DataRowView = CType(obj, DataRowView)
                strIDs = strIDs & IIf(strIDs.Length > 0, ",", "") & dr.Row.Item("BatchID")
            Next
            Return strIDs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GetBatches()
        Try
            If Me.lstBatches.Visible = True Then
                Dim strSQL As String = "Select BatchID from SizeRangeTable where SizeID = " & Me.CurrentId
                Dim dt As DataTable = GetDataTable(strSQL)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Me.lstBatches.SelectedValue = dr.Item(0)
                    Next
                Else
                    Me.lstBatches.SelectedIndex = -1
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
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
                Dim dt As DataTable = GetDataTable("Select * From ArticleSizeDefTable WHERE ArticleSizeId=N'" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.uitxtName.Text = dt.Rows(0).Item("ArticleSizeName").ToString
                        Me.txtSizeCode.Text = dt.Rows(0).Item("SizeCode").ToString
                        Me.uitxtComments.Text = dt.Rows(0).Item("Comments").ToString
                        Me.uitxtSortOrder.Text = dt.Rows(0).Item("SortOrder").ToString
                        Me.uichkActive.Checked = dt.Rows(0).Item("Active")
                        Me.CurrentId = dt.Rows(0).Item("ArticleSizeId").ToString
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

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label4_Click_1(sender As Object, e As EventArgs) Handles lblHeader.Click

    End Sub

    Private Sub lstBatches_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstBatches.SelectedIndexChanged

    End Sub

    Private Sub DataGridView1_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles DataGridView1.FormattingRow

    End Sub
End Class