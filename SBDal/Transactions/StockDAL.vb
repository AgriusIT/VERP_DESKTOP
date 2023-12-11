''06-Feb-2014  TASK:M16 Imran Ali   Add New Fields Engine No And Chassis No. on Sales  
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class StockDAL

    Public Function Add(ByVal StockMaster As StockMaster, Optional ByVal obj As Object = Nothing) As Boolean
        Dim objTrans As SqlTransaction = CType(obj, SqlTransaction)

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open() ' Connection Open
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            'Insert Stock Master Information 
            'before against request no. 934
            'str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks, Project) " _
            '& " Values(N'" & StockMaster.DocNo & "', N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & StockMaster.DocType & ", N'" & StockMaster.Remaks & "', " & IIf(StockMaster.Project = Nothing, "NULL", StockMaster.Project) & ") Select @@Identity "
            'ReqId-934 Resolve comma error
            str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks, Project,Account_Id) " _
            & " Values(N'" & StockMaster.DocNo & "', N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & StockMaster.DocType & ", N'" & StockMaster.Remaks.Replace("'", "''") & "', " & IIf(StockMaster.Project = Nothing, "NULL", StockMaster.Project) & "," & StockMaster.AccountId & ") Select @@Identity "

            StockMaster.StockTransId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            'Insert Stock Detail Information 
            AddDetail(StockMaster.StockTransId, StockMaster, trans)
            'Trans Commit here... 
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Add(ByVal StockMaster As StockMaster, ByVal trans As OleDb.OleDbTransaction, Optional ByVal obj As Object = Nothing) As Boolean
        Dim objTrans As SqlTransaction = CType(obj, SqlTransaction)

        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'Con.Open() ' Connection Open
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            'Insert Stock Master Information 
            'before against request no. 934
            'str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks, Project) " _
            '& " Values(N'" & StockMaster.DocNo & "', N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & StockMaster.DocType & ", N'" & StockMaster.Remaks & "', " & IIf(StockMaster.Project = Nothing, "NULL", StockMaster.Project) & ") Select @@Identity "
            'ReqId-934 Resolve comma error
            str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks, Project, Account_Id) " _
            & " Values(N'" & StockMaster.DocNo & "', N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & StockMaster.DocType & ", N'" & StockMaster.Remaks.Replace("'", "''") & "', " & IIf(StockMaster.Project = Nothing, "NULL", StockMaster.Project) & "," & StockMaster.AccountId & ") Select @@Identity "

            StockMaster.StockTransId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            'Insert Stock Detail Information 
            AddDetail(StockMaster.StockTransId, StockMaster, trans)
            'Trans Commit here... 
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Public Function Add(ByVal StockMaster As StockMaster, ByVal trans As SqlTransaction, Optional ByVal obj As Object = Nothing) As Boolean
        Dim objTrans As SqlTransaction = CType(obj, SqlTransaction)

        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'Con.Open() ' Connection Open
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            'Insert Stock Master Information 
            'before against request no. 934
            'str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks, Project) " _
            '& " Values(N'" & StockMaster.DocNo & "', N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & StockMaster.DocType & ", N'" & StockMaster.Remaks & "', " & IIf(StockMaster.Project = Nothing, "NULL", StockMaster.Project) & ") Select @@Identity "
            'ReqId-934 Resolve comma error
            str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks, Project,Account_Id) " _
            & " Values(N'" & StockMaster.DocNo.Replace("'", "''") & "', N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & StockMaster.DocType & ", N'" & StockMaster.Remaks.Replace("'", "''") & "', " & IIf(StockMaster.Project = Nothing, "NULL", StockMaster.Project) & "," & StockMaster.AccountId & ") Select @@Identity "

            StockMaster.StockTransId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            'Insert Stock Detail Information 
            AddDetail(StockMaster.StockTransId, StockMaster, trans)
            'Trans Commit here... 
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Public Function AddDetail(ByVal StockTransId As Integer, ByVal StockMaster As StockMaster, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim str As String = String.Empty
            Dim StockDetailList As List(Of StockDetail) = StockMaster.StockDetailList
            Dim StockAssemblyList As List(Of StockDetail) = StockMaster.StockListForAssembly ''TFS1957
            For Each StockDetail As StockDetail In StockDetailList
                'Before against request no. 934
                'str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks) " _
                '& " Values (" & StockTransId & ", " & StockDetail.LocationId & ",  " & StockDetail.ArticleDefId & ", " & StockDetail.InQty & ", " & StockDetail.OutQty & ", " & StockDetail.Rate & ", " & StockDetail.InAmount & ", " & StockDetail.OutAmount & ", N'" & StockDetail.Remarks & "')"
                'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'ReqId-934 Resolve Comma Error
                'Before against task:M16
                ' str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks) " _
                '& " Values (" & StockTransId & ", " & StockDetail.LocationId & ",  " & StockDetail.ArticleDefId & ", " & StockDetail.InQty & ", " & StockDetail.OutQty & ", " & StockDetail.Rate & ", " & StockDetail.InAmount & ", " & StockDetail.OutAmount & ", N'" & StockDetail.Remarks.Replace("'", "''") & "')"
                'Task:M16 Added Column Engine_No And Chassis_No
                StockDetail.Engine_No = IIf(StockDetail.Engine_No = Nothing, DBNull.Value.ToString, StockDetail.Engine_No)
                StockDetail.Chassis_No = IIf(StockDetail.Chassis_No = Nothing, DBNull.Value.ToString, StockDetail.Chassis_No)
                StockDetail.ExpiryDate = IIf(StockDetail.ExpiryDate = Nothing, Date.Now, StockDetail.ExpiryDate)
                ''TFS1596 :Ayesha Rehman Added Column BatchNo
                str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks,Engine_No, Chassis_No, Cost_Price, Pack_Qty, In_PackQty, Out_PackQty , BatchNo , ExpiryDate, Origin) " _
              & " Values (" & StockTransId & ", " & StockDetail.LocationId & ",  " & StockDetail.ArticleDefId & ", " & StockDetail.InQty & ", " & StockDetail.OutQty & ", " & StockDetail.Rate & ", " & StockDetail.InAmount & ", " & StockDetail.OutAmount & ", N'" & StockDetail.Remarks.Replace("'", "''") & "', " & IIf(StockDetail.Engine_No.Length > 0, "N'" & StockDetail.Engine_No.Replace("'", "''") & "'", "Null") & "," & IIf(StockDetail.Chassis_No.Length > 0, "N'" & StockDetail.Chassis_No.Replace("'", "''") & "'", "Null") & ", " & StockDetail.CostPrice & ", " & StockDetail.PackQty & ", " & StockDetail.In_PackQty & ", " & StockDetail.Out_PackQty & ", N'" & StockDetail.BatchNo.Replace("'", "''") & "' , " & IIf(StockDetail.ExpiryDate.ToString <> "", "N'" & StockDetail.ExpiryDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "Null") & ", N'" & StockDetail.Origin.Replace("'", "''") & "')"
                'End Task:M16
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            ''Start TFS1957
            If StockAssemblyList IsNot Nothing Then
                For Each StockDetail As StockDetail In StockAssemblyList

                    'Task:M16 Added Column Engine_No And Chassis_No
                    StockDetail.Engine_No = IIf(StockDetail.Engine_No = Nothing, DBNull.Value.ToString, StockDetail.Engine_No)
                    StockDetail.Chassis_No = IIf(StockDetail.Chassis_No = Nothing, DBNull.Value.ToString, StockDetail.Chassis_No)
                    StockDetail.ExpiryDate = IIf(StockDetail.ExpiryDate = Nothing, Date.Now, StockDetail.ExpiryDate)
                    ''TFS1596 :Ayesha Rehman Added Column BatchNo
                    str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks,Engine_No, Chassis_No, Cost_Price, Pack_Qty, In_PackQty, Out_PackQty , BatchNo , ExpiryDate, Origin) " _
                  & " Values (" & StockTransId & ", " & StockDetail.LocationId & ",  " & StockDetail.ArticleDefId & ", " & StockDetail.InQty & ", " & StockDetail.OutQty & ", " & StockDetail.Rate & ", " & StockDetail.InAmount & ", " & StockDetail.OutAmount & ", N'" & StockDetail.Remarks.Replace("'", "''") & "', " & IIf(StockDetail.Engine_No.Length > 0, "N'" & StockDetail.Engine_No.Replace("'", "''") & "'", "Null") & "," & IIf(StockDetail.Chassis_No.Length > 0, "N'" & StockDetail.Chassis_No.Replace("'", "''") & "'", "Null") & ", " & StockDetail.CostPrice & ", " & StockDetail.PackQty & ", " & StockDetail.In_PackQty & ", " & StockDetail.Out_PackQty & ", N'" & StockDetail.BatchNo.Replace("'", "''") & "' , " & IIf(StockDetail.ExpiryDate.ToString <> "", "N'" & StockDetail.ExpiryDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "Null") & ", N'" & StockDetail.Origin.Replace("'", "''") & "')"
                    'End Task:M16
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Next
            End If
            ''End TFS1957
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
        End Try
    End Function
    Public Function AddDetail(ByVal StockTransId As Integer, ByVal StockMaster As StockMaster, ByVal trans As OleDb.OleDbTransaction) As Boolean
        Try

            Dim str As String = String.Empty
            Dim StockDetailList As List(Of StockDetail) = StockMaster.StockDetailList
            Dim StockAssemblyList As List(Of StockDetail) = StockMaster.StockListForAssembly ''TFS1957
            For Each StockDetail As StockDetail In StockDetailList
                'Before against request no. 934
                'str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks) " _
                '& " Values (" & StockTransId & ", " & StockDetail.LocationId & ",  " & StockDetail.ArticleDefId & ", " & StockDetail.InQty & ", " & StockDetail.OutQty & ", " & StockDetail.Rate & ", " & StockDetail.InAmount & ", " & StockDetail.OutAmount & ", N'" & StockDetail.Remarks & "')"
                'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'ReqId-934 Resolve Comma Error
                'Before against task:M16
                ' str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks) " _
                '& " Values (" & StockTransId & ", " & StockDetail.LocationId & ",  " & StockDetail.ArticleDefId & ", " & StockDetail.InQty & ", " & StockDetail.OutQty & ", " & StockDetail.Rate & ", " & StockDetail.InAmount & ", " & StockDetail.OutAmount & ", N'" & StockDetail.Remarks.Replace("'", "''") & "')"
                'Task:M16 Added Column Engine_No And Chassis_No
                StockDetail.Engine_No = IIf(StockDetail.Engine_No = Nothing, DBNull.Value.ToString, StockDetail.Engine_No)
                StockDetail.Chassis_No = IIf(StockDetail.Chassis_No = Nothing, DBNull.Value.ToString, StockDetail.Chassis_No)
                StockDetail.ExpiryDate = IIf(StockDetail.ExpiryDate = Nothing, Date.Now, StockDetail.ExpiryDate)
                ''TFS1596 :Ayesha Rehman Added Column BatchNo
                str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks,Engine_No, Chassis_No, Cost_Price, Pack_Qty, In_PackQty, Out_PackQty, BatchNo , ExpiryDate, Origin) " _
              & " Values (" & StockTransId & ", " & StockDetail.LocationId & ",  " & StockDetail.ArticleDefId & ", " & StockDetail.InQty & ", " & StockDetail.OutQty & ", " & StockDetail.Rate & ", " & StockDetail.InAmount & ", " & StockDetail.OutAmount & ", N'" & StockDetail.Remarks.Replace("'", "''") & "', " & IIf(StockDetail.Engine_No.Length > 0, "N'" & StockDetail.Engine_No.Replace("'", "''") & "'", "Null") & "," & IIf(StockDetail.Chassis_No.Length > 0, "N'" & StockDetail.Chassis_No.Replace("'", "''") & "'", "Null") & ", " & StockDetail.CostPrice & ", " & StockDetail.PackQty & ", " & StockDetail.In_PackQty & ", " & StockDetail.Out_PackQty & ", N'" & StockDetail.BatchNo & "' , " & IIf(StockDetail.ExpiryDate.ToString <> "", "N'" & StockDetail.ExpiryDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "Null") & ", N'" & StockDetail.Origin & "')"
                'End Task:M16
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            ''Start TFS1957
            If StockAssemblyList IsNot Nothing Then
                For Each StockDetail As StockDetail In StockAssemblyList

                    'Task:M16 Added Column Engine_No And Chassis_No
                    StockDetail.Engine_No = IIf(StockDetail.Engine_No = Nothing, DBNull.Value.ToString, StockDetail.Engine_No)
                    StockDetail.Chassis_No = IIf(StockDetail.Chassis_No = Nothing, DBNull.Value.ToString, StockDetail.Chassis_No)
                    StockDetail.ExpiryDate = IIf(StockDetail.ExpiryDate = Nothing, Date.Now, StockDetail.ExpiryDate)
                    ''TFS1596 :Ayesha Rehman Added Column BatchNo
                    str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks,Engine_No, Chassis_No, Cost_Price, Pack_Qty, In_PackQty, Out_PackQty, BatchNo , ExpiryDate, Origin) " _
                  & " Values (" & StockTransId & ", " & StockDetail.LocationId & ",  " & StockDetail.ArticleDefId & ", " & StockDetail.InQty & ", " & StockDetail.OutQty & ", " & StockDetail.Rate & ", " & StockDetail.InAmount & ", " & StockDetail.OutAmount & ", N'" & StockDetail.Remarks.Replace("'", "''") & "', " & IIf(StockDetail.Engine_No.Length > 0, "N'" & StockDetail.Engine_No.Replace("'", "''") & "'", "Null") & "," & IIf(StockDetail.Chassis_No.Length > 0, "N'" & StockDetail.Chassis_No.Replace("'", "''") & "'", "Null") & ", " & StockDetail.CostPrice & ", " & StockDetail.PackQty & ", " & StockDetail.In_PackQty & ", " & StockDetail.Out_PackQty & ", N'" & StockDetail.BatchNo & "' , " & IIf(StockDetail.ExpiryDate.ToString <> "", "N'" & StockDetail.ExpiryDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "Null") & ", N'" & StockDetail.Origin & "')"
                    'End Task:M16
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Next
            End If
            ''End TFS1957
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
        End Try
    End Function
    'Public Function AddDetailByOledbTrans(ByVal StockTransId As Integer, ByVal StockMaster As StockMaster, ByVal trans As OleDb.OleDbTransaction) As Boolean
    '    Dim cmd As New OleDb.OleDbCommand
    '    cmd.Connection = trans.Connection
    '    cmd.Transaction = trans
    '    cmd.CommandType = CommandType.Text
    '    Try

    '        Dim str As String = String.Empty
    '        Dim StockDetailList As List(Of StockDetail) = StockMaster.StockDetailList
    '        For Each StockDetail As StockDetail In StockDetailList
    '            'Before against request no. 934
    '            'str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks) " _
    '            '& " Values (" & StockTransId & ", " & StockDetail.LocationId & ",  " & StockDetail.ArticleDefId & ", " & StockDetail.InQty & ", " & StockDetail.OutQty & ", " & StockDetail.Rate & ", " & StockDetail.InAmount & ", " & StockDetail.OutAmount & ", N'" & StockDetail.Remarks & "')"
    '            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
    '            'ReqId-934 Resolve Comma Error
    '            cmd.CommandText = ""
    '            str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks,Cost_Price) " _
    '           & " Values (" & StockTransId & ", " & StockDetail.LocationId & ",  " & StockDetail.ArticleDefId & ", " & StockDetail.InQty & ", " & StockDetail.OutQty & ", " & StockDetail.Rate & ", " & StockDetail.InAmount & ", " & StockDetail.OutAmount & ", N'" & StockDetail.Remarks.Replace("'", "''") & "'," & StockDetail.CostPrice & ")"
    '            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
    '            cmd.CommandText = str
    '            cmd.ExecuteNonQuery()
    '        Next
    '        Return True
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    End Try
    'End Function
    Public Function Update(ByVal StockMaster As StockMaster) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open() ' Connection Open
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            If StockMaster.StockTransId > 0 Then
                'Update Stock Master Table 
                'Before against request no. 934
                'str = "Update StockMasterTable Set DocNo=N'" & StockMaster.DocNo & "', DocDate=N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', DocType= " & StockMaster.DocType & ", Remarks=N'" & StockMaster.Remaks & "', Project=" & IIf(StockMaster.Project = Nothing, "NULL", StockMaster.Project) & " WHERE StockTransId=" & StockMaster.StockTransId & ""
                'ReqId-934 Resolve Comma Error
                str = "Update StockMasterTable Set DocNo=N'" & StockMaster.DocNo & "', DocDate=N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', DocType= " & StockMaster.DocType & ", Remarks=N'" & StockMaster.Remaks.Replace("'", "''") & "', Project=" & IIf(StockMaster.Project = Nothing, "NULL", StockMaster.Project) & ",Account_Id=" & StockMaster.AccountId & " WHERE StockTransId=" & StockMaster.StockTransId & ""

                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Delete Previouce Data from Stock Detail
                str = "Delete From StockDetailTable WHERE StockTransId=" & StockMaster.StockTransId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Else
                'Before against Request no. 934
                'str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks) " _
                '& " Values(N'" & StockMaster.DocNo & "', N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & StockMaster.DocType & ", N'" & StockMaster.Remaks & "') Select @@Identity "
                'ReqId-934 Resolve Comma Error
                str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks,Project, Account_Id) " _
                & " Values(N'" & StockMaster.DocNo & "', N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & StockMaster.DocType & ", N'" & StockMaster.Remaks.Replace("'", "''") & "'," & StockMaster.Project & "," & StockMaster.AccountId & ") Select @@Identity "

                StockMaster.StockTransId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                'Insert Stock Detail Information 
            End If
            AddDetail(StockMaster.StockTransId, StockMaster, trans)

            'Transaction Commit
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal StockMaster As StockMaster, ByVal trans As OleDb.OleDbTransaction) As Boolean
        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'Con.Open() ' Connection Open
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty





            If StockMaster.StockTransId > 0 Then
                'Update Stock Master Table 
                'Before against request no. 934
                'str = "Update StockMasterTable Set DocNo=N'" & StockMaster.DocNo & "', DocDate=N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', DocType= " & StockMaster.DocType & ", Remarks=N'" & StockMaster.Remaks & "', Project=" & IIf(StockMaster.Project = Nothing, "NULL", StockMaster.Project) & " WHERE StockTransId=" & StockMaster.StockTransId & ""
                'ReqId-934 Resolve Comma Error
                str = "Update StockMasterTable Set DocNo=N'" & StockMaster.DocNo & "', DocDate=N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', DocType= " & StockMaster.DocType & ", Remarks=N'" & StockMaster.Remaks.Replace("'", "''") & "', Project=" & IIf(StockMaster.Project = Nothing, "NULL", StockMaster.Project) & ",Account_Id=" & StockMaster.AccountId & " WHERE StockTransId=" & StockMaster.StockTransId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Delete Previouce Data from Stock Detail
                str = "Delete From StockDetailTable WHERE StockTransId=" & StockMaster.StockTransId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Else
                'Before against Request no. 934
                'str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks) " _
                '& " Values(N'" & StockMaster.DocNo & "', N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & StockMaster.DocType & ", N'" & StockMaster.Remaks & "') Select @@Identity "
                'ReqId-934 Resolve Comma Error
                str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks,Project,Account_Id) " _
                & " Values(N'" & StockMaster.DocNo & "', N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & StockMaster.DocType & ", N'" & StockMaster.Remaks.Replace("'", "''") & "'," & StockMaster.Project & "," & StockMaster.AccountId & ") Select @@Identity "

                StockMaster.StockTransId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                'Insert Stock Detail Information 
            End If
            AddDetail(StockMaster.StockTransId, StockMaster, trans)

            'Transaction Commit
            'trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Public Function Update(ByVal StockMaster As StockMaster, ByVal trans As SqlTransaction) As Boolean
        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'Con.Open() ' Connection Open
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty


            If StockMaster.StockTransId > 0 Then
                'Update Stock Master Table 
                'Before against request no. 934
                'str = "Update StockMasterTable Set DocNo=N'" & StockMaster.DocNo & "', DocDate=N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', DocType= " & StockMaster.DocType & ", Remarks=N'" & StockMaster.Remaks & "', Project=" & IIf(StockMaster.Project = Nothing, "NULL", StockMaster.Project) & " WHERE StockTransId=" & StockMaster.StockTransId & ""
                'ReqId-934 Resolve Comma Error
                str = "Update StockMasterTable Set DocNo=N'" & StockMaster.DocNo & "', DocDate=N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', DocType= " & StockMaster.DocType & ", Remarks=N'" & StockMaster.Remaks.Replace("'", "''") & "', Project=" & IIf(StockMaster.Project = Nothing, "NULL", StockMaster.Project) & ",Account_Id=" & StockMaster.AccountId & " WHERE StockTransId=" & StockMaster.StockTransId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Delete Previouce Data from Stock Detail
                str = "Delete From StockDetailTable WHERE StockTransId=" & StockMaster.StockTransId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Else
                'Before against Request no. 934
                'str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks) " _
                '& " Values(N'" & StockMaster.DocNo & "', N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & StockMaster.DocType & ", N'" & StockMaster.Remaks & "') Select @@Identity "
                'ReqId-934 Resolve Comma Error
                str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks,Project,Account_Id) " _
                & " Values(N'" & StockMaster.DocNo & "', N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & StockMaster.DocType & ", N'" & StockMaster.Remaks.Replace("'", "''") & "'," & StockMaster.Project & "," & StockMaster.AccountId & ") Select @@Identity "

                StockMaster.StockTransId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                'Insert Stock Detail Information 
            End If
            AddDetail(StockMaster.StockTransId, StockMaster, trans)

            'Transaction Commit
            'trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    'Public Function UpdateByTrans(ByVal StockMaster As StockMaster, Optional ByVal trans As OleDb.OleDbTransaction = Nothing) As Boolean
    '    Dim cmd As New OleDb.OleDbCommand
    '    cmd.Connection = trans.Connection
    '    cmd.Transaction = trans
    '    cmd.CommandType = CommandType.Text
    '    Try
    '        Dim str As String = String.Empty
    '        If StockMaster.StockTransId > 0 Then
    '            'Update Stock Master Table 
    '            'Before against request no. 934
    '            'str = "Update StockMasterTable Set DocNo=N'" & StockMaster.DocNo & "', DocDate=N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', DocType= " & StockMaster.DocType & ", Remarks=N'" & StockMaster.Remaks & "', Project=" & IIf(StockMaster.Project = Nothing, "NULL", StockMaster.Project) & " WHERE StockTransId=" & StockMaster.StockTransId & ""
    '            'ReqId-934 Resolve Comma Error
    '            cmd.CommandText = ""
    '            str = "Update StockMasterTable Set DocNo=N'" & StockMaster.DocNo & "', DocDate=N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', DocType= " & StockMaster.DocType & ", Remarks=N'" & StockMaster.Remaks.Replace("'", "''") & "', Project=" & IIf(StockMaster.Project = Nothing, "NULL", StockMaster.Project) & " WHERE StockTransId=" & StockMaster.StockTransId & ""
    '            cmd.CommandText = str
    '            cmd.ExecuteNonQuery()
    '            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
    '            'Delete Previouce Data from Stock Detail
    '            cmd.CommandText = ""
    '            str = "Delete From StockDetailTable WHERE StockTransId=" & StockMaster.StockTransId & ""
    '            cmd.CommandText = str
    '            cmd.ExecuteNonQuery()
    '        Else
    '            'Before against Request no. 934
    '            'str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks) " _
    '            '& " Values(N'" & StockMaster.DocNo & "', N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & StockMaster.DocType & ", N'" & StockMaster.Remaks & "') Select @@Identity "
    '            'ReqId-934 Resolve Comma Error
    '            str = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks) " _
    '            & " Values(N'" & StockMaster.DocNo & "', N'" & StockMaster.DocDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & StockMaster.DocType & ", N'" & StockMaster.Remaks.Replace("'", "''") & "') Select @@Identity "
    '            cmd.CommandText = str
    '            StockMaster.StockTransId = cmd.ExecuteScalar()
    '            'Insert Stock Detail Information 
    '        End If
    '        AddDetailByOledbTrans(StockMaster.StockTransId, StockMaster, trans)
    '        'Transaction Commit
    '        'trans.Commit()
    '        Return True

    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    Finally
    '        'Con.Close()
    '    End Try
    'End Function
    Public Function Delete(ByVal StockMaster As StockMaster) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open() ' Connection Open
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            If StockMaster.StockTransId > 0 Then
                'Delete From Stock Detail Table
                str = "Delete From StockDetailTable WHERE StockTransId=" & StockMaster.StockTransId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Delete From Stock Master Table 
                str = "Delete From StockMasterTable WHERE StockTransId=" & StockMaster.StockTransId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal StockMaster As StockMaster, ByVal trans As OleDb.OleDbTransaction) As Boolean
        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'If Not Con.State = 1 Then Con.Open() ' Connection Open
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            If StockMaster.StockTransId > 0 Then
                'Delete From Stock Detail Table
                str = "Delete From StockDetailTable WHERE StockTransId=" & StockMaster.StockTransId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Delete From Stock Master Table 
                str = "Delete From StockMasterTable WHERE StockTransId=" & StockMaster.StockTransId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal StockMaster As StockMaster, ByVal trans As SqlTransaction) As Boolean
        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'If Not Con.State = 1 Then Con.Open() ' Connection Open
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            If StockMaster.StockTransId > 0 Then
                'Delete From Stock Detail Table
                str = "Delete From StockDetailTable WHERE StockTransId=" & StockMaster.StockTransId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Delete From Stock Master Table 
                str = "Delete From StockMasterTable WHERE StockTransId=" & StockMaster.StockTransId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Public Function GetAllRecord() As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select * From StockMasterTable "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetStockTransId(ByVal ByDocNo As String) As String
        Try
            'Get All Records From Stock Master Table
            Dim dtStockTable As DataTable
            dtStockTable = New StockDAL().GetAllRecord()
            dtStockTable.TableName = "StockTrans"
            If GetFilterDataFromDataTable(dtStockTable, "[DocNo]='" & ByDocNo & "'").ToTable("StockTrans").Rows.Count < 1 Then
                Return "0"
            Else
                Return GetFilterDataFromDataTable(dtStockTable, "[DocNo]='" & ByDocNo & "'").ToTable("StockTrans").Rows(0).Item("StockTransId").ToString()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
