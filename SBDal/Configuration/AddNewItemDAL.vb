Public Class AddNewItemDAL
    Public Function Department() As DataTable
        Try
            Dim str As String = String.Empty
            'str = "select a.ArticleGroupId, a.ArticleGroupName  From ArticleGroupDefTable a INNER JOIN  tblCOAMainSubSub b on b.main_sub_sub_id = a.SubSubID "
            str = "Select ArticleGroupId, ArticleGroupName, SubSubId, main_sub_sub_Id From ArticleGroupDefTable LEFT OUTER JOIN vwCOADetail COA On COA.coa_detail_id = ArticleGroupDefTable.SubsubId"
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function type() As DataTable
        Try
            Dim str As String = String.Empty
            str = "select  ArticleTypeId, ArticleTypeName from ArticleTypeDefTable "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw (ex)
        End Try
    End Function
    Public Function unit() As DataTable
        Try
            Dim str As String = String.Empty
            str = "select ArticleUnitId, ArticleUnitName from ArticleUnitDefTable"
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function size() As DataTable
        Try
            Dim str As String = String.Empty
            str = "select ArticleSizeId , ArticleSizeName from ArticleSizeDefTable"
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Combination() As DataTable
        Try
            Dim str As String = String.Empty
            Str = "select ArticleColorId,ArticleColorName from ArticleColorDefTable"
            Return UtilityDAL.GetDataTable(Str)

        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
