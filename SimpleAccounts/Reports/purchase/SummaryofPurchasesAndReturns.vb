Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Public Class SummaryofPurchasesAndReturns
    ''' <summary>
    ''' 'Ali Faisal : TFS1383 : 07-Sep-2017 : Show button to display the crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>'Ali Faisal : TFS1383 : 07-Sep-2017 :</remarks>
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            GetCrystalReportRights()
            Me.CallShowReport()
        Catch Ex As Exception
            ShowErrorMessage(Ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1383 : 07-Sep-2017 : Get the Procedure from database and apply all filters values to show report
    ''' </summary>
    ''' <param name="Print"></param>
    ''' <remarks>'Ali Faisal : TFS1383 : 07-Sep-2017 :</remarks>
    Sub CallShowReport(Optional ByVal Print As Boolean = False)
        Try
            Me.Cursor = Cursors.WaitCursor
            AddRptParam("@FromDate", Me.dtpFromDate.Value.Date.ToString("yyyy-MM-dd 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.Date.ToString("yyyy-MM-dd 23:59:59"))
            AddRptParam("@VendorId", Me.cmbVendor.Value)
            AddRptParam("@CostCenterId", Me.cmbCostCenter.SelectedValue)
            ShowReport("SummaryOfPurchaseAndReturns")
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1383 : 07-Sep-2017 : Fast print of crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>'Ali Faisal : TFS1383 : 07-Sep-2017 :</remarks>
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        CallShowReport(True)
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1383 : 07-Sep-2017 : Period selection for date range
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>'Ali Faisal : TFS1383 : 07-Sep-2017 :</remarks>
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
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
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1383 : 07-Sep-2017 : Load form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>'Ali Faisal : TFS1383 : 07-Sep-2017 :</remarks>
    Private Sub SummaryofPurchasesAndReturns_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Str As String
            Me.pnlCost.Visible = True
            Me.pnlPeriod.Visible = True
            Me.pnlVendorCustomer.Visible = True
            Me.cmbPeriod.Text = "Current Month"
            ''Start TFS2124
            Str = "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE 1 = 1 "
            If getConfigValueByType("Show Customer On Purchase") = "True" Then
                Str += " AND Account_Type in ('Vendor','Customer') "
            Else
                Str += " AND  Account_Type='Vendor' "
            End If
            Str += " ORDER BY 2 ASC"
            ''End TFS2124

            FillUltraDropDown(cmbVendor, Str)
            FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            Me.cmbVendor.Rows(0).Activate()
            Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 'Ali Faisal : TFS1383 : 07-Sep-2017 : Get security rights for standard user
    ''' </summary>
    ''' <remarks>'Ali Faisal : TFS1383 : 07-Sep-2017 :</remarks>
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
            Else
                Me.btnPrint.Enabled = False
                Me.btnShow.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    End If
                    If RightsDt.FormControlName = "Show" Then
                        Me.btnShow.Enabled = True
                    End If
                    If RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
End Class