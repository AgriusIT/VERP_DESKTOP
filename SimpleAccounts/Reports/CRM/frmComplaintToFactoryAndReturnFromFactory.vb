Imports SBDal
Imports SBUtility


Public Class frmComplaintToFactoryAndReturnFromFactory
    Implements IGeneral
    Dim isFormLoaded As Boolean = False
    Dim _SearchDt As New DataTable
    Private Sub frmComplaintDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillCombos("party")
            FillCombos("product")
            Me.rdoToFactory.Checked = True
            _SearchDt = CType(Me.lstItems.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            Me.cmbPartyName.Rows(0).Activate()
            ApplySecurity(Utility.EnumDataMode.New)
            CtrlGrdBar2_Load(Nothing, Nothing)
            Me.rdoToFactory.Checked = True
            Me.rdoCode.Checked = True
            UltraDropDownSearching(cmbPartyName, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            isFormLoaded = True
            Me.lstItems.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        If rdoToFactory.Checked = True Then
            Me.GridResults.RootTable.Columns("ArticleId").Visible = False
            Me.GridResults.RootTable.Columns("VendorId").Visible = False
            Me.GridResults.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridResults.RootTable.Columns("Total Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Total Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Total Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridResults.RootTable.Columns("Rate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Rate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Rate").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridResults.RootTable.Columns("Pack Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Pack Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Pack Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridResults.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
            Me.GridResults.RootTable.Columns("Rate").FormatString = "N" & TotalAmountRounding
            Me.GridResults.RootTable.Columns("Rate").TotalFormatString = "N" & DecimalPointInValue
            Me.GridResults.RootTable.Columns("Rate").TotalFormatString = "N" & TotalAmountRounding
        Else
            Me.GridResults.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Total Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Rate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Pack Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Total Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Rate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Pack Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridResults.RootTable.Columns("Rate").FormatString = "N" & DecimalPointInValue
            Me.GridResults.RootTable.Columns("Rate").FormatString = "N" & TotalAmountRounding
            Me.GridResults.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridResults.RootTable.Columns("Total Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.GridResults.RootTable.Columns("Rate").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

        End If
        
        'Me.GridResults.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        'Me.GridResults.RootTable.Columns("Total Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        'Me.GridResults.RootTable.Columns("Rate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        'Me.GridResults.RootTable.Columns("Pack Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        'Me.GridResults.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        'Me.GridResults.RootTable.Columns("Total Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        'Me.GridResults.RootTable.Columns("Rate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        'Me.GridResults.RootTable.Columns("Pack Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnPrint.Enabled = True
                Me.btnShow.Enabled = True
                Me.CtrlGrdBar2.mGridPrint.Enabled = True
                Me.CtrlGrdBar2.mGridExport.Enabled = True
                Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                Me.rdoFromFactory.Enabled = True
                Me.rdoToFactory.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnPrint.Enabled = False
            Me.btnShow.Enabled = False
            Me.CtrlGrdBar2.mGridPrint.Enabled = False
            Me.CtrlGrdBar2.mGridExport.Enabled = False
            Me.CtrlGrdBar2.mGridChooseFielder.Enabled = False
            Me.rdoFromFactory.Enabled = False
            Me.rdoToFactory.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    Me.btnShow.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    Me.btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar2.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar2.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Return To Factory" Then
                    Me.rdoToFactory.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Return From Factory" Then
                    Me.rdoFromFactory.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Sub ApplySecurityComplaintReturn()

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "party" Then
                FillUltraDropDown(cmbPartyName, "select coa_detail_id, detail_code as [Party Code], detail_title as [Party Name]from vwCOADetail where account_type = 'Vendor'")
                Me.cmbPartyName.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Me.cmbPartyName.Rows(0).Activate()
                UltraDropDownSearching(cmbPartyName, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            ElseIf Condition = "product" Then
                FillListBox(Me.lstItems.ListItem, "SELECT ArticleDefView.ArticleId ,ArticleDefView.ArticleCode+ ' ~ ' + ArticleDefView.ArticleDescription  AS Product FROM ArticleDefView")
                Me.lstItems.DeSelect()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim strData As String = ""
            Dim dtData As New DataTable
            If rdoToFactory.Checked = True Then
                strData = "EXEC dbo.SP_ReturnToFactory '" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "','" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', '" & Me.cmbPartyName.Value.ToString & "','" & Me.lstItems.SelectedIDs & "'"
            Else
                strData = "EXEC dbo.SP_ReturnFromFactory '" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "','" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 00:00:00") & "', '" & Me.cmbPartyName.Value.ToString & "','" & Me.lstItems.SelectedIDs & "' "
            End If

            dtData = GetDataTable(strData)
            dtData.AcceptChanges()
            Me.GridResults.DataSource = dtData
            Me.GridResults.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            FillCombos("party")
            FillCombos("product")
            CtrlGrdBar2_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            GetAllRecords()
            Me.UltraTabConrol1.Tabs(1).Selected = True
            'If rdoToFactory.Checked = True Then
            '    Me.Label1.Text = "Return To Factory Report"
            'Else
            '    Me.Label1.Text = "Reutrn From Factory Report"
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
            AddRptParam("@Party", Me.cmbPartyName.Value)
            AddRptParam("@Product", Me.lstItems.SelectedIDs)
            If rdoFromFactory.Checked = True Then
                ShowReport("rptReturnFromFactory")
            Else
                ShowReport("rptReturnToFactory")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        Try
            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt
            dv.RowFilter = "Product Like '%" & Me.txtSearch.Text & "%'"
            Me.lstItems.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoComplaint_Click(sender As Object, e As EventArgs) Handles rdoToFactory.CheckedChanged
        ReSetControls()
        Me.Label1.Text = "Return To Factory Report"
    End Sub

    Private Sub rdoCompliantReturn_CheckedChanged(sender As Object, e As EventArgs) Handles rdoFromFactory.CheckedChanged
        Try
            ReSetControls()
            Me.Label1.Text = "Return From Factory Report"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridResults.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridResults.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridResults.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Return To And" & Chr(10) & "From Date : " & Me.dtpFromDate.Value.ToString("yyyy-MM-dd") & "To Date : " & Me.dtpToDate.Value.ToString("yyyy-MM-dd")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoCode_CheckedChanged(sender As Object, e As EventArgs) Handles rdoCode.CheckedChanged
        If isFormLoaded = False Then Exit Sub
        Me.cmbPartyName.DisplayMember = Me.cmbPartyName.Rows(0).Cells(1).Column.Key.ToString
        Me.cmbPartyName.Rows(0).Activate()
    End Sub

    Private Sub rdoName_CheckedChanged(sender As Object, e As EventArgs) Handles rdoName.CheckedChanged
        If isFormLoaded = False Then Exit Sub
        Me.cmbPartyName.DisplayMember = Me.cmbPartyName.Rows(0).Cells(2).Column.Key.ToString
        Me.cmbPartyName.Rows(0).Activate()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ReSetControls()
    End Sub
End Class