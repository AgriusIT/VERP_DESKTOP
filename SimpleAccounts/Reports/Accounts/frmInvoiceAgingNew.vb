'Ali Faisal : TFS2051 : Add report to show 
Imports System
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmInvoiceAgingNew
    Implements IGeneral
    ''' <summary>
    ''' Ali Faisal : TFS2051 : Set indexing of Records grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grd
        AccountId
        AccountCode
        AccountTitle
        VoucherId
        VoucherNo
        VoucherDate
        CreditDays
        DueDate
        PaidDate
        DueDays
        PaidAmount
        PurchaseReturnDate
        PurchaseReturnAmount
        Amount
        NotDue
        Current
        ThirtyToSixty
        SixtyToNinty
        'Rafay:Task Start :to add rows
        NintyToOneTwenty
        OneTwentyToOneFifty
        OneFiftyplus
        'Rafay:Task End
        NintyPlus

        OverDueAmount
        Narration
    End Enum
    ''' <summary>
    ''' Ali Faisal : TFS2051 : Apply grid settings to hide some columns and formating too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns(grd.AccountId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.VoucherId).Visible = False

            Me.grdSaved.RootTable.Columns(grd.ThirtyToSixty).Caption = "30_60"
            Me.grdSaved.RootTable.Columns(grd.SixtyToNinty).Caption = "60_90"
            'Rafay:Task start:
            Me.grdSaved.RootTable.Columns(grd.NintyToOneTwenty).Caption = "90_120"
            Me.grdSaved.RootTable.Columns(grd.OneTwentyToOneFifty).Caption = "120_150"
            Me.grdSaved.RootTable.Columns(grd.OneFiftyplus).Caption = "150+"
            'Rafay:Task End
            Me.grdSaved.RootTable.Columns(grd.NintyPlus).Caption = "90+"

            Me.grdSaved.RootTable.Columns(grd.VoucherDate).FormatString = "" & str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns(grd.DueDate).FormatString = "" & str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns(grd.PaidDate).FormatString = "" & str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns(grd.PurchaseReturnDate).FormatString = "" & str_DisplayDateFormat

            Me.grdSaved.RootTable.Columns(grd.PaidAmount).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.PaidAmount).TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.PaidAmount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.PaidAmount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.PaidAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdSaved.RootTable.Columns(grd.PurchaseReturnAmount).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.PurchaseReturnAmount).TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.PurchaseReturnAmount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.PurchaseReturnAmount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.PurchaseReturnAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdSaved.RootTable.Columns(grd.Amount).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.Amount).TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.Amount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Amount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Amount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdSaved.RootTable.Columns(grd.NotDue).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.NotDue).TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.NotDue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.NotDue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.NotDue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdSaved.RootTable.Columns(grd.Current).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.Current).TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.Current).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Current).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Current).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdSaved.RootTable.Columns(grd.ThirtyToSixty).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.ThirtyToSixty).TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.ThirtyToSixty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.ThirtyToSixty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.ThirtyToSixty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdSaved.RootTable.Columns(grd.SixtyToNinty).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.SixtyToNinty).TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.SixtyToNinty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.SixtyToNinty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.SixtyToNinty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            'Rafay : Task Start 
            Me.grdSaved.RootTable.Columns(grd.NintyToOneTwenty).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.NintyToOneTwenty).TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.NintyToOneTwenty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.NintyToOneTwenty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.NintyToOneTwenty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdSaved.RootTable.Columns(grd.OneTwentyToOneFifty).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.OneTwentyToOneFifty).TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.OneTwentyToOneFifty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.OneTwentyToOneFifty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.OneTwentyToOneFifty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdSaved.RootTable.Columns(grd.OneFiftyplus).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.OneFiftyplus).TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.OneFiftyplus).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.OneFiftyplus).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.OneFiftyplus).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Rafay :Task end
            Me.grdSaved.RootTable.Columns(grd.NintyPlus).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.NintyPlus).TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.NintyPlus).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.NintyPlus).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.NintyPlus).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdSaved.RootTable.Columns(grd.OverDueAmount).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.OverDueAmount).TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.OverDueAmount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.OverDueAmount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.OverDueAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
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
            FillUltraDropDown(Me.cmbVendor, "SELECT coa_detail_id AS Id, detail_title AS Name, detail_code AS Code FROM vwCOADetail WHERE Active = 1 AND account_type = 'Vendor' ORDER BY 1", True)

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
            strData = "SP_InvoiceAgingNew '" & Date.Now.ToString("yyyy-MM-dd 23:59:59") & "', " & Me.cmbVendor.Value & ", '" & Me.chkIncludePaid.CheckState & "'"
            dtData = GetDataTable(strData)
            dtData.AcceptChanges()
            dtData.Columns(grd.OverDueAmount).Expression = "[Current] + [30_60] + [60_90] + [90_120]+[120_150]+[150+]+[90+]"
            Me.grdSaved.DataSource = dtData
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional Mode As Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Not Me.cmbVendor.Value > 0 Then
                msg_Error("Please select any Vendor")
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS2051 : Reset all controls to their default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            FillCombos()
            Me.cmbVendor.Value = 0
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
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Invoice Aging" & Chr(10) & "Upto Date : " & Date.Now.ToString("yyyy-MM-dd")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPLSubSubAccountWiseSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
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
    ''' Ali Faisal : TFS2051 : Show crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            AddRptParam("@ToDate", Date.Now.ToString("yyyy-MM-dd 23:59:59"))
            AddRptParam("@AccountId", Me.cmbVendor.Value)
            AddRptParam("@IncludePaid", Me.chkIncludePaid.CheckState)
            ShowReport("rptInvoiceAging")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            If IsValidate() = True Then
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                Me.Cursor = Cursors.WaitCursor
                GetAllRecords()
                Me.UltraTabControl1.Tabs(1).Selected = True
            End If
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
                Me.btnPrint.Visible = True
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

    Private Sub grdSaved_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdSaved.FormattingRow

    End Sub
End Class