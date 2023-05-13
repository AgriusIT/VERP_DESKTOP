Imports System.Data.OleDb
Public Class ChangePassword

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click

        If Me.PasswordTextBox.Text.Length < 1 Then
            msg_Error("Please enter old password")
            Me.PasswordTextBox.Focus()
            Exit Sub
        End If

        If Me.txtNewPassword.Text.Length < 1 Then
            msg_Error("Please enter new password")
            Me.txtNewPassword.Focus()
            Exit Sub
        End If

        Dim strUserValidity As String = CheckSecurityUser(Me.UsernameTextBox.Text, Me.PasswordTextBox.Text)

        If strUserValidity = "In-Valid" Then
            msg_Error(str_ErrorInvalidUser)
        ElseIf strUserValidity = "In-Active" Then
            msg_Error(str_ErrorInActiveUser)
        ElseIf strUserValidity = "Valid" Then
            If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
            If Update_Record() Then
                msg_Information(str_informUpdate)
                Me.txtNewPassword.Text = ""
                Me.PasswordTextBox.Text = ""
                Me.Close()
            Else
                msg_Error("Error! Record not updated")

            End If

        Else
            End
        End If
    End Sub
    Private Function Update_Record() As Boolean
        Dim objCommand As New oledbCommand
        Dim objCon As oledbConnection
        Dim i As Integer = 0

        objCon = Con 'New oledbConnection("Password=sa;Integrated Security Info=False;User ID=sa;Password=lumensoft2003; Initial Catalog=ShoesStore;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As oledbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            objCommand.CommandText = "Update tblUser set  Password='" & Encrypt(Me.txtNewPassword.Text) & "' Where User_ID= " & LoginUserId & " "

            objCommand.ExecuteNonQuery()

            trans.Commit()
            Update_Record = True


            Try
                ''insert Activity Log
                SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Security, LoginUserId)
            Catch ex As Exception

            End Try

        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
        End Try
    End Function
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub ChangePassword_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.UsernameTextBox.Text = LoginUserCode
        Me.PasswordTextBox.Focus()
    End Sub
End Class
