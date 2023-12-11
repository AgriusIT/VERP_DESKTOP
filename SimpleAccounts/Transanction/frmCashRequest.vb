''26-June-2014 TASK2704 Imran Ali Add new functionality cash request in erp.
''03-Jul-2014 TASKM60 Imran Ali Cash Request Approved 
''16-Jul-2014 TASK:2746 Imran Ali Cash Request Less On PO Validation (Ravi)
''09-Sep-2018 TASK:4439 Add the attachment option in the cash request and link to voucher approval or entry.
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net
Imports System.IO
Imports SBDal
Imports SBModel
Imports SBUtility


Public Class frmCashrequest

    Implements IGeneral
    Dim CashReq As CashRequestHeadBE
    Dim RequestID As Integer = 0I
    Dim IsOpenForm As Boolean = False
    ''TFS3113 : Abubakar Siddiq : This Variable is Added to check ApprovalProcessId ,if it is mapped against the document
    Dim ApprovalProcessId As Integer = 0
    Dim IsEditMode As Boolean = False
    ''TFS3113 : Abubakar Siddiq :End
    Dim arrFile As List(Of String) ''TFS4439
    Enum enmGridDetail
        coa_detail_id
        detail_code
        detail_title
        amount
        description
    End Enum
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = "Detail" Then
                For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                    If Me.grd.RootTable.Columns(c).Index <> enmGridDetail.amount AndAlso Me.grd.RootTable.Columns(c).Index <> enmGridDetail.description Then
                        Me.grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Next
            ElseIf Condition = "Master" Then
                Me.grdSaved.RootTable.Columns("Total_Amount").FormatString = "N"
                ''Making Attachment column of link type
                Me.grdSaved.RootTable.Columns("No_of_Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link ''TFS4439
                Me.grdSaved.RootTable.Columns("No_of_Attachment").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center ''TFS4439
                Me.grdSaved.AutoSizeColumns()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    Me.btnPrint.Enabled = False
                    CtrlGrdBar1.mGridExport.Enabled = False
                    CtrlGrdBar1.mGridPrint.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmCashRequest)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                UserPostingRights = GetUserPostingRights(LoginUserId)
                If UserPostingRights = True Then
                    Me.chkApproved.Visible = True
                Else
                    Me.chkApproved.Visible = False
                    Me.chkApproved.Checked = False
                End If
                GetSecurityByPostingUser(UserPostingRights, btnSave, btnDelete)
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                Me.chkApproved.Visible = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "GridPrint" Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Approved" Then
                        Me.chkApproved.Visible = True
                    End If
                Next
            End If

            If Mode = Utility.EnumDataMode.[New] Then
                Me.btnDelete.Enabled = False
                Me.btnDelete.Visible = False ''TFS3818
                Me.btnNew.Enabled = False
                Me.btnNew.Visible = False ''TFS3818
                Me.btnEdit.Enabled = True
                Me.btnEdit.Visible = True ''TFS3818
            ElseIf Mode = Utility.EnumDataMode.Edit Then
                Me.btnEdit.Enabled = False
                Me.btnEdit.Visible = False ''TFS3818
                Me.btnNew.Enabled = True
                Me.btnNew.Visible = True ''TFS3818
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            Me.grdSaved.UpdateData()
            CashReq = New CashRequestHeadBE
            CashReq.RequestId = Me.grdSaved.GetRow.Cells("RequestID").Value
            CashReq.RequestNo = Me.grdSaved.GetRow.Cells("RequestNo").Value
            CashReq.RequestDate = Me.grdSaved.GetRow.Cells("RequestDate").Value
            If New CashrequestDAL().Delete(CashReq) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty
            If Condition = "Project" Then
                strSQL = String.Empty
                strSQL = "Select CostCenterID, Name From tblDefCostCenter ORDER BY 2 ASC"
                FillDropDown(Me.cmbProject, strSQL)
            ElseIf Condition = "CMFADoc" Then
                strSQL = String.Empty
                'Before against task:M60
                'strSQL = "Select DocID, DocNo + '~' + Convert(varchar, DocDate, 102) as DocNo, IsNull(Approved,0) as Approved From CMFAMasterTable WHERE DocID Not IN(Select CMFADocId From CashRequestHead) AND Approved IN(1,0,NULL) "
                'Task:M60 get CMFA Approved 
                strSQL = "Select DocID, DocNo + '~' + Convert(varchar, DocDate, 102) as DocNo, IsNull(Approved,0) as Approved From CMFAMasterTable WHERE Approved=1 "
                'End Task:M60
                FillDropDown(Me.cmbCMFADocument, strSQL)
            ElseIf Condition = "Employee" Then
                strSQL = String.Empty
                strSQL = "select Employee_Id, Employee_Name +'-'+ Employee_Code as Employee_Name From tblDefEmployee Where Active = 1 "
                FillDropDown(Me.cmbEmployee, strSQL)
            ElseIf Condition = "Accounts" Then
                strSQL = String.Empty
                strSQL = "Select coa_detail_id as ID, detail_title as [Account Description], detail_code as [Account Code], sub_sub_title as [Account Head], Account_type as [Type] From vwCOADetail WHERE detail_title <> '' ORDER By detail_title ASC"
                FillUltraDropDown(Me.cmbAccounts, strSQL)
                Me.cmbAccounts.Rows(0).Activate()
                If Me.cmbAccounts.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbAccounts.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            CashReq = New CashRequestHeadBE
            CashReq.RequestId = RequestID
            CashReq.RequestNo = Me.txtRequestNo.Text
            CashReq.RequestDate = Me.dtpRequestDate.Value
            CashReq.Remarks = Me.txtRemarks.Text
            CashReq.ProjectId = IIf(Me.cmbProject.SelectedIndex > 0, Me.cmbProject.SelectedValue, 0)
            CashReq.EmpID = IIf(Me.cmbEmployee.SelectedIndex > 0, Me.cmbEmployee.SelectedValue, 0)
            CashReq.CMFADocId = IIf(Me.cmbCMFADocument.SelectedIndex > 0, Me.cmbCMFADocument.SelectedValue, 0)
            CashReq.UserId = LoginUserId
            CashReq.EntryDate = Now
            CashReq.Approved = Me.chkApproved.Checked
            CashReq.ApprovedDate = IIf(Me.chkApproved.Checked = True, Now, Nothing)
            CashReq.ModifyUserId = IIf(Me.btnSave.Text = "&Update", LoginUserId, 0)
            CashReq.ModifyDate = IIf(Me.btnSave.Text = "&Update", Now, Nothing)
            CashReq.Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            ''TASK TFS4439
            CashReq.ArrFile = arrFile
            CashReq.Source = Me.Name
            If Not getConfigValueByType("FileAttachmentPath").ToString = "Error" Then
                CashReq.AttachmentPath = getConfigValueByType("FileAttachmentPath").ToString
            Else
                CashReq.AttachmentPath = ""
            End If
            ''END TASK TFS4439
            CashReq.CashRequstDetail = New List(Of CashRequestDetailBE)
            Dim CashReqDt As CashRequestDetailBE
            Me.grd.UpdateData()
            For Each objRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                CashReqDt = New CashRequestDetailBE
                CashReqDt.coa_detail_id = Val(objRow.Cells("coa_detail_id").Value.ToString)
                CashReqDt.Amount = Val(objRow.Cells("Amount").Value.ToString)
                CashReqDt.Comments = objRow.Cells("Comments").Value.ToString
                CashReqDt.CostCenterId = Val(objRow.Cells("CostCenterId").Value.ToString)
                CashReqDt.EmployeeId = Val(objRow.Cells("EmployeeID").Value.ToString)
                CashReq.CashRequstDetail.Add(CashReqDt)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Dim strSQL As String = String.Empty
            If Condition = "Master" Then
                strSQL = String.Empty
                'strSQL = "SELECT dbo.CashRequestHead.RequestId, IsNull(dbo.CashRequestHead.CMFADocId,0) as CMFADocId, ISNULL(dbo.CashRequestHead.ProjectId,0) as ProjectId, dbo.CashRequestHead.RequestNo, " _
                '      & " dbo.CashRequestHead.RequestDate, dbo.CashRequestHead.Remarks, dbo.CashRequestHead.Total_Amount, dbo.CMFAMasterTable.DocNo AS [CMFA No], " _
                '      & " dbo.CMFAMasterTable.DocDate AS [CMFA Date], dbo.tblDefCostCenter.Name AS Project, IsNull(dbo.CashRequestHead.Approved,0) as Approved, tblUser.User_Name " _
                '      & " FROM dbo.CashRequestHead LEFT OUTER JOIN " _
                '      & " dbo.tblDefCostCenter ON dbo.CashRequestHead.ProjectId = dbo.tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
                '      & " dbo.CMFAMasterTable ON dbo.CashRequestHead.CMFADocId = dbo.CMFAMasterTable.DocId LEFT OUTER JOIN tblUser On tblUser.User_Id = dbo.CashRequestHead.UserId"
                ''TFS4439 : getting Column Attachment from DocumentAttachment on the basis of DocId
                strSQL = "SELECT dbo.CashRequestHead.RequestId, IsNull(dbo.CashRequestHead.CMFADocId,0) as CMFADocId, ISNULL(dbo.CashRequestHead.ProjectId,0) as ProjectId, dbo.CashRequestHead.EmpID, dbo.CashRequestHead.RequestNo, " _
                                      & " dbo.CashRequestHead.RequestDate, dbo.CashRequestHead.Remarks, dbo.CashRequestHead.Total_Amount, dbo.CMFAMasterTable.DocNo AS [CMFA No], " _
                                      & " dbo.CMFAMasterTable.DocDate AS [CMFA Date], dbo.tblDefCostCenter.Name AS Project, tblDefEmployee.Employee_Name, tblUser.User_Name ,IsNull(dbo.CashRequestHead.Approved,0) as Approved, IsNull([No_of_Attachment],0) as  [No_of_Attachment]" _
                                      & " FROM dbo.CashRequestHead LEFT OUTER JOIN " _
                                      & " tblDefEmployee ON CashRequestHead.EmpID = tblDefEmployee.Employee_ID LEFT OUTER JOIN " _
                                      & " dbo.tblDefCostCenter ON dbo.CashRequestHead.ProjectId = dbo.tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
                                      & " dbo.CMFAMasterTable ON dbo.CashRequestHead.CMFADocId = dbo.CMFAMasterTable.DocId LEFT OUTER JOIN tblUser On tblUser.User_Id = dbo.CashRequestHead.UserId " _
                                      & " LEFT OUTER JOIN(Select Count(*) as [No_of_Attachment], DocId From DocumentAttachment WHERE Source='" & Me.Name & "' Group By DocId) Att On Att.DocId =  dbo.CashRequestHead.RequestId Order By dbo.CashRequestHead.RequestId DESC"

                Dim objDtMain As New DataTable
                objDtMain = GetDataTable(strSQL)
                For Each r As DataRow In objDtMain.Rows
                    r.BeginEdit()
                    r("User_Name") = Decrypt(r("User_Name").ToString)
                    r.EndEdit()
                Next
                objDtMain.AcceptChanges()
                Me.grdSaved.DataSource = objDtMain
                Me.grdSaved.RetrieveStructure()

                Me.grdSaved.RootTable.Columns("RequestId").Visible = False
                Me.grdSaved.RootTable.Columns("CMFADocId").Visible = False
                Me.grdSaved.RootTable.Columns("ProjectId").Visible = False
                Me.grdSaved.RootTable.Columns("RequestId").Visible = False
                ApplyGridSettings("Master")

            ElseIf Condition = "Detail" Then
                strSQL = String.Empty
                strSQL = "SELECT CRDt.coa_detail_id, COA.detail_code, COA.detail_title, CRDt.Amount, CRDt.Comments, CRDt.CostCenterId, tblDefCostCenter.Name AS CostCenter, CRDt.EmployeeID, tblDefEmployee.Employee_Name FROM CashRequestDetail AS CRDt INNER JOIN vwCOADetail AS COA ON CRDt.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN tblDefEmployee ON CRDt.EmployeeID = tblDefEmployee.Employee_ID LEFT OUTER JOIN tblDefCostCenter ON CRDt.CostCenterId = tblDefCostCenter.CostCenterID WHERE     (CRDt.RequestID = " & RequestID & ") ORDER BY CRDt.RequestDetailID Asc"
                Dim objDt As New DataTable
                objDt = GetDataTable(strSQL)
                objDt.AcceptChanges()
                Me.grd.DataSource = objDt
                ApplyGridSettings("Detail")
            ElseIf Condition = "CMFADetail" Then
                strSQL = String.Empty
                strSQL = "SELECT dbo.CMFAExpenseTable.coa_detail_id, dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title, dbo.CMFAExpenseTable.Amount, '' AS Comments " _
                        & " FROM dbo.CMFAExpenseTable INNER JOIN dbo.vwCOADetail ON dbo.CMFAExpenseTable.coa_detail_id = dbo.vwCOADetail.coa_detail_id WHERE  DocID=" & Me.cmbCMFADocument.SelectedValue & " AND dbo.CMFAExpenseTable.Amount <> 0"
                Dim objDt As New DataTable
                objDt = GetDataTable(strSQL)
                objDt.AcceptChanges()
                Me.grd.DataSource = objDt
                ApplyGridSettings("Detail")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtRequestNo.Text = String.Empty Then
                ShowErrorMessage("Please define document serial no.")
                Me.txtRequestNo.Focus()
                Return False
            End If
            If Me.dtpRequestDate.Value = Date.MinValue Then
                ShowErrorMessage("Please request date is not valid.")
                Me.dtpRequestDate.Focus()
                Return False
            End If
            If Me.grd.RowCount <= 0 Then
                ShowErrorMessage("Record not add in grid.")
                Me.grd.Focus()
                Return False
            End If
            'Task:2746 Cash Request Exceeded Validation 
            If Me.cmbCMFADocument.SelectedIndex > 0 Then
                Dim dt As New DataTable
                dt = GetDataTable("Select IsNull(ProjectedExpAmount,0) as Amount From CMFAMasterTable WHERE DocId=" & Me.cmbCMFADocument.SelectedValue & "")
                If dt IsNot Nothing Then
                    If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                        If Val(dt.Rows(0).Item(0).ToString) < Me.grd.GetTotal(Me.grd.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) Then
                            ShowErrorMessage("Cash request amount exceeded from Projected expense amount in CMFA.")
                            Me.grd.Focus()
                            Return False
                        End If
                    End If
                End If
            End If
            'End Task:2746
            FillModel()
            'start task 3113 Added by abubakar Siddiq
            If IsEditMode = True Then
                If ValidateApprovalProcessMapped(Me.txtRequestNo.Text.Trim) Then
                    If ValidateApprovalProcessInProgress(Me.txtRequestNo.Text.Trim) Then
                        arrFile = New List(Of String)
                        msg_Error("Document is in Approval Process") : Return False : Exit Function
                    End If
                End If
            End If
            'end task 3113, Added by Abubakar Siddiq


            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try

            Me.btnSave.Text = "&Save"
            RequestID = 0I
            Me.txtRequestNo.Text = New CashrequestDAL().GetDocumentNo("CRq-" & Me.dtpRequestDate.Value.ToString("yy") & "-")
            Me.dtpRequestDate.Value = Date.Now
            If Not Me.cmbProject.SelectedIndex = -1 Then Me.cmbProject.SelectedIndex = 0
            If Not Me.cmbEmployee.SelectedIndex = -1 Then Me.cmbEmployee.SelectedIndex = 0
            If Not Me.cmbCMFADocument.SelectedIndex = -1 Then Me.cmbCMFADocument.SelectedIndex = 0
            Me.txtRemarks.Text = String.Empty
            Me.chkApproved.Checked = False
            If Me.cmbAccounts.ActiveRow IsNot Nothing Then Me.cmbAccounts.Rows(0).Activate()
            Me.txtAmount.Text = String.Empty
            Me.txtDescription.Text = String.Empty
            Me.dtpRequestDate.Focus()
            GetAllRecords("Master")
            GetAllRecords("Detail")
            ApplySecurity(Utility.EnumDataMode.[New])
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.btnAdd.Enabled = True
            Me.chkApproved.Enabled = True


            'Abubakar Siddiq : TFS3113 : Enable Approval History button only in Eidt Mode
            If IsEditMode = True Then
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
            Else
                Me.btnApprovalHistory.Visible = False
            End If
            'Abubakar Siddiq : TFS3113 : End
            ''Abubakar Siddiq :TFS3113 :Making Approval Button Enable in Edit Mode
            If Not getConfigValueByType("CashRequestApproval") = "Error" Then
                ApprovalProcessId = Val(getConfigValueByType("CashRequestApproval"))
            End If
            If ApprovalProcessId = 0 Then
                Me.chkApproved.Visible = True
                Me.chkApproved.Enabled = True
            Else
                Me.chkApproved.Visible = False
                Me.chkApproved.Enabled = False
                Me.chkApproved.Checked = False
            End If
            ''Abubakar Siddiq :TFS3113 :End

            ''Start TFS4439
            arrFile = New List(Of String)
            Me.btnAttachments.Text = "Attachment (" & arrFile.Count & ")"
            ''End TFS4439

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Dim requestId As Integer = 0

            If New CashrequestDAL().Add(CashReq, requestId) = True Then
                ''Start TFS3113
                ''insert Approval Log
                SaveApprovalLog(EnumReferenceType.CashRequest, requestId, Me.txtRequestNo.Text.Trim, Me.dtpRequestDate.Value.Date, "Cash Request ," & cmbCMFADocument.Text & "", Me.Name)
                ''End TFS3113
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try

            If New CashrequestDAL().Modify(CashReq) = True Then
                ''Start TFS3113
                If ValidateApprovalProcessMapped(Me.txtRequestNo.Text.Trim, Me.Name) Then
                    If ValidateApprovalProcessIsInProgressAgain(Me.txtRequestNo.Text.Trim, Me.Name) = False Then
                        SaveApprovalLog(EnumReferenceType.CashRequest, RequestID, Me.txtRequestNo.Text.Trim, Me.dtpRequestDate.Value.Date, "Cash Request ," & cmbCMFADocument.Text & "", Me.Name)
                    End If
                End If
                ''End TFS3113
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmCashrequest_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            If e.KeyCode = Keys.F14 Then
                If Me.btnSave.Enabled = True Then
                    Me.btnSave_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Delete Then
                If Me.btnDelete.Enabled = True Then
                    If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                        Me.btnDelete_Click(Nothing, Nothing)
                    End If
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                Me.btnNew_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.F2 Then
                Me.btnEdit_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmCashrequest_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            IsOpenForm = True
            ApplySecurity(Utility.EnumDataMode.[New])
        Catch ex As Exception

        End Try
     
    End Sub

    Private Sub frmCashrequest_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            FillCombos("Employee")
            FillCombos("Project")
            FillCombos("CMFADoc")
            FillCombos("Accounts")
            IsOpenForm = True
            ReSetControls()

            'TFS3360: Aashir: Added select/contain filters on account feilds in all transaction screens
            UltraDropDownSearching(cmbAccounts, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub EditRecords(Optional ByVal Condition As String = "")
        Try

            IsEditMode = True

            RequestID = Me.grdSaved.GetRow.Cells("RequestId").Value.ToString
            Me.txtRequestNo.Text = Me.grdSaved.GetRow.Cells("RequestNo").Value.ToString
            Me.dtpRequestDate.Value = Me.grdSaved.GetRow.Cells("RequestDate").Value
            Me.cmbProject.SelectedValue = Me.grdSaved.GetRow.Cells("ProjectId").Value
            Me.cmbEmployee.SelectedValue = Me.grdSaved.GetRow.Cells("EmpID").Value
            Me.cmbCMFADocument.SelectedValue = Me.grdSaved.GetRow.Cells("CMFADocId").Value
            Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells("Remarks").Value.ToString
            Me.chkApproved.Checked = Me.grdSaved.GetRow.Cells("Approved").Value
            ' Me.btnAttachments.Text = "Attachment (" & Me.grdSaved.GetRow().Cells("No_of_Attachment").Value.ToString & ")" ''TFS4439
            GetAllRecords("Detail")
            Me.btnSave.Text = "&Update"
            ''Start TFS4439
            arrFile = New List(Of String)
            Dim intCountAttachedFiles As Integer = 0I
            If Me.btnSave.Text <> "&Save" Then
                If Me.grdSaved.RowCount > 0 Then
                    intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No_of_Attachment").Value)
                    Me.btnAttachments.Text = "Attachment (" & intCountAttachedFiles & ")"
                End If
            End If
            ''End TFS4439
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            ApplySecurity(Utility.EnumDataMode.Edit)
            Me.dtpRequestDate.Focus()

            ''Abubakar Siddiq :TFS3113 :Making Approval Button Enable in Edit Mode
            Me.btnApprovalHistory.Visible = True
            Me.btnApprovalHistory.Enabled = True
            ''Abubakar Siddiq :TFS3113 :End

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grd_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If e.Column.Key = "Column1" Then
                Me.grd.GetRow.Delete()
                Me.grd.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If Me.cmbAccounts.ActiveRow Is Nothing Then
                ShowErrorMessage("Please select account.")
                Me.cmbAccounts.Focus()
                Exit Sub
            End If
            If Val(Me.txtAmount.Text) <= 0 Then
                ShowErrorMessage("Please enter amount.")
                Me.txtAmount.Focus()
                Exit Sub
            End If


            Me.grd.UpdateData()
            Dim objDt As New DataTable
            objDt = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow
            dr = objDt.NewRow
            dr(0) = Me.cmbAccounts.Value
            dr(1) = Me.cmbAccounts.ActiveRow.Cells("Account Code").Value.ToString
            dr(2) = Me.cmbAccounts.ActiveRow.Cells("Account Description").Value.ToString
            dr(3) = Val(Me.txtAmount.Text)
            dr(4) = Me.txtDescription.Text
            If Me.cmbProject.SelectedValue > 0 Then
                dr(5) = Me.cmbProject.SelectedValue
                dr(6) = Me.cmbProject.Text
            Else
                dr(5) = 0
                dr(6) = ""
            End If
            If Me.cmbEmployee.SelectedValue > 0 Then
                dr(7) = Me.cmbEmployee.SelectedValue
                dr(8) = Me.cmbEmployee.Text
            Else
                dr(7) = 0
                dr(8) = ""
            End If
            objDt.Rows.Add(dr)
            objDt.AcceptChanges()

            Me.txtAmount.Text = String.Empty
            Me.cmbAccounts.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
            Me.cmbProject.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbAccounts_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccounts.Enter
        Try
            Me.cmbAccounts.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                Else

                    Dim strSQL As String = String.Empty
                    strSQL = "Select Count(*) From tblVoucher WHERE CashRequestID=" & Me.grdSaved.GetRow.Cells("RequestId").Value.ToString & ""
                    Dim dt As New DataTable
                    dt = GetDataTable(strSQL)
                    If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                        Throw New Exception("you cannot update this record, because it dependent exist")
                    End If

                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Dim strSQL As String = String.Empty
            strSQL = "Select Count(*) From tblVoucher WHERE CashRequestID=" & Me.grdSaved.GetRow.Cells("RequestId").Value.ToString & ""
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                Throw New Exception("you cannot delete this record, because it dependent exist")
            End If

            ''Start TFS3113 : Ayesha Rehman : 09-04-2018
            If IsEditMode = True Then
                If ValidateApprovalProcessMapped(Me.txtRequestNo.Text.Trim) Then
                    If ValidateApprovalProcessInProgress(Me.txtRequestNo.Text.Trim) Then
                        msg_Error("Document is in Approval Process ") : Exit Sub
                    End If
                End If
            End If
            ''End TFS3113
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Public Function Get_All(ByVal RequestNo As String)
        Try
            frmCashrequest_Shown(Nothing, Nothing)
            Get_All = Nothing
            If Not RequestNo.Length > 0 Then Exit Try
            'If IsOpenForm = True Then
            If RequestNo.Length > 0 Then
                Dim str As String = "Select * from CashRequestHead where RequestNo =N'" & RequestNo & "'"
                Dim dt As DataTable = GetDataTable(str)
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Dim flag As Boolean = False
                        flag = Me.grdSaved.FindAll(Me.grdSaved.RootTable.Columns("RequestNo"), Janus.Windows.GridEX.ConditionOperator.Equal, RequestNo)
                        'If flag = True Then
                        Me.grdSaved_DoubleClick(Nothing, Nothing)
                    End If
                Else
                    Exit Function
                End If
            Else
                Exit Function
                'End If
            End If
            'IsDrillDown = False
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Private Sub cmbCMFADocument_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCMFADocument.SelectedIndexChanged
    '    Try
    '        If IsOpenForm = False Then Exit Sub
    '        'Dim str As String = CType(Me.cmbCMFADocument.SelectedItem, DataRowView).Row.Item("Approved").ToString
    '        'If str.Length > 0 Then
    '        '    Me.chkApproved.Checked = str
    '        'Else
    '        '    Me.chkApproved.Checked = False
    '        'End If
    '        'chkApproved.Enabled = False
    '        GetAllRecords("CMFADetail")
    '        If Me.chkApproved.Checked = True Then
    '            If Me.cmbCMFADocument.SelectedIndex > 0 Then
    '                Me.grd.RootTable.Columns("Amount").EditType = Janus.Windows.GridEX.EditType.NoEdit
    '                Me.btnAdd.Enabled = False
    '            Else
    '                Me.btnAdd.Enabled = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try

    'End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I

            id = Me.cmbProject.SelectedIndex
            FillCombos("Project")
            Me.cmbProject.SelectedIndex = id

            id = Me.cmbEmployee.SelectedIndex
            FillCombos("Employee")
            Me.cmbEmployee.SelectedIndex = id

            id = Me.cmbCMFADocument.SelectedIndex
            FillCombos("CMFADoc")
            Me.cmbCMFADocument.SelectedIndex = id

            id = Me.cmbAccounts.ActiveRow.Cells(0).Value
            FillCombos("Accounts")
            Me.cmbAccounts.Value = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'TAsk:M60 Amount In Numeric 
    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:M60

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub grdSaved_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try
            If e.Column.Key = "No_of_Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.Name
                frm._VoucherId = Val(Me.grdSaved.GetRow.Cells("RequestId").Value.ToString)
                frm.ShowDialog()
                GetAllRecords("Master")
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_LoadingRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdSaved.LoadingRow
        Try
            Dim MyGrdFormatStyle As New Janus.Windows.GridEX.GridEXFormatStyle
            If e.Row.Cells("Approved").Value = True Then
                MyGrdFormatStyle.BackColor = Color.Cyan
                e.Row.RowStyle = MyGrdFormatStyle
            Else
                MyGrdFormatStyle.BackColor = Color.White
                e.Row.RowStyle = MyGrdFormatStyle
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@RequestId", Val(Me.grdSaved.GetRow.Cells("RequestId").Value.ToString))
            ShowReport("rptCashRequest")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbAccounts_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbAccounts.KeyDown
        Try
            ''TFS1781 : Ayesha Rehman :Added for Selection of Customer or Vendor
            If e.KeyCode = Keys.F1 Then

                frmAccountSearch.AccountType = String.Empty
                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbAccounts.Value = frmAccountSearch.SelectedAccountId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub pnlHeader_Paint(sender As Object, e As PaintEventArgs) Handles pnlHeader.Paint

    End Sub

    Private Sub btnApprovalHistory_Click(sender As Object, e As EventArgs) Handles btnApprovalHistory.Click
        Try
            frmApprovalHistory.DocumentNo = Me.txtRequestNo.Text
            frmApprovalHistory.Source = Me.Name
            frmApprovalHistory.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Lsayouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Cash Request"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Lsayouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Cash Request"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanging(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        If e.Tab.Index = 1 Then
            Me.CtrlGrdBar1.Visible = False
            Me.btnAttachments.Visible = False
            Me.CtrlGrdBar2.Visible = True
        Else
            Me.CtrlGrdBar1.Visible = True
            Me.btnAttachments.Visible = True
            Me.CtrlGrdBar2.Visible = False
        End If
    End Sub

    Private Sub btnAttachments_Click(sender As Object, e As EventArgs) Handles btnAttachments.Click
        Try
            SetAttachments()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Setting Attachments
    Private Sub SetAttachments()
        Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty
            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
            "All files (.)|*.*"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a
                If Me.BtnSave.Text <> "&Save" Then
                    If Me.grdSaved.RowCount > 0 Then
                        intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No_of_Attachment").Value)
                    End If
                End If
                Me.btnAttachments.Text = "Attachment (" & arrFile.Count + intCountAttachedFiles & ")"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class