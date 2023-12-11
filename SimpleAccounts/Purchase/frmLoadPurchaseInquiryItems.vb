''TASK TFS3534 Muhammad Ameen created this popup to load remaining purchase inquiry items in EDIT Mode. 13-06-2018

Imports SBDal
Public Class frmLoadPurchaseInquiryItems


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal PurchaseInquiryId As Integer, ByVal VendorId As Integer, ByVal PurchaseInquiryDetailIds As String)
        ' This call is required by the designer.
        InitializeComponent()
        FillCombos("Vendor")
        Me.cmbVendor.Value = VendorId
        FillCombos("PurchaseInquiry")
        Me.cmbPurchaseInquiry.Value = PurchaseInquiryId
        Me.cmbVendor.Enabled = False
        Me.cmbPurchaseInquiry.Enabled = False
        Me.grdItems.DataSource = VendorQuotationDetailDAL.DisplayRemainingPurchaseInquiryDetail(PurchaseInquiryId, PurchaseInquiryDetailIds)
        Me.grdItems.ExpandRecords()
        ' Add any initialization after the InitializeComponent() call.
    End Sub



    Private Sub FillCombos(Optional ByVal Condition As String = "")
        Dim Query As String = String.Empty
        Try
            If Condition = "" Or Condition = "Vendor" Then
                Query = String.Empty
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    Query = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('Vendor', 'Customer') and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbVendor, Query)
                    Me.cmbVendor.Rows(0).Activate()
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Else
                    Query = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail WHERE vwCOADetail.account_type = 'Vendor' and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbVendor, Query)
                    Me.cmbVendor.Rows(0).Activate()
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True

                End If
            ElseIf Condition = "PurchaseInquiry" Then
                Query = " Select PurchaseInquiryMaster.PurchaseInquiryId, PurchaseInquiryMaster.PurchaseInquiryNo, PurchaseInquiryMaster.PurchaseInquiryDate From PurchaseInquiryMaster INNER JOIN PurchaseInquiryVendors ON PurchaseInquiryMaster.PurchaseInquiryId = PurchaseInquiryVendors.PurchaseInquiryId Where PurchaseInquiryVendors.VendorId =" & Val(Me.cmbVendor.Value.ToString) & " And IsNull(PurchaseInquiryMaster.Posted,0) = 1 Order By PurchaseInquiryMaster.PurchaseInquiryDate DESC"
                FillUltraDropDown(Me.cmbPurchaseInquiry, Query)
                Me.cmbPurchaseInquiry.Rows(0).Activate()
                Me.cmbPurchaseInquiry.DisplayLayout.Bands(0).Columns("PurchaseInquiryId").Hidden = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            Dim DT As DataTable = CType(frmVendorQuotation.grdItems.DataSource, DataTable)
            Dim DT1 As DataTable = CType(Me.grdItems.DataSource, DataTable).Clone
            Dim _Row As DataRow
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetCheckedRows
                _Row = CType(Row.DataRow, DataRowView).Row

                DT.ImportRow(_Row)
                'Row.BeginEdit()
                'DT1.Rows.Add(_Row)
                'DT1.ImportRow()
                'Row.EndEdit()
            Next



            'DT.Merge(DT1)
            DT.AcceptChanges()
            Me.Close()
            'Dim DT1 As DataTable = Me.grdItems.GetCheckedRows.
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class