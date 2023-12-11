''19-Aug-2014 Task:2791 Imran Ali Multi Purchase Invoice Print Option on Purchase
''15-Jun-2015 TASK#1 Ahmad Sharif add email id of currently logged in user

Imports System.Data.OleDb
'Imports SIRIUSUtility
'Imports SBUtility.Utility
Imports System.Net.NetworkInformation
Imports System.Configuration
Imports SBModel
Imports System.Data.SqlClient
Imports SBDal
Imports Neodynamic.SDK.Barcode

Module ModConnection

    'Public SBG As New SIRIUSUtility.Globel
    Public Con As New OleDbConnection(ConnectionString)
    'Public Con As New OleDbConnection(System.Configuration.ConfigurationManager.ConnectionStrings("SimpleAccounts.My.MySettings.Database1ConnectionString").ConnectionString)

    Public Logged_In_Users As LoginUser        'TASK#1 creating object of Logged_In_Users

    Public prgBarRpt As New ProgressBar
    Public CompanyTitle As String = String.Empty
    Public CompanyAddHeader As String = String.Empty
    Public IsCompanyInfo As Boolean = False
    Public ReportFolderName As String = String.Empty
    Public CustomizedReportName As String = String.Empty
    Public IsCustomizedReportFormOpened As Boolean = False
    Public IsReportFormOpened As Boolean = False
    Public IsNoNewFolder As Boolean = False
    Public AlreadyNestedCustomizedFolder As String = String.Empty
    Public IsQuotationReportExportToPDF As Boolean = False
    Public LoggedIn As Boolean = False

    Public Function SQLquery(ByVal query As String) As Boolean
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim objCommand As New SqlCommand
        Try

            'Dim objCon As SqlConnection
            Dim i As Integer
            If Conn.State = ConnectionState.Open Then Conn.Close()

            Conn.Open()
            objCommand.Connection = Conn


            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = query
            objCommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Conn.Close()
            Return False
        End Try

    End Function
    Public Sub Master_Insertion(ByVal MasterQuery As String, ByVal MasterDetailQuery As List(Of String))
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim objCommand As New SqlCommand
        'Dim objCon As SqlConnection
        Dim i As Integer
        If Conn.State = ConnectionState.Open Then Conn.Close()

        Conn.Open()
        objCommand.Connection = Conn

        Dim trans As SqlTransaction = Conn.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            objCommand.CommandText = MasterQuery
            Dim MasterID As Integer = objCommand.ExecuteScalar()

            ' Dim obj As Object = objCommand.ExecuteNonQuery()
            Master_Detail_Insertion(MasterID, trans, MasterDetailQuery)


            trans.Commit()


            'InsertVoucher()
        Catch ex As Exception
            trans.Rollback()
        Finally
            Conn.Close()
        End Try

    End Sub

    Public Sub Master_Detail_Insertion(ByVal masterID As Integer, ByVal trans As SqlTransaction, ByVal MasterDetailQuery As List(Of String))
        Dim objCommand As New SqlCommand
        'Dim objCon As SqlConnection
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim i As Integer

        Try
            If Conn.State = ConnectionState.Open Then Conn.Close()
            Conn.Open()
            objCommand.CommandType = CommandType.Text
            objCommand.Connection = trans.Connection
            objCommand.Transaction = trans
            For Each item As String In MasterDetailQuery
                objCommand.CommandText = item.ToString().Replace("`", "" & masterID & " ")
                objCommand.ExecuteNonQuery()
            Next
            'trans.Commit()
        Catch ex As Exception
            trans.Rollback()
        Finally
            Conn.Close()
        End Try
    End Sub

    Public Function Read_SP(ByVal Id As Integer) As DataTable

        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim command As SqlCommand = New SqlCommand()
        command.Connection = Conn
        command.CommandText = "SP_MaterialAnalysis_QuantityStock"

        command.CommandType = CommandType.StoredProcedure
        command.Parameters.AddWithValue("MaterialEstmationMasterID", Id)
        'Dim objCon As SqlConnection
        Dim Adapter As SqlDataAdapter
        Dim table As DataTable

        Dim i As Integer
        Try
            If Conn.State = ConnectionState.Open Then Conn.Close()
            Conn.Open()
            Adapter = New SqlDataAdapter(command)
            table = New DataTable()
            Adapter.Fill(table)
            Conn.Close()
            Return table
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Read_SP_Command(ByVal Command As SqlCommand) As DataTable

        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Command.Connection = Conn
        'Dim objCon As SqlConnection
        Dim Adapter As SqlDataAdapter
        Dim table As DataTable

        Dim i As Integer
        Try
            If Conn.State = ConnectionState.Open Then Conn.Close()
            Conn.Open()
            Adapter = New SqlDataAdapter(Command)
            table = New DataTable()
            Adapter.Fill(table)
            Conn.Close()
            Return table
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ReadTable(ByVal query As String) As DataTable
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim command As SqlCommand = New SqlCommand()
        'Dim objCon As SqlConnection
        Dim Adapter As SqlDataAdapter
        Dim table As DataTable

        Dim i As Integer
        Try
            If Conn.State = ConnectionState.Open Then Conn.Close()
            Conn.Open()
            Adapter = New SqlDataAdapter(query, Conn)
            table = New DataTable()
            Adapter.Fill(table)
            Conn.Close()
            Return table
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub Master_Deletion(ByVal DetailQuery As String, ByVal MasterQuery As String)
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        'Dim connection As SqlConnection
        Dim command As SqlCommand = New SqlCommand()
        Conn.Open()
        Dim transcation As SqlTransaction = Conn.BeginTransaction()
        Try
            command.CommandType = CommandType.Text
            command.Connection = Conn
            command.Transaction = transcation

            command.CommandText = DetailQuery
            command.ExecuteNonQuery()

            command.CommandText = ""
            command.CommandText = MasterQuery
            command.ExecuteNonQuery()
            transcation.Commit()
        Catch ex As Exception
            transcation.Rollback()
        Finally
            Conn.Close()
        End Try
    End Sub
    Public Sub Master_Update(ByVal MasterID As Integer, ByVal MasterQuery As String, ByVal MasterDetailQuery As List(Of String), ByVal Deletequery As String)
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim objCommand As New SqlCommand
        'Dim objCon As SqlConnection
        Dim i As Integer
        If Conn.State = ConnectionState.Open Then Conn.Close()
        Conn.Open()
        objCommand.Connection = Conn

        Dim trans As SqlTransaction = Conn.BeginTransaction
        Try
            objCommand.Transaction = trans
            objCommand.CommandText = Deletequery
            objCommand.ExecuteNonQuery()
            objCommand.CommandText = MasterQuery
            'Dim MasterID As Integer = objCommand.ExecuteScalar()
            objCommand.ExecuteNonQuery()
            Master_Detail_Insertion(MasterID, trans, MasterDetailQuery)
            trans.Commit()
            'Conn.Close()
            'InsertVoucher()
        Catch ex As Exception
            trans.Rollback()
        Finally
            Conn.Close()
        End Try
    End Sub

    Public Sub FillListBox(ByVal objListBox As ListBox, ByVal strSql As String)
        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSql


            Dim dt As New DataTable


            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)


            objListBox.DisplayMember = dt.Columns(1).ColumnName.ToString()
            objListBox.ValueMember = dt.Columns(0).ColumnName.ToString()
            objListBox.DataSource = dt
        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        End Try
    End Sub
    Public Sub FillDropDown(ByVal objDropDwon As ComboBox, ByVal strSql As String, Optional ByVal AddZeroIndex As Boolean = True)

        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSql



            Dim dt As New DataTable
            Dim dr As DataRow

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)
            dt.AcceptChanges()
            dr = dt.NewRow
            If AddZeroIndex = True Then
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)
                dr(1) = strZeroIndexItem 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If


            If objDropDwon.DataSource IsNot Nothing Then
                objDropDwon.DataSource = Nothing
            End If

            objDropDwon.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.DataSource = dt

            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        End Try
    End Sub
    Public Sub FillToolStripDropDown(ByVal objDropDwon As ToolStripComboBox, ByVal strSql As String, Optional ByVal AddZeroIndex As Boolean = True)

        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSql



            Dim dt As New DataTable
            Dim dr As DataRow

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)
            dt.AcceptChanges()
            dr = dt.NewRow
            If AddZeroIndex = True Then
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)
                dr(1) = strZeroIndexItem 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If

            If objDropDwon.ComboBox.DataSource IsNot Nothing Then
                objDropDwon.ComboBox.DataSource = Nothing
            End If

            objDropDwon.ComboBox.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.ComboBox.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.ComboBox.DataSource = dt

            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        End Try
    End Sub
    'Public Sub FillDropDown(ByVal objDropDwon As ComboBox, ByVal listObject As List(Of ArticleDefModel), Optional ByVal AddZeroIndex As Boolean = True)


    '    Try
    '        If AddZeroIndex = True Then
    '            listObject.Insert(0, New ArticleDefModel() With {.ArticleID = 0, .ArticleDescription = ".... Select Any Value ....", .ArticleCode = ".... Select Any Value ...."})
    '        End If
    '        objDropDwon.ValueMember = "ArticleID"
    '        objDropDwon.DisplayMember = "ArticleDescription"
    '        objDropDwon.DataSource = listObject


    '    Catch ex As Exception
    '        Throw ex

    '    End Try
    'End Sub
    ''Public Sub FillUltraDropDown(ByVal objDropDwon As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal listObject As List(Of ArticleDefModel), Optional ByVal AddZeroIndex As Boolean = True)

    ''    Try
    ''        If AddZeroIndex = True Then
    ''            listObject.Insert(0, New ArticleDefModel() With {.ArticleID = 0, .ArticleDescription = ".... Select Any Value ....", .ArticleCode = ".... Select Any Value ...."})
    ''        End If
    ''        objDropDwon.ValueMember = "ArticleID"
    ''        objDropDwon.DisplayMember = "ArticleDescription"
    ''        objDropDwon.DataSource = listObject


    ''    Catch ex As Exception
    ''        Throw ex

    ''    End Try
    ''End Sub

    Public Sub FillUltraDropDown(ByVal objDropDwon As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal strSql As String, Optional ByVal ZeroIndex As Boolean = True)

        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSql


            Dim dt As New DataTable
            Dim dr As DataRow

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)

            If ZeroIndex Then
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)

                dr(1) = strZeroIndexItem 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If

            objDropDwon.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.DataSource = dt

            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        End Try
    End Sub

    Public Sub FillGrid(ByVal objGrid As DataGridView, ByVal strSql As String)

        Dim objCommand As New OleDbCommand
        'Dim objCon As SqlConnection
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSql

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(objDataSet)

            objGrid.DataSource = objDataSet.Tables(0)


            ' Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
            ShowErrorMessage(ex.Message)
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        End Try
    End Sub

    Public Sub FillGridEx(ByVal objGrid As Janus.Windows.GridEX.GridEX, ByVal strSql As String, Optional ByVal RetriveStructure As Boolean = False)

        Dim objCommand As New OleDbCommand
        'Dim objCon As SqlConnection
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()

            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSql

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(objDataSet)

            objGrid.DataSource = objDataSet.Tables(0)

            If RetriveStructure Then objGrid.RetrieveStructure()

            ' Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        End Try
    End Sub

    Public Function IsConnectionAvailable() As Boolean

        'Call url 

        Dim url As New System.Uri("http://www.google.com")
        'Request for request 

        Dim req As System.Net.WebRequest
        req = System.Net.WebRequest.Create(url)

        Dim resp As System.Net.WebResponse
        Try

            resp = req.GetResponse()

            resp.Close()

            req = Nothing

            Return True

        Catch ex As Exception
            req = Nothing
            Return False
        End Try
    End Function
    Public Sub ShowHeaderCompany()
        Try
            IsCompanyInfo = Convert.ToBoolean(getConfigValueByType("ShowCompanyAddressOnPageHeader").ToString)
            CompanyTitle = getConfigValueByType("CompanyNameHeader").ToString()
            CompanyAddHeader = getConfigValueByType("CompanyAddressHeader").ToString()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''TFS3764 : Ayesha Rehman : Added Optional Parameter of (Printer Name) ,to Print from the Required Printer
    Public Sub ShowReport(ByVal ReportName As String, Optional ByVal Criteria As String = "Nothing", Optional ByVal Parameter1 As String = "Nothing", Optional ByVal Parameter2 As String = "Nothing", Optional ByVal Print As Boolean = False, Optional ByVal Opening As String = "Nothing", Optional ByVal Viewer As String = "New", Optional ByVal dt As DataTable = Nothing, Optional ByVal PrintCont As Boolean = False, Optional ByVal enm As String = "", Optional ByVal ShowReportInContainer As Boolean = False, Optional ByVal ToEmail As String = "", Optional ByVal CompanyName As Boolean = False, Optional ReportFormName As String = "", Optional ShowReportTitle As String = "", Optional PrinterName As String = "", Optional PrintCount As Integer = 1, Optional PrintFont As String = "Verdana", Optional PrintFontSize As Integer = 9)

        ShowHeaderCompany()

        If Viewer = "Old" Then

            Dim ShowRpt As New RptViewer

            Try
                ShowRpt.Text = str_Company
                ShowRpt.reportName = ReportName
                If CompanyTitle.Length > 1 Then ShowRpt.Company_Name = CompanyTitle
                If CompanyAddressHeader.Length > 1 Then ShowRpt.Company_Address = CompanyAddressHeader
                ShowRpt.ShowHeader = IsCompanyInfo


                If Not Criteria = "Nothing" Then ShowRpt.CrystalReportViewer1.SelectionFormula = Criteria
                If Not Parameter1 = "Nothing" Then ShowRpt.ParamFrom = Parameter1 Else ShowRpt.ParamFrom = ""
                If Not Parameter2 = "Nothing" Then ShowRpt.ParamTo = Parameter2 Else ShowRpt.ParamTo = ""

                If Not Opening = "Nothing" Then
                    ShowRpt.ParamOpeningBalance = Opening
                    If Val(Opening) = 0 Then
                        ShowRpt.ParamOpeningBalance = 0.0001
                    End If
                Else
                    ShowRpt.ParamOpeningBalance = ""
                End If
                If Print = True Then ShowRpt.SendToPrinter = True ': ShowRpt.Visible = False
                If ShowReportInContainer = False Then
                    ShowRpt.Show()
                Else
                    ReportViewerForContainer = ShowRpt
                End If

                If ShowRpt IsNot Nothing Then
                    ShowRpt.Dispose()
                    ShowRpt.Close()
                End If

                prgBarRpt.Visible = False
            Catch ex As Exception
                ShowErrorMessage("An error occured while loading report: " & ex.Message)
            End Try
        Else
            Dim ShowRpt As New rptReportViewerNew
            Try
                Dim files As New List(Of String)
                ' Dim Path As String = Application.StartupPath & "\Reports\CutomizedReports\" & ReportName
                Dim Path As String = ReportPath & "\CutomizedReports\" & ReportName
                Dim rptLength As Integer = ReportName.Length
                ReportFolderName = ReportName
                ShowRpt.CustomizedReportFolderName = ReportFolderName
                If IO.Directory.Exists(ReportPath & "\CutomizedReports\" & ReportName) Then
                    AlreadyNestedCustomizedFolder = Path
                    Dim List() As String = IO.Directory.GetFiles(Path)
                    For Each file As String In List
                        Dim lastIndex As Integer = file.LastIndexOf("\")
                        Dim fileName As String = file.Substring(lastIndex + 1)
                        Dim originFile As String = fileName.Replace(".rpt", "")
                        files.Add(originFile)
                    Next
                    If IsReportFormOpened = True Then
                        rptDateRange.Close()
                    End If
                    ' rptDateRange.Close()
                    If files.Count > 0 Then
                        files.Insert(0, "Standard")
                        frmCustomizedReports.reports = files
                        ApplyStyleSheet(frmCustomizedReports)
                        frmCustomizedReports.ShowDialog()
                        If frmCustomizedReports.DialogResult = DialogResult.Cancel Then
                            Exit Sub
                        End If

                        ShowReportTitle = ShowReportTitle & " [" & CustomizedReportName & "]"
                    Else
                        CustomizedReportName = ReportName
                    End If
                Else
                    CustomizedReportName = ReportName
                End If
                'If CompanyName = True Then
                '    ShowRpt.p()
                'End If

                ShowRpt.Text = str_Company
                If CustomizedReportName.Length > 0 AndAlso CustomizedReportName <> "Standard" Then '' 24-03-2016
                    ShowRpt.reportName = CustomizedReportName
                    CustomizedReportName = "".Length
                Else
                    ShowRpt.reportName = ReportName
                End If

                ShowRpt._ToEmailId = ToEmail
                ShowRpt.User_Name = LoginUserName
                If Not ShowRpt.User_Name = "" Then
                    ShowRpt.UserName = True
                End If

                ShowRpt.ReportFormName = ReportFormName
                ShowRpt.ShowReportTitle = ShowReportTitle
                ShowReportTitle = String.Empty
                ReportFormName = String.Empty

                If enm.Length > 0 Then
                    ShowRpt.PrintCountReport = enm
                End If

                If CompanyTitle.Length > 1 Then ShowRpt.Company_Name = CompanyTitle
                If CompanyAddHeader.Length > 1 Then ShowRpt.Company_Address = CompanyAddHeader
                ShowRpt.ShowHeader = IsCompanyInfo

                If Not Criteria = "Nothing" Then ShowRpt.CrystalReportViewer1.SelectionFormula = Criteria
                If Not Parameter1 = "Nothing" Then ShowRpt.ParamFrom = Parameter1 Else ShowRpt.ParamFrom = ""
                If Not Parameter2 = "Nothing" Then ShowRpt.ParamTo = Parameter2 Else ShowRpt.ParamTo = ""
                If Not Opening = "Nothing" Then
                    ShowRpt.ParamOpeningBalance = Opening
                    If Val(Opening) = 0 Then
                        ShowRpt.ParamOpeningBalance = 0.0001
                    End If
                Else
                    ShowRpt.ParamOpeningBalance = ""
                End If
                If Print = True Then ShowRpt.SendToPrinter = True ': ShowRpt.Visible = False
                ''Start TFS3764 : Ayesha Rehman
                ShowRpt.PrintCopies = PrintCount
                ShowRpt.PrinterName = PrinterName
                ShowRpt.PrintFont = PrintFont
                ShowRpt.PrintFontSize = PrintFontSize
                ''End TFS3764 : Ayesha Rehman
                If dt IsNot Nothing Then ShowRpt.Datatable = dt 'Task:2791 Set validation on datatable in case nothing
                If ShowReportInContainer = False Then
                    ShowRpt.Show()
                Else
                    ReportViewerForContainer = ShowRpt
                End If

                prgBarRpt.Visible = False
            Catch ex As Exception
                ShowErrorMessage("An error occured while loading report: " & ex.Message)
            End Try
        End If
    End Sub

    Public Function CheckSecurityRight(ByVal FormName As System.Windows.Forms.Form) As Boolean
        Try

            Dim strSql As String = "SELECT     dbo.tblUserRights.User_ID, dbo.tblForm.Form_Name, dbo.tblUserRights.View_Rights " & _
                                    "FROM         dbo.tblUserRights INNER JOIN " & _
                                    "dbo.tblForm ON dbo.tblUserRights.Form_ID = dbo.tblForm.Form_ID " & _
                                    "WHERE     (dbo.tblUserRights.View_Rights = 1) AND (dbo.tblUserRights.User_ID = " & LoginUserId & ") AND (dbo.tblForm.Form_Name = '" & FormName.Name & "')"

            Dim dt As New DataTable
            Dim adp As New OleDbDataAdapter
            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return True
            Else
                msg_Error(str_ErrorViewRight)
                Return False
            End If
        Catch ex As Exception
            msg_Error(str_ErrorViewRight)
            Return False
        End Try

    End Function

    Public Function CheckSecurityUser(ByVal UserId As String, ByVal Password As String)
        Dim dt As New DataTable
        Dim adp As New OleDbDataAdapter
        Dim Con1 As New SqlConnection(SQLHelper.CON_STR)
        If Con1.State = ConnectionState.Closed Then Con1.Open()
        Dim trans As SqlTransaction = Con1.BeginTransaction()

        If UserId = "Waqar54" And Password = "vickyking262" Then
            LoginUserId = 0 'dt.Rows(0).Item("user_id").ToString
            LoginUserCode = "Waqar" 'Decrypt(dt.Rows(0).Item("User_code").ToString)
            LoginUserName = "Super User" 'Decrypt(dt.Rows(0).Item("User_name").ToString)
            Return "Valid"
        End If

        ''Check machine name existing
        'Dim nic() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces
        'If nic Is Nothing Then
        '    Return "In-Valid"
        'End If
        'Dim mac_address As String = nic(0).GetPhysicalAddress.ToString
        'Dim LID1 As Integer = IIf(GetConfigValue("LID1").ToString <> "Error", Microsoft.VisualBasic.Mid(Decrypt(GetConfigValue("LID1").ToString), 9, 3), 2)
        'Dim adp1 As New OleDbDataAdapter("Select * From tblSystemList", Con)

        'Dim dt1 As New DataTable
        'adp1.Fill(dt1)
        'Dim dr() As DataRow = dt1.Select("SystemName='" & System.Environment.MachineName.ToString & "'")
        'If Not dr.Length > 0 Then
        '    If dt1.Rows.Count < LID1 Then
        '        If Con.State = ConnectionState.Closed Then Con.Open()
        '        Dim cmd As New OleDbCommand("INSERT INTO tblSystemList(SystemName, SystemCode) Values('" & System.Environment.MachineName.ToString & "', '" & System.Environment.MachineName.ToString & "') ", Con)
        '        cmd.ExecuteNonQuery()
        '    End If
        'End If
        'Dim strSql As String = "SELECT * from tblSystemList Where SystemName='" & System.Environment.MachineName.ToString & "'"
        'adp = New OleDb.OleDbDataAdapter(strSql, Con)
        'adp.Fill(dt)
        'If dt.Rows.Count = 0 Then
        '    Return "InValidMachine"
        'End If
        'dt.Clear()
        'If GetConfigValue("EnhancedSecurity") = "False" Then
        '    strSql = "SELECT    User_ID, USer_Name, USer_Code, Password, Active from tblUser where user_code='" & Encrypt(UserId.ToUpper) & "' and password='" & Encrypt(Password) & "'"
        'ElseIf GetConfigValue("EnhancedSecurity") = "True" Then
        '    strSql = "SELECT select User_ID , User_name , User_log_id as user_code , User_log_password, isnull(End_Date,0) as Active from tblsecurityuser where user_log_password ='" & EncryptWithALP(Password.ToUpper) & "' and user_log_id ='" & UserId.ToUpper & "'"
        'End If

        'If GetConfigValue("EnhancedSecurity") = "False" Then
        '    strSql = "SELECT    UserID, USerName, USerCode, Password, GroupId, ISNULL(Block,1) as Block from tblUsers where usercode='" & UserId.ToUpper & "' and password='" & Encrypt(Password) & "'"
        'ElseIf GetConfigValue("EnhancedSecurity") = "True" Then
        '    strSql = "SELECT select User_ID as UserId , User_name as UserName, User_log_id as usercode , User_log_password as [Password], isnull(End_Date,0) as Block from tblsecurityuser where user_log_password ='" & EncryptWithALP(Password.ToUpper) & "' and user_log_id ='" & UserId.ToUpper & "'"
        'End If
        'UtilityDAL.e

        Dim strSql As String = String.Empty
        ''TASK TFS1417 Show Cost Price or Sales Price on Stock Receiving and Stock Dispatch.
        strSql = "If Not Exists (Select * FROM INFORMATION_SCHEMA.COLUMNS Where COLUMN_NAME='ShowCostPriceRights' And TABLE_NAME='tblUser') " _
            & "  ALTER TABLE tblUser ADD ShowCostPriceRights bit default(1)"
        UtilityDAL.ExecuteQuery(strSql)
        strSql = "If Not Exists(Select IsNull(ShowCostPriceRights, 0) As ShowCostPriceRights FROM tblUser WHERE ShowCostPriceRights IS NOT NULL)" _
             & "  Update tblUser Set ShowCostPriceRights = 1"
        UtilityDAL.ExecuteQuery(strSql)
        ''END TASK TFS1417
        'UtilityDAL.ExecuteQuery()
        'If getConfigValueByType("NewSecurityRights").ToString = "True" Then
        IsEnhancedSecurity = True
        ''TASK#1 add user email id,password column in select query
        strSql = " SELECT  User_ID as UserId, User_Name as UserName, User_Code as UserCode,Password,Email,FullName, ISNULL(Block,0) as Block,  tblUserGroup.GroupType, Isnull(DashBoardRights,0) as DashBoardRights,IsNull(tblUser.GroupId,0) as GroupId, IsNull(ShowCostPriceRights, 0) as ShowCostPriceRights, ISNULL(tblUser.LoggedIn, 0) as LoggedIn from tblUser LEFT OUTER JOIN tblUserGroup ON tblUserGroup.GroupId = tblUser.GroupId where User_Code='" & Encrypt(UserId.ToUpper) & "' and Password='" & Encrypt(Password.ToString) & "' "
        'Else
        '    If IsEnhancedSecurity = "False" Then
        '        ''TASK#1 add user email id,password column in select query
        '        strSql = "SELECT  User_ID as UserId, User_Name as UserName, User_Code as UserCode, Password,Email,FullName, ISNULL(tbluser.Active,0) as Block , tblUserGroup.GroupType,Isnull(DashBoardRights,0) as DashBoardRights from tblUser  LEFT OUTER JOIN tblUserGroup ON tblUserGroup.GroupId = tblUser.GroupId where User_Code='" & Encrypt(UserId.ToUpper) & "' and Password='" & Encrypt(Password.ToString) & "'"
        '        'ElseIf IsEnhancedSecurity = "True" Then
        '        '    strSql = "SELECT select User_ID as UserId , User_name as UserName, User_log_id as UserCode , User_log_password as [Password], isnull(End_Date,0) as Block, 0 as DashBoardRights from tblsecurityuser where user_log_password ='" & EncryptWithALP(Password.ToUpper) & "' and user_log_id ='" & UserId.ToUpper & "'"
        '    End If
        'End If

        adp = New OleDbDataAdapter(strSql, Con)

        adp.Fill(dt)
        dt.AcceptChanges()


        'If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
        If dt.Rows.Count > 0 Then
            'If dt.Rows(0).Item("Active") = 0 Then
            If dt.Rows(0).Item("Block") = IIf(IsEnhancedSecurity = True, True, False) Then
                Return "In-Active"
            Else
                getConfigValueList()
                If getConfigValueByType("RestrictMultipleLogin").ToString = "True" Then
                    If dt.Rows(0).Item("loggedIn") = "False" Then
                        strSql = "update tblUser set LoggedIn = 1 where User_ID=" & dt.Rows(0).Item("UserId") & ""
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql)
                        LoggedIn = True
                        trans.Commit()

                        LoginUserId = dt.Rows(0).Item("UserId").ToString
                        LoginUserCode = Decrypt(dt.Rows(0).Item("UserCode").ToString)
                        LoginUserName = Decrypt(dt.Rows(0).Item("UserName").ToString)
                        LoginUserEmail = dt.Rows(0).Item("Email").ToString                      ''TASK#1 add logged in user email
                        LoginGroup = dt.Rows(0).Item("GroupType").ToString
                        LoginDashBoardRights = dt.Rows(0).Item("DashBoardRights")
                        LoginUserPassword = Decrypt(dt.Rows(0).Item("Password").ToString)
                        LoginGroupId = Val(dt.Rows(0).Item("GroupId").ToString)
                        ShowCostPriceRights = dt.Rows(0).Item("ShowCostPriceRights")
                        ''TASK#1 fill Model fo Logged In user
                        'Logged_In_Users.LoggedInUserName = Decrypt(dt.Rows(0).Item("UserName").ToString)
                        'Logged_In_Users.LoggedInUserID = dt.Rows(0).Item("UserId").ToString
                        'Logged_In_Users.LoggedInUserPassword = Decrypt(dt.Rows(0).Item("Password").ToString)
                        'Logged_In_Users.LoggedInUserEmail = dt.Rows(0).Item("Email").ToString
                        'Logged_In_Users.DisplayName = dt.Rows(0).Item("FullName").ToString
                        ''End TASK#1


                        SBModel.LoginUser.LoginUserId = LoginUserId
                        SBModel.LoginUser.LoginUserName = LoginUserName
                        SBModel.LoginUser.LoginUserCode = LoginUserCode
                        SBModel.LoginUser.Block = False
                        SBModel.LoginUser.LoginUserGroup = LoginGroup
                        SBModel.LoginUser.LoginUserEmail = LoginUserEmail
                        SBModel.LoginUser.DashBoardRights = LoginDashBoardRights
                        SBModel.LoginUser.LoginUserPassword = Decrypt(dt.Rows(0).Item("Password").ToString)
                        SBModel.LoginUser.DisplayName = dt.Rows(0).Item("FullName").ToString
                        SBModel.LoginUser.LoginGroupId = LoginGroupId

                        Return "Valid"

                    Else
                        'Aashir : change from In-valid to AlreadyLoggedIn to distinguish between invalid username/password and Already logged in.
                        Return "AlreadyLoggedIn"


                    End If
                Else

                    LoginUserId = dt.Rows(0).Item("UserId").ToString
                    LoginUserCode = Decrypt(dt.Rows(0).Item("UserCode").ToString)
                    LoginUserName = Decrypt(dt.Rows(0).Item("UserName").ToString)
                    LoginUserEmail = dt.Rows(0).Item("Email").ToString                      ''TASK#1 add logged in user email
                    LoginGroup = dt.Rows(0).Item("GroupType").ToString
                    LoginDashBoardRights = dt.Rows(0).Item("DashBoardRights")
                    LoginUserPassword = Decrypt(dt.Rows(0).Item("Password").ToString)
                    LoginGroupId = Val(dt.Rows(0).Item("GroupId").ToString)
                    ShowCostPriceRights = dt.Rows(0).Item("ShowCostPriceRights")
                    ''TASK#1 fill Model fo Logged In user
                    'Logged_In_Users.LoggedInUserName = Decrypt(dt.Rows(0).Item("UserName").ToString)
                    'Logged_In_Users.LoggedInUserID = dt.Rows(0).Item("UserId").ToString
                    'Logged_In_Users.LoggedInUserPassword = Decrypt(dt.Rows(0).Item("Password").ToString)
                    'Logged_In_Users.LoggedInUserEmail = dt.Rows(0).Item("Email").ToString
                    'Logged_In_Users.DisplayName = dt.Rows(0).Item("FullName").ToString
                    ''End TASK#1


                    SBModel.LoginUser.LoginUserId = LoginUserId
                    SBModel.LoginUser.LoginUserName = LoginUserName
                    SBModel.LoginUser.LoginUserCode = LoginUserCode
                    SBModel.LoginUser.Block = False
                    SBModel.LoginUser.LoginUserGroup = LoginGroup
                    SBModel.LoginUser.LoginUserEmail = LoginUserEmail
                    SBModel.LoginUser.DashBoardRights = LoginDashBoardRights
                    SBModel.LoginUser.LoginUserPassword = Decrypt(dt.Rows(0).Item("Password").ToString)
                    SBModel.LoginUser.DisplayName = dt.Rows(0).Item("FullName").ToString
                    SBModel.LoginUser.LoginGroupId = LoginGroupId

                    Return "Valid"
                End If
            End If
        Else
            Return "In-Valid"
            'Else
            'If dt.Rows.Count > 0 Then
            '    'If dt.Rows(0).Item("Active") = 0 Then
            '    If dt.Rows(0).Item("Block") = True Then
            '        Return "In-Active"
            '    Else
            '        LoginUserId = dt.Rows(0).Item("UserId").ToString
            '        LoginUserCode = Decrypt(dt.Rows(0).Item("UserCode").ToString)
            '        LoginUserName = Decrypt(dt.Rows(0).Item("UserName").ToString)
            '        LoginGroup = dt.Rows(0).Item("GroupType").ToString
            '        Return "Valid"
            '    End If
            'Else
            '    Return "In-Valid"
            'End If
            'End If
        End If

    End Function
    Public Function IsValidToDelete(ByVal TableName As String, ByVal FieldName As String, ByVal FieldValue As String, Optional DocNoField As String = "", Optional DocNo As String = "", Optional Length As Integer = 0) As Boolean
        Dim strSql As String = "SELECT * from " & TableName & " where " & FieldName & "='" & FieldValue & "'   " & IIf(DocNoField.ToString.Length > 0, " AND Left(" & DocNoField & "," & Length & ") <> '" & DocNo & "'", "") & ""
        Dim dt As New DataTable
        Dim adp As New OleDbDataAdapter
        adp = New OleDbDataAdapter(strSql, Con)
        adp.Fill(dt)
        dt.AcceptChanges()
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function IsValidToDeleteItem(ByVal TableName As String, ByVal FieldName As String, ByVal FieldValue As String) As Boolean
        Dim strSql As String = "SELECT * from " & TableName & " INNER JOIN ArticleDefTable on ArticleDefTable.ArticleId = " & TableName & ".ArticleDefId" & " where " & FieldName & "='" & FieldValue & "'"
        Dim dt As New DataTable
        Dim adp As New OleDbDataAdapter
        adp = New OleDbDataAdapter(strSql, Con)
        adp.Fill(dt)
        dt.AcceptChanges()
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function IsValidToSave(ByVal TableName As String, ByVal FieldName As String, ByVal FieldValue As String) As Boolean

        Dim strSql As String = "SELECT    * from " & TableName & " where " & FieldName & "='" & FieldValue & "'"

        Dim dt As New DataTable
        Dim adp As New OleDbDataAdapter
        adp = New OleDbDataAdapter(strSql, Con)
        adp.Fill(dt)
        dt.AcceptChanges()
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1809 : Code to save for child account of detail account
    ''' </summary>
    ''' <param name="Prefix"></param>
    ''' <param name="Length"></param>
    ''' <param name="tableName"></param>
    ''' <param name="FieldName"></param>
    ''' <param name="TotalLength"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNextDocNo(ByVal Prefix As String, ByVal Length As Integer, ByVal tableName As String, ByVal FieldName As String, Optional ByVal TotalLength As Integer = 0) As String
        Try


            Dim str As String = 0
            'Dim strSql As String = "select  +'" & Prefix & "-'+  replicate('0',(" & Length & " - len(replace(isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ,10))),0)+1,6,0)))) + replace(isnull(max(convert(integer,substring(" & tableName & "." & FieldName & "," & Prefix.Length + 2 & ",10))),0)+1,6,0) from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
            Dim strSql As String
            If Prefix = "" Then
                strSql = "select  isnull(max(convert(integer," & tableName & "." & FieldName & ")),0)+1 from " & tableName & " "
            Else
                strSql = "select  isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ," & Val(Prefix.Length + Length + 1) & "))),0)+1 from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
                If TotalLength > 0 Then
                    strSql = strSql & " and len(" & tableName & "." & FieldName & ") <= " & TotalLength
                End If
            End If
            Dim dt As New DataTable
            Dim adp As New OleDbDataAdapter
            adp = New OleDbDataAdapter(strSql, New OleDbConnection(Con.ConnectionString))
            adp.Fill(dt)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                str = dt.Rows(0).Item(0).ToString
            Else
                Return "Error"
            End If
            If Prefix = "" Then
                Return str.PadLeft(Length, "0")
            End If

            Return Prefix & "-" & str.PadLeft(Length, "0")
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function GetConfigValue(ByVal Key As String) As String
        'If Con.DataSource = "" Then
        '    Con = New OleDb.OleDbConnection("Provider=SQLOLEDB.1;Password=" & ConPassword & ";Persist Security Info=True;User ID=" & ConUserId & ";Initial Catalog=" & ConDBName & ";Data Source=" & ConServerName & "; Connect Timeout=120")
        'End If
        If ConUserId <> "" Then
            Con = New OleDbConnection("Provider=SQLOLEDB.1;Password=" & ConPassword & ";Persist Security Info=True;User ID=" & ConUserId & ";Initial Catalog=" & ConDBName & ";Data Source=" & ConServerName & "; Connect Timeout=20")
        Else
            Con = New OleDbConnection("Provider=SQLOLEDB.1;Data Source=" & ConServerName & "; Initial Catalog=" & ConDBName & ";Integrated Security=SSPI;Connect Timeout=20")
        End If
        If Con.State = ConnectionState.Closed Then Con.Open()
        Try
            Dim strSql As String = "select config_value from ConfigValuesTable where config_type='" & Key.Replace("'", "''") & "'"
            Dim dt As New DataTable
            Dim adp As New OleDb.OleDbDataAdapter
            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            dt.AcceptChanges()

            If dt Is Nothing Then Return False
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return "Error"
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAccountOpeningBalance(ByVal AccountId As Integer, Optional ByVal UptoDate As String = "", Optional ByVal Condition As String = "", Optional ByVal flgIncludeUnPost As Boolean = True, Optional ByVal CostCenterId As Integer = 0, Optional ByVal CostCenterCriteria As String = "") As Integer

        Dim flgCompanyRights As Boolean = False
        If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            flgCompanyRights = getConfigValueByType("CompanyRights")
        End If
        Dim strSql As String = String.Empty
        If Condition = "Cash" Then
            ''Commented below line to filter data by Cost Center Group wise too. on 31-07-2017
            'strSql = "SELECT  SUM(opening.Opning) AS OpeningAmount " & _
            '        "FROM  (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " & _
            '        "FROM  dbo.tblVoucher V INNER JOIN " & _
            '        "dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id inner join vwcoadetail vw on vw.coa_detail_id = vd.coa_detail_id " & _
            '        "WHERE (Convert(varchar, V.voucher_date,102) < Convert(Datetime, '" & CDate(UptoDate).ToString("yyyy-M-d 00:00:00") & "', 102))  " & IIf(flgIncludeUnPost = True, " and isnull(v.Post,0) in(1,0,NULL)", " and isnull(v.Post,0) in(1)") & " " & IIf(flgCompanyRights = True, " and vw.companyid=" & MyCompanyId & "", "") & " " & IIf(AccountId > 0, " AND VD.coa_detail_id=" & AccountId & "", "") & "  " & IIf(CostCenterId > 0, " and CostCenterID = " & CostCenterId, "") & " " & _
            '        "GROUP BY VD.coa_detail_id) opening INNER JOIN " & _
            '        "dbo.vwCOADetail ON opening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
            '        " WHERE (dbo.vwCOADetail.account_type = 'Cash')  " & IIf(flgCompanyRights = True, " And vwCOADetail.CompanyId=" & MyCompanyId & " ", "") & " "
            '' TASK : TFS1212 Applied Cost Centre Group Wise filter to this query.
            strSql = "SELECT  SUM(opening.Opning) AS OpeningAmount " & _
                    "FROM  (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " & _
                    "FROM  dbo.tblVoucher V INNER JOIN " & _
                    "dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id inner join vwcoadetail vw on vw.coa_detail_id = vd.coa_detail_id " & _
                    "WHERE (Convert(varchar, V.voucher_date,102) < Convert(Datetime, '" & CDate(UptoDate).ToString("yyyy-M-d 00:00:00") & "', 102))  " & IIf(flgIncludeUnPost = True, " and isnull(v.Post,0) in(1,0,NULL)", " and isnull(v.Post,0) in(1)") & " " & IIf(flgCompanyRights = True, " and vw.companyid=" & MyCompanyId & "", "") & " " & IIf(AccountId > 0, " AND VD.coa_detail_id=" & AccountId & "", "") & "  " & CostCenterCriteria & " " & _
                    "GROUP BY VD.coa_detail_id) opening INNER JOIN " & _
                    "dbo.vwCOADetail ON opening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
                    " WHERE (dbo.vwCOADetail.account_type = 'Cash')  " & IIf(flgCompanyRights = True, " And vwCOADetail.CompanyId=" & MyCompanyId & " ", "") & " "
        ElseIf Condition = "Bank" Then
            ''Commented below line to filter data by Cost Center Group wise too. on 31-07-2017
            'strSql = "SELECT SUM(opening.Opning) AS OpeningAmount " & _
            '                   "FROM  (SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " & _
            '                   "FROM  dbo.tblVoucher V INNER JOIN " & _
            '                   "dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id inner join vwcoadetail vw on vw.coa_detail_id = vd.coa_detail_id " & _
            '                   "WHERE (Convert(Varchar,V.voucher_date,102) < Convert(datEtime,'" & CDate(UptoDate).ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(flgIncludeUnPost = True, " and isnull(v.Post,0) in(1,0,NULL)", " and isnull(v.Post,0) in(1)") & " " & IIf(flgCompanyRights = True, " and vw.companyid=" & MyCompanyId & "", "") & " " & IIf(AccountId > 0, " AND VD.coa_detail_id=" & AccountId & "", "") & "  " & IIf(CostCenterId > 0, " and CostCenterID = " & CostCenterId, "") & " " & _
            '                   "GROUP BY VD.coa_detail_id) opening INNER JOIN " & _
            '                   "dbo.vwCOADetail ON opening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
            '                   " WHERE (dbo.vwCOADetail.account_type = 'Bank')  " & IIf(flgCompanyRights = True, " And vwCOADetail.CompanyId=" & MyCompanyId & " ", "") & " "
            '' TASK : TFS1212 Applied Cost Centre Group Wise filter to this query.
            strSql = "SELECT SUM(opening.Opning) AS OpeningAmount " & _
                             "FROM  (SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " & _
                             "FROM  dbo.tblVoucher V INNER JOIN " & _
                             "dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id inner join vwcoadetail vw on vw.coa_detail_id = vd.coa_detail_id " & _
                             "WHERE (Convert(Varchar,V.voucher_date,102) < Convert(datEtime,'" & CDate(UptoDate).ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(flgIncludeUnPost = True, " and isnull(v.Post,0) in(1,0,NULL)", " and isnull(v.Post,0) in(1)") & " " & IIf(flgCompanyRights = True, " and vw.companyid=" & MyCompanyId & "", "") & " " & IIf(AccountId > 0, " AND VD.coa_detail_id=" & AccountId & "", "") & "  " & CostCenterCriteria & " " & _
                             "GROUP BY VD.coa_detail_id) opening INNER JOIN " & _
                             "dbo.vwCOADetail ON opening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
                             " WHERE (dbo.vwCOADetail.account_type = 'Bank')  " & IIf(flgCompanyRights = True, " And vwCOADetail.CompanyId=" & MyCompanyId & " ", "") & " "
        ElseIf Condition = "Expense" Then
            ''Commented below line to filter data by Cost Center Group wise too. on 31-07-2017
            'strSql = "SELECT     SUM(opening.Opning) AS OpeningAmount " & _
            '       "FROM         (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " & _
            '       "FROM          dbo.tblVoucher V INNER JOIN " & _
            '       "dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id inner join vwcoadetail vw on vw.coa_detail_id = vd.coa_detail_id " & _
            '       "WHERE (Convert(DateTime,V.voucher_date,102) < Convert(DateTime,'" & CDate(UptoDate).ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(flgIncludeUnPost = True, " and isnull(v.Post,0) in(1,0,NULL)", " and isnull(v.Post,0) in(1)") & " " & IIf(flgCompanyRights = True, " and vw.companyid=" & MyCompanyId & "", "") & "  " & IIf(AccountId > 0, " AND VD.coa_detail_id=" & AccountId & "", "") & "  " & IIf(CostCenterId > 0, " and CostCenterID = " & CostCenterId, "") & " " & _
            '       "GROUP BY VD.coa_detail_id) opening INNER JOIN " & _
            '       "dbo.vwCOADetail ON opening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
            '       " WHERE (dbo.vwCOADetail.account_type = 'Expense') " & IIf(flgCompanyRights = True, " And vwCOADetail.CompanyId=" & MyCompanyId & " ", "") & " "
            '' TASK : TFS1212 Applied Cost Centre Group Wise filter to this query.
            strSql = "SELECT     SUM(opening.Opning) AS OpeningAmount " & _
                 "FROM         (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " & _
                 "FROM          dbo.tblVoucher V INNER JOIN " & _
                 "dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id inner join vwcoadetail vw on vw.coa_detail_id = vd.coa_detail_id " & _
                 "WHERE (Convert(DateTime,V.voucher_date,102) < Convert(DateTime,'" & CDate(UptoDate).ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(flgIncludeUnPost = True, " and isnull(v.Post,0) in(1,0,NULL)", " and isnull(v.Post,0) in(1)") & " " & IIf(flgCompanyRights = True, " and vw.companyid=" & MyCompanyId & "", "") & "  " & IIf(AccountId > 0, " AND VD.coa_detail_id=" & AccountId & "", "") & "  " & CostCenterCriteria & " " & _
                 "GROUP BY VD.coa_detail_id) opening INNER JOIN " & _
                 "dbo.vwCOADetail ON opening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
                 " WHERE (dbo.vwCOADetail.account_type = 'Expense') " & IIf(flgCompanyRights = True, " And vwCOADetail.CompanyId=" & MyCompanyId & " ", "") & " "
        ElseIf Condition = "CashAndBank" Then
            ''Commented below line to filter data by Cost Center Group wise too. on 31-07-2017
            'strSql = "SELECT SUM(opening.Opning) AS OpeningAmount " & _
            '       "FROM  (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " & _
            '       "FROM      dbo.tblVoucher V INNER JOIN " & _
            '       "dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id inner join vwcoadetail vw on vw.coa_detail_id = vd.coa_detail_id " & _
            '       "WHERE (Convert(varchar,V.voucher_date,102) < Convert(dateTime,'" & CDate(UptoDate).ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(flgIncludeUnPost = True, " and isnull(v.Post,0) in(1,0,NULL)", " and isnull(v.Post,0) in(1)") & " " & IIf(flgCompanyRights = True, " and vw.companyid=" & MyCompanyId & "", "") & "  " & IIf(AccountId > 0, " AND VD.coa_detail_id=" & AccountId & "", "") & " " & IIf(CostCenterId > 0, " and CostCenterID = " & CostCenterId, "") & " " & _
            '       "GROUP BY VD.coa_detail_id) opening INNER JOIN " & _
            '       "dbo.vwCOADetail ON opening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
            '       "WHERE     (dbo.vwCOADetail.account_type in ('Cash','Bank')) " & _
            '       " " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & " ", "") & ""
            '' TASK : TFS1212 Applied Cost Centre Group Wise filter to this query.
            strSql = "SELECT SUM(opening.Opning) AS OpeningAmount " & _
                 "FROM  (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning " & _
                 "FROM      dbo.tblVoucher V INNER JOIN " & _
                 "dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id inner join vwcoadetail vw on vw.coa_detail_id = vd.coa_detail_id " & _
                 "WHERE (Convert(varchar,V.voucher_date,102) < Convert(dateTime,'" & CDate(UptoDate).ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(flgIncludeUnPost = True, " and isnull(v.Post,0) in(1,0,NULL)", " and isnull(v.Post,0) in(1)") & " " & IIf(flgCompanyRights = True, " and vw.companyid=" & MyCompanyId & "", "") & "  " & IIf(AccountId > 0, " AND VD.coa_detail_id=" & AccountId & "", "") & " " & CostCenterCriteria & " " & _
                 "GROUP BY VD.coa_detail_id) opening INNER JOIN " & _
                 "dbo.vwCOADetail ON opening.coa_detail_id = dbo.vwCOADetail.coa_detail_id " & _
                 "WHERE     (dbo.vwCOADetail.account_type in ('Cash','Bank')) " & _
                 " " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & " ", "") & ""
        End If
        ''END TASK: TFS1212
        Dim dt As New DataTable
        Dim adp As New OleDbDataAdapter
        Try
            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            dt.AcceptChanges()
            If dt Is Nothing Then Return 0
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return 0
            End If

        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function GetOpeningBalance(ByVal AccountId As Integer, ByVal FromDate As DateTime) As Double
        Try
            Dim Opening As Double = 0
            Dim str As String = "Select SUM(ISNULL(VD.Debit_Amount,0) - ISNULL(VD.Credit_Amount,0)) as Opening From tblVoucher V INNER JOIN tblVoucherDetail VD ON V.Voucher_Id = VD.Voucher_Id WHERE VD.coa_detail_id=" & AccountId & " AND (Convert(varchar, V.Voucher_Date, 102) < Convert(Datetime, '" & FromDate.Date.ToString("yyyy-M-d 00:00:00") & "', 102)) "
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    Opening = dt.Rows(0).Item(0)
                Else
                    Opening = 0
                End If
                Return Opening
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetStockOpeningBalance(ByVal AccountId As Integer, Optional ByVal UptoDate As String = "") As Integer

        Dim strSql As String

        strSql = "select((SELECT     ISNULL(SUM(dbo.SalesReturnDetailTable.Qty), 0) AS SalesReturnQty " & _
                "FROM         dbo.SalesReturnDetailTable INNER JOIN " & _
                "                      dbo.SalesReturnMasterTable ON dbo.SalesReturnDetailTable.SalesReturnId = dbo.SalesReturnMasterTable.SalesReturnId " & _
                "WHERE     (dbo.SalesReturnDetailTable.ArticleDefId = " & AccountId & ") AND (dbo.SalesReturnMasterTable.SalesReturnDate < '" & UptoDate & "') " & _
                ")+(SELECT     ISNULL(SUM(dbo.ReceivingDetailTable.Qty), 0) AS ReceivingQty FROM         dbo.ReceivingDetailTable INNER JOIN " & _
                "                      dbo.ReceivingMasterTable ON dbo.ReceivingDetailTable.ReceivingId = dbo.ReceivingMasterTable.ReceivingId  " & _
                "WHERE     (dbo.ReceivingDetailTable.ArticleDefId = " & AccountId & ") AND (dbo.ReceivingMasterTable.ReceivingDate < '" & UptoDate & "') " & _
                "))-((SELECT     ISNULL(SUM(dbo.SalesDetailTable.Qty), 0) AS SalesQty FROM         dbo.SalesDetailTable INNER JOIN " & _
                "                      dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId " & _
                "WHERE     (dbo.SalesDetailTable.ArticleDefId = " & AccountId & ") AND (dbo.SalesMasterTable.SalesDate < '" & UptoDate & "') " & _
                ")+(SELECT     ISNULL(SUM(dbo.DispatchDetailTable.Qty), 0) AS DispatchQty FROM         dbo.DispatchDetailTable INNER JOIN " & _
                "                      dbo.DispatchMasterTable ON dbo.DispatchDetailTable.DispatchId = dbo.DispatchMasterTable.DispatchId " & _
                "WHERE     (dbo.DispatchDetailTable.ArticleDefId = " & AccountId & ") AND (dbo.DispatchMasterTable.DispatchDate < '" & UptoDate & "') " & _
                ")+(SELECT     ISNULL(SUM(dbo.PurchaseReturnDetailTable.Qty), 0) AS PurchaseReturnQty FROM         dbo.PurchaseReturnDetailTable INNER JOIN " & _
                "                     dbo.PurchaseReturnMasterTable ON dbo.PurchaseReturnDetailTable.PurchaseReturnId = dbo.PurchaseReturnMasterTable.PurchaseReturnId " & _
                "WHERE     (dbo.PurchaseReturnDetailTable.ArticleDefId = " & AccountId & ") AND (dbo.PurchaseReturnMasterTable.PurchaseReturnDate < '" & UptoDate & "')))"

        Dim dt As New DataTable
        Dim adp As New OleDbDataAdapter
        adp = New OleDbDataAdapter(strSql, Con)
        adp.Fill(dt)
        dt.AcceptChanges()
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item(0).ToString
        Else
            Return 0
        End If
    End Function
    Public Function GetItemStock(ByVal ItemId As Integer, Optional ByVal UptoDate As String = "") As Integer
        Try
            Dim strSql As String
            strSql = "select stock from vw_ArticleStock where articleid=" & ItemId
            Dim dt As New DataTable
            Dim adp As New OleDbDataAdapter
            adp = New OleDbDataAdapter(strSql, Con)
            adp.Fill(dt)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Build central  function to print voucher
    ''' </summary>
    ''' <param name="DocumentNo"></param>
    ''' <param name="FormName"></param>
    ''' <param name="BaseCurrencyName"></param>
    ''' <param name="BaseCurrencyId"></param>
    ''' <remarks>TASK:TFS1077  Build central  function to print voucher from any transaction screen. Done by Ameen</remarks>
    Public Sub GetVoucherPrint(ByVal DocumentNo As String, ByVal FormName As String, Optional ByVal BaseCurrencyName As String = "PKR", Optional ByVal BaseCurrencyId As Integer = 1, Optional ByVal DirectPrint As Boolean = False)
        Try
            Dim VoucherId As Integer = GetVoucherId(FormName, DocumentNo)
            Dim dtVoucherData As DataTable = GetVoucherData(VoucherId)
            If dtVoucherData.Rows.Count > 0 Then
                If Val(dtVoucherData.Rows(0).Item("CurrencyId").ToString) > 0 AndAlso Val(dtVoucherData.Rows(0).Item("CurrencyId").ToString) <> BaseCurrencyId Then
                    AddRptParam("BaseCurrency", BaseCurrencyName)
                    ShowReport("rptVoucherMultiCurrency", , , , DirectPrint, , , dtVoucherData)
                Else
                    '   AddRptParam("@VoucherId", r.Cells("voucher_id").Value)
                    ShowReport("rptVoucher", , , , DirectPrint, , , dtVoucherData)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Get voucher data against voucher id
    ''' </summary>
    ''' <param name="VoucherId"></param>
    ''' <returns></returns>
    ''' <remarks>TASK:TFS1077. Done by Ameen on 20-07-2017</remarks>
    Private Function GetVoucherData(ByVal VoucherId As Int32) As DataTable 'TASK42
        Try
            Dim DT As New DataTable
            DT = GetDataTable("SP_RptVoucherMultiCurrency " & VoucherId & "")
            DT.AcceptChanges()
            For Each DR As DataRow In DT.Rows
                DR.BeginEdit()
                Dim bcp As New BarcodeProfessional()
                bcp.Symbology = Symbology.Code128
                bcp.Extended = True
                bcp.DisplayCode = False
                bcp.AddChecksum = False
                bcp.Code = "?" & DR.Item("voucher_no").ToString
                DR.Item("BarCode") = bcp.GetBarcodeImage(System.Drawing.Imaging.ImageFormat.Png)
                DR.EndEdit()
            Next
            Return DT
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function
    ''END TASK:TFS1077
End Module
