''13-Dec-2013 ReqId-911 Imran Ali voucher not show on voucher posting 
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports Neodynamic.SDK.Barcode
Public Class frmVoucherPostUnpost
    Implements IGeneral
    Dim voucher As VouchersMaster
    Dim vlist As List(Of VouchersMaster)

    Dim PostingAllowed As Boolean = False
    Dim UnPostingAllowed As Boolean = False

    Private Sub frmVoucherPostUnpost_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
        Try
            If e.KeyCode = Keys.F4 Then
                btnPost_Click(Nothing, Nothing)

            End If
            If e.KeyCode = Keys.Escape Then

                'btnNew_Click(btnNew, Nothing)
                'Exit Sub
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmVoucherPostUnpost_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'R-974 Ehtisham ul Haq user friendly system modification on 3-1-14 
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()

            Me.dtpFromDate.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpToDate.Value = Date.Now
            Me.rdoPost.Checked = True
            Me.rdoUnPost.Checked = False
            Me.rdoAll.Checked = False
            Me.rbGroupbyNone.Checked = True
            Me.cmbVtype.SelectedIndex = 1
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage("An error occured while loading record" & Chr(10) & ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub frmVoucherPostUnpost_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseKey = "KD83KNYYMA6XA6E7HNEL7QEUSGXGLKPQVCH7YVMEPXD7BG5FUCPA"
            Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseOwner = "LumenSoft Technologies-Standard Edition-OEM Developer License"

            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

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
            Me.grd.DataSource = New VouchersDAL()
        Catch ex As Exception

        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            'Me.dtpFromDate.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            'Me.dtpToDate.Value = Date.Now
            'Me.rdoPost.Checked = True
            'Me.rdoUnPost.Checked = False
            'Me.rdoAll.Checked = False
            btnGenerate_Click(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New VouchersDAL().Update1(vlist, Me.cmbVtype.Text) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
    ''ReqId-911 Added Condition on Voucher Type
    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Me.lblProgress.Visible = True
        Me.lblProgress.BringToFront()
        Application.DoEvents()
        Try
            ''ReqId-911
            If Me.cmbVtype.SelectedIndex = 0 Or Me.cmbVtype.SelectedIndex = -1 Then
                ShowErrorMessage("Select voucher type")
                Me.cmbVtype.Focus()
                Exit Sub
            End If
            Dim str As String = String.Empty
            str = "SELECT dbo.tblVoucher.voucher_id, dbo.tblVoucher.voucher_no, dbo.tblVoucher.voucher_date, dbo.tblDefVoucherType.voucher_type, IsNull(dbo.tblVoucher.post,0) as Post,IsNull(dbo.tblVoucher.Checked,0) as Checked, dbo.tblVoucher.source, dbo.tblVoucher.UserName, dbo.tblVoucher.CheckedByUser, dbo.tblVoucher.Posted_UserName, isnull(Att.AttachmentCount,0) as Attch_Count, Doc.LocationId, Doc.ID, Doc.Type, frm.AccessKey " _
            & " FROM dbo.tblVoucher INNER JOIN " _
            & " dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id LEFT OUTER JOIN(Select DISTINCT LocationId, SalesID as ID, SalesNo as DocNo, Type From (" _
            & " Select SalesMasterTable.LocationId, SalesMasterTable.SalesId, SalesNo,'Sales' as Type From SalesMasterTable " _
            & " Union " _
            & " Select SalesReturnMasterTable.LocationId, SalesReturnMasterTable.SalesReturnId, SalesReturnNo, 'Sales Return' as Type From SalesReturnMasterTable" _
            & " Union " _
            & " Select ReceivingMasterTable.LocationId,ReceivingMasterTable.ReceivingId, ReceivingNo, 'Purchase' as Type From ReceivingMasterTable " _
            & " Union " _
            & " Select PurchaseReturnMasterTable.LocationId,PurchaseReturnMasterTable.PurchaseReturnId, PurchaseReturnNo, 'Purchase Return' As Type From PurchaseReturnMasterTable " _
            & " Union " _
            & " Select DispatchMasterTable.LocationId,DispatchMasterTable.DispatchId, DispatchNo, 'Store Issuance' as Type From DispatchMasterTable " _
            & " Union " _
            & " Select 1 as LocationId, ProductionMasterTable.Production_Id, Production_No,  'Production' as Type  From ProductionMasterTable " _
            & " ) a) Doc On Doc.DocNo = dbo.tblVoucher.Voucher_No LEFT OUTER JOIN tblForm frm ON frm.Form_Name = tblVoucher.Source LEFT OUTER JOIN(Select Source,DocId, Count(*) as AttachmentCount From DocumentAttachment Group By DocId, Source) Att On Att.DocId = dbo.tblVoucher.Voucher_Id and Att.Source = dbo.tblVoucher.source " _
            & " WHERE (CONVERT(varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN CONVERT(datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "' , 102) AND CONVERT(datetime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "' , 102)) "
            If Me.rdoPost.Checked = True Then
                str += "AND dbo.tblVoucher.post = 1 "
            End If
            If Me.rdoUnPost.Checked = True Then
                str += "AND (dbo.tblVoucher.post = 0 Or dbo.tblVoucher.post IS NULL) "
            End If

            If Me.cmbVtype.SelectedIndex = 1 Then
                'Voucher
                str += "and dbo.tblVoucher.source not In ('frmStoreIssuence','frmPurchaseReturn','frmPurchase','frmSalesReturn','frmSales','frmProductionStore','frmReceiptVoucherNew','frmPaymentVoucherNew','frmImport')"
            ElseIf Me.cmbVtype.SelectedIndex = 2 Then
                'Sale
                str += "and dbo.tblVoucher.source In ('frmSales')"
            ElseIf Me.cmbVtype.SelectedIndex = 3 Then
                'Sales Return
                str += "and dbo.tblVoucher.source In ('frmSalesReturn')"
            ElseIf Me.cmbVtype.SelectedIndex = 4 Then
                'Purchase
                str += "and dbo.tblVoucher.source In ('frmPurchase','frmImport')"
            ElseIf Me.cmbVtype.SelectedIndex = 5 Then
                'Purchase Return
                str += "and dbo.tblVoucher.source In ('frmPurchaseReturn')"
            ElseIf Me.cmbVtype.SelectedIndex = 6 Then
                'Stroe Issuance
                str += "and dbo.tblVoucher.source In ('frmStoreIssuence')"
            ElseIf Me.cmbVtype.SelectedIndex = 7 Then
                'Production
                str += "and dbo.tblVoucher.source In ('frmProductionStore')"
            ElseIf Me.cmbVtype.SelectedIndex = 8 Then
                'Invoice Based Receipt Voucher
                str += "and dbo.tblVoucher.source In ('frmReceiptVoucherNew')"
            ElseIf Me.cmbVtype.SelectedIndex = 9 Then
                'Invoice Based Payment Voucher
                str += "and dbo.tblVoucher.source In ('frmPaymentVoucherNew')"
            End If
            Dim dt As DataTable = SBDal.UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                GroupBox1.Visible = True
                Me.rbGroupbyNone.Checked = True
            Else
                GroupBox1.Visible = False
            End If
            Me.grd.DataSource = dt
            'Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns("Voucher_No").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grd.RootTable.Columns("Attch_Count").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            'Me.grd.RootTable.Columns("Attch_Count").
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
            GetSecurityRights()

        Catch ex As Exception
            ShowErrorMessage("An error occured while loading record" & Chr(10) & ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try

            If LoginGroup = "Administrator" Then
                PostingAllowed = True
                UnPostingAllowed = True

            Else
                Me.Visible = False
                PostingAllowed = False
                UnPostingAllowed = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Post" Then
                        PostingAllowed = True
                    ElseIf Rights.Item(i).FormControlName = "UnPost" Then
                        UnPostingAllowed = True
                    End If
                Next
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        Try
            Me.grd.UpdateData()
            vlist = New List(Of VouchersMaster)
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetCheckedRows
                voucher = New VouchersMaster
                voucher.VoucherId = Val(row.Cells("voucher_id").Value.ToString)
                voucher.VoucherNo = row.Cells("voucher_no").Value.ToString
                voucher.Source = row.Cells("source").Value.ToString
                voucher.Posted_UserName = LoginUserName
                voucher.Post = True
                vlist.Add(voucher)
            Next

            If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
            If Save() = True Then
                'msg_Information(str_informUpdate)
                DialogResult = Windows.Forms.DialogResult.Yes
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnUnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnPost.Click
        Try
            Me.grd.UpdateData()
            vlist = New List(Of VouchersMaster)
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetCheckedRows
                voucher = New VouchersMaster
                voucher.VoucherId = Val(row.Cells("voucher_id").Value.ToString)
                voucher.VoucherNo = row.Cells("voucher_no").Value.ToString
                voucher.Source = row.Cells("source").Value.ToString
                voucher.Post = False
                vlist.Add(voucher)
            Next

            If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
            If Save() = True Then
                'msg_Information(str_informUpdate)
                DialogResult = Windows.Forms.DialogResult.Yes
                ReSetControls()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdoUnPost_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoUnPost.CheckedChanged, rdoPost.CheckedChanged, rdoAll.CheckedChanged
        Try
            If rdoUnPost.Checked = True Then
                Me.btnPost.Enabled = True
                Me.btnUnPost.Enabled = False
            ElseIf rdoPost.Checked = True Then

                Me.btnPost.Enabled = False
                Me.btnUnPost.Enabled = True
            Else

                Me.btnPost.Enabled = True
                Me.btnUnPost.Enabled = True

            End If

            '//Setting security rights
            If PostingAllowed = False Then
                btnPost.Enabled = False
            Else
                btnPost.Enabled = True
            End If

            If UnPostingAllowed = False Then
                btnUnPost.Enabled = False
            Else
                btnUnPost.Enabled = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_ColumnButtonClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If e.Column.Key = "Preview" Then
                Dim strSource As String = Me.grd.GetRow.Cells("source").Value.ToString
                Dim IsPrint As Boolean = False
                Select Case strSource
                    Case "frmSales"
                        IsPrint = True
                        AddRptParam("@SaleID", Me.grd.GetRow.Cells("ID").Value.ToString)
                        ShowReport("SalesInvoiceNew" & Me.grd.GetRow.Cells("LocationId").Value.ToString, , , , , , , , , , True)
                    Case "frmSalesReturn"
                        IsPrint = True
                        ShowReport("SalesReturn", "{SalesReturnMasterTable.SalesReturnId}=" & grd.GetRow.Cells("ID").Value.ToString, , , , , , , , , True)
                    Case "frmPurchase"
                        IsPrint = True
                        ShowReport("PurchaseInvoice", "{ReceivingMasterTable.ReceivingId}=" & grd.GetRow.Cells("ID").Value.ToString, , , , , , , , , True)
                    Case "frmPurchaseReturn"
                        IsPrint = True
                        ShowReport("PurchaseReturn", "{PurchaseReturnMasterTable.PurchaseReturnId}=" & grd.GetRow.Cells("ID").Value.ToString, , , , , , , , , True)
                    Case "frmStoreIssuence"
                        IsPrint = True
                        ShowReport("StoreIssuanceInvoice", "{DispatchMasterTable.DispatchId}=" & grd.GetRow.Cells("ID").Value.ToString, , , , , , , , , True)
                End Select
                If IsPrint = False Then
                    'AddRptParam("@VoucherId", Me.grd.GetRow.Cells("Voucher_Id").Value.ToString)
                    ShowReport("rptVoucher", , , , , , , DTFromGrid(Val(Me.grd.GetRow.Cells("Voucher_Id").Value.ToString)), , , True)
                End If
                If ReportViewerForContainer IsNot Nothing Then
                    'ReportViewerForContainer.TopLevel = False
                    'ReportViewerForContainer.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                    'ReportViewerForContainer.Dock = DockStyle.Fill
                    'Me.SplitContainer1.Panel2.Controls.Add(ReportViewerForContainer)
                    ReportViewerForContainer.Show()
                    ReportViewerForContainer.BringToFront()
                End If
                Exit Sub
            ElseIf e.Column.Key = "Preview Attach" Then
                AddRptParam("@CompanyName", CompanyTitle)
                AddRptParam("@CompanyAddress", CompanyAddressHeader)
                AddRptParam("@ShowHeader", True)
                DataSetShowReport("RptVoucherDocument", GetVoucherRecord(), True)
                If ReportViewerForContainer IsNot Nothing Then
                    'ReportViewerForContainer.TopLevel = False
                    'ReportViewerForContainer.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                    'ReportViewerForContainer.Dock = DockStyle.Fill
                    'Me.SplitContainer1.Panel2.Controls.Add(ReportViewerForContainer)
                    ReportViewerForContainer.Show()
                    ReportViewerForContainer.BringToFront()
                End If
            Else
                Exit Sub
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            ReportViewerForContainer = Nothing
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
                    & " FROM dbo.tblVoucher AS V INNER JOIN " _
                    & " dbo.tblVoucherDetail AS VD ON V.voucher_id = VD.voucher_id INNER JOIN " _
                    & " dbo.vwCOADetail AS COA ON VD.coa_detail_id = COA.coa_detail_id LEFT OUTER JOIN  " _
                    & " dbo.tblDefVoucherType AS VTP ON V.voucher_type_id = VTP.voucher_type_id WHERE (V.voucher_id=" & Me.grd.GetRow.Cells("voucher_id").Value & ") " _
                    & " ORDER BY VD.voucher_detail_id "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            ds.Tables.Add(dt)
            ds.Tables(0).TableName = "dtVoucher"


            strSQL = String.Empty
            strSQL = "Select DocId,FileName,Path,Convert(Image,'') as Attachment_Image From DocumentAttachment WHERE (DocId=" & Me.grd.GetRow.Cells("voucher_id").Value & ") AND Source='" & Me.grd.GetRow.Cells("Source").Value.ToString.Replace("frmVoucherNew", "frmVoucher") & "'"
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
    Private Sub grd_LinkClicked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.LinkClicked
        Try
            If e.Column.Key = "voucher_no" Then
                frmModProperty.Tags = Me.grd.GetRow.Cells("voucher_no").Text
                If IsDrillDown = True Then
                    frmMain.LoadControl(Me.grd.GetRow.Cells("AccessKey").Text.ToString)
                    System.Threading.Thread.Sleep(500)
                Else
                    frmMain.LoadControl(Me.grd.GetRow.Cells("AccessKey").Text.ToString)
                    System.Threading.Thread.Sleep(500)
                    frmModProperty.Tags = Me.grd.GetRow.Cells("voucher_no").Text
                    frmMain.LoadControl(Me.grd.GetRow.Cells("AccessKey").Text.ToString)
                    System.Threading.Thread.Sleep(500)

                End If
            ElseIf e.Column.Key = "Attch_Count" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.grd.GetRow.Cells("Source").Value.ToString.Replace("frmVoucherNew", "frmVoucher")
                frm._VoucherId = Me.grd.GetRow.Cells("Voucher_Id").Value.ToString
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load, grd.ForeColorChanged
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Voucher Posting"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function DTFromGrid(ByVal voucherID As Int32) As DataTable 'TASK42
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grd.RowCount = 0 Then Exit Function
            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
            Dim DT As New DataTable
            DT = GetDataTable("SP_RptVoucher " & voucherID & "") ' r.Cells(EnumGridMaster.Voucher_Id).Value
            DT.AcceptChanges()
            'DT.Columns.Add("Convert(image, null) as BarCode")
            'Next
            For Each DR As DataRow In DT.Rows
                DR.BeginEdit()
                Dim bcp As New BarcodeProfessional()
                'bcp.Symbology = Symbology.Code39
                bcp.Symbology = Symbology.Code128
                'bcp.Symbology = Symbology.Code93



                bcp.Extended = True
                bcp.DisplayCode = False

                ' bcp.Text=Symbology.
                'bcp.Text = String.Empty
                'bcp.Symbology = Symbology.Code128

                bcp.AddChecksum = False

                'bcp.BarWidth = 3
                'bcp.BarHeight = 0.04F
                'DR.Item("Convert(image, null) as BarCode")
                bcp.Code = "?" & DR.Item("voucher_no").ToString
                'bcp.Code = DR.Item("Employee_Code").ToString
                DR.Item("BarCode") = bcp.GetBarcodeImage(System.Drawing.Imaging.ImageFormat.Png)
                'LoadPicture(DR, "Picture", DR.Item("EmpPicture"))


                DR.EndEdit()


            Next
            Return DT
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Function

    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow


    End Sub

    Private Sub rbGroupbyType_CheckedChanged(sender As Object, e As EventArgs) Handles rbGroupbyType.CheckedChanged
        Try
            Dim GroupbyType As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Voucher_Type"))
            Dim GroupbyName As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("UserName"))
            If rbGroupbyType.Checked = True Then
                Me.grd.RootTable.Groups.Clear()
                Me.grd.RootTable.Columns(3).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
                Me.grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
                Me.grd.RootTable.Groups.Add(GroupbyType)
            ElseIf Me.rbGroupbyName.Checked = True Then
                Me.grd.RootTable.Groups.Clear()
                Me.grd.RootTable.Columns(6).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
                Me.grd.RootTable.Groups.Add(GroupbyName)
            ElseIf Me.rbGroupbyNone.Checked = True Then
                Me.grd.RootTable.Groups.Clear()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbGroupbyName_CheckedChanged(sender As Object, e As EventArgs) Handles rbGroupbyName.CheckedChanged
        Try
            Dim GroupbyType As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Voucher_Type"))
            Dim GroupbyName As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("UserName"))
            If rbGroupbyType.Checked = True Then
                Me.grd.RootTable.Groups.Clear()
                Me.grd.RootTable.Columns(3).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
                Me.grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
                Me.grd.RootTable.Groups.Add(GroupbyType)
            ElseIf Me.rbGroupbyName.Checked = True Then
                Me.grd.RootTable.Groups.Clear()
                Me.grd.RootTable.Columns(6).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count
                Me.grd.RootTable.Groups.Add(GroupbyName)
            ElseIf Me.rbGroupbyNone.Checked = True Then
                Me.grd.RootTable.Groups.Clear()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class