Imports System.Data.Sql

Public Class frmConnectServer
    Dim servers As SqlDataSourceEnumerator
    Public Shared _Server As String = String.Empty
    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged, RadioButton1.CheckedChanged
        Try
            If Me.RadioButton2.Checked = True Then
                Me.TextBox1.Enabled = True
                Me.TextBox2.Enabled = True
                Me.Label2.Enabled = True
                Me.Label3.Enabled = True
            ElseIf Me.RadioButton1.Checked = True Then
                Me.TextBox1.Enabled = False
                Me.TextBox2.Enabled = False
                Me.Label2.Enabled = False
                Me.Label3.Enabled = False
            Else
                Me.TextBox1.Enabled = True
                Me.TextBox2.Enabled = True
                Me.Label2.Enabled = True
                Me.Label3.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmConnectServer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Me.TextBox1.Text = String.Empty
            Me.TextBox2.Text = String.Empty
            servers = SqlDataSourceEnumerator.Instance
            Dim dt As DataTable = servers.GetDataSources
            dt.Columns.Add("ServerName\InstanceName", GetType(System.String))
            dt.Columns("ServerName\InstanceName").Expression = "IIF(InstanceName <> '', ServerName + '\' + InstanceName, ServerName)"
            dt.AcceptChanges()
            Me.cmbServerName.DisplayMember = "ServerName\InstanceName"
            Me.cmbServerName.ValueMember = "ServerName\InstanceName"
            Me.cmbServerName.DataSource = dt
            Me.cmbServerName.SelectedIndex = 0
            ApplyStyleSheet(Me)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbServerName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbServerName.SelectedIndexChanged
        Try
            If Not Me.cmbServerName.SelectedIndex = -1 Then
                _Server = Me.cmbServerName.Text
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim ds As New DataSet
            Dim dt As New DataTable
            Dim dr As DataRow
            dt.Columns.Add("ServerName", GetType(System.String))
            dt.Columns.Add("UserName", GetType(System.String))
            dt.Columns.Add("Password", GetType(System.String))
            dr = dt.NewRow
            dr(0) = Me.cmbServerName.Text
            dr(1) = Me.TextBox1.Text.ToString
            dr(2) = Encrypt(Me.TextBox2.Text.ToString)
            dt.Rows.InsertAt(dr, 0)
            ds.Tables.Add(dt)
            ds.WriteXml(str_ApplicationStartUpPath & "\SQLBackConnectionString.Xml")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim cn As SqlClient.SqlConnection = New SqlClient.SqlConnection("Server=" & Me.cmbServerName.Text & ";Database=master;" & IIf(Me.RadioButton2.Checked = True, "UId=" & Me.TextBox1.Text & ";PWD=" & Me.TextBox2.Text & ";Integrated Security=False", "Integrated Security=True") & "")
            If cn.State = ConnectionState.Closed Then cn.Open() : msg_Information("Test Connection Succeded") Else ShowErrorMessage("Login faild") : Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class