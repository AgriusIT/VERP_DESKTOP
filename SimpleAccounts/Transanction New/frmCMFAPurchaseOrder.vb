''18-June-2014 TASK:2695 Imran Ali CMFA Load On Purchase Order
''16-Jul-2014 TASK:2744 Imran Ali Problem Facing Record not in grid and cmfa Document Attachment On CMFA Document (Ravi)
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility

Public Class frmCMFAPurchaseOrder
    Dim IsOpenForm As Boolean = False
    Public Sub FillCombo(Optional ByVal Condition As String = "")
        Try

            Dim strSQL As String = String.Empty
            If Condition = "CMFA" Then
                strSQL = "SELECT DocId, DocNo + ' ~ ' + Convert(Varchar, DocDate, 102) as DocNo From CMFAMasterTable WHERE Status=1 AND Approved=1"
                FillDropDown(Me.cmbCMFADocment, strSQL)
            ElseIf Condition = "Vendor" Then
                strSQL = "Select coa_detail_id, detail_title, detail_code From vwCOADetail WHERE Account_Type='Vendor' ORDER BY detail_title ASC"
                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                Me.grd.RootTable.Columns("VendorId").ValueList.PopulateValueList(dt.DefaultView, "coa_detail_id", "detail_title")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillGrid(Optional ByVal Condition As String = "")
        Try
            FillGridEx(Me.grd, "SELECT CMFA.VendorId, CMFA.ArticleDefId, ART.ArticleCode, ART.ArticleDescription, ART.ArticleSizeName, ART.ArticleColorName, CMFA.ArticleSize, (IsNull(CMFA.Sz1,0)-IsNull(POQty,0)) as Qty, IsNull(CMFA.Sz7,0) as PackQty, IsNull(CMFA.Price,0) as Price FROM  dbo.CMFADetailTable AS CMFA INNER JOIN   dbo.ArticleDefView AS ART ON CMFA.ArticleDefId = ART.ArticleId WHERE DocId=" & Me.cmbCMFADocment.SelectedValue & " AND (ISNULL(CMFA.Sz1,0)-IsNull(CMFA.POQty,0)) <> 0 ORDER BY 1 ASC", False)
            FillCombo("Vendor")
            For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                If Me.grd.RootTable.Columns(c).Index <> 8 Then
                    Me.grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
                Me.grd.RootTable.Columns(c).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Next
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbCMFADocment_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCMFADocment.SelectedIndexChanged
        Try
            If IsOpenForm = True Then
                FillGrid()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmCMFAPurchaseOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmCMFAPurchaseOrder_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            FillCombo("CMFA")
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If grd Is Nothing Then Exit Sub
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            ''CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Me.Cursor = Cursors.WaitCursor
        Try

            If Me.grd.RowCount = 0 Then Exit Sub
            grd.UpdateData()
            Dim objGrdEx() As Janus.Windows.GridEX.GridEXRow = Me.grd.GetCheckedRows()
            Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
            dt.TableName = "Default"
            Dim dv As New DataView
            dv.Table = dt
            Dim str() As String = {"VendorId"}
            Dim objdt As DataTable = dv.ToTable("Vendor", True, str)
            Dim flg As Boolean = False
            For i As Integer = 0 To objdt.Rows.Count - 1
                If frmPurchaseOrderNew.grd.RowCount > 0 Then
                    Dim dtpo As DataTable = CType(frmPurchaseOrderNew.grd.DataSource, DataTable)
                    dtpo.Clear()
                    dtpo.AcceptChanges()
                End If
                frmPurchaseOrderNew.BtnSave.Text = "&Save"
                frmPurchaseOrderNew.cmbVendor.Value = objdt.Rows(i).Item(0)
                frmPurchaseOrderNew.cmbVendor_ValueChanged(Nothing, Nothing)
                frmPurchaseOrderNew.cmbCMFADoc.SelectedValue = Me.cmbCMFADocment.SelectedValue
                For Each r As Janus.Windows.GridEX.GridEXRow In objGrdEx
                    If r.Cells(1).Value.ToString = objdt.Rows(i).Item(0).ToString Then
                        frmPurchaseOrderNew.cmbItem.Value = r.Cells(2).Value.ToString
                        frmPurchaseOrderNew.cmbUnit.Text = r.Cells(7).Value.ToString
                        frmPurchaseOrderNew.txtPackQty.Text = r.Cells(9).Value.ToString
                        frmPurchaseOrderNew.txtQty.Text = Val(r.Cells(8).Value.ToString)
                        frmPurchaseOrderNew.txtRate.Text = Val(r.Cells(10).Value.ToString)
                        frmPurchaseOrderNew.btnAdd_Click(Nothing, Nothing)
                        flg = False
                    Else
                        flg = True
                    End If
                Next
                ''16-Jul-2014 TASK:2744 Imran Ali Problem Facing Record not in grid and cmfa Document Attachment On CMFA Document (Ravi)
                If frmPurchaseOrderNew.grd.RowCount > 0 Then
                    frmPurchaseOrderNew.SaveToolStripButton_Click(Nothing, Nothing)
                End If
                'End Task:2744
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.Close()
        End Try
    End Sub
End Class