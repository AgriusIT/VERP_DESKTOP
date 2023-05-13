Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports SBUtility.Utility
Imports System.Net
Imports System.Web
Imports System.Text
Imports System.Drawing
Imports System.IO
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Xml

Public Class SMSTemplateDAL
    Enum enmSMSLog
        SMSLogID
        SMSLogDate
        SMSBody
        SMSType
        PhoneNo
        SentStatus
        SentDate
        DeliveryStatus
        DeliveryDate
        TransactionID
        ProcessLogID
        CreatedByUserID
        SentByUserID
    End Enum
    Public Shared Function AddSMSLog(ByVal objSMSLog As SMSLogBE, Optional ByVal objSMSParam As SMSParameters = Nothing) As Boolean
        Dim objCon As New SqlConnection(SQLHelper.CON_STR) ' Create object of sql client connection
        If objCon.State = ConnectionState.Closed Then objCon.Open() 'Database Connectivity Process
        Dim objTrans As SqlTransaction = objCon.BeginTransaction ' 'Start Transaction  
        Try
            If objSMSLog IsNot Nothing Then
                If objSMSParam IsNot Nothing Then

                    Dim strSMSBody As String = String.Empty
                    strSMSBody = objSMSLog.SMSBody.Replace("@AccountCode", objSMSParam.AccountCode).Replace("@AccountTitle", objSMSParam.AccountTitle).Replace("@DoucmentNo", objSMSParam.DocumentNo).Replace("@DocumentDate", IIf(objSMSParam.DocumentDate = "#12:00:00 AM#", String.Empty, objSMSParam.DocumentDate)).Replace("@OtherDocNo", objSMSParam.OtherDocNo).Replace("@Remarks", objSMSParam.Remarks).Replace("@Amount", objSMSParam.Amount).Replace("@Quantity", objSMSParam.Quantity).Replace("@ChequeNo", objSMSParam.ChequeNo).Replace("@ChequeDate", IIf(objSMSParam.ChequeDate = "#12:00:00 AM#", String.Empty, objSMSParam.ChequeDate)).Replace("@CompanyName", objSMSParam.CompanyName).Replace("@SIRIUS", "Automated by http://www.SIRIUS.net")
                    objSMSLog.SMSBody = strSMSBody
                End If
            Else
                Throw New Exception("Some of data is not provided.")
                Return False
            End If
            objSMSLog.PhoneNo = objSMSLog.PhoneNo.Replace("-", "").Replace(".", "").Replace("_", "").Replace("/", "").Replace("\", "").Replace("+", "")
            If objSMSLog.PhoneNo.Length <= 11 Then
                objSMSLog.PhoneNo = "92" & Microsoft.VisualBasic.Right(objSMSLog.PhoneNo, 10)
            Else
                objSMSLog.PhoneNo = objSMSLog.PhoneNo
            End If
            Dim strSQL As String = String.Empty 'Create Variable for INSERTION Query
            strSQL = "INSERT INTO tblSMSLog(SMSLogDate,SMSBody,PhoneNo,CreatedByUserID) " _
            & " VALUES(Convert(datetime,N'" & objSMSLog.SMSLogDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " _
            & " N'" & objSMSLog.SMSBody.Replace("'", "''") & "', " _
            & " N'" & objSMSLog.PhoneNo.Replace("'", "''") & "', " _
            & " " & objSMSLog.CreatedByUserID & "" _
            & " ) Select @@Identity "
            objSMSLog.SMSLogID = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL) 'Execure SQL Query

            objTrans.Commit() 'Record Save Process
            Return True

        Catch ex As Exception
            objTrans.Rollback() 'because some of data is not provided
            Throw New Exception("Some of data is not provided.")
        Finally
            objCon.Close() 'after safe record in database
        End Try
    End Function
    Public Shared Function SaveSMSLog(ByVal objSMSLog As SMSLogBE, Optional ByVal objSMSParam As SMSParameters = Nothing, Optional ByVal objTrans As OleDb.OleDbTransaction = Nothing) As Boolean
        Dim CMD As New OleDb.OleDbCommand
        Try
            If objSMSLog IsNot Nothing Then
                If objSMSParam IsNot Nothing Then
                    Dim strSMSBody As String = String.Empty
                    strSMSBody = objSMSLog.SMSBody.Replace("@AccountCode", objSMSParam.AccountCode).Replace("@AccountTitle", objSMSParam.AccountTitle).Replace("@DoucmentNo", objSMSParam.DocumentNo).Replace("@DocumentDate", IIf(objSMSParam.DocumentDate = "#12:00:00 AM#", String.Empty, objSMSParam.DocumentDate)).Replace("@OtherDocNo", objSMSParam.OtherDocNo).Replace("@Remarks", objSMSParam.Remarks).Replace("@Amount", objSMSParam.Amount).Replace("@Quantity", objSMSParam.Quantity).Replace("@ChequeNo", objSMSParam.ChequeNo).Replace("@ChequeDate", IIf(objSMSParam.ChequeDate = "#12:00:00 AM#", String.Empty, objSMSParam.ChequeDate)).Replace("@CompanyName", objSMSParam.CompanyName).Replace("@SIRIUS", "Automated by http://www.SIRIUS.net")
                    objSMSLog.SMSBody = strSMSBody
                End If
            Else
                Throw New Exception("Some of data is not provided.")
                Return False
            End If
            objSMSLog.PhoneNo = objSMSLog.PhoneNo.Replace("-", "").Replace(".", "").Replace("_", "").Replace("/", "").Replace("\", "").Replace("+", "")
            If objSMSLog.PhoneNo.Length <= 11 Then
                objSMSLog.PhoneNo = "92" & Microsoft.VisualBasic.Right(objSMSLog.PhoneNo, 10)
            Else
                objSMSLog.PhoneNo = objSMSLog.PhoneNo
            End If
            Dim strSQL As String = String.Empty 'Create Variable for INSERTION Query
            strSQL = "INSERT INTO tblSMSLog(SMSLogDate,SMSBody,PhoneNo,CreatedByUserID) " _
            & " VALUES(Convert(datetime,N'" & objSMSLog.SMSLogDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " _
            & " N'" & objSMSLog.SMSBody.Replace("'", "''") & "', " _
            & " N'" & objSMSLog.PhoneNo.Replace("'", "''") & "', " _
            & " " & objSMSLog.CreatedByUserID & "" _
            & " ) Select @@Identity "
            'objSMSLog.SMSLogID = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL) 'Execure SQL Query
            CMD.Connection = objTrans.Connection
            CMD.Transaction = objTrans
            CMD.CommandType = CommandType.Text
            CMD.CommandText = strSQL
            'objTrans.Commit() 'Record Save Process

            CMD.ExecuteNonQuery()


            Return True

        Catch ex As Exception
            objTrans.Rollback() 'because some of data is not provided
            Throw New Exception("Some of data is not provided.")
            'Finally
            '    objCon.Close() 'after safe record in database
        End Try
    End Function

    Public Shared Function UpdateSMSLog(ByVal objSMSLog As SMSLogBE) As Boolean
        Dim objCon As New SqlConnection(SQLHelper.CON_STR) ' Create object of sql client connection
        If objCon.State = ConnectionState.Closed Then objCon.Open() 'Database Connectivity Process
        Dim objTrans As SqlTransaction = objCon.BeginTransaction ' 'Start Transaction  
        Try

            objSMSLog.PhoneNo = objSMSLog.PhoneNo.Replace("-", "").Replace(".", "").Replace("_", "").Replace("/", "").Replace("\", "").Replace("+", "")
            If objSMSLog.PhoneNo.Length <= 11 Then
                objSMSLog.PhoneNo = "92" & Microsoft.VisualBasic.Right(objSMSLog.PhoneNo, 10)
            Else
                objSMSLog.PhoneNo = objSMSLog.PhoneNo
            End If

            Dim strSQL As String = String.Empty 'Create Variable for INSERTION Query
            strSQL = "Update tblSMSLog SET " _
            & " SMSLogDate = Convert(datetime,N'" & objSMSLog.SMSLogDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " _
            & " SMSBody=N'" & objSMSLog.SMSBody.Replace("'", "''") & "', " _
            & " SMSType=N'" & objSMSLog.SMSType.Replace("'", "''") & "', " _
            & " PhoneNo=N'" & objSMSLog.PhoneNo.Replace("'", "''") & "', " _
            & " SentStatus='Pending', " _
            & " SentDate=NULL, " _
            & " DeliveryStatus=NULL, " _
            & " DeliveryDate=NULL, " _
            & " TransactionID=NULL, " _
            & " ProcessLogID=NULL, " _
            & " CreatedByUserID=" & objSMSLog.CreatedByUserID & ", " _
            & " SentByUserID=NULL " _
            & " WHERE SMSLogId=" & objSMSLog.SMSLogID & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL) 'Execure SQL Query

            objTrans.Commit() 'Record Save Process
            Return True

        Catch ex As Exception
            objTrans.Rollback() 'because some of data is not provided
            Throw New Exception("Some of data is not provided.")
        Finally
            objCon.Close() 'after safe record in database
        End Try
    End Function
    Public Shared Function UpdateTransaction(ByVal objSMSLog As SMSLogBE) As Boolean
        Dim objCon As New SqlConnection(SQLHelper.CON_STR) ' Create object of sql client connection
        If objCon.State = ConnectionState.Closed Then objCon.Open() 'Database Connectivity Process
        Dim objTrans As SqlTransaction = objCon.BeginTransaction ' 'Start Transaction  
        Try
            Dim strSQL As String = String.Empty 'Create Variable for INSERTION Query
            strSQL = "Update tblSMSLog SET " _
            & " SentStatus=N'" & objSMSLog.SentStatus.Replace("'", "''") & "', " _
            & " SentDate=Convert(DateTime,N'" & objSMSLog.SentDate.ToString("yyyy-M-d hh:mm:ss tt ") & "',102), " _
            & " DeliveryStatus=N'" & objSMSLog.DeliveryStatus.Replace("'", "''") & "', " _
            & " DeliveryDate=Convert(DateTime,N'" & objSMSLog.DeliveryDate.ToString("yyyy-M-d hh:mm:ss tt ") & "',102),  " _
            & " TransactionID=" & objSMSLog.TransactionID & ", " _
            & " ProcessLogID=" & objSMSLog.ProcessLogID & ", " _
            & " SentByUserID=" & objSMSLog.SentByUserID & " " _
            & " WHERE SMSLogId=" & objSMSLog.SMSLogID & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL) 'Execure SQL Query

            objTrans.Commit() 'Record Save Process
            Return True

        Catch ex As Exception
            objTrans.Rollback() 'because some of data is not provided
            Throw New Exception("Some of data is not provided.")
        Finally
            objCon.Close() 'after safe record in database
        End Try
    End Function
    Public Shared Function ErrorLog(ByVal ErrorDate, ByVal Description, ByVal ProcessLogID, ByVal SMSLogId, ByVal ErrorCode) As Boolean
        Dim objCon As New SqlConnection(SQLHelper.CON_STR) ' Create object of sql client connection
        If objCon.State = ConnectionState.Closed Then objCon.Open() 'Database Connectivity Process
        Dim objTrans As SqlTransaction = objCon.BeginTransaction ' 'Start Transaction  
        Try
            Dim strSQL As String = String.Empty 'Create Variable for INSERTION Query
            strSQL = "INSERT INTO tblErrorLog(ErrorDate, Description,ProcessLogID, SMSLogId, ErrorCode) VALUES(Convert(DateTime,N'" & CDate(ErrorDate).ToString("yyyy-M-d hh:mm:ss tt ") & "',102),N'" & Description.ToString.Replace("'", "''") & "'," & ProcessLogID & "," & SMSLogId & ",N'" & ErrorCode.ToString.Replace("'", "''") & "' )"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL) 'Execure SQL Query
            objTrans.Commit() 'Record Save Process
            Return True

        Catch ex As Exception
            objTrans.Rollback() 'because some of data is not provided
            Throw New Exception("Some of data is not provided.")
        Finally
            objCon.Close() 'after safe record in database
        End Try
    End Function
    Public Shared Function UpdateErrorLog(ByVal ErrorDate, ByVal Description, ByVal ProcessLogID, ByVal SMSLogId) As Boolean
        Dim objCon As New SqlConnection(SQLHelper.CON_STR) ' Create object of sql client connection
        If objCon.State = ConnectionState.Closed Then objCon.Open() 'Database Connectivity Process
        Dim objTrans As SqlTransaction = objCon.BeginTransaction ' 'Start Transaction  
        Try
            Dim strSQL As String = String.Empty 'Create Variable for INSERTION Query
            strSQL = "Update tblSMSLog Set DeliveryDate=Convert(datetime,N'" & CDate(ErrorDate).ToString("yyyy-M-d hh:mm:ss tt ") & "',102),DeliveryStatus=N'" & Description.ToString.Replace("'", "''") & "',TransactionId=" & ProcessLogID & ", ProcessLogId=0 WHERE SMSLogID=" & SMSLogId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL) 'Execure SQL Query
            objTrans.Commit() 'Record Save Process
            Return True

        Catch ex As Exception
            objTrans.Rollback() 'because some of data is not provided
            Throw New Exception("Some of data is not provided.")
        Finally
            objCon.Close() 'after safe record in database
        End Try
    End Function
    Public Shared Function GetAllRecordsByPendingSMS() As List(Of SMSLogBE)

        Try

            Dim objSMSLogList As New List(Of SMSLogBE)
            Dim strSQL As String = String.Empty
            strSQL = "Select * From tblSMSLog WHERE (DeliveryStatus='Error' Or DeliveryStatus Is Null) AND Len(PhoneNo) > 9"
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)
            Dim objSMSLog As SMSLogBE
            If dr.HasRows Then
                Do While dr.Read
                    objSMSLog = New SMSLogBE
                    objSMSLog.SMSLogID = dr.GetValue(enmSMSLog.SMSLogID)
                    objSMSLog.SMSBody = dr.GetValue(enmSMSLog.SMSBody).ToString
                    objSMSLog.CreatedByUserID = Val(dr.GetValue(enmSMSLog.CreatedByUserID).ToString)
                    If IsDBNull(dr.GetValue(enmSMSLog.DeliveryDate)) Then
                        objSMSLog.DeliveryDate = Nothing
                    Else
                        objSMSLog.DeliveryDate = dr.GetValue(enmSMSLog.DeliveryDate)
                    End If
                    objSMSLog.DeliveryStatus = dr.GetValue(enmSMSLog.DeliveryStatus).ToString
                    objSMSLog.PhoneNo = dr.GetValue(enmSMSLog.PhoneNo).ToString
                    objSMSLog.ProcessLogID = Val(dr.GetValue(enmSMSLog.ProcessLogID).ToString)
                    objSMSLog.SentByUserID = Val(dr.GetValue(enmSMSLog.SentByUserID).ToString)
                    If IsDBNull(dr.GetValue(enmSMSLog.SentDate)) Then
                        objSMSLog.SentDate = Nothing
                    Else
                        objSMSLog.SentDate = dr.GetValue(enmSMSLog.SentDate)
                    End If
                    objSMSLog.SentStatus = dr.GetValue(enmSMSLog.SentStatus).ToString
                    If IsDBNull(dr.GetValue(enmSMSLog.SMSLogDate)) Then
                        objSMSLog.SMSLogDate = Nothing
                    Else
                        objSMSLog.SMSLogDate = dr.GetValue(enmSMSLog.SMSLogDate)
                    End If
                    objSMSLog.SMSType = dr.GetValue(enmSMSLog.SMSType).ToString
                    objSMSLog.TransactionID = Val(dr.GetValue(enmSMSLog.TransactionID).ToString)
                    objSMSLogList.Add(objSMSLog)
                Loop
            End If
            Return objSMSLogList

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetAllRecordsBySentSMS() As List(Of SMSLogBE)

        Try

            Dim objSMSLogList As New List(Of SMSLogBE)
            Dim strSQL As String = String.Empty
            strSQL = "Select * From tblSMSLog WHERE SentStatus='Successfull'"
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)
            Dim objSMSLog As SMSLogBE
            If dr.HasRows Then
                Do While dr.Read
                    objSMSLog = New SMSLogBE
                    objSMSLog.SMSLogID = dr.GetValue(enmSMSLog.SMSLogID)
                    objSMSLog.SMSBody = dr.GetValue(enmSMSLog.SMSBody).ToString
                    objSMSLog.CreatedByUserID = Val(dr.GetValue(enmSMSLog.CreatedByUserID).ToString)
                    If IsDBNull(dr.GetValue(enmSMSLog.DeliveryDate)) Then
                        objSMSLog.DeliveryDate = Nothing
                    Else
                        objSMSLog.DeliveryDate = dr.GetValue(enmSMSLog.DeliveryDate)
                    End If
                    objSMSLog.DeliveryStatus = dr.GetValue(enmSMSLog.DeliveryStatus).ToString
                    objSMSLog.PhoneNo = dr.GetValue(enmSMSLog.PhoneNo).ToString
                    objSMSLog.ProcessLogID = Val(dr.GetValue(enmSMSLog.ProcessLogID).ToString)
                    objSMSLog.SentByUserID = Val(dr.GetValue(enmSMSLog.SentByUserID).ToString)
                    If IsDBNull(dr.GetValue(enmSMSLog.SentDate)) Then
                        objSMSLog.SentDate = Nothing
                    Else
                        objSMSLog.SentDate = dr.GetValue(enmSMSLog.SentDate)
                    End If
                    objSMSLog.SentStatus = dr.GetValue(enmSMSLog.SentStatus).ToString
                    If IsDBNull(dr.GetValue(enmSMSLog.SMSLogDate)) Then
                        objSMSLog.SMSLogDate = Nothing
                    Else
                        objSMSLog.SMSLogDate = dr.GetValue(enmSMSLog.SMSLogDate)
                    End If
                    objSMSLog.SMSType = dr.GetValue(enmSMSLog.SMSType).ToString
                    objSMSLog.TransactionID = Val(dr.GetValue(enmSMSLog.TransactionID).ToString)
                    objSMSLogList.Add(objSMSLog)
                Loop
            End If
            Return objSMSLogList

        Catch ex As Exception
            Throw ex
        End Try
    End Function

   
End Class
