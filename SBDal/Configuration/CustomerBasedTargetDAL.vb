Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class CustomerBasedTargetDAL
    Public Function Add(ByVal CustomerTargetDetail As List(Of CustomerBasedTarget)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            For Each CustomerTarget As CustomerBasedTarget In CustomerTargetDetail
                If Not ExistingRecordCheck(CustomerTarget.TargetDetailId, trans) = True Then
                    strSQL = "INSERT INTO tblDefCustomerTarget(CustomerId, Target_Year, January, February, March, April, May, June, July, August, September, October, November, December) " _
                    & " VALUES(" & CustomerTarget.CustomerId & ", " & CustomerTarget.Target_Year & ", " & CustomerTarget.January & ", " & CustomerTarget.February & ", " & CustomerTarget.March & ", " & CustomerTarget.April & ", " & CustomerTarget.May & ", " & CustomerTarget.June & ", " & CustomerTarget.July & ", " & CustomerTarget.August & ", " & CustomerTarget.September & ", " & CustomerTarget.October & ", " & CustomerTarget.November & ", " & CustomerTarget.December & " )"
                Else
                    strSQL = "UPDATE tblDefCustomerTarget SET CustomerId=" & CustomerTarget.CustomerId & ", " _
                             & " Target_Year=" & CustomerTarget.Target_Year & ", " _
                             & " January=" & CustomerTarget.January & ", " _
                             & " February=" & CustomerTarget.February & "," _
                             & " March=" & CustomerTarget.March & ", " _
                             & " April=" & CustomerTarget.April & ", " _
                             & " May=" & CustomerTarget.May & ", " _
                             & " June=" & CustomerTarget.June & ",  " _
                             & " July=" & CustomerTarget.July & ", " _
                             & " August=" & CustomerTarget.August & ", " _
                             & " September=" & CustomerTarget.September & ", " _
                             & " October=" & CustomerTarget.October & ", " _
                             & " November=" & CustomerTarget.November & ", " _
                             & " December=" & CustomerTarget.December & " WHERE TargetDetailId=" & CustomerTarget.TargetDetailId
                End If

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ExistingRecordCheck(ByVal Id As Int16, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = UtilityDAL.GetDataTable("Select * From tblDefCustomerTarget WHERE TargetDetailId=" & Id, trans)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
