Imports System.Configuration

Public Class frmDBSelection
    Dim strServerName() As String = {}
    Private SIRIUSPartnerName As String = String.Empty
    'Private _DbName As String = String.Empty
    'Private _Password As String = String.Empty
    'Private _UserName As String = String.Empty





    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frmMainLogin.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        frmMainLogin.Show()
        Me.Close()
    End Sub

    Private Sub frmDBSelection_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
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
                    ' Me.Panel7.Visible = True
                    Me.Icon = Nothing
                    'frmMain.Icon = Nothing
                    frmModProperty.Icon = Nothing
                    ''TASK TFS1965 Muhammad Ameen. Hide or show SIRIUS solution title on main form.
                    'frmMain.Text = String.Empty
                    frmModProperty.Text = String.Empty
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
    Private Sub frmDBSelection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnAdd.FlatAppearance.BorderSize = 0
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
                '_DtImage = LoadImage()
                '_DtImage.CaseSensitive = False
            LoadLastSettings()
                LoadCombo()
                'IsOpenForm = True
                'Try


                '    Me.ComboBox1.Text = ConCompany
                '    'Me.UsernameTextBox.Text = LoginUserCode

                '    If LoginUserRememberMe = True Then
                '        Me.PasswordTextBox.Text = LoginUserPassword
                '    End If
                '    Me.chkRememberMyPassword.Checked = LoginUserRememberMe

                'Catch ex As Exception

                'End Try

                'Try
                '    If Me.UsernameTextBox.Text.Length > 0 Then
                '        If _DtImage IsNot Nothing Then
                '            Dim dr() As DataRow = _DtImage.Select("User_Name='" & UsernameTextBox.Text.ToUpper & "'")
                '            If dr.Length > 0 Then
                '                Me.PictureBox1.Image = FillImage(dr(0).Item("UserImage"))
                '                Me.PictureBox3.Image = FillImage(dr(0).Item("UserImage"))
                '            End If
                '        End If
                '    End If
                'Catch ex As Exception
                'End Try
                'If blnSwitchUser = False Then
                '    If Me.ComboBox1.Text.Length > 0 AndAlso Me.UsernameTextBox.Text.Length > 0 AndAlso Me.PasswordTextBox.Text.Length > 0 Then
                '        Me.OK_Click(Nothing, Nothing)
                '    End If
                'End If


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
                'End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            LinkLabel2_LinkClicked(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) ' Handles LinkLabel2.LinkClicked
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

            'Panel5.Visible = False
            'Panel5.Hide()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) As ComboBox
        Try
            'If IsOpenForm = False Then Exit Sub
            If Me.ComboBox1.SelectedIndex = -1 Then Exit Function
            strServerName = CType(Me.ComboBox1.SelectedItem, DataRowView).Row("ServerName").ToString.Split("\")
            Me.TextBox1.Text = CType(Me.ComboBox1.SelectedItem, DataRowView).Row.Item("Title").ToString
            Dim i As Integer = CType(Me.ComboBox1.SelectedItem, DataRowView).Row.Item("Id")
            ModGlobel.TitleOfDb = Me.TextBox1.Text
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
            Dim var As String = Me.TextBox1.Text
            If (var = Me.Text) Then
                Me.Text = Me.TextBox1.Text & " [" & Application.ProductVersion & "]"
            Else
                Me.Text = var
            End If

            frmMainLogin.LoadCombo()
            Me.Close()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        Return ComboBox1
    End Function
End Class