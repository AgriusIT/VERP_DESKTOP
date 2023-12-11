Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Public Class AgeingReceivableSingleDate
    Public flgCompanyRights As Boolean = False
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.CallShowReport()
        Catch Ex As Exception
            ShowErrorMessage(Ex.Message)
        End Try
    End Sub
    Sub CallShowReport(Optional ByVal Print As Boolean = False)
        Try
            GetCrystalReportRights()
            AddRptParam("@ToDate", Me.DateTimePicker2.Value)
            AddRptParam("@Aging", 30)
            'AddRptParam("@AgingName", "CurrentAmount")
            AddRptParam("@1stAging", 60)
            AddRptParam("@1stAgingName", "30_60")
            AddRptParam("@2ndAging", 90)
            AddRptParam("@2ndAgingName", "60_90")
            AddRptParam("@3rdAging", 90)
            AddRptParam("@3rdAgingName", "90+")
            AddRptParam("@IncludeUnPosted", "1")
            AddRptParam("@SubSubID", "0")
            AddRptParam("@CostCenterId", 0)
            ShowReport("AgeingReceivableSingleDate")
            'Me.Cursor = Cursors.WaitCursor
            'Dim UpToDate As String = Me.DateTimePicker2.Value.Year & "," & Me.DateTimePicker2.Value.Month & "," & Me.DateTimePicker2.Value.Day & ",23,59,59"
            'Dim opening As Integer = 0 'GetAccountOpeningBalance(0, Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
            'ShowReport("AgeingReceivableSingleDate", Me.DateTimePicker2.Value.Date.ToString("dd/MMM/yyyy"))
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        CallShowReport(True)
    End Sub
 
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnPrint.Enabled = False
                    Me.Visible = False
                    Exit Sub
                End If
            Else
                Me.btnPrint.Enabled = False
                Me.Visible = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    End If
                    If RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub rptExpenses_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            Me.DateTimePicker2.Value = Date.Today
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class