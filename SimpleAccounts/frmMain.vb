''13-Dec-2013 ReqId-918 Imran Ali ERP Security right Problem
''13-Dec-2013 ReqID-912 Imran Ali Company list show after logging
'' 21-12-2013 ReqID-957   M Ijaz Javed      Bank information entry option
''28-Dec-2013 RM6 Imran Ali          Release 2.1.0.0 Bug
''decDec-2013 Tsk:2363 Imran Ali         Multi Ledger Option For Print
''1-Jan-2014 Tsk:2364    Imran Ali         Add new qoutation and ERP Sales report print 
''3-Jan-2014 Tsk:2357        Imran Ali          Sales Comparison Month Wise Report
''8-Jan-2014  TSK2370           Imran Ali          Invoice wise aging report   
''03-Feb-2014     TASK:2410       Imran Ali           Develop new sale return report of all customer and items like wise sale demand report    
''08-Feb-2014    Task:2418          Imran Ali      Add new report sales certificate and wh tax ceritificate
''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
''21-Feb-2014     TASK:2434 Imran Ali   Average Rate Not Update Properly 
''26-Feb-2014 TASK:2441  Imran Ali   3-list of those customers who can not interact with company  for last fifteen days.
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
''06-Mar-2014   Task:2462  Imran Ali    Add New Report Purchase Daily Working
''12-Mar-2014  TASK:2488  Imran Ali Sales Certificate In ERP
''19-Mar-2014 TASK:2506 Imran Ali  Add batch quantity and finish goods name in store issue detail report
''27-Mar-2014 Task:2522 Imran Ali 2-no. of sale certificate issued record.
''26-Apr-2014 TSK:2594 Imran Ali  New Enhancement Employees Salaries Voucher
'29-Apr-2014 TSK:2592 JUNAID SHEHZAD New Enhancement Employee OverTime Schedule define
''7-May-2014 TASK:2609 Imran Ali Vendor List Visible on Purchase/Purchase Return Invoices Summary Report form
''Task No 2616 Update New Key For Consolidated  
'TASK no 2624 Update The Key For LateTimeSlot
''14-May-2014 Task:M41  Imran ali Added Cost Sheet Menu 
'19-MAY-2014 TASK 2639 JUNAID vendor type in vendor information.
'19-May-2014 task 2640 JUNAID SMS Schedule
''21-May-2014 TASK:2638 Imran Ali Rickshaw Claim Record
''12-June-2014 TASK:2678 Imran Ali Sales Certificate Issued Report
''16-June-2014 TASK:2690 Imran Ali Add Department and Employee Fields On Production Entry
''26-June-2014 TASK2704 Imran Ali Add new functionality cash request in erp.
''08-Jul-2014 TASK:2725 IMRAN ALI Add new report product summary
''05-Aug-2014 Task:2769 Imran Ali Add new report CMFA Summary (Ravi)
''19-Aug-2014 Task:2792 Imran Ali Invoice Aging Report Menu Shift in Accounts #Menu Shift On Design Time
''26-Aug-2014 Task:2810 Imran Ali PL Note Sub Sub Account Wise Summary Report (Cotton Craft)
''06-Sep-2014 Task:2830 Imran Ali Add New Account POP Form Rights Based
''22-Sep-2014 TAKS:2850 Imran Ali Department And Category Wise Purchase Report
''25-Sep-2014 Task:2858 Imran Ali Add New CMFA Detail Report
''2-Oct-2014 Task:2863 Imran Ali Add new report Item Wise Sales Summary
''03-Oct-2014 Task:2864 Imran Ali Cash Receipt Detail Against Employee
''2015-05-15 Task#20150510 Adding Missing Documents forms Ali Ansari
''2015-05-20 Task#20150511 Adding Region,Zone,Belt,State,Country forms Ali Ansari
''2015-05-23 Task#20150516 regarding block style sheets
''2015-05-25 Task#20150517 Adding Project Portfolio Ali Ansari
'19-Jun-2015 Task#119062015 Ahmad Sharif hide forms in version wise
'19-Jun-2015 'Task#2011506017 Ali Ansari Add Partner Form
'31-Jul-2015 Task#31072015 Ahmad Sharif: add key for Employee Attendance email alert 
'01-Aug-2015 Task#01082015 Ahmad Sharif: add background worker for sending employee attendance report via email
'03-Aug-2015  Task#03082015 Ahmad Sharif Set timer, checking my computer name with host (Host save in configuration)
Imports System
Imports System.Diagnostics
Imports System.Windows.Forms
'Imports CQSystem
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win
Imports SBModel
Imports System.Data.OleDb
Imports System.Net
Imports System.Xml
Imports SBDal
Imports Microsoft.VisualStudio.Shell.RegistrationAttribute
Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.IO
Imports Newtonsoft.Json.Linq
Imports System.Web.Script.Serialization

Public Class frmMain

    Public DownloadInProgress As Boolean = False
    Dim imageCount As Integer = 1
    Public ControlName As New Form
    Public LastControlName As New Form
    Public NextControlName As New Form
    'Indicates if we are changing the selected node of the treeview programmatically
    Private ChangingSelectedNode As Boolean
    Dim enm As EnumForms = EnumForms.Non
    Dim Lastenum As EnumForms = EnumForms.Non
    Dim Nextenm As EnumForms = EnumForms.Non
    Dim strControlName As String
    Public dbVersion As String = String.Empty
    Public IsOpenMainForm As Boolean = False
    Public Tags As String = String.Empty
    Public Shared strStartUpPath As String = String.Empty
    Public Shared strDownloadReleasePath As String = String.Empty
    Dim IsBackgroundChanged As Boolean = False
    Dim ShowStartupTip As Boolean = False
    Dim ReminderFromDate As DateTime = Date.Now
    Dim ReminderTime As String = String.Empty
    Dim dtReminder As DataTable
    'Dim frmDefArt As frmDefArticle
    Public Shared flg As Boolean = False
    Dim flgReminder As Boolean = False
    Dim NewSecurityRights As Boolean = True
    Dim FormTag As String = String.Empty
    Dim flgCompanyRights As Boolean = False
    Dim RestrictForm As String = String.Empty
    Dim RestrictSheetAccess As String = String.Empty
    Private Declare Auto Function SendMessage Lib "user32" (ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    Private blnCustomMDI As Boolean = False
    Private _IsRemoveLayout As Boolean = False

    Dim BackupCalled As Boolean = False
    Dim arHistory As New ArrayList

    'Change for auto generated birthday email Mutaza (11/03/2022)
    Dim EmailTemplate As String = String.Empty
    Dim UsersEmail As String = String.Empty
    Dim ContractNo As String = String.Empty
    Dim CustomerId As String = String.Empty
    Dim StartDate As String = String.Empty
    Dim EndDate As String = String.Empty
    Dim ConcernEmployee As String = String.Empty
    Dim dtEmail As DataTable
    Dim EmailDAL As New EmailTemplateDAL
    Dim AfterFieldsElement As String = String.Empty
    Dim AllFields As List(Of String)
    Dim html As StringBuilder
    Dim EmailBody As String = String.Empty
    'Change for auto generated birthday email Mutaza (11/03/2022)
    Dim CompanyBasePrefix As Boolean = False
    'Change for contain and begin with 
    Public blnListSeachStartWith As Boolean = False
    Public blnListSeachEndWith As Boolean = False
    Public blnListSeachContains As Boolean = False
    'Change for contain and begin with 
    Public MACAddressList As String = ""
    Public InvoiceDate As Date
    Dim InvoiceNo As String = String.Empty
    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.pnlEmptyBackground.Visible = True
        Me.pnlEmptyBackground.BringToFront()
        Try
            'If Date.Today > "14-Aug-2013" Then msg_Error("An error occured, application will be terminated  " & Chr(13) & "Error No: 114 - Registery Check Error" & Chr(13) & Chr(13) & Chr(13) & "Please contact " & Chr(13) & "Agrius BUSINESS SOLUTIONS" & Chr(13) & Chr(13) & "Phone: +923-444-114-000" & Chr(13) & "E-Mail: contact@Agrius.net" & Chr(13) & "Web: www.Agrius.net") : End
            str_ApplicationStartUpPath = Application.StartupPath
            strStartUpPath = Application.StartupPath
            'Con.Open()
            'If Con.State = ConnectionState.Open Then
            '    Con.Close()
            'Else : msg_Error("Could not connect to server, please check your network....") : End
            'End If
        Catch ex As Exception
            msg_Error("An error occured while connecting to the server  " & Chr(13) & ex.Message)
            End
        End Try

        If Not IO.File.Exists(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml") Then
            frmSetup.BringToFront()
            frmSetup.ShowDialog()
            If Not IO.File.Exists(str_ApplicationStartUpPath & "\CompanyConnectionInfo.Xml") Then
                End
            End If
        End If

        ''frmmainlogin.BringToFront()
        ''frmmainlogin.ShowDialog()

        'Try

        '    'Get Config Lisjt
        '    If BackgroundWorker8.IsBusy Then Exit Sub
        '    BackgroundWorker8.RunWorkerAsync()
        '    Do While BackgroundWorker8.IsBusy
        '        Application.DoEvents()
        '    Loop



        '    'If Me.BackgroundWorker8.IsBusy = True Then
        '    '    Do While Me.BackgroundWorker8.IsBusy
        '    '        Application.DoEvents()
        '    '    Loop
        '    'End If

        '    'If getConfigValueByType("AgriusPartner").ToString = "True" Then
        '    '    GetAgriusPartner(getConfigValueByType("AgriusPartnerName").ToString)
        '    '    Me.Text = getConfigValueByType("AgriusPartnerName").ToString
        '    'Else
        '    '    str_Company = str_Company
        '    '    str_MessageHeader = str_MessageHeader
        '    '    Me.Text = str_Company
        '    'End If

        '    'dbVersion = getConfigValueByType("Version").ToString
        '    'Me.UltraStatusBar2.Panels(2).Text = "For Help and Support: [" & str_Support_Phone & "] [" & str_Support_Email & "]                                         "
        'Me.UltraStatusBar2.Panels(1).Text = "DB Ver: " & dbVersion "Database: " & Con.Database & " Server: " & Con.DataSource & " DB Ver: " & dbVersion

        '    'If dbVersion.ToString.Replace(".", "") < Replace(Application.ProductVersion, ".", "") Then
        '    '    'Me.Visible = True
        '    '    'Application.DoEvents()
        '    '    LoadControl("UpdateVersion")
        '    '    Try
        '    '        If Not IO.Directory.Exists(Application.StartupPath & "\BackupLayouts") Then
        '    '            IO.Directory.CreateDirectory(Application.StartupPath & "\BackupLayouts")
        '    '        End If
        '    '        If IO.Directory.Exists(Application.StartupPath & "\Layouts") Then
        '    '            Dim strFiles() As String = IO.Directory.GetFiles(Application.StartupPath & "\Layouts", "*_*")
        '    '            If strFiles.Length > 0 Then
        '    '                For Each f As String In strFiles
        '    '                    If IO.File.Exists(f) Then
        '    '                        If IO.File.Exists(Application.StartupPath & "\BackupLayouts\" & f.Substring(f.LastIndexOf("\") + 1) & "") Then
        '    '                            IO.File.Delete(Application.StartupPath & "\BackupLayouts\" & f.Substring(f.LastIndexOf("\") + 1) & "")
        '    '                        End If
        '    '                        IO.File.Copy(f, Application.StartupPath & "\BackupLayouts\" & f.Substring(f.LastIndexOf("\") + 1) & "")
        '    '                    End If
        '    '                Next
        '    '            End If
        '    '        End If
        '    '        If IO.Directory.Exists(Application.StartupPath & "\Layouts") Then
        '    '            Dim strFiles() As String = IO.Directory.GetFiles(Application.StartupPath & "\Layouts", "*_*")
        '    '            If strFiles.Length > 0 Then
        '    '                For Each f As String In strFiles
        '    '                    If IO.File.Exists(f) Then
        '    '                        IO.File.Delete(f)
        '    '                    End If
        '    '                Next
        '    '            End If
        '    '        End If
        '    '    Catch ex As Exception
        '    '    End Try

        '    'End If


        '    If IsCustomizableMDI() = True Then
        '        frmCustomizeEnvirement.Show()
        '        frmCustomizeEnvirement.BringToFront()
        '        blnCustomMDI = True
        '    End If


        '    'CreateCurrentDBBackup()

        '    '-------------------- get Company List/ User Rights
        '    'If BackgroundWorker6.IsBusy Then Exit Sub
        '    'BackgroundWorker6.RunWorkerAsync()
        '    'Do While BackgroundWorker6.IsBusy
        '    '    Application.DoEvents()
        '    'Loop

        '    'If GetConfigValue("EnableSecurity") = "True" Then
        '    'End If
        '    ' Add any initialization after the InitializeComponent() call.
        '    'Try

        '    '    Me.Timer3.Enabled = True
        '    '    Me.Timer3.Interval = 100 '300000

        '    'Catch ex As Exception

        '    'End Try


        '    'If flgCompanyRights = True Then
        '    '    frmMyCompany.ShowDialog()
        '    '    MyCompanyId = frmMyCompany.CompanyId
        '    'End If


        'Catch ex As Exception
        '    Throw ex
        'End Try

        'Try
        '    Dim flgMenuRights As Boolean = False
        '    'If Not getConfigValueByType("MenuRights").ToString = "Error" Then
        '    '    flgMenuRights = getConfigValueByType("MenuRights")
        '    'Else
        '    '    flgMenuRights = False
        '    'End If
        '    If flgMenuRights = True Then
        '        '    Dim clsRights As New SBModel.GroupRights
        '        '    For g As Integer = 0 To (Me.UltraExplorerBar2.Groups.Count - 1)
        '        '        For i As Integer = 0 To (Me.UltraExplorerBar2.Groups(g).Items.Count - 1)
        '        '            If Me.UltraExplorerBar2.Groups(g).Items(i).Tag <> "" Then
        '        '                FormTag = Me.UltraExplorerBar2.Groups(g).Items(i).Tag
        '        '                clsRights = GroupRights.Find(AddressOf getRightsFroms)
        '        '                If Not clsRights Is Nothing Then
        '        '                    Me.UltraExplorerBar2.Groups(g).Items(i).Visible = True
        '        '                Else
        '        '                    Me.UltraExplorerBar2.Groups(g).Items(i).Visible = False
        '        '                End If
        '        '            End If
        '        '        Next
        '        '    Next
        '        '    Me.UltraExplorerBar2.Update()

        '        'For i As Integer = 0 To Me.UltraToolbarsManager1.Tools.Count - 1
        '        '    If Not Me.UltraToolbarsManager1.Tools(i).Tag = "" Then
        '        '        FormTag = Me.UltraToolbarsManager1.Tools(i).Tag
        '        '        clsRights = GroupRights.Find(AddressOf getRightsFroms)
        '        '        If Not clsRights Is Nothing Then
        '        '            Me.UltraToolbarsManager1.Tools(i).SharedProps.Visible = True
        '        '        Else
        '        '            Me.UltraToolbarsManager1.Tools(i).SharedProps.Visible = False
        '        '        End If
        '        '    End If
        '        'Next
        '    End If

        'Catch ex As Exception

        'End Try

        'Try
        '    '------------------------ Checking Date Lock  -----------------------
        '    If BackgroundWorker7.IsBusy Then Exit Sub
        '    BackgroundWorker7.RunWorkerAsync()
        '    Do While BackgroundWorker7.IsBusy
        '        Application.DoEvents()
        '    Loop

        'Catch ex As Exception

        'End Try

    End Sub
    Public Function RestrictSheet(ByVal key As String) As Boolean
        Try

            RestrictSheetAccess = key
            Dim obj As Object = GetFormAccessByArray.Find(AddressOf FindRestrictForm)
            If obj IsNot Nothing AndAlso obj.ToString.Length > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ReturnRights(ByVal Rights As SBModel.GroupRights) As Boolean
        Try
            If Rights.FormName = ControlName.Name Or Rights.FormName = enm.ToString Then
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getRightsFroms(ByVal Rights As SBModel.GroupRights) As Boolean
        Try
            If Rights.FormName = FormTag AndAlso Rights.FormControlName = "View" Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetMyCompany(ByVal Company As SBModel.CompanyInfo) As Boolean
        Try
            If Company.CompanyID = MyCompanyId Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Private Sub frmMain_Activated(sender As Object, e As EventArgs) Handles Me.Activated
    '    Try
    '        Timer10.Interval = 600000
    '        Timer10.Enabled = True
    '        SendAutomatedEmail()
    '    Catch ex As Exception
    '        ShowErrorMessage("Not clicking")
    '    End Try
    'End Sub

    'Private Sub frmMain_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
    '    Try
    '        Timer10.Interval = 10000
    '        Timer10.Enabled = True
    '        SendAutoEmail()
    '    Catch ex As Exception
    '        ShowErrorMessage("Not clicking")
    '    End Try
    'End Sub
    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            'frmBackupReminder.ShowDialog()
            Try
                If BackupCalled = False Then


                    Dim dt As New DataTable

                    dt = GetDataTable("select top 5 * from msdb.dbo.backupset where database_name = db_name() order by backup_finish_date desc")

                    If dt.Rows.Count > 0 Then

                        If CType(dt.Rows(0).Item("backup_finish_date").ToString, DateTime) < Date.Now.ToString("dd-MMM-yyyy 00:00:00") Then

                            Dim lastBackup As DateTime = CType(dt.Rows(0).Item("backup_finish_date").ToString, DateTime)

                            frmBackupReminder.lblDay.Text = lastBackup.DayOfWeek.ToString.ToUpper
                            frmBackupReminder.lblDate.Text = lastBackup.Day
                            frmBackupReminder.lblMonthYear.Text = lastBackup.ToString("MMM yyyy").ToString.ToUpper

                            If frmBackupReminder.ShowDialog() = Windows.Forms.DialogResult.Yes Then

                                LoadControl("UtilBackupNew")
                                BackupCalled = True
                                e.Cancel = True
                                Exit Sub

                            End If

                        End If


                        '    If msg_Confirm("Do you want to backup your data now ?" & Chr(10) & "Your last backup was done at " & CType(dt.Rows(0).Item("backup_finish_date").ToString, DateTime).ToString("dd-MMM-yyy HH:mm") & Chr(10) & Chr(10) & "It is recomended to backup you data now otherwise you might loose your all data.") = True Then

                        '        LoadControl("UtilBackupNew")
                        '        BackupCalled = True
                        '        e.Cancel = True
                        '        Exit Sub

                        '    End If


                    Else

                        msg_Error("You have never backed up your data so do it now otherwise you might loose your all data." & Chr(10) & Chr(10) & "You will be redirected to backup screen where you can do this." & Chr(10) & "Incase you face any issue you can contact your Agrius ERP Administrator")
                        LoadControl("UtilBackupNew")
                        BackupCalled = True
                        e.Cancel = True
                        Exit Sub

                    End If

                End If

            Catch ex As Exception

            End Try

            If DownloadInProgress = True Then

                e.Cancel = True

                Application.DoEvents()
                frmReleaseDownload.WindowState = FormWindowState.Normal
                frmReleaseDownload.TopMost = True

                Exit Sub

            End If

            If msg_Confirm("Do you want to log out?") = True Then
                If System.IO.Directory.Exists(str_ApplicationStartUpPath & "\ApplicationSettings") = False Then
                    System.IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\ApplicationSettings")
                End If
                Me.UltraExplorerBar2.SaveAsXml(str_ApplicationStartUpPath & "\TempExpSetting.xml")
                Me.SaveLayouts(Me.SplitContainer.Panel2)
                'LastGroup = Me.UltraExplorerBar1.ActiveItem.Group.Index
                'LastItem = Me.UltraExplorerBar1.ActiveItem.Index
                SaveLastSettings()
                'If System.IO.File.Exists(str_ApplicationStartUpPath & "\System Files\Sounds\Goodbye.wav") Then
                '    Me.AxWindowsMediaPlayer1.URL = str_ApplicationStartUpPath & "\System Files\Sounds\Goodbye.wav"
                '    Me.AxWindowsMediaPlayer1.Refresh() ' str_ApplicationStartUpPath & "\System Files\Sounds\Goodbye.wav")
                '    'Me.AxWindowsMediaPlayer1.playState.wmppsPlaying()
                'End If
                'changes for validate more than one user
                Dim dt As DataTable
                Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
                If objCon.State = ConnectionState.Closed Then objCon.Open()
                Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
                Dim cmd As New OleDb.OleDbCommand
                cmd.Connection = objCon
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 30
                cmd.Transaction = trans
                'Dim SqlCommand1 As OleDbCommand = New OleDbCommand()
                'SqlCommand1.Connection = Con2
                If getConfigValueByType("RestrictMultipleLogin").ToString = "True" Then
                    Dim strSql As String
                    strSql = "SELECT LoggedIn from tbluser where User_ID = " & LoginUserId & " "
                    dt = GetDataTable(strSql)
                    If dt.Rows(0).Item("loggedIn") = "True" Then
                        strSql = "update tblUser set LoggedIn = 'False' where User_ID=" & LoginUserId & ""
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql)
                        LoggedIn = False
                        trans.Commit()
                    End If
                End If
                'changes for validate more than one user
                Con.Close()
                Con.Dispose()
                frmMainLogin.Close()
            Else
                e.Cancel = True
                Exit Sub
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub SaveLayouts(ByVal control As Control)

        For Each ctl As Control In control.Controls
            If TypeOf ctl Is Form Then
                strControlName = ctl.Name & "_"
            End If
            If control.HasChildren Then Me.SaveLayouts(ctl)
            If TypeOf ctl Is UltraGrid Then
                Dim grd As UltraGrid = CType(ctl, UltraGrid)
                grd.DisplayLayout.SaveAsXml(str_ApplicationStartUpPath & "\ApplicationSettings\" & strControlName & ctl.Name & ".xml")
            ElseIf TypeOf ctl Is UltraCombo Then
                Dim cmb As UltraCombo = CType(ctl, UltraCombo)
                '  cmb.DisplayLayout.SaveAsXml(str_ApplicationStartUpPath & "\ApplicationSettings\" & strControlName & ctl.Name & ".xml")
            End If
        Next
    End Sub

    Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'If BardCodeScanFocused() = False Then Exit Sub
            If e.KeyCode = Keys.Enter AndAlso e.Alt Then

                'Me.SplitContainer.Panel2.Controls(0).
                Try

                    Dim frm As New Form
                    frm = Me.SplitContainer.Panel2.Controls(0)

                    frm.Visible = False
                    Me.SplitContainer.Panel2.Controls.RemoveAt(0)
                    frm.TopLevel = True
                    frm.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                    frm.WindowState = FormWindowState.Maximized
                    'frm.TopMost = True
                    frm.Location = New Point(0, 0)
                    frm.Size = SystemInformation.PrimaryMonitorSize
                    frm.ShowDialog()

                    frm.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                    frm.WindowState = FormWindowState.Maximized
                    frm.TopLevel = False
                    frm.Dock = DockStyle.Fill
                    Me.SplitContainer.Panel2.Controls.Add(frm)
                    frm.BringToFront()
                    frm.Show()
                Catch ex As Exception

                End Try

                Exit Sub

            End If

            If e.KeyCode = Keys.Enter Then
                If Not ControlName.Name = "frmSalesInquiry" AndAlso Not ControlName.Name = "frmPurchaseInquiry" AndAlso Not ControlName.Name = "frmVendorQuotation" AndAlso Not ControlName.Name = "frmAddChildItem" AndAlso Not ControlName.Name = "frmLoadJobCardCard" Then
                    SendKeys.Send("{TAB}")
                End If
            End If
            If e.KeyCode = Keys.I AndAlso e.Alt Then
                LoadControl("RecordSales")
            End If
            'Before against request no. RM6
            'If e.KeyCode = Keys.P AndAlso e.Alt Then
            If e.KeyCode = Keys.Y AndAlso e.Alt Then
                LoadControl("frmPurchase")
            End If
            If e.KeyCode = Keys.V AndAlso e.Alt Then
                LoadControl("frmVoucher")
            End If
            If e.KeyCode = Keys.L AndAlso e.Alt Then
                LoadControl("rptLedger")
            End If
            'Before gainst request no. RM6
            'If e.KeyCode = Keys.U AndAlso e.Alt Then
            If e.KeyCode = Keys.R AndAlso e.Alt Then
                LoadControl("StoreIssuence")
            End If
            'End R:M6
            If e.KeyCode = Keys.T AndAlso e.Alt Then
                LoadControl("frmSalesOrder")
            End If

            'Rafay:Short Key when press f8 then user wise customer is open
            If e.KeyCode = Keys.F8 Then
                LoadControl("frmUserRightsList")
            End If
            'Rafay

            If e.Control And e.KeyCode = Keys.F Then
                frmSearchMenu._Menu = String.Empty
                frmSearchMenu.BringToFront()
                frmSearchMenu.ShowDialog()
            End If
            If e.KeyCode = Keys.F11 Then
                frmItemSearch.ShowDialog()
            End If

            If e.KeyCode = Keys.F12 Then
                frmSearchCustomersVendors.ShowDialog()
            End If

            If e.KeyCode = Keys.F8 Then
                LoadControl("frmUserRightsList")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Sub HideControls()

        Dim g As Integer
        Dim i As Integer
        Dim a As Integer = 0

        Dim dt As New DataTable

        For g = 0 To (Me.UltraExplorerBar2.Groups.Count - 1)
            For i = 0 To (Me.UltraExplorerBar2.Groups(g).Items.Count - 1)
                Me.UltraExplorerBar2.Groups(g).Items(i).Visible = False
            Next
        Next
        Me.UltraExplorerBar2.Update()


    End Sub

    Sub AddItemsInRibbon()
        ' Me.ToolStrip.Visible = False
        'FoldersToolStripMenuItem.Checked = True
        'ToggleFoldersVisible()
        ' Me.UltraExplorerBar1.Visible = False
        Dim g As Integer
        Dim i As Integer
        Dim a As Integer = 0

        Dim dt As New DataTable

        'For g = 0 To Me.UltraToolbarsManager1.Ribbon.Tabs.Count - 1
        '    Me.UltraToolbarsManager1.Ribbon.Tabs.Remove(Me.UltraToolbarsManager1.Ribbon.Tabs(0).Key)
        'Next

        'For g = 0 To Me.UltraToolbarsManager1.Toolbars(0).Tools.Count - 1
        'Me.UltraToolbarsManager1.Toolbars(0).Tools.Remove(Me.UltraToolbarsManager1.Toolbars(0).Tools(0))
        'Next

        'Dim newTool As Infragistics.Win.UltraWinToolbars.PopupMenuControl


        For g = 0 To (Me.UltraExplorerBar2.Groups.Count - 1)
            With Me.UltraExplorerBar2.Groups(g)
                Dim strKey As String = IIf(.Key.Length > 0, .Key, .Text)


                '//Adding ribbon
                'Me.UltraToolbarsManager1.Ribbon.Tabs.Add(strKey)
                'Me.UltraToolbarsManager1.Ribbon.Tabs(IIf(.Key.Length > 0, .Key, .Text)).Caption = .Text
                'Me.UltraToolbarsManager1.Ribbon.Tabs(strKey).Groups.Add("TestGroup" & g)
                'Me.UltraToolbarsManager1.Ribbon.Tabs(strKey).Groups("TestGroup" & g).Caption = ""
                'Me.UltraToolbarsManager1.Ribbon.GroupSettings.Appearance.Image = Me.TreeNodeImageList.Images(3)
                '//Addin toolbar

                'Me.UltraToolbarsManager1.Toolbars(0).Tools.InsertTool(0, strKey)
                'Me.UltraToolbarsManager1.Toolbars(0).Tools(0).SharedProps.Caption = .Text

                'For i = 0 To .Items.Count - 1

                '    Dim strIKey As String = IIf(.Items(i).Key.Length > 0, .Items(i).Key, .Items(i).Text)
                '    Dim StrCaption As String = .Items(i).Text
                '    Dim UltraTool As New Infragistics.Win.UltraWinToolbars.ButtonTool(strIKey)
                '    If Me.UltraToolbarsManager1.Tools.Exists(strIKey) Then Me.UltraToolbarsManager1.Tools.Remove(UltraTool)
                '    'UltraTool.Control.Text = StrCaption
                '    UltraToolbarsManager1.Tools.Add(UltraTool)
                '    UltraToolbarsManager1.Tools.Item(strIKey).CustomizedCaption = StrCaption
                '    'UltraToolbarsManager1.Tools.Item(strIKey).InstanceProps.AppearancesSmall.Appearance.Image = Image.FromFile("C:\29.gif")
                '    'Me.Icon = Image.FromFile("")
                '    'UltraToolbarsManager1.Tools.Item(strIKey).InstanceProps.DisplayStyle = UltraWinToolbars.ToolDisplayStyle.ImageAndText

                '    'Me.UltraToolbarsManager1.Ribbon.Tabs(strkey).groups("")
                '    'Me.UltraToolbarsManager1.Ribbon.Tabs(strKey).Groups("TestGroup" & g).Tools.InsertTool(i, strIKey)
                '    Me.UltraToolbarsManager1.Ribbon.Tabs(strKey).Groups("TestGroup" & g).Tools.AddTool(strIKey)
                '    'Me.UltraToolbarsManager1.Ribbon.Tabs(strKey).Groups("TestGroup" & g).Tools(strIKey).Control.Text = StrCaption
                'Next

            End With

            'For i = 0 To (Me.UltraExplorerBar1.Groups(g).Items.Count - 1)
            'Me.UltraExplorerBar1.Groups(g).Items(i).Visible = False
            'Next
        Next
        Me.UltraExplorerBar2.Update()

    End Sub

    Private Sub frmMain_Layout(sender As Object, e As LayoutEventArgs) Handles Me.Layout

    End Sub
    'Private Sub frmMain_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
    '    If e.KeyCode = Keys.Enter Then
    '        SendKeys.Send("{TAB}")
    '    End If
    'End Sub
    Public Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'LoginForm4.BringToFront()
            'LoginForm4.ShowDialog()
            ''frmmainlogin.BringToFront()
            ''frmmainlogin.ShowDialog()
            Try

                'Get Config Lisjt
                If BackgroundWorker8.IsBusy Then Exit Sub
                BackgroundWorker8.RunWorkerAsync()
                Do While BackgroundWorker8.IsBusy
                    Application.DoEvents()
                Loop
                'If BackgroundWorker23.IsBusy Then Exit Sub
                'BackgroundWorker23.RunWorkerAsync()
                'Do While BackgroundWorker23.IsBusy
                '    Application.DoEvents()
                'Loop
                Me.Timer11.Enabled = True
                Me.Timer11.Interval = 5000
                Me.Timer12.Enabled = True
                ''Me.Timer12.Interval = 5000
                Me.Timer12.Interval = 86400000
                Me.Timer13.Enabled = True
                Me.Timer13.Interval = 1800000
                'Me.Timer12.Interval = 600000
                ''frmmainlogin.BringToFront()
                ''frmmainlogin.ShowDialog()
                If IsCustomizableMDI() = True Then
                    frmCustomizeEnvirement.Show()
                    frmCustomizeEnvirement.BringToFront()
                    blnCustomMDI = True
                End If

            Catch ex As Exception
            End Try
            If blnCustomMDI = True Then
                Me.Visible = False
            Else
                Me.pnlEmptyBackground.Visible = False
                'Me.Visible = True
            End If

            'Change for contain and begin with 
            If getConfigValueByType("ListSearchStartWith").ToString <> "Error" Then
                blnListSeachStartWith = Convert.ToBoolean(getConfigValueByType("ListSearchStartWith").ToString)
            End If
            If getConfigValueByType("ListSearchContains").ToString <> "Error" Then
                blnListSeachContains = Convert.ToBoolean(getConfigValueByType("ListSearchContains").ToString)
            End If
            'Change for contain and begin with 

            'If Me.UltraToolbarsManager1.Toolbars.Count > 3 Then
            '    Me.UltraToolbarsManager1.Toolbars.Remove(Me.UltraToolbarsManager1.Toolbars(2))
            'End If
            'If getConfigValueByType("LoadMenuSettings") = "True" Then
            '    If IO.File.Exists(Application.StartupPath & "\ExpSetting.xml") Then Me.UltraExplorerBar2.LoadFromXml(Application.StartupPath & "\ExpSetting.xml") Else ShowErrorMessage("Some files r missing please contact Agrius-0344-4114000 for more details") : End
            'End If

            'Me.ToolStripStatusLabel.Text = "User: " & LoginUserName
            Me.UltraStatusBar2.Panels("UserInfo").Text = "User: " & LoginUserName
            'Me.UltraStatusBar2.Panels("UserInfo").MarqueeInfo.IsActive = True
            'Set up the UI
            'SetUpListViewColumns()
            'LoadTree()
            ' Me.SplitContainer.Panel2.Controls.Add(CQSystem.frmClientMessages)
            'ControlName = frmVoucher
            'ControlName.TopLevel = False
            'ControlName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            'ControlName.Dock = DockStyle.Fill
            'Me.SplitContainer.Panel2.Controls.Add(ControlName)
            'ControlName.Show()
            'ControlName.BringToFront()
            'Me.LastControlName = ControlName
            'FillLocationCombo()
            'Me.UltraExplorerBar1.Groups(LastGroup).Active = True
            ' Me.UltraExplorerBar1.Groups(LastGroup).Enabled = True
            'Me.UltraExplorerBar1.Groups(LastGroup).Items(LastItem).en = True
            'Me.UltraExplorerBar1.Groups(LastGroup).Items(LastItem).Active = True
            'Me.UltraExplorerBar1_ItemClick(Nothing, Nothing)
            ' Me.LoadControl(LastItem)
            Me.PictureBox1.Visible = False
            Me.WindowState = FormWindowState.Maximized
            'If Not GetConfigValue("BarcodeEnabled") = "True" Then
            '  Me.UltraToolbarsManager1.Ribbon.Visible = True
            'Rafay:Task Start:explorer bar k panel1 ka size change karnay k liye 0 means does not show explorer bar
            Me.SplitContainer.SplitterDistance = 0
            'Rafay:Task End
            Me.ToolStrip.Dock = DockStyle.Top

            'Me.UltraExplorerBar1.Dock = DockStyle.Fill
            'Me.UltraExplorerBar1.BringToFront()

            Me.UltraExplorerBar2.Dock = DockStyle.Fill
            Me.UltraExplorerBar2.BringToFront()

            'Me.UltraToolbarsManager1.Ribbon.RibbonAreaAppearance.
            'Me.AddItemsInRibbon()
            'End If
            'Dim dbVersion As String = String.Empty
            ''
            '' Get Configurations
            'If BackgroundWorker9.IsBusy Then Exit Sub
            'BackgroundWorker9.RunWorkerAsync()
            'Do While BackgroundWorker9.IsBusy
            '    Application.DoEvents()
            'Loop

            ''
            'If Me.BackgroundWorker8.IsBusy = True Then
            '    Do While Me.BackgroundWorker8.IsBusy
            '        Application.DoEvents()
            '    Loop
            'End If
            'Rafay:Task Start : when Login to uae then form name show uae and when login to pak then it show pak on top of the page
            If getConfigValueByType("siriusPartner").ToString = "True" Then
                GetSIRIUSPartner(getConfigValueByType("siriusPartnerName").ToString)
                Me.Text = getConfigValueByType("siriusPartnerName").ToString
            ElseIf Con.Database = "SIRIUS1_DB" Then
                'ElseIf Con.Database = "RemmsTech_UAE_DB" Then
                CompanyPrefix = str_Company1
                str_MessageHeader = str_MessageHeader
                companyinitials = "PK"
                ''Me.Text = str_Company1
            ElseIf Con.Database = "RemmsPAK_DB" Then
                CompanyPrefix = str_Company2
                str_MessageHeader = str_MessageHeader
                companyinitials = "RPK"
            ElseIf Con.Database = "RemmsTech_UAE_DB" Then
                CompanyPrefix = str_Company3
                str_MessageHeader = str_MessageHeader
                companyinitials = "RUAE"
            ElseIf Con.Database = "SIRIUS_KSA_DB" Then
                CompanyPrefix = str_Company4
                str_MessageHeader = str_MessageHeader
                companyinitials = "KSA"
            ElseIf Con.Database = "SIRIUS_MY_DB" Then
                CompanyPrefix = str_Company5
                str_MessageHeader = str_MessageHeader
                companyinitials = "MY"
            ElseIf Con.Database = "SIRIUS_SL_DB" Then
                CompanyPrefix = str_Company6
                str_MessageHeader = str_MessageHeader
                companyinitials = "SL"
            Else
                CompanyPrefix = str_Company
                str_MessageHeader = str_MessageHeader
                ''Me.Text = str_Company
            End If
            Me.Text = CompanyPrefix
            
            ''GetInvoiceData()
            If Me.Text.Contains("UAE") Then
                companycountry = "(UAE)"
            ElseIf Me.Text.Contains("PAK") Then
                companycountry = "(PAK)"
            ElseIf Me.Text.Contains("KSA") Then
                companycountry = "(KSA)"
            ElseIf Me.Text.Contains("Remms-PAK") Then
                companycountry = "(Remms-PAK)"
            ElseIf Me.Text.Contains("Remms-UAE") Then
                companycountry = "(Remms-UAE)"
            ElseIf Me.Text.Contains("MY") Then
                companycountry = "(MY)"
            ElseIf Me.Text.Contains("SL") Then
                companycountry = "(SL)"
            End If
            ''GetInvoiceData()
            'Rafay:Task End
            'If ConUserId <> "" Then
            '    Con = New OleDbConnection("Provider=SQLOLEDB.1;Password=" & ConPassword & ";Persist Security Info=True;User ID=" & ConUserId & ";Initial Catalog=" & ConDBName & ";Data Source=" & ConServerName & ";Connect TimeOut=120")

            '    str_Company = str_Company
            '    ''  str_MessageHeader = str_MessageHeader
            '    Me.Text = str_Company
            'Else
            '    Con = New OleDbConnection("Provider=SQLOLEDB.1;Initial Catalog=" & ConDBName & ";Data Source=" & ConServerName & ";Integrated Security=SSPI;Connect TimeOut=120")

            '    str_Company1 = str_Company1
            '    ''str_MessageHeader = str_MessageHeader
            '    Me.Text = str_Company1
            'End If



            'ValidateLicense()

            dbVersion = getConfigValueByType("Version").ToString
            Me.UltraStatusBar2.Panels("HelpInfo").Text = "For Help and Support: [" & str_Support_Phone & "] [" & str_Support_Email & "]                                         "
            Me.UltraStatusBar2.Panels("DBInfo").Text = "DB Ver: " & dbVersion '"Database: " & Con.Database & " Server: " & Con.DataSource & " DB Ver: " & dbVersion
            If getConfigValueByType("HelpnSupportPanel").ToString = "False" Then
                Me.UltraStatusBar2.Panels("HelpInfo").Visible = False
            Else
                Me.UltraStatusBar2.Panels("HelpInfo").Visible = True
            End If
            If Not LicenseStatus = "Blocked" Then
                frmMainHome.TopLevel = False
                Me.SplitContainer.Panel2.Controls.Add(frmMainHome)
                frmMainHome.Show()
                frmMainHome.BringToFront()
                frmMainHome.Dock = DockStyle.Fill
                Me.SplitContainer.Panel2.VerticalScroll.Enabled = True
            End If
            'Aashir: Release was not downloading So These lines of code were added
            If getConfigValueByType("EnableAutoUpdate").ToString = True Then
                frmReleaseDownload.Visible = False
                frmReleaseDownload.Show()
            End If
            If dbVersion.ToString.Replace(".", "") < Replace(Application.ProductVersion, ".", "") Then
                'Me.Visible = True
                'Application.DoEvents()
                'LoadControl("UpdateVersion")
                'Try
                '    'If Not IO.Directory.Exists(Application.StartupPath & "\BackupLayouts") Then
                '    '    IO.Directory.CreateDirectory(Application.StartupPath & "\BackupLayouts")
                '    'End If
                '    'If IO.Directory.Exists(Application.StartupPath & "\Layouts") Then
                '    '    Dim strFiles() As String = IO.Directory.GetFiles(Application.StartupPath & "\Layouts", "*_*")
                '    '    If strFiles.Length > 0 Then
                '    '        For Each f As String In strFiles
                '    '            If IO.File.Exists(f) Then
                '    '                If IO.File.Exists(Application.StartupPath & "\BackupLayouts\" & f.Substring(f.LastIndexOf("\") + 1) & "") Then
                '    '                    IO.File.Delete(Application.StartupPath & "\BackupLayouts\" & f.Substring(f.LastIndexOf("\") + 1) & "")
                '    '                End If
                '    '                IO.File.Copy(f, Application.StartupPath & "\BackupLayouts\" & f.Substring(f.LastIndexOf("\") + 1) & "")
                '    '            End If
                '    '        Next
                '    '    End If
                '    'End If
                '    If IO.Directory.Exists(Application.StartupPath & "\Layouts") Then
                '        Dim strFiles() As String = IO.Directory.GetFiles(Application.StartupPath & "\Layouts", "*_*")
                '        If strFiles.Length > 0 Then
                '            For Each f As String In strFiles
                '                If IO.File.Exists(f) Then
                '                    IO.File.Delete(f)
                '                End If
                '            Next
                '        End If
                '    End If
                'Catch ex As Exception
                'End Try

            Else


                Try
                    'GetVersionInfo()
                    'If GetVersionInfo().Length > 0 Then Me.Text = Me.Text & " (" & _GetVersionInfo & ")" Else  : Me.Text = Me.Text
                Catch ex As Exception

                End Try


                'Try

                '    If flgCompanyRights = True Then
                '        Dim myComp As New SBModel.CompanyInfo
                '        myComp = CompanyList.Find(AddressOf GetMyCompany)
                '        Me.Text = Me.Text & " - " & myComp.CompanyName
                '    End If

                'Catch ex As Exception

                'End Try
                'Comment against R:M6 
                'If Not getConfigValueByType("DefaultReminder").ToString = "Error" Then
                '    Dim DefaultReminder As Integer = Val(Convert.ToInt32(getConfigValueByType("DefaultReminder").ToString))
                '    If DefaultReminder > 0 Then
                '        Timer2.Enabled = True
                '        Timer2.Interval = Val(DefaultReminder * 60000)
                '    Else
                '        Timer2.Enabled = False
                '    End If
                'Else
                '    Timer2.Enabled = False
                'End If

                Try
                    'Comment against R:M6
                    '-------------------------- get Path Employee Image/ --------------------------
                    'If BackgroundWorker4.IsBusy Then Exit Sub
                    'BackgroundWorker4.RunWorkerAsync()
                    'Do While BackgroundWorker4.IsBusy
                    '    Application.DoEvents()
                    'Loop


                    'If BackgroundWorker5.IsBusy Then Exit Sub
                    'BackgroundWorker5.RunWorkerAsync()
                    'Do While BackgroundWorker5.IsBusy
                    '    Application.DoEvents()
                    'Loop


                    _CurrentRegisterStatus = _CurrentRegisterStatus
                    Dim dt As New DataTable
                    If Not IO.File.Exists(str_ApplicationStartUpPath & "\AutoUpdate.Xml") = True Then
                        dt.TableName = "AutoUpdate"
                        dt.Columns.Add("EnableAutoUpdate", GetType(System.String))
                        Dim dr As DataRow
                        dr = dt.NewRow
                        dr(0) = False
                        dt.Rows.InsertAt(dr, 0)
                        dt.WriteXml(str_ApplicationStartUpPath & "\AutoUpdate.Xml")
                        dt.Dispose()
                    End If
                    'Comment against R:M6 
                    'Dim dtdailyTip As New DataTable
                    'dtdailyTip.TableName = "DailyTip"
                    'dtdailyTip.Columns.Add("StartupTip", GetType(System.Boolean))

                    'Dim drDailyTip As DataRow
                    'If Not IO.File.Exists(str_ApplicationStartUpPath & "\Startuptip.Xml") = True Then
                    '    drDailyTip = dtdailyTip.NewRow
                    '    drDailyTip(0) = True
                    '    dtdailyTip.Rows.InsertAt(drDailyTip, 0)
                    '    dtdailyTip.WriteXml(str_ApplicationStartUpPath & "\Startuptip.Xml")
                    '    ShowStartupTip = True
                    'Else
                    '    dtdailyTip.ReadXml(str_ApplicationStartUpPath & "\Startuptip.Xml")
                    '    If dtdailyTip IsNot Nothing Then
                    '        If dtdailyTip.Rows.Count > 0 Then
                    '            If dtdailyTip.Rows(0).Item(0) = True Then
                    '                ShowStartupTip = dtdailyTip.Rows(0).Item(0)
                    '            End If
                    '        End If
                    '    End If
                    'End If

                    'If Not GetConfigValue("WizardConfig").ToString = "Error" Then
                    '    Dim flag As Boolean = Convert.ToBoolean(GetConfigValue("WizardConfig").ToString)
                    '    If flag = False Then
                    '        frmWizardConfig.ShowDialog()
                    '    End If
                    'End If
                    'End R:M6

                Catch ex As Exception

                End Try


                'LoadControl("frmHome")
                'LoadControl("frmMainAccount")

                If BackgroundWorker6.IsBusy Then Exit Sub
                BackgroundWorker6.RunWorkerAsync()
                Do While BackgroundWorker6.IsBusy
                    Application.DoEvents()
                Loop

                Try




                    IsOpenMainForm = True
                    ''// LoadControl("frmHome")

                    'IsOpenMainForm = True
                    'LoadControl("frmHome")
                    'Comment Against R:M6
                    'If Not getConfigValueByType("Reminder").ToString = "Error" Then
                    '    flgReminder = Convert.ToBoolean(getConfigValueByType("Reminder").ToString)
                    'End If
                    'If flgReminder = True Then
                    '    Timer3.Enabled = True
                    '    Timer3.Interval = 100
                    'End If

                    'If ShowStartupTip = True Then
                    '    frmTodayTopic.ShowDialog()
                    '    Exit Sub
                    'End If

                    'End R:M6

                    If Convert.ToBoolean(getConfigValueByType("EnabledBrandedSMS").ToString) = True Then
                        Me.Timer5.Interval = 60000 * Convert.ToInt32(getConfigValueByType("SMSScheduleTime").ToString)
                        Me.Timer5.Enabled = True
                    End If

                    If Not getConfigValueByType("DefaultReminder").ToString = "Error" Then
                        Dim DefaultReminder As Integer = Val(Convert.ToInt32(getConfigValueByType("DefaultReminder").ToString))
                        If DefaultReminder > 0 Then
                            Timer2.Interval = DefaultReminder * 60000
                            Timer2.Enabled = True
                        Else
                            Timer2.Enabled = False
                        End If
                    Else
                        Timer2.Enabled = False
                    End If


                    Me.Timer6.Enabled = True
                    Me.Timer6.Interval = 100

                    Me.Timer7.Enabled = True
                    Me.Timer7.Interval = 100

                    If Not getConfigValueByType("Reminder").ToString = "Error" Then
                        flgReminder = Convert.ToBoolean(getConfigValueByType("Reminder").ToString)
                    End If
                    If flgReminder = True Then
                        Timer3.Enabled = True
                        Timer3.Interval = 1000
                    End If


                Catch ex As Exception

                End Try


                'Permission Date Lock By User
                If BackgroundWorker15.IsBusy Then Exit Sub
                BackgroundWorker15.RunWorkerAsync()
                Do While BackgroundWorker15.IsBusy
                    Application.DoEvents()
                Loop

                If BackgroundWorker17.IsBusy Then Exit Sub
                BackgroundWorker17.RunWorkerAsync()
                Do While BackgroundWorker17.IsBusy
                    Application.DoEvents()
                Loop


                If BackgroundWorker19.IsBusy Then Exit Sub
                BackgroundWorker19.RunWorkerAsync()
                Do While BackgroundWorker19.IsBusy
                    Application.DoEvents()
                Loop

                'Task#03082015 Set timer, checking my computer name with host (Host save in configuration) 
                If My.Computer.Name.ToUpper.ToString = getConfigValueByType("DNSHostForSMS").ToUpper.ToString Then
                    If getConfigValueByType("EmailAlertDueInvoice").ToString = "True" Then
                        Timer8.Interval = (5 * 60000)
                        Timer8.Enabled = True
                    End If
                End If

                If My.Computer.Name.ToUpper.ToString = getConfigValueByType("DNSHostForSMS").ToUpper.ToString Then
                    If getConfigValueByType("EnabledAttendanceEmailAlert").ToString = "True" Then


                        Timer9.Interval = (5 * 60000)
                        Timer9.Enabled = True
                    End If
                End If
                'End Task#03082015

                If BackgroundWorker21.IsBusy Then Exit Sub
                BackgroundWorker21.RunWorkerAsync()
                Do While BackgroundWorker21.IsBusy
                    Application.DoEvents()
                Loop

                If Me.BackgroundWorker22.IsBusy Then Exit Sub
                BackgroundWorker22.RunWorkerAsync()
                Do While BackgroundWorker22.IsBusy
                    Application.DoEvents()
                Loop



                EnableAutoBackup()

            End If
            'Timer10.Interval = 10000
            'Timer10.Enabled = True

            Try

                GetFavouriteFormsList()

            Catch ex As Exception
                msg_Error("Can't load favourite items: " & ex.Message)
            End Try

            Try

                'Dim NewNotifications As Integer = GetPendingNotificationsCount(LoginUserId)
                'If NewNotifications > 0 Then
                '    Me.btnNotification.Text = NewNotifications
                '    Me.btnNotification.BackColor = Color.Red
                '    btnNotification.Font = New Font(btnNotification.Font.FontFamily.Name, btnNotification.Font.Size, FontStyle.Bold)
                'Else
                '    Me.btnNotification.Text = String.Empty
                '    Me.btnNotification.BackColor = Color.Transparent
                '    btnNotification.Font = New Font(btnNotification.Font.FontFamily.Name, btnNotification.Font.Size, FontStyle.Regular)
                'End If
                ''TASK TFS1175
                If HideLogosIcons = True Then
                    Me.Icon = Nothing
                End If

                ''END TASK TFS1175
                frmNotification.TopLevel = False
                Me.pnlNotification.Controls.Add(frmNotification)
                frmNotification.Dock = DockStyle.Fill
                frmNotification.Show()
                'pnlNotification.Visible = True
                'pnlNotification.BringToFront()

                'frmReleaseDownload.Visible = False
                'frmReleaseDownload.Show()

                If gblnTrialVersion Then

                    msg_Error("You are currently using trial version of Agrius ERP. Please update your license before 31-Dec-2016. For more information you can reach us at www.Agrius.net", 60)

                End If
                LogosDBKeyExists()
            Catch ex As Exception
                ShowErrorMessage("Unable to check notifications: " & ex.Message)
            End Try

            If Val(GetConfigValue("MinimumApplicationVersion").ToString) > 3048 AndAlso My.Application.Info.Version.ToString.Replace(".", "") < Val(GetConfigValue("MinimumApplicationVersion").ToString) Then
                msg_Error("Your Agrius version is not compatible with server. Please update before you continue your work.")
            End If

            'Disable Property Menu
            'If ModGlobel.getConfigValueByType("PropertyTabEnabled").ToString() = "True" Then
            '    Me.UltraToolbarsManager1.Tools("Property").SharedProps.Visible = True
            'Else
            '    Me.UltraToolbarsManager1.Tools("Property").SharedProps.Visible = False
            'End If

        Catch ex As Exception
            msg_Error("Error occured while starting system: " & ex.Message)
        End Try
    End Sub
    Sub FillLocationCombo()
        '  FillDropDown(Me.cmbLocation.ComboBox, "select location_id,location_code from tbldeflocation", False)
        gobjLocationId = 1
    End Sub

    Private Sub LoadTree()
        ' TODO: Add code to add items to the treeview

        'Dim tvRoot As TreeNode
        'Dim tvNode As TreeNode

        'tvRoot = Me.TreeView.Nodes.Add("Root")
        'tvNode = tvRoot.Nodes.Add("TreeItem1")
        'tvNode = tvRoot.Nodes.Add("TreeItem2")
        'tvNode = tvRoot.Nodes.Add("TreeItem3")
    End Sub

    Private Sub LoadListView()
        ' TODO: Add code to add items to the listview based on the selected item in the treeview

        'Dim lvItem As ListViewItem
        'ListView.Items.Clear()

        'lvItem = ListView.Items.Add("ListViewItem1")
        'lvItem.SubItems.AddRange(New String() {"Column2", "Column3"})

        'lvItem = ListView.Items.Add("ListViewItem2")
        'lvItem.SubItems.AddRange(New String() {"Column2", "Column3"})

        'lvItem = ListView.Items.Add("ListViewItem3")
        'lvItem.SubItems.AddRange(New String() {"Column2", "Column3"})
    End Sub

    Private Sub SetUpListViewColumns()
        ' TODO: Add code to set up listview columns
        'ListView.Columns.Add("Column1")
        'ListView.Columns.Add("Column2")
        'ListView.Columns.Add("Column3")
        'SetView(View.Details)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Exit the application
            Global.System.Windows.Forms.Application.Exit()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub SetView(ByVal View As System.Windows.Forms.View)
        'Figure out which menu item should be checked
        Dim MenuItemToCheck As ToolStripMenuItem = Nothing
        Select Case View
            Case View.Details
                MenuItemToCheck = DetailsToolStripMenuItem
            Case View.LargeIcon
                MenuItemToCheck = LargeIconsToolStripMenuItem
            Case View.List
                MenuItemToCheck = ListToolStripMenuItem
            Case View.SmallIcon
                MenuItemToCheck = SmallIconsToolStripMenuItem
            Case View.Tile
                MenuItemToCheck = TileToolStripMenuItem
            Case Else
                Debug.Fail("Unexpected View")
                View = View.Details
                MenuItemToCheck = DetailsToolStripMenuItem
        End Select

        'Check the appropriate menu item and deselect all others under the Views menu
        For Each MenuItem As ToolStripMenuItem In ListViewToolStripButton.DropDownItems
            If MenuItem Is MenuItemToCheck Then
                MenuItem.Checked = True
            Else
                MenuItem.Checked = False
            End If
        Next

        'Finally, set the view requested
        ' ListView.View = View
    End Sub

    Private Sub ListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListToolStripMenuItem.Click
        SetView(View.List)
    End Sub

    Private Sub DetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DetailsToolStripMenuItem.Click
        SetView(View.Details)
    End Sub

    Private Sub LargeIconsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LargeIconsToolStripMenuItem.Click
        SetView(View.LargeIcon)
    End Sub

    Private Sub SmallIconsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SmallIconsToolStripMenuItem.Click
        SetView(View.SmallIcon)
    End Sub

    Private Sub TileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TileToolStripMenuItem.Click
        SetView(View.Tile)
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt"
        OpenFileDialog.ShowDialog(Me)

        Dim FileName As String = OpenFileDialog.FileName
        ' TODO: Add code to open the file
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt"
        SaveFileDialog.ShowDialog(Me)
        Dim FileName As String = SaveFileDialog.FileName
        ' TODO: Add code here to save the current contents of the form to a file.
    End Sub

    Private Sub TreeView_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
        ' TODO: Add code to change the listview contents based on the currently-selected node of the treeview
        LoadListView()
    End Sub
    Private Sub UltraExplorerBar1_ItemClick(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinExplorerBar.ItemEventArgs) Handles UltraExplorerBar2.ItemClick
        Dim strToolTip As String = String.Empty
        strToolTip = e.Item.ToolTipText
        e.Item.ToolTipText = "Loading please wait..."
        Try
            Tags = String.Empty
            LoadControl(e.Item.Key) 'Me.UltraExplorerBar1.ActiveItem.Key)
            e.Item.ToolTipText = strToolTip
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    '' ReqId-918 Filter View Rights
    '' ReqId-912 Added Key frmCompanyList

    Public Sub LoadControl(ByVal key As String)

        Me.pnlNotification.Visible = False

        If ControlName.Text.Length > 0 Then
            Me.LastControlName = ControlName
            'rafay
            Me.BackToolstripButtom.Enabled = True
            Me.Lastenum = Me.enm
        Else
            Me.BackToolstripButtom.Enabled = False
        End If
        'enm = EnumForms.Non
        ' With UltraExplorerBar1.ActiveItem
        'Forms

        enm = EnumForms.Non

        If LicenseExpiryType = "Monthly" Or gblnTrialVersion Then

            'Added by syed Irfan Ahmad on 19 Feb 2018, Task 2411
            If LicenseStatus = "Blocked" Then
                Dim gm As New AgriusMessage
                gm.Message = "License status of Agrius is Blocked please contact Agrius Support for more details."
                gm.ErrorCode = "GEC-LIC-0x007-1122"
                ModGlobel.AgriusMessageLogger.Log(gm)

                msg_Error("License status of Agrius is Blocked please contact Agrius Support for more details." & vbCrLf & vbCrLf & "Error Code: GEC-LIC-0x007-1122", 120)
                key = "Blocked"
                ControlName = frmaboutus
            ElseIf LicenseStatus = "Expired" Then
                msg_Information("License of Agrius is expired please contact Agrius for more details", 120)
            End If

        End If


        If key = "Users" Then
            ControlName = frmDefSecurityUser
            enm = EnumForms.Non
        ElseIf key = "GroupRights" Then
            ControlName = FrmGroupRights
            enm = EnumForms.Non
        End If

        If key = "Exit" Then
            Me.Close()
        End If

        If key = "frmMainAccount" Then

            ControlName = DefMainAcc
            enm = EnumForms.DefMainAcc
            If Tags.Length > 0 Then
                DefMainAcc.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

            ''22-Sep-2014 TAKS:2850 Imran Ali Department And Category Wise Purchase Report
            'Altered By Ali Ansari against Task#20150510 
            ''add missing documents form 
            'VoucherPost
            'Rafay:add back button in quick access toolbar
        ElseIf key = "Back" Then
            Try
                'If ControlName.Text.Length > 0 Then
                '    Me.LastControlName = ControlName
                '    'rafay
                '    Me..Enabled = True
                '    Me.Lastenum = Me.enm
                'Else
                '    Me.BackToolstripButtom.Enabled = False
                'End If

                'If ControlName.Text.Length > 0 Then
                '    Me.LastControlName = ControlName
                '    'rafay
                '    Me.ToolStripButton1.Enabled = True
                '    Me.Lastenum = Me.enm
                'Else
                '    Me.ToolStrip.Enabled = False
                'End If

                ''If ControlName.Text.Length > 0 Then
                '    Me.LastControlName = ControlName
                'End If

                'Me.NextControlName = ControlName
                'Me.Nextenm = Me.enm
                '' Me.ForwardToolStripButton.Enabled = True
                ' ''Me.BackToolStripButton.Enabled = False

                'LastControlName.TopLevel = False
                'LastControlName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                'LastControlName.Dock = DockStyle.Fill
                'Me.SplitContainer.Panel2.Controls.Add(LastControlName)
                'If Me.Lastenum <> EnumForms.Non Then
                '    If GetFormRights(Me.Lastenum).Rows(0).Item("View_Rights") = True Then
                '        LastControlName.Show()
                '        LastControlName.BringToFront()
                '    Else
                '        msg_Information(str_ErrorViewRight)
                '    End If
                'Else
                '    LastControlName.Show()
                '    LastControlName.BringToFront()
                'End If


                If arHistory.Count > 1 Then

                    arHistory.RemoveAt(0)
                    LoadControl(arHistory.Item(0).ToString)
                End If
                ''If arHistory.Count > 0 Then
                '    If Not arHistory.Item(0).ToString = key.ToString Then
                '        arHistory.Insert(0, key.ToString)
                '    End If
                'Else
                '    arHistory.Insert(0, key.ToString)
                'End If

                'If BackToolStripButton.DropDownItems.Count > 0 Then

                '    LoadControl(BackToolStripButton.DropDownItems(1).Tag.ToString)

                'Else

                '    ToolStripButton1_Click(Me, Nothing)

                'End If

            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
        ElseIf key = "frmInvoiceWiseProfitReport" Then
            ControlName = frmInvoiceWiseProfitReport
        ElseIf key = "frmGrdStockMovement" Then
            ControlName = frmGrdStockMovement
        ElseIf key = "InvoiceDueReport" Then
            ControlName = frmInvoiceDueReport
        ElseIf key = "frmAssetsAndLiabilityReport" Then
            ControlName = frmAssetsAndLiabilityReport
        ElseIf key = "frmProjectList" Then
            ControlName = frmProjectList
        ElseIf key = "frmDepriciation" Then
            ControlName = frmDepriciation
        ElseIf key = "frmOpportunityChart" Then
            ControlName = frmOpportunityChart
        ElseIf key = "OpportunityQuarterlyChart" Then
            ControlName = OpportunityQuartelyChart
        ElseIf key = "frmApprovalRejectionReason" Then
            ControlName = frmApprovalRejectionReason
        ElseIf key = "frmApprovalLog" Then
            ControlName = frmApprovalLog
        ElseIf key = "frmApprovalStagesMapping" Then
            ControlName = frmApprovalStagesMapping
        ElseIf key = "frmTicketDetail" Then
            ControlName = frmTicketDetail
        ElseIf key = "frmCentralizeOpportunity" Then
            ControlName = frmCentralizeOpportunity
        ElseIf key = "frmCentralizeContract" Then
            ControlName = frmCentralizeContract

        ElseIf key = "frmApprovalProcess" Then
            ControlName = frmApprovalProcess
        ElseIf key = "frmApprovalStages" Then
            ControlName = frmApprovalStages
        ElseIf key = "frmActivityHistory" Then
            ControlName = frmActivityHistory
        ElseIf key = "frmGrdRptSaleOrderStatusSummary" Then
            ControlName = frmGrdRptSaleOrderStatusSummary
        ElseIf key = "frmPartialReceiptGatePass" Then
            ControlName = frmPartialReceiptGatePass
        ElseIf key = "frmDefBudget" Then
            ControlName = frmDefBudget
            'ElseIf key = "frmDefBudgetList" Then
            '    ControlName = frmDefBudgetList
        ElseIf key = "frmAcctualVsBudgetedCategoryWisePL" Then
            ControlName = frmAcctualVsBudgetedCategoryWisePL
        ElseIf key = "frmAcctualVsBudgetedPLReport" Then
            ControlName = frmAcctualVsBudgetedPLReport
        ElseIf key = "frmConfigMain" Then
            ControlName = frmConfigMain
        ElseIf key = "frmInvoiceAgingNew" Then
            ControlName = frmInvoiceAgingNew
        ElseIf key = "frmLeadProfileList2" Then
            ControlName = frmLeadProfileList2
            'Add by murtaza for CRM CustomerDetailReport 
        ElseIf key = "CustomerDetailReports" Then
            ControlName = frmLeadCustomerDetailReport
            'Add by murtaza for CRM CustomerDetailReport 
        ElseIf key = "frmOpportunityList" Then
            ControlName = frmOpportunityList
        ElseIf key = "frmOpportunityList1" Then
            ControlName = frmOpportunityList


            'Menus added on 24 Feb 2018 -------------- Start
        ElseIf key = "frmLeadProfileList" Then
            ControlName = frmLeadProfileList
        ElseIf key = "frmPlanNewActivity" Then
            ControlName = frmPlanNewActivity
        ElseIf key = "frmActivityPlanList" Then
            ControlName = frmActivityPlanList
        ElseIf key = "frmActivityPlanList 1" Then
            ControlName = frmActivityPlanList
        ElseIf key = "frmActivityFeedback" Then
            ControlName = frmActivityFeedback
        ElseIf key = "frmActivityCalender" Then
            ControlName = frmActivityCalender
        ElseIf key = "frmMissedVisitGraph" Then
            ControlName = frmMissedVisitGraph
        ElseIf key = "frmMissedVisitApproval" Then
            ControlName = frmMissedVisitApproval
        ElseIf key = "frmProjectList" Then
            ControlName = frmProjectList
            'Menus added on 21 Feb 2018 -------------- End

            'Menu added on 12 April 2018 ----- Start
        ElseIf key = "ProductionOrder" Then
            ControlName = frmProductionOrderList
            'Menu added on 12 April 2018 ----- End
        ElseIf key = "ProductionOrder1" Then
            ControlName = frmProductionOrder
            'Rafay: added on 1 October because new screen (production report) will be added
        ElseIf key = "frmProduction" Then
            ControlName = frmProduction


            'Menus added on 21 Feb 2018 -------------- Start
        ElseIf key = "frmProductionPlanningStandard" Then
            ControlName = frmProductionPlanningStandard
        ElseIf key = "frmFinishGoodStandard" Then
            ControlName = frmFinishGoodStandard
        ElseIf key = "frmCloseBatch" Then
            ControlName = frmCloseBatch
        ElseIf key = "frmPlanTicketStandard" Then
            ControlName = frmPlanTicketStandard
        ElseIf key = "frmFixedAssetCategory" Then
            ControlName = frmFixedAssetCategory
            'Menus added on 21 Feb 2018 -------------- End
        ElseIf key = "frmApprovalStages" Then
            ControlName = frmApprovalStages

        ElseIf key = "frmApprovalProcess" Then
            ControlName = frmApprovalProcess

        ElseIf key = "frmApprovalRejectionReason" Then
            ControlName = frmApprovalRejectionReason

        ElseIf key = "frmApprovalStagesMapping" Then
            ControlName = frmApprovalStagesMapping


            'Menus added on 19 Feb 2018 -------------- Start
        ElseIf key = "ChartOfAccountGroups" Then
            ControlName = ChartOfAccountGroups
        ElseIf key = "COAGroupsToAccountsMapping" Then
            ControlName = frmCOAGroupsToAccountsMapping
        ElseIf key = "frmCOAGroupsToUserMapping" Then
            ControlName = frmCOAGroupsToUserMapping

            'Menus added on 19 Feb 2018 -------------- End


            'Menu for propoerty module (10 Feb 2018) ---- START
        ElseIf key = "frmProItemList" Then
            ControlName = frmProItemList
        ElseIf key = "frmPropertyProfileList" Then
            ControlName = frmPropertyProfileList
        ElseIf key = "frmProPurchaseList" Then
            ControlName = frmProPurchaseList
        ElseIf key = "frmProInvestorList" Then
            ControlName = frmProInvestorList
        ElseIf key = "frmProEstateList" Then
            ControlName = frmProEstateList
        ElseIf key = "frmProAgentList" Then
            ControlName = frmProAgentList
        ElseIf key = "frmProOfficeList" Then
            ControlName = frmProOfficeList
        ElseIf key = "frmProBranchList" Then
            ControlName = frmProBranchList
        ElseIf key = "frmProDealerList" Then
            ControlName = frmProDealerList
        ElseIf key = "frmProSalaryList" Then
            ControlName = frmProSalaryList
        ElseIf key = "frmProSalesList" Then
            ControlName = frmProSalesList

            'Menu for propoerty module (10 Feb 2018) ---- END

        ElseIf key = "frmRevenueDataImport" Then
            ControlName = frmRevenueDataImport
        ElseIf key = "frmLoadExcelFile" Then
            ControlName = frmLoadExcelFile
            'ElseIf key = "frmBudgetDefinition" Then
            '    ControlName = frmBudgetDefinition
        ElseIf key = "frmBSandPLTemplateMapping" Then
            ControlName = frmBSandPLTemplateMapping
        ElseIf key = "frmProductionProcess" Then
            ControlName = frmProductionProcess
        ElseIf key = "frmLabourType" Then
            ControlName = frmLabourType
        ElseIf key = "frmCustomerBottomSaleRate" Then
            ControlName = frmCustomerBottomSaleRate
        ElseIf key = "frmBSAccountGroupWiseSummary" Then
            ControlName = frmBSAccountGroupWiseSummary
        ElseIf key = "frmPLAccountGroupWiseSummary" Then
            ControlName = frmPLAccountGroupWiseSummary
        ElseIf key = "frmBSSubSubAccountSummary" Then
            ControlName = frmBSSubSubAccountSummary
        ElseIf key = "frmPLSubSubAccountCostCenterWiseSummary" Then
            ControlName = frmPLSubSubAccountCostCenterWiseSummary
        ElseIf key = "frmPLSubSubAccountWiseSummary" Then
            ControlName = frmPLSubSubAccountWiseSummary
        ElseIf key = "frmAgingPayablesNew" Then
            ControlName = frmAgingPayablesNew
        ElseIf key = "frmBankTypeWiseCashFlow" Then
            ControlName = frmBankTypeWiseCashFlow
        ElseIf key = "frmBSandPLReports" Then
            ControlName = frmBSandPLReports
        ElseIf key = "frmAdvanceType" Then
            ControlName = frmAdvanceType
        ElseIf key = "frmRptAccountWisePurchaseReport" Then
            ControlName = frmRptAccountWisePurchaseReport
        ElseIf key = "frmCustomerTypeWisePriceList" Then
            ControlName = frmCustomerTypeWisePriceList
        ElseIf key = "frmRptInvoiceAging" Then
            ControlName = frmRptInvoiceAging
        ElseIf key = "frmPOSEntry" Then
            Dim str As String
            str = "IF EXISTS(SELECT POSId FROM tblUserPOSRights WHERE UserID = " & LoginUserId & ") SELECT POSId, POSTitle FROM tblPOSConfiguration WHERE POSId IN (SELECT POSId FROM tblUserPOSRights WHERE UserID = " & LoginUserId & ") AND Active = 1 ORDER BY POSId ELSE SELECT POSId, POSTitle FROM tblPOSConfiguration WHERE Active = 1 ORDER BY POSId"
            Dim dt As DataTable
            dt = GetDataTable(str)
            If dt.Rows.Count > 1 Then
                Dim frmposlist As New frmPOSList(dt)
                frmposlist.ShowDialog()
                If frmposlist.ReturnIs = True Then
                    ControlName = frmPOSEntry
                Else
                    Exit Sub
                End If
            Else
                If dt.Rows.Count = 0 Then
                    ShowErrorMessage("You don't have rights of any POS")
                Else
                    Dim str1 As String = "SELECT POSTitle, CompanyId, LocationId, CostCenterId, CashAccountId, BankAccountId, SalesPersonId, DeliveryOption FROM tblPOSConfiguration where POSId = '" & dt.Rows(0).Item("POSId") & "'"
                    Dim dt1 As DataTable
                    dt1 = GetDataTable(str1)
                    If dt1 IsNot Nothing Then
                        frmPOSEntry.Title = dt1.Rows(0).Item("POSTitle")
                        frmPOSEntry.CID = dt1.Rows(0).Item("CompanyId")
                        frmPOSEntry.LID = dt1.Rows(0).Item("LocationId")
                        frmPOSEntry.CCID = dt1.Rows(0).Item("CostCenterId")
                        frmPOSEntry.CAID = dt1.Rows(0).Item("CashAccountId")
                        frmPOSEntry.BAID = dt1.Rows(0).Item("BankAccountId")
                        frmPOSEntry.SPID = dt1.Rows(0).Item("SalesPersonId")
                        frmPOSEntry.DevOption = dt1.Rows(0).Item("DeliveryOption")
                    End If
                    ControlName = frmPOSEntry
                End If
            End If
        ElseIf key = "frmPOSConfiguration" Then
            ControlName = frmPOSConfiguration
        ElseIf key = "frmSkippingSalesInvoicesNumbers" Then
            ControlName = frmSkippingSalesInvoicesNumbers
        ElseIf key = "frmEmployeeWiseLedger" Then
            ControlName = frmEmployeeWiseLedger
        ElseIf key = "frmRptProductionBasedSalary" Then
            ControlName = frmRptProductionBasedSalary
        ElseIf key = "frmRptPurchaseGRNRejectedQty" Then
            ControlName = frmRptPurchaseGRNRejectedQty
        ElseIf key = "frmGRNDetailReport" Then
            ControlName = frmGRNDetailReport
        ElseIf key = "frmMaterialDecomposition" Then
            ControlName = frmMaterialDecomposition
        ElseIf key = "frmPurchaseAdjustmentVoucher" Then
            ControlName = frmPurchaseAdjustmentVoucher
        ElseIf key = "frmIncomeTaxOrSalesTaxAccount" Then
            ControlName = frmIncomeTaxOrSalesTaxAccount
        ElseIf key = "frmEmployeeBirthday" Then
            ControlName = frmEmployeeBirthday
        ElseIf key = "frmSalesInquiryApproval" Then
            ControlName = frmSalesInquiryApproval
        ElseIf key = "frmSalesAdjustmentVoucher" Then
            ControlName = frmSalesAdjustmentVoucher
        ElseIf key = "frmRepeateCustomerReport" Then
            ControlName = frmRepeateCustomerReport
        ElseIf key = "frmEmployeeCNICExpiry" Then
            ControlName = frmEmployeeCNICExpiry
        ElseIf key = "frmEmployeeVisitPlan" Then
            ControlName = frmEmployeeVisitPlan
        ElseIf key = "frmEmployeeVisitPlanEntry" Then
            ControlName = frmEmployeeVisitPlanEntry
        ElseIf key = "frmGrdRptToOrderQty" Then
            ControlName = frmGrdRptToOrderQty
        ElseIf key = "frmShiftChangeRequest" Then
            ControlName = frmShiftChangeRequest
        ElseIf key = "frmEmployeeNoOfHits" Then
            ControlName = frmEmployeeNoOfHits
        ElseIf key = "frmEmpAttendenceInOutMissing" Then
            ControlName = frmEmpAttendenceInOutMissing
        ElseIf key = "frmRptPlansStatus" Then
            ControlName = frmRptPlansStatus
        ElseIf key = "frmInventoryColumnStrings" Then
            ControlName = frmInventoryColumnStrings
        ElseIf key = "frmEmployeeTermination" Then
            ControlName = frmEmployeeTermination
        ElseIf key = "frmEmployeeWarning" Then
            ControlName = frmEmployeeWarning
        ElseIf key = "frmFineDeduction" Then
            ControlName = frmFineDeduction
        ElseIf key = "frmNewLeaveApplication" Then
            ControlName = frmNewLeaveApplication
        ElseIf key = "frmApproveLeaveApplication" Then
            ControlName = frmApproveLeaveApplication
        ElseIf key = "frmLeaveApplication" Then
            ControlName = frmLeaveApplication
        ElseIf key = "frmLeaveAdjustment" Then
            ControlName = frmLeaveAdjustment
        ElseIf key = "frmDefLeaveTypes" Then
            ControlName = frmDefLeaveTypes
        ElseIf key = "frmItemWiseProgressUpto" Then
            ControlName = frmItemWiseProgressUpto
        ElseIf key = "LateArrivalDays" Then
            ControlName = LateArrivalDays
        ElseIf key = "LateInTimeSummary" Then
            ControlName = LateInTimeSummary
        ElseIf key = "SummaryofPurchasesAndReturns" Then
            ControlName = SummaryofPurchasesAndReturns
        ElseIf key = "frmApplySalePriceUtility" Then
            ControlName = frmApplySalePriceUtility
        ElseIf key = "frmRptDailyWorkingReport" Then
            ControlName = frmRptDailyWorkingReport
        ElseIf key = "frmCashInLeaveReport" Then
            ControlName = frmCashInLeaveReport
        ElseIf key = "frmEmployeeStatusList" Then
            ControlName = frmEmployeeStatusList
            enm = EnumForms.frmEmployeeStatusList
        ElseIf key = "frmGrdRptAttendanceRegisterUpdate" Then
            ControlName = frmGrdRptAttendanceRegisterUpdate
            enm = EnumForms.frmGrdRptAttendanceRegisterUpdate
        ElseIf key = "frmSOArticleAliasPendingMapping" Then
            ControlName = frmSOArticleAliasPendingMapping
        ElseIf key = "VendorQuotation" Then
            ControlName = frmVendorQuotation
            ''Start TFS2648
            enm = EnumForms.frmVendorQuotation
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            If Tags.Length > 0 Then
                frmVendorQuotation.Get_All(Tags)
                'Tags = String.Empty
            End If
            ''End TFS2648
        ElseIf key = "frmInquiryComparisonStatement" Then
            ControlName = frmInquiryComparisonStatement
        ElseIf key = "frmPurchaseInquiry" Then
            ControlName = frmPurchaseInquiry
            ''Start TFS2648
            enm = EnumForms.frmPurchaseInquiry
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            If Tags.Length > 0 Then
                frmPurchaseInquiry.Get_All(Tags)
                'Tags = String.Empty
            End If
        ElseIf key = "frmSalesInquiry" Then
            ControlName = frmSalesInquiry
        ElseIf key = "frmSalesInquiryRights" Then
            ControlName = frmSalesInquiryRights
        ElseIf key = "CompanyLocations" Then
            ControlName = frmCompanyLocations
        ElseIf key = "frmNotes" Then
            ControlName = Notes
        ElseIf key = "rptGridItemWiseLC" Then
            ControlName = rptGridItemWiseLC
        ElseIf key = "frmUserRightsList" Then
            ControlName = frmUserRightsList
        ElseIf key = "frmPlanTickets" Then
            ControlName = frmPlanTickets
        ElseIf key = "frmGrdArticleLedgerByPack" Then
            ControlName = frmGrdArticleLedgerByPack
            enm = EnumForms.frmGrdArticleLedgerByPack
        ElseIf key = "frmLabTestRequest" Then
            ControlName = frmLabTestRequest
            'ElseIf key = "frmMaterialEstimation" Then
            '    ControlName = frmMaterialEstimation
        ElseIf key = "frmMaterialEstimation" Then
            Dim ConfigValue As String = getConfigValueByType("CostSheetType")
            If ConfigValue = "Error" Or ConfigValue = "Standard Cost Sheet" Then
                ControlName = frmMaterialEstimationPrior
            Else
                ControlName = frmMaterialEstimation
            End If
        ElseIf key = "frmIssuanceConsumptionReport" Then
            ControlName = frmIssuanceConsumptionReport
        ElseIf key = "frmDepartmentWiseProduction" Then
            ControlName = frmDepartmentWiseProduction
        ElseIf key = "frmMaterialAllocation" Then
            ControlName = frmMaterialAllocation
        ElseIf key = "frmTicketTracking" Then
            ControlName = frmTicketTracking
        ElseIf key = "MaterialAnalysis" Then
            ControlName = MaterialAnalysis
        ElseIf key = "frmnewLC" Then
            ControlName = frmLetterofCredit
        ElseIf key = "frmObservationSample" Then
            ControlName = frmObservationSample
        ElseIf key = "frmParameterConfig" Then
            ControlName = frmParameterConfig
        ElseIf key = "frmResult" Then
            ControlName = frmResult
            '22-June-2017 TFS# 981 : Ali Faisal : Add new form of Service Item Task for 3G Constructions Project management 
        ElseIf key = "frmServiceItemTask" Then
            ControlName = frmServiceItemTask
            enm = EnumForms.frmServiceItemTask
        ElseIf key = "frmItemTaskProgress" Then
            ControlName = frmItemTaskProgress
        ElseIf key = "frmVendorContract" Then
            ControlName = frmVendorContract
        ElseIf key = "frmItemProgressReport" Then
            ControlName = frmItemProgressReport
        ElseIf key = "frmProjectProgressApproval" Then
            ControlName = frmProjectProgressApproval
            '22-June-2017 TFS# 981 : Ali Faisal : End
        ElseIf key = "frmGrdRptEngineWiseStock" Then
            ControlName = frmGrdRptEngineWiseStock
        ElseIf key = "frmReconciliation" Then
            ControlName = frmReconciliation
        ElseIf key = "frmLeaveApplication" Then
            ControlName = frmLeaveApplication
        ElseIf key = "UpdateCurrencyRates" Then
            ControlName = frmUpdateCurrency
            'ProductionProcessing
        ElseIf key = "frmgrdrptcostsheetplandetail" Then
            ControlName = frmGrdRptCostSheetPlanDetail
            'ElseIf key = "frmGrdRptCOADetail" Then
            '    ControlName = frmGrdRptCOADetail
            ''TASK-470
        ElseIf key = "frmLiftWisePercentageReport" Then
            ControlName = frmLiftWisePercentageReport
        ElseIf key = "frmLiftWiseDetailReport" Then
            ControlName = frmLiftWiseDetailReport
        ElseIf key = "frmLiftWiseBusinessReport" Then
            ControlName = frmLiftWiseBusinessReport
        ElseIf key = "frmFreeServiceCardReport" Then
            ControlName = frmFreeServiceCardReport
        ElseIf key = "GroupWiseSalesReport" Then
            ControlName = GroupWiseSalesReport
        ElseIf key = "JobCardCommissionReport" Then
            ControlName = JobCardCommissionReport
        ElseIf key = "JobCardSalesReport" Then
            ControlName = JobCardSalesReport
        ElseIf key = "frmRepeateCustomerReport" Then
            ControlName = frmRepeateCustomerReport
        ElseIf key = "StockStatementbyPack" Then
            ControlName = frmRptGrdStockStatementByPack
            enm = EnumForms.frmRptGrdStockStatementByPack
        ElseIf key = "frmGrdRptClosingStockByGRNnDC" Then
            ControlName = frmGrdRptClosingStockByGRNnDC
            enm = EnumForms.frmGrdRptClosingStockByGRNnDC
            'TFS1011 : Muhammad Ameen : Add Report
        ElseIf key = "frmConsumptionEstimationReport" Then
            ControlName = frmConsumptionEstimationReport
            'TFS1011 : End
        ElseIf key = "frmGrdRptCustomerCashRecovery" Then
            ControlName = frmGrdRptCustomerWiseCashRecovery
        ElseIf key = "frmGrdRptRackWiseClosingStock" Then
            ControlName = frmGrdRptRackWiseClosingStock
        ElseIf key = "frmRptServicesStockLedger" Then
            'applystylesheet(frmRptServicesStockLedger)
            frmRptServicesStockLedger.ShowDialog()
            Exit Sub
        ElseIf key = "EmployeeOverTimeReport" Then
            'applystylesheet(frmEmployeeOverTimeReport)
            frmEmployeeOverTimeReport.ShowDialog()
            Exit Sub
        ElseIf key = "LateComingEmployee" Then
            ControlName = LateComingEmployee
        ElseIf key = "OverTimeEmployee" Then
            ControlName = OverTimeEmployee
        ElseIf key = "frmLocationWiseStockReport" Then
            ControlName = frmLocationWiseStockReport
        ElseIf key = "frmImportDetailReport" Then
            ControlName = frmImportDetailReport
        ElseIf key = "frmLCOutstandingDetailReport" Then
            ControlName = frmLCOutstandingDetailReport
            'ClosingStockByOrder
        ElseIf key = "ModelList" Then
            ControlName = frmModelList
            'applystylesheet(frmModelList)
            enm = EnumForms.frmModelList
            'frmModelList.ShowDialog()
            'Exit Sub
            'ClosingStockByOrder
        ElseIf key = "frmDefTermAndConditions" Then
            'ControlName = frmDefTermAndConditions
        ElseIf key = "frmGrdRptRackWiseClosingStock" Then
            ControlName = frmGrdRptRackWiseClosingStock
        ElseIf key = "frmInstallment" Then
            ControlName = frmInstallment
        ElseIf key = "frmGrdRptInstallmentBalance" Then
            ControlName = frmGrdRptInstallmentBalance
        ElseIf key = "frmGrdRptAttendanceRegister" Then
            ControlName = frmGrdRptAttendanceRegister
        ElseIf key = "frmLiftAssociation" Then
            ControlName = frmLiftAssociation
        ElseIf key = "frmRptServicesReports" Then
            'applystylesheet(frmRptServicesReports)
            frmRptServicesReports.ShowDialog()
            Exit Sub
        ElseIf key = "frmRptProductionReport" Then
            'applystylesheet(frmRptServicesProduction)
            frmRptServicesProduction.ShowDialog()
            Exit Sub
        ElseIf key = "rptTodayTasks" Then
            'applystylesheet(rptTodayTasks)
            rptTodayTasks.ShowDialog()
            Exit Sub
        ElseIf key = "frmTerminalConfiguration" Then
            ControlName = frmTerminalConfiguration
        ElseIf key = "frmDefEmployeeMonthlyTarget" Then
            ControlName = frmDefEmployeeMonthlyTarget
        ElseIf key = "ProductionDispatch" Then
            ControlName = frmServicesDispatch
        ElseIf key = "frmGrdRptCustomerWiseCashRecovery" Then
            ControlName = frmGrdRptCustomerWiseCashRecovery
        ElseIf key = "ServicesInvoice" Then
            ControlName = frmServicesInvoices

        ElseIf key = "ProductionProcessing" Then
            ControlName = frmServicesProduction

        ElseIf key = "LoanApprovalList" Then
            ControlName = frmGrdRptLoanApprovalList
        ElseIf key = "rptDuplicateDocuments" Then
            ControlName = frmGrdRptDuplicateDocuments
        ElseIf key = "frmInwardGatePass" Then
            ControlName = frmIGP
        ElseIf key = "frmWorkInProcess" Then
            ControlName = frmWIP
        ElseIf key = "VoucherPost" Then
            ControlName = frmVoucherPost
        ElseIf key = "frmDefGroupVoucherApproval" Then
            ControlName = frmDefGroupVoucherApproval
            enm = EnumForms.frmDefGroupVoucherApproval
        ElseIf key = "HolidaySetup" Then
            ControlName = frmHolidySetup
            enm = EnumForms.frmHolidySetup
        ElseIf key = "HolidaySetup1" Then
            ControlName = frmHolidySetup
            enm = EnumForms.frmHolidySetup
            'Ali 
        ElseIf key = "TaxSlabs" Then
            ControlName = frmDefTaxSlabs

            'Task#17082015 Add link for Employee Site Visit Charges (Ahmad Sharif)
        ElseIf key = "EmployeeSiteVisitCharges" Then
            ControlName = FrmEmployeeSiteVisitCharges

        ElseIf key = "EmployeeNoofSiteVisits" Then
            ControlName = FrmEmployeeSiteVisit
            'End Task#17082015
        ElseIf key = "frmGRNStatus" Then
            ControlName = frmGRNStatus
        ElseIf key = "frmDeliveryChalanStatus" Then
            ControlName = frmDeliveryChalanStatus
        ElseIf key = "frmSalesInquiryStatus" Then
            ControlName = frmSalesInquiryStatus
        ElseIf key = "CustomerMonthlyTarget" Then
            ControlName = frmCustomerRecoveryTarget
        ElseIf key = "Installment" Then
            ControlName = frmInstallment
        ElseIf key = "ProjectVisit" Then
            ControlName = frmGrdRptProjectVisitDetail
        ElseIf key = "frmProductionProcessing" Then
            ControlName = frmProductionProcessing
        ElseIf key = "TroubleShoot" Then
            ControlName = frmTroubleshoot
            'Altered By Ali Ansari against Task#20150510 
        ElseIf key = "frmDateLockPermission" Then
            ControlName = frmDateLockPermission
        ElseIf key = "frmRptProjectHistory" Then
            'applystylesheet(frmRptProjectHistory)
            frmRptProjectHistory.ShowDialog()
            frmRptProjectHistory.BringToFront()
            Exit Sub
            'Altered By Ali Ansari against Task#20150618 Add Partner Form
        ElseIf key = "Partner" Then
            ControlName = frmDefPartners
            'Altered By Ali Ansari against Task#20150618 Add Partner Form
        ElseIf key = "ProjectPortfolio" Then
            ControlName = frmProjectPortFolio
            'Altered By Ali Ansari against Task#20150511 
            ''add Region,Zone,Belt,State,Country forms
        ElseIf key = "QuotationStatus" Then
            ControlName = frmGrdRptQuotationStatus
        ElseIf key = "frmContractOpening" Then
            ControlName = frmContractOpening
        ElseIf key = "frmTicketOpening" Then
            ControlName = frmTicketOpening
        ElseIf key = "frmProjectVisit" Then
            ControlName = frmProjectVisit
        ElseIf key = "frmProjectVisitType" Then
            ControlName = frmProjectVisitType
        ElseIf key = "Country" Then
            ControlName = FrmCountry
        ElseIf key = "State" Then
            ControlName = frmState
        ElseIf key = "Region" Then
            ControlName = FrmRegions
        ElseIf key = "Zone" Then
            ControlName = FrmZone
        ElseIf key = "Belt" Then
            ControlName = FrmBelt
            'Altered By Ali Ansari against Task#2015051
        ElseIf key = "frmGrdRptCostSheetQtyWise" Then
            ControlName = frmGrdRptCostSheetQtyWise
        ElseIf key = "CustomerSalesContribution" Then
            ''applystylesheet(frmRptCustomerSalesContribution)
            'frmRptCustomerSalesContribution.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = frmRptCustomerSalesContribution
            enm = EnumForms.frmRptCustomerSalesContribution
            'ElseIf key = "frmSalaryConfig" Then
            '    'applystylesheet(frmSalaryConfig)
            '    frmSalaryConfig.ShowDialog()
            '    Exit Sub
            'Ali Faisal : TFS# 934 : Add new for for Terms and Conditions on 15-June-2017
        ElseIf key = "frmTermsandConditions" Then
            ControlName = frmTermsandConditions
            enm = EnumForms.frmTermsandConditions
            'Ali Faisal : TFS# 934 end
            'Ali Faisal : TFS1370 : 24-Aug-2017 : Add Report Summary of Sales And Sales Returns
        ElseIf key = "SummaryofSalesAndReturns" Then
            ControlName = SummaryofSalesAndReturns
            'Ali Faisal : TFS1370 : 24-Aug-2017 : End
        ElseIf key = "frmAutoSalaryGenerate" Then
            ControlName = frmAutoSalaryGenerate
        ElseIf key = "frmEmployeeProfile" Then
            ControlName = frmEmployeeProfile
        ElseIf key = "frmAdvanceRequest" Then
            ControlName = frmAdvanceRequest
        ElseIf key = "frmAdvanceRequest1" Then
            ControlName = frmAdvanceRequest
        ElseIf key = "frmAttendanceStatusReport" Then
            ControlName = frmAttendanceStatusDetailReport
        ElseIf key = "frmGrdRptPurchaseDemandStatus" Then
            ControlName = frmGrdRptPurchaseDemandStatus
        ElseIf key = "frmEmpLoanDeductions" Then
            ControlName = frmEmployeeDeductions
        ElseIf key = "rptAdvancePaymentsPO" Then
            rptDateRange.ReportName = rptDateRange.ReportList.AdvancePaymentsPO
            'applystylesheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "frmGrdRptLocationWiseStockStatementNew" Then
            ControlName = frmGrdRptLocationWiseStockStatementNew
            enm = EnumForms.frmGrdRptLocationWiseStockStatementNew
        ElseIf key = "rptDispatchStatus" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.DispatchStatus
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            ControlName = DispatchStatus
            enm = EnumForms.DispatchStatus
        ElseIf key = "rptAdvanceReceiptsSO" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.AdvanceReceiptsSO
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = AdvanceReceiptsSO
            enm = EnumForms.AdvanceReceiptsSO
        ElseIf key = "frmRptProjectBasedTransactionDetail" Then
            ''applystylesheet(frmRptProjectBasedTransactionDetail)
            'frmRptProjectBasedTransactionDetail.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 07-Feb-2017
            ControlName = frmRptProjectBasedTransactionDetail
            enm = EnumForms.frmRptProjectBasedTransactionDetail
        ElseIf key = "frmRptEmpSalarySheetDetail" Then
            ''applystylesheet(frmRptEmpSalarySheetDetail)
            'frmRptEmpSalarySheetDetail.ShowDialog()
            'Exit Sub
            ControlName = frmRptEmpSalarySheetDetail
        ElseIf key = "rptLocationWiseClosingStock" Then
            ''applystylesheet(frmRptLocationWiseClosingStock)
            'frmRptLocationWiseClosingStock.ShowDialog()
            'Exit Sub
            ControlName = frmRptLocationWiseClosingStock
            enm = EnumForms.frmRptLocationWiseClosingStock
        ElseIf key = "frmSalesTransfer" Then
            ControlName = frmSalesTransfer
        ElseIf key = "frmCashRecoveryReport" Then
            frmCashRecoveryReport.ReportName = frmCashRecoveryReport.enmReportList.ChequeRecovery
            'applystylesheet(frmCashRecoveryReport)
            frmCashRecoveryReport.ShowDialog()
            Exit Sub
        ElseIf key = "PriceCompare" Then
            ControlName = frmGrdRptSalesPriceChange
        ElseIf key = "frmGrdRptEmployeeTargetAchieved" Then
            ControlName = frmGrdRptEmployeeMonthlyTergetAchieved
        ElseIf key = "CustomerChequesDueAll" Then
            frmCashRecoveryReport.ReportName = frmCashRecoveryReport.enmReportList.ChequeDueAll
            'applystylesheet(frmCashRecoveryReport)
            frmCashRecoveryReport.ShowDialog()
            Exit Sub

        ElseIf key = "frmRptTaskDetail" Then
            'Marked Against Task#20150516 blocking forms and style sheets
            ''applystylesheet(frmRptTaskDetail)
            'frmRptTaskDetail.ShowDialog()
            'Exit Sub
            'Marked Against Task#20150516 blocking forms and style sheets
            'Altered Against Task#20150516 blocking forms and style sheets
            RestrictForm = key
            If RestrictSheet(RestrictForm) = False Then
                'applystylesheet(frmRptTaskDetail)
                frmRptTaskDetail.ShowDialog()
                Exit Sub
            Else
                ControlName = frmdisplay
                RestrictForm = String.Empty
            End If
            'Altered Against Task#20150516 blocking forms and style sheets
        ElseIf key = "WarrantyDetailReport" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.WarrantyDetailReport
            'rptDateRange.PnlCostTop = True
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            ControlName = WarrantyDetailReport
            enm = EnumForms.WarrantyDetailReport
        ElseIf key = "rptSummaryOfSalesTaxInvoices" Then
            ''applystylesheet(rptDateRange)
            'rptDateRange.ReportName = rptDateRange.ReportList.rptSummaryOfSalesTaxInvoices
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = SummaryofSalesTaxInvoices
            enm = EnumForms.SummaryofSalesTaxInvoices
        ElseIf key = "frmGrdRptCMFAllRecords" Then
            ControlName = frmGrdRptCMFAllRecords
        ElseIf key = "frmCMFAAll" Then
            'applystylesheet(frmCMFAAll)
            frmCMFAAll.ShowDialog()
            Exit Sub
        ElseIf key = "frmImport" Then
            ControlName = frmImport
        ElseIf key = "ChequeAdjustment" Then
            ControlName = frmChequesAdjustment

        ElseIf key = "rptEmpAttendanceDetail" Then
            ''applystylesheet(rptDateRange)
            'rptDateRange.ReportName = rptDateRange.ReportList.EmployeeAttendanceDetail
            'rptDateRange.ShowDialog()
            'Exit Sub
            ControlName = EmployeeAttendanceDetail
        ElseIf key = "frmRptMonthlyPurchaseSummary" Then
            'applystylesheet(frmRptMonthlyPurchaseSummary)
            frmRptMonthlyPurchaseSummary.ShowDialog()
            Exit Sub
        ElseIf key = "frmGrdRptLocationWiseStockStatement" Then
            ControlName = frmGrdRptLocationWiseStockStatement
            enm = EnumForms.frmGrdRptLocationWiseStockStatement

        ElseIf key = "frmSMSConfiguration" Then
            'Marked Against Task#20150516 regarding block style sheets
            'If RestrictSheet(key) = False Then
            ' 'applystylesheet(frmSMSConfig)
            ' frmSMSConfig.ShowDialog()
            ' Exit Sub
            'End If
            'Marked Against Task#20150516 regarding block style sheets
            'Altered Against Task#20150516 regarding block style sheets
            RestrictForm = key
            If RestrictSheet(RestrictForm) = False Then
                'applystylesheet(frmSMSConfig)
                frmSMSConfig.ShowDialog()
                Exit Sub
            Else
                ControlName = frmdisplay
                RestrictForm = String.Empty
            End If

            'Altered Against Task#20150516 regarding block style sheets
        ElseIf key = "frmEmailConfiguration" Then
            'Marked Against Task#20150516 regarding block style sheets
            'If RestrictSheet(key) = False Then
            ' 'applystylesheet(frmSMSConfig)
            ' frmSMSConfig.ShowDialog()
            ' Exit Sub
            'End If
            'Marked Against Task#20150516 regarding block style sheets
            'Altered Against Task#20150516 regarding block style sheets
            RestrictForm = key
            If RestrictSheet(RestrictForm) = False Then
                'applystylesheet(frmEmailConfiguration)
                frmEmailConfiguration.ShowDialog()
                Exit Sub
            Else
                ControlName = frmdisplay
                RestrictForm = String.Empty
            End If
        ElseIf key = "frmMRPlan" Then
            ControlName = frmMRPlan
        ElseIf key = "frmMRPlan1" Then
            ControlName = frmMRPlan

        ElseIf key = "rptPurchaseItemSummary" Then
            rptDateRange.ReportName = rptDateRange.ReportList.PurchaseItemSummary
            'applystylesheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
            'End Task:2850
            '
            ''2-Oct-2014 Task:2863 Imran Ali Add new report Item Wise Sales Summary

        ElseIf key = "frmGrdRptSalesRegisterActivity" Then
            ControlName = frmGrdRptSalesRegisterActivity
        ElseIf key = "frmQuoteRequestList" Then
            ControlName = frmQuoteRequestList
        ElseIf key = "frmSaleInvoiceDueDate" Then
            ControlName = frmGrdRptSaleInvoicesDue
        ElseIf key = "frmRejectDispatchedStock" Then
            ControlName = frmRejectDispatchedStock
        ElseIf key = "DefineDocumentsPrefix" Then
            ControlName = frmDefDocumentPrefix
        ElseIf key = "frmGrdRptItemWiseSalesSummary" Then
            ControlName = frmGrdRptItemWiseSalesSummary
            'End Task:2863
        ElseIf key = "frmGrdRptCMFAOfSummaries" Then
            ControlName = frmGrdRptCMFAOfSummaries
            'TAsk:2864 Setting for show criteria cash receipt detail report
        ElseIf key = "rptCashReceiptsDetailAgainstEmployee" Then
            ''applystylesheet(rptDateRange)
            'rptDateRange.ReportName = rptDateRange.ReportList.CashReceiptDetailAgainstEmployee
            'rptDateRange.IsEmployee = True
            'rptDateRange.ShowDialog()
            'Exit Sub
            'End task:2864
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 07-Feb-2017
            ControlName = CashReceiptDetailAgainstEmployee
            enm = EnumForms.CashReceiptDetailAgainstEmployee
        ElseIf key = "frmRptCMFADetail" Then
            If Not LoginGroup.ToString = "Administrator" Then
                ControlName.Name = "frmRptCMFADetail"
                Rights = GroupRights.FindAll(AddressOf ReturnRights)
                If Rights.Count = 0 Then Exit Sub
            End If
            'applystylesheet(frmRptCMFADetail)
            frmRptCMFADetail.ShowDialog()
            Exit Sub
        ElseIf key = "frmRptBankReconciliation" Then
            ''applystylesheet(frmRptBankReconciliation)
            'frmRptBankReconciliation.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = frmRptBankReconciliation
            enm = EnumForms.frmRptBankReconciliation
            ''05-Aug-2014 Task:2769 Imran Ali Add new report CMFA Summary (Ravi)
        ElseIf key = "frmSMSTemplate" Then
            'applystylesheet(frmSMSTemplate)
            frmSMSTemplate.ShowDialog()
            Exit Sub
        ElseIf key = "frmInvoiceAdjustment" Then
            ControlName = frmInvoiceAdjustment
        ElseIf key = "frmGrdRptCMFASummary" Then
            ControlName = frmGrdRptCMFASummary
            'End Task:2769
            'Task:2810 Show PL Account Summary Report
        ElseIf key = "rptPLNoteDetailAccountSummary" Then
            ''applystylesheet(rptDateRange)
            'rptDateRange.ReportName = rptDateRange.ReportList.PLDetailAccountSummary
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 07-Feb-2017
            ControlName = PLDetailAccountSummary
            enm = EnumForms.PLDetailAccountSummary
        ElseIf key = "rptPLNoteSubSubAccountSummary" Then
            ''applystylesheet(rptDateRange)
            'rptDateRange.ReportName = rptDateRange.ReportList.PLSubsubAccountSummary
            'rptDateRange.ShowDialog()
            'Exit Sub
            'End Task:2810
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 07-Feb-2017
            ControlName = PLSubSubAccountSummary
            enm = EnumForms.PLSubSubAccountSummary
        ElseIf key = "frmRptDirectorDebitors" Then
            ''applystylesheet(frmRptDirectorDebitors)
            'frmRptDirectorDebitors.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 07-Feb-2017
            ControlName = frmRptDirectorDebitors
            enm = EnumForms.frmRptDirectorDebitors
            ''Task:2434 Added Menu Average Rate
        ElseIf key = "frmAdjeustmentAveragerate" Then
            ControlName = frmAdjeustmentAveragerate
            'Task:2678 Open Sales Certificate Issued Report By User
        ElseIf key = "frmGrdRptSalesCertificateIssued" Then
            ControlName = frmGrdRptSalesCertificateIssued
            'End Task:2678
            'Task:2690 Added Menu Production Comparison in Produciton
        ElseIf key = "frmGrdRptProductionComparison" Then
            ControlName = frmGrdRptProductionComparison
            'End Task:2690

            'TASK:904 
        ElseIf key = "frmItemsConsumption" Then
            ControlName = frmItemsConsumption
            '' End TASK 904
        ElseIf key = "updAvgRate" Then
            'applystylesheet(frmAverageRateUpdate)
            frmAverageRateUpdate.ShowDialog()
            Exit Sub
            'End Task:2434
        ElseIf key = "frmRptInvoiceAgingFormated" Then
            ''applystylesheet(frmRptInvoiceAgingFormated)
            'frmRptInvoiceAgingFormated.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = frmRptInvoiceAgingFormated
            enm = EnumForms.frmRptInvoiceAgingFormated
            'Task:2638 Added New Menu Warranty Claim In Inventory Menu
        ElseIf key = "frmClaim" Then
            ControlName = frmClaim
            enm = EnumForms.frmClaim
            'End Task:2638
            If Tags.Length > 0 Then
                frmClaim.Get_All(Tags)
            End If
        ElseIf key = "Groups" Then
            ControlName = frmDefSecurityGroup
            enm = EnumForms.frmDefSecurityGroup
            Me.Cursor = Cursors.WaitCursor
        ElseIf key = "CustomerBasedDiscounts" Then
            ControlName = frmCustomerDiscounts
            enm = EnumForms.frmSubAccount
        ElseIf key = "CustomerBasedDiscounts1" Then
            ControlName = frmCustomerDiscounts
            enm = EnumForms.frmSubAccount
        ElseIf key = "CustomerBasedDiscountsFlat" Then
            ControlName = frmCustomerDiscountsFlat
            enm = EnumForms.frmSubAccount
            'ElseIf key = "frmUserWiseCustomer" Then
            '    ControlName = frmUserWiseCustomer
            enm = EnumForms.frmUserWiseCustomer
        ElseIf key = "frmSubAccount" Then
            ControlName = frmSubAccount
            enm = EnumForms.frmSubAccount
            If Tags.Length > 0 Then
                frmSubAccount.Get_All(Tags)
            End If
        ElseIf key = "frmSubSubAccount" Then
            ControlName = frmSubSubAccount
            enm = EnumForms.frmSubSubAccount
            If Tags.Length > 0 Then
                frmSubSubAccount.Get_All(Tags)
            End If
        ElseIf key = "frmDetailAccount" Then
            ControlName = frmDetailAccount
            enm = EnumForms.frmDetailAccount
            If Tags.Length > 0 Then
                frmDetailAccount.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ProductionStep" Then
            ControlName = frmproductionSteps
        ElseIf key = "frmProductionLevel" Then
            ControlName = frmProductionLevel
            enm = EnumForms.Non
        ElseIf key = "frmGrdProductionAnalysis" Then
            ControlName = frmGrdProductionAnalaysis
        ElseIf key = "frmGrdRptProductionLevel" Then
            ControlName = frmGrdRptProductionLevel
            enm = EnumForms.Non
        ElseIf key = "FrmEmailconfig" Then
            ControlName = FrmEmailconfig
            ' enm = EnumForms.FrmEmailconfig
        ElseIf key = "frmNewInvItem" Then
            ControlName = frmDefArticle
            enm = EnumForms.SimpleItemDefForm
        ElseIf key = "frmNewInvItem1" Then
            ControlName = frmDefArticle
            enm = EnumForms.SimpleItemDefForm
        ElseIf key = "ArticleDepartment" Then
            ControlName = frmDefArticleDepartment
            enm = EnumForms.SimpleItemDefForm
        ElseIf key = "GenerateBarcodes" Then
            'ControlName = frmBarcodes
            ' enm = EnumForms.SimpleItemDefForm
            'ElseIf key = "frmVoucher" Then
            '    ControlName = frmVoucher
            '    enm = EnumForms.frmVoucher
        ElseIf key = "frmVoucher" Then
            ControlName = frmVoucherNew
            enm = EnumForms.frmVoucher
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
            If Tags.Length > 0 Then
                frmVoucherNew.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "voucherposting" Then
            ControlName = frmVoucherPostUnpost
            enm = EnumForms.frmVoucher
        ElseIf key = "frmBankReconciliation" Then
            ControlName = frmBankReconciliation
            enm = EnumForms.frmVoucher
        ElseIf key = "MobileExpenseEntry" Then
            ControlName = frmMobileExpense
            enm = EnumForms.frmVoucher

            'Task#31072015 Employee Attendance email alert
        ElseIf key = "EmployeeAttendanceEmailAlert" Then
            ControlName = frmEmpAttendanceEmailAlertSchedule
            'End Task#31072015

        ElseIf key = "frmGrdRptCustomerItemWiseSummary" Then
            ControlName = frmGrdRptCustomerItemWiseSummary
        ElseIf key = "frmChangeDetailAccount" Then
            ControlName = frmChangeDetailAccount
        ElseIf key = "frmFrequentlySalesItem" Then
            ControlName = frmGrdRptFrequentellySalesOrderItems

        ElseIf key = "LC" Then
            ControlName = frmLetterCredit
            enm = EnumForms.frmVoucher
        ElseIf key = "LC1" Then
            ControlName = frmLetterCredit
            enm = EnumForms.frmVoucher
        ElseIf key = "EmpSalary" Then
            ControlName = frmEmployeeSalaryVoucher
            enm = EnumForms.frmVoucher
        ElseIf key = "empbarcodestiker" Then
            ShowReport("rptEmployeeBarcodeSticker", , , , , , , GetEmployeeBarcodeStickerData)
            Exit Sub
        ElseIf key = "DailyWagies" Then
            ControlName = frmDailySalaries
            enm = EnumForms.frmVoucher
        ElseIf key = "rptIssuedSalesCertificate" Then
            ''applystylesheet(rptDateRange)
            'rptDateRange.ReportName = rptDateRange.ReportList.SalesCertificateIssued
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = SalesCertificateIssued
            enm = EnumForms.SalesCertificateIssued
        ElseIf key = "frmPurchase" Then
            ControlName = frmPurchaseNew
            enm = EnumForms.frmPurchase
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            If Tags.Length > 0 Then
                frmPurchaseNew.Get_All(Tags)
                'Tags = String.Empty
            End If
        ElseIf key = "frmReceivingNote" Then
            ControlName = frmReceivingNote
            enm = EnumForms.Purchase
            ''Start TFS2648
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            If Tags.Length > 0 Then
                frmReceivingNote.Get_All(Tags)
                'Tags = String.Empty
            End If
            ''End TFS2648
        ElseIf key = "frmReceivingNote1" Then
            ControlName = frmReceivingNote
            enm = EnumForms.Purchase
            ''Start TFS2648
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            If Tags.Length > 0 Then
                frmReceivingNote.Get_All(Tags)
                'Tags = String.Empty
            End If
            ''End TFS2648
        ElseIf key = "frmPurchaseOrder" Then
            ControlName = frmPurchaseOrderNew
            enm = EnumForms.frmPurchaseOrder
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            If Me.Tags.Length > 0 Then
                frmPurchaseOrderNew.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmItemBulk" Then
            ControlName = frmItemBulk
            enm = EnumForms.frmItemBulk
        ElseIf key = "PurchaseReturn" Then
            ControlName = frmPurchaseReturn
            enm = EnumForms.frmPurchaseReturn
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            If Tags.Length > 0 Then
                frmPurchaseReturn.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmSalesOrder" Then
            ControlName = frmSalesOrderNew
            enm = EnumForms.frmSaleOrder
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            If Tags.Length > 0 Then
                frmSalesOrderNew.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmSalesOrder1" Then
            ControlName = frmSalesOrderNew
            enm = EnumForms.frmSaleOrder
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            If Tags.Length > 0 Then
                frmSalesOrderNew.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmQoutationNew" Then
            ControlName = frmQoutationNew
            enm = EnumForms.frmSaleOrder
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            If Tags.Length > 0 Then
                frmQoutationNew.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "RecordSales" Then
            ControlName = frmSales
            enm = EnumForms.frmSales
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            If Tags.Length > 0 Then
                frmSales.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "frmDeliveryChalan" Then
            ControlName = frmDeliveryChalan
            enm = EnumForms.frmSales
        ElseIf key = "frmRptGraphs" Then
            ControlName = frmRptGraphs
            enm = EnumForms.Non
        ElseIf key = "SalesReturn" Then
            ControlName = frmSalesReturn
            enm = EnumForms.frmSalesReturn
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            If Tags.Length > 0 Then
                frmSalesReturn.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ProductionStore" Then
            ControlName = frmProductionStore
            enm = EnumForms.frmProductionStore
            If Tags.ToString.Length > 0 Then
                frmProductionStore.Get_All(Tags.ToString)
            End If
        ElseIf key = "frmCmfa" Then
            ControlName = frmCmfa
        ElseIf key = "frmStockAdjustment" Then
            ControlName = frmStockAdjustment
            'enm = EnumForms.frmStockAdjustment

        ElseIf key = "ReturnableGatepass" Then
            ControlName = frmReturnablegatepass
            enm = EnumForms.frmReturnablegatepass

            'Ahmad sharif: added project quotation
        ElseIf key = "frmProjectQuotation" Then
            ControlName = frmProjQuotion
            'enm = EnumForms.frmReturnablegatepass

        ElseIf key = "InwardGatepass" Then

            ControlName = frmServicesInwardGatePass
            enm = EnumForms.frmInwardGatePass

        ElseIf key = "outwardGatepass" Then

            ControlName = frmServicesInwardGatePass
            enm = EnumForms.frmInwardGatePass


        ElseIf key = "frmRptGrdPurchaseDetailWithWeight" Then
            ControlName = frmRptGrdPurchaseDetailWithWeight
        ElseIf key = "frmGrdRptEmployeeMonthlyAttendance" Then
            ControlName = frmGrdRptEmployeeMonthlyAttendance
        ElseIf key = "frmGRNStockReport" Then
            ControlName = frmGRNStockReport

            'Task#118062015 Ahmad Sharif
        ElseIf key = "frmGrdRptCostSheetMarginCalculationDetail" Then
            ControlName = frmGrdRptCostSheetMarginCalculationDetail
            'End Task#118062015
        ElseIf key = "frmGrdPlanComparison" Then
            ControlName = frmGrdPlanComparison
            enm = EnumForms.frmGrdPlanComparison
        ElseIf key = "UpdateReturnableGatepass" Then
            ControlName = frmUpdateReturnableGatepassDetail
            enm = EnumForms.frmReturnablegatepass

        ElseIf key = "itemWiseRpt" Then
            ControlName = ItemWiseSalesrpt
            enm = EnumForms.frmSalesReturn
            'ElseIf key = "StoreIssuence" Then
            '    ControlName = frmStoreIssuence
            '    enm = EnumForms.frmStoreIssuence
            '    If Tags.Length > 0 Then
            '        frmStoreIssuence.Get_All(Tags)
            '        'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            '    End If
        ElseIf key = "StoreIssuence" Then
            Dim ConfigValue As String = getConfigValueByType("CostSheetType")
            If ConfigValue = "Error" Or ConfigValue = "Standard Cost Sheet" Then
                ControlName = frmStoreIssuence
                enm = EnumForms.frmStoreIssuence
                If Tags.Length > 0 Then
                    frmStoreIssuence.Get_All(Tags)
                End If

            Else
                ControlName = frmStoreIssuenceNew
                If Tags.Length > 0 Then
                    frmStoreIssuenceNew.Get_All(Tags)
                End If
            End If
            'Rafay Duplicate key issue  
        ElseIf key = "StoreIssuence1" Then
            Dim ConfigValue As String = getConfigValueByType("CostSheetType")
            If ConfigValue = "Error" Or ConfigValue = "Standard Cost Sheet" Then
                ControlName = frmStoreIssuence
                enm = EnumForms.frmStoreIssuence
                If Tags.Length > 0 Then
                    frmStoreIssuence.Get_All(Tags)
                End If

            Else
                ControlName = frmStoreIssuenceNew
                If Tags.Length > 0 Then
                    frmStoreIssuenceNew.Get_All(Tags)
                End If
            End If
        ElseIf key = "frmReturnStoreIssuance" Then
            ControlName = frmReturnStoreIssuence
            enm = EnumForms.frmStoreIssuence
            If Tags.Length > 0 Then
                frmReturnStoreIssuence.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmReturnStoreIssuance1" Then
            ControlName = frmReturnStoreIssuence
            enm = EnumForms.frmStoreIssuence
            If Tags.Length > 0 Then
                frmReturnStoreIssuence.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "SalesChart" Then
            ControlName = rptSalesChart
            enm = EnumForms.frmStoreIssuence

        ElseIf key = "Tasks" Then
            ControlName = frmTasks
            'Task 2639 JUNAID VendorType
        ElseIf key = "frmVendorTypeKey" Then
            ControlName = frmVendorType
            'End task 2639
            'Task 2640 JUNAID
        ElseIf key = "SMSScheduleKey" Then
            ControlName = frmScheduleSMS
            'End task 2640

        ElseIf key = "TaskWorking" Then
            ControlName = frmTaskWork

        ElseIf key = "Type" Then
            ControlName = frmTypes
        ElseIf key = "Type1" Then
            ControlName = frmTypes

        ElseIf key = "Status" Then
            ControlName = frmStatus

        ElseIf key = "frmRptGrdAdvances" Then
            'applystylesheet(frmRptGrdAdvances)
            ControlName = frmRptGrdAdvances

        ElseIf key = "rptTaskAssign" Then
            'Marked Against Task#20150516 regarding block style sheets
            '    RptDateRangeEmployees.ReportName = RptDateRangeEmployees.ReportList.rptTaskAssign
            '    'applystylesheet(RptDateRangeEmployees)
            '    RptDateRangeEmployees.ShowDialog()
            'Marked Against Task#20150516 regarding block style sheets
            'Altered Against Task#20150516 regarding block style sheets
            RestrictForm = key
            If RestrictSheet(RestrictForm) = False Then
                RptDateRangeEmployees.ShowDialog()
                Exit Sub
            Else
                ControlName = frmdisplay
                RestrictSheetAccess = String.Empty
            End If
            'Altered Against Task#20150516 regarding block style sheets
        ElseIf key = "frmGrdRptMinimumStockLevel" Then
            ControlName = frmGrdRptMinimumStockLevel
        ElseIf key = "ClosingStockByOrder" Then
            ControlName = frmGrdRptClosingStockByOrders
        ElseIf key = "ArticleBarcodePrinting" Then
            'applystylesheet(frmItemBarCodePrinting)
            frmItemBarCodePrinting.ShowDialog()
            Exit Sub
        ElseIf key = "frmStockStatmentBySize" Then
            'applystylesheet(frmStockStatmentBySize)
            ControlName = frmStockStatmentBySize
            enm = EnumForms.frmStockStatmentBySize
        ElseIf key = "ComposeMessage" Then
            ControlName = frmComposeMessage

        ElseIf key = "Message" Then
            ControlName = frmMessageView
        ElseIf key = "salesmancommission" Then
            ControlName = frmGrdSalesmanCommissionDetail
            ''Task:2357 Added Control
        ElseIf key = "frmGrdRptSalesComparison" Then
            ControlName = frmGrdRptSalesComparison
            'End Task:2357
            ''Tsk:2370 Added Control 
        ElseIf key = "frmGrdRptInvoiceAging" Then
            ControlName = frmGrdRptInvoiceAging
            ''End Task:2370
            ''12-Mar-2014  TASK:2488  Imran Ali Sales Certificate In ERP
        ElseIf key = "frmSalesCertificate" Then
            ControlName = frmSalesCertificate
            'End task:2488
        ElseIf key = "salescomparison" Then
            ControlName = frmSalesComparisonCustomerWise

        ElseIf key = "frmRequestViews" Then
            ControlName = frmRequestViews

        ElseIf key = "OpeningBL" Then
            ''applystylesheet(frmOpening)
            'frmOpening.ShowDialog()
            ControlName = frmOpening
        ElseIf key = "frmYearClose" Then
            ControlName = frmYearClose
            enm = EnumForms.frmVoucher
        ElseIf key = "SalesmanDealerVoucher" Then
            ''applystylesheet(rptVoucher)
            'rptVoucher.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = rptVoucher
            enm = EnumForms.rptVoucher
        ElseIf key = "SalesmanMonthlySales" Then
            ControlName = rptSalesmanMonthlySalesReport
            enm = EnumForms.Sales
        ElseIf key = "SalesDetail" Then
            ControlName = frmGrdSales
        ElseIf key = "SectorWiseSales" Then
            ControlName = frmGrdRptSectorSales
        ElseIf key = "SalesDtbyCategory" Then
            ControlName = frmGrdRptSalesByGender
        ElseIf key = "DailyWorkingReport" Then
            ControlName = rptDailyWorkingReport
            enm = EnumForms.Sales
            ''06-Mar-2014   Task:2462  Imran Ali    Add New Report Purchase Daily Working
        ElseIf key = "rptPurchaseDailyWorkingReport" Then
            ControlName = rptPurchaseDailyWorkingReport
            enm = EnumForms.frmPurchase
            'End Task:2462

        ElseIf key = "rptGrdEachDaysWorking" Then
            ControlName = rptGrdEachDaysWorking
        ElseIf key = "CustomerCollection" Or key = "frmOldCustomerCollection" Then
            Dim ReceiptScreen As Boolean = Convert.ToBoolean(getConfigValueByType("NewReceiptForm").ToString)
            If ReceiptScreen = True Then
                ControlName = frmCustomerCollection
                enm = EnumForms.frmCustomerCollection
                CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
                If Tags.Length > 0 Then
                    frmCustomerCollection.Get_All(Tags)
                    'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
                End If
            Else
                ControlName = frmOldCustomerCollection
                enm = EnumForms.frmCustomerCollection
                CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
                If Tags.Length > 0 Then
                    frmOldCustomerCollection.Get_All(Tags)
                End If
            End If
            'ElseIf key = "frmOldCustomerCollection" Then
            '    Dim ReceiptScreen As Boolean = Convert.ToBoolean(getConfigValueByType("NewReceiptForm").ToString)
            '    If ReceiptScreen = True Then
            '        ControlName = frmCustomerCollection
            '        enm = EnumForms.frmCustomerCollection
            '        CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
            '        If Tags.Length > 0 Then
            '            frmCustomerCollection.Get_All(Tags)
            '            'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            '        End If
            '    Else
            '        ControlName = frmOldCustomerCollection
            '        CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
            '        If Tags.Length > 0 Then
            '            frmOldCustomerCollection.Get_All(Tags)
            '        End If
            '    End If
            'ElseIf key = "CustomerCollection" Or key = "OldCustomerCollection" Then
            '    Dim customercollection As String = getConfigValueByType("NewReceiptForm")
            '    If customercollection = "True" Then
            '        ControlName = frmCustomerCollection
            '        enm = EnumForms.frmCustomerCollection
            '        CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
            '        If Tags.Length > 0 Then
            '            frmCustomerCollection.Get_All(Tags)
            '        End If
            '    Else
            '        ControlName = frmOldCustomerCollection
            '        enm = EnumForms.frmCustomerCollection
            '        CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
            '        If Tags.Length > 0 Then
            '            frmOldCustomerCollection.Get_All(Tags)
            '        End If
            '    End If
            'ElseIf key = "VendorPayments" Then
            '    ControlName = frmVendorPayment
            '    enm = EnumForms.frmVendorPayment
            '    CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
            '    If Tags.Length > 0 Then
            '        frmVendorPayment.Get_All(Tags)
            '        'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            '    End If
        ElseIf key = "VendorPayments" Or key = "frmPaymentNew" Then
            Dim payment As String = getConfigValueByType("NewPaymentForm")
            If payment = "True" Then
                ControlName = frmPaymentNew
                enm = EnumForms.frmVendorPayment
                CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
                If Tags.Length > 0 Then
                    frmPaymentNew.Get_All(Tags)
                End If
            Else
                ControlName = frmVendorPayment
                enm = EnumForms.frmVendorPayment
                CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
                If Tags.Length > 0 Then
                    frmVendorPayment.Get_All(Tags)
                End If
            End If

        ElseIf key = "PaymentVendor" Then
            ControlName = frmPaymentVoucherNew
            enm = EnumForms.frmVendorPayment
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General

        ElseIf key = "ReceiptCustomer" Then
            ControlName = frmReceiptVoucherNew
            enm = EnumForms.frmCustomerCollection
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General

        ElseIf key = "CustomerType" Then
            ControlName = frmDefCustomerType
            enm = EnumForms.DefMainAcc
            If Tags.Length > 0 Then
                frmDefCustomerType.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "EmpAttendance" Then
            ControlName = frmAttendance
            enm = EnumForms.frmAttendance
        ElseIf key = "Adjustment" Then
            ControlName = frmAdjustments
            'enm = EnumForms.Sales
        ElseIf key = "EmpSalaryRpt" Or key = "EmpSalaryRpt1" Then
            ControlName = frmGrdRptEmployeeSalarySheetDetail
            enm = EnumForms.Non
        ElseIf key = "DailyAttendance" Then
            ControlName = frmAttendanceEmployees
            enm = EnumForms.frmAttendance

        ElseIf key = "rptDailyAttendance" Then
            'AddRptParam("@CurrentDate", Date.Now.Date.ToString("yyyy-M-d 00:00:00"))
            'ShowReport("rptEmployeesAttendance")
            'rptDateRange.ReportName = rptDateRange.ReportList.DailyEmployeeAttendance
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Task:2418 Added Report With Holding Tax Certificate
            ControlName = DailyAttendance
            enm = EnumForms.DailyAttendance
        ElseIf key = "WithHoldingTaxCertificate" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.WHTaxCertificate
            ''applystylesheet(rptDateRange)
            'rptDateRange.IsVendor = True
            'rptDateRange.ShowDialog()
            'rptDateRange.IsVendor = False
            'Exit Sub
            'End Task:2418
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 07-Feb-2017
            ControlName = WithHoldingTaxCertificate
            enm = EnumForms.WithHoldingTaxCertificate
        ElseIf key = "FrmVoucherCheckList" Then
            ''applystylesheet(FrmVoucherCheckList)
            'FrmVoucherCheckList.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 07-Feb-2017
            ControlName = FrmVoucherCheckList
            enm = EnumForms.FrmVoucherCheckList
        ElseIf key = "rptAttendanceSummary" Then
            rptDateRange.ReportName = rptDateRange.ReportList.AttendanceSummary
            'applystylesheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "DemandSumm" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.DemandSummary
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = DemandSummary
            enm = EnumForms.DemandSummary
        ElseIf key = "DemandDt" Then
            ControlName = frmGrdRptDemandDetail
            enm = EnumForms.Non
        ElseIf key = "grdDispatchDetail" Then
            ControlName = frmGrdDispatchDetail
            'Task:2410   Added Menu Return Detail
        ElseIf key = "frmGrdSalesReturnDetail" Then
            ControlName = frmGrdSalesReturnDetail
            'End Task:2410
        ElseIf key = "grdSaleManDemandDetail" Then
            ControlName = frmGrdSalemansDemandDetail
        ElseIf key = "AttendanceSummary" Then
            ControlName = AttedanceSummary
            'RptDateRangeEmployees.ReportName = RptDateRangeEmployees.ReportList.ReportAttendanceSummary
            ''applystylesheet(RptDateRangeEmployees)
            'RptDateRangeEmployees.ShowDialog()
            'Exit Sub
        ElseIf key = "DemandSales" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.DemandSales
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = DemandSales
            enm = EnumForms.DemandSales
        ElseIf key = "rptReturnableGatepass" Then
            rptDateRange.ReportName = rptDateRange.ReportList.ReturnableGatepass
            'applystylesheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "frmDateLock" Then
            ControlName = frmDateLock
            enm = EnumForms.Non
        ElseIf key = "frmChequeBook" Then
            ControlName = frmAddChequeBookSerial
            enm = EnumForms.Non
        ElseIf key = "DailySupplyGatepass" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.DailySupply
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            ControlName = frmGrdDailySupply
            'Customer Collection
        ElseIf key = "ConfigCompany" Then
            ControlName = frmDefCategory
            enm = EnumForms.frmDefCompany
            If Tags.Length > 0 Then
                frmDefCategory.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmDepartment" Then
            ControlName = frmDefDepartment
            enm = EnumForms.frmDefEmployee
            If Tags.Length > 0 Then
                frmDefDepartment.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "frmEmployeeCards" Then
            ControlName = frmEmployeeCards
            enm = EnumForms.Non
        ElseIf key = "EmployeeCard" Then
            ControlName = frmEmployeeCard
        ElseIf key = "UnitOfItem" Then
            ControlName = frmDefUnit
            ' enm = EnumForms.frmDefEmployee
            If Tags.Length > 0 Then
                frmDefUnit.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "frmDesignation" Then
            ControlName = frmDefEmpDesignation
            enm = EnumForms.frmDefEmployee
            If Tags.Length > 0 Then
                frmDefEmpDesignation.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ConfigLPO" Then
            ControlName = frmDefSubCategory
            enm = EnumForms.frmDefLpo
            If Tags.Length > 0 Then
                frmDefSubCategory.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ConfigSize" Then
            ControlName = frmDefSize
            enm = EnumForms.frmDefSize
            If Tags.Length > 0 Then
                frmDefSize.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ConfigColor" Then
            ControlName = frmDefColor
            enm = EnumForms.frmDefColor
            If Tags.Length > 0 Then
                frmDefColor.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmDefGender" Then
            ControlName = frmDefGender
            enm = EnumForms.frmDefColor
            If Tags.Length > 0 Then
                frmDefGender.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ConfigCategory" Then
            'ControlName = frmDefCategory
            ControlName = frmDetailAccountCat
            enm = EnumForms.frmDefCategory

        ElseIf key = "CostCenter" Then
            ControlName = frmCostCenter
            enm = EnumForms.DefMainAcc
            If Tags.Length > 0 Then
                frmCostCenter.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "frmAddCostCenter" Then
            'ControlName = frmAddCostCenter
            'enm = EnumForms.DefMainAcc
            'Task:2830 Apply Security Add New Customer
            If Not LoginGroup.ToString = "Administrator" Then
                ControlName.Name = "CostCenter"
                Rights = GroupRights.FindAll(AddressOf ReturnRights)
                If Rights.Count = 0 Then Exit Sub
            End If
            'End Task:2830

            frmAddCostCenter.ShowDialog()

        ElseIf key = "frmEmailTemplate" Then
            'applystylesheet(frmEmailTemplate)
            frmEmailTemplate.ShowDialog()
            Exit Sub
        ElseIf key = "DailyTask" Then
            AddRptParam("@CurrentDate", Date.Now.Date.ToString("yyyy-M-d 00:00:00"))
            ShowReport("rptTask")

        ElseIf key = "EmpWiseTaskReport" Then
            RptDateRangeEmployees.ReportName = RptDateRangeEmployees.ReportList.EmployeeTask
            'applystylesheet(RptDateRangeEmployees)
            RptDateRangeEmployees.ShowDialog()
            Exit Sub
        ElseIf key = "PrintVehicleLog" Then
            rptDateRange.ReportName = rptDateRange.ReportList.PrintVehicleLog
            'applystylesheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "PrintVehicleLog1" Then
            rptDateRange.ReportName = rptDateRange.ReportList.PrintVehicleLog
            'applystylesheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "CompanyInfo" Then
            ControlName = frmCompanyInformation
            enm = EnumForms.DefMainAcc
            If Tags.Length > 0 Then
                frmCompanyInformation.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "AddCustomer" Then
            ControlName = FrmAddCustomers
            enm = EnumForms.DefMainAcc

        ElseIf key = "NewCustomerDefine" Then
            'Task:2830 Apply Security Add New Customer
            If Not LoginGroup.ToString = "Administrator" Then
                ControlName.Name = "AddCustomer"
                Rights = GroupRights.FindAll(AddressOf ReturnRights)
                If Rights.Count = 0 Then Exit Sub
            End If
            'End Task:2830
            Dim CustId As Integer = 0
            FrmAddCustomers.FormType = "Customer"
            FrmAddCustomers.ShowDialog()

        ElseIf key = "NewCashAccount" Then
            'Task:2830 Apply Security Add New Cash Account
            If Not LoginGroup.ToString = "Administrator" Then
                ControlName.Name = "Cash"
                Rights = GroupRights.FindAll(AddressOf ReturnRights)
                If Rights.Count = 0 Then Exit Sub
            End If
            'End Task:2830
            Dim CustId As Integer = 0
            FrmAddCustomers.FormType = "Cash"
            FrmAddCustomers.ShowDialog()
        ElseIf key = "frmDataTransfer" Then
            'applystylesheet(frmDataTransfer)
            frmDataTransfer.ShowDialog()
            Exit Sub
        ElseIf key = "frmDataImport" Then
            'applystylesheet(frmDataImport)
            frmDataImport.ShowDialog()
            Exit Sub
        ElseIf key = "NewBankAccount" Then
            'Task:2830 Apply Security Add New Bank Account
            If Not LoginGroup.ToString = "Administrator" Then
                ControlName.Name = "Bank"
                Rights = GroupRights.FindAll(AddressOf ReturnRights)
                If Rights.Count = 0 Then Exit Sub
            End If
            'End Task:2830
            Dim CustId As Integer = 0
            FrmAddCustomers.FormType = "Bank"
            FrmAddCustomers.ShowDialog()
            Exit Sub
        ElseIf key = "NewVendorDefine" Then
            'Task:2830 Apply Security Add New Vendor
            If Not LoginGroup.ToString = "Administrator" Then
                ControlName.Name = "AddVendor"
                Rights = GroupRights.FindAll(AddressOf ReturnRights)
                If Rights.Count = 0 Then Exit Sub
            End If
            'End Task:2830
            Dim CustId As Integer = 0
            FrmAddCustomers.FormType = "Vendor"
            FrmAddCustomers.ShowDialog()
            Exit Sub
        ElseIf key = "AboutUs" Then
            ControlName = frmaboutus
            'frmaboutus.ShowDialog()
            'Exit Sub
        ElseIf key = "ConfigVendor" Then
            ControlName = frmDefVendor
            enm = EnumForms.frmDefVendor
            If Tags.Length > 0 Then
                frmDefVendor.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ConfigCustomer" Then
            ControlName = frmDefCustomer
            enm = EnumForms.frmDefCustomer
            If Tags.Length > 0 Then
                frmDefCustomer.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ConfigType" Then
            ControlName = frmDefType
            enm = EnumForms.frmDefType
            If Tags.Length > 0 Then
                frmDefType.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

            ''changes by ali ansari
        ElseIf key = "rptSaleCertificateLedger" Then
            ''applystylesheet(frmRptSalesCertificateLedger)
            'frmRptSalesCertificateLedger.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = frmRptSalesCertificateLedger
            enm = EnumForms.frmRptSalesCertificateLedger
        ElseIf key = "frmGrdRptOrdersDetail" Then
            ''changes by ali ansari
            ControlName = frmGrdRptOrdersDetail

        ElseIf key = "CustomerBasedTarget" Then
            ControlName = frmGrdCustomerBasedTarget
        ElseIf key = "SalesmanCommission" Then
            ControlName = frmDefCommissionBySaleman
        ElseIf key = "frmGrdRptContactList" Then
            ControlName = frmGrdRptContactList
            enm = EnumForms.Non
        ElseIf key = "CustomerSalesHistory" Then
            ControlName = frmGrdRptSalesHistory
        ElseIf key = "VehicleInformation" Then
            ControlName = frmDefVehicle
        ElseIf key = "VehicleInformation1" Then
            ControlName = frmDefVehicle
        ElseIf key = "RootPlan" Then
            ControlName = frmRootPlan
        ElseIf key = "VehicleLog" Then
            ControlName = FrmVehicle
        ElseIf key = "VehicleLog1" Then
            ControlName = FrmVehicle
        ElseIf key = "CustSumSalesChart" Then
            ControlName = frmGrdRptCustomersWiseSummarySalesChart
        ElseIf key = "CustItemsSummarySales" Then
            ControlName = frmGrdRptCustomersItemsSummarySales
        ElseIf key = "EmpMonthlySales" Then
            ControlName = frmEmployeeWiseMonthlySale
            enm = EnumForms.Non
        ElseIf key = "frmGrdArticleLedger" Then
            ControlName = frmGrdArticleLedger
            enm = EnumForms.frmGrdArticleLedger
        ElseIf key = "frmGrdRptTaxDuductionDetail" Then
            ControlName = frmGrdRptTaxDuductionDetail
        ElseIf key = "ConfigTransporter" Then
            ControlName = frmDefTransporter
            enm = EnumForms.frmDefTransporter
            If Tags.Length > 0 Then
                frmDefTransporter.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "EmployeeInfo" Then
            ControlName = frmDefEmployee
            enm = EnumForms.frmDefEmployee
            If Tags.Length > 0 Then
                frmDefEmployee.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "frmAllowancetype" Then
            ControlName = frmAllownaceType
            'ElseIf key = "TaskNotifications" Then
            '    ControlName = frmNotificationUtility
            'TaskNotifications
        ElseIf key = "frmDeductionType" Then
            ControlName = frmDeductionType

            'ElseIf key = "UtilBackup" Then
            '    ControlName = frmSqlServerBackup
        ElseIf key = "frmGRNStatus" Then
            ControlName = frmGRNStatus

        ElseIf key = "UtilBackupNew" Then
            ControlName = frmdbbackup
        ElseIf key = "UtilBackupNew1" Then
            ControlName = frmdbbackup

        ElseIf key = "UtilRestoreDatabase" Then
            ControlName = frmRestoreBackup

        ElseIf key = "UserControl" Then
            ControlName = frmDefUser
            enm = EnumForms.frmDefUser
        ElseIf key = "ConfigCity" Then
            ControlName = frmDefCity
            enm = EnumForms.frmDefCity
            If Tags.Length > 0 Then
                frmDefCity.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ConfigTerritory" Then
            ControlName = frmDefArea
            enm = EnumForms.frmDefArea
        ElseIf key = "mnuConfigYearSaleTarget" Then
            ControlName = frmYearlySaleTarget
            enm = EnumForms.frmYearlySaleTarget
        ElseIf key = "invloc" Then
            ControlName = FrmLocation

            enm = EnumForms.frmDefArea
            If Tags.Length > 0 Then
                FrmLocation.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "invloc1" Then
            ControlName = FrmLocation

            enm = EnumForms.frmDefArea
            If Tags.Length > 0 Then
                FrmLocation.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "ActivityLog" Then
            ControlName = frmActivityLog

        ElseIf key = "UpdateVersion" Then
            ControlName = frmReleaseUpdate
        ElseIf key = "frmReleaseDownload" Then
            ControlName = frmReleaseDownload
            frmReleaseDownload.ShowDialog()
            frmReleaseDownload.Show()
            'Stock Dispatch commented for rebuild
        ElseIf key = "Stock Dispatch" Then
            ControlName = frmStockDispatch
            enm = EnumForms.frmStockDispatch
            If Tags.Length > 0 Then
                'Stock Dispatch commented for rebuild
                frmStockDispatch.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "Stock Dispatch1" Then
            'Stock Dispatch commented for rebuild
            ControlName = frmStockDispatch
            enm = EnumForms.frmStockDispatch
            If Tags.Length > 0 Then
                'Stock Dispatch commented for rebuild
                frmStockDispatch.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "Stock Receiving" Then
            ControlName = frmStockReceive
            enm = EnumForms.frmStockReceive
            If Tags.Length > 0 Then
                frmStockReceive.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "Stock Receiving1" Then
            ControlName = frmStockReceive
            enm = EnumForms.frmStockReceive
            If Tags.Length > 0 Then
                frmStockReceive.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "StoreSummary" Then
            'frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.RptStoreIssuanceSummary
            ''applystylesheet(frmRptEnhancementNew)
            'frmRptEnhancementNew.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 08-Feb-2017
            ControlName = StoreIssuanceSummary
            enm = EnumForms.StoreIssuanceSummary
        ElseIf key = "ReturnStoreIssuenceReport" Then
            'frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.RptStoreIssuanceSummary
            'applystylesheet(frmReturnStoreIssuenceReport)
            frmReturnStoreIssuenceReport.ShowDialog()
            Exit Sub
        ElseIf key = "StoreDetail" Then
            'frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.RptStoreIssuanceDetail
            ''applystylesheet(frmRptEnhancementNew)
            'frmRptEnhancementNew.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 08-Feb-2017
            ControlName = StoreIssuanceDetail
            enm = EnumForms.StoreIssuanceDetail
        ElseIf key = "SOStatus" Then
            ControlName = frmSOStatus
            enm = EnumForms.frmSOStatus
        ElseIf key = "frmusergroup" Then
            ControlName = frmUserGroup


        ElseIf key = "frmBudget" Then
            ControlName = frmBudget
            enm = EnumForms.frmBudget
        ElseIf key = "frmCustomerPlanning" Then
            ControlName = frmCustomerPlanning
            enm = EnumForms.frmCustomerPlanning

        ElseIf key = "frmProductionPlanStatus" Then
            ControlName = frmProductionPlanStatus
            enm = EnumForms.frmCustomerPlanning
        ElseIf key = "frmBillAnalysis" Then
            ControlName = frmBillAnalysis
            enm = EnumForms.Sales
        ElseIf key = "UpdateBuiltyNoAndTransportor" Then
            ControlName = frmUpdatebitlyAndTransporter
            enm = EnumForms.frmUpdatebitlyAndTransporter

        ElseIf key = "frmGrdRptStockStatementUnitWise" Then
            ControlName = frmGrdRptStockStatementUnitWise
            enm = EnumForms.frmGrdRptStockStatementUnitWise
        ElseIf key = "frmChequeTransfer" Then
            ControlName = frmChequeTransfer
            enm = EnumForms.frmChequeTransfer

        ElseIf key = "POStatus" Then
            ControlName = frmPOStatus
            enm = EnumForms.frmPOStatus

        ElseIf key = "frmSystemConfigurationNew" Then
            ControlName = frmSystemConfigurationNew
            enm = EnumForms.frmSystemConfiguration
            'Dialog Section

        ElseIf key = "WeightCalculator" Then
            ControlName = frmSalesReturnWeight
        ElseIf key = "CompConnectionInfomation" Then
            ControlName = CompanyAndConnectionInfo
            enm = EnumForms.frmDefUser
        ElseIf key = "frmConfigurationSystemNew" Then
            ControlName = frmSystemConfigurationNew
            enm = EnumForms.frmSystemConfiguration
        ElseIf key = "rtpInventoryLevel" Then
            ControlName = rptInventoryLevelComparison
            enm = EnumForms.rtpInventoryLevel
            'ElseIf key = "frmEmployeeSalaryVoucherNew" Then
            '    ControlName = frmEmployeeSalaryVoucherNew
            '    enm = EnumForms.Non
        ElseIf key = "frmRptStockStatment" Then
            'frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.StockStatementByLPO
            ''applystylesheet(frmRptEnhancementNew)
            'frmRptEnhancementNew.ShowDialog()
            'Exit Sub
            ControlName = StockStatementByLPO
            enm = EnumForms.StockStatementByLPO
        ElseIf key = "frmRptStockStatementSize" Then
            'frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.StockStatementWithSize
            ''applystylesheet(frmRptEnhancementNew)
            'frmRptEnhancementNew.ShowDialog()
            'Exit Sub
            ControlName = StockStatementWithSize
            enm = EnumForms.StockStatementWithSize
        ElseIf key = "PLNotesDetail" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.PLNotesDetail
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 02-Feb-2017
            ControlName = PLNotesDetail
            enm = EnumForms.PLNotesDetail
        ElseIf key = "rptDamageBudget" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.DamageBudget
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = DamageBudget
            enm = EnumForms.DamageBudget
        ElseIf key = "frmExpense" Then
            ControlName = frmExpense
            enm = EnumForms.frmExpense
            If Tags.Length > 0 Then
                frmExpense.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If

        ElseIf key = "frmDefBatch" Then
            ControlName = frmDefBatch
            enm = EnumForms.frmDefBatch
            If Tags.Length > 0 Then
                frmDefBatch.Get_All(Tags)
                'Tags = String.Empty Comment Against 18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
            End If
        ElseIf key = "rptCategoryWiseSaleReport" Then
            ControlName = rptCategoryWiseSaleReport
            enm = EnumForms.rptCategoryWiseSaleReport
        ElseIf key = "agingtemplates" Then
            'applystylesheet(frmAgingBalancesTemplate)
            frmAgingBalancesTemplate.ShowDialog()
            Exit Sub
        ElseIf key = "PriceChangeReport" Then
            ControlName = rptPriceChangeReport
            enm = EnumForms.rptPriceChangeReport

        ElseIf key = "frmRptLedgerNew" Then
            ControlName = frmRptLedgerNew
        ElseIf key = "frmGrdRptAgingPayables" Then
            ControlName = frmGrdRptAgingPayables
        ElseIf key = "frmGrdRptAgingReceiveables" Then
            ControlName = frmGrdRptAgingReceiveables
        ElseIf key = "frmGrdRptAgingReceiveables1" Then
            ControlName = frmGrdRptAgingReceiveables
        ElseIf key = "rptStockAccountsReport" Then
            ControlName = rptStockAccountsReport
            enm = EnumForms.rptStockAccountsReport
        ElseIf key = "AccountsReports" Then
            ControlName = AccountsReports
            enm = EnumForms.frmVoucher
        ElseIf key = "AccountsReports1" Then
            ControlName = AccountsReports
            enm = EnumForms.frmVoucher
            'ElseIf key = "RptCustomerSalesAnlysis" Then
            '    ControlName = RptCustomerSalesAnlysis
            '    enm = EnumForms.rptStockAccountsReport
        ElseIf key = "RptGridItemSalesHistory" Then
            ControlName = RptGridItemSalesHistory
            enm = EnumForms.rptStockAccountsReport

        ElseIf key = "frmGrdStockDeliveryChalan" Then
            ControlName = frmGrd_Prod_DC_WiseStock
            enm = EnumForms.frmGrd_Prod_DC_WiseStock
        ElseIf key = "frmGrdCostSheetComparisonWithStock" Then
            ControlName = frmGrdCostSheetComparisonWithStock
            enm = EnumForms.frmGrdCostSheetComparisonWithStock
        ElseIf key = "frmGrdSalesSummary" Then
            ControlName = frmGrdSalesSummary
            enm = EnumForms.Non

        ElseIf key = "frmGrdPurchaseSummary" Then
            ControlName = frmGrdPurchaseSummary
            enm = EnumForms.Non

        ElseIf key = "frmDefShift" Then
            ControlName = frmDefShift

        ElseIf key = "frmDefShiftGroup" Then
            ControlName = frmDefShiftGroup

        ElseIf key = "frmDefAllocateShiftSchedule" Then
            ControlName = frmDefAllocateShiftSchedule

        ElseIf key = "ChangePassword" Then
            'applystylesheet(ChangePassword)
            ChangePassword.ShowDialog()

        ElseIf key = "frmTerminal" Then
            'applystylesheet(frmTerminal)
            ControlName = frmTerminal
            enm = EnumForms.frmDefUser
            'Reports Section
        ElseIf key = "DailyVoucher" Then

            rptDateRange.ReportName = rptDateRange.ReportList.voucherDetail
            'applystylesheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub

        ElseIf key = "WeightReport" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.WeightReport
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = WeightReport
            enm = EnumForms.WeightReport
            ''19-Mar-2014 TASK:2506 Imran Ali  Add batch quantity and finish goods name in store issue detail report
        ElseIf key = "rptStoreIssuanceDetailBatchWise" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.StoreIssuanceDetailBatchWise
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            'End Task:2506
            ControlName = StoreIssuanceDetailBatchWise
            enm = EnumForms.StoreIssuanceDetailBatchWise
        ElseIf key = "ItemExpiryDate" Then
            ControlName = frmGrdRptItemExpiryDateDetail

        ElseIf key = "BalanceSheetNotesSummary" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.BalanceSheetNotesSummary
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            ControlName = BalanceSheetNotesSummary
            enm = EnumForms.BalanceSheetNotesSummary
        ElseIf key = "BalanceSheetNotes" Then
            ControlName = BalanceSheetNotes
        ElseIf key = "AgeingReceivableSingleDate" Then
            ControlName = AgeingReceivableSingleDate
        ElseIf key = "FreeServiceCard" Then
            ControlName = FreeServiceCard
        ElseIf key = "ChartOfAccounts" Then
            ShowReport("rptChartofAccounts")
            Exit Sub
        ElseIf key = "ListOfItems" Then
            ShowReport("ListOfItems")
            Exit Sub
        ElseIf key = "RepeatedCustomerCount" Then
            If Not getConfigValueByType("CustomerRepeatedCount") = "Error" Then
                Dim RepeatedCustomerCount As Integer = Val(getConfigValueByType("CustomerRepeatedCount"))
                AddRptParam("@RepeatedCustomerCount", RepeatedCustomerCount)
            Else
                AddRptParam("@RepeatedCustomerCount", 1)
            End If
            ShowReport("rptRepeatedCustomerCount")
            Exit Sub
        ElseIf key = "ItemWiseSales" Then
            ControlName = rptItemWiseSales
            enm = EnumForms.rptInventoryForm
        ElseIf key = "ItemWiseSalesReturn" Then
            ControlName = rptItemWiseSalesReturn
            enm = EnumForms.rptInventoryForm
        ElseIf key = "frmRptGrdPLCostCenter" Then
            ControlName = frmRptGrdPLCostCenter
            enm = EnumForms.frmVoucher
        ElseIf key = "JobCardDetail" Then
            ControlName = JobCardDetail
        ElseIf key = "frmDefJobCard" Then
            ControlName = frmDefJobCard
            enm = EnumForms.frmDefJobCard
        ElseIf key = "JobCardSummary" Then
            ControlName = JobCardSummary
        ElseIf key = "frmRptGrdStockInOutDetail" Then
            ControlName = frmRptGrdStockInOutDetail
            enm = EnumForms.frmRptGrdStockInOutDetail
            '' 21-12-2013 ReqID-957   M Ijaz Javed      Bank information entry option
        ElseIf key = "bankbranch" Then
            ControlName = frmBranchNew
            '''''''''''''''''''''''''''''''''
        ElseIf key = "ProductionPlan" Then
            ControlName = frmGrdProductionPlaning
        ElseIf key = "ProductionPlan1" Then
            ControlName = frmGrdProductionPlaning
        ElseIf key = "SalesMarketing" Then
            ControlName = frmLeads
        ElseIf key = "rptPL" Then
            ShowReport("rptProftAndLossStatement")
        ElseIf key = "rptSMTarget" Then
            ''applystylesheet(rptSalesManTarget)
            'rptSalesManTarget.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = rptSalesManTarget
            enm = EnumForms.rptSalesManTarget
        ElseIf key = "rptPLSingleDate" Then
            rptDateRange.ReportName = rptDateRange.ReportList.PLSingleDate
            'applystylesheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "rptPLComparison" Then
            Dim rptPLComparison As New rptPLComparison
            rptPLComparison.ReportName = rptPLComparison.ReportList.PLComparison
            ''applystylesheet(rptPLComparison)
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 07-Feb-2017
            ControlName = rptPLComparison
            enm = EnumForms.rptPLComparison
            'Exit Sub
        ElseIf key = "rptPLAcDetailComparison" Then
            Dim rptPLComparison As New rptPLComparison
            rptPLComparison.ReportName = rptPLComparison.ReportList.PLComparisonDetailAccount
            ''applystylesheet(rptPLComparison)
            'rptPLComparison.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 07-Feb-2017
            ControlName = PLComparisonDetailAccount
            enm = EnumForms.PLComparisonDetailAccount
        ElseIf key = "rptPLAcHeadComparison" Then
            Dim rptPLComparison As New rptPLComparison
            rptPLComparison.ReportName = rptPLComparison.ReportList.PLComparisonSubSubAccount
            ''applystylesheet(rptPLComparison)
            'rptPLComparison.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 07-Feb-2017
            ControlName = PLComparisonSubSubAccount
            enm = EnumForms.PLComparisonSubSubAccount
        ElseIf key = "RcvReport" Then
            rptDateRange.ReportName = rptDateRange.ReportList.ReceivingReport
            'applystylesheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
            '// Report Criteria section
        ElseIf key = "rptLedger" Then
            rptLedger.Text = "Ledger Report"
            'applystylesheet(rptLedger)
            ControlName = rptLedger
            'rptLedger.ShowDialog()
            'Exit Sub
            'ShowReport("Ledger")  
            If Not rptTrialBalance.DrillDown = False Then
                ''Before against request no. 2363
                'rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(1).TabPage.Tab
                'Tsk:2363 tab's index change
                rptLedger.UltraTabControl1.SelectedTab = rptLedger.UltraTabControl1.Tabs(2).TabPage.Tab
                'End Tsk:2363
                rptLedger.DateTimePicker1.Value = rptTrialBalance.DateTimePicker1.Value
                rptLedger.DateTimePicker2.Value = rptTrialBalance.DateTimePicker2.Value
                rptLedger.cmbAccount.Value = rptTrialBalance.grdStock.GetRow.Cells(0).Value
                'rptLedger.cmbCostCenter.SelectedIndex = 0
                rptLedger.GetLedger()
            End If
        ElseIf key = "rptImportLedger" Then
            ControlName = frmGrdImportLedger
            enm = EnumForms.Non
        ElseIf key = "rptLCDetail" Then
            ControlName = frmGrdLCDetail
            enm = EnumForms.Non
        ElseIf key = "frmBalanceSheet" Then
            ControlName = frmBalanceSheet
            IsBackgroundChanged = True
            enm = EnumForms.frmBalanceSheet
        ElseIf key = "frmProfitAndLoss" Then
            ControlName = frmProfitAndLoss
            IsBackgroundChanged = True
            enm = EnumForms.frmProfitAndLoss
        ElseIf key = "rptTrialBalance" Then
            ControlName = rptTrialBalance
            enm = EnumForms.rptTrialBalance
        ElseIf key = "frmRptTrialNew" Then
            ControlName = frmRptTrialNew
            enm = EnumForms.rptTrialBalance
        ElseIf key = "PostDatedCheques" Then
            'applystylesheet(frmRptPostDatedCheques)
            frmRptPostDatedCheques.ShowDialog()
            Exit Sub
        ElseIf key = "PostDateChequeSumm" Then
            ControlName = frmGrdRptPostDatedChequesSummary
            'enm = EnumForms.Non
        ElseIf key = "rptPostDatedCheques" Then
            ControlName = frmGrdRptPostDatedCheques

        ElseIf key = "rptSumOfInv" Then
            'ShowReport("SummaryOfInvoices")
            'rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfSalesInvoices
            ''applystylesheet(rptDateRange)
            'rptDateRange.PnlCostTop = True
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = SummaryofSalesInvoices
            enm = EnumForms.SummaryofSalesInvoices
        ElseIf key = "rptSumOfInvReturn" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.SalesReturnSummary
            'rptDateRange.IsCustomer = True 'Task:2609 Set Satus 
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Task#18082015 by Ahmad Sharif
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = SummaryofSalesInvoicesReturn
            enm = EnumForms.SummaryofSalesInvoicesReturn
        ElseIf key = "rptProduction" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.Production
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            ControlName = frmRptSummaryOfProduction

        ElseIf key = "RptWIP" Then
            rptDateRange.ReportName = rptDateRange.ReportList.WIP
            'applystylesheet(rptDateRange)
            rptDateRange.ShowDialog()
            'End Task#18082015
        ElseIf key = "rptProductionSummary" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.ProductionSummary
            ''applystylesheet(rptDateRange)
            'rptDateRange.PnlCostTop = True
            'rptDateRange.ShowDialog()
            'Exit Sub
            ControlName = frmRptProductionSummary
        ElseIf key = ("rptPurchasereturn") Then
            rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfPurchaseReturn
            rptDateRange.IsVendor = True 'Task:2609 Set Satus 
            'applystylesheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "rptArticleBarcode" Then
            ''applystylesheet(frmRptArticleBarcode)
            'frmRptArticleBarcode.ShowDialog()
            'Exit Sub
            ControlName = frmRptArticleBarcode
            enm = EnumForms.frmRptArticleBarcode
        ElseIf key = ("SumOfPurInv") Then
            rptDateRange.ReportName = rptDateRange.ReportList.SummaryOfPurchaseInvoices
            rptDateRange.IsVendor = True 'Task:2609 Set Satus 
            'applystylesheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = ("rptCashFlow") Then
            'rptDateRange.ReportName = rptDateRange.ReportList.CashFlowStatment
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = CashFlowStatement
            enm = EnumForms.CashFlowStatement
        ElseIf key = ("rptCashFlowStander") Then
            'rptDateRange.ReportName = rptDateRange.ReportList.CashFlowStatmentStander
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = CashFlowStatementStandard
            enm = EnumForms.CashFlowStatementStandard
        ElseIf key = ("rptExpense") Then
            'Me.Cursor = Cursors.WaitCursor
            'rptDateRange.ReportName = rptDateRange.ReportList.rptExpenses
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Me.Cursor = Cursors.Default
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = rptExpenses
            enm = EnumForms.rptExpenses
        ElseIf key = "rptDiscNetRates" Then
            rptDateRange.ReportName = rptDateRange.ReportList.rptDiscountNetRate
            'applystylesheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "BalanceSheet" Then
            rptDateRange.ReportName = rptDateRange.ReportList.rptBSFomated
            'applystylesheet(rptDateRange)
            rptDateRange.ShowDialog()
            Exit Sub
        ElseIf key = "DailySalarySheet" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.DailySalarySheet
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            ControlName = DailySalarySheet
        ElseIf key = "EmpSalarySheet" Then
            ControlName = frmGrdRptEmployeeSalarySheet
        ElseIf key = "DailySalarySheetSummary" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.DailySalarySheetSummary
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            ControlName = DailySalarySheetSummary
        ElseIf key = "rptDeliveryChalanDetail" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.DeliveryChalanDetail
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = DeliveryChallanDetail
            enm = EnumForms.DeliveryChallanDetail
        ElseIf key = "rptDeliveryChalanSummary" Then
            'rptDateRange.ReportName = rptDateRange.ReportList.DeliveryChalanSummary
            ''applystylesheet(rptDateRange)
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = DeliveryChallanSummary
            enm = EnumForms.DeliveryChallanSummary
        ElseIf key = "rptDSRSummary" Then
            ''applystylesheet(frmRptDSRSummary)
            'frmRptDSRSummary.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = frmRptDSRSummary
            enm = EnumForms.frmRptDSRSummary
        ElseIf key = "frmrptRoutePlanGatePass" Then
            ControlName = frmrptRoutePlanGatePass
            enm = EnumForms.frmrptRoutePlanGatePass
        ElseIf key = "rptDSRStatement" Then
            ''applystylesheet(frmRptDSRStatement)
            'frmRptDSRStatement.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = frmRptDSRStatement
            enm = EnumForms.frmRptDSRStatement
        ElseIf key = "AreaWiseProductSales" Then
            ShowReport("rptAreaProdSale")
        ElseIf key = "rptPayables" Then
            Cursor = Cursors.WaitCursor
            AddRptParam("@1stAging", 60)
            AddRptParam("@1stAgingName", "30_60")
            AddRptParam("@2ndAging", 90)
            AddRptParam("@2ndAgingName", "60_90")
            AddRptParam("@3rdAging", 90)
            AddRptParam("@3rdAgingName", "90+")
            ShowReport("AgeingPayable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 00:00:00"), False)
            Cursor = Cursors.Default
        ElseIf key = "rptReceiveables" Then
            Me.Cursor = Cursors.WaitCursor
            AddRptParam("@1stAging", 60)
            AddRptParam("@1stAgingName", "30_60")
            AddRptParam("@2ndAging", 90)
            AddRptParam("@2ndAgingName", "60_90")
            AddRptParam("@3rdAging", 90)
            AddRptParam("@3rdAgingName", "90+")
            ShowReport("AgeingReceivable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 00:00:00"), False)
            Cursor = Cursors.Default
        ElseIf key = "grdrptStock" Then
            ControlName = rptStockForm
            enm = EnumForms.rptStockForm
        ElseIf key = "rptgrdInvForm" Then
            ControlName = rptInventoryForm
            enm = EnumForms.rptInventoryForm
        ElseIf key = "frmStockwithCriteria" Then
            'ControlName = rptStockReportWithCritera
            'enm = EnumForms.rptStockReportWithCritera
            ControlName = rptStockByLocation
            enm = EnumForms.rptInventoryForm
        ElseIf key = "frmCompanyContacts" Then
            ControlName = frmCompanyContacts
        ElseIf key = "areawisesalereport" Then
            ShowReport("rptsales")
        ElseIf key = "SetInventoryLevel" Then
            ControlName = frmInventoryLevel
            enm = EnumForms.frmInventoryLevel
        ElseIf key = "frmRptCustomerSalesAnlysis" Then
            '  ControlName = frmRptCustomerSalesAnlysis
            ' enm = EnumForms.frmInventoryLevel
            'Reports grouping section
            'ElseIf key = "AccountReports" Then
            '   ControlName = Accounts
            '  enm = EnumForms.frmVoucher

        ElseIf key = "CustomerReports" Then
            ControlName = Customer
            enm = EnumForms.frmVoucher

        ElseIf key = "Vendor" Then
            ControlName = Vendors
            enm = EnumForms.frmVoucher
        ElseIf key = "frmGrdRptChart" Then
            ControlName = frmGrdRptCharts
            enm = EnumForms.Non
        ElseIf key = "EmpReport" Then
            ControlName = Employee
            enm = EnumForms.frmVoucher
        ElseIf key = "Inventory" Then
            ControlName = Stock
            enm = EnumForms.frmVoucher
        ElseIf key = "Inventory1" Then
            ControlName = Stock
            enm = EnumForms.frmVoucher
        ElseIf key = "purchase" Then
            ControlName = Purchase
            enm = EnumForms.frmVoucher
        ElseIf key = "purchase1" Then
            ControlName = Purchase
            enm = EnumForms.frmVoucher
        ElseIf key = "sales" Then
            ControlName = Sales
            enm = EnumForms.frmVoucher
        ElseIf key = "sales1" Then
            ControlName = Sales
            enm = EnumForms.frmVoucher
        ElseIf key = "LicenseActivation" Then
            ControlName = frmActiveLicense
        ElseIf key = "RptGrdTopCustomers" Then
            ControlName = RptGrdTopCustomers
            enm = EnumForms.frmVoucher
        ElseIf key = "ItemWiseSalesSummary" Then
            frmRptCustomersSales.ReportName = frmRptCustomersSales.enmReportList.RptCustomerItemSalesSummary
            'applystylesheet(frmRptCustomersSales)
            frmRptCustomersSales.ShowDialog()
            Exit Sub
        ElseIf key = "ItemWiseSalesDetail" Then
            frmRptCustomersSales.ReportName = frmRptCustomersSales.enmReportList.RptCustomerITemSalesDetail
            'applystylesheet(frmRptCustomersSales)
            frmRptCustomersSales.ShowDialog()
            Exit Sub
        ElseIf key = "frmRptGrdInwardgatepass" Then
            'applystylesheet(frmRptGrdInwardgatepass)
            ControlName = frmRptGrdInwardgatepass
        ElseIf key = "ProjectWiseStockLedger" Then
            ControlName = frmGrdRptLocationWiseStockLedger
            enm = EnumForms.frmGrdRptLocationWiseStockLedger
        ElseIf key = "rptProjectWiseLedger" Then
            ControlName = frmGrdRptProjectWiseStockLedger
            enm = EnumForms.frmGrdRptProjectWiseStockLedger
        ElseIf key = "rptStorageBilling" Then
            ControlName = frmRptRental
            enm = EnumForms.frmRptRental
        ElseIf key = "frmHome" Then
            'applystylesheet(frmHome)
            ControlName = frmHome
        ElseIf key = "ItemWiseSalesHistory" Then
            'applystylesheet(RptGridItemSalesHistory)
            ControlName = RptGridItemSalesHistory
            enm = EnumForms.RptGridItemSalesHistory
        ElseIf key = "frmrptGrdProducedItems" Then
            'applystylesheet(frmrptGrdProducedItems)
            ControlName = frmrptGrdProducedItems
        ElseIf key = "frmRptGrdStockStatement" Then
            'applystylesheet(frmRptGrdStockStatement)
            ControlName = frmRptGrdStockStatement
            enm = EnumForms.frmRptGrdStockStatement
        ElseIf key = "RackWiseClosingStock" Then
            'applystylesheet(frmGrdRptRackWiseClosingStock)
            ControlName = frmGrdRptRackWiseClosingStock
            'enm = EnumForms.frmGrdRptRackWiseClosingStock
        ElseIf key = "frmAgreement" Then
            ControlName = frmAgreement
            enm = EnumForms.Non
        ElseIf key = "StoreIssuanceSummary" Then
            frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.RptStoreIssuanceSummary
            'applystylesheet(frmRptEnhancementNew)
            frmRptEnhancementNew.ShowDialog()
            Exit Sub
        ElseIf key = "StoreIssuanceDetailRpt" Then
            frmRptEnhancementNew.Report_Name = frmRptEnhancementNew.ReportList.RptStoreIssuanceDetail
            'applystylesheet(frmRptEnhancementNew)
            frmRptEnhancementNew.ShowDialog()
        ElseIf key = "PLBSNotesSetting" Then
            ControlName = frmBSandPLNotesDetail
        ElseIf key = "frmgrdrptDailyUpdate" Then
            ControlName = frmgrdrptDailyUpdate
        ElseIf key = "frmCostCentreReshuffle" Then
            ControlName = frmCostCentreReshuffle
            enm = EnumForms.frmCostCentreReshuffle
        ElseIf key = "InvoiceBasedPaymentSummaryReport" Then
            ControlName = InvoiceBasedPaymentSummaryReport
            enm = EnumForms.InvoiceBasedPaymentSummaryReport
        ElseIf key = "EmpAttendanceRpt" Then
            'applystylesheet(RptDateRangeEmployees)
            RptDateRangeEmployees.ReportName = RptDateRangeEmployees.ReportList.ReportAttendance
            RptDateRangeEmployees.ShowDialog()
            Exit Sub
        ElseIf key = "frmRptCashDetail" Then
            'applystylesheet(frmRptCashDetail)
            frmRptCashDetail.ShowDialog()
            Exit Sub
        ElseIf key = "EmpAttendanceSummaryRpt" Then
            'applystylesheet(RptDateRangeEmployees)
            RptDateRangeEmployees.ReportName = RptDateRangeEmployees.ReportList.ReportAttendanceSummary
            RptDateRangeEmployees.ShowDialog()
            Me.Cursor = Cursors.Default
            Exit Sub
        ElseIf key = "rptNetSales" Then
            ''applystylesheet(rptDateRange)
            'rptDateRange.ReportName = rptDateRange.ReportList.NetSalesReport
            'rptDateRange.ShowDialog()
            'Exit Sub
            'Ali Faisal : Show as Form instead of Dialog to Implement the Rights of View on 01-Feb-2017
            ControlName = NetSalesReport
            enm = EnumForms.NetSalesReport
        ElseIf key = "frmRptGrdMinMaxPriceSalesDetail" Then
            ControlName = frmRptGrdMinMaxPriceSalesDetail
            'ElseIf key = "SwitchUser" Then
            '    LoginForm4.Text = "Switch User"
            '    LoginForm4.blnSwitchUser = True
            '    LoginForm4.ComboBox1.Enabled = False
            '    LoginForm4.UsernameTextBox.ReadOnly = False
            '    Me.pnlEmptyBackground.Visible = True
            '    LoginForm4.ShowDialog()
            '    'frmmainlogin.Text = "Switch User"
            '    'frmmainlogin.blnSwitchUser = True
            '    'frmmainlogin.ComboBox1.Enabled = False
            '    'frmmainlogin.txtUsername.ReadOnly = False
            '    ''frmmainlogin.Hide()
            '    Me.pnlEmptyBackground.Visible = True
            '    ''frmmainlogin.ShowDialog()
            '    Me.pnlEmptyBackground.Visible = False
            '    Exit Sub
        ElseIf key = "EmpPromotion" Then
            ControlName = frmEmployeePromotion
            enm = EnumForms.Non
            '''''''''''''''''''' Assets Management Menu ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        ElseIf key = "frmAsset" Then
            ControlName = frmAsset
            enm = EnumForms.Non

        ElseIf key = "frmAssetCategory" Then
            ControlName = frmAssetCategory
            enm = EnumForms.Non
        ElseIf key = "AssetType" Then
            ControlName = AssetType
            enm = EnumForms.Non
        ElseIf key = "AssetLocation" Then
            ControlName = frmAssetLocation
            enm = EnumForms.Non
        ElseIf key = "AssetCondition" Then
            ControlName = AssetCondition
            enm = EnumForms.Non
        ElseIf key = "AssetStatus" Then
            ControlName = frmAssetStatus
            enm = EnumForms.Non
        ElseIf key = "AssetsDetail" Then
            ControlName = frmGrdRptAssetsDetail
            enm = EnumForms.Non
        ElseIf key = "Remainder" Then
            ControlName = frmreminder
            ControlName.ShowDialog()
            Exit Sub
        ElseIf key = "frmNotificationTemplates" Then
            ControlName = frmNotificationTemplate
            ControlName.ShowDialog()
            Exit Sub
        ElseIf key = "frmNotificationTemplate" Then
            ControlName = frmNotificationTemplate
            ControlName.ShowDialog()
            Exit Sub
        ElseIf key = "division" Then
            ControlName = frmDefDivision
            enm = EnumForms.Non
        ElseIf key = "payrolldivision" Then
            ControlName = frmDefPayRollDivision
            enm = EnumForms.Non

            'Task:2594 Added Report Employee Salaries Detail
        ElseIf key = "rptEmpSalariesDetail" Then
            ControlName = frmGrdRptGenerelEmployeeSalary
            'End Task:2594

            'Task: 2592 Assign key to open "Employee Over Time" Form
        ElseIf key = "AutoOvertime" Then
            ControlName = frmEmployeeAutoOverTime

        ElseIf key = "frmEmpOverTime" Then
            ControlName = frmEmpOverTimeSchedule
            'End Task: 2592
        ElseIf key = "LeaveEncashment" Then
            'applystylesheet(frmDefLeaveEncashment)
            frmDefLeaveEncashment.ShowDialog()
            Exit Sub
            'Task no 2616 cODE fOR nEWLY Added Field
        ElseIf key = "ConsolidateItemSales" Then
            'applystylesheet(frmGrdRptConsolidateItemSalesCustomerWise)
            ControlName = frmGrdRptConsolidateItemSalesCustomerWise
            'Task End 2616
            'TAASK nO 2624 Adding The Key LateTimeSlot
            'Task:M41 Added Cost Sheet Menu
            'ElseIf key = "defineCostSheet" Then
            '    ControlName = frmCostSheet
            'End Task:M41
        ElseIf key = "defineCostSheet" Then
            Dim ConfigValue As String = getConfigValueByType("CostSheetType")
            If ConfigValue = "Error" Or ConfigValue = "Standard Cost Sheet" Then
                ControlName = frmCostSheet
            Else
                ControlName = frmCostSheetNew
            End If
        ElseIf key = "LateTimeSlot" Then
            'applystylesheet(frmlatetimeSlot)
            frmlatetimeSlot.ShowDialog()
            Exit Sub
            'End Task 2624
        ElseIf key = "TaxRate" Then
            'applystylesheet(frmDefServices)
            'Stock Dispatch commented for rebuild
            'frmDefServices.ShowDialog()
            Exit Sub
            'Task:2725 Add new report product summary.
        ElseIf key = "frmGrdRptProductCustomerWiseReport" Then
            ControlName = frmGrdRptProductCustomerWiseReport
            'End Task:2725
        ElseIf key = "frmGrdRptSalesSummaries" Then
            ControlName = frmGrdRptSalesSummaries
        ElseIf key = "frmGrdRptProductDateWiseReport" Then
            ControlName = frmGrdRptProductDateWiseReport
        ElseIf key = "frmDSRStatementNew" Then
            'applystylesheet(frmDSRStatementNew)
            frmDSRStatementNew.ShowDialog()
            Exit Sub
        ElseIf key = "frmEmployeeArticleCostRate" Then
            ControlName = frmEmployeeArticleCostRate
        ElseIf key = "frmUtilityApplyAverageRate" Then
            ControlName = frmUtilityApplyAverageRate

        ElseIf key = "frmClose" Then
            If Me.Timer1.Enabled = True Then
                Me.Timer1.Enabled = False
                Me.Timer1.Stop()
            Else
                Me.Timer1.Enabled = True
                Me.Timer1.Start()
            End If
            If Me.SplitContainer.Panel2.Controls.Count > 0 Then
                ControlName = Me.SplitContainer.Panel2.Controls(0)
                ControlName.Close()
            End If
            Exit Sub

            'ElseIf key = "EnhancedReports" Then
            '    Shell(str_ApplicationStartUpPath & "\SSC Reports\SSC Reports.exe")
            '    Exit Sub
        ElseIf key = "TodayTopic" Then
            'applystylesheet(frmTodayTopic)
            frmTodayTopic.ShowDialog()
            Exit Sub
        ElseIf key = "frmPurchaseDemand" Then
            ControlName = frmPurchaseDemand
            ''Start TFS2648
            enm = EnumForms.frmPurchase
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            If Tags.Length > 0 Then
                frmPurchaseDemand.Get_All(Tags)
                'Tags = String.Empty
            End If
        ElseIf key = "frmPurchaseDemand1" Then
            ControlName = frmPurchaseDemand
            ''Start TFS2648
            enm = EnumForms.frmPurchase
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            If Tags.Length > 0 Then
                frmPurchaseDemand.Get_All(Tags)
                'Tags = String.Empty
            End If
        ElseIf key = "frmGrdRptNonIntractCustomers" Then
            ControlName = FrmGridRptNonIntractCustomers
        ElseIf key = "frmSiteRegistration" Then
            ControlName = frmSiteRegistration
            ''26-June-2014 TASK2704 Imran Ali Add new functionality cash request in erp.
        ElseIf key = "frmCashRequest" Then
            ControlName = frmCashrequest
            'End Task:2704
        ElseIf key = "Locker" Then
            ControlName = frmLockerConfiguration
        ElseIf key = "NotificationList" Then
            ControlName = frmNotificationList
        ElseIf key = "frmTransferredStockReport" Then
            ControlName = frmTransferredStockReport
            ''15-March-2018 Mohsin Rasheed Add new Form frmAssetsAndLiabilityReport.
        ElseIf key = "AssetsAndLiabilityReport" Then
            ControlName = frmAssetsAndLiabilityReport
            ''16-March-2018 Mohsin Rasheed Add new Form frmBudgetConfiguration.
        ElseIf key = "BudgetConfiguration" Then
            ControlName = frmBudgetConfiguration
            ''20-March-2018 Mohsin Rasheed Add new Form Invoicewiseprofitreport.

        ElseIf key = "InvoiceWiseProfitReport" Then
            ControlName = frmInvoiceWiseProfitReport


        End If

        '        End With

        '// It will check security rights
        'If GetConfigValue("EnableSecurity") = "True" Then
        '    'If Not CheckSecurityRight(ControlName) = True Then Exit Sub
        'End If
        If key = "UpdateVersion" Then
            ControlName.TopLevel = False
            ControlName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            ControlName.Dock = DockStyle.Fill
            Me.SplitContainer.Panel2.Controls.Add(ControlName)
            'applystylesheet(ControlName)
            If IsBackgroundChanged = True Then
                ControlName.BackColor = Color.White
            End If
            IsBackgroundChanged = False
            ControlName.Show()
            ControlName.BringToFront()
            Me.LoadLayouts()
            Exit Sub
        End If

        If Me.BackgroundWorker8.IsBusy Then
            Application.DoEvents()
        End If


        ''R-912 Company Default Login ...
        If flgCompanyRights = True Then
            If Me.BackgroundWorker6.IsBusy Then
                Application.DoEvents()
            End If
            If MyCompanyId = 0 Then
                If CompanyRightsList.Count > 0 Then
                    MyCompanyId = CompanyRightsList.Item(0).CompanyId
                    Dim MyComp As New SBModel.CompanyInfo
                    MyComp = CompanyList.Find(AddressOf GetMyCompany)
                    Me.UltraStatusBar2.Panels("frmCompanyList").Text = MyComp.CompanyName
                End If
            End If
        End If
        'End R-912

        'RestrictForm = ControlName.Name
        'Dim obj As Object = GetFormAccessByArray.Find(AddressOf FindRestrictForm)
        'If (obj IsNot Nothing AndAlso obj.ToString.Length > 0) Then
        '    ControlName = frmdisplay
        '    RestrictForm = String.Empty
        'End If


        If NewSecurityRights = True Then
            'GetFormRights(ControlName.Name)
            Rights = GroupRights.FindAll(AddressOf ReturnRights)
            If Not Rights.Count = 0 Or LoginGroup = "Administrator" Then
                ''Req-918 campare rights and login group
                If Rights.Count > 0 AndAlso LoginGroup <> "Administrator" Then
                    Dim VwRights As SBModel.GroupRights = Rights.Find(AddressOf chkViewFormRights) 'Filter View Rights
                    If VwRights Is Nothing Then
                        msg_Error("You don't have access rights.")
                        Exit Sub
                    End If
                End If
                ControlName.TopLevel = False
                ControlName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                ControlName.Dock = DockStyle.Fill
                Me.SplitContainer.Panel2.Controls.Add(ControlName)
                'applystylesheet(ControlName)
                If IsBackgroundChanged = True Then
                    ControlName.BackColor = Color.White
                End If
                IsBackgroundChanged = False
                ControlName.Show()
                ControlName.BringToFront()
                Me.LoadLayouts()
                'End If
                '  Next
            Else
                If LoginUserId = 0 Then
                    ControlName = frmUserGroup
                    'applystylesheet(ControlName)
                    If IsBackgroundChanged = True Then
                        ControlName.BackColor = Color.White
                    End If
                    IsBackgroundChanged = False
                    ControlName.Show()
                    ControlName.BringToFront()
                    Me.LoadLayouts()
                    '   ToggleFoldersVisible()

                ElseIf key = "frmHome" Or key = "ChangePassword" Then

                Else

                    msg_Information("Sorry! you don't have access rights.")
                    Exit Sub

                End If
            End If
        Else
            ControlName.TopLevel = False
            ControlName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            ControlName.Dock = DockStyle.Fill
            Me.SplitContainer.Panel2.Controls.Add(ControlName)
            Dim dtRights As New DataTable
            Dim rightlist As New Specialized.NameValueCollection
            If IsEnhancedSecurity = True Then
                rightlist = GetFormSecurityControls(ControlName.Name)
            Else
                dtRights = GetFormRights(enm)
            End If
            If enm <> EnumForms.Non Then
                If LoginUserId = 0 Then
                    ControlName = frmDefUser
                    'applystylesheet(ControlName)
                    If IsBackgroundChanged = True Then
                        ControlName.BackColor = Color.White
                    End If
                    IsBackgroundChanged = False
                    ControlName.Show()
                    ControlName.BringToFront()
                    Me.LoadLayouts()
                    ' ToggleFoldersVisible()
                ElseIf dtRights.Rows.Count > 0 AndAlso IsEnhancedSecurity = False Then
                    If dtRights.Rows(0).Item("View_Rights") = True Then
                        'applystylesheet(ControlName)
                        If IsBackgroundChanged = True Then
                            ControlName.BackColor = Color.White
                        End If
                        IsBackgroundChanged = False
                        'If Me.SplitContainer.Panel2.Controls.Count > 0 AndAlso Not ControlName.Name = Me.SplitContainer.Panel2.Controls(0).Name Then
                        Try

                            ControlName.Show()
                            ControlName.BringToFront()
                            Me.LoadLayouts()
                        Catch ex As Exception
                        End Try
                    ElseIf LoginGroup = "Administrator" Then
                        'applystylesheet(ControlName)
                        If IsBackgroundChanged = True Then
                            ControlName.BackColor = Color.White
                        End If
                        IsBackgroundChanged = False
                        'If Me.SplitContainer.Panel2.Controls.Count > 0 AndAlso Not ControlName.Name = Me.SplitContainer.Panel2.Controls(0).Name Then
                        Try
                            ControlName.Show()
                            ControlName.BringToFront()
                            Me.LoadLayouts()
                        Catch ex As Exception
                        End Try
                    End If
                ElseIf IsEnhancedSecurity = True AndAlso rightlist.Count > 0 Then
                    If Not rightlist.Item("View") Is Nothing Then
                        'applystylesheet(ControlName)
                        If IsBackgroundChanged = True Then
                            ControlName.BackColor = Color.White
                        End If
                        IsBackgroundChanged = False
                        'If Me.SplitContainer.Panel2.Controls.Count > 0 AndAlso Not ControlName.Name = Me.SplitContainer.Panel2.Controls(0).Name Then
                        Try
                            ControlName.Show()
                            ControlName.BringToFront()
                            Me.LoadLayouts()
                        Catch ex As Exception
                        End Try
                    End If
                End If
            Else
                'applystylesheet(ControlName)
                If IsBackgroundChanged = True Then
                    ControlName.BackColor = Color.White
                End If
                IsBackgroundChanged = False
                ControlName.Show()
                ControlName.BringToFront()
                Me.LoadLayouts()
            End If
        End If



        If key = "frmusergroup" AndAlso GroupType = EnumGroupType.Administrator.ToString Or key = "frmHome" Or key = "AboutUs" Then
            'If Not Rights Is Nothing Then
            '    For j As Integer = 0 To Rights.Count - 1
            '        If Rights.Item(j).FormControlName = "FirstTabHome" Then
            '            frmHome.UltraTabControl1.SelectedTab = frmHome.UltraTabControl1.Tabs(0).TabPage.Tab
            '        Else
            '            frmHome.UltraTabControl1.SelectedTab = frmHome.UltraTabControl1.Tabs(1).TabPage.Tab
            '        End If
            '    Next
            'Else
            '    frmHome.UltraTabControl1.SelectedTab = frmHome.UltraTabControl1.Tabs(1).TabPage.Tab
            'End If
            ControlName.TopLevel = False
            ControlName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            ControlName.Dock = DockStyle.Fill
            Me.SplitContainer.Panel2.Controls.Add(ControlName)
            'applystylesheet(ControlName)
            If IsBackgroundChanged = True Then
                ControlName.BackColor = Color.White
            End If
            IsBackgroundChanged = False
            ControlName.Show()
            ControlName.BringToFront()
            Me.LoadLayouts()
        End If
        If Not Me.LastControlName.Name = Me.ControlName.Name Then
            LastItem = key.ToString
        End If
        'Tags = String.Empty
        Me.AddHistoryItem(ControlName.Text, key)

        If arHistory.Count > 0 Then
            If Not arHistory.Item(0).ToString = key.ToString Then
                arHistory.Insert(0, key.ToString)
            End If
        Else
            arHistory.Insert(0, key.ToString)

        End If
        Dim Dal As New SBDal.UtilityDAL
        'If Dal.CheckFavouriteForm(ControlName.Name, LoginUserId) Then
        '    ' MsgBox("Form is favourite")

        'End If
    End Sub
    Public Sub GetDrillRecord(ByVal FormName As Form)

        Try
            If FormName.Tag Is Nothing Then Exit Sub
            Select Case FormName.Name
                Case "frmSalesReturn"
                    frmSalesReturn.Get_All(FormName.Tag)

            End Select



        Catch ex As Exception

        End Try
    End Sub

    ''ReqId-918 Added Function 
    Public Function chkViewFormRights(ByVal Rights As SBModel.GroupRights) As Boolean
        Try
            If Rights.FormControlName = "View" Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub AddHistoryItem(ByVal Caption As String, ByVal Key As String)
        Try

            Dim DIR As ToolStripDropDownItem
            DIR = Nothing
            For Each DI As ToolStripDropDownItem In Me.BackToolstripButtom.DropDownItems
                DI.Visible = True
                If DI.Tag.ToString = Key.ToString Then
                    DIR = DI
                End If
            Next
            If Not DIR Is Nothing Then BackToolstripButtom.DropDownItems.Remove(DIR)
            Dim DItem As ToolStripDropDownItem
            DItem = New ToolStripMenuItem
            DItem.Text = Caption.ToString & " [" & Date.Now.ToString("HH:mm:ss") & "]"
            DItem.Tag = Key
            DItem.Visible = False

            BackToolstripButtom.DropDownItems.Insert(0, DItem)

            If BackToolstripButtom.DropDownItems.Count > 1 Then
                BackToolstripButtom.ToolTipText = "Switch back to " & BackToolstripButtom.DropDownItems(1).Text

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub LoadLayouts()

        For Each ctl As Control In ControlName.Controls
            If ctl.HasChildren Then Me.SaveLayouts(ctl)
            If TypeOf ctl Is UltraGrid Then
                Dim grd As UltraGrid = CType(ctl, UltraGrid)
                If System.IO.File.Exists(str_ApplicationStartUpPath & "\ApplicationSettings\" & ControlName.Name & "_" & ctl.Name & ".xml") Then
                    grd.DisplayLayout.LoadFromXml(str_ApplicationStartUpPath & "\ApplicationSettings\" & ControlName.Name & "_" & ctl.Name & ".xml")
                End If
            ElseIf TypeOf ctl Is UltraCombo Then
                Dim cmb As UltraCombo = CType(ctl, UltraCombo)
                If System.IO.File.Exists(str_ApplicationStartUpPath & "\ApplicationSettings\" & ControlName.Name & "_" & ctl.Name & ".xml") Then
                    cmb.DisplayLayout.LoadFromXml(str_ApplicationStartUpPath & "\ApplicationSettings\" & ControlName.Name & "_" & ctl.Name & ".xml")
                End If
            End If
        Next
    End Sub

    Private Sub LockSystemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'frmmainlogin.ShowDialog()
        'LoginForm4.ShowDialog()
    End Sub

    Private Sub BackToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.NextControlName = ControlName
            Me.Nextenm = Me.enm
            ' Me.ForwardToolStripButton.Enabled = True
            Me.BackToolStripButton.Enabled = False

            LastControlName.TopLevel = False
            LastControlName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            LastControlName.Dock = DockStyle.Fill
            Me.SplitContainer.Panel2.Controls.Add(LastControlName)
            If Me.Lastenum <> EnumForms.Non Then
                If GetFormRights(Me.Lastenum).Rows(0).Item("View_Rights") = True Then
                    LastControlName.Show()
                    LastControlName.BringToFront()
                Else
                    msg_Information(str_ErrorViewRight)
                End If
            Else
                LastControlName.Show()
                LastControlName.BringToFront()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ForwardToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToDo.Click
        Try
            'Me.pnlNotification.Visible = False
            'If Not btnToDo.BackColor = Color.Transparent Then
            '    btnToDo.BackColor = Color.Transparent
            '    btnToDo.Font = New Font(btnToDo.Font.FontFamily.Name, btnToDo.Font.Size, FontStyle.Regular)

            'End If
            'If pnlTaskList.Visible = False Then

            '    frmToDoList.TopLevel = False
            '    Me.pnlTaskList.Controls.Add(frmToDoList)
            '    frmToDoList.Dock = DockStyle.Fill
            '    frmToDoList.Show()
            '    pnlTaskList.Visible = True
            '    pnlTaskList.BringToFront()

            '    ' btnToDo.BackColor = Color.FromArgb(249, 180, 84)
            'Else

            '    pnlTaskList.Visible = False

            'End If


            '' Me.NextControlName = ControlName
            'Me.ForwardToolStripButton.Enabled = False
            'Me.BackToolStripButton.Enabled = True

            'NextControlName.TopLevel = False
            'NextControlName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            'NextControlName.Dock = DockStyle.Fill
            'Me.SplitContainer.Panel2.Controls.Add(NextControlName)

            'If Me.Nextenm <> EnumForms.Non Then
            '    If GetFormRights(Me.Nextenm).Rows(0).Item("View_Rights") = True Then
            '        NextControlName.Show()
            '        NextControlName.BringToFront()
            '    Else
            '        msg_Information(str_ErrorViewRight)
            '    End If
            'Else
            '    NextControlName.Show()
            '    NextControlName.BringToFront()
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Sub LoadCQSystem()
        'Dim frm As New CQSystem.frmClientMessages
        'ControlName = frm
        'ControlName.TopLevel = False
        'ControlName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        'ControlName.Dock = DockStyle.Fill
        'Me.SplitContainer.Panel2.Controls.Add(ControlName)
        'ControlName.Show()
        'ControlName.BringToFront()
    End Sub
    'Sub LoadReleaseDownloader()
    '    Try
    '        Dim frm As New ReleaseDownloader
    '        frm.Form1_Shown(Nothing, Nothing)
    '    Catch ex As Exception
    '        ShowErrorMessage("Error occured while downloading release : " & ex.Message)
    '    End Try
    'End Sub
    'Sub LoadAgriusTray()
    '    Try
    '        Dim frm As New AgriusUpdateTray.ReleaseDownloader
    '        frm.ShowInTaskbar = True
    '        frm.ShowDialog()
    '        frm.Visible = False
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub ContentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Me.LoadCQSystem()
    End Sub

    Private Sub HelpToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotification.Click
        Try
            If pnlNotification.Visible = False Then

                btnNotification.BackColor = Color.Transparent
                btnNotification.Font = New Font(btnNotification.Font.FontFamily.Name, btnNotification.Font.Size, FontStyle.Regular)

                pnlNotification.Visible = True
                pnlNotification.BringToFront()

                Dim a = New SBDal.NotificationDAL().MarkNotificationRead(LoginUserId)

                ' btnNotification.BackColor = Color.FromArgb(249, 180, 84)
            Else

                pnlNotification.Visible = False

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'ShowReport("AgeingPayable", "Nothing", "Nothing", Date.Now.Date.ToString("yyyy-M-d 00:00:00"), False)
        'Me.BackgroundWorker1.ReportProgress(100)
        Me.BackgroundWorker1.CancelAsync()
    End Sub

    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.BackgroundWorker1.RunWorkerAsync()
    End Sub


    Private Sub UltraToolbarsManager1_ToolClick(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinToolbars.ToolClickEventArgs)
        If e.Tool.Key = "Exit" Then
            Me.Close()
            Exit Sub
        ElseIf e.Tool.Key = "btndbBackup" Then
            If Me.BackgroundWorker13.IsBusy Then Exit Sub
            Me.BackgroundWorker13.RunWorkerAsync()
            Do While Me.BackgroundWorker13.IsBusy
                Application.DoEvents()
            Loop

        ElseIf e.Tool.Key.Contains("ReleaseNote") Then

            If IO.File.Exists(Application.StartupPath & "\Schema Updates\" & e.Tool.SharedProps.Caption & "\Release notes for version " & e.Tool.SharedProps.Caption.ToString.Replace(".", "") & ".pdf") Then
                Process.Start(Application.StartupPath & "\Schema Updates\" & e.Tool.SharedProps.Caption.ToString & "\Release notes for version " & e.Tool.SharedProps.Caption.ToString.Replace(".", "") & ".pdf")
            End If

        Else
            LoadControl(e.Tool.Key.ToString)
        End If
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        LoadControl("frmHome")
        'btnNotification.Enabled = True
        'btnNotification.Text = 15
        'btnNotification.BackColor = Color.Red
        pnlNotification.Visible = False

    End Sub
    Private Sub BackToolStripButton_DropDownItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles BackToolStripButton.DropDownItemClicked
        Try
            If Not e.ClickedItem.Tag.ToString.Length <= 0 Then
                LoadControl(e.ClickedItem.Tag.ToString)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    '    Try
    '        If Not SendingEmailBgWorker.IsBusy Then SendingEmailBgWorker.RunWorkerAsync()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Try
            Me.Timer2.Enabled = False
            If SendingEmailBgWorker.IsBusy Then Exit Sub
            SendingEmailBgWorker.RunWorkerAsync()
            Do While SendingEmailBgWorker.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
        Finally
            Me.Timer2.Enabled = True
        End Try
    End Sub

    'Task: 2640 SMS Schedule
    Public Sub GetSMSSchedule()
        Try
            Dim dtSchedule As DataTable = GetDataTable("SELECT * FROM tblSMSSchedule WHERE IsCustomer='True' OR IsVendor='True'")
            If dtSchedule IsNot Nothing Then
                If dtSchedule.Rows(0).Item("IsCustomer") = "True" Then
                    Dim dtCustomer As New DataTable
                    dtCustomer = GetDataTable("Select cust.CustomerName,cust.Phone,vch.Debit_Amount-vch.Credit_Amount as Balance, coaDetail.account_type FROM tblCustomer cust Left Outer JOIN" & _
                    "tblCOAMainSubSubDetail coaMain ON cust.AccountId=coaMain.coa_detail_id Left Outer Join" & _
                    "tblVoucherDetail vch ON vch.coa_detail_id=coaMain.coa_detail_id INNER JOIN" & _
                    "tblCOAMainSubSub coaDetail ON coaDetail.main_sub_sub_id=coaMain.main_sub_sub_id" & _
                    "WHERE coaDetail.Account_Type='Customer'")
                    For Each row As DataRow In dtCustomer.Rows

                        'EmailSave = Nothing
                        'Dim toEmail As String = String.Empty
                        'Dim flg As Boolean = False
                        'If IsEmailAlert = True Then
                        '    Dim dtForm As DataTable = GetDataTable("Select ISNULL(EmailAlert,0) as EmailAlert  From tblForm WHERE Form_Name='" & Me.Name & "' AND EmailAlert=1")
                        '    If dtForm.Rows.Count > 0 Then
                        '        flg = True
                        '    Else
                        '        flg = False
                        '    End If
                        '    If flg = True Then
                        '        If AdminEmail <> "" Then
                        '            Dim Email As SBModel.Email
                        '            Email.ToEmail = AdminEmail
                        '            Email.CCEmail = String.Empty
                        '            Email.BccEmail = String.Empty
                        '            Email.Attachment = SourceFile
                        '            Email.Subject = "" & IIf(row.Item("") = "Cash", "Cash Receipt", "Bank Receipt") & " " & setVoucherNo & " "
                        '            Email.Body = "" & IIf(setVoucherType = "Cash", "Cash Receipt", "Bank Receipt") & " " _
                        '            & " " & IIf(setEditMode = False, "of amount " & Total_Amount & " is made", "of amount " & Total_Previouse_Amount & " is updated to " & Total_Amount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By Agrius ERP System"
                        '            Email.Status = "Pending"
                        '            Call New MailSentDAL().Add(Email)
                        '        End If
                        '    End If
                        'End If
                    Next

                ElseIf dtSchedule.Rows(0).Item("IsVendor") = "True" Then

                    Dim str As String = "Select vnd.VendorName,vnd.Mobile,vch.Debit_Amount-vch.Credit_Amount as Balance, coaDetail.account_type FROM tblVendor vnd Left Outer JOIN" & _
                                        "tblCOAMainSubSubDetail coaMain ON vnd.AccountId=coaMain.coa_detail_id Left Outer Join" & _
                                        "tblVoucherDetail vch ON vch.coa_detail_id=coaMain.coa_detail_id INNER JOIN " & _
                                        "tblCOAMainSubSub coaDetail ON coaDetail.main_sub_sub_id=coaMain.main_sub_sub_id" & _
                                        "WHERE coaDetail.Account_Type='Vendor'"
                    Dim dtVendor As New DataTable
                    dtVendor = GetDataTable(str)

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Function SeningAlertMail() As Boolean
        Try

            'If SendingEmailBgWorker.IsBusy Then Exit Function
            'SendingEmailBgWorker.RunWorkerAsync()
            'Do While SendingEmailBgWorker.IsBusy
            '    Application.DoEvents()
            'Loop
            If Not My.Computer.Network.IsAvailable Then Return False
            Dim Email_Send As SBModel.Email
            'Dim AdminEmailId As Integer = Convert.ToInt32(GetConfigValue("AdminEmailId").ToString)
            Dim EmailId As Integer = Convert.ToInt32(getConfigValueByType("DefaultEmailId").ToString)
            Dim dtEmail As DataTable = GetDataTable("Select * From TblDefEmail WHERE EmailId=" & EmailId)
            'Dim dtAdminEmail As DataTable = GetDataTable("Select * From TblDefEmail WHERE EmailId=" & AdminEmailId)
            Dim dtPendingEmail As DataTable = GetDataTable("Select *  From MailSentBoxTable WHERE Status='Pending'")

            If dtEmail IsNot Nothing Then
                If dtEmail.Rows.Count > 0 Then
                    If dtPendingEmail IsNot Nothing Then
                        If dtPendingEmail.Rows.Count > 0 Then
                            For Each r As DataRow In dtPendingEmail.Rows
                                Dim Email As New Net.Mail.MailMessage
                                If r.Item("ToEmail") <> "" Then
                                    Dim sttEmailAdd() As String = r.Item("ToEmail").ToString.Split(";")
                                    For Each str As String In sttEmailAdd
                                        Email.To.Add(str)
                                    Next
                                Else
                                    Email.To.Add(dtEmail.Rows(0).Item("Email").ToString)
                                End If
                                If r.Item("CC") <> "" Then
                                    Email.CC.Add(r.Item("CC").ToString)
                                End If
                                If r.Item("BCC") <> "" Then
                                    Email.Bcc.Add(r.Item("BCC").ToString)
                                End If
                                Email.Subject = r.Item("Subject").ToString
                                Email.Body = r.Item("Body").ToString
                                Email.From = New Net.Mail.MailAddress(dtEmail.Rows(0).Item("Email").ToString, dtEmail.Rows(0).Item("DisplyName").ToString)
                                If IO.File.Exists(r.Item("Attachment").ToString) = True Then
                                    Dim MyAttachments As Net.Mail.Attachment = New Net.Mail.Attachment(r.Item("Attachment").ToString)
                                    Email.Attachments.Add(MyAttachments)
                                End If
                                Dim smtpClient As New Net.Mail.SmtpClient(dtEmail.Rows(0).Item("smtpServer").ToString)
                                smtpClient.Credentials = New System.Net.NetworkCredential(dtEmail.Rows(0).Item("Email"), Decrypt(dtEmail.Rows(0).Item("EmailPassword")))
                                smtpClient.Port = dtEmail.Rows(0).Item("Port")
                                smtpClient.EnableSsl = dtEmail.Rows(0).Item("SSL")
                                smtpClient.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
                                smtpClient.Send(Email)
                                Email.Dispose()
                                If IO.File.Exists(r.Item("Attachment").ToString) = True Then
                                    IO.File.Delete(r.Item("Attachment").ToString)
                                End If
                                Email_Send = New SBModel.Email
                                Email_Send.MailSentBoxId = r.Item("MailSentBoxId")
                                Email_Send.Status = "Sent"
                                Call New SBDal.MailSentDAL().Update(Email_Send)
                            Next
                        End If
                    End If
                End If
            End If
            Return True
        Catch ex As Exception

        End Try
    End Function
    Private Sub SendingEmailBgWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles SendingEmailBgWorker.DoWork
        Try
            SeningAlertMail()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''ReqId-912 Added Button click event
    Private Sub UltraStatusBar2_ButtonClick(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinStatusBar.PanelEventArgs) Handles UltraStatusBar2.ButtonClick
        Try
            If e.Panel.Index = 3 Then
                ''-------------------- get Company List/ User Rights
                'If BackgroundWorker6.IsBusy Then Exit Sub
                'BackgroundWorker6.RunWorkerAsync()
                'Do While BackgroundWorker6.IsBusy
                '    Application.DoEvents()
                'Loop
                'If Not getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                '    NewSecurityRights = getConfigValueByType("NewSecurityRights")
                'End If
                If flgCompanyRights = True Then
                    frmMyCompany.ShowDialog()
                    MyCompanyId = frmMyCompany.CompanyId
                    If MyCompanyId > 0 Then
                        Dim myComp As New SBModel.CompanyInfo
                        myComp = CompanyList.Find(AddressOf GetMyCompany)
                        'Me.Text = Me.Text & " - " & myComp.CompanyName
                        e.Panel.Text = String.Empty
                        e.Panel.Text = myComp.CompanyName 'R-912 Set Company Name on Button's Text 
                    Else
                        Exit Sub
                    End If
                End If
            ElseIf e.Panel.Index = 4 Then
                ''applystylesheet(frmSMSLog)
                'frmSMSLog.ShowDialog()

                If e.Panel.Text = "SMS Error: Agrius ERP is opened multiple times." Then
                    MsgBox("You have opened Agrius ERP multiple times" & Chr(10) & Chr(10) & " To resolve this problem please close extra Agrius ERP applications.", MsgBoxStyle.Critical, "Error")

                    Exit Sub
                End If
                Dim smslog As New frmSMSLog
                'applystylesheet(smslog)
                smslog.Show()
                Exit Sub
            ElseIf e.Panel.Key = "LicenseExpiry" Then
                LoadControl("AboutUs")

            End If
        Catch ex As Exception
            ShowErrorMessage("Error occured while loading form: " & ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker3_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Try
            If flgReminder = True Then dtReminder = getReminders(ReminderFromDate)
        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Try
            If IsOpenMainForm = True Then
                If flgReminder = True Then
                    If Timer3.Tag Is Nothing Then
                        Timer3.Tag = 0
                    End If
                    Timer3.Tag = Val(Timer3.Tag.ToString) + 1

                    If Val(Timer3.Tag.ToString) = 1 Then

                        ReminderFromDate = Date.Now
                        Me.Timer3.Enabled = False
                        If Me.BackgroundWorker3.IsBusy Then Exit Sub
                        Me.BackgroundWorker3.RunWorkerAsync()
                        Exit Sub

                    ElseIf Val(Timer3.Tag.ToString) = 10 Then

                        ShowReminder()
                        Exit Sub

                    ElseIf Val(Timer3.Tag.ToString) < 60 Then

                        Exit Sub
                    End If

                    Timer3.Tag = 0
                    Exit Sub

                    '
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
        Finally
            Me.Timer3.Enabled = True
        End Try
    End Sub

    Sub ShowReminder()
        Try

            If dtReminder IsNot Nothing Then
                If dtReminder.Rows.Count > 0 Then
                    'Dim frm As New frmReminderMessages
                    frmReminderMessages.grdMsgDetail.DataSource = dtReminder.Copy()
                    frmReminderMessages.grdMsgDetail.RetrieveStructure()
                    frmReminderMessages.ApplyGridSettings()
                    frmReminderMessages.TopMost = True
                    'frmReminderMessages.grdMsgDetail.AutoSizeColumns()
                    'applystylesheet(frmReminderMessages)
                    frmReminderMessages.Show()
                    frmReminderMessages.BringToFront()
                    Application.DoEvents()
                    'frmReminderMessages.topmost
                    Exit Sub

                End If
            End If

        Catch ex As Exception
            msg_Error("Error on loading reminders: " & ex.Message)
        End Try
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    'task 2640 SMS Schedule
    Private Sub BackgroundWorker4_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker4.DoWork
        Try
            GetSMSSchedule()
        Catch ex As Exception
            ShowErrorNotification(ex.Message)
        End Try
        'Try
        '    Call GetSMSSchedule()
        '    'Commented Against Request No. RM6
        '    'IsEmailAttachment()
        '    'EmailAlter()
        '    'AdminEmails()
        '    'ShowHeaderCompany()
        '    'FileExportPath()

        'Catch ex As Exception

        'End Try
        'end task 2640
    End Sub
    Private Sub BackgroundWorker23_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker23.DoWork
        'Try
        '    If Birthdate() = True Then
        '        SendAutoEmail()
        '    End If
        'Catch ex As Exception
        '    ShowErrorNotification(ex.Message)
        'End Try
        'Try
        '    Call GetSMSSchedule()
        '    'Commented Against Request No. RM6
        '    'IsEmailAttachment()
        '    'EmailAlter().

        '    'AdminEmails()
        '    'ShowHeaderCompany()
        '    'FileExportPath()

        'Catch ex As Exception

        'End Try
        'end task 2640
    End Sub
    Private Sub BackgroundWorker24_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker24.DoWork
        Try
            ''If DateTime.Now.DayOfWeek.ToString() = "Saturday" Then
            'If DateTime.Now.Hour = "8:00" Then

            'End If

            'GetExpiredEmailData()
            ' '' ''End If
            'SendContractExpiryEmail()
            'GetInvoiceData()
        Catch ex As Exception
            ShowErrorNotification(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker5_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker5.DoWork
        Try
            'Comment against R:M6
            '_EmployeePicPath = getConfigValueByType("EmployeePicturePath").ToString
            '_ArticlePicPath = getConfigValueByType("ArticlePicturePath").ToString
            '_BackupDBPath = getConfigValueByType("BackupDBPath").ToString
            'Dim strQry As String = "Select Config_Value, Isnull(IsActive,0) as IsActive From ConfigValuesTable WHERE Config_Type='DateLock'"
            'Dim dt As New DataTable
            'dt = GetDataTable(strQry)
            'If dt IsNot Nothing Then
            '    If dt.Rows.Count > 0 Then
            '        If IsDBNull(dt.Rows(0)) Then
            '            MyDateLock = Date.Now
            '            flgDateLock = False
            '        Else
            '            MyDateLock = CDate(dt.Rows(0).Item(0).ToString)
            '            flgDateLock = Convert.ToBoolean(dt.Rows(0).Item(1).ToString)
            '        End If
            '    End If
            'End If
        Catch ex As Exception

        End Try
    End Sub
    '' ReqId-918 Comment New Security Configuration And Configuration List Here ...
    Private Sub BackgroundWorker6_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker6.DoWork
        Try
            'GroupRights = New SBDal.GroupRightsBL().GetRights(LoginUserId)
            'If Not getConfigValueByType("NewSecurityRights").ToString = "Error" Then
            '    NewSecurityRights = getConfigValueByType("NewSecurityRights")
            'End If
            GetEnventKeyList()
            GetAllSMSTemplate()
            GetLocationList()
            CompanyList()
            CompanyRightsList()
            LocationRightsList()
            GetDateLock()

            'ConfigValuesDataTable = GetConfigValuesdt()
            'If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            '    flgCompanyRights = getConfigValueByType("CompanyRights")
            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
    '    Try

    '        If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Date.Now.ToString("yyyy-M-d 00:00:00") Then
    '            If DateLock.Lock = True Then
    '                Return True
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Function
    Private Sub BackgroundWorker7_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker7.DoWork
        Try
            'Comment against R:M6
            'Dim dateLock As New SBModel.DateLockBE
            'dateLock = SBDal.DateLockDAL.GetAllDateLock.Find(AddressOf chkDateLock)
            'If dateLock IsNot Nothing Then
            '    If dateLock.DateLock.ToString.Length > 0 Then
            '        flgDateLock = True
            '    Else
            '        flgDateLock = False
            '    End If
            'End If
            'DateLockList = SBDal.DateLockDAL.GetAllDateLock
        Catch ex As Exception

        End Try
    End Sub

    'ReqId-918 Get Configuration List
    Private Sub BackgroundWorker8_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker8.DoWork
        Try
            Dim workingDays As Integer = 0I
            workingDays = GetWorkingDaysInCurrentMonth()
            '   UpdateWorkingDaysConfiguration(workingDays)

            getConfigValueList()
            GroupRights = GetRights(LoginUserId) 'New SBDal.GroupRightsBL().GetRights(LoginUserId)
            'If Not getConfigValueByType("NewSecurityRights").ToString = "Error" Then
            '    NewSecurityRights = Convert.ToBoolean(getConfigValueByType("NewSecurityRights").ToString)
            'End If
            'If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
            '    flgCompanyRights = Convert.ToBoolean(getConfigValueByType("CompanyRights").ToString)
            'End If
            'If Not getConfigValueByType("TotalAmountRounding").ToString = "Error" Then
            '    TotalAmountRounding = Val(getConfigValueByType("TotalAmountRounding").ToString)
            'End If
            If Birthdate() = True Then
                dialouge.Label2.Text = "Its " & BirthdayOfEmployee & " birthday today. Lets Party"
                dialouge.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function BardCodeScanFocused() As Boolean
        Try
            If frmSales.txtBarcodescan.Focused Then
                Return True
            ElseIf frmStoreIssuence.txtBarcodescan.Focused Then
                Return True
            ElseIf frmAttendanceEmployees.txtBarCodeEmpId.Focused Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub BackgroundWorker9_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker9.DoWork
        Try
            'Comment Against R:M6
            'LoginForm4.GetConfigurations()
            ''LoginForm4.getReleaseFromServer()
            'LoginForm4.UserRememberMe()
            'frmmainlogin.GetConfigurations()
            'frmmainlogin.UserRememberMe()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''RM6 Added Worker Completed Event
    Private Sub BackgroundWorker8_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker8.RunWorkerCompleted
        Try
            IsEmailAttachment()
            EmailAlter()
            AdminEmails()
            ShowHeaderCompany()
            FileExportPath()

            _EmployeePicPath = getConfigValueByType("EmployeePicturePath").ToString
            _ArticlePicPath = getConfigValueByType("ArticlePicturePath").ToString
            _BackupDBPath = getConfigValueByType("BackupDBPath").ToString

            'Tsk:2359 Set Configuration Decimal Point In Value 
            If Not getConfigValueByType("DecimalPointInValue").ToString = "Error" Then
                DecimalPointInValue = getConfigValueByType("DecimalPointInValue").ToString
            End If
            If Not getConfigValueByType("DecimalPointInQty").ToString = "Error" Then
                DecimalPointInQty = getConfigValueByType("DecimalPointInQty").ToString
            End If
            'End Task:2359

            If getConfigValueByType("LoadMenuSettings") = "True" Then
                If IO.File.Exists(Application.StartupPath & "\ExpSetting.xml") Then Me.UltraExplorerBar2.LoadFromXml(Application.StartupPath & "\ExpSetting.xml") Else ShowErrorMessage("Some files r missing please contact Agrius-0344-4114000 for more details") : End
            End If
            strAdminMobileNo = getConfigValueByType("AdminMobileNo").ToString
            Dim objConfig As SBModel.ConfigSystem = objConfigValueList.Find(AddressOf FilterConfig)
            If objConfig IsNot Nothing Then
                If IsDBNull(objConfig.Config_Value) Then
                    MyDateLock = Date.Now
                    flgDateLock = False
                Else
                    MyDateLock = CDate(objConfig.Config_Value)
                    flgDateLock = Convert.ToBoolean(objConfig.IsActive)
                End If
            End If




            ''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
            If Not getConfigValueByType("ItemSortOrder").ToString = "Error" Then
                ItemSortOrder = getConfigValueByType("ItemSortOrder").ToString
            End If
            If Not getConfigValueByType("ItemSortOrderByCode").ToString = "Error" Then
                ItemSortOrderByCode = getConfigValueByType("ItemSortOrderByCode").ToString
            End If
            If Not getConfigValueByType("ItemSortOrderByName").ToString = "Error" Then
                ItemSortOrderByName = getConfigValueByType("ItemSortOrderByName").ToString
            End If
            If Not getConfigValueByType("ItemAscending").ToString = "Error" Then
                ItemAscending = getConfigValueByType("ItemAscending").ToString
            End If
            If Not getConfigValueByType("ItemDescending").ToString = "Error" Then
                ItemDescending = getConfigValueByType("ItemDescending").ToString
            End If
            If Not getConfigValueByType("AcSortOrder").ToString = "Error" Then
                AcSortOrder = getConfigValueByType("AcSortOrder").ToString
            End If
            If Not getConfigValueByType("AcSortOrderByCode").ToString = "Error" Then
                AcSortOrderByCode = getConfigValueByType("AcSortOrderByCode").ToString
            End If
            If Not getConfigValueByType("AcSortOrderByName").ToString = "Error" Then
                AcSortOrderByName = getConfigValueByType("AcSortOrderByName").ToString
            End If
            If Not getConfigValueByType("AcAscending").ToString = "Error" Then
                AcAscending = getConfigValueByType("AcAscending").ToString
            End If
            If Not getConfigValueByType("AcDescending").ToString = "Error" Then
                AcDescending = getConfigValueByType("AcDescending").ToString
            End If
            'End Task:2452

            If Not getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                NewSecurityRights = Convert.ToBoolean(getConfigValueByType("NewSecurityRights").ToString)
            End If
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = Convert.ToBoolean(getConfigValueByType("CompanyRights").ToString)
            End If

            If Not getConfigValueByType("TotalAmountRounding").ToString = "Error" Then
                TotalAmountRounding = Val(getConfigValueByType("TotalAmountRounding").ToString)
            End If



            'LoginForm4.GetConfigurations()
            ''LoginForm4.getReleaseFromServer()
            'LoginForm4.UserRememberMe()
            'frmmainlogin.GetConfigurations()
            ''LoginForm4.getReleaseFromServer()
            'frmmainlogin.UserRememberMe()
            'GetAllSMSTemplate()
        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'RM6 Added Function For Filter Config
    Public Function FilterConfig(ByVal Config As SBModel.ConfigSystem) As Boolean
        Try
            If Config.Config_Type = "" Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Task 2640 SMS Schedule Timer
    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        Try
            Me.Timer4.Enabled = False
            If BackgroundWorker4.IsBusy Then Exit Sub
            BackgroundWorker4.RunWorkerAsync()
            Do While BackgroundWorker4.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
        Finally
            Me.Timer4.Enabled = True
        End Try
        'end task 2640
    End Sub
    Private Sub Timer11_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer11.Tick
        Try
            Me.Timer11.Enabled = False
            If BackgroundWorker23.IsBusy Then Exit Sub
            BackgroundWorker23.RunWorkerAsync()
            Do While BackgroundWorker23.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
        Finally
            Me.Timer11.Enabled = True
        End Try
        'end task 2640
    End Sub
    Private Sub Timer12_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer12.Tick
        Try
            Me.Timer12.Enabled = False
            If BackgroundWorker24.IsBusy Then Exit Sub
            BackgroundWorker24.RunWorkerAsync()
            Do While BackgroundWorker24.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
        Finally
            Me.Timer12.Enabled = True
        End Try
        'end task 2640
    End Sub
    Private Sub Timer5_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer5.Tick
        Try
            Timer5.Enabled = False
            Application.DoEvents()
            If System.Net.Dns.GetHostName.ToString.ToUpper <> getConfigValueByType("DNSHostForSMS").ToString.ToUpper Then Exit Sub
            If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
                Me.UltraStatusBar2.Panels("frmSMSLog").Text = "SMS Error: Agrius ERP is opened multiple times."
                Exit Sub
            End If
            Application.DoEvents()
            If BackgroundWorker10.IsBusy Then Exit Sub
            BackgroundWorker10.WorkerReportsProgress = True
            BackgroundWorker10.RunWorkerAsync()
            Do While BackgroundWorker10.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Timer5.Enabled = True
        End Try
    End Sub


    Private Sub BackgroundWorker10_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker10.DoWork
        Dim objSMSLogList As New List(Of SMSLogBE)
        Try
            'Dim objWebClient As New Net.WebClient
            'Try
            If My.Computer.Network.IsAvailable = False Then Exit Sub 'objWebClient.OpenRead("http:\\www.google.com.pk")
            'Catch ex As Exception
            'BackgroundWorker10.ReportProgress(1)
            'Throw New Exception("No internet connection available.")
            'End Try
            'If Val(SoftwareVersion) <= 2 Then Exit Sub
            objSMSLogList = GetPendingSMS()
            If objSMSLogList Is Nothing Then Exit Sub
            If objSMSLogList.Count > 0 Then
                For Each objSMSLog As SMSLogBE In objSMSLogList
                    BackgroundWorker10.ReportProgress(1)
                    'Application.DoEvents()
                    SendBrandedSMS(objSMSLog)
                    BackgroundWorker10.ReportProgress(2)
                    'Application.DoEvents()
                Next
            Else
                Me.BackgroundWorker10.ReportProgress(3)
                'Application.DoEvents()
            End If

        Catch ex As Exception
            BackgroundWorker10.ReportProgress(4)
            'Application.DoEvents()
        Finally
            objSMSLogList.Clear()
        End Try
    End Sub

    Private Sub BackgroundWorker10_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker10.ProgressChanged
        Try
            Me.UltraStatusBar2.Panels("frmSMSLog").Text = String.Empty
            If e.ProgressPercentage = 1 Then
                Me.UltraStatusBar2.Panels("frmSMSLog").Text = "Sending Message..."
            ElseIf e.ProgressPercentage = 2 Then
                Me.UltraStatusBar2.Panels("frmSMSLog").Text = "Message Sent Sucessfully."
            ElseIf e.ProgressPercentage = 3 Then
                Me.UltraStatusBar2.Panels("frmSMSLog").Text = "No Pending Messages."
            ElseIf e.ProgressPercentage = 4 Then
                Me.UltraStatusBar2.Panels("frmSMSLog").Text = "While sending message an error."
                'ElseIf e.ProgressPercentage = 5 Then
                'Me.UltraStatusBar2.Panels("frmSMSLog").Text = "SMS Error: Agrius ERP is opened multiple times."
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Timer6_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer6.Tick
        Try
            Timer6.Enabled = False
            If Not Environment.MachineName.ToUpper.ToString = getConfigValueByType("DNSHostForSMS").ToUpper.ToString Then
                Exit Sub
            End If
            'If RestrictSingleApplicationInstance() = False Then
            '    Exit Sub
            'End If
            If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
                'Me.UltraStatusBar2.Panels("frmSMSLog").Text = "SMS Error: Agrius ERP is opened multiple times."
                Exit Sub
            End If
            BackgroundWorker11.WorkerReportsProgress = True
            If BackgroundWorker11.IsBusy Then Exit Sub
            BackgroundWorker11.RunWorkerAsync()
            Do While BackgroundWorker11.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Timer6.Enabled = True
        End Try
    End Sub
    Private Sub BackgroundWorker11_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker11.DoWork
        Try
            UploadAutoAttendance()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker11_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker11.ProgressChanged
        Try
            If e.ProgressPercentage <= 100 Then

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker11_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker11.RunWorkerCompleted
        Try


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker12_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker12.DoWork
        Try
            If getConfigValueByType("AutoBreakAttendance").ToString = "True" Then
                Dim blnStatus As Boolean = InsertBreakAttendance()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Timer7_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer7.Tick
        Try
            Timer7.Enabled = False
            If Not Environment.MachineName.ToUpper.ToString = getConfigValueByType("DNSHostForSMS").ToUpper.ToString Then
                Exit Sub
            End If
            If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
                'Me.UltraStatusBar2.Panels("frmSMSLog").Text = "SMS Error: Agrius ERP is opened multiple times."
                Exit Sub
            End If
            Me.BackgroundWorker12.WorkerReportsProgress = True
            If Me.BackgroundWorker12.IsBusy Then Exit Sub
            Me.BackgroundWorker12.RunWorkerAsync()
            Do While Me.BackgroundWorker12.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Timer7.Enabled = True
        End Try
    End Sub
    Private Sub BackgroundWorker12_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker12.ProgressChanged
        Try
            If e.ProgressPercentage <= 100 Then
            Else
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker12_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker12.RunWorkerCompleted
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    '2015-04-03 Task@ 2015040001 Enabling Forms According To Version By Ali Ansari
    ' Function to Block forms which not need to be display Ali Ansari
    Public Function GetFormAccess(ByVal key As String) As Boolean
        Try

            If GetProductEdition() = "Corporate Edition" Then
                Return True
            Else

                Return False
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ' Function to Block forms which not need to be display Ali Ansar
    '2015-04-03 Task@ 2015040001 Enabling Forms According To Version By Ali Ansari


    Public Function GetFormAccessByArray() As List(Of String)
        Try

            'Small Business
            'Corporate()
            'Enterprise()
            'Enterprise Plus
            'Custom()

            Dim strFormList As New List(Of String)
            If LicenseVersion = "" Then 'Basic Edition
                'strFormList.Add("FrmLocation")
                strFormList.Add("frmProductionStore")
                strFormList.Add("frmDefEmployee")
                strFormList.Add("frmAttendanceEmployees")
                strFormList.Add("frmEmployeeSalaryVoucher")
                strFormList.Add("frmSiteRegistration")
                strFormList.Add("frmCostCenter")
                strFormList.Add("FrmEmailconfig")
                strFormList.Add("frmTasks")
                strFormList.Add("frmCustomerPlanning")
                strFormList.Add("frmProductionLevel")
                strFormList.Add("frmProductionPlanStatus")
                strFormList.Add("frmCostSheet")
                strFormList.Add("frmSMSConfiguration")
                strFormList.Add("rptTaskAssign")

                strFormList.Add("FrmLocation")

                'Task#119062015 CRM section
                strFormList.Add("frmProjectPortFolio")
                strFormList.Add("frmProjectVisit")
                strFormList.Add("frmProjQuotion")
                strFormList.Add("frmProjectVisitType")
                strFormList.Add("frmRptProjectHistory")
                strFormList.Add("frmTasks")
                strFormList.Add("frmGrdRptQuotationStatus")
                strFormList.Add("frmProjectVisitType")
                strFormList.Add("RptDateRangeEmployees")
                strFormList.Add("frmRptTaskDetail")
                strFormList.Add("SalesMarketing")
                'End Task#119062015 CRM Section

                'Task#119062015 Asset Management section
                strFormList.Add("frmAsset")
                strFormList.Add("frmAssetCategory")
                strFormList.Add("AssetType")
                strFormList.Add("frmAssetLocation")
                strFormList.Add("AssetCondition")
                strFormList.Add("frmAssetStatus")
                strFormList.Add("frmGrdRptAssetsDetail")
                'End Task#119062015 Asset Management section

                'Task#119062015 Site Management section
                strFormList.Add("frmRptCMFADetail")
                'strFormList.Add("frmCMFAAll")
                'strFormList.Add("frmCMFAAll")
                strFormList.Add("frmCMFAAll")
                strFormList.Add("frmGrdRptCMFAllRecords")
                strFormList.Add("frmGrdRptCMFASummary")
                strFormList.Add("frmGrdRptCMFAOfSummaries")
                strFormList.Add("frmRptCMFADetail")
                'End Task#119062015 Site Management section

                'Task#119062015 Production section
                strFormList.Add("frmGrdProductionAnalaysis")
                strFormList.Add("frmMRPlan")
                strFormList.Add("frmStoreIssuence")
                strFormList.Add("frmReturnStoreIssuence")
                strFormList.Add("frmProductionLevel")
                strFormList.Add("frmProductionStore")
                strFormList.Add("frmStockDispatch")
                strFormList.Add("frmStockReceive")
                strFormList.Add("frmCostSheet")
                strFormList.Add("frmProductionPlanStatus")
                strFormList.Add("frmGrdRptProductionLevel")
                strFormList.Add("frmrptGrdProducedItems")
                strFormList.Add("rptDateRange")
                strFormList.Add("frmGrdRptProductionComparison")
                strFormList.Add("frmRptEnhancementNew")
                strFormList.Add("frmRptEnhancementNew")
                strFormList.Add("frmStockStatmentBySize")
                strFormList.Add("frmRptGrdStockStatement")
                strFormList.Add("RptGridItemSalesHistory")
                strFormList.Add("frmRptGrdStockInOutDetail")
                strFormList.Add("frmGrdRptLocationWiseStockLedger")
                strFormList.Add("frmGrdRptProjectWiseStockLedger")
                'End Task#119062015 Production section

                ''Task#20150516 Blcoking block call center,Email COnfigration,Event Configration in basic version Ali Ansari
                strFormList.Add("frmStatus")
                strFormList.Add("frmTypes")
                strFormList.Add("frmLeads")
                strFormList.Add("frmRptTaskDetail")
                'strFormList.Add("frmSMSConfiguration")
                ''Task#20150516 Blcoking block call center,Email COnfigration,Event Configration in basic version Ali Ansari
            ElseIf LicenseVersion = "Small Business" Then 'Small Business Edition
                strFormList.Add("frmCostCenter")
                strFormList.Add("FrmEmailconfig")
                strFormList.Add("frmLeads")
                strFormList.Add("frmTasks")

            ElseIf LicenseVersion = "Corporate" Then 'Corporate Edition
                strFormList.Add("frmLeads")
                strFormList.Add("frmTasks")

                'Task#119062015 CRM section
                strFormList.Add("frmProjectPortFolio")
                strFormList.Add("frmProjectVisit")
                strFormList.Add("frmProjQuotion")
                strFormList.Add("frmProjectVisitType")
                strFormList.Add("frmRptProjectHistory")
                strFormList.Add("frmTasks")
                strFormList.Add("frmGrdRptQuotationStatus")
                strFormList.Add("frmProjectVisitType")
                strFormList.Add("RptDateRangeEmployees")
                strFormList.Add("frmRptTaskDetail")
                strFormList.Add("SalesMarketing")
                'End Task#119062015 CRM Section

                'Task#119062015 Asset Management section
                strFormList.Add("frmAsset")
                strFormList.Add("frmAssetCategory")
                strFormList.Add("AssetType")
                strFormList.Add("frmAssetLocation")
                strFormList.Add("AssetCondition")
                strFormList.Add("frmAssetStatus")
                strFormList.Add("frmGrdRptAssetsDetail")
                'End Task#119062015 Asset Management section

            ElseIf LicenseVersion = "Enterprise" Then 'Enterprise Edition

                'Task#119062015 CRM section
                strFormList.Add("frmProjectPortFolio")
                strFormList.Add("frmProjectVisit")
                strFormList.Add("frmProjQuotion")
                strFormList.Add("frmProjectVisitType")
                strFormList.Add("frmRptProjectHistory")
                strFormList.Add("frmTasks")
                strFormList.Add("frmGrdRptQuotationStatus")
                strFormList.Add("frmProjectVisitType")
                strFormList.Add("RptDateRangeEmployees")
                strFormList.Add("frmRptTaskDetail")
                strFormList.Add("SalesMarketing")
                'End Task#119062015 CRM Section

                'Task#119062015 Asset Management section
                strFormList.Add("frmAsset")
                strFormList.Add("frmAssetCategory")
                strFormList.Add("AssetType")
                strFormList.Add("frmAssetLocation")
                strFormList.Add("AssetCondition")
                strFormList.Add("frmAssetStatus")
                strFormList.Add("frmGrdRptAssetsDetail")
                'End Task#119062015 Asset Management section

            ElseIf LicenseVersion = "Enterprise Plus" Or LicenseVersion = "Custom" Then 'Enterprise Edition Plus

            End If
            Return strFormList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''Altered against Task#20150516 blocking sheet access  Ali Ansari
    ''Making Array of style sheets
    'Public Function GetSheetAccessByArray() As List(Of String)
    '    Try

    '        Dim strSheetList As New List(Of String)
    '        If Val(SoftwareVersion) = 0 Or Val(SoftwareVersion) = 1 Then 'Basic Edition
    '            strSheetList.Add("frmSMSConfiguration")
    '            strSheetList.Add("rptTaskAssign")
    '        ElseIf Val(SoftwareVersion) = 2 Then 'Small Business Edition

    '        ElseIf Val(SoftwareVersion) = 3 Then 'Corporate Edition

    '        ElseIf Val(SoftwareVersion) = 4 Then 'Enterprise Edition

    '        ElseIf Val(SoftwareVersion) = 5 Then 'Enterprise Edition Plus

    '        End If
    '        Return strSheetList
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    ''Altered against Task#20150516 blocking sheet access  Ali Ansari


    Private Function FindRestrictForm(ByVal FormName As String) As Boolean
        Try
            If RestrictForm.Trim.ToUpper = FormName.Trim.ToUpper Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Altered against Task#20150516 blocking sheet access  Ali Ansari
    'Get List of restrict sheets
    Private Function FindRestrictSheet(ByVal Key As String) As Boolean
        Try
            If RestrictSheetAccess.Trim.ToUpper = Key.Trim.ToUpper Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Altered against Task#20150516 blocking sheet access  Ali Ansari
    Public Sub SendSMS()
        Try

            '...................... End Send SMS ................
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub BackgroundWorker13_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker13.DoWork
        Try

            CreateCurrentDBBackup()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker16_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker16.DoWork
        Try
            Dim dt As New DataTable
            dt = GetAllDuePendingInoices()
            dt.AcceptChanges()

            If dt IsNot Nothing Then
                If My.Computer.Network.IsAvailable Then 'checking internete is available or not
                    If Environment.MachineName.ToUpper.ToString = getConfigValueByType("DNSHostForSMS").ToUpper.ToString Then
                        If getConfigValueByType("EmailAlertDueInvoice").ToString = "True" Then
                            SendPendingInvoiceViaEmail(dt)
                        End If
                    Else
                        Exit Sub
                    End If
                End If
            End If

        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker15_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker15.DoWork
        Try
            DateLockPermissionList = New List(Of DateLockPermissionListByUserID)
            Dim dt As New DataTable
            dt = GetDataTable("Select * From tblDateLockPermission WHERE IsNull(Lock,0)=1")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    Dim strUserIDs() As String = r.Item("UserID").ToString.Split(",")
                    If strUserIDs.Length > 0 Then
                        For Each strID As String In strUserIDs
                            If strID.Length > 0 Then
                                If LoginUserId = Val(strID) Then
                                    'blnDateLockPermission = True
                                    Dim obj As New DateLockPermissionListByUserID
                                    If Not IsDBNull(r.Item("DateLockFrom")) Then
                                        obj.DateFrom = r.Item("DateLockFrom")
                                    End If
                                    If Not IsDBNull(r.Item("DateLockTo")) Then
                                        obj.DateTo = r.Item("DateLockTo")
                                    End If
                                    obj.UserID = Val(strID)
                                    DateLockPermissionList.Add(obj)
                                End If
                            End If
                        Next
                    End If
                Next
            Else
                'blnDateLockPermission = False
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub Timer8_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer8.Tick
        Try

            Me.Timer8.Enabled = False
            'Task#03082015 Checking how much instance of ERP application opened in same machine
            If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
                'Me.UltraStatusBar2.Panels("frmSMSLog").Text = "SMS Error: Agrius ERP is opened multiple times."
                Exit Sub
            End If
            'End Task#03082015 
            If BackgroundWorker16.IsBusy Then Exit Sub
            BackgroundWorker16.RunWorkerAsync()
            Do While BackgroundWorker16.IsBusy
                Application.DoEvents()
            Loop

        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
        Finally
            Me.Timer8.Enabled = True
        End Try
    End Sub

    Private Sub BackgroundWorker17_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker17.DoWork
        Try
            If Not Environment.MachineName.ToUpper.ToString = getConfigValueByType("DNSHostForSMS").ToUpper.ToString Then
                Exit Sub
            End If
            If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
                'Me.UltraStatusBar2.Panels("frmSMSLog").Text = "SMS Error: Agrius ERP is opened multiple times."
                Exit Sub
            End If
            AttendanceDayOff()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Task#01082015 add background worker for sending employee attendance report via email
    Private Sub BackgroundWorker18_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker18.DoWork
        Try
            SendEmployeeAttendanceViaEmail()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#01082015

    'Task#03082015 add timer of sending attendance report alert via email 
    Private Sub Timer9_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer9.Tick
        Try

            Me.Timer9.Enabled = False
            'Checking how much instance of ERP application opened in same machine
            If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
                'Me.UltraStatusBar2.Panels("frmSMSLog").Text = "SMS Error: Agrius ERP is opened multiple times."
                Exit Sub
            End If
            If BackgroundWorker18.IsBusy Then Exit Sub
            BackgroundWorker18.RunWorkerAsync()
            Do While BackgroundWorker16.IsBusy
                Application.DoEvents()
            Loop


        Catch ex As Exception
        Finally
            Me.Timer9.Enabled = True
        End Try
    End Sub
    'End Task#03082015
    Private Sub BackgroundWorker19_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker19.DoWork
        Try
            If Not Environment.MachineName.ToUpper.ToString = getConfigValueByType("DNSHostForSMS").ToUpper.ToString Then
                Exit Sub
            End If
            SaveInstallmentSMS()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker20_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker20.DoWork
        Try
            DatabaseBackupPro()
            'HasBackup = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            'HasBackup = True
        End Try
    End Sub

    Sub EnableAutoBackup()
        Try
            Dim flag As Boolean = False

            Dim day As String = getConfigValueByType("BackupScheduleDays")
            Dim str() As String = day.Split("|")
            If str.Length > 0 Then
                For Each s As String In str
                    Dim strday() As String = s.Split("^")
                    If strday.Length > 0 Then
                        If strday(0) = Date.Now.ToString("ddd") AndAlso Convert.ToBoolean(strday(1)) = True Then
                            flag = True
                            Exit For
                        End If
                    End If
                Next
            End If

            If flag = False Then
                Exit Sub
            End If

            Dim schedule As String = getConfigValueByType("BackupSuitableTime")

            Dim schedularr() As String = schedule.Split("^")

            If schedularr.Length > 0 Then
                Dim str1 As String = schedularr(0)
                If str1 = "Any" Then

                    Timer10.Enabled = True

                Else
                    Dim strTimes() As String = schedularr(1).Split("|")
                    If strTimes.Length > 0 Then

                        BackupStartTime = strTimes(0)
                        BackupEndTime = strTimes(1)
                        If Date.Now.ToShortTimeString >= strTimes(0) And Date.Now.ToShortTimeString <= strTimes(1) Then
                            Timer10.Enabled = True
                        ElseIf CType(Date.Now.ToShortTimeString, Date) < CType(strTimes(0), Date) Then

                            Dim interveral As Integer = 60000 * DateDiff(DateInterval.Minute, CType(Date.Now.ToShortTimeString, Date), CType(strTimes(0), Date))

                            If interveral > 0 Then

                                Timer10.Interval = interveral
                                Timer10.Enabled = True

                            End If
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            '// Message displayed here because don't want to handle exception in calling function.
            msg_Error("Error in auto backup schedule settings: " & ex.Message)
        End Try
    End Sub
    Private Sub Timer10_Tick(sender As Object, e As EventArgs) Handles Timer10.Tick
        Try

            Timer10.Enabled = False

            If HasBackup = True Then Exit Sub
            If BackgroundWorker20.IsBusy Then Exit Sub
            Me.BackgroundWorker20.RunWorkerAsync()
            Do While Me.BackgroundWorker20.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            ' Timer10.Enabled = True
        End Try
    End Sub

    Public Function IsCustomizableMDI() As Boolean
        Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand
        Try

            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 300

            cmd.CommandText = ""
            cmd.CommandText = "If exists(select * from information_Schema.tables where table_name='TerminalConfigurationSystems') " & Chr(10) & "  Select Count(*) From TerminalConfigurationSystems WHERE SystemName=N'" & System.Environment.MachineName.ToString & "'"
            Dim intsyscount As Integer = cmd.ExecuteScalar
            If intsyscount > 0 Then
                blnSystemWiseMDI = True
            End If

            cmd.CommandText = ""
            cmd.CommandText = "If exists(select * from information_Schema.tables where table_name='TerminalConfigurationUsers') " & Chr(10) & "  Select Count(*) From TerminalConfigurationUsers WHERE UserId=N'" & LoginUserId & "'"
            Dim intUsercount As Integer = cmd.ExecuteScalar

            If intUsercount > 0 Then
                blnUserWiseMDI = True
            End If


            trans.Commit()

            If intsyscount > 0 Or intUsercount > 0 Then
                Return True
            End If


        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Private Sub BackgroundWorker21_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker21.DoWork
        Try
            LoadUserImages()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker22_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker22.DoWork

        Try
            _IsRemoveLayout = RemoveLayouts(dbVersion, System.Environment.MachineName.ToString())
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub BackgroundWorker22_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker22.RunWorkerCompleted

        Try


            If _IsRemoveLayout = True Then
                If IO.Directory.Exists(Application.StartupPath & "\Layouts") Then
                    Dim strFiles() As String = IO.Directory.GetFiles(Application.StartupPath & "\Layouts", "*_*")
                    If strFiles.Length > 0 Then
                        For Each f As String In strFiles
                            If IO.File.Exists(f) Then
                                IO.File.Delete(f)
                                flg = True
                            End If
                        Next
                    End If
                End If

                Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
                If objCon.State = ConnectionState.Closed Then objCon.Open()
                Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
                Dim cmd As New OleDb.OleDbCommand

                Try

                    cmd.Connection = objCon
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 30
                    cmd.Transaction = trans

                    cmd.CommandText = "INSERT INTO tblLayoutRemove(Release_Version,SystemName,[Status]) VALUES(N'" & dbVersion & "',N'" & System.Environment.MachineName.ToString() & "',1)"
                    cmd.ExecuteNonQuery()
                    trans.Commit()
                Catch ex As Exception
                    trans.Rollback()
                Finally
                    objCon.Close()
                End Try


            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        'If ValidateSystem(System.Environment.MachineName.ToString, GetMACAddress()) = False Then
        '    frmTrialVersion.ShowDialog()
        'End If
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

    'Private Sub bwValidateSystem_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwValidateSystem.DoWork
    '    If ValidateSystem(System.Environment.MachineName.ToString, GetMACAddress()) = False Then
    '        frmTrialVersion.ShowDialog()
    '    End If
    'End Sub
    Sub GetFavouriteFormsList()
        Try
            Dim StrSql As String = "SELECT        tblForms.FormId, tblForms.FormName, tblForms.FormCaption, tblForms.FormModule, tblForms.SortOrder, tblForms.Active, tblForms.AccessKey FROM tblForms INNER JOIN tblFavouriteForms ON tblForms.FormName = tblFavouriteForms.FormName where tblFavouriteForms.UserId=" & LoginUserId & " ORDER BY tblForms.FormModule, tblForms.FormCaption"
            Dim dt As DataTable = GetDataTable(StrSql)
            For Each row As DataRow In dt.Rows
                Dim ToolItem As ToolStripDropDownItem
                ToolItem = New ToolStripMenuItem
                ToolItem.Name = row.Item("FormName").ToString
                ToolItem.Text = row.Item("FormCaption").ToString
                ToolItem.Tag = row.Item("AccessKey").ToString
                Me.FoldersToolStripButton.DropDownItems.Add(ToolItem)

            Next
        Catch ex As Exception
            ' Throw ex
        End Try
    End Sub

    Function GetPendingNotificationsCount(UserId As Integer) As Integer

        Try

            Dim strSQL As String = String.Empty
            strSQL = " SELECT        count(tblGluonNotifications.NotificationId) as NotificationCount  FROM            tblGluonNotificationDetail INNER JOIN " _
                    & " tblGluonNotifications ON tblGluonNotificationDetail.NotificationId = tblGluonNotifications.NotificationId " _
                    & " WHERE        (tblGluonNotificationDetail.ClearStatus = 0) AND (tblGluonNotificationDetail.UserId = " & UserId & ") AND (tblGluonNotifications.ExpireOn > GETDATE()) AND (tblGluonNotificationDetail.ReadStatus = 0) "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows(0).Item(0).ToString)
            Else
                Return 0
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Private Sub FoldersToolStripButton_ButtonClick(sender As Object, e As EventArgs) Handles FoldersToolStripButton.ButtonClick
        Try
            If FoldersToolStripButton.DropDownItems.ContainsKey(ControlName.Name) AndAlso msg_Confirm("Do you want to remove [" & ControlName.Text & "] from favourites?") = False Then
                Exit Sub
            End If
            Dim dal As New SBDal.UtilityDAL
            dal.AddFormToFavourite(ControlName.Name, LoginUserId)

            Dim ToolItem As ToolStripDropDownItem
            ToolItem = New ToolStripMenuItem
            ToolItem.Name = ControlName.Name
            ToolItem.Text = ControlName.Text
            ToolItem.Tag = LastItem.ToString

            If FoldersToolStripButton.DropDownItems.ContainsKey(ControlName.Name) Then
                FoldersToolStripButton.DropDownItems.RemoveByKey(ControlName.Name)
            Else
                FoldersToolStripButton.DropDownItems.Add(ToolItem)
                msg_Information(ControlName.Text & " is added to favourites")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FoldersToolStripButton_DropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles FoldersToolStripButton.DropDownItemClicked
        Try

            LoadControl(e.ClickedItem.Tag)

        Catch ex As Exception

        End Try
    End Sub

    Public Sub BackToolStripButton_ButtonClick(sender As Object, e As EventArgs) Handles BackToolStripButton.ButtonClick
        Try
            If arHistory.Count > 1 Then

                arHistory.RemoveAt(0)
                LoadControl(arHistory.Item(0).ToString)
            End If

            'If BackToolStripButton.DropDownItems.Count > 0 Then

            '    LoadControl(BackToolStripButton.DropDownItems(1).Tag.ToString)

            'Else

            '    ToolStripButton1_Click(Me, Nothing)

            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ShowErrorNotification(ByVal MessageText As String, Optional ByVal MessageStyle As MsgBoxStyle = MsgBoxStyle.Information, Optional ByVal MessageWaitTime As Integer = 10)
        Try
            tmrMessageNotificationLabel.Stop()
            tmrMessageNotificationLabel.Enabled = False
            tmrMessageNotificationLabel.Interval = 1000 * If(MessageWaitTime > 0, MessageWaitTime, 10)

            'If MessageStyle = MsgBoxStyle.Information Or MessageStyle = Nothing Then

            If Me.pnlErrorNotification.Visible = True Then
                Me.pnlErrorNotification.Visible = False
                Application.DoEvents()
            End If

            If MessageStyle = MsgBoxStyle.Critical Then
                pnlErrorNotification.BackColor = Color.FromArgb(238, 17, 17)

            Else
                pnlErrorNotification.BackColor = Color.FromArgb(0, 91, 174)
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

    Sub ValidateLicense()
        Try
            GetLicenseDetails()
            If LicenseVersion = "" Then
            End If
            If LicenseVersion <> String.Empty Then
                Me.Text = Me.Text & " (" & LicenseVersion & ")"
            Else
                Me.Text = Me.Text
            End If
            Me.UltraStatusBar2.Panels("LicenseExpiry").Text = IIf(LicenseExpiryType <> "Monthly", "Service Expiry: ", "License Expiry: ") & LicenseExpiry.ToString("dd-MMM-yyyy")
            If LicenseExpiry <= Date.Now.AddMonths(-1) Then
                LicenseStatus = "Blocked"
                Me.UltraStatusBar2.Panels("LicenseExpiry").Appearance.BackColor = Color.Red
                Me.UltraStatusBar2.Panels("LicenseExpiry").Appearance.ForeColor = Color.White
            ElseIf LicenseExpiry.ToString("dd-MMM-yyyy 23:59:59") < Date.Now Then
                LicenseStatus = "Expired"
                Me.UltraStatusBar2.Panels("LicenseExpiry").Appearance.BackColor = Color.Yellow
            ElseIf LicenseExpiry.ToString("dd-MMM-yyyy 23:59:59") > Date.Now AndAlso LicenseExpiry.ToString("dd-MMM-yyyy 23:59:59") <= Date.Now.AddDays(7) Then
                LicenseStatus = "Expiring"
                Me.UltraStatusBar2.Panels("LicenseExpiry").Appearance.BackColor = Color.Blue
                Me.UltraStatusBar2.Panels("LicenseExpiry").Appearance.ForeColor = Color.White
            Else
                LicenseStatus = "Valid"
            End If
            Dim cn As New SqlClient.SqlConnectionStringBuilder(SBDal.SQLHelper.CON_STR)
            If cn.DataSource.ToUpper.Contains(System.Environment.MachineName.ToUpper.ToString) Then
                'gblnTrialVersion = True
                If (LicenseSystemId1.ToString.Length > 0 AndAlso LicenseSystemId1.ToString.ToUpper = GetMotherboard().ToString.ToUpper) AndAlso (LicenseSystemId2.ToString.Length > 0 AndAlso LicenseSystemId2.ToString.ToUpper = GetBIOS().ToString.ToUpper) Then
                    gblnTrialVersion = False
                Else
                    Dim strMac As String()
                    'Public MACAddressList As String = ""
                    'strMac = GetMACAddressList.Split(",")  'GetMACAddressListNew() is used instead

                    'Added by Syed Irfan Ahmad on 15 Feb 2018, Task 2411
                    If ModGlobel.GetMACAddressListNew(MACAddressList) = False Then
                        Dim gm As New AgriusMessage
                        gm.Message = "Error reading system information. Please contact your System Administrator."
                        gm.ErrorCode = "GEC-LIC-0x000-1302"
                        ModGlobel.AgriusMessageLogger.Log(gm)

                        ShowErrorMessage("Error reading system information. Please contact your System Administrator." & vbCrLf & "Error Code: GEC-LIC-0x000-1302")
                        Exit Sub
                    End If
                    strMac = MACAddressList.Split(",")
                    If strMac.Length > 0 Then
                        For Each Str As String In strMac
                            If Str.ToString.Trim.Replace(" ", "").Length > 4 AndAlso LicenseSystemId = Str.ToString.Trim.Replace(" ", "") Then
                                gblnTrialVersion = False
                                Exit For
                            Else
                                gblnTrialVersion = True
                                Exit For
                            End If
                        Next
                    Else
                        'Added by Syed Irfan Ahmad on 15 Feb 2018, Task 2411
                        Dim gm As New AgriusMessage
                        gm.Message = "Error reading system information. Please contact your System Administrator or Agrius Support."
                        gm.ErrorCode = "GEC-LIC-0x000-1205"
                        ModGlobel.AgriusMessageLogger.Log(gm)

                        ShowErrorMessage("Error reading system information. Please contact your System Administrator or Agrius Support." & vbCrLf & vbCrLf & "Error Code: GEC-LIC-0x000-1205")
                        Exit Sub
                    End If
                End If
                If LicenseDBName.ToString.Length > 0 AndAlso LicenseDBName.ToString.ToUpper <> cn.InitialCatalog.ToString.ToUpper Then
                    'Added by Syed Irfan Ahmad on 15 Feb 2018, Task 2411
                    Dim gm As New AgriusMessage
                    gm.Message = "The license information does not match with currennt system details." & vbCrLf & "Please contact your System Administrator or Agrius Support with the error code."
                    gm.ErrorCode = "GEC-LIC-0x087-1928"
                    gm.Criticality = SBUtility.Utility.MessageCriticality.High
                    gm.Details = "License DB Name: " & LicenseDBName & ", Initial Catalog: " & cn.InitialCatalog.ToString
                    ModGlobel.AgriusMessageLogger.Log(gm)

                    LicenseStatus = "Blocked"
                    ShowErrorMessage("The license information does not match with currennt system details." & vbCrLf & "Please contact your System Administrator or Agrius Support with below error code." & vbCrLf & vbCrLf & "Error Code: GEC-LIC-0x087-1928")
                End If
                If Date.Now > "31-Jan-2017 23:59:59" AndAlso LicenseStatus <> "Blocked" AndAlso gblnTrialVersion Then
                    'Added by Syed Irfan Ahmad on 15 Feb 2018, Task 2411
                    Dim gm As New AgriusMessage
                    gm.Message = "There is a mismatch in system information. Please contact your System Administrator or Agrius Support."
                    gm.ErrorCode = "GEC-LIC-0x655-1820"
                    ModGlobel.AgriusMessageLogger.Log(gm)

                    LicenseStatus = "Blocked"
                    ShowErrorMessage("There is a mismatch in system information. Please contact your System Administrator or Agrius Support." & vbCrLf & vbCrLf & "Error Code: GEC-LIC-0x655-1820")
                End If
            Else
                'TODO: code for terminal license
            End If
            '// Setting Module configurations
            '
            If Not LicenseModuleList Is Nothing AndAlso LicenseModuleList.Trim.Length > 0 Then
                'Me.UltraToolbarsManager1.Tools("mnuAccounts").SharedProps.Visible = False
                'Me.UltraToolbarsManager1.Tools("mnuCashManagement").SharedProps.Visible = False
                'Me.UltraToolbarsManager1.Tools("mnuSales").SharedProps.Visible = False
                'Me.UltraToolbarsManager1.Tools("mnuPurchases").SharedProps.Visible = False
                'Me.UltraToolbarsManager1.Tools("mnuInventory").SharedProps.Visible = False
                'Me.UltraToolbarsManager1.Tools("Production").SharedProps.Visible = False
                'Me.UltraToolbarsManager1.Tools("mnuImport").SharedProps.Visible = False
                'Me.UltraToolbarsManager1.Tools("Human Resource").SharedProps.Visible = False
                'Me.UltraToolbarsManager1.Tools("Payroll").SharedProps.Visible = False
                'Me.UltraToolbarsManager1.Tools("CRM").SharedProps.Visible = False
                'Me.UltraToolbarsManager1.Tools("AssetsManagement").SharedProps.Visible = False
                'Me.UltraToolbarsManager1.Tools("Site Management").SharedProps.Visible = False
                'Me.UltraToolbarsManager1.Tools("grpmnuServices").SharedProps.Visible = False
                For Each mStr As String In LicenseModuleList.Split(",")
                    Select Case mStr.ToString
                        'Case "Accounts"
                        '    Me.UltraToolbarsManager1.Tools("mnuAccounts").SharedProps.Visible = True
                        'Case "Cash"
                        '    Me.UltraToolbarsManager1.Tools("mnuCashManagement").SharedProps.Visible = True
                        'Case "Sales"
                        '    Me.UltraToolbarsManager1.Tools("mnuSales").SharedProps.Visible = True
                        'Case "Purchase"
                        '    Me.UltraToolbarsManager1.Tools("mnuPurchases").SharedProps.Visible = True
                        'Case "Inventory"
                        '    Me.UltraToolbarsManager1.Tools("mnuInventory").SharedProps.Visible = True
                        'Case "Production"
                        '    Me.UltraToolbarsManager1.Tools("Production").SharedProps.Visible = True
                        'Case "Imports"
                        '    Me.UltraToolbarsManager1.Tools("mnuImport").SharedProps.Visible = True
                        'Case "HR"
                        '    Me.UltraToolbarsManager1.Tools("Human Resource").SharedProps.Visible = True
                        'Case "Payroll"
                        '    Me.UltraToolbarsManager1.Tools("Payroll").SharedProps.Visible = True
                        'Case "CRM"
                        '    Me.UltraToolbarsManager1.Tools("CRM").SharedProps.Visible = True
                        'Case "Assets"
                        '    Me.UltraToolbarsManager1.Tools("AssetsManagement").SharedProps.Visible = True
                        'Case "Site"
                        '    Me.UltraToolbarsManager1.Tools("Site Management").SharedProps.Visible = True
                        'Case "Services"
                        '    Me.UltraToolbarsManager1.Tools("grpmnuServices").SharedProps.Visible = True
                    End Select
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub LogosDBKeyExists()
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
                    'dsConnection.ReadXml(str_ApplicationStartUpPath & "\ApplicationSettings\HideShowLogosAndIcons.xml")
                    'Dim dtLogos As DataTable = dsConnection.Tables(0)
                    'If dtLogos.Rows(0).Item(0).ToString = "False" Then
                    '    HideLogosIcons = False
                    '    Me.Panel6.Visible = False
                    '    Me.Panel7.Visible = False
                    'Else
                    '    HideLogosIcons = True
                    '    Me.Panel6.Visible = True
                    '    Me.Panel7.Visible = True
                    '    Me.Icon = Nothing
                    'End If
                    ''If Not dsConnection.Tables(0).Columns.Contains("ReportPath") Then
                    '    dsConnection.Tables(0).Columns.Add("ReportPath")
                    'End If
                End If
            End If
        Catch ex As Exception
            Throw ex

        End Try

    End Sub
    Private Sub frmMain_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged

    End Sub
    Private Sub UltraToolbarsManager1_ToolClick_1(sender As Object, e As UltraWinToolbars.ToolClickEventArgs) Handles UltraToolbarsManager1.ToolClick
        Dim strToolTip As String = String.Empty
        strToolTip = e.Tool.Tag
        e.Tool.Tag = "Loading please wait..."
        Try
            Tags = String.Empty
            LoadControl(e.Tool.Key) 'Me.UltraExplorerBar1.ActiveItem.Key)
            e.Tool.Tag = strToolTip
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub ToolStrip_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)

    End Sub

    Private Sub pnlTaskList_Paint(sender As Object, e As PaintEventArgs) Handles pnlTaskList.Paint

    End Sub

    Private Sub pnlNotification_Paint(sender As Object, e As PaintEventArgs) Handles pnlNotification.Paint

    End Sub

    Private Sub UltraToolbarsManager1_ToolDoubleClick(sender As Object, e As UltraWinToolbars.ToolEventArgs) Handles UltraToolbarsManager1.ToolDoubleClick

    End Sub

    Private Sub UltraStatusBar2_Click(sender As Object, e As EventArgs) Handles UltraStatusBar2.Click

    End Sub

    Private Sub UltraToolbarsManager1_BeforeToolActivate(sender As Object, e As UltraWinToolbars.CancelableToolEventArgs) Handles UltraToolbarsManager1.BeforeToolActivate

    End Sub


    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        LoadControl("frmHome")
        'btnNotification.Enabled = True
        'btnNotification.Text = 15
        'btnNotification.BackColor = Color.Red
        pnlNotification.Visible = False

    End Sub

    Private Sub ToolStripSplitButton1_ButtonClick(sender As Object, e As EventArgs) Handles BackToolstripButtom.ButtonClick
        Try
            If arHistory.Count > 1 Then

                arHistory.RemoveAt(0)
                LoadControl(arHistory.Item(0).ToString)
            End If

            'If BackToolStripButton.DropDownItems.Count > 0 Then

            '    LoadControl(BackToolStripButton.DropDownItems(1).Tag.ToString)

            'Else

            '    ToolStripButton1_Click(Me, Nothing)

            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripSplitButton1_DropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles BackToolstripButtom.DropDownItemClicked
        Try
            If Not e.ClickedItem.Tag.ToString.Length <= 0 Then
                LoadControl(e.ClickedItem.Tag.ToString)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripSplitButton2_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripSplitButton2.ButtonClick
        Try
            If FoldersToolStripButton.DropDownItems.ContainsKey(ControlName.Name) AndAlso msg_Confirm("Do you want to remove [" & ControlName.Text & "] from favourites?") = False Then
                Exit Sub
            End If
            Dim dal As New SBDal.UtilityDAL
            dal.AddFormToFavourite(ControlName.Name, LoginUserId)

            Dim ToolItem As ToolStripDropDownItem
            ToolItem = New ToolStripMenuItem
            ToolItem.Name = ControlName.Name
            ToolItem.Text = ControlName.Text
            ToolItem.Tag = LastItem.ToString

            If FoldersToolStripButton.DropDownItems.ContainsKey(ControlName.Name) Then
                FoldersToolStripButton.DropDownItems.RemoveByKey(ControlName.Name)
            Else
                FoldersToolStripButton.DropDownItems.Add(ToolItem)
                msg_Information(ControlName.Text & " is added to favourites")
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ToolStripSplitButton2_DropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStripSplitButton2.DropDownItemClicked
        Try

            LoadControl(e.ClickedItem.Tag)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Try
            If pnlNotification.Visible = False Then

                btnNotification.BackColor = Color.Transparent
                btnNotification.Font = New Font(btnNotification.Font.FontFamily.Name, btnNotification.Font.Size, FontStyle.Regular)

                pnlNotification.Visible = True
                pnlNotification.BringToFront()

                Dim a = New SBDal.NotificationDAL().MarkNotificationRead(LoginUserId)

                ' btnNotification.BackColor = Color.FromArgb(249, 180, 84)
            Else

                pnlNotification.Visible = False

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStrip_ItemClicked_1(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip.ItemClicked

    End Sub

    'Change for auto generated birthday email Murtaza (11/03/2022)
    Private Sub SendAutoEmail(Optional ByVal Activity As String = "")
        Try
            GetTemplate("Receipt")
            If EmailTemplate.Length > 0 Then
                'GetAutoEmailData()
                Dim query As String = String.Empty
                Dim dt As New DataTable
                query = "Select Count(*) from BirthdayEmailProcess Where (Convert(varchar, BirthdayDate, 102) =   Convert(datetime, '" & Date.Now.ToString("yyyy-M-d 00:00:00") & "', 102)) "
                dt = GetDataTable(query)
                dt.AcceptChanges()
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item(0) > 0 Then
                    Else
                        If Con.Database.Contains("Remms") Then
                            UsersEmail = "everyone@remmsit.com"
                        Else
                            UsersEmail = "everyone@agriusit.com"
                        End If
                        FormatStringBuilder(dtEmail)
                        CreateOutLookMail(UsersEmail)
                        'SaveEmailLog(VoucherNo, UsersEmail, "frmCustomerCollection", Activity)
                    End If
                Else
                    ShowErrorMessage("No email template is found for Receipt.")
                End If
                End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetTemplate(ByVal Title As String)
        Dim Fields As String = String.Empty

        Try
            dtEmail = New DataTable
            EmailTemplate = EmailDAL.GetTemplate(Title)
            If EmailTemplate.Length > 0 Then
                Dim i, j As Integer
                i = EmailTemplate.IndexOf("<Fields>") + "<Fields>".Length
                j = EmailTemplate.IndexOf("</Fields>") - i

                Dim Searched As String = "</Fields>"
                AfterFieldsElement = EmailTemplate.Substring(EmailTemplate.IndexOf(Searched) + Searched.Length)
                Fields = EmailTemplate.Substring(i, j)
                Dim WOAtTheRate As String = Fields.Replace("@", "")
                Dim WOSpace As String = WOAtTheRate.Replace(" ", "")
                Dim IndexOfFieldElement As Integer = EmailTemplate.IndexOf("<Fields>")
                If IndexOfFieldElement > 0 Then
                    EmailTemplate = EmailTemplate.Remove(IndexOfFieldElement)
                End If
                'EmailTemplate = EmailTemplate.Remove(i, j)
                AllFields = New List(Of String)

                dtEmail.Columns.Clear()
                'For Each word As String In WOSpace.Split(",")
                '    Dim TrimSpace As String = word.Trim()
                '    If Me.grd.RootTable.Columns.Contains(TrimSpace) Then
                '        dtEmail.Columns.Add(TrimSpace)
                '        AllFields.Add(TrimSpace)
                '    End If
                'Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    'Private Sub GetAutoEmailData()
    '    'Dim Dr As DataRow
    '    Try
    '        Dim strtest As String
    '        strtest = "Hello"
    '        '    Dim str As String
    '        '    str = "select TOP 1 voucher_id, voucher_no, voucher_date from tblvoucher where source in ('frmCustomerCollection', 'frmOldCustomerCollection') order by 1 desc"
    '        '    Dim dt1 As DataTable = GetDataTable(str)
    '        '    If dt1.Rows.Count > 0 Then
    '        '        VoucherId = dt1.Rows(0).Item("voucher_id")
    '        '        VoucherNo = dt1.Rows(0).Item("voucher_no")
    '        '    End If
    '        '    Dim str1 As String
    '        '    str1 = " SELECT vwCOADetail.detail_title, vwCOADetail.detail_code, ISNULL(tblVoucherDetail.CurrencyAmount, tblVoucherDetail.credit_amount) AS CurrencyAmount, ISNULL(tblVoucherDetail.CurrencyRate, 0) AS CurrencyRate, ISNULL(tblVoucherDetail.BaseCurrencyRate, 0) AS BaseCurrencyRate, tblVoucherDetail.credit_amount AS Amount, tblVoucherDetail.Adjustment AS CurrencyDiscount, ISNULL(tblVoucherDetail.Tax_Percent, 0) AS Tax, ISNULL(tblVoucherDetail.Tax_Amount, 0) AS Tax_Currency_Amount,  ISNULL(tblVoucherDetail.Tax_Amount, 0) AS Tax_Amount, ISNULL(tblVoucherDetail.SalesTaxPer, 0) AS SalesTaxPer,  ISNULL(tblVoucherDetail.SalesTaxAmount, 0) AS SalesTaxAmount, ISNULL(tblVoucherDetail.SalesTaxAmount, 0) AS Sales_Tax, ISNULL(tblVoucherDetail.WHTaxPer, 0) AS WHTaxPer,  ISNULL(tblVoucherDetail.WHTaxAmount, 0) AS WHTaxAmount, ISNULL(tblVoucherDetail.WHTaxAmount, 0) AS WH_Tax, ISNULL(tblVoucherDetail.NetAmount, 0) AS NetAmount, ISNULL(tblVoucherDetail.NetAmount, 0) AS Net_Amount, tblVoucherDetail.comments AS Reference, tblVoucherDetail.Cheque_No, ISNULL(tblVoucherDetail.Cheque_Date, GETDATE()) AS Cheque_Date, tblVoucherDetail.BankDescription, Cust.Mobile,  vwCOADetail.account_type AS Type, SalesMasterTable.SalesNo as InvoiceNo FROM tblVoucherDetail INNER JOIN vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id LEFT OUTER JOIN SalesMasterTable ON tblVoucherDetail.InvoiceId = SalesMasterTable.SalesId LEFT OUTER JOIN tblCustomer AS Cust ON Cust.AccountId = vwCOADetail.coa_detail_id LEFT OUTER JOIN (SELECT DISTINCT VoucherDetailId FROM InvoiceAdjustmentTable) AS InvAdj ON InvAdj.VoucherDetailId = tblVoucherDetail.voucher_detail_id  " _
    '        '      & " Where voucher_id =" & VoucherId & " AND (tblVoucherDetail.credit_amount > 0) Order By tblVoucherDetail.Voucher_Detail_Id ASC "

    '        '    Dim dt As DataTable = GetDataTable(str1)
    '        '    For Each Row1 As DataRow In dt.Rows
    '        '        Dr = dtEmail.NewRow
    '        '        For Each col As String In AllFields
    '        '            If Row1.Table.Columns.Contains(col) Then
    '        '                Dr.Item(col) = Row1.Item(col).ToString
    '        '            End If
    '        '        Next
    '        '        dtEmail.Rows.Add(Dr)
    '        '    Next
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub CreateOutLookMail(ByVal Email As String)
        Try
            Dim senderemail As String
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            Dim OutAccount As Outlook.Account
            'Dim str As String
            'str = "m.ahmad@agriusit.com"
            'Dim dt As DataTable = GetDataTable(str)
            'If dt.Rows.Count > 0 Then
            'senderemail = "v-erp@agriusit.com"
            'OutAccount = oApp.Session.Accounts(senderemail)
            'mailItem.SendUsingAccount = OutAccount
            'End If
            Dim str As String
            str = "select Email from tblUser where User_ID = " & LoginUserId
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                senderemail = dt.Rows(0).Item("Email").ToString
                OutAccount = oApp.Session.Accounts(senderemail)
                mailItem.SendUsingAccount = OutAccount
            End If
            'OutAccount = oApp.Session.Accounts(senderemail)
            'mailItem.SendUsingAccount = OutAccount

            If Birthdate() = True Then
                mailItem.Subject = "Its " & BirthdayOfEmployee & " Birthday Today"
                mailItem.To = Email
                'Email = String.Empty
                mailItem.Importance = Outlook.OlImportance.olImportanceNormal
                mailItem.Display(mailItem)
                Dim myStr As String
                myStr = "Hi " & BirthdayOfEmployee & "," & "<br>" & "<br>" & "Wishing you the best on your birthday and everything good in the year ahead. Hope your day is filled with happiness. Our whole team is wishing you a happy birthday and a wonderful year." & "<br>" & "<br>" & "<b>Best Regards:</b>" & "<br>" & "AgriusIT Team"
                mailItem.HTMLBody = myStr
                'mailItem.HTMLBody = ""
                'EmailBody = html.ToString
                mailItem.Send()
                'Dim ConStrBuilder As New OleDbConnectionStringBuilder(Con.ConnectionString)
                'Dim dbName As String = ConStrBuilder.Item("Initial Catalog")
                'ConStrBuilder.Item("Initial Catalog") = "master"
                'Dim Con1 As New OleDbConnection(ConStrBuilder.ConnectionString.ToString)
                'If Con1.State = ConnectionState.Closed Then Con1.Open()
                ''buLocation = getConfigValueByType("DatabaseBackup").ToString()
                'Application.DoEvents()

                'Dim ServerName As String = ConStrBuilder.DataSource
                'Dim Con2 As New OleDbConnection(ConStrBuilder.ConnectionString.ToString)
                'If Con2.State = ConnectionState.Closed Then Con2.Open()
                'Application.DoEvents()

                Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
                If objCon.State = ConnectionState.Closed Then objCon.Open()
                Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
                Dim cmd As New OleDb.OleDbCommand
                cmd.Connection = objCon
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 30
                cmd.Transaction = trans
                'Dim SqlCommand1 As OleDbCommand = New OleDbCommand()
                'SqlCommand1.Connection = Con2
                cmd.CommandText = "Insert into BirthdayEmailProcess (BirthdayDate, Status, UserID) Values( Convert(datetime, '" & Date.Now.ToString("yyyy-M-d 00:00:00") & "', 102), 1, " & LoginUserId & ") "
                cmd.ExecuteNonQuery()
                trans.Commit()
                Application.DoEvents()
            End If
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FormatStringBuilder(ByVal dt As DataTable)
        Try
            html = New StringBuilder
            html.Append(EmailTemplate)
            html.Append("<table border = '1'>")
            html.Append("<tr bgcolor='#58ACFA'>")
            For Each column As DataColumn In dt.Columns
                Dim ColumnName As String = ""
                Dim Pattern = "([a-z?])[_ ]?([A-Z])"
                If column.ColumnName = "SerialNo" Then
                    ColumnName = "Sr#"
                Else
                    ColumnName = Regex.Replace(column.ColumnName, Pattern, "$1 $2")
                End If
                html.Append("<th>")
                html.Append(ColumnName)
                html.Append("</th>")
            Next
            html.Append("</tr>")

            html.Append("</table>")
            html.Append(AfterFieldsElement)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Change for auto generated birthday email Murtaza (11/03/2022)

    'Change for auto generated notification email Murtaza (02/14/2023)
    Private Sub SendContractExpiryEmail(Optional ByVal Activity As String = "")
        Try
            GetContractExpiryTemplate("ContractExpiry")
            If EmailTemplate.Length > 0 Then
                GetAutoEmailData()
                Dim query As String = String.Empty
                Dim dt As New DataTable
                query = "SP_GetContractExpiryDays"
                dt = GetDataTable(query)
                dt.AcceptChanges()
                'For Each r As DataRow In dt.Rows
                '    If dt.Rows.Count > 0 Then
                '        If r.Item(4) = 90 Then
                '            UsersEmail = "m.ahmad@agriusit.com"
                '            ContractNo = r.Item(2)
                '            FormatStringBuilder1(dtEmail)
                '            CreateOutLookMail90(UsersEmail, ContractNo)
                '        End If
                '        If r.Item(4) = 60 Then
                '            UsersEmail = "m.ahmad@agriusit.com"
                '            ContractNo = r.Item(2)
                '            FormatStringBuilder1(dtEmail)
                '            CreateOutLookMail60(UsersEmail, ContractNo)
                '        End If
                '        If r.Item(4) = 30 Then
                '            UsersEmail = "m.ahmad@agriusit.com"
                '            ContractNo = r.Item(2)
                '            FormatStringBuilder1(dtEmail)
                '            CreateOutLookMail30(UsersEmail, ContractNo)
                '        End If
                '        If r.Item(4) = 15 Then
                '            UsersEmail = "m.ahmad@agriusit.com"
                '            ContractNo = r.Item(2)
                '            FormatStringBuilder1(dtEmail)
                '            CreateOutLookMail15(UsersEmail, ContractNo)
                '        End If
                '        If r.Item(4) = 0 Then
                '            UsersEmail = "m.ahmad@agriusit.com"
                '            ContractNo = r.Item(2)
                '            FormatStringBuilder1(dtEmail)
                '            CreateOutLookMail0(UsersEmail, ContractNo)
                '        End If
                '    End If
                'Next
            Else
                ShowErrorMessage("No email template is found for Purchase Demand.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreateOutLookMail90(ByVal Email As String, ByVal ContractNo As String, ByVal CustomerId As String, ByVal StartDate As String, ByVal EndDate As String)
        Try
            Dim senderemail As String
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            Dim OutAccount As Outlook.Account
            Dim str As String
            'str = "select Email from tblUser where FULLNAME = '" & ConcernEmployee & "'"
            'Dim dt As DataTable = GetDataTable(str)
            'If dt.Rows.Count > 0 Then
            '    senderemail = dt.Rows(0).Item("Email").ToString
            '    OutAccount = oApp.Session.Accounts(senderemail)
            '    mailItem.SendUsingAccount = OutAccount
            'End If
            mailItem.Subject = "Contract Expiry Notification " & ContractNo & " (" & CustomerId & ")"
            mailItem.To = Email
            If Con.Database.Contains("Remms") Then
                mailItem.CC = "adil@remmsit.com"
                mailItem.CC = "m.khan@remmsit.com"
            Else
                mailItem.CC = "adil@agriusit.com"
                mailItem.CC = "m.khan@agriusit.com"
            End If
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            Dim myStr As String
            myStr = "Hi " & ConcernEmployee & "," & "<br>" & "<br>" & ContractNo & " : " & CustomerId & " is going to expire with in 3 months." & "<br>" & "Start Date: " & StartDate & "<br>" & "End Date: " & EndDate
            mailItem.HTMLBody = myStr + html.ToString + mailItem.HTMLBody
            EmailBody = html.ToString
            mailItem.Send()
            Application.DoEvents()
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMail60(ByVal Email As String, ByVal ContractNo As String, ByVal CustomerId As String, ByVal StartDate As String, ByVal EndDate As String)
        Try
            Dim senderemail As String
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            Dim OutAccount As Outlook.Account
            Dim str As String
            str = "select Email from tblUser where User_ID = " & LoginUserId
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                senderemail = dt.Rows(0).Item("Email").ToString
                OutAccount = oApp.Session.Accounts(senderemail)
                mailItem.SendUsingAccount = OutAccount
            End If
            mailItem.Subject = "Contract Expiry Notification " & ContractNo & " (" & CustomerId & ")"
            mailItem.To = Email
            If Con.Database.Contains("Remms") Then
                mailItem.CC = "adil@remmsit.com"
                mailItem.CC = "m.khan@remmsit.com"
            Else
                mailItem.CC = "adil@agriusit.com"
                mailItem.CC = "m.khan@agriusit.com"
            End If
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            Dim myStr As String
            myStr = "Hi " & ConcernEmployee & "," & "<br>" & "<br>" & ContractNo & " : " & CustomerId & " is going to expire with in 2 months." & "<br>" & "Start Date: " & StartDate & "<br>" & "End Date: " & EndDate
            mailItem.HTMLBody = myStr + html.ToString + mailItem.HTMLBody
            EmailBody = html.ToString
            mailItem.Send()
            Application.DoEvents()
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMail30(ByVal Email As String, ByVal ContractNo As String, ByVal CustomerId As String, ByVal StartDate As String, ByVal EndDate As String)
        Try
            Dim senderemail As String
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            Dim OutAccount As Outlook.Account
            Dim str As String
            str = "select Email from tblUser where User_ID = " & LoginUserId
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                senderemail = dt.Rows(0).Item("Email").ToString
                OutAccount = oApp.Session.Accounts(senderemail)
                mailItem.SendUsingAccount = OutAccount
            End If
            mailItem.Subject = "Contract Expiry Notification " & ContractNo & " (" & CustomerId & ")"
            mailItem.To = Email
            If Con.Database.Contains("Remms") Then
                mailItem.CC = "adil@remmsit.com"
                mailItem.CC = "m.khan@remmsit.com"
            Else
                mailItem.CC = "adil@agriusit.com"
                mailItem.CC = "m.khan@agriusit.com"
            End If
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            Dim myStr As String
            myStr = "Hi " & ConcernEmployee & "," & "<br>" & "<br>" & ContractNo & " : " & CustomerId & " is going to expire with in 1 months." & "<br>" & "Start Date: " & StartDate & "<br>" & "End Date: " & EndDate
            mailItem.HTMLBody = myStr + html.ToString + mailItem.HTMLBody
            EmailBody = html.ToString
            mailItem.Send()
            Application.DoEvents()
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMail15(ByVal Email As String, ByVal ContractNo As String, ByVal CustomerId As String, ByVal StartDate As String, ByVal EndDate As String)
        Try
            Dim senderemail As String
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            Dim OutAccount As Outlook.Account
            Dim str As String
            str = "select Email from tblUser where User_ID = " & LoginUserId
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                senderemail = dt.Rows(0).Item("Email").ToString
                OutAccount = oApp.Session.Accounts(senderemail)
                mailItem.SendUsingAccount = OutAccount
            End If
            mailItem.Subject = "Contract Expiry Notification " & ContractNo & " (" & CustomerId & ")"
            mailItem.To = Email
            If Con.Database.Contains("Remms") Then
                mailItem.CC = "adil@remmsit.com"
                mailItem.CC = "m.khan@remmsit.com"
            Else
                mailItem.CC = "adil@agriusit.com"
                mailItem.CC = "m.khan@agriusit.com"
            End If
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            Dim myStr As String
            myStr = "Hi " & ConcernEmployee & "," & "<br>" & "<br>" & ContractNo & " : " & CustomerId & " is going to expire in 15 days." & "<br>" & "Start Date: " & StartDate & "<br>" & "End Date: " & EndDate
            mailItem.HTMLBody = myStr + html.ToString + mailItem.HTMLBody
            EmailBody = html.ToString
            mailItem.Send()
            Application.DoEvents()
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMail0(ByVal Email As String, ByVal ContractNo As String, ByVal CustomerId As String, ByVal StartDate As String, ByVal EndDate As String)
        Try
            Dim senderemail As String
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            Dim OutAccount As Outlook.Account
            Dim str As String
            str = "select Email from tblUser where User_ID = " & LoginUserId
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                senderemail = dt.Rows(0).Item("Email").ToString
                OutAccount = oApp.Session.Accounts(senderemail)
                mailItem.SendUsingAccount = OutAccount
            End If
            mailItem.Subject = "Contract Expiry Notification " & ContractNo & " (" & CustomerId & ")"
            mailItem.To = Email
            If Con.Database.Contains("Remms") Then
                mailItem.CC = "adil@remmsit.com"
                mailItem.CC = "m.khan@remmsit.com"
            Else
                mailItem.CC = "adil@agriusit.com"
                mailItem.CC = "m.khan@agriusit.com"
            End If
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            Dim myStr As String
            myStr = "Hi " & ConcernEmployee & "," & "<br>" & "<br>" & ContractNo & " : " & CustomerId & " is expiring today." & "<br>" & "Start Date: " & StartDate & "<br>" & "End Date: " & EndDate
            mailItem.HTMLBody = myStr + html.ToString + mailItem.HTMLBody
            EmailBody = html.ToString
            mailItem.Send()
            Application.DoEvents()
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMailExpired(ByVal Email As String, ByVal ContractNo As String, ByVal CustomerId As String, ByVal StartDate As String, ByVal EndDate As String)
        Try
            Dim senderemail As String
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            Dim OutAccount As Outlook.Account
            Dim str As String
            str = "select Email from tblUser where User_ID = " & LoginUserId
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                senderemail = dt.Rows(0).Item("Email").ToString
                OutAccount = oApp.Session.Accounts(senderemail)
                mailItem.SendUsingAccount = OutAccount
            End If
            mailItem.Subject = "Contract Expiry Notification " & ContractNo & " (" & CustomerId & ")"
            mailItem.To = Email
            If Con.Database.Contains("Remms") Then
                mailItem.CC = "adil@remmsit.com"
                mailItem.CC = "m.khan@remmsit.com"
            Else
                mailItem.CC = "adil@agriusit.com"
                mailItem.CC = "m.khan@agriusit.com"
            End If
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            Dim myStr As String
            myStr = "Hi " & ConcernEmployee & "," & "<br>" & "<br>" & ContractNo & " : " & CustomerId & " is expired." & "<br>" & "Start Date: " & StartDate & "<br>" & "End Date: " & EndDate
            mailItem.HTMLBody = myStr + html.ToString + mailItem.HTMLBody
            EmailBody = html.ToString
            mailItem.Send()
            Application.DoEvents()
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetAutoEmailData()
        Dim Dr As DataRow
        Try
            Dim query As String = String.Empty
            Dim dt1 As New DataTable
            query = "SP_GetContractExpiryDays"
            dt1 = GetDataTable(query)
            dt1.AcceptChanges()
            For Each r As DataRow In dt1.Rows
                If dt1.Rows.Count > 0 Then
                    If r.Item(4) = 0 Then
                        ContractNo = r.Item(2)
                        CustomerId = r.Item(1)
                        ConcernEmployee = r.Item(5)
                        Dim str1 As String
                        str1 = "SELECT CDT.Brand, CDT.ModelNo, CDT.SerialNo, CDT.SLAInterventionTime, CDT.SLACoverage, convert(varchar(10),CDT.StartDate,101) as StartDate,convert(varchar(10),CDT.EndDate,101) as EndDate ,CDT.Address, CDT.City, CDT.Country FROM ContractDetailTable CDT LEFT OUTER JOIN ContractMasterTable CMT ON CDT.ContractId = CMT.ContractId Where CMT.ContractNo ='" & ContractNo & "'"
                        Dim dt As DataTable = GetDataTable(str1)
                        For Each Row1 As DataRow In dt.Rows
                            Dr = dtEmail.NewRow
                            For Each col As String In AllFields
                                If Row1.Table.Columns.Contains(col) Then
                                    Dr.Item(col) = Row1.Item(col).ToString
                                End If
                            Next
                            dtEmail.Rows.Add(Dr)
                        Next
                        Dim senderemail As String
                        Dim Str As String = "select Email from tblUser where FULLNAME = '" & ConcernEmployee & "'"
                        Dim dt4 As DataTable = GetDataTable(Str)
                        If dt4.Rows.Count > 0 Then
                            senderemail = dt4.Rows(0).Item("Email").ToString
                        End If
                        UsersEmail = senderemail
                        ContractNo = r.Item(2)
                        CustomerId = r.Item(1)
                        StartDate = r.Item(3)
                        EndDate = r.Item(6)
                        FormatStringBuilder1(dtEmail)
                        CreateOutLookMail0(UsersEmail, ContractNo, CustomerId, StartDate, EndDate)
                        dtEmail.Columns.Clear()
                        GetContractExpiryTemplate("ContractExpiry")
                        ConcernEmployee = String.Empty
                    End If
                    If r.Item(4) = 15 Then
                        ContractNo = r.Item(2)
                        CustomerId = r.Item(1)
                        ConcernEmployee = r.Item(5)
                        Dim str1 As String
                        str1 = "SELECT CDT.Brand, CDT.ModelNo, CDT.SerialNo, CDT.SLAInterventionTime, CDT.SLACoverage, convert(varchar(10),CDT.StartDate,101) as StartDate,convert(varchar(10),CDT.EndDate,101) as EndDate ,CDT.Address, CDT.City, CDT.Country FROM ContractDetailTable CDT LEFT OUTER JOIN ContractMasterTable CMT ON CDT.ContractId = CMT.ContractId Where CMT.ContractNo ='" & ContractNo & "'"
                        Dim dt As DataTable = GetDataTable(str1)
                        For Each Row1 As DataRow In dt.Rows
                            Dr = dtEmail.NewRow
                            For Each col As String In AllFields
                                If Row1.Table.Columns.Contains(col) Then
                                    Dr.Item(col) = Row1.Item(col).ToString
                                End If
                            Next
                            dtEmail.Rows.Add(Dr)
                        Next
                        Dim senderemail As String
                        Dim Str As String = "select Email from tblUser where FULLNAME = '" & ConcernEmployee & "'"
                        Dim dt4 As DataTable = GetDataTable(Str)
                        If dt4.Rows.Count > 0 Then
                            senderemail = dt4.Rows(0).Item("Email").ToString
                        End If
                        UsersEmail = senderemail
                        ContractNo = r.Item(2)
                        CustomerId = r.Item(1)
                        StartDate = r.Item(3)
                        EndDate = r.Item(6)
                        FormatStringBuilder1(dtEmail)
                        CreateOutLookMail15(UsersEmail, ContractNo, CustomerId, StartDate, EndDate)
                        dtEmail.Columns.Clear()
                        GetContractExpiryTemplate("ContractExpiry")
                        ConcernEmployee = String.Empty
                    End If
                    If r.Item(4) = 30 Then
                        ContractNo = r.Item(2)
                        CustomerId = r.Item(1)
                        ConcernEmployee = r.Item(5)
                        Dim str1 As String
                        str1 = "SELECT CDT.Brand, CDT.ModelNo, CDT.SerialNo, CDT.SLAInterventionTime, CDT.SLACoverage, convert(varchar(10),CDT.StartDate,101) as StartDate,convert(varchar(10),CDT.EndDate,101) as EndDate ,CDT.Address, CDT.City, CDT.Country FROM ContractDetailTable CDT LEFT OUTER JOIN ContractMasterTable CMT ON CDT.ContractId = CMT.ContractId Where CMT.ContractNo ='" & ContractNo & "'"
                        Dim dt As DataTable = GetDataTable(str1)
                        For Each Row1 As DataRow In dt.Rows
                            Dr = dtEmail.NewRow
                            For Each col As String In AllFields
                                If Row1.Table.Columns.Contains(col) Then
                                    Dr.Item(col) = Row1.Item(col).ToString
                                End If
                            Next
                            dtEmail.Rows.Add(Dr)
                        Next
                        Dim senderemail As String
                        Dim Str As String = "select Email from tblUser where FULLNAME = '" & ConcernEmployee & "'"
                        Dim dt4 As DataTable = GetDataTable(Str)
                        If dt4.Rows.Count > 0 Then
                            senderemail = dt4.Rows(0).Item("Email").ToString
                        End If
                        UsersEmail = senderemail
                        ContractNo = r.Item(2)
                        CustomerId = r.Item(1)
                        StartDate = r.Item(3)
                        EndDate = r.Item(6)
                        FormatStringBuilder1(dtEmail)
                        CreateOutLookMail30(UsersEmail, ContractNo, CustomerId, StartDate, EndDate)
                        dtEmail.Columns.Clear()
                        GetContractExpiryTemplate("ContractExpiry")
                        ConcernEmployee = String.Empty
                    End If
                    If r.Item(4) = 60 Then
                        ContractNo = r.Item(2)
                        CustomerId = r.Item(1)
                        ConcernEmployee = r.Item(5)
                        Dim str1 As String
                        str1 = "SELECT CDT.Brand, CDT.ModelNo, CDT.SerialNo, CDT.SLAInterventionTime, CDT.SLACoverage, convert(varchar(10),CDT.StartDate,101) as StartDate,convert(varchar(10),CDT.EndDate,101) as EndDate ,CDT.Address, CDT.City, CDT.Country FROM ContractDetailTable CDT LEFT OUTER JOIN ContractMasterTable CMT ON CDT.ContractId = CMT.ContractId Where CMT.ContractNo ='" & ContractNo & "'"
                        Dim dt As DataTable = GetDataTable(str1)
                        For Each Row1 As DataRow In dt.Rows
                            Dr = dtEmail.NewRow
                            For Each col As String In AllFields
                                If Row1.Table.Columns.Contains(col) Then
                                    Dr.Item(col) = Row1.Item(col).ToString
                                End If
                            Next
                            dtEmail.Rows.Add(Dr)
                        Next
                        Dim senderemail As String
                        Dim Str As String = "select Email from tblUser where FULLNAME = '" & ConcernEmployee & "'"
                        Dim dt4 As DataTable = GetDataTable(Str)
                        If dt4.Rows.Count > 0 Then
                            senderemail = dt4.Rows(0).Item("Email").ToString
                        End If
                        UsersEmail = senderemail
                        ContractNo = r.Item(2)
                        CustomerId = r.Item(1)
                        StartDate = r.Item(3)
                        EndDate = r.Item(6)
                        FormatStringBuilder1(dtEmail)
                        CreateOutLookMail60(UsersEmail, ContractNo, CustomerId, StartDate, EndDate)
                        dtEmail.Columns.Clear()
                        GetContractExpiryTemplate("ContractExpiry")
                        ConcernEmployee = String.Empty
                    End If
                    If r.Item(4) = 90 Then
                        ContractNo = r.Item(2)
                        CustomerId = r.Item(1)
                        ConcernEmployee = r.Item(5)
                        Dim str1 As String
                        str1 = "SELECT CDT.Brand, CDT.ModelNo, CDT.SerialNo, CDT.SLAInterventionTime, CDT.SLACoverage, convert(varchar(10),CDT.StartDate,101) as StartDate,convert(varchar(10),CDT.EndDate,101) as EndDate,CDT.Address, CDT.City, CDT.Country FROM ContractDetailTable CDT LEFT OUTER JOIN ContractMasterTable CMT ON CDT.ContractId = CMT.ContractId Where CMT.ContractNo ='" & ContractNo & "'"
                        Dim dt As DataTable = GetDataTable(str1)
                        For Each Row1 As DataRow In dt.Rows
                            Dr = dtEmail.NewRow
                            For Each col As String In AllFields
                                If Row1.Table.Columns.Contains(col) Then
                                    Dr.Item(col) = Row1.Item(col).ToString
                                End If
                            Next
                            dtEmail.Rows.Add(Dr)
                        Next
                        Dim senderemail As String
                        Dim Str As String = "select Email from tblUser where FULLNAME = '" & ConcernEmployee & "'"
                        Dim dt4 As DataTable = GetDataTable(Str)
                        If dt4.Rows.Count > 0 Then
                            senderemail = dt4.Rows(0).Item("Email").ToString
                        End If
                        UsersEmail = senderemail
                        ContractNo = r.Item(2)
                        CustomerId = r.Item(1)
                        StartDate = r.Item(3)
                        EndDate = r.Item(6)
                        FormatStringBuilder1(dtEmail)
                        CreateOutLookMail90(UsersEmail, ContractNo, CustomerId, StartDate, EndDate)
                        dtEmail.Columns.Clear()
                        GetContractExpiryTemplate("ContractExpiry")
                        ConcernEmployee = String.Empty
                    End If
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetExpiredEmailData()
        Dim Dr As DataRow
        Try
            GetContractExpiryTemplate("ContractExpiry")
            If EmailTemplate.Length > 0 Then
                Dim Expired As String = String.Empty
                Dim dtexpired As New DataTable
                Expired = "SP_GetExpiredContracts"
                dtexpired = GetDataTable(Expired)
                dtexpired.AcceptChanges()
                For Each r As DataRow In dtexpired.Rows
                    If dtexpired.Rows.Count > 0 Then
                        If (r.Item(4) Mod 7) = 0 Then
                            ContractNo = r.Item(2)
                            CustomerId = r.Item(1)
                            ConcernEmployee = r.Item(5)
                            Dim str1 As String
                            str1 = "SELECT CDT.Brand, CDT.ModelNo, CDT.SerialNo, CDT.SLAInterventionTime, CDT.SLACoverage, convert(varchar(10),CDT.StartDate,101) as StartDate,convert(varchar(10),CDT.EndDate,101) as EndDate ,CDT.Address, CDT.City, CDT.Country FROM ContractDetailTable CDT LEFT OUTER JOIN ContractMasterTable CMT ON CDT.ContractId = CMT.ContractId Where CMT.ContractNo ='" & ContractNo & "'"
                            Dim dt As DataTable = GetDataTable(str1)
                            For Each Row1 As DataRow In dt.Rows
                                Dr = dtEmail.NewRow
                                For Each col As String In AllFields
                                    If Row1.Table.Columns.Contains(col) Then
                                        Dr.Item(col) = Row1.Item(col).ToString
                                    End If
                                Next
                                dtEmail.Rows.Add(Dr)
                            Next
                            Dim senderemail As String
                            Dim Str As String = "select Email from tblUser where FULLNAME = '" & ConcernEmployee & "'"
                            Dim dt4 As DataTable = GetDataTable(Str)
                            If dt4.Rows.Count > 0 Then
                                senderemail = dt4.Rows(0).Item("Email").ToString
                            End If
                            UsersEmail = senderemail
                            ContractNo = r.Item(2)
                            CustomerId = r.Item(1)
                            StartDate = r.Item(3)
                            EndDate = r.Item(6)
                            FormatStringBuilder1(dtEmail)
                            CreateOutLookMailExpired(UsersEmail, ContractNo, CustomerId, StartDate, EndDate)
                            dtEmail.Columns.Clear()
                            GetContractExpiryTemplate("ContractExpiry")
                            ConcernEmployee = String.Empty
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FormatStringBuilder1(ByVal dt As DataTable)
        Try
            html = New StringBuilder
            html.Append(EmailTemplate)
            html.Append("<table border = '1'>")

            'Building the Header row.
            html.Append("<tr bgcolor='#58ACFA'>")
            For Each column As DataColumn In dt.Columns
                Dim ColumnName As String = ""
                Dim Pattern = "([a-z?])[_ ]?([A-Z])"
                ColumnName = Regex.Replace(column.ColumnName, Pattern, "$1 $2")
                html.Append("<th>")
                html.Append(ColumnName)
                html.Append("</th>")
            Next
            html.Append("</tr>")

            'Building the Data rows.
            For Each row As DataRow In dt.Rows
                If row.Table.Columns.Contains("Alternate") Then
                    If row.Item("Alternate") = "Yes" Then
                        html.Append("<tr bgcolor='#A9F5BC'>")
                        For Each column As DataColumn In dt.Columns
                            html.Append("<td>")
                            html.Append(row(column.ColumnName))
                            html.Append("</td>")
                        Next
                        html.Append("</tr>")
                    Else
                        html.Append("<tr>")
                        For Each column As DataColumn In dt.Columns
                            html.Append("<td>")
                            html.Append(row(column.ColumnName))
                            html.Append("</td>")
                        Next
                        html.Append("</tr>")
                    End If
                Else
                    html.Append("<tr>")
                    For Each column As DataColumn In dt.Columns
                        html.Append("<td>")
                        html.Append(row(column.ColumnName))
                        html.Append("</td>")
                    Next
                    html.Append("</tr>")
                End If
            Next
            'Table end.
            html.Append("</table>")
            html.Append(AfterFieldsElement)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Change for auto generated birthday email Murtaza (02/14/2023)

    Private Sub GetContractExpiryTemplate(ByVal Title As String)
        Dim Fields As String = String.Empty
        Try
            dtEmail = New DataTable
            EmailTemplate = EmailDAL.GetTemplate(Title)
            If EmailTemplate.Length > 0 Then
                Dim i, j As Integer
                i = EmailTemplate.IndexOf("<Fields>") + "<Fields>".Length
                j = EmailTemplate.IndexOf("</Fields>") - i

                Dim Searched As String = "</Fields>"
                AfterFieldsElement = EmailTemplate.Substring(EmailTemplate.IndexOf(Searched) + Searched.Length)
                Fields = EmailTemplate.Substring(i, j)
                Dim WOAtTheRate As String = Fields.Replace("@", "")
                Dim WOSpace As String = WOAtTheRate.Replace(" ", "")
                Dim IndexOfFieldElement As Integer = EmailTemplate.IndexOf("<Fields>")
                If IndexOfFieldElement > 0 Then
                    EmailTemplate = EmailTemplate.Remove(IndexOfFieldElement)
                End If
                AllFields = New List(Of String)

                dtEmail.Columns.Clear()
                For Each word As String In WOSpace.Split(",")
                    Dim TrimSpace As String = word.Trim()
                    ' If Me.grdHardware.RootTable.Columns.Contains(TrimSpace) Then
                    dtEmail.Columns.Add(TrimSpace)
                    AllFields.Add(TrimSpace)
                    ' End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub GetInvoiceData()
    '    Dim currentdate As String = String.Empty
    '    currentdate = DateTime.Now.ToString("yyyy-MM-dd")
    '    Dim ContractId As String = String.Empty
    '    'Dim str As String
    '    'str = "select Email from tblUser where User_ID = " & LoginUserId
    '    'Dim dt As DataTable = GetDataTable(str)
    '    'If dt.Rows.Count > 0 Then
    '    '    senderemail = dt.Rows(0).Item("Email").ToString
    '    'End If
    '    Try
    '        Dim COSTCenterID As Integer = 0
    '        Dim LocationID As Integer = 0
    '        Dim CurrencyID As Integer = 0
    '        Dim CurrencyRate As Integer = 0
    '        Dim SalesCustomerID As Integer = 0
    '        Dim SalespersonID As Integer = 0
    '        Dim ArticleDescription As String = String.Empty
    '        Dim query As String = String.Empty
    '        Dim InvoiceMonths As String = String.Empty
    '        Dim AdvanceStartDate As String = String.Empty
    '        Dim AdvanceEndDate As String = String.Empty
    '        Dim Invoiceamount As Integer = 0
    '        Dim SalesAccountId As Integer = 0
    '        Dim coadetailid As Integer = 0
    '        Dim Invoiceyear As Integer = 0
    '        Dim BaseCurrencyId As Integer = getConfigValueByType("Currency").ToString
    '        Dim dt As New DataTable
    '        query = "SELECT InvoiceDetailId,ContractId, InvoiceDate,InvoiceAmount, isnull(previousdate,getdate()) FROM ContractInvoiceDetailTable"
    '        dt = GetDataTable(query)
    '        dt.AcceptChanges()
    '        For Each r As DataRow In dt.Rows
    '            If dt.Rows.Count > 0 Then
    '                If r.Item(2) = currentdate Then
    '                    InvoiceDate = r.Item(2)
    '                    Dim query1 As String = String.Empty
    '                    Dim dt1 As New DataTable
    '                    Dim StartDate1 As String = String.Empty
    '                    Dim EndDate1 As String = String.Empty
    '                    StartDate1 = r.Item(2)
    '                    EndDate1 = r.Item(4)
    '                    Invoiceamount = r.Item(3)
    '                    query1 = "Select * from ContractMasterTable where ContractId = " & r.Item(1) & " "
    '                    dt1 = GetDataTable(query1)
    '                    dt1.AcceptChanges()
    '                    For Each C As DataRow In dt1.Rows
    '                        Dim ContractNo As String = String.Empty
    '                        Dim StartDate As String = String.Empty
    '                        Dim EndDate As String = String.Empty
    '                        Dim CustomerId As String = String.Empty
    '                        Dim OpportunityId As String = String.Empty
    '                        Dim Status As String = String.Empty
    '                        Dim EndCustomer As String = String.Empty
    '                        Dim Site As String = String.Empty
    '                        Dim InvoicingFrequency As String = String.Empty
    '                        Dim PaymentTerms As String = String.Empty
    '                        Dim Comments As String = String.Empty
    '                        Dim Currency As String = String.Empty
    '                        Dim Amount As String = String.Empty
    '                        Dim Employee As String = String.Empty
    '                        Dim ContractType As String = String.Empty
    '                        Dim TerminateStatus As String = String.Empty
    '                        Dim ChkBoxBatteriesIncluded As String = String.Empty
    '                        Dim VoucherNo As String = String.Empty
    '                        Dim ArticleId As Integer = 0
    '                        Dim Tax As Double = 0
    '                        ContractNo = C.Item(1)
    '                        StartDate = C.Item(2)
    '                        EndDate = C.Item(3)
    '                        CustomerId = C.Item(6)
    '                        OpportunityId = C.Item(7)
    '                        Status = C.Item(8)
    '                        EndCustomer = C.Item(9)
    '                        Site = C.Item(12)
    '                        InvoicingFrequency = C.Item(13)
    '                        PaymentTerms = C.Item(14)
    '                        Comments = C.Item(15)
    '                        Currency = C.Item(16)
    '                        Amount = C.Item(17)
    '                        Employee = C.Item(18)
    '                        ArticleId = C.Item(27)
    '                        Tax = C.Item(28)
    '                        InvoiceNo = GetDocumentNo(InvoiceDate)
    '                        Dim Query2 As String = String.Empty
    '                        Dim dt2 As New DataTable
    '                        Query2 = "SELECT * FROM tblDefCostCenter where Name = '" & C.Item(1) & "'"
    '                        dt2 = GetDataTable(Query2)
    '                        dt2.AcceptChanges()
    '                        For Each D As DataRow In dt2.Rows
    '                            COSTCenterID = D.Item(0)
    '                        Next
    '                        Dim Query3 As String = String.Empty
    '                        Dim dt3 As New DataTable
    '                        Query3 = "SELECT * FROM tblDefLocation where location_code Like '%VIR-%'"
    '                        dt3 = GetDataTable(Query3)
    '                        dt3.AcceptChanges()
    '                        For Each D As DataRow In dt3.Rows
    '                            LocationID = D.Item(0)
    '                        Next
    '                        Dim Query4 As String = String.Empty
    '                        Dim dt4 As New DataTable
    '                        Query4 = "GetInvoiceMonth'" & EndDate1 & "','" & StartDate1 & "' "
    '                        dt4 = GetDataTable(Query4)
    '                        dt4.AcceptChanges()
    '                        For Each D As DataRow In dt4.Rows
    '                            Invoiceyear = D.Item(0)
    '                            InvoiceMonths = D.Item(1)
    '                        Next
    '                        Dim Query5 As String = String.Empty
    '                        Dim dt5 As New DataTable
    '                        Query5 = "Select * from ContractInvoiceDetailTable where ContractId = " & r.Item(1) & " AND InvoiceDate >= CAST(GETDATE() AS DATE) "
    '                        dt5 = GetDataTable(Query5)
    '                        dt5.AcceptChanges()
    '                        If dt5.Rows.Count > 1 Then
    '                            AdvanceStartDate = dt5.Rows(0).Item(2)
    '                            AdvanceEndDate = dt5.Rows(1).Item(2)
    '                            Dim Query6 As String = String.Empty
    '                            Dim dt6 As New DataTable
    '                            Query6 = "GetInvoiceMonth'" & AdvanceStartDate & "','" & AdvanceEndDate & "' "
    '                            dt6 = GetDataTable(Query6)
    '                            dt6.AcceptChanges()
    '                            For Each E As DataRow In dt6.Rows
    '                                Invoiceyear = E.Item(0)
    '                                InvoiceMonths = E.Item(1)
    '                            Next
    '                        ElseIf dt5.Rows.Count = 1 Then
    '                            AdvanceStartDate = dt5.Rows(0).Item(2)
    '                            AdvanceStartDate = EndDate
    '                            Dim Query7 As String = String.Empty
    '                            Dim dt7 As New DataTable
    '                            Query7 = "GetInvoiceMonth'" & AdvanceStartDate & "','" & AdvanceStartDate & "' "
    '                            dt7 = GetDataTable(Query7)
    '                            dt7.AcceptChanges()
    '                            For Each E As DataRow In dt7.Rows
    '                                Invoiceyear = E.Item(0)
    '                                InvoiceMonths = E.Item(1)
    '                            Next
    '                        End If
    '                        Dim Query8 As String = String.Empty
    '                        Dim dt8 As New DataTable
    '                        Query8 = "SELECT currency_id FROM tblcurrency where currency_code='" & Currency & "'"
    '                        dt8 = GetDataTable(Query8)
    '                        dt8.AcceptChanges()
    '                        For Each D As DataRow In dt8.Rows
    '                            CurrencyID = D.Item(0)
    '                        Next
    '                        Dim Query9 As String = String.Empty
    '                        Dim dt9 As New DataTable
    '                        Query9 = "SELECT CurrencyRate FROM tblCurrencyRate where CurrencyId=" & CurrencyID & ""
    '                        dt9 = GetDataTable(Query9)
    '                        dt9.AcceptChanges()
    '                        For Each D As DataRow In dt9.Rows
    '                            CurrencyRate = D.Item(0)
    '                        Next
    '                        Dim Query10 As String = String.Empty
    '                        Dim dt10 As New DataTable
    '                        Query10 = "select coa_detail_id from vwCOADetail where detail_title='" & CustomerId & "'"
    '                        dt10 = GetDataTable(Query10)
    '                        dt10.AcceptChanges()
    '                        For Each D As DataRow In dt10.Rows
    '                            SalesCustomerID = D.Item(0)
    '                        Next
    '                        Dim Query11 As String = String.Empty
    '                        Dim dt11 As New DataTable
    '                        Query11 = "select * from tblDefEmployee where Employee_Name ='" & Employee & "'"
    '                        dt11 = GetDataTable(Query11)
    '                        dt11.AcceptChanges()
    '                        For Each D As DataRow In dt11.Rows
    '                            SalespersonID = D.Item(0)
    '                        Next
    '                        Dim Query12 As String = String.Empty
    '                        Dim dt12 As New DataTable
    '                        Query12 = "select ArticleId,ArticleDescription,SalesAccountId from ArticleDefView where ArticleId ='" & ArticleId & "'"
    '                        dt12 = GetDataTable(Query12)
    '                        dt12.AcceptChanges()
    '                        For Each D As DataRow In dt12.Rows
    '                            ArticleId = D.Item(0)
    '                            ArticleDescription = D.Item(1)
    '                            SalesAccountId = D.Item(2)
    '                        Next
    '                        Dim Query13 As String = String.Empty
    '                        Dim dt13 As New DataTable
    '                        If CompanyPrefix = "V-ERP (UAE)" Then
    '                            Query13 = "select coa_detail_id from vwCOADetail where detail_title ='VAT Payable'"
    '                        Else
    '                            Query13 = "select coa_detail_id from vwCOADetail where detail_title ='GST / SST Control Account'"
    '                        End If
    '                        dt13 = GetDataTable(Query13)
    '                        dt13.AcceptChanges()
    '                        For Each D As DataRow In dt13.Rows
    '                            coadetailid = D.Item(0)
    '                        Next
    '                        Dim objCommand As New OleDbCommand
    '                        Dim objCon As OleDbConnection
    '                        objCon = Con
    '                        If objCon.State = ConnectionState.Open Then objCon.Close()
    '                        objCon.Open()
    '                        objCommand.Connection = objCon
    '                        Dim trans As OleDbTransaction = objCon.BeginTransaction
    '                        objCommand.CommandType = CommandType.Text
    '                        objCommand.Transaction = trans
    '                        objCommand.CommandText = "Insert into SalesMasterTable (LocationId,SalesNo,InvoiceType,SalesDate,DueDays,InvoiceParty,CostCenterId,SalesAmount,Post,CustomerCode,EmployeeCode) values('1',N'" & InvoiceNo & "','Credit',N'" & InvoiceDate & "',N'" & PaymentTerms & "',N'" & CustomerId & "',N'" & COSTCenterID & "',N'" & CurrencyRate * Invoiceamount & "','1',N'" & SalesCustomerID & "',N'" & SalespersonID & "')SELECT @@IDENTITY"
    '                        Dim CId As Integer = objCommand.ExecuteScalar
    '                        objCommand.CommandText = "Insert into SalesDetailTable (SalesId,LocationId,ArticleDefId,ArticleSize,Sz1,Sz7,Qty,Price,CurrentPrice,TaxPercent,PurchasePrice,Comments,BaseCurrencyId,BaseCurrencyRate,CurrencyId,CurrencyRate,CurrencyAmount,PostDiscountPrice) VALUES ( " & CId & ",N'" & LocationID & "',N'" & LocationID & "','Loose',1,1,1,N'" & Invoiceamount & "',N'" & Invoiceamount & "',0,0,N'" & InvoiceMonths & "',N'" & BaseCurrencyId & "',1,N'" & CurrencyID & "',N'" & CurrencyRate & "',N'" & CurrencyRate * Invoiceamount & "',N'" & Invoiceamount & "')"
    '                        objCommand.ExecuteNonQuery()
    '                        objCommand.CommandText = "Insert INTO tblVoucher (location_id,finiancial_year_id,voucher_type_id,voucher_no,voucher_date,post,UserName,Posted_UserName,Remarks) values(1,1,7,N'" & InvoiceNo & "',N'" & InvoiceDate & "',1,'V-ERP','V-ERP',N'" & InvoiceMonths & "')SELECT @@IDENTITY"
    '                        Dim VId As Integer = objCommand.ExecuteScalar
    '                        objCommand.CommandText = "INSERT INTO tblVoucherDetail (voucher_id,location_id,coa_detail_id,comments,debit_amount,credit_amount,direction,CostCenterID,ArticleDefId,BaseCurrencyId,BaseCurrencyRate,CurrencyId,CurrencyRate,Currency_debit_amount,Currency_Credit_Amount) VALUES ( " & VId & ",1,N'" & SalesCustomerID & "',N'" & ArticleDescription & ",(" & 1 & " X " & Invoiceamount & ")," & InvoiceMonths & " " & Invoiceyear & "',N'" & CurrencyRate * Invoiceamount & "',0,N'" & ArticleId & "',N'" & COSTCenterID & "',N'" & ArticleId & "',N'" & BaseCurrencyId & "',1,N'" & CurrencyID & "',N'" & CurrencyRate & "',N'" & Invoiceamount & "',0)"
    '                        objCommand.ExecuteNonQuery()
    '                        objCommand.CommandText = "INSERT INTO tblVoucherDetail (voucher_id,location_id,coa_detail_id,comments,debit_amount,credit_amount,direction,CostCenterID,ArticleDefId,BaseCurrencyId,BaseCurrencyRate,CurrencyId,CurrencyRate,Currency_debit_amount,Currency_Credit_Amount) VALUES ( " & VId & ",1,N'" & SalesAccountId & "',N'" & ArticleDescription & ",(" & 1 & " X " & Invoiceamount & ")," & InvoiceMonths & " " & Invoiceyear & "',0,N'" & CurrencyRate * Invoiceamount & "',N'" & ArticleId & "',N'" & COSTCenterID & "',N'" & ArticleId & "',N'" & BaseCurrencyId & "',1,N'" & CurrencyID & "',N'" & CurrencyRate & "',0,N'" & Invoiceamount & "')"
    '                        objCommand.ExecuteNonQuery()
    '                        If Tax > 0 Then
    '                            objCommand.CommandText = "INSERT INTO tblVoucherDetail (voucher_id,location_id,coa_detail_id,comments,debit_amount,credit_amount,direction,CostCenterID,ArticleDefId,BaseCurrencyId,BaseCurrencyRate,CurrencyId,CurrencyRate,Currency_debit_amount,Currency_Credit_Amount) VALUES ( " & VId & ",1,N'" & SalesCustomerID & "',N'Tax Ref : " & InvoiceNo & "',N'" & (CurrencyRate * Invoiceamount * Tax) / 100 & "',0,N'" & ArticleId & "',N'" & COSTCenterID & "',N'" & ArticleId & "',N'" & BaseCurrencyId & "',1,N'" & CurrencyID & "',N'" & CurrencyRate & "',N'" & (Invoiceamount * Tax) / 100 & "',0)"
    '                            objCommand.ExecuteNonQuery()
    '                            objCommand.CommandText = "INSERT INTO tblVoucherDetail (voucher_id,location_id,coa_detail_id,comments,debit_amount,credit_amount,direction,CostCenterID,ArticleDefId,BaseCurrencyId,BaseCurrencyRate,CurrencyId,CurrencyRate,Currency_debit_amount,Currency_Credit_Amount) VALUES ( " & VId & ",1,N'" & coadetailid & "',N'Tax Ref : " & InvoiceNo & "',0,N'" & (CurrencyRate * Invoiceamount * Tax) / 100 & "',N'" & ArticleId & "',N'" & COSTCenterID & "',N'" & ArticleId & "',N'" & BaseCurrencyId & "',1,N'" & CurrencyID & "',N'" & CurrencyRate & "',0,N'" & (Invoiceamount * Tax) / 100 & "')"
    '                            objCommand.ExecuteNonQuery()
    '                        End If
    '                        trans.Commit()
    '                        SendAutoInvoiceEmail()
    '                    Next
    '                End If
    '            End If
    '        Next

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Private Sub GetInvoiceData()
        Dim currentdate As String = String.Empty
        currentdate = DateTime.Now.ToString("yyyy-MM-dd")
        Dim ContractId As String = String.Empty
        'Dim str As String
        'str = "select Email from tblUser where User_ID = " & LoginUserId
        'Dim dt As DataTable = GetDataTable(str)
        'If dt.Rows.Count > 0 Then
        '    senderemail = dt.Rows(0).Item("Email").ToString
        'End If
        Try
            Dim COSTCenterID As Integer = 0
            Dim LocationID As Integer = 0
            Dim CurrencyID As Integer = 0
            Dim CurrencyRate As Double = 0
            Dim SalesCustomerID As Integer = 0
            Dim SalespersonID As Integer = 0
            Dim ArticleDescription As String = String.Empty
            Dim query As String = String.Empty
            Dim InvoiceMonths As String = String.Empty
            Dim AdvanceStartDate As String = String.Empty
            Dim AdvanceEndDate As String = String.Empty
            Dim Invoiceamount As Double = 0
            Dim SalesAccountId As Integer = 0
            Dim coadetailid As Integer = 0
            Dim Invoiceyear As Integer = 0
            Dim BaseCurrencyId As Integer = getConfigValueByType("Currency").ToString
            Dim dt As New DataTable
            query = "SELECT InvoiceDetailId,ContractId,CONVERT(VARCHAR(10),InvoiceDate,23) AS  InvoiceDate,InvoiceAmount, isnull(previousdate,getdate()) AS previousdate FROM ContractInvoiceDetailTable"
            dt = GetDataTable(query)
            dt.AcceptChanges()
            For Each r As DataRow In dt.Rows
                If dt.Rows.Count > 0 Then
                    If r.Item(2) = currentdate Then
                        InvoiceDate = r.Item(2)
                        Dim query1 As String = String.Empty
                        Dim dt1 As New DataTable
                        Dim StartDate1 As String = String.Empty
                        Dim EndDate1 As String = String.Empty
                        StartDate1 = r.Item(2)
                        EndDate1 = r.Item(4)
                        Invoiceamount = r.Item(3)
                        query1 = "SELECT ContractId,ContractNo,StartDate,EndDate,SLAType,PreventionMaintenance,CustomerId,OpportunityId,Status,EndCustomer,PONumber,ContactofNotification,Site,InvoicingFrequency,PaymentTerms,Comments,Currency,Amount,Employee,ISNULL(ContractType,'') AS ContractType,ISNULL(TerminateStatus,'') AS TerminateStatus,ISNULL(ContractStatus,'') AS ContractStatus,PreviousContracts,ISNULL(HoldReason,'') AS HoldReason,ISNULL(OthersDescription,'') AS OthersDescription,ISNULL(HoldCheckBox,0) AS HoldCheckBox,ISNULL(ChkBoxBatteriesIncluded,0) AS ChkBoxBatteriesIncluded,ISNULL(DurationofMonth,'') AS DurationofMonth,ISNULL(InvoicePattern,'') AS InvoicePattern,ISNULL(ArticleId,0) AS ArticleId,ISNULL(Tax,0) AS Tax FROM ContractMasterTable where ContractId = " & r.Item(1) & " "
                        dt1 = GetDataTable(query1)
                        dt1.AcceptChanges()
                        For Each C As DataRow In dt1.Rows
                            Dim ContractNo As String = String.Empty
                            Dim StartDate As String = String.Empty
                            Dim EndDate As String = String.Empty
                            Dim CustomerId As String = String.Empty
                            Dim OpportunityId As String = String.Empty
                            Dim Status As String = String.Empty
                            Dim EndCustomer As String = String.Empty
                            Dim Site As String = String.Empty
                            Dim InvoicingFrequency As String = String.Empty
                            Dim PaymentTerms As String = String.Empty
                            Dim Comments As String = String.Empty
                            Dim Currency As String = String.Empty
                            Dim Amount As String = String.Empty
                            Dim Employee As String = String.Empty
                            Dim ContractType As String = String.Empty
                            Dim TerminateStatus As String = String.Empty
                            Dim ChkBoxBatteriesIncluded As String = String.Empty
                            Dim VoucherNo As String = String.Empty
                            Dim ArticleId As Integer = 0
                            Dim Tax As Double = 0
                            Dim PONumber As String = String.Empty
                            ContractNo = C.Item(1)
                            StartDate = C.Item(2)
                            EndDate = C.Item(3)
                            CustomerId = C.Item(6)
                            OpportunityId = C.Item(7)
                            Status = C.Item(8)
                            EndCustomer = C.Item(9)
                            PONumber = C.Item(10)
                            Site = C.Item(12)
                            InvoicingFrequency = C.Item(13)
                            Dim strWords As String() = C.Item(14).Split(" ")
                            PaymentTerms = strWords(0)
                            Comments = C.Item(15)
                            Currency = C.Item(16)
                            Amount = C.Item(17)
                            Employee = C.Item(18)
                            ArticleId = C.Item(29)
                            Tax = Val(C.Item(30))
                            InvoiceNo = GetDocumentNo(InvoiceDate)
                            Dim Query2 As String = String.Empty
                            Dim dt2 As New DataTable
                            Query2 = "SELECT * FROM tblDefCostCenter where Name = '" & C.Item(1) & "'"
                            dt2 = GetDataTable(Query2)
                            dt2.AcceptChanges()
                            For Each D As DataRow In dt2.Rows
                                COSTCenterID = D.Item(0)
                            Next
                            Dim Query3 As String = String.Empty
                            Dim dt3 As New DataTable
                            Query3 = "SELECT * FROM tblDefLocation where location_code Like '%VIR-%'"
                            dt3 = GetDataTable(Query3)
                            dt3.AcceptChanges()
                            For Each D As DataRow In dt3.Rows
                                LocationID = D.Item(0)
                            Next
                            Dim Query4 As String = String.Empty
                            Dim dt4 As New DataTable
                            Query4 = "GetInvoiceMonth'" & EndDate1 & "','" & StartDate1 & "' "
                            dt4 = GetDataTable(Query4)
                            dt4.AcceptChanges()
                            For Each D As DataRow In dt4.Rows
                                Invoiceyear = D.Item(0)
                                InvoiceMonths = D.Item(1)
                            Next
                            Dim Query5 As String = String.Empty
                            Dim dt5 As New DataTable
                            Query5 = "Select * from ContractInvoiceDetailTable where ContractId = " & r.Item(1) & " AND InvoiceDate >= CAST(GETDATE() AS DATE) "
                            dt5 = GetDataTable(Query5)
                            dt5.AcceptChanges()
                            If dt5.Rows.Count > 1 Then
                                AdvanceStartDate = dt5.Rows(0).Item(2)
                                AdvanceEndDate = dt5.Rows(1).Item(2)
                                Dim Query6 As String = String.Empty
                                Dim dt6 As New DataTable
                                Query6 = "GetInvoiceMonth'" & AdvanceStartDate & "','" & AdvanceEndDate & "' "
                                dt6 = GetDataTable(Query6)
                                dt6.AcceptChanges()
                                For Each E As DataRow In dt6.Rows
                                    Invoiceyear = E.Item(0)
                                    InvoiceMonths = E.Item(1)
                                Next
                            ElseIf dt5.Rows.Count = 1 Then
                                AdvanceStartDate = dt5.Rows(0).Item(2)
                                AdvanceStartDate = EndDate
                                Dim Query7 As String = String.Empty
                                Dim dt7 As New DataTable
                                Query7 = "GetInvoiceMonth'" & AdvanceStartDate & "','" & AdvanceStartDate & "' "
                                dt7 = GetDataTable(Query7)
                                dt7.AcceptChanges()
                                For Each E As DataRow In dt7.Rows
                                    Invoiceyear = E.Item(0)
                                    InvoiceMonths = E.Item(1)
                                Next
                            End If
                            Dim Query8 As String = String.Empty
                            Dim dt8 As New DataTable
                            Query8 = "SELECT currency_id FROM tblcurrency where currency_code='" & Currency & "'"
                            dt8 = GetDataTable(Query8)
                            dt8.AcceptChanges()
                            For Each D As DataRow In dt8.Rows
                                CurrencyID = D.Item(0)
                            Next
                            Dim Query9 As String = String.Empty
                            Dim dt9 As New DataTable
                            Query9 = "SELECT CurrencyRate FROM tblCurrencyRate where CurrencyId=" & CurrencyID & ""
                            dt9 = GetDataTable(Query9)
                            dt9.AcceptChanges()
                            For Each D As DataRow In dt9.Rows
                                CurrencyRate = D.Item(0)
                            Next
                            Dim Query10 As String = String.Empty
                            Dim dt10 As New DataTable
                            Query10 = "select coa_detail_id from vwCOADetail where detail_title='" & CustomerId & "'"
                            dt10 = GetDataTable(Query10)
                            dt10.AcceptChanges()
                            For Each D As DataRow In dt10.Rows
                                SalesCustomerID = D.Item(0)
                            Next
                            Dim Query11 As String = String.Empty
                            Dim dt11 As New DataTable
                            Query11 = "select * from tblDefEmployee where Employee_Name ='" & Employee & "'"
                            dt11 = GetDataTable(Query11)
                            dt11.AcceptChanges()
                            For Each D As DataRow In dt11.Rows
                                SalespersonID = D.Item(0)
                            Next
                            Dim Query12 As String = String.Empty
                            Dim dt12 As New DataTable
                            Query12 = "select ArticleId,ArticleDescription,SalesAccountId from ArticleDefView where ArticleId ='" & ArticleId & "'"
                            dt12 = GetDataTable(Query12)
                            dt12.AcceptChanges()
                            For Each D As DataRow In dt12.Rows
                                ArticleId = D.Item(0)
                                ArticleDescription = D.Item(1)
                                SalesAccountId = D.Item(2)
                            Next
                            Dim Query13 As String = String.Empty
                            Dim dt13 As New DataTable
                            If CompanyPrefix = "V-ERP (UAE)" Then
                                Query13 = "select coa_detail_id from vwCOADetail where detail_title ='VAT Payable'"
                            Else
                                Query13 = "select coa_detail_id from vwCOADetail where detail_title ='GST / SST Control Account'"
                            End If
                            dt13 = GetDataTable(Query13)
                            dt13.AcceptChanges()
                            For Each D As DataRow In dt13.Rows
                                coadetailid = D.Item(0)
                            Next
                            Dim objCommand As New OleDbCommand
                            Dim objCon As OleDbConnection
                            objCon = Con
                            If objCon.State = ConnectionState.Open Then objCon.Close()
                            objCon.Open()
                            objCommand.Connection = objCon
                            Dim trans As OleDbTransaction = objCon.BeginTransaction
                            objCommand.CommandType = CommandType.Text
                            objCommand.Transaction = trans
                            objCommand.CommandText = "Insert into SalesMasterTable (LocationId,SalesNo,InvoiceType,SalesDate,DueDays,InvoiceParty,CostCenterId,SalesAmount,Post,CustomerCode,EmployeeCode,POId,PO_NO) values('1',N'" & InvoiceNo & "','Credit',N'" & InvoiceDate & "',N'" & IIf(PaymentTerms = "Pre-Payment", "0", PaymentTerms) & "',N'" & CustomerId & "',N'" & COSTCenterID & "',N'" & CurrencyRate * Invoiceamount & "','1',N'" & SalesCustomerID & "',N'" & SalespersonID & "',0,N'" & PONumber & "')SELECT @@IDENTITY"
                            Dim CId As Integer = objCommand.ExecuteScalar
                            objCommand.CommandText = "Insert into SalesDetailTable (SalesId,LocationId,ArticleDefId,ArticleSize,Sz1,Sz7,Qty,Price,CurrentPrice,TaxPercent,PurchasePrice,Comments,BaseCurrencyId,BaseCurrencyRate,CurrencyId,CurrencyRate,CurrencyAmount,PostDiscountPrice, BatchID, SampleQty) VALUES ( " & CId & ",N'" & LocationID & "',N'" & ArticleId & "','Loose',1,1,1,N'" & Invoiceamount & "',N'" & Invoiceamount & "',N'" & Tax & "',0,N'" & InvoiceMonths & "',N'" & BaseCurrencyId & "',1,N'" & CurrencyID & "',N'" & CurrencyRate & "',N'" & CurrencyRate * Invoiceamount & "',N'" & Invoiceamount & "',0,0)"
                            objCommand.ExecuteNonQuery()
                            objCommand.CommandText = "Insert INTO tblVoucher (location_id,finiancial_year_id,voucher_type_id,voucher_no,voucher_date,post,UserName,Posted_UserName,Remarks, Source) values(1,1,7,N'" & InvoiceNo & "',N'" & InvoiceDate & "',1,'V-ERP','V-ERP',N'" & InvoiceMonths & "','frmSales')SELECT @@IDENTITY"
                            Dim VId As Integer = objCommand.ExecuteScalar
                            objCommand.CommandText = "INSERT INTO tblVoucherDetail (voucher_id,location_id,coa_detail_id,comments,debit_amount,credit_amount,direction,CostCenterID,ArticleDefId,BaseCurrencyId,BaseCurrencyRate,CurrencyId,CurrencyRate,Currency_debit_amount,Currency_Credit_Amount) VALUES ( " & VId & ",1,N'" & SalesCustomerID & "',N'" & ArticleDescription & ",(" & 1 & " X " & Invoiceamount & ")," & InvoiceMonths & " " & Invoiceyear & "',N'" & CurrencyRate * Invoiceamount & "',0,N'" & ArticleId & "',N'" & COSTCenterID & "',N'" & ArticleId & "',N'" & BaseCurrencyId & "',1,N'" & CurrencyID & "',N'" & CurrencyRate & "',N'" & Invoiceamount & "',0)"
                            objCommand.ExecuteNonQuery()
                            objCommand.CommandText = "INSERT INTO tblVoucherDetail (voucher_id,location_id,coa_detail_id,comments,debit_amount,credit_amount,direction,CostCenterID,ArticleDefId,BaseCurrencyId,BaseCurrencyRate,CurrencyId,CurrencyRate,Currency_debit_amount,Currency_Credit_Amount) VALUES ( " & VId & ",1,N'" & SalesAccountId & "',N'" & ArticleDescription & ",(" & 1 & " X " & Invoiceamount & ")," & InvoiceMonths & " " & Invoiceyear & "',0,N'" & CurrencyRate * Invoiceamount & "',N'" & ArticleId & "',N'" & COSTCenterID & "',N'" & ArticleId & "',N'" & BaseCurrencyId & "',1,N'" & CurrencyID & "',N'" & CurrencyRate & "',0,N'" & Invoiceamount & "')"
                            objCommand.ExecuteNonQuery()
                            If Tax > 0 Then
                                objCommand.CommandText = "INSERT INTO tblVoucherDetail (voucher_id,location_id,coa_detail_id,comments,debit_amount,credit_amount,direction,CostCenterID,ArticleDefId,BaseCurrencyId,BaseCurrencyRate,CurrencyId,CurrencyRate,Currency_debit_amount,Currency_Credit_Amount) VALUES ( " & VId & ",1,N'" & SalesCustomerID & "',N'Tax Ref : " & InvoiceNo & "',N'" & (CurrencyRate * Invoiceamount * Tax) / 100 & "',0,N'" & ArticleId & "',N'" & COSTCenterID & "',N'" & ArticleId & "',N'" & BaseCurrencyId & "',1,N'" & CurrencyID & "',N'" & CurrencyRate & "',N'" & (Invoiceamount * Tax) / 100 & "',0)"
                                objCommand.ExecuteNonQuery()
                                objCommand.CommandText = "INSERT INTO tblVoucherDetail (voucher_id,location_id,coa_detail_id,comments,debit_amount,credit_amount,direction,CostCenterID,ArticleDefId,BaseCurrencyId,BaseCurrencyRate,CurrencyId,CurrencyRate,Currency_debit_amount,Currency_Credit_Amount) VALUES ( " & VId & ",1,N'" & coadetailid & "',N'Tax Ref : " & InvoiceNo & "',0,N'" & (CurrencyRate * Invoiceamount * Tax) / 100 & "',N'" & ArticleId & "',N'" & COSTCenterID & "',N'" & ArticleId & "',N'" & BaseCurrencyId & "',1,N'" & CurrencyID & "',N'" & CurrencyRate & "',0,N'" & (Invoiceamount * Tax) / 100 & "')"
                                objCommand.ExecuteNonQuery()
                            End If
                            trans.Commit()
                            SendAutoInvoiceEmail()
                        Next
                    End If
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SendAutoInvoiceEmail(Optional ByVal Activity As String = "")
        Try
            GetTemplate("Receipt")
            If Con.Database.Contains("Remms") Then
                UsersEmail = "r.ejaz@remmsit.com"
            Else
                UsersEmail = "r.ejaz@agriusit.com"
            End If
            FormatStringBuilder(dtEmail)
            CreateInvoiceOutLookMail(UsersEmail)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateInvoiceOutLookMail(ByVal Email As String)
        Try
            Dim senderemail As String
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            Dim OutAccount As Outlook.Account
            Dim str As String
            str = "select Email from tblUser where User_ID = " & LoginUserId
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                senderemail = dt.Rows(0).Item("Email").ToString
                OutAccount = oApp.Session.Accounts(senderemail)
                mailItem.SendUsingAccount = OutAccount
            End If
            mailItem.Subject = "Notification of invoice " & companycountry & "( " & InvoiceNo & " ) Generation"
            mailItem.To = Email
            'If Con.Database.Contains("Remms") Then
            '    mailItem.CC = "Adil@remmsit.com"
            'Else
            '    mailItem.CC = "Adil@agriusit.com"
            '    mailItem.CC = "w.raza@agriusit.com"
            'End If
            'Email = String.Empty
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            Dim myStr As String
            myStr = "Hi Accounts team," & "<br>" & "<br>" & "New invoice genetred ( " & InvoiceNo & " ).Please review this invoice." & "<br>" & "<br>" & "<b>Best Regards:</b>" & "<br>" & "V-ERP"
            mailItem.HTMLBody = myStr
            mailItem.Send()
            Application.DoEvents()
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Function GetDocumentNo(ByVal InvoiceDate As Date) As String
    '    Dim DocNo As String = String.Empty
    '    Try
    '        If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
    '            If CompanyBasePrefix = True Then
    '                DocNo = GetSerialNo("" & "" & "-", "SalesMasterTable", "SalesNo")
    '            Else
    '                If CompanyPrefix = "V-ERP (UAE)" Then
    '                    DocNo = GetSerialNo("SI" & "-" + Microsoft.VisualBasic.Right(Me.InvoiceDate.Year, 2) + "-", "SalesMasterTable", "SalesNo")
    '                Else
    '                    companyinitials = "PK"
    '                    DocNo = GetNextDocNo("SI" & "-" & companyinitials & "-" & Format(Me.InvoiceDate, "yy"), 4, "SalesMasterTable", "SalesNo")
    '                End If
    '            End If
    '        ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
    '            If CompanyBasePrefix = True Then
    '                DocNo = GetSerialNo("" & "" & "" & "-", "SalesMasterTable", "SalesNo")
    '            Else
    '                If CompanyPrefix = "V-ERP (UAE)" Then
    '                    DocNo = GetNextDocNo("SI" & "" & "-" & Format(Me.InvoiceDate, "yy") & Me.InvoiceDate.Month.ToString("00"), 2, "SalesMasterTable", "SalesNo")
    '                Else
    '                    companyinitials = "PK"
    '                    DocNo = GetNextDocNo("SI" & "-" & companyinitials & "-" & Format(Me.InvoiceDate, "yy"), 4, "SalesMasterTable", "SalesNo")
    '                End If
    '            End If
    '        Else
    '            If CompanyBasePrefix = True Then
    '                DocNo = GetSerialNo("SI" & "" & "" & "-", "SalesMasterTable", "SalesNo")
    '            Else
    '                DocNo = GetNextDocNo("SI", 6, "SalesMasterTable", "SalesNo")
    '            End If
    '        End If
    '        While SalesInvoiceSkipDAL.IsSkipped(DocNo)
    '            Dim LastIndexOfDash As Integer = DocNo.LastIndexOf("-")
    '            Dim DocumentNo As String = DocNo.Substring(LastIndexOfDash + 1)
    '            Dim DocumentNoPrefix As String = DocNo.Substring(0, LastIndexOfDash + 1)
    '            Dim LeadingZeros As String = GetLeadingZerosLength(DocumentNo)
    '            Dim IncrementDocumentNo As String = DocumentNo + 1
    '            If LeadingZeros.Length > 0 Then
    '                DocumentNo = LeadingZeros & IncrementDocumentNo
    '            End If
    '            DocNo = DocumentNoPrefix & DocumentNo
    '        End While
    '        Return DocNo
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Private Function GetLeadingZerosLength(ByVal str As String) As String
        Try
            Dim Zeros As String = String.Empty
            For Each c As Char In str
                If c = "0"c Then
                    Zeros += "0"
                    'Else
                    '    Exit For
                End If
            Next
            Return Zeros
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function GetLiveCurrencyRate()
        Try

            'Dim apiURL As String = "https://xecdapi.xe.com/v1/convert_from.json/?from=" & "USD&to=PKR&amount=1"
            'Dim request As HttpWebRequest = DirectCast(WebRequest.Create(apiURL), HttpWebRequest)
            'request.Headers("Authorization") = "personal523205146 673e764o9bqbkqfcl4poic9u71"



            'Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
            'Dim responseStream As Stream = response.GetResponseStream()
            'Dim streamReader As StreamReader = New StreamReader(responseStream)



            'Dim jsonSerializer As New JavaScriptSerializer()
            'Dim jsonResponse As String = streamReader.ReadToEnd()
            'Dim data As Object = jsonSerializer.Deserialize(jsonResponse, GetType(Object))



            'Dim result As Double = Convert.ToDouble(data("to")[0]("mid"))
            '    txtResult.Text = (result * Convert.ToDouble(txtAmount.Text)).ToString()





            Dim url As String = "https://openexchangerates.org/api/latest.json?app_id=2fa459194b42416aaf3069b61cd9e40b"
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
            request.Method = "GET"

            Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)

            If response.StatusCode = HttpStatusCode.OK Then
                Dim responseStream As Stream = response.GetResponseStream()
                Dim streamReader As StreamReader = New StreamReader(responseStream)

                Dim responseContent As String = streamReader.ReadToEnd()

                ' Parse the JSON response
                Dim jsonResponse As JObject = JObject.Parse(responseContent)

                ' Get the exchange rates
                Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
                If objCon.State = ConnectionState.Closed Then objCon.Open()
                Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
                Dim cmd As New OleDb.OleDbCommand
                cmd.Connection = objCon
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 30
                cmd.Transaction = trans
                Dim str As String
                Dim dt As DataTable
                Dim BaseCurrencyId As Integer = getConfigValueByType("Currency").ToString
                Dim PKRRate As Double = jsonResponse.SelectToken("rates.PKR").ToObject(Of Double)()
                Dim AEDRate As Double = jsonResponse.SelectToken("rates.AED").ToObject(Of Double)()
                Dim EURRate As Double = jsonResponse.SelectToken("rates.EUR").ToObject(Of Double)()
                Dim MYRRate As Double = jsonResponse.SelectToken("rates.MYR").ToObject(Of Double)()
                If BaseCurrencyId = 1 Then
                    AEDRate = PKRRate / AEDRate
                    EURRate = PKRRate / EURRate
                    MYRRate = PKRRate / MYRRate
                    cmd.CommandText = "Update tblCurrencyRate set CurrencyRate = " & PKRRate & ", UpdateDate = Convert(datetime,  '" & Date.Now.ToString("yyyy-M-dd hh:mm:ss tt") & "', 102), UserId = " & LoginUser.LoginUserId & " WHERE CurrencyId = 2"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "Update tblCurrencyRate set CurrencyRate = " & AEDRate & ", UpdateDate = Convert(datetime,  '" & Date.Now.ToString("yyyy-M-dd hh:mm:ss tt") & "', 102), UserId = " & LoginUser.LoginUserId & " WHERE CurrencyId = 9"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "Update tblCurrencyRate set CurrencyRate = " & MYRRate & ", UpdateDate = Convert(datetime,  '" & Date.Now.ToString("yyyy-M-dd hh:mm:ss tt") & "', 102), UserId = " & LoginUser.LoginUserId & " WHERE CurrencyId = 10"
                    cmd.ExecuteNonQuery()

                ElseIf BaseCurrencyId = 9 Then
                    EURRate = AEDRate / EURRate
                    MYRRate = AEDRate / MYRRate
                    cmd.CommandText = "Update tblCurrencyRate set CurrencyRate = 3.6725, UpdateDate = Convert(datetime,  '" & Date.Now.ToString("yyyy-M-dd hh:mm:ss tt") & "', 102), UserId = " & LoginUser.LoginUserId & " WHERE CurrencyId = 2"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "Update tblCurrencyRate set CurrencyRate = " & MYRRate & ", UpdateDate = Convert(datetime,  '" & Date.Now.ToString("yyyy-M-dd hh:mm:ss tt") & "', 102), UserId = " & LoginUser.LoginUserId & " WHERE CurrencyId = 10"
                    cmd.ExecuteNonQuery()

                ElseIf BaseCurrencyId = 10 Then
                    EURRate = MYRRate / EURRate
                    AEDRate = MYRRate / AEDRate
                    cmd.CommandText = "Update tblCurrencyRate set CurrencyRate = " & MYRRate & ", UpdateDate = Convert(datetime,  '" & Date.Now.ToString("yyyy-M-dd hh:mm:ss tt") & "', 102), UserId = " & LoginUser.LoginUserId & " WHERE CurrencyId = 2"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "Update tblCurrencyRate set CurrencyRate = " & AEDRate & ", UpdateDate = Convert(datetime,  '" & Date.Now.ToString("yyyy-M-dd hh:mm:ss tt") & "', 102), UserId = " & LoginUser.LoginUserId & " WHERE CurrencyId = 9"
                    cmd.ExecuteNonQuery()
                End If

                trans.Commit()
                Application.DoEvents()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub Timer13_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer13.Tick
        Try
            If DateTime.Now.Hour = 8 Then
                Me.Timer13.Enabled = False
                If BackgroundWorker25.IsBusy Then Exit Sub
                BackgroundWorker25.RunWorkerAsync()
                Do While BackgroundWorker25.IsBusy
                    Application.DoEvents()
                Loop
                Me.Timer13.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage("Not Getting Currency Rate")
        End Try
        ''end task 2640
    End Sub

    Private Sub BackgroundWorker25_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker25.DoWork
        'Try
        'Try
        '    GetLiveCurrencyRate()
        'Catch ex As Exception
        '    MessageBox.Show("An error occurred: " & ex.Message)
        'End Try
    End Sub
    Function GetDocumentNo(ByVal InvoiceDate As Date) As String
        Dim DocNo As String = String.Empty
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                If CompanyBasePrefix = True Then
                    DocNo = GetSerialNo("" & "" & "-", "SalesMasterTable", "SalesNo")
                Else
                    If CompanyPrefix = "V-ERP (UAE)" Then
                        DocNo = GetSerialNo("SI1" & "-" + Microsoft.VisualBasic.Right(Me.InvoiceDate.Year, 2) + "-", "SalesMasterTable", "SalesNo")
                    Else
                        'companyinitials = "PK"
                        DocNo = GetNextDocNo("SI" & "-" & companyinitials & "-" & Format(Me.InvoiceDate, "yy"), 4, "SalesMasterTable", "SalesNo")
                    End If
                End If
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                If CompanyBasePrefix = True Then
                    DocNo = GetSerialNo("" & "" & "" & "-", "SalesMasterTable", "SalesNo")
                Else
                    If CompanyPrefix = "V-ERP (UAE)" Then
                        DocNo = GetNextDocNo("SI1" & "" & "-" & Format(Me.InvoiceDate, "yy") & Me.InvoiceDate.Month.ToString("00"), 2, "SalesMasterTable", "SalesNo")
                    Else
                        'companyinitials = "PK"
                        DocNo = GetNextDocNo("SI" & "-" & companyinitials & "-" & Format(Me.InvoiceDate, "yy"), 4, "SalesMasterTable", "SalesNo")
                    End If
                End If
            Else
                If CompanyBasePrefix = True Then
                    DocNo = GetSerialNo("SI1" & "" & "" & "-", "SalesMasterTable", "SalesNo")
                Else
                    DocNo = GetNextDocNo("SI1", 6, "SalesMasterTable", "SalesNo")
                End If
            End If
            While SalesInvoiceSkipDAL.IsSkipped(DocNo)
                Dim LastIndexOfDash As Integer = DocNo.LastIndexOf("-")
                Dim DocumentNo As String = DocNo.Substring(LastIndexOfDash + 1)
                Dim DocumentNoPrefix As String = DocNo.Substring(0, LastIndexOfDash + 1)
                Dim LeadingZeros As String = GetLeadingZerosLength(DocumentNo)
                Dim IncrementDocumentNo As String = DocumentNo + 1
                If LeadingZeros.Length > 0 Then
                    DocumentNo = LeadingZeros & IncrementDocumentNo
                End If
                DocNo = DocumentNoPrefix & DocumentNo
            End While
            Return DocNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
