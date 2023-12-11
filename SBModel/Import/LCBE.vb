Public Class LCBE

    Private _LCID As Integer

    Public ExpensesIDs As Dictionary(Of String, Integer)
    Public CurrenyObj As CurrencyRate

    Public Property LCID() As Integer
        Get
            Return _LCID
        End Get
        Set(ByVal value As Integer)
            _LCID = value
        End Set
    End Property

    Private _LCNo As String
    Public Property LCNo() As String
        Get
            Return _LCNo
        End Get
        Set(ByVal value As String)
            _LCNo = value
        End Set
    End Property

    Private _LCDate As DateTime
    Public Property LCDate() As DateTime
        Get
            Return _LCDate
        End Get
        Set(ByVal value As DateTime)
            _LCDate = value
        End Set
    End Property

    Private _LCAccountId As Integer
    Public Property LCAccountId() As Integer
        Get
            Return _LCAccountId
        End Get
        Set(ByVal value As Integer)
            _LCAccountId = value
        End Set
    End Property

    Private _LCExpenseAccountId As Integer
    Public Property LCExpenseAccountId() As Integer
        Get
            Return _LCExpenseAccountId
        End Get
        Set(ByVal value As Integer)
            _LCExpenseAccountId = value
        End Set
    End Property

    Private _ImportName As String
    Public Property ImportName() As String
        Get
            Return _ImportName
        End Get
        Set(ByVal value As String)
            _ImportName = value
        End Set
    End Property

    Private _PerformaNo As String
    Public Property PerformaNo() As String
        Get
            Return _PerformaNo
        End Get
        Set(ByVal value As String)
            _PerformaNo = value
        End Set
    End Property

    Private _PerformaDate As DateTime
    Public Property PerformaDate() As DateTime
        Get
            Return _PerformaDate
        End Get
        Set(ByVal value As DateTime)
            _PerformaDate = value
        End Set
    End Property

    Private _PaymentMode As String
    Public Property PaymentMode() As String
        Get
            Return _PaymentMode
        End Get
        Set(ByVal value As String)
            _PaymentMode = value
        End Set
    End Property

    Private _LCBoreDate As DateTime
    Public Property LCBoreDate() As DateTime
        Get
            Return _LCBoreDate
        End Get
        Set(ByVal value As DateTime)
            _LCBoreDate = value
        End Set
    End Property

    Private _PortOfLoading As String
    Public Property PortOfLoading() As String
        Get
            Return _PortOfLoading
        End Get
        Set(ByVal value As String)
            _PortOfLoading = value
        End Set
    End Property

    Private _PortOfDischarge As String
    Public Property PortOfDischarge() As String
        Get
            Return _PortOfDischarge
        End Get
        Set(ByVal value As String)
            _PortOfDischarge = value
        End Set
    End Property

    Private _SupplierAccountId As Integer
    Public Property SupplierAccountId() As Integer
        Get
            Return _SupplierAccountId
        End Get
        Set(ByVal value As Integer)
            _SupplierAccountId = value
        End Set
    End Property

    Private _IndenterAccountId As Integer
    Public Property IndenterAccountId() As Integer
        Get
            Return _IndenterAccountId
        End Get
        Set(ByVal value As Integer)
            _IndenterAccountId = value
        End Set
    End Property

    Private _PartialShipment As Boolean
    Public Property PartialShipment() As Boolean
        Get
            Return _PartialShipment
        End Get
        Set(ByVal value As Boolean)
            _PartialShipment = value
        End Set
    End Property

    Private _Transhipment As Boolean
    Public Property Transhipment() As Boolean
        Get
            Return _Transhipment
        End Get
        Set(ByVal value As Boolean)
            _Transhipment = value
        End Set
    End Property

    Private _LatestDateOfShipment As DateTime
    Public Property LatestDateOfShipment() As DateTime
        Get
            Return _LatestDateOfShipment
        End Get
        Set(ByVal value As DateTime)
            _LatestDateOfShipment = value
        End Set
    End Property

    Private _LSBDate As DateTime
    Public Property LSBDate() As DateTime
        Get
            Return _LSBDate
        End Get
        Set(ByVal value As DateTime)
            _LSBDate = value
        End Set
    End Property

    Private _Insurrance As Double
    Public Property Insurrance() As Double
        Get
            Return _Insurrance
        End Get
        Set(ByVal value As Double)
            _Insurrance = value
        End Set
    End Property

    Private _ExchangeRate As Double
    Public Property ExchangeRate() As Double
        Get
            Return _ExchangeRate
        End Get
        Set(ByVal value As Double)
            _ExchangeRate = value
        End Set
    End Property

    Private _BankPaidAmount As Double
    Public Property BankPaidAmount() As Double
        Get
            Return _BankPaidAmount
        End Get
        Set(ByVal value As Double)
            _BankPaidAmount = value
        End Set
    End Property

    Private _ShipingCharges As Double
    Public Property ShipingCharges() As Double
        Get
            Return _ShipingCharges
        End Get
        Set(ByVal value As Double)
            _ShipingCharges = value
        End Set
    End Property

    Private _PortCharges As Double
    Public Property PortCharges() As Double
        Get
            Return _PortCharges
        End Get
        Set(ByVal value As Double)
            _PortCharges = value
        End Set
    End Property

    Private _OthersCharges As Double
    Public Property OthersCharges() As Double
        Get
            Return _OthersCharges
        End Get
        Set(ByVal value As Double)
            _OthersCharges = value
        End Set
    End Property

    Private _ExpenseByLC As Double
    Public Property ExpenseByLC() As Double
        Get
            Return _ExpenseByLC
        End Get
        Set(ByVal value As Double)
            _ExpenseByLC = value
        End Set
    End Property

    Private _AssessedValue As Double
    Public Property AssessedValue() As Double
        Get
            Return _AssessedValue
        End Get
        Set(ByVal value As Double)
            _AssessedValue = value
        End Set
    End Property

    Private _DDForCMCC As Double
    Public Property DDForCMCC() As Double
        Get
            Return _DDForCMCC
        End Get
        Set(ByVal value As Double)
            _DDForCMCC = value
        End Set
    End Property

    Private _DDForETO As Double
    Public Property DDForETO() As Double
        Get
            Return _DDForETO
        End Get
        Set(ByVal value As Double)
            _DDForETO = value
        End Set
    End Property

    Private _TotalDuty As Double
    Public Property TotalDuty() As Double
        Get
            Return _TotalDuty
        End Get
        Set(ByVal value As Double)
            _TotalDuty = value
        End Set
    End Property

    Private _TotalQty As Double
    Public Property TotalQty() As Double
        Get
            Return _TotalQty
        End Get
        Set(ByVal value As Double)
            _TotalQty = value
        End Set
    End Property

    Private _AdvanceIncomeTax As Double
    Public Property AdvanceIncomeTax() As Double
        Get
            Return _AdvanceIncomeTax
        End Get
        Set(ByVal value As Double)
            _AdvanceIncomeTax = value
        End Set
    End Property

    Private _SalesTax As Double
    Public Property SalesTax() As Double
        Get
            Return _SalesTax
        End Get
        Set(ByVal value As Double)
            _SalesTax = value
        End Set
    End Property

    Private _AdditionalSalesTax As Double
    Public Property AdditionalSalesTax() As Double
        Get
            Return _AdditionalSalesTax
        End Get
        Set(ByVal value As Double)
            _AdditionalSalesTax = value
        End Set
    End Property

    Private _TotalAmount As Double
    Public Property TotalAmount() As Double
        Get
            Return _TotalAmount
        End Get
        Set(ByVal value As Double)
            _TotalAmount = value
        End Set
    End Property

    Private _UserName As String
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property

    Private _EntryDate As DateTime
    Public Property EntryDate() As DateTime
        Get
            Return _EntryDate
        End Get
        Set(ByVal value As DateTime)
            _EntryDate = value
        End Set
    End Property

    Private _LCDetail As List(Of LCDetailBE)
    Public Property LCDetail() As List(Of LCDetailBE)
        Get
            Return _LCDetail
        End Get
        Set(ByVal value As List(Of LCDetailBE))
            _LCDetail = value
        End Set
    End Property

    Private _ActivityLog As ActivityLog
    Public Property ActivityLog() As ActivityLog
        Get
            Return _ActivityLog
        End Get
        Set(ByVal value As ActivityLog)
            _ActivityLog = value
        End Set
    End Property


    'Private _LCOtherDetail As LCOtherDetail
    'Public Property LCOtherDetail() As LCOtherDetail
    '    Get
    '        Return _LCOtherDetail
    '    End Get
    '    Set(ByVal value As LCOtherDetail)
    '        _LCOtherDetail = value
    '    End Set
    'End Property

    Private _CostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return _CostCenterId
        End Get
        Set(ByVal value As Integer)
            _CostCenterId = value
        End Set
    End Property

    Private _ExciseDutyPercent As Double
    Public Property ExciseDutyPercent() As Double
        Get
            Return _ExciseDutyPercent
        End Get
        Set(ByVal value As Double)
            _ExciseDutyPercent = value
        End Set
    End Property

    Private _ExciseDuty As Double
    Public Property ExciseDuty() As Double
        Get
            Return _ExciseDuty
        End Get
        Set(ByVal value As Double)
            _ExciseDuty = value
        End Set
    End Property

    Private _LCDocId As Integer
    Public Property LCDocId() As Integer
        Get
            Return _LCDocId
        End Get
        Set(ByVal value As Integer)
            _LCDocId = value
        End Set
    End Property

    Private _Voucher_Id As Integer
    Public Property Voucher_Id() As Integer
        Get
            Return _Voucher_Id
        End Get
        Set(ByVal value As Integer)
            _Voucher_Id = value
        End Set
    End Property

    Private _FinancialImpact As Boolean
    Public Property FinancialImpact() As Boolean
        Get
            Return _FinancialImpact
        End Get
        Set(ByVal value As Boolean)
            _FinancialImpact = value
        End Set
    End Property

    Private _ShippingRemarks As String
    Public Property ShippingRemarks() As String
        Get
            Return _ShippingRemarks
        End Get
        Set(ByVal value As String)
            _ShippingRemarks = value
        End Set
    End Property

    Private _PortRemarks As String
    Public Property PortRemarks() As String
        Get
            Return _PortRemarks
        End Get
        Set(ByVal value As String)
            _PortRemarks = value
        End Set
    End Property

    Private _CMCCRemarks As String
    Public Property CMCCRemarks() As String
        Get
            Return _CMCCRemarks
        End Get
        Set(ByVal value As String)
            _CMCCRemarks = value
        End Set
    End Property

    Private _ETORemarks As String
    Public Property ETORemarks() As String
        Get
            Return _ETORemarks
        End Get
        Set(ByVal value As String)
            _ETORemarks = value
        End Set
    End Property

    Private _AdjCMCCAmount As Double
    Public Property AdjCMCCAmount() As Double
        Get
            Return _AdjCMCCAmount
        End Get
        Set(ByVal value As Double)
            _AdjCMCCAmount = value
        End Set
    End Property
    Private _AdjETOAmount As Double
    Public Property AdjETOAmount() As Double
        Get
            Return _AdjETOAmount
        End Get
        Set(ByVal value As Double)
            _AdjETOAmount = value
        End Set
    End Property
    Private _PurchaseOrderId As Integer
    Public Property PurchaseOrderId() As Integer
        Get
            Return _PurchaseOrderId
        End Get
        Set(ByVal value As Integer)
            _PurchaseOrderId = value
        End Set
    End Property
    Private _ReceivingNoteId As Integer
    Public Property ReceivingNoteId() As Integer
        Get
            Return _ReceivingNoteId
        End Get
        Set(ByVal value As Integer)
            _ReceivingNoteId = value
        End Set
    End Property
    ''TASK : TFS1350
    Private _Status As String
    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal value As String)
            _Status = value
        End Set
    End Property
    '' TASK TFS1609
    Private _NewVoucherNo As String
    Public Property NewVoucherNo() As String
        Get
            Return _NewVoucherNo
        End Get
        Set(ByVal value As String)
            _NewVoucherNo = value
        End Set
    End Property

    ''End TASK TFS1609
    '' TASK TFS1609
    Private _VoucherRemarks As String
    Public Property VoucherRemarks() As String
        Get
            Return _VoucherRemarks
        End Get
        Set(ByVal value As String)
            _VoucherRemarks = value
        End Set
    End Property

    ''End TASK TFS1609
    '' TASK TFS1609
    Private _IsNewVoucher As Boolean
    Public Property IsNewVoucher() As Boolean
        Get
            Return _IsNewVoucher
        End Get
        Set(ByVal value As Boolean)
            _IsNewVoucher = value
        End Set
    End Property

    ''End TASK TFS1609
    '' TASK TFS1609
    Private _VoucherDate As DateTime
    Public Property VoucherDate() As DateTime
        Get
            Return _VoucherDate
        End Get
        Set(ByVal value As DateTime)
            _VoucherDate = value
        End Set
    End Property

    ''End TASK TFS1609
    '' TASK TFS1609
    Private _CurrentVoucherNo As String
    Public Property CurrentVoucherNo() As String
        Get
            Return _CurrentVoucherNo
        End Get
        Set(ByVal value As String)
            _CurrentVoucherNo = value
        End Set
    End Property

    ''End TASK TFS1609
    Private _FirstVoucher As Boolean
    Public Property FirstVoucher() As Boolean
        Get
            Return _FirstVoucher
        End Get
        Set(ByVal value As Boolean)
            _FirstVoucher = value
        End Set
    End Property

