Imports System.Data.SqlClient
Imports SBDal
Imports system.Data.Sql
Imports Microsoft.Win32
Imports System.Collections.Generic
Public Class frmcreatenewdatabase
    Dim Con As SqlConnection
    Dim Server_Version As String
    Dim dt As DataTable
    Dim Servers As SqlDataSourceEnumerator

    Private Sub btnCreateDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateDatabase.Click
        Try
            Me.prgContinue.Value = 0
            If Me.txttitle.Text = String.Empty Then
                ShowErrorMessage("Please enter the company title")
                Me.txttitle.Focus()
                Exit Sub
            End If
            If cmbServers.Text = String.Empty Then
                ShowErrorMessage("Please select server")
                Me.cmbServers.Focus()
                Exit Sub
            End If
            If Me.txtUserName.Text <> "" Then
                If Me.txtUserName.Text = String.Empty Then
                    ShowErrorMessage("Please enter database user name")
                    Me.txtUserName.Focus()
                    Exit Sub
                End If
                If Me.txtPassword.Text = String.Empty Then
                    ShowErrorMessage("Please enter database password")
                    Me.txtPassword.Focus()
                    Exit Sub
                End If
            End If
            Me.lblPrograss.Text = "Create Company"
            Do Until Me.prgContinue.Value > 50
                Me.prgContinue.Value = prgContinue.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim sqlCommand As SqlCommand = New SqlCommand("Use Master", Con)
            sqlCommand.ExecuteNonQuery()

            Dim sqlcmd As New SqlClient.SqlCommand("Create Database " & Me.txtDatabase.Text & " On Primary  " _
                                                  & " (Name=N'" & Me.txtDatabase.Text & "', FileName=N'" & str_ApplicationStartUpPath & "\DB\" & Me.txtDatabase.Text + "_Data.Mdf" & "') " _
                                                  & "  Log On " _
                                                  & " (Name=N'" & Me.txtDatabase.Text + "_Log" & "', FileName=N'" & str_ApplicationStartUpPath & "\DB\" & Me.txtDatabase.Text + "_Log.ldf" & "')", Con)
            sqlcmd.ExecuteNonQuery()
            Me.lblPrograss.Text = String.Empty
            Me.lblPrograss.Text = "Creating Connection "
            Do Until Me.prgContinue.Value > 75
                Me.prgContinue.Value = prgContinue.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            Dim ObjDataset As New DataSet
            Dim dtCompConnection As New DataTable
            dtCompConnection.TableName = "Connections"
            dtCompConnection.Columns.Add("Id", GetType(System.String))
            dtCompConnection.Columns.Add("Title", GetType(System.String))
            dtCompConnection.Columns.Add("ServerName", GetType(System.String))
            dtCompConnection.Columns.Add("UserId", GetType(System.String))
            dtCompConnection.Columns.Add("Password", GetType(System.String))
            dtCompConnection.Columns.Add("DBName", GetType(System.String))

            Dim drCompConnection As DataRow
            If Not IO.File.Exists(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml") Then
                drCompConnection = dtCompConnection.NewRow
                drCompConnection.Item(0) = 1
                drCompConnection.Item(1) = "Default"
                drCompConnection.Item(2) = "Rai"
                drCompConnection.Item(3) = "sa"
                drCompConnection.Item(4) = "sa"
                drCompConnection.Item(5) = "SimplePOS"
                dtCompConnection.Rows.InsertAt(drCompConnection, 0)
                ObjDataset.Tables.Add(dtCompConnection)
                ObjDataset.WriteXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml")
            Else
                dtCompConnection.ReadXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml")
                drCompConnection = dtCompConnection.NewRow
                Dim dr() As DataRow = dtCompConnection.Select("Id=Max(Id)", "")
                Dim rCont As Integer = dr(0).ItemArray(0)
                drCompConnection.Item(0) = rCont + 1
                drCompConnection.Item(1) = Me.txttitle.Text.ToString
                drCompConnection.Item(2) = Me.cmbServers.Text.ToString
                drCompConnection.Item(3) = Me.txtUserName.Text.ToString
                drCompConnection.Item(4) = Me.txtPassword.Text.ToString
                drCompConnection.Item(5) = Me.txtDatabase.Text.ToString
                dtCompConnection.Rows.InsertAt(drCompConnection, 0)
                ObjDataset.Tables.Add(dtCompConnection)
                ObjDataset.WriteXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml")
            End If
            Me.lblPrograss.Text = String.Empty
            Me.lblPrograss.Text = "Company has been created"
            Do Until Me.prgContinue.Value > 75
                Me.prgContinue.Value = prgContinue.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            Me.lblPrograss.Text = String.Empty
            Me.lblPrograss.Text = "Please wait..."
            Do Until Me.prgContinue.Value > 90
                Me.prgContinue.Value = prgContinue.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            RestoreBackup()
            lblPrograss.Text = "Process compeleted"
            Do Until Me.prgContinue.Value > 99
                Me.prgContinue.Value = prgContinue.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            msg_Information("Created company successfully")
            Me.Close()
            Con.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
        End Try
    End Sub
    Private Sub RestoreBackup()
        Try

            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim SqlCmd As SqlCommand = New SqlCommand("Use master", Con)
            SqlCmd.ExecuteNonQuery()

            If Me.rdoblankdb.Checked = True Then

                Dim strRestoreBackup As String = "Restore Database " & Me.txtDatabase.Text & " From Disk='" & str_ApplicationStartUpPath & "\RestoreBackup\BlankDB.Bak' WITH REPLACE,MOVE 'SimplePOS_Data' TO '" & str_ApplicationStartUpPath & "\DB\" & Me.txtDatabase.Text + "_Data.mdf', Move 'SimplePOS_Log' TO '" & str_ApplicationStartUpPath & "\DB\" & Me.txtDatabase.Text + "_Log.ldf' "
                Dim SqlCommand As SqlCommand = New SqlCommand(strRestoreBackup, Con)
                SqlCommand.ExecuteNonQuery()

                Dim sqlcomm As New SqlCommand("sp_configure 'allow updates', 0 RECONFIGURE WITH OVERRIDE", Con)
                sqlcomm.ExecuteNonQuery()

                Dim Con_New As SqlConnection
                If txtUserName.Text <> "" Then
                    Con_New = New SqlConnection("Server=" & Me.cmbServers.Text & ";Database=" & Me.txtDatabase.Text & ";UID=" & Me.txtUserName.Text & ";PWD=" & Me.txtPassword.Text & "")
                Else
                    Con_New = New SqlConnection("Server=" & Me.cmbServers.Text & ";Database=" & Me.txtDatabase.Text & ";Integrated Security=SSPI")
                End If
                Con_New.Open()
                Dim sqlCommd As New SqlCommand("Insert Into tblSystemList(SystemCode, SystemName) Values('" & Me.txttitle.Text & "', '" & System.Environment.MachineName.ToString & "')", Con_New)
                sqlCommd.ExecuteNonQuery()
            Else

                Dim strRestoreBackup As String = "Restore Database " & Me.txtDatabase.Text & " From Disk='" & str_ApplicationStartUpPath & "\RestoreBackup\Sample.Bak' WITH REPLACE,MOVE 'SimplePOS_Data' TO '" & str_ApplicationStartUpPath & "\DB\" & Me.txtDatabase.Text + "_Data.mdf', Move 'SimplePOS_Log' TO '" & str_ApplicationStartUpPath & "\DB\" & Me.txtDatabase.Text + "_Log.ldf' "
                Dim SqlCommand As SqlCommand = New SqlCommand(strRestoreBackup, Con)
                SqlCommand.ExecuteNonQuery()

                Dim sqlcomm As New SqlCommand("sp_configure 'allow updates', 0 RECONFIGURE WITH OVERRIDE", Con)
                sqlcomm.ExecuteNonQuery()

                Dim Con_New As SqlConnection
                If txtUserName.Text <> "" Then
                    Con_New = New SqlConnection("Server=" & Me.cmbServers.Text & ";Database=" & Me.txtDatabase.Text & ";UID=" & Me.txtUserName.Text & ";PWD=" & Me.txtPassword.Text & "")
                Else
                    Con_New = New SqlConnection("Server=" & Me.cmbServers.Text & ";Database=" & Me.txtDatabase.Text & ";Integrated Security=True")
                End If
                Con_New.Open()
                Dim sqlCommd As SqlCommand = New SqlCommand("Insert Into tblSystemList(SystemCode, SystemName) Values('" & Me.txttitle.Text & "', '" & System.Environment.MachineName.ToString & "')", Con_New)
                sqlCommd.ExecuteNonQuery()

            End If
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Me.txttitle.Text = String.Empty
            Me.cmbServers.SelectedIndex = 0
            Me.txtUserName.Text = String.Empty
            Me.txtPassword.Text = String.Empty
            Me.txtDatabase.Text = String.Empty
            Me.prgContinue.Value = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmcreatenewdatabase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            FillCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub txttitle_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txttitle.KeyPress
        Try
            If Not (Char.IsLetter(e.KeyChar) Or Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or Microsoft.VisualBasic.Asc(e.KeyChar) = Keys.Space) Then
                e.Handled = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txttitle_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txttitle.KeyUp
        Try

            Me.txtDatabase.Text = Me.txttitle.Text
            Dim str As String = Me.txtDatabase.Text
            str = str.Replace(" ", "")
            Me.txtDatabase.Text = str

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillCombo()
        Me.Cursor = Cursors.WaitCursor
        Try
            Servers = SqlDataSourceEnumerator.Instance
            Dim dt As DataTable = Servers.GetDataSources
            dt.Columns.Add("ServerName\InstanceName", GetType(System.String))
            dt.Columns("ServerName\InstanceName").Expression = "IIF(InstanceName <> '', ServerName +'\'+ InstanceName, ServerName)"
            dt.AcceptChanges()
            Me.cmbServers.DisplayMember = "ServerName\InstanceName" ' & "\" & dt.Columns(1).ToString
            Me.cmbServers.ValueMember = "ServerName\InstanceName"
            Me.cmbServers.DataSource = dt
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Con.State = ConnectionState.Closed Then Con.Open()
            If Con.State = ConnectionState.Open Then
                msg_Information("Test connection succeeded")
            Else
                ShowErrorMessage("Database connection error")
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtUserName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserName.TextChanged, txtPassword.TextChanged, cmbServers.TextChanged, txtDatabase.TextChanged
        Try

            If Me.cmbServers.Text = String.Empty Then Exit Sub
            'If Me.txtUserName.Text = String.Empty Then Exit Sub
            'If Me.txtPassword.Text = String.Empty Then Exit Sub
            If Me.RadioButton1.Checked = True Then
                Me.txtConnectionString.Text = "Data Source=" & Me.cmbServers.Text & ";UId=" & Me.txtUserName.Text & ";Pwd=" & Me.txtPassword.Text & ";Database=master"
            Else
                Me.txtUserName.Text = String.Empty
                Me.txtPassword.Text = String.Empty
                Me.txtConnectionString.Text = "Data Source=" & Me.cmbServers.Text & ";Database=master;Integrated Security=True"
            End If
            Try
                Con = New SqlConnection(Me.txtConnectionString.Text.ToString)
            Catch ex As Exception

            End Try


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbServers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbServers.SelectedIndexChanged
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Try
            If Me.RadioButton1.Checked = True Then
                Me.txtUserName.ReadOnly = False
                Me.txtPassword.ReadOnly = False
            ElseIf Me.RadioButton2.Checked = True Then
                Me.txtUserName.ReadOnly = True
                Me.txtPassword.ReadOnly = True
            Else
                Me.txtUserName.ReadOnly = False
                Me.txtPassword.ReadOnly = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtConnectionString_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConnectionString.TextChanged

    End Sub

    Private Sub rdosampledb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdosampledb.CheckedChanged

    End Sub
End Class