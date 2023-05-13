Imports SBModel
Public Class frmRptAccountWisePurchaseReport
    Private Sub FillCombo()
        Try
            Dim Str As String
            ''Start TFS2124
            Str = " select coa_detail_id,detail_title as Account, detail_code as Code, sub_sub_title as [Account Head], Account_Type as [Type]  from vwcoadetail where 1=1"
            If getConfigValueByType("Show Customer On Purchase") = "True" Then
                Str += " AND Account_Type in ('Vendor','Customer','LC') "
            Else
                Str += " AND  Account_Type in ('Vendor','LC') "
            End If
            ''End TFS2124
            FillUltraDropDown(cmbAccount, Str, True)
            Me.cmbAccount.Rows(0).Activate()
            FillDropDown(Me.cmbCostCentre, "Select CostCenterId, Name From tblDefCostCenter Order By 2 asc")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim CId As Integer = 0
        Dim AId As Integer = 0
        Try
            CId = Me.cmbCostCentre.SelectedValue
            AId = Me.cmbAccount.Value
            FillCombo()
            Me.cmbCostCentre.SelectedValue = CId
            Me.cmbAccount.Value = AId
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            Dim dt As DataTable = GetDataTable("Exec SP_AccountWisePurchase '" & Me.dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00") & "', '" & Me.dtpTo.Value.ToString("yyyy-MM-dd 23:59:59") & "', " & IIf(Me.cmbAccount.ActiveRow Is Nothing, 0, Me.cmbAccount.Value) & ", " & IIf(Me.cmbCostCentre.SelectedIndex = -1, 0, Me.cmbCostCentre.SelectedValue) & "")
            dt.AcceptChanges()
            Me.GridEX1.DataSource = dt
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.RootTable.Columns("LC Date").FormatString = "N" & DecimalPointInQty
            Me.GridEX1.RootTable.Columns("Total Qty").FormatString = "N" & DecimalPointInQty
            Me.GridEX1.RootTable.Columns("Total LC Amount").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Insurrance").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Exchange Rate").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Expense By LC").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Assessed Value").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("DD For CMCC").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Adj CMCC").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("DD For ETO").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Adj ETO").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Total Amount").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Total Duty").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Adj ETO1").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Add Sales Tax").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Adv Income Tax").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Excise Duty%").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Excise Duty").FormatString = "N" & DecimalPointInValue

            ''
            Me.GridEX1.RootTable.Columns("Total Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Total LC Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Insurrance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Exchange Rate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Expense By LC").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Assessed Value").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("DD For CMCC").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Adj CMCC").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("DD For ETO").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Adj ETO").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Total Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Total Duty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Adj ETO1").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Add Sales Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Adv Income Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Excise Duty%").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Excise Duty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            ''
            Me.GridEX1.RootTable.Columns("Total Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Total LC Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Insurrance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Exchange Rate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Expense By LC").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Assessed Value").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("DD For CMCC").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Adj CMCC").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("DD For ETO").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Adj ETO").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Total Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Total Duty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Adj ETO1").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Add Sales Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Adv Income Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Excise Duty%").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Excise Duty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Account Wise Purchase Report " & Chr(10) & ""
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
            Else
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                Me.btnPrint.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                        ''TASK TFS1384 replaced Report Print and Report Export with Report Print and Report Export on 07-09-2017
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                        Me.btnPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        ''End TASK TFS1384
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmRptAccountWisePurchaseReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            FillCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            AddRptParam("@FromDate", Me.dtpFrom.Value.ToString("yyyy-MM-dd 00:00:00"))
            AddRptParam("@ToDate", Me.dtpTo.Value.ToString("yyyy-MM-dd 23:59:59"))
            AddRptParam("@AccountId", IIf(Me.cmbAccount.ActiveRow Is Nothing, 0, Me.cmbAccount.Value))
            AddRptParam("@CostCentreId", IIf(Me.cmbCostCentre.SelectedIndex = -1, 0, Me.cmbCostCentre.SelectedValue))
            ShowReport("rptAccountWisePurchaseReport")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class