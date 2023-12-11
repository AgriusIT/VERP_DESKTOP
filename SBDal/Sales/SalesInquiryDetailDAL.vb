Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Net
Public Class SalesInquiryDetailDAL
    Public Function Add(ByVal obj As SalesInquiryMaster, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        '        SalesInquiryDetailId	int	Unchecked
        'SalesInquiryId	int	Checked
        'SerialNo	nvarchar(100)	Checked
        'RequirementDescription	nvarchar(500)	Checked
        'ArticleId	int	Checked
        'UnitId	int	Checked
        'ItemTypeId	int	Checked
        'CategoryId	int	Checked
        'SubCategoryId	int	Checked
        'OriginId	int	Checked
        'Qty	float	Checked
        'Comments	nvarchar(500)	Checked
        '        Unchecked()
        Try

            For Each objMod As SalesInquiryDetail In obj.DetailList
                Dim strSQL As String = String.Empty
                strSQL = "INSERT INTO SalesInquiryDetail(SalesInquiryId, SerialNo, RequirementDescription, InternalDescription, InternalDescriptionChanged, ArticleId, UnitId, ItemTypeId, CategoryId, SubCategoryId, OriginId, Qty, Comments) " _
                & " VALUES(" & obj.SalesInquiryId & ", '" & objMod.SerialNo.Replace("'", "''") & "', '" & objMod.RequirementDescription.Replace("'", "''") & "', '" & objMod.RequirementDescription.Replace("'", "''") & "', 0, " & objMod.ArticleId & ", " & objMod.UnitId & ", " & objMod.ItemTypeId & ", " & objMod.CategoryId & ", " & objMod.SubCategoryId & ", " & objMod.OriginId & ", " & objMod.Qty & ", '" & objMod.Comments.Replace("'", "''") & "')"
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

    Public Function Update(ByVal objModList As List(Of SalesInquiryDetail), ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction

        Try
            For Each objMod As SalesInquiryDetail In objModList
                'SalesInquiryId, SerialNo, RequirementDescription, ArticleId, UnitId, ItemTypeId, CategoryId, SubCategoryId, OriginId, Qty, Comments
                Dim strSQL As String = String.Empty
                'strSQL = "Update SalesInquiryDetail  SET SalesInquiryId= " & objMod.SalesInquiryId & ", SerialNo='" & objMod.SerialNo.Replace("'", "''") & "', RequirementDescription='" & objMod.RequirementDescription.Replace("'", "''") & "', InternalDescription= Case When InternalDescriptionChanged= 0 Then '" & objMod.RequirementDescription.Replace("'", "''") & "' Else InternalDescription End , ArticleId= " & objMod.ArticleId & ", UnitId=" & objMod.UnitId & ", ItemTypeId= " & objMod.ItemTypeId & " , CategoryId= " & objMod.CategoryId & ", SubCategoryId= " & objMod.SubCategoryId & ", OriginId= " & objMod.OriginId & ", Qty=" & objMod.Qty & ", Comments='" & objMod.Comments & "' WHERE SalesInquiryDetailId=" & objMod.SalesInquiryDetailId & ""
                'Ali Faisal : TFS1455 : Altered to Update if exists or insert into sales inquiry detail records
                strSQL = "If Exists (Select SalesInquiryDetailId From SalesInquiryDetail WHERE SalesInquiryDetailId= " & objMod.SalesInquiryDetailId & ") "
                strSQL += " Update SalesInquiryDetail  SET SalesInquiryId= " & objMod.SalesInquiryId & ", SerialNo='" & objMod.SerialNo.Replace("'", "''") & "', RequirementDescription='" & objMod.RequirementDescription.Replace("'", "''") & "', InternalDescription= Case When InternalDescriptionChanged= 0 Then '" & objMod.RequirementDescription.Replace("'", "''") & "' Else InternalDescription End , ArticleId= " & objMod.ArticleId & ", UnitId=" & objMod.UnitId & ", ItemTypeId= " & objMod.ItemTypeId & " , CategoryId= " & objMod.CategoryId & ", SubCategoryId= " & objMod.SubCategoryId & ", OriginId= " & objMod.OriginId & ", Qty=" & objMod.Qty & ", Comments='" & objMod.Comments & "' WHERE SalesInquiryDetailId=" & objMod.SalesInquiryDetailId & ""
                strSQL += " Else "
                strSQL += " INSERT INTO SalesInquiryDetail(SalesInquiryId, SerialNo, RequirementDescription, InternalDescription, InternalDescriptionChanged, ArticleId, UnitId, ItemTypeId, CategoryId, SubCategoryId, OriginId, Qty, Comments) " _
                & " VALUES(" & objMod.SalesInquiryId & ", '" & objMod.SerialNo.Replace("'", "''") & "', '" & objMod.RequirementDescription.Replace("'", "''") & "', '" & objMod.RequirementDescription.Replace("'", "''") & "', 0, " & objMod.ArticleId & ", " & objMod.UnitId & ", " & objMod.ItemTypeId & ", " & objMod.CategoryId & ", " & objMod.SubCategoryId & ", " & objMod.OriginId & ", " & objMod.Qty & ", '" & objMod.Comments.Replace("'", "''") & "')"
                'Ali Faisal : TFS1455 : End
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
            strSQL = "Delete From SalesInquiryDetail WHERE SalesInquiryDetailId=" & ID & ""
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

    Public Function DeleteSIIWise(ByVal SalesInquiryId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From SalesInquiryDetail WHERE SalesInquiryId=" & SalesInquiryId & ""
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

    Public Function GetSingle(ByVal SalesInquiryId As Integer) As DataTable
        Try
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT Detail.SalesInquiryDetailId,  Detail.SalesInquiryId,  Convert(Int, Detail.SerialNo) As SerialNo,  Detail.RequirementDescription,  Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription,  Detail.UnitId, Unit.ArticleUnitName As Unit,  Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, IsNull( Detail.Qty, 0) As Qty,  Detail.Comments FROM SalesInquiryDetail As Detail  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where SalesInquiryId =" & SalesInquiryId & "Order By Detail.SerialNo ")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAgainstSearch(ByVal GroupId As Integer, ByVal CustomerId As Integer, ByVal SalesInquiryId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsAdmin As Boolean) As DataTable
        Try

            Dim dt As New DataTable
            'Dim strSQL As String = "SELECT Detail.SalesInquiryDetailId, Detail.SalesInquiryId, Detail.SerialNo, Detail.RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, (IsNull( Detail.Qty, 0)-IsNull( Detail.PurchasedQty, 0)) As Qty,  Detail.Comments FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId " & IIf(IsAdmin = True, "", "INNER JOIN SalesInquiryRights ON Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId") & "  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where IsNull(Detail.Qty, 0)>IsNull( Detail.PurchasedQty, 0) " & IIf(IsAdmin = True, "", " And SalesInquiryRights.GroupId = " & GroupId & " And SalesInquiryRights.Rights=1") & ""
            Dim strSQL As String = "SELECT Detail.SalesInquiryDetailId, Detail.SalesInquiryId, Detail.SerialNo, Detail.RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, (IsNull(SalesInquiryRights.Qty, 0)-IsNull(SalesInquiryRights.PurchasedQty, 0)) As Qty,  Detail.Comments FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId INNER JOIN SalesInquiryRights ON Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where IsNull(SalesInquiryRights.Qty, 0) > IsNull(SalesInquiryRights.PurchasedQty, 0)  And SalesInquiryRights.GroupId = " & GroupId & " And SalesInquiryRights.Rights=1 And SalesInquiryRights.Status='Open'"
            If CustomerId > 0 Then
                strSQL += " And SalesInquiryMaster.CustomerId = " & CustomerId & ""
            End If
            If SalesInquiryId > 0 Then
                strSQL += " And SalesInquiryMaster.SalesInquiryId = " & SalesInquiryId & ""
            End If
            If Not FromDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate >= Convert(DateTime, '" & FromDate.ToString("yyyy-M-dd 00:00:00") & "', 102)"
            End If
            If Not ToDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate <= Convert(DateTime, '" & ToDate.ToString("yyyy-M-dd 23:59:59") & "', 102)"
            End If
            ''Start OTC Req : Ayesha Rehman : 28-06-2018
            strSQL += " Order by CAST(Detail.SerialNo AS Numeric(10,0)) Asc "
            ''End 
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAgainstSearch1(ByVal UserId As Integer, ByVal CustomerId As Integer, ByVal SalesInquiryId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsAdmin As Boolean) As DataTable
        Try

            Dim dt As New DataTable
            'Dim strSQL As String = "SELECT Detail.SalesInquiryDetailId, Detail.SalesInquiryId, Detail.SerialNo, Detail.RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, (IsNull( Detail.Qty, 0)-IsNull( Detail.PurchasedQty, 0)) As Qty,  Detail.Comments FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId " & IIf(IsAdmin = True, "", "INNER JOIN SalesInquiryRights ON Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId") & "  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where IsNull(Detail.Qty, 0)>IsNull( Detail.PurchasedQty, 0) " & IIf(IsAdmin = True, "", " And SalesInquiryRights.GroupId = " & GroupId & " And SalesInquiryRights.Rights=1") & ""
            'Dim strSQL As String = "SELECT Detail.SalesInquiryDetailId, Detail.SalesInquiryId, Detail.SerialNo, SalesInquiryRights.RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, (IsNull(SalesInquiryRights.Qty, 0)-IsNull(SalesInquiryRights.PurchasedQty, 0)) As Qty,  Detail.Comments FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId INNER JOIN SalesInquiryRights ON Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId Inner Join SalesInquiryRightsUsers ON SalesInquiryRights.SalesInquiryRightsId = SalesInquiryRightsUsers.SalesInquiryRightsId LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where IsNull(SalesInquiryRights.Qty, 0) > IsNull(SalesInquiryRights.PurchasedQty, 0)  And SalesInquiryRightsUsers.UserId = " & UserId & " And SalesInquiryRights.Rights=1 And SalesInquiryRights.Status='Open'"
            'Dim strSQL As String = "SELECT Detail.SalesInquiryDetailId, Detail.SalesInquiryId, Detail.SerialNo, Detail.InternalDescription as RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, (IsNull(SalesInquiryRights.Qty, 0)-IsNull(SalesInquiryRights.PurchasedQty, 0)) As Qty,  Detail.Comments FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId INNER JOIN SalesInquiryRights ON Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId Inner Join SalesInquiryRightsUsers ON SalesInquiryRights.SalesInquiryRightsId = SalesInquiryRightsUsers.SalesInquiryRightsId LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where SalesInquiryRightsUsers.UserId = " & UserId & " And SalesInquiryRights.Rights=1"
            'Changes for OTC because SalesInquiry not load at Purchase Inquiry by Ali
            Dim strSQL As String = "SELECT Detail.SalesInquiryDetailId, Detail.SalesInquiryId, Detail.SerialNo, Detail.InternalDescription as RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, Detail.Qty As Qty,  Detail.Comments FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId INNER JOIN SalesInquiryRights ON Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId Inner Join SalesInquiryRightsUsers ON SalesInquiryRights.SalesInquiryRightsId = SalesInquiryRightsUsers.SalesInquiryRightsId LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where SalesInquiryRights.Rights=1"
            If CustomerId > 0 Then
                strSQL += " And SalesInquiryMaster.CustomerId = " & CustomerId & ""
            End If
            If SalesInquiryId > 0 Then
                strSQL += " And SalesInquiryMaster.SalesInquiryId = " & SalesInquiryId & ""
            End If
            If Not FromDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate >= Convert(DateTime, '" & FromDate.ToString("yyyy-M-dd 00:00:00") & "', 102)"
            End If
            If Not ToDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate <= Convert(DateTime, '" & ToDate.ToString("yyyy-M-dd 23:59:59") & "', 102)"
            End If
            ''Start OTC Req : Ayesha Rehman : 28-06-2018
            strSQL += " Order by CAST(Detail.SerialNo AS Numeric(10,0)) Asc "
            ''End 
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSalesInquiryStatus(ByVal CustomerId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, Optional ByVal Status As String = "") As DataTable
        Try
            'Dim strSQL As String = " SELECT Detail.SalesInquiryDetailId,  Detail.SalesInquiryId, " _
            '                       & " Detail.SerialNo,  Detail.RequirementDescription,  Detail.ArticleId, " _
            '                       & " Article.ArticleCode As Code, Article.ArticleDescription,  Detail.UnitId, " _
            '                       & " Unit.ArticleUnitName As Unit,  Detail.ItemTypeId, Type.ArticleTypeName As Type, " _
            '                       & " Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, " _
            '                       & " SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, " _
            '                       & " Origin.ArticleGenderName As Origin, IsNull(Detail.Qty, 0) As Qty, " _
            '                       & " ISNULL(Detail.PurchasedQty, 0) As PurchasedQty, (ISNULL(Detail.Qty, 0)-ISNULL(Detail.PurchasedQty, 0)) As Remaining, " _
            '                       & " IsNull(VendorQuotationDetail.Qty, 0) As VendorQuotationQty, Detail.Comments " _
            '                       & " FROM SalesInquiryDetail As Detail Inner Join SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId LEFT OUTER JOIN " _
            '                       & " PurchaseInquiryDetail ON Detail.SalesInquiryDetailId = PurchaseInquiryDetail.SalesInquiryDetailId  LEFT OUTER JOIN " _
            '                       & " VendorQuotationDetail ON PurchaseInquiryDetail.PurchaseInquiryDetailId = VendorQuotationDetail.PurchaseInquiryDetailId LEFT OUTER JOIN  " _
            '                       & " ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT OUTER JOIN " _
            '                       & " ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT OUTER JOIN " _
            '                       & " ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT OUTER JOIN " _
            '                       & " ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT OUTER JOIN " _
            '                       & " ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT OUTER JOIN " _
            '                       & " ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId "
            Dim strSQL As String = " SELECT Detail.SalesInquiryDetailId,  Detail.SalesInquiryId, " _
                                  & " Detail.SerialNo,  Detail.RequirementDescription,  Detail.ArticleId, " _
                                  & " Article.ArticleCode As Code, Article.ArticleDescription,  Detail.UnitId, " _
                                  & " Unit.ArticleUnitName As Unit,  Detail.ItemTypeId, Type.ArticleTypeName As Type, " _
                                  & " FROM SalesInquiryMaster As Detail Inner Join SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId LEFT OUTER JOIN " _
                                  & " PurchaseInquiryDetail ON Detail.SalesInquiryDetailId = PurchaseInquiryDetail.SalesInquiryDetailId  LEFT OUTER JOIN " _
                                  & " VendorQuotationDetail ON PurchaseInquiryDetail.PurchaseInquiryDetailId = VendorQuotationDetail.PurchaseInquiryDetailId LEFT OUTER JOIN  " _
                                  & " ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT OUTER JOIN " _
                                  & " ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT OUTER JOIN " _
                                  & " ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT OUTER JOIN " _
                                  & " ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT OUTER JOIN " _
                                  & " ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT OUTER JOIN " _
                                  & " ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId "
            '             SalesInquiryId	int	Unchecked
            'SalesInquiryNo	nvarchar(100)	Checked
            'SalesInquiryDate	datetime	Checked
            'CustomerId	int	Checked
            'LocationId	int	Checked
            'ContactPersonId	int	Checked
            'CustomerInquiryNo	nvarchar(50)	Checked
            'IndentNo	nvarchar(50)	Checked
            'IndentDepartment	nvarchar(50)	Checked
            'CustomerInquiryDate	datetime	Checked
            'OldInquiryNo	nvarchar(100)	Checked
            'DueDate	datetime	Checked
            'OldInquiryDate	datetime	Checked
            'Remarks	nvarchar(500)	Checked
            '            Unchecked()
            Dim dt As New DataTable
            'Dim strSQL As String = "SELECT Detail.SalesInquiryDetailId, Detail.SalesInquiryId, Detail.SerialNo, Detail.RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, (IsNull( Detail.Qty, 0)-IsNull( Detail.PurchasedQty, 0)) As Qty,  Detail.Comments FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId " & IIf(IsAdmin = True, "", "INNER JOIN SalesInquiryRights ON Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId") & "  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where IsNull(Detail.Qty, 0)>IsNull( Detail.PurchasedQty, 0) " & IIf(IsAdmin = True, "", " And SalesInquiryRights.GroupId = " & GroupId & " And SalesInquiryRights.Rights=1") & ""
            If CustomerId > 0 Then
                strSQL += " And SalesInquiryMaster.CustomerId = " & CustomerId & ""
            End If
            If Not FromDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate >= Convert(DateTime, '" & FromDate.ToString("yyyy-M-dd 00:00:00") & "', 102)"
            End If
            If Not ToDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate <= Convert(DateTime, '" & ToDate.ToString("yyyy-M-dd 23:59:59") & "', 102)"
            End If
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS3960 : Sales Inquiry item delete from grid does not removed from Database.
    ''' </summary>
    ''' <param name="SalesInquiryDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteSalesInquiryDetail(ByVal SalesInquiryDetailId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From SalesInquiryDetail WHERE SalesInquiryDetailId=" & SalesInquiryDetailId & ""
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
End Class
