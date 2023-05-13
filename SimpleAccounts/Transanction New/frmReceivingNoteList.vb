''12-Sep-2014 Task:2842 Imran Ali Good Receiving Note Same Data Load on Purchase
'''2015-02-21 Task# 2015020003 changes By Ali Ansari Making ComboBox AutoFill 
''' 2017-09-06 TASK TFS1391 : Option to load Multi GRNs on Purchase invoice. Ameen
Public Class frmReceivingNoteList
    Public flgMultipleSalesOrder As Boolean = False
    Public flgMultiGRN As Boolean = False
    Public IsClosed As Boolean = False
    Public Sub frmReceivingNoteList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ''''2015-02-21 Task# 2015020003 changes By Ali Ansari Making ComboBox AutoFill 
            'cmbReceiving.AutoCompleteMode = AutoCompleteMode.Suggest
            'cmbReceiving.DropDownStyle = ComboBoxStyle.DropDown
            'cmbReceiving.AutoCompleteSource = AutoCompleteSource.ListItems
            ''''2015-02-21 Task# 2015020003 changes By Ali Ansari Making ComboBox AutoFill 
            If Not Me.cmbVendor.ActiveRow Is Nothing Then
                Dim ID As Integer = 0
                ID = Me.cmbVendor.Value
                FillCombo("Vendor")
                Me.cmbVendor.Value = ID
            Else
                FillCombo("Vendor")
            End If
            FillCombo("Recv")
            'If Not getConfigValueByType("MultipleSalesOrder").ToString = "Error" Then
            '    flgMultipleSalesOrder = Convert.ToBoolean(getConfigValueByType("MultipleSalesOrder").ToString)
            'Else
            '    flgMultipleSalesOrder = False
            'End If
            'If flgMultipleSalesOrder Then
            '    Me.btnClose.Visible = True
            'Else
            '    Me.btnClose.Visible = False
            'End If
            If Not getConfigValueByType("LoadMultiGRN").ToString = "Error" Then
                flgMultiGRN = Convert.ToBoolean(getConfigValueByType("LoadMultiGRN").ToString)
            Else
                flgMultiGRN = False
            End If
            If flgMultiGRN Then
                Me.btnClose.Visible = True
                Me.btnClose.Location = New System.Drawing.Point(350, 91)
                Me.btnLoad.Location = New System.Drawing.Point(280, 91)
            Else
                Me.btnClose.Visible = False
                Me.btnClose.Location = New System.Drawing.Point(300, 19)
                Me.btnLoad.Location = New System.Drawing.Point(350, 91)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            If Condition = "Recv" Then
                'FillDropDown(Me.cmbReceiving, "Select ReceivingNoteMasterTable.ReceivingNoteId,ReceivingNoteMasterTable.ReceivingNo,* From ReceivingNoteMasterTable WHERE ReceivingNoteMasterTable.ReceivingNoteId Not In(Select IsNull(ReceivingNoteId,0) From ReceivingMasterTable) " & IIf(Me.cmbVendor.ActiveRow Is Nothing, "", " AND VendorId=" & Me.cmbVendor.ActiveRow.Cells(0).Value & "") & " AND IsNull(Post,0)=1  ORDER BY ReceivingNoteMasterTable.ReceivingNoteId DESC")
                ''TFS1391 if GRN Id exists in ReceivingDetailTable then it should not load again.
                'FillDropDown(Me.cmbReceiving, "Select ReceivingNoteMasterTable.ReceivingNoteId, ReceivingNoteMasterTable.ReceivingNo,* From ReceivingNoteMasterTable WHERE ReceivingNoteMasterTable.ReceivingNoteId Not In(Select Distinct IsNull(ReceivingNoteId,0) As ReceivingNoteId From ReceivingDetailTable) OR ReceivingNoteMasterTable.ReceivingNoteId NOT IN(Select Distinct IsNull(ReceivingNoteId,0) As ReceivingNoteId From LCDetailTable)  " & IIf(Me.cmbVendor.ActiveRow Is Nothing, "", " AND VendorId=" & Me.cmbVendor.ActiveRow.Cells(0).Value & "") & " AND IsNull(Post,0)=1  ORDER BY ReceivingNoteMasterTable.ReceivingNoteId DESC")
                FillDropDown(Me.cmbReceiving, "Select ReceivingNoteMasterTable.ReceivingNoteId, ReceivingNoteMasterTable.ReceivingNo,* From ReceivingNoteMasterTable WHERE ReceivingNoteMasterTable.ReceivingNoteId Not In(Select Distinct Case When IsNull(Detail.ReceivingNoteId,0) > 0 Then Detail.ReceivingNoteId Else IsNull(Receiving.ReceivingNoteId, 0) END As ReceivingNoteId From ReceivingDetailTable AS Detail INNER JOIN ReceivingMasterTable AS Receiving ON Detail.ReceivingId = Receiving.ReceivingId  Union All Select Distinct IsNull(ReceivingNoteId,0) As ReceivingNoteId From LCDetailTable) " & IIf(Me.cmbVendor.ActiveRow Is Nothing, "", " AND VendorId=" & Me.cmbVendor.ActiveRow.Cells(0).Value & "") & " AND IsNull(Post,0)=1  ORDER BY ReceivingNoteMasterTable.ReceivingNoteId DESC")

            ElseIf Condition = "Vendor" Then
                FillUltraDropDown(Me.cmbVendor, "Select coa_detail_id, detail_title as Vendor, detail_code as [Cdode], sub_sub_title as [Account Head], Account_Type as Type From vwCOADetail where detail_title <> ''  And " & IIf(getConfigValueByType("Show Customer On Purchase") = "True", " Account_Type in('Vendor','Customer')", " Account_Type in('Vendor') ") & " Order By detail_title ASC")
                Me.cmbVendor.Rows(0).Activate()
                If Me.cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            ''12-Sep-2014 Task:2842 Imran Ali Good Receiving Note Same Data Load on Purchase
            'Dim purchase As  frmPurchaseNew
            If flgMultiGRN = False Then
                If Val(frmPurchaseNew.grd.RowCount) > 0 Then
                    If msg_Confirm("Purchase voucher is in updation mode, do you still want to load GRN?") = False Then Exit Sub
                End If
            End If
            If Val(frmPurchaseNew.grd.RowCount) > 0 Then
                Dim dtGrid As DataTable = CType(frmPurchaseNew.grd.DataSource, DataTable)
                For Each Row As DataRow In dtGrid.Rows
                    If Me.cmbReceiving.SelectedValue = Row.Item("ReceivingNoteId") Then
                        msg_Error("Selected GRN already exists.")
                        Exit Sub
                    End If
                Next
            End If
            frmPurchaseNew.cmbVendor.Value = Val(CType(Me.cmbReceiving.SelectedItem, DataRowView).Row.Item("VendorId").ToString)
            frmPurchaseNew.txtRemarks.Text = CType(Me.cmbReceiving.SelectedItem, DataRowView).Row.Item("Remarks").ToString
            frmPurchaseNew.txtDcNo.Text = CType(Me.cmbReceiving.SelectedItem, DataRowView).Row.Item("DcNo").ToString
            frmPurchaseNew.txtInvoiceNo.Text = CType(Me.cmbReceiving.SelectedItem, DataRowView).Row.Item("vendor_invoice_no").ToString
            frmPurchaseNew.cmbProject.SelectedValue = Val(CType(Me.cmbReceiving.SelectedItem, DataRowView).Row.Item("CostCenterId").ToString)
            frmPurchaseNew.txtIGPNo.Text = CType(Me.cmbReceiving.SelectedItem, DataRowView).Row.Item("IGPNo").ToString
            'frmPurchaseNew.cmbCurrency.SelectedValue = Val(CType(Me.cmbReceiving.SelectedItem, DataRowView).Row.Item("CurrencyType").ToString)
            'frmPurchaseNew.txtCurrencyRate.Text = Val(CType(Me.cmbReceiving.SelectedItem, DataRowView).Row.Item("CurrencyRate").ToString)
            frmPurchaseNew.txtVhNo.Text = CType(Me.cmbReceiving.SelectedItem, DataRowView).Row.Item("Vehicle_No").ToString
            frmPurchaseNew.cmbTransportationVendor.Value = Val(CType(Me.cmbReceiving.SelectedItem, DataRowView).Row.Item("Transportation_Vendor").ToString)
            frmPurchaseNew.intReceivingNoteId = Val(Me.cmbReceiving.SelectedValue)
            'End Task:2842
            DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbVendor_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.Leave
        Try
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            If Me.cmbVendor.ActiveRow IsNot Nothing Then
                FillCombo("Recv")
            Else
                Exit Sub
            End If
            If Me.cmbVendor.Value > 0 AndAlso flgMultiGRN = True Then
                Me.cmbVendor.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
            Me.IsClosed = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmReceivingNoteList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            'e.Cancel = True
            'If Not Me.cmbVendor.ActiveRow Is Nothing Then
            '    EM()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmReceivingNoteList_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class