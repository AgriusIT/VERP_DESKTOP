''TASK TFS2329 New form PRO

Imports SBDal
Imports SBModel

Public Class frmProfitSharing
    Public PropertyId As Integer = 0
    Public ProfitSharingId As Integer = 0
    Dim Obj As ProfitSharingMasterBE
    Public IsEditMode As Boolean = False
    Dim CostCnterId As Integer = 0
    Dim InvestmentAccountId As Integer = 0
    Dim ProfitExpenseAccountId As Integer = 0
    Public PurchaseAmount As Double
    Public PlotNo As String
    Dim InvestorName As String

    Private Sub FillModel()
        Try
            Dim NetProfitExpense As Double = 0.0
            Dim NetInvestment As Double = 0.0

            Obj = New ProfitSharingMasterBE()
            Obj.ProfitSharingId = ProfitSharingId
            Obj.SharingDate = Me.dtpDate.Value
            Obj.voucher_no = Me.txtVoucherNo.Text
            Obj.PropertyProfileId = Me.cmbProperty.SelectedValue
            Obj.ProfitForDistribution = Val(Me.txtProfitForDistribution.Text)

            'VoucherMaster'

            Obj.Voucher.VoucherCode = Me.txtVoucherNo.Text
            Obj.Voucher.VoucherNo = Me.txtVoucherNo.Text
            Obj.Voucher.VoucherDate = Now
            Obj.Voucher.LocationId = 1
            Obj.Voucher.VoucherTypeId = 1
            Obj.Voucher.FinancialYearId = 1
            Obj.Voucher.UserName = LoginUserName
            Obj.Voucher.Source = Me.Name
            Obj.Voucher.Remarks = "Profit Sharing Voucher Agianst " & cmbProperty.Text & " Property"
            Obj.Voucher.VoucherDatail = New List(Of VouchersDetail)

            Dim ddtt As DataTable = CType(Me.grdDetail.DataSource, DataTable)
            grdDetail.UpdateData()

            For Each Row As Janus.Windows.GridEX.GridEXRow In grdDetail.GetRows
                Dim Detail As New ProfitSharingDetailBE()
                Detail.ProfitSharingDetailId = Row.Cells(GridDetail.ProfitSharingDetailId).Value
                Detail.ProfitSharingId = Row.Cells(GridDetail.ProfitSharingId).Value
                Detail.InvestorId = Row.Cells(GridDetail.InvestorId).Value
                Detail.AdjustmentAmount = Row.Cells(GridDetail.Adjustment).Value
                Detail.NetProfitAmount = Row.Cells(GridDetail.NetProfit).Value
                Detail.InvestmentBookingId = Row.Cells(GridDetail.InvestmentBookingId).Value


                Detail.InvestmentAccountId = Me.InvestmentAccountId
                Detail.ProfitExpenseAccountId = Me.ProfitExpenseAccountId
                Obj.Detail.Add(Detail)
                ''Voucher Detail Credit
                Dim CreditEntry As New VouchersDetail()
                CreditEntry.CoaDetailId = Val(Row.Cells("InvestorId").Value.ToString())
                CreditEntry.DebitAmount = 0
                CreditEntry.CreditAmount = Val(Row.Cells("InvestmentAmount").Value.ToString())
                CreditEntry.LocationId = 1
                CreditEntry.CostCenter = Me.CostCnterId
                CreditEntry.PlotNo = PlotNo
                CreditEntry.Comments = "Investor Voucher Against " & cmbProperty.Text & " Property And PlotNo: " & frmPropertyProfile.txtPlotNumber.Text & " Investor: " & Val(Row.Cells("InvestorName").Value.ToString())
                Obj.Voucher.VoucherDatail.Add(CreditEntry)

                NetProfitExpense += Val(Row.Cells("NetProfitAmount").Value.ToString())
                NetInvestment += Val(Row.Cells("InvestmentAmount").Value.ToString())


                Dim InvProfitDebit As New VouchersDetail()
                InvProfitDebit.CoaDetailId = Val(Row.Cells("InvestorId").Value.ToString())
                InvProfitDebit.DebitAmount = 0
                InvProfitDebit.CreditAmount = Val(Row.Cells("NetProfitAmount").Value.ToString())
                InvProfitDebit.LocationId = 1
                InvProfitDebit.CostCenter = Me.CostCnterId
                InvProfitDebit.PlotNo = PlotNo
                InvProfitDebit.Comments = "Profit on Investment Against " & cmbProperty.Text & " Property And PlotNo: " & frmPropertyProfile.txtPlotNumber.Text & " Investor: " & Val(Row.Cells("InvestorName").Value.ToString())
                Obj.Voucher.VoucherDatail.Add(InvProfitDebit)

            Next

            'Voucher Detail Debit Expense Account
            Dim ExpenseProfitDebitEntry As New VouchersDetail()
            ExpenseProfitDebitEntry.CoaDetailId = Me.ProfitExpenseAccountId
            ExpenseProfitDebitEntry.DebitAmount = NetProfitExpense
            ExpenseProfitDebitEntry.CreditAmount = 0
            ExpenseProfitDebitEntry.LocationId = 1
            ExpenseProfitDebitEntry.CostCenter = Me.CostCnterId
            ExpenseProfitDebitEntry.PlotNo = PlotNo
            ExpenseProfitDebitEntry.Comments = "Profit Expense Account Voucher Against " & cmbProperty.Text & " Property And PlotNo: " & frmPropertyProfile.txtPlotNumber.Text & " "
            Obj.Voucher.VoucherDatail.Add(ExpenseProfitDebitEntry)

            'Voucher Detail Debit Investment Account
            Dim InvestmentDebitEntry As New VouchersDetail()
            InvestmentDebitEntry.CoaDetailId = Me.InvestmentAccountId
            InvestmentDebitEntry.DebitAmount = NetInvestment
            InvestmentDebitEntry.CreditAmount = 0
            InvestmentDebitEntry.LocationId = 1
            InvestmentDebitEntry.CostCenter = Me.CostCnterId
            InvestmentDebitEntry.PlotNo = PlotNo
            InvestmentDebitEntry.Comments = "Investment Voucher Against " & cmbProperty.Text & " Property And PlotNo: " & frmPropertyProfile.txtPlotNumber.Text & ""
            Obj.Voucher.VoucherDatail.Add(InvestmentDebitEntry)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function GetVoucherNo() As String
        Dim VoucherNo As String = String.Empty
        Dim compWisePrefix As String = String.Empty
        If getConfigValueByType("CompanyWisePrefix").ToString = "True" Then
            compWisePrefix = "JV"
        Else
            compWisePrefix = "JV"
        End If
        If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
            Return GetSerialNo(compWisePrefix.ToString + "-" + Microsoft.VisualBasic.Right(Me.dtpDate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
        Else
            Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
            Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
            If Not dr Is Nothing Then
                If dr("config_Value") = "Monthly" Then
                    Return GetNextDocNo(compWisePrefix & "-" & Format(Me.dtpDate.Value, "yy") & Me.dtpDate.Value.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
                Else
                    VoucherNo = GetNextDocNo(compWisePrefix, 6, "tblVoucher", "voucher_no")
                    Return VoucherNo
                End If
            Else
                VoucherNo = GetNextDocNo(compWisePrefix, 6, "tblVoucher", "voucher_no")
                Return VoucherNo
            End If
        End If
    End Function

    Private Sub ResetControls()
        Try
            cmbProperty.Focus()
            Me.txtVoucherNo.Text = GetVoucherNo()
            If Not Me.cmbProperty.SelectedIndex = -1 Then
                Me.cmbProperty.SelectedIndex = 0
            End If
            Me.txtProfitTillDate.Text = String.Empty
            Me.txtProfitForDistribution.Text = String.Empty
            Me.dtpDate.Value = Now
            Me.grdDetail.DataSource = ProfitSharingDetailDAL.GetPropertyProfile(-1)
            PurchaseAmount = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FillCombo()
        Try
            FillDropDown(Me.cmbProperty, "SELECT PropertyProfileId, DocNo FROM PropertyProfile")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmProfitSharing_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Try
        '    FillCombo()
        '    Me.txtVoucherNo.Text = GetVoucherNo()
        '    Me.grdDetail.DataSource = ProfitSharingDetailDAL.GetPropertyProfile(4)
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Sub New(ByVal PropertyProfileId As Integer, ByVal CostCenterId As Integer, ByVal SellerBallance As Double, ByVal BuyerBallance As Double)
        Try
            ' This call is required by the designer.
            InitializeComponent()
            ' Add any initialization after the InitializeComponent() call.

            Me.CostCnterId = CostCenterId
            Me.InvestmentAccountId = Val(getConfigValueByType("InvestmentBookingAccount").ToString)
            Me.ProfitExpenseAccountId = Val(getConfigValueByType("ProfitExpenseAccount").ToString)
            PurchaseAmount = SellerBallance
            If ProfitSharingDetailDAL.IsPropertyProfileExisting(PropertyProfileId) = False Then
                IsEditMode = False
                FillCombo()
                Me.txtVoucherNo.Text = GetVoucherNo()
                Me.cmbProperty.SelectedValue = PropertyProfileId
                'Me.grdDetail.DataSource = ProfitSharingDetailDAL.GetPropertyProfile(PropertyProfileId)
                Me.grdDetail.DataSource = ProfitSharingDetailDAL.GetProfitSharingDetail(PropertyProfileId)
                Me.txtProfitTillDate.Text = (BuyerBallance - SellerBallance).ToString
                Me.txtProfitForDistribution.Text = (BuyerBallance - SellerBallance).ToString
            Else
                IsEditMode = True
                FillCombo()
                Dim dtPS As DataTable = ProfitSharingDetailDAL.GetProfitSharing(PropertyProfileId)
                Me.dtpDate.Value = dtPS.Rows(0).Item("SharingDate")
                Me.txtVoucherNo.Text = dtPS.Rows(0).Item("voucher_no").ToString
                Me.cmbProperty.SelectedValue = Val(dtPS.Rows(0).Item("PropertyProfileId").ToString)
                ProfitSharingId = Val(dtPS.Rows(0).Item("ProfitSharingId").ToString)
                Dim dt As New DataTable
                Dim InvestorId As Integer = 0
                dt = ProfitSharingDetailDAL.GetProfitSharingDetail(Val(dtPS.Rows(0).Item("PropertyProfileId").ToString), DecimalPointInValue)
                'For Each Row As DataRow In dt.Rows
                '    If (Val(Row.Item("InvestorId").ToString) = InvestorId) Then
                '        Row.BeginEdit()
                '        Row.Delete()
                '        Row.EndEdit()
                '    Else
                '        InvestorId = Val(Row.Item("InvestorId").ToString)
                '    End If
                '    If InvestorName = "" Then
                '        InvestorName = Row.Item("InvestorName").ToString
                '    Else
                '        InvestorName = InvestorName & "," & Row.Item("InvestorName").ToString
                '    End If
                'Next
                Me.grdDetail.DataSource = dt 'ProfitSharingDetailDAL.GetProfitSharingDetail(Val(dtPS.Rows(0).Item("PropertyProfileId").ToString))
                Me.txtProfitForDistribution.Text = Val(dtPS.Rows(0).Item("ProfitForDistribution").ToString)
                Me.txtProfitTillDate.Text = Val(dtPS.Rows(0).Item("ProfitForDistribution").ToString)
            End If

            ''Below lines of code are commented against TASK TFS4897 ON 07-11-2018
            'If ProfitSharingDetailDAL.IsPropertyProfileExisting(PropertyProfileId) = False Then
            '    IsEditMode = False
            '    FillCombo()
            '    Me.txtVoucherNo.Text = GetVoucherNo()
            '    Me.cmbProperty.SelectedValue = PropertyProfileId
            '    Me.grdDetail.DataSource = ProfitSharingDetailDAL.GetPropertyProfile(PropertyProfileId)
            '    Me.txtProfitTillDate.Text = (BuyerBallance - SellerBallance).ToString
            '    Me.txtProfitForDistribution.Text = (BuyerBallance - SellerBallance).ToString
            'Else
            '    IsEditMode = True
            '    FillCombo()
            '    Dim dtPS As DataTable = ProfitSharingDetailDAL.GetProfitSharing(PropertyProfileId)
            '    Me.dtpDate.Value = dtPS.Rows(0).Item("SharingDate")
            '    Me.txtVoucherNo.Text = dtPS.Rows(0).Item("voucher_no").ToString
            '    Me.cmbProperty.SelectedValue = Val(dtPS.Rows(0).Item("PropertyProfileId").ToString)
            '    ProfitSharingId = Val(dtPS.Rows(0).Item("ProfitSharingId").ToString)
            '    Dim dt As New DataTable
            '    Dim InvestorId As Integer = 0
            '    dt = ProfitSharingDetailDAL.GetProfitSharingDetail(Val(dtPS.Rows(0).Item("PropertyProfileId").ToString), DecimalPointInValue)
            '    For Each Row As DataRow In dt.Rows

            '        If (Val(Row.Item("InvestorId").ToString) = InvestorId) Then

            '            Row.BeginEdit()
            '            Row.Delete()
            '            Row.EndEdit()

            '        Else

            '            InvestorId = Val(Row.Item("InvestorId").ToString)

            '        End If
            '        If InvestorName = "" Then
            '            InvestorName = Row.Item("InvestorName").ToString
            '        Else
            '            InvestorName = InvestorName & "," & Row.Item("InvestorName").ToString
            '        End If
            '    Next

            '    Me.grdDetail.DataSource = dt 'ProfitSharingDetailDAL.GetProfitSharingDetail(Val(dtPS.Rows(0).Item("PropertyProfileId").ToString))
            '    Me.txtProfitForDistribution.Text = Val(dtPS.Rows(0).Item("ProfitForDistribution").ToString)
            '    Me.txtProfitTillDate.Text = Val(dtPS.Rows(0).Item("ProfitForDistribution").ToString)

            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            FillModel()
            If ProfitSharingMasterDAL.Add(Obj) Then
                msg_Information("Record has been saved successfully.")
                ResetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtProfitForDistribution_TextChanged(sender As Object, e As EventArgs) Handles txtProfitForDistribution.TextChanged
        Try
            Dim dt As DataTable
            dt = CType(Me.grdDetail.DataSource, DataTable)
            If Val(txtProfitForDistribution.Text) > 0 Then
                Me.grdDetail.RootTable.Columns("Inv").FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns("Inv").TotalFormatString = "N" & DecimalPointInValue
                dt.Columns(GridDetail.InvPer).Expression = "InvestmentAmount*100/" & PurchaseAmount & ""
                dt.Columns("ProfitPortion").Expression = "Inv/100*" & Val(Me.txtProfitForDistribution.Text) & ""
                dt.Columns("ProfitShare").Expression = "ProfitPortion*Profit/100"
                dt.Columns("NetProfitAmount").Expression = "ProfitShare+AdjustmentAmount"
                'dt.Columns(GridDetail.InvPer).Expression = "ProfitPortion*100/" & Val(Me.txtProfitForDistribution.Text) & ""
                dt.AcceptChanges()
            Else
                If Me.txtProfitForDistribution.Text = String.Empty Then
                    dt.Columns("ProfitPortion").Expression = 0
                    dt.AcceptChanges()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            FillModel()
            If IsEditMode = False Then

                If Me.InvestmentAccountId > 0 AndAlso Me.ProfitExpenseAccountId > 0 Then

                    If ProfitSharingMasterDAL.Add(Obj) Then
                        msg_Information("Record has been saved successfully.")
                        ResetControls()
                        Me.Close()
                    End If

                Else

                    ShowErrorMessage("Please Select Investment Account or Expense Account")

                End If
            Else

                If Me.InvestmentAccountId > 0 AndAlso Me.ProfitExpenseAccountId > 0 Then

                    If ProfitSharingMasterDAL.DeleteVoucher(txtVoucherNo.Text) = True Then

                        If ProfitSharingMasterDAL.Update(Obj) Then
                            msg_Information("Record has been updatd successfully.")
                            ResetControls()
                            Me.Close()
                        End If

                    Else

                        ShowErrorMessage("Some error occur while updating")

                    End If

                Else

                    ShowErrorMessage("Please Select Investment Account or Expense Account")

                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetail_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.CellUpdated
        Try
            Dim dt As DataTable = CType(Me.grdDetail.DataSource, DataTable)
            ''dt.Columns(GridDetail.InvPer).Expression = "ProfitPortion*100/" & Val(Me.txtProfitForDistribution.Text) & ""
            'dt.Columns("ProfitShare").Expression = "ProfitPortion*Profit/100"
            'dt.Columns("NetProfitAmount").Expression = "ProfitShare+AdjustmentAmount"
            'dt.Columns(GridDetail.InvPer).Expression = "ProfitPortion*100/" & Val(Me.txtProfitForDistribution.Text) & ""
            dt.Columns(GridDetail.InvPer).Expression = "InvestmentAmount*100/" & Val(PurchaseAmount) & ""
            dt.Columns("ProfitPortion").Expression = "Inv/100*" & Val(Me.txtProfitForDistribution.Text) & ""
            dt.Columns("ProfitShare").Expression = "ProfitPortion*Profit/100"
            dt.Columns("NetProfitAmount").Expression = "ProfitShare+AdjustmentAmount"
            dt.AcceptChanges()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class
Structure GridDetail
    Shared ProfitSharingDetailId As String = "ProfitSharingDetailId"
    Shared ProfitSharingId As String = "ProfitSharingId"
    Shared InvestorId As String = "InvestorId"
    Shared InvestorName As String = "InvestorName"
    Shared Cell As String = "Cell"
    Shared InvPer As String = "Inv"
    Shared ProfitPortion As String = "ProfitPortion"
    Shared ProfitPer As String = "Profit"
    Shared ProfitShare As String = "ProfitShare"
    Shared Adjustment As String = "AdjustmentAmount"
    Shared NetProfit As String = "NetProfitAmount"
    Shared InvestmentBookingId As String = "InvestmentBookingId"
End Structure