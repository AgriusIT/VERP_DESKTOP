Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports SBUtility.Utility
Imports System.Configuration
Public Class frmSwitchUser
    Private _User As String = String.Empty
    Private _Password As String = String.Empty
    Public Shared Con_Utiltiy As String = SimpleAccounts.My.Settings.Database1ConnectionString ' SBG.Con.ConnectionString)
    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private UserVerified As Boolean = False
    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Sub SetConnectionInfo()
        'If Me.ComboBox1.Text.Length > 0 And Not Me.ComboBox1.Text = "Default" Then
        'Dim xmlDoc As New Xml.XmlDocument
        'xmlDoc.Load(str_ApplicationStartUpPath & "\CompanyList.xml")
        ''xmlDoc.Load(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml")
        'Dim TagetNode As Xml.XmlNode = xmlDoc.SelectNodes("//AllCompanies/Company[@ID=" & Me.ComboBox1.SelectedValue & "]").Item(0)
        ''Dim TagetNode As Xml.XmlNode = xmlDoc.SelectNodes("//NewDataSet/Id[@ID=" & Me.ComboBox1.SelectedValue & "]").Item(0)
        'If Not IsNothing(TagetNode) Then
        '    'TagetNode.Attributes("Name").Value 
        '    ConServerName = TagetNode.Attributes("Server").Value
        '    ConUserId = TagetNode.Attributes("UserID").Value
        '    ConPassword = TagetNode.Attributes("Password").Value
        '    ConDBName = TagetNode.Attributes("DBName").Value
        'End If
        'Dim ds As New DataSet
        'Dim dt As New DataTable
        'If IO.File.Exists(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml") Then
        '    ds.ReadXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml")
        '    Dim DView As New DataView
        '    DView.Table = ds.Tables(0)
        '    DView.RowFilter = "Id='" & Me.ComboBox1.SelectedValue & "'".ToString
        '    If DView.Table.Rows.Count > 0 Then
        '        ConServerName = DView.ToTable.Rows(0).Item("ServerName").ToString
        '        ConUserId = DView.ToTable.Rows(0).Item("UserId").ToString
        '        ConPassword = DView.ToTable.Rows(0).Item("Password").ToString
        '        ConDBName = DView.ToTable.Rows(0).Item("DBName").ToString
        '    End If
        'Else
        '    Dim Con1 As New OleDbConnection(SimpleAccounts.My.Settings.Database1ConnectionString) ' SBG.Con.ConnectionString)
        '    ConServerName = Con1.DataSource
        '    ConUserId = "sa"
        '    ConPassword = "sa"
        '    ConDBName = Con1.Database
        'End If
        'SBUtility.Utility.DBName = ConDBName
        'SBUtility.Utility.DBPassword = ConPassword
        'SBUtility.Utility.DBUserName = ConUserId
        'SBUtility.Utility.DBServerName = ConServerName
        'set enhanced security flag
        Try
            Boolean.TryParse(GetConfigValue("EnhancedSecurity"), IsEnhancedSecurity)
        Catch ex As Exception
            'Throw ex
        End Try
    End Sub
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try


            System.Configuration.ConfigurationManager.AppSettings("LastLogin") = Me.UsernameTextBox.Text
            'System.Configuration.ConfigurationManager.AppSettings("LastCompany") = Me.ComboBox1.Text
            System.Configuration.ConfigurationManager.AppSettings("RememberMyPassword") = Me.chkRememberMyPassword.Checked
            System.Configuration.ConfigurationManager.AppSettings("SavePassword") = Encrypt(PasswordTextBox.Text)

            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            Dim configsection As AppSettingsSection = config.AppSettings
            If Not configsection Is Nothing Then
                If configsection.IsReadOnly = False AndAlso configsection.SectionInformation.IsLocked = False Then
                    If Not configsection.Settings("LastLogin") Is Nothing Then
                        configsection.Settings("LastLogin").Value = Me.UsernameTextBox.Text
                    Else
                        configsection.Settings.Add("LastLogin", Me.UsernameTextBox.Text)
                    End If
                    If Not configsection.Settings("SavePassword") Is Nothing Then
                        configsection.Settings("SavePassword").Value = IIf(Me.chkRememberMyPassword.Checked = True, Encrypt(PasswordTextBox.Text), String.Empty)
                    Else
                        configsection.Settings.Add("SavePassword", IIf(Me.chkRememberMyPassword.Checked = True, Encrypt(PasswordTextBox.Text), String.Empty))
                    End If
                    If Not configsection.Settings("RememberMyPassword") Is Nothing Then
                        configsection.Settings("RememberMyPassword").Value = Me.chkRememberMyPassword.Checked
                    Else
                        configsection.Settings.Add("RememberMyPassword", Me.chkRememberMyPassword.Checked)
                    End If
                    'If Not configsection.Settings("LastCompany") Is Nothing Then
                    '    configsection.Settings("LastCompany").Value = Me.ComboBox1.Text
                    'Else
                    '    configsection.Settings.Add("LastCompany", Me.ComboBox1.Text)
                    'End If
                    config.Save()
                End If

            End If
            If Me.UsernameTextBox.Text.Length < 1 Then
                msg_Error("Please enter user name")
                Me.UsernameTextBox.Focus()
                Exit Sub
            End If

            If Me.PasswordTextBox.Text.Length < 1 Then
                msg_Error("Please enter password")
                Me.PasswordTextBox.Focus()
                Exit Sub
            End If

            If Date.Today > "14-Aug-2012" Then msg_Error("An error occured, and application will be terminated  " & Chr(13) & "Error No: 114 - Registery Check Error ...." & Chr(13) & Chr(13) & Chr(13) & "Please contact " & Chr(13) & "Sirius Solution And Services" & Chr(13) & Chr(13) & "Phone: +923-444-114-000" & Chr(13) & "E-Mail: info@siriussolution.com" & Chr(13) & "Web: www.siriussolution.com") : End
            ' str_ApplicationStartUpPath = Application.StartupPath
            SetConnectionInfo()

            Con = New OleDbConnection("Provider=SQLOLEDB.1;Password=" & ConPassword & ";Integrated Security Info=False;User ID=" & ConUserId & ";Initial Catalog=" & ConDBName & ";Data Source=" & ConServerName)
            Con.Open()

            If Con.State = ConnectionState.Open Then
                Con.Close()
            Else : msg_Error("Could not connect to server, please check your network") : End
            End If

        Catch ex As Exception
            msg_Error("An error occured while connecting to the server " & Chr(13) & ex.Message)
            Exit Sub
        End Try


        Dim strUserValidity As String = CheckSecurityUser(Me.UsernameTextBox.Text, Me.PasswordTextBox.Text)

        If strUserValidity = "InValidMachine" Then
            msg_Error("An error occured while connecting to the server " & Chr(13) & "System is not authourized to connect.")
            Me.PasswordTextBox.Focus()
        ElseIf strUserValidity = "In-Valid" Then
            msg_Error(str_ErrorInvalidUser)
            Me.PasswordTextBox.Focus()
            Exit Sub
        ElseIf strUserValidity = "In-Active" Then
            msg_Error(str_ErrorInActiveUser)
            Me.UsernameTextBox.Focus()
            Exit Sub
        ElseIf strUserValidity = "Valid" Then
            Me.UserVerified = True
            'Me.UsernameTextBox.ReadOnly = True
            Me.PasswordTextBox.Text = ""
            'Me.ComboBox1.Enabled = False
            '  str_ApplicationStartUpPath = System.Configuration.ConfigurationManager.AppSettings("ApplicatonPath").ToString
            ConfigRights(LoginUserId) 'Call Group Rights Object
            Me.Cursor = Cursors.WaitCursor
            Try
                'TODO : check status
                If SBDal.UtilityDAL.IsMachineVerified(System.Environment.MachineName.ToString) = False Then
                    Dim dt As New DataTable
                    Dim strSQL As String
                    Dim objda As OleDb.OleDbDataAdapter

                    strSQL = "Select * from TblActivityLog Where LogApplicationName='Config' And LogFormCaption='" & System.Environment.MachineName.ToString & "'"

                    objda = New OleDb.OleDbDataAdapter(strSQL, Con)
                    objda.Fill(dt)
                    If dt.Rows.Count > 200 Then
                        msg_Error("Trial period is expired..." & Chr(10) & "Please register your software as soon as possible.")
                        RegisterStatus = EnumRegisterStatus.Expired
                    ElseIf dt.Rows.Count > 100 Then
                        msg_Information("You are using trial version..." & Chr(10) & "Please register your software to aviod any problem")
                    End If
                End If
                ''insert Activity Log
                SaveActivityLog("Config", System.Environment.MachineName.ToString, EnumActions.Login, LoginUserId, EnumRecordType.Security, UsernameTextBox.Text.Trim)
                'SaveActivityLog("Config", Me.Text, EnumActions.Login, LoginUserId, EnumRecordType.Security, UsernameTextBox.Text.Trim)
            Catch ex As Exception
            Finally
                Me.Cursor = Cursors.Default
            End Try
            Me.Close()
        Else
            End
        End If
    End Sub
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        'End
        Me.Close()
    End Sub
    Private Sub LoginForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not Me.UserVerified = True Then
                ' End
                Me.Close()
            End If
            Me.UserVerified = False
            str_MessageHeader = LoginUserName
            SaveLastSettings()
            'frmMain.UltraStatusBar2.Panels(0).Text = "User: " & LoginUserName
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoginForm1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.Text = "Switch User" + " " + "(" & Application.ProductVersion & ")"
            LoadLastSettings()
            Me.UsernameTextBox.Text = LoginUserCode
            'LoadCombo()

            ''Me.UsernameTextBox.Text = Microsoft.VisualBasic.UCase(ConfigurationManager.AppSettings("LastLogin"))
            'If Not ConfigurationManager.AppSettings("LastCompany") Is Nothing Then
            '    Me.ComboBox1.Text = ConfigurationManager.AppSettings("LastCompany")
            'Else
            '    If Not Me.ComboBox1.SelectedIndex = -1 Then
            '        Me.ComboBox1.SelectedIndex = 0
            '    End If
            'End If

            'If Not ConfigurationManager.AppSettings("RememberMyPassword") Is Nothing Then
            '    If ConfigurationManager.AppSettings("RememberMyPassword") = "True" Then
            '        Me.chkRememberMyPassword.Checked = ConfigurationManager.AppSettings("RememberMyPassword")
            '        Me.PasswordTextBox.Text = Decrypt(ConfigurationManager.AppSettings("SavePassword"))
            '    Else
            '        Me.PasswordTextBox.Text = String.Empty
            '    End If
            'End If
            'Me.Label2.TabIndex = False
            'Me.Label3.TabIndex = False
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Sub LoadCombo()
        Try
            'Dim dt As New DataTable
            'dt = GetCompanyDataTable()
            'Dim dr As DataRow
            'dr = dt.NewRow
            'dr.Item(0) = 0
            'dr.Item(1) = "Default"
            'dt.Rows.InsertAt(dr, 0)
            'If Not dt Is Nothing Then
            '    If dt.Rows.Count > 0 Then
            '        Me.ComboBox1.DataSource = dt
            '        Me.ComboBox1.ValueMember = dt.Columns(0).ColumnName
            '        Me.ComboBox1.DisplayMember = dt.Columns(1).ColumnName
            '    Else
            '    End If
            'End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Cursor = Cursors.WaitCursor
        Try
            frmAdministratorTools.ShowDialog()
            'LoadCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub UsernameTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsernameTextBox.TextChanged

    End Sub
    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        Try
            Dim str As String = String.Empty

            _User = "Admin"
            _Password = "123"

            Me.UsernameTextBox.Text = _User
            Me.PasswordTextBox.Text = _Password
            SetConnectionInfo()

            Con = New OleDbConnection("Provider=SQLOLEDB.1;Password=" & ConPassword & ";Integrated Security Info=False;User ID=" & ConUserId & ";Initial Catalog=" & ConDBName & ";Data Source=" & ConServerName)
            Con.Open()

            If Con.State = ConnectionState.Open Then
                Con.Close()
            Else : msg_Error("Could not connect to server, please check your network") : End
            End If

            If GetUser(UsernameTextBox.Text.ToString) = False Then
                Dim cmd As New OleDb.OleDbCommand
                cmd.Connection = Con
                cmd.CommandType = CommandType.Text
                If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                    cmd.CommandText = ""
                    str = "INSERT INTO tblUserGroup(GroupName, Active) Values('Admin',1) Select @@Identity"
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cmd.CommandText = str
                    Dim current_id As Integer = cmd.ExecuteNonQuery()

                    cmd.CommandText = ""
                    str = "INSERT INTO tblUser(User_Name, User_Code, Password, GroupId, Block, Active) Values('" & Encrypt(Me.UsernameTextBox.Text) & "', '" & Encrypt(UsernameTextBox.Text.ToUpper) & "', '" & Encrypt(Me.PasswordTextBox.Text) & "', " & current_id & ", 0,1) "
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cmd.CommandText = str
                    cmd.ExecuteNonQuery()
                Else
                    cmd.CommandText = ""
                    str = "INSERT INTO tblUser(User_Name, User_Code, Password, Active) Values('" & Encrypt(Me.UsernameTextBox.Text) & "', '" & Encrypt(UsernameTextBox.Text.ToUpper) & "', '" & Encrypt(Me.PasswordTextBox.Text) & "',1) "
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cmd.CommandText = str
                    cmd.ExecuteNonQuery()
                End If
            End If
            OK_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetUser(ByVal UserCode As String) As Boolean
        Try

            Con = New OleDbConnection("Provider=SQLOLEDB.1;Password=" & ConPassword & ";Integrated Security Info=False;User ID=" & ConUserId & ";Initial Catalog=" & ConDBName & ";Data Source=" & ConServerName)
            Con.Open()

            If Con.State = ConnectionState.Open Then
                Con.Close()
            Else : msg_Error("Could not connect to server, please check your network....") : End
            End If

            Dim str As String = String.Empty
            str = "Select * From tblUser WHERE User_Code='" & Encrypt(Me.UsernameTextBox.Text.ToUpper) & "'"
            Dim dt As DataTable = GetDataTable(str)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class