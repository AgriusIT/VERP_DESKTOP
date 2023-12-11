'' 10-Dec-2013 By Imran Ali Tax on Purchase order
'' 14-Dec-2013 ReqId-928  Imran Ali            Add Comments column on Item Detail  in purchase order 
''16-Dec-2013 R933   Imran Ali           Slow working save/update in transaction forms
''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
''19-Dec-2013 R:945 , TaskId:2338        M Ijaz Javed           By default pack rupees
''25-Dec-2013 R:913           Imran Ali           Add Purchase order posting rights
''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
''28-Dec-2013 RM6 Imran Ali            Release 2.1.0.0 Bug
''2-Jan-2014   Tsk:2367    Imran Ali             Calculation Problem
''11-Jan-2014 Task:2373         Imran Ali                Add Columns SubSub Title in Account List on Sales/Purchase
''11-Jan-2014 Task:2374           Imran Ali                Net Amount Sales/Purchase 
''23-Jan-2014 TASK:2392              Imran Ali                 Duplicate PO History
''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
''03-Feb-2014        Task:2406   Imran Ali    FIELD CHOOSER restriction (Senior Rozgar)
''15-Feb-2014 Task:2426 Imran Ali Payment Schedule On Sales Demand And Purchase Demand
''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
''28-Feb-2014  TASK:24445 Imran Ali   Last purchase and sale price show on sale order and purchase order
''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
''20-May-2014 TASK:2637 Imran Ali All account Chek on Purcase and purchase return.
'' 6-June-2014 TASK:2673 Imran Ali CMFA Document Developement (Ravi)
''18-June-2014 TASK:2695 Imran Ali CMFA Load On Purchase Demand
''11-Jul-2014 Task:2734 IMRAN ALI Ehancement CMFA Document
''16-Jul-2014 TASK:2744 Imran Ali Problem Facing Record not in grid and cmfa Document Attachment On CMFA Document (Ravi)
''16-Jul-2014 TASK:2746 Imran Ali Cash Request Less On PO Validation (Ravi)
''24-Jul-2014 Task:2759 Imran ali Amount Round on all transaction forms
''27-Jul-2014 Task:2762 Imran Ali Total Amount Rounding configuration
''25-Sep-2014 Task:2856 Imran Ali Change scenario of cmfa on purchase (ravi) 
'2015-02-20 Changes Against Task# 7 Ali Ansari Add PackQty in Selection 
'2015-06-07 Task# 2015060004 Load Sales Order and transfer values Ali Ansari 
'2015-06-08 Task# 2015060006 Save Attachements Ali Ansari
'12-jun-2015 Task#A1 12-06-2015 Ahmad Sharif: Add Check on grdSaved to check on double click if row less than zero than exit
'12-jun-2015 Task#A2 12-06-2015 Ahmad Sharif: Check Vendor exist in combox list or not
'06-07-2015 Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
'16-Sep-2015 Task#16092015 Ahmad Sharif: Load Companies and Locations user wise
''19-9-2015 TASK24 Imran Ali: UOM Field In Grid Detail.
'10-11-2015 TSK-1115-00031 Muhammad Ameen:	SAME ITEM WITH DIFFRENT TIME SELECTION IN SAME DEMAND LOAD ONLY LAST ITEM QTY ON PO	purchase demand load in po same as we saved and mentain its stauts
''TASK: TFS1055 done by Ameen on 10-07-2017 Cost > Center will not change when user click on new button after saving purchase demand.
'' TASK TFS1592 Ayesha Rehman on 19-10-2017 Future date entry should be rights based
'' TASK TFS2375 Ayesha Rehman on 19-02-2018 Checked by Options on all document sreens of Purchase
''TFS2988 Ayesha Rehman : 09-04-2018 : If document is approved from one stage then it should not change able for previous stage
''TFS2989 Ayesha Rehman : 10-04-2018 : If document is rejected from one stage then it should open for previous stage for approval
''TFS4161 Ayesha Rehman : 09-08-2018 : P QTY: (Should Be Static/ Un-Changeable / Un-Editable on All Screens)
''TFS4689 Ayesha Rehman : 03-10-2018 : Show only relevant Accounts on Transactional screens while User wise COA Configuration.
Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports SBUtility.Utility
Imports System.Text
Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions
Public Class frmPurchaseDemand
    ' Change on 23-11-2013  For Multiple Print Vouchers
    Dim dt As DataTable
    Dim Mode As String = "Normal"
    Dim IsEditMode As Boolean = False
    Dim IsFormLoaded As Boolean = False
    Dim Email As Email
    Dim SourceFile As String = String.Empty
    Dim FileName As String = String.Empty
    Dim setVoucherNo As String = String.Empty
    Dim getVoucher_Id As Integer = 0
    Dim Total_Amount As Double = 0D
    Dim setEditMode As Boolean = False
    Dim crpt As New ReportDocument
    Dim Previouse_Amount As Double = 0D
    Dim PrintLog As PrintLogBE
    Dim flgCompanyRights As Boolean = False
    Dim flgCurrenyonOpenLC As Boolean = False
    ''TFS2375 : Ayesha Rehman : This Variable is Added to check ApprovalProcessId ,if it is mapped against the document
    Dim ApprovalProcessId As Integer = 0
    ''TFS2375 : Ayesha Rehman :End
    'Code Edit for task 1592 future date rights
    Dim IsDateChangeAllowed As Boolean = False
    Dim taxamnt As Double = 0D     ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
    'Marked Against Task#2015060001 Ali Ansari
    'Dim arrfile As String
    'Marked Against Task#2015060001 Ali Ansari
    'Altered Against Task#2015060001 Ali Ansari
    ' Convert string into List of string for making proper count
    Dim arrFile As List(Of String)
    Dim NotificationDAL As New NotificationTemplatesDAL
    Dim IsPackQtyDisabled As Boolean = False ''TFS4161
    'Altered Against Task#2015060001 Ali Ansari
    '' ReqId-899 Added New Enum TaxPercent, TaxAmount
    Dim ItemFilterByName As Boolean = False
    'Changes added by Murtaza for email generation on save (11/14/2022)
    Dim EmailDAL As New EmailTemplateDAL
    Dim EmailTemplate As String = String.Empty
    Dim UsersEmail As List(Of String)
    Dim dtEmail As DataTable
    Dim AfterFieldsElement As String = String.Empty
    Dim AllFields As List(Of String)
    Dim PurchaseDemandNo As String
    Dim EmailBody As String = String.Empty
    Dim PurchaseDemandId As Integer
    Dim html As StringBuilder

    'Changes added by Murtaza for email generation on save (11/14/2022)
    Enum GrdEnum
        'Category
        LocationId
        ArticleCode
        Item
        'TASK24 Added Index
        Color
        Size
        UOM
        Unit
        'End Task24
        Qty
        CategoryId
        ItemId
        PackQty
        CurrentPrice
        Pack_Desc
        Comments
        DeliveredQty
        PurchaseDemandDetailId
        TotalQty
        SalesOrderDetailId
    End Enum

    Private Sub frmPurchaseDemandNew_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 9 -1-14
            If e.KeyCode = Keys.F4 Then
                If BtnSave.Enabled = True Then
                    SaveToolStripButton_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If

            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                PrintToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.F5 Then
                BtnRefresh_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Insert Then
                btnAdd_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.U AndAlso e.Alt Then
                If Me.BtnSave.Text = "&Update" Then
                    If Me.BtnSave.Enabled = False Then
                        RemoveHandler Me.BtnSave.Click, AddressOf Me.SaveToolStripButton_Click
                    End If
                End If
            End If
            If e.KeyCode = Keys.D AndAlso e.Alt Then
                If Me.BtnSave.Text = "&Delete" Then
                    If Me.BtnDelete.Enabled = False Then
                        RemoveHandler Me.BtnDelete.Click, AddressOf Me.DeleteToolStripButton_Click
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmPurchaseDemandNew_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try


            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14 

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            If Not getConfigValueByType("CurrencyonOpenLC").ToString = "Error" Then
                flgCurrenyonOpenLC = Convert.ToBoolean(getConfigValueByType("CurrencyonOpenLC").ToString)
            End If
            ''start TFs4161
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            If Not getConfigValueByType("PurchaseDiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("PurchaseDiablePackQuantity").ToString)
            End If
            ''End TFS4161
            ' ''TASK TFS4544
            'If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            '    ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            'End If
            ' ''END TFS4544
            FillCombo("Category")
            'FillCombo("Item")
            FillCombo("Vendor")
            'FillCombo("Currency")
            'FillCombo("LC") 'R933 Call LC List
            FillCombo("CostCenter")
            FillCombo("Ticket")
            '2015060004 Fill Combo Ali Ansari
            FillCombo("SOrder")
            '2015060004 Fill Combo Ali Ansari
            'FillCombo("CMFADoc") 'Task:2673 Call DropDown function for CMFA Document
            'FillCombo("ArticlePack") 'R933 Commented
            'FillCombo("LC")
           
            RefreshControls()
            'Me.cmbVendor.Focus()
            'Me.DisplayRecord() R933 Commented History Data
            IsFormLoaded = True
            Get_All(frmModProperty.Tags)

            'TFS3360
            UltraDropDownSearching(cmbVendor, frmMain.blnListSeachStartWith, frmMain.blnListSeachContains)
            UltraDropDownSearching(cmbItem, frmMain.blnListSeachStartWith, frmMain.blnListSeachContains)



            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            If frmModProperty.Tags.Length > 0 Then frmModProperty.Tags = String.Empty ''18-Feb-2014 Task:2429 Imran Ali 1-error in payable/receivable tracing
        End Try
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub
    Private Sub DisplayRecord(Optional ByVal strCondition As String = "")
        Dim ClosingDate As DateTime = Convert.ToDateTime(getConfigValueByType("EndOfDate").ToString)
        Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
        Dim str As String = String.Empty
        'str = "SELECT     Recv.PurchaseDemandNo, CONVERT(varchar, Recv.PurchaseDemandDate, 103) AS Date, V.VendorName, Recv.PurchaseDemandQty, Recv.PurchaseDemandAmount, " _
        '       & " Recv.PurchaseDemandId, Recv.VendorCode, Recv.Remarks, convert(varchar, Recv.CashPaid) as CashPaid FROM         dbo.PurchaseDemandMasterTable Recv INNER JOIN dbo.tblVendor V ON Recv.VendorCode = V.AccountId"
        If Mode = "Normal" Then
            'Before against request no. 913
            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseDemandNo, Recv.PurchaseDemandDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseDemandQty,  " & _
            '        "Recv.PurchaseDemandAmount, Recv.PurchaseDemandId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.Status, isnull(LCID,0) as LCId, tblLetterOfCredit.LcDoc_No, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, Recv.Receiving_Date " & _
            '        "FROM dbo.PurchaseDemandMasterTable Recv INNER JOIN  " & _
            '        "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblLetterOfCredit On tblLetterOfCredit.LCdoc_Id = Recv.LcId LEFT OUTER JOIN(Select DISTINCT PurchaseDemandId, LocationId From PurchaseDemandDetailTable) as Location On Location.PurchaseDemandId = Recv.PurchaseDemandId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseDemandNo WHERE Recv.PurchaseDemandNo IS NOT NULL " & _
            '        " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseDemandDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'Before against Task:2392 Duplicate PO History
            'R:913 Added Column Post
            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseDemandNo, Recv.PurchaseDemandDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseDemandQty,  " & _
            '       "Recv.PurchaseDemandAmount, Recv.PurchaseDemandId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.Status, isnull(LCID,0) as LCId, tblLetterOfCredit.LcDoc_No, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, Recv.Receiving_Date,Case When Isnull(Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post " & _
            '       "FROM dbo.PurchaseDemandMasterTable Recv INNER JOIN  " & _
            '       "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblLetterOfCredit On tblLetterOfCredit.LCdoc_Id = Recv.LcId LEFT OUTER JOIN(Select DISTINCT PurchaseDemandId, LocationId From PurchaseDemandDetailTable) as Location On Location.PurchaseDemandId = Recv.PurchaseDemandId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseDemandNo WHERE Recv.PurchaseDemandNo IS NOT NULL " & _
            '       " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseDemandDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'End R:913

            ''23-Jan-2014 TASK:2392              Imran Ali                 Duplicate PO History
            'Before against task:2673
            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseDemandNo, Recv.PurchaseDemandDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseDemandQty,  " & _
            '      "Recv.PurchaseDemandAmount, Recv.PurchaseDemandId, Recv.VendorId, Recv.Remarks, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.Status, isnull(LCID,0) as LCId, tblLetterOfCredit.LcDoc_No, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, Recv.Receiving_Date,Case When Isnull(Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post " & _
            '      "FROM dbo.PurchaseDemandMasterTable Recv INNER JOIN  " & _
            '      "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblLetterOfCredit On tblLetterOfCredit.LCdoc_Id = Recv.LcId LEFT OUTER JOIN(Select DISTINCT PurchaseDemandId From PurchaseDemandDetailTable) as Location On Location.PurchaseDemandId = Recv.PurchaseDemandId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseDemandNo WHERE Recv.PurchaseDemandNo IS NOT NULL " & _
            '      " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseDemandDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'End Task:2392
            'Task:2673 Added Field RefCMFADocId
            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseDemandNo, Recv.PurchaseDemandDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseDemandQty,  " & _
            '      "Recv.PurchaseDemandAmount, Recv.PurchaseDemandId, Recv.VendorId, Recv.Remarks, Recv.Terms_And_Condition as Terms, CONVERT(varchar, Recv.CashPaid) AS CashPaid, Recv.Status, isnull(LCID,0) as LCId, tblLetterOfCredit.LcDoc_No, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(Recv.CurrencyType,0) as CurrencyType, IsNull(Recv.CurrencyRate,0) as CurrencyRate, Recv.Receiving_Date,Case When Isnull(Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post, IsNull(Recv.RefCMFADocId,0) as RefCMFADocId, (CMFAMasterTable.DocNo + '~' + Convert(Varchar,CMFAMasterTable.DocDate,102)) [CMFA Doc No]" & _
            '      "FROM dbo.PurchaseDemandMasterTable Recv INNER JOIN  " & _
            '      "dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN tblLetterOfCredit On tblLetterOfCredit.LCdoc_Id = Recv.LcId LEFT OUTER JOIN CMFAMasterTable On CMFAMasterTable.DocId = Recv.RefCMFADocId LEFT OUTER JOIN(Select DISTINCT PurchaseDemandId From PurchaseDemandDetailTable) as Location On Location.PurchaseDemandId = Recv.PurchaseDemandId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseDemandNo WHERE Recv.PurchaseDemandNo IS NOT NULL " & _
            '      " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseDemandDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'End Task:2673
            'Marked Against Task#2015060004
            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseDemandNo, Recv.PurchaseDemandDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseDemandQty,  " & _
            '                  " Recv.PurchaseDemandId, Recv.VendorId, Recv.Remarks, Recv.Status,CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],Case When Isnull(Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post, dbo.vwCOADetail.Contact_Email as Email,Recv.LocationId,IsNull(Recv.CostCenterId,0) as CostCenterId, Recv.UserName,recv.salesorderid " & _
            '                  " FROM dbo.PurchaseDemandMasterTable Recv LEFT OUTER JOIN  " & _
            '                  " dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select DISTINCT PurchaseDemandId From PurchaseDemandDetailTable) as Location On Location.PurchaseDemandId = Recv.PurchaseDemandId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseDemandNo WHERE Recv.PurchaseDemandNo IS NOT NULL " & _
            '                  " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseDemandDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'Marked Against Task#2015060004
            'Altered against Tasl#201506004 add SalesOrder no in query
            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseDemandNo, Recv.PurchaseDemandDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseDemandQty,  " & _
            '                  " Recv.PurchaseDemandId, Recv.VendorId, Recv.Remarks, Recv.Status,CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],Case When Isnull(Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post, dbo.vwCOADetail.Contact_Email as Email,Recv.LocationId,IsNull(Recv.CostCenterId,0) as CostCenterId, Recv.UserName,IsNull(Sales.salesorderno,0) as Salesorderno " & _
            '                  " FROM dbo.PurchaseDemandMasterTable Recv LEFT OUTER JOIN  " & _
            '                  " dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select DISTINCT PurchaseDemandId From PurchaseDemandDetailTable) as Location On Location.PurchaseDemandId = Recv.PurchaseDemandId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseDemandNo left join dbo.SalesOrderMasterTable Sales on Sales.Salesorderid = Recv.salesorderid  WHERE Recv.PurchaseDemandNo IS NOT NULL " & _
            '                  " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseDemandDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms

            'str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseDemandNo, Recv.PurchaseDemandDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseDemandQty,  " & _
            '                             " Recv.PurchaseDemandId, Recv.VendorId, Recv.Remarks, Recv.Status,CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],Case When Isnull(Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post, dbo.vwCOADetail.Contact_Email as Email,Recv.LocationId,IsNull(Recv.CostCenterId,0) as CostCenterId, Recv.UserName,IsNull(Sales.salesorderno,0) as Salesorderno, IsNull([No Of Attachment],0) as [No Of Attachment] " & _
            '                             " FROM dbo.PurchaseDemandMasterTable Recv LEFT OUTER JOIN  " & _
            '                             " dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select DISTINCT PurchaseDemandId From PurchaseDemandDetailTable) as Location On Location.PurchaseDemandId = Recv.PurchaseDemandId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseDemandNo left join dbo.SalesOrderMasterTable Sales on Sales.Salesorderid = Recv.salesorderid  " & _
            '                             "LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.PurchaseDemandId WHERE Recv.PurchaseDemandNo IS NOT NULL " & _
            '                             " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseDemandDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'Marked Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            'Altered against Tasl#201506004 add SalesOrder no in query
            'Altered against Task#201506006 add attachement
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            str = "SELECT  " & IIf(strCondition.ToString = "All", "", "Top 50") & "  Recv.PurchaseDemandNo, Recv.PurchaseDemandDate AS Date, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseDemandQty,  " & _
                             " Recv.PurchaseDemandId, Recv.VendorId, Recv.Remarks, Recv.Status,CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status],Case When Isnull(Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post, dbo.vwCOADetail.Contact_Email as Email,Recv.LocationId,IsNull(Recv.CostCenterId,0) as CostCenterId, Recv.UserName,IsNull(Sales.salesorderno,0) as Salesorderno, IsNull([No Of Attachment],0) as [No Of Attachment] ,Recv.UserName as 'User Name',Recv.UpdateUserName as [Modified By], Sales.SalesOrderId  " & _
                             " FROM dbo.PurchaseDemandMasterTable Recv LEFT OUTER JOIN  " & _
                             " dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select DISTINCT PurchaseDemandId From PurchaseDemandDetailTable) as Location On Location.PurchaseDemandId = Recv.PurchaseDemandId LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = Recv.PurchaseDemandNo left join dbo.SalesOrderMasterTable Sales on Sales.Salesorderid = Recv.salesorderid  " & _
                             "LEFT OUTER JOIN(Select Count(*) as [No Of Attachment], DocId From DocumentAttachment WHERE Source=N'" & Me.Name & "' Group By DocId) Att On Att.DocId=  Recv.PurchaseDemandId WHERE Recv.PurchaseDemandNo IS NOT NULL " & _
                             " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, Recv.PurchaseDemandDate, 102) > Convert(Datetime, N'" & ClosingDate & "', 102))") & " "
            'Altered against Task#201506006 add attachement
            'Altered Against Task#201507010 Ali Ansari to add user name field in Grid of all transactions forms
            If flgCompanyRights = True Then
                str += " AND Recv.LocationId=" & MyCompanyId
            End If
            If Me.dtpFrom.Checked = True Then
                str += " AND Recv.PurchaseDemandDate >= Convert(Datetime, N'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
            End If
            If Me.dtpTo.Checked = True Then
                str += " AND Recv.PurchaseDemandDate <= Convert(Datetime, N'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
            End If
            If Me.txtSearchDocNo.Text <> String.Empty Then
                str += " AND Recv.PurchaseDemandNo LIKE '%" & Me.txtSearchDocNo.Text & "%'"
            End If
            If Not Me.cmbSearchLocation.SelectedIndex = -1 Then
                If Me.cmbSearchLocation.SelectedIndex > 0 Then
                    str += " AND Location.LocationId=" & Me.cmbSearchLocation.SelectedValue
                End If
            End If
            If Not LoginGroup = "Administrator" Then
                str += " AND Recv.UserName='" & LoginUserName.ToUpper & "'"
            End If

            'If Me.txtFromAmount.Text <> String.Empty Then
            '    If Val(Me.txtFromAmount.Text) > 0 Then
            '        str += " AND Recv.PurchaseDemandAmount >= " & Val(Me.txtFromAmount.Text) & " "
            '    End If
            'End If
            'If Me.txtToAmount.Text <> String.Empty Then
            '    If Val(Me.txtToAmount.Text) > 0 Then
            '        str += " AND Recv.PurchaseDemandAmount <= " & Val(Me.txtToAmount.Text) & ""
            '    End If
            'End If
            If Me.cmbSearchAccount.ActiveRow IsNot Nothing Then
                If Me.cmbSearchAccount.SelectedRow.Cells(0).Value <> 0 Then
                    str += " AND Recv.VendorId = " & Me.cmbSearchAccount.Value
                End If
            End If
            If Me.txtSearchRemarks.Text <> String.Empty Then
                str += " AND Recv.Remarks LIKE '%" & Me.txtSearchRemarks.Text & "%'"
            End If
            'If Me.txtPurchaseNo.Text <> String.Empty Then
            '    str += " AND Recv.PurchaseDemandNo LIKE  '%" & Me.txtPurchaseNo.Text & "%'"
            'End If
            str += " ORDER BY Recv.PurchaseDemandNo DESC"
        End If
        FillGridEx(grdSaved, str, True)
        ' Change on 23-11-2013  For Multiple Print Vouchers
        Me.grdSaved.RootTable.Columns.Add("Column1")
        Me.grdSaved.RootTable.Columns("Column1").UseHeaderSelector = True
        Me.grdSaved.RootTable.Columns("Column1").ActAsSelector = True
        '-----------------------------------------------'
        'grdSaved.Columns(10).Visible = False
        grdSaved.RootTable.Columns(4).Visible = False
        grdSaved.RootTable.Columns(5).Visible = False
        'grdSaved.RootTable.Columns(7).Visible = False
        'grdSaved.RootTable.Columns(8).Visible = False
        'grdSaved.RootTable.Columns("LCId").Visible = False
        grdSaved.RootTable.Columns("LocationId").Visible = False
        Me.grdSaved.RootTable.Columns("CostCenterId").Visible = False
        Me.grdSaved.RootTable.Columns("SalesOrderId").Visible = False

        'grdSaved.RootTable.Columns("RefCMFADocId").Visible = False 'Task:2673 Set Hidden Column
        'grdSaved.RootTable.Columns("EmployeeCode").Visible = False
        'grdSaved.RootTable.Columns("PoId").Visible = False

        grdSaved.RootTable.Columns(0).Caption = "Doc No"
        grdSaved.RootTable.Columns(1).Caption = "Date"
        grdSaved.RootTable.Columns(2).Caption = "Vendor"
        'grdSaved.RootTable.Columns(3).Caption = "S-Demand"
        grdSaved.RootTable.Columns(3).Caption = "Qty"
        'grdSaved.RootTable.Columns(4).Caption = "Amount"
        grdSaved.RootTable.Columns("Remarks").Caption = "Remarks"
        grdSaved.RootTable.Columns("Status").Caption = "Status"
        'grdSaved.RootTable.Columns("LCDoc_No").Caption = "LC No"

        'grdSaved.RootTable.Columns("CurrencyType").Visible = False
        'grdSaved.RootTable.Columns("CurrencyRate").Visible = False
        'grdSaved.RootTable.Columns("Receiving_Date").Visible = False
        grdSaved.RootTable.Columns("Email").Visible = False
        grdSaved.RootTable.Columns("salesorderno").Visible = False
        'grdSaved.RootTable.Columns(8).HeaderText = "Employee"

        grdSaved.RootTable.Columns(0).Width = 100
        grdSaved.RootTable.Columns(1).Width = 100
        grdSaved.RootTable.Columns(2).Width = 250
        grdSaved.RootTable.Columns(3).Width = 50
        grdSaved.RootTable.Columns(5).Width = 80
        ' grdSaved.RootTable.Columns(8).Width = 100
        grdSaved.RootTable.Columns(4).Width = 150
        'grdSaved.RowHeadersVisible = False
        Me.grdSaved.RootTable.Columns("Date").FormatString = str_DisplayDateFormat
        Me.grdSaved.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
        'Task:2759 Set rounded amount format
        'Me.grdSaved.RootTable.Columns("PurchaseDemandAmount").FormatString = "N" & DecimalPointInValue
        'End Task

    End Sub

    Public Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Validate_AddToGrid() Then
            'If Not FindExistsItem(Me.cmbCategory.SelectedValue, Me.cmbItem.Value, Me.cmbUnit.Text, Val(txtPackQty.Text)) = True Then
            AddItemToGrid()
            'End If

            'AddItemToGrid()
            GetTotal()
            ClearDetailControls()
            cmbItem.Focus()
            'FillCombo("Item")
        End If
    End Sub
    Private Sub RefreshControls()
        'Task 1592 Ayesha Rehman Removing the ErrorProvider on btnNew
        ''TASK TFS4544
        If getConfigValueByType("ItemFilterByName").ToString = "True" Then
            ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
        End If
        ''END TFS4544
        FillCombo("Item")
        ErrorProvider1.Clear()
        IsEditMode = False
        txtPONo.Text = ""
        dtpPODate.Value = Now
        Me.dtpPODate.Enabled = True
        txtRemarks.Text = ""
        'rafay
        ''companyinitials = ""
        'rafay
        txtPaid.Text = ""
        'txtAmount.Text = ""
        'txtTotal.Text = "" 'Before ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
        'Me.txtTotal.Text = 0 'After ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
        'txtTotalQty.Text = ""
        txtBalance.Text = ""
        txtPackQty.Text = 1
        Me.txtQty.Text = 0 ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
        'Me.txtRate.Text = 0 ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
        Me.BtnSave.Text = "&Save"
        Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("PO", 6, "PurchaseDemandMasterTable", "PurchaseDemandNo")
        Me.cmbPo.Enabled = True
        'FillCombo("SO") 'R933 Commented
        'FillCombo("LC") R933 Commented
        cmbUnit.SelectedIndex = 0
        cmbVendor.Rows(0).Activate()
        cmbItem.Rows(0).Activate()
        '201506004
        If Not cmbSalesOrder.SelectedIndex = -1 Then Me.cmbSalesOrder.SelectedIndex = 0

        ''TASKZ:TFS1055
        If Not cmbProject.SelectedIndex = -1 Then Me.cmbProject.SelectedIndex = 0
        ''End TASKZ:TFS1055
        '201506004
        'Me.cmbLC.Rows(0).Activate()
        'Marked Against Task#2015060001
        'Array.Clear(arrFile, 0, arrFile.Length)
        'Marked Against Task#2015060001 Ali Ansari
        'Altered Against Task#2015060001 Ali Ansari
        'Clear arrfile
        arrFile = New List(Of String)
        'Altered Against Task#2015060001 Ali Ansari
        'DisplayDetail(-1)
        'Me.cmbVendor.Focus()
        DisplayDetail(-1)

        'grd.Rows.Clear()
        'Me.cmbVendor.Focus() comment against request no. RM6
        Me.GetSecurityRights()

        'Ayesha Rehman : TFS2375 : Enable Approval History button only in Eidt Mode
        If IsEditMode = True Then
            Me.btnApprovalHistory.Visible = True
            Me.btnApprovalHistory.Enabled = True
        Else
            Me.btnApprovalHistory.Visible = False
        End If
        'Ayesha Rehman : TFS2375 : End

        ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
        ApprovalProcessId = getConfigValueByType("PurchaseDemandApproval")
        If ApprovalProcessId = 0 Then
            Me.chkPost.Visible = True
            Me.chkPost.Enabled = True
           
        Else
            Me.chkPost.Visible = False
            Me.chkPost.Enabled = False
            Me.chkPost.Checked = False
        End If
        ''Ayesha Rehman :TFS2375 :End

        'FillComboByEdit() R933 Commented
        Me.dtpFrom.Value = Date.Now.AddMonths(-1)
        Me.dtpTo.Value = Date.Now
        Me.dtpFrom.Checked = False
        Me.dtpTo.Checked = False
        Me.txtSearchDocNo.Text = String.Empty
        'Me.cmbSearchLocation.SelectedIndex = 0
        'Me.txtFromAmount.Text = String.Empty
        'Me.txtToAmount.Text = String.Empty
        'Me.txtPurchaseNo.Text = String.Empty
        ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
        ' Before Me.txtTaxPercent.Text = String.Empty
        ' After
        'Me.txtTaxPercent.Text = 0
        '''''''''''''''''''''''''''''''''''''''''''''''
        'Me.cmbSearchAccount.Rows(0).Activate()
        Me.txtSearchRemarks.Text = String.Empty
        Me.SplitContainer1.Panel1Collapsed = True
        'DisplayRecord()
        Me.lblPrintStatus.Text = String.Empty
        'FillCombo("CMFADoc")
        'Me.txtCMFAAmount.Text = String.Empty
        'Me.txtPOAmountAgainstCMFA.Text = String.Empty
        'Me.txtCMFADiff.Text = String.Empty

        'Me.cmbCurrency.SelectedIndex = 0
        'Me.txtCurrencyRate.Text = String.Empty
        'If flgCurrenyonOpenLC = True Then
        '    Me.grpCurrency.Visible = True
        '    If Not Me.cmbCurrency.SelectedIndex = -1 Then Me.cmbCurrency.SelectedIndex = 1 ''19-Dec-2013 R:945 , TaskId:2338        M Ijaz Javed           By default pack rupees
        '    Me.txtCurrencyRate.Text = 1     ''19-Dec-2013 R:945 , TaskId:2338        M Ijaz Javed           By default pack rupees
        'Else
        '    Me.grpCurrency.Visible = False
        'End If
        'Me.txtTerms_And_Condition.Text = GetTermsCondition()

        ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
        '2015060004  Fill Combo box
        FillCombo("SOrder")
        '2015060004  Fill Combo box
        Me.BtnDelete.Visible = False
        Me.BtnPrint.Visible = False
        Me.BtnEdit.Visible = False
        '''''''''''''''''''''''''''
        'Clear Attached file records
        arrFile = New List(Of String)
        Me.btnAttachment.Text = "Attachment (" & arrFile.Count & ")"
        'Altered Against Task#2015060001 Ali Ansri
        'Array.Clear(arrFile, 0, arrFile.Length)

        Me.dtpPODate.Focus() 'RM6 Set Focus
    End Sub
    '' ReqId-899 Reset TaxPercent TextBox
    Private Sub ClearDetailControls()
        'cmbCategory.SelectedIndex = 0
        cmbUnit.SelectedIndex = 0
        txtQty.Text = 0
      'txtRate.Text = ""
        'Me.txtNetTotal.Text = 0  ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
        'Me.txtTaxPercent.Text = String.Empty ' Before ReqId-954
        'Me.txtTaxPercent.Text = 0 ' After ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
        'txtTotal.Text = 0
        txtPackQty.Text = 1
        txtTotalQuantity.Text = 0

        'Me.txtPackRate.Text = String.Empty
    End Sub
    Private Function Validate_AddToGrid() As Boolean
        If cmbItem.ActiveRow.Cells(0).Value <= 0 Then
            msg_Error("Please select an item")
            cmbItem.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        If Val(txtQty.Text) <= 0 Then
            msg_Error("Quantity is not greater than 0")
            txtQty.Focus() : Validate_AddToGrid = False : Exit Function
        End If
        If Val(txtTotalQuantity.Text) <= 0 Then
            msg_Error("Total Quantity more than 0 is required")
            txtTotalQuantity.Focus() : Validate_AddToGrid = False : Exit Function
        End If

        'If Val(txtRate.Text) < 0 Then
        '    msg_Error("Rate is not greater than 0")
        '    txtRate.Focus() : Validate_AddToGrid = False : Exit Function
        'End If

        Validate_AddToGrid = True
    End Function

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress, txtPackQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.LostFocus
        Try
            '' Before 27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            'Dim dblTaxValue As Double = 0D
            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1
            '    'txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
            '    dblTaxValue = ((Val(txtQty.Text) * Val(txtRate.Text)) * IIf(Val(txtTaxPercent.Text) <> 0, (Val(txtTaxPercent.Text) / 100), 0))
            '    txtTotal.Text = ((Val(txtQty.Text) * Val(txtRate.Text)) + dblTaxValue)
            'Else
            '    'txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
            '    dblTaxValue = (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) * IIf(Val(txtTaxPercent.Text) <> 0, (Val(txtTaxPercent.Text) / 100), 0))
            '    txtTotal.Text = (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + dblTaxValue)
            'End If

            '' After 27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            'Dim TaxValue As Double = 0D
            'Dim totalValue As Double = 0D
            'If Not Val(Me.txtPackQty.Text) > 1 Then
            '    Me.txtTotal.Text = Val(Me.txtQty.Text) * Val(Me.txtRate.Text)
            '    If Val(Me.txtTaxPercent.Text) > 0 Then
            '        TaxValue = (Val(Me.txtTotal.Text) * Val(Me.txtTaxPercent.Text)) / 100
            '        Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + TaxValue
            '    Else
            '        Me.txtNetTotal.Text = Val(Me.txtTotal.Text)
            '    End If
            'Else
            '    Me.txtTotal.Text = (Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text) * Val(Me.txtRate.Text))
            '    If Val(Me.txtTaxPercent.Text) > 0 Then
            '        TaxValue = (Val(Me.txtTotal.Text) * Val(Me.txtTaxPercent.Text)) / 100
            '        Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + TaxValue
            '    Else
            '        Me.txtNetTotal.Text = Val(Me.txtTotal.Text)
            '    End If
            'End If

            'Task:2367 Change Total
            'If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtQty.Text) <> 0 AndAlso Val(Me.txtRate.Text) = 0 Then
            '    Me.txtRate.Text = Val(Me.txtTotal.Text) / Val(Me.txtQty.Text)
            'Else
            '    If Val(Me.txtPackQty.Text) = 0 Then
            '        txtPackQty.Text = 1
            '        txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
            '    Else
            '        txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
            '    End If
            'End If

            'If Val(Me.txtQty.Text) <> 0 AndAlso Val(Me.txtTotal.Text) = 0 Then
            '    If Val(Me.txtPackQty.Text) = 0 Then
            '        txtPackQty.Text = 1
            '        txtTotal.Text = (Val(txtQty.Text) * Val(txtRate.Text)) + ((Val(txtQty.Text) * Val(txtRate.Text)))
            '    Else
            '        txtTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)))
            '    End If
            'End If

            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1
            '    txtNetTotal.Text = (Val(txtQty.Text) * Val(txtRate.Text)) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTaxPercent.Text)) / 100)
            'Else
            '    txtNetTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTaxPercent.Text)) / 100)
            'End If
            'End Task:2367
            If Val(Me.txtPackQty.Text) > 0 AndAlso Val(Me.txtQty.Text) > 0 Then
                Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            Else
                Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
    '    Try
    '        If Me.txtPackRate.Text.Length > 0 AndAlso (Me.txtPackRate.Text) > 0 Then
    '            If Me.cmbUnit.Text <> "Loose" Then
    '                Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub txtRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            '' Before 27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            'Dim dblTaxValue As Double = 0D
            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1
            '    dblTaxValue = ((Val(txtQty.Text) * Val(txtRate.Text)) * IIf(Val(txtTaxPercent.Text) <> 0, (Val(txtTaxPercent.Text) / 100), 0))
            '    txtTotal.Text = ((Val(txtQty.Text) * Val(txtRate.Text)) + dblTaxValue)
            'Else
            '    dblTaxValue = (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) * IIf(Val(txtTaxPercent.Text) <> 0, (Val(txtTaxPercent.Text) / 100), 0))
            '    txtTotal.Text = (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + dblTaxValue)
            'End If

            '' After 27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            'Dim dblTaxValue As Double = 0D
            'Dim totalValue As Double = 0D
            'If Val(Me.txtPackQty.Text) > 1 Then
            '    Me.txtTotal.Text = ((Val(txtPackQty.Text) * Val(txtQty.Text) * Val(Me.txtRate.Text)))
            '    If Val(Me.txtTaxPercent.Text) > 0 Then
            '        dblTaxValue = (Val(Me.txtTotal.Text) * Val(Me.txtTaxPercent.Text)) / 100
            '        Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + dblTaxValue
            '    Else
            '        Me.txtNetTotal.Text = Val(Me.txtTotal.Text)
            '    End If
            'Else
            '    Me.txtTotal.Text = (Val(txtQty.Text) * Val(Me.txtRate.Text))
            '    If Val(Me.txtTaxPercent.Text) > 0 Then
            '        dblTaxValue = (totalValue * Val(Me.txtTaxPercent.Text)) / 100
            '        Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + dblTaxValue
            '    Else
            '        Me.txtNetTotal.Text = Val(Me.txtTotal.Text)
            '    End If
            'End If
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'Task:2367 Change Total
            'If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtRate.Text) <> 0 AndAlso Val(Me.txtQty.Text) = 0 Then
            '    Me.txtQty.Text = Math.Round((Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)), 2)
            'Else
            '    If Val(Me.txtPackQty.Text) = 0 Then
            '        txtPackQty.Text = 1
            '        txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
            '    Else
            '        txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
            '    End If
            'End If

            'If Val(Me.txtQty.Text) <> 0 AndAlso Val(Me.txtTotal.Text) = 0 Then
            '    If Val(Me.txtPackQty.Text) = 0 Then
            '        txtPackQty.Text = 1
            '        txtTotal.Text = (Val(txtQty.Text) * Val(txtRate.Text)) + ((Val(txtQty.Text) * Val(txtRate.Text)))
            '    Else
            '        txtTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)))
            '    End If
            'End If

            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1
            '    txtNetTotal.Text = (Val(txtQty.Text) * Val(txtRate.Text)) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTaxPercent.Text)) / 100)
            'Else
            '    txtNetTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTaxPercent.Text)) / 100)
            'End If
            'End Task:2367

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        ''get the qty in case of pack unit
        If Me.cmbUnit.Text = "Loose" Then
            'txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
            txtPackQty.Text = 1
            Me.txtPackQty.Enabled = False
            Me.txtPackQty.TabStop = False
            Me.txtTotalQuantity.Enabled = False
        Else
            ''Start TFS4161
            If IsPackQtyDisabled = True Then
                Me.txtPackQty.Enabled = False
                Me.txtTotalQuantity.Enabled = False
            Else
                Me.txtPackQty.Enabled = True
                Me.txtTotalQuantity.Enabled = True
                Me.txtPackQty.TabStop = True
            End If
            ''End TFS4161
            'Dim objCommand As New OleDbCommand
            'Dim objCon As OleDbConnection
            'Dim objDataAdapter As New OleDbDataAdapter
            'Dim objDataSet As New DataSet

            'objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

            'If objCon.State = ConnectionState.Open Then objCon.Close()

            'objCon.Open()
            'objCommand.Connection = objCon
            'objCommand.CommandType = CommandType.Text


            'objCommand.CommandText = "Select PackQty from ArticleDefTable where ArticleID = " & cmbItem.ActiveRow.Cells(0).Value

            'txtPackQty.Text = objCommand.ExecuteScalar()
            If TypeOf Me.cmbUnit.SelectedItem Is DataRowView Then
                Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
            End If
            'txtTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text))


        End If

    End Sub
    Private Function FindExistsItem(LocationId As Integer, ByVal ArticleId As Integer, ByVal Pack As String, ByVal PackQty As Double) As Boolean 'TSK-1115-00031
        Try
            'Task:2432 Added Flg Marge Item

            Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
            Dim dr() As DataRow
            dr = dt.Select("LocationId=" & LocationId & " AND ArticleDefId=" & ArticleId & "  AND Unit='" & Pack & "' AND PackQty =" & Val(PackQty) & " ")
            If dr.Length > 0 Then
                For Each r As DataRow In dr
                    If dr(0).ItemArray(0) = r.ItemArray(0) AndAlso dr(0).ItemArray(9) = r.ItemArray(9) AndAlso dr(0).ItemArray(6) = r.ItemArray(6) AndAlso dr(0).ItemArray(10) = r.ItemArray(10) Then
                        r.BeginEdit()
                        r(7) = Val(dr(0).ItemArray(7)) + Val(Me.txtQty.Text)
                        r.EndEdit()
                    End If
                Next
                Return True
            Else
                Return False
            End If

            'End Task:2432
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '' ReqId-899 Added new field of TaxPercent and TaxAmount
    Public Sub AddItemToGrid()
        Try

            Dim dtGrd As DataTable
            dtGrd = CType(Me.grd.DataSource, DataTable)
            dtGrd.AcceptChanges()
            Dim drGrd As DataRow
            drGrd = dtGrd.NewRow
            'If dtGrd.Rows.Count > 0 Then
            '    Dim ArticleCode As String = drGrd.Item(GrdEnum.ArticleCode).ToString()
            '    If drGrd.Item(GrdEnum.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString AndAlso drGrd.Item(GrdEnum.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString AndAlso drGrd.Item(GrdEnum.Unit) = Me.cmbUnit.Text.ToString Then
            '        drGrd.Item(GrdEnum.Qty) += Me.txtQty.Text

            '    End If
            'End If
            'drGrd.Item(GrdEnum.Category) = Me.cmbCategory.Text
            drGrd.Item(GrdEnum.LocationId) = Me.cmbCategory.SelectedValue
            drGrd.Item(GrdEnum.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text.ToString
            drGrd.Item(GrdEnum.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text.ToString
            'TASK24 Add To Grid Color, Size, UOM 
            drGrd.Item(GrdEnum.Color) = Me.cmbItem.ActiveRow.Cells("Combination").Text.ToString
            drGrd.Item(GrdEnum.Size) = Me.cmbItem.ActiveRow.Cells("Size").Text.ToString
            drGrd.Item(GrdEnum.UOM) = Me.cmbItem.ActiveRow.Cells("UOM").Text.ToString
            'END TASK24
            drGrd.Item(GrdEnum.Unit) = IIf(Me.cmbUnit.Text.ToString <> "Loose", "Pack", Me.cmbUnit.Text.ToString)
            drGrd.Item(GrdEnum.Qty) = Me.txtQty.Text
            'drGrd.Item(GrdEnum.Rate) = Me.txtRate.Text
            ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            ' Before 
            'drGrd.Item(GrdEnum.Total) = Me.txtTotal.Text
            'After
            'drGrd.Item(GrdEnum.Total) = Val(Me.txtNetTotal.Text)
            ''''''''''''''''''''''''''''''
            'drGrd.Item(GrdEnum.TaxPercent) = Val(Me.txtTaxPercent.Text)
            'drGrd.Item(GrdEnum.TaxAmount) = Val(taxamnt)
            drGrd.Item(GrdEnum.CategoryId) = Me.cmbCategory.SelectedValue
            drGrd.Item(GrdEnum.ItemId) = Me.cmbItem.ActiveRow.Cells(0).Value
            drGrd.Item(GrdEnum.PackQty) = Val(Me.txtPackQty.Text)
            drGrd.Item(GrdEnum.CurrentPrice) = Me.cmbItem.ActiveRow.Cells("Price").Text
            'drGrd.Item(GrdEnum.PackPrice) = Val(Me.txtPackRate.Text)
            drGrd.Item(GrdEnum.Pack_Desc) = Me.cmbUnit.Text.ToString
            ''ReqId-928 Added Field of Comments
            drGrd.Item(GrdEnum.Comments) = String.Empty
            drGrd.Item(GrdEnum.TotalQty) = Val(Me.txtTotalQuantity.Text)
            '' End ReqId-928
            'dtGrd.Rows.InsertAt(drGrd, 0)
            dtGrd.Rows.Add(drGrd)
            dtGrd.AcceptChanges()

            'Task 3913 Saad Afzaal move scroll bar at the end when item added into the grid 
            grd.MoveLast()

        Catch ex As Exception
            Throw ex
        End Try


    End Sub
    Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged
        'If cmbCategory.SelectedIndex > 0 Then
        '    FillCombo("ItemFilter")
        'End If
        Try
            If IsFormLoaded = True Then FillCombo("Item")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetTotal()
        Try
            'Dim i As Integer
            'Dim dblTotalAmount As Double
            'Dim dblTotalQty As Double
            ''For i = 0 To grd.Rows.Count - 1
            'For i = 0 To grd.RowCount - 1
            '    dblTotalAmount = dblTotalAmount + Val(grd.GetRows(i).Cells(6).Value)
            '    dblTotalQty = dblTotalQty + Val(grd.GetRows(i).Cells(4).Value)
            'Next
            'txtAmount.Text = dblTotalAmount
            'txtTotalQty.Text = dblTotalQty
            'txtBalance.Text = Val(txtAmount.Text) - Val(txtPaid.Text)
            'Me.lblRecordCount.Text = "Total Records: " & Me.grd.RowCount
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillCombo(ByVal strCondition As String)
        Dim str As String
        If strCondition = "Item" Then
            'Marked Against Task# 7 Ali Ansari 
            'str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleSizeName as Size, ArticleColorName as Combination, ISNULL(PurchasePrice,0) as Price, ArticleDefView.SortOrder FROM  ArticleDefView where Active=1 "
            'Marked Against Task# 7 Ali Ansari 
            'Changes Against Task# 7 Ali Ansari Add PackQty in Selection 2015-02-20
            str = "SELECT ArticleId as Id, ArticleCode Code, ArticleDescription Item, ArticleColorName as Combination, ArticleSizeName as Size,ArticleUnitName as UOM,ArticleDefView.ArticleBrandName As Grade, PackQty as PackQty, ISNULL(PurchasePrice,0) as Price, ArticleDefView.SortOrder FROM  ArticleDefView where Active=1 "
            'Changes Against Task# 7 Ali Ansari Add PackQty in Selection 2015-02-20
            If flgCompanyRights = True Then
                str += " AND ArticleDefView.CompanyId=" & MyCompanyId
            End If
            If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                    str += " AND ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & Me.cmbCategory.SelectedValue & " AND Restricted=1)"
                    'Else
                    '    str += " ORDER BY ArticleDefView.SortOrder" Comment against task:2452
                End If
            End If
       

            ''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
            If ItemSortOrder = True Then
                str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByCode = True Then
                str += " ORDER BY ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByName = True Then
                str += " ORDER BY ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            Else
                str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            End If
            'End Task:2452



            'FillDropDown(cmbItem, str)
            FillUltraDropDown(Me.cmbItem, str)
            Me.cmbItem.Rows(0).Activate()
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                If ItemFilterByName = True Then
                    Me.rdoName.Checked = True
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                Else
                    If Me.rdoName.Checked = True Then
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                    Else
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                    End If
                End If
            End If

        
        ElseIf strCondition = "Category" Then
            'Task#16092015 Load  user wise locations
            'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
            '    str = "Select Location_Id, Location_Code from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order"
            'Else
            '    str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
            'End If

            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"

            FillDropDown(cmbCategory, str, False)

        ElseIf strCondition = "SearchLocation" Then
            str = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
            FillDropDown(Me.cmbSearchLocation, str, True)

        ElseIf strCondition = "ItemFilter" Then
            str = "SELECT  ArticleId as Id,  ArticleCode Code, ArticleDescription Item, ArticleColorName as Combination,ArticleSizeName as Size, ArticleUnitName as UOM,ArticleDefView.ArticleBrandName As Grade, PurchasePrice as Price FROM ArticleDefView where Active=1 and ArticleGroupID = " & cmbCategory.SelectedValue & ""
            If flgCompanyRights = True Then
                str += " AND ArticleDefView.CompanyId=" & MyCompanyId
            End If
            FillUltraDropDown(cmbItem, str)
            Me.cmbItem.Rows(0).Activate()
        ElseIf strCondition = "Vendor" Then
            'str = "SELECT     tblVendor.AccountId AS ID, tblVendor.VendorName AS Name, tblListTerritory.TerritoryName AS Territory, tblListCity.CityName AS City,  " & _
            '        "tblListState.StateName AS State, tblVendor.AccountId AS AcId " & _
            '        "FROM         tblListTerritory INNER JOIN " & _
            '        "tblListCity ON tblListTerritory.CityId = tblListCity.CityId INNER JOIN " & _
            '        "tblListState ON tblListCity.StateId = tblListState.StateId INNER JOIN " & _
            '        "tblVendor ON tblListTerritory.TerritoryId = tblVendor.Territory"
            'Before against task:2373
            'str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
            '                    "dbo.tblListTerritory.TerritoryName as Territory, tblVendor.Email, tblVendor.Phone " & _
            '                    "FROM dbo.tblVendor INNER JOIN " & _
            '                    "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
            '                    "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
            '                    "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                    "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '                    "WHERE (dbo.vwCOADetail.account_type = 'Vendor') "
            'Before gainst task:2637
            'Task:2373 Added Column Sub Sub Title
            'str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
            '                 "dbo.tblListTerritory.TerritoryName as Territory, tblVendor.Email, tblVendor.Phone, vwCOADetail.Sub_Sub_Title " & _
            '                 "FROM dbo.tblVendor INNER JOIN " & _
            '                 "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
            '                 "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
            '                 "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                 "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '                 "WHERE (dbo.vwCOADetail.account_type = 'Vendor') "
            'End Task:2373
            ''20-May-2014 TASK:2637 Imran Ali All account Chek on Purcase and purchase return.
            str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                          "dbo.tblListTerritory.TerritoryName as Territory, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                          "FROM dbo.tblVendor INNER JOIN " & _
                          "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                          "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                          "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                          "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id "
            If getConfigValueByType("Show Customer On Purchase") = "True" Then
                str += "WHERE  (dbo.vwCOADetail.account_type in('Vendor','Customer')) "
            Else
                str += "WHERE  (dbo.vwCOADetail.account_type = 'Vendor') "
            End If
            'End Task:2637
            If flgCompanyRights = True Then
                str += " AND vwCOADetail.CompanyId=" & MyCompanyId
            End If
            ''Start TFS3322 : Ayesha Rehman : 15-05-2018
            'If LoginGroup = "Administrator" Then
            If GetMappedUserId() > 0 And getGroupAccountsConfigforPurchase(Me.Name) And LoginGroup <> "Administrator" Then ''TFS4689
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                   "dbo.tblListTerritory.TerritoryName as Territory, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                   "FROM dbo.tblVendor INNER JOIN " & _
                   "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                   "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                   "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                   "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id WHERE  (dbo.vwCOADetail.detail_title Is Not NULL ) "
                str += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ")) "
                ''Start TFS4689 :  Ayesha Rehman : 02-10-2018
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    str += " And vwCOADetail.account_type in ('Customer' , 'Vendor') "
                Else
                    str += " And vwCOADetail.account_type = 'Vendor' "
                End If
                ''End TFS4689
            End If
            ''End TFS3322
            If IsEditMode = False Then
                str += " AND vwCOADetail.Active=1"
            Else
                str += " AND vwCOADetail.Active in(0,1,Null)"
            End If
            str += " order by tblVendor.Sortorder, vwCOADetail.detail_title "
            FillUltraDropDown(cmbVendor, str)
            If Me.cmbVendor.DisplayLayout.Bands.Count > 0 Then
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head" 'Task:2373 Change Caption
            End If
        ElseIf strCondition = "SearchVendor" Then
        'Before against task:2637
        'str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
        '                               "dbo.tblListTerritory.TerritoryName as Territory, tblVendor.Email, tblVendor.Phone " & _
        '                               "FROM  dbo.tblVendor INNER JOIN " & _
        '                               "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
        '                               "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
        '                               "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
        '                               "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
        '                               "WHERE  (dbo.vwCOADetail.account_type = 'Vendor') AND dbo.vwCOADetail.detail_title Is Not NULL "
        ''20-May-2014 TASK:2637 Imran Ali All account Chek on Purcase and purchase return.
        str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                                   "dbo.tblListTerritory.TerritoryName as Territory, dbo.vwCOADetail.Contact_Email as Email, dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile,vwCOADetail.Sub_Sub_Title " & _
                                                   "FROM  dbo.tblVendor INNER JOIN " & _
                                                   "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                                   "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                                   "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                                   "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id "
        If Not getConfigValueByType("Show Customer On Purchase") = "True" Then
            str += "WHERE  (dbo.vwCOADetail.account_type = 'Vendor') AND dbo.vwCOADetail.detail_title Is Not NULL "
        Else
            str += "WHERE  (dbo.vwCOADetail.account_type IN ('Vendor','Customer')) AND dbo.vwCOADetail.detail_title Is Not NULL "
        End If
        'End Task:2637
        If flgCompanyRights = True Then
            str += " AND vwCOADetail.CompanyId=" & MyCompanyId
        End If
        str += " order by tblVendor.Sortorder, vwCOADetail.detail_title"
        FillUltraDropDown(cmbSearchAccount, str)
        If Me.cmbSearchAccount.DisplayLayout.Bands.Count > 0 Then
            Me.cmbSearchAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
            Me.cmbSearchAccount.DisplayLayout.Bands(0).Columns("Email").Hidden = True
        End If
        '2015060004 Fill Combobox

        ElseIf strCondition = "SOrder" Then

        str = "Select Distinct a.SalesOrderId, SalesOrderNo from SalesOrderMasterTable a left join PurchaseDemandMasterTable b on a.salesorderid = b.salesorderid where a.salesorderid  is not null And a.Status ='Open' "
        str += IIf(IsEditMode, " And b.PurchaseDemandNo = '" & txtPONo.Text & "'", "")
        str += " order by a.SalesOrderId DESC"

        FillDropDown(cmbSalesOrder, str)

        '2015060004 Fill Combobox
        ElseIf strCondition = "SO" Then
        str = "Select SalesID, SalesNo from SalesMasterTable where SalesId not in(select PoId from salesReturnMasterTable)"
            FillDropDown(cmbPo, str)

        ElseIf strCondition = "Ticket" Then
            str = "select * from TicketMasterTable where Status = 'Open'"
            FillDropDown(cmbTicketNo, str)

        ElseIf strCondition = "SOComplete" Then
        str = "Select SalesID, SalesNo from SalesMasterTable "
        FillDropDown(cmbPo, str)
        ElseIf strCondition = "SM" Then
        str = "Select EmployeeID, EmployeeName  + ' - ' + employeeCode as EmployeeName from EmployeeDefTable"
        FillDropDown(Me.cmbSalesMan, str)
        ElseIf strCondition = "grdLocation" Then
        'Task#16092015 Load  user wise locations
        'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
        '    str = "Select Location_Id, Location_Name From tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")"
        'Else
        '    str = "Select Location_Id, Location_Name From tblDefLocation"
        'End If

        str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
               & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
               & " Else " _
               & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"

        Dim dt As DataTable = GetDataTable(str)
        Me.grd.RootTable.Columns(GrdEnum.LocationId).ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
        ElseIf strCondition = "LC" Then
        If IsEditMode = True Then
            str = "Select LCdoc_Id, LCdoc_No, LCdoc_Date, Bank, LCdoc_Type,CostCenter From tblLetterOfCredit WHERE VendorId=" & Me.cmbVendor.Value & ""
        Else
            str = "Select LCdoc_Id, LCdoc_No, LCdoc_Date, Bank, LCdoc_Type,CostCenter From tblLetterOfCredit WHERE Active=1 " & IIf(Me.cmbVendor.ActiveRow Is Nothing, "", " AND VendorId=" & Me.cmbVendor.Value & "") & ""
        End If
        'FillUltraDropDown(Me.cmbLC, str)
        'If Me.cmbLC.DisplayLayout.Bands.Count > 0 Then
        '    Me.cmbLC.DisplayLayout.Bands(0).Columns("LCdoc_Id").Hidden = True
        '    Me.cmbLC.DisplayLayout.Bands(0).Columns("CostCenter").Hidden = True
        '    Me.cmbLC.DisplayLayout.Bands(0).Columns("LCdoc_No").Header.Caption = "Doc No"
        '    Me.cmbLC.DisplayLayout.Bands(0).Columns("LCdoc_Date").Header.Caption = "Do Date"
        '    Me.cmbLC.DisplayLayout.Bands(0).Columns("LCdoc_Type").Header.Caption = "Type"
        'End If
        ElseIf strCondition = "Currency" Then
        'str = "Select currency_id, Currency_Code From tblCurrency"
        'FillDropDown(Me.cmbCurrency, str)
        ElseIf strCondition = "ArticlePack" Then
        Me.cmbUnit.ValueMember = "ArticlePackId"
        Me.cmbUnit.DisplayMember = "PackName"
        Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
        ''TASK:2673 Fill Dropdown for CMFA Document
        ElseIf strCondition = "CMFADoc" Then
        Dim strQuery As String = String.Empty
        'Before against task:2856
        'strQuery = "SELECT DISTINCT CMFA.DocId, CMFA.DocNo + '~' + CONVERT(Varchar, CONVERT(Varchar, CMFA.DocDate, 102)) AS DocNo, CMFADT.VendorId, CMFA.DocDate, COA.detail_code,COA.detail_title FROM dbo.CMFAMasterTable AS CMFA INNER JOIN dbo.CMFADetailTable AS CMFADT ON CMFA.DocId = CMFADT.DocId LEFT OUTER JOIN dbo.vwCOADetail AS COA ON CMFADT.VendorId = COA.coa_detail_id WHERE IsNull(Status,0)=1 AND IsNull(Approved,0)=1 AND CMFADT.VendorId=" & Me.cmbVendor.Value & ""
        'End Task:2673
        'Task:2856 changed query 
        strQuery = "SELECT CMFA.DocId, CMFA.DocNo + '~' + CONVERT(Varchar, CONVERT(Varchar, CMFA.DocDate, 102)) AS DocNo, IsNull(PO.POAmount,0) as POAmount, (IsNull(TotalAmount,0)+IsNull(Tax_Amount,0))  as CMFA_Amount FROM dbo.CMFAMasterTable  AS CMFA LEFT OUTER JOIN(Select DocId, SUM( (IsNull(Tax_Percent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) as Tax_Amount From CMFADetailTable Group By DocId) CMFA_Dt On CMFA_Dt.DocId = CMFA.DocId LEFT OUTER JOIN (Select IsNull(RefCMFADocId,0) as RefCMFADocId, SUM((IsNull(Qty,0)* IsNull(Price,0))+((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)* IsNull(Price,0)))) as [POAmount] From PurchaseDemandDetailTable PO_DT INNER JOIN PurchaseDemandMasterTable PO On PO.PurchaseDemandId = PO_DT.PurchaseDemandId WHERE RefCMFADocId <> 0 AND PO.PurchaseDemandId <> " & Val(Me.txtReceivingID.Text) & " Group By IsNull(RefCMFADocId,0)) PO On PO.RefCMFADocId = CMFA.DocId  WHERE IsNull(Approved,0)=1"
        'Task:2856
        'FillDropDown(Me.cmbCMFADoc, strQuery)
        ElseIf strCondition = "CostCenter" Then
            FillDropDown(Me.cmbProject, "Select * From tblDefCostCenter WHERE Active = 1 AND Contract = 1 ORDER BY Name ASC")



        End If
    End Sub
    Private Sub txtPaid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPaid.TextChanged
        Try
            txtBalance.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(txtPaid.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''ReqId-899 Added new field TaxPercent, TaxAmount
    Private Function Save() As Boolean
        'R:913
        If Me.chkPost.Visible = False Then
            Me.chkPost.Checked = False
        End If
        'End R:913
        'Task:2673 Validation CMFA Document
        Dim strCMFA As String = String.Empty
        Dim objDt As New DataTable
        'If Me.cmbCMFADoc.SelectedIndex > 0 Then
        '    strCMFA = "Select CMFADetailTable.ArticleDefId, SUM(IsNull(Sz1,0)) as Qty, SUM(IsNull(POQty,0)) as PoQty From CMFADetailTable WHERE DocId=" & Me.cmbCMFADoc.SelectedValue & " Group By CMFADetailTable.ArticleDefId Having SUM(IsNull(Sz1,0)-IsNull(POQty,0)) <> 0"
        '    objDt = GetDataTable(strCMFA)
        'End If
        'End Task:2673
        Me.grd.UpdateData()
        Me.txtPONo.Text = GetDocumentNo() 'GetNextDocNo("PO", 6, "PurchaseDemandMasterTable", "PurchaseDemandNo")
        setVoucherNo = Me.txtPONo.Text
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        gobjLocationId = MyCompanyId
        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            'objCon.BeginTransaction()
            'Before against request no. 913
            'objCommand.CommandText = "Insert into PurchaseDemandMasterTable (LocationId, PurchaseDemandNo,PurchaseDemandDate,VendorId,PurchaseDemandQty,PurchaseDemandAmount, CashPaid, Remarks,UserName, Status, LCID, CurrencyType, CurrencyRate, Receiving_Date, Terms_And_Condition) values( " _
            '                        & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.ToString.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & Val(Me.cmbLC.Value) & ", " & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & "," & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", " & IIf(Me.dtpReceivingDate.Checked = True, "N'" & Me.dtpReceivingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "') Select @@Identity"
            'R:913 Added Column Post
            objCommand.CommandText = ""
            ' Before Against Task:2673 
            'objCommand.CommandText = "Insert into PurchaseDemandMasterTable (LocationId, PurchaseDemandNo,PurchaseDemandDate,VendorId,PurchaseDemandQty,PurchaseDemandAmount, CashPaid, Remarks,UserName, Status, LCID, CurrencyType, CurrencyRate, Receiving_Date, Terms_And_Condition,Post) values( " _
            '                        & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & "," & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", " & Val(txtPaid.Text) & ",N'" & txtRemarks.Text.ToString.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & Val(Me.cmbLC.Value) & ", " & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & "," & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", " & IIf(Me.dtpReceivingDate.Checked = True, "N'" & Me.dtpReceivingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & ") Select @@Identity"
            'End R:913
            'Task:2673 Added Field RefCMFADocId
            'Marked Against Task@2015060004 
            'objCommand.CommandText = "Insert into PurchaseDemandMasterTable (LocationId, PurchaseDemandNo,PurchaseDemandDate,VendorId,PurchaseDemandQty,Remarks,UserName, Status,Post,CostCenterId) values( " _
            '                        & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",N'" & txtRemarks.Text.ToString.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & "," & Me.cmbProject.SelectedValue & ") Select @@Identity"
            'getVoucher_Id = objCommand.ExecuteScalar 'objCommand.ExecuteNonQuery()
            'Marked Against Task@2015060004 
            'Task#201506000 Add SalesOrderId in PurchaseDemandMasterTable to block salesorder no which were saved before
                objCommand.CommandText = "Insert into PurchaseDemandMasterTable (LocationId, PurchaseDemandNo,PurchaseDemandDate,VendorId,PurchaseDemandQty,Remarks,UserName, Status,Post,CostCenterId,salesorderid) values( " _
                                        & gobjLocationId & ", N'" & txtPONo.Text & "',N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & cmbVendor.ActiveRow.Cells(0).Value & ", " & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",N'" & txtRemarks.Text.ToString.Replace("'", "''") & "',N'" & LoginUserName & "', N'" & EnumStatus.Open.ToString & "', " & IIf(Me.chkPost.Checked = True, 1, 0) & "," & Me.cmbProject.SelectedValue & "," & Me.cmbSalesOrder.SelectedValue & ") Select @@Identity"
       
            getVoucher_Id = objCommand.ExecuteScalar 'objCommand.ExecuteNonQuery()

            objCommand.CommandText = "insert into tblDefCostCenter(Name,Code,sortorder, CostCenterGroup, Active, OutwardGatepass, DayShift, IsLogical, Amount, SOBudget, SalaryBudget, DepartmentBudget, Contract, PurchaseDemand) values(N'" & txtPONo.Text.Replace("'", "''") & "','" & txtPONo.Text.Replace("'", "''") & "','1', N'', 1, 0, 0, 0,0, 0, 0, 0, 0, 1)"
            objCommand.ExecuteNonQuery()
            'Task#201506000 Add SalesOrderId in PurchaseDemandMasterTable to block salesorder no which were saved before
            'End Task:2673
            'objCommand.CommandText = ""
            'Marked Against Task#2015060001 Ali Ansari
            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                SaveDocument(getVoucher_Id, Me.Name, trans)
            End If
            For i = 0 To grd.RowCount - 1
                '' ReqId-928 Added Column Comments in Query
                'Task:2673 Validate If PO Qty Exceeded From CMFA Qty
                'Dim objDR() As DataRow
                'If Me.cmbCMFADoc.SelectedIndex > 0 Then
                '    objDR = objDt.Select("ArticleDefId=" & Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString & "")
                '    If objDR IsNot Nothing Then
                '        If objDR.Length > 0 Then
                '            If Val(objDR(0).ItemArray(1).ToString) < (Val(objDR(0).Item(2).ToString) + Val(Me.grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString)) Then
                '                Throw New Exception(" [" & Me.grd.GetRows(i).Cells(GrdEnum.Item).Value.ToString & "] PO qty exceeded from CMFA qty.")
                '            End If
                '        End If
                '    End If
                'End If
                'End Task:2673
                objCommand.CommandText = ""
                objCommand.CommandText = "Insert into PurchaseDemandDetailTable(PurchaseDemandId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Sz7,CurrentPrice,Pack_Desc, Comments, SalesOrderDetailId) values( " _
                                        & " ident_current('PurchaseDemandMasterTable'), " & Val(grd.GetRows(i).Cells(GrdEnum.LocationId).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", " _
                                        & " " & Val(grd.GetRows(i).Cells(GrdEnum.TotalQty).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value) & "  , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value) & ", N'" & grd.GetRows(i).Cells(GrdEnum.Pack_Desc).Value.ToString.Replace("'", "''") & "',N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ") "
                '& " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value) & "  , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value) & ", N'" & grd.GetRows(i).Cells(GrdEnum.Pack_Desc).Value.ToString.Replace("'", "''") & "',N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "') "
                objCommand.ExecuteNonQuery()
                ''End ReqId-928
                'Val(grd.Rows(i).Cells(5).Value)


                '' 14-11-2016
                objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                               & " SET DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", DeliveredTotalQty= IsNull(DeliveredTotalQty,0) + " & Val(Me.grd.GetRows(i).Cells(GrdEnum.TotalQty).Value) & " " _
                                               & " WHERE     (SalesOrderID = " & IIf(Me.cmbSalesOrder.SelectedIndex = -1, 0, Me.cmbSalesOrder.SelectedValue) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value) & ") And (SalesOrderDetailId =" & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ")  "
                objCommand.ExecuteNonQuery()
                '' 14-11-2016


            Next



            If Me.cmbSalesOrder.SelectedIndex > 0 Then
                objCommand.CommandText = "Select IsNull(Sz1,0) as Qty , isnull(DeliveredQty , 0) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & Me.cmbSalesOrder.SelectedValue & ""
                Dim da As New OleDbDataAdapter(objCommand)
                Dim dt As New DataTable
                da.Fill(dt)
                Dim blnEqual As Boolean = True

                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
                            blnEqual = False
                            Exit For
                        End If
                    Next
                End If
                If blnEqual = True Then
                    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & Me.cmbSalesOrder.SelectedValue & ""
                    objCommand.ExecuteNonQuery()
                Else
                    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Me.cmbSalesOrder.SelectedValue & ""
                    objCommand.ExecuteNonQuery()
                End If
            End If

            'Task:2673 Update POQty And Set Status Open Or Close
            'If Me.cmbCMFADoc.SelectedIndex > 0 Then

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "UPDATE CMFADetailTable SET POQty = IsNull(a.Qty,0) " _
            '     & " FROM (SELECT dbo.PurchaseDemandMasterTable.VendorId, dbo.PurchaseDemandMasterTable.RefCMFADocId, SUM(dbo.PurchaseDemandDetailTable.Sz1) AS Qty, " _
            '     & " dbo.PurchaseDemandDetailTable.ArticleDefId,dbo.PurchaseDemandDetailTable.ArticleSize " _
            '     & " FROM dbo.PurchaseDemandDetailTable INNER JOIN " _
            '     & " dbo.PurchaseDemandMasterTable ON dbo.PurchaseDemandDetailTable.PurchaseDemandId = dbo.PurchaseDemandMasterTable.PurchaseDemandId " _
            '     & " WHERE(ISNULL(dbo.PurchaseDemandMasterTable.RefCMFADocId, 0)=" & Me.cmbCMFADoc.SelectedValue & ") " _
            '     & " GROUP BY dbo.PurchaseDemandMasterTable.VendorId, dbo.PurchaseDemandMasterTable.RefCMFADocId, dbo.PurchaseDemandDetailTable.ArticleDefId,dbo.PurchaseDemandDetailTable.ArticleSize) a " _
            '     & " WHERE " _
            '     & " a.ArticleDefId = CMFADetailTable.ArticleDefId " _
            '     & " AND CMFADetailTable.DocId = a.RefCMFADocId  " _
            '     & " AND CMFADetailTable.VendorId = a.VendorId " _
            '     & " AND CMFADetailTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
            '    objCommand.ExecuteNonQuery()

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "UPDATE CMFAMasterTable SET Status=CASE WHEN a.BalanceQty <=0 Then 0 ELSE 1 end From( " _
            '               & " SELECT dbo.CMFAMasterTable.DocId, SUM(ISNULL(dbo.CMFADetailTable.Sz1, 0) - ISNULL(dbo.CMFADetailTable.POQty, 0)) AS BalanceQty " _
            '               & " FROM dbo.CMFADetailTable INNER JOIN " _
            '               & " dbo.CMFAMasterTable ON dbo.CMFADetailTable.DocId = dbo.CMFAMasterTable.DocId WHERE dbo.CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & " " _
            '               & " GROUP BY dbo.CMFAMasterTable.DocId) a " _
            '               & " WHERE(a.DocId = CMFAMasterTable.DocId) AND dbo.CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
            '    objCommand.ExecuteNonQuery()

            'End If
            'End Task:2673

            trans.Commit()
            Save = True
            'InsertVoucher()
            setEditMode = False
            Total_Amount = 0D ';Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
            '2015060004 Reseting Combobox
            cmbSalesOrder.SelectedValue = 0
            '2015060004 Reseting Combobox
        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try

        ''insert Activity Log
        SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Purchase, Me.txtPONo.Text.Trim, True)

        ''Start TFS2375
        ''insert Approval Log
        SaveApprovalLog(EnumReferenceType.PurchaseDemand, getVoucher_Id, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Purchase Demand - " & cmbVendor.Text & "", Me.Name)
        ''End TFS2375
        SendSMS()
        Dim ValueTable As DataTable = GetSingle(getVoucher_Id)
        NotificationDAL.SaveAndSendNotification("Purchase Demand", "PurchaseDemandMasterTable", getVoucher_Id, ValueTable, "Purchase > Purchase Demand")
    End Function


    Sub InsertVoucher()
        Try
            SaveVoucherEntry(GetVoucherTypeId("SV"), GetNextDocNo("SV", 6, "tblVoucher", "voucher_no"), Me.dtpPODate.Value, "", Nothing, Val(Me.cmbVendor.ActiveRow.Cells(0).Text.ToString), getConfigValueByType("SalesCreditAccount"), Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum), Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum), "Both", Me.Name, Me.txtPONo.Text, True)
        Catch ex As Exception
            ShowErrorMessage("An error occured while saving voucher: " & ex.Message)
        End Try

    End Sub

    Private Function FormValidate() As Boolean

        If txtPONo.Text = "" Then
            msg_Error("Please enter PO No.")
            txtPONo.Focus() : FormValidate = False : Exit Function
        End If

        ''Task#A2 12-06-2015 Check Vendor exist in combox list or not
        If Not Me.cmbVendor.IsItemInList Then
            msg_Error("Please select Vendor")
            Me.cmbVendor.Focus() : FormValidate = False : Exit Function
        End If

        If Me.cmbVendor.Text = String.Empty Then
            msg_Error("Please select Vendor")
            Me.cmbVendor.Focus() : FormValidate = False : Exit Function
        End If
        ''End Task#A2 12-06-2015

        If cmbVendor.ActiveRow.Cells(0).Value < 0 Then
            msg_Error("Please select Vendor")
            cmbVendor.Focus() : FormValidate = False : Exit Function
        End If

        If Not Me.grd.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            cmbItem.Focus() : FormValidate = False : Exit Function
        End If
        If chkRefill.Checked = True Then

        Else
            If cmbTicketNo.SelectedValue = 0 Then
                msg_Error("Please select Ticket to Proceed")
                cmbTicketNo.Focus() : FormValidate = False : Exit Function
            End If
        End If
        ''Start TFS2988
        If IsEditMode = True Then
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim) Then
                If ValidateApprovalProcessInProgress(Me.txtPONo.Text.Trim) Then
                    msg_Error("Document Can Not be Changed ,because Approval Process is in Progress") : Return False : Exit Function
                End If
            End If
        End If
        ''End TFS2988

        'If ValidateCMFATotalAmount() = True Then
        '    Return True
        'Else
        '    Return False
        'End If

        'If Me.cmbCMFADoc.SelectedIndex > 0 Then
        '    If Val(Me.txtCMFAAmount.Text) < (Val(Me.txtPOAmountAgainstCMFA.Text) + Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) + Me.grd.GetTotal(Me.grd.RootTable.Columns("TaxAmount"), Janus.Windows.GridEX.AggregateFunction.Sum)) Then
        '        ShowErrorMessage("PO amount exceeded from CMFA amount.")
        '        Me.cmbCMFADoc.Focus()
        '        Return False
        '    End If
        'End If

        Return True

    End Function
    'Public Function ValidateCMFATotalAmount() As Boolean
    '    Try
    '        Dim strSQL As String = String.Empty
    '        Dim objDt As New DataTable
    '        If BtnSave.Text = "&Save" Then
    '            If Me.cmbCMFADoc.SelectedIndex > 0 Then
    '                'Before against task:2734
    '                'strSQL = "Select (SUM(IsNull(Qty,0)*IsNull(Price,0))) Amount From CMFADetailTable INNER JOIN CMFAMasterTable On CMFAMasterTable.DocId = CMFADetailTable.DocId  LEFT OUTER JOIN(Select RefCMFADocId, (IsNull(Qty,0)*IsNull(Price,0)) as POAmount From PurchaseDemandDetailTable INNER JOIN PurchaseDemandMasterTable On PurchaseDemandMasterTable.PurchaseDemandId = PurchaseDemandDetailTable.PurchaseDemandId WHERE RefCMFADocId=N'" & Me.cmbCMFADoc.SelectedValue & "') PO On PO.RefCMFADocId = CMFAMasterTable.DocId WHERE CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
    '                'Task:2734 Change Query
    '                'Task:2746 Add Cash Request Total Amount
    '                strSQL = "Select DocId, ISNULL(ProjectedExpAmount,0) as Amount, IsNull(PO.POAmount,0) as POAmount, IsNull(Req.Total_Amount,0) as Total_Amount From CMFAMasterTable LEFT OUTER JOIN(Select RefCMFADocId, Sum(IsNull(PurchaseDemandAmount,0)) as POAmount From PurchaseDemandMasterTable  WHERE RefCMFADocId=N'" & Me.cmbCMFADoc.SelectedValue & "' Group by RefCMFADocId) PO On PO.RefCMFADocId = CMFAMasterTable.DocId LEFT OUTER JOIN (SELECT CMFADocId, SUM(Total_Amount) AS Total_Amount FROM dbo.CashRequestHead WHERE CMFADocId=" & Me.cmbCMFADoc.SelectedValue & " AND IsNull(Approved,0)=1 GROUP BY CMFADocId) Req On Req.CMFADocId = CMFAMasterTable.DocId WHERE CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
    '                'End Task:2746
    '                'End Task:2734
    '            End If
    '        Else
    '            If Me.cmbCMFADoc.SelectedIndex > 0 Then
    '                'Before against task:2734
    '                'strSQL = "Select (SUM(IsNull(Qty,0)*IsNull(Price,0)))-SUM(IsNull(POAmount,0)) Amount From CMFADetailTable INNER JOIN CMFAMasterTable On CMFAMasterTable.DocId = CMFADetailTable.DocId LEFT OUTER JOIN(Select RefCMFADocId, (IsNull(Qty,0)*IsNull(Price,0)) as POAmount From PurchaseDemandDetailTable INNER JOIN PurchaseDemandMasterTable On PurchaseDemandMasterTable.PurchaseDemandId = PurchaseDemandDetailTable.PurchaseDemandId WHERE PurchaseDemandNo <> N'" & Me.txtPONo.Text & "') PO On PO.RefCMFADocId = CMFAMasterTable.DocId  WHERE CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
    '                'Task:2734 Change Query
    '                'Task:2746 Add Cash Request Total Amount
    '                strSQL = "Select DocId, ISNULL(ProjectedExpAmount,0)  as Amount,IsNull(PO.POAmount,0) as POAmount, IsNull(Req.Total_Amount,0) as Total_Amount  From CMFAMasterTable LEFT OUTER JOIN(Select RefCMFADocId, Sum(IsNull(PurchaseDemandAmount,0)) as POAmount From PurchaseDemandMasterTable  WHERE RefCMFADocId=N'" & Me.cmbCMFADoc.SelectedValue & "' AND PurchaseDemandNo <> N'" & Me.txtPONo.Text & "' Group by RefCMFADocId) PO On PO.RefCMFADocId = CMFAMasterTable.DocId LEFT OUTER JOIN (SELECT CMFADocId, SUM(Total_Amount) AS Total_Amount FROM dbo.CashRequestHead WHERE CMFADocId=" & Me.cmbCMFADoc.SelectedValue & " AND IsNull(Approved,0)=1 GROUP BY CMFADocId) Req On Req.CMFADocId = CMFAMasterTable.DocId WHERE CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
    '                'End Task:2746
    '                'End Task:2734
    '            End If
    '        End If
    '        If strSQL.Length > 0 Then
    '            objDt = GetDataTable(strSQL)
    '            If Val(objDt.Rows(0).Item(0).ToString) > 0 Then
    '                'Task:2746 Less Cash Request Total Amount
    '                If (Val(objDt.Rows(0).Item(1).ToString) - Val(objDt.Rows(0).Item(3).ToString)) < Val(objDt.Rows(0).Item(2).ToString) + Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum)) Then
    '                    'End Task:2746
    '                    ShowErrorMessage("PO amount exceeded from CMFA amount.")
    '                    Return False
    '                Else
    '                    Return True
    '                End If
    '            Else
    '                Return True
    '            End If
    '        Else
    '            Return True
    '        End If

    '        'If Val(Me.txtCMFAAmount.Text) < Val( Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(GrdEnum.TotalAmount), Janus.Windows.GridEX.AggregateFunction.Sum)) Then
    '        '    ShowErrorMessage("PO amount exceeded from CMFA amount.")
    '        'End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Sub EditRecord()
        Try

            IsEditMode = True
            'FillCombo("Vendor") 'R933 Commented 
            'FillCombo("LC")'R933 Commented
            If Not Me.grdSaved.RowCount > 0 Then Exit Sub
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            'Me.FillCombo("SOComplete") 'R933 Commented
            txtPONo.Text = grdSaved.CurrentRow.Cells(0).Value.ToString
            Me.GetSecurityRights()
            'Task 1592
            If grdSaved.CurrentRow.Cells(1).Value > Date.Today.ToString("yyyy-MM-dd 23:59:59") AndAlso IsDateChangeAllowed = False Then
                dtpPODate.MaxDate = dtpPODate.Value.AddMonths(3)
                dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            Else
                dtpPODate.Value = CType(grdSaved.CurrentRow.Cells(1).Value, Date)
            End If
            txtReceivingID.Text = grdSaved.CurrentRow.Cells("PurchaseDemandId").Value
            'TODO. ----
            cmbVendor.Value = grdSaved.CurrentRow.Cells("VendorId").Value
            ''R933  Validate Vendor Data 
            cmbVendor.Value = grdSaved.CurrentRow.Cells("VendorId").Value
            If Me.cmbVendor.ActiveRow Is Nothing Then
                ShowErrorMessage("Vendor is disable.")
                Exit Sub
            End If

            ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
            ApprovalProcessId = getConfigValueByType("PurchaseDemandApproval")
            If ApprovalProcessId = 0 Then
                Me.btnApprovalHistory.Visible = False
                Me.btnApprovalHistory.Enabled = False
            Else
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
                Me.chkPost.Visible = False
            End If
            ''Ayesha Rehman :TFS2375 :End

            txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value & ""
            'txtPaid.Text = grdSaved.CurrentRow.Cells("CashPaid").Value & ""
            'Me.cmbSalesMan.SelectedValue = grdSaved.CurrentRow.Cells("EmployeeCode").Value.ToString
            'Me.cmbPo.SelectedValue = Me.grdSaved.CurrentRow.Cells("PoId").Value
            'Me.cmbCurrency.SelectedValue = Me.grdSaved.GetRow.Cells("CurrencyType").Text.ToString
            'Me.txtCurrencyRate.Text = Me.grdSaved.GetRow.Cells("CurrencyRate").Text.ToString
            'If IsDBNull(Me.grdSaved.GetRow.Cells("Receiving_Date").Value) Then
            '    Me.dtpReceivingDate.Value = Now
            '    Me.dtpReceivingDate.Checked = False
            'Else
            '    Me.dtpReceivingDate.Value = Me.grdSaved.GetRow.Cells("Receiving_Date").Value
            '    Me.dtpReceivingDate.Checked = True
            'End If
            Call DisplayDetail(grdSaved.CurrentRow.Cells("PurchaseDemandId").Value)
            Previouse_Amount = 0D 'Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
            'Me.cmbLC.Value = grdSaved.CurrentRow.Cells("LcId").Value.ToString
            'R:913 Retrieve Post Value
           
            'End R:913
            'Task:2673 Check Status CMFA Document
            Me.cmbProject.SelectedValue = Val(Me.grdSaved.GetRow.Cells("CostCenterId").Value.ToString)
            'Altered against Task#201506004 to set combobox values
            FillCombo("SOrder")
            Me.cmbSalesOrder.Text = Me.grdSaved.GetRow.Cells("SalesOrderNo").Value.ToString
            Me.cmbSalesOrder.Enabled = False
            'Altered against Task#201506004 to set combobox values
            'If Val(Me.grdSaved.GetRow.Cells("RefCMFADocId").Value.ToString) > 0 Then
            '    Dim objDt As DataTable = CType(Me.cmbCMFADoc.DataSource, DataTable)
            '    objDt.TableName = "Default"
            '    Dim dv As New DataView
            '    dv.Table = objDt
            '    dv.RowFilter = "DocId=" & Me.grdSaved.GetRow.Cells("RefCMFADocId").Value.ToString & ""

            '    If Not dv.ToTable.Rows.Count > 0 Then
            '        Dim dr As DataRow
            '        dr = objDt.NewRow
            '        dr(0) = Me.grdSaved.GetRow.Cells("RefCMFADocId").Value.ToString
            '        dr(1) = Me.grdSaved.GetRow.Cells("CMFA Doc No").Value.ToString
            '        dr(2) = Me.cmbVendor.Value
            '        objDt.Rows.Add(dr)
            '        objDt.AcceptChanges()
            '    End If
            'End If
            ''RemoveHandler cmbCMFADoc.SelectedIndexChanged, AddressOf cmbCMFADoc_SelectedIndexChanged
            'Me.cmbCMFADoc.SelectedValue = Me.grdSaved.GetRow.Cells("RefCMFADocId").Value.ToString
            'AddHandler cmbCMFADoc.SelectedIndexChanged, AddressOf cmbCMFADoc_SelectedIndexChanged
            'End Taks:2673

            GetTotal()
            Me.BtnSave.Text = "&Update"
            Me.cmbPo.Enabled = False
            'Me.GetSecurityRights()
            If grdSaved.CurrentRow.Cells("Post").Value.ToString = "UnPost" Then
                Me.chkPost.Checked = False
            Else
                Me.chkPost.Checked = True
            End If




            'Me.txtTerms_And_Condition.Text = Me.grdSaved.GetRow.Cells("Terms").Value.ToString
            Me.UltraTabControl1.SelectedTab = Me.UltraTabPageControl1.Tab
            Me.lblPrintStatus.Text = "Print Status: " & Me.grdSaved.GetRow.Cells("Print Status").Text.ToString
            '//Start TASK # 1592
            '24-OCT-2017: Ayesha Rehman: If user dont have update rights then btnsave should not be enable true
            If BtnSave.Enabled = True Then
                If grdSaved.CurrentRow.Cells(1).Value > Date.Today.ToString("yyyy-MM-dd 23:59:59") AndAlso IsDateChangeAllowed = False Then
                    Me.BtnSave.Enabled = False
                    ErrorProvider1.SetError(Me.Label2, "Future Date can not be edit")
                    ErrorProvider1.BlinkRate = 1000
                    ErrorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
                Else
                    Me.BtnSave.Enabled = True
                    ErrorProvider1.Clear()
                End If
            End If
            'End Task # 1592
            If flgDateLock = True Then
                If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00"))) Then
                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    Me.dtpPODate.Enabled = False
                Else
                    Me.dtpPODate.Enabled = True
                End If
            Else
                Me.dtpPODate.Enabled = True
            End If
            
            'Altered Against Task# 2015060001 Ali Ansari
            'Get no of attached files
            Dim intCountAttachedFiles As Integer = 0I
            If Me.BtnSave.Text <> "&Save" Then
                If Me.grdSaved.RowCount > 0 Then
                    intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
                    Me.btnAttachment.Text = "Attachment (" & intCountAttachedFiles & ")"
                End If
            End If
            'Altered Against Task# 2015060001 Ali Ansari

            ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.BtnDelete.Visible = True
            Me.BtnPrint.Visible = True
            '''''''''''''''''''''''''

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub DisplayPODetail(ByVal ReceivingID As Integer)
    '    Dim str As String
    '    'Dim i As Integer
    '    str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
    '          & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total,Isnull(Recv_D.TaxPercent,0) as TaxPercent, 0 as TaxAmount, " _
    '          & " Article.ArticleGroupId,Recv_D.ArticleDefId,Sz7 as PackQty,Recv_D.Price as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice FROM dbo.SalesDetailTable Recv_D INNER JOIN " _
    '          & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
    '          & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
    '          & " Where Recv_D.SalesID =" & ReceivingID & ""

    '    'Dim objCommand As New OleDbCommand
    '    'Dim objCon As OleDbConnection
    '    'Dim objDataAdapter As New OleDbDataAdapter
    '    'Dim objDataSet As New DataSet

    '    'objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

    '    'If objCon.State = ConnectionState.Open Then objCon.Close()

    '    'objCon.Open()
    '    'objCommand.Connection = objCon
    '    'objCommand.CommandType = CommandType.Text


    '    'objCommand.CommandText = str

    '    'objDataAdapter.SelectCommand = objCommand
    '    'objDataAdapter.Fill(objDataSet)

    '    'grd.Rows.Clear()
    '    'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

    '    '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9))

    '    '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
    '    '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

    '    'Next
    '    Dim dtDisplayPODetail As New DataTable
    '    dtDisplayPODetail = GetDataTable(str)
    '    Me.grd.DataSource = Nothing
    '    dtDisplayPODetail.Columns("Total").Expression = "IIF(Unit='Pack', ((PackQty*Qty)*Price), Qty*Price)"
    '    Me.grd.DataSource = dtDisplayPODetail
    '    ApplyGridSetting()
    '    FillCombo("grdLocation")
    'End Sub
    '' ReqId-899 Added New Field TaxPercent, TaxAmount In Query
    Private Sub DisplayDetail(ByVal ReceivingID As Integer, Optional ByVal Condition As String = "")
        Dim str As String = String.Empty
        'Dim i As Integer

        'str = "SELECT     Article_Group.ArticleGroupName AS Category, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
        '      & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, " _
        '      & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, isnull(Article_Group.ArticleGroupId,0) ArticleGroupId  FROM dbo.PurchaseDemandDetailTable Recv_D INNER JOIN " _
        '      & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
        '      & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId " _
        '      & " Where Recv_D.PurchaseDemandID =" & ReceivingID & ""
        If Condition = String.Empty Then
            'Before against task:2374
            '' ReqId-928 Added Column Comments
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Recv_D.Price, " _
            '     & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, Isnull(Recv_D.TaxPercent,0) as TaxPercent, 0 as TaxAmount, " _
            '     & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice, isnull(Recv_D.PackPrice,0) as PackPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments  FROM dbo.PurchaseDemandDetailTable Recv_D INNER JOIN " _
            '     & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '     & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '     & " Where Recv_D.PurchaseDemandID =" & ReceivingID & ""
            '' End ReqId-928
            'Task:3274 Added Column Total Amount 
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, " _
            '  & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments  FROM dbo.PurchaseDemandDetailTable Recv_D INNER JOIN " _
            '  & " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '  & " Where Recv_D.PurchaseDemandID =" & ReceivingID & ""

            'End Task:2374
            'TASK24 Added Fields Color, Size, UOM
            str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, " _
           & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments, Recv_D.DeliveredQty ,  Recv_D.PurchaseDemandDetailId, IsNull(Recv_D.Qty, 0) As TotalQty, IsNull(Recv_D.SalesOrderDetailId, 0) As SalesOrderDetailId FROM dbo.PurchaseDemandDetailTable Recv_D INNER JOIN " _
           & " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
           & " Where Recv_D.PurchaseDemandID =" & ReceivingID & ""

            'ElseIf Condition = "LoadSalesDemand" Then
            '    str = String.Empty
            '    'Before against Task:2374
            '    '' ReqId-928 Added Column Comments
            '    'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Isnull(Recv_D.PurchasePrice,0) as Price, " _
            '    '    & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, Isnull(Recv_D.SalesTax_Percentage,0) as TaxPercent, 0 as TaxAmount, " _
            '    '    & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, Isnull(Recv_D.PurchasePrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments  FROM dbo.SalesDemandDetailTable Recv_D INNER JOIN " _
            '    '    & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '    '    & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '    '    & " Where Recv_D.SalesDemandID =" & ReceivingID & ""
            '    '' End ReqId-928
            '    'Task:2374 Added Column Total Amount
            '    str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, Isnull(Recv_D.PurchasePrice,0) as Price, " _
            '        & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, Isnull(Recv_D.SalesTax_Percentage,0) as TaxPercent, 0 as TaxAmount, Convert(float,0) as [Total Amount], " _
            '        & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, Isnull(Recv_D.PurchasePrice,0) as CurrentPrice, Isnull(Recv_D.PackPrice,0) as PackPrice, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments  FROM dbo.SalesDemandDetailTable Recv_D INNER JOIN " _
            '        & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '        & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '        & " Where Recv_D.SalesDemandID =" & ReceivingID & ""
            'ElseIf Condition = "LoadCMFADocument" Then
            '    str = " SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Sz1,0)-IsNull(Recv_D.POQty,0)) AS Qty, Isnull(Recv_D.Price,0) as Price,  " _
            '         & " CASE WHEN recv_d.articlesize = 'Loose' THEN Recv_D.Sz1 * Recv_D.Price ELSE Recv_D.Sz1 * Recv_D.Price * Article.PackQty END AS Total, 0 as TaxPercent, 0 as TaxAmount, Convert(float,0) as [Total Amount], " _
            '         & " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty, Isnull(Recv_D.Current_Price,0) as CurrentPrice, 0 as PackPrice, Isnull(Recv_D.PackDesc,Recv_D.ArticleSize) as Pack_Desc, '' as Comments  FROM dbo.CMFADetailTable Recv_D INNER JOIN  " _
            '         & " dbo.ArticleDefTable Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN  " _
            '         & " dbo.ArticleGroupDefTable Article_Group ON Article.ArticleGroupId = Article_Group.ArticleGroupId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '         & " Where Recv_D.DocId =" & ReceivingID & " AND Recv_D.VendorId=" & Me.cmbVendor.Value & ""
            'Task#2015060004 to display item record against SalesOrder
        ElseIf Condition = "SalesOrder" Then
            'str = " select b.locationid,articlecode,articledescription as Item, " _
            '& "A.ARTICLESIZE AS UNIT,a.qty,ARTICLEGROUPID,Articledefid,c.packqty," _
            '   & "         a.currentprice, a.pack_desc, a.comments " _
            '& "from SalesOrderDetailTable a  inner join SalesOrderMasterTable b on a.salesorderid = b.salesorderid  " _
            '& "left join ArticleDefView c on a.articledefid = c.articleid  left join tbldeflocation d on a.locationid = d.location_id  " _
            '& "left outer join  tblvendor e on  b.vendorid = e.vendorid  where b.salesorderid = " & ReceivingID & ""
            'str = "SELECT Recv_D.LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, Recv_D.Sz1 AS Qty, " _
            '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Recv_D.Sz7 as PackQty,Recv_D.CurrentPrice,Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Comments  FROM dbo.PurchaseDemandDetailTable Recv_D INNER JOIN " _
            '& " dbo.ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN tblDefLocation Loc ON Loc.Location_Id = Recv_D.LocationId " _
            '& " Where Recv_D.PurchaseDemandID =" & ReceivingID & ""
            'Task#2015060004 to display item record against SalesOrder
            'TASK24 Added Fields Color, Size, UOM
            str = " select b.locationid,articlecode,articledescription as Item, c.ArticleColorName as Color, c.ArticleSizeName as Size, c.ArticleUnitName as UOM, " _
           & "A.ARTICLESIZE AS UNIT, IsNull(a.Sz1, 0)-IsNull(a.DeliveredQty, 0) As Qty, ARTICLEGROUPID,Articledefid,c.packqty," _
           & " a.currentprice, a.pack_desc, a.comments, 0 As DeliveredQty ,  0 As PurchaseDemandDetailId, IsNull(a.Qty, 0)-IsNull(a.DeliveredTotalQty, 0) As TotalQty, a.SalesOrderDetailId " _
           & "from SalesOrderDetailTable a  inner join SalesOrderMasterTable b on a.salesorderid = b.salesorderid  " _
           & "left join ArticleDefView c on a.articledefid = c.articleid  left join tbldeflocation d on a.locationid = d.location_id  " _
           & "left outer join  tblvendor e on  b.vendorid = e.vendorid  where b.salesorderid = " & ReceivingID & " And IsNull(a.Qty, 0)>IsNull(a.DeliveredTotalQty, 0)"
            'End TASK24
        ElseIf Condition = "CostSheet" Then

            'str = "SELECT 1 as LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Qty,0) * " & Val(frmLoadCostSheet.txtPlanQty.Text) & ") AS Qty, " _
            ' & " Article.ArticleGroupId, Recv_D.ArticleId as ArticleDefId, 1 as PackQty, PurchasePrice as CurrentPrice,Recv_D.ArticleSize as Pack_Desc, Recv_D.Remarks as Comments  FROM dbo.tblCostSheet Recv_D INNER JOIN " _
            ' & " dbo.ArticleDefView Article ON Recv_D.ArticleId = Article.ArticleId " _
            ' & " Where Recv_D.MasterArticleId =" & ReceivingID & ""
            'TASK24 Added Fields Color, Size, UOM
            str = "SELECT 1 as LocationId, Article.ArticleCode, Article.ArticleDescription AS item, Article.ArticleColorName as Color, Article.ArticleSizeName as Size, Article.ArticleUnitName as UOM, Recv_D.ArticleSize AS unit, (IsNull(Recv_D.Qty,0) * " & Val(frmLoadCostSheet.txtPlanQty.Text) & ") AS Qty, " _
           & " Article.ArticleGroupId, Recv_D.ArticleId as ArticleDefId, 1 as PackQty, PurchasePrice as CurrentPrice,Recv_D.ArticleSize as Pack_Desc, Recv_D.Remarks as Comments , 0 As DeliveredQty ,  0 As PurchaseDemandDetailId, IsNull(Recv_D.Qty, 0) As TotalQty, 0 As SalesOrderDetailId  FROM dbo.tblCostSheet Recv_D INNER JOIN " _
           & " dbo.ArticleDefView Article ON Recv_D.ArticleId = Article.ArticleId " _
           & " Where Recv_D.MasterArticleId =" & ReceivingID & ""
            'End TASK24

        End If

        'Dim objCommand As New OleDbCommand
        'Dim objCon As OleDbConnection
        'Dim objDataAdapter As New OleDbDataAdapter
        'Dim objDataSet As New DataSet

        'objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        'If objCon.State = ConnectionState.Open Then objCon.Close()

        'objCon.Open()
        'objCommand.Connection = objCon
        'objCommand.CommandType = CommandType.Text


        'objCommand.CommandText = str

        'objDataAdapter.SelectCommand = objCommand
        'objDataAdapter.Fill(objDataSet)

        'grd.Rows.Clear()
        'For i = 0 To objDataSet.Tables(0).Rows.Count - 1

        '    grd.Rows.Add(objDataSet.Tables(0).Rows(i)(0), objDataSet.Tables(0).Rows(i)(10), objDataSet.Tables(0).Rows(i)(1), objDataSet.Tables(0).Rows(i)(2), objDataSet.Tables(0).Rows(i)(3), objDataSet.Tables(0).Rows(i)(4), objDataSet.Tables(0).Rows(i)(5), objDataSet.Tables(0).Rows(i)(6), objDataSet.Tables(0).Rows(i)(7), objDataSet.Tables(0).Rows(i)(8), objDataSet.Tables(0).Rows(i)(9))

        '    'grd.Rows(i).Cells(0).Value = objDataSet.Tables(0).Rows(i)(0)
        '    'grd.Rows(i).Cells(1).Value = objDataSet.Tables(0).Rows(i)(1)

        'Next
        Dim dtDisplayDetail As New DataTable
        dtDisplayDetail = GetDataTable(str)
        Me.grd.DataSource = Nothing
        'dtDisplayDetail.Columns("Total").Expression = "IIF(Unit='Pack',((PackQty*Qty)*Price), Qty*Price)"
        'dtDisplayDetail.Columns("TaxAmount").Expression = "((TaxPercent/100)*Total)"
        'dtDisplayDetail.Columns("Total Amount").Expression = "Total+TaxAmount" 'Task:2374 Show Total Amount
        Me.grd.DataSource = dtDisplayDetail
        ApplyGridSetting()
        FillCombo("grdLocation")


    End Sub
    '' ReqId-899 Added New Field of TaxPercent, TaxAmount
    Private Function Update_Record() As Boolean
        If ApprovalProcessId = 0 Then
            'R:913 added Feasibility Post Rights
            If Me.chkPost.Visible = False Then
                Me.chkPost.Checked = False
            End If
        Else
            Me.chkPost.Visible = False
        End If
        'End R:970
        'Task:2673 Validtion CMFA Document
        Dim strCMFA As String = String.Empty
        Dim objDt As New DataTable
        'If Me.cmbCMFADoc.SelectedIndex > 0 Then
        '    strCMFA = "Select CMFADetailTable.ArticleDefId, SUM(IsNull(Sz1,0)) as Qty, SUM(IsNull(POQty,0)-IsNull(PQty,0)) as PoQty From CMFADetailTable LEFT OUTER JOIN(Select ArticleDefId, IsNull(Sz1,0) as PQty From PurchaseDemandDetailTable WHERE PurchaseDemandId IN(Select PurchaseDemandId From PurchaseDemandMasterTable WHERE PurchaseDemandNo=N'" & Me.txtPONo.Text & "' AND RefCMFADocId=" & Me.cmbCMFADoc.SelectedValue & ")) PO On Po.ArticleDefId = CMFADetailTable.ArticleDefId  WHERE DocId=" & Me.cmbCMFADoc.SelectedValue & "   Group By CMFADetailTable.ArticleDefId "
        '    objDt = GetDataTable(strCMFA)
        'End If
        'End Task:2673
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        gobjLocationId = MyCompanyId
        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans


            objCommand.CommandText = String.Empty
            objCommand.CommandText = "SELECT  ISNULL(Sz1,0) as Qty, IsNull(ArticleDefID, 0) As ArticleDefID, IsNull(Qty,0) as TotalQty, IsNull(SalesOrderDetailId, 0) As SalesOrderDetailId FROM PurchaseDemandDetailTable WHERE  PurchaseDemandId = " & Me.grdSaved.CurrentRow.Cells("PurchaseDemandId").Value & ""
            Dim da As New OleDbDataAdapter(objCommand)
            Dim dtSavedItems As New DataTable
            da.Fill(dtSavedItems)
            dtSavedItems.AcceptChanges()


            If dtSavedItems.Rows.Count > 0 Then
                For Each r As DataRow In dtSavedItems.Rows
                    objCommand.CommandText = String.Empty
                    objCommand.CommandText = "Update SalesOrderDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) - " & r.Item(0) & "), DeliveredTotalQty= abs(IsNull(DeliveredTotalQty,0) - " & r.Item(2) & ") where  ArticleDefID = " & r.Item(1) & " And SalesOrderDetailId = " & r.Item(3) & " "    'Task#29082015
                    objCommand.ExecuteNonQuery()
                Next

            End If

            objCommand.CommandText = String.Empty
            objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Val(Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) & ""
            objCommand.ExecuteNonQuery()



            'objCon.BeginTransaction()
            'objCommand.CommandText = "Update PurchaseDemandMasterTable set PurchaseDemandNo =N'" & txtPONo.Text & "',PurchaseDemandDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
            '& " PurchaseDemandQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",PurchaseDemandAmount=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text & "',UserName=N'" & LoginUserName & "', LCID=" & Val(Me.cmbLC.Value) & ", CurrencyType=" & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & ", CurrencyRate=" & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", Receiving_Date=" & IIf(Me.dtpReceivingDate.Checked = True, "N'" & Me.dtpReceivingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Terms_And_Condition=N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "'  Where PurchaseDemandID= " & txtReceivingID.Text & " "
            ''ReqId-928 Solve Comma Error on Remarks
            'Before against request no. 913
            'objCommand.CommandText = "Update PurchaseDemandMasterTable set PurchaseDemandNo =N'" & txtPONo.Text & "',PurchaseDemandDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
            '           & " PurchaseDemandQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",PurchaseDemandAmount=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', LCID=" & Val(Me.cmbLC.Value) & ", CurrencyType=" & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & ", CurrencyRate=" & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", Receiving_Date=" & IIf(Me.dtpReceivingDate.Checked = True, "N'" & Me.dtpReceivingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Terms_And_Condition=N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "'  Where PurchaseDemandID= " & txtReceivingID.Text & " "
            'R:913 Added Column Post
            objCommand.CommandText = ""
            'Before against task:2673
            'objCommand.CommandText = "Update PurchaseDemandMasterTable set PurchaseDemandNo =N'" & txtPONo.Text & "',PurchaseDemandDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
            '           & " PurchaseDemandQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ",PurchaseDemandAmount=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", CashPaid=" & Val(txtPaid.Text) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',UserName=N'" & LoginUserName & "', LCID=" & Val(Me.cmbLC.Value) & ", CurrencyType=" & IIf(Me.grpCurrency.Visible = True, "" & Me.cmbCurrency.SelectedValue & "", "NULL") & ", CurrencyRate=" & IIf(Me.grpCurrency.Visible = True, "" & Val(Me.txtCurrencyRate.Text) & "", "NULL") & ", Receiving_Date=" & IIf(Me.dtpReceivingDate.Checked = True, "N'" & Me.dtpReceivingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", Terms_And_Condition=N'" & Me.txtTerms_And_Condition.Text.Replace("'", "''") & "', Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & "  Where PurchaseDemandID= " & txtReceivingID.Text & " "
            'Task:2673 Added Field RefCMFADocId


            objCommand.CommandText = "Update PurchaseDemandMasterTable set PurchaseDemandNo=N'" & txtPONo.Text & "',PurchaseDemandDate=N'" & dtpPODate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',VendorId=" & cmbVendor.ActiveRow.Cells(0).Value & ", " _
                       & " PurchaseDemandQty=" & Me.grd.GetTotal(Me.grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum) & ", Remarks=N'" & txtRemarks.Text.Replace("'", "''") & "',Post=" & IIf(Me.chkPost.Checked = True, 1, 0) & ",CostCenterId=" & Me.cmbProject.SelectedValue & ", UpdateUserName='" & LoginUserName.Replace("'", "''") & "'  Where PurchaseDemandID= " & Val(txtReceivingID.Text) & ""
            'End Task:2673
            objCommand.ExecuteNonQuery()

            'Marked Against Task#2015060001 Ali Ansari
            'If arrFile.Length > 0 Then
            '    SaveDocument(Val(txtReceivingID.Text), Me.Name, trans)
            'End If
            'Marked Against Task#2015060001 Ali Ansari
            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                SaveDocument(Val(txtReceivingID.Text), Me.Name, trans)
            End If

            objCommand.CommandText = ""
            objCommand.CommandText = "Delete from PurchaseDemandDetailTable where PurchaseDemandID = " & txtReceivingID.Text
            objCommand.ExecuteNonQuery()

            'objCommand.CommandText = ""

            'For i = 0 To grd.Rows.Count - 1
            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "Insert into PurchaseDemandDetailTable (PurchaseDemandId, ArticleDefId,ArticleSize, Sz1,Qty,Price, Sz7,CurrentPrice) values( " _
            '                            & " " & txtReceivingID.Text & " ," & Val(grd.Rows(i).Cells(8).Value) & ",N'" & (grd.Rows(i).Cells(3).Value) & "'," & Val(grd.Rows(i).Cells(4).Value) & ", " _
            '                            & " " & IIf(grd.Rows(i).Cells(3).Value = "Loose", Val(grd.Rows(i).Cells(4).Value), (Val(grd.Rows(i).Cells(4).Value) * Val(grd.Rows(i).Cells(9).Value))) & ", " & Val(grd.Rows(i).Cells(5).Value) & ", " & Val(grd.Rows(i).Cells(9).Value) & "  , " & Val(grd.Rows(i).Cells(10).Value) & ") "

            '    objCommand.ExecuteNonQuery()
            For i = 0 To grd.RowCount - 1
                '' ReqId-928 Added Column Comments in Query
                'Task:2673 Validate If PO Qty Exceeded From CMFA Qty
                'Dim objDR() As DataRow
                'If Me.cmbCMFADoc.SelectedIndex > 0 Then
                '    objDR = objDt.Select("ArticleDefId=" & Me.grd.GetRows(i).Cells(GrdEnum.ItemId).Value.ToString & "")
                '    If objDR IsNot Nothing Then
                '        If objDR.Length > 0 Then
                '            If Val(objDR(0).ItemArray(1).ToString) < (Val(objDR(0).Item(2).ToString) + Val(Me.grd.GetRows(i).Cells(GrdEnum.Qty).Value.ToString)) Then
                '                Throw New Exception(" [" & Me.grd.GetRows(i).Cells(GrdEnum.Item).Value.ToString & "] PO qty exceeded from CMFA qty.")
                '            End If
                '        End If
                '    End If
                'End If
                'End Task:2673

                objCommand.CommandText = ""
                objCommand.CommandText = "Insert into PurchaseDemandDetailTable (PurchaseDemandId, LocationId, ArticleDefId,ArticleSize, Sz1,Qty,Sz7,CurrentPrice,Pack_Desc,Comments, SalesOrderDetailId) values( " _
                                        & " " & txtReceivingID.Text & ", " & Val(Me.grd.GetRows(i).Cells(GrdEnum.LocationId).Value) & " ," & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value) & ",N'" & (grd.GetRows(i).Cells(GrdEnum.Unit).Value) & "'," & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", " _
                                        & " " & Val(grd.GetRows(i).Cells(GrdEnum.TotalQty).Value) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value) & "  , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value) & ", N'" & grd.GetRows(i).Cells(GrdEnum.Pack_Desc).Value.ToString.Replace("'", "''") & "', N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "', " & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ") Select @@Identity"
                '& " " & IIf(grd.GetRows(i).Cells(GrdEnum.Unit).Value = "Loose", Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value), (Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) * Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value))) & ", " & Val(grd.GetRows(i).Cells(GrdEnum.PackQty).Value) & "  , " & Val(grd.GetRows(i).Cells(GrdEnum.CurrentPrice).Value) & ", N'" & grd.GetRows(i).Cells(GrdEnum.Pack_Desc).Value.ToString.Replace("'", "''") & "', N'" & grd.GetRows(i).Cells("Comments").Value.ToString.Replace("'", "''") & "') Select @@Identity"

                Dim intPurchaseDemandDetailId As Integer = objCommand.ExecuteScalar()


                'Dim demandDetailId As Integer = objCommand.ExecuteScalar()
                'objCommand.CommandText = ""
                'objCommand.CommandText = ""
                '' End ReqId-928
                'Val(grd.Rows(i).Cells(5).Value)


                '' 14-11-2016
                objCommand.CommandText = "UPDATE  SalesOrderDetailTable " _
                                               & " SET DeliveredQty = isnull(DeliveredQty,0) +  " & Val(grd.GetRows(i).Cells(GrdEnum.Qty).Value) & ", DeliveredTotalQty= IsNull(DeliveredTotalQty,0) + " & Val(Me.grd.GetRows(i).Cells(GrdEnum.TotalQty).Value) & " " _
                                               & " WHERE     (SalesOrderID = " & IIf(Me.cmbSalesOrder.SelectedIndex = -1, 0, Me.cmbSalesOrder.SelectedValue) & ") AND (ArticleDefId = " & Val(grd.GetRows(i).Cells(GrdEnum.ItemId).Value) & ") And (SalesOrderDetailId =" & Val(grd.GetRows(i).Cells("SalesOrderDetailId").Value.ToString) & ")  "
                objCommand.ExecuteNonQuery()
                '' 14-11-2016


                objCommand.CommandText = ""
                objCommand.CommandText = "Update PurchaseOrderDetailTable Set PurchaseDemandDetailId=" & intPurchaseDemandDetailId & " WHERE PurchaseDemandDetailId=" & Val(Me.grd.GetRows(i).Cells("PurchaseDemandDetailId").Value.ToString) & ""
                objCommand.ExecuteNonQuery()

            Next



            If Me.cmbSalesOrder.SelectedIndex > 0 Then
                objCommand.CommandText = "Select IsNull(Sz1,0) as Qty , isnull(DeliveredQty , 0) as DeliveredQty from SalesOrderDetailTable where SalesOrderID = " & Me.cmbSalesOrder.SelectedValue & ""
                Dim DA1 As New OleDbDataAdapter(objCommand)
                Dim dt As New DataTable
                DA1.Fill(dt)
                Dim blnEqual As Boolean = True

                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        If r.Item(0) <> r.Item(1) AndAlso r.Item(0) > r.Item(1) Then
                            blnEqual = False
                            Exit For
                        End If
                    Next
                End If
                If blnEqual = True Then
                    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Close.ToString & "' where SalesOrderID = " & Me.cmbSalesOrder.SelectedValue & ""
                    objCommand.ExecuteNonQuery()
                Else
                    objCommand.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Me.cmbSalesOrder.SelectedValue & ""
                    objCommand.ExecuteNonQuery()
                End If
            End If

            'Task:2673 Update POQty And Set Status Open Or Close
            'If Me.cmbCMFADoc.SelectedIndex > 0 Then

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "UPDATE CMFADetailTable SET POQty = IsNull(a.Qty,0) " _
            '     & " FROM (SELECT dbo.PurchaseDemandMasterTable.VendorId, dbo.PurchaseDemandMasterTable.RefCMFADocId, SUM(dbo.PurchaseDemandDetailTable.Sz1) AS Qty, " _
            '     & " dbo.PurchaseDemandDetailTable.ArticleDefId, dbo.PurchaseDemandDetailTable.ArticleSize " _
            '     & " FROM dbo.PurchaseDemandDetailTable INNER JOIN " _
            '     & " dbo.PurchaseDemandMasterTable ON dbo.PurchaseDemandDetailTable.PurchaseDemandId = dbo.PurchaseDemandMasterTable.PurchaseDemandId " _
            '     & " WHERE(ISNULL(dbo.PurchaseDemandMasterTable.RefCMFADocId, 0)=" & Me.cmbCMFADoc.SelectedValue & ") " _
            '     & " GROUP BY dbo.PurchaseDemandMasterTable.VendorId, dbo.PurchaseDemandMasterTable.RefCMFADocId, dbo.PurchaseDemandDetailTable.ArticleDefId, dbo.PurchaseDemandDetailTable.ArticleSize) a " _
            '     & " WHERE " _
            '     & " a.ArticleDefId = CMFADetailTable.ArticleDefId " _
            '     & " AND CMFADetailTable.DocId = a.RefCMFADocId  " _
            '     & " AND CMFADetailTable.VendorId = a.VendorId " _
            '     & " AND CMFADetailTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
            '    objCommand.ExecuteNonQuery()

            '    objCommand.CommandText = ""
            '    objCommand.CommandText = "UPDATE CMFAMasterTable SET Status=CASE WHEN a.BalanceQty <=0 Then 0 ELSE 1 end From( " _
            '               & " SELECT dbo.CMFAMasterTable.DocId, SUM(ISNULL(dbo.CMFADetailTable.Sz1, 0) - ISNULL(dbo.CMFADetailTable.POQty, 0)) AS BalanceQty " _
            '               & " FROM dbo.CMFADetailTable INNER JOIN " _
            '               & " dbo.CMFAMasterTable ON dbo.CMFADetailTable.DocId = dbo.CMFAMasterTable.DocId WHERE dbo.CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & " " _
            '               & " GROUP BY dbo.CMFAMasterTable.DocId) a " _
            '               & " WHERE(a.DocId = CMFAMasterTable.DocId) AND dbo.CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
            '    objCommand.ExecuteNonQuery()

            'End If
            'End Task:2673

            trans.Commit()
            Update_Record = True
            'InsertVoucher()
            getVoucher_Id = Me.txtReceivingID.Text
            setVoucherNo = Me.txtPONo.Text
            setEditMode = True
            Total_Amount = 0D 'Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try

        ''insert Activity Log
        SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Purchase, Me.txtPONo.Text.Trim, True)
        ''Start TFS2989
        If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim, Me.Name) Then
            If ValidateApprovalProcessIsInProgressAgain(Me.txtPONo.Text.Trim, Me.Name) = False Then
                SaveApprovalLog(EnumReferenceType.PurchaseDemand, getVoucher_Id, Me.txtPONo.Text.Trim, Me.dtpPODate.Value.Date, "Purchase Demand - " & cmbVendor.Text & "", Me.Name)
            End If
        End If
        ''End TFS2989
        SendSMS()
    End Function

    Public Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Me.BtnSave.Enabled = False Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            'If flgDateLock = True Then
            '    If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00"))) Then
            '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '    End If
            'End If
            If IsDateLock(Me.dtpPODate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If
            If Me.dtpPODate.Value <= Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)) Then
                ShowErrorMessage("Your can not change this becuase financial year is closed")
                Me.dtpPODate.Focus()
                Exit Sub
            End If

            If FormValidate() Then
                Me.grd.UpdateData()
                If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then

                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()

                    If Save() Then
                        'EmailSave()
                        'msg_Information(str_informSave)
                        SendAutoEmail()
                        RefreshControls()
                        '201506004
                        'FillCombo("SOrder")
                        '201506004
                        ClearDetailControls()
                        'grd.Rows.Clear()
                        'DisplayRecord() R933 Commented History Data

                        If BackgroundWorker2.IsBusy Then Exit Sub
                        BackgroundWorker2.RunWorkerAsync()
                        'Do While BackgroundWorker2.IsBusy
                        '    Application.DoEvents()
                        'Loop
                        '-------------------------------------
                        If BackgroundWorker1.IsBusy Then Exit Sub
                        BackgroundWorker1.RunWorkerAsync()
                        'Do While BackgroundWorker1.IsBusy
                        '    Application.DoEvents()
                        'Loop

                    Else
                        Exit Sub 'MsgBox("Record has not been added")
                    End If
                Else
                    If IsValidToDelete("PurchaseOrderDetailTable", "DemandID", Me.grdSaved.CurrentRow.Cells("PurchaseDemandId").Value.ToString) = True Then
                        If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                        Me.lblProgress.Text = "Processing Please Wait ..."
                        Me.lblProgress.Visible = True
                        Application.DoEvents()

                        If Update_Record() Then
                            'EmailSave()
                            'msg_Information(str_informUpdate)
                            SendAutoEmail()
                            RefreshControls()
                            '201506004
                            'FillCombo("SOrder")
                            '201506004
                            ClearDetailControls()
                            'grd.Rows.Clear()
                            'DisplayRecord() R933 Commented History Data

                            If BackgroundWorker2.IsBusy Then Exit Sub
                            BackgroundWorker2.RunWorkerAsync()
                            'Do While BackgroundWorker2.IsBusy
                            '    Application.DoEvents()
                            'Loop
                            '-------------------------------------
                            If BackgroundWorker1.IsBusy Then Exit Sub
                            BackgroundWorker1.RunWorkerAsync()
                            'Do While BackgroundWorker1.IsBusy
                            '    Application.DoEvents()
                            'Loop

                        Else
                            Exit Sub 'MsgBox("Record has not been updated")
                        End If
                    Else
                        msg_Error(str_ErrorDependentUpdateRecordFound)
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.Cursor = Cursors.WaitCursor
        'Dim s As String
        's = "1234-567-890"
        'MsgBox(Microsoft.VisualBasic.Right(s, InStr(1, s, "-") - 2))
        cmbSalesOrder.Enabled = True
        If Me.grd.RowCount > 0 Then
            If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
        End If
       
        RefreshControls()
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdSaved_CellDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        ''Task#A1 12-06-2015 Add Check on grdSaved to check on double click if row less than zero than exit
        Try
            If Me.grdSaved.Row < 0 Then
                Exit Sub
            Else
                EditRecord()
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        ''End Task#A1 12-06-2015
        
    End Sub

    Private Sub cmbPo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPo.SelectedIndexChanged

        'Me.DisplayPODetail(Me.cmbPo.SelectedValue)
        'If Me.cmbPo.SelectedIndex > 0 Then
        '    Dim adp As New OleDbDataAdapter
        '    Dim dt As New DataTable
        '    Dim Sql As String = "Select * from SalesMasterTable where SalesId=" & Me.cmbPo.SelectedValue
        '    adp = New OleDbDataAdapter(Sql, Con)
        '    adp.Fill(dt)
        '    'TODO -----
        '    Me.cmbVendor.Value = dt.Rows(0).Item("VendorCode")
        '    Me.cmbVendor.Enabled = False

        'Else
        '    Me.cmbVendor.Enabled = True
        '    Me.cmbVendor.Rows(0).Activate()
        'End If
    End Sub

    Private Sub grd_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        With Me.grd.CurrentRow

            If Me.grd.CurrentRow.Cells("Unit").Value = "Loose" Then
                'txtPackQty.Text = 1
                .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value)
            Else
                .Cells("Total").Value = Val(.Cells("Qty").Value) * Val(.Cells("Rate").Value) * Val(.Cells("PackQty").Value)
            End If
            GetTotal()
        End With
    End Sub

    Private Sub cmbItem_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbItem.KeyDown
        Try
            ''TFS1858 : Ayesha Rehman :Item dropdown shall be searchable
            If e.KeyCode = Keys.F1 Then
                If flgCompanyRights = True Then
                    frmItemSearch.CompanyId = MyCompanyId
                End If
                If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                    If GetRestrictedItemFlg(Me.cmbCategory.SelectedValue) = True Then
                        frmItemSearch.LocationId = Me.cmbCategory.SelectedValue
                    Else
                        frmItemSearch.LocationId = 0
                    End If
                End If

                frmItemSearch.BringToFront()
                frmItemSearch.ShowDialog()
                If frmItemSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbItem.Value = frmItemSearch.ArticleId
                    txtQty.Text = frmItemSearch.Qty
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
        Try
            'If Me.cmbItem.IsItemInList = False Then
            '    Me.txtStock.Text = 0
            '    Exit Sub
            'End If
            'Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            'Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            'If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1 ' Before  ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            Me.txtQty.Text = 0 ' After ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
            ClearDetailControls()
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            FillCombo("ArticlePack")
            'Me.cmbVendor.DisplayLayout.Grid.Show( me.cmbVendor.contr)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Enter
        Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub

    Private Sub cmbVendor_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVendor.Enter
        Me.cmbVendor.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        'ValidateDateLock()
        'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
        'If flgDateLock = True Then
        '    If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00"))) Then
        '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
        '    End If
        'End If
        If IsDateLock(Me.dtpPODate.Value) = True Then
            ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
        End If
        If Not Me.grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        ''Start TFS2988 : Ayesha Rehman : 09-04-2018
        If IsEditMode = True Then
            If ValidateApprovalProcessMapped(Me.txtPONo.Text.Trim) Then
                If ValidateApprovalProcessInProgress(Me.txtPONo.Text.Trim) Then
                    msg_Error("Document Can Not be Deleted ,because Approval Process is in Progress") : Exit Sub
                End If
            End If
        End If
        ''End TFS2988
        If IsValidToDelete("PurchaseOrderDetailTable", "DemandID", Me.grdSaved.CurrentRow.Cells("PurchaseDemandId").Value.ToString) = True Then
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Me.Cursor = Cursors.WaitCursor
            Try
                'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14 
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                Dim cm As New OleDbCommand
                Dim objTrans As OleDbTransaction
                If Con.State = ConnectionState.Closed Then Con.Open()
                objTrans = Con.BeginTransaction




                'Task:2673 Update POQty And Set Status Open Or Close
                'If Me.cmbCMFADoc.SelectedIndex > 0 Then

                '    cm.CommandText = ""
                '    cm.Connection = Con
                '    cm.Transaction = objTrans
                '    cm.CommandText = "UPDATE CMFADetailTable SET POQty = IsNull(POQty,0)-IsNull(a.Qty,0) " _
                '     & " FROM (SELECT dbo.PurchaseDemandMasterTable.VendorId, dbo.PurchaseDemandMasterTable.RefCMFADocId, SUM(dbo.PurchaseDemandDetailTable.Sz1) AS Qty, " _
                '     & " dbo.PurchaseDemandDetailTable.ArticleDefId, dbo.PurchaseDemandDetailTable.ArticleSize " _
                '     & " FROM dbo.PurchaseDemandDetailTable INNER JOIN " _
                '     & " dbo.PurchaseDemandMasterTable ON dbo.PurchaseDemandDetailTable.PurchaseDemandId = dbo.PurchaseDemandMasterTable.PurchaseDemandId " _
                '     & " WHERE(ISNULL(dbo.PurchaseDemandMasterTable.RefCMFADocId, 0)=" & Me.cmbCMFADoc.SelectedValue & ") AND (PurchaseDemandDetailTable.PurchaseDemandId=" & Me.grdSaved.CurrentRow.Cells(5).Value.ToString & ") " _
                '     & " GROUP BY dbo.PurchaseDemandMasterTable.VendorId, dbo.PurchaseDemandMasterTable.RefCMFADocId, dbo.PurchaseDemandDetailTable.ArticleDefId, dbo.PurchaseDemandDetailTable.ArticleSize) a " _
                '     & " WHERE " _
                '     & " a.ArticleDefId = CMFADetailTable.ArticleDefId " _
                '     & " AND CMFADetailTable.DocId = a.RefCMFADocId  " _
                '     & " AND CMFADetailTable.VendorId = a.VendorId " _
                '     & " AND (CMFADetailTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ")"
                '    cm.ExecuteNonQuery()

                '    cm.CommandText = ""
                '    cm.Connection = Con
                '    cm.Transaction = objTrans
                '    cm.CommandText = "UPDATE CMFAMasterTable SET Status=CASE WHEN a.BalanceQty <=0 Then 0 ELSE 1 end From( " _
                '               & " SELECT dbo.CMFAMasterTable.DocId, SUM(ISNULL(dbo.CMFADetailTable.Sz1, 0) - ISNULL(dbo.CMFADetailTable.POQty, 0)) AS BalanceQty " _
                '               & " FROM dbo.CMFADetailTable INNER JOIN " _
                '               & " dbo.CMFAMasterTable ON dbo.CMFADetailTable.DocId = dbo.CMFAMasterTable.DocId WHERE dbo.CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & " " _
                '               & " GROUP BY dbo.CMFAMasterTable.DocId) a " _
                '               & " WHERE(a.DocId = CMFAMasterTable.DocId) AND dbo.CMFAMasterTable.DocId=" & Me.cmbCMFADoc.SelectedValue & ""
                '    cm.ExecuteNonQuery()

                'End If
                'End Task:2673

                cm.Connection = Con
                cm.Transaction = objTrans


                cm.CommandText = String.Empty
                cm.CommandText = "SELECT  ISNULL(Sz1,0) as Qty, IsNull(ArticleDefID, 0) As ArticleDefID, IsNull(Qty,0) as TotalQty, IsNull(SalesOrderDetailId, 0) As SalesOrderDetailId FROM PurchaseDemandDetailTable WHERE  PurchaseDemandId = " & Me.grdSaved.CurrentRow.Cells("PurchaseDemandId").Value & ""
                Dim da As New OleDbDataAdapter(cm)
                Dim dtSavedItems As New DataTable
                da.Fill(dtSavedItems)
                dtSavedItems.AcceptChanges()


                If dtSavedItems.Rows.Count > 0 Then
                    For Each r As DataRow In dtSavedItems.Rows
                        cm.CommandText = String.Empty
                        cm.CommandText = "Update SalesOrderDetailTable set DeliveredQty = abs(Isnull(DeliveredQty,0) - " & r.Item(0) & "), DeliveredTotalQty= abs(IsNull(DeliveredTotalQty,0) - " & r.Item(2) & ") where  ArticleDefID = " & r.Item(1) & " And SalesOrderDetailId = " & r.Item(3) & " "    'Task#29082015
                        cm.ExecuteNonQuery()
                    Next

                End If

                cm.CommandText = String.Empty
                cm.CommandText = "Update SalesOrderMasterTable set Status = N'" & EnumStatus.Open.ToString & "' where SalesOrderID = " & Val(Me.grdSaved.CurrentRow.Cells("SalesOrderId").Value.ToString) & ""
                cm.ExecuteNonQuery()







                cm.CommandText = "delete from PurchaseDemandDetailTable where PurchaseDemandid=" & Me.grdSaved.CurrentRow.Cells("PurchaseDemandId").Value.ToString
                cm.ExecuteNonQuery()



                cm = New OleDbCommand
                cm.Connection = Con
                cm.CommandText = "delete from PurchaseDemandMasterTable where PurchaseDemandid=" & Me.grdSaved.CurrentRow.Cells("PurchaseDemandId").Value.ToString

                cm.Transaction = objTrans

                cm.ExecuteNonQuery()
                objTrans.Commit()
                'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
                'msg_Information(str_informDelete)
                'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 25-1-14 


                Me.txtReceivingID.Text = 0


            Catch ex As Exception
                msg_Error("Error occured while deleting record: " & ex.Message)

            Finally
                Con.Close()
                Me.Cursor = Cursors.Default
                Me.lblProgress.Visible = False
            End Try

            ''insert Activity Log
            SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Purchase, grdSaved.CurrentRow.Cells(0).Value.ToString, True)

            Me.grdSaved.CurrentRow.Delete()
            Me.RefreshControls()

        Else

            msg_Error(str_ErrorDependentRecordFound)
        End If




    End Sub
    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = grdSaved.GetRow.Cells("PurchaseDemandNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            ShowReport("PurchaseDemand", "{PurchaseDemandMasterTable.PurchaseDemandId}=" & grdSaved.CurrentRow.Cells("PurchaseDemandId").Value, , , , , , , , , , Me.grdSaved.GetRow.Cells("Email").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grd_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
        Me.GetTotal()
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.chkPost.Visible = True 'R:913 Set Post View On
                Me.chkPost.Checked = True 'R:M6 Set Default Checked
                'Task 1592 Apply future date rights
                IsDateChangeAllowed = True
                dtpPODate.MaxDate = Date.Today.AddMonths(3)
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmPurchaseOrder)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                UserPriceAllowedRights = GetUserPriceAllowedRights(LoginUserId)
                'If UserPriceAllowedRights = True Then
                '    Me.pnlRateHidden.Visible = True
                '    Me.grd.RootTable.Columns("Price").Visible = True
                '    Me.grd.RootTable.Columns("Total").Visible = True
                'Else
                '    Me.pnlRateHidden.Visible = False
                '    Me.grd.RootTable.Columns("Price").Visible = False
                '    Me.grd.RootTable.Columns("Total").Visible = False
                'End If
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.btnSearchDelete.Enabled = False
                Me.btnSearchPrint.Enabled = False
                Me.BtnPrint.Enabled = False
                'Task 1592 Apply future date rights
                IsDateChangeAllowed = False
                DateChange(False)
                'Me.pnlRateHidden.Visible = False
                'Me.grd.RootTable.Columns("Price").Visible = False
                'Me.grd.RootTable.Columns("Total").Visible = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar2.mGridExport.Enabled = False  ''TFS1823
                Me.chkPost.Visible = False 'R:913 Set Post View Off
                CtrlGrdBar1.mGridChooseFielder.Enabled = False 'Task:2406 Added Field Chooser Rights
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                        Me.btnSearchDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        Me.btnSearchPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                        'Task:1592 Added Future Date Rights
                    ElseIf RightsDt.FormControlName = "Future Transaction" Then
                        IsDateChangeAllowed = True
                        DateChange(True)
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                        CtrlGrdBar2.mGridExport.Enabled = True ''TFS1823
                    ElseIf RightsDt.FormControlName = "Post" Then
                        Me.chkPost.Visible = True 'R:913 
                        Me.chkPost.Checked = True 'R:M6 Set Default Checked 
                    ElseIf RightsDt.FormControlName = "Price Allow" Then
                        'Me.pnlRateHidden.Visible = True
                        'Me.grd.RootTable.Columns("Price").Visible = True
                        'Me.grd.RootTable.Columns("Total").Visible = True
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    'Task:1592 Added Future Date Rights
    Private Sub DateChange(ByRef IsDateChangeAllowed As Boolean)
        Try
            If IsDateChangeAllowed = False Then
                dtpPODate.MaxDate = DateTime.Now.ToString("yyyy/M/d 23:59:59")
            Else
                dtpPODate.MaxDate = Date.Today.AddMonths(3)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'END TASk:1592
    Private Sub TableLayoutPanel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub
    'Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
    '    If Me.UltraTabControl1.SelectedTab.Index = 0 Then
    '        Me.BtnLoaddAll.Visible = False
    '        Me.ToolStripButton1.Visible = False
    '        Me.ToolStripSeparator1.Visible = False
    '    Else
    '        Me.BtnLoaddAll.Visible = True
    '        Me.ToolStripButton1.Visible = True
    '        Me.ToolStripSeparator1.Visible = True
    '    End If
    'End Sub
    'Private Sub BtnLoaddAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoaddAll.Click
    '    Me.Cursor = Cursors.WaitCursor
    '    DisplayRecord("All")
    '    Me.DisplayDetail(-1)
    '    Me.Cursor = Cursors.Default
    'End Sub
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14 
            'If Not msg_Confirm(str_ConfirmRefresh) = True Then Exit Sub
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            ''TASK TFS4544
            If getConfigValueByType("ItemFilterByName").ToString = "True" Then
                ItemFilterByName = Convert.ToBoolean(getConfigValueByType("ItemFilterByName").ToString)
            End If
            ''END TFS4544
            Dim id As Integer = 0

            id = Me.cmbVendor.SelectedRow.Cells(0).Value
            FillCombo("Vendor")
            Me.cmbVendor.Value = id

            id = Me.cmbCategory.SelectedValue
            FillCombo("Category")
            Me.cmbCategory.SelectedValue = id

            id = Me.cmbItem.SelectedRow.Cells(0).Value
            FillCombo("Item")
            Me.cmbItem.Value = id

            FillCombo("grdLocation")

            'id = Me.cmbCurrency.SelectedIndex
            'FillCombo("Currency")
            'Me.cmbCurrency.SelectedIndex = id
            ' ''R933 Call LC List
            'id = Me.cmbCurrency.SelectedIndex
            'FillCombo("LC")
            'Me.cmbCurrency.SelectedIndex = id
            'End R933 
            id = Me.cmbProject.SelectedIndex
            FillCombo("CostCenter")
            FillCombo("Ticket")
            ''start TFs4161
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            If Not getConfigValueByType("PurchaseDiablePackQuantity").ToString = "Error" Then
                IsPackQtyDisabled = Convert.ToBoolean(getConfigValueByType("PurchaseDiablePackQuantity").ToString)
            End If
            ''End TFS4161
            'Task#2015060004 fill combo box if edit mode is off
            If IsEditMode = False Then
                FillCombo("SOrder")
            End If
            'Task#2015060004 fill combo box if edit mode is off
            Me.cmbProject.SelectedIndex = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try

    End Sub
    Private Sub lnkVendorsItem_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try
            Dim Str As String = "SELECT ArticleDefTable.ArticleId FROM ArticleDefTable INNER JOIN ArticleDefVendors ON ArticleDefTable.MasterID = ArticleDefVendors.ArticleDefId WHERE ArticleDefVendors.VendorId=" & Me.cmbVendor.Value & ""
            Dim dt As DataTable = GetDataTable(Str)
            If dt.Rows.Count < 1 Then Exit Sub
            For Each Row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.cmbItem.Rows
                If Row.Index > 0 Then
                    Me.cmbItem.Focus()
                    Row.Selected = True
                    Me.txtQty.Focus()
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If Row.Cells(0).Value = dt.Rows(i).Item("ArticleId") Then
                            AddItemToGrid()
                            Application.DoEvents()
                        End If
                    Next
                End If
            Next
            Me.cmbItem.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.CloseDropdown)
            Me.GetTotal()
            Me.ClearDetailControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub AllItems()
        Try
            If IsFormLoaded = True Then
                If Me.rbtAll.Checked = True Then
                    FillCombo("Item")
                Else
                    Dim Str As String = "SELECT ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleDescription AS Item, ArticleDefView.ArticleCode AS Code, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination, ArticleDefView.PurchasePrice AS Price FROM  ArticleDefView INNER JOIN  ArticleDefVendors ON ArticleDefView.MasterID = ArticleDefVendors.ArticleDefId WHERE (ArticleDefView.Active = 1) AND (ArticleDefVendors.VendorId=" & Me.cmbVendor.Value & ")"
                    'FillDropDown(cmbItem, str)
                    FillUltraDropDown(Me.cmbItem, Str)
                    Me.cmbItem.Rows(0).Activate()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbVendor_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbVendor.KeyDown
        Try
            ''TFS1781 : Ayesha Rehman :Added for Selection of Customer or Vendor
            If e.KeyCode = Keys.F1 Then
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    'frmSearchCustomersVendors.rbtCustomers.Checked = True
                    'frmSearchCustomersVendors.rbtVendors.Checked = True
                    'frmSearchCustomersVendors.rbtCustomers.Visible = True
                    'frmSearchCustomersVendors.rbtVendors.Visible = True
                    frmAccountSearch.AccountType = "'Vendor','Customer' "
                Else
                    'frmSearchCustomersVendors.rbtCustomers.Checked = False
                    'frmSearchCustomersVendors.rbtVendors.Checked = True
                    'frmSearchCustomersVendors.rbtVendors.Visible = True
                    frmAccountSearch.AccountType = "'Vendor'"
                End If

                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbVendor.Value = frmAccountSearch.SelectedAccountId
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub cmbVendor_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.Leave
        Try
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            If Me.rbtVendor.Checked = True Then AllItems()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rbtAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtAll.CheckedChanged, rbtVendor.CheckedChanged
        Try
            AllItems()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillComboByEdit()
        Try
            If IsFormLoaded = True Then
                FillCombo("Vendor")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '' ReqId-899 Set Property Editable Column TaxAmount
    Private Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            '' ReqId-928 Added Index For Editable Comments Field
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If col.Index <> GrdEnum.LocationId AndAlso col.Index <> GrdEnum.Qty AndAlso col.Index <> GrdEnum.TotalQty AndAlso col.Index <> GrdEnum.Comments Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            '' End ReqId-928
            'grd.AutoSizeColumns()
            ''Start TFS4161
            If IsPackQtyDisabled = True Then
                Me.grd.RootTable.Columns(GrdEnum.TotalQty).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Else
                Me.grd.RootTable.Columns(GrdEnum.TotalQty).EditType = Janus.Windows.GridEX.EditType.TextBox
            End If
            ''End TFS4161
            'Rafay:Task Start:Edit UOM field in enum grd
            Me.grd.RootTable.Columns(GrdEnum.UOM).EditType = Janus.Windows.GridEX.EditType.TextBox
            'Rafay:Task End
            Me.grd.RootTable.Columns("Pack_Desc").Position = Me.grd.RootTable.Columns("Unit").Index
            Me.grd.RootTable.Columns("Unit").Position = Me.grd.RootTable.Columns("Pack_Desc").Index

            Dim bln As Boolean = Convert.ToBoolean(getConfigValueByType("GridFreezColumn").Replace("Error", "False").Replace("''", "False"))
            If bln = True Then
                Me.grd.FrozenColumns = Me.grd.RootTable.Columns("Size").Index
            Else
                Me.grd.FrozenColumns = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.grd.GetRow.Delete()
                grd.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub cmbItem_RowSelected(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
    '    Try
    '        If Me.cmbItem.IsItemInList = False Then
    '            Me.txtStock.Text = 0
    '            Exit Sub
    '        Else
    '            If Me.cmbItem.Value Is Nothing Then Exit Sub
    '            Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
    '            FillCombo("ArticlePack")
    '            Me.txtLastPrice.Text = LastPrice(Me.cmbVendor.Value, Me.cmbItem.Value) 'Task:2445 Call Last Price Function  and implement
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub rdoCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCode.CheckedChanged
        Try
            If Not IsFormLoaded = True Then Exit Sub
            If Me.rdoCode.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Purchase Demand"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                '      Return GetSerialNo("PD" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "PurchaseDemandMasterTable", "PurchaseDemandNo")
                'rafay:task start
                If CompanyPrefix = "V-ERP (UAE)" Then
                    ' companyinitials = "UE"
                    Return GetSerialNo("PD" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "PurchaseDemandMasterTable", "PurchaseDemandNo")
                Else
                    ''companyinitials = "PK"
                    ''  Return GetNextDocNo("PD" & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "PurchaseDemandMasterTable", "PurchaseDemandNo")
                    Return GetNextDocNo("PD" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "PurchaseDemandMasterTable", "PurchaseDemandNo")
                End If
                'rafay

            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                'rafay:task start
                If CompanyPrefix = "V-ERP (UAE)" Then
                    ' companyinitials = "UE"
                    Return GetSerialNo("PD" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "PurchaseDemandMasterTable", "PurchaseDemandNo")
                Else
                    ''companyinitials = "PK"
                    ''  Return GetNextDocNo("PD" & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "PurchaseDemandMasterTable", "PurchaseDemandNo")
                    Return GetNextDocNo("PD" & "-" & companyinitials & "-" & Format(Me.dtpPODate.Value, "yy"), 4, "PurchaseDemandMasterTable", "PurchaseDemandNo")
                End If
                'rafay
            Else
                Return GetNextDocNo("PD", 6, "PurchaseDemandMasterTable", "PurchaseDemandNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnAddNewitem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewitem.Click
        Call frmAddItem.ShowDialog()
        Call FillCombo("Item")
    End Sub
    Public Sub cmbVendor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.ValueChanged
        Try
            If Not IsFormLoaded = True Then Exit Sub
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            'FillCombo("LC")
            'FillCombo("CMFADoc")
            CtrlGrdBar1.Email = New SBModel.SendingEmail
            CtrlGrdBar1.Email.ToEmail = Me.cmbVendor.ActiveRow.Cells("Email").Text
            CtrlGrdBar1.Email.Subject = "Purchase Demand: " + "(" & Me.txtPONo.Text & ")"
            CtrlGrdBar1.Email.DocumentNo = Me.txtPONo.Text
            CtrlGrdBar1.Email.DocumentDate = Me.dtpPODate.Value
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function EmailSave()
        EmailSave = Nothing
        Dim flg As Boolean = False
        If Me.cmbVendor.ActiveRow Is Nothing Then Exit Function

        If IsEmailAlert = True Then
            Dim dtForm As DataTable = GetDataTable("Select ISNULL(EmailAlert,0) as EmailAlert  From tblForm WHERE Form_Name='frmPurchase' AND EmailAlert=1")
            If dtForm.Rows.Count > 0 Then
                flg = True
            Else
                flg = False
            End If
            If flg = True Then
                Email = New Email
                Email.ToEmail = AdminEmail
                Email.CCEmail = String.Empty
                Email.BccEmail = Me.cmbVendor.ActiveRow.Cells("Email").Text.ToString
                Email.Attachment = SourceFile
                Email.Subject = "Purchase Demand " & setVoucherNo & ""
                Email.Body = "Purchase Demand " _
                & " " & IIf(setEditMode = False, "of amount " & Total_Amount & " is made", "of amount " & Previouse_Amount & " is updated to " & Total_Amount & "") & " by user " & LoginUserName & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & " " & vbCrLf & "Auto Generated By SIRIUS ERP System"
                Email.Status = "Pending"
                Call New MailSentDAL().Add(Email)
            End If
        End If
        Return EmailSave

    End Function
    'Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
    '    Try
    '        If Me.SplitContainer1.Panel1Collapsed = True Then
    '            Me.SplitContainer1.Panel1Collapsed = False
    '        Else
    '            Me.SplitContainer1.Panel1Collapsed = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            DisplayRecord("All")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Get_All(ByVal PurchaseDemandNo As String)
        Try
            Get_All = Nothing
            If IsFormLoaded = True Then
                If PurchaseDemandNo.Length > 0 Then
                    Dim str As String = "Select * From PurchaseDemandMasterTable WHERE PurchaseDemandNo=N'" & PurchaseDemandNo & "'"
                    Dim dt As DataTable = GetDataTable(str)
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then

                            IsEditMode = True

                            ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
                            ApprovalProcessId = getConfigValueByType("PurchaseDemandApproval")
                            If ApprovalProcessId = 0 Then
                                Me.btnApprovalHistory.Visible = False
                                Me.btnApprovalHistory.Enabled = False
                            Else
                                Me.btnApprovalHistory.Visible = True
                                Me.btnApprovalHistory.Enabled = True
                                Me.chkPost.Visible = False
                            End If
                            ''Ayesha Rehman :TFS2375 :End

                            Me.txtReceivingID.Text = dt.Rows(0).Item("PurchaseDemandId")
                            Me.txtPONo.Text = dt.Rows(0).Item("PurchaseDemandNo")
                            Me.dtpPODate.Value = dt.Rows(0).Item("PurchaseDemandDate")
                            'Me.cmbCompany.SelectedValue = dt.Rows(0).Item("LocationId")
                            Me.cmbVendor.Value = dt.Rows(0).Item("VendorId")
                            Me.txtRemarks.Text = dt.Rows(0).Item("Remarks")
                            'Me.txtPaid.Text = dt.Rows(0).Item("CashPaid")

                            'If IsDBNull(dt.Rows(0).Item("CurrencyType")) Then
                            '    Me.cmbCurrency.SelectedIndex = 0
                            'Else
                            '    cmbCurrency.SelectedValue = dt.Rows(0).Item("CurrencyType")
                            'End If

                            'If IsDBNull(dt.Rows(0).Item("CurrencyRate")) Then
                            '    Me.cmbCurrency.SelectedIndex = 0
                            'Else
                            '    cmbCurrency.SelectedValue = dt.Rows(0).Item("CurrencyRate")
                            'End If

                            DisplayDetail(dt.Rows(0).Item("PurchaseDemandId"))
                            GetTotal()
                            Me.BtnSave.Text = "&Update"
                            Me.cmbPo.Enabled = False
                            GetSecurityRights()
                            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
                            'Me.cmbCompany.Enabled = False
                            IsDrillDown = True
                            Me.cmbVendor.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)

                            'If IsDBNull(dt.Rows(0).Item("Receiving_Date")) Then
                            '    Me.dtpReceivingDate.Value = Now
                            '    Me.dtpReceivingDate.Checked = False
                            'Else
                            '    Me.dtpReceivingDate.Value = dt.Rows(0).Item("Receiving_Date")
                            '    Me.dtpReceivingDate.Checked = True
                            'End If

                            If flgDateLock = True Then
                                If Convert.ToDateTime(CDate(MyDateLock.ToString("yyyy-M-d 00:00:00"))) >= Convert.ToDateTime(CDate(Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00"))) Then
                                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                                    Me.dtpPODate.Enabled = False
                                Else
                                    Me.dtpPODate.Enabled = True
                                End If
                            Else
                                Me.dtpPODate.Enabled = True
                            End If


                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                End If
                IsDrillDown = False
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            DisplayRecord("All")
            Me.DisplayDetail(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnSearchDocument_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDocument.Click
        Try
            If Not Me.cmbSearchAccount.IsItemInList Then
                FillCombo("SearchVendor")
                Me.cmbSearchAccount.Rows(0).Activate()
            Else
                Me.cmbSearchAccount.Rows(0).Activate()
            End If
            If Not Me.cmbSearchLocation.Items.Count > 0 Then
                FillCombo("SearchLocation")
                If Not Me.cmbSearchLocation.SelectedIndex = -1 Then Me.cmbSearchLocation.SelectedIndex = 0
            Else
                If Not Me.cmbSearchLocation.SelectedIndex = -1 Then Me.cmbSearchLocation.SelectedIndex = 0
            End If

            If Me.SplitContainer1.Panel1Collapsed = True Then
                Me.SplitContainer1.Panel1Collapsed = False
            Else
                Me.SplitContainer1.Panel1Collapsed = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearchDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDelete.Click
        Try
            DeleteToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSearchEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchEdit.Click
        Try
            OpenToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSearchPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPrint.Click
        Try
            PrintToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                DisplayRecord()
            Else
                ''16-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
                If IsEditMode = False Then Me.BtnDelete.Visible = False
                If IsEditMode = False Then Me.BtnPrint.Visible = False
                Me.BtnEdit.Visible = False
                ''''''''''''''''''''''''''
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ExportFile(ByVal VoucherId As Integer)
        Try
            If IsEmailAlert = True Then
                If IsAttachmentFile = True Then
                    crpt = New ReportDocument
                    If IO.File.Exists(str_ApplicationStartUpPath & "\Reports\PurchaseDemand.rpt") = False Then Exit Sub
                    crpt.Load(str_ApplicationStartUpPath & "\Reports\PurchaseDemand.rpt", DBServerName)
                    If DBUserName <> "" Then
                        crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, DBUserName, DBPassword)
                        crpt.DataSourceConnections.Item(0).SetLogon(DBName, DBPassword)
                    Else
                        crpt.DataSourceConnections.Item(0).SetConnection(DBServerName, DBName, True)
                    End If

                    Dim ConnectionInfo As New ConnectionInfo
                    With ConnectionInfo
                        .ServerName = DBServerName
                        .DatabaseName = DBName
                        If DBUserName <> "" Then
                            .UserID = DBUserName
                            .Password = DBPassword
                            .IntegratedSecurity = False
                        Else
                            .IntegratedSecurity = True
                        End If
                    End With
                    Dim tbLogOnInfo As New TableLogOnInfo
                    For Each dt As Table In crpt.Database.Tables
                        tbLogOnInfo = dt.LogOnInfo
                        tbLogOnInfo.ConnectionInfo = ConnectionInfo
                        dt.ApplyLogOnInfo(tbLogOnInfo)
                    Next
                    crpt.RecordSelectionFormula = "{PurchaseDemandMasterTable.PurchaseDemandId}=" & VoucherId



                    Dim crExportOps As New ExportOptions
                    Dim crDiskOps As New DiskFileDestinationOptions
                    Dim crExportType As New PdfRtfWordFormatOptions


                    If Not IO.Directory.Exists(str_ApplicationStartUpPath & "\EmailAttachments\") Then
                        IO.Directory.CreateDirectory(str_ApplicationStartUpPath & "\EmailAttachments\")
                    Else
                    End If
                    FileName = String.Empty
                    FileName = "Purchase Demand" & "-" & setVoucherNo & ""
                    SourceFile = String.Empty
                    SourceFile = _FileExportPath & "\" & FileName & ".pdf"
                    crDiskOps.DiskFileName = SourceFile
                    crExportOps = crpt.ExportOptions
                    With crExportOps
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                        .ExportDestinationOptions = crDiskOps
                        .ExportFormatOptions = crExportType
                    End With
                    crpt.Refresh()
                    Try
                        crpt.SetParameterValue("@CompanyName", CompanyTitle)
                        crpt.SetParameterValue("@CompanyAddress", CompanyAddHeader)
                        crpt.SetParameterValue("@ShowHeader", IsCompanyInfo)
                    Catch ex As Exception
                        'IsCompanyInfo = False
                        'CompanyTitle = String.Empty
                        'CompanyAddHeader = String.Empty
                    End Try
                    crpt.Export(crExportOps)

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            EmailSave()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            ExportFile(getVoucher_Id)
        Catch ex As Exception
            'ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim frm As New frmLetterCredit
            frm.Size = New Size(750, 500)
            frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            frm.MaximizeBox = False
            frm.MinimizeBox = False
            frm.WindowState = FormWindowState.Normal
            frm.StartPosition = FormStartPosition.CenterParent
            frm.ShowDialog()
            Dim id As Integer = 0
            'id = Me.cmbLC.Value
            'FillCombo("LC")
            'Me.cmbLC.Value = id
            Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.dtpPODate.Value.ToString("yyyy-M-d 00:00:00") Then
                If DateLock.Lock = True Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Sub ValidateDateLock()
        Try
            Dim dateLock As New SBModel.DateLockBE
            dateLock = DateLockList.Find(AddressOf chkDateLock)
            If dateLock IsNot Nothing Then
                If dateLock.DateLock.ToString.Length > 0 Then
                    flgDateLock = True
                Else
                    flgDateLock = False
                End If
            Else
                flgDateLock = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Private Sub btnLoadSalesDemand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim frm As New frmSalesDemandList
    '        If frm.ShowDialog() = Windows.Forms.DialogResult.Yes Then
    '            If Me.BtnSave.Text = "&Update" Then
    '                RefreshControls()
    '            Else
    '                DisplayDetail(-1)
    '            End If
    '            DisplayDetail(frm.ReceivingID, "LoadSalesDemand")
    '        End If

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    'Function GetTermsCondition() As String
    '    Try

    '        Dim str As String = "Select Terms_And_Condition From PurchaseDemandMasterTable WHERE PurchaseDemandId in (Select Max(PurchaseDemandId) From PurchaseDemandMasterTable)"
    '        Dim dt As New DataTable
    '        dt = GetDataTable(str)

    '        If dt IsNot Nothing Then
    '            If dt.Rows.Count > 0 Then
    '                Return dt.Rows(0).Item(0).ToString
    '            Else
    '                Return ""
    '            End If
    '        Else
    '            Return ""
    '        End If


    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Private Sub txtPackRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If Val(Me.txtPackQty.Text) = 0 Then
    '            txtPackQty.Text = 1
    '            txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
    '        Else
    '            txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtPackRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.txtPackRate.Text.Length > 0 AndAlso (Me.txtPackRate.Text) > 0 Then
    '            If Me.cmbUnit.Text <> "Loose" Then
    '                Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtPackQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackQty.TextChanged
    '    Try
    '        If Me.txtPackRate.Text.Length > 0 AndAlso (Me.txtPackRate.Text) > 0 Then
    '            If Me.cmbUnit.Text <> "Loose" Then
    '                Me.txtRate.Text = ((Val(Me.txtPackRate.Text)) / Val(Me.txtPackQty.Text))
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub PrintSelectedVouchersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintSelectedVouchersToolStripMenuItem.Click
        ' Change on 23-11-2013  For Multiple Print Vouchers
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                ShowReport("PurchaseDemand", "{PurchaseDemandMasterTable.PurchaseDemandId}=" & r.Cells("PurchaseDemandId").Value, , , True)
                PrintLog = New SBModel.PrintLogBE
                PrintLog.DocumentNo = r.Cells("PurchaseDemandNo").Value.ToString
                PrintLog.UserName = LoginUserName
                PrintLog.PrintDateTime = Date.Now
                Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
    Private Sub txtTotal_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Commented against task:2367
            'Dim dbl As Double = 0.0
            'If Val(Me.txtTotal.Text) > 0 And Val(Me.txtQty.Text) > 0 Then
            '    dbl = Me.txtTotal.Text / Me.txtQty.Text
            '    Me.txtRate.Text = Math.Round(dbl, 2).ToString()
            'Else
            '    dbl = Me.txtTotal.Text / Me.txtRate.Text
            '    Me.txtQty.Text = Math.Round(dbl, 2).ToString()
            'End If


            'Task:2367 Change Total
            'If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtRate.Text) <> 0 AndAlso Val(Me.txtQty.Text) = 0 Then
            '    Me.txtQty.Text = Val(Me.txtTotal.Text) / Val(Me.txtRate.Text)
            'End If

            'If Val(Me.txtTotal.Text) <> 0 AndAlso Val(Me.txtQty.Text) <> 0 AndAlso Val(Me.txtRate.Text) = 0 Then
            '    Me.txtRate.Text = Val(Me.txtTotal.Text) / Val(Me.txtQty.Text)
            'End If

            'If Val(Me.txtPackQty.Text) = 0 Then
            '    txtPackQty.Text = 1
            '    txtNetTotal.Text = (Val(txtQty.Text) * Val(txtRate.Text)) + ((Val(txtQty.Text) * Val(txtRate.Text) * Val(Me.txtTaxPercent.Text)) / 100)
            'Else
            '    txtNetTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text)) + (((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text) * Val(Me.txtTaxPercent.Text)) / 100)
            'End If
            'End Task:2367

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
    'Private Sub txtTaxPercent_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Val(Me.txtTaxPercent.Text) > 0 Then
    '            taxamnt = (Me.txtTotal.Text * Me.txtTaxPercent.Text) / 100
    '            Me.txtNetTotal.Text = Me.txtTotal.Text + taxamnt
    '        ElseIf Me.txtTaxPercent.Text = String.Empty Then
    '            taxamnt = 0D
    '            Me.txtNetTotal.Text = Val(Me.txtTotal.Text)
    '            Exit Sub
    '        Else
    '            Me.txtNetTotal.Text = Val(Me.txtTotal.Text)
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    '''''''''''''''''''''''''''''''
    ''27-Dec-2013   ReqId-954   M Ijaz Javed    Item rate against generate Total
    'Private Sub txtTaxPercent_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If Val(Me.txtTaxPercent.Text) > 0 Then
    '            Me.txtNetTotal.Text = Val(Me.txtTotal.Text) + ((Val(Me.txtTotal.Text) * Val(Me.txtTaxPercent.Text)) / 100)
    '        Else
    '            Me.txtNetTotal.Text = Val(Me.txtTotal.Text)
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Me.BtnEdit, Nothing)
            Exit Sub
        End If
        ''31-Jan-2014     Task:2404 Imran Delete Record Problem In Transaction Forms   
        'If e.KeyCode = Keys.Delete Then
        '    DeleteToolStripButton_Click(BtnDelete, Nothing)
        '    Exit Sub
        'End If
    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 20-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            If Me.grdSaved.RowCount <= 0 Then Exit Sub
            DeleteToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
    ''15-Feb-2014 Task:2426 Imran Ali Payment Schedule On Sales Demand And Purchase Demand
    'Private Sub btnPaymentSchedule1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Me.btnPaymentSchedule_Click(sender, e)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnPaymentSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.BtnSave.Text = "&Update" Or Me.BtnSave.Text = "Update" Or Me.UltraTabControl1.SelectedTab.Index = 1 Then
    '            ApplyStyleSheet(frmPaymentTermsSchedule)
    '            frmPaymentTermsSchedule.FormName = frmPaymentTermsSchedule.enmDemandType.PO
    '            frmPaymentTermsSchedule.DemandId = grdSaved.CurrentRow.Cells("PurchaseDemandId").Value 'Val(Me.txtReceivingID.Text)
    '            frmPaymentTermsSchedule.DemandNo = grdSaved.CurrentRow.Cells(0).Value.ToString 'Me.txtPONo.Text.ToString
    '            frmPaymentTermsSchedule.ShowDialog()
    '        Else
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    'End Task:2426
    ''28-Feb-2014  TASK:24445 Imran Ali   Last purchase and sale price show on sale order and purchase order
    Public Function LastPrice(ByVal AccountId As Integer, ByVal ItemId As Integer) As Double
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Select Isnull(Max(Price),0) as LastPrice From ReceivingDetailTable WHERE ArticleDefId=" & ItemId & " AND ReceivingId In (Select IsNull(Max(ReceivingId),0) as ReceivingId From ReceivingMasterTable WHERE VendorId=" & AccountId & ")"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return Val(dt.Rows(0).Item(0).ToString)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''18-June-2014 TASK:2695 Imran Ali CMFA Load On Purchase Demand
    'End Task:2445
    'Task:26733 Load CMFA Document From User
    'Private Sub cmbCMFADoc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCMFADoc.SelectedIndexChanged
    '    'Try
    '    '    If IsFormLoaded = True Then
    '    '        If Not Me.cmbCMFADoc.SelectedIndex = -1 Then
    '    '            DisplayDetail(Me.cmbCMFADoc.SelectedValue, "LoadCMFADocument")
    '    '        End If
    '    '    Else
    '    '        Exit Sub
    '    '    End If
    '    'Catch ex As Exception
    '    '    ShowErrorMessage(ex.Message)
    '    'End Try
    'End Sub
    'End Task:2673

    'Task:2673 Added Function For PO Qty Exceeded From CMFA Qty 
    'Public Sub ChkCMFAQty(ByVal ArticleDefId As Integer, ByVal DocId As Int32, ByVal Qty As Double, Optional ByVal trans As OleDbTransaction = Nothing)
    '    Try

    '        Dim objDt As New DataTable
    '        If Me.BtnSave.Text = "&Save" Then
    '            objDt = GetDataTable("Select CMFADetailTable.ArticleDefId, SUM(IsNull(Sz1,0)) as Qty, SUM(IsNull(POQty,0)) as PoQty From CMFADetailTable WHERE DocId=" & DocId & " Group By CMFADetailTable.ArticleDefId")
    '        Else
    '            objDt = GetDataTable("Select CMFADetailTable.ArticleDefId, SUM(IsNull(Sz1,0)) as Qty, SUM(IsNull(POQty,0)) as PoQty From CMFADetailTable WHERE CMFADetailTable.ArticleDefId=" & ArticleDefId & " AND DocId=" & DocId & " Group By CMFADetailTable.ArticleDefId") ')
    '        End If
    '        If objDt IsNot Nothing Then
    '            If objDt.Rows.Count > 0 Then

    '                If Val(objDt.Rows(0).Item(1).ToString) < (Val(objDt.Rows(0).Item(2).ToString) + Qty) Then
    '                    Throw New Exception(" " & Me.grd.GetRow.Cells(GrdEnum.Item).Value.ToString & " PO Qty Exceeded From CMFA Qty.")
    '                End If

    '            End If
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    'End Task:2673


    ''18-June-2014 TASK:2695 Imran Ali CMFA Load On Purchase Demand
    'Private Sub btnCMFA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        ApplyStyleSheet(frmCMFAPurchaseDemand)
    '        frmCMFAPurchaseDemand.ShowDialog()

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    'End Task:2695

    'Private Sub cmbCMFADoc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try

    '        If IsFormLoaded = True Then
    '            If Me.cmbCMFADoc.SelectedIndex > 0 Then
    '                Me.txtCMFAAmount.Text = Val(CType(Me.cmbCMFADoc.SelectedItem, DataRowView).Row.Item("CMFA_Amount").ToString)
    '                Me.txtPOAmountAgainstCMFA.Text = Val(CType(Me.cmbCMFADoc.SelectedItem, DataRowView).Row.Item("POAmount").ToString)
    '                Me.txtCMFADiff.Text = Val(Me.txtCMFAAmount.Text) - Val(Me.txtPOAmountAgainstCMFA.Text)
    '            Else
    '                Me.txtCMFAAmount.Text = String.Empty
    '                Me.txtPOAmountAgainstCMFA.Text = String.Empty
    '                Me.txtCMFADiff.Text = String.Empty
    '            End If
    '        End If

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
#Region "SMS Template Setting"
    Public Function GetSMSParamters() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("@AccountCode")
            str.Add("@AccountTitle")
            str.Add("@DocumentNo")
            str.Add("@DocumentDate")
            str.Add("@Remarks")
            str.Add("@Quantity")
            str.Add("@DetailInformation")
            str.Add("@CompanyName")
            str.Add("@SIRIUS")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSMSKey() As List(Of String)
        Try
            Dim str As New List(Of String)
            str.Add("Purchase Demand")
            Return str
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSMSTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSMSTemplate.Click
        Try
            Dim frmSMS As New frmSMSTemplate
            ApplyStyleSheet(frmSMS)
            frmSMS.cmbKey.DataSource = GetSMSKey()
            frmSMS.lstParameters.DataSource = GetSMSParamters()
            frmSMS.Show()
            frmSMS.BringToFront()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub SendSMS()
        Try
            '...................... Send SMS .............................
            If GetSMSConfig("SMS To Location On Purchase Demand").Enable = True Then
                Dim strSMSBody As String = String.Empty
                Dim objData As DataTable = CType(Me.grd.DataSource, DataTable)
                Dim dt_Loc As DataTable = objData.DefaultView.ToTable("Default", True, "LocationId")
                Dim drData() As DataRow
                For j As Integer = 0 To dt_Loc.Rows.Count - 1
                    strSMSBody = String.Empty
                    strSMSBody += "Purchase Demand, Doc No: " & Me.txtPONo.Text & ", Doc Date: " & Me.dtpPODate.Value.ToShortDateString & ", Supplier: " & Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString & ", Remarks:" & Me.txtRemarks.Text & ", "
                    drData = objData.Select("LocationId=" & dt_Loc.Rows(j).Item(0).ToString & "")
                    Dim dblTotalQty As Double = 0D
                    Dim dblTotalAmount As Double = 0D
                    For Each dr As DataRow In drData
                        dblTotalQty += IIf(dr(GrdEnum.Unit).ToString = "Loose", Val(dr(GrdEnum.Qty).ToString), Val(dr(GrdEnum.Qty).ToString) * Val(dr(GrdEnum.PackQty).ToString))
                        strSMSBody += " " & dr(GrdEnum.Item).ToString & ", " & IIf(dr(GrdEnum.Unit).ToString = "Loose", " " & dr(GrdEnum.Qty).ToString & "", "" & (Val(dr(GrdEnum.Qty)).ToString * Val(dr(GrdEnum.PackQty)).ToString) & "") & ", "
                    Next
                    strSMSBody += " Total Qty: " & dblTotalQty & ""
                    Dim LocationPhone() As String = GetLocation(Val(dt_Loc.Rows(j).Item(0).ToString)).Mobile_No.Replace(",", ";").Replace("|", ";").Replace("\", ";").Replace("/", ";").Replace("^", ";").Replace("*", ";").Split(";")
                    If LocationPhone.Length > 0 Then
                        For Each strPhone As String In LocationPhone
                            If strPhone.Length > 0 Then
                                Try
                                    SaveSMSLog(strSMSBody, strPhone, "SMS to Location")
                                Catch ex As Exception
                                    Throw ex
                                End Try
                            End If
                        Next
                    End If
                Next
            End If
            If GetSMSConfig("Purchase Demand").Enable = True Then
                If msg_Confirm(str_ConfirmSendSMSMessage) = False Then Exit Try
                Dim strDetailMessage As String = String.Empty
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    If strDetailMessage.Length = 0 Then
                        strDetailMessage = r.Cells(GrdEnum.Item).Value.ToString & ", Qty: " & IIf(r.Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(r.Cells(GrdEnum.Qty).Value.ToString), Val(r.Cells(GrdEnum.Qty).Value.ToString) * Val(r.Cells(GrdEnum.PackQty).Value.ToString))
                    Else
                        strDetailMessage += "," & r.Cells(GrdEnum.Item).Value.ToString & ", Qty: " & IIf(r.Cells(GrdEnum.Unit).Value.ToString = "Loose", Val(r.Cells(GrdEnum.Qty).Value.ToString), Val(r.Cells(GrdEnum.Qty).Value.ToString) * Val(r.Cells(GrdEnum.PackQty).Value.ToString))
                    End If
                Next
                Dim objTemp As New SMSTemplateParameter
                Dim obj As Object = GetSMSTemplate("Purchase Demand")
                If obj IsNot Nothing Then
                    objTemp.SMSTemplate = CType(obj, SMSTemplateParameter).SMSTemplate
                    Dim strMessage As String = objTemp.SMSTemplate
                    strMessage = strMessage.Replace("@AccountTitle", Me.cmbVendor.ActiveRow.Cells("Name").Value.ToString).Replace("@AccountCode", Me.cmbVendor.ActiveRow.Cells("Code").Value.ToString).Replace("@DocumentNo", Me.txtPONo.Text).Replace("@DocumentDate", Me.dtpPODate.Value.ToShortDateString).Replace("@Remarks", Me.txtRemarks.Text).Replace("@Quantity", Me.grd.GetTotal(grd.RootTable.Columns("Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)).Replace("@SONo", IIf(Me.cmbPo.SelectedIndex > 0, Me.cmbPo.Text, String.Empty)).Replace("@CompanyName", CompanyTitle).Replace("@SIRIUS", "Automated by www.SIRIUS.net").Replace("@DetailInformation", strDetailMessage)
                    SaveSMSLog(strMessage, Me.cmbVendor.ActiveRow.Cells("Mobile").Value.ToString, "Purchase Demand")

                    If GetSMSConfig("Purchase Demand").EnabledAdmin = True Then
                        For Each strMob As String In strAdminMobileNo.Replace(",", ";").Replace("|", ";").Replace("^", ";").Split(";")
                            If strMob.Length > 10 Then
                                SaveSMSLog(strMessage, strMob, "Purchase Demand")
                            End If
                        Next
                    End If
                End If
            End If

            '...................... End Send SMS ................
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

    'Private Sub btnPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.grdSaved.RowCount = 0 Then Exit Sub
    '        frmSOCashReceipt._CompanyId = Val(Me.grdSaved.GetRow.Cells("LocationId").Value.ToString)
    '        frmSOCashReceipt._CostCenterId = Val(Me.grdSaved.GetRow.Cells("CostCenterId").Value.ToString)
    '        frmSOCashReceipt._CustomerId = Val(Me.grdSaved.GetRow.Cells("VendorId").Value.ToString)
    '        frmSOCashReceipt._SaleDemandId = Val(Me.grdSaved.GetRow.Cells("PurchaseDemandId").Value.ToString)
    '        frmSOCashReceipt._SaleDemandNo = Me.grdSaved.GetRow.Cells("PurchaseDemandNo").Value.ToString
    '        frmSOCashReceipt._SaleDemandDate = Me.grdSaved.GetRow.Cells("Date").Value
    '        frmSOCashReceipt._CustomerName = Me.grdSaved.GetRow.Cells("VendorName").Value.ToString
    '        frmSOCashReceipt._NetAmount = Val(Me.grdSaved.GetRow.Cells("PurchaseDemandAmount").Value.ToString)
    '        frmSOCashReceipt.ShowDialog()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

   
    Private Sub cmbSalesOrder_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSalesOrder.SelectedIndexChanged, cmbTicketNo.SelectedIndexChanged
        Try
            If IsFormLoaded = False Then Exit Sub
            If Not cmbSalesOrder.SelectedIndex = -1 Then
                If IsEditMode = False Then
                    DisplayDetail(cmbSalesOrder.SelectedValue, "SalesOrder")
                Else
                    ' If cmbSalesOrder.SelectedIndex = -1 Then Exit Sub
                    '  cmbSalesOrder.SelectedIndex = 1
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Altered Against Task#2015060006 to save attachement
    Private Sub btnAttachment_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachment.ButtonClick
        Try

            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty
            'Marked Against Task#2015060005 
            'OpenFileDialog1.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG"
            'Marked Against Task#2015060005 

            '            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG"
            'Altered Against Task#2015060006 to make all files attachement physible
            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
           "All files (*.*)|*.*"
            'Altered Against Task#2015060006 to make all files attachement physible
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                'Altered Against Task#2015060001 Ali Ansari
                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a
                'Altered Against Task#2015060001 Ali Ansari

                If Me.BtnSave.Text <> "&Save" Then
                    If Me.grdSaved.RowCount > 0 Then
                        intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachment").Value)
                    End If
                End If


                'Marked Against Task#2015060001 Ali Ansari
                'Me.btnAttachment.Text = "Attachment (" & arrFile.Length + intCountAttachedFiles & ")"
                'Marked Against Task#2015060001 Ali Ansari
                'Altered Against Task#2015060001 Ali Ansari
                Me.btnAttachment.Text = "Attachment (" & arrFile.Count + intCountAttachedFiles & ")"
                'Altered Against Task#2015060001 Ali Ansari
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Altered Against Task#2015060006 to save attachement
    'Altered Against Task#2015060006 to save attachement
    Public Function SaveDocument(ByVal DocId As Integer, ByVal Source As String, ByVal objTrans As OleDb.OleDbTransaction) As Boolean
        Dim cmd As New OleDbCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            dt.AcceptChanges()


            Dim objdt As New DataTable
            objdt = GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            Dim intId As Integer = objdt.Rows(0)(0)

            Dim strSQL As String = String.Empty
            cmd.CommandText = String.Empty
            strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            Dim objPath As String = getConfigValueByType("FileAttachmentPath").ToString
            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                'Altered Against Task#2015060001 Ali Ansari
                'Marked Against Task#2015060001 Ali Ansari
                '            If arrFile.Length > 0 Then
                'Marked Against Task#2015060001 Ali Ansari
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If
                        Dim New_Files As String = intId & "_" & DocId & "_" & Me.dtpPODate.Value.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
                        Dim dr As DataRow
                        dr = dt.NewRow
                        dr(0) = DocId
                        dr(1) = Source
                        dr(2) = objPath & "\" & New_Files
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                        If IO.File.Exists(objPath & "\" & New_Files) Then
                            IO.File.Delete(objPath & "\" & New_Files)
                        End If
                        IO.File.Copy(objFile, objPath & "\" & New_Files)
                        intId += 1
                    End If
                Next
            End If


            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        Dim strPath As String = objPath
                        Dim strFileName As String = r.Item("FileNames").ToString.Substring(r.Item("FileNames").ToString.LastIndexOf("\") + 1)
                        cmd.CommandText = String.Empty
                        strSQL = "INSERT INTO DocumentAttachment(DocId, Source, FileName, Path) Values(" & Val(r("DocId").ToString) & ",N'" & r.Item("Source").ToString.Replace("'", "''") & "', N'" & strFileName.Replace("'", "''") & "', N'" & strPath.Replace("'", "''") & "')"
                        cmd.CommandText = strSQL
                        cmd.ExecuteNonQuery()
                    Next
                End If
            End If


        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function
    'Altered Against Task#2015060006 to save attachement

    Private Sub grdSaved_LinkClicked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try
            If e.Column.Key = "No Of Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.Name
                frm._VoucherId = Val(Me.grdSaved.GetRow.Cells("PurchaseDemandId").Value.ToString)
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnLoadCostSheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadCostSheet.Click
        Try
            ApplyStyleSheet(frmLoadCostSheet)
            If frmLoadCostSheet.ShowDialog = Windows.Forms.DialogResult.Yes Then
                DisplayDetail(frmLoadCostSheet.cmbItem.Value, "CostSheet")
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged
        Try
            If Val(Me.txtPackQty.Text) > 0 AndAlso Val(Me.txtQty.Text) > 0 Then
                Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            Else
                Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQty_LostFocus(sender As Object, e As EventArgs) Handles txtPackQty.LostFocus
        If Val(Me.txtPackQty.Text) > 0 AndAlso Val(Me.txtQty.Text) > 0 Then
            Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
        Else
            Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)

        End If
    End Sub

    Private Sub txtPackQty_TextChanged(sender As Object, e As EventArgs) Handles txtPackQty.TextChanged
        Try
            If Val(Me.txtPackQty.Text) > 0 Then
                Me.txtTotalQuantity.Text = Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)
            Else
                Me.txtTotalQuantity.Text = Val(Me.txtQty.Text)

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
   Public Sub GetGridDetailQtyCalculate(ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try
            Me.grd.UpdateData()
            If e.Column.Index = GrdEnum.Qty Or e.Column.Index = GrdEnum.PackQty Then
                If Val(Me.grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value = (Val(Me.grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString) * Val(Me.grd.GetRow.Cells(GrdEnum.Qty).Value.ToString))
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                Else
                    Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value = Val(Me.grd.GetRow.Cells(GrdEnum.Qty).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value
                End If
            ElseIf e.Column.Index = GrdEnum.TotalQty Then
                If Not Val(Me.grd.GetRow.Cells(GrdEnum.PackQty).Value.ToString) > 1 Then
                    Me.grd.GetRow.Cells(GrdEnum.Qty).Value = Val(Me.grd.GetRow.Cells(GrdEnum.TotalQty).Value.ToString)
                    'Me.grd.GetRow.Cells(GrdEnum.LoadQty).Value = Me.grd.GetRow.Cells(GrdEnum.Qty).Value
                End If
            End If
            'Me.grd.Refetch()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grd_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try
            Me.grd.UpdateData()
            GetGridDetailQtyCalculate(e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetSingle(ByVal PurchaseDemandId As Integer)
        Dim Str As String = ""
        Try
            Str = "SELECT  Recv.PurchaseDemandNo, Recv.PurchaseDemandDate, dbo.vwCOADetail.detail_title AS VendorName, Recv.PurchaseDemandQty,  " & _
                                    " Recv.PurchaseDemandId, Recv.VendorId, Recv.Remarks, Recv.Status, Case When Isnull(Post,0)=0 Then 'UnPost' ELSE 'Post' End as Post, dbo.vwCOADetail.Contact_Email as Email, Recv.LocationId, IsNull(Recv.CostCenterId,0) as CostCenterId, Recv.UserName, IsNull(Sales.salesorderno,0) as Salesorderno, Recv.UserName, Recv.UpdateUserName, Sales.SalesOrderId  " & _
                                    " FROM dbo.PurchaseDemandMasterTable Recv LEFT OUTER JOIN  " & _
                                    " dbo.vwCOADetail ON Recv.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN(Select DISTINCT PurchaseDemandId From PurchaseDemandDetailTable) as Location On Location.PurchaseDemandId = Recv.PurchaseDemandId left join dbo.SalesOrderMasterTable Sales on Sales.Salesorderid = Recv.salesorderid  " & _
                                    " WHERE Recv.PurchaseDemandNo IS NOT NULL And Recv.PurchaseDemandId = " & PurchaseDemandId & ""
            Dim dt As DataTable = GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ayesha Rehman : TFS2375 : Show Approval History of the current Document
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnApprovalHistory_Click(sender As Object, e As EventArgs) Handles btnApprovalHistory.Click
        Try
            frmApprovalHistory.DocumentNo = Me.txtPONo.Text
            frmApprovalHistory.Source = Me.Name
            frmApprovalHistory.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Purchase Demand"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Changes added by Murtaza for email generation on save (11/14/2022)
    Private Sub SendAutoEmail(Optional ByVal Activity As String = "")
        Try
            GetTemplate("Purchase Demand")
            If EmailTemplate.Length > 0 Then
                GetAutoEmailData()
                'GetVendorsEmails() ''Commented Against TFS3239
                'FillDataSet()
                UsersEmail = New List(Of String)
                'Added by murtaza (01/05/2023) for Remms email
                If Con.Database.Contains("Remms") Then
                    UsersEmail.Add("purchase@remmsit.com")
                Else
                    UsersEmail.Add("purchase@agriusit.com")
                End If
                'Added by murtaza (01/05/2023) for Remms email
                Me.Text = CompanyPrefix
                'UsersEmail.Add("purchase@agriusit.com")
                'UsersEmail.Add("m.ahmad@agriusit.com")
                FormatStringBuilder(dtEmail)
                'CreateOutLookMail()
                For Each _email As String In UsersEmail
                    CreateOutLookMail(_email)
                    SaveEmailLog(PurchaseDemandNo, _email, "frmPurchaseDemand", Activity)
                Next
                'SaveCCBCC(CC, BCC)
                'CC = ""
                'BCC = ""
            Else
                ShowErrorMessage("No email template is found for Purchase Demand.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FormatStringBuilder(ByVal dt As DataTable)
        Try
            html = New StringBuilder
            html.Append(EmailTemplate)
            html.Append("<table border = '1'>")

            'Building the Header row.
            html.Append("<tr bgcolor='#58ACFA'>")
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

            'Building the Data rows.
            For Each row As DataRow In dt.Rows
                If row.Table.Columns.Contains("Alternate") Then
                    If row.Item("Alternate") = "Yes" Then
                        html.Append("<tr bgcolor='#A9F5BC'>")
                        For Each column As DataColumn In dt.Columns
                            html.Append("<td>")
                            html.Append(row(column.ColumnName))
                            html.Append("</td>")
                        Next
                        html.Append("</tr>")
                    Else
                        html.Append("<tr>")
                        For Each column As DataColumn In dt.Columns
                            html.Append("<td>")
                            html.Append(row(column.ColumnName))
                            html.Append("</td>")
                        Next
                        html.Append("</tr>")
                    End If
                Else
                    html.Append("<tr>")
                    For Each column As DataColumn In dt.Columns
                        html.Append("<td>")
                        html.Append(row(column.ColumnName))
                        html.Append("</td>")
                    Next
                    html.Append("</tr>")
                End If
            Next
            'Table end.
            html.Append("</table>")
            html.Append(AfterFieldsElement)
            
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMail(ByVal Email As String)
        Try
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            mailItem.Subject = "Creating New Purchase Demand: " + txtPONo.Text & " " & "(" & txtRemarks.Text & ")"
            mailItem.To = Email
            ''mailItem.CC = "dispatch@agriusit.com"
            If Con.Database.Contains("Remms") Then
                mailItem.CC = "dispatch@remmsit.com"
            Else
                mailItem.CC = "dispatch@agriusit.com"
            End If
            Email = String.Empty
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            mailItem.Display(mailItem)
            mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
            EmailBody = html.ToString
            mailItem.Send()
            mailItem = Nothing
            oApp = Nothing
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
                    If Me.grd.RootTable.Columns.Contains(TrimSpace) Then
                        dtEmail.Columns.Add(TrimSpace)
                        AllFields.Add(TrimSpace)
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
        Dim Dr As DataRow
        Try
            Dim str As String
            str = "SELECT Top 1 PurchaseDemandId, PurchaseDemandNo from PurchaseDemandMasterTable order by 1 desc"
            Dim dt1 As DataTable = GetDataTable(str)
            If dt1.Rows.Count > 0 Then
                PurchaseDemandId = dt1.Rows(0).Item("PurchaseDemandId")
                PurchaseDemandNo = dt1.Rows(0).Item("PurchaseDemandNo")
            End If
            Dim str1 As String
            str1 = "SELECT ADT.ArticleCode,ADT.ArticleDescription AS Item,PDDT.Qty,PDDT.CurrentPrice,PDDT.Comments FROM PurchaseDemandDetailTable PDDT left join ArticleDefTable ADT ON PDDT.ArticleDefId=ADT.ArticleId Where PDDT.PurchaseDemandId =" & PurchaseDemandId & " ORDER BY ADT.SortOrder Asc"
            Dim dt As DataTable = GetDataTable(str1)
            For Each Row1 As DataRow In dt.Rows
                Dr = dtEmail.NewRow
                For Each col As String In AllFields
                    If Row1.Table.Columns.Contains(col) Then
                        Dr.Item(col) = Row1.Item(col).ToString
                    End If
                Next
                dtEmail.Rows.Add(Dr)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Changes added by Murtaza for email generation on save (11/14/2022)

    Private Sub cmbProject_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbProject.SelectedValueChanged
        Try
            If cmbProject.SelectedValue > 0 Then
                Dim str As String
                Dim str1 As String
                Dim contractId As Integer
                Dim dt As DataTable
                str = "SELECT ContractId from ContractMasterTable WHERE ContractNo = '" & cmbProject.Text & "'"
                dt = GetDataTable(str)
                If dt.Rows.Count > 0 Then
                    contractId = dt.Rows(0).Item(0)
                    str1 = "select * from TicketMasterTable where Status = 'Open' AND ContractId = " & contractId & ""
                    FillDropDown(cmbTicketNo, str1)
                End If
            Else
                FillCombo("Ticket")
            End If
            
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
