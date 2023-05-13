Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Net
Imports System.Text.RegularExpressions

Public Class PurchaseInquiryDetailDAL
    Public Function Add(ByVal obj As PurchaseInquiryMaster, ByVal trans As SqlTransaction, ByVal GroupId As Integer, ByVal UserId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            For Each objMod As PurchaseInquiryDetail In obj.DetailList
                Dim strSQL As String = String.Empty
                If objMod.SalesInquiryDetailId > 0 Then
                    UpdateSalesInquiryStatusAgainstAddition(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans)
                    UpdateAssigningAdditionStatus(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans, GroupId)
                End If
                strSQL = "INSERT INTO PurchaseInquiryDetail(PurchaseInquiryId, SerialNo, RequirementDescription, ArticleId, UnitId, ItemTypeId, CategoryId, SubCategoryId, OriginId, Qty, ReferenceNo, Comments, SalesInquiryId, SalesInquiryDetailId) " _
                & " VALUES(" & obj.PurchaseInquiryId & ", '" & objMod.SerialNo.Replace("'", "''") & "', '" & objMod.RequirementDescription.Replace("'", "''") & "', " & objMod.ArticleId & ", " & objMod.UnitId & ", " & objMod.ItemTypeId & ", " & objMod.CategoryId & ", " & objMod.SubCategoryId & ", " & objMod.OriginId & ", " & objMod.Qty & " , '" & objMod.ReferenceNo.Replace("'", "''") & "', '" & objMod.Comments.Replace("'", "''") & "', " & objMod.SalesInquiryId & ", " & objMod.SalesInquiryDetailId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                UpdateSecurityStatus(objMod.SalesInquiryDetailId, GroupId, objMod.Qty, trans)
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
    Public Function AddUpdate(ByVal obj As PurchaseInquiryMaster, ByVal trans As SqlTransaction, ByVal GroupId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            For Each objMod As PurchaseInquiryDetail In obj.DetailList
                Dim strSQL As String = String.Empty
                If objMod.SalesInquiryDetailId > 0 AndAlso objMod.PurchaseInquiryDetailId > 0 Then
                    UpdateSalesInquiryStatusAgainstSubtraction(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans)
                    UpdateSalesInquiryStatusAgainstAddition(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans)
                    UpdateAssigningAdditionStatus(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans, GroupId)
                    UpdateAssigningSubtractionStatus(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans, GroupId)
                ElseIf objMod.SalesInquiryDetailId > 0 AndAlso objMod.PurchaseInquiryDetailId = 0 Then
                    UpdateSalesInquiryStatusAgainstAddition(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans)
                    UpdateAssigningAdditionStatus(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans, GroupId)
                End If
                strSQL = "INSERT INTO PurchaseInquiryDetail(PurchaseInquiryId, SerialNo, RequirementDescription, ArticleId, UnitId, ItemTypeId, CategoryId, SubCategoryId, OriginId, Qty, ReferenceNo, Comments, SalesInquiryId, SalesInquiryDetailId) " _
                & " VALUES(" & obj.PurchaseInquiryId & ", '" & objMod.SerialNo.Replace("'", "''") & "', '" & objMod.RequirementDescription.Replace("'", "''") & "', " & objMod.ArticleId & ", " & objMod.UnitId & ", " & objMod.ItemTypeId & ", " & objMod.CategoryId & ", " & objMod.SubCategoryId & ", " & objMod.OriginId & ", " & objMod.Qty & " , '" & objMod.ReferenceNo.Replace("'", "''") & "', '" & objMod.Comments.Replace("'", "''") & "', " & objMod.SalesInquiryId & ", " & objMod.SalesInquiryDetailId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                UpdateSecurityStatus(objMod.SalesInquiryDetailId, GroupId, objMod.Qty, trans)
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

    Public Function Update(ByVal objModList As List(Of PurchaseInquiryDetail), ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction

        Try
            For Each objMod As PurchaseInquiryDetail In objModList
                'PurchaseInquiryId, RequirementDescription, ArticleId, UnitId, ItemTypeId, CategoryId, SubCategoryId, OriginId, Qty, ReferenceNo, Comments
                Dim strSQL As String = String.Empty
                strSQL = "Update PurchaseInquiryDetail  SET PurchaseInquiryId= " & objMod.PurchaseInquiryId & ", SerialNo = '" & objMod.SerialNo.Replace("'", "''") & "', RequirementDescription='" & objMod.RequirementDescription.Replace("'", "''") & "', ArticleId= " & objMod.ArticleId & ", UnitId=" & objMod.UnitId & ", ItemTypeId= " & objMod.ItemTypeId & " , CategoryId= " & objMod.CategoryId & ", SubCategoryId= " & objMod.SubCategoryId & ", OriginId= " & objMod.OriginId & ", Qty=" & objMod.Qty & ",  ReferenceNo='" & objMod.ReferenceNo.Replace("'", "''") & "', Comments='" & objMod.Comments.Replace("'", "''") & "', SalesInquiryId= " & objMod.SalesInquiryId & ",  SalesInquiryDetailId= " & objMod.SalesInquiryDetailId & " WHERE PurchaseInquiryDetailId=" & objMod.PurchaseInquiryDetailId & ""
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
            strSQL = "Delete From PurchaseInquiryDetail WHERE PurchaseInquiryDetailId=" & ID & ""
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
    Public Function DeleteSIIWise(ByVal PurchaseInquiryId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From PurchaseInquiryDetail WHERE PurchaseInquiryId=" & PurchaseInquiryId & ""
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
    Public Function GetSingle(ByVal PurchaseInquiryId As Integer) As DataTable
        Try
            'PurchaseInquiryId, RequirementDescription, ArticleId, UnitId, ItemTypeId, CategoryId, SubCategoryId, OriginId, Qty, ReferenceNo, Comments
            Dim dt As New DataTable
            ''Commented Against TFS3696 : Ayesha Rehman : 28-06-2018
            ' dt = UtilityDAL.GetDataTable("SELECT Detail.PurchaseInquiryDetailId,  Detail.PurchaseInquiryId, Detail.SerialNo, Detail.RequirementDescription,  IsNull(Detail.ArticleId, 0) As ArticleId, Article.ArticleCode As Code, Article.ArticleDescription,  Detail.UnitId, Unit.ArticleUnitName As Unit,  Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, IsNull(Detail.Qty, 0) As Qty, Detail.ReferenceNo, Detail.Comments, IsNull(Detail.SalesInquiryId, 0) As SalesInquiryId, IsNull(Detail.SalesInquiryDetailId, 0) As SalesInquiryDetailId FROM PurchaseInquiryDetail As Detail  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where PurchaseInquiryId =" & PurchaseInquiryId & "Order by Detail.SerialNo")
            dt = UtilityDAL.GetDataTable("SELECT Detail.PurchaseInquiryDetailId,  Detail.PurchaseInquiryId, Detail.SerialNo, Detail.RequirementDescription,  IsNull(Detail.ArticleId, 0) As ArticleId, Article.ArticleCode As Code, Article.ArticleDescription,  Detail.UnitId, Unit.ArticleUnitName As Unit,  Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, IsNull(Detail.Qty, 0) As Qty, Detail.ReferenceNo, Detail.Comments, IsNull(Detail.SalesInquiryId, 0) As SalesInquiryId, IsNull(Detail.SalesInquiryDetailId, 0) As SalesInquiryDetailId FROM PurchaseInquiryDetail As Detail  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where PurchaseInquiryId =" & PurchaseInquiryId & "Order by CAST(Detail.SerialNo AS Numeric(10,0)) Asc ")

            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSingleArticle(ByVal QuotationDetailId As Integer) As DataTable
        Try
            'PurchaseInquiryId, RequirementDescription, ArticleId, UnitId, ItemTypeId, CategoryId, SubCategoryId, OriginId, Qty, ReferenceNo, Comments
            Dim dt As New DataTable
            'dt = UtilityDAL.GetDataTable("SELECT Detail.PurchaseInquiryDetailId,  Detail.PurchaseInquiryId, Detail.RequirementDescription,  Detail.UnitId, Unit.ArticleUnitName As Unit,  Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, IsNull(Detail.SalesInquiryDetailId, 0)-IsNull(Detail.PurchaseInquiryDetailId, 0)-Detail.ReferenceNo As Code FROM PurchaseInquiryDetail As Detail INNER JOIN QuotationDetailTable ON Detail.PurchaseInquiryDetailId = QuotationDetailTable.PurchaseInquiryDetailId  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where QuotationDetailTable.QuotationDetailId =" & QuotationDetailId & "")
            'dt = UtilityDAL.GetDataTable("SELECT QuotationDetailTable.QuotationDetailId, Detail.PurchaseInquiryDetailId, Detail.RequirementDescription,  Detail.UnitId, Unit.ArticleUnitName As Unit,  Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, Detail.SerialNo + '-' + IsNull(Detail.PurchaseInquiryDetailId, 0) + '-' + Detail.ReferenceNo As Code FROM VendorQuotationDetail As Detail INNER JOIN QuotationDetailTable ON  Case When Detail.PurchaseInquiryDetailId Is Null Then Detail.HeadArticleId Else Detail.PurchaseInquiryDetailId End = Case When QuotationDetailTable.PurchaseInquiryDetailId Is Null Then QuotationDetailTable.HeadArticleId Else QuotationDetailTable.PurchaseInquiryDetailId  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where QuotationDetailTable.QuotationDetailId =" & QuotationDetailId & "")
            dt = UtilityDAL.GetDataTable("SELECT QuotationDetailTable.QuotationDetailId, Detail.PurchaseInquiryDetailId, Detail.RequirementDescription,  Detail.UnitId, Unit.ArticleUnitName As Unit,  Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, Convert(nvarchar(50), QuotationDetailTable.QuotationDetailId, 113) + '-' + Detail.SerialNo + '-' + Detail.ReferenceNo As Code FROM VendorQuotationDetail As Detail INNER JOIN QuotationDetailTable ON Detail.VendorQuotationDetailId = QuotationDetailTable.VendorQuotationDetailId  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where QuotationDetailTable.QuotationDetailId =" & QuotationDetailId & "")
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateSalesInquiryStatusAgainstAddition(ByVal SalesInquiryId As Integer, ByVal SalesInquiryDetailId As Integer, ByVal Qty As Double, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update SalesInquiryDetail Set PurchasedQty = IsNull(PurchasedQty, 0)+" & Qty & " WHERE SalesInquiryDetailId=" & SalesInquiryDetailId & ""
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
    Public Function UpdateAssigningAdditionStatus(ByVal SalesInquiryId As Integer, ByVal SalesInquiryDetailId As Integer, ByVal Qty As Double, ByVal trans As SqlTransaction, ByVal GroupId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            'strSQL = "Update SalesInquiryRights Set PurchasedQty = IsNull(PurchasedQty, 0)+" & Qty & " WHERE SalesInquiryDetailId=" & SalesInquiryDetailId & " And SalesInquiryId=" & SalesInquiryId & " And GroupId = " & GroupId & ""
            strSQL = "Update SalesInquiryRights Set PurchasedQty = IsNull(PurchasedQty, 0)+" & Qty & " WHERE SalesInquiryDetailId=" & SalesInquiryDetailId & " And SalesInquiryId=" & SalesInquiryId & ""
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
    Public Function UpdateSalesInquiryStatusAgainstSubtraction(ByVal SalesInquiryId As Integer, ByVal SalesInquiryDetailId As Integer, ByVal Qty As Double, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update SalesInquiryDetail Set PurchasedQty = IsNull(PurchasedQty, 0)-" & Qty & " WHERE SalesInquiryDetailId=" & SalesInquiryDetailId & ""
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
    Public Function UpdateAssigningSubtractionStatus(ByVal SalesInquiryId As Integer, ByVal SalesInquiryDetailId As Integer, ByVal Qty As Double, ByVal trans As SqlTransaction, ByVal GroupId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            'strSQL = "Update SalesInquiryRights Set PurchasedQty = IsNull(PurchasedQty, 0)-" & Qty & " WHERE SalesInquiryDetailId=" & SalesInquiryDetailId & " And SalesInquiryId=" & SalesInquiryId & " And GroupId = " & GroupId & ""
            strSQL = "Update SalesInquiryRights Set PurchasedQty = IsNull(PurchasedQty, 0)-" & Qty & " WHERE SalesInquiryDetailId=" & SalesInquiryDetailId & " And SalesInquiryId=" & SalesInquiryId & ""
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
    Public Function UpdateSalesInquiryStatusForOneDelete(ByVal SalesInquiryId As Integer, ByVal SalesInquiryDetailId As Integer, ByVal Qty As Double, ByVal PurchaseInquiryDetailId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "If Exists(Select PurchaseInquiryDetailId From PurchaseInquiryDetail Where PurchaseInquiryDetailId =" & PurchaseInquiryDetailId & " And SalesInquiryDetailId=" & SalesInquiryDetailId & " ) Update SalesInquiryDetail Set PurchasedQty = IsNull(PurchasedQty, 0)-" & Qty & " WHERE SalesInquiryDetailId=" & SalesInquiryDetailId & ""
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
    Public Function UpdateSecurityStatus(ByVal SalesInquiryDetailId As Integer, ByVal GroupId As Integer, ByVal Qty As Double, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = " If exists(Select SalesInquiryDetailId FROM SalesInquiryRights Where SalesInquiryRights.PurchasedQty >= SalesInquiryRights.Qty And SalesInquiryRights.SalesInquiryDetailId =" & SalesInquiryDetailId & " And SalesInquiryRights.GroupId =" & GroupId & ") Update SalesInquiryRights Set Status = 'Close' WHERE SalesInquiryDetailId=" & SalesInquiryDetailId & " And GroupId = " & GroupId & " " _
                   & " Else Update SalesInquiryRights Set Status = 'Open' WHERE SalesInquiryDetailId=" & SalesInquiryDetailId & " And GroupId = " & GroupId & ""
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

End Class
