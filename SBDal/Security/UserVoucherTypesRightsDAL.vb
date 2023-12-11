''TASK:988 Display rights based voucher types on voucher entry. Ameen on 22-06-2017 
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility

Public Class UserVoucherTypesRightsDAL
    Public Function Add(ByVal VoucherTypesRights As List(Of UserVoucherTypesRightsBE), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strQuery As String = ""
            strQuery = "Delete From tblUserVoucherTypesRights WHERE UserID=" & VoucherTypesRights.Item(0).UserId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            For Each Rights As UserVoucherTypesRightsBE In VoucherTypesRights
                Rights.UserId = VoucherTypesRights.Item(0).UserId
                strQuery = String.Empty
                strQuery = "INSERT INTO tblUserVoucherTypesRights(UserId, VoucherTypeId, Rights) Values(" & Rights.UserId & ", " & IIf(Rights.VoucherTypeId = 0, "NULL", Rights.VoucherTypeId) & ", " & IIf(Rights.Rights = True, 1, 0) & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)
            Next
            Return True
        Catch ex As Exception
            trans.Rollback()
        End Try
    End Function
    Public Shared Function UserVoucherTypesRightsList() As List(Of UserVoucherTypesRightsBE)
        Try

            Dim strQuery As String = "Select * from tblUserVoucherTypesRights"
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            Dim RightsList As New List(Of UserVoucherTypesRightsBE)
            Dim Rights As UserVoucherTypesRightsBE
            If dr.HasRows Then
                Do While dr.Read
                    Rights = New UserVoucherTypesRightsBE
                    Rights.Id = dr.GetValue(0)
                    Rights.UserId = dr.GetValue(1)
                    Rights.VoucherTypeId = IIf(dr.GetValue(2) Is DBNull.Value, 0, dr.GetValue(2))
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
