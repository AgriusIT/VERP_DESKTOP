Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Public Class CustomArticleDAL
    Public Shared ArticleMasterId As Integer
    Public Shared ItemDiscountId As Integer
    Public Function Add(ByVal objModel As Article) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Dim strSQL As String = "insert into ArticleDefTableMaster(ArticleCode,ArticleDescription,ArticleBARCode,ArticleGroupId,ArticleGenderId," & _
                               " ArticleTypeId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty,ArticleCategoryId,Active,ArticleBARCodeDisable,AutoCode)" & _
                               " values(N'" & objModel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objModel.ArticleDescription.Trim.Replace("'", "''") & "',N'" & objModel.ArticleBARCode.Trim.Replace("'", "''") & "'," & _
                               objModel.ArticleGroupID & "," & objModel.ArticleGenderID & "," & objModel.ArticleTypeID & "," & objModel.ArticleUnitID & "," & objModel.ArticleLPOID & "," & _
                               objModel.PurchasePrice & "," & objModel.SalePrice & "," & objModel.PackQty & "," & _
                               objModel.ArticleCategoryId & "," & 1 & ", " & IIf(objModel.ArticleBARCodeDisable = True, 1, 0) & ",'1') Select @@Identity"


            objModel.ArticleID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL))

            strSQL = "insert into  ItemWiseDiscountMaster (FromDate, ToDate, DiscountType, Discount, RepeatingNextYear) values (N'" & Date.Now.ToString("yyyy-M-d h:mm:ss tt") & "', N'2022-08-10 23:59:59', " & 1 & ", " & objModel.DiscountFactor & ", " & 0 & ") Select @@Identity"
            ItemDiscountId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL))

            ArticleMasterId = objModel.ArticleID
            strSQL = String.Empty
            strSQL = "INSERT INTO ArticleDefPackTable(ArticleMasterId, PackName, PackQty) Values(" & ArticleMasterId & ", 'Loose', 1)"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "INSERT INTO ArticleDefPackTable(ArticleMasterId, PackName, PackQty) Values(" & ArticleMasterId & ", 'Pack', " & objModel.PackQty & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''
            If objModel.ArticleBARCodeDisable = False Then
                If objModel.DefaultBarCodeSourceValue = "Product ID" Then
                    strSQL = " Update ArticleDefTableMaster Set ArticleBARCode = '" & objModel.ArticleID.ToString & "' WHERE ArticleId = " & objModel.ArticleID & ""

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                Else
                    strSQL = " Update ArticleDefTableMaster Set ArticleBARCode = N'" & objModel.ArticleCode.Trim.Replace("'", "''") & "' WHERE ArticleId = " & objModel.ArticleID & ""

                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                End If
            End If

            If Me.AddDetail(objModel.ArticleID, objModel, trans) Then

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
    Private Function AddDetail(ByVal MasterID As Integer, ByVal objArticle As Article, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim strSQL As String = String.Empty
            Dim objmodels As List(Of ArticleDetail) = objArticle.ArticleDetails

            If objmodels Is Nothing Then Return False
            If objmodels.Count = 0 Then Return False

            For Each objmodel As ArticleDetail In objmodels
                strSQL = "insert into ArticleDefTable(ArticleCode,ArticleDescription,ArticleBARCode,ArticleGroupId,ArticleGenderId," & _
                                " ArticleTypeId,ArticleUnitId,ArticleLPOId,PurchasePrice,SalePrice,PackQty," & _
                                " SizeRangeId,ArticleColorId, MasterID, ArticleCategoryId,Active,ArticleBARCodeDisable)" & _
                                " values(N'" & objmodel.ArticleCode.Trim.Replace("'", "''") & "',N'" & objmodel.ArticleDescription.Trim.Replace("'", "''") & "',N'" & objmodel.ArticleBARCode.Trim.Replace("'", "''") & "'," & _
                                objmodel.ArticleGroupID & "," & objmodel.ArticleGenderID & "," & objmodel.ArticleTypeID & "," & objmodel.ArticleUnitID & "," & objmodel.ArticleLPOID & "," & _
                                objmodel.PurchasePrice & "," & objmodel.SalePrice & "," & objmodel.PackQty & "," & _
                                objmodel.SizeRangeID & "," & objmodel.ArticleColorID & "," & MasterID & "," & objmodel.ArticleCategoryId & "," & 1 & ", " & IIf(objmodel.ArticleBARCodeDisable = True, 1, 0) & ") select @@Identity"
                Dim Id As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

                strSQL = "insert into  ItemWiseDiscountDetail (ItemWiseDiscountId, ArticleId) values (" & ItemDiscountId & ", " & Id & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                If objArticle.ArticleBARCodeDisable = False Then
                    If objArticle.DefaultBarCodeSourceValue = "Product ID" Then
                        strSQL = " Update ArticleDefTable Set ArticleBARCode = '" & Id.ToString() & "' WHERE ArticleId = " & Id & ""
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    Else
                        strSQL = " Update ArticleDefTable Set ArticleBARCode = N'" & objArticle.ArticleCode.Trim.Replace("'", "''") & "' WHERE ArticleId = " & Id & ""
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    End If
                End If
                ''Start TFS4395
                strSQL = " Insert into ArticleBarcodeDefTAble (ArticleId,ArticleMasterId,ArticleBarCode ,ArticleCode ,ArticleName) " _
                          & " select ArticleId,MasterID,ArticleBArcode,ArticleCode , ArticleDescription   from ArticleDefTable where ArticleId = " & Id & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    ''End TFS4395
            Next
            Return True
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
