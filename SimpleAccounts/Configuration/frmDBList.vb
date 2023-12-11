Public Class frmDBList
    Public _DBName As String
    Private _ServerName As String
    Private _UserName As String
    Private _Password As String
    Dim Con As New SqlClient.SqlConnection
    Private Sub frmDBList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim str As String = String.Empty
            'Dim dtServer As New DataTable
            'dtServer.TableName = "srvConnectionString"
            'dtServer.Columns.Add("Server", GetType(System.String))
            'dtServer.Columns.Add("UserId", GetType(System.String))
            'dtServer.Columns.Add("Password", GetType(System.String))
            'dtServer.Columns.Add("Database", GetType(System.String))
            'If Not IO.File.Exists(str_ApplicationStartUpPath & "\srvConnection.Xml") Then
            '    Dim drServer As DataRow
            '    drServer = dtServer.NewRow
            '    drServer.Item(0) = frmServersList._Server.ToString
            '    drServer.Item(1) = frmServersList._UserId.ToString
            '    drServer.Item(2) = frmServersList._Password.ToString
            '    drServer.Item(3) = "Master"
            '    dtServer.Rows.Add(drServer)
            '    dtServer.WriteXml(str_ApplicationStartUpPath & "\srvConnection.Xml")
            'End If
            'dtServer.ReadXml(str_ApplicationStartUpPath & "\srvConnection.Xml")
            If CompanyAndConnectionInfo.RadioButton1.Checked = True Then
                Con = New SqlClient.SqlConnection("Server=" & CompanyAndConnectionInfo._Server_Name & ";UId=" & CompanyAndConnectionInfo._DB_UserName.ToString & ";Pwd=" & CompanyAndConnectionInfo._DB_Password.ToString & ";Database=master;")
            Else
                Con = New SqlClient.SqlConnection("Server=" & CompanyAndConnectionInfo._Server_Name & ";Database=master;Integrated Security Info=True")
            End If
            str = "Select [Name] From sysDatabases"
            Dim dt As New DataTable
            Dim adp As New SqlClient.SqlDataAdapter(str, Con)
            adp.Fill(dt)
            Me.grdDB.DataSource = dt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdDB_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdDB.DoubleClick
        Try
            If Me.grdDB.RowCount = 0 Then Exit Sub
            _DBName = Me.grdDB.GetRow.Cells(0).Text
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmDBList_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        'Try
        '    If Asc(e.KeyChar) = Keys.Cancel Then Me.Close()
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
    Private Sub frmDBList_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class