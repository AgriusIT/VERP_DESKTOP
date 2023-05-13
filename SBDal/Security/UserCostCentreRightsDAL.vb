Imports SBModel
Imports System.Data.SqlClient
''Ameen 02-03-16
Public Class UserCostCentreRightsDAL

    Public Function Add(ByVal CostCentreRights As List(Of UserCostCentreRightsBE), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strQuery As String = ""
            strQuery = "Delete From tblUserCostCentreRights WHERE UserID=" & CostCentreRights.Item(0).UserID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            For Each Rights As UserCostCentreRightsBE In CostCentreRights
                Rights.UserID = CostCentreRights.Item(0).UserID
                strQuery = String.Empty
                strQuery = "INSERT INTO tblUserCostCentreRights(UserId, CostCentre_Id, Rights) Values(" & Rights.UserID & ", " & IIf(Rights.CostCentreId = 0, "NULL", Rights.CostCentreId) & ", " & IIf(Rights.Rights = True, 1, 0) & ")" '170316
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
            Next
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function
    Public Shared Function UserCostCentreRightsList() As List(Of UserCostCentreRightsBE)
        Try

            Dim strQuery As String = "Select * from tblUserCostCentreRights"
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            Dim RightsList As New List(Of UserCostCentreRightsBE)
            Dim Rights As UserCostCentreRightsBE
            If dr.HasRows Then
                Do While dr.Read
                    Rights = New UserCostCentreRightsBE
                    Rights.AccountRightsID = dr.GetValue(0)
                    Rights.UserID = dr.GetValue(1)
                    Rights.CostCentreId = IIf(dr.GetValue(2) Is DBNull.Value, 0, dr.GetValue(2))
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


