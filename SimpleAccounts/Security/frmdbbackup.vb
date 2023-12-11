Imports SBDal
Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports Ionic.Zip
Imports SBModel

Public Class frmdbbackup
    Dim strBackup As String
    Dim strDate As String
    Dim dtDatabase As New DataTable
    Dim drDatabase As DataRow
    Dim dtLocation As New DataTable
    Dim drLocation As DataRow
    Dim Con As SqlConnection
    Dim dtServer As New DataTable
    Dim ServerName As String = String.Empty
    Dim UserName As String = String.Empty
    Dim Password As String = String.Empty
    Dim strBackupDBPath As String = String.Empty
    Dim key As String
    Private Sub btnbrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbrowse.Click
        Try
            If fbDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtlocation.Text = fbDialog.SelectedPath
                If Me.txtlocation.Text = String.Empty Then Exit Sub
                Me.lblPrograssBar1.Text = String.Empty
                Me.prgContinue1.Value = 0
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            'Finally
            '    Con.Close()
        End Try
    End Sub
    Private Sub frmdbbackup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            strBackupDBPath = getConfigValueByType("BackupDBPath").ToString

            'dtServer.TableName = "Servers"
            'dtServer.Columns.Add("ServerName", GetType(System.String))
            'dtDatabase.TableName = "Database"
            'dtDatabase.Columns.Add("DatabaseName", GetType(System.String))
            'dtLocation.TableName = "Location"
            'dtLocation.Columns.Add("LocationName", GetType(System.String))
            'txtServer_TextChanged(Nothing, Nothing)
            'FillCombo()
            'Dim str As String = String.Empty
            'If IO.File.Exists(str_ApplicationStartUpPath & "\DefaultLocation.Xml") Then
            '    dtLocation.ReadXml(str_ApplicationStartUpPath & "\DefaultLocation.Xml")
            '    Me.txtlocation.Text = dtLocation.Rows(0).Item(0).ToString
            'Else
            '    Dim DrLocation As DataRow
            '    DrLocation = dtLocation.NewRow
            '    DrLocation.Item(0) = "E:\Backup"
            '    dtLocation.Rows.InsertAt(DrLocation, 0)
            '    dtLocation.WriteXml(str_ApplicationStartUpPath & "\DefaultLocation.Xml")
            'End If

            Me.txtlocation.Text = strBackupDBPath

            'Dim ds As New DataSet
            'If IO.File.Exists(str_ApplicationStartUpPath & "\DefaultServer.Xml") Then
            '    ds.ReadXml(str_ApplicationStartUpPath & "\DefaultServer.Xml")
            '    Me.txtServer.Text = ds.Tables(0).Rows(0).Item(0).ToString
            'End If
            'btnReset_Click(Nothing, Nothing)
            GetSecurityRights()
            Me.ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Try
            If Me.txtDatabase.Text = String.Empty Then Exit Sub
            If Me.txtlocation.Text = String.Empty Then Exit Sub
            Me.prgContinue1.Value = 0
            Me.lblPrograssBar1.Text = String.Empty
            Me.lblPrograssBar1.Text = "Databse : " & Me.txtDatabase.Text & ""
            Do Until Me.prgContinue1.Value > 50
                Me.prgContinue1.Value = Me.prgContinue1.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            Me.lblPrograssBar1.Text = String.Empty
            Me.lblPrograssBar1.Text = "Set Location : " & Me.txtlocation.Text & ""
            Do Until Me.prgContinue1.Value > 75
                Me.prgContinue1.Value = Me.prgContinue1.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop

            Dim ConStrBuilder As New SqlClient.SqlConnectionStringBuilder(SQLHelper.CON_STR)
            ConStrBuilder.Item("Initial Catalog") = "master"
            ConStrBuilder.Password = ConPassword.ToString
            Dim Con1 As New SqlConnection(ConStrBuilder.ConnectionString.ToString)
            If Con1.State = ConnectionState.Closed Then Con1.Open()
            Dim mPassword As String = Decrypt(getConfigValueByType("DatabaseBackupPassword").ToString())
            strDate = Date.Now.Date.ToString("dd/MM/yyyy")
            ' strBackup = "Backup Database " & Me.txtDatabase.Text & " To Disk='" & Me.txtlocation.Text + "\" + Me.txtDatabase.Text + strDate.Replace("/", "") + ".Bak" & "' WITH MEDIAPASSWORD = '" & mPassword & "'"
            strBackup = "Backup Database " & Me.txtDatabase.Text & " To Disk='" & Me.txtlocation.Text + "\" + Me.txtDatabase.Text + strDate.Replace("/", "") + ".Bak' "
            Dim SqlCommand As SqlCommand = New SqlCommand(strBackup, Con1)
            SqlCommand.CommandTimeout = 600
            SqlCommand.ExecuteNonQuery()

            'Compressed("'" & Me.txtlocation.Text + "\" + Me.cmbDatabases.Text + strDate.Replace("/", "") + ".Bak" & "'")
            Dim strFile As String = "'" & Me.txtlocation.Text + "\" + Me.txtDatabase.Text + strDate.Replace("/", "") + ".Bak" & "'"

            If mPassword <> "Error" AndAlso mPassword.Trim.Length > 0 Then
                Zip(strFile.Replace("'", ""), mPassword)
            Else
                Zip(strFile.Replace("'", ""))
            End If

            Me.lblPrograssBar1.Text = String.Empty
            Do Until Me.prgContinue1.Value > 99
                Me.prgContinue1.Value = Me.prgContinue1.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            Me.lblPrograssBar1.Text = "Database Backup Successfully"
            msg_Information(str_informBackup)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then Con.Close()
        End Try
    End Sub
    'Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
    '    Try
    '        'Me.txtDatabase.Text = String.Empty
    '        'Dim ds As New DataSet
    '        'ds.ReadXml(str_ApplicationStartUpPath & "\DefaultDatabase.Xml")
    '        'Me.txtDatabase.Text = ds.Tables(0).Rows(0).Item(0).ToString
    '        'Me.prgContinue1.Value = 0

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub DatabaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveDatabaseToolStripMenuItem.Click
        Try
            Dim ds As New DataSet
            If Not IO.File.Exists(str_ApplicationStartUpPath & " \DefaultDatabase.Xml") Then
                ds.Tables(0).Columns.Add("DatabaseName", GetType(System.String))
            Else
                ds.ReadXml(str_ApplicationStartUpPath & " \DefaultDatabase.Xml")
            End If
            dtDatabase.Rows.Clear()
            drDatabase = ds.Tables(0).NewRow
            drDatabase.Item(0) = Me.txtDatabase.Text
            ds.Tables(0).Rows.InsertAt(drDatabase, 0)
            ds.WriteXml(str_ApplicationStartUpPath & "\DefaultDatabase.Xml")
            msg_Information("Database Save Successfully")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub btnAddServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddServer.Click
    '    Try
    '        frmConnectServer.ShowDialog()
    '        If frmConnectServer._Server <> "" Then
    '            Me.txtServer.Text = frmConnectServer._Server
    '            frmConnectServer._Server = String.Empty
    '        End If

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    'Private Sub SaveServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        'dtServer.TableName = "Servers"
    '        dtServer.ReadXml(str_ApplicationStartUpPath & "\DefaultServer.Xml")
    '        dtServer.Rows.Clear()
    '        Dim dr As DataRow
    '        dr = dtServer.NewRow
    '        dr.Item(0) = Me.txtServer.Text
    '        dtServer.Rows.InsertAt(dr, 0)
    '        dtServer.WriteXml(str_ApplicationStartUpPath & "\DefaultServer.Xml")
    '        dtServer.ReadXml(str_ApplicationStartUpPath & "\DefaultServer.Xml")
    '        Me.txtServer.Text = dtServer.Rows(0).Item(0).ToString
    '        msg_Information(str_informSave)

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    'Private Sub txtServer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServer.TextChanged
    '    Try
    '        Dim ds As New DataSet
    '        If IO.File.Exists(str_ApplicationStartUpPath & "\SQLBackConnectionString.Xml") = True Then
    '            ds.ReadXml(str_ApplicationStartUpPath & "\SQLBackConnectionString.Xml")
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                RemoveHandler txtServer.TextChanged, AddressOf txtServer_TextChanged
    '                Me.txtServer.Text = ds.Tables(0).Rows(0).Item("ServerName").ToString
    '                ServerName = ds.Tables(0).Rows(0).Item("ServerName").ToString
    '                UserName = ds.Tables(0).Rows(0).Item("UserName").ToString
    '                Password = Decrypt(ds.Tables(0).Rows(0).Item("Password").ToString)
    '                Try
    '                    Con = New SqlConnection("Server=" & ServerName & ";Database=master;" & IIf(UserName <> "", "UID=" & UserName & ";Password=" & Password & "; Integrated Security=False", "Integrated Security=True") & ";Connection Timeout=120")
    '                    If Con.State = ConnectionState.Closed Then Con.Open()
    '                    FillCombo()
    '                Catch ex As Exception

    '                End Try
    '            End If
    '        Else
    '            If frmConnectServer.ShowDialog = Windows.Forms.DialogResult.OK Then
    '                If Me.txtServer.Text <> "" Then
    '                    Try
    '                        ds.ReadXml(str_ApplicationStartUpPath & "\SQLBackConnectionString.Xml")
    '                        If ds.Tables(0).Rows.Count > 0 Then
    '                            RemoveHandler txtServer.TextChanged, AddressOf txtServer_TextChanged
    '                            Me.txtServer.Text = ds.Tables(0).Rows(0).Item("ServerName").ToString
    '                            ServerName = ds.Tables(0).Rows(0).Item("ServerName").ToString
    '                            UserName = ds.Tables(0).Rows(0).Item("UserName").ToString
    '                            Password = Decrypt(ds.Tables(0).Rows(0).Item("Password").ToString)
    '                            Try
    '                                Con = New SqlConnection("Server=" & ServerName & ";Database=master;" & IIf(UserName <> "", "UID=" & UserName & ";Password=" & Password & "; Integrated Security Info =False", "Integrated Security Info=True") & ";Connection Timeout=120")
    '                                If Con.State = ConnectionState.Closed Then Con.Open()
    '                                FillCombo()
    '                            Catch ex As Exception

    '                            End Try
    '                        End If
    '                    Catch ex As Exception

    '                    End Try
    '                End If

    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub FillCombo()
        Try
            Dim dtDatabases As DataTable = New DataTable
            Dim Str As String = String.Empty
            Str = "Select DISTINCT name as DatabaseName From sysDatabases"
            Dim ObjAdp As SqlDataAdapter = New SqlDataAdapter(Str, Con)
            ObjAdp.Fill(dtDatabases)


            'cmbDatabases.DisplayMember = dtDatabases.Columns(0).ColumnName.ToString
            'cmbDatabases.ValueMember = dtDatabases.Columns(0).ColumnName.ToString
            'Me.cmbDatabases.DataSource = dtDatabases

            Dim ds As New DataSet
            If IO.File.Exists(Application.ExecutablePath & "\DefaultDatabase.Xml") Then
                ds.ReadXml(str_ApplicationStartUpPath & "\DefaultDatabase.Xml")
                'Me.cmbDatabases.Text = ds.Tables(0).Rows(0).Item(0).ToString
            Else
                Dim dr As DataRow
                Dim dt As New DataTable
                dt.TableName = "Databases"
                dr = dt.NewRow
                dt.Columns.Add("Database")
                dr(0) = "SimplePOS"
                dt.Rows.Add(dr)
                ds.Tables.Add(dt)
                ds.WriteXml(str_ApplicationStartUpPath & "\DefaultDatabase.Xml")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lblServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblServer.Click

    End Sub
    'Public Sub Zip(ByVal FilePath As String, Optional Password As String = "")
    '    Try

    '        Dim FileName As String = FilePath.Substring(FilePath.LastIndexOf("\") + 1)
    '        Dim Zip As New ZipFile
    '        If Password.Trim.Length > 0 Then
    '            Zip.Password = Password
    '        End If
    '        Zip.AddFile(FilePath)
    '        Zip.Save(FilePath.Substring(0, FilePath.LastIndexOf("\")) & "\" & FileName.Replace(".Bak", "") & ".zip")
    '    Catch ex As Exception
    '    Finally
    '        File.Delete(FilePath)
    '    End Try
    'End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                'DatabaseBackupPassword
                Dim enc As String = Encrypt(Me.txtPassword.Text.ToString())
                Dim IsPassword = GetConfigValue("DatabaseBackupPassword").ToString
                If IsPassword.ToString.Length > 0 Then
                    msg_Error("Password has already been set. You can change it by providing old one is required")
                Else
                    SaveConfiguration("DatabaseBackupPassword", enc)
                    msg_Information("Password has been created successfully")
                    Me.lblLinkChange.Visible = True
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Public Function SaveConfiguration(ByVal KeyType As String, ByVal KeyValue As String) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Dim Cmd As New SqlCommand
        Cmd.Connection = Con
        Cmd.Transaction = trans
        Try
            Dim strSQL As String = String.Empty
            strSQL = "UPDATE ConfigValuesTable SET Config_Value=N'" & KeyValue & "' WHERE Config_Type=N'" & KeyType & "'"
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strSQL
            Cmd.ExecuteNonQuery()
            trans.Commit()

            SaveActivityLog("Configuration", Me.Text.ToString, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, KeyType, , , , , Me.Name.ToString)
            key = KeyType
            Dim config As ConfigSystem = objConfigValueList.Find(AddressOf GetObj)

            objConfigValueList.Remove(config)
            Dim AddConfig As New ConfigSystem
            AddConfig.Config_Type = KeyType.ToString
            AddConfig.Config_Value = KeyValue.ToString
            If config IsNot Nothing Then
                If config.Comments IsNot Nothing Then
                    AddConfig.Comments = config.Comments
                Else
                    AddConfig.Comments = Nothing
                End If
                AddConfig.IsActive = config.IsActive
            End If

            objConfigValueList.Add(AddConfig)
            key = String.Empty
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetObj(ByVal Config As ConfigSystem) As Boolean
        Try
            If Config.Config_Type = key Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub lblLinkChange_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblLinkChange.LinkClicked
        Try
            If Me.txtOldPassword.Visible = False AndAlso Me.txtNewPassword.Visible = False AndAlso Me.lblOld.Visible = False AndAlso Me.lblNew.Visible = False Then
                Me.txtOldPassword.Visible = True
                Me.txtNewPassword.Visible = True
                Me.lblOld.Visible = True
                Me.lblNew.Visible = True
                Me.btnbrowse.Location = New Point(108, 230)
                Me.btnStart.Location = New Point(340, 230)

            Else
                Me.txtOldPassword.Visible = False
                Me.txtNewPassword.Visible = False
                Me.lblOld.Visible = False
                Me.lblNew.Visible = False
                Me.btnbrowse.Location = New Point(108, 177)
                Me.btnStart.Location = New Point(340, 177)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtNewPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNewPassword.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                'DatabaseBackupPassword
                'Dim enc As String = Me.txtOldPassword.Text.ToString()
                Dim enc As String = Encrypt(Me.txtOldPassword.Text.ToString())
                Dim IsPassword As String = GetConfigValue("DatabaseBackupPassword").ToString
                If IsPassword.ToString.Length = 0 Then
                    msg_Error("You have not set any password before")
                ElseIf Not enc.Equals(IsPassword) Then
                    msg_Error("Old password is not correct")
                Else
                    'Dim NewEncrypt As String = Me.txtNewPassword.Text.ToString()
                    Dim NewEncrypt As String = Encrypt(Me.txtNewPassword.Text.ToString())

                    SaveConfiguration("DatabaseBackupPassword", NewEncrypt)
                    msg_Information("New password has been created successfully")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If Rights.Count = 0 Then
                Me.txtPassword.Visible = False
                Me.txtOldPassword.Visible = False
                Me.txtNewPassword.Visible = False
                Me.lblOld.Visible = False
                Me.lblPassword.Visible = False
                Me.lblNew.Visible = False
                Me.lblLinkChange.Visible = False

                Me.btnbrowse.Location = New Point(108, 151)
                Me.btnStart.Location = New Point(340, 151)

                'Me.txtPassword.Enabled = False
                'Me.txtOldPassword.Enabled = False
                'Me.txtNewPassword.Enabled = False
                'Me.lblOld.Enabled = False
                'Me.lblNew.Enabled = False
                Exit Sub
            End If
            'If Rights Is Nothing Then Exit Sub
            For Each RightstDt As GroupRights In Rights
                If RightstDt.FormControlName = "Password Change" Then

                    Dim Pass As String = Decrypt(getConfigValueByType("DatabaseBackupPassword").ToString())
                    If Pass.Length > 0 Then
                        Me.txtPassword.Text = Pass
                        Me.txtPassword.Visible = True
                        Me.lblPassword.Visible = True
                        Me.txtPassword.Enabled = False
                        Me.lblLinkChange.Visible = True

                        Me.txtOldPassword.Visible = False
                        Me.txtNewPassword.Visible = False
                        Me.lblOld.Visible = False
                        Me.lblNew.Visible = False
                        Me.btnbrowse.Location = New Point(108, 177)
                        Me.btnStart.Location = New Point(340, 177)
                    Else
                        Me.txtPassword.Enabled = True
                        Me.txtPassword.Text = ""
                        Me.txtPassword.Visible = True
                        Me.lblPassword.Visible = True


                        'Me.txtPassword.Visible = True
                        'Me.txtOldPassword.Visible = True
                        'Me.txtNewPassword.Visible = True
                        'Me.lblOld.Visible = True
                        'Me.lblNew.Visible = True
                        Me.lblLinkChange.Visible = False

                        'Me.btnbrowse.Location = New Point(108, 230)
                        'Me.btnStart.Location = New Point(244, 230)
                        'Me.btnReset.Location = New Point(340, 230)

                        'Me.txtPassword.Enabled = True
                        'Me.txtOldPassword.Enabled = True
                        'Me.txtNewPassword.Enabled = True
                        'Me.lblOld.Enabled = True
                        'Me.lblNew.Enabled = True
                    End If
                Else
                    Me.txtPassword.Visible = False
                    Me.txtOldPassword.Visible = False
                    Me.txtNewPassword.Visible = False
                    Me.lblOld.Visible = False
                    Me.lblNew.Visible = False
                    Me.lblPassword.Visible = False
                    Me.lblLinkChange.Visible = False

                    Me.btnbrowse.Location = New Point(108, 151)
                    Me.btnStart.Location = New Point(340, 151)

                    'Me.txtPassword.Enabled = False
                    'Me.txtOldPassword.Enabled = False
                    'Me.txtNewPassword.Enabled = False
                    'Me.lblOld.Enabled = False
                    'Me.lblNew.Enabled = False



                End If
                If RightstDt.FormControlName = "View" Then

                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmdbbackup_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Try
        '    GetSecurityRights()
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub lblRefresh_Click(sender As Object, e As EventArgs) Handles lblRefresh.Click
        Try
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        Try
            'If Asc(e.KeyChar) = Keys.Space Then
            '    MessageBox.Show("Space is not allowed")
            'End If
            If (e.KeyChar) = "" Then
                MessageBox.Show("Space is not allowed")
                e.Handled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
       
    End Sub
    Private Sub ResetControls()
        Dim cb As New SqlConnectionStringBuilder(SQLHelper.CON_STR)
        Me.txtServer.Text = cb.DataSource
        Me.txtDatabase.Text = cb.InitialCatalog
        'Dim Pass As String = Decrypt(getConfigValueByType("DatabaseBackupPassword").ToString())
        'If Pass.Length > 0 Then
        '    Me.txtPassword.Text = Pass
        '    Me.txtPassword.Enabled = False
        'Else
        '    Me.txtPassword.Enabled = True
        '    Me.txtPassword.Text = ""
        'End If

    End Sub

    Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click
        Try
            If Not frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
                frmMain.LoadControl("frmSystemConfiguration")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Utility
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class