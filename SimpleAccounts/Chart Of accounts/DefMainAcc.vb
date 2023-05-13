''03-Aug-2014 Task:2766 Imran Ali Duplicate Account Validation (Craft)
Imports SBDal.GroupRightsBL
Imports System.Data.OleDb
Imports SBModel
Imports System.Collections.Generic
Public Class DefMainAcc
    Dim CurrentId As Integer
    Dim IsLoadedForm As Boolean = False

    Private Sub DefMainAcc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.F4 Then
            SaveToolStripButton_Click(btnSave, Nothing)
            Exit Sub
        End If
        If e.KeyCode = Keys.Escape Then

            NewToolStripButton_Click(btnNew, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.P AndAlso e.Control = True Then
            btnPrint_Click(btnPrint, Nothing)
            Exit Sub
        End If


    End Sub

    Private Sub DefMainAcc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub DefMainAcc_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.lblProgress.Text = "Loading please wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Me.GetSecurityRights()
            Me.ComboBox1.SelectedIndex = 0
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

    Sub RefreshForm()

        Me.btnSave.Text = "&Save"
        Me.TextBox1.Text = ""
        Me.TextBox1.Focus()
        Me.TextBox2.Text = ""
        'Me.ComboBox1.SelectedIndex = 0
        Me.BindGrid()

        Me.GetSecurityRights()

    End Sub
    Sub BindGrid()
        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable
        adp = New OleDbDataAdapter("select * from tblCoaMain where main_type='" & Me.ComboBox1.Text & "'", Con)
        adp.Fill(dt)
        Me.Datagridview1.DataSource = dt
    End Sub
    Sub EditRecord()
        Me.TextBox1.Text = Datagridview1.GetRow.Cells("main_code").Value
        Me.TextBox2.Text = Datagridview1.GetRow.Cells("main_title").Value
        Me.ComboBox1.SelectedItem = Datagridview1.GetRow.Cells("main_type").Value
        Me.CurrentId = Me.Datagridview1.CurrentRow.Cells(0).Value
        Me.btnSave.Text = "&Update"
        Me.GetSecurityRights()
    End Sub


    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click


        If Me.TextBox1.Text = "" Then
            msg_Error("Please enter valid A/C code")
            Me.TextBox1.Focus()
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
            dt = GetDataTable("Select Count(*) From tblCoaMain WHERE coa_main_id <> " & Me.CurrentId & " AND (main_code='" & Me.TextBox1.Text.Replace("'", "''") & "' OR main_title='" & Me.TextBox2.Text.Replace("'", "''") & "')")
        Else
            dt = GetDataTable("Select Count(*) From tblCoaMain WHERE coa_main_id <> " & Me.CurrentId & " AND main_code='" & Me.TextBox1.Text.Replace("'", "''") & "'")
        End If
        If dt IsNot Nothing Then
            If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                ShowErrorMessage("Account already exists. ")
                Exit Sub
            End If
        End If
        'End Task:2766

        'R-974 Ehtisham ul haq confrimation msgs on save update and delet have been removed on 28-12-13
        If Not btnSave.Text = "Save" And Not btnSave.Text = "&Save" Then
            If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
        End If

        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()

        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "&Save" Then
                cm.CommandText = "insert into tblCoaMain(main_code,main_title,main_type) values('" & Me.TextBox1.Text & "',N'" & Me.TextBox2.Text.Replace("'", "''") & "','" & Me.ComboBox1.SelectedItem & "')"
            Else
                cm.CommandText = "update tblcoaMain set main_code='" & Me.TextBox1.Text & "',main_title=N'" & Me.TextBox2.Text.Replace("'", "''") & "', main_type='" & Me.ComboBox1.SelectedItem & "' where coa_main_id=" & Me.CurrentId
            End If
            cm.ExecuteNonQuery()
            'msg_Information(str_informSave)
            Me.CurrentId = 0
            Try
                ''insert Activity Log
                SaveActivityLog("Accounts", Me.Text, IIf(Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.AccountTransaction, Me.TextBox1.Text.Trim, True)
            Catch ex As Exception
                Throw ex
            Finally
                Me.Cursor = Cursors.Default
            End Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
        End Try
        Me.RefreshForm()
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Me.RefreshForm()
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If Not Datagridview1.GetRow Is Nothing Then Me.EditRecord()
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        If Not Datagridview1.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)

            Exit Sub
        End If
        If IsValidToDelete("tblCOAMainSub", "coa_main_id", Me.Datagridview1.GetRow.Cells("coa_main_id").Value) = True Then
            If msg_Confirm(str_ConfirmDelete) = True Then
                Try
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    Dim cm As New OleDbCommand

                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "delete from tblCoaMain where Coa_Main_Id=" & Me.Datagridview1.CurrentRow.Cells(0).Value
                    cm.ExecuteNonQuery()
                    'msg_Information(str_informDelete)
                    Me.CurrentId = 0

                    Try
                        ''insert Activity Log
                        SaveActivityLog("Accounts", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, Datagridview1.CurrentRow.Cells("main_code").Value, True)
                    Catch ex As Exception

                    End Try

                Catch ex As Exception
                    msg_Error("Error occured while deleting information: " & ex.Message)
                Finally
                    Me.lblProgress.Visible = False
                    Con.Close()

                End Try
                Me.RefreshForm()
            End If
        Else
            msg_Error(str_ErrorDependentRecordFound)
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Me.RefreshForm()

    End Sub
#Region "New Security Enhacement"
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.DefMainAcc)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each Rightsdt As GroupRights In Rights
                    If Rightsdt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rightsdt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
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
    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Datagridview1.DoubleClick
        If Not Datagridview1.GetRow Is Nothing Then Me.EditRecord()
    End Sub
    Private Sub HelpToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripButton.Click
        Try



        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@FromDate", Date.Today.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Date.Today.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("PLevel", "Detail A/c")
            ShowReport("rptChartofAccountsTrial", "{vwGlCOADetail.main_type} = '" & Me.ComboBox1.Text & "'")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From tblCOAMain WHERE main_code='" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.ComboBox1.SelectedValue = dt.Rows(0).Item("main_type").ToString
                        Me.TextBox1.Text = dt.Rows(0).Item("main_code").ToString
                        Me.TextBox2.Text = dt.Rows(0).Item("main_title").ToString
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
    Private Sub Datagridview1_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles Datagridview1.ColumnButtonClick
        Try
            If Me.Datagridview1.RowCount = 0 Then Exit Sub
            If e.Column.Key = "Column1" Then
                frmMain.LoadControl("frmSubAccount")
                System.Threading.Thread.Sleep(500)
                frmSubAccount.Label5_Click(Nothing, Nothing)
                frmSubAccount.ComboBox1.SelectedValue = Me.Datagridview1.GetRow.Cells("coa_main_id").Value
                frmSubAccount.IsSelectedForm = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Datagridview1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Datagridview1.KeyDown
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Me.btnEdit, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            DeleteToolStripButton_Click(btnDelete, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub lblProgress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblProgress.Click

    End Sub
End Class
