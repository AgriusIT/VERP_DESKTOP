Public Class frmPropertyProfileDetailReport
    Implements IGeneral
    Dim _SearchDt As New DataTable
    Dim dealCompleted As String
    Private Sub frmPropertyProfileDetailReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        refreshControls()
        ApplySecurityRights()
        CtrlGrdBar1_Load(Nothing, Nothing)
    End Sub

    Private Sub refreshControls()
        Me.rdoCompleteDeals.Checked = True
        FillCombos("ProjectHead")
        Me.lstProjectHead.DeSelect()
        FillCombos("ProjectName")
        Me.lstProjectName.DeSelect()
        FillCombos("Agent")
        FillCombos("PropertyType")
        Me.lstPropertyType.DeSelect()
        _SearchDt = CType(Me.lstAgentName.ListItem.DataSource, DataTable)
        _SearchDt.AcceptChanges()
        Me.lstAgentName.DeSelect()
        Me.dtpFrom.Value = Date.Today
        Me.dtpTo.Value = Date.Today
        Me.cmbPeriod.SelectedIndex = 0
    End Sub
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Me.GridResults.RootTable.Columns("Margin").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.GridResults.RootTable.Columns("Margin").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.GridResults.RootTable.Columns("CommissionAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Me.GridResults.RootTable.Columns("CommissionAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        CtrlGrdBar1_Load(Nothing, Nothing)
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

        Dim Str As String

        If Condition = "ProjectHead" Then
            Str = "SELECT DISTINCT CostCenterGroup, CostCenterGroup FROM tblDefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1"
            FillListBox(Me.lstProjectHead.ListItem, Str)
        End If

        If Condition = "ProjectName" Then
            Str = "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name,Code FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 Order By Name"
            FillListBox(Me.lstProjectName.ListItem, Str)
        End If

        If Condition = "Agent" Then
            Str = "SELECT AgentId, Name FROM Agent WHERE Active = 1 ORDER BY AgentId"
            FillListBox(Me.lstAgentName.ListItem, Str)
        End If


        If Condition = "PropertyType" Then
            Str = "select PropertyTypeId, PropertyType from PropertyType where Active = 1"
            FillListBox(Me.lstPropertyType.ListItem, Str)
        End If
    End Sub

    Private Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.btnShow.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            Me.Visible = False

            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            Me.btnShow.Enabled = False
            Me.btnPrint.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    Me.btnShow.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    Me.btnPrint.Enabled = True
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
                GetCrystalReportRights()
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    
    Private Sub lstProjectHead_SelectedIndexChaned(sender As Object, e As EventArgs) Handles lstProjectHead.SelectedIndexChaned
        Try
            If Me.lstProjectHead.SelectedIDs.Length > 0 Then

                FillListBox(Me.lstProjectName.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 AND CostCenterGroup IN (" & Me.lstProjectHead.SelectedItems.ToString & ") Else SELECT  CostCenterID,Name FROM tblDefCostCenter WHERE Active=1 AND CostCenterGroup IN (" & Me.lstProjectHead.SelectedItems.ToString & ") ")
                Me.lstProjectName.DeSelect()
            Else

                FillListBox(Me.lstProjectName.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name FROM tblDefCostCenter WHERE Active=1 ")
                Me.lstProjectName.DeSelect()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridResults.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridResults.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridResults.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Propert Detail Report"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Dim strData As String = ""
        Dim dtData As New DataTable
        'If rdoCompleteDeals.Checked Then
        '    dealCompleted = "True"
        'ElseIf rdoPending.Checked Then
        '    dealCompleted = "False"
        'Else
        '    dealCompleted = "All"
        'End If
        strData = "EXEC dbo.SP_PropertyDetailReport '" & Me.dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00") & "','" & Me.dtpTo.Value.ToString("yyyy-MM-dd 00:00:00") & "', '" & Me.lstProjectName.SelectedIDs & "','" & Me.lstAgentName.SelectedIDs & "','" & Me.lstPropertyType.SelectedIDs & "', '" & dealCompleted & "' "
        dtData = GetDataTable(strData)
        dtData.AcceptChanges()
        Me.GridResults.DataSource = dtData
        Me.GridResults.RetrieveStructure()
        ApplyGridSettings()
        Me.UltraTabConrol1.SelectedTab = Me.UltraTabConrol1.Tabs(1).TabPage.Tab
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            AddRptParam("@FromDate", Me.dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00"))
            AddRptParam("@ToDate", Me.dtpTo.Value.ToString("yyyy-MM-dd 23:59:59"))
            AddRptParam("@ProjectIDs", Me.lstProjectName.SelectedIDs)
            AddRptParam("@AgentIDs", Me.lstAgentName.SelectedIDs)
            AddRptParam("@ProjectTypeIDs", Me.lstPropertyType.SelectedIDs)
            AddRptParam("@DealCompleted", dealCompleted.ToString)
            ShowReport("rptPropertyDetailReport")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub refresh_Click(sender As Object, e As EventArgs) Handles refresh.Click
        refreshControls()
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.dtpFrom.Value = Date.Today
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-1)
                Me.dtpTo.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
                Me.dtpTo.Value = Date.Today
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtAgentSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtAgentSearch.KeyUp
        Try
            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt
            dv.RowFilter = "Name Like '%" & Me.txtAgentSearch.Text & "%'"
            Me.lstAgentName.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoCompleteDeals_CheckedChanged(sender As Object, e As EventArgs) Handles rdoCompleteDeals.CheckedChanged
        dealCompleted = "True"
    End Sub

    Private Sub rdoPending_CheckedChanged(sender As Object, e As EventArgs) Handles rdoPending.CheckedChanged
        dealCompleted = "False"
    End Sub

    Private Sub rdoAllDeals_CheckedChanged(sender As Object, e As EventArgs) Handles rdoAllDeals.CheckedChanged
        dealCompleted = "All"
    End Sub
End Class