Imports SBDal
Imports SBModel

Public Class frmAgentList

    Private Sub Button7_Click(sender As Object, e As EventArgs)
        Try
            Dim Agent As New frmProAgent(False)
            Agent.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdAgentList_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdAgentList.ColumnButtonClick
        Try
            If e.Column.Key = "Edit" Then
                Dim Obj As New BEAgent
                Obj.AgentId = Me.grdAgentList.GetRow.Cells("AgentId").Value
                Obj.Name = Me.grdAgentList.GetRow.Cells("Name").Value.ToString
                Obj.FathersName = Me.grdAgentList.GetRow.Cells("FathersName").Value.ToString
                Obj.PrimaryMobile = Me.grdAgentList.GetRow.Cells("PrimaryMobile").Value.ToString
                Obj.SecondaryMobile = Me.grdAgentList.GetRow.Cells("SecondaryMobile").Value.ToString
                Obj.CityId = Me.grdAgentList.GetRow.Cells("CityId").Value
                'Me.cmbAccount.SelectedValue = dt.Rows(0).Item("coa_detail_id")
                Obj.SpecialityId = Me.grdAgentList.GetRow.Cells("SpecialityId").Value
                Obj.AddressLine1 = Me.grdAgentList.GetRow.Cells("AddressLine1").Value.ToString
                Obj.AddressLine2 = Me.grdAgentList.GetRow.Cells("AddressLine2").Value.ToString
                Obj.Email = Me.grdAgentList.GetRow.Cells("Email").Value.ToString
                Obj.CNIC = Me.grdAgentList.GetRow.Cells("CNIC").Value.ToString
                Obj.coa_detail_id = Me.grdAgentList.GetRow.Cells("coa_detail_id").Value
                Obj.AccountTitle = Me.grdAgentList.GetRow.Cells("Account").Value.ToString
                Obj.Active = Me.grdAgentList.GetRow.Cells("AgentId").Value
                Dim Agent As New frmProAgent(Obj, False)
                Agent.ShowDialog()
                Me.grdAgentList.DataSource = AgentDAL.GetAll()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmAgentList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmAgentList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.grdAgentList.DataSource = AgentDAL.GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim Agent As New frmProAgent(False)
            Agent.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class