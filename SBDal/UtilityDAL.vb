''/////////////////////////////////////////////////////////////////////////////////////////
''//                      Candela New
''/////////////////////////////////////////////////////////////////////////////////////////
''//-------------------------------------------------------------------------------------
''// File Name       : Daily Activity Report .. 
''// Programmer	     : Tariq Majeed
''// Creation Date	 : 16-July-2009
''// Description     : 
''// Function List   : 								                                    
''//											                                            
''//-------------------------------------------------------------------------------------
''// Date Modified     Modified by         Brief Description			                
''//------------------------------------------------------------------------------------
''/////////////////////////////////////////////////////////////////////////////////////////
''27-10-15 TASKM2710151 Imran Ali: Create Duplication Voucher On Update Mode.
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBUtility.Utility
Imports SBModel
Imports System.Net
Imports System.Security.Cryptography

Public Class UtilityDAL

    Public Function AddFormToFavourite(FormName As String, UserId As Integer) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction

        Try
            Dim strSQL As String = String.Empty

            strSQL = " If Not EXISTS(Select * from tblFavouriteForms where FormName='" & FormName & "' and UserId='" & UserId & "' ) " & _
                     " Insert into tblFavouriteForms (FormName, UserId) values('" & FormName & "', " & UserId & ") " & _
                     " Else " & _
                     " Delete from tblFavouriteForms where FormName='" & FormName & "' and UserId='" & UserId & "' "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            If Not conn Is Nothing AndAlso conn.State = ConnectionState.Open Then conn.Close()
            Throw ex
        Finally
            If Not conn Is Nothing AndAlso conn.State = ConnectionState.Open Then conn.Close()
        End Try

    End Function

    Public Function CheckFavouriteForm(FormName As String, UserId As String) As Boolean
        Try
            Dim strSQL As String = String.Empty
            Dim dr As SqlDataReader
            strSQL = "Select * from tblFavouriteForms where FormName='" & FormName & "' and UserId='" & UserId & "' "
            dr = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)
            If dr.HasRows Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Encrypt(ByVal strText As String) As String
        Dim strEncrKey As String = "simpleaccounts"
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Try
            Dim bykey() As Byte = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Left(strEncrKey, 8))
            Dim InputByteArray() As Byte = System.Text.Encoding.UTF8.GetBytes(strText)
            Dim des As New DESCryptoServiceProvider
            Dim ms As New IO.MemoryStream
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(bykey, IV), CryptoStreamMode.Write)
            cs.Write(InputByteArray, 0, InputByteArray.Length)
            cs.FlushFinalBlock()
            Dim strEncrypt As String = Convert.ToBase64String(ms.ToArray())
            ms.Close()
            cs.Close()
            des.Clear()
            Return strEncrypt
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
    Public Shared Function Decrypt(ByVal strText As String) As String
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim sDecrKey As String = "simpleaccounts"
        Dim inputByteArray(strText.Length) As Byte
        Try
            Dim byKey() As Byte = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Left(sDecrKey, 8))
            Dim des As New DESCryptoServiceProvider
            inputByteArray = Convert.FromBase64String(strText)
            Dim ms As New IO.MemoryStream
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Dim strDecrypt = encoding.GetString(ms.ToArray())
            ms.Close()
            cs.Close()
            des.Clear()
            Return strDecrypt
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
    Public Shared Function CreateDuplicationVoucher(Voucher_Id As Integer, ActivityLog As String, LoginUserId As Integer, LoginUserName As String, trans As SqlClient.SqlTransaction) As Boolean

        Dim cmd As New SqlClient.SqlCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        cmd.CommandTimeout = 120

        Try


            Dim strSQL As String = String.Empty
            Dim dttmpVoucherColumns As New DataTable
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='tmptblVoucher' AND Column_Name <> 'tmp_Voucher_Id'"
            dttmpVoucherColumns = UtilityDAL.GetDataTable(strSQL, trans)
            dttmpVoucherColumns.AcceptChanges()
            Dim strtmpColumns As String = String.Empty
            For Each r As DataRow In dttmpVoucherColumns.Rows
                If strtmpColumns.Length > 0 Then
                    strtmpColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strtmpColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next


            Dim dtvouchercolumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='tblVoucher' AND column_Name in (Select Column_Name From information_schema.columns where table_name='tmptblVoucher')"
            dtvouchercolumns = UtilityDAL.GetDataTable(strSQL, trans)
            dtvouchercolumns.AcceptChanges()
            Dim strColumns As String = String.Empty
            For Each r As DataRow In dtvouchercolumns.Rows
                If strColumns.Length > 0 Then
                    strColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next


            strSQL = ""
            strSQL = "INSERT INTO tmptblVoucher(" & strtmpColumns.ToString & ") Select " & strColumns & ", Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & LoginUserId & ", N'" & LoginUserName.Replace("'", "''") & "', N'" & System.Environment.MachineName.ToString.Replace("'", "''") & "',N'" & ActivityLog.Replace("'", "''") & "'  From tblVoucher WHERE Voucher_Id=" & Voucher_Id & ""
            cmd.CommandText = ""
            cmd.CommandText = strSQL
            Dim intVoucherID As Integer = cmd.ExecuteScalar()


            Dim dttmpVoucherDetailColumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='tmptblVoucherDetail' AND Column_Name <> 'tmp_Voucher_Detail_Id'"
            dttmpVoucherDetailColumns = UtilityDAL.GetDataTable(strSQL, trans)
            dttmpVoucherDetailColumns.AcceptChanges()
            Dim strtmpDetailColumns As String = String.Empty
            For Each r As DataRow In dttmpVoucherDetailColumns.Rows
                If strtmpDetailColumns.Length > 0 Then
                    strtmpDetailColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strtmpDetailColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next

            Dim dtvoucherDetailcolumns As New DataTable
            strSQL = ""
            strSQL = "Select * From Information_Schema.Columns Where Table_Name='tblVoucherDetail' AND column_Name in (Select Column_Name From information_schema.columns where table_name='tmptblVoucherDetail')"
            dtvoucherDetailcolumns = UtilityDAL.GetDataTable(strSQL, trans)
            dtvoucherDetailcolumns.AcceptChanges()
            Dim strDetailColumns As String = String.Empty
            For Each r As DataRow In dtvoucherDetailcolumns.Rows
                If strDetailColumns.Length > 0 Then
                    strDetailColumns += "," & "[" & r.Item("Column_Name").ToString & "]"
                Else
                    strDetailColumns = "[" & r.Item("Column_Name").ToString & "]"
                End If
            Next


            strSQL = ""
            strSQL = "INSERT INTO tmptblVoucherDetail(" & strtmpDetailColumns & ") Select Ident_Current('tmptblVoucher'), " & strDetailColumns & " From tblVoucherDetail WHERE Voucher_ID=" & Voucher_Id & ""
            cmd.CommandText = ""
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()


            'trans.Commit()

            Return True


        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally

        End Try
    End Function

    Public Shared Function GetMac() As String

        Dim strMACAddresses As New List(Of String)
        Dim nic As NetworkInformation.NetworkInterface = Nothing
        Dim mac_Address As String = String.Empty
        For Each nic In NetworkInformation.NetworkInterface.GetAllNetworkInterfaces
            strMACAddresses.Add(nic.GetPhysicalAddress().ToString)
            Return nic.GetPhysicalAddress().ToString
            If strMACAddresses.Count > 0 Then Exit For
        Next

        If strMACAddresses.Count > 0 Then
            Return strMACAddresses(0).ToString
        Else
            Return String.Empty
        End If

    End Function


    Public Shared Function IsMachineVerified(ByVal MachineName As String) As Boolean
        Dim strSQL As String
        Try
            'strSQL = "Select * from tblSystemList Where SystemName='" & MachineName & "'"
            'Dim dr As DataRow = ReturnDataRow(strSQL)
            ''If Not dr Is Nothing Then
            ''    If IsDBNull(dr.Item("SystemID")) Then
            ''        Return False
            ''    Else
            ''        Dim strId As String = GetMac.ToString
            ''        If Not dr.Item("SystemID").ToString = strId Then
            ''            Return False
            ''        End If
            ''    End If
            ''Else
            ''    Return False
            ''End If
            ''Return True
            'Dim FlagId As Boolean = False
            'Dim strId As String = GetMac.ToString
            'If dr IsNot Nothing Then
            '    For Each strMac As String In dr.Item("SystemId").ToString.Split(";")
            '        If Not strMac = strId Then
            '            FlagId = False
            '        Else
            '            FlagId = True
            '            Exit For
            '        End If
            '    Next
            'End If
            'Return FlagId

            Dim FlagId As Boolean = False
            Dim str() As String = {}
            strSQL = String.Empty
            strSQL = "Select * From tblSystemList"
            Dim dt As DataTable = GetDataTable(strSQL)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If FlagId = True Then Exit For
                        str = dr.Item("SystemId").ToString.Split(";")
                        If str.Length > 0 Then
                            For Each strSystemId As String In str
                                If FlagId = True Then Exit For
                                If strSystemId = GetMac.ToString Then
                                    FlagId = True
                                End If
                            Next
                        End If
                    Next
                End If
            End If
            Return FlagId
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetNextDocNo(ByVal Prefix As String, ByVal Length As Integer, ByVal tableName As String, ByVal FieldName As String, Optional ByVal trans As SqlClient.SqlTransaction = Nothing, Optional ByVal TotalLength As Integer = 0) As String
        Dim str As String = 0
        'Dim strSql As String = "select  +'" & Prefix & "-'+  replicate('0',(" & Length & " - len(replace(isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ,10))),0)+1,6,0)))) + replace(isnull(max(convert(integer,substring(" & tableName & "." & FieldName & "," & Prefix.Length + 2 & ",10))),0)+1,6,0) from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
        Dim strSql As String
        If Prefix = "" Then
            strSql = "select  isnull(max(convert(integer," & tableName & "." & FieldName & ")),0)+1 from " & tableName & " "
        Else
            strSql = "select  isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ," & Val(Prefix.Length + Length + 1) & "))),0)+1 from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
            If TotalLength > 0 Then
                strSql = strSql & " and len(" & tableName & "." & FieldName & ") <= " & TotalLength
            End If
        End If
        Dim dt As DataTable = UtilityDAL.GetDataTable(strSql, trans)
        If dt.Rows.Count > 0 Then
            str = dt.Rows(0).Item(0).ToString
        Else
            Return "Error"
        End If
        If Prefix = "" Then
            Return str.PadLeft(Length, "0")
        End If

        Return Prefix & "-" & str.PadLeft(Length, "0")


    End Function

    Public Shared Function GetNextDocNoOleDB(ByVal Prefix As String, ByVal Length As Integer, ByVal tableName As String, ByVal FieldName As String, Optional ByVal trans As OleDb.OleDbTransaction = Nothing) As String
        Dim str As String = 0
        'Dim strSql As String = "select  +'" & Prefix & "-'+  replicate('0',(" & Length & " - len(replace(isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ,10))),0)+1,6,0)))) + replace(isnull(max(convert(integer,substring(" & tableName & "." & FieldName & "," & Prefix.Length + 2 & ",10))),0)+1,6,0) from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
        Dim strSql As String
        If Prefix = "" Then
            strSql = "select  isnull(max(convert(integer," & tableName & "." & FieldName & ")),0)+1 from " & tableName & " "
        Else
            strSql = "select  isnull(max(convert(integer,substring (" & tableName & "." & FieldName & ", " & Prefix.Length + 2 & " ," & Val(Prefix.Length + Length + 1) & "))),0)+1 from " & tableName & " where " & tableName & "." & FieldName & " like '" & Prefix & "%'"
        End If
        Dim dt As DataTable = UtilityDAL.GetDataTableOleDB(strSql, trans)
        If dt.Rows.Count > 0 Then
            str = dt.Rows(0).Item(0).ToString
        Else
            Return "Error"
        End If
        If Prefix = "" Then
            Return str.PadLeft(Length, "0")
        End If

        Return Prefix & "-" & str.PadLeft(Length, "0")


    End Function

    Public Shared Function GetLanguageBaseControls() As DataTable
        Dim objDA As SqlClient.SqlDataAdapter
        Try
            Dim strSQL As String
            strSQL = "SELECT  tblLanguageBasedControls.FORMID  [Form ID],  tblSecurityForm.FORM_NAME_New [Form Name], tblLanguageBasedControls.ControlType [Control Type], tblLanguageBasedControls.ControlName [Control Name], " & EnumLanguagConstants.ENGL_US.ToString & "," & EnumLanguagConstants.URDU_PK.ToString & ", " & EnumLanguagConstants.ARABIC_UAE.ToString & " " _
            & " FROM         tblLanguageBasedControls Left Outer JOIN tblSecurityForm ON tblLanguageBasedControls.FormID = tblSecurityForm.FORM_ID " _
            & " ORDER BY [Form Name], [Control Name]"

            objDA = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim MyCollectionList As New DataTable("MyLanguageBasedControlList")
            objDA.Fill(MyCollectionList)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing

        End Try
    End Function



    Friend Shared Sub BuildActivityLog(ByVal Log As ActivityLog, ByVal trans As SqlTransaction)
        Try
            Dim strSQL As String
            Dim strLogComments As String = String.Empty
            'If Log.LogComments IsNot Nothing Then
            '    strLogComments = Log.LogComments
            'Else
            '    strLogComments = String.Empty
            'End If
            Dim strUserName As String = String.Empty
            Dim dt As New DataTable
            dt = GetDataTable("Select User_Id, User_Code From tblUser WHERE User_ID=" & Log.UserID & "", trans)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    strUserName = SymmetricEncryption.Decrypt(dt.Rows(0).Item(1).ToString)
                End If
            End If

            If Not Log.ActivityName.ToString = "Login" Then
                Log.LogComments = "" & strUserName & " " & Log.ActivityName.ToString & " Record " & Log.RefNo & " On " & Log.FormCaption.ToString & " " & Date.Now.ToString("yyyy-M-d h:mm:ss tt")
            Else
                Log.LogComments = "" & strUserName & " " & Log.ActivityName.ToString & " " & Date.Now.ToString("yyyy-M-d h:mm:ss tt")
            End If

            '            strLogComments = Log.LogComments.ToString
            '            LogID	numeric(18, 0)	Unchecked
            'LogApplicationName	varchar(100)	Checked
            'LogFormCaption	varchar(100)	Checked
            'LogActivityName	varchar(30)	Checked
            'LogUserID	numeric(18, 0)	Checked
            'LogRecordType	varchar(1500)	Checked
            'LogRecordRefNo	varchar(1500)	Checked
            'LogDateTime	datetime	Checked
            'LogComments	varchar(1500)	Checked
            'LogSystem	nvarchar(300)	Checked
            'TotalAmount	float	Checked
            'TotalQty	float	Checked
            'AccessKey	nvarchar(300)	Checked
            'LogFormName	nvarchar(300)	Checked
            '            Unchecked()

            strSQL = "INSERT INTO TblActivityLog (LogApplicationName, LogFormCaption, LogActivityName, LogUserID, LogRecordType, LogRecordRefNo, LogDateTime, LogComments, LogFormName) " _
            & " VALUES (N'" & Log.ApplicationName.Trim.Replace("'", "''") & "',N'" & Log.FormCaption.Trim.Replace("'", "''") & "',N'" & Log.ActivityName.Trim.ToString.Replace("'", "''") & "','" & Log.UserID & "', N'" & Log.RecordType.Trim.ToString.Replace("'", "''") & "', N'" & Log.RefNo & "', Convert(DateTime,'" & Now.ToString("yyyy-M-d h:mm:ss tt") & "',102), " & IIf(strLogComments = String.Empty, "NULL", "N'" & strLogComments.Trim.Replace("'", "''") & "'") & ", N'" & Log.FormName.Trim.Replace("'", "''") & "' )"
            ''Execute SQL 
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Friend Shared Sub BuildSQLLog(ByVal Log As ActivityLog, ByVal trans As SqlTransaction)
        Try
            Dim strSQL As String
            strSQL = "INSERT INTO tblGLDMLLog  (dml_sql, log_date, user_id) "
            ' & " VALUES( '" & Log.SQL.Trim.Replace("'", "''") & "' , getdate()   , " & Log.UserID & ")"
            ''Execute SQL 
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function funExecuteScriptFile(ByVal ApplicationStartuoPath As String, ByVal ScriptCollection As Collection, ByVal dblSchemaVer As Double) As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        ''Commit Transaction
        Dim trans As SqlTransaction = conn.BeginTransaction

        Dim strSQL As String

        Try


            For intIndex As Integer = 1 To ScriptCollection.Count

                Dim FileName As String = ApplicationStartuoPath & "\" & ScriptCollection.Item(intIndex)



                strSQL = System.IO.File.ReadAllText(FileName)

                'execute sql
                Call SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)


                'log to file
                Call funSaveToFile(ApplicationStartuoPath & "\" & "UpdateLog", strSQL, dblSchemaVer)


            Next intIndex


            strSQL = "Update tblRCMSConfiguration Set config_value ='" & dblSchemaVer & "' Where config_name='Schema_Version'"
            Call SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)

            trans.Commit()


            'log to file
            Call funSaveToFile(ApplicationStartuoPath & "\" & "UpdateLog", strSQL, dblSchemaVer)

        Catch ex As Exception
            trans.Rollback()
            If conn.State = ConnectionState.Open Then conn.Close()
            Throw ex

        End Try
    End Function


    ' This function is used save grid contents into disk
    Public Shared Function funSaveToFile(ByVal FileName As String, ByVal strSql As String, ByVal dblVersion As Double) As Boolean
        Try

            funSaveToFile = True
            Dim strFilePath As String = String.Empty

            System.IO.File.AppendAllText(FileName & ".Rtf", IO.FileMode.Append)

            funSaveToFile = True




        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ' Tariq Majeed Sheikh .. 
    ' This Function When Called Returns The Company Information .. 
    Public Shared Function setCompanyInfo(ByVal strLocationID As String) As DataTable

        Dim ObjDataAdapter As SqlClient.SqlDataAdapter
        Try

            Dim strSQL As String
            strSQL = " Select IsNull(location_name, '-') as CompanyName, IsNull(location_address, '-') as CompanyAddress, IsNull(location_phone, '-') as CompanyPhone, IsNull(location_fax, '-') as CompanyFax, IsNull(location_url, '-')as CompanyURL from tblGlDefLocation Where location_id = " & strLocationID


            ObjDataAdapter = New SqlClient.SqlDataAdapter(strSQL, SQLHelper.CON_STR)

            Dim myDataTable As New DataTable("ComapanyInfo")
            ObjDataAdapter.Fill(myDataTable)


            Return myDataTable

        Catch ex As SqlException
            Throw ex

        Catch ex As Exception
            Throw ex

        Finally
            ObjDataAdapter = Nothing

        End Try


    End Function
    Public Shared Function GetCurrentServerDate() As DateTime
        Dim str As String = String.Empty
        Dim dateTime As DateTime
        'Dim trans As SqlTransaction
        Try
            str = "Select GetDate()"
            dateTime = SQLHelper.ExecuteScaler(SQLHelper.CON_STR, CommandType.Text, str)
            Return dateTime
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function getStock(ByVal Id As Integer, Optional ByVal trans As SqlTransaction = Nothing) As Integer
        Try

            Dim strSQL As String = "SELECT Stock  FROM vw_ArticleStock  WHERE(ArticleId = " & Id & ")"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL, trans)
            If dt Is Nothing Then Return False
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)(0)
            Else
                Return 0
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Shared Function GetDataTable(ByVal strSql As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim ObjCon As SqlClient.SqlConnection
        Dim objDA As SqlClient.SqlDataAdapter
        Dim Objcmd As SqlClient.SqlCommand

        'ObjCon = New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If trans Is Nothing Then
            ObjCon = New SqlClient.SqlConnection(SQLHelper.CON_STR)
        Else
            ObjCon = trans.Connection
        End If
        If Not ObjCon.State = 1 Then ObjCon.Open()
        Objcmd = New SqlClient.SqlCommand(strSql)

        Try

            Objcmd.CommandTimeout = 300
            Objcmd.Connection = ObjCon
            Objcmd.Transaction = trans

            objDA = New SqlClient.SqlDataAdapter
            objDA.SelectCommand = Objcmd

            Dim MyCollectionList As New DataTable
            objDA.Fill(MyCollectionList)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
            If trans Is Nothing Then
                If ObjCon.State = ConnectionState.Open Then ObjCon.Close()
                ObjCon.Dispose()
            End If

        End Try
    End Function
    Public Shared Function GetDataTableOleDB(ByVal strSql As String, Optional ByVal trans As OleDb.OleDbTransaction = Nothing) As DataTable
        Dim ObjCon As OleDb.OleDbConnection
        Dim objDA As OleDb.OleDbDataAdapter
        Dim Objcmd As OleDb.OleDbCommand

        'ObjCon = New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If trans Is Nothing Then
            ObjCon = New OleDb.OleDbConnection(SQLHelper.CON_STR)
        Else
            ObjCon = trans.Connection
        End If
        If Not ObjCon.State = 1 Then ObjCon.Open()
        Objcmd = New OleDb.OleDbCommand(strSql)

        Try

            Objcmd.CommandTimeout = 300
            Objcmd.Connection = ObjCon
            Objcmd.Transaction = trans

            objDA = New OleDb.OleDbDataAdapter
            objDA.SelectCommand = Objcmd

            Dim MyCollectionList As New DataTable
            objDA.Fill(MyCollectionList)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
            If trans Is Nothing Then
                If ObjCon.State = ConnectionState.Open Then ObjCon.Close()
                ObjCon.Dispose()
            End If

        End Try
    End Function
    Public Shared Function OleDbGetDataTable(ByVal strSql As String, Optional ByVal trans As OleDb.OleDbTransaction = Nothing) As DataTable
        Dim ObjCon As OleDb.OleDbConnection
        Dim objDA As OleDb.OleDbDataAdapter
        Dim Objcmd As OleDb.OleDbCommand

        'ObjCon = New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If trans Is Nothing Then
            ObjCon = New OleDb.OleDbConnection(SQLHelper.CON_STR)
        Else
            ObjCon = trans.Connection
        End If
        If Not ObjCon.State = 1 Then ObjCon.Open()
        Objcmd = New OleDb.OleDbCommand(strSql)

        Try

            Objcmd.CommandTimeout = 300
            Objcmd.Connection = ObjCon
            Objcmd.Transaction = trans

            objDA = New OleDb.OleDbDataAdapter
            objDA.SelectCommand = Objcmd

            Dim MyCollectionList As New DataTable
            objDA.Fill(MyCollectionList)

            Return MyCollectionList

        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
            If trans Is Nothing Then
                If ObjCon.State = ConnectionState.Open Then ObjCon.Close()
                ObjCon.Dispose()
            End If

        End Try
    End Function

    Public Shared Function ReturnDataRow(ByVal strSql As String, Optional ByVal trans As SqlTransaction = Nothing) As DataRow
        Dim Objcmd As SqlClient.SqlCommand
        Dim objDA As New SqlDataAdapter
        Dim objDS As New DataSet
        Dim ObjCon As SqlClient.SqlConnection

        If trans Is Nothing Then
            ObjCon = New SqlClient.SqlConnection(SQLHelper.CON_STR)
        Else
            ObjCon = trans.Connection
        End If
        'If Not ObjCon.State = 1 Then ObjCon.Open()
        Objcmd = New SqlClient.SqlCommand(strSql)
        Try

            'Dim trans As SqlTransaction = conn.BeginTransaction
            Objcmd.Connection = ObjCon
            Objcmd.CommandTimeout = 90000
            Objcmd.CommandType = CommandType.Text
            Objcmd.CommandText = strSql
            Objcmd.Transaction = trans
            objDA.SelectCommand = Objcmd

            objDA.Fill(objDS) '

            If objDS.Tables(0).Rows.Count > 0 Then
                Return objDS.Tables(0).Rows(0)
            Else
                Return Nothing
            End If
        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        Finally
            objDA = Nothing
            If trans Is Nothing Then
                If ObjCon.State = ConnectionState.Open Then ObjCon.Close()
                ObjCon.Dispose()
            End If
        End Try

    End Function


    Public Shared Function ExecuteQuery(ByVal strSql As String, Optional ByVal strCondition As String = "") As Boolean

        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        conn.Open()

        Dim trans As SqlTransaction = conn.BeginTransaction

        Try

            ' Execute SQL ..
            Convert.ToInt32(SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSql, Nothing))


            ' Commit Transaction .. 
            trans.Commit()


            ' Return ..
            Return True

        Catch ex As SqlException
            trans.Rollback()
            Throw ex

        Catch ex As Exception
            trans.Rollback()
            Throw ex

        Finally
            conn.Close()

        End Try

    End Function
    Public Shared Function GetVoucherTypeId(ByVal VoucherType As String) As Integer
        Try
            Dim str As String = "Select voucher_type_id From  tblDefVoucherType WHERE voucher_type='" & VoucherType & "'"
            Dim dt As New DataTable
            dt = GetDataTable(str)
            If dt Is Nothing Then Return False
            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows(0).Item(0).ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetVoucherIdByVoucherNo(ByVal VoucherNo As String) As Integer
        Try
            Dim str As String = String.Empty
            str = "Select Voucher_Id From tblVoucher WHERE Voucher_No='" & VoucherNo & "'"
            Dim dt As DataTable = GetDataTable(str)
            If dt Is Nothing Then Return False
            If dt.Rows.Count > 0 Then
                Return Convert.ToInt32(dt.Rows(0).Item(0).ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetConfigValue(ByVal config_type As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            Dim str As String = String.Empty
            str = "Select config_Value from ConfigValuesTable WHERE config_Type='" & config_type & "'"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(str, trans)
            If dt Is Nothing Then Return False
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return "Error"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetConfigValueOleDB(ByVal config_type As String, Optional ByVal trans As OleDb.OleDbTransaction = Nothing) As String
        Try
            Dim str As String = String.Empty
            str = "Select config_Value from ConfigValuesTable WHERE config_Type='" & config_type & "'"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTableOleDB(str, trans)
            If dt Is Nothing Then Return False
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return "Error"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetSerialNo(ByVal FirstChar As String, ByVal TableName As String, ByVal FieldSerialNo As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            Dim Serial As Integer = 0
            Dim SerialNo As String = String.Empty
            Dim str As String
            str = "Select ISNULL(Max(Right(" & FieldSerialNo & ",5)),0) as Serial From " & TableName & " WHERE LEFT(" & FieldSerialNo & "," & FirstChar.Length & ")='" & FirstChar & "'"
            Dim dt As DataTable = GetDataTable(str, trans)
            If Not dt Is Nothing Then
                If dt.Rows.Count < 0 Then
                    Serial = 0
                Else
                    Serial = CInt(dt.Rows(0).Item(0))
                End If
            End If
            Serial = Serial + 1
            SerialNo = CStr(FirstChar + Microsoft.VisualBasic.Right("00000" + CStr(Serial), 5))
            Return SerialNo
        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

    Public Shared Function GetSerialNoOleDB(ByVal FirstChar As String, ByVal TableName As String, ByVal FieldSerialNo As String, Optional ByVal trans As OleDb.OleDbTransaction = Nothing) As String
        Try
            Dim Serial As Integer = 0
            Dim SerialNo As String = String.Empty
            Dim str As String
            str = "Select ISNULL(Max(Right(" & FieldSerialNo & ",5)),0) as Serial From " & TableName & " WHERE LEFT(" & FieldSerialNo & "," & FirstChar.Length & ")='" & FirstChar & "'"
            Dim dt As DataTable = GetDataTableOleDB(str, trans)
            If Not dt Is Nothing Then
                If dt.Rows.Count < 0 Then
                    Serial = 0
                Else
                    Serial = CInt(dt.Rows(0).Item(0))
                End If
            End If
            Serial = Serial + 1
            SerialNo = CStr(FirstChar + Microsoft.VisualBasic.Right("00000" + CStr(Serial), 5))
            Return SerialNo
        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

End Class
Public Class SMSTemplatesDAL
    Public Function SaveSMSTemplate(ByVal Template As SMSTemplateParameter) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Delete From SMSTemplateTable WHERE TemplateKey='" & Template.Key.Replace("'", "''") & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "INSERT INTO SMSTemplateTable(TemplateKey,TemplateDescription) VALUES(N'" & Template.Key.Replace("'", "''") & "',N'" & Template.SMSTemplate.Replace("'", "''") & "' ) "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAllRecordByKey(ByVal TemplateKey As String) As String
        Try
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select * From SMSTemplateTable WHERE TemplateKey='" & TemplateKey.Replace("'", "''") & "'")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("TemplateDescription").ToString
                Else
                    Return String.Empty
                End If
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Enum enmTemplate
        ID
        TemplateKey
        TemplateDescription
    End Enum
    Public Shared Function SMSTemplateList() As List(Of SMSTemplateParameter)
        Try
            Dim objSMSTemplateList As New List(Of SMSTemplateParameter)
            Dim objSMSTemplate As SMSTemplateParameter
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, "Select * From SMSTemplateTable", Nothing)
            If dr.HasRows Then
                Do While dr.Read
                    objSMSTemplate = New SMSTemplateParameter
                    objSMSTemplate.ID = Val(dr.GetValue(enmTemplate.ID).ToString)
                    objSMSTemplate.Key = dr.GetValue(enmTemplate.TemplateKey).ToString
                    objSMSTemplate.SMSTemplate = dr.GetValue(enmTemplate.TemplateDescription).ToString
                    objSMSTemplateList.Add(objSMSTemplate)
                Loop
            End If
            Return objSMSTemplateList
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class

