Imports System.Windows.Forms
Imports SBDal
Imports SBModel
Imports System.Data.OleDb

Public Class frmSearchSalesInquiry
    Public flag As Boolean = False
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Dim UnCheckRows() As Janus.Windows.GridEX.GridEXRow = Me.grdItems.GetUncheckedRows
            Dim CheckRows() As Janus.Windows.GridEX.GridEXRow = Me.grdItems.GetCheckedRows
            If CheckRows.Length <= 0 Then
                msg_Error("At least one row is required. Please select rows")
            Else
                If flag = True Then
                    For Each ROW As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetCheckedRows
                        'frmPurchaseInquiry.ReSetControls()
                        frmQoutationNew.AddToGridFromSalesInquiry(ROW)
                    Next
                    flag = False
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    If frmPurchaseInquiry.grdItems.DataSource Is Nothing Then
                        frmPurchaseInquiry.DisplayDetail(-1)
                    End If
                    For Each ROW As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetCheckedRows
                        'frmPurchaseInquiry.ReSetControls()
                        frmPurchaseInquiry.AddToGridFromSalesInquiry(ROW)
                    Next
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                End If
                
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
      
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Public Function GetAllRecords() As DataTable
        Try
            'SalesInquiryNo, SalesInquiryDate, CustomerId, LocationId, ContactPersonId, CustomerInquiryNo, IndentNo, IndentDepartment, CustomerInquiryDate, OldInquiryNo, DueDate, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT SalesInquiry.SalesInquiryId, SalesInquiry.SalesInquiryNo, SalesInquiry.SalesInquiryDate, IsNull(COA.coa_detail_id,0) as CustomerId, COA.detail_code As Code, COA.detail_title As Customer, SalesInquiry.LocationId, SalesInquiry.ContactPersonId, SalesInquiry.CustomerInquiryNo, SalesInquiry.IndentNo, SalesInquiry.IndentDepartment, SalesInquiry.CustomerInquiryDate, SalesInquiry.OldInquiryNo, SalesInquiry.DueDate, SalesInquiry.OldInquiryDate, SalesInquiry.Remarks FROM dbo.SalesInquiryMaster AS SalesInquiry LEFT OUTER JOIN  dbo.vwCOADetail AS COA ON SalesInquiry.CustomerId = COA.coa_detail_id ")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub FillCombos(ByVal Condition As String)
        Dim strQuery As String = String.Empty
        Try
            If Condition = "Customer" Then
                strQuery = String.Empty
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    strQuery = "SELECT Distinct IsNull(SalesInquiryMaster.CustomerId,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail Inner Join SalesInquiryMaster ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId WHERE vwCOADetail.account_type in('Vendor', 'Customer') and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbReference, strQuery)
                    Me.cmbReference.Rows(0).Activate()
                    Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Else
                    strQuery = "SELECT Distinct IsNull(SalesInquiryMaster.CustomerId,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail Inner Join SalesInquiryMaster ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId WHERE vwCOADetail.account_type = 'Customer' and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbReference, strQuery)
                    Me.cmbReference.Rows(0).Activate()
                    Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                End If
            ElseIf Condition = "InquiryNumber" Then
                'strQuery = "Select SalesInquiryId, SalesInquiryNo, SalesInquiryDate From SalesInquiryMaster Where SalesInquiryId In(Select SalesInquiryDetail.SalesInquiryId From SalesInquiryDetail INNER JOIN SalesInquiryMaster ON SalesInquiryDetail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId GROUP BY SalesInquiryDetail.SalesInquiryId Having Sum(IsNull(SalesInquiryDetail.Qty, 0)-IsNull(SalesInquiryDetail.PurchasedQty, 0))>0) Order By SalesInquiryDate DESC"
                strQuery = "Select SalesInquiryId, SalesInquiryNo, SalesInquiryDate From SalesInquiryMaster WHERE SalesInquiryId > 0 " & IIf(Me.dtpInquiryFromDate.Checked = True, " AND CONVERT(VARCHAR, SalesInquiryDate, 102) >= CONVERT(DATETIME, N'" & dtpInquiryFromDate.Value.ToString("yyyy-M-dd 00:00:00") & "', 102) ", "") & " " & IIf(Me.dtpInquiryToDate.Checked = True, " AND CONVERT(VARCHAR, SalesInquiryDate, 102) <= CONVERT(DATETIME, N'" & dtpInquiryToDate.Value.ToString("yyyy-M-dd 23:59:59") & "', 102) ", "") & " Order By SalesInquiryDate DESC"
                FillUltraDropDown(Me.cmbInquiryNumber, strQuery)
                Me.cmbInquiryNumber.Rows(0).Activate()
                Me.cmbInquiryNumber.DisplayLayout.Bands(0).Columns("SalesInquiryId").Hidden = True
                Me.cmbInquiryNumber.DisplayMember = Me.cmbInquiryNumber.Rows(0).Cells(1).Column.Key.ToString
            ElseIf Condition = "InquiryNumberAgainstCustomer" Then
                'strQuery = "Select SalesInquiryId, SalesInquiryNo, SalesInquiryDate From SalesInquiryMaster Where SalesInquiryId In(Select SalesInquiryDetail.SalesInquiryId From SalesInquiryDetail INNER JOIN SalesInquiryMaster ON SalesInquiryDetail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId Where SalesInquiryMaster.CustomerId =" & Me.cmbReference.Value & " GROUP BY SalesInquiryDetail.SalesInquiryId Having Sum(IsNull(SalesInquiryDetail.Qty, 0)-IsNull(SalesInquiryDetail.PurchasedQty, 0))>0) Order By SalesInquiryNo DESC"
                strQuery = "Select SalesInquiryId, SalesInquiryNo, SalesInquiryDate From SalesInquiryMaster Where SalesInquiryMaster.CustomerId =" & Me.cmbReference.Value & " " & IIf(Me.dtpInquiryFromDate.Checked = True, " AND CONVERT(VARCHAR, SalesInquiryDate, 102) >= CONVERT(DATETIME, N'" & dtpInquiryFromDate.Value.ToString("yyyy-M-dd 00:00:00") & "', 102) ", "") & " " & IIf(Me.dtpInquiryToDate.Checked = True, " AND CONVERT(VARCHAR, SalesInquiryDate, 102) <= CONVERT(DATETIME, N'" & dtpInquiryToDate.Value.ToString("yyyy-M-dd 23:59:59") & "', 102) ", "") & "  Order By SalesInquiryDate DESC"
                FillUltraDropDown(Me.cmbInquiryNumber, strQuery)
                Me.cmbInquiryNumber.Rows(0).Activate()
                Me.cmbInquiryNumber.DisplayLayout.Bands(0).Columns("SalesInquiryId").Hidden = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            DisplayDetail()
            For c As Integer = 0 To Me.grdItems.RootTable.Columns.Count - 1
                grdItems.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.TextBox
                grdItems.RootTable.Columns(c).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub DisplayDetail()
        Try
            Dim IsAdmin As Boolean = False
            Dim GroupId As Integer = 0
            Dim UserId As Integer = 0
            'If LoginGroup = "Administrator" Then
            '    IsAdmin = True
            'Else
            GroupId = LoginGroupId
            UserId = LoginUserId
            'End If
            Dim CustomerId As Integer = 0
            Dim FromDate As New DateTime
            Dim ToDate As New DateTime
            Dim SalesInquiryId As Integer = 0
            If Me.cmbReference.Value > 0 Then
                CustomerId = Me.cmbReference.Value
            End If
            If dtpInquiryFromDate.Checked = True Then
                FromDate = dtpInquiryFromDate.Value
            Else
                FromDate = DateTime.MinValue
            End If
            If dtpInquiryToDate.Checked = True Then
                ToDate = dtpInquiryToDate.Value
            Else
                ToDate = DateTime.MinValue
            End If
            If Me.cmbInquiryNumber.Value > 0 Then
                SalesInquiryId = Me.cmbInquiryNumber.Value
            End If
            Dim SalesInquiryDetailDAL As New SalesInquiryDetailDAL()
            Dim dt As New DataTable
            'Dim dv As New DataView
            'Dim dv1 As New DataView
            If Not Me.grdItems.DataSource Is Nothing Then
                dt = CType(Me.grdItems.DataSource, DataTable)
            End If
            'Dim dt1 As DataTable = SalesInquiryDetailDAL.GetAgainstSearch(GroupId, CustomerId, SalesInquiryId, FromDate, ToDate, IsAdmin)
            Dim dt1 As DataTable = SalesInquiryDetailDAL.GetAgainstSearch1(UserId, CustomerId, SalesInquiryId, FromDate, ToDate, IsAdmin)
            'For Each dr As DataRow In dt.Rows
            '    For Each dr1 As DataRow In dt1.Rows
            '        If dr.Item("SalesInquiryDetailId") = dr1.Item("SalesInquiryDetailId") Then
            '            dr1.BeginEdit()
            '            dr1.Delete()
            '            dr1.EndEdit()
            '        End If
            '    Next
            'Next
            dt1.AcceptChanges()
            dt.Merge(dt1)
            dt.AcceptChanges()
            Me.grdItems.DataSource = dt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmSearchSalesInquiry_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FillCombos("Customer")
            FillCombos("InquiryNumber")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItems.ColumnButtonClick
        Try
            If e.Column.Key = "Remove" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                Me.grdItems.GetRow.Delete()
                Me.grdItems.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmSearchSalesInquiry_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Me.grdItems.DataSource = Nothing
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbReference_ValueChanged(sender As Object, e As EventArgs) Handles cmbReference.ValueChanged
        Try
            If Me.cmbReference.Value > 0 Then
                FillCombos("InquiryNumberAgainstCustomer")
            Else
                FillCombos("InquiryNumber")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbInquiryNumber_Enter(sender As Object, e As EventArgs) Handles cmbInquiryNumber.Enter
        Me.cmbInquiryNumber.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub

    Private Sub dtpInquiryFromDate_Leave(sender As Object, e As EventArgs) Handles dtpInquiryFromDate.Leave
        ''TASK TFS4873
        If Me.dtpInquiryFromDate.Checked = True Or Me.dtpInquiryToDate.Checked = True Then
            If Me.cmbReference.Value > 0 Then
                FillCombos("InquiryNumberAgainstCustomer")
            Else
                FillCombos("InquiryNumber")
            End If
        End If
    End Sub

    Private Sub dtpInquiryToDate_Leave(sender As Object, e As EventArgs) Handles dtpInquiryToDate.Leave
        Try
            ''TASK TFS4873
            If Me.dtpInquiryFromDate.Checked = True Or Me.dtpInquiryToDate.Checked = True Then
                If Me.cmbReference.Value > 0 Then
                    FillCombos("InquiryNumberAgainstCustomer")
                Else
                    FillCombos("InquiryNumber")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
