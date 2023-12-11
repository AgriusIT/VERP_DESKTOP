Imports SBDal
Imports SBModel
Public Class frmProAgentList
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    Private Sub btnAddDock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDock.Click
        Try
            Dim Agent As New frmProAgent(DoHaveSaveRights)
            Agent.ShowDialog()
            Me.grdAgentList.DataSource = AgentDAL.GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProDealerList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            btnAddDock.FlatAppearance.BorderSize = 0
            btnAddDock.FlatAppearance.MouseOverBackColor = btnAddDock.BackColor
            Me.grdAgentList.DataSource = AgentDAL.GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdAgentList_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdAgentList.ColumnButtonClick
        Try
            Try
                If e.Column.Key = "Delete" Then
                    If Me.grdAgentList.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                        If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                        If DoHaveDeleteRights = True Then
                            AgentDAL.Delete(Val(Me.grdAgentList.GetRow.Cells("AgentId").Value.ToString))
                            Me.grdAgentList.GetRow.Delete()
                        Else
                            msg_Information("You do not have delete rights.")
                        End If
                    End If
                End If
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
            'If e.Column.Key = "Edit" Then
            '    Dim Obj As New BEAgent
            '    Obj.AgentId = Me.grdAgentList.GetRow.Cells("AgentId").Value
            '    Obj.Name = Me.grdAgentList.GetRow.Cells("Name").Value.ToString
            '    Obj.FathersName = Me.grdAgentList.GetRow.Cells("FathersName").Value.ToString
            '    Obj.PrimaryMobile = Me.grdAgentList.GetRow.Cells("PrimaryMobile").Value.ToString
            '    Obj.SecondaryMobile = Me.grdAgentList.GetRow.Cells("SecondaryMobile").Value.ToString
            '    Obj.CityId = Me.grdAgentList.GetRow.Cells("CityId").Value
            '    'Me.cmbAccount.SelectedValue = dt.Rows(0).Item("coa_detail_id")
            '    Obj.SpecialityId = Me.grdAgentList.GetRow.Cells("SpecialityId").Value
            '    Obj.AddressLine1 = Me.grdAgentList.GetRow.Cells("AddressLine1").Value.ToString
            '    Obj.AddressLine2 = Me.grdAgentList.GetRow.Cells("AddressLine2").Value.ToString
            '    Obj.Email = Me.grdAgentList.GetRow.Cells("Email").Value.ToString
            '    Obj.CNIC = Me.grdAgentList.GetRow.Cells("CNIC").Value.ToString
            '    Obj.coa_detail_id = Me.grdAgentList.GetRow.Cells("coa_detail_id").Value
            '    Obj.AccountTitle = Me.grdAgentList.GetRow.Cells("Account").Value.ToString
            '    Obj.Active = Me.grdAgentList.GetRow.Cells("AgentId").Value
            '    Dim Agent As New frmProAgent(Obj, DoHaveUpdateRights)
            '    Agent.ShowDialog()
            '    Me.grdAgentList.DataSource = AgentDAL.GetAll()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdAgentList_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdAgentList.RowDoubleClick
        Try
            If Me.grdAgentList.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
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
                Obj.Remarks = Me.grdAgentList.GetRow.Cells("Remarks").Value.ToString
                'Saba Shabbir TFS2566 set BloodGroup value
                Obj.BloodGroup = Me.grdAgentList.GetRow.Cells("BloodGroup").Value.ToString
                Dim Agent As New frmProAgent(Obj, DoHaveUpdateRights)
                Agent.ShowDialog()
                Me.grdAgentList.DataSource = AgentDAL.GetAll()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                DoHaveSaveRights = True
                DoHaveUpdateRights = True
                DoHaveDeleteRights = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    DoHaveSaveRights = False
                    DoHaveUpdateRights = False
                    DoHaveDeleteRights = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                DoHaveSaveRights = False
                DoHaveUpdateRights = False
                DoHaveDeleteRights = False
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        DoHaveSaveRights = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        DoHaveUpdateRights = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        DoHaveDeleteRights = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdAgentList.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdAgentList.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdAgentList.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class