''03-Aug-2014 Task:2766 Imran Ali Duplicate Account Validation (Craft)
Imports System.Data.OleDb
Public Class frmSubAccount
    Dim CurrentId As Integer
    Dim IsLoadedForm As Boolean = False
    Public IsSelectedForm As Boolean = False
    'R-974 Ehtisham ul Haq short keys added on 29-12-13
    Private Sub frmSubAccount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            SaveToolStripButton_Click(BtnSave, Nothing)
            Exit Sub
        End If
        If e.KeyCode = Keys.Escape Then

            NewToolStripButton_Click(BtnNew, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.P AndAlso e.Control = True Then
            BtnPrint_Click(BtnPrint, Nothing)
            Exit Sub
        End If

    End Sub
    Private Sub frmSubAccount_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            Me.lblProgress.Text = "Loading please wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If IsSelectedForm = False Then
                Me.RefreshForm()
                Me.FillComboBox()
            End If
            'If GetConfigValue("LockEditHeadCode") = "True" Then Me.txtCode.ReadOnly = False
            Me.txtMainCode.ReadOnly = CType(getConfigValueByType("LockEditHeadCode"), Boolean)
            IsLoadedForm = True
            Get_All(frmModProperty.Tags)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
            IsSelectedForm = False
        End Try

    End Sub
    Private Sub DefMainAcc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub
    Sub RefreshForm()
        Me.BtnSave.Text = "&Save"
        Me.txtCode.Text = ""
        Me.txtCode.Focus()
        Me.TextBox2.Text = ""
        Me.BindGrid()
        Me.SelectAccountCode()
        If Me.ComboBox1.SelectedIndex > 0 Then
            Me.txtCode.Text = Microsoft.VisualBasic.Right(GetNextDocNo(Me.txtMainCode.Text, 3, "tblCoaMainSub", "sub_code"), 3)
        End If
        GetSecurityRights()
        IsSelectedForm = False
    End Sub
    Sub GetNewAccountCode()

    End Sub
    Sub BindGrid()
        If Me.ComboBox1.SelectedValue > 0 Then

            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            Dim strSql As String
            strSql = " SELECT tblCoaMainSub.main_sub_id, main_title + ' - ' + main_code  AS Title, tblCoaMainSub.sub_code AS sub_code, sub_title" & _
                 " From tblCoaMain INNER JOIN" & _
                 " tblCoaMainSub ON tblCoaMain.coa_main_id = tblCoaMainSub.coa_main_id" & _
                 "  where tblCoaMain.coa_main_id = " & Me.ComboBox1.SelectedValue & _
                 " ORDER BY sub_code "
            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            Me.DataGridView1.DataSource = dt
            ' Me.DataGridView1.RetrieveStructure()
            Me.DataGridView1.RootTable.Columns(0).Visible = False

        End If
    End Sub
    Sub SelectAccountCode()
        If Me.ComboBox1.SelectedValue > 0 Then

            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            Dim strSql As String
            strSql = " SELECT main_code  AS Title From tblCoaMain where coa_main_id = " & Me.ComboBox1.SelectedValue
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
        '    adp = New OleDbDataAdapter("SELECT     coa_main_id, main_code, main_title, main_type FROM         dbo.tblCOAMain", Con)
        '    adp.Fill(dt)

        '    Me.ComboBox1.ValueMember = "COA_MAIN_ID"
        '    Me.ComboBox1.DisplayMember = "MAIN_TITLE"
        '    Me.ComboBox1.DataSource = dt

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        FillDropDown(Me.ComboBox1, "SELECT  coa_main_id,  main_title +' - '+ main_code as main_title, main_type FROM         dbo.tblCOAMain order by 2")
    End Sub
    Sub EditRecord()
        If DataGridView1.Row < 0 Then Exit Sub
        Me.CurrentId = Val(Me.DataGridView1.CurrentRow.Cells("main_sub_id").Value.ToString)
        Dim str As String = DataGridView1.CurrentRow.Cells("sub_code").Value.ToString
        Me.txtMainCode.Text = Mid(str, 1, InStr(1, str, "-") - 1)
        Me.txtCode.Text = Mid(str, InStr(1, str, "-") + 1)
        Me.TextBox2.Text = DataGridView1.CurrentRow.Cells("sub_title").Value.ToString
        'Me.ComboBox1.SelectedValue = DataGridView1.CurrentRow.Cells("coa_main_id").Value
        Me.BtnSave.Text = "&Update"

        GetSecurityRights()

    End Sub
    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Me.EditRecord()
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click


        Dim Code As String
        If Me.txtCode.TextLength = 1 Then
            Code = "00" & Me.txtCode.Text
        ElseIf txtCode.TextLength = 2 Then
            Code = "0" & Me.txtCode.Text
        Else
            Code = Me.txtCode.Text
        End If

        If Not Me.ComboBox1.SelectedIndex > 0 Then
            msg_Error("Please select type")
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

        ''TASK TFS1782 Muhammad Ameen on 12-12-2017. Here is checked to allow or not allow duplicate account name.
        Dim DuplicateAccountName As Boolean = False
        If Not getConfigValueByType("DuplicateAccountName").ToString = "Error" Then
            DuplicateAccountName = Convert.ToBoolean(getConfigValueByType("DuplicateAccountName").ToString)
        End If

        ''03-Aug-2014 Task:2766 Imran Ali Duplicate Account Validation (Craft)
        Dim dt As New DataTable
        If DuplicateAccountName = False Then
            dt = GetDataTable("Select Count(*) From tblCOAMainSub WHERE main_sub_id <> " & Me.CurrentId & " AND ([Sub_Code]='" & CStr(Me.txtMainCode.Text & "-" & Code).Replace("'", "''") & "' OR [Sub_Title]='" & Me.TextBox2.Text.Replace("'", "''") & "')")
        Else
            dt = GetDataTable("Select Count(*) From tblCOAMainSub WHERE main_sub_id <> " & Me.CurrentId & " AND [Sub_Code]='" & CStr(Me.txtMainCode.Text & "-" & Code).Replace("'", "''") & "'")
        End If
        If dt IsNot Nothing Then
            If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                ShowErrorMessage("Account already exists. ")
                Exit Sub
            End If
        End If
        'End Task:2766

        'R-974_EHT_confirmation message removed on 28/12/13
        If Not BtnSave.Text = "Save" And Not BtnSave.Text = "&Save" Then
            If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
        End If

        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            'Dim Code As String
            'If Me.txtCode.TextLength = 1 Then
            '    Code = "00" & Me.txtCode.Text
            'ElseIf txtCode.TextLength = 2 Then
            '    Code = "0" & Me.txtCode.Text
            'Else
            '    Code = Me.txtCode.Text
            'End If

            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "&Save" Then
                cm.CommandText = "INSERT INTO [tblCOAMainSub]([coa_main_id], [sub_code], [sub_title]) " & _
                                    "VALUES( " & Me.ComboBox1.SelectedValue & ", '" & Me.txtMainCode.Text & "-" & Code & "', N'" & Me.TextBox2.Text.Replace("'", "''") & "')"
            Else
                cm.CommandText = "update[tblCOAMainSub] set [coa_main_id]= " & Me.ComboBox1.SelectedValue & ", [sub_code]='" & Me.txtMainCode.Text & "-" & Code & "', [sub_title]=N'" & Me.TextBox2.Text.Replace("'", "''") & "' where main_sub_id=" & Me.CurrentId
            End If
            cm.ExecuteNonQuery()
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
            Me.Cursor = Cursors.Default
        End Try
        Me.RefreshForm()
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.Cursor = Cursors.WaitCursor
        Me.RefreshForm()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Me.EditRecord()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Me.RefreshForm()

    End Sub

    Public Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        'DefMainAcc.ShowDialog()
        Me.FillComboBox()
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not DataGridView1.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If IsValidToDelete("tblCOAMainSubSub", "main_sub_id", Me.DataGridView1.CurrentRow.Cells("main_sub_id").Value.ToString) = True Then
            If msg_Confirm(str_ConfirmDelete) = True Then
                Me.Cursor = Cursors.WaitCursor
                Try
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    Dim cm As New OleDbCommand

                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "delete from tblCoaMainSub where main_sub_id=" & Me.DataGridView1.CurrentRow.Cells("main_sub_id").Value.ToString
                    cm.ExecuteNonQuery()
                    'msg_Information(str_informDelete)
                    Me.CurrentId = 0

                    Try
                        ''insert Activity Log
                        SaveActivityLog("Accounts", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, DataGridView1.CurrentRow.Cells("main_code").Value, True)
                    Catch ex As Exception
 

                    End Try


                Catch ex As Exception
                    msg_Error("Error occured while deleting record: " & ex.Message)
                Finally
                    Me.lblProgress.Visible = False
                    Con.Close()
                    Me.Cursor = Cursors.Default
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
                Dim dt As DataTable = GetFormRights(EnumForms.frmSubAccount)
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
    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        EditRecord()
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            AddRptParam("@FromDate", Date.Today.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Date.Today.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("PLevel", "Detail A/c")
            ShowReport("rptChartofAccountsTrial", "{vwGlCOADetail.coa_main_id}= " & Me.ComboBox1.SelectedValue & "")
        Catch ex As Exception

        Finally
            Me.lblProgress.Visible = False
        End Try

    End Sub
    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From tblCOAMainSub WHERE sub_code='" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        CurrentId = dt.Rows(0).Item("main_sub_id").ToString
                        Me.ComboBox1.SelectedValue = dt.Rows(0).Item("coa_main_id").ToString
                        Me.txtMainCode.Text = Mid((dt.Rows(0).Item("sub_code").ToString), 1, InStr(1, (dt.Rows(0).Item("sub_code").ToString), "-") - 1)
                        Me.txtCode.Text = Mid((dt.Rows(0).Item("sub_code").ToString), InStr(1, (dt.Rows(0).Item("sub_code").ToString), "-") + 1)
                        Me.TextBox2.Text = dt.Rows(0).Item("sub_title").ToString
                        Me.BtnSave.Text = "&Update"
                        GetSecurityRights()
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

    Private Sub DataGridView1_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles DataGridView1.ColumnButtonClick
        Try
            frmMain.LoadControl("frmSubSubAccount")
            System.Threading.Thread.Sleep(500)
            frmSubSubAccount.Label5_Click(Nothing, Nothing)
            frmSubSubAccount.ComboBox1.SelectedValue = Me.DataGridView1.GetRow.Cells("main_sub_id").Value
            frmSubSubAccount.IsSelectedForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

   
    'R-974 Ehtisham ul haq short keys are added on 29-12-13
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

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged

    End Sub
End Class