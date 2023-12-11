''8-May-2014 TASK:2614 Imran Ali Delete Record Production Analysis
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class GrdProductionAnalysisDAL

    Public Function Save(ByVal plist As List(Of GrdProductionAlnalysisBE)) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim str As String = String.Empty
            'str = "Delete From tblProductionAnalysis Where AnalysisDate = N'" & plist(0).AnalysisDate & "' "
            str = "Delete From tblProductionAnalysis Where (Convert(Varchar, AnalysisDate,102) = Convert(DateTime, N'" & plist(0).AnalysisDate.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(plist(0).ProjectId > 0, " AND ProjectId = N'" & plist(0).ProjectId & "'", "") & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            For Each pa As GrdProductionAlnalysisBE In plist
                str = "Insert into tblProductionAnalysis(AnalysisDate,ProjectId,ArticleDefId,PackQty,Demand,CurrentStock,Production,Estimate,Batch) values (N'" & pa.AnalysisDate.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & pa.ProjectId & "',N'" & pa.ArticleDefId & "',N'" & pa.PackQty & "',N'" & pa.Demand & "',N'" & pa.CurrentStock & "',N'" & pa.Production & "',N'" & pa.Estimate & "',N'" & pa.Batch & "'  )"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    ''8-May-2014 TASK:2614 Imran Ali Delete Record Production Analysis
    Public Function Delete(ByVal plist As List(Of GrdProductionAlnalysisBE)) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Delete From tblProductionAnalysis Where (Convert(Varchar, AnalysisDate,102) = Convert(DateTime, N'" & plist(0).AnalysisDate.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(plist(0).ProjectId > 0, " AND ProjectId = N'" & plist(0).ProjectId & "'", "") & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
