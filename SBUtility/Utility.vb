'Imports System.Web.Mail

Public Class Utility

#Region "Constant and Enumerations"


    '>>>>>>>>>> Added by Syed Irfan Ahmad on 19 Feb 2018, Task: 2411 >>>>>>>>>>
    Public Enum MessageTypes
        Exception
        LogicalError
        UnexpectedSituation
        DeuggingPurpose
        Other
    End Enum

    Public Enum MessageCriticality
        High
        Medium
        Low
    End Enum
    '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

    Public Enum EnumHashTableKeyConstants

        GetUserFormList           'Config      
        GetLineItemList           'tblDeflLineItems 
        GetSizeList               'tblDefSizes
        GetCombinationList        'tblDefConbinitions
        GetCategoryList           'tblDefCategory
        GetSubCategoryList        'tblDefDefects
        GetProductGroupList       'tblDefProductGroups
        GetProductVariable1List   'tblDefAgeGrouops 
        GetProductVariable2List   'tblDefPackagingCodes
        GetProductVariable3List   'cboProductLifeType
        GetProductVariable4List   'cboProductGender
        GetProductVariable5List   'tblDefProductValueAdditionBy
        GetCityList               'tblDefCitites
        GetAreaList               'tblDefCityAreas 
        GetCreditCardList         'tblDefCreditCards
        GetEmployeeTypeList       'tblDefEmployeeType
        GetSupplierList           'tblDefSuppliers  
        GetCalendarSeasonList     'tblDefCalendarSeasons
        GetAccountHeadList        'tblDefAccountHeads  
        GetCustomerTypeList       'tblDefMemberTypes 
        GetUnitList               'cboDiscountTypes  
        GetShopList               'tblDefShops
        GetShopEmployeeList       'tblDefShopEmployees
        ''GetShopAccountList        'tblAccountShopBAB              (Not Needed)
        GetShopCreditCardList     'tblDefShopCreditCards
        GetShopPriorityList       'cboshoppriority
        GetShopTypeList           'tbldefshopgroups
        GetShopGroupList          'cboshoptype
        GetShopEmployeeTypeList   'tblDefEmployeeType
        GetCustomerGroupList      'tblDefMembershipGroups
        GetSystemConfigurationList 'tblRCMSConfiguration
        GetGLAccountList          'tblGlCOAMainSubSubDetail
        GetPaymentTypeList        'tblPaymentType  
        GetLanguageBasedControlList 'tblLanguageBasedControls
        GetPOStatusList                 'CboTableCollection
        GetDiscountTypeList                 'CboTableCollection
        GetDiscountChargeToList                 'CboTableCollection
        GetDiscountSharingList                 'CboTableCollection
        GetSecurityGroupTypeList                'CboTableCollection 
        GetSecurityGroupList            'tblSecurityGroup

        GetProductList            'tblDefProducts 
        GetSKUList          'tblDefProducts, tblDefProductItem, tblDefSizes, tblDefProductCombination, tblDefCombination
        GetSKUShopWiseList
        GetProductAttributeList

        GetProductPurchaseTypeList  'cboProductPurchaseType
        GetProductAcquireTypeList   'cboProductAcquireType
        GetProductManufecturedByList 'cboProductManufacturedBy
        GetInventoryLevelTemplateList 'tblDefProdInvTemplateDetails
        GetCustomerList     'tblMemberInfo
        GetMemberShipGroupList 'cboTableCollection
        GetDeginList 'cboTableCollection
        GetCostTypeList 'cboTableCollection
        GetUserShopList '
        GetGenderList
        GetComputerList 'tblComputerList
        GetStrPrintOptionList 'cboTableCollection
        GetProductHelpScreenList 'cboTableCollection
        GetEntrySequenceList 'cboTableCollection
        GetShopAccountTypeList 'cboTableCollection
        GetProductCodeDataTypeList 'cboTableCollection
        GetMushroomCommonList 'tblMushroomCommon 
        GetGLLocationList 'tblGlDefLocation
        GetDiscTypeList 'DiscountType List ..CboDiscount
        GetChargeToList 'CboChargeTo
        SetReportParametersList
        SetReportCriteria
        ReportViewOption
        GetUserList           'tblSecurityUser
        GetSupplierPaymentTypeList 'tblSuppPaymentType
        GetToolBarCustomizationList 'tblCustomToolbar
        GetOrderStatus 'CboOrderStatus
        GetTemplate 'tbldefTemplate

        GetCupPieceDiscountList     ''TblRCMSConfiguration ('CutPiece_Discount')

    End Enum

    Public Enum EnumProjectForms

        frmLogin
        MDIMain
        frmDefProduct
        frmDefShops
        frmDefCities
        frmDefCityAreas
        frmDefCategory
        frmDefCreditCards
        frmDefAgeGroups
        frmDefPackagingCodes
        frmDefLineItems

        frmDefUnits
        frmDefSubCategory
        frmSetItemAvgCost
        frmChangeProductNo_Range
        frmSetProductStatus
        frmInventorySnapshot
        frmMonthlyShopSalesReport
        frmWeeklyShopSalesReport
        frmShopInventoryExceeding
        frmInventoryLevelTemplate
        frmProductInventoryLevelAndBlock
        frmPoIntransit                      'F-11 Purchase Order Intransit Report
        frmGrdRptPoBalanc                   'F-01 Purchase Order Balance Report
        frmRptGRNReport                     'F-03 GRN Item Wise Report
        frmRptGRNDateWiseReport             'F-04 GRN Date Wise Report
        frmRptInTransitStock                'D-01  In-Transit Stock Report
        frmShopAutding                      'B-01 Shop Audit Report
        frmCrptProductAudit                 'B-02  Product Audit Report
        frmGrdRptBinCard                    'B-11  Bin Card Report
        frmTimeWiseShopSalesReport          'C-64  Clock Wise Shop Sales Report (Graphical)
        frmCrptMemberClubList               'H-02  Customer Club List Report
        frmMemberReport                     'H-01  Customer Report
        frmAtsDailyWiseReconciliation       'E-02  Daily wise reconciliation report new
        frmEmail                            'Utilities > Send Message

        frmDefMembershipTypes
        frmDefSizes
        frmDefCombinition
        frmSalesAndReturns
        frmToolbarCustomization
        frmAttributeSelectionHelp
        frmDefEmployeeType
        frmDefProductLifeType
        frmDefProductGroups
        frmDefCalendarSeasons
        frmDefProductGender
        frmDefCombinitions
        frmDefProductValueAdditionBy
        frmDefSuppliers
        frmProductsBlocked

        frmShopAccountingTransaction
        frmShopAccountClosing
        frmPhysicalAudit
        frmPhysicalAuditSearch
        frmPhysicalReversal
        frmPhysicalAuditSession
        frmDaySummaryReport

        frmPasswordChang
        frmSearchProduct
        frmDefProductCostPrice

        frmDefShopPriorityTemplate

        frmATSDailySalesReport 'frmSalesTaxReport
        frmDailySalesReport 'frmPeriodicSalesReportWithTax
        frmSalesTrendAnalysis

        frmProductLabelsNew 'frmProductBarcodes
        frmRptProductPriceNotDefined 'frmUndefinedPrice 
        frmDBBackup

        frmCutPieceDiscount

        frmPurchaseOrderNew
        frmSuppPurchaseReturn

        frmCustomerReceipt
        frmMemberClosing
        frmCustomerPayment
        frmSystemConfig
        frmGroup
        frmUser
        frmMembershipInfo
        frmFormManagement
        frmGroupShopsRight
        frmSalesReportNew 'C-11  Shop Sales Report , Reports > Sales Reports
        frmMembershipCardPrinting
        frmSuppPurchaseAndReturn
        frmMemberAddressBlockage
        frmSalesReportWiseSalePerson 'C-12 Sales Person Wise Sales Report , Reports > Sales Reports
        frmDefMembershipCard 'Membership Bulk Cards Generatioform
        frmCategoryShopSalesReport 'C-13  Category Wise Sales Report
        frmDayWiseSalesReport 'C-31  Day Wise Sales Report
        frmDiscounts
        frmShopsSalesPostionReport 'C-32
        frmShopWiseSales 'C-42 Shop wise sales summary report
        frmSalesAnalysisReport ' C-41 sales analysis report
        frmProductAvgCostListing ' G-01 Product Price With Profit Margin Report
        frmProductSpecificationReport ' G-03 Product Specifications Report
        frmShopClosingBalanceReport
        frmProductPriceChange ' G-04 Product Price Change Report
        frmRptSalesAndStock 'G-22
        frmRptPriceList 'G-02 Product Retail Price Report
        frmLedger 'Customer Ledger Report
        frmSupplierLedger   'Supplier Ledger Report
        frmRptIncomeStatement 'Shop Income Statement Report
        frmRptBusinessWorth 'Net Inventory Worth Report
        frmCrptReceivablePayable 'Receivable and Payable Report
        frmCrptMembershipJoining    'Customer/Member List Shop Wise Report 
        frmMemberSalesReport        'Memlber Sales and Receivable Report
        frmReplication      ' Form used to replicate between Server and Client
        frmCrptPurchaseAndReturnReport 'Grn invoice wise report

        frmShopDayWiseMovement
        frmLog 'log viewer


        frmProductAvgCost ' D-16 Inventory Average Cost ..
        frmRptAgeingReceivable ' A-25 Ageing Receivable Report ..
        frmRptAgeingPayable ' A-24 Ageing Payable Report ..
        frmDailySalesPostionReport ' A-23 Daily Cash Position Report ..
        frmShopsCCBanksReport ' A-22 Credit Card Bank Report .
        frmBDDateWiseReport ' A-21 Shop Bank Deposit Report ..
        frmShopMessages ' Shop Messages .. 
        frmERF ' Customer Claims Form .. 
        frmAtsMergArticleMovementNew ' Article Movement Report .. 
        frmCrptPosCashFlow ' POS Cash Flow .. (Shift Cash Flow .. )
        frmCrptShopDailyReport ' Shop Daily Closing ..   
        frmShopExpenseReport ' A06 ..
        frmShopWastages ' Shop Wastage .. 


        frmStrReportNew 'Process STR Screen
        frmPurchaseOrderSpreadSheet ' PO Matrix

        frmStrMasterOneToOne
        frmSuppPayments
        frmSuppAccountClosing
        frmSTRMaster1
        frmSTRManyToOne
        frmStrByInventoryLevel
        frmAtsFranchiseSalesReport ' I-01  Franchise Sales Report
        frmSuppPurchaseSpreadSheet
        frmAtsFranchiseDayWiseSalesReport ' I 02 Franchise Movement and Account Report
        frmShopIntertransferReport ' E21 Shop Inter Transfer Report
        frmSTRMasterView
        frmDefAccountHeads
        frmAcccountsJnLDisplay
        frmShopInventory
        frmShopInventoryAudit  'D-09
        frmShopInventorySpeadSheet

        frmShopProductStockViewer
        frmAtsDailyWiseReconcilation
        frmCutSizeReport 'D-27 Cut Size Report
        frmAlteration '--Customer Alteration
        frmWareHouseStcokTransferReport 'D-28 Ware House Stock Transfer
        frmProductRetailPriceChange
        frmProductAssemblyCreationDismantling

        ''The ForAllForms will be the last item in this enum
        ForAllForms = 9999
    End Enum

    Public Enum EnumDataMode
        ''None
        [New]
        Edit
        Disabled
        [ReadOnly]
    End Enum

    Public Enum EnumLanguagConstants
        ENGL_US = 1
        URDU_PK = 2
        ARABIC_UAE = 3


    End Enum

    Public Enum EnumProductHelpCallOn
        None
        Reports
        Forms
    End Enum

    Public Enum EnumProductHelpFor
        ProductList
        SKUList
        SKUShopWiseList
    End Enum

    Public Enum EnumProductHelp
        LineItemID
        ProductItemID
        ProductID
        SizeID
        CombinationID
        ProductCombinitionID
        ProductCode
        ProductName
        LineItemName
        Size
        SizeName
        Combination
        CombinationName
        Price
        CostPrice
        Stock
        VAT
        OtherCode
        IsUserDefine
        CustomerSKUCode
        CostType
    End Enum

    Public Enum EnumTransactionType
        CustReceipt = 1
        CustPayment = 2
        Purchase
        Payment
        [Return]
        Production
        Receipts
        ConsigPayment
        ConsigReceipts
    End Enum

    Public Enum EnumPaymentMode
        cashPayment = 1
        CreditPayment = 2
    End Enum

    Public Enum EnumSaleTransactionType
        Cash = 1
        CreditCard = 2
        Credit = 3
        Mixed = 4

    End Enum

    Public Enum EnumActions
        [Save] = 1
        Update = 2
        Delete = 3
    End Enum

