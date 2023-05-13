Imports SBDal
Imports SBModel
Public Class frmProductionOrderList
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    Public Sub GetAll()
        Try
            'Dim DAL As New ProductionOrderDAL    
            Me.grdProductionOrder.DataSource = Nothing
            Me.grdProductionOrder.DataSource = New ProductionOrderDAL().GetAll()
            'Me.grdProductionOrder.RetrieveStructure()
            'Me.grdProductionOrder.RootTable.Columns("FinishGoodId").Visible = False
            'Me.grdProductionOrder.RootTable.Columns("ProductId").Visible = False
            'Me.grdProductionOrder.RootTable.Columns("ProductionOrderId").Visible = False
            'Me.grdProductionOrder.RootTable.Columns("ProductionOrderId").Visible = False
            Me.grdProductionOrder.RootTable.Columns("ProductionOrderDate").FormatString = str_DisplayDateFormat
            Me.grdProductionOrder.RootTable.Columns("ExpiryDate").FormatString = str_DisplayDateFormat
            'Me.grdProductionOrder.RootTable.Columns("CGSAccountId").Visible = False
            'ProductionOrderDate()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click, btnAddDock.Click
        Try
            'Me.Panel1.BackColor = Color.Blue
            'Me.Label2.ForeColor = Color.White
            Dim frm As New frmProductionOrder(False, , DoHaveSaveRights)
            frm.ShowDialog()
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProductionOrderList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GetAll()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdProductionOrder_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdProductionOrder.RowDoubleClick
        'ProductionOrder.ProductionOrderId, ProductionOrder.ProductionOrderNo, ProductionOrder.ProductionOrderDate, ProductionOrder.BatchNo, ProductionOrder.ExpiryDate, ProductionOrder.ProductId, ArticleDefTable.ArticleDescription, ProductionOrder.FinishGoodId, FinishGoodMaster.StandardName,  ProductionOrder.BatchSize, ProductionOrder.Section, ProductionOrder.Remarks, Convert(bit, IsNull(ProductionOrder.Approved, 0)) AS Approved
        Try
            If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim POrder As New ProductionOrderBE
                POrder.ProductionOrderId = Me.grdProductionOrder.GetRow.Cells("ProductionOrderId").Value
                POrder.ProductionOrderNo = Me.grdProductionOrder.GetRow.Cells("ProductionOrderNo").Value.ToString
                POrder.TicketNo = Me.grdProductionOrder.GetRow.Cells("TicketNo").Value.ToString
                POrder.ProductionOrderDate = Me.grdProductionOrder.GetRow.Cells("ProductionOrderDate").Value
                POrder.BatchNo = Me.grdProductionOrder.GetRow.Cells("BatchNo").Value.ToString
                POrder.ExpiryDate = Me.grdProductionOrder.GetRow.Cells("ExpiryDate").Value
                POrder.ProductId = Me.grdProductionOrder.GetRow.Cells("ProductId").Value
                POrder.FinishGoodId = Me.grdProductionOrder.GetRow.Cells("FinishGoodId").Value
                POrder.BatchSize = Me.grdProductionOrder.GetRow.Cells("BatchSize").Value
                POrder.Section = Me.grdProductionOrder.GetRow.Cells("Section").Value.ToString
                POrder.Remarks = Me.grdProductionOrder.GetRow.Cells("Remarks").Value.ToString
                POrder.CGSAccountId = Val(Me.grdProductionOrder.GetRow.Cells("CGSAccountId").Value.ToString)
                POrder.Approved = Me.grdProductionOrder.GetRow.Cells("Approved").Value
                POrder.TotalQuantity = Val(Me.grdProductionOrder.GetRow.Cells("TotalQuantity").Value.ToString)
                'Task 3240 get DispatchId and Production_ID from ProductionGrid
                POrder.DispatchId = Val(Me.grdProductionOrder.GetRow.Cells("DispatchId").Value.ToString)
                POrder.Production_Id = Val(Me.grdProductionOrder.GetRow.Cells("Production_ID").Value.ToString)
                Dim frm As New frmProductionOrder(True, POrder, , DoHaveUpdateRights)
                frm.ShowDialog()
                GetAll()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdProductionOrder_KeyPress(sender As Object, e As KeyPressEventArgs)
        'Try
        '    If e.Key = Keys.Enter Then

        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub grdProductionOrder_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            'ProductionOrder.ProductionOrderId, ProductionOrder.ProductionOrderNo, ProductionOrder.ProductionOrderDate, ProductionOrder.BatchNo, ProductionOrder.ExpiryDate, ProductionOrder.ProductId, ArticleDefTable.ArticleDescription, ProductionOrder.FinishGoodId, FinishGoodMaster.StandardName,  ProductionOrder.BatchSize, ProductionOrder.Section, ProductionOrder.Remarks, Convert(bit, IsNull(ProductionOrder.Approved, 0)) AS Approved
            If e.KeyCode = Keys.Delete Then
                If DoHaveDeleteRights = True Then
                    If msg_Confirm("Do you want to delete selected row?") = False Then Exit Sub
                    If Me.grdProductionOrder.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                        Dim Obj As New ProductionOrderBE
                        Obj.ProductionOrderId = Me.grdProductionOrder.GetRow.Cells("ProductionOrderId").Value
                        Obj.ProductionOrderNo = Me.grdProductionOrder.GetRow.Cells("ProductionOrderNo").Value
                        Obj.Voucher.VoucherNo = Obj.ProductionOrderNo
                        Obj.Voucher.VNo = Obj.ProductionOrderNo
                        Obj.Voucher.VoucherCode = Obj.ProductionOrderNo
                        If ProductionOrderDAL.Delete(Obj) = True Then
                            ShowErrorMessage("Record has been deleted successfully.")
                            GetAll()
                        End If
                    End If
                Else
                    ShowErrorMessage("You do not have rights to delete.")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs)
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdProductionOrder.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdProductionOrder.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdProductionOrder.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdProductionOrder.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdProductionOrder.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdProductionOrder.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                DoHaveSaveRights = True
                DoHaveUpdateRights = True
                DoHaveDeleteRights = True
                Me.CtrlGrdBar3.mGridPrint.Enabled = True
                Me.CtrlGrdBar3.mGridExport.Enabled = True
                Me.CtrlGrdBar3.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    DoHaveSaveRights = False
                    DoHaveUpdateRights = False
                    DoHaveDeleteRights = False
                    Me.CtrlGrdBar3.mGridPrint.Enabled = False
                    Me.CtrlGrdBar3.mGridExport.Enabled = False
                    Me.CtrlGrdBar3.mGridChooseFielder.Enabled = False
                    Exit Sub
                End If
            Else
                DoHaveSaveRights = False
                DoHaveUpdateRights = False
                DoHaveDeleteRights = False
                Me.CtrlGrdBar3.mGridPrint.Enabled = False
                Me.CtrlGrdBar3.mGridExport.Enabled = False
                Me.CtrlGrdBar3.mGridChooseFielder.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar3.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar3.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar3.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        DoHaveSaveRights = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        DoHaveUpdateRights = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        DoHaveDeleteRights = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdProductionOrder_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdProductionOrder.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If DoHaveDeleteRights = True Then
                    If msg_Confirm("Do you want to delete selected row?") = False Then Exit Sub
                    If Me.grdProductionOrder.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                        Dim Obj As New ProductionOrderBE
                        Obj.ProductionOrderId = Val(Me.grdProductionOrder.GetRow.Cells("ProductionOrderId").Value.ToString)
                        Obj.ProductionOrderNo = Me.grdProductionOrder.GetRow.Cells("ProductionOrderNo").Value.ToString
                        Obj.Voucher.VoucherNo = Obj.ProductionOrderNo
                        Obj.Voucher.VNo = Obj.ProductionOrderNo
                        Obj.Voucher.VoucherCode = Obj.ProductionOrderNo
                        Obj.Voucher.ActivityLog = New ActivityLog()

                        'Task 3420 get Dispatchid and StoreIssuanceID TO DELETE FROM their tables'

                        Dim DispatchID As Integer = Val(Me.grdProductionOrder.GetRow.Cells("DispatchId").Value.ToString)
                        Dim ProductionID = Val(Me.grdProductionOrder.GetRow.Cells("Production_ID").Value.ToString)

                        If ProductionOrderDAL.DeleteProductionEntryAndStoreIssuance(DispatchID, ProductionID) = True Then

                            If ProductionOrderDAL.Delete(Obj) = True Then
                                msg_Information("Record has been deleted successfully.")
                                GetAll()
                            End If

                        End If
                    End If
                    GetAll()
                Else
                    msg_Information("You do not have rights to delete.")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class