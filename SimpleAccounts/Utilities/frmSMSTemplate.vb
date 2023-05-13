Public Class frmSMSTemplate

    Dim Id As Integer = 0I
    Private Const EM_CHARFROMPOS As Int32 = &HD7
    Private Structure POINTAPI
        Public X As Integer
        Public Y As Integer
    End Structure
    Private Declare Function SendMessageLong Lib "user32" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal wMsg As Int32, ByVal wParam As Int32, ByVal lParam As Int32) As Long
    Public Function TextBoxCursorPos(ByVal txt As TextBox, _
    ByVal X As Single, ByVal Y As Single) As Long
        ' Convert screen coordinates into control coordinates.
        Dim pt As Point = txtSMSTemplate.PointToClient(New Point(X, _
            Y))

        ' Get the character number
        TextBoxCursorPos = SendMessageLong(txt.Handle, _
            EM_CHARFROMPOS, 0&, CLng(pt.X + pt.Y * &H10000)) _
            And &HFFFF&
    End Function
    Private Sub lstParameters_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lstParameters.DragDrop
        Try
            lstParameters.DoDragDrop(Me.lstParameters.SelectedItem, DragDropEffects.Copy)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSMSTemplate_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txtSMSTemplate.DragDrop
        Try
            Me.txtSMSTemplate.SelectedText = e.Data.GetData(DataFormats.Text).ToString & " "

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtSMSTemplate_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txtSMSTemplate.DragOver
        Try
            If e.Data.GetDataPresent(DataFormats.StringFormat) Then
                e.Effect = DragDropEffects.Copy
                Me.txtSMSTemplate.Select(TextBoxCursorPos(txtSMSTemplate, e.X, e.Y), 0)
            Else
                e.Effect = DragDropEffects.None
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub frmSMSTemplate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.txtSMSTemplate.AllowDrop = True
            Me.cmbKey.SelectedIndex = 0

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub lstParameters_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstParameters.MouseDown
        Try
            Me.lstParameters.DoDragDrop(Me.lstParameters.SelectedItem, DragDropEffects.Copy)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.txtSMSTemplate.Text = String.Empty Then
                ShowErrorMessage("Please template description")
                Me.txtSMSTemplate.Focus()
                Exit Sub
            End If

            Dim objTemplate As New SBModel.SMSTemplateParameter
            objTemplate.Key = Me.cmbKey.Text
            objTemplate.SMSTemplate = Me.txtSMSTemplate.Text
            Call New SBDal.SMSTemplatesDAL().SaveSMSTemplate(objTemplate)
            GetAllSMSTemplate()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Me.txtSMSTemplate.Text = String.Empty

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbKey_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbKey.SelectedIndexChanged
        Try

            Me.txtSMSTemplate.Text = New SBDal.SMSTemplatesDAL().GetAllRecordByKey(Me.cmbKey.Text)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class