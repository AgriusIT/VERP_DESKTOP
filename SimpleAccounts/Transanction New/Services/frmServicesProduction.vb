Imports System.Data
Imports System.Data.OleDb
Public Class frmServicesProduction
    Implements IGeneral
    Enum enmDetail
        LocationId
        ArticleDefId
        ArticleDescription
        ArticleCode
        PackQty
        Qty
        TotalQty
        CurrentPrice
        PackPrice
        Price
        TotalAmount
        Comments
        IssuedQty
        IssuedTotalQty
        DocDetailId
    End Enum
    Dim DocId As Integer = 0I
    Dim IsOpenForm As Boolean = False
    Dim IsEditMode As Boolean = False
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdGatePass.RootTable.Columns
                If col.Index <> enmDetail.PackQty AndAlso col.Index <> enmDetail.Qty AndAlso col.Index <> enmDetail.TotalQty AndAlso col.Index <> enmDetail.Price AndAlso col.Index <> enmDetail.Comments AndAlso col.Index <> enmDetail.PackPrice Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
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
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmCustomerPlanning)
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
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                For Each RightsDt As SBModel.GroupRights In Rights
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
                    End If
                Next
            End If

            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                Me.btnDelete.Visible = False
            Else
                Me.btnDelete.Visible = True
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        'Me.txtDocNo.Text = GetDocumentNo()

        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDbCommand

        Try


            cmd.CommandType = CommandType.Text
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 120

            cmd.CommandText = ""
            cmd.CommandText = "Delete From ServicesProductionDetailTable WHERE DocId=" & Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString) & ""
            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "Delete From ServicesProductionMasterTable WHERE DocId=" & Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString) & ""
            cmd.ExecuteNonQuery()

            trans.Commit()

            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try

            If Condition = "Location" Then
                FillDropDown(Me.cmbLocation, "Select Location_Id, Location_Name From tblDefLocation ORDER BY Sort_Order ASC", False)
            ElseIf Condition = String.Empty Then
                FillUltraDropDown(Me.cmbVendor, "Select coa_detail_id, detail_title as [Customer], detail_code as [Account Code], Sub_Sub_Title as [Account Head], Account_Type as [Account Type], Contact_Mobile as [Mobile], Contact_Email as [Email], CityName as City From vwCOADetail WHERE Account_Type In('Customer','Vendor') And Detail_Title <> '' Order By Detail_Title")
                Me.cmbVendor.Rows(0).Activate()
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
            ElseIf Condition = "Item" Then
                'Marked Against Task#201508010 By Ali Ansari to add Sales AccountID in Combo 
                'FillUltraDropDown(Me.cmbItem, "Select ArticleId, ArticleDescription as [Item], ArticleCode as [Code], ArticleGroupName as [Department],ArticleTypeName as [Type/Brand],ArticleCompanyName as [Category], ArticleSizeName as [Size], ArticleColorName as Combination, PurchasePrice as Price, MasterID From ArticleDefView WHERE ArticleDescription <> '' ORDER BY ArticleDescription ASC")
                FillUltraDropDown(Me.cmbItem, "Select ArticleId, ArticleDescription as [Item], ArticleCode as [Code], ArticleGroupName as [Department],ArticleTypeName as [Type/Brand],ArticleCompanyName as [Category], ArticleSizeName as [Size], ArticleColorName as Combination, PurchasePrice as Price, MasterID,SalesAccountId From ArticleDefView WHERE ArticleDescription <> '' ORDER BY ArticleDescription ASC")
                'Marked Against Task#201508010 By Ali Ansari to add Sales AccountID in Combo 
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SalesAccountId").Hidden = True
            ElseIf Condition = "UnitPack" Then
                FillDropDown(Me.cmbUnit, "Select ArticlePackId, PackName, PackQty from ArticleDefPackTable WHERE ArticleMasterID=" & Val(Me.cmbItem.ActiveRow.Cells("MasterID").Value.ToString) & "", False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim strSQL As String = String.Empty
            If Condition = "Master" Then
                strSQL = String.Empty
                strSQL = "SELECT PP.DocId,PP.Job_No, PP.DocNo, PP.DocDate, PP.LocationId, PP.CustomerCode, COA.detail_title as [Customer], COA.detail_code as [A/c Code], COA.account_type as [Type], COA.StateName as [Region], COA.CityName as [City], " _
                        & " COA.Contact_Phone as [Phone], PP.Remarks, PP.TotalQty, PP.TotalAmount, PP.EntryDate, PP.UserName " _
                        & " FROM dbo.ServicesProductionMasterTable AS PP INNER JOIN dbo.vwCOADetail AS COA ON PP.CustomerCode = COA.coa_detail_id ORDER BY PP.DocId DESC "
                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                dt.AcceptChanges()

                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()


                Me.grdSaved.RootTable.Columns("DocId").Visible = False
                Me.grdSaved.RootTable.Columns("CustomerCode").Visible = False
                Me.grdSaved.RootTable.Columns("LocationId").Visible = False

                Me.grdSaved.AutoSizeColumns()



            ElseIf Condition = "Detail" Then

                strSQL = String.Empty
                strSQL = "SELECT PP_D.LocationId, PP_D.ArticleDefId, Art.ArticleDescription, Art.ArticleCode, PP_D.PackQty, PP_D.Qty, PP_D.TotalQty, IsNull(PP_D.CurrentPrice,0) as CurrentPrice, IsNull(PP_D.PackPrice,0) as PackPrice, " _
                & "  PP_D.Price, PP_D.TotalAmount, PP_D.Comments,IsNull(PP_D.IssuedQty,0) as IssuedQty, IsNull(PP_D.IssuedTotalQty,0) as IssuedTotalQty, IsNull(PP_D.DocDetailId,0) as DocDetailId FROM dbo.ServicesProductionDetailTable AS PP_D INNER JOIN dbo.ArticleDefView AS Art ON PP_D.ArticleDefId = Art.ArticleId WHERE PP_D.DocId=" & DocId & " ORDER BY PP_D.DocId "
                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                dt.AcceptChanges()
                dt.Columns("TotalAmount").Expression = "(IsNull(TotalQty,0)*IsNull(Price,0))"
                Me.grdGatePass.DataSource = dt

                Dim dtLocation As New DataTable
                dtLocation = GetDataTable("Select Location_Id, Location_Name From tblDefLocation Order By Sort_Order ASC")
                dtLocation.AcceptChanges()
                Me.grdGatePass.RootTable.Columns("LocationId").ValueList.PopulateValueList(dtLocation.DefaultView, "Location_Id", "Location_Name")
                ApplyGridSettings()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.cmbVendor.IsItemInList = False Then Return False
            If Me.cmbVendor.ActiveRow Is Nothing Then Return False
            If Me.cmbVendor.Value = 0 Then
                ShowErrorMessage("Please select Customer.")
                Me.cmbVendor.Focus()
                Return False
            End If
            'If Len(Trim(Me.TxtJobNo.Text)) = 0 Then
            '    ShowErrorMessage("Please select job no.")
            '    Me.TxtJobNo.Focus()
            '    Return False
            'End If
            If Me.grdGatePass.RowCount = 0 Then
                ShowErrorMessage("Record not in grid.")
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            DocId = 0
            Me.btnSave.Text = "&Save"
            Me.dtpDocDate.Value = Date.Now
            Me.txtDocNo.Text = GetDocumentNo()
            Me.cmbVendor.Rows(0).Activate()
            Me.cmbVendor.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
            Me.txtRemarks.Text = String.Empty
            Me.TxtJobNo.Text = String.Empty
            GetAllRecords("Master")
            GetAllRecords("Detail")
            Me.cmbVendor.Focus()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.UltraTabControl1_SelectedTabChanged(Nothing, Nothing)
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save


        Me.txtDocNo.Text = GetDocumentNo()

        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDbCommand

        Try


            cmd.CommandType = CommandType.Text
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 120


            cmd.CommandText = ""
            cmd.CommandText = "INSERT INTO ServicesProductionMasterTable(DocNo,DocDate,LocationId,CustomerCode,Remarks,TotalQty,TotalAmount,EntryDate,UserName,job_no) " _
            & " VALUES('" & Me.txtDocNo.Text & "',Convert(DateTime,'" & Me.dtpDocDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),1," & Me.cmbVendor.Value & ",N'" & Me.txtRemarks.Text.Replace("'", "''") & "'," & Me.grdGatePass.GetTotal(Me.grdGatePass.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & Me.grdGatePass.GetTotal(Me.grdGatePass.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102),'" & LoginUserName.Replace("'", "''") & "',N'" & Me.TxtJobNo.Text.Replace("'", "''") & "') Select @@Identity"
            Dim objID As Object = cmd.ExecuteScalar

            SaveDetail(objID, trans)


            trans.Commit()

            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Public Sub SaveDetail(ByVal DocId As Integer, ByVal trans As OleDbTransaction)
        Dim cmd As New OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        Try

            For Each jsRow As Janus.Windows.GridEX.GridEXRow In Me.grdGatePass.GetRows

                cmd.CommandText = ""
                cmd.CommandText = "INSERT INTO ServicesProductionDetailTable(DocId,LocationId,ArticleDefId,PackQty,Qty,TotalQty,CurrentPrice,PackPrice,Price,TotalAmount,Comments,IssuedQty,IssuedTotalQty) " _
                & " VALUES(" & DocId & "," & Val(jsRow.Cells("LocationId").Value.ToString) & "," & Val(jsRow.Cells("ArticleDefId").Value.ToString) & "," & Val(jsRow.Cells("PackQty").Value.ToString) & "," & Val(jsRow.Cells("Qty").Value.ToString) & "," & Val(jsRow.Cells("TotalQty").Value.ToString) & ",   " _
                & " " & Val(jsRow.Cells("CurrentPrice").Value.ToString) & "," & Val(jsRow.Cells("PackPrice").Value.ToString) & "," & Val(jsRow.Cells("Price").Value.ToString) & "," & Val(jsRow.Cells("TotalAmount").Value.ToString) & ",'" & jsRow.Cells("Comments").Value.ToString & "'," & Val(jsRow.Cells("IssuedQty").Value.ToString) & "," & Val(jsRow.Cells("IssuedTotalQty").Value.ToString) & ")Select @@Identity "
                Dim intID As Integer = cmd.ExecuteScalar()

                cmd.CommandText = ""
                cmd.CommandText = "Update ServicesDispatchDetailTable SET Ref_DocDetailId=" & intID & " WHERE Ref_DocId=" & Val(DocId) & " AND Ref_DocDetailId=" & Val(jsRow.Cells("DocDetailId").Value.ToString) & ""
                cmd.ExecuteNonQuery()


            Next

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

        'Me.txtDocNo.Text = GetDocumentNo()

        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDbCommand

        Try


            cmd.CommandType = CommandType.Text
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 120


            cmd.CommandText = ""
            cmd.CommandText = "UPDATE ServicesProductionMasterTable  SET  DocNo='" & Me.txtDocNo.Text & "',DocDate=Convert(DateTime,'" & Me.dtpDocDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),LocationId=1,CustomerCode=" & Me.cmbVendor.Value & ",Remarks=N'" & Me.txtRemarks.Text.Replace("'", "''") & "',TotalQty=" & Me.grdGatePass.GetTotal(Me.grdGatePass.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",TotalAmount=" & Me.grdGatePass.GetTotal(Me.grdGatePass.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",EntryDate=Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102),UserName='" & LoginUserName.Replace("'", "''") & "' WHERE DocId=" & Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString) & ""
            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "Delete From ServicesProductionDetailTable WHERE DocId=" & Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString) & ""
            cmd.ExecuteNonQuery()

            SaveDetail(Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString), trans)


            trans.Commit()

            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("SP" + "-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "ServicesProductionMasterTable", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("SP" & "-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "ServicesProductionMasterTable", "DocNo")
            Else
                Return GetNextDocNo("SP", 6, "ServicesProductionMasterTable", "DocNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmProductionProcessing(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            FillCombos()
            FillCombos("Location")
            FillCombos("Item")
            FillCombos("UnitPack")
            ReSetControls()
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub EditRecords()


        Try



            If Not Me.grdSaved.RecordCount > 0 Then Exit Sub
            'Me.IsEditMode = True
            IsDrillDown = False
            txtDocNo.Text = grdSaved.CurrentRow.Cells("DocNo").Value.ToString
            dtpDocDate.Value = CType(grdSaved.CurrentRow.Cells("DocDate").Value, Date)
            cmbVendor.Value = Val(grdSaved.CurrentRow.Cells("CustomerCode").Value.ToString)
            txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
            TxtJobNo.Text = grdSaved.CurrentRow.Cells("Job_No").Value & ""

            DocId = grdSaved.CurrentRow.Cells("DocId").Value

            Call GetAllRecords("Detail")


            Me.btnDelete.Visible = True
            Me.btnPrint.Visible = True
            Me.btnSave.Text = "Update"
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdGatePass_CellEdited(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdGatePass.CellEdited

    End Sub

    Private Sub grdGatePass_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdGatePass.CellUpdated
        Try

            Me.grdGatePass.UpdateData()
            With Me.grdGatePass.CurrentRow
                If e.Column.DataMember = "Qty" Or e.Column.DataMember = "PackQty" Then
                    .BeginEdit()
                    '.Cells("TotalAmount").Value = (Val(.Cells("Qty").Value) * Val(.Cells("Price").Value) * Val(.Cells("PackQty").Value))
                    .Cells("TotalQty").Value = Val(.Cells("PackQty").Value) * Val(.Cells("qty").Value)
                    .EndEdit()
                ElseIf e.Column.DataMember = "TotalQty" Then
                    If Not Val(Me.grdGatePass.GetRow.Cells("PackQty").Value.ToString) > 1 Then
                        Me.grdGatePass.GetRow.Cells("Qty").Value = Val(Me.grdGatePass.GetRow.Cells("TotalQty").Value.ToString)
                    End If
                ElseIf e.Column.DataMember = "PackPrice" Then
                    If getConfigValueByType("Apply40KgRate").ToString = "True" Then
                        Me.grdGatePass.GetRow.Cells(enmDetail.Price).Value = (Val(Me.grdGatePass.GetRow.Cells(enmDetail.PackPrice).Value.ToString) / 40)
                    Else
                        If Val(Me.grdGatePass.GetRow.Cells(enmDetail.PackQty).Value.ToString) > 1 Then
                            Me.grdGatePass.GetRow.Cells(enmDetail.Price).Value = (Val(Me.grdGatePass.GetRow.Cells(enmDetail.PackPrice).Value.ToString) / Val(Me.grdGatePass.GetRow.Cells(enmDetail.PackQty).Value.ToString))
                        End If
                    End If
                End If
            End With
            'GetTotal()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdGatePass_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdGatePass.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "btnDelete1" Then
                Me.grdGatePass.GetRow.Delete()
                grdGatePass.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdGatePass_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdGatePass.DoubleClick
        Try
            EditRecords()
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
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
       

            
            If IsValide_Add() = True Then

                Dim dt As DataTable = CType(Me.grdGatePass.DataSource, DataTable)
                Dim dr As DataRow
                dr = dt.NewRow
                dr.Item(enmDetail.LocationId) = Me.cmbLocation.SelectedValue
                dr.Item(enmDetail.ArticleDefId) = Me.cmbItem.ActiveRow.Cells("ArticleId").Text.ToString
                dr.Item(enmDetail.ArticleDescription) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
                dr.Item(enmDetail.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
                dr.Item(enmDetail.PackQty) = Val(Me.txtPackQty.Text).ToString
                dr.Item(enmDetail.Qty) = Val(Me.txtQty.Text).ToString
                dr.Item(enmDetail.TotalQty) = Val(txtTotalQty.Text).ToString
                dr.Item(enmDetail.CurrentPrice) = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
                dr.Item(enmDetail.PackPrice) = Val(Me.txtPackPrice.Text)
                dr.Item(enmDetail.Price) = Val(Me.txtPrice.Text).ToString
                dr.Item(enmDetail.TotalAmount) = Val(txtTotalAmount.Text).ToString
                dr.Item(enmDetail.Comments) = String.Empty
                dt.Rows.InsertAt(dr, 0)
                dt.AcceptChanges()

            End If



            ClearDetailControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPrice_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice.LostFocus

        'If Val(Me.txtTotalAmount.Text) <> 0 AndAlso Val(Me.txtPrice.Text) <> 0 AndAlso Val(Me.txtQty.Text) = 0 Then
        '    Me.txtQty.Text = Math.Round((Val(Me.txtTotalAmount.Text) / Val(Me.txtPrice.Text)), 2)
        'Else
        '    If Val(Me.txtPackQty.Text) = 0 Then
        '        txtPackQty.Text = 1
        '        txtTotalAmount.Text = Val(txtQty.Text) * Val(txtPrice.Text)
        '    Else
        '        txtTotalAmount.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtPrice.Text)
        '    End If
        'End If

    End Sub
    Private Sub ClearDetailControls()
        Try

        
            If cmbUnit.SelectedIndex <> -1 Then cmbUnit.SelectedIndex = 0
            txtQty.Text = String.Empty
            txtPrice.Text = String.Empty
            txtTotalAmount.Text = String.Empty
            txtPackQty.Text = String.Empty
            Me.txtTotalAmount.Text = String.Empty
            Me.txtTotalQty.Text = String.Empty
            Me.txtPackPrice.Text = String.Empty
            Me.txtPrice.Text = String.Empty
        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub grdSaved_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        If e.KeyCode = Keys.F2 Then
            btnSave_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.grdSaved.RowCount <= 0 Then Exit Sub
            btnDelete_click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()
            
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            FillCombos("UnitPack")
            If Me.cmbUnit.SelectedIndex = -1 Then
                Dim dt As New DataTable
                dt = CType(Me.cmbUnit.DataSource, DataTable)
                If dt IsNot Nothing Then
                    dt.AcceptChanges()
                    Dim dr As DataRow
                    dr = dt.NewRow
                    dr(0) = 0
                    dr(1) = "Loose"
                    dr(2) = 1
                    dt.Rows.Add(dr)
                    dr = dt.NewRow
                    dr(0) = 0
                    dr(1) = "Pack"
                    dr(2) = 1
                    dt.Rows.Add(dr)
                    dt.AcceptChanges()
                End If
            End If
            Me.txtPrice.Text = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
            GetTotalAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbUnit.SelectedIndex = -1 Then Exit Sub

            If Me.cmbUnit.Text <> "Loose" Then
                Me.txtPackQty.Text = CType(Me.cmbUnit.SelectedItem, DataRowView).Row("PackQty").ToString
            Else
                Me.txtPackQty.Text = 1
            End If
            GetTotalQty()
            GetTotalAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetTotalAmount()
        Try
            Dim dblTotalAmount As Double = Val(Me.txtTotalQty.Text) * Val(Me.txtPrice.Text)
            Me.txtTotalAmount.Text = dblTotalAmount
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetTotalQty()
        Try
            Dim dblTotalQty As Double = IIf(Val(Me.txtPackQty.Text) > 1, (Val(Me.txtQty.Text) * Val(Me.txtPackQty.Text)), Val(Me.txtQty.Text))
            Me.txtTotalQty.Text = dblTotalQty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValide_Add() As Boolean
        Try
            If Me.cmbItem.IsItemInList = False Then Return False
            If Me.cmbItem.ActiveRow Is Nothing Then Return False

            If Me.cmbItem.Value <= 0 Then
                ShowErrorMessage("Please select item.")
                Me.cmbItem.Focus()
                Return False
            End If
            If Val(Me.txtQty.Text) <= 0 Then
                ShowErrorMessage("Qty should be greater than zero.")
                Me.txtQty.Focus()
                Return False
            End If
            'If Val(Me.txtPrice.Text) = 0 Then
            '    ShowErrorMessage("Please enter the price.")
            '    Me.txtQty.Focus()
            '    Return False
            'End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.LostFocus
        Try
            GetTotalAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        Try
            GetTotalQty()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPrice_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice.TextChanged
        Try
            GetTotalAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPackPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackPrice.TextChanged
        Try
            If getConfigValueByType("Apply40KgRate").ToString = "False" Then
                If Val(Me.txtPackQty.Text) > 1 Then
                    txtPrice.Text = Val(Me.txtPackPrice.Text) / (Me.txtPackQty.Text)
                Else
                    txtPrice.Text = Val(txtPrice.Text)
                End If
            Else
                txtPrice.Text = (Val(Me.txtPackPrice.Text) / 40)
            End If
            GetTotalAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPackQty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalQty.KeyPress, txtQty.KeyPress, txtPrice.KeyPress, txtPackQty.KeyPress, txtPackPrice.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.SelectedTab.Index = 0 Then
                Me.btnSave.Visible = True
                If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                    Me.btnDelete.Visible = False
                Else
                    Me.btnDelete.Visible = True
                End If
            Else
                Me.btnDelete.Visible = True
                Me.btnSave.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@DocId", Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString))
            ShowReport("rptServiceProduction")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPackQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPackQty.TextChanged
        Try
            GetTotalQty()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalQty.LostFocus
        Try
            GetTotalAmount()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalQty.TextChanged
        Try

            If Not Val(Me.txtPackQty.Text) > 1 Then
                Me.txtQty.Text = Val(Me.txtTotalQty.Text)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbVendor.ActiveRow.Cells(0).Value
            FillCombos()
            Me.cmbVendor.Value = id
            id = Me.cmbLocation.SelectedIndex
            FillCombos("Location")
            Me.cmbLocation.SelectedIndex = id
            If cmbItem.ActiveRow Is Nothing Then
                Me.cmbItem.Rows(0).Activate()
            End If
            id = Me.cmbItem.ActiveRow.Cells(0).Value
            FillCombos("Item")
            Me.cmbItem.Value = id
            'id = Me.cmbUnit.SelectedIndex
            'FillCombos("UnitPack")
            'Me.cmbUnit.SelectedIndex = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles lblHeader.Click

    End Sub
End Class