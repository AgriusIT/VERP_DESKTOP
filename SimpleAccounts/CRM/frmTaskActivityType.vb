Imports SBModel
Imports SBDal
Public Class frmTaskActivityType
    Dim obj As ActivityType
    Private Sub FillModel()
        Try
            obj = New ActivityType
            obj.Description = Me.txtType.Text.ToString
            obj.Remarks = Me.txtRemarks.Text.ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Save()
        Try
            Dim dal As New TaskDAL
            dal.AddActivityType(obj)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress1.Text = ".....Please wait while saving"
            Me.lblProgress1.BackColor = Color.Yellow
            Me.lblProgress1.Visible = True
            Application.DoEvents()
            FillModel()
            Save()
            'msg_Information("New type has been created successfully")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress1.Visible = False
        End Try
    End Sub

    Private Sub frmTaskActivityType_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress1.Text = ".....Please wait....."
            Me.lblProgress1.BackColor = Color.Yellow
            Me.lblProgress1.Visible = True
            Application.DoEvents()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress1.Visible = False
        End Try
    End Sub
End Class