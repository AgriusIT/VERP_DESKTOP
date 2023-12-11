Imports System.Data
Imports System.Data.OleDb
Public Class frmServicesInvoices
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
        SaleAccountId
        DocId
        DocDetailId
        WIPDocId
        WIPDocDetailId
    End Enum
    Dim DocId As Integer = 0I
    Dim IsOpenForm As Boolean = False
    Dim IsEditMode As Boolean = False
    Dim intVoucherId As Integer = 0I
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            Me.grdGatePass.RootTable.Columns("SaleAccountId").Visible = False
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
                If Me.btnSave.Text = "Save" Then Me.chkPost.Checked = True
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
                    ElseIf RightsDt.FormControlName = "Post" Then
                        If Me.btnSave.Text = "Save" Then Me.chkPost.Checked = True
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


            SaveDocDetail(Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString), "0", trans)

            cmd.CommandText = ""
            cmd.CommandText = "Delete From ServicesInvoiceDetailTable WHERE DocId=" & Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString) & ""
            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "Delete From ServicesInvoiceMasterTable WHERE DocId=" & Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString) & ""
            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "Delete From tblVoucherDetail WHERE Voucher_Id In (Select Voucher_Id From tblVoucher WHERE Voucher_No='" & Me.grdSaved.GetRow.Cells("DocNo").Value.ToString & "')"
            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "Delete From tblVoucher WHERE Voucher_No='" & Me.grdSaved.GetRow.Cells("DocNo").Value.ToString & "'"
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
                FillUltraDropDown(Me.cmbVendor, "Select coa_detail_id, detail_title as [Customer], detail_code as [Account Code], Sub_Sub_Title as [Account Head], Account_Type as [Account Type], Contact_Mobile as [Mobile], Contact_Email as [Email], CityName as City From vwCOADetail WHERE Account_Type In ('Customer','Vendor') AND Detail_Title <> '' Order By Detail_Title")
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
            ElseIf Condition = "IGP_Documents" Then
                'FillDropDown(Me.cmbGatepass, "select IGPMasterTable.DocId,DocNo from IGPMasterTable left outer join (select DocId,Sum(IsNull(Qty,0)-IsNull(IssuedQty,0)) as Qty from IGPDetailTable group by DocId) a on a.DocId=IGPMasterTable.DocId  where DocNo <> '' and a.Qty > 0")
                Dim strSQL As String = String.Empty
                strSQL = "select IGPMasterTable.DocId,DocNo,BiltyNo as  Job_No from IGPMasterTable left outer join (select DocId,Sum(IsNull(Qty,0)-IsNull(IssuedQty,0)) as Qty " _
                            & " from IGPDetailTable group by DocId) a on a.DocId=IGPMasterTable.DocId  left outer join vwCOADetail on IGPMasterTable.CustomerCode = vwCOADetail.coa_detail_id " _
                            & " where DocNo <> '' and IsNull(a.Qty,0) > 0 And vwCOADetail.coa_detail_id = " & Val(Me.cmbVendor.ActiveRow.Cells("coa_detail_id").Value.ToString)
                FillDropDown(Me.cmbGatepass, strSQL, True)
            ElseIf Condition = "WIP_Documents" Then
                Dim strSQL As String = String.Empty
                strSQL += "select WIPMasterTable.DocId,DocNo,Job_No from WIPMasterTable left outer join (select DocId,Sum(IsNull(Qty,0)-IsNull(IssuedQty,0)) as Qty " _
                            & " from WIPDetailTable group by DocId) a on a.DocId=WIPMasterTable.DocId  left outer join vwCOADetail on WIPMasterTable.CustomerCode = vwCOADetail.coa_detail_id " _
                            & " where DocNo <> '' and IsNull(a.Qty,0) > 0 And vwCOADetail.coa_detail_id = " & Val(Me.cmbVendor.ActiveRow.Cells("coa_detail_id").Value.ToString)
                FillDropDown(Me.cmbGatepass, strSQL, True)
                'Task#07092015 add account and fill on screen
            ElseIf Condition = "Accounts" Then
                Dim strSQL As String = String.Empty
                strSQL = "SELECT  coa_detail_Id,  detail_title FROM  dbo.vwCOADetail WHERE (main_type = 'Income') AND detail_title <> ''"
                FillUltraDropDown(Me.cmbAccount, strSQL)
                Me.cmbAccount.Rows(0).Activate()
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                'End Task#07092015
            ElseIf Condition = "Project" Then
                FillDropDown(Me.cmbProject, "Select CostCenterID, Name, Code From tblDefCostCenter Order BY Name ASC")
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
                'strSQL = "SELECT SI.DocId,SI.Job_No, SI.DocNo, SI.DocDate, SI.LocationId, SI.CustomerCode, COA.detail_title as [Customer], COA.detail_code as [A/c Code], COA.account_type as [Type], IsNull(SI.ProjectID,0) as ProjectID, cot.Name as [Project], COA.StateName as [Region], COA.CityName as [City], " _
                '        & " COA.Contact_Phone as [Phone], SI.Remarks, SI.TotalQty, SI.TotalAmount, SI.EntryDate, SI.UserName " _
                '        & " FROM dbo.ServicesInvoiceMasterTable AS SI INNER JOIN dbo.vwCOADetail AS COA ON SI.CustomerCode = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter cot on cot.CostCenterID = SI.ProjectID ORDER BY SI.DocId DESC "

                strSQL = "SELECT SI.DocId,SI.Job_No, SI.DocNo, SI.DocDate, SI.LocationId,IsNull(SI.Post,0) as Post,Case When IsNull(SI.Post,0) =0 then 'Un Posted' else 'Posted' end as [Post Status], SI.CustomerCode, COA.detail_title as [Customer], COA.detail_code as [A/c Code], COA.account_type as [Type], IsNull(SI.ProjectID,0) as ProjectID, cot.Name as [Project], COA.StateName as [Region], COA.CityName as [City], " _
                      & " COA.Contact_Phone as [Phone], SI.Remarks, SI.TotalQty, SI.TotalAmount, SI.EntryDate, SI.UserName " _
                      & " FROM dbo.ServicesInvoiceMasterTable AS SI INNER JOIN dbo.vwCOADetail AS COA ON SI.CustomerCode = COA.coa_detail_id LEFT OUTER JOIN tblDefCostCenter cot on cot.CostCenterID = SI.ProjectID ORDER BY SI.DocId DESC "

                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                dt.AcceptChanges()

                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()

                Me.grdSaved.RootTable.Columns("DocId").Visible = False
                Me.grdSaved.RootTable.Columns("ProjectId").Visible = False
                Me.grdSaved.RootTable.Columns("CustomerCode").Visible = False
                Me.grdSaved.RootTable.Columns("LocationId").Visible = False
                Me.grdSaved.RootTable.Columns("Post").Visible = False
                Me.grdSaved.AutoSizeColumns()


            ElseIf Condition = "Detail" Then

                strSQL = String.Empty
                strSQL = "SELECT PP_D.LocationId, PP_D.ArticleDefId, Art.ArticleDescription, Art.ArticleCode, PP_D.PackQty, PP_D.Qty, PP_D.TotalQty, IsNull(PP_D.CurrentPrice,0) as CurrentPrice, IsNull(PP_D.PackPrice,0) as PackPrice, " _
                & "  PP_D.Price, PP_D.TotalAmount, PP_D.Comments, IsNull(PP_D.SaleAccountId,0) as SaleAccountId, IsNull(PP_D.Ref_DocId,0) as Ref_DocId, IsNull(PP_D.Ref_DocDetailId,0) as Ref_DocDetailId, IsNull(PP_D.Ref_WIP_DocId,0) as Ref_WIP_DocId, IsNull(PP_D.Ref_WIP_DocDetailId,0) as Ref_WIP_DocDetailId FROM dbo.ServicesInvoiceDetailTable AS PP_D INNER JOIN dbo.ArticleDefView AS Art ON PP_D.ArticleDefId = Art.ArticleId WHERE PP_D.DocId=" & DocId & " ORDER BY PP_D.DocId "
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
            If Me.cmbAccount.ActiveRow Is Nothing Then
                ShowErrorMessage("Invalid sale account")
                Me.cmbAccount.Focus()
                Return False
            End If
            If Me.cmbAccount.Value = 0 Then
                ShowErrorMessage("Please select sale account.")
                Me.cmbAccount.Focus()
                Return False
            End If
            If Me.cmbVendor.ActiveRow Is Nothing Then
                ShowErrorMessage("Invalid customer account.")
                Me.cmbVendor.Focus()
                Return False
            End If
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
            Me.cmbVendor.Enabled = True
            Me.cmbVendor.Rows(0).Activate()
            Me.cmbVendor.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
            Me.txtRemarks.Text = String.Empty
            Me.TxtJobNo.Text = String.Empty
            Me.rbtIGP.Checked = True
            FillCombos("IGP_Documents")
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


            'cmd.CommandText = ""
            'cmd.CommandText = "INSERT INTO ServicesInvoiceMasterTable(DocNo,DocDate,LocationId,CustomerCode,Remarks,TotalQty,TotalAmount,EntryDate,UserName,job_no,ProjectID) " _
            '& " VALUES('" & Me.txtDocNo.Text & "',Convert(DateTime,'" & Me.dtpDocDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),1," & Me.cmbVendor.Value & ",N'" & Me.txtRemarks.Text.Replace("'", "''") & "'," & Me.grdGatePass.GetTotal(Me.grdGatePass.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & Me.grdGatePass.GetTotal(Me.grdGatePass.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102),'" & LoginUserName.Replace("'", "''") & "',N'" & Me.TxtJobNo.Text.Replace("'", "''") & "'," & Me.cmbProject.SelectedValue & ") Select @@Identity"
            'Dim objID As Object = cmd.ExecuteScalar

            cmd.CommandText = ""
            cmd.CommandText = "INSERT INTO ServicesInvoiceMasterTable(DocNo,DocDate,LocationId,CustomerCode,Remarks,TotalQty,TotalAmount,EntryDate,UserName,job_no,ProjectID,Post) " _
            & " VALUES('" & Me.txtDocNo.Text & "',Convert(DateTime,'" & Me.dtpDocDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),1," & Me.cmbVendor.Value & ",N'" & Me.txtRemarks.Text.Replace("'", "''") & "'," & Me.grdGatePass.GetTotal(Me.grdGatePass.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & Me.grdGatePass.GetTotal(Me.grdGatePass.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102),'" & LoginUserName.Replace("'", "''") & "',N'" & Me.TxtJobNo.Text.Replace("'", "''") & "'," & Me.cmbProject.SelectedValue & "," & IIf(Me.chkPost.Checked = True, 1, 0) & ") Select @@Identity"
            Dim objID As Object = cmd.ExecuteScalar


            cmd.CommandText = ""
            cmd.CommandText = "INSERT INTO tblVoucher(Location_Id, Voucher_Code, Voucher_No,Voucher_Date,finiancial_year_id,voucher_type_id,coa_detail_id,source,UserName,Remarks,Post) " _
            & " VALUES(1,'" & Me.txtDocNo.Text.Replace("'", "''") & "','" & Me.txtDocNo.Text.Replace("'", "''") & "',Convert(DateTime,'" & Me.dtpDocDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),1,1," & Me.cmbVendor.Value & ",'" & Me.Name.Replace("'", "''") & "','" & LoginUserName.Replace("'", "''") & "','" & Me.txtRemarks.Text.Replace("'", "''") & "'," & IIf(Me.chkPost.Checked = True, 1, 0) & ")Select @@Identity"
            intVoucherId = cmd.ExecuteScalar
            SaveDetail(objID, trans)

            SaveDocDetail(objID, "1", trans)
            intVoucherId = 0I

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
                cmd.CommandText = "INSERT INTO dbo.ServicesInvoiceDetailTable(DocId,LocationId,ArticleDefId,PackQty,Qty,TotalQty,CurrentPrice, PackPrice, Price,TotalAmount,Comments,Ref_DocId,Ref_DocDetailId, Ref_WIP_DocId, Ref_WIP_DocDetailId) " _
                & " VALUES(" & DocId & "," & Val(jsRow.Cells("LocationId").Value.ToString) & "," & Val(jsRow.Cells("ArticleDefId").Value.ToString) & "," & Val(jsRow.Cells("PackQty").Value.ToString) & "," & Val(jsRow.Cells("Qty").Value.ToString) & "," & Val(jsRow.Cells("TotalQty").Value.ToString) & ",   " _
                & " " & Val(jsRow.Cells("CurrentPrice").Value.ToString) & "," & Val(jsRow.Cells("PackPrice").Value.ToString) & "," & Val(jsRow.Cells("Price").Value.ToString) & "," & Val(jsRow.Cells("TotalAmount").Value.ToString) & ",'" & jsRow.Cells("Comments").Value.ToString & "'," & Val(jsRow.Cells("Ref_DocId").Value.ToString) & "," & Val(jsRow.Cells("Ref_DocDetailId").Value.ToString) & ", " & Val(jsRow.Cells("Ref_WIP_DocId").Value.ToString) & "," & Val(jsRow.Cells("Ref_WIP_DocDetailId").Value.ToString) & ") "

                cmd.ExecuteNonQuery()


                cmd.CommandText = ""
                cmd.CommandText = "Insert Into tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments,debit_amount,credit_amount,costcenterId) " _
                & " VALUES(" & intVoucherId & ",1," & Me.cmbVendor.Value & ",'" & jsRow.Cells("ArticleDescription").Value.ToString.Replace("'", "''") & "(" & Val(jsRow.Cells("TotalQty").Value.ToString) & " X " & Val(jsRow.Cells("Price").Value.ToString) & ")" & "', " & Val(jsRow.Cells("TotalAmount").Value.ToString) & ",0," & Me.cmbProject.SelectedValue & ")"
                cmd.ExecuteNonQuery()


                cmd.CommandText = ""
                'cmd.CommandText = "Insert Into tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments,credit_amount,debit_amount,costcenterId) " _
                '& " VALUES(" & intVoucherId & ",1," & Val(jsRow.Cells("SaleAccountId").Value.ToString) & ",'" & jsRow.Cells("ArticleDescription").Value.ToString.Replace("'", "''") & "(" & Val(jsRow.Cells("TotalQty").Value.ToString) & " X " & Val(jsRow.Cells("Price").Value.ToString) & ")" & "', " & Val(jsRow.Cells("TotalAmount").Value.ToString) & ",0,0)"
                'Task#07092015 altered query by ahmad sharif
                cmd.CommandText = "Insert Into tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments,credit_amount,debit_amount,costcenterId) " _
                & " VALUES(" & intVoucherId & ",1," & Val(Me.cmbAccount.ActiveRow.Cells("coa_detail_id").Value.ToString) & ",'" & jsRow.Cells("ArticleDescription").Value.ToString.Replace("'", "''") & "(" & Val(jsRow.Cells("TotalQty").Value.ToString) & " X " & Val(jsRow.Cells("Price").Value.ToString) & ")" & "', " & Val(jsRow.Cells("TotalAmount").Value.ToString) & ",0," & Me.cmbProject.SelectedValue & ")"
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


            SaveDocDetail(Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString), "0", trans)


            cmd.CommandText = ""
            cmd.CommandText = "UPDATE ServicesInvoiceMasterTable  SET  DocNo='" & Me.txtDocNo.Text & "',DocDate=Convert(DateTime,'" & Me.dtpDocDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),LocationId=1,CustomerCode=" & Me.cmbVendor.Value & ",Remarks=N'" & Me.txtRemarks.Text.Replace("'", "''") & "',TotalQty=" & Me.grdGatePass.GetTotal(Me.grdGatePass.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",TotalAmount=" & Me.grdGatePass.GetTotal(Me.grdGatePass.RootTable.Columns("TotalAmount"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",EntryDate=Convert(DateTime,'" & Date.Now.ToString("yyyy-M-d hh:mm:ss tt") & "',102),UserName='" & LoginUserName.Replace("'", "''") & "',ProjectID=" & Me.cmbProject.SelectedValue & ",Post=" & IIf(chkPost.Checked = True, 1, 0) & " WHERE DocId=" & Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString) & ""
            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "Update tblVoucher Set Voucher_Date=Convert(DateTime,'" & Me.dtpDocDate.Value.ToString("yyyy-M-d hh:mm:ss tt") & "',102),coa_detail_id=" & Me.cmbVendor.Value & ",Remarks='" & Me.txtRemarks.Text.Replace("'", "''") & "',Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & " WHERE Voucher_No='" & Me.txtDocNo.Text.Replace("'", "''") & "'"
            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "Delete From ServicesInvoiceDetailTable WHERE DocId=" & Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString) & ""
            cmd.ExecuteNonQuery()


            cmd.CommandText = ""
            cmd.CommandText = "Delete From tblVoucherDetail WHERE Voucher_Id In(Select Voucher_Id From tblVoucher WHERE Voucher_No='" & Me.txtDocNo.Text.Replace("'", "''") & "')"
            cmd.ExecuteNonQuery()


            cmd.CommandText = ""
            cmd.CommandText = "Select Voucher_Id From tblVoucher WHERE Voucher_No='" & Me.txtDocNo.Text.Replace("'", "''") & "'"
            intVoucherId = cmd.ExecuteScalar

            SaveDetail(Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString), trans)
            SaveDocDetail(Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString), "1", trans)

            intVoucherId = 0I

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
                Return GetSerialNo("SSI" + "-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "ServicesInvoiceMasterTable", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("SSI" & "-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "ServicesInvoiceMasterTable", "DocNo")
            Else
                Return GetNextDocNo("SSI", 6, "ServicesInvoiceMasterTable", "DocNo")
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
            FillCombos("Accounts")
            FillCombos("Project")
            IsOpenForm = True
            ReSetControls()

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
            Me.cmbProject.SelectedValue = Val(grdSaved.CurrentRow.Cells("ProjectID").Value.ToString)
            DocId = grdSaved.CurrentRow.Cells("DocId").Value
            Me.chkPost.Checked = Me.grdSaved.GetRow.Cells("Post").Value
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
                dr.Item(enmDetail.ArticleDefId) = Me.cmbItem.ActiveRow.Cells("ArticleId").Value.ToString
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
                dr.Item(enmDetail.SaleAccountId) = Val(Me.cmbItem.ActiveRow.Cells("SalesAccountId").Value.ToString)
                dt.Rows.InsertAt(dr, 0)
                dt.AcceptChanges()

            End If



            ClearDetailControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
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
            btnDelete_Click(Nothing, Nothing)
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
            GetTotal()
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
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetTotal()
        Try
            Dim dblTotalQty As Double = IIf(Val(Me.txtPackQty.Text) > 0, (Val(Me.txtQty.Text) * Val(Me.txtPackQty.Text)), Val(Me.txtQty.Text))
            Dim dblTotalAmount As Double = dblTotalQty * Val(Me.txtPrice.Text)
            Me.txtTotalQty.Text = dblTotalQty
            Me.txtTotalAmount.Text = dblTotalAmount
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

    Private Sub txtQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPrice_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdGatePass_RecordsDeleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdGatePass.RecordsDeleted
        Try
            GetTotal()
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
            GetTotal()
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
            ShowReport("rptServiceInvoice")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SaveDocDetail(ByVal DocId As Integer, ByVal Mode As String, ByVal trans As OleDbTransaction)

        Try
            Dim strSQL As String = String.Empty
            Dim cmd As New OleDbCommand
            cmd.Connection = trans.Connection
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text
            Dim strDocType As String = String.Empty



            strSQL = String.Empty
            strSQL = "Update IGPDetailTable Set IssuedQty=" & IIf(Mode = "1", "IsNull(IssuedQty,0) + IsNull(a.Qty,0) ", "IsNull(IssuedQty,0) - IsNull(a.Qty,0) ") & ", IssuedTotalQty=" & IIf(Mode = "1", "IsNull(IssuedTotalQty,0) + IsNull(a.TotalQty,0) ", "IsNull(IssuedTotalQty,0) - IsNull(a.TotalQty,0) ") & "  From IGPDetailTable," _
            & " (Select Ref_DocId as IGP_No, Ref_DocDetailId, ArticleDefId,PackQty,ServicesInvoiceDetailTable.LocationId, SUM(IsNull(ServicesInvoiceDetailTable.Qty,0))  as Qty, SUM(ISNull(ServicesInvoiceDetailTable.TotalQty,0)) as TotalQty  From ServicesInvoiceDetailTable " _
            & " inner join ServicesInvoiceMasterTable on ServicesInvoiceDetailTable.DocId = ServicesInvoiceMasterTable.DocId WHERE ServicesInvoiceDetailTable.DocId= " & DocId & "  Group By  Ref_DocId, ArticleDefId,PackQty,ServicesInvoiceDetailTable.LocationId,Ref_DocDetailId ) a" _
            & " WHERE a.IGP_No = IGPDetailTable.DocId AND a.ArticleDefId = IGPDetailTable.ArticleDefId And a.PackQty = IGPDetailTable.PackQty And a.LocationId = IGPDetailTable.LocationId And a.Ref_DocDetailId = IGPDetailTable.DocDetailId"

            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            strSQL = String.Empty
            strSQL = "Update WIPDetailTable Set IssuedQty=" & IIf(Mode = "1", "IsNull(IssuedQty,0) + IsNull(a.Qty,0) ", "IsNull(IssuedQty,0) - IsNull(a.Qty,0) ") & ", IssuedTotalQty=" & IIf(Mode = "1", "IsNull(IssuedTotalQty,0) + IsNull(a.TotalQty,0) ", "IsNull(IssuedTotalQty,0) - IsNull(a.TotalQty,0) ") & "  From WIPDetailTable," _
                               & " (Select Ref_WIP_DocId as IGP_No, Ref_WIP_DocDetailId as Ref_DocDetailId, ArticleDefId,PackQty,ServicesInvoiceDetailTable.LocationId, SUM(IsNull(ServicesInvoiceDetailTable.Qty,0))  as Qty, SUM(ISNull(ServicesInvoiceDetailTable.TotalQty,0)) as TotalQty  From ServicesInvoiceDetailTable " _
                               & " inner join ServicesInvoiceMasterTable on ServicesInvoiceDetailTable.DocId = ServicesInvoiceMasterTable.DocId WHERE ServicesInvoiceDetailTable.DocId= " & DocId & "  Group By  Ref_WIP_DocId, ArticleDefId,PackQty,ServicesInvoiceDetailTable.LocationId,Ref_WIP_DocDetailId ) a" _
                               & " WHERE a.IGP_No = WIPDetailTable.DocId AND a.ArticleDefId = WIPDetailTable.ArticleDefId And a.PackQty = WIPDetailTable.PackQty And a.LocationId = WIPDetailTable.LocationId And a.Ref_DocDetailId = WIPDetailTable.DocDetailId"


            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()
         
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub
    Private Function ValidateGatePass(ByVal dt As DataTable) As Boolean
        Try
            If Me.cmbGatepass.SelectedIndex > 0 Then
                Dim strDocType As String = String.Empty
                strDocType = Me.cmbGatepass.Text.Substring(0, 1)
                For i As Integer = 0 To dt.Rows.Count - 1
                    If strDocType.ToString = "I" Then
                        If Val(dt.Rows(i).Item("Ref_DocId").ToString) = Val(Me.cmbGatepass.SelectedValue) Then
                            Return False
                            Exit For
                        End If
                    Else
                        If Val(dt.Rows(i).Item("Ref_WIP_DocId").ToString) = Val(Me.cmbGatepass.SelectedValue) Then
                            Return False
                            Exit For
                        End If
                    End If
                Next
            Else
                Return True
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub LoadGatePass(ByVal DocId As Integer)
        Try
            'Task#19082015 Load Multiple gate passes data into grid by AHmad SHarif
            Dim dt As New DataTable
            dt = CType(Me.grdGatePass.DataSource, DataTable)
            dt.AcceptChanges()
            If ValidateGatePass(dt) = False Then
                ShowErrorMessage("This gate pass already exist in grid")
                Me.cmbGatepass.Focus()
                Exit Sub
            End If


            Dim dr As DataRow = dt.NewRow
            Dim dt2 As New DataTable
            Dim strSQL As String = String.Empty


            Dim strDoctype As String = String.Empty
            If Me.cmbGatepass.SelectedIndex > 0 Then
                strDoctype = Me.cmbGatepass.Text.Substring(0, 1).ToString

                If strDoctype = "I" Then
                    strSQL = "SELECT IGP_D.LocationId, IGP_D.ArticleDefId, Art.ArticleDescription, Art.ArticleCode, IGP_D.PackQty, IsNull(IGP_D.Qty,0)  - IsNull(IGP_D.IssuedQty,0) as Qty , IsNull(IGP_D.TotalQty,0)-IsNull(IGP_D.IssuedTotalQty,0) as TotalQty, IGP_D.CurrentPrice, IsNull(IGP_D.PackPrice,0) as PackPrice, " _
                        & "  IGP_D.Price, IGP_D.TotalAmount, IGP_D.Comments, Art.SalesAccountId as SaleAccountId, IGP_D.DocId, IGP_D.DocDetailId, 0 as Ref_WIP_DocId, 0 as Ref_WIP_DocDetailId FROM dbo.IGPDetailTable AS IGP_D INNER JOIN dbo.ArticleDefView AS Art ON IGP_D.ArticleDefId = Art.ArticleId WHERE IGP_D.DocId=" & DocId & " And  (IsNull(IGP_D.Qty,0)  - IsNull(IGP_D.IssuedQty,0)) > 0 ORDER BY IGP_D.DocId "
                Else
                    strSQL = "SELECT WIP_D.LocationId, WIP_D.ArticleDefId, Art.ArticleDescription, Art.ArticleCode, WIP_D.PackQty, IsNull(WIP_D.Qty,0)  - IsNull(WIP_D.IssuedQty,0) as Qty , IsNull(WIP_D.TotalQty,0)-IsNull(WIP_D.IssuedTotalQty,0) as TotalQty, WIP_D.CurrentPrice, IsNull(WIP_D.PackPrice,0) as PackPrice, " _
                       & "  WIP_D.Price, WIP_D.TotalAmount, WIP_D.Comments, Art.SalesAccountId as SaleAccountId, 0 as DocId, 0 as DocDetailId, WIP_D.DocId as Ref_WIP_DocId, WIP_D.DocDetailId as Ref_WIP_DocDetailId FROM dbo.WIPDetailTable AS WIP_D INNER JOIN dbo.ArticleDefView AS Art ON WIP_D.ArticleDefId = Art.ArticleId WHERE WIP_D.DocId=" & DocId & " And  (IsNull(WIP_D.Qty,0)  - IsNull(WIP_D.IssuedQty,0)) > 0 ORDER BY WIP_D.DocId "

                End If
            Else
                Exit Sub
            End If
            dt2 = GetDataTable(strSQL)
            dt2.AcceptChanges()

            For Each row As DataRow In dt2.Rows
                dr = dt.NewRow
                dr(enmDetail.LocationId) = row("LocationId").ToString
                dr(enmDetail.ArticleDefId) = row("ArticleDefId").ToString
                dr(enmDetail.ArticleDescription) = row("ArticleDescription").ToString
                dr(enmDetail.ArticleCode) = row("ArticleCode").ToString
                dr(enmDetail.PackQty) = Val(row("PackQty").ToString)
                dr(enmDetail.Qty) = Val(row("Qty").ToString)
                dr(enmDetail.TotalQty) = Val(row("TotalQty").ToString)
                dr(enmDetail.CurrentPrice) = Val(row("CurrentPrice").ToString)
                dr(enmDetail.PackPrice) = Val(row("PackPrice").ToString)
                dr(enmDetail.Price) = Val(row("Price").ToString)
                'dr(enmDetail.TotalAmount) = Val(row("TotalAmount").ToString)
                dr(enmDetail.Comments) = row("Comments").ToString
                dr(enmDetail.SaleAccountId) = Val(row("SaleAccountId").ToString)
                dr(enmDetail.DocId) = Val(row("DocId").ToString)
                dr(enmDetail.DocDetailId) = Val(row("DocDetailId").ToString)
                dr(enmDetail.WIPDocId) = Val(row("Ref_WIP_DocId").ToString)
                dr(enmDetail.WIPDocDetailId) = Val(row("Ref_WIP_DocDetailId").ToString)
                dt.Rows.Add(dr)
                dt.AcceptChanges()
            Next
            'End Task#19082015

            'Dim dt As New DataTable
            'Dim strSQL As String = String.Empty
            'strSQL = "SELECT IGP_D.LocationId, IGP_D.ArticleDefId, Art.ArticleDescription, Art.ArticleCode, IGP_D.PackQty, IsNull(IGP_D.Qty,0)  - IsNull(IGP_D.IssuedQty,0) as Qty , IsNull(IGP_D.TotalQty,0) as TotalQty, IGP_D.CurrentPrice, " _
            '    & "  IGP_D.Price, IGP_D.TotalAmount, IGP_D.Comments FROM dbo.IGPDetailTable AS IGP_D INNER JOIN dbo.ArticleDefView AS Art ON IGP_D.ArticleDefId = Art.ArticleId WHERE IGP_D.DocId=" & DocId & " And  (IsNull(IGP_D.Qty,0)  - IsNull(IGP_D.IssuedQty,0)) > 0 ORDER BY IGP_D.DocId "
            'dt = GetDataTable(strSQL)
            'dt.AcceptChanges()
            'Me.grdGatePass.DataSource = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbGatepass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGatepass.SelectedIndexChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.cmbGatepass.SelectedValue = 0 Then Exit Sub

            Me.TxtJobNo.Text = CType(Me.cmbGatepass.SelectedItem, DataRowView).Row.Item(2).ToString
            LoadGatePass(Me.cmbGatepass.SelectedValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVendor_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendor.Leave
        Try
            If IsOpenForm = False Then Exit Sub

            If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then
                Me.cmbVendor.Enabled = False
                If Me.rbtIGP.Checked = True Then
                    FillCombos("IGP_Documents")
                Else
                    FillCombos("WIP_Documents")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtIGP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtIGP.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.rbtIGP.Checked = True Then
                FillCombos("IGP_Documents")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtWIP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtWIP.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.rbtWIP.Checked = True Then
                FillCombos("WIP_Documents")
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
            id = Me.cmbProject.SelectedIndex
            FillCombos("Project")
            Me.cmbProject.SelectedIndex = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class