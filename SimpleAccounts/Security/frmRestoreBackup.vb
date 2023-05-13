Imports System.Data.SqlClient
Public Class frmRestoreBackup
    Dim Con As SqlConnection
    Dim dtServer As New DataTable
    Dim dtDatabase As New DataTable
    Dim IsOpenForm As Boolean = False
    Dim ServerName As String = String.Empty
    Dim UserName As String = String.Empty
    Dim Password As String = String.Empty

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            Me.OpenFileDialog1.Filter = "Backup File |*.Bak|File|.File|All|*.*"
            If Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtLocation.Text = Me.OpenFileDialog1.FileName
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmRestoreBackup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim str As String = String.Empty
            'dtServer.TableName = "Servers"
            'dtServer.Columns.Add("ServerName", GetType(System.String))
            'dtDatabase.TableName = "Database"
            'dtDatabase.Columns.Add("DatabaseName", GetType(System.String))
            IsOpenForm = True
            txtServer_TextChanged(Nothing, Nothing)
            Dim ds As New DataSet
            If IO.File.Exists(str_ApplicationStartUpPath & "\DefaultServer.Xml") Then
                ds.ReadXml(str_ApplicationStartUpPath & "\DefaultServer.Xml")
                Me.txtServer.Text = ds.Tables(0).Rows(0).Item(0).ToString
            End If
            btnReset_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            'Con.Close()
        End Try
    End Sub
    Private Sub FillCombo(Optional ByVal condition As String = "")
        Try

            Dim dtDatabases As New DataTable
            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim Str As String = String.Empty
            Str = "Select DISTINCT name as DatabaseName From sysDatabases"
            Dim ObjAdp As SqlDataAdapter = New SqlDataAdapter(Str, Con)
            ObjAdp.Fill(dtDatabases)


            cmbDatabases.DisplayMember = dtDatabases.Columns(0).ColumnName.ToString
            cmbDatabases.ValueMember = dtDatabases.Columns(0).ColumnName.ToString
            Me.cmbDatabases.DataSource = dtDatabases

            Dim ds As New DataSet
            If IO.File.Exists(Application.ExecutablePath & "\DefaultDatabase.Xml") Then
                ds.ReadXml(str_ApplicationStartUpPath & "\DefaultDatabase.Xml")
                Me.cmbDatabases.Text = ds.Tables(0).Rows(0).Item(0).ToString
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
    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Try
            Me.lblPrograssBar1.Text = String.Empty
            Me.lblPrograssBar1.Text = "Start Restore Database "
            Do Until Me.ToolStripProgressBar2.Value > 50
                Me.ToolStripProgressBar2.Value = Me.ToolStripProgressBar2.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            Me.lblPrograssBar1.Text = String.Empty
            Me.lblPrograssBar1.Text = "Please Wait..."
            Do Until Me.ToolStripProgressBar2.Value > 50
                Me.ToolStripProgressBar2.Value = Me.ToolStripProgressBar2.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            Dim strRestoreBackup As String = String.Empty
            If Con.State = ConnectionState.Closed Then Con.Open()
            strRestoreBackup = "Restore Database " & Me.cmbDatabases.Text & " From Disk= '" & Me.txtLocation.Text & "'"
            Dim SqlCommand As SqlCommand = New SqlCommand(strRestoreBackup, Con)
            SqlCommand.ExecuteNonQuery()
            Me.lblPrograssBar1.Text = String.Empty
            Do Until Me.ToolStripProgressBar2.Value > 99
                Me.ToolStripProgressBar2.Value = Me.ToolStripProgressBar2.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            Me.lblPrograssBar1.Text = "Successfully Restored Database"
            msg_Information(str_informRestore)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Me.cmbDatabases.Text = String.Empty
            Dim ds As New DataSet
            ds.ReadXml(str_ApplicationStartUpPath & "\DefaultDatabase.Xml")
            Me.cmbDatabases.Text = ds.Tables(0).Rows(0).Item(0).ToString
            Me.txtLocation.Text = String.Empty

            Me.ToolStripProgressBar2.Value = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnAddServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddServer.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If frmConnectServer.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If frmConnectServer._Server <> "" Then
                    Me.txtServer.Text = frmConnectServer._Server
                    frmConnectServer._Server = String.Empty
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    'Private Sub btnServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try

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
    Private Sub txtServer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServer.TextChanged
        Try
            Dim ds As New DataSet
            If IO.File.Exists(str_ApplicationStartUpPath & "\SQLBackConnectionString.Xml") = True Then
                ds.ReadXml(str_ApplicationStartUpPath & "\SQLBackConnectionString.Xml")
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.txtServer.Text = ds.Tables(0).Rows(0).Item("ServerName").ToString
                    ServerName = ds.Tables(0).Rows(0).Item("ServerName").ToString
                    UserName = ds.Tables(0).Rows(0).Item("UserName").ToString
                    Password = Decrypt(ds.Tables(0).Rows(0).Item("Password").ToString)
                    Try
                        Con = New SqlConnection("Server=" & ServerName & ";Database=master;" & IIf(UserName <> "", "UID=" & UserName & ";Password=" & Password & "; Integrated Security=False", "Integrated Security=True") & ";Connection Timeout=120")
                        If Con.State = ConnectionState.Closed Then Con.Open()
                        FillCombo()
                    Catch ex As Exception

                    End Try
                End If
            Else
                If frmConnectServer.ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Me.txtServer.Text <> "" Then
                        ds.ReadXml(str_ApplicationStartUpPath & "\SQLBackConnectionString.Xml")
                        If ds.Tables(0).Rows.Count > 0 Then
                            Me.txtServer.Text = ds.Tables(0).Rows(0).Item("ServerName").ToString
                            ServerName = ds.Tables(0).Rows(0).Item("ServerName").ToString
                            UserName = ds.Tables(0).Rows(0).Item("UserName").ToString
                            Password = Decrypt(ds.Tables(0).Rows(0).Item("Password").ToString)
                            Try
                                Con = New SqlConnection("Server=" & ServerName & ";Database=master;" & IIf(UserName <> "", "UID=" & UserName & ";Password=" & Password & "; Integrated Security Info =False", "Integrated Security Info=True") & ";Connection Timeout=120")
                                If Con.State = ConnectionState.Closed Then Con.Open()
                                FillCombo()
                            Catch ex As Exception

                            End Try
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SaveDatabaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveDatabaseToolStripMenuItem1.Click
        Try
            Dim ds As New DataSet
            If Not IO.File.Exists(str_ApplicationStartUpPath & " \DefaultDatabase.Xml") Then
                ds.Tables(0).Columns.Add("DatabaseName", GetType(System.String))
            Else
                ds.ReadXml(str_ApplicationStartUpPath & " \DefaultDatabase.Xml")
            End If
            dtDatabase.Rows.Clear()
            Dim drDatabase As DataRow
            drDatabase = ds.Tables(0).NewRow
            drDatabase.Item(0) = Me.cmbDatabases.Text
            ds.Tables(0).Rows.InsertAt(drDatabase, 0)
            ds.WriteXml(str_ApplicationStartUpPath & "\DefaultDatabase.Xml")
            msg_Information("Database Save Successfully")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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