Imports System.Data.SqlClient
Imports SBModel
Imports SBDal.UtilityDAL
Public Class BSandPLTemplateMappingDAL
    Public Function SaveSubSub(ByVal pay As BSandPLTemplateMappingBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "If Exists(Select ReportTemplateDetailId From ReportTemplateDetail Where ReportTemplateDetailId=" & pay.ReportTemplateDetailId & ")  Update ReportTemplateDetail Set ReportTemplateId = " & pay.ReportTemplateId & ", AccountId = " & pay.AccountId & ", AccountLevel = N'" & pay.AccountLevel & "', BSNotesId = " & pay.BSNotesId & ", PLNotesId = " & pay.PLNotesId & ", CategoryId = " & pay.CategoryId & " Where ReportTemplateDetailId =" & pay.ReportTemplateDetailId & "" _
                    & " Else Insert Into ReportTemplateDetail(ReportTemplateId,AccountId,AccountLevel,BSNotesId,PLNotesId,CategoryId) values(" & pay.ReportTemplateId & ", " & pay.AccountId & ", '" & pay.AccountLevel & "'," & pay.BSNotesId & ", " & pay.PLNotesId & ", " & pay.CategoryId & ")Select @@Identity"
            pay.ReportTemplateId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function SaveDetail(ByVal pay As BSandPLTemplateMappingBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Insert Into ReportTemplateDetail(ReportTemplateId,AccountId,AccountLevel,BSNotesId,PLNotesId,CategoryId) values(" & pay.ReportTemplateId & ", " & pay.AccountId2 & ", '" & pay.AccountLevel & "'," & pay.BSNotesId & ", " & pay.PLNotesId & ", " & pay.CategoryId & ")Select @@Identity"
            pay.ReportTemplateId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function UpdateSubSub(ByVal pay As BSandPLTemplateMappingBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update ReportTemplateDetail set ReportTemplateId=" & pay.ReportTemplateId & ",AccountId=" & pay.AccountId & ", AccountLevel=N'" & pay.AccountLevel & "', BSNotesId=" & pay.BSNotesId & ", PLNotesId=" & pay.PLNotesId & ", CategoryId = " & pay.CategoryId & " WHERE ReportTemplateDetailId = " & pay.ReportTemplateDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function UpdateDetail(ByVal pay As BSandPLTemplateMappingBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update ReportTemplateDetail set ReportTemplateId=" & pay.ReportTemplateId & ",AccountId=" & pay.AccountId2 & ", AccountLevel=N'" & pay.AccountLevel & "', BSNotesId=" & pay.BSNotesId & ", PLNotesId=" & pay.PLNotesId & ", CategoryId = " & pay.CategoryId & " WHERE ReportTemplateDetailId = " & pay.ReportTemplateDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function DeleteDetail(ByVal id As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From ReportTemplateDetail WHERE ReportTemplateDetailId=" & id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function SaveCategory(ByVal pay As BSandPLTemplateMappingBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO ReportTemplateNotesCategory(CategoryTitle,BSNotesId,PLNotesId) values(N'" & pay.CategoryTitle & "'," & pay.BSNotesId & ", " & pay.PLNotesId & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function DeleteCategory(ByVal Id As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "DELETE FROM ReportTemplateNotesCategory WHERE CategoryId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
End Class
