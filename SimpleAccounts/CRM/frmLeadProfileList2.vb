''TASK TFS4795 Login User Wise history and implementation of Security Rights Wise 'Get All Record' is required. Done by Amin on 15-10-2018 
Imports SBDal
Imports SBModel
Public Class frmLeadProfileList2
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    Public DoHavePrintRights As Boolean = False
    Public DoHaveExportRights As Boolean = False
    Public DoHaveFieldChooserRights As Boolean = False
    Public DoHaveActivityHistoryRights As Boolean = False
    Public DoHaveConvertToAccountRights As Boolean = False
    Public DoHaveGetAllRights As Boolean = False
    Dim leadProfileDAL As New LeadProfileDAL2()
    Public _tEST As Integer

    'Sub New(TEST As Integer)
    '    _tEST = TEST
    '    ''Me.grdLeadProfileList.DataSource = leadProfileDAL.GetById(TEST)
    'End Sub
    Private Sub btnAddDock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDock.Click
        Try
            Dim Lead As New frmLeadProfile2(DoHaveSaveRights, DoHavePrintRights, DoHaveExportRights, DoHaveFieldChooserRights)
            Lead.ShowDialog()
            GetAll(LoginUserName)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub frmLeadProfileList2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            btnAddDock.FlatAppearance.BorderSize = 0
            btnAddDock.FlatAppearance.MouseOverBackColor = btnAddDock.BackColor
            GetAll(LoginUserName)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmLeadProfileList2_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdLeadProfileList.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If Me.grdLeadProfileList.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                    If DoHaveDeleteRights = True Then
                        Dim Obj As New LeadProfileBE2()
                        Obj.ActivityLog = New ActivityLog()
                        Obj.LeadProfileId = Val(Me.grdLeadProfileList.GetRow.Cells("LeadProfileId").Value.ToString)
                        Obj.IsAccountCreated = CBool(Me.grdLeadProfileList.GetRow.Cells("IsAccountCreated").Value)
                        Obj.DocNo = Me.grdLeadProfileList.GetRow.Cells("DocNo").Value.ToString
                        Obj.CompanyName = Me.grdLeadProfileList.GetRow.Cells("CompanyName").Value.ToString
                        Obj.AccountId = Val(Me.grdLeadProfileList.GetRow.Cells("AccountId").Value.ToString)
                        leadProfileDAL.Delete(Obj)
                        Me.grdLeadProfileList.GetRow.Delete()
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
    End Sub

    Private Sub grdLeadProfileList_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdLeadProfileList.RowDoubleClick
        Try
            If Me.grdLeadProfileList.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                '           [LeadProfileId] [int] IDENTITY(1,1) NOT NULL,
                '[DocNo] [nvarchar](50) NULL,
                '[DocDate] [datetime] NULL,
                '[TypeId] [int] NULL,
                '[CompanyName] [nvarchar](250) NULL,
                '[coa_detail_id] [int] NULL,
                '[ActivityId] [int] NULL,
                '[SourceId] [int] NULL,
                '[IndustryId] [int] NULL,
                '[StatusId] [int] NULL,
                '[InterestedInId] [nvarchar](2500) NULL,
                '[BrandFocusId] [nvarchar](2500) NULL,
                '[Remarks] [nvarchar](2500) NULL,
                '[UserName] [nvarchar](50) NULL,
                '[ModifiedUser] [nvarchar](50) NULL,
                '[ModifiedDate] [datetime] NULL,
                Dim Obj As New LeadProfileBE2
                Obj.LeadProfileId = Me.grdLeadProfileList.GetRow.Cells("LeadProfileId").Value
                Obj.DocNo = Me.grdLeadProfileList.GetRow.Cells("DocNo").Value.ToString
                Obj.DocDate = Me.grdLeadProfileList.GetRow.Cells("DocDate").Value
                Obj.TypeId = Val(Me.grdLeadProfileList.GetRow.Cells("TypeId").Value.ToString)
                Obj.CompanyName = Me.grdLeadProfileList.GetRow.Cells("CompanyName").Value.ToString
                'Rafay
                Obj.CountryName = Me.grdLeadProfileList.GetRow.Cells("CountryId").Value.ToString
                'Rafay
                Obj.Website = Me.grdLeadProfileList.GetRow.Cells("WebSite").Value.ToString
                Obj.AccountId = Val(Me.grdLeadProfileList.GetRow.Cells("AccountId").Value.ToString)
                Obj.ActivityId = Val(Me.grdLeadProfileList.GetRow.Cells("ActivityId").Value.ToString)
                'Me.cmbAccount.SelectedValue = dt.Rows(0).Item("coa_detail_id")
                Obj.SourceId = Val(Me.grdLeadProfileList.GetRow.Cells("SourceId").Value.ToString)
                Obj.IndustryId = Val(Me.grdLeadProfileList.GetRow.Cells("IndustryId").Value.ToString)
                Obj.Status = Me.grdLeadProfileList.GetRow.Cells("Status").Value.ToString
                Obj.InterestedInId = Me.grdLeadProfileList.GetRow.Cells("InterestedInId").Value.ToString
                Obj.BrandFocusId = Me.grdLeadProfileList.GetRow.Cells("BrandFocusId").Value.ToString
                Obj.NoofEmployeeId = Val(Me.grdLeadProfileList.GetRow.Cells("NoofEmployeeId").Value.ToString)
                Obj.Remarks = Me.grdLeadProfileList.GetRow.Cells("Remarks").Value.ToString
                Obj.UserName = Me.grdLeadProfileList.GetRow.Cells("UserName").Value.ToString
                Obj.ModifiedUser = Me.grdLeadProfileList.GetRow.Cells("ModifiedUser").Value.ToString
                Obj.ModifiedDate = Me.grdLeadProfileList.GetRow.Cells("ModifiedDate").Value
                Obj.IsAccountCreated = CBool(Me.grdLeadProfileList.GetRow.Cells("IsAccountCreated").Value)
                Obj.NoOfAttachments = Val(Me.grdLeadProfileList.GetRow.Cells("No Of Attachment").Value.ToString)
                Obj.EmployeeId = Val(Me.grdLeadProfileList.GetRow.Cells("EmployeeId").Value.ToString)
                Obj.EmployeeName = Me.grdLeadProfileList.GetRow.Cells("EmployeeName").Value.ToString
                Obj.CountryId = Val(Me.grdLeadProfileList.GetRow.Cells("CountryId").Value.ToString)
                Obj.Address = Me.grdLeadProfileList.GetRow.Cells("Address").Value.ToString
                'IsAccountCreated
                Dim LeadProfile As New frmLeadProfile2(DoHaveUpdateRights, DoHaveActivityHistoryRights, DoHaveConvertToAccountRights, DoHavePrintRights, DoHaveExportRights, DoHaveFieldChooserRights, DoHaveDeleteRights, Obj)
                LeadProfile.ShowDialog()
                GetAll(LoginUserName)
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
                DoHavePrintRights = True
                DoHaveExportRights = True
                DoHaveFieldChooserRights = True
                DoHaveActivityHistoryRights = True
                DoHaveConvertToAccountRights = True
                DoHaveGetAllRights = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                btnLoadAll.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    DoHaveSaveRights = False
                    DoHaveUpdateRights = False
                    DoHaveDeleteRights = False
                    DoHavePrintRights = False
                    DoHaveExportRights = False
                    DoHaveFieldChooserRights = False
                    DoHaveActivityHistoryRights = False
                    DoHaveConvertToAccountRights = False
                    DoHaveGetAllRights = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    btnLoadAll.Enabled = False
                    Exit Sub
                End If
            Else
                DoHaveSaveRights = False
                DoHaveUpdateRights = False
                DoHaveDeleteRights = False
                DoHaveGetAllRights = False
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                btnLoadAll.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                        DoHavePrintRights = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                        DoHaveExportRights = True

                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        DoHaveFieldChooserRights = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        DoHaveSaveRights = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        DoHaveUpdateRights = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        DoHaveDeleteRights = True
                    ElseIf RightsDt.FormControlName = "View Activity" Then
                        DoHaveActivityHistoryRights = True
                    ElseIf RightsDt.FormControlName = "Convert To Account" Then
                        DoHaveConvertToAccountRights = True
                    ElseIf RightsDt.FormControlName = "Get All" Then
                        btnLoadAll.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdLeadProfileList.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdLeadProfileList.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdLeadProfileList.LoadLayoutFile(fs)
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
    Public Sub GetAll(Optional ByVal UserName As String = "")
        Try
            Me.grdLeadProfileList.DataSource = leadProfileDAL.GetAll(UserName)
            Me.grdLeadProfileList.RootTable.Columns("ModifiedDate").FormatString = str_DisplayDateFormat
            Me.grdLeadProfileList.RootTable.Columns("DocDate").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            Throw
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS4795
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLoadAll_Click(sender As Object, e As EventArgs) Handles btnLoadAll.Click
        Try
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdLeadProfileList_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdLeadProfileList.LinkClicked
        Try
            If e.Column.Key = "No Of Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = "frmLeadProfile2"
                frm._VoucherId = Val(Me.grdLeadProfileList.GetRow.Cells("LeadProfileId").Value.ToString)
                frm.ShowDialog()
                Exit Sub
            ElseIf e.Column.Key = "Logo" Then
                Dim frm As New frmAttachmentView
                frm._Source = "frmLeadProfileList2"
                frm._VoucherId = Val(Me.grdLeadProfileList.GetRow.Cells("LeadProfileId").Value.ToString)
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pnlHeader_Paint(sender As Object, e As PaintEventArgs) Handles pnlHeader.Paint

    End Sub

    Private Sub grdLeadProfileList_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdLeadProfileList.FormattingRow

    End Sub
    Public Sub Getbyid(Optional ByVal UserName As String = "")
        Try
            'Me.grdLeadProfileList.DataSource = leadProfileDAL.GetAll(UserName)
            'Me.grdLeadProfileList.RootTable.Columns("ModifiedDate").FormatString = str_DisplayDateFormat
            'Me.grdLeadProfileList.RootTable.Columns("DocDate").FormatString = str_DisplayDateFormat
            Me.grdLeadProfileList.DataSource = leadProfileDAL.GetById(_tEST)
            grdLeadProfileList.MoveFirst()
            grdLeadProfileList_RowDoubleClick(Nothing, Nothing)
            _tEST = 0
                
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class