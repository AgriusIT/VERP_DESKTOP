'Ali Faisal : Added Form to save the Record on 24-June-2016
Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports SBUtility
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports System.Data.SqlClient

Public Class frmParameterConfig

    Dim IsEditMode As Boolean = False
    Dim CurrentId As Integer
    Enum grdenm
        'Ali Faisal : Set Indexing on 24-June-2016
        ParameterDetailId
        ParameterId
        ParameterName
        Specification
        ProductionStepId
    End Enum


    Private Sub frmParameterConfig_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            'Ali Faisal : System UserFriendly modification : to FillCombo and Refresh Controls at Form shown on 24-June-2016
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            FillComboBox("Item")
            RefreshControls()
            DisplayDetail(-1)
            DisplayRecord()
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Function GetDocumentNo() As String
        Try
            'Ali Faisal : Set auto Request number Normal,Yearly,Monthly on Configuration Bases on 18-June-2016
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("IPM" + "-" + Microsoft.VisualBasic.Right(Me.dtpParameterDate.Value.Year, 2) + "-", "LabParameterMasterTable", "ParameterNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("IPM" & "-" & Format(Me.dtpParameterDate.Value, "yy") & Me.dtpParameterDate.Value.Month.ToString("00"), 4, "LabParameterMasterTable", "ParameterNo")
            Else
                Return GetNextDocNo("IPM", 6, "LabParameterMasterTable", "ParameterNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub RefreshControls()
        Try
            'Ali Faisal : Changes to refresh Controls on 24-June-2016
            Me.dtpParameterDate.Value = Now
            Me.dtpParameterDate.Enabled = True
            Me.txtParameterNo.Text = GetDocumentNo()
            Me.cmbItem.Rows(0).Activate()
            Me.cmbParameterName.Text = ""
            Me.btnSave.Text = "&Save"
            FillComboBox("Parameter")
            FillComboBox("Item")
            Me.cmbItem.Focus()
            Me.rbCode.Checked = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillComboBox(ByVal strCondition As String)
        Try
            'Ali Faisal : Fill all combos on 24-June-2016
            Dim str As String = String.Empty
            If strCondition = "Item" Then
                'cmbItem.DataSource = Nothing
                str = " SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleColorName as Combination,ArticleUnitName as UOM,ArticleSizeName as Size, PackQty as PackQty, ISNULL(PurchasePrice,0) as Price, ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model, ArticleDefView.SortOrder FROM  ArticleDefView where Active=1 "
                '               str = " SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleColorName as Combination,ArticleUnitName as UOM,ArticleSizeName as Size," &
                '"  PackQty as PackQty, ISNULL(PurchasePrice,0) as Price, ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model, " &
                ' "   ArticleDefView.SortOrder , LabParameterMasterTable.ParameterId    " &
                ' "  FROM  ArticleDefView   left join LabParameterMasterTable on LabParameterMasterTable.ItemId = ArticleDefView.ArticleId   " &
                '  " where Active=1 and ParameterId is null"
                FillUltraDropDown(Me.cmbItem, str, True)

                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If

            ElseIf strCondition = "Parameter" Then
                str = " SELECT distinct ParameterName  FROM  LabParameterDetailTable "
                '"Insert Into LabParameterDetailTable (ParameterId, ParameterName, Specification, ProductionStepId) "
                Dim con As SqlConnection = New SqlConnection(SQLHelper.CON_STR)
                con.Open()
                Dim cmd As SqlCommand = New SqlCommand(str, con)
                Dim dt As DataTable = New DataTable()
                Dim dr As SqlDataAdapter = New SqlDataAdapter(str, con)
                dr.Fill(dt)

                con.Close()
                Me.cmbParameterName.DataSource = dt
                cmbParameterName.DisplayMember = "ParameterName"
                'FillDropDown(Me.cmbParameterName, str, True)




                'SqlDataAdapter ObjAdp = new SqlDataAdapter(sql, con);
                'if (transaction != null)
                '    ObjAdp.SelectCommand.Transaction = transaction;
                'ObjAdp.Fill(Data);



            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function IsDuplicateParameter(name As String, Optional InclusiveCheck As Boolean = False)
        'if updating value is within grid cells then InclusiveCheck must be true otherwise false'
        Dim counter As Integer = 0
        For Each row As Janus.Windows.GridEX.GridEXRow In grd.GetRows
            If (name = row.Cells("ParameterName").Value.ToString()) Then
                counter = counter + 1
            End If
        Next
        If InclusiveCheck Then
            If counter >= 2 Then
                Return True
            Else
                Return False
            End If
        Else
            If counter >= 1 Then
                Return True
            Else
                Return False
            End If
        End If
        Return False
    End Function
    Private Function Validate_AddToGrid() As Boolean
        Try
            If Me.cmbItem.IsItemInList = False Then
                msg_Error("Item not found...")
                Me.cmbItem.Focus() ': Validate_AddToGrid = False : Exit Function
                Return False
            End If
            If cmbItem.Value <= 0 Then
                msg_Error("Please Select an Item...")
                cmbItem.Focus() '': Validate_AddToGrid = False : Exit Function
                Return False
            End If
            If IsDuplicateParameter(cmbParameterName.Text) = True Then
                msg_Error("Parameter is already defined")
                Return False
            End If
            If cmbParameterName.Text.Trim = "" Then
                msg_Error("Parameter Name is Empty")
                Return False
            End If
            Validate_AddToGrid = True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function FormValidate() As Boolean
        Try
            'Ali Faisal : Add function to validate that Item is selected and detail grid is not empty on 24-June-2016
            If txtParameterNo.Text = "" Then
                msg_Error("Please enter Parameter No.")
                txtParameterNo.Focus() : FormValidate = False : Exit Function
            End If
            If cmbItem.ActiveRow.Cells(0).Value <= 0 Then
                msg_Error("Please select an Item")
                cmbItem.Focus() : FormValidate = False : Exit Function
            End If
            If Not Me.grd.RowCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
                cmbItem.Focus() : FormValidate = False : Exit Function
            End If
            Return True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function
    Private Sub DisplayDetail(ByVal Parameter_Id As Integer)
        Try
            'Ali Faisal : To display detail record in detail grid on 24-June-2016
            Dim str As String
            str = "Select IsNull(ParameterDetailId,0) As ParameterDetailId,IsNull(ParameterId,0) As ParameterId,ParameterName,Specification, ProductionStepId From LabParameterDetailTable Where ParameterId=" & Parameter_Id & ""
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
            str = "SELECT        LabParameterMasterTable.ParameterId,LabParameterMasterTable.Date,LabParameterMasterTable.ParameterNo, " _
                & "              LabParameterMasterTable.ItemId,ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription " _
                & " FROM         LabParameterMasterTable INNER JOIN " _
                & "              ArticleDefView ON LabParameterMasterTable.ItemId = ArticleDefView.ArticleId " _
                & " WHERE        (ArticleDefView.Active = '1') " _
                & " Order By     LabParameterMasterTable.ParameterId Desc "
            dt = GetDataTable(str)
            dt.AcceptChanges()
            grdSaved.DataSource = dt
            grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("ParameterId").Visible = False
            Me.grdSaved.RootTable.Columns("ItemId").Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub AddToGrid()
        Try
            'Ali Faisal : Add record into grid on 24-June-2016
            Dim dtGrd As DataTable
            dtGrd = CType(Me.grd.DataSource, DataTable)
            dtGrd.AcceptChanges()
            Dim drGrd As DataRow
            drGrd = dtGrd.NewRow
            drGrd.Item(grdenm.ParameterName) = Me.cmbParameterName.Text.ToString
            drGrd.Item(grdenm.Specification) = ""
            drGrd.Item(grdenm.ProductionStepId) = cmbProductionStep.SelectedValue.ToString

            'dtGrd.Rows.InsertAt(drGrd, 0)
            dtGrd.Rows.Add(drGrd)
            dtGrd.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
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

                'Ali Faisal : Insert master record on 27-June-2016
                objCommand.CommandText = "Insert into LabParameterMasterTable (Date,ParameterNo,ItemId)" _
                                       & " Values (N'" & dtpParameterDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "' ,N'" & txtParameterNo.Text & "',N'" & cmbItem.ActiveRow.Cells(0).Value & "') SELECT @@IDENTITY  "
                Dim masterId As String = objCommand.ExecuteScalar().ToString()
                'Ali Faisal : Insert detail record on 27-June-2016
                For i = 0 To grd.RowCount - 1
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Insert Into LabParameterDetailTable (ParameterId, ParameterName, Specification, ProductionStepId) " _
                                           & " Values ('" & masterId.ToString() & "',N'" & grd.GetRows(i).Cells(grdenm.ParameterName).Value.ToString & "',N'" _
                                        & grd.GetRows(i).Cells(grdenm.Specification).Value.ToString & "', " & grd.GetRows(i).Cells(grdenm.ProductionStepId).Value.ToString & ")"
                    objCommand.ExecuteNonQuery()
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
        'Ali Faisal : Insert Activity Log on 27-June-2016
        SaveActivityLog("ItemParameterConfig", Me.Text, IIf(Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Lab, Me.CurrentId, True)
    End Function
    Private Sub EditRecord()
        Try
            IsEditMode = True
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.dtpParameterDate.Value = CType(grdSaved.CurrentRow.Cells("Date").Value, Date)
            Me.txtParameterNo.Text = Me.grdSaved.CurrentRow.Cells("ParameterNo").Value.ToString
            Me.cmbItem.Value = Val(Me.grdSaved.CurrentRow.Cells("ItemId").Value)
            DisplayDetail(Val(grdSaved.CurrentRow.Cells("ParameterId").Value))
            Me.btnSave.Text = "&Update"
            Me.btnDelete.Visible = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
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
            objCommand.CommandText = "Update LabParameterMasterTable Set Date = N'" & dtpParameterDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',ParameterNo = N'" & txtParameterNo.Text & "', ItemId = N'" & cmbItem.ActiveRow.Cells(0).Value & "' Where ParameterId=" & Me.grdSaved.CurrentRow.Cells("ParameterId").Value.ToString & " "
            objCommand.ExecuteNonQuery()
            objCommand.CommandText = ""
            objCommand.CommandText = "Delete from LabParameterDetailTable Where ParameterId=" & Me.grdSaved.CurrentRow.Cells("ParameterId").Value.ToString
            objCommand.ExecuteNonQuery()
            For i = 0 To grd.RowCount - 1
                objCommand.CommandText = ""
                objCommand.CommandText = "Insert Into LabParameterDetailTable (ParameterId,ParameterName,Specification) " _
                                           & " Values (Ident_Current ('LabParameterMasterTable'),N'" & grd.GetRows(i).Cells(grdenm.ParameterName).Value.ToString & "',N'" & grd.GetRows(i).Cells(grdenm.Specification).Value.ToString & "')"
                objCommand.ExecuteNonQuery()
            Next
            trans.Commit()
            UpdateRecord = True
            DisplayRecord()
            'Insert Activity Log
            SaveActivityLog("ItemParameterConfig", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Lab, Me.txtParameterNo.Text.Trim, True)
        Catch ex As Exception
            trans.Rollback()
            UpdateRecord = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try
    End Function
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
    Private Sub cmbItem_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Enter
        'Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)


        'Try
        '    Dim parameterId As String = ""
        '    Dim parameterNo As String = ""
        '    Dim itemId As String = ""
        '    Dim datee As DateTime = DateTime.Now
        '    For Each items As Janus.Windows.GridEX.GridEXRow In grdSaved.GetRows
        '        If cmbItem.Value = items.Cells("ItemId").Value Then
        '            parameterId = items.Cells("ParameterId").Value
        '            itemId = items.Cells("ItemId").Value
        '            datee = CType(items.Cells("Date").Value, Date)
        '            parameterNo = items.Cells("ParameterNo").Value
        '        End If
        '    Next
        '    IsEditMode = True
        '    If Not Me.grdSaved.RowCount > 0 Then Exit Sub
        '    If Me.grd.RowCount > 0 Then
        '        If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
        '    End If
        '    Me.dtpParameterDate.Value = datee
        '    Me.txtParameterNo.Text = parameterNo
        '    Me.cmbItem.Value = Val(itemId)
        '    DisplayDetail(Val(parameterId))
        '    Me.btnSave.Text = "&Update"
        '    Me.btnDelete.Visible = True

        'Catch ex As Exception
        '    msg_Error(ex.Message)
        'End Try



    End Sub
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.grd.GetRow.Delete()
                grd.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If Validate_AddToGrid() = False Then Exit Sub
            AddToGrid()
            Me.cmbParameterName.Text = ""
            'Me.cmbItem.Focus()
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
            If FormValidate() Then
                Me.grd.UpdateData()
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Me.Save() Then
                        RefreshControls()
                        DisplayDetail(-1)
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
            cm.CommandText = "Delete from LabParameterDetailTable Where ParameterId=" & Me.grdSaved.CurrentRow.Cells("ParameterId").Value.ToString
            cm.Transaction = objTrans
            cm.ExecuteNonQuery()
            cm = New OleDbCommand
            cm.Connection = Con
            'Ali Faisal : Master record delete on 22-June-2016
            cm.CommandText = "Delete from LabParameterMasterTable Where ParameterId=" & Me.grdSaved.CurrentRow.Cells("ParameterId").Value.ToString
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
        'Ali Faisal : Insert Activity Log on 21-June-2016
        SaveActivityLog("ItemParameterConfig", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Lab, grdSaved.CurrentRow.Cells(0).Value.ToString, True)
        Me.RefreshControls()
        Me.DisplayDetail(-1)
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            'Ali Faisal : Refresh on 24-June-2016
            Dim id As Integer = 0
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            id = Me.cmbItem.SelectedRow.Cells(0).Value
            FillComboBox("Item")
            Me.cmbItem.Value = id
            Me.lblProgress.Visible = False
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

    Private Sub frmParameterConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim sqlQuery As String = " select ProdStep_Id, prod_step from tblproSteps order by prod_step"
        FillDropDown(cmbProductionStep, sqlQuery)

    End Sub


    Private Sub cmbItem_ValueChanged(sender As Object, e As EventArgs) Handles cmbItem.ValueChanged
        Try
            Dim parameterId As String = ""
            Dim parameterNo As String = ""
            Dim itemId As String = ""
            Dim datee As DateTime = DateTime.Now
            For Each items As Janus.Windows.GridEX.GridEXRow In grdSaved.GetRows
                If cmbItem.Value = items.Cells("ItemId").Value Then
                    parameterId = items.Cells("ParameterId").Value
                    itemId = items.Cells("ItemId").Value
                    datee = CType(items.Cells("Date").Value, Date)
                    parameterNo = items.Cells("ParameterNo").Value
                End If
            Next
            IsEditMode = True
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            'If Me.grd.RowCount > 0 Then
            '    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            'End If
            Me.dtpParameterDate.Value = datee
            Me.txtParameterNo.Text = parameterNo
            If Val(itemId) > 0 Then
                Me.cmbItem.Value = Val(itemId)
            End If
            DisplayDetail(Val(parameterId))
            If Me.grd.RowCount > 0 Then
                Me.btnSave.Text = "&Update"
                Me.btnDelete.Visible = True
            Else
                Me.btnSave.Text = "&Save"
                Me.btnDelete.Visible = False
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

  
    'Private Sub grd_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
    '    Try

    '        Select Case e.Column.Key
    '            Case "Qty"
    '                If Val(grd.GetRow.Cells(grdDetail.Qty).Value) < 0 Then
    '                    ShowErrorMessage("Quantity should be greater than zero")
    '                    grd.CancelCurrentEdit()
    '                End If
    '            Case "Rate"
    '                If Val(grd.GetRow.Cells(grdDetail.Rate).Value) < 0 Then
    '                    ShowErrorMessage("Rate should be greater than Zero")
    '                    grd.CancelCurrentEdit()
    '                End If
    '        End Select
    '        grd.UpdateData()
    '        txtTotal.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.NetAmount), Janus.Windows.GridEX.AggregateFunction.Sum))
    '        txtNetTotal.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.NetAmount), Janus.Windows.GridEX.AggregateFunction.Sum))
    '        txtCash.Text = Val(txtNetTotal.Text)
    '    Catch ex As Exception
    '        msg_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub grd_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        If IsDuplicateParameter(grd.GetRow.Cells("ParameterName").Value.ToString(), True) Then
            grd.CancelCurrentEdit()
            msg_Error("Value is already exist !")
        End If
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.Tabs(0).Selected = True Then
                Me.CtrlGrdBar1.Visible = False
                Me.CtrlGrdBar2.Visible = True
            Else
                Me.CtrlGrdBar1.Visible = True
                Me.CtrlGrdBar2.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load

    End Sub
End Class