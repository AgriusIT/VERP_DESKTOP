Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class UpdateReturnableGatepassDAL
    Dim id As Integer
    Public Function Update(ByVal ReturnableGatepass As ReturnAbleGatePassDetail) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Update GatepassDetailTable Set ReceivedDate='" & ReturnableGatepass.RecivingDate.ToString("yyyy-M-d h:mm:ss tt") & "' ,Comments='" & ReturnableGatepass.Comments.ToString & "' WHERE IssueDetailID=" & ReturnableGatepass.IssueDetailID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'str = "INSERT INTO PartialInMasterTable( DocNo, ReceivingDate) VALUES(N'" &
            '   ReturnableGatepass.DocNo & "','" & ReturnableGatepass.RecivingDate.ToString("yyyy-M-d h:mm:ss tt") & "') Select @@Identity"
            'id = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAllRecord(Optional ByVal Condition As String = "") As DataTable
        Try
            Dim str As String = String.Empty
            If Condition = "Pending" Then

                ''Aashi: 4268: Record not filtering correctly
                ''Start
                str = "SELECT GatepassDetailTable.IssueDetailID, GatepassMasterTable.Issue_id, GatepassMasterTable.Issue_No, GatepassMasterTable.Issue_date, GatepassMasterTable.Issue_to, " _
                                            & "  GatepassMasterTable.Remarks, GatepassDetailTable.IssueDetail,  Case When GatePassDetailTable.ReceivedDate Is Null then 'Pending' else 'Received' end  as Status, GatepassDetailTable.ReceivedDate, GatepassDetailTable.Comments" _
                                            & "  FROM GatepassMasterTable INNER JOIN GatepassDetailTable ON GatepassMasterTable.Issue_id = GatepassDetailTable.Issue_Id "
                str += " WHERE (Convert(varchar, GatepassDetailTable.ReceivedDate,102) Is Null)"

                'str = "SELECT Distinct OutTable.IssueDetailID, OutTable.Issue_id, OutTable.Issue_No, OutTable.Issue_date, OutTable.Issue_to, OutTable.Remarks, OutTable.IssueDetail, CASE WHEN ISNULL(OutTable.IssueQty, 0) - ISNULL(InTable.InQty, 0) > 0 " _
                '            & "  THEN 'Pending' ELSE 'Received' END AS Status, ReceivedDate, OutTable.Comments  " _
                '            & "  FROM (SELECT GatepassMasterTable.Issue_id, GatepassMasterTable.Issue_No, GatepassMasterTable.Issue_date, GatepassMasterTable.Issue_to,  " _
                '            & "  GatepassDetailTable.IssueDetail, GatepassDetailTable.IssueQty, GatepassDetailTable.Comments, GatepassDetailTable.IssueDetailID,  " _
                '            & "  GatepassDetailTable.ReceivedDate, GatepassMasterTable.Remarks, GatepassDetailTable.SrNo " _
                '            & "  FROM GatepassMasterTable INNER JOIN " _
                '            & "  GatepassDetailTable ON GatepassMasterTable.Issue_id = GatepassDetailTable.Issue_Id) AS OutTable LEFT OUTER JOIN " _
                '            & "  (SELECT outDetailSrNo, outMasterId, SUM(InQuantity) AS InQty " _
                '            & "  FROM PartialInDetailTable " _
                '            & "  GROUP BY outDetailSrNo, outMasterId) AS InTable ON OutTable.Issue_id = InTable.outMasterId AND OutTable.SrNo = InTable.outDetailSrNo "
                'str += "where ISNULL(OutTable.IssueQty, 0) - ISNULL(InTable.InQty, 0) > 0"

            ElseIf Condition = "Received" Then
                str = "SELECT GatepassDetailTable.IssueDetailID, GatepassMasterTable.Issue_id, GatepassMasterTable.Issue_No, GatepassMasterTable.Issue_date, GatepassMasterTable.Issue_to, " _
                            & "  GatepassMasterTable.Remarks, GatepassDetailTable.IssueDetail, Case When GatePassDetailTable.ReceivedDate Is Null then 'Pending' else 'Received' end  as Status,  GatepassDetailTable.ReceivedDate, GatepassDetailTable.Comments " _
                            & "  FROM GatepassMasterTable INNER JOIN GatepassDetailTable ON GatepassMasterTable.Issue_id = GatepassDetailTable.Issue_Id "
                str += " WHERE (Convert(varchar, GatepassDetailTable.ReceivedDate,102) Is Not Null)"

                'str = "SELECT Distinct OutTable.IssueDetailID, OutTable.Issue_id, OutTable.Issue_No, OutTable.Issue_date, OutTable.Issue_to, OutTable.Remarks, OutTable.IssueDetail, CASE WHEN ISNULL(OutTable.IssueQty, 0) - ISNULL(InTable.InQty, 0) > 0 " _
                '            & "  THEN 'Pending' ELSE 'Received' END AS Status, ReceivedDate, OutTable.Comments  " _
                '            & "  FROM (SELECT GatepassMasterTable.Issue_id, GatepassMasterTable.Issue_No, GatepassMasterTable.Issue_date, GatepassMasterTable.Issue_to,  " _
                '            & "  GatepassDetailTable.IssueDetail, GatepassDetailTable.IssueQty, GatepassDetailTable.Comments, GatepassDetailTable.IssueDetailID,  " _
                '            & "  GatepassDetailTable.ReceivedDate, GatepassMasterTable.Remarks, GatepassDetailTable.SrNo " _
                '            & "  FROM GatepassMasterTable INNER JOIN " _
                '            & "  GatepassDetailTable ON GatepassMasterTable.Issue_id = GatepassDetailTable.Issue_Id) AS OutTable LEFT OUTER JOIN " _
                '            & "  (SELECT outDetailSrNo, outMasterId, SUM(InQuantity) AS InQty " _
                '            & "  FROM PartialInDetailTable " _
                '            & "  GROUP BY outDetailSrNo, outMasterId) AS InTable ON OutTable.Issue_id = InTable.outMasterId AND OutTable.SrNo = InTable.outDetailSrNo "
                'str += "where ISNULL(OutTable.IssueQty, 0) - ISNULL(InTable.InQty, 0) = 0"
                ''End 4269


            ElseIf Condition = "All" Then
                str = "SELECT GatepassDetailTable.IssueDetailID, GatepassMasterTable.Issue_id, GatepassMasterTable.Issue_No, GatepassMasterTable.Issue_date, GatepassMasterTable.Issue_to, " _
              & "  GatepassMasterTable.Remarks, GatepassDetailTable.IssueDetail, Case When GatePassDetailTable.ReceivedDate Is Null then 'Pending' else 'Received' end  as Status,GatepassDetailTable.ReceivedDate, GatepassDetailTable.Comments" _
              & "  FROM GatepassMasterTable INNER JOIN GatepassDetailTable ON GatepassMasterTable.Issue_id = GatepassDetailTable.Issue_Id "
                'str = "SELECT Distinct OutTable.IssueDetailID, OutTable.Issue_id, OutTable.Issue_No, OutTable.Issue_date, OutTable.Issue_to, OutTable.Remarks, OutTable.IssueDetail, CASE WHEN ISNULL(OutTable.IssueQty, 0) - ISNULL(InTable.InQty, 0) > 0 " _
                '            & "  THEN 'Pending' ELSE 'Received' END AS Status, ReceivedDate, OutTable.Comments  " _
                '            & "  FROM (SELECT GatepassMasterTable.Issue_id, GatepassMasterTable.Issue_No, GatepassMasterTable.Issue_date, GatepassMasterTable.Issue_to,  " _
                '            & "  GatepassDetailTable.IssueDetail, GatepassDetailTable.IssueQty, GatepassDetailTable.Comments, GatepassDetailTable.IssueDetailID,  " _
                '            & "  GatepassDetailTable.ReceivedDate, GatepassMasterTable.Remarks, GatepassDetailTable.SrNo " _
                '            & "  FROM GatepassMasterTable INNER JOIN " _
                '            & "  GatepassDetailTable ON GatepassMasterTable.Issue_id = GatepassDetailTable.Issue_Id) AS OutTable LEFT OUTER JOIN " _
                '            & "  (SELECT outDetailSrNo, outMasterId, SUM(InQuantity) AS InQty " _
                '            & "  FROM PartialInDetailTable " _
                '            & "  GROUP BY outDetailSrNo, outMasterId) AS InTable ON OutTable.Issue_id = InTable.outMasterId AND OutTable.SrNo = InTable.outDetailSrNo "

            End If
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