End Class








Public Class CurrencyRate


    Public BaseCurrencyId As Integer
    Public CurrencyId As Integer
    Public CurrencyDr As Double
    Public CurrencyCr As Double
    Public CurrencyRate As Double
    Public BaseCurrencyRates As Double
    Public CurrencySymbol As String

End Class
Public Class LCDetailBE

    Private _ImportAmount As Integer
    Public Property ImportAmount() As Integer
        Get
            Return _ImportAmount
        End Get
        Set(ByVal value As Integer)
            _ImportAmount = value
        End Set
    End Property

    Private _LCDetailId As Integer
    Public Property LCDetailId() As Integer
        Get
            Return _LCDetailId
        End Get
        Set(ByVal value As Integer)
            _LCDetailId = value
        End Set
    End Property

    Private _LCId As Integer
    Public Property LCId() As Integer
        Get
            Return _LCId
        End Get
        Set(ByVal value As Integer)
            _LCId = value
        End Set
    End Property

    Private _LocationId As Integer
    Public Property LocationId() As Integer
        Get
            Return _LocationId
        End Get
        Set(ByVal value As Integer)
            _LocationId = value
        End Set
    End Property

    Private _ArticleDefId As Integer
    Public Property ArticleDefId() As Integer
        Get
            Return _ArticleDefId
        End Get
        Set(ByVal value As Integer)
            _ArticleDefId = value
        End Set
    End Property

    Private _ArticleDescription As String

    Public Property ArticleDescription() As String
        Get
            Return _ArticleDescription
        End Get
        Set(ByVal value As String)
            _ArticleDescription = value
        End Set
    End Property

    Private _ArticleSize As String
    Public Property ArticleSize() As String
        Get
            Return _ArticleSize
        End Get
        Set(ByVal value As String)
            _ArticleSize = value
        End Set
    End Property

    Private _Weight As Double
    Public Property Weight() As Double
        Get
            Return _Weight
        End Get
        Set(ByVal value As Double)
            _Weight = value
        End Set
    End Property

    Private _Per_Kg_Cost As Double
    Public Property Per_Kg_Cost() As Double
        Get
            Return _Per_Kg_Cost
        End Get
        Set(ByVal value As Double)
            _Per_Kg_Cost = value
        End Set
    End Property
    

    Private _Weighted_Cost As Double
    Public Property Weighted_Cost() As Double
        Get
            Return _Weighted_Cost
        End Get
        Set(ByVal value As Double)
            _Weighted_Cost = value
        End Set
    End Property


    Private _PackDesc As String
    Public Property PackDesc() As String
        Get
            Return _PackDesc
        End Get
        Set(ByVal value As String)
            _PackDesc = value
        End Set
    End Property
    Private _Sz1 As Double
    Public Property Sz1() As Double
        Get
            Return _Sz1
        End Get
        Set(ByVal value As Double)
            _Sz1 = value
        End Set
    End Property

    Private _Sz7 As Double
    Public Property Sz7() As Double
        Get
            Return _Sz7
        End Get
        Set(ByVal value As Double)
            _Sz7 = value
        End Set
    End Property

    Private _Qty As Double
    Public Property Qty() As Double
        Get
            Return _Qty
        End Get
        Set(ByVal value As Double)
            _Qty = value
        End Set
    End Property

    Private _Price As Double
    Public Property Price() As Double
        Get
            Return _Price
        End Get
        Set(ByVal value As Double)
            _Price = value
        End Set
    End Property

    Private _TotalAmount As Double
    Public Property TotalAmount() As Double
        Get
            Return _TotalAmount
        End Get
        Set(ByVal value As Double)
            _TotalAmount = value
        End Set
    End Property

    Private _Insurrance As Double
    Public Property Insurrance() As Double
        Get
            Return _Insurrance
        End Get
        Set(ByVal value As Double)
            _Insurrance = value
        End Set
    End Property

    Private _AddCostPercent As Double
    Public Property AddCostPercent() As Double
        Get
            Return _AddCostPercent
        End Get
        Set(ByVal value As Double)
            _AddCostPercent = value
        End Set
    End Property

    Private _AssessedValue As Double
    Public Property AssessedValue() As Double
        Get
            Return _AssessedValue
        End Get
        Set(ByVal value As Double)
            _AssessedValue = value
        End Set
    End Property

    Private _DutyPercent As Double
    Public Property DutyPercent() As Double
        Get
            Return _DutyPercent
        End Get
        Set(ByVal value As Double)
            _DutyPercent = value
        End Set
    End Property

    Private _Duty As Double
    Public Property Duty() As Double
        Get
            Return _Duty
        End Get
        Set(ByVal value As Double)
            _Duty = value
        End Set
    End Property
    Private _SaleTaxPercent As Double
    Public Property SaleTaxPercent() As Double
        Get
            Return _SaleTaxPercent
        End Get
        Set(ByVal value As Double)
            _SaleTaxPercent = value
        End Set
    End Property

    Private _SaleTax As Double
    Public Property SaleTax() As Double
        Get
            Return _SaleTax
        End Get
        Set(ByVal value As Double)
            _SaleTax = value
        End Set
    End Property

    Private _AddSaleTaxPercent As Double
    Public Property AddSaleTaxPercent() As Double
        Get
            Return _AddSaleTaxPercent
        End Get
        Set(ByVal value As Double)
            _AddSaleTaxPercent = value
        End Set
    End Property

    Private _AddSaleTax As Double
    Public Property AddSaleTax() As Double
        Get
            Return _AddSaleTax
        End Get
        Set(ByVal value As Double)
            _AddSaleTax = value
        End Set
    End Property

    Private _AdvIncomeTaxPercent As Double
    Public Property AdvIncomeTaxPercent() As Double
        Get
            Return _AdvIncomeTaxPercent
        End Get
        Set(ByVal value As Double)
            _AdvIncomeTaxPercent = value
        End Set
    End Property

    Private _AdvIncomeTax As Double
    Public Property AdvIncomeTax() As Double
        Get
            Return _AdvIncomeTax
        End Get
        Set(ByVal value As Double)
            _AdvIncomeTax = value
        End Set
    End Property


    Private _Comments As String
    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
        End Set
    End Property

    Private _PurchaseAccountId As Integer
    Public Property PurchaseAccountId() As Integer
        Get
            Return _PurchaseAccountId
        End Get
        Set(ByVal value As Integer)
            _PurchaseAccountId = value
        End Set
    End Property

    Private _LedgerComments As String

    Public Property LedgerComments() As String
        Get
            Return _LedgerComments
        End Get
        Set(ByVal value As String)
            _LedgerComments = value
        End Set
    End Property

    Private _Exch_Rate As Double
    Public Property Exch_Rate() As Double
        Get
            Return _Exch_Rate
        End Get
        Set(ByVal value As Double)
            _Exch_Rate = value
        End Set
    End Property

    Private _Import_Value As Double
    Public Property Import_Value() As Double
        Get
            Return _Import_Value
        End Get
        Set(ByVal value As Double)
            _Import_Value = value
        End Set
    End Property

    Private _ExciseDutyPercent As Double
    Public Property ExciseDutyPercent() As Double
        Get
            Return _ExciseDutyPercent
        End Get
        Set(ByVal value As Double)
            _ExciseDutyPercent = value
        End Set
    End Property

    Private _ExciseDuty As Double
    Public Property ExciseDuty() As Double
        Get
            Return _ExciseDuty
        End Get
        Set(ByVal value As Double)
            _ExciseDuty = value
        End Set
    End Property

    Private _Other_Charges As Double
    Public Property Other_Charges() As Double
        Get
            Return _Other_Charges
        End Get
        Set(ByVal value As Double)
            _Other_Charges = value
        End Set
    End Property

    Private _Net_Amount As Double
    Public Property Net_Amount() As Double
        Get
            Return _Net_Amount
        End Get
        Set(ByVal value As Double)
            _Net_Amount = value
        End Set
    End Property
    Private _AdditionalCostAccountId As Integer
    Public Property AdditionalCostAccountId() As Integer
        Get
            Return _AdditionalCostAccountId
        End Get
        Set(ByVal value As Integer)
            _AdditionalCostAccountId = value
        End Set
    End Property

    Private _CustomDutyAccountId As Integer
    Public Property CustomDutyAccountId() As Integer
        Get
            Return _CustomDutyAccountId
        End Get
        Set(ByVal value As Integer)
            _CustomDutyAccountId = value
        End Set
    End Property

    Private _SalesTaxAccountId As Integer
    Public Property SalesTaxAccountId() As Integer
        Get
            Return _SalesTaxAccountId
        End Get
        Set(ByVal value As Integer)
            _SalesTaxAccountId = value
        End Set
    End Property

    Private _AdditionalSalesTaxAccountId As Integer
    Public Property AdditionalSalesTaxAccountId() As Integer
        Get
            Return _AdditionalSalesTaxAccountId
        End Get
        Set(ByVal value As Integer)
            _AdditionalSalesTaxAccountId = value
        End Set
    End Property

    Private _AdvanceIncomeTaxAccountId As Integer
    Public Property AdvanceIncomeTaxAccountId() As Integer
        Get
            Return _AdvanceIncomeTaxAccountId
        End Get
        Set(ByVal value As Integer)
            _AdvanceIncomeTaxAccountId = value
        End Set
    End Property

    Private _ExciseDutyAccountId As Integer
    Public Property ExciseDutyAccountId() As Integer
        Get
            Return _ExciseDutyAccountId
        End Get
        Set(ByVal value As Integer)
            _ExciseDutyAccountId = value
        End Set
    End Property
    Private _PurchaseOrderId As Integer
    Public Property PurchaseOrderId() As Integer
        Get
            Return _PurchaseOrderId
        End Get
        Set(ByVal value As Integer)
            _PurchaseOrderId = value
        End Set
    End Property
    Private _PurchaseOrderDetailId As Integer
    Public Property PurchaseOrderDetailId() As Integer
        Get
            Return _PurchaseOrderDetailId
        End Get
        Set(ByVal value As Integer)
            _PurchaseOrderDetailId = value
        End Set
    End Property

    ''TASK TFS1609
    Private _Check_Other_Charges As Double
    Public Property Check_Other_Charges() As Double
        Get
            Return _Check_Other_Charges
        End Get
        Set(ByVal value As Double)
            _Check_Other_Charges = value
        End Set
    End Property
    ''END TASK TFS1609
    ''TASK TFS1753
    Private _ReceivingNoteId As Integer
    Public Property ReceivingNoteId() As Integer
        Get
            Return _ReceivingNoteId
        End Get
        Set(ByVal value As Integer)
            _ReceivingNoteId = value
        End Set
    End Property
    ''END TASK TFS1753
    ''TASK TFS1956
    Private _AddCustomDuty As Double
    Public Property AddCustomDuty() As Double
        Get
            Return _AddCustomDuty
        End Get
        Set(ByVal value As Double)
            _AddCustomDuty = value
        End Set
    End Property

    Private _AddCustomDutyPercent As Double
    Public Property AddCustomDutyPercent() As Double
        Get
            Return _AddCustomDutyPercent
        End Get
        Set(ByVal value As Double)
            _AddCustomDutyPercent = value
        End Set
    End Property

    Private _RegulatoryDuty As Double
    Public Property RegulatoryDuty() As Double
        Get
            Return _RegulatoryDuty
        End Get
        Set(ByVal value As Double)
            _RegulatoryDuty = value
        End Set
    End Property
    Private _RegulatoryDutyPercent As Double
    Public Property RegulatoryDutyPercent() As Double
        Get
            Return _RegulatoryDutyPercent
        End Get
        Set(ByVal value As Double)
            _RegulatoryDutyPercent = value
        End Set
    End Property
    ''Start TFS4163
    Private _BatchNo As String
    Public Property BatchNo() As String
        Get
            Return _BatchNo
        End Get
        Set(ByVal value As String)
            _BatchNo = value
        End Set
    End Property
    Private _ExpiryDate As DateTime
    Public Property ExpiryDate() As DateTime
        Get
            Return _ExpiryDate
        End Get
        Set(ByVal value As DateTime)
            _ExpiryDate = value
        End Set
    End Property
    ''End TFS4163

    ''END TASK TFS1956

    ' ''TASK-408 added TotalQty property on 11-06-2016

    'Private _TotalQty As Double
    'Public Property TotalQty() As Double
    '    Get
    '        Return _TotalQty
    '    End Get
    '    Set(ByVal value As Double)
    '        _TotalQty = value
    '    End Set
    'End Property
    ' ''End TASK-408

