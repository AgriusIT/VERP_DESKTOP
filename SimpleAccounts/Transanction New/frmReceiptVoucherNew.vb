''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''7-Jan-2014   Task:2370       Imran Ali     Sale and purchase invoice wise aging report 
''13-Jan-2014   Task:2375        Imran Ali        Covnerter Problems And Development
'' 24-Jan-2014     TASK:2381          Imran Ali          Reasign Invoice Based Payment object  
''29-Jan-2014        TASK:2398           Imran Ali        Update, Delete Problem in Cash Management
''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
''07-Mar-2014 Task:2470 Add month in invoice base payment narration
''01-Apr-2014 TASK:2534 Imran Ali Automobile Development/Problem
''04-Apr-2014 TASK2539 Imran Ali  Add Debit No/Credit Amount Fields on InvoiceBasedPayment/Receipt 
' Task No 2537 Amendments Regarding Post checkBox ,cURENT Balance and Sale Tax 
'28-4-2014 TASK:M35 Imran Ali Show All Record 
''26-May-2014 TASK:2647 Imran Ali New Enhancement 
''14-Jul-2014 TASK:2736 Imran Ali Delete Voucher Problem On Invoice Base Receipt
''16-Jul-2014 TASK:2745 Imran Ali Cheque Comments On Ledger (Ravi)
''24-Jul-2014 Task:2759 Imran ali Amount Round on all transaction forms
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''07-Aug-2014 Task:2775 Imran Ali Add Delete Button Field On Grid Detail in InvoiceBasedReceipt/Payment (Ravi)
''13-Aug-2014 Task:2782 Imran Ali Post Rights Implement On Invoice Base Payment\Receipt Voucher
''3-Sep-2014 TASK:2823 Imran Ali Invoice Aging Formated Report Revised (Ravi)
''12-Sep-2014 Task:2839 Imran Ali Change Description In Invoice Based Receipt Voucher
''13-Sep-2014 Task:2844 Imran Ali Posted Balance Show On Invoice Based Receipt/Payment
'04-Aug-2015 'Task#04082015 Ahmad Sharif Concatenate Company location id with prefix of invoice number if configuration on of company wise prefix
''27-10-15 TASKM2710151 Imran Ali: Create Duplication Voucher On Update Mode.

Imports SBDal
Imports SBModel
Imports SBUtility
Imports Janus.Windows.GridEX
Imports Neodynamic.SDK.Barcode

