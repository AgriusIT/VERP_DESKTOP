Imports SBDal
Imports SBModel
Public Class frmProInvestmentBooking
    Implements IGeneral
    Dim objModel As InvestmentBookingBE
    Dim objDAL As InvestmentBookingDAL
    Public Shared blnEditMode As Boolean = False
    Public Shared ProInvestmentId As Integer = 0I
    Public Shared editInvestor As Integer = 0
    Public Shared PropertyProfileId As Integer = 0I
    Public Shared InvestmentAmount As Double = 0I
    Public Shared CostCenterId As Double = 0I
    Public PlotNo As String

    Private Sub frmProInvestmentBooking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Dim AccountSubSubId As Integer = Val(getConfigValueByType("InvestorSubSub").ToString)

        If AccountSubSubId <= 0 Then

            ShowErrorMessage("Please Select a sub sub Account to map Against the Investor")

        End If

        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor

        ' Tooltip
        Ttip.SetToolTip(Me.dtpDate, "Select date")
        Ttip.SetToolTip(Me.txtVoucherNo, "Enter voucher number")
        Ttip.SetToolTip(Me.cmbProperty, "Select property")
        Ttip.SetToolTip(Me.txtInvestmentReq, "Enter investment required")
        Ttip.SetToolTip(Me.cmbInvestor, "Select investor")
        Ttip.SetToolTip(Me.txtAvailableBal, "Enter available balance")
        Ttip.SetToolTip(Me.txtInvestmentPer, "Enter investment percentage")
        Ttip.SetToolTip(Me.txtInvestmentAmount, "Enter investment amount")
        Ttip.SetToolTip(Me.txtRemarks, "Enter remarks")

        ReSetControls()

        Me.cmbProperty.SelectedValue = PropertyProfileId
        Me.cmbProperty.Enabled = False
        If (editInvestor = 1) Then
            Me.cmbInvestor.Enabled = False
            editInvestor = 0
        Else
            Me.cmbInvestor.Enabled = True
        End If

        GetAllRecords()

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnCancel.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnSave.Enabled = False
            Me.btnCancel.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Save" Then
                    If blnEditMode = False Then Me.btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Update" Then
                    If blnEditMode = True Then Me.btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Cancel" Then
                    Me.btnCancel.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New InvestmentBookingDAL
            objDAL.Delete(objModel)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "Property" Then
                FillDropDown(Me.cmbProperty, "SELECT PropertyProfile.PropertyProfileId, tblDefCostCenter.Name FROM PropertyProfile LEFT OUTER JOIN tblDefCostCenter ON PropertyProfile.CostCenterId = tblDefCostCenter.CostCenterID ORDER BY PropertyProfile.PropertyProfileId", True)
            ElseIf Condition = "Investor" Then
                FillDropDown(Me.cmbInvestor, "SELECT coa_detail_id, Name FROM Investor WHERE Active = 1 ORDER BY coa_detail_id", True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New InvestmentBookingBE
            If blnEditMode = False Then
                objModel.InvestmentBookingId = 0
                objModel.VoucherNo = GetDocumentNo()
            Else
                objModel.InvestmentBookingId = ProInvestmentId
                objModel.VoucherNo = Me.txtVoucherNo.Text
            End If
            objModel.PLotNo = PlotNo
            objModel.BookingDate = Me.dtpDate.Value
            objModel.PropertyProfileId = Me.cmbProperty.SelectedValue
            objModel.PropertyProfile = Me.cmbProperty.Text
            objModel.InvestorId = Me.cmbInvestor.SelectedValue
            objModel.Investor = Me.cmbInvestor.Text
            objModel.InvestmentAmount = Val(Me.txtInvestmentAmount.Text)
            objModel.Remarks = Me.txtRemarks.Text
            objModel.ProfitPercentage = Val(Me.txtProfitPercentage.Text)
            objModel.InvestmentAccountId = CType(getConfigValueByType("InvestmentBookingAccount"), Integer)
            objModel.VoucherId = GetVoucherId("frmProInvestmentBooking", objModel.VoucherNo)
            objModel.InvestmentRequired = Val(txtInvestmentReq.Text)
            objModel.ActivityLog = New ActivityLog
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            objDAL = New InvestmentBookingDAL
            Dim dt As DataTable = objDAL.GetById(ProInvestmentId)
            If dt.Rows.Count > 0 Then
                ProInvestmentId = dt.Rows(0).Item("InvestmentBookingId").ToString
                Me.dtpDate.Value = dt.Rows(0).Item("BookingDate").ToString
                Me.txtVoucherNo.Text = dt.Rows(0).Item("VoucherNo").ToString
                Me.cmbProperty.SelectedValue = Val(dt.Rows(0).Item("PropertyProfileId").ToString)
                Me.cmbInvestor.SelectedValue = Val(dt.Rows(0).Item("InvestorId").ToString)
                Me.txtInvestmentAmount.Text = dt.Rows(0).Item("InvestmentAmount").ToString
                Me.txtProfitPercentage.Text = dt.Rows(0).Item("ProfitPercentage").ToString
                Me.txtInvestmentReq.Text = dt.Rows(0).Item("InvestmentRequired").ToString
                Dim answer As Double = Val(dt.Rows(0).Item("InvestmentRequired").ToString) / Val(dt.Rows(0).Item("InvestmentAmount").ToString)
                Me.txtInvestmentPer.Text = 100 / answer
                Me.txtRemarks.Text = dt.Rows(0).Item("Remarks").ToString
                blnEditMode = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtVoucherNo.Text = "" Then
                msg_Error("Voucher No is empty")
                Return False
            End If


            If Me.cmbInvestor.SelectedValue < 1 Then
                msg_Error("Investor is required.")
                Me.cmbInvestor.Focus()
                Return False
            End If
            If Val(Me.txtInvestmentAmount.Text) <= 0 Then
                msg_Error("Investment is required.")
                Me.txtInvestmentAmount.Focus()
                Return False
            End If
            If blnEditMode = False Then
                Dim strSQL As String
                strSQL = "select InvestorId from InvestmentBooking where PropertyProfileId = " & Me.cmbProperty.SelectedValue & ""
                Dim Dt As DataTable = GetDataTable(strSQL)
                Dim i As Integer = 0
                For i = 0 To Dt.Rows.Count - 1
                    If cmbInvestor.SelectedValue = Dt.Rows(i).Item("InvestorId") Then
                        ShowErrorMessage("This Investor Already Exists")
                        Me.cmbInvestor.Focus()
                        Exit Function
                    End If
                Next
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            'ProInvestmentId = 0
            cmbProperty.Focus()
            FillCombos("Property")
            FillCombos("Investor")
            Me.dtpDate.Value = Date.Now
            Me.txtVoucherNo.Text = GetDocumentNo()
            Me.cmbProperty.SelectedValue = 0
            Me.txtInvestmentReq.Text = 0
            Me.cmbInvestor.SelectedValue = 0
            Me.txtInvestmentPer.Text = 0
            Me.txtAvailableBal.Text = 0
            Me.txtInvestmentAmount.Text = 0
            Me.txtProfitPercentage.Text = 0
            Me.txtRemainingBalance.Text = ""
            Me.txtRemarks.Text = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New InvestmentBookingDAL
            If IsValidate() = True Then
                objDAL.Add(objModel)
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New InvestmentBookingDAL
            If IsValidate() = True Then
                objDAL.Update(objModel)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("JV-" + Microsoft.VisualBasic.Right(Me.dtpDate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("JV-" & Format(Me.dtpDate.Value, "yy") & Me.dtpDate.Value.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
            Else
                Return GetNextDocNo("JV-", 6, "tblVoucher", "voucher_no")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If blnEditMode = False Then
                If Save() = True Then
                    ReSetControls()
                    GetAllRecords()
                    msg_Information(str_informSave)
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                Update1()
                ReSetControls()
                ProInvestmentId = 0
                GetAllRecords()
                blnEditMode = False
                msg_Information(str_informUpdate)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            ReSetControls()
            blnEditMode = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbInvestor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbInvestor.SelectedIndexChanged
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT SUM(debit_amount) AS Balance FROM tblVoucherDetail WHERE (CostCenterID = " & Me.cmbProperty.SelectedValue & ") " & IIf(Me.cmbInvestor.SelectedValue IsNot Nothing, "AND (coa_detail_id = " & Me.cmbInvestor.SelectedValue & ")", "") & ""
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Me.txtAvailableBal.Text = Val(dt.Rows(0).Item(0).ToString)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtInvestmentPer_TextChanged(sender As Object, e As EventArgs) Handles txtInvestmentPer.TextChanged
        Try
            If Not Me.txtInvestmentPer.Text = "" Then
                Me.txtInvestmentAmount.Text = Val(Me.txtInvestmentReq.Text * Me.txtInvestmentPer.Text) / 100
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtInvestmentAmount_TextChanged(sender As Object, e As EventArgs) Handles txtInvestmentAmount.TextChanged
        Try
            If Not Me.txtInvestmentAmount.Text = "" Then
                Me.txtRemainingBalance.Text = Val(Me.txtAvailableBal.Text) - Val(Me.txtInvestmentAmount.Text)
            End If
            If txtInvestmentAmount.Text = "" Then
                lblNumberConvert.Text = ""
            Else
                lblNumberConvert.Text = ModGlobel.NumberToWords(Me.txtInvestmentAmount.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProInvestmentBooking_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        blnEditMode = False
    End Sub
End Class