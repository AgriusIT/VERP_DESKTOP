Imports SBModel
Imports SBDal
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class RestrictedItemByLocationDAL
    Public Function Add(ByVal RestrictedItemByLocation As List(Of RestrictedItemsByLocation), ByVal LocationId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty

            str = "Delete From RestrictedItemByLocationTable WHERE LocationId=" & LocationId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            For Each RestrictedItemDetail As RestrictedItemsByLocation In RestrictedItemByLocation
                str = "INSERT INTO RestrictedItemByLocationTable(LocationId, ArticleDefId, Restricted, EntryDate, UserName) " _
                & " Values(" & RestrictedItemDetail.LocationId & ", " & RestrictedItemDetail.ArticleDefId & ", " & IIf(RestrictedItemDetail.Restricted = True, 1, 0) & ", N'" & RestrictedItemDetail.EntryDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & RestrictedItemDetail.UserName & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetRestrictedItemsByLocationId(ByVal LocationId As Integer) As DataTable
        'Try
        '    Dim str As String = String.Empty
        '    str = " SELECT Restricted.LocationId, ArticleDefView.ArticleGroupName , dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, " _
        '          & " dbo.ArticleDefView.ArticleColorName, dbo.ArticleDefView.ArticleSizeName, dbo.ArticleDefView.ArticleUnitName, Isnull(Restricted.Restricted,0) as Restricted " _
        '          & " FROM dbo.ArticleDefView LEFT OUTER JOIN " _
        '          & " (SELECT     dbo.RestrictedItemByLocationTable.ArticleDefId, dbo.RestrictedItemByLocationTable.LocationId,  " _
        '          & " dbo.RestrictedItemByLocationTable.Restricted " _
        '          & " FROM dbo.RestrictedItemByLocationTable INNER JOIN  " _
        '          & " dbo.tblDefLocation ON dbo.RestrictedItemByLocationTable.LocationId = dbo.tblDefLocation.location_id WHERE tblDefLocation.Location_Id=" & LocationId & ") Restricted ON  " _
        '          & " Restricted.ArticleDefId = dbo.ArticleDefView.ArticleId " _
        '          & " ORDER BY ArticleDefView.SortOrder Asc "
        '    Return UtilityDAL.GetDataTable(str)
        'Catch ex As Exception
        '    Throw ex
        'End Try
        Try
            Dim str As String = String.Empty
            str = " SELECT     Restricted.LocationId, ArticleDefView.ArticleGroupName , ArticleDefView.ArticleTypeName, ArticleDefView.ArticleCompanyName , ArticleDefView.ArticleLpoName, ArticleDefView.ArticleId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, " _
                   & " ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, Isnull(Restricted.Restricted, 0) AS Restricted" _
                   & " FROM dbo.ArticleDefView LEFT OUTER JOIN " _
                   & " (SELECT     dbo.RestrictedItemByLocationTable.ArticleDefId, dbo.RestrictedItemByLocationTable.LocationId,  " _
                   & " Convert(bit, IsNull(dbo.RestrictedItemByLocationTable.Restricted, 0)) As Restricted " _
                   & " FROM dbo.RestrictedItemByLocationTable LEFT OUTER JOIN  " _
                   & " dbo.tblDefLocation ON dbo.RestrictedItemByLocationTable.LocationId = dbo.tblDefLocation.location_id WHERE tblDefLocation.Location_Id=" & LocationId & ") Restricted ON  " _
                   & " Restricted.ArticleDefId = dbo.ArticleDefView.ArticleId " _
                   & " ORDER BY ArticleDefView.SortOrder Asc "
            ' & " (SELECT dbo.RestrictedItemByLocationTable.ArticleDefId, dbo.RestrictedItemByLocationTable.LocationId, RestrictedItemByLocationTable.Restricted" _
            '& " FROM          RestrictedItemByLocationTable INNER JOIN " _
            '& " tblDefLocation ON RestrictedItemByLocationTable.LocationId = tblDefLocation.location_id " _
            '& " WHERE (tblDefLocation.location_id = 0)) Restricted ON Restricted.ArticleDefId = ArticleDefView.ArticleId " _
            '& "  ORDER BY ArticleDefView.SortOrder Asc"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
