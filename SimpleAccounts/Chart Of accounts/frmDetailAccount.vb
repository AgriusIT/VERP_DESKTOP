''03-Aug-2014 Task:2766 Imran Ali Duplicate Account Validation (Craft)
'31-July-2017       TFS# 1103       R@! Shahid      Deactivated Account should not display on Trial Balance Screen

Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Public Class frmDetailAccount
    Implements IGeneral
    Dim CurrentId As Integer
    Dim Code As String
    Dim OpeningBalance As OpeningBalance
    Dim OpeningBalanceDt As OpeningBalanceDt
    Dim MasterVoucherId As Integer = 0
    Dim VoucherTypeId As Integer = 0
    Dim AccountId As Integer = 0
    Dim IsLoadedForm As Boolean = False
    Public IsSelectedForm As Boolean = False

    Private Sub frmDetailAccount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            If Me.BtnSave.Enabled = True Then
                SaveToolStripButton_Click(BtnSave, Nothing)
            End If
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
        If e.KeyCode = Keys.D And e.Alt Then
            If Me.BtnSave.Enabled = False Then
                RemoveHandler Me.BtnDelete.Click, AddressOf DeleteToolStripButton_Click
            End If
        End If
    End Sub
    Private Sub frmDetailAccount_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
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
        Try

            Dim dt As New DataTable
            dt = GetDataTable("select voucher_id, voucher_date from tblVoucher where voucher_code='Opening'")
            If dt.Rows.Count = 0 Then

                Me.txtOpening.Visible = True
                lblOpeningBalance.Visible = True
                grd.RootTable.Columns("OpeningBalance").Visible = True

            Else

                Me.txtOpening.Visible = False
                lblOpeningBalance.Visible = False
                grd.RootTable.Columns("OpeningBalance").Visible = False

            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub
    Sub RefreshForm()
        'Ali Faisal : TFS1809 : Code to save for child account of detail account
        Try
            Me.BtnSave.Text = "&Save"
            Me.txtCode.Text = ""
            Me.txtCode.Focus()
            Me.TextBox2.Text = ""
            Me.txtOpening.Text = 0
            Me.cmbAccessLevel.SelectedIndex = 0
            Me.lblAlert.Visible = False
            Me.BindGrid()
            Me.SelectAccountCode()
            If Me.ComboBox1.SelectedIndex > 0 AndAlso Me.cmbParent.SelectedIndex = 0 Then
                Me.txtCode.Text = Microsoft.VisualBasic.Right(GetNextDocNo(Me.txtMainCode.Text, (Me.txtMainCode.Text.Trim.Length + 1), "tblCOAMainSubSubDetail", "detail_code", (Me.txtMainCode.Text.Trim.Length + 6)), 5)
            End If
            Me.GetSecurityRights()
            IsSelectedForm = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub GetNewAccountCode()
    End Sub
    Sub BindGrid()
        Try

            If Me.ComboBox1.SelectedValue > 0 Then
                Dim adp As New OleDbDataAdapter
                Dim dt As New DataTable
                Dim strSql As String
                strSql = "  SELECT dbo.tblCOAMainSubSubDetail.coa_detail_id, dbo.tblCOAMainSubSubDetail.main_sub_sub_id,  " & _
                            "dbo.tblCOAMainSubSub.sub_sub_title + '-' + dbo.tblCOAMainSubSub.sub_sub_code AS SubAc, dbo.tblCOAMainSubSubDetail.detail_code, " & _
                            "dbo.tblCOAMainSubSubDetail.detail_title, OpeningBalance, IsNull(tblCOAMainSubSubDetail.Active,0) as Active, Parent_Id, IsNull(AccessLevel, 'Everyone') as AccessLevel " & _
                            "FROM  dbo.tblCOAMainSubSub INNER JOIN " & _
                            "dbo.tblCOAMainSubSubDetail ON dbo.tblCOAMainSubSub.main_sub_sub_id = dbo.tblCOAMainSubSubDetail.main_sub_sub_id " & _
                            "  where tblCOAMainSubsub.main_sub_sub_id = " & Me.ComboBox1.SelectedValue & _
                            " ORDER BY detail_code "
                adp = New OleDbDataAdapter(strSql, Con)
                adp.Fill(dt)
                ' Me.DataGridView1.DataSource = dt
                Me.grd.DataSource = dt
                Me.grd.ExpandRecords()


                strSql = "  SELECT dbo.tblCOAMainSubSubDetail.coa_detail_id,  " & _
                           "dbo.tblCOAMainSubSubDetail.detail_title + '-' + tblCOAMainSubSubDetail.detail_code AS DetailAccount " & _
                           "FROM  dbo.tblCOAMainSubSub INNER JOIN " & _
                           "dbo.tblCOAMainSubSubDetail ON dbo.tblCOAMainSubSub.main_sub_sub_id = dbo.tblCOAMainSubSubDetail.main_sub_sub_id " & _
                           "  where tblCOAMainSubsub.main_sub_sub_id = " & Me.ComboBox1.SelectedValue & _
                           " ORDER BY tblCOAMainSubSubDetail.detail_title "
                FillDropDown(Me.cmbParent, strSql)
            End If

        Catch ex As Exception
            Throw ex
        End Try
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
    ''' <summary>
    ''' Ali Faisal : TFS1809 : Code to save for child account of detail account
    ''' </summary>
    ''' <remarks></remarks>
    Sub SelectDetailAccountCode()
        If Me.cmbParent.SelectedValue > 0 Then
            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            Dim strSql As String
            strSql = " SELECT detail_code  AS Title From tblCOAMainSubSubDetail where coa_detail_id = " & Me.cmbParent.SelectedValue
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

        FillDropDown(Me.ComboBox1, "select main_sub_sub_id, sub_sub_title +' - '+ sub_sub_code from tblCoaMainsubSub order by 2")
    End Sub
    Sub EditRecord()
        Me.CurrentId = Me.grd.CurrentRow.Cells("coa_detail_id").Value
        Dim str As String = grd.CurrentRow.Cells("detail_code").Value
        Me.txtMainCode.Text = Microsoft.VisualBasic.Left(str, str.Length - 6) 'InStr(1,  str, "-") - 2) 'Mid(str, 1, InStr(1, str, "-") - 1)
        Me.txtCode.Text = Microsoft.VisualBasic.Right(str, 5)
        Me.TextBox2.Text = grd.CurrentRow.Cells("Detail_title").Value
        Me.txtOpening.Text = grd.CurrentRow.Cells("OpeningBalance").Value.ToString
        Me.chkActive.Checked = grd.GetRow.Cells("Active").Value
        Me.cmbAccessLevel.Text = grd.GetRow.Cells("AccessLevel").Value.ToString
        Me.cmbParent.SelectedValue = Val(grd.CurrentRow.Cells("Parent_Id").Value.ToString)
        Me.BtnSave.Text = "&Update"
        If grd.GetRow.Cells("AccessLevel").Value.ToString = "Everyone" Then
            Me.cmbAccessLevel.SelectedIndex = 0
            Me.lblAlert.Visible = False
        Else
            Me.cmbAccessLevel.Text = grd.GetRow.Cells("AccessLevel").Value.ToString
            Me.lblAlert.Visible = True

        End If
        Me.SelectAccountCode()
        Me.GetSecurityRights()
    End Sub
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Me.BtnSave.Enabled = False Then Exit Sub
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

        ''TASK TFS1782 Muhammad Ameen on 12-12-2017. Here is checked to allow or not allow duplicate account name.
        Dim DuplicateAccountName As Boolean = False
        If Not getConfigValueByType("DuplicateAccountName").ToString = "Error" Then
            DuplicateAccountName = Convert.ToBoolean(getConfigValueByType("DuplicateAccountName").ToString)
        End If
        ''03-Aug-2014 Task:2766 Imran Ali Duplicate Account Validation (Craft)
        Dim dt As New DataTable
        If DuplicateAccountName = False Then
            dt = GetDataTable("Select Count(*) From tblCOAMainSubsubDetail WHERE coa_detail_id <> " & Me.CurrentId & " AND ([Detail_Code]='" & CStr(Me.txtMainCode.Text & "-" & Code).Replace("'", "''") & "' OR [Detail_Title]='" & Me.TextBox2.Text.Replace("'", "''") & "')")
        Else
            dt = GetDataTable("Select Count(*) From tblCOAMainSubsubDetail WHERE coa_detail_id <> " & Me.CurrentId & " AND [Detail_Code]='" & CStr(Me.txtMainCode.Text & "-" & Code).Replace("'", "''") & "'")
        End If
        If dt IsNot Nothing Then
            If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                ShowErrorMessage("Account already exists. ")
                Exit Sub
            End If
        End If
        'End Task:2766

        'If isvalidatetoupdate Then
        'R-974 Eht_confirmation msgs removed from save update on 28-12-13
        If Not BtnSave.Text = "Save" And Not BtnSave.Text = "&Save" Then
            If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
        End If
        Me.lblProgress.Text = "Processing Please wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Dim cm As New OleDbCommand
        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con
        Me.txtCode_Leave(Me, New EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "&Save" Then
                'cm.CommandText = "INSERT INTO [tblCOAMainSubsubDetail]([main_sub_sub_id], [Detail_code], [Detail_title],OpeningBalance) " & _
                '                    "VALUES( " & Me.ComboBox1.SelectedValue & ", '" & Me.txtMainCode.Text & "-" & Code & "', '" & Me.TextBox2.Text & "'," & Me.txtOpening.Text & ")SELECT @@Identity"
                'cm.ExecuteNonQuery()
                cm.CommandText = "INSERT INTO [tblCOAMainSubsubDetail]([main_sub_sub_id], [Detail_code], [Detail_title],OpeningBalance, Active, Parent_Id, AccessLevel) " & _
                                    "VALUES( " & Me.ComboBox1.SelectedValue & ", '" & Me.txtMainCode.Text & "-" & Code & "', N'" & Me.TextBox2.Text & "'," & Me.txtOpening.Text & ", " & IIf(Me.chkActive.Checked = True, 1, 0) & ", " & Me.cmbParent.SelectedValue & ", '" & Me.cmbAccessLevel.Text.ToString & "')SELECT @@Identity"
                cm.ExecuteNonQuery()

            Else

                '31-July-2017       TFS# 1103       R@! Shahid      Deactivated Account should not display on Trial Balance Screen
                'Implementing this change to block deactivation of those account which have some balance.
                If chkActive.Checked = False Then
                    '// Validating balance of the account
                    If Val(GetDataTable("select round(isnull(sum(debit_amount) - sum(credit_amount),0),2) from tblVoucherDetail where coa_detail_id=" & Me.CurrentId).Rows(0).Item(0).ToString) <> 0 Then
                        ShowErrorMessage("Can't update record. Account must have zero balance to deactivate.")
                        Exit Sub
                    End If
                End If
                '//End TFS# 1103

                'cm.CommandText = "update[tblCOAMainSubsubDetail] set [main_Sub_sub_id]= " & Me.ComboBox1.SelectedValue & ", [Detail_code]='" & Me.txtMainCode.Text & "-" & Code & "', [Detail_title]='" & Me.TextBox2.Text & "', OpeningBalance=" & Val(Me.txtOpening.Text) & " where Coa_Detail_id=" & Me.CurrentId & " "
                'cm.ExecuteNonQuery()
                cm.CommandText = "update[tblCOAMainSubsubDetail] set [main_Sub_sub_id]= " & Me.ComboBox1.SelectedValue & ", [Detail_code]='" & Me.txtMainCode.Text & "-" & Code & "', [Detail_title]=N'" & Me.TextBox2.Text & "', OpeningBalance=" & Val(Me.txtOpening.Text) & ", Active=" & IIf(Me.chkActive.Checked = True, 1, 0) & ", Parent_Id=" & Me.cmbParent.SelectedValue & ", AccessLevel='" & Me.cmbAccessLevel.Text.ToString & "' where Coa_Detail_id=" & Me.CurrentId & " "
                cm.ExecuteNonQuery()
                'cm.CommandText = " Update ArticleDefTable set ArticleDescription = '" & Me.TextBox2.Text & "' where AccountID = " & Me.CurrentId
                'cm.ExecuteNonQuery()
            End If
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
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.RefreshForm()

        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        If Not Me.grd.GetRow Is Nothing Then Me.EditRecord()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            Me.RefreshForm()
            Me.TextBox2.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        'DefMainAcc.ShowDialog()
        Me.FillComboBox()
    End Sub
    Private Sub txtCode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.Leave
        Try
            If Me.cmbParent.SelectedIndex > 0 Then
                If Me.txtCode.TextLength = 1 Then
                    Code = "00" & Me.txtCode.Text
                ElseIf txtCode.TextLength = 2 Then
                    Code = "0" & Me.txtCode.Text
                Else
                    Code = Me.txtCode.Text
                End If
            Else
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
            End If
            Me.txtCode.Text = Code
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not grd.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If IsValidToDelete("tblVoucherDetail", "Coa_Detail_id", Me.grd.CurrentRow.Cells("coa_detail_id").Value.ToString) = True Then
            If msg_Confirm(str_ConfirmDelete) = True Then
                Me.Cursor = Cursors.WaitCursor
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                Try
                    Dim cm As New OleDbCommand
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "delete from tblCoaMainSubSubDetail where Coa_Detail_id=" & Me.grd.CurrentRow.Cells("coa_detail_id").Value.ToString
                    cm.ExecuteNonQuery()
                    'msg_Information(str_informDelete)
                    Me.CurrentId = 0
                Catch ex As Exception
                    msg_Error("Error occured while deleting information: " & ex.Message)
                Finally
                    Me.lblProgress.Visible = False
                    Con.Close()
                    Me.Cursor = Cursors.Default
                End Try
                Try
                    ''insert Activity Log
                    SaveActivityLog("Accounts", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, grd.CurrentRow.Cells("detail_code").Value.ToString, True)
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

    Private Sub grd_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.DoubleClick
        If Not Me.grd.GetRow Is Nothing Then Me.EditRecord()
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            OpeningBalance = New OpeningBalance
            OpeningBalance.Voucher_Id = MasterVoucherId
            OpeningBalance.LocationId = 1
            OpeningBalance.voucher_code = "Opening-Balance"
            OpeningBalance.voucher_no = "Opening-Balance"
            OpeningBalance.voucher_date = DateTime.Now
            OpeningBalance.voucher_type = GetVoucherTypeId("OP")
            If Me.BtnSave.Text = "&Save" Then
                OpeningBalance.detail_Id = AccountId
            Else
                OpeningBalance.detail_Id = Me.CurrentId
            End If
            OpeningBalance.Reference = String.Empty
            OpeningBalance.UserName = LoginUserName
            OpeningBalance.OpeningBalanceDt = New List(Of OpeningBalanceDt)
            OpeningBalanceDt = New OpeningBalanceDt
            OpeningBalanceDt.voucher_id = MasterVoucherId
            OpeningBalanceDt.location_id = 1
            If Me.BtnSave.Text = "&Save" Then
                OpeningBalanceDt.coa_detail_id = AccountId
            Else
                OpeningBalanceDt.coa_detail_id = Me.CurrentId
            End If
            If Val(Me.txtOpening.Text) < 0 Then
                OpeningBalanceDt.credit_amount = Val(Me.txtOpening.Text)
            ElseIf Val(Me.txtOpening.Text) > 0 Then
                OpeningBalanceDt.debit_amount = Val(Me.txtOpening.Text)
            Else
                OpeningBalanceDt.debit_amount = 0
                OpeningBalanceDt.credit_amount = 0
            End If
            OpeningBalance.OpeningBalanceDt.Add(OpeningBalanceDt)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As New DataTable
            dt = New OpeningBalanceDAL().GetOpeningBalanceById(AccountId)
            If dt.Rows.Count > 0 Then
                MasterVoucherId = dt.Rows(0).Item(0).ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New OpeningBalanceDAL().AddOpeningBalance(OpeningBalance) Then Return True
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

    End Function
    Function GetVoucherTypeId(Optional ByVal Condition As String = "") As Integer
        Try
            Dim VoucherTypeId As Double = 0
            Dim str As String = String.Empty
            Dim dt As New DataTable
            Dim adp As OleDbDataAdapter
            If Condition = "OP" Then
                str = "Select voucher_type_id From tblDefVoucherType WHERE voucher_type='OP'"
                adp = New OleDbDataAdapter(str, Con)
                adp.Fill(dt)
            End If
            VoucherTypeId = Val(dt.Rows(0).Item(0).ToString)
            Return VoucherTypeId
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub
    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From tblCOAMainSubSubDetail WHERE detail_code='" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.CurrentId = dt.Rows(0).Item("coa_detail_id").ToString
                        Me.ComboBox1.SelectedValue = dt.Rows(0).Item("main_sub_sub_id").ToString
                        Me.txtMainCode.Text = Microsoft.VisualBasic.Left(dt.Rows(0).Item("detail_code").ToString, dt.Rows(0).Item("detail_code").ToString.Length - 6)
                        Me.txtCode.Text = Microsoft.VisualBasic.Right(dt.Rows(0).Item("detail_code").ToString, 5)
                        Me.TextBox2.Text = dt.Rows(0).Item("detail_title").ToString
                        Me.txtOpening.Text = dt.Rows(0).Item("OpeningBalance").ToString
                        Me.cmbAccessLevel.Text = IIf(dt.Rows(0).Item("AccessLevel").ToString = "", "Everyone", dt.Rows(0).Item("AccessLevel").ToString)
                        Me.BtnSave.Text = "&Update"
                        Me.SelectAccountCode()
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



    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click

    End Sub

    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        'R-974 Ehtisham short keys are added on 29-12-13
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Me.BtnEdit, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            DeleteToolStripButton_Click(BtnDelete, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1809 : Code to save for child account of detail account
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbParent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbParent.SelectedIndexChanged
        Try
            SelectDetailAccountCode()
            If Me.cmbParent.SelectedIndex > 0 Then
                Me.txtCode.Text = Microsoft.VisualBasic.Right(GetNextDocNo(Me.txtMainCode.Text, (Me.txtMainCode.Text.Trim.Length + 1), "tblCOAMainSubSubDetail", "detail_code", (Me.txtMainCode.Text.Trim.Length + 6)), 3)
            Else
                RefreshForm()
            End If
            Me.TextBox2.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class