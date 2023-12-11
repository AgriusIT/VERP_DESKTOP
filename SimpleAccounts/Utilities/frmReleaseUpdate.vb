Imports System.IO
Imports System.Diagnostics
Imports System.Text
Imports Microsoft.Win32
Imports System.Data.OleDb
Imports System.Net
Imports System.ComponentModel
Imports System.Reflection  'Added by Syed Irfan Ahmad on 28 Feb 2018, Task No: 2620
Imports System.Security.Cryptography  'Added by Syed Irfan Ahmad on 28 Feb 2018, Task No: 2620

Public Class frmReleaseUpdate

    '========================= Added by Syed Irfan Ahmad on 28 Feb 2018, Task No: 2620  ================== Start
    Private TripleDes As New TripleDESCryptoServiceProvider

    'The value of "ENCRYPTION_KEY_FOR_RELEASE_SCRIPT_FILES" is VERY CRITICAL. Embedded files in 
    'SchemaUpdaterV4 assembly are emcoded with this key. If this key is changed, those embedded
    'files can't be decrypted back to text files
    Private Const ENCRYPTION_KEY_FOR_RELEASE_SCRIPT_FILES As String = "B9Jk3XzpQ4"

    'If at some point in time, it may be required that, in case of an error in a script file, further 
    'scripts should be run, then value of the following constant should be set to TRUE
    Private Const DONT_EXECUTE_FURTHER_IF_ANY_SCRIPT_CUASES_ERROR As Boolean = True

    'The following array is a two-dimensional array. First dimension represents the release versions
    'and second dimension represents the PREFIX of script file for that release.
    ' !!!!! IMPORTANT !!!
    'No two release versions should have same prefix.
    'Even if a release hove NO SCRIPTS, its prefix should be mentioed in the following array as others are defined
    Private ReadOnly Relv4(,) As String =
        {
            {"4.0.0.0", "RSCR4000"},
            {"4.0.0.1", "RSCR4001"},
            {"4.0.0.2", "RSCR4002"},
            {"4.0.0.3", "RSCR4003"},
            {"4.0.0.4", "RSCR4004"},
            {"4.0.0.5", "RSCR4005"},
            {"4.0.0.6", "RSCR4006"},
            {"4.0.0.7", "RSCR4007"},
            {"4.0.0.8", "RSCR4008"},
            {"4.0.0.9", "RSCR4009"},
            {"4.0.1.0", "RSCR4010"},
            {"4.0.1.1", "RSCR4011"},
            {"4.0.1.2", "RSCR4012"},
            {"4.0.1.3", "RSCR4013"},
            {"4.0.1.4", "RSCR4014"},
            {"4.0.1.7", "RSCR4017"},
            {"4.0.1.8", "RSCR4018"},
            {"4.0.1.9", "RSCR4019"},
            {"4.0.2.2", "RSCR4022"},
            {"4.0.2.3", "RSCR4023"},
            {"4.0.2.5", "RSCR4025"},
            {"4.0.2.6", "RSCR4026"},
            {"4.0.2.7", "RSCR4027"},
            {"4.0.2.8", "RSCR4028"},
            {"4.0.2.9", "RSCR4029"},
            {"4.0.3.0", "RSCR4030"},
            {"4.0.3.1", "RSCR4031"},
            {"4.0.3.2", "RSCR4032"},
            {"4.0.3.4", "RSCR4034"},
            {"4.0.3.5", "RSCR4035"},
            {"4.0.3.7", "RSCR4037"},
            {"4.0.3.8", "RSCR4038"},
            {"4.0.3.9", "RSCR4039"},
            {"4.0.4.0", "RSCR4040"},
            {"4.0.4.1", "RSCR4041"},
            {"4.0.4.3", "RSCR4043"},
            {"4.0.4.4", "RSCR4044"},
            {"4.0.4.5", "RSCR4045"},
            {"4.0.4.6", "RSCR4046"},
            {"4.0.4.7", "RSCR4047"},
            {"4.0.4.8", "RSCR4048"},
            {"4.0.4.9", "RSCR4049"},
            {"4.0.5.0", "RSCR4050"},
            {"4.0.5.1", "RSCR4051"},
            {"4.0.5.2", "RSCR4052"},
            {"4.0.5.3", "RSCR4053"},
            {"4.0.5.4", "RSCR4054"},
            {"4.0.5.5", "RSCR4055"},
            {"4.0.5.6", "RSCR4056"},
            {"4.0.5.7", "RSCR4057"},
            {"4.0.5.8", "RSCR4058"},
            {"4.0.5.9", "RSCR4059"},
            {"4.0.6.0", "RSCR4060"},
            {"4.0.6.1", "RSCR4061"},
            {"4.0.6.2", "RSCR4062"},
            {"4.0.6.3", "RSCR4063"},
            {"4.0.6.4", "RSCR4064"},
            {"4.0.6.5", "RSCR4065"},
            {"4.0.6.6", "RSCR4066"},
            {"4.0.6.7", "RSCR4067"},
            {"4.0.6.8", "RSCR4068"},
            {"4.0.6.9", "RSCR4069"},
            {"4.0.7.0", "RSCR4070"},
            {"4.0.7.1", "RSCR4071"},
            {"4.0.7.2", "RSCR4072"},
            {"4.0.7.3", "RSCR4073"},
            {"4.0.7.4", "RSCR4074"},
            {"4.0.7.5", "RSCR4075"},
            {"4.0.7.6", "RSCR4076"},
            {"4.0.7.7", "RSCR4077"},
            {"4.0.7.8", "RSCR4078"},
            {"4.0.7.9", "RSCR4079"},
            {"4.0.8.0", "RSCR4080"},
            {"4.0.8.1", "RSCR4081"},
            {"4.0.8.2", "RSCR4082"},
            {"4.0.8.3", "RSCR4083"},
            {"4.0.8.4", "RSCR4084"},
            {"4.0.8.5", "RSCR4085"},
            {"4.0.8.6", "RSCR4086"},
            {"4.0.8.7", "RSCR4087"},
            {"4.0.8.8", "RSCR4088"},
            {"4.0.8.9", "RSCR4089"},
            {"4.0.9.0", "RSCR4090"},
            {"4.0.9.1", "RSCR4091"},
            {"4.0.9.2", "RSCR4092"},
            {"5.0.0.0", "RSCR5000"},
            {"5.0.0.1", "RSCR5001"},
            {"5.0.0.2", "RSCR5002"},
            {"5.0.0.3", "RSCR5003"},
            {"5.0.0.4", "RSCR5004"},
            {"5.0.0.5", "RSCR5005"},
            {"5.0.0.6", "RSCR5006"},
            {"5.0.0.7", "RSCR5007"},
            {"5.0.0.8", "RSCR5008"},
            {"5.0.0.9", "RSCR5009"},
            {"5.0.1.0", "RSCR5010"},
            {"5.0.1.1", "RSCR5011"},
            {"5.0.1.2", "RSCR5012"},
            {"5.0.1.3", "RSCR5013"},
            {"5.0.1.4", "RSCR5014"},
            {"5.0.1.5", "RSCR5015"},
            {"5.0.1.6", "RSCR5016"},
            {"5.0.1.7", "RSCR5017"},
            {"5.0.1.8", "RSCR5018"},
            {"5.0.1.9", "RSCR5019"},
            {"5.0.2.0", "RSCR5020"},
            {"5.0.2.1", "RSCR5021"},
            {"5.0.2.2", "RSCR5022"},
            {"5.0.2.3", "RSCR5023"},
            {"5.0.2.4", "RSCR5024"},
            {"5.0.2.5", "RSCR5025"},
            {"5.0.2.6", "RSCR5026"},
            {"5.0.2.7", "RSCR5027"},
            {"5.0.2.8", "RSCR5028"},
            {"5.0.2.9", "RSCR5029"},
            {"5.0.3.0", "RSCR5030"},
            {"5.0.3.1", "RSCR5031"},
            {"5.0.3.2", "RSCR5032"},
            {"5.0.3.3", "RSCR5033"},
            {"5.0.3.4", "RSCR5034"},
            {"5.0.3.5", "RSCR5035"},
            {"5.0.3.6", "RSCR5036"},
            {"5.0.3.7", "RSCR5037"},
            {"5.0.3.8", "RSCR5038"},
            {"5.0.3.9", "RSCR5039"},
            {"5.0.4.0", "RSCR5040"},
            {"5.0.4.1", "RSCR5041"},
            {"5.0.4.2", "RSCR5042"},
            {"5.0.4.3", "RSCR5043"},
            {"5.0.4.4", "RSCR5044"},
            {"5.0.4.5", "RSCR5045"},
            {"5.0.4.6", "RSCR5046"},
            {"5.0.4.7", "RSCR5047"},
            {"5.0.4.8", "RSCR5048"},
            {"5.0.4.9", "RSCR5049"},
            {"5.0.5.0", "RSCR5050"},
            {"5.0.5.1", "RSCR5051"},
            {"5.0.5.2", "RSCR5052"},
            {"5.0.5.3", "RSCR5053"},
            {"5.0.5.4", "RSCR5054"},
            {"5.0.5.5", "RSCR5055"},
            {"5.0.5.6", "RSCR5056"},
            {"5.0.5.7", "RSCR5057"},
            {"5.0.5.8", "RSCR5058"},
            {"5.0.5.9", "RSCR5059"},
            {"5.0.6.0", "RSCR5060"},
            {"5.0.6.1", "RSCR5061"},
            {"5.0.6.2", "RSCR5062"},
            {"5.0.6.3", "RSCR5063"},
            {"5.0.6.4", "RSCR5064"},
            {"5.0.6.5", "RSCR5065"},
            {"5.0.6.6", "RSCR5066"},
            {"5.0.6.7", "RSCR5067"},
            {"5.0.6.8", "RSCR5068"},
            {"5.0.6.9", "RSCR5069"}
        }

    '===================================================================================================== End


    Dim LastVersion As Integer
    Dim i As Integer = 0
    Dim DBVer As Integer = 0
    Dim ProgBarVal As Integer = 0
    Dim ProgBarOverAllVal As Integer = 0
    Dim ProgBarOrverAllMaxVal As Integer = 0
    Private Sub frmReleaseUpdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.txtDBVersion.Text = GetConfigValue("Version").ToString()
            Me.txtApplicationVersion.Text = Application.ProductVersion
            DBVer = Val(Me.txtApplicationVersion.Text.Replace(".", "")) - Val(Me.txtDBVersion.Text.Replace(".", ""))
            Me.btnHome.Visible = False
            Me.ProgressBar1.Value = 0
            Me.ProgressBar2.Value = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub CreateCurrentDBBackup()
        Try
            Dim objConStringBuilder As New OleDb.OleDbConnectionStringBuilder(Con.ConnectionString)
            Dim strFileName As String = String.Empty
            Dim strFilePath As String = String.Empty
            Me.BackgroundWorker1.ReportProgress(10)
            strFilePath = Application.StartupPath & "\DB_Backup"
            If Not IO.Directory.Exists(strFilePath) Then
                IO.Directory.CreateDirectory(strFilePath)
            End If
            Me.BackgroundWorker1.ReportProgress(20)
            Dim strSQL As String = String.Empty
            strFileName = objConStringBuilder.Item("Initial Catalog").ToString & "_" & Date.Now.ToString("yyyyMMddhhmmss") & ".bak"
            strSQL = "BACKUP DATABASE " & objConStringBuilder.Item("Initial Catalog").ToString & " TO DISK='" & strFilePath & "\" & strFileName.ToString & "'"
            If Con.State = ConnectionState.Closed Then Con.Open()
            Me.BackgroundWorker1.ReportProgress(40)
            Dim cmd As New OleDbCommand(strSQL, Con)
            cmd.CommandTimeout = 120
            cmd.ExecuteNonQuery()
            Me.BackgroundWorker1.ReportProgress(30)
            'msg_Information("Create backup successfully." & Chr(10) & strFilePath & "\" & strFileName)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.lblprogress.Text = String.Empty
            Me.txtDBVersion.Text = GetConfigValue("Version").ToString()
            If Val(Me.txtDBVersion.Text.ToString.Replace(".", "")) < 1018 Then
                MsgBox("The system database is too old. Please contact SIRIUS Support to update upto version 1.0.1.8", vbCritical)  'Added by Syed Irfan, 1 Mar 2018, Task No: 2620
                Exit Sub
            End If
            ''TFS4788 : Release should be updated whether DB version > Application version or DB version < Application version
            If Val(Me.txtDBVersion.Text.ToString.Replace(".", "")) <> Val(Me.txtApplicationVersion.Text.ToString.Replace(".", "")) Then
                If Me.BackgroundWorker1.IsBusy Then Exit Sub
                Me.BackgroundWorker1.RunWorkerAsync()
                Do While Me.BackgroundWorker1.IsBusy
                    Application.DoEvents()
                Loop

                If UpdateVersion(Val(Me.txtDBVersion.Text.ToString.Replace(".", ""))) Then
                    getConfigValueList()
                    If frmModProperty.BackgroundWorker4.IsBusy Then Exit Sub
                    frmModProperty.BackgroundWorker4.RunWorkerAsync()
                    Do While frmModProperty.BackgroundWorker4.IsBusy
                        Application.DoEvents()
                    Loop
                    If frmModProperty.BackgroundWorker6.IsBusy Then Exit Sub
                    frmModProperty.BackgroundWorker6.RunWorkerAsync()
                    Do While frmModProperty.BackgroundWorker6.IsBusy
                        Application.DoEvents()
                    Loop
                    UserDecrypt()
                    msg_Information("Update process completed successfully.")
                    Me.btnHome.Visible = True
                    frmModProperty.dbVersion = GetConfigValue("Version").ToString()
                    'frmMain.UltraStatusBar2.Panels(1).Text = "DB Ver: " & GetConfigValue("Version").ToString()
                End If
                FillDropDown(Me.cmbReleaseLog, "Select DISTINCT Release_Version, Release_Version From Release_Log ORDER BY 1 DESC", False)
                Me.cmbReleaseLog.Text = Me.txtDBVersion.Text
                FillDropDown(Me.cmbVersionto, "Select DISTINCT Release_Version, Release_Version From Release_Log ORDER BY 1 DESC", False)
                'Me.gbRelease.Visible = True
                Dim strSQL As String = "Select 0 as Serial_No, Release_Module, Release_Version, Release_Detail, Status From Release_Log WHERE Release_Version BETWEEN '" & Me.cmbReleaseLog.SelectedValue & "' AND '" & Me.cmbVersionto.SelectedValue & "' Group By Release_Module, Release_Version, Release_Detail, Status ORDER BY Release_Version DESC"
                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                Dim int As Int32 = 1I
                If dt IsNot Nothing Then
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        r("Serial_No") = int
                        r.EndEdit()
                        int += 1
                    Next
                End If
                Me.grdSaved.DataSource = Nothing
                Me.grdSaved.DataSource = dt
                Me.grdSaved.AutoSizeColumns()
            Else
                msg_Information("Schema is already updated")
                If Me.txtDBVersion.Text = Me.txtApplicationVersion.Text Then
                    FillDropDown(Me.cmbReleaseLog, "Select DISTINCT Rel ease_Version, Release_Version From Release_Log ORDER BY 1 ASC", False)
                    FillDropDown(Me.cmbVersionto, "Select DISTINCT Release_Version, Release_Version From Release_Log ORDER BY 1 DESC", False)
                    'Me.gbRelease.Visible = True
                    Dim strSQL As String = "Select 0 as Serial_No, Release_Module, Release_Version, Release_Detail, Status From Release_Log WHERE Release_Version BETWEEN '" & Me.cmbReleaseLog.SelectedValue & "' AND '" & Me.cmbVersionto.SelectedValue & "' Group By Release_Module,Release_Version, Release_Detail, Status ORDER BY Release_Version DESC"
                    Dim dt As New DataTable
                    dt = GetDataTable(strSQL)
                    Dim int As Int32 = 1I
                    If dt IsNot Nothing Then
                        For Each r As DataRow In dt.Rows
                            r.BeginEdit()
                            r("Serial_No") = int
                            r.EndEdit()
                            int += 1
                        Next
                    End If
                    Me.grdSaved.DataSource = Nothing
                    Me.grdSaved.DataSource = dt
                    Me.grdSaved.AutoSizeColumns()
                End If
            End If
        Catch ex As Exception
            'msg_Error("An error has occured while updating schema " & Chr(10) & Chr(10) & ex.Message)
            MsgBox("An error has occured while updating schema " & vbCrLf & vbCrLf & ex.Message, vbCritical)
        Finally
            Me.Cursor = Cursors.Default
        End Try

        'Me.txtDBVersion.Text = GetConfigValue("Version").ToString()
        If GetConfigValue("NewSecurityRights").ToString = "True" Then
            If CheckRights() = False Then
                ApplyStyleSheet(frmUserGroup)
                frmUserGroup.ShowDialog()
            End If
        End If
        Me.ProgressBar2.Value = ProgressBar2.Maximum
        Me.ProgressBar2.Update()

    End Sub

    Function UpdateVersion(ByVal CurrentVersion As Integer) As Boolean
        Try

            System.Threading.Thread.Sleep(100)

            ProgressBar2.Minimum = 1I
            ProgressBar2.Value = 1
            Me.ProgressBar2.Maximum = DBVer
            ProgressBar2.Step = DBVer
            ProgBarOrverAllMaxVal = 0

            If CurrentVersion < 1019 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Maximum = ProgBarVal
                Me.ProgressBar1.Value = 0
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.1.9\" & "1_ConfigValuesTable_VoucherNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.1.9\" & "2_Update_DB_Version.sql")
                CurrentVersion = 1019
                ProgBar()
            End If

            If CurrentVersion < 1020 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Maximum = ProgBarVal
                Me.ProgressBar1.Value = 0
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.0\" & "1_Sps.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.0\" & "2_sp_BalanceSheet.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.0\" & "3_spBalanceSheetNotesDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.0\" & "4_spCashAccountPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.0\" & "5_spCashAccountingExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.0\" & "6_spCashAccountingSale.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.0\" & "7_spDailySaleReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.0\" & "8_spProfitLossNotesDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.0\" & "9_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 1021 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Maximum = ProgBarVal
                Me.ProgressBar1.Value = 0
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.1\" & "0_Drop_Procedures.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.1\" & "7_tblvoucherDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.1\" & "1_sp_rpt_Ledger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.1\" & "2_sp_pl_singleDate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.1\" & "3_sp_pl_Comparison.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.1\" & "4_vw_expense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.1\" & "5_vw_cash_flow.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.1\" & "6_tblDefCostCenter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.1\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 1022 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 1
                Me.ProgressBar1.Maximum = ProgBarVal
                Me.ProgressBar1.Value = 0
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.2\" & "1_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 1023 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Maximum = ProgBarVal
                Me.ProgressBar1.Value = 0
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.3\" & "1_AddCol_SalesMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.3\" & "2-configvalue.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.3\" & "3-sp_Profit & Loss notes detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.3\" & "4_AddColum_tblCustomer.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.3\" & "5_tblDefLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.3\" & "7-configvaluefornewinvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.3\" & "8_spsaleinvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.3\" & "9_spSaleInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.3\" & "6_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 1024 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Maximum = ProgBarVal
                Me.ProgressBar1.Value = 0
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.4\" & "0_Drop_SP.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.4\" & "1_AdjustmentnSalesTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.4\" & "2_SampleQtyInSaleDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.4\" & "3_tblcustomertypes.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.4\" & "4_spPLComparison.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.4\" & "5_spPLSigleDate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.4\" & "6_tblCustomer.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.4\" & "7_rptItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.4\" & "8_spBalanceSheet.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.4\" & "9_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 1025 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Maximum = ProgBarVal
                Me.ProgressBar1.Value = 0
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.5\" & "1_sp_trial.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.5\" & "2_sp_trial.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.5\" & "3_tblcostsheet.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.5\" & "4_tbldefcustomerbasediscounts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.5\" & "5_sp_DailySales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.5\" & "6_sp_DailySales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.5\" & "7_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 1026 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 11
                Me.ProgressBar1.Maximum = ProgBarVal
                Me.ProgressBar1.Value = 0
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.6\" & "1ControlRight.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.6\" & "2EnhancedSecurityKey.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.6\" & "3FormControl.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.6\" & "4GroupIDIntblSecurityUser.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.6\" & "5GroupsForm_OldSecurityScript.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.6\" & "6SecurityForm.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.6\" & "7tblSecurityGroup.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.6\" & "8tblsecurityUser.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.6\" & "91SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.6\" & "9SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.6\" & "10Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 1027 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 38
                Me.ProgressBar1.Maximum = ProgBarVal
                Me.ProgressBar1.Value = 0
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "1_Drop_Proc_SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "2_Create_Proc_Sp_Balance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "3_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "4_1_Drop_ProductList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "4_Sp_ProductList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "5_UpdateEmployeeSalePerson.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "5_1_Update_Employees.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "6_1_2_SP_StoreIssuanceReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "6_1_3_SP_StoreIssuanceReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "6_SP_StoreIssuanceReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "6_1_SP_StoreIssuanceReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "7_Drop_Proc_SP_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "8_1_SP_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "8_SP_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "9_Drop_Proc_SP_Customer_Item_Sales_Summary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "10_SP_ItemWiseSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "11_Drop_Proc_SP_Customer_Item_Sales_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "12_SP_ItemWiseDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "13_Drop_Proc_sp_Store_Issuence.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "14_SP_StoreIssuanceDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "15_Drop_Proc_sp_Store_Issuence_Summary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "16_SP_StoreIssuanceSummary.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "17_AlterColumnCompanyId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "18_Drop_TopCustomers.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "19_SP_TopCustomer.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "20_Update_CostCenterGroup.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "21_NEWCOADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "22_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "23_InvoiceBasedPaymentDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "24_InvliceBaseReceipts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "25_InvoiceBasedReceiptsDetails.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "26_1_Drop_SP_StockStatmentByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "27_2_Drop_SP_StockStatmentByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "28_SP_AttendanceSummaryDrop.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "29_SP_AttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "31_AddColumn_ArticleColorCode.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "32_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "34_EmpAttendanceStatus.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.7\" & "30_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 1028 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 15
                Me.ProgressBar1.Maximum = ProgBarVal
                Me.ProgressBar1.Value = 0
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "1_Drop_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "2_Create_BS.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "3_SecurityGroup.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "4_UserSecurity.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "5_Message_Head.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "6_Message_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "7_CreateTableTaskStatus.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "8_CreateTableTask.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "9_CreateTableTaskType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "10_AddColumn_ArticleColorCode.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "11_ArticleDefViewAlter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "12_DROP_SP_ProductList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "13_SP_ProductList_AddSalesItem.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "14_UPDATEARTICELGROUPDEFTABLE.SQL")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.8\" & "30_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 1029 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Maximum = ProgBarVal
                Me.ProgressBar1.Value = 0
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.9\" & "1_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.9\" & "2_OpeningBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.9\" & "3_AddColumn_CostCenterId_In_SalesMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.9\" & "4_AddColumn_CostCenterId_In_CompanyTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.9\" & "5_Drop_Sp_Stockstatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.9\" & "6_SP_Stock_Statement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.9\" & "7_Drop_Table_Invoice_Based_Receipt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.9\" & "8_Drop_Table_Invoice_Based_ReceiptDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.9\" & "9_Invoice_Based_Receipt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\1.0.2.9\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2000 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 34
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "1_VW_GL_Voucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "2_Update_ControlForm.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "3_DROP_SP_Lrg_Aging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "4_SP_Lgr_Aging_Balance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "5_AddColumnDcNoOnPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "6_AddColumnCostCenterIDonInvoiceBaseReceipt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "7_AddColumnCostCenterIDonInvoiceBasePayment.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "8_InsertInvoiceBaseVoucherForm.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "9_AddColumnPostOnSalesForm.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "10_AddColumnPriceAllowedSecurityInUser.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "11_AddColumnPostingUserSecurityInUser.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "12_AddColumnPostOnPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "13_AddColumnPostOnPurchaseReturn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "14_AddColumnPostOnSalesReturnForm.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "15_AddColumnReceivedQtyOnPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "16_AddColumnRejectedQtyOnPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "17_AddTableArticleDefVendorsItem.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "18_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "19_AddColumnCostCenterIdOnInvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "20_AddColumnCostCenterIdOnInvoiceBasedReceipt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "21_DROP_SP_Lrg_Aging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "22_SP_ProductCostSheet.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "23_Updatepost.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "24_taxupdatevalue.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "25_AddColumnDcNoOnPO.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "update_userright.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "AddColumnGSTOnInvoiceBasedReceiptDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "AddColumnGSTOnInvoiceBasedPaymentDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "AddColumnDcDateOnPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "AddColumntvehicleNOnPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "AddColumndriverNameOnPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "DROP_SP_purchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "sp_purchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "DROP_SP_StockByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "SP_StockBylocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.0\" & "26_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2001 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 13
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.1\" & "1_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.1\" & "2_DROP_SP_PODetailHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.1\" & "3_SP_PODetailHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.1\" & "4_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.1\" & "5_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.1\" & "6_AddColumnDcDateOnPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.1\" & "7_AddColumndriverNameOnPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.1\" & "8_AddColumntvehicleNOnPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.1\" & "9_DROP_SP_Lgr_Aging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.1\" & "10_SP_Lgr_Aging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.1\" & "12_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.1\" & "13_AddColumntvehicleNOnPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.1\" & "11_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2002 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 17
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "1_Drop_SP_SalesOrderHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "2_SP_SalesOrderHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "3_Drop_SP_PurchaseInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "4_SP_PurchaseInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "5_Drop_SP_AgingBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "6_SP_AgingBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "7_Drop_SP_ArticleHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "8_SP_ArticleHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "9_Drop_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "10_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "12_Drop_SP_StockDispatch.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "13_SP_StockDispatch.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "14_AlterColumnTaxInvoiceBasedPaymentDt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "15_AlterColumnTaxInvoiceBasedReceiptDt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "16_tblFormInvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "17_tblFormInvoiceBasedReceipt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.2\" & "11_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2003 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.3\" & "1_UpdateFuelExpAccount.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.3\" & "2_UpdateAdjustmentExpAccount.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.3\" & "3_UpdateOtherExpAccount.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.3\" & "4_Drop_SP_AgingBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.3\" & "5_SP_AgingBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.0.3\" & "6_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2010 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.0\" & "1_Drop_SP_AgingBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.0\" & "2_SP_AgingBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.0\" & "3_Drop_SP_ItemWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.0\" & "4_SP_ItemWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.0\" & "5_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2011 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.1\" & "1_Drop_SP_ProfitAndLossNoteDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.1\" & "2_SP_ProfitAndLossNoteDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.1\" & "3_ViewCOADetailActive.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.1\" & "10_ViewArticleDef.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.1\" & "4_UpdateActiveDetailAccount.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.1\" & "5_Drop_SP_StockReceing.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.1\" & "6_SP_StoreProcedure.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.1\" & "8_Drop_SP_StockDispatch.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.1\" & "9_SP_StockDispatch.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.1.1\" & "7_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2020 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 12
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.0\" & "1_AttendanceTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.0\" & "2_Drop_SP_Employees_Attendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.0\" & "3_SP_Employees_Attendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.0\" & "4_Drop_SP_Employees_Attendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.0\" & "5_SP_EmployeesAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.0\" & "6_Drop_SP_DailySupplyAndGatePass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.0\" & "7_SP_DailySupplyAndGatePass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.0\" & "8_DropSecurityUser.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.0\" & "9_UserSecurity.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.0\" & "11_Drop_SP_SalesDemand.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.0\" & "12_SP_SalesDemand.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.0\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2021 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 46
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "1_Drop_TableTask.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "2_TaskTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "3_Drop_SP_Task.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "4_SP_TaskManagement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "5_Drop_SP_TaskEmpAndDateRange.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "6_SP_TaskByEmpAndDateRange.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "7_Drop_SP_SalesReturn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "8_SP_SalesReturnWeight.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "9_ArticleDefWeight.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "10_Drop_SP_RptWeight.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "11_SP_RptWeight.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "12_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "13_Drop_SP_StockReceiving.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "14_SP_StockReceiving.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "15_AlterColumnVendorInvoiceNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "16_AddFormProductionStore.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "17_AddFormReturnablegatepass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "18_Drop_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "19_SP_StockStatementSize.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "20_AddTableGatePasMaster.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "21_AddTableGatepassDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "22_DispatchScript.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "23_1_DropView_Employee_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "23_View_EmployeeDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "24_1_DropView_Customer_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "24_View_Customer.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "25_1_DropView_Vendor_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "25_View_Vendor.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "26_addtabletaskdetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "27_Drop_SP_ArticleHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "28_SP_ArticleHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "29_Drop_SP_HistoryByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "30_SP_HistoryByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "31_AddTableProductionMaster.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "32_AddTableProductionDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "33_Drop_SP_BalanceSheetSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "34_SP_BalanceSheetSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "35_Drop_SP_ProductionStore.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "36_SP_ProductionStore.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "37_Drop_SP_Rpt_ReturnableGatepass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "38_SP_Rpt_ReturnableGatepass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "AlterColumnInvoiceBasedPaymentVendorInvoiceNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "AddTableStockMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "InsertStockDocumentType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.1\" & "39_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2022 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 29
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "1_DropTablesStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "2_AddTableStockMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "3.1_Drop_SP_StockUpgrading.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "3_StockUpgrade.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "3.2_StockUpgrading.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "4_UpdateChequeDate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "5_Drop_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "6_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "7_AddColumnEmpAccountId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "8_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "9_AddTableEmployeeSalary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "10_Drop_SP_StockByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "11_SP_StockByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "12_Drop_SP_ReturnableGatepass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "13_SP_Returnablegatepass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "14_Drop_SP_Receiveables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "15_SP_Receiveables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "16_Drop_SP_Payables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "17_SP_Payables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "18_AlterColumnSP_Refrence.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "19_VwGLVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "20_Drop_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "21_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "22_AddColumnTaxPurchaseReturn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "23_AddColumnTaxSaleReturn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "24_Drop_SP_StockSummaryByDate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "25_SP_StockSummaryByDate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "27_InsertEmployeeSalaryVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.2\" & "26_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2023 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.3\" & "1_DropSPExpenses.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.3\" & "2_SP_Expense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.3\" & "3_InsertEmployeeSalaryVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.3\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2024 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 36
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "1_AddColumnActiveInCostCenter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "1.1_AddColumnActiveInCostCenter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "2_AddTableCostingDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "3_DailySalariesDetailtbl.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "4_DailySalariesmastertbl.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "5_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "6_Update_Config_SalariesAccount.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "7_DropSPEmployee_Salary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "8_SP_Employee_Salary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "9_DropSPProductionDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "10_SP_Production_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "11_DS_Voucher_Type.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "12_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "13_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "14_DropSP_DailySalarySummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "15_SP_DailySalarySummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "16_DropSP_DailySalaryVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "17_SP_Daily_Salary_Voucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "18_DropSP_ProductionItems.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "19_sp_productionItmes.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "20_DropSP_ArticleHistorykByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "21_SP_ArticleHistory_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "22_DropSP_StockByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "23_SP_Stock_By_Location.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "24_DropSP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "25_SP_Rpt_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "26_DropSP_ArticleHistoryBySize.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "27_SP_ArticleHistory_BySize.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "28_DropSP_ProductList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "29_SP_ProductList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "30_DropSP_Advances.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "31_SP_Advances.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "32_AddFormSalariesVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "33_DropSP_DailySalaries.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "34_SP_DailySalaries.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.4\" & "35_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2025 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 36
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "1_DropSP_DailySalary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "2_SP_Daily_Salaries.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "3_DropSP_DailySalarySummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "4_SP_DailySalarySheetSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "5_Drop_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "6_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "7_Drop_SP_StockByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "8_SP_StockByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "9_Drop_SP_ArticleByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "10_SP_ArticleHistoryByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "11_Drop_SP_ArticleBySize.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "12_SP_ArticleHistoryBySize.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "13_Drop_SP_Dispatch.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "14_sp_dispatch.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "15_Drop_SP_Payables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "16_sp_rpt_payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "17_Drop_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "18_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "19_Drop_SP_Receivables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "20_sp_rpt_receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "21_Drop_SP_StoreIssuenceDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "22_SP_StoreIssuenceDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "23_Drop_SP_StoreIssuenceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "24_SP_StoreIssuenceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "25_Drop_SP_StoreIssuenceHistoryByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "26_sp_storeissuancehistorybyproduction.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "27_Drop_SP_Drop_Production_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "28_SP_Production_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "29_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "30_Drop_SP_Drop_AgingBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "31_SP_AgingBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "32_SP_Drop_ProductionItems.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "33_SP_ProductionItems.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "34_SP_Drop_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "35_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.5\" & "36_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2026 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 17
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "1_Drop_SP_DailySalarySummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "2_SP_DailySalarySheetSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "3_Drop_SP_DailySalary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "4_SP_DailySalarySheet.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "5_Drop_SP_Production_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "6_SP_Production_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "7_Drop_SP_Production_History.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "8_SP_ProductionHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "9_AddColumnIGPNoOnProductionStore.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "10_Drop_SP_StockUpgrading.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "11_SP_StockUpgrading.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "12_Exe_SP_StockUpgrading.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "13_Drop_SP_ProductionStoreIGP.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "14_SP_ProductionStoreIGP.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "15_Drop_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "16_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.6\" & "17_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2027 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 15
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "1_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "2_Drop_SP_Stock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "3_SP_STock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "4_Drop_SP_ProduceItems.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "5_SP_ProducedItems.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "6_Drop_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "7_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "8_Drop_SP_StockStatementBySize.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "9_SP_StockStatementBySize.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "10_Drop_SP_StockByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "11_SP_Stock_By_Location.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "12_Drop_SP_Ledger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "13_SP_Ledger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "14_AddColumnCostCenterOnPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.7\" & "15_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2028 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 28
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "1_AddColumnNTNNOCustomer.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "2_AddColumnSalesTaxNoOnCustomer.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "3_AddColumnNTNNoOnVendor.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "4_AddColumnSalesTaxNoOnVendor.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "5_AddColumnArticleSizeCode.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "6_Drop_SP_ProductList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "7_SP_ProductList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "8_Drop_View_ArticleDef.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "9_View_ArticleDef.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "10_AddColumnRestrictedItemsOnLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "11_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "12_AddRestrictedItemsTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "13_Voucher_Type.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "14_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "15_AddTableDailySupplyAndGatePass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "16_Drop_SP_UpdateDailySupply.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "17_SP_UpdateDailySupply.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "18_Drop_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "19_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "20_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "21_AddColumnSizeCode.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "22_Drop_SP_ItemWeight.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "23_SP_ItemWeight.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "24_AddColumnIssueQty.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "25_AddColumnReference.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "26_Drop_SP_ReturnableGatepassInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "27_SP_ReturnableGatepassinvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.8\" & "28_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2029 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.9\" & "1_Drop_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.9\" & "2_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.9\" & "3_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.9\" & "4_AddColumnSizeCode.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.9\" & "5_AddColumnRefDocumentONReceiving.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.9\" & "6_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.9\" & "7_AddColumnPostOnProduction.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.9\" & "8_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.2.9\" & "9_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2030 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 22
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "1_Update_CostCenterActive.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "2_Drop_View_Expenses.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "3_View_Expense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "4_AddTableInwardGatePass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "5_InsertFormInwardGatePass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "6_Drop_SP_InwardGatePass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "7_SP_InwardGatepass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "8_Drop_SP_PL.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "9_SP_PL.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "10_Drop_SP_Aging_Balance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "11_SP_Aging_Balance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "12_Drop_SP_Trial.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "13_SP_Trial.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "14_Upadate_GL_Notes.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "15_AddColumnComments.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "16_AddColumnIssuedStore.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "17_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "18_AddColumnRefDocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "19_AlterColumnDispatchQty.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "20_AlterColumnDispatchAmount.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "21_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.0\" & "22_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2031 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 87
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "0_SecurityTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "1_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "2_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "3_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "4_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "5_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "6_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "7_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "8_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "9_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "10_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "11_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "12_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "13_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "14_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "15_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "16_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "17_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "19_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "20_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "21_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "22_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "23_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "24_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "25_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "26_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "27_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "28_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "29_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "30_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "31_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "32_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "33_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "34_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "35_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "36_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "37_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "38_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "39_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "40_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "41_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "42_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "43_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "44_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "45_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "46_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "47_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "48_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "49_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "50_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "51_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "52_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "53_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "54_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "55_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "56_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "57_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "58_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "59_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "60_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "61_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "61_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "62_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "63_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "64_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "65_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "66_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "67_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "68_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "70_AddColumnReference.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "71_AddColumnPessiNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "72_AddColumnEobiNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "73_AddColumnEmpPicture.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "74_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "75_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "76_AddFinancialYearCloseStatusTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "77_Drop_SP_DailySupply.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "78_SP_DailySupplyAndGatepass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "79_Drop_SP_StockBySize.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "80_SP_StockBySize.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "81_Drop_SP_StockByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "82_SP_StockByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "83_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "84_Drop_SP_FinancialYear.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "85_SP_Financial_Year_Closing.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "86_Update_FirstEndOfDate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.1\" & "87_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2032 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 77
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "0_Drop_SP_GetRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "1_SP_GetRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "2_Drop_SP_Rpt_Ledger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "3_SP_Rpt_Ledger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "4_Drop_SP_Trial_Balance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "5_SP_Trial_Balance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "6_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "7_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "8_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "9_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "10_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "11_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "12_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "13_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "14_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "15_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "16_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "17_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "18_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "19_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "20_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "21_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "22_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "23_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "24_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "25_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "26_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "27_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "28_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "29_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "30_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "31_AddColumnExpiryDate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "32_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "33_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "34_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "35_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "36_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "37_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "38_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "39_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "40_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "41_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "42_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "43_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "44_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "45_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "46_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "46_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "47_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "48_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "49_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "50_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "51_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "52_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "53_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "54_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "55_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "56_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "57_Drop_SP_DailyUpdate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "58_Sp_DailyUpdate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "59_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "60_AddColumnBlock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "61_AddColumnGroupId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "62_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "63_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "64_AddColumnSEDPercent.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "65_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "66_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "67_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "68_Drop_SP_SalesInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "69_SP_SalesInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "70_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "71_Drop_SP_UpdateDailyInwardGatepass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "72_SP_UpdateDailyInwardGatepass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "73_AddColumnBiltyNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "74_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.2\" & "75_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2033 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 22
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "1_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "2_Drop_SP_UpdateDailyInwardGatepass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "3_SP_UpdateDailyInwardGatepass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "4_AddColumnBiltyNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "5_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "6_Drop_SP_SalesReturnSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "7_SP_SalesReturnSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "8_Drop_SP_ProductionSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "9_SP_ProductionSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "10_Drop_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "11_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "12_Drop_SP_Lgr_Aging_Balance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "13_SP_Lgr_Aging_Balance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "14_AddColumnAccessKey.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "15_AlterColumnAccessKey.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "15_Drop_SP_DailyUpdate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "16_SP_DailyUpdate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "17_AddTableEmail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "18_Drop_SP_EmailAccount.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "19_SP_EmailAccount.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "20_UpdateAccessKey.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.3\" & "21_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2034 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 43
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "1_AddColumnGroupType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "2_InsertAdminGroup.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "3_UpdateUserGroupId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "4_Drop_SP_SalesOrderQuotation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "5_SP_SalesOrderQuotation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "6_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "7_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "8_Drop_SP_StockInOutDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "9_SP_StockInOutDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "10_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "11_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "12_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "13_AddTableMailSentBox.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "14_AddColumnEmailAlert.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "15_AddColumnServiceItemArticleMaster.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "16_AddColumnServiceItemArticleDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "17_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "18_AddColumnLogComments.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "19_AddColumnServiceQty.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "20_AddColumnServiceItemSale.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "21_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "22_Drop_SP_GetRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "23_SP_GetRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "24_AddTablesShift.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "25_AddColumnShiftGroupId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "26_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "27_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "28_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "29_AddColumnAttendanceShiftId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "30_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "31_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "32_Drop_SP_EmployeeShiftDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "33_SP_EmployeeShiftDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "34_Drop_SP_EmployeeAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "35_SP_EmployeeAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "36_AddColumnRefProductionNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "37_AddColumnRefDispatchNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "38_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "39_Update_Shift.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "40_AddColumnProjectOnStockMaster.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "41_Drop_SP_StockInOutDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "42_SP_StockInOutDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.4\" & "43_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2035 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.5\" & "1_AddColumnUnitOnInwardgatepass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.5\" & "2_SP_Drop_Inwardgatepass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.5\" & "3_SP_Inwardgatepass.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.5\" & "4_SP_Drop_Stock_In_Out_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.5\" & "5_SP_Stock_In_Out_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.5\" & "6_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2036 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.6\" & "1_AddColumnArticlePicture.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.6\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2037 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.7\" & "1_SP_Drop_ItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.7\" & "2_SP_ItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.7\" & "3_AddColumnBankDesc.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.7\" & "4_AddColumnDashBoardRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.7\" & "5_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2038 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 14
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.8\" & "1_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.8\" & "2_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.8\" & "3_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.8\" & "4_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.8\" & "5_AddColumnIGPNoOnPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.8\" & "6_ALTERColumnDispatch.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.8\" & "7_SP_Drop_Stock_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.8\" & "8_SP_Stock_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.8\" & "9_SP_Drop_ProductionStore.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.8\" & "10_SP_ProductionStore.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.8\" & "11_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.8\" & "12_SP_Drop_SP_StockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.8\" & "13_SP_StockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.8\" & "14_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2039 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 26
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "1_SP_Drop_SP_StockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "2_SP_StockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "3_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "4_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "5_AddColumnExPlantPrice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "6_AddColumnSalesTaxPercentage.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "7_AddColumnSchemeQty.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "8_AddColumnDiscount_Percentage.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "9_AddColumnFreight.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "10_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "11_SP_Drop_SP_SalesOrderAnalysis.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "12_SP_SalesOrderAnalysis.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "13_SP_Drop_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "14_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "15_AddColumnsOnSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "16_SP_Drop_SP_StockInOutDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "17_SP_StockInOutDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "18_SP_Drop_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "19_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "20_AddColumnsOnArticleTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "21_SP_Drop_SP_LastPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "22_SP_LastPayments.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "23_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "24_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "25_AddColumnItemWeight.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.3.9\" & "26_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2040 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 22
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "1_AddColumnItemWeight.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "2_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "2.1_sp_renameColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "3_AddColumnMarketReturnsOnSalesDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "4_1_AddColumnPosted.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "4_SP_Drop_SP_ProductionPlaning.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "5_SP_ProductionPlaning.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "6_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "7_MailBoxTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "8_Drop_SP_Stock_By_Location.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "9_SP_Stock_By_Location.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "10_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "11_Drop_View_ArticleDef.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "12_Views_ArticleDef.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "13_Drop_SP_SalesOrderAnalysis.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "14_SP_SalesOrderAnalysis.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "15_Update_EmailAlert.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "16_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "17_AddColumnTransitInsurance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "18_Drop_SP_SalesReturnItemWeight.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "19_SP_SalesReturnItemWeight.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.0\" & "20_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2041 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.1\" & "1_AddColumnDeliveredSchemQty.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.1\" & "2_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.1\" & "3_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.1\" & "4_AddColumnCustomerCodeOnCustomer.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.1\" & "5_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.1\" & "6_Update_Config_Value_LID2.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.1\" & "7_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2042 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 14
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.2\" & "1_Add_Salaries_Type_Table.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.2\" & "2_Drop_SP_ItemHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.2\" & "3_SP_ItemHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.2\" & "4_Drop_SP_Stock_InOut_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.2\" & "5_Stock_InOut_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.2\" & "6_Drop_SP_Aging_Payables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.2\" & "7_SP_Aging_Payables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.2\" & "8_Drop_SP_Aging_Receiveables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.2\" & "9_SP_Aging_Receiveables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.2\" & "10_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.2\" & "11_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.2\" & "12_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.2\" & "13_AddTable_Registration_Local.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.2\" & "14_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2043 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 25
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "1_AddTable_Leads.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "2_AddTable_Print_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "3_AddTable_Tips.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "4_Update_CostCenterGroup.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "5_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "6_AddTableCustomerBasedTarget.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "7_Drop_SP_CustomerBasedTarget.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "8_SP_CustomerBasedTarget.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "9_Drop_SP_CustomersSalesHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "10_SP_CustomersSalesHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "11_AddTableDefVehicle.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "12_AddTableVehicleLog.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "13_Drop_SP_ProjectWiseStockLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "14_SP_ProjectWiseStockLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "15_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "16_AddColumnPO_StoreIssuance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "17_Drop_SP_ItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "18_SP_ItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "19_Drop_SP_ItemWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "20_SP_ItemWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "21_Drop_SP_CustomerWiseSummarySalesChart.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "22_CustomerWiseSummarySalesChart.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "23_Drop_SP_VehicleLog.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "24_SP_VehicleLog.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.3\" & "25_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2044 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.4\" & "1_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2045 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 18
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "1_AddTableAnalysisType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "2_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "3_AddColumnReceiveableAccountIdOnEmployee.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "4_AddTableWizardConfigTab.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "5_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "6_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "7_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "8_Drop_SP_ProductionsItems.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "9_SP_ProductionItems.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "10_ADDSalesAnalysisType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "11_Drop_View_GL_Voucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "12_View_GLVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "13_AddTableMarketReturnsDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "14_Drop_SP_SalesGrowthByCustomers.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "15_SP_SalesGrowthByCustomers.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "16_AddColumnLeadNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "17_AddColumnEntryNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.5\" & "18_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2046 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.6\" & "1_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.6\" & "2_Drop_SP_Ledger_By_Invoices.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.6\" & "3_SP_Rpt_Ledger_By_Invoices.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.6\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2047 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 11
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.7\" & "1_AddColumnAdjustment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.7\" & "2_AddColumnSalesReturns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.7\" & "3_Drop_SP_CustomerGrowthSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.7\" & "4_SP_CustomerGrowthSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.7\" & "5_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.7\" & "6_Drop_SP_ProjectWiseLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.7\" & "7_SP_ProjectWiseLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.7\" & "8_Drop_SP_ProjectWiseLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.7\" & "9_SP_LocationWiseLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.7\" & "10_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.7\" & "11_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2048 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.8\" & "1_Drop_SP_LocationWiseLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.8\" & "2_SP_Stock_Ledger_By_Location.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.8\" & "3_Drop_SP_ProjectWiseLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.8\" & "4_SP_Stock_Ledger_By_Project.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.8\" & "5_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2049 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 11
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.9\" & "1_Drop_SP_ItemsWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.9\" & "2_SP_ItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.9\" & "3_Drop_SP_ItemsWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.9\" & "4_SP_ItemSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.9\" & "5_Drop_SP_GrowthSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.9\" & "6_SP_GrowthSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.9\" & "7_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.9\" & "8_AddColumnPrefix.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.9\" & "9_Drop_SP_ProjectWiseLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.9\" & "10_SP_Stock_Ledger_By_Project.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.4.9\" & "11_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2050 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 21
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "1_Drop_SP_InvoiceWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "2_SP_InvoiceWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "3_Drop_SP_SalesItem.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "4_SP_SalesItem.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "5_AddColumnEmployee_IdOnVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "6_AddTableMobileExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "7_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "8_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "9_AddColumnSeriveItemOnArticleGroup.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "10_Drop_SP_ItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "11_SP_ItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "12_Drop_SP_ItemWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "13_SP_ItemWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "14_AddTableArticleDefCustomers.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "15_Drop_SP_PostDatedChequeSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "16_SP_PostDatedChequeSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "17_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "18_Assest_Management_Tabls.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "19_Secutiry_Assets_Management.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "20_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.0\" & "21_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2051 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 11
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.1\" & "1_Drop_SP_ItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.1\" & "2_SP_ItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.1\" & "3_Drop_SP_ItemWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.1\" & "4_SP_ItemWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.1\" & "5_AddColumnAssetMangement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.1\" & "6_Drop_SP_AssetsDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.1\" & "7_SP_AssetsDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.1\" & "8_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.1\" & "9_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.1\" & "10_Update_Security.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.1\" & "11_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2052 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.2\" & "1_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.2\" & "2_SP_Update_Asset_Management.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.2\" & "3_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.2\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2053 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 17
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "1_Drop_SP_ItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "2_SP_ItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "3_Drop_SP_ItemWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "4_SP_ItemWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "5_SP_Add_New_Notes.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "6_Drop_SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "7_SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "8_Drop_SP_BalanceSheetSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "9_SP_BalanceSheetSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "10_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "11_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "12_Drop_SP_DemandSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "13_SP_DemandSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "14_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "15_Drop_SP_Demand.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "16_SP_Demand.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.3\" & "17_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2054 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 12
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.4\" & "1_ADD_Table_Delivery_Chalan.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.4\" & "2_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.4\" & "3_AddColumn_SO_Id.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.4\" & "4_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.4\" & "5_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.4\" & "6_Drop_SP_UndertakingLetter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.4\" & "7_SP_UndertakingLetter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.4\" & "8_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.4\" & "9_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.4\" & "10_Drop_SP_DeliveryChalan.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.4\" & "11_SP_Delivery_Chalan.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.4\" & "12_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2055 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.5\" & "1_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2056 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.6\" & "1_InwardExpenseDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.6\" & "2_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.6\" & "3_AddColumnLCId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.6\" & "4_AddTableLetterOfCredit.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.6\" & "5_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2057 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.7\" & "1_AddColumnHSCode.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.7\" & "2_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.7\" & "3_AddTableCurrency.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.7\" & "4_Update_Currency.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.7\" & "5_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.7\" & "6_Drop_SP_ImportLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.7\" & "7_SP_ImportLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.7\" & "8_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.7\" & "9_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.7\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2058 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 11
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.8\" & "1_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.8\" & "2_Drop_SP_CustomerSalesHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.8\" & "3_SP_CustomerSalesHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.8\" & "4_AddTableReminder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.8\" & "5_Drop_SP_Demand_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.8\" & "6_SP_Demand_Detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.8\" & "7_AddColumnPONo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.8\" & "8_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.8\" & "9_AddTable_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.8\" & "10_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.8\" & "11_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2059 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.9\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.5.9\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2060 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.0\" & "1_AddColumnDamage_Budget.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.0\" & "2_Drop_SP_Damage_Budget.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.0\" & "3_SP_Damage_Budget.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.0\" & "4_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.0\" & "5_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.0\" & "6_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.0\" & "7_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2061 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.1\" & "1_Drop_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.1\" & "2_SP_Rpt_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.1\" & "3_Drop_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.1\" & "4_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.1\" & "5_AddColumnCostCenter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.1\" & "6_AddTable_OpeningBudget.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.1\" & "7_AddColumnLargestPackQty.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.1\" & "8_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.1\" & "9_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2062 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 12
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.2\" & "1_AddStockAdjustmentTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.2\" & "2_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.2\" & "3_Add_Table_RootPlan.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.2\" & "4_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.2\" & "5_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.2\" & "6_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.2\" & "7_Alter_view_ArticleDefView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.2\" & "8_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.2\" & "8.1_ALTER_COADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.2\" & "8.2_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.2\" & "8.3_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.2\" & "9_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2063 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 23
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "1_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "2_Drop_SP_SalesOrderDetailHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "3_SP_SalesOrderDetailHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "4_Drop_SP_DSR_Statement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "5_SP_DSR_Statement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "6_Drop_SP_DSR_Summary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "7_SP_DSR_Summary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "8_Drop_SP_UnderTakingLatter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "9_SP_UnderTakingLatter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "10_Drop_SP_StockWiseLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "11_SP_StockWiseLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "12_Drop_SP_InvoiceWiseLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "13_SP_InvoiceWiseLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "14_Drop_SP_Rpt_Ledger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "15_SP_Rpt_Ledger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "16_Drop_SP_Payables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "17_SP_Payables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "18_Drop_SP_Receivables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "19_SP_Receivables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "20_Drop_View_GL_Voucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "21_Vew_GL_Voucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "22_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.3\" & "23_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2064 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 15
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "1_Drop_SP_AttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "2_SP_AttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "3_Drop_SP_Daily_Attendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "4_SP_Daily_Attendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "5_Drop_SP_Employee_Attendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "6_SP_Daily_Employee_Attendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "7_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "8_Drop_SP_DeliveryChalanSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "9_SP_DeliveryChalanSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "10_Drop_SP_DeliveryChalanSummUOM.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "11_SP_DeliveryChalanSummUOM.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "12_Drop_SP_AgreementLatter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "13_SP_AgreementLatter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "14_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.4\" & "15_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2065 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.5\" & "1_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.5\" & "2_Drop_SP_SalesOrderDetailHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.5\" & "3_SP_SalesOrderDetailHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.5\" & "4_Drop_SP_AddressEnvelop.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.5\" & "5_SP_AddressEnvelop.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.5\" & "6_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.5\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.5\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2066 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.6\" & "1_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.6\" & "2_Drop_SP_DSRStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.6\" & "3_SP_DSRStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.6\" & "4_Drop_SP_SubDSRExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.6\" & "5_SP_SubDSRExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.6\" & "6_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.6\" & "7_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2067 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.7\" & "1_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.7\" & "2_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.7\" & "3_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.7\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2068 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.8\" & "1_AddTableDateLock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.8\" & "2_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.8\" & "3_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.8\" & "4_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.8\" & "5_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.8\" & "6_Drop_SP_Stock_DeliveryChalan.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.8\" & "7_SP_Stock_DeliveryChalan.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.8\" & "8_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.8\" & "9_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.8\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2069 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.9\" & "1_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.9\" & "2_Drop_SP_SalesOrderDetailHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.9\" & "3_SP_SalesOrderDetailHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.9\" & "4_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.9\" & "5_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.6.9\" & "6_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2070 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.0\" & "1_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.0\" & "2_Add_Table_Agreement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.0\" & "3_Drop_SP_Agreement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.0\" & "4_SP_Agreement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.0\" & "5_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.0\" & "6_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.0\" & "7_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2071 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 15
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "1_Add_Table_EmployeeAccounts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "2_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "3_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "4_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "5_Drop_SP_ArticleLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "6_SP_ArticleLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "7_Update_Employee_Account_Info.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "8_Drop_SP_EmployeeBalanes.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "9_SP_EmployeeBalances.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "10_Drop_SP_EmployeeAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "11_SP_EmployeeAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "12_Drop_SP_EmpAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "13_SP_EmpAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "14_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.1\" & "15_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2072 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.2\" & "1_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.2\" & "2_Add_Table_PlanCostSheet.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.2\" & "3_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.2\" & "4_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.2\" & "5_Drop_SP_SalesOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.2\" & "6_SP_SalesOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.2\" & "7_Drop_SP_SalesOrderAnalysis.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.2\" & "8_SP_SalesOrderAnalysis.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.2\" & "9_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.2\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2073 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 22
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "1_AddTableProductionLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "2_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "3_Drop_SP_EmpAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "4_SP_EmpAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "5_Drop_SP_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "6_SP_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "7_Drop_SP_DailyEmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "8_SP_DailyEmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "9_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "10_Add_Table_PlanCostSheet.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "11_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "12_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "13_Drop_SP_SalesOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "14_SP_SalesOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "15_Drop_SP_SalesOrderAnalysis.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "16_SP_SalesOrderAnalysis.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "17_Drop_SP_StoreIssuanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "18_SP_StoreIssuanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "19_Drop_SP_StoreIssuance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "20_Sp_StoreIssuance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "21_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.3\" & "22_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2074 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.4\" & "1_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.4\" & "2_Drop_SP_StockstatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.4\" & "3_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.4\" & "4_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.4\" & "5_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.4\" & "6_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2075 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.5\" & "1_Drop_SP_Employee_Attendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.5\" & "2_SP_Employee_Attendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.5\" & "3_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.5\" & "4_Drop_SP_StoreIssuanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.5\" & "5_SP_StoreIssuanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.5\" & "6_Drop_SP_StoreIssuanceDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.5\" & "7_SP_StoreIssuanceDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.5\" & "8_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.5\" & "9_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2076 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.6\" & "1_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.6\" & "2_Drop_SP_SalesOrderQoutation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.6\" & "3_SP_SalesOrderQoutation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.6\" & "4_Drop_SP_Stock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.6\" & "5_SP_Stock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.6\" & "6_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.6\" & "7_Drop_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.6\" & "8_SP_StockStatement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.6\" & "9_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.6\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2077 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.7\" & "1_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.7\" & "2_Drop_SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.7\" & "3_SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.7\" & "4_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.7\" & "5_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.7\" & "6_Drop_SP_ItemLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.7\" & "7_SP_ItemLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.7\" & "8_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.7\" & "9_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2078 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.8\" & "1_AlterColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.8\" & "2_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.8\" & "3_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2079 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.9\" & "1_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.9\" & "2_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.9\" & "3_Drop_SP_BillEmberiodery.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.9\" & "4_SP_BillEmberiodery.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.9\" & "5_AlterColumnComments.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.9\" & "6_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.7.9\" & "7_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2080 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 20
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "1_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "2_Drop_SP_RptVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "3_SP_RptVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "4_Drop_SP_PostDatedChequeSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "5_SP_PostDatedChequeSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "6_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "7_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "8_Drop_SP_EmployeeAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "9_SP_EmployeeAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "10_Drop_SP_EmpAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "11_SP_EmpAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "12_Drop_SP_SalesOrderQuotation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "13_Create_SP_SalesOrderQuotation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "14_Drop_SP_ProductionAnalysis.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "15_Create_SP_ProductionAnalysis.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "16_Update_Tables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "17_Drop_SP_EmployeeInformation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "18_Sp_EmployeeInformation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "19_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.0\" & "20_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2081 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.1\" & "1_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.1\" & "2_Drop_SP_EmployeesLetters.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.1\" & "3_SP_EmployeesLetters.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.1\" & "4_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.1\" & "5_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2082 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.2\" & "1_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.2\" & "2_AddTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.2\" & "3_Drop_SP_EmpAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.2\" & "4_SP_EmpAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.2\" & "5_Drop_SP_ArticleHistoryByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.2\" & "6_SP_ArticleHistoryByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.2\" & "7_Drop_SP_StorageBilling.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.2\" & "8_SP_Storage_Billing.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.2\" & "9_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.2\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2083 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 12
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.3\" & "1_Drop_SP_Employee_Attendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.3\" & "2_SP_Employee_Attendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.3\" & "3_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.3\" & "4_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.3\" & "5_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.3\" & "6_Drop_SP_EmployeeAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.3\" & "7_SP_EmployeeAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.3\" & "8_ALTER_View_ArticleDefTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.3\" & "9_Drop_SP_EmpAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.3\" & "10_SP_EmpAttendanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.3\" & "11_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.3\" & "12_Update_DB_Version.sql")
                ProgBar()
            End If


            If CurrentVersion < 2084 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.4\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.4\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2085 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 20
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "1_Drop_SP_ContactList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "2_SP_ContactList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "3_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "4_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "5_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "6_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "7_Drop_SP_ItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "8_SP_ItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "9_Drop_SP_ItemWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "10_SP_ItemWiseSaleConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "11_Drop_SP_CashReceipt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "12_SP_CashReceipt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "13_Drop_SP_CashPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "14_SP_CashPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "15_SP_Drop_EmployeeAttendanceDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "16_SP_EmployeeAttendanceDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "17_SP_Drop_EmployeeFinalSettlement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "18_SP_EmployeeFinalSettlement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "19_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.5\" & "20_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2086 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.6\" & "1_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.6\" & "2_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.6\" & "3_Add_Unit_ArticlePackTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.6\" & "4_Drop_SP_ItemLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.6\" & "5_SP_ItemLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.6\" & "6_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.6\" & "7_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2087 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.7\" & "1_Drop_SP_Store_Issuance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.7\" & "2_SP_Store_Issuance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.7\" & "3_Drop_SP_Store_Issuance_Summary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.7\" & "4_SP_Store_Issuance_Summary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.7\" & "5_Drop_SP_Receivables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.7\" & "6_SP_Rpt_Receivables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.7\" & "7_Drop_SP_Paybles.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.7\" & "8_SP_Rpt_Payables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.7\" & "9_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.7\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2088 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 22
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "1_Drop_SP_Voucher_Check_List.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "2_SP_Voucher_Check_List.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "3_ADD_Table.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "4_Drop_SP_EmployeePromotionIncreament.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "6_Add_Table_ChequeSerial.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "7_Drop_SP_ItemWiseSalesReturnSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "8_SP_ItemWiseSalesReturnSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "9_Drop_SP_ItemWiseSalesReturnConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "10_SP_ItemWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "11_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "12_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "13_Drop_SP_SalesmanCommission.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "14_SP_SalesmanCommission.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "15_Drop_SP_SalesComparison.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "16_SP_SalesComparison.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "17_Drop_View_ArticleView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "18_Alter_ArticleView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "19_Drop_SP_ItemLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "20_SP_ItemLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "21_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.8\" & "22_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2089 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 21
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "1_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "2_Drop_SP_ItemLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "3_SP_ItemLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "4_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "5_Drop_SP_EmployeeSalary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "6_SP_Employee_Salary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "7_Drop_SP_EmployeePromotion.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "8_SP_EmployeePromotion.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "9_Add_Table_EmployeePromotion.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "10_Update_VehicleLogRightModule.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "11_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "12_Drop_SP_StockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "13_SP_StockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "14_Drop_SP_LocationWiseStockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "15_SP_LocationWiseStockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "16_Drop_SP_LocationWiseStockLevelSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "17_SP_LocationWiseStockLevelSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "18_Drop_SP_BalancesheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "19_Sp_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "20_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.8.9\" & "21_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2090 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 13
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.0\" & "1_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.0\" & "2_Drop_SP_StockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.0\" & "3_SP_StockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.0\" & "4_Drop_SP_LocationWiseStockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.0\" & "5_SP_LocationWiseStockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.0\" & "6_Drop_SP_LocationWiseStockLevelSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.0\" & "7_SP_LocationWiseStockLevelSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.0\" & "8_Drop_SP_BalancesheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.0\" & "9_Sp_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.0\" & "10_ALTERColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.0\" & "11_AlterCOAView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.0\" & "12_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.0\" & "13_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2091 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 12
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.1\" & "1_Drop_SP_BalancesheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.1\" & "2_Sp_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.1\" & "3_Drop_View_Article.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.1\" & "4_View_Article.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.1\" & "5_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.1\" & "6_Drop_SP_UnderTakingLetter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.1\" & "7_SP_UrderTakingLetter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.1\" & "8_Drop_PreviouseBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.1\" & "9_vw_PreviousBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.1\" & "10_Add_Columns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.1\" & "11_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.1\" & "12_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2092 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.2\" & "R925_1_IMR_AlterColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.0.9.2\" & "R928_2_IMR_AddColumn_Comments_PurchaseOrderDetail.sql")
                ProgBar()
            End If
            If CurrentVersion < 2100 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 21
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "1_R916_IMR_AddColumn_Comments.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "2_R928_IMR_AddColumn_Comments_PurchaseOrderDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "3_R929_IMR_ConfigValues_OnetimeSalesReturn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "4_R883_IMR_AddColum_Credit_Limit.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "5_R957_MIJ_AddTable_tblBankInfo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "6_R957_MIJ_Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "7_R941_MIJ_UpdateFormsCaption.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "8_R958_MIJ_DeleteSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "9_R958_MIJ_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "10_R966_MIJ_DeleteSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "11_R966_MIJ_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "12_R970_IMR_AddReceivingNoteMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "13_R970_IMR_AddTableReceivingNoteDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "14_R970_IMR_AddColumn_ReceivingNoteId_ReceivingMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "15_R970_IMR_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "15.1_R913_IMR_AddColumn_Post.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "15.2_R913_IMR_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "15.3.1_R970_Drop_SP_ReceivingNote.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "15.3_R970_IMR_SP_ReceivingNote.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "16_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.0\" & "17_Update_DB_Version.sql")
                ProgBar()

            End If
            If CurrentVersion < 2101 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 24
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "1_R968_MIJ_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "2_RM4_IMR_Drop_SP_Lgr_Aging_Balance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "3_RM4_IMR_SP_LGR_AGING_BALANCE.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "4_RM5_IMR_Drop_SP_SalesReturnSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "5_RM5_IMR_SP_SalesReturnSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "6_RM3_IMR_Drop_SP_Receiveables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "7_RM3_IMR_SP_Receiveables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "8_RM3_IMR_Drop_SP_Payables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "9_RM3_IMR_SP_Payables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "10_RM6_IMR_AddColumn_Branch_PhoneNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "11_RM6_IMR_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "12_R979_IMR_ConfigValues_AutoLoadPO.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "13_TSK2359_IMR_UpdateConfigDecimalPoint.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "14_R955_R@!_AddColumnPayeeTitle.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "15_TSK2364_IMR_AddTableQuotationMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "16_TSK2364_IMR_AddTableQuotationDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "17_TSK2364_IMR_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "19_TSK2357_IMR_Drop_SP_SalesComparisonYearWise.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "20_TSK2357_IMR_SP_SalesComparisonYearWise.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "21_TSK2368_IMR_AddTable_tblChequeLayout.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "22_TSK2368_IMR_AddColumn_ChequeLayoutId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "23_TSK2357_IMR_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "24_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.1\" & "25_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2102 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 13
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.2\" & "1_TSK2369_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.2\" & "2_TSK2370_IMR_AddColumns_InvoicesBasedPayment_AND_Receipt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.2\" & "3_TSK2371_Drop_SP_SalesOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.2\" & "4_TSK2371_IMR_SP_SalesOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.2\" & "5_TSK2371_Drop_SP_DeliveryChalan.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.2\" & "6_TSK2371_IMR_SP_DeliveryChalan.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.2\" & "7_TSK2370_IMR_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.2\" & "8_TSK2370_Drop_SP_InvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.2\" & "9_TSK2370_IMR_SP_InvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.2\" & "10_TSK2370_Drop_SP_PurchaseInvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.2\" & "11_TSK2370_IMR_SP_PurchaseInvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.2\" & "12_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.2\" & "13_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2103 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.3\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.3\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2104 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.4\" & "1_TSK2377_IMR_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.4\" & "2_TSK2376_IMR_INSERTPurchaseCommentsLeyoutConfigurations.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.4\" & "3_TSK2377_Drop_SP_PostDatedChequeSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.4\" & "4_TSK2377_IMR_SP_PostDatedChequeSummary_Revised.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.4\" & "5_TSK2382_IMR_AddColumn_PayeeTitleInvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.4\" & "6_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.4\" & "7_Update_Release_Log.sql")


                ProgBar()
            End If

            If CurrentVersion < 2105 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.5\" & "1_TSK2380_IMR_AddRights_CreditSalesPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.5\" & "2_TSK2385_IMR_Drop_SP_PostDatedChequeSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.5\" & "3_TSK2385_IMR_SP_PostDatedChequeSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.5\" & "4_TSK2388_IMR_DropView_ArticleDefView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.5\" & "5_TSK2388_IMR_AlterArticleDefView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.5\" & "6_TSK2391_IMR_AddColumn_ArticleDefId_tblVoucherDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.5\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.5\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If


            If CurrentVersion < 2106 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.6\" & "1_TSK2397_IMR_Drop_SP_TaxCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.6\" & "2_TSK2397_IMR_SP_TaxCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.6\" & "3_TSK2399_IMR_Drop_SP_Rpt_Receiable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.6\" & "4_TSK2399_IMR_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.6\" & "5_TSK2399_IMR_Drop_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.6\" & "6_TSK2399_IMR_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.6\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.6\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2107 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.7\" & "1_TSK2400_IMR_AddColumn_Attachment_tblVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.7\" & "2_TSK2400_IMR_ConfigValues_FileAttachmentPath.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.7\" & "3_TSK2395_IMR_AddNewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.7\" & "4_TSK2406_IMR_FieldChooserSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.7\" & "5_TSK2408_IMR_Added_Posted_Rights_On_SalesOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.7\" & "6_TSK2410_IMR_AddSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.7\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.7\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2108 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.8\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.8\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2109 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.9\" & "1_TSKM16_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.9\" & "2_TSKM16_IMR_AddConfiguration_VehicleIdentificationInfo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.9\" & "3_TSK2416_IMR_DropView_ArticleDefView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.9\" & "4_TSK2416_IMR_ALTER_ArticleDefView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.9\" & "5_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.0.9\" & "6_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2110 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.0\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.0\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2111 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.1\" & "1_TSK2426_IMR_AddTables_PaymentSchedule.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.1\" & "2_TSK2432_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.1\" & "3_TSK2433_Drop_SP_PostDatedChequeSummary.SQL")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.1\" & "4_TSK2433_SP_PostDatedChequeSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.1\" & "5_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.1\" & "6_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2112 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.2\" & "1_TASK2435_IMR_AddColumn_CostPrice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.2\" & "2_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.2\" & "3_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2113 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.3\" & "1_TSKM20_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.3\" & "2_TSKM21_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.3\" & "3_TASKM21_IMR_Add_Security_Issued.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.3\" & "4_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.3\" & "5_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2114 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.4\" & "1_TASKM22_IMRAN_AddColumns_AnnualAllowedLeave.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.4\" & "2_TASKM23_IMR_AddTable_tblEmployeeLeaveDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.4\" & "3_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.4\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2115 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.5\" & "1_TSK2441_IMR_Drop_SP_NonInteractCustomers.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.5\" & "2_TSK2441_IMR_SP_NonInteractCustomers.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.5\" & "3_TSK2441_IMR_Drop_SP_NonInteractVendors.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.5\" & "4_TSK2441_IMR_SP_NonInteractVendors.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.5\" & "5_TSK2441_IMR_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.5\" & "6_TSK2442_IMR_ConfigValues_CommentDCNoAndInvoiceNo_On_PurchaseLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.5\" & "7_TSK2443_IMR_AlterColumnCheque_No_Voucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.5\" & "8_TSK2446_IMR_ConfigValues_DCNO_Engine_No.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.5\" & "9_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.5\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2116 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 14
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.6\" & "1_TSKM22_IMR_Drop_SP_Payables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.6\" & "2_TSKM22_IMR_SP_Rpt_Payables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.6\" & "3_TSKM23_IMR_Drop_SP_Receivables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.6\" & "4_TSKM23_IMR_SP_Rpt_Receivables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.6\" & "5_TSK2451_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.6\" & "6_TSK2456_IMR_Drop_SP_Rpt_TaxCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.6\" & "7_TSK2456_IMR_SP_Rpt_TaxCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.6\" & "8_TSK2457_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.6\" & "9_TSK2457_IMR_Drop_SP_Rpt_SalesCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.6\" & "10_TSK2457_IMR_SP_Rpt_SalesCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.6\" & "11_TSK2462_IMR_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.6\" & "12_TSK2470_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.6\" & "13_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.6\" & "14_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2117 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 15
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "1_TSK2488_IMR_AddTable_SalelsCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "2_TSK2488_IMR_Drop_SP_Rpt_SalesCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "3_TSK2488_IMR_SP_Rpt_SalesCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "4_TSK2488_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "5_TASK2488_IMR_AddColumn_SaleCertificateId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "6_TSK2488_IMR_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "7_TSK2489_IMR_Drop_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "8_TSK2489_IMR_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "9_TSK2502_IMR_Drop_ProductionSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "10_TSK2502_IMR_ProductionSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "11_TSK2502_IMR_Drop_ProductionItems.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "12_TSK2502_IMR_SP_ProductionItems.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "13_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "14_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.7\" & "15_TSK2504_IMR_ALTER_tblVoucherDetail.sql")
                ProgBar()
            End If
            If CurrentVersion < 2118 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.8\" & "1_TSK2506_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.8\" & "2_TSK2506_IMR_Drop_SP_StoreIssuanceDetailBatchWise.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.8\" & "3_TSK2506_IMR_SP_StoreIssuanceDetailBatchWise.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.8\" & "4_TSK2507_IMR_Drop_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.8\" & "5_TSK2507_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.8\" & "6_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.8\" & "7_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2119 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.9\" & "1_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.1.9\" & "2_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2120 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.0\" & "1_TSK2522_IMR_Drop_SP_IssuedSalesCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.0\" & "2_TSK2522_IMR_SP_SalesCertificateIssued.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.0\" & "3_TASK2526_IMR_AddColumn_Cheque_Status.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.0\" & "4_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.0\" & "5_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2121 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.1\" & "1_TASK2528_IMR_ew Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.1\" & "2_TSK2532_MUG_AddColumn_ModelCode.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.1\" & "3_TSK2532_MUG RevisedSalesCertificate_Drop_SP_Rpt_SalesCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.1\" & "4_TSK2532_MUG RevisedSalesCertificate_SP_Rpt_SalesCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.1\" & "5_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.1\" & "6_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2122 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.2\" & "1_TASK2538_IMR_Drop_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.2\" & "2_TASK2538_IMR_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.2\" & "3_TSK2537_MUG_ConfigrValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.2\" & "4_TSK2537_MUG_AddColumn_Post.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.2\" & "5_TSK2537_MUG_AddColumn_Post.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.2\" & "6_TASK2540_IMR_Drop_SP_Rpt_GP_Printing.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.2\" & "7_TASK2540_IMR_SP_Rpt_GP_Printing.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.2\" & "8_TSK2541_MUG_AddColumn_UserName_DateEntry.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.2\" & "9_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.2\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2123 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.3\" & "1_TSK2553_MUG Receiveables_Drop_SP_Rpt_Receiveables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.3\" & "2_TSK2553_MUG Receiveables_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.3\" & "3_TASK2558_IMR_Drop_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.3\" & "4_TASK2558_IMR_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.3\" & "5_TASK2559_IMR_Drop_SP_CashPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.3\" & "6_TASK2559_IMR_SP_CashPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.3\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.3\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2124 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.4\" & "1_TSK2555_MUG_AddColumn_UOM.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.4\" & "2_TSK2574_IMR_Drop_SP_Rpt_Receiveables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.4\" & "3_TASK2574_IMR_SP_Rpt_Recievables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.4\" & "4_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.4\" & "5_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2125 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.5\" & "1_TASK2575_IMR_Drop_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.5\" & "2_TASK2575_IMR_Sp_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.5\" & "3_TASK2576_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.5\" & "4_TASK2577_IMR_Drop_SP_ContactList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.5\" & "5_TASK2577_IMR_SP_ContactList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.5\" & "6_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.5\" & "7_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2126 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.6\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.6\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2127 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.7\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.7\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2128 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 18
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "1_TSK2593_IMR_AddColumn_AttendanceAuto.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "2_TASK2592_IMR_AddTable_tblEmployeeOverTimeSchedule.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "3_TSK2593_MUG_AddColumn_Flexiblity_In_Time.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "4_TSK2593_MUG_AddColumn_Flexiblity_Out_Time.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "5_TSK2593_MUG_AddColumn_Auto.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "6_TSK2593_MUG_AddColumn_Sch_In_Time.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "7_TSK2593_MUG_AddColumn_Sch_Out_Time.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "8_TSK2598_MUG_AddColumn_Comments.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "9_TASK2595_IMR_Table_tblTempEmployeeAttedanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "10_TASK2595_IMR_Drop_SP_EmpAttendanceDt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "11_TASK2595_IMR_SP_EmpAttendanceDt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "12_TSK2595_IMR_AddNewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "13_TSK2595_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "14_TSK2591_JUN_ADD_COLUMN_ShiftTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "15_TASK2593_IMR_AlterColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "16_TSK2592_JUN_Security_Rights_tblForms.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "17_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.8\" & "18_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2129 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.9\" & "1_TASK2538_IMR_Drop_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.9\" & "2_TASK2599_IMR_SP_StockDeliveryChalan_AND_Production.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.9\" & "3_TSK2598_MUG_AddColumn_Comments.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.9\" & "4_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.2.9\" & "5_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2130 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.0\" & "1_TSK2593_IMR_AddColumn_AttendanceAuto.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.0\" & "2_TASK2595_IMR_AddTableLeaveEncashment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.0\" & "3_TASK2595_IMR_Drop_SP_EmpAttendanceDt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.0\" & "4_TASK2595_IMR_SP_EmpAttendanceDt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.0\" & "5_TSK2595_IMR_AddNewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.0\" & "6_TSK2595_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.0\" & "7_TASK2595_IMR_Table_tblTempEmployeeAttedanceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.0\" & "8_TSK2592_JUN_Security_Rights_tblForms.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.0\" & "9_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.0\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2131 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.1\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.1\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2132 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.2\" & "1_TSK2622_MUG StockDilveryByChalan_Drop_SP_StockDeliveryChalan_AND_Production.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.2\" & "2_TSK2622_MUG StockByDilveryChalan_SP_StockDelivery ByChalan_AND_Production.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.2\" & "3_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.2\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2133 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.3\" & "1_TASKM2623_IMR_tblLatetimeSlot.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.3\" & "2_TSK2623_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.3\" & "3_TSK2616_MUG_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.3\" & "4_TASK2625_IMR_AddNewSecurity_CostSheet.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.3\" & "5_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.3\" & "6_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2134 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 15
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "1_TSK2634_IMR_AddTable_AdjustmentAvgRate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "2_TSK_2639_JUN_Create_tblVendorType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "3_TSK2642_IMR_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "4_TSK2638_IMR_ADDTABLE_WarrantyClaimTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "5_TSK_2639_JUN_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "6_TSK_2639_JUN_AddColumn_tblVendor.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "7_TASK2638_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "8_TASK2643_IMR_Drop_SP_WarrantyClaim.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "9_TASK2643_IMR_SP_WarrantyClaim.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "10_TSK2645_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "11_TSK2645_IMR_Add_Security_Rights_None_Financial_Invoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "12_TSK2660_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "13_TSK2660_IMR_AddFormSecurity.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "14_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.4\" & "15_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2135 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.5\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.5\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2136 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.6\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.6\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2137 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 21
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "1_TSK2673_IMR_AddTableCMFA.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "2_TASK2673_IMR_AddColumns_RefCMFADocId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "2.1_TASK2673_IMR_AddColumn_Opex_Sale_Percent.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "3_TSKM53_IMR_Drop_SP_CMFADocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "4_TASKM53_IMR_SP_CMFADocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "5_TSKM53_IMR_Drop_SP_SMFAExp.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "6_TASKM53_IMR_SP_CMFAExp.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "7_TSKM53_IMR_Drop_SP_DeliveryChalanStock_And_Production.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "8_TSKM53_IMR_SP_StockDeliveryChalan_And_Production.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "9_TASK2677_IMR_AddColumn_CommercialInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "10_TASK2678_IMR_AddColumns_InvoiceAmountSalesTax.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "11_TASK2678_IMR_Drop_SP_ManualSalesInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "12_TASK2678_IMR_SP_ManualSaleInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "13_TASK2680_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "14_TSK_2673_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "14_TSK_2673_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "15_TSK2681_IMR_AddColumns_InvoiceParty.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "16_TASK2689_IMR_AddColumn_DepartmentId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "17_TASK2690_IMR_AddColumns_DepartmentIdEmployeeIdOnProduction.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "18_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.7\" & "19_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2138 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.8\" & "1_TASK2697_IMR_ALTERColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.8\" & "2_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.8\" & "3_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2139 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.9\" & "1_TASK2701_IMR_AddConfiguration.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.9\" & "2_TASK2701_IMR_AddTable_CMFAExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.9\" & "3_TASK2701_IMR_AddColumn_CashRequesID.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.9\" & "4_TASK2702_IMR_AddTable_CashRequest.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.9\" & "5_TASK2701_IMR_Drop_SP_CMFAExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.9\" & "6_TASK2701_IMR_SP_CMFAExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.9\" & "7_TASK2701_IMR_Drop_SP_CMFADocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.9\" & "8_TASK2701_AddSecurity_CMFAComparison.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.9\" & "9_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.3.9\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2140 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 13
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.0\" & "1_TASK2702_IMR_AddColumns_CMFADocID.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.0\" & "2_TASK2702_IMR_AddConfig_CMFADocumentOnSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.0\" & "3_TASK2702_IMR_AddColumn_SoldQty.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.0\" & "4_TASK2703_IMR_ADDColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.0\" & "5_TASK2703_IMR_AddTable_CMFAAttachDocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.0\" & "6_TASK2704_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.0\" & "7_TASK2703_IMR_AddColumn_CMFAType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.0\" & "8_TASK2703_IMR_AddConfig_AttachPath.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.0\" & "9_TASK2704_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.0\" & "10_TSK2703_IMR_Add_Security_Rights_None_Financial_Invoice.sql")
                Try
                    executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.0\" & "11_TASK2704_IMR_ALTERTABLE_CashRequestDetail.sql")
                Catch ex As Exception
                End Try
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.0\" & "12_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.0\" & "13_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2141 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.1\" & "1_TASK2705_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.1\" & "2_TASK2706_IMR_AddTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.1\" & "3_TSK2705_IMR_Add_Security_Right.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.1\" & "4_TASKM59_IMR_AddColumns_ActivityLog.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.1\" & "5_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.1\" & "6_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2142 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.2\" & "1_TASK2707_IMR_AddColumn_AccessKey_tblForms.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.2\" & "2_TASK2707_IMR_AddColumns_PurchaseCGSAccount_AdjustmentAverageRateDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.2\" & "3_TASK2708_IMR_AddColumns_RegistrationFor_Tax_Percent.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.2\" & "4_TAKS2708_IMR_Drop_SP_SalesCertificateIssued.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.2\" & "5_TASK2708_IMR_SP_SalesCertificateIssued.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.2\" & "6_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.2\" & "7_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2143 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.3\" & "1_TASKM60_ADDColumns_CMFADocId_tblVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.3\" & "2_TASKM60_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.3\" & "3_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.3\" & "4_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2144 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.4\" & "1_TSK2716_IMR_Add_Security_Rights_SelectedIssuenceUpdate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.4\" & "2_TASKM2716_ADDColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.4\" & "3_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.4\" & "4_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2145 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.5\" & "1_TASK2717_IMR_AddColumn_ReferenceNo_SalesCertificateTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.5\" & "2_TASK2717_IMR_Drop_SP_SalesCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.5\" & "3_TASK2717_IMR_SP_SalesCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.5\" & "4_TASKM62_IMR_Update_Security_CashRequest.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.5\" & "5_TASK2718_IMR_Drop_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.5\" & "6_TASK2718_IMR_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.5\" & "9_TASK2723_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.5\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.5\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2146 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.6\" & "1_TASK2725_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.6\" & "2_TASK2726_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.6\" & "3_TSK2728_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.6\" & "4_TASK2730_IMR_Drop_SP_CMFADocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.6\" & "5_TASK2730_IMR_SP_CMFADocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.6\" & "6_TASK2708_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.6\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.6\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2147 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.7\" & "1_TASK2734_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.7\" & "2_TASK2734_UpdateProjectedExpAmount.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.7\" & "3_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.7\" & "4_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2148 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.8\" & "1_TASKM70_IMR_Drop_SP_CMFADocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.8\" & "2_TASKM70_IMR_SP_CMFADocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.8\" & "3_TASKM71_IMR_Drop_SP_DSRStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.8\" & "4_TASKM71_IMR_SP_DSRStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.8\" & "5_TASKM72_IMR_AddConfigCMFADocumentAttachment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.8\" & "6_TASKM72_IMR_Drop_SP_Damage_Budget.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.8\" & "7_TASKM72_IMR_SP_Damage_Budget.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.8\" & "8_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.8\" & "9_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2149 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.9\" & "1_TSK2753_IMR_AddColumn_EmployeeID_ProductionMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.9\" & "2_TASKM74_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.9\" & "3_TASK2757_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.9\" & "4_TASK2756_IMR_AddTable_tblEmployeeArticleCostRate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.9\" & "5_TASK2756_ADDColumn_RefEmployeeId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.9\" & "8_TASKM74_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.9\" & "6_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.4.9\" & "7_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2150 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.0\" & "1_TASK2763_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.0\" & "2_TSK2762_AddConfigu.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.0\" & "3_Task2765_IMR_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.0\" & "4_TASK2765_IMR_Drop_SP_SiteRegistration.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.0\" & "5_TASK2765_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.0\" & "6_TASK2765_IMR_SP_SiteRegistration.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.0\" & "7_TSK2767_IMR_AddConfigu.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.0\" & "8_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.0\" & "9_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2151 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 13
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.1\" & "1_TASK2769_IMR_Drop_SP_CMFASummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.1\" & "2_TASK2769_IMR_SP_CMFASummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.1\" & "3_TASK2769_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.1\" & "4_TASK2770_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.1\" & "5_TASK2774_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.1\" & "6_TASK2774_IMR_Drop_SP_PurchaseInvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.1\" & "7_TASK2774_IMR_SP_PurchaseInvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.1\" & "8_TASK2774_IMR_Drop_SP_InvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.1\" & "9_TASK2774_IMR_Sp_InvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.1\" & "10_TSKM90_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.1\" & "11_TASK2784_IMR_AddConfig_PurchaseAccountMappingFrontEnd.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.1\" & "12_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.1\" & "13_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2153 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.3\" & "1_TASK2790_IMR_AddColumn_PurchaseAcId_ReceivingMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.3\" & "2_TASK2791_IMR_Drop_SP_PurchaseInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.3\" & "3_TASK2791_IMR_SP_PurchaseInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.3\" & "4_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.3\" & "5_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2154 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.4\" & "1_TASK2796_IMR_AddConfig_OrderQtyExceed.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.4\" & "2_TASK2798_IMR_Drop_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.4\" & "3_TASK2798_IMR_Sp_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.4\" & "4_TASK2799_IMR_AddColumns_tblCustomer.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.4\" & "5_TASK2799_IMR_Drop_vwCOADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.4\" & "6_Task2799_IMR_vwCOADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.4\" & "7_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.4\" & "8_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2155 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.5\" & "1_TASK2810_Drop_SP_PLSubSubAccountSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.5\" & "2_TASK2810_IMR_SP_PLSubSubAccountSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.5\" & "3_TASK2810_IMR_Drop_SP_PLDetailAccountSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.5\" & "4_TASK2810_IMR_SP_PLDetailAccountSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.5\" & "5_TASK2811_IMR_Drop_SP_DirectorDebitors.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.5\" & "6_TASK2811_IMR_SP_DirectorDebitors.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.5\" & "7_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.5\" & "8_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2156 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.6\" & "1_TASKM90_IMR_AddTable_SMSTemplate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.6\" & "2_TASK2818_IMR_AddTable_InvoiceAdjustmentTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.6\" & "3_TASK2818_IMR_Drop_SP_VoucherBalancesList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.6\" & "4_TASK2818_IMR_SP_VoucherBalancesList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.6\" & "5_TASK2818_IMR_Drop_SP_InvoiceList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.6\" & "6_Task2818_IMR_SP_InvoiceList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.6\" & "7_TASK2818_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.6\" & "8_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.6\" & "9_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2157 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.7\" & "1_TASK2820_IMR_Drop_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.7\" & "2_TASK2820_IMR_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.7\" & "3_TASK2820_IMR_Drop_SP_VoucherBalanceList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.7\" & "4_TASK2820_IMR_SP_VoucherBalancesList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.7\" & "5_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.7\" & "6_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2158 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 15
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "1_TASK2823_IMR_AddTable_tblTempInvoiceAdjustmentBalances.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "2_TASK2823_IMR_Drop_SP_AdjustmentBalances.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "3_TASK2823_IMR_SP_AdjustmentBalances.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "4_TASK2823_IMR_Drop_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "5_TASK2823_IMR_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "6_TASK2823_IMR_Drop_SP_InvoiceList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "7_TASK2823_IMR_SP_InvoiceList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "8_TASK2823_IMR_Drop_SP_VoucherBalancesList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "9_TASK2823_IMR_SP_VoucherBalanceList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "10_TASK2824_IMR_AddColumn_CheckedStatus_CMFAMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "11_TASK2824_IMR_Update_CheckedStatus_CMFAMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "12_TASK2826_IMR_AddColumn_Checked_tblVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "13_TASK2826_AddSecurityRights_Checked_Voucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "14_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.8\" & "15_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2159 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.9\" & "1_TASK2830_IMR_AddSecurityRights_AddCashBankCustomerVendor_Accounts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.9\" & "2_TASK2831_IMR_Drop_SP_InvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.9\" & "3_TASK2831_IMR_SP_InvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.9\" & "4_TASK2831_IMR_Drop_SP_PurchaseInvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.9\" & "5_TASK2831_IMR_SP_PurchaseInvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.9\" & "6_TASK2832_IMR_AddColumn_PlanedQty_SalesOrderDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.9\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.5.9\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2160 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.0\" & "1_TASKM101_IMR_AddColumn_Remarks_tblVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.0\" & "2_TASKM100_IMR_Drop_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.0\" & "3_TASKM100_IMR_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.0\" & "4_TASK2845_IMR_AddTables_SMSLog.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.0\" & "5_TASK2845_IMR_AddTable_SMSEnableConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.0\" & "6_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.0\" & "7_Update_Release_Log.sql")

                ProgBar()
            End If
            If CurrentVersion < 2161 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.1\" & "1_TASK2845_IMR_AddConfig_SMSScheduleTime.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.1\" & "2_TASKM102_IMR_AddColumn_PurchaseAcId_PurchaseReturnMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.1\" & "3_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.1\" & "4_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2162 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.2\" & "1_AddTable_DocumentAttachment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.2\" & "2_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.2\" & "3_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2163 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 14
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.3\" & "1_TASK189142_IMR_Drop_SP_InvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.3\" & "2_TASKM189142_IMR_SP_InvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.3\" & "3_TASKM189142_IMR_Drop_SP_PurchaseInvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.3\" & "4_TASKM1819142_IMR_SP_PurchaseInvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.3\" & "5_TASK2847_IMR_AddConfig_TotalAmountWiseInvoiceBasedVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.3\" & "6_TASK189141_IMR_Drop_SP_Rpt_BankReconciliation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.3\" & "7_TASKM199141_IMR_SP_Rpt_BankReconciliation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.3\" & "8_TASKM229141_IMR_AddSecurity_ShowBalance_PaybleReceivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.3\" & "9_TASKM229142_Drop_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.3\" & "10_TASKM229142_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.3\" & "11_TASK2850_IMR_Drop_SP_PurchaseItemSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.3\" & "12_TASK2850_IMR_SP_PurchaseItemSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.3\" & "13_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.3\" & "14_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2164 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 13
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.4\" & "1_TASKM239141_IMR_Drop_SP_SalesOfInvoiceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.4\" & "2_TASKM239141_IMR_SP_SalesOfInvoiceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.4\" & "3_TASK2856_IMR_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.4\" & "4_TASK2856_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.4\" & "5_TASK2858_IMR_Drop_SP_CMFADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.4\" & "6_TASK2858_IMR_SP_CMFADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.4\" & "7_TASK2858_IMR_Drop_SP_SummaryAgainstCMFA.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.4\" & "8_TASK2858_IMR_SP_SummaryAgainstCMFA.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.4\" & "9_TASK2858_IMR_Drop_SP_CMFAOfSummaries.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.4\" & "10_TASK2858_IMR_SP_CMFAOfSummaries.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.4\" & "11_TASK2858_IMR_AddSecurityRights_CMFAOfSummaries.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.4\" & "12_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.4\" & "13_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2165 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.5\" & "1_TASKM102143_IMR_Drop_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.5\" & "2_TASKM102143_IMR_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.5\" & "3_TASK2863_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.5\" & "4_TASK2864_IMR_Drop_SP_CashReceiptsAgainstEmployee.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.5\" & "5_TASK2864_IMR_SP_CashReceiptsAgainstEmployee.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.5\" & "6_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.5\" & "7_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2166 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.6\" & "1_TASK109141_IMR_Drop_SP_ArticleHistory_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.6\" & "2_TASK109141_IMR_ArticleHistory_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.6\" & "5_TAKS_109142_IMR_Drop_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.6\" & "6_TASK109142_IMR_SP_StockStatementNew.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.6\" & "3_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.6\" & "4_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2167 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 12
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.7\" & "1_TASKM1013143_IMR_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.7\" & "2_TASKM1016141_IMR_Drop_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.7\" & "3_TASKM1016141_IMR_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.7\" & "4_TASKM1016142_IMR_Drop_SP_CMFADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.7\" & "5_TASKM1016142_IMR_SP_CMFADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.7\" & "6_TASKM1016142_IMR_Drop_SP_CMFASalesInv.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.7\" & "7_TASKM1016142_IMR_SP_CMFASalesInv.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.7\" & "8_TASKM1017141_IMR_AddConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.7\" & "9_TASKM1017142_IMR_AddColumn_StoreIssuanceAccountId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.7\" & "10_TASKM1013143_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.7\" & "11_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.7\" & "12_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2168 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.8\" & "1_TASKM1020141_ADDTABLE.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.8\" & "2_TASKM1020141_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.8\" & "3_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.8\" & "4_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2169 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 16
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "1_TASKM2810141_IMR_Update_SMS_Key.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "2_TASKM2810142_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "3_TSKM1029141_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "4_TASKM1029142_IMR_SMSConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "5_TASKM1030141_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "6_TASKM1030142_IMR_Drop_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "7_TASKM1030142_IMR_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "8_TASKM1030143_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "9_TASKM1030144_IMR_Drop_vwCOADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "10_TASKM1030144_IMR_vwCOADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "11_TASKM1031141_IMR_Drop_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "12_TASKM1031141_IMR_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "13_TASKM1031141_IMR_Drop_SP_InvoiceBasedReceipt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "14_TASKM1031141_IMR_SP_InvoiceBasedReceipt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "15_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.6.9\" & "16_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2170 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.0\" & "1_Update_DB_Version.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.0\" & "2_Update_Release_Log.sql")
                ProgBar()
            End If
            If CurrentVersion < 2171 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.1\" & "1_TASKM117141_IMR_Drop_SP_LocationWiseStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.1\" & "2_TASKM117141_IMR_SP_LocationWiseStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.1\" & "3_TASKM118141_IMR_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.1\" & "4_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.1\" & "5_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2172 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.2\" & "1_TASKM1211141_IMR_Drop_SP_MonthlyPurchaseSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.2\" & "2_TASKM1211141_IMR_SP_MonthlyPurchaseSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.2\" & "3_TAKSM1411141_IMR_Drop_SP_CMFAAll.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.2\" & "4_TASKM1411141_IMR_SP_CMFAAll.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.2\" & "5_TAKSM1411142_IMR_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.2\" & "6_TASKM1411142_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.2\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.2\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2173 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.3\" & "1_TAKSM1711141_IMR_AddConfigSMS.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.3\" & "2_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.3\" & "3_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2174 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.4\" & "1_TASKM1911141_IMR_Drop_SP_Employee_Attendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.4\" & "2_TASKM1911141_IMR_SP_Employee_Attendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.4\" & "3_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.4\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2175 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.5\" & "1_TASKM2011141_IMR_Drop_vwRemainingStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.5\" & "2_TASKM2011141_IMR_vwRemainingStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.5\" & "3_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.5\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2176 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.6\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.6\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2177 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.7\" & "1_TASKM2411141_IMR_AddConfigDNSHostForSMS.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.7\" & "2_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.7\" & "3_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2178 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.8\" & "1_TASKM2711143_IMR_Drop_SP_VoucherBalanceList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.8\" & "2_TASKM2711143_IMR_SP_VoucherBalanceList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.8\" & "3_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.8\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2179 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.9\" & "1_TASKM2811141_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.9\" & "2_TASKM2911141_IMR_Drop_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.9\" & "3_TASKM2911141_IMR_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.9\" & "4_TASKM2911141_IMR_Drop_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.9\" & "5_TASKM2911141_IMR_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.9\" & "6_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.7.9\" & "7_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2180 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.0\" & "1_TASKM312141_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.0\" & "2_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.0\" & "3_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2181 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.1\" & "1_TASKM312141_IMR_Drop_SP_RptVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.1\" & "2_TASKM312141_IMR_SP_RptVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.1\" & "3_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.1\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2182 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.2\" & "1_TASKM412141_IMR_ALTERColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.2\" & "2_TASKM312142_IMR_AddConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.2\" & "3_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.2\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2183 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.3\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.3\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2184 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.4\" & "1_TASKM612141_IMR_DeleteDuplicateCustomerInformation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.4\" & "2_TASKM612142_IMR_Drop_SP_InvoiceList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.4\" & "3_TASKM612142_IMR_SP_InvoiceList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.4\" & "4_TASKM612143_IMR_AddSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.4\" & "5_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.4\" & "6_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2185 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.5\" & "1_TASKM812141_IMR_Drop_SP_SummaryOfSalesInvoices.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.5\" & "2_TASKM812141_IMR_SP_SummaryOfSalesTaxInvoices.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.5\" & "3_TASKM812142_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.5\" & "4_TASKM912141_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.5\" & "5_TASKM912142_IMR_Drop_SP_SalesOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.5\" & "6_TASKM912142_IMR_SP_SalesOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.5\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.5\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2186 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 18
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "1_TASKM1612141_IMR_Drop_SP_ItemLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "2_TASKM1612141_IMR_SP_ItemLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "3_TASKM1612141_IMR_Drop_SP_Stock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "4_TASKM1612141_IMR_SP_SP_Stock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "5_TASKM1612141_IMR_Drop_SP_ProduceItems.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "6_TAKSM1612141_IMR_SP_ProduceItems.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "7_TASKM1612141_IMR_Drop_SP_ProductionHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "8_TASKM1612141_IMR_SP_ProductionHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "9_TASKM1712141_IMR_Drop_vwRemainingStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "10_TASKM1712141_IMR_vwRemainingStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "11_TASKM1812141_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "12_TASKM1812141_IMR_Drop_SP_PLSubSubAccountSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "13_TASKM1812141_IMR_SP_PLSubSubAccountSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "14_TASKM1812141_IMR_Drop_SP_PLDetailAccountSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "15_TASKM1812141_IMR_SP_PLDetailAccountSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "15.1_TASKM1912141_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "16_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.6\" & "17_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2187 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.7\" & "1_TASKM2212141_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.7\" & "2_TASKM2212141_IMR_AddConfigSMS.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.7\" & "3_TASKM2212142_IMR_Drop_SP_PLComparisonDetailAccountSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.7\" & "4_TASKM2212142_IMR_SP_PLComparisonDetailAccountSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.7\" & "5_TASKM2212142_IMR_Drop_SP_PLComparisonSubSubAccountSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.7\" & "6_TASKM2212142_IMR_SP_PLComparisonSubSubAccountSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.7\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.7\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2188 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.8\" & "1_TASKM2412141_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.8\" & "2_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.8\" & "3_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2189 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 15
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "1_TASKM2912141_IMR_Drop_SP_EmpAttendanceDetailByIn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "2_TASKM2912141_IMR_SP_EmpAttendanceDetailByIn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "3_TASKM2912142_IMR_Drop_SP_PrintAddressEnvelopByLeads.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "4_TASKM2912142_IMR_SP_PrintAddressEnvelopByLeads.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "5_TAKSM3012141_IMR_AddConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "6_TASKM3012142_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "7_TASKM3012142_IMR_RenameColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "8_TASKM3012143_IMR_Drop_SP_PurchaseInvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "9_TASKM3012143_IMR_SP_PurchaseInvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "10_TASKM3012143_IMR_Drop_SP_InvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "11_TASKM3012143_IMR_AP_InvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "12_TASKM3012144_IMR_Drop_SP_Store_Issuence_Summary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "13_TASKM3012144_IMR_SP_Store_Issuence_Summary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "14_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.8.9\" & "15_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2190 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.9.0\" & "1_TASKM3112141_IMR_Drop_SP_Quotation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.9.0\" & "2_TASKM3112141_IMR_SP_Quotation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.9.0\" & "3_TASKM3112142_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.9.0\" & "4_TASKM21151_IMR_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.9.0\" & "5_TASKM21151_IMR_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.9.0\" & "6_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.9.0\" & "7_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2191 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.9.1\" & "1_TASKM51151_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.9.1\" & "2_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.1.9.1\" & "3_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2200 Then
                ProgBarOrverAllMaxVal += (1 + 9)
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.0.0\" & "1_TASKM91151_IMR_RenameTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.0.0\" & "2_TASKM91151_IMR_ADDTablesZKProj.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.0.0\" & "3_TASKM101151_IMR_ADDColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.0.0\" & "4_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.0.0\" & "5_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2210 Then
                ProgBarOrverAllMaxVal += (1 + 10)
                ProgBarVal = 17
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "1_TASKM221151_IMR_Drop_ArticleHistory_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "2_TASKM221151_IMR_ArticleHistory_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "3_TASKM221151_IMR_Drop_ArticleHistory_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "4_TASKM221151_IMR_ArticleHistory_BySize.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "5_TASKM221151_IMR_Drop_SP_StockSummaryByDate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "6_TASKM221151_IMR_SP_StockSummaryByDate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "7_TASKM221151_IMR_Drop_Sp_Store_Issuence_Summary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "8_TASKM221151_IMR_SP_Store_Issuence_Summary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "9_TASKM221151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "10_TSK231151_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "11_TASKM231151_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "12_TASKM261151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "13_TASKM261151_IMR_Drop_View_ArticleDefView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "14_TASKM261151_IMR_Alter_ArticleDefView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "15_TASKM261151_IMR_ADDTAGIDCOLUMN.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "16_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.0\" & "17_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2211 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.1\" & "1_TASKM281151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.1\" & "2_TASKM281151_IMR_AlterTable_tblVoucherDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.1\" & "3_TASKM291151_IMR_Drop_View_vwCOADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.1\" & "4_TASKM291151_IMR_AlterView_vwCOADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.1\" & "5_TASKM291151_IMR_AddPrimaryKeys.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.1\" & "6_TASKM291151_IMR_AddColumn_DepartmentId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.1\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.1\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If


            If CurrentVersion < 2212 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 17
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "1_TASKM311151_IMR_Drop_SP_InvoiceList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "2_TASKM311151_IMR_SP_InvoiceList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "3_TASKM31151_IMR_Drop_SP_SalesOfInvoiceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "4_TASKM311151_IMR_SP_SalesOfInvoiceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "5_TASKM22151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "6_TASKM42151_IMR_Drop_vw_Expenses.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "7_TASKM42151_IMR_vw_Expenses.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "8_TASKM72151_IMR_Drop_SP_DeliveryChalanDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "9_TASKM42151_IMR_SP_DeliveryChalanDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "10_TASKM62151_IMR_AddColumnsSpecialDayBreakTime.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "11_TASKM62151_IMR_Drop_SP_Financial_Year_Closing.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "12_TASKM_62151_IMR_SP_Financial_Year_Closing.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "13_TASKM62151_IMR_Drop_SP_CashRecoveryReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "14_TASKM62151_IMR_SP_CashRecoveryReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "15_TASKM62151_IMR_AddTableReceivedChequeAdjustment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "16_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.2\" & "17_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2213 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.3\" & "1_TASKM92151_IMR_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.3\" & "2_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.3\" & "3_Update_DB_Version.sql")
                ProgBar()
            End If


            If CurrentVersion < 2214 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.4\" & "1_TASKM112151_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.4\" & "2_TASKM112151_IMR_AddColumnsCostPrice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.4\" & "3_TASKM112151_IMR_Drop_View_Article.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.4\" & "4_TASKM112151_IMR_Alter_View_Article.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.4\" & "5_TASKM122151_IMR_AddColumn_CostPrice_PurchaseReturnDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.4\" & "6_TASKM142151_I MR_Drop_SP_Cheque_Recovery.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.4\" & "7_TASKM142151_IMR_SP_Cheque_Recory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.4\" & "8_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.4\" & "9_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2215 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.5\" & "1_TSK192151_IMR_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.5\" & "2_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.5\" & "3_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2216 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.6\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.6\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2217 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.7\" & "1_TASKM92151_IMR_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.7\" & "2_TASKM212151_IMR_Drop_SP_ArticleHistory_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.7\" & "3_TASKM212151_IMR_ArticleHistory_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.7\" & "4_TASKM212151_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.7\" & "5_TASKM212151_IMR_Drop_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.7\" & "6_TASKM212151_IMR_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.7\" & "7_TASKM212151_IMR_Drop_SP_Cheque_Recovery.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.7\" & "8_TASKM212151_IMR_SP_Cheque_Recovery.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.7\" & "9_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.7\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2218 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.8\" & "1_TASKM242151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.8\" & "2_TSK252151_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.8\" & "3_TASKM252151_IMR_AddFormSecurityImport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.8\" & "4_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.8\" & "5_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2219 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.9\" & "1_TASKM33151_IMR_Drop_SP_ImportDocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.9\" & "2_TASKM33151_IMR_SP_ImportDocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.9\" & "3_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.1.9\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If


            If CurrentVersion < 2220 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.0\" & "1_TASKM34151_IMR_SP_Drop_WarrantyClaimDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.0\" & "2_TASKM34151_IMR_SP_WarrantyClaimDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.0\" & "3_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.0\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2221 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.1\" & "1_TASKM35151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.1\" & "2_TASKM35151_IMR_Drop_SP_Tasks.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.1\" & "3_TASKM35151_IMR_SP_Tasks.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.1\" & "4_TASK63151_IMR_AddConfiguration.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.1\" & "5_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.1\" & "6_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2222 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.2\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.2\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2223 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.3\" & "1_TASKM311151_IMR_Drop_SP_PriceHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.3\" & "2_TASKM311151_IMR_SP_PriceHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.3\" & "3_TASKM311151_IMR_AddSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.3\" & "4_TASKM123151_IMR_Drop_SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.3\" & "5_TASKM123151_IMR_SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.3\" & "6_TASKM312151_IMR_Drop_SP_Rpt_Trial.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.3\" & "7_TASKM123151_IMR_SP_Rpt_Trial.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.3\" & "8_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.3\" & "9_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2224 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.4\" & "1_TASKM133151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.4\" & "2_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.4\" & "3_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2225 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.5\" & "1_TASKM143151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.5\" & "2_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.5\" & "3_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2226 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.6\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.6\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2227 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.7\" & "1_TASKM183151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.7\" & "2_TASKM183151_IMR_Drop_SP_LCDocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.7\" & "3_TASKM183151_IMR_SP_LCDocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.7\" & "4_TASKSM_318151_IMR_Drop_SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.7\" & "5_TASKM318151_IMR_SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.7\" & "6_TASKM193151_IMR_AddConfigs.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.7\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.7\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2228 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.8\" & "1_TASKM243151_IMR_Drop_SP_LocationWiseClosingStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.8\" & "2_TASKM243151_IMR_SP_LocationWiseStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.8\" & "3_TASKM243151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.8\" & "4_TASKSM243151_IMR_Drop_SP_Cheque_Recovery.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.8\" & "5_TASKM243151_IMR_SP_Cheque_Recovery.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.8\" & "6_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.8\" & "7_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2229 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.9\" & "1_TSK253151_IMR_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.9\" & "2_TASKM253151_IMR_Drop_SP_ArticleHistory_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.9\" & "3_TASKM253151_IMR_SP_ArticleHistoryByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.9\" & "4_TASKM253151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.9\" & "5_TASKM253151_IMR_AddColumn_Task_No.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.9\" & "6_TASKM253151_IMR_AddSMSConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.9\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.2.9\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2230 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 7
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.0\" & "1_TASKM263151_IMR_AddColumn_Bank_Ac_Name.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.0\" & "2_TASKM263151_IMR_Drop_SP_SalarySheetDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.0\" & "3_TASKM263151_IMR_SP_SalarySheetDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.0\" & "4_TASKM263151_IMR_Drop_SP_SalesCertificateLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.0\" & "5_TASKM263151_IMR_SP_SalesCertificateLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.0\" & "6_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.0\" & "7_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2231 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.1\" & "1_TASKM283151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.1\" & "2_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.1\" & "3_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2232 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 24
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "1_TASKM41151_IMR_AddSMSConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "2_TASKM303151_IMR_AlterColumntblAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "3_TASKM303151_IMR_Drop_SP_EmpAttendanceDetailByIn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "4_TASKM303151_IMR_SP_EmpAttendanceDetailByIn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "5_TASKM313151_IMR_AddTable_tblTempAttendanceDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "6_TASK42151_IMR_AddColumnCostCenter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "6_TASKM313151_IMR_Drop_SP_EmployeeDailyAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "7_TASKM313151_IMR_SP_EmployeeDailyAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "8_TASKM313151_IMR_Drop_View_Employees.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "9_TASKM313151_IMR_View_Employee.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "10_TASKM313151_IMR_Drop_Func_InStrIDs.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "11_TASKM313151_IMR_Func_InStrIDs.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "12_TASKM313151_IMR_Drop_SP_CashDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "13_TASKM313151_IMR_SP_CashDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "14_TASKM41151_IMR_Drop_SP_InvoicesLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "15_TASKM41151_IMR_SP_InvoicesLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "16_TASKM42151_IMR_Drop_SP_AdvanceReceipts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "17_TASKM42151_IMR_SP_AdvanceReceipts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "18_TASKM42151_IMR_Drop_SP_AdvancePayments.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "19_TASKM42151_IMR_SP_AdvancePayments.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "20_TASKM42151_IMR_Drop_SP_ProjectBasedTransactionsDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "21_TASKM42151_IMR_SP_ProjectBasedTransactionDetail.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "22_TASKM42151_IMR_AlterColumnAttendances.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "23_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.2\" & "24_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2233 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 14
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.3\" & "1_TASKM43151_IMR_AddColumnSMSConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.3\" & "2_TASKM43151_IMR_AddConfigExpenseSMS.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.3\" & "3_TASKM44151_IMR_AddConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.3\" & "4_TASKM46151_IMR_Drop_EmployeesView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.3\" & "5_TASKM46151_IMR_View_Employee.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.3\" & "6_TASKM46151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.3\" & "7_TASKM46151_IMR_AddConfigs.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.3\" & "8_TASKM46151_IMR_AddColumnDeptAccountHeadId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.3\" & "9_TASKM47151_IMR_Drop_GetSODetails.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.3\" & "10_TASKM47151_IMR_GetSODetails.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.3\" & "11_TASKM47151_IMR_Drop_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.3\" & "12_TASKM47151_IMR_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.3\" & "13_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.3\" & "14_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2234 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.4\" & "1_TASKM49151_IMR_Drop_SP_CashDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.4\" & "2_TASKM49151_IMR_SP_CashDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.4\" & "3_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.4\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2235 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.5\" & "1_TASKM114151_IMR_AddColumnAccount_Id.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.5\" & "2_TASKM411151_IMR_Drop_SP_PriceHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.5\" & "3_TASKM411151_IMR_SP_PriceHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.5\" & "4_TASKM411151_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.5\" & "5_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.5\" & "6_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2236 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.6\" & "1_TASKM413151_Drop_SP_PriceHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.6\" & "2_TASKM413151_IMR_SP_PriceHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.6\" & "3_TASKM413151_IMR_AddConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.6\" & "4_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.6\" & "5_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2237 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 13
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.7\" & "1_TASKM416151_IMR_AddSecurityContactList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.7\" & "2_TASKM417151_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.7\" & "3_TASKM417151_IMR_Drop_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.7\" & "4_TASKM4171151_IMR_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.7\" & "5_TASKM417151_IMR_Drop_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.7\" & "6_TASKM417151_IMR_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.7\" & "7_TASKM417151_IMR_AddColumnInvoiceType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.7\" & "8_TASKM420151_IMR_Drop_SP_PriceHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.7\" & "9_TASKM420151_IMR_SP_PriceHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.7\" & "10_TASKM420151_IMR_Drop_SP_DispatchStatus.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.7\" & "11_TASKM420151_IMR_SP_DispatchStatus.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.7\" & "12_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.7\" & "13_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2238 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 16
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "1_TASKM214151_IMR_AddImportRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "2_TASKM421151_IMR_Drop_SP_WarrantyClaim.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "3_TASKM421151_IMR_SP_WarrantyClaim.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "4_TASKM422151_IMR_Drop_SP_Quotation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "5_TASKM421151_IMR_SP_Quotation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "6_TASKM422151_IMR_Drop_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "7_TASKM422151_IMR_SP_Rpt_Receivables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "8_TASKM422151_IMR_Drop_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "9_TASKM422151_IMR_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "10_TASKM422151_IMR_AddColumnsLCMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "11_TASKM423151_IMR_Drop_SP_ImportDocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "12_TASKM422151_IMR_SP_ImportDocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "13_TASKM422151_IMR_Drop_GetSODetails.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "14_TASKM422151_IMR_GetSODetails.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "15_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.3.8\" & "16_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2240 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.4.0\" & "1_TASKM424151_IMR_Drop_SP_Rpt_Ledger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.4.0\" & "2_TASKM424151_IMR_SP_Rpt_Ledger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.4.0\" & "3_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.4.0\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2241 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.4.1\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.4.1\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2242 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.4.2\" & "1_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.4.2\" & "2_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2250 Then
                ProgBarOrverAllMaxVal += 8
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.5.0\" & "1_TASKM428151_IMR_Drop_SP_Cheque_Recovery.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.5.0\" & "2_TASKM274151_IMR_SP_Cheque_Recovery.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.5.0\" & "3_TASKM428151_IMR_AddColumn_Mobile_No.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.5.0\" & "4_TASKM428151_IMR_AddConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.5.0\" & "5_TASKM428151_IMR_Drop_SP_Financial_Year_Closing.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.5.0\" & "6_TASKM428151_IMR_SP_Financial_Year_Closing.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.5.0\" & "7_TASKM429151_IMR_AddTable_DatabaseBackup.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.5.0\" & "8_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.5.0\" & "9_Update_DB_Version.sql")
                ProgBar()
            End If
            If CurrentVersion < 2260 Then
                ProgBarOrverAllMaxVal += 10
                ProgBarVal = 13
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.6.0\" & "1_TASKM5420152_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.6.0\" & "2_TASKM54152_IMR_AddDuplicateIndex.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.6.0\" & "3_TASKM54153_IMR_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.6.0\" & "4_TASKM_55151_IMR_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.6.0\" & "5_TASKM55151_IMR_AddConfigs.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.6.0\" & "6_TASKM55151_IMR_AddColumnFullName_tblUser.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.6.0\" & "7_TASKM55151_IMR_AddTable_AdvanceRequestTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.6.0\" & "8_TASKM55151_IMR_AddTable_tblLeaveDeductions.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.6.0\" & "9_TASKM65151_IMR_AddTable_DeductionsDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.6.0\" & "10_TASKM65151_IMR_Drop_View_EmployeesView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.6.0\" & "11_TAKSM65151_IMR_View_EmployeesView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.6.0\" & "12_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.6.0\" & "13_Update_DB_Version.sql")

                ProgBar()
            End If
            If CurrentVersion < 2270 Then
                ProgBarOrverAllMaxVal += 10
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.7.0\" & "1_Ali_AddColumn_Apprved.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.7.0\" & "1_TASKM512151_IMR_AddColumn_POID.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.7.0\" & "2_TASKM_135151_IMR_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.7.0\" & "3_TASKM145151_IMR_Drop_View_EmployeesView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.7.0\" & "4_TASKM145151_IMR_View_EmployeesView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.7.0\" & "5_TASKM515151_IMR_Drop_SP_SalesCertificateLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.7.0\" & "6_TASKM515151_IMR_SP_SalesCertificateLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.7.0\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.7.0\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2280 Then
                ProgBarOrverAllMaxVal += 10
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.8.0\" & "1_TASKM185151_IMR_AlterLength_Article.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.8.0\" & "2_TASKM195151_IMR_AddColumn_Config.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.8.0\" & "3_TASKM195151_IMR_UpdateAllowMinusStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.8.0\" & "4_TASKM195151_IMR_AddColumnsTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.8.0\" & "5_Ali_Add _Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.8.0\" & "6_Ali_Add_Identity.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.8.0\" & "6_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.8.0\" & "7_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2290 Then
                ProgBarOrverAllMaxVal += 10
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.9.0\" & "1_TASKM2851511_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.9.0\" & "2_TASKM61151_IMR_AddTable_tblProjectVisitType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.9.0\" & "3_TSK62151_IMR_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.9.0\" & "4_TASKM62151_IMR_AddColumnsProjectPortFolio.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.9.0\" & "5_TASKM62151_IMR_Drop_EmployeesView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.9.0\" & "6_TASKM62151_IMR_EmployeesView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.9.0\" & "7_TASK_6-2-15_AHMAD_ADDTABLE.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.9.0\" & "8_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.2.9.0\" & "9_Update_DB_Version.sql")
                ProgBar()
            End If



            If CurrentVersion < 2300 Then
                ProgBarOrverAllMaxVal += 10
                ProgBarVal = 11
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.0.0\" & "1_TASKM_66151_IMR_Drop_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.0.0\" & "2_TASKM66151_IMR_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.0.0\" & "3_TASKM66151_IMR_Drop_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.0.0\" & "4_TASKM66151_IMR_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.0.0\" & "5_TASKM66151_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.0.0\" & "6_AhmadSharif_AddCol.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.0.0\" & "7_AhmadSharif_AddCol.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.0.0\" & "8_AhmadSharif_AddCol.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.0.0\" & "9_TASKM66151_IMR_UpdateEmployeeData.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.0.0\" & "9_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.0.0\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2310 Then
                ProgBarOrverAllMaxVal += 10
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.1.0\" & "1_AhmadSharif_AddTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.1.0\" & "2_TAKSM76151_IMR_ADDTABLE.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.1.0\" & "3_TASK76151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.1.0\" & "4_TASKM96151_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.1.0\" & "5_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.1.0\" & "6_Update_DB_Version.sql")
                ProgBar()
            End If


            If CurrentVersion < 2320 Then
                ProgBarOrverAllMaxVal += 10
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.2.0\" & "1_TASKM106151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.2.0\" & "2_TASKM116151_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.2.0\" & "3_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.2.0\" & "4_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2330 Then
                ProgBarOrverAllMaxVal += 10
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.3.0\" & "1_TASKM126151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.3.0\" & "2_AhmadSharif_tblLoanPolicy_AddTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.3.0\" & "3_AhmadSharif_tblLoanReasons_AddTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.3.0\" & "4_AhmadSharif_AddDefaultRecords.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.3.0\" & "5_AhmadSharif_AddDefaultRecords.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.3.0\" & "6_AhmadSharif_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.3.0\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.3.3.0\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2400 Then
                ProgBarOrverAllMaxVal += 70
                ProgBarVal = 20
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "1_TASKM156151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "2_TASKM156151_IMR_AddColum.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "3_AhmadSharif_AddTable_tblStockDispatchStatus.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "4_AhmadSharif_tblStockDispatchStatus_AddDefaultRecords.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "5_AhmadSharif_AddCol_PurchaseOrderMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "6_TASKM176151_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "7_AhmadSharif_AddTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "8_TASKM176152_IMR_Drop_SP_QuotationItemSpecification.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "9_TASKM176152_IMR_SP_QuotationItemSpecification.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "10_AliAnsari_AddTableEmployeeType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "11_AliAnsari_AddDefaultValues_EmployeeType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "12_AliAnsari_DropProcedure_Sp_EmployeePromotionIncrement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "13_CreateSP_AliAnsari_Sp_EmployeePromotionIncrement.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "14_AliAnsari_AddColumns_DefEmployee.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "15_TAKSM196151_IMR_AddColumnAccountId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "16_TASKM196151_IMR_UpdateAccountId_StockMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "16_1_TAKSM196151_IMR_Drop_SP_ProjectHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "16_1_TASKM196151_IMR_SP_ProjectHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "17_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.0\" & "18_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2401 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 15
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "1_TASKM206151_IMR_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "2_TASKM206152_IMR_Drop_SP_QuotationItemSpecification.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "3_TASKM206152_IMR_SP_QuotationItemSpecification.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "4_TASKM206153_IMR_Drop_SP_ProjectHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "5_TASKM206153_IMR_SP_ProjectHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "6_TASKM206154_IMR_Drop_SP_ProjectVisitDetail1.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "7_TASKM206154_IMR_SP_ProjectVisitDetail1.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "8_TASKM206154_IMR_Drop_SP_ProjectVisitDetail2.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "9_TASKM206154_IMR_SP_ProjectVisitDetail2.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "10_TASKM206154_IMR_Drop_SP_ProjectVisitDetail3.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "11_TASKM206154_IMR_SP_ProjectVisitDetail3.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "12_TASKM206154_IMR_Drop_SP_ProjectVisitDetail4.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "13_TASKM206154_IMR_SP_ProjectVisitDetail4.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "14_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.1\" & "15_Update_DB_Version.sql")
                ProgBar()
            End If


            If CurrentVersion < 2402 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 28
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "1_TASKM226151_IMR_UpdateDispatchStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "2_Ali.Ansari_AddCoulumns_TblDefTasks.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "3_0_AhmadSharif_Drop_SP_ProjectVisitComments.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "3_AhmadSharif_AddSP_ProjectVisitComments.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "4_TASKM246151_IMR_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "5_0_AhmadSharif_DropSP_StockAdjustment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "5_1_AhmadSharif_AddSP_StockAdjustment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "6_AhmadSharif_DropSP.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "7_AhmadSharif_AddSP_ProjectVisitDetail1.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "8_AhmadSharif_AddSP_ProjectVisitDetail2.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "9_AhmadSharif_AddSP_ProjectVisitDetail3.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "10_AhmadSharif_AddSP_ProjectVisitDetail4.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "11_AhmadSharif_AddCol_tblDateLock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "12_AhmadSharif_DropSP_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "13_AhmadSharif_AddSP_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "14_AhmadSharif_DropSP_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "15_AhmadSharif_AddSP_Rpt_Receiveable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "16_TASKM66151_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "17_TASKM276151_IMR_AddColumnCostPricetblCostSheet.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "18_TASKM276151_IMR_Drop_SP_QuotationItemSpecification.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "19_TASKM276151_IMR_SP_QuotationItemSpecification.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "20_Ali_Drop_Sp_Production.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "21_Ali_Create_Sp_Production.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "22_TASKM296151_IMR_Drop_SP_ProductionStore.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "23_TASKM296151_IMR_SP_ProductionStore.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "23_0_TASKM306151_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "24_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.2\" & "25_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2403 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 12
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.3\" & "1_TASKM306151_IMR_Drop_SP_QuotationItemSpecification.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.3\" & "2_TASKM306151_IMR_SP_QuotationItemSpecification.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.3\" & "3_Ali_Ansari_Drop_SP_Tasks.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.3\" & "4_Ali_Ansari_Add_SP_Tasks.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.3\" & "5_TASKM71151_IMR_AddSecurityAdvanceReceipt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.3\" & "6_TASKM27151_IMR_Drop_EmpBalances.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.3\" & "7_TASKM27151_IMR_SP_EmpBalances.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.3\" & "8_TASKM37151_IMR_Drop_DeliveryChalanItemSpecification.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.3\" & "9_TASKM37151_IMR_SP_DeliveryChalanItemSpecification.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.3\" & "10_TASKM47151_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.3\" & "11_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.3\" & "12_Update_DB_Version.sql")
                ProgBar()
            End If



            If CurrentVersion < 2404 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 24
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "1_Task201507004_Ali_AddColumns_SalaryType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "2_Task201507004_Ali_AddTable_TblHolidaySetup.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "3_Ali_AddSecurityRights_FrmHolidaySetup.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "4_Ali_AddConfigration.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "5_TASKM67151_IMR_AddRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "5_Ali_DropView_Vw_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "6_Ali_Create_Vw_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "7_TASKM77151_IMR_AddConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "8_TASKM77152_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "9_TASKM77152_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "10_AhmadSharif_AddCol_SalesMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "10_TASKM77152_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "11_AhmadSharif_AddTable_tblSalesTaxInvoiceStatus.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "12_AhmadSharif_AddDefaultStatus_In_tblSalesTaxInvoiceStatus.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "13_Ali_AddSecurity_FrmVoucherPost.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "14_TASKM78151_IMR_AddTableCustomerBasedDiscountFlat.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "15_AhmadSharif_DropSp_SP_SalesRegisterHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "16_AhmadSharif_AddSP_SP_SalesRegisterHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "17_Ali_AddColumnTblVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "18_Ch-Ahmad_AddSecurity_frmGrdRptOrdersDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "19_TASKM78152_IMR_Drop_SP_CustomerContributionGraphs.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "20_TASKM87152_IMR_SP_CustomerContributionGraphs.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "21_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.4\" & "22_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2405 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.5\" & "1_TASKM97151_IMR_CompanyContacts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.5\" & "2_TASKM97151_IMR_Drop_SP_ContactList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.5\" & "3_TASKM79151_IMR_SP_ContactList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.5\" & "4_AhmadSharif_DropSP_SP_CompanyContacts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.5\" & "5_AhmadSharif_AddSP_SP_CompanyContacts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.5\" & "6_AhmadSharif_AddCol_tblUser.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.5\" & "7_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.5\" & "8_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2406 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.6\" & "1_AhmadSharif_AddCol_tblCompanyContacts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.6\" & "2_AhmadSharif_DropSP_SP_CompanyContacts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.6\" & "3_AhmadSharif_AddSP_CompnayContacts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.6\" & "4_AhmadSharif_AddSecurityRights_CompanyContacts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.6\" & "5_DropSP_PV1_Ali.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.6\" & "6_CreateSP_PV1_Ali.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.6\" & "7_DropSP_PV2_Ali.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.6\" & "8_CreateSP_PV2_Ali.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.6\" & "9_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.6\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2407 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.7\" & "1_AhmadSharif_AddTable_tblPrefix.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.7\" & "2_AhmadSharif_AddCol_tblCompanyContacts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.7\" & "2_Ali_Ansari_DropSp_Sp__ProjectVisitDetail4.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.7\" & "3_Ali_Ansari_AddSp_Sp__ProjectVisitDetail4.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.7\" & "5_AhmadSharif_AddCol_tblCompanyContacts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.7\" & "6_AhmadSharif_DropSP_SP_CompanyContacts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.7\" & "7_AhmadSharif_AddSP_SP_CompanyContacts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.7\" & "8_AhmadSharif_AddSecurityRights_Prefix.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.7\" & "9_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.7\" & "10_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2408 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 15
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "1_TASKM227151_IMR_AddTableDateLockPermission.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "2_TSK725151_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "3_AliAnsari_AddConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "4_TASKM287151_IMR_Drop_View_vwCOADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "5_TASKM287151_IMR_VwCoaDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "6_TASKM287151_IMR_Drop_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "7_TASKM287151_IMR_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "8_TASKM287151_IMR_Drop_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "9_TASKM287151_IMR_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "10_AhmadSharif_AddCol_SalesMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "11_AhmadSharif_UpdateCol_EmailStatus_SalesMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "12_AhmadSharif_AddConfig_configvaluestable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "13_TASKM297151_IMR_AddColumn_PO_ID_ReceivingNoteDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "14_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.8\" & "15_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2409 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.9\" & "1_TASKM287151_IMR_Drop_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.9\" & "2_TASKM287151_IMR_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.9\" & "3_TASKM287151_IMR_Drop_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.9\" & "4_TASKM287151_IMR_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.9\" & "5_TASKM307151_IMR_AddColumn_RefReceivingId_ProductionProcess.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.9\" & "6_TASKM18151_IMR_Drop_SP_VehicleLog.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.9\" & "7_TASKM18151_IMR_SP_VehicleLog.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.9\" & "8_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.0.9\" & "9_Update_DB_Version.sql")
                ProgBar()
            End If


            If CurrentVersion < 2410 Then
                ProgBarOrverAllMaxVal += 1
                ProgBarVal = 52
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "1_AhmadSharif_AddTable_EmailAlertAttenadance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "1_TASKM206151_IMR_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "2_AhmadSharif_AddTable_MasterEmailAlertAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "3_AhmadSharif_ConfigValue_AttendanceEmailAlert.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "4_AliAnsari_DropView_Vw_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "5_AliAnsari_CreateView_Vw_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "6_AhmadSharif_ConfigValue_CompanyWisePrefixOnVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "7_AliAnsari_Drop_SP_CashRecoveryReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "8_AliAnsari_Create_SP_CashRecoveryReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "9_AhmadSharif_AddCol_itemWiseDiscount_ReceivingDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "10__Ali_AddColumns_SalaryType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "10_AhmadSharif_AddCol_PurchaseOrderDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "11__Ali_AddTable_TblDefTaxSlab.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "11_TASKM78151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "12__Ali_AddSecurityRightsCRM.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "12_AhmadSharif_AddCols_QuotationMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "13_AhmadSharif_DropSP_Quotation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "14_AhmadSharif_AddSP_SP_Quotation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "15_TASKM88151_IMR_Drop_SP_ProductionStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "16_TASKM88151_IMR_SP_ProductionStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "17_AhmadSharif_AddConfigValues_DeliveryChallanByEngineNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "17_TASKM88151_IMR_Drop_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "18_TASKM88151_IMR_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "19_TASKM88151_IMR_Drop_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "20_TASKM88151_IMR_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "21_Drop_SpProductionInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "22_Create_SpProductionnvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "23_Ali_AddColumn_UserId_CRMForms.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "24_Ali_Drop_Sp_ChequeRecovery.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "24_Drop_Ali_Ansari_Sp_ProjectVisitDetail1.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "25_Ali_Create_Sp_ChequeRecovery.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "25_Drop_Ali_Ansari_Sp_ProjectVisitDetail2.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "26_Drop_Ali_Ansari_Sp_ProjectVisitDetail3.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "26_TASKM108151_IMR_Drop_SP_SummaryAgainstCMFA.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "27_Drop_Ali_Ansari_Sp_ProjectVisitDetail4.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "27_TASKM108151_IMR_SP_SummaryAgainstCMFA.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "28_AhmadSharif_AddCol_ReceivingNoteDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "28_Create_Ali_Ansari_Sp_ProjectVisitDetail1.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "29_Create_Ali_Ansari_Sp_ProjectVisitDetail2.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "29_TASKM138151_IMR_AddtblDefInwardAccounts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "30_Create_Ali_Ansari_Sp_ProjectVisitDetail3.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "31_Create_Ali_Ansari_Sp_ProjectVisitDetail4.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "32_Ali_AlterTable_TblEmployeeType_AddColumn_LeavesAlloted.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "33_Ali_DropView_VwEmployees.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "34_Ali_CreateView_VwEmployees.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "35_TASKM158151_IMR_AddColumn_EntryDate_tblLeads.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "36_Ali_AlterTable_TblAttendanceDetail_AddColumn_PolicyId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "37_Drop_Ali_Ansari_Sp_CashRecoveryReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "38_Create_Ali_Ansari_Sp_CashRecoveryReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "38_1_TASKM816151_IMR_AddColumn_AutoAdjustAbsentAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "39_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.4.1.0\" & "40_Update_DB_Version.sql")


                ProgBar()
            End If




            If CurrentVersion < 2500 Then
                ProgBarOrverAllMaxVal += 90
                ProgBarVal = 46
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "1_AhmadSharif_AddTable_EmployeeNoOfVisit.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "2_AhmadSharif_AddTable_EmployeeVisitCharges.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "3_AhmadSharif_AddCol_tblDefEmployee.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "4_TASKM198151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "5_AhmadSharif_AddCol_QuotationDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "6_TSKM208151_IMR_ConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "7_TASKM218151_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "8_TASKM248151_IMR_AddTablesServices.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "9_TASKM248151_IMR_Drop_SP_ServicesStockLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "10_TASKM248151_IMR_SP_ServicesStockLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "11_TASKM248151_IMR_Drop_SP_InwardGatePassDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "12_TASKM248151_IMR_SP_InwardGatePassDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "13_TASKM248151_IMR_Drop_SP_WIPDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "14_TASKM248151_IMR_SP_WIPDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "15_TASKM248151_IMR_Drop_SP_ServicesProductionDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "16_TASKM248151_IMR_SP_ServicesProductionDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "17_TASKM248151_IMR_Drop_SP_ServicesDispatchDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "18_TASKM248151_IMR_SP_ServicesDispatchDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "19_TASKM248151_IMR_Drop_SP_ServicesInvoiceDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "20_TASKM248151_IMR_SP_ServicesInvoiceDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "21_AhmadSharif_AddCol_DeliveryChalanDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "22_TASKM258151_IMR_Drop_SP_Rpt_Receivables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "23_TASKM258151_IMR_SP_Rpt_Receivables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "24_TASKM258151_IMR_Drop_SP_Rpt_Payable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "25_TASKM258151_IMR_SP_Rpt_Payables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "26_TASKM268151_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "27_TASKM268151_IMR_AddColumnsUpdateUserName.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "28_TASKM268151_IMR_Drop_SP_ServicesInvoiceDetailById.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "29_TASKM268151_IMR_SP_ServicesInvoiceDetailById.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "30_TASKM268151_IMR_Drop_SP_ServicesDispatchDetailById.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "31_TASKM268151_IMR_SP_ServicesDispatchDetailById.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "32_TASKM268151_IMR_Drop_SP_ServicesProductionDetailById.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "33_TASKM268151_IMR_SP_ServicesProductionDetailById.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "34_TASKM268151_IMR_Drop_SP_WIPDetailById.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "35_TASKM268151_IMR_SP_WIPDetailById.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "36_TASKM268151_IMR_Drop_SP_InwardGatepassDetailById.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "37_TASKM268151_IMR_SP_InwardGatepassDetailById.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "38_AhmadSharif_AddCol_DispatchDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "39_AhmadSharif_AddCol_ReturnDispatchDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "40_TASKM298151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "41_TASKM19151_IMR_Add_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "42_TASKM59151_IMR_AddTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "43_TASKM59151_IMR_Drop_SP_Stock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "44_TASKM59151_IMR_SP_Stock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "45_Update_Release_Log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.0\" & "46_Update_DB_Version.sql")
                ProgBar()
            End If

            If CurrentVersion < 2501 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.0.1")
                ProgBar()
            End If

            If CurrentVersion < 2502 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.0.2")
                ProgBar()
            End If

            If CurrentVersion < 2503 Then
                ProgBarOrverAllMaxVal += 10
                ProgBarVal = 10
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.3\" & "1_TASKM88151_IMR_Drop_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.3\" & "2_TASKM88151_IMR_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.3\" & "3_Ahmad_AddCols_AdvanceRequestTable.sql")
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.0.3")
                ProgBar()
            End If

            If CurrentVersion < 2504 Then
                ProgBarOrverAllMaxVal += 5
                ProgBarVal = 9

                'Copy Update exe
                If IO.File.Exists(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.4\SIRIUS ERP Updater.exe") Then

                    Dim ProcessRunning1() As Process = Process.GetProcessesByName("SIRIUS ERP Updater")
                    If Not ProcessRunning1.Length = 0 Then
                        ProcessRunning1(0).Kill()
                        System.IO.File.Copy(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.4\SIRIUS ERP Updater.exe", str_ApplicationStartUpPath & "\SIRIUS ERP Updater.exe", True)
                        Process.Start(Application.StartupPath & "\SIRIUS ERP Updater.exe")
                    Else
                        System.IO.File.Copy(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.4\SIRIUS ERP Updater.exe", str_ApplicationStartUpPath & "\SIRIUS ERP Updater.exe", True)

                    End If
                    If File.Exists(str_ApplicationStartUpPath & "\AutoUpdate.xml") Then System.IO.File.Delete(str_ApplicationStartUpPath & "\AutoUpdate.xml")
                End If

                'End Copy update exe

                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.4\" & "1_TASKM914151_IMR_Drop_SP_VoucherDetailList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.4\" & "2_TASKM914151_IMR_SP_VoucherDetailList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.4\" & "3_TASKM914151_IMR_Drop_SP_AdjustmentBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.4\" & "4_TASKM914151_IMR_SP_AdjustmentBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.4\" & "5_TASKM169151_IMR_AddColumn_Value_In.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.4\" & "6_TASKM916151_IMR_Drop_Vw_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.4\" & "7_TASKM916151_IMR_Add_Vw_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.4\" & "8_TASKM916153_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.4\" & "9_AhmadS_AddConfigValues.sql")

                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.0.4")
                ProgBar()
            End If


            If CurrentVersion < 2505 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.0.5")
                ProgBar()
            End If



            If CurrentVersion < 2506 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.6\" & "1_TASKM189151_IMR_Drop_SP_AdjustmentBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.6\" & "2_TASKM189151_IMR_SP_AdjustmentBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.6\" & "3_TASKM189152_IMR_AddColumns.sql")
                UpdateDbVersion("2.5.0.6")
                ProgBar()
            End If


            If CurrentVersion < 2507 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.7\" & "1_TASK22_IMR_AddColumnDocType.sql")
                UpdateDbVersion("2.5.0.7")
                ProgBar()
            End If

            If CurrentVersion < 2508 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.8\" & "1_TASKM229151_IMR_AddColumns.sql")
                UpdateDbVersion("2.5.0.8")
                ProgBar()
            End If

            If CurrentVersion < 2509 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.0.8\" & "1_TASKM229151_IMR_AddColumns.sql")
                UpdateDbVersion("2.5.0.9")
                ProgBar()
            End If

            If CurrentVersion < 2510 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.0\" & "1_TASK289151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.0\" & "2_TASKM289151_IMR_Drop_ViewEmployee.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.0\" & "3_TASKM289151_IMR_ViewEmployee.sql")
                UpdateDbVersion("2.5.1.0")
                ProgBar()
            End If


            If CurrentVersion < 2511 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.1\" & "1_TASKM299151_IMR_Drop_SP_InvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.1\" & "2_TASKM299151_IMR_SP_InvoiceAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.1\" & "3_TASKM299151_IMR_Drop_SP_PL_SingleDate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.1\" & "4_TASKM299151_IMR_SP_PL_SingleDate.sql")
                UpdateDbVersion("2.5.1.1")
                ProgBar()
            End If



            If CurrentVersion < 2512 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.2\" & "1_TASKM299151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.2\" & "2_TASKM309151_IMR_AddConfigs.sql")
                UpdateDbVersion("2.5.1.2")
                ProgBar()
            End If



            If CurrentVersion < 2513 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.3\" & "1_TASKM110151_IMR_AddColumns.sql")
                UpdateDbVersion("2.5.1.3")
                ProgBar()
            End If


            If CurrentVersion < 2514 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.1.4")
                ProgBar()
            End If



            If CurrentVersion < 2515 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.5\" & "1_TASKM210151_IMR_Drop_vwCOADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.5\" & "2_TASKM210151_IMR_vwCOADetail.sql")
                UpdateDbVersion("2.5.1.5")
                ProgBar()
            End If

            If CurrentVersion < 2516 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.6\" & "1_TASKM310151_IMR_Drop_SP_AdjustmentBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.6\" & "2_TASKM310151_IMR_SP_AdjustmentBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.6\" & "3_TASKM310151_IMR_Drop_SP_VoucherAdjustmentList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.6\" & "4_TASKM310151_IMR_SP_VoucherAdjustmentList.sql")
                UpdateDbVersion("2.5.1.6")
                ProgBar()
            End If

            If CurrentVersion < 2517 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.1.7")
                ProgBar()
            End If


            If CurrentVersion < 2518 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.8\" & "1_TASKM710151_IMR_Drop_SaleReturmSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.8\" & "2_TASKM710151_IMR_SaleReturmSummary.sql")
                UpdateDbVersion("2.5.1.8")
                ProgBar()
            End If

            If CurrentVersion < 2519 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.9\" & "1_TASKM810151_IMR_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.9\" & "2_TASKM810151_IMR_AttendanceStatusValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.9\" & "3_TASKM810151_IMR_Drop_SP_AttendenceSetupDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.9\" & "4_TASKM810151_IMR_SP_AttendenceSetupDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.1.9\" & "5_TASKM910151_IMR_AddApprovedRights_Quotation.sql")
                UpdateDbVersion("2.5.1.9")
                ProgBar()
            End If

            If CurrentVersion < 2520 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.2.0")
                ProgBar()
            End If
            If CurrentVersion < 2521 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.2.1")
                ProgBar()
            End If
            If CurrentVersion < 2522 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 12
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.2\" & "1_TASKM1110151_IMR_Drop_SP_SalesRegisterHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.2\" & "2_TASKM1110151_IMR_SP_SalesRegisterHistory.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.2\" & "3_TASKM1110151_IMR_Drop_Cust_WelcomeLetter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.2\" & "4_TASKM1110151_IMR_Cust_WelcomeLetter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.2\" & "5_TASKM1110151_IMR_Drop_rptItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.2\" & "6_TASKM1110151_IMR_rptItemWiseSales.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.2\" & "7_TASKM1510151_IMR_Drop_SP_AdjustmentBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.2\" & "8_TASKM1110151_IMR_SP_AdjustmentBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.2\" & "9_TASKM1110151_IMR_Drop_rptItemWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.2\" & "10_TASKM1110151_IMR_rptItemWiseSalesConsolidate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.2\" & "11_TASKM1510151_IMR_Drop_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.2\" & "12_TASKM1510151_IMR_SP_InvoiceAgingFormated.sql")
                UpdateDbVersion("2.5.2.2")
                ProgBar()
            End If
            If CurrentVersion < 2523 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.3\" & "1_TASKM1910151_IMR_Drop_SP_VoucherBalanceList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.3\" & "2_TASKM1910151_IMR_SP_VoucherBalanceList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.3\" & "3_TASK1910151_IMR_Drop_SP_VoucherAdjustmentList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.3\" & "4_TASK1910151_IMR_SP_VoucherAdjustmentList.sql")
                UpdateDbVersion("2.5.2.3")
            End If

            If CurrentVersion < 2524 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.4\" & "1_TAKSM2010151_IMR_UpdateHolidaySetupRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.4\" & "2_TAKSM2110151_IMR_UpdateStoreIssuenceRights.sql")
                UpdateDbVersion("2.5.2.4")
            End If
            If CurrentVersion < 2525 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.2.5")
            End If

            If CurrentVersion < 2526 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.6\" & "1_TASKM2710151_IMR_Drop_SP_ClosingStockByOrders.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.6\" & "2_TASKM2710151_IMR_SP_ClosingStockByOrders.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.6\" & "3_TASKM2710151_IMR_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.6\" & "5_TASKM2810151_IMR_AddTables_tmptblVoucher_detail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.6\" & "6_TASKM2810151_IMR_AddConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.6\" & "7_TASKM2810151_IMR_AddTable_ArticleStatus.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.6\" & "8_TASKM2810151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.6\" & "9_TASKM2810151_IMR_Drop_ArticleDefView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.6\" & "10_TASKM2810151_IMR_ArticleDefView.sql")
                UpdateDbVersion("2.5.2.6")
            End If
            If CurrentVersion < 2527 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.7\" & "1_TASKM3010151_IMR_AddColumn_tmp_activity_log.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.7\" & "2_TASKM3010151_IMR_Drop_SP_tmpRptVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.7\" & "3_TASKM3010151_IMR_SP_tmpRptVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.7\" & "4_TASKM3010151_IMR_Drop_SP_Quotation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.7\" & "5_TASKM3010151_IMR_SP_Quotation.sql")
                UpdateDbVersion("2.5.2.7")
            End If
            If CurrentVersion < 2528 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.8\" & "1_TASKM511151_IMR_Drop_SP_ArticleHistory_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.8\" & "2_TASKM511151_IMR_SP_ArticleHistory_ByLocation.sql")
                UpdateDbVersion("2.5.2.8")
            End If
            If CurrentVersion < 2529 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.9\" & "1_TASKM611152_IMR_Drop_SP_ClosingStockByOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.9\" & "2_TASKM611152_IMR_SP_ClosingStockByOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.9\" & "3_TASKM611152_IMR_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.9\" & "4_TASKM611153_IMR_Drop_SP_RptVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.9\" & "5_TASKM611153_IMR_SP_RptVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.2.9\" & "6_TASKM911151_IMR_AddColumns.sql")
                UpdateDbVersion("2.5.2.9")
            End If

            If CurrentVersion < 2530 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.0\" & "1_TASK41_IMR_Drop_SP_InwardExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.0\" & "2_TASK41_IMR_SP_InwardExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.0\" & "3_TASK41_IMR_Drop_SP_OutwardExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.0\" & "4_TASK41_IMR_SP_OutwardExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.0\" & "5_TASKM1011151_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.0\" & "6_TASKM1011151_IMR_InwardExpenseUpdate.sql")
                UpdateDbVersion("2.5.3.0")
            End If
            If CurrentVersion < 2531 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.3.1")
            End If
            If CurrentVersion < 2532 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.2\" & "1_TASKM1211151_IMR_AddTable_tblDefOutwaredAccounts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.2\" & "2_TASKM1211151_IMR_AddConfigAdTax.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.2\" & "3_TASK51_IMR_AddColumns_AdTax_In_Purchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.2\" & "4_TASK51_IMR_Drop_SP_SummaryOfPurchaseInvoices.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.2\" & "5_TASK51_IMR_SP_SummaryOfPurchaseInvoices.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.2\" & "6_TASK51_Drop_SP_SummaryOfPurchaseReturnInvoices.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.2\" & "7_TASK51_SP_SummaryOfPurchaseReturnInvoices.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.2\" & "8_TASKM1411151_IMR_Drop_SP_RptVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.2\" & "9_TASKM1411151_IMR_SP_RptVoucher.sql")
                UpdateDbVersion("2.5.3.2")
            End If
            If CurrentVersion < 2533 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.3.3")
            End If
            If CurrentVersion < 2534 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 20
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "1_TASK_TFS_46_IMR_Drop_SP_CostPriceForProduction.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "2_TASK_TFS_46_IMR_SP_CostPriceForProduction.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "3_TASK_TFS_46_IMR_Drop_SP_CostPriceForRawMaterial.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "4_TASK_TFS_46_IMR_SP_CostPriceForRawMaterial.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "5_TASK_TFS_46_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "6_TASK_TFS-46_Drop_IMR_SP_ProduceItems.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "7_TASK_TFS-46_IMR_SP_ProduceItems.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "8_0_TASK_TFS-57_IMR_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "8_TASK_TFS-57_IMR_Drop_SP_InstallmentBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "9_TASK_TFS-57_IMR_SP_InstallmentBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "10_TASK_TFS-57_IMR_Drop_SP_InstallmentChartData.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "11_TASK_TFS-57_IMR_SP_InstallmentChartData.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "12_TASK_TFS_57_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "13_TASK_TFS_57_Drop_SP_InstallmentLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "14_TASK_TFS_57_SP_InstallmentLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "15_TASK_TFS_57_Drop_SP_InstallmentDocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "16_TASK_TFS_57_SP_InstallmentDocument.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "17_TASKM24112015_IMR_Drop_View_EmployeesView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "18_TASKM24112015_IMR_View_EmployeesView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.4\" & "19_TASK_TFS_46_AddConfiguration.sql")
                UpdateDbVersion("2.5.3.4")
            End If
            If CurrentVersion < 2535 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.3.5")
            End If
            If CurrentVersion < 2536 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 16
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "1_TASK_TFS_65_IMR_Drop_ArticleHistory_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "2_TASK_TFS_65_IMR_ArticleHistory_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "3_TASK_TFS_58_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "4_TASK_TFS_58_IMR_Drop_SP_TaxDeductionDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "5_TASK_TFS_58_IMR_SP_TaxDeductionDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "6_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "7_TASK_35_Drop_SP_ApplyAverageRateOnSalesInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "8_TASK_35_SP_ApplyAverageRateOnSalesInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "9_New Security Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "10_TASK_35_AddTableUpdateAvgRateProcess.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "11_TASK_35_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "12_TASK_Ameen_AddColumn_FromDate_AdjustmentAvgRateDetailTable-26-11-2015.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "13_TASK_Ameen_AddColumn_ToDate_AdjustmentAvgRateDetailTable-26-11-2015.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "14_TASKM3011151_IMR_AddTableSurvey.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "15_TASK_58_Drop_SalesTaxDeductionDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.6\" & "16_TASK_58_SalesTaxDeductionDetail.sql")
                UpdateDbVersion("2.5.3.6")
            End If
            If CurrentVersion < 2537 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.3.7")
            End If
            If CurrentVersion < 2538 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.8\" & "1_TASK_TFS_64_AddConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.8\" & "2_TASK_64_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.8\" & "4_TASK_TFS_73_IMR_Drop_SP_SalesCertificateLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.8\" & "5_TASK_TFS_73_IMR_SP_SalesCertificateLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.8\" & "6_TAKST_TFS_78_Ameen_frmDeliveryChalanStatus-04-12-2015.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.8\" & "7_TASK_TFS_78_Ameen_Security_frmGRNStatus-04-12-2015.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.8\" & "8_TASK_TSF_78_Update_Status_DeliveryChalan.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.3.8\" & "9_TASKM512151_IMR_AddColumns.sql")
                UpdateDbVersion("2.5.3.8")
            End If
            If CurrentVersion < 2539 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.3.9")
                ProgBar()
            End If
            If CurrentVersion < 2540 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.4.0")
                ProgBar()
            End If
            If CurrentVersion < 2541 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.1\" & "1_TASK_Ahmad_AddColumn_FromDate_AdjustmentAvgRateDetailTable-17-12-2015.sql")
                UpdateDbVersion("2.5.4.1")
                ProgBar()
            End If
            If CurrentVersion < 2542 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 17
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "1_TASK_TFS_IMR_AddTablesTaget.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "2_TASKM_1112151_IMR_AddColumnInSale.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "3_TASKM_1212151_IMR_AddColumnsCostPrice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "4_TASKM_1212151_IMR_Drop_SP_ApplyAverageRateOnSalesInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "5_TASKM_1212151_IMR_SP_ApplyAverageRateOnSalesInvoice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "6_TASKM_1412151_IMR_AddConfigValuesFreezColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "7_TASKM_1412151_IMR_AddColumnsInstallmentDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "8_TASKM_1512151_IMR_AddColumnsLetterofcredit.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "9_TASKM_1712151_IMR_AddConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "10_TASK_Ahmad_AddColumn_FromDate_AdjustmentAvgRateDetailTable-17-12-2015.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "11_Ameen_Drop_SP_SalesOfInvoiceSummary-16-12-2015.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "12_Ameen_Create_SP_SalesOfInvoiceSummary-16-12-2015.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "13_TASKTFS150_Ameen_AddColumn_AssignedTo_tblDefTasks-15-12-15.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "14_TASKTFS150_Ameen_AddColumn_AssignedTo_tblLeads-15-12-15.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "15_TASKM1812151_IMR_SP_AddConfigOverTimeWorkingHours.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "16_TASKM1912151_IMR_AddColumnAdjETODD.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.2\" & "17_Ameen_AddColumn_Completed_TblDefTasks-19-12-15.sql")
                UpdateDbVersion("2.5.4.2")
                ProgBar()
            End If
            If CurrentVersion < 2543 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.3\" & "1_TASKM_IMR_CreateUniqueIndexTransactionTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.3\" & "2_TASKM_IMR_Drop_SP_DuplicateDocuments.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.3\" & "3_TASKM_IMR_SP_DuplicateDocuments.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.3\" & "4_TASKM_IMR_Drop_SP_DuplicateDocumentsCount.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.3\" & "5_TASKM_IMR_SP_DuplicateDocumentsCount.sql")
                UpdateDbVersion("2.5.4.3")
                ProgBar()
            End If
            If CurrentVersion < 2544 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.4\" & "1_TASK_TFS_AddColumnsCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.4\" & "2_TASK_TFS_Drop_SP_SaleAgreementLetter.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.4\" & "3_TASK_TFS_SP_SaleAgreementLetter.sql")
                UpdateDbVersion("2.5.4.4")
                ProgBar()
            End If
            If CurrentVersion < 2545 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 15
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "1_TASK_TFS_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "2_TASK_TFS_IMR_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "3_TASK_TFS_IMR_AddValuesOrderType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "4_TASK_TFS_IMR_AltercolumnOrderTypeSalesOrderMaster.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "5_TASK_TFS_IMR_InstallmentScheduleSMSTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "6_TASK_TFS_IMR_AddConfigAllowAddZeroPriceOnPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "7_TASK_TFS_IMR_Drop_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "8_TASK_TFS_IMR_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "9_TASK_TFS_IMR_AddColumnDeliveryIDWarrantyClaimMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "10_TASK_TFS_IMR_AddRightsPostDC.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "11_TASK_TFS_IMR_UpdatePostDeliveryChalan.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "12_TASK_TFS_Drop_SP_CostSheetPrint.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "13_TASK_TFS_SP_CostSheetPrint.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "14_TASK_TFS_Drop_SP_SalesOrderLog.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.5\" & "15_TASK_TFS_SP_SalesOrderLog.sql")
                UpdateDbVersion("2.5.4.5")
                ProgBar()
            End If
            If CurrentVersion < 2546 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.6\" & "1_IMR_TASKM162016_Drop_ArticleHistory_ByLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.6\" & "2_IMR_TASKM162016_ArticleHistory_ByLocation.sql")
                UpdateDbVersion("2.5.4.6")
                ProgBar()
            End If
            If CurrentVersion < 2547 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.7\" & "1_TASKM_19161_IMR_AddArticleBrandDefTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.7\" & "2_TASKM_19161_IMR_AddColumnsArticleBrandId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.7\" & "3_TASKM_19161_IMR_Drop_ArticleDefView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.7\" & "4_TASKM_19161_IMR_ArticleDefView.sql")
                UpdateDbVersion("2.5.4.7")
                ProgBar()
            End If
            If CurrentVersion < 2548 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.8\" & "1_TASKM_161161_IMR_Drop_Vw_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.8\" & "2_TASKM_161161_IMR_Vw_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.8\" & "3_TASKM_161161_IMR_AddFormControl.sql")
                UpdateDbVersion("2.5.4.8")
                ProgBar()
            End If
            If CurrentVersion < 2549 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.9\" & "1_ADDColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.9\" & "2_TASKM201161_IMR_Drop_SP_LoanRequest.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.9\" & "3_TASKM201161_IMR_SP_LoanRequest.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.9\" & "4_TASKM201161_IMR_Drop_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.4.9\" & "5_TASKM201161_IMR_SP_InvoiceBasedPayment.sql")
                UpdateDbVersion("2.5.4.9")
                ProgBar()
            End If
            If CurrentVersion < 2550 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.5.0")
                ProgBar()
            End If
            If CurrentVersion < 2551 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.1\" & "1_TASKM281161_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.1\" & "2_TASKM281161_IMR_Drop_SP_DuplicateRecords.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.1\" & "3_TASKM281161_IMR_SP_DuplicateRecords.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.1\" & "4_TASKM291161_IMR_Drop_SP_SalesCertificateLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.1\" & "5_TASKM291161_IMR_SP_SalesCertificateLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.1\" & "6_TASKM291161_IMR_AddColumnInvoiceBasePaymentDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.1\" & "7_TASKM301161_IMR_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.1\" & "8_TASKM301161_IMR_Rights.sql")
                UpdateDbVersion("2.5.5.1")
                ProgBar()
            End If
            If CurrentVersion < 2552 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.2\" & "1_TASKM121161_IMR_Drop_SP_CostPriceForProduction.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.2\" & "2_TASKM121161_IMR_SP_CostPriceForProduction.sql")
                UpdateDbVersion("2.5.5.2")
                ProgBar()
            End If
            If CurrentVersion < 2553 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.5.3")
                ProgBar()
            End If
            If CurrentVersion < 2554 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.4\" & "1_TASKM62161_IMR_Drop_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.4\" & "2_TASKM62161_IMR_SP_Rpt_Receivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.4\" & "3_TASKM62161_IMR_Drop_SP_DirectorDebtors.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.4\" & "4_TASKM62161_IMR_SP_DirectorDebtors.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.4\" & "5_TASKM62161_IMR_AddTableLayoutRemove.sql")
                UpdateDbVersion("2.5.5.4")
                ProgBar()
            End If
            If CurrentVersion < 2555 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.5\" & "1_TASKM82161_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.5\" & "2_TASKM82161_IMR_Drop_EmployeesView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.5\" & "3_TASKM82161_IMR_EmployeesView.sql")
                UpdateDbVersion("2.5.5.5")
                ProgBar()
            End If
            If CurrentVersion < 2556 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.6\" & "1_TASKM92161_IMR_AddTablesAndColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.6\" & "2_TASKM92161_IMR_Drop_SP_SOAndPlanStatus.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.6\" & "3_TASKM92161_IMR_SP_SOAndPlanStatus.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.6\" & "4_TASKM112161_IMR_NewSecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.6\" & "5_TASKM112161_Ameen-Drop_AddColumn-ArticlePicture-To-ArticleDefView-10-02-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.6\" & "6_TASKM112161_Ameen-AddColumn-ArticlePicture-To-ArticleDefView-10-02-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.6\" & "7_TASKM112161_Ameen_Drop_Create_SP_SalesOrderPrint-02-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.6\" & "8_TASKM112161_Ameen_Create_SP_SalesOrderPrint-02-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.6\" & "9_TASKM112161_IMR_Drop_CreateFuncAttendanceDates.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.6\" & "10_TASKM112161_IMR_CreateFuncAttendanceDates.sql")
                UpdateDbVersion("2.5.5.6")
                ProgBar()
            End If
            If CurrentVersion < 2557 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.7\" & "1_TASKM162161_IMR_AddColumn.sql")
                UpdateDbVersion("2.5.5.7")
                ProgBar()
            End If
            If CurrentVersion < 2558 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 13
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.8\" & "1_TASKM232161_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.8\" & "2_TASKM232161_IMR_Drop_SP_RptVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.8\" & "3_TASKM232161_IMR_SP_RptVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.8\" & "4_TASKM232161_IMR_Drop_SP_Rpt_Ledger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.8\" & "5_TASKM232161_IMR_SP_Rpt_Ledger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.8\" & "6_TASKM232161_IMR_Drop_SP_RackWiseClosingStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.8\" & "7_TASKM232161_IMR_SP_RackWiseClosingStock.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.8\" & "8_TASKM232161_IMR_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.8\" & "9_TASKM232161_IMR_tblTermsAndConditionType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.8\" & "10_TAKSM232161_IMR_AddTermsAndCondition.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.8\" & "11_TASKM232161_IMR_New_Security_Rights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.8\" & "12_TASKM232161_IMR_Drop_Vew_EmpAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.8\" & "13_TASKM232161_IMR_Vew_EmpAttendance.sql")
                UpdateDbVersion("2.5.5.8")
                ProgBar()
            End If
            If CurrentVersion < 2559 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.9\" & "1_TASKM262161_IMR_Drop_SP_CostPriceForProduction.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.9\" & "2_TASKM262161_IMR_SP_CostPriceForProduction.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.9\" & "3_TASKM262161_IMR_Drop_SP_CostPriceForRawMaterial.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.5.9\" & "4_TASKM262161_IMR_SP_CostPriceForRawMaterial.sql")
                UpdateDbVersion("2.5.5.9")
                ProgBar()
            End If
            If CurrentVersion < 2560 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 11
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.0\" & "0_Ameen_AddColumn_EndDate_tblCustomer-18-02-16.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.0\" & "0_Ameen_AddColumn_StartDate_tblCustomer-18-02-16.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.0\" & "1_Imr_addColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.0\" & "2_Drop_vwCOADetail-19-02-2019.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.0\" & "3_AddColumn_ContactInfo.EndDate-To-vwCOADetail-19-02-2019.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.0\" & "4_Imr_AddRights_TradePriceExceed.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.0\" & "5_Imr_Drop_SP_RptPurchaseOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.0\" & "6_Imr_SP_RptPurchaseOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.0\" & "7_Drop_SP_RptVoucher-22-02-16.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.0\" & "8_ALTER_SP_RptVoucher-22-02-16.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.0\" & "9_UpdateOffDay.sql")
                UpdateDbVersion("2.5.6.0")
                ProgBar()
            End If
            If CurrentVersion < 2561 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 19
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "1_Imr_Drop_SP_Rpt_Ledger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "2_Imr_SP_Rpt_Ledger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "3_Ameen_AddColumn_IsDailyWages_To_tblDefEmployee-03-03-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "4_Ameen_AddTabel_tblUserAccountRights-02-03-16.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "5__TASKm34161_Ameen_Drop_EmployeesView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "6_Ameen_IsDailyWages_Column_To_EmployeesView-03-03-16.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "7_Ameen_AddColumnUserPicture.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "8_TASKM161161_AMEEN_AddNotificationTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "9_TASKM161161_AMEEN_AddtblNotificationsDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "10_TASKM_161161_IMR_AddTableNotificationActivity.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "11_TASKM_161161_IMR_AddTabletblNotificationActivityConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "12_TASKM161161_IMR_Drop_SP_NotificationConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "13_TASKM161161_IMR_SP_NotificationConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "14_TASKM_161161_IMR_AddtblNotifications.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "15_TASKM34161_IMR_Drop_FunctionAttendanceData.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "16_TASKM34161_IMR_FunctionAttendanceData.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "17_TASKM34161_IMR_AddColumnApprovedUserOnQuotation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "18_TASKM34161_IMR_AlterLengthVoucher_No.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.1\" & "19_TASKM34161_IMR_AddIndexesonVoucher.sql")
                UpdateDbVersion("2.5.6.1")
                ProgBar()
            End If
            If CurrentVersion < 2562 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.2\" & "1_TASKM73161_IMR_Drop_FncAttendanceData.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.2\" & "2_TASKM73161_IMR_FncAttendanceData.sql")
                UpdateDbVersion("2.5.6.2")
                ProgBar()
            End If
            If CurrentVersion < 2563 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 11
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.3\" & "1_drop_Invoice_based_payment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.3\" & "2_Invoice_based_payment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.3\" & "3_AddConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.3\" & "4_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.3\" & "5_UpdateFuelExp.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.3\" & "6_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.3\" & "7_TASKM_Drop_View_ArticleDefView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.3\" & "8_TASKM_View_ArticleDefView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.3\" & "9_TASKM_AddConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.3\" & "10_TASKM_Drop_SP_RptPurchaseOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.3\" & "11_TASKM_SP_RptPurchaseOrder.sql")
                UpdateDbVersion("2.5.6.3")
                ProgBar()
            End If
            If CurrentVersion < 2564 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("2.5.6.4")
                ProgBar()
            End If
            If CurrentVersion < 2565 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.5\" & "1_TASKM243161_IMR_Drop_SP_LastLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.5\" & "2_TASKM243161_IMR_SP_LastLedger.sql")
                UpdateDbVersion("2.5.6.5")
                ProgBar()
            End If
            If CurrentVersion < 2566 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 16
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "1_ALTER_Drop_SP_RptVoucher-22-02-16.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "2_ALTER_SP_RptVoucher-22-02-16.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "3_TASKM94161_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "4_TASKM114161_IMR_Drop_SP_SOAndPlanStatus.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "5_TASKM114161_IMR_SP_SOAndPlanStatus.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "6_TASKM411161_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "7_TASKM411161_IMR_ActiveUpdateLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "Ameen_Add_Drop_SP_MaterialPlan-02-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "Ameen_Add_SP_MaterialPlan-02-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "Ameen_Add_Drop_SP_Plan-02-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "Ameen_Add_SP_Plan-02-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "Ameen_AddViewRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "Ameen_Create_Drop_spEmployeeOverTime-25-02-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "Ameen_Create_spEmployeeOverTime-25-02-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "Ameen_Drop_SP_RptPurchaseOrder_Joined_COA_DETAIL_ID_With_VendorId-30-03-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.6\" & "Ameen_SP_RptPurchaseOrder_Joined_COA_DETAIL_ID_With_VendorId-30-03-2016.sql")
                UpdateDbVersion("2.5.6.6")
                ProgBar()
            End If
            If CurrentVersion < 2567 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "0_update_zeroId.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "0_voucher_id.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "1-AddMissingMasterIdOfArticleDefMasterTableFromArticleDefDetailTable-16-02-2016.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "2-AddMissingIDsForDeliveryChalanMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "3-AddMissingIDsForDispatchMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "4-AddMissingIDsForPurchaseOrderMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "5-AddMissingIDsForPurchaseReturnMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "6-AddMissingIDsForQuotationMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "7-AddMissingIDsForReceivingMasteTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "8-AddMissingIDsForReceivingNoteMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "9-AddMissingIdsForSalesMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "10-AddMissingIDsForSalesOrderMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "11-AddMissingIdsForSalesReturnMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "12-AddMissingIDsForStockMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "13-AddMissingIDsFortblCOAMainSubSub.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "14-AddMissingIDsFortblCOAMainSubSubDetailAgainsttblVoucherDetail.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "15-AddMissingIDsFortblDefLocationFromStockDetailTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "16-AddMissingIDsFortblVoucher.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\1\" & "17-AddMissingIDsForWarrantyClaimMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\2\" & "UpdateDuplicateColumnValuesForSalesMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\2\" & "UpdateDuplicateDeliveryNoColumnValuesForDeliveryChalanMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\2\" & "UpdateDuplicateDispatchNoColumnValuesForDispatchMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\2\" & "UpdateDuplicateDocNoColumnValuesForStockDispatchMaster.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\2\" & "UpdateDuplicateDocNoColumnValuesForStockMaster.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\2\" & "UpdateDuplicateDocNoColumnValuesForWarrantyClaimMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\2\" & "UpdateDuplicatePurchaseDemandNoForPurchaseDemandMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\2\" & "UpdateDuplicatePurchaseOrderNoColumnValuesForPurchaseOrderMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\2\" & "UpdateDuplicatePurchaseReturnNoColumnValuesForPurchaseReturnMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\2\" & "UpdateDuplicateQuotationNoColumnValuesForQuotationMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\2\" & "UpdateDuplicateReceivingNoColumnValuesForReceivingMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\2\" & "UpdateDuplicateReceivingNoColumnValuesForReceivingNoteMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\2\" & "UpdateDuplicateSalesOrderNoColumnValuesForSalesOrderMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\2\" & "UpdateDuplicateSalesReturnNoColumnValuesForSalesReturnMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "0_TASK_TFS_CreateConstraintRulls_ArticleDefTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "0_TASKM221161_IMR_DeleteMissingData.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "1_TASK_TFS_CreateContraintsRulls_SalesMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "2_TASK_TFS_CreateContraintsRulls_SalesReturnMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "3_TASK_TFS_CreateContraintsRulls_SalesOrderMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "4_TASK_TFS_CreateContraintsRulls_QuotationMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "5_TASK_TFS_CreateContraintsRulls_DeliveryChalanMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "6_TASK_TFS_CreateContraintsRulls_ReceivingMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "7_TASK_TFS_CreateContraintsRulls_PurchaseReturnMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "8_TASK_TFS_CreateContraintsRulls_PurchaseOrderMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "9_TASK_TFS_CreateContraintsRulls_PurchaseDemandMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "10_TASK_TFS_CreateContraintsRulls_ReceivingNoteMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "11_TASK_TFS_CreateContraintsRulls_DispatchMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "12_TASK_TFS_CreateContraintsRulls_ProductionMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "13_TASK_TFS_CreateContraintsRulls_LCMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "14_TASK_TFS_CreateContraintsRulls_WarrantyClaimMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "15_TASK_TFS_CreateContraintsRulls_StockAdjustmentMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "16_TASK_TFS_CreateContraintsRulls_ReturnDispatchMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "17_TASK_TFS_CreateConstrainRulls_tblVoucher.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "18_TASK_TFS_CreateConstraintRulls_StockMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "19_TASK134161_IMR_AddColumn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.7\" & "20_TASKM134161_IMR_PKVoucherType.sql")
                UpdateDbVersion("2.5.6.7")
                ProgBar()
            End If
            If CurrentVersion < 2568 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 32
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "1_Ameen_Drop_SP_InstallmentDocument-20-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "2_Ameen_Altered_SP_InstallmentDocument-20-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "3_Ameen_CreatedColumn_AC_To_Installment-20-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "4_Ameen_CreatedColumn_Cast_To_InstallmentMasterTable-20-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "5_Ameen_CreatedColumn_CNIC_To_InstallmentMasterTable-20-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "6_Ameen_CreatedColumn_EngineNo_To_InstallmentMasterTable-20-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "7_Ameen_CreatedColumn_FatherName_To_InstallmentMasterTable-20-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "8_Ameen_CreatedColumn_RegistrationNo_To_InstallmentMasterTable-20-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "9_Ameen_CreatedTable_InstallmentWitnessOne-20-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "10_Ameen_CreatedTable_InstallmentWitnessTwo-20-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "10A_Ameen_CreatedColumn_creditdifference_To_tblCustomer.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "10B_Ameen_CreatedColumn_debitdifference_To_tblCustomer.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "11_TASKM21416_IMR_Drop_SP_SalesCertificateLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "12_TASKM21416_IMR_SP_SalesCertificateLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "13_TASK214161_IMR_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "14_TASKM214161_IMR_Drop_SP_LocationWiseStockLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "15_TASKM214161_IMR_SP_LocationWiseStockLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "16_Ameen_AddedColumn_SOQuantity_ToTable_QuotationDetailTable-13-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "17_Ameen_InsertValuesOf_EnableDuplicateQuotation_ToConfigValuesTable-16-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "18_Ameen_InsertValuesOf_EnableDuplicateSalesOrder_ToConfigValuesTable-16-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "19_Ameen_AddColumn_SED_Tax_Percent_ToQuotationDetailTable-163-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "20_Ameen_AddedColumn_SED_Tax_Amount_ToQuotationDetailTable-13-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "21_Ameen_Created_QuotationDetailId_Column_For_SalesOrderDetailTable-12-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "22_Ameen_CreatedTable_tmpQuotation-16-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "23_Ameen_CreatedTable_tmpQuotationDetail-16-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "24_Ameen_CreatedTable_tmpSalesOrder-16-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "25_Ameen_CreatedTable_tmpSalesOrderDetail-16-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "26_Ameen_Drop_SP_SaleReturnSummary-15-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "27_Ameen_Modified_SaleReturnSummary_AddedColumns_CustomerType_&_Weight-15-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "28_TASKM224161_IMR_Drop_SP_AttendanceDetailByIn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "29_TASKM224161_IMR_SP_AttendanceDetailByIn.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "30_Ameen_Drop_sp_Return_Store_Issuence_Summary-22-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "31_Ameen_Added_sp_Return_Store_Issuence_Summary-22-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.8\" & "32_TASKM224161_IMR_UpdateBSNotes.sql")
                UpdateDbVersion("2.5.6.8")
                ProgBar()
            End If
            If CurrentVersion < 2569 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.9\" & "1_TASKM254161_IMR_AddColumnChangerSerialNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.9\" & "2_TASKM254161_IMR_ChangeSerialNoSalesCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.9\" & "3_TASKM254161_IMR_Drop_SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.9\" & "4_TASKM254161_IMR_SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.9\" & "5_TASKM274161_IMR_Drop_SP_InvoiceAgingFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.6.9\" & "6_TASKM274161_IMR_SP_InvoiceAgingFormated.sql")
                UpdateDbVersion("2.5.6.9")
                ProgBar()
            End If
            If CurrentVersion < 2570 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 33
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "1_Ameen_Drop_tmpQuotationDetail-26-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "2_Ameen_CreatedTable_tmpQuotationDetail-26-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "3_Ameen_Drop_tmpQuotation-26-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "4_Ameen_CreatedTable_tmpQuotation-26-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "5_Ameen_AddedNewColumn_ AttendanceDate_To_tblEmployeeOverTimeSchedule-05-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "6_Ameen_AddedNewColumn_AutoOverTime_To_tblEmployeeOverTimeSchedule-05-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "7_Ameen_AddedNewColumn_Hours_To_tblEmployeeOverTimeSchedule-05-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "8_Ameen_AddedNewColumn_OverTime_StartTime_To_ShiftTable-04-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "9_Ameen_AddedValue_KeepConfigurationMonthDays_To_ConfigValuesTable-04-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "10_Ameen_AddValues_Receivables To Customers_To_SMSConfigurationTable-02-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "11_Ameen_Drop_SP_DuplicateQuotation-26-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "12_Ameen_Created_SP_DuplicateQuotation_For_rptQuotationDuplicate-23-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "13_Ameen_DROP_SP_SalesOrderDuplicate-27-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "14A_Drop_SP_GetAutoEmployeesOverTime.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "14B_SP_GetAutoEmployeesOverTime-03-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "15_Ameen_Drop_tmpSalesOrder-27-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "16_Ameen_CreatedTable_tmpSalesOrder-27-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "17_Ameen_Drop_ProductionSummary-28-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "18_Ameen_Modified_ProductionSummary_WithAddingColumn_EmployeeID-28-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "19_Ameen_Drop_SP_Rpt_Receivable-29-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "20_Ameen_Modified_SP_Rpt_Receivable_WithAddingParameter_@SubSubID-29-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "21_Ameen_Drop_vwCOADetail-28-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "22_Ameen_AddedNewColumn_CustomerTypeID_To_vwCOADetail-28-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "23_TASKM254161_IMR_Drop_SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "24_TASKM254161_IMR_SP_BalanceSheetFormated.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "25_Ameen_Drop_tmpSalesOrderDetail-27-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "26_Ameen_CreatedTable_tmpSalesOrderDetail-27-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "27_Ameen_Drop_SP_SalesOfInvoiceSummary-27-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "28_Ameen_Modified_SP_SalesOfInvoiceSummary-27-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "29_TASKM254161_IMR_AddColumnChangerSerialNo.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "30_TASKM254161_IMR_ChangeSerialNoSalesCertificate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "31_Ameen_Created_SP_SalesOrderDuplicate-27-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "32_Ameen_Drop_SaleReturnSummary-28-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.0\" & "33_Ameen_AddedNewColumn_CustomerTypeID_To_SaleReturnSummary-28-04-2016.sql")
                UpdateDbVersion("2.5.7.0")
                ProgBar()
            End If

            If CurrentVersion < 2571 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.1\" & "19_Ameen_Drop_SP_Rpt_Receivable-29-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.1\" & "20_Ameen_Modified_ SP_Rpt_Receivable-09-05-2016.sql")
                UpdateDbVersion("2.5.7.1")
                ProgBar()
            End If
            If CurrentVersion < 2572 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.2\" & "1_Ameen_Drop_SP_Rpt_Receivable-29-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.2\" & "2_Ameen_Modified_SP_Rpt_Receivable-10-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.2\" & "3_TASKM105161_IMR_Drop_SP_InvoiceBasedAgingReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.2\" & "4_TASKM105161_IMR_SP_InvoiceBasedAgingReport.sql")
                UpdateDbVersion("2.5.7.2")
                ProgBar()
            End If
            If CurrentVersion < 2573 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.3\" & "1_Ameen_Drop_SP_InvoiceAging-18-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.3\" & "2_SP_Invoice_aging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.3\" & "3_Ameen_Drop_SP_PurchaseInvoiceAging-18-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.3\" & "4_SP_PurchaseInvoice_Aging.sql")
                UpdateDbVersion("2.5.7.3")
                ProgBar()
            End If
            If CurrentVersion < 2574 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 6
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "1_Ameen_Drop_SP_InvoiceAging-18-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "2_Invoice_aging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "3_Ameen_Drop_SP_PurchaseInvoiceAging-18-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "4_PurchaseInvoice_Aging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "5_Drop_SP_SalesOfInvoiceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "6_Ameen_Modified_SP_SalesOfInvoiceSummary_By_Adding_Weightage-16-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "7_AM_AddedColumn_MinHalfAbsentHours_ShiftTable-05-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "8_Ameen_AddedValues_Currency_To_ConfigValuesTable-09-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "9_Ameen_AddedColumn_Currency_API_To_tblCurrency-09-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "10_Ameen_AddedTable_tblCurrencyRate-09-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "11_Ameen_AddedColumn_BaseCurrencyId_To_tblVoucherDetail-11-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "12_Ameen_AddedColumn_BaseCurrencyRate_To_tblVoucherDetail-11-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "13_Ameen_AddedColumn_CurrencyAmount_To_tblVoucherDetail-11-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "14_Ameen_AddedColumn_CurrencyId_To_tblVoucherDetail-11-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\2.5.7.4\" & "15_Ameen_AddedColumn_CurrencyRate_To_tblVoucherDetail-11-05-2016.sql")
                UpdateDbVersion("2.5.7.4")
                ProgBar()
            End If

            If CurrentVersion < 3000 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 100
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "0_update_zeroId.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "0_voucher_id.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "1-AddMissingMasterIdOfArticleDefMasterTableFromArticleDefDetailTable-16-02-2016.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "2-AddMissingIDsForDeliveryChalanMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "3-AddMissingIDsForDispatchMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "4-AddMissingIDsForPurchaseOrderMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "5-AddMissingIDsForPurchaseReturnMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "6-AddMissingIDsForQuotationMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "7-AddMissingIDsForReceivingMasteTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "8-AddMissingIDsForReceivingNoteMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "9-AddMissingIdsForSalesMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "10-AddMissingIDsForSalesOrderMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "11-AddMissingIdsForSalesReturnMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "12-AddMissingIDsForStockMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "13-AddMissingIDsFortblCOAMainSubSub.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "14-AddMissingIDsFortblCOAMainSubSubDetailAgainsttblVoucherDetail.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "15-AddMissingIDsFortblDefLocationFromStockDetailTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "16-AddMissingIDsFortblVoucher.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\1\" & "17-AddMissingIDsForWarrantyClaimMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\2\" & "UpdateDuplicateColumnValuesForSalesMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\2\" & "UpdateDuplicateDeliveryNoColumnValuesForDeliveryChalanMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\2\" & "UpdateDuplicateDispatchNoColumnValuesForDispatchMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\2\" & "UpdateDuplicateDocNoColumnValuesForStockDispatchMaster.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\2\" & "UpdateDuplicateDocNoColumnValuesForStockMaster.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\2\" & "UpdateDuplicateDocNoColumnValuesForWarrantyClaimMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\2\" & "UpdateDuplicatePurchaseDemandNoForPurchaseDemandMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\2\" & "UpdateDuplicatePurchaseOrderNoColumnValuesForPurchaseOrderMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\2\" & "UpdateDuplicatePurchaseReturnNoColumnValuesForPurchaseReturnMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\2\" & "UpdateDuplicateQuotationNoColumnValuesForQuotationMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\2\" & "UpdateDuplicateReceivingNoColumnValuesForReceivingMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\2\" & "UpdateDuplicateReceivingNoColumnValuesForReceivingNoteMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\2\" & "UpdateDuplicateSalesOrderNoColumnValuesForSalesOrderMasterTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\2\" & "UpdateDuplicateSalesReturnNoColumnValuesForSalesReturnMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "0_TASK_TFS_CreateConstraintRulls_ArticleDefTable.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "0_TASKM221161_IMR_DeleteMissingData.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "1_TASK_TFS_CreateContraintsRulls_SalesMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "2_TASK_TFS_CreateContraintsRulls_SalesReturnMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "3_TASK_TFS_CreateContraintsRulls_SalesOrderMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "4_TASK_TFS_CreateContraintsRulls_QuotationMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "5_TASK_TFS_CreateContraintsRulls_DeliveryChalanMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "6_TASK_TFS_CreateContraintsRulls_ReceivingMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "7_TASK_TFS_CreateContraintsRulls_PurchaseReturnMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "8_TASK_TFS_CreateContraintsRulls_PurchaseOrderMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "9_TASK_TFS_CreateContraintsRulls_PurchaseDemandMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "10_TASK_TFS_CreateContraintsRulls_ReceivingNoteMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "11_TASK_TFS_CreateContraintsRulls_DispatchMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "12_TASK_TFS_CreateContraintsRulls_ProductionMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "13_TASK_TFS_CreateContraintsRulls_LCMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "14_TASK_TFS_CreateContraintsRulls_WarrantyClaimMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "15_TASK_TFS_CreateContraintsRulls_StockAdjustmentMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "16_TASK_TFS_CreateContraintsRulls_ReturnDispatchMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "17_TASK_TFS_CreateConstrainRulls_tblVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "18_TASK_TFS_CreateConstraintRulls_StockMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "19_TASKM111161_IMR_Drop_SP_CashBankBalances.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "20_TASKM111161_IMR_SP_CashBankBalances.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "21_TASKM121161_IMR_Drop_SP_SalesPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "22_TASKM121161_IMR_SP_SalesPurchase.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "23_TASKM121161_IMR_Drop_SP_PayableReceivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "24_TASKM121161_IMR_SP_PayableReceivable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "25_TASKM121161_IMR_Drop_SP_DashboardExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "26_TASKM121161_IMR_SP_DashboardExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "27_TASKM121161_IMR_Drop_SP_DashboardPostedVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "28_TASKM121161_IMR_SP_DashboardPostedVoucher.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "29_TASKM121161_IMR_Drop_SP_DashboardAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "30_TASKM121161_IMR_SP_DashboardAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "31_TASKM121161_IMR_AddConfigBelowRetailPrice.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "32_Ameen_AddColumn_Completed_TblDefTasks.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "33_Ameen_AddColumn_FormName_TblDefTasks.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "34_Ameen_AddColumn_Ref_No_TblDefTasks.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "35_Ameen_Create_Table_DatabaseBackupProcess.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "36_Ameen_CreateTable_tblPictures.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "37_TASKM141161_IMR_Drop_SP_DashboardTasks.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "38_TASKM141161_IMR_SP_DashboardTasks.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "39_TASKM141161_IMR_Dorp_View_PendingGRN.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "40_TASKM141161_IMR_View_PendingGRN.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "41_TASKM161161_AMEEN_AddtblNotificationsDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "42_TASKM_161161_IMR_AddTableNotificationActivity.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "43_TASKM_161161_IMR_AddTabletblNotificationActivityConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "44_TASKM161161_IMR_Drop_SP_NotificationConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "45_TASKM161161_IMR_SP_NotificationConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "46_TASKM_161161_IMR_AddtblNotifications.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "47_Ameen_AddTable_TerminalConfigurationDetail-19-01-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "48_Ameen_AddTable_TerminalConfigurationMaster-19-01-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "49_Ameen_AddTable_TerminalConfigurationSystems-19-01-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "50_Ameen_AddTable_TerminalConfigurationUsers-19-01-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "51_TASKM1201161_IMR_Drop_SP_DashboardStockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "52_TASKM1201161_IMR_SP_DashboardStockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "53_TASKM211161_IMR_Drop_SP_SingleModulesMDI.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "54_TASKM211161_IMR_SP_SingleModulesMDI.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "55_TASKM211161_IMR_Drop_SP_MultiModulesMDI.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "56_TASKM211161_IMR_SP_MultiModulesMDI.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "57_TASKM221161_AMN_UpdateAccessKeyForms.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "58_TASKM221161_IMR_AddConfig.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "59_Ameen_AddColumnUserPicture.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "60_TASKM_IMR_AddTabletblCommisssionDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "61_Ameen_AddColumn_Drop_SP_Customer_Item_Sales_Summary-25-01-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "62_Ameen_AddColumn-dbo.ArticleColorDefTable.ArticleColorName_To_SP_Customer_Item_Sales_Summary-25-01-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "63_Ameen_Comments-detail_title-Cheque_No-ColumnsLengthIncreased_TblrptCashFlowStander-22-01-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "64_TASKM_AMN_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "65_TASKM82161_IMR_AddConfigValues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "66_Ameen_AddTable_tblTaskActivityUpdate-08-02-16.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.0\" & "67_Ameen_AddTable_tblTaskActivityType-11-02-2016.sql")
                UpdateDbVersion("3.0.0.0")
                ProgBar()
            End If
            If CurrentVersion < 3001 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.1\" & "1_AddColumnEntryDate.sql")
                UpdateDbVersion("3.0.0.1")
                ProgBar()
            End If
            If CurrentVersion < 3002 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 8
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.2\" & "1_TASKM243161_IMR_Drop_SP_LastLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.2\" & "2_TASKM243161_IMR_SP_LastLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.2\" & "3_AddColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.2\" & "4_altercolumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.2\" & "5_Ameen_AddColumn_Active_To_tblDefLocation_14-03-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.2\" & "6_Ameen_Drop_SP_SummaryOfPurchaseInvoices_14-03-2019.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.2\" & "7_Ameen_Create_SP_SummaryOfPurchaseInvoices_14-03-2019.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.2\" & "8_Ameen_Table_tblUserCostCentreRights-17-03-2016.sql")
                UpdateDbVersion("3.0.0.2")
                ProgBar()
            End If
            If CurrentVersion < 3003 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 40
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "1_TASKM28316_IMR_Drop_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "2_TASKM28316_IMR_SP_InvoiceBasedPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "3_TASKM234161_IMR_AddColumnApproved_Group_Id.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "4_TASKM234161_IMR_Drop_SP_API_ExpenseDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "5_TASKM234161_IMR_SP_API_ExpenseDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "6_TASKM234161_IMR_Drop_SP_API_OpeningCashBankBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "7_TASKM234161_IMR_SP_API_OpeningCashBankBalance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "8_TASKM234161_IMR_Drop_SP_API_PayableAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "9_TASKM234161_IMR_SP_API_PayableAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "10_TASKM234161_IMR_Drop_SP_API_ReceivableAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "11_TASKM234161_IMR_SP_API_ReceivableAging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "12_TASKM234161_IMR_Drop_SP_API_TCOADetailLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "13_TASKM234161_IMR_SP_API_TCOADetailLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "14_TASKM234161_IMR_Drop_SP_API_TDetailAccounts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "15_TASKM234161_IMR_SP_API_TDetailAccounts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "16_TASKM234161_IMR_Drop_SP_API_TMainAccounts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "17_TASKM234161_IMR_SP_API_TMainAccounts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "18_TASKM234161_IMR_Drop_SP_API_TSubAccounts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "19_TASKM234161_IMR_SP_API_TSubAccounts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "20_TASKM234161_IMR_Drop_SP_API_TSubSubAccounts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "21_TASKM234161_IMR_SP_API_TSubSubAccounts.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "22_TASKM234161_IMR_Drop_SP_CashBankBalances.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "23_TASKM234161_IMR_SP_CashBankBalances.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "24_TASKM234161_IMR_DROP_SP_CashDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "25_TASKM234161_IMR_SP_CashDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "26_TASKM234161_IMR_Drop_SP_CashPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "27_TASKM234161_IMR_SP_CashPayment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "28_TAKSM234161_IMR_Drop_SP_CashReceipt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "29_TAKSM234161_IMR_SP_CashReceipt.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "30_TASKM234161_IMR_Drop_SP_DashboardAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "31_TASKM234161_IMR_SP_DashboardAttendance.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "32_TASKM234161_IMR_Drop_SP_DashboardTasks.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "33_TASKM234161_IMR_SP_DashboardTasks.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "34_TASKM234161_IMR_AddTablesVoucherLog.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "34_TASKM234161_IMR_AddTablesVoucherLog.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "35_TASKM234161_IMR_Drop__SP_VoucherStatus_Changer.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "36_TASKM234161_IMR__SP_VoucherStatus_Changer.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "37_TASKM234161_IMR_Drop_SP_DashboardExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "38_TASKM234161_IMR_SP_DashboardExpense.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "39_TASKM234161_IMR_Drop_SP_DashboardStockLevel.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.3\" & "40_TASKM234161_IMR_SP_DashboardStockLevel.sql")
                UpdateDbVersion("3.0.0.3")
                ProgBar()
            End If
            If CurrentVersion < 3004 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 44
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "1_AddTables.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "2_updatecostsheetvalues.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "3_addColumns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "4_Addcolumns1.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "5_UpdateSpecificationUpdate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "6_AddColumn2.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "7_ArticleCodeIncreasedLength.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "8_Ameen_CreatedTable_SalesOrderStatusTable_AndAlsoFilledWithDataFrom_SalesOrderTypeTable-16-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "9_Ameen_Renamed_SalesOrderTypeTable_To_SalesOrderTypeTableOld-16-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "10_RenameOrderTypeColumnOnSalesOrderMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "11_AddTableSalesOrderType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "12_tblDefConsigneeLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "13_Ameen_AddColumn_SED_Tax_Percent_ToQuotationDetailTable-163-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "14_Ameen_AddedColumn_SED_Tax_Amount_ToQuotationDetailTable-13-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "15_Ameen_AddedColumn_SOQuantity_ToTable_QuotationDetailTable-13-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "16_Ameen_Created_QuotationDetailId_Column_For_SalesOrderDetailTable-12-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "17_Ameen_CreatedTable_tmpQuotation-16-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "18_Ameen_CreatedTable_tmpQuotationDetail-16-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "19_Ameen_CreatedTable_tmpSalesOrder-16-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "20_Ameen_CreatedTable_tmpSalesOrderDetail-16-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "21_Ameen_Drop_SP_SaleReturnSummary-15-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "22_Ameen_InsertValuesOf_EnableDuplicateQuotation_ToConfigValuesTable-16-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "23_Ameen_InsertValuesOf_EnableDuplicateSalesOrder_ToConfigValuesTable-16-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "24_Ameen_Modified_SaleReturnSummary_AddedColumns_CustomerType_&_Weight-15-04-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "25_Ameen_Drop_SP_TaxDeductionDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "26_Ameen_SP_TaxDeductionDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "27_AddTableArticleAliasDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "28_AddColumnArticleAliasSalesOrderDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "29_TASKM2942016_IMR_AddColumn_ConsignLocation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "30_TASKM294161_AddColumnInGatePassMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "31_TASKM304161_IMR_AddTable_tmpCostSheet.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "32_TASKM304161_IMR_AddTable_tblCostSheetPlan.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "33_TASKM305161_IMR_AddColumnsSaleOrderType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "34_TASKM35161_IMR_AddColumnManufactured.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "35_TASKM95161_IMR_AddOfficeLocationTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "36_TAKSM95161_IMR_AddColumnOfficeLocationId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "37_TASKM105161_IMR_Drop_ArticleDefView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "38_TASKM105161_IMR_ArticleDefView.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "39_TASKM105161_IMR_AddTable_tblCostSheetOpening.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "40_TASKM105161_IMR_Drop_SP_InvoiceBasedAgingReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "41_TASKM105161_IMR_SP_InvoiceBasedAgingReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "42_TASKM115161_IMR_AddColumnsLoanRequestIdtblVoucherDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "43_TASKM11520161_IMR_AddColunns_In_WIP_Services.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.0.4\" & "44_TAKSM175161_IMR_RenameColumnLot_No_StoreIssuence.sql")
                UpdateDbVersion("3.0.0.4")
                ProgBar()
            End If
            If CurrentVersion < 3010 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 31
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "1_Ameen_Drop_SP_SalesOfInvoiceSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "02_Ameen_Modified_SP_SalesOfInvoiceSummary_By_Adding_Weightage-16-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "3_AddTableCurrency.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "4_Ameen_AddedColumn_Currency_API_To_tblCurrency-09-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "4A_Add_CurrencyList.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "5_DropTable_tblCurrencyRate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "5A_Add_Table_tblCurrencyRate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "6_Add_Currency_Rate_Default_Values.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "7_Add_VoucherDetail_Columns.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "8_Set_Base_Currency_In_ConfigValueTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "9_AM_AddedColumn_MinHalfAbsentHours_ShiftTable-05-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "11_Ameen_AddedColumn_ConsignLocationId_To_SalesMasterTable-21-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "12_Ameen_AddedColumn_DeliveredTotalQty_To_SalesOrderDetailTable-31-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "13_Ameen_AddedColumn_OfficeLocationId_To_SalesMasterTable-21-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "14_Update_Previous_Voucher_data.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "15_Ameen_Drop_SP_InvoiceAging-18-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "16_Invoice_aging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "17_Ameen_Drop_SP_PurchaseInvoiceAging-18-05-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "18_PurchaseInvoice_Aging.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "19_Drop_SP_Rpt_Ledger_MultiCurrency.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "20_Create_SP_Rpt_Ledger_MultiCurrency.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "21_Drop_SP_RptVoucherMultiCurrency.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "22_Create_SP_RptVoucherMultiCurrency.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "23_Ameen_AddedColumn_DeliveredTotalQty_To_DeliveryChalanDetailTable-06-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "24_Ameen_AddedColumn_DeliveredTotalQty_To_PurchaseOrderDetailTable-11-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "25_Ameen_AddedColumn_ProducedTotalQty_To_PlanDetailTable-14-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "26_Ameen_AddedColumn_PurchaseReturnTotalQty_To_ReceivingDetailTable-08-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "27_Ameen_AddedColumn_ReceivedTotalQty_To_PurchaseOrderDetailTable-08-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "28_Ameen_AddedColumn_SalesReturnTotalQty_To_SalesDetailTable-11-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "29_Ameen_AddedColumn_To_QuotationDetailTable-11-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "30_Ameen_UpdatedColumn_DeliveredTotalQty_InAllTransactionsTableIfColumnIsNull-16-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "31_Drop_Sp_Invoice_Receipt_Details.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.0\" & "32_Create_Sp_Invoice_Receipt_Details.sql")
                UpdateDbVersion("3.0.1.0")
                ProgBar()
            End If
            If CurrentVersion < 3011 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 22
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "01_Ameen_CreatedTable_LabParameterDetailTable-30-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "02_Ameen_CreatedTable_LabParameterMasterTable-30-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "03_Ameen_CreatedTable_LabRequestDetailTable-30-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "04_Ameen_CreatedTable_LabRequestMasterTable-30-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "05_Ameen_CreatedTable_LabResultDetailTable-30-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "06_Ameen_CreatedTable_LabResultMasterTable-30-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "07_Ameen_CreatedTable_LabSampleParameters-30-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "08_Ameen_CreatedTable_LabTestingParameter-30-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "09_Ameen_CreatedTable_LabTestSampleObvDetail-30-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "10_Ameen_CreatedTable_LabTestSampleObvMaster-30-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "11_Ameen_AddedColumn_TestReq_Detail_Id_To_LabTestSampleObvDetail-23-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "12_Ameen_AddedColumn_TestReq_Id_To_LabTestSampleObvMaster-24-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "13_Ameen_Drop_SP_LabResult-28-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "14_Ameen_Create_SP_LabResult-28-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "15_Ameen_Drop_SP_LabObservationSample-25-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "16_Ameen_Created_SP_LabObservationSample-25-06-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "17_Ameen_Drop_SP_LabRequestSingleItem-25-06-16.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "18_Ameen_Created_SP_LabRequestSingleItem-25-06-16.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "19_AliFaisal_Drop_SP_LabRequest.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "20_AliFaisal_Create_SP_LabRequest.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "21_Drop_sp_LocationWiseStockLedger.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "22_Create_sp_LocationWiseStockLedger.sql")
                UpdateDbVersion("3.0.1.1")
                ProgBar()
            End If

            If CurrentVersion < 3012 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal

                UpdateDbVersion("3.0.1.2")
                ProgBar()
            End If
            If CurrentVersion < 3020 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 12
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.0\" & "01_Ameen_AddedColumn_Cost_Price_To_StockDetailTable-01-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.0\" & "02_Ameen_AddedColumn_In_PackQty_To_StockDetailTable-01-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.0\" & "03_Ameen_AddedColumn_Out_PackQty_To_StockDetailTable-01-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.0\" & "04_Ameen_AddedColumn_Pack_Qty_To_StockDetailTable-01-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.0\" & "05_Add_OverTimeTable_Col_OverTime_Hours.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.0\" & "06_Add_ShiftTable_Col_OverTime_StartTime.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.0\" & "07_Col_ParentId_tblCOAMainSubSubDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.0\" & "08_R@!_Drop_vwCOADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.0\" & "09_R@!_AddedNewColumn_Parent_ID_To_vwCOADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.0\" & "10_Create Table tblFavouriteForms.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.0\" & "11_tblVoucherTemplate.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.0\" & "12_tblVoucherTemplateDetail.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "13_Ameen_Drop_SP_LabResult-28-06-2016.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "14_Ameen_Create_SP_LabResult-28-06-2016.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "15_Ameen_Drop_SP_LabObservationSample-25-06-2016.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "16_Ameen_Created_SP_LabObservationSample-25-06-2016.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "17_Ameen_Drop_SP_LabRequestSingleItem-25-06-16.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "18_Ameen_Created_SP_LabRequestSingleItem-25-06-16.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "19_AliFaisal_Drop_SP_LabRequest.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "20_AliFaisal_Create_SP_LabRequest.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "21_Drop_sp_LocationWiseStockLedger.sql")
                'executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.1.1\" & "22_Create_sp_LocationWiseStockLedger.sql")
                UpdateDbVersion("3.0.2.0")
                ProgBar()
            End If

            If CurrentVersion < 3021 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("3.0.2.1")
                ProgBar()
            End If

            If CurrentVersion < 3022 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.2\" & "1_Ahmed_BackupPassword_Rights.sql")
                UpdateDbVersion("3.0.2.2")
                ProgBar()
            End If

            If CurrentVersion < 3023 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.3\" & "1_AliFaisal_Drop_SP_SecurityRightsReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.3\" & "2_AliFaisal_Create_SP_SecurityRightsReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.3\" & "3_Drop_SP_Un_PostedVoucherCount.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.3\" & "4_Create_Un_PostedVoucherCount.sql")
                UpdateDbVersion("3.0.2.3")
                ProgBar()
            End If

            If CurrentVersion < 3024 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.4\" & "1_drop_SP_UnPostVouchers.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.4\" & "2_Create_SP_UnPostVouchers.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.4\" & "3_drop_Sp_UnPostedVoucherCount.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.2.4\" & "4_Create_Sp_UnPostedVoucherCount.sql")
                UpdateDbVersion("3.0.2.4")
                ProgBar()
            End If

            If CurrentVersion < 3025 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("3.0.2.5")
                ProgBar()
            End If

            If CurrentVersion < 3030 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 57
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "0.1_Ameen_CreatedTable_DepartmentWiseProduction-23-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "0.1_Ameen_CreatedTable_MaterialAnalysisMaster-30-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "0.2_Ameen_CreatedTable_DepartmentWiseProductionDetail-23-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "0.2_Ameen_CreatedTable_MaterialAnalysisDetail-30-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "0.3_Ameen_CreatedTable_AllocationDetail-12-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "0.4_Ameen_CreatedTable_AllocationMaster-12-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "0.5_Ameen_CreatedTable_PlanTickets-26-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "0.6_Ameen_CreatedTable_QuotationDetailHistory-12-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "0.7_Ameen_CreatedTable_QuotationHistory-12-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "0.8_Ameen_CreatedTable_SalesOrderDetailHistory-12-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "0.9_Ameen_CreatedTable_SalesOrderHistory-12-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "0.99_Ameen_CreatedTable_SOItemDeliverySchedule-22-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "1.1_Ameen_AddedColumn_SubDepartmentID_DispatchDetailTable-22-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "1.1_Ameen_ModifiedTable_MaterialEstimation-28-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "1.1_Ameen_ModifiedTable_MaterialEstimationDetailTable-28-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "1.2_Ameen_AddedColumn_SubDepartmentID_MaterialEstimationDetailTable-22-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "1.3_Ameen_AddedColumn_SubDepartmentID_To_tblCostSheet-22-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "1.3_RenameColumn_SaleOrderStatusId_OrderType_In_SalesOrderMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "1.4_AddedColumn_SaleOrderStatusId_To_SalesOrderMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "1.4_Ameen_AddedColumn_RevisionNumber_To_SalesOrderMasterTable-12-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "1.5_Ameen_AddedColumn_PlanTicketId_To_DispatchMasterTable-12-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "1.6_Ameen_AddedColumn_PlanTicketId_To_ProductionMasterTable-13-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "1.7_Ameen_AddedColumn_RevisionNumber_To_QuotationMasterTable-12-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "1.8_Ameen_AddColumn_SpecialInstructions_To_SalesOrderMasterTable-22-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "1.9_Ameen_AddedColumn_TicketID_ReturnDispatchMasterTable-13-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "2.0_Ameen_AddedColumn_TicketID_To_ReceivingMasterTable-13-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "2.1_AliFaisal_Drop_SP_EmpSalarySheet.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "2.1_Ameen_AddValues_To_frmSalaryGenerationWizard-18-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "2_AliFaisal_Create_SP_EmpSalarySheet.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "3_AliFaisal_Drop_SP_EmployeeSalarySheet.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "4_AliFaisal_Create_SP_EmployeeSalarySheet.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "5_AliFaisal_Drop_SP_Rpt_Installment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "6_AliFaisal_Create_SP_Rpt_Installment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "7_AliFaisal_Drop_SP_SalesOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "8_AliFaisal_Create_SP_SalesOrder.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "9_AliFaisal_Drop_SP_SOSchedule.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "10_AliFaisal_Create_SP_SOSchedule.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "11_AliFaisal_Drop_SP_PlanTicket.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "12_AliFaisal_Create_SP_PlanTicket.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "21_Ameen_Drop_SP_QuotationHistory-13-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "22_Ameen_Created_SP_QuotationHistory-13-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "23_Ameen_Drop_SP_SalesOrderHistory-13-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "24_Ameen_Created_SP_SalesOrderHistory-13-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "32_Ameen_Drop_FuncAttendanceData_Description_Commented_HalfLeaveSection-12-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "33_Ameen_Modified_FuncAttendanceData_Description_Commented_HalfLeaveSection-12-07-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "36_AliFaisal_Production_SecurityRights.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "37_AliFaisal_Drop_SP_SecurityRightsReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "38_AliFaisal_Create_SP_SecurityRightsReport.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "42_Ameen_DefineRights_For_frmDepartmentWiseProduction-24-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "45_Ameen_Drop_vw_ArticleStock-27-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "46_Ameen_Modified_vw_ArticleStock_By_AddedColumn_DispatchReturnQty-27-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "47_Ameen_Drop_SP_DepartmentWiseProduction25-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "48_Ameen_Created_SP_DepartmentWiseProduction-25-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "49_Ameen_Drop_SP_CheckStockForStoreIssuence-27-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "50_Ameen_Created_SP_CheckStockForStoreIssuence-27-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "53_Ameen_DROP_SP_MaterialAllocation-26-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "54_Ameen_Created_SP_MaterialAllocation-26-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "55_Ameen_Drop_SP_Estimation-30-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "56_Ameen_SP_Estimation-30-08-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "57_Drop_VT_MaterialAnalysis_AvailableStock_Quantity.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "58_Create_VT_MaterialAnalysis_AvailableStock_Quantity.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "59_Drop_SP_StockAnlysis by Haseeb.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "60_Create_SP_StockAnlysis by Haseeb.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "61_Drop_SP_TT by Haseeb.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "62_Create_SP_TT by Haseeb.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "63_Drop_Sp_TT_DR by Haseeb.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "64_Create_Sp_TT_DR by Haseeb.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "65_Drop_Sp_DepartmentWiseTracking by Haseeb.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "66_Create_Sp_DepartmentWiseTracking by Haseeb.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.0\" & "67_AliFaisal_Services_SecurityRights.sql")
                UpdateDbVersion("3.0.3.0")
                ProgBar()
            End If

            If CurrentVersion < 3031 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 14
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.1\" & "1_Ameen_CreatedColumn_Parent_Id_To_tblCOAMainSubSubDetail-02-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.1\" & "2_Ameen_AddedColumn_CompanyLocation_To_tblCompanyContacts-05-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.1\" & "3_Ameen_AddedColumn__DispatchToLocation_To_SalesOrderMasterTable-05-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.1\" & "4_Ameen_AddedColumn_InvoiceToLocation_To_SalesOrderMasterTable-05-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.1\" & "5_Ameen_AddedColumn_TechnicalDrawingNumber_To_SalesOrderMasterTable-05-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.1\" & "6_Ameen_AddedColumn_TechnicalDrawingDate_To_SalesOrderMasterTable-05-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.1\" & "7_Ameen_AddedColumn_AccountsRemarks_To_SalesOrderMasterTable-05-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.1\" & "8_Ameen_AddedColumn_StoreRemarks_To_SalesOrderMasterTable-05-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.1\" & "9_Ameen_AddedColumn_ProductionRemarks_To_SalesOrderMasterTable-05-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.1\" & "10_Ameen_AddedColumn_ServicesRemarks_To_SalesOrderMasterTable-05-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.1\" & "11_Ameen_AddedColumn_SalesRemarks_To_SalesOrderMasterTable-05-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.1\" & "12_Ameen_AddedColumn_TermOfPayments_To_SalesOrderMasterTable-06-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.1\" & "13_Ameen_Drop_SP_SalesOrder-06-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.1\" & "14_Ameen_Altered_SP_SalesOrder-06-09-2016.sql")
                UpdateDbVersion("3.0.3.1")
                ProgBar()
            End If

            If CurrentVersion < 3032 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 14
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "1_Haseeb_AddColumn_Arrival_Time_Departure_Time on 07092016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "2_Haseeb_AddColumn_Gross Tray Net_Weight on 07092016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "3_Haseeb_AddColumn_SalesMasterTable_Arrival_Time_Departure_Time on 07092016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "4_Haseeb_AddColumn_SalesDetailTable_Gross Tray Net_Weights on 07092016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "5.1_AddConfigValues_Currency_on07092016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "5.2_AddConfigValues_DataBaseBackupPassword_on07092016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "5_Haseeb_AddConfigValues For DC on 07092016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "6_Haseeb_AddConfigValues For Sales on 07092016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "7_Drop_sp_CashnBank.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "8_sp_CashnBank.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "9_Ameen_Drop_SP_rptItemWiseSales-08-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "10_Ameen_Altered_SP_rptItemWiseSales-08-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "11_Ameen_Drop_Sp_rptItemWiseSalesConsolidate-08-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "12_Ameen_Altered_Sp_rptItemWiseSalesConsolidate-08-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "13_Ameen_AddedAttachmentRights_For_frmSales-07-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "14_AliFaisal_Drop_SP_DeliveryChallanDetail on 10092016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.2\" & "15_AliFaisal_Create_SP_DeliveryChallanDetail on 10092016.sql")
                UpdateDbVersion("3.0.3.2")
                ProgBar()
            End If

            If CurrentVersion < 3033 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 17
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "1_Ameen_CreatedTable_PlanTicketsMaster-09-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "2_Ameen_CreatedTable_PlanTicketsDetail-09-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "3_Ameen_AddedColumn_PlanTicketsDetailID_To_PlanDetailTable-17-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "4_Ameen_AddedColumn_TicketIssuedQty_To_PlanDetailTable-17-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "5_Ameen_AddedSecurityRightsForfrmPlanTickets-16-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "6_Ameen_Drop_SP_PlanTicketsReport-19-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "7_Ameen_Created_SP_PlanTicketsReport-19-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "8_Ameen_DropView_VT_MaterialAnalysis_AvailableStock_Quantity-21-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "9_Ameen_ModifiedView_VT_MaterialAnalysis_AvailableStock_Quantity-21-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "10_CreateTable_Notes_17-sept-16.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "11_AliFaisal_Drop_SP_Ticket_Tracking-23-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "12_AliFaisal_Created_SP_Ticket_Tracking-23-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "13_AliFaisal_Drop_SP_Ticket_Tracking_DateRange-23-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "14_AliFaisal_Created_SP_Ticket_Tracking_DateRange-23-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "15_Ameen_AddedColumn_DepartmentID_To_DepartmentWiseProductionDetail-23-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "16_AliFaisal_Drop_SP_DepartmentWiseTracking-23-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.3\" & "17_AliFaisal_Created_SP_DepartmentWiseTracking-23-09-2016.sql")
                UpdateDbVersion("3.0.3.3")
                ProgBar()
            End If

            If CurrentVersion < 3034 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 20
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "1_Ameen_AddedColumn_CostCentre_To_tblDefEmployee-26-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "2_Ameen_AddedColumn_CostCentre_To_AdvanceRequestTable-27-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "3_Ameen_Drop_SP_Employee_Attendance-26-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "4_Ameen_Altered_SP_Employee_Attendance-26-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "5_Ameen_Drop_SP_AllItemWiseLC-21-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "6_Ameen_Created_SP_AllItemWiseLC-21-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "7_Ameen_Drop_SP_ItemWiseLC-21-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "8_Ameen_CreatedSP_SP_ItemWiseLC-21-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "9_Ameen_Drop_EmployeesView-26-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "10_Ameen_Modified_EmployeesView-26-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "11_Ameen_Drop_SP_EmpAttendance-27-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "12_Ameen_Modified_SP_EmpAttendance_ByAdding_CostCentreColumn-27-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "13_Ameen_Drop_SP_EmpAttendanceDetailByIn-27-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "14_Ameen_Modified_SP_EmpAttendanceDetailByIn_By_AddingCostCentreField-27-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "15_Ameen_DROP_SP_SalarySheetDetail-27-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "16_Ameen_Modified_SP_SalarySheetDetail_ByAdding_CostCentre-27-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "17_Ameen_Drop_spEmployeeOverTime-28-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "18_Ameen_Modified_spEmployeeOverTime-28-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "19_Ameen_Drop_SP_ItemWiseLC-29-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.4\" & "20_Ameen_Modified_SP_ItemWiseLC-29-09-2016.sql")
                UpdateDbVersion("3.0.3.4")
                ProgBar()
            End If

            If CurrentVersion < 3035 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 15
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "01_Ameen_AddedColumn_ReferencceNo_To_DepartmentWiseProduction-30-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "02_Ameen_Drop_ArticleHistory_BySize-28-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "03_Ameen_Modified_ArticleHistory_BySize_ByAdding_ArticleSize&Sz7-28-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "04_Ameen_Drop_DepartmentWiseTracking-30-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "05_Ameen_Modified_DepartmentWiseTracking_ByAdding_ReferenceNo_And_ProductionDate-30-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "06_Ameen_Drop_SP_ItemWiseLC-29-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "07_Ameen_Modified_SP_ItemWiseLC-29-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "08_Waqar-GatepassDetailTable-Comment.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "09_Haseeb_AddColumn in LCDetailTable-6-oct-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "10_DropProcedure_SP_CompanyContacts_26-sept-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "11_CreateProcedure_SP_CompanyContacts_26-sept-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "12_Haseeb_AlterTable_AddColumns_SalesOrderMasterTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "13_Haseeb_CreateTable_CommunicationLog 26-sept-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "14_DropProcedure_SP_DashboardAttendance 26-sept-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "15_CreateProcedure_SP_DashboardAttendance 26-sept-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "16_Ameen_Drop_ArticleHistory_BySize-07-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.5\" & "17_Ameen_Modifided_SP_ArticleHistory_BySize-07-10-2016.sql")
                UpdateDbVersion("3.0.3.5")
                ProgBar()
            End If

            If CurrentVersion < 3036 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.6\" & "1_Waqar_AlterTable_InstallmentWitnessOne_AddColumn_Phone.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.6\" & "2_Waqar_AlterTable_InstallmentWitnessTwo_AddColumn_Phone.sql")
                UpdateDbVersion("3.0.3.6")
                ProgBar()
            End If

            If CurrentVersion < 3037 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 3
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("3.0.3.7")
                ProgBar()
            End If

            If CurrentVersion < 3038 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.8\" & "1_AddConfigValues_ImportWeightWiseCalculation_by Haseeb on 18-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.8\" & "2_AddColumns_LCDetailTable_by Haseeb on 18-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.8\" & "3_Ameen_AddedColumns_Currency_To_SalesDetailTable-17-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.8\" & "4_Ameen_AddedColumns_Currency_To_SalesReturnDetailTable-17-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.8\" & "5_Ameen_AddedColumns_Currency_To_ReceivingDetailTable-17-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.8\" & "6_Ameen_AddedColumns_Currency_To_PurchaseReturnDetailTable-17-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.8\" & "7_AddColumns_LCMasterTable_by Haseeb on 18-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.8\" & "8_AliFaisal_Drop_SP_ItemWiseLC-on-19102016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.8\" & "9_AliFaisal_Create_SP_ItemWiseLC-on-19102016.sql")
                UpdateDbVersion("3.0.3.8")
                ProgBar()
            End If

            If CurrentVersion < 3039 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 4
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.9\" & "1_Ameen_Drop_SP_EmployeeSalarySheet-24-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.9\" & "2_Ameen_Modified_SP_EmployeeSalarySheet-24-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.9\" & "3_Ameen_Drop_SP_LocationWiseStockLedger-24-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.3.9\" & "4_Ameen_Modified_SP_LocationWiseStockLedger_For_SettingDateSortOrder-24-10-2016.sql")
                UpdateDbVersion("3.0.3.9")
                ProgBar()
            End If

            If CurrentVersion < 3040 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 15
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.0\" & "1_Ameen_Added_Column_PlanId_To_AllocationMaster-31-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.0\" & "2_Ameen_Added_Column_SalesOrderId_To_AllocationMaster-31-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.0\" & "3_Ameen_AlteredColumnQuantityOfAllocationDetailDataType_To_Float-31-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.0\" & "4_Ameen_Drop_SP_SP_MaterialAnalysisQuanityCalculationLatest-31-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.0\" & "5_Ameen_Created_SP_SP_MaterialAnalysisQuanityCalculationLatest-31-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.0\" & "6_Drop_SP_ProductionPlaningItemWise_26-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.0\" & "7_Create_SP_ProductionPlaningItemWise_26-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.0\" & "8_Drop_View_Vw_ItemPlan_26-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.0\" & "9_Create_View_Vw_ItemPlan_26-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.0\" & "10_Drop_SP_Plan_26-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.0\" & "11_Create_SP_Plan_26-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.0\" & "12_Add_SecurityRights_frmLeads.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.0\" & "13_Update_DocType_PUR_InwardExpenseDetailTable.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.0\" & "14_Update_DocType_SI_OutwardExpenseDetailTable.sql")
                UpdateDbVersion("3.0.4.0")
                ProgBar()
            End If

            If CurrentVersion < 3041 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.1\" & "1_Ameen_AlteredColumn_Per_Kg_Cost_To_LcDetail-07-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.1\" & "2_Ameen_AlteredColumn_Weight_To_LcDetail-07-11-2016.sql")
                UpdateDbVersion("3.0.4.1")
                ProgBar()
            End If

            If CurrentVersion < 3042 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 9
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.2\" & "1_Create_Table_Locker_Notification_tblLicenseInformation.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.2\" & "2_Add_Column_AccessLevel_to_tblCOAMainSubSubDetails.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.2\" & "3_Drop_Procedure_Sp_GetLockerSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.2\" & "4_Create_Procedure_Sp_GetLockerSummary.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.2\" & "5_Amen_AddedColumn_AllocationDetailId_To_DispatchDetailTable-04-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.2\" & "6_Amen_IssuedQuantity_AllocationDetail-02-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.2\" & "7_Drop_View_vwCOADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.2\" & "8_Create_View_vwCOADetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.2\" & "9_Add_Column_in_VoucherDetail.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.2\" & "10_Ameen_Drop_SP_LocationWiseStockLedger-08-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.2\" & "11_Ameen_Altered_SP_LocationWiseStockLedger-08-09-2016.sql")
                UpdateDbVersion("3.0.4.2")
                ProgBar()
            End If

            If CurrentVersion < 3043 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.3\" & "1_Ameen_Drop_View_[dbo].[ArticleDefView]-10112016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.3\" & "2_Ameen_Modified_ArticleDefView_For_ArticleCategory-10112016.sql")
                UpdateDbVersion("3.0.4.3")
                ProgBar()
            End If
            If CurrentVersion < 3044 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.4\" & "1_Ameen_Drop_SP_StoreIssuencAgainstTicket.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.4\" & "2_Ameen_Created_SP_StoreIssuenceAgainstTicket-09112016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.4\" & "3_Ameen_AddedColumn_DispatchDetailId_To_ReturnDispatchDetailTable-11-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.4\" & "4_Ameen_AddedColumn_ReturnedQty_DispatchDetailTable-11-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.4\" & "5_Ameen_AddedColumn_ReturnedTotalQty_DispatchDetailTable-11-11-2016.sql")
                UpdateDbVersion("3.0.4.4")
                ProgBar()
            End If
            If CurrentVersion < 3045 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("3.0.4.5")
                ProgBar()
            End If
            If CurrentVersion < 3046 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 1
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                UpdateDbVersion("3.0.4.6")
                ProgBar()
            End If
            If CurrentVersion < 3047 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.7\" & "1_Ameen_AddedColumn_SalesOrderDetailId_To_PurchaseDemandDetailTable-14-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.7\" & "2_Ameen_Drop_SP_LocationWiseStockLedger-17-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.7\" & "3_Ameen_Altered_SP_LocationWiseStockLedger-08-09-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.7\" & "4_Ameen_Drop_SP_DispatchStatus-15-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.7\" & "5_Ameen_Modifed_SP_DispatchStatus-15-11-2016.sql")
                UpdateDbVersion("3.0.4.7")
                ProgBar()
            End If
            If CurrentVersion < 3048 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 10
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.8\" & "01_Drop_SP_SummaryOfSalesTaxInvoices-18-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.8\" & "02_Ameen_Altered_SP_SummaryOfSalesTaxInvoices-18-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.8\" & "03_Drop_rptItemWiseSales-18-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.8\" & "04_Ameen_Modified_rptItemWiseSales-18-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.8\" & "05_Drop_rptItemWiseSalesConsolidate-18-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.8\" & "06_Ameen_Modified_rptItemWiseSalesConsolidate-18-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.8\" & "07_Ameen_Drop_SP_SalesCertificateLedger-23-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.8\" & "08_Ameen_Modified_SP_SalesCertificateLedger23-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.8\" & "09_Ameen_Drop_Sp_BalanceSheetFormated-20-10-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.8\" & "10_Ameen_Modified_SP_BalanceSheetFormated_By_Adding_CostCentreFilter-20-10-2016.sql")
                UpdateDbVersion("3.0.4.8")
                ProgBar()
            End If
            If CurrentVersion < 3049 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.9\" & "1_Ameen_AlteredColumn_OverTime_StartTime_To_ShiftTable_25-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.4.9\" & "2_AliFaisal_AddColumns_ContactNameNo_to_tblCustomer.sql")
                UpdateDbVersion("3.0.4.9")
                ProgBar()
            End If
            If CurrentVersion < 3050 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.5.0\" & "1_DROP_SP_SummaryOfSalesTaxInvoices28-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.5.0\" & "2_SP_SummaryOfSalesTaxInvoices-25-11-2016.sql")
                UpdateDbVersion("3.0.5.0")
                ProgBar()
            End If

            If CurrentVersion < 3051 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.5.1\" & "1_AddColumn_CompanyLocationId.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.5.1\" & "2_CreateTables_tblDefContactGroups.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.5.1\" & "3_InsertValuestoTables_tblDefCompanyLocationType.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.5.1\" & "4_InsertValuestoTables_tblDefContactGroups.sql")
                UpdateDbVersion("3.0.5.1")
                ProgBar()
            End If

            If CurrentVersion < 3052 Then
                ProgBarOrverAllMaxVal += 2
                ProgBarVal = 2
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.5.2\" & "01_Ameen_AddedColumn_EngineNo_To_tblVoucherDetail-30-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.5.2\" & "02_Ameen_AddedColumn_ChassisNo_To_tblVoucherDetail-30-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.5.2\" & "03_Drop_SP_SalesCertificateLedger-30-11-2016.sql")
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.5.2\" & "04_Ameen_Modified_SP_SalesCertificateLedger-30-11-2016.sql")
                UpdateDbVersion("3.0.5.2")
                ProgBar()
            End If

            If CurrentVersion < 3059 Then
                ProgBarOrverAllMaxVal += 5
                ProgBarVal = 5
                Me.ProgressBar1.Value = 0
                Me.ProgressBar1.Maximum = ProgBarVal
                executQuery(str_ApplicationStartUpPath & "\Schema Updates\3.0.5.9\" & "1_AhmadRaza_AddRights_frmSales_ReceiptVoucherPost.sql")
                UpdateDbVersion("3.0.5.9")
                ProgBar()
            End If


            If CurrentVersion > 3059 AndAlso CurrentVersion < 6000 Then
                Me.UpdateVersionV4(Me.txtDBVersion.Text)
            End If

            Return True
        Catch ex As Exception
            btnUpdate.Visible = True
            Throw ex
        Finally
            ProgressBar1.Value = ProgressBar1.Maximum
            ProgressBar2.Maximum = 2
            Me.ProgressBar2.Value = ProgressBar2.Maximum
            Me.ProgressBar2.Update()

            If Con.State = ConnectionState.Open Then Con.Close()
        End Try
    End Function


    Public Function UpdateDbVersion(ByVal VersionNumber As String) As Boolean
        Dim Con As New SqlClient.SqlConnection(SBDal.SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Dim Cmd As New SqlClient.SqlCommand
        Cmd.Connection = Con
        Cmd.Transaction = trans
        Try
            Dim strSQL As String = String.Empty
            strSQL = "UPDATE ConfigValuesTable SET Config_Value=N'" & VersionNumber & "' WHERE Config_Type=N'Version'"
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strSQL
            Cmd.ExecuteNonQuery()
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Function executQuery2(ByVal FileName As String, ByRef cm As SqlClient.SqlCommand) As Boolean
        'Dim objCon As New SqlClient.SqlConnection(SBDal.SQLHelper.CON_STR)
        Try

            'If Not objCon.State = ConnectionState.Open Then
            'objCon.Open()
            'End If

            'Dim cm As New SqlClient.SqlCommand
            'cm.Connection = objCon

            Me.lblprogress.Text = String.Empty
            Me.lblprogress.Text = FileName
            Application.DoEvents()

            'If Not Con.State = ConnectionState.Open Then
            '    Con.Open()
            'End If
            cm.CommandTimeout = 1500
            cm.CommandText = File.OpenText(FileName).ReadToEnd
            cm.ExecuteNonQuery()

            If Me.ProgressBar1.Maximum < Me.ProgressBar1.Value + 1 Then Me.ProgressBar1.Value += 1
            Application.DoEvents()

        Catch ex As Exception
            Throw New Exception(FileName & Chr(10) & ex.Message)
        Finally
            'objCon.Close()
        End Try
    End Function

    Function executQuery(ByVal FileName As String) As Boolean
        Dim objCon As New SqlClient.SqlConnection(SBDal.SQLHelper.CON_STR)
        Try

            If Not objCon.State = ConnectionState.Open Then
                objCon.Open()
            End If

            Dim cm As New SqlClient.SqlCommand
            cm.Connection = objCon

            Me.lblprogress.Text = String.Empty
            Me.lblprogress.Text = FileName
            Application.DoEvents()

            'If Not Con.State = ConnectionState.Open Then
            '    Con.Open()
            'End If
            cm.CommandTimeout = 1500
            cm.CommandText = File.OpenText(FileName).ReadToEnd
            cm.ExecuteNonQuery()

            If Me.ProgressBar1.Maximum < Me.ProgressBar1.Value + 1 Then Me.ProgressBar1.Value += 1
            Application.DoEvents()

        Catch ex As Exception
            Throw New Exception(FileName & Chr(10) & ex.Message)
        Finally
            objCon.Close()
        End Try
    End Function
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Private Sub ProgBar()
        Try
            Me.ProgressBar2.Minimum = 1
            Me.ProgressBar2.Maximum = DBVer
            ProgressBar2.Step = ProgBarOrverAllMaxVal
            Me.ProgressBar2.PerformStep()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function CheckRights() As Boolean
        Try
            Dim str As String = "Select * From tblRights "
            Dim dt As DataTable = GetDataTable(str)
            If Not dt Is Nothing Then
                If dt.Rows.Count = 0 Then
                    Return False
                Else
                    Return True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmReleaseUpdate_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            btnUpdate_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbReleaseLog_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbReleaseLog.SelectedIndexChanged, cmbVersionto.SelectedIndexChanged
        Try
            If Me.cmbReleaseLog.SelectedIndex = -1 Then Exit Sub
            Dim strSQL As String = "Select 0 as Serial_No, Release_Module, Release_Version, Release_Detail, Status From Release_Log WHERE Release_Version BETWEEN '" & Me.cmbReleaseLog.SelectedValue & "' AND  '" & Me.cmbVersionto.SelectedValue & "' Group By Release_Module,Release_Version, Release_Detail, Status ORDER BY Release_Version DESC"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            Dim int As Int32 = 1I
            If dt IsNot Nothing Then
                For Each r As DataRow In dt.Rows
                    r.BeginEdit()
                    r("Serial_No") = int
                    r.EndEdit()
                    int += 1
                Next
            End If
            Me.grdSaved.DataSource = Nothing
            Me.grdSaved.DataSource = dt
            Me.grdSaved.AutoSizeColumns()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Me.BackgroundWorker1.ReportProgress(1)
            CreateCurrentDBBackup()
            Me.BackgroundWorker1.ReportProgress(2)

        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
            Me.BackgroundWorker1.ReportProgress(3)
        End Try
    End Sub
    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        Try

            If e.ProgressPercentage = 1 Then
                Me.lblprogress.Text = "Creating database backup."
            ElseIf e.ProgressPercentage = 2 Then
                Me.lblprogress.Text = "Database backup created successfully."
            ElseIf e.ProgressPercentage = 3 Then
                Me.lblprogress.Text = "Unsuccessfull database backup creation."
            End If

        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Sep2015ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Nov2015ToolStripMenuItem7.Click, Dec2015ToolStripMenuItem.Click, Dec2015ToolStripMenuItem1.Click, Dec2015ToolStripMenuItem2.Click, ToolStripMenuItem3.Click, ToolStripMenuItem4.Click, ToolStripMenuItem5.Click, ToolStripMenuItem6.Click, Feb2016ToolStripMenuItem.Click, Feb2016ToolStripMenuItem1.Click, Feb2016ToolStripMenuItem2.Click, Feb2016ToolStripMenuItem3.Click, Feb2016ToolStripMenuItem4.Click, Feb2016ToolStripMenuItem5.Click, Feb2016ToolStripMenuItem6.Click, Feb2016ToolStripMenuItem7.Click, Mar2016ToolStripMenuItem.Click, Mar2016ToolStripMenuItem1.Click, Apr2016ToolStripMenuItem.Click, Apr2016ToolStripMenuItem1.Click, Apr2016ToolStripMenuItem2.Click
        Try
            If IO.File.Exists(Application.StartupPath & "\Schema Updates\" & CType(sender, ToolStripMenuItem).Tag.ToString & "\Release notes for version " & CType(sender, ToolStripMenuItem).Tag.ToString.Replace(".", "") & ".pdf") Then
                Process.Start(Application.StartupPath & "\Schema Updates\" & CType(sender, ToolStripMenuItem).Tag.ToString & "\Release notes for version " & CType(sender, ToolStripMenuItem).Tag.ToString.Replace(".", "") & ".pdf")
            End If
        Catch ex As Exception
            msg_Error("An error occured while opening the file" & Chr(10) & Chr(10) & "Please check you have PDF file reader installed on your system")
        End Try
    End Sub
    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        Try
            'frmModProperty.frmModProperty_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function IsValidateVersion3() As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Select Count(Case When Cont > 0 then Cont else 0 end) From(Select Count(*) Cont From SalesOrderMasterTable Group By SalesOrderNo Having Count(*) > 1" _
                    & "  Union All " _
                    & " Select Count(*) Cont From QuotationMasterTable Group By QuotationNo Having Count(*) > 1" _
                    & " Union All " _
                    & " Select Count(*) Cont From DeliveryChalanMasterTable Group By DeliveryNo Having Count(*) > 1" _
                    & " Union All " _
                    & " Select Count(*) Cont From SalesMasterTable Group By SalesNo Having Count(*) > 1" _
                    & " Union All " _
                    & " Select Count(*) Cont From SalesReturnMasterTable Group By SalesReturnNo Having Count(*) > 1" _
                    & " Union All " _
                    & " Select Count(*) Cont From PurchaseOrderMasterTable Group By PurchaseOrderNo Having Count(*) > 1" _
                    & " Union All " _
                    & " Select Count(*) Cont From PurchaseDemandMasterTable Group By PurchaseDemandNo Having Count(*) > 1" _
                    & " Union All " _
                    & " Select Count(*) Cont From ReceivingNoteMasterTable Group By ReceivingNo Having Count(*) > 1" _
                    & " Union All " _
                    & " Select Count(*) Cont From ReceivingMasterTable Group By ReceivingNo Having Count(*) > 1" _
                    & " Union All " _
                    & " Select Count(*) Cont From PurchaseReturnMasterTable Group By PurchaseReturnNo Having Count(*) > 1" _
                    & " Union All " _
                    & " Select Count(*) Cont From ProductionMasterTable Group By Production_No Having Count(*) > 1" _
                    & " Union All " _
                    & " Select Count(*) Cont From DispatchMasterTable Group By DispatchNo Having Count(*) > 1" _
                    & " Union All " _
                    & " Select Count(*) Cont From ReturnDispatchMasterTable Group By ReturnDispatchNo Having Count(*) > 1" _
                    & " Union All " _
                    & " Select Count(*) Cont From StockAdjustmentMaster Group By Doc_No Having Count(*) > 1" _
                    & " Union All " _
                    & " Select Count(*) Cont From WarrantyClaimMasterTable Group By DocNo Having Count(*) > 1" _
                    & " Union All " _
                    & " Select Count(*) Cont From tblVoucher Group By voucher_no Having Count(*) > 1 Union All Select Count(*) From StockDetailTable where LocationId not in(Select Location_Id From tblDefLocation)) as bcs "

            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub ToolStripDropDownButton1_Click(sender As Object, e As EventArgs) Handles ToolStripDropDownButton1.Click
        
    End Sub

    Private Sub May2016ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles May2016ToolStripMenuItem.Click
        Try
            If IO.File.Exists(Application.StartupPath & "\Schema Updates\" & CType(sender, ToolStripMenuItem).Tag.ToString & "\Release notes for version " & CType(sender, ToolStripMenuItem).Tag.ToString.Replace(".", "") & ".pdf") Then
                Process.Start(Application.StartupPath & "\Schema Updates\" & CType(sender, ToolStripMenuItem).Tag.ToString & "\Release notes for version " & CType(sender, ToolStripMenuItem).Tag.ToString.Replace(".", "") & ".pdf")
            End If
        Catch ex As Exception
            msg_Error("An error occured while opening the file" & Chr(10) & Chr(10) & "Please check you have PDF file reader installed on your system")
        End Try
    End Sub

    Private Sub btnReleaseDownload_Click(sender As Object, e As EventArgs) Handles btnReleaseDownload.Click
        Try
            Dim Release As New frmReleaseDownload
            Release.WindowState = FormWindowState.Normal
            Release.TopMost = True
            Release.ShowDialog()
            Release.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ReleaseNotesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseNotesToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-4-0-5-6"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release4060ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release4060ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-4-0-6-0"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release4065ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release4065ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-4-0-6-5"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Function UpdateVersionV4(ByVal CurrentVersion As String) As Boolean
        'Function added by Syed Irfan Ahmad on 28 Feb 2018, Task No: 2620
        '
        'This function executes all scripts from current version to the last defined version of V4. Release versions
        'are define in the form-level array "Relv4"
        'Returns True on success, False otherwise
        '
        '!!!!! Important !!!!!
        'If value of "CurrentVersion" parameter is empty, then update of V4 will start from the first release version of V4

        Me.GenCryptoKeys(ENCRYPTION_KEY_FOR_RELEASE_SCRIPT_FILES)    '<<<< Encryption/decrption wont work without these keys

        Try

            'Keeping in view the release version in "CurrentVersion" parameter, figure out the next release version
            'and its index in the Relv4 array

            Dim IndexOfNextReleaseVersionToUpdate As Integer
            If CurrentVersion = "" Or IsDBNull(CurrentVersion) Or CurrentVersion Is Nothing Then
                IndexOfNextReleaseVersionToUpdate = 0  'start release update from the 1st release of version 4
            Else
                IndexOfNextReleaseVersionToUpdate = Me.FindReleaseIndexInArray(CurrentVersion) + 1
                If IndexOfNextReleaseVersionToUpdate < 1 Then
                    'Current release version not found in the array
                    Throw New Exception("Release version " & CurrentVersion & " could not be recoganized")
                End If
            End If


            Dim ScriptFilesCountFromNextReleaseTillLastRelease = CountScriptFilesFromNextReleaseTillLastRelease(IndexOfNextReleaseVersionToUpdate)

            'Update properties of main progress bar
            ProgressBar2.Minimum = 0
            ProgressBar2.Maximum = ScriptFilesCountFromNextReleaseTillLastRelease
            ProgressBar2.Value = 0

            Dim ScriptFileNames As New ArrayList()  'To contain names of script files
            Dim ScriptFilesPrefix As String  'Prefix of script files

            'Dim ConnectionString As String = "Server=DESKTOP-EVMIQV3;Database=V3;User Id=sa;Password=sa2012"
            Dim DBCon As New SqlClient.SqlConnection(SBDal.SQLHelper.CON_STR)
            If DBCon.State <> ConnectionState.Open Then
                DBCon.Open()
            End If

            Dim cmd As New SqlClient.SqlCommand
            cmd.Connection = DBCon
            cmd.CommandTimeout = 10  '10 seconds

            'Calculate the number of one-dimensional items in the array to use in loop
            Dim ArrayLength As Integer = (Me.Relv4.Length / 2) - 1

            For i As Integer = IndexOfNextReleaseVersionToUpdate To ArrayLength
                ScriptFilesPrefix = Me.Relv4(i, 1)  'Get the prefix
                If Me.GetScriptFileNames(ScriptFilesPrefix, ScriptFileNames) Then
                    If ScriptFileNames.Count > 0 Then
                        If Me.ExecuteScripts(ScriptFileNames, cmd) = False Then
                            'Some error has occured executing a script
                            If DONT_EXECUTE_FURTHER_IF_ANY_SCRIPT_CUASES_ERROR = True Then
                                'Return from here so that no further script is executed
                                Return False
                            End If
                        End If
                    End If
                End If

                'Update the DB version
                Me.UpdateDbVersion(Me.Relv4(i, 0))
            Next

            cmd.Dispose()
            DBCon.Close()

            Return True

        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Private Function FindReleaseIndexInArray(ByVal ReleaseVerToFind As String) As Integer
        'Function added by Syed Irfan Ahmad on 28 Feb 2018, Task No: 2620
        'Finds the first occurance of "ReleaseVerToFind" in the two dimensional array and returns the index.
        'Returns -1 if not found

        Try
            'Calculate the number of one-dimensional items in the array to use in loop
            Dim ArrayLength As Integer = (Me.Relv4.Length / 2) - 1

            For i As Integer = 0 To ArrayLength
                If Me.Relv4(i, 0) = ReleaseVerToFind Then
                    Return i
                End If
            Next

            Return -1  'Indicating not found case

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function ExecuteScripts(ByVal ScriptFileNames As ArrayList, ByRef cmd As SqlClient.SqlCommand) As Boolean
        'Function added by Syed Irfan Ahmad on 28 Feb 2018, Task No: 2620
        'Helping Links:
        '1. GetResourceContent - Retrieving the content of an embedded text file
        '   http://www.devx.com/vb2themax/Tip/19589

        Dim ScriptFile As String = ""

        Try
            Dim DecryptedContents As String = ""

            Dim ScriptFilesCount As Integer = ScriptFileNames.Count

            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = ScriptFilesCount
            ProgressBar1.Value = 0

            Dim Script As String = ""
            Dim ScriptLength As Integer = 0

            For Each ScriptFile In ScriptFileNames
                lblprogress.Text = ScriptFile

                Dim ResourceStream As System.IO.Stream = SchemaUpdaterV4.Utils.GetManifestResourceStream(ScriptFile)
                Dim reader As New System.IO.StreamReader(ResourceStream)

                Script = reader.ReadToEnd()
                DecryptedContents = Me.DecryptData(Script)
                cmd.CommandText = DecryptedContents
                cmd.ExecuteNonQuery()

                If ProgressBar1.Value < ProgressBar1.Maximum Then
                    ProgressBar1.Value += 1 'Progress bar for one release script files
                End If

                If ProgressBar2.Value < ProgressBar2.Maximum Then
                    ProgressBar2.Value += 1 'Main progress bar
                End If

                Application.DoEvents()  'Let application process its own events, if any

            Next

            lblprogress.Text = ""

            Return True

        Catch ex As Exception
            Throw New Exception("An error has occured while executing following script:" & vbCrLf & vbCrLf &
                ScriptFile & vbCrLf & vbCrLf & "Error: " & ex.Message)
        End Try

    End Function

    Private Function GetScriptFileNames(ByVal Prefix As String, ByRef FileList As ArrayList) As Boolean
        'Function added by Syed Irfan Ahmad on 28 Feb 2018, Task No: 2620

        Try

            'Get names of all resources in the assembly
            Dim mrn As String() = SchemaUpdaterV4.Utils.GetManifestResourceNames()

            'Filter resource names by prefix
            Dim ScriptFileNames As String() = Filter(mrn, Prefix, True, CompareMethod.Text)

            FileList.Clear()  'Delete items of ArrayList

            For Each rn As String In ScriptFileNames
                FileList.Add(rn)  'Add resource name in the ArryList
            Next

            FileList.Sort()

            Return True

        Catch ex As Exception
            Const ERROR_CODE As String = "GEC-SUP-0x282-2018"
            Dim gm As New AgriusMessage
            gm.ErrorCode = ERROR_CODE
            gm.Message = "There is somoe issue accessing script files"
            gm.OccuranceDateTime = DateTime.Now
            gm.SourceFunction = "GetScriptFileNames()"
            gm.MessageType = SBUtility.Utility.MessageTypes.Exception
            gm.SourceItem = Me.Name
            gm.Criticality = SBUtility.Utility.MessageCriticality.High
            gm.Details = "Exception: " & ex.Message
            ModGlobel.AgriusMessageLogger.Log(gm)  'Log exception

            Throw New Exception("There is somoe issue accessing script files. Please contact" & _
                   vbCrLf & "SIRIUS Support with the below error code: " & _
                   vbCrLf & vbCrLf & "Error Code: " & ERROR_CODE)
        End Try

    End Function

    Private Sub GenCryptoKeys(ByVal key As String)
        'Function added by Syed Irfan Ahmad on 28 Feb 2018, Task No: 2620

        'This function is copied from the below link:
        'https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/strings/walkthrough-encrypting-and-decrypting-strings
        Try
            ' Initialize the crypto provider.
            TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8)
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function TruncateHash(ByVal key As String, ByVal length As Integer) As Byte()
        'Function added by Syed Irfan Ahmad on 28 Feb 2018, Task No: 2620

        'This function is copied from the below link:
        'https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/strings/walkthrough-encrypting-and-decrypting-strings

        Try
            Dim sha1 As New SHA1CryptoServiceProvider

            ' Hash the key.
            Dim keyBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(key)
            Dim hash() As Byte = sha1.ComputeHash(keyBytes)

            ' Truncate or pad the hash.
            ReDim Preserve hash(length - 1)

            Return hash

        Catch ex As Exception
            Const ERROR_CODE As String = "GEC-SUP-0x833-1576"
            Dim gm As New AgriusMessage
            gm.ErrorCode = ERROR_CODE
            gm.Message = "There is somoe issue with the hash computation"
            gm.OccuranceDateTime = DateTime.Now
            gm.SourceFunction = "EncryptData()"
            gm.MessageType = SBUtility.Utility.MessageTypes.Exception
            gm.SourceItem = Me.Name
            gm.Criticality = SBUtility.Utility.MessageCriticality.High
            gm.Details = "Exception: " & ex.Message
            ModGlobel.AgriusMessageLogger.Log(gm)  'Log exception

            Throw New Exception("There is somoe issue with the hash computation. Please contact" & _
                   vbCrLf & "SIRIUS Support with the below error code: " & _
                   vbCrLf & vbCrLf & "Error Code: " & ERROR_CODE)
        End Try

    End Function

    Private Function EncryptData(ByVal plaintext As String) As String
        'Function added by Syed Irfan Ahmad on 28 Feb 2018, Task No: 2620

        'This function is copied from the below link:
        'https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/strings/walkthrough-encrypting-and-decrypting-strings

        Try
            ' Convert the plaintext string to a byte array.
            Dim plaintextBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(plaintext)

            ' Create the stream.
            Dim ms As New System.IO.MemoryStream
            ' Create the encoder to write to the stream.
            Dim encStream As New CryptoStream(ms, TripleDes.CreateEncryptor(), CryptoStreamMode.Write)

            ' Use the crypto stream to write the byte array to the stream.
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
            encStream.FlushFinalBlock()

            ' Convert the encrypted stream to a printable string.
            Return Convert.ToBase64String(ms.ToArray)

        Catch ex As Exception
            Const ERROR_CODE As String = "GEC-SUP-0x833-1575"
            Dim gm As New AgriusMessage
            gm.ErrorCode = ERROR_CODE
            gm.Message = "There is somoe issue with the contents of a script file"
            gm.OccuranceDateTime = DateTime.Now
            gm.SourceFunction = "EncryptData()"
            gm.MessageType = SBUtility.Utility.MessageTypes.Exception
            gm.SourceItem = Me.Name
            gm.Criticality = SBUtility.Utility.MessageCriticality.High
            gm.Details = "Exception: " & ex.Message
            ModGlobel.AgriusMessageLogger.Log(gm)  'Log exception

            Throw New Exception("There is somoe issue with the contents of a script file. Please contact" & _
                   vbCrLf & "SIRIUS Support with the below error code: " & _
                   vbCrLf & vbCrLf & "Error Code: " & ERROR_CODE)
        End Try

    End Function

    Private Function DecryptData(ByVal encryptedtext As String) As String
        'Function added by Syed Irfan Ahmad on 28 Feb 2018, Task No: 2620

        'This function is copied from the below link:
        'https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/strings/walkthrough-encrypting-and-decrypting-strings

        Try
            ' Convert the encrypted text string to a byte array.
            Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)

            ' Create the stream.
            Dim ms As New System.IO.MemoryStream
            ' Create the decoder to write to the stream.
            Dim decStream As New CryptoStream(ms,
                TripleDes.CreateDecryptor(),
                System.Security.Cryptography.CryptoStreamMode.Write)

            ' Use the crypto stream to write the byte array to the stream.
            decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
            decStream.FlushFinalBlock()

            ' Convert the plaintext stream to a string.
            Return System.Text.Encoding.Unicode.GetString(ms.ToArray)

        Catch ex As Exception
            Const ERROR_CODE As String = "GEC-SUP-0x833-1574"
            Dim gm As New AgriusMessage
            gm.ErrorCode = ERROR_CODE
            gm.Message = "There is somoe issue with the contents of a script file"
            gm.OccuranceDateTime = DateTime.Now
            gm.SourceFunction = "DecryptData()"
            gm.MessageType = SBUtility.Utility.MessageTypes.Exception
            gm.SourceItem = Me.Name
            gm.Criticality = SBUtility.Utility.MessageCriticality.High
            gm.Details = "Exception: " & ex.Message
            ModGlobel.AgriusMessageLogger.Log(gm)  'Log exception

            Throw New Exception("There is somoe issue with the contents of a script file. Please contact" & _
                   vbCrLf & "SIRIUS Support with the below error code: " & _
                   vbCrLf & vbCrLf & "Error Code: " & ERROR_CODE)
        End Try

    End Function

    Private Function CountScriptFilesFromNextReleaseTillLastRelease(ByVal IndexOfNextRelease As Integer) As Integer
        'Function added by Syed Irfan Ahmad on 1 Mar 2018, Task No: 2620
        '
        'Counts the number of script files from next release version till last release version
        Try
            'Calculate the number of one-dimensional items in the array to use in loop
            Dim ArrayLength As Integer = (Me.Relv4.Length / 2) - 1

            Dim ScriptFilesPrefix As String
            Dim FileCount As Integer = 0

            For i As Integer = IndexOfNextRelease To ArrayLength
                ScriptFilesPrefix = Me.Relv4(i, 1)  'Get the prefix
                FileCount = FileCount + Me.CountScriptFilesForAPrefix(ScriptFilesPrefix)
            Next

            Return FileCount

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function CountScriptFilesForAPrefix(ByVal Prefix As String) As Integer
        'Function added by Syed Irfan Ahmad on 1 Mar 2018, Task No: 2620
        '
        'Counts the number of script files matching the prefix

        Try
            'Get names of all resources in the assembly
            Dim mrn As String() = SchemaUpdaterV4.Utils.GetManifestResourceNames()
            'Me.v4.GetManifestResourceNames()

            'Filter resource names by prefix
            Dim ScriptFileNames As String() = Filter(mrn, Prefix, True, CompareMethod.Text)

            Return ScriptFileNames.Length

        Catch ex As Exception
            Throw ex
        End Try


    End Function
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub lblTitle_Click(sender As Object, e As EventArgs) Handles lblHeader.Click

    End Sub

    Private Sub Release5001ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5001ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-0-1/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5002ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5002ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-0-2/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5004ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5004ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-0-4/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5006ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5006ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-0-6/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5007ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5007ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-0-7/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5008ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5008ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-0-8/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5009ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5009ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-0-9/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5010ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5010ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-1-0/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5011ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5011ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-1-1/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5012ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5012ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-1-2/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5013ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5013ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-1-3/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5014ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5014ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-1-4/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5015ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5015ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-1-5/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5016ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5016ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-1-6/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5017ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5017ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-1-7/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5018ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5018ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-1-8/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5019ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5019ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-1-9/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5020ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5020ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-2-0/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5021ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5021ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-2-1/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5022ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5022ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-2-2/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5023ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5023ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-2-3/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5024ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5024ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-2-4/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Release5025ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5025ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-2-5/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5026ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5026ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-2-6/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5027ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Releas5027ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-2-7/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5028ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5028ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-2-8/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Release5029ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5029ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-2-9/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Release5030ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5030ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-3-0/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5031ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5031ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-3-1/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5032ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5032ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-3-2/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5033ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5033ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-3-3/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5034ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5034ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-3-4/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5035ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Release5035ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-3-5/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub Release5036ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5036ToolStripMenuItem1.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-3-6/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5037ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5037ToolStripMenuItem1.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-3-7/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5038ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5038ToolStripMenuItem1.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-3-8/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5039ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5039ToolStripMenuItem1.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-3-9/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5040ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5040ToolStripMenuItem1.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-4-0/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5041ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5041ToolStripMenuItem1.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-4-1/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5042ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5042ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-4-2/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5043ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5043ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-4-3/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Release5044ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5044ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-4-4/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5045ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5045ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-4-5/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5046ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5046ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-4-6/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5047ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5047ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-4-7/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5048ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5048ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-4-8/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub Release5049ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5049ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-4-9/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub Release5050ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5050ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-5-0/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub Release5051ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5051ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-5-1/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5052ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5052ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-5-2/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5053ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5053ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-5-3/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5054ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5054ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-5-4/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5055ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5055ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-5-5/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Release5056ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5056ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-5-6/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5057ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5057ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-5-7/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5058ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5058ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-5-8/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5059ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5059ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-5-9/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5060ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5060ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-6-0/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5061ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5061ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-6-1/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5062ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5062ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-6-2/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5063ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5063ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-6-3/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5064ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5064ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-6-4/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5065ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5065ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-6-5/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5066ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5066ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-6-6/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Release5067ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Release5067ToolStripMenuItem.Click
        Try
            Dim url As String = "http://www.SIRIUS.net/Agrius-release-5-0-6-7/"
            Process.Start(url)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class