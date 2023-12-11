Public Class frmOrderStatusChange

    Public _CustomerId As Integer = 0I
    Public _OrderId As Integer = 0I
    Public _OrderDate As DateTime
    Public _SalePerson As Integer = 0I
    Public _Adjustment As Double = 0D
    Public _Adj_Flag As Boolean = False
    Public _Adjustment_Percentage As Double = 0D
    Public _IsOpenForm As Boolean = False

    Private Sub frmOrderStatusChange_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.Yellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Me.dtpOrderDate.Value = Date.Now
            FillDropDown(Me.cmbRootPlan, "Select RootPlanId, RootPlanName From tblDefRootPlan")
            FillDropDown(Me.cmbSalesPerson, "Select Employee_Id, Employee_Name From tblDefEmployee WHERE isnull(SalePerson,0)=1")
            _IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub frmOrderStatusChange_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub
    Sub FillGrid()
        'Me.Cursor = Cursors.WaitCursor
        Try
            If Me.cmbRootPlan.SelectedIndex = -1 Then Exit Sub
            If Me.cmbRootPlan.SelectedIndex = 0 Then
                ShowErrorMessage("Please select root plan")
                Me.cmbRootPlan.Focus()
                Exit Sub
            End If
            If Me.cmbSalesPerson.SelectedIndex = -1 Then Exit Sub
            If Me.cmbSalesPerson.SelectedIndex = 0 Then
                ShowErrorMessage("Please select sale person")
                Me.cmbRootPlan.Focus()
                Exit Sub
            End If
            Dim strSQL As String = " SELECT SalesOrderMasterTable.VendorId, dbo.SalesOrderMasterTable.SalesOrderId, 0 as Employee_Code, dbo.SalesOrderMasterTable.SalesOrderNo, " _
                 & " dbo.SalesOrderMasterTable.SalesOrderDate, dbo.vwCOADetail.detail_title, dbo.SalesOrderMasterTable.SalesOrderQty,  " _
                 & " dbo.SalesOrderMasterTable.SalesOrderAmount,  isnull(SalesOrderMasterTable.Adjustment,0) as Adjustment, isnull(SalesOrderMasterTable.Adj_Flag,0) as Adj_Flag, isnull(SalesOrderMasterTable.SpecialAdjustment,0) as Adjustment_Percentage  " _
                 & " FROM dbo.SalesOrderMasterTable INNER JOIN " _
                 & " dbo.vwCOADetail ON dbo.SalesOrderMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                 & " dbo.tblDefEmployee ON dbo.SalesOrderMasterTable.SOP_ID = dbo.tblDefEmployee.Employee_ID " _
                 & " WHERE (dbo.SalesOrderMasterTable.Status = N'Open') AND (Convert(DateTime, Convert(varchar, LEFT(dbo.SalesOrderMasterTable.SalesOrderDate,11), 102),102) = Convert(DateTime, '" & Me.dtpOrderDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND SalesOrderMasterTable.VendorId in (Select AccountId From tblCustomer Where RootPlanId =" & Me.cmbRootPlan.SelectedValue & ") "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            If dt IsNot Nothing Then
                Me.grdLetterOfCreadit.DataSource = Nothing
                Me.grdLetterOfCreadit.DataSource = dt
                Dim dtEmpData As New DataTable
                dtEmpData = GetDataTable("Select Employee_Id, Employee_Name From tblDefEmployee")
                grdLetterOfCreadit.RootTable.Columns("Employee_Code").ValueList.PopulateValueList(dtEmpData.DefaultView, "Employee_Id", "Employee_Name")


                If grdLetterOfCreadit.RowCount = 0 Then Exit Sub
                Dim dtData As DataTable = CType(Me.grdLetterOfCreadit.DataSource, DataTable)
                For Each r As DataRow In dtData.Rows
                    r.BeginEdit()
                    r("Employee_Code") = Me.cmbSalesPerson.SelectedValue
                    r.EndEdit()
                Next




                For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grdLetterOfCreadit.RootTable.Columns
                    If c.Index <> 2 Then
                        c.EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Next

            End If

          

        Catch ex As Exception
            Throw ex
        Finally
            'Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Me.ToolStripProgressBar1.Visible = True
        Try

            FillGrid()
            cmbSalesPerson_SelectedIndexChanged(Nothing, Nothing)
            Me.ToolStripProgressBar1.Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub btnGenerateInvoices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateInvoices.Click
        Me.ToolStripProgressBar1.Visible = True
        Try
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdLetterOfCreadit.GetCheckedRows
                _CustomerId = 0I
                _OrderId = 0I
                _OrderDate = Date.Now
                _CustomerId = r.Cells("VendorId").Value
                _OrderId = r.Cells("SalesOrderId").Value
                _OrderDate = r.Cells("SalesOrderDate").Value
                _SalePerson = r.Cells("Employee_Code").Value
                _Adjustment = r.Cells("Adjustment").Value
                _Adj_Flag = r.Cells("Adj_Flag").Value
                _Adjustment_Percentage = r.Cells("Adjustment_Percentage").Value
                If frmSales.BtnSave.Text = "Update" Then
                    frmSales.RefreshControls()
                End If


                frmSales.flgAutoInvoiceGenerate = True
                frmSales.cmbVendor.Value = _CustomerId
                frmSales.cmbPo.SelectedValue = _OrderId
                frmSales.cmbSalesMan.SelectedValue = _SalePerson


                If _Adj_Flag = False Then
                    frmSales.txtAdjustment.Text = _Adjustment
                    frmSales.rbtAdjFlat.Checked = True
                Else
                    frmSales.txtAdjustment.Text = _Adjustment_Percentage
                    frmSales.rbtAdjPercentage.Checked = True
                End If

                frmSales.cmbPo_SelectedIndexChanged(Nothing, Nothing)
                frmSales.SaveToolStripButton_Click(Nothing, Nothing)
            Next
            Me.ToolStripProgressBar1.Visible = False
            DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            frmSales.flgAutoInvoiceGenerate = False
        End Try
    End Sub
    Private Sub cmbSalesPerson_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSalesPerson.SelectedIndexChanged
        Try
            If Not _IsOpenForm = True Then Exit Sub
            If Me.grdLetterOfCreadit.RowCount = 0 Then Exit Sub
            Dim dtData As DataTable = CType(Me.grdLetterOfCreadit.DataSource, DataTable)
            For Each r As DataRow In dtData.Rows
                r.BeginEdit()
                r("Employee_Code") = Me.cmbSalesPerson.SelectedValue
                r.EndEdit()
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    
End Class