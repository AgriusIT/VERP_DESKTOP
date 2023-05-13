Imports System.Data.oledb
Imports System.Math

Public Class frmDefUser

    Private Sub tblUser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RefreshControls()
        DisplayRecord()
        '        DisplayForms()
    End Sub

    Private Sub DisplayRecord()
        Dim str As String
        str = "SELECT  User_ID, USer_Name, USer_Code, Password, Active, IsNull(PriceAllowedRight,0) as PriceAllowedRight, IsNull(PostingRight,0) As PostingRight from tblUser"
        FillGrid(grdSaved, str)
        grdSaved.Columns(0).Visible = False
        grdSaved.Columns(3).Visible = False

        grdSaved.Columns(1).HeaderText = "Name"
        grdSaved.Columns(2).HeaderText = "Code"
        grdSaved.Columns(4).HeaderText = "Active"
        grdSaved.Columns(5).HeaderText = "Price Right"
        grdSaved.Columns(6).HeaderText = "Posting Right"

        'grdSaved.Columns(4).co = DataGridViewCheckBoxCell
        grdSaved.Columns(1).Width = 150
        grdSaved.Columns(2).Width = 150
        grdSaved.Columns(4).Width = 50
        'grdSaved.Columns(7).Width = 150

        Dim Ri As Integer
        Dim Ci As Integer
        For Ri = 0 To Me.grdSaved.Rows.Count - 1
            For Ci = 1 To 3
                Me.grdSaved.Rows(Ri).Cells(Ci).Value = Decrypt(Me.grdSaved.Rows(Ri).Cells(Ci).Value)
            Next
        Next


    End Sub

    Private Sub RefreshControls()
        Me.BtnSave.Text = "&Save"
        txtName.Text = ""
        txtCode.Text = ""
        txtPassword.Enabled = True
        txtConPsw.Enabled = True
        txtPassword.Text = ""
        txtConPsw.Text = ""
        chkActive.Checked = True
        txtUserID.Text = ""
        TabControl1.Enabled = False
        grdForm.DataSource = Nothing
        '  grdReport.DataSource = Nothing
        Me.chkView.Checked = False
        Me.chkSave.Checked = False
        Me.chkUpdate.Checked = False
        Me.chkDelete.Checked = False
        Me.chkPrint.Checked = False
        Me.chkExport.Checked = False
        Me.chkPriceAllowed.Checked = False
        Me.chkPostingUser.Checked = False
        Me.GetSecurityRights()
    End Sub

    Private Function Save() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer = 0

        objCon = Con 'New oledbConnection("Password=sa;Integrated Security Info=False;User ID=sa;Password=sa; Initial Catalog=simplepos;Data Source=rai")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            objCommand.CommandText = "Insert into tblUser (User_Name,User_Code,Password,Active, PriceAllowedRight, PostingRight) values( " _
                                    & " '" & Encrypt(txtName.Text) & "','" & Encrypt(txtCode.Text.ToUpper) & "','" & Encrypt(txtPassword.Text) & "', " & Abs(Val(chkActive.Checked)) & ", " & IIf(Me.chkPriceAllowed.Checked = True, 1, 0) & ", " & IIf(Me.chkPostingUser.Checked = True, 1, 0) & ") Select @@Identity "
            Dim identity As Integer = Convert.ToInt32(objCommand.ExecuteScalar())
            objCommand.CommandText = ""
            trans.Commit()
            Save = True
            Try
                ''insert Activity Log
                SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Security, identity)
            Catch ex As Exception

            End Try

        Catch ex As Exception
            trans.Rollback()
            Save = False
        End Try


    End Function

    Private Function FormValidate() As Boolean
        If txtName.Text = "" Then
            msg_Error("Please enter user name")
            txtName.Focus() : FormValidate = False : Exit Function
        End If

        If txtCode.Text = "" Then
            msg_Error("Please enter user code")
            txtCode.Focus() : FormValidate = False : Exit Function
        End If

        If txtPassword.Text = "" Then
            msg_Error("Please enter password")
            txtPassword.Focus() : FormValidate = False : Exit Function
        End If

        If Trim(txtPassword.Text) <> Trim(txtConPsw.Text) Then
            msg_Error("Password does not match")
            txtConPsw.Focus() : FormValidate = False : Exit Function
        End If

        Return True

    End Function

    Private Function Update_Record() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer = 0

        objCon = Con 'New oledbConnection("Password=sa;Integrated Security Info=False;User ID=sa;Password=lumensoft2003; Initial Catalog=ShoesStore;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            objCommand.CommandText = "Update tblUser set User_Name ='" & Encrypt(txtName.Text) & "',User_Code='" & Encrypt(txtCode.Text.ToUpper) & "', Password='" & Encrypt(txtPassword.Text) & "', " _
            & " Active=" & Abs(Val(chkActive.Checked)) & ", PriceAllowedRight=" & IIf(Me.chkPriceAllowed.Checked = True, 1, 0) & ", PostingRight=" & IIf(Me.chkPostingUser.Checked = True, 1, 0) & "  Where User_ID= " & txtUserID.Text & " "

            objCommand.ExecuteNonQuery()

            trans.Commit()
            Update_Record = True

            Try
                ''insert Activity Log
                SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Security, txtUserID.Text)
            Catch ex As Exception

            End Try

        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
        End Try


    End Function
    Sub EditRecord()
        txtUserID.Text = grdSaved.CurrentRow.Cells(0).Value
        txtName.Text = grdSaved.CurrentRow.Cells(1).Value
        txtCode.Text = grdSaved.CurrentRow.Cells(2).Value
        txtPassword.Text = grdSaved.CurrentRow.Cells(3).Value & ""
        txtConPsw.Text = grdSaved.CurrentRow.Cells(3).Value & ""
        txtPassword.Enabled = False
        txtConPsw.Enabled = False
        chkActive.Checked = grdSaved.CurrentRow.Cells(4).Value
        chkPriceAllowed.Checked = grdSaved.CurrentRow.Cells(5).Value
        chkPostingUser.Checked = grdSaved.CurrentRow.Cells(6).Value
        'Call DisplayDetail(grdSaved.currentrow.Cells(5).Value)
        DisplayForms()
        DisplayReport()
        Me.BtnSave.Text = "&Update"
        TabControl1.Enabled = True
        Me.GetSecurityRights()
    End Sub

    Private Sub DisplayForms()
        Dim str As String
        'str = "SELECT    convert(bit, 0) AS Selection, Form_ID, Form_Name, CONVERT(bit, 0) AS VIEW_R, CONVERT(bit, 0) AS SAVE_R, " _
        '      & "CONVERT(bit, 0) AS UPDATE_R, CONVERT(bit, 0) AS DELETE_R FROM tblForm " _
        '      & " WHERE (Form_Type = 'Form') AND (Form_ID NOT IN (SELECT     Form_ID " _
        '      & " FROM  tblUserRights Where User_ID=" & Val(txtUserID.Text) & ")) ORDER BY Module_Name"

        str = "SELECT convert(bit, 0) AS Selection, Form_ID, Form_Caption, CONVERT(bit, 0) AS VIEW_R, CONVERT(bit, 0) AS SAVE_R, " _
             & " CONVERT(bit, 0) AS UPDATE_R, CONVERT(bit, 0) AS DELETE_R, Convert(bit, 0) as [Print], Convert(bit , 0) as [Export] FROM tblForm WHERE (Form_Type = 'Form') AND " _
             & " (Form_ID NOT IN (SELECT     Form_ID FROM  tblUserRights Where User_ID=" & Val(txtUserID.Text) & ")) " _
             & " union " _
             & " SELECT     CONVERT(bit, 1) AS Selection, UR.Form_ID, F.Form_Caption, UR.View_Rights as View_R, UR.Save_Rights as Save_R, UR.Update_Rights as Update_R, UR.Delete_Rights as Delete_R " _
             & " , UR.Print_Rights as [Print] ,UR.Export_Rights AS Export" _
             & " FROM    tblUserRights UR INNER JOIN tblForm F ON UR.Form_ID = F.Form_ID WHERE(UR.User_ID = " & Val(txtUserID.Text) & ") order by 3"

        FillGrid(grdForm, str)
        grdForm.Columns(1).Visible = False
        grdForm.Columns(0).Visible = False



        grdForm.Columns(2).HeaderText = "Form Name"
        grdForm.Columns(3).HeaderText = "View"
        grdForm.Columns(4).HeaderText = "Save"
        grdForm.Columns(5).HeaderText = "Update"
        grdForm.Columns(6).HeaderText = "Delete"

        'grdSaved.Columns(4).co = DataGridViewCheckBoxCell
        grdForm.Columns(0).Width = 65
        grdForm.Columns(2).Width = 100
        grdForm.Columns(3).Width = 35
        grdForm.Columns(4).Width = 37
        grdForm.Columns(5).Width = 50
        grdForm.Columns(6).Width = 45
        grdForm.Columns(7).Width = 35
        grdForm.Columns(8).Width = 45


    End Sub

    Private Function SaveForm() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        'Dim User_ID As Integer


        objCon = Con 'New oledbConnection("Password=sa;Integrated Security Info=False;User ID=sa;Password=lumensoft2003; Initial Catalog=ShoesStore;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans

            'If btnSave.Text = "Save" Then
            '    User_ID = "ident_current('tblUSer')"
            'Else
            '    User_ID = txtUserID.Text
            'End If
            'objCon.BeginTransaction()
            'objCommand.CommandText = ""
            objCommand.CommandText = "delete from tblUserRights where User_ID= " & Val(txtUserID.Text) & ""
            objCommand.ExecuteNonQuery()

            Me.grdForm.Update()


            For i = 0 To grdForm.Rows.Count - 1
                'If CBool(grdForm.Rows(i).Cells(0).Value) = True Then
                objCommand.CommandText = ""
                objCommand.CommandText = "Insert into tblUserRights (User_ID, Form_ID,View_Rights,Save_Rights,Update_Rights,Delete_Rights, Print_Rights , Export_Rights) values( " _
                                        & " " & Val(txtUserID.Text) & ", " & Val(grdForm.Rows(i).Cells(1).Value) & "," & Abs(Val(grdForm.Rows(i).Cells(3).Value)) & "," & Abs(Val(grdForm.Rows(i).Cells(4).Value)) & ", " _
                                        & " " & Abs(Val(grdForm.Rows(i).Cells(5).Value)) & ", " & Abs(Val(grdForm.Rows(i).Cells(6).Value)) & ", " & Abs(Val(grdForm.Rows(i).Cells(7).Value)) & ", " & Abs(Val(grdForm.Rows(i).Cells(8).Value)) & "  ) "

                objCommand.ExecuteNonQuery()
                'End If


            Next

            objCommand.CommandText = ""


            trans.Commit()
            SaveForm = True
        Catch ex As Exception
            trans.Rollback()
            SaveForm = False
        End Try


    End Function

    Private Function SaveReport() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer = 0
        'Dim User_ID As Integer


        objCon = Con 'New oledbConnection("Password=sa;Integrated Security Info=False;User ID=sa;Password=lumensoft2003; Initial Catalog=ShoesStore;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans

            'If btnSave.Text = "Save" Then
            '    User_ID = "ident_current('tblUSer')"
            'Else
            '    User_ID = txtUserID.Text
            'End If
            'objCon.BeginTransaction()
            'For i = 0 To grdReport.Rows.Count - 1
            '    'If CBool(grdReport.Rows(i).Cells(0).Value) = True Then


            '    Dim strSql As String = "Insert into tblUserRights (User_ID, Form_ID,View_Rights,Print_Rights,Export_Rights) values( "
            '    strSql = strSql + "" & Val(txtUserID.Text) & ", "
            '    strSql = strSql + "" & Val(grdReport.Rows(i).Cells(2).Value) & ","
            '    strSql = strSql + "" & Abs(Val(grdReport.Rows(i).Cells(3).Value.ToString)) & ","
            '    strSql = strSql + "" & Abs(Val(grdReport.Rows(i).Cells(4).Value.ToString)) & ", "
            '    strSql = strSql + "" & Abs(Val(grdReport.Rows(i).Cells(5).Value.ToString)) & ") "

            '    objCommand.CommandText = strSql
            '    objCommand.ExecuteNonQuery()
            '    'End If


            'Next

            objCommand.CommandText = ""


            trans.Commit()
            SaveReport = True
        Catch ex As Exception
            trans.Rollback()
            SaveReport = False
        End Try


    End Function

    Private Sub DisplayReport()
        Dim str As String
        str = "SELECT    convert(bit, 0) AS Selection, Form_ID, Form_Caption, CONVERT(bit, 0) AS VIEW_R,  " _
             & " CONVERT(bit, 0) AS Print_R, CONVERT(bit, 0) AS Export_R FROM tblForm WHERE (Form_Type = 'Report') AND " _
             & " (Form_ID NOT IN (SELECT     Form_ID FROM  tblUserRights Where User_ID=" & Val(txtUserID.Text) & ")) " _
             & " union " _
             & " SELECT     CONVERT(bit, 1) AS Selection, UR.Form_ID, F.Form_Caption, UR.View_Rights as View_R, UR.Print_Rights as Print_R, UR.Export_Rights as Export_R " _
             & " FROM         tblUserRights UR INNER JOIN tblForm F ON UR.Form_ID = F.Form_ID WHERE(UR.User_ID = " & Val(txtUserID.Text) & ") and F.Form_Type='Report'"

        'FillGrid(grdReport, str)
        'grdReport.Columns(1).Visible = False
        'grdReport.Columns(0).Visible = False



        'grdReport.Columns(2).HeaderText = "Form Name"
        'grdReport.Columns(3).HeaderText = "View"
        'grdReport.Columns(4).HeaderText = "Print"
        'grdReport.Columns(5).HeaderText = "Export"

        ''grdSaved.Columns(4).co = DataGridViewCheckBoxCell
        'grdReport.Columns(0).Width = 65
        'grdReport.Columns(2).Width = 100
        'grdReport.Columns(3).Width = 50
        'grdReport.Columns(4).Width = 50
        'grdReport.Columns(5).Width = 50



    End Sub

    Private Sub grdSaved_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSaved.CellDoubleClick
        Me.EditRecord()
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.EditRecord()

    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If FormValidate() Then
            If BtnSave.Text = "Save" Or BtnSave.Text = "&Save" Then
                If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                If Save() Then
                    msg_Information(str_informSave)
                    RefreshControls()
                    DisplayRecord()
                Else
                    msg_Error("Error! Record not added")

                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                If Update_Record() Then
                    msg_Information(str_informUpdate)
                    RefreshControls()
                    DisplayRecord()
                Else
                    msg_Error("Error! Record not updated")

                End If
            End If

        End If
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.RefreshControls()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        If SaveForm() Then
            msg_Information(str_informSave)
            RefreshControls()
            'DisplayRecord()
        Else
            msg_Error("Error! Record not added")

        End If
    End Sub

    Private Sub ToolStripButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If SaveReport() Then
            msg_Information(str_informSave)
            RefreshControls()
            'DisplayRecord()
        Else
            msg_Error("Error! Record not added")

        End If
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click

    End Sub

    Private Sub GetSecurityRights()
        ''TODO Implement Security
        If LoginUserId = 0 Then
            Me.lnklblSetDefault.Visible = True
            Exit Sub
        End If
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefUser)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                            Me.lnklblSetDefault.Visible = False
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                            Me.lnklblSetDefault.Visible = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString

                    End If
                End If
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkView.CheckedChanged, chkSave.CheckedChanged, chkUpdate.CheckedChanged, chkDelete.CheckedChanged, chkPrint.CheckedChanged, chkExport.CheckedChanged

        If grdForm.Rows.Count = 0 Then Exit Sub

        Dim colIndex As Integer = 0
        Dim chk As CheckBox = CType(sender, CheckBox)

        Select Case chk.Name
            Case Me.chkView.Name
                colIndex = 3
            Case Me.chkSave.Name
                colIndex = 4
            Case Me.chkUpdate.Name
                colIndex = 5
            Case Me.chkDelete.Name
                colIndex = 6
            Case Me.chkPrint.Name
                colIndex = 7
            Case Me.chkExport.Name
                colIndex = 8
        End Select

        For Each r As DataGridViewRow In Me.grdForm.Rows
            r.Cells(colIndex).Value = chk.Checked
        Next
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnklblSetDefault.LinkClicked
        Dim objCon As OleDbConnection
        objCon = Con
        Try
            If Not msg_Confirm("Do you want to reset the password?") = True Then Exit Sub
            Dim cmd As New OleDbCommand
            cmd.CommandType = CommandType.Text

            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            cmd.Connection = Con
            cmd.CommandText = "UPDATE    tblUser  SET Password = '" & Encrypt("123") & "' WHERE     (User_ID = " & Me.txtUserID.Text & ")"
            cmd.ExecuteNonQuery()
            msg_Information("Passwored is 123")

        Catch ex As Exception
            msg_Error(ex.Message)
        Finally
            objCon.Close()
        End Try

        ''insert Activity Log
        SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Security, Me.txtName.Text.ToString.Replace("'", "''"))
        RefreshControls()
        DisplayRecord()
    End Sub

    Private Sub chkPriceAllowed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPriceAllowed.CheckedChanged

    End Sub
End Class