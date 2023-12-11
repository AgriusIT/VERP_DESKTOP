'16-Jun-2015 Task#1 set host value

Imports System.Data.SqlClient
Imports System.Data
Imports SBModel

Public Class EmailSettingDAL
    'Dim Logged_In_User As New SBModel.Logged_In_Users()

    Enum enmEmail
        EmailId
        DisplayName
        EmailUser
        Host
        Email
        Password
        smptServer
        Port
        SSL
    End Enum
    Public Function add(ByVal emailseeting As EmailSeeting) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Dim str As String = "insert into TbldefEmail (DisplyName,EmailUser,Host,Email,EmailPassword,SmtpServer,port,ssl) values (N'" & emailseeting.DisplayName & "',N'" & emailseeting.EmailUser & "', N'" & emailseeting.Host & "', N'" & emailseeting.Email & "', N'" & emailseeting.EmailPassword & "', N'" & emailseeting.SmtpServer & "',' " & emailseeting.port & "', " & IIf(emailseeting.ssl = True, 1, 0) & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            trans.Commit()
            Return True
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
    Public Function Update(ByVal emailsetting As EmailSeeting) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction()
        Try
            Dim str As String = "update TbldefEmail set DisplyName = N'" & emailsetting.DisplayName & "', EmailUser=N'" & emailsetting.EmailUser & "', Host=N'" & emailsetting.Host & "',  Email = N'" & emailsetting.Email & "', EmailPassword = N'" & emailsetting.EmailPassword & "' ,SmtpServer = N'" & emailsetting.SmtpServer & "',port = N'" & emailsetting.port & "',ssl = " & IIf(emailsetting.ssl = True, 1, 0) & " where EmailId = " & emailsetting.EmailID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
            con.Close()
        End Try
    End Function
    Public Function Delete(ByVal emailsetting As EmailSeeting) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction()

        Try
            Dim str As String = "Delete from TbldefEmail where EmailID = N'" & emailsetting.EmailID & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function GetAllRecords(Optional ByVal condition As String = "") As DataTable
        Try

            Dim str As String = String.Empty
            str = "select * from TbldefEmail"
            'str = "select User_ID,User_Name,Password,Email from tblUser where User_ID=" & Logged_In_User.LoggedInUserID.ToString & " And User_Name=" & Logged_In_User.LoggedInUserName.Replace("'", "''").ToString

            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function EmailList() As List(Of EmailSeeting)
        Try
            Dim Email_List As List(Of EmailSeeting)
            Email_List = New List(Of EmailSeeting)
            Dim Email As EmailSeeting
            Dim str As String = String.Empty
            str = "Select * From tblDefEmail WHERE"
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, str, Nothing)
            If dr.HasRows Then
                Do While dr.Read
                    Email = New EmailSeeting
                    Email.EmailID = dr.GetValue(enmEmail.EmailId)
                    Email.DisplayName = dr.GetValue(enmEmail.DisplayName)
                    Email.Email = dr.GetValue(enmEmail.Email)
                    Email.EmailPassword = dr.GetValue(enmEmail.Password)
                    Email.SmtpServer = dr.GetValue(enmEmail.smptServer)
                    Email.port = dr.GetValue(enmEmail.Port)
                    Email.ssl = dr.GetValue(enmEmail.SSL)
                    Email_List.Add(Email)
                Loop
            End If
            Return Email_List
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetEmailSetting(ByVal EmailAddress As String, Optional ByVal id As Integer = 0) As List(Of EmailSeeting)
        Try
            Dim Email_List As List(Of EmailSeeting)
            Email_List = New List(Of EmailSeeting)
            Dim Email As EmailSeeting
            Dim str As String = String.Empty

            If EmailAddress.Length > 0 Then
                str = "Select * From tblDefEmail WHERE Email =N'" & EmailAddress & "'"
            Else
                str = "Select * From tblDefEmail WHERE EmailId =" & id
            End If

            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, str, Nothing)
            If dr.HasRows Then
                Do While dr.Read
                    Email = New EmailSeeting
                    Email.EmailID = dr.GetValue(enmEmail.EmailId)
                    Email.DisplayName = dr.GetValue(enmEmail.DisplayName)
                    Email.Email = dr.GetValue(enmEmail.Email)
                    Email.EmailPassword = dr.GetValue(enmEmail.Password)
                    Email.SmtpServer = dr.GetValue(enmEmail.smptServer)
                    Email.port = dr.GetValue(enmEmail.Port)
                    Email.ssl = dr.GetValue(enmEmail.SSL)
                    Email.Host = dr.GetValue(enmEmail.Host)         'Task#1 Add set host value
                    Email_List.Add(Email)
                Loop
            End If
            Return Email_List
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
