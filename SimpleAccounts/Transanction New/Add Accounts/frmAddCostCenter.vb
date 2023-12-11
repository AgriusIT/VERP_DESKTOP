Public Class frmAddCostCenter
    Public _CostCenterId As Integer

    Private Sub frmAddCostCenter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            btnAdd_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Escape Then
            btnCancel_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmAddCostCenter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillDropDown(Me.cmbHead, "SELECT DISTINCT CostCenterGroup, CostCenterGroup From tblDefCostCenter", False)
            btnCancel_Click(btnCancel, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.txtCostCenter.Text = String.Empty
            Me.cmbHead.Text = String.Empty
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Me.txtCostCenter.Text = String.Empty Then Exit Sub
            Dim CostCenter As SBModel.CostCenter
            CostCenter = New SBModel.CostCenter
            CostCenter.CostCenterId = 0
            CostCenter.CostCenter = Me.txtCostCenter.Text.ToString
            CostCenter.CostCenterHead = Me.cmbHead.Text.ToString
            CostCenter.Active = IIf(Me.chkActive.Checked = True, 1, 0)
            CostCenter.OutWardGatePass = IIf(Me.CheckBox1.Checked = True, 1, 0)
            _CostCenterId = New SBDal.CostCenterDal().AddCostCenter(CostCenter)
            btnCancel_Click(btnCancel, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Close()
        End Try
    End Sub
End Class