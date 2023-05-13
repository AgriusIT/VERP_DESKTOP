Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal
Class frmApprovalRejectionDetail

    Dim ApprovalHistoryDetail As ApprovalHistoryDetailBE
    Public Shared RejectionRemarks As String = String.Empty
    Public Shared RejectionId As Integer
    Private Sub frmApprovalRejectionDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillCombos()
    End Sub

  

    Public Sub FillCombos(Optional Condition As String = "")
        Dim Str As String
        Str = "Select ApprovalRejectionReasonId , Title from ApprovalRejectionReason where Active = 1 Order By SortOrder "
        FillUltraDropDown(cmbRejectionReason, Str)
        Me.cmbRejectionReason.Rows(0).Activate()
        Me.cmbRejectionReason.DisplayLayout.Bands(0).Columns("ApprovalRejectionReasonId").Hidden = True
        Me.cmbRejectionReason.DisplayLayout.Bands(0).Columns("Title").Width = 500
    End Sub
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If Me.txtRemarks.Text = String.Empty Then
            ShowErrorMessage("Please Enter Some Remarks")
            Exit Sub
        Else
            RejectionRemarks = Me.txtRemarks.Text
            RejectionId = Me.cmbRejectionReason.ActiveRow.Cells(0).Value
            Me.DialogResult = Windows.Forms.DialogResult.OK
            ResetControls()
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        ResetControls()
        Me.Close()
    End Sub
    Private Sub ResetControls()
        Me.cmbRejectionReason.Rows(0).Activate()
        Me.txtRemarks.Text = String.Empty
    End Sub
End Class