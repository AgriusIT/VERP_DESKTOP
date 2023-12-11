'Ayesha Rehman : TFS2728 : Add report to show -ve balance in Credit Balance and +ve balance in Debit Balance of any detail account
Imports System
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmAssetsAndLiabilityReport

    Implements IGeneral
    Dim _SearchDt As New DataTable

    ''' <summary>
    ''' Ayesha Rehman : TFS2728 : Set indexing of Records grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grd
        AccountId
        AccountCode
        AccountName
        DebitBalance
        CreditbBalance
    End Enum
    ''' <summary>
    ''' Ayesha Rehman : TFS2728 : Apply grid settings to hide some columns and formating too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns(grd.AccountId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.DebitBalance).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.DebitBalance).TotalFormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.DebitBalance).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.DebitBalance).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.DebitBalance).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.CreditbBalance).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.CreditbBalance).TotalFormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.CreditbBalance).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.CreditbBalance).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.CreditbBalance).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : TFS2728 : Apply security to show specific controls to standard users
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnPrint.Enabled = True
                Me.btnPrintPreview.Enabled = True
                Me.btnShow.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnPrint.Enabled = False
            Me.btnPrintPreview.Enabled = False
            Me.btnShow.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    Me.btnShow.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print Preview" Then
                    Me.btnPrintPreview.Enabled = True
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
    ''' Ayesha Rehman : TFS2728 : Fill list bosex
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim Str As String = String.Empty
            If Condition = "Company" Then
                Str = "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable "
                FillDropDown(Me.cmbCompany, Str, True)
            ElseIf Condition = "HeadCostCenter" Then
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
            ElseIf Condition = "Main Account" Then
                FillListBox(Me.lstMain.ListItem, "Select coa_main_id , main_code + '~' +  main_title from tblCOAMain")
                'TFS3412: Waqar Raza: Addedby Waqar to Deselect the List for User Friendly
                'Start TFS3412
                lstMain.DeSelect()
                'End TFS3412
            ElseIf Condition = "Sub Account" Then
                FillListBox(Me.lstSub.ListItem, "Select main_sub_id , sub_code + '~' + sub_title  from tblCOAMainSub")
                'TFS3412: Waqar Raza: Addedby Waqar to Deselect the List for User Friendly
                'Start TFS3412
                lstSub.DeSelect()
                'End TFS3412
            ElseIf Condition = "Sub Sub Account" Then
                FillListBox(Me.lstSubSub.ListItem, "Select main_sub_sub_id , sub_sub_code + '~' + sub_sub_title  from tblCOAMainSubSub")
                'TFS3412: Waqar Raza: Addedby Waqar to Deselect the List for User Friendly
                'Start TFS3412
                lstSubSub.DeSelect()
                'End TFS3412
            ElseIf Condition = "Detail Account" Then
                FillListBox(Me.lstDetail.ListItem, "Select coa_detail_id , detail_code + '~' +  detail_title As AccountName  from tblCOAMainSubSubDetail")
                'TFS3412: Waqar Raza: Addedby Waqar to Deselect the List for User Friendly
                'Start TFS3412
                lstDetail.DeSelect()
                'End TFS3412
            End If






        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub
    ''' <summary>
    ''' Ayesha Rehman : TFS2728 : Get all records to get data in given duration
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>+
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""

            str = "SELECT tblVoucherDetail.coa_detail_id AS AccountId, vwCOADetail.detail_code AS [Account Code] ," _
                  & "vwCOADetail.detail_title AS [Account Name], CASE WHEN SUM(tblVoucherDetail.debit_amount-tblVoucherDetail.credit_amount) > 0 THEN ISNULL(SUM(ISNULL(tblVoucherDetail.debit_amount,0)- ISNull(tblVoucherDetail.credit_amount,0)),0) ELSE 0 END As [Debit Balance] , " _
                & " CASE WHEN SUM(tblVoucherDetail.debit_amount-tblVoucherDetail.credit_amount) < 0 THEN (-1) *( ISNULL(SUM(ISNULL(tblVoucherDetail.debit_amount,0)- ISNull(tblVoucherDetail.credit_amount,0)),0)) ELSE 0 END As [Credit Balance] " _
                  & "FROM tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id LEFT OUTER JOIN CompanyDefTable ON tblVoucher.location_id = CompanyDefTable.CompanyId INNER JOIN vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id LEFT OUTER JOIN tblDefCostCenter ON tblVoucherDetail.CostCenterID = tblDefCostCenter.CostCenterID " _
                & "WHERE tblVoucherDetail.coa_detail_id > 0 AND tblVoucher.voucher_date <= '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "'"

            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                str += " AND tblVoucherDetail.CostCenterID IN (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstDetail.SelectedIDs.Length > 0 Then
                str += " AND tblVoucherDetail.coa_detail_id IN (" & Me.lstDetail.SelectedIDs & ")"
            End If
            If Me.cmbCompany.SelectedValue > 0 Then
                str += " AND tblVoucher.location_id  =" & Me.cmbCompany.SelectedValue & ""
            End If
            str += " GROUP BY tblVoucherDetail.coa_detail_id, vwCOADetail.detail_code,vwCOADetail.detail_title "
            str += " Having ISNULL(SUM(ISNULL(tblVoucherDetail.debit_amount,0)- ISNull(tblVoucherDetail.credit_amount,0)),0) <> 0  "
            str += "Order by tblVoucherDetail.coa_detail_id "
            Dim dt As DataTable = GetDataTable(str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function
    ''' <summary>
    ''' Ayesha Rehman : TFS2728 : Reset all controls to their default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.cmbCompany.SelectedIndex = 0
            FillCombos("HeadCostCenter")
            FillCombos("CostCenter")
            FillCombos("Company")
            FillCombos("Main Account")
            FillCombos("Sub Account")
            FillCombos("Sub Sub Account")
            FillCombos("Detail Account")
            _SearchDt = CType(Me.lstDetail.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            'TFS3412: Waqar Raza: Addedby Waqar to Deselect the List for User Friendly
            'Start TFS3412
            lstDetail.DeSelect()
            'End TFS3412
            Me.UltraTabControl1.Tabs(0).Selected = True
            CtrlGrdBar1_Load(Nothing, Nothing)
            ApplySecurity(Utility.EnumDataMode.New)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    ''' <summary>
    ''' Search Account by name or code
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        Try
            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt
            dv.RowFilter = "AccountName  Like '%" & Me.txtSearch.Text & "%'"
            Me.lstDetail.ListItem.DataSource = dv.ToTable
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

    Public Sub SetNavigationButtons(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
   
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Assets And Liability Report"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmAssetsAndLiabilityReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
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
    ''' Ayesha Rehman : TFS2728 : Show crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click, btnPrintPreview.Click
        Try
            GetCrystalReportRights()
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
            AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
            AddRptParam("@VendorIds", Me.lstDetail.SelectedIDs)
            AddRptParam("@CompanyId", Me.cmbCompany.SelectedValue)
            ShowReport("rptAssetsAndLiabilityReport")
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
            Me.UltraTabControl1.Tabs(1).Selected = True
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
                Me.btnPrint.Visible = True
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
    Private Sub lstMain_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstMain.SelectedIndexChaned
        If Me.lstMain.SelectedIDs.Length > 0 Then
            FillListBox(Me.lstSub.ListItem, "SELECT main_sub_id , sub_code + '~' + sub_title  FROM tblCOAMainSub WHERE  coa_main_id  IN (" & Me.lstMain.SelectedIDs & ")")
        Else
            FillCombos("Sub Account")
        End If
    End Sub

    Private Sub lstSub_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstSub.SelectedIndexChaned
        If Me.lstSub.SelectedIDs.Length > 0 Then
            FillListBox(Me.lstSubSub.ListItem, "SELECT main_sub_sub_id  , sub_sub_code + '~' + sub_sub_title  FROM tblCOAMainSubSub  WHERE  main_sub_id  IN (" & Me.lstSub.SelectedIDs & ")")
        Else
            FillCombos("Sub Sub Account")
        End If
    End Sub

    Private Sub lstSubSub_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstSubSub.SelectedIndexChaned
        If Me.lstSubSub.SelectedIDs.Length > 0 Then
            FillListBox(Me.lstDetail.ListItem, "SELECT coa_detail_id  , detail_code  + '~' + detail_title As AccountName  FROM tblCOAMainSubSubDetail  WHERE main_sub_sub_id IN (" & Me.lstSubSub.SelectedIDs & ")")
            _SearchDt = CType(Me.lstDetail.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            Else
            FillCombos("Detail Account")
        End If
    End Sub

   
End Class