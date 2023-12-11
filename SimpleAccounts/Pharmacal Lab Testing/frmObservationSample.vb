Imports System.Data.OleDb
Imports SBModel
Imports System.Data.SqlClient
Imports SBDal

Public Class frmObservationSample
    Implements IGeneral
    Dim PrintLog As PrintLogBE
    Dim dtLabTestRequest As New DataTable




    Public Sub GetAllParameters(Optional ByVal Condition As String = "")

        Dim strSql As String = String.Empty
        Dim dt As New DataTable
        strSql = "Select Parameter_Id, Parameter_Name, Convert(bit, 0) As Ok, '' As Comments " _
            & " From LabSampleParameters"
        'strSql = "Select Parameters.Parameter_Id, Parameters.Parameter_Name, Detail.[Status] As Ok, Detail.Comment As Comments " _
        '& "From LabSampleParameters As Parameters Left Outer Join LabTestSampleObvDetail As Detail On Detail.LabTestingParameterID = Parameters.Parameter_Id " ''LabTestSampleObvDetail
        '& " Where Detail.ObserverSampleMasterID = " & ObserverSampleMasterID & ""
        'strSql = "Select  Parameter_Id	, Parameter_Name, '' As Comments, Convert(bit, 0) As Ok from LabSampleParameters"
        dt = GetDataTable(strSql)
        dt.AcceptChanges()
        Me.grdObservationParameters.DataSource = dt
    End Sub
    Private Sub DisplayLTR(Optional ByVal strCondition As String = "")
        Try
            '            //LabRequestDetailTable
            'TestReq_Detail_Id	int	Unchecked
            'TestReq_Id	int	Checked
            'TestReq_ItemId	int	Checked
            'TestReq_ItemCode	nvarchar(50)	Checked
            'TestReq_Item	nvarchar(50)	Checked
            'TestReq_Qty	nvarchar(50)	Checked
            'TestReq_Batch	nvarchar(50)	Checked
            'TestReq_NoofContainers	nvarchar(50)	Checked
            'TestReq_Comments	nvarchar(250)	Checked


            '//LabRequestMasterTable
            'TestReq_Id	int	Unchecked
            'TestReq_Date	datetime	Checked
            'TestReq_DocNo	nvarchar(50)	Checked
            'TestReq_Supplier	nvarchar(250)	Checked
            'TestReq_PONo	nvarchar(50)	Checked
            'TestReq_GRNNo	nvarchar(50)	Checked
            'TestReq_LCNo	nvarchar(50)	Checked
            'TestReq_Department	nvarchar(50)	Checked
            'TestReq_Stage	nvarchar(50)	Checked
            'TestReq_Remarks	nvarchar(250)	Checked
            '            Unchecked()
            Dim str As String = String.Empty
            Dim dt As New DataTable
            str = "SELECT LRM.TestReq_Id, LRD.TestReq_Detail_Id, LRD.TestReq_ItemId, LRD.TestReq_ItemCode, LRD.TestReq_Item,  LRD.TestReq_Qty, LRD.TestReq_Batch, LRD.TestReq_NoofContainers, " &
              " LRD.TestReq_Comments,  LRM.TestReq_Date , LRM.TestReq_DocNo,LRM.TestReq_Supplier,LRM.TestReq_PONo,LRM.TestReq_GRNNo,LRM.TestReq_LCNo,LRM.TestReq_Department, " &
              " vwCOADetail.detail_title, PurchaseOrderMasterTable.PurchaseOrderNo, " _
            & " ReceivingNoteMasterTable.ReceivingNo, tblLetterOfCredit.LCdoc_No, EmployeeDeptDefTable.EmployeeDeptName, LRM.TestReq_Stage, LRM.TestReq_Remarks, " &
              " LRD.TestReq_RetestDate, LRD.TestReq_MfgDate, LRD.TestReq_ExpDate, LRM.TicketId, PlanTicketsMaster.TicketNo, tblproSteps.prod_step " _
            & " FROM ReceivingNoteMasterTable RIGHT OUTER JOIN " _
            & " LabRequestMasterTable AS LRM Inner Join " _
            & " LabRequestDetailTable LRD ON LRM.TestReq_Id = LRD.TestReq_Id left join PlanTicketsMaster on PlanTicketsMaster.PlanTicketsMasterID =  LRM.TicketId " &
            " left join tblproSteps on tblproSteps.ProdStep_Id = LRM.StageId   LEFT OUTER JOIN " _
            & " EmployeeDeptDefTable ON LRM.TestReq_Department = EmployeeDeptDefTable.EmployeeDeptId LEFT OUTER JOIN " _
            & " tblLetterOfCredit ON LRM.TestReq_LCNo = tblLetterOfCredit.LCdoc_Id ON ReceivingNoteMasterTable.ReceivingNoteId = LRM.TestReq_GRNNo LEFT OUTER JOIN" _
            & " PurchaseOrderMasterTable ON LRM.TestReq_PONo = PurchaseOrderMasterTable.PurchaseOrderId LEFT OUTER JOIN " _
            & " vwCOADetail ON LRM.TestReq_Supplier = vwCOADetail.coa_detail_id Where LRM.TestReq_Id Not in (Select TestReq_Id from LabTestSampleObvMaster) " _
            & " Order by LRD.TestReq_Detail_Id Desc"

            dt = GetDataTable(str)
            dt.AcceptChanges()
            dtLabTestRequest = dt
            'grdLTR.DataSource = dt
            'ApplyGridSetting()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub DisplayLTREdit(ByVal TestReq_Id As Integer, ByVal TestReq_Detail_Id As Integer)
        Try

            Dim str As String = String.Empty
            Dim dt As New DataTable
            str = "SELECT LRM.TestReq_Id , LRD.TestReq_Detail_Id, LRD.TestReq_ItemId, LRD.TestReq_ItemCode, LRD.TestReq_Item,  LRD.TestReq_Qty, LRD.TestReq_Batch, LRD.TestReq_NoofContainers, LRD.TestReq_Comments,  LRM.TestReq_Date , LRM.TestReq_DocNo,LRM.TestReq_Supplier,LRM.TestReq_PONo,LRM.TestReq_GRNNo,LRM.TestReq_LCNo,LRM.TestReq_Department, vwCOADetail.detail_title, PurchaseOrderMasterTable.PurchaseOrderNo, " _
            & " ReceivingNoteMasterTable.ReceivingNo, tblLetterOfCredit.LCdoc_No, EmployeeDeptDefTable.EmployeeDeptName, LRM.TestReq_Stage, LRM.TestReq_Remarks, LRD.TestReq_RetestDate, LRD.TestReq_MfgDate, LRD.TestReq_ExpDate , LRM.TicketId" _
            & " FROM ReceivingNoteMasterTable RIGHT OUTER JOIN " _
            & " LabRequestMasterTable AS LRM Inner Join " _
            & " LabRequestDetailTable LRD ON LRM.TestReq_Id = LRD.TestReq_Id LEFT OUTER JOIN " _
            & " EmployeeDeptDefTable ON LRM.TestReq_Department = EmployeeDeptDefTable.EmployeeDeptId LEFT OUTER JOIN " _
            & " tblLetterOfCredit ON LRM.TestReq_LCNo = tblLetterOfCredit.LCdoc_Id ON ReceivingNoteMasterTable.ReceivingNoteId = LRM.TestReq_GRNNo LEFT OUTER JOIN" _
            & " PurchaseOrderMasterTable ON LRM.TestReq_PONo = PurchaseOrderMasterTable.PurchaseOrderId LEFT OUTER JOIN " _
            & " vwCOADetail ON LRM.TestReq_Supplier = vwCOADetail.coa_detail_id Where LRM.TestReq_Id = " & TestReq_Id & " And LRD.TestReq_Detail_Id=" & TestReq_Detail_Id & "" _
            & " Order by LRD.TestReq_Detail_Id Desc"

            dt = GetDataTable(str)
            dt.AcceptChanges()
            Me.txtRequestNo.Text = dt.Rows.Item(0).Item("TestReq_DocNo").ToString ''Me.grdLTR.GetRow.Cells("TestReq_DocNo").Value.ToString
            'Me.dtpTime.Value = Me.grdLTR.GetRow.Cells("TestReq_Date").Value
            Me.txtProductMatName.Text = dt.Rows.Item(0).Item("TestReq_Item").ToString ''Me.grdLTR.GetRow.Cells("TestReq_Item").Value.ToString
            Me.txtGRNNo.Text = dt.Rows.Item(0).Item("ReceivingNo").ToString '' Me.grdLTR.GetRow.Cells("TestReq_GRNNo").Value.ToString
            Me.txtPOLCNo.Text = dt.Rows.Item(0).Item("LCdoc_No").ToString ''Me.grdLTR.GetRow.Cells("LCdoc_No").Value.ToString
            Me.cmbStage.Text = dt.Rows.Item(0).Item("TestReq_Stage").ToString ''Me.grdLTR.GetRow.Cells("TestReq_Stage").Value.ToString
            Me.txtBatchNo.Text = dt.Rows.Item(0).Item("TestReq_Batch").ToString ''Me.grdLTR.GetRow.Cells("TestReq_Batch").Value.ToString
            Me.txtSuppliers.Text = dt.Rows.Item(0).Item("detail_title").ToString ''Me.grdLTR.GetRow.Cells("detail_title").Value.ToString
            Me.txtRemarsk.Text = dt.Rows.Item(0).Item("TestReq_Remarks").ToString ''Me.grdLTR.GetRow.Cells("TestReq_Remarks").Value.ToString
            Me.cmbTicket.SelectedValue = Val(dt.Rows.Item(0).Item("TicketId").ToString)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            '    Me.grdLTR.RootTable.Columns("TestReq_Id").Visible = False
            '    Me.grdLTR.RootTable.Columns("TestReq_Supplier").Visible = False
            '    Me.grdLTR.RootTable.Columns("TestReq_PONo").Visible = False
            '    Me.grdLTR.RootTable.Columns("TestReq_GRNNo").Visible = False
            '    Me.grdLTR.RootTable.Columns("TestReq_LCNo").Visible = False
            '    Me.grdLTR.RootTable.Columns("TestReq_Department").Visible = False

            Me.grdLTR.RootTable.Columns("TestReq_MfgDate").FormatString = str_DisplayDateFormat
            Me.grdLTR.RootTable.Columns("TestReq_ExpDate").FormatString = str_DisplayDateFormat
            Me.grdLTR.RootTable.Columns("TestReq_RetestDate").FormatString = str_DisplayDateFormat
            Me.grdLTR.RootTable.Columns("TestReq_Date").FormatString = str_DisplayDateFormat


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub LoadLabRequest()
        Try
            'LRM.TestReq_Id, LRD.TestReq_ItemId, LRD.TestReq_ItemCode, LRD.TestReq_Item, LRD.TestReq_Qty, LRD.TestReq_Batch, LRD.TestReq_NoofContainers, LRD.TestReq_Comments, LRM.TestReq_Date, LRM.TestReq_DocNo, LRM.TestReq_Supplier, LRM.TestReq_PONo, LRM.TestReq_GRNNo, LRM.TestReq_LCNo, LRM.TestReq_Department, vwCOADetail.detail_title, PurchaseOrderMasterTable.PurchaseOrderNo, " _"
            '& " ReceivingNoteMasterTable.ReceivingNo, tblLetterOfCredit.LCdoc_No, EmployeeDeptDefTable.EmployeeDeptName, LRM.TestReq_Stage, LRM.TestReq_Remarks " _
            If grdLTR.RowCount = 0 Then Exit Sub
            Me.txtRequestNo.Text = Me.grdLTR.GetRow.Cells("TestReq_DocNo").Value.ToString
            'Me.dtpTime.Value = Me.grdLTR.GetRow.Cells("TestReq_Date").Value
            Me.txtProductMatName.Text = Me.grdLTR.GetRow.Cells("TestReq_Item").Value.ToString
            Me.txtGRNNo.Text = Me.grdLTR.GetRow.Cells("ReceivingNo").Value.ToString
            Me.txtPOLCNo.Text = Me.grdLTR.GetRow.Cells("LCdoc_No").Value.ToString
            Me.txtBatchNo.Text = Me.grdLTR.GetRow.Cells("TestReq_Batch").Value.ToString
            Me.txtSuppliers.Text = Me.grdLTR.GetRow.Cells("detail_title").Value.ToString
            Me.txtRemarsk.Text = Me.grdLTR.GetRow.Cells("TestReq_Remarks").Value.ToString
            Me.txtQtyBatchSize.Text = Me.grdLTR.GetRow.Cells("TestReq_Qty").Value.ToString
            If Me.grdLTR.GetRow.Cells("TicketId").Value.ToString() <> "0" Or Me.grdLTR.GetRow.Cells("TicketId").Value.ToString() <> "" Then
                Int32.TryParse(Me.grdLTR.GetRow.Cells("TicketId").Value.ToString, Me.cmbTicket.SelectedValue)
            Else
                Me.cmbTicket.SelectedValue = 0
            End If
            If Me.grdLTR.GetRow.Cells("prod_step").Value.ToString() <> "0" Or Me.grdLTR.GetRow.Cells("prod_step").Value.ToString() <> "" Then
                Me.cmbStage.Text = Me.grdLTR.GetRow.Cells("prod_step").Value.ToString
            Else
                Me.cmbStage.SelectedValue = 0
            End If

            'Me.cmbStage.Text = Me.grdLTR.GetRow.Cells("TestReq_Stage").Value.ToString
            If (Me.grdLTR.GetRow.Cells("TestReq_MfgDate").Value.ToString() <> "") Then
                Me.dtpMfgDate.Value = Me.grdLTR.GetRow.Cells("TestReq_MfgDate").Value
            End If
            If Me.grdLTR.GetRow.Cells("TestReq_ExpDate").Value.ToString() <> "" Then
                Me.dtoExpDate.Value = Me.grdLTR.GetRow.Cells("TestReq_ExpDate").Value
            End If
            If Me.grdLTR.GetRow.Cells("TestReq_RetestDate").Value.ToString() <> "" Then
                Me.dtpRefreshDate.Value = Me.grdLTR.GetRow.Cells("TestReq_RetestDate").Value
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdLTR_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdLTR.FormattingRow

    End Sub

    Private Sub frmObservationSample_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.GetAllRecords()
            Me.DisplayLTR()
            Me.ReSetControls()
            Timer1.Interval = (5 * 60000)
            Timer1.Enabled = True

            Dim sqlQuery As String = "  select PlanTicketsMasterID, BatchNo As TicketNo  from  " &
                  "  (select  PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.BatchNo , " &
                  "   isnull( sum(Quantity),0) as TicketQty , " &
                  "   isnull( sum(ProductionMasterTable.TotalQty),0) as ProductionQty from PlanTicketsMaster " &
                  "  left join  PlanTicketsDetail on PlanTicketsDetail.PlanTicketsMasterID = PlanTicketsMaster.PlanTicketsMasterID " &
                  "  left join ProductionMasterTable on ProductionMasterTable.PlanTicketId = PlanTicketsMaster.PlanTicketsMasterID " &
                  "  group by  PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.BatchNo ) as fd  " &
                  "    where ProductionQty < TicketQty "
            FillDropDown(cmbTicket, sqlQuery)

            Dim SqlQuery2 As String = "SELECT Distinct CollectedBy , CollectedBy FROM LabTestSampleObvMaster where CollectedBy <> ''"
            FillDropDown(cmbBy, SqlQuery2, False)

            Me.cmbBy.Text = ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            'Ali Faisal : Set auto Request number Normal,Yearly,Monthly on Configuration Bases on 18-June-2016
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("QC" + "-" + Microsoft.VisualBasic.Right(Me.dtpObserDate.Value.Year, 2) + "-", "LabTestSampleObvMaster", "QcNumber")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("QC" & "-" & Format(Me.dtpObserDate.Value, "yy") & Me.dtpObserDate.Value.Month.ToString("00"), 4, "LabTestSampleObvMaster", "QcNumber")
            Else
                Return GetNextDocNo("QC", 6, "LabTestSampleObvMaster", "QcNumber")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub grdLTR_Click(sender As Object, e As EventArgs) Handles grdLTR.Click
        Try
            Me.LabTestRequestGridClick()
            LoadLabRequest()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Dim str As String = String.Empty
        Try

            str = String.Empty
            str = "Delete FROM LabTestSampleObvDetail Where ObserverSampleMasterID=" & Me.grdSaved.GetRow.Cells("ObserverSampleMasterID").Value & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From LabTestSampleObvMaster Where ObserverSampleMasterID=" & Me.grdSaved.GetRow.Cells("ObserverSampleMasterID").Value & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Try
            '            //LabTestSampleObvMaster
            'ObserverSampleMasterID	int	Unchecked
            'QcNumber	nvarchar(50)	Checked
            'ObserverDate	datetime	Checked
            'CollectionDate	datetime	Checked
            'CollectionTime	datetime	Checked
            'CollectedBy	nvarchar(50)	Checked
            'SampleQuantity	int	Checked
            'ContainerNo	float	Checked
            '            TestReq_Detail_Id()
            str = "SELECT     LabTestSampleObvMaster.ObserverSampleMasterID, LabTestSampleObvMaster.QcNumber, LabTestSampleObvMaster.ObserverDate, LabTestSampleObvMaster.CollectionDate, " _
            & " LabTestSampleObvMaster.CollectionTime, LabTestSampleObvMaster.CollectedBy, LabTestSampleObvMaster.SampleQuantity, LabTestSampleObvMaster.ContainerNo, " _
            & " LabTestSampleObvMaster.TestReq_Detail_Id, LabTestSampleObvMaster.TestReq_Id, LabTestSampleObvMaster.MfgDate, LabTestSampleObvMaster.ExpDate, LabRequestDetailTable.TestReq_RetestDate, LabRequestDetailTable.TestReq_Qty , ReceivingNoteMasterTable.ReceivingNo" _
            & " FROM  LabTestSampleObvMaster INNER JOIN LabRequestDetailTable ON LabTestSampleObvMaster.TestReq_Detail_Id = LabRequestDetailTable.TestReq_Detail_Id INNER JOIN LabRequestMasterTable ON LabTestSampleObvMaster.TestReq_Id = LabRequestMasterTable.TestReq_Id LEFT OUTER JOIN ReceivingNoteMasterTable ON LabRequestMasterTable.TestReq_GRNNo = ReceivingNoteMasterTable.ReceivingNoteId order by LabTestSampleObvMaster.ObserverSampleMasterID Desc"

            'str = "SELECT LabTestSampleObvMaster.ObserverSampleMasterID, LabTestSampleObvMaster.QcNumber, LabTestSampleObvMaster.ObserverDate, LabTestSampleObvMaster.CollectionDate,LabTestSampleObvMaster.CollectionTime, LabTestSampleObvMaster.CollectedBy, LabTestSampleObvMaster.SampleQuantity, LabTestSampleObvMaster.ContainerNo,LabTestSampleObvMaster.TestReq_Detail_Id, LabTestSampleObvMaster.TestReq_Id, LabTestSampleObvMaster.MfgDate, LabTestSampleObvMaster.ExpDate FROM  LabTestSampleObvMaster "

            dt = GetDataTable(str)
            dt.AcceptChanges()
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()

            Me.grdSaved.RootTable.Columns("ObserverDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("CollectionDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("MfgDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("ExpDate").FormatString = str_DisplayDateFormat

            Me.grdSaved.RootTable.Columns("ObserverSampleMasterID").Visible = False
            Me.grdSaved.RootTable.Columns("TestReq_Detail_Id").Visible = False
            Me.grdSaved.RootTable.Columns("TestReq_Id").Visible = False
            Me.grdSaved.RootTable.Columns("QcNumber").Caption = "QC Number"
            Me.grdSaved.RootTable.Columns("ObserverDate").Caption = "Observation Date"
            Me.grdSaved.RootTable.Columns("CollectionDate").Caption = "Collection Date"
            Me.grdSaved.RootTable.Columns("CollectionTime").Caption = "Collection Time"
            Me.grdSaved.RootTable.Columns("CollectionTime").FormatString = "hh:mm:ss"
            Me.grdSaved.RootTable.Columns("CollectedBy").Caption = "Collected By"
            Me.grdSaved.RootTable.Columns("SampleQuantity").Caption = "Sample Quantity"
            Me.grdSaved.RootTable.Columns("ContainerNo").Caption = "Container No"
            Me.grdSaved.RootTable.Columns("MfgDate").Caption = "Mfg. Date"
            Me.grdSaved.RootTable.Columns("ExpDate").Caption = "Exp. Date"
            Me.grdSaved.RootTable.Columns("TestReq_RetestDate").Visible = False
            Me.grdSaved.RootTable.Columns("TestReq_Qty").Visible = False
            Me.grdSaved.RootTable.Columns("ReceivingNo").Visible = False
            Me.grdSaved.AutoSizeColumns()



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            ''Lab Test Request
            'Me.txtRequestNo.Text = ""
            'Me.dtpDate.Value = Now
            'Me.dtpTime.Value = Now
            'Me.txtProductMatName.Text = ""
            'Me.txtGRNNo.Text = ""
            'Me.txtPOLCNo.Text = ""
            'Me.dtpRefreshDate.Value = Now
            'Me.txtStage.Text = ""
            'Me.txtBatchNo.Text = ""
            'Me.dtpMfgDate.Value = Now
            'Me.txtSuppliers.Text = ""
            'Me.txtQtyBatchSize.Text = ""
            'Me.dtoExpDate.Value = Now
            'Me.txtRemarsk.Text = ""
            If Me.txtRequestNo.Text = "" Then
                ShowErrorMessage("Request No field is empty")
                Return False
            End If
            ''QC Section
            If Me.txtQCNumber.Text = String.Empty Then
                ShowErrorMessage("QC Number is required")
                Return False
            End If
            If Me.cmbBy.Text = String.Empty Then
                ShowErrorMessage("User Name is required")
                Return False
            End If
            If Me.txtQuantityOfSample.Text = String.Empty Then
                ShowErrorMessage("Sample Quantity is required")
                Return False
            End If
            If Me.grdObservationParameters.RowCount = 0 Then
                ShowErrorMessage("No record is found in the grid")
                Return False
            End If
            Return True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Dim SqlQuery2 As String = "SELECT Distinct CollectedBy , CollectedBy FROM LabTestSampleObvMaster where CollectedBy <> ''"
            FillDropDown(cmbBy, SqlQuery2, False)

            Dim sqlQuery As String = "  select PlanTicketsMasterID, BatchNo As TicketNo  from  " &
                  "  (select  PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.BatchNo , " &
                  "   isnull( sum(Quantity),0) as TicketQty , " &
                  "   isnull( sum(ProductionMasterTable.TotalQty),0) as ProductionQty from PlanTicketsMaster " &
                  "  left join  PlanTicketsDetail on PlanTicketsDetail.PlanTicketsMasterID = PlanTicketsMaster.PlanTicketsMasterID " &
                  "  left join ProductionMasterTable on ProductionMasterTable.PlanTicketId = PlanTicketsMaster.PlanTicketsMasterID " &
                  "  group by  PlanTicketsMaster.PlanTicketsMasterID, PlanTicketsMaster.BatchNo ) as fd  " &
                  "    where ProductionQty < TicketQty "
            FillDropDown(cmbTicket, sqlQuery)

            Me.btnSave.Text = "&Save"
            Me.txtRequestNo.Text = ""
            Me.dtpDate.Value = Now
            Me.dtpTime.Value = Now
            Me.txtProductMatName.Text = ""
            Me.txtGRNNo.Text = ""
            Me.txtPOLCNo.Text = ""
            Me.dtpRefreshDate.Value = Now
            'Ali Faisal : TFS3849 : Re-test date and Product Parameter configuration changes on Lab Testing module.
            Me.dtpRefreshDate.Checked = False
            'End
            Me.cmbStage.Text = ""
            Me.txtBatchNo.Text = ""
            Me.dtpMfgDate.Value = Now
            Me.dtpMfgDate.Checked = False
            Me.txtSuppliers.Text = ""
            Me.txtQtyBatchSize.Text = ""
            Me.dtoExpDate.Value = Now
            Me.dtoExpDate.Checked = False
            Me.txtRemarsk.Text = ""
            ''QC
            Me.txtQCNumber.Text = GetDocumentNo()
            Me.dtpObserDate.Value = Now
            Me.dtpSamplecollectedon.Value = Now
            Me.dtpQCTime.Value = Now
            Me.cmbBy.Text = ""
            cmbTicket.SelectedIndex = 0
            Me.txtQuantityOfSample.Text = ""
            Me.txtContainerNo.Text = ""
            Me.GetAllRecords()
            Me.DisplayLTR()
            DisplayDetail(-1)
            Me.GetAllParameters()
            Me.grdLTR.DataSource = dtLabTestRequest
            ApplyGridSetting()
            'GetAllParameters()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub LabTestRequestGridClick(Optional Condition As String = "")
        Try
            Me.btnSave.Text = "&Save"
            Me.txtRequestNo.Text = ""
            Me.dtpDate.Value = Now
            Me.dtpTime.Value = Now
            Me.txtProductMatName.Text = ""
            Me.txtGRNNo.Text = ""
            Me.txtPOLCNo.Text = ""
            Me.dtpRefreshDate.Value = Now
            'Ali Faisal : TFS3849 : Re-test date and Product Parameter configuration changes on Lab Testing module.
            Me.dtpRefreshDate.Checked = False
            'End
            Me.cmbStage.Text = ""
            Me.txtBatchNo.Text = ""
            Me.dtpMfgDate.Value = Now
            Me.dtpMfgDate.Checked = False
            Me.txtSuppliers.Text = ""
            Me.txtQtyBatchSize.Text = ""
            Me.dtoExpDate.Value = Now
            Me.dtoExpDate.Checked = False
            Me.txtRemarsk.Text = ""
            ''QC
            Me.txtQCNumber.Text = GetDocumentNo()
            Me.dtpObserDate.Value = Now
            Me.dtpSamplecollectedon.Value = Now
            Me.dtpQCTime.Value = Now
            Me.cmbBy.Text = ""
            Me.txtQuantityOfSample.Text = ""
            Me.txtContainerNo.Text = ""
            Me.GetAllRecords()
            'Me.DisplayLTR()
            DisplayDetail(-1)
            Me.GetAllParameters()
            'Me.grdLTR.DataSource = dtLabTestRequest
            'GetAllParameters()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Dim com As New OleDbCommand
        Dim connection As OleDbConnection
        Dim masterId As Integer = 0I
        Dim trans As OleDbTransaction
        '' Try
        connection = Con
        If connection.State = ConnectionState.Open Then connection.Close()
        connection.Open()
        com.Connection = connection
        trans = connection.BeginTransaction
        Try
            com.Transaction = trans
            com.CommandText = ""
            com.CommandText = "Insert into LabTestSampleObvMaster(QcNumber, ObserverDate, CollectionDate, CollectionTime, CollectedBy, SampleQuantity, ContainerNo, TestReq_Detail_Id, TestReq_Id, MfgDate, ExpDate) Values( " _
                          & " N'" & Me.txtQCNumber.Text.Replace("'", "''") & "', N'" & Me.dtpObserDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & Me.dtpSamplecollectedon.Value.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & Me.dtpQCTime.Value.ToString("h:mm:ss tt") & "', N'" & Me.cmbBy.Text.Replace("'", "''") & "', N'" & Me.txtQuantityOfSample.Text.Replace("'", "''") & "', N'" & Me.txtContainerNo.Text.Replace("'", "''") & "', " & Val(Me.grdLTR.GetRow.Cells("TestReq_Detail_Id").Value.ToString) & ", " & Val(Me.grdLTR.GetRow.Cells("TestReq_Id").Value.ToString) & ", " & IIf(Me.dtpMfgDate.Checked = True, "Convert(Datetime,'" & Me.dtpMfgDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102)", "Null") & ", " & IIf(Me.dtoExpDate.Checked = True, "Convert(Datetime,'" & Me.dtoExpDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102)", "Null") & ") Select @@Identity"
            masterId = com.ExecuteScalar()
            For i As Int32 = 0 To Me.grdObservationParameters.RowCount - 1
                com.CommandText = ""
                com.CommandText = "Insert into LabTestSampleObvDetail(ObserverSampleMasterID, LabTestingParameterID, Status, Comment) Values(" & masterId & ", " & Val(Me.grdObservationParameters.GetRows(i).Cells("Parameter_Id").Value.ToString) & ", " & IIf(Me.grdObservationParameters.GetRows(i).Cells("Ok").Value.ToString = "True", 1, 0) & ", N'" & Me.grdObservationParameters.GetRows(i).Cells("Comments").Value.ToString & "')"
                com.ExecuteNonQuery()
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
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
        Dim com As New OleDbCommand
        Dim connection As OleDbConnection
        Dim masterId As Integer = 0I
        connection = Con
        If connection.State = ConnectionState.Open Then connection.Close()
        connection.Open()
        Dim trans As OleDbTransaction
        trans = connection.BeginTransaction
        Try
            com.Connection = connection

            com.Transaction = trans
            com.CommandText = ""
            com.CommandText = "Update LabTestSampleObvMaster Set QcNumber = N'" & Me.txtQCNumber.Text.Replace("'", "''") & "' , ObserverDate =N'" & Me.dtpObserDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', CollectionDate = N'" & Me.dtpSamplecollectedon.Value.ToString("yyyy-M-d h:mm:ss tt") & "' , CollectionTime = N'" & Me.dtpQCTime.Value.ToString("h:mm:ss tt") & "' , CollectedBy = N'" & Me.cmbBy.Text.Replace("'", "''") & "' , SampleQuantity = N'" & Me.txtQuantityOfSample.Text.Replace("'", "''") & "', ContainerNo = N'" & Me.txtContainerNo.Text.Replace("'", "''") & "', MfgDate = " & IIf(Me.dtpMfgDate.Checked = True, "Convert(Datetime,'" & Me.dtpMfgDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102)", "Null") & ", ExpDate = " & IIf(Me.dtoExpDate.Checked = True, "Convert(Datetime,'" & Me.dtoExpDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102)", "Null") & " Where ObserverSampleMasterID=" & Val(Me.grdSaved.GetRow.Cells("ObserverSampleMasterID").Value.ToString) & " "
            com.ExecuteNonQuery()
            masterId = Me.grdSaved.GetRow.Cells("ObserverSampleMasterID").Value

            com.CommandText = ""
            com.CommandText = "Delete From LabTestSampleObvDetail Where ObserverSampleMasterID = " & masterId & ""
            com.ExecuteNonQuery()

            For i As Int32 = 0 To Me.grdObservationParameters.RowCount - 1
                com.CommandText = ""
                com.CommandText = "Insert into LabTestSampleObvDetail(ObserverSampleMasterID, LabTestingParameterID, Status, Comment) Values(" & masterId & ", " & Val(Me.grdObservationParameters.GetRows(i).Cells("Parameter_Id").Value.ToString) & ", " & IIf(Me.grdObservationParameters.GetRows(i).Cells("Ok").Value = True, 1, 0) & ", N'" & Me.grdObservationParameters.GetRows(i).Cells("Comments").Value.ToString & "')"
                com.ExecuteNonQuery()
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            DisplayLTR()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Timer1.Enabled = False
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Timer1.Enabled = False
        End Try
    End Sub
    Private Sub DisplayDetail(ByVal ObserverSampleMasterID As Integer)
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Try
            '//LabTestSampleObvDetail
            '            'ObserverSampleDetailID	int	Unchecked
            '            'ObserverSampleMasterID	int	Checked
            '            'LabTestingParameterID	int	Checked
            '            'Status	bit	Checked
            '            'Comment	text	Checked

            '            //LabSampleParameters
            'Parameter_Id	int	Unchecked
            'Parameter_Name	nvarchar(100)	Checked
            'Parameter_Ok	nvarchar(100)	Checked
            'Parameter_NotOk	nvarchar(100)	Checked
            'Parameter_Comments	nvarchar(100)	Checked
            str = "Select Parameters.Parameter_Id, Parameters.Parameter_Name, Detail.[Status] As Ok, Detail.Comment As Comments " _
                & "From LabTestSampleObvDetail As Detail Inner Join LabSampleParameters As Parameters On Detail.LabTestingParameterID = Parameters.Parameter_Id " _
                & " Where Detail.ObserverSampleMasterID = " & ObserverSampleMasterID & ""
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Me.grdObservationParameters.DataSource = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub EditRecord()
        Try
            '' ObserverSampleMasterID, QcNumber, ObserverDate, CollectionDate, CollectionTime, CollectedBy, SampleQuantity, ContainerNo, TestReq_Detail_Id, TestReq_Id
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Me.btnSave.Text = "&Update"
            Me.DisplayLTREdit(Val(Me.grdSaved.GetRow.Cells("TestReq_Id").Value.ToString), Val(Me.grdSaved.GetRow.Cells("TestReq_Detail_Id").Value.ToString))
            Me.txtQCNumber.Text = Me.grdSaved.GetRow.Cells("QcNumber").Value.ToString
            Me.dtpObserDate.Value = Me.grdSaved.GetRow.Cells("ObserverDate").Value.ToString
            Me.dtpSamplecollectedon.Value = Me.grdSaved.GetRow.Cells("CollectionDate").Value
            Me.dtpQCTime.Value = Me.grdSaved.GetRow.Cells("CollectionTime").Value
            Me.cmbBy.Text = Me.grdSaved.GetRow.Cells("CollectedBy").Value.ToString
            Me.txtQuantityOfSample.Text = Me.grdSaved.GetRow.Cells("SampleQuantity").Value.ToString
            Me.txtContainerNo.Text = Me.grdSaved.GetRow.Cells("ContainerNo").Value.ToString
            Me.DisplayDetail(Val(Me.grdSaved.GetRow.Cells("ObserverSampleMasterID").Value.ToString))

            If Not Me.grdSaved.GetRow.Cells("MfgDate").Value.ToString = String.Empty Then

                Me.dtpMfgDate.Value = Me.grdSaved.GetRow.Cells("MfgDate").Value


            Else

                Me.dtpMfgDate.Checked = False

            End If

            If Not Me.grdSaved.GetRow.Cells("ExpDate").Value.ToString = String.Empty Then

                Me.dtoExpDate.Value = Me.grdSaved.GetRow.Cells("ExpDate").Value

            Else

                Me.dtoExpDate.Checked = False

            End If

            If Not Me.grdSaved.GetRow.Cells("TestReq_RetestDate").Value.ToString = String.Empty Then

                Me.dtpRefreshDate.Value = Me.grdSaved.GetRow.Cells("TestReq_RetestDate").Value

                'Ali Faisal : TFS3849 : Re-test date and Product Parameter configuration changes on Lab Testing module.
            Else

                Me.dtpRefreshDate.Checked = False

                'End
            End If


            Me.txtQtyBatchSize.Text = Me.grdSaved.GetRow.Cells("TestReq_Qty").Value.ToString

            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Not IsValidate() = True Then Exit Sub
            Me.grdObservationParameters.UpdateData()
            If btnSave.Text = "&Save" Or btnSave.Text = "Save" Then
                If Save() = True Then
                    'If BackgroundWorker1.IsBusy Then Exit Sub
                    'BackgroundWorker1.RunWorkerAsync()
                    ReSetControls()
                End If

            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                If Update1() = True Then
                    'If BackgroundWorker1.IsBusy Then Exit Sub
                    'BackgroundWorker1.RunWorkerAsync()
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            'If Not IsValidate() = True Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) Then Exit Sub
            If Delete() Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnFormPrint_Click(sender As Object, e As EventArgs) Handles btnFormPrint.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("QcNumber").Value.ToString ''ObserverSampleMasterID, QcNumber
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            AddRptParam("@RequestId", Me.grdSaved.GetRow.Cells("TestReq_Id").Value)
            AddRptParam("@RequestDetailId", Me.grdSaved.GetRow.Cells("TestReq_Detail_Id").Value)
            ShowReport("rptLabObsvrSample") ''rptLabSampledSticker
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnPrintSticker_Click(sender As Object, e As EventArgs) Handles btnPrintSticker.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("QcNumber").Value.ToString ''ObserverSampleMasterID, QcNumber
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            AddRptParam("@RequestId", Me.grdSaved.GetRow.Cells("TestReq_Id").Value)
            AddRptParam("@RequestDetailId", Me.grdSaved.GetRow.Cells("TestReq_Detail_Id").Value)
            ShowReport("rptLabSampledSticker") ''rptLabSampledSticker
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnLabTestRequest_Click(sender As Object, e As EventArgs) Handles btnLabTestRequest.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdLTR.GetRow.Cells("TestReq_DocNo").Value.ToString ''ObserverSampleMasterID, QcNumber
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            AddRptParam("@RequestId", Me.grdLTR.GetRow.Cells("TestReq_Id").Value) ''TestReq_Detail_Id, TestReq_Id
            AddRptParam("@RequestDetailId", Me.grdLTR.GetRow.Cells("TestReq_Detail_Id").Value)
            ShowReport("rptLabTestRequestQC") ''rptLabSampledSticker
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            Me.grdLTR.DataSource = dtLabTestRequest
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            If grdSaved.Row < 0 Then
                Exit Sub
            Else
                If Me.grdObservationParameters.RowCount > 0 Then
                    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                    EditRecord()
                Else
                    EditRecord()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            If grdSaved.Row < 0 Then
                Exit Sub
            Else
                If Me.grdObservationParameters.RowCount > 0 Then
                    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                    EditRecord()
                Else
                    EditRecord()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQuantityOfSample_KeyDown(sender As Object, e As KeyEventArgs) Handles txtQuantityOfSample.KeyDown

    End Sub

    Private Sub txtQuantityOfSample_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQuantityOfSample.KeyPress
        NumValidation(sender, e)
    End Sub
End Class