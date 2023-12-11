Imports System.Data.SqlClient
Imports SBModel
Imports SBDal.UtilityDAL
Public Class BSandPLReportTemplateDAL
    Public Function Save(ByVal pay As BSandPLReportTemplateBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Insert Into ReportTemplate(Title,Remarks,Type,SortOrder,Active) values(N'" & pay.Title & "', N'" & pay.Remarks.Replace("'", "''") & "', '" & pay.Type & "'," & pay.SortOrder & ", " & IIf(pay.Active = True, 1, 0) & ")Select @@Identity"
            pay.ReportTemplateId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal pay As BSandPLReportTemplateBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update ReportTemplate set Title=N'" & pay.Title.Replace("'", "''") & "',Remarks=N'" & pay.Remarks.Replace("'", "''") & "', Type=N'" & pay.Type & "', SortOrder=" & pay.SortOrder & ", Active=" & IIf(pay.Active = True, 1, 0) & " WHERE ReportTemplateId = " & pay.ReportTemplateId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete_Record(ByVal pay As BSandPLReportTemplateBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From ReportTemplate WHERE ReportTemplateId=" & pay.ReportTemplateId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function GetAll() As DataTable
        Try
            Dim str As String = String.Empty
            str = "SELECT * FROM ReportTemplate"
            Return GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
