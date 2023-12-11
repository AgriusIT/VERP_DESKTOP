Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports SBUtility.Utility
Imports System.Net
Imports System.Web
Imports System.Text
Imports System.Drawing
Imports System.IO
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Xml
Public Class EmailTemplateDAL
    Public Function Add(ByVal Title As String, ByVal EmailTemplate As String, ByVal HtmlContent As String, ByVal FieldsContent As String) As Boolean
        Dim objCon As New SqlConnection(SQLHelper.CON_STR) ' Create object of sql client connection
        If objCon.State = ConnectionState.Closed Then objCon.Open() 'Database Connectivity Process
        Dim objTrans As SqlTransaction = objCon.BeginTransaction ' 'Start Transaction  
        Try
            Dim strSQL As String = String.Empty 'Create Variable for INSERTION Query
            strSQL = "Insert Into EmailTemplateTable(Title, EmailTemplate, HTMLContent, FieldsContent) Values(N'" & Title.Replace("'", "''") & "', N'" & EmailTemplate.Replace("'", "''") & "', N'" & HtmlContent.Replace("'", "''") & "', N'" & FieldsContent.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL) 'Execure SQL Query
            objTrans.Commit() 'Record Save Process
            Return True

        Catch ex As Exception
            objTrans.Rollback() 'because some of data is not provided
            Throw New Exception("Some of data is not provided.")
        Finally
            objCon.Close() 'after safe record in database
        End Try
    End Function
    Public Function Update(ByVal Title As String, ByVal EmailTemplate As String, ByVal HtmlContent As String, ByVal FieldsContent As String, ByVal ID As Integer) As Boolean
        Dim objCon As New SqlConnection(SQLHelper.CON_STR) ' Create object of sql client connection
        If objCon.State = ConnectionState.Closed Then objCon.Open() 'Database Connectivity Process
        Dim objTrans As SqlTransaction = objCon.BeginTransaction ' 'Start Transaction  
        Try
            Dim strSQL As String = String.Empty 'Create Variable for INSERTION Query
            strSQL = "Update EmailTemplateTable Set Title ='" & Title.Replace("'", "''") & "', EmailTemplate ='" & EmailTemplate.Replace("'", "''") & "', HTMLContent ='" & HtmlContent.Replace("'", "''") & "', FieldsContent ='" & FieldsContent.Replace("'", "''") & "' Where ID= " & ID & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL) 'Execure SQL Query
            objTrans.Commit() 'Record Save Process
            Return True

        Catch ex As Exception
            objTrans.Rollback() 'because some of data is not provided
            Throw New Exception("Some of data is not provided.")
        Finally
            objCon.Close() 'after safe record in database
        End Try
    End Function
    Public Function Delete(ByVal ID As Integer) As Boolean
        Dim objCon As New SqlConnection(SQLHelper.CON_STR) ' Create object of sql client connection
        If objCon.State = ConnectionState.Closed Then objCon.Open() 'Database Connectivity Process
        Dim objTrans As SqlTransaction = objCon.BeginTransaction ' 'Start Transaction  
        Try
            Dim strSQL As String = String.Empty 'Create Variable for INSERTION Query
            strSQL = "Delete From EmailTemplateTable Where ID =" & ID & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL) 'Execure SQL Query
            objTrans.Commit() 'Record Save Process
            Return True

        Catch ex As Exception
            objTrans.Rollback() 'because some of data is not provided
            Throw New Exception("Some of data is not provided.")
        Finally
            objCon.Close() 'after safe record in database
        End Try
    End Function
    Public Function GetAll() As DataTable
        Dim dt As New DataTable
        Try
            dt = UtilityDAL.GetDataTable("Select * FROM EmailTemplateTable")
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTemplate(ByVal EmailTitle As String) As String
        Dim Template As String = String.Empty
        Dim dt As New DataTable
        Try
            dt = UtilityDAL.GetDataTable("Select EmailTemplate FROM EmailTemplateTable Where Title Like '%" & EmailTitle & "%'")
            If dt.Rows.Count > 0 Then
                Template = dt.Rows(0).Item(0).ToString
            End If
            Return Template
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsExisted(ByVal EmailTitle As String) As Boolean
        Dim Template As String = String.Empty
        Dim dt As New DataTable
        Try
            dt = UtilityDAL.GetDataTable("Select Count(*) AS Count1 FROM EmailTemplateTable Where Title Like '%" & EmailTitle & "%'")
            If dt.Rows(0).Item(0) > 0 Then
                Return True
            Else
                Return False
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetColumns(ByVal TableName As String) As DataTable
        Dim Template As String = String.Empty
        Dim dt As New DataTable
        Try
            If TableName = "Purchase Inquiry" Then
                '     IsNull(Detail.SalesInquiryDetailId, 0) As SalesInquiryDetailId
                dt = UtilityDAL.GetDataTable("SELECT 'SerialNo' AS COLUMN_NAME, 'SerialNo' COLUMN_NAME " _
                    & " Union SELECT 'RequirementDescription' AS COLUMN_NAME, 'RequirementDescription' AS COLUMN_NAME " _
                    & " Union  SELECT 'Code' AS COLUMN_NAME, 'Code' AS COLUMN_NAME " _
                    & " Union  SELECT 'ArticleDescription' AS COLUMN_NAME, 'ArticleDescription' AS COLUMN_NAME " _
                    & " Union  SELECT 'Unit' AS COLUMN_NAME, 'Unit' AS COLUMN_NAME " _
                    & " Union  SELECT 'Type' AS COLUMN_NAME, 'Type' AS COLUMN_NAME" _
                    & " Union  SELECT 'Category' AS COLUMN_NAME, 'Category' AS COLUMN_NAME " _
                    & " Union  SELECT 'SubCategory' AS COLUMN_NAME, 'SubCategory' AS COLUMN_NAME " _
                    & " Union  SELECT 'Origin' AS COLUMN_NAME, 'Origin' AS COLUMN_NAME " _
                    & " Union  SELECT 'Qty' AS COLUMN_NAME, 'Qty' AS COLUMN_NAME " _
                    & " Union  SELECT 'ReferenceNo' AS COLUMN_NAME, 'ReferenceNo' AS COLUMN_NAME " _
                    & " Union  SELECT 'Comments' AS COLUMN_NAME, 'Comments' AS COLUMN_NAME " _
                    & " Union  SELECT 'Comments' AS COLUMN_NAME, 'Comments' AS COLUMN_NAME ")
                'Changes for email of purchase demand
            ElseIf TableName = "Purchase Demand" Then
                '     IsNull(Detail.SalesInquiryDetailId, 0) As SalesInquiryDetailId
                dt = UtilityDAL.GetDataTable("SELECT 'ArticleCode' AS COLUMN_NAME, 'ArticleCode' COLUMN_NAME " _
                    & " Union SELECT 'Item' AS COLUMN_NAME, 'Item' AS COLUMN_NAME " _
                    & " Union  SELECT 'Qty' AS COLUMN_NAME, 'Qty' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrentPrice' AS COLUMN_NAME, 'CurrentPrice' AS COLUMN_NAME " _
                    & " Union  SELECT 'Comments' AS COLUMN_NAME, 'Comments' AS COLUMN_NAME ")
                'Changes for email of purchase demand
            ElseIf TableName = "Sales Quotation" Then
                '& "   Convert(bit,Recv_D.Alternate) As Alternate
                dt = UtilityDAL.GetDataTable("SELECT 'SerialNo1' AS COLUMN_NAME, 'SerialNo1' COLUMN_NAME " _
                  & " Union SELECT 'ArticleCode' AS COLUMN_NAME, 'ArticleCode' AS COLUMN_NAME " _
                  & " Union  SELECT 'item' AS COLUMN_NAME, 'item' AS COLUMN_NAME " _
                  & " Union  SELECT 'RequirementDescription' AS COLUMN_NAME, 'RequirementDescription' AS COLUMN_NAME " _
                  & " Union  SELECT 'unit' AS COLUMN_NAME, 'unit' AS COLUMN_NAME " _
                  & " Union  SELECT 'Qty' AS COLUMN_NAME, 'Qty' AS COLUMN_NAME" _
                  & " Union  SELECT 'Category' AS COLUMN_NAME, 'Category' AS COLUMN_NAME " _
                  & " Union  SELECT 'SubCategory' AS COLUMN_NAME, 'SubCategory' AS COLUMN_NAME " _
                  & " Union  SELECT 'Origin' AS COLUMN_NAME, 'Origin' AS COLUMN_NAME " _
                  & " Union  SELECT 'Qty' AS COLUMN_NAME, 'Qty' AS COLUMN_NAME " _
                  & " Union  SELECT 'PostDiscountPrice' AS COLUMN_NAME, 'PostDiscountPrice' AS COLUMN_NAME " _
                  & " Union  SELECT 'Price' AS COLUMN_NAME, 'Price' AS COLUMN_NAME " _
                  & " Union  SELECT 'BaseCurrencyRate' AS COLUMN_NAME, 'BaseCurrencyRate' AS COLUMN_NAME " _
                  & " Union  SELECT 'CurrencyAmount' AS COLUMN_NAME, 'CurrencyAmount' AS COLUMN_NAME " _
                  & " Union  SELECT 'TotalCurrencyAmount' AS COLUMN_NAME, 'TotalCurrencyAmount' AS COLUMN_NAME " _
                  & " Union  SELECT 'DiscountFactor' AS COLUMN_NAME, 'DiscountFactor' AS COLUMN_NAME " _
                  & " Union  SELECT 'DiscountValue' AS COLUMN_NAME, 'DiscountValue' AS COLUMN_NAME " _
                  & " Union  SELECT 'Total' AS COLUMN_NAME, 'Total' AS COLUMN_NAME " _
                  & " Union  SELECT 'PackQty' AS COLUMN_NAME, 'PackQty' AS COLUMN_NAME " _
                  & " Union  SELECT 'CurrentPrice' AS COLUMN_NAME, 'CurrentPrice' AS COLUMN_NAME " _
                  & " Union  SELECT 'PackPrice' AS COLUMN_NAME, 'PackPrice' AS COLUMN_NAME " _
                  & " Union  SELECT 'SalesTax_Percentage' AS COLUMN_NAME, 'SalesTax_Percentage' AS COLUMN_NAME " _
                  & " Union  SELECT 'SalesTaxAmount' AS COLUMN_NAME, 'SalesTaxAmount' AS COLUMN_NAME " _
                  & " Union  SELECT 'CurrencySalesTaxAmount' AS COLUMN_NAME, 'CurrencySalesTaxAmount' AS COLUMN_NAME " _
                  & " Union  SELECT 'SED_Tax_Percent' AS COLUMN_NAME, 'SED_Tax_Percent' AS COLUMN_NAME " _
                  & " Union  SELECT 'CurrencySEDAmount' AS COLUMN_NAME, 'CurrencySEDAmount' AS COLUMN_NAME " _
                 & " Union  SELECT 'Net_Amount' AS COLUMN_NAME, 'Net_Amount' AS COLUMN_NAME " _
                  & " Union  SELECT 'SchemeQty' AS COLUMN_NAME, 'SchemeQty' AS COLUMN_NAME " _
                  & " Union  SELECT 'Discount_Percentage' AS COLUMN_NAME, 'Discount_Percentage' AS COLUMN_NAME " _
                  & " Union  SELECT 'PurchasePrice' AS COLUMN_NAME, 'PurchasePrice' AS COLUMN_NAME " _
                   & " Union  SELECT 'Net_Amount' AS COLUMN_NAME, 'Net_Amount' AS COLUMN_NAME " _
                  & " Union  SELECT 'Pack_Desc' AS COLUMN_NAME, 'Pack_Desc' AS COLUMN_NAME " _
                  & " Union  SELECT 'Comments' AS COLUMN_NAME, 'Comments' AS COLUMN_NAME " _
                  & " Union  SELECT 'ItemDescription' AS COLUMN_NAME, 'ItemDescription' AS COLUMN_NAME " _
                 & " Union  SELECT 'Brand' AS COLUMN_NAME, 'Brand' AS COLUMN_NAME " _
                  & " Union  SELECT 'Specification' AS COLUMN_NAME, 'Specification' AS COLUMN_NAME " _
                  & " Union  SELECT 'ItemRegistrationNo' AS COLUMN_NAME, 'ItemRegistrationNo' AS COLUMN_NAME " _
                  & " Union  SELECT 'TradePrice' AS COLUMN_NAME, 'TradePrice' AS COLUMN_NAME " _
                  & " Union  SELECT 'TenderSrNo' AS COLUMN_NAME, 'TenderSrNo' AS COLUMN_NAME " _
                  & " Union  SELECT 'CostPrice' AS COLUMN_NAME, 'CostPrice' AS COLUMN_NAME " _
                  & " Union  SELECT 'SOQuantity' AS COLUMN_NAME, 'SOQuantity' AS COLUMN_NAME " _
                  & " Union  SELECT 'TotalQty' AS COLUMN_NAME, 'TotalQty' AS COLUMN_NAME " _
                 & " Union  SELECT 'Alternate' AS COLUMN_NAME, 'Alternate' AS COLUMN_NAME ")
            ElseIf TableName = "Sales Inquiry" Then
                dt = UtilityDAL.GetDataTable("SELECT 'SerialNo' AS COLUMN_NAME, 'SerialNo' COLUMN_NAME " _
                    & " Union SELECT 'RequirementDescription' AS COLUMN_NAME, 'RequirementDescription' AS COLUMN_NAME " _
                    & " Union  SELECT 'Code' AS COLUMN_NAME, 'Code' AS COLUMN_NAME " _
                    & " Union  SELECT 'ArticleDescription' AS COLUMN_NAME, 'ArticleDescription' AS COLUMN_NAME " _
                    & " Union  SELECT 'Unit' AS COLUMN_NAME, 'Unit' AS COLUMN_NAME " _
                    & " Union  SELECT 'Type' AS COLUMN_NAME, 'Type' AS COLUMN_NAME" _
                    & " Union  SELECT 'Category' AS COLUMN_NAME, 'Category' AS COLUMN_NAME " _
                    & " Union  SELECT 'SubCategory' AS COLUMN_NAME, 'SubCategory' AS COLUMN_NAME " _
                    & " Union  SELECT 'Origin' AS COLUMN_NAME, 'Origin' AS COLUMN_NAME " _
                    & " Union  SELECT 'Qty' AS COLUMN_NAME, 'Qty' AS COLUMN_NAME " _
                    & " Union  SELECT 'Comments' AS COLUMN_NAME, 'Comments' AS COLUMN_NAME ")
            ElseIf TableName = "Approval Log" Then
                'Str = "  " _
                '        & " ApprovalHistory.Description, ApprovalStages.Title , ApprovalHistory.Source"
                dt = UtilityDAL.GetDataTable("SELECT 'Level' AS COLUMN_NAME, 'Level' COLUMN_NAME " _
                    & " Union SELECT 'ReferenceType' AS COLUMN_NAME, 'ReferenceType' AS COLUMN_NAME " _
                    & " Union  SELECT 'DocumentNo' AS COLUMN_NAME, 'DocumentNo' AS COLUMN_NAME " _
                    & " Union  SELECT 'DocumentDate' AS COLUMN_NAME, 'DocumentDate' AS COLUMN_NAME " _
                    & " Union  SELECT 'voucher_type' AS COLUMN_NAME, 'voucher_type' AS COLUMN_NAME " _
                    & " Union  SELECT 'CustomerCode' AS COLUMN_NAME, 'CustomerCode' AS COLUMN_NAME" _
                    & " Union  SELECT 'Qty' AS COLUMN_NAME, 'Qty' AS COLUMN_NAME " _
                    & " Union  SELECT 'Amount' AS COLUMN_NAME, 'Amount' AS COLUMN_NAME " _
                    & " Union  SELECT 'PartyName' AS COLUMN_NAME, 'PartyName' AS COLUMN_NAME " _
                    & " Union  SELECT 'Attachments' AS COLUMN_NAME, 'Attachments' AS COLUMN_NAME " _
                    & " Union  SELECT 'Description' AS COLUMN_NAME, 'Description' AS COLUMN_NAME " _
                    & " Union  SELECT 'Title' AS COLUMN_NAME, 'Title' AS COLUMN_NAME " _
                    & " Union  SELECT 'Source' AS COLUMN_NAME, 'Source' AS COLUMN_NAME ")
            ElseIf TableName = "Purchase Order" Then
                dt = UtilityDAL.GetDataTable("SELECT 'SerialNo' AS COLUMN_NAME, 'SerialNo' AS COLUMN_NAME " _
                    & " Union  SELECT 'VendorName' AS COLUMN_NAME, 'VendorName' AS COLUMN_NAME " _
                    & " Union  SELECT 'ArticleCode' AS COLUMN_NAME, 'ArticleCode' AS COLUMN_NAME " _
                    & " Union  SELECT 'item' AS COLUMN_NAME, 'item' AS COLUMN_NAME " _
                    & " Union  SELECT 'Color' AS COLUMN_NAME, 'Color' AS COLUMN_NAME " _
                    & " Union  SELECT 'Size' AS COLUMN_NAME, 'Size' AS COLUMN_NAME " _
                    & " Union  SELECT 'UOM' AS COLUMN_NAME, 'UOM' AS COLUMN_NAME " _
                    & " Union  SELECT 'unit' AS COLUMN_NAME, 'unit' AS COLUMN_NAME " _
                    & " Union  SELECT 'Qty' AS COLUMN_NAME, 'Qty' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrentPrice' AS COLUMN_NAME, 'CurrentPrice' AS COLUMN_NAME " _
                    & " Union  SELECT 'RateDiscPercent' AS COLUMN_NAME, 'RateDiscPercent' AS COLUMN_NAME " _
                    & " Union  SELECT 'Price' AS COLUMN_NAME, 'Price' AS COLUMN_NAME " _
                    & " Union  SELECT 'BaseCurrencyRate' AS COLUMN_NAME, 'BaseCurrencyRate' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyRate' AS COLUMN_NAME, 'CurrencyRate' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyAmount' AS COLUMN_NAME, 'CurrencyAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyTotalAmount' AS COLUMN_NAME, 'CurrencyTotalAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Total' AS COLUMN_NAME, 'Total' AS COLUMN_NAME " _
                    & " Union  SELECT 'TaxPercent' AS COLUMN_NAME, 'TaxPercent' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyTaxAmount' AS COLUMN_NAME, 'CurrencyTaxAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'AdTax_Percent' AS COLUMN_NAME, 'AdTax_Percent' AS COLUMN_NAME " _
                    & " Union  SELECT 'AdTax_Amount' AS COLUMN_NAME, 'AdTax_Amount' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyAdTaxAmount' AS COLUMN_NAME, 'CurrencyAdTaxAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'TotalAmount' AS COLUMN_NAME, 'TotalAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'PackQty' AS COLUMN_NAME, 'PackQty' AS COLUMN_NAME " _
                    & " Union  SELECT 'PackPrice' AS COLUMN_NAME, 'PackPrice' AS COLUMN_NAME " _
                    & " Union  SELECT 'Pack_Desc' AS COLUMN_NAME, 'Pack_Desc' AS COLUMN_NAME " _
                    & " Union  SELECT 'Comments' AS COLUMN_NAME, 'Comments' AS COLUMN_NAME " _
                    & " Union  SELECT 'TotalQty' AS COLUMN_NAME, 'TotalQty' AS COLUMN_NAME ")
            ElseIf TableName = "Sales Order" Then
                dt = UtilityDAL.GetDataTable("SELECT 'SerialNo' AS COLUMN_NAME, 'SerialNo' AS COLUMN_NAME " _
                    & " Union  SELECT 'ArticleCode' AS COLUMN_NAME, 'ArticleCode' AS COLUMN_NAME " _
                    & " Union  SELECT 'item' AS COLUMN_NAME, 'item' AS COLUMN_NAME " _
                    & " Union  SELECT 'ArticleAliasName' AS COLUMN_NAME, 'ArticleAliasName' AS COLUMN_NAME " _
                    & " Union  SELECT 'Size' AS COLUMN_NAME, 'Size' AS COLUMN_NAME " _
                    & " Union  SELECT 'Color' AS COLUMN_NAME, 'Color' AS COLUMN_NAME " _
                    & " Union  SELECT 'unit' AS COLUMN_NAME, 'unit' AS COLUMN_NAME " _
                    & " Union  SELECT 'Qty' AS COLUMN_NAME, 'Qty' AS COLUMN_NAME " _
                    & " Union  SELECT 'PostDiscountPrice' AS COLUMN_NAME, 'PostDiscountPrice' AS COLUMN_NAME " _
                    & " Union  SELECT 'Price' AS COLUMN_NAME, 'Price' AS COLUMN_NAME " _
                    & " Union  SELECT 'BaseCurrencyRate' AS COLUMN_NAME, 'BaseCurrencyRate' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyRate' AS COLUMN_NAME, 'CurrencyRate' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyAmount' AS COLUMN_NAME, 'CurrencyAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'DiscountFactor' AS COLUMN_NAME, 'DiscountFactor' AS COLUMN_NAME " _
                    & " Union  SELECT 'DiscountValue' AS COLUMN_NAME, 'DiscountValue' AS COLUMN_NAME " _
                    & " Union  SELECT 'TotalAmount' AS COLUMN_NAME, 'TotalAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Discount_Percentage' AS COLUMN_NAME, 'Discount_Percentage' AS COLUMN_NAME " _
                    & " Union  SELECT 'PurchasePrice' AS COLUMN_NAME, 'PurchasePrice' AS COLUMN_NAME " _
                    & " Union  SELECT 'Pack_Desc' AS COLUMN_NAME, 'Pack_Desc' AS COLUMN_NAME " _
                    & " Union  SELECT 'Comments' AS COLUMN_NAME, 'Comments' AS COLUMN_NAME " _
                    & " Union  SELECT 'TotalQuantity' AS COLUMN_NAME, 'TotalQuantity' AS COLUMN_NAME ")
            ElseIf TableName = "Purchase Return" Then
                dt = UtilityDAL.GetDataTable("SELECT 'ArticleCode' AS COLUMN_NAME, 'ArticleCode' AS COLUMN_NAME " _
                    & " Union  SELECT 'item' AS COLUMN_NAME, 'item' AS COLUMN_NAME " _
                    & " Union  SELECT 'BatchNo' AS COLUMN_NAME, 'BatchNo' AS COLUMN_NAME " _
                    & " Union  SELECT 'unit' AS COLUMN_NAME, 'unit' AS COLUMN_NAME " _
                    & " Union  SELECT 'Qty' AS COLUMN_NAME, 'Qty' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrentPrice' AS COLUMN_NAME, 'CurrentPrice' AS COLUMN_NAME " _
                    & " Union  SELECT 'RateDiscPercent' AS COLUMN_NAME, 'RateDiscPercent' AS COLUMN_NAME " _
                    & " Union  SELECT 'Price' AS COLUMN_NAME, 'Price' AS COLUMN_NAME " _
                    & " Union  SELECT 'BaseCurrencyRate' AS COLUMN_NAME, 'BaseCurrencyRate' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyRate' AS COLUMN_NAME, 'CurrencyRate' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyAmount' AS COLUMN_NAME, 'CurrencyAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Total' AS COLUMN_NAME, 'Total' AS COLUMN_NAME " _
                    & " Union  SELECT 'PackPrice' AS COLUMN_NAME, 'PackPrice' AS COLUMN_NAME " _
                    & " Union  SELECT 'Tax_Percent' AS COLUMN_NAME, 'Tax_Percent' AS COLUMN_NAME " _
                    & " Union  SELECT 'TaxAmount' AS COLUMN_NAME, 'TaxAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyTaxAmount' AS COLUMN_NAME, 'CurrencyTaxAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'AdTax_Percent' AS COLUMN_NAME, 'AdTax_Percent' AS COLUMN_NAME " _
                    & " Union  SELECT 'AdTax_Amount' AS COLUMN_NAME, 'AdTax_Amount' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyAdTaxAmount' AS COLUMN_NAME, 'CurrencyAdTaxAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'TotalAmount' AS COLUMN_NAME, 'TotalAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Transportation_Charges' AS COLUMN_NAME, 'Transportation_Charges' AS COLUMN_NAME " _
                    & " Union  SELECT 'Pack_Desc' AS COLUMN_NAME, 'Pack_Desc' AS COLUMN_NAME " _
                    & " Union  SELECT 'Comments' AS COLUMN_NAME, 'Comments' AS COLUMN_NAME " _
                    & " Union  SELECT 'Cost_Price' AS COLUMN_NAME, 'Cost_Price' AS COLUMN_NAME " _
                    & " Union  SELECT 'TotalQty' AS COLUMN_NAME, 'TotalQty' AS COLUMN_NAME ")
            ElseIf TableName = "Sales Return" Then
                dt = UtilityDAL.GetDataTable("SELECT 'ArticleCode' AS COLUMN_NAME, 'ArticleCode' AS COLUMN_NAME " _
                    & " Union  SELECT 'item' AS COLUMN_NAME, 'item' AS COLUMN_NAME " _
                    & " Union  SELECT 'BatchNo' AS COLUMN_NAME, 'BatchNo' AS COLUMN_NAME " _
                    & " Union  SELECT 'unit' AS COLUMN_NAME, 'unit' AS COLUMN_NAME " _
                    & " Union  SELECT 'Qty' AS COLUMN_NAME, 'Qty' AS COLUMN_NAME " _
                    & " Union  SELECT 'Price' AS COLUMN_NAME, 'Price' AS COLUMN_NAME " _
                    & " Union  SELECT 'BaseCurrencyRate' AS COLUMN_NAME, 'BaseCurrencyRate' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyRate' AS COLUMN_NAME, 'CurrencyRate' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyAmount' AS COLUMN_NAME, 'CurrencyAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'TotalCurrencyAmount' AS COLUMN_NAME, 'TotalCurrencyAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Total' AS COLUMN_NAME, 'Total' AS COLUMN_NAME " _
                    & " Union  SELECT 'PackQty' AS COLUMN_NAME, 'PackQty' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrentPrice' AS COLUMN_NAME, 'CurrentPrice' AS COLUMN_NAME " _
                    & " Union  SELECT 'PackPrice' AS COLUMN_NAME, 'PackPrice' AS COLUMN_NAME " _
                    & " Union  SELECT 'Tax_Percent' AS COLUMN_NAME, 'Tax_Percent' AS COLUMN_NAME " _
                    & " Union  SELECT 'TaxAmount' AS COLUMN_NAME, 'TaxAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyTaxAmount' AS COLUMN_NAME, 'CurrencyTaxAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'TotalAmount' AS COLUMN_NAME, 'TotalAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'SampleQty' AS COLUMN_NAME, 'SampleQty' AS COLUMN_NAME " _
                    & " Union  SELECT 'PurchasePrice' AS COLUMN_NAME, 'PurchasePrice' AS COLUMN_NAME " _
                    & " Union  SELECT 'Pack_Desc' AS COLUMN_NAME, 'Pack_Desc' AS COLUMN_NAME " _
                    & " Union  SELECT 'Comments' AS COLUMN_NAME, 'Comments' AS COLUMN_NAME " _
                    & " Union  SELECT 'CostPrice' AS COLUMN_NAME, 'CostPrice' AS COLUMN_NAME " _
                    & " Union  SELECT 'TotalQty' AS COLUMN_NAME, 'TotalQty' AS COLUMN_NAME ")
            ElseIf TableName = "Voucher Entry" Then
                dt = UtilityDAL.GetDataTable("SELECT 'Head' AS COLUMN_NAME, 'Head' AS COLUMN_NAME " _
                    & " Union  SELECT 'Account' AS COLUMN_NAME, 'Account' AS COLUMN_NAME " _
                    & " Union  SELECT 'AccountCode' AS COLUMN_NAME, 'AccountCode' AS COLUMN_NAME " _
                    & " Union  SELECT 'Description' AS COLUMN_NAME, 'Description' AS COLUMN_NAME " _
                    & " Union  SELECT 'Cheque_No' AS COLUMN_NAME, 'Cheque_No' AS COLUMN_NAME " _
                    & " Union  SELECT 'Cheque_Date' AS COLUMN_NAME, 'Cheque_Date' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyDr' AS COLUMN_NAME, 'CurrencyDr' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyCr' AS COLUMN_NAME, 'CurrencyCr' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyRate' AS COLUMN_NAME, 'CurrencyRate' AS COLUMN_NAME " _
                    & " Union  SELECT 'BaseCurrencyRate' AS COLUMN_NAME, 'BaseCurrencyRate' AS COLUMN_NAME " _
                    & " Union  SELECT 'Debit' AS COLUMN_NAME, 'Debit' AS COLUMN_NAME " _
                    & " Union  SELECT 'Credit' AS COLUMN_NAME, 'Credit' AS COLUMN_NAME " _
                    & " Union  SELECT 'Type' AS COLUMN_NAME, 'Type' AS COLUMN_NAME ")
            ElseIf TableName = "Receipt" Then
                dt = UtilityDAL.GetDataTable("SELECT 'detail_title' AS COLUMN_NAME, 'detail_title' AS COLUMN_NAME " _
                    & " Union  SELECT 'detail_code' AS COLUMN_NAME, 'detail_code' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyAmount' AS COLUMN_NAME, 'CurrencyAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyRate' AS COLUMN_NAME, 'CurrencyRate' AS COLUMN_NAME " _
                    & " Union  SELECT 'BaseCurrencyRate' AS COLUMN_NAME, 'BaseCurrencyRate' AS COLUMN_NAME " _
                    & " Union  SELECT 'Amount' AS COLUMN_NAME, 'Amount' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyDiscount' AS COLUMN_NAME, 'CurrencyDiscount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Tax' AS COLUMN_NAME, 'Tax' AS COLUMN_NAME " _
                    & " Union  SELECT 'Tax_Currency_Amount' AS COLUMN_NAME, 'Tax_Currency_Amount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Tax_Amount' AS COLUMN_NAME, 'Tax_Amount' AS COLUMN_NAME " _
                    & " Union  SELECT 'SalesTaxPer' AS COLUMN_NAME, 'SalesTaxPer' AS COLUMN_NAME " _
                    & " Union  SELECT 'SalesTaxAmount' AS COLUMN_NAME, 'SalesTaxAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Sales_Tax' AS COLUMN_NAME, 'Sales_Tax' AS COLUMN_NAME " _
                    & " Union  SELECT 'WHTaxPer' AS COLUMN_NAME, 'WHTaxPer' AS COLUMN_NAME " _
                    & " Union  SELECT 'WHTaxAmount' AS COLUMN_NAME, 'WHTaxAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'WH_Tax' AS COLUMN_NAME, 'WH_Tax' AS COLUMN_NAME " _
                    & " Union  SELECT 'NetAmount' AS COLUMN_NAME, 'NetAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Net_Amount' AS COLUMN_NAME, 'Net_Amount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Reference' AS COLUMN_NAME, 'Reference' AS COLUMN_NAME " _
                    & " Union  SELECT 'Cheque_No' AS COLUMN_NAME, 'Cheque_No' AS COLUMN_NAME " _
                    & " Union  SELECT 'Cheque_Date' AS COLUMN_NAME, 'Cheque_Date' AS COLUMN_NAME " _
                    & " Union  SELECT 'BankDescription' AS COLUMN_NAME, 'Net_Amount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Mobile' AS COLUMN_NAME, 'Reference' AS COLUMN_NAME " _
                    & " Union  SELECT 'Type' AS COLUMN_NAME, 'Cheque_No' AS COLUMN_NAME " _
                    & " Union  SELECT 'InvoiceNo' AS COLUMN_NAME, 'InvoiceNo' AS COLUMN_NAME ")
            ElseIf TableName = "Payment" Then
                dt = UtilityDAL.GetDataTable("SELECT 'detail_title' AS COLUMN_NAME, 'detail_title' AS COLUMN_NAME " _
                    & " Union  SELECT 'detail_code' AS COLUMN_NAME, 'detail_code' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyAmount' AS COLUMN_NAME, 'CurrencyAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyRate' AS COLUMN_NAME, 'CurrencyRate' AS COLUMN_NAME " _
                    & " Union  SELECT 'BaseCurrencyRate' AS COLUMN_NAME, 'BaseCurrencyRate' AS COLUMN_NAME " _
                    & " Union  SELECT 'Amount' AS COLUMN_NAME, 'Amount' AS COLUMN_NAME " _
                    & " Union  SELECT 'CurrencyDiscount' AS COLUMN_NAME, 'CurrencyDiscount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Tax' AS COLUMN_NAME, 'Tax' AS COLUMN_NAME " _
                    & " Union  SELECT 'Tax_Currency_Amount' AS COLUMN_NAME, 'Tax_Currency_Amount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Tax_Amount' AS COLUMN_NAME, 'Tax_Amount' AS COLUMN_NAME " _
                    & " Union  SELECT 'SalesTaxPer' AS COLUMN_NAME, 'SalesTaxPer' AS COLUMN_NAME " _
                    & " Union  SELECT 'SalesTaxAmount' AS COLUMN_NAME, 'SalesTaxAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Sales_Tax' AS COLUMN_NAME, 'Sales_Tax' AS COLUMN_NAME " _
                    & " Union  SELECT 'WHTaxPer' AS COLUMN_NAME, 'WHTaxPer' AS COLUMN_NAME " _
                    & " Union  SELECT 'WHTaxAmount' AS COLUMN_NAME, 'WHTaxAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'WH_Tax' AS COLUMN_NAME, 'WH_Tax' AS COLUMN_NAME " _
                    & " Union  SELECT 'NetAmount' AS COLUMN_NAME, 'NetAmount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Net_Amount' AS COLUMN_NAME, 'Net_Amount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Reference' AS COLUMN_NAME, 'Reference' AS COLUMN_NAME " _
                    & " Union  SELECT 'Cheque_No' AS COLUMN_NAME, 'Cheque_No' AS COLUMN_NAME " _
                    & " Union  SELECT 'Cheque_Date' AS COLUMN_NAME, 'Cheque_Date' AS COLUMN_NAME " _
                    & " Union  SELECT 'BankDescription' AS COLUMN_NAME, 'Net_Amount' AS COLUMN_NAME " _
                    & " Union  SELECT 'Mobile' AS COLUMN_NAME, 'Reference' AS COLUMN_NAME " _
                    & " Union  SELECT 'Type' AS COLUMN_NAME, 'Cheque_No' AS COLUMN_NAME " _
                    & " Union  SELECT 'InvoiceNo' AS COLUMN_NAME, 'InvoiceNo' AS COLUMN_NAME ")
            End If
                Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
