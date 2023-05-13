Imports SBModel
Imports System.Data.SqlClient
''Ameen 02-03-16
Public Class UserAccountRightsDAL

    Public Function Add(ByVal AccountRights As List(Of UserAccountRightsBE), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strQuery As String = ""
            strQuery = "Delete From tblUserAccountRights WHERE UserID=" & AccountRights.Item(0).UserID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            For Each Rights As UserAccountRightsBE In AccountRights
                Rights.UserID = AccountRights.Item(0).UserID
                strQuery = String.Empty
                strQuery = "INSERT INTO tblUserAccountRights(UserId, Account_Id, Rights) Values(" & Rights.UserID & ", " & IIf(Rights.AccountID = 0, "NULL", Rights.AccountID) & ", " & IIf(Rights.Rights = True, 1, 0) & ")" '191015
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
            Next
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function
    Public Shared Function UserAccountRightsList() As List(Of UserAccountRightsBE)
        Try

            Dim strQuery As String = "Select * from tblUserAccountRights"
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            Dim RightsList As New List(Of UserAccountRightsBE)
            Dim Rights As UserAccountRightsBE
            If dr.HasRows Then
                Do While dr.Read
                    Rights = New UserAccountRightsBE
                    Rights.AccountRightsID = dr.GetValue(0)
                    Rights.UserID = dr.GetValue(1)
                    Rights.AccountID = IIf(dr.GetValue(2) Is DBNull.Value, 0, dr.GetValue(2))
                    Rights.Rights = dr.GetValue(3)
                    RightsList.Add(Rights)
                Loop
            End If
            Return RightsList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

