Imports SBModel


Module ModUtlVariables


#Region "Public Variables"

    Public gobjMyImageListForOperationBar As System.Windows.Forms.ImageList
    ' Public gObjUserInfo As SecurityUser
    '  Public gObjFinancialYearInfo As FiniancialYear
    Public gobjBusinessStartDate As Date
    ' Public gobjLocationInfo As Company
    Public G_START_DATE As String
    Public G_END_DATE As String
    Public pbIsCallFromSearchForm As Boolean = False


    Public gObjToolTip As New ToolTip

    Public gEnumIsRightToLeft As Windows.Forms.RightToLeft


    Public gintProductHelpLeftLocation As Integer = 0
    Public gintProductHelpTopLocation As Integer = 0
    Public gstrReportPath As String = Application.StartupPath & "\Reports\" '"D:\OfficeWork\GLNet\GLNet\bin\Debug\" ' Tariq .. 
    Public str_ApplicationStartUpPath1 As String

#End Region

#Region "Global Variables For UI"

    Public RegisterStatus As EnumRegisterStatus = EnumRegisterStatus.Trial

    '  Public _blnTempVouchers As Boolean = False
    Public _blnCallFromSearchPost As Boolean = False
    Public _gstrVoucherIDs As String = String.Empty

    Public _TempVouchers As Boolean = False

    Public gstrMsgTitle As String = "SIRIUS BUSINESS SOLUTIONS"

    ''DML Confirmation Messages
    Public gblnShowSaveConfirmationMessages As Boolean = True
    Public gblnShowAfterUpdateMessages As Boolean = True

    Public gstrComboZeroIndexString As String = "---Select---"

    Public gstrMsgSave As String = "Do you want to save...?"
    Public gstrMsgUpdate As String = "Do you want to Update...?"
    Public gstrMsgAfterUpdate As String = "Record updated successfully"
    Public gstrMsgAfterSave As String = "Record saved successfully"
    Public gstrMsgDelete As String = "Do you want to Delete...?"
    Public gstrMsgExit As String
    Public gstrValueRequired As String
    Public gstrMsgRecordNotFound As String = "No record found in the grid ...."
    Public gstrMsgAfterDelete As String = "Record deleted successfully"
    Public gstrMsgDependentRecordFound As String = "Sorry record can't be deleted because dependent record exist..."
    Public gstrMsgPasswordMatch As String
    Public gstrMsgDeleteByEndDate As String



    ''Validation Messages
    Public gstrMsgNameRequired As String
    Public gstrMsgCodeRequired As String
    Public gstrMsgComboSelectionRequired As String
    Public gstrMsgNumericRequired As String
    Public gstrMsgWrongInput As String
    Public gstrMsgRecordRequiredInGrid As String = "There must be one records in grid"

    ''After Export to excel or to CSV Messages
    Public gstrMsgAfterExport As String
    Public gstrMsgAfterCSV As String
    Public gstrMsgAfterPrint As String
    Public gstrMsgAfterPDF As String

    ''Security Rights Messages
    Public gstrMsgRightToViewOption As String

    '' Message variables declare by Khalid
    Public gMsgSTRUpdation As String
    Public gMsgAssPrdGrdPopulate As String
    Public gMsgAssRevertPhyAudit As String
    Public gMsgBatchCntZero As String
    Public gMsgNtFoundBussStartDt As String
    Public gMsgCntDeletePO As String
    Public gMsgCntDeleteLastClDt As String
    Public gMsgCntUpdClosedPO As String
    Public gMsgCntUpdShopInv As String
    Public gMsgMustSelectCC As String
    Public gMsgCustSignNtSaved As String
    Public gMsgCClaimsPosted As String
    Public gMsgCustJoinDtSmallThnShopClDt As String
    Public gMsgDBBackupSucc As String
    Public gMsgDBRestoredSucc As String
    Public gMsgPODtCntLessThnClDt As String
    Public gMsgDtRangeCntBeGreaterThnMonth As String
    Public gMsgDelNtAllowed As String
    Public gMsgDeliveryDtCntLesThnOrdDt As String
    Public gMsgDeliveryDtMustGrtThenOrdDt As String
    Public gMsgSTRGenerateFailedDelivMedia As String
    Public gMsgAssuranceSendEmail As String
    Public gMsgAssuranceDeleteEmptyRows As String
    Public gMsgAssuranceDelHoldInvoice As String
    Public gMsgAssuranceToHoldInv As String
    Public gMsgAssuranceUpdtHoldInv As String
    Public gMsgDOBCntGrtThnCDt As String
    Public gMsgDueDtMustGrtThnInvDt As String
    Public gMsgEmailSentSucc As String
    Public gMsgEnterValidComb As String
    Public gMsgFrmDtMustLesThnToDt As String
    Public gMsgEnterValidPrc As String
    Public gMSgEnterValidPassPort As String
    Public gMsgEnterValidRecDt As String
    Public gMsgEnterValidSize As String
    Public gMsgEnterValidInvNo As String
    Public gMsgEnterPrdCdInGrdCnt As String
    Public gMsgEnterReturnInvNo As String
    Public gMsgEnterValidDispDt As String
    Public gMsgSTRCompleteAdjus As String
    Public gMsgDefNewLineItems As String
    Public gMsgDefSuppOpenBal As String
    Public gMsgSelectDistinationShop As String
    Public gMsgFrmTimeMustLesThnToTime As String
    Public gMsgSTRFurtherNotAllowed As String
    Public gMsgInvalidPassword As String
    Public gMsgInvalidPasswordTryAgain As String
    Public gMsgInvalidSizesCntShdBeLess As String
    Public gMsgInvalidValue As String
    Public gMsgInvCntDelBecReturnISThere As String
    Public gMsgInvCntBeDupAgainstSupp As String
    Public gMsgLastClDtCantGrtThanInvDt As String
    Public gMsgLastClDtCantGrtThanRetDt As String
    Public gMsgLenSMSCntBeGreater As String
    Public gMsgLoginInvalid As String
    Public gMsgSTRFromNoMustBeGrtThnToNo As String
    Public gMsgMemberBlockUnblockComp As String
    Public gMsgMemberCardGenerated As String
    Public gMsgSendingMsgFailed As String
    Public gMsgLenOfMessage As String
    Public gMsgSentTo As String
    Public gMsgSTRDispAtleaseOneQty As String
    Public gMsgSTRCntGenerateNegInv As String
    Public gMsgSTRNewProdNotAllowed As String
    Public gMsgSeelctBatchForLables As String
    Public gMsgSelectMemberForPrinting As String
    Public gMsgSelectProCombination As String
    Public gMsgNoDataFound As String
    Public gMsgNoDataFoundInGrid As String
    Public gMsgNoDataToSave As String
    Public gMsgNoParametersToSearch As String
    Public gMsgNoLineItemsDefined As String
    Public gMsgNoLineItemsSelected As String
    Public gMsgNoMemberProvidedForSearch As String
    Public gMsgNoMoreSTRQty As String
    Public gMsgNoProductSelected As String
    Public gMsgNoRecordSelected As String
    Public gMsgNoDataToSaveGenerateCards As String
    Public gMsgNoSizeSelected As String
    Public gMsgNoSTRSelectedForPrinting As String
    Public gMsgTransFailed As String
    Public gMsgNumericValueReq As String
    Public gMsgNoShopSelected As String
    Public gMsgOverlapingRange As String
    Public gMsgPasswordChanged As String
    Public gMsgAmountCantZero As String
    Public gMsgPDategrThanClosingDate As String
    Public gMsgPhAuditReverted As String
    Public gMsgPhAuditRevertedSuccessfully As String
    Public gMsgValidClosingDate As String
    Public gMsgConfirmNewPsw As String
    Public gMsgValidBarredDate As String
    Public gMsgAmountValidation As String
    Public gMsgItemwithOrderQty As String
    Public gMsgBackupFileLocation As String
    Public gMsgBackupFileName As String
    Public gMsgBackupName As String
    Public gMsgCashInHand As String
    Public gMsgCustomerCodeValidation As String
    Public gMsgGLAccountValidation As String
    Public gMsgMessageValidatin As String
    Public gMsgNewPasswordValidation As String
    Public gMsgOldPasswordValidation As String
    Public gMsgAlterationDetailValidation As String
    Public gMsgPositiveAmountValidation As String
    Public gMsgRegKeyValidatin As String
    Public gMsgTemplateNameValidation As String
    Public gMsgMessageDescValidation As String
    Public gMsgSubjectValidatin As String
    Public gMsgValueRangeValidation As String
    Public gMsgGenerateReportValidation As String
    Public gMsgPhAuditReindex As String
    Public gMsgSelectDBValidation As String
    Public gMsgSupplierValidation As String
    Public gMsgAccountValidation As String
    Public gMsgEmailAddValidation As String
    Public gMsgMobNoValidation As String
    Public gMsgLineItemValidation As String
    Public gMsgMemeberShipTypeValidation As String
    Public gMsgFileSendValidation As String
    Public gMsgSTRQuantityValidation As String
    Public gMsgDueBalanceValidation As String
    Public gMsgPOPhysicaAutidDateValidation As String
    Public gMsgPOCloseStatusValidation As String
    Public gMsgProdExistsValidation As String
    Public gMsgProdExistsInGridValidation As String
    Public gMsgFingerPrintConfirm As String
    Public gMsgProductSelectionValidation As String
    Public gMsgPublisherAndPublicationValidation As String
    Public gMsgPublisherNameValidation As String
    Public gMsgPurDateLastPhyValidation As String
    Public gMsgQtyNumericValidation As String
    Public gMsgQtyZeroValidation As String
    Public gMsgReceiptAgainstPOValidation As String
    Public gMsgRecvDispDateValidation As String
    Public gMsgLastClosingDateValidation As String
    Public gMsgRecordNoFoundValidatin As String
    Public gMsgRecordSaveConfirm As String
    Public gMsgRegFailedConfirm As String
    Public gMsgReplicationCompConfirm As String
    Public gMsgQtyNoAvailValidation As String
    Public gMsgRetDateLastPhyValidation As String
    Public gMsgSDRExisitsValidatin As String
    Public gMsgSDRDateValidation As String
    Public gMsgSearchDateValidation As String
    Public gMsgCreditCardValidation As String
    Public gMsgExpenseValidation As String
    Public gMsgSalesTypeValidation As String
    Public gMsgShopSelectionValidation As String
    Public gMsgStatusSelectionValidation As String
    Public gMsgVendorValidation As String
    Public gMsgCustomerGroupValidation As String
    Public gMsgCustomerTypeValidation As String
    Public gMsgDestShopValidation As String
    Public gMsgProductCombValidation As String
    Public gMsgProductCodeValidation As String
    Public gMsgProductSizeValidation As String
    Public gMsgSourceShopValidation As String
    Public gMsgSizeNotExisitsValidation As String
    Public gMsgServerNotFoundValidation As String
    Public gMsgShopInvUpdatedConfirm As String
    Public gMsgShopClosingDateValidation As String
    Public gMsgShopMsgSaveConfirm As String
    Public gMsgShopMsgUpdateConfirm As String
    Public gMsgSizeorPriceValidation As String
    Public gMsgCodeInGivenRangeValidation As String
    Public gMsgProductCantDeactiveValidation As String
    Public gMsgProductZeroPrice As String
    Public gMsgProductNotFoundSTR As String
    Public gMsgSourceRangeValidation As String
    Public gMsgSplictInvAmtGrZeroValidation As String
    Public gMsgSplitInvCantUpdateValidation As String
    Public gMsgSQLRepFailureConfirm As String
    Public gMsgStartMergReplInfo As String
    Public gMsgSTRRecNotExisitValidation As String
    Public gMsgSTRSizeValidation As String
    Public gMsgSTRCloseConfirm As String
    Public gMsgSTRDispConfirm As String
    Public gMsgSTRRecvConfirm As String
    Public gMsgSTRSettedConfirm As String
    Public gMsgSTRNotDispConfirm As String
    Public gMsgSupClosingDateGrInvDateValidation As String
    Public gMsgSupClosingDateGrRetDateValidation As String
    Public gMsgSwapingResValidation As String
    Public gMsgSystemInfoConfirm As String
    Public gMsgTemplateNameExistsValidation As String
    Public gMsgRegLSProductConfirm As String
    Public gMsgNegativeAmtValidation As String
    Public gMsgNoChangeInGridValidation As String
    Public gMsgNoValidRecInGridValidation As String
    Public gMsgOneRecordValidation As String
    Public gMsgWarehouseShopValidation As String
    Public gMsgProductEnteredValidation As String
    Public gMsgProdBlockForShopValidation As String
    Public gMsgRefNotExisitsValidation As String
    Public gMsgSTRNoAdjDataValidation As String
    Public gMsgReplUserValidation As String
    Public gMsgHOUserValiadtion As String
    Public gMsgTransBeforePhyValidation As String
    Public gMsgTransFailedConfirm As String
    Public gMsgTryDateValidation As String
    Public gMsgUserGroupValidation As String
    Public gMsgValueRequired As String
    Public gMsgNewandConfirmPswValidation As String
    Public gMsgNegativeSourceShopValidation As String
    Public gMsgUpdatePOValidation As String
    Public gstrSelectedAttribute As String
    ''--------------

    ''Visual Effect Variables
    Public gobjRequiredFieldtBKColor As Color = System.Drawing.Color.FromArgb(255, 255, 192)
    Public gobjDisabledFieldtBKColor As Color = System.Drawing.Color.FromArgb(255, 201, 222, 250)  ''System.Windows.Forms.Button.DefaultBackColor ''System.Drawing.Color.FromArgb(255, 255, 192)
    Public gobjDefaultForColorForInputField As Color = System.Windows.Forms.Button.DefaultForeColor ''System.Drawing.Color.FromArgb(255, 255, 192)
    Public gobjDefaultFontSettingForLables As Font
    Public gobjDefaultFontSettingForInput As Font
    Public gobjDefaultFontSettingForTabs As Font
    Public gobjDefaultFontSettingForMenu As Font

    ''Defing Variables for rounding
    'Public gintRoundValueOn As Integer = 0
    'Public gintRoundLength As Integer = 0
    'Public gintAccountRoundLength As Integer = 0

    ''Global Variable for checking trial version

    Public gblnTrialVersion As Boolean





#End Region

    Enum EnumRegisterStatus
        Trial
        Registered
        Expired
    End Enum
End Module