Public Class frmReceiptVoucherNew
    Implements IGeneral
    Dim ObjMod As InvoicesBasedReceiptMaster
    Dim MasterID As Integer = 0
    Dim EditMode As Boolean = False
    Dim strInvoiceRemark As String = String.Empty
    Dim PrintLog As PrintLogBE
    Dim flgInvoiceWiseTaxPercent As Boolean = False
    Dim EnabledBrandedSMS As Boolean = False
    Dim flgCompanyRights As Boolean = False


    Enum enmReceiptMaster
        ReceiptID
        ReceiptDate
        ReceiptNo
        Remarks
        CustomerCode
        CustomerName
        ReceiptAmount
        ChequeNo
        ChequeDate
        PaymentMethod
        PaymentAccountId
        CostCenterId
    End Enum
    Enum enmGrdReceiptDetail
        ReceiptDetailId
        SalesID
        SalesNo
        SalesDate
        Remarks
        Gst_Percentage
        Gst_Amount
        'Task:2370 Added Index
        SalesTaxAmount
        OtherTaxAmount
        OtherTaxAccountId
        'End Task:2370
        NetAmount 'TAsk:2775 Added Index
        SalesAmount
        ReceiptAmount
        BalanceAmount
        Description 'Task:2470 Added Index
        'Task 3198
        CostCenter
    End Enum

    Private Sub frmReceiptVoucherNew_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 9 -1-14
            If e.KeyCode = Keys.F4 Then
                If BtnSave.Enabled = True Then
                    SaveToolStripButton_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(BtnNew, Nothing)
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
    Private Sub frmReceiptVoucherNew_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
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
            If Me.cmbPaymentMethod.SelectedIndex = 0 Then
                Me.lblChequeNo.Visible = False
                Me.lblChequeDate.Visible = False
                Me.txtChequeNo.Visible = False
                Me.DtpChequeDate.Visible = False
            End If
            If Not getConfigValueByType("InvoiceWiseTaxPercent").ToString = "Error" Then
                flgInvoiceWiseTaxPercent = getConfigValueByType("InvoiceWiseTaxPercent").ToString
            Else
                flgInvoiceWiseTaxPercent = False
            End If
            If Not getConfigValueByType("EnabledBrandedSMS").ToString = "Error" Then
                EnabledBrandedSMS = getConfigValueByType("EnabledBrandedSMS")
            End If
            Me.ReSetControls()


            'TASK3360_Aashir_Added select/contain filters on account feilds in all transaction screens
            UltraDropDownSearching(cmbAccounts, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally

            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        End Try
    End Sub
    Private Sub frmReceiptVoucherNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

            'dr(0) = Convert.ToInt32(4)
            dr(0) = Convert.ToInt32(5)
            dr(1) = "Bank"
            dt.Rows.InsertAt(dr, 0)

            'dr1(0) = Convert.ToInt32(2)
            dr1(0) = Convert.ToInt32(3)
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

            '603867686
            '5787
            If cmbInvoiceList.ActiveRow Is Nothing Then Exit Sub 'Task:2375 Check Invoices
            Dim Dt As New DataTable
            Dt = CType(Me.GrdReceiptDetail.DataSource, DataTable)
            Dt.AcceptChanges()
            Dim drFound() As DataRow = Dt.Select("SalesId=" & Me.cmbInvoiceList.Value & "")
            If drFound.Length > 0 Then
                ShowErrorMessage("Invoice No. [" & cmbInvoiceList.ActiveRow.Cells("SalesNo").Text.ToString & "] already exist")
                Exit Sub
            End If
            Dim Dr As DataRow
            Dr = Dt.NewRow
            Dr(enmGrdReceiptDetail.SalesID) = Me.cmbInvoiceList.Value
            Dr(enmGrdReceiptDetail.SalesNo) = Me.cmbInvoiceList.SelectedRow.Cells(1).Value
            Dr(enmGrdReceiptDetail.SalesDate) = Me.cmbInvoiceList.SelectedRow.Cells(2).Value
            Dr(enmGrdReceiptDetail.Remarks) = Me.txtRemarks.Text
            'Dr(enmGrdReceiptDetail.Gst_Percentage) = 0
            Dr(enmGrdReceiptDetail.Gst_Percentage) = Val(Me.txtWHTax.Text)
            Dr(enmGrdReceiptDetail.Gst_Amount) = 0
            Dr(enmGrdReceiptDetail.SalesAmount) = Val(Me.txtInvoicoAmount.Text)
            'Task:2370 Added Column In Add Items SalesTaxAmount, OtherTaxAmount,OtherTaxAccountId
            Dr(enmGrdReceiptDetail.SalesTaxAmount) = Val(Me.txtSalesTaxAmount.Text)
            Dr(enmGrdReceiptDetail.OtherTaxAmount) = Val(Me.txtOtherTaxAmount.Text)
            Dr(enmGrdReceiptDetail.OtherTaxAccountId) = 0
            'End Task:2370
            Dr(enmGrdReceiptDetail.ReceiptAmount) = Val(Me.txtReceivedAmt.Text)

            'Task 3198 Add CostCenter Value in Grid
            Dr(enmGrdReceiptDetail.CostCenter) = Me.cmbCostCenter.SelectedValue

            Dt.Rows.InsertAt(Dr, 0)
            'Dt.Columns(enmGrdReceiptDetail.Gst_Amount).Expression = "(([Receipt Amount] * [Tax])/100)"
            If flgInvoiceWiseTaxPercent = True Then

                'Task 3197 Commented against 

                'Dt.Columns(enmGrdReceiptDetail.Gst_Amount).Expression = "(([Tax]*[Receipt Amount])/100)"

                Dt.Columns(enmGrdReceiptDetail.Gst_Amount).Expression = "(([Tax]*([Receipt Amount]-([OtherTaxAmount]+[SalesTaxAmount])))/100)"

            Else

                'Task 3197 Commented against 
                'Dt.Columns(enmGrdReceiptDetail.Gst_Percentage).Expression = "(iif([Tax Amount]=0,0,([Tax Amount]/[Receipt Amount])*100))"

                Dt.Columns(enmGrdReceiptDetail.Gst_Percentage).Expression = "(iif([Tax Amount]=0,0,([Tax Amount]/([Receipt Amount]-([Tax Amount]+[OtherTaxAmount]+[SalesTaxAmount])))*100))"

            End If
            'Dt.Columns(enmGrdReceiptDetail.BalanceAmount).Expression = "(([Invoice Amount] - [Receipt Amount]))"

            Dt.Columns(enmGrdReceiptDetail.BalanceAmount).Expression = "(([Invoice Amount] - ([Receipt Amount]-([Tax Amount]+[OtherTaxAmount]+[SalesTaxAmount]))))"
            Dt.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            'Me.GrdReceiptDetail.AutoSizeColumns()
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GrdReceiptDetail.RootTable.Columns
                'Before against task:2370
                'If Not col.Caption = "Remarks" AndAlso Not col.Caption = "Receipt Amount" AndAlso Not col.Caption = "Tax" AndAlso Not col.Caption = "Tax Amount" Then
                '    col.EditType = EditType.NoEdit
                'Task:2370 Change Edit Columns
                'Before against task:2470
                'If Not col.Caption = "Remarks" AndAlso Not col.Caption = "Receipt Amount" AndAlso Not col.Caption = "Tax" AndAlso Not col.Caption = "Tax Amount" AndAlso Not col.DataMember = "SalesTaxAmount" AndAlso Not col.DataMember = "OtherTaxAmount" AndAlso Not col.DataMember = "OtherTaxAccountId" Then
                'Task:2470 Set Editable Column Description
                If Not col.Caption = "Remarks" AndAlso Not col.Caption = "Receipt Amount" AndAlso Not col.Caption = "Tax" AndAlso Not col.Caption = "Tax Amount" AndAlso Not col.DataMember = "SalesTaxAmount" AndAlso Not col.DataMember = "OtherTaxAmount" AndAlso Not col.DataMember = "OtherTaxAccountId" AndAlso Not col.DataMember = "Description" AndAlso Not col.DataMember = "CostCenter" Then
                    'End Task:2470
                    col.EditType = EditType.NoEdit
                    'End Task:2370
                End If
                If col.Caption = "Receipt Detail Id" Or col.Caption = "Sales Id" Or col.Caption = "[Tax Amount]" Then
                    col.Visible = False
                End If
            Next
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.Remarks).Width = 250
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.SalesDate).Width = 100
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.SalesNo).Width = 100
            'Me.GrdReceiptDetail.RootTable.Columns("Tax").FormatString = "N"
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.Gst_Amount).AggregateFunction = AggregateFunction.Sum
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.SalesAmount).AggregateFunction = AggregateFunction.Sum
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.ReceiptAmount).AggregateFunction = AggregateFunction.Sum
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.BalanceAmount).AggregateFunction = AggregateFunction.Sum
            'Task:2370 Set Formating And Aggregate Funciton 


            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.Gst_Amount).TextAlignment = TextAlignment.Far
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.SalesAmount).TextAlignment = TextAlignment.Far
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.ReceiptAmount).TextAlignment = TextAlignment.Far
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.BalanceAmount).TextAlignment = TextAlignment.Far
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.Gst_Amount).HeaderAlignment = TextAlignment.Far
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.SalesAmount).HeaderAlignment = TextAlignment.Far
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.ReceiptAmount).HeaderAlignment = TextAlignment.Far
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.BalanceAmount).HeaderAlignment = TextAlignment.Far
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.Gst_Percentage).TextAlignment = TextAlignment.Far
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.Gst_Percentage).HeaderAlignment = TextAlignment.Far

            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.SalesTaxAmount).AggregateFunction = AggregateFunction.Sum
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.OtherTaxAmount).AggregateFunction = AggregateFunction.Sum

            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.SalesTaxAmount).TextAlignment = TextAlignment.Far
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.OtherTaxAmount).TextAlignment = TextAlignment.Far
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.SalesTaxAmount).HeaderAlignment = TextAlignment.Far
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.OtherTaxAmount).HeaderAlignment = TextAlignment.Far

            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.SalesTaxAmount).Caption = "Sales Tax"
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.OtherTaxAmount).Caption = "Others"
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.OtherTaxAccountId).Caption = "Others Account"
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.Gst_Percentage).Caption = "W/T Tax %"
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.Gst_Amount).Caption = "W/T Tax Amount"

            'Task2759: Set rounded amount format
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.Gst_Amount).FormatString = "N" & DecimalPointInValue
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.Gst_Amount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.BalanceAmount).FormatString = "N" & DecimalPointInValue
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.BalanceAmount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.SalesTaxAmount).FormatString = "N" & DecimalPointInValue
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.SalesTaxAmount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.OtherTaxAmount).FormatString = "N" & DecimalPointInValue
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.OtherTaxAmount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.ReceiptAmount).FormatString = "N" & DecimalPointInValue
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.ReceiptAmount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
            'Task:2775 Format 
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.NetAmount).FormatString = "N" & DecimalPointInValue
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.NetAmount).TotalFormatString = "N" & TotalAmountRounding ''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration

            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.NetAmount).AggregateFunction = AggregateFunction.Sum
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.NetAmount).TextAlignment = TextAlignment.Far
            Me.GrdReceiptDetail.RootTable.Columns(enmGrdReceiptDetail.NetAmount).HeaderAlignment = TextAlignment.Far
            'End Task:2775
            'End Task:2759

            Me.GrdReceiptDetail.AutoSizeColumns()
            'End Task:2370 
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            ''Task:2381 Reasign Invoice Based Payment object
            ObjMod = New InvoicesBasedReceiptMaster
            ObjMod.ReceiptNo = Me.GrdSaved.GetRow.Cells("ReceiptNo").Value.ToString
            ObjMod.ReceiptNo = Me.GrdSaved.GetRow.Cells("ReceiptNo").Value.ToString
            ObjMod.VoucherMaster = New VouchersMaster
            ObjMod.RVNo = Me.GrdSaved.GetRow.Cells("ReceiptNo").Value.ToString
            ''14-Jul-2014 TASK:2736 Imran Ali Delete Voucher Problem On Invoice Base Receipt
            ObjMod.VoucherMaster.VoucherCode = ObjMod.RVNo
            ObjMod.VoucherMaster.VoucherNo = ObjMod.RVNo
            ObjMod.VoucherMaster.VNo = ObjMod.RVNo
            'End Task:2736
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
            If New InvoicesBasedReceiptDAL().Delete(ObjMod) Then Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "Customer" Then
                'Dim str As String = "Select coa_detail_id, detail_title,sub_sub_title AS [Sub Sub A/c], CityName From vwCOADetail WHERE Account_Type='Customer' AND detail_title Is Not Null"
                Dim str As String = "Select coa_detail_id, detail_title,detail_code, sub_sub_title AS [Sub Sub A/c], CityName, Contact_Mobile as Mobile From vwCOADetail WHERE detail_title Is Not Null"
                ''TFS4683 : 02-10-2018 : Ayesha Rehman : Implemended Show Vendor On Sales Configuration
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    str += " And (Account_Type in ('Vendor','Customer')) "
                Else
                    str += " And Account_Type='Customer' "
                End If
                If EditMode = False Then
                    str += " AND vwCOADetail.Active=1"
                End If
                str += "ORDER BY vwCOADetail.detail_title"
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

                'Dim str As String = "select coa_detail_id,detail_title from vwCoaDetail where account_type='" & Me.cmbPaymentMethod.Text & "'   AND detail_title Is Not Null"
                If EditMode = False Then
                    Str += " AND vwCOADetail.Active=1"
                End If
                str += " ORDER BY vwCOADetail.detail_title"
                FillDropDown(cmbPayFrom, Str, True)
            ElseIf Condition = "Invoice" Then
                'Before against task:2370
                'Dim str As String = " SELECT SalesMasterTable.SalesID,  SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, (SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)),  (SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)) - ISNULL(Receipts.ReceivedAmount, 0) AS InvoiceBalance " & _
                '      " FROM SalesMasterTable INNER JOIN tblCOAMainSubSubDetail ON SalesMasterTable.CustomerCode = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select SalesId, SUM(((Qty*Price)*TaxPercent)/100) as Tax From SalesDetailTable Group by SalesId) SalesTax ON SalesTax.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN(SELECT invoiceId, SUM(IsNull(ReceiptAmount,0) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedReceiptsDetails GROUP BY InvoiceId) Receipts ON SalesMasterTable.SalesId = Receipts.invoiceId " & _
                '      " WHERE ((SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)) - ISNULL(Receipts.ReceivedAmount, 0) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND SalesMasterTable.SalesDate<=Convert(Datetime, '" & Me.DtpFillDate.Value & "',102)", "") & ""
                'str = str & IIf(Me.cmbAccounts.Value > 0, "AND(SalesMasterTable.CustomerCode=" & Me.cmbAccounts.Value & ")", "")
                'Task:2370 Add Tax
                'Before against task:2534
                'Dim str As String = " SELECT SalesMasterTable.SalesID,  SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, (SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)),  (SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)) - ISNULL(Receipts.ReceivedAmount, 0) AS InvoiceBalance " & _
                '     " FROM SalesMasterTable INNER JOIN tblCOAMainSubSubDetail ON SalesMasterTable.CustomerCode = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select SalesId, (SUM(((Qty*Price)*TaxPercent)/100)+SUM(((Qty*Price)*SEDPercent)/100))  as Tax From SalesDetailTable Group by SalesId) SalesTax ON SalesTax.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN(SELECT invoiceId, SUM(IsNull(ReceiptAmount,0) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedReceiptsDetails GROUP BY InvoiceId) Receipts ON SalesMasterTable.SalesId = Receipts.invoiceId " & _
                '     " WHERE ((SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)) - ISNULL(Receipts.ReceivedAmount, 0) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND SalesMasterTable.SalesDate<=Convert(Datetime, '" & Me.DtpFillDate.Value & "',102)", "") & ""
                'str = str & IIf(Me.cmbAccounts.Value > 0, "AND(SalesMasterTable.CustomerCode=" & Me.cmbAccounts.Value & ")", "")
                'Task:2534 Added SubQuery And Set Filter On InvoiceBalance
                'Before against task:2539
                ' Dim str As String = "  SELECT SalesID, SalesNo, SalesDate, InvoiceAmount, InvoiceBalance From  (SELECT SalesMasterTable.SalesID,  SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, (SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)) as InvoiceAmount,  (SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)) - ISNULL(Receipts.ReceivedAmount, 0) AS InvoiceBalance " & _
                '" FROM SalesMasterTable INNER JOIN tblCOAMainSubSubDetail ON SalesMasterTable.CustomerCode = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select SalesId, (SUM(((Qty*Price)*TaxPercent)/100)+SUM(((Qty*Price)*SEDPercent)/100))  as Tax From SalesDetailTable Group by SalesId) SalesTax ON SalesTax.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN(SELECT invoiceId, SUM(IsNull(ReceiptAmount,0) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedReceiptsDetails GROUP BY InvoiceId) Receipts ON SalesMasterTable.SalesId = Receipts.invoiceId " & _
                '" WHERE ((SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)) - ISNULL(Receipts.ReceivedAmount, 0) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND SalesMasterTable.SalesDate<=Convert(Datetime, '" & Me.DtpFillDate.Value & "',102)", "") & ""
                ' str = str & IIf(Me.cmbAccounts.Value > 0, "AND(SalesMasterTable.CustomerCode=" & Me.cmbAccounts.Value & ")", "") & ") a WHERE a.InvoiceBalance <> 0"
                'Task:2539 Added SubQuery for CreditAmount

                'Dim str As String = "  SELECT SalesID, SalesNo, SalesDate, InvoiceAmount, Isnull([Credit Amount],0) as [Credit Amount], (Isnull(InvoiceBalance,0)) as InvoiceBalance From  (SELECT SalesMasterTable.SalesID,  SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, (SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)) as InvoiceAmount, IsNull(SalesReturnAmount,0) as [Credit Amount], ((IsNull(SalesMasterTable.SalesAmount,0)+ISNULL(SalesTax.Tax,0))-IsNull(SalesReturnAmount,0)) - ISNULL(Receipts.ReceivedAmount, 0) AS InvoiceBalance " & _
                '      " FROM SalesMasterTable INNER JOIN tblCOAMainSubSubDetail ON SalesMasterTable.CustomerCode = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select SalesId, (SUM(((Qty*Price)*TaxPercent)/100)+SUM(((Qty*Price)*SEDPercent)/100))  as Tax From SalesDetailTable Group by SalesId) SalesTax ON SalesTax.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN(SELECT invoiceId, SUM(IsNull(ReceiptAmount,0) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedReceiptsDetails GROUP BY InvoiceId) Receipts ON SalesMasterTable.SalesId = Receipts.invoiceId " & _
                '      " LEFT OUTER JOIN(Select SalesReturn.POId, (Isnull(SalesReturn.SalesReturnAmount,0)+Isnull(Tax,0)) as SalesReturnAmount From(Select POId, SUM(Isnull(SalesReturnAmount,0)) as SalesReturnAmount From SalesReturnMasterTable WHERE POId <> 0 AND dbo.SalesReturnMasterTable.CustomerCode=" & IIf(Me.cmbAccounts.Value Is Nothing, 0, Me.cmbAccounts.Value) & "" & _
                '      " Group By POId) SalesReturn " & _
                '      " LEFT OUTER JOIN(Select a.POId, SUM(((Qty*Price)*Tax_Percent)/100) as Tax From SalesReturnDetailTable b INNER JOIN SalesReturnMasterTable a on a.SalesReturnId = b.SalesReturnId  " & _
                '      " Group By a.POId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.POId = SalesReturn.POId) Ret On Ret.POId  = SalesMasterTable.SalesId " & _
                '      " WHERE ((SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)) - ISNULL(Receipts.ReceivedAmount, 0) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND SalesMasterTable.SalesDate<=Convert(Datetime, '" & Me.DtpFillDate.Value & "',102)", "") & ""
                ''3-Sep-2014 TASK:2823 Imran Ali Invoice Aging Formated Report Revised (Ravi)
                'Waqar Start Commented these lines for Invoice Due not due report
                ' ''Dim str As String = "  SELECT SalesID, SalesNo, SalesDate, IsNull(InvoiceAmount,0) as InvoiceAmount,  IsNull(AdjInv.Adj,0) as Adjustment,Isnull([Credit Amount],0) as [Credit Amount], (Isnull(InvoiceBalance,0)-IsNull(AdjInv.Adj,0)) as InvoiceBalance From  (SELECT SalesMasterTable.SalesID,  SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, (SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)) as InvoiceAmount, IsNull(SalesReturnAmount,0) as [Credit Amount], ((IsNull(SalesMasterTable.SalesAmount,0)+ISNULL(SalesTax.Tax,0))-IsNull(SalesReturnAmount,0)) - ISNULL(Receipts.ReceivedAmount, 0) - IsNull(Vocher.VocherAmount,0) AS InvoiceBalance " & _
                ' ''      " FROM SalesMasterTable INNER JOIN tblCOAMainSubSubDetail ON SalesMasterTable.CustomerCode = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select SalesId, (SUM(((Qty*Price)*TaxPercent)/100)+SUM(((Qty*Price)*SEDPercent)/100))  as Tax From SalesDetailTable Group by SalesId) SalesTax ON SalesTax.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN(SELECT invoiceId, SUM(IsNull(ReceiptAmount,0) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedReceiptsDetails GROUP BY InvoiceId) Receipts ON SalesMasterTable.SalesId = Receipts.invoiceId " & _
                ' ''     " LEFT OUTER JOIN (SELECT     InvoiceId, IsNull(IsNull(SUM(NetAmount),0),0) AS VocherAmount FROM dbo.tblVoucherDetail GROUP BY InvoiceId ) Vocher on Vocher.InvoiceId = SalesMasterTable.SalesId " & _
                ' ''      " LEFT OUTER JOIN(Select SalesReturn.POId, (Isnull(SalesReturn.SalesReturnAmount,0)+Isnull(Tax,0)) as SalesReturnAmount From(Select POId, SUM(Isnull(SalesReturnAmount,0)) as SalesReturnAmount From SalesReturnMasterTable WHERE POId <> 0 AND dbo.SalesReturnMasterTable.CustomerCode=" & IIf(Me.cmbAccounts.Value Is Nothing, 0, Me.cmbAccounts.Value) & "" & _
                ' ''      " Group By POId) SalesReturn " & _
                ' ''      " LEFT OUTER JOIN(Select a.POId, SUM(((Qty*Price)*Tax_Percent)/100) as Tax From SalesReturnDetailTable b INNER JOIN SalesReturnMasterTable a on a.SalesReturnId = b.SalesReturnId  " & _
                ' ''      " Group By a.POId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.POId = SalesReturn.POId) Ret On Ret.POId  = SalesMasterTable.SalesId " & _
                ' ''      " WHERE ((SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)) - ISNULL(Receipts.ReceivedAmount, 0) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND (Convert(varchar,SalesMasterTable.SalesDate,102) <=  Convert(Datetime, '" & Me.DtpFillDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))", "") & "" ' AND SalesMasterTable.SalesId Not In (Select InvoiceId From InvoiceAdjustmentTable WHERE InvoiceType='Sales')"
                '' ''End Task:2823
                ' ''str = str & IIf(Me.cmbAccounts.Value > 0, " AND(SalesMasterTable.CustomerCode=" & Me.cmbAccounts.Value & ")", "") & ") a  LEFT OUTER JOIN(SELECT InvoiceId,coa_detail_Id, SUM(ISNULL(AdjustmentAmount, 0)) AS Adj FROM  dbo.InvoiceAdjustmentTable WHERE InvoiceType='Sales' GROUP BY InvoiceId,coa_detail_Id) AdjInv On AdjInv.InvoiceID = a.SalesId  WHERE (IsNull(a.InvoiceBalance,0)-IsNull(AdjInv.Adj,0)) <> 0"
                'Waqar End Commented these lines for Invoice Due not due report


                Dim str As String = "  SELECT SalesID, SalesNo, SalesDate, IsNull(InvoiceAmount,0) as InvoiceAmount,  IsNull(AdjInv.Adj,0) as Adjustment,Isnull([Credit Amount],0) as [Credit Amount], (Isnull(InvoiceBalance,0)-IsNull(AdjInv.Adj,0)) as InvoiceBalance From  (SELECT SalesMasterTable.SalesID,  SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, (SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)) as InvoiceAmount, 0 as [Credit Amount], ((IsNull(SalesMasterTable.SalesAmount,0)+ISNULL(SalesTax.Tax,0))) - ISNULL(Receipts.ReceivedAmount, 0) AS InvoiceBalance " & _
                      " FROM SalesMasterTable INNER JOIN tblCOAMainSubSubDetail ON SalesMasterTable.CustomerCode = tblCOAMainSubSubDetail.coa_detail_id LEFT OUTER JOIN(Select SalesId, (SUM(((Qty*Price)*TaxPercent)/100)+SUM(((Qty*Price)*SEDPercent)/100))  as Tax From SalesDetailTable Group by SalesId) SalesTax ON SalesTax.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN(SELECT invoiceId, SUM(IsNull(ReceiptAmount,0) + IsNull(AdjustAmount,0)) AS ReceivedAmount FROM InvoiceBasedReceiptsDetails GROUP BY InvoiceId) Receipts ON SalesMasterTable.SalesId = Receipts.invoiceId " & _
                     " LEFT OUTER JOIN (SELECT     InvoiceId, IsNull(IsNull(SUM(NetAmount),0),0) AS VocherAmount FROM dbo.tblVoucherDetail GROUP BY InvoiceId ) Vocher on Vocher.InvoiceId = SalesMasterTable.SalesId " & _
                      " LEFT OUTER JOIN(Select SalesReturn.POId, (Isnull(SalesReturn.SalesReturnAmount,0)+Isnull(Tax,0)) as SalesReturnAmount From(Select POId, SUM(Isnull(SalesReturnAmount,0)) as SalesReturnAmount From SalesReturnMasterTable WHERE POId <> 0 AND dbo.SalesReturnMasterTable.CustomerCode=" & IIf(Me.cmbAccounts.Value Is Nothing, 0, Me.cmbAccounts.Value) & "" & _
                      " Group By POId) SalesReturn " & _
                      " LEFT OUTER JOIN(Select a.POId, SUM(((Qty*Price)*Tax_Percent)/100) as Tax From SalesReturnDetailTable b INNER JOIN SalesReturnMasterTable a on a.SalesReturnId = b.SalesReturnId  " & _
                      " Group By a.POId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.POId = SalesReturn.POId) Ret On Ret.POId  = SalesMasterTable.SalesId " & _
                      " WHERE ((SalesMasterTable.SalesAmount+ISNULL(SalesTax.Tax,0)) - ISNULL(Receipts.ReceivedAmount, 0) <> 0) " & IIf(Me.DtpFillDate.Checked = True, "AND (Convert(varchar,SalesMasterTable.SalesDate,102) <=  Convert(Datetime, '" & Me.DtpFillDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))", "") & "" ' AND SalesMasterTable.SalesId Not In (Select InvoiceId From InvoiceAdjustmentTable WHERE InvoiceType='Sales')"
                'End Task:2823
                str = str & IIf(Me.cmbAccounts.Value > 0, " AND(SalesMasterTable.CustomerCode=" & Me.cmbAccounts.Value & ")", "") & ") a  LEFT OUTER JOIN(SELECT InvoiceId,coa_detail_Id, SUM(ISNULL(AdjustmentAmount, 0)) AS Adj FROM  dbo.InvoiceAdjustmentTable WHERE InvoiceType='Sales' GROUP BY InvoiceId,coa_detail_Id) AdjInv On AdjInv.InvoiceID = a.SalesId  WHERE (IsNull(a.InvoiceBalance,0)-IsNull(AdjInv.Adj,0)) <> 0"
                'End Task:2539
                'End Task:2534
                'End Task:2370
                FillUltraDropDown(Me.cmbInvoiceList, str)
                'Task:2539 Set Hidden Column
                'If Me.cmbInvoiceList.DisplayLayout.Bands.Count > 0 Then
                '    Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'End If
                'End Task:2539

            ElseIf Condition = "CostCenter" Then
                Dim Str As String = "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") " _
                  & " Select IsNull(CostCenterID, 0) As CostCenterID , Name As [Cost Center] from tblDefCostCenter where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights  where UserID = " & LoginUserId & ") And IsNull(Active, 0) =1 ORDER BY 2 ASC " _
                  & " Else " _
                  & " Select IsNull(CostCenterID, 0) As CostCenterID, Name As [Cost Center] from tblDefCostCenter Where IsNull(Active, 0) = 1 ORDER BY 2 ASC "
                'Dim str As String = "Select IsNull(CostCenterID,0) As CostCenterID, Name as [Cost Center] From tblDefCostCenter"
                FillDropDown(Me.cmbCostCenter, Str, True)
                ''Task:2370 Fill DropDown COA on Grid

                'Task 3198 Add CostCenter Combo in Grid'

            ElseIf Condition = "CostCenterGrid" Then

                Dim Str As String = "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") " _
                  & " Select IsNull(CostCenterID, 0) As CostCenterID , Name As CostCenter from tblDefCostCenter where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights  where UserID = " & LoginUserId & ") And IsNull(Active, 0) =1 Union Select 0,'... Select Any Value ...' ORDER BY 2 ASC " _
                  & " Else " _
                  & " Select IsNull(CostCenterID, 0) As CostCenterID, Name As CostCenter from tblDefCostCenter Where IsNull(Active, 0) = 1 Union Select 0,'... Select Any Value ...' ORDER BY 2 ASC"

                Dim dtAc As New DataTable
                dtAc = GetDataTable(Str)
                Me.GrdReceiptDetail.RootTable.Columns("CostCenter").ValueList.PopulateValueList(dtAc.DefaultView, "CostCenterID", "CostCenter")

            ElseIf Condition = "TaxAccounts" Then
                Dim strSQL = String.Empty
                strSQL = "Select coa_detail_id, detail_title from vwCOADetail  WHERE Account_Type <> 'Inventory' AND detail_title <> '' Union Select 0,'... Select Any Value ...'"
                Dim dtAc As New DataTable
                dtAc = GetDataTable(strSQL)
                Me.GrdReceiptDetail.RootTable.Columns("OtherTaxAccountId").ValueList.PopulateValueList(dtAc.DefaultView, "coa_detail_id", "detail_title")
                'End Task:2370
                'Task#04082015 fill combo box with companies (Ahmad Sharif)
            ElseIf Condition = "Company" Then
                Dim strSQL As String = String.Empty
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
            Dim TaxReceiveableAcId As Integer
            'TaxReceiveableAcId = Val(getConfigValueByType("TaxreceiveableACid").ToString)
            TaxReceiveableAcId = Val(getConfigValueByType("taxpayableACid").ToString)
            'TaxReceiveableAcId = Val(getConfigValueByType("TaxReceiveableAcId").ToString)
            'Task No 2537 Update The Tag Value 
            Dim SaleTaxDeductionAcId As Integer = 0I
            SaleTaxDeductionAcId = Val(getConfigValueByType("SaleTaxDeductionAcId").ToString)
            Dim strVoucherNo As String = String.Empty
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                strVoucherNo = GetVoucherNo()
            Else
                strVoucherNo = Me.txtVoucherNo.Text
            End If


            Dim NetReceipt As Double
            ObjMod = New InvoicesBasedReceiptMaster 'With New Variable Invoice Based Receipt Master Class 
            With ObjMod
                .ReceiptID = MasterID
                .ReceiptNo = strVoucherNo 'Me.GetVoucherNo
                .ReceiptDate = Me.dtpDate.Value
                .Reference = Me.txtReference.Text.ToString.Replace("'", "''")
                .PaymentAccountId = Me.cmbPayFrom.SelectedValue
                .PaymentMethod = Me.cmbPaymentMethod.SelectedValue
                'Task#4082015 Fill model for CompanyName added  by Ahmad Sharif
                .CompanyName = Me.cmbCompany.SelectedValue
                'End Task#04082015
                'Task No 2537 Append One cODE Line For Assigning The Checked Value Of Check Box To Prperty
                .Post = Me.ChkPost.Checked
                .CustomerCode = Me.cmbAccounts.Value
                .ChequeNo = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text.Replace("'", "''") & "'", String.Empty)
                '.ChequeDate = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing) 'before task:2375
                .ChequeDate = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType
                .UserName = LoginUserName
                .RVNo = strVoucherNo 'Me.txtVoucherNo.Text
                .ProjectId = Me.cmbCostCenter.SelectedValue
                .UserID = LoginUserId
                .InvoiceBasedReceiptDetail = New List(Of InvoicesBasedReceiptDetail) 'There Is Add Property Of Invoice Base Receipt Detail Class List
                .VoucherMaster = New VouchersMaster 'New Voucher Master Class
                .VoucherMaster.VoucherDatail = New List(Of VouchersDetail)
                .VoucherMaster.LocationId = Me.cmbCompany.SelectedValue
                .VoucherMaster.VoucherCode = strVoucherNo 'Me.GetVoucherNo
                .VoucherMaster.FinancialYearId = "1"
                .VoucherMaster.VoucherTypeId = Me.cmbPaymentMethod.SelectedValue
                .VoucherMaster.VoucherMonth = Me.dtpDate.Value.Date.Month
                .VoucherMaster.VoucherNo = strVoucherNo 'Me.GetVoucherNo
                .VoucherMaster.VoucherDate = Me.dtpDate.Value
                .VoucherMaster.CoaDetailId = Me.cmbAccounts.Value
                .VoucherMaster.ChequeNo = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty)
                '.VoucherMaster.ChequeDate = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing) 'Before against task:2375
                .VoucherMaster.ChequeDate = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType
                'Task No 2537 Append One cODE Line For Assigning The Checked Value Of Check Box To Prperty
                .VoucherMaster.Post = Me.ChkPost.Checked
                .VoucherMaster.Source = Me.Name
                .VoucherMaster.References = Me.txtReference.Text
                .VoucherMaster.UserName = LoginUserName
                .VoucherMaster.Posted_UserName = LoginUserName
                .VoucherMaster.VNo = strVoucherNo 'Me.txtVoucherNo.Text
                .ActivityLog = New ActivityLog 'With New Variable Acitvity Log Class
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
                Dim InvoiceDetail As InvoicesBasedReceiptDetail 'Here Import Invoice Base Receipt Detail Model Class
                Dim VoucherDt As VouchersDetail 'Here Import Voucher Detail Model Class
                Dim GrdDt As DataTable
                GrdDt = CType(Me.GrdReceiptDetail.DataSource, DataTable)
                GrdDt.AcceptChanges()
                Dim Dr As DataRow
                Dr = GrdDt.NewRow
                Dim flgTotalAmountVoucher As Boolean = False

                If getConfigValueByType("TotalAmountWiseInvoiceBasedVoucher").ToString = "" Then
                    flgTotalAmountVoucher = False
                Else
                    flgTotalAmountVoucher = Convert.ToBoolean(getConfigValueByType("TotalAmountWiseInvoiceBasedVoucher").ToString)
                End If

                For Each Dr In GrdDt.Rows
                    InvoiceDetail = New InvoicesBasedReceiptDetail 'With New Variable Invoice Based Receipt Detail Class
                    With InvoiceDetail
                        .InvoiceId = Dr.Item(enmGrdReceiptDetail.SalesID).ToString
                        .InvoiceNo = Dr.Item(enmGrdReceiptDetail.SalesNo).ToString
                        .InvoiceDate = Dr.Item(enmGrdReceiptDetail.SalesDate).ToString
                        .Remarks = Dr.Item(enmGrdReceiptDetail.Remarks).ToString
                        .Gst_Percentage = Val(Dr.Item(enmGrdReceiptDetail.Gst_Percentage).ToString) 'Tax Receiveable Here... 
                        .InvoiceAmount = Val(Dr.Item(enmGrdReceiptDetail.SalesAmount).ToString) 'Invoice Amount Here... 
                        .ReceiptAmount = Val(Dr.Item(enmGrdReceiptDetail.ReceiptAmount).ToString) 'Receitp Amount From Customer Account Or Other Receiveables
                        .InvoiceBalance = Dr.Item(enmGrdReceiptDetail.BalanceAmount).ToString ' Due Balance of Customer Account 
                        NetReceipt = NetReceipt + Dr.Item(enmGrdReceiptDetail.ReceiptAmount).ToString 'Net Receipt Amount From Customer Account 
                        ' Task 3198 Add CostCenter in Receipt Detail
                        .CostCenter = Val(Dr.Item(enmGrdReceiptDetail.CostCenter).ToString)
                        'Task:2370 Set Value in SalesTaxAmount, OtherTaxAmount, OtherTaxAccountId
                        .SalesTaxAmount = Val(Dr.Item(enmGrdReceiptDetail.SalesTaxAmount).ToString)
                        .OtherTaxAmount = Val(Dr.Item(enmGrdReceiptDetail.OtherTaxAmount).ToString)
                        .OtherTaxAccountId = Val(Dr.Item(enmGrdReceiptDetail.OtherTaxAccountId).ToString)
                        'End Task:2370
                        .Description = Dr.Item(enmGrdReceiptDetail.Description).ToString 'Task:2470 Set Description Value
                    End With
                    .InvoiceBasedReceiptDetail.Add(InvoiceDetail) 'Here Add Row In Invoice Base Receipt Detail.....  
                    VoucherDt = New VouchersDetail
                    With VoucherDt
                        .LocationId = "1"
                        .CoaDetailId = Me.cmbPayFrom.SelectedValue
                        .Comments = Dr.Item(enmGrdReceiptDetail.Remarks).ToString & " " & IIf(Dr.Item(enmGrdReceiptDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdReceiptDetail.Description).ToString & "", "")
                        .DebitAmount = Val(Dr.Item(enmGrdReceiptDetail.ReceiptAmount)) - (Val(Dr.Item("Tax Amount") + Val(Dr.Item("SalesTaxAmount").ToString) + Val(Dr.Item("OtherTaxAmount").ToString))) 'Debit Amount Cash Or Bank
                        .CreditAmount = 0
                        ' Task 3198 Add CostCenter in Receipt Detail
                        .CostCenter = Val(Dr.Item(enmGrdReceiptDetail.CostCenter).ToString)
                        .SPReference = Dr.Item("Invoice No").ToString
                        '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Task:2381 Problem In Invoice Based Payment 
                        .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task :2381 Problem In Invoice Based Payment 
                        '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing) 'Before against Task:2375 
                        .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType
                        .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                    End With
                    '.VoucherDetail.Add(VoucherDt)
                    .VoucherMaster.VoucherDatail.Add(VoucherDt)
                    Dim VoucherDtCredit As VouchersDetail
                    VoucherDtCredit = New VouchersDetail
                    With VoucherDtCredit
                        .LocationId = "1"
                        .CoaDetailId = Me.cmbAccounts.Value
                        'Before against Task:2370 
                        '.Comments = Dr.Item(4).ToString
                        .Comments = Dr.Item(enmGrdReceiptDetail.Remarks).ToString & " " & IIf(Dr.Item(enmGrdReceiptDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdReceiptDetail.Description).ToString & "", "")
                        'End Task:2370
                        .DebitAmount = 0
                        .CreditAmount = Val(Dr.Item(enmGrdReceiptDetail.ReceiptAmount)) - (Val(Dr.Item("Tax Amount") + Val(Dr.Item("SalesTaxAmount").ToString) + Val(Dr.Item("OtherTaxAmount").ToString)))  '(Val(Dr.Item("Receipt Amount")) - Val(Dr.Item("Tax Amount").ToString)) 'Dr.Item(8).ToString 'Credit Amount From Account List                       
                        ' Task 3198 Add CostCenter in Receipt Detail
                        .CostCenter = Val(Dr.Item(enmGrdReceiptDetail.CostCenter).ToString)
                        .SPReference = Dr.Item("Invoice No").ToString
                        '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Task:2381 Problem In Invoice Based Payment 
                        .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task :2381 Problem In Invoice Based Payment 
                        '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing) 'Before against Task:2375 
                        .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType
                        .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                    End With
                    '.VoucherDetail.Add(VoucherDtCredit)
                    .VoucherMaster.VoucherDatail.Add(VoucherDtCredit)
                    'Tax Receiveable Will be Credit here ..... 

                    Dim VoucherDtTaxReceiveable As VouchersDetail
                    If Val(Dr.Item("Tax Amount").ToString) <> 0 Then
                        VoucherDtTaxReceiveable = New VouchersDetail
                        With VoucherDtTaxReceiveable
                            .LocationId = "1"
                            .CoaDetailId = TaxReceiveableAcId
                            'Comment angainst Task:2370
                            '.Comments = "Income Tax Deducted Against Invoice No." & Mid(Dr.Item(4).ToString, 29, 10)
                            .Comments = "WH Tax deduction to " & Me.cmbAccounts.Text & " by Invoice No." & Dr.Item("Invoice No").ToString & " @ " & Val(Dr.Item("Receipt Amount").ToString) & "/" & Val(Dr.Item("Tax Amount").ToString) & " " & IIf(Dr.Item(enmGrdReceiptDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdReceiptDetail.Description).ToString & "", "")
                            'End Task:2370
                            .DebitAmount = Val(Dr.Item("Tax Amount").ToString) 'Income Tax Amount From Account List                       
                            .CreditAmount = 0
                            .CostCenter = Val(Dr.Item(enmGrdReceiptDetail.CostCenter).ToString)
                            .SPReference = Dr.Item("Invoice No").ToString
                            '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Task:2381 Problem In Invoice Based Payment 
                            .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task :2381 Problem In Invoice Based Payment 
                            '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing) 'Before against Task:2375 
                            .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType
                            .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                        End With
                        '.VoucherDetail.Add(VoucherDtCredit)
                        .VoucherMaster.VoucherDatail.Add(VoucherDtTaxReceiveable)


                        VoucherDtTaxReceiveable = New VouchersDetail
                        With VoucherDtTaxReceiveable
                            .LocationId = "1"
                            .CoaDetailId = Me.cmbAccounts.Value
                            'Comment angainst Task:2370
                            '.Comments = "Income Tax Deducted Against Invoice No." & Mid(Dr.Item(4).ToString, 29, 10)
                            .Comments = "WH Tax deduction to " & Me.cmbAccounts.Text & " by Invoice No." & Dr.Item("Invoice No").ToString & " @ " & Val(Dr.Item("Receipt Amount").ToString) & "/" & Val(Dr.Item("Tax Amount").ToString) & " " & IIf(Dr.Item(enmGrdReceiptDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdReceiptDetail.Description).ToString & "", "")
                            'End Task:2370
                            .DebitAmount = 0 'Income Tax Amount From Account List                       
                            .CreditAmount = Val(Dr.Item("Tax Amount").ToString)
                            .CostCenter = Val(Dr.Item(enmGrdReceiptDetail.CostCenter).ToString)
                            .SPReference = Dr.Item("Invoice No").ToString
                            '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Task:2381 Problem In Invoice Based Payment 
                            .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task :2381 Problem In Invoice Based Payment 
                            '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing) 'Before against Task:2375 
                            .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType
                            .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                        End With
                        '.VoucherDetail.Add(VoucherDtCredit)
                        .VoucherMaster.VoucherDatail.Add(VoucherDtTaxReceiveable)

                    End If
                    If Val(Dr.Item("SalesTaxAmount").ToString) <> 0 Then
                        VoucherDtTaxReceiveable = New VouchersDetail
                        With VoucherDtTaxReceiveable
                            .LocationId = "1"
                            '.CoaDetailId = TaxReceiveableAcId
                            'Task 2537 Updating The Code Line Above By Updateing The Tag Name
                            If Val(SaleTaxDeductionAcId) = Val(0) Then
                                Throw New Exception("Please Select Sales Tax Deduction Account")
                            End If
                            .CoaDetailId = SaleTaxDeductionAcId
                            'Comment angainst Task:2370
                            '.Comments = "Income Tax Deducted Against Invoice No." & Mid(Dr.Item(4).ToString, 29, 10)
                            .Comments = "Sales Tax deduction to " & Me.cmbAccounts.Text & " by Invoice No." & Dr.Item("Invoice No").ToString & " " & IIf(Dr.Item(enmGrdReceiptDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdReceiptDetail.Description).ToString & "", "")
                            'End Task:2370
                            .DebitAmount = Dr.Item("SalesTaxAmount") 'Income Tax Amount From Account List                       
                            .CreditAmount = 0
                            .CostCenter = Val(Dr.Item(enmGrdReceiptDetail.CostCenter).ToString)
                            .SPReference = Dr.Item("Invoice No").ToString
                            '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Task:2381 Problem In Invoice Based Payment 
                            .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task :2381 Problem In Invoice Based Payment 
                            '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing) 'Before against Task:2375 
                            .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType
                            .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                        End With
                        '.VoucherDetail.Add(VoucherDtCredit)
                        .VoucherMaster.VoucherDatail.Add(VoucherDtTaxReceiveable)


                        VoucherDtTaxReceiveable = New VouchersDetail
                        With VoucherDtTaxReceiveable
                            .LocationId = "1"
                            '.CoaDetailId = TaxReceiveableAcId
                            'Task 2537 Updating The Code Line Above By Updateing The Tag Name
                            .CoaDetailId = Me.cmbAccounts.Value
                            'Comment angainst Task:2370
                            '.Comments = "Income Tax Deducted Against Invoice No." & Mid(Dr.Item(4).ToString, 29, 10)
                            .Comments = "Sales Tax deduction to " & Me.cmbAccounts.Text & " by Invoice No." & Dr.Item("Invoice No").ToString & " " & IIf(Dr.Item(enmGrdReceiptDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdReceiptDetail.Description).ToString & "", "")
                            'End Task:2370
                            .DebitAmount = 0
                            .CreditAmount = Dr.Item("SalesTaxAmount") 'Income Tax Amount From Account List       
                            .CostCenter = Val(Dr.Item(enmGrdReceiptDetail.CostCenter).ToString)
                            .SPReference = Dr.Item("Invoice No").ToString
                            '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Task:2381 Problem In Invoice Based Payment 
                            .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task :2381 Problem In Invoice Based Payment 
                            '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing) 'Before against Task:2375 
                            .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType
                            .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                        End With
                        '.VoucherDetail.Add(VoucherDtCredit)
                        .VoucherMaster.VoucherDatail.Add(VoucherDtTaxReceiveable)

                    End If
                    If Val(Dr.Item("OtherTaxAmount").ToString) <> 0 Then
                        VoucherDtTaxReceiveable = New VouchersDetail
                        With VoucherDtTaxReceiveable
                            .LocationId = "1"
                            .CoaDetailId = Val(Dr.Item(enmGrdReceiptDetail.OtherTaxAccountId).ToString)
                            'Comment angainst Task:2370
                            '.Comments = "Income Tax Deducted Against Invoice No." & Mid(Dr.Item(4).ToString, 29, 10)
                            .Comments = "Other Tax deduction to " & Me.cmbAccounts.Text & " by Invoice No." & Dr.Item("Invoice No").ToString & " " & IIf(Dr.Item(enmGrdReceiptDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdReceiptDetail.Description).ToString & "", "")
                            'End Task:2370
                            .DebitAmount = Dr.Item("OtherTaxAmount") 'Income Tax Amount From Account List                       
                            .CreditAmount = 0
                            .CostCenter = Val(Dr.Item(enmGrdReceiptDetail.CostCenter).ToString)
                            .SPReference = Dr.Item("Invoice No").ToString
                            '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Task:2381 Problem In Invoice Based Payment 
                            .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task :2381 Problem In Invoice Based Payment 
                            '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing) 'Before against Task:2375 
                            .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType
                            .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                        End With
                        '.VoucherDetail.Add(VoucherDtCredit)
                        .VoucherMaster.VoucherDatail.Add(VoucherDtTaxReceiveable)



                        VoucherDtTaxReceiveable = New VouchersDetail
                        With VoucherDtTaxReceiveable
                            .LocationId = "1"
                            .CoaDetailId = Me.cmbAccounts.Value 'Val(Dr.Item(enmGrdReceiptDetail.OtherTaxAccountId).ToString)
                            'Comment angainst Task:2370
                            '.Comments = "Income Tax Deducted Against Invoice No." & Mid(Dr.Item(4).ToString, 29, 10)
                            .Comments = "Other Tax deduction to " & Me.cmbAccounts.Text & " by Invoice No." & Dr.Item("Invoice No").ToString & " " & IIf(Dr.Item(enmGrdReceiptDetail.Description).ToString.Length > 0, "," & Dr.Item(enmGrdReceiptDetail.Description).ToString & "", "")
                            'End Task:2370
                            .DebitAmount = 0
                            .CreditAmount = Dr.Item("OtherTaxAmount") 'Income Tax Amount From Account List      
                            .CostCenter = Val(Dr.Item(enmGrdReceiptDetail.CostCenter).ToString)
                            .SPReference = Dr.Item("Invoice No").ToString
                            '.Cheque_No = IIf(Me.txtChequeNo.Visible = True, "'" & Me.txtChequeNo.Text & "'", String.Empty) 'Task:2381 Problem In Invoice Based Payment 
                            .Cheque_No = IIf(Me.txtChequeNo.Visible = True, Me.txtChequeNo.Text, String.Empty) 'Task :2381 Problem In Invoice Based Payment 
                            '.Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, "'" & Me.DtpChequeDate.Value & "'", Nothing) 'Before against Task:2375 
                            .Cheque_Date = IIf(Me.DtpChequeDate.Visible = True, Me.DtpChequeDate.Value, Nothing) 'Task:2375 Change DataType
                            .ChequeDescription = GetComments(Dr).Replace("'", "''") 'Task:2745 Set Cheque Comments
                        End With
                        '.VoucherDetail.Add(VoucherDtCredit)
                        .VoucherMaster.VoucherDatail.Add(VoucherDtTaxReceiveable)

                    End If
                Next 'Here Move Row With Data
                .NetReceipt = NetReceipt
            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            '26-4-2014 TASK:M35 Added Condition For All Record
            Dim dt As New DataTable
            dt = New InvoicesBasedReceiptDAL().GetAllRecord(Condition) 'Get DataResource From Invoice Base Receipt DAL
            Me.GrdSaved.DataSource = dt
            Me.GrdSaved.RetrieveStructure()
            'end Task:M35
            Me.GrdSaved.RootTable.Columns("ReceiptDate").FormatString = str_DisplayDateFormat
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
                ShowErrorMessage("Please Select Deposit Account")
                Return False
                'If VerifyData = (Me.cmbPayFrom.SelectedIndex) Then
                '    Me.cmbPayFrom.Focus()
                '    ShowErrorMessage("Please Select Deposit Account")
                '    Return False
            ElseIf VerifyData = (Me.cmbAccounts.Value) Then
                Me.cmbAccounts.Focus()
                ShowErrorMessage("Please Select Receiveable Account")
                Return False
            ElseIf VerifyData = (Me.GrdReceiptDetail.RowCount) Then
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
        Me.txtReceivedAmt.Text = 0
        Me.txtDueAmount.Text = 0
        Me.txtRemarks.Text = ""
        Me.txtVoucherNo.Text = ""
        Me.dtpDate.Value = DateTime.Now
        Me.txtVoucherNo.Text = GetVoucherNo()
        Me.txtReference.Text = ""
        Me.txtInvoicoAmount.Text = ""
        Me.txtReceivedAmt.Text = ""
        Me.txtDueAmount.Text = ""
        If Me.cmbPaymentMethod.SelectedIndex = 0 Then
            Me.lblChequeNo.Visible = False
            Me.lblChequeDate.Visible = False
            Me.txtChequeNo.Visible = False
            Me.DtpChequeDate.Visible = False
        End If
        Me.cmbPaymentMethod.Enabled = True
        Me.cmbPaymentMethod.SelectedIndex = 0
        Me.cmbPaymentMethod.Show()
        Me.cmbPayFrom.SelectedIndex = 0
        Me.txtCurrentBalance.Text = "" ''TFS4653 : Ayesha Rehman : 28-09-2018
        Me.txtChequeNo.Text = ""
        Me.DtpChequeDate.Checked = False
        Me.cmbAccounts.Rows(0).Activate()
        FillCombos("Invoice")
        Me.cmbInvoiceList.Rows(0).Activate()
        Me.cmbCostCenter.SelectedIndex = 0
        Me.cmbAccounts.Enabled = True
        Me.cmbCostCenter.Enabled = True
        Me.DisplayRecord(-1)
        ' Me.GetAllRecords()
        FillComboByEdit()
        Me.lblPrintStatus.Text = String.Empty
        Me.txtSalesTaxAmount.Text = String.Empty
        Me.dtpDate.Enabled = True
        ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
        Me.BtnEdit.Visible = False
        Me.BtnDelete.Visible = False
        Me.BtnPrint.Visible = False
        Me.cmbCompany.SelectedValue = 1
        '''''''''''''''''''''''''''
        btnUpdateTimes.Text = "No of update times"
        Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        GetSecurityRights(Utility.EnumDataMode.[New]) ''13-Aug-2014 Task:2782 Imran Ali Post Rights Implement On Invoice Base Payment\Receipt Voucher

    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New InvoicesBasedReceiptDAL().Add(ObjMod) Then Return True
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


            If New InvoicesBasedReceiptDAL().Update(ObjMod) Then Return True
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
            FillCombos("Invoice")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        Try
            If GrdValidate() Then
                GridAddITems()
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
            Me.txtReceivedAmt.Text = Me.cmbInvoiceList.SelectedRow.Cells("InvoiceBalance").Value.ToString
            Me.txtDueAmount.Text = 0
            Me.txtInvoicoAmount.Text = Me.cmbInvoiceList.SelectedRow.Cells("InvoiceBalance").Value.ToString
            Me.txtRemarks.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtReceivedAmt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtReceivedAmt.Leave
        Try

            If Val(Me.txtInvoicoAmount.Text) > 0 Then
                Me.txtDueAmount.Text = (Val(Me.txtInvoicoAmount.Text) - Val(Me.txtReceivedAmt.Text))
                Me.BtnAdd.Enabled = True
                If Val(Me.txtDueAmount.Text) < 0 Then
                    If msg_Confirm("due amount grater then invoice amount, do you want to add") = True Then
                        Me.BtnAdd.Enabled = True
                    Else
                        Me.BtnAdd.Enabled = False
                        Me.txtReceivedAmt.Text = "0"
                        Me.txtReceivedAmt.Focus()
                        Exit Sub
                    End If
                End If
            End If

            getNetAmount()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GrdValidate()
        Try
            If Me.cmbAccounts.SelectedRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please Select Customer")
                Me.cmbAccounts.Focus()
                Return False
            End If
            If Not Val(Me.txtReceivedAmt.Text) > 0 Then 'When Receipt Amount is Zero 
                ShowErrorMessage("Please Enter Receipt Amount")
                Me.txtReceivedAmt.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetGrd()
        Me.txtRemarks.Text = ""
        Me.txtInvoicoAmount.Text = "0"
        Me.txtReceivedAmt.Text = "0"
        Me.txtDueAmount.Text = "0"
        Me.txtWHTax.Text = String.Empty
        Me.txtSalesTaxAmount.Text = String.Empty
        Me.txtOtherTaxAmount.Text = String.Empty
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
            '    If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpDate.Value.ToString("yyyy-M-d 00:00:00"))) Then
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
            Me.GrdReceiptDetail.UpdateData()

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.GrdReceiptDetail.GetRows
                If Val(r.Cells(enmGrdReceiptDetail.OtherTaxAmount).Value.ToString) > 0 Then
                    If Val(r.Cells(enmGrdReceiptDetail.OtherTaxAccountId).Value.ToString) = 0 Then
                        ShowErrorMessage("Please select other tax account.")
                        Exit Sub
                    End If
                End If
            Next
            'If Not msg_Confirm(str_ConfirmSave) Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then

                If Save() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
                'MsgBox("Record Successfully Saved", MsgBoxStyle.Information, str_MessageHeader)
                'msg_Information(str_informSave)

                If Me.ChkPost.Checked = True Then
                    If EnabledBrandedSMS = True Then
                        If GetSMSConfig("Invoice Receipt").Enable = True Then
                            If (Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString <> "" Or Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString.Length >= 10) Then
                                Try
                                    Dim strMSGBody As String = String.Empty
                                    Dim objSMSTemp As New SMSTemplateParameter
                                    If Me.cmbPaymentMethod.Text = "Bank" Then
                                        objSMSTemp = GetSMSTemplate("Invoice Based Bank Receipt")
                                    Else
                                        objSMSTemp = GetSMSTemplate("Invoice Based Cash Receipt")
                                    End If
                                    If objSMSTemp IsNot Nothing Then
                                        Dim objSMSParam As New SMSParameters
                                        objSMSParam.AccountCode = Me.cmbAccounts.ActiveRow.Cells("detail_code").Value.ToString
                                        objSMSParam.AccountTitle = Me.cmbAccounts.ActiveRow.Cells("detail_title").Value.ToString
                                        objSMSParam.DocumentNo = Me.txtVoucherNo.Text
                                        objSMSParam.DocumentDate = Me.dtpDate.Value
                                        objSMSParam.Remarks = Me.txtRemarks.Text
                                        objSMSParam.CellNo = Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString
                                        objSMSParam.Amount = Math.Round(Me.GrdReceiptDetail.GetTotal(Me.GrdReceiptDetail.RootTable.Columns("Net Amount"), AggregateFunction.Sum), 0)
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
                If Not msg_Confirm(str_ConfirmUpdate) Then Exit Sub
                If Update1() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
                'MsgBox("Record Successfully Update", MsgBoxStyle.Information, str_MessageHeader)
                'msg_Information(str_informSave)



                If Me.ChkPost.Checked = True Then
                    If EnabledBrandedSMS = True Then
                        If GetSMSConfig("Invoice Receipt").Enable = True Then
                            If (Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString <> "" Or Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString.Length >= 10) Then
                                Try
                                    Dim strMSGBody As String = String.Empty
                                    Dim objSMSTemp As New SMSTemplateParameter
                                    If Me.cmbPaymentMethod.Text = "Bank" Then
                                        objSMSTemp = GetSMSTemplate("Bank Invoice Based Receipt")
                                    Else
                                        objSMSTemp = GetSMSTemplate("Cash Invoice Based Receipt")
                                    End If
                                    If objSMSTemp IsNot Nothing Then
                                        Dim objSMSParam As New SMSParameters
                                        objSMSParam.AccountCode = Me.cmbAccounts.ActiveRow.Cells("detail_code").Value.ToString
                                        objSMSParam.AccountTitle = Me.cmbAccounts.ActiveRow.Cells("detail_title").Value.ToString
                                        objSMSParam.DocumentNo = Me.txtVoucherNo.Text
                                        objSMSParam.DocumentDate = Me.dtpDate.Value
                                        objSMSParam.Remarks = Me.txtRemarks.Text
                                        objSMSParam.CellNo = Me.cmbAccounts.ActiveRow.Cells("Mobile").Value.ToString
                                        objSMSParam.Amount = Math.Round(Me.GrdReceiptDetail.GetTotal(Me.GrdReceiptDetail.RootTable.Columns("Net Amount"), AggregateFunction.Sum), 0)
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

            Me.cmbAccounts.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.Dropdown)
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
    Private Sub DisplayRecord(ByVal ReceiptId As Integer)
        Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
        Dim dt As New DataTable
        'Dim strsql = " SELECT dbo.InvoiceBasedReceiptsDetails.ReceiptDetailId, dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo as [Invoice No], dbo.SalesMasterTable.SalesDate as [Invoice Date], " _
        '            & " InvoiceBasedReceiptsDetails.Comments as Remarks, IsNull(InvoiceBasedReceiptsDetails.Gst_Percentage,0) as [Tax], ((dbo.InvoiceBasedReceiptsDetails.ReceiptAmount*IsNull(InvoiceBasedReceiptsDetails.Gst_Percentage,0))/100) as [Tax Amount], (dbo.SalesMasterTable.SalesAmount+ISNULL(SalesTax.SalesTax,0)) as [Invoice Amount], dbo.InvoiceBasedReceiptsDetails.ReceiptAmount as [Receipt Amount], " _
        '            & " (dbo.SalesMasterTable.SalesAmount+ISNULL(SalesTax.SalesTax,0)) - dbo.InvoiceBasedReceiptsDetails.ReceiptAmount as [Due Amount] " _
        '            & " FROM dbo.InvoiceBasedReceiptsDetails RIGHT OUTER JOIN " _
        '            & " dbo.SalesMasterTable ON dbo.InvoiceBasedReceiptsDetails.InvoiceId = dbo.SalesMasterTable.SalesId LEFT OUTER JOIN(Select SalesId, SUM(((Qty*Price)*TaxPercent)/100) as SalesTax From SalesDetailTable Group by SalesId ) SalesTax On SalesTax.SalesId = SalesMasterTable.SalesId " _
        '            & " where InvoiceBasedReceiptsDetails.ReceiptId=" & ReceiptId
        'Before against task:2470
        'Dim strsql = " SELECT dbo.InvoiceBasedReceiptsDetails.ReceiptDetailId,   dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo as [Invoice No],    dbo.SalesMasterTable.SalesDate as [Invoice Date],  " _
        '            & " InvoiceBasedReceiptsDetails.Comments as Remarks, IsNull(InvoiceBasedReceiptsDetails.Gst_Percentage,0) as [Tax], ((dbo.InvoiceBasedReceiptsDetails.ReceiptAmount*IsNull(InvoiceBasedReceiptsDetails.Gst_Percentage,0))/100) as [Tax Amount],     ISNULL(InvoiceBasedReceiptsDetails.SalesTaxAmount,0) as SalesTaxAmount,      Isnull(InvoiceBasedReceiptsDetails.OtherTaxAmount,0) as OtherTaxAmount,  Isnull(InvoiceBasedReceiptsDetails.OtherTaxAccountId,0) as OtherTaxAccountId,     (Isnull(dbo.SalesMasterTable.SalesAmount,0)+ISNULL(tblSalesTax.SalesTax,0)) as [Invoice Amount],      IsNull(dbo.InvoiceBasedReceiptsDetails.ReceiptAmount,0) as [Receipt Amount], " _
        '            & " (Isnull(dbo.SalesMasterTable.SalesAmount,0)+ISNULL(tblSalesTax.SalesTax,0)) - dbo.InvoiceBasedReceiptsDetails.ReceiptAmount as [Due Amount]    " _
        '            & " FROM dbo.InvoiceBasedReceiptsDetails RIGHT OUTER JOIN  " _
        '            & " dbo.SalesMasterTable ON dbo.InvoiceBasedReceiptsDetails.InvoiceId = dbo.SalesMasterTable.SalesId  LEFT OUTER JOIN(Select SalesId, SUM(((Isnull(Qty,0)*Isnull(Price,0))*Isnull(TaxPercent,0))/100) as SalesTax From SalesDetailTable Group by SalesId HAVING SUM(((Isnull(Qty,0)*Isnull(Price,0))*Isnull(TaxPercent,0))/100) <> 0 ) tblSalesTax On tblSalesTax.SalesId = dbo.SalesMasterTable.SalesId " _
        '            & " where InvoiceBasedReceiptsDetails.ReceiptId=" & ReceiptId
        'Task:2470 Added Column Description
        'Before against task:2539
        'Dim strsql = " SELECT dbo.InvoiceBasedReceiptsDetails.ReceiptDetailId,   dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo as [Invoice No],    dbo.SalesMasterTable.SalesDate as [Invoice Date],  " _
        '           & " InvoiceBasedReceiptsDetails.Comments as Remarks, IsNull(InvoiceBasedReceiptsDetails.Gst_Percentage,0) as [Tax], ((dbo.InvoiceBasedReceiptsDetails.ReceiptAmount*IsNull(InvoiceBasedReceiptsDetails.Gst_Percentage,0))/100) as [Tax Amount],     ISNULL(InvoiceBasedReceiptsDetails.SalesTaxAmount,0) as SalesTaxAmount,      Isnull(InvoiceBasedReceiptsDetails.OtherTaxAmount,0) as OtherTaxAmount,  Isnull(InvoiceBasedReceiptsDetails.OtherTaxAccountId,0) as OtherTaxAccountId,     (Isnull(dbo.SalesMasterTable.SalesAmount,0)+ISNULL(tblSalesTax.SalesTax,0)) as [Invoice Amount],      IsNull(dbo.InvoiceBasedReceiptsDetails.ReceiptAmount,0) as [Receipt Amount], " _
        '           & " (Isnull(dbo.SalesMasterTable.SalesAmount,0)+ISNULL(tblSalesTax.SalesTax,0)) - dbo.InvoiceBasedReceiptsDetails.ReceiptAmount as [Due Amount],dbo.InvoiceBasedReceiptsDetails.Description     " _
        '           & " FROM dbo.InvoiceBasedReceiptsDetails RIGHT OUTER JOIN  " _
        '           & " dbo.SalesMasterTable ON dbo.InvoiceBasedReceiptsDetails.InvoiceId = dbo.SalesMasterTable.SalesId  LEFT OUTER JOIN(Select SalesId, SUM(((Isnull(Qty,0)*Isnull(Price,0))*Isnull(TaxPercent,0))/100) as SalesTax From SalesDetailTable Group by SalesId HAVING SUM(((Isnull(Qty,0)*Isnull(Price,0))*Isnull(TaxPercent,0))/100) <> 0 ) tblSalesTax On tblSalesTax.SalesId = dbo.SalesMasterTable.SalesId "
        '           & " where InvoiceBasedReceiptsDetails.ReceiptId=" & ReceiptId
        'End Task:2470
        'Task:2539 Added SubQuery For Debit Amount 
        'Dim strsql = " SELECT dbo.InvoiceBasedReceiptsDetails.ReceiptDetailId,   dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo as [Invoice No],    dbo.SalesMasterTable.SalesDate as [Invoice Date],  " _
        '   & " InvoiceBasedReceiptsDetails.Comments as Remarks, IsNull(InvoiceBasedReceiptsDetails.Gst_Percentage,0) as [Tax], ((dbo.InvoiceBasedReceiptsDetails.ReceiptAmount*IsNull(InvoiceBasedReceiptsDetails.Gst_Percentage,0))/100) as [Tax Amount],     ISNULL(InvoiceBasedReceiptsDetails.SalesTaxAmount,0) as SalesTaxAmount,      Isnull(InvoiceBasedReceiptsDetails.OtherTaxAmount,0) as OtherTaxAmount,  Isnull(InvoiceBasedReceiptsDetails.OtherTaxAccountId,0) as OtherTaxAccountId,     (Isnull(dbo.SalesMasterTable.SalesAmount,0)+ISNULL(tblSalesTax.SalesTax,0)) as [Invoice Amount],      IsNull(dbo.InvoiceBasedReceiptsDetails.ReceiptAmount,0) as [Receipt Amount], " _
        '   & " ((Isnull(dbo.SalesMasterTable.SalesAmount,0)+ISNULL(tblSalesTax.SalesTax,0))-IsNull(Ret.SalesReturnAmount,0)) - dbo.InvoiceBasedReceiptsDetails.ReceiptAmount as [Due Amount],dbo.InvoiceBasedReceiptsDetails.Description     " _
        '   & " FROM dbo.InvoiceBasedReceiptsDetails RIGHT OUTER JOIN  " _
        '   & " dbo.SalesMasterTable ON dbo.InvoiceBasedReceiptsDetails.InvoiceId = dbo.SalesMasterTable.SalesId  LEFT OUTER JOIN(Select SalesId, SUM(((Isnull(Qty,0)*Isnull(Price,0))*Isnull(TaxPercent,0))/100) as SalesTax From SalesDetailTable Group by SalesId HAVING SUM(((Isnull(Qty,0)*Isnull(Price,0))*Isnull(TaxPercent,0))/100) <> 0 ) tblSalesTax On tblSalesTax.SalesId = dbo.SalesMasterTable.SalesId " _
        '   & " LEFT OUTER JOIN(Select SalesReturn.POId, (Isnull(SalesReturn.SalesReturnAmount,0)+Isnull(Tax,0)) as SalesReturnAmount From(Select POId, SUM(Isnull(SalesReturnAmount,0)) as SalesReturnAmount From SalesReturnMasterTable WHERE POId <> 0 AND dbo.SalesReturnMasterTable.CustomerCode=" & IIf(Me.cmbAccounts.Value Is Nothing, 0, Me.cmbAccounts.Value) & "" _
        '   & " Group By POId) SalesReturn " _
        '   & " LEFT OUTER JOIN(Select a.POId, SUM(((Qty*Price)*Tax_Percent)/100) as Tax From SalesReturnDetailTable b INNER JOIN SalesReturnMasterTable a on a.SalesReturnId = b.SalesReturnId  " _
        '   & " Group By a.POId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.POId = SalesReturn.POId) Ret On Ret.POId  = dbo.SalesMasterTable.SalesId " _
        '   & " where InvoiceBasedReceiptsDetails.ReceiptId=" & ReceiptId
        'End Task:2539
        'Task:27775 Added Field Deposit Amount
        Dim strsql = " SELECT dbo.InvoiceBasedReceiptsDetails.ReceiptDetailId,   dbo.SalesMasterTable.SalesId, dbo.SalesMasterTable.SalesNo as [Invoice No],    dbo.SalesMasterTable.SalesDate as [Invoice Date],  " _
          & " InvoiceBasedReceiptsDetails.Comments as Remarks, IsNull(InvoiceBasedReceiptsDetails.Gst_Percentage,0) as [Tax], ((dbo.InvoiceBasedReceiptsDetails.ReceiptAmount*IsNull(InvoiceBasedReceiptsDetails.Gst_Percentage,0))/100) as [Tax Amount],     ISNULL(InvoiceBasedReceiptsDetails.SalesTaxAmount,0) as SalesTaxAmount,      Isnull(InvoiceBasedReceiptsDetails.OtherTaxAmount,0) as OtherTaxAmount, Isnull(InvoiceBasedReceiptsDetails.OtherTaxAccountId,0) as OtherTaxAccountId,Convert(float,0) as [Net Amount], (Isnull(dbo.SalesMasterTable.SalesAmount,0)+ISNULL(tblSalesTax.SalesTax,0)) as [Invoice Amount],IsNull(dbo.InvoiceBasedReceiptsDetails.ReceiptAmount,0) as [Receipt Amount], " _
          & " ((Isnull(dbo.SalesMasterTable.SalesAmount,0)+ISNULL(tblSalesTax.SalesTax,0))-IsNull(Ret.SalesReturnAmount,0)) - dbo.InvoiceBasedReceiptsDetails.ReceiptAmount as [Due Amount],dbo.InvoiceBasedReceiptsDetails.Description  , Isnull(InvoiceBasedReceiptsDetails.CostCenterId, 0) As CostCenter   " _
          & " FROM dbo.InvoiceBasedReceiptsDetails RIGHT OUTER JOIN  " _
          & " dbo.SalesMasterTable ON dbo.InvoiceBasedReceiptsDetails.InvoiceId = dbo.SalesMasterTable.SalesId  LEFT OUTER JOIN(Select SalesId, SUM(((Isnull(Qty,0)*Isnull(Price,0))*Isnull(TaxPercent,0))/100) as SalesTax From SalesDetailTable Group by SalesId HAVING SUM(((Isnull(Qty,0)*Isnull(Price,0))*Isnull(TaxPercent,0))/100) <> 0 ) tblSalesTax On tblSalesTax.SalesId = dbo.SalesMasterTable.SalesId " _
          & " LEFT OUTER JOIN(Select SalesReturn.POId, (Isnull(SalesReturn.SalesReturnAmount,0)+Isnull(Tax,0)) as SalesReturnAmount From(Select POId, SUM(Isnull(SalesReturnAmount,0)) as SalesReturnAmount From SalesReturnMasterTable WHERE POId <> 0 AND dbo.SalesReturnMasterTable.CustomerCode=" & IIf(Me.cmbAccounts.Value Is Nothing, 0, Me.cmbAccounts.Value) & "" _
          & " Group By POId) SalesReturn " _
          & " LEFT OUTER JOIN(Select a.POId, SUM(((Qty*Price)*Tax_Percent)/100) as Tax From SalesReturnDetailTable b INNER JOIN SalesReturnMasterTable a on a.SalesReturnId = b.SalesReturnId  " _
          & " Group By a.POId Having SUM(((Qty*Price)*Tax_Percent)/100)<> 0) RetTax On RetTax.POId = SalesReturn.POId) Ret On Ret.POId  = dbo.SalesMasterTable.SalesId " _
          & " where InvoiceBasedReceiptsDetails.ReceiptId=" & ReceiptId
        'End Task:2775

        dt = GetDataTable(strsql)
        Me.GrdReceiptDetail.DataSource = dt
        Me.GrdReceiptDetail.RetrieveStructure()
        'Task:2370 Call Tax Account DropDown Method
        Me.GrdReceiptDetail.RootTable.Columns("OtherTaxAccountId").EditType = EditType.Combo
        Me.GrdReceiptDetail.RootTable.Columns("OtherTaxAccountId").HasValueList = True

        'Task:3198 Call CostCenter DropDown Method
        Me.GrdReceiptDetail.RootTable.Columns("CostCenter").EditType = EditType.Combo
        Me.GrdReceiptDetail.RootTable.Columns("CostCenter").HasValueList = True

        'Task:2775 Added Delete Button Field
        Me.GrdReceiptDetail.RootTable.Columns.Add("Column1")
        Me.GrdReceiptDetail.RootTable.Columns("Column1").ButtonDisplayMode = CellButtonDisplayMode.Always
        Me.GrdReceiptDetail.RootTable.Columns("Column1").ButtonStyle = ButtonStyle.ButtonCell
        Me.GrdReceiptDetail.RootTable.Columns("Column1").ButtonText = "Delete"
        Me.GrdReceiptDetail.RootTable.Columns("Column1").TextAlignment = TextAlignment.Center
        Me.GrdReceiptDetail.RootTable.Columns("Column1").HeaderAlignment = TextAlignment.Center
        Me.GrdReceiptDetail.RootTable.Columns("Column1").Caption = "Action"
        'End Task

        FillCombos("TaxAccounts")

        'Task 3198
        FillCombos("CostCenterGrid")

        'End Task:2370

        dt.Columns("Net Amount").Expression = "([Receipt Amount]-([Tax Amount]+[OtherTaxAmount]+[SalesTaxAmount]))" 'Task:2775 Net Amount Calc

        If flgInvoiceWiseTaxPercent = True Then

            'Task:3197 Amount Calc

            'dt.Columns(enmGrdReceiptDetail.Gst_Amount).Expression = "(([Tax]*[Receipt Amount])/100)"

            dt.Columns(enmGrdReceiptDetail.Gst_Amount).Expression = "(([Tax]*([Receipt Amount]-([OtherTaxAmount]+[SalesTaxAmount])))/100)"

        Else
            'Task:3197 Amount Calc

            'dt.Columns(enmGrdReceiptDetail.Gst_Percentage).Expression = "(iif([Tax Amount]=0,0,([Tax Amount]/[Receipt Amount])*100))"

            dt.Columns(enmGrdReceiptDetail.Gst_Percentage).Expression = "(iif([Tax Amount]=0,0,([Tax Amount]/([Receipt Amount]-([Tax Amount]+[OtherTaxAmount]+[SalesTaxAmount])))*100))"

        End If
        'dt.Columns(enmGrdReceiptDetail.BalanceAmount).Expression = "(([Invoice Amount] - [Receipt Amount]))"

        dt.Columns(enmGrdReceiptDetail.BalanceAmount).Expression = "(([Invoice Amount] - ([Receipt Amount]-([Tax Amount]+[OtherTaxAmount]+[SalesTaxAmount]))))"

        ApplyGridSettings()
    End Sub
    Public Function GetVoucherNo() As String

        Dim VType As String = String.Empty
        If Me.cmbPaymentMethod.SelectedIndex > 0 Then
            'Task#04082015 Concatenate Company location id with prefix of invoice number if configuration on of company wise prefix
            VType = "BRV" & IIf(getConfigValueByType("CompanyWisePrefix").ToString = "True", Me.cmbCompany.SelectedValue, String.Empty)
        Else
            VType = "CRV" & IIf(getConfigValueByType("CompanyWisePrefix").ToString = "True", Me.cmbCompany.SelectedValue, String.Empty)
            'End Task#04082015
        End If
        If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
            Return GetSerialNo(VType + "-" + Microsoft.VisualBasic.Right(Me.dtpDate.Value.Year, 2) + "-", "InvoiceBasedReceipts", "ReceiptNo")
        Else
            Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
            Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
            If Not dr Is Nothing Then
                If dr("config_Value") = "Monthly" Then
                    Return GetNextDocNo(VType & "-" & Format(Me.dtpDate.Value, "yy") & Me.dtpDate.Value.Month.ToString("00"), 4, "InvoiceBasedReceipts", "ReceiptNo")
                Else
                    Return GetNextDocNo(VType, 6, "InvoiceBasedReceipts", "ReceiptNo")
                End If
            Else
                Return GetNextDocNo(VType, 6, "InvoiceBasedReceipts", "ReceiptNo")
            End If
            Return ""
        End If
    End Function
    Private Sub cmbPaymentMethod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPaymentMethod.SelectedIndexChanged
        Try
            If EditMode = False Then
                If Me.cmbPaymentMethod.SelectedIndex > 0 Then
                    Me.txtVoucherNo.Text = Me.GetVoucherNo
                Else
                    Me.txtVoucherNo.Text = Me.GetVoucherNo
                End If
            Else
                EditMode = True
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
        'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()

        Me.Cursor = Cursors.WaitCursor
        Dim Id As Integer
        Id = 0

        Id = Me.cmbAccounts.Value
        FillCombos("Customer")
        Me.cmbAccounts.Value = Id

        Id = Me.cmbPaymentMethod.SelectedValue
        FillCombos("Payfrom")
        Me.cmbPaymentMethod.SelectedValue = Id

        FillCombos("Company")

        If Not getConfigValueByType("EnabledBrandedSMS").ToString = "Error" Then
            EnabledBrandedSMS = getConfigValueByType("EnabledBrandedSMS")
        End If

        Me.Cursor = Cursors.Default
        Me.lblProgress.Visible = False

    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
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
                ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
            End If
            'If Not IsValidate() Then Exit Sub Comment against task:2381
            If Not msg_Confirm(str_ConfirmDelete) Then Exit Sub
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Delete() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 
            Me.GrdSaved.CurrentRow.Delete()

            Me.ReSetControls()
            'MsgBox("Record Successfully Deleted", MsgBoxStyle.Information, str_MessageHeader)
            ' msg_Information(str_informDelete)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try

    End Sub
    Private Sub cmbInvoiceList_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbInvoiceList.Enter
        Me.cmbInvoiceList.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub
    Private Sub txtRemarks_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRemarks.Leave
        If Me.txtRemarks.Text = "" AndAlso Me.cmbInvoiceList.Value <> 0 Then
            strInvoiceRemark = "Receipt Against Invoice No. " & Me.cmbInvoiceList.Text & ""
        Else
            strInvoiceRemark = Me.txtRemarks.Text
        End If
        Me.txtRemarks.Text = strInvoiceRemark
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
            MasterID = Me.GrdSaved.GetRow.Cells("ReceiptId").Value
            Me.txtReference.Text = Me.GrdSaved.GetRow.Cells("Remarks").Value.ToString
            Me.cmbAccounts.Value = Me.GrdSaved.GetRow.Cells("CustomerCode").Value

            'Task 3198 Commented Against Cost Center 
            'Me.cmbCostCenter.SelectedValue = Me.GrdSaved.GetRow.Cells("CostCenterId").Value

            Me.cmbPaymentMethod.SelectedValue = Me.GrdSaved.GetRow.Cells("PaymentMethod").Value
            Me.cmbPayFrom.SelectedValue = Me.GrdSaved.GetRow.Cells("PaymentAccountId").Value
            'Task No 2537 Append One Line Of Code For Geeting Value In Check Box 
            Me.ChkPost.Checked = Me.GrdSaved.GetRow.Cells("Post").Value
            Me.txtVoucherNo.Text = Me.GrdSaved.GetRow.Cells("ReceiptNo").Value.ToString
            Me.dtpDate.Value = Me.GrdSaved.GetRow.Cells("ReceiptDate").Value
            Me.txtChequeNo.Text = Me.GrdSaved.GetRow.Cells("ChequeNo").Value.ToString
            If Not IsDBNull(Me.GrdSaved.GetRow.Cells("ChequeDate").Value) Then
                'If Me.GrdSaved.GetRow.Cells("ChequeDate").Value <> Me.GrdSaved.GetRow.Cells("ChequeDate").Value Then
                Me.DtpChequeDate.Value = Me.GrdSaved.GetRow.Cells("ChequeDate").Value
                'End If
            End If
            Call DisplayRecord(Me.GrdSaved.GetRow.Cells("ReceiptId").Value.ToString) 'Here Call Local Method Of Dispaly Record Wich Is Used For Detail Record
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.BtnSave.Text = "&Update"
            If Me.BtnSave.Text = "&Update" Then
                Me.cmbAccounts.Enabled = False

                'Task 3198
                'Me.cmbCostCenter.Enabled = False

                Me.cmbPaymentMethod.Enabled = False
            End If
            Me.lblPrintStatus.Text = "Print Status: " & GrdSaved.GetRow.Cells("Print Status").Text.ToString
            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnPrint.Visible = True
            Me.BtnDelete.Visible = True
            ''''''''''''''''''''''''''''
            'Task#04082015 Edit company name (Ahmad Sharif)
            Me.cmbCompany.Text = Me.GrdSaved.GetRow.Cells("Company Name").Value.ToString

            GetSecurityRights(Utility.EnumDataMode.Edit) ''13-Aug-2014 Task:2782 Imran Ali Post Rights Implement On Invoice Base Payment\Receipt Voucher



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
        Me.cmbAccounts.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub

    Private Sub cmbAccounts_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbAccounts.KeyDown
        ''TFS1781 : Ayesha Rehman :Added for Selection of Vendor
        Try
            If e.KeyCode = Keys.F1 Then
                frmAccountSearch.AccountType = "'Customer'"
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
            txtCurrentBalance.Text = GetCurrentBalance(cmbAccounts.Value)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.ButtonClick
        Me.Cursor = Cursors.WaitCursor
        Try

            If Me.GrdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = GrdSaved.GetRow.Cells("ReceiptNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            AddRptParam("@ReceiptId", Me.GrdSaved.CurrentRow.Cells("ReceiptId").Value)
            ShowReport("rptinoivcebasereceipt")

            'If Me.GrdSaved.RowCount = 0 Then Exit Sub
            'PrintLog = New SBModel.PrintLogBE
            'PrintLog.DocumentNo = GrdSaved.GetRow.Cells("ReceiptNo").Value.ToString
            'PrintLog.UserName = LoginUserName
            'PrintLog.PrintDateTime = Date.Now
            'Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            'ShowReport("rptVoucher", "{VwGlVoucherSingle.Voucher_No}='" & Me.GrdSaved.CurrentRow.Cells("ReceiptNo").Value & "'")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    ''07-Aug-2014 Task:2775 Imran Ali Add Delete Button Field On Grid Detail in InvoiceBasedReceipt/Payment (Ravi)
    Private Sub GrdReceiptDetail_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GrdReceiptDetail.ColumnButtonClick
        Try
            If e.Column.Key = "Column1" Then
                Me.GrdReceiptDetail.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2775

    Private Sub GrdReceiptDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GrdReceiptDetail.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        If e.KeyCode = Keys.F2 Then
            NewToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
        'TASK:2398           Imran Ali        Update, Delete Problem in Cash Management
        'If e.KeyCode = Keys.Delete Then
        '    BtnDelete_Click(Nothing, Nothing)
        '    Exit Sub
        'End If

    End Sub
    Private Sub GrdReceiptDetail_UpdatingCell(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.UpdatingCellEventArgs) Handles GrdReceiptDetail.UpdatingCell
        Try

        Catch ex As Exception

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
    Private Sub GetSecurityRights(ByVal Mode As SBUtility.Utility.EnumDataMode)
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                ''13-Aug-2014 Task:2782 Imran Ali Post Rights Implement On Invoice Base Payment\Receipt Voucher
                Me.ChkPost.Visible = True
                'Me.ChkPost.Checked = True
                'End Task 2782
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
                'Me.Visible = False

                'If Mode = Utility.EnumDataMode.[New] Then
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'Me.ChkPost.Visible = False
                'Me.ChkPost.Checked = False
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
                        If Mode = Utility.EnumDataMode.[New] Then Me.ChkPost.Checked = True
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
                Me.CtrlGrdBar1.Visible = True
                Me.CtrlGrdBar2.Visible = False
                Me.BtnLoadAll.Visible = False
                ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                Me.BtnPrint.Visible = False
                Me.BtnDelete.Visible = False
                Me.BtnPrint.Visible = False
                '''''''''''''''''''''''''''
            Else
                Me.CtrlGrdBar1.Visible = False
                Me.CtrlGrdBar2.Visible = True
                Me.BtnLoadAll.Visible = True
                GetAllRecords()
                ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                Me.BtnPrint.Visible = True
                Me.BtnDelete.Visible = True
                Me.BtnPrint.Visible = True
                '''''''''''''''''''''''''''
            End If
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


    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub


    Private Sub PrintVoucherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintVoucherToolStripMenuItem.Click
        Try
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
            PrintVoucherBC(Me.GrdSaved.GetRow.Cells("VId").Value, Me.GrdSaved.GetRow.Cells("ReceiptNo").Value.ToString())
            
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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
            str.Add("Invoice Based Cash Receipt")
            str.Add("Invoice Based Bank Receipt")
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

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    'Task#04082015 company changed from combo box
    Private Sub cmbCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCompany.SelectedIndexChanged
        Try
            If EditMode = False Then
                Me.txtVoucherNo.Text = GetVoucherNo()
            End If
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

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not GrdSaved.GetRow Is Nothing AndAlso GrdSaved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frmReceiptVoucherNew"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = GrdSaved.CurrentRow.Cells(1).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Invoice base Receipt voucher (" & frmtask.Ref_No & ") "
                frmtask.Width = 950
                frmtask.ShowDialog()
                frmtask.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
                'frmtask.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    'Task 3197'

    Private Sub getNetAmount()
        Try
            Dim ReceivedAmount As Double = 0
            Dim SalesTax As Double = 0
            Dim Others As Double = 0

            ReceivedAmount = Val(Me.txtReceivedAmt.Text)
            SalesTax = Val(Me.txtSalesTaxAmount.Text)
            Others = Val(Me.txtOtherTaxAmount.Text)

            If ReceivedAmount >= 0 AndAlso SalesTax >= 0 AndAlso Others >= 0 Then

                Me.txtNetAmount.Text = Val(Me.txtReceivedAmt.Text) - (Val(Me.txtSalesTaxAmount.Text) + Val(Me.txtOtherTaxAmount.Text))

            End If

        Catch ex As Exception
            ShowErrorMessage("Plz Enter Valid Values")
        End Try
    End Sub

    Private Sub txtSalesTaxAmount_Leave(sender As Object, e As EventArgs) Handles txtSalesTaxAmount.Leave

        Try
            getNetAmount()
        Catch ex As Exception
            ShowErrorMessage("Plz Enter Valid Values")
        End Try

    End Sub

    Private Sub txtOtherTaxAmount_Leave(sender As Object, e As EventArgs) Handles txtOtherTaxAmount.Leave
        Try
            getNetAmount()
        Catch ex As Exception
            ShowErrorMessage("Plz Enter Valid Values")
        End Try
    End Sub

    Private Sub txtWHTax_Leave(sender As Object, e As EventArgs) Handles txtWHTax.Leave

        If Val(txtWHTax.Text) >= 0 Then

            Me.txtTaxAmount.Text = Val(Me.txtNetAmount.Text) * (Val(txtWHTax.Text) / 100)

            Me.txtAmount.Text = Val(Me.txtNetAmount.Text) - Val(Me.txtTaxAmount.Text)

        Else

            ShowErrorMessage("Plz Enter Positive Amount")

        End If

    End Sub

    Private Sub txtReceivedAmt_TextChanged(sender As Object, e As EventArgs) Handles txtReceivedAmt.TextChanged

        Try
            getNetAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdReceiptDetail.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdReceiptDetail.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GrdReceiptDetail.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Invoice Based Receipt"
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
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Invoice Based Receipt"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class