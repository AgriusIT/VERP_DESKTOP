'TFS1751: Waqar Raza
'Start TFS1751
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class UserPOSRightsDAL

    Public Function Add(ByVal POSRights As List(Of UserPOSRightsBE), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strQuery As String = ""
            strQuery = "Delete From tblUserPOSRights WHERE UserID=" & POSRights.Item(0).UserId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            For Each Rights As UserPOSRightsBE In POSRights
                Rights.UserId = POSRights.Item(0).UserId
                strQuery = String.Empty
                strQuery = "INSERT INTO tblUserPOSRights(UserId, POSId, Rights) Values(" & Rights.UserId & ", " & IIf(Rights.POSId = 0, "NULL", Rights.POSId) & ", " & IIf(Rights.Rights = True, 1, 0) & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
            Next
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function

    Public Shared Function UserPOSRightsList() As List(Of UserPOSRightsBE)
        Try

            Dim strQuery As String = "Select * from tblUserPOSRights"
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            Dim RightsList As New List(Of UserPOSRightsBE)
            Dim Rights As UserPOSRightsBE
            If dr.HasRows Then
                Do While dr.Read
                    Rights = New UserPOSRightsBE
                    Rights.POSRightsId = dr.GetValue(0)
                    Rights.UserId = dr.GetValue(1)
                    Rights.POSId = IIf(dr.GetValue(2) Is DBNull.Value, 0, dr.GetValue(2))
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
'End TFS1751
