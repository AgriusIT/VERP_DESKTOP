Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.IO
Imports System.Xml
Imports System.Text
Public Class CompanyAndConnectionInfo
    Implements IGeneral
    Dim CompanyCon As CompConnectionInfo
    Dim Id As String = 0
    Dim Ids As Integer
    Dim ConFlg As Boolean = False
    Dim IsFormLoaded As Boolean = False
    Public _Server_Name As String
    Public _DB_UserName As String
    Public _DB_Password As String

    Public Enum GrdEnum
        Id
        Title
        Servername
        UserId
        Password
        DBName
        ReportPath
    End Enum

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            Dim dt As New DataTable
            Dim objds As New DataSet
            dt = CType(Me.grdConnectionInfo.DataSource, DataTable)
            dt.TableName = "Connections"
            'dt.PrimaryKey = New DataColumn() {dt.Columns("DBName")}
            Dim Row_Index As Integer = Me.grdConnectionInfo.CurrentRow.RowIndex
            'Dim dr() As DataRow = dt.Select("Title='" & grdConnectionInfo.GetRow.Cells("Title").Value & "'")
            dt.Rows.RemoveAt(Row_Index)
            'Dim dr As DataRow
            'dr = dt.NewRow
            'dr.Item(GrdEnum.Id) = dt.Rows.Count - 1
            'dr.Item(GrdEnum.UserId) = Me.txtUserId.Text
            'dr.Item(GrdEnum.Title) = Me.txtTitle.Text
            'dr.Item(GrdEnum.Servername) = Me.txtServerName.Text
            'dr.Item(GrdEnum.Password) = Me.txtPassword.Text
            'dr.Item(GrdEnum.DBName) = Me.txtDBName.Text
            'dt.Rows.Add(dr)
            dt.AcceptChanges()
            'objds.Tables.Add(dt)
            dt.WriteXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml")

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            CompanyCon = New CompConnectionInfo

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dsConnection As New DataSet
            If Not System.IO.File.Exists(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml") Then
                Dim dt As New DataTable("Connections")
                dt.Columns.Add(GrdEnum.Id.ToString)
                dt.Columns.Add(GrdEnum.Title.ToString)
                dt.Columns.Add(GrdEnum.Servername.ToString)
                dt.Columns.Add(GrdEnum.UserId.ToString)
                dt.Columns.Add(GrdEnum.Password.ToString)
                dt.Columns.Add(GrdEnum.DBName.ToString)
                dt.Columns.Add(GrdEnum.ReportPath.ToString)
                Dim dr As DataRow

                dr = dt.NewRow
                dr.Item(0) = 1
                dr.Item(1) = "Default"
                dr.Item(2) = "Rai"
                dr.Item(3) = "sa"
                dr.Item(4) = Encrypt("sa")
                dr.Item(5) = "SimplePos"
                dr.Item(6) = ""

                dt.Rows.InsertAt(dr, 0)
                'dt.WriteXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml")
                dsConnection.Tables.Add(dt)
                dsConnection.WriteXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml")

            End If

            If IO.File.ReadAllText(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml").StartsWith("FilePath") Then

                Me.TabControl1.SelectedTab = TabControl1.TabPages(0)
                Dim str As String() = IO.File.ReadAllText(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml").Split("|")
                txtBrowseConnection.Text = str(1).ToString

            Else

                Me.TabControl1.SelectedTab = TabControl1.TabPages(1)

                dsConnection.ReadXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml")
                Me.grdConnectionInfo.DataSource = dsConnection.Tables(0)

                If Not dsConnection.Tables(0).Columns.Contains("ReportPath") Then
                    dsConnection.Tables(0).Columns.Add("ReportPath")
                End If


                ' Me.grdConnectionInfo.RetrieveStructure()
                Me.grdConnectionInfo.CollapseCards()

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.grdConnectionInfo.RowCount > 0 Then
                If Me.BtnSave.Text = "&Save" Then
                    Dim dt As DataTable = CType(Me.grdConnectionInfo.DataSource, DataTable)
                    Dim dr() As DataRow
                    dr = dt.Select("Title='" & Me.txtTitle.Text & "'")
                    If Not dr Is Nothing Then
                        If dr.Length > 0 Then
                            ShowErrorMessage("Company already exits ")
                            Exit Function
                        End If
                    End If
                End If
            End If
            If Me.txtTitle.Text = String.Empty Then
                ShowErrorMessage("Please Enter Title")
                Me.txtTitle.Focus()
                Return False
            End If
            If Me.txtServerName.Text = String.Empty Then
                ShowErrorMessage("Please Enter Server Name")
                Me.txtServerName.Focus()
                Return False
            End If
            If Me.RadioButton1.Checked = True Then
                If Me.txtUserId.Text = String.Empty Then
                    ShowErrorMessage("Please Enter Database User Name")
                    Me.txtUserId.Focus()
                    Return False
                End If
                If Me.txtPassword.Text = String.Empty Then
                    ShowErrorMessage("Please Enter Database Password")
                    Me.txtPassword.Focus()
                    Return False
                End If
            End If
            If txtDBName.Text = String.Empty Then
                ShowErrorMessage("Please Enter Database Name")
                txtDBName.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.BtnSave.Text = "&Save"
            Me.ProgressBar1.Visible = True
            Me.ProgressBar1.Value = 0
            lblPrograssbar.Text = String.Empty
            Me.txtTitle.Text = String.Empty
            Me.txtServerName.Text = String.Empty
            Me.txtUserId.Text = String.Empty
            Me.txtPassword.Text = String.Empty
            txtDBName.Text = String.Empty
            txtReportsFolder.Text = String.Empty

            'txtServerName_SelectedIndexChanged(Nothing, Nothing)
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Dim dt As New DataTable
            Dim ds As New DataSet
            dt = CType(Me.grdConnectionInfo.DataSource, DataTable)
            If dt Is Nothing Then
                dt = New DataTable("Connections")
                dt.Columns.Add(GrdEnum.Id.ToString)
                dt.Columns.Add(GrdEnum.Title.ToString)
                dt.Columns.Add(GrdEnum.Servername.ToString)
                dt.Columns.Add(GrdEnum.UserId.ToString)
                dt.Columns.Add(GrdEnum.Password.ToString)
                dt.Columns.Add(GrdEnum.DBName.ToString)
                dt.Columns.Add(GrdEnum.ReportPath.ToString)
                Dim dr As DataRow

                dr = dt.NewRow
                dr.Item(GrdEnum.Id) = 1 'dt.Rows.Count + 1
                dr.Item(GrdEnum.Title) = Me.txtTitle.Text.ToString
                dr.Item(GrdEnum.Servername) = Me.txtServerName.Text.ToString
                dr.Item(GrdEnum.UserId) = Me.txtUserId.Text.ToString
                dr.Item(GrdEnum.Password) = Encrypt(Me.txtPassword.Text.ToString)
                dr.Item(GrdEnum.DBName) = txtDBName.Text.ToString
                dr.Item(GrdEnum.ReportPath) = txtReportsFolder.Text.ToString

                dt.Rows.InsertAt(dr, 0)
                dt.WriteXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml")

            Else
                dt.TableName = "Connections"
                'dt.PrimaryKey = New DataColumn() {dt.Columns("DBName")}
                Dim IdCont() As DataRow = dt.Select("Id=Max(Id)", "")
                Dim RCont As Integer = IdCont(0).ItemArray(0)
                dt.AcceptChanges()
                lblPrograssbar.Text = ""
                Me.lblPrograssbar.Text = "Creating Connection..."
                Do Until Me.ProgressBar1.Value >= 50
                    ProgressBar1.Value = ProgressBar1.Value + 1
                    Application.DoEvents()
                    System.Threading.Thread.Sleep(50)
                Loop
                Dim dr As DataRow
                dr = dt.NewRow
                dr.Item(GrdEnum.Id) = RCont + 1 'dt.Rows.Count + 1
                dr.Item(GrdEnum.Title) = Me.txtTitle.Text.ToString
                dr.Item(GrdEnum.Servername) = Me.txtServerName.Text.ToString
                dr.Item(GrdEnum.UserId) = Me.txtUserId.Text.ToString
                dr.Item(GrdEnum.Password) = Encrypt(Me.txtPassword.Text.ToString)
                dr.Item(GrdEnum.DBName) = txtDBName.Text.ToString
                dr.Item(GrdEnum.ReportPath) = txtReportsFolder.Text.ToString
                dt.Rows.InsertAt(dr, 0)
                dt.AcceptChanges()
                dt.WriteXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml")
                Do Until Me.ProgressBar1.Value >= 99
                    ProgressBar1.Value = ProgressBar1.Value + 1
                    Application.DoEvents()
                    System.Threading.Thread.Sleep(50)
                Loop

            End If

            Me.lblPrograssbar.Text = "Connection Complete Successfully"
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            Dim Row_Index As Integer
            Dim dt As New DataTable
            Dim ds As New DataSet
            dt = CType(Me.grdConnectionInfo.DataSource, DataTable)
            Row_Index = Me.grdConnectionInfo.CurrentRow.RowIndex
            dt.Rows(Row_Index).Delete()
            Me.lblPrograssbar.Text = ""
            Me.lblPrograssbar.Text = "Updating Connection..."
            Do Until Me.ProgressBar1.Value >= 50
                ProgressBar1.Value = ProgressBar1.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item(GrdEnum.Id) = Id 'dt.Compute("Max(Id)", String.Empty) + 1
            dr.Item(GrdEnum.Title) = Me.txtTitle.Text.ToString
            dr.Item(GrdEnum.Servername) = Me.txtServerName.Text.ToString
            dr.Item(GrdEnum.UserId) = Me.txtUserId.Text.ToString
            dr.Item(GrdEnum.Password) = Encrypt(Me.txtPassword.Text.ToString)
            dr.Item(GrdEnum.DBName) = txtDBName.Text.ToString
            dr.Item(GrdEnum.ReportPath) = txtReportsFolder.Text.ToString
            dt.Rows.InsertAt(dr, 0)
            dt.AcceptChanges()
            'ds.Tables.Add(dt)
            dt.WriteXml(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml")
            Do Until Me.ProgressBar1.Value >= 99
                ProgressBar1.Value = ProgressBar1.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            Me.lblPrograssbar.Text = "Connection Complete Successfully"
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub BtnConnectionTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConnectionTest.Click
        Me.Cursor = Cursors.WaitCursor
        Try

            Dim Con_Str As String = String.Empty
            If Me.RadioButton1.Checked = True Then
                Con_Str = "Provider=SQLOLEDB.1;Data Source=" & Me.txtServerName.Text & ";User Id=" & Me.txtUserId.Text & ";Password=" & Me.txtPassword.Text & ";Initial Catalog=" & txtDBName.Text & ";Integrated Security Info=False"
            Else
                Con_Str = "Provider=SQLOLEDB.1;Data Source=" & Me.txtServerName.Text & ";Initial Catalog=" & txtDBName.Text & ";Integrated Security Info=True"
            End If

            Dim Cn As New OleDb.OleDbConnection(Con_Str)
            If Cn.State = ConnectionState.Closed Then
                Cn.Open()
                ConFlg = True
                ShowErrorNotification("Tested connection succeeded")
            Else
                ConFlg = False
            End If
            Cn.Close()
        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
            ShowErrorNotification("Could not connect to database", MsgBoxStyle.Critical)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    'Aashir: dismiss notification
    Private Sub btnDismissMessage_Click(sender As Object, e As EventArgs) Handles btnDismissMessage.Click
        Try

            Me.pnlErrorNotification.Visible = False

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub tmrLabel_Tick(sender As Object, e As EventArgs) Handles tmrMessageNotificationLabel.Tick
        pnlErrorNotification.Visible = False
    End Sub
    'Aashir: show notification on test connection
    Public Sub ShowErrorNotification(ByVal MessageText As String, Optional ByVal MessageStyle As MsgBoxStyle = MsgBoxStyle.Information)
        Try
            tmrMessageNotificationLabel.Stop()
            tmrMessageNotificationLabel.Enabled = False

            'If MessageStyle = MsgBoxStyle.Information Or MessageStyle = Nothing Then


            If MessageStyle = MsgBoxStyle.Critical Then
                pnlErrorNotification.BackColor = Color.FromArgb(238, 17, 17)

            Else
                pnlErrorNotification.BackColor = Color.FromArgb(0, 91, 174)
            End If

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
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Not IsValidate() Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            If Me.BtnSave.Text = "&Save" Then
                'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                If Save() Then DialogResult = Windows.Forms.DialogResult.Yes
                'MsgBox("Record Saved Successfully", MsgBoxStyle.Information, str_MessageHeader)
                'msg_Information(str_informSave)
                Me.ReSetControls()
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Update1() Then DialogResult = Windows.Forms.DialogResult.Yes
                'MsgBox("Record Update Successfully", MsgBoxStyle.Information, str_MessageHeader)
                'msg_Information(str_informUpdate)
                Me.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Hide()
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub CompanyAndConnectionInfo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 10 -1-14
            If e.KeyCode = Keys.F4 Then
                BtnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                BtnNew_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub CompanyAndConnectionInfo_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            'Dim dt As New DataTable
            'dt = SmoApplication.EnumAvailableSqlServers(False)
            'txtServerName.DataSource = dt
            'txtServerName.DisplayMember = dt.Columns(0).ColumnName.ToString
            'txtServerName.ValueMember = dt.Columns(0).ColumnName.ToString
            ReSetControls()
            IsFormLoaded = True
            'txtServerName_SelectedIndexChanged(Nothing, Nothing)
            'ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub CompanyAndConnectionInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub EditRecord()
        Try
            'Me.ReSetControls()
            Id = Me.grdConnectionInfo.GetRow.Cells(GrdEnum.Id).Text.ToString
            Me.txtTitle.Text = Me.grdConnectionInfo.GetRow.Cells(GrdEnum.Title).Text
            Me.txtServerName.Text = Me.grdConnectionInfo.GetRow.Cells(GrdEnum.Servername).Text
            Me.txtUserId.Text = Me.grdConnectionInfo.GetRow.Cells(GrdEnum.UserId).Text
            If Not (Decrypt(Me.grdConnectionInfo.GetRow.Cells(GrdEnum.Password).Text).Contains("Base-64") Or Decrypt(Me.grdConnectionInfo.GetRow.Cells(GrdEnum.Password).Text).Contains("Bad")) Then
                Me.txtPassword.Text = Decrypt(Me.grdConnectionInfo.GetRow.Cells(GrdEnum.Password).Text)
            Else
                Me.txtPassword.Text = Me.grdConnectionInfo.GetRow.Cells(GrdEnum.Password).Text
            End If
            txtDBName.Text = Me.grdConnectionInfo.GetRow.Cells(GrdEnum.DBName).Text
            txtReportsFolder.Text = Me.grdConnectionInfo.GetRow.Cells(GrdEnum.ReportPath).Text
            Me.BtnSave.Text = "&Update"
            If Me.txtUserId.Text <> "" Then
                Me.RadioButton1.Checked = True
            ElseIf Me.txtUserId.Text = "" Then
                Me.RadioButton2.Checked = True
            Else
                Me.RadioButton1.Checked = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grdConnectionInfo_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdConnectionInfo.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            'If IsValidate() = True Then
            If Me.grdConnectionInfo.RowCount > 0 Then
                'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Delete() Then DialogResult = Windows.Forms.DialogResult.Yes
                'MsgBox("Record Deleted Successfully", MsgBoxStyle.Information, str_MessageHeader)
                'msg_Information(str_informDelete)
                Me.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    'Private Sub txtServerName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If IsFormLoaded = True Then
    '            txtDBName.Items.Clear()
    '            If Me.txtUserId.Text = String.Empty Then Exit Sub
    '            If Me.txtPassword.Text = String.Empty Then Exit Sub
    '            Dim dbServer As Server
    '            Dim ServerName As String = txtServerName.Text
    '            dbServer = New Server(ServerName)
    '            dbServer.ConnectionContext.LoginSecure = False
    '            dbServer.ConnectionContext.Login = Me.txtUserId.Text
    '            dbServer.ConnectionContext.Password = Me.txtPassword.Text
    '            For Each DatabaseList As Database In dbServer.Databases
    '                With txtDBName
    '                    .Items.Add(DatabaseList.Name)
    '                    .ValueMember = DatabaseList.Name.ToString
    '                End With
    '            Next
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub txtPassword_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'If Me.txtServerName.SelectedIndex < 0 Then Exit Sub
            If Me.txtUserId.Text = String.Empty Then Exit Sub
            If Me.txtPassword.Text = String.Empty Then Exit Sub
            'txtServerName_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnServer.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            frmServersList.ShowDialog()
            If frmServersList._Server <> "" Then
                Me.txtServerName.Text = frmServersList._Server
                frmServersList._Server = String.Empty
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            'If Me.txtServerName.Text = String.Empty Then Exit Sub
            'If Me.txtUserId.Text = String.Empty Then Exit Sub
            'If Me.txtPassword.Text = String.Empty Then Exit Sub
            _Server_Name = Me.txtServerName.Text
            _DB_UserName = Me.txtUserId.Text
            _DB_Password = Me.txtPassword.Text

            frmDBList.ShowDialog()
            If frmDBList._DBName <> "" Then
                Me.txtDBName.Text = frmDBList._DBName
                frmDBList._DBName = String.Empty
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            Me.EditRecord()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Try
            If Me.RadioButton1.Checked = True Then
                Me.txtUserId.ReadOnly = False
                Me.txtPassword.ReadOnly = False
            ElseIf Me.RadioButton2.Checked = True Then
                Me.txtUserId.ReadOnly = True
                Me.txtPassword.ReadOnly = True
            Else
                Me.txtUserId.ReadOnly = True
                Me.txtPassword.ReadOnly = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub grdConnectionInfo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdConnectionInfo.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14
        If e.KeyCode = Keys.F2 Then
            BtnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            BtnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If

    End Sub

    Private Sub btnBrowsReports_Click(sender As Object, e As EventArgs) Handles btnBrowsReports.Click
        Try
            If fbDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtReportsFolder.Text = fbDialog.SelectedPath
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            'Finally
            '    Con.Close()
        End Try
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try

            If fbDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtBrowseConnection.Text = fbDialog.SelectedPath
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnSaveConnectionFile_Click(sender As Object, e As EventArgs) Handles btnSaveConnectionFile.Click
        Try

            If txtBrowseConnection.Text.Trim = "" Then
                msg_Error("Please select connection file location")
                Me.txtBrowseConnection.Focus()
                Exit Sub
            End If

            If Not IO.File.Exists(txtBrowseConnection.Text & "\CompanyConnectionInfo.xml") Then
                msg_Error("Could not find connection file please recheck and try again.")
                Me.txtBrowseConnection.Focus()
                Exit Sub
            End If

            IO.File.WriteAllText(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml", "FilePath|" & txtBrowseConnection.Text)
            msg_Information("Information saved successfully.")
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

End Class