''19-Dec-2013 ReqId-934 Imran Ali           Hide Edit/Delete/Price Button
Imports System.Data.OleDb
Public Class frmUpdatebitlyAndTransporter
    Dim dt As DataTable
    Dim IsFormOpend As Boolean = False
    Private Sub FillCombo(ByVal strCondition As String)
        Try
            Dim str As String
            If strCondition = "Transporter" Then
                str = "select * from tbldeftransporter where active=1 order by sortorder,2"
                FillDropDown(Me.cmbTransporter, str)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmUpdatebitlyAndTransporter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
            If e.KeyCode = Keys.F4 Then
                SaveToolStripButton_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.F5 Then
                btnRefresh_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub frmUpdatebitlyAndTransporter_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.GetSecurityRights()
            Me.FillCombo("Transporter")
            IsFormOpend = True
            Me.DisplayRecord()
            ' Me.IsFormOpend = True
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub frmUpdatebitlyAndTransporter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub

    Private Sub DisplayRecord()
        Try
            Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
            Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
            Dim str As String = String.Empty
            'str = "SELECT     Recv.SalesNo, CONVERT(varchar, Recv.SalesDate, 103) AS Date, V.CustomerName, Recv.SalesQty, Recv.SalesAmount, " _
            '       & " Recv.SalesId, Recv.CustomerCode, Recv.Remarks, convert(varchar, Recv.CashPaid) as CashPaid FROM         dbo.SalesMasterTable Recv INNER JOIN dbo.tblCustomer V ON Recv.CustomerCode = V.AccountId"

            'str = "SELECT Recv.SalesNo, Recv.SalesDate AS Date, Recv.DeliveryDate as [Delivery Date], vwCOADetail.detail_title as CustomerName, V.SalesOrderNo, Recv.SalesQty, Recv.SalesAmount, ISNULL(Recv.Delivered,0) as Delivered, Recv.SalesId,  " & _
            '        "Recv.CustomerCode, tbldefEmployee.Employee_Name, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId, Recv.BiltyNo,isnull(Recv.TransporterId ,0) as TransporterId " & _
            '        "FROM SalesMasterTable Recv INNER JOIN " & _
            '        "vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " & _
            '        "tblDefEmployee ON Recv.EmployeeCode = tblDefEmployee.Employee_Id LEFT OUTER JOIN " & _
            '        "SalesOrderMasterTable V ON Recv.POId = V.SalesOrderId " & _

            str = "SELECT Recv.SalesNo, Recv.SalesDate AS Date, Recv.DeliveryDate as [Delivery Date], vwCOADetail.detail_title as CustomerName, " &
                   "V.SalesOrderNo, deliverychalanmastertable.DeliveryNo As [DC NO] , deliverychalanmastertable.DcDate, Recv.SalesQty, Recv.SalesAmount, ISNULL(Recv.Delivered,0) as Delivered, Recv.SalesId,  Recv.CustomerCode, " &
                   "tbldefEmployee.Employee_Name, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId, Recv.BiltyNo, " &
                   "vwCOADetail.StateName As Province , vwCOADetail.CityName As City ,SalesInvoiceMastertable.Username as [Billed By], Recv.Username As [Printed By], " &
                   "Recv.DispatchBy , Recv.Cartons , Recv.Checking , Recv.CheckingLoading As [Checking Loading] , V.UpdateUserName as [Last Updated By] , Recv.FuelExpense as [Potage Fuel Billed] , Recv.PoastageActual As [Poastage Actual], " &
                   "isnull(Recv.TransporterId ,0) as TransporterId , tbldeftransporter.TransporterName as Transporter , Recv.Remarks " &
                   "FROM SalesMasterTable Recv " &
                   "INNER JOIN vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id " &
                   "LEFT OUTER JOIN tblDefEmployee ON Recv.EmployeeCode = tblDefEmployee.Employee_Id " &
                   "LEFT OUTER JOIN SalesOrderMasterTable V ON Recv.POId = V.SalesOrderId " &
                   "Left Outer Join tbldeftransporter On Recv.TransporterId = tbldeftransporter.TransporterId " &
                   "Left Outer Join SalesInvoiceMastertable On Recv.SalesId = SalesInvoiceMastertable.SalesId " &
                   "Left Outer Join deliverychalanmastertable On Recv.DeliveryChalanId = deliverychalanmastertable.DeliveryId" &
                    " WHERE Recv.SalesNo Is Not Null " & IIf(PreviouseRecordShow = True, "", " And (Convert(varchar, Recv.SalesDate,102)  > Convert(Datetime, '" & ClosingDate & "', 102))") & ""
            If Me.rdbDelivered.Checked = True Then
                str += " AND ISNULL(Recv.Delivered,0)=1"
            ElseIf rdbUnDelivered.Checked = True Then
                str += " AND ISNULL(Recv.Delivered,0)=0"
            ElseIf rdbBoth.Checked = True Then
                str += " AND ISNULL(Recv.Delivered,0) in (1,0,NULL,'') "
            End If
            str += " ORDER BY Recv.SalesNo DESC"
            FillGridEx(grdSaved, str, True)
            'grdSaved.RetrieveStructure()
            grdSaved.RootTable.Columns("CashPaid").Visible = False
            'grdSaved.RootTable.Columns(4).Visible = False
            grdSaved.RootTable.Columns("SalesId").Visible = False
            grdSaved.RootTable.Columns("CustomerCode").Visible = False
            grdSaved.RootTable.Columns("EmployeeCode").Visible = False
            grdSaved.RootTable.Columns("PoId").Visible = False
            grdSaved.RootTable.Columns("TransporterId").Visible = False
            grdSaved.RootTable.Columns("BiltyNo").Visible = True
            grdSaved.RootTable.Columns("DcDate").Visible = False

            grdSaved.RootTable.Columns("SalesNo").Caption = "Issue No"
            grdSaved.RootTable.Columns("Date").Caption = "Date"
            grdSaved.RootTable.Columns("CustomerName").Caption = "Customer"
            grdSaved.RootTable.Columns("SalesOrderNo").Caption = "S-Order"
            grdSaved.RootTable.Columns("SalesQty").Caption = "Qty"
            grdSaved.RootTable.Columns("SalesAmount").Caption = "Amount"
            grdSaved.RootTable.Columns("Employee_Name").Caption = "Employee"

            grdSaved.RootTable.Columns("SalesNo").Width = 100
            grdSaved.RootTable.Columns("Date").Width = 100
            grdSaved.RootTable.Columns("CustomerName").Width = 150
            grdSaved.RootTable.Columns("SalesQty").Width = 50
            grdSaved.RootTable.Columns("SalesAmount").Width = 80
            grdSaved.RootTable.Columns("CustomerCode").Width = 100
            grdSaved.RootTable.Columns("Remarks").Width = 150

            grdSaved.RootTable.Columns("SalesNo").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("Date").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("Delivery Date").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("CustomerName").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("SalesOrderNo").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("DC NO").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("DcDate").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("SalesQty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("SalesAmount").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("Delivered").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("SalesId").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("CustomerCode").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("Employee_Name").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("CashPaid").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("EmployeeCode").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("PoId").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("Province").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("City").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("Billed By").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("Printed By").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("Last Updated By").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("Potage Fuel Billed").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("BiltyNo").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("TransporterId").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("Transporter").EditType = Janus.Windows.GridEX.EditType.NoEdit

            grdSaved.RootTable.Columns("Remarks").EditType = Janus.Windows.GridEX.EditType.TextBox
            grdSaved.RootTable.Columns("Poastage Actual").EditType = Janus.Windows.GridEX.EditType.TextBox
            grdSaved.RootTable.Columns("Cartons").EditType = Janus.Windows.GridEX.EditType.TextBox
            grdSaved.RootTable.Columns("Checking").EditType = Janus.Windows.GridEX.EditType.TextBox
            grdSaved.RootTable.Columns("Checking Loading").EditType = Janus.Windows.GridEX.EditType.TextBox
            grdSaved.RootTable.Columns("DispatchBy").EditType = Janus.Windows.GridEX.EditType.TextBox


            'grdSaved.RowHeadersVisible = False
            GridBarUserControl1_Load(Nothing, Nothing)
            GetSecurityRights()
            Me.grdSaved.RootTable.Columns("Date").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("Delivery Date").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("DcDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("Potage Fuel Billed").FormatString = DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grdSaved_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        'Me.grdSaved.CurrentRow.Selected = True
        Me.IsFormOpend = True
        Try
            Me.EditRecord()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Sub EditRecord()
        Try

            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            Me.uitxtBiltyNo.Text = grdSaved.CurrentRow.Cells("BiltyNo").Value & ""
            Me.cmbTransporter.SelectedValue = Me.grdSaved.CurrentRow.Cells("TransporterId").Value
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("Delivery Date").Value) Then
                Me.dtpDeliveryDate.Value = Me.grdSaved.GetRow.Cells("Delivery Date").Value
            Else
                Me.dtpDeliveryDate.Value = Date.Now
            End If
            Me.GetSecurityRights()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If FormValidate() Then

                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                If Update_Record() Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
                    'msg_Information(str_informUpdate)
                    DisplayRecord()
                Else
                    Exit Sub 'MsgBox("Record has not been updated")
                End If
            End If

        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function FormValidate() As Boolean
        If Not Me.grdSaved.RowCount > 0 Then
            Return False
        End If
        Return True
    End Function

    Private Function Update_Record() As Boolean

        Me.grdSaved.UpdateData()

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer = 0

        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'Before against ReqId-934
            'objCommand.CommandText = "Update SalesMasterTable set TransporterId=" & Me.cmbTransporter.SelectedValue & ",BiltyNo='" & Me.uitxtBiltyNo.Text & "', DeliveryDate='" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Delivered=" & IIf(Me.chkDelivered.Checked = True, 1, 0) & " Where SalesID= " & Me.grdSaved.CurrentRow.Cells("SalesId").Value & " "
            'ReqId-934 Resolve comma error
            objCommand.CommandText = "Update SalesMasterTable set TransporterId=" & Me.cmbTransporter.SelectedValue & ", DispatchBy='" & Me.grdSaved.CurrentRow.Cells("DispatchBy").Value & "' , CheckingLoading='" & Me.grdSaved.CurrentRow.Cells("Checking Loading").Value & "' , Checking='" & Me.grdSaved.CurrentRow.Cells("Checking").Value & "' , Cartons='" & Me.grdSaved.CurrentRow.Cells("Cartons").Value & "' , PoastageActual='" & Me.grdSaved.CurrentRow.Cells("Poastage Actual").Value & "' , Remarks='" & Me.grdSaved.CurrentRow.Cells("Remarks").Value & "' , BiltyNo='" & Me.uitxtBiltyNo.Text.Replace("'", "''") & "', DeliveryDate='" & Me.dtpDeliveryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Delivered=" & IIf(Me.chkDelivered.Checked = True, 1, 0) & " Where SalesID= " & Me.grdSaved.CurrentRow.Cells("SalesId").Value & " "

            objCommand.ExecuteNonQuery()

            Me.uitxtBiltyNo.Text = String.Empty
            trans.Commit()
            Update_Record = True

            SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Sales, Me.grdSaved.CurrentRow.Cells("SalesNo").Value, True)
            Me.GetSecurityRights()
            'insertvoucher()
        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try
    End Function

    Private Sub grdSaved_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                'Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmUpdatebitlyAndTransporter)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        'Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                'Me.BtnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As SBModel.GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        '    Me.BtnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GridBarUserControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 25-1-14
      
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub grdSaved_SelectionChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.SelectionChanged
        Try

            Try
                If Me.IsFormOpend = False Then Exit Sub
                Me.EditRecord()
            Catch ex As Exception
                msg_Error(ex.Message)
            End Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim id As Integer = 0
            id = Me.cmbTransporter.SelectedValue
            FillCombo("Transporter")
            Me.cmbTransporter.SelectedValue = id
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdbUnDelivered_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbUnDelivered.CheckedChanged, rdbBoth.CheckedChanged, rdbDelivered.CheckedChanged
        Try
            If IsFormOpend = True Then DisplayRecord()
        Catch ex As Exception
            ShowErrorMessage("" & ex.Message)
        End Try
    End Sub

    
End Class