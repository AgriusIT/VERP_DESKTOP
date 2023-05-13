Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class MailSentDAL
    Public Function Add(ByVal Email As Email) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = "INSERT INTO MailSentBoxTable(ToEmail, CC, BCC, Subject, Body, Status, Attachment) " _
            & " VALUES(N'" & Email.ToEmail & "', N'" & Email.CCEmail & "', N'" & Email.BccEmail & "', N'" & Email.Subject.Replace("'", "''") & "', N'" & Email.Body.Replace("'", "''") & "', N'" & Email.Status & "', N'" & Email.Attachment & "') SELECT @@Identity"
            Email.MailSentBoxId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            'Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal Email As Email) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = "UPDATE MailSentBoxTable Set Status=N'" & Email.Status & "' WHERE MailSentBoxId=" & Email.MailSentBoxId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            'Throw ex
        Finally
            Con.Close()
        End Try
    End Function
End Class
