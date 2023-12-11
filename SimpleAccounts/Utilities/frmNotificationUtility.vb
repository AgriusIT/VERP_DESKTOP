Imports SBModel

Public Class frmNotificationUtility
    Public Function GetConnectionString(Optional ByVal strOpt As String = "") As DataTable
        Dim dt As New DataTable("CompanyConnectionInfo")
        Try
            dt.Columns.Add("ConnectionString")
            dt.ReadXml(Application.StartupPath & "\CompanyConnectionInfo.xml")
            'dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub frmNotificationUtility_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GetConnectionString()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Timer1.Enabled = False
            Application.DoEvents()
            If System.Net.Dns.GetHostName.ToString.ToUpper <> getConfigValueByType("DNSHostForSMS").ToString.ToUpper Then Exit Sub
            If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then

                Exit Sub
            End If
            Application.DoEvents()
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.WorkerReportsProgress = True
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Timer1.Enabled = True
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim objSMSLogList As New List(Of SMSLogBE)
        Try
            If My.Computer.Network.IsAvailable = False Then Exit Sub 
            objSMSLogList = GetPendingSMS()
            If objSMSLogList Is Nothing Then Exit Sub
            If objSMSLogList.Count > 0 Then
                For Each objSMSLog As SMSLogBE In objSMSLogList
                    BackgroundWorker1.ReportProgress(1)

                    SendBrandedSMS(objSMSLog)
                    BackgroundWorker1.ReportProgress(2)

                Next
            Else
                Me.BackgroundWorker1.ReportProgress(3)

            End If

        Catch ex As Exception
            BackgroundWorker1.ReportProgress(4)

        Finally
            objSMSLogList.Clear()
        End Try
    End Sub
End Class