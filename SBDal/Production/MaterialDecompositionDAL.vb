Imports SBUtility.Utility
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports SBModel

Public Class MaterialDecompositionDAL
    Private Property table As DataTable

    Public Sub Master_Insertion(ByVal MasterQuery As String, ByVal MasterDetailQuery As List(Of String), ByVal StockMaster As StockMaster, ByVal Obj As MaterialDecompositionModel, ByVal dt As DataTable, ByVal Source As String, ByVal MyCompanyId As Integer, ByVal WastedStockAccount As Integer, ByVal ScrappedStockAccount As Integer)
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim objCommand As New SqlCommand
        'Dim objCon As SqlConnection
        Dim i As Integer
        If Conn.State = ConnectionState.Open Then Conn.Close()

        Conn.Open()
        objCommand.Connection = Conn

        Dim trans As SqlTransaction = Conn.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            objCommand.CommandText = MasterQuery
            Dim MasterID As Integer = objCommand.ExecuteScalar()

            'Dim obj As Object = objCommand.ExecuteNonQuery()
            Master_Detail_Insertion(MasterID, trans, MasterDetailQuery)
            Call New StockDAL().Add(StockMaster, trans)
            AddVoucher(Obj, dt, trans, Source, MyCompanyId, WastedStockAccount, ScrappedStockAccount)
            trans.Commit()

            'InsertVoucher()
        Catch ex As Exception
            trans.Rollback()
        Finally
            Conn.Close()
        End Try

    End Sub

    Public Sub Master_Detail_Insertion(ByVal masterID As Integer, ByVal trans As SqlTransaction, ByVal MasterDetailQuery As List(Of String))
        Dim objCommand As New SqlCommand
        'Dim objCon As SqlConnection
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim i As Integer

        Try
            If Conn.State = ConnectionState.Open Then Conn.Close()
            Conn.Open()
            objCommand.Connection = trans.Connection
            objCommand.Transaction = trans
            For Each item As String In MasterDetailQuery
                objCommand.CommandText = item.ToString().Replace("`", " " & masterID & " ")
                objCommand.ExecuteNonQuery()
            Next
            'trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            'Finally
            '    Conn.Close()
        End Try
    End Sub

    Public Function ReadTable(ByVal query As String) As DataTable
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim command As SqlCommand = New SqlCommand()
        'Dim objCon As SqlConnection
        Dim Adapter As SqlDataAdapter
        Dim table As DataTable

        Dim i As Integer
        Try
            If Conn.State = ConnectionState.Open Then Conn.Close()
            Conn.Open()
            Adapter = New SqlDataAdapter(query, Conn)
            table = New DataTable()
            Adapter.Fill(table)
            Conn.Close()
            Return table
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub Master_Deletion(ByVal DetailQuery As String, ByVal MasterQuery As String, ByVal StockTransId As Integer, Optional ByVal DocumentNo As String = "", Optional ByVal VoucherId As Integer = 0)
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        'Dim connection As SqlConnection
        Dim command As SqlCommand = New SqlCommand()
        If Conn.State = ConnectionState.Open Then Conn.Close()
        Conn.Open()
        Dim transcation As SqlTransaction = Conn.BeginTransaction()
        Try
            command.CommandType = CommandType.Text
            command.Connection = Conn
            command.Transaction = transcation

            command.CommandText = DetailQuery
            command.ExecuteNonQuery()

            command.CommandText = String.Empty
            command.CommandText = MasterQuery
            command.ExecuteNonQuery()
            Dim StockMaster As StockMaster = New StockMaster()
            StockMaster.StockTransId = StockTransId
            Call New StockDAL().Delete(StockMaster, transcation)
            DeleteVoucher(VoucherId, transcation)
            transcation.Commit()
        Catch ex As Exception
            transcation.Rollback()
        Finally
            Conn.Close()
        End Try
    End Sub
    Public Sub Master_Update(ByVal MasterID As Integer, ByVal MasterQuery As String, ByVal MasterDetailQuery As List(Of String), ByVal Deletequery As String, ByVal StockMaster As StockMaster, ByVal Obj As MaterialDecompositionModel, ByVal Source As String, ByVal dt As DataTable, ByVal MyCompanyId As Integer, ByVal WastedStockAccount As Integer, ByVal ScrappedStockAccount As Integer, ByVal VoucherId As Integer)
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim objCommand As New SqlCommand
        'Dim objCon As SqlConnection
        Dim i As Integer
        If Conn.State = ConnectionState.Open Then Conn.Close()
        Conn.Open()
        objCommand.Connection = Conn

        Dim trans As SqlTransaction = Conn.BeginTransaction
        Try
            objCommand.Transaction = trans
            objCommand.CommandText = Deletequery
            objCommand.ExecuteNonQuery()

            objCommand.CommandText = String.Empty
            objCommand.CommandText = MasterQuery
            'Dim MasterID As Integer = objCommand.ExecuteScalar()
            objCommand.ExecuteNonQuery()
            Master_Detail_Insertion(MasterID, trans, MasterDetailQuery)
            Call New StockDAL().Update(StockMaster, trans)

            UpdateVoucher(Obj, dt, trans, Source, MyCompanyId, WastedStockAccount, ScrappedStockAccount, VoucherId)
            trans.Commit()
            'Conn.Close()
            'InsertVoucher()
        Catch ex As Exception
            trans.Rollback()
        Finally
            Conn.Close()
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS1927
    ''' </summary>
    ''' <param name="Obj"></param>
    ''' <param name="Trans"></param>
    ''' <param name="Source"></param>
    ''' <param name="MyCompanyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddVoucher(ByVal Obj As MaterialDecompositionModel, ByVal dt As DataTable, ByVal Trans As SqlTransaction, ByVal Source As String, ByVal MyCompanyId As Integer, ByVal WastedStockAccount As Integer, ByVal ScrappedStockAccount As Integer) As Boolean
        Dim Query As String = ""
        Dim VoucherId As Integer = 0
        Try
            '[ConsumptionDetailId] [int] IDENTITY(1,1) NOT NULL,
            '[ConsumptionId] [int] NULL,
            '[ArticleId] [int] NULL,
            '[Qty] [float] NULL,
            '[Rate] [float] NULL,
            '[DispatchId] [int] NULL,
            '[DispatchDetailId] [int] NULL,

            Query = "  Insert Into tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, cheque_no, cheque_date, post, Source, voucher_code, Remarks) " _
                    & " Values(" & MyCompanyId & ", 1, 1, '" & Obj.DocumentNo.Replace("'", "''") & "', '" & Obj.DecompositionDate.ToString("yyyy-M-d h:mm:ss tt") & "', NULL, NULL, 1, N'" & Source & "', '" & Obj.DocumentNo.Replace("'", "''") & "', N'" & Obj.Remarks.Replace("'", "''") & "') Select @@Identity"
            VoucherId = SQLHelper.ExecuteScaler(Trans, CommandType.Text, Query)
            For Each drRow As DataRow In dt.Rows
                If Val(drRow.Item("DValue").ToString) > 0 Then
                    'Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments
                    Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                      & " Values(" & VoucherId & ", " & drRow.Item("LocationId") & ", " & drRow.Item("SubSubId") & ", " & Val(drRow.Item("DValue").ToString) & ", 0, '')"
                    SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                    Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                     & " Values(" & VoucherId & ", " & drRow.Item("LocationId") & ", " & drRow.Item("PlanItemSubSubId") & ", 0, " & Val(drRow.Item("DValue").ToString) & ", '')"
                    SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                End If
                If Val(drRow.Item("WValue").ToString) > 0 Then
                    'Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments
                    Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                      & " Values(" & VoucherId & ", " & drRow.Item("LocationId") & ", " & WastedStockAccount & ", " & Val(drRow.Item("WValue").ToString) & ", 0, '')"
                    SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                    Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                     & " Values(" & VoucherId & ", " & drRow.Item("LocationId") & ", " & drRow.Item("PlanItemSubSubId") & ", 0, " & Val(drRow.Item("WValue").ToString) & ", '')"
                    SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                End If
                If Val(drRow.Item("SValue").ToString) > 0 Then
                    'Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments
                    Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                      & " Values(" & VoucherId & ", " & drRow.Item("LocationId") & ", " & ScrappedStockAccount & ", " & Val(drRow.Item("SValue").ToString) & ", 0, '')"
                    SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                    Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                     & " Values(" & VoucherId & ", " & drRow.Item("LocationId") & ", " & drRow.Item("PlanItemSubSubId") & ", 0, " & Val(drRow.Item("SValue").ToString) & ", '')"
                    SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS1927
    ''' </summary>
    ''' <param name="Obj"></param>
    ''' <param name="Trans"></param>
    ''' <param name="Source"></param>
    ''' <param name="MyCompanyId"></param>
    ''' <param name="VoucherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateVoucher(ByVal Obj As MaterialDecompositionModel, ByVal dt As DataTable, ByVal Trans As SqlTransaction, ByVal Source As String, ByVal MyCompanyId As Integer, ByVal WastedStockAccount As Integer, ByVal ScrappedStockAccount As Integer, ByVal VoucherId As Integer) As Boolean
        Dim Query As String = ""
        Try
            Query = "  UPDATE tblVoucher SET location_id = " & MyCompanyId & ", finiancial_year_id = 1, voucher_type_id=1, voucher_no='" & Obj.DocumentNo & "', voucher_date='" & Obj.DecompositionDate.ToString("yyyy-M-d h:mm:ss tt") & "', cheque_no=NULL, cheque_date=NULL, post=1, Source= N'" & Source & "', voucher_code='" & Obj.DocumentNo & "', Remarks=N'" & Obj.Remarks.Replace("'", "''") & "' Where voucher_id=" & VoucherId & ""
            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
            Query = "Delete FROM tblVoucherDetail Where voucher_id = " & VoucherId & ""
            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
            For Each drRow As DataRow In dt.Rows
                If Val(drRow.Item("DValue").ToString) > 0 Then
                    'Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments
                    Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                      & " Values(" & VoucherId & ", " & drRow.Item("LocationId") & ", " & drRow.Item("SubSubId") & ", " & Val(drRow.Item("DValue").ToString) & ", 0, '')"
                    SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                    Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                     & " Values(" & VoucherId & ", " & drRow.Item("LocationId") & ", " & drRow.Item("PlanItemSubSubId") & ", 0, " & Val(drRow.Item("DValue").ToString) & ", '')"
                    SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                End If
                If Val(drRow.Item("WValue").ToString) > 0 Then
                    'Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments
                    Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                      & " Values(" & VoucherId & ", " & drRow.Item("LocationId") & ", " & WastedStockAccount & ", " & Val(drRow.Item("WValue").ToString) & ", 0, '')"
                    SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                    Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                     & " Values(" & VoucherId & ", " & drRow.Item("LocationId") & ", " & drRow.Item("PlanItemSubSubId") & ", 0, " & Val(drRow.Item("WValue").ToString) & ", '')"
                    SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                End If
                If Val(drRow.Item("SValue").ToString) > 0 Then
                    'Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments
                    Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                      & " Values(" & VoucherId & ", " & drRow.Item("LocationId") & ", " & ScrappedStockAccount & ", " & Val(drRow.Item("SValue").ToString) & ", 0, '')"
                    SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                    Query = "Insert Into tblVoucherDetail(Voucher_Id, Location_Id,coa_detail_id, debit_amount,credit_amount, comments) " _
                     & " Values(" & VoucherId & ", " & drRow.Item("LocationId") & ", " & drRow.Item("PlanItemSubSubId") & ", 0, " & Val(drRow.Item("SValue").ToString) & ", '')"
                    SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, Query)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS1927
    ''' </summary>
    ''' <param name="VoucherId"></param>
    ''' <param name="Trans"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteVoucher(ByVal VoucherId As Integer, ByVal Trans As SqlTransaction) As Boolean
        Dim Query As String = ""
        'Dim VoucherId As Integer = 0
        Try
            '[ConsumptionDetailId] [int] IDENTITY(1,1) NOT NULL,
            '[ConsumptionId] [int] NULL,
            '[ArticleId] [int] NULL,
            '[Qty] [float] NULL,
            '[Rate] [float] NULL,
            '[DispatchId] [int] NULL,
            '[DispatchDetailId] [int] NULL,
            Query = "  Delete From tblVoucherDetail Where voucher_id =" & VoucherId & " "
            SQLHelper.ExecuteScaler(Trans, CommandType.Text, Query)
            Query = "  Delete From tblVoucher Where voucher_id =" & VoucherId & " "
            SQLHelper.ExecuteScaler(Trans, CommandType.Text, Query)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class


