''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''7-Jan-2014   Task:2370       Imran Ali     Sale and purchase invoice wise aging report 
''13-Jan-2014   Task:2375        Imran Ali        Covnerter Problems And Development
'' 16-Jan-2014    TASK:2382        Imran Ali         Add Field Payee Title In Voucher And Invoice Based Payment
'' 24-Jan-2014     TASK:2381          Imran Ali          Reasign Invoice Based Payment object  
''29-Jan-2014 TASK:2398           Imran Ali        Update, Delete Problem in Cash Management.
''07-Mar-2014  TASK:2470  IMRAN ALI  Add month in invoice base payment narration
''01-Apr-2014 TASK:2534 Imran Ali Automobile Development/Problem
''04-Apr-2014 TASK2539 Imran Ali  Add Debit No/Credit Amount Fields on InvoiceBasedPayment/Receipt 
' Task No 2537 Amendments Regarding Post checkBox ,Current Balance and Sale Tax 
'26-4-2014 TASK:M35 Imran Ali Show All Record 
'' 26-May-2014 TASK:2647 Imran Ali New Enhancement 
''19-June-2014 TASK:2699 Imran Ali Apply WithHolding Tax After Other And SalexTax Deduction On Invoice based payment
''16-Jul-2014 TASK:2745 Imran Ali Cheque Comments On Ledger (Ravi)
''24-Jul-2014 Task:2759 Imran ali Amount Round on all transaction forms
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''07-Aug-2014 Task:2775 Imran Ali Add Delete Button Field On Grid Detail in InvoiceBasedReceipt/Payment (Ravi)
''13-Aug-2014 Task:2782 Imran Ali Post Rights Implement On Invoice Base Payment\Receipt Voucher
''3-Sep-2014 TASK:2823 Imran Ali Invoice Aging Formated Report Revised (Ravi)
''12-Sep-2014 TASK:2838 Imran Ali Cheque Print Option in Invoice Based Payment
''13-Sep-2014 Task:2844 Imran Ali Posted Balance Show On Invoice Based Receipt/Payment
'2015-02-20 Task # 6 Add Payee Title in customer list By Ali Ansari
'04-Aug-2015 Task#04082015 Ahmad Shari: Left Outer Join CompanyDefTable with tblVoucher and  Add Column CompanyName from CompanyDefTable in query,add company drop down on designer , and Fill Drop down with companies for setting company wise invoice number

Imports SBDal
Imports SBModel
Imports SBUtility
Imports SBUtility.Utility
Imports Janus.Windows.GridEX
Imports Neodynamic.SDK.Barcode



