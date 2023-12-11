Public Class frmDataTransfer
    Public FromDate As DateTime = Now
    Public DataFile As String = String.Empty
    Public LogFile As String = String.Empty
    Public ErrorStatus As String = String.Empty
    Enum enmTables
        'Version 1.0.0.0
        tblVoucher
        tblVoucherDetail
        SalesOrderMasterTable
        SalesOrderDetailTable
        SalesMasterTable
        SalesDetailTable
        SalesReturnMasterTable
        SalesReturnDetailTable
        PurchaseOrderMasterTable
        PurchaseOrderDetailTable
        PurchaseReturnMasterTable
        PurchaseReturnDetailTable
        ReceivingMasterTable
        ReceivingDetailTable
        DispatchMasterTable
        DispatchDetailTable
        ProductionMasterTable
        ProductionDetailTable
        StockAdjustmentMaster
        StockAdjustmentDetail
        StockMasterTable
        StockDetailTable
    End Enum
    Public Function CreateDatabase(ByVal dtpDateFrom As DateTime) As Boolean
        ConStringBuilder.Item("Initial Catalog") = "master"
        Dim SQLClientCon As New SqlClient.SqlConnection(ConStringBuilder.ConnectionString)
        If SQLClientCon.State = ConnectionState.Closed Then SQLClientCon.Open()
        Dim cmd As New SqlClient.SqlCommand
        Try
            Dim strSQL As String = String.Empty


            strSQL = "Use Master IF NOT EXISTS(Select * From SysDatabases WHERE Name='UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & "') CREATE DATABASE UPDATE_" & Me.dtpDateFrom.Value.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ""
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text
            cmd.Connection = SQLClientCon
            cmd.ExecuteNonQuery()

            GetDatabasePath("UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & "")

            If Not IO.Directory.Exists("C:\Temp") Then
                IO.Directory.CreateDirectory("C:\Temp")
            End If
            Dim FileName As String = String.Empty
            FileName = "C:\Temp\DBFilePath.txt"
            Dim myByte() As Byte = {}
            Dim MyChar() As Char = DataFile
            myByte = System.Text.ASCIIEncoding.Unicode.GetBytes(MyChar)
            Dim fs As IO.FileStream = New IO.FileStream(FileName, IO.FileMode.Create, IO.FileAccess.ReadWrite)
            fs.Write(myByte, 0, myByte.Length)
            fs.Dispose()
            fs.Close()
            Return True
        Catch ex As Exception
            ErrorStatus = "Unsuccessfully"
            Throw ex
        Finally
            SQLClientCon.Close()
        End Try
    End Function
    Public Function CreateTable(ByVal Table_Name As String) As Boolean
        ConStringBuilder.Item("Initial Catalog") = "UPDATE_" & Me.dtpDateFrom.Value.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ""
        Dim SQLClientCon As New SqlClient.SqlConnection(ConStringBuilder.ConnectionString)
        If SQLClientCon.State = ConnectionState.Closed Then SQLClientCon.Open()
        Dim cmd As New SqlClient.SqlCommand
        Try

            Dim strSQL As String = String.Empty
            strSQL = "select syscolumns.name, systypes.name as DataType, syscolumns.length from sysColumns INNER JOIN sysTypes on systypes.xType = syscolumns.xtype where id in (select Id from Sysobjects WHERE Name='" & Table_Name & "') AND sysTypes.Name <> 'sysname' ORDER BY ID"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)

            Dim strCreateTable As String = String.Empty
            Dim DataTypeflg As Boolean = False

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        If (r.Item("DataType").ToString = "int" Or r.Item("DataType").ToString = "float" Or r.Item("DataType").ToString = "bit" Or r.Item("DataType").ToString = "datetime" Or r.Item("DataType").ToString = "timestamp" Or r.Item("DataType").ToString = "bigint" Or r.Item("DataType").ToString = "money" Or r.Item("DataType").ToString = "ntext" Or r.Item("DataType").ToString = "real" Or r.Item("DataType").ToString = "smalldatetime" Or r.Item("DataType").ToString = "smallint" Or r.Item("DataType").ToString = "smallmoney" Or r.Item("DataType").ToString = "sql_variant" Or r.Item("DataType").ToString = "text" Or r.Item("DataType").ToString = "tinyint" Or r.Item("DataType").ToString = "uniqueidentifier") Then
                            DataTypeflg = True
                        Else
                            DataTypeflg = False
                        End If
                        If strCreateTable.Length > 0 Then
                            strCreateTable += ",[" & r.Item("name").ToString & "][" & r.Item("DataType").ToString & "]" & IIf(DataTypeflg = True, "NULL", "(" & r.Item("length").ToString & ") NULL") & ""
                        Else
                            strCreateTable = "Use UPDATE_" & Me.dtpDateFrom.Value.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & " IF NOT EXISTS (Select * From Information_Schema.Tables WHERE Table_Name='" & Table_Name & "') CREATE TABLE " & Table_Name.ToString & "( [" & r.Item("name").ToString & "][" & r.Item("DataType").ToString & "] " & IIf(DataTypeflg = True, "NULL", "(" & r.Item("length").ToString & ") NULL") & ""
                        End If
                    Next
                    strCreateTable = strCreateTable & ")"
                End If
            End If

            cmd.CommandText = strCreateTable
            cmd.CommandType = CommandType.Text
            cmd.Connection = SQLClientCon
            cmd.ExecuteNonQuery()
            Return True

        Catch ex As Exception
            ErrorStatus = "Unsuccessfully"
            Throw ex
        Finally
            SQLClientCon.Close()
        End Try
    End Function

    Public Function CreateTables() As Boolean
        Try
            CreateTable(enmTables.DispatchDetailTable.ToString)
            CreateTable(enmTables.DispatchMasterTable.ToString)
            CreateTable(enmTables.ProductionDetailTable.ToString)
            CreateTable(enmTables.ProductionMasterTable.ToString)
            CreateTable(enmTables.PurchaseOrderDetailTable.ToString)
            CreateTable(enmTables.PurchaseOrderMasterTable.ToString)
            CreateTable(enmTables.PurchaseReturnDetailTable.ToString)
            CreateTable(enmTables.PurchaseReturnMasterTable.ToString)
            CreateTable(enmTables.ReceivingDetailTable.ToString)
            CreateTable(enmTables.ReceivingMasterTable.ToString)
            CreateTable(enmTables.SalesDetailTable.ToString)
            CreateTable(enmTables.SalesMasterTable.ToString)
            CreateTable(enmTables.SalesOrderDetailTable.ToString)
            CreateTable(enmTables.SalesOrderMasterTable.ToString)
            CreateTable(enmTables.SalesReturnDetailTable.ToString)
            CreateTable(enmTables.SalesReturnMasterTable.ToString)
            CreateTable(enmTables.StockAdjustmentDetail.ToString)
            CreateTable(enmTables.StockAdjustmentMaster.ToString)
            CreateTable(enmTables.StockDetailTable.ToString)
            CreateTable(enmTables.StockMasterTable.ToString)
            CreateTable(enmTables.tblVoucher.ToString)
            CreateTable(enmTables.tblVoucherDetail.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function TransferData(ByVal dtpDateFrom As DateTime) As Boolean
        ConStringBuilder.Item("Initial Catalog") = "UPDATE_" & Me.dtpDateFrom.Value.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ""
        Dim SQLClientCon As New SqlClient.SqlConnection(ConStringBuilder.ConnectionString)
        If SQLClientCon.State = ConnectionState.Closed Then SQLClientCon.Open()
        Dim trans As SqlClient.SqlTransaction = SQLClientCon.BeginTransaction
        Dim cmd As New SqlClient.SqlCommand
        Try

            cmd.Connection = SQLClientCon
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text

            Dim strSQL As String = String.Empty
            '' Voucher Master
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.tblVoucher" & " SELECT * FROM " & ConDBName & ".dbo.tblVoucher WHERE (Convert(varchar, Voucher_Date,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Voucher Detail
            strSQL = String.Empty
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.tblVoucherDetail" & " SELECT * FROM " & ConDBName & ".dbo.tblVoucherDetail WHERE Voucher_Id in(Select Voucher_Id From tblVoucher  WHERE (Convert(varchar, Voucher_Date,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102)))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Sales Order Master
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.SalesOrderMasterTable" & " SELECT * FROM " & ConDBName & ".dbo.SalesOrderMasterTable WHERE (Convert(varchar, SalesOrderDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Sales Order Detail
            strSQL = String.Empty
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.SalesOrderDetailTable" & " SELECT * FROM " & ConDBName & ".dbo.SalesOrderDetailTable WHERE SalesOrderId in(Select SalesOrderId From SalesOrderMasterTable  WHERE (Convert(varchar, SalesOrderDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102)))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Sales Master
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.SalesMasterTable" & " SELECT * FROM " & ConDBName & ".dbo.SalesMasterTable WHERE (Convert(varchar, SalesDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Sales Detail
            strSQL = String.Empty
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.SalesDetailTable" & " SELECT * FROM " & ConDBName & ".dbo.SalesDetailTable WHERE SalesId in(Select SalesId From SalesMasterTable  WHERE (Convert(varchar, SalesDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102)))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Sales Return Master
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.SalesReturnMasterTable" & " SELECT * FROM " & ConDBName & ".dbo.SalesReturnMasterTable WHERE (Convert(varchar, SalesReturnDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Sales Return Detail
            strSQL = String.Empty
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.SalesReturnDetailTable" & " SELECT * FROM " & ConDBName & ".dbo.SalesReturnDetailTable WHERE SalesReturnId in(Select SalesReturnId From SalesReturnMasterTable  WHERE (Convert(varchar, SalesReturnDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102)))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Purchase Order Master
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.PurchaseOrderMasterTable" & " SELECT * FROM " & ConDBName & ".dbo.PurchaseOrderMasterTable WHERE (Convert(varchar, PurchaseOrderDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Purchase Order Detail
            strSQL = String.Empty
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.PurchaseOrderDetailTable" & " SELECT * FROM " & ConDBName & ".dbo.PurchaseOrderDetailTable WHERE PurchaseOrderId in(Select PurchaseOrderId From PurchaseOrderMasterTable  WHERE (Convert(varchar, PurchaseOrderDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102)))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Purchase Return Master
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.PurchaseReturnMasterTable" & " SELECT * FROM " & ConDBName & ".dbo.PurchaseReturnMasterTable WHERE (Convert(varchar, PurchaseReturnDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Purchase Return Detail
            strSQL = String.Empty
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.PurchaseReturnDetailTable" & " SELECT * FROM " & ConDBName & ".dbo.PurchaseReturnDetailTable WHERE PurchaseReturnId in(Select PurchaseReturnId From PurchaseReturnMasterTable  WHERE (Convert(varchar, PurchaseReturnDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102)))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Receiving Master
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.ReceivingMasterTable" & " SELECT * FROM " & ConDBName & ".dbo.ReceivingMasterTable WHERE (Convert(varchar, ReceivingDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Receiving Detail
            strSQL = String.Empty
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.ReceivingDetailTable" & " SELECT * FROM " & ConDBName & ".dbo.ReceivingDetailTable WHERE ReceivingId in(Select ReceivingId From ReceivingMasterTable  WHERE (Convert(varchar, ReceivingDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102)))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Dispatch Master
            strSQL = "TRUNCATE TABLE UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.DispatchMasterTable INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.DispatchMasterTable" & " SELECT * FROM " & ConDBName & ".dbo.DispatchMasterTable WHERE (Convert(varchar, DispatchDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Dispatch Detail
            strSQL = String.Empty
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.DispatchDetailTable" & " SELECT * FROM " & ConDBName & ".dbo.DispatchDetailTable WHERE DispatchId in(Select DispatchId From DispatchMasterTable  WHERE (Convert(varchar, DispatchDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102)))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Production Master
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.ProductionMasterTable" & " SELECT * FROM " & ConDBName & ".dbo.ProductionMasterTable WHERE (Convert(varchar, Production_date,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Production Detail
            strSQL = String.Empty
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.ProductionDetailTable" & " SELECT * FROM " & ConDBName & ".dbo.ProductionDetailTable WHERE Production_Id in(Select Production_Id From ProductionMasterTable  WHERE (Convert(varchar, Production_Date,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102)))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Stock Adjustment Master
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.StockAdjustmentMaster" & " SELECT * FROM " & ConDBName & ".dbo.StockAdjustmentMaster WHERE (Convert(varchar, Doc_Date,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Stock Adjustment Detail
            strSQL = String.Empty
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.StockAdjustmentDetail" & " SELECT * FROM " & ConDBName & ".dbo.StockAdjustmentDetail WHERE SA_ID in(Select SA_ID From StockAdjustmentMaster  WHERE (Convert(varchar, Doc_Date,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102)))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Stock  Master
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.StockMasterTable" & " SELECT * FROM " & ConDBName & ".dbo.StockMasterTable WHERE (Convert(varchar, DocDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            '' Stock  Detail
            strSQL = String.Empty
            strSQL = "INSERT INTO UPDATE_" & dtpDateFrom.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & ".dbo.StockDetailTable" & " SELECT * FROM " & ConDBName & ".dbo.StockDetailTable WHERE StockTransId in(Select StockTransId From StockMasterTable  WHERE (Convert(varchar, DocDate,102) BETWEEN Convert(DateTime, '" & dtpDateFrom.ToString("yyyy-M-d 00:00:00") & "',102)  AND Convert(DateTime, '" & Now.ToString("yyyy-M-d 23:59:59") & "', 102)))"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            trans.Commit()

         
            Return True

        Catch ex As Exception
            ErrorStatus = "Unsuccessfully"
            trans.Rollback()
            Throw ex
        Finally
            SQLClientCon.Close()
        End Try

    End Function
    Public Function DB_Detach() As Boolean
        ConStringBuilder.Item("Initial Catalog") = "master"
        Dim SQLClientCon As New SqlClient.SqlConnection(ConStringBuilder.ConnectionString)
        If SQLClientCon.State = ConnectionState.Closed Then SQLClientCon.Open()
        Dim cmd As New SqlClient.SqlCommand
        Try


            cmd.Connection = SQLClientCon
            Dim strSQL As String = " use master " _
            & " Declare @DatabaseName nvarchar(50)" _
            & " SET @DatabaseName=N'UPDATE_" & Me.dtpDateFrom.Value.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & "'" _
            & " DECLARE @SQL varchar(4000) " _
            & " SET @SQL = '' " _
            & " SELECT @SQL = @SQL + 'Kill ' + Convert(varchar, SPId) + ';' " _
            & " FROM MASTER..SysProcesses " _
            & " WHERE DBId = DB_ID(@DatabaseName) AND SPId <> @@SPId " _
            & " EXEC(@SQL)"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
            SQLClientCon.Close()

            If SQLClientCon.State = ConnectionState.Closed Then SQLClientCon.Open()
            cmd.Connection = SQLClientCon
            Dim strSQL1 As String = "EXEC sp_detach_db @dbname = N'UPDATE_" & Me.dtpDateFrom.Value.ToString("yyyyMMdd") & "_" & Now.ToString("yyyyMMdd") & "'"
            cmd.CommandText = strSQL1
            cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            ErrorStatus = "Unsuccessfully"
            Throw ex
        Finally
            SQLClientCon.Close()
        End Try
    End Function
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            CreateDatabase(FromDate)
            CreateTables()
            TransferData(FromDate)
            DB_Detach()
            ErrorStatus = "Successfully"
        Catch ex As Exception
            ErrorStatus = "Unsuccessfully"
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        Try
            If e.ProgressPercentage < 101 Then Me.ToolStripProgressBar1.Value = e.ProgressPercentage
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Me.ToolStripLabel1.Visible = True
        Me.ToolStripProgressBar1.Visible = True
        Dim errorflg As Boolean = False
        Me.lblStatus.Visible = True
        Try
            FromDate = Me.dtpDateFrom.Value
            If Me.BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Dim i As Integer = 1
            Do While BackgroundWorker1.IsBusy
                BackgroundWorker1.ReportProgress(i)
                i += 1
                Application.DoEvents()
            Loop
            SaveLog(ErrorStatus)
            If ErrorStatus = "Successfully" Then
                Dim AddFiles As New List(Of String)
                '' IO File
                If Not IO.Directory.Exists("C:\Temp") Then
                    IO.Directory.CreateDirectory("C:\Temp")
                End If
                If IO.File.Exists(DataFile) Then
                    IO.File.Move(DataFile, "C:\Temp\" & DataFile.Substring(DataFile.LastIndexOf("\") + 1) & "")
                End If
                If IO.File.Exists(LogFile) Then
                    IO.File.Move(LogFile, "C:\Temp\" & LogFile.Substring(LogFile.LastIndexOf("\") + 1) & "")
                End If


                'Dim Zip As New Ionic.Zip.ZipFile
                'If IO.File.Exists("C:\Temp\" & DataFile.Substring(DataFile.LastIndexOf("\") + 1) & "") Then
                '    'Zip.AddFile("C:\Temp\" & DataFile.Substring(LogFile.LastIndexOf("\") + 1) & "")
                '    AddFiles.Add("C:\Temp\" & DataFile.Substring(LogFile.LastIndexOf("\") + 1) & "")
                'End If
                'If IO.File.Exists("C:\Temp\" & LogFile.Substring(LogFile.LastIndexOf("\") + 1) & "") Then
                '    'Zip.AddFile("C:\Temp\" & LogFile.Substring(LogFile.LastIndexOf("\") + 1) & "")
                '    AddFiles.Add("C:\Temp\" & LogFile.Substring(LogFile.LastIndexOf("\") + 1) & "")
                'End If
                'If IO.File.Exists("C:\Temp\DBFilePath.txt") Then
                '    'Zip.AddFile("C:\Temp\DBFilePath.txt")
                '    AddFiles.Add("C:\Temp\DBFilePath.txt")
                'End If
                'Zip.AddFiles(AddFiles)
                'Zip.Name = "Update_" & FromDate.ToString("yyyyMMdd") & "" & Now.ToString("yyyyMMdd") & ".zip"
                'Zip.Save()
                'Zip.Dispose()

            End If
            Me.ToolStripLabel1.Visible = False
            Me.ToolStripProgressBar1.Visible = False
            lblStatus.ForeColor = Color.Green
            Me.lblStatus.Text = ErrorStatus
        Catch ex As Exception
            errorflg = True
            lblStatus.ForeColor = Color.Red
            Me.lblStatus.Text = ErrorStatus
            ShowErrorMessage(ex.Message)
        Finally
            If errorflg = False Then
                'If IO.File.Exists("C:\Temp\DBFilePath.txt") Then
                '    IO.File.Delete("C:\Temp\DBFilePath.txt")
                'End If
                'If IO.File.Exists("C:\Temp\" & DataFile.Substring(DataFile.LastIndexOf("\") + 1) & "") Then
                '    IO.File.Delete("C:\Temp\" & DataFile.Substring(DataFile.LastIndexOf("\") + 1) & "")
                'End If
                'If IO.File.Exists("C:\Temp\" & LogFile.Substring(LogFile.LastIndexOf("\") + 1) & "") Then
                '    IO.File.Delete("C:\Temp\" & LogFile.Substring(LogFile.LastIndexOf("\") + 1) & "")
                'End If
            End If
        End Try
    End Sub
    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Function SaveLog(ByVal Status As String) As Boolean
        Try
            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim cmd As New OleDb.OleDbCommand("INSERT INTO tblDataTransferLog(LogDate, Status,UserName) Values('" & Now.ToString("yyyy-M-d hh:mm:ss tt") & "', '" & Status & "', '" & LoginUserName & "')", Con)
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public ConStringBuilder As OleDb.OleDbConnectionStringBuilder
    Public Function GetLastDate() As DateTime
        Try
            Dim dt As DataTable = GetDataTable("Select LogDate From tblDataTransferLog WHERE LogId IN(Select Max(LogId) From tblDataTransferLog WHERE Status=N'Successfully')")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0)
                Else
                    Return CDate("2007-1-1 00:00:00")
                End If
            Else
                Return CDate("2007-1-1 00:00:00")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmDataTransfer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ConStringBuilder = New OleDb.OleDbConnectionStringBuilder("Data Source=" & ConServerName & ";User ID=" & ConUserId & ";Password=" & ConPassword & ";Initial Catalog=master;Connect TimeOut=120")
            Me.dtpDateFrom.Value = GetLastDate()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetDatabasePath(ByVal DatabaseName As String)
        Try
            Dim dt As DataTable = GetDataTable("select * from master.dbo.sysdatabases where name='" & DatabaseName & "'")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    DataFile = dt.Rows(0).Item("FileName").ToString
                    LogFile = dt.Rows(0).Item("FileName").ToString.Substring(0, dt.Rows(0).Item("FileName").ToString.LastIndexOf(".")) & "_log.LDF"
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
