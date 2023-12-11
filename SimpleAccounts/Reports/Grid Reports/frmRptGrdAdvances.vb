Imports SBModel
Public Class frmRptGrdAdvances
    Private Sub rdoVendorAdvances_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoVendorAdvances.CheckedChanged, rdoCustomerAdvances.CheckedChanged
        Try
            If rdoVendorAdvances.Checked = True Then
                lblHeader.Text = "Vendor Advances"
                Me.CtrlGrdBar1.txtGridTitle.Text = "" & lblHeader.Text + " -- " + Date.Now
            Else
                lblHeader.Text = "Customer Advances"
                Me.CtrlGrdBar1.txtGridTitle.Text = "" & lblHeader.Text + " -- " + Date.Now
            End If
            GetAllRecords()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmRptGrdAdvances_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub GetAllRecords()
        Try
            Dim str As String = String.Empty
            str = "SP_Advances '" & IIf(Me.rdoVendorAdvances.Checked = True, "Vendor", "Customer") & "'"
            Dim dtData As New DataTable
            dtData = GetDataTable(str)
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dtData
            Me.grd.RetrieveStructure()
            grd.RootTable.Columns("detail_code").Caption = "Account Code"
            grd.RootTable.Columns("detail_title").Caption = "Account Description"
            grd.RootTable.Columns(2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grd.RootTable.Columns(2).FormatString = "N"
            grd.RootTable.Columns(2).TotalFormatString = "N"
            grd.RootTable.Columns(2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grd.RootTable.Columns(2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grd_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow

    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Vendor Advances"
        Catch ex As Exception

        End Try
    End Sub
End Class