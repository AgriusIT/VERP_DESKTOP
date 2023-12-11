Public Class SalesInquiryMaster
    Public Property SalesInquiryId As Integer
    Public Property SalesInquiryNo As String
    Public Property SalesInquiryDate As DateTime
    Public Property CustomerId As Integer
    Public Property LocationId As Integer
    Public Property ContactPersonId As Integer
    Public Property CustomerInquiryNo As String
    Public Property IndentNo As String
    Public Property IndentDepartment As String
    Public Property CustomerInquiryDate As DateTime
    Public Property OldInquiryNo As String
    ''Start TFS3113
    Public Property Posted As Boolean
    Public Property Posted_UserName As String
    Public Property PostedDate As DateTime
    ''End TFS3113
    Public Property DueDate As DateTime
    Public OldInquiryDate As DateTime
    Public Property Remarks As String
    Public Property UserName As String
    Public Property DetailList As List(Of SalesInquiryDetail)
    Public Sub New()
        DetailList = New List(Of SalesInquiryDetail)
    End Sub

    Property Notification As AgriusNotifications

End Class
