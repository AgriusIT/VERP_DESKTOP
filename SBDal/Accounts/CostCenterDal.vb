Public Class CostCenterDal
    Public Function AddCostCenter(ByVal CostCenter As SBModel.CostCenter) As Integer
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "INSERT INTO tblDefCostCenter(Name, CostCenterGroup, Active, OutWardGatePass) Values(N'" & CostCenter.CostCenter & "', N'" & CostCenter.CostCenterHead & "', " & IIf(CostCenter.Active = True, 1, 0) & ", " & IIf(CostCenter.OutWardGatePass = True, 1, 0) & ") SELECT @@Identity"
            CostCenter.CostCenterId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            trans.Commit()
            Return CostCenter.CostCenterId
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
End Class
