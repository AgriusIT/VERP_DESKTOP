'RID 123    R@!   08-Jun-2013 activity log is performing slow
''TASK TFS1384 done by Ameen applied security rights of Report Print and Report Export on 07-09-2017
Imports System.Data
Imports SBModel

Public Class frmActivityLog
    Private Sub FillCombos()

        'RID 123    R@!   08-Jun-2013 activity log is performing slow

        'Setting from date one month past/before current date
        Me.dtpFrom.Value = Date.Now.Date.AddMonths(-1)

        'Set dates checked to add in criteria by default
        dtpFrom.Checked = True
        dtpTo.Checked = True

        Dim strSQL As String

        strSQL = "Select distinct 0 as ID ,LogApplicationName as [Application] From tblActivityLog " _
        & " ORDER BY [Application]"

        Call FillDropDown(ddlApplication, strSQL)

        strSQL = "Select distinct 0 as ID ,LogFormCaption as [Form Caption] From tblActivityLog " _
        & " ORDER BY [Form Caption]"

        Call FillDropDown(ddlForms, strSQL)

        strSQL = "Select User_ID, FullName,User_Name From tblUser " _
        & " ORDER BY User_Name"
        FillDropDown(Me.ddlUser, strSQL)
        'Dim dt As DataTable = GetDataTable(strSQL)

        'If Not dt Is Nothing Then
        '    If Not dt.Rows.Count = 0 Then
        '        For Each r As DataRow In dt.Rows
        '            r.Item("User_Name") = Decrypt(r.Item("User_Name"))
        '        Next
        '    End If
        'End If

        'Dim dr As DataRow = dt.NewRow
        'dr(0) = 0
        'dr(1) = strZeroIndexItem
        'dt.Rows.InsertAt(dr, 0)
        'Me.ddlUser.ValueMember = "User_ID"
        'Me.ddlUser.DisplayMember = "User_Name"
        'Me.ddlUser.DataSource = dt

        strSQL = "Select distinct 0 as ID ,LogActivityName as [Activity Name] From tblActivityLog " _
        & " ORDER BY [Activity Name]"

        Call FillDropDown(ddlActivity, strSQL)



        ddlRecordType.DataSource = System.Enum.GetNames(GetType(EnumRecordType))



    End Sub


    Private Sub frmActivityLog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Call FillCombos()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ShowActivityLog()
        Try

            Dim strAppliation As String = ""
            Dim strForm As String = ""
            Dim strActivity As String = ""
            Dim intUserID As String = 0
            Dim strRecordType As String = ""
            Dim dtFrom As Date
            Dim dtTo As Date

            Dim strWhere As String

            strWhere = ""

            If ddlApplication.SelectedIndex > 0 Then
                strAppliation = ddlApplication.Text
                strWhere = strWhere & " LogApplicationName = '" & strAppliation & "' "
            End If

            If ddlForms.SelectedIndex > 0 Then
                strForm = ddlForms.Text
                strWhere = strWhere & IIf(strWhere.Length > 0, " AND ", " ")
                strWhere = strWhere & " LogFormCaption = '" & strForm & "' "
            End If


            If ddlActivity.SelectedIndex > 0 Then
                strActivity = ddlActivity.Text
                strWhere = strWhere & IIf(strWhere.Length > 0, " AND ", " ")
                strWhere = strWhere & " LogActivityName = '" & strActivity & "' "
            End If


            If ddlUser.SelectedIndex > 0 Then
                intUserID = ddlUser.SelectedValue
                strWhere = strWhere & IIf(strWhere.Length > 0, " AND ", " ")
                strWhere = strWhere & " LogUserID = '" & intUserID & "' "
            End If


            If ddlRecordType.SelectedIndex > 0 Then
                strRecordType = ddlRecordType.Text
                strWhere = strWhere & IIf(strWhere.Length > 0, " AND ", " ")
                strWhere = strWhere & " LogRecordType = '" & strRecordType & "' "
            End If

            If dtpFrom.Checked Then
                dtFrom = dtpFrom.Value
                strWhere = strWhere & IIf(strWhere.Length > 0, " AND ", " ")
                strWhere = strWhere & " (Convert(varchar,LogDateTime,102) >= Convert(dateTime,'" & dtFrom.ToString("yyyy-M-d 00:00:00") & "',102)) "
            End If

            If dtpTo.Checked Then
                dtTo = dtpTo.Value
                strWhere = strWhere & IIf(strWhere.Length > 0, " AND ", " ")
                strWhere = strWhere & " (Convert(varchar,LogDateTime,102) <= Convert(DateTime,'" & dtTo.ToString("yyyy-M-d 23:59:59") & "',102)) "
            End If


            Dim strSQL As String
            strSQL = "SELECT tblActivityLog.LogApplicationName AS Application, tblActivityLog.LogDateTime AS [Date and Time], tblUser.FullName AS [User Name], tblActivityLog.LogFormCaption AS [Form Caption], tblActivityLog.LogActivityName AS Activity, tblActivityLog.LogRecordType AS [Record Type],   tblActivityLog.LogRecordRefNo AS [Ref No], tblActivityLog.LogSystem as [System Name]  " _
            & " FROM  tblActivityLog INNER JOIN  tblUser ON tblActivityLog.LogUserID = tblUser.User_ID " _
            & IIf(strWhere.Trim.Length > 0, " WHERE " & strWhere, "")
            strSQL = strSQL & "Order By LogDateTime Desc"
            Call FillGridEx(Me.grdLog, strSQL)
            grdLog.RetrieveStructure()
            'If Me.grdLog.GetRows.Length - 1 Then
            'For Each r As DataGridViewRow In grdLog.Rows
            'For Each r As Janus.Windows.GridEX.GridEXRow In grdLog.GetRows
            '    r.BeginEdit()
            '    r.Cells.Item("User Name").Value = Decrypt(r.Cells.Item("User Name").Value)
            '    r.EndEdit()
            'Next
            Me.grdLog.RootTable.Columns("Date and Time").FormatString = "dd/MMM/yyyy"
            'End If

            grdLog.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            'RID 123    R@!   08-Jun-2013 activity log is performing slow
            Call ShowActivityLog()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage("Error occured while showing report: " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub FillGrdActivity()
        'Try
        '    Dim str As String = String.Empty

        '    Dim strAppliation As String = ""
        '    Dim strForm As String = ""
        '    Dim strActivity As String = ""
        '    Dim intUserID As String = 0
        '    Dim strRecordType As String = ""
        '    Dim dtFrom As Date
        '    Dim dtTo As Date

        '    Dim strWhere As String

        '    strWhere = ""

        '    If ddlApplication.SelectedIndex > 0 Then
        '        strAppliation = ddlApplication.Text
        '        strWhere = strWhere & " tblActivityLog.LogApplicationName = '" & strAppliation & "' "
        '    End If

        '    If ddlForms.SelectedIndex > 0 Then
        '        strForm = ddlForms.Text
        '        strWhere = strWhere & IIf(strWhere.Length > 0, " AND ", " ")
        '        strWhere = strWhere & " tblActivityLog.LogFormCaption = '" & strForm & "' "
        '    End If


        '    If ddlActivity.SelectedIndex > 0 Then
        '        strActivity = ddlActivity.Text
        '        strWhere = strWhere & IIf(strWhere.Length > 0, " AND ", " ")
        '        strWhere = strWhere & " tblActivityLog.LogActivityName = '" & strActivity & "' "
        '    End If


        '    If ddlUser.SelectedIndex > 0 Then
        '        intUserID = ddlUser.SelectedValue
        '        strWhere = strWhere & IIf(strWhere.Length > 0, " AND ", " ")
        '        strWhere = strWhere & " tblActivityLog.LogUserID = '" & intUserID & "' "
        '    End If


        '    If ddlRecordType.SelectedIndex > 0 Then
        '        strRecordType = ddlRecordType.Text
        '        strWhere = strWhere & IIf(strWhere.Length > 0, " AND ", " ")
        '        strWhere = strWhere & " tblActivityLog.LogRecordType = '" & strRecordType & "' "
        '    End If

        '    If dtpFrom.Checked Then
        '        dtFrom = dtpFrom.Text & " 00:00:00 AM"
        '        strWhere = strWhere & IIf(strWhere.Length > 0, " AND ", " ")
        '        strWhere = strWhere & " tblActivityLog.LogDateTime >= '" & dtFrom & "' "
        '    End If

        '    If dtpTo.Checked Then
        '        dtTo = dtpTo.Text & " 11:59:59 PM"
        '        strWhere = strWhere & IIf(strWhere.Length > 0, " AND ", " ")
        '        strWhere = strWhere & " tblActivityLog.LogDateTime <= '" & dtTo & "' "
        '    End If

        '    str = "SELECT DISTINCT dbo.tblForm.AccessKey, dbo.tblActivityLog.LogRecordRefNo, dbo.tblActivityLog.LogActivityName, dbo.tblActivityLog.LogComments FROM dbo.tblActivityLog INNER JOIN   dbo.tblForm ON dbo.tblActivityLog.LogFormCaption = dbo.tblForm.Form_Caption WHERE dbo.tblForm.AccessKey Is Not Null AND " & strWhere & ""
        '    'str += "" & IIf(Me.dtpFrom.Checked = True, "AND (Convert(varchar, tblActivityLog.LogDateTime,102) >= Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("yyyy-M-d 00:00:00") & "',102))", "") & ""
        '    'str += "" & IIf(Me.dtpFrom.Checked = True, "AND (Convert(varchar, tblActivityLog.LogDateTime,102) <= Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("yyyy-M-d 23:59:59") & "',102))", "") & ""

        '    Dim dt As DataTable = GetDataTable(str)
        '    dt.AcceptChanges()
        '    Me.GridEX1.DataSource = Nothing
        '    Me.GridEX1.DataSource = dt
        '    GridEX1.RetrieveStructure()
        '    For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
        '        If Not col.Index = 3 Then
        '            col.Visible = False
        '        End If
        '    Next
        '    Me.GridEX1.RootTable.Columns(3).Caption = "Activity Log"
        '    Me.GridEX1.AutoSizeColumns()
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub GridEX1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    frmModProperty.Tags = String.Empty
        '    If Me.GridEX1.RowCount = 0 Then
        '        Exit Sub
        '    End If
        '    If Me.GridEX1.GetRow().Cells("LogActivityName").Text.ToString = "Delete" Or Me.GridEX1.GetRow().Cells("LogActivityName").Text.ToString = "Login" Then
        '        Exit Sub
        '    End If
        '    frmModProperty.Tags = Me.GridEX1.GetRow().Cells("LogRecordRefNo").Text.ToString
        '    If IsDrillDown = True Then
        '        frmMain.LoadControl(Me.GridEX1.GetRow().Cells("AccessKey").Text.ToString)
        '    Else
        '        frmMain.LoadControl(Me.GridEX1.GetRow().Cells("AccessKey").Text.ToString)
        '        frmModProperty.Tags = Me.GridEX1.GetRow().Cells("LogRecordRefNo").Text.ToString
        '        frmMain.LoadControl(Me.GridEX1.GetRow().Cells("AccessKey").Text.ToString)
        '    End If

        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub MultiColumnCombo1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    ''' <summary>
    ''' TASK TFS1384
    ''' </summary>
    ''' <remarks> Applied security rights of Report Print and Report Export</remarks>
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Report Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Report Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdLog.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdLog.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdLog.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Activity Log " & Chr(10) & "From Date: " & dtpFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpTo.Value.ToString("dd-MM-yyyy").ToString & ""

            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class