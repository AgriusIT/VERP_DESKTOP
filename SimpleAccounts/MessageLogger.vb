Public Class MessageLogger

    Private Count As Integer  'To count the number of messages that have been logged so far
    Private Const SEPARATOR As String = "--------------------------------------------------------"
    Private Structure MessageLogType
        Const Detailed As Integer = 100
        Const Mini As Integer = 200
    End Structure

    Private Const DEFAULT_LOG_TYPE As Integer = MessageLogType.Detailed

    Public Sub New()
        Me.Count = 0  'Initialise the 
    End Sub
    Public Function Log(ByVal Msg As String) As Boolean

        Return Me.Log(New AgriusMessage(Msg))

    End Function
    Public Function Log(ByVal GMsg As AgriusMessage) As Boolean
        'Logs the message in StringBuilder
        '
        'StringBuilder.Appeend() method can generate "ArgumentOutOfRangeException" exception if the size of string in
        'StringBuilder exceeds "MaxCapacity" of StringBuilder.
        '
        'TO-DO: Implement check for the above mentioned situation

        Try
            If MessageLogger.DEFAULT_LOG_TYPE = MessageLogger.MessageLogType.Mini Then
                ModGlobel.SysMessages.Append(GMsg.ToStringMini()).AppendLine()
            ElseIf MessageLogger.DEFAULT_LOG_TYPE = MessageLogger.MessageLogType.Detailed Then
                ModGlobel.SysMessages.Append(GMsg.ToString()).AppendLine()
            Else
                ModGlobel.SysMessages.Append(GMsg.ToStringMini()).AppendLine()
            End If

            ModGlobel.SysMessages.Append(MessageLogger.SEPARATOR)
            ModGlobel.SysMessages.AppendLine()

            Me.Count += 1  'Increment the counter

            Return True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            Return False
        End Try

    End Function

    Public Function Clear() As Boolean
        'Clears the message log
        ModGlobel.SysMessages.Clear()

        Me.Count = 0  'Reset the counter

        Return True

    End Function

    Public Function GetAllMessages() As String

        Return ModGlobel.SysMessages.ToString

    End Function

    Public Function MessageCount() As Integer

        Return Me.Count

    End Function
End Class
