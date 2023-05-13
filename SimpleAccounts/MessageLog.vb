Public Class frmMessageLog
    Implements IGeneral

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub frmMessageLog_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub frmMessageLog_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ShowMessages()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()

    End Sub

    Private Function ShowMessages() As Boolean
        txtMessages.Text = ""  'Clear the message log

        Dim MsgCount As Integer = ModGlobel.AgriusMessageLogger.MessageCount()

        lblRecordedMessages.Text = "Recorded Messages: " & MsgCount

        If MsgCount < 1 Then
            txtMessages.Text = "(( There are no messages to show ))"
            Return False  'Since there are no messages to show
        Else
            txtMessages.Text = ModGlobel.AgriusMessageLogger.GetAllMessages()
        End If

        Return True  'Since messages are shown in the textbox
    End Function

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Me.ShowMessages()
    End Sub

    Private Sub btnCopyToClipboard_Click(sender As Object, e As EventArgs) Handles btnCopyToClipboard.Click

        If ModGlobel.AgriusMessageLogger.MessageCount() > 0 Then
            Clipboard.SetText(txtMessages.Text)
        Else
            Clipboard.Clear()
        End If
    End Sub

    Private Sub btnClearLog_Click(sender As Object, e As EventArgs) Handles btnClearLog.Click
        ModGlobel.AgriusMessageLogger.Clear()
        txtMessages.Text = ""
        lblRecordedMessages.Text = "Recorded Messages: 0"
        MsgBox("Message Log has been cleared.", vbInformation)
    End Sub
End Class