Imports SBDal
Imports SBModel
Imports System
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop

Public Class frmTicketOpening
    Implements IGeneral
    Dim objModel As TicketOpeningBE
    Dim objDAL As TicketOpeningDAL
    Dim list As List(Of TicketOpeningDetailBE)
    Dim TicketId As Integer
    Dim iseditmode As Boolean
    Dim _SearchDt As New DataTable
    Public DoHaveDeleteRights As Boolean = False
    Dim flag As Boolean = False
    'Changes added by Murtaza for email generation on save (12/21/2022)
    Dim EmailDAL As New EmailTemplateDAL
    Dim EmailTemplate As String = String.Empty
    Dim dtEmail As DataTable
    Dim AfterFieldsElement As String = String.Empty
    Dim AllFields As List(Of String)
    Dim PurchaseDemandNo As String
    Dim EmailBody As String = String.Empty
    Dim PurchaseDemandId As Integer
    Dim html As StringBuilder
    Dim UsersEmail As List(Of String)
    Dim status As String = String.Empty
    Dim Approved As String = String.Empty
    Dim dtpsperverdate As Date
    Dim EticketNo As Integer = 0

    'Changes added by Murtaza for email generation on save (12/21/2022)
    Enum grdPOS
        POSId
        POSTitle
        CompanyId
        Company
        LocationId
        Location
        CostCenterId
        CostCenter
        CashAccountId
        CashAccount
        BankAccountId
        BankAccount
        SalesPersonId
        SalesPerson
        DeliveryOption
        Active
        DiscountPer
    End Enum

    Enum grdCredit
        CreditCardId
        POSID
        MachineTitle
        BankAccountId2
    End Enum
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns(grdPOS.POSId).Visible = False
            Me.grd.RootTable.Columns(grdPOS.CompanyId).Visible = False
            Me.grd.RootTable.Columns(grdPOS.LocationId).Visible = False
            Me.grd.RootTable.Columns(grdPOS.CostCenterId).Visible = False
            Me.grd.RootTable.Columns(grdPOS.CashAccountId).Visible = False
            Me.grd.RootTable.Columns(grdPOS.BankAccountId).Visible = False
            Me.grd.RootTable.Columns(grdPOS.SalesPersonId).Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty

            Select Case Condition

                Case "SerialNo"
                    If flag = True Then
                        'strSQL = "SELECT ContractDetailId, SerialNo from ContractDetailTable where ContractId = " & cmbContractNo.SelectedValue & ""
                        strSQL = ";with cte as(select ContractdetailId,SerialNo,ROW_NUMBER()over(partition by SerialNo order by Contractdetailid DESC) as rn from contractdetailtable Where ContractId = " & cmbContractNo.SelectedValue & " ) SELECT * from cte where rn=1"
                    Else
                        'strSQL = "SELECT ContractDetailId, SerialNo from ContractDetailTable"
                        strSQL = ";with cte as(select ContractdetailId,SerialNo,ROW_NUMBER()over(partition by SerialNo order by Contractdetailid DESC) as rn from contractdetailtable) SELECT * from cte where rn=1"
                    End If
                    FillUltraDropDown(cmbSerialNo, strSQL)
                    Me.cmbSerialNo.Rows(0).Activate()
                    Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("ContractdetailId").Hidden = True
                    Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("rn").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("ContractId").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("OpportunityId").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("Brand").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("ModelNo").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("SLACoverage").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("Address").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("City").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("Province").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("Country").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("StartDate").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("EndDate").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("Type").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("UnitPrice").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("FilePath").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("SLA").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("SLAInterventionTime").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("SLAFixTime").Hidden = True
                    'Me.cmbSerialNo.DisplayLayout.Bands(0).Columns("OnsiteIntervention").Hidden = True
                    'Me.cmbSerialNo.DisplayMember = Me.cmbSerialNo.Rows(0).Cells(4).Column.Key.ToString
                Case "Contract"
                    strSQL = "SELECT ContractId,ContractNo,HoldCheckBox,ChkBoxBatteriesIncluded FROM ContractMasterTable"
                    FillDropDown(Me.cmbContractNo, strSQL, True)
                Case "Engineer"
                    strSQL = "select Employee_ID, Employee_Name from tblDefEmployee where Dept_ID = 7 and Active = 1"
                    FillListBox(Me.lstEngineerAssigned.ListItem, strSQL)
                    FillListBox(Me.lstOnsiteEngineer.ListItem, strSQL)
                    FillDropDown(Me.cmbEngineerAssigned, strSQL, False)
                    'FillDropDown(Me.cmbOnsiteEngineer, strSQL, False)
                Case "PartNo"
                    strSQL = "SELECT ArticleId AS Id, ArticleCode AS Code, ArticleDescription AS Item, ArticleSizeName AS Size, ArticleColorName AS Combination, ArticleBrandName AS Grade, ISNULL(SalePrice, 0) AS SalePrice, SizeRangeId AS [Size ID], ISNULL(ArticleTaxId, 0) AS [Tax ID], ISNULL(PurchasePrice, 0) AS PurchasePrice, ISNULL(SubSubID, 0) AS AccountId, ISNULL(ServiceItem, 0) AS ServiceItem, SortOrder, ArticleGroupName AS Dept, ArticleTypeName AS Type, ArticleGenderName AS Origin, ArticleLpoName AS Brand, ISNULL(LogicalItem, 0) AS LogicalItem, SalesAccountId, CGSAccountId, MasterID, ISNULL(Cost_Price, 0) AS Cost_Price, ISNULL(TradePrice, 0) AS [Trade Price], ISNULL(PrintedRetailPrice, 0) AS [Retail Price], ISNULL(ApplyAdjustmentFuelExp, 1) AS ApplyAdjustmentFuelExp FROM ArticleDefView Where ArticleDefView.Active=1  "
                    FillListBox(Me.lstPNUsed.ListItem, strSQL)
                    _SearchDt = CType(Me.lstPNUsed.ListItem.DataSource, DataTable)
                    _SearchDt.AcceptChanges()
                Case "SaleOrderNo"
                    strSQL = "SELECT SOMT.SalesOrderId,SOMT.SalesOrderNo from SalesOrderMasterTable SOMT JOIN SalesOrderDetailTable SODT ON SOMT.SalesOrderId=SODT.SalesOrderId WHERE SODT.ArticleDefId in (177,3271,4804)"
                    FillDropDown(Me.cmbSaleorderNo, strSQL, True)
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub EditRecords()
        Try
            TicketId = Me.grd.CurrentRow.Cells("TicketId").Value.ToString
            txtTicketNo.Text = Me.grd.CurrentRow.Cells("TicketNo").Value.ToString
            'rafay
            dtpTicketDate.Value = Me.grd.CurrentRow.Cells("TicketDate").Value.ToString
            If Me.cmbSerialNo.Rows.Count > 0 Then cmbSerialNo.Rows(0).Activate()
            cmbContractNo.SelectedValue = Me.grd.CurrentRow.Cells("ContractId").Value.ToString
            'Murtza
            cmbSaleorderNo.SelectedValue = Me.grd.CurrentRow.Cells("SaleOrderId").Value
            'Murtaza
            cmbSerialNo.Text = Me.grd.CurrentRow.Cells("SerialNo").Value.ToString
            cmbSerialNo_ValueChanged(Nothing, Nothing)
            'cmbContractNo_Leave(Nothing, Nothing)
            'cmbContractNo.SelectedValue = Me.grd.CurrentRow.Cells("ContractId").Value.ToString
            txtCallerName.Text = Me.grd.CurrentRow.Cells("CallerName").Value.ToString
            txtSite.Text = Me.grd.CurrentRow.Cells("Site").Value.ToString
            'rafay:Add Company Name and End Customer
            txtCompanyName.Text = Me.grd.CurrentRow.Cells("CompanyName").Value.ToString
            txtCustomerName.Text = Me.grd.CurrentRow.Cells("CustomerName").Value.ToString
            'rafay:task end
            cmbContactVia.Text = Me.grd.CurrentRow.Cells("ContactVia").Value.ToString
            dtpContactTime.Value = Me.grd.CurrentRow.Cells("ContactTime").Value.ToString
            cmbSeverity.Text = Me.grd.CurrentRow.Cells("Severity").Value.ToString
            cmbStatus.Text = Me.grd.CurrentRow.Cells("Status").Value.ToString
            status = Me.grd.CurrentRow.Cells("Status").Value.ToString
            txtTicketHistory.Text = Me.grd.CurrentRow.Cells("TicketHistory").Value.ToString
            chkPartUsed.Checked = Me.grd.CurrentRow.Cells("PartUsed").Value.ToString
            ''ChkBatteriesIncluded.Checked = Me.grd.CurrentRow.Cells("ChkBoxBatteriesIncluded").Value
            'rafay
            '       txtserialpartno.Text = Me.grdCreditDetail.CurrentRow.Cells("Partno").Value.ToString
            'rafay
            'Dim strSQL As String = "Select Employee_ID from tbldefEmployee where Em" Me.grd.CurrentRow.Cells("EngineerAssigned").Value.ToString
            'Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            'Dim strIDs As String = String.Empty
            'Me.lstEngineerAssigned.DeSelect()
            'If dt.Rows.Count > 0 Then

            '    For Each r As DataRow In dt.Rows
            '        If strIDs.Length = 0 Then
            '            strIDs = r.Item(0)
            '        Else
            '            strIDs = strIDs & "," & r.Item(0)
            '        End If
            '    Next
            'End If
            lstEngineerAssigned.DeSelect()
            Me.lstEngineerAssigned.SelectItemsByIDs(Me.grd.CurrentRow.Cells("EngineerAssigned").Value.ToString)
            txtInitialProblem.Text = Me.grd.CurrentRow.Cells("InitialProblem").Value.ToString
            GetAllRecords("TicketDetail")
            Me.btnSave.Text = "&Update"
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GetCommaSeparatedValues(ByVal dt As DataTable) As String
        Dim ReturnValue As String = ""
        Dim Counter As Integer = 0

        For Each row As DataRow In dt.Rows
            If Counter = 0 Then
                ReturnValue = "" & row.Item("Employee_Name") & ""
            Else
                ReturnValue += ", " & row.Item("Employee_Name") & ""
            End If
            Counter += 1
        Next

        Return ReturnValue

    End Function

    Private Function GetCommaSeparatedValuesPNUSED(ByVal dt As DataTable) As String
        Dim ReturnValue As String = ""
        Dim Counter As Integer = 0

        For Each row As DataRow In dt.Rows
            If Counter = 0 Then
                ReturnValue = "" & row.Item("ArticleCode") & ""
            Else
                ReturnValue += ", " & row.Item("ArticleCode") & ""
            End If
            Counter += 1
        Next

        Return ReturnValue

    End Function



    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New TicketOpeningBE
            If Me.btnSave.Text = "&Save" Then
                objModel.TicketId = 0
            Else
                objModel.TicketId = Val(Me.grd.CurrentRow.Cells("TicketId").Value.ToString)
            End If
            objModel.TicketNo = txtTicketNo.Text
            objModel.TicketDate = dtpTicketDate.Value
            objModel.SerialNo = Me.cmbSerialNo.Text
            objModel.CallerName = Me.txtCallerName.Text
            objModel.Site = Me.txtSite.Text
            'rafay
            objModel.CustomerName = Me.txtCustomerName.Text
            objModel.CompanyName = Me.txtCompanyName.Text
            objModel.ContractId = Me.cmbContractNo.SelectedValue
            'Murtaza
            objModel.SaleOrderId = Me.cmbSaleorderNo.SelectedValue
            'Murtaza
            objModel.Brand = Me.txtBrand.Text
            objModel.ModelNo = Me.txtModelNo.Text
            'rafay 12-4-22
            objModel.ChkBoxBatteriesIncluded = ChkBatteriesIncluded.Checked
            'rafay
            objModel.ContactVia = Me.cmbContactVia.Text
            objModel.ContactTime = Me.dtpContactTime.Value
            objModel.Severity = Me.cmbSeverity.Text
            objModel.Status = Me.cmbStatus.Text
            'Dim listtext As String = ModGlobel.GetRowValuesIntoString(_SearchDt, GridColumns.CUSTOMER)
            objModel.EngineerAssigned = Me.lstEngineerAssigned.SelectedIDs
            objModel.InitialProblem = Me.txtInitialProblem.Text
            Dim strticketHistory As String = String.Empty
            Dim str10 As String = String.Empty
            'If Me.cmbTopic.Text <> CurrentTopic Then
            '    str = "[" & Me.cmbTopic.Text & "]"
            'End If
            str10 += Chr(10) & "[" & LoginUserName & ":" & Date.Now & "]"
            strticketHistory = str10 & Chr(10) & Me.txtTicketHistory.Text
            objModel.TicketHistory = strticketHistory
            objModel.PartUsed = chkPartUsed.Checked
            list = New List(Of TicketOpeningDetailBE)
            For i As Integer = 0 To grdCreditDetail.RowCount - 1
                Dim Detail As New TicketOpeningDetailBE
                If Me.btnSave.Text = "&Save" Then
                    Detail.TicketId = 0
                    Detail.TicketDetailId = 0
                Else
                    objModel.TicketId = Val(Me.grd.CurrentRow.Cells("TicketId").Value.ToString)
                End If
                Detail.TicketDetailId = grdCreditDetail.GetRows(i).Cells("TicketDetailId").Value
                If grdCreditDetail.GetRows(i).Cells("TicketDetailId").Value = 0 Then
                    If grdCreditDetail.GetRows(i).Cells("OnsiteEngineer").Value.ToString <> "" Then
                        Dim str As String = "select Employee_Name from tbldefemployee where Employee_Name in (" & grdCreditDetail.GetRows(i).Cells("OnsiteEngineer").Value.ToString & ")"
                        Dim dt As DataTable = GetDataTable(str)
                        Dim onsiteengineer As String = GetCommaSeparatedValues(dt)
                        Detail.OnsiteEngineer = onsiteengineer
                    End If
                    Detail.TimeOnSite = grdCreditDetail.GetRows(i).Cells("TimeOnSite").Value.ToString
                    Detail.ActivityStart = grdCreditDetail.GetRows(i).Cells("ActivityStart").Value.ToString
                    Detail.ActivityEnd = grdCreditDetail.GetRows(i).Cells("ActivityEnd").Value.ToString
                    Detail.ActivityPerformed = grdCreditDetail.GetRows(i).Cells("ActivityPerformed").Value.ToString
                    Detail.TicketState = grdCreditDetail.GetRows(i).Cells("TicketState").Value.ToString
                    Detail.Escalation = grdCreditDetail.GetRows(i).Cells("Escalation").Value.ToString
                    If grdCreditDetail.GetRows(i).Cells("PNUsed").Value.ToString <> "" Then
                        Dim str1 As String = "select ArticleCode from articledefview where ArticleCode in (" & grdCreditDetail.GetRows(i).Cells("PNUsed").Value.ToString & ")"
                        Dim dt1 As DataTable = GetDataTable(str1)
                        Dim pnused As String = GetCommaSeparatedValuesPNUSED(dt1)
                        Detail.PNUsed = pnused
                    End If

                    Detail.Partno = grdCreditDetail.GetRows(i).Cells("Partno").Value.ToString
                    Detail.Quantity = grdCreditDetail.GetRows(i).Cells("Quantity").Value.ToString
                    Detail.PartDescription = grdCreditDetail.GetRows(i).Cells("PartDescription").Value.ToString
                    Detail.FaultyPartSN = grdCreditDetail.GetRows(i).Cells("FaultyPartSN").Value.ToString
                    list.Add(Detail)
                Else
                    'Dim str As String = "select Employee_Name from tbldefemployee where Employee_Name in (" & grdCreditDetail.GetRows(i).Cells("OnsiteEngineer").Value.ToString & ")"
                    'Dim dt As DataTable = GetDataTable(str)
                    'Dim onsiteengineer As String = GetCommaSeparatedValues(dt)
                    Detail.OnsiteEngineer = grdCreditDetail.GetRows(i).Cells("OnsiteEngineer").Value.ToString
                    Detail.TimeOnSite = grdCreditDetail.GetRows(i).Cells("TimeOnSite").Value
                    Detail.ActivityStart = grdCreditDetail.GetRows(i).Cells("ActivityStart").Value
                    Detail.ActivityEnd = grdCreditDetail.GetRows(i).Cells("ActivityEnd").Value
                    Detail.ActivityPerformed = grdCreditDetail.GetRows(i).Cells("ActivityPerformed").Value.ToString
                    Detail.TicketState = grdCreditDetail.GetRows(i).Cells("TicketState").Value.ToString
                    Detail.Escalation = grdCreditDetail.GetRows(i).Cells("Escalation").Value.ToString
                    'Dim str1 As String = "select ArticleCode from articledefview where ArticleCode in (" & grdCreditDetail.GetRows(i).Cells("PNUsed").Value.ToString & ")"
                    'Dim dt1 As DataTable = GetDataTable(str1)
                    'Dim pnused As String = GetCommaSeparatedValuesPNUSED(dt1)
                    Detail.PNUsed = grdCreditDetail.GetRows(i).Cells("PNUsed").Value.ToString
                    Detail.Partno = grdCreditDetail.GetRows(i).Cells("Partno").Value.ToString
                    Detail.Quantity = Val(grdCreditDetail.GetRows(i).Cells("Quantity").Text)
                    Detail.PartDescription = grdCreditDetail.GetRows(i).Cells("PartDescription").Value.ToString
                    Detail.FaultyPartSN = grdCreditDetail.GetRows(i).Cells("FaultyPartSN").Value.ToString
                    list.Add(Detail)

                End If

            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnSave.Enabled = True
                'Rafay:give rights to admin to view print ,export,choosefielder 
                Me.CtrlGrdBar3.mGridPrint.Enabled = True
                Me.CtrlGrdBar3.mGridExport.Enabled = True
                Me.CtrlGrdBar3.mGridChooseFielder.Enabled = True
                DoHaveDeleteRights = True
                'Rafay:Task End
                Exit Sub
            End If
            Me.Visible = False
            Me.btnSave.Enabled = False
            'Rafay:Task Start
            Me.CtrlGrdBar3.mGridPrint.Enabled = False
            Me.CtrlGrdBar3.mGridExport.Enabled = False
            Me.CtrlGrdBar3.mGridChooseFielder.Enabled = False
            'Rafay:Task End
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Save" Then
                    btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Update" Then
                    btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Delete" Then
                    DoHaveDeleteRights = True
                    'Rafay:Task Start
                ElseIf Rights.Item(i).FormControlName = "Export" Then
                    Me.CtrlGrdBar3.mGridExport.Enabled = True
                End If
                'Rafay:Task End
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If Condition = "TicketMaster" Then
                Dim str As String = "SELECT TicketId, TicketNo, TicketDate, SerialNo, CallerName,CustomerName,CompanyName, ContactVia, ContactTime, Severity, TicketMasterTable.Status, EngineerAssigned, InitialProblem,ContractId,Site,SaleOrderId,SalesOrderNo,TicketHistory,ISNULL(PartUsed,0) as PartUsed FROM TicketMasterTable LEFT join SalesOrderMasterTable ON TicketMasterTable.SaleOrderId=SalesOrderMasterTable.SalesOrderId ORDER BY TicketMasterTable.TicketId DESC"
                Dim dt As DataTable
                dt = GetDataTable(str)
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                Me.grd.RootTable.Columns("TicketId").Visible = False
                Me.grd.RootTable.Columns("EngineerAssigned").Visible = False
                Me.grd.RootTable.Columns("InitialProblem").Width = 300
                Me.grd.RootTable.Columns("ContractId").Visible = False
                'Murtaza
                Me.grd.RootTable.Columns("SaleOrderId").Visible = False
                'Murtaza
                Me.grd.RootTable.Columns("TicketNo").Width = 150
                Me.grd.RootTable.Columns("TicketHistory").Width = 150
                Me.grd.RootTable.Columns("ContactTime").FormatString = "h:mm:ss tt"
            ElseIf Condition = "TicketDetail" Then
                Dim str As String = "SELECT TicketDetailId, TicketId, OnsiteEngineer, TimeOnSite, ActivityStart, ActivityEnd, ActivityPerformed, TicketState, Escalation, PNUsed,Partno,ISNULL(Quantity,0) AS Quantity,PartDescription,FaultyPartSN FROM TicketDetailTable where TicketId = " & TicketId & " ORDER BY TicketDetailTable.TicketDetailId ASC"
                Dim dt As DataTable
                dt = GetDataTable(str)
                Me.grdCreditDetail.DataSource = dt
                Me.grdCreditDetail.RetrieveStructure()
                Me.grdCreditDetail.RootTable.Columns("TicketDetailId").Visible = False
                Me.grdCreditDetail.RootTable.Columns("TicketId").Visible = False
                Me.grdCreditDetail.RootTable.Columns("TimeOnSite").FormatString = "h:mm:ss tt"
                Me.grdCreditDetail.RootTable.Columns("ActivityStart").FormatString = "h:mm:ss tt"
                Me.grdCreditDetail.RootTable.Columns("ActivityEnd").FormatString = "h:mm:ss tt"
                If Me.grdCreditDetail.RootTable.Columns.Contains("Delete") = False Then
                    Me.grdCreditDetail.RootTable.Columns.Add("Delete")
                    Me.grdCreditDetail.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdCreditDetail.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdCreditDetail.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grdCreditDetail.RootTable.Columns("Delete").Key = "Delete"
                    Me.grdCreditDetail.RootTable.Columns("Delete").Caption = "Action"
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If cmbSerialNo.Value = 0 Then
                msg_Error("Please Write correct Serial No")
                Return False
            End If
            If dtpTicketDate.Value < dtpMachineEndDate.Value And status = "Open" Then
            Else
                If dtpMachineEndDate.Value < dtpsperverdate Then
                    msg_Error("Machine Maintenance Expired.")
                    Return False
                End If
                If dtpTicketDate.Value < dtpsperverdate Then
                    msg_Error("You cannot enter a ticket in previous date.")
                    Return False
                End If
            End If
            Dim str As String = "SELECT ISNULL(tblCustomer.Hold,0) as Hold FROM ContractMasterTable LEFT OUTER JOIN tblCustomer ON ContractMasterTable.CustomerId = tblCustomer.CustomerName WHERE  (ContractMasterTable.ContractNo = '" & cmbContractNo.Text & "')"
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0).ToString = "True" Then
                    ShowErrorMessage("Cannot approve this SO because this Customer is on Hold.")
                    Return False
                End If
            End If
            'rafay agar contractscreen say contract hold select hai tou phr error show kar day
            If CType(Me.cmbContractNo.SelectedItem, DataRowView).Row.Item("HoldCheckBox").ToString = "True" Then
                msg_Error("This contract is on hold. Please contact Accounts Department")
                Return False
            End If
            'If dtpTicketDate.Value < ServerDate() Then
            '    msg_Error("You cannot enter a ticket in previous date.")
            '    Return False
            'End If
            'rafay task end
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            TicketId = 0
            Me.btnSave.Text = "&Save"
            Me.txtTicketNo.Text = GetDocumentNo()
            dtpTicketDate.Value = Date.Now
            dtpsperverdate = TOServerDate()
            cmbContractNo.SelectedIndex = 0
            'Murtaza
            cmbSaleorderNo.SelectedIndex = 0
            'Murtaza
            If Me.cmbSerialNo.Rows.Count > 0 Then cmbSerialNo.Rows(0).Activate()
            txtCompanyName.Text = ""
            txtTicketHistory.Text = ""
            If chkPartUsed.Checked = True Then
                chkPartUsed.Checked = False
            End If
            txtCustomerName.Text = ""
            txtBrand.Text = ""
            txtModelNo.Text = ""
            txtAddress1.Text = ""
            txtAddress2.Text = ""
            txtCity.Text = ""
            txtCountry.Text = ""
            dtpContractStartDate.Value = Date.Now
            dtpContractEndDate.Value = Date.Now
            dtpMachineEndDate.Value = Date.Now
            dtpMachineStartDate.Value = Date.Now
            txtSignedMachine.Text = ""
            txtTypeMachine.Text = ""
            txtCallerName.Text = ""
            txtSite.Text = ""
            'rafay :task start
            txtCustomerName.Text = ""
            txtCompanyName.Text = ""
            txtTicketHistory.Text = ""
            ''companyinitials = ""
            'rafay:Task end
            'rfay
            If ChkBatteriesIncluded.Checked = True Then
                ChkBatteriesIncluded.Checked = False
            End If
            'rafay
            If chkTroubleShoot.Checked = True Then
                chkTroubleShoot.Checked = False
            End If
            cmbContactVia.SelectedIndex = 0
            dtpContactTime.Value = Date.Now
            cmbSeverity.SelectedIndex = 0
            cmbStatus.SelectedIndex = 0
            cmbapproval.SelectedIndex = 0
            cmbapproval.Visible = False
            txtapproval.Visible = False
            txtInitialProblem.Text = ""
            'cmbOnsiteEngineer.SelectedIndex = 0
            dtpTimeonSite.Value = Date.Now
            dtpActivityStart.Value = Date.Now
            dtpActivityEnd.Value = Date.Now
            If dtpTimeonSite.Checked = True Then
                dtpTimeonSite.Checked = False
            End If
            If dtpActivityStart.Checked = True Then
                dtpActivityStart.Checked = False
            End If
            If dtpActivityEnd.Checked = True Then
                dtpActivityEnd.Checked = False
            End If
            txtActivityPerformed.Text = ""
            txtserialpartno.Text = ""
            txtquantity.Text = ""
            txtpartdescription.Text = ""
            txtfaultypartsn.Text = ""
            cmbTicketState.SelectedIndex = 0
            cmbEscalation.SelectedIndex = 0
            Me.lstEngineerAssigned.DeSelect()
            Me.lstOnsiteEngineer.DeSelect()
            status = String.Empty
            Me.lstPNUsed.DeSelect()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New TicketOpeningDAL
            If IsValidate() = True Then
                If objDAL.Save(objModel) = True Then
                    If objDAL.SaveCreditDetail(list) = True Then
                        SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtTicketNo.Text, True)
                        Return True
                    Else
                        Return False
                    End If
                    Return True
                Else
                    Return False
                End If
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
            Try
                If IsValidate() = True Then
                    objDAL = New TicketOpeningDAL
                    FillModel()
                    objDAL.Update(objModel)
                    objDAL.UpdateCreditDetail(list)
                    SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtTicketNo.Text, True)
                    Return True
                End If
                Return False
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Private Sub frmPOSConfiguration_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Try
            If e.KeyCode = Keys.F4 Then
                If btnSave.Enabled = True Then
                    btnSave_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.F5 Then
                If btnSave.Enabled = True Then
                    btnRefresh_Click(Nothing, Nothing)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPOSConfiguration_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            GetAllRecords("TicketMaster")
            GetAllRecords("TicketDetail")
            Me.txtTicketNo.Text = GetDocumentNo()
            FillCombos("SerialNo")
            FillCombos("Contract")
            FillCombos("Engineer")
            FillCombos("PartNo")
            FillCombos("SaleOrderNo")
            cmbapproval.Visible = False
            txtapproval.Visible = False
            'Rafay
            dtpsperverdate = TOServerDate()
            'Rafay
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Rafay:Task Start : Rafay created this function to get serial no
    Function GetDocumentNo() As String
        Try
            Dim PreFix As String = ""
            Dim CompanyWisePrefix As Boolean = False ''companyinitials
            CompanyWisePrefix = Convert.ToBoolean(getConfigValueByType("ShowCompanyWisePrefix").ToString)
            If CompanyPrefix = "V-ERP (UAE)" Then
                'companyinitials = "UE"
                Return GetNextDocNo("TKT-" & Format(Me.dtpTicketDate.Value, "yy") & Me.dtpTicketDate.Value.Month.ToString("00") & Me.dtpTicketDate.Value.Day.ToString("00"), 4, "TicketMasterTable", "TicketNo")
                Return GetNextDocNo("TKT-" & Format(Me.dtpTicketDate.Value, "yy") & Me.dtpTicketDate.Value.Month.ToString("00") & Me.dtpTicketDate.Value.Day.ToString("00"), 4, "TicketMasterTable", "TicketNo")
            Else
                ''companyinitials = "PK"
                'Return GetNextDocNo("TKT-" & companyinitials & "-" & Format(Me.dtpTicketDate.Value, "yy") & Me.dtpTicketDate.Value.Month.ToString("00") & Me.dtpTicketDate.Value.Day.ToString("00"), 4, "TicketMasterTable", "TicketNo")
                Return GetNextDocNo("TKT-" & companyinitials & "-" & Format(Me.dtpTicketDate.Value, "yy") & Me.dtpTicketDate.Value.Month.ToString("00") & Me.dtpTicketDate.Value.Day.ToString("00"), 4, "TicketMasterTable", "TicketNo")
            End If
            If CompanyPrefix = "V-ERP (UAE)" Then
                'companyinitials = "UE"
                Return GetNextDocNo("TKT-" & Format(Me.dtpTicketDate.Value, "yy") & Me.dtpTicketDate.Value.Month.ToString("00") & Me.dtpTicketDate.Value.Day.ToString("00"), 4, "TicketMasterTable", "TicketNo")
            Else
                ''companyinitials = "PK"
                'Return GetNextDocNo("TKT-" & companyinitials & "-" & Format(Me.dtpTicketDate.Value, "yy") & Me.dtpTicketDate.Value.Month.ToString("00") & Me.dtpTicketDate.Value.Day.ToString("00"), 4, "TicketMasterTable", "TicketNo")
                Return GetNextDocNo("TKT-" & companyinitials & "-" & Format(Me.dtpTicketDate.Value, "yy") & Me.dtpTicketDate.Value.Month.ToString("00") & Me.dtpTicketDate.Value.Day.ToString("00"), 4, "TicketMasterTable", "TicketNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Me.btnSave.Text = "&Save" Then
                If Save() = True Then
                    If cmbSaleorderNo.SelectedValue > 0 And cmbStatus.Text = "Close" Then
                        SendAutoEmail()
                    End If
                    If chkTroubleShoot.Checked = True And cmbStatus.Text = "Open" Then
                        SendAutoEmailApproval()
                    End If
                    If cmbStatus.Text = "Close" Then
                        SendPartNoEmail()
                    End If
                    msg_Information(str_informSave)
                    btnNew_Click(Nothing, Nothing)
                End If
            Else
                If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                If Update1() = True Then
                    If cmbSaleorderNo.SelectedValue > 0 And cmbStatus.Text = "Close" Then
                        SendAutoEmail()
                    End If
                    If cmbStatus.Text = "Close" Then
                        SendPartNoEmail()
                    End If
                    msg_Information(str_informUpdate)
                    btnNew_Click(Nothing, Nothing)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            flag = False
            TicketId = 0
            FillCombos("SerialNo")
            FillCombos("Engineer")
            FillCombos("PartNo")
            FillCombos("Contract")
            ReSetControls()
            GetAllRecords("TicketMaster")
            GetAllRecords("TicketDetail")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs)
        Try
            Me.btnSave.Text = "Update"
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim dt As DataTable
            dt = CType(Me.grdCreditDetail.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr("TicketDetailId") = 0
            dr("OnsiteEngineer") = lstOnsiteEngineer.SelectedItems
            'dr("TimeOnSite") = dtpTimeonSite.Value.ToString("yyyy-M-d h:mm:ss tt")
            'dr("ActivityStart") = dtpActivityStart.Value.ToString("yyyy-M-d h:mm:ss tt")
            'dr("ActivityEnd") = dtpActivityEnd.Value.ToString("yyyy-M-d h:mm:ss tt")
            dr("TimeOnSite") = dtpTimeonSite.Value.ToString("yyyy-M-d h:mm:ss tt")
            dr("ActivityStart") = dtpActivityStart.Value.ToString("yyyy-M-d h:mm:ss tt")
            dr("ActivityEnd") = dtpActivityEnd.Value.ToString("yyyy-M-d h:mm:ss tt")
            dr("ActivityPerformed") = txtActivityPerformed.Text
            dr("TicketState") = cmbTicketState.Text
            dr("Escalation") = cmbEscalation.Text
            dr("PNUsed") = lstPNUsed.SelectedItems
            dr("Partno") = txtserialpartno.Text
            dr("Quantity") = Val(txtquantity.Text)
            dr("PartDescription") = txtpartdescription.Text
            dr("FaultyPartSN") = txtfaultypartsn.Text
            dt.Rows.Add(dr)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdCreditDetail_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdCreditDetail.ColumnButtonClick
        Try
            If DoHaveDeleteRights = True Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                If e.Column.Key = "Delete" Then
                    objDAL = New TicketOpeningDAL
                    objDAL.DeleteDetail(Val(Me.grdCreditDetail.GetRow.Cells("TicketDetailId").Value.ToString))
                    Me.grdCreditDetail.GetRow.Delete()
                    grdCreditDetail.UpdateData()
                End If
            Else
                ShowErrorMessage("You do not have rights to Delete")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            FillCombos("SerialNo")
            FillCombos("Contract")
            FillCombos("Engineer")
            FillCombos("PartNo")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub txtDiscountPer_KeyPress(sender As Object, e As KeyPressEventArgs)
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub cmbSerialNo_ValueChanged(sender As Object, e As EventArgs) Handles cmbSerialNo.ValueChanged
        Try
            If cmbSerialNo.Value > 0 Then
                Dim str1 As String
                str1 = "SELECT ContractDetailId,ContractId, OpportunityId, Brand, ModelNo, SerialNo, SLACoverage, Address, City, Province, Country, StartDate, EndDate, Type, UnitPrice, FilePath, SLA, SLAInterventionTime, SLAFixTime, OnsiteIntervention, ISNULL(Operator, '') as Operator FROM ContractDetailTable where ContractDetailId = " & cmbSerialNo.Value & ""
                Dim dt1 As DataTable
                dt1 = GetDataTable(str1)
                If flag = True And cmbContractNo.SelectedValue <> 0 Then

                Else
                    Me.cmbContractNo.SelectedValue = dt1.Rows(0).Item("ContractId")
                End If

                Me.txtBrand.Text = dt1.Rows(0).Item("Brand")
                Me.txtModelNo.Text = dt1.Rows(0).Item("ModelNo")
                Me.txtAddress1.Text = dt1.Rows(0).Item("Address")
                Me.txtCountry.Text = dt1.Rows(0).Item("Country")
                Me.txtCity.Text = dt1.Rows(0).Item("City")
                Me.dtpMachineStartDate.Value = dt1.Rows(0).Item("StartDate")
                Me.dtpMachineEndDate.Value = dt1.Rows(0).Item("EndDate")
                Me.txtTypeMachine.Text = dt1.Rows(0).Item("Type")
                Me.txtSignedMachine.Text = dt1.Rows(0).Item("SLACoverage")
                Me.txtOperator.Text = dt1.Rows(0).Item("Operator")
                Dim str As String
                'rafay 1modified query to add and show check box of batteries Included'
                str = "select customerid, StartDate, EndDate, isnull(EndCustomer, '') as EndCustomer, isnull(Employee, '') as Employee,ChkBoxBatteriesIncluded FROM ContractMasterTable WHERE ContractId = " & cmbContractNo.SelectedValue & ""
                Dim dt As DataTable
                dt = GetDataTable(str)
                txtCompanyName.Text = dt.Rows(0).Item("customerid")
                txtCustomerName.Text = dt.Rows(0).Item("EndCustomer")
                txtEmployee.Text = dt.Rows(0).Item("Employee")
                dtpContractStartDate.Value = dt.Rows(0).Item("StartDate")
                dtpContractEndDate.Value = dt.Rows(0).Item("EndDate")
                'rafay 12-4-22
                ChkBatteriesIncluded.Checked = IsDBNull(dt.Rows(0).Item("ChkBoxBatteriesIncluded"))
                'txtTicketHistory.Text = dt.Rows(0).Item("TicketHistory")
                'chkPartUsed.Checked = IsDBNull(dt.Rows(0).Item("PartUsed"))
                'Me.ChkBatteriesIncluded.Checked = cmbSerialNo.ActiveRow.Cells("BatteriesIncluded").Value.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSearch.KeyPress

    End Sub

    Private Sub cmbContractNo_Leave(sender As Object, e As EventArgs) Handles cmbContractNo.Leave
        Try
            If cmbContractNo.SelectedValue <> 0 Then
                flag = True
                'Dim str As String
                FillCombos("SerialNo")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbArticle_Leave(sender As Object, e As EventArgs) Handles cmbSaleorderNo.Leave
        Try
            If cmbSaleorderNo.SelectedValue <> 0 Then
                flag = True
                'Dim str As String
                FillCombos("SalesOrderNo")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt
            dv.RowFilter = "Code Like '%" & Me.txtSearch.Text & "%'"
            Me.lstPNUsed.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            'Catch ex As Exception
            '    ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs)

    End Sub
    'Rafay:Task Start:Add ctrlgrid bar for export
    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            ' CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    'Rafay:TaskEnd

    Private Sub grdCreditDetail_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdCreditDetail.FormattingRow

    End Sub

    Private Sub AMCToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles AMCToolStripMenuItem.Click
        'Abdul Rafay: show the report of amc (task given by adil bhai)
        AddRptParam("@id", Val(Me.grd.CurrentRow.Cells("TicketId").Value))
        ShowReport("Ticket")

    End Sub

    Private Sub AMCToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged

    End Sub

    Private Sub GrdStatus_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs)

    End Sub

    Private Sub grd_FormattingRow_1(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow

    End Sub

    Private Sub grd_RowDoubleClick_1(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grd.RowDoubleClick
        Try
            Me.btnSave.Text = "Update"
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    'Changes added by Murtaza for email generation on save (12/21/2022)
    Private Sub SendAutoEmail(Optional ByVal Activity As String = "")
        Try
            'Approved = cmbapproval.Text
            'Dim str As String
            'Dim senderemail As String
            GetTemplate("Ticket")
            If EmailTemplate.Length > 0 Then
                'If chkTroubleShoot.Checked = True And cmbStatus.Text = "Open" And btnSave.Text = "&Save" Then
                '    str = "select Email from tblUser where fullname like '%" & Approved & "%'"
                '    Dim dt As DataTable = GetDataTable(str)
                '    If dt.Rows.Count > 0 Then
                '        senderemail = dt.Rows(0).Item("Email").ToString
                '        UsersEmail = New List(Of String)
                '        UsersEmail.Add(senderemail)
                '            FormatStringBuilder(dtEmail)
                '            For Each _email As String In UsersEmail
                '                CreateOutLookMailaproval(_email)
                '                SaveEmailLog(txtTicketNo.Text, _email, "frmTicketOpening", Activity)
                '            Next
                '    End If
                'End If
                'If cmbSaleorderNo.SelectedValue > 0 And cmbStatus.Text = "Close" Then
                UsersEmail = New List(Of String)
                If Con.Database.Contains("Remms") Then
                    UsersEmail.Add("accounts@remmsit.com")
                Else
                    UsersEmail.Add("accounts@agriusit.com")
                End If
                FormatStringBuilder(dtEmail)
                For Each _email As String In UsersEmail
                    CreateOutLookMail(_email)
                    SaveEmailLog(txtTicketNo.Text, _email, "frmTicketOpening", Activity)
                Next
                'End If

            Else
                ShowErrorMessage("No email template is found for Ticket.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SendAutoEmailApproval(Optional ByVal Activity As String = "")
        Try
            Approved = cmbapproval.Text
            Dim str As String
            Dim senderemail As String
            GetTemplate("Ticket")
            If EmailTemplate.Length > 0 Then
                'If chkTroubleShoot.Checked = True And cmbStatus.Text = "Open" And btnSave.Text = "&Save" Then
                str = "select Email from tblUser where fullname like '%" & Approved & "%'"
                Dim dt As DataTable = GetDataTable(str)
                If dt.Rows.Count > 0 Then
                    senderemail = dt.Rows(0).Item("Email").ToString
                    UsersEmail = New List(Of String)
                    UsersEmail.Add(senderemail)
                    FormatStringBuilder(dtEmail)
                    For Each _email As String In UsersEmail
                        CreateOutLookMailaproval(_email)
                        SaveEmailLog(txtTicketNo.Text, _email, "frmTicketOpening", Activity)
                    Next
                End If
                'End If
                'If cmbSaleorderNo.SelectedValue > 0 And cmbStatus.Text = "Close" Then
                '    UsersEmail.Add("m.ahmad@agriusit.com")
                '    FormatStringBuilder(dtEmail)
                '    For Each _email As String In UsersEmail
                '        CreateOutLookMail(_email)
                '        SaveEmailLog(txtTicketNo.Text, _email, "frmTicketOpening", Activity)
                '    Next
                'End If

            Else
                ShowErrorMessage("No email template is found for Ticket.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SendPartNoEmail(Optional ByVal Activity As String = "")
        Dim Dr As DataRow
        Try
            GetpartnoTemplate("Ticket")
            If EmailTemplate.Length > 0 Then
                'Approved = cmbapproval.Text

                Dim str As String
                Dim senderemail As String
                str = "select * from ticketmastertable  where TicketNo ='" & txtTicketNo.Text & "'"
                Dim dt As DataTable = GetDataTable(str)
                For Each R1 As DataRow In dt.Rows
                    If dt.Rows.Count > 0 Then
                        EticketNo = R1.Item(0)
                    End If
                    Dim str1 As String
                    str1 = "select TicketId,OnsiteEngineer,ActivityPerformed,PNUsed from TicketDetailTable where TicketId=" & EticketNo & " and ISNULL(PNUsed,'')<>'' "
                    Dim dt1 As DataTable = GetDataTable(str1)
                    For Each Row1 As DataRow In dt1.Rows
                        Dr = dtEmail.NewRow
                        For Each col As String In AllFields
                            If Dr.Table.Columns.Contains(col) Then
                                Dr.Item(col) = Row1.Item(col).ToString
                            End If
                        Next
                        dtEmail.Rows.Add(Dr)
                    Next
                Next
                If dtEmail.Rows.Count > 0 Then
                    UsersEmail = New List(Of String)
                    If Con.Database.Contains("Remms") Then
                        UsersEmail.Add("m.fayyaz@remmsit.com")
                    Else
                        UsersEmail.Add("m.fayyaz@agriusit.com")
                    End If
                    For Each _email As String In UsersEmail
                        FormatStringBuilder(dtEmail)
                        CreateParnoOutLookMail(_email)
                        SaveEmailLog(txtTicketNo.Text, _email, "frmTicketOpening", Activity)
                    Next
                Else
                    Exit Sub
                End If
                End If
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
                ColumnName = Regex.Replace(column.ColumnName, Pattern, "$1 $2")
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
            'Table end.
            html.Append("</table>")
            html.Append(AfterFieldsElement)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMail(ByVal Email As String)
        Try
            Dim senderemail As String
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            Dim OutAccount As Outlook.Account
            Dim str As String
            str = "select Email from tblUser where User_ID = " & LoginUserId
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                senderemail = dt.Rows(0).Item("Email").ToString
                OutAccount = oApp.Session.Accounts(senderemail)
                mailItem.SendUsingAccount = OutAccount
            End If

            mailItem.Subject = txtTicketNo.Text & " Closed."
            mailItem.To = Email
            'Email = String.Empty
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            Dim myStr As String
            myStr = "Dear Accounts Team," & "<br>" & "<br>" & "This Ticket has been closed against (" & cmbSaleorderNo.Text & ") Sales Order for customer (" & txtCustomerName.Text & ")."
            mailItem.HTMLBody = myStr
            'mailItem.HTMLBody = ""
            'EmailBody = html.ToString
            mailItem.Send()
            Application.DoEvents()
            'End If
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateParnoOutLookMail(ByVal Email As String)
        Try
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            mailItem.Subject = "Ticket Closed Agrinst: " + txtTicketNo.Text

            mailItem.To = Email
            Email = String.Empty
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            Dim myStr As String
            'myStr = "Dear ," & "<br>" & "<br>" & "Please approve this for maintenance"
            myStr = "Dear Fayyaz ," & "<br>" & "<br>" & "This Ticket (" & txtTicketNo.Text & ") has been closed using following parts please verify accordingly."
            'mailItem.HTMLBody = myStr
            mailItem.HTMLBody = myStr + html.ToString + mailItem.HTMLBody
            EmailBody = html.ToString
            mailItem.Send()
            mailItem = Nothing
            oApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMailaproval(ByVal Email As String)
        Try
            Dim senderemail As String
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            Dim OutAccount As Outlook.Account
            Dim str As String
            str = "select Email from tblUser where User_ID = " & LoginUserId
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                senderemail = dt.Rows(0).Item("Email").ToString
                OutAccount = oApp.Session.Accounts(senderemail)
                mailItem.SendUsingAccount = OutAccount
            End If

            mailItem.Subject = txtTicketNo.Text & " Opened."
            mailItem.To = Email
            'Email = String.Empty
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            Dim myStr As String
            'myStr = "Dear ," & "<br>" & "<br>" & "Please approve this for maintenance"
            myStr = "Dear " & Approved & "," & "<br>" & "<br>" & "This Ticket (" & txtTicketNo.Text & ") has been opened with you approval against customer (" & txtCustomerName.Text & ")."
            mailItem.HTMLBody = myStr
            'mailItem.HTMLBody = ""
            'EmailBody = html.ToString
            mailItem.Send()
            Application.DoEvents()
            'End If
            mailItem = Nothing
            oApp = Nothing
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
                'EmailTemplate = EmailTemplate.Remove(i, j)
                AllFields = New List(Of String)

                dtEmail.Columns.Clear()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetpartnoTemplate(ByVal Title As String)
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

    'Changes added by Murtaza for email generation on save (12/21/2022)
    'change for server date get only date (01/31/2023)
    Public Function TOServerDate(Optional trans As OleDbTransaction = Nothing) As DateTime
        Try
            Dim dt As New DataTable
            dt = GetDataTable("select  Convert(date, getdate())", trans)
            dt.AcceptChanges()
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0).Item(0)) Then
                        GetServerDate = Convert.ToDateTime(dt.Rows(0).Item(0))
                    Else
                        GetServerDate = Date.Now
                    End If
                Else
                    GetServerDate = Date.Now
                End If
            Else
                GetServerDate = Date.Now
            End If
            Return GetServerDate
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbSaleorderNo_SelectedValueChanged(sender As Object, e As EventArgs) Handles chkTroubleShoot.CheckedChanged
        Try
            If chkTroubleShoot.Checked = True Then
                txtapproval.Visible = True
                cmbapproval.Visible = True
            Else
                txtapproval.Visible = False
                cmbapproval.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    'change for server date get only date (01/31/2023)
End Class