Public Class frmPaymentVoucherNew
    Implements IGeneral
    Dim ObjMod As InvoicesBasedPaymentMaster
    Dim MasterID As Integer = 0
    Dim EditMode As Boolean = False
    Dim strInvoiceRemark As String
    Dim PrintLog As PrintLogBE
    Dim DiscountAccountId As Integer = 0I
    Dim flgInvoiceWiseTaxPercent As Boolean = False
    Dim EnabledBrandedSMS As Boolean = False
    Public Event SelectedIndexChanged As EventHandler
    Dim DeductionWHTaxOnTotal As Boolean = False
    Dim flgCompanyRights As Boolean = False


    Enum enmPaymentMaster
        PaymentID
        PaymentDate
        PaymentNo
        Remarks
        VendorCode
        VendorName
        PaymentAmount
        ChequeNo
        ChequeDate
        PaymentMethod
        PaymentAccountId
        PayeeTitle
    End Enum
    Enum enmGrdPaymentDetail
        PaymentDetailId
        ReceivingID
        ReceivingNo
        ReceivingDate
        Remarks
        Gst_Percentage
        Gst_Amount
        'Task:2370 Added Index
        InvoiceTax
        TaxPercent
        SalesTaxAmount
        OtherTaxAmount
        OtherTaxAccountId
        'End Task:2370
        NetAmount 'Task:2775 Added Index
        ReceivingAmount
        OtherPayment
        PaidAmount
        BalanceAmount
        Memo
        Description 'Task:2470 Added Index
        CostCenter  'Task 3199 Add CostCentre in detail grid
        PaymentId
    End Enum

    Private Sub frmPaymentVoucherNew_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
            If e.KeyCode = Keys.F4 Then
                If Me.BtnSave.Enabled = True Then
                    SaveToolStripButton_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(BtnNew, Nothing)
                Exit Sub
            End If

            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                BtnPrint_ButtonClick(BtnPrint, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.F5 Then
                BtnRefresh_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Insert Then
                BtnAdd_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.U AndAlso e.Alt Then
                If Me.BtnSave.Text = "&Update" Then
                    If Me.BtnSave.Enabled = False Then
                        RemoveHandler Me.BtnSave.Click, AddressOf Me.SaveToolStripButton_Click
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmPaymentVoucherNew_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14 
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Me.FillPaymentMethod()
            Me.cmbPaymentMethod.SelectedIndex = 0
            Me.cmbPaymentMethod.Show()
            FillCombos("Customer")
            FillCombos("Payfrom")
            FillCombos("CostCenter")
            FillCombos("Invoice")

            'Task#04082015 fill combo box with company names
            FillCombos("Company")

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            'End Task#04082015

            'GetAllRecords()
            'Me.DisplayRecord(-1)
            If Me.cmbPaymentMethod.SelectedText = "Cash" Then
                Me.lblChequeNo.Visible = False
                Me.lblChequeDate.Visible = False
                Me.txtChequeNo.Visible = False
                Me.DtpChequeDate.Visible = False
                Me.Label15.Visible = False
                Me.txtPayeeTitle.Visible = False
            End If


            If Not getConfigValueByType("SalesDiscountAccount").ToString = "Error" Then
                DiscountAccountId = getConfigValueByType("SalesDiscountAccount").ToString
            Else : DiscountAccountId = 0
            End If
            If Not getConfigValueByType("InvoiceWiseTaxPercent").ToString = "Error" Then
                flgInvoiceWiseTaxPercent = getConfigValueByType("InvoiceWiseTaxPercent").ToString
            Else
                flgInvoiceWiseTaxPercent = False
            End If
            If Not getConfigValueByType("EnabledBrandedSMS").ToString = "Error" Then
                EnabledBrandedSMS = getConfigValueByType("EnabledBrandedSMS")
            End If
            If Not getConfigValueByType("DeductionWHTaxOnTotal").ToString = "Error" Then
                DeductionWHTaxOnTotal = getConfigValueByType("DeductionWHTaxOnTotal")
            End If


            'TFS3360_Aashir_Added select/contain filters on account feilds in all transaction screens
            UltraDropDownSearching(cmbAccounts, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)

            Me.ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub frmPaymentVoucherNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseKey = "KD83KNYYMA6XA6E7HNEL7QEUSGXGLKPQVCH7YVMEPXD7BG5FUCPA"
            Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseOwner = "LumenSoft Technologies-Standard Edition-OEM Developer License"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillPaymentMethod()
        Try
            'blnFirstTimeInvoked = True
            Dim dt As New DataTable
            Dim dr As DataRow
            Dim dr1 As DataRow
            dt.Columns.Add("Id")
            dt.Columns.Add("Name")
            dr = dt.NewRow
            dr1 = dt.NewRow

            dr(0) = Convert.ToInt32(4)
            dr(1) = "Bank"
            dt.Rows.InsertAt(dr, 0)

            dr1(0) = Convert.ToInt32(2)
            dr1(1) = "Cash"
            dt.Rows.InsertAt(dr1, 0)

            Me.cmbPaymentMethod.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            Me.cmbPaymentMethod.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            Me.cmbPaymentMethod.DataSource = dt

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub GridAddITems()
        Try
            If Me.cmbInvoiceList Is Nothing Then Exit Sub 'Task:2375 Check Invoices
            Dim Dt As New DataTable
            Dt = CType(Me.GrdPaymentDetail.DataSource, DataTable)
            Dt.AcceptChanges()
            'If GetFilterDataFromDataTable(Dt, "VendorId=" & Me.cmbAccounts.Value).Count > 0 Then
            '    ShowErrorMessage("Already Record Exist In Grid")
            '    Exit Sub
            'End If
            Dim drFound() As DataRow = Dt.Select("ReceivingId=" & Me.cmbInvoiceList.Value & "")
            If drFound.Length > 0 Then
                ShowErrorMessage("Invoice No. [" & cmbInvoiceList.ActiveRow.Cells("ReceivingNo").Text.ToString & "] already exist")
                Exit Sub
            End If
            Dim Dr As DataRow
            Dr = Dt.NewRow
            Dr(enmGrdPaymentDetail.ReceivingID) = Me.cmbInvoiceList.Value
            Dr(enmGrdPaymentDetail.ReceivingNo) = Me.cmbInvoiceList.SelectedRow.Cells("ReceivingNo").Value
            Dr(enmGrdPaymentDetail.ReceivingDate) = Me.cmbInvoiceList.SelectedRow.Cells("ReceivingDate").Value
            Dr(enmGrdPaymentDetail.Remarks) = Me.txtRemarks.Text
            Dr(enmGrdPaymentDetail.Gst_Percentage) = 0
            Dr(enmGrdPaymentDetail.ReceivingAmount) = Val(Me.txtInvoicoAmount.Text)
            Dr(enmGrdPaymentDetail.OtherPayment) = Val(Me.txtOtherPayment.Text)
            Dr(enmGrdPaymentDetail.PaidAmount) = Val(Me.txtPaidAmt.Text)
            'Task:2370 Added Columns
            Dr(enmGrdPaymentDetail.InvoiceTax) = Val(Me.txtInvoiceTaxAmount.Text)
            Dr(enmGrdPaymentDetail.TaxPercent) = Val(Me.txtTaxPercent.Text)
            Dr(enmGrdPaymentDetail.SalesTaxAmount) = Val(Me.txtSalesTaxAmount.Text)
            Dr(enmGrdPaymentDetail.OtherTaxAmount) = Val(Me.txtOtherTaxAmount.Text)
            Dr(enmGrdPaymentDetail.OtherTaxAccountId) = 0
            'End Task:2370
            Dr(enmGrdPaymentDetail.Memo) = Me.cmbInvoiceList.ActiveRow.Cells("Vendor_Invoice_No").Text

            'Task 3199 Add CostCenter Value in Grid
            Dr(enmGrdPaymentDetail.CostCenter) = Me.cmbCostCenter.SelectedValue

            Dt.Rows.InsertAt(Dr, 0)
            Dt.AcceptChanges()
            'Comment against task:2699
            'Dt.Columns(enmGrdPaymentDetail.Gst_Amount).Expression = "(([Tax]*[Paid Amount])/100)"
            'If flgInvoiceWiseTaxPercent = True Then
            '    Dt.Columns(enmGrdPaymentDetail.Gst_Amount).Expression = "(([Tax]*[Paid Amount])/100)"
            'Else
            '    Dt.Columns(enmGrdPaymentDetail.Gst_Percentage).Expression = "(IIF([Tax Amount]=0,0,([Tax Amount]/[Paid Amount])*100))"
            'End If
            '' Dt.Columns(enmGrdPaymentDetail.Gst_Percentage).Expression = "((([Tax Amount]/[Paid Amount]))*100)"
            'Dt.Columns(enmGrdPaymentDetail.BalanceAmount).Expression = "(([Invoice Amount]-[Paid Amount]))"
            'End Task:2699
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            'Me.GrdPaymentDetail.AutoSizeColumns()

            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GrdPaymentDetail.RootTable.Columns
                'Before against task:2370
                'If Not col.Caption = "Remarks" AndAlso Not col.Caption = "Paid Amount" AndAlso Not col.Caption = "Tax" AndAlso Not col.Caption = "Tax Amount" Then
                '    col.EditType = EditType.NoEdit
                'Task:2370 Set Edit Columns SalesTaxAmount, OtherTaxAmount, OtherTaxAccountId
                'Before against task:2470
                'If Not col.Caption = "Remarks" AndAlso Not col.Caption = "Paid Amount" AndAlso Not col.Caption = "Tax" AndAlso Not col.Caption = "Tax Amount" AndAlso Not col.DataMember = "SalesTaxAmount" AndAlso Not col.DataMember = "OtherTaxAmount" AndAlso Not col.DataMember = "OtherTaxAccountId" Then
                'Task:2470 @ToDo Editable Column Description
                If Not col.Caption = "Remarks" AndAlso Not col.Caption = "Paid Amount" AndAlso Not col.Caption = "Tax" AndAlso Not col.Caption = "Tax Amount" AndAlso Not col.DataMember = "SalesTaxAmount" AndAlso Not col.DataMember = "OtherTaxAmount" AndAlso Not col.DataMember = "OtherTaxAccountId" AndAlso Not col.DataMember = "Description" AndAlso Not col.DataMember = "Other Payment" AndAlso Not col.DataMember = "Tax_Percent" AndAlso Not col.DataMember = "Invoice Tax" AndAlso Not col.DataMember = "CostCenter" Then
                    'End Task:2470
                    col.EditType = EditType.NoEdit
                    'End Task:2370
                End If
                If col.Caption = "Payment Detail Id" Or col.Caption = "Receiving Id" Or col.Caption = "[Tax Amount]" And col.Caption = "Memo" Then
                    col.Visible = False
                End If
            Next
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.PaymentDetailId).Visible = False
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.ReceivingID).Visible = False
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.Remarks).Width = 250
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.ReceivingDate).Width = 100
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.ReceivingNo).Width = 100
            'Me.GrdPaymentDetail.RootTable.Columns("Tax").FormatString = "N"
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.Gst_Amount).AggregateFunction = AggregateFunction.Sum
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.ReceivingAmount).AggregateFunction = AggregateFunction.Sum
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.PaidAmount).AggregateFunction = AggregateFunction.Sum
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.BalanceAmount).AggregateFunction = AggregateFunction.Sum

            'Task:2370 Set Aggregrate Function Sum And Formating

            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.Gst_Amount).TextAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.ReceivingAmount).TextAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.PaidAmount).TextAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.BalanceAmount).TextAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.Gst_Amount).HeaderAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.ReceivingAmount).HeaderAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.PaidAmount).HeaderAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.BalanceAmount).HeaderAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.Gst_Percentage).TextAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.Gst_Percentage).HeaderAlignment = TextAlignment.Far

            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.OtherPayment).TextAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.OtherPayment).HeaderAlignment = TextAlignment.Far

            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.InvoiceTax).AggregateFunction = AggregateFunction.Sum
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.SalesTaxAmount).AggregateFunction = AggregateFunction.Sum
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.OtherTaxAmount).AggregateFunction = AggregateFunction.Sum
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.OtherPayment).AggregateFunction = AggregateFunction.Sum

            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.SalesTaxAmount).TextAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.OtherTaxAmount).TextAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.SalesTaxAmount).HeaderAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.OtherTaxAmount).HeaderAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.OtherTaxAccountId).Caption = "Others Account"
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.SalesTaxAmount).Caption = "Sales Tax"
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.OtherTaxAmount).Caption = "Others"
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.TaxPercent).Caption = "Tax %"
            'End Task:2370
            'Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.Discount).AggregateFunction = AggregateFunction.Sum
            'Task:2375 Column Formating
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.Gst_Percentage).Caption = "W/T Tax %"
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.Gst_Amount).Caption = "W/T Tax Amount"
            'End Task:23757
            'Task:2759
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.Gst_Amount).FormatString = "N" & DecimalPointInValue
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.Gst_Amount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            'End Task:2759
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.BalanceAmount).FormatString = "N" & DecimalPointInValue
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.BalanceAmount).FormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.SalesTaxAmount).FormatString = "N" & DecimalPointInValue
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.SalesTaxAmount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.OtherTaxAmount).FormatString = "N" & DecimalPointInValue
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.OtherTaxAmount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.ReceivingAmount).FormatString = "N" & DecimalPointInValue
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.ReceivingAmount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            'Task:2775 Format
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.NetAmount).FormatString = "N" & DecimalPointInValue
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.NetAmount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.NetAmount).AggregateFunction = AggregateFunction.Sum
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.NetAmount).TextAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.NetAmount).HeaderAlignment = TextAlignment.Far

            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.InvoiceTax).TextAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.InvoiceTax).HeaderAlignment = TextAlignment.Far

            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.TaxPercent).TextAlignment = TextAlignment.Far
            Me.GrdPaymentDetail.RootTable.Columns(enmGrdPaymentDetail.TaxPercent).HeaderAlignment = TextAlignment.Far
            'End Task:2775

            Me.GrdPaymentDetail.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            ''Task:2381 Reasign Invoice Based Payment object
            ObjMod = New InvoicesBasedPaymentMaster
            ObjMod.PVNo = Me.GrdSaved.GetRow.Cells("PaymentNo").Value.ToString
            ObjMod.PaymentNo = Me.GrdSaved.GetRow.Cells("PaymentNo").Value.ToString
            ObjMod.VoucherMaster = New VouchersMaster
            ObjMod.VoucherMaster.VNo = Me.GrdSaved.GetRow.Cells("PaymentNo").Value.ToString
            ObjMod.VoucherMaster.ActivityLog = New ActivityLog
            ObjMod.VoucherMaster.ActivityLog.ApplicationName = "Accounts"
            ObjMod.VoucherMaster.ActivityLog.FormCaption = Me.Text
            ObjMod.VoucherMaster.ActivityLog.UserID = LoginUserId
            ObjMod.VoucherMaster.ActivityLog.LogDateTime = Date.Now
            ObjMod.ActivityLog = New ActivityLog
            ObjMod.ActivityLog.ApplicationName = "Accounts"
            ObjMod.ActivityLog.FormCaption = Me.Text
            ObjMod.ActivityLog.UserID = LoginUserId
            ObjMod.ActivityLog.LogDateTime = Date.Now
            'End Task:2381
            If New InvoicesBasedPaymentDAL().Delete(ObjMod) Then Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty
            If Condition = "Customer" Then
                'Dim str As String = "Select coa_detail_id, detail_title,sub_sub_title AS [Sub Sub A/c], CityName From vwCOADetail WHERE Account_Type='Vendor'  AND detail_title Is Not Null"
                'Marked Against Task# 6 
                'Dim str As String = "Select coa_detail_id, detail_title,detail_code,sub_sub_title AS [Sub Sub A/c], CityName,Contact_Mobile as Mobile From vwCOADetail WHERE Account_Type='Vendor'  AND detail_title Is Not Null"
                'Marked Against Task# 6 

                '2015-02-20 Task # 6 Add Payee Title in customer list By Ali Ansari
                Dim str As String = "Select coa_detail_id, detail_title,detail_code,sub_sub_title AS [Sub Sub A/c], CityName,Contact_Mobile as Mobile, tblvendor.PayeeTitle as  PayeeTitle  From vwCOADetail left outer join tblvendor on vwCOADetail.coa_detail_id = tblvendor.accountid WHERE  detail_title Is Not Null"
                '2015-02-20 Task # 6 Add Payee Title in customer list By Ali Ansari
                ''TFS4683 : 02-10-2018 : Ayesha Rehman : Implemended Show Customer On Purchase Configuration
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    str += " And (Account_Type in ('Vendor','Customer')) "
                Else
                    str += " And Account_Type='Vendor' "
                End If
                If EditMode = False Then
                    str += " AND vwcoadetail.Active=1"
                End If
                str += " ORDER By detail_title"
                FillUltraDropDown(cmbAccounts, str, True)
                Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbAccounts.DisplayLayout.Bands(0).Columns(1).Width = 250
                Me.cmbAccounts.DisplayLayout.Bands(0).Columns(2).Width = 100
                Me.cmbAccounts.DisplayLayout.Bands(0).Columns(3).Width = 150
                Me.cmbAccounts.DisplayLayout.Bands(0).Columns("detail_code").Header.Caption = "Code"
            ElseIf Condition = "Payfrom" Then
                Dim Str As String = "If  exists(select Account_Id FROM tblUserAccountRights where UserID = " & LoginUserId & " And Rights = 1 And Account_Id Is Not Null) " _
                                  & " select coa_detail_id, detail_title from vwCoaDetail where account_type=N'" & Me.cmbPaymentMethod.Text & "' And coa_detail_id in (select Account_Id FROM tblUserAccountRights where UserID = " & LoginUserId & " And Rights = 1) " & IIf(flgCompanyRights = True, " AND vwCoaDetail.CompanyId=" & MyCompanyId & "", "") & " " _
                                  & " Else " _
                                  & " select coa_detail_id, detail_title from vwCoaDetail where account_type=N'" & Me.cmbPaymentMethod.Text & "' " & IIf(flgCompanyRights = True, " AND vwCoaDetail.CompanyId=" & MyCompanyId & "", "") & " "

                'Dim str As String = "select coa_detail_id,detail_title,sub_sub_title AS [Sub Sub A/c] from vwCoaDetail where account_type='" & Me.cmbPaymentMethod.Text & "'  AND detail_title Is Not Null"
                If EditMode = False Then
                    Str += " And vwCOADetail.Active=1"
                End If
                Str += " ORDER BY detail_title"
                FillDropDown(cmbPayFrom, Str, True)

            ElseIf Condition = "Invoice" Then
                'If Me.rbtInvoice.Checked = True Then
                'Before against task:2534
                'strSQL = " SELECT ReceivingMasterTable.ReceivingID, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No, ReceivingMasterTable.ReceivingDate, (ReceivingMasterTable.ReceivingAmount+ISNULL(SalesTax.Tax,0)) as ReceivingAmount,  (ReceivingMasterTable.ReceivingAmount+ISNULL(SalesTax.Tax,0)) - ISNULL(Payments.ReceivedAmount, 0) AS InvoiceBalance " & _
                '      " FROM ReceivingMasterTable INNER JOIN tblCOAMainSubSubDetail ON ReceivingMasterTable.VendorId = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as Tax From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(SELECT invoiceId, SUM((IsNull(PaymentAmount,0)) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedPaymentsDetail GROUP BY InvoiceId) Payments ON ReceivingMasterTable.ReceivingId = Payments.invoiceId " & _
                '      " WHERE (ReceivingMasterTable.ReceivingAmount - ISNULL(Payments.ReceivedAmount, 0) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND ReceivingMasterTable.ReceivingDate<=Convert(Datetime, '" & Me.DtpFillDate.Value & "',102)", "") & ""
                'strSQL = strSQL & IIf(Me.cmbAccounts.Value > 0, "AND(ReceivingMasterTable.VendorId=" & Me.cmbAccounts.Value & ")", "")
                'strSQL += " And Left(ReceivingMasterTable.ReceivingNo,3)='Pur' "
                'Task:2534 Added subquery a and set filter on invoiceBalance 
                'Before against task:2539
                'strSQL = " SELECT ReceivingId, ReceivingNo, Vendor_Invoice_No, ReceivingDate, ReceivingAmount, InvoiceBalance From  (SELECT ReceivingMasterTable.ReceivingID, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No, ReceivingMasterTable.ReceivingDate, (ReceivingMasterTable.ReceivingAmount+ISNULL(SalesTax.Tax,0)) as ReceivingAmount,  (ReceivingMasterTable.ReceivingAmount+ISNULL(SalesTax.Tax,0)) - ISNULL(Payments.ReceivedAmount, 0) AS InvoiceBalance " & _
                '      " FROM ReceivingMasterTable INNER JOIN tblCOAMainSubSubDetail ON ReceivingMasterTable.VendorId = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as Tax From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(SELECT invoiceId, SUM((IsNull(PaymentAmount,0)) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedPaymentsDetail GROUP BY InvoiceId) Payments ON ReceivingMasterTable.ReceivingId = Payments.invoiceId " & _
                '      " WHERE (ReceivingMasterTable.ReceivingAmount - ISNULL(Payments.ReceivedAmount, 0) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND ReceivingMasterTable.ReceivingDate<=Convert(Datetime, '" & Me.DtpFillDate.Value & "',102)", "") & ""
                'strSQL = strSQL & IIf(Me.cmbAccounts.Value > 0, "AND(ReceivingMasterTable.VendorId=" & Me.cmbAccounts.Value & ")", "")
                'strSQL += " And Left(ReceivingMasterTable.ReceivingNo,3)='Pur') a WHERE A.InvoiceBalance <> 0"
                'End Task:2534
                'Task:2539 Added SubQuery For Debit Amount

                'strSQL = " SELECT ReceivingId, ReceivingNo, Vendor_Invoice_No, ReceivingDate, ReceivingAmount, Isnull([Debit Amount],0) as [Debit Amount], InvoiceBalance From  (SELECT ReceivingMasterTable.ReceivingID, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No, ReceivingMasterTable.ReceivingDate, (ReceivingMasterTable.ReceivingAmount+ISNULL(SalesTax.Tax,0)) as ReceivingAmount, IsNull(Ret.PurReturnAmount,0) as [Debit Amount], (ReceivingMasterTable.ReceivingAmount+ISNULL(SalesTax.Tax,0)) - (ISNULL(Payments.ReceivedAmount,0)+IsNull(Ret.PurReturnAmount,0)) AS InvoiceBalance " & _
                '    " FROM ReceivingMasterTable INNER JOIN tblCOAMainSubSubDetail ON ReceivingMasterTable.VendorId = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as Tax From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(SELECT invoiceId, SUM((IsNull(PaymentAmount,0)) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedPaymentsDetail GROUP BY InvoiceId) Payments ON ReceivingMasterTable.ReceivingId = Payments.invoiceId " & _
                '    " LEFT OUTER JOIN(Select PurReturn.PurchaseOrderId, (Isnull(PurReturn.PurReturnAmount,0)+Isnull(Tax,0)) as PurReturnAmount From(Select PurchaseOrderID, SUM(Isnull(PurchaseReturnAmount,0)) as PurReturnAmount From PurchaseReturnMasterTable WHERE PurchaseOrderId <> 0 AND PurchaseReturnMasterTable.VendorId=" & Me.cmbAccounts.Value & " Group By PurchaseOrderId) PurReturn " & _
                '    " LEFT OUTER JOIN(Select a.PurchaseOrderId, SUM(((Qty*Price)*Tax_Percent)/100) as Tax From PurchaseReturnDetailTable b INNER JOIN PurchaseReturnMasterTable a on a.PurchaseReturnId = b.PurchaseReturnId " & _
                '    " Group By a.PurchaseOrderId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.PurchaseOrderId = PurReturn.PurchaseOrderId) Ret On Ret.PurchaseOrderID  = ReceivingMasterTable.ReceivingId " & _
                '    " WHERE (ReceivingMasterTable.ReceivingAmount - ISNULL(Payments.ReceivedAmount, 0) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND ReceivingMasterTable.ReceivingDate<=Convert(Datetime, '" & Me.DtpFillDate.Value & "',102)", "") & ""
                ''3-Sep-2014 TASK:2823 Imran Ali Invoice Aging Formated Report Revised (Ravi)
                'LEFT OUTER JOIN(SELECT InvoiceId,coa_detail_Id, SUM(ISNULL(AdjustmentAmount, 0)) AS Adj FROM  dbo.InvoiceAdjustmentTable WHERE InvoiceType='Purchase' GROUP BY InvoiceId,coa_detail_Id) AdjInv On AdjInv.InvoiceID = ReceivingMasterTable.ReceivingId And AdjInv.coa_detail_id = ReceivingMasterTable.VendorId 


                'strSQL = " SELECT ReceivingId, ReceivingNo, Vendor_Invoice_No, ReceivingDate, IsNull(ReceivingAmount,0) as ReceivingAmount, IsNull(AdjInv.Adj,0) as Adjustment, Isnull([Debit Amount],0) as [Debit Amount], (IsNull(a.InvoiceBalance,0)-IsNull(AdjInv.Adj,0)) as InvoiceBalance From  (SELECT ReceivingMasterTable.ReceivingID, ReceivingMasterTable.ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No, ReceivingMasterTable.ReceivingDate, ((IsNull(ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.Tax,0))-IsNull(ReceivingMasterTable.TaxDeductionAmount,0)) as ReceivingAmount, IsNull(Ret.PurReturnAmount,0) as [Debit Amount], ((IsNull(ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.Tax,0))-IsNull(ReceivingMasterTable.TaxDeductionAmount,0)) - (ISNULL(Payments.ReceivedAmount,0)+IsNull(Ret.PurReturnAmount,0)) AS InvoiceBalance " & _
                '   " FROM ReceivingMasterTable INNER JOIN tblCOAMainSubSubDetail ON ReceivingMasterTable.VendorId = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as Tax From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(SELECT invoiceId, SUM((IsNull(PaymentAmount,0)) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedPaymentsDetail GROUP BY InvoiceId) Payments ON ReceivingMasterTable.ReceivingId = Payments.invoiceId " & _
                '   " LEFT OUTER JOIN(Select PurReturn.PurchaseOrderId, (Isnull(PurReturn.PurReturnAmount,0)+Isnull(Tax,0)) as PurReturnAmount From(Select PurchaseOrderID, SUM(Isnull(PurchaseReturnAmount,0)) as PurReturnAmount From PurchaseReturnMasterTable WHERE PurchaseOrderId <> 0 AND PurchaseReturnMasterTable.VendorId=" & Me.cmbAccounts.Value & " Group By PurchaseOrderId) PurReturn " & _
                '   " LEFT OUTER JOIN(Select a.PurchaseOrderId, SUM(((Qty*Price)*Tax_Percent)/100) as Tax From PurchaseReturnDetailTable b INNER JOIN PurchaseReturnMasterTable a on a.PurchaseReturnId = b.PurchaseReturnId " & _
                '   " Group By a.PurchaseOrderId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.PurchaseOrderId = PurReturn.PurchaseOrderId) Ret On Ret.PurchaseOrderID  = ReceivingMasterTable.ReceivingId " & _
                '   " WHERE (IsNull(ReceivingMasterTable.ReceivingAmount,0) - (ISNULL(Payments.ReceivedAmount, 0)) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND (Convert(varchar,ReceivingMasterTable.ReceivingDate,102) <= Convert(Datetime, '" & Me.DtpFillDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) ", "") & " "
                'End Task:2823

                'strSQL = " SELECT ReceivingId, ReceivingNo, Vendor_Invoice_No, ReceivingDate, IsNull(ReceivingAmount,0) as ReceivingAmount,IsNull([Tax Amount],0) as [Tax Amount], IsNull(ExpAmount,0) as Expense, IsNull(AdjInv.Adj,0) as Adjustment, Isnull([Debit Amount],0) as [Debit Amount], (IsNull(a.InvoiceBalance,0)-IsNull(AdjInv.Adj,0)) as InvoiceBalance From  (SELECT ReceivingMasterTable.ReceivingID, " & IIf(Me.rbtInvoice.Checked = True, "ReceivingMasterTable.ReceivingNo", "ReceivingMasterTable.Vendor_Invoice_No") & " as ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No, ReceivingMasterTable.ReceivingDate, ((IsNull(ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.Tax,0)+IsNull(SalesTax.AdTax,0))-IsNull(ReceivingMasterTable.TaxDeductionAmount,0)) as ReceivingAmount, (ISNULL(SalesTax.Tax,0)+IsNull(SalesTax.AdTax,0)) as [Tax Amount], IsNull(PurExp.ExpAmount,0) as ExpAmount, IsNull(Ret.PurReturnAmount,0) as [Debit Amount], ((IsNull(ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.Tax,0)+IsNull(SalesTax.AdTax,0))-IsNull(ReceivingMasterTable.TaxDeductionAmount,0)) - (ISNULL(Payments.ReceivedAmount,0)+IsNull(Ret.PurReturnAmount,0)) AS InvoiceBalance " & _
                '  " FROM ReceivingMasterTable INNER JOIN tblCOAMainSubSubDetail ON ReceivingMasterTable.VendorId = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as Tax, SUM(((Qty*Price)*AdTax_Percent)/100) as AdTax From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(SELECT invoiceId, SUM((IsNull(PaymentAmount,0)) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedPaymentsDetail GROUP BY InvoiceId) Payments ON ReceivingMasterTable.ReceivingId = Payments.invoiceId " & _
                '  " LEFT OUTER JOIN(Select PurReturn.PurchaseOrderId, (Isnull(PurReturn.PurReturnAmount,0)+Isnull(Tax,0)) as PurReturnAmount From(Select PurchaseOrderID, SUM(Isnull(PurchaseReturnAmount,0)) as PurReturnAmount From PurchaseReturnMasterTable WHERE PurchaseOrderId <> 0 AND PurchaseReturnMasterTable.VendorId=" & Me.cmbAccounts.Value & " Group By PurchaseOrderId) PurReturn " & _
                '  " LEFT OUTER JOIN(Select a.PurchaseOrderId, SUM(((Qty*Price)*(Tax_Percent)/100)+((Qty*Price)*AdTax_Percent)/100)) as Tax From PurchaseReturnDetailTable b INNER JOIN PurchaseReturnMasterTable a on a.PurchaseReturnId = b.PurchaseReturnId " & _
                '  " Group By a.PurchaseOrderId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.PurchaseOrderId = PurReturn.PurchaseOrderId) Ret On Ret.PurchaseOrderID  = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN (Select PurchaseId, SUM(IsNull(Exp_Amount,0))  as ExpAmount From InwardExpenseDetailTable WHERE IsNull(Exp_Amount,0) > 0 Group By PurchaseId) PurExp on PurExp.PurchaseId = ReceivingMasterTable.ReceivingId " & _
                '  " WHERE (IsNull(ReceivingMasterTable.ReceivingAmount,0) - (ISNULL(Payments.ReceivedAmount, 0)) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND (Convert(varchar,ReceivingMasterTable.ReceivingDate,102) <= Convert(Datetime, '" & Me.DtpFillDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) ", "") & " "

                strSQL = " SELECT ReceivingId, ReceivingNo, Vendor_Invoice_No, ReceivingDate, IsNull(ReceivingAmount,0) as ReceivingAmount, IsNull([Tax Amount],0) as [Tax Amount], IsNull(ExpAmount,0) as Expense, IsNull(AdjInv.Adj,0) as Adjustment, Isnull([Debit Amount],0) as [Debit Amount], (IsNull(a.InvoiceBalance,0)-IsNull(AdjInv.Adj,0)) as InvoiceBalance From  (SELECT ReceivingMasterTable.ReceivingID, " & IIf(Me.rbtInvoice.Checked = True, "ReceivingMasterTable.ReceivingNo", "ReceivingMasterTable.Vendor_Invoice_No") & " as ReceivingNo, ReceivingMasterTable.Vendor_Invoice_No, ReceivingMasterTable.ReceivingDate, ((IsNull(ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.Tax,0)+IsNull(SalesTax.AdTax,0))-IsNull(ReceivingMasterTable.TaxDeductionAmount,0)) as ReceivingAmount, (ISNULL(SalesTax.Tax,0)+IsNull(SalesTax.AdTax,0)) as [Tax Amount], IsNull(PurExp.ExpAmount,0) as ExpAmount, IsNull(Ret.PurReturnAmount,0) as [Debit Amount], ((IsNull(ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.Tax,0)+IsNull(SalesTax.AdTax,0))-IsNull(ReceivingMasterTable.TaxDeductionAmount,0)) - (ISNULL(Payments.ReceivedAmount,0)+IsNull(Ret.PurReturnAmount,0) + IsNull(Vocher.VocherAmount,0)) AS InvoiceBalance " & _
                 " FROM ReceivingMasterTable INNER JOIN tblCOAMainSubSubDetail ON ReceivingMasterTable.VendorId = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as Tax, SUM(((Qty*Price)*AdTax_Percent)/100) as AdTax From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(SELECT invoiceId, SUM((IsNull(PaymentAmount,0)) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedPaymentsDetail GROUP BY InvoiceId) Payments ON ReceivingMasterTable.ReceivingId = Payments.invoiceId " & _
                 " LEFT OUTER JOIN (SELECT     InvoiceId, IsNull(IsNull(SUM(NetAmount),0),0) AS VocherAmount FROM dbo.tblVoucherDetail GROUP BY InvoiceId ) Vocher on Vocher.InvoiceId = ReceivingMasterTable.ReceivingId " & _
                 " LEFT OUTER JOIN(Select PurReturn.PurchaseOrderId, (Isnull(PurReturn.PurReturnAmount,0)+Isnull(Tax,0)) as PurReturnAmount From(Select PurchaseOrderID, SUM(Isnull(PurchaseReturnAmount,0)) as PurReturnAmount From PurchaseReturnMasterTable WHERE PurchaseOrderId <> 0 AND PurchaseReturnMasterTable.VendorId=" & Me.cmbAccounts.Value & " Group By PurchaseOrderId) PurReturn " & _
                 " LEFT OUTER JOIN(Select a.PurchaseOrderId, SUM(((Isnull(Qty,0)*IsNull(Price,0))*(IsNull(Tax_Percent,0))/100)+((IsNull(Qty,0)*IsNull(Price,0))*(IsNull(AdTax_Percent,0))/100)) as Tax From PurchaseReturnDetailTable b INNER JOIN PurchaseReturnMasterTable a on a.PurchaseReturnId = b.PurchaseReturnId " & _
                 " Group By a.PurchaseOrderId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.PurchaseOrderId = PurReturn.PurchaseOrderId) Ret On Ret.PurchaseOrderID  = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN (Select PurchaseId, SUM(IsNull(Exp_Amount,0))  as ExpAmount From InwardExpenseDetailTable WHERE IsNull(Exp_Amount,0) > 0 Group By PurchaseId) PurExp on PurExp.PurchaseId = ReceivingMasterTable.ReceivingId " & _
                 " WHERE (IsNull(ReceivingMasterTable.ReceivingAmount,0) - (ISNULL(Payments.ReceivedAmount, 0)) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND (Convert(varchar,ReceivingMasterTable.ReceivingDate,102) <= Convert(Datetime, '" & Me.DtpFillDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) ", "") & " "
                strSQL = strSQL & IIf(Me.cmbAccounts.Value > 0, " AND(ReceivingMasterTable.VendorId=" & Me.cmbAccounts.Value & ")", "")
                strSQL += " And Left(ReceivingMasterTable.ReceivingNo,3)='Pur') a  LEFT OUTER JOIN(SELECT InvoiceId,coa_detail_Id, SUM(ISNULL(AdjustmentAmount, 0)) AS Adj FROM  dbo.InvoiceAdjustmentTable WHERE InvoiceType='Purchase' GROUP BY InvoiceId,coa_detail_Id) AdjInv On AdjInv.InvoiceID = a.ReceivingId WHERE (IsNull(a.InvoiceBalance,0)-IsNull(AdjInv.Adj,0)) <> 0"
                'Else
                'Before against task:2534
                'strSQL = " SELECT ReceivingMasterTable.ReceivingID, ReceivingMasterTable.Vendor_Invoice_No as [ReceivingNo], ReceivingMasterTable.ReceivingNo as [Vendor_Invoice_No], ReceivingMasterTable.ReceivingDate, (ReceivingMasterTable.ReceivingAmount+ISNULL(SalesTax.Tax,0)) as ReceivingAmount,  (ReceivingMasterTable.ReceivingAmount + ISNULL(SalesTax.Tax,0))- ISNULL(Payments.ReceivedAmount, 0) AS InvoiceBalance " & _
                '                         " FROM ReceivingMasterTable INNER JOIN tblCOAMainSubSubDetail ON ReceivingMasterTable.VendorId = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as Tax From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(SELECT invoiceId, SUM((IsNull(PaymentAmount,0)) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedPaymentsDetail GROUP BY InvoiceId) Payments ON ReceivingMasterTable.ReceivingId = Payments.invoiceId " & _
                '                         " WHERE (ReceivingMasterTable.ReceivingAmount - ISNULL(Payments.ReceivedAmount, 0) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND ReceivingMasterTable.ReceivingDate<=Convert(Datetime, '" & Me.DtpFillDate.Value & "',102)", "") & ""
                'strSQL = strSQL & IIf(Me.cmbAccounts.Value > 0, "AND(ReceivingMasterTable.VendorId=" & Me.cmbAccounts.Value & ")", "")
                'strSQL += " And Left(ReceivingMasterTable.ReceivingNo,3)='Pur' "
                'Task:2534 Added subquery a and set filter on invoiceBalance 
                'Task:2539 Added SubQuery For Debit Amount
                'strSQL = " SELECT ReceivingId, ReceivingNo, Vendor_Invoice_No, ReceivingDate, ReceivingAmount, Isnull([Debit Amount],0) as [Debit Amount], InvoiceBalance From (SELECT ReceivingMasterTable.ReceivingID, ReceivingMasterTable.Vendor_Invoice_No as [ReceivingNo], ReceivingMasterTable.ReceivingNo as [Vendor_Invoice_No], ReceivingMasterTable.ReceivingDate, (ReceivingMasterTable.ReceivingAmount+ISNULL(SalesTax.Tax,0)) as ReceivingAmount,  Isnull(Ret.PurReturnAmount,0) as [Debit Amount], (ReceivingMasterTable.ReceivingAmount + ISNULL(SalesTax.Tax,0))- (ISNULL(Payments.ReceivedAmount, 0)+Isnull(Ret.PurReturnAmount,0)) AS InvoiceBalance " & _
                '                        " FROM ReceivingMasterTable INNER JOIN tblCOAMainSubSubDetail ON ReceivingMasterTable.VendorId = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as Tax From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(SELECT invoiceId, SUM((IsNull(PaymentAmount,0)) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedPaymentsDetail GROUP BY InvoiceId) Payments ON ReceivingMasterTable.ReceivingId = Payments.invoiceId " & _
                '                        " LEFT OUTER JOIN(Select PurReturn.PurchaseOrderId, (Isnull(PurReturn.PurReturnAmount,0)+Isnull(Tax,0)) as PurReturnAmount From(Select PurchaseOrderID, SUM(Isnull(PurchaseReturnAmount,0)) as PurReturnAmount From PurchaseReturnMasterTable WHERE PurchaseOrderId <> 0 AND PurchaseReturnMasterTable.VendorId=" & Me.cmbAccounts.Value & " Group By PurchaseOrderId) PurReturn " & _
                '    " LEFT OUTER JOIN(Select a.PurchaseOrderId, SUM(((Qty*Price)*Tax_Percent)/100) as Tax From PurchaseReturnDetailTable b INNER JOIN PurchaseReturnMasterTable a on a.PurchaseReturnId = b.PurchaseReturnId " & _
                '    " Group By a.PurchaseOrderId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.PurchaseOrderId = PurReturn.PurchaseOrderId) Ret On Ret.PurchaseOrderID  = ReceivingMasterTable.ReceivingId " & _
                '                        " WHERE (ReceivingMasterTable.ReceivingAmount - ISNULL(Payments.ReceivedAmount, 0) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND ReceivingMasterTable.ReceivingDate<=Convert(Datetime, '" & Me.DtpFillDate.Value & "',102)", "") & ""
                ''3-Sep-2014 TASK:2823 Imran Ali Invoice Aging Formated Report Revised (Ravi)
                'strSQL = " SELECT ReceivingId, ReceivingNo, Vendor_Invoice_No, ReceivingDate, ReceivingAmount, IsNull(AdjInv.Adj,0) as Adjustment, Isnull([Debit Amount],0) as [Debit Amount], (IsNull(InvoiceBalance,0)-IsNull(AdjInv.Adj,0)) as InvoiceBalance From (SELECT ReceivingMasterTable.ReceivingID, ReceivingMasterTable.Vendor_Invoice_No as [ReceivingNo], ReceivingMasterTable.ReceivingNo as [Vendor_Invoice_No], ReceivingMasterTable.ReceivingDate, (ReceivingMasterTable.ReceivingAmount+ISNULL(SalesTax.Tax,0)) as ReceivingAmount,  Isnull(Ret.PurReturnAmount,0) as [Debit Amount], (ReceivingMasterTable.ReceivingAmount + ISNULL(SalesTax.Tax,0))- (ISNULL(Payments.ReceivedAmount, 0)+Isnull(Ret.PurReturnAmount,0)) AS InvoiceBalance " & _
                '                       " FROM ReceivingMasterTable INNER JOIN tblCOAMainSubSubDetail ON ReceivingMasterTable.VendorId = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as Tax From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(SELECT invoiceId, SUM((IsNull(PaymentAmount,0)) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedPaymentsDetail GROUP BY InvoiceId) Payments ON ReceivingMasterTable.ReceivingId = Payments.invoiceId " & _
                '                       " LEFT OUTER JOIN(Select PurReturn.PurchaseOrderId, (Isnull(PurReturn.PurReturnAmount,0)+Isnull(Tax,0)) as PurReturnAmount From(Select PurchaseOrderID, SUM(Isnull(PurchaseReturnAmount,0)) as PurReturnAmount From PurchaseReturnMasterTable WHERE PurchaseOrderId <> 0 AND PurchaseReturnMasterTable.VendorId=" & Me.cmbAccounts.Value & " Group By PurchaseOrderId) PurReturn " & _
                '   " LEFT OUTER JOIN(Select a.PurchaseOrderId, SUM(((Qty*Price)*Tax_Percent)/100) as Tax From PurchaseReturnDetailTable b INNER JOIN PurchaseReturnMasterTable a on a.PurchaseReturnId = b.PurchaseReturnId " & _
                '   " Group By a.PurchaseOrderId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.PurchaseOrderId = PurReturn.PurchaseOrderId) Ret On Ret.PurchaseOrderID  = ReceivingMasterTable.ReceivingId " & _
                '                       " WHERE (ReceivingMasterTable.ReceivingAmount - ISNULL(Payments.ReceivedAmount, 0) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND (Convert(Varchar,ReceivingMasterTable.ReceivingDate,102) <= Convert(Datetime, '" & Me.DtpFillDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))", "") & ""
                'End Task:2823


                'strSQL = " SELECT ReceivingId, ReceivingNo, Vendor_Invoice_No, ReceivingDate, IsNull(ReceivingAmount,0) as ReceivingAmount, IsNull(AdjInv.Adj,0) as Adjustment, Isnull([Debit Amount],0) as [Debit Amount], (IsNull(InvoiceBalance,0)-IsNull(AdjInv.Adj,0)) as InvoiceBalance From (SELECT ReceivingMasterTable.ReceivingID, ReceivingMasterTable.Vendor_Invoice_No as [ReceivingNo], ReceivingMasterTable.ReceivingNo as [Vendor_Invoice_No], ReceivingMasterTable.ReceivingDate, ((IsNull(ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.Tax,0))-IsNull(ReceivingMasterTable.TaxDeductionAmount,0)) as ReceivingAmount,  Isnull(Ret.PurReturnAmount,0) as [Debit Amount], ((IsNull(ReceivingMasterTable.ReceivingAmount,0) + ISNULL(SalesTax.Tax,0))-IsNull(ReceivingMasterTable.TaxDeductionAmount,0))- (ISNULL(Payments.ReceivedAmount, 0)+Isnull(Ret.PurReturnAmount,0)) AS InvoiceBalance " & _
                '                      " FROM ReceivingMasterTable INNER JOIN tblCOAMainSubSubDetail ON ReceivingMasterTable.VendorId = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as Tax From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(SELECT invoiceId, SUM((IsNull(PaymentAmount,0)) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedPaymentsDetail GROUP BY InvoiceId) Payments ON ReceivingMasterTable.ReceivingId = Payments.invoiceId " & _
                '                      " LEFT OUTER JOIN(Select PurReturn.PurchaseOrderId, (Isnull(PurReturn.PurReturnAmount,0)+Isnull(Tax,0)) as PurReturnAmount From(Select PurchaseOrderID, SUM(Isnull(PurchaseReturnAmount,0)) as PurReturnAmount From PurchaseReturnMasterTable WHERE PurchaseOrderId <> 0 AND PurchaseReturnMasterTable.VendorId=" & Me.cmbAccounts.Value & " Group By PurchaseOrderId) PurReturn " & _
                '  " LEFT OUTER JOIN(Select a.PurchaseOrderId, SUM(((Qty*Price)*Tax_Percent)/100) as Tax From PurchaseReturnDetailTable b INNER JOIN PurchaseReturnMasterTable a on a.PurchaseReturnId = b.PurchaseReturnId " & _
                '  " Group By a.PurchaseOrderId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.PurchaseOrderId = PurReturn.PurchaseOrderId) Ret On Ret.PurchaseOrderID  = ReceivingMasterTable.ReceivingId " & _
                '                      " WHERE (ReceivingMasterTable.ReceivingAmount - ISNULL(Payments.ReceivedAmount, 0) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND (Convert(Varchar,ReceivingMasterTable.ReceivingDate,102) <= Convert(Datetime, '" & Me.DtpFillDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))", "") & ""


                'strSQL = strSQL & IIf(Me.cmbAccounts.Value > 0, "AND(ReceivingMasterTable.VendorId=" & Me.cmbAccounts.Value & ")", "")
                'strSQL += " And Left(ReceivingMasterTable.ReceivingNo,3)='Pur') a  LEFT OUTER JOIN(SELECT InvoiceId,coa_detail_Id, SUM(ISNULL(AdjustmentAmount, 0)) AS Adj FROM  dbo.InvoiceAdjustmentTable WHERE InvoiceType='Purchase' GROUP BY InvoiceId,coa_detail_Id) AdjInv On AdjInv.InvoiceID = a.ReceivingId  WHERE (IsNull(a.InvoiceBalance,0)-IsNull(AdjInv.Adj,0)) <> 0"
                'End Task:2539
                'End Task:2534
                'End If
                FillUltraDropDown(Me.cmbInvoiceList, strSQL)
                'Task:2539 Set Hidden Column Of ReceivingId
                If Me.cmbInvoiceList.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
                'End Task:2539
            ElseIf Condition = "CostCenter" Then
                Dim str As String = "Select CostCenterId, Name as [Cost Center] From tblDefCostCenter"
                FillDropDown(Me.cmbCostCenter, str, True)

                'Task 3199 Add CostCenter Combo in Grid'

            ElseIf Condition = "CostCenterGrid" Then

                Dim Str As String = "Select CostCenterID, Name as CostCenter From tblDefCostCenter Union Select 0,'... Select Any Value ...' ORDER BY 2 ASC"

                Dim dtAc As New DataTable
                dtAc = GetDataTable(Str)
                Me.GrdPaymentDetail.RootTable.Columns("CostCenter").ValueList.PopulateValueList(dtAc.DefaultView, "CostCenterID", "CostCenter")

                ''Task:2370 Fill DropDown COA on Grid
            ElseIf Condition = "TaxAccounts" Then
                strSQL = String.Empty
                strSQL = "Select coa_detail_id, detail_title from vwCOADetail  WHERE Account_Type <> 'Inventory' AND detail_title <> '' Union Select 0,'... Select Any Value ...'"
                Dim dtAc As New DataTable
                dtAc = GetDataTable(strSQL)
                Me.GrdPaymentDetail.RootTable.Columns("OtherTaxAccountId").ValueList.PopulateValueList(dtAc.DefaultView, "coa_detail_id", "detail_title")
                'End Task:2370
                'Task#04082015 fill combo box with companies (Ahmad Sharif)
            ElseIf Condition = "Company" Then
                strSQL = String.Empty
                strSQL = "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""
                FillDropDown(Me.cmbCompany, strSQL, False)
                'End Task#04082015
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Dim TaxReceiveAbleAcId As Integer
            TaxReceiveAbleAcId = Val(getConfigValueByType("TaxreceiveableACid").ToString)
            'Task No 2537 Update The Tag Value 
            Dim PurchaseTaxDeductionAcId As Integer = 0I
            PurchaseTaxDeductionAcId = Val(getConfigValueByType("PurchaseTaxDeductionAcId").ToString)
            Dim strVoucherNo As String = String.Empty
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                strVoucherNo = Me.GetVoucherNo
            Else
                strVoucherNo = Me.txtVoucherNo.Text
            End If
            Dim NetPayment As Double
            ObjMod = New InvoicesBasedPaymentMaster 'With New Variable Invoice Based Payment Master Class 
            With ObjMod
                .PaymentID = MasterID
                .PaymentNo = strVoucherNo
                .PaymentDate = Me.dtpDate.Value
                .Reference = Me.txtReference.Text.ToString.Replace("'", "''")
                .PaymentAccountId = Me.cmbPayFrom.SelectedValue
                .PaymentMethod = Me.cmbPaymentMethod.SelectedValue
                'Task#4082015 Fill model for CompanyName added  by Ahmad Sharif
                .CompanyName = Me.cmbCompany.SelectedValue
                'End Task#04082015
                'Task No 2537 Append One cODE Line For Assigning The Checked Value Of Check Box To Prperty
                .Post = Me.ChkPost.Checked
                .VendorCode = Me.cmbAccounts.Value
                '.ChequeNo = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Before against task:2381
                .ChequeNo = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text.Replace("'", "''") & "'", String.Empty) 'Task:2381 Change apostrophe
                '.ChequeDate = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing) 'Before against task:2375
                .ChequeDate = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType In DateTime
                .UserName = LoginUserName
                .PVNo = strVoucherNo 'Me.txtVoucherNo.Text.ToString.Replace("'", "''")
                .ProjectId = Me.cmbCostCenter.SelectedValue
                .PayeeTitle = Me.txtPayeeTitle.Text
                .ChequeLayoutIndex = Me.cmbLayout.SelectedIndex
                .UserID = LoginUserId
                .DueDate = IIf(Me.DtpFillDate.Visible = True, Me.DtpFillDate.Value, Nothing)
                .InvoiceBasedPaymentDetail = New List(Of InvoicesBasedPaymentDetail) 'There Is Add Property Of Invoice Base Payment Detail Class List
                .VoucherMaster = New VouchersMaster 'New Voucher Master Class
                .VoucherMaster.VoucherDatail = New List(Of VouchersDetail)
                .VoucherMaster.LocationId = Me.cmbCompany.SelectedValue
                .VoucherMaster.VoucherCode = strVoucherNo
                .VoucherMaster.FinancialYearId = "1"
                .VoucherMaster.VoucherTypeId = Me.cmbPaymentMethod.SelectedValue
                .VoucherMaster.VoucherMonth = Me.dtpDate.Value.Date.Month
                .VoucherMaster.VoucherNo = strVoucherNo
                .VoucherMaster.VoucherDate = Me.dtpDate.Value
                .VoucherMaster.CoaDetailId = Me.cmbAccounts.Value
                '.VoucherMaster.ChequeNo = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty)'Before againsta task:2381
                .VoucherMaster.ChequeNo = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task:2381 Change Apostrophe
                '.VoucherMaster.ChequeDate = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing)'Before against task:2375 
                .VoucherMaster.ChequeDate = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType In DateTime
                'Task No 2537 Append One cODE Line For Assigning The Checked Value Of Check Box To Prperty
                .VoucherMaster.Post = Me.ChkPost.Checked
                .VoucherMaster.Source = Me.Name
                .VoucherMaster.References = Me.txtReference.Text
                .VoucherMaster.UserName = LoginUserName
                .VoucherMaster.Posted_UserName = LoginUserName
                .VoucherMaster.VNo = strVoucherNo
                .ActivityLog = New ActivityLog
                .ActivityLog.ApplicationName = "Accounts"
                .ActivityLog.FormCaption = Me.Text
                .ActivityLog.UserID = LoginUserId
                .ActivityLog.ID = MasterID
                .ActivityLog.LogDateTime = Date.Now
                .VoucherMaster.ActivityLog = New ActivityLog
                .VoucherMaster.ActivityLog.ApplicationName = "Accounts"
                .VoucherMaster.ActivityLog.FormCaption = Me.Text
                .VoucherMaster.ActivityLog.UserID = LoginUserId
                .VoucherMaster.ActivityLog.LogDateTime = Date.Now
                Dim InvoiceDetail As InvoicesBasedPaymentDetail 'Here Import Invoice Base Payment Detail Class
                Dim VoucherDt As VouchersDetail 'Here Import Voucher Detail Model Class
                Dim GrdDt As DataTable
                GrdDt = CType(Me.GrdPaymentDetail.DataSource, DataTable)
                GrdDt.AcceptChanges()
                Dim Dr As DataRow
                Dr = GrdDt.NewRow
                Dim flgTotalAmountVoucher As Boolean = False
                If getConfigValueByType("TotalAmountWiseInvoiceBasedVoucher").ToString = "" Then
                    flgTotalAmountVoucher = False
                Else
                    flgTotalAmountVoucher = Convert.ToBoolean(getConfigValueByType("TotalAmountWiseInvoiceBasedVoucher").ToString)
                End If
                'If flgTotalAmountVoucher = False Then
                For Each Dr In GrdDt.Rows
                    InvoiceDetail = New InvoicesBasedPaymentDetail 'With New Variable Invoice Based Payment Detail Class
                    With InvoiceDetail
                        'Task:2370 Changed Index 
                        .InvoiceId = Dr.Item(enmGrdPaymentDetail.ReceivingID).ToString
                        .InvoiceNo = Dr.Item(enmGrdPaymentDetail.ReceivingNo).ToString
                        .InvoiceDate = Dr.Item(enmGrdPaymentDetail.ReceivingDate).ToString
                        .Remarks = Dr.Item(enmGrdPaymentDetail.Remarks).ToString
                        .Gst_Percentage = Val(Dr.Item(enmGrdPaymentDetail.Gst_Percentage).ToString)
                        .InvoiceAmount = Val(Dr.Item(enmGrdPaymentDetail.ReceivingAmount).ToString)
                        .PaymentAmount = Val(Dr.Item(enmGrdPaymentDetail.PaidAmount).ToString)
                        .InvoiceBalance = Val(Dr.Item(enmGrdPaymentDetail.BalanceAmount).ToString)
                        NetPayment = NetPayment + Val(Dr.Item(enmGrdPaymentDetail.PaidAmount).ToString)
                        .Vendor_Invoice_No = Dr.Item("Memo").ToString
                        .SalesTaxAmount = Val(Dr.Item(enmGrdPaymentDetail.SalesTaxAmount).ToString)
                        .OtherTaxAmount = Val(Dr.Item(enmGrdPaymentDetail.OtherTaxAmount).ToString)
                        .OtherTaxAccountId = Val(Dr.Item(enmGrdPaymentDetail.OtherTaxAccountId).ToString)
                        .Description = Dr.Item(enmGrdPaymentDetail.Description).ToString 'Task:2470 Set Description Value
                        'Task 3199 Add CostCenter in Payment Detail
                        .CostCenter = Val(Dr.Item(enmGrdPaymentDetail.CostCenter).ToString)
                        .OtherPayment = Val(Dr.Item(enmGrdPaymentDetail.OtherPayment).ToString)
                        .InvoiceTax = Val(Dr.Item(enmGrdPaymentDetail.InvoiceTax).ToString)
                    End With
                    .InvoiceBasedPaymentDetail.Add(InvoiceDetail)
                    VoucherDt = New VouchersDetail
                    With VoucherDt
                        .LocationId = "1"
                        .CoaDetailId = Me.cmbPayFrom.SelectedValue
                        .Comments = Dr.Item(enmGrdPaymentDetail.Remarks).ToString & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "")
                        .DebitAmount = 0
                        '.CreditAmount = Val(Dr.Item("Paid Amount")) - Val(Dr.Item("Tax Amount"))  'Dr.Item(8).ToString  'Credit Amount Cash Or Bank
                        'Task:2370 Less SalesTaxAmount And OtherTaxAmount
                        .CreditAmount = (Val(Dr.Item("Paid Amount").ToString) - (Val(Dr.Item("Tax Amount").ToString) + Val(Dr.Item("SalesTaxAmount").ToString) + Val(Dr.Item("OtherTaxAmount").ToString)))  'Dr.Item(8).ToString  'Credit Amount Cash Or Bank
                        .CostCenter = Val(Dr.Item(enmGrdPaymentDetail.CostCenter).ToString)
                        .SPReference = Dr.Item("Memo").ToString
                        '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Before against Task:2381
                        .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task:2381 Change Apostrophe
                        '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing)'Before gainst task:2375
                        .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType In DateTime
                        .PayeeTitle = Me.txtPayeeTitle.Text 'Task:2382 Added Field Payee Title And assing Value
                        .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                    End With
                    .VoucherMaster.VoucherDatail.Add(VoucherDt)

                    Dim VoucherDtCredit As VouchersDetail
                    VoucherDtCredit = New VouchersDetail
                    With VoucherDtCredit
                        .LocationId = "1"
                        .CoaDetailId = Me.cmbAccounts.Value
                        .Comments = Dr.Item(enmGrdPaymentDetail.Remarks).ToString & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "")
                        .DebitAmount = (Val(Dr.Item("Paid Amount").ToString) - (Val(Dr.Item("Tax Amount").ToString) + Val(Dr.Item("SalesTaxAmount").ToString) + Val(Dr.Item("OtherTaxAmount").ToString))) '(Val(Dr.Item("Paid Amount")) - Val(Dr.Item("Tax Amount"))) 'Debit Amount From Account List                       
                        .CreditAmount = 0
                        .CostCenter = Val(Dr.Item(enmGrdPaymentDetail.CostCenter).ToString)
                        .SPReference = Dr.Item("Memo").ToString
                        '.Discount = Dr.Item("Discount").ToString
                        .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty)
                        '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing)'Before gainst task:2375
                        .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType In DateTime
                        .PayeeTitle = Me.txtPayeeTitle.Text 'Task:2382 Added Field Payee Title And assing Value
                        .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                    End With
                    .VoucherMaster.VoucherDatail.Add(VoucherDtCredit)
                    Dim VoucherDtInComTax As VouchersDetail
                    If Val(Dr.Item("Tax Amount").ToString) <> 0 Then
                        VoucherDtInComTax = New VouchersDetail
                        With VoucherDtInComTax
                            .LocationId = "1"
                            .CoaDetailId = TaxReceiveAbleAcId 'Incom tax Account Id
                            .Comments = "WH Tax deduction to " & Me.cmbAccounts.Text & " by Invoice No." & Dr.Item("Invoice No").ToString & " @" & Val(Dr.Item("Paid Amount").ToString) & "/" & Val(Dr.Item("Tax Amount").ToString) & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "") '"This supplier " & Me.cmbAccounts.Text & " income tax deduction against Invoice No. " & Mid(Dr.Item(enmGrdPaymentDetail.Remarks), 29, 10) & " deduction @" & Val(Dr.Item("Net Amount").ToString) & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "")
                            .DebitAmount = 0
                            .CreditAmount = Val(Dr.Item("Tax Amount").ToString) 'Income Tax Credit Amount From Account List                       
                            .CostCenter = Val(Dr.Item(enmGrdPaymentDetail.CostCenter).ToString)
                            .SPReference = Dr.Item("Memo").ToString
                            '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Before against Task:2381
                            .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task:2381 Change Apostrophe
                            '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing)'Before gainst task:2375
                            .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType In DateTime
                            .PayeeTitle = Me.txtPayeeTitle.Text 'Task:2382 Set Payee Title Value
                            .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                        End With
                        .VoucherMaster.VoucherDatail.Add(VoucherDtInComTax)

                        VoucherDtInComTax = New VouchersDetail
                        With VoucherDtInComTax
                            .LocationId = "1"
                            .CoaDetailId = Me.cmbAccounts.Value  'Incom tax Account Id
                            .Comments = "WH Tax deduction to " & Me.cmbAccounts.Text & " by Invoice No." & Dr.Item("Invoice No").ToString & " @" & Val(Dr.Item("Paid Amount").ToString) & "/" & Val(Dr.Item("Tax Amount").ToString) & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "") '"This supplier " & Me.cmbAccounts.Text & " income tax deduction against Invoice No. " & Mid(Dr.Item(enmGrdPaymentDetail.Remarks), 29, 10) & " deduction @" & Val(Dr.Item("Net Amount").ToString) & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "") '"This supplier " & Me.cmbAccounts.Text & " income tax deduction against Invoice No. " & Mid(Dr.Item(enmGrdPaymentDetail.Remarks), 29, 10) & " deduction @" & Val(Dr.Item("Net Amount").ToString) & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "")
                            .DebitAmount = Val(Dr.Item("Tax Amount").ToString)
                            .CreditAmount = 0 'Income Tax Credit Amount From Account List                       
                            .CostCenter = Val(Dr.Item(enmGrdPaymentDetail.CostCenter).ToString)
                            .SPReference = Dr.Item("Memo").ToString
                            '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Before against Task:2381
                            .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task:2381 Change Apostrophe
                            '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing)'Before gainst task:2375
                            .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType In DateTime
                            .PayeeTitle = Me.txtPayeeTitle.Text 'Task:2382 Set Payee Title Value
                            .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                        End With
                        .VoucherMaster.VoucherDatail.Add(VoucherDtInComTax)

                    End If
                    If Val(Dr.Item("SalesTaxAmount").ToString) <> 0 Then
                        ''Task:2370 SalesTaxAmount Voucher
                        VoucherDtInComTax = New VouchersDetail
                        With VoucherDtInComTax
                            .LocationId = "1"
                            '.CoaDetailId = TaxReceiveableAcId
                            'Task 2537 Updating The Code Line Above By Updateing The Tag Name
                            .CoaDetailId = PurchaseTaxDeductionAcId 'TaxReceiveAbleAcId 'Incom tax Account Id
                            .Comments = "Sales Tax deduction to " & Me.cmbAccounts.Text & " by Invoice No." & Dr.Item("Invoice No").ToString & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "") '"This supplier " & Me.cmbAccounts.Text & " sales tax deduction against Invoice No. " & Mid(Dr.Item(enmGrdPaymentDetail.Remarks), 29, 10) & " deduction @" & Val(Dr.Item("Net Amount").ToString) & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "")
                            .DebitAmount = 0
                            .CreditAmount = Val(Dr.Item("SalesTaxAmount").ToString) 'Income Tax Credit Amount From Account List                       
                            .CostCenter = Val(Dr.Item(enmGrdPaymentDetail.CostCenter).ToString)
                            .SPReference = Dr.Item("Memo").ToString
                            '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Before against Task:2381
                            .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task:2381 Change Apostrophe
                            '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing)'Before gainst task:2375
                            .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType In DateTime
                            .PayeeTitle = Me.txtPayeeTitle.Text 'Task:2382 Set Payee Title Value
                            .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                        End With
                        .VoucherMaster.VoucherDatail.Add(VoucherDtInComTax)

                        VoucherDtInComTax = New VouchersDetail
                        With VoucherDtInComTax
                            .LocationId = "1"
                            '.CoaDetailId = TaxReceiveableAcId
                            'Task 2537 Updating The Code Line Above By Updateing The Tag Name
                            .CoaDetailId = Me.cmbAccounts.Value  'TaxReceiveAbleAcId 'Incom tax Account Id
                            .Comments = "Sales Tax deduction to " & Me.cmbAccounts.Text & " by Invoice No." & Dr.Item("Invoice No").ToString & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "") '"This supplier " & Me.cmbAccounts.Text & " sales tax deduction against Invoice No. " & Mid(Dr.Item(enmGrdPaymentDetail.Remarks), 29, 10) & " deduction @" & Val(Dr.Item("Net Amount").ToString) & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "")
                            .DebitAmount = Val(Dr.Item("SalesTaxAmount").ToString)
                            .CreditAmount = 0 'Income Tax Credit Amount From Account List                       
                            .CostCenter = Val(Dr.Item(enmGrdPaymentDetail.CostCenter).ToString)
                            .SPReference = Dr.Item("Memo").ToString
                            '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Before against Task:2381
                            .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task:2381 Change Apostrophe
                            '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing)'Before gainst task:2375
                            .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType In DateTime
                            .PayeeTitle = Me.txtPayeeTitle.Text 'Task:2382 Set Payee Title Value
                            .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                        End With
                        .VoucherMaster.VoucherDatail.Add(VoucherDtInComTax)

                    End If

                    If Val(Dr.Item("OtherTaxAmount").ToString) <> 0 Then
                        VoucherDtInComTax = New VouchersDetail
                        With VoucherDtInComTax
                            .LocationId = "1"
                            .CoaDetailId = Val(Dr.Item("OtherTaxAccountId").ToString) 'TaxReceiveAbleAcId 'Incom tax Account Id
                            .Comments = "Other Tax deduction to " & Me.cmbAccounts.Text & " by Invoice No." & Dr.Item("Invoice No").ToString & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "") '"This supplier " & Me.cmbAccounts.Text & " other tax deduction against Invoice No. " & Mid(Dr.Item(enmGrdPaymentDetail.Remarks), 29, 10) & " deduction @" & Val(Dr.Item("Net Amount").ToString) & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "")
                            .DebitAmount = 0
                            .CreditAmount = Val(Dr.Item("OtherTaxAmount").ToString) 'Income Tax Credit Amount From Account List                       
                            .CostCenter = Val(Dr.Item(enmGrdPaymentDetail.CostCenter).ToString)
                            .SPReference = Dr.Item("Memo").ToString
                            '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Before against Task:2381
                            .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task:2381 Change Apostrophe
                            '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing)'Before gainst task:2375
                            .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType In DateTime
                            .PayeeTitle = Me.txtPayeeTitle.Text 'Task:2382 Set Payee Title Value
                            .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                        End With
                        .VoucherMaster.VoucherDatail.Add(VoucherDtInComTax)


                        VoucherDtInComTax = New VouchersDetail
                        With VoucherDtInComTax
                            .LocationId = "1"
                            .CoaDetailId = Me.cmbAccounts.Value  'TaxReceiveAbleAcId 'Incom tax Account Id
                            .Comments = "Other Tax deduction to " & Me.cmbAccounts.Text & " by Invoice No." & Dr.Item("Invoice No").ToString & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "") '"This supplier " & Me.cmbAccounts.Text & " other tax deduction against Invoice No. " & Mid(Dr.Item(enmGrdPaymentDetail.Remarks), 29, 10) & " deduction @" & Val(Dr.Item("Net Amount").ToString) & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "")
                            .DebitAmount = Val(Dr.Item("OtherTaxAmount").ToString)
                            .CreditAmount = 0 'Income Tax Credit Amount From Account List                       
                            .CostCenter = Val(Dr.Item(enmGrdPaymentDetail.CostCenter).ToString)
                            .SPReference = Dr.Item("Memo").ToString
                            '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Before against Task:2381
                            .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task:2381 Change Apostrophe
                            '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing)'Before gainst task:2375
                            .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType In DateTime
                            .PayeeTitle = Me.txtPayeeTitle.Text 'Task:2382 Set Payee Title Value
                            .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                        End With
                        .VoucherMaster.VoucherDatail.Add(VoucherDtInComTax)

                    End If
                    'End Task:2370




                    If Val(Dr.Item("Other Payment").ToString) <> 0 Then
                        VoucherDtInComTax = New VouchersDetail
                        With VoucherDtInComTax
                            .LocationId = "1"
                            .CoaDetailId = Me.cmbPayFrom.SelectedValue 'Val(Dr.Item("OtherTaxAccountId").ToString) 'TaxReceiveAbleAcId 'Incom tax Account Id
                            .Comments = "Other Payment to " & Me.cmbAccounts.Text & " by Invoice No." & Dr.Item("Invoice No").ToString & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "") '"This supplier " & Me.cmbAccounts.Text & " other tax deduction against Invoice No. " & Mid(Dr.Item(enmGrdPaymentDetail.Remarks), 29, 10) & " deduction @" & Val(Dr.Item("Net Amount").ToString) & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "")
                            .DebitAmount = 0
                            .CreditAmount = Val(Dr.Item("Other Payment").ToString) 'Income Tax Credit Amount From Account List                       
                            .CostCenter = Val(Dr.Item(enmGrdPaymentDetail.CostCenter).ToString)
                            .SPReference = Dr.Item("Memo").ToString
                            '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Before against Task:2381
                            .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task:2381 Change Apostrophe
                            '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing)'Before gainst task:2375
                            .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType In DateTime
                            .PayeeTitle = Me.txtPayeeTitle.Text 'Task:2382 Set Payee Title Value
                            .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                        End With
                        .VoucherMaster.VoucherDatail.Add(VoucherDtInComTax)


                        VoucherDtInComTax = New VouchersDetail
                        With VoucherDtInComTax
                            .LocationId = "1"
                            .CoaDetailId = Me.cmbAccounts.Value  'TaxReceiveAbleAcId 'Incom tax Account Id
                            .Comments = "Other Payment to " & Me.cmbAccounts.Text & " by Invoice No." & Dr.Item("Invoice No").ToString & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "") '"This supplier " & Me.cmbAccounts.Text & " other tax deduction against Invoice No. " & Mid(Dr.Item(enmGrdPaymentDetail.Remarks), 29, 10) & " deduction @" & Val(Dr.Item("Net Amount").ToString) & " " & IIf(Dr.Item(enmGrdPaymentDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdPaymentDetail.Description).ToString & "", "")
                            .DebitAmount = Val(Dr.Item("Other Payment").ToString)
                            .CreditAmount = 0 'Income Tax Credit Amount From Account List                       
                            .CostCenter = Val(Dr.Item(enmGrdPaymentDetail.CostCenter).ToString)
                            .SPReference = Dr.Item("Memo").ToString
                            '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Before against Task:2381
                            .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task:2381 Change Apostrophe
                            '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing)'Before gainst task:2375
                            .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType In DateTime
                            .PayeeTitle = Me.txtPayeeTitle.Text 'Task:2382 Set Payee Title Value
                            .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                        End With
                        .VoucherMaster.VoucherDatail.Add(VoucherDtInComTax)

                    End If




                Next 'Here Move Row With Data
                .NetPayment = NetPayment
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            'Task:M35 Added Condition For All Record
            Me.GrdSaved.DataSource = New InvoicesBasedPaymentDAL().GetAllRecord(Condition) 'Get DataResource From Invoice Base Payment DAL
            'end Task:M35
            Me.GrdSaved.RootTable.Columns("PaymentDate").FormatString = str_DisplayDateFormat

            'Task 3199 Set Cheque Date Format 
            Me.GrdSaved.RootTable.Columns("ChequeDate").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            Dim VerifyData As Integer
            VerifyData = 0
            If Me.cmbPayFrom.SelectedIndex <= 0 Then
                Me.cmbPayFrom.Focus()
                ShowErrorMessage("Please Select Pay From Account")
                Return False
                'If VerifyData = (Me.cmbPayFrom.SelectedIndex) Then
                '    Me.cmbPayFrom.Focus()
                '    ShowErrorMessage("Please Select Pay From Account")
                '    Return False
            ElseIf VerifyData = (Me.cmbAccounts.Value) Then
                Me.cmbAccounts.Focus()
                ShowErrorMessage("Please Select Payables Acount")
                Return False
            ElseIf VerifyData = (Me.GrdPaymentDetail.RowCount) Then
                ShowErrorMessage("Please Add Invoice In Grid")
                Return False
            Else
                Call FillModel()
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        EditMode = False
        Me.BtnSave.Text = "&Save"
        Me.txtInvoicoAmount.Text = 0
        Me.txtPaidAmt.Text = 0
        Me.txtDueAmount.Text = 0
        Me.txtRemarks.Text = ""
        Me.txtVoucherNo.Text = ""
        Me.dtpDate.Value = DateTime.Now
        Me.txtVoucherNo.Text = GetVoucherNo()
        Me.cmbAccounts.Value = 0
        Me.DtpFillDate.Checked = False
        Me.txtReference.Text = ""
        If Me.cmbPaymentMethod.SelectedText = "Cash" Then
            Me.lblChequeNo.Visible = False
            Me.lblChequeDate.Visible = False
            Me.txtChequeNo.Visible = False
            Me.DtpChequeDate.Visible = False
            'Task:2382 Visibility Control
            Me.Label15.Visible = False
            Me.txtPayeeTitle.Visible = False
            'End Task:2382
        End If
        Me.cmbPaymentMethod.Enabled = True
        Me.cmbPaymentMethod.SelectedIndex = 0
        Me.cmbPaymentMethod.Show()
        Me.cmbPayFrom.SelectedIndex = 0
        Me.txtChequeNo.Text = ""
        Me.DtpChequeDate.Checked = False
        Me.cmbAccounts.Rows(0).Activate()
        Me.FillCombos("Invoice")
        Me.cmbAccounts.Enabled = True
        Me.cmbCostCenter.Enabled = True
        Me.cmbInvoiceList.Rows(0).Activate()
        Me.DisplayRecord(-1)
        'Me.GetAllRecords()
        FillComboByEdit()
        Me.lblPrintStatus.Text = String.Empty
        Me.dtpDate.Enabled = True
        ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
        Me.BtnEdit.Visible = False
        Me.BtnDelete.Visible = False
        Me.BtnPrint.Visible = False
        Me.txtPayeeTitle.Text = String.Empty 'Task:2382 Reset Payee Title
        Me.txtSalesTaxAmount.Text = String.Empty
        Me.txtInvoiceTaxAmount.Text = String.Empty
        Me.txtTaxPercent.Text = String.Empty
        Me.cmbLayout.SelectedIndex = 0
        Me.cmbCompany.SelectedValue = 1
        btnUpdateTimes.Text = "No of update times"
        Me.DtpFillDate.Checked = False
        Me.DtpFillDate.Value = DateTime.Now
        Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        GetSecurityRights(EnumDataMode.[New]) ''13-Aug-2014 Task:2782 Imran Ali Post Rights Implement On Invoice Base Payment\Receipt Voucher
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New InvoicesBasedPaymentDAL().Add(ObjMod) Then Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If New InvoicesBasedPaymentDAL().Update(ObjMod) Then Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ChangeMethod()
        Try
            If Me.cmbPaymentMethod.SelectedIndex = 0 Then
                Me.lblChequeNo.Visible = False
                Me.lblChequeDate.Visible = False
                Me.txtChequeNo.Visible = False
                Me.DtpChequeDate.Visible = False
            Else
                Me.lblChequeNo.Visible = True
                Me.lblChequeDate.Visible = True
                Me.txtChequeNo.Visible = True
                Me.DtpChequeDate.Visible = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DtpFillDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DtpFillDate.CheckedChanged
        Try
            If Me.cmbInvoiceList.IsItemInList Then FillCombos("Invoice")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        Try
            'GetAllRecords()
            If GrdValidate() Then
                GridAddITems()
                GetSalesTaxTotal()
                ReSetGrd()
                Me.cmbInvoiceList.Focus()
                Me.cmbInvoiceList.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.Dropdown)
                Me.cmbAccounts.Enabled = False
                Me.cmbPaymentMethod.Enabled = False

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            ReSetControls()
            '  Me.GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub DtpFillDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DtpFillDate.ValueChanged
        Try
            If Me.DtpFillDate.Checked = True Then
                FillCombos("Invoice")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbInvoiceList_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbInvoiceList.Leave
        Try
            If Me.cmbInvoiceList.IsItemInList = False Then Exit Sub
            Me.txtInvoicoAmount.Text = 0
            'Me.txtPaidAmt.Text = Me.cmbInvoiceList.SelectedRow.Cells(4).Value.ToString 'Comment against task:2539
            Me.txtInvoiceTaxAmount.Text = Val(Me.cmbInvoiceList.SelectedRow.Cells("Tax Amount").Value.ToString)
            Me.txtOtherPayment.Text = Val(Me.cmbInvoiceList.SelectedRow.Cells("Expense").Value.ToString)
            Me.txtPaidAmt.Text = Val(Me.cmbInvoiceList.SelectedRow.Cells("InvoiceBalance").Value.ToString) 'Task:2539 Changed Column For Payment
            Me.txtDueAmount.Text = 0
            'Me.txtInvoicoAmount.Text = Me.cmbInvoiceList.SelectedRow.Cells(4).Value.ToString 'Comment against task:2539
            Me.txtInvoicoAmount.Text = Val(Me.cmbInvoiceList.SelectedRow.Cells("InvoiceBalance").Value.ToString) 'Task:2539 Changed Column For Payment
            Me.txtRemarks.Focus()
            'Ali Faisal : Sales tax and percentage column set empty on invoice selection or invoice combo leaving on 25-08-2016
            Me.txtTaxPercent.Text = 0
            Me.txtSalesTaxAmount.Text = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub txtReceivedAmt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaidAmt.Leave
    '    Try
    '        If Val(Me.txtInvoicoAmount.Text) > 0 Then
    '            Me.txtDueAmount.Text = (Val(Me.txtInvoicoAmount.Text) - (Val(Me.txtPaidAmt.Text) + Val(Me.txtDiscount.Text)))
    '            Me.BtnAdd.Enabled = True
    '            If Val(Me.txtDueAmount.Text) < 0 Then
    '                If msg_Confirm("due amount grater then invoice amount, do you want to add") = True Then
    '                    Me.BtnAdd.Enabled = True
    '                Else
    '                    Me.BtnAdd.Enabled = False
    '                    Me.txtPaidAmt.Text = "0"
    '                    Me.txtPaidAmt.Focus()
    '                    Exit Sub
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Function GrdValidate()
        Try
            If Me.cmbAccounts.SelectedRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please Select Vendor")
                Me.cmbAccounts.Focus()
                Return False
            End If
            If Not Val(Me.txtPaidAmt.Text) > 0 Then 'When Payment Amount is Zero 
                ShowErrorMessage("Please Enter Payment Amount")
                Me.txtPaidAmt.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetGrd()
        Me.txtRemarks.Text = ""
        Me.txtInvoicoAmount.Text = 0
        Me.txtPaidAmt.Text = 0
        Me.txtDueAmount.Text = 0
        Me.txtSalesTaxAmount.Text = String.Empty
        Me.txtOtherTaxAmount.Text = String.Empty
        Me.txtOtherPayment.Text = String.Empty
        Me.txtInvoiceTaxAmount.Text = String.Empty
        Me.txtTaxPercent.Text = String.Empty
        Me.cmbCostCenter.SelectedValue = 0
        Me.cmbInvoiceList.Focus()
    End Sub
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Me.BtnSave.Enabled = False Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            'If flgDateLock = True Then
            '    If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpDate.Value.ToString("yyyy-M-d 00:00:00") Then
            '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '    End If
            'End If
            If IsDateLock(Me.dtpDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If

            If Me.dtpDate.Value <= Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)) Then
                ShowErrorMessage("Your can not change this becuase financial year is closed")
                Me.dtpDate.Focus()
                Exit Sub
            End If

            If Not IsValidate() Then Exit Sub
            Me.GrdPaymentDetail.UpdateData()

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.GrdPaymentDetail.GetRows
                If Val(r.Cells(enmGrdPaymentDetail.OtherTaxAmount).Value.ToString) > 0 Then
                    If Val(r.Cells(enmGrdPaymentDetail.OtherTaxAccountId).Value.ToString) = 0 Then
                        ShowErrorMessage("Please select other tax account.")
                        Exit Sub
                    End If
                End If
            Next

            'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14 
            'If Not msg_Confirm(str_ConfirmSave) Then Exit Sub
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Save() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
                'MsgBox("Record Successfully Saved", MsgBoxStyle.Information, str_MessageHeader)
                'msg_Information(str_informSave)


                If Me.ChkPost.Checked = True Then
                    If EnabledBrandedSMS = True Then
                        If GetSMSConfig("Invoice Payment").Enable = True Then
                            If (Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString <> "" Or Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString.Length >= 10) Then
                                Try
                                    Dim strMSGBody As String = String.Empty
                                    Dim objSMSTemp As New SMSTemplateParameter
                                    If Me.cmbPaymentMethod.Text = "Bank" Then
                                        objSMSTemp = GetSMSTemplate("Invoice Based Bank Payment")
                                    Else
                                        objSMSTemp = GetSMSTemplate("Invoice Based Cash Payment")
                                    End If
                                    If objSMSTemp IsNot Nothing Then
                                        Dim objSMSParam As New SMSParameters
                                        objSMSParam.AccountCode = Me.cmbAccounts.ActiveRow.Cells("detail_code").Value.ToString
                                        objSMSParam.AccountTitle = Me.cmbAccounts.ActiveRow.Cells("detail_title").Value.ToString
                                        objSMSParam.DocumentNo = Me.txtVoucherNo.Text
                                        objSMSParam.DocumentDate = Me.dtpDate.Value
                                        objSMSParam.Remarks = Me.txtRemarks.Text
                                        objSMSParam.CellNo = Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString
                                        objSMSParam.Amount = Math.Round(Me.GrdPaymentDetail.GetTotal(Me.GrdPaymentDetail.RootTable.Columns("Net Amount"), AggregateFunction.Sum), 0)
                                        If Me.cmbPaymentMethod.Text = "Bank" Then
                                            objSMSParam.ChequeNo = Me.txtChequeNo.Text
                                            If Me.DtpChequeDate.Visible = False Then
                                                objSMSParam.ChequeDate = Nothing
                                            Else
                                                objSMSParam.ChequeDate = Me.DtpChequeDate.Value
                                            End If
                                        End If
                                        objSMSParam.CompanyName = CompanyTitle
                                        Dim objSMSLog As New SMSLogBE
                                        objSMSLog.SMSBody = objSMSTemp.SMSTemplate
                                        objSMSLog.PhoneNo = Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString
                                        objSMSLog.CreatedByUserID = LoginUserId
                                        Call SMSTemplateDAL.AddSMSLog(objSMSLog, objSMSParam)
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        End If
                    End If
                End If

                Me.ReSetControls()
            Else
                'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14 
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Update1() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
                'MsgBox("Record Successfully Update", MsgBoxStyle.Information, str_MessageHeader)
                'msg_Information(str_informSave)



                If Me.ChkPost.Checked = True Then
                    If EnabledBrandedSMS = True Then
                        If GetSMSConfig("Invoice Payment").Enable = True Then
                            If (Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString <> "" Or Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString.Length >= 10) Then
                                Try
                                    Dim strMSGBody As String = String.Empty
                                    Dim objSMSTemp As New SMSTemplateParameter
                                    If Me.cmbPaymentMethod.Text = "Bank" Then
                                        objSMSTemp = GetSMSTemplate("Invoice Based Bank Payment")
                                    Else
                                        objSMSTemp = GetSMSTemplate("Invoice Based Cash Payment")
                                    End If
                                    If objSMSTemp IsNot Nothing Then
                                        Dim objSMSParam As New SMSParameters
                                        objSMSParam.AccountCode = Me.cmbAccounts.ActiveRow.Cells("detail_code").Value.ToString
                                        objSMSParam.AccountTitle = Me.cmbAccounts.ActiveRow.Cells("detail_title").Value.ToString
                                        objSMSParam.DocumentNo = Me.txtVoucherNo.Text
                                        objSMSParam.DocumentDate = Me.dtpDate.Value
                                        objSMSParam.Remarks = Me.txtRemarks.Text
                                        objSMSParam.CellNo = Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString
                                        objSMSParam.Amount = Math.Round(Me.GrdPaymentDetail.GetTotal(Me.GrdPaymentDetail.RootTable.Columns("Net Amount"), AggregateFunction.Sum), 0)
                                        If Me.cmbPaymentMethod.Text = "Bank" Then
                                            objSMSParam.ChequeNo = Me.txtChequeNo.Text
                                            If Me.DtpChequeDate.Visible = False Then
                                                objSMSParam.ChequeDate = Nothing
                                            Else
                                                objSMSParam.ChequeDate = Me.DtpChequeDate.Value
                                            End If
                                        End If
                                        objSMSParam.CompanyName = CompanyTitle
                                        Dim objSMSLog As New SMSLogBE
                                        objSMSLog.SMSBody = objSMSTemp.SMSTemplate
                                        objSMSLog.PhoneNo = Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString
                                        objSMSLog.CreatedByUserID = LoginUserId
                                        Call SMSTemplateDAL.AddSMSLog(objSMSLog, objSMSParam)
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        End If
                    End If
                End If
                Me.ReSetControls()
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    'Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
    '    If Me.UltraTabControl1.SelectedTab.Index = 0 Then
    '        Me.BtnLoadAll.Visible = False
    '    Else
    '        Me.BtnLoadAll.Visible = True
    '    End If
    'End Sub
    Private Sub DisplayRecord(ByVal PaymentId As Integer)
        Dim dt As New DataTable
        ''Before against Tsk:2370 
        'Dim strsql = "SELECT  dbo.InvoiceBasedPaymentsDetail.PaymentDetailId, dbo.ReceivingMasterTable.ReceivingId, dbo.ReceivingMasterTable.ReceivingNo as [Invoice No], dbo.ReceivingMasterTable.ReceivingDate as [Invoice Date], " _
        '            & " InvoiceBasedPaymentsDetail.Comments as Remarks, IsNull(InvoiceBasedPaymentsDetail.Gst_Percentage,0) As [Tax], ((IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0)*IsNull(InvoiceBasedPaymentsDetail.Gst_Percentage,0))/100) as [Tax Amount],  IsNull(dbo.ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.SalesTax,0) as [Invoice Amount],   IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0) as [Paid Amount],  " _
        '            & " (IsNull(dbo.ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.SalesTax,0)) - IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0) as [Due Amount],  InvoiceBasedPaymentsDetail.Vendor_Invoice_No as Memo" _
        '            & " FROM dbo.InvoiceBasedPaymentsDetail RIGHT OUTER JOIN " _
        '            & " dbo.ReceivingMasterTable ON dbo.InvoiceBasedPaymentsDetail.InvoiceId = dbo.ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as SalesTax  From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId " _
        '            & " Where InvoiceBasedPaymentsDetail.PaymentId=" & PaymentId
        'Task:2370 Added Columns SalesTaxAmount, OtherTaxAmount, OtherTaxAccountId
        'Before against task:2470
        'Dim strsql = "SELECT  dbo.InvoiceBasedPaymentsDetail.PaymentDetailId, dbo.ReceivingMasterTable.ReceivingId, dbo.ReceivingMasterTable.ReceivingNo as [Invoice No], dbo.ReceivingMasterTable.ReceivingDate as [Invoice Date], " _
        '          & " InvoiceBasedPaymentsDetail.Comments as Remarks, IsNull(InvoiceBasedPaymentsDetail.Gst_Percentage,0) As [Tax], ((IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0)*IsNull(InvoiceBasedPaymentsDetail.Gst_Percentage,0))/100) as [Tax Amount], Isnull(InvoiceBasedPaymentsDetail.SalesTaxAmount,0) as SalesTaxAmount, Isnull(InvoiceBasedPaymentsDetail.OtherTaxAmount,0) as OtherTaxAmount, Isnull(InvoiceBasedPaymentsDetail.OtherTaxAccountId,0) as OtherTaxAccountId, IsNull(dbo.ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.SalesTax,0) as [Invoice Amount],   IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0) as [Paid Amount],  " _
        '          & " (IsNull(dbo.ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.SalesTax,0)) - IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0) as [Due Amount],  InvoiceBasedPaymentsDetail.Vendor_Invoice_No as Memo " _
        '          & " FROM dbo.InvoiceBasedPaymentsDetail RIGHT OUTER JOIN " _
        '          & " dbo.ReceivingMasterTable ON dbo.InvoiceBasedPaymentsDetail.InvoiceId = dbo.ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as SalesTax  From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId " _
        '          & " Where InvoiceBasedPaymentsDetail.PaymentId=" & PaymentId
        'End Task:2370
        'Task:2470 Added Column Description In This Query
        'Before against task:2539
        'Dim strsql = "SELECT  dbo.InvoiceBasedPaymentsDetail.PaymentDetailId, dbo.ReceivingMasterTable.ReceivingId, dbo.ReceivingMasterTable.ReceivingNo as [Invoice No], dbo.ReceivingMasterTable.ReceivingDate as [Invoice Date], " _
        '          & " InvoiceBasedPaymentsDetail.Comments as Remarks, IsNull(InvoiceBasedPaymentsDetail.Gst_Percentage,0) As [Tax], ((IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0)*IsNull(InvoiceBasedPaymentsDetail.Gst_Percentage,0))/100) as [Tax Amount], Isnull(InvoiceBasedPaymentsDetail.SalesTaxAmount,0) as SalesTaxAmount, Isnull(InvoiceBasedPaymentsDetail.OtherTaxAmount,0) as OtherTaxAmount, Isnull(InvoiceBasedPaymentsDetail.OtherTaxAccountId,0) as OtherTaxAccountId, IsNull(dbo.ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.SalesTax,0) as [Invoice Amount],   IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0) as [Paid Amount],  " _
        '          & " (IsNull(dbo.ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.SalesTax,0)) - IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0) as [Due Amount],  InvoiceBasedPaymentsDetail.Vendor_Invoice_No as Memo,InvoiceBasedPaymentsDetail.Description  " _
        '          & " FROM dbo.InvoiceBasedPaymentsDetail RIGHT OUTER JOIN " _
        '          & " dbo.ReceivingMasterTable ON dbo.InvoiceBasedPaymentsDetail.InvoiceId = dbo.ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as SalesTax  From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId " _
        '          & " Where InvoiceBasedPaymentsDetail.PaymentId=" & PaymentId
        'end Task:2470
        'Task:2539 Added SubQuery Debit Amount
        'Dim strsql = "SELECT  dbo.InvoiceBasedPaymentsDetail.PaymentDetailId, dbo.ReceivingMasterTable.ReceivingId, dbo.ReceivingMasterTable.ReceivingNo as [Invoice No], dbo.ReceivingMasterTable.ReceivingDate as [Invoice Date], " _
        '          & " InvoiceBasedPaymentsDetail.Comments as Remarks, IsNull(InvoiceBasedPaymentsDetail.Gst_Percentage,0) As [Tax], ((IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0)*IsNull(InvoiceBasedPaymentsDetail.Gst_Percentage,0))/100) as [Tax Amount], Isnull(InvoiceBasedPaymentsDetail.SalesTaxAmount,0) as SalesTaxAmount, Isnull(InvoiceBasedPaymentsDetail.OtherTaxAmount,0) as OtherTaxAmount, Isnull(InvoiceBasedPaymentsDetail.OtherTaxAccountId,0) as OtherTaxAccountId, ((IsNull(dbo.ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.SalesTax,0))-Isnull(Ret.PurReturnAmount,0)) as [Invoice Amount],   IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0) as [Paid Amount],  " _
        '          & " (IsNull(dbo.ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.SalesTax,0)) - IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0) as [Due Amount],  InvoiceBasedPaymentsDetail.Vendor_Invoice_No as Memo,InvoiceBasedPaymentsDetail.Description  " _
        '          & " FROM dbo.InvoiceBasedPaymentsDetail RIGHT OUTER JOIN " _
        '          & " dbo.ReceivingMasterTable ON dbo.InvoiceBasedPaymentsDetail.InvoiceId = dbo.ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as SalesTax  From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId " _
        '          & " LEFT OUTER JOIN(Select PurReturn.PurchaseOrderId, (Isnull(PurReturn.PurReturnAmount,0)+Isnull(Tax,0)) as PurReturnAmount From(Select PurchaseOrderID, SUM(Isnull(PurchaseReturnAmount,0)) as PurReturnAmount From PurchaseReturnMasterTable WHERE PurchaseOrderId <> 0 AND PurchaseReturnMasterTable.VendorId=" & Me.cmbAccounts.Value & " Group By PurchaseOrderId) PurReturn " _
        '          & " LEFT OUTER JOIN(Select a.PurchaseOrderId, SUM(((Qty*Price)*Tax_Percent)/100) as Tax From PurchaseReturnDetailTable b INNER JOIN PurchaseReturnMasterTable a on a.PurchaseReturnId = b.PurchaseReturnId " _
        '          & " Group By a.PurchaseOrderId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.PurchaseOrderId = PurReturn.PurchaseOrderId) Ret On Ret.PurchaseOrderID  = ReceivingMasterTable.ReceivingId " _
        '          & " Where InvoiceBasedPaymentsDetail.PaymentId=" & PaymentId
        'End Task:2539
        ''Task:2775 Added Field Net Amount
        'Dim strsql = "SELECT  IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentDetailId,0) as PaymentDetailId, IsNull(dbo.ReceivingMasterTable.ReceivingId,0) as ReceivingId, dbo.ReceivingMasterTable.ReceivingNo as [Invoice No], dbo.ReceivingMasterTable.ReceivingDate as [Invoice Date], " _
        '         & " InvoiceBasedPaymentsDetail.Comments as Remarks, IsNull(InvoiceBasedPaymentsDetail.Gst_Percentage,0) As [Tax], ((IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0)*IsNull(InvoiceBasedPaymentsDetail.Gst_Percentage,0))/100) as [Tax Amount], Isnull(InvoiceBasedPaymentsDetail.SalesTaxAmount,0) as SalesTaxAmount, Isnull(InvoiceBasedPaymentsDetail.OtherTaxAmount,0) as OtherTaxAmount, Isnull(InvoiceBasedPaymentsDetail.OtherTaxAccountId,0) as OtherTaxAccountId, Convert(float,0) as [Net Amount], ((IsNull(dbo.ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.SalesTax,0))-Isnull(Ret.PurReturnAmount,0)) as [Invoice Amount],   IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0) as [Paid Amount],  " _
        '         & " (IsNull(dbo.ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.SalesTax,0)) - IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0) as [Due Amount],  InvoiceBasedPaymentsDetail.Vendor_Invoice_No as Memo,InvoiceBasedPaymentsDetail.Description  " _
        '         & " FROM dbo.InvoiceBasedPaymentsDetail RIGHT OUTER JOIN " _
        '         & " dbo.ReceivingMasterTable ON dbo.InvoiceBasedPaymentsDetail.InvoiceId = dbo.ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as SalesTax  From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId " _
        '         & " LEFT OUTER JOIN(Select PurReturn.PurchaseOrderId, (Isnull(PurReturn.PurReturnAmount,0)+Isnull(Tax,0)) as PurReturnAmount From(Select PurchaseOrderID, SUM(Isnull(PurchaseReturnAmount,0)) as PurReturnAmount From PurchaseReturnMasterTable WHERE PurchaseOrderId <> 0 AND PurchaseReturnMasterTable.VendorId=" & Me.cmbAccounts.Value & " Group By PurchaseOrderId) PurReturn " _
        '         & " LEFT OUTER JOIN(Select a.PurchaseOrderId, SUM(((Qty*Price)*Tax_Percent)/100) as Tax From PurchaseReturnDetailTable b INNER JOIN PurchaseReturnMasterTable a on a.PurchaseReturnId = b.PurchaseReturnId " _
        '         & " Group By a.PurchaseOrderId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.PurchaseOrderId = PurReturn.PurchaseOrderId) Ret On Ret.PurchaseOrderID  = ReceivingMasterTable.ReceivingId " _
        '         & " Where InvoiceBasedPaymentsDetail.PaymentId=" & PaymentId
        'Task:2775
        'Task:2775 Added Field Net Amount
        Dim strsql = "SELECT  IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentDetailId,0) as PaymentDetailId, IsNull(dbo.ReceivingMasterTable.ReceivingId,0) as ReceivingId, dbo.ReceivingMasterTable.ReceivingNo as [Invoice No], dbo.ReceivingMasterTable.ReceivingDate as [Invoice Date], " _
                 & " InvoiceBasedPaymentsDetail.Comments as Remarks, IsNull(InvoiceBasedPaymentsDetail.Gst_Percentage,0) As [Tax], ((IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0)*IsNull(InvoiceBasedPaymentsDetail.Gst_Percentage,0))/100) as [Tax Amount], IsNull(InvoiceBasedPaymentsDetail.Invoice_Tax,0) as [Invoice Tax],  Case When IsNull(InvoiceBasedPaymentsDetail.Invoice_Tax,0) >  Isnull(InvoiceBasedPaymentsDetail.SalesTaxAmount,0)    then ((IsNull(InvoiceBasedPaymentsDetail.SalesTaxAmount,0)/Isnull(InvoiceBasedPaymentsDetail.Invoice_Tax,0))*100)  else 0 end as Tax_Percent, Isnull(InvoiceBasedPaymentsDetail.SalesTaxAmount,0) as SalesTaxAmount, Isnull(InvoiceBasedPaymentsDetail.OtherTaxAmount,0) as OtherTaxAmount, Isnull(InvoiceBasedPaymentsDetail.OtherTaxAccountId,0) as OtherTaxAccountId, Convert(float,0) as [Net Amount], ((IsNull(dbo.ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.SalesTax,0)+ISNULL(SalesTax.AdTax,0))-Isnull(Ret.PurReturnAmount,0)) as [Invoice Amount],  IsNull(dbo.InvoiceBasedPaymentsDetail.OtherPayment,0) as [Other Payment],  IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0) as [Paid Amount],  " _
                 & " (IsNull(dbo.ReceivingMasterTable.ReceivingAmount,0)+ISNULL(SalesTax.SalesTax,0)) - IsNull(dbo.InvoiceBasedPaymentsDetail.PaymentAmount,0) as [Due Amount],  InvoiceBasedPaymentsDetail.Vendor_Invoice_No as Memo,InvoiceBasedPaymentsDetail.Description , InvoiceBasedPaymentsDetail.CostCenterId As CostCenter  " _
                 & " FROM dbo.InvoiceBasedPaymentsDetail RIGHT OUTER JOIN " _
                 & " dbo.ReceivingMasterTable ON dbo.InvoiceBasedPaymentsDetail.InvoiceId = dbo.ReceivingMasterTable.ReceivingId LEFT OUTER JOIN(Select ReceivingId, SUM(((Qty*Price)*TaxPercent)/100) as SalesTax, SUM(((Qty*Price)*AdTax_Percent)/100) as AdTax From ReceivingDetailTable Group By ReceivingId) SalesTax On SalesTax.ReceivingId = ReceivingMasterTable.ReceivingId " _
                 & " LEFT OUTER JOIN(Select PurReturn.PurchaseOrderId, (Isnull(PurReturn.PurReturnAmount,0)+Isnull(Tax,0)) as PurReturnAmount From(Select PurchaseOrderID, SUM(Isnull(PurchaseReturnAmount,0)) as PurReturnAmount From PurchaseReturnMasterTable WHERE PurchaseOrderId <> 0 AND PurchaseReturnMasterTable.VendorId=" & Me.cmbAccounts.Value & " Group By PurchaseOrderId) PurReturn " _
                 & " LEFT OUTER JOIN(Select a.PurchaseOrderId, SUM(((Qty*Price)*Tax_Percent)/100) as Tax From PurchaseReturnDetailTable b INNER JOIN PurchaseReturnMasterTable a on a.PurchaseReturnId = b.PurchaseReturnId " _
                 & " Group By a.PurchaseOrderId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.PurchaseOrderId = PurReturn.PurchaseOrderId) Ret On Ret.PurchaseOrderID  = ReceivingMasterTable.ReceivingId " _
                 & " Where InvoiceBasedPaymentsDetail.PaymentId=" & PaymentId
        dt = GetDataTable(strsql)
        Me.GrdPaymentDetail.DataSource = dt
        Me.GrdPaymentDetail.RetrieveStructure()
        'Task:2370 Call Tax Account DropDown Method
        Me.GrdPaymentDetail.RootTable.Columns("OtherTaxAccountId").EditType = EditType.Combo
        Me.GrdPaymentDetail.RootTable.Columns("OtherTaxAccountId").HasValueList = True

        'Task:3199 Call CostCenter DropDown Method
        Me.GrdPaymentDetail.RootTable.Columns("CostCenter").EditType = EditType.Combo
        Me.GrdPaymentDetail.RootTable.Columns("CostCenter").HasValueList = True

        'Task:2775 Added Delete Button Field
        Me.GrdPaymentDetail.RootTable.Columns.Add("Column1")
        Me.GrdPaymentDetail.RootTable.Columns("Column1").ButtonDisplayMode = CellButtonDisplayMode.Always
        Me.GrdPaymentDetail.RootTable.Columns("Column1").ButtonStyle = ButtonStyle.ButtonCell
        Me.GrdPaymentDetail.RootTable.Columns("Column1").ButtonText = "Delete"
        Me.GrdPaymentDetail.RootTable.Columns("Column1").TextAlignment = TextAlignment.Center
        Me.GrdPaymentDetail.RootTable.Columns("Column1").HeaderAlignment = TextAlignment.Center
        Me.GrdPaymentDetail.RootTable.Columns("Column1").Caption = "Action"
        'End Task

        FillCombos("TaxAccounts")
        'End Task:2370
        Me.GrdPaymentDetail.RootTable.Columns("Memo").Visible = False


        'Task 3199
        FillCombos("CostCenterGrid")

        If flgInvoiceWiseTaxPercent = True Then
            'Before against task:2699
            'dt.Columns(enmGrdPaymentDetail.Gst_Amount).Expression = "(([Tax]*[Paid Amount])/100)"
            ''19-June-2014 TASK:2699 Imran Ali Apply WithHolding Tax After Other And SalexTax Deduction On Invoice based payment
            If DeductionWHTaxOnTotal = False Then
                dt.Columns(enmGrdPaymentDetail.Gst_Amount).Expression = "(([Tax]*([Paid Amount]-([OtherTaxAmount]+[SalesTaxAmount])))/100)"
            Else
                dt.Columns(enmGrdPaymentDetail.Gst_Amount).Expression = "(([Tax]*[Paid Amount])/100)"
            End If
            'End Task:2699
        Else
            'Before against task:2699
            'dt.Columns(enmGrdPaymentDetail.Gst_Percentage).Expression = "(IIF([Tax Amount]=0,0,([Tax Amount]/[Paid Amount])*100))"
            ''19-June-2014 TASK:2699 Imran Ali Apply WithHolding Tax After Other And SalexTax Deduction On Invoice based payment
            If DeductionWHTaxOnTotal = False Then
                dt.Columns(enmGrdPaymentDetail.Gst_Percentage).Expression = "(IIF([Tax Amount]=0,0,([Tax Amount]/([Paid Amount]-([OtherTaxAmount]+[SalesTaxAmount])))*100))"
            Else
                dt.Columns(enmGrdPaymentDetail.Gst_Percentage).Expression = "(IIF([Tax Amount]=0,0,([Tax Amount]/([Paid Amount]))*100))"
            End If
            'End Task:2699
        End If
        dt.Columns(enmGrdPaymentDetail.BalanceAmount).Expression = "(([Invoice Amount]-[Paid Amount]))"
        dt.Columns("Net Amount").Expression = "([Paid Amount]-([Tax Amount]+[OtherTaxAmount]+[SalesTaxAmount]))" 'Task:2775 Net Amount Calc
        ApplyGridSettings()
    End Sub
    Public Function GetVoucherNo() As String

        Dim VType As String = String.Empty
        If Me.cmbPaymentMethod.SelectedIndex > 0 Then
            'Task#04082015 Concatenate Company location id with prefix of invoice number if configuration on of company wise prefix
            VType = "BPV" & IIf(getConfigValueByType("CompanyWisePrefix").ToString = "True", Me.cmbCompany.SelectedValue, String.Empty)
        Else
            VType = "CPV" & IIf(getConfigValueByType("CompanyWisePrefix").ToString = "True", Me.cmbCompany.SelectedValue, String.Empty)
            'End Task#04082015
        End If
        If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
            Return GetSerialNo(VType + "-" + Microsoft.VisualBasic.Right(Me.dtpDate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
        Else
            Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
            Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
            If Not dr Is Nothing Then
                If dr("config_Value") = "Monthly" Then
                    Return GetNextDocNo(VType & "-" & Format(Me.dtpDate.Value, "yy") & Me.dtpDate.Value.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
                Else
                    Return GetNextDocNo(VType, 6, "tblVoucher", "voucher_no")
                End If
            Else
                Return GetNextDocNo(VType, 6, "tblVoucher", "voucher_no")
            End If
            Return ""
        End If
    End Function
    Private Sub cmbPaymentMethod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPaymentMethod.SelectedIndexChanged
        Try
            If Me.EditMode = False Then
                If Me.cmbPaymentMethod.SelectedIndex > 0 Then
                    Me.txtVoucherNo.Text = Me.GetVoucherNo
                    Me.Label15.Visible = True
                    Me.txtPayeeTitle.Visible = True
                    Me.Label17.Visible = True
                    Me.cmbLayout.Visible = True
                Else
                    Me.txtVoucherNo.Text = Me.GetVoucherNo
                    Me.Label15.Visible = False
                    Me.txtPayeeTitle.Visible = False
                    Me.Label17.Visible = False
                    Me.cmbLayout.Visible = False
                End If
            Else
                Me.EditMode = True
            End If
            FillCombos("Payfrom")
            ChangeMethod()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GrdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GrdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14
        If e.KeyCode = Keys.F2 Then
            BtnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.GrdSaved.RowCount <= 0 Then Exit Sub
            BtnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
        If e.KeyCode = Keys.F5 Then
            BtnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub GrdSaved_RowDoubleClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles GrdSaved.RowDoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoadAll.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            GetAllRecords("All")
            DisplayRecord(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14 
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Dim Id As Integer
        Id = 0
        Id = Me.cmbAccounts.Value
        FillCombos("Customer")
        Me.cmbAccounts.Value = Id

        Id = Me.cmbPaymentMethod.SelectedValue
        FillCombos("Payfrom")
        Me.cmbPaymentMethod.SelectedValue = Id

        FillCombos("Company")

        If Not getConfigValueByType("SalesDiscountAccount").ToString = "Error" Then
            DiscountAccountId = getConfigValueByType("SalesDiscountAccount").ToString
        Else : DiscountAccountId = 0
        End If

        If Not getConfigValueByType("InvoiceWiseTaxPercent").ToString = "Error" Then
            flgInvoiceWiseTaxPercent = getConfigValueByType("InvoiceWiseTaxPercent").ToString
        Else
            flgInvoiceWiseTaxPercent = False
        End If

        If Not getConfigValueByType("EnabledBrandedSMS").ToString = "Error" Then
            EnabledBrandedSMS = getConfigValueByType("EnabledBrandedSMS")
        End If

        If Not getConfigValueByType("DeductionWHTaxOnTotal").ToString = "Error" Then
            DeductionWHTaxOnTotal = getConfigValueByType("DeductionWHTaxOnTotal")
        End If

        Me.Cursor = Cursors.Default
        Me.lblProgress.Visible = False
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpDate.Value.ToString("yyyy-M-d 00:00:00") Then
                    ShowErrorMessage("Previous date work not allowed") : Exit Sub
                End If
            End If

            If IsDateLock(Me.dtpDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
            End If
            'If Not IsValidate() Then Exit Sub Comment against task:2381
            If Not msg_Confirm(str_ConfirmDelete) Then Exit Sub
            If Delete() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 
            Me.GrdSaved.CurrentRow.Delete()
            Me.ReSetControls()
            'MsgBox("Record Successfully Delete", MsgBoxStyle.Information, str_MessageHeader)
            ' msg_Information(str_informDelete)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Call GetAllRecords()  'Task 2389 Ehtisham ul Haq, reload history after delete record 21-1-14
        End Try
    End Sub
    Private Sub cmbInvoiceList_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbInvoiceList.Enter
        Me.cmbInvoiceList.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub
    Private Sub txtRemarks_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRemarks.Leave
        If Me.txtRemarks.Text = "" AndAlso Me.cmbInvoiceList.Value <> 0 Then
            strInvoiceRemark = "Payment Against Invoice No. " & Me.cmbInvoiceList.Text & ""
        Else
            strInvoiceRemark = Me.txtRemarks.Text
        End If
        Me.txtRemarks.Text = Me.strInvoiceRemark
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub EditRecord()
        Try

            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpDate.Value.ToString("yyyy-M-d 00:00:00") Then
                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    Me.dtpDate.Enabled = False
                Else
                    Me.dtpDate.Enabled = True
                End If
            Else
                Me.dtpDate.Enabled = True
            End If

            If Me.GrdSaved.RowCount = 0 Then Exit Sub
            EditMode = True
            If EditMode = True Then
                FillCombos("Customer")
                FillCombos("Payfrom")
            End If
            Me.BtnSave.Text = "&Update"
            MasterID = Me.GrdSaved.GetRow.Cells("PaymentId").Value.ToString
            Me.txtReference.Text = Me.GrdSaved.GetRow.Cells("Remarks").Value.ToString
            Me.cmbAccounts.Value = Me.GrdSaved.GetRow.Cells("VendorCode").Value

            'Task 3199 Commented Against Cost Center 
            'Me.cmbCostCenter.SelectedValue = Me.GrdSaved.GetRow.Cells("CostCenterId").Value

            Me.cmbPaymentMethod.SelectedValue = Me.GrdSaved.GetRow.Cells("PaymentMethod").Value
            Me.cmbPayFrom.SelectedValue = Me.GrdSaved.GetRow.Cells("PaymentAccountId").Value
            Me.txtVoucherNo.Text = Me.GrdSaved.GetRow.Cells("PaymentNo").Value.ToString
            Me.dtpDate.Value = Me.GrdSaved.GetRow.Cells("PaymentDate").Value
            'Task No 2537 Append One Line Of Code For Geeting Value In Check Box 
            Me.ChkPost.Checked = Me.GrdSaved.GetRow.Cells("Post").Value
            Me.txtChequeNo.Text = Me.GrdSaved.GetRow.Cells("ChequeNo").Value.ToString
            If Not IsDBNull(Me.GrdSaved.GetRow.Cells("ChequeDate").Value) Then
                'Task 3199 Show Saved Grid Cheque Date Value when double click on Record 
                'If Me.GrdSaved.GetRow.Cells("ChequeDate").Value <> Me.GrdSaved.GetRow.Cells("ChequeDate").Value Then
                Me.DtpChequeDate.Value = Me.GrdSaved.GetRow.Cells("ChequeDate").Value
                'End If
            End If
            If Me.BtnSave.Text = "&Update" Then
                Me.cmbAccounts.Enabled = False

                'Task 3199
                'Me.cmbCostCenter.Enabled = False

                Me.cmbPaymentMethod.Enabled = False
            End If
            Me.cmbLayout.SelectedIndex = Val(Me.GrdSaved.GetRow.Cells("ChequeLayoutIndex").Value.ToString)
            Call DisplayRecord(Me.GrdSaved.GetRow.Cells("PaymentId").Value.ToString) 'Here Call Local Method Of Dispaly Record Wich Is Used For Detail Record
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.lblPrintStatus.Text = "Print Status: " & GrdSaved.GetRow.Cells("Print Status").Text.ToString
            'Task#04082015 Edit company name (Ahmad Sharif)
            Me.cmbCompany.Text = Me.GrdSaved.GetRow.Cells("Company Name").Value.ToString
            If Not IsDBNull(Me.GrdSaved.GetRow.Cells("DueDate").Value) Then
                Me.DtpFillDate.Checked = True
                Me.DtpFillDate.Value = Me.GrdSaved.GetRow.Cells("DueDate").Value
            Else
                Me.DtpFillDate.Checked = False
            End If

            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnPrint.Visible = True
            Me.BtnEdit.Visible = True
            Me.BtnDelete.Visible = True

            'Task:2382 Visibility Payee Title Control
            If Me.cmbPaymentMethod.SelectedIndex > 0 Then
                Me.Label15.Visible = True
                Me.txtPayeeTitle.Visible = True
                Me.txtPayeeTitle.Text = GrdSaved.GetRow.Cells("PayeeTitle").Text.ToString
                Me.Label17.Visible = True
                Me.cmbLayout.Visible = True
            Else
                Me.Label15.Visible = False
                Me.txtPayeeTitle.Visible = False
                Me.txtPayeeTitle.Text = String.Empty
                Me.Label17.Visible = False
                Me.cmbLayout.Visible = False
            End If
            'End Task:2382
            GetSecurityRights(EnumDataMode.Edit) ''13-Aug-2014 Task:2782 Imran Ali Post Rights Implement On Invoice Base Payment\Receipt Voucher



            If getConfigValueByType("EnabledDuplicateVoucherLog").ToString.ToUpper = "TRUE" Then
                Dim intCountVouchers As Integer = 0
                Dim dtCountVouches As New DataTable
                dtCountVouches = GetDuplicateVouchers(Me.txtVoucherNo.Text)
                dtCountVouches.AcceptChanges()
                Dim VoucherId As Integer = 0I
                If dtCountVouches.Rows.Count > 0 Then
                    VoucherId = Val(dtCountVouches.Rows(0).Item("voucher_id").ToString)
                    intCountVouchers = dtCountVouches.Rows.Count
                End If
                btnUpdateTimes.Text = "No of update times (" & intCountVouchers & ")"
                Call CreateContextMenu(Val(VoucherId), btnUpdateTimes)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbAccounts_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccounts.Enter
        Me.cmbAccounts.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.Dropdown)
    End Sub

    Private Sub cmbAccounts_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbAccounts.KeyDown
        ''TFS1781 : Ayesha Rehman :Added for Selection of Vendor
        Try
            If e.KeyCode = Keys.F1 Then
                frmAccountSearch.AccountType = "'Vendor'"
                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbAccounts.Value = frmAccountSearch.SelectedAccountId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub cmbAccounts_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccounts.Leave
        Try
            If Me.cmbAccounts.IsItemInList = False Then Exit Sub
            FillCombos("Invoice")
            'Task 2537 Append One Line Of Code For Geeting Current Balance In Text Box 
            Me.txtCurrentBalance.Text = GetCurrentBalance(cmbAccounts.Value)
        Catch ex As Exception
            ShowErrorMessage(ex.Message) 'Throw ex 
        End Try
    End Sub

    Private Sub FillComboByEdit()
        Try
            FillCombos("Customer")
            FillCombos("Payfrom")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtInvoice.CheckedChanged, rbtVendorInvoice.CheckedChanged
        Try
            If Me.cmbInvoiceList.IsItemInList Then FillCombos("Invoice")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights(ByVal Mode As SBUtility.Utility.EnumDataMode)
        Try

            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                ''13-Aug-2014 Task:2782 Imran Ali Post Rights Implement On Invoice Base Payment\Receipt Voucher
                Me.ChkPost.Visible = True
                'Me.ChkPost.Checked = True
                'End Task:2782
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefColor)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else

                'If Mode = EnumDataMode.[New] Then
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'End If
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        ''13-Aug-2014 Task:2782 Imran Ali Post Rights Implement On Invoice Base Payment\Receipt Voucher
                    ElseIf RightsDt.FormControlName = "Post" Then
                        Me.ChkPost.Visible = True
                        If Mode = EnumDataMode.[New] Then Me.ChkPost.Checked = True
                    End If
                    'End TAsk:2782
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 0 Then
                Me.BtnLoadAll.Visible = False
                ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                Me.BtnEdit.Visible = False
                Me.BtnPrint.Visible = False
                Me.BtnDelete.Visible = False
                Me.BtnSave.Visible = True
                Me.CtrlGrdBar1.Visible = True
                Me.CtrlGrdBar2.Visible = False
            Else
                Me.BtnLoadAll.Visible = True
                GetAllRecords()
                ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                Me.BtnDelete.Visible = True
                Me.BtnPrint.Visible = True
                Me.BtnEdit.Visible = True
                Me.BtnSave.Visible = False
                Me.CtrlGrdBar1.Visible = False
                Me.CtrlGrdBar2.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPaidAmt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaidAmt.TextChanged
        Try
            Me.txtDueAmount.Text = Val(Me.txtInvoicoAmount.Text) - Val(Me.txtPaidAmt.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.dtpDate.Value.ToString("yyyy-M-d 00:00:00") Then
                If DateLock.Lock = True Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Sub ValidateDateLock()
        Try
            Dim dateLock As New SBModel.DateLockBE
            dateLock = DateLockList.Find(AddressOf chkDateLock)
            If dateLock IsNot Nothing Then
                If dateLock.DateLock.ToString.Length > 0 Then
                    flgDateLock = True
                Else
                    flgDateLock = False
                End If
            Else
                flgDateLock = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GrdPaymentDetail_CellUpdated(sender As Object, e As ColumnActionEventArgs) Handles GrdPaymentDetail.CellUpdated
        Try
            GetSalesTaxTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Task:2775 Delete Row By User
    Private Sub GrdPaymentDetail_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GrdPaymentDetail.ColumnButtonClick
        Try
            If e.Column.Key = "Column1" Then
                Me.GrdPaymentDetail.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2775


    Private Sub GrdPaymentDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GrdPaymentDetail.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        If e.KeyCode = Keys.F2 Then
            BtnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If
        'TASK:2398           Imran Ali        Update, Delete Problem in Cash Management
        'If e.KeyCode = Keys.Delete Then
        '    BtnDelete_Click(Nothing, Nothing)
        '    Exit Sub
        'End If
    End Sub

    '' 26-May-2014 TASK:2647 Imran Ali New Enhancement 
    Private Sub BtnPrintVoucher_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPrintVoucher.Click
        If Me.GrdSaved.RowCount = 0 Then Exit Sub
        'PrintLog = New SBModel.PrintLogBE
        ''PrintLog.DocumentNo = GrdSaved.GetRow.Cells("VoucherCode").Value.ToString
        'PrintLog.UserName = LoginUserName
        'PrintLog.PrintDateTime = Date.Now
        'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
        ''ShowReport("rptVoucher", "{VwGlVoucherSingle.VoucherId}=" & Me.grdVouchers.CurrentRow.Cells(0).Value & "")
        ''Chaning Against Request No 798
        'AddRptParam("@VoucherId", Me.GrdSaved.CurrentRow.Cells("VId").Value)
        'ShowReport("rptVoucher")
        PrintVoucherBC(Me.GrdSaved.GetRow.Cells("VId").Value, Me.GrdSaved.GetRow.Cells("PaymentNo").Value.ToString())
    End Sub
    'End Task:2647

    Private Sub BtnPrint_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.ButtonClick
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.GrdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = GrdSaved.GetRow.Cells("PaymentNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            AddRptParam("@PaymentId", Me.GrdSaved.CurrentRow.Cells("PaymentId").Value)
            ShowReport("rptinoivcebasepayment") ', "{VwGlVoucherSingle.Voucher_No}='" & Me.GrdSaved.CurrentRow.Cells("PaymentNo").Value & "'")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''16-Jul-2014 TASK:2745 Imran Ali Cheque Comments On Ledger (Ravi)
    Public Function GetComments(ByVal Row As DataRow) As String
        Try
            Dim str As String = String.Empty
            Dim blnCommentsChequeNo As Boolean = Boolean.Parse(getConfigValueByType("CommentsChequeNo").ToString)
            Dim blnCommentsChequeDate As Boolean = Boolean.Parse(getConfigValueByType("CommentsChequeDate").ToString)
            Dim blnCommentsPartyName As Boolean = Boolean.Parse(getConfigValueByType("CommentsPartyName").ToString)
            If Me.cmbPaymentMethod.Text = "Bank" Then
                If Row IsNot Nothing Then
                    If blnCommentsChequeNo = True Then
                        str += " Chq No. " & Me.txtChequeNo.Text & ""
                    End If
                    If blnCommentsChequeDate = True Then
                        str += " Chq Date. " & Me.DtpChequeDate.Value.Date & ""
                    End If
                    If blnCommentsPartyName = True Then
                        str += " Party Name. " & Me.cmbAccounts.ActiveRow.Cells("detail_title").Value.ToString & ""
                    End If
                End If
            End If
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task:2745

    ''12-Sep-2014 TASK:2838 Imran Ali Cheque Print Option in Invoice Based Payment
    Private Sub PrintChequeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintChequeToolStripMenuItem.Click
        Try
            AddRptParam("@Account_Name", Me.txtPayeeTitle.Text)
            AddRptParam("@Amount", Me.GrdPaymentDetail.GetTotal(Me.GrdPaymentDetail.RootTable.Columns("Net Amount"), AggregateFunction.Sum))
            AddRptParam("@Cheque_Date", Me.DtpChequeDate.Value)
            AddRptParam("@CrossCheque", 0)
            frmRptChequePrintReportViewer.ReportName = "rptChequePrint" & Me.cmbLayout.SelectedIndex
            frmRptChequePrintReportViewer.Show()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintCrossChequeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintCrossChequeToolStripMenuItem.Click
        Try
            AddRptParam("@Account_Name", Me.txtPayeeTitle.Text)
            AddRptParam("@Amount", Me.GrdPaymentDetail.GetTotal(Me.GrdPaymentDetail.RootTable.Columns("Net Amount"), AggregateFunction.Sum))
            AddRptParam("@Cheque_Date", Me.DtpChequeDate.Value)
            AddRptParam("@CrossCheque", True)
            frmRptChequePrintReportViewer.ReportName = "rptChequePrint" & Me.cmbLayout.SelectedIndex
            frmRptChequePrintReportViewer.Show()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2838
#Region "SMS Template Setting"
    Public Function GetSMSParamters() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("@AccountCode")
            str.Add("@AccountTitle")
            str.Add("@DocumentNo")
            str.Add("@DocumentDate")
            str.Add("@OtherDocNo")
            str.Add("@Remarks")
            str.Add("@Amount")
            str.Add("@ChequeNo")
            str.Add("@ChequeDate")
            str.Add("@CompanyName")
            str.Add("@CellNo")
            str.Add("@SIRIUS")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSMSKey() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("Invoice Based Cash Payment")
            str.Add("Invoice Based Bank Payment")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSMSTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMSTemplate.Click
        Try
            Dim frmSMS As New frmSMSTemplate
            ApplyStyleSheet(frmSMS)
            frmSMS.cmbKey.DataSource = GetSMSKey()
            frmSMS.lstParameters.DataSource = GetSMSParamters()
            frmSMS.Show()
            frmSMS.BringToFront()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
#End Region
    '2015-02-20 Task # 6 Add Payee Title in customer list By Ali Ansari
    Private Sub cmbAccounts_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccounts.ValueChanged

        Try
            If Me.cmbAccounts.IsItemInList = False Then
                Exit Sub
            End If
            If Me.cmbAccounts.ActiveRow Is Nothing Then
                Exit Sub
            End If

            Me.txtPayeeTitle.Text = cmbAccounts.ActiveRow.Cells("PayeeTitle").Value.ToString




        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try



    End Sub
    '2015-02-20 Task # 6 Add Payee Title in customer list By Ali Ansari
    'Task#04082015 company changed from combo box
    Private Sub cmbCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCompany.SelectedIndexChanged
        Try
            'Ali Faisal : TFS2278 : Voucher # to be remain same in edit mode
            If Not EditMode = True Then
                Me.txtVoucherNo.Text = GetVoucherNo()
            End If
            'Me.txtVoucherNo.Text = GetVoucherNo()
            'Ali Faisal : TFS2278 : End
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#04082015
    Private Sub PrintVoucherBC(ByVal voucherID As Integer, Optional ByVal voucherNo As String = Nothing, Optional print As Boolean = False) 'TASK42
        Dim DT As New DataTable
        DT = DTFromGrid(voucherID) 'GetDataTable("SP_RptVoucher " & r.Cells(EnumGridMaster.Voucher_Id).Value & "")
        DT.AcceptChanges()


        '   AddRptParam("@VoucherId", r.Cells("voucher_id").Value)
        ShowReport("rptVoucher", , , , print, , , DT)
        PrintLog = New SBModel.PrintLogBE
        PrintLog.DocumentNo = voucherNo 'r.Cells("Voucher_No").Value.ToString
        PrintLog.UserName = LoginUserName
        PrintLog.PrintDateTime = Date.Now
        Call SBDal.PrintLogDAL.PrintLog(PrintLog)

    End Sub
    Private Function DTFromGrid(ByVal voucherID As Int32) As DataTable 'TASK42
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Function
            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
            Dim DT As New DataTable
            DT = GetDataTable("SP_RptVoucher " & voucherID & "") ' r.Cells(EnumGridMaster.Voucher_Id).Value
            DT.AcceptChanges()
            'DT.Columns.Add("Convert(image, null) as BarCode")
            'Next
            For Each DR As DataRow In DT.Rows
                DR.BeginEdit()
                Dim bcp As New BarcodeProfessional()
                'bcp.Symbology = Symbology.Code39
                bcp.Symbology = Symbology.Code128
                'bcp.Symbology = Symbology.Code93



                bcp.Extended = True
                bcp.DisplayCode = False

                ' bcp.Text=Symbology.
                'bcp.Text = String.Empty
                'bcp.Symbology = Symbology.Code128

                bcp.AddChecksum = False

                'bcp.BarWidth = 3
                'bcp.BarHeight = 0.04F
                'DR.Item("Convert(image, null) as BarCode")
                bcp.Code = "?" & DR.Item("voucher_no").ToString
                'bcp.Code = DR.Item("Employee_Code").ToString
                DR.Item("BarCode") = bcp.GetBarcodeImage(System.Drawing.Imaging.ImageFormat.Png)
                'LoadPicture(DR, "Picture", DR.Item("EmpPicture"))


                DR.EndEdit()


            Next
            Return DT
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Function
    Private Sub txtTaxPercent_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTaxPercent.KeyPress, txtInvoiceTaxAmount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtInvoiceTaxAmount_TextChanged(sender As Object, e As EventArgs) Handles txtInvoiceTaxAmount.TextChanged
        Try

            If Val(Me.txtTaxPercent.Text) = 0 Then
                Me.txtSalesTaxAmount.Text = Me.txtInvoiceTaxAmount.Text
            Else
                Me.txtSalesTaxAmount.Text = (Val(Me.txtTaxPercent.Text) / 100) * Val(Me.txtInvoiceTaxAmount.Text)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtTaxPercent_TextChanged(sender As Object, e As EventArgs) Handles txtTaxPercent.TextChanged
        Try
            If Val(Me.txtInvoiceTaxAmount.Text) = 0 Then
                Me.txtSalesTaxAmount.Text = Me.txtSalesTaxAmount.Text
            Else
                Me.txtSalesTaxAmount.Text = (Val(Me.txtTaxPercent.Text) / 100) * Val(Me.txtInvoiceTaxAmount.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetSalesTaxTotal()
        Try
            Me.GrdPaymentDetail.UpdateData()
            If Val(Me.GrdPaymentDetail.GetRow.Cells(enmGrdPaymentDetail.TaxPercent).Value.ToString) > 0 Then
                If Val(Me.GrdPaymentDetail.GetRow.Cells(enmGrdPaymentDetail.InvoiceTax).Value.ToString) > 0 Then
                    Me.GrdPaymentDetail.GetRow.Cells(enmGrdPaymentDetail.SalesTaxAmount).Value = (Val(Me.GrdPaymentDetail.GetRow.Cells(enmGrdPaymentDetail.TaxPercent).Value.ToString) / 100) * Val(Val(Me.GrdPaymentDetail.GetRow.Cells(enmGrdPaymentDetail.InvoiceTax).Value.ToString))
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GrdPaymentDetail_RecordsDeleted(sender As Object, e As EventArgs) Handles GrdPaymentDetail.RecordsDeleted
        Try
            GetSalesTaxTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not GrdSaved.GetRow Is Nothing AndAlso GrdSaved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frmPaymentVoucherNew"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = GrdSaved.CurrentRow.Cells(1).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Invoice base Payment Voucher (" & frmtask.Ref_No & ") "
                frmtask.Width = 950
                frmtask.ShowDialog()
                frmtask.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
                'frmtask.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click
        Try
            If Not frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
                frmMain.LoadControl("frmSystemConfiguration")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Accounts
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdPaymentDetail.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdPaymentDetail.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GrdPaymentDetail.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Invoice Based Payment"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GrdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Invoice Based Payment"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class