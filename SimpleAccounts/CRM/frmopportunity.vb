''This screen is built on 04-10-2018 by Muhammad Amin against TASK TFS4591 and TFS4593
'' TASK TFS4867 : Implementation of displaying Employee image and Name mapped against a user. Done on 22-10-2018 by Muhammad Amin
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports System.IO
Imports System.Text
Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions
Imports GemBox.SpreadSheet
'Imports System.Net.Mime.MediaTypeNames
Public Class frmopportunity
    Implements IGeneral
    Public IsEditMode As Boolean = False
    Dim DoHaveDeleteRights As Boolean = False
    Dim DAL As New OpportunityDAL
    Dim Obj As OpportunityBE
    Dim OpportunityId As Integer = 0
    Dim OpportunityName As String = String.Empty
    Dim TotalAttachments As Integer = 0
    Dim arrFile As New List(Of String)
    Dim ObjPath As String = String.Empty
    Dim EmployeeId As Integer = 0
    Dim EmailTemplate As String = String.Empty
    Dim UsersEmail As List(Of String)
    Dim EmailBody As String = String.Empty
    Dim SalesOrderId As Integer
    Dim AllFields As List(Of String)
    Dim AfterFieldsElement As String = String.Empty
    Dim dtEmail As DataTable
    Dim dtEmailMaster As DataTable
    Dim EmailDAL As New EmailTemplateDAL
    Dim html As StringBuilder
    Dim OpportunityNo As String
    Public Structure OpportunityType
        Public Shared Hardware As String = "Hardware"
        Public Shared Support As String = "Support"
    End Structure

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub New(ByVal Rights As RightsModel)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        EnableDisableControls(Rights, False, "Support")
        FillAllCombos()
        ReSetControls()
    End Sub
    Public Sub New(ByVal Obj As OpportunityBE, ByVal Rights As RightsModel)

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        EnableDisableControls(Rights, True, Obj.OpportunityType)
        FillAllCombos()
        ReSetControls()
        EditRecord(Obj)
    End Sub

    Private Sub frmopportunity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'rafay 11-4-2022
        'If rbHardware.Checked = True Then
        '    ChkBatteriesIncluded.Visible = False
        'End If
        'rafay
        'If rbHardware.Checked = True Then
        '    pnlHarware.Visible = True
        '    pnlSupport.Visible = False
        'End If
        'rbSupport.Checked = True
        'pnlHarware.Visible = False
        'pnlSupport.Visible = True
        'End If
    End Sub
    'Rafay:Task Start:function is create to check serial no if serial no is duplicate then show message serial no already exist
    Public Function CheckDuplicateSerialNo() As Boolean
        Try
            Dim Display As String
            If Me.grdSupport.RowCount = 0 Then Return False
            For i As Int32 = 0 To Me.grdSupport.RowCount - 1
                For j As Int32 = i + 1 To Me.grdSupport.RowCount - 1
                    If Me.grdSupport.GetRows(j).Cells("SerialNo").Value.ToString.Length > 0 Then
                        If Me.grdSupport.GetRows(j).Cells("SerialNo").Value.ToString = Me.grdSupport.GetRows(i).Cells("SerialNo").Value.ToString Then
                            Display = Me.grdSupport.GetRows(i).Cells("SerialNo").Value.ToString
                            ShowErrorMessage("Serial No [ " & Display & " ] already exist ")
                            Return True
                        End If
                    End If
                Next
            Next
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CheckType() As Boolean
        Try
            If Me.grdSupport.RowCount = 0 Then Return False
            For j As Int32 = 0 To Me.grdSupport.RowCount - 1
                If Me.grdSupport.GetRows(j).Cells("Type").Value.ToString.Length = 0 Then
                    frmMessage.MsgBox("Please fill all Types in your details", "")
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'rafay:Task End
    Private Sub cmbSupportProbability_InitializeLayout(sender As Object, e As Win.UltraWinGrid.InitializeLayoutEventArgs)

    End Sub

    Private Sub pnlSupport_Paint(sender As Object, e As PaintEventArgs)

    End Sub
    Public Sub FillCombos(Optional Condition As String = "")
        Try
            Dim str As String
            If Condition = "Company" Then
                str = "Select LeadProfileId, CompanyName FROM tblDefLeadProfile WHERE ISNULL(IsAccountCreated, 0) = 1"
                FillUltraDropDown(Me.cmbCompany, str, True)
                Me.cmbCompany.Rows(0).Activate()
                Me.cmbCompany.DisplayLayout.Bands(0).Columns("LeadProfileId").Hidden = True
            ElseIf Condition = "ContactPerson" Then
                str = "Select ContactId, FirstName +' '+ LastName AS FullName FROM tblDefLeadProfileContacts WHERE LeadProfileId = " & Me.cmbCompany.Value & ""
                FillUltraDropDown(Me.cmbPerson, str, True)
                Me.cmbPerson.Rows(0).Activate()
                Me.cmbPerson.DisplayLayout.Bands(0).Columns("ContactId").Hidden = True
            ElseIf Condition = "EndUser" Then
                'str = "Select Id, Title from tblDefLeadIndustry where Active = 1"
                'FillUltraDropDown(Me.cmbEndUser, str, True)
                'Me.cmbEndUser.Rows(0).Activate()
                'Me.cmbEndUser.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "Type" Then
                str = "select Id, Title From tblDefOpportunityType Where Active = 1"
                FillUltraDropDown(Me.cmbType, str, True)
                Me.cmbType.Rows(0).Activate()
                Me.cmbType.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "Currency" Then
                str = "select currency_id AS Id , currency_code AS Currency From tblCurrency "
                FillUltraDropDown(Me.cmbCurrency, str, False)
                Me.cmbCurrency.Rows(0).Activate()
                Me.cmbCurrency.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "Stage" Then
                str = "select Id, Title From tblDefOpportunityStage Where Active = 1"
                FillUltraDropDown(Me.cmbStage, str, True)
                Me.cmbStage.Rows(0).Activate()
                Me.cmbStage.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "LoosReason" Then
                str = "select Id, Title From tblDefOpportunityLoosReason Where Active = 1"
                FillUltraDropDown(Me.cmbReasonforloosing, str, True)
                Me.cmbReasonforloosing.Rows(0).Activate()
                Me.cmbReasonforloosing.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "Probability" Then
                str = "select Id, Title From tblDefOpportunityProbability Where Active = 1"
                FillUltraDropDown(Me.cmbProbability, str, True)
                Me.cmbProbability.Rows(0).Activate()
                Me.cmbProbability.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "PaymentHardware" Then
                str = "SELECT Id, Title From tblDefOpportunityPayment Where Active = 1"
                FillUltraDropDown(Me.cmbPaymentHardware, str, True)
                Me.cmbPaymentHardware.Rows(0).Activate()
                Me.cmbPaymentHardware.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "PaymentSupport" Then
                str = "SELECT Id, Title From tblDefOpportunityPayment Where Active = 1"
                FillUltraDropDown(Me.cmbPaymentSupport, str, True)
                Me.cmbPaymentSupport.Rows(0).Activate()
                Me.cmbPaymentSupport.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "DeliveryInformation" Then
                str = "SELECT Id, Title AS Delivery From tblDefOpportunityDelivery Where Active = 1 "
                FillUltraDropDown(Me.cmbDeliveryInformation, str, True)
                Me.cmbDeliveryInformation.Rows(0).Activate()
                Me.cmbDeliveryInformation.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "InvoiceFrequency" Then
                str = "SELECT Id, Title AS Frequency From tblDefOpportunityFrequency Where Active = 1 "
                FillUltraDropDown(Me.cmbInvoiceFrequency, str, True)
                Me.cmbInvoiceFrequency.Rows(0).Activate()
                Me.cmbInvoiceFrequency.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "SupportType" Then
                str = "SELECT Id, Title AS Frequency From tblDefOpportunitySupportType Where Active = 1 "
                FillUltraDropDown(Me.cmbSupportType, str, True)
                Me.cmbSupportType.Rows(0).Activate()
                Me.cmbSupportType.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "PreventionMaintenance" Then
                str = "SELECT Id, Title AS Maintenance From tblDefOpportunityMaintenance Where Active = 1 "
                FillUltraDropDown(Me.cmbPreventionMaintenance, str, True)
                Me.cmbPreventionMaintenance.Rows(0).Activate()
                Me.cmbPreventionMaintenance.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            ElseIf Condition = "GrdType" Then
                str = "SELECT Type, Type from tblHardwareType"
                Dim dt As DataTable
                dt = GetDataTable(str)
                dt.AcceptChanges()
                Me.grdHardware.RootTable.Columns("Type").ValueList.PopulateValueList(dt.DefaultView, "Type", "Type")
            ElseIf Condition = "GrdBrand" Then
                str = "select Title, Title From tblDefBrandFocus Where Active = 1"
                Dim dt As DataTable
                dt = GetDataTable(str)
                dt.AcceptChanges()
                Me.grdSupport.RootTable.Columns("Brand").ValueList.PopulateValueList(dt.DefaultView, "Title", "Title")
                Me.grdHardware.RootTable.Columns("BrandNo").ValueList.PopulateValueList(dt.DefaultView, "Title", "Title")
            ElseIf Condition = "GrdOnsite" Then
                str = "select Onsite, Onsite from tblOpportunityOnsite"
                FillDropDown(cmbOnsiteIntervention, str, True)
            ElseIf Condition = "GrdFixTime" Then
                str = "select FixTime, FixTime from tblOpportunityFixTime"
                Dim dt As DataTable
                dt = GetDataTable(str)
                dt.AcceptChanges()
                Me.grdSupport.RootTable.Columns("SLAFixTime").ValueList.PopulateValueList(dt.DefaultView, "FixTime", "FixTime")
            ElseIf Condition = "GrdCoverrage" Then
                str = "select Coverrage, Coverrage from tblOppotunitySLACoverage"
                Dim dt As DataTable
                dt = GetDataTable(str)
                dt.AcceptChanges()
                Me.grdSupport.RootTable.Columns("SLACoverage").ValueList.PopulateValueList(dt.DefaultView, "Coverrage", "Coverrage")
            ElseIf Condition = "GrdInternentionTime" Then
                str = "select InternentionTime, InternentionTime from tblOpportunityIntervention"
                Dim dt As DataTable
                dt = GetDataTable(str)
                dt.AcceptChanges()
                Me.grdSupport.RootTable.Columns("SLAInterventionTime").ValueList.PopulateValueList(dt.DefaultView, "InternentionTime", "InternentionTime")
            ElseIf Condition = "grdSuuportType" Then
                str = "SELECT Name, Name from tblSupportType"
                Dim dt As DataTable
                dt = GetDataTable(str)
                dt.AcceptChanges()
                Me.grdSupport.RootTable.Columns("Type").ValueList.PopulateValueList(dt.DefaultView, "Name", "Name")
            ElseIf Condition = "Country" Then
                str = "select CountryId, CountryName From tblListCountry Where Active = 1 order by 2 asc"
                FillUltraDropDown(Me.cmbCountry, str, True)
                Me.cmbCountry.Rows(0).Activate()
                Me.cmbCountry.DisplayLayout.Bands(0).Columns("CountryId").Hidden = True
            ElseIf Condition = "GrdCountry" Then
                str = "select CountryName, CountryName From tblListCountry Where Active = 1"
                Dim dtCountry As DataTable = GetDataTable(str)
                dtCountry.AcceptChanges()
                Me.grdSupport.RootTable.Columns("Country").ValueList.PopulateValueList(dtCountry.DefaultView, "CountryName", "CountryName")
            ElseIf Condition = "Owner" Then
                str = "SELECT User_ID, FULLNAME from tblUSER "
                FillDropDown(Me.cmbOpportuniyyOwner, str, True)
            ElseIf Condition = "CoverageWindow" Then
                str = "select InternentionTime, InternentionTime from tblOpportunityIntervention"
                'FillUltraDropDown(Me.cmbInterestedIn, str, True)
                'Me.UiListControl1.Rows(0).Activate()
                'Me.UiListControl1.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                FillListBox(Me.UiListControl1.ListItem, str)
            ElseIf Condition = "OnsiteIntervention" Then
                str = "select InternentionTime, InternentionTime from tblOpportunityIntervention"
                'FillUltraDropDown(Me.cmbInterestedIn, str, True)
                'Me.UiListControl1.Rows(0).Activate()
                'Me.UiListControl1.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                FillListBox(Me.lstInterestedIn.ListItem, str)
            ElseIf Condition = "Item" Then
                str = "SELECT ArticleId AS Id, ArticleCode AS Code, ArticleDescription AS Item FROM ArticleDefView Where ArticleDefView.Active=1  and ArticleDefView.ArticleGroupId = 8"
                FillUltraDropDown(Me.cmbItem, str, True)
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Item").Width = 350
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FillAllCombos()
        Try
            FillCombos("Company")
            FillCombos("EndUser")
            FillCombos("Type")
            FillCombos("Currency")
            FillCombos("Stage")
            FillCombos("LoosReason")
            FillCombos("Probability")
            FillCombos("PaymentHardware")
            FillCombos("PaymentSupport")
            FillCombos("DeliveryInformation")
            FillCombos("InvoiceFrequency")
            FillCombos("Item")
            FillCombos("SupportType")
            FillCombos("PreventionMaintenance")
            FillCombos("GrdType")
            FillCombos("GrdBrand")
            FillCombos("GrdOnsite")
            FillCombos("GrdFixTime")
            FillCombos("GrdCoverrage")
            FillCombos("GrdInternentionTime")
            FillCombos("GrdCountry")
            FillCombos("Owner")
            FillCombos("Country")
            FillCombos("CoverageWindow")
            FillCombos("OnsiteIntervention")
            FillCombos("grdSuuportType")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rbHardware_CheckedChanged(sender As Object, e As EventArgs) Handles rbHardware.CheckedChanged
        If Me.rbHardware.Checked Then
            pnlHarware.Visible = True
            'rafay 11-4-22
            ChkBatteriesIncluded.Visible = False
            'rafay 11-4-22
            pnlSupport.Visible = False
            Me.cmbType.Visible = False
            Me.Label5.Visible = False
            'Me.txtContactName.Visible = True
            'Me.lblContactName.Visible = True
            CtrlGrdBar2.Visible = True
            CtrlGrdBar2.BringToFront()
            CtrlGrdBar1.Visible = False
            CtrlGrdBar1.SendToBack()
        Else
            CtrlGrdBar2.Visible = False
            CtrlGrdBar2.SendToBack()
            CtrlGrdBar1.Visible = True
            CtrlGrdBar1.BringToFront()
            pnlHarware.Visible = False
            pnlSupport.Visible = True
            'Me.txtContactName.Visible = False
            'Me.lblContactName.Visible = False
            Me.cmbType.Visible = True
            'Rafay
            ChkBatteriesIncluded.Visible = True
            'Rafay
            Me.Label5.Visible = True
        End If
    End Sub

    ''
    'Private Sub exportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles exportExcel.Click
    '    Dim xlApp As Excel.Application
    '    Dim xlWorkBook As Excel.Workbook
    '    Dim xlWorkSheet As Excel.Worksheet
    '    Dim misValue As Object = System.Reflection.Missing.Value
    '    Dim i As Integer
    '    Dim j As Integer
    '    xlApp = New Excel.Application
    '    xlWorkBook = xlApp.Workbooks.Add(misValue)
    '    xlWorkSheet = xlWorkBook.Sheets("sheet1")

    '    For Each col As DataGridViewColumn In Me.BurnPermitInformationDataGridView.Columns
    '        xlWorkSheet.Cells(1, col.Index + 1) = col.HeaderText.ToString
    '    Next
    '    For i = 2 To Me.BurnPermitInformationDataGridView.Rows.Count - 1
    '        For j = 0 To Me.BurnPermitInformationDataGridView.ColumnCount
    '            xlWorkSheet.Cells(i + 1, j + 1) = Me.BurnPermitInformationDataGridView(j, i).Value.ToString()
    '        Next
    '    Next

    '    xlWorkBook.Activate()

    '    '//get path
    '    Me.FolderBrowserDialog1.ShowDialog()
    '    Dim path As String = Me.FolderBrowserDialog1.SelectedPath

    '    xlWorkBook.SaveAs(path & "\burn permit export.xls")
    '    'xlWorkSheet.SaveAs("burn permit export.xls")

    '    xlWorkBook.Close()
    '    xlApp.Quit()

    '    releaseObject(xlApp)
    '    releaseObject(xlWorkBook)
    '    releaseObject(xlWorkSheet)

    '    MsgBox("You can find your report at " & path & "\burn permit export.xls")
    'End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos1(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Obj = New OpportunityBE()
            Obj.OpportunityId = OpportunityId
            If IsEditMode = True Then
                Obj.DocNo = txtDocumentNo.Text
            Else
                Obj.DocNo = GetDocumentNo()
            End If
            Obj.DocDate = Me.dtpDocDate.Value
            Obj.CompanyId = Me.cmbCompany.Value
            Obj.ContactId = Me.cmbPerson.Value
            'rafay assign value 11-4-2022
            Obj.ChkBoxBatteriesIncluded = IIf(Me.ChkBatteriesIncluded.Checked = True, 1, 0)
            Obj.EndUserId = Me.cmbEndUser.Text
            Obj.OpportunityName = Me.txtOpportunityName.Text
            Obj.TypeId = Me.cmbType.Value
            Obj.CurrencyId = Me.cmbCurrency.Value
            Obj.OpportunityOwner = Me.cmbOpportuniyyOwner.Text
            Obj.CloseDate = Me.dtpCloseDate.Value
            Obj.StageId = Me.cmbStage.Value
            Obj.LoosReasonId = Me.cmbReasonforloosing.Value
            Obj.ProbabilityId = Me.cmbProbability.Value
            Obj.UserName = LoginUserName
            Obj.ModifiedUser = LoginUserName
            Obj.ModifiedDate = Now
            Obj.EmployeeId = EmployeeId
            Obj.UserId = cmbOpportuniyyOwner.SelectedValue
            Obj.TotalAmount = lblTotalAmount.Text
            If Me.rbHardware.Checked = True Then
                ''Reset support fields
                'Obj.TaxAmount = 0
                Obj.TaxAmount = Val(Me.txtSST.Text)
                Obj.Duration = ""
                Obj.StartDate = Now
                Obj.FrequencyId = 0
                Obj.ImplementationTime = ""
                Obj.SupportTypeId = 0
                Obj.MaterialLocation = ""
                Obj.TargetPrice = 0
                Obj.MaintenanceId = 0
                Obj.OnsiteId = ""
                ''
                Obj.ContactName = Me.txtContactName.Text
                Obj.OpportunityType = OpportunityType.Hardware
                Obj.LeadTimeInDays = Me.txtLeadTime.Text
                Obj.Freight = Val(txtFreight.Text)
                Obj.PaymentId = cmbPaymentHardware.Value
                Obj.CountryId = Me.cmbCountry.Value
                Obj.DeliveryId = Me.cmbDeliveryInformation.Value
                Me.grdHardware.UpdateData()
                For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdHardware.GetRows
                    Dim HardwareDetail As New OpportunityHardwareDetailBE()
                    '                [OpportunityHardwareDetailId] [int] IDENTITY(1,1) NOT NULL,
                    '[OpportunityId] [int] NULL,
                    '[PartNo] [nvarchar](1000) NULL,
                    '[Type] [nvarchar](250) NULL,
                    '[BrandNo] [nvarchar](1000) NULL,
                    '[Description] [nvarchar](2500) NULL,
                    '[Status] [nvarchar](50) NULL,
                    '[Warranty] [nvarchar](100) NULL,
                    '[DeliveryInfo] [nvarchar](100) NULL,
                    '[Qty] [nvarchar](100) NULL,
                    '[Price] [float] NULL,
                    '[Discount] [float] NULL,
                    '[TotalAmount] [float] NULL,
                    HardwareDetail.OpportunityHardwareDetailId = Val(Row.Cells("OpportunityHardwareDetailId").Value.ToString)
                    HardwareDetail.OpportunityId = Val(Row.Cells("OpportunityId").Value.ToString)
                    HardwareDetail.PartNo = Row.Cells("PartNo").Value.ToString
                    HardwareDetail.Type = Row.Cells("Type").Value.ToString
                    HardwareDetail.BrandNo = Row.Cells("BrandNo").Value.ToString
                    HardwareDetail.Description = Row.Cells("Description").Value.ToString
                    HardwareDetail.Status = Row.Cells("Status").Value.ToString
                    HardwareDetail.Warranty = Row.Cells("Warranty").Value.ToString
                    HardwareDetail.LeadTime = Row.Cells("LeadTime").Value.ToString
                    'HardwareDetail.DeliveryInfo = Row.Cells("DeliveryInfo").Value.ToString
                    HardwareDetail.Qty = Val(Row.Cells("Qty").Value.ToString)
                    HardwareDetail.Price = Val(Row.Cells("Price").Value.ToString)
                    HardwareDetail.Discount = Val(Row.Cells("Discount").Value.ToString)
                    HardwareDetail.TotalAmount = Val(Row.Cells("TotalAmount").Value.ToString)
                    HardwareDetail.FilePath = Row.Cells("FilePath").Value.ToString
                    Obj.HardwareDetail.Add(HardwareDetail)
                Next
            ElseIf rbSupport.Checked Then
                ''Reset Hardware fields
                Obj.ContactName = ""
                Obj.LeadTimeInDays = ""
                Obj.Freight = 0
                Obj.DeliveryId = 0
                'Obj.ContactName = ""
                Obj.TaxAmount = Val(Me.txtSST.Text)
                Obj.Duration = Me.txtDurationInMonths.Text
                Obj.StartDate = Me.dtpStartDate.Value
                Obj.PaymentId = Me.cmbPaymentSupport.Value
                'Obj.CountryId = Me.cmbCountry.Value
                Obj.FrequencyId = Me.cmbInvoiceFrequency.Value
                Obj.ImplementationTime = Me.txtImplementationTime.Text
                Obj.SupportTypeId = Me.cmbSupportType.Value
                Obj.MaterialLocation = Me.txtMaterialLocation.Text
                Obj.TargetPrice = Val(Me.txtTargetPrice.Text)
                Obj.MaintenanceId = Me.cmbPreventionMaintenance.Value
                Obj.OnsiteId = cmbOnsiteIntervention.Text
                Obj.CoverageWindow = UiListControl1.SelectedIDs
                Obj.OnsiteIntervention = lstInterestedIn.SelectedIDs
                Obj.DurationofMonth = cmbDurationofMonth.Text
                Obj.InvoicePattern = cmbInvoicePattern.Text
                Obj.ArticleId = cmbItem.Value
                Obj.PMFrequency = cmbPMFrequency.Text
                Obj.OpportunityType = OpportunityType.Support
                Me.grdSupport.UpdateData()
                For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSupport.GetRows
                    Dim SupportDetail As New OpportunitySupportDetailBE()
                    '[OpportunitySupportDetailId] [int] IDENTITY(1,1) NOT NULL,
                    '[OpportunityId] [int] NULL,
                    '[Brand] [nvarchar](1000) NULL,
                    '[ModelNo] [nvarchar](1000) NULL,
                    '[SerialNo] [nvarchar](1000) NULL,
                    '[SLACoverage] [nvarchar](1000) NULL,
                    '[Address] [nvarchar](500) NULL,
                    '[City] [nvarchar](100) NULL,
                    '[Province] [nvarchar](100) NULL,
                    '[Country] [nvarchar](100) NULL,
                    '[StartDate] [datetime] NULL,
                    '[EndDate] [datetime] NULL,
                    '[Type] [nvarchar](250) NULL,
                    '[UnitPrice] [float] NULL,
                    SupportDetail.OpportunitySupportDetailId = Val(Row.Cells("OpportunitySupportDetailId").Value.ToString)
                    SupportDetail.OpportunityId = Val(Row.Cells("OpportunityId").Value.ToString)
                    SupportDetail.Brand = Row.Cells("Brand").Value.ToString
                    SupportDetail.ModelNo = Row.Cells("ModelNo").Value.ToString
                    SupportDetail.SerialNo = Row.Cells("SerialNo").Value.ToString
                    'SupportDetail.SLA = Row.Cells("SLA").Value.ToString
                    SupportDetail.SLACoverage = Row.Cells("SLACoverage").Value.ToString
                    SupportDetail.SLAInterventionTime = Row.Cells("SLAInterventionTime").Value.ToString
                    SupportDetail.SLAFixTime = Row.Cells("SLAFixTime").Value.ToString
                    ' SupportDetail.OnsiteIntervention = Row.Cells("OnsiteInetvention").Value.ToString
                    SupportDetail.Address = Row.Cells("Address").Value.ToString
                    SupportDetail.City = Row.Cells("City").Value.ToString
                    SupportDetail.Province = Row.Cells("Province").Value.ToString
                    SupportDetail.Country = Row.Cells("Country").Value.ToString
                    SupportDetail.StartDate = IIf(Row.Cells("StartDate").Value Is DBNull.Value, Now, Row.Cells("StartDate").Value)
                    SupportDetail.EndDate = IIf(Row.Cells("EndDate").Value Is DBNull.Value, Now, Row.Cells("EndDate").Value)
                    SupportDetail.Type = Row.Cells("Type").Value.ToString
                    SupportDetail.UnitPrice = Val(Row.Cells("UnitPrice").Value.ToString)
                    SupportDetail.FilePath = Row.Cells("FilePath").Value.ToString
                    Obj.SupportDetail.Add(SupportDetail)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbCompany.ActiveRow.Index < 1 Then
                ShowErrorMessage("Company is required.")
                Me.cmbCompany.Focus()
                Return False
            End If
            If Not Me.txtOpportunityName.Text.Length > 0 Then
                ShowErrorMessage("Opportunity name is required.")
                Me.txtOpportunityName.Focus()
                Return False
            End If
            If rbHardware.Checked = True AndAlso Me.grdHardware.RowCount = 0 Then
                ShowErrorMessage("At least one row at grid is required.")
                Me.grdHardware.Focus()
                Return False
            End If
            If rbSupport.Checked = True AndAlso Me.grdSupport.RowCount = 0 Then
                ShowErrorMessage("At least one row at grid is required.")
                Me.grdSupport.Focus()
                Return False
            End If
            If cmbItem.Visible = True AndAlso cmbItem.ActiveRow.Index < 1 Then
                MsgBox("Item is required to put this opportunity into WON.")
                Me.cmbItem.Focus()
                Return False
            End If
            'If rbSupport.Checked = True And cmbStage.Value = 8 Then
            '    If cmbItem.Value Then
            '    End If
            'Rafay:Task Start:To check serial no if serial no is dublicate then show error
            'If CheckDuplicateSerialNo() = True Then
            '    Me.grdSupport.Focus()
            '    Return False
            'End If
            'Rafay:Task End
            If CheckType() = True Then
                Me.grdSupport.Focus()
                Return False
            End If

            If IsEditMode = False Then
                ' If DAL.IsOpportunityExisted(Me.txtOpportunityName.Text) Then
                '    ShowErrorMessage("Opportunity name already exists.")
                '    Me.txtOpportunityName.Focus()
                '    Return False
                'End If
            Else
                'If OpportunityName <> Me.txtOpportunityName.Text Then
                '    If DAL.IsOpportunityExisted(Me.txtOpportunityName.Text) Then
                '        ShowErrorMessage("Opportunity name already exists.")
                '        Me.txtOpportunityName.Focus()
                '        Return False
                '    End If
                'End If
            End If

            If chkVATMandatory.Checked = False And txtSST.Text = "" Then
                ShowErrorMessage("Please add SST/VAT.")
                Me.txtSST.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            ObjPath = getConfigValueByType("FileAttachmentPath").ToString
            Me.dtpDocDate.Value = Now
            Me.txtDocumentNo.Text = GetDocumentNo()
            Me.cmbCompany.Rows(0).Activate()
            If Me.cmbPerson.ActiveRow IsNot Nothing Then
                Me.cmbPerson.Rows(0).Activate()
            End If
            Me.cmbEndUser.Text = String.Empty
            Me.cmbCountry.Rows(0).Activate()
            Me.txtOpportunityName.Text = String.Empty
            Me.cmbType.Rows(0).Activate()
            Me.cmbCurrency.Rows(0).Activate()
            Me.txtOpportunityOwner.Text = String.Empty
            Me.dtpCloseDate.Value = Now
            Me.cmbStage.Rows(0).Activate()
            Me.cmbReasonforloosing.Rows(0).Activate()
            Me.cmbProbability.Rows(0).Activate()
            Me.txtContactName.Text = String.Empty
            Me.txtLeadTime.Text = String.Empty
            cmbOpportuniyyOwner.SelectedValue = LoginUserId
            cmbOnsiteIntervention.SelectedValue = 0
            cmbDurationofMonth.SelectedIndex = 0
            cmbPMFrequency.SelectedIndex = 0
            cmbInvoicePattern.SelectedIndex = 0
            txtFreight.Text = String.Empty
            cmbPaymentHardware.Rows(0).Activate()
            Me.UiListControl1.DeSelect()
            Me.lstInterestedIn.DeSelect()
            Me.cmbDeliveryInformation.Rows(0).Activate()
            Me.txtSST.Text = String.Empty
            Me.txtDurationInMonths.Text = String.Empty
            Me.dtpStartDate.Value = Now
            'Rafay
            ''companyinitials = ""
            If ChkBatteriesIncluded.Checked = True Then
                ChkBatteriesIncluded.Checked = False
            End If
            'Rafay
            Me.cmbInvoiceFrequency.Rows(0).Activate()
            Me.cmbInvoiceFrequency.Rows(0).Activate()
            Me.txtImplementationTime.Text = String.Empty
            Me.cmbSupportType.Rows(0).Activate()
            Me.txtMaterialLocation.Text = String.Empty
            Me.txtTargetPrice.Text = String.Empty
            Me.cmbPreventionMaintenance.Rows(0).Activate()
            OpportunityName = String.Empty
            OpportunityId = 0
            lblTotalAmount.Text = ""
            'btnSave.Text = "Save"
            IsEditMode = False
            GetSupportDetail(-1)
            GetHardwareDetail(-1)
            '' TASK TFS4867
            GetUseWiseEmployee(LoginUserId)
            '' END TFS4867
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If DAL.Add(Obj, ObjPath, arrFile) Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If DAL.Update(Obj, ObjPath, arrFile) Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & CtrlGrdBar1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.CtrlGrdBar1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                grdSupport.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & CtrlGrdBar2.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.CtrlGrdBar2.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                grdHardware.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub EnableDisableControls(ByVal Rights As RightsModel, ByVal IsEditMode As Boolean, ByVal _opportunityType As String)
        Try
            CtrlGrdBar1.mGridChooseFielder.Enabled = Rights.FieldChooser
            CtrlGrdBar1.mGridPrint.Enabled = Rights.Print
            CtrlGrdBar1.mGridExport.Enabled = Rights.Export
            CtrlGrdBar2.mGridChooseFielder.Enabled = Rights.FieldChooser
            CtrlGrdBar2.mGridPrint.Enabled = Rights.Print
            CtrlGrdBar2.mGridExport.Enabled = Rights.Export
            If IsEditMode = False Then
                rbSupport.Checked = True
                Me.btnSave.Text = "Save"
                Me.btnSaveHardware.Text = "Save"
                Me.btnSave.Enabled = Rights.Save
                Me.btnSaveHardware.Enabled = Rights.Save
                Me.btnExport.Enabled = Rights.Export
                Me.btnSaveAndExport.Enabled = Rights.SaveAndExport
                rbHardware.Enabled = True
                rbSupport.Enabled = True
                cmbCompany.Enabled = True
                Me.btnPrint.Visible = False
                Me.Button1.Visible = False

            Else
                If _opportunityType = OpportunityType.Hardware Then
                    rbHardware.Checked = True
                ElseIf _opportunityType = OpportunityType.Support Then
                    rbSupport.Checked = True
                End If
                rbHardware.Enabled = False
                rbSupport.Enabled = False
                cmbCompany.Enabled = False
                Me.btnSave.Text = "Update"
                Me.btnSaveHardware.Text = "Update"
                Me.btnSave.Enabled = Rights.Update
                Me.btnSaveHardware.Enabled = Rights.Update
                Me.btnExport.Enabled = Rights.Export
                Me.btnSaveAndExport.Enabled = Rights.SaveAndExport
                Me.btnPrint.Visible = True
                Me.Button1.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetHardwareDetail(ByVal OpportunityId As Integer)
        Try
            Dim dt As DataTable = GetDataTable("Select OpportunityHardwareDetailId, OpportunityId, PartNo, Type, BrandNo, Description, Status,Warranty,Qty,LeadTime, Price, Discount, TotalAmount, FilePath from tblDefOpportunityHardwareDetail  WHERE OpportunityId=" & OpportunityId)
            dt.Columns("TotalAmount").Expression = "(ISNULL(Qty, 0)*ISNULL(Price, 0))-ISNULL(Discount, 0)"
            Me.grdHardware.DataSource = dt
            Me.grdHardware.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdHardware.RootTable.Columns("Price").FormatString = "N" & DecimalPointInValue
            Me.grdHardware.RootTable.Columns("Discount").FormatString = "N" & DecimalPointInValue
            Me.grdHardware.RootTable.Columns("TotalAmount").FormatString = "N" & DecimalPointInValue

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSupportDetail(ByVal OpportunityId As Integer)
        Try
            Me.grdSupport.DataSource = New OpportunitySupportDetailDAL().GetDetail(OpportunityId)
            Me.grdSupport.RootTable.Columns("UnitPrice").FormatString = "N" & DecimalPointInValue
            Me.grdSupport.RootTable.Columns("StartDate").FormatString = str_DisplayDateFormat
            Me.grdSupport.RootTable.Columns("EndDate").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSaveHardware_Click(sender As Object, e As EventArgs) Handles btnSaveHardware.Click
        Try
            If IsValidate() Then
                If IsEditMode = False Then
                    Save()
                    If cmbStage.Value = 2 Then
                        SendAutoEmail("Hardware")
                    End If
                    ReSetControls()
                    msg_Information("Record has been saved successfully.")
                    'Me.Close()
                Else
                    If msg_Confirm("Do you want to update record?") = False Then Exit Sub
                    Update1()
                    If cmbStage.Value = 2 Then
                        SendAutoEmail("Hardware")
                    End If
                    ReSetControls()
                    msg_Information("Record has been Updated successfully.")
                    'Me.Close()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function GetDocumentNo() As String
        Dim StandardNo As String = String.Empty
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                'rafay
                'StandardNo = GetSerialNo("OP" + "-" + Microsoft.VisualBasic.Right(dtpDocDate.Value.Year, 2) + "-", "tblDefOpportunity", "DocNo")
                If CompanyPrefix = "V-ERP (UAE)" Then
                    'companyinitials = "UE"
                    StandardNo = GetSerialNo("OP" & "-" + Microsoft.VisualBasic.Right(dtpDocDate.Value.Year, 2) + "-", "tblDefOpportunity", "DocNo")
                Else
                    ''companyinitials = "PK"
                    StandardNo = GetNextDocNo("OP" & "-" & companyinitials & "-" & Format(dtpDocDate.Value, "yy"), 4, "tblDefOpportunity", "DocNo")
                    'ElseIf CompanyPrefix = "V-ERP (KSA)" Then
                    '    companyinitials = "KSA"
                    '    StandardNo = GetNextDocNo("OP" & "-" & companyinitials & "-" & Format(dtpDocDate.Value, "yy"), 4, "tblDefOpportunity", "DocNo")
                    'ElseIf CompanyPrefix = "V-ERP (MY)" Then
                    '    companyinitials = "MY"
                    '    StandardNo = GetNextDocNo("OP" & "-" & companyinitials & "-" & Format(dtpDocDate.Value, "yy"), 4, "tblDefOpportunity", "DocNo")
                    'ElseIf CompanyPrefix = "V-ERP (Remms-PAK)" Then
                    '    companyinitials = "RPK"
                    '    StandardNo = GetNextDocNo("OP" & "-" & companyinitials & "-" & Format(dtpDocDate.Value, "yy"), 4, "tblDefOpportunity", "DocNo")
                    'ElseIf CompanyPrefix = "V-ERP (Remms-UAE)" Then
                    '    companyinitials = "RUAE"
                    '    StandardNo = GetNextDocNo("OP" & "-" & companyinitials & "-" & Format(dtpDocDate.Value, "yy"), 4, "tblDefOpportunity", "DocNo")
                End If
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                'rafay
                'StandardNo = GetSerialNo("OP" + "-" + Microsoft.VisualBasic.Right(dtpDocDate.Value.Year, 2) + "-", "tblDefOpportunity", "DocNo")
                If CompanyPrefix = "V-ERP (UAE)" Then
                    'companyinitials = "UE"
                    StandardNo = GetSerialNo("OP" & "-" + Microsoft.VisualBasic.Right(dtpDocDate.Value.Year, 2) + "-", "tblDefOpportunity", "DocNo")
                Else
                    ''companyinitials = "PK"
                    StandardNo = GetNextDocNo("OP" & "-" & companyinitials & "-" & Format(dtpDocDate.Value, "yy"), 4, "tblDefOpportunity", "DocNo")
                    'ElseIf CompanyPrefix = "V-ERP (KSA)" Then
                    '    companyinitials = "KSA"
                    '    StandardNo = GetNextDocNo("OP" & "-" & companyinitials & "-" & Format(dtpDocDate.Value, "yy"), 4, "tblDefOpportunity", "DocNo")
                    'ElseIf CompanyPrefix = "V-ERP (MY)" Then
                    '    companyinitials = "MY"
                    '    StandardNo = GetNextDocNo("OP" & "-" & companyinitials & "-" & Format(dtpDocDate.Value, "yy"), 4, "tblDefOpportunity", "DocNo")
                    'ElseIf CompanyPrefix = "V-ERP (Remms-PAK)" Then
                    '    companyinitials = "RPK"
                    '    StandardNo = GetNextDocNo("OP" & "-" & companyinitials & "-" & Format(dtpDocDate.Value, "yy"), 4, "tblDefOpportunity", "DocNo")
                    'ElseIf CompanyPrefix = "V-ERP (Remms-UAE)" Then
                    '    companyinitials = "RUAE"
                    '    StandardNo = GetNextDocNo("OP" & "-" & companyinitials & "-" & Format(dtpDocDate.Value, "yy"), 4, "tblDefOpportunity", "DocNo")
                End If
                '  StandardNo = GetNextDocNo("OP" & "-" & Format(dtpDocDate.Value, "yy") & dtpDocDate.Value.Month.ToString("00"), 4, "tblDefOpportunity", "DocNo")
                'rafay
            Else
                StandardNo = GetNextDocNo("OP", 6, "tblDefOpportunity", "StandardNo")
            End If
            'Public str_Company As String = "V-ERP (UAE)"
            'Public str_Company1 As String = "V-ERP (PAK)"
            'Public str_Company2 As String = "V-ERP (Remms-PAK)"
            'Public str_Company3 As String = "V-ERP (Remms-UAE)"
            'Public str_Company4 As String = "V-ERP (KSA)"
            'Public str_Company5 As String = "V-ERP (MY)"
            Return StandardNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub EditRecord(ByVal Obj As OpportunityBE)
        '       [OpportunityId] [int] IDENTITY(1,1) NOT NULL,
        '[DocNo] [nvarchar](50) NULL,
        '[DocDate] [datetime] NULL,
        '[CompanyId] [int] NULL,
        '[ContactId] [int] NULL,
        '[EndUserId] [int] NULL,
        '[OpportunityName] [nvarchar](500) NULL,
        '[TypeId] [int] NULL,
        '[CurrencyId] [int] NULL,
        '[OpportunityOwner] [nvarchar](250) NULL,
        '[CloseDate] [datetime] NULL,
        '[StageId] [int] NULL,
        '[LoosReasonId] [int] NULL,
        '[ProbabilityId] [int] NULL,
        '[ContactName] [nvarchar] (250) NULL,
        '[HardwareContact] [nvarchar](50) NULL,
        '[TaxAmount] [float] NULL,
        '[Duration] [nvarchar](50) NULL,
        '[StartDate] [datetime] NULL,
        '[PaymentId] [int] NULL,
        '[DeliveryId] [int] NULL,
        '[FrequencyId] [int] NULL,
        '[Freight] [float] NULL,
        '[ImplementationTime] [nvarchar](50) NULL,
        '[SupportTypeId] [int] NULL,
        '[MaterialLocation] [nvarchar](500) NULL,
        '[TargetPrice] [float] NULL,
        '[MaintenanceId] [int] NULL,
        '[UserName] [nvarchar](50) NULL,
        '[ModifiedUser] [nvarchar](50) NULL,
        '[ModifiedDate] [datetime] NULL,
        '[OpportunityType] [nvarchar](50) NULL,
        Try
            OpportunityId = Obj.OpportunityId
            Me.dtpDocDate.Value = Obj.DocDate
            Me.txtDocumentNo.Text = Obj.DocNo
            Me.cmbCompany.Value = Obj.CompanyId
            Me.cmbPerson.Value = Obj.ContactId
            'rafay 11-4-22
            Me.ChkBatteriesIncluded.Checked = Obj.ChkBoxBatteriesIncluded
            'rafay
            Me.cmbEndUser.Text = Obj.EndUserId
            Me.txtOpportunityName.Text = Obj.OpportunityName
            OpportunityName = Obj.OpportunityName
            Me.cmbType.Value = Obj.TypeId
            Me.cmbCurrency.Value = Obj.CurrencyId
            Me.txtOpportunityOwner.Text = Obj.OpportunityOwner
            Me.dtpCloseDate.Value = Obj.CloseDate
            Me.cmbStage.Value = Obj.StageId
            Me.cmbReasonforloosing.Value = Obj.LoosReasonId
            Me.cmbProbability.Value = Obj.ProbabilityId
            Me.txtContactName.Text = Obj.ContactName
            Me.txtLeadTime.Text = Obj.LeadTimeInDays
            txtFreight.Text = Obj.Freight
            Me.cmbPaymentHardware.Value = Obj.PaymentId
            Me.cmbPaymentSupport.Value = Obj.PaymentId
            Me.cmbCountry.Value = Obj.CountryId
            Me.cmbDeliveryInformation.Value = Obj.DeliveryId
            Me.txtSST.Text = Obj.TaxAmount
            Me.txtDurationInMonths.Text = Obj.Duration
            Me.dtpStartDate.Value = Obj.StartDate
            Me.cmbInvoiceFrequency.Value = Obj.FrequencyId
            Me.txtImplementationTime.Text = Obj.ImplementationTime
            Me.cmbSupportType.Value = Obj.SupportTypeId
            Me.txtMaterialLocation.Text = Obj.MaterialLocation
            Me.txtTargetPrice.Text = Obj.TargetPrice
            Me.cmbPreventionMaintenance.Value = Obj.MaintenanceId
            Me.cmbOnsiteIntervention.Text = Obj.OnsiteId
            Me.cmbDurationofMonth.Text = Obj.DurationofMonth
            Me.cmbInvoicePattern.Text = Obj.InvoicePattern
            Me.cmbItem.Value = Obj.ArticleId
            Me.cmbPMFrequency.Text = Obj.PMFrequency
            Me.UiListControl1.SelectItemsByIDs(Obj.CoverageWindow)
            Me.lstInterestedIn.SelectItemsByIDs(Obj.OnsiteIntervention)
            Me.lblTotalAmount.Text = Obj.TotalAmount
            TotalAttachments = Obj.NoOfAttachments
            Me.btnAttachments.Text = "Attachments (" & TotalAttachments & ") "
            If Obj.OpportunityType = "Support" Then
                GetSupportDetail(OpportunityId)
            Else
                GetHardwareDetail(OpportunityId)
            End If
            '' TASK TFS4867
            If Obj.EmployeeId > 0 Then
                EmployeeId = Obj.EmployeeId
                GetEmployee(EmployeeId)
            End If
            '' END TASK TFS4867
            'btnSave.Text = "Save"
            IsEditMode = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdHardware_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdHardware.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If Me.grdHardware.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                    If IsEditMode = True Then
                        'If DoHaveDeleteRights = True Then
                        Call New OpportunityHardwareDetailDAL().DeleteSingle(Val(Me.grdHardware.GetRow.Cells("OpportunityHardwareDetailId").Value.ToString))
                        Me.grdHardware.GetRow.Delete()
                        'Else
                        msg_Information("You do not have delete rights.")
                        'End If
                    Else
                        Me.grdHardware.GetRow.Delete()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSupport_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSupport.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If Me.grdSupport.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
                    If IsEditMode = True Then
                        ''If DoHaveDeleteRights = True Then
                        Call New OpportunitySupportDetailDAL().Delete(Val(Me.grdSupport.GetRow.Cells("OpportunitySupportDetailId").Value.ToString))
                        Me.grdHardware.GetRow.Delete()
                        ''Else
                        ''msg_Information("You do not have delete rights.")
                        ''End If
                    Else
                        Me.grdSupport.GetRow.Delete()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    '    With ExcelSheet
    '   For Each column As DataGridViewColumn In DataGridView1.Columns
    '      .cells( 1, column.Index + 1 ) = column.HeaderText
    '   Next 
    '   For i = 1 To Me.DataGridView1.RowCount
    '      .cells(i + 1, 1) = Me.DataGridView1.Rows(i - 1).Cells("id").Value
    '      For j = 1 To DataGridView1.Columns.Count - 1
    '         .cells(i + 1, j + 1) = DataGridView1.Rows(i - 1).Cells(j).Value
    '      Next
    '   Next
    'End With
    Private Sub ExportExcel()
        Dim xlApp As Microsoft.Office.Interop.Excel.Application
        Dim xlBook As Excel.Workbook
        Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim oValue As Object = System.Reflection.Missing.Value
        Dim sPath As String = String.Empty
        Dim dlgSave As New SaveFileDialog
        dlgSave.DefaultExt = "xls"
        dlgSave.Filter = "Microsoft Excel|*.xls"
        Dim app As Application
        dlgSave.InitialDirectory = Application.StartupPath
        If dlgSave.ShowDialog = DialogResult.OK Then
            Try
                xlApp = New Microsoft.Office.Interop.Excel.Application
                xlBook = xlApp.Workbooks.Add(oValue)
                xlSheet = xlBook.Worksheets("sheet1")
                Dim xlRow As Long = 2
                Dim xlCol As Short = 1
                For Each column As Janus.Windows.GridEX.GridEXColumn In grdHardware.RootTable.Columns
                    xlSheet.Cells(1, column.Index + 1) = column.Caption
                Next
                'For k As Integer = 0 To DataGridView1.ColumnCount - 1
                '    xlSheet.Cells(1, xlCol) = DataGridView1(k, 0).Value
                '    xlCol += 1
                'Next
                'For k As Integer = 0 To DataGridView1.ColumnCount - 1
                '    xlSheet.Cells(2, xlCol) = DataGridView1(k, 0).Value
                '    xlCol += 1
                'Next
                'For i As Integer = 0 To DataGridView1.RowCount - 1
                '    xlCol = 1

                '    For k As Integer = 0 To DataGridView1.ColumnCount - 1
                '        xlSheet.Cells(xlRow, xlCol) = DataGridView1(k, i).Value
                '        xlCol += 1

                '    Next

                '    xlRow += 1

                'Next

                xlSheet.Columns.AutoFit()
                Dim sFileName As String = Replace(dlgSave.FileName, ".xlsx", "xlx")
                xlSheet.SaveAs(sFileName)
                xlApp = Nothing
                xlBook = Nothing
                xlSheet = Nothing
                'xlBook.Close()
                'xlApp.Quit()
                'releaseObject(xlApp)
                'releaseObject(xlBook)
                'releaseObject(xlSheet)
                MsgBox("Data successfully exported.", MsgBoxStyle.Information, "PRMS/SOB Date Tagging")
            Catch
                MsgBox(ErrorToString)
            Finally
            End Try
        End If
        'rafay
        'Dim xlApp As Microsoft.Office.Interop.Excel.Application
        'Dim xlBook As Excel.Workbook
        'Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet
        'Dim oValue As Object = System.Reflection.Missing.Value
        'Dim sPath As String = String.Empty
        'Dim dlgSave As New SaveFileDialog
        'dlgSave.DefaultExt = "xls"
        'dlgSave.Filter = "Microsoft Excel|*.xls"
        'Dim app As Application
        '' Dim datagridview1 As Janus.Windows.GridEX.GridEX
        'dlgSave.InitialDirectory = Application.StartupPath
        'If dlgSave.ShowDialog = DialogResult.OK Then
        '    Try
        '        xlApp = New Microsoft.Office.Interop.Excel.Application
        '        xlBook = xlApp.Workbooks.Add(oValue)
        '        xlSheet = xlBook.Worksheets("sheet1")
        '        Dim xlRow As Long = 2
        '        Dim xlCol As Short = 1
        '        For Each column As Janus.Windows.GridEX.GridEXColumn In grdHardware.RootTable.Columns
        '            xlSheet.Cells(1, column.Index + 1) = column.Caption
        '        Next
        '        'For k As Integer = 0 To grdHardware.RootTable.Columns.Count - 1
        '        '    xlSheet.Cells(1, xlCol) = grdHardware.Row(1 - k).cells(k).Value
        '        '    xlCol += 1
        '        'Next
        '        'For k As Integer = 0 To DataGridView1.ColumnCount - 1
        '        '    xlSheet.Cells(2, xlCol) = DataGridView1(k, 0).Value
        '        '    xlCol += 1
        '        'Next
        '        'For i As Integer = 0 To DataGridView1.RowCount - 1
        '        '    xlCol = 1

        '        '    For k As Integer = 0 To DataGridView1.ColumnCount - 1
        '        '        xlSheet.Cells(xlRow, xlCol) = DataGridView1(k, i).Value
        '        '        xlCol += 1

        '        '    Next

        '        '    xlRow += 1

        '        'Next

        '        xlSheet.Columns.AutoFit()
        '        Dim sFileName As String = Replace(dlgSave.FileName, ".xlsx", "xlx")
        '        xlSheet.SaveAs(sFileName)
        '        xlApp = Nothing
        '        xlBook = Nothing
        '        xlSheet = Nothing
        '        'xlBook.Close()
        '        'xlApp.Quit()
        '        'releaseObject(xlApp)
        '        'releaseObject(xlBook)
        '        'releaseObject(xlSheet)
        '        MsgBox("Data successfully exported.", MsgBoxStyle.Information, "PRMS/SOB Date Tagging") '"PRMS/SOB Date Tagging"
        '    Catch
        '        MsgBox(ErrorToString)
        '    Finally
        '    End Try
        'End If
    End Sub

    Private Sub cmbUploadBOQ_Click(sender As Object, e As EventArgs) Handles cmbUploadBOQ.Click
        Try
            ImportExcelToSupport()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MakeExcelHardwareHeaders()
        'rafay:export the data in excel (perffect)
        Dim ExcelApp As Microsoft.Office.Interop.Excel.Application
        Dim ExcelWorkBk As Microsoft.Office.Interop.Excel.Workbook
        Dim ExcelWorkSht As Microsoft.Office.Interop.Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim i As Integer
        Dim j As Integer
        Dim sPath As String = String.Empty
        Dim dlgSave As New SaveFileDialog
        dlgSave.DefaultExt = "xls"
        dlgSave.Filter = "All Files (*.*)|*.*|Excel files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv|XLS Files (*.xls)|*xls"
        'Dim app As Application
        dlgSave.InitialDirectory = My.Computer.FileSystem.Drives.Item(1).Name.ToString
        Try
            If dlgSave.ShowDialog = DialogResult.OK Then
                ExcelApp = New Microsoft.Office.Interop.Excel.Application
                ExcelWorkBk = ExcelApp.Workbooks.Add(misValue)
                ExcelWorkSht = ExcelWorkBk.Sheets("Sheet1")
                ' ExcelWorkSht = ExcelWorkBk.Sheets.Add("FormulaSheet")
                'rafay:Adding total amount formula in excel
                'ExcelWorkSht.Range("K2").Formula = "=H1*I1-J1"
                ' ExcelWorkSht.Cells("K1").calculate()
                'ExcelWorkBk.CalculateMode = CalculateMode.Auto
                'ExcelWorkBk.CalculationOnSave = True
                'Export Header Names Start
                Dim columnsCount As Integer = grdHardware.RootTable.Columns.Count
                ''  For Each column As Janus.Windows.GridEX.GridEXColumn In grdHardware.RootTable.Columns
                ''ExcelWorkSht.Cells(1, column.Index + 1) = column.Caption
                '' Next
                'Export the data from the datagrid to an excel spreadsheet
                ''For i = 0 To grdHardware.RowCount - 1
                '  For j = 0 To grdHardware.RootTable.Columns.Count - 1
                Dim counter As Integer = 1
                For k As Integer = 1 To grdHardware.RootTable.Columns.Count

                    If grdHardware.RootTable.Columns(k - 1).Key = "OpportunityHardwareDetailId" Or grdHardware.RootTable.Columns(k - 1).Key = "OpportunityId" Or grdHardware.RootTable.Columns(k - 1).Key = "Delete" Or grdHardware.RootTable.Columns(k - 1).Key = "FilePath" Then
                        '' k -= 1
                    Else

                        'ExcelWorkSht.Cells(1, k) = grdHardware.RootTable.Columns(k - 1).Key
                        ExcelWorkSht.Cells(1, counter) = grdHardware.RootTable.Columns(k - 1).Key
                        'ExcelWorkSht.Cells(i + 1, j + 1) = grdHardware(j, i).Value.ToString()
                        ''ExcelWorkSht.Cells(i + 1, j + 1) = grdHardware.GetRows(i).Cells(j).Value.ToString
                        '' ExcelWorkSht.Cells(i + 1, j + 1) = grdHardware.GetRows(i + 1).Cells(j).Value.ToString
                        counter += 1
                    End If
                Next
                'Next
                'Next
                Dim sFileName As String = Replace(dlgSave.FileName, ".xlsx", ".xlsx")
                ExcelWorkSht.SaveAs(sFileName)
                With ExcelApp
                    'Rafay:Adding total amount formula in excel
                    'ExcelWorkSht.Cell("K1").Formula = "PRODUCT(H1:I1)"
                    .Visible = True
                    'Rafay:this will show excel file bring into front when download button is click
                    .Columns.AutoFit()
                    .Rows("1:1").Font.FontStyle = "Bold"
                    .Rows("1:1").Font.Size = 11
                    '.Range("K2").Select()
                    .Cells.Select()
                    .Cells.EntireColumn.AutoFit()
                    .Cells(1, 1).Select()
                End With
            End If

            msg_Information("Grid headers have been created successfully to Excel")
        Catch ex As Exception
            Throw ex
        End Try
        'rafay: Export the data into excel sheet 
        'Dim ExcelApp As Microsoft.Office.Interop.Excel.Application
        'Dim myDailog As New System.Windows.Forms.SaveFileDialog()
        'myDailog.AddExtension = True
        'myDailog.DefaultExt = ".xls"
        'myDailog.Filter = "Excel Files|*.xls"

        'If (myDailog.ShowDialog = DialogResult.OK) Then
        '    Dim strFileName As String
        '    strFileName = myDailog.FileName
        '    If strFileName.Length > 0 Then

        '        Dim fs As New IO.FileStream(strFileName, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
        '        Me.GridEXExporter1.GridEX = grdHardware
        '        Me.GridEXExporter1.Export(fs)
        '        fs.Flush()
        '        fs.Close()
        '        fs.Dispose()
        '        'With ExcelApp
        '        '    .Visible = True
        '        '    .Columns.AutoFit()
        '        '    .Rows("1:1").Font.FontStyle = "Bold"
        '        '    .Rows("1:1").Font.Size = 11
        '        '    .Cells.Select()
        '        '    .Cells.EntireColumn.AutoFit()
        '        '    .Cells(1, 1).Select()
        '        'End With

        '        msg_Information("Exported successfully")


        '    End If
        'End If
        '  rafay perfect
        'Dim ExcelApp As Microsoft.Office.Interop.Excel.Application
        'Dim ExcelWorkBk As Microsoft.Office.Interop.Excel.Workbook
        'Dim ExcelWorkSht As Microsoft.Office.Interop.Excel.Worksheet
        'Dim i As Integer
        'Dim j As Integer
        'Dim sPath As String = String.Empty
        'Dim dlgSave As New SaveFileDialog
        'dlgSave.DefaultExt = "xls"
        'dlgSave.Filter = "Microsoft Excel|*.xls"
        'Dim app As Application
        'dlgSave.InitialDirectory = My.Computer.FileSystem.Drives.Item(1).Name.ToString
        'Try
        '    If dlgSave.ShowDialog = DialogResult.OK Then
        '        ExcelApp = New Microsoft.Office.Interop.Excel.Application
        '        ExcelWorkBk = ExcelApp.Workbooks.Add()
        '        ExcelWorkSht = ExcelWorkBk.Sheets("Sheet1")
        '        'Export Header Names Start
        '        Dim columnsCount As Integer = grdHardware.RootTable.Columns.Count
        '        'For Each column In dgvOrderNonShipped.Columns
        '        'ExcelWorkSht.Cells(1, column.Index + 1) = column.HeaderText
        '        'Next
        '        'Export the data from the datagrid to an excel spreadsheet
        '        'For i = 0 To dgvOrderNonShipped.RowCount - 1
        '        '    For j = 0 To dgvOrderNonShipped.ColumnCount - 1
        '        Dim counter As Integer = 1
        '        For k As Integer = 1 To grdHardware.RootTable.Columns.Count
        '            If grdHardware.RootTable.Columns(k - 1).Key = "OpportunityHardwareDetailId" Or grdHardware.RootTable.Columns(k - 1).Key = "OpportunityId" Or grdHardware.RootTable.Columns(k - 1).Key = "Delete" Or grdHardware.RootTable.Columns(k - 1).Key = "FilePath" Then
        '                'k -= 1
        '            Else
        '                'ExcelWorkSht.Cells(1, k) = grdHardware.RootTable.Columns(k - 1).Key
        '                ExcelWorkSht.Cells(1, counter) = grdHardware.RootTable.Columns(k - 1).Key
        '                'ExcelWorkSht.Cells(i + 1, j + 1) = dgvOrderNonShipped(j, i).Value
        '                counter += 1
        '            End If
        '        Next
        '        '    Next
        '        'Next
        '        Dim sFileName As String = Replace(dlgSave.FileName, ".xlsx", "xlx")
        '        ExcelWorkSht.SaveAs(sFileName)
        '        'ExcelWorkSht.Visible = False
        '        With ExcelApp
        '            .Visible = True
        '            .Columns.AutoFit()
        '            .Rows("1:1").Font.FontStyle = "Bold"
        '            .Rows("1:1").Font.Size = 11
        '            .Cells.Select()
        '            .Cells.EntireColumn.AutoFit()
        '            .Cells(1, 1).Select()
        '        End With
        '    End If

        '    MsgBox("Grid headers have been created successfully to Excel")
        'Catch ex As Exception
        '    Throw ex
        'End Try 
        'rafay
        'Dim ExcelApp As Microsoft.Office.Interop.Excel.Application
        'Dim ExcelWorkBk As Microsoft.Office.Interop.Excel.Workbook
        'Dim ExcelWorkSht As Microsoft.Office.Interop.Excel.Worksheet
        'Dim i As Integer
        'Dim j As Integer
        'Dim sPath As String = String.Empty
        'Dim dlgSave As New SaveFileDialog
        'dlgSave.DefaultExt = "xls"
        'dlgSave.Filter = "Microsoft Excel|*.xls"
        'Dim app As Application
        'dlgSave.InitialDirectory = My.Computer.FileSystem.Drives.Item(1).Name.ToString
        'Try
        '    If dlgSave.ShowDialog = DialogResult.OK Then
        '        ExcelApp = New Microsoft.Office.Interop.Excel.Application
        '        ExcelWorkBk = ExcelApp.Workbooks.Add()
        '        ExcelWorkSht = ExcelWorkBk.Sheets("Sheet1")
        '        'Export Header Names Start
        '        Dim columnsCount As Integer = grdHardware.RootTable.Columns.Count
        '        'For Each column In dgvOrderNonShipped.Columns
        '        'ExcelWorkSht.Cells(1, column.Index + 1) = column.HeaderText
        '        'Next
        '        'Export the data from the datagrid to an excel spreadsheet
        '        'For i = 0 To dgvOrderNonShipped.RowCount - 1
        '        '    For j = 0 To dgvOrderNonShipped.ColumnCount - 1
        '        Dim counter As Integer = 1
        '        For k As Integer = 1 To grdHardware.RootTable.Columns.Count
        '            If grdHardware.RootTable.Columns(k - 1).Key = "OpportunityHardwareDetailId" Or grdHardware.RootTable.Columns(k - 1).Key = "OpportunityId" Or grdHardware.RootTable.Columns(k - 1).Key = "Delete" Or grdHardware.RootTable.Columns(k - 1).Key = "FilePath" Then
        '                'k -= 1
        '            Else
        '                'ExcelWorkSht.Cells(1, k) = grdHardware.RootTable.Columns(k - 1).Key
        '                ExcelWorkSht.Cells(1, counter) = grdHardware.RootTable.Columns(k - 1).Key
        '                'ExcelWorkSht.Cells(i + 1, j + 1) = dgvOrderNonShipped(j, i).Value
        '                counter += 1
        '            End If
        '        Next
        '        '    Next
        '        'Next
        '        Dim sFileName As String = Replace(dlgSave.FileName, ".xlsx", "xlx")
        '        ExcelWorkSht.SaveAs(sFileName)
        '        'ExcelWorkSht.Visible = False
        '        With ExcelApp
        '            .Visible = True
        '            .Columns.AutoFit()
        '            .Rows("1:1").Font.FontStyle = "Bold"
        '            .Rows("1:1").Font.Size = 11
        '            .Cells.Select()
        '            .Cells.EntireColumn.AutoFit()
        '            .Cells(1, 1).Select()
        '        End With
        '    End If

        '    MsgBox("Grid headers have been created successfully to Excel")
        'Catch ex As Exception
        '    Throw ex
        'End Try
        'rafay update
        'Dim xlApp As Microsoft.Office.Interop.Excel.Application
        'Dim xlBook As Excel.Workbook
        'Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet
        'Dim oValue As Object = System.Reflection.Missing.Value
        'Dim sPath As String = String.Empty
        'Dim dlgSave As New SaveFileDialog
        ''Dim Row As Janus.Windows.GridEX.GridEXRow
        'dlgSave.DefaultExt = "xls"
        'dlgSave.Filter = "Microsoft Excel|*.xls"
        'Dim app As Application
        'dlgSave.InitialDirectory = app.StartupPath
        'If dlgSave.ShowDialog = DialogResult.OK Then
        '    Try
        '        xlApp = New Microsoft.Office.Interop.Excel.Application
        '        xlBook = xlApp.Workbooks.Add(oValue)
        '        xlSheet = xlBook.Worksheets("sheet1")
        '        Dim xlRow As Long = 2
        '        Dim xlCol As Short = 1
        '        For Each column As Janus.Windows.GridEX.GridEXColumn In grdHardware.RootTable.Columns
        '            xlSheet.Cells(1, column.Index + 1) = column.Caption
        '        Next
        '        For k As Integer = 0 To grdHardware.RootTable.Columns.Count - 1
        '            '               xlSheet.Cells(1, xlCol) = grdHardware.RootTable.Columns.Caption(k, 0).Value
        '            xlCol += 1
        '        Next
        '        For k As Integer = 0 To grdHardware.RootTable.Columns.Count - 1
        '            '        xlSheet.Cells(2, xlCol) = grdHardware.Row(k, 0).Value
        '            xlCol += 1
        '        Next
        '        For i As Integer = 0 To grdHardware.RowCount -1
        '            xlCol = 1

        '            For k As Integer = 0 To grdHardware.RootTable.Columns.Count - 1
        '                '                 xlSheet.Cells(xlRow, xlCol) = grdHardware.Row(k, i).Value.ToString
        '                xlCol += 1

        '            Next

        '            xlRow += 1

        '        Next

        '        xlSheet.Columns.AutoFit()
        '        Dim sFileName As String = Replace(dlgSave.FileName, ".xlsx", "xlx")
        '        xlSheet.SaveAs(sFileName)
        '        xlApp = Nothing
        '        xlBook = Nothing
        '        xlSheet = Nothing
        '        'xlBook.Close()
        '        'xlApp.Quit()
        '        'releaseObject(xlApp)
        '        'releaseObject(xlBook)
        '        'releaseObject(xlSheet)
        '        MsgBox("Data successfully exported.")
        '    Catch
        '        MsgBox(ErrorToString)
        '    Finally
        '    End Try
        'End If
        'rafay
        'Dim ExcelApp As Microsoft.Office.Interop.Excel.Application
        'Dim ExcelWorkBk As Microsoft.Office.Interop.Excel.Workbook
        'Dim ExcelWorkSht As Microsoft.Office.Interop.Excel.Worksheet
        'Dim i As Integer
        'Dim j As Integer
        'Dim sPath As String = String.Empty
        'Dim dlgSave As New SaveFileDialog
        'dlgSave.DefaultExt = "xls"
        'dlgSave.Filter = "Microsoft Excel|*.xls"
        'Dim app As Application
        'dlgSave.InitialDirectory = My.Computer.FileSystem.Drives.Item(1).Name.ToString
        'Try
        '    If dlgSave.ShowDialog = DialogResult.OK Then
        '        ExcelApp = New Microsoft.Office.Interop.Excel.Application
        '        ExcelWorkBk = ExcelApp.Workbooks.Add()
        '        ExcelWorkSht = ExcelWorkBk.Sheets("Sheet1")
        '        'Export Header Names Start
        '        Dim columnsCount As Integer = grdHardware.RootTable.Columns.Count
        '        '    For Each column In dgvOrderNonShipped.Columns
        '        'ExcelWorkSht.Cells(1, column.Index + 1) = column.HeaderText
        '        'Next
        '        'Export the data from the datagrid to an excel spreadsheet
        '        For i = 0 To grdHardware.RowCount - 1
        '            For j = 0 To grdHardware.RootTable.Columns.Count - 1
        '                Dim counter As Integer = 1
        '                For k As Integer = 1 To grdHardware.RootTable.Columns.Count
        '                    If grdHardware.RootTable.Columns(k - 1).Key = "OpportunityHardwareDetailId" Or grdHardware.RootTable.Columns(k - 1).Key = "OpportunityId" Or grdHardware.RootTable.Columns(k - 1).Key = "Delete" Or grdHardware.RootTable.Columns(k - 1).Key = "FilePath" Then
        '                        'k -= 1
        '                    Else
        '                        '  ExcelWorkSht.Cells(1, k) = grdHardware.RootTable.Columns(k - 1).Key
        '                        ExcelWorkSht.Cells(1, counter) = grdHardware.RootTable.Columns(k - 1).Key
        '                        'ExcelWorkSht.Cells(i + 1, j + 1) = dgvOrderNonShipped(j, i).Value
        '                        counter += 1
        '                    End If
        '                Next
        '            Next
        '        Next
        '        Dim sFileName As String = Replace(dlgSave.FileName, ".xlsx", "xlx")
        '        ExcelWorkSht.SaveAs(sFileName)
        '        ExcelWorkSht.Visible = False
        '        With ExcelApp
        '            .Visible = True
        '            .Columns.AutoFit()
        '            .Rows("1:1").Font.FontStyle = "Bold"
        '            .Rows("1:1").Font.Size = 11
        '            .Cells.Select()
        '            .Cells.EntireColumn.AutoFit()
        '            .Cells(1, 1).Select()
        '        End With
        '    End If

        '    MsgBox("Grid headers have been created successfully to Excel")
        'Catch ex As Exception
        '    Throw ex
        'End Try

    End Sub
    Private Sub MakeExcelSupportHeaders()
        ''rafay: Export the data into excel sheet 
        'Dim myDailog As New System.Windows.Forms.SaveFileDialog()
        'myDailog.AddExtension = True
        'myDailog.DefaultExt = ".xls"
        'myDailog.Filter = "Excel Files|*.xls"

        'If (myDailog.ShowDialog = DialogResult.OK) Then
        '    Dim strFileName As String
        '    strFileName = myDailog.FileName
        '    If strFileName.Length > 0 Then

        '        Dim fs As New IO.FileStream(strFileName, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
        '        Me.GridEXExporter1.GridEX = grdSupport
        '        Me.GridEXExporter1.Export(fs)

        '        fs.Flush()
        '        fs.Close()
        '        fs.Dispose()

        '        msg_Information("Exported successfully")

        '    End If
        'End If
        'rafay perfect
        Dim ExcelApp As Microsoft.Office.Interop.Excel.Application
        Dim ExcelWorkBk As Microsoft.Office.Interop.Excel.Workbook
        Dim ExcelWorkSht As Microsoft.Office.Interop.Excel.Worksheet
        Dim i As Integer
        Dim j As Integer
        Dim sPath As String = String.Empty
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim dlgSave As New SaveFileDialog
        dlgSave.DefaultExt = "xls"
        dlgSave.Filter = "Microsoft Excel|*.xls"
        'Dim app As Application
        dlgSave.InitialDirectory = My.Computer.FileSystem.Drives.Item(1).Name.ToString
        Try
            If dlgSave.ShowDialog = DialogResult.OK Then
                ExcelApp = New Microsoft.Office.Interop.Excel.Application
                ExcelWorkBk = ExcelApp.Workbooks.Add(misValue)
                ExcelWorkSht = ExcelWorkBk.Sheets("Sheet1")
                'rafay:column SLACoverage is fixed in excel as text
                ExcelWorkSht.Range("D:D").NumberFormat = "@"
                'Export Header Names Start
                Dim columnsCount As Integer = grdSupport.RootTable.Columns.Count
                'For Each column In dgvOrderNonShipped.Columns
                'ExcelWorkSht.Cells(1, column.Index + 1) = column.HeaderText
                'Next
                'Export the data from the datagrid to an excel spreadsheet
                'For i = 0 To dgvOrderNonShipped.RowCount - 1
                '    For j = 0 To dgvOrderNonShipped.ColumnCount - 1
                Dim counter As Integer = 1
                For k As Integer = 1 To grdSupport.RootTable.Columns.Count
                    If grdSupport.RootTable.Columns(k - 1).Key = "OpportunitySupportDetailId" Or grdSupport.RootTable.Columns(k - 1).Key = "OpportunityId" Or grdSupport.RootTable.Columns(k - 1).Key = "Delete" Or grdSupport.RootTable.Columns(k - 1).Key = "FilePath" Then
                        'k -= 1
                    Else

                        'ExcelWorkSht.Cells(1, k) = grdHardware.RootTable.Columns(k - 1).Key
                        ExcelWorkSht.Cells(1, counter) = grdSupport.RootTable.Columns(k - 1).Key
                        'ExcelWorkSht.Cells(i + 1, j + 1) = dgvOrderNonShipped(j, i).Value
                        'ExcelWorkSht.Cells(i + 1, j + 1) = grdSupport.GetRows(i + 1).Cells(j).Value.ToString
                        counter += 1
                    End If
                Next
                '    Next
                'Next

                Dim sFileName As String = Replace(dlgSave.FileName, ".xlsx", ".xlx")
                ExcelWorkSht.SaveAs(sFileName)
                With ExcelApp
                    .Visible = True
                    'rafay:this will show excel file bring to frront when download button is click 
                    .Columns.AutoFit()
                    .Rows("1:1").Font.FontStyle = "Bold"
                    .Rows("1:1").Font.Size = 11
                    .Cells.Select()
                    .Cells.EntireColumn.AutoFit()
                    .Cells(1, 1).Select()
                End With
            End If
            msg_Information("Grid headers have been created successfully to Excel")
        Catch ex As Exception
            Throw ex
        End Try

        'rafay gridview to excel through exporter
        'Using sfd = New SaveFileDialog()
        '    sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        '    sfd.AddExtension = True
        '    sfd.Filter = "Excel file (*.xls, *.xlsx)|*.xls;*.xlsx"
        '    '; If DialogResult.OK = sfd.ShowDialog() Then
        '    If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
        '        Dim st As New IO.FileStream(SaveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.None)
        '        Me.GridEXExporter1.Export(Me.grdSupport, sfd.Filename)
        '        If DialogResult.Yes = MessageBox.Show("Do you want to open the file", "Excel", MessageBoxButtons.YesNo) Then
        '            System.Diagnostics.Process.Start(sfd.FileName)
        '        End If
        '    End If
        'End Using

        ''rafayyyyy
        'Dim ExcelApp As Microsoft.Office.Interop.Excel.Application
        'Dim ExcelWorkBk As Microsoft.Office.Interop.Excel.Workbook
        'Dim ExcelWorkSht As Microsoft.Office.Interop.Excel.Worksheet
        'Dim misvalue As Object = System.Reflection.Missing.Value
        'Dim i As Integer
        ''   Dim j As Integer
        'Dim sPath As String = String.Empty
        'Dim dlgSave As New SaveFileDialog
        'dlgSave.DefaultExt = "xls"
        'dlgSave.Filter = "Microsoft Excel|*.xls"
        ''rafay Dim app As Application
        'dlgSave.InitialDirectory = My.Computer.FileSystem.Drives.Item(1).Name.ToString
        'Try
        '    If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSupport.Name) Then
        '        Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSupport.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
        '        Me.grdSupport.LoadLayoutFile(fs)
        '    End If
        '    If dlgSave.ShowDialog = DialogResult.OK Then
        '        ExcelApp = New Microsoft.Office.Interop.Excel.Application
        '        ExcelWorkBk = ExcelApp.Workbooks.Add(misvalue)
        '        ExcelWorkSht = ExcelWorkBk.Sheets("Sheet1")
        '        'rafay Export Header Names Start
        '        '  Dim columnsCount As Integer = grdSupport.RootTable.Columns.Count
        '        '    For Each column As Janus.Windows.GridEX.GridEXColumn In grdSupport.RootTable.Columns
        '        'ExcelWorkSht.Cells(1, column.Index + 1) = column.Caption  'missing header text
        '        ' Next
        '        'rafay Export the data from the datagrid to an excel spreadsheet
        '        For i = 0 To grdSupport.RowCount - 1
        '        Next
        '        Dim sFileName As String = Replace(dlgSave.FileName, ".xlsx", "xlx")
        '        ExcelWorkSht.SaveAs(sFileName)
        '        With ExcelApp
        '            .Visible = True
        '            .Columns.AutoFit()
        '            .Rows("1:1").Font.FontStyle = "Bold"
        '            .Rows("1:1").Font.Size = 11
        '            .Cells.Select()
        '            .Cells.EntireColumn.AutoFit()
        '            .Cells(1, 1).Select()

        '            For j As Integer = 0 To grdSupport.RootTable.Columns.Count - 1
        '                Dim counter As Integer = 1
        '                For k As Integer = 1 To grdSupport.RootTable.Columns.Count
        '                    If grdSupport.RootTable.Columns(k - 1).Key = "OpportunitySupportDetailId" Or grdSupport.RootTable.Columns(k - 1).Key = "OpportunityId" Or grdSupport.RootTable.Columns(k - 1).Key = "Delete" Or grdSupport.RootTable.Columns(k - 1).Key = "FilePath" Then
        '                        'k -= 1
        '                        'Else

        '                        'ExcelWorkSht.Cells(1, k) = grdHardware.RootTable.Columns(k - 1).Key
        '                        ExcelWorkSht.Cells(1, k) = grdSupport.RootTable.Columns(k - 1).Caption

        '                        ExcelWorkSht.Cells(i + 2, j + 1) = grdSupport.GetRows(i + 1).Cells(j).Value.ToString

        '                        counter += 1
        '                    End If
        '                Next
        '            Next
        '            'Next
        '            'Dim sFileName As String = Replace(dlgSave.FileName, ".xlsx", "xlx")
        '            'ExcelWorkSht.SaveAs(sFileName)
        '            'With ExcelApp
        '            '    .Visible = True
        '            '    .Columns.AutoFit()
        '            '    .Rows("1:1").Font.FontStyle = "Bold"
        '            '    .Rows("1:1").Font.Size = 11
        '            '    .Cells.Select()
        '            '    .Cells.EntireColumn.AutoFit()
        '            '    .Cells(1, 1).Select()
        '        End With
        '    End If
        '    msg_Information("Grid headers have been created successfully to Excel")
        'Catch ex As Exception
        '    Throw ex
        'End Try

        'rafay
        'Dim xlApp As Microsoft.Office.Interop.Excel.Application
        'Dim xlBook As Excel.Workbook
        'Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet
        'Dim oValue As Object = System.Reflection.Missing.Value
        'Dim sPath As String = String.Empty
        'Dim k As Integer
        'Dim counter As Integer = 0
        'Dim dlgSave As New SaveFileDialog
        'dlgSave.DefaultExt = "xls"
        'dlgSave.Filter = "Microsoft Excel|*.xls"
        'Dim app As Application
        'dlgSave.InitialDirectory = app.StartupPath
        'If dlgSave.ShowDialog = DialogResult.OK Then
        '    Try
        '        xlApp = New Microsoft.Office.Interop.Excel.Application
        '        xlBook = xlApp.Workbooks.Add(oValue)
        '        xlSheet = xlBook.Worksheets("sheet1")
        '        If grdSupport.RootTable.Columns(k - 1).Key = "OpportunitySupportDetailId" Or grdSupport.RootTable.Columns(k - 1).Key = "OpportunityId" Or grdSupport.RootTable.Columns(k - 1).Key = "Delete" Or grdSupport.RootTable.Columns(k - 1).Key = "FilePath" Then
        '            'k -= 1
        '        Else

        '            'ExcelWorkSht.Cells(1, k) = grdHardware.RootTable.Columns(k - 1).Key
        '            xlSheet.Cells(1, counter) = grdSupport.RootTable.Columns(k - 1).Key
        '            'ExcelWorkSht.Cells(i + 1, j + 1) = dgvOrderNonShipped(j, i).Value
        '            counter += 1
        '        End If
        '        Dim xlRow As Long = 2
        '        Dim xlCol As Short = 1
        '        For Each column As Janus.Windows.GridEX.GridEXColumn In grdSupport.RootTable.Columns
        '            xlSheet.Cells(1, column.Index + 1) = column.Caption
        '        Next
        '        'For k As Integer = 0 To DataGridView1.ColumnCount - 1
        '        '    xlSheet.Cells(1, xlCol) = DataGridView1(k, 0).Value
        '        '    xlCol += 1
        '        'Next
        '        'For k As Integer = 0 To DataGridView1.ColumnCount - 1
        '        '    xlSheet.Cells(2, xlCol) = DataGridView1(k, 0).Value
        '        '    xlCol += 1
        '        'Next
        '        'For i As Integer = 0 To DataGridView1.RowCount - 1
        '        '    xlCol = 1

        '        '    For k As Integer = 0 To DataGridView1.ColumnCount - 1
        '        '        xlSheet.Cells(xlRow, xlCol) = DataGridView1(k, i).Value
        '        '        xlCol += 1

        '        '    Next

        '        '    xlRow += 1

        '        'Next

        '        xlSheet.Columns.AutoFit()
        '        Dim sFileName As String = Replace(dlgSave.FileName, ".xlsx", "xlx")
        '        xlSheet.SaveAs(sFileName)
        '        xlApp = Nothing
        '        xlBook = Nothing
        '        xlSheet = Nothing
        '        'xlBook.Close()
        '        'xlApp.Quit()
        '        'releaseObject(xlApp)
        '        'releaseObject(xlBook)
        '        'releaseObject(xlSheet)
        '        MsgBox("Data successfully exported.", MsgBoxStyle.Information, "PRMS/SOB Date Tagging")
        '    Catch
        '        MsgBox(ErrorToString)
        '    Finally
        '    End Try
        'End If


    End Sub

    Private Sub btnUploadHardwareBOQ_Click(sender As Object, e As EventArgs) Handles btnUploadHardwareBOQ.Click
        Try
            ImportExcelToHardware()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ImportExcelToHardware()
        Try
            'rafay   Dim cmd As OleDbCommand
            Dim dts As DataSet
            Dim excel As String
            Dim OpenFileDialog As New OpenFileDialog
            OpenFileDialog.InitialDirectory = My.Computer.FileSystem.Drives.Item(1).Name.ToString
            OpenFileDialog.Filter = "All Files (*.*)|*.*|Excel files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv|XLS Files (*.xls)|*xls"
            If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
                Dim fi As New FileInfo(OpenFileDialog.FileName)
                Dim FileName As String = OpenFileDialog.FileName
                excel = fi.FullName
                ' rafay Dim FilePath As String = OpenFileDialog.InitialDirectory
                Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel + ";Extended Properties=Excel 12.0;")
                'rafay :Add the column name 
                Dim dta As New OleDbDataAdapter("Select 0 AS OpportunityHardwareDetailId, 0 AS OpportunityId, PartNo, Type, BrandNo, Description, Status, Warranty,LeadTime, Qty, Price, Discount, TotalAmount, '" & excel & "' As FilePath From [Sheet1$]", conn)
                dts = New DataSet
                Try
                    dta.Fill(dts, "[Sheet1$]")
                    'rafay:to show excel data into data grid view
                    grdHardware.DataSource = dts.Tables(0)
                    grdHardware.DataMember = "[Sheet1$]"
                Catch
                    msg_Error("You chose a wrong file.")
                    conn.Close()
                    Exit Sub
                End Try
                For Each Path As Janus.Windows.GridEX.GridEXRow In Me.grdHardware.GetRows
                    If Path.Cells("FilePath").Value.ToString.Contains(excel) Then
                        msg_Error("This file has already been loaded.")
                        Exit Sub
                    End If
                Next
                Dim dtMerge As DataTable = CType(Me.grdHardware.DataSource, DataTable)
                dtMerge.Merge(dts.Tables(0))
                dtMerge.AcceptChanges()
                'DataGridView1.DataMember = "[Sheet1$]"
                conn.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ImportExcelToSupport()
        Try
            'rafay Dim cmd As OleDbCommand
            Dim dts As DataSet
            Dim excel As String
            Dim OpenFileDialog As New OpenFileDialog
            OpenFileDialog.InitialDirectory = My.Computer.FileSystem.Drives.Item(1).Name.ToString
            OpenFileDialog.Filter = "All Files (*.*)|*.*|Excel files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv|XLS Files (*.xls)|*xls"
            If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
                Dim fi As New FileInfo(OpenFileDialog.FileName)
                Dim FileName As String = OpenFileDialog.FileName
                excel = fi.FullName
                'Dim FilePath As String = OpenFileDialog.InitialDirectory
                Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel + "; Extended Properties=Excel 12.0;")
                Dim dta As New OleDbDataAdapter("Select 0 AS OpportunitySupportDetailId, 0 AS OpportunityId,  Brand, ModelNo, SerialNo, SLACoverage,SLAInterventionTime,SLAFixTime, Address, City, Province, Country, StartDate, EndDate, Type, UnitPrice, '" & excel & "' As FilePath From [Sheet1$]", conn) '' 0 AS OpportunitySupportDetailId, 0 AS OpportunityId,
                dts = New DataSet
                Try
                    dta.Fill(dts, "[Sheet1$]")
                    'rafay : to show excel data into datagrid view 
                    grdSupport.DataSource = dts.Tables(0)
                    grdSupport.DataMember = "[Sheet1$]"
                Catch
                    msg_Error("You chose a wrong file.")
                    conn.Close()
                    Exit Sub
                End Try
                For Each Path As Janus.Windows.GridEX.GridEXRow In Me.grdSupport.GetRows
                    If Path.Cells("FilePath").Value.ToString.Contains(excel) Then
                        msg_Error("This file has already been loaded.")
                        Exit Sub
                    End If
                Next
                Dim dtMerge As DataTable = CType(Me.grdSupport.DataSource, DataTable)
                dtMerge.Merge(dts.Tables(0))
                dtMerge.AcceptChanges()
                'grdSupport.DataSource = dts.Tables(0)
                'DataGridView1.DataMember = "[Sheet1$]"
                conn.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnDownloadBOQ1_Click(sender As Object, e As EventArgs) Handles btnDownloadBOQ1.Click
        Try
            MakeExcelSupportHeaders()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDownloadBOQ_Click(sender As Object, e As EventArgs) Handles btnDownloadBOQ.Click
        Try
            MakeExcelHardwareHeaders()
            'rafay ExportExcel()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCompany_RowSelected(sender As Object, e As Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbCompany.RowSelected
        Try
            If Me.cmbCompany.ActiveRow IsNot Nothing Then
                FillCombos("ContactPerson")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() Then
                If IsEditMode = False Then
                    Save()
                    If cmbStage.Value = 2 Then
                        SendAutoEmailSupport("Support")
                    End If
                    ReSetControls()
                    msg_Information("Record has been Saved successfully.")
                    'Me.Close()
                Else
                    If msg_Confirm("Do you want to update record?") = False Then Exit Sub
                    Update1()
                    If cmbStage.Value = 2 Then
                        SendAutoEmailSupport("Support")
                    End If
                    ReSetControls()
                    msg_Information("Record has been Updated successfully.")
                    'Me.Close()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SendAutoEmail(Optional ByVal Activity As String = "")
        Try
            GetTemplate("Opportunity Hardware")
            If EmailTemplate.Length > 0 Then
                GetAutoEmailData()
                UsersEmail = New List(Of String)
                'Added by murtaza (01/05/2023) for Remms email
                If Con.Database.Contains("Remms") Then
                    UsersEmail.Add("purchase@remmsit.com")
                Else
                    UsersEmail.Add("purchase@agriusit.com")
                End If
                'Added by murtaza (01/05/2023) for Remms email

                ''UsersEmail.Add("a.rafay@agriusit.com")

                'FormatStringBuilderMaster(dtEmailMaster)
                FormatStringBuilder(dtEmail)
                For Each _email As String In UsersEmail
                    CreateOutLookMail(_email)
                    SaveEmailLog(OpportunityNo, _email, "frmopportunity", Activity)
                Next
            Else
                ShowErrorMessage("No email template is found for Opportunity Hardware.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SendAutoEmailSupport(Optional ByVal Activity As String = "")
        Try
            GetTemplateSupport("Opportunity Support")
            If EmailTemplate.Length > 0 Then
                GetAutoEmailDataSupport()
                UsersEmail = New List(Of String)
                UsersEmail = New List(Of String)
                'Added by murtaza (01/05/2023) for Remms email
                If Con.Database.Contains("Remms") Then
                    UsersEmail.Add("purchase@remmsit.com")
                Else
                    UsersEmail.Add("purchase@agriusit.com")
                End If
                'Added by murtaza (01/05/2023) for Remms email


                FormatStringBuilder(dtEmail)
                For Each _email As String In UsersEmail
                    CreateOutLookMail(_email)
                    SaveEmailLog(OpportunityNo, _email, "frmopportunity", Activity)
                Next
            Else
                ShowErrorMessage("No email template is found for Opportunity Support.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetAutoEmailData()
        Dim Dr As DataRow
        Try
            Dim str As String
            str = "select OpportunityId,DocNo from tblDefOpportunity where DocNo = '" & txtDocumentNo.Text & "'"
            Dim dt1 As DataTable = GetDataTable(str)
            If dt1.Rows.Count > 0 Then
                SalesOrderId = dt1.Rows(0).Item("OpportunityId")
                OpportunityNo = dt1.Rows(0).Item("DocNo")
            End If
            'Dim str2 As String
            'str2 = "SELECT tblDefLeadType.Title as CustomerType, tblDefLeadProfileContacts.CityId + ',' + tblListCountry.CountryName as ShippingAddress, tblDefOpportunityDelivery.Title AS IncoTerms FROM tblDefLeadType RIGHT OUTER JOIN tblDefOpportunityDelivery RIGHT OUTER JOIN tblDefOpportunity ON tblDefOpportunityDelivery.Id = tblDefOpportunity.DeliveryId LEFT OUTER JOIN tblDefLeadProfile ON tblDefOpportunity.CompanyId = tblDefLeadProfile.LeadProfileId ON tblDefLeadType.Id = tblDefLeadProfile.TypeId LEFT OUTER JOIN tblListCountry RIGHT OUTER JOIN tblDefLeadProfileContacts ON tblListCountry.CountryId = tblDefLeadProfileContacts.CountryId ON tblDefLeadProfile.LeadProfileId = tblDefLeadProfileContacts.LeadProfileId  WHERE tblDefOpportunity.OpportunityId=" & SalesOrderId
            'Dim dtmaster As DataTable = GetDataTable(str2)
            'For Each Row1 As DataRow In dtmaster.Rows
            '    Dr = dtEmail.NewRow
            '    For Each col As String In AllFields
            '        If Row1.Table.Columns.Contains(col) Then
            '            Dr.Item(col) = Row1.Item(col).ToString
            '        End If
            '    Next
            '    dtEmailMaster.Rows.Add(Dr)
            'Next
            Dim str1 As String
            str1 = "SELECT tblDefLeadType.Title AS CustomerType,tblListCountry.CountryName as ShippingCountry, tblDefOpportunity.LeadTimeInDays as City, tblDefOpportunityDelivery.Title as IncoTerms, 'OEM standard' as WarrantyRequested, tblDefOpportunityHardwareDetail.PartNo,  tblDefOpportunityHardwareDetail.Type, tblDefOpportunityHardwareDetail.BrandNo, tblDefOpportunityHardwareDetail.Description, tblDefOpportunityHardwareDetail.Status, tblDefOpportunityHardwareDetail.Warranty,  tblDefOpportunityHardwareDetail.Qty, tblDefOpportunityHardwareDetail.Price, tblDefOpportunityHardwareDetail.Discount, tblDefOpportunityHardwareDetail.TotalAmount,  tblDefOpportunityHardwareDetail.FilePath, tblDefOpportunityHardwareDetail.LeadTime FROM tblDefLeadProfile LEFT OUTER JOIN tblListCountry RIGHT OUTER JOIN tblDefOpportunityDelivery RIGHT OUTER JOIN tblDefOpportunity LEFT OUTER JOIN tblDefOpportunityHardwareDetail ON tblDefOpportunity.OpportunityId = tblDefOpportunityHardwareDetail.OpportunityId ON tblDefOpportunityDelivery.Id = tblDefOpportunity.DeliveryId ON tblListCountry.CountryId = tblDefOpportunity.CountryId ON tblDefLeadProfile.LeadProfileId = tblDefOpportunity.CompanyId LEFT OUTER JOIN tblDefLeadType ON tblDefLeadProfile.TypeId = tblDefLeadType.Id  WHERE tblDefOpportunity.OpportunityId=" & SalesOrderId
            Dim dt As DataTable = GetDataTable(str1)
            For Each Row1 As DataRow In dt.Rows
                Dr = dtEmail.NewRow
                For Each col As String In AllFields
                    If Row1.Table.Columns.Contains(col) Then
                        Dr.Item(col) = Row1.Item(col).ToString
                    End If
                Next
                dtEmail.Rows.Add(Dr)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetAutoEmailDataSupport()
        Dim Dr As DataRow
        Try
            Dim str As String
            str = "select top 1 OpportunityId,DocNo from tblDefOpportunity order by 1 desc"
            Dim dt1 As DataTable = GetDataTable(str)
            If dt1.Rows.Count > 0 Then
                SalesOrderId = dt1.Rows(0).Item("OpportunityId")
                OpportunityNo = dt1.Rows(0).Item("DocNo")
            End If
            Dim str1 As String
            str1 = "Select Brand, ModelNo, SerialNo, SLACoverage, Address, City, Province, Country, StartDate, EndDate, Type, UnitPrice, FilePath from tblDefOpportunitySupportDetail  where OpportunityId=" & OpportunityId
            Dim dt As DataTable = GetDataTable(str1)
            For Each Row1 As DataRow In dt.Rows
                Dr = dtEmail.NewRow
                For Each col As String In AllFields
                    If Row1.Table.Columns.Contains(col) Then
                        Dr.Item(col) = Row1.Item(col).ToString
                    End If
                Next
                dtEmail.Rows.Add(Dr)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FormatStringBuilder(ByVal dt As DataTable)
        Try
            html = New StringBuilder
            html.Append(EmailTemplate)
            html.Append("<table border = '1'>")
            'Building the Header row.
            html.Append("<tr bgcolor='#58ACFA'>")
            For Each column As DataColumn In dt.Columns
                Dim ColumnName As String = ""
                Dim Pattern = "([a-z?])[_ ]?([A-Z])"
                If column.ColumnName = "SerialNo" Then
                    ColumnName = "Sr#"
                Else
                    ColumnName = Regex.Replace(column.ColumnName, Pattern, "$1 $2")
                End If
                html.Append("<th>")
                html.Append(ColumnName)
                html.Append("</th>")
            Next
            html.Append("</tr>")
            'Building the Data rows.
            For Each row As DataRow In dt.Rows
                If row.Table.Columns.Contains("Alternate") Then
                    If row.Item("Alternate") = "Yes" Then
                        html.Append("<tr bgcolor='#A9F5BC'>")
                        For Each column As DataColumn In dt.Columns
                            html.Append("<td>")
                            html.Append(row(column.ColumnName))
                            html.Append("</td>")
                        Next
                        html.Append("</tr>")
                    Else
                        html.Append("<tr>")
                        For Each column As DataColumn In dt.Columns
                            html.Append("<td>")
                            html.Append(row(column.ColumnName))
                            html.Append("</td>")
                        Next
                        html.Append("</tr>")
                    End If
                Else
                    html.Append("<tr>")
                    For Each column As DataColumn In dt.Columns
                        html.Append("<td>")
                        html.Append(row(column.ColumnName))
                        html.Append("</td>")
                    Next
                    html.Append("</tr>")
                End If
            Next
            html.Append("</table>")
            html.Append(AfterFieldsElement)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FormatStringBuilderMaster(ByVal dt As DataTable)
        Try
            html = New StringBuilder
            html.Append(EmailTemplate)
            html.Append("<table border = '1'>")
            'Building the Header row.
            html.Append("<tr bgcolor='#58ACFA'>")
            For Each column As DataColumn In dt.Columns
                Dim ColumnName As String = ""
                Dim Pattern = "([a-z?])[_ ]?([A-Z])"
                If column.ColumnName = "SerialNo" Then
                    ColumnName = "Sr#"
                Else
                    ColumnName = Regex.Replace(column.ColumnName, Pattern, "$1 $2")
                End If
                html.Append("<th>")
                html.Append(ColumnName)
                html.Append("</th>")
            Next
            html.Append("</tr>")
            'Building the Data rows.
            For Each row As DataRow In dt.Rows
                If row.Table.Columns.Contains("Alternate") Then
                    If row.Item("Alternate") = "Yes" Then
                        html.Append("<tr bgcolor='#A9F5BC'>")
                        For Each column As DataColumn In dt.Columns
                            html.Append("<td>")
                            html.Append(row(column.ColumnName))
                            html.Append("</td>")
                        Next
                        html.Append("</tr>")
                    Else
                        html.Append("<tr>")
                        For Each column As DataColumn In dt.Columns
                            html.Append("<td>")
                            html.Append(row(column.ColumnName))
                            html.Append("</td>")
                        Next
                        html.Append("</tr>")
                    End If
                Else
                    html.Append("<tr>")
                    For Each column As DataColumn In dt.Columns
                        html.Append("<td>")
                        html.Append(row(column.ColumnName))
                        html.Append("</td>")
                    Next
                    html.Append("</tr>")
                End If
            Next
            html.Append("</table>")
            html.Append(AfterFieldsElement)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetTemplate(ByVal Title As String)
        Dim Fields As String = String.Empty
        Try
            dtEmail = New DataTable
            EmailTemplate = EmailDAL.GetTemplate(Title)
            If EmailTemplate.Length > 0 Then
                Dim i, j As Integer
                i = EmailTemplate.IndexOf("<Fields>") + "<Fields>".Length
                j = EmailTemplate.IndexOf("</Fields>") - i

                Dim Searched As String = "</Fields>"
                AfterFieldsElement = EmailTemplate.Substring(EmailTemplate.IndexOf(Searched) + Searched.Length)
                Fields = EmailTemplate.Substring(i, j)
                Dim WOAtTheRate As String = Fields.Replace("@", "")
                Dim WOSpace As String = WOAtTheRate.Replace(" ", "")
                Dim IndexOfFieldElement As Integer = EmailTemplate.IndexOf("<Fields>")
                If IndexOfFieldElement > 0 Then
                    EmailTemplate = EmailTemplate.Remove(IndexOfFieldElement)
                End If
                AllFields = New List(Of String)

                dtEmail.Columns.Clear()
                For Each word As String In WOSpace.Split(",")
                    Dim TrimSpace As String = word.Trim()
                    ' If Me.grdHardware.RootTable.Columns.Contains(TrimSpace) Then
                    dtEmail.Columns.Add(TrimSpace)
                    AllFields.Add(TrimSpace)
                    ' End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetTemplateSupport(ByVal Title As String)
        Dim Fields As String = String.Empty
        Try
            dtEmail = New DataTable
            EmailTemplate = EmailDAL.GetTemplate(Title)
            If EmailTemplate.Length > 0 Then
                Dim i, j As Integer
                i = EmailTemplate.IndexOf("<Fields>") + "<Fields>".Length
                j = EmailTemplate.IndexOf("</Fields>") - i

                Dim Searched As String = "</Fields>"
                AfterFieldsElement = EmailTemplate.Substring(EmailTemplate.IndexOf(Searched) + Searched.Length)
                Fields = EmailTemplate.Substring(i, j)
                Dim WOAtTheRate As String = Fields.Replace("@", "")
                Dim WOSpace As String = WOAtTheRate.Replace(" ", "")
                Dim IndexOfFieldElement As Integer = EmailTemplate.IndexOf("<Fields>")
                If IndexOfFieldElement > 0 Then
                    EmailTemplate = EmailTemplate.Remove(IndexOfFieldElement)
                End If
                AllFields = New List(Of String)

                dtEmail.Columns.Clear()
                For Each word As String In WOSpace.Split(",")
                    Dim TrimSpace As String = word.Trim()
                    If Me.grdSupport.RootTable.Columns.Contains(TrimSpace) Then
                        dtEmail.Columns.Add(TrimSpace)
                        AllFields.Add(TrimSpace)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMail(ByVal Email As String)
        Try
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            mailItem.Subject = "Creating New Opportunity against: " + txtOpportunityName.Text
            mailItem.To = Email
            Email = String.Empty
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
            EmailBody = html.ToString
            mailItem.Send()
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSaveAndExport_Click(sender As Object, e As EventArgs) Handles btnSaveAndExport.Click

    End Sub

    Private Sub txtLeadTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLeadTime.KeyPress
        Try
            'NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtFreight_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFreight.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtGrandTotal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGrandTotal.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSST_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSST.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtImplementationTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtImplementationTime.KeyPress
        Try
            'NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTargetPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTargetPrice.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddHardware_Click(sender As Object, e As EventArgs) Handles btnAddHardware.Click
        Try
            Dim dtHardware As DataTable = CType(Me.grdHardware.DataSource, DataTable)
            'dtHardware.Columns("Tax_Amount").Expression = "(((Amount-Discount)*Tax)/100)"
            Dim dr As DataRow = dtHardware.NewRow()
            dtHardware.Rows.InsertAt(dr, 0)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddSupport_Click(sender As Object, e As EventArgs) Handles btnAddSupport.Click
        Try
            'Dim support As OpportunitySupportDetailBE()
            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSupport.GetRows
            '    For i As Integer = 0 To grdSupport.RootTable.Columns.Count - 1
            '        If OpportunityName.ToString = Row.Cells(0).Value Then
            '            MessageBox.Show("serial no duplicate")
            '            Return
            '        End If
            '        'If DAL.IsSerialNoExisted(Me.grdSupport.RootTable.Columns("SerialNo").ToString) Then
            '        '    ShowErrorMessage("Serial name already exists.")
            '        '    Me.txtOpportunityName.Focus()
            '        '    'Return False
3226980299:
            '        'Else
            '    Next
            'Next
            Dim dtSupport As DataTable = CType(Me.grdSupport.DataSource, DataTable)
            Dim dr As DataRow = dtSupport.NewRow()
            dtSupport.Rows.InsertAt(dr, 0)
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetAttachments()
        Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty
            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
            "All files (*.*)|*.*"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a
                'If Me.btnSave.Text <> "&Save" Then
                '    If Me.grdSaved.RowCount > 0 Then
                '        intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
                '    End If
                'End If
                Me.btnAttachments.Text = "Attachments (" & arrFile.Count + TotalAttachments & ")"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAttachments_Click(sender As Object, e As EventArgs) Handles btnAttachments.Click
        Try
            GetAttachments()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS4867
    ''' </summary>
    ''' <param name="UserId"></param>
    ''' <remarks></remarks>
    Private Sub GetUseWiseEmployee(ByVal UserId As Integer)
        Try
            ''TASK TFS4867
            Dim dtEmployee As DataTable = DAL.GetUserWiseEmployee(LoginUserId)
            If dtEmployee.Rows.Count > 0 Then
                lblEmployeeName.Text = dtEmployee.Rows(0).Item("EmployeeName").ToString
                EmployeeId = Val(dtEmployee.Rows(0).Item("EmployeeId").ToString)
                If Not IsDBNull(dtEmployee.Rows(0).Item("EmpPicture")) Then
                    If IO.File.Exists(dtEmployee.Rows(0).Item("EmpPicture")) Then
                        Me.pbUser.ImageLocation = dtEmployee.Rows(0).Item("EmpPicture")
                        Me.pbUser.Update()
                    Else
                        Me.pbUser.Image = Nothing
                    End If
                Else
                    Me.pbUser.Image = Nothing
                End If
            End If
            ''END TASK TFS4867
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetEmployee(ByVal EmployeeId As Integer)
        Try
            ''TASK TFS4867
            Dim dtEmployee As DataTable = DAL.GetEmployee(EmployeeId)
            If dtEmployee.Rows.Count > 0 Then
                lblEmployeeName.Text = dtEmployee.Rows(0).Item("EmployeeName").ToString
                If Not IsDBNull(dtEmployee.Rows(0).Item("EmpPicture")) Then
                    If IO.File.Exists(dtEmployee.Rows(0).Item("EmpPicture")) Then
                        Me.pbUser.ImageLocation = dtEmployee.Rows(0).Item("EmpPicture")
                        Me.pbUser.Update()
                    Else
                        Me.pbUser.Image = Nothing
                    End If
                Else
                    Me.pbUser.Image = Nothing
                End If
            End If
            ''END TASK TFS4867
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click, Button1.Click

        AddRptParam("@id", Me.OpportunityId)
        If rbSupport.Checked = True Then
            ShowReport("rptOppurtunitySoftware")
        Else
            ShowReport("rptOppurtunityHardware1")
        End If

    End Sub

    Private Sub pnlContract_Paint(sender As Object, e As PaintEventArgs) Handles pnlContract.Paint

    End Sub

    Private Sub chkVATMandatory_CheckedChanged(sender As Object, e As EventArgs) Handles chkVATMandatory.CheckedChanged
        Try
            If chkVATMandatory.Checked = True Then
                txtSST.Enabled = False
            Else
                txtSST.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbStage_ValueChanged(sender As Object, e As EventArgs) Handles cmbStage.ValueChanged
        Try
            If cmbStage.Value = 10 Then
                cmbReasonforloosing.Visible = True
                Label10.Visible = True
            Else
                cmbReasonforloosing.Visible = False
                Label10.Visible = False
            End If
            If cmbStage.Value = 8 AndAlso rbSupport.Checked = True Then
                cmbItem.Visible = True
            Else
                cmbItem.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSupport_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSupport.CellUpdated
        Try
            lblTotalAmount.Text = Math.Round((((Val(Me.grdSupport.GetTotal(Me.grdSupport.RootTable.Columns("UnitPrice"), Janus.Windows.GridEX.AggregateFunction.Sum))))), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdHardware_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdHardware.CellUpdated
        Try
            lblTotalAmount.Text = Math.Round((((Val(Me.grdHardware.GetTotal(Me.grdHardware.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum))))), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdHardware_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdHardware.FormattingRow
        Try
            lblTotalAmount.Text = Math.Round((((Val(Me.grdHardware.GetTotal(Me.grdHardware.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum))))), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSupport_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdSupport.FormattingRow
        Try
            lblTotalAmount.Text = Math.Round((((Val(Me.grdSupport.GetTotal(Me.grdSupport.RootTable.Columns("UnitPrice"), Janus.Windows.GridEX.AggregateFunction.Sum))))), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSupport_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSupport.CellValueChanged
        Try
            lblTotalAmount.Text = Math.Round((((Val(Me.grdSupport.GetTotal(Me.grdSupport.RootTable.Columns("UnitPrice"), Janus.Windows.GridEX.AggregateFunction.Sum))))), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdHardware_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdHardware.CellValueChanged
        Try
            lblTotalAmount.Text = Math.Round((((Val(Me.grdHardware.GetTotal(Me.grdHardware.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum))))), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pnlHarware_Paint(sender As Object, e As PaintEventArgs) Handles pnlHarware.Paint

    End Sub

    Private Sub lblHeader_Click(sender As Object, e As EventArgs) Handles lblHeader.Click

    End Sub

    Private Sub cmbOnsiteIntervention_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOnsiteIntervention.SelectedIndexChanged, cmbInvoicePattern.SelectedIndexChanged, cmbDurationofMonth.SelectedIndexChanged, ComboBox1.SelectedIndexChanged, ComboBox2.SelectedIndexChanged, cmbPMFrequency.SelectedIndexChanged

    End Sub

    Private Sub rbSupport_CheckedChanged(sender As Object, e As EventArgs) Handles rbSupport.CheckedChanged

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

    End Sub

    Private Function CalculateMode() As Object
        Throw New NotImplementedException
    End Function

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click

    End Sub

    Private Sub btnExportHardware_Click(sender As Object, e As EventArgs) Handles btnExportHardware.Click

    End Sub






End Class
