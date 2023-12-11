'Ali Faisal : TFS2051 : Add report to show the Sub Sub Account Wise BS Summary
''TASK TFS3425 Muhammad Amin done on 01-06-2018 : Show record of zero or null cost centers.

Imports System
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmBSSubSubAccountSummary
    Implements IGeneral
    Dim IsOpenForm As Boolean = False
    ''' <summary>
    ''' Ali Faisal : TFS2051 : Set indexing of Records grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grd
        MainId
        MainCode
        MainTitle
        SubId
        SubCode
        SubTitle
        SubSubId
        SubSubCode
        SubSubTitle
        BSGroup
        SortOrder
        NotesId
        Balance
    End Enum
    ''' <summary>
    ''' Ali Faisal : TFS2051 : Apply grid settings to hide some columns and formating too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If rdoSub.Checked = True Then
                Me.grdSaved.RootTable.Columns(grd.SubSubId).Visible = False
                Me.grdSaved.RootTable.Columns(grd.SubSubCode).Visible = False
                Me.grdSaved.RootTable.Columns(grd.SubSubTitle).Visible = False
            Else
                Me.grdSaved.RootTable.Columns(grd.SubSubId).Visible = False
                Me.grdSaved.RootTable.Columns(grd.SubId).Visible = False
                Me.grdSaved.RootTable.Columns(grd.SubCode).Visible = False
                Me.grdSaved.RootTable.Columns(grd.SubTitle).Visible = False
                Me.grdSaved.RootTable.Columns(grd.NotesId).Visible = False
            End If
            Me.grdSaved.RootTable.Columns(grd.MainId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SubId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.BSGroup).Visible = False
            Me.grdSaved.RootTable.Columns(grd.MainCode).Visible = False
            Me.grdSaved.RootTable.Columns(grd.MainTitle).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SortOrder).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SubTitle).Width = 250
            Me.grdSaved.RootTable.Columns(grd.SubSubTitle).Width = 250

            Me.grdSaved.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Dim grdGroupBy As New Janus.Windows.GridEX.GridEXGroup(Me.grdSaved.RootTable.Columns(grd.BSGroup))
            grdGroupBy.GroupPrefix = String.Empty
            Me.grdSaved.RootTable.Groups.Add(grdGroupBy)
            'Waqar Raza: Commented these lines to link Balance Column not Title
            'If rdoSub.Checked = True Then
            '    Me.grdSaved.RootTable.Columns(grd.SubTitle).ColumnType = Janus.Windows.GridEX.ColumnType.Link
            'Else
            '    Me.grdSaved.RootTable.Columns(grd.SubSubTitle).ColumnType = Janus.Windows.GridEX.ColumnType.Link
            'End If
            Me.grdSaved.RootTable.Columns("Balance").ColumnType = Janus.Windows.GridEX.ColumnType.Link
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS2051 : Apply security to show specific controls to standard users
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnPrint.Enabled = True
                Me.btnShow.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnPrint.Enabled = False
            Me.btnShow.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    Me.btnShow.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    If Me.btnPrint.Text = "&Print" Then btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    ''' <summary>
    ''' Ali Faisal : TFS2051 : Fill list bosex
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "HeadCostCenter" Then
                FillListBox(Me.lstHeadCostCenter.ListItem, "SELECT DISTINCT CostCenterGroup, CostCenterGroup FROM tblDefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")
                'TFS3412: Waqar Raza: Addedby Waqar to Deselect the List for User Friendly
                'Start TFS3412
                lstHeadCostCenter.DeSelect()
                'End TFS3412
            ElseIf Condition = "CostCenter" Then
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1")
                'TFS3412: Waqar Raza: Addedby Waqar to Deselect the List for User Friendly
                'Start TFS3412
                lstCostCenter.DeSelect()
                'End TFS3412
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS2051 : Get all records to get data in given duration
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim strData As String = ""
            Dim dtData As New DataTable
            'strData = "SELECT tblDefGLNotes.Gl_note_id AS NoteId, tblDefGLNotes.note_title AS NoteTitle, tblCOAMain.main_type AS MainType, tblCOAMain.coa_main_id AS MainId, tblCOAMain.main_code AS MainCode, tblCOAMain.main_title AS MainTitle, tblCOAMainSub.main_sub_id AS SubId, tblCOAMainSub.sub_code AS SubCode, tblCOAMainSub.sub_title AS SubTitle, tblCOAMainSubSub.main_sub_sub_id AS SubSubId, tblCOAMainSubSub.sub_sub_code AS SubSubCode, tblCOAMainSubSub.sub_sub_title AS SubSubTitle, SUM(isnull(tblVoucherDetail.debit_amount,0) - isnull(tblVoucherDetail.credit_amount,0)) AS NetAmount " _
            '            & "FROM tblVoucherDetail INNER JOIN tblVoucher ON tblVoucherDetail.voucher_id = tblVoucher.voucher_id INNER JOIN tblCOAMainSubSubDetail ON tblVoucherDetail.coa_detail_id = tblCOAMainSubSubDetail.coa_detail_id INNER JOIN tblCOAMainSubSub ON tblCOAMainSubSubDetail.main_sub_sub_id = tblCOAMainSubSub.main_sub_sub_id INNER JOIN tblDefGLNotes ON tblCOAMainSubSub.CrBS_note_id = tblDefGLNotes.Gl_note_id INNER JOIN tblCOAMainSub ON tblCOAMainSubSub.main_sub_id = tblCOAMainSub.main_sub_id INNER JOIN tblCOAMain ON tblCOAMainSub.coa_main_id = tblCOAMain.coa_main_id LEFT OUTER JOIN tblDefCostCenter ON tblVoucherDetail.CostCenterID = tblDefCostCenter.CostCenterID " _
            '            & "WHERE ((tblCOAMainSubSub.CrBS_note_id IN (9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30)) Or (tblCOAMainSubSub.CrBS_note_id IN (9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30))) AND (tblDefGLNotes.note_title <> '') AND (CONVERT(Varchar, tblVoucher.voucher_date, 102) BETWEEN CONVERT(DateTime,'2001-01-01 00:00:00', 102) AND CONVERT(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "', 102)) "
            'If Me.lstCostCenter.SelectedIDs.Length > 0 Then
            '    strData += "AND tblVoucherDetail.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ") "
            'End If
            'If Me.chkIncludeUnPosted.Checked = True Then
            '    strData += "AND ISNULL(tblVoucher.post,0) IN (0,1) "
            'Else
            '    strData += "AND ISNULL(tblVoucher.post,0) IN (1) "
            'End If
            'strData += "GROUP BY tblDefGLNotes.note_title, tblCOAMainSubSub.sub_sub_code, tblCOAMainSubSub.sub_sub_title, tblCOAMainSubSub.main_sub_sub_id, tblCOAMainSub.sub_code, tblCOAMainSub.sub_title, tblCOAMainSub.main_sub_id, tblDefGLNotes.Gl_note_id, tblCOAMain.main_code, tblCOAMain.main_title, tblCOAMain.coa_main_id, tblCOAMain.main_type " _
            '            & "ORDER BY NoteId"
            If rdoSubSub.Checked = True Then
                If Me.chkShowWithOutCostCenter.Checked = False Then
                    strData = "SP_SubSubHeadWiseBSFormated '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "'," & Me.chkIncludeUnPosted.CheckState & ",'" & Me.lstCostCenter.SelectedIDs & "'"
                Else
                    strData = "SP_SubSubHeadWiseBSFormated '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "'," & Me.chkIncludeUnPosted.CheckState & ",'" & 0 & "'"
                End If
            Else
                If Me.chkShowWithOutCostCenter.Checked = False Then
                    strData = "SP_SubHeadWiseBSFormated '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "'," & Me.chkIncludeUnPosted.CheckState & ",'" & Me.lstCostCenter.SelectedIDs & "'"
                Else
                    strData = "SP_SubHeadWiseBSFormated '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "'," & Me.chkIncludeUnPosted.CheckState & ",'" & 0 & "'"
                End If

            End If
            dtData = GetDataTable(strData)
            dtData.AcceptChanges()
            Me.grdSaved.DataSource = dtData
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional Mode As Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function
    ''' <summary>
    ''' Ali Faisal : TFS2051 : Reset all controls to their default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Label3.Text = ""
            Me.cmbPeriod.SelectedIndex = 0
            Me.cmbPeriod.Text = "Current Month"
            FillCombos("HeadCostCenter")
            FillCombos("CostCenter")
            Me.UltraTabControl1.Tabs(0).Selected = True
            CtrlGrdBar1_Load(Nothing, Nothing)
            ApplySecurity(Utility.EnumDataMode.New)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.dtpFromDate.Value = Date.Today
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.dtpFromDate.Value = Date.Today.AddDays(-1)
                Me.dtpToDate.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
                Me.dtpToDate.Value = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Head Wise Balance Sheet Summary" & Chr(10) & "Upto Date : " & Me.dtpToDate.Value.ToString("dd-MM-yyyy")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPLSubSubAccountWiseSummary_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            IsOpenForm = True
            btnNew_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS2051 : Show crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            'GetCrystalReportRights()
            'Me.lblProgress.Text = "Processing Please Wait ..."
            'Me.lblProgress.Visible = True
            'Application.DoEvents()
            'Me.Cursor = Cursors.WaitCursor
            'Dim dt As DataTable = CType(Me.grdSaved.DataSource, DataTable)
            'dt.AcceptChanges()
            'ShowReport("rptPLSubSubAccountWiseSummary", , , , , , , dt, , , , , , , )
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            GetAllRecords()
            Label3.Text = " Till Date " + dtpToDate.Value.ToString("dd-MM-yyyy")
            Me.UltraTabControl1.Tabs(1).Selected = True
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.Tabs(0).Selected = True Then
                Me.btnPrint.Visible = False
            End If
            If Me.UltraTabControl1.Tabs(1).Selected = True Then
                Me.btnPrint.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstHeadCostCenter_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstHeadCostCenter.SelectedIndexChaned
        Try
            If Me.lstHeadCostCenter.SelectedIDs.Length > 0 Then
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1  AND CostCenterGroup IN (" & Me.lstHeadCostCenter.SelectedItems & ")")
            Else
                FillCombos("CostCenter")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try
            If Not frmMain.Panel2.Controls.Contains(rptTrialBalance) Then
                frmMain.LoadControl("rptTrialBalance")
            End If
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Sub A/C"
            rptTrialBalance.DateTimePicker1.Value = "2001-01-01 00:00:00"
            rptTrialBalance.DateTimePicker2.Value = Me.dtpToDate.Value
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = Me.chkIncludeUnPosted.CheckState
            Dim CostCenterIds As String = ""
            If chkShowWithOutCostCenter.Checked = True Then
                If Me.lstCostCenter.SelectedIDs.Length > 0 AndAlso lstCostCenter.Enabled = True Then
                    CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                    rptTrialBalance.CostID = "0," + CostCenterIds
                Else
                    CostCenterIds = "0"
                End If
            Else
                If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                    CostCenterIds = Me.lstCostCenter.SelectedIDs.ToString
                    rptTrialBalance.CostID = "0," + CostCenterIds
                Else
                    CostCenterIds = ""
                End If
            End If
            
            rptTrialBalance.Tracking = True
            If rdoSub.Checked = True Then
                rptTrialBalance.SubSubAccount = True
                rptTrialBalance.GetSubSubWiseSubSubAccounts(Val(Me.grdSaved.GetRow.Cells(grd.SubId).Value.ToString), Val(Me.grdSaved.GetRow.Cells(grd.NotesId).Value.ToString), CostCenterIds)
            Else
                rptTrialBalance.GetSubSubWiseDetailAccounts(Val(Me.grdSaved.GetRow.Cells(grd.SubSubId).Value.ToString), Val(Me.grdSaved.GetRow.Cells(grd.NotesId).Value.ToString), CostCenterIds)
            End If

            frmMain.LoadControl("rptTrialBalance")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkShowWithOutCostCenter_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowWithOutCostCenter.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If chkShowWithOutCostCenter.Checked Then
                Me.lstHeadCostCenter.Enabled = False
                Me.lstCostCenter.Enabled = False
            Else
                Me.lstHeadCostCenter.Enabled = True
                Me.lstCostCenter.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class