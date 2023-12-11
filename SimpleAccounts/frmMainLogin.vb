Imports System.Data.OleDb
Imports System.Configuration
Imports System.IO

Public Class frmMainLogin
    Private _User As String = String.Empty
    Private _Password As String = String.Empty
    Private _UserName As String = String.Empty
    Private _UserPassword As String = String.Empty
    Private _RememberMe As Boolean = False
    Private _CompanyTitle As String = String.Empty
    Private UserVerified As Boolean = False
    Private SIRIUSPartnerName As String = String.Empty
    Public Shared strStartUpPath As String = String.Empty
    Public blnSwitchUser As Boolean = False
    Dim strServerName() As String = {}
    Private _DtImage As New DataTable

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
                        'Me.txtUsername.Text = _UserName
                        TextBox1.Text = _UserName
                    Else
                        configsection.Settings.Add("LastLogin", _UserName) 'Me.UsernameTextBox.Text)
                    End If
                    If Not configsection.Settings("SavePassword") Is Nothing Then
                        configsection.Settings("SavePassword").Value = IIf(_RememberMe = True, Encrypt(_UserPassword), String.Empty)
                    Else
                        configsection.Settings.Add("SavePassword", IIf(Me.cbStaySignedIn.Checked = True, Encrypt(_UserPassword), String.Empty))
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


    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Try
            str_ApplicationStartUpPath = Application.StartupPath
            strStartUpPath = Application.StartupPath
        Catch ex As Exception
            msg_Error("An error occured while connecting to the server  " & Chr(13) & ex.Message)
            End
        End Try

        'If Not IO.File.Exists(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml") Then
        '    frmSetup.BringToFront()
        '    frmSetup.ShowDialog()
        '    If Not IO.File.Exists(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml") Then
        '        End
        '    End If
        'End If
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
            If _RememberMe = True Then
                frmModProperty.ShowDialog()
                frmModProperty.BringToFront()
            End If
            ''END TASK TFS1175
        Catch ex As Exception

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
            'If getConfigValueByType("SIRIUSPartner").ToString = "True" Then
            '    Dim Config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            '    Dim ConfigSection As AppSettingsSection = Config.AppSettings
            '    If Not ConfigSection Is Nothing Then
            '        If ConfigSection.IsReadOnly = False AndAlso ConfigSection.SectionInformation.IsLocked = False Then
            '            If Not ConfigSection.Settings("PartnerName") Is Nothing Then
            '                ConfigSection.Settings("PartnerName").Value = Encrypt(GetConfigValue("SIRIUSPartnerName").ToString)
            '            Else
            '                ConfigSection.Settings.Add("PartnerName", Encrypt(GetConfigValue("SIRIUSPartnerName").ToString))
            '            End If
            '        End If
            '        Config.Save()
            '    End If
            'R:M6 configurate change 
            If getConfigValueByType("SIRIUSPartner").ToString = "True" Then
                Dim Config As System.Configuration.Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                Dim ConfigSection As AppSettingsSection = Config.AppSettings
                If Not ConfigSection Is Nothing Then
                    If ConfigSection.IsReadOnly = False AndAlso ConfigSection.SectionInformation.IsLocked = False Then
                        If Not ConfigSection.Settings("PartnerName") Is Nothing Then
                            ConfigSection.Settings("PartnerName").Value = Encrypt(getConfigValueByType("SIRIUSPartnerName").ToString)
                        Else
                            ConfigSection.Settings.Add("PartnerName", Encrypt(getConfigValueByType("SIRIUSPartnerName").ToString))
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
                    'Me.Panel6.Visible = False
                    'Me.Panel7.Visible = False
                Else
                    HideLogosIcons = True
                    'Me.Panel6.Visible = True
                    'Me.Panel7.Visible = True
                    Me.Icon = Nothing
                    'frmMain.Icon = Nothing
                    ''TASK TFS1965 Muhammad Ameen. Hide or show SIRIUS solution title on main form.
                    'frmMain.Text = String.Empty
                    ''END TASKTFS1965
                End If

            Else
                HideLogosIcons = False
                'Me.Panel6.Visible = False
                'Me.Panel7.Visible = False
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
            Dim AgriusLogoVisibility As String = getConfigValueByType("AgriusLogoVisibility")
            If Not AgriusLogoVisibility = "Error" Then
                If Not System.IO.File.Exists(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml") Then
                    Dim dt As New DataTable("Logos")
                    dt.Columns.Add("Hide")
                    Dim dr As DataRow
                    dr = dt.NewRow
                    dr.Item(0) = AgriusLogoVisibility
                    dt.Rows.InsertAt(dr, 0)
                    'dt.WriteXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml")
                    dsConnection.Tables.Add(dt)
                    dsConnection.WriteXml(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml")
                    dsConnection.ReadXml(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml")
                    Dim dtLogos As DataTable = dsConnection.Tables(0)
                    If dtLogos.Rows(0).Item(0).ToString = "False" Then
                        HideLogosIcons = False
                        'Me.Panel6.Visible = False
                        'Me.Panel7.Visible = False
                    Else
                        HideLogosIcons = True
                        'Me.Panel6.Visible = True
                        'Me.Panel7.Visible = True
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
                    Me.ComboBox1.Text = Text
                End If
            End If
            'Me.ComboBox1.Text = ModGlobel.TitleOfDb

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

            'Me.ListView1.Items.Clear()
            'For Each row As DataRow In dt.Rows
            '    Dim listItem As New ListViewItem
            '    listItem.Name = row.Item(1).ToString
            '    listItem.Text = row.Item(1).ToString
            '    listItem.Tag = row.Item(0).ToString
            '    listItem.ImageKey = 1
            '    ListView1.Items.Add(listItem)
            'Next

        Catch ex As Exception
            Throw ex
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
    Private Sub frmMainLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'btnBack.FlatAppearance.BorderSize = 0
        'btnLogin.FlatAppearance.BorderSize = 0
        'need to pass connection string
        'SBDal.SQLHelper.CON_STR = ""

        TextBox1.BackColor = Color.White

        Try
            LogosConfigurationFileExists()
            If Not ConfigurationManager.AppSettings("PartnerName") Is Nothing Then
                If ConfigurationManager.AppSettings("PartnerName") <> "" Then
                    SIRIUSPartnerName = Decrypt(ConfigurationManager.AppSettings("PartnerName"))
                Else
                    SIRIUSPartnerName = Me.Text
                End If
            End If
            If SIRIUSPartnerName.Length > 0 Then
                Me.Text = SIRIUSPartnerName
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
                Me.TextBox1.Text = ConCompany
                Me.txtUsername.Text = LoginUserCode
                If blnSwitchUser = False Then
                    If LoginUserRememberMe = True Then
                        Me.txtPassword.Text = LoginUserPassword
                    End If
                End If
                Me.cbStaySignedIn.Checked = LoginUserRememberMe

            Catch ex As Exception

            End Try

            Try
                If Me.txtUsername.Text.Length > 0 Then
                    If _DtImage IsNot Nothing Then
                        Dim dr() As DataRow = _DtImage.Select("User_Name='" & txtUsername.Text.ToUpper & "'")
                        If dr.Length > 0 Then
                            'Me.PictureBox1.Image = FillImage(dr(0).Item("UserImage"))
                            Me.PictureBox3.Image = FillImage(dr(0).Item("UserImage"))
                        End If
                    End If
                End If
            Catch ex As Exception
            End Try
            If blnSwitchUser = False Then
                If Me.ComboBox1.Text.Length > 0 AndAlso Me.txtUsername.Text.Length > 0 AndAlso Me.txtPassword.Text.Length > 0 Then
                    Me.btnLogin_Click(Nothing, Nothing)
                End If
            End If

            'Panel5.Visible = False
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
            'If LoginUserCode IsNot Nothing Then
            '    If LoginUserCode.ToString.Length > 0 Then
            '        Button1_Click(Me, Nothing)
            '    End If
            'Else
            '    'Me.Panel1.Hide()
            '    'Me.Panel2.Show()
            '    Me.Show()
            'End If

        Catch ex As Exception
            msg_Error(ex.Message)
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
            'Dim var As String = Me.TextBox1.Text
            'If (var = Me.Text) Then
            Me.Text = Me.TextBox1.Text & " [" & Application.ProductVersion & "]"
            'Else
            'Me.Text = var
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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
                LoginUserRememberMe = Me.cbStaySignedIn.Checked

                If ReportColExist = True AndAlso objRow.Item("ReportPath").ToString.Trim.Length > 0 Then
                    ReportPath = objRow.Item("ReportPath").ToString
                Else
                    ReportPath = Application.StartupPath & "\Reports\"
                End If

            Else
                Dim Con1 As New OleDbConnection(SimpleAccounts.My.Settings.Database1ConnectionString) ' SBG.Con.ConnectionString)
                ConCompany = "SIRIUS"
                ConServerName = Con1.DataSource
                ConUserId = "sa"
                ConPassword = "sa"
                ConDBName = Con1.Database
                LoginUserRememberMe = Me.cbStaySignedIn.Checked
                ReportPath = Application.StartupPath & "\Reports\"
            End If
        Else
            Dim Con1 As New OleDbConnection(SimpleAccounts.My.Settings.Database1ConnectionString) ' SBG.Con.ConnectionString)
            ConCompany = "SIRIUS"
            ConServerName = Con1.DataSource
            ConUserId = "sa"
            ConPassword = "sa"
            ConDBName = Con1.Database
            LoginUserRememberMe = Me.cbStaySignedIn.Checked
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
    'Private Sub frmMainLogin_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

    '    Try
    '        If e.KeyCode = Keys.Escape Then
    '            Me.Close()
    '        Else
    '            btnLogin_Click(Nothing, Nothing)
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try

    'End Sub

    Private Sub txtUsername_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtUsername.MouseClick
        If txtUsername.ReadOnly = False Then
            txtUsername.Text = ""
        End If
    End Sub

    Private Sub txtPassword_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtPassword.MouseClick
        txtPassword.Text = ""
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
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        'frmDBSelection.ShowDialog()
        Try
            LinkLabel2_LinkClicked(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) 'Handles btnLogin.Click
        If Me.txtUsername.Text.Length < 1 Then
            msg_Error("Please enter user name")
            Me.txtUsername.Focus()
            Exit Sub
        End If
        'Label1.Text = txtUsername.Text
        'Panel1.Show()
        'Panel2.Hide()

    End Sub
    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try

            If Me.txtUsername.Text = "" And Me.txtPassword.Text = "" Then
                ShowErrorNotification("Please enter username and password")
                'ShowErrorMessage("Please enter username and password")
                Exit Sub
            End If

            If Me.txtPassword.Text = "" Then
                ShowErrorNotification("Please enter password")
                'ShowErrorMessage("Please enter password")
                txtPassword.Focus()
                Exit Sub
            End If

            If Me.txtUsername.Text = "" Then
                ShowErrorNotification("Please enter UserName")
                'ShowErrorMessage("Please enter username")
                Me.txtUsername.Focus()
                Exit Sub
            End If


            SetConnectionInfo()
            'Aashir: Verify Release for specific Customers
            If Not GetConfigValue("CustomerName").ToString = "Error" Then
                If Not GetConfigValue("CustomerName").ToString = "sirius" Then
                    ShowErrorNotification("This Release is only for SIRIUS SOLUTION")
                    Exit Sub
                End If
            End If

            Con.Open()

            If Con.State = ConnectionState.Open Then
                Con.Close()
            Else : msg_Error("Could not connect to server, please check your network") : End
            End If

        Catch ex As Exception
            msg_Error("An error occured while connecting to the server  " & Chr(13) & ex.Message)
            Exit Sub
        End Try

        Dim strUserValidity As String = CheckSecurityUser(Me.txtUsername.Text, Me.txtPassword.Text)
        str_ApplicationStartUpPath = Application.StartupPath
        _UserName = Me.txtUsername.Text
        _UserPassword = Me.txtPassword.Text
        _CompanyTitle = Me.ComboBox1.Text
        _RememberMe = Me.cbStaySignedIn.Checked

        If strUserValidity = "InValidMachine" Then
            msg_Error("An error occured while connecting the server....  " & Chr(13) & "System is not authourized to connect.")
            Me.txtPassword.Focus()


        ElseIf strUserValidity = "AlreadyLoggedIn" Then
            ShowErrorNotification("This User is already logged in")
            Exit Sub
        ElseIf strUserValidity = "In-Valid" Then
            'MsgBox(str_ErrorInvalidUser)
            'ShowErrorMessage(str_ErrorInvalidUser)
            'msg_Error(str_ErrorInvalidUser)


            ''commented By Aashir: because already logged in notification was not proper
            '--------------------------------------------
            'Dim str As String
            'str = "SELECT LoggedIn from tblUser where User_Code='" & Encrypt(txtUsername.Text.ToUpper) & "'"
            'Dim dt As DataTable = GetDataTable(str)
            'If dt.Rows(0).Item("LoggedIn").ToString = "True" Then
            '    ShowErrorNotification("This User is already logged in")
            'Else
            '    ShowErrorNotification(str_ErrorInvalidUser)
            'End If
            '--------------------------------------------------


            ShowErrorNotification(str_ErrorInvalidUser)
            Me.txtPassword.Focus()
            Exit Sub
        ElseIf strUserValidity = "In-Active" Then
            'MsgBox(str_ErrorInActiveUser)
            ShowErrorNotification(str_ErrorInActiveUser)
            'msg_Error(str_ErrorInActiveUser)
            Me.txtUsername.Focus()
            Exit Sub
        ElseIf strUserValidity = "Valid" Then
            Me.UserVerified = True
            Me.txtUsername.ReadOnly = True
            Me.txtPassword.Text = ""
            Me.ComboBox1.Enabled = False

            Dim str As String = "Switch User" + " " + "( " + Application.ProductVersion + " )"
            If Me.Text = str Then
                'frmMain.UltraStatusBar2.Panels(0).Text = "User: " & LoginUserName
            End If
            Me.Cursor = Cursors.WaitCursor
            Try

                SaveActivityLog("Config", System.Environment.MachineName.ToString, EnumActions.Login, LoginUserId, EnumRecordType.Security, txtUsername.Text.Trim, True)

                'End If
            Catch ex As Exception
            Finally
                Me.Cursor = Cursors.Default
            End Try
            If _RememberMe = True Then
                Me.Close()
            Else
                Me.Hide()
                frmMain.Show()
                frmMain.BringToFront()
            End If
            SaveLastSettings()
        Else
            End
        End If

    End Sub
    ''Aashir:TFS3791: Error notification whwn enter wrong password or username
    Private Sub tmrLabel_Tick(sender As Object, e As EventArgs) Handles tmrMessageNotificationLabel.Tick
        pnlErrorNotification.Visible = False
    End Sub

    Private Sub ShowErrorNotification(ByVal MessageText As String)
        Try
            tmrMessageNotificationLabel.Stop()
            tmrMessageNotificationLabel.Enabled = False

            'If MessageStyle = MsgBoxStyle.Information Or MessageStyle = Nothing Then


            If Me.pnlErrorNotification.Visible = True Then
                Me.pnlErrorNotification.Visible = False
                Application.DoEvents()
            End If


            Me.lblErrorNotification.Text = MessageText
            Me.pnlErrorNotification.Visible = True
            Me.pnlErrorNotification.BringToFront()
            tmrMessageNotificationLabel.Enabled = True
            tmrMessageNotificationLabel.Start()

        Catch ex As Exception
            msg_Error(ex.Message)

        End Try
    End Sub
    'End TFS3791

    Private Sub cbStaySignedIn_CheckedChanged(sender As Object, e As EventArgs) Handles cbStaySignedIn.CheckedChanged
        Try
            UserRememberMe()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUsername.KeyDown, txtPassword.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                btnLogin_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Aashir: Dismiss error notification
    Private Sub btnDismissMessage_Click(sender As Object, e As EventArgs) Handles btnDismissMessage.Click
        Try

            Me.pnlErrorNotification.Visible = False

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class