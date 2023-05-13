Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports SBUtility
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Public Class frmResult

    Dim IsEditMode As Boolean = False
    Dim CurrentId As Integer
    Dim PrintLog As PrintLogBE
    Dim ItemId As String = String.Empty
    Dim ResultStatus As Integer = 1

    Enum grdenm
        ResultDetailID
        ResultID
        ParameterID
        Specification
        Result_Comments
    End Enum

    Private Sub frmResult_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            'Ali Faisal : System UserFriendly modification : to FillCombo and Refresh Controls at Form shown on 27-June-2016
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            FillComboBox("Vendor")
            FillComboBox("GRN")
            FillComboBox("QC")
            RefreshControls()
            DisplayRecord()
            DisplayDetail(-1)
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub RefreshControls()
        Try
            'Ali Faisal : Changes to refresh Controls on 27-June-2016
            Me.dtpResultDate.Value = Now
            Me.dtpResultDate.Enabled = True
            Me.txtResultNo.Text = GetDocumentNo()
            Me.cmbSupplier.Rows(0).Activate()
            Me.cmbGRNNo.SelectedIndex = 0
            Me.cmbQCNo.SelectedIndex = 0
            Me.txtContainerType.Text = ""
            Me.txtNoofContainer.Text = ""
            Me.txtBatchNo.Text = ""
            Me.txtBatchSize.Text = ""
            Me.txtDRNo.Text = ""
            Me.txtPackSize.Text = ""
            Me.txtQCSampled.Text = ""
            Me.txtAnalyticalMethod.Text = ""
            Me.txtProSpecNo.Text = ""
            Me.dtpSampledDate.Value = Now
            Me.dtpExpDate.Value = Now
            Me.dtpMfgDate.Value = Now
            Me.txtRemarks.Text = ""
            Me.rdbApproved.Checked = False
            Me.rdbRejected.Checked = False
            Me.btnSave.Text = "&Save"
            Me.cmbSupplier.Focus()
            IsEditMode = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            'Ali Faisal : Set auto result entry number Normal,Yearly,Monthly on Configuration Bases on 24-June-2016
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("RE" + "-" + Microsoft.VisualBasic.Right(Me.dtpResultDate.Value.Year, 2) + "-", "LabResultMasterTable", "ResultNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("RE" & "-" & Format(Me.dtpResultDate.Value, "yy") & Me.dtpResultDate.Value.Month.ToString("00"), 4, "LabResultMasterTable", "ResultNo")
            Else
                Return GetNextDocNo("RE", 6, "LabResultMasterTable", "ResultNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub FillComboBox(ByVal strCondition As String)
        Try
            'Ali Faisal : Fill all combos on 24-June-2016
            Dim str As String = String.Empty
            If strCondition = "Vendor" Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                                 "dbo.tblListTerritory.TerritoryName as Territory,dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                                                 "FROM dbo.tblVendor INNER JOIN " & _
                                                 "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                                 "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                                 "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                                 "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                                 "WHERE     (dbo.vwCOADetail.account_type ='Vendor') "
                FillUltraDropDown(Me.cmbSupplier, str)

            ElseIf strCondition = "QC" Then
                str = "Select ObserverSampleMasterId, QcNumber+ ' ~ ' + Convert(Varchar(12), ObserverDate, 113) as QCNumber, IsNull(LabRequestDetailTable.TestReq_ItemId, 0) As ItemId, LabRequestMasterTable.TestReq_Supplier, LabRequestDetailTable.TestReq_Qty, LabRequestDetailTable.TestReq_Batch, LabRequestDetailTable.TestReq_NoofContainers, LabRequestDetailTable.TestReq_MfgDate, LabRequestDetailTable.TestReq_ExpDate,LabTestSampleObvMaster.SampleQuantity " _
                    & " from LabTestSampleObvMaster LEFT OUTER JOIN LabRequestDetailTable ON LabTestSampleObvMaster.TestReq_Detail_Id = LabRequestDetailTable.TestReq_Detail_Id " _
                    & " LEFT OUTER JOIN LabRequestMasterTable ON LabTestSampleObvMaster.TestReq_Id = LabRequestMasterTable.TestReq_Id Where LabRequestMasterTable.TestReq_Supplier = " & Me.cmbSupplier.Value & " Order By ObserverSampleMasterId DESC "
                FillDropDown(Me.cmbQCNo, str)

            ElseIf strCondition = "GRN" Then
                str = "Select ReceivingNoteMasterTable.ReceivingNoteId,ReceivingNoteMasterTable.ReceivingNo  + ' ~ ' + Convert(Varchar(12), ReceivingDate, 113) as ReceivingNo,* From ReceivingNoteMasterTable WHERE ReceivingNoteMasterTable.ReceivingNoteId Not In(Select IsNull(ReceivingNoteId,0) From ReceivingMasterTable) And ReceivingNoteMasterTable.VendorId = " & Me.cmbSupplier.Value & "  ORDER BY ReceivingNoteMasterTable.ReceivingNoteId DESC"
                FillDropDown(Me.cmbGRNNo, str)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayItemParameters(ByVal ItemId As Integer)
        Try
            ''LabParameterMasterTable
            'ParameterId	int	Unchecked
            'Date	datetime	Checked
            'ParameterNo	nvarchar(50)	Checked
            'ItemId	int	Checked
            '            Unchecked()

            ''//LabParameterDetailTable
            'ParameterDetailId	int	Unchecked
            'ParameterId	int	Checked
            'ParameterName	nvarchar(50)	Checked
            'Specification	nvarchar(50)	Checked
            '            Unchecked()
            Dim str As String
            str = "Select LabParameterDetailTable.ParameterDetailId As ParameterId , LabParameterDetailTable.ParameterName, " &
 " LabParameterDetailTable.Specification, '' As Result, LabParameterMasterTable.ItemId" &
 " From LabParameterMasterTable left join LabParameterDetailTable " &
   "      ON LabParameterDetailTable.ParameterId = LabParameterMasterTable.ParameterId   Where LabParameterMasterTable.ItemId =" & ItemId & ""
            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            dtDisplayDetail.AcceptChanges()
            Me.grd.DataSource = dtDisplayDetail
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayDetail(ByVal ResultId As Integer)
        Try
            ''LabParameterMasterTable
            'ParameterId	int	Unchecked
            'Date	datetime	Checked
            'ParameterNo	nvarchar(50)	Checked
            'ItemId	int	Checked
            '            Unchecked()

            ''//LabParameterDetailTable
            'ParameterDetailId	int	Unchecked
            'ParameterId	int	Checked
            'ParameterName	nvarchar(50)	Checked
            'Specification	nvarchar(50)	Checked
            '            Unchecked()
            Dim str As String
            str = "Select ParameterDetail.ParameterDetailId As ParameterId, ParameterDetail.ParameterName, LabResultDetailTable.Specification," _
                & " LabResultDetailTable.Result_Comments As Result From LabParameterDetailTable As ParameterDetail" _
                & " INNER JOIN LabResultDetailTable ON ParameterDetail.ParameterDetailId = LabResultDetailTable.ParameterId " _
                & " INNER JOIN LabResultMasterTable ON  LabResultDetailTable.ResultId = LabResultMasterTable.ResultId  Where LabResultMasterTable.ResultId =" & ResultId & ""
            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            dtDisplayDetail.AcceptChanges()
            Me.grd.DataSource = dtDisplayDetail
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayRecord(Optional ByVal strCondition As String = "")
        Try
            Dim str As String = String.Empty
            Dim dt As New DataTable
            str = "SELECT     LabResultMasterTable.ResultID, LabResultMasterTable.ResultNo, LabResultMasterTable.ResultDate, LabResultMasterTable.VendorId, vwCOADetail.detail_code, " _
            & " vwCOADetail.detail_title, LabResultMasterTable.ArticleId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, LabResultMasterTable.ReceivingNoteId, " _
            & " ReceivingNoteMasterTable.ReceivingNo, ReceivingNoteMasterTable.ReceivingDate, LabResultMasterTable.QCId, LabTestSampleObvMaster.QcNumber, " _
            & " LabTestSampleObvMaster.ObserverDate, LabResultMasterTable.SampleDate, LabResultMasterTable.QcSampleQty, LabResultMasterTable.BatchNo, " _
            & " LabResultMasterTable.BatchSize, LabResultMasterTable.ParameterID, LabResultMasterTable.ContainerType, LabResultMasterTable.ContainerNumber, " _
            & " LabResultMasterTable.DrNo, LabResultMasterTable.PackSize, LabResultMasterTable.ProductSpecNo, LabResultMasterTable.AnalyticalNo, " _
            & " LabResultMasterTable.MfgDate, LabResultMasterTable.ExpDate, LabResultMasterTable.Remarks, LabResultMasterTable.ResultStatus " _
            & " FROM         LabResultMasterTable LEFT OUTER JOIN " _
            & " LabTestSampleObvMaster ON LabResultMasterTable.QCId = LabTestSampleObvMaster.ObserverSampleMasterID LEFT OUTER JOIN " _
            & " vwCOADetail ON LabResultMasterTable.VendorId = vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
            & " ReceivingNoteMasterTable ON LabResultMasterTable.ReceivingNoteId = ReceivingNoteMasterTable.ReceivingNoteId LEFT OUTER JOIN " _
            & " ArticleDefView ON LabResultMasterTable.ArticleId = ArticleDefView.ArticleId " _
            & " WHERE (ArticleDefView.Active = '1') " _
            & " Order By LabResultMasterTable.ResultID Desc "
            '& " LabParameterMasterTable ON LabResultMasterTable.ParameterID = LabParameterMasterTable.ParameterId " _
            dt = GetDataTable(str)
            dt.AcceptChanges()
            grdSaved.DataSource = dt
            grdSaved.RetrieveStructure()
            ApplyGridSetting()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            Me.grdSaved.RootTable.Columns("ResultID").Visible = False
            Me.grdSaved.RootTable.Columns("VendorId").Visible = False
            Me.grdSaved.RootTable.Columns("ArticleId").Visible = False
            Me.grdSaved.RootTable.Columns("ReceivingNoteId").Visible = False
            Me.grdSaved.RootTable.Columns("QCId").Visible = False
            Me.grdSaved.RootTable.Columns("ParameterID").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function FormValidate() As Boolean
        Try
            'Ali Faisal : Add function to validate that Vendor is selected and detail grid is not empty on 20-June-2016
            If txtResultNo.Text = "" Then
                msg_Error("Please enter Result No.")
                txtResultNo.Focus() : FormValidate = False : Exit Function
            End If

            If cmbQCNo.SelectedValue <= 0 Then
                If cmbSupplier.ActiveRow.Cells(0).Value <= 0 Then
                    msg_Error("Please select Vendor Or QCNo")
                    cmbSupplier.Focus() : FormValidate = False : Exit Function
                End If
            End If

            If Not Me.grd.RowCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
                cmbSupplier.Focus() : FormValidate = False : Exit Function
            End If
            If rdbApproved.Checked = False And rdbRejected.Checked = False Then

                msg_Error("Please Select Stage Result ")
                Return False
            End If
            Return True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function
    Private Function Save() As Boolean
        Try
            Me.grd.UpdateData()
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

                If rdbApproved.Checked = True Then

                    Me.ResultStatus = 1

                ElseIf rdbApproved.Checked = False Then

                    Me.ResultStatus = 0

                End If


                'Ali Faisal : Insert master record on 27-June-2016
                objCommand.CommandText = " Insert Into LabResultMasterTable (ResultNo,ResultDate,ContainerType,ContainerNumber,BatchNo,BatchSize,DrNo,PackSize,QcSampleQty,SampleDate, " _
                                        & " ProductSpecNo,AnalyticalNo,MfgDate,ExpDate,Remarks,ArticleId, ReceivingNoteId,VendorId,QCId, ResultStatus) " _
                                        & " Values (N'" & txtResultNo.Text & "' ,N'" & dtpResultDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "' ,N'" & txtContainerType.Text & "' , " _
                                        & " N'" & txtNoofContainer.Text & "' ,N'" & txtBatchNo.Text & "' ,N'" & txtBatchSize.Text & "' ,N'" & txtDRNo.Text & "' ,N'" & txtPackSize.Text & "' , " & Val(txtQCSampled.Text) & ",  " _
                                        & " N'" & dtpSampledDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "' , " & txtProSpecNo.Text & " , " & txtAnalyticalMethod.Text & ", " _
                                        & " N'" & dtpMfgDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & dtpExpDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & txtRemarks.Text & "', " _
                                        & " " & Val(ItemId) & ", " & cmbGRNNo.SelectedValue & ", " & cmbSupplier.ActiveRow.Cells(0).Value & ", " & cmbQCNo.SelectedValue & ", " & Me.ResultStatus & ")"
                objCommand.ExecuteNonQuery()
                'Ali Faisal : Insert detail record on 27-June-2016
                For i = 0 To grd.RowCount - 1
                    objCommand.CommandText = ""
                    objCommand.CommandText = " Insert Into LabResultDetailTable (ResultID,ParameterID,Specification,Result_Comments) " _
                                            & " Values (Ident_Current('LabResultMasterTable')," & Val(grd.GetRows(i).Cells("ParameterId").Value) & ", " _
                                            & " N'" & grd.GetRows(i).Cells("Specification").Value.ToString & "' ,N'" & grd.GetRows(i).Cells("Result").Value.ToString & "') "
                    objCommand.ExecuteNonQuery()
                Next
                trans.Commit()
                Save = True
                'DisplayRecord()
            Catch ex As Exception
                trans.Rollback()
                Save = False
                Throw ex
            End Try
        Catch ex As Exception
            ShowErrorMessage("An error occured while saving voucher: " & ex.Message)
        End Try
        'Ali Faisal : Insert Activity Log on 27-June-2016
        SaveActivityLog("LabResult", Me.Text, IIf(Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Lab, Me.CurrentId, True)
    End Function
    Private Function UpdateRecord() As Boolean
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

            If rdbApproved.Checked = True Then

                Me.ResultStatus = 1

            ElseIf rdbApproved.Checked = False Then

                Me.ResultStatus = 0

            End If


            objCommand.CommandText = " Update  LabResultMasterTable Set ResultNo = N'" & txtResultNo.Text & "' , ResultDate = N'" & dtpResultDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "' , " _
                                    & " ContainerType = N'" & txtContainerType.Text & "' ,ContainerNumber = N'" & txtNoofContainer.Text & "' , " _
                                    & " BatchNo = N'" & txtBatchNo.Text & "', BatchSize = N'" & txtBatchSize.Text & "' ,DrNo = N'" & txtDRNo.Text & "' , " _
                                    & " PackSize = N'" & txtPackSize.Text & "', QcSampleQty = " & Val(txtQCSampled.Text) & ", SampleDate = N'" & dtpSampledDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "' , " _
                                    & " ProductSpecNo = " & txtProSpecNo.Text & ", AnalyticalNo = " & txtAnalyticalMethod.Text & ", MfgDate = N'" & dtpMfgDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                    & " ExpDate = N'" & dtpExpDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',Remarks = N'" & txtRemarks.Text & "', " _
                                    & " ArticleId = " & Val(ItemId) & ", ReceivingNoteId = " & cmbGRNNo.SelectedValue & ", VendorId = " & cmbSupplier.ActiveRow.Cells(0).Value & ", QCId = " & cmbQCNo.SelectedValue & ", ResultStatus = " & Me.ResultStatus & ""
            objCommand.ExecuteNonQuery()
            objCommand.CommandText = ""
            objCommand.CommandText = "Delete From LabResultDetailTable Where ResultID =" & Me.grdSaved.GetRow.Cells("ResultID").Value & ""
            objCommand.ExecuteNonQuery()
            For i = 0 To grd.RowCount - 1
                objCommand.CommandText = ""
                objCommand.CommandText = " Insert Into LabResultDetailTable (ResultID,ParameterID,Specification,Result_Comments) " _
                                            & " Values (Ident_Current('LabResultMasterTable')," & Val(grd.GetRows(i).Cells("ParameterId").Value) & ", " _
                                            & " N'" & grd.GetRows(i).Cells("Specification").Value.ToString & "' ,N'" & grd.GetRows(i).Cells("Result").Value.ToString & "' ) "
                objCommand.ExecuteNonQuery()
            Next
            trans.Commit()
            UpdateRecord = True
            'DisplayRecord()
            'Ali Faisal : Insert Activity Log on 27-June-2016
            SaveActivityLog("LabResult", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Lab, Me.txtResultNo.Text.Trim, True)
        Catch ex As Exception
            trans.Rollback()
            UpdateRecord = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try
    End Function
    Private Sub EditRecord()
        Try
            IsEditMode = True
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.dtpResultDate.Value = CType(grdSaved.CurrentRow.Cells("ResultDate").Value, Date)
            Me.txtResultNo.Text = Me.grdSaved.CurrentRow.Cells("ResultNo").Value.ToString
            Me.cmbSupplier.Value = Val(Me.grdSaved.CurrentRow.Cells("VendorId").Value)
            Me.cmbGRNNo.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("ReceivingNoteId").Value)
            Me.cmbQCNo.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("QCId").Value)
            Me.txtContainerType.Text = Me.grdSaved.CurrentRow.Cells("ContainerType").Text.ToString
            Me.txtNoofContainer.Text = Me.grdSaved.CurrentRow.Cells("ContainerNumber").Text.ToString
            Me.txtBatchNo.Text = Me.grdSaved.CurrentRow.Cells("BatchNo").Text.ToString
            Me.txtBatchSize.Text = Me.grdSaved.CurrentRow.Cells("BatchSize").Text.ToString
            Me.txtDRNo.Text = Me.grdSaved.CurrentRow.Cells("DrNo").Text.ToString
            Me.txtPackSize.Text = Me.grdSaved.CurrentRow.Cells("PackSize").Text.ToString
            Me.txtQCSampled.Text = Me.grdSaved.CurrentRow.Cells("QcSampleQty").Text.ToString
            Me.dtpSampledDate.Value = CType(grdSaved.CurrentRow.Cells("SampleDate").Value, Date)
            Me.txtProSpecNo.Text = Me.grdSaved.CurrentRow.Cells("ProductSpecNo").Text.ToString
            Me.txtAnalyticalMethod.Text = Me.grdSaved.CurrentRow.Cells("AnalyticalNo").Text.ToString
            Me.dtpMfgDate.Value = CType(grdSaved.CurrentRow.Cells("MfgDate").Value, Date)
            Me.dtpExpDate.Value = CType(grdSaved.CurrentRow.Cells("ExpDate").Value, Date)
            Me.txtRemarks.Text = Me.grdSaved.CurrentRow.Cells("Remarks").Text.ToString

            If Val(Me.grdSaved.CurrentRow.Cells("ResultStatus").Value) = 1 Then

                Me.rdbApproved.Checked = True

            ElseIf Val(Me.grdSaved.CurrentRow.Cells("ResultStatus").Value) = 0 Then

                Me.rdbRejected.Checked = True

            End If

            'Boolean.TryParse(Me.grdSaved.CurrentRow.Cells("ResultStatus").Text.ToString, Me.rdbApproved.Checked)
            'Me.rdbRejected.Checked = Not Me.rdbApproved.Checked

            DisplayDetail(Val(grdSaved.CurrentRow.Cells("ResultID").Value))
            Me.btnSave.Text = "&Update"
            Me.btnDelete.Visible = True
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
            'Ali Faisal : Refresh all combos on 27-June-2016
            Dim id As Integer = 0
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()

            id = Me.cmbSupplier.SelectedRow.Cells(0).Value
            FillComboBox("Vendor")
            Me.cmbSupplier.Value = id

            id = Me.cmbGRNNo.SelectedValue
            FillComboBox("GRN")
            Me.cmbGRNNo.SelectedValue = id

            id = Me.cmbQCNo.SelectedValue
            FillComboBox("QC")
            Me.cmbQCNo.SelectedValue = id
            Me.lblProgress.Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            RefreshControls()
            DisplayRecord()
            DisplayDetail(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            EditRecord()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Me.btnSave.Enabled = False Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Try
            If Not FormValidate() = True Then Exit Sub
            Me.grd.UpdateData()
            ItemId = String.Empty
            ItemId = CType(Me.cmbQCNo.SelectedItem, DataRowView).Row("ItemId").ToString
            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Me.Save() Then
                    RefreshControls()
                    DisplayRecord()
                    DisplayDetail(-1)
                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If UpdateRecord() Then
                    RefreshControls()
                    DisplayRecord()
                    DisplayDetail(-1)
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
            cm.CommandText = "Delete From LabResultDetailTable Where ResultID=" & Me.grdSaved.CurrentRow.Cells("ResultID").Value.ToString
            cm.Transaction = objTrans
            cm.ExecuteNonQuery()
            cm = New OleDbCommand
            cm.Connection = Con
            'Ali Faisal : Master record delete on 22-June-2016
            cm.CommandText = "Delete From LabResultMasterTable Where ResultID=" & Me.grdSaved.CurrentRow.Cells("ResultID").Value.ToString
            cm.Transaction = objTrans
            cm.ExecuteNonQuery()
            objTrans.Commit()
            SaveActivityLog("ItemParameterConfig", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Lab, grdSaved.CurrentRow.Cells(0).Value.ToString, True)
            Me.RefreshControls()
            Me.DisplayRecord()
            Me.DisplayDetail(-1)
            'Me.grdSaved.CurrentRow.Delete()
        Catch ex As Exception
            msg_Error("Error occured while deleting record: " & ex.Message)
        Finally
            Con.Close()
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
        'Ali Faisal : Insert Activity Log on 21-June-2016

    End Sub

    Private Sub btnParametersMapping_Click(sender As Object, e As EventArgs) Handles btnParametersMapping.Click
        Try
            frmParameterConfig.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSupplier_ValueChanged(sender As Object, e As EventArgs) Handles cmbSupplier.ValueChanged
        Try
            FillComboBox("QC")
            FillComboBox("GRN")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbQCNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbQCNo.SelectedIndexChanged
        Try
            If cmbQCNo.SelectedValue > 0 Then
                If Not IsEditMode = True Then
                    If Not cmbQCNo.SelectedItem Is Nothing Then
                        ItemId = String.Empty
                        ItemId = CType(Me.cmbQCNo.SelectedItem, DataRowView).Row("ItemId").ToString
                        Me.txtBatchNo.Text = CType(Me.cmbQCNo.SelectedItem, DataRowView).Row("TestReq_Batch").ToString
                        Me.txtBatchSize.Text = CType(Me.cmbQCNo.SelectedItem, DataRowView).Row("TestReq_Qty").ToString
                        Me.txtNoofContainer.Text = CType(Me.cmbQCNo.SelectedItem, DataRowView).Row("TestReq_NoofContainers").ToString
                        If CType(Me.cmbQCNo.SelectedItem, DataRowView).Row("TestReq_MfgDate").ToString() <> "" Then
                            Me.dtpMfgDate.Value = Convert.ToDateTime(CType(Me.cmbQCNo.SelectedItem, DataRowView).Row("TestReq_MfgDate"))
                        End If
                        If (CType(Me.cmbQCNo.SelectedItem, DataRowView).Row("TestReq_ExpDate").ToString() <> "") Then
                            Me.dtpExpDate.Value = Convert.ToDateTime(CType(Me.cmbQCNo.SelectedItem, DataRowView).Row("TestReq_ExpDate"))
                        End If

                        Me.txtQCSampled.Text = CType(Me.cmbQCNo.SelectedItem, DataRowView).Row("SampleQuantity").ToString()



                        If Not ItemId = String.Empty Then
                            DisplayItemParameters(Val(ItemId))
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrintForm_Click(sender As Object, e As EventArgs) Handles btnPrintForm.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("QcNumber").Value.ToString ''ObserverSampleMasterID, QcNumber
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            AddRptParam("@ResultId", Me.grdSaved.GetRow.Cells("ResultID").Value)
            'AddRptParam("@RequestDetailId", Me.grdSaved.GetRow.Cells("TestReq_Detail_Id").Value)
            ShowReport("rptLabResult") ''rptLabSampledSticker
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load_1(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
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

    Private Sub frmResult_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class