End Class


'Public Class LCOtherDetail

'    Private _ID As Integer
'    Public Property ID() As Integer
'        Get
'            Return _ID
'        End Get
'        Set(ByVal value As Integer)
'            _ID = value
'        End Set
'    End Property

'    Private _LCId As Integer
'    Public Property LCId() As Integer
'        Get
'            Return _LCId
'        End Get
'        Set(ByVal value As Integer)
'            _LCId = value
'        End Set
'    End Property

'    Private _RefNo As String
'    Public Property RefNo() As String
'        Get
'            Return _RefNo
'        End Get
'        Set(ByVal value As String)
'            _RefNo = value
'        End Set
'    End Property

'    Private _OpeningDate As DateTime
'    Public Property OpeningDate() As DateTime
'        Get
'            Return _OpeningDate
'        End Get
'        Set(ByVal value As DateTime)
'            _OpeningDate = value
'        End Set
'    End Property

'    Private _ExpiryDate As DateTime
'    Public Property ExpiryDate() As DateTime
'        Get
'            Return _ExpiryDate
'        End Get
'        Set(ByVal value As DateTime)
'            _ExpiryDate = value
'        End Set
'    End Property

'    Private _Amendment As String
'    Public Property Amendment() As String
'        Get
'            Return _Amendment
'        End Get
'        Set(ByVal value As String)
'            _Amendment = value
'        End Set
'    End Property


