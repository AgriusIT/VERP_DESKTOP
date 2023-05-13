Imports SBModel
Imports SBUtility.Utility
Public Class AgriusMessage
    'Create by: Syed Irfan Ahmad
    'Date Created: 14 Feb 2018
    'Last Updated: 15 Feb 2018
    '
    Private Const DATE_FORMAT1 As String = "d MMM yyyy, h:mm:ss tt"  'Format Excample: 15 Feb 2018, 8:07:55 AM
    Private Const DEFAULT_DATE_FORMAT As String = DATE_FORMAT1

    Public Property Message As String
    Public Property ErrorNumber As Integer
    Public Property ErrorCode As String  'Error code
    Public Property SourceItem As String  'Where the error occured. It could be a Form or any other item
    Public Property SourceFunction As String  'Function name where the error originated
    Public Property LineNumber As Integer
    Public Property OccuranceDateTime As DateTime
    Public Property MessageType As MessageTypes
    Public Property Reason As String
    Public Property Details As String  'Detail if any
    Public Property Criticality As MessageCriticality  'Property describing how critical the message is

    Public Sub New()
        Me.Message = ""
        Me.ErrorNumber = 0
        Me.ErrorCode = ""
        Me.SourceItem = ""
        Me.SourceFunction = ""
        Me.LineNumber = 0
        Me.OccuranceDateTime = DateTime.Now
        Me.MessageType = MessageTypes.Other
        Me.Reason = ""
        Me.Details = ""
        Me.Criticality = MessageCriticality.Low
    End Sub
    Public Sub New(ByVal ErrMsg As String)
        Me.Message = ErrMsg & ""  'ErrMsg is concatenated with empty string to convery "Nothing" into an empty string
        Me.ErrorNumber = 0
        Me.ErrorCode = ""
        Me.SourceItem = ""
        Me.SourceFunction = ""
        Me.LineNumber = 0
        Me.OccuranceDateTime = DateTime.Now
        Me.MessageType = MessageTypes.Other
        Me.Reason = ""
        Me.Details = ""
        Me.Criticality = MessageCriticality.Low
    End Sub
    Public Sub New(ByVal ErrMsg As String, Optional ByVal ErrCode As String = "", Optional ByVal MsgType As MessageTypes = MessageTypes.LogicalError)
        Me.Message = ErrMsg
        Me.ErrorNumber = 0
        Me.ErrorCode = ErrCode
        Me.SourceItem = ""
        Me.SourceFunction = ""
        Me.LineNumber = 0
        Me.OccuranceDateTime = DateTime.Now
        Me.MessageType = MsgType
        Me.Reason = ""
        Me.Details = ""
        Me.Criticality = MessageCriticality.Low
    End Sub

    Public Sub New(ByVal e As Exception)
        Me.Message = e.Message
        Me.ErrorNumber = 0
        Me.ErrorCode = ""
        Me.SourceItem = e.Source
        Me.SourceFunction = ""
        Me.LineNumber = 0
        Me.OccuranceDateTime = DateTime.Now
        Me.MessageType = MessageTypes.Exception.ToString
        Me.Reason = ""
        Me.Details = "Exception Stack Trace:" & vbCrLf & e.StackTrace & vbCrLf & vbCrLf & "Help Link: " & e.HelpLink
        Me.Criticality = MessageCriticality.High
    End Sub

    Public Overrides Function ToString() As String
        'Converts the AgriusMessage into a String
        Dim ReturnValue As String = ""

        ReturnValue = "Message: " & Me.Message & vbCrLf & _
            "Error Number: " & Me.ErrorNumber & vbCrLf & _
            "Error Code: " & Me.ErrorCode & vbCrLf & _
            "Source Item: " & Me.SourceItem & vbCrLf & _
            "Source Function: " & Me.SourceFunction & vbCrLf & _
            "Line Number: " & Me.LineNumber & vbCrLf & _
            "Occurance DateTime: " & Me.OccuranceDateTime.ToString(AgriusMessage.DEFAULT_DATE_FORMAT) & vbCrLf & _
            "Message Type: " & Me.MessageType.ToString & vbCrLf & _
            "Reason: " & Me.Reason & vbCrLf & _
            "Criticality: " & Me.Criticality.ToString & vbCrLf & _
            "Details: " & Me.Details

        Return ReturnValue

    End Function

    Public Function ToStringMini() As String
        'Converts the AgriusMessage into a String
        Dim ReturnValue As String = ""

        ReturnValue = "Message: " & Me.Message & vbCrLf & _
            "Error Number: " & Me.ErrorNumber & vbCrLf & _
            "Error Code: " & Me.ErrorCode & vbCrLf & _
            "Occurance DateTime: " & Me.OccuranceDateTime.ToString(AgriusMessage.DEFAULT_DATE_FORMAT) & vbCrLf & _
            "Criticality: " & Me.Criticality.ToString & vbCrLf & _
            "Details: " & Me.Details

        Return ReturnValue

    End Function

End Class
