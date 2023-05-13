''This screen is built on 04-10-2018 by Muhammad Amin against TASK TFS4591 and TFS4593
Imports SBDal
Imports SBModel
Public Class frmOpportunityList
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    Public DoHavePrintRights As Boolean = False
    Public DoHaveExportRights As Boolean = False
    Public DoHaveFieldChooserRights As Boolean = False
    Public DoHaveSaveAndExportRights As Boolean = False
    Dim DAL As New OpportunityDAL()
    Dim rightsModel As New RightsModel
    Private Sub btnAddDock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDock.Click
        Try
            Dim Opportunity As New frmopportunity(rightsModel)
            Opportunity.ShowDialog()
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmLeadProfileList2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            btnAddDock.FlatAppearance.BorderSize = 0
            btnAddDock.FlatAppearance.MouseOverBackColor = btnAddDock.BackColor
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmLeadProfileList2_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdOpportunityList.ColumnButtonClick
        Try
            Try
                If e.Column.Key = "Delete" Then
                    If Me.grdOpportunityList.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                        If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                        If DoHaveDeleteRights = True Then
                            Dim Obj As New OpportunityBE()
                            'Obj.ActivityLog = New ActivityLog()
                            Obj.OpportunityId = Val(Me.grdOpportunityList.GetRow.Cells("OpportunityId").Value.ToString)
                            Obj.DocNo = Me.grdOpportunityList.GetRow.Cells("DocNo").Value.ToString
                            Obj.OpportunityType = Me.grdOpportunityList.GetRow.Cells("OpportunityType").Value.ToString

                            'Obj.DocNo = Me.grdOpportunityList.GetRow.Cells("DocNo").Value.ToString
                            DAL.Delete(Obj)
                            Me.grdOpportunityList.GetRow.Delete()
                        Else
                            msg_Information("You do not have delete rights.")
                        End If
                    End If
                End If
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdOpportunityList_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdOpportunityList.RowDoubleClick
        Try
            If Me.grdOpportunityList.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                '[] [int] IDENTITY(1,1) NOT NULL,
                '[] [nvarchar](50) NULL,
                '[] [datetime] NULL,
                '[] [nvarchar](250) NULL,
                '[] [nvarchar](250) NULL,
                '[] [nvarchar](250) NULL,
                '[] [nvarchar](500) NULL,
                '[] [int] NULL,
                '[] [int] NULL,
                '[] [nvarchar](250) NULL,
                '[] [datetime] NULL,
                '[] [int] NULL,
                '[] [int] NULL,
                '[] [int] NULL,
                '[] [nvarchar](50) NULL,
                '[] [float] NULL,
                '[] [nvarchar](50) NULL,
                '[] [datetime] NULL,
                '[] [int] NULL,
                '[] [int] NULL,
                '[] [int] NULL,
                '[] [float] NULL,
                '[] [nvarchar](50) NULL,
                '[] [int] NULL,
                '[] [nvarchar](500) NULL,
                '[] [float] NULL,
                '[] [int] NULL,
                '[] [nvarchar](50) NULL,
                '[] [nvarchar](50) NULL,
                '[] [datetime] NULL,
                '[] [nvarchar](50) NULL,
                Dim Obj As New OpportunityBE
                Obj.OpportunityId = Val(Me.grdOpportunityList.GetRow.Cells("OpportunityId").Value.ToString)
                Obj.DocNo = Me.grdOpportunityList.GetRow.Cells("DocNo").Value.ToString
                Obj.DocDate = IIf(Me.grdOpportunityList.GetRow.Cells("DocDate").Value Is DBNull.Value, Now, Me.grdOpportunityList.GetRow.Cells("DocDate").Value)
                Obj.CompanyId = Val(Me.grdOpportunityList.GetRow.Cells("CompanyId").Value.ToString)
                Obj.ContactId = Val(Me.grdOpportunityList.GetRow.Cells("ContactId").Value.ToString)
                Obj.EndUserId = Me.grdOpportunityList.GetRow.Cells("EndUserId").Value.ToString
                Obj.OpportunityName = Me.grdOpportunityList.GetRow.Cells("OpportunityName").Value.ToString
                Obj.TypeId = Val(Me.grdOpportunityList.GetRow.Cells("TypeId").Value.ToString)
                Obj.CurrencyId = Val(Me.grdOpportunityList.GetRow.Cells("CurrencyId").Value.ToString)
                Obj.OpportunityOwner = Me.grdOpportunityList.GetRow.Cells("OpportunityOwner").Value.ToString
                Obj.CloseDate = IIf(Me.grdOpportunityList.GetRow.Cells("CloseDate").Value Is DBNull.Value, Now, Me.grdOpportunityList.GetRow.Cells("CloseDate").Value)
                Obj.StageId = Val(Me.grdOpportunityList.GetRow.Cells("StageId").Value.ToString)
                Obj.LoosReasonId = Val(Me.grdOpportunityList.GetRow.Cells("LoosReasonId").Value.ToString)
                Obj.ProbabilityId = Val(Me.grdOpportunityList.GetRow.Cells("ProbabilityId").Value.ToString)
                Obj.ContactName = Me.grdOpportunityList.GetRow.Cells("ContactName").Value.ToString
                Obj.HardwareContact = Me.grdOpportunityList.GetRow.Cells("HardwareContact").Value.ToString
                Obj.TaxAmount = Val(Me.grdOpportunityList.GetRow.Cells("TaxAmount").Value.ToString)
                Obj.Duration = Me.grdOpportunityList.GetRow.Cells("Duration").Value.ToString
                Obj.StartDate = IIf(Me.grdOpportunityList.GetRow.Cells("StartDate").Value Is DBNull.Value, Now, Me.grdOpportunityList.GetRow.Cells("StartDate").Value)
                Obj.PaymentId = Val(Me.grdOpportunityList.GetRow.Cells("PaymentId").Value.ToString)
                Obj.DeliveryId = Val(Me.grdOpportunityList.GetRow.Cells("DeliveryId").Value.ToString)
                Obj.FrequencyId = Val(Me.grdOpportunityList.GetRow.Cells("FrequencyId").Value.ToString)
                Obj.Freight = Val(Me.grdOpportunityList.GetRow.Cells("Freight").Value.ToString)
                Obj.ImplementationTime = Me.grdOpportunityList.GetRow.Cells("ImplementationTime").Value.ToString
                Obj.MaterialLocation = Me.grdOpportunityList.GetRow.Cells("MaterialLocation").Value.ToString
                Obj.TargetPrice = Val(Me.grdOpportunityList.GetRow.Cells("TargetPrice").Value.ToString)
                Obj.MaintenanceId = Val(Me.grdOpportunityList.GetRow.Cells("MaintenanceId").Value.ToString)
                Obj.UserName = Me.grdOpportunityList.GetRow.Cells("UserName").Value.ToString
                Obj.ModifiedUser = Me.grdOpportunityList.GetRow.Cells("ModifiedUser").Value.ToString
                Obj.ModifiedDate = IIf(Me.grdOpportunityList.GetRow.Cells("ModifiedDate").Value Is DBNull.Value, Now, Me.grdOpportunityList.GetRow.Cells("ModifiedDate").Value)
                Obj.OpportunityType = Me.grdOpportunityList.GetRow.Cells("OpportunityType").Value.ToString
                Obj.SupportTypeId = Val(Me.grdOpportunityList.GetRow.Cells("SupportTypeId").Value.ToString)
                Obj.NoOfAttachments = Val(Me.grdOpportunityList.GetRow.Cells("No Of Attachment").Value.ToString)
                Obj.LeadTimeInDays = Me.grdOpportunityList.GetRow.Cells("LeadTimeInDays").Value.ToString
                Obj.EmployeeId = Val(Me.grdOpportunityList.GetRow.Cells("EmployeeId").Value.ToString)
                Obj.EmployeeName = Me.grdOpportunityList.GetRow.Cells("EmployeeName").Value.ToString
                Obj.OnsiteId = Me.grdOpportunityList.GetRow.Cells("OnsiteId").Value.ToString
                Obj.CoverageWindow = Me.grdOpportunityList.GetRow.Cells("CoverageWindow").Value.ToString
                Obj.OnsiteIntervention = Me.grdOpportunityList.GetRow.Cells("OnsiteIntervention").Value.ToString
                Obj.TotalAmount = Val(Me.grdOpportunityList.GetRow.Cells("TotalAmount").Value.ToString)
                Obj.CountryId = Me.grdOpportunityList.GetRow.Cells("CountryId").Value.ToString
                Obj.DurationofMonth = Me.grdOpportunityList.GetRow.Cells("DurationofMonth").Value.ToString
                Obj.InvoicePattern = Me.grdOpportunityList.GetRow.Cells("InvoicePattern").Value.ToString
                Obj.ArticleId = Val(Me.grdOpportunityList.GetRow.Cells("ArticleId").Value.ToString)
                ''
                Obj.ChkBoxBatteriesIncluded = Me.grdOpportunityList.GetRow.Cells("ChkBoxBatteriesIncluded").Value.ToString
                Dim Opportunity As New frmopportunity(Obj, rightsModel)
                Opportunity.ShowDialog()
                GetAll()
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
                DoHaveFieldChooserRights = True
                DoHaveExportRights = True
                DoHaveSaveAndExportRights = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                rightsModel.Save = True
                rightsModel.Update = True
                rightsModel.Delete = True
                rightsModel.Print = True
                rightsModel.Export = True
                rightsModel.FieldChooser = True
                rightsModel.Other = True
                rightsModel.SaveAndExport = True
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
                    DoHaveSaveAndExportRights = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False

                    rightsModel.Save = False
                    rightsModel.Update = False
                    rightsModel.Delete = False
                    rightsModel.Print = False
                    rightsModel.Export = False
                    rightsModel.FieldChooser = False
                    rightsModel.Other = False
                    rightsModel.SaveAndExport = False
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
                        DoHavePrintRights = True
                        rightsModel.Print = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                        DoHaveExportRights = True
                        rightsModel.Export = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        rightsModel.FieldChooser = True
                        DoHaveFieldChooserRights = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        DoHaveSaveRights = True
                        rightsModel.Save = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        DoHaveUpdateRights = True
                        rightsModel.Update = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        DoHaveDeleteRights = True
                        rightsModel.Delete = True
                    ElseIf RightsDt.FormControlName = "SaveAndExport" Then
                        DoHaveSaveAndExportRights = True
                        rightsModel.SaveAndExport = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Click(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Click

    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdOpportunityList.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdOpportunityList.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdOpportunityList.LoadLayoutFile(fs)
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
    Public Sub GetAll()
        Try
            Me.grdOpportunityList.DataSource = DAL.GetAll()
            Me.grdOpportunityList.RootTable.Columns("ModifiedDate").FormatString = str_DisplayDateFormat
            Me.grdOpportunityList.RootTable.Columns("DocDate").FormatString = str_DisplayDateFormat
            Me.grdOpportunityList.RootTable.Columns("StartDate").FormatString = str_DisplayDateFormat
            Me.grdOpportunityList.RootTable.Columns("CloseDate").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub grdOpportunityList_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdOpportunityList.LinkClicked
        Try
            If e.Column.Key = "No Of Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = "frmopportunity"
                frm._VoucherId = Val(Me.grdOpportunityList.GetRow.Cells("OpportunityId").Value.ToString)
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

   

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        '  Dim sum As Integer = 0

        'For i As Integer = 0 To grdOpportunityList.Rows.Count - 1 - 1
        'sum += Convert.ToInt32(grdOpportunityList.Rows(i).Cells("ToTal Amount").Value)
        'Next

        'If TextBox1.Text = String.Empty AndAlso TextBox1.Text <> String.Empty Then
        'TextBox1.Text = Convert.ToInt32(0).ToString()
        'Else
        'Dim value As Integer = Convert.ToInt32(TextBox1.Text)
        'Dim result As Integer = value * sum
        '     TextBox1.Text = result.ToString()
        'End If
    End Sub

    Private Sub grdOpportunityList_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdOpportunityList.FormattingRow


    End Sub

    Private Sub pnlHeader_Paint(sender As Object, e As PaintEventArgs) Handles pnlHeader.Paint

    End Sub
End Class