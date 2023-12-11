Imports System
Imports System.Data
Imports Microsoft.SqlServer.Server.SqlTriggerContext
Public Class frmServerSetup
    Private Sub rbtWindowsAthentication_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtWindowsAthentication.CheckedChanged, rbtSQLServer.CheckedChanged
        Try
            If Me.rbtWindowsAthentication.Checked = True Then
                Me.GroupBox1.Visible = False
            ElseIf Me.rbtSQLServer.Checked = True Then
                Me.GroupBox1.Visible = True
            Else
                Me.GroupBox1.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            _SvrName = Me.txtServerName.Text
            _DB_Name = Me.txtDatabase.Text
            _DB_Password = Me.txtPassword.Text
            _User_Id = Me.txtUserId.Text
            _flgRestoreData = IIf(Me.rbtBlank.Checked = True, False, True)
            GetConnectionString()
            GetConnectionStringMaster()
            frmFinishSetup.TopLevel = False
            frmFinishSetup.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            frmFinishSetup.Dock = DockStyle.Fill
            frmSetup.Panel2.Controls.Add(frmFinishSetup)
            frmFinishSetup.Show()
            frmFinishSetup.BringToFront()
            frmSetup.UltraTabControl1.SelectedTab = frmSetup.UltraTabControl1.Tabs(2).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmServerSetup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.txtServerName.Text = System.Environment.MachineName.ToString & "\SQLExpress"
            Me.txtDatabase.Text = "SimplePOS1"
            Me.txtPassword.Text = String.Empty
            Me.txtUserId.Text = String.Empty
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            frmSetup.UltraTabControl1.SelectedTab = frmSetup.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnTestConnection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestConnection.Click
        Try
            _SvrName = Me.txtServerName.Text
            _DB_Name = Me.txtDatabase.Text
            _DB_Password = Me.txtPassword.Text
            _User_Id = Me.txtUserId.Text
            GetConnectionString()
            GetConnectionStringMaster()
            Dim con As New SqlClient.SqlConnection(get_ConnectionStringMaster.ConnectionString)
            If con.State = ConnectionState.Closed Then con.Open()
            If con.State = ConnectionState.Open Then
                MsgBox("Test Connection Succeeded", MsgBoxStyle.Information, "Test Connection")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtBlank.CheckedChanged, rbtSample.CheckedChanged
        Try
            If Me.rbtBlank.Checked = True Then
                _flgRestoreData = False
            Else
                _flgRestoreData = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class