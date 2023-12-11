''20-Feb-2018 TFS2375 : Ayesha Rehman : This Form Is Added to get the Approval Log For All Documents
''30-May-2018 TFS3432 : Ayesha Rehman :Remarks and Detail Title should be in multirow view in grid
''13-Sep-2018 TFS4431 : Ayesha Rehman :Configure the approval hierarchy of invoice transfer.
Imports SBModel
Imports SBDal
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Text
Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions
Public Class frmApprovalLog
    Dim ApprovalHistory As ApprovalHistoryBE
    Dim ApprovalHistoryDetail As ApprovalHistoryDetailBE
    Dim ApprovalHistoryDetailList As List(Of ApprovalHistoryDetailBE)
    Dim ObjDAL As ApprovalHistoryDAL = New ApprovalHistoryDAL()
    Dim dtEmail As DataTable
    Dim EmailTemplate As String = String.Empty
    Dim AfterFieldsElement As String = String.Empty
    Dim AllFields As List(Of String)
    Dim EmailDAL As New EmailTemplateDAL
    Dim html As StringBuilder
    Dim EmailBody As String = String.Empty
    Dim UsersEmail As List(Of String)
    Dim VendorEmails As String = String.Empty
    Dim EmailNotification As Boolean = False
    Dim DocumentNo As String = String.Empty
    Dim AutoEmail As Boolean = False
    Enum enmgrd
        ApprovalHistoryId
        AprovalHistoryDetailId
        StageId
        Level
        ReferenceType
        ReferenceId
        DocumentNo
        DocumentDate
        voucher_type
        CustomerCode
        Qty
        Amount
        PartyName
        LocationId
        Attachments
        Description
        Title
        Source
    End Enum
    Private Sub FillCombos()

    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Me.Cursor = Cursors.WaitCursor
        Try

            Call ShowApprovalLog()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage("Error occured while showing Record: " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub frmApprovalLog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Call FillCombos()
            GetSecurityRights()
            ''TASK TFS4456
            If getConfigValueByType("EmailNotificationOnApproval").ToString = "True" Then
                EmailNotification = True
            Else
                EmailNotification = False
            End If
            If getConfigValueByType("AutoEmail").ToString = "True" Then
                AutoEmail = True
            Else
                AutoEmail = False
            End If
            ''END TASK TFS4456
            'Call ShowApprovalLog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ReSetControls(Optional ByVal Condition As String = "")
        Try
            Me.dtpFromDate.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpToDate.Value = Date.Now
            Me.cmbVtype.SelectedIndex = 0
            btnShow_Click(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ShowApprovalLog()
        Try

            Dim strAppliation As String = ""
            Dim strForm As String = ""
            Dim strApproval As String = ""
            Dim intUserID As String = 0
            Dim strRecordType As String = ""

            'If Me.cmbVtype.SelectedIndex = 0 Or Me.cmbVtype.SelectedIndex = -1 Then
            '    ShowErrorMessage("Select voucher type")
            '    Me.cmbVtype.Focus()
            '    Exit Sub
            'End If
            Dim Str As String
            ''Below lines are commented on 29-06-2018
            'Str = "SELECT ApprovalHistory.ApprovalHistoryId, ApprovalHistoryDetail.AprovalHistoryDetailId, ApprovalHistoryDetail.StageId , ApprovalHistoryDetail.[Level], ApprovalHistory.ReferenceType , ApprovalHistory.ReferenceId ,ApprovalHistory.DocumentNo, ApprovalHistory.DocumentDate, isNull(dbo.tblDefVoucherType.voucher_type , '') As voucher_type , Doc.CustomerCode ,Doc.Qty , Doc.Amount ,Doc.PartyName , Doc.LocationId," _
            '        & " isnull(Att.AttachmentCount,0) as Attachments, ApprovalHistory.Description, ApprovalStages.Title , ApprovalHistory.Source" _
            '        & " FROM ApprovalUsersGroup INNER JOIN ApprovalHistory INNER JOIN" _
            '        & " ApprovalHistoryDetail ON ApprovalHistory.ApprovalHistoryId = ApprovalHistoryDetail.AprovalHistoryId INNER JOIN" _
            '        & " ApprovalStages ON ApprovalHistoryDetail.StageId = ApprovalStages.ApprovalStagesId" _
            '        & "  ON ApprovalUsersGroup.ApprovalhistoryDetailId = ApprovalHistoryDetail.AprovalHistoryDetailId " _
            '        & " LEFT OUTER Join dbo.tblDefVoucherType ON dbo.ApprovalHistory.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id " _
            '        & " LEFT OUTER JOIN(Select Source,DocId, Count(*) as AttachmentCount From DocumentAttachment Group By DocId, Source) Att On Att.DocId = ApprovalHistory.ReferenceId and Att.Source = ApprovalHistory.Source " _
            '        & " LEFT OUTER JOIN(Select DISTINCT CustomerCode, SalesID as ID, SalesNo as DocNo , SalesQty As Qty ,SalesAmount As Amount ,detail_title As PartyName , LocationId From (" _
            '        & " Select SalesMasterTable.CustomerCode , vwCOADetail.detail_title , SalesMasterTable.LocationId ,SalesMasterTable.SalesId,SalesQty,SalesAmount , SalesNo  From SalesMasterTable Left Outer Join vwCOADetail on SalesMasterTable.CustomerCode = vwCOADetail.coa_detail_id " _
            '        & " Union " _
            '        & " Select PurchaseOrderMasterTable.VendorId ,vwCOADetail.detail_title , PurchaseOrderMasterTable.LocationId , PurchaseOrderMasterTable.PurchaseOrderId , PurchaseOrderQty , PurchaseOrderAmount , PurchaseorderNo  From PurchaseOrderMasterTable Left Outer Join vwCOADetail on PurchaseOrderMasterTable.VendorId  = vwCOADetail.coa_detail_id  " _
            '        & " Union " _
            '        & " Select ReceivingMasterTable.VendorId  , vwCOADetail.detail_title , ReceivingMasterTable.LocationId ,ReceivingMasterTable.ReceivingId , ReceivingQty , ReceivingAmount  , ReceivingNo  From ReceivingMasterTable Left Outer Join vwCOADetail on ReceivingMasterTable.VendorId  = vwCOADetail.coa_detail_id " _
            '        & " Union " _
            '        & " Select ReceivingNoteMasterTable.VendorId ,vwCOADetail.detail_title , ReceivingNoteMasterTable.LocationId , ReceivingNoteMasterTable.ReceivingNoteId , ReceivingQty , ReceivingAmount , ReceivingNo  From ReceivingNoteMasterTable Left Outer Join vwCOADetail on ReceivingNoteMasterTable.VendorId  = vwCOADetail.coa_detail_id " _
            '        & "Union " _
            '        & " Select SalesOrderMasterTable.VendorId , vwCOADetail.detail_title , SalesOrderMasterTable.LocationId ,SalesOrderMasterTable.SalesOrderId,SalesOrderQty,SalesOrderAmount , SalesOrderNo  From SalesOrderMasterTable Left Outer Join vwCOADetail on SalesOrderMasterTable.VendorId = vwCOADetail.coa_detail_id " _
            '        & " Union " _
            '        & " Select PurchaseReturnMasterTable.VendorId ,vwCOADetail.detail_title , PurchaseReturnMasterTable.LocationId , PurchaseReturnMasterTable.PurchaseReturnId , PurchaseReturnQty , PurchaseReturnAmount , PurchaseReturnNo  From PurchaseReturnMasterTable Left Outer Join vwCOADetail on PurchaseReturnMasterTable.VendorId  = vwCOADetail.coa_detail_id " _
            '        & " Union " _
            '        & " Select DeliveryChalanMasterTable.CustomerCode , vwCOADetail.detail_title , DeliveryChalanMasterTable.LocationId ,DeliveryChalanMasterTable.DeliveryId , DeliveryQty,DeliveryAmount  , DeliveryNo  From DeliveryChalanMasterTable Left Outer Join vwCOADetail on DeliveryChalanMasterTable.CustomerCode = vwCOADetail.coa_detail_id " _
            '        & " union " _
            '        & " Select SalesReturnMasterTable.CustomerCode , vwCOADetail.detail_title , SalesReturnMasterTable.LocationId ,SalesReturnMasterTable.SalesReturnId,SalesReturnQty,SalesReturnAmount , SalesReturnNo  From SalesReturnMasterTable Left Outer Join vwCOADetail on SalesReturnMasterTable.CustomerCode = vwCOADetail.coa_detail_id " _
            '        & " Union " _
            '        & " Select QuotationMasterTable.VendorId ,vwCOADetail.detail_title , QuotationMasterTable.LocationId , QuotationMasterTable.QuotationId , SalesOrderQty , SalesOrderAmount , QuotationNo  From QuotationMasterTable Left Outer Join vwCOADetail on QuotationMasterTable.VendorId  = vwCOADetail.coa_detail_id " _
            '        & " Union " _
            '        & "  Select CashRequestHead.EmpID , tblDefEmployee.Employee_Name , 0 As LocationId ,CashRequestHead.RequestId , 0 As Qty ,Total_Amount , RequestNo  From CashRequestHead Left Outer Join tblDefEmployee on CashRequestHead.EmpID  = tblDefEmployee.Employee_ID " _
            '        & " Union " _
            '        & "  Select AdvanceRequestTable.EmployeeID  ,EmployeesView.Employee_Name , 0 As LocationId , AdvanceRequestTable.RequestId , 0 as Qty , AdvanceAmount , RequestNo  From AdvanceRequestTable Left Outer Join EmployeesView on AdvanceRequestTable.EmployeeID   = EmployeesView.Employee_ID " _
            '        & " Union " _
            '        & " Select SalesInquiryMaster.CustomerId  , vwCOADetail.detail_title , SalesInquiryMaster.LocationId ,SalesInquiryMaster.SalesInquiryId, Sum(SalesInquiryDetail.Qty) , 0 As Amount , SalesInquiryNo  From SalesInquiryMaster Left Outer Join vwCOADetail on SalesInquiryMaster.CustomerId = vwCOADetail.coa_detail_id Inner Join SalesInquiryDetail on  SalesInquiryMaster.SalesInquiryId = SalesInquiryDetail.SalesInquiryId Group By SalesInquiryMaster.CustomerId ,  vwCOADetail.detail_title , SalesInquiryMaster.LocationId ,SalesInquiryMaster.SalesInquiryId , SalesInquiryNo " _
            '        & "  Union " _
            '        & " Select 0 As CustomerId  , [dbo].[Detailtitle] (PurchaseInquiryMaster.PurchaseInquiryId) As DetailTitle, " _
            '        & " 0 as LocationId ,PurchaseInquiryMaster.PurchaseInquiryId, Sum(PurchaseInquiryDetail.Qty) As Qty , 0 As Amount , PurchaseInquiryNo  From PurchaseInquiryMaster " _
            '        & " Inner Join PurchaseInquiryDetail on  PurchaseInquiryMaster.PurchaseInquiryId = PurchaseInquiryDetail.PurchaseInquiryId " _
            '        & " Group By PurchaseInquiryMaster.PurchaseInquiryId , PurchaseInquiryNo " _
            '        & " Union " _
            '        & " Select VendorQuotationMaster.VendorId ,vwCOADetail.detail_title , 0 as LocationId , VendorQuotationMaster.VendorQuotationId  , Sum(VendorQuotationDetail.Qty) As Qty , VendorQuotationMaster.NetTotal as Amount , VendorQuotationNo  From VendorQuotationMaster Left Outer Join vwCOADetail on VendorQuotationMaster.VendorId  = vwCOADetail.coa_detail_id  Inner Join VendorQuotationDetail on  VendorQuotationMaster.VendorQuotationId = VendorQuotationDetail.VendorQuotationId Group By VendorQuotationMaster.VendorId ,  vwCOADetail.detail_title ,VendorQuotationMaster.VendorQuotationId , VendorQuotationNo , VendorQuotationMaster.NetTotal " _
            '        & " Union " _
            '        & " Select PurchaseDemandMasterTable.VendorId ,vwCOADetail.detail_title , PurchaseDemandMasterTable.LocationId , PurchaseDemandMasterTable.PurchaseDemandId , PurchaseDemandQty , 0 as Amount , PurchaseDemandNo From PurchaseDemandMasterTable Left Outer Join vwCOADetail on PurchaseDemandMasterTable.VendorId  = vwCOADetail.coa_detail_id " _
            '        & " Union " _
            '        & " Select  tblVoucher.voucher_id , [dbo].[DetailtitleForVouchers] (tblVoucher.voucher_id,tblVoucher.voucher_type_id )  , tblVoucher.location_id  , tblVoucher.voucher_id , 0 as Qty , Case when voucher_type_id = 2 or voucher_type_id = 4 then SUM(tblVoucherDetail.debit_amount) when voucher_type_id = 3 or voucher_type_id = 5  then SUM(tblVoucherDetail.credit_amount ) else SUM(tblVoucherDetail.debit_amount) end as  Amount , voucher_no  From tblVoucher  inner join tblVoucherDetail on tblVoucher.voucher_id = tblvoucherDetail.voucher_id   group by  tblVoucher.location_id , tblVoucher.voucher_id , voucher_type_id , voucher_no " _
            '        & ") a) Doc On Doc.DocNo = ApprovalHistory.DocumentNo And  Doc.ID  = ApprovalHistory.ReferenceId " _
            '        & " where ApprovalUsersGroup.GroupId = " & LoginGroupId & " " _
            '        & " And (CONVERT(varchar, dbo.ApprovalHistory.DocumentDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102)) "


            Str = "SELECT ApprovalHistory.ApprovalHistoryId, ApprovalHistoryDetail.AprovalHistoryDetailId, ApprovalHistoryDetail.StageId , ApprovalHistoryDetail.[Level], ApprovalHistory.ReferenceType , ApprovalHistory.ReferenceId ,ApprovalHistory.DocumentNo, ApprovalHistory.DocumentDate, isNull(dbo.tblDefVoucherType.voucher_type , '') As voucher_type , Doc.CustomerCode ,Doc.Qty , Doc.Amount ,Doc.PartyName , Doc.LocationId," _
                    & " isnull(Att.AttachmentCount,0) as Attachments, ApprovalHistory.Description, ApprovalStages.Title , ApprovalHistory.Source" _
                    & " FROM ApprovalUsersGroup INNER JOIN ApprovalHistory INNER JOIN" _
                    & " ApprovalHistoryDetail ON ApprovalHistory.ApprovalHistoryId = ApprovalHistoryDetail.AprovalHistoryId INNER JOIN" _
                    & " ApprovalStages ON ApprovalHistoryDetail.StageId = ApprovalStages.ApprovalStagesId" _
                    & "  ON ApprovalUsersGroup.ApprovalhistoryDetailId = ApprovalHistoryDetail.AprovalHistoryDetailId " _
                    & " LEFT OUTER Join dbo.tblDefVoucherType ON dbo.ApprovalHistory.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id " _
                    & " LEFT OUTER JOIN (Select Source,DocId, Count(*) as AttachmentCount From DocumentAttachment Group By DocId, Source) Att On Att.DocId = ApprovalHistory.ReferenceId and Att.Source = ApprovalHistory.Source " _
                    & " LEFT OUTER JOIN (Select SalesMasterTable.CustomerCode , vwCOADetail.detail_title As PartyName , SalesMasterTable.LocationId ,SalesMasterTable.SalesId AS ID,SalesQty AS Qty,SalesAmount AS Amount , SalesNo as DocNo  From SalesMasterTable Left Outer Join vwCOADetail on SalesMasterTable.CustomerCode = vwCOADetail.coa_detail_id WHERE (CONVERT(varchar, dbo.SalesMasterTable.SalesDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102)) And Change_Customer_Code is Null " _
                    & " UNION ALL" _
                    & " Select PurchaseOrderMasterTable.VendorId AS CustomerCode ,vwCOADetail.detail_title As PartyName , PurchaseOrderMasterTable.LocationId , PurchaseOrderMasterTable.PurchaseOrderId AS ID , PurchaseOrderQty AS Qty , PurchaseOrderAmount AS Amount, PurchaseorderNo AS DocNo  From PurchaseOrderMasterTable Left Outer Join vwCOADetail on PurchaseOrderMasterTable.VendorId  = vwCOADetail.coa_detail_id WHERE (CONVERT(varchar, dbo.PurchaseOrderMasterTable.PurchaseOrderDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102)) " _
                    & " UNION ALL " _
                    & " Select ReceivingMasterTable.VendorId AS CustomerCode, vwCOADetail.detail_title As PartyName , ReceivingMasterTable.LocationId ,ReceivingMasterTable.ReceivingId AS ID, ReceivingQty AS Qty, ReceivingAmount AS Amount, ReceivingNo AS DocNo  From ReceivingMasterTable Left Outer Join vwCOADetail on ReceivingMasterTable.VendorId  = vwCOADetail.coa_detail_id WHERE (CONVERT(varchar, dbo.ReceivingMasterTable.ReceivingDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102)) " _
                    & " UNION ALL " _
                    & " Select ReceivingNoteMasterTable.VendorId AS CustomerCode, vwCOADetail.detail_title As PartyName , ReceivingNoteMasterTable.LocationId , ReceivingNoteMasterTable.ReceivingNoteId AS ID , ReceivingQty AS Qty , ReceivingAmount AS Amount , ReceivingNo AS DocNo  From ReceivingNoteMasterTable Left Outer Join vwCOADetail on ReceivingNoteMasterTable.VendorId  = vwCOADetail.coa_detail_id WHERE (CONVERT(varchar, dbo.ReceivingNoteMasterTable.ReceivingDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102))" _
                    & " UNION ALL " _
                    & " Select SalesOrderMasterTable.VendorId AS CustomerCode, vwCOADetail.detail_title As PartyName , SalesOrderMasterTable.LocationId ,SalesOrderMasterTable.SalesOrderId AS ID, SalesOrderQty AS Qty, SalesOrderAmount AS Amount, SalesOrderNo AS DocNo  From SalesOrderMasterTable Left Outer Join vwCOADetail on SalesOrderMasterTable.VendorId = vwCOADetail.coa_detail_id WHERE (CONVERT(varchar, dbo.SalesOrderMasterTable.SalesOrderDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102))" _
                    & " UNION ALL " _
                    & " Select PurchaseReturnMasterTable.VendorId AS CustomerCode ,vwCOADetail.detail_title As PartyName , PurchaseReturnMasterTable.LocationId , PurchaseReturnMasterTable.PurchaseReturnId AS ID , PurchaseReturnQty AS Qty , PurchaseReturnAmount AS Amount , PurchaseReturnNo AS DocNo  From PurchaseReturnMasterTable Left Outer Join vwCOADetail on PurchaseReturnMasterTable.VendorId  = vwCOADetail.coa_detail_id WHERE (CONVERT(varchar, dbo.PurchaseReturnMasterTable.PurchaseReturnDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102))" _
                    & " UNION ALL " _
                    & " Select DeliveryChalanMasterTable.CustomerCode , vwCOADetail.detail_title As PartyName , DeliveryChalanMasterTable.LocationId ,DeliveryChalanMasterTable.DeliveryId AS ID , DeliveryQty AS Qty, DeliveryAmount AS Amount  , DeliveryNo AS DocNo  From DeliveryChalanMasterTable Left Outer Join vwCOADetail on DeliveryChalanMasterTable.CustomerCode = vwCOADetail.coa_detail_id WHERE (CONVERT(varchar, dbo.DeliveryChalanMasterTable.DeliveryDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102))" _
                    & " UNION ALL " _
                    & " Select SalesReturnMasterTable.CustomerCode , vwCOADetail.detail_title As PartyName , SalesReturnMasterTable.LocationId ,SalesReturnMasterTable.SalesReturnId AS ID,SalesReturnQty AS Qty, SalesReturnAmount AS Amount  , SalesReturnNo  AS DocNo From SalesReturnMasterTable Left Outer Join vwCOADetail on SalesReturnMasterTable.CustomerCode = vwCOADetail.coa_detail_id WHERE (CONVERT(varchar, dbo.SalesReturnMasterTable.SalesReturnDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102))" _
                    & " UNION ALL " _
                    & " Select QuotationMasterTable.VendorId AS CustomerCode ,vwCOADetail.detail_title As PartyName, QuotationMasterTable.LocationId , QuotationMasterTable.QuotationId AS ID , SalesOrderQty AS Qty , SalesOrderAmount AS Amount , QuotationNo AS DocNo From QuotationMasterTable Left Outer Join vwCOADetail on QuotationMasterTable.VendorId  = vwCOADetail.coa_detail_id WHERE (CONVERT(varchar, dbo.QuotationMasterTable.QuotationDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102))" _
                    & " UNION ALL " _
                    & "  Select CashRequestHead.EmpID AS CustomerCode , tblDefEmployee.Employee_Name As PartyName , 0 As LocationId ,CashRequestHead.RequestId AS ID , 0 As Qty ,Total_Amount AS Amount , RequestNo AS DocNo  From CashRequestHead Left Outer Join tblDefEmployee on CashRequestHead.EmpID  = tblDefEmployee.Employee_ID WHERE (CONVERT(varchar, dbo.CashRequestHead.RequestDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102)) " _
                    & " UNION ALL " _
                    & "  Select AdvanceRequestTable.EmployeeID AS CustomerCode  ,EmployeesView.Employee_Name As PartyName , 0 As LocationId , AdvanceRequestTable.RequestId AS ID , 0 as Qty , AdvanceAmount AS Amount , RequestNo AS DocNo  From AdvanceRequestTable Left Outer Join EmployeesView on AdvanceRequestTable.EmployeeID   = EmployeesView.Employee_ID WHERE (CONVERT(varchar, dbo.AdvanceRequestTable.RequestDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102)) " _
                    & " UNION ALL " _
                    & " Select SalesInquiryMaster.CustomerId AS CustomerCode  , vwCOADetail.detail_title As PartyName , SalesInquiryMaster.LocationId ,SalesInquiryMaster.SalesInquiryId AS ID, Sum(SalesInquiryDetail.Qty) AS Qty , 0 As Amount , SalesInquiryNo AS DocNo  From SalesInquiryMaster Left Outer Join vwCOADetail on SalesInquiryMaster.CustomerId = vwCOADetail.coa_detail_id Inner Join SalesInquiryDetail on  SalesInquiryMaster.SalesInquiryId = SalesInquiryDetail.SalesInquiryId WHERE (CONVERT(varchar, dbo.SalesInquiryMaster.SalesInquiryDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102)) Group By SalesInquiryMaster.CustomerId ,  vwCOADetail.detail_title , SalesInquiryMaster.LocationId ,SalesInquiryMaster.SalesInquiryId , SalesInquiryNo " _
                    & " UNION ALL " _
                    & " Select 0 As CustomerCode  , [dbo].[Detailtitle] (PurchaseInquiryMaster.PurchaseInquiryId) As PartyName, " _
                    & " 0 as LocationId ,PurchaseInquiryMaster.PurchaseInquiryId AS ID, Sum(PurchaseInquiryDetail.Qty) As Qty , 0 As Amount , PurchaseInquiryNo AS DocNo  From PurchaseInquiryMaster  " _
                    & " Inner Join PurchaseInquiryDetail on  PurchaseInquiryMaster.PurchaseInquiryId = PurchaseInquiryDetail.PurchaseInquiryId " _
                    & " WHERE (CONVERT(varchar, dbo.PurchaseInquiryMaster.PurchaseInquiryDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102)) Group By PurchaseInquiryMaster.PurchaseInquiryId , PurchaseInquiryNo " _
                    & " UNION ALL " _
                    & " Select VendorQuotationMaster.VendorId As CustomerCode ,vwCOADetail.detail_title As PartyName , 0 as LocationId , VendorQuotationMaster.VendorQuotationId AS ID, Sum(VendorQuotationDetail.Qty) As Qty , VendorQuotationMaster.NetTotal as Amount , VendorQuotationNo AS DocNo  From VendorQuotationMaster Left Outer Join vwCOADetail on VendorQuotationMaster.VendorId  = vwCOADetail.coa_detail_id  Inner Join VendorQuotationDetail on  VendorQuotationMaster.VendorQuotationId = VendorQuotationDetail.VendorQuotationId WHERE (CONVERT(varchar, dbo.VendorQuotationMaster.VendorQuotationDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102)) Group By VendorQuotationMaster.VendorId ,  vwCOADetail.detail_title ,VendorQuotationMaster.VendorQuotationId , VendorQuotationNo , VendorQuotationMaster.NetTotal " _
                    & " UNION ALL " _
                    & " Select PurchaseDemandMasterTable.VendorId As CustomerCode , vwCOADetail.detail_title As PartyName, PurchaseDemandMasterTable.LocationId , PurchaseDemandMasterTable.PurchaseDemandId AS ID, PurchaseDemandQty As Qty , 0 as Amount , PurchaseDemandNo AS DocNo From PurchaseDemandMasterTable Left Outer Join vwCOADetail on PurchaseDemandMasterTable.VendorId  = vwCOADetail.coa_detail_id WHERE (CONVERT(varchar, dbo.PurchaseDemandMasterTable.PurchaseDemandDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102))" _
                    & " UNION ALL " _
                    & " Select  0 AS CustomerCode , [dbo].[DetailtitleForVouchers] (tblVoucher.voucher_id,tblVoucher.voucher_type_id ) As PartyName, tblVoucher.location_id AS LocationId , tblVoucher.voucher_id AS ID , 0 as Qty , Case when voucher_type_id = 2 or voucher_type_id = 4 then SUM(tblVoucherDetail.debit_amount) when voucher_type_id = 3 or voucher_type_id = 5  then SUM(tblVoucherDetail.credit_amount ) else SUM(tblVoucherDetail.debit_amount) end as  Amount , voucher_no AS DocNo  From tblVoucher  inner join tblVoucherDetail on tblVoucher.voucher_id = tblvoucherDetail.voucher_id WHERE (CONVERT(varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102)) group by  tblVoucher.location_id , tblVoucher.voucher_id , voucher_type_id , voucher_no " _
                    & " UNION ALL " _
                    & " Select SalesMasterTable.CustomerCode , vwCOADetail.detail_title + '~' +  Coa.detail_title  As PartyName , SalesMasterTable.LocationId ,SalesMasterTable.SalesId AS ID,SalesQty AS Qty,SalesAmount AS Amount , SalesNo as DocNo  From SalesMasterTable  Left Outer Join vwCOADetail on SalesMasterTable.CustomerCode = vwCOADetail.coa_detail_id  LEFT OUTER JOIN vwCOADetail As Coa on IsNull(SalesMasterTable.Change_Customer_Code,0)  = Coa.coa_detail_id WHERE (CONVERT(varchar, dbo.SalesMasterTable.SalesDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102)) And Change_Customer_Code is Not Null " _
                    & " ) Doc On Doc.DocNo = ApprovalHistory.DocumentNo And  Doc.ID  = ApprovalHistory.ReferenceId " _
                    & " where ApprovalUsersGroup.GroupId = " & LoginGroupId & " " _
                    & " And (CONVERT(varchar, dbo.ApprovalHistory.DocumentDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102)) "







            If Me.RdoUnApproved.Checked = True Then
                Str += " And ApprovalHistory.Status = 'InProgress' And ApprovalHistoryDetail.Status = 'WaitingForApproval' "
            ElseIf Me.RdoApproved.Checked = True Then
                Str += " And ApprovalHistory.AprovalProcessId <> 0 and ApprovalHistory.Status <> 'Rejected' And ApprovalHistoryDetail.Level = [dbo].[funGetMaxLevel](ApprovalHistory.ApprovalHistoryId, " & LoginUserId & ") "
            ElseIf Me.RdoRejected.Checked = True Then
                Str += " and ApprovalHistory.Status = 'Rejected' And ApprovalHistoryDetail.Status = 'Rejected' "
            End If
            ''End TFS3227

            If Me.cmbVtype.SelectedIndex = 1 Then
                'Voucher
                Str += "and dbo.ApprovalHistory.source not In ('frmPurchaseDemand','frmPurchaseOrderNew','frmPurchaseNew','frmPurchaseReturn','frmVendorQuotation','frmPurchaseInquiry','frmReceiptVoucherNew','frmReceivingNote','frmImport','frmSalesInquiry','frmQoutationNew', 'frmSales','frmSalesOrderNew', 'frmDeliveryChalan', 'frmSalesReturn' , 'frmCashRequest' , 'frmAdvanceRequest' )"
            ElseIf Me.cmbVtype.SelectedIndex = 2 Then
                'Purchase
                Str += "and dbo.ApprovalHistory.source In ('frmPurchaseNew')"
            ElseIf Me.cmbVtype.SelectedIndex = 3 Then
                'Purchase Return
                Str += "and dbo.ApprovalHistory.source In ('frmPurchaseReturn')"
            ElseIf Me.cmbVtype.SelectedIndex = 4 Then
                'Purchase Demand
                Str += "and dbo.ApprovalHistory.source In ('frmPurchaseDemand')"
            ElseIf Me.cmbVtype.SelectedIndex = 5 Then
                'Purchase Order
                Str += "and dbo.ApprovalHistory.source In ('frmPurchaseOrderNew')"
            ElseIf Me.cmbVtype.SelectedIndex = 6 Then
                'Receiving Note
                Str += "and dbo.ApprovalHistory.source In ('frmReceivingNote')"
            ElseIf Me.cmbVtype.SelectedIndex = 7 Then
                'Vendor Quotation
                Str += "and dbo.ApprovalHistory.source In ('frmVendorQuotation')"
            ElseIf Me.cmbVtype.SelectedIndex = 8 Then
                'Purchase Inquiry
                Str += "and dbo.ApprovalHistory.source In ('frmPurchaseInquiry')"
                ''Start TFS3113
            ElseIf Me.cmbVtype.SelectedIndex = 9 Then
                ' Sales Inquiry
                Str += "and dbo.ApprovalHistory.source In ('frmSalesInquiry')"
            ElseIf Me.cmbVtype.SelectedIndex = 10 Then
                'Sales Quotation 
                Str += "and dbo.ApprovalHistory.source In ('frmQoutationNew')"
            ElseIf Me.cmbVtype.SelectedIndex = 11 Then
                'Purchase Demand
                Str += "and dbo.ApprovalHistory.source In ('frmSales')"
            ElseIf Me.cmbVtype.SelectedIndex = 12 Then
                'Sales Order 
                Str += "and dbo.ApprovalHistory.source In ('frmSalesOrderNew')"
            ElseIf Me.cmbVtype.SelectedIndex = 13 Then
                'Delivery Chalan
                Str += "and dbo.ApprovalHistory.source In ('frmDeliveryChalan')"
            ElseIf Me.cmbVtype.SelectedIndex = 14 Then
                'Sales Retrun
                Str += "and dbo.ApprovalHistory.source In ('frmSalesReturn')"
            ElseIf Me.cmbVtype.SelectedIndex = 15 Then
                'Sales Invoice Transfer : TFS4431 
                Str += "and dbo.ApprovalHistory.source In ('frmSalesTransfer')"
            ElseIf Me.cmbVtype.SelectedIndex = 16 Then
                'Cash Request
                Str += "and dbo.ApprovalHistory.source In ('frmCashRequest')"
            ElseIf Me.cmbVtype.SelectedIndex = 17 Then
                'Employee Deduction
                Str += "and dbo.ApprovalHistory.source In ('frmAdvanceRequest')"
                ''End TFS3113
            End If
            Str += " order by ApprovalHistory.DocumentNo DESC"
            Call FillGridEx(Me.grdLog, Str)
            grdLog.RetrieveStructure()
            'If Me.grdLog.GetRows.Length - 1 Thenp
            'For Each r As DataGridViewRow In grdLog.Rows
            'For Each r As Janus.Windows.GridEX.GridEXRow In grdLog.GetRows
            '    r.BeginEdit()
            '    r.Cells.Item("User Name").Value = Decrypt(r.Cells.Item("User Name").Value)
            '    r.EndEdit()
            'Next
            'Me.grdLog.RootTable.Columns("Date and Time").FormatString = "dd/MMM/yyyy"
            'End If
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub RdoApproved_CheckedChanged(sender As Object, e As EventArgs) Handles RdoApproved.CheckedChanged
        Try
            Call ShowApprovalLog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RdoUnApproved_CheckedChanged(sender As Object, e As EventArgs) Handles RdoUnApproved.CheckedChanged
        Try
            Call ShowApprovalLog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RdoRejected_CheckedChanged(sender As Object, e As EventArgs) Handles RdoRejected.CheckedChanged
        Try
            Call ShowApprovalLog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings(Optional Condition As String = "")
        Try
            If Me.grdLog.RowCount = 0 Then Exit Sub
            Me.grdLog.RootTable.Columns(enmgrd.ApprovalHistoryId).Visible = False
            Me.grdLog.RootTable.Columns(enmgrd.AprovalHistoryDetailId).Visible = False
            Me.grdLog.RootTable.Columns(enmgrd.Level).Visible = False
            Me.grdLog.RootTable.Columns(enmgrd.StageId).Visible = False
            Me.grdLog.RootTable.Columns(enmgrd.ReferenceId).Visible = False
            Me.grdLog.RootTable.Columns(enmgrd.LocationId).Visible = False ''TFS3229
            Me.grdLog.RootTable.Columns(enmgrd.CustomerCode).Visible = False ''TFS3229
            Me.grdLog.RootTable.Columns(enmgrd.Source).Visible = False
            Me.grdLog.RootTable.Columns(enmgrd.ReferenceType).Visible = False
            Me.grdLog.RootTable.Columns(enmgrd.voucher_type).Caption = "Doc Type"
            Me.grdLog.RootTable.Columns(enmgrd.voucher_type).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grdLog.RootTable.Columns(enmgrd.Title).Caption = "Stages"
            Me.grdLog.RootTable.Columns(enmgrd.Title).Width = 70
            Me.grdLog.RootTable.Columns(enmgrd.Attachments).Caption = "Attachment Preview"
            Me.grdLog.RootTable.Columns(3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grdLog.RootTable.Columns(enmgrd.Amount).FormatString = "N" & DecimalPointInValue ''TFS3404
            Me.grdLog.RootTable.Columns(enmgrd.Amount).FormatString = "N" & TotalAmountRounding
            Me.grdLog.RootTable.Columns(enmgrd.Amount).TotalFormatString = "N" & TotalAmountRounding
            Me.grdLog.RootTable.Columns(enmgrd.Attachments).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grdLog.RootTable.Columns(enmgrd.DocumentNo).ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdLog.RootTable.Columns(enmgrd.Attachments).ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdLog.RootTable.Columns(enmgrd.Amount).FormatString = "N" & DecimalPointInValue ''TFS3404
            Me.grdLog.RootTable.Columns(enmgrd.Amount).FormatString = "N" & TotalAmountRounding
            Me.grdLog.RootTable.Columns(enmgrd.Amount).TotalFormatString = "N" & TotalAmountRounding
            Me.grdLog.RootTable.Columns(enmgrd.Qty).FormatString = "N" & DecimalPointInValue ''TFS3404
            Me.grdLog.RootTable.Columns(enmgrd.Qty).FormatString = "N" & TotalAmountRounding
            Me.grdLog.RootTable.Columns(enmgrd.Qty).TotalFormatString = "N" & TotalAmountRounding

            'For c As Integer = 0 To Me.grdLog.RootTable.Columns.Count - 1
            '    grdLog.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
            '    grdLog.RootTable.Columns(c).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            'Next
            'Add Checkbox Column in grdLoandRequests grid
            If Me.grdLog.RootTable.Columns.Contains("Column1") = False Then
                Me.grdLog.RootTable.Columns.Add("Column1")
                Me.grdLog.RootTable.Columns("Column1").UseHeaderSelector = True
                Me.grdLog.RootTable.Columns("Column1").ActAsSelector = True
            End If
            If Me.grdLog.RootTable.Columns.Contains("Approve") = False Then
                Me.grdLog.RootTable.Columns.Add("Approve")
                Me.grdLog.RootTable.Columns("Approve").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdLog.RootTable.Columns("Approve").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdLog.RootTable.Columns("Approve").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdLog.RootTable.Columns("Approve").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdLog.RootTable.Columns("Approve").Width = 70
                Me.grdLog.RootTable.Columns("Approve").ButtonText = "Approve"
                Me.grdLog.RootTable.Columns("Approve").Key = "Approve"
                Me.grdLog.RootTable.Columns("Approve").Caption = "Approve"
            End If

            If Me.grdLog.RootTable.Columns.Contains("Reject") = False Then
                Me.grdLog.RootTable.Columns.Add("Reject")
                Me.grdLog.RootTable.Columns("Reject").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdLog.RootTable.Columns("Reject").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdLog.RootTable.Columns("Reject").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdLog.RootTable.Columns("Reject").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdLog.RootTable.Columns("Reject").Width = 70
                Me.grdLog.RootTable.Columns("Reject").ButtonText = "Reject"
                Me.grdLog.RootTable.Columns("Reject").Key = "Reject"
                Me.grdLog.RootTable.Columns("Reject").Caption = "Reject"
            End If
            If Me.grdLog.RootTable.Columns.Contains("History") = False Then
                Me.grdLog.RootTable.Columns.Add("History")
                Me.grdLog.RootTable.Columns("History").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdLog.RootTable.Columns("History").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdLog.RootTable.Columns("History").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdLog.RootTable.Columns("History").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdLog.RootTable.Columns("History").Width = 150
                Me.grdLog.RootTable.Columns("History").ButtonText = "History "
                Me.grdLog.RootTable.Columns("History").Key = "History"
                Me.grdLog.RootTable.Columns("History").Caption = "Approval Log"
            End If

            If Me.grdLog.RootTable.Columns.Contains("Preview") = False Then
                Me.grdLog.RootTable.Columns.Add("Preview")
                Me.grdLog.RootTable.Columns("Preview").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdLog.RootTable.Columns("Preview").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdLog.RootTable.Columns("Preview").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdLog.RootTable.Columns("Preview").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdLog.RootTable.Columns("Preview").Width = 150
                Me.grdLog.RootTable.Columns("Preview").ButtonText = "Preview"
                Me.grdLog.RootTable.Columns("Preview").Key = "Preview"
                Me.grdLog.RootTable.Columns("Preview").Caption = "Document Preview"
            End If

            ''Start TFS3227
            If RdoUnApproved.Checked = True Then
                Me.grdLog.RootTable.Columns("Column1").Visible = True
                Me.grdLog.RootTable.Columns("Approve").Visible = True
                Me.grdLog.RootTable.Columns("Reject").Visible = True
                Me.btnApproveRequest.Enabled = True
                Me.btnRejectRequest.Enabled = True
                Me.btnApproveRequest.Visible = True
                Me.btnRejectRequest.Visible = True
            ElseIf RdoApproved.Checked = True Then
                Me.grdLog.RootTable.Columns("Column1").Visible = True
                Me.grdLog.RootTable.Columns("Approve").Visible = False
                Me.grdLog.RootTable.Columns("Reject").Visible = True
                Me.btnApproveRequest.Enabled = False
                Me.btnRejectRequest.Enabled = True
                Me.btnApproveRequest.Visible = False
                Me.btnRejectRequest.Visible = True
            ElseIf RdoRejected.Checked = True Then
                Me.grdLog.RootTable.Columns("Column1").Visible = False
                Me.grdLog.RootTable.Columns("Approve").Visible = False
                Me.grdLog.RootTable.Columns("Reject").Visible = False
                Me.btnApproveRequest.Enabled = False
                Me.btnRejectRequest.Enabled = False
                Me.btnApproveRequest.Visible = False
                Me.btnRejectRequest.Visible = False
            End If
            ''End TFS3227

            'If Me.grdLog.RootTable.Columns.Contains("Preveiw Attach") = False Then
            '    Me.grdLog.RootTable.Columns.Add("Preveiw Attach")
            '    Me.grdLog.RootTable.Columns("Preveiw Attach").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            '    Me.grdLog.RootTable.Columns("Preveiw Attach").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            '    Me.grdLog.RootTable.Columns("Preveiw Attach").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '    Me.grdLog.RootTable.Columns("Preveiw Attach").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '    Me.grdLog.RootTable.Columns("Preveiw Attach").Width = 50
            '    Me.grdLog.RootTable.Columns("Preveiw Attach").ButtonText = "Preveiw Attach "
            '    Me.grdLog.RootTable.Columns("Preveiw Attach").Key = "Preveiw Attach"
            '    Me.grdLog.RootTable.Columns("Preveiw Attach").Caption = "Action"
            'End If
            Me.grdLog.RootTable.Columns(enmgrd.DocumentDate).FormatString = str_DisplayDateFormat
            Me.grdLog.AutoSizeColumns()
            Me.grdLog.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            ''Start TFS3432
            Me.grdLog.RootTable.Columns(enmgrd.Description).MaxLines = 10 ''Added to multi line Description
            Me.grdLog.RootTable.Columns(enmgrd.Description).MinLines = 1
            Me.grdLog.RootTable.Columns(enmgrd.Description).Width = 250
            Me.grdLog.RootTable.Columns(enmgrd.Description).WordWrap = True
            Me.grdLog.RootTable.Columns(enmgrd.PartyName).Width = 250
            Me.grdLog.RootTable.Columns(enmgrd.PartyName).MaxLines = 10 ''Added to multi line PartyName
            Me.grdLog.RootTable.Columns(enmgrd.PartyName).MinLines = 1
            Me.grdLog.RootTable.Columns(enmgrd.PartyName).WordWrap = True
            ''End TFS3432
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.RdoUnApproved.Visible = True
                Me.RdoApproved.Visible = True
                Me.RdoRejected.Visible = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Me.RdoUnApproved.Visible = False
                    Me.RdoApproved.Visible = False
                    Me.RdoRejected.Visible = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                Me.RdoUnApproved.Visible = False
                Me.RdoApproved.Visible = False
                Me.RdoRejected.Visible = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "View Approved" Then
                        Me.RdoUnApproved.Visible = True
                        Me.RdoApproved.Visible = True
                    ElseIf RightsDt.FormControlName = "View Rejected" Then
                        Me.RdoUnApproved.Visible = True
                        Me.RdoRejected.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdLog.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdLog.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdLog.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Approval Log "

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdLog_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdLog.ColumnButtonClick
        Try

            ApprovalHistory = New ApprovalHistoryBE
            ApprovalHistoryDetail = New ApprovalHistoryDetailBE
            ApprovalHistory.ApprovalHistoryId = Val(Me.grdLog.CurrentRow.Cells(enmgrd.ApprovalHistoryId).Value.ToString)
            ApprovalHistory.ReferenceType = Me.grdLog.CurrentRow.Cells(enmgrd.ReferenceType).Value.ToString
            ApprovalHistory.DocumentNo = Me.grdLog.CurrentRow.Cells(enmgrd.DocumentNo).Value.ToString
            ApprovalHistory.Source = Me.grdLog.CurrentRow.Cells(enmgrd.Source).Value.ToString
            ApprovalHistory.ReferenceId = Me.grdLog.CurrentRow.Cells(enmgrd.ReferenceId).Value.ToString
            ApprovalHistoryDetail.AprovalHistoryDetailId = Val(Me.grdLog.CurrentRow.Cells(enmgrd.AprovalHistoryDetailId).Value.ToString)
            ApprovalHistoryDetail.Level = Val(Me.grdLog.CurrentRow.Cells(enmgrd.Level).Value.ToString)

            If e.Column.Key = "Approve" Then

                ''Start: Waqar Added these lines just to Lock the Approval for SO if Customer is on Hold.
                If Me.grdLog.CurrentRow.Cells(enmgrd.DocumentNo).Value.ToString.Contains("SO") Then
                    Dim str As String = "SELECT ISNULL(Hold,0) as Hold from tblCustomer where AccountId = " & Me.grdLog.CurrentRow.Cells(enmgrd.CustomerCode).Value.ToString & ""
                    Dim dt As DataTable = GetDataTable(str)
                    If dt.Rows.Count > 0 Then
                        If dt.Rows(0).Item(0).ToString = "True" Then
                            ShowErrorMessage("Cannot approve this SO because this Customer is on Hold.")
                            Exit Sub
                        End If
                    End If
                End If
                ''End: Waqar Added these lines just to Lock the Approval for SO if Customer is on Hold.

                If Me.grdLog.CurrentRow.Cells(enmgrd.DocumentNo).Value.ToString.Contains("PO") Then
                    Dim str As String = "SELECT Amount,RemainingAmount from tbldefCostCenter where CostCenterID IN ( SELECT CostCenterId from PurchaseOrderMasterTable WHERE PurchaseOrderNo = '" & Me.grdLog.CurrentRow.Cells(enmgrd.DocumentNo).Value.ToString & "')"
                    'Dim str As String = "SELECT Amount,RemainingAmount from tbldefCostCenter where AccountId = " & Me.grdLog.CurrentRow.Cells(enmgrd.CustomerCode).Value.ToString & ""
                    Dim dt As DataTable = GetDataTable(str)
                    If dt.Rows.Count > 0 Then
                        If dt.Rows(0).Item(0).ToString = "True" Then
                            ShowErrorMessage("Cannot approve this SO because this Customer is on Hold.")
                            Exit Sub
                        End If
                    End If
                End If

                If Not msg_Confirm("Do You Want to Approve") = True Then Exit Sub
                ApprovalHistory.Status = "Approved"
                'UpdateApprovalHistory(ApprovalHistory)
                ApprovalHistoryDetail.Status = "Approved"
                If UpdateApprovalHistoryDetail(ApprovalHistoryDetail, ApprovalHistory) = True Then
                    ''TASK TFS4444
                    If EmailNotification = True Then
                        UsersEmail = New List(Of String)
                        DocumentNo = ApprovalHistory.DocumentNo
                        Dim dtUsersEmail As DataTable = GetUsersEmail(ApprovalHistory.ApprovalHistoryId, ApprovalHistoryDetail.Level)
                        If dtUsersEmail.Rows.Count > 0 Then
                            For Each _Email As DataRow In dtUsersEmail.Rows
                                If _Email.Item("Email").ToString.Length > 0 Then
                                    UsersEmail.Add(_Email.Item("Email").ToString)
                                End If
                            Next
                        End If
                        SendAutoEmail("Approve")
                    End If
                    ''END TASK TFS4444
                End If
                ShowApprovalLog()
            End If
            If e.Column.Key = "Reject" Then
                If Not msg_Confirm("Do You Want to Reject") = True Then Exit Sub
                If RdoApproved.Checked = True Then
                    If ValidateHigherHierarchy(ApprovalHistory.ApprovalHistoryId, ApprovalHistoryDetail.Level) = False Then
                        frmApprovalRejectionDetail.BringToFront()
                        frmApprovalRejectionDetail.Text = ApprovalHistory.DocumentNo
                        frmApprovalRejectionDetail.ShowDialog()

                        If frmApprovalRejectionDetail.DialogResult = Windows.Forms.DialogResult.OK Then
                            ApprovalHistoryDetail.Status = "Rejected"
                            ApprovalHistory.Status = "Rejected"
                            ApprovalHistoryDetail.ApprovalRejectionReason = frmApprovalRejectionDetail.RejectionRemarks
                            ApprovalHistoryDetail.ApprovalRejectionReasonId = frmApprovalRejectionDetail.RejectionId
                            If UpdateApprovalHistoryForRejection(ApprovalHistoryDetail, ApprovalHistory) = True Then
                                ''TASK TFS4444
                                If EmailNotification = True Then
                                    UsersEmail = New List(Of String)
                                    DocumentNo = ApprovalHistory.DocumentNo & " has been rejected by " & LoginUserName & "."
                                    Dim dtUsersEmail As DataTable = GetUsersEmail(ApprovalHistory.ApprovalHistoryId, 0)
                                    If dtUsersEmail.Rows.Count > 0 Then
                                        For Each _Email As DataRow In dtUsersEmail.Rows
                                            If _Email.Item("Email").ToString.Length > 0 Then
                                                UsersEmail.Add(_Email.Item("Email").ToString)
                                            End If
                                        Next
                                    End If
                                    SendAutoEmail("Reject")
                                End If
                                ''END TASK TFS4444
                            End If
                        End If
                    Else
                        ShowErrorMessage("Document can not be Rejected , higher herarchey dependency")
                        Exit Sub
                    End If
                Else
                    frmApprovalRejectionDetail.BringToFront()
                    frmApprovalRejectionDetail.Text = ApprovalHistory.DocumentNo
                    frmApprovalRejectionDetail.ShowDialog()

                    If frmApprovalRejectionDetail.DialogResult = Windows.Forms.DialogResult.OK Then
                        ApprovalHistoryDetail.Status = "Rejected"
                        ApprovalHistory.Status = "Rejected"
                        ApprovalHistoryDetail.ApprovalRejectionReason = frmApprovalRejectionDetail.RejectionRemarks
                        ApprovalHistoryDetail.ApprovalRejectionReasonId = frmApprovalRejectionDetail.RejectionId
                        If UpdateApprovalHistoryForRejection(ApprovalHistoryDetail, ApprovalHistory) = True Then
                            ''TASK TFS4444
                            If EmailNotification = True Then
                                UsersEmail = New List(Of String)
                                DocumentNo = ApprovalHistory.DocumentNo & " has been rejected by " & LoginUserName & "."
                                Dim dtUsersEmail As DataTable = GetUsersEmail(ApprovalHistory.ApprovalHistoryId, 0)
                                If dtUsersEmail.Rows.Count > 0 Then
                                    For Each _Email As DataRow In dtUsersEmail.Rows
                                        If _Email.Item("Email").ToString.Length > 0 Then
                                            UsersEmail.Add(_Email.Item("Email").ToString)
                                        End If
                                    Next
                                End If
                                SendAutoEmail("Reject")
                            End If
                            ''END TASK TFS4444
                        End If
                    End If
                End If

                ShowApprovalLog()
            End If
            If e.Column.Key = "History" Then
                frmApprovalHistory.BringToFront()
                frmApprovalHistory.DocumentNo = ApprovalHistory.DocumentNo
                frmApprovalHistory.Source = ApprovalHistory.Source
                frmApprovalHistory.ShowDialog()

            End If

            If e.Column.Key = "Preview" Then
                Dim strSource As String = GetSource(Me.grdLog.GetRow.Cells(enmgrd.ReferenceType).Text.ToString)
                Dim IsPrint As Boolean = False
                ''The Parameter (true) for Showing Report in container is removed from each Show Report : TFS3404
                Select Case strSource
                    Case "frmPurchase"
                        IsPrint = True
                        ShowReport("PurchaseInvoice", "{ReceivingMasterTable.ReceivingId}=" & grdLog.GetRow.Cells(enmgrd.ReferenceId).Value.ToString, , , , , , , , , )
                    Case "frmPurchaseReturn"
                        IsPrint = True
                        ShowReport("PurchaseReturn", "{PurchaseReturnMasterTable.PurchaseReturnId}=" & grdLog.GetRow.Cells(enmgrd.ReferenceId).Value.ToString, , , , , , , , , )
                    Case "frmVendorQuotation"
                        IsPrint = True
                        ShowInformationMessage("Print preview not available")
                    Case "frmPurchaseOrderNew"
                        IsPrint = True
                        ShowReport("PurchaseOrder", "{PurchaseOrderMasterTable.PurchaseOrderId}=" & grdLog.GetRow.Cells(enmgrd.ReferenceId).Value.ToString, , , , , , , , , )
                    Case "frmPurchaseDemand"
                        IsPrint = True
                        ShowReport("PurchaseDemand", "{PurchaseDemandMasterTable.PurchaseDemandId}=" & grdLog.GetRow.Cells(enmgrd.ReferenceId).Value.ToString, , , , , , , , , )
                    Case "frmReceivingNote"
                        IsPrint = True
                        AddRptParam("@ReceivingNoteId", grdLog.GetRow.Cells(enmgrd.ReferenceId).Value)
                        ShowReport("inwardgatepassreceived", , , , , , , , , , , )
                    Case "frmPurchaseInquiry"
                        IsPrint = True
                        ShowInformationMessage("Print preview not available")
                        ''Start TFS3113
                    Case "frmQoutationNew"
                        IsPrint = True
                        AddRptParam("@QuotationId", Val(Me.grdLog.GetRow.Cells(enmgrd.ReferenceId).Value.ToString))
                        ShowReport("rptQuotationItemSpecifications")
                    Case "frmSales"
                        IsPrint = True
                        'Dim newinvoice As Boolean = False
                        'Dim strCriteria As String = "Nothing"
                        'newinvoice = getConfigValueByType("NewInvoice")
                        'If newinvoice = True Then
                        '    str_ReportParam = "@SaleID|" & grdLog.CurrentRow.Cells(enmgrd.ReferenceId).Value
                        'Else
                        '    str_ReportParam = String.Empty
                        '    strCriteria = "{SalesDetailTable.SalesId} = " & grdLog.CurrentRow.Cells(enmgrd.ReferenceId).Value
                        'End If

                        'ShowReport(IIf(newinvoice = False, "SalesInvoice", "SalesInvoiceNew") & grdLog.CurrentRow.Cells(enmgrd.LocationId).Value.ToString, strCriteria, "Nothing", "Nothing", False, , "New", , , , , )
                        AddRptParam("@SaleID", Me.grdLog.GetRow.Cells(enmgrd.ReferenceId).Value.ToString)
                        ShowReport("SalesInvoiceNew" & Me.grdLog.GetRow.Cells(enmgrd.LocationId).Value.ToString, , , , , , , , , , )
                    Case "frmDeliveryChalan"
                        IsPrint = True
                        'Dim newinvoice As Boolean = False
                        'Dim strCriteria As String = "Nothing"
                        'newinvoice = getConfigValueByType("NewInvoice")
                        'If newinvoice = True Then
                        '    str_ReportParam = "@DeliveryID|" & grdLog.CurrentRow.Cells(enmgrd.ReferenceId).Value
                        'Else
                        '    str_ReportParam = String.Empty
                        '    strCriteria = "{DeliveryChalanDetailTable.DeliveryId} = " & grdLog.CurrentRow.Cells(enmgrd.ReferenceId).Value
                        'End If

                        'ShowReport(IIf(newinvoice = False, "DeliveryChalan", "DeliveryChalanNew") & grdLog.CurrentRow.Cells(enmgrd.LocationId).Value.ToString, strCriteria, "Nothing", "Nothing", False, , "New", , , , , )
                        AddRptParam("@DeliveryID", Me.grdLog.GetRow.Cells(enmgrd.ReferenceId).Value.ToString)
                        ShowReport("DeliveryChalanNew" & Me.grdLog.GetRow.Cells(enmgrd.LocationId).Value.ToString, , , , , , , , , , )
                    Case "frmSalesOrderNew"
                        IsPrint = True
                        AddRptParam("@SalesOrderId", Me.grdLog.GetRow.Cells(enmgrd.ReferenceId).Value)
                        ShowReport("rptSalesOrder", , , , , , , , , , , )
                    Case "frmSalesReturn"
                        IsPrint = True
                        ShowReport("SalesReturn", "{SalesReturnMasterTable.SalesReturnId}=" & grdLog.GetRow.Cells(enmgrd.ReferenceId).Value, , , , , , , , , )
                    Case "frmSalesInquiry"
                        IsPrint = True
                        ShowInformationMessage("Print preview not available")
                    Case "frmCashRequest"
                        IsPrint = True
                        ShowInformationMessage("Print preview not available")
                    Case "frmAdvanceRequest"
                        IsPrint = True
                        ShowInformationMessage("Print preview not available")
                    Case "frmSalesTransfer" ''TFS4431 
                        IsPrint = True
                        ShowInformationMessage("Print preview not available")
                        ''End TFS3113
                End Select
                If IsPrint = False Then
                    'AddRptParam("@VoucherId", Me.grd.GetRow.Cells("Voucher_Id").Value.ToString)
                    ShowReport("rptVoucher", , , , , , , DTFromGrid(Val(Me.grdLog.GetRow.Cells(enmgrd.ReferenceId).Value.ToString)), , , )
                End If
                If ReportViewerForContainer IsNot Nothing Then
                    ReportViewerForContainer.Show()
                    ReportViewerForContainer.BringToFront()
                End If
                'If ReportViewerForContainer IsNot Nothing Then
                '    ReportViewerForContainer.Show()
                '    ReportViewerForContainer.BringToFront()
                'End If
                'Exit Sub
                'ElseIf e.Column.Key = "Preveiw Attach" Then
                '    AddRptParam("@CompanyName", CompanyTitle)
                '    AddRptParam("@CompanyAddress", CompanyAddressHeader)
                '    AddRptParam("@ShowHeader", True)
                '    DataSetShowReport("RptVoucherDocument", GetVoucherRecord(), True)
                '    If ReportViewerForContainer IsNot Nothing Then
                '        ReportViewerForContainer.Show()
                '        ReportViewerForContainer.BringToFront()
                '    End If
                'Else
                '    Exit Sub
            End If


            'Ali Faisal : Commented due to stop recall of grid data refil on report opening or attachement opening
            'ShowApprovalLog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function GetVoucherRecord() As DataSet
        Try

            Dim strSQL As String = String.Empty
            Dim ds As New dsVoucherDocumentAttachment
            ds.Tables.Clear()
            strSQL = "SELECT  TOP 100 PERCENT V.voucher_id, V.voucher_no, V.voucher_date, V.voucher_code, VTP.voucher_type, V.Reference, V.post, V.BankDesc, V.UserName, " _
                    & " V.Posted_UserName, V.CheckedByUser, V.Checked, VD.voucher_detail_id, VD.coa_detail_id, COA.detail_code, COA.detail_title, VD.comments, VD.debit_amount,  " _
                    & " VD.credit_amount, VD.sp_refrence, VD.direction, VD.CostCenterID, VD.Adjustment, VD.Cheque_No, VD.Cheque_Date, VD.BankDescription, VD.Tax_Percent,  " _
                    & " VD.Tax_Amount, VD.Cheque_Clearance_Date, VD.PayeeTitle, VD.Cheque_Status, VD.ChequeDescription, COA.sub_sub_code, COA.sub_sub_title " _
                    & " FROM dbo.ApprovalHistory AS V INNER JOIN " _
                    & " dbo.ApprovalHistoryDetail AS VD ON V.voucher_id = VD.voucher_id INNER JOIN " _
                    & " dbo.vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN  " _
                    & " dbo.tblDefVoucherType AS VTP ON V.voucher_type_id = VTP.voucher_type_id WHERE (V.voucher_id=" & Me.grdLog.GetRow.Cells(enmgrd.ReferenceId).Value & ") " _
                    & " ORDER BY VD.voucher_detail_id "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            ds.Tables.Add(dt)
            ds.Tables(0).TableName = "dtVoucher"


            strSQL = String.Empty
            strSQL = "Select DocId,FileName,Path,Convert(Image,'') as Attachment_Image From DocumentAttachment WHERE (DocId=" & Me.grdLog.GetRow.Cells(enmgrd.ReferenceId).Value & ") AND Source='" & Me.grdLog.GetRow.Cells(enmgrd.ReferenceType).Value.ToString.Replace("VoucherEntry", "frmVoucherNew") & "'"
            Dim dtAttach As New DataTable
            dtAttach.TableName = "dtAttachment"
            dtAttach = GetDataTable(strSQL)

            If dtAttach IsNot Nothing Then
                If dtAttach.Rows.Count > 0 Then
                    For Each r As DataRow In dtAttach.Rows
                        r.BeginEdit()
                        LoadPicture(r, "Attachment_Image", CStr(r("Path").ToString & "\" & r("FileName").ToString))
                        r.EndEdit()
                    Next
                End If
            End If

            ds.Tables.Add(dtAttach)
            ds.Tables(1).TableName = "dtAttachment"
            ds.AcceptChanges()

            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function DTFromGrid(ByVal voucherID As Int32) As DataTable
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim DT As New DataTable
            DT = GetDataTable("SP_RptVoucher " & voucherID & "")
            DT.AcceptChanges()

            Return DT
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Function
    Private Sub grdLog_LinkClicked(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdLog.LinkClicked
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim Source As String = String.Empty
            If Me.grdLog.RowCount = 0 Then Exit Sub
            frmModProperty.Tags = String.Empty

            If e.Column.Key = "DocumentNo" Then
                frmModProperty.Tags = Me.grdLog.GetRow.Cells(enmgrd.DocumentNo).Text
                If IsDrillDown = True Then
                    frmMain.LoadControl(GetSource(Me.grdLog.GetRow.Cells(enmgrd.ReferenceType).Text.ToString))
                    System.Threading.Thread.Sleep(500)
                Else
                    frmMain.LoadControl(GetSource(Me.grdLog.GetRow.Cells(enmgrd.ReferenceType).Text.ToString))
                    System.Threading.Thread.Sleep(500)
                    frmModProperty.Tags = Me.grdLog.GetRow.Cells(enmgrd.DocumentNo).Text
                    frmMain.LoadControl(GetSource(Me.grdLog.GetRow.Cells(enmgrd.ReferenceType).Text.ToString))
                    System.Threading.Thread.Sleep(500)
                End If
            ElseIf e.Column.Key = "Attachments" Then
                Dim frm As New frmAttachmentViewForApproval
                frm._Source = Me.grdLog.GetRow.Cells(enmgrd.Source).Value.ToString
                frm._VoucherId = Val(Me.grdLog.GetRow.Cells(enmgrd.ReferenceId).Value.ToString)
                frm.ShowDialog()
                Exit Sub
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function GetSource(ByVal Reference As String) As String
        Dim str As String = ""

        Try

            If Reference = EnumReferenceType.PurchaseDemand.ToString Then
                str = "frmPurchaseDemand"
            ElseIf Reference = EnumReferenceType.Purchase.ToString Then
                str = "frmPurchase"
            ElseIf Reference = EnumReferenceType.PurchaseOrder.ToString Then
                str = "frmPurchaseOrderNew"
            ElseIf Reference = EnumReferenceType.PurchaseReturn.ToString Then
                str = "frmPurchaseReturn"
            ElseIf Reference = EnumReferenceType.PurchaseInquiry.ToString Then
                str = "frmPurchaseInquiry"
            ElseIf Reference = EnumReferenceType.GRN.ToString Then
                str = "frmReceivingNote"
            ElseIf Reference = EnumReferenceType.VendorQuotation.ToString Then
                str = "frmVendorQuotation"
            ElseIf Reference = EnumReferenceType.VoucherEntry.ToString Then
                str = "frmVoucher"
            ElseIf Reference = EnumReferenceType.Expense.ToString Then
                str = "frmExpense"
            ElseIf Reference = EnumReferenceType.Payment.ToString Then
                str = "frmVendorPayment"
            ElseIf Reference = EnumReferenceType.Receipt.ToString Then
                str = "frmCustomerCollection"
            ElseIf Reference = EnumReferenceType.SalesQuotation.ToString Then
                str = "frmQoutationNew"
            ElseIf Reference = EnumReferenceType.SalesInquiry.ToString Then
                str = "frmSalesInquiry"
            ElseIf Reference = EnumReferenceType.SalesReturn.ToString Then
                str = "frmSalesReturn"
            ElseIf Reference = EnumReferenceType.SalesOrder.ToString Then
                str = "frmSalesOrderNew"
            ElseIf Reference = EnumReferenceType.Sales.ToString Then
                str = "frmSales"
            ElseIf Reference = EnumReferenceType.DeliveryChallan.ToString Then
                str = "frmDeliveryChalan"
            ElseIf Reference = EnumReferenceType.CashRequest.ToString Then
                str = "frmCashRequest"
            ElseIf Reference = EnumReferenceType.EmployeeLoanRequest.ToString Then
                str = "frmAdvanceRequest"
            ElseIf Reference = EnumReferenceType.SalesInvoiceTransfer.ToString Then
                str = "frmSales"
            End If

            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnApproveRequest_Click(sender As Object, e As EventArgs) Handles btnApproveRequest.Click
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As OleDbTransaction = objCon.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = objTrans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text
            Dim strSQL As String = String.Empty

            If Not msg_Confirm("Do You Want to Approve") = True Then Exit Sub
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdLog.GetCheckedRows
                ApprovalHistory = New ApprovalHistoryBE
                ApprovalHistoryDetail = New ApprovalHistoryDetailBE
                ApprovalHistory.ApprovalHistoryId = Val(r.Cells(enmgrd.ApprovalHistoryId).Value.ToString)
                ApprovalHistory.ReferenceType = r.Cells(enmgrd.ReferenceType).Value.ToString
                ApprovalHistory.DocumentNo = r.Cells(enmgrd.DocumentNo).Value.ToString
                ApprovalHistory.Source = r.Cells(enmgrd.Source).Value.ToString
                ApprovalHistory.ReferenceId = r.Cells(enmgrd.ReferenceId).Value.ToString
                ApprovalHistoryDetail.AprovalHistoryDetailId = Val(r.Cells(enmgrd.AprovalHistoryDetailId).Value.ToString)
                ApprovalHistoryDetail.Level = Val(r.Cells(enmgrd.Level).Value.ToString)
                ApprovalHistory.Status = "Approved"
                'UpdateApprovalHistory(ApprovalHistory)
                ApprovalHistoryDetail.Status = "Approved"
                UpdateApprovalHistoryDetail(ApprovalHistoryDetail, ApprovalHistory)
            Next

            Me.btnApproveRequest.Visible = False
            Me.btnRejectRequest.Visible = False


            objTrans.Commit()
            Call ShowApprovalLog()

        Catch ex As Exception
            objTrans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            objCon.Close()
        End Try
    End Sub

    Function UpdateApprovalHistoryForRejection(ByVal objModel As ApprovalHistoryDetailBE, ByVal objMod As ApprovalHistoryBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction

            str = " Update ApprovalHistoryDetail Set Status = '" & objModel.Status & "' , ApprovalRejectionReason =' " & objModel.ApprovalRejectionReason & "' , ApprovalRejectionReasonId = " & objModel.ApprovalRejectionReasonId & " , ApprovalUserId = " & LoginUserId & " , ApprovalDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "'  where  AprovalHistoryDetailId = " & objModel.AprovalHistoryDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = " Update ApprovalHistory Set Status = '" & objMod.Status & "' , LogUserID = " & LoginUserId & " where  ApprovalHistoryId = " & objMod.ApprovalHistoryId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            UpdateDocumentForRejection(objMod.ReferenceType, objMod.DocumentNo, objMod.ReferenceId)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Function UpdateApprovalHistoryDetail(ByVal objModel As ApprovalHistoryDetailBE, ByVal objMod As ApprovalHistoryBE) As Boolean
        Dim NoOfLevels As Integer = 0
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction

            str = " Update ApprovalHistoryDetail Set Status = '" & objModel.Status & "', ApprovalUserId = " & LoginUserId & ", ApprovalDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where AprovalHistoryDetailId = " & objModel.AprovalHistoryDetailId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = "Select COUNT(*) from ApprovalHistoryDetail where AprovalHistoryId = " & objMod.ApprovalHistoryId & " And ApprovalHistoryDetail.StageId > 0 "
            NoOfLevels = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            If NoOfLevels = objModel.Level Then
                str = " Update ApprovalHistory Set Status = '" & objMod.Status & "' where  ApprovalHistoryId = " & objMod.ApprovalHistoryId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                UpdateDocument(objMod.ReferenceType, objMod.DocumentNo, objMod.ReferenceId)
            Else
                str = " Update ApprovalHistoryDetail Set Status = 'WaitingForApproval' where  Level = " & objModel.Level + 1 & " And AprovalHistoryId = " & objMod.ApprovalHistoryId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Public Function UpdateDocument(ByVal Reference As String, ByVal DocumentNo As String, Optional ByVal ReferenceId As Integer = 0) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            If Reference = EnumReferenceType.PurchaseDemand.ToString Then
                str = " Update PurchaseDemandMasterTable Set Post = 1 , Posted_UserName = '" & LoginUserName & "', PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where PurchaseDemandNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ElseIf Reference = EnumReferenceType.Purchase.ToString Then
                str = " Update ReceivingMasterTable Set Post = 1 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where ReceivingNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ElseIf Reference = EnumReferenceType.PurchaseOrder.ToString Then
                str = " Update PurchaseOrderMasterTable Set Post = 1 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where PurchaseOrderNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ElseIf Reference = EnumReferenceType.PurchaseReturn.ToString Then
                str = " Update PurchaseReturnMasterTable Set Post = 1 , Posted_UserName = '" & LoginUserName & "', PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where PurchaseReturnNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ElseIf Reference = EnumReferenceType.PurchaseInquiry.ToString Then
                str = " Update PurchaseInquiryMaster Set Posted = 1 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where PurchaseInquiryNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ElseIf Reference = EnumReferenceType.GRN.ToString Then
                str = " Update ReceivingNoteMasterTable Set Post = 1 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where ReceivingNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ElseIf Reference = EnumReferenceType.VendorQuotation.ToString Then
                str = " Update VendorQuotationMaster Set Posted = 1 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where VendorQuotationNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.ActivityFeedBack.ToString Then
                str = " Update ActivityFeedback Set Approval = 1  where  ActivityFeedbackId = " & ReferenceId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.VoucherEntry.ToString Then
                str = " Update tblVoucher Set Post = 1 , Posted_UserName = '" & LoginUserName & "' , Posting_date = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Checked = 1 , CheckedByUser = '" & LoginUserName & "'  where voucher_no = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.Expense.ToString Then
                str = " Update tblVoucher Set Post = 1 , Posted_UserName = '" & LoginUserName & "' , Posting_date = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Checked = 1 , CheckedByUser = '" & LoginUserName & "'  where voucher_no = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.Payment.ToString Then
                str = " Update tblVoucher Set Post = 1 , Posted_UserName = '" & LoginUserName & "' , Posting_date = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Checked = 1 , CheckedByUser = '" & LoginUserName & "'  where voucher_no = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.Receipt.ToString Then
                str = " Update tblVoucher Set Post = 1 , Posted_UserName = '" & LoginUserName & "' , Posting_date = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Checked = 1 , CheckedByUser = '" & LoginUserName & "'  where voucher_no = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.SalesReturn.ToString Then
                str = " Update SalesReturnMasterTable Set Post = 1 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where SalesReturnNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.DeliveryChallan.ToString Then
                str = " Update deliverychalanmastertable Set Post = 1 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Delivered = 1 where DeliveryNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.SalesOrder.ToString Then
                str = " Update SalesOrderMasterTable Set Posted = 1 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where SalesOrderNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                str = ""
                str = " Update tbldefCostCenter Set Active = 1 WHERE Name = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.SalesQuotation.ToString Then
                str = " Update QuotationMasterTable Set Posted = 1 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Apprved = 1   where QuotationNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.Sales.ToString Then
                str = " Update SalesMasterTable Set Post = 1 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Delivered = 1   where SalesNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.SalesInquiry.ToString Then
                str = " Update SalesInquiryMaster Set Post = 1 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "'  where SalesInquiryNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.CashRequest.ToString Then
                str = " Update CashRequestHead Set Approved = 1 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where RequestNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.EmployeeLoanRequest.ToString Then
                GetAllRecordsForEmployeeLoan(ReferenceId)
                ApproveLoan(ReferenceId)
            ElseIf Reference = EnumReferenceType.SalesInvoiceTransfer.ToString Then ''TFS4431
                str = " Update SalesMasterTable Set Post = 1 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Delivered = 1   where SalesNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    ''' <summary>
    ''' This Function Is Added to get the required Record for Employee Request Loan ,To made changes in Voucher Accordingly
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetAllRecordsForEmployeeLoan(ByVal RequestID As Integer)
        Try
            Dim strSQL As String = String.Empty

            strSQL = "select tblDefEmployee.Employee_Name as Name,tblDefEmployee.Employee_Code as Code, IsNull(tblDefEmployee.CostCentre, 0) As CostCentre, tblDefCostCenter.Name AS [Cost Center Name], AdvanceRequestTable.RequestID, " _
                & " AdvanceRequestTable.RequestNo, AdvanceRequestTable.RequestDate,AdvanceRequestTable.EmployeeID, IsNull(AdvanceRequestTable.AdvanceAmount,0) as RequestedAmount,IsNull(AdvanceRequestTable.ApprovedAmount,0) as ApprovedAmount,AdvanceRequestTable.Loan_Details as Description, IsNull(AdvanceRequestTable.Advance_TypeID, 0) AS AdvanceTypeId, AdvanceType.Title AS [Advance Type], ISNULL(AdvanceType.SalaryDeduct, 0) AS SalaryDeduct " _
                & " from AdvanceRequestTable inner Join tblDefEmployee on AdvanceRequestTable.EmployeeID = tblDefEmployee.Employee_ID LEFT JOIN tblDefCostCenter ON AdvanceRequestTable.CostCentre = tblDefCostCenter.CostCenterID LEFT OUTER JOIN tblDefAdvancesType AS AdvanceType ON  AdvanceRequestTable.Advance_TypeID = AdvanceType.Id " _
                & " where AdvanceRequestTable.RequestID = " & RequestID & " "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            Me.grdLoandRequests.DataSource = dt
            Me.grdLoandRequests.RetrieveStructure()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Approve Loan Request
    Private Function ApproveLoan(ByVal ReqID As Integer) As Boolean
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As OleDbTransaction = objCon.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = objTrans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text
            Dim strSQL As String = String.Empty

            'Update AdvanceRequestTable table with Approved Status for Loan Request
            strSQL = "Update AdvanceRequestTable set RequestStatus = 'Approved', ApprovedAmount = " & Val(Me.grdLoandRequests.CurrentRow.Cells("ApprovedAmount").Value.ToString) & ", UserId = " & LoginUserId & ", UserName = '" & LoginUserName & "', ApprovedDate = '" & GettingServerDate().ToString("dd/MMM/yyyy") & "' where RequestID = " & ReqID
            cmd.CommandText = String.Empty
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()


            'Get Receiveable Account Id of Employee
            ''
            If CBool(Me.grdLoandRequests.CurrentRow.Cells("SalaryDeduct").Value) = True Then
                Dim RecvId As Integer = 0I
                strSQL = String.Empty
                strSQL = "select IsNull(ReceiveableAccountId,0) as RecvId from tblDefEmployee where Employee_ID = " & Me.grdLoandRequests.CurrentRow.Cells("EmployeeID").Value.ToString
                cmd.CommandText = String.Empty
                cmd.CommandText = strSQL
                RecvId = Convert.ToInt32(cmd.ExecuteScalar())
                'Get Salary Account Id of Employee
                Dim AccId As Integer = 0I
                strSQL = String.Empty
                strSQL = "select IsNull(EmpSalaryAccountId,0) as AccId from tblDefEmployee where Employee_ID = " & Me.grdLoandRequests.CurrentRow.Cells("EmployeeID").Value.ToString
                cmd.CommandText = String.Empty
                cmd.CommandText = strSQL
                AccId = Convert.ToInt32(cmd.ExecuteScalar())

                'Saving Voucher Of Loan Request in Voucher Master Table
                Dim VoucherID As Integer = 0I
                strSQL = String.Empty
                strSQL = "Insert into tblVoucher(voucher_no,location_id,voucher_type_id,voucher_date,Employee_Id,Source, Remarks, Reference, post, Posted_UserName, UserName, Checked, CheckedByUser, Posting_date)" _
                    & " Values(N'" & Me.grdLoandRequests.CurrentRow.Cells("RequestNo").Value.ToString & "'," _
                    & " 1,1," _
                    & " N'" & Me.grdLoandRequests.CurrentRow.Cells("RequestDate").Value.ToString & "'," _
                    & " " & Val(Me.grdLoandRequests.CurrentRow.Cells("EmployeeID").Value.ToString) & "," _
                    & " N'" & Me.Name.ToString & "'," _
                    & " N'" & Me.grdLoandRequests.CurrentRow.Cells("Description").Value.ToString & "' , 'Employee loan of amount " & IIf(Val(Me.grdLoandRequests.CurrentRow.Cells("ApprovedAmount").Value.ToString) > 0, Val(Me.grdLoandRequests.CurrentRow.Cells("ApprovedAmount").Value.ToString), Val(Me.grdLoandRequests.CurrentRow.Cells("RequestedAmount").Value.ToString)) & " is approved against request number " & grdLoandRequests.CurrentRow.Cells("RequestNo").Value.ToString & " for " & grdLoandRequests.CurrentRow.Cells("Name").Value & " [" & grdLoandRequests.CurrentRow.Cells("Code").Value.ToString & "] by " & LoginUserName & "', " _
                    & " 1, '" & LoginUserName & "', '" & LoginUserName & "', 1, '" & LoginUserName & "', getdate() ) " _
                    & " SELECT @@IDENTITY"

                cmd.CommandText = String.Empty
                cmd.CommandText = strSQL
                VoucherID = Convert.ToInt32(cmd.ExecuteScalar())

                'Deleting Voucher From Voucher Detail Table if Exist already
                strSQL = String.Empty
                strSQL = "Delete from tblVoucherDetail where voucher_id = " & VoucherID
                cmd.CommandText = String.Empty
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()

                'Saving Voucher Details Of Loan Request in Voucher Detail Table (Debit aginst Receiveable Accont Id )
                strSQL = String.Empty
                strSQL = "Insert into tblVoucherDetail(Voucher_id,location_id,coa_detail_id,debit_amount, credit_amount, comments,EmpID, CostCenterID)" _
                    & " Values(" & VoucherID & ",1," _
                    & " " & RecvId & "," _
                    & " " & IIf(Val(Me.grdLoandRequests.CurrentRow.Cells("ApprovedAmount").Value.ToString) > 0, Val(Me.grdLoandRequests.CurrentRow.Cells("ApprovedAmount").Value.ToString), Val(Me.grdLoandRequests.CurrentRow.Cells("RequestedAmount").Value.ToString)) & ", " _
                    & " null, " _
                    & " 'Loan approved for " & grdLoandRequests.CurrentRow.Cells("Name").Value & " [" & grdLoandRequests.CurrentRow.Cells("Code").Value.ToString & "] (Details :" & grdLoandRequests.CurrentRow.Cells("Description").Value.ToString & ") '," _
                    & " " & Val(Me.grdLoandRequests.CurrentRow.Cells("EmployeeID").Value.ToString) & ", " & Val(Me.grdLoandRequests.CurrentRow.Cells("CostCentre").Value.ToString) & ")"
                cmd.CommandText = String.Empty
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()

                'Saving Voucher Details Of Loan Request in Voucher Detail Table (Credit aginst Salary Accont Id )
                strSQL = String.Empty
                strSQL = "Insert into tblVoucherDetail(Voucher_id,location_id,coa_detail_id,debit_amount, credit_amount, comments,EmpID, CostCenterID)" _
                    & " Values(" & VoucherID & ",1," _
                    & " " & AccId & "," _
                    & " null, " _
                    & " " & IIf(Val(Me.grdLoandRequests.CurrentRow.Cells("ApprovedAmount").Value.ToString) > 0, Val(Me.grdLoandRequests.CurrentRow.Cells("ApprovedAmount").Value.ToString), Val(Me.grdLoandRequests.CurrentRow.Cells("RequestedAmount").Value.ToString)) & "," _
                    & " 'Loan approved for " & grdLoandRequests.CurrentRow.Cells("Name").Value & " [" & grdLoandRequests.CurrentRow.Cells("Code").Value.ToString & "] (Details :" & grdLoandRequests.CurrentRow.Cells("Description").Value.ToString & ") '," _
                    & " " & Val(Me.grdLoandRequests.CurrentRow.Cells("EmployeeID").Value.ToString) & ", " & Val(Me.grdLoandRequests.CurrentRow.Cells("CostCentre").Value.ToString) & ")"
                cmd.CommandText = String.Empty
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()
            End If
            objTrans.Commit()
            Return True

        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    'Reject Loan Request
    Private Function RejectLoan(ByVal ReqID As Integer) As Boolean
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As OleDbTransaction = objCon.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = objTrans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            Dim strSQL As String = String.Empty
            strSQL = "Update AdvanceRequestTable set RequestStatus = 'Reject'  where RequestID = " & ReqID
            cmd.CommandText = String.Empty
            cmd.CommandText = strSQL

            cmd.ExecuteNonQuery()

            objTrans.Commit()
            Return True
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function
    Public Function UpdateDocumentForRejection(ByVal Reference As String, ByVal DocumentNo As String, Optional ByVal ReferenceId As Integer = 0) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            If Reference = EnumReferenceType.PurchaseDemand.ToString Then
                str = " Update PurchaseDemandMasterTable Set Post = 0 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where PurchaseDemandNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ElseIf Reference = EnumReferenceType.Purchase.ToString Then
                str = " Update ReceivingMasterTable Set Post = 0 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where ReceivingNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ElseIf Reference = EnumReferenceType.PurchaseOrder.ToString Then
                str = " Update PurchaseOrderMasterTable Set Post = 0 , Posted_UserName = '" & LoginUserName & "', PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where PurchaseOrderNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ElseIf Reference = EnumReferenceType.PurchaseReturn.ToString Then
                str = " Update PurchaseReturnMasterTable Set Post = 0 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where PurchaseReturnNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ElseIf Reference = EnumReferenceType.PurchaseInquiry.ToString Then
                str = " Update PurchaseInquiryMaster Set Posted = 0 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where PurchaseInquiryNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ElseIf Reference = EnumReferenceType.GRN.ToString Then
                str = " Update ReceivingNoteMasterTable Set Post = 0 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where ReceivingNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ElseIf Reference = EnumReferenceType.VendorQuotation.ToString Then
                str = " Update VendorQuotationMaster Set Posted = 0 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "' where VendorQuotationNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ElseIf Reference = EnumReferenceType.ActivityFeedBack.ToString Then
                str = " Update ActivityFeedback Set Approval = 0 where  ActivityFeedbackId = " & ReferenceId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.VoucherEntry.ToString Then
                str = " Update tblVoucher Set Post = 0 , Posted_UserName = '" & LoginUserName & "' , Posting_date = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Checked = 0 , CheckedByUser = '" & LoginUserName & "'  where voucher_no = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.Expense.ToString Then
                str = " Update tblVoucher Set Post = 0 , Posted_UserName = '" & LoginUserName & "' , Posting_date = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Checked = 0 , CheckedByUser = '" & LoginUserName & "'  where voucher_no = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.Payment.ToString Then
                str = " Update tblVoucher Set Post = 0 , Posted_UserName = '" & LoginUserName & "' , Posting_date = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Checked = 0 , CheckedByUser = '" & LoginUserName & "'  where voucher_no = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.Receipt.ToString Then
                str = " Update tblVoucher Set Post = 0 , Posted_UserName = '" & LoginUserName & "' , Posting_date = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Checked = 0 , CheckedByUser = '" & LoginUserName & "'  where voucher_no = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)


            ElseIf Reference = EnumReferenceType.SalesReturn.ToString Then
                str = " Update SalesReturnMasterTable Set Post = 0 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "'  where SalesReturnNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.DeliveryChallan.ToString Then
                str = " Update deliverychalanmastertable Set Post = 0 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Delivered = 0  where DeliveryNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.SalesOrder.ToString Then
                str = " Update SalesOrderMasterTable Set Posted = 0 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "'  where SalesOrderNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.SalesQuotation.ToString Then
                str = " Update QuotationMasterTable Set Posted = 0 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Apprved = 0  where QuotationNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.Sales.ToString Then
                str = " Update SalesMasterTable Set Post = 0 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Delivered = 0  where SalesNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.SalesInquiry.ToString Then
                str = " Update SalesInquiryMaster Set Post = 0 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "'  where SalesInquiryNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.CashRequest.ToString Then
                str = " Update CashRequestHead Set Approved = 0 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "'  where RequestNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ElseIf Reference = EnumReferenceType.EmployeeLoanRequest.ToString Then
                GetAllRecordsForEmployeeLoan(ReferenceId)
                RejectLoan(ReferenceId)
            ElseIf Reference = EnumReferenceType.SalesInvoiceTransfer.ToString Then
                str = "select Change_Customer_Code from SalesMasterTable where SalesNo ='" & DocumentNo & "'"
                Dim coadetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                str = " Update tblVoucherDetail Set coa_detail_id = " & coadetailId & " WHERE voucher_id in(select voucher_id from tblVoucher Where Voucher_No='" & DocumentNo & "') AND coa_detail_id in (select CustomerCode from SalesMasterTable where SalesNo ='" & DocumentNo & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                str = " Update SalesMasterTable Set Post = 1 , Posted_UserName = '" & LoginUserName & "' , PostedDate = '" & DateTime.Now.ToString("yyyy-M-d h:mm:ss tt") & "', Delivered = 0 , CustomerCode = Change_Customer_Code   where SalesNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                str = " Update SalesMasterTable Set Change_Customer_Code = NULL where SalesNo = '" & DocumentNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    ''' <summary>
    ''' This Function is made to get if Higher Approved or rejected Record Exist in Approval Process of a document
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>TFS3227 : Ayesha Rehman : 14-05-2018 </remarks>
    Private Function ValidateHigherHierarchy(ByVal ApprovalHistoryId As Integer, ByVal Level As Integer) As Boolean
        Try
            Dim str As String = ""
            str = "Select ApprovalHistoryDetail.Status from ApprovalHistoryDetail Left outer join ApprovalHistory on ApprovalHistoryDetail.AprovalHistoryDetailId = ApprovalHistory.ApprovalHistoryId " _
                   & " where ApprovalHistoryDetail.AprovalHistoryId = " & ApprovalHistoryId & " And ApprovalHistoryDetail.Level = " & Level + 1 & " "
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("Status").ToString = "Approved" Or dt.Rows(0).Item("Status").ToString = "Rejected" Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        Try

            Call ShowApprovalLog()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage("Error occured while showing Record: " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub frmApprovalLog_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()

            Me.dtpFromDate.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpToDate.Value = Date.Now
            Me.cmbVtype.SelectedIndex = 0
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage("An error occured while loading record" & Chr(10) & ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdLog_RowCheckStateChanged(sender As Object, e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs) Handles grdLog.RowCheckStateChanged
        Try
            Dim CheckedRows As Integer = 0I
            CheckedRows = Convert.ToInt32(Me.grdLog.GetCheckedRows.Length)    'Getting count of checked rows from grdLoandRequests 

            If Me.grdLog.RowCount = 0 Then
                Me.btnApproveRequest.Visible = False
                Me.btnApproveRequest.Text = "Approve"
                Me.btnRejectRequest.Visible = False
                Me.btnRejectRequest.Text = "Reject"
                'When all rows checked
            ElseIf Me.grdLog.RowCount = CheckedRows Then
                Me.btnApproveRequest.Visible = True
                Me.btnApproveRequest.Text = "Approve(All)"
                Me.btnRejectRequest.Visible = True
                Me.btnRejectRequest.Text = "Reject(All)"
                'when one row checked
            ElseIf CheckedRows > 0 Then
                Me.btnApproveRequest.Visible = True
                Me.btnApproveRequest.Text = "Approve(" & CheckedRows & ")"
                Me.btnRejectRequest.Visible = True
                Me.btnRejectRequest.Text = "Reject(" & CheckedRows & ")"
            Else
                Me.btnApproveRequest.Visible = False
                Me.btnRejectRequest.Visible = False
                Me.btnApproveRequest.Text = "Approve"
                Me.btnRejectRequest.Text = "Reject"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRejectRequest_Click(sender As Object, e As EventArgs) Handles btnRejectRequest.Click
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As OleDbTransaction = objCon.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = objTrans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text
            Dim strSQL As String = String.Empty

            'Checked rows int grdLog, Reject all Documents
            If Not msg_Confirm("Do You Want to Reject") = True Then Exit Sub
            frmApprovalRejectionDetail.BringToFront()
            frmApprovalRejectionDetail.Text = ApprovalHistory.DocumentNo
            frmApprovalRejectionDetail.ShowDialog()
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdLog.GetCheckedRows
                ApprovalHistory = New ApprovalHistoryBE
                ApprovalHistoryDetail = New ApprovalHistoryDetailBE
                ApprovalHistory.ApprovalHistoryId = Val(r.Cells(enmgrd.ApprovalHistoryId).Value.ToString)
                ApprovalHistory.ReferenceType = r.Cells(enmgrd.ReferenceType).Value.ToString
                ApprovalHistory.DocumentNo = r.Cells(enmgrd.DocumentNo).Value.ToString
                ApprovalHistory.Source = r.Cells(enmgrd.Source).Value.ToString
                ApprovalHistory.ReferenceId = r.Cells(enmgrd.ReferenceId).Value.ToString
                ApprovalHistoryDetail.AprovalHistoryDetailId = Val(r.Cells(enmgrd.AprovalHistoryDetailId).Value.ToString)
                ApprovalHistoryDetail.Level = Val(r.Cells(enmgrd.Level).Value.ToString)
                If frmApprovalRejectionDetail.DialogResult = Windows.Forms.DialogResult.OK Then
                    ApprovalHistoryDetail.Status = "Rejected"
                    ApprovalHistory.Status = "Rejected"
                    ApprovalHistoryDetail.ApprovalRejectionReason = frmApprovalRejectionDetail.RejectionRemarks
                    ApprovalHistoryDetail.ApprovalRejectionReasonId = frmApprovalRejectionDetail.RejectionId
                    If RdoApproved.Checked = True Then
                        If ValidateHigherHierarchy(ApprovalHistory.ApprovalHistoryId, ApprovalHistoryDetail.Level) = False Then
                            UpdateApprovalHistoryForRejection(ApprovalHistoryDetail, ApprovalHistory)
                        Else
                            ShowErrorMessage("Document can not be Rejected , higher herarchey dependency")
                            Exit Sub
                        End If
                    Else
                        UpdateApprovalHistoryForRejection(ApprovalHistoryDetail, ApprovalHistory)
                    End If
                End If
            Next

            Me.btnApproveRequest.Visible = False
            Me.btnRejectRequest.Visible = False


            objTrans.Commit()

            Call ShowApprovalLog()
        Catch ex As Exception
            objTrans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            objCon.Close()
        End Try
    End Sub

    Public Function GetUsersEmail(ByVal ApprovalHistoryId As Integer, ByVal Level As Integer) As DataTable
        Try
            Dim QUERY As String = "SELECT User_ID, User_Name , Email FROM tblUser WHERE GroupId IN (SELECT GroupId FROM ApprovalUsersGroup INNER JOIN ApprovalHistoryDetail ON ApprovalUsersGroup.ApprovalHistoryDetailId = ApprovalHistoryDetail.AprovalHistoryDetailId WHERE ApprovalHistoryDetail.AprovalHistoryId = " & ApprovalHistoryId & " AND ApprovalHistoryDetail.Level = " & Level + 1 & ") "
            Dim DT As DataTable = GetDataTable(QUERY)
            Return DT
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub SendAutoEmail(Optional ByVal Activity As String = "")
        Try
            GetTemplate("Approval Log")
            If EmailTemplate.Length > 0 Then
                GetAutoEmailData()
                'GetVendorsEmails() ''Commented Against TFS3239
                FormatStringBuilder(dtEmail)
                For Each _Email As String In UsersEmail
                    VendorEmails = "" & _Email & ""
                    If Not VendorEmails = "" Then
                        CreateOutLookMail(AutoEmail)
                        SaveEmailLog(DocumentNo, VendorEmails, "frmApprovalLog", Activity)
                        VendorEmails = String.Empty
                    End If
                Next
                'SaveCCBCC(CC, BCC)
                'CC = ""
                'BCC = ""
            Else
                msg_Error("No email template is found for Approval Log.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetTemplate(ByVal Title As String)
        Dim Fields As String = String.Empty

        Try
            dtEmail = New DataTable
            'GetEmailData()
            EmailTemplate = EmailDAL.GetTemplate(Title)
            If EmailTemplate.Length > 0 Then
                Dim i, j As Integer
                i = EmailTemplate.IndexOf("<Fields>") + "<Fields>".Length
                j = EmailTemplate.IndexOf("</Fields>") - i

                Dim Searched As String = "</Fields>"
                AfterFieldsElement = EmailTemplate.Substring(EmailTemplate.IndexOf(Searched) + Searched.Length)
                Fields = EmailTemplate.Substring(i, j)
                Dim WOAtTheRate As String = Fields.Replace("@", "")
                Dim WOSpace As String = WOAtTheRate.Replace(" ", "")
                Dim IndexOfFieldElement As Integer = EmailTemplate.IndexOf("<Fields>")
                If IndexOfFieldElement > 0 Then
                    EmailTemplate = EmailTemplate.Remove(IndexOfFieldElement)
                End If
                'EmailTemplate = EmailTemplate.Remove(i, j)
                AllFields = New List(Of String)

                dtEmail.Columns.Clear()
                For Each word As String In WOSpace.Split(",")
                    Dim TrimSpace As String = word.Trim()
                    If Me.grdLog.RootTable.Columns.Contains(TrimSpace) Then
                        If dtEmail.Columns.Contains(TrimSpace) = False Then
                            dtEmail.Columns.Add(TrimSpace)
                        End If
                        If AllFields.Contains(TrimSpace) = False Then
                            AllFields.Add(TrimSpace)
                        End If
                        'Else
                        '    msg_Error("'" & TrimSpace & "'column does not exist")
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetAutoEmailData()
        'dtEmail = New DataTable
        Dim Dr As DataRow
        Try
            'For Each Row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows
            Dr = dtEmail.NewRow
            For Each col As String In AllFields
                If Me.grdLog.GetRow.Table.Columns.Contains(col) Then
                    'Dr.Item(col) = Row.Cells(col).Value.ToString
                    Dr.Item(col) = Me.grdLog.GetRow.Cells(col).Value.ToString
                End If
            Next
            dtEmail.Rows.Add(Dr)
            'Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FormatStringBuilder(ByVal dt As DataTable)
        Try
            html = New StringBuilder
            'Dim dt As DataTable = Me.GetData()

            'Building an HTML string.
            'Dim html As New StringBuilder()

            'Table start.
            'html.Append()
            html.Append(EmailTemplate)
            html.Append("<table border = '1'>")

            'Building the Header row.
            html.Append("<tr>")
            For Each column As DataColumn In dt.Columns
                Dim ColumnName As String = ""
                Dim Pattern = "([a-z?])[_ ]?([A-Z])"
                If column.ColumnName = "SerialNo" Then
                    ColumnName = "Sr#"
                Else
                    ColumnName = Regex.Replace(column.ColumnName, Pattern, "$1 $2")
                End If
                html.Append("<th>")
                html.Append(ColumnName)
                html.Append("</th>")
            Next
            html.Append("</tr>")
            'string[] textLines = text.Split(new[]{ Environment.NewLine }, StringSplitOptions.None);
            'var result = input.Split(System.Environment.NewLine.ToCharArray());
            'Building the Data rows.
            For Each row As DataRow In dt.Rows
                html.Append("<tr>")
                For Each column As DataColumn In dt.Columns
                    html.Append("<td>")
                    If column.ColumnName = "RequirementDescription" Then
                        Dim var = row(column.ColumnName).ToString.Split(System.Environment.NewLine.ToCharArray())
                        Dim Lines As String = ""
                        For Each Line As String In var
                            'Dim Line1 As String = Line.Replace(" ", "")
                            If Line.Length > 0 Then
                                If Lines.Length > 0 Then
                                    Lines += "<br/>" & Line
                                Else
                                    Lines = Line
                                End If
                            End If
                        Next
                        html.Append(Lines)
                    Else
                        html.Append(row(column.ColumnName))
                    End If
                    html.Append("</td>")
                Next
                html.Append("</tr>")
            Next

            'Table end.
            html.Append("</table>")
            html.Append(AfterFieldsElement)
            'html.Append("</body>")
            'html.Append("</html>")

            '     'Append the HTML string to Placeholder.
            'PlaceHolder1.Controls.Add(New Literal() With { _
            '   .Text = html.ToString() _


            'sb.Append("<table border=1 cellspacing=1 cellpadding=1><thead>")
            'For colIndx As Integer = 0 To colIndx < dt.Columns.Count
            '    sb.Append("<th>")
            '    sb.Append(dt.Columns(colIndx).ColumnName)
            '    sb.Append("</th>")
            'Next
            'sb.Append("</thead>")
            ''//add data rows to html table
            'For rowIndx As Integer = 0 To rowIndx < dt.Rows.Count Step 1
            '    sb.Append("<tr>")
            '    For colIndx As Integer = 0 To colIndx < dt.Columns.Count Step 1
            '        sb.Append("<td>")
            '        dt.Rows(rowIndx)(colIndx).ToString()
            '        sb.Append("</td>")
            '    Next
            '    sb.Append("</tr>")
            'Next
            'sb.Append("</table>")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMail(Optional ByVal _AutoEmail As Boolean = False)
        Try
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            mailItem.Subject = DocumentNo
            mailItem.To = VendorEmails
            'VendorEmails = String.Empty
            'Dim dtAttachments As DataTable = MasterDAL.GetAttachments(Me.Name, PurchaseInquiryId)
            'If dtAttachments.Rows.Count > 0 Then
            '    For Each Row As DataRow In dtAttachments.Rows
            '        mailItem.Attachments.Add(Row.Item("Path").ToString & "\" & Row.Item("FileName").ToString, Outlook.OlAttachmentType.olEmbeddeditem, Nothing, "Picture")
            '    Next
            'End If
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            'mailItem.
            If _AutoEmail = False Then
                mailItem.Display(mailItem)
                mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
                EmailBody = html.ToString

            Else
                mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
                EmailBody = html.ToString
                mailItem.Send()
            End If
            mailItem = Nothing
            oApp = Nothing

            '     mailItem.Display(mailItem);
            'mailItem.HTMLBody = body + mailItem.HTMLBody;
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class