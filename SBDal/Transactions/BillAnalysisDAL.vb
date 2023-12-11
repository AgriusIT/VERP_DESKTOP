Imports SBDal
Imports SBUtility.Utility
Imports SBModel
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class BillAnalysisDAL

    Public Function Save(ByVal BillAnalysis As BillAnalysisMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        BillAnalysis.DocNo = GetNextDocNo("SI-" & BillAnalysis.DocDate.ToString("yy") & "-", trans)
        Try

            Dim strSQL As String = "INSERT INTO tblBillAnalysisMaster(DocNo,DocDate, OGPNo, OGPDate, LotNo, CustomerId, Note, Rate_1, Rate_2, Rate_3,Unit, Total_Qty,Total_Amount,CompanyId, UserName) " _
            & " VALUES(N'" & BillAnalysis.DocNo & "', N'" & BillAnalysis.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & BillAnalysis.OGPNo & "', N'" & BillAnalysis.OGPDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & BillAnalysis.LotNo.Replace("'", "''") & "', " & BillAnalysis.CustomerId & ", N'" & BillAnalysis.Note.Replace("'", "''") & "', " & BillAnalysis.Rate_1 & "," & BillAnalysis.Rate_2 & "," & BillAnalysis.Rate_3 & ",N'" & BillAnalysis.Unit.Replace("'", "''") & "'," & BillAnalysis.Total_Qty & ", " & BillAnalysis.Total_Amount & ", " & BillAnalysis.CompanyId & ", N'" & BillAnalysis.UserName & "') Select @@Identity"
            BillAnalysis.DocId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            SaveDetail(BillAnalysis, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function SaveDetail(ByVal BillAnalysis As BillAnalysisMasterBE, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each BillDt As BillAnalaysisDetailBE In BillAnalysis.BillAnalysisDetail
                strSQL = String.Empty
                strSQL = "INSERT INTO tblBillAnalysisDetail(DocId,LocationId, ArticleDefId, Fabric_Detail, DesignNo, ArticleSize, Stitches, Sequins, Tilla, PackQty, Qty, Rate) " _
                & " VALUES(" & BillAnalysis.DocId & ", " & BillDt.LocationId & ", " & BillDt.ArticleDefId & ", N'" & BillDt.Fabric.Replace("'", "''") & "', N'" & BillDt.DesignNo.Replace("'", "''") & "', N'" & BillDt.ArticleSize.Replace("'", "''") & "', " & BillDt.Stitches & ", " & BillDt.Sequins & ", " & BillDt.Tilla & ", " & BillDt.PackQty & "," & BillDt.Qty & ", " & BillDt.Rate & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function Update(ByVal BillAnalysis As BillAnalysisMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = "UPDATE tblBillAnalysisMaster SET DocNo=N'" & BillAnalysis.DocNo & "',DocDate=N'" & BillAnalysis.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', OGPNo=N'" & BillAnalysis.OGPNo & "', OGPDate=N'" & BillAnalysis.OGPDate.ToString("yyyy-M-d h:mm:ss tt") & "', LotNo=N'" & BillAnalysis.LotNo.Replace("'", "''") & "', CustomerId=" & BillAnalysis.CustomerId & ", Note=N'" & BillAnalysis.Note.Replace("'", "''") & "', Rate_1=" & BillAnalysis.Rate_1 & ", Rate_2=" & BillAnalysis.Rate_2 & ", Rate_3=" & BillAnalysis.Rate_3 & ", Unit=N'" & BillAnalysis.Unit.Replace("'", "''") & "', Total_Qty=" & BillAnalysis.Total_Qty & ", Total_Amount=" & BillAnalysis.Total_Amount & ", CompanyId=" & BillAnalysis.CompanyId & ",  UserName=N'" & BillAnalysis.UserName.Replace("'", "''") & "' WHERE DocId=" & BillAnalysis.DocId & " "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "Delete From tblBillAnalysisDetail WHERE DocId=" & BillAnalysis.DocId & " "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            SaveDetail(BillAnalysis, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal BillAnalysis As BillAnalysisMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = "Delete From tblBillAnalysisMaster WHERE DocId=" & BillAnalysis.DocId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "Delete From tblBillAnalysisDetail WHERE DocId=" & BillAnalysis.DocId & ""
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

    Public Shared Function GetNextDocNo(ByVal FirstChar As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            Return UtilityDAL.GetSerialNo(FirstChar, "tblBillAnalysisMaster", "DocNo", trans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DisplayDetail(ByVal ReceivingId As Integer) As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT  Isnull(tblBillAnalysisDetail.LocationId,0) as LocationId, dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.tblBillAnalysisDetail.Fabric_Detail, " _
                     & " dbo.tblBillAnalysisDetail.DesignNo, ISNULL(dbo.tblBillAnalysisDetail.Stitches, 0) AS Stitches, ISNULL(dbo.tblBillAnalysisDetail.Sequins, 0) AS Sequins," _
                     & " ISNULL(dbo.tblBillAnalysisDetail.Tilla, 0) AS Tilla, ISNULL(dbo.tblBillAnalysisDetail.PackQty, 0) AS PackQty, ISNULL(dbo.tblBillAnalysisDetail.Qty, 0) AS Qty, " _
                     & " ISNULL(dbo.tblBillAnalysisDetail.Rate, 0) AS Rate, Convert(float,0) as Total " _
                     & " FROM dbo.tblBillAnalysisDetail LEFT OUTER JOIN " _
                     & " dbo.ArticleDefView ON dbo.tblBillAnalysisDetail.ArticleDefId = dbo.ArticleDefView.ArticleId WHERE tblBillAnalysisDetail.DocId=" & ReceivingId & ""
            Return UtilityDAL.GetDataTable(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DetailRecords(Optional ByVal Condition As String = "") As DataTable
        Try
            Dim strSQL As String = String.Empty
            'strSQL = "SELECT  " & IIf(Condition = "All", "", "TOP 50") & " dbo.tblBillAnalysisMaster.DocId, dbo.tblBillAnalysisMaster.DocNo, dbo.tblBillAnalysisMaster.DocDate, dbo.tblBillAnalysisMaster.OGPNo, " _
            '     & "  dbo.tblBillAnalysisMaster.OGPDate, dbo.tblBillAnalysisMaster.LotNo, dbo.tblBillAnalysisMaster.CustomerId, dbo.vwCOADetail.detail_title,  " _
            '     & "  dbo.tblBillAnalysisMaster.Note, ISNULL(dbo.tblBillAnalysisMaster.Rate_1, 0) AS Rate_1, ISNULL(dbo.tblBillAnalysisMaster.Rate_2, 0) AS Rate_2, " _
            '     & "  ISNULL(dbo.tblBillAnalysisMaster.Rate_3, 3) AS Rate_3, tblBillAnalysisMaster.Unit, ISNULL(dbo.tblBillAnalysisMaster.Total_Qty, 0) AS Total_Qty,   " _
            '     & "  ISNULL(dbo.tblBillAnalysisMaster.Total_Amount, 0) AS Total_Amount, Isnull(tblBillAnalysisMaster.CompanyId,0) as CompanyId " _
            '     & "  FROM dbo.tblBillAnalysisMaster INNER JOIN " _
            '     & "  dbo.vwCOADetail ON dbo.tblBillAnalysisMaster.CustomerId = dbo.vwCOADetail.coa_detail_id  " _
            '     & "  ORDER BY dbo.tblBillAnalysisMaster.DocNo DESC "


            strSQL = "SELECT  " & IIf(Condition = "All", "", "TOP 50") & " dbo.tblBillAnalysisMaster.DocId, dbo.tblBillAnalysisMaster.DocNo, dbo.tblBillAnalysisMaster.DocDate, dbo.tblBillAnalysisMaster.OGPNo, " _
                            & "  dbo.tblBillAnalysisMaster.OGPDate, dbo.tblBillAnalysisMaster.LotNo, dbo.tblBillAnalysisMaster.CustomerId, dbo.vwCOADetail.detail_title, BilDt.DesignNo,  " _
                            & "  dbo.tblBillAnalysisMaster.Note, ISNULL(dbo.tblBillAnalysisMaster.Rate_1, 0) AS Rate_1, ISNULL(dbo.tblBillAnalysisMaster.Rate_2, 0) AS Rate_2, " _
                            & "  ISNULL(dbo.tblBillAnalysisMaster.Rate_3, 3) AS Rate_3, tblBillAnalysisMaster.Unit, ISNULL(dbo.tblBillAnalysisMaster.Total_Qty, 0) AS Total_Qty,   " _
                            & "  ISNULL(dbo.tblBillAnalysisMaster.Total_Amount, 0) AS Total_Amount, Isnull(tblBillAnalysisMaster.CompanyId,0) as CompanyId " _
                            & "  FROM dbo.tblBillAnalysisMaster INNER JOIN " _
                            & "  dbo.vwCOADetail ON dbo.tblBillAnalysisMaster.CustomerId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select Max(DocDetailId) as DocDetailId, DesignNo,DocId From dbo.tblBillAnalysisDetail  Group by DocId,DesignNo) BilDt on BilDt.DocId = tblBillAnalysisMaster.DocId    " _
                            & "  ORDER BY dbo.tblBillAnalysisMaster.DocNo DESC "


            Return UtilityDAL.GetDataTable(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
