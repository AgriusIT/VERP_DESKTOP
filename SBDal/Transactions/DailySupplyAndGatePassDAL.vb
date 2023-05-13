Imports SBModel
Imports System.Data.SqlClient
Public Class DailySupplyAndGatePassDAL
    Public Function Add(ByVal DailySupplyDetail As List(Of SBModel.DailySupplyAndGatePass)) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If Not con.State = 1 Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try

            Dim str As String = String.Empty
            For Each DailySupply As DailySupplyAndGatePass In DailySupplyDetail

                str = "Delete From  DailySupplyAndGatePassTable WHERE SalesNo=N'" & DailySupply.SalesNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = "INSERT INTO DailySupplyAndGatePassTable(DeliveryDate, SalesNo, Status, UserName, EntryDate, BiltyNo) " _
                                    & " Values(N'" & DailySupply.DeliveryDate & "', N'" & DailySupply.SalesNo & "', N'" & DailySupply.Status & "', N'" & DailySupply.UserName & "', N'" & DailySupply.EntryDate & "', N'" & DailySupply.BiltyNo & "') Select @@Identity "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function GetSalesNo(ByVal SalesNo As String) As Boolean
        Try
            Dim str As String = String.Empty
            str = "Select SalesNo From DailySupplyAndGatePassTable WHERE SalesNo=N'" & SalesNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
