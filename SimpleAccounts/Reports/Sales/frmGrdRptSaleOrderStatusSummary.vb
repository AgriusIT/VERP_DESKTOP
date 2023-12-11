Imports SBUtility.Utility
Imports SBDal
Imports SBModel

Public Class frmGrdRptSaleOrderStatusSummary
    Implements IGeneral
    Private _dt As DataTable
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        'Try
        '    If Condition = "Master" Then
        '        Me.grdSaved.RootTable.Columns(enmAgreement.AgreementId).Visible = False
        '        Me.grdSaved.RootTable.Columns(enmAgreement.StateID).Visible = False
        '        Me.grdSaved.RootTable.Columns(enmAgreement.CityID).Visible = False
        '        Me.grdSaved.RootTable.Columns(enmAgreement.TerritoryID).Visible = False
        '        Me.grdSaved.RootTable.Columns(enmAgreement.Product_Category_Condition).Visible = False
        '        Me.grdSaved.RootTable.Columns(enmAgreement.Warranty_Condition).Visible = False
        '        Me.grdSaved.RootTable.Columns(enmAgreement.Term_Condition).Visible = False
        '        Me.grdSaved.RootTable.Columns(enmAgreement.Termination_Condition).Visible = False
        '        Me.grdSaved.RootTable.Columns(enmAgreement.AgreementDate).FormatString = "dd/MMM/yyyy"
        '    ElseIf Condition = "Detail" Then
        '        For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
        '            If Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.LocationId AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Qty AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Price AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Comments Then
        '                grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
        '            End If
        '        Next
        '    End If
        '    Me.grd.RootTable.Columns("Pack_Desc").Position = Me.grd.RootTable.Columns("Unit").Index
        '    Me.grd.RootTable.Columns("Unit").Position = Me.grd.RootTable.Columns("Pack_Desc").Index
        '    Me.grd.RootTable.Columns("Pack_Desc").Visible = True
        '    Me.grd.RootTable.Columns("Unit").Visible = False
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        'Try
        '    If LoginGroup = "Administrator" Then
        '        Me.btnSave.Enabled = True
        '        Me.btnDelete.Enabled = True
        '        Me.btnPrint.Enabled = True
        '        Exit Sub
        '    End If
        '    If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
        '        If RegisterStatus = EnumRegisterStatus.Expired Then
        '            Me.btnSave.Enabled = False
        '            Me.btnDelete.Enabled = False
        '            'Me.btnSearchDelete.Enabled = False
        '            Me.btnPrint.Enabled = False
        '            ' Me.btnSearchPrint.Enabled = False
        '            'Me.PrintListToolStripMenuItem.Enabled = False
        '            'PrintToolStripMenuItem.Enabled = False
        '            Exit Sub
        '        End If
        '        Dim dt As DataTable = GetFormRights(EnumForms.frmSalesReturn)
        '        If Not dt Is Nothing Then
        '            If Not dt.Rows.Count = 0 Then
        '                If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
        '                    Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
        '                Else
        '                    Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
        '                End If
        '                Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
        '                'Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
        '                Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
        '                'Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
        '                Me.Visible = dt.Rows(0).Item("View_Rights").ToString
        '            End If
        '        End If
        '        UserPostingRights = GetUserPostingRights(LoginUserId)
        '        If UserPostingRights = True Then
        '            Me.chkPost.Visible = True
        '        Else
        '            Me.chkPost.Visible = False
        '            Me.chkPost.Checked = False
        '        End If
        '        GetSecurityByPostingUser(UserPostingRights, btnSave, btnDelete)
        '    Else
        '        'Me.Visible = False
        '        Me.btnSave.Enabled = False
        '        Me.btnDelete.Enabled = False
        '        'Me.btnSearchDelete.Enabled = False
        '        Me.btnPrint.Enabled = False
        '        'Me.btnSearchPrint.Enabled = False
        '        Me.chkPost.Visible = False
        '        'CtrlGrdBar1.mGridPrint.Enabled = False
        '        'CtrlGrdBar1.mGridExport.Enabled = False

        '        For Each Rightsdt As GroupRights In Rights
        '            If Rightsdt.FormControlName = "View" Then
        '                'Me.Visible = True
        '            ElseIf Rightsdt.FormControlName = "Save" Then
        '                If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
        '            ElseIf Rightsdt.FormControlName = "Update" Then
        '                If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
        '            ElseIf Rightsdt.FormControlName = "Delete" Then
        '                Me.btnDelete.Enabled = True
        '                'Me.btnSearchDelete.Enabled = True
        '            ElseIf Rightsdt.FormControlName = "Print" Then
        '                Me.btnPrint.Enabled = True
        '                'Me.btnSearchPrint.Enabled = True
        '                'CtrlGrdBar1.mGridPrint.Enabled = True
        '            ElseIf Rightsdt.FormControlName = "Export" Then
        '                'CtrlGrdBar1.mGridExport.Enabled = True
        '            ElseIf Rightsdt.FormControlName = "Post" Then
        '                Me.chkPost.Visible = True
        '            End If
        '        Next
        '    End If


        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        'Try
        '    If Me.grdSaved.RootTable Is Nothing Then Return False
        '    Agreement = New AgreementMasterBE
        '    Agreement.AgreementId = Me.grdSaved.GetRow.Cells(enmAgreement.AgreementId).Value
        '    If New AgreementDAL().Delete(Agreement) Then
        '        Return True
        '    Else
        '        Return False
        '    End If

        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Dim strQuery As String = String.Empty
        Try
            ''Start TFS2945
            Dim strStatus() As String = System.Enum.GetNames(GetType(EnumStatus))
            Me.cmbStatus.Items.Clear()
            For Each sts As String In strStatus
                ' If sts <> Me.cmbStatus.SelectedItem.ToString Then
                Me.cmbStatus.Items.Add(sts)
                'End If
            Next
            Me.cmbStatus.SelectedIndex = 0

            strQuery = " SELECT coa_detail_id, detail_title from vwCOADetail  where account_type = 'customer' ORDER BY detail_title "
            FillDropDown(Me.cmbCustomer, strQuery)
            cmbCustomer.AutoCompleteMode = AutoCompleteMode.Append
            'Me.cmbCustomer.ValueMember = "coa_detail_id"
            'Me.cmbCustomer.DisplayMember = "detail_title"

            'Dim strQuery As String = String.Empty
            'strQuery = " SELECT coa_detail_id, detail_title from vwCOADetail  where account_type = 'customer' "
            'Dim dt As New DataTable
            'dt = UtilityDAL.GetDataTable(strQuery)
            'Me.cmbCustomer.DataSource = dt
        Catch ex As Exception

        End Try
        '' New CustomerDAL().getBusinessType

    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        'Try
        '    Agreement = New AgreementMasterBE
        '    Agreement.AgreementId = AgreementId
        '    Agreement.AgreementNo = Me.txtAgreementNo.Text
        '    Agreement.AgreementDate = Me.dtpAgreementDate.Value
        '    Agreement.Delivery_Date = Me.dtpDeliveryDate.Value
        '    Agreement.First_Payment = Val(Me.txtFirstPayment.Text)
        '    Agreement.AgreementType = IIf(Me.cmbAgreementType.Text.Length > 0, Me.cmbAgreementType.Text, "")
        '    Agreement.Business_Name = Me.txtBusinessName.Text
        '    Agreement.Contact_Name = Me.txtContactName.Text
        '    Agreement.StateID = Me.cmbState.SelectedValue
        '    Agreement.CityID = Me.cmbCity.SelectedValue
        '    Agreement.TerritoryID = Me.cmbTerritory.SelectedValue
        '    Agreement.Business_Type = Me.cmbBusinessType.Text
        '    Agreement.Address = Me.txtAddress.Text
        '    Agreement.Phone = Me.txtPhone.Text
        '    Agreement.FaxNo = Me.txtFaxNo.Text
        '    Agreement.Email = Me.txtEmail.Text
        '    Agreement.Status = IIf(Me.chkPost.Visible = True, Me.chkPost.Checked, False)
        '    Agreement.Product_Category_Condition = Me.txtProudctAndCategory.Text
        '    Agreement.Term_Condition = Me.txtTermsAndCondition.Text
        '    Agreement.Warranty_Condition = Me.txtWarnty.Text
        '    Agreement.Termination_Condition = Me.txtDeployementSchedule.Text
        '    Agreement.Total_Qty = Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGrdDetail.Qty), Janus.Windows.GridEX.AggregateFunction.Sum)
        '    Agreement.Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGrdDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)
        '    Agreement.User_Name = LoginUserName
        '    Agreement.Discount = Val(Me.txtDiscount.Text)
        '    Agreement.AgreementDetail = New List(Of AgreementDetailBE)
        '    Dim AgreementDt As AgreementDetailBE
        '    For Each row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
        '        With Agreement.AgreementDetail
        '            AgreementDt = New AgreementDetailBE
        '            AgreementDt.AgreementId = AgreementId
        '            AgreementDt.LocationId = row.Cells("LocationId").Value
        '            AgreementDt.ArticleDefId = row.Cells("ArticleDefId").Value
        '            AgreementDt.ArticleSize = row.Cells("Unit").Value.ToString
        '            AgreementDt.Sz1 = Val(row.Cells("Qty").Value.ToString)
        '            AgreementDt.Sz2 = row.Cells("PackQty").Value
        '            AgreementDt.Qty = IIf(row.Cells("Unit").Value.ToString = "Loose", row.Cells("Qty").Value, (Val(row.Cells("Qty").Value.ToString) * Val(row.Cells("PackQty").Value.ToString)))
        '            AgreementDt.Price = Val(row.Cells("Price").Value.ToString)
        '            AgreementDt.CurrentPrice = Val(row.Cells("CurrentPrice").Value.ToString)
        '            AgreementDt.Comments = row.Cells("Comments").Value.ToString
        '            AgreementDt.Pack_Desc = row.Cells("Pack_Desc").Value.ToString
        '            .Add(AgreementDt)
        '        End With
        '    Next
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Dim strQuery As String = String.Empty

            'strQuery = " select  row_number() OVER (ORDER BY SalesOrderDate desc) SrNo,  SalesOrderDate, detail_title, dbo.SalesOrderMasterTable.SalesOrderId," &
            '            "   dbo.SalesOrderMasterTable.SalesOrderNo, dbo.SalesOrderMasterTable.VendorId, NULL as CustomerName,   dbo.SalesOrderMasterTable.SalesOrderId,  " &
            '            "   sum( dbo.SalesOrderMasterTable.SalesOrderQty ) as SalesOrderQty ,ISNULL( sum(dbo.DeliveryChalanMasterTable.DeliveryQty) ,0 ) as DeliveredQuantity, " &
            '            "   IIF(sum( dbo.SalesOrderMasterTable.SalesOrderQty ) <= sum(dbo.DeliveryChalanMasterTable.DeliveryQty)  , 'CLOSE', 'OPEN') as DelQuantityStatus, " &
            '            "   isnull( sum(SalesMasterTable.SalesQty), 0)  as InvoiceQuantity, " &
            '            "  iif(sum( dbo.SalesOrderMasterTable.SalesOrderQty ) <= sum(SalesMasterTable.SalesQty), 'ClOSE','OPEN') as InvouceQuantityStatus  " &
            '            "   from dbo.SalesOrderMasterTable " &
            '            "  left join dbo.DeliveryChalanMasterTable on dbo.DeliveryChalanMasterTable.POId = dbo.SalesOrderMasterTable.SalesOrderId   " &
            '            "  left join dbo.SalesMasterTable  on(SalesMasterTable.POId =  dbo.SalesOrderMasterTable.SalesOrderId  or dbo.SalesMasterTable.DeliveryChalanId = " &
            '            "  dbo.DeliveryChalanMasterTable.DeliveryId)  left join vwCOADetail on vwCOADetail.coa_detail_id = dbo.SalesOrderMasterTable.VendorId  " &
            '            " where 1 = 1 "

            'strQuery = " select   SalesOrderDate, detail_title, dbo.SalesOrderMasterTable.SalesOrderId," &
            '           "   dbo.SalesOrderMasterTable.SalesOrderNo, dbo.SalesOrderMasterTable.VendorId, NULL as CustomerName,   dbo.SalesOrderMasterTable.SalesOrderId,  " &
            '           "dbo.SalesOrderMasterTable.SalesOrderQty  as SalesOrderQty ,ISNULL( sum(dbo.DeliveryChalanMasterTable.DeliveryQty) , 0 ) as DeliveredQuantity, " &
            '           "   IIF( dbo.SalesOrderMasterTable.SalesOrderQty  <= sum(dbo.DeliveryChalanMasterTable.DeliveryQty)  , 'CLOSE', 'OPEN') as DelQuantityStatus, " &
            '           "   isnull(SUM( SalesMasterTable.SalesQty), 0)  as InvoiceQuantity, " &
            '           "   iif( dbo.SalesOrderMasterTable.SalesOrderQty  <= sum(SalesMasterTable.SalesQty), 'ClOSE', 'OPEN') as InvouceQuantityStatus  " &
            '           "   from dbo.SalesOrderMasterTable " &
            '           "   left join dbo.DeliveryChalanMasterTable on dbo.DeliveryChalanMasterTable.POId = dbo.SalesOrderMasterTable.SalesOrderId   " &
            '           "   left join dbo.SalesMasterTable  on(SalesMasterTable.POId =  dbo.SalesOrderMasterTable.SalesOrderId  or dbo.SalesMasterTable.DeliveryChalanId = " &
            '           "   dbo.DeliveryChalanMasterTable.DeliveryId)  left join vwCOADetail on vwCOADetail.coa_detail_id = dbo.SalesOrderMasterTable.VendorId  " &
            '           "   where 1 = 1 "

            ''Commented Against TFS2945 : Ayesha Rehman : 11-04-2018
            '            strQuery = " select   SalesOrderDate, detail_title, dbo.SalesOrderMasterTable.SalesOrderId, " &
            ' "  dbo.SalesOrderMasterTable.SalesOrderNo, dbo.SalesOrderMasterTable.VendorId, NULL as CustomerName,   dbo.SalesOrderMasterTable.SalesOrderId,  " &
            ' "  dbo.SalesOrderMasterTable.SalesOrderQty  as SalesOrderQty ,ISNULL( sum(dbo.DeliveryChalanMasterTable.DeliveryQty) , 0 ) as DeliveredQuantity,   " &
            ' "  case when dbo.SalesOrderMasterTable.SalesOrderQty  <= sum(dbo.DeliveryChalanMasterTable.DeliveryQty)  then 'CLOSE' else 'OPEN' end as DelQuantityStatus,  " &
            '"  isnull(SUM( SalesMasterTable.SalesQty), 0)  as InvoiceQuantity,   " &
            '"  case when dbo.SalesOrderMasterTable.SalesOrderQty  <= sum(SalesMasterTable.SalesQty) then 'ClOSE' else 'OPEN' end as InvouceQuantityStatus    " &
            '"  from dbo.SalesOrderMasterTable left join dbo.DeliveryChalanMasterTable on dbo.DeliveryChalanMasterTable.POId = dbo.SalesOrderMasterTable.SalesOrderId    " &
            '"  left join dbo.SalesMasterTable  on(SalesMasterTable.POId =  dbo.SalesOrderMasterTable.SalesOrderId  or dbo.SalesMasterTable.DeliveryChalanId =   " &
            '"  dbo.DeliveryChalanMasterTable.DeliveryId)  left join vwCOADetail on vwCOADetail.coa_detail_id = dbo.SalesOrderMasterTable.VendorId  where 1 = 1  "
            ''Commented Against TFS3326 : Ayesha Rehman : 31-05-2018

            'strQuery = "SELECT SalesOrderMasterTable.SalesOrderNo, SalesOrderMasterTable.SalesOrderDate, " _
            '        & " vwCOADetail.detail_title, SalesOrderMasterTable.Remarks, SUM(SalesOrderDetailTable.qty) AS SOQty, ISNULL(SUM(SalesOrderDetailTable.DCTotalQty), 0) " _
            '        & " AS DeliveredQty, SalesOrderMasterTable.DCStatus, isnull(SUM(SalesDetailTable.Qty), 0)  as SalesQty, SalesOrderMasterTable.Status , " _
            '        & " SalesOrderMasterTable.SalesOrderId " _
            '        & " FROM SalesOrderDetailTable INNER JOIN " _
            '        & " SalesOrderMasterTable ON SalesOrderDetailTable.SalesOrderId = SalesOrderMasterTable.SalesOrderId INNER JOIN " _
            '        & " vwCOADetail ON SalesOrderMasterTable.VendorID = vwCOADetail.coa_detail_id Left Outer JOIN  " _
            '        & " dbo.SalesDetailTable  ON SalesDetailTable.SO_Detail_ID  =  dbo.SalesOrderDetailTable.SalesOrderDetailId   " _
            '        & " " & IIf(Me.cmbStatus.Text = "Close", " Where (SalesOrderMasterTable.DCStatus = '" & Me.cmbStatus.Text & "' And SalesOrderMasterTable.Status = '" & Me.cmbStatus.Text & "')", " Where (SalesOrderMasterTable.DCStatus In('Open', 'Close', 'Reject', 'DeActive') OR SalesOrderMasterTable.Status In('Open', 'Close', 'Reject', 'DeActive')) ") & " "

            ''Start TFS3793 : Ayesha Rehman : 04-07-2018 : (SalesOrderDetailTable.SalesOrderDetailId ) Added in Query to group by against it 
            strQuery = "SELECT SalesOrderMasterTable.SalesOrderNo, SalesOrderMasterTable.SalesOrderDate, " _
                    & " vwCOADetail.detail_title, SalesOrderMasterTable.Remarks, SUM(SalesOrderDetailTable.qty) AS SOQty, ISNULL(SUM(SalesOrderDetailTable.DCTotalQty), 0) " _
                    & " AS DeliveredQty, SalesOrderMasterTable.DCStatus, a.SalesQty   as SalesQty, SalesOrderMasterTable.Status , " _
                    & " SalesOrderMasterTable.SalesOrderId ,  SalesOrderDetailTable.SalesOrderDetailId " _
                    & " FROM SalesOrderDetailTable Left Outer JOIN  " _
                    & "(  Select isnull(SUM(SalesDetailTable.Qty), 0)  as SalesQty , SalesDetailTable.SO_Detail_ID   from dbo.SalesDetailTable Inner Join SalesOrderDetailTable ON SalesDetailTable.SO_Detail_ID  =  dbo.SalesOrderDetailTable.SalesOrderDetailId group by SO_Detail_ID )a on a.SO_Detail_ID = SalesOrderDetailTable.SalesOrderDetailId   " _
                    & " INNER JOIN SalesOrderMasterTable ON SalesOrderDetailTable.SalesOrderId = SalesOrderMasterTable.SalesOrderId INNER JOIN " _
                    & " vwCOADetail ON SalesOrderMasterTable.VendorID = vwCOADetail.coa_detail_id " _
                    & " " & IIf(Me.cmbStatus.Text = "Close", " Where (SalesOrderMasterTable.DCStatus = '" & Me.cmbStatus.Text & "' And SalesOrderMasterTable.Status = '" & Me.cmbStatus.Text & "')", "" & IIf(Me.cmbStatus.Text = "Open", " Where ((SalesOrderMasterTable.DCStatus != '" & EnumStatus.Close.ToString & "' Or SalesOrderMasterTable.Status != '" & EnumStatus.Close.ToString & "'))", " Where (SalesOrderMasterTable.DCStatus In('Open', 'Close', 'Reject', 'DeActive') OR SalesOrderMasterTable.Status In('Open', 'Close', 'Reject', 'DeActive')) ") & " ") & " "
            Dim count As Integer = 0
            If rdbSpecificOrder.Checked = True Then
                If txtOrderNo.Text.Trim() <> "" Then
                    strQuery += " and  dbo.SalesOrderMasterTable.SalesOrderNo like  '%" & txtOrderNo.Text & "%'"
                    If cmbCustomer.SelectedValue > 0 Then
                        strQuery += " and  dbo.SalesOrderMasterTable.VendorId = " & cmbCustomer.SelectedValue.ToString() & ""
                    End If
                    count = count + 1
                Else
                    strQuery += " and   2 = 3 "
                End If
            Else
                If cmbCustomer.SelectedValue > 0 Then
                    strQuery += " and  dbo.SalesOrderMasterTable.VendorId = " & cmbCustomer.SelectedValue.ToString() & ""
                    count = count + 1
                End If
                If rdbAllSalesOrders.Checked = True Then
                    count = count + 1
                End If
                If rdbDateRange.Checked = True Then
                    If Convert.ToDateTime(dtTo.Value.ToString("yyyy-M-d")) < Convert.ToDateTime(dtFrom.Value.ToString("yyyy-M-d")) Then
                        msg_Error("From Date must be less than To Date")
                        Return
                    End If
                    count = count + 1
                    'strQuery += " and Convert(Varchar,SalesOrderDate, 102) >= Convert(Datetime,'" & dtFrom.Value.ToString("MM/dd/yyyy") & "', 102) and Convert(Varchar,SalesOrderDate, 102) <= Convert(Datetime,'" & dtTo.Value.AddDays(1).ToString("MM/dd/yyyy") & "', 102)"
                    strQuery += " and Convert(Varchar,SalesOrderDate, 102) >= Convert(Datetime,'" & dtFrom.Value.ToString("yyyy-M-d") & "', 102) and Convert(Varchar,SalesOrderDate, 102) <= Convert(Datetime,'" & dtTo.Value.AddDays(1).ToString("yyyy-M-d") & "', 102)"
                End If
            End If
            If count = 0 Then
                strQuery += " and   2 = 3 "
            End If

            strQuery += " GROUP BY SalesOrderMasterTable.SalesOrderNo, SalesOrderMasterTable.SalesOrderDate, vwCOADetail.detail_title,  SalesOrderMasterTable.Remarks," _
                    & "  SalesOrderDetailTable.SalesOrderDetailId , SalesOrderMasterTable.SalesOrderId, SalesOrderMasterTable.Status, SalesOrderMasterTable.DCStatus, a.SalesQty "
            strQuery += " ORDER BY SalesOrderMasterTable.SalesOrderDate Desc"
            FillGridEx(grd, strQuery, False)
            _dt = CType(Me.grd.DataSource, DataTable)
            grd.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.False
            grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                col.AutoSize()
                col.Table.Columns(6).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                col.Table.Columns(7).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                col.Table.Columns(8).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                col.Table.Columns(5).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.Table.Columns(6).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.Table.Columns(7).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.Table.Columns(8).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.Table.Columns(6).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.Table.Columns(7).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                col.Table.Columns(8).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Next
            'Me.grdSaved.RootTable.Columns("Dev Date").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grd.RootTable.Columns(0).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("SalesOrderDate").FormatString = str_DisplayDateFormat

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        'Try
        'how is it going?

        '    If Me.txtBusinessName.Text = String.Empty Then
        '        ShowErrorMessage("Please enter business name")
        '        Me.txtBusinessName.Focus()
        '        Return False
        '    End If
        '    If txtContactName.Text = String.Empty Then
        '        ShowErrorMessage("Please enter contact name")
        '        Me.txtContactName.Focus()
        '        Return False
        '    End If
        '    If Me.txtAddress.Text = String.Empty Then
        '        ShowErrorMessage("Please enter address")
        '        Me.txtAddress.Focus()
        '        Return False
        '    End If
        '    If Me.txtAddress.Text = String.Empty Then
        '        ShowErrorMessage("Please enter phone")
        '        Me.txtPhone.Focus()
        '        Return False
        '    End If
        '    If Me.grd.RowCount = 0 Then
        '        ShowErrorMessage("Record not in grid")
        '        Me.grd.Focus()
        '        Return False
        '    End If

        '    FillModel() 'Call Fillmodel 
        '    Return True
        'Catch ex As Exception

        'End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            txtOrderNo.Text = ""
            rdbSpecificOrder.Checked = False
            rdbDateRange.Checked = False
            rdbAllSalesOrders.Checked = False
            grd.DataSource = Nothing
        Catch ex As Exception
            Throw ex
        End Try
        'Try
        '    AgreementId = 0I
        '    Me.BtnSave.Text = "&Save"
        '    Me.txtAgreementNo.Text = GetDocumentNo()
        '    Me.dtpAgreementDate.Value = Date.Now
        '    Me.dtpDeliveryDate.Value = Date.Now
        '    Me.txtFirstPayment.Text = String.Empty
        '    Me.txtBusinessName.Text = String.Empty
        '    Me.txtContactName.Text = String.Empty
        '    Me.txtAddress.Text = String.Empty
        '    Me.txtPhone.Text = String.Empty
        '    Me.txtFaxNo.Text = String.Empty
        '    Me.txtEmail.Text = String.Empty
        '    Me.cmbUnit.SelectedIndex = 0
        '    Me.txtBusinessName.Focus()
        '    Me.txtTermsAndCondition.Text = New AgreementDAL().GetTermAndCondition
        '    Me.txtDeployementSchedule.Text = New AgreementDAL().GetTerminationCondition
        '    Me.txtWarnty.Text = New AgreementDAL().GetWarrantyCondition
        '    Me.txtProudctAndCategory.Text = New AgreementDAL().GetProductionCategoryCondition
        '    FillCombos("BusinessType")
        '    FillCombos("AgreementType")
        '    If Not Me.cmbAgreementType.SelectedIndex = -1 Then Me.cmbAgreementType.Text = String.Empty
        '    GetAllRecords("Master")
        '    GetAllRecords("Detail")

        '    ApplySecurity(EnumDataMode.[New])
        '    'ClearData()
        '    Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        'Try
        '    If New AgreementDAL().Add(Agreement) Then
        '        Return True
        '    Else
        '        Return False
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        'Try
        '    If New AgreementDAL().Update(Agreement) Then
        '        Return True
        '    Else
        '        Return False
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Function
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                'Me.BtnSave.Enabled = True
                'Me.BtnDelete.Enabled = True
                Me.Print.Enabled = True
                CtrlGrdBarFront.mGridExport.Visible = True
                CtrlGrdBarFront.mGridPrint.Visible = True
                CtrlGrdBarFront.mGridChooseFielder.Visible = True
                'Me.grdMaterialEstimation.RootTable.Columns("Approve").Visible = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    'Me.BtnSave.Enabled = False
                    'Me.BtnDelete.Enabled = False
                    'Me.btnPrint.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmMaterialEstimation)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        'If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                        '    Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        'Else
                        '    Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        'End If
                        'Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.Print.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'Me.PrintListToolStripMenuItem.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'PrintToolStripMenuItem.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString ''R934 Added Dlete Button
                        'Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString  ''R934 Added Print Button
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If

            Else
                'Me.btnSave.Enabled = False
                'Me.BtnDelete.Enabled = False
                Me.Print.Enabled = False
                CtrlGrdBarFront.mGridExport.Visible = False
                CtrlGrdBarFront.mGridPrint.Visible = False
                CtrlGrdBarFront.mGridChooseFielder.Visible = False
                'Me.grdMaterialEstimation.RootTable.Columns("Approve").Visible = False
                If Rights Is Nothing Then Exit Sub
                For Each RightstDt As GroupRights In Rights
                    If RightstDt.FormControlName = "View" Then
                        Me.Visible = True
                        'ElseIf RightstDt.FormControlName = "Save" Then
                        '    If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                        'ElseIf RightstDt.FormControlName = "Update" Then
                        '    If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        'ElseIf RightstDt.FormControlName = "Delete" Then
                        '    Me.BtnDelete.Enabled = True
                    ElseIf RightstDt.FormControlName = "Print" Then
                        Me.Print.Enabled = True
                        CtrlGrdBarFront.mGridPrint.Visible = True
                        'ElseIf RightstDt.FormControlName = "Approve" Then
                        'Me.grdMaterialEstimation.RootTable.Columns("Approve").Visible = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightstDt.FormControlName = "Export" Then
                        CtrlGrdBarFront.mGridExport.Visible = True


                        'ElseIf RightstDt.FormControlName = "Post" Then

                    ElseIf RightstDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBarFront.mGridChooseFielder.Visible = True
                        'End Task:2406
                        'ElseIf RightstDt.FormControlName = "Attachment" Then
                        'Me.btnAttachment.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmGrdRptSaleOrderStatusSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetSecurityRights()
        ToolTip1.SetToolTip(cmbCustomer, "Select Customer")
        ToolTip1.SetToolTip(dtTo, "Select Initial Date")
        ToolTip1.SetToolTip(dtFrom, "Select End Date")
        ToolTip1.SetToolTip(rdbAllSalesOrders, "Select All Orders")
        ToolTip1.SetToolTip(rdbDateRange, "Search Records by Date Range")
        ToolTip1.SetToolTip(rdbSpecificOrder, "Select Specific Order")
        ToolTip1.SetToolTip(btnLoad, "Click to load records")
        FillCombos()
        rdbAllSalesOrders.Checked = True
        cmbCustomer.Focus()

    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        GetAllRecords()
    End Sub

    Private Sub txtOrderNo_KeyPress(sender As Object, e As KeyPressEventArgs)
        NumValidation(sender, e)
    End Sub

    Private Sub dtTo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtTo.Validating

    End Sub

    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs)

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        FillCombos()
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub CtrlGrdBarFront_Load(sender As Object, e As EventArgs) Handles CtrlGrdBarFront.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBarFront.txtGridTitle.Text = CompanyTitle & Chr(10) & " Import Detail " & Chr(10) & "From Date: " & dtFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtTo.Value.ToString("dd-MM-yyyy").ToString()

            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStatus.SelectedIndexChanged
        Try

            Me.cmbSetTo.Items.Clear()
            Dim strStatus() As String = System.Enum.GetNames(GetType(EnumStatus))
            For Each sts As String In strStatus
                If sts <> Me.cmbStatus.SelectedItem.ToString Then
                    Me.cmbSetTo.Items.Add(sts)
                End If
            Next
            If Me.cmbSetTo.Items.Count > 0 Then
                Me.cmbSetTo.SelectedIndex = 0
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim trans As OleDb.OleDbTransaction
        If Con.State = ConnectionState.Open Then Con.Close()
        Con.Open()
        trans = Con.BeginTransaction
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim cmd As New OleDb.OleDbCommand
            cmd.Connection = Con



            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans

            If Me.grd.RowCount = 0 Then Exit Sub
            'If Not Me.IsValidate() Then Exit Sub
            If Not msg_Confirm("Do you want to update selected records?") Then Exit Sub
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetCheckedRows
                If r.Cells(0).Value = True Then
                    cmd.CommandText = "Update SalesOrderMasterTable set DCStatus = '" & cmbSetTo.SelectedItem.ToString & "' where SalesOrderID = " & r.Cells("SalesOrderId").Value & ""
                    cmd.ExecuteNonQuery()
                    cmd.CommandText = "Update SalesOrderMasterTable set Status = '" & cmbSetTo.SelectedItem.ToString & "' where SalesOrderID = " & r.Cells("SalesOrderId").Value & ""
                    cmd.ExecuteNonQuery()
                End If
            Next
            trans.Commit()

            msg_Information("Records updated successfully")
            ''insert Activity Log
            SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Sales, "abc", True)
            Me.GetAllRecords()
            Me.GetSecurityRights()
        Catch ex As Exception
            trans.Rollback()
            msg_Error(ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub Print_Click(sender As Object, e As EventArgs) Handles Print.Click
        Try
            ShowReport("rptSOStatusWithDC ", , , , , , , _dt)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

 
End Class

'select SalesOrderDate, dbo.SalesOrderMasterTable.SalesOrderId, dbo.SalesOrderMasterTable.VendorId, dbo.tblCustomer.CustomerName, 
' dbo.SalesOrderMasterTable.SalesOrderId, dbo.SalesOrderMasterTable.SalesOrderQty,
'sum(dbo.DeliveryChalanMasterTable.DeliveryQty) as DeliveredQuantity ,  
'CASE 
'    WHEN  (sum(dbo.SalesOrderMasterTable.SalesOrderQty) - sum(dbo.DeliveryChalanMasterTable.DeliveryQty)) <> 0 THEN 'OPEN' 
'     WHEN  (sum(dbo.SalesOrderMasterTable.SalesOrderQty) - sum(dbo.DeliveryChalanMasterTable.DeliveryQty)) = 0 THEN 'CLOSE'
'  END AS DelQuantityStatus, 
'  sum(SalesMasterTable.SalesQty) as InvoiceQuantity,
'  CASE 
'    WHEN  (sum(dbo.SalesOrderMasterTable.SalesOrderQty) - sum(SalesMasterTable.SalesQty)) <> 0 THEN 'OPEN' 
'     WHEN  (sum(dbo.SalesOrderMasterTable.SalesOrderQty) - sum(SalesMasterTable.SalesQty)) = 0 THEN 'CLOSE' 
'  END AS InvouceQuantityStatus
'from dbo.SalesOrderMasterTable 
'left join dbo.DeliveryChalanMasterTable on dbo.DeliveryChalanMasterTable.POId = dbo.SalesOrderMasterTable.SalesOrderId
'left join dbo.SalesMasterTable on SalesMasterTable.POId =  dbo.SalesOrderMasterTable.SalesOrderId
'left join dbo.tblCustomer on dbo.tblCustomer.CustomerID = dbo.SalesOrderMasterTable.VendorId
'  group by SalesOrderDate, dbo.SalesOrderMasterTable.SalesOrderId, dbo.SalesOrderMasterTable.VendorId,  dbo.tblCustomer.CustomerName,
'  dbo.SalesOrderMasterTable.SalesOrderId, dbo.SalesOrderMasterTable.SalesOrderQty 
