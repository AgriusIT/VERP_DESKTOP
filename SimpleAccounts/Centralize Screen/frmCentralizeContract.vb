'Rafay:created this screen to show detail report
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmCentralizeContract
    Implements IGeneral

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                ''  Me.btnSave.Enabled = True
                'Rafay:give rights to admin to view print ,export,choosefielder 
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                '' DoHaveDeleteRights = True
                'Rafay:Task End
                Exit Sub
            End If
            Me.Visible = False
            'Me.btnSave.Enabled = False
            'Rafay:Task Start
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            'Rafay:Task End
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True

                End If

                'Rafay:Task End
            Next
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub frmCentralizeContract_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetSecurityRights()
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = String.Empty
            Dim strFilter As String = String.Empty
            ''//Get all and Get top 50//'' TASK # 975
            'Ali Faisal : TFS1606 : Added CompanyId column in history
            'Rafay:add user rights agar tou login user admin hai tou sab record show karay ga aur agar tou normal user hai tou else wali str show karay ga
            'Rafay :add 2 column (ContractStatus,PreviousContracts) 
            If LoginUser.LoginUserGroup = "Administrator" Then
                str = "SELECT ContractMasterTable.ContractId, ContractMasterTable.ContractNo, ContractMasterTable.StartDate, ContractMasterTable.EndDate, ContractMasterTable.SLAType, ContractMasterTable.PreventionMaintenance, ContractMasterTable.CustomerId, ContractMasterTable.OpportunityId, ContractMasterTable.Status, ContractMasterTable.EndCustomer, ContractMasterTable.PONumber, ContractMasterTable.ContractType, ContractMasterTable.ContactofNotification, ContractMasterTable.Site, ContractMasterTable.InvoicingFrequency, ContractMasterTable.PaymentTerms, ContractMasterTable.Comments, ContractMasterTable.Employee , IsNull(Doc_Att.[No Of Attachment],0) as  [No Of Attachment], ContractMasterTable.Amount, ContractMasterTable.Currency, ContractMasterTable.ContractStatus, ContractMasterTable.PreviousContracts,TerminateStatus,GETDATE() as CurrentDate,isnull(HoldReason,'') as HoldReason,isnull(OthersDescription,'') as OthersDescription,isnull(HoldCheckBox,0) as HoldCheckBox  FROM [SIRIUS1_DB].[dbo].ContractMasterTable LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From [SIRIUS1_DB].[dbo].DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId,source) Doc_Att on Doc_Att.DocId = [SIRIUS1_DB].[dbo].ContractMasterTable.ContractId" _
                      & " Union all " _
                      & "SELECT ContractMasterTable.ContractId, ContractMasterTable.ContractNo, ContractMasterTable.StartDate, ContractMasterTable.EndDate, ContractMasterTable.SLAType, ContractMasterTable.PreventionMaintenance, ContractMasterTable.CustomerId, ContractMasterTable.OpportunityId, ContractMasterTable.Status, ContractMasterTable.EndCustomer, ContractMasterTable.PONumber, ContractMasterTable.ContractType, ContractMasterTable.ContactofNotification, ContractMasterTable.Site, ContractMasterTable.InvoicingFrequency, ContractMasterTable.PaymentTerms, ContractMasterTable.Comments, ContractMasterTable.Employee , IsNull(Doc_Att.[No Of Attachment],0) as  [No Of Attachment], ContractMasterTable.Amount, ContractMasterTable.Currency, ContractMasterTable.ContractStatus, ContractMasterTable.PreviousContracts,TerminateStatus,GETDATE() as CurrentDate,isnull(HoldReason,'') as HoldReason,isnull(OthersDescription,'') as OthersDescription,isnull(HoldCheckBox,0) as HoldCheckBox  FROM [SIRIUS_UAE_DB].[dbo].ContractMasterTable LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From [SIRIUS_UAE_DB].[dbo].DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId,source) Doc_Att on Doc_Att.DocId = [SIRIUS_UAE_DB].[dbo].ContractMasterTable.ContractId  order by ContractMasterTable.ContractId DESC"
            Else
                str = "SELECT ContractMasterTable.ContractId, ContractMasterTable.ContractNo, ContractMasterTable.StartDate, ContractMasterTable.EndDate, ContractMasterTable.SLAType, ContractMasterTable.PreventionMaintenance, ContractMasterTable.CustomerId, ContractMasterTable.OpportunityId, ContractMasterTable.Status, ContractMasterTable.EndCustomer, ContractMasterTable.PONumber, ContractMasterTable.ContractType, ContractMasterTable.ContactofNotification, ContractMasterTable.Site, ContractMasterTable.InvoicingFrequency, ContractMasterTable.PaymentTerms, ContractMasterTable.Comments, ContractMasterTable.Employee , IsNull(Doc_Att.[No Of Attachment],0) as  [No Of Attachment], ContractMasterTable.Amount, ContractMasterTable.Currency, ContractMasterTable.ContractStatus, ContractMasterTable.PreviousContracts,TerminateStatus,GETDATE() as CurrentDate,isnull(HoldReason,'') as HoldReason,isnull(OthersDescription,'') as OthersDescription,isnull(HoldCheckBox,0) as HoldCheckBox  FROM [SIRIUS1_DB].[dbo].ContractMasterTable LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId,source) Doc_Att on Doc_Att.DocId = ContractMasterTable.ContractId where ContractStatus <> 'Terminate'" _
                    & " Union all " _
                    & "SELECT ContractMasterTable.ContractId, ContractMasterTable.ContractNo, ContractMasterTable.StartDate, ContractMasterTable.EndDate, ContractMasterTable.SLAType, ContractMasterTable.PreventionMaintenance, ContractMasterTable.CustomerId, ContractMasterTable.OpportunityId, ContractMasterTable.Status, ContractMasterTable.EndCustomer, ContractMasterTable.PONumber, ContractMasterTable.ContractType, ContractMasterTable.ContactofNotification, ContractMasterTable.Site, ContractMasterTable.InvoicingFrequency, ContractMasterTable.PaymentTerms, ContractMasterTable.Comments, ContractMasterTable.Employee , IsNull(Doc_Att.[No Of Attachment],0) as  [No Of Attachment], ContractMasterTable.Amount, ContractMasterTable.Currency, ContractMasterTable.ContractStatus, ContractMasterTable.PreviousContracts,TerminateStatus,GETDATE() as CurrentDate,isnull(HoldReason,'') as HoldReason,isnull(OthersDescription,'') as OthersDescription,isnull(HoldCheckBox,0) as HoldCheckBox  [SIRIUS_UAE_DB].[dbo].FROM ContractMasterTable LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId,source) Doc_Att on Doc_Att.DocId = ContractMasterTable.ContractId where ContractStatus <> 'Terminate' order by ContractMasterTable.ContractId DESC"
            End If
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            '    dt.Columns("ContractStatus").Expression = "IIF(EndDate < CurrentDate,'Expired',ContractStatus)"
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdSaved.RootTable.Columns("No Of Attachment").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            grdSaved.RootTable.Columns("ContractId").Visible = False
            grdSaved.RootTable.Columns("CurrentDate").Visible = False
            'GrdStatus.RootTable.Columns("CustomerId").Visible = False
            'GrdStatus.RootTable.Columns("OpportunityId").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub btnLoadAll_Click(sender As Object, e As EventArgs) Handles btnLoadAll.Click
        Try
            GetAllRecords()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim str As String = String.Empty
            Dim strFilter As String = String.Empty
            ''//Get all and Get top 50//'' TASK # 975
            'Ali Faisal : TFS1606 : Added CompanyId column in history
            'Rafay:add user rights agar tou login user admin hai tou sab record show karay ga aur agar tou normal user hai tou else wali str show karay ga
            'Rafay :add 2 column (ContractStatus,PreviousContracts) 
            If LoginUser.LoginUserGroup = "Administrator" Then
                str = "SELECT ContractMasterTable.ContractId, ContractMasterTable.ContractNo, ContractMasterTable.StartDate, ContractMasterTable.EndDate, ContractMasterTable.SLAType, ContractMasterTable.PreventionMaintenance, ContractMasterTable.CustomerId, ContractMasterTable.OpportunityId, ContractMasterTable.Status, ContractMasterTable.EndCustomer, ContractMasterTable.PONumber, ContractMasterTable.ContractType, ContractMasterTable.ContactofNotification, ContractMasterTable.Site, ContractMasterTable.InvoicingFrequency, ContractMasterTable.PaymentTerms, ContractMasterTable.Comments, ContractMasterTable.Employee , IsNull(Doc_Att.[No Of Attachment],0) as  [No Of Attachment], ContractMasterTable.Amount, ContractMasterTable.Currency, ContractMasterTable.ContractStatus, ContractMasterTable.PreviousContracts,TerminateStatus,GETDATE() as CurrentDate,isnull(HoldReason,'') as HoldReason,isnull(OthersDescription,'') as OthersDescription,isnull(HoldCheckBox,0) as HoldCheckBox  FROM [SIRIUS1_DB].[dbo].ContractMasterTable LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From [SIRIUS1_DB].[dbo].DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId,source) Doc_Att on Doc_Att.DocId = [SIRIUS1_DB].[dbo].ContractMasterTable.ContractId  where (((ContractMasterTable.StartDate) >= '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "' And (ContractMasterTable.StartDate) <= '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "')) " _
                      & "Union all " _
                      & "SELECT ContractMasterTable.ContractId, ContractMasterTable.ContractNo, ContractMasterTable.StartDate, ContractMasterTable.EndDate, ContractMasterTable.SLAType, ContractMasterTable.PreventionMaintenance, ContractMasterTable.CustomerId, ContractMasterTable.OpportunityId, ContractMasterTable.Status, ContractMasterTable.EndCustomer, ContractMasterTable.PONumber, ContractMasterTable.ContractType, ContractMasterTable.ContactofNotification, ContractMasterTable.Site, ContractMasterTable.InvoicingFrequency, ContractMasterTable.PaymentTerms, ContractMasterTable.Comments, ContractMasterTable.Employee , IsNull(Doc_Att.[No Of Attachment],0) as  [No Of Attachment], ContractMasterTable.Amount, ContractMasterTable.Currency, ContractMasterTable.ContractStatus, ContractMasterTable.PreviousContracts,TerminateStatus,GETDATE() as CurrentDate,isnull(HoldReason,'') as HoldReason,isnull(OthersDescription,'') as OthersDescription,isnull(HoldCheckBox,0) as HoldCheckBox  FROM [SIRIUS_UAE_DB].[dbo].ContractMasterTable LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From [SIRIUS_UAE_DB].[dbo].DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId,source) Doc_Att on Doc_Att.DocId = [SIRIUS_UAE_DB].[dbo].ContractMasterTable.ContractId where (((ContractMasterTable.StartDate) >= '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "' And (ContractMasterTable.StartDate) <= '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "')) order by ContractMasterTable.ContractId DESC "
            Else
                str = "SELECT ContractMasterTable.ContractId, ContractMasterTable.ContractNo, ContractMasterTable.StartDate, ContractMasterTable.EndDate, ContractMasterTable.SLAType, ContractMasterTable.PreventionMaintenance, ContractMasterTable.CustomerId, ContractMasterTable.OpportunityId, ContractMasterTable.Status, ContractMasterTable.EndCustomer, ContractMasterTable.PONumber, ContractMasterTable.ContractType, ContractMasterTable.ContactofNotification, ContractMasterTable.Site, ContractMasterTable.InvoicingFrequency, ContractMasterTable.PaymentTerms, ContractMasterTable.Comments, ContractMasterTable.Employee , IsNull(Doc_Att.[No Of Attachment],0) as  [No Of Attachment], ContractMasterTable.Amount, ContractMasterTable.Currency, ContractMasterTable.ContractStatus, ContractMasterTable.PreviousContracts,TerminateStatus,GETDATE() as CurrentDate,isnull(HoldReason,'') as HoldReason,isnull(OthersDescription,'') as OthersDescription,isnull(HoldCheckBox,0) as HoldCheckBox  FROM [SIRIUS1_DB].[dbo].ContractMasterTable LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From [SIRIUS1_DB].[dbo].DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId,source) Doc_Att on Doc_Att.DocId = [SIRIUS1_DB].[dbo].ContractMasterTable.ContractId where ContractStatus <> 'Terminate' and (((ContractMasterTable.StartDate) >= '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "' And (ContractMasterTable.StartDate) <= '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "')) " _
                    & "Union all " _
                    & "SELECT ContractMasterTable.ContractId, ContractMasterTable.ContractNo, ContractMasterTable.StartDate, ContractMasterTable.EndDate, ContractMasterTable.SLAType, ContractMasterTable.PreventionMaintenance, ContractMasterTable.CustomerId, ContractMasterTable.OpportunityId, ContractMasterTable.Status, ContractMasterTable.EndCustomer, ContractMasterTable.PONumber, ContractMasterTable.ContractType, ContractMasterTable.ContactofNotification, ContractMasterTable.Site, ContractMasterTable.InvoicingFrequency, ContractMasterTable.PaymentTerms, ContractMasterTable.Comments, ContractMasterTable.Employee , IsNull(Doc_Att.[No Of Attachment],0) as  [No Of Attachment], ContractMasterTable.Amount, ContractMasterTable.Currency, ContractMasterTable.ContractStatus, ContractMasterTable.PreviousContracts,TerminateStatus,GETDATE() as CurrentDate,isnull(HoldReason,'') as HoldReason,isnull(OthersDescription,'') as OthersDescription,isnull(HoldCheckBox,0) as HoldCheckBox  FROM [SIRIUS_UAE_DB].[dbo].ContractMasterTable LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From [SIRIUS_UAE_DB].[dbo].DocumentAttachment WHERE (source = N'" & Me.Name & "') Group By DocId,source) Doc_Att on Doc_Att.DocId = [SIRIUS_UAE_DB].[dbo].ContractMasterTable.ContractId where ContractStatus <> 'Terminate' and (((ContractMasterTable.StartDate) >= '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "' And (ContractMasterTable.StartDate) <= '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "')) order by ContractMasterTable.ContractId DESC "
            End If
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            '    dt.Columns("ContractStatus").Expression = "IIF(EndDate < CurrentDate,'Expired',ContractStatus)"
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.ApplyGridSettings()
            Me.grdSaved.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdSaved.RootTable.Columns("No Of Attachment").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            grdSaved.RootTable.Columns("ContractId").Visible = False
            grdSaved.RootTable.Columns("CurrentDate").Visible = False
            'GrdStatus.RootTable.Columns("CustomerId").Visible = False
            'GrdStatus.RootTable.Columns("OpportunityId").Visible = False

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load

    End Sub
End Class