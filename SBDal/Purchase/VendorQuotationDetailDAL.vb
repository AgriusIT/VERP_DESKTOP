Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Net
Public Class VendorQuotationDetailDAL
   
    Public Function Add(ByVal obj As VendorQuotationMaster, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            'If Not Exists(Select PurchaseInquiryDetailId FROM VendorQuotationDetail Where PurchaseInquiryDetailId ="& objMod.PurchaseInquiryDetailId &" )
            For Each objMod As VendorQuotationDetail In obj.DetailList
                Dim strSQL As String = String.Empty
                'If objMod.SalesInquiryDetailId > 0 Then
                '    UpdateSalesInquiryStatusAgainstAddition(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans)
                'End If
                'strSQL = "INSERT INTO VendorQuotationDetail(VendorQuotationId, SerialNo, RequirementDescription, ArticleId, UnitId, ItemTypeId, CategoryId, SubCategoryId, OriginId, Qty, QuotedTerms, ValidityOfQuotation, DeliveryPeriod, Warranty, ApproxGrossWeight, HSCode, DeliveryPort, GenuineOrReplacement, LiteratureOrDatasheet, NewOrRefurbish, Price, DiscountPer, SalesTaxPer, AddTaxPer, IncTaxPer, CDPer, NetPrice, OtherCharges, PurchaseInquiryDetailId, ReferenceNo, Comments, HeadArticleId) " _
                '& " VALUES(" & obj.VendorQuotationId & ", '" & objMod.SerialNo.Replace("'", "''") & "', '" & objMod.RequirementDescription.Replace("'", "''") & "', " & IIf(objMod.ArticleId > 0, objMod.ArticleId, "NULL"),  & ", " & objMod.UnitId & ", " & objMod.ItemTypeId & ", " & objMod.CategoryId & ", " & objMod.SubCategoryId & ", " & objMod.OriginId & ", " & objMod.Qty & " , '" & objMod.QuotedTerms.Replace("'", "''") & "', '" & objMod.ValidityOfQuotation.Replace("'", "''") & "', '" & objMod.DeliveryPeriod.Replace("'", "''") & "', '" & objMod.Warranty.Replace("'", "''") & "', '" & objMod.ApproxGrossWeight.Replace("'", "''") & "', '" & objMod.HSCode.Replace("'", "''") & "', '" & objMod.DeliveryPort.Replace("'", "''") & "', '" & objMod.GenuineOrReplacement.Replace("'", "''") & "', '" & objMod.LiteratureOrDatasheet.Replace("'", "''") & "', '" & objMod.NewOrRefurbish.Replace("'", "''") & "', " & objMod.Price & ", " & objMod.DiscountPer & ", " & objMod.SalesTaxPer & ", " & objMod.AddTaxPer & ", " & objMod.IncTaxPer & ", " & objMod.CDPer & " , " & objMod.NetPrice & ", " & objMod.OtherCharges & ", " & objMod.PurchaseInquiryDetailId & ", '" & objMod.ReferenceNo.Replace("'", "''") & "', '" & objMod.Comments.Replace("'", "''") & "',  " & objMod.HeadArticleId & ") Select @@Identity"
                'Dim VendorQuotationDetailId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                'objMod.VendorQuotationDetailId = VendorQuotationDetailId
                'AddCharges(objMod, trans)
                'Ali Faisal : TFS1310 : Insert and Update Ex works column value in detail table
                strSQL = "If Not Exists(Select VendorQuotationDetailId FROM VendorQuotationDetail Where VendorQuotationDetailId =" & objMod.VendorQuotationDetailId & " ) INSERT INTO VendorQuotationDetail(VendorQuotationId, SerialNo, RequirementDescription, ArticleId, UnitId, ItemTypeId, CategoryId, SubCategoryId, OriginId, Qty, QuotedTerms, ValidityOfQuotation, DeliveryPeriod, Warranty, ApproxGrossWeight, HSCode, DeliveryPort, GenuineOrReplacement, LiteratureOrDatasheet, NewOrRefurbish, Price, DiscountPer, SalesTaxPer, AddTaxPer, IncTaxPer, CDPer, NetPrice, OtherCharges, PurchaseInquiryDetailId, ReferenceNo, Comments, HeadArticleId, ExWorks) " _
                & " VALUES(" & obj.VendorQuotationId & ", '" & objMod.SerialNo.Replace("'", "''") & "', '" & objMod.RequirementDescription.Replace("'", "''") & "', " & IIf(objMod.ArticleId > 0, objMod.ArticleId, "NULL") & ", " & objMod.UnitId & ", " & objMod.ItemTypeId & ", " & objMod.CategoryId & ", " & objMod.SubCategoryId & ", " & objMod.OriginId & ", " & objMod.Qty & " , '" & objMod.QuotedTerms.Replace("'", "''") & "', '" & objMod.ValidityOfQuotation.Replace("'", "''") & "', '" & objMod.DeliveryPeriod.Replace("'", "''") & "', '" & objMod.Warranty.Replace("'", "''") & "', '" & objMod.ApproxGrossWeight.Replace("'", "''") & "', '" & objMod.HSCode.Replace("'", "''") & "', '" & objMod.DeliveryPort.Replace("'", "''") & "', '" & objMod.GenuineOrReplacement.Replace("'", "''") & "', '" & objMod.LiteratureOrDatasheet.Replace("'", "''") & "', '" & objMod.NewOrRefurbish.Replace("'", "''") & "', " & objMod.Price & ", " & objMod.DiscountPer & ", " & objMod.SalesTaxPer & ", " & objMod.AddTaxPer & ", " & objMod.IncTaxPer & ", " & objMod.CDPer & " , " & objMod.NetPrice & ", " & objMod.OtherCharges & ", " & objMod.PurchaseInquiryDetailId & ", '" & objMod.ReferenceNo.Replace("'", "''") & "', '" & objMod.Comments.Replace("'", "''") & "',  " & objMod.HeadArticleId & ", '" & objMod.ExWorks.Replace("'", "''") & "') " _
                & " Else Update VendorQuotationDetail SET VendorQuotationId= " & objMod.VendorQuotationId & ", SerialNo = '" & objMod.SerialNo.Replace("'", "''") & "', RequirementDescription='" & objMod.RequirementDescription.Replace("'", "''") & "', ArticleId= " & IIf(objMod.ArticleId > 0, objMod.ArticleId, "NULL") & ", UnitId=" & objMod.UnitId & ", ItemTypeId= " & objMod.ItemTypeId & " , CategoryId= " & objMod.CategoryId & ", SubCategoryId= " & objMod.SubCategoryId & ", OriginId= " & objMod.OriginId & ", Qty=" & objMod.Qty & ", QuotedTerms = '" & objMod.QuotedTerms.Replace("'", "''") & "',  ValidityOfQuotation = '" & objMod.ValidityOfQuotation.Replace("'", "''") & "',  DeliveryPeriod = '" & objMod.DeliveryPeriod.Replace("'", "''") & "',  Warranty = '" & objMod.Warranty.Replace("'", "''") & "',  ApproxGrossWeight = '" & objMod.ApproxGrossWeight.Replace("'", "''") & "', HSCode = '" & objMod.HSCode.Replace("'", "''") & "', DeliveryPort = '" & objMod.DeliveryPort.Replace("'", "''") & "', GenuineOrReplacement = '" & objMod.GenuineOrReplacement.Replace("'", "''") & "', LiteratureOrDatasheet = '" & objMod.LiteratureOrDatasheet.Replace("'", "''") & "', NewOrRefurbish = '" & objMod.NewOrRefurbish.Replace("'", "''") & "', Price=" & objMod.Price & ", DiscountPer=" & objMod.DiscountPer & ", SalesTaxPer= " & objMod.SalesTaxPer & ",  AddTaxPer= " & objMod.AddTaxPer & ",  IncTaxPer= " & objMod.IncTaxPer & ",  CDPer= " & objMod.CDPer & ",  NetPrice= " & objMod.NetPrice & ",  OtherCharges= " & objMod.OtherCharges & ",  PurchaseInquiryDetailId= " & objMod.PurchaseInquiryDetailId & ", ReferenceNo = '" & objMod.ReferenceNo.Replace("'", "''") & "', Comments = '" & objMod.Comments.Replace("'", "''") & "', HeadArticleId = " & objMod.HeadArticleId & ", ExWorks = '" & objMod.ExWorks.Replace("'", "''") & "' WHERE VendorQuotationDetailId=" & objMod.VendorQuotationDetailId & " Select @@Identity "
                Dim VendorQuotationDetailId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                If VendorQuotationDetailId > 0 Then
                    objMod.VendorQuotationDetailId = VendorQuotationDetailId
                End If
                AddCharges(objMod, trans)
            Next
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddSingle(ByVal obj As VendorQuotationMaster, ByVal trans As SqlTransaction) As Integer
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Dim VendorQuotationDetailId As Integer = 0
        Try
            For Each objMod As VendorQuotationDetail In obj.DetailList
                Dim strSQL As String = String.Empty
                'Ali Faisal : TFS1310 : Insert and Update Ex works column value in detail table
                strSQL = "If Not Exists(Select VendorQuotationDetailId FROM VendorQuotationDetail Where VendorQuotationDetailId =" & objMod.VendorQuotationDetailId & " ) INSERT INTO VendorQuotationDetail(VendorQuotationId, SerialNo, RequirementDescription, ArticleId, UnitId, ItemTypeId, CategoryId, SubCategoryId, OriginId, Qty, QuotedTerms, ValidityOfQuotation, DeliveryPeriod, Warranty, ApproxGrossWeight, HSCode, DeliveryPort, GenuineOrReplacement, LiteratureOrDatasheet, NewOrRefurbish, Price, DiscountPer, SalesTaxPer, AddTaxPer, IncTaxPer, CDPer, NetPrice, OtherCharges, PurchaseInquiryDetailId, ReferenceNo, Comments, HeadArticleId, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencySymbol, Alternate, ExWorks, StatusId) " _
                & " VALUES(" & obj.VendorQuotationId & ", '" & objMod.SerialNo.Replace("'", "''") & "', '" & objMod.RequirementDescription.Replace("'", "''") & "', " & IIf(objMod.ArticleId > 0, objMod.ArticleId, "NULL") & ", " & objMod.UnitId & ", " & objMod.ItemTypeId & ", " & objMod.CategoryId & ", " & objMod.SubCategoryId & ", " & objMod.OriginId & ", " & objMod.Qty & " , '" & objMod.QuotedTerms.Replace("'", "''") & "', '" & objMod.ValidityOfQuotation.Replace("'", "''") & "', '" & objMod.DeliveryPeriod.Replace("'", "''") & "', '" & objMod.Warranty.Replace("'", "''") & "', '" & objMod.ApproxGrossWeight.Replace("'", "''") & "', '" & objMod.HSCode.Replace("'", "''") & "', '" & objMod.DeliveryPort.Replace("'", "''") & "', '" & objMod.GenuineOrReplacement.Replace("'", "''") & "', '" & objMod.LiteratureOrDatasheet.Replace("'", "''") & "', '" & objMod.NewOrRefurbish.Replace("'", "''") & "', " & objMod.Price & ", " & objMod.DiscountPer & ", " & objMod.SalesTaxPer & ", " & objMod.AddTaxPer & ", " & objMod.IncTaxPer & ", " & objMod.CDPer & " , " & objMod.NetPrice & ", " & objMod.OtherCharges & ", " & IIf(objMod.PurchaseInquiryDetailId > 0, objMod.PurchaseInquiryDetailId, "NULL") & ", '" & objMod.ReferenceNo.Replace("'", "''") & "', '" & objMod.Comments.Replace("'", "''") & "',  " & objMod.HeadArticleId & ",  " & objMod.BaseCurrencyId & ",  " & objMod.BaseCurrencyRate & ",  " & objMod.CurrencyId & ",  " & objMod.CurrencyRate & ", '" & objMod.CurrencySymbol.Replace("'", "''") & "', " & IIf(objMod.IsAlternate = False, 0, 1) & ", '" & objMod.ExWorks.Replace("'", "''") & "',1) " _
                & " Else Update VendorQuotationDetail SET VendorQuotationId= " & objMod.VendorQuotationId & ", SerialNo = '" & objMod.SerialNo.Replace("'", "''") & "', RequirementDescription='" & objMod.RequirementDescription.Replace("'", "''") & "', ArticleId= " & IIf(objMod.ArticleId > 0, objMod.ArticleId, "NULL") & ", UnitId=" & objMod.UnitId & ", ItemTypeId= " & objMod.ItemTypeId & " , CategoryId= " & objMod.CategoryId & ", SubCategoryId= " & objMod.SubCategoryId & ", OriginId= " & objMod.OriginId & ", Qty=" & objMod.Qty & ", QuotedTerms = '" & objMod.QuotedTerms.Replace("'", "''") & "',  ValidityOfQuotation = '" & objMod.ValidityOfQuotation.Replace("'", "''") & "',  DeliveryPeriod = '" & objMod.DeliveryPeriod.Replace("'", "''") & "',  Warranty = '" & objMod.Warranty.Replace("'", "''") & "',  ApproxGrossWeight = '" & objMod.ApproxGrossWeight.Replace("'", "''") & "', HSCode = '" & objMod.HSCode.Replace("'", "''") & "', DeliveryPort = '" & objMod.DeliveryPort.Replace("'", "''") & "', GenuineOrReplacement = '" & objMod.GenuineOrReplacement.Replace("'", "''") & "', LiteratureOrDatasheet = '" & objMod.LiteratureOrDatasheet.Replace("'", "''") & "', NewOrRefurbish = '" & objMod.NewOrRefurbish.Replace("'", "''") & "', Price=" & objMod.Price & ", DiscountPer=" & objMod.DiscountPer & ", SalesTaxPer= " & objMod.SalesTaxPer & ",  AddTaxPer= " & objMod.AddTaxPer & ",  IncTaxPer= " & objMod.IncTaxPer & ",  CDPer= " & objMod.CDPer & ",  NetPrice= " & objMod.NetPrice & ",  OtherCharges= " & objMod.OtherCharges & ",  PurchaseInquiryDetailId= " & IIf(objMod.PurchaseInquiryDetailId > 0, objMod.PurchaseInquiryDetailId, "NULL") & ", ReferenceNo = '" & objMod.ReferenceNo.Replace("'", "''") & "', Comments = '" & objMod.Comments.Replace("'", "''") & "', HeadArticleId = " & objMod.HeadArticleId & ", BaseCurrencyId = " & objMod.BaseCurrencyId & ", BaseCurrencyRate = " & objMod.BaseCurrencyRate & ", CurrencyId = " & objMod.CurrencyId & ", CurrencyRate = " & objMod.CurrencyRate & ", CurrencySymbol = '" & objMod.CurrencySymbol.Replace("'", "''") & "', Alternate =  " & IIf(objMod.IsAlternate = False, 0, 1) & ", ExWorks = '" & objMod.ExWorks.Replace("'", "''") & "',StatusId = 1  WHERE VendorQuotationDetailId=" & objMod.VendorQuotationDetailId & " Select @@Identity"
                VendorQuotationDetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                If VendorQuotationDetailId > 0 Then
                    objMod.VendorQuotationDetailId = VendorQuotationDetailId
                Else
                    VendorQuotationDetailId = objMod.VendorQuotationDetailId
                End If
                AddCharges(objMod, trans)
            Next
            'trans.Commit()
            Return VendorQuotationDetailId
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Add1(ByVal obj As VendorQuotationMaster, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            For Each objMod As VendorQuotationDetail In obj.DetailList
                Dim strSQL As String = String.Empty
                'If objMod.SalesInquiryDetailId > 0 Then
                '    UpdateSalesInquiryStatusAgainstAddition(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans)
                'End If
                If objMod.VendorQuotationDetailId > 0 Then
                    'Ali Faisal : TFS1310 : Insert and Update Ex works column value in detail table
                    strSQL = "Update VendorQuotationDetail  SET VendorQuotationId= " & objMod.VendorQuotationId & ", SerialNo = '" & objMod.SerialNo.Replace("'", "''") & "', RequirementDescription='" & objMod.RequirementDescription.Replace("'", "''") & "', ArticleId= " & IIf(objMod.ArticleId > 0, objMod.ArticleId, "NULL") & ", UnitId=" & objMod.UnitId & ", ItemTypeId= " & objMod.ItemTypeId & " , CategoryId= " & objMod.CategoryId & ", SubCategoryId= " & objMod.SubCategoryId & ", OriginId= " & objMod.OriginId & ", Qty=" & objMod.Qty & ", QuotedTerms = '" & objMod.QuotedTerms.Replace("'", "''") & "',  ValidityOfQuotation = '" & objMod.ValidityOfQuotation.Replace("'", "''") & "',  DeliveryPeriod = '" & objMod.DeliveryPeriod.Replace("'", "''") & "',  Warranty = '" & objMod.Warranty.Replace("'", "''") & "',  ApproxGrossWeight = '" & objMod.ApproxGrossWeight.Replace("'", "''") & "', HSCode = '" & objMod.HSCode.Replace("'", "''") & "', DeliveryPort = '" & objMod.DeliveryPort.Replace("'", "''") & "', GenuineOrReplacement = '" & objMod.GenuineOrReplacement.Replace("'", "''") & "', LiteratureOrDatasheet = '" & objMod.LiteratureOrDatasheet.Replace("'", "''") & "', NewOrRefurbish = '" & objMod.NewOrRefurbish.Replace("'", "''") & "', Price=" & objMod.Price & ", DiscountPer=" & objMod.DiscountPer & ", SalesTaxPer= " & objMod.SalesTaxPer & ",  AddTaxPer= " & objMod.AddTaxPer & ",  IncTaxPer= " & objMod.IncTaxPer & ",  CDPer= " & objMod.CDPer & ",  NetPrice= " & objMod.NetPrice & ",  OtherCharges= " & objMod.OtherCharges & ",  PurchaseInquiryDetailId= " & objMod.PurchaseInquiryDetailId & ", ReferenceNo = '" & objMod.ReferenceNo.Replace("'", "''") & "', Comments = '" & objMod.Comments.Replace("'", "''") & "', HeadArticleId =  " & objMod.HeadArticleId & ", BaseCurrencyId = " & objMod.BaseCurrencyId & ", BaseCurrencyRate = " & objMod.BaseCurrencyRate & ", CurrencyId = " & objMod.CurrencyId & ", CurrencyRate = " & objMod.CurrencyRate & ", CurrencySymbol = '" & objMod.CurrencySymbol.Replace("'", "''") & "', ExWorks = '" & objMod.ExWorks.Replace("'", "''") & "' WHERE PurchaseInquiryDetailId=" & objMod.PurchaseInquiryDetailId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    AddCharges(objMod, trans)
                Else
                    'Ali Faisal : TFS1310 : Insert and Update Ex works column value in detail table
                    strSQL = "INSERT INTO VendorQuotationDetail(VendorQuotationId, SerialNo, RequirementDescription, ArticleId, UnitId, ItemTypeId, CategoryId, SubCategoryId, OriginId, Qty, QuotedTerms, ValidityOfQuotation, DeliveryPeriod, Warranty, ApproxGrossWeight, HSCode, DeliveryPort, GenuineOrReplacement, LiteratureOrDatasheet, NewOrRefurbish, Price, DiscountPer, SalesTaxPer, AddTaxPer, IncTaxPer, CDPer, NetPrice, OtherCharges, PurchaseInquiryDetailId, ReferenceNo, Comments, HeadArticleId, BaseCurrencyId, BaseCurrencyRate, CurrencyId, CurrencyRate, CurrencySymbol, ExWorks) " _
                    & " VALUES(" & obj.VendorQuotationId & ", '" & objMod.SerialNo.Replace("'", "''") & "', '" & objMod.RequirementDescription.Replace("'", "''") & "', " & IIf(objMod.ArticleId > 0, objMod.ArticleId, "NULL") & ", " & objMod.UnitId & ", " & objMod.ItemTypeId & ", " & objMod.CategoryId & ", " & objMod.SubCategoryId & ", " & objMod.OriginId & ", " & objMod.Qty & " , '" & objMod.QuotedTerms.Replace("'", "''") & "', '" & objMod.ValidityOfQuotation.Replace("'", "''") & "', '" & objMod.DeliveryPeriod.Replace("'", "''") & "', '" & objMod.Warranty.Replace("'", "''") & "', '" & objMod.ApproxGrossWeight.Replace("'", "''") & "', '" & objMod.HSCode.Replace("'", "''") & "', '" & objMod.DeliveryPort.Replace("'", "''") & "', '" & objMod.GenuineOrReplacement.Replace("'", "''") & "', '" & objMod.LiteratureOrDatasheet.Replace("'", "''") & "', '" & objMod.NewOrRefurbish.Replace("'", "''") & "', " & objMod.Price & ", " & objMod.DiscountPer & ", " & objMod.SalesTaxPer & ", " & objMod.AddTaxPer & ", " & objMod.IncTaxPer & ", " & objMod.CDPer & " , " & objMod.NetPrice & ", " & objMod.OtherCharges & ", " & objMod.PurchaseInquiryDetailId & ", '" & objMod.ReferenceNo.Replace("'", "''") & "', '" & objMod.Comments.Replace("'", "''") & "',  " & objMod.HeadArticleId & ",  " & objMod.BaseCurrencyId & ",  " & objMod.BaseCurrencyRate & ",  " & objMod.CurrencyId & ",  " & objMod.CurrencyRate & ", '" & objMod.CurrencySymbol.Replace("'", "''") & "', '" & objMod.ExWorks.Replace("'", "''") & "') Select @@Identity"
                    Dim VendorQuotationDetailId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                    objMod.VendorQuotationDetailId = VendorQuotationDetailId
                    AddCharges(objMod, trans)
                End If
            Next
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    'Public Function AddUpdate(ByVal obj As PurchaseInquiryMaster, ByVal trans As SqlTransaction) As Boolean
    '    Dim Con As New SqlConnection(SQLHelper.CON_STR)
    '    If Con.State = ConnectionState.Closed Then Con.Open()
    '    'Dim trans As SqlTransaction = Con.BeginTransaction
    '    Try
    '        For Each objMod As PurchaseInquiryDetail In obj.DetailList
    '            Dim strSQL As String = String.Empty
    '            If objMod.SalesInquiryDetailId > 0 Then
    '                UpdateSalesInquiryStatusAgainstSubtraction(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans)
    '                UpdateSalesInquiryStatusAgainstAddition(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans)
    '            End If
    '            strSQL = "INSERT INTO PurchaseInquiryDetail(PurchaseInquiryId, SerialNo, RequirementDescription, ArticleId, UnitId, ItemTypeId, CategoryId, SubCategoryId, OriginId, Qty, ReferenceNo, Comments, SalesInquiryId, SalesInquiryDetailId) " _
    '            & " VALUES(" & obj.PurchaseInquiryId & ", '" & objMod.SerialNo.Replace("'", "''") & "', '" & objMod.RequirementDescription.Replace("'", "''") & "', " & objMod.ArticleId & ", " & objMod.UnitId & ", " & objMod.ItemTypeId & ", " & objMod.CategoryId & ", " & objMod.SubCategoryId & ", " & objMod.OriginId & ", " & objMod.Qty & " , '" & objMod.ReferenceNo.Replace("'", "''") & "', '" & objMod.Comments.Replace("'", "''") & "', " & objMod.SalesInquiryId & ", " & objMod.SalesInquiryDetailId & ")"
    '            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
    '        Next
    '        'trans.Commit()
    '        Return True
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function

    Public Function Update(ByVal objModList As List(Of VendorQuotationDetail), ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction


        Try
            'QuotedTerms()
            'ValidityOfQuotation()
            'DeliveryPeriod()
            'Warranty()
            'ApproxGrossWeight()
            'HSCode()
            'DeliveryPort()
            'GenuineOrReplacement()
            'LiteratureOrDatasheet()
            'NewOrRefurbish()
            For Each objMod As VendorQuotationDetail In objModList

                Dim strSQL As String = String.Empty
                'Ali Faisal : TFS1310 : Insert and Update Ex works column value in detail table
                strSQL = "Update VendorQuotationDetail SET VendorQuotationId= " & objMod.VendorQuotationId & ", SerialNo = '" & objMod.SerialNo.Replace("'", "''") & "', RequirementDescription='" & objMod.RequirementDescription.Replace("'", "''") & "', ArticleId= " & IIf(objMod.ArticleId > 0, objMod.ArticleId, "NULL") & ", UnitId=" & objMod.UnitId & ", ItemTypeId= " & objMod.ItemTypeId & " , CategoryId= " & objMod.CategoryId & ", SubCategoryId= " & objMod.SubCategoryId & ", OriginId= " & objMod.OriginId & ", Qty=" & objMod.Qty & ", QuotedTerms = '" & objMod.QuotedTerms.Replace("'", "''") & "',  ValidityOfQuotation = '" & objMod.ValidityOfQuotation.Replace("'", "''") & "',  DeliveryPeriod = '" & objMod.DeliveryPeriod.Replace("'", "''") & "',  Warranty = '" & objMod.Warranty.Replace("'", "''") & "',  ApproxGrossWeight = '" & objMod.ApproxGrossWeight.Replace("'", "''") & "', HSCode = '" & objMod.HSCode.Replace("'", "''") & "', DeliveryPort = '" & objMod.DeliveryPort.Replace("'", "''") & "', GenuineOrReplacement = '" & objMod.GenuineOrReplacement.Replace("'", "''") & "', LiteratureOrDatasheet = '" & objMod.LiteratureOrDatasheet.Replace("'", "''") & "', NewOrRefurbish = '" & objMod.NewOrRefurbish.Replace("'", "''") & "', Price=" & objMod.Price & ", DiscountPer=" & objMod.DiscountPer & ", SalesTaxPer= " & objMod.SalesTaxPer & ",  AddTaxPer= " & objMod.AddTaxPer & ",  IncTaxPer= " & objMod.IncTaxPer & ",  CDPer= " & objMod.CDPer & ",  NetPrice= " & objMod.NetPrice & ",  OtherCharges= " & objMod.OtherCharges & ",  PurchaseInquiryDetailId= " & IIf(objMod.PurchaseInquiryDetailId > 0, objMod.PurchaseInquiryDetailId, "NULL") & ", ReferenceNo = '" & objMod.ReferenceNo.Replace("'", "''") & "', Comments = '" & objMod.Comments.Replace("'", "''") & "', HeadArticleId =  " & objMod.HeadArticleId & " BaseCurrencyId = " & objMod.BaseCurrencyId & ", BaseCurrencyRate = " & objMod.BaseCurrencyRate & ", CurrencyId = " & objMod.CurrencyId & ", CurrencyRate = " & objMod.CurrencyRate & ", CurrencySymbol = '" & objMod.CurrencySymbol.Replace("'", "''") & "', ExWorks = '" & objMod.ExWorks.Replace("'", "''") & "' WHERE PurchaseInquiryDetailId=" & objMod.PurchaseInquiryDetailId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next

            'trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try

    End Function

    Public Function Delete(ByVal ID As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From VendorQuotationDetail WHERE VendorQuotationDetailId=" & ID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function DeleteDetailWithPurhcase(ByVal VendorQuotationDetailId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From VendorQuotationDetail WHERE VendorQuotationDetailId=" & VendorQuotationDetailId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function DeleteSIIWise(ByVal Detail As List(Of VendorQuotationDetail), ByVal VendorQuoationId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            For Each obj As VendorQuotationDetail In Detail
                DeleteCharges(obj.VendorQuotationDetailId, trans)
            Next
            Dim strSQL As String = String.Empty
            strSQL = "Delete From VendorQuotationDetail WHERE VendorQuotationId=" & VendorQuoationId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function DisplayDetailForPurchase(ByVal PurchaseInquiryId As Integer) As DataTable
        Try

            '           PurchaseInquiryDetailId	int	Unchecked
            'PurchaseInquiryId	int	Checked
            'SerialNo	nvarchar(100)	Checked
            'RequirementDescription	nvarchar(500)	Checked
            'ArticleId	int	Checked
            'UnitId	int	Checked
            'ItemTypeId	int	Checked
            'CategoryId	int	Checked
            'SubCategoryId	int	Checked
            'OriginId	int	Checked
            'Qty	float	Checked
            'ReferenceNo	nvarchar(100)	Checked
            'Comments	nvarchar(500)	Checked
            'SalesInquiryDetailId	int	Checked
            'SalesInquiryId	int	Checked
            'Unchecked()
            Dim dt As New DataTable
            'dt = UtilityDAL.GetDataTable("SELECT 0 As VendorQuotationDetailId,  0 As VendorQuotationId, Detail.SerialNo, Detail.RequirementDescription,  IsNull(Detail.ArticleId, 0) As ArticleId, Article.ArticleCode As Code, Article.ArticleDescription,  IsNull(Detail.UnitId, 0) As UnitId, Unit.ArticleUnitName As Unit,  IsNull(Detail.ItemTypeId, 0) As ItemTypeId, Type.ArticleTypeName As Type,  IsNull(Detail.CategoryId, 0) As CategoryId, Category.ArticleCompanyName As Category,  IsNull(Detail.SubCategoryId, 0) As SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  IsNull(Detail.OriginId, 0) As OriginId, Origin.ArticleGenderName As Origin, IsNull(Detail.Qty, 0) As Qty, '' As QuotedTerms, '' As ValidityOfQuotation, '' As DeliveryPeriod, '' As Warranty, '' As ApproxGrossWeight, '' As HSCode, '' As DeliveryPort, '' As GenuineOrReplacement, '' As LiteratureOrDatasheet, '' As NewOrRefurbish, Convert(float, 0) As Price, Convert(float, 0) As DiscountPer, Convert(float, 0) As DiscountAmount,  Convert(float, 0) As SalesTaxPer, Convert(float, 0) As SalesTaxAmount, Convert(float, 0) As AddTaxPer,  Convert(float, 0) As AddTaxAmount, Convert(float, 0) As IncTaxPer,  Convert(float, 0) As IncTaxAmount, Convert(float, 0) As CDPer, Convert(float, 0) As CDAmount,  Convert(float, 0) As NetPrice, Convert(float, 0) As Amount,  Convert(float, 0) As OtherCharges, Convert(float, 0) As NetCostValue, IsNull(Detail.PurchaseInquiryDetailId, 0) As PurchaseInquiryDetailId, Detail.ReferenceNo, Detail.Comments, 0 As HeadArticleId, 0 As BaseCurrencyId, Convert(Float, 0) As BaseCurrencyRate, 0 CurrencyId, '' As Currency, Convert(Float, 0) As CurrencyRate, ''  CurrencySymbol, Convert(Bit, 0) As Alternate FROM PurchaseInquiryDetail As Detail INNER JOIN PurchaseInquiryMaster ON Detail.PurchaseInquiryId = PurchaseInquiryMaster.PurchaseInquiryId  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where Detail.PurchaseInquiryId =" & PurchaseInquiryId & " Order By Detail.SerialNo")
            'Ali Faisal : For proper sorting on 10-May-2017
            'Ali Faisal : TFS1310 : Add column of Ex works
            dt = UtilityDAL.GetDataTable("SELECT 0 As VendorQuotationDetailId,  0 As VendorQuotationId, Detail.SerialNo, Detail.RequirementDescription,  IsNull(Detail.ArticleId, 0) As ArticleId, Article.ArticleCode As Code, Article.ArticleDescription,  IsNull(Detail.UnitId, 0) As UnitId, Unit.ArticleUnitName As Unit,  IsNull(Detail.ItemTypeId, 0) As ItemTypeId, Type.ArticleTypeName As Type,  IsNull(Detail.CategoryId, 0) As CategoryId, Category.ArticleCompanyName As Category,  IsNull(Detail.SubCategoryId, 0) As SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  IsNull(Detail.OriginId, 0) As OriginId, Origin.ArticleGenderName As Origin, IsNull(Detail.Qty, 0) As Qty, '' As QuotedTerms, '' As ValidityOfQuotation, '' As DeliveryPeriod, '' As Warranty, '' As ApproxGrossWeight, '' As HSCode, '' As DeliveryPort, '' As GenuineOrReplacement, '' As LiteratureOrDatasheet, '' As NewOrRefurbish, Convert(float, 0) As Price, Convert(float, 0) As DiscountPer, Convert(float, 0) As DiscountAmount,  Convert(float, 0) As SalesTaxPer, Convert(float, 0) As SalesTaxAmount, Convert(float, 0) As AddTaxPer,  Convert(float, 0) As AddTaxAmount, Convert(float, 0) As IncTaxPer,  Convert(float, 0) As IncTaxAmount, Convert(float, 0) As CDPer, Convert(float, 0) As CDAmount,  Convert(float, 0) As NetPrice, Convert(float, 0) As Amount,  Convert(float, 0) As OtherCharges, Convert(float, 0) As NetCostValue, IsNull(Detail.PurchaseInquiryDetailId, 0) As PurchaseInquiryDetailId, Detail.ReferenceNo, Detail.Comments, 0 As HeadArticleId, 0 As BaseCurrencyId, Convert(Float, 0) As BaseCurrencyRate, 0 CurrencyId, '' As Currency, Convert(Float, 0) As CurrencyRate, ''  CurrencySymbol, Convert(Bit, 0) As Alternate, '' As ExWorks FROM PurchaseInquiryDetail As Detail INNER JOIN PurchaseInquiryMaster ON Detail.PurchaseInquiryId = PurchaseInquiryMaster.PurchaseInquiryId  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where Detail.PurchaseInquiryId =" & PurchaseInquiryId & " Order By CAST(Detail.SerialNo AS Numeric(10,0)) Asc ")
            dt.AcceptChanges()
            'Detail.DiscountPer, Convert(float, 0) As DiscountAmount,  Detail.SalesTaxPer, Convert(float, 0) As SalesTaxAmount, Detail.AddTaxPer,  Convert(float, 0) As AddTaxAmount, Detail.IncTaxPer,  Convert(float, 0) As IncTaxAmount, Detail.CDPer, Convert(float, 0) As CDAmount,  Detail.NetPrice, Convert(float, 0) As Amount,  Detail.OtherCharges, Convert(float, 0) As NetCostValue
            dt.Columns("DiscountAmount").Expression = "(IsNull(Price, 0)*IsNull(DiscountPer, 0))/100"
            dt.Columns("SalesTaxAmount").Expression = "(IsNull(Price, 0)*IsNull(SalesTaxPer, 0))/100"
            dt.Columns("AddTaxAmount").Expression = "(IsNull(Price, 0)*IsNull(AddTaxPer, 0))/100"
            dt.Columns("IncTaxAmount").Expression = "(IsNull(Price, 0)*IsNull(IncTaxPer, 0))/100"
            dt.Columns("CDAmount").Expression = "(IsNull(Price, 0)*IsNull(CDPer, 0))/100"
            dt.Columns("NetPrice").Expression = "(IsNull(Price, 0)-DiscountAmount)+SalesTaxAmount+AddTaxAmount+IncTaxAmount+CDAmount"
            dt.Columns("Amount").Expression = "(IsNull(NetPrice, 0)*IsNull(Qty, 0))"
            dt.Columns("NetCostValue").Expression = "(IsNull(Amount, 0)+IsNull(OtherCharges, 0))"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DisplayDetailForHistory(ByVal VendorQuotationId As Integer) As DataTable
        Try

            'VendorQuotationDetailId	int	Unchecked
            'VendorQuotationId	int	Checked
            'SerialNo	nvarchar(100)	Checked
            'RequirementDescription	nvarchar(200)	Checked
            'ArticleId	int	Checked
            'UnitId	int	Checked
            'ItemTypeId	int	Checked
            'CategoryId	int	Checked
            'SubCategoryId	int	Checked
            'OriginId	int	Checked
            'Qty	float	Checked
            'Price	float	Checked
            'DiscountPer	float	Checked
            'SalesTaxPer	float	Checked
            'AddTaxPer	float	Checked
            'IncTaxPer	float	Checked
            'CDPer	float	Checked
            'NetPrice	float	Checked
            'OtherCharges	float	Checked
            'PurchaseInquiryDetailId	int	Checked
            'Comments	nvarchar(300)	Checked
            '		Unchecked
            Dim dt As New DataTable
            'dt = UtilityDAL.GetDataTable("SELECT IsNull(Detail.VendorQuotationDetailId, 0) As VendorQuotationDetailId,  IsNull(Detail.VendorQuotationId, 0) As VendorQuotationId, Detail.SerialNo, Detail.RequirementDescription,  Detail.ArticleId As ArticleId, Article.ArticleCode As Code, Article.ArticleDescription,  IsNull(Detail.UnitId, 0) As UnitId, Unit.ArticleUnitName As Unit,  IsNull(Detail.ItemTypeId, 0) As ItemTypeId, Type.ArticleTypeName As Type,  IsNull(Detail.CategoryId, 0) As CategoryId, Category.ArticleCompanyName As Category,  IsNull(Detail.SubCategoryId, 0) As SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  IsNull(Detail.OriginId, 0) As OriginId, Origin.ArticleGenderName As Origin, IsNull(Detail.Qty, 0) As Qty, Detail.QuotedTerms, Detail.ValidityOfQuotation, Detail.DeliveryPeriod, Detail.Warranty, Detail.ApproxGrossWeight, Detail.HSCode, Detail.DeliveryPort, Detail.GenuineOrReplacement, Detail.LiteratureOrDatasheet, Detail.NewOrRefurbish, IsNull(Detail.Price, 0) As Price, IsNull(Detail.DiscountPer, 0) As DiscountPer, Convert(float, 0) As DiscountAmount,  IsNull(Detail.SalesTaxPer, 0) As SalesTaxPer, Convert(float, 0) As SalesTaxAmount, IsNull(Detail.AddTaxPer, 0) As AddTaxPer,  Convert(float, 0) As AddTaxAmount, IsNull(Detail.IncTaxPer, 0) As IncTaxPer,  Convert(float, 0) As IncTaxAmount, IsNull(Detail.CDPer, 0) As CDPer, Convert(float, 0) As CDAmount,  IsNull(Detail.NetPrice, 0) As NetPrice, Convert(float, 0) As Amount,  IsNull(Detail.OtherCharges, 0) As OtherCharges, Convert(float, 0) As NetCostValue, Detail.PurchaseInquiryDetailId, Detail.ReferenceNo, Detail.Comments, IsNull(Detail.HeadArticleId, 0) As HeadArticleId, IsNull(Detail.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Detail.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Detail.CurrencyId, 0) As CurrencyId, tblcurrency.currency_code As Currency, IsNull(Detail.CurrencyRate, 0) As CurrencyRate, Detail.CurrencySymbol, Convert(Bit, IsNull(Detail.Alternate, 0))  As Alternate FROM VendorQuotationDetail As Detail  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId LEFT OUTER JOIN tblcurrency ON Detail.CurrencyId = tblcurrency.currency_id Where Detail.VendorQuotationId =" & VendorQuotationId & " Order By Detail.SerialNo")
            'Ali Faisal : For proper sorting on 10-May-2017
            'Ali Faisal : TFS1310 : Add column of Ex Works
            'Ayesha Rehman : TFS3694 : Change Order by Clause
            ''Commented Against TFS3694
            ' dt = UtilityDAL.GetDataTable("SELECT IsNull(Detail.VendorQuotationDetailId, 0) As VendorQuotationDetailId,  IsNull(Detail.VendorQuotationId, 0) As VendorQuotationId, Detail.SerialNo, Detail.RequirementDescription,  Detail.ArticleId As ArticleId, Article.ArticleCode As Code, Article.ArticleDescription,  IsNull(Detail.UnitId, 0) As UnitId, Unit.ArticleUnitName As Unit,  IsNull(Detail.ItemTypeId, 0) As ItemTypeId, Type.ArticleTypeName As Type,  IsNull(Detail.CategoryId, 0) As CategoryId, Category.ArticleCompanyName As Category,  IsNull(Detail.SubCategoryId, 0) As SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  IsNull(Detail.OriginId, 0) As OriginId, Origin.ArticleGenderName As Origin, IsNull(Detail.Qty, 0) As Qty, Detail.QuotedTerms, Detail.ValidityOfQuotation, Detail.DeliveryPeriod, Detail.Warranty, Detail.ApproxGrossWeight, Detail.HSCode, Detail.DeliveryPort, Detail.GenuineOrReplacement, Detail.LiteratureOrDatasheet, Detail.NewOrRefurbish, IsNull(Detail.Price, 0) As Price, IsNull(Detail.DiscountPer, 0) As DiscountPer, Convert(float, 0) As DiscountAmount,  IsNull(Detail.SalesTaxPer, 0) As SalesTaxPer, Convert(float, 0) As SalesTaxAmount, IsNull(Detail.AddTaxPer, 0) As AddTaxPer,  Convert(float, 0) As AddTaxAmount, IsNull(Detail.IncTaxPer, 0) As IncTaxPer,  Convert(float, 0) As IncTaxAmount, IsNull(Detail.CDPer, 0) As CDPer, Convert(float, 0) As CDAmount,  IsNull(Detail.NetPrice, 0) As NetPrice, Convert(float, 0) As Amount,  IsNull(Detail.OtherCharges, 0) As OtherCharges, Convert(float, 0) As NetCostValue, Detail.PurchaseInquiryDetailId, Detail.ReferenceNo, Detail.Comments, IsNull(Detail.HeadArticleId, 0) As HeadArticleId, IsNull(Detail.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Detail.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Detail.CurrencyId, 0) As CurrencyId, tblcurrency.currency_code As Currency, IsNull(Detail.CurrencyRate, 0) As CurrencyRate, Detail.CurrencySymbol, Convert(Bit, IsNull(Detail.Alternate, 0))  As Alternate, Detail.ExWorks FROM VendorQuotationDetail As Detail  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId LEFT OUTER JOIN tblcurrency ON Detail.CurrencyId = tblcurrency.currency_id Where Detail.VendorQuotationId =" & VendorQuotationId & " Order By Detail.SerialNo")
            dt = UtilityDAL.GetDataTable("SELECT IsNull(Detail.VendorQuotationDetailId, 0) As VendorQuotationDetailId,  IsNull(Detail.VendorQuotationId, 0) As VendorQuotationId, Detail.SerialNo, Detail.RequirementDescription,  Detail.ArticleId As ArticleId, Article.ArticleCode As Code, Article.ArticleDescription,  IsNull(Detail.UnitId, 0) As UnitId, Unit.ArticleUnitName As Unit,  IsNull(Detail.ItemTypeId, 0) As ItemTypeId, Type.ArticleTypeName As Type,  IsNull(Detail.CategoryId, 0) As CategoryId, Category.ArticleCompanyName As Category,  IsNull(Detail.SubCategoryId, 0) As SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  IsNull(Detail.OriginId, 0) As OriginId, Origin.ArticleGenderName As Origin, IsNull(Detail.Qty, 0) As Qty, Detail.QuotedTerms, Detail.ValidityOfQuotation, Detail.DeliveryPeriod, Detail.Warranty, Detail.ApproxGrossWeight, Detail.HSCode, Detail.DeliveryPort, Detail.GenuineOrReplacement, Detail.LiteratureOrDatasheet, Detail.NewOrRefurbish, IsNull(Detail.Price, 0) As Price, IsNull(Detail.DiscountPer, 0) As DiscountPer, Convert(float, 0) As DiscountAmount,  IsNull(Detail.SalesTaxPer, 0) As SalesTaxPer, Convert(float, 0) As SalesTaxAmount, IsNull(Detail.AddTaxPer, 0) As AddTaxPer,  Convert(float, 0) As AddTaxAmount, IsNull(Detail.IncTaxPer, 0) As IncTaxPer,  Convert(float, 0) As IncTaxAmount, IsNull(Detail.CDPer, 0) As CDPer, Convert(float, 0) As CDAmount,  IsNull(Detail.NetPrice, 0) As NetPrice, Convert(float, 0) As Amount,  IsNull(Detail.OtherCharges, 0) As OtherCharges, Convert(float, 0) As NetCostValue, Detail.PurchaseInquiryDetailId, Detail.ReferenceNo, Detail.Comments, IsNull(Detail.HeadArticleId, 0) As HeadArticleId, IsNull(Detail.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Detail.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Detail.CurrencyId, 0) As CurrencyId, tblcurrency.currency_code As Currency, IsNull(Detail.CurrencyRate, 0) As CurrencyRate, Detail.CurrencySymbol, Convert(Bit, IsNull(Detail.Alternate, 0))  As Alternate, Detail.ExWorks FROM VendorQuotationDetail As Detail  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId LEFT OUTER JOIN tblcurrency ON Detail.CurrencyId = tblcurrency.currency_id Where Detail.VendorQuotationId =" & VendorQuotationId & " Order By CAST(Detail.SerialNo AS Numeric(10,0)) Asc")
            dt.AcceptChanges()
            'Detail.DiscountPer, Convert(float, 0) As DiscountAmount,  Detail.SalesTaxPer, Convert(float, 0) As SalesTaxAmount, Detail.AddTaxPer,  Convert(float, 0) As AddTaxAmount, Detail.IncTaxPer,  Convert(float, 0) As IncTaxAmount, Detail.CDPer, Convert(float, 0) As CDAmount,  Detail.NetPrice, Convert(float, 0) As Amount,  Detail.OtherCharges, Convert(float, 0) As NetCostValue
            dt.Columns("DiscountAmount").Expression = "(IsNull(Price, 0)*IsNull(DiscountPer, 0))/100"
            dt.Columns("SalesTaxAmount").Expression = "(IsNull(Price, 0)*IsNull(SalesTaxPer, 0))/100"
            dt.Columns("AddTaxAmount").Expression = "(IsNull(Price, 0)*IsNull(AddTaxPer, 0))/100"
            dt.Columns("IncTaxAmount").Expression = "(IsNull(Price, 0)*IsNull(IncTaxPer, 0))/100"
            dt.Columns("CDAmount").Expression = "(IsNull(Price, 0)*IsNull(CDPer, 0))/100"
            dt.Columns("NetPrice").Expression = "(IsNull(Price, 0)-DiscountAmount)+SalesTaxAmount+AddTaxAmount+IncTaxAmount+CDAmount"
            dt.Columns("Amount").Expression = "(IsNull(NetPrice, 0)*IsNull(Qty, 0))"
            dt.Columns("NetCostValue").Expression = "(IsNull(Amount, 0)+IsNull(OtherCharges, 0))"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Public Function UpdateSalesInquiryStatusAgainstAddition(ByVal SalesInquiryId As Integer, ByVal SalesInquiryDetailId As Integer, ByVal Qty As Double, ByVal trans As SqlTransaction) As Boolean
    '    Dim Con As New SqlConnection(SQLHelper.CON_STR)
    '    If Con.State = ConnectionState.Closed Then Con.Open()
    '    'Dim trans As SqlTransaction = Con.BeginTransaction
    '    Try
    '        Dim strSQL As String = String.Empty
    '        strSQL = "Update SalesInquiryDetail Set PurchasedQty = IsNull(PurchasedQty, 0)+" & Qty & " WHERE SalesInquiryDetailId=" & SalesInquiryDetailId & ""
    '        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
    '        'trans.Commit()
    '        Return True
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function
    'Public Function UpdateSalesInquiryStatusAgainstSubtraction(ByVal SalesInquiryId As Integer, ByVal SalesInquiryDetailId As Integer, ByVal Qty As Double, ByVal trans As SqlTransaction) As Boolean
    '    Dim Con As New SqlConnection(SQLHelper.CON_STR)
    '    If Con.State = ConnectionState.Closed Then Con.Open()
    '    'Dim trans As SqlTransaction = Con.BeginTransaction
    '    Try
    '        Dim strSQL As String = String.Empty
    '        strSQL = "Update SalesInquiryDetail Set PurchasedQty = IsNull(PurchasedQty, 0)-" & Qty & " WHERE SalesInquiryDetailId=" & SalesInquiryDetailId & ""
    '        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
    '        'trans.Commit()
    '        Return True
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function
    Public Function AddChargesType(ByVal ChargesType As String) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
                'If objMod.SalesInquiryDetailId > 0 Then
                '    UpdateSalesInquiryStatusAgainstAddition(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans)
                'End If
            strSQL = "INSERT INTO VendorQuotationChargesType(TypeName) " _
                & " VALUES('" & ChargesType.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddCharges(ByVal obj As VendorQuotationDetail, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            DeleteCharges(obj.VendorQuotationDetailId, trans)
            For Each objMod As VendorQuotationDetailCharges In obj.ChargesDetail
                Dim strSQL As String = String.Empty
                strSQL = "INSERT INTO VendorQuotationDetailCharges(VendorQuotationDetailId, VendorQuotationChargesTypeId, Amount) " _
                & " VALUES(" & obj.VendorQuotationDetailId & ", " & objMod.VendorQuotationChargesTypeId & ", " & objMod.Amount & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetChargesTypes() As DataTable
        Try
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT 0 As VendorQuotationDetailChargesId, VenorQuotationChargesTypeId As VendorQuotationChargesTypeId, TypeName, 0 As VendorQuotationDetailId, Convert(float, 0) As Amount From VendorQuotationChargesType")
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDetailCharges(ByVal VendorQuoationDetailId As Integer) As DataTable
        Try
            '            VendorQuotationDetailChargesId	int	Unchecked
            'VendorQuotationDetailId	int	Checked
            'VendorQuotationChargesTypeId	int	Checked
            'Amount	float	Checked
            '            Unchecked()
            Dim dt As New DataTable
            If VendorQuoationDetailId > 0 Then
                'dt = UtilityDAL.GetDataTable("SELECT Charges.VendorQuotationChargesTypeId, ChargesType.TypeName, Charges.VendorQuotationDetailChargesId,  Charges.VendorQuotationDetailId, Charges.Amount From VendorQuotationChargesType As ChargesType Left Outer Join VendorQuotationDetailCharges As Charges  ON Charges.VendorQuotationChargesTypeId = ChargesType.VenorQuotationChargesTypeId Where Charges.VendorQuotationDetailId = " & VendorQuoationDetailId & " Union SELECT VenorQuotationChargesTypeId As VendorQuotationChargesTypeId, TypeName, 0 As VendorQuotationDetailChargesId,  0 As VendorQuotationDetailId, Convert(float, 0) As Amount From VendorQuotationChargesType")
                'Ali Faisal : TFS1314 : Show only that charges types which have charges more than zero amount
                dt = UtilityDAL.GetDataTable("SELECT ChargesType.VenorQuotationChargesTypeId As VendorQuotationChargesTypeId, ChargesType.TypeName, Charges.VendorQuotationDetailChargesId,  Charges.VendorQuotationDetailId, IsNull(Charges.Amount, 0) As Amount From VendorQuotationChargesType As ChargesType Inner Join( Select VendorQuotationChargesTypeId, VendorQuotationDetailChargesId, VendorQuotationDetailId, Amount From  VendorQuotationDetailCharges  Where VendorQuotationDetailId = " & VendorQuoationDetailId & " ) As Charges  ON Charges.VendorQuotationChargesTypeId = ChargesType.VenorQuotationChargesTypeId ")
                'Ali Faisal : TFS1314 : End
            Else
                dt = UtilityDAL.GetDataTable("SELECT VenorQuotationChargesTypeId As VendorQuotationChargesTypeId, TypeName, 0 As VendorQuotationDetailChargesId,  0 As VendorQuotationDetailId, Convert(float, 0) As Amount From VendorQuotationChargesType")
            End If
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetVendorCharges(ByVal VendorQuoationDetailId As Integer) As DataTable
        Try
            Dim dt As New DataTable
            If VendorQuoationDetailId > 0 Then
                dt = UtilityDAL.GetDataTable("SELECT ChargesType.VenorQuotationChargesTypeId As VendorQuotationChargesTypeId, ChargesType.TypeName, Charges.VendorQuotationChargesId,  Charges.VendorQuotationDetailId, IsNull(Charges.Amount, 0) As Amount From VendorQuotationChargesType As ChargesType Inner Join ( Select VendorQuotationChargesTypeId, VendorQuotationChargesId, VendorQuotationDetailId, Amount From  VendorQuotationCharges  Where VendorQuotationDetailId = " & VendorQuoationDetailId & " ) As Charges  ON Charges.VendorQuotationChargesTypeId = ChargesType.VenorQuotationChargesTypeId ")
            Else
                dt = UtilityDAL.GetDataTable("SELECT VenorQuotationChargesTypeId As VendorQuotationChargesTypeId, TypeName, 0 As VendorQuotationDetailChargesId,  0 As VendorQuotationDetailId, Convert(float, 0) As Amount From VendorQuotationChargesType")
            End If
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DeleteCharges(ByVal VendorQuotationDetailId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From VendorQuotationDetailCharges WHERE VendorQuotationDetailId=" & VendorQuotationDetailId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            Con.Close()
        End Try

    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1314 : To get the Unsaved charges type for further charges add to quotation
    ''' </summary>
    ''' <param name="VendorQuoationDetailId"></param>
    ''' <returns></returns>
    ''' <remarks>Ali Faisal : TFS1314 : 11-Aug-2017</remarks>
    Public Function GetRemainingChargesTypes(ByVal VendorQuoationDetailId As Integer) As DataTable
        Try
            Dim dt As New DataTable
            If VendorQuoationDetailId > 0 Then
                dt = UtilityDAL.GetDataTable("SELECT ChargesType.VenorQuotationChargesTypeId As VendorQuotationChargesTypeId, ChargesType.TypeName, Charges.VendorQuotationDetailChargesId,  Charges.VendorQuotationDetailId, IsNull(Charges.Amount, 0) As Amount From VendorQuotationChargesType As ChargesType Left Outer Join( Select VendorQuotationChargesTypeId, VendorQuotationDetailChargesId, VendorQuotationDetailId, Amount From  VendorQuotationDetailCharges  Where VendorQuotationDetailId = " & VendorQuoationDetailId & " ) As Charges  ON Charges.VendorQuotationChargesTypeId = ChargesType.VenorQuotationChargesTypeId ")
            Else
                dt = UtilityDAL.GetDataTable("SELECT VenorQuotationChargesTypeId As VendorQuotationChargesTypeId, TypeName, 0 As VendorQuotationDetailChargesId,  0 As VendorQuotationDetailId, Convert(float, 0) As Amount From VendorQuotationChargesType")
            End If
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1314 : Delete charges types that are not used against any Quotation
    ''' </summary>
    ''' <param name="VendorQuotationChargesTypeId"></param>
    ''' <returns></returns>
    ''' <remarks>Ali Faisal : TFS1314 : 11-Aug-2017</remarks>
    Public Function DeleteChargesTypes(ByVal VendorQuotationChargesTypeId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From VendorQuotationChargesType Where VenorQuotationChargesTypeId Not In(Select VendorQuotationChargesTypeId from VendorQuotationDetailCharges) And VenorQuotationChargesTypeId = " & VendorQuotationChargesTypeId & ""
            Dim dt As DataTable
            dt = UtilityDAL.GetDataTable("Select VenorQuotationChargesTypeId From VendorQuotationChargesType Where VenorQuotationChargesTypeId Not In(Select VendorQuotationChargesTypeId from VendorQuotationDetailCharges) And VenorQuotationChargesTypeId = " & VendorQuotationChargesTypeId & "")
            If dt.Rows.Count > 0 Then
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Else
                Return False
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
            Return False
        Finally
            Con.Close()
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1314 : Modify the charges types
    ''' </summary>
    ''' <param name="VendorQuotationChargesTypeId"></param>
    ''' <param name="TypeName"></param>
    ''' <returns></returns>
    ''' <remarks>Ali Faisal : TFS1314 : 11-Aug-2017</remarks>
    Public Function UpdateChargesTypes(ByVal VendorQuotationChargesTypeId As Integer, ByVal TypeName As String) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update VendorQuotationChargesType Set TypeName='" & TypeName & "' Where VenorQuotationChargesTypeId = " & VendorQuotationChargesTypeId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetCustomerRemainingChargesTypes(ByVal VendorQuoationDetailId As Integer) As DataTable
        Try
            Dim dt As New DataTable
            If VendorQuoationDetailId > 0 Then
                dt = UtilityDAL.GetDataTable("SELECT ChargesType.VenorQuotationChargesTypeId As VendorQuotationChargesTypeId, ChargesType.TypeName, Charges.VendorQuotationDetailChargesId,  Charges.VendorQuotationDetailId, IsNull(Charges.Amount, 0) As Amount From VendorQuotationChargesType As ChargesType Left Outer Join( Select VendorQuotationChargesTypeId, VendorQuotationDetailChargesId, VendorQuotationDetailId, Amount From  VendorQuotationDetailCharges  Where VendorQuotationDetailId = " & VendorQuoationDetailId & " ) As Charges  ON Charges.VendorQuotationChargesTypeId = ChargesType.VenorQuotationChargesTypeId ")
            Else
                dt = UtilityDAL.GetDataTable("SELECT VenorQuotationChargesTypeId As VendorQuotationChargesTypeId, TypeName, 0 As VendorQuotationDetailChargesId,  0 As VendorQuotationDetailId, Convert(float, 0) As Amount From VendorQuotationChargesType")
            End If
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetVendorRemainingChargesTypes(ByVal VendorQuoationDetailId As Integer) As DataTable
        Try
            Dim dt As New DataTable
            If VendorQuoationDetailId > 0 Then
                dt = UtilityDAL.GetDataTable("SELECT ChargesType.VenorQuotationChargesTypeId As VendorQuotationChargesTypeId, ChargesType.TypeName, Charges.VendorQuotationChargesId,  Charges.VendorQuotationDetailId, IsNull(Charges.Amount, 0) As Amount From VendorQuotationChargesType As ChargesType Left Outer Join ( Select VendorQuotationChargesTypeId, VendorQuotationChargesId, VendorQuotationDetailId, Amount From  VendorQuotationCharges  Where VendorQuotationDetailId = " & VendorQuoationDetailId & " ) As Charges  ON Charges.VendorQuotationChargesTypeId = ChargesType.VenorQuotationChargesTypeId")
            Else
                dt = UtilityDAL.GetDataTable("SELECT VenorQuotationChargesTypeId As VendorQuotationChargesTypeId, TypeName, 0 As VendorQuotationDetailChargesId,  0 As VendorQuotationDetailId, Convert(float, 0) As Amount From VendorQuotationChargesType")
            End If
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS3534
    ''' </summary>
    ''' <param name="PurchaseInquiryId"></param>
    ''' <param name="PurchaseInquiryDetailIds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DisplayRemainingPurchaseInquiryDetail(ByVal PurchaseInquiryId As Integer, ByVal PurchaseInquiryDetailIds As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT 0 As VendorQuotationDetailId,  0 As VendorQuotationId, Detail.SerialNo, Detail.RequirementDescription,  IsNull(Detail.ArticleId, 0) As ArticleId, Article.ArticleCode As Code, Article.ArticleDescription,  IsNull(Detail.UnitId, 0) As UnitId, Unit.ArticleUnitName As Unit,  IsNull(Detail.ItemTypeId, 0) As ItemTypeId, Type.ArticleTypeName As Type,  IsNull(Detail.CategoryId, 0) As CategoryId, Category.ArticleCompanyName As Category,  IsNull(Detail.SubCategoryId, 0) As SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  IsNull(Detail.OriginId, 0) As OriginId, Origin.ArticleGenderName As Origin, IsNull(Detail.Qty, 0) As Qty, '' As QuotedTerms, '' As ValidityOfQuotation, '' As DeliveryPeriod, '' As Warranty, '' As ApproxGrossWeight, '' As HSCode, '' As DeliveryPort, '' As GenuineOrReplacement, '' As LiteratureOrDatasheet, '' As NewOrRefurbish, Convert(float, 0) As Price, Convert(float, 0) As DiscountPer, Convert(float, 0) As DiscountAmount,  Convert(float, 0) As SalesTaxPer, Convert(float, 0) As SalesTaxAmount, Convert(float, 0) As AddTaxPer,  Convert(float, 0) As AddTaxAmount, Convert(float, 0) As IncTaxPer,  Convert(float, 0) As IncTaxAmount, Convert(float, 0) As CDPer, Convert(float, 0) As CDAmount,  Convert(float, 0) As NetPrice, Convert(float, 0) As Amount,  Convert(float, 0) As OtherCharges, Convert(float, 0) As NetCostValue, IsNull(Detail.PurchaseInquiryDetailId, 0) As PurchaseInquiryDetailId, Detail.ReferenceNo, Detail.Comments, 0 As HeadArticleId, 0 As BaseCurrencyId, Convert(Float, 0) As BaseCurrencyRate, 0 CurrencyId, '' As Currency, Convert(Float, 0) As CurrencyRate, ''  CurrencySymbol, Convert(Bit, 0) As Alternate, '' As ExWorks FROM PurchaseInquiryDetail As Detail INNER JOIN PurchaseInquiryMaster ON Detail.PurchaseInquiryId = PurchaseInquiryMaster.PurchaseInquiryId  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where Detail.PurchaseInquiryId =" & PurchaseInquiryId & " " & IIf(PurchaseInquiryDetailIds.Length > 0, " AND IsNull(Detail.PurchaseInquiryDetailId, 0) NOT IN (" & PurchaseInquiryDetailIds & ") ", "") & " Order By Detail.SerialNo")
            dt.AcceptChanges()
            dt.Columns("DiscountAmount").Expression = "(IsNull(Price, 0)*IsNull(DiscountPer, 0))/100"
            dt.Columns("SalesTaxAmount").Expression = "(IsNull(Price, 0)*IsNull(SalesTaxPer, 0))/100"
            dt.Columns("AddTaxAmount").Expression = "(IsNull(Price, 0)*IsNull(AddTaxPer, 0))/100"
            dt.Columns("IncTaxAmount").Expression = "(IsNull(Price, 0)*IsNull(IncTaxPer, 0))/100"
            dt.Columns("CDAmount").Expression = "(IsNull(Price, 0)*IsNull(CDPer, 0))/100"
            dt.Columns("NetPrice").Expression = "(IsNull(Price, 0)-DiscountAmount)+SalesTaxAmount+AddTaxAmount+IncTaxAmount+CDAmount"
            dt.Columns("Amount").Expression = "(IsNull(NetPrice, 0)*IsNull(Qty, 0))"
            dt.Columns("NetCostValue").Expression = "(IsNull(Amount, 0)+IsNull(OtherCharges, 0))"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
