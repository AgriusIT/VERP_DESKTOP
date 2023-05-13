Module ModActivityLog


#Region "Activity Log Entry"

    Enum EnumActions
        Save
        Update
        Delete
        Login
        LogOut
        Export
        Print
    End Enum

    Enum EnumRecordType
        NA
        Configuration
        Sales
        Purchase
        AccountTransaction
        StoreIssuence
        Security
        StockDispatch
        StockReceive
        Budget
        Report
        Lab
    End Enum

    Public Sub SaveActivityLog(ByVal LogApplicationName As String, ByVal LogFormCaption As String, ByVal LogActivityName As EnumActions, ByVal LogUserID As Integer, ByVal LogRecordType As EnumRecordType, ByVal LogRecordRefNo As String, Optional ByVal LogCommentsFlg As Boolean = True, Optional ByVal TotalAmount As Double = 0, Optional ByVal TotalQty As Double = 0, Optional ByVal AccessKey As String = "", Optional ByVal LogFormName As String = "")


        Try
            Dim strLogComments As String = String.Empty
            Dim str As String = String.Empty
            Dim StrUserCode As String = String.Empty
            'str = "Select * From tblUser WHERE User_Id=" & LogUserID
            'Dim dt As DataTable = GetDataTable(str)
            'If dt IsNot Nothing Then
            '    If dt.Rows.Count > 0 Then
            '        StrUserCode = Decrypt(dt.Rows(0).Item("User_Code").ToString)
            '    End If
            'End If
            StrUserCode = LoginUserCode
            If LogCommentsFlg = True Then
                If Not LogActivityName.ToString = "Login" Then
                    strLogComments = "" & StrUserCode & " " & LogActivityName.ToString & " Record " & LogRecordRefNo & " On " & LogFormCaption.ToString & " " & Date.Now.ToString("yyyy-M-d h:mm:ss tt")
                Else
                    strLogComments = "" & StrUserCode & " " & LogActivityName.ToString & " " & Date.Now.ToString("yyyy-M-d h:mm:ss tt")
                End If
            Else
                strLogComments = String.Empty
            End If

            Dim strSQL As String
            If Not LogActivityName = 3 Then
                ' Change due to date formate on 19-Sep-2013 by Ijaz
                strSQL = "INSERT INTO TblActivityLog (LogApplicationName, LogFormCaption, LogActivityName, LogUserID, LogRecordType, LogRecordRefNo, LogDateTime, LogComments, LogSystem,TotalAmount,TotalQty,AccessKey,LogFormName) " _
                & " VALUES (N'" & LogApplicationName.Replace("'", "''") & "',N'" & LogFormCaption.Replace("'", "''") & "',N'" & LogActivityName.ToString.Replace("'", "''") & "','" & LogUserID & "', N'" & LogRecordType.ToString.Replace("'", "''") & "', N'" & LogRecordRefNo & "',Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d h:mm:ss tt") & "',102), N'" & IIf(strLogComments = String.Empty, "NULL", strLogComments.Trim.Replace("'", "''")) & "', N'" & System.Environment.MachineName.ToString.Replace("'", "''") & "', " & TotalAmount & ", " & TotalQty & ",N'" & AccessKey.Replace("'", "''") & "',N'" & LogFormName.Replace("'", "''") & "')"
                '& " VALUES ('" & LogApplicationName.Replace("'", "''") & "','" & LogFormCaption.Replace("'", "''") & "','" & LogActivityName.ToString.Replace("'", "''") & "','" & LogUserID & "', '" & LogRecordType.ToString.Replace("'", "''") & "', '" & LogRecordRefNo & "', GetDate(), '" & IIf(strLogComments = String.Empty, "NULL", strLogComments.Trim.Replace("'", "''")) & "', '" & System.Environment.MachineName.ToString.Replace("'", "''") & "')"
            Else
                ' Change due to date formate on 19-Sep-2013 by Ijaz
                strSQL = "INSERT INTO TblActivityLog (LogApplicationName, LogFormCaption, LogActivityName, LogUserID, LogRecordType, LogRecordRefNo, LogDateTime, LogComments) " _
                & " VALUES (N'" & LogApplicationName.Replace("'", "''") & "',N'" & LogFormCaption.Replace("'", "''") & "',N'" & LogActivityName.ToString.Replace("'", "''") & "','" & LogUserID & "', N'" & LogRecordType.ToString.Replace("'", "''") & "', N'" & LogRecordRefNo & "',Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d h:mm:ss tt") & "',102), N'" & IIf(strLogComments = String.Empty, "NULL", strLogComments.Trim.Replace("'", "''")) & "')"
                '& " VALUES ('" & LogApplicationName.Replace("'", "''") & "','" & LogFormCaption.Replace("'", "''") & "','" & LogActivityName.ToString.Replace("'", "''") & "','" & LogUserID & "', '" & LogRecordType.ToString.Replace("'", "''") & "', '" & LogRecordRefNo & "', GetDate(), '" & IIf(strLogComments = String.Empty, "NULL", strLogComments.Trim.Replace("'", "''")) & "', '" & System.Environment.MachineName.ToString.Replace("'", "''") & "')"
            End If
            Dim cmd As New OleDb.OleDbCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSQL


            cmd.Connection = Con
            If Con.State = ConnectionState.Closed Then Con.Open()
            cmd.ExecuteNonQuery()
            Con.Close()

        Catch ex As Exception
            'Throw ex
        Finally

        End Try
    End Sub

#End Region
End Module
