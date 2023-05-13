Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class frmMissingDocuments

    Implements IGeneral

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim strSQL As String = String.Empty

            strSQL = "SELECT     'Trial Balance' AS DocumentType, ROUND(SUM(debit_amount) - SUM(credit_amount), 0) AS DeferedValues " _
            & " FROM dbo.tblVoucherDetail  UNION ALL SELECT 'Stock Mismatch' AS Expr1, COUNT(SalesNo) AS SalesNo " _
            & "FROM  (SELECT     SalesNo, SalesDate FROM          dbo.SalesMasterTable UNION ALL " _
            & " SELECT  SalesReturnNo, SalesReturnDate FROM dbo.SalesReturnMasterTable  UNION ALL " _
            & " SELECT  ReceivingNo, ReceivingDate FROM dbo.ReceivingMasterTable UNION ALL " _
            & " SELECT  PurchaseReturnNo, PurchaseReturnDate FROM dbo.PurchaseReturnMasterTable UNION ALL " _
            & " SELECT  DispatchNo, DispatchDate FROM dbo.DispatchMasterTable UNION ALL " _
            & " SELECT Doc_no, Doc_Date FROM dbo.StockAdjustmentMaster UNION ALL " _
            & " SELECT  DocNo, DocDate FROM dbo.WarrantyClaimMasterTable UNION ALL " _
            & " SELECT  Production_no, Production_date FROM dbo.ProductionMasterTable) AS a " _
            & " WHERE (SalesNo NOT IN (SELECT  DocNo FRom dbo.StockMasterTable)) UNION ALL " _
            & "SELECT  'Missing Documents' AS Expr1, COUNT(DocNo) AS Expr2 FROM  dbo.StockMasterTable AS StockMasterTable_1 " _
            & " WHERE  (DocNo NOT IN (SELECT     SalesNo FROM (SELECT     SalesNo, SalesDate FROM  dbo.SalesMasterTable AS SalesMasterTable_1 UNION ALL " _
            & " SELECT     SalesReturnNo, SalesReturnDate FROM dbo.SalesReturnMasterTable AS SalesReturnMasterTable_1 UNION ALL " _
            & " SELECT     ReceivingNo, ReceivingDate FROM  dbo.ReceivingMasterTable AS ReceivingMasterTable_1 UNION ALL " _
            & " SELECT     PurchaseReturnNo, PurchaseReturnDate FROM dbo.PurchaseReturnMasterTable AS PurchaseReturnMasterTable_1 UNION ALL " _
            & " SELECT     DispatchNo, DispatchDate FROM  dbo.DispatchMasterTable AS DispatchMasterTable_1 UNION ALL " _
            & " SELECT     Doc_no, Doc_Date FROM  dbo.StockAdjustmentMaster AS StockAdjustmentMaster_1 UNION ALL " _
            & " SELECT     DocNo, DocDate FROM  dbo.WarrantyClaimMasterTable AS WarrantyClaimMasterTable_1 UNION ALL " _
            & "SELECT     Production_no, Production_date FROM  dbo.ProductionMasterTable AS ProductionMasterTable_1) AS a_1))"



            'strSQL = " SELECT DocumentType,DeferedValues from VMissingDocuments"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            If dt IsNot Nothing Then
                Me.GrdDocHeader.DataSource = Nothing
                Me.GrdDocHeader.DataSource = dt
                GrdDocHeader.RetrieveStructure()
                GrdDocHeader.RootTable.Columns("DeferedValues").FormatString = "N0"
                GrdDocHeader.RootTable.Columns.Add("Status")
                GrdDocHeader.RootTable.Columns("Status").ColumnType = Janus.Windows.GridEX.ColumnType.Image
                Dim c As Integer = 0
                For c = 0 To GrdDocHeader.RowCount - 1
                    If GrdDocHeader.GetRow(c).Cells("DeferedValues").Value.ToString = 0 Then

                        GrdDocHeader.GetRow(c).Cells("Status").Image = My.Resources._20604_24_button_ok_icon
                    Else
                        GrdDocHeader.GetRow(c).Cells("Status").Image = My.Resources.cross_icon
                    End If
                Next
            End If
            'Me.grdDocHeader.AutoSizeColumns()
            GrdDocHeader.RootTable.Columns("DocumentType").Width = 200
            GrdDocHeader.RootTable.Columns("DeferedValues").Width = 100
            GrdDocHeader.RootTable.Columns("Status").Width = 30
            GrdDocHeader.RootTable.Columns("Status").Caption = ""
            GrdDocHeader.RootTable.RowHeight = 30
            strSQL = String.Empty
            strSQL = "SELECT     SalesNo AS DocNo, SalesDate AS DocDate, Detail, Remarks " _
                        & " FROM  (SELECT     a.SalesNo, a.SalesDate, ISNULL(b.CustomerName, '-') AS Detail, a.Remarks " _
                        & " FROM dbo.SalesMasterTable AS a LEFT OUTER JOIN dbo.tblCustomer AS b ON a.CustomerCode = b.CustomerID UNION ALL " _
                        & " SELECT     a.SalesReturnNo, a.SalesReturnDate, ISNULL(b.CustomerName, '-') AS Expr1, a.Remarks FROM  dbo.SalesReturnMasterTable AS a LEFT OUTER JOIN dbo.tblCustomer AS b ON a.CustomerCode = b.CustomerID UNION ALL " _
                        & " SELECT     a.ReceivingNo, a.ReceivingDate, ISNULL(b.VendorName, '-') AS Expr1, a.Remarks FROM         dbo.ReceivingMasterTable AS a LEFT OUTER JOIN dbo.tblVendor AS b ON a.VendorId = b.VendorID UNION ALL " _
                        & " SELECT     a.PurchaseReturnNo, a.PurchaseReturnDate, ISNULL(b.VendorName, '-') AS Expr1, a.Remarks " _
                        & " FROM         dbo.PurchaseReturnMasterTable AS a LEFT OUTER JOIN dbo.tblVendor AS b ON a.VendorId = b.VendorID  UNION ALL " _
                        & " SELECT     a.DispatchNo, a.DispatchDate, ISNULL(b.VendorName, '-') AS Expr1, a.Remarks FROM dbo.DispatchMasterTable AS a LEFT OUTER JOIN dbo.tblVendor AS b ON a.VendorId = b.VendorID UNION ALL " _
                        & " SELECT     Doc_no, Doc_Date, '-' AS Expr1, remarks FROM dbo.StockAdjustmentMaster UNION ALL " _
                        & " SELECT     a.DocNo, a.DocDate, ISNULL(b.CustomerName, '-') AS Expr1, a.Remarks FROM dbo.WarrantyClaimMasterTable AS a LEFT OUTER JOIN dbo.tblCustomer AS b ON a.CustomerCode = b.CustomerID UNION ALL " _
                        & " SELECT     Production_no, Production_date, '' AS Expr1, Remarks FROM dbo.ProductionMasterTable) AS a " _
                        & " WHERE     (SalesNo NOT IN (SELECT DocNo FROM  dbo.StockMasterTable))"
            dt = GetDataTable(strSQL)
            If dt IsNot Nothing Then
                Me.GrdDetail.DataSource = Nothing
                Me.GrdDetail.DataSource = dt
                GrdDetail.RetrieveStructure()
                Me.GrdDetail.AutoSizeColumns()
            End If
            strSQL = " SELECT     DocNo, DocDate, Remarks FROM dbo.StockMasterTable WHERE     (DocNo NOT IN (SELECT SalesNo " _
                    & " FROM  (SELECT     dbo.SalesMasterTable.SalesNo, dbo.SalesMasterTable.SalesDate, ISNULL(dbo.tblCustomer.CustomerName, '-') AS Detail, dbo.SalesMasterTable.Remarks FROM  dbo.SalesMasterTable LEFT OUTER JOIN " _
                    & " dbo.tblCustomer ON dbo.SalesMasterTable.CustomerCode = dbo.tblCustomer.CustomerID UNION ALL " _
                    & " SELECT     dbo.SalesReturnMasterTable.SalesReturnNo, dbo.SalesReturnMasterTable.SalesReturnDate, ISNULL(dbo.tblCustomer.CustomerName, '-') AS Detail, dbo.SalesReturnMasterTable.Remarks FROM dbo.SalesReturnMasterTable LEFT OUTER JOIN  dbo.tblCustomer ON dbo.SalesReturnMasterTable.CustomerCode = dbo.tblCustomer.CustomerID UNION ALL " _
                    & " SELECT     dbo.ReceivingMasterTable.ReceivingNo, dbo.ReceivingMasterTable.ReceivingDate, ISNULL(dbo.tblVendor.VendorName, '-') AS Detail,dbo.ReceivingMasterTable.Remarks FROM         dbo.ReceivingMasterTable LEFT OUTER JOIN dbo.tblVendor ON dbo.ReceivingMasterTable.VendorId = dbo.tblVendor.VendorID UNION ALL " _
                    & " SELECT     dbo.PurchaseReturnMasterTable.PurchaseReturnNo, dbo.PurchaseReturnMasterTable.PurchaseReturnDate, ISNULL(dbo.tblVendor.VendorName, '-') AS Detail, dbo.PurchaseReturnMasterTable.Remarks FROM         dbo.PurchaseReturnMasterTable LEFT OUTER JOIN dbo.tblVendor ON dbo.PurchaseReturnMasterTable.VendorId = dbo.tblVendor.VendorID UNION ALL " _
                    & " SELECT     DispatchNo, DispatchDate, '-' AS Expr1, Remarks FROM dbo.DispatchMasterTable UNION ALL " _
                    & " SELECT     Doc_no, Doc_Date, '-' AS Expr1, remarks FROM  dbo.StockAdjustmentMaster UNION ALL " _
                    & " SELECT     dbo.WarrantyClaimMasterTable.DocNo, dbo.WarrantyClaimMasterTable.DocDate, ISNULL(dbo.tblCustomer.CustomerName, '-') AS Detail, dbo.WarrantyClaimMasterTable.Remarks FROM dbo.WarrantyClaimMasterTable LEFT OUTER JOIN dbo.tblCustomer ON dbo.WarrantyClaimMasterTable.CustomerCode = dbo.tblCustomer.CustomerID UNION ALL " _
                    & " SELECT     Production_no, Production_date, '-' AS Expr1, Remarks FROM dbo.ProductionMasterTable) AS a)) "
            dt = GetDataTable(strSQL)
            If dt IsNot Nothing Then
                Me.GrdDetail2.DataSource = Nothing
                Me.GrdDetail2.DataSource = dt
                GrdDetail2.RetrieveStructure()
                Me.GrdDetail2.AutoSizeColumns()


            End If




        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub frmMissingDocuments_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            GetAllRecords()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Call ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GrdDocHeader_RowDoubleClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowActionEventArgs) Handles GrdDocHeader.RowDoubleClick
        If e.Row.Cells(0).Value.ToString = "Stock Mismatch" Then
            CtrlGrdBar1.Visible = True
            CtrlGrdBar2.Visible = False
            Me.StockTab.SelectedTab = Me.StockTab.Tabs(1).TabPage.Tab
            CtrlGrdBar1.Visible = False
            CtrlGrdBar2.Visible = True
        ElseIf e.Row.Cells(0).Value.ToString = "Missing Documents" Then
            Me.StockTab.SelectedTab = Me.StockTab.Tabs(2).TabPage.Tab
        End If
    End Sub

    Private Sub btnRefresh_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdDetail.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdDetail.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GrdDetail.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdDetail2.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GrdDetail2.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GrdDetail.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GrdDocHeader_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles GrdDocHeader.FormattingRow

    End Sub
End Class