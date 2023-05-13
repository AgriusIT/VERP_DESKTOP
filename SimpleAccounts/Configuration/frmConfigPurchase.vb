Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Public Class frmConfigPurchase

    Public isFormOpen As Boolean = False
    Dim KeyType As String = String.Empty
    Dim KeyValue As String = String.Empty

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try

            Me.chkExpiryDate.Checked = Convert.ToBoolean(getConfigValueByType("ItemExpiryDateOnPurchase").ToString)

            If Me.chkExpiryDate.Checked = False Then
                Me.chkExpiryDateNot.Checked = True
            End If

            Me.chkCurrency.Checked = Convert.ToBoolean(getConfigValueByType("CurrencyonOpenLC").ToString)

            If Me.chkCurrency.Checked = False Then
                Me.chkCurrencyNot.Checked = True
            End If

            Me.chkLoadMultiplePO.Checked = Convert.ToBoolean(getConfigValueByType("LoadMultiPO"))

            If Me.chkLoadMultiplePO.Checked = False Then
                Me.chkLoadMultiplePONot.Checked = True
            End If

            Me.txtVendorQuotationDocPrefix.Text = IIf(getConfigValueByType("VendorQuotation").ToString = "Error", "", getConfigValueByType("VendorQuotation").ToString)

            If Not getConfigValueByType("LoadMultiGRN").ToString = "Error" Then
                Me.chkLoadMultiGRN.Checked = Convert.ToBoolean(getConfigValueByType("LoadMultiGRN").ToString)
            End If

            If Me.chkLoadMultiGRN.Checked = False Then
                Me.chkLoadMultiGRNNot.Checked = True
            End If

            If Not getConfigValueByType("GRNStockImpact").ToString = "Error" Then
                Me.cbGRNStockImpact.Checked = Convert.ToBoolean(getConfigValueByType("GRNStockImpact").ToString)
                If Me.cbGRNStockImpact.Checked = True Then
                    Me.cbGRNStockImpact.Enabled = False
                Else
                    Me.cbGRNStockImpact.Enabled = True
                End If
            End If

            If Me.cbGRNStockImpact.Checked = False Then
                Me.cbGRNStockImpactNot.Checked = True
            End If

            If Not getConfigValueByType("POFromCS").ToString = "Error" Then
                Me.chkPOFromCS.Checked = Convert.ToBoolean(getConfigValueByType("POFromCS").ToString)
            End If

            If Me.chkPOFromCS.Checked = False Then
                Me.chkPOFromCSNot.Checked = True
            End If

            If Not getConfigValueByType("ShowCompanyWisePrefix").ToString = "Error" Then
                Me.checkShowCompanyWisePrefix.Checked = Convert.ToBoolean(getConfigValueByType("ShowCompanyWisePrefix").ToString)
            End If

            If Me.checkShowCompanyWisePrefix.Checked = False Then
                Me.checkShowCompanyWisePrefixNot.Checked = True
            End If
            ''TASK TFS4667
            If Not getConfigValueByType("TaxExcludeFromRateOnQuotationFromCS").ToString = "Error" Then
                Me.rbTaxExcludeFromRateOnQuotationFromCS.Checked = Convert.ToBoolean(getConfigValueByType("TaxExcludeFromRateOnQuotationFromCS").ToString)
            End If
            If Me.rbTaxExcludeFromRateOnQuotationFromCS.Checked = False Then
                Me.rbTaxExcludeFromRateOnQuotationFromCSNot.Checked = True
            End If
            ''End TASK TFS4667

            If Not getConfigValueByType("PurchaseDiablePackQuantity").ToString = "Error" Then
                Me.rdoDisablePackQty.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseDiablePackQuantity").ToString)
            End If
            If Me.rdoDisablePackQty.Checked = False Then
                Me.rdoDisablePackQtyNot.Checked = True
            End If

            Me.chkCommentsVendorFormat.Checked = Convert.ToBoolean(getConfigValueByType("CommentVendorFormat").ToString)
            Me.chkPurchaseCommentArticleDescriptionFormat.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseCommentArticleFormat").ToString)
            Me.chkPurchaseCommentArticleSizeFormat.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseCommentArticleSizeFormat").ToString)
            Me.chkPurchaseCommentArticleColorFormat.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseCommentArticleColorFormat").ToString)
            Me.chkPurchaseCommentQtyFormat.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseCommentQtyFormat").ToString)
            Me.chkPurchaseCommentPriceFormat.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseCommentPriceFormat").ToString)
            Me.chkPurchaseCommentRemarksFormat.Checked = Convert.ToBoolean(getConfigValueByType("PurchaseCommentRemarksFormat").ToString)
            Me.chkCommentInvoiceNo.Checked = Convert.ToBoolean(getConfigValueByType("CommentsInvNoOnPurchase").ToString)
            Me.chkCommentsDcNo.Checked = Convert.ToBoolean(getConfigValueByType("CommentsDCNoOnPurchase").ToString)

            Me.chkWeighbridgeOnGRN.Checked = Convert.ToBoolean(getConfigValueByType("WeighbridgeGRN").ToString)
            Me.chkWeighbridgePurchase.Checked = Convert.ToBoolean(getConfigValueByType("WeighbridgePurchase").ToString)

            ''Start TFS3762
            If Not getConfigValueByType("AddItemForMall").ToString = "Error" Then
                Me.chkAddItemForMall.Checked = Convert.ToBoolean(getConfigValueByType("AddItemForMall").ToString)
            End If

            If Me.chkAddItemForMall.Checked = False Then
                Me.chkAddItemForMallNot.Checked = True
            End If
            ''End TFS3762


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub linkAccounts_Click(sender As Object, e As EventArgs) Handles linkAccounts.Click
        Try
            If frmConfigPurchaseAccount.isFormOpen = True Then
                frmConfigPurchaseAccount.Dispose()
            End If

            frmConfigPurchaseAccount.ShowDialog()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkSecurity_Click(sender As Object, e As EventArgs) Handles linkSecurity.Click
        Try
            If frmConfigPurchaseSecurity.isFormOpen = True Then
                frmConfigPurchaseSecurity.Dispose()
            End If

            frmConfigPurchaseSecurity.ShowDialog()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmConfigPurchase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        getConfigValueList()
        GetAllRecords()
    End Sub

    Private Sub chkExpiryDate_CheckedChanged(sender As Object, e As EventArgs) Handles chkExpiryDate.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkCurrency_CheckedChanged(sender As Object, e As EventArgs) Handles chkCurrency.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkLoadMultiplePO_CheckedChanged(sender As Object, e As EventArgs) Handles chkLoadMultiplePO.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub txtVendorQuotationDocPrefix_Leave(sender As Object, e As EventArgs) Handles txtVendorQuotationDocPrefix.Leave
        Try
            KeyType = "VendorQuotation"
            KeyValue = Me.txtVendorQuotationDocPrefix.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkLoadMultiGRN_CheckedChanged(sender As Object, e As EventArgs) Handles chkLoadMultiGRN.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub cbGRNStockImpact_CheckedChanged(sender As Object, e As EventArgs) Handles cbGRNStockImpact.CheckedChanged

        If CheckOpenGRNs() Then
            msg_Information("Opened GRNs are found. Please close them first. ")
            cbGRNStockImpactNot.Checked = True
            DisplayOpenedGRNs()
            Exit Sub
        End If

        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkPOFromCS_CheckedChanged(sender As Object, e As EventArgs) Handles chkPOFromCS.CheckedChanged, chkAddItemForMall.CheckedChanged ''TFS3762
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub checkShowCompanyWisePrefix_CheckedChanged(sender As Object, e As EventArgs) Handles checkShowCompanyWisePrefix.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkCommentsVendorFormat_CheckedChanged(sender As Object, e As EventArgs) Handles chkCommentsVendorFormat.CheckedChanged, chkPurchaseCommentArticleDescriptionFormat.CheckedChanged, chkPurchaseCommentArticleSizeFormat.CheckedChanged, chkPurchaseCommentArticleColorFormat.CheckedChanged, chkPurchaseCommentQtyFormat.CheckedChanged, chkPurchaseCommentPriceFormat.CheckedChanged, chkPurchaseCommentRemarksFormat.CheckedChanged, chkCommentsDcNo.CheckedChanged, chkCommentInvoiceNo.CheckedChanged
        Try
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim strValues As String = String.Empty

            If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, chk.Checked)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function CheckOpenGRNs() As Boolean
        Dim checkOpen As String = ""
        Try
            ''Below line is commented on 08-02-2017
            'checkOpen = "SELECT IsNull(ReceivingNoteId, 0) As ReceivingNoteId FROM ReceivingNoteMasterTable WHERE ReceivingNoteId NOT IN (SELECT DISTINCT CASE WHEN IsNull(ReceivingDetail.ReceivingNoteId, 0) > 0 THEN IsNull(ReceivingDetail.ReceivingNoteId, 0) ELSE IsNull(Receiving.ReceivingNoteId, 0) END As ReceivingNoteId From ReceivingDetailTable AS ReceivingDetail INNER JOIN ReceivingMasterTable AS Receiving ON ReceivingDetail.ReceivingId = Receiving.ReceivingId) AND ReceivingNoteId NOT IN (SELECT DISTINCT CASE WHEN IsNull(LCDetailTable.ReceivingNoteId, 0) > 0 THEN IsNull(LCDetailTable.ReceivingNoteId, 0) ELSE IsNull(LCMasterTable.ReceivingNoteId, 0) END As ReceivingNoteId From LCMasterTable INNER JOIN LCDetailTable ON LCMasterTable.LCId = LCDetailTable.LCId)"
            checkOpen = "SELECT IsNull(ReceivingNoteId, 0) As ReceivingNoteId FROM ReceivingNoteMasterTable WHERE ReceivingNoteId NOT IN (SELECT DISTINCT CASE WHEN IsNull(ReceivingDetail.ReceivingNoteId, 0) > 0 THEN IsNull(ReceivingDetail.ReceivingNoteId, 0) ELSE IsNull(Receiving.ReceivingNoteId, 0) END As ReceivingNoteId From ReceivingDetailTable AS ReceivingDetail INNER JOIN ReceivingMasterTable AS Receiving ON ReceivingDetail.ReceivingId = Receiving.ReceivingId) AND ReceivingNoteId NOT IN (SELECT DISTINCT CASE WHEN IsNull(LCDetailTable.ReceivingNoteId, 0) > 0 THEN IsNull(LCDetailTable.ReceivingNoteId, 0) ELSE IsNull(LCMasterTable.ReceivingNoteId, 0) END As ReceivingNoteId From LCMasterTable INNER JOIN LCDetailTable ON LCMasterTable.LCId = LCDetailTable.LCId) AND ReceivingNo NOT IN(SELECT DocNo FROM StockMasterTable)"
            Dim dtCheckOpen As DataTable = GetDataTable(checkOpen)
            If dtCheckOpen.Rows.Count > 0 AndAlso dtCheckOpen.Rows(0).Item(0) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function DisplayOpenedGRNs() As Boolean
        Dim checkOpen As String = ""
        Try
            checkOpen = "SELECT ReceivingNo FROM ReceivingNoteMasterTable WHERE ReceivingNoteId NOT IN (SELECT DISTINCT CASE WHEN IsNull(ReceivingDetail.ReceivingNoteId, 0) > 0 THEN IsNull(ReceivingDetail.ReceivingNoteId, 0) ELSE IsNull(Receiving.ReceivingNoteId, 0) END As ReceivingNoteId From ReceivingDetailTable AS ReceivingDetail INNER JOIN ReceivingMasterTable AS Receiving ON ReceivingDetail.ReceivingId = Receiving.ReceivingId) AND ReceivingNoteId NOT IN (SELECT DISTINCT CASE WHEN IsNull(LCDetailTable.ReceivingNoteId, 0) > 0 THEN IsNull(LCDetailTable.ReceivingNoteId, 0) ELSE IsNull(LCMasterTable.ReceivingNoteId, 0) END As ReceivingNoteId From LCMasterTable INNER JOIN LCDetailTable ON LCMasterTable.LCId = LCDetailTable.LCId) AND ReceivingNo NOT IN(SELECT DocNo FROM StockMasterTable) "
            Dim dtCheckOpen As DataTable = GetDataTable(checkOpen)
            Dim _List As List(Of String) = dtCheckOpen.AsEnumerable().Select(Function(dr) dr.Field(Of String)(0)).ToList
            'Dim _List As List(Of String) = dtCheckOpen.Rows.Cast(Of DataRow).Select(Function(dr) dr(0).ToString).ToList
            Dim _Message = String.Join(Environment.NewLine, _List)
            MsgBox(_Message, MsgBoxStyle.Information, "Following GRNs are opened. Please close them.")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub chkWeighbridgeOnGRN_CheckedChanged(sender As Object, e As EventArgs) Handles chkWeighbridgeOnGRN.CheckedChanged, chkWeighbridgePurchase.CheckedChanged
        Try
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim strValues As String = String.Empty

            If chk.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(chk.Tag, chk.Checked)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rbTaxExcludeFromRateOnQuotationFromCS_CheckedChanged(sender As Object, e As EventArgs) Handles rbTaxExcludeFromRateOnQuotationFromCS.CheckedChanged
        ''TASK TFS4667
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub rdoDisablePackQty_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDisablePackQty.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

End Class