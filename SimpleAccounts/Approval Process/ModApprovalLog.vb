Imports SBModel
Imports SBDal
Imports System
Imports System.Data
Imports System.Data.SqlClient
Module ModApprovalLog

#Region "Approval Log Entry"
    Dim ApprovalHistory As ApprovalHistoryBE
    Dim ApprovalHistoryDetail As ApprovalHistoryDetailBE
    Dim ApprovalHistoryDetailList As List(Of ApprovalHistoryDetailBE)
    Dim ApprovalUsersGroup As ApprovalUsersGroupBE
    Dim ApprovalUsersGroupList As List(Of ApprovalUsersGroupBE)
    Dim ObjDAL As ApprovalHistoryDAL = New ApprovalHistoryDAL()
    Dim dt As DataTable
    Enum EnumApprovalMasterStatus
        InProgress
        Approved
        AutoApproved
        Rejected
    End Enum
    Enum EnumApprovalDetailStatus
        None
        WaitingForApproval
        Approved
        Rejected
    End Enum

    Enum EnumReferenceType
        NA
        Configuration
        SalesInquiry
        Sales
        SalesQuotation
        SalesOrder
        SalesReturn
        DeliveryChallan
        SalesCertificate
        SalesAdjVoucher
        SalesInvoiceTransfer
        Purchase
        PurchaseDemand
        PurchaseOrder
        PurchaseReturn
        GRN
        VendorQuotation
        PurchaseInquiry
        AccountTransaction
        StoreIssuence
        Security
        StockDispatch
        StockReceive
        Budget
        Report
        Lab
        ActivityFeedBack
        VoucherEntry
        Payment
        Receipt
        Expense
        CashRequest
        EmployeeLoanRequest
    End Enum

    Public Sub SaveApprovalLog(ByVal ReferenceType As EnumReferenceType, _
                               ByVal ReferenceId As Integer, _
                               ByVal DocumentNo As String, _
                               ByVal DocumentDate As DateTime, _
                               ByVal Description As String, _
                               Optional ByVal Source As String = " ", _
                               Optional ByVal voucher_type_id As Integer = 0, _
                               Optional ByRef ErrorText As String = "")

        Try

            Dim i As Integer
            ApprovalHistory = New ApprovalHistoryBE
            ApprovalHistory.AprovalProcessId = getApprovalProcessId(ReferenceType.ToString)
            ApprovalHistory.DocumentDate = DocumentDate
            ApprovalHistory.DocumentNo = DocumentNo
            ApprovalHistory.ReferenceType = ReferenceType.ToString
            ApprovalHistory.ReferenceId = ReferenceId
            ApprovalHistory.Description = Description
            ApprovalHistory.voucher_type_id = voucher_type_id
            ApprovalHistory.Source = Source
            ApprovalHistory.Status = IIf(ApprovalHistory.AprovalProcessId = 0, EnumApprovalMasterStatus.AutoApproved.ToString, EnumApprovalMasterStatus.InProgress.ToString)
            ApprovalHistory.LogUserID = LoginUserId
            ApprovalHistory.CurrentStage = "First"

            ApprovalHistoryDetailList = New List(Of ApprovalHistoryDetailBE)
            For i = 1 To getLevels()
                ApprovalHistoryDetail = New ApprovalHistoryDetailBE
                ApprovalHistoryDetail.ApprovalUserId = 0
                ApprovalHistoryDetail.Level = i
                ApprovalHistoryDetail.StageId = getStageId(i)
                ApprovalHistoryDetail.Remarks = Description
                If i = 1 Then
                    ApprovalHistoryDetail.Status = EnumApprovalDetailStatus.WaitingForApproval.ToString
                Else
                    ApprovalHistoryDetail.Status = EnumApprovalDetailStatus.None.ToString
                End If
                ApprovalHistoryDetailList.Add(ApprovalHistoryDetail)
            Next

            'If dt.Rows.Count > 0 Then
            '    ApprovalHistoryDetail.ApprovalUsersGroupList = New List(Of ApprovalUsersGroupBE)
            '    For i = 0 To dt.Rows.Count - 1
            '        ApprovalUsersGroup = New ApprovalUsersGroupBE
            '        ApprovalUsersGroup.GroupId = Val(dt.Rows(i).Item(0).ToString)
            '    Next
            'End If

            ApprovalHistory.ApprovalHistoryDetailList = ApprovalHistoryDetailList

            ''This is the Call To DAL of Approval History,Approval History Master And Approval History Detail Is Added To It
            If Not ObjDAL.Add(ApprovalHistory) Then
                ShowErrorMessage("The Approval Process Does not Mapped with the Current Document")
            End If

        Catch ex As Exception
            Throw ex

        Finally

        End Try
    End Sub
    Public Function getApprovalProcessId(ByVal Reference As String) As Integer
        Dim ApprovalProcessId As Integer = 0
        If Reference = EnumReferenceType.Purchase.ToString Then
            If getConfigValueByType("PurchaseApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("PurchaseApproval")
            End If

        ElseIf Reference = EnumReferenceType.PurchaseDemand.ToString Then
            If getConfigValueByType("PurchaseDemandApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("PurchaseDemandApproval")
            End If
        ElseIf Reference = EnumReferenceType.PurchaseOrder.ToString Then
            If getConfigValueByType("PurchaseOrderApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("PurchaseOrderApproval")
            End If
        ElseIf Reference = EnumReferenceType.PurchaseReturn.ToString Then
            If getConfigValueByType("PurchaseReturnApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("PurchaseReturnApproval")
            End If
        ElseIf Reference = EnumReferenceType.PurchaseInquiry.ToString Then
            If getConfigValueByType("PurchaseInquiryApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("PurchaseInquiryApproval")
            End If
        ElseIf Reference = EnumReferenceType.GRN.ToString Then
            If getConfigValueByType("GRNApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("GRNApproval")
            End If
        ElseIf Reference = EnumReferenceType.VendorQuotation.ToString Then
            If getConfigValueByType("VendorQuotationApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("VendorQuotationApproval")
            End If
        ElseIf Reference = EnumReferenceType.ActivityFeedBack.ToString Then
            If getConfigValueByType("ActivityFeedBackApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("ActivityFeedBackApproval")
            End If
        ElseIf Reference = EnumReferenceType.VoucherEntry.ToString Then
            If getConfigValueByType("VoucherEntryApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("VoucherEntryApproval")
            End If
        ElseIf Reference = EnumReferenceType.Payment.ToString Then
            If getConfigValueByType("PaymentApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("PaymentApproval")
            End If
        ElseIf Reference = EnumReferenceType.Receipt.ToString Then
            If getConfigValueByType("ReceiptApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("ReceiptApproval")
            End If
        ElseIf Reference = EnumReferenceType.Expense.ToString Then
            If getConfigValueByType("ExpenseApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("ExpenseApproval")
            End If
            ''Start TFS3113
        ElseIf Reference = EnumReferenceType.SalesInquiry.ToString Then
            If getConfigValueByType("SalesInquiryApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("SalesInquiryApproval")
            End If
        ElseIf Reference = EnumReferenceType.Sales.ToString Then
            If getConfigValueByType("SalesInvoiceApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("SalesInvoiceApproval")
            End If
        ElseIf Reference = EnumReferenceType.SalesQuotation.ToString Then
            If getConfigValueByType("SalesQuotationApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("SalesQuotationApproval")
            End If
        ElseIf Reference = EnumReferenceType.SalesOrder.ToString Then
            If getConfigValueByType("SalesOrderApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("SalesOrderApproval")
            End If
        ElseIf Reference = EnumReferenceType.SalesReturn.ToString Then
            If getConfigValueByType("SalesReturnApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("SalesReturnApproval")
            End If
        ElseIf Reference = EnumReferenceType.DeliveryChallan.ToString Then
            If getConfigValueByType("DeliveryChallanApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("DeliveryChallanApproval")
            End If
        ElseIf Reference = EnumReferenceType.EmployeeLoanRequest.ToString Then
            If getConfigValueByType("EmployeeLoanRequestApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("EmployeeLoanRequestApproval")
            End If
        ElseIf Reference = EnumReferenceType.CashRequest.ToString Then
            If getConfigValueByType("CashRequestApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("CashRequestApproval")
            End If
        ElseIf Reference = EnumReferenceType.SalesInvoiceTransfer.ToString Then
            If getConfigValueByType("SalesInvoiceTranferApproval") = "Error" Then
                ApprovalProcessId = 0
            Else
                ApprovalProcessId = getConfigValueByType("SalesInvoiceTranferApproval")
            End If
        End If
        ''End TFS3113
        Return ApprovalProcessId
    End Function
    Public Function getLevels() As Integer
        Dim NoOfLevels As Integer = 0
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction

            str = "Select COUNT(*) from ApprovalStagesMappping where ApprovalProcessId = " & ApprovalHistory.AprovalProcessId & " And ApprovalStagesId > 0"
            NoOfLevels = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            trans.Commit()
            Return NoOfLevels
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    'Public Function getGroupId(ByVal Level As Integer) As Integer
    '    Dim GroupId As Integer = 0
    '    Dim str As String = ""
    '    Dim conn As New SqlConnection(SQLHelper.CON_STR)
    '    Dim trans As SqlTransaction
    '    Try
    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If
    '        trans = conn.BeginTransaction

    '        str = "SELECT ApprovalUsersMapping.GroupId FROM  ApprovalStagesMappping INNER JOIN " _
    '             & "ApprovalUsersMapping ON ApprovalStagesMappping.ApprovalStagesMapppingId = ApprovalUsersMapping.ApprovalStagesMapppingId" _
    '             & " WHERE ApprovalStagesMappping.ApprovalProcessId = " & ApprovalHistory.AprovalProcessId & " And ApprovalStagesMappping.Level = " & Level
    '        GroupId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
    '        trans.Commit()
    '        Return GroupId
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw ex
    '    Finally
    '        conn.Close()
    '    End Try
    'End Function
    'Public Function getGroupId(ByVal Level As Integer) As DataTable

    '    Dim str As String = ""
    '    Try
    '        str = "SELECT ApprovalUsersMapping.GroupId FROM  ApprovalStagesMappping INNER JOIN " _
    '             & "ApprovalUsersMapping ON ApprovalStagesMappping.ApprovalStagesMapppingId = ApprovalUsersMapping.ApprovalStagesMapppingId" _
    '             & " WHERE ApprovalStagesMappping.ApprovalProcessId = " & ApprovalHistory.AprovalProcessId & " And ApprovalStagesMappping.Level = " & Level
    '        dt = GetDataTable(str)
    '        Return dt
    '    Catch ex As Exception

    '        Throw ex
    '    End Try
    'End Function

    Public Function getStageId(ByVal Level As Integer) As Integer
        Dim StageId As Integer = 0
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction

            str = "SELECT ApprovalStagesMappping.ApprovalStagesId FROM  ApprovalStagesMappping " _
                 & " WHERE ApprovalStagesMappping.ApprovalProcessId = " & ApprovalHistory.AprovalProcessId & " And ApprovalStagesMappping.Level = " & Level
            StageId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            trans.Commit()
            Return StageId
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
#End Region
End Module
