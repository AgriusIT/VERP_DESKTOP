Imports System
Imports System.Data.SqlClient
Imports Microsoft.SqlServer
Imports SBDal.SQLHelper
Public Class frmFinishSetup

    Dim CompanyTitle As String = String.Empty
    Dim CompanyAddress As String = String.Empty
    Dim CompanyPhone As String = String.Empty
    Dim CompanyEmail As String = String.Empty
    Dim dbPath As String = String.Empty
    Dim dbBackup As String = String.Empty
    Dim conStr As String = String.Empty '"Data Source=.\SQLExpress;Database=master;Integrated Security Info=SSPI"
    'Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim strDB As String = String.Empty '"SchoolDB1"
    Dim dtSimplePOSConnection As New DataTable
    Dim dsSimplePOSConnection As New DataSet
    Dim con1 As SqlConnection
    Dim FlgError As Boolean = False
    'Sub New()
    '    conStr = get_ConnectionStringMaster.ConnectionString
    '    strDB = Get_ConnectionString.InitialCatalog
    '    ' This call is required by the designer.
    '    InitializeComponent()
    '    ' Add any initialization after the InitializeComponent() call.
    'End Sub
    Private Sub frmFinishSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Button1.Visible = False
            Me.ProgressBar1.Visible = True
            ProgressBar1.Value = 0
            Me.Button2.Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Dim con As SqlConnection = New SqlConnection(GetConnectionStringMaster.ConnectionString)
            If con.State = ConnectionState.Closed Then con.Open()
        Catch ex As Exception
            FlgError = True
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Dim con As New SqlConnection(get_ConnectionStringMaster.ConnectionString)
        strDB = Get_ConnectionString.InitialCatalog
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            cmd = New SqlCommand("Create DATABASE " & strDB & "  ON Primary (Name=N'" & strDB & "_Data', FileName=N'" & Application.StartupPath & "\DB\" & strDB & "_.mdf" & "') " _
                   & " Log On " _
                   & " (Name=N'" & strDB & "_Log', FileName=N'" & Application.StartupPath & "\DB\" & strDB & "_Log.ldf" & "') ", con)
            cmd.ExecuteNonQuery()
            cmd = Nothing

        Catch ex As Exception
            FlgError = True
            ShowErrorMessage(ex.Message)
        Finally
            cmd = Nothing
            con.Close()
        End Try
    End Sub
    Private Sub BackgroundWorker3_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Dim con As New SqlConnection(get_ConnectionStringMaster.ConnectionString)
        strDB = Get_ConnectionString.InitialCatalog
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            If _flgRestoreData = False Then
                cmd = New SqlCommand("Restore Database " & strDB & " From Disk='" & Application.StartupPath & "\RestoreBackup\BlankDB.Bak' WITH REPLACE, MOVE 'SimplePos_Data' TO '" & Application.StartupPath & "\DB\" & strDB + "_Data.mdf', Move 'SimplePos_Log' TO '" & Application.StartupPath & "\DB\" & strDB + "_Log.ldf' ", con) 'for restore data 
            Else
                cmd = New SqlCommand("Restore Database " & strDB & " From Disk='" & Application.StartupPath & "\RestoreBackup\Sample.Bak' WITH REPLACE, MOVE 'SimplePos_Data' TO '" & Application.StartupPath & "\DB\" & strDB + "_Data.mdf', Move 'SimplePos_Log' TO '" & Application.StartupPath & "\DB\" & strDB + "_Log.ldf' ", con) 'for restore data 
            End If
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            FlgError = True
            ShowErrorMessage(ex.Message)
        Finally
            cmd = Nothing
            con.Close()
        End Try
    End Sub

    Private Sub frmFinishSetup_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try


            '------------------------------ Connecting to server -------------------------
            Me.lblConnecting.Visible = True
            Me.lnkConnecting.Visible = True



            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop

            If FlgError = False Then
                Me.lnkConnecting.Text = "Completed"
                Me.pbxConnectionServer.Image = My.Resources.ok
            Else
                Me.lnkConnecting.Text = "Error"
                Me.pbxConnectionServer.Image = My.Resources.Cross
                Exit Sub
            End If
            '------------------------------ Create DB -------------------------------------
            Me.lblCreateDb.Visible = True
            Me.lnkCreateDb.Visible = True

            If FlgError = False Then
                If BackgroundWorker2.IsBusy Then Exit Sub
                BackgroundWorker2.RunWorkerAsync()
                Do While BackgroundWorker2.IsBusy
                    Application.DoEvents()
                Loop
                Me.lnkCreateDb.Text = "Completed"
                Me.pbxCreateDatabase.Image = My.Resources.ok
            Else
                Me.lnkCreateDb.Text = "Error"
                Me.pbxCreateDatabase.Image = My.Resources.Cross
                Exit Sub
            End If
            '------------------------------- Restore DB -----------------------------------
            Me.lblRestoreDb.Visible = True
            Me.lnkRestoreDb.Visible = True

            If FlgError = False Then
                If BackgroundWorker3.IsBusy Then Exit Sub
                BackgroundWorker3.RunWorkerAsync()
                Do While BackgroundWorker3.IsBusy
                    Application.DoEvents()
                Loop
                Me.lnkRestoreDb.Text = "Completed"
                Me.pbxRestoreBlankDB.Image = My.Resources.ok
            Else
                Me.lnkRestoreDb.Text = "Error"
                Me.pbxRestoreBlankDB.Image = My.Resources.Cross
                Exit Sub
            End If

            If FlgError = False Then
                '-------------------------------------------------- Finalizing Setup ------------------------------
                Me.lblFanalizing.Visible = True
                Me.lnkFanalizing.Visible = True
                CompanyTitle = frmSetup._CompanyTitle
                CompanyAddress = frmSetup._CompanyAddress
                'CompanyPhone = frmSetup._CompanyPhone
                CompanyEmail = frmSetup._CompanyEmail
                dtSimplePOSConnection.TableName = "CompanyConnectionInfo"
                dtSimplePOSConnection.Columns.Add("Id", GetType(System.Int32))
                dtSimplePOSConnection.Columns.Add("Title", GetType(System.String))
                dtSimplePOSConnection.Columns.Add("ServerName", GetType(System.String))
                dtSimplePOSConnection.Columns.Add("UserId", GetType(System.String))
                dtSimplePOSConnection.Columns.Add("Password", GetType(System.String))
                dtSimplePOSConnection.Columns.Add("DBName", GetType(System.String))
                Dim dr As DataRow
                If IO.File.Exists(Application.StartupPath & "\CompanyConnectionInfo.Xml") = False Then
                    dr = dtSimplePOSConnection.NewRow
                    dr(0) = 1
                    dr(1) = CompanyTitle
                    dr(2) = Get_ConnectionString.DataSource
                    'If Not get_ConnectionString.UserID = "" Then
                    dr(3) = Get_ConnectionString.UserID
                    dr(4) = Encrypt(Get_ConnectionString.Password.ToString())
                    'Get_ConnectionString.IntegratedSecurity = False
                    'Else
                    '    Get_ConnectionString.IntegratedSecurity = True
                    '    Get_ConnectionString.PersistSecurityInfo = True
                    'End If
                    dr(5) = Get_ConnectionString.InitialCatalog
                    dtSimplePOSConnection.Rows.InsertAt(dr, 0)
                    dsSimplePOSConnection.Tables.Add(dtSimplePOSConnection)
                    dsSimplePOSConnection.WriteXml(Application.StartupPath & "\CompanyConnectionInfo.Xml")
                Else
                    dsSimplePOSConnection.ReadXml(Application.StartupPath & "\CompanyConnectionInfo.Xml")
                    Dim dr1 As DataRow
                    Dim dr2() As DataRow
                    dr2 = dsSimplePOSConnection.Tables(0).Select("ID=ISNULL(MAX(ID),0)", "")
                    dr1 = dsSimplePOSConnection.Tables(0).NewRow
                    dr1(0) = Convert.ToInt32(dr2(0).ItemArray(0)) + 1
                    dr1(1) = CompanyTitle
                    dr1(2) = Get_ConnectionString.DataSource
                    'If Not Get_ConnectionString.UserID = "" Then
                    dr1(3) = Get_ConnectionString.UserID
                    dr1(4) = Encrypt(Get_ConnectionString.Password.ToString())
                    'Get_ConnectionString.IntegratedSecurity = False
                    '    Else
                    '    Get_ConnectionString.IntegratedSecurity = True
                    '    Get_ConnectionString.PersistSecurityInfo = True
                    'End If
                    dr1(5) = Get_ConnectionString.InitialCatalog
                    dsSimplePOSConnection.Tables(0).Rows.InsertAt(dr1, 0)
                    dsSimplePOSConnection.WriteXml(Application.StartupPath & "\CompanyConnectionInfo.Xml")
                End If

                'Dim ConStr1 As String = "Data Source=.\SQLExpress;Database=" & strDB & ";Integrated Security Info=SSPI"
                con1 = New SqlConnection(Get_ConnectionString.ConnectionString)
                If con1.State = ConnectionState.Closed Then con1.Open()

                cmd = New SqlCommand("IF NOT EXISTS(Select * From ConfigValuesTable WHERE Config_Type='CompanyNameHeader') INSERT INTO ConfigValuesTable(Config_Type, Config_Value, Comments, IsActive) VALUES('CompanyNameHeader', '" & CompanyTitle & "','',1) ELSE Update ConfigValuesTable Set Config_Value='" & CompanyTitle & "' WHERE Config_Type='CompanyNameHeader'", con1)
                cmd.ExecuteNonQuery()

                cmd = New SqlCommand("IF NOT EXISTS(Select * From ConfigValuesTable WHERE Config_Type='CompanyAddressHeader') INSERT INTO ConfigValuesTable(Config_Type, Config_Value, Comments, IsActive) VALUES('CompanyAddressHeader', '" & CompanyAddress & "','',1) ELSE Update ConfigValuesTable Set Config_Value='" & CompanyAddress & "' WHERE Config_Type='CompanyAddressHeader'", con1)
                cmd.ExecuteNonQuery()

                cmd = New SqlCommand("IF NOT EXISTS(Select * From ConfigValuesTable WHERE Config_Type='AdminEmailId') INSERT INTO ConfigValuesTable(Config_Type, Config_Value, Comments, IsActive) VALUES('AdminEmailId', '" & CompanyEmail & "','',1) ELSE Update ConfigValuesTable Set Config_Value='" & CompanyEmail & "' WHERE Config_Type='AdminEmailId'", con1)
                cmd.ExecuteNonQuery()
                Me.lnkFanalizing.Text = "Completed"
                pbxFinalazingSetup.Image = My.Resources.ok
            Else
                Me.lnkFanalizing.Text = "Error"
                pbxFinalazingSetup.Image = My.Resources.Cross
                Exit Sub
            End If

            'If Me.lnkFanalizing.Text = "Completed" Then
            If FlgError = True Then
                Me.Button2.Visible = True
            Else
                Me.Button2.Visible = False
            End If
            Me.Button1.Visible = True
            Me.ProgressBar1.Visible = False
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            con1.Close()
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Me.Close()
            frmSetup.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            'Me.Hide()
            frmSetup.UltraTabControl1.SelectedTab = frmSetup.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class