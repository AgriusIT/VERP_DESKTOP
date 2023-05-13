''12-jan-2018 task# 2087   Muhammad Abdullah new class created
Imports SBModel
Imports System.Data.SqlClient

Public Class PartialInMasterDAL

    'Public Function Add(objMod As PartialinMasterBE) As Object
    '    Dim Con As New SqlConnection(SQLHelper.CON_STR)
    '    If Con.State = ConnectionState.Closed Then Con.Open()
    '    Dim trans As SqlTransaction = Con.BeginTransaction
    '    Try

    '        Dim strSQL As String = String.Empty
    '        strSQL = "INSERT INTO PartialInMasterTable( ReceivingDate, RecieverName, Remarks, DocId, OutGatePassId) VALUES(" &
    '            objMod.ReceivingDate & ",N'" & objMod.RecieverName & "',N'" &
    '         objMod.Remarks & "',N'" & objMod.DocId & "',N'" & objMod.OutGatePassId & "') Select @@Identity"
    '        Dim id As String = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
    '        trans.Commit()
    '        Return id

    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function
    Public Function save(ByVal objmod As PartialinMasterBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim str As String
            ''TASK TFS1556: Added two new fields of DriverName and VehicleNo on 10-02-2017
            str = "INSERT INTO PartialInMasterTable( DocNo, ReceivingDate, Remarks ) VALUES(N'" &
               objmod.DocNo & "',N'" & objmod.ReceivingDate.ToString() & "',N'" &
            objmod.Remarks & "') Select @@Identity"
            objmod.Id = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            savedetail(objmod.Id, objmod, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function

    'Public Function Update(objMod As AdvanceTypeBE) As Boolean
    '    Dim Con As New SqlConnection(SQLHelper.CON_STR)
    '    If Con.State = ConnectionState.Closed Then Con.Open()
    '    Dim trans As SqlTransaction = Con.BeginTransaction
    '    Try

    '        Dim strSQL As String = String.Empty
    '        strSQL = "Update tblDefAdvancesType SET Title=" & objMod.ArticleMasterId & ", ArticleAliasCode=N'" &
    '            objMod.ArticleAliasCode.Replace("'", "''") & "', ArticleAliasName=N'" & objMod.ArticleAliasName.Replace("'", "''") &
    '            "', VendorId=" & objMod.VendorId & ",Active=" & IIf(objMod.Active = True, 1, 0) & ",SortOrder=" & Val(objMod.SortOrder) &
    '            " WHERE ArticleAliasID=" & objMod.ArticleAliasId & ""
    '        SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
    '        trans.Commit()
    '        Return True

    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function
    Public Function savedetail(ByVal masterid As Integer, ByVal objmod As PartialinMasterBE, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim returnablegatepasslist As List(Of PartialinDetailBE) = objmod.Detail
            Dim str As String
            For Each gatepasslist As PartialinDetailBE In returnablegatepasslist 'PartialInMasterId, outDetailSrNo, outMasterId, InQuantity, Remarks''

                str = "insert into PartialInDetailTable(PartialInMasterId, outDetailSrNo, outMasterId, InQuantity, Remarks) values ('" & masterid.ToString() &
                    "'," & gatepasslist.outDetailSrNo & ", N'" & gatepasslist.outMasterId & "', " & gatepasslist.InQuantity & ", N'" & gatepasslist.Remarks.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            
            Return True
        Catch ex As Exception
        End Try
    End Function
    Public Function Update(ByVal objmod As PartialinMasterBE) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim str As String
            'Update Master Data
            ''TASK TFS1556: Added two new fields of DriverName and VehicleNo on 10-02-2017 ''DocNo, ReceivingDate, Remarks 
            str = "Update PartialInMasterTable set DocNo = N'" & objmod.DocNo & "',ReceivingDate = N'" &
                objmod.ReceivingDate.ToString("yyyy-M-d h:mm:ss tt") & "', Remarks = N'" & objmod.Remarks & "' "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Previouse Data from Gata pass Detail Table 
            str = "Delete from PartialInDetailTable where PartialInMasterId = N'" & objmod.Id & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Gate pass Data Detail 
            savedetail(objmod.Id, objmod, trans)

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
            str += "select Id, DocNo, ReceivingDate, InDetailId, PartialInMasterId, outDetailSrNo, outMasterId, InQuantity, dbo.PartialInDetailTable.Remarks, IssueDetailID, Issue_Id, IssueDetail, ReceivedDate, IssueQty, Reference, LocationId, ArticleDefId, Comments, SrNo, NULL as limitQuantity   from dbo.PartialInMasterTable "
            str += " left join dbo.PartialInDetailTable on  dbo.PartialInDetailTable.PartialInMasterId = dbo.PartialInMasterTable.Id "
            str += " left join dbo.GatepassDetailTable on           dbo.GatepassDetailTable.Issue_id = dbo.PartialInDetailTable.outMasterId"
            str += " And dbo.GatepassDetailTable.SrNo = dbo.PartialInDetailTable.outDetailSrNo"
            str += " where dbo.PartialInMasterTable.Id = " & MasterId
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAllRecords() As DataTable
        Try
            Dim str As String
            ''TASK TFS1556: Added two new fields of DriverName and VehicleNo on 10-02-2017
            str = "   select  dbo.PartialInMasterTable.Id, dbo.PartialInMasterTable.ReceivingDate, dbo.PartialInMasterTable.DocNo,  dbo.PartialInMasterTable.Remarks," &
                "   dbo.PartialInDetailTable.InDetailId, dbo.PartialInDetailTable.PartialInMasterId, dbo.PartialInDetailTable.outDetailSrNo, " &
                "      dbo.PartialInDetailTable.outMasterId, dbo.PartialInDetailTable.InQuantity, dbo.PartialInDetailTable.Remarks as Reference" &
            " from dbo.PartialInMasterTable " &
            " left join dbo.PartialInDetailTable on dbo.PartialInDetailTable.PartialInMasterId = dbo.PartialInMasterTable.Id " &
            "  left join dbo.GatepassDetailTable on dbo.PartialInDetailTable.outDetailSrNo = dbo.GatepassDetailTable.SrNo  and dbo.PartialInDetailTable.outMasterId = dbo.GatepassDetailTable.Issue_Id   Order By Id DESC "

            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Delete(masterId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = " Delete From PartialInMasterTable  WHERE Id= '" & masterId & "'  Delete From PartialInDetailTable  WHERE PartialInMasterId= '" & masterId & "' "
            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
End Class

Public Class PartialInDetailDAL
    Public Function GetDetail(MasterId) As DataTable
        Try

            'Dim strQuery As String = " select * from dbo.PartialInMasterTable left join dbo.PartialInDetailTable on dbo.PartialInDetailTable.PartialInMasterId = dbo.PartialInMasterTable.Id  where PartialInMasterTable.Id = " & MasterId
            Dim strQuery As String = "  select Id, DocNo, ReceivingDate, InDetailId, PartialInMasterId, outDetailSrNo, outMasterId, InQuantity, dbo.PartialInDetailTable.Remarks, IssueDetailID, Issue_Id, IssueDetail, ReceivedDate, IssueQty, Reference, " &
            " LocationId, ArticleDefId, Comments, SrNo, NULL as limitQuantity   from dbo.PartialInMasterTable " &
            " left join dbo.PartialInDetailTable on dbo.PartialInDetailTable.PartialInMasterId = dbo.PartialInMasterTable.Id " &
            "  left join dbo.GatepassDetailTable on dbo.PartialInDetailTable.outDetailSrNo = dbo.GatepassDetailTable.SrNo   where PartialInMasterTable.Id = " & MasterId
            Return UtilityDAL.GetDataTable(strQuery)

        Catch ex As Exception
            Throw ex
        End Try

    End Function
  
    'Public Function Add(objMod As PartialinDetailBE) As Boolean
    '    Dim Con As New SqlConnection(SQLHelper.CON_STR)
    '    If Con.State = ConnectionState.Closed Then Con.Open()
    '    Dim trans As SqlTransaction = Con.BeginTransaction
    '    Try

    '        Dim strSQL As String = String.Empty
    '        strSQL = "INSERT INTO PartialInDetailTable(  PartialInMasterId, outDetailId, InQuantity, Remarks) VALUES(" &
    '            objMod.PartialInMasterId & ",N'" & objMod.outDetailId & "',N'" &
    '         objMod.InQuantity & "," & objMod.Remarks & ") Select @@Identity"
    '        SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
    '        trans.Commit()
    '        Return True

    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function

    'Public Function Update(objMod As AdvanceTypeBE) As Boolean
    '    Dim Con As New SqlConnection(SQLHelper.CON_STR)
    '    If Con.State = ConnectionState.Closed Then Con.Open()
    '    Dim trans As SqlTransaction = Con.BeginTransaction
    '    Try

    '        Dim strSQL As String = String.Empty
    '        strSQL = "Update tblDefAdvancesType SET Title=" & objMod.ArticleMasterId & ", ArticleAliasCode=N'" &
    '            objMod.ArticleAliasCode.Replace("'", "''") & "', ArticleAliasName=N'" & objMod.ArticleAliasName.Replace("'", "''") &
    '            "', VendorId=" & objMod.VendorId & ",Active=" & IIf(objMod.Active = True, 1, 0) & ",SortOrder=" & Val(objMod.SortOrder) &
    '            " WHERE ArticleAliasID=" & objMod.ArticleAliasId & ""
    '        SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
    '        trans.Commit()
    '        Return True

    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function

    'Public Function Delete(objMod As ArticleAliasBE) As Boolean
    '    Dim Con As New SqlConnection(SQLHelper.CON_STR)
    '    If Con.State = ConnectionState.Closed Then Con.Open()
    '    Dim trans As SqlTransaction = Con.BeginTransaction
    '    Try

    '        Dim strSQL As String = String.Empty
    '        strSQL = "Delete From ArticleAliasDefTable WHERE ArticleAliasID=" & objMod.ArticleAliasId & ""
    '        SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
    '        trans.Commit()
    '        Return True

    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function
End Class