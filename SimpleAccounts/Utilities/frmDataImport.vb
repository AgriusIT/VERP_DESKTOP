Imports System.Text
Imports System.IO
Imports Ionic.Zip
Imports SBUtility.Utility
Imports SBDal.SQLHelper
Imports SBDal
Public Class frmDataImport
    Dim dbName As String
    Dim DatabaseName As String = String.Empty
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
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim OpenDialogFile As New OpenFileDialog
            OpenDialogFile.Filter = "Zip|*.*zip"
            If OpenDialogFile.ShowDialog = Windows.Forms.DialogResult.OK Then
                If OpenDialogFile.CheckFileExists = True Then
                    Me.TextBox1.Text = OpenDialogFile.FileName
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.ToolStripProgressBar1.Visible = True
        Try
            If IO.File.Exists(Me.TextBox1.Text) Then
                UnZip(Me.TextBox1.Text)
            End If
            If BackgroundWorker1.IsBusy Then Exit Sub
            Dim i As Integer = 1
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                BackgroundWorker1.ReportProgress(i)
                i += 1
                Application.DoEvents()
            Loop
            Me.ToolStripProgressBar1.Visible = False
            msg_Information("Data import successfully")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub UnZip(ByVal FileName As String)
        Try
            If Not IO.Directory.Exists("C:\Temp") Then
                IO.Directory.CreateDirectory("C:\Temp")
            End If
            Dim FilePath = "C:\Temp"
            Dim UnZip As ZipFile = ZipFile.Read(FileName)
            For Each ExtractZip As ZipEntry In UnZip
                ExtractZip.Extract(FilePath, ExtractExistingFileAction.OverwriteSilently)
                Application.DoEvents()
            Next
            UnZip.Dispose()

            Dim strFileName As String = String.Empty
            If IO.File.Exists("C:\Temp\DBFilePath.txt") Then
                Dim Bytes() As Byte = {}
                Bytes = My.Computer.FileSystem.ReadAllBytes("C:\Temp\DBFilePath.txt")
                strFileName = System.Text.ASCIIEncoding.Unicode.GetString(Bytes)
                Db_Attach("C:\Temp" & strFileName.Substring(strFileName.LastIndexOf("\")) & "")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Db_Attach(ByVal FileName As String) As Boolean
        If Not IO.File.Exists(FileName) Then Return False
        ConStr.Item("Initial Catalog") = "master"
        Dim Conn As New SqlClient.SqlConnection(ConStr.ConnectionString)
        If Conn.State = ConnectionState.Closed Then Conn.Open()
        Try
            If Not IO.Directory.Exists("C:\Temp") Then
                IO.Directory.CreateDirectory("C:\Temp")
            End If
            dbName = FileName.Substring(FileName.LastIndexOf("\") + 1)
            DatabaseName = dbName.Substring(0, dbName.LastIndexOf("."))
            Dim strSQL As String = "EXEC sp_attach_db @dbname=N'" & DBName.Substring(0, DBName.LastIndexOf(".")) & "', @filename1=N'C:\Temp\" & DBName & "', @filename2=N'C:\Temp\" & DBName.Substring(0, DBName.LastIndexOf(".")) & "_log.LDF" & "'"
            Dim cm As New SqlClient.SqlCommand(strSQL, Conn)
            cm.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function
    Public Function DB_Detach() As Boolean
        ConStr.Item("Initial Catalog") = "master"
        Dim SQLClientCon As New SqlClient.SqlConnection(ConStr.ConnectionString)
        If SQLClientCon.State = ConnectionState.Closed Then SQLClientCon.Open()
        Dim cmd As New SqlClient.SqlCommand
        Try

            cmd.Connection = SQLClientCon
            Dim strSQL As String = " use master " _
            & " Declare @DatabaseName nvarchar(50)" _
            & " SET @DatabaseName=N'" & dbName.Substring(0, dbName.LastIndexOf(".")) & "'" _
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
            Dim strSQL1 As String = "EXEC sp_detach_db @dbname = N'" & dbName.Substring(0, dbName.LastIndexOf(".")) & "'"
            cmd.CommandText = strSQL1
            cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            'ErrorStatus = "Unsuccessfully"
            Throw ex
        Finally
            SQLClientCon.Close()
        End Try
    End Function
    Public Function ImportData(ByVal TableName As String) As Boolean
        Dim ClientCon As New SqlClient.SqlConnection(CON_STR)
        If ClientCon.State = ConnectionState.Closed Then ClientCon.Open()
        Dim trans As SqlClient.SqlTransaction = ClientCon.BeginTransaction
        Try
            Dim strTableColumns As String = String.Empty
            strTableColumns = "Select Name, DataType, Length, ID From(" _
                            & " Select syscolumns.name, systypes.name as DataType, syscolumns.length, ID from sysColumns INNER JOIN sysTypes on systypes.xType = syscolumns.xtype where id in (select Id from Sysobjects WHERE Name='" & TableName & "') AND sysTypes.Name <> 'sysname')objTable WHERE Name in  " _
                            & " (Select Name From( " _
                            & " Select " & DatabaseName & ".dbo.syscolumns.name, " & DatabaseName & ".dbo.systypes.name as DataType, " & DatabaseName & ".dbo.syscolumns.length from " & DatabaseName & ".dbo.sysColumns INNER JOIN " & DatabaseName & ".dbo.sysTypes on " & DatabaseName & ".dbo.systypes.xType = " & DatabaseName & ".dbo.syscolumns.xtype where id in (select Id from " & DatabaseName & ".dbo.Sysobjects WHERE Name='" & TableName & "') AND " & DatabaseName & ".dbo.sysTypes.Name <> 'sysname')objTable1) ORDER BY ID "
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strTableColumns, trans)
            Dim FilterColumn As String = String.Empty
            Dim Columns As String = String.Empty
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Columns = Columns & IIf(Columns.Length > 0, ",", "") & dt.Rows(i).Item(0).ToString
                        If i = 0 Then
                            FilterColumn = dt.Rows(i).Item(0).ToString
                        End If
                    End If
                End If
            Next

            Dim strSQL As String = String.Empty

            strSQL = "SET IDENTITY_INSERT " & TableName & " ON "
            ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = " INSERT INTO " & TableName & "(" & Columns & ") SELECT " & Columns & " From " & DatabaseName & ".dbo." & TableName & " WHERE " & FilterColumn & " NOT IN (Select " & FilterColumn & " From " & TableName & ")"
            ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "SET IDENTITY_INSERT " & TableName & " OFF "
            ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            ClientCon.Close()
        End Try
    End Function
    Public Sub InsertData()
        Try

            ImportData(enmTables.tblVoucher.ToString)
            ImportData(enmTables.tblVoucherDetail.ToString)
            ImportData(enmTables.SalesOrderMasterTable.ToString)
            ImportData(enmTables.SalesOrderDetailTable.ToString)
            ImportData(enmTables.SalesMasterTable.ToString)
            ImportData(enmTables.SalesDetailTable.ToString)
            ImportData(enmTables.SalesReturnMasterTable.ToString)
            ImportData(enmTables.SalesReturnDetailTable.ToString)
            ImportData(enmTables.PurchaseOrderMasterTable.ToString)
            ImportData(enmTables.PurchaseOrderDetailTable.ToString)
            ImportData(enmTables.ReceivingMasterTable.ToString)
            ImportData(enmTables.ReceivingDetailTable.ToString)
            ImportData(enmTables.DispatchMasterTable.ToString)
            ImportData(enmTables.DispatchDetailTable.ToString)
            ImportData(enmTables.PurchaseOrderMasterTable.ToString)
            ImportData(enmTables.PurchaseOrderDetailTable.ToString)
            ImportData(enmTables.StockAdjustmentMaster.ToString)
            ImportData(enmTables.StockAdjustmentDetail.ToString)
            ImportData(enmTables.StockMasterTable.ToString)
            ImportData(enmTables.StockDetailTable.ToString)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public ConStr As New SqlClient.SqlConnectionStringBuilder(CON_STR)
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            InsertData()
            DB_Detach()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        Try
            If Not e.ProgressPercentage > 100 Then Me.ToolStripProgressBar1.Value = e.ProgressPercentage
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class