Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class ReturnAbleGatePassDAL
    Public Function save(ByVal objmod As ReturnAbleGatePassMaster) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim str As String
            ''TASK TFS1556: Added two new fields of DriverName and VehicleNo on 10-02-2017
            str = "insert into GatepassMasterTable (Issue_No,Issue_date,Issue_to,Remarks,Username,Fdate, DriverName, VehicleNo) values (N'" & objmod.Issue_No & "',N'" & objmod.Issue_date.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & objmod.Issue_to & "',N'" & objmod.Remarks & "',  N'" & objmod.Username & "',N'" & objmod.Fdate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objmod.DriverName.Replace("'", "''") & "', N'" & objmod.VehicleNo.Replace("'", "''") & "') Select @@Identity"
            objmod.Issue_id = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            savedetail(objmod.Issue_id, objmod, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function Update(ByVal objmod As ReturnAbleGatePassMaster) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim str As String
            'Update Master Data
            ''TASK TFS1556: Added two new fields of DriverName and VehicleNo on 10-02-2017
            str = "Update gatepassmastertable set issue_no=N'" & objmod.Issue_No & "',Issue_date=N'" & objmod.Issue_date.ToString("yyyy-M-d h:mm:ss tt") & "',Issue_to=N'" & objmod.Issue_to & "', remarks=N'" & objmod.Remarks & "',username=N'" & objmod.Username & "',fdate=N'" & objmod.Fdate.ToString("yyyy-M-d h:mm:ss tt") & "', DriverName=N'" & objmod.DriverName.Replace("'", "''") & "', VehicleNo=N'" & objmod.VehicleNo.Replace("'", "''") & "' where issue_id = N'" & objmod.Issue_id & "' "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Previouse Data from Gata pass Detail Table 
            str = "Delete from GatepassDetailTable where issue_id = N'" & objmod.Issue_id & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Gate pass Data Detail 
            savedetail(objmod.Issue_id, objmod, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Public Function savedetail(ByVal masterid As Integer, ByVal objmod As ReturnAbleGatePassMaster, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim returnablegatepasslist As List(Of ReturnAbleGatePassDetail) = objmod.ReturnableGatePassDetail
            Dim str As String
            For Each gatepasslist As ReturnAbleGatePassDetail In returnablegatepasslist
                str = "insert into GatepassDetailTable(Issue_Id, IssueDetail, IssueQty, Reference) values (" & masterid & ", N'" & gatepasslist.IssueDetail.Replace("'", "''") & "', " & gatepasslist.IssueQty & ", N'" & gatepasslist.Reference.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            str = "Update GatePassDetailTable set SrNo = IssueDetailID where Issue_Id = '" & masterid.ToString() & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Return True
        Catch ex As Exception
        End Try
    End Function
    Public Function GetRecordById(ByVal MasterId As Integer) As DataTable
        Try
            Dim str As String
            str = "select Issue_Id, IssueDetail, Isnull(IssueQty,0) as IssueQty, Isnull(Reference, '') as Reference  from GatepassDetailTable where issue_id = " & MasterId & " "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAllRecords() As DataTable
        Try
            Dim str As String
            ''TASK TFS1556: Added two new fields of DriverName and VehicleNo on 10-02-2017
            str = " select  issue_id,Issue_No,Issue_date,Issue_to,Remarks, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], DriverName, VehicleNo from gatepassmastertable LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Issue_No Order By Issue_date DESC"
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Delete(ByVal GatePass As ReturnAbleGatePassMaster) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String
            'Delete Gate Pass Detail
            str = "Delete From GatePassDetailTable WHERE Issue_Id=" & GatePass.Issue_id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Gate Pass Master Infomation
            str = "Delete From GatePassMasterTable WHERE Issue_Id=" & GatePass.Issue_id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
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
