
Public Class frmHome
    Dim UiBtn As Janus.Windows.EditControls.UIButton
    Dim _dtpFrom As New DateTimePicker
    Dim _dtpTo As New DateTimePicker
    Dim IsDashBoardRights As Boolean
    Dim IsOpenForm As Boolean = False
    Private _Dt As DataTable
    Private _dtLog As DataTable
    Private _ActivityLogData As DataTable
    Dim blnActivityLogShowOnHomePage As Boolean = False
    Dim blnFirstDateInvoke As Boolean = False
    Private Sub frmHome_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            'If Me.BackgroundWorker2.IsBusy Then Exit Sub
            'BackgroundWorker2.RunWorkerAsync()
            blnFirstDateInvoke = True
            If LoginDashBoardRights = True Then
                '    Me.UltraTabControl1.Tabs(1).Visible = False
                '    Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
                'Else
                frmDashboard.TopLevel = False
                frmDashboard.Dock = DockStyle.Fill
                frmDashboard.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                Me.Panel1.Controls.Add(frmDashboard)
                frmDashboard.Show()
                frmDashboard.BringToFront()
                Me.UltraTabControl1.Tabs(1).Visible = True
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
            Else
                Me.UltraTabControl1.Tabs(1).Visible = False
            End If

            If LoginDashBoardRights = True Then
                'Me.BackgroundWorker1.RunWorkerAsync()
                'Me.CtrlCashBank1.GetCash()
                'Me.CtrlExpense1.GetExpense()
                'Me.CtrlSales1.GetSales()
                'Me.CtrlPurchase1.GetPurchase()
                'Me.CrtlPostDatedCheques1.GetPostDatedCheque()
                'Me.CtrlBalances1.GetBalances()
                'Me.UiStockLevel1.GetStockLevel()
                'Me.UrSalesType1.GetSales()
                'Me.UrPurchaseType1.GetSales()
                'Me.CntrPendingTasks1.GetTasks()

                'Me.UcEmpAttendance1.BackgroundWorker1.RunWorkerAsync()
                'Do While Me.UcEmpAttendance1.BackgroundWorker1.IsBusy
                '    Application.DoEvents()
                'Loop

                'Me.UcEmpAttendance1.BackgroundWorker2.RunWorkerAsync()
                'Do While Me.UcEmpAttendance1.BackgroundWorker2.IsBusy
                '    Application.DoEvents()
                'Loop

                'UcSMSBalance1.ucSMSBalance_Load(Nothing, Nothing)

            End If


            _ActivityLogData = New DataTable
            _ActivityLogData.Columns.Add("LogRecordRefNo", GetType(System.String))
            _ActivityLogData.Columns.Add("Log", GetType(System.String))
            _ActivityLogData.Columns.Add("LogRecordType", GetType(System.String))
            _ActivityLogData.Columns.Add("AccessKey", GetType(System.String))
            'Me.grdHistory.DataSource = _ActivityLogData
            'For c As Integer = 0 To Me.grdHistory.RootTable.Columns.Count - 1
            '    Me.grdHistory.RootTable.Columns(c).AllowSort = False
            'Next

            '''''' Open Survey For User ......
            If Date.Now.Date < CDate("2016-1-1") Then
                Dim str() As String = GetSurvey()
                Dim strStatus As String = String.Empty
                Dim strDate As DateTime = DateTime.Now
                If str.Length > 1 Then
                    strStatus = str(0)
                    Dim blnDate As Boolean = DateTime.TryParse(CDate(str(1)), strDate)
                    If blnDate = True Then
                        strDate = strDate
                    Else
                        strDate = DateTime.Now
                    End If
                End If
                If strStatus = "No" Or strStatus = "" Then
                    If DateTime.Now.Date = strDate.Date Then
                        If My.Computer.Network.IsAvailable Then
                            frmUserServay.ShowDialog()
                            Exit Sub
                        End If
                    End If
                End If
                '....... End Survey .............
            End If

            If bwValidateSystem.IsBusy Then Exit Sub
            bwValidateSystem.RunWorkerAsync()
            Do While bwValidateSystem.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14 

        'Me.lblProgress.Text = "Processing Please Wait ..."
        'Me.lblProgress.BackColor = Color.LightYellow
        'Me.Cursor = Cursors.WaitCursor
        'Me.lblProgress.Visible = True
        'Application.DoEvents()
        'Try
        '    frmHome_Shown(Nothing, Nothing)
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'Finally
        '    Me.lblProgress.Visible = False
        '    Me.Cursor = Cursors.Default
        'End Try
    End Sub
    Private Sub frmHome_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            If BackgroundWorker2.IsBusy Then Exit Sub
            BackgroundWorker2.RunWorkerAsync()
            Do While BackgroundWorker2.IsBusy
                Application.DoEvents()
            Loop
            Dim dt As DataTable = _Dt 'FillRecentForms()
            If Not dt Is Nothing Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If Me.flowPanelHome.Controls.Find(dt.Rows(i).Item("Form_Name").ToString, False).Length = 0 Then
                        UiBtn = New Janus.Windows.EditControls.UIButton
                        UiBtn.Text = dt.Rows(i).ItemArray(0).ToString
                        UiBtn.Name = dt.Rows(i).ItemArray(1).ToString
                        UiBtn.VisualStyle = Janus.Windows.UI.VisualStyle.Office2007
                        UiBtn.Size = New System.Drawing.Size(160, 160)
                        UiBtn.Font = New System.Drawing.Font("Verdana", 12.0!, FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
                        UiBtn.TextVerticalAlignment = Janus.Windows.EditControls.TextAlignment.Center
                        AddHandler UiBtn.Click, AddressOf OpenForm
                        Me.flowPanelHome.Controls.Add(CType(UiBtn, Janus.Windows.EditControls.UIButton))
                    Else
                        Me.flowPanelHome.Controls(i).Visible = True
                    End If
                Next
            End If
            'Me.cmbPeriod.Text = "Today"
            IsOpenForm = True

            If Not getConfigValueByType("ActivityLogShowOnHomePage").ToString = "Error" Then
                blnActivityLogShowOnHomePage = Convert.ToBoolean(getConfigValueByType("ActivityLogShowOnHomePage").ToString)
            End If

            If blnActivityLogShowOnHomePage = True Then
                Me.Timer1.Interval = 60000 '1 Minute
                Me.Timer1.Enabled = True
                'Me.SplitContainer1.Panel2Collapsed = False
            Else
                'Me.SplitContainer1.Panel2Collapsed = True
            End If

        

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function FillRecentForms() As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select TOP 12  LogFormCaption, tblForm.Form_Name, Count(LogFormCaption) as Cont From tblActivityLog INNER JOIN tblForm ON tblForm.Form_Caption = LogFormCaption Group By LogFormCaption, tblForm.Form_Name ORDER BY 3 DESC"
            Return GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub OpenForm(ByVal sender As Object, ByVal e As EventArgs)
        Try

            For Each flwPanel As Control In flowPanelHome.Controls
                If TypeOf flwPanel Is Janus.Windows.EditControls.UIButton Then
                    If flwPanel.Name = sender.Name Then
                        GetFormKey(sender.name)
                    End If
                End If
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetFormKey(ByVal Form_Name As String)
        Try
            Dim str As String = String.Empty
            str = "Select * From tblForm WHERE Form_Name='" & Form_Name & "'"
            Dim dt As DataTable = GetDataTable(str)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    frmMain.LoadControl("" & dt.Rows(0).Item("AccessKey").ToString & "")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmVoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmVoucher.Click
        Try
            frmMain.LoadControl("frmVoucher")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmSales.Click
        Try
            frmMain.LoadControl("RecordSales")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmSalesReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmSalesReturn.Click
        Try
            frmMain.LoadControl("SalesReturn")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmPurchase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmPurchase.Click
        Try
            frmMain.LoadControl("frmPurchase")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmPurchaseReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmPurchaseReturn.Click
        Try
            frmMain.LoadControl("PurchaseReturn")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmStoreIssuence_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmStoreIssuence.Click
        Try
            frmMain.LoadControl("frmStoreIssuence")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmProductionStore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmProductionStore.Click
        Try
            frmMain.LoadControl("ProductionStore")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmDetailAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmDetailAccount.Click
        frmMain.LoadControl("frmDetailAccount")
    End Sub

    Private Sub UiButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmSubSubAccount.Click
        frmMain.LoadControl("frmSubSubAccount")
    End Sub

    Private Sub frmVendorPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmVendorPayment.Click
        Try
            frmMain.LoadControl("VendorPayments")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmCustomerCollection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmCustomerCollection.Click
        Try
            frmMain.LoadControl("CustomerCollection")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmExpense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frmExpense.Click
        Try
            frmMain.LoadControl("frmExpense")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            If IsOpenForm = False Then Exit Sub
            'If Me.cmbPeriod.Text = "Today" Then
            '    Me.DateTimePicker1.Value = Date.Today
            '    Me.DateTimePicker2.Value = Date.Today
            'ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            '    Me.DateTimePicker1.Value = Date.Today.AddDays(-1)
            '    Me.DateTimePicker2.Value = Date.Today.AddDays(-1)
            'ElseIf Me.cmbPeriod.Text = "Current Week" Then
            '    Me.DateTimePicker1.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            '    Me.DateTimePicker2.Value = Date.Today
            'ElseIf Me.cmbPeriod.Text = "Current Month" Then
            '    Me.DateTimePicker1.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            '    Me.DateTimePicker2.Value = Date.Today
            'ElseIf Me.cmbPeriod.Text = "Current Year" Then
            '    Me.DateTimePicker1.Value = New Date(Date.Now.Year, 1, 1)
            '    Me.DateTimePicker2.Value = Date.Today
            'End If
            frmHome_Shown(Nothing, Nothing)
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            _Dt = FillRecentForms()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'If Me.SplitContainer1.Panel2Collapsed = True Then
            '    Me.SplitContainer1.Panel2Collapsed = False
            'Else
            '    Me.SplitContainer1.Panel2Collapsed = True
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            Me.Timer1.Enabled = False
            If Me.BackgroundWorker3.IsBusy Then Exit Sub
            Me.BackgroundWorker3.RunWorkerAsync()
            Do While BackgroundWorker3.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            blnFirstDateInvoke = False
            Me.Timer1.Enabled = True
        End Try
    End Sub
    Private Function GetAllRecords() As DataTable
        Dim myCon As New System.Data.OleDb.OleDbConnection(Con.ConnectionString)
        Dim cmd As New OleDb.OleDbCommand
        Dim trans As OleDb.OleDbTransaction = Nothing
        Try
            Dim strSQL As String = String.Empty
            Dim dtProcess As New DataTable
            dtProcess = GetDataTable("Select Max(ProcessDate) From tblActivityLogProcess WHERE Status='Successful'")
            Dim lastProcessDate As DateTime
            If dtProcess IsNot Nothing Then
                If IsDBNull(dtProcess.Rows(0).Item(0)) Then
                    lastProcessDate = Date.Now.AddMinutes(-5)
                Else
                    lastProcessDate = Convert.ToDateTime(dtProcess.Rows(0).Item(0))
                End If
            End If
            If blnFirstDateInvoke = False Then
                lastProcessDate = lastProcessDate
            Else
                lastProcessDate = Date.Now.ToString("yyyy-M-d 00:00:00")
            End If
            strSQL = "Select DISTINCT LogRecordRefNo, LogActivityName,LogDateTime,IsNull(TotalQty,0) as TotalQty, IsNull(TotalAmount,0) as TotalAmount, AccessKey, User_Name,LogFormCaption, LogComments as Log,LogFormName From tblActivitylog INNER JOIN tblUser ON tblUser.User_ID = tblActivityLog.LogUserId WHERE (Convert(DateTime, LogDateTime, 102) BETWEEN Convert(DateTime,'" & lastProcessDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102) AND Convert(dateTime,GetDate(),102)) AND LogRecordRefNo <> ''"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            _dtLog = New DataTable
            _dtLog = dt

            If myCon.State = ConnectionState.Closed Then myCon.Open()
            trans = myCon.BeginTransaction
            Try
                strSQL = String.Empty
                strSQL = "INSERT INTO tblActivityLogProcess(ProcessDate,Status) VALUES('" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "','Successful')"
                cmd.CommandText = strSQL
                cmd.Connection = myCon
                cmd.Transaction = trans
                cmd.ExecuteNonQuery()
                trans.Commit()
                myCon.Close()
            Catch ex As Exception
                trans.Rollback()
            End Try
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub BackgroundWorker3_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Try
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker3_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker3.RunWorkerCompleted
        Try
            'Dim dt As DataTable = CType(Me.grdHistory.DataSource, DataTable)
            'dt.AcceptChanges()
            'Dim dr As DataRow
            'If _dtLog IsNot Nothing Then
            '    If _dtLog.Rows.Count > 0 Then
            '        For Each r As DataRow In _dtLog.Rows
            '            dr = dt.NewRow
            '            dr(0) = r.Item("LogRecordRefNo").ToString
            '            dr(1) = r.Item("Log").ToString
            '            dr(2) = r.Item("LogActivityName").ToString
            '            dr(3) = r.Item("AccessKey").ToString
            '            dt.Rows.InsertAt(dr, 0)
            '            dt.AcceptChanges()
            '            Application.DoEvents()
            '        Next
            '    End If
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdHistory_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try
            'If e.Column.Key = "Column1" Then
            '    If Me.grdHistory.GetRow.Cells("LogFormName").Value.ToString.Length > 0 Then
            '        Dim Frm As New Form
            '        Frm.Name = Me.grdHistory.GetRow.Cells("LogFormName").Value.ToString
            '        If Not Me.grdHistory.GetRow.Cells("LogActivityName").Value.ToString = "Delete" Or Me.grdHistory.GetRow.Cells("LogActivityName").Value.ToString = "Login" Then
            '            If Frm IsNot Nothing Then
            '                frmMain.LoadControl(Me.grdHistory.GetRow.Cells("AccessKey").Value.ToString)
            '            End If
            '            Frm.Tag = Me.grdHistory.GetRow.Cells("LogRecordRefNo").Value
            '            If Frm.Tag IsNot Nothing Then
            '                frmMain.LoadControl(Me.grdHistory.GetRow.Cells("AccessKey").Value.ToString)
            '            End If
            '        End If
            '    End If
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Public Sub New()

    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub chkIncludeUnPost_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIncludeUnPost.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            Me.ToolStripButton1_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            'If e.Tab.Index = 0 Then
            '    For i As Integer = 0 To Me.ToolStrip1.Items.Count - 1
            '        Me.ToolStrip1.Items(i).Visible = False
            '    Next
            'Else
            '    For i As Integer = 0 To Me.ToolStrip1.Items.Count - 1
            '        Me.ToolStrip1.Items(i).Visible = True
            '    Next
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Public Function GetSurvey() As String()
        Try
            Dim sr As IO.StreamReader
            Dim file As String = str_ApplicationStartUpPath & "\UserSurvey.txt"
            Dim strSurvey() As String = {}
            If IO.File.Exists(file) Then
                sr = IO.File.OpenText(file)
                strSurvey = sr.ReadToEnd().ToString().Split(",")
                sr.Dispose()
                sr.Close()
            End If

            Return strSurvey
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SetSurveyNo() As String()
        Try
            Dim sw As IO.StreamWriter
            Dim file As String = str_ApplicationStartUpPath & "\UserSurvey.txt"
            Dim strSurvey() As String = {}
            If IO.File.Exists(file) Then
                IO.File.Delete(file)
            End If
            If Not IO.File.Exists(file) Then
                sw = IO.File.CreateText(file)
                sw.WriteLine("No" & "," & DateTime.Now.AddDays(2).ToString("yyyy-M-d hh:mm:ss tt") & "," & LoginUserName & "")
                sw.Dispose()
                sw.Close()
            End If
            Return strSurvey
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SetSurveyYes() As String()
        Try
            Dim sw As IO.StreamWriter
            Dim file As String = str_ApplicationStartUpPath & "\UserSurvey.txt"

            If IO.File.Exists(file) Then
                IO.File.Delete(file)
            End If

            Dim strSurvey() As String = {}
            If Not IO.File.Exists(file) Then
                sw = IO.File.CreateText(file)
                sw.WriteLine("Yes" & "," & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "," & LoginUserName & "")
                sw.Dispose()
                sw.Close()
            End If

            Return strSurvey
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub bwValidateSystem_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwValidateSystem.DoWork
        'If ValidateSystem(System.Environment.MachineName.ToString, GetMACAddress()) = False Then
        '    frmTrialVersion.ShowDialog()
        'End If
    End Sub
    Public Function ValidateSystem(ByVal SystemName As String, ByVal SystemID As String) As Boolean
        Dim dt As New DataTable
        Dim query As String = ""
        Dim val As Boolean = False
        Try
            query = "Select * From tblSystemList Where SystemName= '" & SystemName & "' And SystemId = '" & EncryptLicense(SystemID) & "'"
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
End Class