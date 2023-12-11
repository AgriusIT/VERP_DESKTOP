'Ali Faisal : Added LabTestRequest Form to save the Record on 16-June-2016
Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports SBUtility
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports System.Data.SqlClient

Public Class frmLabTestRequest

    Dim IsEditMode As Boolean = False
    Dim CurrentId As Integer
    Enum grdenm
        'Ali Faisal : Set Indexing on 16-June-2016
        TestReq_Detail_Id
        TestReq_ItemId
        TestReq_ItemCode
        TestReq_Item
        TestReq_Qty
        TestReq_Batch
        TestReq_NoofContainers
        TestReq_Comments
        TestReq_RetestDate
        TestReq_MfgDate
        TestReq_ExpDate
    End Enum
    Private Sub frmLabTestRequest_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            'Ali Faisal : System User friendly modification : to set short keys on 20-June-2016
            If e.KeyCode = Keys.F4 Then
                If btnSave.Enabled = True Then
                    btnSave_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then

                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If

            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                btnPrintForm_Click(Nothing, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.F5 Then
                btnRefresh_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Insert Then
                btnAdd_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.U AndAlso e.Alt Then
                If Me.btnSave.Text = "&Update" Then
                    If Me.btnSave.Enabled = False Then
                        RemoveHandler Me.btnSave.Click, AddressOf Me.btnSave_Click
                    End If
                End If
            End If
            If e.KeyCode = Keys.D AndAlso e.Alt Then
                If Me.btnDelete.Text = "&Delete" Then
                    If Me.btnDelete.Enabled = False Then
                        RemoveHandler Me.btnDelete.Click, AddressOf Me.btnDelete_Click
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmLabTestRequest_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            'Ali Faisal : System UserFriendly modification : to FillCombo and Refresh Controls at Form shown on 16-June-2016
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            FillComboBox("Vendor")
            FillComboBox("PO")
            FillComboBox("GRN")
            FillComboBox("Item")
            FillComboBox("Department")
            FillComboBox("LC")
            RefreshControls()
            DisplayDetail(-1)
            DisplayRecord()
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RefreshControls()
        Try
            'Ali Faisal : Changes to refresh Controls on 16-June-2016
            Me.dtpRequestDate.Value = Now
            Me.dtpRequestDate.Enabled = True
            Me.txtRequestNo.Text = GetDocumentNo()
            Me.cmbSupplier.Rows(0).Activate()
            Me.cmbItem.Rows(0).Activate()

            Me.cmbLCNo.SelectedValue = 0
            Me.cmbPONo.SelectedIndex = 0
            Me.cmbGRNNo.SelectedIndex = 0
            Me.cmbDepartment.SelectedIndex = 0
            Me.txtBatchNo.Text = ""
            Me.txtNoofContainers.Text = ""
            Me.txtQty.Text = ""
            Me.txtComments.Text = ""
            Me.txtStage.Text = ""
            Me.cmbStage.SelectedValue = 0
            Me.cmbPlan.SelectedValue = 0
            Me.cmbTicket.SelectedValue = 0
            Me.txtRemarks.Text = ""
            Me.btnSave.Text = "&Save"
            Me.cmbSupplier.Focus()
            Me.rbCode.Checked = True
            Me.dtpRetestDate.Value = Now
            'Ali Faisal : TFS3849 : Re-test date and Product Parameter configuration changes on Lab Testing module.
            Me.dtpRetestDate.Checked = False
            'End
            Me.dtpMfgDate.Value = Now
            Me.dtpMfgDate.Checked = False
            Me.dtpExpDate.Value = Now
            Me.dtpExpDate.Checked = False

            FillEmployeeCombo()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ClearDetailControls()
        Try
            'Ali Faisal : Clear item detail records on 18-June-2016
            Me.cmbItem.Rows(0).Activate()
            Me.txtBatchNo.Text = ""
            Me.txtNoofContainers.Text = ""
            Me.txtQty.Text = ""
            Me.txtComments.Text = ""
            Me.dtpRetestDate.Value = Now
            'Ali Faisal : TFS3849 : Re-test date and Product Parameter configuration changes on Lab Testing module.
            Me.dtpRetestDate.Checked = False
            'End
            Me.dtpMfgDate.Value = Now
            Me.dtpMfgDate.Checked = False
            Me.dtpExpDate.Value = Now
            Me.dtpExpDate.Checked = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillComboBox(ByVal strCondition As String)
        Try
            'Ali Faisal : Fill all combos on 16-June-2016
            Dim str As String = String.Empty
            If strCondition = "Item" Then
                str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleColorName as Combination,ArticleUnitName as UOM,ArticleSizeName as Size, PackQty as PackQty, ISNULL(PurchasePrice,0) as Price, ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model, ArticleDefView.SortOrder FROM  ArticleDefView where Active=1 "
                FillUltraDropDown(Me.cmbItem, str, True)

                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If

            ElseIf strCondition = "ItemFilter" Then
                'str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleColorName as Combination,ArticleUnitName as UOM,ArticleSizeName as Size, PackQty as PackQty, ISNULL(PurchasePrice,0) as Price, ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model, ArticleDefView.SortOrder FROM ArticleDefView INNER JOIN planticketsmaster ON planticketsmaster.MasterArticleId = ArticleDefView.ArticleId INNER JOIN finishgoodmaster ON finishgoodmaster.MasterArticleId = ArticleDefView.ArticleId  where Active=1 and finishgoodmaster.Default1 = 1  and PlanTicketsMaster.PlanTicketsMasterID = " & cmbTicket.SelectedValue
                'str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleColorName as Combination,ArticleUnitName as UOM,ArticleSizeName as Size, PackQty as PackQty, ISNULL(PurchasePrice,0) as Price, ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model, ArticleDefView.SortOrder FROM ArticleDefView INNER JOIN PlanDetailTable ON ArticleDefView.ArticleId = PlanDetailTable.ArticleDefId where PlanDetailTable.PlanId = " & cmbPlan.SelectedValue
                'str = "SELECT ArticleDefTableMaster.ArticleId as Id, ArticleDefTableMaster.ArticleCode Code, ArticleDefTableMaster.ArticleDescription Item, ArticleDefView.ArticleColorName as Combination,ArticleDefView.ArticleUnitName as UOM,ArticleDefView.ArticleSizeName as Size, ArticleDefTableMaster.PackQty as PackQty, ISNULL(ArticleDefTableMaster.PurchasePrice,0) as Price, ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model, PlanDetailTable.PlanId,ArticleDefTableMaster.SortOrder FROM ArticleDefView INNER JOIN PlanDetailTable ON ArticleDefView.ArticleId = PlanDetailTable.ArticleDefId INNER JOIN ArticleDefTableMaster ON ArticleDefTableMaster.ArticleId = ArticleDefView.ArticleId where PlanDetailTable.PlanId = " & cmbPlan.SelectedValue
                str = "Select ArticleDefTable.ArticleId As Id , ArticleDefTable.ArticleCode Code, ArticleDefTable.ArticleDescription Item,ArticleDefView.ArticleColorName as Combination,ArticleDefView.ArticleUnitName as UOM,ArticleDefView.ArticleSizeName as Size,ArticleDefTable.PackQty as PackQty, ISNULL(ArticleDefTable.PurchasePrice,0) as Price,ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model, ArticleDefTable.SortOrder From PlanTicketsDetail INNER JOIN ArticleDefTable ON PlanTicketsDetail.ArticleId = ArticleDefTable.ArticleId INNER JOIN ArticleDefView ON ArticleDefTable.ArticleId = ArticleDefView.ArticleId LEFT JOIN ArticleUnitDefTable ON ArticleDefTable.ArticleUnitId = ArticleUnitDefTable.ArticleUnitId Where PlanTicketsDetail.PlanTicketsMasterID = " & cmbTicket.SelectedValue
                FillUltraDropDown(Me.cmbItem, str, True)

                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If

                'Ali Faisal : Fill combo to filter Items for Specific GRN on 31-Jan-2017
            ElseIf strCondition = "GRNItem" Then
                str = "SELECT     ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleCode AS Code, ArticleDefView.ArticleDescription AS Item, ArticleDefView.ArticleColorName AS Combination, ArticleDefView.ArticleUnitName AS UOM," _
                      & " ArticleDefView.ArticleSizeName AS Size, ArticleDefView.PackQty, ArticleDefView.PurchasePrice, ArticleDefView.ArticleCompanyName AS Category, ArticleDefView.ArticleLpoName AS Model, ArticleDefView.SortOrder" _
                      & " FROM         ReceivingNoteDetailTable INNER JOIN " _
                      & " ArticleDefView ON ReceivingNoteDetailTable.ArticleDefId = ArticleDefView.ArticleId " _
                      & " WHERE ReceivingNoteDetailTable.ReceivingNoteId = " & Me.cmbGRNNo.SelectedValue & ""
                FillUltraDropDown(Me.cmbItem, str, True)

            ElseIf strCondition = "Vendor" Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                                 "dbo.tblListTerritory.TerritoryName as Territory,dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                                                 "FROM dbo.tblVendor INNER JOIN " & _
                                                 "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                                 "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                                 "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                                 "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                                 "WHERE     (dbo.vwCOADetail.account_type ='Vendor') "
                FillUltraDropDown(Me.cmbSupplier, str)

            ElseIf strCondition = "PO" Then
                str = "Select PurchaseOrderID, PurchaseOrderNo + ' ~ ' + Convert(Varchar(12), PurchaseOrderDate, 113) as PurchaseOrderNo, DcNo,Remarks, IsNull(CostCenterId, 0) As CostCenterId from PurchaseOrderMasterTable where VendorId= " & Me.cmbSupplier.Value & " AND PurchaseOrderMasterTable.PurchaseOrderNo Not In(Select IsNull(TestReq_PONo,0) From LabRequestMasterTable) AND IsNull(Post,0)=1 Order By PurchaseOrderID DESC "
                FillDropDown(Me.cmbPONo, str)

            ElseIf strCondition = "GRN" Then
                str = "Select ReceivingNoteMasterTable.ReceivingNoteId,ReceivingNoteMasterTable.ReceivingNo  + ' ~ ' + Convert(Varchar(12), ReceivingDate, 113) as ReceivingNo,* From ReceivingNoteMasterTable WHERE VendorId= " & Me.cmbSupplier.Value & "   ORDER BY ReceivingNoteMasterTable.ReceivingNoteId DESC"
                FillDropDown(Me.cmbGRNNo, str)

            ElseIf strCondition = "Department" Then
                str = "Select EmployeeDeptId,EmployeeDeptName,Active from EmployeeDeptDefTable where Active='1' Order By EmployeeDeptName ASC"
                FillDropDown(Me.cmbDepartment, str)

            ElseIf strCondition = "LC" Then
                str = "Select LCdoc_Id, LCdoc_No, LCdoc_Date, Bank, LCdoc_Type,CostCenter From tblLetterOfCredit WHERE Active=1"
                FillDropDown(Me.cmbLCNo, str)
                'ElseIf strCondition = "PLAN" Then
                '    str = "Select PlanTicketsMaster.PlanId, PlanTicketsMaster.PlanNo from PlanMasterTable left join PlanTicketsMaster on PlanTicketsMaster.PlanID = PlanMasterTable.PlanId "
                '    FillDropDown(Me.cmbPlan, str)
                'ElseIf strCondition = "TICKET" Then
                '    str = "Select PlanTicketsId, TicketNo From PlanTickets where PlanId = '" & cmbPlan.SelectedValue.ToString() & "'"
                'FillDropDown(Me.cmbTicket, str)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub AddToGrid()
        Try
            'Ali Faisal : Add record into grid on 17-June-2016
            Dim dtGrd As DataTable
            dtGrd = CType(Me.grdLabRequest.DataSource, DataTable)
            dtGrd.AcceptChanges()
            Dim drGrd As DataRow
            drGrd = dtGrd.NewRow
            drGrd.Item(grdenm.TestReq_ItemId) = Me.cmbItem.Value
            drGrd.Item(grdenm.TestReq_ItemCode) = Me.cmbItem.ActiveRow.Cells("Code").Value.ToString
            drGrd.Item(grdenm.TestReq_Item) = Me.cmbItem.ActiveRow.Cells("Item").Value.ToString
            drGrd.Item(grdenm.TestReq_Qty) = Val(Me.txtQty.Text)
            drGrd.Item(grdenm.TestReq_Batch) = Me.txtBatchNo.Text.ToString
            drGrd.Item(grdenm.TestReq_NoofContainers) = Me.txtNoofContainers.Text.ToString
            drGrd.Item(grdenm.TestReq_Comments) = Me.txtComments.Text.ToString
            'Ali Faisal : Add record in detail grid on 30-Jan-2017
            'drGrd.Item(grdenm.TestReq_RetestDate) = CType(Me.dtpRetestDate.Value, Date)
            'Ali Faisal : TFS3849 : Re-test date and Product Parameter configuration changes on Lab Testing module.
            drGrd.Item(grdenm.TestReq_RetestDate) = IIf(Me.dtpRetestDate.Checked = True, Me.dtpRetestDate.Value, DBNull.Value)
            'End
            drGrd.Item(grdenm.TestReq_MfgDate) = IIf(Me.dtpMfgDate.Checked = True, Me.dtpMfgDate.Value, DBNull.Value)
            drGrd.Item(grdenm.TestReq_ExpDate) = IIf(Me.dtpExpDate.Checked = True, Me.dtpExpDate.Value, DBNull.Value)
            dtGrd.Rows.InsertAt(drGrd, 0)
            dtGrd.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub DisplayDetail(ByVal TestReq_Id As Integer)
        Try
            'Ali Faisal : To display detail record in detail grid on 17-June-2016
            Dim str As String
            str = "Select IsNull(TestReq_Detail_Id, 0), TestReq_ItemId, TestReq_ItemCode,TestReq_Item,TestReq_Qty,TestReq_Batch,TestReq_NoofContainers,TestReq_Comments,TestReq_RetestDate,TestReq_MfgDate,TestReq_ExpDate From LabRequestDetailTable where LabRequestDetailTable.TestReq_Id=" & TestReq_Id & ""
            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            dtDisplayDetail.AcceptChanges()
            Me.grdLabRequest.DataSource = dtDisplayDetail
            Me.grdLabRequest.RootTable.Columns("TestReq_Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdLabRequest.RootTable.Columns("TestReq_NoofContainers").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdLabRequest.RootTable.Columns("TestReq_RetestDate").FormatString = str_DisplayDateFormat
            Me.grdLabRequest.RootTable.Columns("TestReq_MfgDate").FormatString = str_DisplayDateFormat
            Me.grdLabRequest.RootTable.Columns("TestReq_ExpDate").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Function Validate_AddToGrid() As Boolean
        Try
            If Me.cmbItem.IsItemInList = False Then
                msg_Error("Item not found...")
                Me.cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
            End If
            If cmbItem.Value <= 0 Then
                msg_Error("Please Select an Item...")
                cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
            End If
            If Val(txtQty.Text) <= 0 Then
                msg_Error("Quantity is not greater than 0")
                txtQty.Focus() : Validate_AddToGrid = False : Exit Function
            End If
            Validate_AddToGrid = True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetDocumentNo() As String
        Try
            'Ali Faisal : Set auto Request number Normal,Yearly,Monthly on Configuration Bases on 18-June-2016
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("LR" + "-" + Microsoft.VisualBasic.Right(Me.dtpRequestDate.Value.Year, 2) + "-", "LabRequestMasterTable", "TestReq_DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("LR" & "-" & Format(Me.dtpRequestDate.Value, "yy") & Me.dtpRequestDate.Value.Month.ToString("00"), 4, "LabRequestMasterTable", "TestReq_DocNo")
            Else
                Return GetNextDocNo("LR", 6, "LabRequestMasterTable", "TestReq_DocNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub DisplayRecord(Optional ByVal strCondition As String = "")
        Try
            Dim str As String = String.Empty
            Dim dt As New DataTable
            str = "SELECT " & IIf(strCondition.ToString = "All", "", "Top 50") & " LRM.TestReq_Id, LRM.TestReq_Date, LRM.TestReq_DocNo,LRM.TestReq_Supplier, " &
                "LRM.TestReq_PONo,LRM.TestReq_GRNNo,LRM.TestReq_LCNo,LRM.TestReq_Department, vwCOADetail.detail_title, PurchaseOrderMasterTable.PurchaseOrderNo, " _
            & " ReceivingNoteMasterTable.ReceivingNo, tblLetterOfCredit.LCdoc_No, EmployeeDeptDefTable.EmployeeDeptName, LRM.TestReq_Stage, LRM.TestReq_Remarks, LRM.TicketId, LRM.StageId , LRM.Employee " _
            & " FROM         ReceivingNoteMasterTable RIGHT OUTER JOIN " _
            & " LabRequestMasterTable AS LRM LEFT OUTER JOIN " _
            & " EmployeeDeptDefTable ON LRM.TestReq_Department = EmployeeDeptDefTable.EmployeeDeptId LEFT OUTER JOIN " _
            & " tblLetterOfCredit ON LRM.TestReq_LCNo = tblLetterOfCredit.LCdoc_Id ON ReceivingNoteMasterTable.ReceivingNoteId = LRM.TestReq_GRNNo LEFT OUTER JOIN" _
            & " PurchaseOrderMasterTable ON LRM.TestReq_PONo = PurchaseOrderMasterTable.PurchaseOrderId LEFT OUTER JOIN " _
            & " vwCOADetail ON LRM.TestReq_Supplier = vwCOADetail.coa_detail_id " _
            & " Where LRM.TestReq_Id <> 0 "
            If Me.dtpFromDate.Checked = True Then
                str += " AND LRM.TestReq_Date >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
            End If
            If Me.dtpToDate.Checked = True Then
                str += " AND LRM.TestReq_Date <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
            End If
            If Me.txtGRNNo.Text <> String.Empty Then
                str += " AND ReceivingNo LIKE '%" & Me.txtGRNNo.Text & "%'"
            End If
            If Me.txtPONo.Text <> String.Empty Then
                str += " AND PurchaseOrderNo LIKE '%" & Me.txtPONo.Text & "%'"
            End If
            If Me.txtSupplierName.Text <> String.Empty Then
                str += " AND detail_title LIKE '%" & Me.txtSupplierName.Text & "%'"
            End If

            str += " Order by LRM.TestReq_Id Desc"

            dt = GetDataTable(str)
            dt.AcceptChanges()
            grdSaved.DataSource = dt
            grdSaved.RetrieveStructure()
            ApplyGridSetting()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs)
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            'Ali Faisal : Refresh all text boxes and combos on 17-June-2016
            Dim id As Integer = 0
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            id = Me.cmbSupplier.SelectedRow.Cells(0).Value
            FillComboBox("Vendor")
            Me.cmbSupplier.Value = id

            id = Me.cmbPONo.SelectedValue
            FillComboBox("PO")
            Me.cmbPONo.SelectedValue = id

            id = Me.cmbGRNNo.SelectedValue
            FillComboBox("GRN")
            Me.cmbGRNNo.SelectedValue = id

            id = Me.cmbLCNo.SelectedValue
            FillComboBox("")
            Me.cmbLCNo.SelectedValue = id

            id = Me.cmbDepartment.SelectedValue
            FillComboBox("Department")
            Me.cmbDepartment.SelectedValue = id

            id = Me.cmbItem.SelectedRow.Cells(0).Value
            FillComboBox("Item")
            Me.cmbItem.Value = id
            Me.lblProgress.Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function FormValidate() As Boolean
        Try
            'Ali Faisal : Add function to validate that Vendor is selected and detail grid is not empty on 20-June-2016
            If txtRequestNo.Text = "" Then
                msg_Error("Please enter Request No.")
                txtRequestNo.Focus() : FormValidate = False : Exit Function
            End If
            If cmbTicket.SelectedValue <= 0 Then

                If cmbSupplier.ActiveRow.Cells(0).Value <= 0 Then
                    msg_Error("Please select Vendor")
                    cmbSupplier.Focus() : FormValidate = False : Exit Function
                End If

            End If

            If Not Me.grdLabRequest.RowCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
                cmbItem.Focus() : FormValidate = False : Exit Function
            End If
            Return True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Private Function Save() As Boolean
        Try
            'Dim statusId As String = ""
            'If btnStatus.Enabled = False Then
            '    Dim con As SqlConnection = New SqlConnection(SQLHelper.CON_STR)
            '    If con.State = ConnectionState.Open Then con.Close()
            '    con.Open()
            '    Dim cmd As SqlCommand = New SqlCommand(" select StatusId from tblDefStatusTypes where StatusCode = 'Sent For QC'", con)
            '    statusId = cmd.ExecuteScalar().ToString()
            '    con.Close()
            'End If

            Me.grdLabRequest.UpdateData()
            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            Dim i As Integer
            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            objCommand.Connection = objCon
            Dim trans As OleDbTransaction = objCon.BeginTransaction
            Try
                objCommand.CommandType = CommandType.Text
                objCommand.Transaction = trans
                objCommand.CommandText = ""

                'Ali Faisal : Insert master record on 21-June-2016
                objCommand.CommandText = "insert into LabRequestMasterTable (TestReq_Date,TestReq_DocNo,TestReq_Supplier,TestReq_PONo,TestReq_GRNNo,TestReq_LCNo,TestReq_Department,TestReq_Stage,TestReq_Remarks, TicketId, StageId, Employee)" _
                   & " Values (N'" & dtpRequestDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "' , N'" & txtRequestNo.Text & "' , N'" & cmbSupplier.ActiveRow.Cells(0).Value & "', N'" & Me.cmbPONo.SelectedValue & "' , N'" & Me.cmbGRNNo.SelectedValue & "' , N'" & Me.cmbLCNo.SelectedValue & "'  , N'" & Me.cmbDepartment.SelectedValue & "', N'" & txtStage.Text & "',  N'" & txtRemarks.Text & "','" & cmbTicket.SelectedValue & "','" & cmbStage.SelectedValue & "','" & cmbEmployee.Text & "')"
                objCommand.ExecuteNonQuery()
                'Ali Faisal : Insert detail record on 21-June-2016
                For i = 0 To grdLabRequest.RowCount - 1
                    objCommand.CommandText = ""
                    'objCommand.CommandText = "insert into LabRequestDetailTable (TestReq_Id,TestReq_ItemId,TestReq_ItemCode,TestReq_Item,TestReq_Qty,TestReq_Batch,TestReq_NoofContainers,TestReq_Comments) " _
                    '  & "  Values (Ident_Current('LabRequestMasterTable') , " & Val(grdLabRequest.GetRows(i).Cells(grdenm.TestReq_ItemId).Value) & " , N'" & grdLabRequest.GetRows(i).Cells(grdenm.TestReq_ItemCode).Value.ToString & "' , N'" & grdLabRequest.GetRows(i).Cells(grdenm.TestReq_Item).Value.ToString & "' , " & Val(grdLabRequest.GetRows(i).Cells(grdenm.TestReq_Qty).Value) & " , N'" & grdLabRequest.GetRows(i).Cells(grdenm.TestReq_Batch).Value.ToString & "' , " & Val(grdLabRequest.GetRows(i).Cells(grdenm.TestReq_NoofContainers).Value) & "  ,  N'" & grdLabRequest.GetRows(i).Cells(grdenm.TestReq_Comments).Value.ToString & "' ) "

                    'Dim TestReq_RetestDate As Date = grdLabRequest.GetRow(i).Cells("TestReq_RetestDate").Value
                    'Ali Faisal : TFS3849 : Re-test date and Product Parameter configuration changes on Lab Testing module.
                    Dim TestReq_RetestDate As Date = Nothing

                    Dim TestReq_MfgDate As Date = Nothing
                    Dim TestReq_ExpDate As Date = Nothing

                    If IsDBNull(grdLabRequest.GetRow(i).Cells("TestReq_MfgDate").Value) = False Then
                        TestReq_MfgDate = grdLabRequest.GetRow(i).Cells("TestReq_MfgDate").Value
                    End If

                    If IsDBNull(grdLabRequest.GetRow(i).Cells("TestReq_ExpDate").Value) = False Then
                        TestReq_ExpDate = grdLabRequest.GetRow(i).Cells("TestReq_ExpDate").Value
                    End If

                    'Ali Faisal : TFS3849 : Re-test date and Product Parameter configuration changes on Lab Testing module.
                    If IsDBNull(grdLabRequest.GetRow(i).Cells("TestReq_RetestDate").Value) = False Then
                        TestReq_RetestDate = grdLabRequest.GetRow(i).Cells("TestReq_RetestDate").Value
                    End If

                    'Ali Faisal : TFS3849 : Re-test date and Product Parameter configuration changes on Lab Testing module.
                    objCommand.CommandText = "insert into LabRequestDetailTable (TestReq_Id,TestReq_ItemId,TestReq_ItemCode,TestReq_Item,TestReq_Qty,TestReq_Batch,TestReq_NoofContainers,TestReq_Comments,TestReq_RetestDate,TestReq_MfgDate,TestReq_ExpDate) " _
                      & "  Values (Ident_Current('LabRequestMasterTable') , " & Val(grdLabRequest.GetRows(i).Cells(grdenm.TestReq_ItemId).Value) & " , N'" & grdLabRequest.GetRows(i).Cells(grdenm.TestReq_ItemCode).Value.ToString & "' , N'" & grdLabRequest.GetRows(i).Cells(grdenm.TestReq_Item).Value.ToString & "' , " & Val(grdLabRequest.GetRows(i).Cells(grdenm.TestReq_Qty).Value) & " , N'" & grdLabRequest.GetRows(i).Cells(grdenm.TestReq_Batch).Value.ToString & "' , " & Val(grdLabRequest.GetRows(i).Cells(grdenm.TestReq_NoofContainers).Value) & "  ,  N'" & grdLabRequest.GetRows(i).Cells(grdenm.TestReq_Comments).Value.ToString & "', " &
                     "" & IIf(IsDBNull(grdLabRequest.GetRow(i).Cells(grdenm.TestReq_RetestDate).Value) = True, "NULL", "N'" & TestReq_RetestDate.ToString("yyyy-MM-dd hh:mm:ss") & "'") & "," & IIf(IsDBNull(grdLabRequest.GetRow(i).Cells(grdenm.TestReq_MfgDate).Value) = True, "NULL", "N'" & TestReq_MfgDate.ToString("yyyy-MM-dd hh:mm:ss") & "'") & "," & IIf(IsDBNull(grdLabRequest.GetRow(i).Cells(grdenm.TestReq_ExpDate).Value) = True, "NULL", "N'" & TestReq_ExpDate.ToString("yyyy-MM-dd hh:mm:ss") & "'") & "  ) "
                    objCommand.ExecuteNonQuery()
                    'End
                Next
                trans.Commit()
                Save = True
                DisplayRecord()
            Catch ex As Exception
                trans.Rollback()
                Save = False
                Throw ex
            End Try
        Catch ex As Exception
            ShowErrorMessage("An error occured while saving voucher: " & ex.Message)
        End Try
        'Ali Faisal : Insert Activity Log on 21-June-2016
        SaveActivityLog("LabTesting", Me.Text, IIf(Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Lab, Me.CurrentId, True)
    End Function
    Private Function UpdateRecord() As Boolean
        ' it is observed by Muhammad Abdullah that detail record is updated against last master id instead of current master id which is going to be updated '
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        objCon = Con
        If objCon.State = ConnectionState.Open Then objCon.Close()
        objCon.Open()
        objCommand.Connection = objCon
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            objCommand.CommandText = ""
            objCommand.CommandText = "Update LabRequestMasterTable set TestReq_Date =N'" & dtpRequestDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',TestReq_DocNo =N'" & txtRequestNo.Text & "',TestReq_Supplier =" & cmbSupplier.ActiveRow.Cells(0).Value & ",TestReq_PONo= " & Val(cmbPONo.SelectedValue) & ",TestReq_GRNNo =" & Val(cmbGRNNo.SelectedValue) & ", " _
                                    & " TestReq_LCNo =" & Val(cmbLCNo.SelectedValue) & ",TestReq_Department = " & Val(cmbDepartment.SelectedValue) & ",TestReq_Stage =N'" & txtStage.Text & "',TestReq_Remarks = N'" & txtRemarks.Text.ToString & "', TicketId = '" & cmbTicket.SelectedValue & "',  StageId = '" & cmbStage.SelectedValue & "' , Employee = '" & cmbEmployee.Text & "'  Where TestReq_Id=" & Me.grdSaved.GetRow.Cells("TestReq_Id").Value & ""
            objCommand.ExecuteNonQuery()
            objCommand.CommandText = ""
            objCommand.CommandText = "Delete from LabRequestDetailTable where TestReq_Id =" & Me.grdSaved.GetRow.Cells("TestReq_Id").Value & ""
            objCommand.ExecuteNonQuery()
            For i = 0 To grdLabRequest.RowCount - 1
                objCommand.CommandText = ""

                'Ali Faisal : TFS3849 : Re-test date and Product Parameter configuration changes on Lab Testing module.
                'Dim TestReq_RetestDate As Date = grdLabRequest.GetRow(i).Cells("TestReq_RetestDate").Value
                Dim TestReq_RetestDate As Date = Nothing

                Dim TestReq_MfgDate As Date = Nothing
                Dim TestReq_ExpDate As Date = Nothing

                If IsDBNull(grdLabRequest.GetRow(i).Cells("TestReq_MfgDate").Value) = False Then
                    TestReq_MfgDate = grdLabRequest.GetRow(i).Cells("TestReq_MfgDate").Value
                End If

                If IsDBNull(grdLabRequest.GetRow(i).Cells("TestReq_ExpDate").Value) = False Then
                    TestReq_ExpDate = grdLabRequest.GetRow(i).Cells("TestReq_ExpDate").Value
                End If

                'Ali Faisal : TFS3849 : Re-test date and Product Parameter configuration changes on Lab Testing module.
                If IsDBNull(grdLabRequest.GetRow(i).Cells("TestReq_RetestDate").Value) = False Then
                    TestReq_RetestDate = grdLabRequest.GetRow(i).Cells("TestReq_RetestDate").Value
                End If

                objCommand.CommandText = "insert into LabRequestDetailTable (TestReq_Id,TestReq_ItemId,TestReq_ItemCode,TestReq_Item,TestReq_Qty,TestReq_Batch,TestReq_NoofContainers,TestReq_Comments,TestReq_RetestDate,TestReq_MfgDate,TestReq_ExpDate) " _
                  & "  Values (" & Me.grdSaved.GetRow.Cells("TestReq_Id").Value & " , " & Val(grdLabRequest.GetRows(i).Cells(grdenm.TestReq_ItemId).Value) & " , N'" & grdLabRequest.GetRows(i).Cells(grdenm.TestReq_ItemCode).Value.ToString & "' , N'" & grdLabRequest.GetRows(i).Cells(grdenm.TestReq_Item).Value.ToString & "' , " & Val(grdLabRequest.GetRows(i).Cells(grdenm.TestReq_Qty).Value) & " , N'" & grdLabRequest.GetRows(i).Cells(grdenm.TestReq_Batch).Value.ToString & "' , " & Val(grdLabRequest.GetRows(i).Cells(grdenm.TestReq_NoofContainers).Value) & "  ,  N'" & grdLabRequest.GetRows(i).Cells(grdenm.TestReq_Comments).Value.ToString & "',  " & IIf(IsDBNull(grdLabRequest.GetRow(i).Cells(grdenm.TestReq_RetestDate).Value) = True, "Null", "N'" & TestReq_RetestDate.ToString("yyyy-MM-dd hh:mm:ss") & "'") & " , " & IIf(IsDBNull(grdLabRequest.GetRow(i).Cells(grdenm.TestReq_MfgDate).Value) = True, "Null", "N'" & TestReq_MfgDate.ToString("yyyy-MM-dd hh:mm:ss") & "'") & " , " & IIf(IsDBNull(grdLabRequest.GetRow(i).Cells(grdenm.TestReq_ExpDate).Value) = True, "Null", "N'" & TestReq_ExpDate.ToString("yyyy-MM-dd hh:mm:ss") & "'") & " ) "
                objCommand.ExecuteNonQuery()
                'End
            Next
            trans.Commit()
            UpdateRecord = True
            DisplayRecord()
            'Insert Activity Log
            SaveActivityLog("LabTesting", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Lab, Me.txtRequestNo.Text.Trim, True)
        Catch ex As Exception
            trans.Rollback()
            UpdateRecord = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try
    End Function
    Private Function GetPlanIdByTicketId(ticketId As Integer) As Integer
        Dim sql As String = " select PlanID from PlanTicketsMaster where PlanTicketsMasterID =  " & ticketId
        Dim con As SqlConnection = New SqlConnection(SQLHelper.CON_STR)
        con.Open()
        Dim da As SqlDataAdapter = New SqlDataAdapter(sql, con)
        Dim cmd As SqlCommand = New SqlCommand(sql, con)
        Dim id As String = cmd.ExecuteScalar().ToString()
        con.Close()
        Return id
    End Function
    Private Sub EditRecord()
        Try
            IsEditMode = True
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grdLabRequest.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.dtpRequestDate.Value = CType(grdSaved.CurrentRow.Cells("TestReq_Date").Value, Date)
            Me.txtRequestNo.Text = Me.grdSaved.CurrentRow.Cells("TestReq_DocNo").Value.ToString
            Me.cmbSupplier.Value = Val(Me.grdSaved.CurrentRow.Cells("TestReq_Supplier").Value)
            Me.cmbPONo.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("TestReq_PONo").Value)
            Me.cmbGRNNo.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("TestReq_GRNNo").Value)
            Me.cmbLCNo.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("TestReq_LCNo").Value)
            Me.cmbDepartment.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("TestReq_Department").Value)
            Me.cmbEmployee.Text = Me.grdSaved.CurrentRow.Cells("Employee").Value.ToString
            'FillOpenTicketPlanCombo()
            'Application.DoEvents()
            FillPlanCombo()

            If Me.grdSaved.CurrentRow.Cells("TicketId").Text.ToString = "" Or Me.grdSaved.CurrentRow.Cells("TicketId").Text.ToString = "0" Then
                Me.cmbTicket.SelectedValue = 0
            Else
                Dim id As Integer = GetPlanIdByTicketId(Convert.ToInt32(Me.grdSaved.CurrentRow.Cells("TicketId").Text.ToString))
                cmbPlan.SelectedValue = id
                Me.cmbTicket.SelectedValue = Convert.ToInt32(Me.grdSaved.CurrentRow.Cells("TicketId").Text.ToString)

            End If
            'Me.cmbTicket.SelectedValue = Me.grdSaved.CurrentRow.Cells("TicketId").Text.ToString
            Me.txtStage.Text = Me.grdSaved.CurrentRow.Cells("TestReq_Stage").Text.ToString
            If Me.grdSaved.CurrentRow.Cells("StageId").Text.ToString = "" Or Me.grdSaved.CurrentRow.Cells("StageId").Text.ToString = "0" Then
                Me.cmbStage.SelectedValue = 0
            Else
                Me.cmbStage.SelectedValue = Convert.ToInt32(Me.grdSaved.CurrentRow.Cells("StageId").Text.ToString)
            End If
            Me.txtRemarks.Text = Me.grdSaved.CurrentRow.Cells("TestReq_Remarks").Text.ToString
            DisplayDetail(Val(grdSaved.CurrentRow.Cells("TestReq_Id").Value))
            Me.btnSave.Text = "&Update"
            Me.GetSecurityRights()
            Me.btnDelete.Visible = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            Me.grdSaved.RootTable.Columns("TestReq_Id").Visible = False
            Me.grdSaved.RootTable.Columns("TestReq_Supplier").Visible = False
            Me.grdSaved.RootTable.Columns("TestReq_PONo").Visible = False
            Me.grdSaved.RootTable.Columns("TestReq_GRNNo").Visible = False
            Me.grdSaved.RootTable.Columns("TestReq_LCNo").Visible = False
            Me.grdSaved.RootTable.Columns("TestReq_Department").Visible = False
            Me.grdSaved.RootTable.Columns("TicketId").Visible = False
            Me.grdSaved.RootTable.Columns("StageId").Visible = False
            Me.grdSaved.RootTable.Columns("TestReq_Date").FormatString = str_DisplayDateFormat
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrintForm.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmPurchaseOrder)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrintForm.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrintForm.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrintForm.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Post" Then
                    ElseIf RightsDt.FormControlName = "Price Allow" Then
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            If Me.grdLabRequest.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            RefreshControls()
            FillComboBox("PO")
            FillComboBox("GRN")
            FillPlanCombo()
            FillEmployeeCombo()
            DisplayDetail(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Validate_AddToGrid() = False Then Exit Sub
            AddToGrid()
            ClearDetailControls()
            Me.cmbItem.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

            If Me.btnSave.Enabled = False Then Exit Sub
            Me.Cursor = Cursors.WaitCursor
            Try
                If FormValidate() Then
                    Me.grdLabRequest.UpdateData()
                    If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then

                        Dim ResultStatus As Integer = 1
                        Dim i As Integer = cmbStage.SelectedIndex - 1
                        If i > 0 Then
                            Dim itemAtIndex As DataRowView = cmbStage.Items(i)
                            ResultStatus = GetTicketStageStatus(cmbTicket.SelectedValue, Convert.ToInt32(itemAtIndex.Row("ProdStep_Id")))
                        End If

                    If ResultStatus = 0 Then

                        ShowErrorMessage("Previous Stages Result is not Approved")

                    Else

                        Me.lblProgress.Text = "Processing Please Wait ..."
                        Me.lblProgress.Visible = True
                        Application.DoEvents()
                        If Me.Save() Then
                            RefreshControls()
                            DisplayDetail(-1)
                        End If

                    End If


                    Else
                        If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                        Me.lblProgress.Text = "Processing Please Wait ..."
                        Me.lblProgress.Visible = True
                        Application.DoEvents()
                        If UpdateRecord() Then
                            RefreshControls()
                            DisplayDetail(-1)
                        End If
                    End If
                    End If
            Catch ex As Exception
                ShowErrorMessage("Error occured while saving record: " & ex.Message)
            Finally
                Me.Cursor = Cursors.Default
                Me.lblProgress.Visible = False
            End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim cm As New OleDbCommand
            Dim objTrans As OleDbTransaction
            If Con.State = ConnectionState.Closed Then Con.Open()
            objTrans = Con.BeginTransaction
            cm.Connection = Con
            cm.Transaction = objTrans
            'Ali Faisal : Detail record delete on 22-June-2016
            cm.CommandText = "Delete from LabRequestDetailTable where TestReq_Id=" & Me.grdSaved.CurrentRow.Cells("TestReq_Id").Value.ToString
            cm.Transaction = objTrans
            cm.ExecuteNonQuery()
            cm = New OleDbCommand
            cm.Connection = Con
            'Ali Faisal : Master record delete on 22-June-2016
            cm.CommandText = "Delete from LabRequestMasterTable where TestReq_Id=" & Me.grdSaved.CurrentRow.Cells("TestReq_Id").Value.ToString
            cm.Transaction = objTrans
            cm.ExecuteNonQuery()
            objTrans.Commit()
            Me.grdSaved.CurrentRow.Delete()
        Catch ex As Exception
            msg_Error("Error occured while deleting record: " & ex.Message)
        Finally
            Con.Close()
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
        Me.RefreshControls()
        Me.DisplayDetail(-1)
    End Sub
    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrintForm_Click(sender As Object, e As EventArgs) Handles btnPrintForm.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@RequestId", Me.grdSaved.GetRow.Cells("TestReq_Id").Value)
            ShowReport("rptLabTestRequest")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            If Me.grdSaved.Row < 0 Then
                Exit Sub
            Else
                EditRecord()
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Enter
        Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub
    Private Sub cmbSupplier_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSupplier.Enter
        Me.cmbSupplier.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Me.txtGRNNo.Text = ""
            Me.txtPONo.Text = ""
            Me.txtSupplierName.Text = ""
            Me.dtpFromDate.Value = Now
            Me.dtpFromDate.Checked = False
            Me.dtpToDate.Value = Now
            Me.dtpToDate.Checked = False
            Me.DisplayRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdLabRequest_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdLabRequest.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.grdLabRequest.GetRow.Delete()
                grdLabRequest.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdLabRequest_KeyDown(sender As Object, e As KeyEventArgs) Handles grdLabRequest.KeyDown
        If e.KeyCode = Keys.F2 Then
            EditRecord()
            Exit Sub
        End If
    End Sub
    Private Sub btnLoadAll_Click(sender As Object, e As EventArgs) Handles btnLoadAll.Click
        Try
            DisplayRecord("All")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSearchDetail_Click(sender As Object, e As EventArgs) Handles btnSearchDetail.Click
        Try
            DisplayRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If grpSearch.Visible = False Then
                grpSearch.Visible = True
            Else
                Me.grpSearch.Visible = False
                Me.btnReset_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbCode_CheckedChanged(sender As Object, e As EventArgs) Handles rbCode.CheckedChanged
        Try
            If Me.rbCode.Checked = True Then Me.cmbItem.DisplayMember = "Code"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbName_CheckedChanged(sender As Object, e As EventArgs) Handles rbName.CheckedChanged
        Try
            If Me.rbName.Checked = True Then Me.cmbItem.DisplayMember = "Item"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Lab Test Request"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSupplier_ValueChanged(sender As Object, e As EventArgs) Handles cmbSupplier.ValueChanged
        Try
            FillComboBox("PO")
            FillComboBox("GRN")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbGRNNo_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbGRNNo.SelectedValueChanged
        Try
            If Me.cmbGRNNo.SelectedValue > 0 Then
                FillComboBox("GRNItem")
            Else
                FillComboBox("Item")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FillPlanCombo()
        Try
            Dim sqlQuery As String = " Select PlanMasterTable.PlanId, PlanMasterTable.PlanNo from PlanMasterTable order by PlanMasterTable.PlanId DESC "
            FillDropDown(cmbPlan, sqlQuery)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub FillEmployeeCombo()
        Try
            Dim sqlQuery As String = "select Distinct Employee , Employee from LabRequestMasterTable where Employee <> ''"
            FillDropDown(cmbEmployee, sqlQuery, False)

            cmbEmployee.Text = String.Empty
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Sub frmLabTestRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillPlanCombo()
            FillEmployeeCombo()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try


    End Sub

    'Private Sub cmbPlan_TextChanged(sender As Object, e As EventArgs) Handles cmbPlan.TextChanged
    '    'Dim sqlQuery As String = " select PlanTicketsMasterID, TicketNo  from PlanTicketsMaster  where PlanTicketsMaster.PlanID = '" & cmbPlan.SelectedValue & "'"
    '    'Dim sqlQuery As String = "  select PlanTicketsMasterID, TicketNo  from  " &
    '    '            "  (select  PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.TicketNo , " &
    '    '            "   isnull( sum(Quantity),0) as TicketQty , " &
    '    '            "   isnull( sum(ProductionMasterTable.TotalQty),0) as ProductionQty from PlanTicketsMaster " &
    '    '            "  left join  PlanTicketsDetail on PlanTicketsDetail.PlanTicketsMasterID = PlanTicketsMaster.PlanTicketsMasterID " &
    '    '            "  left join ProductionMasterTable on ProductionMasterTable.PlanTicketId = PlanTicketsMaster.PlanTicketsMasterID " &
    '    '            "  group by  PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.TicketNo ) as fd  " &
    '    '            "    where ProductionQty < TicketQty "

    'End Sub
    Private Sub FillStageCombo()
        Try
            'Dim sqlQuery As String = " select ProdStep_Id, prod_step  from tblproSteps "
            Dim sqlQuery As String = " select  ProdStep_Id, prod_step  from PlanTicketsMaster " &
                   "     left join ProductionTicketStages on ProductionTicketStages.TicketId =  PlanTicketsMaster.PlanTicketsMasterID" &
                    "    left join tblproSteps on tblproSteps.ProdStep_Id = ProductionTicketStages.ProductionStageId where PlanTicketsMaster.PlanTicketsMasterID = '" & cmbTicket.SelectedValue & "' And ISNULL(tblproSteps.QCVerificationRequired, 0) = 1 order by tblproSteps.sort_order ASC"
            FillDropDown(cmbStage, sqlQuery)
            Application.DoEvents()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbTicket_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTicket.SelectedIndexChanged
        FillStageCombo()

        If (cmbTicket.SelectedValue <= 0) Then

            FillComboBox("Item")

        Else

            FillComboBox("ItemFilter")

        End If

    End Sub

    Private Sub btnStatus_Click(sender As Object, e As EventArgs)
        'btnStatus.Enabled = False
    End Sub
    Private Sub FillTicketCombo()
        Try
            Dim sqlQuery As String = "  select PlanTicketsMasterID, BatchNo As TicketNo,  PlanID  from " &
             "  (select  PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.BatchNo ,  " &
                 "   isnull( sum(Quantity),0) as TicketQty ,  " &
                "   isnull( sum(ProductionMasterTable.TotalQty),0) as ProductionQty, PlanTicketsMaster.PlanID from PlanTicketsMaster  " &
                "  left join  PlanTicketsDetail on PlanTicketsDetail.PlanTicketsMasterID = PlanTicketsMaster.PlanTicketsMasterID  " &
                "   left join ProductionMasterTable on ProductionMasterTable.PlanTicketId = PlanTicketsMaster.PlanTicketsMasterID  " &
                "   group by  PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.BatchNo, PlanTicketsMaster.PlanID ) as fd   " &
                "     where ProductionQty < TicketQty and PlanID = '" & cmbPlan.SelectedValue & "' order by  PlanTicketsMasterID DESC"
            FillDropDown(cmbTicket, sqlQuery)
            Application.DoEvents()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbPlan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPlan.SelectedIndexChanged
        FillTicketCombo()

    End Sub

    Private Function GetTicketStageStatus(ByVal ticketId As Integer, ByVal stageId As Integer) As Integer
        Try

            Dim sql As String = " select TOP(1) LabResultMasterTable.ResultStatus from LabRequestMasterTable " &
                            "Inner Join LabTestSampleObvMaster On LabRequestMasterTable.TestReq_Id = LabTestSampleObvMaster.TestReq_Id " &
                            "Inner Join LabResultMasterTable On LabTestSampleObvMaster.ObserverSampleMasterId = LabResultMasterTable.QCId " &
                            "where LabRequestMasterTable.TicketId = " & ticketId & " And LabRequestMasterTable.StageId = " & stageId & " order by LabResultMasterTable.ResultID DESC"
            Dim con As SqlConnection = New SqlConnection(SQLHelper.CON_STR)
            con.Open()
            Dim da As SqlDataAdapter = New SqlDataAdapter(sql, con)
            Dim cmd As SqlCommand = New SqlCommand(sql, con)
            Dim ResultStatus As String = cmd.ExecuteScalar().ToString()
            con.Close()
            Return Val(ResultStatus)

        Catch ex As Exception
            Return 1
        End Try
    End Function

End Class
