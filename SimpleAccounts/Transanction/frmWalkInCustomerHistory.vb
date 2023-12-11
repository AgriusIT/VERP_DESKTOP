Imports System.Data.OleDb
Public Class frmWalkInCustomerHistory

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.grd.DataSource = Nothing
            Me.detailRdo.Checked = True
            Me.fromDate.Checked = False
            Me.toDate.Checked = False
            'Ali Faisal : Top Search apllied to increase the speed of screen opening.
            Me.txtNo.Text = ""
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBarWalkInHistory.mGridChooseFielder.Enabled = True
                CtrlGrdBarWalkInHistory.mGridExport.Enabled = True
                CtrlGrdBarWalkInHistory.mGridPrint.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            CtrlGrdBarWalkInHistory.mGridChooseFielder.Enabled = False
            CtrlGrdBarWalkInHistory.mGridExport.Enabled = False
            CtrlGrdBarWalkInHistory.mGridPrint.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "Print" Then
                    CtrlGrdBarWalkInHistory.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Export" Then
                    CtrlGrdBarWalkInHistory.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    CtrlGrdBarWalkInHistory.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmWalkInCustomerHistory_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                'Ali Faisal : Top Search apllied to increase the speed of screen opening.
                Me.txtNo.Text = ""
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmWalkInCustomerHistory_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()

            Dim str As String = String.Empty

            If Me.detailRdo.Checked = True Then

                If Me.fromDate.Checked = True AndAlso Me.toDate.Checked = True Then

                    'Ali Faisal : Top Search apllied to increase the speed of screen opening.
                    If frmPOSEntry.txtMobile.Focused Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SaleDetailId, SalesDetailTable.SalesId, SalesDetailTable.ArticleDefId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, ArticleDefView.ArticleDescription, Sz7 as Qty, Price, case when isNull(DiscountId,1) = 1 then 'Percentage' Else 'Flat' End as DiscountType , DiscountFactor, (IsNull(Sz7, 0) * IsNull(Price, 0)) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where MobileNo = '" & frmPOSEntry.txtMobile.Text & "') and SalesMasterTable.SalesDate between '" & Me.fromDate.Value.ToString("yyyy-M-d 00:00:00") & "' and '" & Me.toDate.Value.ToString("yyyy-M-d 23:59:59") & "'"
                    ElseIf frmPOSEntry.txtCustomer.Focused Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SaleDetailId, SalesDetailTable.SalesId, SalesDetailTable.ArticleDefId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, ArticleDefView.ArticleDescription, Sz7 as Qty, Price, case when isNull(DiscountId,1) = 1 then 'Percentage' Else 'Flat' End as DiscountType, DiscountFactor,  (IsNull(Sz7, 0) * IsNull(Price, 0)) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where CustomerName = '" & frmPOSEntry.txtCustomer.Text & "')and SalesMasterTable.SalesDate between '" & Me.fromDate.Value.ToString("yyyy-M-d 00:00:00") & "' and '" & Me.toDate.Value.ToString("yyyy-M-d 23:59:59") & "'"
                    ElseIf frmPOSEntry.txtCNIC.Focused Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SaleDetailId, SalesDetailTable.SalesId, SalesDetailTable.ArticleDefId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, ArticleDefView.ArticleDescription, Sz7 as Qty, Price, case when isNull(DiscountId,1) = 1 then 'Percentage' Else 'Flat' End as DiscountType, DiscountFactor,  (IsNull(Sz7, 0) * IsNull(Price, 0)) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where CNIC = '" & frmPOSEntry.txtCNIC.Text & "') and SalesMasterTable.SalesDate between '" & Me.fromDate.Value.ToString("yyyy-M-d 00:00:00") & "' and '" & Me.toDate.Value.ToString("yyyy-M-d 23:59:59") & "'"
                    End If
                    If frmPOSEntry.txtMobile.Text.Length = 0 Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SaleDetailId, SalesDetailTable.SalesId, SalesDetailTable.ArticleDefId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, SalesMasterTable.CustomerName, SalesMasterTable.MobileNo, ArticleDefView.ArticleDescription, Sz7 as Qty, Price, case when isNull(DiscountId,1) = 1 then 'Percentage' Else 'Flat' End as DiscountType, DiscountFactor, (IsNull(Sz7, 0) * IsNull(Price, 0)) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where CustomerName <> '') and SalesMasterTable.SalesDate between '" & Me.fromDate.Value.ToString("yyyy-M-d 00:00:00") & "' and '" & Me.toDate.Value.ToString("yyyy-M-d 23:59:59") & "'"
                    End If

                Else

                    'Ali Faisal : Top Search apllied to increase the speed of screen opening.
                    If frmPOSEntry.txtMobile.Focused Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SaleDetailId, SalesDetailTable.SalesId, SalesDetailTable.ArticleDefId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, ArticleDefView.ArticleDescription, Sz7 as Qty, Price, case when isNull(DiscountId,1) = 1 then 'Percentage' Else 'Flat' End as DiscountType, DiscountFactor, (IsNull(Sz7, 0) * IsNull(Price, 0)) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where MobileNo = '" & frmPOSEntry.txtMobile.Text & "') "
                    ElseIf frmPOSEntry.txtCustomer.Focused Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SaleDetailId, SalesDetailTable.SalesId, SalesDetailTable.ArticleDefId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, ArticleDefView.ArticleDescription, Sz7 as Qty, Price, case when isNull(DiscountId,1) = 1 then 'Percentage' Else 'Flat' End as DiscountType, DiscountFactor,  (IsNull(Sz7, 0) * IsNull(Price, 0)) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where CustomerName = '" & frmPOSEntry.txtCustomer.Text & "') "
                    ElseIf frmPOSEntry.txtCNIC.Focused Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SaleDetailId, SalesDetailTable.SalesId, SalesDetailTable.ArticleDefId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, ArticleDefView.ArticleDescription, Sz7 as Qty, Price, case when isNull(DiscountId,1) = 1 then 'Percentage' Else 'Flat' End as DiscountType, DiscountFactor,  (IsNull(Sz7, 0) * IsNull(Price, 0)) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where CNIC = '" & frmPOSEntry.txtCNIC.Text & "') "
                    End If
                    If frmPOSEntry.txtMobile.Text.Length = 0 Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SaleDetailId, SalesDetailTable.SalesId, SalesDetailTable.ArticleDefId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, SalesMasterTable.CustomerName, SalesMasterTable.MobileNo, ArticleDefView.ArticleDescription, Sz7 as Qty, Price, case when isNull(DiscountId,1) = 1 then 'Percentage' Else 'Flat' End as DiscountType, DiscountFactor, (IsNull(Sz7, 0) * IsNull(Price, 0)) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where CustomerName <> '') "
                    End If

                End If

                Dim dt As DataTable
                dt = GetDataTable(str)
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                Me.grd.RootTable.Columns("SaleDetailId").Visible = False
                Me.grd.RootTable.Columns("SalesId").Visible = False
                Me.grd.RootTable.Columns("ArticleDefId").Visible = False
                Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("TotalAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.AutoSizeColumns()

            ElseIf Me.summaryRdo.Checked = True Then

                If Me.fromDate.Checked = True AndAlso Me.toDate.Checked = True Then

                    'Ali Faisal : Top Search apllied to increase the speed of screen opening.
                    If frmPOSEntry.txtMobile.Focused Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SalesDetailTable.SalesId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, sum(isNull(Sz7,0)) as Qty, sum((IsNull(Sz7, 0) * IsNull(Price, 0))) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where MobileNo = '" & frmPOSEntry.txtMobile.Text & "') and SalesMasterTable.SalesDate between '" & Me.fromDate.Value.ToString("yyyy-M-d 00:00:00") & "' and '" & Me.toDate.Value.ToString("yyyy-M-d 23:59:59") & "' group by SalesDetailTable.SalesId , SalesMasterTable.SalesNo, SalesMasterTable.SalesDate"
                    ElseIf frmPOSEntry.txtCustomer.Focused Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SalesDetailTable.SalesId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, sum(isNull(Sz7,0)) as Qty, sum((IsNull(Sz7, 0) * IsNull(Price, 0))) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where CustomerName = '" & frmPOSEntry.txtCustomer.Text & "')and SalesMasterTable.SalesDate between '" & Me.fromDate.Value.ToString("yyyy-M-d 00:00:00") & "' and '" & Me.toDate.Value.ToString("yyyy-M-d 23:59:59") & "' group by SalesDetailTable.SalesId , SalesMasterTable.SalesNo, SalesMasterTable.SalesDate"
                    ElseIf frmPOSEntry.txtCNIC.Focused Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SalesDetailTable.SalesId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, sum(isNull(Sz7,0)) as Qty, sum((IsNull(Sz7, 0) * IsNull(Price, 0))) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where CNIC = '" & frmPOSEntry.txtCNIC.Text & "') and SalesMasterTable.SalesDate between '" & Me.fromDate.Value.ToString("yyyy-M-d 00:00:00") & "' and '" & Me.toDate.Value.ToString("yyyy-M-d 23:59:59") & "' group by SalesDetailTable.SalesId , SalesMasterTable.SalesNo, SalesMasterTable.SalesDate"
                    End If
                    If frmPOSEntry.txtMobile.Text.Length = 0 Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SalesDetailTable.SalesId , SalesMasterTable.SalesNo, SalesMasterTable.SalesDate,SalesMasterTable.CustomerName, SalesMasterTable.MobileNo, sum(isNull(Sz7,0)) as Qty, sum((IsNull(Sz7, 0) * IsNull(Price, 0))) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where CustomerName <> '') and SalesMasterTable.SalesDate between '" & Me.fromDate.Value.ToString("yyyy-M-d 00:00:00") & "' and '" & Me.toDate.Value.ToString("yyyy-M-d 23:59:59") & "' group by SalesDetailTable.SalesId , SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, SalesMasterTable.CustomerName, SalesMasterTable.MobileNo"
                    End If

                Else

                    'Ali Faisal : Top Search apllied to increase the speed of screen opening.
                    If frmPOSEntry.txtMobile.Focused Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SalesDetailTable.SalesId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, sum(isNull(Sz7,0)) as Qty, sum((IsNull(Sz7, 0) * IsNull(Price, 0))) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where MobileNo = '" & frmPOSEntry.txtMobile.Text & "') group by SalesDetailTable.SalesId , SalesMasterTable.SalesNo, SalesMasterTable.SalesDate"
                    ElseIf frmPOSEntry.txtCustomer.Focused Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SalesDetailTable.SalesId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, sum(isNull(Sz7,0)) as Qty, sum((IsNull(Sz7, 0) * IsNull(Price, 0))) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where CustomerName = '" & frmPOSEntry.txtCustomer.Text & "') group by SalesDetailTable.SalesId , SalesMasterTable.SalesNo, SalesMasterTable.SalesDate"
                    ElseIf frmPOSEntry.txtCNIC.Focused Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SalesDetailTable.SalesId, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, sum(isNull(Sz7,0)) as Qty, sum((IsNull(Sz7, 0) * IsNull(Price, 0))) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where CNIC = '" & frmPOSEntry.txtCNIC.Text & "') group by SalesDetailTable.SalesId , SalesMasterTable.SalesNo, SalesMasterTable.SalesDate"
                    End If
                    If frmPOSEntry.txtMobile.Text.Length = 0 Then
                        str = "Select Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SalesDetailTable.SalesId , SalesMasterTable.SalesNo, SalesMasterTable.SalesDate,SalesMasterTable.CustomerName, SalesMasterTable.MobileNo, sum(isNull(Sz7,0)) as Qty, sum((IsNull(Sz7, 0) * IsNull(Price, 0))) as TotalAmount from SalesDetailTable INNER JOIN SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.salesid in (Select SalesId from SalesMasterTable where CustomerName <> '') group by SalesDetailTable.SalesId , SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, SalesMasterTable.CustomerName, SalesMasterTable.MobileNo"
                    End If

                End If

                Dim dt As DataTable
                dt = GetDataTable(str)
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                'Me.grd.RootTable.Columns("SaleDetailId").Visible = False
                Me.grd.RootTable.Columns("SalesId").Visible = False
                Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("TotalAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                'Me.grd.RootTable.Columns("ArticleDefId").Visible = False
                Me.grd.AutoSizeColumns()

            End If

            Me.CtrlGrdBarWalkInHistory_Load(Nothing, Nothing)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Dim cm As New OleDbCommand
            If Con.State = ConnectionState.Closed Then Con.Open()
            cm.Connection = Con

            cm.CommandText = "DELETE from tblVoucherdetail where voucher_id in (SELECT voucher_id FROM tblVoucher WHERE voucher_code = '" & grd.CurrentRow.Cells("SalesNo").Value.ToString & "')"
            cm.ExecuteNonQuery()
            cm.CommandText = "DELETE from tblVoucher where voucher_id in (SELECT voucher_id FROM tblVoucher WHERE voucher_code = '" & grd.CurrentRow.Cells("SalesNo").Value.ToString & "')"
            cm.ExecuteNonQuery()
            cm.CommandText = "DELETE from SalesDetailTable where SalesId = " & Val(grd.CurrentRow.Cells("SalesId").Value.ToString) & ""
            cm.ExecuteNonQuery()
            cm.CommandText = "DELETE from SalesMasterTable where SalesId = " & Val(grd.CurrentRow.Cells("SalesId").Value.ToString) & ""
            cm.ExecuteNonQuery()
            cm.CommandText = "DELETE from StockDetailTable where StockTransId in (Select StockTransId from StockMasterTable Where DocNo = '" & grd.CurrentRow.Cells("SalesNo").Value.ToString & "')"
            cm.ExecuteNonQuery()
            cm.CommandText = "DELETE from StockMasterTable where DocNo = '" & grd.CurrentRow.Cells("SalesNo").Value.ToString & "'"
            cm.ExecuteNonQuery()
            Dim str1 As String
            Dim dt1 As DataTable
            str1 = "SELECT SalesDetailId FROM tblBardanaAdjustment where SalesDetailId = " & Val(grd.CurrentRow.Cells("SalesId").Value.ToString) & ""
            dt1 = GetDataTable(str1)
            Dim SalesId As Integer
            If dt1.Rows.Count > 0 Then
                SalesId = dt1.Rows(0).Item("SalesDetailId")
            End If
            cm.CommandText = "DELETE from tblBardanaAdjustment where SalesDetailId = " & SalesId & ""
            cm.ExecuteNonQuery()
            msg_Information(str_informDelete)
            frmWalkInCustomerHistory_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub loadBtn_Click(sender As Object, e As EventArgs) Handles loadBtn.Click
        Try
            frmWalkInCustomerHistory_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBarWalkInHistory_Load(sender As Object, e As EventArgs) Handles CtrlGrdBarWalkInHistory.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then

                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()

            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBarWalkInHistory.txtGridTitle.Text = CompanyTitle & Chr(10) & "Delivery Chalan"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub detailRdo_CheckedChanged(sender As Object, e As EventArgs) Handles detailRdo.CheckedChanged, summaryRdo.CheckedChanged
        Try
            frmWalkInCustomerHistory_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class