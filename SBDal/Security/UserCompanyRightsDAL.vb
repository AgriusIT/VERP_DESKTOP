''Muhammad Ameen Task 191015: Assign 0 to Properties CompanyId & Location_ID in case returned value is null from table. //UserCompanyRightsList
'' Muhammad Ameen Task 191015: Insert NULL into column in case CompanyId is 0;
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class UserCompanyRightsDAL
    Public Function Add(ByVal CompanyRights As List(Of UserCompanyRightsBE), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strQuery As String = ""
            strQuery = "Delete From tblUserCompanyRights WHERE User_ID=" & CompanyRights.Item(0).User_Id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            For Each Rights As UserCompanyRightsBE In CompanyRights
                Rights.User_Id = CompanyRights.Item(0).User_Id
                strQuery = String.Empty
                strQuery = "INSERT INTO tblUserCompanyRights(User_Id, CompanyId, Rights) Values(" & Rights.User_Id & ", " & IIf(Rights.CompanyId = 0, "NULL", Rights.CompanyId) & ", " & IIf(Rights.Rights = True, 1, 0) & ")" '191015
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
            Next
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function
    Public Shared Function UserCompanyRightsList() As List(Of UserCompanyRightsBE)
        Try

            Dim strQuery As String = "Select * from tblUserCompanyRights"
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            Dim RightsList As New List(Of UserCompanyRightsBE)
            Dim Rights As UserCompanyRightsBE
            If dr.HasRows Then
                Do While dr.Read
                    Rights = New UserCompanyRightsBE
                    Rights.CompanyRightsId = dr.GetValue(0)
                    Rights.User_Id = dr.GetValue(1)
                    Rights.CompanyId = IIf(dr.GetValue(2) Is DBNull.Value, 0, dr.GetValue(2)) '191015
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
