Public Class frmRepeatedCustomerReport

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Try
            PrintReport()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Public Sub GetRecord()
    '    Try
    '        Dim IsOneChecked As Integer = 0
    '        Dim IsTwoChecked As Integer = 0
    '        Dim IsBothChecked As Integer = 0
    '        'Dim FromDate As New DateTime
    '        'Dim ToDate As New DateTime
    '        If Me.dtpInquiryFromDate.Checked = True AndAlso Me.dtpInquiryToDate.Checked = False Then
    '            IsOneChecked = 1
    '        ElseIf Me.dtpInquiryFromDate.Checked = False AndAlso Me.dtpInquiryToDate.Checked = True Then
    '            IsTwoChecked = 1
    '        ElseIf Me.dtpInquiryFromDate.Checked = True AndAlso Me.dtpInquiryToDate.Checked = True Then
    '            IsBothChecked = 1
    '        End If
    '        Dim dt As DataTable = GetDataTable("Exec SP_LiftWisePercentage '" & dtpInquiryFromDate.Value & "', '" & dtpInquiryToDate.Value & "', " & IsOneChecked & ", " & IsTwoChecked & ", " & IsBothChecked & "")
    '        dt.AcceptChanges()
    '        GridEX1.DataSource = dt
    '        GridEX1.RetrieveStructure()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs)
    '    Try
    '        GetRecord()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            PrintReport()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintReport()
        Try
            Dim IsOneChecked As Integer = 0
            Dim IsTwoChecked As Integer = 0
            Dim IsBothChecked As Integer = 0
            'Dim FromDate As New DateTime
            'Dim ToDate As New DateTime
            If Me.dtpInquiryFromDate.Checked = True AndAlso Me.dtpInquiryToDate.Checked = False Then
                IsOneChecked = 1
            ElseIf Me.dtpInquiryFromDate.Checked = False AndAlso Me.dtpInquiryToDate.Checked = True Then
                IsTwoChecked = 1
            ElseIf Me.dtpInquiryFromDate.Checked = True AndAlso Me.dtpInquiryToDate.Checked = True Then
                IsBothChecked = 1
            End If
            AddRptParam("@NO", txtNo.Text)
            AddRptParam("@FromDate", dtpInquiryFromDate.Value)
            AddRptParam("@ToDate", dtpInquiryToDate.Value)
            AddRptParam("@IsOneChecked", IsOneChecked)
            AddRptParam("@IsTwoChecked", IsTwoChecked)
            AddRptParam("@IsBothChecked", IsBothChecked)
            ShowReport("rptRepeatedCustomerCount")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class