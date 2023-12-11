Imports System
Imports System.Configuration
Imports System.Net
Imports System.Data
Imports System.Data.SqlClient
Public Class frmRequestViews
    Private _IsLogin As Boolean = False
    Private _Email As String = String.Empty
    Private _Password As String = String.Empty
    Private _CustomerId As Integer = 0
    Private _Lbl As New Label
    Private _IsValidateLogin As Boolean = False
    Private _LoadedData As Boolean = False
    Dim dt As New DataTable
    Dim dtLogin As New DataTable
    Dim ErrorMsg As String = String.Empty
    Dim IsError As Boolean = False
    Dim dtTypeData As DataTable
    Dim dtPriorityData As DataTable
    Dim dtStatusData As DataTable
    Private _Title As String
    Dim dv As DataView
    Dim dtConfigEmail As New DataTable

    Public Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
        End Set
    End Property

    Private _OpenedDate As DateTime
    Public Property OpenedDate() As DateTime
        Get
            Return _OpenedDate
        End Get
        Set(ByVal value As DateTime)
            _OpenedDate = value
        End Set
    End Property

    Private _Customer As Integer
    Public Property Customer() As Integer
        Get
            Return _Customer
        End Get
        Set(ByVal value As Integer)
            _Customer = value
        End Set
    End Property

    Private _Status As Integer
    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal value As Integer)
            _Status = value
        End Set
    End Property

    Private _Type As Integer
    Public Property Type() As Integer
        Get
            Return _Type
        End Get
        Set(ByVal value As Integer)
            _Type = value
        End Set
    End Property

    Private _Priority As Integer
    Public Property Priority() As Integer
        Get
            Return _Priority
        End Get
        Set(ByVal value As Integer)
            _Priority = value
        End Set
    End Property

    Private _Description As String
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property

    Private _Client_System_Id As String
    Public Property Client_System_Id() As String
        Get
            Return _Client_System_Id
        End Get
        Set(ByVal value As String)
            _Client_System_Id = value
        End Set
    End Property

    Private _Client_System_Name As String
    Public Property Client_System_Name() As String
        Get
            Return _Client_System_Name
        End Get
        Set(ByVal value As String)
            _Client_System_Name = value
        End Set
    End Property

    Private _EmailId As String
    Public Property EmailId() As String
        Get
            Return _EmailId
        End Get
        Set(ByVal value As String)
            _EmailId = value
        End Set
    End Property

    Public Function GetConnectionString() As String
        Try
            Dim Con_Str As New SqlClient.SqlConnectionStringBuilder("Password=sa2012;Integrated Security=False;User ID=SIRIUS_sa;Initial Catalog=112_softb;Data Source=SIRIUS.net")
            Return Con_Str.ConnectionString
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCon() As SqlClient.SqlConnection
        Try
            Dim cn As New SqlClient.SqlConnection(GetConnectionString())
            If cn.State = ConnectionState.Closed Then cn.Open()
            Return cn
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmRequestViews_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If BackgroundWorker3.IsBusy Then Exit Sub
            BackgroundWorker3.RunWorkerAsync()
            Do While BackgroundWorker3.IsBusy
                Application.DoEvents()
            Loop


            Me.cmbTypes.DataSource = dtTypeData
            Me.cmbTypes.ValueMember = "TypeId"
            Me.cmbTypes.DisplayMember = "TypeName"

            Me.cmbPriority.DataSource = dtPriorityData
            Me.cmbPriority.ValueMember = "PriorityId"
            Me.cmbPriority.DisplayMember = "PriorityName"

            Me.cmbStatus.DataSource = dtStatusData
            Me.cmbStatus.ValueMember = "StatusId"
            Me.cmbStatus.DisplayMember = "StatusName"

            _LoadedData = True
            Dim DefaultEmailId As Integer
            If Not IsDBNull(getConfigValueByType("DefaultEmailId")) Then
                DefaultEmailId = Convert.ToInt32(getConfigValueByType("DefaultEmailId"))
            Else
                DefaultEmailId = 0
            End If
            dtConfigEmail = GetDataTable("Select * From TblDefEmail WHERE EmailId=" & DefaultEmailId)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try


            'If LoginVerify(Me.txtUserName.Text.ToString, Me.txtPassword.Text.ToString) = True Then
            _Lbl.Visible = True
            _Lbl.AutoSize = False
            _Lbl.Dock = DockStyle.Fill
            _Lbl.Text = "Log on to server please wait..."
            Application.DoEvents()
            'Me.Controls.Add(_Lbl)
            Me.UltraTabControl1.Tabs(0).TabPage.Controls.Add(_Lbl)
            _Lbl.BringToFront()

            _Email = Me.txtUserName.Text
            _Password = Me.txtPassword.Text

            If BackgroundWorker2.IsBusy Then Exit Sub
            BackgroundWorker2.RunWorkerAsync()
            Do While BackgroundWorker2.IsBusy
                Application.DoEvents()
            Loop

            If IsError = True Then
                Me.lblErrorMsg.Text = ErrorMsg
            Else
                Me.lblErrorMsg.Text = String.Empty
            End If

            If _IsLogin = True Then
                _Lbl.Text = String.Empty
                _Lbl.AutoSize = False
                _Lbl.Dock = DockStyle.Fill
                _Lbl.Text = "Loading, Please Wait..."
                Me.UltraTabControl1.Tabs(0).TabPage.Controls.Add(_Lbl)
                _Lbl.BringToFront()

                If Me.BackgroundWorker1.IsBusy Then Exit Sub
                BackgroundWorker1.RunWorkerAsync()
                Do While BackgroundWorker1.IsBusy
                    Application.DoEvents()
                Loop
                GetAll()

                Me.GroupBox1.Visible = False
            Else
                Me.GroupBox1.Visible = True
                _IsLogin = False
            End If
        Catch ex As Exception
        Finally
            _Lbl.Visible = False
        End Try
    End Sub
    Public Function LoginVerify(ByVal Email As String, ByVal Password As String) As Boolean
        Try
            Dim da As New SqlClient.SqlDataAdapter("SP_GetByEmailAndPasswordtblCustomers '" & Email & "', '" & Password & "'", GetConnectionString)
            da.Fill(dtLogin)
            If dtLogin IsNot Nothing Then
                If dtLogin.Rows.Count > 0 Then
                    _Email = dtLogin.Rows(0).Item("EMail").ToString
                    _CustomerId = dtLogin.Rows(0).Item("CustomerId")
                    _IsLogin = True
                    _IsValidateLogin = True
                Else
                    _IsLogin = False
                    _IsValidateLogin = False
                    IsError = True
                    ErrorMsg = "Email Or Password Is Invalid!"
                End If
            Else
                _IsValidateLogin = False
                _IsLogin = False
                IsError = IsError
                ErrorMsg = "Email Or Password Is Invalid!"
            End If
            Return _IsLogin
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getData() As DataView
        Try
            Dim da As New SqlClient.SqlDataAdapter("SP_GetAlltblRequests", GetConnectionString)
            da.Fill(dt)
            dv = New DataView
            dt.TableName = "Requests"
            dv.Table = dt
            dv.RowFilter = "Customer='" & _CustomerId & "'"
            Return dv

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub GetAll()

        Try

            'RemoveHandler grdMsgDetail.SelectionChanged, AddressOf grdMsgDetail_SelectionChanged
            Me.grdMsgDetail.DataSource = dv
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdMsgDetail.RootTable.Columns
                col.Visible = False
            Next

            Me.grdMsgDetail.RootTable.Columns("RequestId").Visible = True
            Me.grdMsgDetail.RootTable.Columns("Title").Visible = True
            Me.grdMsgDetail.RootTable.Columns("OpenedDate").Visible = True
            Me.grdMsgDetail.RootTable.Columns("StatusName").Visible = True
            Me.grdMsgDetail.RootTable.Columns("ResolvedDate").Visible = True
            Me.grdMsgDetail.AutoSizeColumns()


        Catch ex As Exception
            _LoadedData = False
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            _Lbl.Text = String.Empty
            _Lbl.Visible = True
            _Lbl.AutoSize = False
            _Lbl.Dock = DockStyle.Fill
            _Lbl.Text = "Loading, Please Wait..."
            Me.UltraTabControl1.Tabs(0).TabPage.Controls.Add(_Lbl)
            _Lbl.BringToFront()
            Me.grdMsgDetail.DataSource = Nothing
            dv.Table.Clear()
            If _IsLogin = True Then
                If BackgroundWorker1.IsBusy Then Exit Sub
                BackgroundWorker1.RunWorkerAsync()
                Do While BackgroundWorker1.IsBusy
                    Application.DoEvents()
                Loop
            Else
                Me.GroupBox1.Visible = True
            End If

            GetAll()
            Me.lblDatetime.Text = "Loading Last Time.  " & Date.Now

        Catch ex As Exception
        Finally
            _Lbl.Visible = False
        End Try
    End Sub
    Private Sub grdMsgDetail_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdMsgDetail.SelectionChanged
        Try
            If Me.grdMsgDetail.RowCount = 0 Then Exit Sub
            If _LoadedData = True Then
                If _IsLogin = True Then Me.lblHeadDescription.Text = Me.grdMsgDetail.GetRow.Cells("Description").Text.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage("Error occured while selection changed: " & ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            getData()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmRequestViews_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            _Lbl.Visible = True
            _Lbl.AutoSize = False
            _Lbl.Dock = DockStyle.Fill
            _Lbl.Text = "Loading, Please Wait..."
            Me.UltraTabControl1.Tabs(0).TabPage.Controls.Add(_Lbl)
            _Lbl.BringToFront()

            Dim str() As String = getConfigValueByType("SupportSystemConfig").Split(";")
            If str.Length > 1 Then
                Me.GroupBox1.Visible = False
                Me.txtUserName.Text = str(0)
                Me.txtPassword.Text = Decrypt(str(1))

                _Email = Me.txtUserName.Text
                _Password = Me.txtPassword.Text

                _Lbl.Visible = True
                _Lbl.AutoSize = False
                _Lbl.Dock = DockStyle.Fill
                _Lbl.Text = "Log on to server please wait..."
                Me.UltraTabControl1.Tabs(0).TabPage.Controls.Add(_Lbl)
                _Lbl.BringToFront()


                If BackgroundWorker2.IsBusy Then Exit Sub
                BackgroundWorker2.RunWorkerAsync()
                Do While BackgroundWorker2.IsBusy
                    Application.DoEvents()
                Loop

                If _IsLogin = True Then
                    _Lbl.Text = String.Empty
                    _Lbl.Text = "Loading, Please Wait..."
                    Me.UltraTabControl1.Tabs(0).TabPage.Controls.Add(_Lbl)
                    _Lbl.BringToFront()

                    If BackgroundWorker1.IsBusy Then Exit Sub
                    BackgroundWorker1.RunWorkerAsync()
                    Do While BackgroundWorker1.IsBusy
                        Application.DoEvents()
                    Loop
                    GetAll()
                    _LoadedData = True
                    _Lbl.Visible = False
                    Me.GroupBox1.Visible = False
                    Me.lblDatetime.Text = "Loading, Last Time.  " & Date.Now
                Else
                    _IsLogin = False
                End If
                Me.lblErrorMsg.Text = String.Empty
            Else
                Me.GroupBox1.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            _Lbl.Visible = False
        End Try
    End Sub
    Private Sub grdMsgDetail_LoadingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdMsgDetail.LoadingRow
        Try

            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdMsgDetail.GetRows
                Dim RowStyle1 As New Janus.Windows.GridEX.GridEXFormatStyle
                If row.Cells("StatusName").Text = "Close" Then
                    RowStyle1.BackColor = Color.White
                    row.RowStyle = RowStyle1
                ElseIf row.Cells("StatusName").Text = "In Progress" Then
                    Dim RowStyle2 As New Janus.Windows.GridEX.GridEXFormatStyle
                    RowStyle2.BackColor = Color.LightCoral
                    row.RowStyle = RowStyle2
                ElseIf row.Cells("StatusName").Text = "Open" Then
                    Dim RowStyle3 As New Janus.Windows.GridEX.GridEXFormatStyle
                    RowStyle3.BackColor = Color.LightYellow
                    row.RowStyle = RowStyle3
                ElseIf row.Cells("StatusName").Text = "Wait for release" Then
                    Dim RowStyle4 As New Janus.Windows.GridEX.GridEXFormatStyle
                    RowStyle4.BackColor = Color.LightBlue
                    row.RowStyle = RowStyle4
                Else
                    Dim RowStyle5 As New Janus.Windows.GridEX.GridEXFormatStyle
                    RowStyle5.BackColor = Color.White
                    row.RowStyle = RowStyle5
                End If
            Next

        Catch ex As Exception
        End Try
    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            If Not e.Cancelled = True Then

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try

            If LoginVerify(_Email, _Password) = True Then
                _IsLogin = True
                Dim strLogin As String = getConfigValueByType("SupportSystemConfig").ToString
                If strLogin = String.Empty Then
                    Dim str As String = "Update ConfigValuesTable Set config_Value='" & _Email & ";" & Encrypt(_Password) & "' WHERE Config_Type='SupportSystemConfig'"
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    Dim cmd As New OleDb.OleDbCommand(str, Con)
                    cmd.ExecuteNonQuery()
                    Con.Close()
                End If
            End If


        Catch ex As Exception
            ShowErrorMessage("Error occurred while updating record: " & ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker3_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Try
            FillCombos("RequestType")
            FillCombos("RequestPriority")
            FillCombos("RequestStatus")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            Dim str As String = String.Empty
            If Condition = "RequestType" Then
                str = "sp_SelecttblDefTypesAll"
                dtTypeData = Get_DataTable(str)
            ElseIf Condition = "RequestPriority" Then
                str = "Select * From tblDefPriority"
                dtPriorityData = Get_DataTable(str)
            ElseIf Condition = "RequestStatus" Then
                str = "Select * From tblDefStatus"
                dtStatusData = Get_DataTable(str)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function AddRequest() As Boolean
        If GetCon.State = ConnectionState.Closed Then GetCon.Open()
        Dim trans As SqlClient.SqlTransaction = GetCon.BeginTransaction
        Try
            Dim AdminEmail As String = getConfigValueByType("AdminEmailId").ToString

            Dim str As String = String.Empty
            str = "INSERT INTO tblRequests(Title, OpenedDate, Customer, Status, Type, Priority, Description) Values('" & Title & "', '" & OpenedDate & "',  " & Customer & ", " & Status & ", " & Type & ", " & Priority & ", '" & Description & "')"
            ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()


            '-------------------------- Send Email -----------------------
            Dim MailAdd As New Mail.MailMessage
            MailAdd.From = New Mail.MailAddress(dtConfigEmail.Rows(0).Item("Email").ToString)
            Dim strEmailTo As String = "info@siriussolution.com"
            MailAdd.To.Add(strEmailTo)
            If Not IsDBNull(AdminEmail) Then
                MailAdd.CC.Add(AdminEmail)
            End If
            MailAdd.Subject = Title
            MailAdd.Body = Description & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & vbCrLf & "" & CompanyTitle & ""

            Try
                Dim client As New Mail.SmtpClient(dtConfigEmail.Rows(0).Item("SmtpServer").ToString)
                client.Port = dtConfigEmail.Rows(0).Item("Port")
                client.EnableSsl = dtConfigEmail.Rows(0).Item("SSL")
                client.Credentials = New Net.NetworkCredential(dtConfigEmail.Rows(0).Item("Email").ToString, Decrypt(dtConfigEmail.Rows(0).Item("EmailPassword").ToString))
                client.DeliveryMethod = Mail.SmtpDeliveryMethod.Network
                client.Send(MailAdd)
                MailAdd.Dispose()
            Catch ex As Exception

            End Try

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            GetCon.Close()
        End Try
    End Function
    Private Sub FillModel()
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Function CreateParameter(ByVal name As String, ByVal dbType As Data.SqlDbType, ByVal obj As Object)
        Try
            Dim cmdParameter As New SqlClient.SqlParameter(name, dbType, obj)
            Return cmdParameter
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Get_DataTable(ByVal str As String, Optional ByVal trans As SqlClient.SqlTransaction = Nothing) As DataTable
        If GetCon.State = ConnectionState.Closed Then GetCon.Open()

        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Try


            If trans IsNot Nothing Then
                cmd.Connection = trans.Connection
                cmd.Transaction = trans
            Else
                cmd.Connection = GetCon()
            End If

            cmd.CommandType = CommandType.Text
            cmd.CommandText = str

            da.SelectCommand = cmd
            da.Fill(dt)

            If dt IsNot Nothing Then
                Return dt
            Else
                Return Nothing
            End If

            cmd = Nothing
            da = Nothing
            GetCon.Close()

        Catch ex As Exception
            Throw ex
        Finally
            cmd = Nothing
            da = Nothing
            GetCon.Close()
        End Try
    End Function
    Public Function ExecuteNonQuery(ByVal trans As SqlClient.SqlTransaction, ByVal CommandType As CommandType, ByVal str As String, Optional ByVal Parameters() As SqlClient.SqlParameter = Nothing, Optional ByVal ReturnVal As Boolean = False) As Integer
        If GetCon.State = ConnectionState.Closed Then GetCon.Open()
        Try
            Dim cmd As New SqlClient.SqlCommand
            If trans IsNot Nothing Then
                cmd.Connection = trans.Connection
                cmd.Transaction = trans
            Else
                cmd.Connection = GetCon()
            End If
            cmd.CommandType = CommandType
            cmd.CommandText = str

            If Parameters IsNot Nothing Then
                For Each Param As SqlClient.SqlParameter In Parameters
                    cmd.Parameters.Add(Param)
                Next
            End If

            Dim obj As Object = cmd.ExecuteNonQuery
            cmd = Nothing

            If ReturnVal = True Then
                Return obj
            Else
                Return Nothing
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

    Private Sub BackgroundWorker4_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker4.DoWork
        Try
            AddRequest()
        Catch ex As Exception
            ShowErrorMessage("Error occurred while saving record: " & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try


            Dim FromEmail() As String = getConfigValueByType("SupportSystemConfig").ToString.Split(";")

            Dim strMac As String = String.Empty
            Title = Me.txtRequestTitle.Text
            OpenedDate = Date.Now
            Customer = _CustomerId
            Type = Me.cmbTypes.SelectedValue
            Priority = Me.cmbPriority.SelectedValue
            Status = Me.cmbStatus.SelectedValue
            Description = Me.txtRequestDescription.Text
            EmailId = FromEmail(0).ToString
            If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
            If Me.txtRequestTitle.Text = String.Empty Then
                ShowErrorMessage("Please enter request title")
                Me.txtRequestTitle.Focus()
                Exit Sub
            End If

            If BackgroundWorker4.IsBusy Then Exit Sub
            BackgroundWorker4.RunWorkerAsync()
            Do While BackgroundWorker4.IsBusy
                Application.DoEvents()
            Loop
            msg_Information(str_informSave)
            ClearData()

        Catch ex As Exception
            ShowErrorMessage("Error occurred while saving record: " & ex.Message)
        Finally
            Con.Close()
        End Try
    End Sub
    Private Sub ClearData()
        Try

            Me.txtRequestTitle.Text = String.Empty
            Me.txtRequestDescription.Text = String.Empty
            Me.cmbTypes.SelectedIndex = 0
            Me.cmbPriority.SelectedIndex = 0
            Me.cmbStatus.SelectedIndex = 0

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class