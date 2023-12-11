'' Muhammad Ameen Task 191015: Insert NULL into column in case Location_ID is 0;
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility

Public Class UserLocationRightsDAL
    Public Function Add(ByVal LocationRights As List(Of UserLocationRightsBE), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strQuery As String = ""
            strQuery = "Delete From tblUserLocationRights WHERE UserID=" & LocationRights.Item(0).UserID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            For Each Rights As UserLocationRightsBE In LocationRights
                Rights.UserID = LocationRights.Item(0).UserID
                strQuery = String.Empty
                strQuery = "INSERT INTO tblUserLocationRights(UserId, Location_Id, Rights) Values(" & Rights.UserID & ", " & IIf(Rights.Location_ID = 0, "NULL", Rights.Location_ID) & ", " & IIf(Rights.Rights = True, 1, 0) & ")" '191015
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
            Next
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function
    Public Shared Function UserLocationRightsList() As List(Of UserLocationRightsBE)
        Try

            Dim strQuery As String = "Select * from tblUserLocationRights"
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            Dim RightsList As New List(Of UserLocationRightsBE)
            Dim Rights As UserLocationRightsBE
            If dr.HasRows Then
                Do While dr.Read
                    Rights = New UserLocationRightsBE
                    Rights.ID = dr.GetValue(0)
                    Rights.UserID = dr.GetValue(1)
                    Rights.Location_ID = IIf(dr.GetValue(2) Is DBNull.Value, 0, dr.GetValue(2))
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