'    Private _OpeningBankAcId As Integer
'    Public Property OpeningBankAcId() As Integer
'        Get
'            Return _OpeningBankAcId
'        End Get
'        Set(ByVal value As Integer)
'            _OpeningBankAcId = value
'        End Set
'    End Property

'    Private _AdvisingBank As String
'    Public Property AdvisingBank() As String
'        Get
'            Return _AdvisingBank
'        End Get
'        Set(ByVal value As String)
'            _AdvisingBank = value
'        End Set
'    End Property

'    Private _SpecialInstruction As String
'    Public Property SepcialInstruction() As String
'        Get
'            Return _SpecialInstruction
'        End Get
'        Set(ByVal value As String)
'            _SpecialInstruction = value
'        End Set
'    End Property

'    Private _Remarks As String
'    Public Property Remarks() As String
'        Get
'            Return _Remarks
'        End Get
'        Set(ByVal value As String)
'            _Remarks = value
'        End Set
'    End Property

'    Private _OpenedBy As Integer
'    Public Property OpenedBy() As Integer
'        Get
'            Return _OpenedBy
'        End Get
'        Set(ByVal value As Integer)
'            _OpenedBy = value
'        End Set
'    End Property

'    Private _Vessel As String
'    Public Property Vessel() As String
'        Get
'            Return _Vessel
'        End Get
'        Set(ByVal value As String)
'            _Vessel = value
'        End Set
'    End Property

