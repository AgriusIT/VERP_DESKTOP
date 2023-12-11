Imports SBDal
Imports SBModel

Public Class frmBulkTicketsCreation

    Dim Query As String = String.Empty
    Dim PlanDetail As List(Of PlanDetailTableBE)
    Dim Plan As PlanMasterTableBE
    Private Sub FillSalesOrderCombo()
        Try
            Query = "SELECT SalesOrderId, SalesOrderNo, IsNull(VendorId, 0) AS CustomerId, IsNull(LocationId, 0) AS LocationId FROM SalesOrderMasterTable ORDER BY SalesOrderDate DESC "
            FillDropDown(Me.cmbSalesOrder, Query)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillProductCombo(ByVal SalesOrderId As Integer)
        Try
            Query = " SELECT Article.ArticleId, Article.ArticleCode AS Code, Article.ArticleDescription AS Product, (IsNull(Detail.Qty, 0)-IsNull(Detail.TicketQty, 0)) AS Qty FROM ArticleDefTable AS Article INNER JOIN ArticleDefTableMaster AS MasterArticle " _
                & " ON Article.MasterID = MasterArticle.ArticleId " _
                & " INNER JOIN SalesOrderDetailTable AS Detail ON Article.ArticleId=Detail.ArticleDefId Where Detail.SalesOrderId= " & SalesOrderId & ""
            '  Query = " SELECT MasterArticle.ArticleId, MasterArticle.ArticleDescription AS Product FROM ArticleDefTable AS Article INNER JOIN ArticleDefTableMaster AS MasterArticle " _
            '  & " ON Article.MasterID = MasterArticle.MasterID " _
            '  & " INNER JOIN SalesOrderDetailTable AS Detail ON Article.ArticleId=Detail.ArticleDefId Where Detail.SalesOrderId= " & SalesOrderId & ""
            FillUltraDropDown(Me.cmbProduct, Query)
            Me.cmbProduct.Rows(0).Activate()
            Me.cmbProduct.DisplayLayout.Bands(0).Columns("ArticleId").Hidden = True

            If rbByName.Checked = True Then
                Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells("Product").Column.Key.ToString
            Else
                Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells("Code").Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function Process() As Boolean
        Try
            If BulkTicketsCreationDAL.InsertPlan(Plan, LoginUserName, LoginUserId) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmBulkTicketsCreation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillSalesOrderCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSalesOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSalesOrder.SelectedIndexChanged
        Try
            If Not Me.cmbSalesOrder.SelectedIndex = -1 Then
                If Me.cmbSalesOrder.SelectedValue > 0 Then
                    PlanDetail = BulkTicketsCreationDAL.GetSODetails(Me.cmbSalesOrder.SelectedValue)
                End If
                FillProductCombo(Me.cmbSalesOrder.SelectedValue)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbProduct_ValueChanged(sender As Object, e As EventArgs) Handles cmbProduct.ValueChanged
        If Me.cmbProduct.ActiveRow IsNot Nothing Then
            Me.txtQuantity.Text = Val(Me.cmbProduct.ActiveRow.Cells("Qty").Value.ToString)
        End If
    End Sub

    Private Sub txtQuantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQuantity.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Try
            If IsValid() Then
                If Process() Then
                    msg_Information("Process has been completed successfully")
                    ResetControls()
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillModel()
        Try
            Plan = New PlanMasterTableBE()
            Plan.PlanNo = GetNextPlanNo()
            Plan.PlanDate = Now
            Plan.StartDate = Me.dtpStartDate.Value
            Plan.CustomerId = Val(CType(Me.cmbSalesOrder.SelectedItem, DataRowView).Item("CustomerId").ToString)
            Plan.LocationId = Val(CType(Me.cmbSalesOrder.SelectedItem, DataRowView).Item("LocationId").ToString)
            Plan.PoId = Me.cmbSalesOrder.SelectedValue
            Plan.UserName = LoginUserName
            Plan.Remarks = Me.txtRemarks.Text

            ''Activity Log
            Plan.ActivityLog = New ActivityLog()
            Plan.ActivityLog.ActivityName = "Save"
            Plan.ActivityLog.ApplicationName = "Production"
            Plan.ActivityLog.FormCaption = "Production Planning Standard"
            Plan.ActivityLog.FormName = "frmProductionPlanningStandard"
            Plan.ActivityLog.LogDateTime = Date.Now
            Plan.ActivityLog.LogComments = String.Empty
            Plan.ActivityLog.User_Name = LoginUserName
            Plan.ActivityLog.UserID = LoginUserId
            Plan.ActivityLog.Source = "frmProductionPlanningStandard"

            ''
            If Me.rbAllProducts.Checked = True Then
                Plan.Detail = PlanDetail
            ElseIf Me.rbSelectedProduct.Checked = True Then
                Dim _detail As PlanDetailTableBE = PlanDetail.Find(Function(d) d.ArticleDefId = Me.cmbProduct.Value)
                PlanDetail.Clear()
                PlanDetail.Add(_detail)
                Plan.Detail = PlanDetail
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetNextPlanNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("PN" + "-" + Microsoft.VisualBasic.Right(Now.Year, 2) + "-", "PlanMasterTable", "PlanNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("PN" & "-" & Format(Now, "yy") & Now.Month.ToString("00"), 4, "PlanMasterTable", "PlanNo")
            Else
                Return GetNextDocNo("PN", 6, "PlanMasterTable", "PlanNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsValid() As Boolean
        Try
            If Me.cmbSalesOrder.SelectedValue < 1 Then
                ShowErrorMessage("Sales order is required")
                Me.cmbSalesOrder.Focus()
                Return False
            End If
            If PlanDetail IsNot Nothing Then
                If PlanDetail.Count = 0 Then
                    ShowErrorMessage("Selected Sales Order do not have pending record.")
                    Return False
                End If
            Else
                ShowErrorMessage("Selected Sales Order do not have pending record.")
                Return False
            End If
            If Me.rbSelectedProduct.Checked = True Then
                If Me.cmbProduct.Value < 1 Then
                    ShowErrorMessage("Product is required")
                    Me.cmbProduct.Focus()
                    Return False
                End If
                If Val(Me.txtQuantity.Text) < 1 Then
                    ShowErrorMessage("Quantity should be greator than zero")
                    Me.txtQuantity.Focus()
                    Return False
                End If
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub ResetControls()
        Try
            If Not cmbSalesOrder.SelectedIndex = -1 Then
                Me.cmbSalesOrder.SelectedIndex = 0
            End If
            If Not cmbProduct.ActiveRow Is Nothing Then
                Me.cmbProduct.Rows(0).Activate()
            End If
            Me.dtpStartDate.Value = Now
            Me.txtRemarks.Text = String.Empty
            PlanDetail = Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Id As Integer = 0
        Try
            Id = Me.cmbSalesOrder.SelectedValue
            FillSalesOrderCombo()
            Me.cmbSalesOrder.SelectedValue = Id
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbByCode_CheckedChanged(sender As Object, e As EventArgs) Handles rbByCode.CheckedChanged
        Try
            If Me.cmbProduct.IsItemInList = False Then Exit Sub
            Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells("Code").Column.Key.ToString
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbByName_CheckedChanged(sender As Object, e As EventArgs) Handles rbByName.CheckedChanged
        If Me.cmbProduct.IsItemInList = False Then Exit Sub
        Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells("Product").Column.Key.ToString
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtQuantity.TextChanged
        Try
            If Not Me.cmbProduct.ActiveRow Is Nothing Then
                If Val(Me.txtQuantity.Text) > Val(Me.cmbProduct.ActiveRow.Cells("Qty").Value.ToString) Then
                    Me.txtQuantity.Text = Val(Me.cmbProduct.ActiveRow.Cells("Qty").Value.ToString)
                    ShowErrorMessage("Quantity should not be greator than available quantity.")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    
End Class