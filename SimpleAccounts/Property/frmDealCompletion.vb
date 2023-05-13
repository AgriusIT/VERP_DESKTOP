Imports SBDal
Imports SBModel
Public Class frmDealCompletion

    Dim objModel As PropertyProfileAgentDealerBE
    Dim objModelList As List(Of PropertyProfileAgentDealerBE)
    Dim objDAL As PropertyProfileAgentDealerDAL
    Dim _dtAgentAndDealer As DataTable
    Private Const _Agent As String = "Agent"
    Dim _PlotNo As String = String.Empty
    Dim _IsDealCompleted As Boolean = False
    Dim PropertyProfileId As Integer = 0
    Dim SalesAmountForDealCompletion As Double = 0 ''TFS4496
    Dim PurchaseAmountForDealCompletion As Double = 0 ''TFS4496
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub New(ByVal SalesAmount As Double, ByVal PurchaseAmount As Double, ByVal CommissionAgentDealer As Double, ByVal ProfitShared As Double, ByVal OtherExpenses As Double, ByVal NDCExpenses As Double, ByVal Commission As Double, ByVal PlotNo As String, ByVal IsDealCompleted As Boolean, ByVal dtAgentAndDealer As DataTable, ByVal PropertyProfileId As Integer, ByVal dtInvestor As DataTable)
        ' This call is required by the designer.
        InitializeComponent()
        _dtAgentAndDealer = dtAgentAndDealer

        _PlotNo = PlotNo
        ''Start TFS4496
        If SalesAmount <> PurchaseAmount Or CommissionAgentDealer > 0 Or ProfitShared > 0 Or OtherExpenses > 0 Or dtAgentAndDealer.Rows.Count > 0 Or dtInvestor.Rows.Count > 0 Then
            Me.btnQuickDealComplete.Visible = False
        Else
            Me.btnQuickDealComplete.Visible = True
        End If
        ''End TFS4496
        If IsDealCompleted = True Then
            Me.btnCompleteDeal.Enabled = False
            Me.btnQuickDealComplete.Enabled = False
        End If
        Me._IsDealCompleted = IsDealCompleted
        Me.PropertyProfileId = PropertyProfileId
        SalesAmountForDealCompletion = SalesAmount
        PurchaseAmountForDealCompletion = PurchaseAmount
        Display(SalesAmount, PurchaseAmount, CommissionAgentDealer, ProfitShared, OtherExpenses, NDCExpenses, Commission)
        ' Add any initialization after the InitializeComponent() call.
    End Sub


    Private Sub Display(ByVal SalesAmount As Double, ByVal PurchaseAmount As Double, ByVal CommissionAgentDealer As Double, ByVal ProfitShared As Double, ByVal OtherExpenses As Double, ByVal NDCExpenses As Double, ByVal Commission As Double)
        Try
            Me.lblSale.Text = PurchaseAmount
            Me.lblPurchase.Text = SalesAmount
            Me.lblMargin.Text = PurchaseAmount - SalesAmount
            ' Me.lblGrossProfit.Text = PurchaseAmount - SalesAmount ''TRFS4496
            Me.lblCommission.Text = Commission
            Me.lblCommissionAgentDealer.Text = CommissionAgentDealer
            Me.lblGrossProfit.Text = Val(lblMargin.Text) + Commission ''TRFS4496
            Me.lblProfitShared.Text = ProfitShared
            Me.lblOtherExpenses.Text = OtherExpenses
            Me.lblNDCExpense.Text = NDCExpenses
            Me.lblTotalExpenses.Text = CommissionAgentDealer + ProfitShared + OtherExpenses + NDCExpenses
            Me.lblNetProfit.Text = Val(Me.lblGrossProfit.Text) - Val(Me.lblTotalExpenses.Text)
            'Dim VoucherDate As DateTime
            objDAL = New PropertyProfileAgentDealerDAL()
            For Each Row As DataRow In _dtAgentAndDealer.Rows
                If Row.Item("VoucherNo").ToString.Length > 0 Then
                    dtpDate.Value = objDAL.GetVoucherDate(Row.Item("VoucherNo").ToString)
                    Exit For
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub RefreshControls()
        Try
            Me.lblSale.Text = 0
            Me.lblPurchase.Text = 0
            Me.lblGrossProfit.Text = 0
            Me.lblCommissionAgentDealer.Text = 0
            Me.lblCommission.Text = 0
            Me.lblNDCExpense.Text = 0
            Me.lblProfitShared.Text = 0
            Me.lblOtherExpenses.Text = 0
            Me.lblTotalExpenses.Text = 0
            Me.lblNetProfit.Text = 0
            dtpDate.Value = Now
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "")
        ''strSQL = "SELECT PropertyProfileAgent.PropertyProfileAgentId AS Id, PropertyProfileAgent.PropertyProfileId, PropertyProfileAgent.AgentId AS AccountId, Agent.Name, 'Agent' AS Type, PropertyProfileAgent.Activity, PropertyProfileAgent.CommissionAmount, PropertyProfileAgent.Remarks, PropertyProfileAgent.VoucherNo FROM PropertyProfileAgent INNER JOIN Agent ON PropertyProfileAgent.AgentId = Agent.AgentId where PropertyProfileAgent.PropertyProfileId=" & ProfileId & " UNION ALL SELECT PropertyProfileDealer.PropertyProfileDealerId AS Id, PropertyProfileDealer.PropertyProfileId, PropertyProfileDealer.DealerId AS AccountId, Dealer.Name, 'Dealer' AS Type, PropertyProfileDealer.Activity, PropertyProfileDealer.CommissionAmount, PropertyProfileDealer.Remarks, PropertyProfileDealer.VoucherNo FROM PropertyProfileDealer INNER JOIN Dealer  ON PropertyProfileDealer.DealerId = Dealer.DealerId where PropertyProfileDealer.PropertyProfileId=" & ProfileId
        Try
            ''Below if condition is removed against TASK TFS4183
            'If _dtAgentAndDealer.Rows.Count > 0 Then
            objModelList = New List(Of PropertyProfileAgentDealerBE)
            _dtAgentAndDealer.AcceptChanges()
            For Each Row As DataRow In _dtAgentAndDealer.Rows
                objModel = New PropertyProfileAgentDealerBE
                If Row.Item("Type").ToString = _Agent Then
                    objModel.PropertyProfileAgentId = Row.Item("Id")
                    objModel.AgentId = Row.Item("AccountId")

                    objModel.PropertyProfileDealerId = 0
                    objModel.DealerId = 0

                Else
                    objModel.PropertyProfileDealerId = Row.Item("Id")
                    objModel.PropertyProfileAgentId = 0
                    objModel.DealerId = Row.Item("AccountId")
                    objModel.AgentId = 0
                End If
                objModel.VoucherNo = Row.Item("VoucherNo")
                objModel.VoucherDate = dtpDate.Value
                objModel.PropertyProfileId = Val(Row.Item("PropertyProfileId").ToString)
                objModel.PlotNo = _PlotNo
                objModel.name = Row.Item("Name").ToString
                objModel.Remarks = Row.Item("Remarks").ToString

                objModel.CommissionAmount = Val(Row.Item("CommissionAmount").ToString)
                objModel.CommissionAccount = getConfigValueByType("CommissionAccount").ToString
                objModel.Activity = Row.Item("Activity").ToString
                objModel.ActivityLog = New ActivityLog
                objModelList.Add(objModel)
            Next
            'End If


            'objModel = New PropertyProfileAgentDealerBE
            'objModel.PropertyProfileId = PropertyProfileId
            'If Me.grd.RowCount > 0 Then
            '    If blnEditMode = True Then
            '        If Me.rbtnDealers.Checked = True Then
            '            objModel.PropertyProfileDealerId = AgentID 'Val(Me.grd.GetRow.Cells("Id").Value.ToString)
            '            objModel.PropertyProfileAgentId = 0
            '        Else
            '            objModel.PropertyProfileAgentId = AgentID
            '            objModel.PropertyProfileDealerId = 0
            '        End If
            '    End If
            'End If
            'If Me.rbtnDealers.Checked = True Then
            '    objModel.DealerId = Me.cmbDealers.SelectedValue
            '    objModel.AgentId = 0
            'Else
            '    objModel.AgentId = Me.cmbDealers.SelectedValue
            '    objModel.DealerId = 0
            'End If
            'If PSID = 0 Then
            '    objModel.VoucherNo = GetDocumentNo()
            'Else
            '    objModel.VoucherNo = VoucherNo
            'End If
            'objModel.PlotNo = PlotNo
            'objModel.name = Me.cmbDealers.Text
            'objModel.Remarks = Me.txtRemarks.Text
            'objModel.CommissionAmount = Me.txtCommission.Text
            'objModel.CommissionAccount = getConfigValueByType("CommissionAccount").ToString
            'objModel.Activity = Me.cmbActivity.Text
            'objModel.ActivityLog = New ActivityLog
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function GetDocumentNo() As String
        Dim DocNo As String = String.Empty
        Try
            DocNo = GetNextDocNo("JV-" & Format(Date.Now, "yy") & Date.Now.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
            Return DocNo
        Catch ex As Exception
            msg_Error(ex.Message)
            Return ""
        End Try
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            If Me._IsDealCompleted = True Then
                FillModel()
                objDAL = New PropertyProfileAgentDealerDAL()
                If objDAL.CancelDeal(objModelList, PropertyProfileId) Then
                    msg_Information("Deal has been cancelled successfully.")
                    frmPropertyProfile.IsDealCompleted = False
                Else
                    msg_Information("Faild to cancel the deal")
                End If
            End If
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDealCompletion_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub frmDealCompletion_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            _dtAgentAndDealer = Nothing
            objModelList = Nothing
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCompleteDeal_Click(sender As Object, e As EventArgs) Handles btnCompleteDeal.Click
        Try
            FillModel()
            ''Below two condition are removed against TASK TFS4183
            'If Not objModelList Is Nothing Then
            'If objModelList.Count > 0 Then
            objDAL = New PropertyProfileAgentDealerDAL()
            If objDAL.AddVoucher(objModelList, PropertyProfileId) Then
                msg_Information("Record has been saved successfuly.")
                RefreshControls()
                frmPropertyProfile.IsDealCompleted = True
                Me.Close()
            End If
            'Else
            '    ShowErrorMessage("No record found to make voucher")
            'End If
            'Else
            'ShowErrorMessage("No record found to make voucher")
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnQuickDealComplete_Click(sender As Object, e As EventArgs) Handles btnQuickDealComplete.Click
        Try
            If SalesAmountForDealCompletion <> PurchaseAmountForDealCompletion Then
                ShowErrorMessage("SalesAmount is not equal to PurchaseAmount Deal can not be closed")
                Exit Sub
            Else
                objDAL = New PropertyProfileAgentDealerDAL()
                If objDAL.AddVoucherForDealCompletion(GetDocumentNo(), PropertyProfileId, SalesAmountForDealCompletion, PurchaseAmountForDealCompletion) Then
                    msg_Information("Deal Completed successfuly.")
                    RefreshControls()
                    frmPropertyProfile.IsDealCompleted = True
                    Me.Close()
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Function GetDocumentNo() As String
    '    Try
    '        If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
    '            Return GetSerialNo("JV-" + Microsoft.VisualBasic.Right(dtpDate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
    '        ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
    '            Return GetNextDocNo("JV-" & Format(dtpDate.Value, "yy") & dtpDate.Value.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
    '        Else
    '            Return GetNextDocNo("JV-", 6, "tblVoucher", "Voucher_No")
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

   
End Class