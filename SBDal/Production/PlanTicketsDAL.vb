Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Imports SBDal.UtilityDAL
Public Class PlanTicketsDAL
    Public Sub Save(ByVal PlanTicketsMaster As PlanTicketsMaster)
        'Dim objTrans As SqlTransaction = CType(obj, SqlTransaction)

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open() ' Connection Open
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            'PlanTicketsMasterID()
            'TicketNo()
            'TicketDate()
            'CustomerID()
            'SalesOrderID()
            'PlanID()
            'SpecialInstructions()
            Dim str As String = String.Empty
            str = "Insert Into PlanTicketsMaster(TicketNo, TicketDate, CustomerID, SalesOrderID, PlanID, SpecialInstructions) " _
            & " Values(N'" & PlanTicketsMaster.TicketNo & "', N'" & PlanTicketsMaster.TicketDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & PlanTicketsMaster.CustomerID & ", " & PlanTicketsMaster.SalesOrderID & ", " & PlanTicketsMaster.PlanID & ", N'" & PlanTicketsMaster.SpecialInstructions.Replace("'", "''") & "') Select @@Identity "

            PlanTicketsMaster.PlanTicketsMasterID = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            'Insert Stock Detail Information 
            SaveDetail(PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.Detail, trans)
            'Trans Commit here... 
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub
    Public Sub Update(ByVal PlanTicketsMaster As PlanTicketsMaster)
        'Dim objTrans As SqlTransaction = CType(obj, SqlTransaction)

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open() ' Connection Open
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            'PlanTicketsMasterID()
            'TicketNo()
            'TicketDate()
            'CustomerID()
            'SalesOrderID()
            'PlanID()
            'SpecialInstructions()
            Dim str As String = String.Empty
            str = "Update PlanTicketsMaster Set TicketNo = N'" & PlanTicketsMaster.TicketNo & "', TicketDate = N'" & PlanTicketsMaster.TicketDate.ToString("yyyy-M-d h:mm:ss tt") & "' , CustomerID = " & PlanTicketsMaster.CustomerID & ", SalesOrderID = " & PlanTicketsMaster.SalesOrderID & ", PlanID = " & PlanTicketsMaster.PlanID & ", SpecialInstructions = N'" & PlanTicketsMaster.SpecialInstructions.Replace("'", "''") & "' Where PlanTicketsMasterID = " & PlanTicketsMaster.PlanTicketsMasterID & " "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            For Each Detail As PlanTicketsDetail In PlanTicketsMaster.Detail
                str = ""
                str = "If Exists(Select IsNull(PlanTicketsDetailID, 0) As PlanTicketsDetailID From PlanTicketsDetail Where PlanTicketsDetailID = " & Detail.PlanTicketsDetailID & ") UPDATE PlanDetailTable SET TicketIssuedQty = IsNull(TicketIssuedQty, 0) - " & Detail.Quantity & " Where PlanDetailId = " & Detail.PlanDetailId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next

            str = String.Empty
            str = "Delete From PlanTicketsDetail Where PlanTicketsMasterID = " & PlanTicketsMaster.PlanTicketsMasterID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            SaveDetail(PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.Detail, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub
    Public Sub Delete(ByVal PlanTicketsMasterID As Integer)

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Delete From PlanTicketsMaster Where PlanTicketsMasterID = " & PlanTicketsMasterID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            DeleteDetail(PlanTicketsMasterID, trans)
            'trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub
    Public Sub DeleteDetail(ByVal PlanTicketsMasterID As Integer, ByVal trans As SqlTransaction)
        Dim dt As New DataTable
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Try

            Dim str As String = String.Empty
            str = "Select PlanDetailId, IsNull(Quantity, 0) As Quantity From PlanTicketsDetail Where PlanTicketsMasterID = " & PlanTicketsMasterID & ""
            dt = UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            For Each dr As DataRow In dt.Rows
                SubtractQty(Val(dr.Item(0).ToString), Val(dr.Item(1).ToString))
            Next
            str = ""
            str = "Delete FROM PlanTicketsDetail Where PlanTicketsMasterID = " & PlanTicketsMasterID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub
    Public Sub SaveDetail(ByVal PlanTecketsMasterID As Integer, ByVal PlanTicketsDetailList As List(Of PlanTicketsDetail), ByVal trans As SqlTransaction)
        Try
            Dim str As String = String.Empty
            For Each PlanTicketsDetail As PlanTicketsDetail In PlanTicketsDetailList
                '//PlanTicketsDetail
                'PlanTicketsDetailID()
                'PlanTicketsMasterID()
                'ArticleId()
                'PlanDetailId()
                'Quantity()
                str = "Insert Into PlanTicketsDetail(PlanTicketsMasterID, ArticleId, PlanDetailId, Quantity) " _
              & " Values (" & PlanTecketsMasterID & ", " & PlanTicketsDetail.ArticleId & ",  " & PlanTicketsDetail.PlanDetailId & ", " & PlanTicketsDetail.Quantity & " )"
                'End Task:M16
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = ""
                str = "UPDATE PlanDetailTable SET TicketIssuedQty = IsNull(TicketIssuedQty, 0) + " & PlanTicketsDetail.Quantity & " Where PlanDetailId = " & PlanTicketsDetail.PlanDetailId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
        End Try
    End Sub
    Public Function GetAll() As DataTable
        Dim dt As New DataTable
        Dim Query As String = String.Empty
        Try
            Query = "SELECT  PlanTicketsMaster.TicketNo, PlanTicketsMaster.TicketDate,  PlanMasterTable.PlanNo, PlanMasterTable.PlanDate, PlanTicketsMaster.PlanTicketsMasterID, SalesOrderMasterTable.SalesOrderNo, SalesOrderMasterTable.SalesOrderDate, " _
            & " PlanTicketsMaster.SpecialInstructions, PlanTicketsMaster.CustomerID, PlanTicketsMaster.PlanID, PlanTicketsMaster.SalesOrderID " _
            & " FROM PlanTicketsMaster LEFT OUTER JOIN " _
            & " SalesOrderMasterTable ON PlanTicketsMaster.SalesOrderID = SalesOrderMasterTable.SalesOrderId LEFT OUTER JOIN " _
            & " PlanMasterTable ON PlanTicketsMaster.PlanId = PlanMasterTable.PlanId " _
            & " ORDER BY PlanTicketsMaster.TicketDate DESC"
            dt = UtilityDAL.GetDataTable(Query)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDetail(ByVal ID As Integer) As DataTable
        Dim dt As New DataTable
        Dim Query As String = String.Empty
        Try
            '//PlanTicketsDetail
            'PlanTicketsDetailID()
            'PlanTicketsMasterID()
            'ArticleId()
            'PlanDetailId()
            'Quantity()
            Query = "Select PlanTicketsDetail.PlanTicketsDetailID, PlanTicketsDetail.PlanTicketsMasterID, PlanTicketsDetail.ArticleId, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription, ArticleUnitDefTable.ArticleUnitName As UnitName, IsNull(PlanTicketsDetail.PlanDetailId, 0) As PlanDetailId, IsNull(PlanTicketsDetail.Quantity, 0) As Quantity From PlanTicketsDetail INNER JOIN ArticleDefTable ON PlanTicketsDetail.ArticleId = ArticleDefTable.ArticleId LEFT JOIN ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Where PlanTicketsDetail.PlanTicketsMasterID = " & ID & " "
            dt = UtilityDAL.GetDataTable(Query)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub SubtractQty(ByVal PlainDetailId As Integer, ByVal Quantity As Double)
        'Dim objTrans As SqlTransaction = CType(obj, SqlTransaction)
        Dim Str As String = String.Empty
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open() ' Connection Open
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Str = ""
            Str = "UPDATE PlanDetailTable SET TicketIssuedQty = IsNull(TicketIssuedQty, 0) - " & Quantity & " Where PlanDetailId = " & PlainDetailId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Str)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub
    Public Sub DeleteDetailRow(ByVal PlanTicketsDetailId As Integer)
        'Dim objTrans As SqlTransaction = CType(obj, SqlTransaction)
        Dim Str As String = String.Empty
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open() ' Connection Open
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Str = ""
            Str = " Delete From PlanTicketsDetail Where PlanTicketsDetailId = " & PlanTicketsDetailId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Str)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub

    Public Function GetDeatilsByTickets(query As String) As DataTable
        Try
            'Dim str As String = String.Empty

            'str = "select PlanTicketsMaster.TicketNo , ArticleDefTable.ArticleDescription as ProductName , tblproSteps.prod_step as Stage, " _
            '      & "PlanTicketMaterialDetail.Qty as PendingQty from PlanTicketMaterialDetail " _
            '      & "INNER JOIN PlanTicketsMaster on " _
            '      & "PlanTicketMaterialDetail.TicketId = PlanTicketsMaster.PlanTicketsMasterID " _
            '      & "INNER JOIN ArticleDefTable on " _
            '      & "ArticleDefTable.ArticleId = PlanTicketMaterialDetail.MaterialArticleId " _
            '      & "INNER JOIN tblproSteps on " _
            '      & "tblproSteps.ProdStep_id = PlanTicketMaterialDetail.DepartmentId " _
            '      & "where TicketId = " & ticketId

            Return GetDataTable(query)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
