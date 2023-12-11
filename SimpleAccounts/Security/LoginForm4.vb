''28-Dec-2013 R:M6 Imran Ali        Release 2.1.0.0 Bug
'' TASK TFS1175 Muhammad Ameen on 04-10-2017 : Hide and show logos and icons on login screen and home screen on configuration file based.
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports SBUtility.Utility
Imports System.Configuration
Imports System.Net.NetworkInformation
Imports System.IO
Imports System.IO.Compression
Imports Ionic.Zip
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Public Class LoginForm4
    Private _User As String = String.Empty
    Private _Password As String = String.Empty
    Private _UserName As String = String.Empty
    Private _UserPassword As String = String.Empty
    Private _RememberMe As Boolean = False
    Private _CompanyTitle As String = String.Empty
    Dim blnStatus As Boolean = False
    Dim strServerName() As String = {}
    Public blnSwitchUser As Boolean = False
    Dim IsOpenForm As Boolean = False
    Public Shared Con_Utiltiy As String = SimpleAccounts.My.Settings.Database1ConnectionString ' SBG.Con.ConnectionString)
    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.
    Private UserVerified As Boolean = False
    Private SoftbeatsPartnerName As String = String.Empty
    Private _DtImage As New DataTable
    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub SetConnectionInfo()
        
        Me.ComboBox1_SelectedIndexChanged(Nothing, Nothing)
        If Not Me.ComboBox1.SelectedIndex = -1 Then

            Dim ReportColExist As Boolean = CType(ComboBox1.DataSource, DataTable).Columns.Contains("ReportPath")

            Dim objRow As DataRowView = CType(Me.ComboBox1.SelectedItem, DataRowView)
            If objRow IsNot Nothing Then
                ConCompany = objRow.Item("Title").ToString
                ConServerName = objRow.Item("ServerName").ToString
                ConUserId = objRow.Item("UserId").ToString
                If Not (Decrypt(objRow.Item("Password").ToString).Contains("Base-64") Or Decrypt(objRow.Item("Password").ToString).Contains("Bad")) Then
                    ConPassword = Decrypt(objRow.Item("Password").ToString)
                Else
                    ConPassword = objRow.Item("Password").ToString
                End If
                ConDBName = objRow.Item("DBName").ToString
                LoginUserRememberMe = Me.chkRememberMyPassword.Checked

                If ReportColExist = True AndAlso objRow.Item("ReportPath").ToString.Trim.Length > 0 Then
                    ReportPath = objRow.Item("ReportPath").ToString
                Else
                    ReportPath = Application.StartupPath & "\Reports\"
                End If

            Else
                Dim Con1 As New OleDbConnection(SimpleAccounts.My.Settings.Database1ConnectionString) ' SBG.Con.ConnectionString)
                ConCompany = "Softbeats"
                ConServerName = Con1.DataSource
                ConUserId = "sa"
                ConPassword = "sa"
                ConDBName = Con1.Database
                LoginUserRememberMe = Me.chkRememberMyPassword.Checked
                ReportPath = Application.StartupPath & "\Reports\"
            End If
        Else
            Dim Con1 As New OleDbConnection(SimpleAccounts.My.Settings.Database1ConnectionString) ' SBG.Con.ConnectionString)
            ConCompany = "Softbeats"
            ConServerName = Con1.DataSource
            ConUserId = "sa"
            ConPassword = "sa"
            ConDBName = Con1.Database
            LoginUserRememberMe = Me.chkRememberMyPassword.Checked
            ReportPath = Application.StartupPath & "\Reports\"
        End If
        SBUtility.Utility.DBName = ConDBName
        SBUtility.Utility.DBPassword = ConPassword
        SBUtility.Utility.DBUserName = ConUserId
        SBUtility.Utility.DBServerName = ConServerName

        Try
            Boolean.TryParse(GetConfigValue("EnhancedSecurity"), IsEnhancedSecurity)

        Catch ex As Exception
            'Throw ex
        End Try
    End Sub
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
     
        Try

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

       
            SetConnectionInfo()
         

            Con.Open()

            If Con.State = ConnectionState.Open Then
                Con.Close()
            Else : msg_Error("Could not connect to server, please check your network") : End
            End If

        Catch ex As Exception
            msg_Error("An error occured while connecting to the server  " & Chr(13) & ex.Message)
            Exit Sub
        End Try

        Dim strUserValidity As String = CheckSecurityUser(Me.UsernameTextBox.Text, Me.PasswordTextBox.Text)
        str_ApplicationStartUpPath = Application.StartupPath
        _UserName = Me.UsernameTextBox.Text
        _UserPassword = Me.PasswordTextBox.Text
        _CompanyTitle = Me.ComboBox1.Text
        _RememberMe = Me.chkRememberMyPassword.Checked

        If strUserValidity = "InValidMachine" Then
            msg_Error("An error occured while connecting the server....  " & Chr(13) & "System is not authourized to connect.")
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
            Me.UsernameTextBox.ReadOnly = True
            Me.PasswordTextBox.Text = ""
            Me.ComboBox1.Enabled = False






            Dim str As String = "Switch User" + " " + "( " + Application.ProductVersion + " )"
            If Me.Text = str Then
                frmMain.UltraStatusBar2.Panels(0).Text = "User: " & LoginUserName
            End If
            Me.Cursor = Cursors.WaitCursor
            Try
           
                SaveActivityLog("Config", System.Environment.MachineName.ToString, EnumActions.Login, LoginUserId, EnumRecordType.Security, UsernameTextBox.Text.Trim, True)
                
                'End If
            Catch ex As Exception
            Finally
                Me.Cursor = Cursors.Default
            End Try
            Me.Close()
        Else
            End
        End If
    End Sub
    
    Private Sub LoginForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try

            If Not Me.UserVerified = True Then
                End
            End If
            Me.UserVerified = False
            str_MessageHeader = LoginUserName
            SaveLastSettings()
            ''TASK TFS1175
            'LogosDBKeyExists()
            ''END TASK TFS1175
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoginForm4_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LogosConfigurationFileExists()
            If Not ConfigurationManager.AppSettings("PartnerName") Is Nothing Then
                If ConfigurationManager.AppSettings("PartnerName") <> "" Then
                    SoftbeatsPartnerName = Decrypt(ConfigurationManager.AppSettings("PartnerName"))
                Else
                    SoftbeatsPartnerName = Me.Text
                End If
            End If
            If SoftbeatsPartnerName.Length > 0 Then
                Me.Text = SoftbeatsPartnerName
            Else
                Me.Text = Me.Text
            End If
            Me.Text = Me.Text + " " + "(" & Application.ProductVersion & ")"
            _DtImage = LoadImage()
            _DtImage.CaseSensitive = False
            LoadLastSettings()
            LoadCombo()
            'IsOpenForm = True
            Try


                Me.ComboBox1.Text = ConCompany
                Me.UsernameTextBox.Text = LoginUserCode

                If LoginUserRememberMe = True Then
                    Me.PasswordTextBox.Text = LoginUserPassword
                End If
                Me.chkRememberMyPassword.Checked = LoginUserRememberMe

            Catch ex As Exception

            End Try

            Try
                If Me.UsernameTextBox.Text.Length > 0 Then
                    If _DtImage IsNot Nothing Then
                        Dim dr() As DataRow = _DtImage.Select("User_Name='" & UsernameTextBox.Text.ToUpper & "'")
                        If dr.Length > 0 Then
                            Me.PictureBox1.Image = FillImage(dr(0).Item("UserImage"))
                            Me.PictureBox3.Image = FillImage(dr(0).Item("UserImage"))
                        End If
                    End If
                End If
            Catch ex As Exception
            End Try
            If blnSwitchUser = False Then
                If Me.ComboBox1.Text.Length > 0 AndAlso Me.UsernameTextBox.Text.Length > 0 AndAlso Me.PasswordTextBox.Text.Length > 0 Then
                    Me.OK_Click(Nothing, Nothing)
                End If
            End If


            Panel5.Visible = False
            'LoadCombo()
            'Me.UsernameTextBox.Text = Microsoft.VisualBasic.UCase(ConfigurationManager.AppSettings("LastLogin"))
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
            'Me.Label3.TabIndex = False\
            If LoginUserCode IsNot Nothing Then
                If LoginUserCode.ToString.Length > 0 Then
                    Button1_Click(Me, Nothing)
                End If
            Else
                Me.Panel1.Hide()
                Me.Panel2.Show()
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub
    Sub LoadCombo()
        Try
            Dim dt As New DataTable
            dt = GetCompanyDataTable()
            'Dim dr As DataRow
            'dr = dt.NewRow
            'dr.Item(0) = 0
            'dr.Item(1) = "Default"
            'dt.Rows.InsertAt(dr, 0)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    Me.ComboBox1.ValueMember = dt.Columns(0).ColumnName
                    Me.ComboBox1.DisplayMember = dt.Columns(1).ColumnName
                    Me.ComboBox1.DataSource = dt
                End If
            End If

            '-------------------------------------------------------------------------------------

            '            Dim count As Integer
            '            For Each dr As DataRow In dt.Rows

            '                Dim pic As New PictureBox
            '                Dim rand As New Random
            '                Dim c As Color = Color.FromArgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, _
            '256))
            '                pic.Height = 24
            '                pic.Width = 24
            '                Dim strname = Microsoft.VisualBasic.Mid(dr.Item("title").ToString, 1, 1)
            '                pic.Image = ConvertTextToImage(strname, c)
            '                pic.Name = count + 1
            '                pic.Tag = dr.Item("id").ToString

            '                AddHandler pic.Click, AddressOf companylogo_Click
            '                FlowLayoutPanel1.Controls.Add(CType(pic, PictureBox))

            '            Next

            Me.ListView1.Items.Clear()
            For Each row As DataRow In dt.Rows
                Dim listItem As New ListViewItem
                listItem.Name = row.Item(1).ToString
                listItem.Text = row.Item(1).ToString
                listItem.Tag = row.Item(0).ToString
                listItem.ImageKey = 1
                ListView1.Items.Add(listItem)
            Next




        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function GetUser(ByVal UserCode As String) As Boolean
        Try
            If ConUserId <> "" Then
                Con = New OleDbConnection("Provider=SQLOLEDB.1;Password=" & ConPassword & ";Persist Security Info=True;User ID=" & ConUserId & ";Initial Catalog=" & ConDBName & ";Data Source=" & ConServerName & ";Connect TimeOut=120")
            Else
                Con = New OleDbConnection("Provider=SQLOLEDB.1;Initial Catalog=" & ConDBName & ";Data Source=" & ConServerName & ";Integrated Security=SSPI;Connect TimeOut=120")
            End If
            Con.Open()

            If Con.State = ConnectionState.Open Then
                Con.Close()
            Else : msg_Error("Could not connect to server, please check your network") : End
            End If

            Dim str As String = String.Empty
            str = "Select * From tblUser WHERE User_Code='" & Encrypt(Me.UsernameTextBox.Text.ToUpper) & "'"
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()


            If Not dt Is Nothing Then
                System.Configuration.ConfigurationManager.AppSettings("UserID") = dt.Rows(0).Item(0)
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
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Me.Cursor = Cursors.WaitCursor
        Try
            frmAdministratorTools.ShowDialog()
            LoadCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Dim str As String = String.Empty


            _User = "Admin"
            _Password = "123"

            Me.UsernameTextBox.Text = _User
            Me.PasswordTextBox.Text = _Password
            SetConnectionInfo()
            If ConUserId <> "" Then
                Con = New OleDbConnection("Provider=SQLOLEDB.1;Password=" & ConPassword & ";Persist Security Info=True;User ID=" & ConUserId & ";Initial Catalog=" & ConDBName & ";Data Source=" & ConServerName & ";Connect TimeOut=120")
            Else
                Con = New OleDbConnection("Provider=SQLOLEDB.1;Initial Catalog=" & ConDBName & ";Data Source=" & ConServerName & ";Integrated Security=SSPI;Connect TimeOut=120")
            End If
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
                    str = "INSERT INTO tblUser(User_Name, User_Code, Password, Active) Values('" & Encrypt(Me.UsernameTextBox.Text) & "', '" & Encrypt(UsernameTextBox.Text.ToUpper) & "', '" & Encrypt(Me.PasswordTextBox.Text) & "',1)  Select @@Identity"
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cmd.CommandText = str
                    Dim user_Id As Integer = cmd.ExecuteNonQuery()

                    cmd.CommandText = ""
                    str = "INSERT INTO tblUserRights(User_Id, Form_Id, View_Rights, Save_Rights, Update_Rights) Select " & user_Id & ", Form_Id,1,1,1 from tblForm"
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
    Private Sub LoginForm4_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Panel2.Visible Then
                    Button1_Click(Nothing, Nothing)
                Else
                    OK_Click(Nothing, Nothing)
                End If
            ElseIf e.KeyCode = Keys.Escape Then
                End
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub UnZip(ByVal _CurrentFile As String, ByVal strReadFilePath As String, ByVal strTargetUpdateFolder As String)
        'Try
        '    Dim UnZipFile As ZipFile = ZipFile.Read(strReadFilePath & "\" & _CurrentFile)
        '    For Each e As ZipEntry In UnZipFile
        '        e.Extract(strTargetUpdateFolder, ExtractExistingFileAction.OverwriteSilently)
        '        Application.DoEvents()
        '    Next
        '    UnZipFile.Dispose()
        'Catch ex As Exception

        'End Try
    End Sub
    Public Sub UserRememberMe()
        Try
            System.Configuration.ConfigurationManager.AppSettings("LastLogin") = _UserName 'Me.UsernameTextBox.Text
            System.Configuration.ConfigurationManager.AppSettings("LastCompany") = _CompanyTitle 'Me.ComboBox1.Text
            System.Configuration.ConfigurationManager.AppSettings("RememberMyPassword") = _RememberMe 'Me.chkRememberMyPassword.Checked
            System.Configuration.ConfigurationManager.AppSettings("SavePassword") = Encrypt(_UserPassword) 'Encrypt(PasswordTextBox.Text)
            System.Configuration.ConfigurationManager.AppSettings("SavedPassword") = _UserPassword
            Dim config As System.Configuration.Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            Dim configsection As AppSettingsSection = config.AppSettings
            If Not configsection Is Nothing Then
                If configsection.IsReadOnly = False AndAlso configsection.SectionInformation.IsLocked = False Then
                    If Not configsection.Settings("LastLogin") Is Nothing Then
                        configsection.Settings("LastLogin").Value = _UserName 'Me.UsernameTextBox.Text
                        TextBox1.Text = _UserName
                    Else
                        configsection.Settings.Add("LastLogin", _UserName) 'Me.UsernameTextBox.Text)
                    End If
                    If Not configsection.Settings("SavePassword") Is Nothing Then
                        configsection.Settings("SavePassword").Value = IIf(_RememberMe = True, Encrypt(_UserPassword), String.Empty)
                    Else
                        configsection.Settings.Add("SavePassword", IIf(Me.chkRememberMyPassword.Checked = True, Encrypt(_UserPassword), String.Empty))
                    End If
                    If Not configsection.Settings("RememberMyPassword") Is Nothing Then
                        configsection.Settings("RememberMyPassword").Value = _RememberMe
                    Else
                        configsection.Settings.Add("RememberMyPassword", _RememberMe)
                    End If
                    If Not configsection.Settings("LastCompany") Is Nothing Then
                        configsection.Settings("LastCompany").Value = _CompanyTitle

                    Else
                        configsection.Settings.Add("LastCompany", _CompanyTitle)
                    End If
                    config.Save()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub getReleaseFromServer()
        Try
            If Microsoft.VisualBasic.Replace(Application.ProductVersion, ".", "") < getConfigValueByType("Version").ToString.Replace(".", "") Then
                If System.Environment.MachineName.ToString <> Con.DataSource.Substring(0, Con.DataSource.LastIndexOf("\") + 1).Replace("\", "") Then
                    Try
                        If IO.Directory.Exists("\\" & Con.DataSource.Substring(0, Con.DataSource.LastIndexOf("\") + 1).Replace("\", "") & "\Update_After") Then
                            If IO.File.Exists("\\" & Con.DataSource.Substring(0, Con.DataSource.LastIndexOf("\") + 1).Replace("\", "") & "\Update_After\Release_File.txt") Then
                                Dim strFileName As String = IO.File.ReadAllText("\\" & Con.DataSource.Substring(0, Con.DataSource.LastIndexOf("\") + 1).Replace("\", "") & "\Update_After\Release_File.txt")
                                If IO.File.Exists("\\" & Con.DataSource.Substring(0, Con.DataSource.LastIndexOf("\") + 1).Replace("\", "") & "\Update_After\" & strFileName) Then
                                    IO.File.Delete(Application.StartupPath & "\Downloads\" & strFileName)
                                    IO.File.Copy("\\" & Con.DataSource.Substring(0, Con.DataSource.LastIndexOf("\") + 1).Replace("\", "") & "\Update_After\" & strFileName, Application.StartupPath & "\Downloads\" & strFileName)
                                    UnZip(strFileName, "\\" & Con.DataSource.Substring(0, Con.DataSource.LastIndexOf("\") + 1).Replace("\", "") & "\Update_After", Application.StartupPath)
                                    msg_Information("Software is updated you need to login again.")
                                    End
                                End If
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                Else
                    'ShowErrorMessage("Application not compatible with latest version")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetConfigurations()
        Try
            'str_ApplicationStartUpPath = System.Configuration.ConfigurationManager.AppSettings("ApplicatonPath").ToString
            'If getConfigValueByType("NewSecurityRights").ToString = "True" Then
            'ConfigRights(LoginUserId) 'Call Group Rights Object
            'End If
            If LoginGroup = "Administrator" Then
                GroupType = EnumGroupType.Administrator.ToString
            End If
            '    Catch ex As Exception
            '        'throw ex 
            '    End Try
            'End If
            'Before against request no. R:M6
            'If getConfigValueByType("SoftbeatsPartner").ToString = "True" Then
            '    Dim Config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            '    Dim ConfigSection As AppSettingsSection = Config.AppSettings
            '    If Not ConfigSection Is Nothing Then
            '        If ConfigSection.IsReadOnly = False AndAlso ConfigSection.SectionInformation.IsLocked = False Then
            '            If Not ConfigSection.Settings("PartnerName") Is Nothing Then
            '                ConfigSection.Settings("PartnerName").Value = Encrypt(GetConfigValue("SoftbeatsPartnerName").ToString)
            '            Else
            '                ConfigSection.Settings.Add("PartnerName", Encrypt(GetConfigValue("SoftbeatsPartnerName").ToString))
            '            End If
            '        End If
            '        Config.Save()
            '    End If
            'R:M6 configurate change 
            If getConfigValueByType("SoftbeatsPartner").ToString = "True" Then
                Dim Config As System.Configuration.Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                Dim ConfigSection As AppSettingsSection = Config.AppSettings
                If Not ConfigSection Is Nothing Then
                    If ConfigSection.IsReadOnly = False AndAlso ConfigSection.SectionInformation.IsLocked = False Then
                        If Not ConfigSection.Settings("PartnerName") Is Nothing Then
                            ConfigSection.Settings("PartnerName").Value = Encrypt(getConfigValueByType("SoftbeatsPartnerName").ToString)
                        Else
                            ConfigSection.Settings.Add("PartnerName", Encrypt(getConfigValueByType("SoftbeatsPartnerName").ToString))
                        End If
                    End If
                    Config.Save()
                End If
                'End R:M6
            Else
                Dim Config As System.Configuration.Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                Dim ConfigSection As AppSettingsSection = Config.AppSettings
                If Not ConfigSection Is Nothing Then
                    If ConfigSection.IsReadOnly = False AndAlso ConfigSection.SectionInformation.IsLocked = False Then
                        If Not ConfigSection.Settings("PartnerName") Is Nothing Then
                            ConfigSection.Settings("PartnerName").Value = String.Empty
                        Else
                            ConfigSection.Settings.Add("PartnerName", String.Empty)
                        End If
                    End If
                    Config.Save()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            'If IsOpenForm = False Then Exit Sub
            If Me.ComboBox1.SelectedIndex = -1 Then Exit Sub
            strServerName = CType(Me.ComboBox1.SelectedItem, DataRowView).Row("ServerName").ToString.Split("\")
            Me.TextBox1.Text = CType(Me.ComboBox1.SelectedItem, DataRowView).Row.Item("Title").ToString
            'If strServerName.Length > 0 Then
            '    Dim blnStatus As Boolean = My.Computer.Network.Ping(strServerName(0).ToString(), 100)
            '    If blnStatus = True Then
            '        Me.pbxServer.Image = My.Resources.start_server
            '    End If
            'End If
            'If BackgroundWorker2.IsBusy Then Exit Sub
            'BackgroundWorker2.RunWorkerAsync()
            'Do While BackgroundWorker2.IsBusy
            '    Application.DoEvents()
            'Loop
            Me.Text = Me.TextBox1.Text & " [" & Application.ProductVersion & "]"

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            'If strServerName.Length > 0 Then
            blnStatus = My.Computer.Network.Ping(strServerName(0).ToString(), 100)
            'End If
        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
            blnStatus = False
        End Try
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        Try
            If blnStatus = True Then
                Me.pbxServer.Image = My.Resources._20604_24_button_ok_icon
            Else
                Me.pbxServer.Image = My.Resources.errorImage
            End If
            Array.Clear(strServerName, 0, strServerName.Length)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.UsernameTextBox.Text.Length < 1 Then
            msg_Error("Please enter user name")
            Me.UsernameTextBox.Focus()
            Exit Sub
        End If
        Label1.Text = UsernameTextBox.Text
        Panel1.Show()
        Panel2.Hide()

    End Sub
    Private Sub Panel3_Click(sender As Object, e As EventArgs) Handles Panel3.Click
        Panel2.Show()
        Panel1.Hide()
    End Sub


    Private Sub UsernameTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles UsernameTextBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1_Click(Me, Nothing)
        End If
    End Sub
    Dim StringToPrint As String

    '    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '        Dim t As String = TextBox1.Text
    '        Dim a As String = Mid(TextBox3.Text, 1, 1)
    '        Dim b As String = Mid(TextBox1.Text, 1, 1)
    '        Dim rand As New Random
    '        Dim c As Color = Color.FromArgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, _
    '256))


    '        TextBox2.BackColor = c
    '        TextBox4.Text = TextBox3.Text & " " & TextBox1.Text
    '        TextBox2.Text = a & b
    '        PictureBox1.Image = ConvertTextToImage(TextBox2.Text, c)
    '    End Sub
    Public Function ConvertTextToImage(txt As String, bgcolor As Color) As Bitmap
        Dim bmp As New Bitmap(25, 25)
        Using graphics__1 As Graphics = Graphics.FromImage(bmp)
            Dim font As New Font("Microsoft Sans Serif", 10)
            graphics__1.FillRectangle(New SolidBrush(bgcolor), 0, 0, 25, 25)
            graphics__1.DrawString(txt, font, New SolidBrush(Color.White), 0, 0)
            graphics__1.Flush()
            font.Dispose()
            graphics__1.Dispose()
        End Using
        Return bmp
    End Function



    Private Sub companylogo_Click(sender As Object, e As EventArgs)
        Try
            Dim a As PictureBox = CType(sender, PictureBox)
            ComboBox1.SelectedValue = a.Tag


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub Panel4_Click(sender As Object, e As EventArgs) Handles Panel4.Click
        If Panel5.Visible = False Then
            Panel5.Visible = True
            Panel5.Show()
        Else
            Panel5.Visible = False
            Panel5.Hide()
        End If

    End Sub




    'Private Sub ListView1_Click(sender As Object, e As EventArgs) Handles ListView1.Click
    '    Dim opt As ListView = CType(sender, ListView)
    '    'opt.SelectedItems =
    '    Panel5.Hide()
    '    TextBox1.Visible = True
    '    TextBox1.Text = UsernameTextBox.Text
    'End Sub

    'Private Sub ListView1_ItemSelectionChanged(sender As Object, e As EventArgs) Handles ListView1.ItemSelectionChanged
    '    Try
    '        'If IsOpenForm = False Then Exit Sub
    '        'Dim listview As ListView = CType(sender, ListView)
    '        'Dim tes As ListView.SelectedListViewItemCollection = listview.SelectedItems

    '        Dim tes As ListView.SelectedListViewItemCollection = _
    '    Me.ListView1.SelectedItems

    '        Dim ID As Object = 0I
    '        Dim item As ListViewItem
    '        For Each item In tes
    '            ID = item.SubItems(0).Tag
    '        Next
    '        Me.ComboBox1.SelectedValue = ID
    '        Me.ComboBox1_SelectedIndexChanged(Nothing, Nothing)
    '        Me.Panel1.Show()
    '        Me.Panel1.BringToFront()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnaddComp_Click(sender As Object, e As EventArgs) Handles btnaddComp.Click
        Try
            LinkLabel2_LinkClicked(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkRememberMyPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkRememberMyPassword.CheckedChanged
        Try
            UserRememberMe()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function LoadImage() As DataTable
        Try

            Dim dt As New DataTable
            dt.TableName = "UserImages"
            dt.Columns.Add("User_Id", GetType(System.Int32))
            dt.Columns.Add("User_Name", GetType(System.String))
            dt.Columns.Add("User_Picture", GetType(System.String))
            dt.Columns.Add("UserImage", GetType(System.Byte()))
            dt.AcceptChanges()

            If IO.File.Exists(str_ApplicationStartUpPath & "\ApplicationSettings\UserImages.xml") Then
                dt.ReadXml(str_ApplicationStartUpPath & "\ApplicationSettings\UserImages.xml")
            End If
            dt.AcceptChanges()

            Return dt

        Catch ex As Exception
            'Throw ex
        End Try
    End Function
    Public Function FillImage(bt() As Byte) As Image
        Try
            Dim ms As New MemoryStream
            ms.Write(bt, 0, bt.Length)
            Dim img As Image
            img = Image.FromStream(ms)
            ms.Dispose()
            Return img
        Catch ex As Exception
            ' Throw ex
        End Try
    End Function

    Private Sub UsernameTextBox_LostFocus(sender As Object, e As EventArgs) Handles UsernameTextBox.LostFocus
        Try
            If _DtImage Is Nothing Then Exit Sub
            If Me.UsernameTextBox.Text.Length > 0 Then
                Dim dr() As DataRow = _DtImage.Select("User_Name='" & UsernameTextBox.Text.ToUpper & "'")
                If dr.Length > 0 Then
                    Me.PictureBox1.Image = FillImage(dr(0).Item("UserImage"))
                    Me.PictureBox3.Image = FillImage(dr(0).Item("UserImage"))
                Else
                    Me.PictureBox1.Image = Nothing
                    Me.PictureBox3.Image = Nothing
                End If
            End If

        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function ValidateSystem(ByVal SystemName As String, ByVal SystemID As String) As Boolean
        Dim dt As New DataTable
        Dim query As String = ""
        Dim val As Boolean = False
        Try
            query = "Select * From tblSystemList Where SystemName= '" & SystemName & "' Or SystemId = '" & EncryptLicense(SystemID) & "'"
            dt = GetDataTable(query)

            If dt.Rows.Count = 0 Then
                val = False
            Else
                val = True
            End If
            Return val
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub ListView1_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        Try
            'If IsOpenForm = False Then Exit Sub
            'Dim listview As ListView = CType(sender, ListView)
            'Dim tes As ListView.SelectedListViewItemCollection = listview.SelectedItems
            '    e.Item.Tag
            '    Dim tes As ListView.SelectedListViewItemCollection = _
            'Me.ListView1.SelectedItems

            '    Dim ID As Object = 0I
            '    Dim item As ListViewItem
            '    For Each item In tes
            '        ID = item.SubItems(0).Tag
            '    Next
            Me.ComboBox1.Text = e.Item.Text
            Me.ComboBox1_SelectedIndexChanged(Nothing, Nothing)
            'Me.Panel1.Show()
            'Me.Panel1.BringToFront()
            'Panel2.Show()
            'Panel1.Hide()
            'Panel4_Click(Me, Nothing)

            Panel5.Visible = False
            Panel5.Hide()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS1175
    ''' </summary>
    ''' <remarks> Hide and show logos and icons on login screen and home screen on configuration file based.</remarks>
    Public Sub LogosConfigurationFileExists()
        Try
            Dim dsConnection As New DataSet("Configuration")
            'If Not System.IO.File.Exists(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml") Then
            '    Dim dt As New DataTable("Logos")
            '    dt.Columns.Add("Hide")
            '    Dim dr As DataRow
            '    dr = dt.NewRow
            '    dr.Item(0) = "False"
            '    dt.Rows.InsertAt(dr, 0)
            '    'dt.WriteXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml")
            '    dsConnection.Tables.Add(dt)
            '    dsConnection.WriteXml(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml")

            'End If

            ''If IO.File.ReadAllText(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml").StartsWith("FilePath") Then
            'If IO.File.ReadAllText(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml").StartsWith("Logos") Then

            '    'Me.TabControl1.SelectedTab = TabControl1.TabPages(0)
            '    Dim str As String() = IO.File.ReadAllText(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml").Split("|")
            'txtBrowseConnection.Text = str(1).ToString
            If System.IO.File.Exists(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml") Then
                dsConnection.ReadXml(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml")
                Dim dtLogos As DataTable = dsConnection.Tables(0)
                If dtLogos.Rows(0).Item(0).ToString = "False" Then
                    HideLogosIcons = False
                    Me.Panel6.Visible = False
                    Me.Panel7.Visible = False
                Else
                    HideLogosIcons = True
                    Me.Panel6.Visible = True
                    Me.Panel7.Visible = True
                    Me.Icon = Nothing
                    frmMain.Icon = Nothing
                    ''TASK TFS1965 Muhammad Ameen. Hide or show softbeats solution title on main form.
                    frmMain.Text = String.Empty
                    ''END TASKTFS1965
                End If

            Else
                HideLogosIcons = False
                Me.Panel6.Visible = False
                Me.Panel7.Visible = False
                'If Not dsConnection.Tables(0).Columns.Contains("ReportPath") Then
                '    dsConnection.Tables(0).Columns.Add("ReportPath")
                'End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub LogosDBKeyExists()
        Try
            Dim dsConnection As New DataSet("Configuration")
            'If Not System.IO.File.Exists(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml") Then
            '    Dim dt As New DataTable("Logos")
            '    dt.Columns.Add("Hide")
            '    Dim dr As DataRow
            '    dr = dt.NewRow
            '    dr.Item(0) = "False"
            '    dt.Rows.InsertAt(dr, 0)
            '    'dt.WriteXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml")
            '    dsConnection.Tables.Add(dt)
            '    dsConnection.WriteXml(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml")

            'End If

            ''If IO.File.ReadAllText(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml").StartsWith("FilePath") Then
            'If IO.File.ReadAllText(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml").StartsWith("Logos") Then

            '    'Me.TabControl1.SelectedTab = TabControl1.TabPages(0)
            '    Dim str As String() = IO.File.ReadAllText(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml").Split("|")
            'txtBrowseConnection.Text = str(1).ToString
            Dim GluonLogoVisibility As String = getConfigValueByType("GluonLogoVisibility")
            If Not GluonLogoVisibility = "Error" Then
                If Not System.IO.File.Exists(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml") Then
                    Dim dt As New DataTable("Logos")
                    dt.Columns.Add("Hide")
                    Dim dr As DataRow
                    dr = dt.NewRow
                    dr.Item(0) = GluonLogoVisibility
                    dt.Rows.InsertAt(dr, 0)
                    'dt.WriteXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml")
                    dsConnection.Tables.Add(dt)
                    dsConnection.WriteXml(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml")
                    dsConnection.ReadXml(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml")
                    Dim dtLogos As DataTable = dsConnection.Tables(0)
                    If dtLogos.Rows(0).Item(0).ToString = "False" Then
                        HideLogosIcons = False
                        Me.Panel6.Visible = False
                        Me.Panel7.Visible = False
                    Else
                        HideLogosIcons = True
                        Me.Panel6.Visible = True
                        Me.Panel7.Visible = True
                        Me.Icon = Nothing
                    End If
                    'If Not dsConnection.Tables(0).Columns.Contains("ReportPath") Then
                    '    dsConnection.Tables(0).Columns.Add("ReportPath")
                    'End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class



