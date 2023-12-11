Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class AddArticleDAL
    Public Function Add(ByVal objModel As Article) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()

        Try

            'insert into accounts
            'objModel.COADetail.COADetailID = New COADetailDAL().Add(objModel.COADetail, objModel.AccountID, trans)

            'insert master
            Dim strSQL As String = "insert into ArticleDefTableMaster(ArticleCode,ArticleDescription,ArticleGroupId," & _
            " ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
            " StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID, Remarks, ArticlePicture, LargestPackQty)" & _
            " values(N'" & objModel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objModel.ArticleDescription.Trim.Replace("'", "''") & "'," & _
            objModel.ArticleGroupID & "," & objModel.ArticleTypeID & "," & objModel.ArticleGenderID & "," & objModel.ArticleUnitID & "," & objModel.ArticleLPOID & "," & _
            objModel.PurchasePrice & "," & objModel.SalePrice & "," & objModel.PackQty & "," & objModel.StockLevel & "," & _
            objModel.StockLevelOpt & "," & objModel.StockLevelMax & "," & IIf(objModel.Active = True, 1, 0) & "," & objModel.SortOrder & ",N'" & _
            objModel.IsDate & "'," & objModel.COADetail.COADetailID & ",N'" & objModel.ArticleRemarks.ToString & "', N'" & objModel.ArticlePicture & "', " & objModel.LargestPackQty & ") Select @@Identity"
            objModel.ArticleID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL))
            ''add deltail
            If Me.AddDetail(objModel.ArticleID, objModel, trans, objModel.COADetail.COADetailID) Then
                'add increment reduction
                ''add activity log
                objModel.ActivityLog.ApplicationName = "Config"
                objModel.ActivityLog.FormCaption = "Article Definition"
                objModel.ActivityLog.RefNo = objModel.ArticleCode
                objModel.ActivityLog.LogDateTime = Date.Today
                objModel.ActivityLog.ActivityName = "Save"
                objModel.ActivityLog.RecordType = "Configuration"
                UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
                trans.Commit()
                Return True
            End If

        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try

    End Function
    Private Function AddDetail(ByVal MasterID As Integer, ByVal objArticle As Article, ByVal trans As SqlTransaction, ByVal COADetailID As Integer) As Boolean
        Try

            Dim strSQL As String = String.Empty
            Dim objmodels As List(Of ArticleDetail) = objArticle.ArticleDetails

            If objmodels Is Nothing Then Return False
            If objmodels.Count = 0 Then Return False
            Dim obj As Object = Nothing
            For Each objmodel As ArticleDetail In objmodels
                strSQL = "insert into ArticleDefTable(ArticleCode,ArticleDescription,ArticleGroupId," & _
                                       " ArticleTypeId,ArticleGenderId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,StockLevel," & _
                                       " StockLevelOpt,StockLevelMax,Active,SortOrder,IsDate, AccountID,SizeRangeId,ArticleColorId, MasterID, Remarks, LargestPackQty)" & _
                                       " values(N'" & objmodel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objmodel.ArticleDescription.Trim.Replace("'", "''") & "'," & _
                                       objmodel.ArticleGroupID & "," & objmodel.ArticleTypeID & "," & objmodel.ArticleGenderID & "," & objmodel.ArticleUnitID & "," & objmodel.ArticleLPOID & "," & _
                                       objmodel.PurchasePrice & "," & objmodel.SalePrice & "," & objmodel.PackQty & "," & objmodel.StockLevel & "," & _
                                       objmodel.StockLevelOpt & "," & objmodel.StockLevelMax & "," & IIf(objmodel.Active = True, 1, 0) & "," & objmodel.SortOrder & ",N'" & _
                                       objmodel.IsDate & "'," & COADetailID & "," & objmodel.SizeRangeID & "," & objmodel.ArticleColorID & "," & MasterID & ",'', " & objmodel.LargestPackQty & ")Select @@Identity "
                obj = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Next



            Dim objALRList As List(Of ArticalLocationRank) = objArticle.ArticalLocationRank
            For Each objALR As ArticalLocationRank In objALRList
                strSQL = "Insert Into ArticalDefLocation (ArticalID,LocationID,Ranks) Values (" & MasterID & "," & objALR.LocationID & ",N'" & objALR.Rank & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next

            objArticle.IncrementReduction = New IncrementReduction
            objArticle.IncrementReduction.IncrementDate = Date.Now.Date
            objArticle.IncrementReduction.PurchasePriceNew = objArticle.PurchasePrice
            objArticle.IncrementReduction.PurchasePriceOld = 0
            objArticle.IncrementReduction.SalePriceNew = objArticle.SalePrice
            objArticle.IncrementReduction.SalePriceOld = 0
            objArticle.IncrementReduction.Stock = 0
            objArticle.IncrementReduction.ArticleID = obj
            Call New IncrementReductionDAL().Add(objArticle.IncrementReduction, trans)

            Return True
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
