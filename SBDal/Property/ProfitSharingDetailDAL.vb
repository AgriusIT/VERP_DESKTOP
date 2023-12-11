Imports SBModel
Imports System.Data.SqlClient

Public Class ProfitSharingDetailDAL
    Function Add(ByVal objModel As ProfitSharingDetailBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            'Add(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function Add(ByVal Detail As List(Of ProfitSharingDetailBE), ByVal ProfitSharingId As Integer, trans As SqlTransaction) As Boolean
        Try
            For Each Obj As ProfitSharingDetailBE In Detail
                Dim strSQL As String = String.Empty
                If Obj.ProfitSharingDetailId = 0 Then
                    strSQL = "INSERT INTO  ProfitSharingDetail(ProfitSharingId, InvestorId, AdjustmentAmount, NetProfitAmount, InvestmentBookingId, InvestmentAccountId, ProfitExpenseAccountId) values (" & ProfitSharingId & ", " & Obj.InvestorId & ", " & Obj.AdjustmentAmount & ", " & Obj.NetProfitAmount & ", " & Obj.InvestmentBookingId & ", " & Obj.InvestmentAccountId & ", " & Obj.ProfitExpenseAccountId & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                Else
                    strSQL = "UPDATE ProfitSharingDetail SET ProfitSharingId = " & ProfitSharingId & ", InvestorId = " & Obj.InvestorId & ", AdjustmentAmount = " & Obj.AdjustmentAmount & ", NetProfitAmount = " & Obj.NetProfitAmount & ", InvestmentBookingId = " & Obj.InvestmentBookingId & " , InvestmentAccountId = " & Obj.InvestmentAccountId & ", ProfitExpenseAccountId = " & Obj.ProfitExpenseAccountId & " WHERE ProfitSharingDetailId = " & Obj.ProfitSharingDetailId & "  "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As ProfitSharingDetailBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As ProfitSharingDetailBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update ProfitSharingDetail set ProfitSharingId= N'" & objModel.ProfitSharingId & "', InvestorId= N'" & objModel.InvestorId & "', AdjustmentAmount= N'" & objModel.AdjustmentAmount & "', NetProfitAmount= N'" & objModel.NetProfitAmount & "' where ProfitSharingDetailId=" & objModel.ProfitSharingDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As ProfitSharingDetailBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal objModel As ProfitSharingDetailBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ProfitSharingDetail  where ProfitSharingDetailId= " & objModel.ProfitSharingDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ProfitSharingDetailId, ProfitSharingId, InvestorId, AdjustmentAmount, NetProfitAmount from ProfitSharingDetail  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ProfitSharingDetailId, ProfitSharingId, InvestorId, AdjustmentAmount, NetProfitAmount from ProfitSharingDetail  where ProfitSharingDetailId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetPropertyProfile(ByVal PropertyProfileId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        'Shared ProfitSharingDetailId As String = "ProfitSharingDetailId"
        'Shared ProfitSharingId As String = "ProfitSharingId"
        'Shared InvestorId As String = "InvestorId"
        'Shared InvestorName As String = "InvestorName"
        'Shared Cell As String = "Cell"
        'Shared InvPer As String = "Inv"
        'Shared ProfitPortion As String = "ProfitPortion"
        'Shared ProfitPer As String = "Profit"
        'Shared ProfitShare As String = "ProfitShare"
        'Shared Adjustment As String = "AdjustmentAmount"
        'Shared NetProfit As String = "NetProfitAmount"
        Try
            'Saba Shabbir TFS2560 to display profit shring investor name 

            ''Below line is commented on 7-11-2018
            strSQL = " SELECT 0 AS ProfitSharingDetailId, 0 AS ProfitSharingId, ISNULL(vwCOADetail.coa_detail_id, 0) AS InvestorId, 0 AS Inv, ISNULL(InvestmentBooking.InvestmentAmount, 0) As InvestmentAmount,0 AS ProfitPortion, ISNULL(InvestmentBooking.ProfitPercentage, 0)  AS Profit, 0 AS ProfitShare, 0 AS AdjustmentAmount, 0 AS NetProfitAmount, vwCOADetail.detail_title as InvestorName, ISNULL(InvestmentBooking.InvestmentBookingId, 0) AS InvestmentBookingId  FROM vwCOADetail RIGHT OUTER JOIN InvestmentBooking ON vwCOADetail.coa_detail_id = InvestmentBooking.InvestorId RIGHT OUTER JOIN PropertyProfile ON InvestmentBooking.PropertyProfileId = PropertyProfile.PropertyProfileId WHERE PropertyProfile.PropertyProfileId = " & PropertyProfileId & " "

            '& " ELSE Select 0 AS ProfitSharingDetailId, 0 AS ProfitSharingId, IsNull(PropertyProfile.InvId, 0) AS InvestorId, Investor.Name AS InvestorName, " _
            '& " Investor.PrimaryMobile AS Cell, 0 AS Inv, IsNull(InvestmentBooking.InvestmentAmount, 0) AS ProfitPortion, IsNull(InvestmentBooking.ProfitPercentage, 0) AS Profit, 0 AS ProfitShare, " _
            '& " 0 AS  AdjustmentAmount, " _
            '& " 0 AS NetProfitAmount from PropertyProfile LEFT OUTER JOIN InvestmentBooking ON PropertyProfile.PropertyProfileId = InvestmentBooking.PropertyProfileId " _
            '& " LEFT OUTER JOIN Investor ON " _
            '& " PropertyProfile.InvId = Investor.InvestorId Where PropertyProfile.PropertyProfileId = " & PropertyProfileId & " "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            'dt.Columns("ProfitShare").Expression = "ProfitPortion*Profit/100"
            'dt.Columns("NetProfitAmount").Expression = "ProfitShare+AdjustmentAmount"
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function IsPropertyProfileExisting(ByVal PropertyProfileId As Integer) As Boolean
        Try
            Dim _String As String = "SELECT Count(*) AS Records FROM ProfitSharingMaster WHERE PropertyProfileId =" & PropertyProfileId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(_String)
            If Not dt Is Nothing Then
                If dt.Rows(0).Item(0) > 0 Then
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
    Public Shared Function GetProfitSharingDetail(ByVal PropertyProfileId As Integer, Optional ByVal DecimalPointsInValues As Integer = 0I) As DataTable
        Dim strSQL As String = String.Empty
        Try
            ''Below line is commented on 07-11-2018 against TASK TFS
            'strSQL = " SELECT ProfitSharingDetail.ProfitSharingDetailId, ProfitSharingDetail.ProfitSharingId, InvestmentBooking.InvestorId AS InvestorId, vwCOADetail.detail_title as InvestorName, Convert(Decimal(18, " & DecimalPointsInValues & "), 0) AS Inv, Convert(Decimal(18, " & DecimalPointsInValues & "), ISNULL(InvestmentBooking.InvestmentAmount, 0)) AS InvestmentAmount, 0 AS ProfitPortion,  ISNULL(InvestmentBooking.ProfitPercentage, 0) AS Profit, 0 AS ProfitShare, ISNULL(ProfitSharingDetail.AdjustmentAmount, 0) AS AdjustmentAmount, ISNULL(ProfitSharingDetail.NetProfitAmount, 0) AS NetProfitAmount FROM vwCOADetail RIGHT OUTER JOIN InvestmentBooking ON vwCOADetail.coa_detail_id = InvestmentBooking.InvestorId RIGHT OUTER JOIN ProfitSharingMaster INNER JOIN ProfitSharingDetail ON ProfitSharingMaster.ProfitSharingId = ProfitSharingDetail.ProfitSharingId LEFT OUTER JOIN PropertyProfile ON ProfitSharingMaster.PropertyProfileId = PropertyProfile.PropertyProfileId ON InvestmentBooking.PropertyProfileId = PropertyProfile.PropertyProfileId  Where ProfitSharingMaster.PropertyProfileId = " & PropertyProfileId & " order by InvestmentBooking.InvestorId ASC " _
            strSQL = " SELECT ProfitSharingDetail.ProfitSharingDetailId, ProfitSharingDetail.ProfitSharingId, InvestmentBooking.InvestorId AS InvestorId, vwCOADetail.detail_title as InvestorName, Convert(Decimal(18, " & DecimalPointsInValues & "), 0) AS Inv, Convert(Decimal(18, " & DecimalPointsInValues & "), ISNULL(InvestmentBooking.InvestmentAmount, 0)) AS InvestmentAmount, 0 AS ProfitPortion,  ISNULL(InvestmentBooking.ProfitPercentage, 0) AS Profit, 0 AS ProfitShare, ISNULL(ProfitSharingDetail.AdjustmentAmount, 0) AS AdjustmentAmount, ISNULL(ProfitSharingDetail.NetProfitAmount, 0) AS NetProfitAmount, ISNULL(ProfitSharingDetail.InvestmentBookingId, 0) AS InvestmentBookingId  FROM  ProfitSharingDetail INNER JOIN ProfitSharingMaster ON ProfitSharingMaster.ProfitSharingId = ProfitSharingDetail.ProfitSharingId LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = ProfitSharingDetail.InvestorId LEFT OUTER JOIN InvestmentBooking ON InvestmentBooking.InvestmentBookingId = ProfitSharingDetail.InvestmentBookingId  WHERE ProfitSharingMaster.PropertyProfileId = " & PropertyProfileId & "  " _
                     & " UNION ALL " _
                     & " SELECT 0 AS ProfitSharingDetailId, 0 AS ProfitSharingId, ISNULL(vwCOADetail.coa_detail_id, 0) AS InvestorId, vwCOADetail.detail_title as InvestorName, 0 AS Inv, ISNULL(InvestmentBooking.InvestmentAmount, 0) As InvestmentAmount,0 AS ProfitPortion, ISNULL(InvestmentBooking.ProfitPercentage, 0)  AS Profit, 0 AS ProfitShare, 0 AS AdjustmentAmount, 0 AS NetProfitAmount, ISNULL(InvestmentBooking.InvestmentBookingId, 0) AS InvestmentBookingId  FROM InvestmentBooking LEFT OUTER JOIN vwCOADetail ON vwCOADetail.coa_detail_id = InvestmentBooking.InvestorId WHERE InvestmentBooking.PropertyProfileId = " & PropertyProfileId & " AND InvestmentBooking.InvestmentBookingId NOT IN (SELECT ISNULL(InvestmentBookingId, 0) AS InvestmentBookingId FROM ProfitSharingDetail INNER JOIN ProfitSharingMaster ON ProfitSharingDetail.ProfitSharingId = ProfitSharingMaster.ProfitSharingId WHERE ProfitSharingMaster.PropertyProfileId = " & PropertyProfileId & " ) ORDER BY InvestorId ASC"
            '& " Investor.PrimaryMobile AS Cell, 0 AS Inv, IsNull(InvestmentBooking.InvestmentAmount, 0) AS ProfitPortion, IsNull(InvestmentBooking.ProfitPercentage, 0) AS Profit, 0 AS ProfitShare, " _
            '& " ISNULL(ProfitSharingDetail.AdjustmentAmount, 0) AS AdjustmentAmount, " _
            '& " IsNull(ProfitSharingDetail.NetProfitAmount, 0) AS NetProfitAmount from ProfitSharingMaster INNER JOIN ProfitSharingDetail ON  ProfitSharingMaster.ProfitSharingId = ProfitSharingDetail.ProfitSharingId " _
            '& " LEFT OUTER JOIN PropertyProfile ON ProfitSharingMaster.PropertyProfileId = PropertyProfile.PropertyProfileId LEFT OUTER JOIN InvestmentBooking ON PropertyProfile.PropertyProfileId = InvestmentBooking.PropertyProfileId " _
            '& " LEFT OUTER JOIN Investor ON " _
            '& " PropertyProfile.InvId = Investor.InvestorId"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            dt.Columns("ProfitShare").Expression = "ProfitPortion*Profit/100"
            dt.Columns("NetProfitAmount").Expression = "ProfitShare+AdjustmentAmount"
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetProfitSharing(ByVal PropertyProfileId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT ProfitSharingId, PropertyProfileId, SharingDate, voucher_no, ProfitForDistribution FROM ProfitSharingMaster WHERE PropertyProfileId =" & PropertyProfileId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetPropertyProfileNetProfit(ByVal PropertyProfileId As Integer, Optional ByVal DecimalPointsInValues As Integer = 0I) As Double
        Dim strSQL As String = String.Empty
        Try
            strSQL = " SELECT  SUM(ISNULL(ProfitSharingDetail.NetProfitAmount, 0)) AS NetProfitAmount FROM ProfitSharingMaster INNER JOIN ProfitSharingDetail ON ProfitSharingMaster.ProfitSharingId = ProfitSharingDetail.ProfitSharingId LEFT OUTER JOIN PropertyProfile ON ProfitSharingMaster.PropertyProfileId = PropertyProfile.PropertyProfileId  Where ProfitSharingMaster.PropertyProfileId = " & PropertyProfileId & ""
            '& " Investor.PrimaryMobile AS Cell, 0 AS Inv, IsNull(InvestmentBooking.InvestmentAmount, 0) AS ProfitPortion, IsNull(InvestmentBooking.ProfitPercentage, 0) AS Profit, 0 AS ProfitShare, " _
            '& " ISNULL(ProfitSharingDetail.AdjustmentAmount, 0) AS AdjustmentAmount, " _
            '& " IsNull(ProfitSharingDetail.NetProfitAmount, 0) AS NetProfitAmount from ProfitSharingMaster INNER JOIN ProfitSharingDetail ON  ProfitSharingMaster.ProfitSharingId = ProfitSharingDetail.ProfitSharingId " _
            '& " LEFT OUTER JOIN PropertyProfile ON ProfitSharingMaster.PropertyProfileId = PropertyProfile.PropertyProfileId LEFT OUTER JOIN InvestmentBooking ON PropertyProfile.PropertyProfileId = InvestmentBooking.PropertyProfileId " _
            '& " LEFT OUTER JOIN Investor ON " _
            '& " PropertyProfile.InvId = Investor.InvestorId"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows(0).Item(0).ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class