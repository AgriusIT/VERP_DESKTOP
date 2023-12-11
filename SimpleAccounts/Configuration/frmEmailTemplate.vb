Public Class frmEmailTemplate
    Private Sub InsertFields(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Me.txtTemplateBody.Focused = True Then

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmEmailTemplate_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class