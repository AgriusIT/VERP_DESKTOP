Imports SBModel
Imports SBDal
Imports SBUtility
Imports System.Data.SqlClient
Public Class frmReminderMessages
    Dim AD As New Microsoft.VisualBasic.Devices.Audio
    Dim Index As Integer = 0I
    Enum enmSnooze
        Minutes
        Hour
        Hours
        Day
        Days
        Week
        Weeks
    End Enum

    Private Sub frmReminderMessages_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
       
            AD.Stop()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmReminderMessages_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Cancel Then
                If Me.grdMsgDetail.RootTable Is Nothing Then Exit Sub
                Dim ReminderList As New List(Of ReminderDetail)
                Dim ReminderDismiss As New ReminderDetail
                ReminderDismiss.ReminderDetailId = Me.grdMsgDetail.GetRow.Cells("ReminderDetailId").Value
                ReminderDismiss.ReminderId = Me.grdMsgDetail.GetRow.Cells("ReminderId").Value
                ReminderDismiss.UserID = LoginUserId
                ReminderList.Add(ReminderDismiss)
                Call New reminderDAL().Dismiss(ReminderList)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmReminderMessages_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub Play()
        'Dim af() As Byte = IO.File.ReadAllBytes(Application.StartupPath & "\Sounds\Reminder.wav")
        'AD.Play(af, AudioPlayMode.BackgroundLoop)
    End Sub
    Private Sub grdMsgDetail_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdMsgDetail.SelectionChanged
        Try
            If Me.grdMsgDetail.RootTable Is Nothing Then Exit Sub
            If Not grdMsgDetail.GetRow Is Nothing AndAlso grdMsgDetail.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.Label1.Text = "Reminder: " & Me.grdMsgDetail.GetRow.Cells("User_Reminder_Date").Text & ", " & Me.grdMsgDetail.GetRow.Cells("User_Reminder_Time").Text & " " & Chr(10) & "" & Me.grdMsgDetail.GetRow.Cells("Reminder_Description").Text.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings()
        Try

            Me.cmbSnooze.SelectedIndex = 0
            If Me.grdMsgDetail.RootTable Is Nothing Or grdMsgDetail.RowCount < 1 Then Me.Close()
            Me.grdMsgDetail.RootTable.Columns(0).Visible = False
            Me.grdMsgDetail.RootTable.Columns(1).Visible = False
            Me.grdMsgDetail.RootTable.Columns(2).Visible = False
            Me.grdMsgDetail.RootTable.Columns(3).Visible = False
            Me.grdMsgDetail.RootTable.Columns(4).Visible = False
            Me.grdMsgDetail.RootTable.Columns(6).Visible = False
            Me.grdMsgDetail.RootTable.Columns(7).Visible = False
            Me.grdMsgDetail.RootTable.Columns(8).Visible = False
            Me.grdMsgDetail.RootTable.Columns("User_Reminder_Date").FormatString = "dddd,MMMMM,dd,yy"
            Me.grdMsgDetail.RootTable.Columns(5).Width = 400


            Me.Label1.Text = String.Empty
            grdMsgDetail_SelectionChanged(Nothing, Nothing)
            If Me.grdMsgDetail.RowCount > 1 Then
                Me.btnDismissAll.Enabled = True
            Else
                Me.btnDismissAll.Enabled = False
            End If
            Me.btnDismiss.Focus()

            System.Media.SystemSounds.Exclamation.Play()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub frmReminderMessages_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            '  Play()
        Catch ex As Exception
            'Finally
            'AD.Stop()
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDismissAll.Click, btnDismiss.Click
        Try
            Dim btn As Button = CType(sender, Button)
            Dim ReminderList As New List(Of ReminderDetail)
            Select Case btn.Name
                Case Me.btnDismissAll.Name
                    If Me.grdMsgDetail.RootTable Is Nothing Then Exit Sub
                    Dim ReminderAllDismiss As ReminderDetail
                    For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdMsgDetail.GetRows
                        ReminderAllDismiss = New ReminderDetail
                        ReminderAllDismiss.ReminderDetailId = r.Cells("ReminderDetailId").Value
                        ReminderAllDismiss.ReminderId = r.Cells("ReminderId").Value
                        ReminderAllDismiss.UserID = LoginUserId
                        ReminderList.Add(ReminderAllDismiss)
                    Next
                    Call New reminderDAL().DismissAll(ReminderList)
                    Me.Close()
                Case btnDismiss.Name
                    If Me.grdMsgDetail.RootTable Is Nothing Then Exit Sub
                    Dim ReminderDismiss As New ReminderDetail
                    ReminderDismiss.ReminderDetailId = grdMsgDetail.GetRow.Cells("ReminderDetailId").Value
                    ReminderDismiss.ReminderId = Me.grdMsgDetail.GetRow.Cells("ReminderId").Value
                    ReminderDismiss.UserID = LoginUserId
                    ReminderList.Add(ReminderDismiss)
                    Call New reminderDAL().Dismiss(ReminderList)
                    Me.Close()
            End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub btnSnooze_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSnooze.Click
        Try
            If Me.grdMsgDetail.RootTable Is Nothing Then Exit Sub
            Dim Reminderdt As ReminderDetail
            Dim ReminderList As New List(Of ReminderDetail)
            Dim Snooze As String = String.Empty
            Snooze = Me.cmbSnooze.Text
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdMsgDetail.GetRows
                Reminderdt = New ReminderDetail
                If Snooze.Substring(IIf(Snooze.IndexOf("Day") = -1, 0, Snooze.IndexOf("Day") - 1)).Trim = enmSnooze.Day.ToString Then
                    Reminderdt.User_Reminder_Date = Date.Now.AddDays(Convert.ToInt32(Me.cmbSnooze.Text.Replace("Day", ""))).Date
                    Reminderdt.User_Reminder_Time = Date.Now.ToLongTimeString
                ElseIf Snooze.Substring(IIf(Snooze.IndexOf("Days") = -1, 0, Snooze.IndexOf("Days") - 1)).Trim = enmSnooze.Days.ToString Then
                    Reminderdt.User_Reminder_Date = Date.Now.AddDays(Convert.ToInt32(Me.cmbSnooze.Text.Replace("Days", ""))).Date
                    Reminderdt.User_Reminder_Time = Date.Now.ToLongTimeString
                ElseIf Snooze.Substring(IIf(Snooze.IndexOf("Week") = -1, 0, Snooze.IndexOf("Week") - 1)).Trim = enmSnooze.Week.ToString Then
                    Reminderdt.User_Reminder_Date = Date.Now.AddDays(7).Date
                    Reminderdt.User_Reminder_Time = Date.Now.ToLongTimeString
                ElseIf Snooze.Substring(IIf(Snooze.IndexOf("Weeks") = -1, 0, Snooze.IndexOf("Weeks") - 1)).Trim = enmSnooze.Weeks.ToString Then
                    Dim myWeeks As Integer = Me.cmbSnooze.Text.Replace("Weeks", "")
                    myWeeks = myWeeks * 7
                    Reminderdt.User_Reminder_Date = Date.Now.AddDays(myWeeks).Date
                    Reminderdt.User_Reminder_Time = Date.Now.ToLongTimeString
                End If

                If Snooze.Substring(IIf(Snooze.IndexOf("Minutes") = -1, 0, Snooze.IndexOf("Minutes") - 1)).Trim = enmSnooze.Minutes.ToString Then
                    'If Me.cmbSnooze.SelectedIndex = 0 Or Me.cmbSnooze.SelectedIndex = 1 Or Me.cmbSnooze.SelectedIndex = 2 Or Me.cmbSnooze.SelectedIndex = 3 Then
                    Reminderdt.User_Reminder_Date = Date.Now.AddMinutes(Convert.ToInt32(Me.cmbSnooze.Text.Replace("Minutes", ""))).Date
                    Reminderdt.User_Reminder_Time = Date.Now.AddMinutes(Convert.ToInt32(Me.cmbSnooze.Text.Replace("Minutes", ""))).ToLongTimeString
                ElseIf Snooze.Substring(IIf(Snooze.IndexOf("Hour") = -1, 0, Snooze.IndexOf("Hour") - 1)).Trim = enmSnooze.Hour.ToString Then
                    Reminderdt.User_Reminder_Date = Date.Now.AddHours(Convert.ToInt32(Me.cmbSnooze.Text.Replace("Hour", ""))).Date
                    Reminderdt.User_Reminder_Time = Date.Now.AddHours(Convert.ToInt32(Me.cmbSnooze.Text.Replace("Hour", ""))).ToLongTimeString
                ElseIf Snooze.Substring(IIf(Snooze.IndexOf("Hours") = -1, 0, Snooze.IndexOf("Hours") - 1)).Trim = enmSnooze.Hours.ToString Then
                    Reminderdt.User_Reminder_Date = Date.Now.AddHours(Convert.ToInt32(Me.cmbSnooze.Text.Replace("Hours", ""))).Date
                    Reminderdt.User_Reminder_Time = Date.Now.AddHours(Convert.ToInt32(Me.cmbSnooze.Text.Replace("Hours", ""))).ToLongTimeString
                End If
                Reminderdt.ReminderId = r.Cells("ReminderId").Value
                Reminderdt.ReminderDetailId = r.Cells("ReminderDetailId").Value
                Reminderdt.UserID = LoginUserId
                ReminderList.Add(Reminderdt)
            Next
            Call New reminderDAL().Snooze(ReminderList)
            DialogResult = Windows.Forms.DialogResult.Yes
            Me.Close()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class