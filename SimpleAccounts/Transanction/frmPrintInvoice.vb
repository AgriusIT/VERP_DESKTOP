Imports System.Windows.Forms

Public Class frmPrintInvoice

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If Me.grdSaved.RowCount = 0 Then Exit Sub
        If Not Me.IsValidate() Then Exit Sub
        Me.Hide()
        Me.BackgroundWorker1.RunWorkerAsync()
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DisplayRecord(Optional ByVal Condition As String = "")
        Dim str As String
        'str = "SELECT     Recv.SalesNo, CONVERT(varchar, Recv.SalesDate, 103) AS Date, V.CustomerName, Recv.SalesQty, Recv.SalesAmount, " _
        '       & " Recv.SalesId, Recv.CustomerCode, Recv.Remarks, convert(varchar, Recv.CashPaid) as CashPaid FROM         dbo.SalesMasterTable Recv INNER JOIN dbo.tblCustomer V ON Recv.CustomerCode = V.AccountId"

        str = "SELECT " & IIf(Condition = "All", "", "Top 50") & " Recv.SalesNo, Recv.SalesDate AS Date, vwCOADetail.detail_title as CustomerName, V.SalesOrderNo, Recv.SalesQty, Recv.SalesAmount, Recv.SalesId,  " & _
                "Recv.CustomerCode, tbldefEmployee.Employee_Name, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.EmployeeCode, Recv.PoId, Recv.BiltyNo,isnull(Recv.TransporterId ,0) as TransporterId " & _
                "FROM SalesMasterTable Recv INNER JOIN " & _
                "vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN " & _
                "tblDefEmployee ON Recv.EmployeeCode = tblDefEmployee.Employee_Id LEFT OUTER JOIN " & _
                "SalesOrderMasterTable V ON Recv.POId = V.SalesOrderId " & _
                " Where 1 = 1 "
        If Me.dtpFromDate.Checked Then
            str = str & " AND Recv.SalesDate >= '" & Me.dtpFromDate.Value.ToString("dd/MMM/yyyy") & "'"
        End If

        If Me.dtpToDate.Checked Then
            str = str & " AND Recv.SalesDate <= '" & Me.dtpToDate.Value.ToString("dd/MMM/yyyy") & "'"
        End If

        If Me.cmbFromInvoice.SelectedIndex > 0 Then
            str = str & " AND Recv.SalesId >= " & Me.cmbFromInvoice.SelectedValue & ""
        End If

        If Me.cmbToInvoice.SelectedIndex > 0 Then
            str = str & " AND Recv.SalesId <= " & Me.cmbToInvoice.SelectedValue & ""
        End If
        str = str & "ORDER BY Recv.SalesNo DESC"

        FillGridEx(grdSaved, str, False)
        grdSaved.RootTable.Columns(11).Visible = False
        'grdSaved.RootTable.Columns(4).Visible = False
        grdSaved.RootTable.Columns(7).Visible = False
        grdSaved.RootTable.Columns(8).Visible = False
        grdSaved.RootTable.Columns("EmployeeCode").Visible = False
        grdSaved.RootTable.Columns("PoId").Visible = False
        grdSaved.RootTable.Columns("TransporterId").Visible = False
        grdSaved.RootTable.Columns("BiltyNo").Visible = False

        grdSaved.RootTable.Columns(1).Caption = "Issue No"
        grdSaved.RootTable.Columns(2).Caption = "Date"
        grdSaved.RootTable.Columns(3).Caption = "Customer"
        grdSaved.RootTable.Columns(4).Caption = "S-Order"
        grdSaved.RootTable.Columns(5).Caption = "Qty"
        grdSaved.RootTable.Columns(6).Caption = "Amount"
        grdSaved.RootTable.Columns(9).Caption = "Employee"

        grdSaved.RootTable.Columns(1).Width = 100
        grdSaved.RootTable.Columns(2).Width = 100
        grdSaved.RootTable.Columns(3).Width = 150
        grdSaved.RootTable.Columns(5).Width = 50
        grdSaved.RootTable.Columns(6).Width = 80
        grdSaved.RootTable.Columns(9).Width = 100
        grdSaved.RootTable.Columns(10).Width = 150
        grdSaved.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.False
        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdSaved.RootTable.Columns
            col.AutoSize()
        Next
        Me.grdSaved.RootTable.Columns(0).EditType = Janus.Windows.GridEX.EditType.NoEdit
        Me.grdSaved.RootTable.Columns("Date").FormatString = str_DisplayDateFormat
    End Sub


    Private Sub frmPrintInvoice_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Me.FillCombo()
        Me.RefreshControls()
        Me.DisplayRecord()
        Me.lblProgress.Visible = False
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub frmPrintInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub

    Private Sub FillCombo()
        Dim Str As String = "Select SalesID, SalesNo from SalesMasterTable Order By SalesNo"
        FillDropDown(Me.cmbToInvoice, Str)
        FillDropDown(Me.cmbFromInvoice, Str)
    End Sub

    Private Sub RefreshControls()
        Me.dtpFromDate.Value = Date.Today.AddDays(-30)
        Me.dtpToDate.Value = Date.Today
        Me.dtpFromDate.Checked = False
        Me.dtpToDate.Checked = False
        Me.cmbFromInvoice.SelectedIndex = 0
        Me.cmbToInvoice.SelectedIndex = 0
    End Sub
    Private Sub btnSearh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearh.Click
        'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Me.DisplayRecord()
        Me.chkAll_CheckedChanged(sender, e)
        Me.lblProgress.Visible = False
    End Sub
    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        If Not Me.grdSaved.RowCount > 0 Then Exit Sub
        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
            r.Cells(0).Value = Me.chkAll.Checked
        Next
    End Sub

    Private Function IsValidate() As Boolean
        If Me.dtpFromDate.Checked AndAlso Me.dtpToDate.Checked Then
            If Me.dtpFromDate.Value > Me.dtpToDate.Value Then
                msg_Error("From Date Can't be greate than To Date")
                Me.dtpFromDate.Focus()
                Return False
            End If
        End If
        Dim intIsChecked As Integer = 0
        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
            If r.Cells(0).Value = True Then
                intIsChecked = 1
                Exit For
            End If
        Next

        If intIsChecked = 0 Then
            msg_Error("You must select at least one record from grid")
            Me.grdSaved.Focus()
            Return False
        End If
        Return True
    End Function
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If Me.grdSaved.RowCount = 0 Then Exit Sub
        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
            If r.Cells(0).Value = True Then
                ShowReport("SalesInvoice", "{SalesMasterTable.SalesId}=" & r.Cells("SalesId").Value, "Nothing", "Nothing", True)
            End If
        Next
    End Sub

    Private Sub btnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
        Try
            DisplayRecord("All")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    
End Class
