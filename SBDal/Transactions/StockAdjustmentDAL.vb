Imports SBModel
Imports SBDal.SqlHelper
Imports System.Data
Imports System.Data.SqlClient
Public Class StockAdjustmentDAL
    Public Function save(ByVal adj As StockAdjustmenMasterBE) As Boolean
        Dim DocNo As String = UtilityDAL.GetSerialNo("SA" & "-" & Right(adj.Doc_Date.Year, 2) & "-", "StockAdjustmentMaster", "Doc_No")
        adj.Doc_no = DocNo
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "insert into StockAdjustmentMaster(Doc_no,Doc_Date,Project,remarks) values( N'" & adj.Doc_no & "', N'" & adj.Doc_Date.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & adj.Project & "', N'" & adj.remarks & "') Select @@Identity"
            adj.SA_id = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            saveDetail(adj, trans)

            str = String.Empty
            str = "insert into StockMasterTable(DocNo, DocDate, DocType, Remarks, Project) Values(N'" & adj.Doc_no & "', N'" & adj.StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & adj.StockMaster.DocType & ", N'" & adj.StockMaster.Remaks & "', " & adj.StockMaster.Project & ") Select @@Identity"
            adj.StockMaster.StockTransId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            AddStockDetail(adj.StockMaster, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Public Function update(ByVal adj As StockAdjustmenMasterBE) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = String.Empty
            str = "Select StockTransId From StockMasterTable WHERE DocNo='" & adj.Doc_no & "'"
            adj.StockMaster.StockTransId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

            str = String.Empty
            str = "update StockAdjustmentMaster  set Doc_no= N'" & adj.Doc_no & "', Doc_Date= N'" & adj.Doc_Date.ToString("yyyy-M-d h:mm:ss tt") & "',Project= N'" & adj.Project & "',remarks= N'" & adj.remarks & "' where SA_id= " & adj.SA_id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = " delete from StockAdjustmentDetail where SA_id= " & adj.SA_id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            saveDetail(adj, trans)



            'Dim dt As New DataTable
            'Dim da As New SqlDataAdapter
            'Dim cmd As New SqlClient.SqlCommand
            'cmd.CommandText = "Select StockTransId From StockMasterTable WHERE DocNo=N'" & adj.Doc_no & "'"
            'cmd.CommandType = CommandType.Text
            'cmd.Connection = trans.Connection
            'cmd.Transaction = trans
            'da.SelectCommand = cmd
            'da.Fill(dt)
            'Dim StockTransId As Integer = 0I
            'If dt IsNot Nothing Then
            '    If dt.Rows.Count > 0 Then
            '        StockTransId = dt.Rows(0).Item(0)
            '    End If
            'End If




            If adj.StockMaster.StockTransId > 0 Then

                'adj.StockMaster.StockTransId = StockTransId
                str = String.Empty
                str = "UPDATE StockMasterTable  SET DocNo=N'" & adj.Doc_no & "', DocDate=N'" & adj.StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', DocType=" & adj.StockMaster.DocType & ", Remarks=N'" & adj.StockMaster.Remaks.Replace("'", "''") & "', Project=" & adj.StockMaster.Project & " WHERE StockTransId= " & adj.StockMaster.StockTransId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Else

                str = String.Empty
                str = "insert into StockMasterTable(DocNo, DocDate, DocType, Remarks, Project) Values(N'" & adj.Doc_no & "', N'" & adj.StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & adj.StockMaster.DocType & ", N'" & adj.StockMaster.Remaks & "', " & adj.StockMaster.Project & ") Select @@Identity"
                adj.StockMaster.StockTransId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

            End If


            str = String.Empty
            str = "Delete From StockDetailTable WHERE StockTransId=" & adj.StockMaster.StockTransId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            AddStockDetail(adj.StockMaster, trans)


            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Public Function delete(ByVal adj As StockAdjustmenMasterBE) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim trans As SqlTransaction = conn.BeginTransaction()
        Try
            Dim str As String = String.Empty
            str = " delete from StockAdjustmentDetail where SA_id= " & adj.SA_id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = " delete from StockAdjustmentMaster where SA_id= " & adj.SA_id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)



            Dim dt As New DataTable
            Dim da As New SqlDataAdapter
            Dim cmd As New SqlClient.SqlCommand
            cmd.CommandText = "Select StockTransId From StockMasterTable WHERE DocNo=N'" & adj.Doc_no & "'"
            cmd.CommandType = CommandType.Text
            cmd.Connection = trans.Connection
            cmd.Transaction = trans
            da.SelectCommand = cmd
            da.Fill(dt)
            Dim StockTransId As Integer = 0I
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    StockTransId = dt.Rows(0).Item(0)
                End If
            End If

            str = String.Empty
            str = "Delete From StockDetailTable WHERE StockTransId=" & StockTransId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From StockMasterTable WHERE StockTransId=" & StockTransId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Public Function saveDetail(ByVal adj As StockAdjustmenMasterBE, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = String.Empty
            For Each row As StockAdjustmenDetailBE In adj.StockAdjustmentDetail
                str = "insert into StockAdjustmentDetail (SA_id,location_id,Artical_id,ArticalSize,Qty,S1,S2,S3,S4,S5,S6,S7,Current_price,Price,AdjustmentTypeId, Pack_Desc, BatchNo, ExpiryDate, Origin) values(N'" & adj.SA_id & "',N'" & row.location_id & "', N'" & row.Artical_id & "', N'" & row.ArticalSize & "',N'" & row.Qty & "',N'" & row.S1 & "',N'" & row.S2 & "',N'" & row.S3 & "',N'" & row.S4 & "',N'" & row.S5 & "',N'" & row.S6 & "',N'" & row.S7 & "',N'" & row.Current_price & "',N'" & row.Price & "',N'" & row.AdjustmentTypeId & "', N'" & row.Pack_Desc.Replace("'", "''") & "', N'" & row.BatchNo.Replace("'", "''") & "', " & IIf(row.ExpiryDate.ToString <> "", "N'" & row.ExpiryDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "Null") & ", N'" & row.Origin.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function AddStockDetail(ByVal Stock As StockMaster, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = String.Empty
            For Each StockDt As StockDetail In Stock.StockDetailList
                str = "Insert Into StockDetailTable (StockTransId,LocationId,ArticleDefId,InQty,OutQty,Rate,InAmount,OutAmount, Remarks ,In_PackQty ,Out_PackQty ,Pack_Qty, BatchNo, ExpiryDate, Origin) Values(" & Stock.StockTransId & ", " & StockDt.LocationId & ", " & StockDt.ArticleDefId & ", " & StockDt.InQty & ", " & StockDt.OutQty & ", " & StockDt.Rate & ", " & StockDt.InAmount & ", " & StockDt.OutAmount & ", N'" & StockDt.Remarks.Replace("'", "''") & "', " & StockDt.In_PackQty & ", " & StockDt.Out_PackQty & ", " & StockDt.PackQty & " , N'" & StockDt.BatchNo.Replace("'", "''") & "' , " & IIf(StockDt.ExpiryDate.ToString <> "", "N'" & StockDt.ExpiryDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "Null") & ", N'" & StockDt.Origin.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function getAll() As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select Project,Sa_ID, Doc_No,Doc_Date,tblDefCostCenter.Name as CostCenter, Remarks From StockAdjustmentMaster LEFT OUTER JOIN tblDefCostCenter on tblDefCostCenter.CostCenterId = StockAdjustmentMaster.project ORDER By Sa_ID Desc "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getAllDetail(ByVal StockAdjustmentId As Integer) As DataTable
        Try
            Dim strQuery As String = "SELECT dbo.StockAdjustmentDetail.location_id AS LocationId, dbo.StockAdjustmentDetail.AdjustmentTypeId, dbo.ArticleDefView.ArticleCompanyName, dbo.StockAdjustmentDetail.Artical_id AS ArticleDefId, " _
                   & " dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleSizeName, dbo.ArticleDefView.ArticleColorName,  " _
                   & " dbo.StockAdjustmentDetail.Articalsize as ArticleSize,dbo.StockAdjustmentDetail.S1 as PackQty, StockAdjustmentDetail.S7 AS Qty, dbo.StockAdjustmentDetail.S1 * StockAdjustmentDetail.S7 AS TotalQty, dbo.StockAdjustmentDetail.Price,  " _
                   & " dbo.StockAdjustmentDetail.Current_price as CurrentPrice, 0 as Total, Isnull(StockAdjustmentDetail.Pack_Desc, StockAdjustmentDetail.Articalsize) as Pack_Desc, ISNULL(StockAdjustmentDetail.BatchNo, 'xxxx') as BatchNo , StockAdjustmentDetail.ExpiryDate, isnull(StockAdjustmentDetail.Origin,'') as Origin  " _
                   & " FROM dbo.StockAdjustmentDetail INNER JOIN " _
                   & " dbo.ArticleDefView ON dbo.StockAdjustmentDetail.Artical_id = dbo.ArticleDefView.ArticleId WHERE SA_Id=" & StockAdjustmentId & ""
            Return UtilityDAL.GetDataTable(strQuery)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetAllTypeList() As List(Of AdjustmentTypeBE)
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Try
            Dim str As String = ""
            str = "Select AdjType_Id, AdjType, isnull(Sort_Order,0) as Sort_Order, isnull(Active,0) as Active, isnull(AdjustmentInShort,0) as AdjustmentInShort From tblAdjustmentType"
            Dim cmd As New SqlCommand
            cmd.CommandText = str
            cmd.CommandType = CommandType.Text
            cmd.Connection = con
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader

            Dim TypeList As New List(Of AdjustmentTypeBE)
            Dim Type As AdjustmentTypeBE
            If dr.HasRows Then
                Do While dr.Read
                    Type = New AdjustmentTypeBE
                    Type.AdjTypeId = dr.GetValue(0)
                    Type.AdjTypeName = dr.GetValue(1)
                    Type.Sort_Order = dr.GetValue(2)
                    Type.Active = dr.GetValue(3)
                    Type.AdjustmentInShort = dr.GetValue(4)
                    TypeList.Add(Type)
                Loop
            End If
            Return TypeList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
