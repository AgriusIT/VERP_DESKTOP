Imports System.Data.OleDb
Public Class frmDetailAccountCat
    Dim CurrentId As Integer
    Dim Code As String

    Private Sub frmDetailAccountCat_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            SaveToolStripButton_Click(BtnSave, Nothing)
            Exit Sub
        End If
        If e.KeyCode = Keys.Escape Then

            NewToolStripButton_Click(BtnNew, Nothing)
            Exit Sub
        End If
    End Sub
    Private Sub frmDetailAccountCat_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Me.RefreshForm()
            Me.FillComboBox()
            Me.txtMainCode.ReadOnly = CType(getConfigValueByType("LockEditHeadCode"), Boolean)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False

        End Try
    End Sub
    Private Sub DefMainAcc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        
    End Sub
    Sub RefreshForm()
        Me.BtnSave.Text = "&Save"
        Me.txtCode.Text = ""
        Me.txtCode.Focus()
        Me.TextBox2.Text = ""
        Me.txtOpening.Text = 0
        Me.BindGrid()
        Me.SelectAccountCode()
        If Me.ComboBox1.SelectedIndex > 0 Then
            Me.txtCode.Text = Microsoft.VisualBasic.Right(GetNextDocNo(Me.txtMainCode.Text, 11, "tblCOAMainSubSubDetail", "detail_code"), 5)
        End If
        Me.GetSecurityRights()

    End Sub
    Sub GetNewAccountCode()

    End Sub
    Sub BindGrid()
        If Me.ComboBox1.SelectedValue > 0 Then

            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            Dim strSql As String
            strSql = "  SELECT     dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblCOAMainSubSubDetail.main_sub_sub_id,  " & _
                        "dbo.tblCOAMainSubSub.sub_sub_title + '-' + dbo.tblCOAMainSubSub.sub_sub_code AS SubAc, dbo.tblCOAMainSubSubDetail.detail_code, " & _
                        "dbo.tblCOAMainSubSubDetail.detail_title, OpeningBalance " & _
                        "FROM         dbo.tblCOAMainSubSub INNER JOIN " & _
                        "dbo.tblCOAMainSubSubDetail ON dbo.tblCOAMainSubSub.main_sub_sub_id = dbo.tblCOAMainSubSubDetail.main_sub_sub_id And tblCOAMainSubSub.Account_Type = 'Inventory' " & _
                        "  where tblCOAMainSubsub.main_sub_sub_id = " & Me.ComboBox1.SelectedValue & _
                        " ORDER BY detail_code "
            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            Me.DataGridView1.DataSource = dt
            ' Me.DataGridView1.RetrieveStructure()
        End If
    End Sub
    Sub SelectAccountCode()
        If Me.ComboBox1.SelectedValue > 0 Then

            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            Dim strSql As String
            strSql = " SELECT sub_sub_code  AS Title From tblCoaMainsubSub where main_sub_sub_id = " & Me.ComboBox1.SelectedValue
            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            Me.txtMainCode.Text = dt.Rows(0).Item(0).ToString

        End If
    End Sub

    Sub FillComboBox()
        'Dim adp As New OleDbDataAdapter
        'Dim dt As New DataTable
        'Dim strSql As String
        'Try
        '    'strSql = " SELECT coa_main_id,main_sub_id, main_title + '  " - "  ' + main_code  AS Title, tblCoaMainSub.sub_code AS sub_code, sub_title" & _
        '    '     " From tblCoaMain INNER JOIN" & _
        '    '     " tblCoaMainSub ON tblCoaMain.coa_main_id = tblCoaMainSub.coa_main_id" & _
        '    '     "  where tblCoaMain.coa_main_id = " & cboAccMaincode.ItemData(cboAccMaincode.ListIndex) & _
        '    '     " ORDER BY sub_code "
        '    adp = New OleDbDataAdapter("select * from tblCoaMainsubSub", Con)
        '    adp.Fill(dt)

        '    Me.ComboBox1.ValueMember = "MAIN_SUB_SUB_ID"
        '    Me.ComboBox1.DisplayMember = "SUB_SUB_TITLE"
        '    Me.ComboBox1.DataSource = dt

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        FillDropDown(Me.ComboBox1, "select main_sub_sub_id, sub_sub_title +' - '+ sub_sub_code from tblCoaMainsubSub Where Account_type='Inventory' order by 2")
    End Sub
    Sub EditRecord()
        Me.CurrentId = Me.DataGridView1.CurrentRow.Cells("coa_detail_id").Value
        Dim str As String = DataGridView1.CurrentRow.Cells("detail_code").Value
        Me.txtMainCode.Text = Microsoft.VisualBasic.Left(str, str.Length - 6) 'InStr(1,  str, "-") - 2) 'Mid(str, 1, InStr(1, str, "-") - 1)
        Me.txtCode.Text = Microsoft.VisualBasic.Right(str, 5)
        Me.TextBox2.Text = DataGridView1.CurrentRow.Cells("Detail_title").Value
        Me.txtOpening.Text = DataGridView1.CurrentRow.Cells("OpeningBalance").Value.ToString
        'Me.ComboBox1.SelectedValue = DataGridView1.CurrentRow.Cells("coa_main_id").Value
        Me.BtnSave.Text = "&Update"
        Me.SelectAccountCode()
        Me.GetSecurityRights()
    End Sub
    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Me.EditRecord()
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click

        If Not Me.ComboBox1.SelectedIndex > 0 Then
            msg_Error("Please select type ...")
            Me.ComboBox1.Focus()
            Exit Sub
        End If

        If Me.txtCode.Text = "" Then
            msg_Error("Please enter valid A/C code")
            Me.txtCode.Focus()
            Exit Sub
        End If

        If Me.TextBox2.Text = "" Then
            msg_Error("Please enter valid A/C name")
            Me.TextBox2.Focus()
            Exit Sub
        End If

        'If isvalidatetoupdate Then
        If Not BtnSave.Text = "Save" And Not BtnSave.Text = "&Save" Then
            If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
        End If
        
        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con
        Me.txtCode_Leave(Me, New EventArgs)
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "&Save" Then
                cm.CommandText = "INSERT INTO [tblCOAMainSubsubDetail]([main_sub_sub_id], [Detail_code], [Detail_title],OpeningBalance) " & _
                                    "VALUES( " & Me.ComboBox1.SelectedValue & ", '" & Me.txtMainCode.Text.ToString.Replace("'", "''") & "-" & Code & "', '" & Me.TextBox2.Text.ToString.Replace("'", "''") & "'," & Me.txtOpening.Text.ToString.Replace("'", "''") & ") select @@identity"
                Dim id As Integer = cm.ExecuteScalar()

                cm.CommandText = "INSERT INTO ArticleGroupDefTable" _
                                   & " (ArticleGroupName, Comments, Active, SortOrder, IsDate, SubSubID) " _
                                   & " VALUES('" & Me.TextBox2.Text.Trim.Replace("'", "''") & "','' ,1 ,0 ,getdate() ," & id & " )"
                cm.ExecuteNonQuery()

            Else
                cm.CommandText = "update[tblCOAMainSubsubDetail] set [main_Sub_sub_id]= " & Me.ComboBox1.SelectedValue & ", [Detail_code]='" & Me.txtMainCode.Text.ToString.Replace("'", "''") & "-" & Code & "', [Detail_title]='" & Me.TextBox2.Text.ToString.Replace("'", "''") & "',OpeningBalance=" & Val(Me.txtOpening.Text.ToString.Replace("'", "''")) & " where Coa_Detail_id=" & Me.CurrentId
                cm.ExecuteNonQuery()

                'cm.CommandText = " Update ArticleDefTable set ArticleDescription = '" & Me.TextBox2.Text & "' where AccountID = " & Me.CurrentId
                'cm.ExecuteNonQuery()

                cm.CommandText = "UPDATE  ArticleGroupDefTable" _
                                   & " set ArticleGroupName = '" & Me.TextBox2.Text.Trim.Replace("'", "''") & "' " _
                                   & " where SubSubID = " & CurrentId

                cm.ExecuteNonQuery()
            End If

            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13 
            'msg_Information(str_informSave)
            Me.CurrentId = 0

            Try
                ''insert Activity Log
                SaveActivityLog("Accounts", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.AccountTransaction, Me.txtMainCode.Text & "-" & Code, True)
            Catch ex As Exception

            End Try


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Con.Close()
        End Try
        Me.RefreshForm()
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.RefreshForm()
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.EditRecord()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

        Me.RefreshForm()

    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        'DefMainAcc.ShowDialog()
        Me.FillComboBox()
    End Sub

    Private Sub txtCode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.Leave

        If Me.txtCode.TextLength = 1 Then
            Code = "0000" & Me.txtCode.Text
        ElseIf txtCode.TextLength = 2 Then
            Code = "000" & Me.txtCode.Text
        ElseIf txtCode.TextLength = 3 Then
            Code = "00" & Me.txtCode.Text
        ElseIf txtCode.TextLength = 4 Then
            Code = "0" & Me.txtCode.Text
        Else

            Code = Me.txtCode.Text
        End If
        Me.txtCode.Text = Code
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not DataGridView1.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If IsValidToDelete("tblVoucherDetail", "Coa_Detail_id", Me.DataGridView1.CurrentRow.Cells("coa_detail_id").Value.ToString) = True Then
            Dim cmdV As New OleDbCommand
            'Check Category Item Validation
            If Con.State = ConnectionState.Closed Then Con.Open()
            cmdV.Connection = Con
            cmdV.CommandText = "SELECT * FROM  ArticleGroupDefTable INNER JOIN ArticleDefTable ON ArticleGroupDefTable.ArticleGroupId = ArticleDefTable.ArticleGroupId " & _
                "WHERE ArticleGroupDefTable.SubSubID = " & Me.DataGridView1.CurrentRow.Cells("coa_detail_id").Value.ToString
            Dim Count As Double = cmdV.ExecuteNonQuery
            If Count > 0 Then
                msg_Error(str_ErrorDependentRecordFound)
                Exit Sub
            End If

            If msg_Confirm(str_ConfirmDelete) = True Then
                Try
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    Dim cm As New OleDbCommand

                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "delete from tblCoaMainSubSubDetail where Coa_Detail_id=" & Me.DataGridView1.CurrentRow.Cells("coa_detail_id").Value.ToString
                    cm.ExecuteNonQuery()
                    'msg_Information(str_informDelete)
                    Me.CurrentId = 0
                Catch ex As Exception
                    msg_Error("Error occured while deleting information: " & ex.Message)
                Finally
                    Me.lblProgress.Visible = False
                    Con.Close()
                End Try

                Try
                    ''insert Activity Log
                    SaveActivityLog("Accounts", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, DataGridView1.CurrentRow.Cells("detail_code").Value.ToString, True)
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
                Dim dt As DataTable = GetFormRights(EnumForms.frmDetailAccount)
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

    
    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click

    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Me.BtnEdit, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            DeleteToolStripButton_Click(BtnDelete, Nothing)
            Exit Sub
        End If
    End Sub
End Class