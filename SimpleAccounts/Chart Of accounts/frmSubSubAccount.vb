Imports System.Data.OleDb
Public Class frmSubSubAccount
    Dim CurrentId As Integer
    Dim IsLoadedForm As Boolean = False
    Public IsDetail As Boolean = False
    Public flgCompanyRights As Boolean = False
    Public IsSelectedForm As Boolean = False
    Private Sub frmSubSubAccount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    Private Sub frmSubSubAccount_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.lblProgress.Text = "Loading please wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            If IsSelectedForm = False Then
                Me.RefreshForm()
                Me.FillComboBox()
            End If
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
        'Me.txtCode.Focus()
        Me.TextBox2.Text = ""
        Me.BindGrid()
        Me.SelectAccountCode()
        Me.uicmbType.SelectedIndex = 0
        Me.cmbPLNotes.SelectedIndex = 0
        Me.cboBSNotes.SelectedValue = 0
        If Not Me.cmbCompany.SelectedIndex = -1 Then Me.cmbCompany.SelectedIndex = 0
        If Me.ComboBox1.SelectedIndex > 0 Then
            Me.txtCode.Text = Microsoft.VisualBasic.Right(GetNextDocNo(Me.txtMainCode.Text, 7, "tblCOAMainSubSub", "Sub_sub_code"), 3)
        End If
        GetSecurityRights()
        If flgCompanyRights = False Then
            Me.cmbCompany.Enabled = False
        Else
            Me.cmbCompany.Enabled = True
        End If
        IsSelectedForm = False
    End Sub

    Sub GetNewAccountCode()

    End Sub

    Sub BindGrid()

        If Me.ComboBox1.SelectedValue > 0 Then

            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable


            Dim strSql As String
            strSql = "  select distinct main_sub_sub_id, main_sub_id,sub_title + '-' + sub_code AS SubAc, sub_sub_code, sub_sub_title,account_type, PL_Note_Title,pl_note_id,ISNULL(DrBS_note_id,0) DrBS_note_id,ISNULL(DrBS_note_Title, ISNULL(CrBS_note_Title,'')) BS_Note_Title,ISNULL(CrBS_note_id,0) CrBS_note_id,ISNULL(CrBS_note_Title,'') as Cr_BS_Note_Title, isnull(CompanyId,0) as CompanyId  from vwCOADetail " & _
                        "  where main_sub_id = " & Me.ComboBox1.SelectedValue & _
                        " ORDER BY sub_sub_code "

            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            Me.DataGridView1.DataSource = dt

        End If

    End Sub

    Sub SelectAccountCode()
        If Me.ComboBox1.SelectedValue > 0 Then

            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            Dim strSql As String
            strSql = " SELECT sub_code  AS Title From tblCoaMainsub where main_sub_id = " & Me.ComboBox1.SelectedValue
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
        '    adp = New OleDbDataAdapter("select * from tblCoaMainsub", Con)
        '    adp.Fill(dt)

        '    Me.ComboBox1.ValueMember = "MAIN_SUB_ID"
        '    Me.ComboBox1.DisplayMember = "SUB_TITLE"
        '    Me.ComboBox1.DataSource = dt

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable ORDER BY 2")
        FillDropDown(Me.ComboBox1, "select main_sub_id, sub_title +' - ' + sub_code from tblCoaMainsub order by 2")
        FillDropDown(Me.cmbPLNotes, "select * from tbldefglnotes where note_type='PL' order by 2")
        FillDropDown(Me.cboBSNotes, "SELECT * FROM tblDefGLNotes WHERE note_type='BS' order by 2")


    End Sub

    Sub EditRecord()
        Me.CurrentId = Me.DataGridView1.CurrentRow.Cells("main_sub_sub_id").Value
        Dim str As String = DataGridView1.CurrentRow.Cells("sub_sub_code").Value
        Me.txtMainCode.Text = Microsoft.VisualBasic.Left(str, str.Length - 4)
        Me.txtCode.Text = Microsoft.VisualBasic.Right(str, 3)
        Me.TextBox2.Text = DataGridView1.CurrentRow.Cells("sub_sub_title").Value
        Me.uicmbType.SelectedItem = DataGridView1.CurrentRow.Cells("account_type").Value
        Me.cmbPLNotes.SelectedValue = Val(DataGridView1.CurrentRow.Cells("PL_Note_Id").Value.ToString)

        If Val(DataGridView1.CurrentRow.Cells("DrBS_Note_id").Value.ToString) > 0 Then
            Me.cboBSNotes.SelectedValue = Val(DataGridView1.CurrentRow.Cells("DrBS_Note_id").Value.ToString)
        Else
            Me.cboBSNotes.SelectedValue = Val(DataGridView1.CurrentRow.Cells("CrBS_Note_id").Value.ToString)
        End If
        Me.cmbCompany.SelectedValue = DataGridView1.CurrentRow.Cells("CompanyId").Value
        'Me.ComboBox1.SelectedValue = DataGridView1.CurrentRow.Cells("coa_main_id").Value
        Me.BtnSave.Text = "&Update"
        GetSecurityRights()
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click

        If Not Me.ComboBox1.SelectedIndex > 0 Then
            msg_Error("Please select a type")
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
        Dim Code As String

        If Me.txtCode.TextLength = 1 Then
            Code = "00" & Me.txtCode.Text
        ElseIf txtCode.TextLength = 2 Then
            Code = "0" & Me.txtCode.Text
        Else
            Code = Me.txtCode.Text
        End If

        'Comment Against task:2766
        'If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "&Save" Then
        '    If Not IsValidToSave("tblCOAMainSubsub", "sub_sub_code", "" & Me.txtMainCode.Text & "-" & Code & "") Then
        '        msg_Error("Code already exist ... ")
        '        Exit Sub
        '    End If
        'Else
        'End If
        'End Task:2766

        ''TASK TFS1782 Muhammad Ameen on 12-12-2017. Here is checked to allow or not allow duplicate account name.
        Dim DuplicateAccountName As Boolean = False
        If Not getConfigValueByType("DuplicateAccountName").ToString = "Error" Then
            DuplicateAccountName = Convert.ToBoolean(getConfigValueByType("DuplicateAccountName").ToString)
        End If
        ''03-Aug-2014 Task:2766 Imran Ali Duplicate Account Validation (Craft)
        Dim dt As New DataTable
        If DuplicateAccountName = False Then
            dt = GetDataTable("Select Count(*) From tblCOAMainSubSub WHERE main_sub_sub_id <> " & Me.CurrentId & " AND ([Sub_Sub_Code]='" & CStr(Me.txtMainCode.Text & "-" & Code).Replace("'", "''") & "' OR [Sub_Sub_Title]='" & Me.TextBox2.Text.Replace("'", "''") & "')")
        Else
            dt = GetDataTable("Select Count(*) From tblCOAMainSubSub WHERE main_sub_sub_id <> " & Me.CurrentId & " AND [Sub_Sub_Code]='" & CStr(Me.txtMainCode.Text & "-" & Code).Replace("'", "''") & "'")
        End If
        If dt IsNot Nothing Then
            If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                ShowErrorMessage("Account already exists. ")
                Exit Sub
            End If
        End If
        'End Task:2766

        If Not BtnSave.Text = "Save" And Not BtnSave.Text = "&Save" Then
            If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
        End If
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con
        Dim trans As OleDbTransaction = Con.BeginTransaction()
        Me.Cursor = Cursors.WaitCursor
        Try

            cm.Transaction = trans

            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "&Save" Then
                'cm.CommandText = "INSERT INTO [tblCOAMainSubsub]([main_sub_id], [sub_sub_code], [sub_sub_title], [account_type],[pl_note_id],[DrBS_note_id],[CrBS_note_id],[CompanyId]) " & _
                '                    "VALUES( " & Me.ComboBox1.SelectedValue & ", '" & Me.txtMainCode.Text & "-" & Code & "', N'" & Me.TextBox2.Text.Replace("'", "''") & "', '" & Me.uicmbType.SelectedItem & _
                '                    "'," & Me.cmbPLNotes.SelectedValue & ",0,0, " & IIf(Me.cmbCompany.SelectedIndex = -1, 0, Me.cmbCompany.SelectedValue) & ") select @@identity"
                cm.CommandText = "INSERT INTO [tblCOAMainSubsub]([main_sub_id], [sub_sub_code], [sub_sub_title], [account_type],[pl_note_id],[DrBS_note_id],[CrBS_note_id],[CompanyId]) " & _
                                   "VALUES( " & Me.ComboBox1.SelectedValue & ", '" & Me.txtMainCode.Text & "-" & Code & "', N'" & Me.TextBox2.Text.Replace("'", "''") & "', '" & Me.uicmbType.SelectedItem & _
                                   "'," & Me.cmbPLNotes.SelectedValue & ",0,0, " & IIf(Me.cmbCompany.SelectedIndex = -1, 0, Me.cmbCompany.SelectedValue) & ") select @@identity"

                Dim id As Integer = cm.ExecuteScalar()

                'Set Balance Sheet nodes to account
                Dim strMainType As String = ""
                If Me.cboBSNotes.SelectedIndex > 0 Then
                    cm.CommandText = "Select DISTINCT main_type from vwCOADetail where sub_code='" & Me.txtMainCode.Text & "'"
                    Dim objdr As OleDb.OleDbDataReader = cm.ExecuteReader
                    If objdr.HasRows = True Then
                        While objdr.Read
                            strMainType = objdr.Item("main_type").ToString
                        End While
                    End If
                    objdr.Close()
                    'If strMainType = "Assets" Or strMainType = "Expense" Then
                    '    cm.CommandText = "Update tblCOAMainSubSub SET DrBS_note_id=" & Me.cboBSNotes.SelectedValue & ",CrBS_note_id = 0 Where main_sub_sub_id = " & id
                    'Else
                    cm.CommandText = "Update tblCOAMainSubSub SET DrBS_note_id = " & Me.cboBSNotes.SelectedValue & ",CrBS_note_id=" & Me.cboBSNotes.SelectedValue & " Where main_sub_sub_id = " & id
                    'End If

                    cm.ExecuteNonQuery()
                End If

                'If Me.uicmbType.Text = "Inventory" Then
                '    cm.CommandText = "INSERT INTO ArticleGroupDefTable" _
                '                   & " (ArticleGroupName, Comments, Active, SortOrder, IsDate, SubSubID) " _
                '                   & " VALUES('" & Me.TextBox2.Text.Trim.Replace("'", "''") & "','' ,1 ,0 ,getdate() ," & id & " )"

                '    cm.ExecuteNonQuery()

                'End If

            Else



                cm.CommandText = "update[tblCOAMainSubsub] set [main_sub_id]= " & Me.ComboBox1.SelectedValue & ", [sub_sub_code]='" & Me.txtMainCode.Text & "-" & Code & "', [sub_sub_title]=N'" & Me.TextBox2.Text.Replace("'", "''") & "', [account_type]='" & Me.uicmbType.SelectedItem & "', [pl_note_id]=" & Me.cmbPLNotes.SelectedValue & ", [CompanyId]=" & IIf(Me.cmbCompany.SelectedIndex = -1, 0, Me.cmbCompany.SelectedValue) & _
                    ",CrBS_note_id=0,DrBS_note_id=0 where main_sub_sub_id=" & Me.CurrentId

                cm.ExecuteNonQuery()

                'Set Balance Sheet nodes to account
                Dim strMainType As String = ""
                If Me.cboBSNotes.SelectedIndex > 0 Then
                    cm.CommandText = "Select DISTINCT main_type from vwCOADetail where sub_code='" & Me.txtMainCode.Text & "'"
                    Dim objdr As OleDb.OleDbDataReader = cm.ExecuteReader
                    If objdr.HasRows = True Then
                        While objdr.Read
                            strMainType = objdr.Item("main_type").ToString
                        End While
                    End If
                    objdr.Close()
                    'If strMainType = "Assets" Or strMainType = "Expense" Then
                    '    cm.CommandText = "Update tblCOAMainSubSub SET DrBS_note_id=" & Me.cboBSNotes.SelectedValue & ",CrBS_note_id = 0 Where main_sub_sub_id = " & Me.CurrentId
                    'Else
                    cm.CommandText = "Update tblCOAMainSubSub SET DrBS_note_id =" & Me.cboBSNotes.SelectedValue & ",CrBS_note_id=" & Me.cboBSNotes.SelectedValue & " Where main_sub_sub_id = " & Me.CurrentId
                    'End If

                    cm.ExecuteNonQuery()
                End If

                'If Me.uicmbType.Text = "Inventory" Then
                '    cm.CommandText = "UPDATE  ArticleGroupDefTable" _
                '                   & " set ArticleGroupName = '" & Me.TextBox2.Text.Trim.Replace("'", "''") & "' " _
                '                   & " where SubSubID = " & CurrentId

                '    cm.ExecuteNonQuery()

                'End If

            End If
            'msg_Information(str_informSave)
            Me.CurrentId = 0



            trans.Commit()


            Try
                ''insert Activity Log
                SaveActivityLog("Accounts", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.AccountTransaction, Me.txtMainCode.Text & "-" & Code, True)
            Catch ex As Exception

            End Try

        Catch ex As Exception
            trans.Rollback()
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
        If Not DataGridView1.GetRow Is Nothing Then
            Me.EditRecord()
            Me.Cursor = Cursors.Default
        End If
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

        If IsValidToDelete("tblCOAMainSubSubDetail", "main_sub_sub_id", Me.DataGridView1.CurrentRow.Cells("main_sub_sub_id").Value.ToString) = True Then
            If msg_Confirm(str_ConfirmDelete) = True Then
                If Con.State = ConnectionState.Closed Then Con.Open()
                Dim trans As OleDbTransaction = Con.BeginTransaction()
                Me.Cursor = Cursors.WaitCursor
                Try
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    Dim cm As New OleDbCommand
                    cm.Transaction = trans

                    cm.Connection = Con
                    cm.CommandText = "delete from tblCoaMainSubSub where main_sub_sub_id=" & Me.DataGridView1.CurrentRow.Cells("main_sub_sub_id").Value.ToString
                    cm.ExecuteNonQuery()

                    cm.CommandText = "DELETE FROM ArticleGroupDefTable where SubSubID = " & Me.DataGridView1.CurrentRow.Cells("main_sub_sub_id").Value.ToString
                    cm.ExecuteNonQuery()

                    trans.Commit()
                    'msg_Information(str_informDelete)
                    Me.CurrentId = 0

                    Try
                        ''insert Activity Log
                        SaveActivityLog("Accounts", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, Me.DataGridView1.CurrentRow.Cells("sub_sub_code").Value.ToString, True)
                    Catch ex As Exception

                    End Try
                Catch ex As Exception
                    msg_Error("Error occured while deleting information: " & ex.Message)
                    trans.Rollback()
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
                Dim dt As DataTable = GetFormRights(EnumForms.frmSubSubAccount)
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


    Private Sub ChartsOfAccountToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChartsOfAccountToolStripMenuItem1.Click
        frmMain.LoadControl("ChartOfAccounts")
    End Sub

    Private Sub CashFlowStatmentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashFlowStatmentToolStripMenuItem.Click
        frmMain.LoadControl("rptCashFlow")
    End Sub

    Private Sub StanderCashFlowStatementToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StanderCashFlowStatementToolStripMenuItem1.Click
        frmMain.LoadControl("rptCashFlowStander")
    End Sub

    Private Sub CostcenterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub uicmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uicmbType.SelectedIndexChanged

    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If Not DataGridView1.GetRow Is Nothing Then
            Me.EditRecord()
        End If

    End Sub

    Private Sub DataGridView1_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles DataGridView1.FormattingRow

    End Sub

    Private Sub DataGridView1_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles DataGridView1.ColumnButtonClick
        Try
            If e.Column.Key = "Detail" Then
                frmMain.LoadControl("frmDetailAccount")
                System.Threading.Thread.Sleep(500)
                frmDetailAccount.Label5_Click(Nothing, Nothing)
                frmDetailAccount.ComboBox1.SelectedValue = Me.DataGridView1.GetRow.Cells("main_sub_sub_id").Value
                frmDetailAccount.IsSelectedForm = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            AddRptParam("@FromDate", Date.Today.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Date.Today.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("PLevel", "Detail A/c")
            ShowReport("rptChartofAccountsTrial", "{vwGlCOADetail.main_sub_id} = " & Me.ComboBox1.SelectedValue & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False

        End Try
    End Sub
    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From tblCOAMainSubSub WHERE sub_sub_code='" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        CurrentId = dt.Rows(0).Item("main_sub_sub_id").ToString
                        Me.ComboBox1.SelectedValue = dt.Rows(0).Item("main_sub_id").ToString
                        Me.txtMainCode.Text = Microsoft.VisualBasic.Left(dt.Rows(0).Item("sub_sub_code").ToString, dt.Rows(0).Item("sub_sub_code").ToString.Length - 4)
                        Me.txtCode.Text = Microsoft.VisualBasic.Right(dt.Rows(0).Item("sub_sub_code").ToString, 3)
                        Me.TextBox2.Text = dt.Rows(0).Item("sub_sub_title").ToString
                        Me.uicmbType.SelectedValue = dt.Rows(0).Item("account_type").ToString
                        Me.cmbPLNotes.SelectedValue = dt.Rows(0).Item("PL_note_id").ToString
                        If Val(dt.Rows(0).Item("DrBS_note_id").ToString) > 0 Then
                            Me.cboBSNotes.SelectedValue = Val(dt.Rows(0).Item("DrBS_note_id").ToString)
                        Else
                            Me.cboBSNotes.SelectedValue = Val(dt.Rows(0).Item("CrBS_note_id").ToString)
                        End If
                        Me.cmbCompany.SelectedValue = Val(dt.Rows(0).Item("CompanyId").ToString)
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
    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label10.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbCompany.SelectedIndex
            FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable ORDER BY 2")
            Me.cmbCompany.SelectedIndex = id
        Catch ex As Exception
            Throw ex
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

   
End Class