#End Region

#Region "Public Shared Variables"

    Public Shared gObjMyAppHashTable As New Hashtable
    Public Shared gstrSystemLanguage As String  '' This variable is being used in GUI as well as DAL
    Public Shared gstrInputLanguage As String '' for input only

    ''Variable for Replication
    Public Shared gblnISPublisher As Boolean '' 
    Public Shared gblnIsReplicatedShop As Boolean ''
    Public Shared gblnIsReplicationDone As Boolean ''
    Public Shared gReportDataTable As DataTable
    Public Shared gintFirstWareHouseShopID As Integer
    Public Shared SmtpServer As String
    Public Shared MailSender As String

    ''Variable for Trial Version
    Public Shared IsTrialVersion As Boolean = False
    Public Shared pbBuisnessStartDate As Date = Date.Today.Date.AddYears(-2)
    Public Shared pbDateFormat As String = "yyyy-MM-dd"

    Public Const G_OPEN_FYEAR As String = "Open"                          'USE for Open financial year status
    '''''''''''
    Public Const G_VOUCHER_TYPE_JV As Integer = 4                                'Use for closing
    Public Const G_VOUCHER_ZERO As String = "000000"                                'Use for closing

    Public Shared G_SEPERATOR As String
    Public Shared G_PROFIT_LOSS_ACC_ID As Integer



#Region "Global Variables For All Application"
    Public Shared gintQtyRound As Integer = 2
    Public Shared gintAmountRound As Integer = 2

    '//Connection Info
    'Public Shared DBServerName As String = "Rai"
    'Public Shared DBName As String = "SimplePOS"
    'Public Shared DBUserName As String = "sa"
    'Public Shared DBPassword As String = "sa"

    Public Shared DBServerName As String = String.Empty '"Rai"
    Public Shared DBName As String = String.Empty '"SimplePOS"
    Public Shared DBUserName As String = String.Empty '"sa"
    Public Shared DBPassword As String = String.Empty '"sa"



#End Region


#Region "DAL Messages Variables"

    Public Shared gstrMsgDuplicateName As String = "Name Already Exists"
    Public Shared gstrMsgDuplicateCode As String = "Code Already Exists"
    Public Shared gstrMsgDuplicateValue As String = "Value Already Exists"
    Public Shared gstrMsgDependentRecordExist As String = "Dependent Record Exists, therefore the record can not be deleted."
    Public Shared gstrDuplicateUserID As String

#End Region

#End Region

#Region "Functions"


    Public Shared Function StringFixedLength(ByVal Text As String, ByVal Length As Integer) As String
        Try
            Dim ReturnString As String = String.Empty
            Dim str As String = String.Empty
            If Text.Trim.Length > Length Then
                str = Text.Substring(0, Length)
            Else
                str = String.Concat(Text, Microsoft.VisualBasic.Space(Length - Text.Trim.Length))
            End If
            ReturnString = str
            Return ReturnString
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function ConvertImageToBinary(ByVal image As System.Drawing.Image) As Byte()
        Try
            If image Is Nothing Then
                Return Nothing
            End If
            Dim ms As New System.IO.MemoryStream()
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Dim b() As Byte = ms.GetBuffer()
            Return b
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetFilterDataFromDataTable(ByVal objDataTable As DataTable, ByVal RecordFilterCriteria As String) As DataView
        Try


            'If objDataTable.Rows.Count > 0 Then
            Dim Dview As New DataView
            objDataTable.TableName = "Config"
            Dview.Table = objDataTable
            Dview.RowFilter = RecordFilterCriteria

            Return Dview

            'Else
            'Return Nothing

            'End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Shared Function GetSystemConfigurationValue(ByVal Key As String) As String
    '    Try


    '        Dim dv As DataView = GetFilterDataFromDataTable(CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetSystemConfigurationList.ToString), DataTable), "[Configuration Name] = '" & Key & "'")
    '        If Not dv Is Nothing Then
    '            If dv.Count > 0 Then
    '                Dim dr As DataRowView = dv(0)
    '                Return dr.Item("Configuration Value").ToString()
    '            End If
    '        End If
    '        Return String.Empty
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Public Shared Sub SendMail(ByVal strTo As String, ByVal strCC As String, ByVal strBCC As String, ByVal strSubject As String, ByVal strBody As String, ByVal strAttachments As String, ByVal IS_HTMLFormat As Boolean)

        ''send the email 
        'Try
        '    Dim insMail As New MailMessage()
        '    With insMail
        '        .From = MailSender
        '        .To = strTo
        '        .Subject = strSubject
        '        .Body = strBody
        '        .Cc = strCC
        '        .Bcc = strBCC
        '        If Not strAttachments.Equals(String.Empty) Then
        '            Dim strFile As String
        '            Dim strAttach() As String = strAttachments.Split(";")
        '            For Each strFile In strAttach
        '                .Attachments.Add(New MailAttachment(strFile.Trim()))
        '            Next
        '        End If
        '    End With

        '    If Not SmtpServer.Equals(String.Empty) Then
        '        SmtpMail.SmtpServer = SmtpServer
        '    End If

        '    SmtpMail.Send(insMail)

        'Catch ex As Exception
        '    Throw ex
        'End Try

    End Sub

    Public Shared Function IsNonReplicatedShop(ByVal ShopID As Integer) As Boolean
        Try

            Dim dt As DataTable
            dt = CType(gObjMyAppHashTable.Item(EnumHashTableKeyConstants.GetShopList.ToString()), DataTable).Copy
            Dim objDV As DataView = GetFilterDataFromDataTable(dt, "[Shop ID] = " & ShopID)

            If CBool(objDV.Item(0).Item("IsReplecatedShop").ToString) = False Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Encryption and Decryption"

    '////////////////////////////////////////////////////////////////////////
    '//     The code of this module was downloaded from net by Ghulam Ali
    ''//    and being used for encryption and decryption. The two methods used are:
    '//     1. DecryptWithALP and 2. EncryptWithALP
    '//     Main Encryption/Decryption module used throughout the application
    '//     The password of every user made in the application is stored after
    '/      encryption and at the login time this password is decrypted to get the
    '//     original password and match it with the user-given password.
    '////////////////////////////////////////////////////////////////////////


    Private Declare Function CryptAcquireContext Lib "advapi32.dll" Alias "CryptAcquireContextA" (ByRef phProv As Long, ByVal pszContainer As String, ByVal pszProvider As String, ByVal dwProvType As Long, ByVal dwFlags As Long) As Long
    Private Declare Function CryptGetProvParam Lib "advapi32.dll" (ByVal hProv As Long, ByVal dwParam As Long, ByRef pbData As VariantType, ByRef pdwDataLen As Long, ByVal dwFlags As Long) As Long
    Private Declare Function CryptCreateHash Lib "advapi32.dll" (ByVal hProv As Long, ByVal Algid As Long, ByVal hKey As Long, ByVal dwFlags As Long, ByRef phHash As Long) As Long
    Private Declare Function CryptHashData Lib "advapi32.dll" (ByVal hHash As Long, ByVal pbData As String, ByVal dwDataLen As Long, ByVal dwFlags As Long) As Long
    Private Declare Function CryptDeriveKey Lib "advapi32.dll" (ByVal hProv As Long, ByVal Algid As Long, ByVal hBaseData As Long, ByVal dwFlags As Long, ByRef phKey As Long) As Long
    Private Declare Function CryptDestroyHash Lib "advapi32.dll" (ByVal hHash As Long) As Long
    Private Declare Function CryptEncrypt Lib "advapi32.dll" (ByVal hKey As Long, ByVal hHash As Long, ByVal Final As Long, ByVal dwFlags As Long, ByVal pbData As String, ByRef pdwDataLen As Long, ByVal dwBufLen As Long) As Long
    Private Declare Function CryptDestroyKey Lib "advapi32.dll" (ByVal hKey As Long) As Long
    Private Declare Function CryptReleaseContext Lib "advapi32.dll" (ByVal hProv As Long, ByVal dwFlags As Long) As Long
    Private Declare Function CryptDecrypt Lib "advapi32.dll" (ByVal hKey As Long, ByVal hHash As Long, ByVal Final As Long, ByVal dwFlags As Long, ByVal pbData As String, ByRef pdwDataLen As Long) As Long
    Private Const SERVICE_PROVIDER As String = "Microsoft Base Cryptographic Provider v1.0"
    Private Const PROV_RSA_FULL As Long = 1
    Private Const PP_NAME As Long = 4
    Private Const PP_CONTAINER As Long = 6
    Private Const CRYPT_NEWKEYSET As Long = 8
    Private Const ALG_CLASS_DATA_ENCRYPT As Long = 24576
    Private Const ALG_CLASS_HASH As Long = 32768
    Private Const ALG_TYPE_ANY As Long = 0
    Private Const ALG_TYPE_STREAM As Long = 2048
    Private Const ALG_SID_RC4 As Long = 1
    Private Const ALG_SID_MD5 As Long = 3
    Private Const CALG_MD5 As Long = ((ALG_CLASS_HASH Or ALG_TYPE_ANY) Or ALG_SID_MD5)
    Private Const CALG_RC4 As Long = ((ALG_CLASS_DATA_ENCRYPT Or ALG_TYPE_STREAM) Or ALG_SID_RC4)
    Private Const ENCRYPT_ALGORITHM As Long = CALG_RC4
    Private Const ENCRYPT_NUMBERKEY As String = "16006833"
    Private Shared lngCryptProvider As Long
    Private Shared avarSeedValues As Object
    Private Shared lngSeedLevel As Long
    Private Shared lngDecryptPointer As Long
    Private Shared astrEncryptionKey(131) As String
    Private Const lngALPKeyLength As Long = 8
    Public Shared strKeyContainer As String
    Public Shared Function DecryptWithALP(ByVal strData As String) As String
        DecryptWithALP = String.Empty
        On Error GoTo Err
        Dim strALPKey As String = String.Empty
        Dim strALPKeyMask As String = String.Empty
        Dim lngIterator As Long = 0
        Dim blnOscillator As Boolean
        Dim strOutput As String = String.Empty
        Dim lngHex As Long = 0
        If Len(strData) = 0 Then
            Exit Function
        End If
        strALPKeyMask = Right$(New String("0", lngALPKeyLength) + DoubleToBinary(CLng("&H" + Left$(strData, 2))), lngALPKeyLength)
        strData = Right$(strData, Len(strData) - 2)
        For lngIterator = lngALPKeyLength To 1 Step -1
            If Mid$(strALPKeyMask, lngIterator, 1) = "1" Then
                strALPKey = Left$(strData, 1) + strALPKey
                strData = Right$(strData, Len(strData) - 1)
            Else
                strALPKey = Right$(strData, 1) + strALPKey
                strData = Left$(strData, Len(strData) - 1)
            End If
        Next lngIterator
        lngIterator = 0
        Do Until Len(strData) = 0
            blnOscillator = Not blnOscillator
            lngIterator = lngIterator + 1
            If lngIterator > lngALPKeyLength Then
                lngIterator = 1
            End If
            lngHex = IIf(blnOscillator, CLng("&H" + Left$(strData, 2) - Asc(Mid$(strALPKey, lngIterator, 1))), CLng("&H" + Left$(strData, 2) + Asc(Mid$(strALPKey, lngIterator, 1))))
            If lngHex > 255 Then
                lngHex = lngHex - 255
            ElseIf lngHex < 0 Then
                lngHex = lngHex + 255
            End If
            strOutput = strOutput + Chr(lngHex)
            strData = Right$(strData, Len(strData) - 2)
        Loop
        DecryptWithALP = strOutput
        Exit Function
Err:

    End Function
    Public Shared Function DecryptWithClipper(ByVal strData As String, ByVal strCryptKey As String) As String
        Dim strDecryptionChunk As String = String.Empty
        Dim strDecryptedText As String = String.Empty
        On Error Resume Next
        InitCrypt(strCryptKey)
        Do Until Len(strData) < 16
            strDecryptionChunk = ""
            strDecryptionChunk = Left$(strData, 16)
            strData = Right$(strData, Len(strData) - 16)
            If Len(strDecryptionChunk) > 0 Then
                strDecryptedText = strDecryptedText + PerformClipperDecryption(strDecryptionChunk)
            End If
        Loop
        DecryptWithClipper = strDecryptedText
    End Function
    Public Shared Function DecryptWithCSP(ByVal strData As String, ByVal strCryptKey As String) As String
        DecryptWithCSP = String.Empty
        On Error GoTo Err
        Dim lngEncryptionCount As Long
        Dim strDecrypted As String
        Dim strCurrentCryptKey As String
        If EncryptionCSPConnect() Then
            lngEncryptionCount = DecryptNumber(Mid$(strData, 1, 8))
            strCurrentCryptKey = strCryptKey & lngEncryptionCount
            strDecrypted = EncryptDecrypt(Mid$(strData, 9), strCurrentCryptKey, False)
            DecryptWithCSP = strDecrypted
            EncryptionCSPDisconnect()
        End If
        Exit Function
Err:
    End Function
    Public Shared Function EncryptWithALP(ByVal strData As String) As String
        EncryptWithALP = String.Empty
        On Error GoTo Err
        Dim strALPKey As String = String.Empty
        Dim strALPKeyMask As String = String.Empty
        Dim lngIterator As Long = 0
        Dim blnOscillator As Boolean
        Dim strOutput As String = String.Empty
        Dim lngHex As Long
        If Len(strData) = 0 Then
            Exit Function
        End If
        Randomize()
        For lngIterator = 1 To lngALPKeyLength
            strALPKey = strALPKey + Trim$(Hex$(Int(16 * Rnd())))
            strALPKeyMask = strALPKeyMask + Trim$(Int(2 * Rnd()))
        Next lngIterator
        lngIterator = 0
        Do Until Len(strData) = 0
            blnOscillator = Not blnOscillator
            lngIterator = lngIterator + 1
            If lngIterator > lngALPKeyLength Then
                lngIterator = 1
            End If
            lngHex = IIf(blnOscillator, CLng(Asc(Left$(strData, 1)) + Asc(Mid$(strALPKey, lngIterator, 1))), CLng(Asc(Left$(strData, 1)) - Asc(Mid$(strALPKey, lngIterator, 1))))
            If lngHex > 255 Then
                lngHex = lngHex - 255
            ElseIf lngHex < 0 Then
                lngHex = lngHex + 255
            End If
            strOutput = strOutput + Right$(New String("0", 2) + Hex$(lngHex), 2)
            strData = Right$(strData, Len(strData) - 1)
        Loop
        For lngIterator = 1 To lngALPKeyLength
            If Mid$(strALPKeyMask, lngIterator, 1) = "1" Then
                strOutput = Mid$(strALPKey, lngIterator, 1) + strOutput
            Else
                strOutput = strOutput + Mid$(strALPKey, lngIterator, 1)
            End If
        Next lngIterator
        EncryptWithALP = Right$(New String("0", 2) + Hex$(BinaryToDouble(strALPKeyMask)), 2) + strOutput
        Exit Function
Err:

    End Function
    Public Shared Function EncryptWithClipper(ByVal strData As String, ByVal strCryptKey As String) As String
        Dim strEncryptionChunk As String
        Dim strEncryptedText As String = String.Empty
        If Len(strData) > 0 Then
            InitCrypt(strCryptKey)
            Do Until Len(strData) = 0
                strEncryptionChunk = ""
                If Len(strData) > 6 Then
                    strEncryptionChunk = Left$(strData, 6)
                    strData = Right$(strData, Len(strData) - 6)
                Else
                    strEncryptionChunk = Left$(strData + Space(6), 6)
                    strData = ""
                End If
                If Len(strEncryptionChunk) > 0 Then
                    strEncryptedText = strEncryptedText + PerformClipperEncryption(strEncryptionChunk)
                End If
            Loop
        End If
        EncryptWithClipper = strEncryptedText
    End Function
    Public Shared Function EncryptWithCSP(ByVal strData As String, ByVal strCryptKey As String) As String
        EncryptWithCSP = String.Empty
        On Error GoTo Err
        Dim strEncrypted As String
        Dim lngEncryptionCount As Long
        Dim strCurrentCryptKey As String
        If EncryptionCSPConnect() Then
            lngEncryptionCount = 0
            strCurrentCryptKey = strCryptKey & lngEncryptionCount
            strEncrypted = EncryptDecrypt(strData, strCurrentCryptKey, True)
            Do While (InStr(1, strEncrypted, vbCr) > 0) Or (InStr(1, strEncrypted, vbLf) > 0) Or (InStr(1, strEncrypted, Chr(0)) > 0) Or (InStr(1, strEncrypted, vbTab) > 0)
                lngEncryptionCount = lngEncryptionCount + 1
                strCurrentCryptKey = strCryptKey & lngEncryptionCount
                strEncrypted = EncryptDecrypt(strData, strCurrentCryptKey, True)
                If lngEncryptionCount = 99999999 Then
                    Err.Raise(vbObjectError + 999, "EncryptWithCSP", "This Data cannot be successfully encrypted")
                    EncryptWithCSP = ""
                    Exit Function
                End If
            Loop
            EncryptWithCSP = EncryptNumber(lngEncryptionCount) & strEncrypted
            EncryptionCSPDisconnect()
        End If
        Exit Function
Err:

    End Function
    Public Shared Function GetCSPDetails() As String
        GetCSPDetails = String.Empty
        Dim lngDataLength As Long
        Dim bytContainer() As Byte
        If EncryptionCSPConnect() Then
            If lngCryptProvider = 0 Then
                GetCSPDetails = "Not connected to CSP"
                Exit Function
            End If
            lngDataLength = 1000
            ReDim bytContainer(lngDataLength)
            If CryptGetProvParam(lngCryptProvider, PP_NAME, bytContainer(0), lngDataLength, 0) <> 0 Then
                GetCSPDetails = "Cryptographic Service Provider name: " & ByteToString(bytContainer, lngDataLength)
            End If
            lngDataLength = 1000
            ReDim bytContainer(lngDataLength)
            If CryptGetProvParam(lngCryptProvider, PP_CONTAINER, bytContainer(0), lngDataLength, 0) <> 0 Then
                GetCSPDetails = GetCSPDetails & vbCrLf & "Key Container name: " & ByteToString(bytContainer, lngDataLength)
            End If
            EncryptionCSPDisconnect()
        Else
            GetCSPDetails = "Not connected to CSP"
        End If
    End Function
    Private Shared Function DecryptNumber(ByVal strData As String) As Long
        Dim lngIterator As Long
        For lngIterator = 1 To 8
            DecryptNumber = (10 * DecryptNumber) + (Asc(Mid$(strData, lngIterator, 1)) - Asc(Mid$(ENCRYPT_NUMBERKEY, lngIterator, 1)))
        Next lngIterator
    End Function
    Private Shared Function EncryptDecrypt(ByVal strData As String, ByVal strCryptKey As String, ByVal Encrypt As Boolean) As String
        EncryptDecrypt = String.Empty
        Dim lngDataLength As Long
        Dim strTempData As String
        Dim lngHaslngCryptKey As Long
        Dim lngCryptKey As Long
        If lngCryptProvider = 0 Then
            Err.Raise(vbObjectError + 999, "EncryptDecrypt", "Not connected to CSP")
            Exit Function
        End If
        If CryptCreateHash(lngCryptProvider, CALG_MD5, 0, 0, lngHaslngCryptKey) = 0 Then
            Err.Raise(vbObjectError + 999, "EncryptDecrypt", "Error during CryptCreateHash.")
        End If
        If CryptHashData(lngHaslngCryptKey, strCryptKey, Len(strCryptKey), 0) = 0 Then
            Err.Raise(vbObjectError + 999, "EncryptDecrypt", "Error during CryptHashData.")
        End If
        If CryptDeriveKey(lngCryptProvider, ENCRYPT_ALGORITHM, lngHaslngCryptKey, 0, lngCryptKey) = 0 Then
            Err.Raise(vbObjectError + 999, "EncryptDecrypt", "Error during CryptDeriveKey!")
        End If
        strTempData = strData
        lngDataLength = Len(strData)
        If Encrypt Then
            If CryptEncrypt(lngCryptKey, 0, 1, 0, strTempData, lngDataLength, lngDataLength) = 0 Then
                Err.Raise(vbObjectError + 999, "EncryptDecrypt", "Error during CryptEncrypt.")
            End If
        Else
            If CryptDecrypt(lngCryptKey, 0, 1, 0, strTempData, lngDataLength) = 0 Then
                Err.Raise(vbObjectError + 999, "EncryptDecrypt", "Error during CryptDecrypt.")
            End If
        End If
        EncryptDecrypt = Mid$(strTempData, 1, lngDataLength)
        If lngCryptKey <> 0 Then
            CryptDestroyKey(lngCryptKey)
        End If
        If lngHaslngCryptKey <> 0 Then
            CryptDestroyHash(lngHaslngCryptKey)
        End If
    End Function
    Private Shared Function EncryptionCSPConnect() As Boolean
        If Len(strKeyContainer) = 0 Then
            strKeyContainer = "FastTrack"
        End If
        If CryptAcquireContext(lngCryptProvider, strKeyContainer, SERVICE_PROVIDER, PROV_RSA_FULL, CRYPT_NEWKEYSET) = 0 Then
            If CryptAcquireContext(lngCryptProvider, strKeyContainer, SERVICE_PROVIDER, PROV_RSA_FULL, 0) = 0 Then
                Err.Raise(vbObjectError + 999, "EncryptionCSPConnect", "Error during CryptAcquireContext for a new key container." & vbCrLf & "A container with this name probably already exists.")
                EncryptionCSPConnect = False
                Exit Function
            End If
        End If
        EncryptionCSPConnect = True
    End Function
    Private Shared Function EncryptNumber(ByVal lngData As Long) As String
        EncryptNumber = String.Empty
        Dim lngIterator As Long
        Dim strData As String
        strData = Format$(lngData, "00000000")
        For lngIterator = 1 To 8
            EncryptNumber = EncryptNumber & Chr(Asc(Mid$(ENCRYPT_NUMBERKEY, lngIterator, 1)) + Val(Mid$(strData, lngIterator, 1)))
        Next lngIterator
    End Function
    Private Shared Sub EncryptionCSPDisconnect()
        If lngCryptProvider <> 0 Then
            CryptReleaseContext(lngCryptProvider, 0)
        End If
    End Sub
    Private Shared Sub InitCrypt(ByRef strEncryptionKey As String)
        Dim myAry() As String = {"A3", "D7", "09", "83", "F8", "48", "F6", "F4", "B3", "21", "15", "78", "99", "B1", "AF", _
        "F9", "E7", "2D", "4D", "8A", "CE", "4C", "CA", "2E", "52", "95", "D9", "1E", "4E", "38", "44", "28", "0A", "DF", _
        "02", "A0", "17", "F1", "60", "68", "12", "B7", "7A", "C3", "E9", "FA", "3D", "53", "96", "84", "6B", "BA", "F2", _
        "63", "9A", "19", "7C", "AE", "E5", "F5", "F7", "16", "6A", "A2", "39", "B6", "7B", "0F", "C1", "93", "81", "1B", _
        "EE", "B4", "1A", "EA", "D0", "91", "2F", "B8", "55", "B9", "DA", "85", "3F", "41", "BF", "E0", "5A", "58", "80", _
        "5F", "66", "0B", "D8", "90", "35", "D5", "C0", "A7", "33", "06", "65", "69", "45", "00", "94", "56", "6D", "98", _
        "9B", "76", "97", "FC", "B2", "C2", "B0", "FE", "DB", "20", "E1", "EB", "D6", "E4", "DD", "47", "4A", "1D", "42", _
        "ED", "9E", "6E", "49", "3C", "CD", "43", "27", "D2", "07", "D4", "DE", "C7", "67", "18", "89", "CB", "30", "1F", _
        "8D", "C6", "8F", "AA", "C8", "74", "DC", "C9", "5D", "5C", "31", "A4", "70", "88", "61", "2C", "9F", "0D", "2B", _
        "87", "50", "82", "54", "64", "26", "7D", "03", "40", "34", "4B", "1C", "73", "D1", "C4", "FD", "3B", "CC", "FB", _
        "7F", "AB", "E6", "3E", "5B", "A5", "AD", "04", "23", "9C", "14", "51", "22", "F0", "29", "79", "71", "7E", "FF", _
        "8C", "0E", "E2", "0C", "EF", "BC", "72", "75", "6F", "37", "A1", "EC", "D3", "8E", "62", "8B", "86", "10", "E8", _
        "08", "77", "11", "BE", "92", "4F", "24", "C5", "32", "36", "9D", "CF", "F3", "A6", "BB", "AC", "5E", "6C", "A9", _
        "13", "57", "25", "B5", "E3", "BD", "A8", "3A", "01", "05", "59", "2A", "46"}

        avarSeedValues = CType(myAry, Object)
        SetKey(strEncryptionKey)
    End Sub
    Private Shared Function PerformClipperDecryption(ByVal strData As String) As String
        Dim bytChunk(4, 32) As String
        Dim bytCounter(0 To 32) As Byte
        Dim lngIterator As Long
        Dim strDecryptedData As String
        On Error Resume Next
        bytChunk(1, 32) = Mid(strData, 1, 4)
        bytChunk(2, 32) = Mid(strData, 5, 4)
        bytChunk(3, 32) = Mid(strData, 9, 4)
        bytChunk(4, 32) = Mid(strData, 13, 4)
        lngSeedLevel = 32
        lngDecryptPointer = 31
        For lngIterator = 0 To 32
            bytCounter(lngIterator) = lngIterator + 1
        Next lngIterator
        For lngIterator = 1 To 8
            bytChunk(1, lngSeedLevel - 1) = PerformClipperDecryptionChunk(bytChunk(2, lngSeedLevel), astrEncryptionKey)
            bytChunk(2, lngSeedLevel - 1) = PerformXOR(PerformClipperDecryptionChunk(bytChunk(2, lngSeedLevel), astrEncryptionKey), PerformXOR(bytChunk(3, lngSeedLevel), Hex(bytCounter(lngSeedLevel - 1))))
            bytChunk(3, lngSeedLevel - 1) = bytChunk(4, lngSeedLevel)
            bytChunk(4, lngSeedLevel - 1) = bytChunk(1, lngSeedLevel)
            lngDecryptPointer = lngDecryptPointer - 1
            lngSeedLevel = lngSeedLevel - 1
        Next lngIterator
        For lngIterator = 1 To 8
            bytChunk(1, lngSeedLevel - 1) = PerformClipperDecryptionChunk(bytChunk(2, lngSeedLevel), astrEncryptionKey)
            bytChunk(2, lngSeedLevel - 1) = bytChunk(3, lngSeedLevel)
            bytChunk(3, lngSeedLevel - 1) = bytChunk(4, lngSeedLevel)
            bytChunk(4, lngSeedLevel - 1) = PerformXOR(PerformXOR(bytChunk(1, lngSeedLevel), bytChunk(2, lngSeedLevel)), Hex(bytCounter(lngSeedLevel - 1)))
            lngDecryptPointer = lngDecryptPointer - 1
            lngSeedLevel = lngSeedLevel - 1
        Next lngIterator
        For lngIterator = 1 To 8
            bytChunk(1, lngSeedLevel - 1) = PerformClipperDecryptionChunk(bytChunk(2, lngSeedLevel), astrEncryptionKey)
            bytChunk(2, lngSeedLevel - 1) = PerformXOR(PerformClipperDecryptionChunk(bytChunk(2, lngSeedLevel), astrEncryptionKey), PerformXOR(bytChunk(3, lngSeedLevel), Hex(bytCounter(lngSeedLevel - 1))))
            bytChunk(3, lngSeedLevel - 1) = bytChunk(4, lngSeedLevel)
            bytChunk(4, lngSeedLevel - 1) = bytChunk(1, lngSeedLevel)
            lngDecryptPointer = lngDecryptPointer - 1
            lngSeedLevel = lngSeedLevel - 1
        Next lngIterator
        For lngIterator = 1 To 8
            bytChunk(1, lngSeedLevel - 1) = PerformClipperDecryptionChunk(bytChunk(2, lngSeedLevel), astrEncryptionKey)
            bytChunk(2, lngSeedLevel - 1) = bytChunk(3, lngSeedLevel)
            bytChunk(3, lngSeedLevel - 1) = bytChunk(4, lngSeedLevel)
            bytChunk(4, lngSeedLevel - 1) = PerformXOR(PerformXOR(bytChunk(1, lngSeedLevel), bytChunk(2, lngSeedLevel)), Hex(bytCounter(lngSeedLevel - 1)))
            lngDecryptPointer = lngDecryptPointer - 1
            lngSeedLevel = lngSeedLevel - 1
        Next lngIterator
        strDecryptedData = HexToString(bytChunk(1, 0) & bytChunk(2, 0) & bytChunk(3, 0) & bytChunk(4, 0))
        If InStr(strDecryptedData, Chr(0)) > 0 Then
            strDecryptedData = Left$(strDecryptedData, InStr(strDecryptedData, Chr(0)) - 1)
        End If
        PerformClipperDecryption = strDecryptedData
    End Function
    Private Shared Function PerformClipperDecryptionChunk(ByVal strData As String, ByRef strEncryptionKey() As String) As String
        Dim astrDecryptionLevel(6) As String
        Dim strDecryptedString As String
        astrDecryptionLevel(5) = Mid(strData, 1, 2)
        astrDecryptionLevel(6) = Mid(strData, 3, 2)
        strDecryptedString = avarSeedValues(CByte(PerformTranslation(PerformXOR(astrDecryptionLevel(5), strEncryptionKey((4 * lngDecryptPointer) + 3)))))
        astrDecryptionLevel(4) = PerformXOR(strDecryptedString, astrDecryptionLevel(6))
        strDecryptedString = avarSeedValues(CByte(PerformTranslation(PerformXOR(astrDecryptionLevel(4), strEncryptionKey((4 * lngDecryptPointer) + 2)))))
        astrDecryptionLevel(3) = PerformXOR(strDecryptedString, astrDecryptionLevel(5))
        strDecryptedString = avarSeedValues(CByte(PerformTranslation(PerformXOR(astrDecryptionLevel(3), strEncryptionKey((4 * lngDecryptPointer) + 1)))))
        astrDecryptionLevel(2) = PerformXOR(strDecryptedString, astrDecryptionLevel(4))
        strDecryptedString = avarSeedValues(CByte(PerformTranslation(PerformXOR(astrDecryptionLevel(2), strEncryptionKey(4 * lngDecryptPointer)))))
        astrDecryptionLevel(1) = PerformXOR(strDecryptedString, astrDecryptionLevel(3))
        strDecryptedString = astrDecryptionLevel(1) & astrDecryptionLevel(2)
        PerformClipperDecryptionChunk = strDecryptedString
    End Function
    Private Shared Function PerformClipperEncryption(ByVal strData As String) As String
        Dim bytChunk(4, 32) As String
        Dim lngCounter As Long
        Dim lngIterator As Long
        On Error Resume Next
        strData = StringToHex(strData)
        bytChunk(1, 0) = Mid(strData, 1, 4)
        bytChunk(2, 0) = Mid(strData, 5, 4)
        bytChunk(3, 0) = Mid(strData, 9, 4)
        bytChunk(4, 0) = Mid(strData, 13, 4)
        lngSeedLevel = 0
        lngCounter = 1
        For lngIterator = 1 To 8
            bytChunk(1, lngSeedLevel + 1) = PerformXOR(PerformXOR(PerformClipperEncryptionChunk(bytChunk(1, lngSeedLevel), astrEncryptionKey), bytChunk(4, lngSeedLevel)), Hex(lngCounter))
            bytChunk(2, lngSeedLevel + 1) = PerformClipperEncryptionChunk(bytChunk(1, lngSeedLevel), astrEncryptionKey)
            bytChunk(3, lngSeedLevel + 1) = bytChunk(2, lngSeedLevel)
            bytChunk(4, lngSeedLevel + 1) = bytChunk(3, lngSeedLevel)
            lngCounter = lngCounter + 1
            lngSeedLevel = lngSeedLevel + 1
        Next lngIterator
        For lngIterator = 1 To 8
            bytChunk(1, lngSeedLevel + 1) = bytChunk(4, lngSeedLevel)
            bytChunk(2, lngSeedLevel + 1) = PerformClipperEncryptionChunk(bytChunk(1, lngSeedLevel), astrEncryptionKey)
            bytChunk(3, lngSeedLevel + 1) = PerformXOR(PerformXOR(bytChunk(1, lngSeedLevel), bytChunk(2, lngSeedLevel)), Hex(lngCounter))
            bytChunk(4, lngSeedLevel + 1) = bytChunk(3, lngSeedLevel)
            lngCounter = lngCounter + 1
            lngSeedLevel = lngSeedLevel + 1
        Next lngIterator
        For lngIterator = 1 To 8
            bytChunk(1, lngSeedLevel + 1) = PerformXOR(PerformXOR(PerformClipperEncryptionChunk(bytChunk(1, lngSeedLevel), astrEncryptionKey), bytChunk(4, lngSeedLevel)), Hex(lngCounter))
            bytChunk(2, lngSeedLevel + 1) = PerformClipperEncryptionChunk(bytChunk(1, lngSeedLevel), astrEncryptionKey)
            bytChunk(3, lngSeedLevel + 1) = bytChunk(2, lngSeedLevel)
            bytChunk(4, lngSeedLevel + 1) = bytChunk(3, lngSeedLevel)
            lngCounter = lngCounter + 1
            lngSeedLevel = lngSeedLevel + 1
        Next lngIterator
        For lngIterator = 1 To 8
            bytChunk(1, lngSeedLevel + 1) = bytChunk(4, lngSeedLevel)
            bytChunk(2, lngSeedLevel + 1) = PerformClipperEncryptionChunk(bytChunk(1, lngSeedLevel), astrEncryptionKey)
            bytChunk(3, lngSeedLevel + 1) = PerformXOR(PerformXOR(bytChunk(1, lngSeedLevel), bytChunk(2, lngSeedLevel)), Hex(lngCounter))
            bytChunk(4, lngSeedLevel + 1) = bytChunk(3, lngSeedLevel)
            lngCounter = lngCounter + 1
            lngSeedLevel = lngSeedLevel + 1
        Next lngIterator
        PerformClipperEncryption = bytChunk(1, 32) & bytChunk(2, 32) & bytChunk(3, 32) & bytChunk(4, 32)
    End Function
    Private Shared Function PerformClipperEncryptionChunk(ByVal strData As String, ByRef strEncryptionKey() As String) As String
        Dim astrEncryptionLevel(6) As String
        Dim strEncryptedString As String
        astrEncryptionLevel(1) = Mid(strData, 1, 2)
        astrEncryptionLevel(2) = Mid(strData, 3, 2)
        strEncryptedString = avarSeedValues(CByte(PerformTranslation(PerformXOR(astrEncryptionLevel(2), strEncryptionKey(4 * lngSeedLevel)))))
        astrEncryptionLevel(3) = PerformXOR(strEncryptedString, astrEncryptionLevel(1))
        strEncryptedString = avarSeedValues(CByte(PerformTranslation(PerformXOR(astrEncryptionLevel(3), strEncryptionKey((4 * lngSeedLevel) + 1)))))
        astrEncryptionLevel(4) = PerformXOR(strEncryptedString, astrEncryptionLevel(2))
        strEncryptedString = avarSeedValues(CByte(PerformTranslation(PerformXOR(astrEncryptionLevel(4), strEncryptionKey((4 * lngSeedLevel) + 2)))))
        astrEncryptionLevel(5) = PerformXOR(strEncryptedString, astrEncryptionLevel(3))
        strEncryptedString = avarSeedValues(CByte(PerformTranslation(PerformXOR(astrEncryptionLevel(5), strEncryptionKey((4 * lngSeedLevel) + 3)))))
        astrEncryptionLevel(6) = PerformXOR(strEncryptedString, astrEncryptionLevel(4))
        strEncryptedString = astrEncryptionLevel(5) & astrEncryptionLevel(6)
        PerformClipperEncryptionChunk = strEncryptedString
    End Function
    Private Shared Function PerformTranslation(ByVal strData As String) As Double
        Dim strTranslationString As String
        Dim strTranslationChunk As String
        Dim lngTranslationIterator As Long
        Dim lngHexConversion As Long
        Dim lngHexConversionIterator As Long
        Dim dblTranslation As Double
        Dim lngTranslationMarker As Long
        Dim lngTranslationModifier As Long
        Dim lngTranslationLayerModifier As Long
        strTranslationString = strData
        strTranslationString = Right$(strTranslationString, 8)
        strTranslationChunk = New String("0", 8 - Len(strTranslationString)) + strTranslationString
        strTranslationString = ""
        For lngTranslationIterator = 1 To 8
            lngHexConversion = Val("&H" + Mid$(strTranslationChunk, lngTranslationIterator, 1))
            For lngHexConversionIterator = 3 To 0 Step -1
                If lngHexConversion And 2 ^ lngHexConversionIterator Then
                    strTranslationString = strTranslationString + "1"
                Else
                    strTranslationString = strTranslationString + "0"
                End If
            Next lngHexConversionIterator
        Next lngTranslationIterator
        dblTranslation = 0
        For lngTranslationIterator = Len(strTranslationString) To 1 Step -1
            If Mid(strTranslationString, lngTranslationIterator, 1) = "1" Then
                lngTranslationLayerModifier = 1
                lngTranslationMarker = (Len(strTranslationString) - lngTranslationIterator)
                lngTranslationModifier = 2
                Do While lngTranslationMarker > 0
                    Do While (lngTranslationMarker / 2) = (lngTranslationMarker \ 2)
                        lngTranslationModifier = (lngTranslationModifier * lngTranslationModifier) Mod 255
                        lngTranslationMarker = lngTranslationMarker / 2
                    Loop
                    lngTranslationLayerModifier = (lngTranslationModifier * lngTranslationLayerModifier) Mod 255
                    lngTranslationMarker = lngTranslationMarker - 1
                Loop
                dblTranslation = dblTranslation + lngTranslationLayerModifier
            End If
        Next lngTranslationIterator
        PerformTranslation = dblTranslation
    End Function
    Private Shared Function PerformXOR(ByVal strData As String, ByVal strMask As String) As String
        Dim strXOR As String = String.Empty
        Dim lngXORIterator As Long
        Dim lngXORMarker As Long
        lngXORMarker = Len(strData) - Len(strMask)
        If lngXORMarker < 0 Then
            strXOR = Left$(strMask, Math.Abs(lngXORMarker))
            strMask = Mid$(strMask, Math.Abs(lngXORMarker) + 1)
        ElseIf lngXORMarker > 0 Then
            strXOR = Left$(strData, Math.Abs(lngXORMarker))
            strData = Mid$(strData, lngXORMarker + 1)
        End If
        For lngXORIterator = 1 To Len(strData)
            strXOR = strXOR + Hex$(Val("&H" + Mid$(strData, lngXORIterator, 1)) Xor Val("&H" + Mid$(strMask, lngXORIterator, 1)))
        Next lngXORIterator
        PerformXOR = Right(strXOR, 8)
    End Function
    Private Shared Sub SetKey(ByVal strEncryptionKey As String)
        Dim intEncryptionKeyIterator As Integer
        For intEncryptionKeyIterator = 0 To 131 Step 10
            If intEncryptionKeyIterator = 130 Then
                astrEncryptionKey(intEncryptionKeyIterator + 0) = Mid(strEncryptionKey, 1, 2)
                astrEncryptionKey(intEncryptionKeyIterator + 1) = Mid(strEncryptionKey, 3, 2)
            Else
                astrEncryptionKey(intEncryptionKeyIterator + 0) = Mid(strEncryptionKey, 1, 2)
                astrEncryptionKey(intEncryptionKeyIterator + 1) = Mid(strEncryptionKey, 3, 2)
                astrEncryptionKey(intEncryptionKeyIterator + 2) = Mid(strEncryptionKey, 5, 2)
                astrEncryptionKey(intEncryptionKeyIterator + 3) = Mid(strEncryptionKey, 7, 2)
                astrEncryptionKey(intEncryptionKeyIterator + 4) = Mid(strEncryptionKey, 9, 2)
                astrEncryptionKey(intEncryptionKeyIterator + 5) = Mid(strEncryptionKey, 11, 2)
                astrEncryptionKey(intEncryptionKeyIterator + 6) = Mid(strEncryptionKey, 13, 2)
                astrEncryptionKey(intEncryptionKeyIterator + 7) = Mid(strEncryptionKey, 15, 2)
                astrEncryptionKey(intEncryptionKeyIterator + 8) = Mid(strEncryptionKey, 17, 2)
                astrEncryptionKey(intEncryptionKeyIterator + 9) = Mid(strEncryptionKey, 19, 2)
            End If
        Next
    End Sub


    Private Shared Function BinaryToDouble(ByVal strData As String) As Double
        Dim dblOutput As Double
        Dim lngIterator As Long
        Do Until Len(strData) = 0
            dblOutput = dblOutput + IIf(Right$(strData, 1) = "1", (2 ^ lngIterator), 0)
            strData = Left$(strData, Len(strData) - 1)
            lngIterator = lngIterator + 1
        Loop
        BinaryToDouble = dblOutput
    End Function

    Private Shared Function DoubleToBinary(ByVal dblData As Double) As String
        Dim strOutput As String = String.Empty
        Dim lngIterator As Long
        Do Until (2 ^ lngIterator) > dblData
            strOutput = IIf(((2 ^ lngIterator) And dblData) > 0, "1", "0") + strOutput
            lngIterator = lngIterator + 1
        Loop
        DoubleToBinary = strOutput
    End Function
    Private Shared Function HexToString(ByVal strData As String) As String
        Dim strOutput As String = String.Empty
        Do Until Len(strData) < 2
            strOutput = strOutput + Chr(CLng("&H" + Left$(strData, 2)))
            strData = Right$(strData, Len(strData) - 2)
        Loop
        HexToString = strOutput
    End Function

    Private Shared Function StringToHex(ByVal strData As String) As String
        Dim strOutput As String = String.Empty
        Do Until Len(strData) = 0
            strOutput = strOutput + Right$(New String("0", 2) + Hex$(Asc(Left$(strData, 1))), 2)
            strData = Right$(strData, Len(strData) - 1)
        Loop
        StringToHex = strOutput
    End Function
    Private Shared Function ByteToString(ByRef bytData() As Byte, ByVal lngDataLength As Long) As VariantType
        Dim lngIterator As Long
        For lngIterator = LBound(bytData) To (LBound(bytData) + lngDataLength)
            ByteToString = ByteToString & Chr(bytData(lngIterator))
        Next lngIterator
    End Function

#End Region

#Region ".Net Encryption"
    Public Class SymmetricEncryption

        Public Shared Function Encrypt(ByVal strData As String) As String
            'Try
            '    Dim strEncryptedData As String

            '    Dim sym As New EncryptionClassLibrary.Encryption.Symmetric(EncryptionClassLibrary.Encryption.Symmetric.Provider.Rijndael)
            '    Dim key As New EncryptionClassLibrary.Encryption.Data(strKey)
            '    Dim encryptedData As EncryptionClassLibrary.Encryption.Data
            '    encryptedData = sym.Encrypt(New EncryptionClassLibrary.Encryption.Data(strData), key)
            '    strEncryptedData = encryptedData.Hex.ToString

            '    Return strEncryptedData

            'Catch ex As Exception
            '    Throw ex
            'End Try

            Dim strEncrKey As String = "simpleaccounts"
            Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
            Try
                Dim bykey() As Byte = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Left(strEncrKey, 8))
                Dim InputByteArray() As Byte = System.Text.Encoding.UTF8.GetBytes(strData)
                Dim des As New Security.Cryptography.DESCryptoServiceProvider
                Dim ms As New IO.MemoryStream
                Dim cs As New Security.Cryptography.CryptoStream(ms, des.CreateEncryptor(bykey, IV), Security.Cryptography.CryptoStreamMode.Write)
                cs.Write(InputByteArray, 0, InputByteArray.Length)
                cs.FlushFinalBlock()
                Return Convert.ToBase64String(ms.ToArray())
            Catch ex As Exception
                Return ex.Message
            End Try

        End Function

        Public Shared Function Decrypt(ByVal strEncryptedData As String) As String
            'Try
            'Dim strDecryptedData As String

            'Dim sym As New EncryptionClassLibrary.Encryption.Symmetric(EncryptionClassLibrary.Encryption.Symmetric.Provider.Rijndael)
            'Dim key As New EncryptionClassLibrary.Encryption.Data(strKey)
            'Dim encryptedData As New EncryptionClassLibrary.Encryption.Data
            'encryptedData.Hex = strEncryptedData
            'Dim decryptedData As EncryptionClassLibrary.Encryption.Data
            'decryptedData = sym.Decrypt(encryptedData, key)

            'strDecryptedData = decryptedData.ToString

            'Return strDecryptedData


            Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
            Dim sDecrKey As String = "simpleaccounts"
            Dim inputByteArray(strEncryptedData.Length) As Byte
            Try
                Dim byKey() As Byte = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Left(sDecrKey, 8))
                Dim des As New Security.Cryptography.DESCryptoServiceProvider
                inputByteArray = Convert.FromBase64String(strEncryptedData)
                Dim ms As New IO.MemoryStream
                Dim cs As New Security.Cryptography.CryptoStream(ms, des.CreateDecryptor(byKey, IV), Security.Cryptography.CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
                Return encoding.GetString(ms.ToArray())
            Catch ex As Exception
                Return ex.Message
            End Try

            'Catch ex As Exception
            '    Throw ex
            'End Try
        End Function
    End Class
#End Region




End Class
