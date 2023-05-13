Imports SBModel
Imports System.Data.SqlClient

Public Class LabourTypeDAL
    Public Function save(ByVal objmod As LabourTypeBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim str As String
            ''TASK TFS1556: Added two new fields of DriverName and VehicleNo on 10-02-2017
            str = "INSERT INTO tblLabourType(LabourType, ChargeTypeId, Remarks, SortOrder,  Active, AccountId) VALUES(N'" &
               objmod.LabourType & "', N'" & objmod.ChargeTypeId.ToString() & "', N'" &
            objmod.Remarks & "', '" & objmod.SortOrder.ToString() & "','" & Convert.ToBoolean(objmod.Active.ToString()) & "', " & objmod.AccountId & ") Select @@Identity"
            objmod.Id = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function


    Public Function Update(ByVal objmod As LabourTypeBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim str As String
            'Update Master Data
            ''TASK TFS1556: Added two new fields of DriverName and VehicleNo on 10-02-2017 ''DocNo, ReceivingDate, Remarks 
            str = "Update tblLabourType set LabourType = N'" & objmod.LabourType & "',ChargeTypeId = N'" &
                objmod.ChargeTypeId.ToString() & "', Remarks = N'" & objmod.Remarks & "', SortOrder = '" & objmod.SortOrder.ToString() & "', Active = '" & Convert.ToBoolean(objmod.Active.ToString()) & "', AccountId = " & objmod.AccountId & " WHERE Id =" & objmod.Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Previouse Data from Gata pass Detail Table 
            'Insert Gate pass Data Detail 
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function GetRecordById(ByVal MasterId As Integer) As DataTable
        Try
            Dim str As String
            str += " select tblLabourType.Id, dbo.tblLabourType.LabourType, ChargeType.Charge, ISNULL(dbo.tblLabourType.AccountId, 0) AS AccountId, Account.detail_title AS Account,  dbo.tblLabourType.Remarks, dbo.tblLabourType.SortOrder, Convert(bit, IsNull(dbo.tblLabourType.Active, 0)) AS Active , ChargeType.Id as chId from dbo.tblLabourType  left join ChargeType on ChargeType.Id = dbo.tblLabourType.ChargeTypeId LEFT OUTER JOIN vwCOADetail AS Account ON tblLabourType.AccountId = Account.coa_detail_id where Id = '" & MasterId & "' order by tblLabourType.Id desc"
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAllRecords() As DataTable
        Try
            Dim str As String
            ''TASK TFS1556: Added two new fields of DriverName and VehicleNo on 10-02-2017
            str = "select tblLabourType.Id, dbo.tblLabourType.LabourType, ChargeType.Charge, ISNULL(dbo.tblLabourType.AccountId, 0) AS AccountId, Account.detail_title AS Account, dbo.tblLabourType.Remarks, dbo.tblLabourType.SortOrder, Convert(bit, IsNull(dbo.tblLabourType.Active, 0)) AS Active , ChargeType.Id as chId, ISNULL(dbo.tblLabourType.AccountId, 0) AS AccountId, Account.detail_title AS Account  from dbo.tblLabourType  left join ChargeType on ChargeType.Id = dbo.tblLabourType.ChargeTypeId LEFT OUTER JOIN vwCOADetail AS Account ON tblLabourType.AccountId = Account.coa_detail_id order by tblLabourType.Id desc "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Delete(ByVal LabourTypeId As Integer) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim str As String
            'Update Master Data
            ''TASK TFS1556: Added two new fields of DriverName and VehicleNo on 10-02-2017 ''DocNo, ReceivingDate, Remarks 
            str = "Delete FROM tblLabourType WHERE Id = " & LabourTypeId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Previouse Data from Gata pass Detail Table 
            'Insert Gate pass Data Detail 
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
End Class
