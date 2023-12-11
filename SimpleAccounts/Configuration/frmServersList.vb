Imports System.Data.SqlClient
Imports System.Data.Sql.SqlDataSourceEnumerator
Public Class frmServersList
    Public _Server As String
    Public _UserId As String
    Public _Password As String
    Dim Con As New SqlClient.SqlConnection



    Private Sub frmServersList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dt As New DataTable

        'dt = SmoApplication.EnumAvailableSqlServers()
        Dim str As String = String.Empty
        Dim dtServer As New DataTable
        dtServer.TableName = "srvConnectionString"
        dtServer.Columns.Add("Server", GetType(System.String))
        dtServer.Columns.Add("UserId", GetType(System.String))
        dtServer.Columns.Add("Password", GetType(System.String))
        dtServer.Columns.Add("Database", GetType(System.String))
        If Not IO.File.Exists(str_ApplicationStartUpPath & "\srvConnection.Xml") Then
            Dim drServer As DataRow
            drServer = dtServer.NewRow
            drServer.Item(0) = "Rai"
            drServer.Item(1) = "sa"
            drServer.Item(2) = "sa"
            drServer.Item(3) = "Master"
            dtServer.Rows.Add(drServer)
            dtServer.WriteXml(str_ApplicationStartUpPath & "\srvConnection.Xml")
        End If
        dtServer.ReadXml(str_ApplicationStartUpPath & "\srvConnection.Xml")
        'Con = New SqlClient.SqlConnection("Server=" & dtServer.Rows(0).Item(0).ToString & ";UId=" & dtServer.Rows(0).Item(1).ToString & ";Pwd=" & dtServer.Rows(0).Item(2).ToString & ";Database=" & dtServer.Rows(0).Item(3).ToString & ";")
        'str = "Select srvName as [Name] From sysservers"
        'Dim adp As New SqlClient.SqlDataAdapter(str, Con)
        'adp.Fill(dt)
        'Me.GridEX1.DataSource = dt

        Dim SQLSRV As Sql.SqlDataSourceEnumerator
        SQLSRV = Sql.SqlDataSourceEnumerator.Instance
        Dim dtServers As DataTable = SQLSRV.GetDataSources

        dtServers.Columns.Add("ServerName\InstanceName", GetType(System.String))
        dtServers.Columns("ServerName\InstanceName").Expression = "IIF(InstanceName <> '', ServerName +'\'+ InstanceName, ServerName)"
        dtServers.AcceptChanges()
        Me.GridEX1.DataSource = dtServers

    End Sub
    Private Sub GridEX1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick
        Try
            If Me.GridEX1.RowCount = 0 Then Exit Sub
            _Server = Me.GridEX1.GetRow.Cells("ServerName\InstanceName").Text.ToString
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub frmServersList_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    '    If Keys.Escape Then Me.Close()
    'End Sub

    Private Sub frmServersList_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class