'    Private _BLNo As String
'    Public Property BLNo() As String
'        Get
'            Return _BLNo
'        End Get
'        Set(ByVal value As String)
'            _BLNo = value
'        End Set
'    End Property

'    Private _BLDate As DateTime
'    Public Property BLDate() As DateTime
'        Get
'            Return _BLDate
'        End Get
'        Set(ByVal value As DateTime)
'            _BLDate = value
'        End Set
'    End Property

'    Private _ETDDate As DateTime
'    Public Property ETDDate() As DateTime
'        Get
'            Return _ETDDate
'        End Get
'        Set(ByVal value As DateTime)
'            _ETDDate = value
'        End Set
'    End Property

'    Private _ETADate As DateTime
'    Public Property ETADate() As DateTime
'        Get
'            Return _ETADate
'        End Get
'        Set(ByVal value As DateTime)
'            _ETADate = value
'        End Set
'    End Property

'    Private _BankDocumentDate As DateTime
'    Public Property BankDocumentDate() As DateTime
'        Get
'            Return _BankDocumentDate
'        End Get
'        Set(ByVal value As DateTime)
'            _BankDocumentDate = value
'        End Set
'    End Property

'    Private _BankPaymentDate As DateTime
'    Public Property BankPaymentDate() As DateTime
'        Get
'            Return _BankPaymentDate
'        End Get
'        Set(ByVal value As DateTime)
'            _BankPaymentDate = value
'        End Set
'    End Property

'    Private _ClearingAgent As String
'    Public Property ClearingAgent() As String
'        Get
'            Return _ClearingAgent
'        End Get
'        Set(ByVal value As String)
'            _ClearingAgent = value
'        End Set
'    End Property

'    Private _TransporterId As Integer
'    Public Property TransporterId() As Integer
'        Get
'            Return _TransporterId
'        End Get
'        Set(ByVal value As Integer)
'            _TransporterId = value
'        End Set
'    End Property


'End Class
