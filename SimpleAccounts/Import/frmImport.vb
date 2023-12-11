''10-12-2015 TASKTFS123 Muhammad Ameen: Allow to modify DD amount for CMC and ETO on import document.
'' TASK: TFS1261 Muhammad Ameen on 15-08-2017. Added new GRN Combo and filtered items contained within selected GRN.
''TASK TFS1350 added new field of Status on 22-08-2017
''TASK TFS06112017 Muhammad Ameen on 06-11-2017. LC Amount is not divided accurately when item value is changed. 
''TASK TFS1753 Muhammad Ameen on 14-11-2017. Track GRN Record detail wise.
''TASK TFS1956 Ayesha Rehman on 27-12-2017. Two new fields on Import Document Screen.
''TFS4163 Ayesha Rehman : Adding Item Batch Wise : 08-Aug-2018
''TFS4161 Ayesha Rehman : 09-08-2018 : P QTY: (Should Be Static/ Un-Changeable / Un-Editable on All Screens)
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net
Imports System.Text.Encoding
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Convert
Public Class frmImport
    Implements IGeneral
    Dim LCId As Integer = 0I
    Dim LC As LCBE
    Dim IsOpenedForm As Boolean = False
    Dim LoadAllStatus As String = String.Empty
    Dim arrFile() As String = {}
    Dim _AddCostAccountId As Integer = 0I
    Dim _CustomDutyAccountId As Integer = 0I
    Dim _SalesTaxAccountId As Integer = 0I
    Dim _AdditionalSalesTaxAccountId As Integer = 0I
    Dim _AdvanceIncomeTaxAccountId As Integer = 0I
    Dim _ExciseDutyAccountId As Integer = 0I
    Public IsFromItemWiseLC As Boolean = False
    Dim ExpenseIDList As Dictionary(Of String, Integer)
    Dim PerKG As Decimal
    Dim BaseCurrencyId As Integer
    Dim BaseCurrencyName As String = String.Empty
    Dim CurrencyObject As CurrencyRate
    Dim PurchaseOrderId As Integer = 0I
    Dim NewVoucherExpected As Boolean = False
    Dim StockInConfigration As String = "" ''4163
    Dim IsPackQtyDisabled As Boolean = False ''TFS4161
    Enum enmGrdDetail
        LocationId
        ArticleDefId
        Code
        Item
        HS_Code
        Origin
        Color
        Size
        Unit
        PackDesc
        Qty
        PackQty
        Exch_Rate
        Import_Value
        ImportAmount
        Price
        TotalAmount
        Weight
        Per_Kg_Cost
        Weighted_Cost
        Insurrance
        AddCostPercent
        AssessedValue
        DutyPercent
        Duty
        AddCustomDutyPercent   ''TFS1956
        AddCustomDuty          ''TFS1956
        RegulatoryDutyPercent  ''TFS1956
        RegulatoryDuty         ''TFS1956
        SaleTaxPercent
        SaleTax
        AddSaleTaxPercent
        AddSaleTax
        AdvIncomeTaxPercent
        AdvIncomeTax
        Net_Amount
        ExciseDutyPercent
        ExciseDuty
        BatchNo
        ExpiryDate
        Other_Charges
        Comments
        PurchaseAccountId
        TotalQty
        PurchaseOrderId
        PurchaseOrderDetailId
        LCDetailId
        CheckOtherCharges
        ReceivingNoteId

    End Enum
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        AddHandler txtImportName.KeyUp, AddressOf SetBgColorForTextBox
        'AddHandler txtBankPaidAmount.KeyUp, AddressOf SetBgColorForTextBox
        AddHandler txtExchangeRate.KeyUp, AddressOf SetBgColorForTextBox
        AddHandler txtInsurrance.KeyUp, AddressOf SetBgColorForTextBox
        'AddHandler txtPerformaNo.KeyUp, AddressOf SetBgColorForTextBox
        'AddHandler txtPortCharges.KeyUp, AddressOf SetBgColorForTextBox
        'AddHandler txtShippingCharges.KeyUp, AddressOf SetBgColorForTextBox
        'AddHandler cmbPortOfLoading.KeyUp, AddressOf SetBgColorForTextBox
        'AddHandler cmbPortOfDischarge.KeyUp, AddressOf SetBgColorForTextBox
        'AddHandler txtAmendment.KeyUp, AddressOf SetBgColorForTextBox
        'AddHandler txtAdvisingBank.KeyUp, AddressOf SetBgColorForTextBox
        'AddHandler txtSpecialInstruction.KeyUp, AddressOf SetBgColorForTextBox
        'AddHandler txtRemarks.KeyUp, AddressOf SetBgColorForTextBox
        'AddHandler txtVessel.KeyUp, AddressOf SetBgColorForTextBox
        'AddHandler txtBLNo.KeyUp, AddressOf SetBgColorForTextBox

        'AddHandler txtBankPaidAmount.KeyPress, AddressOf NumValidation
        AddHandler txtExchangeRate.KeyPress, AddressOf NumValidation
        AddHandler txtInsurrance.KeyPress, AddressOf NumValidation
        'AddHandler txtPortCharges.KeyPress, AddressOf NumValidation
        AddHandler txtCMCC.KeyPress, AddressOf NumValidation
        AddHandler txtETO.KeyPress, AddressOf NumValidation
        'AddHandler txtShippingCharges.KeyPress, AddressOf NumValidation
        AddHandler txtTotalAmount.KeyPress, AddressOf NumValidation
        AddHandler txtPackQty.KeyPress, AddressOf NumValidation
        AddHandler txtQty.KeyPress, AddressOf NumValidation
        AddHandler txtPrice.KeyPress, AddressOf NumValidation
        AddHandler txtAddCostPercent.KeyPress, AddressOf NumValidation
        AddHandler txtAssessedValue.KeyPress, AddressOf NumValidation
        AddHandler txtDutyPercent.KeyPress, AddressOf NumValidation
        AddHandler txtDuty.KeyPress, AddressOf NumValidation
        AddHandler txtAddCustomDutyPercent.KeyPress, AddressOf NumValidation  ''TFS1956
        AddHandler txtAddCustomDuty.KeyPress, AddressOf NumValidation         ''TFS1956
        AddHandler txtRegulatoryDutyPercent.KeyPress, AddressOf NumValidation ''TFS1956
        AddHandler txtRegulatoryDuty.KeyPress, AddressOf NumValidation        ''TFS1956
        AddHandler txtSalesTaxPercent.KeyPress, AddressOf NumValidation
        AddHandler txtSalesTax.KeyPress, AddressOf NumValidation
        AddHandler txtAddSalesTaxPercent.KeyPress, AddressOf NumValidation
        AddHandler txtAddSalesTax.KeyPress, AddressOf NumValidation
        AddHandler txtAdvIncomeTaxPercent.KeyPress, AddressOf NumValidation
        AddHandler txtAdvIncomeTax.KeyPress, AddressOf NumValidation

    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            If Condition = "Master" Then
                Me.grdHistory.RootTable.Columns("LCID").Visible = False
                Me.grdHistory.RootTable.Columns("LCAccountId").Visible = False
                Me.grdHistory.RootTable.Columns("CostCenterId").Visible = False
                Me.grdHistory.RootTable.Columns("LCDocId").Visible = False
                Me.grdHistory.RootTable.Columns("Financial_Impact").Visible = False
                Me.grdHistory.RootTable.Columns("ReceivingNoteId").Visible = False
                Me.grdHistory.RootTable.Columns("PurchaseOrderId").Visible = False
                Me.grdHistory.RootTable.Columns("Status").Visible = False
                Me.grdHistory.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
                Dim FormatCondition As New Janus.Windows.GridEX.GridEXFormatCondition(Me.grdHistory.RootTable.Columns("Financial_Impact"), Janus.Windows.GridEX.ConditionOperator.Equal, 1)
                FormatCondition.FormatStyle.BackColor = Color.LightYellow
                Me.grdHistory.RootTable.FormatConditions.Add(FormatCondition)
                Me.grdHistory.AutoSizeColumns()
                ' '' TASK TFS1462 Turn color of row to light yellow in case financial impact is available in voucher
                'FormatCondition.Column = Me.grdHistory.RootTable.Columns("Financial_Impact")
                'FormatCondition.TargetColumn = Me.grdHistory.RootTable.Columns("Financial_Impact")
                'FormatCondition.Value1 = 1
                'Dim FilterCondition As New Janus.Windows.GridEX.GridEXFilterCondition
                'FilterCondition.ConditionOperator = Janus.Windows.GridEX.ConditionOperator.Equal
                'FormatCondition.FilterCondition = FilterCondition
                'Dim FormatStyle As New Janus.Windows.GridEX.GridEXFormatStyle
                'FormatStyle.BackColor = Color.LightYellow
                'FormatCondition.FormatStyle = FormatStyle
                'Me.grdHistory.RootTable.FormatConditions.Add(FormatCondition)
                'FormatCondition.Value1 = 1
                ''END TASK TFS1462

                ''Start TFS4225
                Me.grdHistory.RootTable.Columns("Insurrance").FormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("Insurrance").TotalFormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("ExchangeRate").FormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("ExchangeRate").TotalFormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("OtherCharges").FormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("OtherCharges").TotalFormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("ExpenseByLC").FormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("ExpenseByLC").TotalFormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("AssessedValue").FormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("AssessedValue").TotalFormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("SalesTax").FormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("SalesTax").TotalFormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("Add Sales Tax").FormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("Add Sales Tax").TotalFormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("Adv Income Tax").FormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("Adv Income Tax").TotalFormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("Excise Duty%").FormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("Excise Duty%").TotalFormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("Duty").FormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("Duty").TotalFormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("ExciseDuty").FormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("ExciseDuty").TotalFormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("TotalAmount").FormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("TotalAmount").TotalFormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("DDForCMCC").FormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("DDForCMCC").TotalFormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("DDForETO").FormatString = "N" & DecimalPointInValue
                Me.grdHistory.RootTable.Columns("DDForETO").TotalFormatString = "N" & DecimalPointInValue

                ''End TFS4225

            ElseIf Condition = "Detail" Then
                For c As Integer = 0 To Me.grdDetail.RootTable.Columns.Count - 1
                    If Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.LocationId AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.Weight AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.Qty AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.TotalQty AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.Exch_Rate AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.Import_Value AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.AddCostPercent AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.DutyPercent AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.SaleTaxPercent AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.AddSaleTaxPercent AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.AdvIncomeTaxPercent AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.ExciseDutyPercent AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.Comments AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.AddCustomDutyPercent AndAlso Me.grdDetail.RootTable.Columns(c).Index <> enmGrdDetail.RegulatoryDutyPercent Then ''TFS1956
                        Me.grdDetail.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Next
                ''Start TFS4161
                If IsPackQtyDisabled = True Then
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.TotalQty).EditType = Janus.Windows.GridEX.EditType.NoEdit
                Else
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.TotalQty).EditType = Janus.Windows.GridEX.EditType.TextBox
                End If
                ''End TFS4161
                If getConfigValueByType("ImportWieghtWiseCalculation").ToString.ToUpper = "TRUE" Then
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.Weight).Visible = True
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.Weighted_Cost).Visible = True
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.Per_Kg_Cost).Visible = True
                    Me.txtWeight.Enabled = True
                Else
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.Weight).Visible = False
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.Weighted_Cost).Visible = False
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.Per_Kg_Cost).Visible = False
                    Me.txtWeight.Enabled = False
                End If
                Me.txtWeight.Text = "1"
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Weighted_Cost).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Price).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Exch_Rate).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Import_Value).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.ImportAmount).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Weight).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Per_Kg_Cost).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Other_Charges).FormatString = "N" & DecimalPointInValue

                Me.grdDetail.RootTable.Columns(enmGrdDetail.CheckOtherCharges).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.SaleTax).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.TotalAmount).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Insurrance).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Duty).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.AddCustomDuty).FormatString = "N" & DecimalPointInValue ''TFS1956
                Me.grdDetail.RootTable.Columns(enmGrdDetail.RegulatoryDuty).FormatString = "N" & DecimalPointInValue  ''TFS1956
                Me.grdDetail.RootTable.Columns(enmGrdDetail.AddSaleTax).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.AdvIncomeTax).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Net_Amount).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.ExciseDuty).FormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.SaleTax).TotalFormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.TotalAmount).TotalFormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Insurrance).TotalFormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Duty).TotalFormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.AddCustomDuty).TotalFormatString = "N" & DecimalPointInValue ''TFS1956
                Me.grdDetail.RootTable.Columns(enmGrdDetail.RegulatoryDuty).TotalFormatString = "N" & DecimalPointInValue  ''TFS1956
                Me.grdDetail.RootTable.Columns(enmGrdDetail.AddSaleTax).TotalFormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.AdvIncomeTax).TotalFormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Net_Amount).TotalFormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.ExciseDuty).TotalFormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Other_Charges).TotalFormatString = "N" & DecimalPointInValue
                Me.grdDetail.RootTable.Columns(enmGrdDetail.CheckOtherCharges).TotalFormatString = "N" & DecimalPointInValue


                ''
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Qty).FormatString = "N" & DecimalPointInQty
                Me.grdDetail.RootTable.Columns(enmGrdDetail.TotalQty).FormatString = "N" & DecimalPointInQty
                Me.grdDetail.RootTable.Columns(enmGrdDetail.PackQty).FormatString = "N" & DecimalPointInQty
                Me.grdDetail.RootTable.Columns(enmGrdDetail.Qty).TotalFormatString = "N" & DecimalPointInQty
                Me.grdDetail.RootTable.Columns(enmGrdDetail.TotalQty).TotalFormatString = "N" & DecimalPointInQty
                Me.grdDetail.RootTable.Columns(enmGrdDetail.PackQty).TotalFormatString = "N" & DecimalPointInQty

                Me.grdDetail.RootTable.Columns(enmGrdDetail.AddCustomDuty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far  ''TFS1956
                Me.grdDetail.RootTable.Columns(enmGrdDetail.RegulatoryDuty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far   ''TFS1956
                Me.grdDetail.RootTable.Columns(enmGrdDetail.AddCustomDuty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far  ''TFS1956
                Me.grdDetail.RootTable.Columns(enmGrdDetail.RegulatoryDuty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far   ''TFS1956
                Me.grdDetail.RootTable.Columns(enmGrdDetail.AddCustomDutyPercent).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far  ''TFS1956
                Me.grdDetail.RootTable.Columns(enmGrdDetail.RegulatoryDutyPercent).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far   ''TFS1956
                Me.grdDetail.RootTable.Columns(enmGrdDetail.AddCustomDutyPercent).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far  ''TFS1956
                Me.grdDetail.RootTable.Columns(enmGrdDetail.RegulatoryDutyPercent).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far   ''TFS1956

                'Start Task 4163
                If StockInConfigration.Equals("Disabled") Then
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.BatchNo).Visible = False
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.ExpiryDate).Visible = False
                ElseIf StockInConfigration.Equals("Enabled") Then
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.BatchNo).Visible = True
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.ExpiryDate).Visible = True
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.BatchNo).EditType = Janus.Windows.GridEX.EditType.TextBox
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
                Else
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.BatchNo).Visible = True
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.ExpiryDate).Visible = True
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.BatchNo).EditType = Janus.Windows.GridEX.EditType.TextBox
                    Me.grdDetail.RootTable.Columns(enmGrdDetail.ExpiryDate).EditType = Janus.Windows.GridEX.EditType.CalendarDropDown
                End If
                'End Task 4163
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                PrintVoucherToolStripMenuItem.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefColor)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                PrintVoucherToolStripMenuItem.Enabled = False
                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print Voucher" Then
                        PrintVoucherToolStripMenuItem.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New LCDAL().Delete(LC) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function DeleteDetail(ByVal LCID As Integer, ByVal LCDetailId As Integer) As Boolean
        Try
            If New LCDAL().DeleteDetail(LCID, LCDetailId, txtLCNo.Text) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty
            If Condition = "LCAccount" Then
                FillUltraDropDown(Me.cmbLCAccount, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Code], sub_sub_title as [Account Head], Account_Type as [Type] From vwCOADetail WHERE detail_title <> '' AND Account_type IN('Vendor','LC') ORDER BY detail_title Asc")
                Me.cmbLCAccount.Rows(0).Activate()
                Me.cmbLCAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'ElseIf Condition = "LCExpAccount" Then
                '    FillUltraDropDown(Me.cmbLCExpAccount, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Code], sub_sub_title as [Account Head] From vwCOADetail WHERE detail_title <> '' ORDER BY detail_title Asc")
                '    Me.cmbLCExpAccount.Rows(0).Activate()
                '    Me.cmbLCExpAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'ElseIf Condition = "SupplierAccount" Then
                '    FillUltraDropDown(Me.cmbSupplierAccount, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Code], sub_sub_title as [Account Head] From vwCOADetail WHERE detail_title <> '' ORDER BY detail_title Asc")
                '    Me.cmbSupplierAccount.Rows(0).Activate()
                '    Me.cmbSupplierAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'ElseIf Condition = "IndenterAccount" Then
                '    FillUltraDropDown(Me.cmbIndenterAccount, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Code], sub_sub_title as [Account Head] From vwCOADetail WHERE detail_title <> '' ORDER BY detail_title Asc")
                '    Me.cmbIndenterAccount.Rows(0).Activate()
                '    Me.cmbIndenterAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'ElseIf Condition = "OpeningBank" Then
                '    FillUltraDropDown(Me.cmbOpeningBank, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Code], sub_sub_title as [Account Head] From vwCOADetail WHERE detail_title <> '' ANd Account_Type='Bank' ORDER BY detail_title Asc")
                '    Me.cmbOpeningBank.Rows(0).Activate()
                '    Me.cmbOpeningBank.DisplayLayout.Bands(0).Columns(0).Hidden = True
                'ElseIf Condition = "Transporter" Then
                '    FillDropDown(Me.cmbTransporter, "Select TransporterId, TransporterName From tblDefTransporter")
                'ElseIf Condition = "OpenedBy" Then
                '    FillDropDown(Me.cmbOpenedBy, "Select Employee_Id, Employee_Name,Employee_Code From tblDefEmployee")
            ElseIf Condition = "Location" Then
                strSQL = String.Empty
                'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
                '    strSQL = "Select Location_Id, Location_Code, Location_Name From tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")"
                'Else
                '    strSQL = "Select Location_Id, Location_Code, Location_Name From tblDefLocation"
                'End If
                strSQL = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                FillDropDown(Me.cmbLocation, strSQL, False)
            ElseIf Condition = "grdLocation" Then
                strSQL = String.Empty
                'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
                '    strSQL = "Select Location_Id, Location_Code, Location_Name From tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")"
                'Else
                '    strSQL = "Select Location_Id, Location_Code, Location_Name From tblDefLocation"
                'End If
                strSQL = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
               & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
               & " Else " _
               & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                dt.AcceptChanges()
                Me.grdDetail.RootTable.Columns("LocationId").ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
            ElseIf Condition = "Item" Then
                FillUltraDropDown(Me.cmbItem, "Select ArticleId, ArticleDescription as [Item], ArticleCode as [Code], HS_Code as [HS Code], ArticleGenderName as [Origin], ArticleColorName as [Color],ArticleSizeName as [Size], PurchasePrice as [Price],MasterID, IsNull(SubSubId,0) as PurchaseAccountId From ArticleDefView")
                Me.cmbItem.Rows(0).Activate()
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("ArticleId").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterID").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchaseAccountId").Hidden = True
                End If
            ElseIf Condition = "Pack" Then
                If Me.cmbItem.IsItemInList = True Then
                    Me.cmbUnit.ValueMember = "ArticlePackId"
                    Me.cmbUnit.DisplayMember = "PackName"
                    Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
                End If
                'ElseIf Condition = "PortDischarge" Then
                '    FillDropDown(Me.cmbPortOfDischarge, "Select DISTINCT PortOfDischarge, PortOfDischarge From LCMasterTable", False)
                'ElseIf Condition = "PortLoading" Then
                '    FillDropDown(Me.cmbPortOfLoading, "Select DISTINCT PortOfLoading, PortOfLoading From LCMasterTable", False)
                'ElseIf Condition = "ClearingAgent" Then
                '    FillDropDown(Me.cmbClearingAgent, "Select DISTINCT ClearingAgent, ClearingAgent From LCOtherDetailTable", False)
            ElseIf Condition = "Project" Then
                Dim Str As String = "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") " _
                  & " Select CostCenterID, Name As [Cost Center] from tblDefCostCenter where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights  where UserID = " & LoginUserId & ") And IsNull(Active, 0) =1 ORDER BY 2 ASC " _
                  & " Else " _
                  & " Select CostCenterID, Name As [Cost Center] from tblDefCostCenter Where IsNull(Active, 0) = 1 ORDER BY 2 ASC "
                FillDropDown(Me.cmbProject, Str)
                'FillDropDown(Me.cmbProject, "Select costcenterId, name as [Cost Center] From tblDefCostCenter")
            ElseIf Condition = "LC" Then
                FillDropDown(Me.cmbLC, "Select IsNull(LCDoc_Id,0), LCDoc_No,* from tblLetterOfCredit WHERE LCDoc_No <> '' " & IIf(Me.btnSave.Text <> "&Save", "", " AND IsNull(LCDoc_ID,0) Not In(Select IsNull(LCDocID,0) as LCDocID From LCMasterTable) ") & "   ORDER BY LCDoc_ID DESC")
            ElseIf Condition = "Currency" Then
                Dim Str As String = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
                FillDropDown(Me.cmbCurrency, Str, False)
                Me.cmbCurrency.SelectedValue = BaseCurrencyId
                '' TASK TFS1261 Added new combo for GRN items.

            ElseIf Condition = "GRN" Then
                Dim Str As String = "Select ReceivingNoteId, ReceivingNo + '~' + Convert(Varchar(12), ReceivingDate, 102) As [Receiving No] From ReceivingNoteMasterTable Where LCId = " & IIf(cmbLC.SelectedValue = Nothing, 0, cmbLC.SelectedValue) & " AND LCId > 0  AND ReceivingNoteId NOT IN (SELECT IsNull(ReceivingNoteId, 0) AS ReceivingNoteId FROM ReceivingDetailTable Where ReceivingNoteId Is Not Null)"
                FillDropDown(Me.cmbGRN, Str)
            ElseIf Condition = "GRNItems" Then
                FillUltraDropDown(Me.cmbItem, "Select ArticleId, ArticleDescription as [Item], ArticleCode as [Code], HS_Code as [HS Code], ArticleGenderName as [Origin], ArticleColorName as [Color],ArticleSizeName as [Size], PurchasePrice as [Price],MasterID, IsNull(SubSubId,0) as PurchaseAccountId, IsNull(Detail.Qty, 0) As Qty From ArticleDefView INNER JOIN ReceivingNoteDetailTable As Detail ON ArticleDefView.ArticleId = Detail.ArticleDefId INNER JOIN ReceivingNoteMasterTable AS Receiving ON Detail.ReceivingNoteId = Receiving.ReceivingNoteId Where Receiving.ReceivingNoteId =" & Me.cmbGRN.SelectedValue & "")
                Me.cmbItem.Rows(0).Activate()
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("ArticleId").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterID").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchaseAccountId").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Qty").Hidden = True
                End If
                ''TASK TFS1350 added new field of Status on 22-08-2017
            ElseIf Condition = "Status" Then
                Dim Str As String = "Select Distinct Status, Status From LCMasterTable Where Status <> '' "
                FillDropDown(Me.cmbStatus, Str)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Me.grdDetail.UpdateData()
            ApplyInsurrance()
            GetTotal()

            Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString


            CurrencyObject = New CurrencyRate
            With CurrencyObject
                .CurrencyId = Me.cmbCurrency.SelectedValue
                .BaseCurrencyId = ConfigCurrencyVal
                .BaseCurrencyRates = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
                .CurrencyRate = (Val(txtExchangeRate.Text))
                .CurrencySymbol = Me.cmbCurrency.Text


            End With



            LC = New LCBE
            With LC
                .CurrenyObj = CurrencyObject
                .LCID = LCId
                .LCNo = Me.txtLCNo.Text
                .LCDate = Me.dtpLCDate.Value
                .LCAccountId = Me.cmbLCAccount.Value
                .LCExpenseAccountId = 0 'Me.cmbLCExpAccount.Value
                .ImportName = Me.txtImportName.Text
                .PerformaNo = String.Empty 'Me.txtPerformaNo.Text
                .PerformaDate = Date.Now 'Me.dtpPerformaDate.Value
                .PaymentMode = String.Empty 'Me.cmbPaymentMethod.Text
                .LCBoreDate = Date.Now 'Me.dtpLCBeforeDate.Value
                .PortOfDischarge = 0 'Me.cmbPortOfDischarge.Text
                .PortOfLoading = 0 'Me.cmbPortOfLoading.Text
                .SupplierAccountId = 0 'Me.cmbSupplierAccount.Value
                .IndenterAccountId = 0 'Me.cmbIndenterAccount.Value
                .PartialShipment = 0 'IIf(Me.chkPartialShipment.Checked = True, 1, 0)
                .Transhipment = 0 'IIf(Me.chkTranship.Checked = True, 1, 0)
                .LatestDateOfShipment = Date.MinValue 'Me.dtpLatestDateofShipment.Value
                .LSBDate = Date.MinValue 'Me.dtpLSBDate.Value
                .Insurrance = Val(Me.txtInsurrance.Text)
                .ExchangeRate = Val(Me.txtExchangeRate.Text)
                .BankPaidAmount = 0 'Val(Me.txtBankPaidAmount.Text)
                .ShipingCharges = Val(Me.txtShippingCharges.Text)
                .PortCharges = Val(Me.txtPortCharges.Text)
                .DDForCMCC = Val(Me.txtCMCC.Text)
                .DDForETO = Val(Me.txtETO.Text)
                .TotalAmount = Val(Me.txtTotalAmount.Text)
                .TotalDuty = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.Duty), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.AddCustomDuty), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.RegulatoryDuty), Janus.Windows.GridEX.AggregateFunction.Sum)  ''TFS1956
                ''TASK-408
                '.TotalQty = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.Qty), Janus.Windows.GridEX.AggregateFunction.Sum)
                .TotalQty = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.TotalQty), Janus.Windows.GridEX.AggregateFunction.Sum) ''TASK-408 added TotalQty instead of Qty
                .SalesTax = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.SaleTax), Janus.Windows.GridEX.AggregateFunction.Sum)
                .AdditionalSalesTax = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.AddSaleTax), Janus.Windows.GridEX.AggregateFunction.Sum)
                .AdvanceIncomeTax = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.AdvIncomeTax), Janus.Windows.GridEX.AggregateFunction.Sum)
                .AssessedValue = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.AssessedValue), Janus.Windows.GridEX.AggregateFunction.Sum)
                .UserName = LoginUserName
                .EntryDate = Date.Now
                .CostCenterId = Me.cmbProject.SelectedValue
                .ExciseDutyPercent = 0 'Val(Me.txtExciseDutyPercent.Text)
                .ExciseDuty = Val(Me.txtETO.Text) 'Val(Me.txtExciseDuty.Text)
                .LCDocId = Me.cmbLC.SelectedValue
                .OthersCharges = Val(Me.txtOtherCharges.Text)
                .ExpenseByLC = Val(Me.txtExpenseByLC.Text)
                .FinancialImpact = Me.chkFinancialImpact.Checked
                .ShippingRemarks = Me.txtShippingRemarks.Text
                .PortRemarks = Me.txtPortRemarks.Text
                .CMCCRemarks = Me.txtCMCCRemarks.Text
                .ETORemarks = Me.txtETORemarks.Text
                .AdjCMCCAmount = Val(Me.txtAdjDDForCMCC.Text)
                .AdjETOAmount = Val(Me.txtAdjDDETO.Text)
                .ExpensesIDs = ExpenseIDList
                .PurchaseOrderId = PurchaseOrderId
                .NewVoucherNo = ""
                .VoucherRemarks = ""
                .IsNewVoucher = False
                If Not Me.cmbGRN.SelectedIndex = -1 Then
                    .ReceivingNoteId = Me.cmbGRN.SelectedValue
                Else
                    .ReceivingNoteId = 0
                End If
                If Not Me.cmbStatus.Text = "" Then
                    .Status = Me.cmbStatus.Text
                Else
                    .Status = ""
                End If
                .VoucherRemarks = ""
                .CurrentVoucherNo = Me.txtLCNo.Text
                .NewVoucherNo = ""
                .IsNewVoucher = False
                .VoucherDate = Me.dtpLCDate.Value
                .FirstVoucher = True
                .LCDetail = New List(Of LCDetailBE)
                Dim objLCDetail As LCDetailBE
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                    With .LCDetail
                        objLCDetail = New LCDetailBE
                        With objLCDetail
                            .AddCostPercent = Val(r.Cells(enmGrdDetail.AddCostPercent).Value.ToString)
                            .AddSaleTax = Val(r.Cells(enmGrdDetail.AddSaleTax).Value.ToString)
                            .AddSaleTaxPercent = Val(r.Cells(enmGrdDetail.AddSaleTaxPercent).Value.ToString)
                            .AdvIncomeTax = Val(r.Cells(enmGrdDetail.AdvIncomeTax).Value.ToString)
                            .Weight = Val(r.Cells(enmGrdDetail.Weight).Value.ToString)
                            .Weighted_Cost = Val(r.Cells(enmGrdDetail.Weighted_Cost).Value.ToString)
                            .Per_Kg_Cost = Val(r.Cells(enmGrdDetail.Per_Kg_Cost).Value.ToString)
                            .AdvIncomeTaxPercent = Val(r.Cells(enmGrdDetail.AdvIncomeTaxPercent).Value.ToString)
                            .ArticleDefId = Val(r.Cells(enmGrdDetail.ArticleDefId).Value.ToString)
                            .ArticleSize = r.Cells(enmGrdDetail.Unit).Value.ToString
                            .AssessedValue = Val(r.Cells(enmGrdDetail.AssessedValue).Value.ToString)
                            .Comments = r.Cells(enmGrdDetail.Comments).Value.ToString
                            .Duty = Val(r.Cells(enmGrdDetail.Duty).Value.ToString)
                            .DutyPercent = Val(r.Cells(enmGrdDetail.DutyPercent).Value.ToString)
                            .AddCustomDuty = Val(r.Cells(enmGrdDetail.AddCustomDuty).Value.ToString) ''TFS1956
                            .AddCustomDutyPercent = Val(r.Cells(enmGrdDetail.AddCustomDutyPercent).Value.ToString) ''TFS1956
                            .RegulatoryDuty = Val(r.Cells(enmGrdDetail.RegulatoryDuty).Value.ToString) ''TFS1956
                            .RegulatoryDutyPercent = Val(r.Cells(enmGrdDetail.RegulatoryDutyPercent).Value.ToString) ''TFS1956
                            .Insurrance = Val(r.Cells(enmGrdDetail.Insurrance).Value.ToString)
                            .LCDetailId = Val(r.Cells(enmGrdDetail.LCDetailId).Value.ToString)
                            .LCId = LCId
                            .LocationId = Val(r.Cells(enmGrdDetail.LocationId).Value.ToString)
                            .PackDesc = r.Cells(enmGrdDetail.PackDesc).Value.ToString
                            .Price = Val(r.Cells(enmGrdDetail.Price).Value.ToString)
                            ''TASK-408 Commented below line to replace Qty with TotalQty
                            '.Qty = IIf(.ArticleSize.ToString = "Loose", Val(r.Cells(enmGrdDetail.Qty).Value.ToString), (Val(r.Cells(enmGrdDetail.Qty).Value.ToString) * Val(r.Cells(enmGrdDetail.PackQty).Value.ToString)))
                            .Qty = Val(r.Cells(enmGrdDetail.TotalQty).Value.ToString) ''TASK-408 on 11-06-2016
                            .SaleTax = Val(r.Cells(enmGrdDetail.SaleTax).Value.ToString)
                            .SaleTaxPercent = Val(r.Cells(enmGrdDetail.SaleTaxPercent).Value.ToString)
                            .Sz1 = Val(r.Cells(enmGrdDetail.Qty).Value.ToString)
                            .Sz7 = Val(r.Cells(enmGrdDetail.PackQty).Value.ToString)
                            .TotalAmount = Val(r.Cells(enmGrdDetail.TotalAmount).Value.ToString)
                            .PurchaseAccountId = Val(r.Cells(enmGrdDetail.PurchaseAccountId).Value.ToString)
                            .Exch_Rate = Val(r.Cells(enmGrdDetail.Exch_Rate).Value.ToString)
                            .Import_Value = Val(r.Cells(enmGrdDetail.Import_Value).Value.ToString)
                            .LedgerComments = String.Empty
                            .ArticleDescription = r.Cells(enmGrdDetail.Item).Value.ToString
                            .ExciseDutyPercent = Val(r.Cells(enmGrdDetail.ExciseDutyPercent).Value.ToString)
                            .ExciseDuty = Val(r.Cells(enmGrdDetail.ExciseDuty).Value.ToString)
                            .Other_Charges = Val(r.Cells(enmGrdDetail.Other_Charges).Value.ToString)
                            .Check_Other_Charges = Val(r.Cells(enmGrdDetail.CheckOtherCharges).Value.ToString)
                            .Net_Amount = Val(r.Cells(enmGrdDetail.Net_Amount).Value.ToString)
                            .AdditionalCostAccountId = _AddCostAccountId
                            .CustomDutyAccountId = _CustomDutyAccountId
                            .SalesTaxAccountId = _SalesTaxAccountId
                            .AdditionalSalesTaxAccountId = _AdditionalSalesTaxAccountId
                            .AdvanceIncomeTaxAccountId = _AdvanceIncomeTaxAccountId
                            .ExciseDutyAccountId = _ExciseDutyAccountId
                            .PurchaseOrderId = Val(r.Cells(enmGrdDetail.PurchaseOrderId).Value.ToString)
                            .PurchaseOrderDetailId = Val(r.Cells(enmGrdDetail.PurchaseOrderDetailId).Value.ToString)
                            .ReceivingNoteId = Val(r.Cells(enmGrdDetail.ReceivingNoteId).Value.ToString)
                            ''Start TFS4163
                            .BatchNo = r.Cells(enmGrdDetail.BatchNo).Value.ToString
                            .ExpiryDate = CType(r.Cells(enmGrdDetail.ExpiryDate).Value, Date).ToString("yyyy-M-d h:mm:ss tt")
                            ''End TFS4163
                            If .Other_Charges <> .Check_Other_Charges Then
                                NewVoucherExpected = True
                            End If
                        End With
                        .Add(objLCDetail)
                    End With
                Next
                .ActivityLog = New ActivityLog
                With .ActivityLog
                    .User_Name = LoginUserName
                    .UserID = LoginUserId
                    .Source = Me.Name
                    .FormCaption = Me.Text
                    .LogDateTime = Date.Now
                End With
            End With


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            FillCombos("Currency")
            If Condition = "Master" Then
                Dim dt As New DataTable
                dt = New LCDAL().DisplayRecord(LoadAllStatus)
                dt.AcceptChanges()
                Me.grdHistory.DataSource = dt
                Me.grdHistory.RetrieveStructure()
                Me.grdHistory.AutoSizeColumns()
                ApplyGridSettings("Master")
            ElseIf Condition = "Detail" Then
                Dim dtDetail As New DataTable
                dtDetail = New LCDAL().DisplayDetail(LCId)
                dtDetail.AcceptChanges()
                'dtDetail.Columns("Price").Expression = "IIF(Unit='Loose', (([Exch_Rate]*[Import_Value])*[Qty]), (([Exch_Rate]*[Import_Value])*([Qty]*[Pack Qty])))"
                dtDetail.Columns("Price").Expression = "([Exch_Rate]*[Import_Value])"
                '' Commented below line for TASK-408 on 11-06-2016
                ''dtDetail.Columns("TotalAmount").Expression = "IIF(Unit='Loose', (Qty*Price),((Qty*[Pack Qty])*Price))"
                dtDetail.Columns("TotalAmount").Expression = "([TotalQty]*[Price])"
                dtDetail.Columns("Assd.Value").Expression = "(([Add.Cost%]/100)*([Insurrance]+[TotalAmount]))+[Insurrance]+[TotalAmount]"
                dtDetail.Columns("Duty").Expression = "(([Duty%]/100)*[Assd.Value])"
                dtDetail.Columns("AddCustomDuty").Expression = "(([AddCustomDuty%]/100)*[Assd.Value])" ''TFS1956
                dtDetail.Columns("RegulatoryDuty").Expression = "(([RegulatoryDuty%]/100)*[Assd.Value])" ''TFS1956
                dtDetail.Columns("SaleTax").Expression = "([S. Tax%]/100)*([Assd.Value]+[Duty] +[AddCustomDuty] +[RegulatoryDuty])" ''TFS1956
                dtDetail.Columns("Add.S.Tax").Expression = "([Add.S.Tax%]/100)*([Assd.Value]+[Duty] +[AddCustomDuty]+[RegulatoryDuty])" ''TFS1956
                dtDetail.Columns("Adv.I.Tax").Expression = "([Adv.I.Tax%]/100)*([Assd.Value]+[Duty]+[AddCustomDuty]+[RegulatoryDuty]+[SaleTax]+[Add.S.Tax])" ''TFS1956
                dtDetail.Columns("Net Amount").Expression = "(([Duty]+[AddCustomDuty]+[RegulatoryDuty]+[SaleTax]+[Add.S.Tax]+[Adv.I.Tax]))" ''TFS1956
                dtDetail.Columns("ExciseDuty").Expression = "(([ExciseDutyPercent]/100)*[Assd.Value])"

                dtDetail.AcceptChanges()
                Me.grdDetail.DataSource = dtDetail
                Dim dblOtherCharges As Double = (Val(Me.txtExpenseByLC.Text))
                Dim dblInsurrance As Double = Val(Me.txtInsurrance.Text)
                Dim dblTotalAmount As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.Duty), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.AddCustomDuty), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.RegulatoryDuty), Janus.Windows.GridEX.AggregateFunction.Sum) ''TFS1956
                'If (dtDetail.Rows.Count > 0) Then
                '    If Not IsDBNull(dtDetail.Rows(0).Item("Per_Kg_Cost")) Then
                '        PerKG = dtDetail.Rows(0).Item("Per_Kg_Cost")
                '    Else
                '        PerKG = 1

                '    End If


                'End If
                Dim cost As Decimal
                Dim Totalweight As Decimal
                Dim WeightedCost As Decimal
                Dim TotalItemsAmount As Decimal

                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                    TotalItemsAmount = Val(Me.txtAmount.Text) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum)

                    cost = Val(Me.txtExpenseByLC.Text) - TotalItemsAmount
                    Totalweight = Val(Me.txtWeight.Text) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.Weight), Janus.Windows.GridEX.AggregateFunction.Sum) - 1
                    If Totalweight = 0 Then
                        Totalweight = 1
                    End If
                    PerKG = Math.Round(Val(Me.txtExpenseByLC.Text) / Totalweight, DecimalPointInValue)

                    r.BeginEdit()
                    r.Cells(enmGrdDetail.ImportAmount).Value = r.Cells(enmGrdDetail.Import_Value).Value * r.Cells(enmGrdDetail.TotalQty).Value
                    If (Val(r.Cells(enmGrdDetail.TotalAmount).Value) > 0) Then

                        r.Cells(enmGrdDetail.Insurrance).Value = Math.Round(((dblInsurrance * ((Val(r.Cells(enmGrdDetail.TotalAmount).Value) + Val(r.Cells(enmGrdDetail.Duty).Value.ToString) + Val(r.Cells(enmGrdDetail.AddCustomDuty).Value.ToString) + Val(r.Cells(enmGrdDetail.RegulatoryDuty).Value.ToString)) / dblTotalAmount)) / 100) * 100, 0) ''TFS1956

                    End If

                    If getConfigValueByType("ImportWieghtWiseCalculation").ToString.ToUpper = "TRUE" Then

                        'after configuration
                        r.Cells(enmGrdDetail.Per_Kg_Cost).Value = PerKG
                        r.Cells(enmGrdDetail.Weighted_Cost).Value = r.Cells(enmGrdDetail.Weight).Value * r.Cells(enmGrdDetail.Per_Kg_Cost).Value
                        r.Cells(enmGrdDetail.Other_Charges).Value = r.Cells(enmGrdDetail.Weighted_Cost).Value + r.Cells(enmGrdDetail.TotalAmount).Value
                        'end 

                    Else
                        'r.Cells(enmGrdDetail.Other_Charges).Value = Math.Round(((dblOtherCharges * ((Val(r.Cells(enmGrdDetail.TotalAmount).Value.ToString) + Val(r.Cells(enmGrdDetail.Duty).Value.ToString)) / dblTotalAmount)) / 100) * 100, 0)
                        ''TASK TFS1724 removed Math.Round function to show lc amount values with decimal points.
                        r.Cells(enmGrdDetail.Other_Charges).Value = (((dblOtherCharges * ((Val(r.Cells(enmGrdDetail.TotalAmount).Value.ToString) + Val(r.Cells(enmGrdDetail.Duty).Value.ToString) + Val(r.Cells(enmGrdDetail.AddCustomDuty).Value.ToString) + Val(r.Cells(enmGrdDetail.RegulatoryDuty).Value.ToString)) / dblTotalAmount)) / 100) * 100) ''TFS1956

                    End If

                    r.EndEdit()
                Next





                FillCombos("grdLocation")
                ApplyGridSettings("Detail")
            ElseIf Condition = "PODetail" Then
                Dim dtDetail As New DataTable
                dtDetail = New LCDAL().DisplayPODetail(PurchaseOrderId)
                dtDetail.AcceptChanges()
                'dtDetail.Columns("Price").Expression = "IIF(Unit='Loose', (([Exch_Rate]*[Import_Value])*[Qty]), (([Exch_Rate]*[Import_Value])*([Qty]*[Pack Qty])))"
                dtDetail.Columns("Price").Expression = "([Exch_Rate]*[Import_Value])"
                '' Commented below line for TASK-408 on 11-06-2016
                ''dtDetail.Columns("TotalAmount").Expression = "IIF(Unit='Loose', (Qty*Price),((Qty*[Pack Qty])*Price))"
                dtDetail.Columns("TotalAmount").Expression = "([TotalQty]*[Price])"
                dtDetail.Columns("Assd.Value").Expression = "(([Add.Cost%]/100)*([Insurrance]+[TotalAmount]))+[Insurrance]+[TotalAmount]"
                dtDetail.Columns("Duty").Expression = "(([Duty%]/100)*[Assd.Value])"
                dtDetail.Columns("AddCustomDuty").Expression = "(([AddCustomDuty%]/100)*[Assd.Value])" ''TFS1956
                dtDetail.Columns("RegulatoryDuty").Expression = "(([RegulatoryDuty%]/100)*[Assd.Value])" ''TFS1956
                dtDetail.Columns("SaleTax").Expression = "([S. Tax%]/100)*([Assd.Value]+[Duty]+[AddCustomDuty] +[RegulatoryDuty])" ''TFS1956
                dtDetail.Columns("Add.S.Tax").Expression = "([Add.S.Tax%]/100)*([Assd.Value]+[Duty]+[AddCustomDuty] +[RegulatoryDuty])" ''TFS1956
                dtDetail.Columns("Adv.I.Tax").Expression = "([Adv.I.Tax%]/100)*([Assd.Value]+[Duty]+[AddCustomDuty] +[RegulatoryDuty]+[SaleTax]+[Add.S.Tax])" ''TFS1956
                dtDetail.Columns("Net Amount").Expression = "(([Duty]+[AddCustomDuty] +[RegulatoryDuty]+[SaleTax]+[Add.S.Tax]+[Adv.I.Tax]))" ''TFS1956
                dtDetail.Columns("ExciseDuty").Expression = "(([ExciseDutyPercent]/100)*[Assd.Value])"

                dtDetail.AcceptChanges()
                Me.grdDetail.DataSource = dtDetail

                If dtDetail.Rows.Count > 0 Then
                    If IsDBNull(dtDetail.Rows.Item(0).Item("CurrencyId")) Or Val(dtDetail.Rows.Item(0).Item("CurrencyId").ToString) = 0 Then
                        'Me.cmbCurrency.SelectedValue = Nothing
                        'Me.cmbCurrency.Enabled = False
                    Else
                        'IsCurrencyEdit = True
                        'IsNotCurrencyRateToAll = True
                        FillCombos("Currency")
                        Me.cmbCurrency.SelectedValue = Val(dtDetail.Rows.Item(0).Item("CurrencyId").ToString)
                        'Me.cmbCurrency.Enabled = False
                    End If
                    'cmbCurrency_SelectedIndexChanged(Nothing, Nothing)
                End If
                Dim dblOtherCharges As Double = (Val(Me.txtExpenseByLC.Text))
                Dim dblInsurrance As Double = Val(Me.txtInsurrance.Text)
                Dim dblTotalAmount As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.Duty), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.AddCustomDuty), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.RegulatoryDuty), Janus.Windows.GridEX.AggregateFunction.Sum) ''TFS1956
                'If (dtDetail.Rows.Count > 0) Then
                '    If Not IsDBNull(dtDetail.Rows(0).Item("Per_Kg_Cost")) Then
                '        PerKG = dtDetail.Rows(0).Item("Per_Kg_Cost")
                '    Else
                '        PerKG = 1

                '    End If


                'End If
                Dim cost As Decimal
                Dim Totalweight As Decimal
                Dim WeightedCost As Decimal
                Dim TotalItemsAmount As Decimal

                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                    TotalItemsAmount = Val(Me.txtAmount.Text) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum)

                    cost = Val(Me.txtExpenseByLC.Text) - TotalItemsAmount
                    Totalweight = Val(Me.txtWeight.Text) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.Weight), Janus.Windows.GridEX.AggregateFunction.Sum) - 1
                    If Totalweight = 0 Then
                        Totalweight = 1
                    End If
                    PerKG = Math.Round(Val(Me.txtExpenseByLC.Text) / Totalweight, DecimalPointInValue)

                    r.BeginEdit()
                    r.Cells(enmGrdDetail.ImportAmount).Value = r.Cells(enmGrdDetail.Import_Value).Value * r.Cells(enmGrdDetail.TotalQty).Value
                    If (Val(r.Cells(enmGrdDetail.TotalAmount).Value) > 0) Then

                        r.Cells(enmGrdDetail.Insurrance).Value = Math.Round(((dblInsurrance * ((Val(r.Cells(enmGrdDetail.TotalAmount).Value) + Val(r.Cells(enmGrdDetail.Duty).Value.ToString) + Val(r.Cells(enmGrdDetail.AddCustomDuty).Value.ToString) + Val(r.Cells(enmGrdDetail.RegulatoryDuty).Value.ToString)) / dblTotalAmount)) / 100) * 100, 0) ''TFS1956

                    End If

                    If getConfigValueByType("ImportWieghtWiseCalculation").ToString.ToUpper = "TRUE" Then

                        'after configuration
                        r.Cells(enmGrdDetail.Per_Kg_Cost).Value = PerKG
                        r.Cells(enmGrdDetail.Weighted_Cost).Value = r.Cells(enmGrdDetail.Weight).Value * r.Cells(enmGrdDetail.Per_Kg_Cost).Value
                        r.Cells(enmGrdDetail.Other_Charges).Value = r.Cells(enmGrdDetail.Weighted_Cost).Value + r.Cells(enmGrdDetail.TotalAmount).Value
                        'end 

                    Else
                        r.Cells(enmGrdDetail.Other_Charges).Value = Math.Round(((dblOtherCharges * ((Val(r.Cells(enmGrdDetail.TotalAmount).Value.ToString) + Val(r.Cells(enmGrdDetail.Duty).Value.ToString) + Val(r.Cells(enmGrdDetail.AddCustomDuty).Value.ToString) + Val(r.Cells(enmGrdDetail.RegulatoryDuty).Value.ToString)) / dblTotalAmount)) / 100) * 100, 0) ''TFS1956
                    End If

                    r.EndEdit()
                Next
                FillCombos("grdLocation")
                ApplyGridSettings("PODetail")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub EditRecord(Optional ByVal Condition As String = "")
        Try

            If Me.grdHistory.RowCount = 0 Then Exit Sub
            If Me.grdDetail.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.btnSave.Text = "&Update"
            FillCombos("LC")
            LCId = Me.grdHistory.GetRow.Cells("LCID").Value
            PurchaseOrderId = Val(Me.grdHistory.GetRow.Cells("PurchaseOrderId").Value.ToString)
            Dim intVoucherId As Integer = 0I

            Me.txtLCNo.Text = Me.grdHistory.GetRow.Cells("LCNo").Value.ToString
            Me.dtpLCDate.Value = Me.grdHistory.GetRow.Cells("LCDate").Value
            Me.cmbLCAccount.Value = Val(Me.grdHistory.GetRow.Cells("LCAccountId").Value.ToString)
            'Me.cmbLCExpAccount.Value = Val(Me.grdHistory.GetRow.Cells("LCExpenseAccountId").Value.ToString)
            Me.txtImportName.Text = Me.grdHistory.GetRow.Cells("ImportName").Value.ToString

            Me.txtInsurrance.Text = Val(Me.grdHistory.GetRow.Cells("Insurrance").Value.ToString)
            Me.txtExchangeRate.Text = Val(Me.grdHistory.GetRow.Cells("ExchangeRate").Value.ToString)
            'Me.txtBankPaidAmount.Text = Val(Me.grdHistory.GetRow.Cells("BankPaidAmount").Value.ToString)
            RemoveHandler txtCMCC.TextChanged, AddressOf Me.txtCMCC_TextChanged
            Me.txtAdjDDForCMCC.Text = Val(Me.grdHistory.GetRow.Cells("Adj CMCC").Value.ToString)
            Me.txtAdjDDETO.Text = Val(Me.grdHistory.GetRow.Cells("Adj ETO").Value.ToString)
            Me.txtCMCC.Text = Val(Me.grdHistory.GetRow.Cells("DDForCMCC").Value.ToString)

            Me.txtExpenseByLC.Text = Val(Me.grdHistory.GetRow.Cells("ExpenseByLC").Value.ToString)

            Me.cmbProject.SelectedValue = Val(Me.grdHistory.GetRow.Cells("CostCenterId").Value.ToString)
            Me.txtExciseDutyPercent.Text = Val(Me.grdHistory.GetRow.Cells("Excise Duty%").Value.ToString)
            Me.txtExciseDuty.Text = Val(Me.grdHistory.GetRow.Cells("ExciseDuty").Value.ToString)
            Me.btnAttachment.Text = "Attachment (" & Val(Me.grdHistory.GetRow.Cells("No Of Attachment").Value.ToString) & ")"

            Me.cmbLC.SelectedValue = Val(Me.grdHistory.GetRow.Cells("LCDocID").Value.ToString)
            Me.cmbGRN.SelectedValue = Val(Me.grdHistory.GetRow.Cells("ReceivingNoteId").Value.ToString)
            Me.chkFinancialImpact.Checked = Me.grdHistory.GetRow.Cells("Financial_Impact").Value
            Me.cmbStatus.Text = Me.grdHistory.GetRow.Cells("Status").Value.ToString
            cmbLCAccount.Enabled = False
            Me.cmbLC.Enabled = False
            Me.cmbProject.Enabled = False
            GetAllRecords("Detail")
            GetCurrencyRate()
            ApplyInsurrance()
            GetTotal()
            ApplySecurity(Utility.EnumDataMode.Edit)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            ' ''This check is added to validate that the LC must exist before some import is being made against it.
            'Const LC_Req As Boolean = True
            'If LC_Req Then
            '    If Me.cmbLC.SelectedIndex = 0 Then
            '        ShowErrorMessage("Select LC .")
            '        Me.cmbLC.Focus()
            '        Return False
            '    End If
            'End If
            If Me.cmbLCAccount.ActiveRow Is Nothing Then Return False
            If Me.cmbLCAccount.Value = 0 Then
                ShowErrorMessage("Select LC Account.")
                Me.cmbLCAccount.Focus()
                Return False
            End If
            If Me.cmbProject.SelectedIndex <= 0 Then
                ShowErrorMessage("Please Select Project")
                Me.cmbProject.Focus()
                Return False
            End If
            If Me.grdDetail.RowCount = 0 Then
                ShowErrorMessage("No record exists in grid")
                Return False
            End If
            ''Start TFS4163
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                If StockInConfigration.Equals("Required") AndAlso r.Cells(enmGrdDetail.BatchNo).Value.ToString = String.Empty Then
                    msg_Error("Please Enter Value in Batch No")
                    Return False
                    Exit For
                End If
            Next
            ''End TFS4163
            'If Me.cmbLCExpAccount.ActiveRow Is Nothing Then Return False
            'If Me.cmbLCExpAccount.Value = 0 Then
            '    ShowErrorMessage("Select LC Expense Account")
            '    Me.cmbLCExpAccount.Focus()
            '    Return False
            'End If
            'If Me.txtImportName.Text = String.Empty Then
            '    ShowErrorMessage("Enter Import Name.")
            '    Me.txtImportName.Focus()
            '    Return False
            'End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            If Condition = String.Empty Then
                IsFromItemWiseLC = False
                LCId = 0I
                PurchaseOrderId = 0I
                Me.btnSave.Text = "&Save"
                Me.txtLCNo.Text = GetNextDoc().ToString
                LoadAllStatus = String.Empty
                FillCombos("PortDischarge")
                FillCombos("PortLoading")
                FillCombos("ClearingAgent")
                FillCombos("LC")
                FillCombos("Project")
                If Not Me.cmbLC.SelectedIndex = -1 Then Me.cmbLC.SelectedIndex = 0
                Me.cmbLC.Enabled = True
                If Not Me.cmbLCAccount.ActiveRow Is Nothing Then Me.cmbLCAccount.Rows(0).Activate()
                'If Not Me.cmbLCExpAccount.ActiveRow Is Nothing Then Me.cmbLCExpAccount.Rows(0).Activate()
                Me.txtImportName.Text = String.Empty
                If Me.cmbItem.ActiveRow Is Nothing Then
                    cmbItem.Rows(0).Activate()
                Else
                    cmbItem.Rows(0).Activate()
                End If
                If Not Me.cmbUnit.SelectedIndex = -1 Then Me.cmbUnit.SelectedIndex = 0 ''TFS4161
                'Me.txtPerformaNo.Text = String.Empty
                'Me.dtpPerformaDate.Value = Date.Now
                'Me.cmbPaymentMethod.SelectedIndex = 0
                'Me.dtpLCBeforeDate.Value = Date.Now
                'Me.cmbPortOfDischarge.Text = String.Empty
                'Me.cmbPortOfLoading.Text = String.Empty
                'If Not Me.cmbSupplierAccount.ActiveRow Is Nothing Then Me.cmbSupplierAccount.Rows(0).Activate()
                'If Not Me.cmbIndenterAccount.ActiveRow Is Nothing Then Me.cmbIndenterAccount.Rows(0).Activate()
                'Me.chkTranship.Checked = True
                'Me.dtpLatestDateofShipment.Value = Date.Now
                'Me.dtpLSBDate.Value = Date.Now
                Me.txtImportValue.Text = String.Empty
                Me.cmbStatus.Text = String.Empty
                Me.txtInsurrance.Text = String.Empty
                Me.txtExchangeRate.Text = String.Empty
                'Me.txtBankPaidAmount.Text = String.Empty
                Me.txtCMCC.Text = String.Empty
                Me.txtAdjDDForCMCC.Text = String.Empty
                Me.txtETO.Text = String.Empty
                Me.txtAdjDDETO.Text = String.Empty
                'Me.txtShippingCharges.Text = String.Empty
                'Me.txtPortCharges.Text = String.Empty
                Me.txtTotalAmount.Text = String.Empty
                Me.txtExciseDutyPercent.Text = String.Empty
                Me.txtExciseDuty.Text = String.Empty
                cmbLCAccount.Enabled = True
                Me.cmbProject.Enabled = True
                'Me.txtRefNo.Text = String.Empty
                'Me.dtpOpeningDate.Value = Date.Now
                'Me.dtpExpiryDate.Value = Date.Now
                'Me.txtAmendment.Text = String.Empty
                'If Not Me.cmbOpeningBank.ActiveRow Is Nothing Then Me.cmbOpeningBank.Rows(0).Activate()
                'Me.txtAdvisingBank.Text = String.Empty
                'Me.txtSpecialInstruction.Text = String.Empty
                'Me.txtRemarks.Text = String.Empty
                'If Not Me.cmbOpenedBy.SelectedIndex = -1 Then Me.cmbOpenedBy.SelectedIndex = 0
                'Me.txtVessel.Text = String.Empty
                'Me.txtBLNo.Text = String.Empty
                'Me.dtpBLDate.Value = Date.Now
                'Me.dtpETDDate.Value = Date.Now
                'Me.dtpETADate.Value = Date.Now
                'Me.dtpBankDocumentDate.Value = Date.Now
                'Me.dtpBankPaymentDate.Value = Date.Now
                'Me.cmbClearingAgent.Text = String.Empty
                'If Not Me.cmbTransporter.SelectedIndex = -1 Then Me.cmbTransporter.SelectedIndex = 0
                Me.txtPackQty.Text = String.Empty
                Me.txtQty.Text = String.Empty
                Me.txtPrice.Text = String.Empty
                Me.txtTotalAmount.Text = String.Empty
                Me.txtAddCostPercent.Text = String.Empty
                Me.txtAssessedValue.Text = String.Empty
                Me.txtDutyPercent.Text = String.Empty
                Me.txtDuty.Text = String.Empty
                Me.txtAddCustomDutyPercent.Text = String.Empty ''TFS1956
                Me.txtAddCustomDuty.Text = String.Empty ''TFS1956
                Me.txtRegulatoryDutyPercent.Text = String.Empty ''TFS1956
                Me.txtRegulatoryDuty.Text = String.Empty ''TFS1956
                Me.txtSalesTaxPercent.Text = String.Empty
                Me.txtSalesTax.Text = String.Empty
                Me.txtAddSalesTaxPercent.Text = String.Empty
                Me.txtAddSalesTax.Text = String.Empty
                Me.txtAdvIncomeTaxPercent.Text = String.Empty
                Me.txtAdvIncomeTax.Text = String.Empty
                Me.txtNetAmount.Text = String.Empty
                Me.txtExpenseByLC.Text = String.Empty
                Me.txtOtherCharges.Text = String.Empty
                Me.txtShippingCharges.Text = String.Empty
                Me.txtPortCharges.Text = String.Empty
                Me.txtWeight.Text = "1"
                Me.txtShippingRemarks.Text = String.Empty
                Me.txtPortRemarks.Text = String.Empty
                Me.txtCMCCRemarks.Text = String.Empty
                Me.txtETORemarks.Text = String.Empty
                Me.chkFinancialImpact.Checked = False
                Me.IsFromItemWiseLC = False
                AddHandler txtCMCC.TextChanged, AddressOf Me.txtCMCC_TextChanged
                If Not Me.cmbProject.SelectedIndex = -1 Then Me.cmbProject.SelectedIndex = 0
                Array.Clear(arrFile, 0, arrFile.Length)
                Me.btnAttachment.Text = "Attachment"
                GetAllRecords("Master")
                GetAllRecords("Detail")
                ApplySecurity(Utility.EnumDataMode.[New])
                Me.dtpLCDate.Focus()
                SetBGColorTextBox()
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Else

                Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
                Me.cmbItem.Focus()
                Me.txtPackQty.Text = String.Empty
                Me.txtQty.Text = String.Empty
                Me.txtPrice.Text = String.Empty
                Me.txtAddCostPercent.Text = String.Empty
                Me.txtAssessedValue.Text = String.Empty
                Me.txtDutyPercent.Text = String.Empty
                Me.txtDuty.Text = String.Empty
                Me.txtAddCustomDutyPercent.Text = String.Empty ''TFS1956
                Me.txtAddCustomDuty.Text = String.Empty ''TFS1956
                Me.txtRegulatoryDutyPercent.Text = String.Empty ''TFS1956
                Me.txtRegulatoryDuty.Text = String.Empty ''TFS1956
                Me.txtSalesTaxPercent.Text = String.Empty
                Me.txtSalesTax.Text = String.Empty
                Me.txtWeight.Text = "1"
                Me.txtAddSalesTaxPercent.Text = String.Empty
                Me.txtAddSalesTax.Text = String.Empty
                Me.txtAdvIncomeTaxPercent.Text = String.Empty
                Me.txtAdvIncomeTax.Text = String.Empty
                Me.txtNetAmount.Text = String.Empty
                Me.txtImportValue.Text = String.Empty
                Me.txtTotalQuantity.Text = String.Empty
            End If
            NewVoucherExpected = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New LCDAL().Add(LC) = True Then
                If Con.State = ConnectionState.Closed Then Con.Open()
                Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
                Try
                    SaveDocument(LC.LCID, Me.Name, trans)
                Catch ex As Exception
                    trans.Rollback()
                Finally
                    Con.Close()
                End Try
                Return True
            Else
                Return False
            End If
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
            If New LCDAL().Update(LC) = True Then
                If Con.State = ConnectionState.Closed Then Con.Open()
                Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
                Try
                    SaveDocument(LC.LCID, Me.Name, trans)
                    trans.Commit()
                Catch ex As Exception
                    trans.Rollback()
                Finally
                    Con.Close()
                End Try
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmImport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                Me.btnLoadAll.Visible = True
                Me.btnRefresh.Visible = False
                Me.btnSave.Visible = False
                CtrlGrdBar2.Visible = True
                CtrlGrdBar1.Visible = False
            Else
                Me.btnLoadAll.Visible = False
                Me.btnRefresh.Visible = True
                Me.btnSave.Visible = True
                CtrlGrdBar2.Visible = False
                CtrlGrdBar1.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbItem_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbItem.KeyDown
        Try
            ''TFS1858 : Ayesha Rehman :Item dropdown shall be searchable
            If e.KeyCode = Keys.F1 Then

                frmItemSearch.CompanyId = 0
                frmItemSearch.LocationId = 0


                frmItemSearch.ModelId = 0
                frmItemSearch.VendorId = 0


                frmItemSearch.BringToFront()
                frmItemSearch.ShowDialog()
                If frmItemSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbItem.Value = frmItemSearch.ArticleId
                    'txtQty.Text = frmItemSearch.Qty
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
        Try
            If cmbItem.IsItemInList = False Then Exit Sub
            If cmbItem.ActiveRow IsNot Nothing Then
                GetLastTaxPercentage(Me.cmbItem.Value)
                If Not cmbGRN.SelectedIndex = -1 AndAlso Me.cmbGRN.SelectedValue > 0 Then
                    Me.txtQty.Text = Val(Me.cmbItem.ActiveRow.Cells("Qty").Value.ToString)
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.ValueChanged
        Try

            If IsOpenedForm = False Then Exit Sub
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            FillCombos("Pack")


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.cmbUnit.Text = "Loose" Then
                Me.txtPackQty.Text = 1
                Me.txtPackQty.Enabled = False
                Me.txtTotalQuantity.Enabled = False
            Else
                Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Row.Item("PackQty").ToString)
                ''Start TFS4161
                If IsPackQtyDisabled = True Then
                    Me.txtPackQty.Enabled = False
                    Me.txtTotalQuantity.Enabled = False
                Else
                    Me.txtPackQty.Enabled = True
                    Me.txtTotalQuantity.Enabled = True
                End If
                ''End TFS4161
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmImport_ResizeBegin(sender As Object, e As EventArgs) Handles Me.ResizeBegin

    End Sub

    Private Sub frmImport_Scroll(sender As Object, e As ScrollEventArgs) Handles Me.Scroll

    End Sub

    Private Sub frmImport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Cursor = Cursors.WaitCursor
        Try
            IsOpenedForm = True
            BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)
            'Me.cmbCurrency.SelectedValue = Me.BaseCurrencyId

            FillCombos("LCAccount")
            FillCombos("LCExpAccount")
            FillCombos("SupplierAccount")
            FillCombos("IndenterAccount")
            FillCombos("OpeningBank")
            FillCombos("OpenedBy")
            FillCombos("Transporter")
            FillCombos("Location")
            FillCombos("Item")
            FillCombos("Pack")
            FillCombos("Project")
            FillCombos("Currency")
            FillCombos("Status")

            ''Start TFS4163
            If Not getConfigValueByType("StockInConfigration").ToString = "Error" Then ''1596
                StockInConfigration = getConfigValueByType("StockInConfigration").ToString
            End If
            ''End TFS4163
            ''start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = System.Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161
            'ROWStyle.ForeColor = Color.LightYellow
            If IsFromItemWiseLC = False Then
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Function IsFormValidate() As Boolean
        Try
            If Me.cmbLocation.SelectedIndex < 0 Then
                ShowErrorMessage("Select Location")
                Me.cmbLocation.Focus()
                Return False
            End If

            If Me.cmbItem.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Select Item")
                Me.cmbItem.Focus()
                Return False
            End If

            If Me.cmbUnit.SelectedIndex < 0 Then
                ShowErrorMessage("Select Unit")
                Me.cmbUnit.Focus()
                Return False
            End If
            If Val(Me.txtQty.Text) <= 0 Then
                ShowErrorMessage("Please Enter Qty")
                Me.txtQty.Focus()
                Return False
            End If
            If Val(Me.txtPrice.Text) <= 0 Then
                ShowErrorMessage("Please Enter Price")
                Me.txtPrice.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If IsFormValidate() = False Then Exit Sub

            Dim dtData As DataTable = CType(Me.grdDetail.DataSource, DataTable)
            dtData.AcceptChanges()

            Dim cost As Decimal
            Dim Totalweight As Decimal
            Dim WeightedCost As Decimal
            Dim TotalItemsAmount As Decimal

            Dim dr As DataRow
            dr = dtData.NewRow


            dr(enmGrdDetail.LocationId) = Me.cmbLocation.SelectedValue
            dr(enmGrdDetail.ArticleDefId) = Me.cmbItem.Value
            dr(enmGrdDetail.Code) = Me.cmbItem.ActiveRow.Cells("Code").Value.ToString
            dr(enmGrdDetail.Item) = Me.cmbItem.ActiveRow.Cells("Item").Value.ToString
            dr(enmGrdDetail.HS_Code) = Me.cmbItem.ActiveRow.Cells("HS Code").Value.ToString
            dr(enmGrdDetail.Origin) = Me.cmbItem.ActiveRow.Cells("Origin").Value.ToString
            dr(enmGrdDetail.Color) = Me.cmbItem.ActiveRow.Cells("Color").Value.ToString
            dr(enmGrdDetail.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value.ToString
            dr(enmGrdDetail.PackDesc) = Me.cmbUnit.Text
            dr(enmGrdDetail.Unit) = IIf(Me.cmbUnit.Text = "Loose", "Loose", "Pack")
            ''Start TFS4163
            If StockInConfigration.Equals("Required") Then
                dr(enmGrdDetail.BatchNo) = String.Empty
            Else
                dr(enmGrdDetail.BatchNo) = "xxxx"
            End If
            dr(enmGrdDetail.ExpiryDate) = System.Convert.ToDateTime(Date.Now.AddMonths(1))
            ''End TFS4163
            dr(enmGrdDetail.Qty) = Val(Me.txtQty.Text)
            dr(enmGrdDetail.Weight) = Val(Me.txtWeight.Text)
            TotalItemsAmount = Val(Me.txtAmount.Text) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum)

            cost = Val(Me.txtExpenseByLC.Text) - TotalItemsAmount
            Totalweight = Val(Me.txtWeight.Text) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.Weight), Janus.Windows.GridEX.AggregateFunction.Sum)
            PerKG = Math.Round(Val(Me.txtExpenseByLC.Text) / Totalweight, DecimalPointInValue)


            dr(enmGrdDetail.Per_Kg_Cost) = Val(Me.txtExpenseByLC.Text) / Totalweight
            WeightedCost = PerKG * Val(Me.txtWeight.Text)

            dr(enmGrdDetail.Weighted_Cost) = WeightedCost

            dr(enmGrdDetail.Other_Charges) = WeightedCost + Val(Me.txtAmount.Text)
            ''TASK TFS1609
            dr(enmGrdDetail.CheckOtherCharges) = 0
            dr(enmGrdDetail.LCDetailId) = 0
            ''END TASK TFS1609

            ' dr(enmGrdDetail.TotalAmount) = WeightedCost + Val(Me.txtAmount.Text)
            dr(enmGrdDetail.ImportAmount) = Val(Me.txtTotalQuantity.Text) * Val(Me.txtImportValue.Text)


            dr(enmGrdDetail.PackQty) = Val(Me.txtPackQty.Text)

            dr(enmGrdDetail.Price) = Val(Me.txtPrice.Text)
            dr(enmGrdDetail.AddCostPercent) = Val(Me.txtAddCostPercent.Text)
            'dr(enmGrdDetail.AssessedValue) = Val(Me.txtAssessedValue.Text)
            dr(enmGrdDetail.DutyPercent) = Val(Me.txtDutyPercent.Text)
            dr(enmGrdDetail.Duty) = Val(Me.txtDuty.Text)
            dr(enmGrdDetail.AddCustomDutyPercent) = Val(Me.txtAddCustomDutyPercent.Text) ''TFS1956
            dr(enmGrdDetail.AddCustomDuty) = Val(Me.txtAddCustomDuty.Text) ''TFS1956
            dr(enmGrdDetail.RegulatoryDutyPercent) = Val(Me.txtRegulatoryDutyPercent.Text) ''TFS1956
            dr(enmGrdDetail.RegulatoryDuty) = Val(Me.txtRegulatoryDuty.Text) ''TFS1956
            dr(enmGrdDetail.SaleTaxPercent) = Val(Me.txtSalesTaxPercent.Text)
            'dr(enmGrdDetail.SaleTax) = Val(Me.txtSalesTax.Text)
            dr(enmGrdDetail.AddSaleTaxPercent) = Val(Me.txtAddSalesTaxPercent.Text)
            'dr(enmGrdDetail.AddSaleTax) = Val(Me.txtAddSalesTax.Text)
            dr(enmGrdDetail.AdvIncomeTaxPercent) = Val(Me.txtAdvIncomeTaxPercent.Text)
            'dr(enmGrdDetail.AdvIncomeTax) = Val(Me.txtAdvIncomeTax.Text)
            dr(enmGrdDetail.Comments) = String.Empty
            dr(enmGrdDetail.PurchaseAccountId) = Val(Me.cmbItem.ActiveRow.Cells("PurchaseAccountId").Value.ToString)
            dr(enmGrdDetail.Exch_Rate) = Val(Me.txtExchangeRate.Text)
            dr(enmGrdDetail.Import_Value) = Val(Me.txtImportValue.Text)
            dr(enmGrdDetail.ExciseDutyPercent) = Val(Me.txtExciseDutyPercent.Text)
            dr(enmGrdDetail.TotalQty) = Val(Me.txtTotalQuantity.Text) ''TASK-408 on 11-06-2016

            ''TASK TFS1753
            dr(enmGrdDetail.ReceivingNoteId) = IIf(Me.cmbGRN.SelectedValue = Nothing, 0, Me.cmbGRN.SelectedValue)
            ''END TASK TFS1753

            ''dr.Item(EnumGrid.CurrencyAmount) = IIf(Me.txtDebit.Text = 0, Val(Me.txtCredit.Text), Val(Me.txtDebit.Text))
            'dr.Item(enmGrdDetail.CurrencyDr) = Val(Me.txtDebit.Text)
            'dr.Item(enmGrdDetail.CurrencyCr) = Val(Me.txtCredit.Text)

            'dr.Item(enmGrdDetail.Exch_Rate) = Val(Me.txtCurrencyRate.Text)
            'Dim ConfigCurrencyVal As String = getConfigValueByType("Currency").ToString
            'If ConfigCurrencyVal.Length > 0 AndAlso Not ConfigCurrencyVal.ToString.ToUpper = "ERROR" Then
            '    dr.Item(enmGrdDetail.BaseCurrencyId) = Val(ConfigCurrencyVal)
            '    dr.Item(enmGrdDetail.BaseCurrencyRate) = Val(GetCurrencyRate(Val(ConfigCurrencyVal)))
            'End If


            dtData.Rows.Add(dr)
            dtData.AcceptChanges()
            ApplyInsurrance()
            GetTotal()
            ReSetControls("1=1")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    Dim flg As Boolean = False
                    'If chkFinancialImpact.Checked = True Then
                    '    Dim DirectVoucherPrinting As Boolean
                    '    If Not getConfigValueByType("DirectVoucherPrinting").ToString = "Error" Then
                    '        DirectVoucherPrinting = CBool(getConfigValueByType("DirectVoucherPrinting").ToString)
                    '    End If
                    '    If DirectVoucherPrinting = True Then
                    '        'If msg_Confirm("Financial Record Exist. Do you want to proceed to update this record. ?") = False Then flg = True : Exit Sub
                    '        If msg_Confirm2("Do you want to print voucher?", False, DirectVoucherPrinting, True, Me.txtLCNo.Text, True, False, Me.txtLCNo.Text) = False Then flg = True : Exit Sub
                    '        LC.VoucherRemarks = frmImportPopup.VoucherReference
                    '        LC.CurrentVoucherNo = frmImportPopup.VoucherNo
                    '        LC.NewVoucherNo = ""
                    '        LC.IsNewVoucher = frmImportPopup.IsNewVoucher
                    '        LC.VoucherDate = frmImportPopup.VoucherDate
                    '        LC.FirstVoucher = True
                    '    End If
                    'End If
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes

                    If chkFinancialImpact.Checked = True Then
                        '' Made changes to  against TASK TFS1462 on 13-09-2017
                        Dim Printing As Boolean = False
                        'Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        Dim DirectVoucherPrinting As Boolean
                        If Not getConfigValueByType("DirectVoucherPrinting").ToString = "Error" Then
                            DirectVoucherPrinting = CBool(getConfigValueByType("DirectVoucherPrinting").ToString)
                        End If
                        If Printing = True Or DirectVoucherPrinting = True Then
                            If msg_Confirm("Do you want to print", Printing, DirectVoucherPrinting) = True Then
                                Dim Print1 As Boolean = frmMessages.Print
                                Dim PrintVoucher As Boolean = frmMessages.DirectVoucherPrinting
                                'If Print1 = True Then
                                '    Me.PurchaseReturnToolStripMenuItem_Click(Nothing, Nothing)
                                'End If
                                If PrintVoucher = True Then
                                    GetVoucherPrint(Me.txtLCNo.Text, Me.Name, "PKR", 1, True)
                                End If
                            End If
                        End If
                    End If
                    ''END TASK TFS1462
                    'Dim PrintVoucher As Boolean = frmImportPopup.DirectVoucherPrinting

                    'If PrintVoucher = True AndAlso chkFinancialImpact.Checked = True Then
                    '    GetVoucherPrint(LC.CurrentVoucherNo, "frmImport", BaseCurrencyName, BaseCurrencyId, True)
                    'End If
                    ReSetControls()
                Else
                    Dim flg As Boolean = False
                    If grdHistory.RowCount = 0 Then Exit Sub
                    If Not IsDBNull(Me.grdHistory.GetRow.Cells("Financial_Impact").Value) Then
                        If Me.grdHistory.GetRow.Cells("Financial_Impact").Value = True Then
                            Dim DirectVoucherPrinting As Boolean
                            DirectVoucherPrinting = CBool(getConfigValueByType("DirectVoucherPrinting").ToString)
                            'If msg_Confirm("Financial Record Exist. Do you want to proceed to update this record. ?") = False Then flg = True : Exit Sub
                            Dim VoucherNo As String = ""
                            Dim VoucherReference As String = ""
                            'If NewVoucherExpected = True Then
                            Dim lcDAL As New LCDAL()
                            VoucherNo = lcDAL.GetCurrentVoucherNo(Me.txtLCNo.Text)
                            VoucherReference = lcDAL.GetVoucherReference(VoucherNo)
                            'Else
                            '    VoucherNo = txtLCNo.Text
                            'End If
                            If Me.chkFinancialImpact.Checked = False Then
                                If msg_Confirm("Financial Record Exist. Do you want to proceed to update this record. ?") = False Then flg = True : Exit Sub
                                LC.CurrentVoucherNo = VoucherNo
                                LC.VoucherRemarks = String.Empty
                                LC.NewVoucherNo = String.Empty
                                LC.IsNewVoucher = False
                                LC.VoucherDate = Date.Now
                                LC.FirstVoucher = False
                            Else
                                If msg_Confirm2("Financial Record Exist. Do you want to update this record ?", False, DirectVoucherPrinting, chkFinancialImpact.Checked, VoucherNo, False, NewVoucherExpected, Me.txtLCNo.Text, VoucherReference) = False Then flg = True : Exit Sub
                                LC.CurrentVoucherNo = VoucherNo
                                LC.VoucherRemarks = frmImportPopup.VoucherReference
                                LC.NewVoucherNo = frmImportPopup.VoucherNo
                                LC.IsNewVoucher = frmImportPopup.IsNewVoucher
                                LC.VoucherDate = frmImportPopup.VoucherDate
                                LC.FirstVoucher = False
                            End If
                            'Else
                            '    'Dim flg As Boolean = False
                            '    If chkFinancialImpact.Checked = True Then
                            '        Dim DirectVoucherPrinting As Boolean
                            '        DirectVoucherPrinting = CBool(getConfigValueByType("DirectVoucherPrinting").ToString)
                            'If msg_Confirm("Financial Record Exist. Do you want to proceed to update this record. ?") = False Then flg = True : Exit Sub
                            '        If msg_Confirm2("Do you want to save record with financial impact?", False, DirectVoucherPrinting, True, Me.txtLCNo.Text, True, False, Me.txtLCNo.Text) = False Then flg = True : Exit Sub
                            '        LC.VoucherRemarks = frmImportPopup.VoucherReference
                            '        LC.CurrentVoucherNo = frmImportPopup.VoucherNo
                            '        LC.NewVoucherNo = ""
                            '        LC.IsNewVoucher = frmImportPopup.IsNewVoucher
                            '        LC.VoucherDate = frmImportPopup.VoucherDate
                            '        LC.FirstVoucher = True

                            '    End If
                        End If
                    End If
                    If IsCostCentreReshuffled(Me.cmbProject.SelectedValue) Then
                        ShowErrorMessage("Update does not allow as cost centre has been shifted.")
                        Exit Sub
                    End If
                    'If flg = False Then If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    'Dim PrintVoucher As Boolean = frmImportPopup.DirectVoucherPrinting
                    If chkFinancialImpact.Checked = True AndAlso Me.grdHistory.GetRow.Cells("Financial_Impact").Value = False Then
                        '' Made changes to  against TASK TFS1462 on 13-09-2017
                        Dim Printing As Boolean = False
                        'Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        Dim DirectVoucherPrinting As Boolean
                        If Not getConfigValueByType("DirectVoucherPrinting").ToString = "Error" Then
                            DirectVoucherPrinting = CBool(getConfigValueByType("DirectVoucherPrinting").ToString)
                        End If
                        If Printing = True Or DirectVoucherPrinting = True Then
                            If msg_Confirm("Do you want to print", Printing, DirectVoucherPrinting) = True Then
                                Dim Print1 As Boolean = frmMessages.Print
                                Dim PrintVoucher As Boolean = frmMessages.DirectVoucherPrinting
                                'If Print1 = True Then
                                '    Me.PurchaseReturnToolStripMenuItem_Click(Nothing, Nothing)
                                'End If
                                If PrintVoucher = True Then
                                    GetVoucherPrint(Me.txtLCNo.Text, Me.Name, "PKR", 1, True)
                                End If
                            End If
                        End If
                        ''END TASK TFS1462

                    End If
                    If chkFinancialImpact.Checked = True AndAlso Me.grdHistory.GetRow.Cells("Financial_Impact").Value = True Then
                        If LC.IsNewVoucher = True Then
                            GetVoucherPrint(LC.NewVoucherNo, "frmImport", BaseCurrencyName, BaseCurrencyId, True)
                        Else
                            GetVoucherPrint(LC.CurrentVoucherNo, "frmImport", BaseCurrencyName, BaseCurrencyId, True)
                        End If
                    End If
                    ReSetControls()
                End If
            Else
                Exit Sub
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grdHistory.RowCount = 0 Then Exit Sub
            Dim flg As Boolean = False
            If Not IsDBNull(Me.grdHistory.GetRow.Cells("Financial_Impact").Value) Then
                If Me.grdHistory.GetRow.Cells("Financial_Impact").Value = True Then
                    If msg_Confirm("Financial Record Exist. Do you want to proceed to delete this record. ?") = False Then flg = True : Exit Sub
                End If
            End If
            If IsCostCentreReshuffled(Me.cmbProject.SelectedValue) Then
                ShowErrorMessage("Update does not allow as cost centre has been shifted.")
                Exit Sub
            End If
            LC = New LCBE
            LCId = Me.grdHistory.GetRow.Cells("LCID").Value
            LC.LCID = LCId
            LC.LCNo = Me.grdHistory.GetRow.Cells("LCNo").Value
            LC.LCDate = Me.grdHistory.GetRow.Cells("LCDate").Value
            LC.PurchaseOrderId = PurchaseOrderId
            LC.ActivityLog = New ActivityLog
            With LC.ActivityLog
                .UserID = LoginUserId
                .User_Name = LoginUserName
                .Source = Me.Name
                .LogDateTime = Date.Now
                .FormCaption = Me.Text
            End With

            'If flg = False Then If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetNextDoc() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("LC" + "-" + Microsoft.VisualBasic.Right(Me.dtpLCDate.Value.Year, 2) + "-", "LCMasterTable", "LCNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("LC-" & Format(Me.dtpLCDate.Value, "yy") & Me.dtpLCDate.Value.Month.ToString("00"), 4, "LCMasterTable", "LCNo")
            Else
                Return GetNextDocNo("LC", 6, "LCMasterTable", "LCNo")
            End If

            'Dim strDocNo As String = String.Empty
            'If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
            '    Return GetNextDocNo("LC-" & Me.dtpLCDate.Value.ToString("yy"), 5, "LCMasterTable", "LCNo")
            'ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
            '    Return GetNextDocNo("LC-" & Me.dtpLCDate.Value.ToString("yyMM"), 5, "LCMasterTable", "LCNo")
            'Else
            '    Return GetNextDocNo("LC", 5, "LCMasterTable", "LCNo")
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ApplyInsurrance(Optional ByVal Condition As String = "")
        Try
            Dim cost As Decimal
            Dim Totalweight As Decimal
            Dim WeightedCost As Decimal
            Dim TotalItemsAmount As Decimal
            If Me.grdDetail.RootTable Is Nothing Then Exit Sub
            Me.grdDetail.UpdateData()
            Dim dblInsurrance As Double = Val(Me.txtInsurrance.Text)
            'Dim dblOtherCharges As Double = (Val(Me.txtShippingCharges.Text) + Val(Me.txtPortCharges.Text) + Val(Me.txtOtherCharges.Text) + Val(Me.txtExpenseByLC.Text))
            Dim dblOtherCharges As Double = (Val(Me.txtExpenseByLC.Text))
            Dim dblTotalAmount As Double = Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.Duty), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.AddCustomDuty), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.RegulatoryDuty), Janus.Windows.GridEX.AggregateFunction.Sum) ''TFS1956
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
                r.BeginEdit()
                cost = Val(Me.txtExpenseByLC.Text) - TotalItemsAmount
                Totalweight = Val(Me.txtWeight.Text) + Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.Weight), Janus.Windows.GridEX.AggregateFunction.Sum) - 1
                If Totalweight = 0 Then
                    Totalweight = 1
                End If
                PerKG = Math.Round(Val(Me.txtExpenseByLC.Text) / Totalweight, DecimalPointInValue)
                r.Cells(enmGrdDetail.ImportAmount).Value = r.Cells(enmGrdDetail.Import_Value).Value * r.Cells(enmGrdDetail.TotalQty).Value
                If (Val(r.Cells(enmGrdDetail.TotalAmount).Value) > 0) Then

                    r.Cells(enmGrdDetail.Insurrance).Value = Math.Round(((dblInsurrance * ((Val(r.Cells(enmGrdDetail.TotalAmount).Value) + Val(r.Cells(enmGrdDetail.Duty).Value.ToString) + Val(r.Cells(enmGrdDetail.AddCustomDuty).Value.ToString) + Val(r.Cells(enmGrdDetail.RegulatoryDuty).Value.ToString)) / dblTotalAmount)) / 100) * 100, 0) ''TFS1956

                End If

                If getConfigValueByType("ImportWieghtWiseCalculation").ToString.ToUpper = "TRUE" Then

                    'after configuration
                    r.Cells(enmGrdDetail.Per_Kg_Cost).Value = PerKG
                    r.Cells(enmGrdDetail.Weighted_Cost).Value = r.Cells(enmGrdDetail.Weight).Value * r.Cells(enmGrdDetail.Per_Kg_Cost).Value
                    r.Cells(enmGrdDetail.Other_Charges).Value = r.Cells(enmGrdDetail.Weighted_Cost).Value + r.Cells(enmGrdDetail.TotalAmount).Value
                    'end 

                Else
                    ''Commented below line against TASK TFS1719
                    'r.Cells(enmGrdDetail.Other_Charges).Value = Math.Round(((dblOtherCharges * ((Val(r.Cells(enmGrdDetail.TotalAmount).Value.ToString) + Val(r.Cells(enmGrdDetail.Duty).Value.ToString)) / dblTotalAmount)) / 100) * 100, 0)
                    r.Cells(enmGrdDetail.Other_Charges).Value = (((dblOtherCharges * ((Val(r.Cells(enmGrdDetail.TotalAmount).Value.ToString) + Val(r.Cells(enmGrdDetail.Duty).Value.ToString) + Val(r.Cells(enmGrdDetail.AddCustomDuty).Value.ToString) + Val(r.Cells(enmGrdDetail.RegulatoryDuty).Value.ToString)) / dblTotalAmount)) / 100) * 100) ''TFS1956
                End If

                r.EndEdit()
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetTotal()
        Try
            If Me.grdDetail.RootTable Is Nothing Then Exit Sub
            Me.grdDetail.UpdateData()
            'Me.txtExciseDuty.Text = Math.Round((Val(Me.txtExciseDutyPercent.Text) / 100) * Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("Assd.Value"), Janus.Windows.GridEX.AggregateFunction.Sum), DecimalPointInValue)
            Me.txtCMCC.Text = Math.Round((Val(Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.Net_Amount), Janus.Windows.GridEX.AggregateFunction.Sum))), DecimalPointInValue) + Val(Me.txtAdjDDForCMCC.Text)
            Me.txtETO.Text = Math.Round(Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns(enmGrdDetail.ExciseDuty), Janus.Windows.GridEX.AggregateFunction.Sum), DecimalPointInValue) + Val(Me.txtAdjDDETO.Text)
            'Me.txtTotalAmount.Text = Math.Round(Val(Val(Me.txtPortCharges.Text) + Val(Me.txtShippingCharges.Text) + Val(Me.txtETO.Text) + Val(Me.txtCMCC.Text) + Val(Me.txtOtherCharges.Text) + Val(Me.txtExpenseByLC.Text)), DecimalPointInValue) '
            Me.txtTotalAmount.Text = Math.Round(Val(Val(Me.txtETO.Text) + Val(Me.txtCMCC.Text) + Val(Me.txtPortCharges.Text) + Val(Me.txtShippingCharges.Text) + Val(Me.txtOtherCharges.Text)), DecimalPointInValue) '
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grdDetail_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.CellUpdated
        Try
            GetGridDetailQtyCalculate(e)
            ApplyInsurrance()
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetail_RecordsDeleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDetail.RecordsDeleted
        Try
            ApplyInsurrance()
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtInsurrance_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInsurrance.TextChanged
        Try
            ApplyInsurrance()
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Dim id As Integer = 0I
            id = Me.cmbLCAccount.ActiveRow.Cells(0).Value
            FillCombos("LCAccount")
            Me.cmbLCAccount.Value = id
            'id = Me.cmbLCExpAccount.ActiveRow.Cells(0).Value
            'FillCombos("LCExpAccount")
            'Me.cmbLCExpAccount.Value = id
            'id = Me.cmbSupplierAccount.ActiveRow.Cells(0).Value
            'FillCombos("SupplierAccount")
            'Me.cmbSupplierAccount.Value = id
            'id = Me.cmbIndenterAccount.ActiveRow.Cells(0).Value
            'FillCombos("IndenterAccount")
            'Me.cmbIndenterAccount.Value = id
            'id = Me.cmbOpeningBank.ActiveRow.Cells(0).Value
            'FillCombos("OpeningBank")
            'Me.cmbOpeningBank.Value = id
            'id = Me.cmbOpenedBy.SelectedValue
            'FillCombos("OpenedBy")
            'Me.cmbOpenedBy.SelectedValue = id
            'id = Me.cmbTransporter.SelectedValue
            'FillCombos("Transporter")
            'Me.cmbTransporter.SelectedValue = id
            id = Me.cmbLocation.SelectedValue
            FillCombos("Location")
            Me.cmbLocation.SelectedValue = id
            id = Me.cmbItem.ActiveRow.Cells(0).Value
            FillCombos("Item")
            Me.cmbItem.Value = id
            id = Me.cmbProject.SelectedIndex
            FillCombos("Project")
            Me.cmbProject.SelectedIndex = id
            id = Me.cmbCurrency.SelectedIndex
            FillCombos("Currency")
            Me.cmbCurrency.SelectedIndex = id
            id = Me.cmbStatus.SelectedIndex
            FillCombos("Status")
            Me.cmbStatus.SelectedIndex = id
            ''Start TFS4163
            If Not getConfigValueByType("StockInConfigration").ToString = "Error" Then ''1596
                StockInConfigration = getConfigValueByType("StockInConfigration").ToString
            End If
            ''End TFS4163
            ''start TFS4161
            If Not getConfigValueByType("DiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = System.Convert.ToBoolean(getConfigValueByType("DiablePackQuantity").ToString)
            End If
            ''End TFS4161
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnLoadAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
        Try
            LoadAllStatus = "All"
            GetAllRecords("Master")
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPackQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        Try

            If Me.cmbUnit.Text = "Loose" Then
                Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
                Me.txtPrice.Text = (Val(Me.txtExchangeRate.Text) * Val(Me.txtImportValue.Text))
                Me.txtAmount.Text = Math.Round(Val(Me.txtTotalQuantity.Text) * Val(Me.txtPrice.Text), DecimalPointInValue)
            Else
                Me.txtTotalQuantity.Text = (Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text))
                Me.txtPrice.Text = (Val(Me.txtExchangeRate.Text) * Val(Me.txtImportValue.Text))
                ''Commented below line against TASK-408 added TotalQty instead of Pack Qty * Qty
                ''Me.txtAmount.Text = Math.Round(((Val(Me.txtQty.Text) * Val(Me.txtPackQty.Text)) * Val(Me.txtPrice.Text)), DecimalPointInValue)
                Me.txtAmount.Text = Math.Round(((Val(Me.txtTotalQuantity.Text)) * Val(Me.txtPrice.Text)), DecimalPointInValue) ''
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDutyPercent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackQty.TextChanged, txtPrice.TextChanged, txtInsurrance.TextChanged, txtAddCostPercent.TextChanged, txtExciseDutyPercent.TextChanged, txtSalesTaxPercent.TextChanged, txtAddSalesTaxPercent.TextChanged, txtAdvIncomeTaxPercent.TextChanged, txtExciseDutyPercent.TextChanged, txtDutyPercent.TextChanged
        Try
            GetNetAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Start TFS1956
    Private Sub txtAddCustomDutyPercent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackQty.TextChanged, txtPrice.TextChanged, txtInsurrance.TextChanged, txtAddCostPercent.TextChanged, txtExciseDutyPercent.TextChanged, txtSalesTaxPercent.TextChanged, txtAddSalesTaxPercent.TextChanged, txtAdvIncomeTaxPercent.TextChanged, txtExciseDutyPercent.TextChanged, txtDutyPercent.TextChanged, txtAddCustomDutyPercent.TextChanged, txtRegulatoryDutyPercent.TextChanged
        Try
            GetNetAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtRegulatoryDutyPercent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackQty.TextChanged, txtPrice.TextChanged, txtInsurrance.TextChanged, txtAddCostPercent.TextChanged, txtExciseDutyPercent.TextChanged, txtSalesTaxPercent.TextChanged, txtAddSalesTaxPercent.TextChanged, txtAdvIncomeTaxPercent.TextChanged, txtExciseDutyPercent.TextChanged, txtDutyPercent.TextChanged, txtAddCustomDutyPercent.TextChanged, txtRegulatoryDutyPercent.TextChanged
        Try
            GetNetAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''End TFS1956
    Private Sub Importtotal()
        'dr(enmGrdDetail.Qty)
        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDetail.GetRows
            r.BeginEdit()
            'If Not IsDBNull(r.Cells(enmGrdDetail.Import_Value).Value) Then
            r.Cells(enmGrdDetail.ImportAmount).Value = r.Cells(enmGrdDetail.Import_Value).Value * r.Cells(enmGrdDetail.TotalQty).Value
            '  End If

            r.EndEdit()
        Next

    End Sub
    Private Sub grdHistory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdHistory.DoubleClick
        Try
            EditRecord()

            Importtotal()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub SetBGColorTextBox()
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetNetAmount(Optional ByVal Condition As String = "")
        Try
            Dim dblInsurrance As Double = 0D
            If Me.grdDetail.RowCount = 0 Then
                Me.txtAssessedValue.Text = Math.Round(((Val(Me.txtAddCostPercent.Text) / 100) * (Val(Me.txtAmount.Text) + Val(Me.txtInsurrance.Text))) + (Val(Me.txtAmount.Text) + Val(Me.txtInsurrance.Text)), DecimalPointInValue)
            Else
                dblInsurrance = (((Val(Me.txtAmount.Text) / Me.grdDetail.GetTotal(Me.grdDetail.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) * Val(Me.txtInsurrance.Text)) / 100) * 100
                Me.txtAssessedValue.Text = Math.Round(((Val(Me.txtAddCostPercent.Text) / 100) * (Val(Me.txtAmount.Text) + Val(dblInsurrance))) + (Val(Me.txtAmount.Text) + Val(dblInsurrance)), DecimalPointInValue)
            End If
            Me.txtDuty.Text = Math.Round(((Val(Me.txtDutyPercent.Text) / 100) * Val(Me.txtAssessedValue.Text)), DecimalPointInValue)
            Me.txtAddCustomDuty.Text = Math.Round(((Val(Me.txtAddCustomDutyPercent.Text) / 100) * Val(Me.txtAssessedValue.Text)), DecimalPointInValue) ''TFS1956
            Me.txtRegulatoryDuty.Text = Math.Round(((Val(Me.txtRegulatoryDutyPercent.Text) / 100) * Val(Me.txtAssessedValue.Text)), DecimalPointInValue) ''TFS1956
            Me.txtSalesTax.Text = Math.Round(((Val(Me.txtSalesTaxPercent.Text) / 100) * (Val(Me.txtDuty.Text) + Val(Me.txtAddCustomDuty.Text) + Val(Me.txtRegulatoryDuty.Text) + Val(Me.txtAssessedValue.Text))), DecimalPointInValue)  ''TFS1956
            Me.txtAddSalesTax.Text = Math.Round(((Val(Me.txtAddSalesTaxPercent.Text) / 100) * (Val(Me.txtDuty.Text) + Val(Me.txtAddCustomDuty.Text) + Val(Me.txtRegulatoryDuty.Text) + Val(Me.txtAssessedValue.Text))), DecimalPointInValue)  ''TFS1956
            Me.txtAdvIncomeTax.Text = Math.Round(((Val(Me.txtAdvIncomeTaxPercent.Text) / 100) * (Val(Me.txtAssessedValue.Text) + Val(Me.txtSalesTax.Text) + Val(Me.txtAddSalesTax.Text) + Val(Me.txtDuty.Text) + Val(Me.txtAddCustomDuty.Text) + Val(Me.txtRegulatoryDuty.Text))), DecimalPointInValue)  ''TFS1956
            Me.txtNetAmount.Text = Math.Round((Val(Me.txtDuty.Text) + Val(Me.txtAddCustomDuty.Text) + Val(Me.txtRegulatoryDuty.Text) + Val(Me.txtSalesTax.Text) + Val(Me.txtAddSalesTax.Text) + Val(Me.txtAdvIncomeTax.Text)), DecimalPointInValue)  ''TFS1956
            Me.txtExciseDuty.Text = ((Val(Me.txtExciseDutyPercent.Text) / 100) * Val(Me.txtAssessedValue.Text))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grdDetail_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDetail.ColumnButtonClick
        Try
            If e.Column.Key = "Column1" Then
                If Not msg_Confirm("Are you sure ,You want to delete this record") = True Then Exit Sub
                ''Start TFS3079 : Ayesah Rehman : 18-04-2018
                If LCId > 0 Then
                    If DeleteDetail(LCId, Me.grdDetail.GetRow.Cells("LCDetailId").Value) = True Then DialogResult = Windows.Forms.DialogResult.Yes
                End If
                ''End TFS3079
                Me.grdDetail.GetRow.Delete()
                Me.grdDetail.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetLastTaxPercentage(ByVal ItemId As Integer)
        Try
            Dim dt As New DataTable
            ''Edit for TFS1956
            dt = GetDataTable("Select ArticleDefId, IsNull(AddCostPercent,0) as AddCostPercent,IsNull(DutyPercent,0) as DutyPercent, IsNull(AddCustomDutyPercent,0) as AddCustomDutyPercent,IsNull(RegulatoryDutyPercent,0) as RegulatoryDutyPercent,IsNull(SaleTaxPercent,0) as SaleTaxPercent,IsNull(AddSaleTaxPercent,0) as AddSaleTaxPercent,IsNull(AdvIncomeTaxPercent,0) as AdvIncomeTaxPercent From LCDetailTable WHERE LCDetailId in (Select Max(LCDetailId) From LCDetailTable WHERE ArticleDefId=" & ItemId & " Group By ArticleDefId) AND ArticleDefId=" & ItemId & "")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Me.txtAddCostPercent.Text = Val(dt.Rows(0).Item("AddCostPercent").ToString)
                    Me.txtDutyPercent.Text = Val(dt.Rows(0).Item("DutyPercent").ToString)
                    Me.txtAddCustomDutyPercent.Text = Val(dt.Rows(0).Item("AddCustomDutyPercent").ToString) ''TFS1956
                    Me.txtRegulatoryDutyPercent.Text = Val(dt.Rows(0).Item("RegulatoryDutyPercent").ToString) ''TFS1956
                    Me.txtSalesTaxPercent.Text = Val(dt.Rows(0).Item("SaleTaxPercent").ToString)
                    Me.txtAddSalesTaxPercent.Text = Val(dt.Rows(0).Item("AddSaleTaxPercent").ToString)
                    Me.txtAdvIncomeTaxPercent.Text = Val(dt.Rows(0).Item("AdvIncomeTaxPercent").ToString)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub txtExciseDutyPercent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExciseDutyPercent.TextChanged
        Try
            ApplyInsurrance()
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDetail_DeletingRecord(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionCancelEventArgs) Handles grdDetail.DeletingRecord
        Try
            ApplyInsurrance()
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtCMCC_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ApplyInsurrance()
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnAttachment_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAttachment.ButtonClick
        Try
            OpenFileDialog1.FileName = String.Empty
            OpenFileDialog1.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                arrFile = OpenFileDialog1.FileNames
                Dim intCountAttachedFiles As Integer = 0I
                If Me.btnSave.Text <> "&Save" Then
                    If Me.grdHistory.RowCount > 0 Then
                        intCountAttachedFiles = Val(Me.grdHistory.CurrentRow.Cells("No Of Attachment").Value)
                    End If
                End If
                Me.btnAttachment.Text = "Attachment (" & arrFile.Length + intCountAttachedFiles & ")"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function SaveDocument(ByVal DocId As Integer, ByVal Source As String, ByVal objTrans As OleDb.OleDbTransaction) As Boolean
        Dim cmd As New OleDb.OleDbCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source='" & Source & "'", objTrans)
            dt.AcceptChanges()


            Dim objdt As New DataTable
            objdt = GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source='" & Source & "'", objTrans)
            Dim intId As Integer = objdt.Rows(0)(0)

            Dim strSQL As String = String.Empty
            cmd.CommandText = String.Empty
            strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source='" & Source & "'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            Dim objPath As String = getConfigValueByType("FileAttachmentPath").ToString

            If arrFile.Length > 0 Then
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If
                        Dim New_Files As String = intId & "_" & DocId & "_LC_" & Me.dtpLCDate.Value.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
                        Dim dr As DataRow
                        dr = dt.NewRow
                        dr(0) = DocId
                        dr(1) = Source
                        dr(2) = objPath & "\" & New_Files
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                        If IO.File.Exists(objPath & "\" & New_Files) Then
                            IO.File.Delete(objPath & "\" & New_Files)
                        End If
                        IO.File.Copy(objFile, objPath & "\" & New_Files)
                        intId += 1
                    End If
                Next
            End If


            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        Dim strPath As String = objPath
                        Dim strFileName As String = r.Item("FileNames").ToString.Substring(r.Item("FileNames").ToString.LastIndexOf("\") + 1)
                        cmd.CommandText = String.Empty
                        strSQL = "INSERT INTO DocumentAttachment(DocId, Source, FileName, Path) Values(" & Val(r("DocId").ToString) & ",'" & r.Item("Source").ToString.Replace("'", "''") & "', '" & strFileName.Replace("'", "''") & "', '" & strPath.Replace("'", "''") & "')"
                        cmd.CommandText = strSQL
                        cmd.ExecuteNonQuery()
                    Next
                End If
            End If


        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function GetVoucherRecord() As DataSet
        Try

            Dim strSQL As String = String.Empty
            Dim ds As New dsVoucherDocumentAttachment
            ds.Tables.Clear()
            strSQL = "SP_LCDocument " & Val(Me.grdHistory.GetRow.Cells("LCId").Value.ToString) & ""
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            ds.Tables.Add(dt)
            ds.Tables(0).TableName = "dtLCDocument"


            strSQL = String.Empty
            strSQL = "Select DocId,FileName,Path,Convert(Image,'') as Attachment_Image From DocumentAttachment WHERE (DocId=" & Me.grdHistory.GetRow.Cells("LCId").Value & ") AND Source='" & Me.Name & "'"
            Dim dtAttach As New DataTable
            dtAttach.TableName = "dtAttachment"
            dtAttach = GetDataTable(strSQL)

            If dtAttach IsNot Nothing Then
                If dtAttach.Rows.Count > 0 Then
                    For Each r As DataRow In dtAttach.Rows
                        r.BeginEdit()
                        If IO.File.Exists(CStr(r("Path").ToString & "\" & r("FileName").ToString)) Then
                            LoadPicture(r, "Attachment_Image", CStr(r("Path").ToString & "\" & r("FileName").ToString))
                        End If
                        r.EndEdit()
                    Next
                End If
            End If

            ds.Tables.Add(dtAttach)
            ds.Tables(1).TableName = "dtAttachment"
            ds.AcceptChanges()

            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub PrintAttachmentLCDocumentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintAttachmentLCDocumentToolStripMenuItem.Click
        Try
            If Me.grdHistory.RowCount = 0 Then Exit Sub
            'AddRptParam("Pm-dtVoucher.Voucher_Id", Me.grdVouchers.GetRow.Cells(0).Value)
            DataSetShowReport("rptImportDocumentAttachment", GetVoucherRecord())

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdHistory_LinkClicked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdHistory.LinkClicked
        Try
            If e.Column.Key = "No Of Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.Name
                frm._VoucherId = Me.grdHistory.GetRow.Cells("LCID").Value.ToString
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbLC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLC.SelectedIndexChanged
        Try
            If IsOpenedForm = False Then Exit Sub
            If Me.cmbLC.SelectedIndex > 0 Then
                Me.cmbLCAccount.Value = Val(CType(Me.cmbLC.SelectedItem, DataRowView).Row.Item("VendorId").ToString)
                Me.cmbProject.SelectedValue = Val(CType(Me.cmbLC.SelectedItem, DataRowView).Row.Item("CostCenter").ToString)
                Me.txtExchangeRate.Text = Val(CType(Me.cmbCurrency.SelectedItem, DataRowView).Row.Item("CurrencyRate").ToString)
                Me.txtExpenseByLC.Text = ExpAmountLC()
                Me.cmbLCAccount.Enabled = False
                Me.cmbProject.Enabled = False
                FillCombos("GRN")
            Else
                FillCombos("GRN")
                cmbLCAccount.Enabled = True
                Me.cmbProject.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtExpenseByLC_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function ExpAmountLC() As Double
        Try
            Dim ExpAmount As Integer

            Dim pair As KeyValuePair(Of String, Integer)
            If Me.cmbLC.SelectedIndex > 0 Then
                ExpenseIDList = GetExpenseByLC(Val(CType(Me.cmbLC.SelectedItem, DataRowView).Row.Item("CostCenter").ToString))
                For Each pair In ExpenseIDList
                    ExpAmount = pair.Value + ExpAmount
                Next

                'ApplyInsurrance()
                'GetTotal()
            End If
            Return ExpAmount

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked

        Me.txtExpenseByLC.Text = ExpAmountLC()
    End Sub
    Private Sub btnPrint_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.ButtonClick
        Try
            If Me.grdHistory.RowCount = 0 Then Exit Sub
            AddRptParam("@LCDocId", Me.grdHistory.GetRow.Cells(0).Value)
            ShowReport("rptImportDocument")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPortCharges_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPortCharges.TextChanged, txtShippingCharges.TextChanged, txtOtherCharges.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtAdjDDForCMCC_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAdjDDForCMCC.KeyPress, txtAdjDDETO.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAdjDDForCMCC_TextChanged(sender As Object, e As EventArgs) Handles txtAdjDDForCMCC.TextChanged, txtAdjDDETO.TextChanged
        Try
            ApplyInsurrance()
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetGridDetailQtyCalculate(ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try
            Me.grdDetail.UpdateData()
            If e.Column.Index = enmGrdDetail.Qty Or e.Column.Index = enmGrdDetail.PackQty Then
                If Val(Me.grdDetail.GetRow.Cells(enmGrdDetail.PackQty).Value.ToString) > 1 Then
                    Me.grdDetail.GetRow.Cells(enmGrdDetail.TotalQty).Value = (Val(Me.grdDetail.GetRow.Cells(enmGrdDetail.PackQty).Value.ToString) * Val(Me.grdDetail.GetRow.Cells(enmGrdDetail.Qty).Value.ToString))
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                Else
                    Me.grdDetail.GetRow.Cells(enmGrdDetail.TotalQty).Value = Val(Me.grdDetail.GetRow.Cells(enmGrdDetail.Qty).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                End If
            ElseIf e.Column.Index = enmGrdDetail.TotalQty Then
                If Not Val(Me.grdDetail.GetRow.Cells(enmGrdDetail.PackQty).Value.ToString) > 1 Then
                    Me.grdDetail.GetRow.Cells(enmGrdDetail.Qty).Value = Val(Me.grdDetail.GetRow.Cells(enmGrdDetail.TotalQty).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.Qty).Value
                End If
            End If
            'Me.grd.Refetch()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTotalQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtTotalQuantity.TextChanged, txtInsurrance.TextChanged, txtExciseDutyPercent.TextChanged
        Try
            Me.txtPrice.Text = (Val(Me.txtExchangeRate.Text) * Val(Me.txtImportValue.Text))
            Me.txtAmount.Text = Math.Round(((Val(Me.txtTotalQuantity.Text)) * Val(Me.txtPrice.Text)), DecimalPointInValue) ''
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtImportValue_TextChanged(sender As Object, e As EventArgs) Handles txtImportValue.TextChanged
        Me.txtPrice.Text = (Val(Me.txtExchangeRate.Text) * Val(Me.txtImportValue.Text))
        Me.txtAmount.Text = Math.Round(((Val(Me.txtTotalQuantity.Text)) * Val(Me.txtPrice.Text)), DecimalPointInValue)
    End Sub

    Private Sub txtExchangeRate_TextChanged(sender As Object, e As EventArgs) Handles txtExchangeRate.TextChanged
        Me.txtPrice.Text = (Val(Me.txtExchangeRate.Text) * Val(Me.txtImportValue.Text))
        Me.txtAmount.Text = Math.Round(((Val(Me.txtTotalQuantity.Text)) * Val(Me.txtPrice.Text)), DecimalPointInValue)
    End Sub

    Public Sub GetLCFromItemWiseLC(ByVal ID As Integer)
        Try
            If Me.grdDetail.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.btnSave.Text = "&Update"
            Dim dt As DataTable = New LCDAL().GetSingle(ID)
            FillCombos("LC")
            FillCombos("LCAccount")
            LCId = dt.Rows(0).Item("LCID")
            Me.txtLCNo.Text = dt.Rows(0).Item("LCNo").ToString
            Me.dtpLCDate.Value = dt.Rows(0).Item("LCDate")
            Me.cmbLCAccount.Value = Val(dt.Rows(0).Item("LCAccountId").ToString)
            'Me.cmbLCExpAccount.Value = Val(Me.grdHistory.GetRow.Cells("LCExpenseAccountId").Value.ToString)
            Me.txtImportName.Text = dt.Rows(0).Item("ImportName").ToString

            Me.txtInsurrance.Text = Val(dt.Rows(0).Item("Insurrance").ToString)
            Me.txtExchangeRate.Text = Val(dt.Rows(0).Item("ExchangeRate").ToString)
            'Me.txtBankPaidAmount.Text = Val(Me.grdHistory.GetRow.Cells("BankPaidAmount").Value.ToString)
            RemoveHandler txtCMCC.TextChanged, AddressOf Me.txtCMCC_TextChanged
            Me.txtAdjDDForCMCC.Text = Val(dt.Rows(0).Item("Adj CMCC").ToString)
            Me.txtAdjDDETO.Text = Val(dt.Rows(0).Item("Adj ETO").ToString)
            Me.txtCMCC.Text = Val(dt.Rows(0).Item("DDForCMCC").ToString)
            'Me.txtShippingCharges.Text = Val(Me.grdHistory.GetRow.Cells("ShipingCharges").Value.ToString)
            'Me.txtPortCharges.Text = Val(Me.grdHistory.GetRow.Cells("PortCharges").Value.ToString)
            'Me.txtOtherCharges.Text = Val(Me.grdHistory.GetRow.Cells("OtherCharges").Value.ToString)
            Me.txtExpenseByLC.Text = Val(dt.Rows(0).Item("ExpenseByLC").ToString)
            'Me.txtETO.Text = Val(Me.grdHistory.GetRow.Cells("DDForETO").Value.ToString)
            Me.cmbProject.SelectedValue = Val(dt.Rows(0).Item("CostCenterId").ToString)
            Me.txtExciseDutyPercent.Text = Val(dt.Rows(0).Item("Excise Duty%").ToString)
            Me.txtExciseDuty.Text = Val(dt.Rows(0).Item("ExciseDuty").ToString)
            Me.btnAttachment.Text = "Attachment (" & Val(dt.Rows(0).Item("No Of Attachment").ToString) & ")"

            Me.cmbLC.SelectedValue = Val(dt.Rows(0).Item("LCDocID").ToString)
            Me.chkFinancialImpact.Checked = dt.Rows(0).Item("Financial_Impact")
            cmbLCAccount.Enabled = False
            Me.cmbLC.Enabled = False
            Me.cmbProject.Enabled = False
            GetAllRecords("Detail")
            ApplyInsurrance()
            GetTotal()
            ApplySecurity(Utility.EnumDataMode.Edit)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurrency.SelectedIndexChanged
        Try
            If Not Me.cmbCurrency.SelectedItem Is Nothing Then
                Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
                Me.txtExchangeRate.Text = dr.Row.Item("CurrencyRate").ToString

                ' R@!    11-Jun-2016 Dollor account
                ' Setting default rate to zero
                If Val(Me.txtExchangeRate.Text) = 0 Then
                    Me.txtExchangeRate.Text = 1
                End If

                'R@!    11-Jun-2016 Dollor account
                'Code Commented
                ' Me.grd.RootTable.Columns("CurrencyAmount").Caption = "" & Me.cmbCurrency.Text & " Amount"
                ' Added 2 coloumns and changed caption

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function GetCurrencyRate(ByVal currencyId As Integer) As Double ''TAKS-407
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Dim currencyRate As Double = 0
        Try
            str = " Select CurrencyRate, CurrencyId From tblCurrencyRate Where CurrencyRateId in ( Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId) And CurrencyId = " & currencyId & ""
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                currencyRate = Val(dt.Rows.Item(0).Item(0).ToString)
            End If

            Return currencyRate

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub GetCurrencyRate()
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Dim currencyRate As Double = 0
        Try
            str = "select isnull( CurrencyID,1 ), isnull (CurrencyRate,1 ) from LCMasterTable where LCNO=N'" & Me.txtLCNo.Text & "'"
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                cmbCurrency.SelectedValue = Val(dt.Rows.Item(0).Item(0).ToString)
                txtExchangeRate.Text = Val(dt.Rows.Item(0).Item(1).ToString)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnPurchaseOrder_Click(sender As Object, e As EventArgs) Handles btnPurchaseOrder.Click
        Try
            Dim frm As New frmPurchaseOrderList
            If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then
                If Me.btnSave.Text = "&Update" Then
                    ReSetControls()
                Else
                    GetAllRecords("Detail")
                End If
                PurchaseOrderId = frm.ReceivingID
                GetAllRecords("PODetail")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub cmbGRN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGRN.SelectedIndexChanged
        Try
            If Not cmbGRN.SelectedIndex = -1 AndAlso cmbGRN.SelectedValue > 0 Then
                FillCombos("GRNItems")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS1462
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks> Turn color of row to light yellow in case financial impact is available in voucher.</remarks>
    Private Sub grdHistory_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs)
        'Try
        '    If Me.grdHistory.RowCount > 0 Then
        '        If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
        '            If e.Row.Cells("Financial_Impact").Value = True Then
        '                Dim RowStyle As New Janus.Windows.GridEX.GridEXFormatStyle()
        '                RowStyle.BackColor = Color.LightYellow
        '                e.Row.RowStyle = RowStyle
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
    ''' <summary>
    ''' TASK TFS1462
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks> </remarks>
    Private Sub PrintVoucherToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintVoucherToolStripMenuItem.Click
        Try
            If Me.grdHistory.CurrentRow.Cells("Financial_Impact").Value = True Then
                GetVoucherPrint(Me.grdHistory.CurrentRow.Cells("LCNo").Value.ToString, Me.Name, BaseCurrencyName, BaseCurrencyId)
            Else
                msg_Information("Financial impact is not found against selected entry")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub cmbLCAccount_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbLCAccount.KeyDown
        ''TFS1781 : Ayesha Rehman :Added for Selection of Vendor
        Try
            If e.KeyCode = Keys.F1 Then
                frmAccountSearch.AccountType = "'Vendor','LC'"
                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbLCAccount.Value = frmAccountSearch.SelectedAccountId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdDetail.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Import"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdHistory.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdHistory.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdHistory.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Import"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class